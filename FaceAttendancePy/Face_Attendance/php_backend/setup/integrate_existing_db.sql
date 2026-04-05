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
    `emp_id`        VARCHAR(50)  NOT NULL,
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
    -- Reference existing employee table
    CONSTRAINT `fk_face_emp`
        FOREIGN KEY (`emp_id`)
        REFERENCES `pos_employeeinfo` (`emp_id`)
        ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
  COMMENT='Face recognition encodings — mirrors employee_fingerprints structure';

-- ============================================================
-- TABLE 2: PATCH employee_attendance
-- Add face/source tracking columns
-- (Safe to run — uses IF NOT EXISTS)
-- ============================================================

ALTER TABLE `employee_attendance`
  ADD COLUMN IF NOT EXISTS `att_source`
      ENUM('face','fingerprint','manual','web','mobile')
      NOT NULL DEFAULT 'manual'
      AFTER `updated_at`,

  ADD COLUMN IF NOT EXISTS `att_method`
      VARCHAR(30) DEFAULT NULL
      COMMENT 'Details: face_scan / fp_device / web_face_api / manual_entry'
      AFTER `att_source`,

  ADD COLUMN IF NOT EXISTS `latitude`
      DECIMAL(10, 7) DEFAULT NULL
      AFTER `att_method`,

  ADD COLUMN IF NOT EXISTS `longitude`
      DECIMAL(10, 7) DEFAULT NULL
      AFTER `latitude`,

  ADD COLUMN IF NOT EXISTS `gps_accuracy`
      FLOAT DEFAULT NULL
      AFTER `longitude`,

  ADD COLUMN IF NOT EXISTS `liveness_pass`
      TINYINT(1) NOT NULL DEFAULT 1
      COMMENT '1=liveness verified, 0=skipped'
      AFTER `gps_accuracy`;

-- ============================================================
-- VIEW: face_attendance_today
-- Joins pos_employeeinfo + employee_attendance for easy queries
-- ============================================================

CREATE OR REPLACE VIEW `v_face_attendance_today` AS
SELECT
    a.att_id,
    a.emp_id,
    a.com_id,
    a.loc_id,
    a.att_date,
    a.morning_in       AS check_in,
    a.evening_out      AS check_out,
    a.morning_out      AS break_out,
    a.break_in         AS break_return,
    a.total_work_hours,
    a.total_morning_hours,
    a.total_break_hours,
    a.att_source,
    a.att_method,
    a.latitude,
    a.longitude,
    a.liveness_pass,
    -- Employee info from pos_employeeinfo
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
     WHERE f.emp_id = a.emp_id) AS face_registered,
    -- Fingerprint registered?
    (SELECT COUNT(*) FROM employee_fingerprints fp
     WHERE fp.emp_id = a.emp_id) AS fingerprint_registered,
    -- Late detection (after 09:00)
    CASE
        WHEN TIME(a.morning_in) > '09:00:00' THEN 'late'
        WHEN a.morning_in IS NOT NULL         THEN 'present'
        ELSE 'absent'
    END AS attendance_status
FROM
    `employee_attendance` a
    JOIN `pos_employeeinfo` e ON e.emp_id = a.emp_id
WHERE
    a.att_date = CURDATE();

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
    -- Today attendance
    (SELECT morning_in FROM employee_attendance a
     WHERE a.emp_id = e.emp_id AND a.att_date = CURDATE() LIMIT 1)
        AS today_check_in,
    (SELECT evening_out FROM employee_attendance a
     WHERE a.emp_id = e.emp_id AND a.att_date = CURDATE() LIMIT 1)
        AS today_check_out,
    (SELECT att_source FROM employee_attendance a
     WHERE a.emp_id = e.emp_id AND a.att_date = CURDATE() LIMIT 1)
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
    a.emp_id,
    CONCAT(e.emp_firstname, ' ', e.emp_lastname) AS full_name,
    e.emp_designation,
    a.com_id,
    a.loc_id,
    DATE_FORMAT(a.att_date, '%Y-%m')    AS month_year,
    COUNT(*)                             AS total_days,
    SUM(CASE WHEN TIME(a.morning_in) <= '09:00:00' THEN 1 ELSE 0 END) AS on_time_days,
    SUM(CASE WHEN TIME(a.morning_in) >  '09:00:00' THEN 1 ELSE 0 END) AS late_days,
    SUM(a.total_work_hours)              AS total_hours,
    AVG(a.total_work_hours)              AS avg_daily_hours,
    SUM(CASE WHEN a.att_source = 'face'        THEN 1 ELSE 0 END) AS face_checkins,
    SUM(CASE WHEN a.att_source = 'fingerprint' THEN 1 ELSE 0 END) AS fp_checkins,
    SUM(CASE WHEN a.att_source = 'web'
              OR a.att_source = 'mobile'       THEN 1 ELSE 0 END) AS remote_checkins
FROM
    `employee_attendance` a
    JOIN `pos_employeeinfo` e ON e.emp_id = a.emp_id
GROUP BY
    a.emp_id, month_year, a.com_id, a.loc_id;

-- ============================================================
-- STORED PROCEDURE: sp_mark_face_attendance
-- Call this from PHP when face is recognized
-- Usage: CALL sp_mark_face_attendance('EMP001','COM001','LOC001','face','web',1,3.1390,101.6869)
-- ============================================================

DELIMITER $$

DROP PROCEDURE IF EXISTS `sp_mark_face_attendance`$$

CREATE PROCEDURE `sp_mark_face_attendance`(
    IN p_emp_id     VARCHAR(50),
    IN p_com_id     VARCHAR(50),
    IN p_loc_id     VARCHAR(50),
    IN p_source     VARCHAR(20),   -- 'face','fingerprint','web','mobile','manual'
    IN p_method     VARCHAR(30),   -- 'face_scan','web_face_api','fp_device', etc.
    IN p_liveness   TINYINT(1),
    IN p_lat        DECIMAL(10,7),
    IN p_lng        DECIMAL(10,7)
)
BEGIN
    DECLARE v_today      DATE DEFAULT CURDATE();
    DECLARE v_now        DATETIME DEFAULT NOW();
    DECLARE v_checkin    DATETIME DEFAULT NULL;
    DECLARE v_work_hrs   DECIMAL(5,2) DEFAULT 0;
    DECLARE v_status     VARCHAR(20);

    -- Determine late status
    IF TIME(v_now) > '09:00:00' THEN
        SET v_status = 'late';
    ELSE
        SET v_status = 'present';
    END IF;

    -- Check if already has attendance today
    SELECT morning_in INTO v_checkin
    FROM employee_attendance
    WHERE emp_id = p_emp_id AND att_date = v_today
    LIMIT 1;

    IF v_checkin IS NULL THEN
        -- ---- CHECK IN ----
        INSERT INTO employee_attendance
            (emp_id, com_id, loc_id, att_date, morning_in,
             att_source, att_method, liveness_pass, latitude, longitude, created_at, updated_at)
        VALUES
            (p_emp_id, p_com_id, p_loc_id, v_today, v_now,
             p_source, p_method, p_liveness, p_lat, p_lng, v_now, v_now)
        ON DUPLICATE KEY UPDATE
            morning_in   = IF(morning_in IS NULL, v_now, morning_in),
            att_source   = p_source,
            att_method   = p_method,
            liveness_pass= p_liveness,
            latitude     = p_lat,
            longitude    = p_lng,
            updated_at   = v_now;

        SELECT 'checkin' AS action, v_status AS status, v_now AS time_marked;

    ELSE
        -- ---- CHECK OUT ----
        SET v_work_hrs = ROUND(TIMESTAMPDIFF(MINUTE, v_checkin, v_now) / 60.0, 2);

        UPDATE employee_attendance
        SET
            evening_out      = v_now,
            total_work_hours = v_work_hrs,
            att_source       = p_source,
            updated_at       = v_now
        WHERE emp_id = p_emp_id AND att_date = v_today;

        SELECT 'checkout' AS action, 'present' AS status, v_now AS time_marked,
               v_work_hrs AS hours_worked;
    END IF;
END$$

DELIMITER ;

-- ============================================================
-- VERIFY — Check everything was created
-- ============================================================
SELECT 'Tables created:' AS info;
SHOW TABLES LIKE 'employee_face%';

SELECT 'Columns in employee_attendance (new ones):' AS info;
SELECT COLUMN_NAME, COLUMN_TYPE, COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'myposqrc_posretail'
  AND TABLE_NAME   = 'employee_attendance'
  AND COLUMN_NAME  IN ('att_source','att_method','latitude','longitude','liveness_pass');

SELECT 'Views created:' AS info;
SHOW FULL TABLES WHERE Table_type = 'VIEW';
