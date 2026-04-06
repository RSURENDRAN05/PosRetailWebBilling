-- ============================================================
-- Face Attendance — Integration with Existing POS Database
-- Database: myposqrc_posretail
-- Run this in cPanel phpMyAdmin
-- ============================================================

USE `myposqrc_posretail`;

-- ============================================================
-- TABLE 1: employee_face_encodings
-- Mirrors structure of employee_fingerprints
-- Stores 128-dim face vectors (dlib/face_recognition)
-- ============================================================

CREATE TABLE IF NOT EXISTS `employee_face_encodings` (
    `id`            INT          NOT NULL AUTO_INCREMENT,
    `emp_id`        INT          NOT NULL,
    `com_id`        VARCHAR(50)  NOT NULL DEFAULT '',
    `loc_id`        VARCHAR(50)  NOT NULL DEFAULT '',
    `encoding`      MEDIUMTEXT   NOT NULL,          -- JSON array of 128 floats
    `sample_index`  INT          NOT NULL DEFAULT 0, -- multiple samples (0-9)
    `face_name`     VARCHAR(50)  NOT NULL DEFAULT 'face',    -- like finger_name
    `facetype`      VARCHAR(20)  NOT NULL DEFAULT 'full',    -- like fingertype
    `created_at`    TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at`    TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    KEY `idx_emp_com`     (`emp_id`, `com_id`),
    KEY `idx_emp_loc`     (`emp_id`, `loc_id`),
    KEY `idx_com_loc`     (`com_id`, `loc_id`),
    -- Reference existing employee table (INT emp_id)
    CONSTRAINT `fk_face_emp`
        FOREIGN KEY (`emp_id`)
        REFERENCES `pos_employeeinfo` (`emp_id`)
        ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
  COMMENT='Face recognition encodings — mirrors employee_fingerprints structure';

-- ============================================================
-- TABLE 2: employee_face_attendance  (face biometric metadata only)
-- ● Face detection writes morning_in / evening_out directly into
--   the EXISTING employee_attendance table. NO timing here.
-- ● emp_id is INT — matches employee_attendance.emp_id.
-- ● att_id FK links to the exact employee_attendance row updated.
-- ● com_id / loc_id updateable on employee branch transfer.
-- ● One metadata row per employee per date.
-- ============================================================

CREATE TABLE IF NOT EXISTS `employee_face_attendance` (
    `id`            INT              NOT NULL AUTO_INCREMENT,

    -- INT to match employee_attendance.emp_id
    `emp_id`        INT              NOT NULL,

    -- FK to employee_attendance row that was updated by face detection
    `att_id`        INT              DEFAULT NULL,

    -- Location — updateable independently on branch transfer
    `com_id`        VARCHAR(50)      NOT NULL DEFAULT '',
    `loc_id`        VARCHAR(50)      NOT NULL DEFAULT '',

    -- Attendance timing
    `att_date`      DATE             NOT NULL,

    -- Face biometric metadata ONLY (NO check_in / check_out / work_hours)
    `att_source`    ENUM('face','fingerprint','web','mobile','manual')
                                     NOT NULL DEFAULT 'face',
    `att_method`    VARCHAR(30)      DEFAULT NULL
                                     COMMENT 'face_scan / fp_device / web_face_api / manual_entry',
    `liveness_pass` TINYINT(1)       NOT NULL DEFAULT 1
                                     COMMENT '1=liveness verified, 0=skipped',

    -- GPS (optional)
    `latitude`      DECIMAL(10,7)    DEFAULT NULL,
    `longitude`     DECIMAL(10,7)    DEFAULT NULL,
    `gps_accuracy`  FLOAT            DEFAULT NULL   COMMENT 'Accuracy in metres',

    -- Audit
    `created_at`    TIMESTAMP        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at`    TIMESTAMP        NOT NULL DEFAULT CURRENT_TIMESTAMP
                                     ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (`id`),
    UNIQUE KEY `uq_emp_date`   (`emp_id`, `att_date`),
    KEY `idx_att_id`           (`att_id`),
    KEY `idx_date`             (`att_date`),
    KEY `idx_com_loc_date`     (`com_id`, `loc_id`, `att_date`),

    CONSTRAINT `fk_face_meta_att`
        FOREIGN KEY (`att_id`)
        REFERENCES `employee_attendance` (`att_id`)
        ON DELETE SET NULL ON UPDATE CASCADE

) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
    COMMENT='Face biometric metadata — timing written to employee_attendance (morning_in/evening_out)';

-- ============================================================
-- VIEW: v_face_attendance_today
-- Uses employee_face_attendance (new table — no ALTER needed)
-- ============================================================

CREATE OR REPLACE VIEW `v_face_attendance_today` AS
SELECT
    ea.att_id,
    ea.emp_id,
    ea.com_id,
    ea.loc_id,
    ea.att_date,
    -- Timing — lives in employee_attendance
    ea.morning_in,
    ea.morning_out,
    ea.break_in,
    ea.evening_out,
    ea.total_morning_hours,
    ea.total_break_hours,
    ea.total_work_hours,
    -- Face biometric metadata (NULL if not face-sourced)
    fa.att_source,
    fa.att_method,
    fa.liveness_pass,
    fa.latitude,
    fa.longitude,
    -- Employee info
    e.emp_firstname,
    e.emp_lastname,
    e.emp_printname,
    CONCAT(e.emp_firstname, ' ', e.emp_lastname) AS full_name,
    e.emp_designation,
    e.emp_compid,
    e.emp_locid,
    e.emp_active,
    -- Face registered?
    (SELECT COUNT(*) FROM employee_face_encodings f
     WHERE f.emp_id = e.emp_id) AS face_registered,
    -- Fingerprint registered?
    (SELECT COUNT(*) FROM employee_fingerprints fp
     WHERE fp.emp_id = e.emp_id) AS fingerprint_registered,
    -- Late detection (after 09:00)
    CASE
        WHEN TIME(ea.morning_in) > '09:00:00' THEN 'late'
        WHEN ea.morning_in IS NOT NULL         THEN 'present'
        ELSE 'absent'
    END AS attendance_status
FROM
    `employee_attendance` ea
    LEFT JOIN `employee_face_attendance` fa ON fa.att_id = ea.att_id
    LEFT JOIN `pos_employeeinfo` e ON e.emp_id = ea.emp_id
WHERE
    ea.att_date = CURDATE();

-- ============================================================
-- VIEW: v_employee_biometrics
-- Shows each employee with their fingerprint + face status
-- ============================================================

CREATE OR REPLACE VIEW `v_employee_biometrics` AS
SELECT
    e.emp_id,
    CONCAT(e.emp_firstname, ' ', e.emp_lastname) AS full_name,
    e.emp_printname,
    e.emp_designation,
    e.emp_compid   AS com_id,
    e.emp_locid    AS loc_id,
    e.emp_active,
    e.emp_image,
    -- Fingerprint info
    (SELECT COUNT(*) FROM employee_fingerprints fp WHERE fp.emp_id = e.emp_id)
        AS fingerprint_count,
    (SELECT MAX(created_at) FROM employee_fingerprints fp WHERE fp.emp_id = e.emp_id)
        AS fp_registered_at,
    -- Face info
    (SELECT COUNT(*) FROM employee_face_encodings fe WHERE fe.emp_id = e.emp_id)
        AS face_sample_count,
    (SELECT MAX(created_at) FROM employee_face_encodings fe WHERE fe.emp_id = e.emp_id)
        AS face_registered_at,
    -- Today attendance (from new employee_face_attendance table)
    -- Today timing — face detection writes to employee_attendance
    (SELECT morning_in  FROM employee_attendance ea
     WHERE ea.emp_id = e.emp_id AND ea.att_date = CURDATE() LIMIT 1)
        AS today_check_in,
    (SELECT evening_out FROM employee_attendance ea
     WHERE ea.emp_id = e.emp_id AND ea.att_date = CURDATE() LIMIT 1)
        AS today_check_out,
    (SELECT fa.att_source FROM employee_face_attendance fa
     JOIN employee_attendance ea ON fa.att_id = ea.att_id
     WHERE ea.emp_id = e.emp_id AND ea.att_date = CURDATE() LIMIT 1)
        AS today_source
FROM
    `pos_employeeinfo` e
WHERE
    e.emp_active = 1
ORDER BY
    e.emp_printname;

-- ============================================================
-- VIEW: v_monthly_attendance_summary
-- Monthly report per employee
-- ============================================================

CREATE OR REPLACE VIEW `v_monthly_attendance_summary` AS
SELECT
    ea.emp_id,
    CONCAT(e.emp_firstname, ' ', e.emp_lastname) AS full_name,
    e.emp_designation,
    ea.com_id,
    ea.loc_id,
    DATE_FORMAT(ea.att_date, '%Y-%m')     AS month_year,
    COUNT(*)                             AS total_days,
    SUM(CASE WHEN TIME(ea.morning_in) <= '09:00:00' THEN 1 ELSE 0 END) AS on_time_days,
    SUM(CASE WHEN TIME(ea.morning_in) >  '09:00:00' THEN 1 ELSE 0 END) AS late_days,
    SUM(ea.total_work_hours)              AS total_hours,
    AVG(ea.total_work_hours)              AS avg_daily_hours,
    -- Source breakdown from face metadata
    SUM(CASE WHEN fa.att_source = 'face'           THEN 1 ELSE 0 END) AS face_checkins,
    SUM(CASE WHEN fa.att_source = 'fingerprint'    THEN 1 ELSE 0 END) AS fp_checkins,
    SUM(CASE WHEN fa.att_source IN ('web','mobile') THEN 1 ELSE 0 END) AS remote_checkins
FROM
    `employee_attendance` ea
    LEFT JOIN `employee_face_attendance` fa ON fa.att_id = ea.att_id
    LEFT JOIN `pos_employeeinfo` e ON e.emp_id = ea.emp_id
GROUP BY
    ea.emp_id, month_year, ea.com_id, ea.loc_id;

-- ============================================================
-- STORED PROCEDURE: sp_mark_face_attendance
-- Call this from PHP when face is recognized
-- Usage: CALL sp_mark_face_attendance(2,1,3,1,'MorningIn','face','face_scan',1,3.1390,101.6869,5.0,NOW())
-- Usage: CALL sp_mark_face_attendance(2,1,3,1,'EveningOut','face','face_scan',1,3.1390,101.6869,5.0,NOW())
-- ============================================================

 USE `myposqrc_posretail`;

ALTER TABLE `employee_face_attendance`
ADD COLUMN IF NOT EXISTS `gps_accuracy` FLOAT DEFAULT NULL COMMENT 'Accuracy in metres' AFTER `longitude`;

DELIMITER $$

DROP PROCEDURE IF EXISTS `sp_mark_face_attendance`$$

CREATE PROCEDURE `sp_mark_face_attendance`(
    IN p_emp_id        INT,
    IN p_com_id        INT,
    IN p_loc_id        INT,
    IN p_pm_id         INT,
    IN p_action        VARCHAR(20),    -- MorningIn|BreakOut|BreakIn|EveningOut
    IN p_source        VARCHAR(20),    -- face|fingerprint|web|mobile|manual
    IN p_method        VARCHAR(30),
    IN p_liveness      TINYINT(1),
    IN p_lat           DECIMAL(10,7),
    IN p_lng           DECIMAL(10,7),
    IN p_gps_accuracy  FLOAT,
    IN p_punchtime     DATETIME
)
proc_begin: BEGIN
    DECLARE v_today DATE DEFAULT DATE(p_punchtime);
    DECLARE v_att_id INT DEFAULT NULL;
    DECLARE v_morning_in DATETIME;
    DECLARE v_morning_out DATETIME;
    DECLARE v_break_in DATETIME;
    DECLARE v_evening_out DATETIME;
    DECLARE v_total_morning DECIMAL(5,2) DEFAULT 0.00;
    DECLARE v_total_break DECIMAL(5,2) DEFAULT 0.00;
    DECLARE v_total_work DECIMAL(5,2) DEFAULT 0.00;
    DECLARE v_status VARCHAR(20) DEFAULT 'present';
    DECLARE v_profile_id INT DEFAULT NULL;
    DECLARE v_check_in_end TIME DEFAULT '09:00:00';

    -- Time profile per employee (latest effective profile)
    SELECT etp.time_profile_id
      INTO v_profile_id
      FROM employee_time_profiles etp
     WHERE etp.employee_id = p_emp_id
       AND etp.effective_date <= v_today
     ORDER BY etp.effective_date DESC
     LIMIT 1;

    IF v_profile_id IS NOT NULL THEN
        SELECT tp.check_in_end
          INTO v_check_in_end
          FROM time_profiles tp
         WHERE tp.id = v_profile_id
         LIMIT 1;
    END IF;

    -- Attendance status only for MorningIn
    IF p_action = 'MorningIn' THEN
        IF TIME(p_punchtime) > v_check_in_end THEN
            SET v_status = 'late';
        ELSE
            SET v_status = 'present';
        END IF;
    END IF;

    -- check if record exists for today
    SELECT att_id, morning_in, morning_out, break_in, evening_out
      INTO v_att_id, v_morning_in, v_morning_out, v_break_in, v_evening_out
      FROM employee_attendance
     WHERE emp_id = p_emp_id AND att_date = v_today
     LIMIT 1;

    -- if no record exists and action is MorningIn, insert
    IF v_att_id IS NULL AND p_action = 'MorningIn' THEN
        INSERT INTO employee_attendance (
            emp_id, com_id, loc_id, pm_id, att_date, morning_in
        ) VALUES (
            p_emp_id, p_com_id, p_loc_id, p_pm_id, v_today, p_punchtime
        );
        SET v_att_id = LAST_INSERT_ID();

    ELSEIF v_att_id IS NULL THEN
        SELECT 'error' AS status,
               'No morning punch record for today' AS message,
               NULL AS attendanceId,
               0 AS morningHours,
               0 AS breakHours,
               0 AS workHours;
        LEAVE proc_begin;
    END IF;

    -- prevent duplicate punches
    IF (p_action = 'MorningIn'  AND v_morning_in  IS NOT NULL) OR
       (p_action = 'BreakOut'   AND v_morning_out IS NOT NULL) OR
       (p_action = 'BreakIn'    AND v_break_in    IS NOT NULL) OR
       (p_action = 'EveningOut' AND v_evening_out IS NOT NULL) THEN
        SELECT 'already punched' AS status,
               v_att_id AS attendanceId,
               COALESCE(total_morning_hours,0) AS morningHours,
               COALESCE(total_break_hours,0) AS breakHours,
               COALESCE(total_work_hours,0) AS workHours
          FROM employee_attendance
         WHERE att_id = v_att_id;
        LEAVE proc_begin;
    END IF;

    -- update fields by action
    IF p_action = 'MorningIn' THEN
        UPDATE employee_attendance SET morning_in = p_punchtime WHERE att_id = v_att_id;
    ELSEIF p_action = 'BreakOut' THEN
        UPDATE employee_attendance SET morning_out = p_punchtime WHERE att_id = v_att_id;
    ELSEIF p_action = 'BreakIn' THEN
        UPDATE employee_attendance SET break_in = p_punchtime WHERE att_id = v_att_id;
    ELSEIF p_action = 'EveningOut' THEN
        UPDATE employee_attendance SET evening_out = p_punchtime WHERE att_id = v_att_id;
    ELSE
        SELECT 'error' AS status,
               CONCAT('Unknown action: ', p_action) AS message,
               v_att_id AS attendanceId,
               0 AS morningHours,
               0 AS breakHours,
               0 AS workHours;
        LEAVE proc_begin;
    END IF;

    -- recalc totals
    SELECT morning_in, morning_out, break_in, evening_out
      INTO v_morning_in, v_morning_out, v_break_in, v_evening_out
      FROM employee_attendance
     WHERE att_id = v_att_id;

    IF v_morning_in IS NOT NULL AND v_morning_out IS NOT NULL THEN
        SET v_total_morning = TIMESTAMPDIFF(MINUTE, v_morning_in, v_morning_out) / 60;
    END IF;

    IF v_morning_out IS NOT NULL AND v_break_in IS NOT NULL THEN
        SET v_total_break = TIMESTAMPDIFF(MINUTE, v_morning_out, v_break_in) / 60;
    END IF;

    IF v_morning_in IS NOT NULL AND v_evening_out IS NOT NULL THEN
        SET v_total_work = (TIMESTAMPDIFF(MINUTE, v_morning_in, v_evening_out) / 60) - v_total_break;
    END IF;

    UPDATE employee_attendance
       SET total_morning_hours = v_total_morning,
           total_break_hours = v_total_break,
           total_work_hours = v_total_work,
           updated_at = NOW()
     WHERE att_id = v_att_id;

    -- always upsert face metadata for the attendance row
    INSERT INTO employee_face_attendance (
        emp_id, att_id, com_id, loc_id, att_date,
        att_source, att_method, liveness_pass, latitude, longitude, gps_accuracy
    ) VALUES (
        p_emp_id, v_att_id, p_com_id, p_loc_id, v_today,
        p_source, p_method, p_liveness, p_lat, p_lng, p_gps_accuracy
    )
    ON DUPLICATE KEY UPDATE
        att_id        = v_att_id,
        com_id        = p_com_id,
        loc_id        = p_loc_id,
        att_source    = p_source,
        att_method    = p_method,
        liveness_pass = p_liveness,
        latitude      = p_lat,
        longitude     = p_lng,
        gps_accuracy  = p_gps_accuracy;

    SELECT 'success' AS status,
           p_action AS action,
           v_status AS attendance_status,
           v_att_id AS attendanceId,
           v_total_morning AS morningHours,
           v_total_break AS breakHours,
           v_total_work AS workHours;

END proc_begin$$

DELIMITER ;

-- ============================================================
-- VERIFY — Check everything was created
-- ============================================================
SELECT 'Tables created (face):' AS info;
SHOW TABLES LIKE 'employee_face%';

SELECT 'Columns in employee_face_attendance:' AS info;
SELECT COLUMN_NAME, COLUMN_TYPE, COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'myposqrc_posretail'
  AND TABLE_NAME   = 'employee_face_attendance'
ORDER BY ORDINAL_POSITION;

SELECT 'NOTE: employee_attendance was NOT modified.' AS info;

SELECT 'Views created:' AS info;
SHOW FULL TABLES WHERE Table_type = 'VIEW';
