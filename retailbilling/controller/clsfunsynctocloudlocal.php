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
    public function connect()
    {
        $this->conn = mysqli_connect(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);
        if (!$this->conn) {
            die("Connection failed: " . mysqli_connect_error());
        }
        mysqli_set_charset($this->conn, "utf8");
        return $this->conn;
    }
    public function GetPosMaster($pm_id, $comid, $locid)
    {
        // Use a more generic approach to handle different column naming patterns
        $possibleQueries = array(
            "SELECT * FROM pos_master WHERE psm_id = ? AND psm_comid = ? AND psm_locid = ?",
            "SELECT * FROM pos_master WHERE pm_id = ? AND comid = ? AND locid = ?",
            "SELECT * FROM pos_master WHERE pmid = ? AND comid = ? AND locid = ?",
            "SELECT * FROM pos_master WHERE id = ? AND comid = ? AND locid = ?"
        );

        foreach ($possibleQueries as $sql) {
            $stmt = mysqli_prepare($this->conn, $sql);
            if ($stmt) {
                mysqli_stmt_bind_param($stmt, "sii", $pm_id, $comid, $locid);
                if (mysqli_stmt_execute($stmt)) {
                    $result = mysqli_stmt_get_result($stmt);
                    mysqli_stmt_close($stmt);
                    return $result;
                }
                mysqli_stmt_close($stmt);
            }
        }

        throw new Exception("Could not find matching column structure for pos_master table");
    }

    public function GetSaleInvoiceHdr($pm_id, $trno, $comid, $locid)
    {
        $sql = "SELECT COUNT(*) FROM pos_sale_invoicehdr WHERE psih_invoice_pmid = ? AND psih_invoice_trno = ? AND psih_invoice_comid = ? AND psih_invoice_locid = ?";
        $stmt = mysqli_prepare($this->conn, $sql);
        if (!$stmt) {
            throw new Exception("Prepare failed: " . mysqli_error($this->conn));
        }
        mysqli_stmt_bind_param($stmt, "ssii", $pm_id, $trno, $comid, $locid);
        mysqli_stmt_execute($stmt);
        $result = mysqli_stmt_get_result($stmt);
        mysqli_stmt_close($stmt);
        return $result;
    }

    public function GetSaleInvoiceDtl($pm_id, $trno, $comid, $locid)
    {
        $sql = "SELECT COUNT(*) FROM pos_sale_invoicedtl WHERE psid_invoice_pmid = ? AND psid_invoice_trno = ? AND psid_invoice_comid = ? AND psid_invoice_locid = ?";
        $stmt = mysqli_prepare($this->conn, $sql);
        if (!$stmt) {
            throw new Exception("Prepare failed: " . mysqli_error($this->conn));
        }
        mysqli_stmt_bind_param($stmt, "ssii", $pm_id, $trno, $comid, $locid);
        mysqli_stmt_execute($stmt);
        $result = mysqli_stmt_get_result($stmt);
        mysqli_stmt_close($stmt);
        return $result;
    }

    public function SaveInvoiceData($data)
    {
        try {
            // Start transaction
            mysqli_autocommit($this->conn, false);

            // First, delete existing records if they exist
            foreach ($data['invoice_hdr'] as $hdr) {
                // Delete existing invoice details first (foreign key dependency)
                $deleteDtlSql = "DELETE FROM pos_sale_invoicedtl
                               WHERE psid_invoice_pmid = '" . mysqli_real_escape_string($this->conn, isset($hdr['psih_invoice_pmid']) ? $hdr['psih_invoice_pmid'] : '0') . "'
                               AND psid_invoice_trno = '" . mysqli_real_escape_string($this->conn, isset($hdr['psih_invoice_trno']) ? $hdr['psih_invoice_trno'] : '') . "'
                               AND psid_invoice_comid = '" . mysqli_real_escape_string($this->conn, isset($hdr['psih_invoice_comid']) ? $hdr['psih_invoice_comid'] : '0') . "'
                               AND psid_invoice_locid = '" . mysqli_real_escape_string($this->conn, isset($hdr['psih_invoice_locid']) ? $hdr['psih_invoice_locid'] : '0') . "'";

                mysqli_query($this->conn, $deleteDtlSql);

                // Delete existing invoice header
                $deleteHdrSql = "DELETE FROM pos_sale_invoicehdr
                               WHERE psih_invoice_pmid = '" . mysqli_real_escape_string($this->conn, isset($hdr['psih_invoice_pmid']) ? $hdr['psih_invoice_pmid'] : '0') . "'
                               AND psih_invoice_trno = '" . mysqli_real_escape_string($this->conn, isset($hdr['psih_invoice_trno']) ? $hdr['psih_invoice_trno'] : '') . "'
                               AND psih_invoice_comid = '" . mysqli_real_escape_string($this->conn, isset($hdr['psih_invoice_comid']) ? $hdr['psih_invoice_comid'] : '0') . "'
                               AND psih_invoice_locid = '" . mysqli_real_escape_string($this->conn, isset($hdr['psih_invoice_locid']) ? $hdr['psih_invoice_locid'] : '0') . "'";

                mysqli_query($this->conn, $deleteHdrSql);
            }

            // Insert Invoice Header
            foreach ($data['invoice_hdr'] as $hdr) {
                $sql = "INSERT INTO pos_sale_invoicehdr (
                        psih_invoice_refid, psih_invoice_trno, psih_invoice_date,
                        psih_invoice_prefix, psih_invoice_tqty, psih_invoice_tamount,
                        psih_invoice_titemdisper, psih_invoice_titemdisamt,
                        psih_invoice_tbilldiscper, psih_invoice_tbilldiscamt,
                        psih_invoice_totdiscper, psih_invoice_totdiscamt,
                        psih_invoice_tgrossamt, psih_invoice_ttaxamt,
                        psih_invoice_sercharge, psih_invoice_roundoff,
                        psih_invoice_tnetamt, psih_invoice_saletype,
                        psih_invoice_billtype, psih_invoice_billstatus,
                        psih_invoice_paymode, psih_invoice_customerid,
                        psih_invoice_description, psih_invoice_countername,
                        psih_invoice_userid, psih_invoice_comid,
                        psih_invoice_locid, psih_invoice_pmid,
                        psih_invoice_print, psih_invoice_billremarks,
                        psih_invoice_advamt, psih_invoice_outstanding,
                        psih_invoice_givenamt, psih_invoice_balamt,
                        psih_invoice_shiftno, psih_invoice_dayno,
                        psih_invoice_created, psih_invoice_modified

                    )
                    VALUES (
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_id']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_trno']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_date']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_prefix']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_tqty']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_tamount']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_titemdisper']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_titemdisamt']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_tbilldiscper']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_tbilldiscamt']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_totdiscper']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_totdiscamt']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_tgrossamt']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_ttaxamt']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_sercharge']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_roundoff']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_tnetamt']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_saletype']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_billtype']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_billstatus']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_paymode']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_customerid']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_description']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_countername']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_userid']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_comid']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_locid']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_pmid']) . "',
                        '" . mysqli_real_escape_string($this->conn, isset($hdr['psih_invoice_print']) ? $hdr['psih_invoice_print'] : '0') . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_billremarks']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_advamt']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_outstanding']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_givenamt']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_balamt']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_shiftno']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_dayno']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_created']) . "',
                        '" . mysqli_real_escape_string($this->conn, $hdr['psih_invoice_modified']) . "'

                    )";
                $result = mysqli_query($this->conn, $sql);
                if (!$result) {
                    throw new Exception("Error inserting invoice header: " . mysqli_error($this->conn));
                }
            }

            // Insert Invoice Details
            foreach ($data['invoice_dtl'] as $dtl) {
                $sql = "INSERT INTO pos_sale_invoicedtl (
                        psid_invoice_refid, psid_invoice_salid, psid_invoice_sno,
                        psid_invoice_date, psid_invoice_trno, psid_invoice_barcode,
                        psid_invoice_procode, psid_invoice_description,
                        psid_invoice_serialno, psid_invoice_uom, psid_invoice_proqty,
                        psid_invoice_rate, psid_invoice_amt, psid_invoice_itemdisp,
                        psid_invoice_itemdisamt, psid_invoice_billdisp,
                        psid_invoice_billdisamt, psid_invoice_totdper,
                        psid_invoice_totdamt, psid_invoice_gross, psid_invoice_taxinex,
                        psid_invoice_taxvalue, psid_invoice_taxamt, psid_invoice_netamt,
                        psid_invoice_remarks, psid_invoice_batchno,
                        psid_invoice_salesmanid, psid_invoice_salemanper,
                        psid_invoice_shiftno, psid_invoice_dayno,
                        psid_invoice_created, psid_invoice_modified,
                        psid_invoice_comid, psid_invoice_locid, psid_invoice_pmid

                    )
                    VALUES (
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_id']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_salid']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_sno']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_date']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_trno']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_barcode']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_procode']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_description']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_serialno']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_uom']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_proqty']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_rate']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_amt']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_itemdisp']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_itemdisamt']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_billdisp']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_billdisamt']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_totdper']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_totdamt']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_gross']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_taxinex']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_taxvalue']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_taxamt']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_netamt']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_remarks']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_batchno']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_salesmanid']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_salemanper']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_shiftno']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_dayno']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_created']) . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_modified']) . "',
                        '" . mysqli_real_escape_string($this->conn, isset($dtl['psid_invoice_comid']) ? $dtl['psid_invoice_comid'] : '0') . "',
                        '" . mysqli_real_escape_string($this->conn, isset($dtl['psid_invoice_locid']) ? $dtl['psid_invoice_locid'] : '0') . "',
                        '" . mysqli_real_escape_string($this->conn, $dtl['psid_invoice_pmid']) . "'

                    )";

                $result = mysqli_query($this->conn, $sql);
                if (!$result) {
                    throw new Exception("Error inserting invoice detail: " . mysqli_error($this->conn));
                }
            }

            // Commit transaction
            mysqli_commit($this->conn);
            mysqli_autocommit($this->conn, true);

            return array('success' => true, 'message' => 'Invoice data saved successfully');
        } catch (Exception $e) {
            // Rollback transaction on error
            mysqli_rollback($this->conn);
            mysqli_autocommit($this->conn, true);

            return array('success' => false, 'message' => 'Error saving invoice data: ' . $e->getMessage());
        }
    }

    public function cleanJsonData($jsonString)
    {
        if (empty($jsonString)) {
            return $jsonString;
        }

        // Remove control characters (0x00-0x1F and 0x7F) except allowed ones
        // Keep tab (0x09), line feed (0x0A), and carriage return (0x0D) for now, will replace later
        $cleaned = preg_replace('/[\x00-\x08\x0B\x0C\x0E-\x1F\x7F]/', '', $jsonString);

        // Replace newlines, carriage returns, and tabs with spaces in string values only
        // This regex targets content within quotes to avoid breaking JSON structure
        $cleaned = preg_replace_callback('/"([^"\\\\]*(\\\\.[^"\\\\]*)*)"/', function ($matches) {
            $content = $matches[1];
            // Replace control characters within the string content
            $content = str_replace(["\r", "\n", "\t", "\r\n"], [' ', ' ', ' ', ' '], $content);
            // Remove extra spaces
            $content = preg_replace('/\s+/', ' ', $content);
            $content = trim($content);
            return '"' . $content . '"';
        }, $cleaned);

        // Final cleanup - remove any remaining problematic characters
        $cleaned = str_replace(["\r", "\n", "\t"], [' ', ' ', ' '], $cleaned);

        // Remove extra spaces outside of quoted strings
        $cleaned = preg_replace('/\s+(?=(?:[^"]*"[^"]*")*[^"]*$)/', ' ', $cleaned);

        return trim($cleaned);
    }
    public function GetSalesReport($comid, $locid, $startDate, $endDate)
    {
        // Escape variables first
        $comid     = mysqli_real_escape_string($this->conn, $comid);
        $locid     = mysqli_real_escape_string($this->conn, $locid);
        $startDate = mysqli_real_escape_string($this->conn, $startDate);
        $endDate   = mysqli_real_escape_string($this->conn, $endDate);

        // Add time component to date range for proper filtering
        $startDateTime = $startDate . ' 00:00:00';
        $endDateTime = $endDate . ' 23:59:59';

        // Build query with proper date range
        $sql = "
        SELECT *
        FROM pos_sale_invoicehdr
        WHERE psih_invoice_comid = '$comid'
          AND psih_invoice_locid = '$locid'
          AND psih_invoice_created >= '$startDateTime'
          AND psih_invoice_created <= '$endDateTime'
        ORDER BY psih_invoice_created DESC
         ";

        // Log the query for debugging (remove in production)
        error_log("Sales Report Query: " . $sql);

        $result = mysqli_query($this->conn, $sql);
        if (!$result) {
            throw new Exception("Error fetching sales report: " . mysqli_error($this->conn));
        }
        return $result;
    }
    public function GetSalesReportDetails($comid, $locid, $startDate, $endDate)
    {
        // Escape variables first
        $comid     = mysqli_real_escape_string($this->conn, $comid);
        $locid     = mysqli_real_escape_string($this->conn, $locid);
        $startDate = mysqli_real_escape_string($this->conn, $startDate);
        $endDate   = mysqli_real_escape_string($this->conn, $endDate);

        // Add time component to date range for proper filtering
        $startDateTime = $startDate . ' 00:00:00';
        $endDateTime = $endDate . ' 23:59:59';

        // Build query with proper date range
        $sql = "
        SELECT *
        FROM pos_sale_invoicedtl
        WHERE psid_invoice_comid = '$comid'
          AND psid_invoice_locid = '$locid'
          AND psid_invoice_created >= '$startDateTime'
          AND psid_invoice_created <= '$endDateTime'
        ORDER BY psid_invoice_created DESC
         ";

        // Log the query for debugging (remove in production)
        error_log("Sales Report Details Query: " . $sql);

        $result = mysqli_query($this->conn, $sql);
        if (!$result) {
            throw new Exception("Error fetching sales report details: " . mysqli_error($this->conn));
        }
        return $result;
    }
    public function GetSalesManReport($comid, $locid, $salesmanId, $startDate, $endDate)
    {
        // Escape variables first
        $comid     = mysqli_real_escape_string($this->conn, $comid);
        $locid     = mysqli_real_escape_string($this->conn, $locid);
        $salesmanId = mysqli_real_escape_string($this->conn, $salesmanId);
        $startDate = mysqli_real_escape_string($this->conn, $startDate);
        $endDate   = mysqli_real_escape_string($this->conn, $endDate);

        // Add time component to date range for proper filtering
        $startDateTime = $startDate . ' 00:00:00';
        $endDateTime = $endDate . ' 23:59:59';

        // Build detailed commission report query
        $sql = "
        SELECT
            pe.emp_id AS ID,
            pe.emp_printname AS Name,
            psid.psid_invoice_description AS ItemName,
            CAST(psid.psid_invoice_netamt AS DECIMAL(18,2)) AS NetAmt,
            psid.psid_invoice_salemanper AS Percentage,
            CAST((psid.psid_invoice_netamt * psid.psid_invoice_salemanper) / 100 AS DECIMAL(18,2)) AS Commission,
            psid.psid_invoice_date AS Date,
            psid.psid_invoice_trno AS TransactionNo
        FROM pos_sale_invoicedtl AS psid
        INNER JOIN pos_employeeinfo AS pe
            ON psid.psid_invoice_salesmanid = pe.emp_id
        WHERE psid.psid_invoice_date BETWEEN '$startDate' AND '$endDate'
          AND psid.psid_invoice_comid = '$comid'
          AND psid.psid_invoice_locid = '$locid'";

        // Add salesman filter if not 'ALL' or '0'
        if (!empty($salesmanId) && $salesmanId != '0' && strtoupper($salesmanId) != 'ALL') {
            $sql .= " AND psid.psid_invoice_salesmanid = '$salesmanId'";
        }

        $sql .= " ORDER BY pe.emp_printname, psid.psid_invoice_date DESC";

        // Log the query for debugging (remove in production)
        error_log("Salesman Report Query: " . $sql);

        $result = mysqli_query($this->conn, $sql);
        if (!$result) {
            throw new Exception("Error fetching salesman report: " . mysqli_error($this->conn));
        }
        return $result;
    }

    public function GetSalesManReportSummary($comid, $locid, $salesmanId, $startDate, $endDate)
    {
        // Escape variables first
        $comid     = mysqli_real_escape_string($this->conn, $comid);
        $locid     = mysqli_real_escape_string($this->conn, $locid);
        $salesmanId = mysqli_real_escape_string($this->conn, $salesmanId);
        $startDate = mysqli_real_escape_string($this->conn, $startDate);
        $endDate   = mysqli_real_escape_string($this->conn, $endDate);

        // Build summary commission report query (grouped by salesman)
        $sql = "
        SELECT
            pe.emp_id AS ID,
            pe.emp_printname AS Name,
            COUNT(psid.psid_invoice_netamt) AS TotalItems,
            CAST(SUM(psid.psid_invoice_netamt) AS DECIMAL(18,2)) AS TotalNetAmt,
            CAST(AVG(psid.psid_invoice_salemanper) AS DECIMAL(5,2)) AS AvgPercentage,
            CAST(SUM((psid.psid_invoice_netamt * psid.psid_invoice_salemanper) / 100) AS DECIMAL(18,2)) AS TotalCommission
        FROM pos_sale_invoicedtl AS psid
        INNER JOIN pos_employeeinfo AS pe
            ON psid.psid_invoice_salesmanid = pe.emp_id
        WHERE psid.psid_invoice_date BETWEEN '$startDate' AND '$endDate'
          AND psid.psid_invoice_comid = '$comid'
          AND psid.psid_invoice_locid = '$locid'";

        // Add salesman filter if not 'ALL' or '0'
        if (!empty($salesmanId) && $salesmanId != '0' && strtoupper($salesmanId) != 'ALL') {
            $sql .= " AND psid.psid_invoice_salesmanid = '$salesmanId'";
        }

        $sql .= " GROUP BY pe.emp_id, pe.emp_printname ORDER BY TotalCommission DESC";

        // Log the query for debugging (remove in production)
        error_log("Salesman Report Summary Query: " . $sql);

        $result = mysqli_query($this->conn, $sql);
        if (!$result) {
            throw new Exception("Error fetching salesman report summary: " . mysqli_error($this->conn));
        }
        return $result;
    }

    public function GetSalesManReportAll($comid, $locid)
    {
        // Escape variables first
        $comid     = mysqli_real_escape_string($this->conn, $comid);
        $locid     = mysqli_real_escape_string($this->conn, $locid);

        // Build query to get all salesman commission data (no date filter)
        $sql = "
        SELECT
            pe.emp_id AS ID,
            pe.emp_printname AS Name,
            psid.psid_invoice_description AS ItemName,
            CAST(psid.psid_invoice_netamt AS DECIMAL(18,2)) AS NetAmt,
            psid.psid_invoice_salemanper AS Percentage,
            CAST((psid.psid_invoice_netamt * psid.psid_invoice_salemanper) / 100 AS DECIMAL(18,2)) AS Commission,
            psid.psid_invoice_date AS Date,
            psid.psid_invoice_trno AS TransactionNo
        FROM pos_sale_invoicedtl AS psid
        INNER JOIN pos_employeeinfo AS pe
            ON psid.psid_invoice_salesmanid = pe.emp_id
        WHERE psid.psid_invoice_comid = '$comid'
          AND psid.psid_invoice_locid = '$locid'
        ORDER BY pe.emp_printname, psid.psid_invoice_date DESC";

        // Log the query for debugging (remove in production)
        error_log("Salesman Report All Query: " . $sql);

        $result = mysqli_query($this->conn, $sql);
        if (!$result) {
            throw new Exception("Error fetching all salesman report: " . mysqli_error($this->conn));
        }
        return $result;
    }


    // /**
    //  * Process large datasets in batches to avoid memory and timeout issues
    //  */
    // public function SaveInvoiceDataBatch($data, $batchSize = 50)
    // {
    //     try {
    //         $results = array('success' => true, 'message' => '', 'processed' => 0, 'errors' => array());

    //         // Process headers in batches
    //         if (isset($data['invoice_hdr']) && count($data['invoice_hdr']) > $batchSize) {
    //             $hdrBatches = array_chunk($data['invoice_hdr'], $batchSize);
    //             foreach ($hdrBatches as $batch) {
    //                 $batchData = array('invoice_hdr' => $batch, 'invoice_dtl' => array());
    //                 $result = $this->SaveInvoiceData($batchData);
    //                 if (!$result['success']) {
    //                     $results['errors'][] = 'Header batch error: ' . $result['message'];
    //                 }
    //                 $results['processed'] += count($batch);
    //             }
    //         }

    //         // Process details in batches
    //         if (isset($data['invoice_dtl']) && count($data['invoice_dtl']) > $batchSize) {
    //             $dtlBatches = array_chunk($data['invoice_dtl'], $batchSize);
    //             foreach ($dtlBatches as $batch) {
    //                 $batchData = array('invoice_hdr' => array(), 'invoice_dtl' => $batch);
    //                 $result = $this->SaveInvoiceData($batchData);
    //                 if (!$result['success']) {
    //                     $results['errors'][] = 'Detail batch error: ' . $result['message'];
    //                 }
    //                 $results['processed'] += count($batch);
    //             }
    //         } else {
    //             // Process normally if not too large
    //             return $this->SaveInvoiceData($data);
    //         }

    //         if (!empty($results['errors'])) {
    //             $results['success'] = false;
    //             $results['message'] = 'Batch processing completed with errors: ' . implode(', ', $results['errors']);
    //         } else {
    //             $results['message'] = 'Batch processing completed successfully. Processed ' . $results['processed'] . ' records.';
    //         }

    //         return $results;
    //     } catch (Exception $e) {
    //         return array('success' => false, 'message' => 'Batch processing error: ' . $e->getMessage());
    //     }
    // }
}
