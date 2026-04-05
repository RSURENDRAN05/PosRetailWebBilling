<?php
// ============================================================
// Attendance API — uses existing employee_attendance table
//
// Existing columns used:
//   att_id, emp_id, com_id, loc_id, pm_id
//   att_date
//   morning_in   → check-in time
//   morning_out  → break start (optional)
//   break_in     → break end (optional)
//   evening_out  → check-out time
//   total_morning_hours, total_break_hours, total_work_hours
//   created_at, updated_at
//
// New columns added by integrate_existing_db.sql:
//   att_source  (face / fingerprint / web / mobile / manual)
//   att_method  (face_scan / web_face_api / fp_device)
//   latitude, longitude, gps_accuracy
//   liveness_pass
// ============================================================

require_once __DIR__ . '/../config/cors.php';
require_once __DIR__ . '/../config/database.php';
require_once __DIR__ . '/../config/auth.php';

$auth = requireApiKeyOrAuth();
$db   = Database::getInstance()->getConnection();

// ---- Work start time (for late detection) ------------------
define('WORK_START', '09:00:00');

switch ($_SERVER['REQUEST_METHOD']) {

    // ==========================================================
    // GET — Fetch attendance records
    // ==========================================================
    case 'GET':
        $comId = $auth['com_id'] ?? sanitize($_GET['com_id'] ?? '');
        $locId = $auth['loc_id'] ?? sanitize($_GET['loc_id'] ?? '');

        $where  = "WHERE 1=1";
        $params = [];

        if ($comId) { $where .= " AND a.com_id = ?";  $params[] = $comId; }
        if ($locId) { $where .= " AND a.loc_id = ?";  $params[] = $locId; }

        // Employee filter
        if (!empty($_GET['emp'])) {
            $where  .= " AND a.emp_id = ?";
            $params[] = sanitize($_GET['emp']);
        }

        // Date range filter
        if (!empty($_GET['from']) && !empty($_GET['to'])) {
            $where  .= " AND a.att_date BETWEEN ? AND ?";
            $params[] = sanitize($_GET['from']);
            $params[] = sanitize($_GET['to']);
        } elseif (!empty($_GET['from'])) {
            $where  .= " AND a.att_date >= ?";
            $params[] = sanitize($_GET['from']);
        } elseif (!isset($_GET['all'])) {
            // Default: today
            $where  .= " AND a.att_date = ?";
            $params[] = sanitize($_GET['date'] ?? date('Y-m-d'));
        }

        // Source filter
        if (!empty($_GET['source'])) {
            $where  .= " AND a.att_source = ?";
            $params[] = sanitize($_GET['source']);
        }

        $limit = isset($_GET['limit']) ? " LIMIT " . (int)$_GET['limit'] : " LIMIT 500";

        $stmt = $db->prepare(
            "SELECT
                a.att_id,
                a.emp_id,
                a.com_id,
                a.loc_id,
                a.att_date,
                a.morning_in                              AS check_in,
                a.morning_out                             AS break_out,
                a.break_in                                AS break_return,
                a.evening_out                             AS check_out,
                a.total_morning_hours,
                a.total_break_hours,
                a.total_work_hours,
                a.att_source,
                a.att_method,
                a.latitude,
                a.longitude,
                a.liveness_pass,
                a.created_at,
                -- Employee details from pos_employeeinfo
                e.emp_printname                           AS employee_name,
                CONCAT(e.emp_firstname,' ',e.emp_lastname) AS full_name,
                e.emp_designation,
                e.emp_image,
                -- Status calculation
                CASE
                    WHEN a.evening_out IS NOT NULL
                         THEN 'completed'
                    WHEN a.morning_in IS NOT NULL
                         AND TIME(a.morning_in) > WORK_START
                         THEN 'late'
                    WHEN a.morning_in IS NOT NULL
                         THEN 'present'
                    ELSE 'absent'
                END                                       AS status
             FROM employee_attendance a
             JOIN pos_employeeinfo e ON e.emp_id = a.emp_id
             $where
             ORDER BY a.att_date DESC, a.morning_in DESC
             $limit"
        );
        $stmt->execute($params);
        $records = $stmt->fetchAll();

        // Summary counts
        $present  = 0; $late = 0;
        foreach ($records as $r) {
            if ($r['status'] === 'present' || $r['status'] === 'completed') $present++;
            if ($r['status'] === 'late') $late++;
        }

        sendResponse(true, 'OK', [
            'records' => $records,
            'summary' => [
                'total'   => count($records),
                'present' => $present,
                'late'    => $late,
            ],
        ]);
        break;

    // ==========================================================
    // POST — Mark attendance (check-in or check-out)
    // ==========================================================
    case 'POST':
        $body     = getRequestBody();
        $empId    = sanitize($body['emp_id']      ?? '');
        $action   = sanitize($body['action']     ?? 'checkin');
        $source   = sanitize($body['source']     ?? 'face');
        $method   = sanitize($body['method']     ?? 'face_scan');
        $comId    = sanitize($body['com_id']     ?? ($auth['com_id'] ?? ''));
        $locId    = sanitize($body['loc_id']     ?? ($auth['loc_id'] ?? ''));
        $liveness = (int)($body['liveness_pass'] ?? 1);
        $lat      = !empty($body['latitude'])  ? (float)$body['latitude']  : null;
        $lng      = !empty($body['longitude']) ? (float)$body['longitude'] : null;
        $gpsAcc   = isset($body['gps_accuracy']) ? (float)$body['gps_accuracy']
              : (isset($body['accuracy']) ? (float)$body['accuracy'] : null);

        if (!$empId) {
            sendResponse(false, 'emp_id required', null, 400);
        }

        // Look up employee in pos_employeeinfo
        $empStmt = $db->prepare(
            "SELECT emp_id, emp_printname, emp_compid, emp_locid
             FROM pos_employeeinfo
             WHERE emp_id = ? AND emp_active = 1"
        );
        $empStmt->execute([$empId]);
        $emp = $empStmt->fetch();

        if (!$emp) {
            sendResponse(false, "Active employee '$empId' not found in pos_employeeinfo", null, 404);
        }

        // Auto-fill com_id / loc_id from employee record
        if (!$comId) $comId = $emp['emp_compid'];
        if (!$locId) $locId = $emp['emp_locid'];

        $today  = date('Y-m-d');
        $now    = date('Y-m-d H:i:s');
        $pmId   = (int)($body['pm_id'] ?? 0);

        // Check existing record for today
        $existing = $db->prepare(
            "SELECT att_id, morning_in, morning_out, break_in, evening_out
             FROM employee_attendance
             WHERE emp_id = ? AND att_date = ?
             LIMIT 1"
        );
        $existing->execute([$empId, $today]);
        $record = $existing->fetch();

        // ---- CHECK IN -------------------------------------------
        if ($action === 'checkin') {
            if ($record && $record['morning_in']) {
                sendResponse(false, "Already checked in today at " . date('H:i', strtotime($record['morning_in'])), [
                    'att_id'    => $record['att_id'],
                    'check_in'  => $record['morning_in'],
                    'check_out' => $record['evening_out'],
                ], 409);
            }

            $isLate  = (strtotime($now) > strtotime(date('Y-m-d') . ' ' . WORK_START)) ? 1 : 0;
            $attStatus = $isLate ? 'late' : 'present';

            if ($record) {
                // Row exists but morning_in is NULL — update it
                $stmt = $db->prepare(
                    "UPDATE employee_attendance
                     SET morning_in   = ?,
                         att_source   = ?,
                         att_method   = ?,
                         liveness_pass= ?,
                         latitude     = ?,
                         longitude    = ?,
                         gps_accuracy = ?,
                         updated_at   = NOW()
                     WHERE att_id = ?"
                );
                $stmt->execute([$now, $source, $method, $liveness, $lat, $lng, $gpsAcc, $record['att_id']]);
            } else {
                // Insert new attendance row
                $stmt = $db->prepare(
                    "INSERT INTO employee_attendance
                        (emp_id, com_id, loc_id, pm_id, att_date, morning_in,
                         att_source, att_method, liveness_pass, latitude, longitude, gps_accuracy,
                         created_at, updated_at)
                     VALUES (?,?,?,?,?,?,?,?,?,?,?, ?,NOW(),NOW())"
                );
                $stmt->execute([
                    $empId, $comId, $locId, $pmId, $today, $now,
                    $source, $method, $liveness, $lat, $lng, $gpsAcc,
                ]);
            }

            sendResponse(true, "Check-in recorded for {$emp['emp_printname']}", [
                'emp_id'        => $empId,
                'employee_name' => $emp['emp_printname'],
                'att_date'      => $today,
                'check_in'      => $now,
                'status'        => $attStatus,
                'source'        => $source,
                'com_id'        => $comId,
                'loc_id'        => $locId,
            ], 201);
        }

        // ---- CHECK OUT ------------------------------------------
        elseif ($action === 'checkout') {
            if (!$record || !$record['morning_in']) {
                sendResponse(false, 'No check-in found for today. Must check in first.', null, 404);
            }
            if ($record['evening_out']) {
                sendResponse(false, "Already checked out at " . date('H:i', strtotime($record['evening_out'])), [
                    'att_id'    => $record['att_id'],
                    'check_in'  => $record['morning_in'],
                    'check_out' => $record['evening_out'],
                ], 409);
            }

            // Calculate hours
            $totalMins    = (strtotime($now) - strtotime($record['morning_in'])) / 60;
            $breakMins    = 0;
            if ($record['morning_out'] && $record['break_in']) {
                $breakMins = (strtotime($record['break_in']) - strtotime($record['morning_out'])) / 60;
            }
            $workMins     = max(0, $totalMins - $breakMins);
            $totalWorkHrs = round($workMins / 60, 2);
            $morningHrs   = round(
                (strtotime($record['morning_out'] ?? $now) - strtotime($record['morning_in'])) / 3600, 2
            );

            $stmt = $db->prepare(
                "UPDATE employee_attendance
                 SET evening_out         = ?,
                     total_work_hours    = ?,
                     total_morning_hours = ?,
                     total_break_hours   = ?,
                     att_source          = ?,
                     updated_at          = NOW()
                 WHERE att_id = ?"
            );
            $stmt->execute([
                $now,
                $totalWorkHrs,
                $morningHrs,
                round($breakMins / 60, 2),
                $source,
                $record['att_id'],
            ]);

            sendResponse(true, "Check-out recorded for {$emp['emp_printname']}", [
                'emp_id'          => $empId,
                'employee_name'   => $emp['emp_printname'],
                'att_date'        => $today,
                'check_in'        => $record['morning_in'],
                'check_out'       => $now,
                'total_work_hours'=> $totalWorkHrs,
                'source'          => $source,
            ]);
        }

        // ---- BREAK OUT / BREAK IN --------------------------------
        elseif ($action === 'break_out') {
            if (!$record || !$record['morning_in']) {
                sendResponse(false, 'Must check in before break', null, 400);
            }
            $db->prepare("UPDATE employee_attendance SET morning_out=?, updated_at=NOW() WHERE att_id=?")
               ->execute([$now, $record['att_id']]);
            sendResponse(true, "Break started for {$emp['emp_printname']}", ['break_out' => $now]);
        }

        elseif ($action === 'break_in') {
            if (!$record || !$record['morning_out']) {
                sendResponse(false, 'No break started', null, 400);
            }
            $db->prepare("UPDATE employee_attendance SET break_in=?, updated_at=NOW() WHERE att_id=?")
               ->execute([$now, $record['att_id']]);
            sendResponse(true, "Break ended for {$emp['emp_printname']}", ['break_in' => $now]);
        }

        else {
            sendResponse(false, 'Invalid action. Use: checkin | checkout | break_out | break_in', null, 400);
        }
        break;

    default:
        sendResponse(false, 'Method not allowed', null, 405);
}
