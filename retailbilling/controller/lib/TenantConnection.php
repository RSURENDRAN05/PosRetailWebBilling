<?php

/**
 * =====================================================================
 * TenantConnection - multi-tenant MySQL connection resolver
 * =====================================================================
 * Resolves the client (tenant) database from the SyncId passed in the
 * request (?SyncId=123), using the master database myposqrc_master.
 *
 * Flow:
 *   1. Validate SyncId (present, alphanumeric, max length).
 *   2. Rate-limit repeated invalid requests per IP.
 *   3. Look up client_connection via prepared statement.
 *   4. Decrypt the stored DB password (AES-256-GCM, "enc:v1:" prefix).
 *   5. Open and return the mysqli connection to the client database.
 *
 * All failures emit a JSON error response and terminate the request.
 *
 * PHP 7.4+ / 8.x compatible. Requires ext-mysqli and ext-openssl.
 * =====================================================================
 */

require_once __DIR__ . '/../config/master.config.php';

final class TenantConnection
{
    private const ENC_PREFIX = 'enc:v1:';
    private const ENC_CIPHER = 'aes-256-gcm';

    /** @var mysqli|null master DB connection */
    private $masterConn = null;

    /** @var mysqli|null resolved tenant DB connection */
    private $tenantConn = null;

    /** @var array<string,mixed>|null resolved client row */
    private $client = null;

    /** @var string validated SyncId */
    private $syncId = '';

    /** @var self|null one instance per request (one tenant per request) */
    private static $instance = null;

    private function __construct()
    {
        date_default_timezone_set(defined('MYPOS_TIMEZONE') ? MYPOS_TIMEZONE : 'UTC');
    }

    /**
     * Resolve (once per request) and return the tenant mysqli connection.
     */
    public static function get(): mysqli
    {
        if (self::$instance === null) {
            self::$instance = new self();
            self::$instance->resolve();
        }
        return self::$instance->tenantConn;
    }

    /**
     * Client metadata (ClientID, ClientName, SyncId, DBName...) of the
     * resolved tenant. Password is never exposed.
     *
     * @return array<string,mixed>
     */
    public static function clientInfo(): array
    {
        self::get();
        $info = self::$instance->client;
        unset($info['DBPassword']);
        return $info;
    }

    /**
     * Raw connection credentials of the resolved tenant (password
     * decrypted). Needed only for legacy code that reconnects via the
     * DB_* constants. Do NOT expose these values in any response.
     *
     * @return array{host:string,user:string,password:string,database:string}
     */
    public static function credentials(): array
    {
        self::get();
        $c = self::$instance->client;
        return array(
            'host' => (string) $c['DBHost'],
            'user' => (string) $c['DBUser'],
            'password' => self::decryptPassword((string) $c['DBPassword']),
            'database' => (string) $c['DBName'],
        );
    }

    // -----------------------------------------------------------------
    // Resolution pipeline
    // -----------------------------------------------------------------

    private function resolve(): void
    {
        $this->syncId = $this->readSyncId();
        $this->connectMaster();
        $this->enforceRateLimit();
        $this->client = $this->fetchClient($this->syncId);
        $this->tenantConn = $this->connectTenant($this->client);
        self::writeLog('INFO', 'TENANT_CONNECTED', array(
            'syncId' => $this->syncId,
            'clientId' => (string) $this->client['ClientID'],
            'database' => (string) $this->client['DBName'],
        ));
    }

    /**
     * Read + validate SyncId from the query string.
     */
    private function readSyncId(): string
    {
        $raw = isset($_GET['SyncId']) ? trim((string) $_GET['SyncId']) : '';

        if ($raw === '') {
            // Cannot log/rate-limit against master yet without a connection;
            // connect lazily just for logging.
            $this->connectMaster(true);
            $this->enforceRateLimit();
            $this->logInvalidAttempt(null, 'MISSING_SYNCID');
            self::fail(400, 'MISSING_SYNCID', 'SyncId parameter is required.');
        }

        if (strlen($raw) > SYNCID_MAX_LENGTH || !ctype_alnum($raw)) {
            $this->connectMaster(true);
            $this->enforceRateLimit();
            $this->logInvalidAttempt($raw, 'INVALID_SYNCID');
            self::fail(400, 'INVALID_SYNCID', 'SyncId format is invalid.');
        }

        return $raw;
    }

    /**
     * Connect to the master database.
     *
     * @param bool $soft when true, failures are ignored (used for
     *                   best-effort logging paths).
     */
    private function connectMaster(bool $soft = false): void
    {
        if ($this->masterConn instanceof mysqli) {
            return;
        }

        mysqli_report(MYSQLI_REPORT_OFF);
        $conn = @mysqli_connect(MASTER_DB_HOST, MASTER_DB_USER, MASTER_DB_PASSWORD, MASTER_DB_NAME);

        if (!$conn) {
            self::writeLog('ERROR', 'MASTER_DB_CONNECTION_FAILED', array(
                'error' => mysqli_connect_error(),
            ));
            if ($soft) {
                return;
            }
            self::fail(500, 'MASTER_DB_ERROR', 'Service temporarily unavailable.');
        }

        mysqli_set_charset($conn, 'utf8mb4');
        $this->masterConn = $conn;
    }

    /**
     * Block IPs that produced too many invalid attempts recently.
     */
    private function enforceRateLimit(): void
    {
        if (!defined('RATE_LIMIT_ENABLED') || RATE_LIMIT_ENABLED !== true) {
            return;
        }

        if (!$this->masterConn) {
            return; // best effort - do not block valid traffic on log outage
        }

        $ip = self::clientIp();
        $stmt = mysqli_prepare(
            $this->masterConn,
            'SELECT COUNT(*) FROM api_access_log WHERE IPAddress = ? AND AttemptTime > (NOW() - INTERVAL ? SECOND)'
        );
        if (!$stmt) {
            return;
        }

        $window = RATE_LIMIT_WINDOW_SECONDS;
        mysqli_stmt_bind_param($stmt, 'si', $ip, $window);
        mysqli_stmt_execute($stmt);
        mysqli_stmt_bind_result($stmt, $attempts);
        mysqli_stmt_fetch($stmt);
        mysqli_stmt_close($stmt);

        if ((int) $attempts >= RATE_LIMIT_MAX_ATTEMPTS) {
            header('Retry-After: ' . RATE_LIMIT_WINDOW_SECONDS);
            self::fail(429, 'RATE_LIMITED', 'Too many invalid requests. Try again later.');
        }
    }

    /**
     * Fetch the active client row for a SyncId (prepared statement).
     *
     * @return array<string,mixed>
     */
    private function fetchClient(string $syncId): array
    {
        $stmt = mysqli_prepare(
            $this->masterConn,
            'SELECT Id, ClientID, ClientName, SyncId, DBHost, DBUser, DBPassword, DBName, Status
               FROM client_connection
              WHERE SyncId = ?
              LIMIT 1'
        );
        if (!$stmt) {
            self::writeLog('ERROR', 'MASTER_DB_PREPARE_FAILED', array(
                'syncId' => $syncId,
                'error' => mysqli_error($this->masterConn),
            ));
            self::fail(500, 'MASTER_DB_ERROR', 'Service temporarily unavailable.');
        }

        mysqli_stmt_bind_param($stmt, 's', $syncId);
        mysqli_stmt_execute($stmt);
        $result = mysqli_stmt_get_result($stmt);
        $row = $result ? mysqli_fetch_assoc($result) : null;
        mysqli_stmt_close($stmt);

        if (!$row) {
            $this->logInvalidAttempt($syncId, 'UNKNOWN_SYNCID');
            self::fail(404, 'UNKNOWN_SYNCID', 'SyncId is not registered.');
        }

        if ((int) $row['Status'] !== 1) {
            $this->logInvalidAttempt($syncId, 'CLIENT_DISABLED');
            self::fail(403, 'CLIENT_DISABLED', 'This client account is disabled.');
        }

        return $row;
    }

    /**
     * Open the tenant database connection.
     *
     * @param array<string,mixed> $client
     */
    private function connectTenant(array $client): mysqli
    {
        $password = self::decryptPassword((string) $client['DBPassword']);

        mysqli_report(MYSQLI_REPORT_OFF);
        $conn = @mysqli_connect(
            (string) $client['DBHost'],
            (string) $client['DBUser'],
            $password,
            (string) $client['DBName']
        );

        if (!$conn) {
            self::writeLog('ERROR', 'TENANT_DB_CONNECTION_FAILED', array(
                'syncId' => (string) $client['SyncId'],
                'clientId' => (string) $client['ClientID'],
                'host' => (string) $client['DBHost'],
                'user' => (string) $client['DBUser'],
                'database' => (string) $client['DBName'],
                'error' => mysqli_connect_error(),
            ));
            self::fail(500, 'TENANT_DB_ERROR', 'Unable to connect to the client database.');
        }

        mysqli_set_charset($conn, 'utf8');
        return $conn;
    }

    // -----------------------------------------------------------------
    // Password encryption helpers (AES-256-GCM)
    // -----------------------------------------------------------------

    /**
     * Encrypt a plain-text DB password for storage in client_connection.
     * Used by lib/encrypt_password.php.
     */
    public static function encryptPassword(string $plain): string
    {
        $key = self::encryptionKey();
        $iv = random_bytes(12);
        $tag = '';
        $cipher = openssl_encrypt($plain, self::ENC_CIPHER, $key, OPENSSL_RAW_DATA, $iv, $tag);
        if ($cipher === false) {
            throw new RuntimeException('Encryption failed: ' . openssl_error_string());
        }
        return self::ENC_PREFIX . base64_encode($iv . $tag . $cipher);
    }

    /**
     * Decrypt a stored password. Values without the "enc:v1:" prefix are
     * treated as legacy plain-text and returned unchanged.
     */
    public static function decryptPassword(string $stored): string
    {
        if (strncmp($stored, self::ENC_PREFIX, strlen(self::ENC_PREFIX)) !== 0) {
            return $stored; // legacy plain-text value
        }

        $blob = base64_decode(substr($stored, strlen(self::ENC_PREFIX)), true);
        if ($blob === false || strlen($blob) < 29) { // 12 IV + 16 tag + >=1 byte
            self::fail(500, 'CREDENTIAL_ERROR', 'Stored credentials are corrupt.');
        }

        $iv = substr($blob, 0, 12);
        $tag = substr($blob, 12, 16);
        $cipher = substr($blob, 28);

        $plain = openssl_decrypt($cipher, self::ENC_CIPHER, self::encryptionKey(), OPENSSL_RAW_DATA, $iv, $tag);

        // Backward compatibility for values encrypted by older versions,
        // which hashed the configured hex text instead of decoding it.
        if ($plain === false && self::hasHexEncryptionKey()) {
            $plain = openssl_decrypt(
                $cipher,
                self::ENC_CIPHER,
                hash('sha256', MYPOS_ENC_KEY, true),
                OPENSSL_RAW_DATA,
                $iv,
                $tag
            );
        }

        if ($plain === false) {
            self::writeLog('ERROR', 'PASSWORD_DECRYPTION_FAILED', array(
                'error' => 'Stored password could not be decrypted; verify MYPOS_ENC_KEY.',
            ));
            self::fail(500, 'CREDENTIAL_ERROR', 'Unable to read client credentials.');
        }
        return $plain;
    }

    private static function encryptionKey(): string
    {
        if (self::hasHexEncryptionKey()) {
            return hex2bin(MYPOS_ENC_KEY);
        }

        // Support non-hex passphrases while always producing 32 bytes.
        return hash('sha256', MYPOS_ENC_KEY, true);
    }

    private static function hasHexEncryptionKey(): bool
    {
        return strlen(MYPOS_ENC_KEY) === 64 && ctype_xdigit(MYPOS_ENC_KEY);
    }

    // -----------------------------------------------------------------
    // Logging / responses
    // -----------------------------------------------------------------

    private function logInvalidAttempt(?string $syncId, string $reason): void
    {
        self::writeLog('WARNING', $reason, array(
            'syncId' => $syncId,
        ));

        if (!$this->masterConn) {
            return;
        }

        $stmt = mysqli_prepare(
            $this->masterConn,
            'INSERT INTO api_access_log (SyncId, IPAddress, RequestUri, Reason) VALUES (?, ?, ?, ?)'
        );
        if (!$stmt) {
            self::writeLog('ERROR', 'ACCESS_LOG_PREPARE_FAILED', array(
                'error' => mysqli_error($this->masterConn),
            ));
            return;
        }

        $syncIdTrunc = $syncId !== null ? substr($syncId, 0, 64) : null;
        $ip = self::clientIp();
        $uri = isset($_SERVER['REQUEST_URI']) ? substr((string) $_SERVER['REQUEST_URI'], 0, 255) : null;

        mysqli_stmt_bind_param($stmt, 'ssss', $syncIdTrunc, $ip, $uri, $reason);
        if (!mysqli_stmt_execute($stmt)) {
            self::writeLog('ERROR', 'ACCESS_LOG_INSERT_FAILED', array(
                'error' => mysqli_stmt_error($stmt),
            ));
        }
        mysqli_stmt_close($stmt);
    }

    /**
     * Write one JSON line to the dedicated tenant log. Passwords and
     * query-string values are intentionally never included.
     *
     * @param array<string,mixed> $context
     */
    private static function writeLog(string $level, string $event, array $context = array()): void
    {
        if (defined('TENANT_LOG_ENABLED') && TENANT_LOG_ENABLED !== true) {
            return;
        }

        $entry = array_merge(array(
            'time' => date('Y-m-d H:i:s'),
            'level' => $level,
            'event' => $event,
            'ip' => self::clientIp(),
            'method' => isset($_SERVER['REQUEST_METHOD']) ? (string) $_SERVER['REQUEST_METHOD'] : 'CLI',
            'script' => isset($_SERVER['SCRIPT_NAME']) ? (string) $_SERVER['SCRIPT_NAME'] : '',
        ), $context);

        $line = json_encode($entry, JSON_UNESCAPED_SLASHES) . PHP_EOL;
        $logFile = defined('TENANT_LOG_FILE') ? TENANT_LOG_FILE : dirname(__DIR__) . '/logs/tenant_connection.log';
        $logDirectory = dirname($logFile);

        if (!is_dir($logDirectory) && !@mkdir($logDirectory, 0750, true) && !is_dir($logDirectory)) {
            error_log('[TenantConnection] Cannot create log directory: ' . $logDirectory);
            error_log(trim($line));
            return;
        }

        if (@file_put_contents($logFile, $line, FILE_APPEND | LOCK_EX) === false) {
            error_log('[TenantConnection] Cannot write dedicated log: ' . $logFile);
            error_log(trim($line));
        }
    }

    private static function clientIp(): string
    {
        // REMOTE_ADDR is the only value that cannot be spoofed by the client.
        return isset($_SERVER['REMOTE_ADDR']) ? (string) $_SERVER['REMOTE_ADDR'] : 'cli';
    }

    /**
     * Emit a JSON error response and terminate.
     */
    public static function fail(int $httpCode, string $errorCode, string $message): void
    {
        self::writeLog('ERROR', 'API_ERROR_RESPONSE', array(
            'httpCode' => $httpCode,
            'errorCode' => $errorCode,
            'message' => $message,
        ));

        if (!headers_sent()) {
            http_response_code($httpCode);
            header('Content-Type: application/json; charset=utf-8');
        }
        echo json_encode(array(
            'Success' => false,
            'ErrorCode' => $errorCode,
            'Msg' => $message,
        ));
        exit;
    }
}
