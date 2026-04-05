<?php
// ============================================================
// Authentication API
// POST /api/auth.php  {"action":"admin_login","username":"","password":""}
// POST /api/auth.php  {"action":"pin_login","loc_id":"","pin":""}
// POST /api/auth.php  {"action":"logout"}
// GET  /api/auth.php?action=me    (verify token)
// ============================================================

require_once __DIR__ . '/../config/cors.php';
require_once __DIR__ . '/../config/database.php';
require_once __DIR__ . '/../config/auth.php';

$db     = Database::getInstance()->getConnection();
$method = $_SERVER['REQUEST_METHOD'];

// ---- Token Verify -------------------------------------------
if ($method === 'GET') {
    $payload = requireAuth();
    sendResponse(true, 'Token valid', $payload);
}

if ($method !== 'POST') {
    sendResponse(false, 'Method not allowed', null, 405);
}

$body   = getRequestBody();
$action = sanitize($body['action'] ?? '');

// ---- Admin Login --------------------------------------------
if ($action === 'admin_login') {
    $username = sanitize($body['username'] ?? '');
    $password = $body['password'] ?? '';

    if (!$username || !$password) {
        sendResponse(false, 'Username and password required', null, 400);
    }

    $stmt = $db->prepare(
        "SELECT * FROM admin_users WHERE username = ? AND status = 'active'"
    );
    $stmt->execute([$username]);
    $user = $stmt->fetch();

    if (!$user || !password_verify($password, $user['password'])) {
        sendResponse(false, 'Invalid credentials', null, 401);
    }

    // Update last login
    $db->prepare("UPDATE admin_users SET last_login = NOW() WHERE id = ?")->execute([$user['id']]);

    $token = JWTHelper::encode([
        'user_id'   => $user['id'],
        'username'  => $user['username'],
        'full_name' => $user['full_name'],
        'role'      => $user['role'],
        'com_id'    => $user['com_id'],
        'branch_id' => $user['branch_id'],
        'loc_id'    => null,
        'type'      => 'admin',
    ]);

    sendResponse(true, 'Login successful', [
        'token'     => $token,
        'user'      => [
            'username'  => $user['username'],
            'full_name' => $user['full_name'],
            'role'      => $user['role'],
            'com_id'    => $user['com_id'],
            'branch_id' => $user['branch_id'],
        ]
    ]);
}

// ---- Device PIN Login (kiosk mode) --------------------------
if ($action === 'pin_login') {
    $loc_id = sanitize($body['loc_id'] ?? '');
    $pin    = sanitize($body['pin']    ?? '');

    if (!$loc_id || !$pin) {
        sendResponse(false, 'loc_id and pin required', null, 400);
    }

    $stmt = $db->prepare(
        "SELECT l.*, b.name AS branch_name, c.name AS company_name
         FROM locations l
         JOIN branches b  ON b.branch_id = l.branch_id
         JOIN companies c ON c.com_id    = l.com_id
         WHERE l.loc_id = ? AND l.device_pin = ? AND l.status = 'active'"
    );
    $stmt->execute([$loc_id, $pin]);
    $loc = $stmt->fetch();

    if (!$loc) {
        sendResponse(false, 'Invalid PIN or location not found', null, 401);
    }

    $token = JWTHelper::encode([
        'role'         => 'device',
        'com_id'       => $loc['com_id'],
        'branch_id'    => $loc['branch_id'],
        'loc_id'       => $loc_id,
        'loc_name'     => $loc['name'],
        'branch_name'  => $loc['branch_name'],
        'company_name' => $loc['company_name'],
        'type'         => 'device',
    ]);

    sendResponse(true, 'Device authenticated', [
        'token'        => $token,
        'location'     => [
            'loc_id'       => $loc_id,
            'loc_name'     => $loc['name'],
            'branch_id'    => $loc['branch_id'],
            'branch_name'  => $loc['branch_name'],
            'com_id'       => $loc['com_id'],
            'company_name' => $loc['company_name'],
        ]
    ]);
}

sendResponse(false, 'Unknown action', null, 400);
