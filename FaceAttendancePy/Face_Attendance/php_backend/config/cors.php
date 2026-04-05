<?php
// ============================================================
// CORS + JSON Headers + Auth Helper
// ============================================================

header('Content-Type: application/json; charset=utf-8');
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type, Authorization, X-API-Key');

// Handle preflight requests
if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    http_response_code(200);
    exit;
}

// ---- Helpers ------------------------------------------------

function sendResponse(bool $success, string $message, $data = null, int $code = 200): void {
    http_response_code($code);
    $response = ['success' => $success, 'message' => $message];
    if ($data !== null) $response['data'] = $data;
    echo json_encode($response);
    exit;
}

function getRequestBody(): array {
    $raw = file_get_contents('php://input');
    $data = json_decode($raw, true);
    return is_array($data) ? $data : [];
}

function verifyApiKey(): void {
    require_once __DIR__ . '/database.php';
    $key = $_SERVER['HTTP_X_API_KEY'] ?? ($_GET['api_key'] ?? '');
    if ($key !== API_SECRET) {
        sendResponse(false, 'Unauthorized: Invalid API key', null, 401);
    }
}

function sanitize(string $value): string {
    return htmlspecialchars(strip_tags(trim($value)));
}
