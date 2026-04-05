<?php
// ============================================================
// Database Configuration
// UPDATE these values with your cPanel credentials
// ============================================================

define('DB_HOST',     'YOUR_SERVER_IP');   // e.g. 123.45.67.89
define('DB_PORT',     '3306');             // Default MySQL port
define('DB_USER',     'YOUR_DB_USERNAME');
define('DB_PASS',     'YOUR_DB_PASSWORD');
define('DB_NAME',     'YOUR_DATABASE_NAME');

// API Secret Key (must match Python client config.py)
define('API_SECRET',  'face_attendance_secret_2024');

class Database {
    private static $instance = null;
    private $conn;

    private function __construct() {
        try {
            $dsn = "mysql:host=" . DB_HOST . ";port=" . DB_PORT .
                   ";dbname=" . DB_NAME . ";charset=utf8mb4";
            $this->conn = new PDO($dsn, DB_USER, DB_PASS, [
                PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION,
                PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
                PDO::ATTR_EMULATE_PREPARES   => false,
            ]);
        } catch (PDOException $e) {
            http_response_code(500);
            echo json_encode(['success' => false, 'message' => 'DB connection failed: ' . $e->getMessage()]);
            exit;
        }
    }

    public static function getInstance(): Database {
        if (self::$instance === null) {
            self::$instance = new Database();
        }
        return self::$instance;
    }

    public function getConnection(): PDO {
        return $this->conn;
    }
}
