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

        if ($action == 'get_button_properties') {
            // Get POST data
            $id = $_POST['id'] ?? 0;
            $menu_type = $_POST['menu_type'] ?? '';

            // Validate required fields
            if (empty($id) || empty($menu_type)) {
                echo json_encode([
                    'success' => false,
                    'error' => 'Missing required fields: id and menu_type'
                ]);
                exit;
            }

            // Get button properties
            $sql = "SELECT * FROM button_properties WHERE item_id = :id AND menu_type = :menu_type";
            $stmt = $conn->prepare($sql);
            $stmt->bindParam(':id', $id);
            $stmt->bindParam(':menu_type', $menu_type);
            $stmt->execute();

            if ($stmt->rowCount() > 0) {
                $result = $stmt->fetch(PDO::FETCH_ASSOC);
                echo json_encode([
                    'success' => true,
                    'data' => [
                        'id' => $result['item_id'],
                        'menu_type' => $result['menu_type'],
                        'item_name' => $result['item_name'],
                        'button_width' => $result['button_width'],
                        'button_height' => $result['button_height'],
                        'font_size' => $result['font_size'],
                        'font_name' => $result['font_name'],
                        'font_style' => $result['font_style'],
                        'text_color' => $result['text_color'],
                        'back_color' => $result['back_color'],
                        'position' => $result['position'],
                        'created_at' => $result['created_at'],
                        'updated_at' => $result['updated_at']
                    ]
                ]);
            } else {
                echo json_encode([
                    'success' => false,
                    'error' => 'Button properties not found',
                    'data' => null
                ]);
            }
        } elseif ($action == 'get_all_properties') {
            // Get all button properties for a menu type
            $menu_type = $_POST['menu_type'] ?? '';

            if (empty($menu_type)) {
                echo json_encode([
                    'success' => false,
                    'error' => 'Missing required field: menu_type'
                ]);
                exit;
            }

            $sql = "SELECT * FROM button_properties WHERE menu_type = :menu_type ORDER BY position, item_id";
            $stmt = $conn->prepare($sql);
            $stmt->bindParam(':menu_type', $menu_type);
            $stmt->execute();

            $results = $stmt->fetchAll(PDO::FETCH_ASSOC);

            echo json_encode([
                'success' => true,
                'count' => count($results),
                'data' => $results
            ]);
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
