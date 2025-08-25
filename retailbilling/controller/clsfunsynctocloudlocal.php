<?php

class clsfuncsync
{
    private $conn;

    public function __construct()
    {
        require_once 'dbconnect.php';
        $db = new database();
        $this->conn = $db->connect();
    }
    public function GetPosMaster($pm_id, $comid, $locid)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM `POS_MASTER` WHERE `PM_COMID`='" . $comid . "' AND `PM_LOCID`='" . $locid . "' AND `PM_ID`='" . $pm_id . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }
}
