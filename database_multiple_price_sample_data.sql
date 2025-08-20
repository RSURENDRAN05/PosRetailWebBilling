-- Sample Multiple Price Data
-- Insert sample multiple price data for testing

-- Clear existing test data (optional)
-- DELETE FROM item_multiple_price WHERE com_id = 1 AND loc_id = 1;

-- Sample multiple prices for different items
-- Assuming you have items with ITEMCODE 1, 2, 3, etc.

-- Item 1 - Multiple price options
INSERT INTO `item_multiple_price` (`item_id`, `price_name`, `price_value`, `com_id`, `loc_id`, `status`) VALUES
(1, 'Regular Price', 100.00, 1, 1, 1),
(1, 'Wholesale', 85.00, 1, 1, 1),
(1, 'VIP Customer', 90.00, 1, 1, 1),
(1, 'Staff Discount', 75.00, 1, 1, 1),
(1, 'Bulk Order (10+)', 80.00, 1, 1, 1);

-- Item 2 - Multiple price options
INSERT INTO `item_multiple_price` (`item_id`, `price_name`, `price_value`, `com_id`, `loc_id`, `status`) VALUES
(2, 'Regular Price', 500.00, 1, 1, 1),
(2, 'Wholesale', 450.00, 1, 1, 1),
(2, 'Corporate Rate', 480.00, 1, 1, 1),
(2, 'Bulk Order', 420.00, 1, 1, 1),
(2, 'Student Discount', 475.00, 1, 1, 1);

-- Item 3 - Multiple price options
INSERT INTO `item_multiple_price` (`item_id`, `price_name`, `price_value`, `com_id`, `loc_id`, `status`) VALUES
(3, 'Regular Price', 250.00, 1, 1, 1),
(3, 'Wholesale', 220.00, 1, 1, 1),
(3, 'Member Price', 235.00, 1, 1, 1),
(3, 'Seasonal Offer', 200.00, 1, 1, 1);

-- Item 4 - Multiple price options
INSERT INTO `item_multiple_price` (`item_id`, `price_name`, `price_value`, `com_id`, `loc_id`, `status`) VALUES
(4, 'Regular Price', 75.00, 1, 1, 1),
(4, 'Wholesale', 65.00, 1, 1, 1),
(4, 'Loyalty Customer', 70.00, 1, 1, 1),
(4, 'Staff Rate', 60.00, 1, 1, 1),
(4, 'Promotional', 55.00, 1, 1, 1);

-- Item 5 - Multiple price options
INSERT INTO `item_multiple_price` (`item_id`, `price_name`, `price_value`, `com_id`, `loc_id`, `status`) VALUES
(5, 'Regular Price', 150.00, 1, 1, 1),
(5, 'Wholesale', 135.00, 1, 1, 1),
(5, 'Premium Customer', 145.00, 1, 1, 1),
(5, 'Quick Sale', 125.00, 1, 1, 1);

-- Verify the inserted data
-- SELECT imp.*, im.ITEMNAME
-- FROM item_multiple_price imp
-- LEFT JOIN itemmaster im ON imp.item_id = im.ITEMCODE
-- WHERE imp.com_id = 1 AND imp.loc_id = 1 AND imp.status = 1
-- ORDER BY im.ITEMNAME, imp.price_name;
