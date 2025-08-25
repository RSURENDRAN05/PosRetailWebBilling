<?php

include_once 'clsfunsynctocloudlocal.php';
$clsfunreq = new clsfuncsync();


header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Credentials:true");
header("Access-Control-Allow-Methods: GET, POST, OPTIONS");
header("Access-Control-Allow-Headers:Origin,Content-Type,X-Amz-Date,Authorization,X-Api-Key,X-Amz-Security-Token,locale");
header("Content-Type:application/json");
header('Content-Type: application/json; charset=utf-8');
if (isset($_REQUEST['AjaxRequest'])) { //POS_MASTER
    if ((int)$_REQUEST['AjaxRequest'] === 1) {
        $pm_id = $_REQUEST['pm_id'];
        $comid = $_REQUEST['comid'];
        $locid = $_REQUEST['locid'];
        $GetPosMaster = $clsfunreq->GetPosMaster($pm_id, $comid, $locid);
        $GetPosMasterRes = array();
        while ($rows = mysqli_fetch_assoc($GetPosMaster)) {
            $GetPosMasterRes[] = $rows;
        }
        if ($GetPosMaster) {
            echo json_encode(array("Success" => true, "Data" => $GetPosMasterRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
}
