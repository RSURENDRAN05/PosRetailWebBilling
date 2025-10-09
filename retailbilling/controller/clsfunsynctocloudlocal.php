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

    public function GetPMId($comid, $locid)
    {
        $sql = "SELECT PM_ID FROM POS_MASTER WHERE PM_COMID = ? AND PM_LOCID = ?";
        $stmt = mysqli_prepare($this->conn, $sql);

        if (!$stmt) {
            throw new Exception("Prepare failed: " . mysqli_error($this->conn));
        }

        mysqli_stmt_bind_param($stmt, "ii", $comid, $locid);
        mysqli_stmt_execute($stmt);
        $result = mysqli_stmt_get_result($stmt);

        $pm_id = null;
        if ($row = mysqli_fetch_assoc($result)) {
            $pm_id = $row['PM_ID'];
        }

        mysqli_stmt_close($stmt);
        return $pm_id; // Returns PM_ID value or null if not found
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
            $pm_id = isset($data['invoice_hdr'][0]['psih_invoice_pmid']) ? $data['invoice_hdr'][0]['psih_invoice_pmid'] : '0';
            $trno = isset($data['invoice_hdr'][0]['psih_invoice_trno']) ? $data['invoice_hdr'][0]['psih_invoice_trno'] : '0';
            $comid = isset($data['invoice_hdr'][0]['psih_invoice_comid']) ? $data['invoice_hdr'][0]['psih_invoice_comid'] : '0';
            $locid = isset($data['invoice_hdr'][0]['psih_invoice_locid']) ? $data['invoice_hdr'][0]['psih_invoice_locid'] : '0';

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
                // Delete existing salpaymode
                $deletePaymodeSql = "DELETE FROM pos_sale_paymode
                                   WHERE PmId = '" . mysqli_real_escape_string($this->conn, isset($hdr['psih_invoice_pmid']) ? $hdr['psih_invoice_pmid'] : '0') . "'
                                   AND Sal_ID = '" . mysqli_real_escape_string($this->conn, isset($hdr['psih_invoice_trno']) ? $hdr['psih_invoice_trno'] : '') . "'
                                   AND ComId = '" . mysqli_real_escape_string($this->conn, isset($hdr['psih_invoice_comid']) ? $hdr['psih_invoice_comid'] : '0') . "'
                                   AND LocId = '" . mysqli_real_escape_string($this->conn, isset($hdr['psih_invoice_locid']) ? $hdr['psih_invoice_locid'] : '0') . "'";

                mysqli_query($this->conn, $deletePaymodeSql);
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
            // Insert Payment Modes
            foreach ($data['payment_mode'] as $paymode) {
                $sql = "INSERT INTO pos_sale_paymode (Sal_ID, Paymode, Amount, ShiftNo, Dayno, Created, PmId, ComId, LocId)
                        VALUES (
                            '" . mysqli_real_escape_string($this->conn, $trno) . "',
                            '" . mysqli_real_escape_string($this->conn, $paymode['Paymode']) . "',
                            '" . mysqli_real_escape_string($this->conn, $paymode['Amount']) . "',
                            '" . mysqli_real_escape_string($this->conn, $paymode['ShiftNo']) . "',
                            '" . mysqli_real_escape_string($this->conn, $paymode['Dayno']) . "',
                            '" . mysqli_real_escape_string($this->conn, $paymode['Created']) . "',
                            '" . mysqli_real_escape_string($this->conn, $pm_id) . "',
                            '" . mysqli_real_escape_string($this->conn, $comid) . "',
                            '" . mysqli_real_escape_string($this->conn, $locid) . "'
                        )";

                $result = mysqli_query($this->conn, $sql);
                if (!$result) {
                    throw new Exception("Error inserting payment mode: " . mysqli_error($this->conn));
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
    public function GetSalesReportSummary($comid, $locid, $startDate, $endDate)
    {
        // Escape variables first
        $comid     = mysqli_real_escape_string($this->conn, $comid);
        $locid     = mysqli_real_escape_string($this->conn, $locid);
        $startDate = mysqli_real_escape_string($this->conn, $startDate);
        $endDate   = mysqli_real_escape_string($this->conn, $endDate);

        // Add time component to date range for proper filtering
        // $startDateTime = $startDate . ' 00:00:00';
        // $endDateTime = $endDate . ' 23:59:59';

        // Build query with proper date range
        $sql = "
        SELECT `psih_invoice_id`, `psih_invoice_refid`, `psih_invoice_trno`, `psih_invoice_date`, `psih_invoice_prefix`, `psih_invoice_tqty`, `psih_invoice_tamount`, `psih_invoice_titemdisper`, `psih_invoice_titemdisamt`, `psih_invoice_tbilldiscper`, `psih_invoice_tbilldiscamt`, `psih_invoice_totdiscper`, `psih_invoice_totdiscamt`, `psih_invoice_tgrossamt`, `psih_invoice_ttaxamt`, `psih_invoice_sercharge`, `psih_invoice_roundoff`, `psih_invoice_tnetamt`, `psih_invoice_saletype`, `psih_invoice_billtype`, `psih_invoice_billstatus`, `psih_invoice_paymode`, `psih_invoice_customerid`, `psih_invoice_description`, `psih_invoice_countername`, `psih_invoice_userid`, `psih_invoice_comid`, `psih_invoice_locid`, `psih_invoice_pmid`, `psih_invoice_print`, `psih_invoice_billremarks`, `psih_invoice_advamt`, `psih_invoice_outstanding`, `psih_invoice_givenamt`, `psih_invoice_balamt`, `psih_invoice_shiftno`, `psih_invoice_dayno`, `psih_invoice_created`, `psih_invoice_modified`,pm.pcm_name,pl.plm_name FROM `pos_sale_invoicehdr` as ph  INNER JOIN pos_company_mast AS pm ON pm.pcm_id = ph.psih_invoice_comid
        INNER JOIN pos_location_mast AS pl ON pl.plm_id = ph.psih_invoice_locid
        WHERE psih_invoice_comid = '$comid'
          AND psih_invoice_locid = '$locid'
          AND psih_invoice_date >= '$startDate'
          AND psih_invoice_date <= '$endDate'
        ORDER BY psih_invoice_date DESC
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

        // Build query with proper date range and JOINs for detailed information
        $sql = "
        SELECT
            psid.psid_invoice_date,
            psid.psid_invoice_trno,
            psid.psid_invoice_barcode,
            psid.psid_invoice_procode,
            psid.psid_invoice_description,
            psid.psid_invoice_proqty,
            psid.psid_invoice_rate,
            psid.psid_invoice_amt,
            psid.psid_invoice_totdper,
            psid.psid_invoice_totdamt,
            psid.psid_invoice_gross,
            psid.psid_invoice_taxamt,
            psid.psid_invoice_netamt,
            psid.psid_invoice_salesmanid,
            CONCAT(CAST(ROUND(psid_invoice_salemanper, 0) AS UNSIGNED), '%') AS psid_invoice_salemanper,
            psid.psid_invoice_shiftno,
            psid.psid_invoice_dayno,
            psid.psid_invoice_created,
            psid.psid_invoice_modified,
            psid.psid_invoice_comid,
            psid.psid_invoice_locid,
            psid.psid_invoice_pmid,
            pe.emp_printname,
            pm.pcm_name,
            pl.plm_name,
            CAST((psid.psid_invoice_netamt * psid.psid_invoice_salemanper) / 100 AS DECIMAL(18,2)) AS Commission
        FROM pos_sale_invoicedtl AS psid
        INNER JOIN pos_employeeinfo AS pe ON psid.psid_invoice_salesmanid = pe.emp_id
        INNER JOIN pos_company_mast AS pm ON pm.pcm_id = psid.psid_invoice_comid
        INNER JOIN pos_location_mast AS pl ON pl.plm_id = psid.psid_invoice_locid
        WHERE psid.psid_invoice_comid = '$comid'
          AND psid.psid_invoice_locid = '$locid'
          AND psid.psid_invoice_created >= '$startDate'
          AND psid.psid_invoice_created <= '$endDate'
        ORDER BY psid.psid_invoice_date DESC
         ";

        // Log the query for debugging (remove in production)
        error_log("Sales Report Details Query: " . $sql);

        $result = mysqli_query($this->conn, $sql);
        if (!$result) {
            throw new Exception("Error fetching sales report details: " . mysqli_error($this->conn));
        }
        return $result;
    }
    public function GetSalesManDetailReport($comid, $locid, $salesmanId, $startDate, $endDate)
    {
        // Escape variables first
        $comid     = mysqli_real_escape_string($this->conn, $comid);
        $locid     = mysqli_real_escape_string($this->conn, $locid);
        $salesmanId = mysqli_real_escape_string($this->conn, $salesmanId);
        $startDate = mysqli_real_escape_string($this->conn, $startDate);
        $endDate   = mysqli_real_escape_string($this->conn, $endDate);

        // Build detailed commission report query
        $sql = "
        SELECT
            pe.emp_id AS ID,
            pe.emp_printname AS Name,
            psid.psid_invoice_description AS ItemName,
            SUM(CAST(psid.psid_invoice_netamt AS DECIMAL(18,2))) AS NetAmt,
            psid.psid_invoice_salemanper AS Percentage,
            SUM(CAST((psid.psid_invoice_netamt * psid.psid_invoice_salemanper) / 100 AS DECIMAL(18,2))) AS Commission,
            psid.psid_invoice_date AS Date,
            psid.psid_invoice_trno AS TransactionNo,
            pm.pcm_name AS CompanyName,
            pl.plm_name AS LocationName,
            SUM(CAST(psid.psid_invoice_proqty AS DECIMAL(18,2))) AS Qty
        FROM pos_sale_invoicedtl AS psid
        INNER JOIN pos_employeeinfo AS pe ON psid.psid_invoice_salesmanid = pe.emp_id
        INNER JOIN pos_company_mast AS pm ON pm.pcm_id = psid.psid_invoice_comid
        INNER JOIN pos_location_mast AS pl ON pl.plm_id = psid.psid_invoice_locid
        WHERE psid.psid_invoice_date >= '$startDate' AND psid.psid_invoice_date <=  '$endDate'";

        // Add salesman filter if not 'ALL' or '0'
        if (!empty($salesmanId) && $salesmanId != '0' && strtoupper($salesmanId) != 'ALL') {
            $sql .= " AND psid.psid_invoice_salesmanid = '$salesmanId'";
        }
        if (!empty($comid) && $comid != '0' && strtoupper($comid) != 'ALL') {
            $sql .= " AND psid.psid_invoice_comid = '$comid'";
        }
        if (!empty($locid) && $locid != '0' && strtoupper($locid) != 'ALL') {
            $sql .= " AND psid.psid_invoice_locid = '$locid'";
        }
        $sql .= " GROUP BY pe.emp_id, pe.emp_printname, psid.psid_invoice_description, psid.psid_invoice_salemanper, psid.psid_invoice_date, psid.psid_invoice_trno, pm.pcm_name, pl.plm_name";
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
            CAST(SUM((psid.psid_invoice_netamt * psid.psid_invoice_salemanper) / 100) AS DECIMAL(18,2)) AS TotalCommission,
            pm.pcm_name AS CompanyName,
            pl.plm_name AS LocationName,
            CAST(SUM(psid.psid_invoice_proqty) AS DECIMAL(18,2)) AS Qty
        FROM pos_sale_invoicedtl AS psid
        INNER JOIN pos_employeeinfo AS pe ON psid.psid_invoice_salesmanid = pe.emp_id
        INNER JOIN pos_company_mast AS pm ON pm.pcm_id = psid.psid_invoice_comid
        INNER JOIN pos_location_mast AS pl ON pl.plm_id = psid.psid_invoice_locid
        WHERE psid.psid_invoice_date BETWEEN '$startDate' AND '$endDate'";

        // Add salesman filter if not 'ALL' or '0'
        if (!empty($salesmanId) && $salesmanId != '0' && strtoupper($salesmanId) != 'ALL') {
            $sql .= " AND psid.psid_invoice_salesmanid = '$salesmanId'";
        }
        if (!empty($comid) && $comid != '0' && strtoupper($comid) != 'ALL') {
            $sql .= " AND psid.psid_invoice_comid = '$comid'";
        }
        if (!empty($locid) && $locid != '0' && strtoupper($locid) != 'ALL') {
            $sql .= " AND psid.psid_invoice_locid = '$locid'";
        }
        $sql .= " GROUP BY pe.emp_id, pe.emp_printname,pm.pcm_name, pl.plm_name ORDER BY TotalCommission DESC";

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
            psid.psid_invoice_trno AS TransactionNo,
            CAST(SUM(psid.psid_invoice_proqty) AS DECIMAL(18,2)) AS Qty
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
    public function SavePayoutData($data, $comid, $locid, $pm_id)
    {
        try {
            // Start transaction
            mysqli_autocommit($this->conn, false);
            // Get the actual payout data array
            $payoutData = isset($data['payout_dtl']) ? $data['payout_dtl'] : $data;

            // Insert new payout records
            foreach ($payoutData as $payout) {
                // Check delete status for each individual record
                $deleteState = isset($payout['payd_deletestatus']) ? $payout['payd_deletestatus'] : 'I';
                //if exists previous record
                // $deleteitem = "DELETE FROM pos_payout_dtl WHERE payd_refid = '" . mysqli_real_escape_string($this->conn, $payout['payd_id']) . "' AND ComId = '" . mysqli_real_escape_string($this->conn, $comid) . "' AND LocId = '" . mysqli_real_escape_string($this->conn, $locid) . "' AND PmId = '" . mysqli_real_escape_string($this->conn, $pm_id) . "'";
                // mysqli_query($this->conn, $deleteitem);
                // if (mysqli_affected_rows($this->conn) > 0) {
                //     // Record was deleted
                //     error_log("Deleted payout record: " . $payout['payd_id']);
                // }

                if ($deleteState == 'D') {
                    $payd_id = isset($payout['payd_id']) ? $payout['payd_id'] : '';
                    if (!empty($payd_id)) {
                        $deleteResult = $this->DeletePayoutRecord($payd_id, $comid, $locid);
                        if (!$deleteResult['success']) {
                            throw new Exception("Failed to delete payout record with ID: $payd_id. " . $deleteResult['message']);
                        }
                    }
                } else {
                    // Insert the record
                    // Handle payd_id - use the actual value from JSON
                    $payd_refid = isset($payout['payd_id']) && !empty($payout['payd_id']) ?
                        "'" . mysqli_real_escape_string($this->conn, $payout['payd_id']) . "'" :
                        'NULL';

                    $sql = "INSERT INTO pos_payout_dtl (
                        payd_refid, payd_ledgerid, payd_name,
                        payd_amount, payd_remarks, payd_shiftno,
                        payd_dayno, payd_user, payd_datetime,
                        PmId, ComId, LocId
                    )
                    VALUES (
                        " . $payd_refid . ",
                        '" . mysqli_real_escape_string($this->conn, isset($payout['payd_ledgerid']) ? $payout['payd_ledgerid'] : '0') . "',
                        '" . mysqli_real_escape_string($this->conn, isset($payout['payd_name']) ? $payout['payd_name'] : '') . "',
                        '" . mysqli_real_escape_string($this->conn, isset($payout['payd_amount']) ? $payout['payd_amount'] : '0') . "',
                        '" . mysqli_real_escape_string($this->conn, isset($payout['payd_remarks']) ? $payout['payd_remarks'] : '') . "',
                        '" . mysqli_real_escape_string($this->conn, isset($payout['payd_shiftno']) ? $payout['payd_shiftno'] : '0') . "',
                        '" . mysqli_real_escape_string($this->conn, isset($payout['payd_dayno']) ? $payout['payd_dayno'] : '0') . "',
                        '" . mysqli_real_escape_string($this->conn, isset($payout['payd_user']) ? $payout['payd_user'] : '0') . "',
                        NOW(),
                        '" . mysqli_real_escape_string($this->conn, $pm_id) . "',
                        '" . mysqli_real_escape_string($this->conn, $comid) . "',
                        '" . mysqli_real_escape_string($this->conn, $locid) . "'
                    )";

                    $result = mysqli_query($this->conn, $sql);
                    if (!$result) {
                        throw new Exception("Error inserting payout record: " . mysqli_error($this->conn) . " SQL: " . $sql);
                    }
                }
            }

            // Commit transaction
            mysqli_commit($this->conn);
            mysqli_autocommit($this->conn, true);
            return array('success' => true, 'message' => 'Payout data saved successfully');
        } catch (Exception $e) {
            // Rollback transaction on error
            mysqli_rollback($this->conn);
            mysqli_autocommit($this->conn, true);
            return array('success' => false, 'message' => 'Error saving payout data: ' . $e->getMessage());
        }
    }

    public function SavePayoutDataToCloud($data, $comid, $locid, $pm_id)
    {
        try {
            // Start transaction
            mysqli_autocommit($this->conn, false);

            // Get the actual payout data array
            $payoutData = isset($data['payout_dtl']) ? $data['payout_dtl'] : $data;

            // Debug: Log the structure of payoutData
            error_log("SavePayoutDataToCloud - Data structure: " . print_r($payoutData, true));
            error_log("SavePayoutDataToCloud - Data count: " . (is_array($payoutData) ? count($payoutData) : 'Not an array'));

            // Ensure we have an array to work with
            if (!is_array($payoutData)) {
                throw new Exception("Payout data is not an array");
            }

            // Insert new payout records
            foreach ($payoutData as $index => $payout) {
                error_log("Processing payout record $index: " . print_r($payout, true));

                // Skip if this isn't a valid payout array
                if (!is_array($payout)) {
                    error_log("Skipping non-array payout at index $index");
                    continue;
                }

                // Escape and validate all values
                $payd_refid = mysqli_real_escape_string($this->conn, isset($payout['payd_refid']) ? $payout['payd_refid'] : '0');
                $payd_ledgerid = mysqli_real_escape_string($this->conn, isset($payout['payd_ledgerid']) ? $payout['payd_ledgerid'] : '0');
                $payd_name = mysqli_real_escape_string($this->conn, isset($payout['payd_name']) ? $payout['payd_name'] : '');
                $payd_amount = mysqli_real_escape_string($this->conn, isset($payout['payd_amount']) ? $payout['payd_amount'] : '0');
                $payd_remarks = mysqli_real_escape_string($this->conn, isset($payout['payd_remarks']) ? $payout['payd_remarks'] : '');
                $payd_shiftno = mysqli_real_escape_string($this->conn, isset($payout['payd_shiftno']) ? $payout['payd_shiftno'] : '0');
                $payd_dayno = mysqli_real_escape_string($this->conn, isset($payout['payd_dayno']) ? $payout['payd_dayno'] : '0');
                $payd_user = mysqli_real_escape_string($this->conn, isset($payout['payd_user']) ? $payout['payd_user'] : '0');

                // Handle datetime properly - escape it too
                $payd_datetime = isset($payout['payd_datetime']) && !empty($payout['payd_datetime']) ?
                    mysqli_real_escape_string($this->conn, $payout['payd_datetime']) :
                    date('Y-m-d H:i:s');

                // Escape the other parameters
                $pm_id_escaped = mysqli_real_escape_string($this->conn, $pm_id);
                $comid_escaped = mysqli_real_escape_string($this->conn, $comid);
                $locid_escaped = mysqli_real_escape_string($this->conn, $locid);

                $sql = "INSERT INTO pos_payout_dtl (
                        payd_refid, payd_ledgerid, payd_name,
                        payd_amount, payd_remarks, payd_shiftno,
                        payd_dayno, payd_user, payd_datetime,
                        PmId, ComId, LocId
                    ) VALUES (
                        '$payd_refid',
                        '$payd_ledgerid',
                        '$payd_name',
                        '$payd_amount',
                        '$payd_remarks',
                        '$payd_shiftno',
                        '$payd_dayno',
                        '$payd_user',
                        '$payd_datetime',
                        '$pm_id_escaped',
                        '$comid_escaped',
                        '$locid_escaped'
                    )";

                error_log("Executing SQL: " . $sql);
                $result = mysqli_query($this->conn, $sql);
                if (!$result) {
                    throw new Exception("Error inserting payout record: " . mysqli_error($this->conn) . " SQL: " . $sql);
                }
                error_log("Successfully inserted payout record for: " . $payd_name);
            }

            // Commit transaction
            mysqli_commit($this->conn);
            mysqli_autocommit($this->conn, true);

            return array('success' => true, 'message' => 'Payout data saved successfully');
        } catch (Exception $e) {
            // Rollback transaction on error
            mysqli_rollback($this->conn);
            mysqli_autocommit($this->conn, true);

            return array('success' => false, 'message' => 'Error saving payout data: ' . $e->getMessage());
        }
    }

    /**
     * Delete a payout record by ID, company, location, and pm_id.
     * Returns array('success' => bool, 'message' => string)
     */
    public function DeletePayoutRecord($payd_id, $comid, $locid)
    {
        $payd_id = mysqli_real_escape_string($this->conn, $payd_id);
        $comid   = mysqli_real_escape_string($this->conn, $comid);
        $locid   = mysqli_real_escape_string($this->conn, $locid);
        $pm_id = $this->GetPMId($comid, $locid);

        if (!$pm_id) {
            throw new Exception("Invalid comid or locid - could not find pm_id");
        }

        $sql = "DELETE FROM pos_payout_dtl WHERE payd_refid = '$payd_id' AND ComId = '$comid' AND LocId = '$locid' AND PmId = '$pm_id'";
        $result = mysqli_query($this->conn, $sql);

        if ($result) {
            return array('success' => true, 'message' => 'Payout record deleted successfully.');
        } else {
            return array('success' => false, 'message' => 'Error deleting payout record: ' . mysqli_error($this->conn));
        }
    }
    public function DeletePayoutRecordCloud($payd_id, $comid, $locid)
    {
        $payd_id = mysqli_real_escape_string($this->conn, $payd_id);
        $comid   = mysqli_real_escape_string($this->conn, $comid);
        $locid   = mysqli_real_escape_string($this->conn, $locid);
        $pm_id = $this->GetPMId($comid, $locid);

        if (!$pm_id) {
            throw new Exception("Invalid comid or locid - could not find pm_id");
        }

        $sql = "DELETE FROM pos_payout_dtl WHERE payd_id = '$payd_id' AND ComId = '$comid' AND LocId = '$locid' AND PmId = '$pm_id'";
        $result = mysqli_query($this->conn, $sql);

        if ($result) {
            return array('success' => true, 'message' => 'Payout record deleted successfully.');
        } else {
            return array('success' => false, 'message' => 'Error deleting payout record: ' . mysqli_error($this->conn));
        }
    }
    public function GetPayoutReport($comid, $locid, $pm_id, $startDate, $endDate, $empId)
    {
        // Escape variables first
        $comid     = mysqli_real_escape_string($this->conn, $comid);
        $locid     = mysqli_real_escape_string($this->conn, $locid);
        $pm_id     = mysqli_real_escape_string($this->conn, $pm_id);
        $startDate = mysqli_real_escape_string($this->conn, $startDate);
        $endDate   = mysqli_real_escape_string($this->conn, $endDate);
        $empId     = mysqli_real_escape_string($this->conn, $empId);
        //SELECT `payd_id` as Id, `payd_refid` as StaffId,  `payd_name` as Name, `payd_amount` as Amount, `payd_remarks` as Remarks, `payd_shiftno`, `payd_dayno`, `payd_user`, `payd_datetime`, `PmId`, `ComId`, `LocId`, `CurrentDate` FROM `pos_payout_dtl` WHERE `payd_datetime`=$payd_datetime, `PmId` =$PmId, `ComId`=$ComId, `LocId`=$LocId
        $sql = "SELECT
                    ppd.payd_id AS ID,
                    ppd.payd_refid AS StaffId,
                    ppd.payd_name AS Name,
                    ppd.payd_amount AS Amount,
                    ppd.payd_remarks AS Remarks,
                    ppd.payd_shiftno AS ShiftNo,
                    ppd.payd_dayno AS DayNo,
                    ppd.payd_user AS User,
                    ppd.payd_datetime AS DateTime,
                    ppd.PmId,
                    ppd.ComId,
                    ppd.LocId,
                    pm.pcm_name AS CompanyName,
                    pl.plm_name AS LocationName
                FROM pos_payout_dtl AS ppd
                INNER JOIN pos_company_mast AS pm ON pm.pcm_id = ppd.ComId
                INNER JOIN pos_location_mast AS pl ON pl.plm_id = ppd.LocId
                WHERE DATE(ppd.payd_datetime) >= '$startDate'
                  AND DATE(ppd.payd_datetime) <= '$endDate'
                --   AND ppd.ComId = '$comid'
                --   AND ppd.LocId = '$locid'
                --   AND ppd.PmId = '$pm_id'
                  AND ppd.payd_refid = '$empId'
                ORDER BY ppd.payd_datetime DESC, ppd.payd_name";
        // Log the query for debugging (remove in production)
        error_log("Payout Report Query: " . $sql);
        $result = mysqli_query($this->conn, $sql);
        return $result;
    }
    public function GetAdvanceReport($comid, $locid, $startDate, $endDate, $salesmanId, $OperationType, $OptionsSalesMan, $OptionComidLocid)
    {
        // (Operation Type = 1: Summary, 2: Detailed), (OptionsSalesMan : 1: All, 2: By SalesManId as Payd_LedgerId)
        // (OptionComidLocid : 1: All, 2: By ComId and LocId)

        // Escape variables first
        $comid = mysqli_real_escape_string($this->conn, $comid);
        $locid = mysqli_real_escape_string($this->conn, $locid);
        $salesmanId = mysqli_real_escape_string($this->conn, $salesmanId);
        $startDate = mysqli_real_escape_string($this->conn, $startDate);
        $endDate = mysqli_real_escape_string($this->conn, $endDate);

        // Base query structure based on Operation Type
        if ($OperationType == 1) {
            // Summary Report - Group by employee
            $sql = "SELECT
                        pe.emp_id AS ID,
                        pe.emp_printname AS Name,
                        pm.pcm_name AS CompanyName,
                        pl.plm_name AS LocationName,
                        COUNT(ppd.payd_refid) AS TotalTransactions,
                        SUM(ppd.payd_amount) AS TotalAmount,
                        MIN(ppd.payd_datetime) AS FirstAdvance,
                        MAX(ppd.payd_datetime) AS LastAdvance
                    FROM pos_payout_dtl AS ppd
                    INNER JOIN pos_employeeinfo AS pe ON ppd.payd_ledgerid = pe.emp_id
                    INNER JOIN pos_company_mast AS pm ON pm.pcm_id = ppd.ComId
                    INNER JOIN pos_location_mast AS pl ON pl.plm_id = ppd.LocId
                    WHERE DATE(ppd.payd_datetime) BETWEEN '$startDate' AND '$endDate'";
        } else {
            // Detailed Report - Show all records
            $sql = "SELECT
                        ppd.payd_refid AS ID,
                        pe.emp_id AS EmployeeID,
                        pe.emp_printname AS Name,
                        ppd.payd_amount AS Amount,
                        ppd.payd_remarks AS Remarks,
                        ppd.payd_datetime AS DateTime,
                        ppd.payd_shiftno AS ShiftNo,
                        ppd.payd_dayno AS DayNo,
                        pm.pcm_name AS CompanyName,
                        pl.plm_name AS LocationName,
                        ppd.PmId,
                        ppd.ComId,
                        ppd.LocId
                    FROM pos_payout_dtl AS ppd
                    INNER JOIN pos_employeeinfo AS pe ON ppd.payd_ledgerid = pe.emp_id
                    INNER JOIN pos_company_mast AS pm ON pm.pcm_id = ppd.ComId
                    INNER JOIN pos_location_mast AS pl ON pl.plm_id = ppd.LocId
                    WHERE DATE(ppd.payd_datetime) BETWEEN '$startDate' AND '$endDate'";
        }

        // Apply SalesMan filter based on OptionsSalesMan
        if ($OptionsSalesMan == 2 && !empty($salesmanId) && $salesmanId != '0') {
            // Filter by specific SalesMan ID
            $sql .= " AND ppd.payd_ledgerid = '$salesmanId'";
        }
        // If OptionsSalesMan == 1, show all salesmen (no additional filter needed)

        // Apply Company and Location filter based on OptionComidLocid
        if ($OptionComidLocid == 2) {
            // Filter by specific Company and Location
            if (!empty($comid) && $comid != '0') {
                $sql .= " AND ppd.ComId = '$comid'";
            }
            if (!empty($locid) && $locid != '0') {
                $sql .= " AND ppd.LocId = '$locid'";
            }
        }
        // If OptionComidLocid == 1, show all companies and locations (no additional filter needed)

        // Add GROUP BY for summary report
        if ($OperationType == 1) {
            $sql .= " GROUP BY pe.emp_id, pe.emp_printname, pm.pcm_name, pl.plm_name, ppd.ComId, ppd.LocId";
            $sql .= " ORDER BY TotalAmount DESC, pe.emp_printname";
        } else {
            $sql .= " ORDER BY ppd.payd_datetime DESC, pe.emp_printname";
        }

        // Log the query for debugging (remove in production)
        error_log("Advance Report Query: " . $sql);

        $result = mysqli_query($this->conn, $sql);

        if ($result) {
            $data = array();
            while ($row = mysqli_fetch_assoc($result)) {
                $data[] = $row;
            }
            return array('success' => true, 'data' => $data);
        } else {
            return array('success' => false, 'message' => 'Error fetching advance report: ' . mysqli_error($this->conn));
        }
    }

    public function GetMonthlySummaryReportAll($year, $month)
    {
        try {
            // Escape variables for security
            $year = (int)mysqli_real_escape_string($this->conn, $year);
            $month = (int)mysqli_real_escape_string($this->conn, $month);

            // Call stored procedure to get all result sets
            $sql = "CALL sp_monthly_sales_report($year, $month)";

            // Log the query for debugging
            error_log("Monthly Summary Report Query: " . $sql);

            // Execute the stored procedure
            if (!mysqli_multi_query($this->conn, $sql)) {
                throw new Exception("Error executing stored procedure: " . mysqli_error($this->conn));
            }

            $resultSets = array(
                'SalesmanData' => array(),
                'ItemwiseData' => array(),
                'AdvanceData' => array()
            );

            $resultIndex = 0;
            $resultNames = array('SalesmanData', 'ItemwiseData', 'AdvanceData');

            // Process each result set
            do {
                $result = mysqli_store_result($this->conn);
                if ($result) {
                    $data = array();
                    while ($row = mysqli_fetch_assoc($result)) {
                        $data[] = $row;
                    }

                    if ($resultIndex < count($resultNames)) {
                        $resultSets[$resultNames[$resultIndex]] = $data;
                    }

                    mysqli_free_result($result);
                }
                $resultIndex++;
            } while (mysqli_next_result($this->conn));

            // Check if we have any data
            $totalRecords = count($resultSets['SalesmanData']) + count($resultSets['ItemwiseData']) + count($resultSets['AdvanceData']);

            if ($totalRecords > 0) {
                return array('success' => true, 'data' => $resultSets);
            } else {
                return array('success' => false, 'message' => 'No data found for the specified month and year');
            }
        } catch (Exception $e) {
            error_log("GetMonthlySummaryReportAll Error: " . $e->getMessage());
            return array('success' => false, 'message' => 'Error fetching monthly summary report: ' . $e->getMessage());
        }
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
