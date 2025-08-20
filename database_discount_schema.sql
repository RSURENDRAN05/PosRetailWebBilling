-- Discount Management Table Schema
-- This table stores discount types that can be applied to items or bills

CREATE TABLE IF NOT EXISTS `discount_master` (
  `discount_id` int(11) NOT NULL AUTO_INCREMENT,
  `discount_name` varchar(100) NOT NULL,
  `discount_type` enum('percentage','amount') NOT NULL DEFAULT 'percentage',
  `discount_value` decimal(10,2) NOT NULL DEFAULT '0.00',
  `discount_description` text,
  `status` tinyint(1) NOT NULL DEFAULT '1',
  `com_id` int(11) NOT NULL DEFAULT '1',
  `loc_id` int(11) NOT NULL DEFAULT '1',
  `created` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`discount_id`),
  KEY `idx_status` (`status`),
  KEY `idx_type` (`discount_type`),
  KEY `idx_company_location` (`com_id`,`loc_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Sample discount data
INSERT INTO `discount_master` (`discount_name`, `discount_type`, `discount_value`, `discount_description`, `status`, `com_id`, `loc_id`) VALUES
('Senior Citizen', 'percentage', 10.00, 'Senior citizen discount - 10% off', 1, 1, 1),
('Staff Discount', 'percentage', 15.00, 'Employee discount - 15% off', 1, 1, 1),
('VIP Customer', 'percentage', 20.00, 'VIP customer special discount', 1, 1, 1),
('Bulk Order', 'percentage', 5.00, 'Bulk order discount for large quantities', 1, 1, 1),
('Festival Offer', 'percentage', 25.00, 'Special festival season discount', 1, 1, 1),
('First Time Customer', 'amount', 50.00, 'Flat Rs.50 off for new customers', 1, 1, 1),
('Express Discount', 'amount', 100.00, 'Flat Rs.100 off on express orders', 1, 1, 1),
('Student Discount', 'percentage', 12.00, 'Student discount with valid ID', 1, 1, 1);
