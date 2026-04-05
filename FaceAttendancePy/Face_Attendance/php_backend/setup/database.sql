-- ============================================================
-- Face Attendance System - Database Setup
-- Run this SQL in your cPanel phpMyAdmin
-- ============================================================

-- Create employees table
CREATE TABLE IF NOT EXISTS `employees` (
    `id`            INT AUTO_INCREMENT PRIMARY KEY,
    `employee_id`   VARCHAR(50)  NOT NULL UNIQUE,
    `name`          VARCHAR(100) NOT NULL,
    `department`    VARCHAR(100) DEFAULT NULL,
    `email`         VARCHAR(100) DEFAULT NULL,
    `phone`         VARCHAR(20)  DEFAULT NULL,
    `position`      VARCHAR(100) DEFAULT NULL,
    `status`        ENUM('active','inactive') DEFAULT 'active',
    `created_at`    TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `updated_at`    TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Create face_encodings table (stores 128-dim face vectors as JSON)
CREATE TABLE IF NOT EXISTS `face_encodings` (
    `id`            INT AUTO_INCREMENT PRIMARY KEY,
    `employee_id`   VARCHAR(50) NOT NULL,
    `encoding`      MEDIUMTEXT  NOT NULL,   -- JSON array of 128 floats
    `sample_index`  INT DEFAULT 0,          -- multiple samples per employee
    `created_at`    TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (`employee_id`) REFERENCES `employees`(`employee_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Create attendance table
CREATE TABLE IF NOT EXISTS `attendance` (
    `id`            INT AUTO_INCREMENT PRIMARY KEY,
    `employee_id`   VARCHAR(50)  NOT NULL,
    `employee_name` VARCHAR(100) DEFAULT NULL,
    `date`          DATE         NOT NULL,
    `check_in`      DATETIME     DEFAULT NULL,
    `check_out`     DATETIME     DEFAULT NULL,
    `duration_mins` INT          DEFAULT NULL,
    `status`        ENUM('present','late','absent') DEFAULT 'present',
    `notes`         VARCHAR(255) DEFAULT NULL,
    `created_at`    TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE KEY `unique_attendance` (`employee_id`, `date`),
    FOREIGN KEY (`employee_id`) REFERENCES `employees`(`employee_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ============================================================
-- Sample data (optional - remove in production)
-- ============================================================
-- INSERT INTO `employees` (`employee_id`, `name`, `department`, `email`)
-- VALUES ('EMP001', 'John Doe', 'IT', 'john@company.com');
