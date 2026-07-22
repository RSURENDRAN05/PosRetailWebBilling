-- =====================================================================
-- MYPOSQR MULTI-TENANT MASTER DATABASE
-- Database : myposqrc_master
-- Purpose  : Maps every SyncId to a client's own MySQL database.
--            Adding a new client = inserting ONE row into
--            client_connection. No source-code changes required.
-- Compatible: MySQL 5.7+ / MariaDB 10.2+
-- =====================================================================

CREATE DATABASE IF NOT EXISTS `myposqrc_master`
    DEFAULT CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE `myposqrc_master`;

-- ---------------------------------------------------------------------
-- 1. CLIENT CONNECTION MAP
--    One row per client / SyncId.
--    DBPassword SHOULD be stored encrypted. Generate the encrypted
--    value with:  php controller/lib/encrypt_password.php 'PlainPassword'
--    (values starting with "enc:v1:" are decrypted automatically;
--     plain values still work to ease migration, but are discouraged).
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `client_connection` (
    `Id`          INT UNSIGNED     NOT NULL AUTO_INCREMENT,
    `ClientID`    VARCHAR(50)      NOT NULL,
    `ClientName`  VARCHAR(150)     NOT NULL,
    `SyncId`      VARCHAR(32)      NOT NULL,
    `DBHost`      VARCHAR(150)     NOT NULL DEFAULT 'localhost',
    `DBUser`      VARCHAR(100)     NOT NULL,
    `DBPassword`  VARCHAR(512)     NOT NULL,          -- encrypted (enc:v1:...) or plain (legacy)
    `DBName`      VARCHAR(100)     NOT NULL,
    `Status`      TINYINT(1)       NOT NULL DEFAULT 1, -- 1 = Active, 0 = Disabled
    `CreatedDate` DATETIME         NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`Id`),
    UNIQUE KEY `uq_syncid` (`SyncId`),
    KEY `idx_clientid` (`ClientID`),
    KEY `idx_status`   (`Status`)
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_unicode_ci
  COMMENT = 'Maps SyncId -> per-client database credentials';

-- ---------------------------------------------------------------------
-- 2. API ACCESS / SECURITY LOG
--    Every invalid SyncId attempt is recorded here.
--    Also used for IP based rate-limiting of invalid requests.
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `api_access_log` (
    `Id`          BIGINT UNSIGNED  NOT NULL AUTO_INCREMENT,
    `SyncId`      VARCHAR(64)      NULL,               -- raw (truncated) value received
    `IPAddress`   VARCHAR(45)      NOT NULL,           -- supports IPv6
    `RequestUri`  VARCHAR(255)     NULL,
    `Reason`      VARCHAR(50)      NOT NULL,           -- MISSING_SYNCID / INVALID_SYNCID / UNKNOWN_SYNCID / ...
    `AttemptTime` DATETIME         NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`Id`),
    KEY `idx_ip_time` (`IPAddress`, `AttemptTime`)
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_unicode_ci
  COMMENT = 'Invalid SyncId attempts + rate limiting source';

-- ---------------------------------------------------------------------
-- 3. SAMPLE CLIENT RECORDS
--    NOTE: The passwords below are PLAIN-TEXT placeholders so the
--    script runs out of the box. In production, replace them with the
--    "enc:v1:..." value produced by:
--        php controller/lib/encrypt_password.php 'TheRealPassword'
-- ---------------------------------------------------------------------
INSERT INTO `client_connection`
    (`ClientID`, `ClientName`, `SyncId`, `DBHost`, `DBUser`, `DBPassword`, `DBName`, `Status`)
VALUES
    ('CL0001', 'MYPOS Retail (Main)', '123', 'localhost',
     'myposqrc_accts', 'CHANGE_ME_OR_USE_enc:v1:value', 'myposqrc_posretail', 1),
    ('CL0002', 'MYPOS QR Client',     '456', 'localhost',
     'myposqrc_db',    'CHANGE_ME_OR_USE_enc:v1:value', 'myposqrc_qr',        1)
ON DUPLICATE KEY UPDATE
    `ClientName` = VALUES(`ClientName`),
    `DBHost`     = VALUES(`DBHost`),
    `DBUser`     = VALUES(`DBUser`),
    `DBName`     = VALUES(`DBName`);

-- ---------------------------------------------------------------------
-- 4. (RECOMMENDED) DEDICATED, LEAST-PRIVILEGE MASTER DB USER
--    The API only ever needs SELECT on client_connection and
--    SELECT/INSERT on api_access_log. Run as root, then put these
--    credentials in controller/config/master.config.php.
-- ---------------------------------------------------------------------
-- CREATE USER 'myposqrc_masterapi'@'localhost' IDENTIFIED BY 'StrongPasswordHere';
-- GRANT SELECT ON myposqrc_master.client_connection TO 'myposqrc_masterapi'@'localhost';
-- GRANT SELECT, INSERT ON myposqrc_master.api_access_log TO 'myposqrc_masterapi'@'localhost';
-- FLUSH PRIVILEGES;

-- ---------------------------------------------------------------------
-- 5. HOUSEKEEPING (optional): purge access-log rows older than 30 days.
--    Requires event_scheduler = ON.
-- ---------------------------------------------------------------------
-- CREATE EVENT IF NOT EXISTS `ev_purge_api_access_log`
-- ON SCHEDULE EVERY 1 DAY
-- DO DELETE FROM `api_access_log` WHERE `AttemptTime` < NOW() - INTERVAL 30 DAY;
