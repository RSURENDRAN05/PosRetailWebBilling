<?php
// ============================================================
// Face Attendance — DB Class
// Connects Python face-recognition app to existing POS database
// Tables: pos_employeeinfo, employee_face_encodings,
//         employee_attendance, pos_company_mast, pos_location_mast
// ============================================================

class FaceAttendanceFunc
{
    private $conn;

    public function __construct()
    {
        require_once 'dbconnect.php';
        $db = new database();
        $this->conn = $db->connect();
    }

    // ============================================================
    // AUTH
    // ============================================================

    public function adminLogin($username, $password)
    {
        $conn = $this->conn;
        $u = mysqli_real_escape_string($conn, $username);
        $p = md5($password);
        $sql = "SELECT id, username, comid, rid, group_id
                FROM users
                WHERE username='$u' AND password='$p' AND status='1'
                LIMIT 1";
        $res = mysqli_query($conn, $sql);
        return $res ? mysqli_fetch_assoc($res) : null;
    }

    public function pinLogin($loc_id, $pin)
    {
        $conn = $this->conn;
        $l = mysqli_real_escape_string($conn, $loc_id);
        $p = md5($pin);
        $sql = "SELECT id, username, comid, rid, locid
                FROM users
                WHERE locid='$l' AND pin='$p' AND status='1'
                LIMIT 1";
        $res = mysqli_query($conn, $sql);
        return $res ? mysqli_fetch_assoc($res) : null;
    }

    // ============================================================
    // EMPLOYEES — pos_employeeinfo (read-only)
    // ============================================================

    public function getEmployees($com_id = null, $loc_id = null,
                                  $search = null, $active = 1)
    {
        $conn  = $this->conn;
        $where = ['emp_active = ' . (int)$active];
        if ($com_id) $where[] = "emp_compid = '" . mysqli_real_escape_string($conn, $com_id) . "'";
        if ($loc_id) $where[] = "emp_locid  = '" . mysqli_real_escape_string($conn, $loc_id) . "'";
        if ($search) {
            $s      = mysqli_real_escape_string($conn, $search);
            $where[] = "(emp_printname LIKE '%$s%' OR emp_id LIKE '%$s%')";
        }
        $sql = "SELECT emp_id, emp_printname, emp_firstname, emp_lastname,
                       emp_designation, emp_compid, emp_locid,
                       emp_contactno, emp_active, emp_image, emp_joindate
                FROM pos_employeeinfo
                WHERE " . implode(' AND ', $where) . "
                ORDER BY emp_printname ASC";
        return mysqli_query($conn, $sql);
    }

    public function getEmployee($emp_id)
    {
        $conn = $this->conn;
        $id   = mysqli_real_escape_string($conn, $emp_id);
        $sql  = "SELECT emp_id, emp_printname, emp_firstname, emp_lastname,
                        emp_designation, emp_compid, emp_locid,
                        emp_contactno, emp_active, emp_image, emp_joindate
                 FROM pos_employeeinfo
                 WHERE emp_id='$id'
                 LIMIT 1";
        $res  = mysqli_query($conn, $sql);
        return $res ? mysqli_fetch_assoc($res) : null;
    }

    // ============================================================
    // FACE ENCODINGS — employee_face_encodings
    // ============================================================

    public function saveFaceEncodings($emp_id, $com_id, $loc_id,
                                       $encodings, $replace = true)
    {
        $conn = $this->conn;
        $eid  = mysqli_real_escape_string($conn, $emp_id);
        $cid  = mysqli_real_escape_string($conn, $com_id);
        $lid  = mysqli_real_escape_string($conn, $loc_id);
        $enc  = mysqli_real_escape_string($conn, json_encode($encodings));

        if ($replace) {
            mysqli_query($conn, "DELETE FROM employee_face_encodings WHERE emp_id='$eid'");
        }

        // Insert one row per encoding sample array
        $samples = is_array($encodings[0] ?? null) ? $encodings : [$encodings];
        $ok = true;
        foreach ($samples as $i => $sample) {
            $s   = mysqli_real_escape_string($conn, json_encode($sample));
            $sql = "INSERT INTO employee_face_encodings
                        (emp_id, com_id, loc_id, encoding, sample_index, face_name, facetype)
                    VALUES ('$eid','$cid','$lid','$s',$i,'face','full')";
            if (!mysqli_query($conn, $sql)) { $ok = false; }
        }
        return $ok;
    }

    public function getAllFaceEncodings($com_id = null, $loc_id = null)
    {
        $conn  = $this->conn;
        $where = ['1=1'];
        if ($com_id) $where[] = "efe.com_id = '" . mysqli_real_escape_string($conn, $com_id) . "'";
        if ($loc_id) $where[] = "efe.loc_id = '" . mysqli_real_escape_string($conn, $loc_id) . "'";
        $sql = "SELECT efe.id, efe.emp_id, efe.com_id, efe.loc_id,
                       efe.encoding, efe.sample_index, efe.face_name,
                       efe.facetype, efe.created_at,
                       ei.emp_printname
                FROM employee_face_encodings efe
                LEFT JOIN pos_employeeinfo ei ON efe.emp_id = ei.emp_id
                WHERE " . implode(' AND ', $where) . "
                ORDER BY efe.emp_id, efe.sample_index";
        return mysqli_query($conn, $sql);
    }

    public function getEmployeeFaceEncodings($emp_id)
    {
        $conn = $this->conn;
        $eid  = mysqli_real_escape_string($conn, $emp_id);
        $sql  = "SELECT * FROM employee_face_encodings WHERE emp_id='$eid' ORDER BY sample_index";
        return mysqli_query($conn, $sql);
    }

    public function deleteFaceEncodings($emp_id)
    {
        $conn = $this->conn;
        $eid  = mysqli_real_escape_string($conn, $emp_id);
        return mysqli_query($conn, "DELETE FROM employee_face_encodings WHERE emp_id='$eid'");
    }

    // ============================================================
    // ATTENDANCE — employee_attendance
    // morning_in = check-in | evening_out = check-out
    // ============================================================

    public function checkIn($emp_id, $com_id, $loc_id,
                             $source = 'face', $method = 'face_scan',
                             $liveness_pass = 1,
                             $latitude = null, $longitude = null)
    {
        $conn  = $this->conn;
        $eid   = (int)$emp_id;                                    // INT — matches employee_attendance.emp_id
        $cid   = mysqli_real_escape_string($conn, $com_id);
        $lid   = mysqli_real_escape_string($conn, $loc_id);
        $src   = mysqli_real_escape_string($conn, $source);
        $mtd   = mysqli_real_escape_string($conn, $method);
        $today = date('Y-m-d');
        $now   = date('Y-m-d H:i:s');
        $lv    = (int)$liveness_pass;
        $lat = $latitude  !== null ? (float)$latitude  : 'NULL';
        $lon = $longitude !== null ? (float)$longitude : 'NULL';

        // Check today's attendance record in employee_attendance
        $chk = mysqli_fetch_assoc(mysqli_query($conn,
            "SELECT att_id, morning_in FROM employee_attendance
             WHERE emp_id=$eid AND att_date='$today' LIMIT 1"));

        if ($chk && $chk['morning_in'] !== null) {
            // Already checked in via any method — return existing time
            return ['already_in' => true, 'morning_in' => $chk['morning_in'],
                    'att_id' => $chk['att_id']];
        }

        if ($chk) {
            // Record exists but morning_in not yet set — update it
            $att_id = $chk['att_id'];
            mysqli_query($conn,
                "UPDATE employee_attendance
                 SET morning_in='$now', com_id='$cid', loc_id='$lid', updated_at=NOW()
                 WHERE att_id=$att_id");
        } else {
            // No record for today — insert new row
            mysqli_query($conn,
                "INSERT INTO employee_attendance
                     (emp_id, com_id, loc_id, att_date, morning_in, created_at, updated_at)
                 VALUES ($eid,'$cid','$lid','$today','$now',NOW(),NOW())");
            $att_id = mysqli_insert_id($conn);
        }

        // Store face biometric metadata (no timing columns)
        mysqli_query($conn,
            "INSERT INTO employee_face_attendance
                 (emp_id, att_id, com_id, loc_id, att_date,
                  att_source, att_method, liveness_pass, latitude, longitude)
             VALUES ($eid,$att_id,'$cid','$lid','$today',
                     '$src','$mtd',$lv,$lat,$lon)
             ON DUPLICATE KEY UPDATE
                 att_id=$att_id, att_source='$src', att_method='$mtd',
                 liveness_pass=$lv, latitude=$lat, longitude=$lon");

        return ['success' => true, 'morning_in' => $now, 'att_id' => $att_id];
    }

    public function checkOut($emp_id, $com_id, $loc_id, $source = 'face')
    {
        $conn  = $this->conn;
        $eid   = (int)$emp_id;
        $cid   = mysqli_real_escape_string($conn, $com_id);
        $lid   = mysqli_real_escape_string($conn, $loc_id);
        $src   = mysqli_real_escape_string($conn, $source);
        $today = date('Y-m-d');
        $now   = date('Y-m-d H:i:s');

        // Get today's attendance from employee_attendance
        $row = mysqli_fetch_assoc(mysqli_query($conn,
            "SELECT att_id, morning_in FROM employee_attendance
             WHERE emp_id=$eid AND att_date='$today' AND morning_in IS NOT NULL LIMIT 1"));

        if (!$row) {
            return ['success' => false, 'message' => 'No check-in found for today'];
        }

        $att_id     = $row['att_id'];
        $work_hours = round((strtotime($now) - strtotime($row['morning_in'])) / 3600, 2);

        // Write evening_out + total_work_hours into employee_attendance
        mysqli_query($conn,
            "UPDATE employee_attendance
             SET evening_out='$now', total_work_hours=$work_hours, updated_at=NOW()
             WHERE att_id=$att_id");

        // Update face metadata (location may have changed on transfer)
        mysqli_query($conn,
            "UPDATE employee_face_attendance
             SET att_source='$src', com_id='$cid', loc_id='$lid'
             WHERE att_id=$att_id");

        return ['success' => true, 'evening_out' => $now, 'work_hours' => $work_hours];
    }

    public function getAttendance($date = null, $emp_id = null,
                                   $from_date = null, $to_date = null,
                                   $com_id = null, $loc_id = null)
    {
        $conn  = $this->conn;
        $where = ['1=1'];
         if ($date)      $where[] = "ea.att_date = '"  . mysqli_real_escape_string($conn, $date)      . "'";
         if ($emp_id)    $where[] = "ea.emp_id = "     . (int)$emp_id;
         if ($from_date) $where[] = "ea.att_date >= '" . mysqli_real_escape_string($conn, $from_date) . "'";
         if ($to_date)   $where[] = "ea.att_date <= '" . mysqli_real_escape_string($conn, $to_date)   . "'";
         if ($com_id)    $where[] = "ea.com_id = '"    . mysqli_real_escape_string($conn, $com_id)    . "'";
         if ($loc_id)    $where[] = "ea.loc_id = '"    . mysqli_real_escape_string($conn, $loc_id)    . "'";

         // Timing from employee_attendance, metadata from employee_face_attendance
         $sql = "SELECT ea.att_id, ea.emp_id, ea.com_id, ea.loc_id,
                  ea.att_date, ea.morning_in, ea.morning_out, ea.break_in,
                  ea.evening_out, ea.total_morning_hours, ea.total_break_hours,
                  ea.total_work_hours,
                  fa.att_source, fa.att_method, fa.liveness_pass,
                  fa.latitude, fa.longitude,
                  ei.emp_printname
              FROM employee_attendance ea
              LEFT JOIN employee_face_attendance fa ON fa.att_id = ea.att_id
              LEFT JOIN pos_employeeinfo ei ON ei.emp_id = ea.emp_id
              WHERE " . implode(' AND ', $where) . "
              ORDER BY ea.att_date DESC, ea.morning_in DESC";
        return mysqli_query($conn, $sql);
    }

    // ============================================================
    // STATS — dashboard summary
    // ============================================================

    public function getStats($com_id = null, $loc_id = null)
    {
        $conn  = $this->conn;
        $today = date('Y-m-d');

        $we = ['emp_active = 1'];
        $wa = ["att_date = '$today'"];
        if ($com_id) { $we[] = "emp_compid = '" . mysqli_real_escape_string($conn, $com_id) . "'";
                       $wa[] = "com_id = '"     . mysqli_real_escape_string($conn, $com_id) . "'"; }
        if ($loc_id) { $we[] = "emp_locid = '"  . mysqli_real_escape_string($conn, $loc_id) . "'";
                       $wa[] = "loc_id = '"     . mysqli_real_escape_string($conn, $loc_id) . "'"; }

        $total   = mysqli_fetch_assoc(mysqli_query($conn,
            "SELECT COUNT(*) AS cnt FROM pos_employeeinfo WHERE " . implode(' AND ', $we)))['cnt'];
        // Count from employee_attendance — morning_in is written there by face detection
        $present = mysqli_fetch_assoc(mysqli_query($conn,
            "SELECT COUNT(*) AS cnt FROM employee_attendance WHERE "
            . implode(' AND ', $wa) . " AND morning_in IS NOT NULL"))['cnt'];
        $late    = mysqli_fetch_assoc(mysqli_query($conn,
            "SELECT COUNT(*) AS cnt FROM employee_attendance WHERE "
            . implode(' AND ', $wa) . " AND TIME(morning_in) > '09:00:00'"))['cnt'];

        return [
            'total'   => (int)$total,
            'present' => (int)$present,
            'absent'  => (int)$total - (int)$present,
            'late'    => (int)$late,
            'date'    => $today,
        ];
    }

    // ============================================================
    // BRANCHES — pos_company_mast
    // ============================================================

    public function getBranches($com_id = null)
    {
        $conn  = $this->conn;
        $where = ['1=1'];
        if ($com_id) $where[] = "pcm_id = '" . mysqli_real_escape_string($conn, $com_id) . "'";
        $sql = "SELECT pcm_id AS id, pcm_name AS name, pcm_active AS active
                FROM pos_company_mast
                WHERE " . implode(' AND ', $where) . "
                ORDER BY pcm_name";
        return mysqli_query($conn, $sql);
    }

    // ============================================================
    // LOCATIONS — pos_location_mast
    // ============================================================

    public function getLocations($branch_id = null)
    {
        $conn  = $this->conn;
        $where = ['1=1'];
        if ($branch_id) $where[] = "plm_compid = '" . mysqli_real_escape_string($conn, $branch_id) . "'";
        $sql = "SELECT plm_id AS id, plm_name AS name, plm_compid AS branch_id
                FROM pos_location_mast
                WHERE " . implode(' AND ', $where) . "
                ORDER BY plm_name";
        return mysqli_query($conn, $sql);
    }
}
