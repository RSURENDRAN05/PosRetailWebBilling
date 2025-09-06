-- SQL Script to Enable MenuSales Permission
-- This will make the Sales ribbon tab (containing Menu Design button) visible

-- Check if MenuSales permission exists for your user
SELECT * FROM user_policy_table WHERE menu_name = 'MenuSales';

-- If it doesn't exist, insert it (replace USER_ID with your actual user ID)
INSERT INTO user_policy_table (user_id, menu_name, menu_active)
VALUES (YOUR_USER_ID, 'MenuSales', '1');

-- If it exists but is disabled, update it
UPDATE user_policy_table
SET menu_active = '1'
WHERE menu_name = 'MenuSales' AND user_id = YOUR_USER_ID;
