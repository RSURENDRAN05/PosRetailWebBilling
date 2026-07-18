-- ============================================================
--  pos_mismatch_data  –  Table reference
--  The SP sp_pos_mismatch_data already exists on the server.
--  Only run the CREATE TABLE if the table is missing.
-- ============================================================

-- Create the table if it does not exist
-- Column names confirmed from phpMyAdmin: pmd_created / pmd_updated
-- pmd_trno and pmd_status are both INT  (SP parameters are VARCHAR but columns are INT)
CREATE TABLE IF NOT EXISTS pos_mismatch_data (
    pmd_id      INT      NOT NULL AUTO_INCREMENT PRIMARY KEY,
    pmd_trno    INT      NOT NULL DEFAULT 0,
    pmd_comid   INT      NOT NULL DEFAULT 0,
    pmd_locid   INT      NOT NULL DEFAULT 0,
    pmd_status  INT      NOT NULL DEFAULT 0,   -- 0 = unprocessed, 1 = processed
    pmd_created DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    pmd_updated DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX idx_pmd_trno    (pmd_trno),
    INDEX idx_pmd_status  (pmd_status),
    INDEX idx_pmd_com_loc (pmd_comid, pmd_locid)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ============================================================
--  The SP below is already on the server (shown for reference).
--  DO NOT re-run unless you need to recreate it.
-- ============================================================
/*
DELIMITER $$
CREATE DEFINER=`myposqrc`@`localhost` PROCEDURE `sp_pos_mismatch_data`(
    IN p_mode       VARCHAR(10),
    IN p_pmd_id     INT,
    IN p_pmd_trno   VARCHAR(50),
    IN p_pmd_comid  INT,
    IN p_pmd_locid  INT,
    IN p_pmd_status VARCHAR(20)
)
BEGIN
    IF p_mode = 'INSERT' THEN
        DELETE FROM pos_mismatch_data
        WHERE pmd_trno  = p_pmd_trno
          AND pmd_comid = p_pmd_comid
          AND pmd_locid = p_pmd_locid;

        INSERT INTO pos_mismatch_data
            (pmd_trno, pmd_comid, pmd_locid, pmd_status, pmd_created, pmd_updated)
        VALUES
            (p_pmd_trno, p_pmd_comid, p_pmd_locid, p_pmd_status, NOW(), NOW());

        SELECT LAST_INSERT_ID() AS pmd_id;

    ELSEIF p_mode = 'UPDATE' THEN
        UPDATE pos_mismatch_data
        SET  pmd_status  = p_pmd_status,
             pmd_updated = NOW()
        WHERE pmd_id    = p_pmd_id
          AND pmd_trno  = p_pmd_trno
          AND pmd_comid = p_pmd_comid
          AND pmd_locid = p_pmd_locid;

        SELECT ROW_COUNT() AS rows_affected;

    ELSEIF p_mode = 'SELECT' THEN
        SELECT pmd_id, pmd_trno, pmd_comid, pmd_locid,
               pmd_status, pmd_created, pmd_updated
        FROM   pos_mismatch_data
        WHERE  pmd_comid  = p_pmd_comid
          AND  pmd_locid  = p_pmd_locid
          AND  pmd_status = p_pmd_status
        ORDER BY pmd_id DESC;
    END IF;
END$$
DELIMITER ;
*/
