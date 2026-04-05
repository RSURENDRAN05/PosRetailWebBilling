-- ============================================================
-- Face Attendance System v2 - Full Database Schema
-- Multi-tenant: Company → Branch → Location
-- Run in cPanel phpMyAdmin
-- ============================================================

SET FOREIGN_KEY_CHECKS = 0;

-- ---- Companies -----------------------------------------------
CREATE TABLE IF NOT EXISTS `companies` (
    `com_id`       VARCHAR(20)  NOT NULL PRIMARY KEY,
    `name`         VARCHAR(150) NOT NULL,
    `address`      TEXT         DEFAULT NULL,
    `email`        VARCHAR(100) DEFAULT NULL,
    `phone`        VARCHAR(30)  DEFAULT NULL,
    `logo_url`     VARCHAR(255) DEFAULT NULL,
    `status`       ENUM('active','inactive') DEFAULT 'active',
    `created_at`   TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---- Branches ------------------------------------------------
CREATE TABLE IF NOT EXISTS `branches` (
    `branch_id`    VARCHAR(20)  NOT NULL PRIMARY KEY,
    `com_id`       VARCHAR(20)  NOT NULL,
    `name`         VARCHAR(150) NOT NULL,
    `address`      TEXT         DEFAULT NULL,
    `manager_name` VARCHAR(100) DEFAULT NULL,
    `phone`        VARCHAR(30)  DEFAULT NULL,
    `status`       ENUM('active','inactive') DEFAULT 'active',
    `created_at`   TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (`com_id`) REFERENCES `companies`(`com_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---- Locations -----------------------------------------------
CREATE TABLE IF NOT EXISTS `locations` (
    `loc_id`       VARCHAR(20)  NOT NULL PRIMARY KEY,
    `branch_id`    VARCHAR(20)  NOT NULL,
    `com_id`       VARCHAR(20)  NOT NULL,
    `name`         VARCHAR(150) NOT NULL,
    `description`  VARCHAR(255) DEFAULT NULL,
    `device_pin`   VARCHAR(10)  NOT NULL DEFAULT '1234',
    `status`       ENUM('active','inactive') DEFAULT 'active',
    `created_at`   TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (`branch_id`) REFERENCES `branches`(`branch_id`) ON DELETE CASCADE,
    FOREIGN KEY (`com_id`)    REFERENCES `companies`(`com_id`)   ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---- Admin Users (web/app login) ----------------------------
CREATE TABLE IF NOT EXISTS `admin_users` (
    `id`           INT AUTO_INCREMENT PRIMARY KEY,
    `username`     VARCHAR(50)  NOT NULL UNIQUE,
    `password`     VARCHAR(255) NOT NULL,           -- bcrypt hash
    `full_name`    VARCHAR(100) DEFAULT NULL,
    `email`        VARCHAR(100) DEFAULT NULL,
    `com_id`       VARCHAR(20)  NOT NULL,
    `branch_id`    VARCHAR(20)  DEFAULT NULL,       -- NULL = all branches
    `role`         ENUM('superadmin','admin','manager') DEFAULT 'admin',
    `status`       ENUM('active','inactive') DEFAULT 'active',
    `last_login`   TIMESTAMP    DEFAULT NULL,
    `created_at`   TIMESTAMP    DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (`com_id`) REFERENCES `companies`(`com_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---- Employees ----------------------------------------------
CREATE TABLE IF NOT EXISTS `employees` (
    `id`           INT AUTO_INCREMENT PRIMARY KEY,
    `employee_id`  VARCHAR(50)  NOT NULL,
    `com_id`       VARCHAR(20)  NOT NULL,
    `branch_id`    VARCHAR(20)  NOT NULL,
    `loc_id`       VARCHAR(20)  NOT NULL,
    `name`         VARCHAR(100) NOT NULL,
    `department`   VARCHAR(100) DEFAULT NULL,
    `position`     VARCHAR(100) DEFAULT NULL,
    `email`        VARCHAR(100) DEFAULT NULL,
    `phone`        VARCHAR(30)  DEFAULT NULL,
    `status`       ENUM('active','inactive') DEFAULT 'active',
    `created_at`   TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `updated_at`   TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    UNIQUE KEY `uq_emp_com` (`employee_id`, `com_id`),
    FOREIGN KEY (`com_id`)    REFERENCES `companies`(`com_id`)   ON DELETE CASCADE,
    FOREIGN KEY (`branch_id`) REFERENCES `branches`(`branch_id`) ON DELETE CASCADE,
    FOREIGN KEY (`loc_id`)    REFERENCES `locations`(`loc_id`)   ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---- Face Encodings -----------------------------------------
CREATE TABLE IF NOT EXISTS `face_encodings` (
    `id`           INT AUTO_INCREMENT PRIMARY KEY,
    `employee_id`  VARCHAR(50)  NOT NULL,
    `com_id`       VARCHAR(20)  NOT NULL,
    `encoding`     MEDIUMTEXT   NOT NULL,           -- JSON 128-float array
    `sample_index` INT          DEFAULT 0,
    `created_at`   TIMESTAMP    DEFAULT CURRENT_TIMESTAMP,
    INDEX `idx_face_emp_com` (`employee_id`, `com_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---- Attendance ---------------------------------------------
CREATE TABLE IF NOT EXISTS `attendance` (
    `id`            INT AUTO_INCREMENT PRIMARY KEY,
    `employee_id`   VARCHAR(50)  NOT NULL,
    `com_id`        VARCHAR(20)  NOT NULL,
    `branch_id`     VARCHAR(20)  NOT NULL,
    `loc_id`        VARCHAR(20)  NOT NULL,
    `employee_name` VARCHAR(100) DEFAULT NULL,
    `date`          DATE         NOT NULL,
    `check_in`      DATETIME     DEFAULT NULL,
    `check_out`     DATETIME     DEFAULT NULL,
    `duration_mins` INT          DEFAULT NULL,
    `status`        ENUM('present','late','absent') DEFAULT 'present',
    `source`        ENUM('windows','web','mobile','manual') DEFAULT 'windows',
    `notes`         VARCHAR(255) DEFAULT NULL,
    `created_at`    TIMESTAMP    DEFAULT CURRENT_TIMESTAMP,
    UNIQUE KEY `uq_att` (`employee_id`, `com_id`, `date`),
    FOREIGN KEY (`com_id`)    REFERENCES `companies`(`com_id`)   ON DELETE CASCADE,
    FOREIGN KEY (`branch_id`) REFERENCES `branches`(`branch_id`) ON DELETE CASCADE,
    FOREIGN KEY (`loc_id`)    REFERENCES `locations`(`loc_id`)   ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

SET FOREIGN_KEY_CHECKS = 1;

-- ============================================================
-- SEED DATA — Replace with your real values
-- ============================================================
INSERT IGNORE INTO `companies` (`com_id`, `name`, `email`, `phone`) VALUES
('COM001', 'My Company Sdn Bhd', 'admin@mycompany.com', '+60123456789');

INSERT IGNORE INTO `branches` (`branch_id`, `com_id`, `name`, `address`) VALUES
('BR001', 'COM001', 'HQ Branch',      'Kuala Lumpur'),
('BR002', 'COM001', 'Penang Branch',  'Penang');

INSERT IGNORE INTO `locations` (`loc_id`, `branch_id`, `com_id`, `name`, `device_pin`) VALUES
('LOC001', 'BR001', 'COM001', 'Main Entrance',  '1234'),
('LOC002', 'BR001', 'COM001', 'Back Entrance',  '5678'),
('LOC003', 'BR002', 'COM001', 'Penang Office',  '9999');

-- Default admin user  (password: Admin@123)
INSERT IGNORE INTO `admin_users` (`username`, `password`, `full_name`, `email`, `com_id`, `role`) VALUES
('admin', '$2y$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 'System Admin', 'admin@mycompany.com', 'COM001', 'superadmin');
-- NOTE: Change password immediately after first login!

-- ============================================================
-- PATCH: Add GPS + liveness + method columns to attendance
-- Run this if already created the table
-- ============================================================
ALTER TABLE `attendance`
  ADD COLUMN IF NOT EXISTS `latitude`       DECIMAL(10,7) DEFAULT NULL AFTER `notes`,
  ADD COLUMN IF NOT EXISTS `longitude`      DECIMAL(10,7) DEFAULT NULL AFTER `latitude`,
  ADD COLUMN IF NOT EXISTS `gps_accuracy`   FLOAT         DEFAULT NULL AFTER `longitude`,
  ADD COLUMN IF NOT EXISTS `method`         ENUM('face','fingerprint','manual') DEFAULT 'face' AFTER `gps_accuracy`,
  ADD COLUMN IF NOT EXISTS `liveness_pass`  TINYINT(1)    DEFAULT 1 AFTER `method`;
