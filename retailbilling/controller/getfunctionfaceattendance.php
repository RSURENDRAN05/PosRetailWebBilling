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
//   }
// ============================================================

// ---------- CORS + headers ----------
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: GET, POST, DELETE, OPTIONS");
header("Access-Control-Allow-Headers: Origin, Content-Type, X-Api-Key, Authorization");
header("Content-Type: application/json; charset=utf-8");

if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    http_response_code(200);
    exit;
}

// ---------- API Key validation ----------
define('FACE_API_KEY', 'face_attendance_secret_2024');
define('RECOGNITION_SERVICE_URL', 'http://127.0.0.1');

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

// ---------- Load class ----------
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
    echo json_encode(['success' => true,  'data' => $data]);
}
function fail($msg, $code = 400)
{
    http_response_code($code);
    echo json_encode(['success' => false, 'message' => $msg]);
}

// ---------- Route ----------
$endpoint = strtolower(trim($_GET['endpoint'] ?? ''));
$method   = $_SERVER['REQUEST_METHOD'];
$body     = ($method === 'POST' || $method === 'DELETE') ? jsonBody() : [];

switch ($endpoint) {

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
    // POST ?endpoint=encode  { image: base64 }
    // Proxies to the Python recognition service to compute a
    // 128-float face encoding from a raw base64 image.
    // Response: { success: true, encoding: [128 floats] }
    // ============================================================
    case 'encode':
        if ($method !== 'POST') {
            fail('POST required');
            break;
        }
        $image = $body['image'] ?? '';
        if (!$image) {
            fail('image required');
            break;
        }
        $pyUrl = RECOGNITION_SERVICE_URL;
        $ch = curl_init($pyUrl . '/encode');
        curl_setopt_array($ch, [
            CURLOPT_POST           => true,
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_TIMEOUT        => 20,
            CURLOPT_HTTPHEADER     => ['Content-Type: application/json'],
            CURLOPT_POSTFIELDS     => json_encode(['image' => $image]),
        ]);
        $pyRaw  = curl_exec($ch);
        $pyErr  = curl_error($ch);
        $pyCode = curl_getinfo($ch, CURLINFO_HTTP_CODE);
        curl_close($ch);
        if ($pyErr || !$pyRaw) {
            fail('Recognition service unavailable: ' . ($pyErr ?: 'empty response'), 503);
            break;
        }
        $pyData = json_decode($pyRaw, true);
        if (json_last_error() !== JSON_ERROR_NONE) {
            fail('Recognition service returned invalid JSON', 502);
            break;
        }
        if (!isset($pyData['success'])) {
            $pyData['success'] = ($pyCode >= 200 && $pyCode < 300);
        }
        echo json_encode($pyData);
        break;

    // ============================================================
    // RECOGNIZE
    // POST ?endpoint=recognize  { image: base64, threshold?: 0..1, top_k?: int }
    // Proxies to recognition service and normalizes response to:
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

        $image = $body['image'] ?? '';
        if (!$image) {
            fail('image required');
            break;
        }

        $threshold = isset($body['threshold']) ? (float)$body['threshold'] : 0.82;
        $topK = isset($body['top_k']) ? max(1, (int)$body['top_k']) : 3;

        $payload = [
            'image' => $image,
            'threshold' => $threshold,
            'top_k' => $topK,
            // Optional passthrough context for backends that support filtering
            'com_id' => $body['com_id'] ?? null,
            'loc_id' => $body['loc_id'] ?? null,
        ];

        $pyUrl = RECOGNITION_SERVICE_URL;
        $ch = curl_init($pyUrl . '/recognize');
        curl_setopt_array($ch, [
            CURLOPT_POST           => true,
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_TIMEOUT        => 25,
            CURLOPT_HTTPHEADER     => ['Content-Type: application/json'],
            CURLOPT_POSTFIELDS     => json_encode($payload),
        ]);
        $pyRaw  = curl_exec($ch);
        $pyErr  = curl_error($ch);
        $pyCode = curl_getinfo($ch, CURLINFO_HTTP_CODE);
        curl_close($ch);

        if ($pyErr || !$pyRaw) {
            fail('Recognition service unavailable: ' . ($pyErr ?: 'empty response'), 503);
            break;
        }

        $pyData = json_decode($pyRaw, true);
        if (json_last_error() !== JSON_ERROR_NONE || !is_array($pyData)) {
            fail('Recognition service returned invalid JSON', 502);
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

    default:
        fail('Unknown endpoint: ' . htmlspecialchars($endpoint), 404);
        break;
}
