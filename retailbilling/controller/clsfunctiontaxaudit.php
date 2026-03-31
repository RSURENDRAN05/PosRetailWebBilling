<?php

class funcProcessTaxAudit
{
    private $conn;

    public function __construct()
    {
        require_once 'dbconnect.php';
        $db = new database();
        $this->conn = $db->connect();
    }

    // OperationType: 1=Transfer, 2=Delete by Date Range, 3=Delete by Month
    // pFromDate / pToDate  : used for OperationType 1 & 2 (pass NULL for type 3)
    // pYear / pMonth       : used for OperationType 3 (pass NULL for type 1 & 2)
    public function CallSaleToTaxTransfer(
        $pOperationType,
        $pComId,
        $pLocId,
        $pPmId,
        $pFromDate,
        $pToDate,
        $pYear,
        $pMonth
    ) {
        $conn = $this->conn;

        $opType   = (int) $pOperationType;
        $comId    = (int) $pComId;
        $locId    = (int) $pLocId;
        $pmId     = (int) $pPmId;
        $fromDate = $pFromDate !== null ? "'" . mysqli_real_escape_string($conn, $pFromDate) . "'" : "NULL";
        $toDate   = $pToDate   !== null ? "'" . mysqli_real_escape_string($conn, $pToDate)   . "'" : "NULL";
        $year     = $pYear  !== null ? (int) $pYear  : "NULL";
        $month    = $pMonth !== null ? (int) $pMonth : "NULL";

        $sql = "CALL sp_pos_sale_to_tax_transfer("
            . $opType  . ","
            . $comId   . ","
            . $locId   . ","
            . $pmId    . ","
            . $fromDate . ","
            . $toDate   . ","
            . $year    . ","
            . $month   . ")";

        $result = mysqli_query($conn, $sql);
        if ($result === false) {
            return array("Success" => false, "Msg" => mysqli_error($conn));
        }
        return array("Success" => true, "Msg" => "Operation completed successfully.");
    }

    private function ClearProcResults()
    {
        $conn = $this->conn;
        while (mysqli_more_results($conn)) {
            mysqli_next_result($conn);
            $tmp = mysqli_store_result($conn);
            if ($tmp instanceof mysqli_result) {
                mysqli_free_result($tmp);
            }
        }
    }

    // Calls: CALL sp_modifysalesreport(Mode, FromDate, ToDate, NoOfRows, BillNo, ComId, LocId)
    public function ModifySalesReport($pMode, $pFromDate, $pToDate, $pNoOfRows, $pBillNo, $pComId, $pLocId)
    {
        $conn = $this->conn;

        $mode     = "'" . mysqli_real_escape_string($conn, $pMode) . "'";
        $fromDate = $pFromDate !== null ? "'" . mysqli_real_escape_string($conn, $pFromDate) . "'" : "NULL";
        $toDate   = $pToDate   !== null ? "'" . mysqli_real_escape_string($conn, $pToDate)   . "'" : "NULL";
        $noOfRows = (int) $pNoOfRows;
        $billNo   = (int) $pBillNo;
        $comId    = (int) $pComId;
        $locId    = (int) $pLocId;

        $sql = "CALL sp_modifysalesreport("
            . $mode . ","
            . $fromDate . ","
            . $toDate . ","
            . $noOfRows . ","
            . $billNo . ","
            . $comId . ","
            . $locId . ")";

        $result = mysqli_query($conn, $sql);
        if ($result === false) {
            $msg = mysqli_error($conn);
            $this->ClearProcResults();
            return array("Success" => false, "Msg" => $msg, "Data" => array());
        }

        $rows = array();
        if ($result instanceof mysqli_result) {
            while ($row = mysqli_fetch_assoc($result)) {
                $rows[] = $row;
            }
            mysqli_free_result($result);
        }

        $this->ClearProcResults();

        if (strtoupper((string) $pMode) === 'PROCESS') {
            return array("Success" => true, "Msg" => "Process completed successfully.", "Data" => array());
        }
        return array("Success" => true, "Msg" => "Data loaded successfully.", "Data" => $rows);
    }

}
