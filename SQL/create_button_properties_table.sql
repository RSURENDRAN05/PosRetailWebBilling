-- SQL script to create button_properties table
-- Run this script in your MySQL database

CREATE TABLE IF NOT EXISTS `button_properties` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `item_id` int(11) NOT NULL,
  `menu_type` enum('Main','Sub','Item') NOT NULL,
  `font_size` decimal(4,1) DEFAULT 10.0,
  `font_name` varchar(100) DEFAULT 'Segoe UI',
  `font_style` varchar(50) DEFAULT 'Regular',
  `text_color` varchar(50) DEFAULT 'Argb(255,255,255,255)',
  `back_color` varchar(50) DEFAULT 'Argb(255,72,61,139)',
  `position` int(11) DEFAULT 0,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `unique_item_menu` (`item_id`,`menu_type`),
  KEY `idx_menu_type` (`menu_type`),
  KEY `idx_item_id` (`item_id`),
  KEY `idx_position` (`position`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Insert some sample data with ARGB format (optional)
INSERT INTO `button_properties`
(`item_id`, `menu_type`, `item_name`, `button_width`, `button_height`, `font_size`, `font_name`, `font_style`, `text_color`, `back_color`, `position`)
VALUES
(1, 'Main', 'Food Items', 150, 70, 12.0, 'Arial', 'Bold', 'Argb(255,255,255,255)', 'Argb(255,255,107,107)', 1),
(2, 'Main', 'Beverages', 130, 65, 11.0, 'Tahoma', 'Regular', 'Argb(255,0,0,0)', 'Argb(255,78,205,196)', 2),
(3, 'Main', 'Desserts', 140, 60, 10.5, 'Segoe UI', 'Bold', 'Argb(255,255,255,255)', 'Argb(255,69,183,209)', 3);

-- Create index for faster queries
CREATE INDEX `idx_menu_position` ON `button_properties` (`menu_type`, `position`);

-- Sample query to get all properties for Main menu
-- SELECT * FROM button_properties WHERE menu_type = 'Main' ORDER BY position;

-- Update existing records to ARGB format (if you have existing data)
-- UPDATE button_properties SET
--   text_color = CASE
--     WHEN text_color = '#FFFFFF' THEN 'Argb(255,255,255,255)'
--     WHEN text_color = '#000000' THEN 'Argb(255,0,0,0)'
--     ELSE CONCAT('Argb(255,', CONV(SUBSTRING(text_color, 2, 2), 16, 10), ',', CONV(SUBSTRING(text_color, 4, 2), 16, 10), ',', CONV(SUBSTRING(text_color, 6, 2), 16, 10), ')')
--   END,
--   back_color = CASE
--     WHEN back_color = '#483D8B' THEN 'Argb(255,72,61,139)'
--     WHEN back_color = '#FF6B6B' THEN 'Argb(255,255,107,107)'
--     ELSE CONCAT('Argb(255,', CONV(SUBSTRING(back_color, 2, 2), 16, 10), ',', CONV(SUBSTRING(back_color, 4, 2), 16, 10), ',', CONV(SUBSTRING(back_color, 6, 2), 16, 10), ')')
--   END;
