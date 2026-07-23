<?php
// ============================================================
// Face Attendance — API Router
// Single entry-point for the Python face-recognition client.
//
// Python config.py must set:
//   API_BASE_URL = "https://myposqr.com/retailbilling/controller/getfunctionfaceattendance.php"
//   ENDPOINTS = {
//       "auth"       : API_BASE_URL + "?endpoint=auth",
//       "employees"  : API_BASE_URL + "?endpoint=employees",
//       "encode"     : API_BASE_URL + "?endpoint=encode",  // proxies to Python recognition service
//       "recognize"  : API_BASE_URL + "?endpoint=recognize", // identify employee from image
//       "faces"      : API_BASE_URL + "?endpoint=faces",
//       "attendance" : API_BASE_URL + "?endpoint=attendance",
//       "stats"      : API_BASE_URL + "?endpoint=stats",
//       "branches"   : API_BASE_URL + "?endpoint=branches",
//       "locations"  : API_BASE_URL + "?endpoint=locations",
//       "getalllocations" : API_BASE_URL + "?endpoint=getalllocations",  // optional: returns all locations without filtering by branch
//   } 110426
// ============================================================

// ---------- CORS + headers ----------
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: GET, POST, DELETE, OPTIONS");
header("Access-Control-Allow-Headers: Origin, Content-Type, X-Api-Key, X-Sync-Id, Authorization");
header("Content-Type: application/json; charset=utf-8");

if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    http_response_code(200);
    exit;
}
// ---------- Recognition Service Config (Auto-Switch) ----------

$currentDomain = $_SERVER['HTTP_HOST'] ?? '';
// ---------- API Key validation ----------
define('FACE_API_KEY', 'face_attendance_secret_2024');
define('ENABLE_SERVER_ERROR_LOG', true);
define('DIRECT_RECOGNITION_SERVICE_URL', 'http://121.122.31.13:5000');
if (strpos($currentDomain, 'myposqr.com') !== false) {
    // Running ON myposqr.com — call Python directly
    define('RECOGNITION_SERVICE_URL',     DIRECT_RECOGNITION_SERVICE_URL);
    define('RECOGNITION_TEST_PATH',       '/test');
    define('RECOGNITION_ENCODE_PATH',     '/encode');
    define('RECOGNITION_RECOGNIZE_PATH',  '/recognize');
} else {
    // Running on dinainas.com (or any other domain) — proxy via myposqr.com
    define('RECOGNITION_SERVICE_URL',     'https://myposqr.com/retailbilling/controller/getfunctionfaceattendance.php');
    define('RECOGNITION_TEST_PATH',       '?endpoint=middleware_test');
    define('RECOGNITION_ENCODE_PATH',     '?endpoint=middleware_encode');
    define('RECOGNITION_RECOGNIZE_PATH',  '?endpoint=middleware_recognize');
}


function getRequestHeader($name)
{
    $key = 'HTTP_' . strtoupper(str_replace('-', '_', $name));
    return $_SERVER[$key] ?? ($_SERVER['HTTP_' . strtoupper($name)] ?? null);
}

$apiKey = getRequestHeader('X-Api-Key') ?? getRequestHeader('X-API-Key');
if ($apiKey !== FACE_API_KEY) {
    http_response_code(401);
    echo json_encode(['success' => false, 'message' => 'Unauthorized: invalid API key']);
    exit;
}

// Each client (tenant) has its OWN SyncId — there is no single shared
// default value. The SyncId just needs to be present here; it is then
// validated per-client (existence + Status=1 in client_connection)
// by TenantConnection when FaceAttendanceFunc opens the DB connection
// below. Accept it from either the query string or the X-Sync-Id
// header, and make sure it's visible to dbconnect.php/TenantConnection
// via $_GET['SyncId'] regardless of which one the client used.
$syncId = trim((string)($_GET['SyncId'] ?? getRequestHeader('X-Sync-Id') ?? ''));
if ($syncId === '') {
    http_response_code(400);
    echo json_encode(['success' => false, 'message' => 'Bad Request: SyncId is required']);
    exit;
}
$_GET['SyncId'] = $syncId;
error_log('[FaceAttendance] SyncId received from app: ' . $syncId);

// ---------- Load class ----------
// Instantiating FaceAttendanceFunc opens the tenant DB connection via
// dbconnect.php -> TenantConnection::get(), which validates $syncId
// against the client_connection table (per-client SyncId lookup) and
// terminates with a JSON error if it is unknown/disabled/invalid.
require_once 'clsfunctionfaceattendance.php';
$fa = new FaceAttendanceFunc();

// ---------- Helpers ----------
function jsonBody()
{
    $raw = file_get_contents('php://input');
    return $raw ? (json_decode($raw, true) ?? []) : [];
}

function rows($result)
{
    $out = [];
    if ($result) while ($r = mysqli_fetch_assoc($result)) $out[] = $r;
    return $out;
}

function ok($data)
{
    global $syncId;
    echo json_encode(['success' => true, 'sync_id' => $syncId, 'data' => $data]);
}
function fail($msg, $code = 400)
{
    global $syncId;
    if (defined('ENABLE_SERVER_ERROR_LOG') && ENABLE_SERVER_ERROR_LOG) {
        $context = [
            'code' => $code,
            'endpoint' => $_GET['endpoint'] ?? '',
            'method' => $_SERVER['REQUEST_METHOD'] ?? '',
            'remote_addr' => $_SERVER['REMOTE_ADDR'] ?? '',
            'sync_id' => $syncId,
        ];
        $ctx = json_encode($context, JSON_UNESCAPED_SLASHES | JSON_UNESCAPED_UNICODE);
        if ($ctx !== false && strlen($ctx) > 600) {
            $ctx = substr($ctx, 0, 600) . '...';
        }
        error_log('[FaceAttendance][ERROR] ' . $msg . ' | ' . ($ctx ?: '{}'));
    }

    http_response_code($code);
    echo json_encode(['success' => false, 'sync_id' => $syncId, 'message' => $msg]);
}

function recognitionPayload($body)
{
    if (isset($body['payload']) && is_array($body['payload'])) {
        return $body['payload'];
    }
    return is_array($body) ? $body : [];
}

function extractEncodingPayload($pyData)
{
    if (!is_array($pyData)) return null;

    // Support direct vector responses: [0.12, 0.34, ...]
    if (!empty($pyData) && isset($pyData[0]) && is_numeric($pyData[0])) {
        return $pyData;
    }

    // Support direct multi-vector responses: [[...], [...]]
    if (!empty($pyData) && isset($pyData[0]) && is_array($pyData[0])) {
        return $pyData;
    }

    $candidates = [
        $pyData['encoding'] ?? null,
        $pyData['encodings'] ?? null,
        $pyData['embedding'] ?? null,
        $pyData['face_encoding'] ?? null,
        $pyData['vector'] ?? null,
        $pyData['embeddings'] ?? null,
        isset($pyData['data']) && is_array($pyData['data']) ? ($pyData['data']['encoding'] ?? null) : null,
        isset($pyData['data']) && is_array($pyData['data']) ? ($pyData['data']['encodings'] ?? null) : null,
        isset($pyData['data']) && is_array($pyData['data']) ? ($pyData['data']['embedding'] ?? null) : null,
        isset($pyData['data']) && is_array($pyData['data']) ? ($pyData['data']['face_encoding'] ?? null) : null,
        isset($pyData['data']) && is_array($pyData['data']) ? ($pyData['data']['vector'] ?? null) : null,
        isset($pyData['data']) && is_array($pyData['data']) ? ($pyData['data']['embeddings'] ?? null) : null,
    ];

    foreach ($candidates as $candidate) {
        if (is_array($candidate) && !empty($candidate)) {
            return $candidate;
        }
    }

    return null;
}

function verifyRecognitionService($pyBase)
{
    $testUrl = $pyBase . RECOGNITION_TEST_PATH;

    $ch = curl_init($testUrl);
    curl_setopt_array($ch, [
        CURLOPT_HTTPGET        => true,
        CURLOPT_RETURNTRANSFER => true,
        CURLOPT_TIMEOUT        => 10,
        CURLOPT_HTTPHEADER     => [
            'Accept: application/json',
            'X-Api-Key: ' . FACE_API_KEY,
        ],
    ]);

    $raw = curl_exec($ch);
    $err = curl_error($ch);
    $code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
    $contentType = curl_getinfo($ch, CURLINFO_CONTENT_TYPE);
    curl_close($ch);

    if ($err || !$raw) {
        return [
            'ok' => false,
            'message' => 'Recognition test failed: ' . ($err ?: 'empty response') . ' | test=' . $testUrl,
        ];
    }

    $data = json_decode($raw, true);
    if (json_last_error() !== JSON_ERROR_NONE || !is_array($data)) {
        $preview = substr(preg_replace('/\s+/', ' ', (string)$raw), 0, 200);
        return [
            'ok' => false,
            'message' => 'Recognition test invalid JSON | test=' . $testUrl . ' | status=' . $code . ' | content_type=' . ($contentType ?: 'n/a') . ' | body=' . $preview,
        ];
    }

    $isSuccess = !empty($data['success']);
    if ($code < 200 || $code >= 300 || !$isSuccess) {
        return [
            'ok' => false,
            'message' => 'Recognition test failed | test=' . $testUrl . ' | status=' . $code . ' | body=' . substr(json_encode($data), 0, 200),
        ];
    }

    return ['ok' => true, 'message' => 'Connection successful'];
}

// ---------- Route ----------
$endpoint = strtolower(trim($_GET['endpoint'] ?? ''));
$method   = $_SERVER['REQUEST_METHOD'];
$body     = ($method === 'POST' || $method === 'DELETE') ? jsonBody() : [];

switch ($endpoint) {

    // ============================================================
    // MIDDLEWARE (MyPOSQR proxy layer)
    // Used by other domains to proxy to Python without DB access.
    // ============================================================
    case 'middleware_test':
        if ($method !== 'GET') {
            fail('GET required');
            break;
        }

        $pyBase = rtrim(getenv('FACE_RECOGNITION_URL') ?: DIRECT_RECOGNITION_SERVICE_URL, '/');
        $upstreamUrl = $pyBase . '/test';

        $ch = curl_init($upstreamUrl);
        curl_setopt_array($ch, [
            CURLOPT_HTTPGET        => true,
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_TIMEOUT        => 10,
            CURLOPT_HTTPHEADER     => [
                'Accept: application/json',
                'X-Api-Key: ' . FACE_API_KEY,
            ],
        ]);
        $pyRaw  = curl_exec($ch);
        $pyErr  = curl_error($ch);
        $pyCode = curl_getinfo($ch, CURLINFO_HTTP_CODE);
        $pyContentType = curl_getinfo($ch, CURLINFO_CONTENT_TYPE);
        curl_close($ch);

        if ($pyErr || !$pyRaw) {
            fail('Middleware test upstream unavailable: ' . ($pyErr ?: 'empty response'), 503);
            break;
        }

        $pyData = json_decode($pyRaw, true);
        if (json_last_error() !== JSON_ERROR_NONE || !is_array($pyData)) {
            $preview = substr(preg_replace('/\s+/', ' ', (string)$pyRaw), 0, 240);
            fail('Middleware test invalid JSON | upstream=' . $upstreamUrl . ' | status=' . $pyCode . ' | content_type=' . ($pyContentType ?: 'n/a') . ' | body=' . $preview, 502);
            break;
        }

        if ($pyCode < 200 || $pyCode >= 300) {
            $upstreamMessage = $pyData['message'] ?? $pyData['error'] ?? 'Upstream non-2xx response';
            fail('Middleware test failed: ' . $upstreamMessage . ' | upstream=' . $upstreamUrl . ' | status=' . $pyCode, 502);
            break;
        }

        if (!isset($pyData['success'])) {
            $pyData['success'] = true;
        }

        echo json_encode($pyData);
        break;

    case 'middleware_encode':
        if ($method !== 'POST') {
            fail('POST required');
            break;
        }

        $requestPayload = recognitionPayload($body);
        $image = $requestPayload['image_base64'] ?? ($requestPayload['image'] ?? '');
        if (!$image) {
            fail('image required');
            break;
        }

        $pyBase = rtrim(getenv('FACE_RECOGNITION_URL') ?: DIRECT_RECOGNITION_SERVICE_URL, '/');
        $upstreamUrl = $pyBase . '/encode';
        $forwardPayload = [
            'image_base64' => $image,
            'image' => $image,
            'com_id' => $requestPayload['com_id'] ?? null,
            'loc_id' => $requestPayload['loc_id'] ?? null,
        ];

        $ch = curl_init($upstreamUrl);
        curl_setopt_array($ch, [
            CURLOPT_POST           => true,
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_TIMEOUT        => 20,
            CURLOPT_HTTPHEADER     => [
                'Content-Type: application/json',
                'X-Api-Key: ' . FACE_API_KEY,
            ],
            CURLOPT_POSTFIELDS     => json_encode($forwardPayload),
        ]);
        $pyRaw  = curl_exec($ch);
        $pyErr  = curl_error($ch);
        $pyCode = curl_getinfo($ch, CURLINFO_HTTP_CODE);
        $pyContentType = curl_getinfo($ch, CURLINFO_CONTENT_TYPE);
        curl_close($ch);

        if ($pyErr || !$pyRaw) {
            fail('Middleware encode upstream unavailable: ' . ($pyErr ?: 'empty response'), 503);
            break;
        }

        $pyData = json_decode($pyRaw, true);
        if (json_last_error() !== JSON_ERROR_NONE || !is_array($pyData)) {
            $preview = substr(preg_replace('/\s+/', ' ', (string)$pyRaw), 0, 240);
            fail('Middleware encode invalid JSON | upstream=' . $upstreamUrl . ' | status=' . $pyCode . ' | content_type=' . ($pyContentType ?: 'n/a') . ' | body=' . $preview, 502);
            break;
        }

        if ($pyCode < 200 || $pyCode >= 300) {
            $upstreamMessage = $pyData['message'] ?? $pyData['error'] ?? 'Upstream non-2xx response';
            fail('Middleware encode failed: ' . $upstreamMessage . ' | upstream=' . $upstreamUrl . ' | status=' . $pyCode, 502);
            break;
        }

        if (!isset($pyData['success'])) {
            $pyData['success'] = true;
        }

        echo json_encode($pyData);
        break;

    case 'middleware_recognize':
        if ($method !== 'POST') {
            fail('POST required');
            break;
        }

        $requestPayload = recognitionPayload($body);
        $image = $requestPayload['image_base64'] ?? ($requestPayload['image'] ?? '');
        if (!$image) {
            fail('image required');
            break;
        }

        $forwardPayload = [
            'image_base64' => $image,
            'image' => $image,
            'threshold' => isset($requestPayload['threshold']) ? (float)$requestPayload['threshold'] : 0.70,
            'top_k' => isset($requestPayload['top_k']) ? max(1, (int)$requestPayload['top_k']) : 3,
            'known_encodings' => is_array($requestPayload['known_encodings'] ?? null) ? $requestPayload['known_encodings'] : [],
            'com_id' => $requestPayload['com_id'] ?? null,
            'loc_id' => $requestPayload['loc_id'] ?? null,
        ];

        $pyBase = rtrim(getenv('FACE_RECOGNITION_URL') ?: DIRECT_RECOGNITION_SERVICE_URL, '/');
        $upstreamUrl = $pyBase . '/recognize';

        $ch = curl_init($upstreamUrl);
        curl_setopt_array($ch, [
            CURLOPT_POST           => true,
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_TIMEOUT        => 25,
            CURLOPT_HTTPHEADER     => [
                'Content-Type: application/json',
                'X-Api-Key: ' . FACE_API_KEY,
            ],
            CURLOPT_POSTFIELDS     => json_encode($forwardPayload),
        ]);
        $pyRaw  = curl_exec($ch);
        $pyErr  = curl_error($ch);
        $pyCode = curl_getinfo($ch, CURLINFO_HTTP_CODE);
        $pyContentType = curl_getinfo($ch, CURLINFO_CONTENT_TYPE);
        curl_close($ch);

        if ($pyErr || !$pyRaw) {
            fail('Middleware recognize upstream unavailable: ' . ($pyErr ?: 'empty response'), 503);
            break;
        }

        $pyData = json_decode($pyRaw, true);
        if (json_last_error() !== JSON_ERROR_NONE || !is_array($pyData)) {
            $preview = substr(preg_replace('/\s+/', ' ', (string)$pyRaw), 0, 240);
            fail('Middleware recognize invalid JSON | upstream=' . $upstreamUrl . ' | status=' . $pyCode . ' | content_type=' . ($pyContentType ?: 'n/a') . ' | body=' . $preview, 502);
            break;
        }

        if ($pyCode < 200 || $pyCode >= 300) {
            $upstreamMessage = $pyData['message'] ?? $pyData['error'] ?? 'Upstream non-2xx response';
            fail('Middleware recognize failed: ' . $upstreamMessage . ' | upstream=' . $upstreamUrl . ' | status=' . $pyCode, 502);
            break;
        }

        if (!isset($pyData['success'])) {
            $pyData['success'] = true;
        }

        echo json_encode($pyData);
        break;

    // ============================================================
    // AUTH
    // POST ?endpoint=auth  { action: "admin_login"|"pin_login", ... }
    // ============================================================
    case 'auth':
        if ($method === 'GET') {
            if (($_GET['action'] ?? '') === 'admin_users') {
                $res = $fa->getAdminUsers($_GET['com_id'] ?? null, $_GET['loc_id'] ?? null);
                ok(rows($res));
            } else {
                fail('Unknown auth action', 400);
            }
            break;
        }
        if ($method !== 'POST') {
            fail('POST required');
            break;
        }
        $action = $body['action'] ?? '';
        if ($action === 'admin_login') {
            $user = $fa->adminLogin($body['username'] ?? '', $body['password'] ?? '');
            if ($user) {
                ok([
                    'user_id'  => $user['id'],
                    'username' => $user['username'],
                    'com_id'   => $user['comid'],
                    'loc_id'   => $user['locid'] ?? '',
                    'token'    => base64_encode($user['id'] . ':' . time()),
                ]);
            } else {
                fail('Invalid username or password', 401);
            }
        } elseif ($action === 'pin_login') {
            $user = $fa->pinLogin($body['loc_id'] ?? '', $body['pin'] ?? '');
            if ($user) {
                ok([
                    'user_id' => $user['id'],
                    'com_id'  => $user['comid'],
                    'loc_id'  => $user['locid'],
                    'token'   => base64_encode($user['id'] . ':' . time()),
                ]);
            } else {
                fail('Invalid PIN or location', 401);
            }
        } else {
            fail('Unknown action: ' . $action);
        }
        break;

    // ============================================================
    // EMPLOYEES
    // GET  ?endpoint=employees[&com_id=&loc_id=&search=&status=]
    // GET  ?endpoint=employees&id=EMP001
    // ============================================================
    case 'employees':
        if ($method !== 'GET') {
            fail('GET required');
            break;
        }
        if (!empty($_GET['id'])) {
            $emp = $fa->getEmployee($_GET['id']);
            $emp ? ok($emp) : fail('Employee not found', 404);
        } else {
            $active = (($_GET['status'] ?? 'active') === 'active') ? 1 : 0;
            $res    = $fa->getEmployees(
                $_GET['com_id'] ?? null,
                $_GET['loc_id'] ?? null,
                $_GET['search'] ?? null,
                $active
            );
            ok(rows($res));
        }
        break;

    // ============================================================
    // ENCODE
    // POST ?endpoint=encode  { emp_id, image: base64, com_id, loc_id, replace }
    // Proxies to Python recognition service, receives an encoding array,
    // then saves it into MySQL via saveFaceEncodings().
    // ============================================================
    case 'encode':
        if ($method !== 'POST') {
            fail('POST required');
            break;
        }
        $requestPayload = recognitionPayload($body);
        $image = $requestPayload['image_base64'] ?? ($requestPayload['image'] ?? '');
        $empId = $requestPayload['emp_id'] ?? '';
        $comId = $requestPayload['com_id'] ?? '';
        $locId = $requestPayload['loc_id'] ?? '';
        $replace = isset($requestPayload['replace']) ? (bool)$requestPayload['replace'] : true;
        if (!$image) {
            fail('image required');
            break;
        }

        $pyBase = rtrim(getenv('FACE_RECOGNITION_URL') ?: RECOGNITION_SERVICE_URL, '/');
        $health = verifyRecognitionService($pyBase);
        if (empty($health['ok'])) {
            fail($health['message'] ?? 'Recognition test failed', 503);
            break;
        }

        $upstreamUrl = $pyBase . RECOGNITION_ENCODE_PATH;
        $forwardPayload = [
            'image_base64' => $image,
            'image' => $image,
            'com_id' => $requestPayload['com_id'] ?? null,
            'loc_id' => $requestPayload['loc_id'] ?? null,
        ];

        $ch = curl_init($upstreamUrl);
        curl_setopt_array($ch, [
            CURLOPT_POST           => true,
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_TIMEOUT        => 20,
            CURLOPT_HTTPHEADER     => [
                'Content-Type: application/json',
                'X-Api-Key: ' . FACE_API_KEY,
            ],
            CURLOPT_POSTFIELDS     => json_encode($forwardPayload),
        ]);
        $pyRaw  = curl_exec($ch);
        $pyErr  = curl_error($ch);
        $pyCode = curl_getinfo($ch, CURLINFO_HTTP_CODE);
        $pyContentType = curl_getinfo($ch, CURLINFO_CONTENT_TYPE);
        curl_close($ch);
        if ($pyErr || !$pyRaw) {
            fail('Recognition service unavailable: ' . ($pyErr ?: 'empty response'), 503);
            break;
        }
        $pyData = json_decode($pyRaw, true);
        if (json_last_error() !== JSON_ERROR_NONE) {
            $preview = substr(preg_replace('/\s+/', ' ', (string)$pyRaw), 0, 240);
            fail('Recognition service returned invalid JSON | upstream=' . $upstreamUrl . ' | status=' . $pyCode . ' | content_type=' . ($pyContentType ?: 'n/a') . ' | body=' . $preview, 502);
            break;
        }
        if ($pyCode < 200 || $pyCode >= 300) {
            $upstreamMessage = $pyData['message'] ?? $pyData['error'] ?? 'Upstream non-2xx response';
            fail('Encode upstream failed: ' . $upstreamMessage . ' | upstream=' . $upstreamUrl . ' | status=' . $pyCode, 502);
            break;
        }
        if (!isset($pyData['success'])) {
            $pyData['success'] = ($pyCode >= 200 && $pyCode < 300);
        }
        $pyData['sync_id'] = $syncId;

        if (!$pyData['success']) {
            echo json_encode($pyData);
            break;
        }

        if ($empId !== '') {
            $encodings = extractEncodingPayload($pyData);
            if (!is_array($encodings) || empty($encodings)) {
                $availableKeys = implode(',', array_keys($pyData));
                fail('Recognition service did not return encoding array | keys=' . $availableKeys . ' | raw=' . substr(json_encode($pyData), 0, 300), 502);
                break;
            }

            $saveResult = $fa->saveFaceEncodings($empId, $comId, $locId, $encodings, $replace);
            if (empty($saveResult['success'])) {
                fail('Failed to save face encodings: ' . ($saveResult['message'] ?? 'Unknown error'), 500);
                break;
            }

            echo json_encode([
                'success' => true,
                'sync_id' => $syncId,
                'emp_id' => (string)$empId,
                'saved_count' => $saveResult['saved_count'] ?? 0,
                'message' => $saveResult['message'] ?? 'Face encodings saved',
                'raw' => $pyData,
            ]);
            break;
        }

        echo json_encode($pyData);
        break;

    // ============================================================
    // RECOGNIZE
    // POST ?endpoint=recognize  { image: base64, threshold?: 0..1, top_k?: int, com_id?, loc_id? }
    // Fetches employee face encodings from MySQL and passes them to Python service.
    // Python matches the probe image against the passed encodings.
    // Response normalized to:
    // {
    //   success: true,
    //   matched: true|false,
    //   emp_id: "133"|null,
    //   confidence: 0.91,
    //   raw: { ...serviceResponse }
    // }
    // ============================================================
    case 'recognize':
        if ($method !== 'POST') {
            fail('POST required');
            break;
        }

        $requestPayload = recognitionPayload($body);
        $image = $requestPayload['image_base64'] ?? ($requestPayload['image'] ?? '');
        if (!$image) {
            fail('image required');
            break;
        }

        $threshold = isset($requestPayload['threshold']) ? (float)$requestPayload['threshold'] : 0.70;
        $topK = isset($requestPayload['top_k']) ? max(1, (int)$requestPayload['top_k']) : 3;
        $comId = $requestPayload['com_id'] ?? null;
        $locId = $requestPayload['loc_id'] ?? null;

        // Fetch all face encodings from MySQL for this location/company
        $knownEncodings = [];
        $resEnc = $fa->getAllFaceEncodings($comId, $locId);
        if ($resEnc) {
            while ($row = mysqli_fetch_assoc($resEnc)) {
                $empId = (string)($row['emp_id'] ?? '');
                $encoding = $row['encoding'] ?? '[]';
                $parsed = json_decode($encoding, true);

                // Validate encoding is a numeric array
                if (is_array($parsed) && !empty($parsed) && isset($parsed[0]) && is_numeric($parsed[0])) {
                    $knownEncodings[] = [
                        'emp_id' => $empId,
                        'encoding' => $parsed,
                    ];
                }
            }
        }

        if (empty($knownEncodings)) {
            fail('No face encodings found for recognition', 404);
            break;
        }

        $payload = [
            'image_base64' => $image,
            'image' => $image,
            'threshold' => $threshold,
            'top_k' => $topK,
            'known_encodings' => $knownEncodings,  // Pass DB encodings to Python
            'com_id' => $comId,
            'loc_id' => $locId,
        ];

        // Upstream URL can be overridden via env var on server:
        //   export FACE_RECOGNITION_URL="http://127.0.0.1:8000"
        // Keep trailing slash normalized.
        $pyBase = rtrim(getenv('FACE_RECOGNITION_URL') ?: RECOGNITION_SERVICE_URL, '/');
        $health = verifyRecognitionService($pyBase);
        if (empty($health['ok'])) {
            fail($health['message'] ?? 'Recognition test failed', 503);
            break;
        }

        $upstreamUrl = $pyBase . RECOGNITION_RECOGNIZE_PATH;

        $ch = curl_init($upstreamUrl);
        curl_setopt_array($ch, [
            CURLOPT_POST           => true,
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_TIMEOUT        => 25,
            CURLOPT_HTTPHEADER     => [
                'Content-Type: application/json',
                'X-Api-Key: ' . FACE_API_KEY,
            ],
            CURLOPT_POSTFIELDS     => json_encode($payload),
        ]);
        $pyRaw  = curl_exec($ch);
        $pyErr  = curl_error($ch);
        $pyCode = curl_getinfo($ch, CURLINFO_HTTP_CODE);
        curl_close($ch);

        if ($pyErr || !$pyRaw) {
            fail('Recognition service unavailable: ' . ($pyErr ?: 'empty response') . ' | upstream=' . $upstreamUrl, 503);
            break;
        }

        $pyData = json_decode($pyRaw, true);
        if (json_last_error() !== JSON_ERROR_NONE || !is_array($pyData)) {
            $preview = substr(preg_replace('/\s+/', ' ', (string)$pyRaw), 0, 200);
            fail('Recognition service returned invalid JSON | upstream=' . $upstreamUrl . ' | status=' . $pyCode . ' | body=' . $preview, 502);
            break;
        }

        if ($pyCode < 200 || $pyCode >= 300) {
            $upstreamMessage = $pyData['message'] ?? $pyData['error'] ?? 'Upstream non-2xx response';
            fail('Recognition upstream failed: ' . $upstreamMessage . ' | upstream=' . $upstreamUrl . ' | status=' . $pyCode, 502);
            break;
        }

        // If backend returns explicit failure, pass it through.
        if (isset($pyData['success']) && !$pyData['success']) {
            echo json_encode($pyData);
            break;
        }

        // Normalize common response shapes to emp_id + confidence.
        $empId = null;
        $confidence = null;

        if (isset($pyData['emp_id'])) {
            $empId = (string)$pyData['emp_id'];
            if (isset($pyData['confidence'])) $confidence = (float)$pyData['confidence'];
        } elseif (isset($pyData['employee_id'])) {
            $empId = (string)$pyData['employee_id'];
            if (isset($pyData['confidence'])) $confidence = (float)$pyData['confidence'];
        } elseif (isset($pyData['subject'])) {
            $empId = (string)$pyData['subject'];
            if (isset($pyData['similarity'])) $confidence = (float)$pyData['similarity'];
        } elseif (isset($pyData['result']) && is_array($pyData['result']) && count($pyData['result']) > 0) {
            // CompreFace-like shape: result[0].subjects[0].{subject,similarity}
            $first = $pyData['result'][0];
            if (isset($first['subjects']) && is_array($first['subjects']) && count($first['subjects']) > 0) {
                $top = $first['subjects'][0];
                if (isset($top['subject'])) $empId = (string)$top['subject'];
                if (isset($top['similarity'])) $confidence = (float)$top['similarity'];
            }
        } elseif (isset($pyData['best_match']) && is_array($pyData['best_match'])) {
            $best = $pyData['best_match'];
            if (isset($best['emp_id'])) $empId = (string)$best['emp_id'];
            if (isset($best['employee_id'])) $empId = (string)$best['employee_id'];
            if (isset($best['subject'])) $empId = (string)$best['subject'];
            if (isset($best['confidence'])) $confidence = (float)$best['confidence'];
            if (isset($best['similarity'])) $confidence = (float)$best['similarity'];
        }

        $matched = !empty($empId);
        if (!$matched && isset($pyData['matched'])) {
            $matched = (bool)$pyData['matched'];
        }

        $normalized = [
            'success' => ($pyCode >= 200 && $pyCode < 300),
            'matched' => $matched,
            'emp_id' => $matched ? $empId : null,
            'confidence' => $confidence,
            'raw' => $pyData,
        ];

        echo json_encode($normalized);
        break;

    // ============================================================
    // FACES
    // GET    ?endpoint=faces[&com_id=&loc_id=]   → all encodings
    // GET    ?endpoint=faces&id=1           → one employee
    // POST   ?endpoint=faces  { emp_id, encodings, com_id, loc_id, replace }
    // DELETE ?endpoint=faces  { id: emp_id }  OR  ?id=1
    // ============================================================
    case 'faces':
        if ($method === 'GET') {
            if (!empty($_GET['id'])) {
                $res = $fa->getEmployeeFaceEncodings($_GET['id']);
                ok(rows($res));
            } else {
                $res = $fa->getAllFaceEncodings($_GET['com_id'] ?? null, $_GET['loc_id'] ?? null);
                ok(rows($res));
            }
        } elseif ($method === 'POST') {
            $emp_id    = $body['emp_id']    ?? '';
            $encodings = $body['encodings'] ?? [];
            $com_id    = $body['com_id']    ?? '';
            $loc_id    = $body['loc_id']    ?? '';
            $replace   = (bool)($body['replace'] ?? true);
            if (!$emp_id || !$encodings) {
                fail('emp_id and encodings required');
                break;
            }
            $result = $fa->saveFaceEncodings($emp_id, $com_id, $loc_id, $encodings, $replace);
            if (!empty($result['success'])) {
                ok($result);
            } else {
                fail('Failed to save face encodings: ' . ($result['message'] ?? 'Unknown error'));
            }
        } elseif ($method === 'DELETE') {
            $eid = $body['id'] ?? $_GET['id'] ?? '';
            if (!$eid) {
                fail('id required');
                break;
            }
            $ok = $fa->deleteFaceEncodings($eid);
            $ok ? ok(['deleted' => true]) : fail('Failed to delete face encodings');
        } else {
            fail('GET, POST or DELETE required');
        }
        break;

    // ============================================================
    // ATTENDANCE
    // GET  ?endpoint=attendance[&date=&emp=&from=&to=&com_id=&loc_id=]
    // POST ?endpoint=attendance  { emp_id, action: "checkin"|"checkout", ... }
    // ============================================================
    case 'attendance':
        if ($method === 'GET') {
            $res = $fa->getAttendance(
                $_GET['date']   ?? null,
                $_GET['emp']    ?? null,
                $_GET['from']   ?? null,
                $_GET['to']     ?? null,
                $_GET['com_id'] ?? null,
                $_GET['loc_id'] ?? null
            );
            ok(rows($res));
        } elseif ($method === 'POST') {
            $action = $body['action'] ?? '';
            $emp_id = $body['emp_id'] ?? '';
            $com_id = $body['com_id'] ?? '';
            $loc_id = $body['loc_id'] ?? '';
            if (!$emp_id) {
                fail('emp_id required');
                break;
            }

            if ($action === 'checkin') {
                $result = $fa->checkIn(
                    $emp_id,
                    $com_id,
                    $loc_id,
                    $body['source']        ?? 'face',
                    $body['method']        ?? 'face_scan',
                    $body['liveness_pass'] ?? 1,
                    $body['latitude']      ?? null,
                    $body['longitude']     ?? null,
                    $body['gps_accuracy']  ?? null
                );
                ok($result);
            } elseif ($action === 'checkout') {
                $result = $fa->checkOut(
                    $emp_id,
                    $com_id,
                    $loc_id,
                    $body['source']        ?? 'face',
                    $body['method']        ?? 'face_scan',
                    $body['liveness_pass'] ?? 1,
                    $body['latitude']      ?? null,
                    $body['longitude']     ?? null,
                    $body['gps_accuracy']  ?? null
                );
                ok($result);
            } else {
                fail('action must be checkin or checkout');
            }
        } else {
            fail('GET or POST required');
        }
        break;

    // ============================================================
    // STATS
    // GET ?endpoint=stats[&com_id=&loc_id=]
    // ============================================================
    case 'stats':
        if ($method !== 'GET') {
            fail('GET required');
            break;
        }
        $data = $fa->getStats($_GET['com_id'] ?? null, $_GET['loc_id'] ?? null);
        ok($data);
        break;

    // ============================================================
    // BRANCHES
    // GET ?endpoint=branches[&com_id=]
    // ============================================================
    case 'branches':
        if ($method !== 'GET') {
            fail('GET required');
            break;
        }
        $res = $fa->getBranches($_GET['com_id'] ?? null);
        ok(rows($res));
        break;

    // ============================================================
    // LOCATIONS
    // GET ?endpoint=locations[&branch=]
    // ============================================================
    case 'locations':
        if ($method !== 'GET') {
            fail('GET required');
            break;
        }
        $res = $fa->getLocations($_GET['branch'] ?? null);
        ok(rows($res));
        break;
    case 'getalllocations':
        if ($method !== 'GET') {
            fail('GET required');
            break;
        }
        $res = $fa->getAllLocations();
        ok(rows($res));
        break;
    default:
        fail('Unknown endpoint: ' . htmlspecialchars($endpoint), 404);
        break;
}
