<?php

include_once 'clsfunctiontaxaudit.php';
$clsfunreq = new funcProcessTaxAudit();

header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Credentials:true");
header("Access-Control-Allow-Methods: GET, POST, OPTIONS");
header("Access-Control-Allow-Headers:Origin,Content-Type,X-Amz-Date,Authorization,X-Api-Key,X-Amz-Security-Token,locale");
header("Content-Type:application/json");
header('Content-Type: application/json; charset=utf-8');
if (isset($_REQUEST['AjaxRequest'])) {
    // AjaxRequest 1: Transfer Sale -> Tax (OperationType = 1)
    // Required POST/GET params: ComId, LocId, PmId, FromDate (YYYY-MM-DD), ToDate (YYYY-MM-DD)
    if ((int) $_REQUEST['AjaxRequest'] == 1) {
        $getjson = $_REQUEST['json'];
        $row     = json_decode($getjson, true);
        $comId    = $row['ComId'];
        $locId    = $row['LocId'];
        $pmId     = $row['PmId'];
        $fromDate = $row['FromDate'];
        $toDate   = $row['ToDate'];
        $res = $clsfunreq->CallSaleToTaxTransfer(1, $comId, $locId, $pmId, $fromDate, $toDate, null, null);
        if ($res['Success']) {
            echo json_encode(array("Success" => true, "Msg" => $res['Msg']));
        } else {
            echo json_encode(array("Success" => false, "Msg" => $res['Msg']));
        }
    }

    // AjaxRequest 2: Delete Tax data by Date Range (OperationType = 2)
    // Required POST/GET params: ComId, LocId, PmId, FromDate (YYYY-MM-DD), ToDate (YYYY-MM-DD)
    if ((int) $_REQUEST['AjaxRequest'] == 2) {
        $getjson = $_REQUEST['json'];
        $row     = json_decode($getjson, true);
        $comId    = $row['ComId'];
        $locId    = $row['LocId'];
        $pmId     = $row['PmId'];
        $fromDate = $row['FromDate'];
        $toDate   = $row['ToDate'];
        $res = $clsfunreq->CallSaleToTaxTransfer(2, $comId, $locId, $pmId, $fromDate, $toDate, null, null);
        if ($res['Success']) {
            echo json_encode(array("Success" => true, "Msg" => $res['Msg']));
        } else {
            echo json_encode(array("Success" => false, "Msg" => $res['Msg']));
        }
    }

    // AjaxRequest 3: Delete Tax data by Month (OperationType = 3)
    // Required POST/GET params: ComId, LocId, PmId, Year, Month (1-12)
    if ((int) $_REQUEST['AjaxRequest'] == 3) {
        $getjson = $_REQUEST['json'];
        $row     = json_decode($getjson, true);
        $comId = $row['ComId'];
        $locId = $row['LocId'];
        $pmId  = $row['PmId'];
        $year  = $row['Year'];
        $month = $row['Month'];
        $res = $clsfunreq->CallSaleToTaxTransfer(3, $comId, $locId, $pmId, null, null, $year, $month);
        if ($res['Success']) {
            echo json_encode(array("Success" => true, "Msg" => $res['Msg']));
        } else {
            echo json_encode(array("Success" => false, "Msg" => $res['Msg']));
        }
    }

    // AjaxRequest 4: sp_modifysalesreport
    // Required POST/GET params in json: Mode, FromDate, ToDate, NoOfRows, BillNo, ComId, LocId
    if ((int) $_REQUEST['AjaxRequest'] == 4) {
        $getjson = $_REQUEST['json'];
        $row     = json_decode($getjson, true);

        $mode     = isset($row['Mode']) ? $row['Mode'] : 'Header';
        $fromDate = isset($row['FromDate']) ? $row['FromDate'] : null;
        $toDate   = isset($row['ToDate']) ? $row['ToDate'] : null;
        $noOfRows = isset($row['NoOfRows']) ? $row['NoOfRows'] : 0;
        $billNo   = isset($row['BillNo']) ? $row['BillNo'] : 0;
        $comId    = isset($row['ComId']) ? $row['ComId'] : 0;
        $locId    = isset($row['LocId']) ? $row['LocId'] : 0;

        $res = $clsfunreq->ModifySalesReport($mode, $fromDate, $toDate, $noOfRows, $billNo, $comId, $locId);
        if ($res['Success']) {
            echo json_encode(array("Success" => true, "Msg" => $res['Msg'], "Data" => $res['Data']));
        } else {
            echo json_encode(array("Success" => false, "Msg" => $res['Msg'], "Data" => array()));
        }
    }
}
