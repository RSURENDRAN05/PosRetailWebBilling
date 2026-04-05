<?php
// ============================================================
// JWT Auth Helper — no external library needed
// ============================================================

define('JWT_SECRET', 'face_att_jwt_secret_change_me_2024');
define('JWT_EXPIRY',  86400);   // 24 hours (seconds)
define('PIN_EXPIRY', 43200);   // 12 hours for device PIN sessions

class JWTHelper {

    // ---- Encode ---------------------------------------------
    public static function encode(array $payload): string {
        $header  = self::b64url(json_encode(['alg' => 'HS256', 'typ' => 'JWT']));
        $payload['iat'] = time();
        $payload['exp'] = time() + JWT_EXPIRY;
        $body    = self::b64url(json_encode($payload));
        $sig     = self::b64url(hash_hmac('sha256', "$header.$body", JWT_SECRET, true));
        return "$header.$body.$sig";
    }

    // ---- Decode (returns payload array or null on failure) --
    public static function decode(string $token): ?array {
        $parts = explode('.', $token);
        if (count($parts) !== 3) return null;

        [$header, $body, $sig] = $parts;
        $expected = self::b64url(hash_hmac('sha256', "$header.$body", JWT_SECRET, true));
        if (!hash_equals($expected, $sig)) return null;

        $payload = json_decode(base64_decode(strtr($body, '-_', '+/')), true);
        if (!$payload || (isset($payload['exp']) && $payload['exp'] < time())) return null;

        return $payload;
    }

    private static function b64url(string $data): string {
        return rtrim(strtr(base64_encode($data), '+/', '-_'), '=');
    }
}

// ---- Require JWT from Authorization header ------------------
function requireAuth(): array {
    $header = $_SERVER['HTTP_AUTHORIZATION'] ?? '';
    $token  = str_starts_with($header, 'Bearer ') ? substr($header, 7) : '';
    if (!$token) {
        sendResponse(false, 'Unauthorized: No token provided', null, 401);
    }
    $payload = JWTHelper::decode($token);
    if (!$payload) {
        sendResponse(false, 'Unauthorized: Invalid or expired token', null, 401);
    }
    return $payload;
}

// ---- Require API Key (for Python client) -------------------
function requireApiKeyOrAuth(): array {
    require_once __DIR__ . '/database.php';
    $apiKey = $_SERVER['HTTP_X_API_KEY'] ?? ($_GET['api_key'] ?? '');
    if ($apiKey === API_SECRET) {
        // API key auth — return minimal context
        return ['role' => 'api', 'com_id' => null, 'branch_id' => null, 'loc_id' => null];
    }
    return requireAuth();
}

// ---- Apply com_id/branch_id/loc_id filter from context -----
function applyTenantFilter(array $auth, array &$params, string &$where): void {
    if ($auth['role'] !== 'api' && !empty($auth['com_id'])) {
        $where   .= " AND com_id = ?";
        $params[] = $auth['com_id'];
    }
    if (!empty($auth['branch_id'])) {
        $where   .= " AND branch_id = ?";
        $params[] = $auth['branch_id'];
    }
    if (!empty($auth['loc_id'])) {
        $where   .= " AND loc_id = ?";
        $params[] = $auth['loc_id'];
    }
}
