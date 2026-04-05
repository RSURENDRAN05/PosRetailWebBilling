<?php
// ============================================================
// Face Encodings API
// Table: employee_face_encodings
// Links to: pos_employeeinfo (via emp_id)
// Mirrors: employee_fingerprints structure
// ============================================================

require_once __DIR__ . '/../config/cors.php';
require_once __DIR__ . '/../config/database.php';
require_once __DIR__ . '/../config/auth.php';

$auth  = requireApiKeyOrAuth();
$db    = Database::getInstance()->getConnection();
$empId = isset($_GET['id']) ? sanitize($_GET['id']) : null;

switch ($_SERVER['REQUEST_METHOD']) {

    // ---- GET ALL ENCODINGS (for Python/JS recognition) ------
    case 'GET':
        $comId = $auth['com_id'] ?? sanitize($_GET['com_id'] ?? '');
        $locId = $auth['loc_id'] ?? sanitize($_GET['loc_id'] ?? '');

        if ($empId) {
            // Get encodings for a specific employee
            $stmt = $db->prepare(
                "SELECT fe.*,
                        e.emp_printname    AS employee_name,
                        e.emp_firstname,
                        e.emp_lastname,
                        e.emp_designation,
                        e.emp_compid       AS com_id,
                        e.emp_locid        AS loc_id
                 FROM employee_face_encodings fe
                 JOIN pos_employeeinfo e ON e.emp_id = fe.emp_id
                 WHERE fe.emp_id = ?
                 ORDER BY fe.sample_index"
            );
            $stmt->execute([$empId]);
        } else {
            // Get ALL active employee encodings (used for real-time recognition)
            $where  = "WHERE e.emp_active = 1";
            $params = [];
            if ($comId) { $where .= " AND fe.com_id = ?";  $params[] = $comId; }
            if ($locId) { $where .= " AND fe.loc_id = ?";  $params[] = $locId; }
            if (isset($_GET['branch_id'])) {
                $where  .= " AND fe.loc_id = ?";
                $params[] = sanitize($_GET['branch_id']);
            }

            $stmt = $db->prepare(
                "SELECT
                    fe.id,
                    fe.emp_id,
                    fe.encoding,
                    fe.sample_index,
                    fe.face_name,
                    fe.facetype,
                    fe.com_id,
                    fe.loc_id,
                    e.emp_printname    AS employee_name,
                    CONCAT(e.emp_firstname, ' ', e.emp_lastname) AS full_name,
                    e.emp_designation,
                    e.emp_compid,
                    e.emp_locid
                 FROM employee_face_encodings fe
                 JOIN pos_employeeinfo e ON e.emp_id = fe.emp_id
                 $where
                 ORDER BY fe.emp_id, fe.sample_index"
            );
            $stmt->execute($params);
        }

        $rows = $stmt->fetchAll();

        // Parse JSON encoding strings back to arrays for the client
        foreach ($rows as &$row) {
            $decoded = json_decode($row['encoding'], true);
            $row['encoding'] = is_array($decoded) ? $decoded : [];
            // Prefer emp_printname, fallback to full_name
            if (empty($row['employee_name'])) {
                $row['employee_name'] = $row['full_name'] ?? $row['emp_id'];
            }
        }
        unset($row);

        sendResponse(true, 'OK', $rows);
        break;

    // ---- SAVE FACE ENCODING(S) for an employee --------------
    case 'POST':
        $body  = getRequestBody();
        $empId = sanitize($body['emp_id'] ?? '');
        $comId = sanitize($body['com_id'] ?? ($auth['com_id'] ?? ''));
        $locId = sanitize($body['loc_id'] ?? ($auth['loc_id'] ?? ''));

        if (!$empId) {
            sendResponse(false, 'emp_id required', null, 400);
        }
        if (empty($body['encodings']) || !is_array($body['encodings'])) {
            sendResponse(false, 'encodings array (128-float arrays) required', null, 400);
        }

        // Verify employee exists in pos_employeeinfo
        $check = $db->prepare(
            "SELECT emp_id, emp_printname, emp_compid, emp_locid
             FROM pos_employeeinfo WHERE emp_id = ? AND emp_active = 1"
        );
        $check->execute([$empId]);
        $emp = $check->fetch();

        if (!$emp) {
            sendResponse(false, "Employee '$empId' not found in pos_employeeinfo or is inactive", null, 404);
        }

        // Auto-fill com_id and loc_id from employee record if not provided
        if (!$comId) $comId = $emp['emp_compid'];
        if (!$locId) $locId = $emp['emp_locid'];

        // Replace existing encodings for this employee?
        if (!empty($body['replace'])) {
            $del = $db->prepare("DELETE FROM employee_face_encodings WHERE emp_id = ?");
            $del->execute([$empId]);
        }

        // Get current max sample_index
        $maxStmt = $db->prepare(
            "SELECT COALESCE(MAX(sample_index), -1) FROM employee_face_encodings WHERE emp_id = ?"
        );
        $maxStmt->execute([$empId]);
        $startIdx = (int)$maxStmt->fetchColumn() + 1;

        $insert = $db->prepare(
            "INSERT INTO employee_face_encodings
                (emp_id, com_id, loc_id, encoding, sample_index, face_name, facetype)
             VALUES (?, ?, ?, ?, ?, ?, ?)"
        );

        $saved    = 0;
        $faceName = sanitize($body['face_name'] ?? 'face');
        $faceType = sanitize($body['facetype']  ?? 'full');

        foreach ($body['encodings'] as $i => $enc) {
            if (!is_array($enc) || count($enc) !== 128) continue;  // Must be 128 floats
            $insert->execute([
                $empId,
                $comId,
                $locId,
                json_encode($enc),
                $startIdx + $i,
                $faceName,
                $faceType,
            ]);
            $saved++;
        }

        if ($saved === 0) {
            sendResponse(false, 'No valid 128-dimension face encodings provided. Each encoding must have exactly 128 values.', null, 400);
        }

        sendResponse(true, "Saved $saved face encoding(s) for {$emp['emp_printname']} ($empId)", [
            'emp_id'    => $empId,
            'emp_name'  => $emp['emp_printname'],
            'saved'     => $saved,
            'com_id'    => $comId,
            'loc_id'    => $locId,
        ], 201);
        break;

    // ---- DELETE FACE DATA for an employee -------------------
    case 'DELETE':
        if (!$empId) sendResponse(false, 'emp_id required', null, 400);

        // Verify employee exists
        $check = $db->prepare("SELECT emp_printname FROM pos_employeeinfo WHERE emp_id = ?");
        $check->execute([$empId]);
        $emp = $check->fetch();
        if (!$emp) sendResponse(false, 'Employee not found', null, 404);

        $stmt = $db->prepare("DELETE FROM employee_face_encodings WHERE emp_id = ?");
        $stmt->execute([$empId]);
        $deleted = $stmt->rowCount();

        sendResponse(true, "Deleted $deleted face encoding(s) for {$emp['emp_printname']}", [
            'emp_id'  => $empId,
            'deleted' => $deleted,
        ]);
        break;

    default:
        sendResponse(false, 'Method not allowed', null, 405);
}
