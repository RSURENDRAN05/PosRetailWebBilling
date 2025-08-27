<?php
// Enable error reporting for debugging 500 errors
error_reporting(E_ALL);
ini_set('display_errors', 1);
ini_set('log_errors', 1);

// Configure PHP for handling large POST data
ini_set('memory_limit', '128M');
ini_set('post_max_size', '32M');
ini_set('max_input_vars', '10000');
ini_set('max_execution_time', '300'); // 5 minutes

include_once 'clsfunsynctocloudlocal.php';
// Try to instantiate the class
try {
    $clsfunreq = new clsfuncsync();
} catch (Exception $e) {
    header("Content-Type: application/json; charset=utf-8");
    echo json_encode(array("Success" => false, "Msg" => "Failed to create class instance: " . $e->getMessage(), "Data" => ""));
    exit;
}

header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Credentials:true");
header("Access-Control-Allow-Methods: GET, POST, OPTIONS");
header("Access-Control-Allow-Headers:Origin,Content-Type,X-Amz-Date,Authorization,X-Api-Key,X-Amz-Security-Token,locale");
header("Content-Type: application/json; charset=utf-8");
if (isset($_REQUEST['AjaxRequest'])) { //POS_MASTER

    if ((int)$_REQUEST['AjaxRequest'] === 1) {
        try {
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
        } catch (Exception $e) {
            echo json_encode(array("Success" => false, "Msg" => "Request 1 Error: " . $e->getMessage(), "Data" => ""));
        }
        exit;
    }

    if ((int) $_REQUEST['AjaxRequest'] == 2) {
        try {
            // Get parameters from URL - match VB.NET parameter names
            $pm_id = isset($_GET['pm_id']) ? $_GET['pm_id'] : '';
            $trno = isset($_GET['trno']) ? $_GET['trno'] : '';
            $comid = isset($_GET['comid']) ? $_GET['comid'] : '';
            $locid = isset($_GET['locid']) ? $_GET['locid'] : '';

            // Validate required parameters
            if (empty($pm_id) || empty($trno) || empty($comid) || empty($locid)) {
                throw new Exception("Missing required parameters: pm_id, trno, comid, or locid");
            }

            // Check if invoice already exists
            $GetSaleInvoiceHdr = $clsfunreq->GetSaleInvoiceHdr($pm_id, $trno, $comid, $locid);
            $hdrExists = 0;
            if ($GetSaleInvoiceHdr && mysqli_num_rows($GetSaleInvoiceHdr) > 0) {
                $hdrRow = mysqli_fetch_array($GetSaleInvoiceHdr);
                $hdrExists = isset($hdrRow[0]) ? (int)$hdrRow[0] : 0;
            }

            $GetSaleInvoiceDtl = $clsfunreq->GetSaleInvoiceDtl($pm_id, $trno, $comid, $locid);
            $dtlExists = 0;
            if ($GetSaleInvoiceDtl && mysqli_num_rows($GetSaleInvoiceDtl) > 0) {
                $dtlRow = mysqli_fetch_array($GetSaleInvoiceDtl);
                $dtlExists = isset($dtlRow[0]) ? (int)$dtlRow[0] : 0;
            }

            if ($hdrExists == 0 && $dtlExists == 0) {
                // Get form data from VB.NET POST request
                $hdrdata = isset($_POST['hdrdata']) ? $_POST['hdrdata'] : '';
                $dtldata = isset($_POST['dtldata']) ? $_POST['dtldata'] : '';

                if (empty($hdrdata) || empty($dtldata)) {
                    throw new Exception("No header or detail data received in POST");
                }

                // URL decode the data (VB.NET sends it URL-encoded)
                $hdrdata = urldecode($hdrdata);
                $dtldata = urldecode($dtldata);

                // Decode JSON data
                $hdrArray = json_decode($hdrdata, true);
                $dtlArray = json_decode($dtldata, true);

                if (!$hdrArray || !$dtlArray) {
                    $hdrError = json_last_error_msg();
                    json_decode($dtldata, true); // Reset error state
                    $dtlError = json_last_error_msg();
                    throw new Exception("Invalid JSON data - Header: $hdrError, Detail: $dtlError");
                }

                // Prepare data structure for SaveInvoiceData
                $data = array(
                    'invoice_hdr' => $hdrArray,
                    'invoice_dtl' => $dtlArray
                );

                $result = $clsfunreq->SaveInvoiceData($data);

                if ($result['success']) {
                    echo json_encode(array("Success" => true, "Msg" => $result['message'], "Data" => "Transaction saved: " . $trno));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => $result['message'], "Data" => "Transaction failed: " . $trno));
                }
            } else {
                // Invoice already exists
                echo json_encode(array("Success" => false, "Msg" => "Exists", "Data" => "Transaction already exists: " . $trno));
            }
        } catch (Exception $e) {
            echo json_encode(array("Success" => false, "Msg" => "Request 2 Error: " . $e->getMessage(), "Data" => "Transaction failed: " . (isset($trno) ? $trno : 'unknown')));
        }
        exit;
    }
    if ((int) $_REQUEST['AjaxRequest'] == 3) { //get sales report
        try {
            // Get parameters from URL - match VB.NET parameter names
            $comid = isset($_GET['comid']) ? $_GET['comid'] : '';
            $locid = isset($_GET['locid']) ? $_GET['locid'] : '';
            $startDate = isset($_GET['startDate']) ? $_GET['startDate'] : '';
            $endDate = isset($_GET['endDate']) ? $_GET['endDate'] : '';

            // Validate required parameters
            if (empty($comid) || empty($locid) || empty($startDate) || empty($endDate)) {
                throw new Exception("Missing required parameters: comid, locid, startDate, or endDate");
            }

            // Fetch sales report data
            $salesReport = $clsfunreq->GetSalesReport($comid, $locid, $startDate, $endDate);
            $arr = array();
            if ($salesReport) {
                while ($row = mysqli_fetch_assoc($salesReport)) {
                    $arr[] = $row;
                }
                echo json_encode(array("Success" => true, "Data" => $arr));
            } else {
                echo json_encode(array("Success" => false, "Msg" => "No sales data found", "Data" => ""));
            }
        } catch (Exception $e) {
            echo json_encode(array("Success" => false, "Msg" => "Request 3 Error: " . $e->getMessage(), "Data" => ""));
        }
    }
}

// Fallback for invalid requests
echo json_encode(array("Success" => false, "Msg" => "Invalid or missing AjaxRequest parameter", "Data" => ""));
