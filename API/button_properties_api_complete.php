<?php

/**
 * Complete Button Properties API with ARGB Color Support
 * Handles saving and retrieving button properties with ARGB color format
 * Format: Argb(255,255,255,255) where values are Alpha,Red,Green,Blue
 */

header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type');

// Database configuration - Update these values
$servername = "localhost";
$username = "your_username";
$password = "your_password";
$dbname = "your_database";

try {
    // Create PDO connection
    $conn = new PDO("mysql:host=$servername;dbname=$dbname;charset=utf8mb4", $username, $password);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    $conn->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_ASSOC);

    if ($_SERVER['REQUEST_METHOD'] == 'POST') {
        $action = $_POST['action'] ?? '';

        switch ($action) {
            case 'save_button_properties':
                saveButtonProperties($conn);
                break;
            case 'get_button_properties':
                getButtonProperties($conn);
                break;
            case 'get_all_properties':
                getAllProperties($conn);
                break;
            case 'delete_button_properties':
                deleteButtonProperties($conn);
                break;
            case 'test_argb_conversion':
                testARGBConversion();
                break;
            default:
                echo json_encode([
                    'success' => false,
                    'error' => 'Invalid action. Supported actions: save_button_properties, get_button_properties, get_all_properties, delete_button_properties'
                ]);
        }
    } else {
        echo json_encode([
            'success' => false,
            'error' => 'Only POST method allowed'
        ]);
    }
} catch (PDOException $e) {
    echo json_encode([
        'success' => false,
        'error' => 'Database connection error: ' . $e->getMessage()
    ]);
} catch (Exception $e) {
    echo json_encode([
        'success' => false,
        'error' => 'Server error: ' . $e->getMessage()
    ]);
}

// Close connection
$conn = null;

/**
 * Save button properties with ARGB color support
 */
function saveButtonProperties($conn)
{
    try {
        // Get and validate input
        $id = $_POST['id'] ?? 0;
        $menu_type = $_POST['menu_type'] ?? '';
        $item_name = $_POST['item_name'] ?? '';
        $button_width = $_POST['button_width'] ?? 120;
        $button_height = $_POST['button_height'] ?? 60;
        $font_size = $_POST['font_size'] ?? 10.0;
        $font_name = $_POST['font_name'] ?? 'Segoe UI';
        $font_style = $_POST['font_style'] ?? 'Regular';
        $text_color = $_POST['text_color'] ?? 'Argb(255,255,255,255)';
        $back_color = $_POST['back_color'] ?? 'Argb(255,72,61,139)';
        $position = $_POST['position'] ?? 0;

        // Validate required fields
        if (empty($id) || empty($menu_type)) {
            echo json_encode([
                'success' => false,
                'error' => 'Missing required fields: id and menu_type'
            ]);
            return;
        }

        // Validate ARGB color format
        if (!isValidARGB($text_color)) {
            echo json_encode([
                'success' => false,
                'error' => 'Invalid text_color ARGB format. Expected: Argb(255,255,255,255)'
            ]);
            return;
        }

        if (!isValidARGB($back_color)) {
            echo json_encode([
                'success' => false,
                'error' => 'Invalid back_color ARGB format. Expected: Argb(255,255,255,255)'
            ]);
            return;
        }

        // Check if record exists
        $checkSql = "SELECT id FROM button_properties WHERE item_id = :id AND menu_type = :menu_type";
        $checkStmt = $conn->prepare($checkSql);
        $checkStmt->bindParam(':id', $id, PDO::PARAM_INT);
        $checkStmt->bindParam(':menu_type', $menu_type, PDO::PARAM_STR);
        $checkStmt->execute();

        if ($checkStmt->rowCount() > 0) {
            // Update existing record
            $sql = "UPDATE button_properties SET
                        item_name = :item_name,
                        button_width = :button_width,
                        button_height = :button_height,
                        font_size = :font_size,
                        font_name = :font_name,
                        font_style = :font_style,
                        text_color = :text_color,
                        back_color = :back_color,
                        position = :position,
                        updated_at = NOW()
                    WHERE item_id = :id AND menu_type = :menu_type";
        } else {
            // Insert new record
            $sql = "INSERT INTO button_properties
                        (item_id, menu_type, item_name, button_width, button_height,
                         font_size, font_name, font_style, text_color, back_color,
                         position, created_at, updated_at)
                    VALUES
                        (:id, :menu_type, :item_name, :button_width, :button_height,
                         :font_size, :font_name, :font_style, :text_color, :back_color,
                         :position, NOW(), NOW())";
        }

        $stmt = $conn->prepare($sql);
        $stmt->bindParam(':id', $id, PDO::PARAM_INT);
        $stmt->bindParam(':menu_type', $menu_type, PDO::PARAM_STR);
        $stmt->bindParam(':item_name', $item_name, PDO::PARAM_STR);
        $stmt->bindParam(':button_width', $button_width, PDO::PARAM_INT);
        $stmt->bindParam(':button_height', $button_height, PDO::PARAM_INT);
        $stmt->bindParam(':font_size', $font_size, PDO::PARAM_STR);
        $stmt->bindParam(':font_name', $font_name, PDO::PARAM_STR);
        $stmt->bindParam(':font_style', $font_style, PDO::PARAM_STR);
        $stmt->bindParam(':text_color', $text_color, PDO::PARAM_STR);
        $stmt->bindParam(':back_color', $back_color, PDO::PARAM_STR);
        $stmt->bindParam(':position', $position, PDO::PARAM_INT);

        if ($stmt->execute()) {
            echo json_encode([
                'success' => true,
                'message' => 'Button properties saved successfully',
                'data' => [
                    'id' => $id,
                    'menu_type' => $menu_type,
                    'item_name' => $item_name,
                    'text_color' => $text_color,
                    'back_color' => $back_color,
                    'text_color_rgba' => argbToRGBA($text_color),
                    'back_color_rgba' => argbToRGBA($back_color)
                ]
            ]);
        } else {
            echo json_encode([
                'success' => false,
                'error' => 'Failed to save button properties'
            ]);
        }
    } catch (Exception $e) {
        echo json_encode([
            'success' => false,
            'error' => 'Error saving properties: ' . $e->getMessage()
        ]);
    }
}

/**
 * Get button properties by id and menu_type
 */
function getButtonProperties($conn)
{
    try {
        $id = $_POST['id'] ?? 0;
        $menu_type = $_POST['menu_type'] ?? '';

        if (empty($id) || empty($menu_type)) {
            echo json_encode([
                'success' => false,
                'error' => 'Missing required fields: id and menu_type'
            ]);
            return;
        }

        $sql = "SELECT * FROM button_properties WHERE item_id = :id AND menu_type = :menu_type";
        $stmt = $conn->prepare($sql);
        $stmt->bindParam(':id', $id, PDO::PARAM_INT);
        $stmt->bindParam(':menu_type', $menu_type, PDO::PARAM_STR);
        $stmt->execute();

        if ($stmt->rowCount() > 0) {
            $result = $stmt->fetch();

            // Add RGBA conversion for convenience
            $result['text_color_rgba'] = argbToRGBA($result['text_color']);
            $result['back_color_rgba'] = argbToRGBA($result['back_color']);

            echo json_encode([
                'success' => true,
                'data' => $result
            ]);
        } else {
            echo json_encode([
                'success' => false,
                'error' => 'Button properties not found',
                'data' => null
            ]);
        }
    } catch (Exception $e) {
        echo json_encode([
            'success' => false,
            'error' => 'Error retrieving properties: ' . $e->getMessage()
        ]);
    }
}

/**
 * Get all button properties, optionally filtered by menu_type
 */
function getAllProperties($conn)
{
    try {
        $menu_type = $_POST['menu_type'] ?? '';

        if (!empty($menu_type)) {
            $sql = "SELECT * FROM button_properties WHERE menu_type = :menu_type ORDER BY position, item_id";
            $stmt = $conn->prepare($sql);
            $stmt->bindParam(':menu_type', $menu_type, PDO::PARAM_STR);
        } else {
            $sql = "SELECT * FROM button_properties ORDER BY menu_type, position, item_id";
            $stmt = $conn->prepare($sql);
        }

        $stmt->execute();
        $results = $stmt->fetchAll();

        // Add RGBA conversion for each result
        foreach ($results as &$result) {
            $result['text_color_rgba'] = argbToRGBA($result['text_color']);
            $result['back_color_rgba'] = argbToRGBA($result['back_color']);
        }

        echo json_encode([
            'success' => true,
            'count' => count($results),
            'data' => $results
        ]);
    } catch (Exception $e) {
        echo json_encode([
            'success' => false,
            'error' => 'Error retrieving all properties: ' . $e->getMessage()
        ]);
    }
}

/**
 * Delete button properties
 */
function deleteButtonProperties($conn)
{
    try {
        $id = $_POST['id'] ?? 0;
        $menu_type = $_POST['menu_type'] ?? '';

        if (empty($id) || empty($menu_type)) {
            echo json_encode([
                'success' => false,
                'error' => 'Missing required fields: id and menu_type'
            ]);
            return;
        }

        $sql = "DELETE FROM button_properties WHERE item_id = :id AND menu_type = :menu_type";
        $stmt = $conn->prepare($sql);
        $stmt->bindParam(':id', $id, PDO::PARAM_INT);
        $stmt->bindParam(':menu_type', $menu_type, PDO::PARAM_STR);

        if ($stmt->execute() && $stmt->rowCount() > 0) {
            echo json_encode([
                'success' => true,
                'message' => 'Button properties deleted successfully'
            ]);
        } else {
            echo json_encode([
                'success' => false,
                'error' => 'Button properties not found or already deleted'
            ]);
        }
    } catch (Exception $e) {
        echo json_encode([
            'success' => false,
            'error' => 'Error deleting properties: ' . $e->getMessage()
        ]);
    }
}

/**
 * Test ARGB conversion functions
 */
function testARGBConversion()
{
    $testColors = [
        'Argb(255,255,255,255)',
        'Argb(255,0,0,0)',
        'Argb(255,255,0,0)',
        'Argb(128,255,255,255)',
        'invalid_format'
    ];

    $results = [];
    foreach ($testColors as $color) {
        $results[] = [
            'input' => $color,
            'is_valid' => isValidARGB($color),
            'rgba_conversion' => argbToRGBA($color)
        ];
    }

    echo json_encode([
        'success' => true,
        'message' => 'ARGB conversion test results',
        'data' => $results
    ]);
}

/**
 * Validate ARGB color format: Argb(255,255,255,255)
 */
function isValidARGB($argb)
{
    $pattern = '/^Argb\(\s*(\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(\d{1,3})\s*\)$/';
    if (preg_match($pattern, $argb, $matches)) {
        // Check if all values are within 0-255 range
        for ($i = 1; $i <= 4; $i++) {
            $value = intval($matches[$i]);
            if ($value < 0 || $value > 255) {
                return false;
            }
        }
        return true;
    }
    return false;
}

/**
 * Convert ARGB format to RGBA array for convenience
 */
function argbToRGBA($argb)
{
    $pattern = '/^Argb\(\s*(\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(\d{1,3})\s*\)$/';
    if (preg_match($pattern, $argb, $matches)) {
        return [
            'a' => intval($matches[1]),
            'r' => intval($matches[2]),
            'g' => intval($matches[3]),
            'b' => intval($matches[4]),
            'hex' => sprintf('#%02X%02X%02X', intval($matches[2]), intval($matches[3]), intval($matches[4]))
        ];
    }
    return null;
}

/**
 * Convert hex color to ARGB format (utility function)
 */
function hexToARGB($hex, $alpha = 255)
{
    $hex = ltrim($hex, '#');
    if (strlen($hex) === 6) {
        $r = hexdec(substr($hex, 0, 2));
        $g = hexdec(substr($hex, 2, 2));
        $b = hexdec(substr($hex, 4, 2));
        return "Argb($alpha,$r,$g,$b)";
    }
    return null;
}
