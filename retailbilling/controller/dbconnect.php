<?php

/**
 * =====================================================================
 * dbconnect.php - multi-tenant database connection (drop-in include)
 * =====================================================================
 * Every API only needs:
 *
 *     require_once 'dbconnect.php';
 *     $conn = db();                       // tenant mysqli connection
 *
 * or (legacy style, used by existing cls* classes - still works):
 *
 *     $db = new database();
 *     $conn = $db->connect();
 *
 * The connection is selected dynamically from the SyncId in the URL
 * (?SyncId=123) via the master database myposqrc_master. See
 * lib/TenantConnection.php for validation, rate limiting, logging and
 * password decryption.
 *
 * On any failure this include emits a JSON error and terminates, so
 * code after the include can safely assume a working connection.
 * =====================================================================
 */

require_once __DIR__ . '/lib/TenantConnection.php';

header('Content-Type: application/json; charset=utf-8');

// Resolve the tenant connection immediately (validates SyncId,
// rate-limits, logs invalid attempts, emits JSON errors on failure).
$GLOBALS['MYPOS_TENANT_CONN'] = \TenantConnection::get();

// ---------------------------------------------------------------------
// Legacy compatibility: some classes (clsfunsynctocloudlocal,
// clsfunctionchqmodule) reconnect using the DB_* constants directly.
// Define them from the resolved tenant so that path keeps working.
// ---------------------------------------------------------------------
if (!defined('DB_HOST')) {
    $myposCreds = \TenantConnection::credentials();
    define('DB_HOST', $myposCreds['host']);
    define('DB_USER', $myposCreds['user']);
    define('DB_PASSWORD', $myposCreds['password']);
    define('DB_DATABASE', $myposCreds['database']);
    unset($myposCreds);
}

/**
 * Preferred accessor for the tenant connection.
 */
function db(): mysqli
{
    return $GLOBALS['MYPOS_TENANT_CONN'];
}

/**
 * Legacy wrapper kept for backward compatibility with existing code:
 *     $db = new database(); $conn = $db->connect();
 */
class database
{
    public $title = 'Mypos Portal';
    public $Version = 'Admin Ver 1.0';

    /** @var mysqli */
    private $conn;

    public function __construct()
    {
        $this->conn = \TenantConnection::get();
    }

    public function connect(): mysqli
    {
        return $this->conn;
    }
}

// Legacy alias: clsfunctionchqmodule.php uses `new Db_Connect()`.
if (!class_exists('Db_Connect', false)) {
    class Db_Connect extends database
    {
    }
}
