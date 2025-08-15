<?php

class DB_Connect {

    private $conn;

    // Connecting to database
    public function connect() {

        require_once '../controller/Config.php';
        $conn = mysqli_connect(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);
        if ($conn) {
            $this->conn = $conn;
            return $conn;
        } else {
            echo 'Sql Failed';
        }
    }
}
 

?>