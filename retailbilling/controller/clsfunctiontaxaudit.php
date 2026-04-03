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

        $this->ClearProcResults();

        if ($opType === 2) {
            $this->DeletePosTaxFinalByDate($pFromDate, $comId, $locId);
        } elseif ($opType === 3) {
            $this->DeletePosTaxFinalByMonth($year, $month, $comId, $locId);
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

    // ───────────────────────────────────────────────────────────────
    //  pos_tax_final  CRUD methods
    // ───────────────────────────────────────────────────────────────

    // Delete rows by a single date (yyyy-mm-dd)
    public function DeletePosTaxFinalByDate($pDate, $pComId, $pLocId)
    {
        $conn = $this->conn;

        $stmt = $conn->prepare(
            "DELETE FROM pos_tax_final
              WHERE DATE(ptf_date) = ?
                AND ptf_comid = ?
                AND ptf_locid = ?"
        );
        if (!$stmt) {
            return array("Success" => false, "Msg" => mysqli_error($conn));
        }

        $stmt->bind_param("sii", $pDate, $pComId, $pLocId);

        if (!$stmt->execute()) {
            $msg = $stmt->error;
            $stmt->close();
            return array("Success" => false, "Msg" => $msg);
        }

        $affected = $stmt->affected_rows;
        $stmt->close();
        return array("Success" => true, "Msg" => "$affected row(s) deleted.", "AffectedRows" => $affected);
    }

    // Delete rows by month (year + month)
    public function DeletePosTaxFinalByMonth($pYear, $pMonth, $pComId, $pLocId)
    {
        $conn = $this->conn;

        $stmt = $conn->prepare(
            "DELETE FROM pos_tax_final
              WHERE YEAR(ptf_date)  = ?
                AND MONTH(ptf_date) = ?
                AND ptf_comid = ?
                AND ptf_locid = ?"
        );
        if (!$stmt) {
            return array("Success" => false, "Msg" => mysqli_error($conn));
        }

        $year  = (int) $pYear;
        $month = (int) $pMonth;
        $comId = (int) $pComId;
        $locId = (int) $pLocId;

        $stmt->bind_param("iiii", $year, $month, $comId, $locId);

        if (!$stmt->execute()) {
            $msg = $stmt->error;
            $stmt->close();
            return array("Success" => false, "Msg" => $msg);
        }

        $affected = $stmt->affected_rows;
        $stmt->close();
        return array("Success" => true, "Msg" => "$affected row(s) deleted.", "AffectedRows" => $affected);
    }

    // Select rows by a single date (yyyy-mm-dd)
    public function SelectPosTaxFinalByDate($pDate, $pComId, $pLocId)
    {
        $dateTimestamp = strtotime($pDate);
        if ($dateTimestamp === false) {
            return array("Success" => false, "Msg" => "Invalid date.", "Data" => array(), "PayMode" => array());
        }

        $dateValue = date('Y-m-d', $dateTimestamp);
        $year = (int) date('Y', $dateTimestamp);
        $month = (int) date('n', $dateTimestamp);

        return $this->SelectPosTaxFinalReport($year, $month, $dateValue, $pComId, $pLocId);
    }

    // Select monthly final summary + paymode breakdown via SP
    public function SelectPosTaxFinalByMonth($pYear, $pMonth, $pComId, $pLocId)
    {
        return $this->SelectPosTaxFinalReport($pYear, $pMonth, null, $pComId, $pLocId);
    }

    private function SelectPosTaxFinalReport($pYear, $pMonth, $pDate, $pComId, $pLocId)
    {
        $conn = $this->conn;

        $year  = (int) $pYear;
        $month = (int) $pMonth;
        $date  = $pDate !== null ? "'" . mysqli_real_escape_string($conn, $pDate) . "'" : "NULL";
        $comId = (int) $pComId;
        $locId = (int) $pLocId;

        $sql = "CALL sp_pos_tax_report_final_and_paymode("
            . $year  . ","
            . $month . ","
            . $date  . ","
            . $comId . ","
            . $locId . ")";

        $result = mysqli_query($conn, $sql);
        if ($result === false) {
            return array("Success" => false, "Msg" => mysqli_error($conn), "Data" => array(), "PayMode" => array());
        }

        // Resultset #1: Monthly final summary
        $finalRows = array();
        if ($result instanceof mysqli_result) {
            while ($row = mysqli_fetch_assoc($result)) {
                $finalRows[] = $row;
            }
            mysqli_free_result($result);
        }

        // Resultset #2: Paymode-wise breakdown
        $payModeRows = array();
        if (mysqli_more_results($conn) && mysqli_next_result($conn)) {
            $result2 = mysqli_store_result($conn);
            if ($result2 instanceof mysqli_result) {
                while ($row = mysqli_fetch_assoc($result2)) {
                    $payModeRows[] = $row;
                }
                mysqli_free_result($result2);
            }
        }

        $this->ClearProcResults();

        return array(
            "Success" => true,
            "Msg"     => "Data loaded.",
            "Data"    => $finalRows,
            "PayMode" => $payModeRows
        );
    }

    // Insert final tax record via stored procedure (delete existing + insert from pos_tax_invoicehdr)
    public function InsertPosTaxFinalSP($pDate, $pComId, $pLocId)
    {
        $conn = $this->conn;

        // First delete any existing record for this date
        $delResult = $this->DeletePosTaxFinalByDate($pDate, $pComId, $pLocId);
        if (!$delResult["Success"]) {
            return $delResult;
        }

        // Call the stored procedure to insert from pos_tax_invoicehdr
        $stmt = $conn->prepare("CALL sp_pos_tax_final_insert(?, ?, ?)");
        if (!$stmt) {
            return array("Success" => false, "Msg" => mysqli_error($conn));
        }

        $stmt->bind_param("sii", $pDate, $pComId, $pLocId);

        if (!$stmt->execute()) {
            $msg = $stmt->error;
            $stmt->close();
            return array("Success" => false, "Msg" => $msg);
        }

        $stmt->close();
        $this->ClearProcResults();

        return array(
            "Success"     => true,
            "Msg"         => "Final record inserted successfully (deleted " . $delResult["AffectedRows"] . " old row(s)).",
            "DeletedRows" => $delResult["AffectedRows"]
        );
    }
}
