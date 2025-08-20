-- Multiple Price Management Table Schema
-- This table stores multiple pricing options for each item

CREATE TABLE IF NOT EXISTS `item_multiple_price` (
  `price_id` int(11) NOT NULL AUTO_INCREMENT,
  `item_id` int(11) NOT NULL,
  `price_name` varchar(100) NOT NULL,
  `price_value` decimal(10,2) NOT NULL DEFAULT '0.00',
  `com_id` int(11) NOT NULL DEFAULT '1',
  `loc_id` int(11) NOT NULL DEFAULT '1',
  `status` tinyint(1) NOT NULL DEFAULT '1',
  `created` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`price_id`),
  KEY `idx_item_id` (`item_id`),
  KEY `idx_status` (`status`),
  KEY `idx_company_location` (`com_id`,`loc_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Sample data for testing (optional)
-- INSERT INTO `item_multiple_price` (`item_id`, `price_name`, `price_value`, `com_id`, `loc_id`, `status`) VALUES
-- (1, 'Wholesale', 85.00, 1, 1, 1),
-- (1, 'VIP Customer', 90.00, 1, 1, 1),
-- (1, 'Staff Discount', 75.00, 1, 1, 1),
-- (2, 'Wholesale', 450.00, 1, 1, 1),
-- (2, 'Bulk Order', 420.00, 1, 1, 1);
