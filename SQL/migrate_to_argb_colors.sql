-- Migration script to update existing button_properties table to use ARGB format
-- Run this script to migrate from hex colors (#FFFFFF) to ARGB format (Argb(255,255,255,255))

-- Step 1: Modify the table structure to support longer color strings
ALTER TABLE `button_properties`
MODIFY COLUMN `text_color` varchar(50) DEFAULT 'Argb(255,255,255,255)',
MODIFY COLUMN `back_color` varchar(50) DEFAULT 'Argb(255,72,61,139)';

-- Step 2: Convert existing hex colors to ARGB format
UPDATE `button_properties` SET
  `text_color` = CASE
    -- Common hex colors to ARGB conversion
    WHEN `text_color` = '#FFFFFF' THEN 'Argb(255,255,255,255)'
    WHEN `text_color` = '#000000' THEN 'Argb(255,0,0,0)'
    WHEN `text_color` = '#FF0000' THEN 'Argb(255,255,0,0)'
    WHEN `text_color` = '#00FF00' THEN 'Argb(255,0,255,0)'
    WHEN `text_color` = '#0000FF' THEN 'Argb(255,0,0,255)'
    WHEN `text_color` = '#FFFF00' THEN 'Argb(255,255,255,0)'
    WHEN `text_color` = '#FF00FF' THEN 'Argb(255,255,0,255)'
    WHEN `text_color` = '#00FFFF' THEN 'Argb(255,0,255,255)'
    -- For other hex colors, convert programmatically
    WHEN `text_color` LIKE '#%' AND LENGTH(`text_color`) = 7 THEN
      CONCAT('Argb(255,',
        CONV(SUBSTRING(`text_color`, 2, 2), 16, 10), ',',
        CONV(SUBSTRING(`text_color`, 4, 2), 16, 10), ',',
        CONV(SUBSTRING(`text_color`, 6, 2), 16, 10), ')')
    -- If already in ARGB format or unknown format, keep as is
    ELSE `text_color`
  END,
  `back_color` = CASE
    -- Common hex colors to ARGB conversion
    WHEN `back_color` = '#FFFFFF' THEN 'Argb(255,255,255,255)'
    WHEN `back_color` = '#000000' THEN 'Argb(255,0,0,0)'
    WHEN `back_color` = '#483D8B' THEN 'Argb(255,72,61,139)'
    WHEN `back_color` = '#FF6B6B' THEN 'Argb(255,255,107,107)'
    WHEN `back_color` = '#4ECDC4' THEN 'Argb(255,78,205,196)'
    WHEN `back_color` = '#45B7D1' THEN 'Argb(255,69,183,209)'
    WHEN `back_color` = '#FF0000' THEN 'Argb(255,255,0,0)'
    WHEN `back_color` = '#00FF00' THEN 'Argb(255,0,255,0)'
    WHEN `back_color` = '#0000FF' THEN 'Argb(255,0,0,255)'
    -- For other hex colors, convert programmatically
    WHEN `back_color` LIKE '#%' AND LENGTH(`back_color`) = 7 THEN
      CONCAT('Argb(255,',
        CONV(SUBSTRING(`back_color`, 2, 2), 16, 10), ',',
        CONV(SUBSTRING(`back_color`, 4, 2), 16, 10), ',',
        CONV(SUBSTRING(`back_color`, 6, 2), 16, 10), ')')
    -- If already in ARGB format or unknown format, keep as is
    ELSE `back_color`
  END
WHERE `text_color` LIKE '#%' OR `back_color` LIKE '#%';

-- Step 3: Verify the migration
SELECT
  id,
  item_id,
  menu_type,
  item_name,
  text_color,
  back_color,
  CASE
    WHEN text_color LIKE 'Argb(%' THEN 'ARGB Format'
    WHEN text_color LIKE '#%' THEN 'Hex Format'
    ELSE 'Unknown Format'
  END AS text_color_format,
  CASE
    WHEN back_color LIKE 'Argb(%' THEN 'ARGB Format'
    WHEN back_color LIKE '#%' THEN 'Hex Format'
    ELSE 'Unknown Format'
  END AS back_color_format
FROM `button_properties`
ORDER BY menu_type, position;

-- Optional: Show conversion summary
SELECT
  'Migration Summary' AS info,
  COUNT(*) AS total_records,
  SUM(CASE WHEN text_color LIKE 'Argb(%' THEN 1 ELSE 0 END) AS text_argb_count,
  SUM(CASE WHEN back_color LIKE 'Argb(%' THEN 1 ELSE 0 END) AS back_argb_count,
  SUM(CASE WHEN text_color LIKE '#%' THEN 1 ELSE 0 END) AS text_hex_remaining,
  SUM(CASE WHEN back_color LIKE '#%' THEN 1 ELSE 0 END) AS back_hex_remaining
FROM `button_properties`;
