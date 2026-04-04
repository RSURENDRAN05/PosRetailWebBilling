DELIMITER $$

DROP PROCEDURE IF EXISTS sp_get_attendance_report $$
CREATE DEFINER=`myposqrc`@`localhost` PROCEDURE `sp_get_attendance_report`(
    IN p_mode VARCHAR(10),
    IN p_date DATE,
    IN p_com_id INT,
    IN p_loc_id INT,
    IN p_emp_id INT
)
BEGIN
    IF p_mode = 'DATEWISE' THEN

        SELECT
            ea.att_id, ea.emp_id, ea.com_id, ea.loc_id, ea.pm_id,
            ea.att_date, ea.morning_in, ea.morning_out, ea.break_in, ea.evening_out,
            ROUND(
              CASE
                WHEN ea.break_in IS NOT NULL AND ea.morning_out IS NOT NULL
                  THEN TIMESTAMPDIFF(SECOND, ea.break_in, ea.morning_out) / 3600
                ELSE 0
              END
            , 2) AS calc_break_hours,
            ROUND(
              CASE
                WHEN ea.morning_in IS NOT NULL AND ea.evening_out IS NOT NULL
                  THEN (
                    TIMESTAMPDIFF(SECOND, ea.morning_in, ea.evening_out)
                    - CASE
                        WHEN ea.break_in IS NOT NULL AND ea.morning_out IS NOT NULL
                          THEN TIMESTAMPDIFF(SECOND, ea.break_in, ea.morning_out)
                        ELSE 0
                      END
                  ) / 3600
                ELSE 0
              END
            , 2) AS calc_total_work_hours,
            ei.emp_printname, pc.pcm_name, pl.plm_name,
            CASE
              WHEN ea.morning_in IS NULL THEN 'ABSENT'
              WHEN ea.evening_out IS NULL THEN 'INCOMPLETE'
              WHEN TIME(ea.morning_in) <= tp.check_in_end THEN 'GOOD'
              WHEN TIME(ea.morning_in) <= tp.break_in_end THEN 'LATE'
              ELSE 'TOO LATE'
            END AS attendance_status,
            GREATEST(
              0,
              TIMESTAMPDIFF(MINUTE, CONCAT(ea.att_date, ' ', tp.check_in_end), ea.morning_in)
            ) AS late_minutes
        FROM employee_attendance ea
        INNER JOIN pos_employeeinfo ei ON ei.emp_id = ea.emp_id
        INNER JOIN employee_time_profiles etp ON etp.employee_id = ea.emp_id
        INNER JOIN time_profiles tp ON tp.id = etp.time_profile_id
        INNER JOIN pos_company_mast pc ON pc.pcm_id = ea.com_id
        INNER JOIN pos_location_mast pl ON pl.plm_id = ea.loc_id
        WHERE ea.att_date = p_date
          AND (p_com_id = 0 OR ea.com_id = p_com_id)
          AND (p_loc_id = 0 OR ea.loc_id = p_loc_id)
          AND (p_emp_id = 0 OR ea.emp_id = p_emp_id);

    ELSEIF p_mode = 'MONTHLY' THEN

        SELECT
            ea.emp_id,
            ei.emp_printname,
            ea.com_id, pc.pcm_name,
            ea.loc_id, pl.plm_name,
            ROUND(SUM(
              CASE
                WHEN ea.morning_in IS NOT NULL AND ea.evening_out IS NOT NULL
                  THEN (
                    TIMESTAMPDIFF(SECOND, ea.morning_in, ea.evening_out)
                    - CASE
                        WHEN ea.break_in IS NOT NULL AND ea.morning_out IS NOT NULL
                          THEN TIMESTAMPDIFF(SECOND, ea.break_in, ea.morning_out)
                        ELSE 0
                      END
                  ) / 3600
                ELSE 0
              END
            ), 2) AS total_work_hours,
            ROUND(SUM(
              CASE
                WHEN ea.break_in IS NOT NULL AND ea.morning_out IS NOT NULL
                  THEN TIMESTAMPDIFF(SECOND, ea.break_in, ea.morning_out) / 3600
                ELSE 0
              END
            ), 2) AS total_break_hours,
            SUM(CASE WHEN ea.morning_in IS NULL THEN 1 ELSE 0 END) AS absent_days,
            SUM(CASE WHEN ea.morning_in IS NOT NULL AND ea.evening_out IS NULL THEN 1 ELSE 0 END) AS incomplete_days,
            SUM(CASE WHEN ea.morning_in IS NOT NULL AND TIME(ea.morning_in) <= tp.check_in_end THEN 1 ELSE 0 END) AS good_days,
            SUM(CASE WHEN ea.morning_in IS NOT NULL
                      AND TIME(ea.morning_in) > tp.check_in_end
                      AND TIME(ea.morning_in) <= tp.break_in_end
                     THEN 1 ELSE 0 END) AS late_days,
            SUM(CASE WHEN ea.morning_in IS NOT NULL AND TIME(ea.morning_in) > tp.break_in_end THEN 1 ELSE 0 END) AS too_late_days,
            COUNT(*) AS total_rows,
            COUNT(ea.att_id) AS total_days
        FROM employee_attendance ea
        INNER JOIN pos_employeeinfo ei ON ei.emp_id = ea.emp_id
        INNER JOIN employee_time_profiles etp ON etp.employee_id = ea.emp_id
        INNER JOIN time_profiles tp ON tp.id = etp.time_profile_id
        INNER JOIN pos_company_mast pc ON pc.pcm_id = ea.com_id
        INNER JOIN pos_location_mast pl ON pl.plm_id = ea.loc_id
        WHERE MONTH(ea.att_date) = MONTH(p_date)
          AND YEAR(ea.att_date) = YEAR(p_date)
          AND (p_com_id = 0 OR ea.com_id = p_com_id)
          AND (p_loc_id = 0 OR ea.loc_id = p_loc_id)
          AND (p_emp_id = 0 OR ea.emp_id = p_emp_id)
        GROUP BY
            ea.emp_id, ei.emp_printname,
            ea.com_id, pc.pcm_name,
            ea.loc_id, pl.plm_name;

    END IF;
END $$

DELIMITER ;
