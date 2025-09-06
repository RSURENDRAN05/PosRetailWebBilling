<?php

/**
 * Test script for Button Properties API (MenuRequest=11)
 * This demonstrates how to use the new button properties functionality
 */

// Base URL for your API
$base_url = "http://localhost/retailbilling/controller/getfunctionmgmt.php";

/**
 * Test function to make API calls
 */
function testButtonPropertiesAPI($operation, $data = array())
{
    global $base_url;

    $data['MenuRequest'] = 11;
    $data['operation'] = $operation;

    // Convert data to URL-encoded string
    $postData = http_build_query($data);

    // Create context for POST request
    $context = stream_context_create([
        'http' => [
            'method' => 'POST',
            'header' => 'Content-Type: application/x-www-form-urlencoded',
            'content' => $postData
        ]
    ]);

    // Make the request
    $response = file_get_contents($base_url, false, $context);

    return json_decode($response, true);
}

echo "<h2>Button Properties API Test Results</h2>";

// Test 1: Insert a new button property
echo "<h3>Test 1: Insert Button Properties</h3>";
$insertData = [
    'item_id' => 1,
    'menu_type' => 'Main',
    'font_size' => 12.0,
    'font_name' => 'Arial',
    'font_style' => 'Bold',
    'text_color' => 'Argb(255,255,255,255)', // White
    'back_color' => 'Argb(255,255,107,107)', // Light Red
    'position' => 1
];

$result1 = testButtonPropertiesAPI('INSERT', $insertData);
echo "<pre>" . print_r($result1, true) . "</pre>";

// Test 2: Insert another button property
echo "<h3>Test 2: Insert Another Button Property</h3>";
$insertData2 = [
    'item_id' => 2,
    'menu_type' => 'Main',
    'font_size' => 11.0,
    'font_name' => 'Tahoma',
    'font_style' => 'Regular',
    'text_color' => 'Argb(255,0,0,0)', // Black
    'back_color' => 'Argb(255,78,205,196)', // Teal
    'position' => 2
];

$result2 = testButtonPropertiesAPI('INSERT', $insertData2);
echo "<pre>" . print_r($result2, true) . "</pre>";

// Test 3: Get all button properties
echo "<h3>Test 3: Get All Button Properties</h3>";
$result3 = testButtonPropertiesAPI('SELECT');
echo "<pre>" . print_r($result3, true) . "</pre>";

// Test 4: Get button properties filtered by menu_type
echo "<h3>Test 4: Get Button Properties by Menu Type</h3>";
$result4 = testButtonPropertiesAPI('SELECT', ['menu_type' => 'Main']);
echo "<pre>" . print_r($result4, true) . "</pre>";

// Test 5: Update button properties
echo "<h3>Test 5: Update Button Properties</h3>";
$updateData = [
    'item_id' => 1,
    'menu_type' => 'Main',
    'font_size' => 14.0,
    'font_name' => 'Segoe UI',
    'font_style' => 'Bold',
    'text_color' => 'Argb(255,255,255,255)', // White
    'back_color' => 'Argb(255,69,183,209)', // Blue
    'position' => 1
];

$result5 = testButtonPropertiesAPI('UPDATE', $updateData);
echo "<pre>" . print_r($result5, true) . "</pre>";

// Test 6: Get specific button properties
echo "<h3>Test 6: Get Specific Button Properties</h3>";
$result6 = testButtonPropertiesAPI('SELECT', ['item_id' => 1, 'menu_type' => 'Main']);
echo "<pre>" . print_r($result6, true) . "</pre>";

// Test 7: Try to insert duplicate (should update instead)
echo "<h3>Test 7: Insert Duplicate (Should Update)</h3>";
$duplicateData = [
    'item_id' => 1,
    'menu_type' => 'Main',
    'font_size' => 16.0,
    'font_name' => 'Times New Roman',
    'font_style' => 'Italic',
    'text_color' => 'Argb(255,255,0,0)', // Red
    'back_color' => 'Argb(255,0,255,0)', // Green
    'position' => 1
];

$result7 = testButtonPropertiesAPI('INSERT', $duplicateData);
echo "<pre>" . print_r($result7, true) . "</pre>";

// Test 8: Delete button properties
echo "<h3>Test 8: Delete Button Properties</h3>";
$result8 = testButtonPropertiesAPI('DELETE', ['item_id' => 2, 'menu_type' => 'Main']);
echo "<pre>" . print_r($result8, true) . "</pre>";

// Test 9: Final check - get all remaining properties
echo "<h3>Test 9: Final Check - Get All Remaining Properties</h3>";
$result9 = testButtonPropertiesAPI('SELECT');
echo "<pre>" . print_r($result9, true) . "</pre>";

echo "<h3>Testing Complete!</h3>";
?>

<!DOCTYPE html>
<html>

<head>
    <title>Button Properties API Test</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }

        h2 {
            color: #333;
            border-bottom: 2px solid #ddd;
        }

        h3 {
            color: #666;
            margin-top: 30px;
        }

        pre {
            background: #f5f5f5;
            padding: 10px;
            border-radius: 5px;
            overflow-x: auto;
        }

        .success {
            color: green;
        }

        .error {
            color: red;
        }
    </style>
</head>

<body>

    <h2>Button Properties API Usage Examples</h2>

    <h3>VB.NET Usage Example:</h3>
    <pre>
' Example VB.NET code to use the Button Properties API

Dim url As String = "http://yourserver.com/retailbilling/controller/getfunctionmgmt.php"
Dim postData As String = ""

' INSERT Example
postData = "MenuRequest=11&operation=INSERT" &
           "&item_id=1&menu_type=Main" &
           "&font_size=12.0&font_name=Arial&font_style=Bold" &
           "&text_color=Argb(255,255,255,255)" &
           "&back_color=Argb(255,255,107,107)" &
           "&position=1"

' SELECT Example
postData = "MenuRequest=11&operation=SELECT&menu_type=Main"

' UPDATE Example
postData = "MenuRequest=11&operation=UPDATE" &
           "&item_id=1&menu_type=Main" &
           "&font_size=14.0&font_name=Segoe UI&font_style=Bold" &
           "&text_color=Argb(255,255,255,255)" &
           "&back_color=Argb(255,69,183,209)" &
           "&position=1"

' DELETE Example
postData = "MenuRequest=11&operation=DELETE&item_id=1&menu_type=Main"
</pre>

    <h3>JSON Data Example:</h3>
    <pre>
{
    "MenuRequest": 11,
    "operation": "INSERT",
    "item_id": 1,
    "menu_type": "Main",
    "font_size": 12.0,
    "font_name": "Arial",
    "font_style": "Bold",
    "text_color": "Argb(255,255,255,255)",
    "back_color": "Argb(255,255,107,107)",
    "position": 1
}
</pre>

    <h3>Database Table Structure:</h3>
    <pre>
CREATE TABLE `pos_button_properties` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `item_id` int(11) NOT NULL,
  `menu_type` enum('Main','Sub','Item') NOT NULL,
  `font_size` decimal(4,1) DEFAULT 10.0,
  `font_name` varchar(100) DEFAULT 'Segoe UI',
  `font_style` varchar(50) DEFAULT 'Regular',
  `text_color` varchar(50) DEFAULT 'Argb(255,255,255,255)',
  `back_color` varchar(50) DEFAULT 'Argb(255,72,61,139)',
  `position` int(11) DEFAULT 0,
  `created_at` timestamp DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `unique_item_menu` (`item_id`,`menu_type`)
);
</pre>

</body>

</html>
