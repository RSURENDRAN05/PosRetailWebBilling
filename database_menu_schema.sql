-- Menu Management Database Schema
-- Execute this script to create the required tables for the Menu Manager

-- Create pos_headermenu table
CREATE TABLE IF NOT EXISTS `pos_headermenu` (
  `phid` int(11) NOT NULL AUTO_INCREMENT,
  `ph_name` varchar(100) NOT NULL,
  `ph_menucode` varchar(50) NOT NULL,
  `ph_active` tinyint(1) DEFAULT '1',
  `ph_created_date` timestamp DEFAULT CURRENT_TIMESTAMP,
  `ph_updated_date` timestamp DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`phid`),
  UNIQUE KEY `uk_ph_menucode` (`ph_menucode`),
  KEY `idx_ph_active` (`ph_active`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Create pos_submenu table
CREATE TABLE IF NOT EXISTS `pos_submenu` (
  `psid` int(11) NOT NULL AUTO_INCREMENT,
  `ps_name` varchar(100) NOT NULL,
  `ps_menucode` varchar(50) NOT NULL,
  `ph_id` int(11) NOT NULL,
  `ps_active` tinyint(1) DEFAULT '1',
  `ps_created_date` timestamp DEFAULT CURRENT_TIMESTAMP,
  `ps_updated_date` timestamp DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`psid`),
  UNIQUE KEY `uk_ps_menucode` (`ps_menucode`),
  KEY `fk_ph_id` (`ph_id`),
  KEY `idx_ps_active` (`ps_active`),
  CONSTRAINT `fk_submenu_header` FOREIGN KEY (`ph_id`) REFERENCES `pos_headermenu` (`phid`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Sample data for testing (optional)
-- Insert some sample header menus
INSERT IGNORE INTO `pos_headermenu` (`ph_name`, `ph_menucode`, `ph_active`) VALUES
('Master Data', 'MASTER', 1),
('Sales', 'SALES', 1),
('Purchase', 'PURCHASE', 1),
('Reports', 'REPORTS', 1),
('Settings', 'SETTINGS', 1);

-- Insert some sample sub menus
INSERT IGNORE INTO `pos_submenu` (`ps_name`, `ps_menucode`, `ph_id`, `ps_active`) VALUES
('Item Master', 'ITEM_MASTER', 1, 1),
('Customer Master', 'CUSTOMER_MASTER', 1, 1),
('Supplier Master', 'SUPPLIER_MASTER', 1, 1),
('POS Sales', 'POS_SALES', 2, 1),
('Sales Report', 'SALES_REPORT', 2, 1),
('Purchase Entry', 'PURCHASE_ENTRY', 3, 1),
('Purchase Report', 'PURCHASE_REPORT', 3, 1),
('Daily Sales', 'DAILY_SALES', 4, 1),
('Monthly Sales', 'MONTHLY_SALES', 4, 1),
('User Management', 'USER_MGMT', 5, 1),
('System Settings', 'SYS_SETTINGS', 5, 1);

-- Optimized query for the menu manager form (as mentioned in your request)
-- This query joins both tables to show sub menu with its parent header menu
SELECT
    sm.psid,
    sm.ps_name AS SubMenuName,
    hm.phid,
    hm.ph_name AS HeaderMenuName,
    sm.ps_active AS SubMenuActive,
    hm.ph_active AS HeaderMenuActive,
    sm.ps_menucode AS SubMenuCode,
    hm.ph_menucode AS HeaderMenuCode
FROM
    pos_submenu sm
INNER JOIN
    pos_headermenu hm
    ON sm.ph_id = hm.phid
ORDER BY
    hm.ph_name, sm.ps_name;
