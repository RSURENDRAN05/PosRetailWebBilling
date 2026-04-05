<?php
// ============================================================
// Stats API — uses pos_employeeinfo + employee_attendance
// ============================================================

require_once __DIR__ . '/../config/cors.php';
require_once __DIR__ . '/../config/database.php';
require_once __DIR__ . '/../config/auth.php';

if ($_SERVER['REQUEST_METHOD'] !== 'GET') {
    sendResponse(false, 'Method not allowed', null, 405);
}

$auth  = requireApiKeyOrAuth();
$db    = Database::getInstance()->getConnection();
$today = date('Y-m-d');

// Build tenant WHERE
$comId = $auth['com_id'] ?? sanitize($_GET['com_id'] ?? '');
$locId = $auth['loc_id'] ?? sanitize($_GET['loc_id'] ?? '');

// Employee WHERE (pos_employeeinfo uses emp_compid / emp_locid)
$eWhere = "WHERE e.emp_active = 1";
$eParams = [];
if ($comId) { $eWhere .= " AND e.emp_compid = ?"; $eParams[] = $comId; }
if ($locId) { $eWhere .= " AND e.emp_locid = ?";  $eParams[] = $locId; }

// Attendance WHERE
$aWhere  = "WHERE a.att_date = ?";
$aParams = [$today];
if ($comId) { $aWhere .= " AND a.com_id = ?"; $aParams[] = $comId; }
if ($locId) { $aWhere .= " AND a.loc_id = ?"; $aParams[] = $locId; }

// ---- Total employees ----------------------------------------
$totalStmt = $db->prepare("SELECT COUNT(*) FROM pos_employeeinfo e $eWhere");
$totalStmt->execute($eParams);
$totalEmp = (int)$totalStmt->fetchColumn();

// ---- Today's present / late ---------------------------------
$presentStmt = $db->prepare("SELECT COUNT(*) FROM employee_attendance a $aWhere AND a.morning_in IS NOT NULL");
$presentStmt->execute($aParams);
$present = (int)$presentStmt->fetchColumn();

$lateStmt = $db->prepare("SELECT COUNT(*) FROM employee_attendance a $aWhere AND TIME(a.morning_in) > '09:00:00'");
$lateStmt->execute($aParams);
$late = (int)$lateStmt->fetchColumn();

// ---- Biometric registration status -------------------------
$faceStmt = $db->prepare(
    "SELECT COUNT(DISTINCT fe.emp_id) FROM employee_face_encodings fe
     JOIN pos_employeeinfo e ON e.emp_id = fe.emp_id $eWhere"
);
$faceStmt->execute($eParams);
$faceRegistered = (int)$faceStmt->fetchColumn();

$fpStmt = $db->prepare(
    "SELECT COUNT(DISTINCT fp.emp_id) FROM employee_fingerprints fp
     JOIN pos_employeeinfo e ON e.emp_id = fp.emp_id $eWhere"
);
$fpStmt->execute($eParams);
$fpRegistered = (int)$fpStmt->fetchColumn();

// ---- By source today (face / fingerprint / web / mobile) ----
$sourceStmt = $db->prepare(
    "SELECT att_source, COUNT(*) AS cnt
     FROM employee_attendance a $aWhere AND a.morning_in IS NOT NULL
     GROUP BY att_source"
);
$sourceStmt->execute($aParams);
$bySource = [];
foreach ($sourceStmt->fetchAll() as $r) {
    $bySource[$r['att_source']] = (int)$r['cnt'];
}

// ---- Recent check-ins today (last 10) -----------------------
$recentStmt = $db->prepare(
    "SELECT
        a.emp_id,
        e.emp_printname     AS employee_name,
        e.emp_designation,
        a.morning_in        AS check_in,
        a.evening_out       AS check_out,
        a.att_source        AS source,
        a.liveness_pass,
        CASE
            WHEN TIME(a.morning_in) > '09:00:00' THEN 'late'
            ELSE 'present'
        END                 AS status
     FROM employee_attendance a
     JOIN pos_employeeinfo e ON e.emp_id = a.emp_id
     $aWhere AND a.morning_in IS NOT NULL
     ORDER BY a.morning_in DESC
     LIMIT 10"
);
$recentStmt->execute($aParams);
$recent = $recentStmt->fetchAll();

// ---- Monthly chart (last 30 days) ---------------------------
$monthWhere  = str_replace("a.att_date = ?", "a.att_date >= DATE_SUB(NOW(), INTERVAL 30 DAY)", $aWhere);
$monthParams = array_slice($aParams, 1);  // drop today's date param
$monthStmt   = $db->prepare(
    "SELECT a.att_date AS date, COUNT(*) AS count
     FROM employee_attendance a $monthWhere AND a.morning_in IS NOT NULL
     GROUP BY a.att_date ORDER BY a.att_date DESC LIMIT 30"
);
$monthStmt->execute($monthParams);
$monthChart = $monthStmt->fetchAll();

// ---- Not yet checked in today (absent) ----------------------
$absentCount = max(0, $totalEmp - $present);

sendResponse(true, 'OK', [
    'today' => [
        'date'            => $today,
        'total_employees' => $totalEmp,
        'present'         => $present,
        'absent'          => $absentCount,
        'late'            => $late,
        'face_registered' => $faceRegistered,
        'fp_registered'   => $fpRegistered,
        'unregistered'    => max(0, $totalEmp - $faceRegistered),
    ],
    'by_source'          => $bySource,
    'recent'             => $recent,
    'monthly_chart'      => $monthChart,
    'context'            => [
        'com_id' => $comId,
        'loc_id' => $locId,
    ],
]);
