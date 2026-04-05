<?php
// ============================================================
// Employees API — uses existing pos_employeeinfo table
// Column mapping:
//   emp_compid = com_id    emp_locid = loc_id
//   emp_active = 1(active) / 0(inactive)
//   emp_printname = display name
// ============================================================

require_once __DIR__ . '/../config/cors.php';
require_once __DIR__ . '/../config/database.php';
require_once __DIR__ . '/../config/auth.php';

$auth  = requireApiKeyOrAuth();
$db    = Database::getInstance()->getConnection();
$empId = isset($_GET['id']) ? sanitize($_GET['id']) : null;

switch ($_SERVER['REQUEST_METHOD']) {

    // ---- LIST / GET SINGLE ----------------------------------
    case 'GET':
        $where  = "WHERE e.emp_active = ?";
        $active = (isset($_GET['status']) && $_GET['status'] === 'inactive') ? 0 : 1;
        $params = [$active];

        // Tenant filters
        $comId = $auth['com_id'] ?? sanitize($_GET['com_id'] ?? '');
        $locId = $auth['loc_id'] ?? sanitize($_GET['loc_id'] ?? '');
        if ($comId) { $where .= " AND e.emp_compid = ?"; $params[] = $comId; }
        if ($locId) { $where .= " AND e.emp_locid = ?";  $params[] = $locId; }

        if ($empId) {
            $where .= " AND e.emp_id = ?"; $params[] = $empId;
        }
        if (!empty($_GET['search'])) {
            $like    = "%" . sanitize($_GET['search']) . "%";
            $where  .= " AND (e.emp_printname LIKE ? OR e.emp_firstname LIKE ?
                          OR e.emp_lastname LIKE ? OR e.emp_id LIKE ?)";
            $params  = array_merge($params, [$like, $like, $like, $like]);
        }
        if (!empty($_GET['designation'])) {
            $where  .= " AND e.emp_designation = ?";
            $params[] = sanitize($_GET['designation']);
        }
        if (!empty($_GET['branch_id'])) {
            // branch_id maps to location — use loc_id
            $where  .= " AND e.emp_locid = ?";
            $params[] = sanitize($_GET['branch_id']);
        }

        $stmt = $db->prepare(
            "SELECT
                e.emp_id,
                e.emp_firstname,
                e.emp_lastname,
                e.emp_printname,
                e.emp_designation,
                e.emp_compid        AS com_id,
                e.emp_locid         AS loc_id,
                e.emp_contactno     AS phone,
                e.emp_joindate,
                e.emp_dob,
                e.emp_active        AS status,
                e.emp_image,
                e.emp_created,
                e.emp_nationality,
                e.emp_currentstatus,
                e.emp_remarks,
                -- Biometric status
                (SELECT COUNT(*) FROM employee_face_encodings fe
                 WHERE fe.emp_id = e.emp_id)                    AS face_count,
                (SELECT COUNT(*) FROM employee_fingerprints fp
                 WHERE fp.emp_id = e.emp_id)                    AS fingerprint_count,
                -- Today's attendance
                (SELECT morning_in FROM employee_attendance a
                 WHERE a.emp_id = e.emp_id AND a.att_date = CURDATE()
                 LIMIT 1)                                       AS today_check_in,
                (SELECT evening_out FROM employee_attendance a
                 WHERE a.emp_id = e.emp_id AND a.att_date = CURDATE()
                 LIMIT 1)                                       AS today_check_out
             FROM pos_employeeinfo e
             $where
             ORDER BY e.emp_printname ASC"
        );
        $stmt->execute($params);
        $data = $empId ? $stmt->fetch() : $stmt->fetchAll();

        if ($empId && !$data) {
            sendResponse(false, 'Employee not found', null, 404);
        }
        sendResponse(true, 'OK', $data);
        break;

    // ---- GET EMPLOYEE BY SEARCH (for kiosk dropdown) --------
    // Handled above via GET with search param

    default:
        sendResponse(false, 'Only GET supported for employee listing. Use your existing POS system to add/edit employees.', null, 405);
}

// NOTE: Employee CREATE/UPDATE/DELETE is handled by the existing
// POS system (pos_employeeinfo is a shared table).
// This API is READ-ONLY for employee info.
// Face registration is via /api/faces.php
