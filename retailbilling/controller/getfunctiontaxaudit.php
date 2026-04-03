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

    // ───────────────────────────────────────────────────────────────
    //  pos_tax_final  endpoints (AjaxRequest 5 – 10)
    // ───────────────────────────────────────────────────────────────

    // AjaxRequest 5: Delete from pos_tax_final by Date
    // json params: Date (YYYY-MM-DD), ComId, LocId
    if ((int) $_REQUEST['AjaxRequest'] == 5) {
        $getjson = $_REQUEST['json'];
        $row     = json_decode($getjson, true);

        $res = $clsfunreq->DeletePosTaxFinalByDate($row['Date'], $row['ComId'], $row['LocId']);
        echo json_encode($res);
    }

    // AjaxRequest 6: Delete from pos_tax_final by Month
    // json params: Year, Month, ComId, LocId
    if ((int) $_REQUEST['AjaxRequest'] == 6) {
        $getjson = $_REQUEST['json'];
        $row     = json_decode($getjson, true);

        $res = $clsfunreq->DeletePosTaxFinalByMonth($row['Year'], $row['Month'], $row['ComId'], $row['LocId']);
        echo json_encode($res);
    }

    // AjaxRequest 7: Select final summary + paymode by Date
    // json params: Date (YYYY-MM-DD), ComId, LocId
    if ((int) $_REQUEST['AjaxRequest'] == 7) {
        $getjson = $_REQUEST['json'];
        $row     = json_decode($getjson, true);

        $res = $clsfunreq->SelectPosTaxFinalByDate($row['Date'], $row['ComId'], $row['LocId']);
        echo json_encode($res);
    }

    // AjaxRequest 8: Select final summary + paymode by Month
    // json params: Year, Month, ComId, LocId
    if ((int) $_REQUEST['AjaxRequest'] == 8) {
        $getjson = $_REQUEST['json'];
        $row     = json_decode($getjson, true);

        $res = $clsfunreq->SelectPosTaxFinalByMonth($row['Year'], $row['Month'], $row['ComId'], $row['LocId']);
        echo json_encode($res);
    }

    // AjaxRequest 10: Insert final tax record (delete existing + insert via SP)
    // json params: Date (YYYY-MM-DD), ComId, LocId
    if ((int) $_REQUEST['AjaxRequest'] == 10) {
        $getjson = $_REQUEST['json'];
        $row     = json_decode($getjson, true);

        $res = $clsfunreq->InsertPosTaxFinalSP($row['Date'], $row['ComId'], $row['LocId']);
        echo json_encode($res);
    }
}
