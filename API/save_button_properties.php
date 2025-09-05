<?php
header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type');

// Database configuration
$servername = "localhost";
$username = "your_username";
$password = "your_password";
$dbname = "your_database";

try {
    // Create connection
    $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    if ($_SERVER['REQUEST_METHOD'] == 'POST') {
        $action = $_POST['action'] ?? '';

        if ($action == 'save_button_properties') {
            // Get POST data
            $id = $_POST['id'] ?? 0;
            $menu_type = $_POST['menu_type'] ?? '';
            $item_name = $_POST['item_name'] ?? '';
            $button_width = $_POST['button_width'] ?? 120;
            $button_height = $_POST['button_height'] ?? 60;
            $font_size = $_POST['font_size'] ?? 10;
            $font_name = $_POST['font_name'] ?? 'Segoe UI';
            $font_style = $_POST['font_style'] ?? 'Regular';
            $text_color = $_POST['text_color'] ?? '#FFFFFF';
            $back_color = $_POST['back_color'] ?? '#483D8B';
            $position = $_POST['position'] ?? 0;

            // Validate required fields
            if (empty($id) || empty($menu_type)) {
                echo json_encode([
                    'success' => false,
                    'error' => 'Missing required fields: id and menu_type'
                ]);
                exit;
            }

            // Check if record exists
            $checkSql = "SELECT id FROM button_properties WHERE item_id = :id AND menu_type = :menu_type";
            $checkStmt = $conn->prepare($checkSql);
            $checkStmt->bindParam(':id', $id);
            $checkStmt->bindParam(':menu_type', $menu_type);
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
            $stmt->bindParam(':id', $id);
            $stmt->bindParam(':menu_type', $menu_type);
            $stmt->bindParam(':item_name', $item_name);
            $stmt->bindParam(':button_width', $button_width);
            $stmt->bindParam(':button_height', $button_height);
            $stmt->bindParam(':font_size', $font_size);
            $stmt->bindParam(':font_name', $font_name);
            $stmt->bindParam(':font_style', $font_style);
            $stmt->bindParam(':text_color', $text_color);
            $stmt->bindParam(':back_color', $back_color);
            $stmt->bindParam(':position', $position);

            if ($stmt->execute()) {
                echo json_encode([
                    'success' => true,
                    'message' => 'Button properties saved successfully',
                    'data' => [
                        'id' => $id,
                        'menu_type' => $menu_type,
                        'item_name' => $item_name
                    ]
                ]);
            } else {
                echo json_encode([
                    'success' => false,
                    'error' => 'Failed to save button properties'
                ]);
            }
        } else {
            echo json_encode([
                'success' => false,
                'error' => 'Invalid action'
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
        'error' => 'Database error: ' . $e->getMessage()
    ]);
} catch (Exception $e) {
    echo json_encode([
        'success' => false,
        'error' => 'Server error: ' . $e->getMessage()
    ]);
}

// Close connection
$conn = null;
