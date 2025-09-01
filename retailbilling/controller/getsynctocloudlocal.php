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
            // Get parameters from REQUEST (works for both GET and POST)
            $pm_id = isset($_REQUEST['pm_id']) ? $_REQUEST['pm_id'] : '';
            $trno = isset($_REQUEST['trno']) ? $_REQUEST['trno'] : '';
            $comid = isset($_REQUEST['comid']) ? $_REQUEST['comid'] : '';
            $locid = isset($_REQUEST['locid']) ? $_REQUEST['locid'] : '';

            // Validate required parameters
            if (empty($pm_id) || empty($trno) || empty($comid) || empty($locid)) {
                throw new Exception("Missing required parameters: pm_id, trno, comid, or locid");
            }
            $hdrExists = 0;
            $dtlExists = 0;
            // // Check if invoice already exists
            // $GetSaleInvoiceHdr = $clsfunreq->GetSaleInvoiceHdr($pm_id, $trno, $comid, $locid);
            // $hdrExists = 0;
            // if ($GetSaleInvoiceHdr && mysqli_num_rows($GetSaleInvoiceHdr) > 0) {
            //     $hdrRow = mysqli_fetch_array($GetSaleInvoiceHdr);
            //     $hdrExists = isset($hdrRow[0]) ? (int)$hdrRow[0] : 0;
            // }

            // $GetSaleInvoiceDtl = $clsfunreq->GetSaleInvoiceDtl($pm_id, $trno, $comid, $locid);
            // $dtlExists = 0;
            // if ($GetSaleInvoiceDtl && mysqli_num_rows($GetSaleInvoiceDtl) > 0) {
            //     $dtlRow = mysqli_fetch_array($GetSaleInvoiceDtl);
            //     $dtlExists = isset($dtlRow[0]) ? (int)$dtlRow[0] : 0;
            // }

            if ($hdrExists == 0 && $dtlExists == 0) {
                // Get form data from VB.NET POST request
                $hdrdata = isset($_POST['hdrdata']) ? $_POST['hdrdata'] : '';
                $dtldata = isset($_POST['dtldata']) ? $_POST['dtldata'] : '';
                $paymodedata = isset($_POST['paymodedata']) ? $_POST['paymodedata'] : '';
                if (empty($hdrdata) || empty($dtldata)) {
                    throw new Exception("No header or detail data received in POST");
                }

                // URL decode the data (VB.NET sends it URL-encoded)
                $hdrdata = urldecode($hdrdata);
                $dtldata = urldecode($dtldata);
                $paymodedata = urldecode($paymodedata);

                // Decode JSON data
                $hdrArray = json_decode($hdrdata, true);
                $dtlArray = json_decode($dtldata, true);
                $paymodeArray = json_decode($paymodedata, true);

                if (!$hdrArray || !$dtlArray) {
                    $hdrError = json_last_error_msg();
                    json_decode($dtldata, true); // Reset error state
                    $dtlError = json_last_error_msg();
                    throw new Exception("Invalid JSON data - Header: $hdrError, Detail: $dtlError");
                }

                // Prepare data structure for SaveInvoiceData
                $data = array(
                    'invoice_hdr' => $hdrArray,
                    'invoice_dtl' => $dtlArray,
                    'payment_mode' => $paymodeArray
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
            // Log the incoming request for debugging (check both GET and POST)
            error_log("AjaxRequest=3 called with GET parameters: " . print_r($_GET, true));
            error_log("AjaxRequest=3 called with POST parameters: " . print_r($_POST, true));

            // Get parameters from REQUEST (works for both GET and POST)
            $comid = isset($_REQUEST['comid']) ? $_REQUEST['comid'] : '';
            $locid = isset($_REQUEST['locid']) ? $_REQUEST['locid'] : '';
            $startDate = isset($_REQUEST['startDate']) ? $_REQUEST['startDate'] : '';
            $endDate = isset($_REQUEST['endDate']) ? $_REQUEST['endDate'] : '';

            // Validate required parameters
            if (empty($comid) || empty($locid) || empty($startDate) || empty($endDate)) {
                throw new Exception("Missing required parameters: comid=$comid, locid=$locid, startDate=$startDate, endDate=$endDate");
            }

            // Fetch sales report data
            $salesReport = $clsfunreq->GetSalesReportSummary($comid, $locid, $startDate, $endDate);
            $arr = array();
            if ($salesReport) {
                while ($row = mysqli_fetch_assoc($salesReport)) {
                    $arr[] = $row;
                }
                error_log("Sales report fetched successfully. Records found: " . count($arr));
                echo json_encode(array("Success" => true, "Data" => $arr));
            } else {
                echo json_encode(array("Success" => false, "Msg" => "No sales data found", "Data" => ""));
            }
        } catch (Exception $e) {
            error_log("Request 3 Error: " . $e->getMessage());
            echo json_encode(array("Success" => false, "Msg" => "Request 3 Error: " . $e->getMessage(), "Data" => ""));
        }
        exit;
    }
    if ((int) $_REQUEST['AjaxRequest'] == 4) { //get salesman by Comid,Locid,SalesmanId,From Date,To Date , This Month
        try {
            // Log the incoming request for debugging (check both GET and POST)
            error_log("AjaxRequest=4 called with GET parameters: " . print_r($_GET, true));
            error_log("AjaxRequest=4 called with POST parameters: " . print_r($_POST, true));

            // Get parameters from REQUEST (works for both GET and POST)
            $comid = isset($_REQUEST['comid']) ? $_REQUEST['comid'] : '';
            $locid = isset($_REQUEST['locid']) ? $_REQUEST['locid'] : '';
            $startDate = isset($_REQUEST['startDate']) ? $_REQUEST['startDate'] : '';
            $endDate = isset($_REQUEST['endDate']) ? $_REQUEST['endDate'] : '';

            // Validate required parameters
            if (empty($comid) || empty($locid) || empty($startDate) || empty($endDate)) {
                throw new Exception("Missing required parameters: comid=$comid, locid=$locid, startDate=$startDate, endDate=$endDate");
            }

            // Fetch sales report data
            $salesReport = $clsfunreq->GetSalesReportDetails($comid, $locid, $startDate, $endDate);
            $arr = array();
            if ($salesReport) {
                while ($row = mysqli_fetch_assoc($salesReport)) {
                    $arr[] = $row;
                }
                error_log("Sales report fetched successfully. Records found: " . count($arr));
                echo json_encode(array("Success" => true, "Data" => $arr));
            } else {
                echo json_encode(array("Success" => false, "Msg" => "No sales data found", "Data" => ""));
            }
        } catch (Exception $e) {
            error_log("Request 4 Error: " . $e->getMessage());
            echo json_encode(array("Success" => false, "Msg" => "Request 4 Error: " . $e->getMessage(), "Data" => ""));
        }
        exit;
    }
    if ((int) $_REQUEST['AjaxRequest'] == 5) { //get salesman commission report - detailed
        try {
            // Log the incoming request for debugging (check both GET and POST)
            error_log("AjaxRequest=5 called with GET parameters: " . print_r($_GET, true));
            error_log("AjaxRequest=5 called with POST parameters: " . print_r($_POST, true));
            error_log("AjaxRequest=5 called with REQUEST parameters: " . print_r($_REQUEST, true));

            // Get parameters from REQUEST (works for both GET and POST)
            $comid = isset($_REQUEST['comid']) ? $_REQUEST['comid'] : '';
            $locid = isset($_REQUEST['locid']) ? $_REQUEST['locid'] : '';
            $salesmanId = isset($_REQUEST['salesmanId']) ? $_REQUEST['salesmanId'] : '';
            $startDate = isset($_REQUEST['startDate']) ? $_REQUEST['startDate'] : '';
            $endDate = isset($_REQUEST['endDate']) ? $_REQUEST['endDate'] : '';

            // Validate required parameters - use isset() for numeric fields to allow 0 values
            if (!isset($_REQUEST['comid']) || !isset($_REQUEST['locid']) || !isset($_REQUEST['salesmanId']) || empty($startDate) || empty($endDate)) {
                throw new Exception("Missing required parameters: comid=$comid, locid=$locid, salesmanId=$salesmanId, startDate=$startDate, endDate=$endDate");
            }

            // Fetch salesman commission report data (detailed)
            $salesmanReport = $clsfunreq->GetSalesManReportSummary($comid, $locid, $salesmanId, $startDate, $endDate);
            $arr = array();
            if ($salesmanReport) {
                while ($row = mysqli_fetch_assoc($salesmanReport)) {
                    $arr[] = $row;
                }
                error_log("Salesman commission report fetched successfully. Records found: " . count($arr));
                echo json_encode(array("Success" => true, "Data" => $arr));
            } else {
                echo json_encode(array("Success" => false, "Msg" => "No salesman commission data found", "Data" => ""));
            }
        } catch (Exception $e) {
            error_log("Request 5 Error: " . $e->getMessage());
            echo json_encode(array("Success" => false, "Msg" => "Request 5 Error: " . $e->getMessage(), "Data" => ""));
        }
        exit;
    }

    if ((int) $_REQUEST['AjaxRequest'] == 6) { //get salesman commission report - summary
        try {
            // Log the incoming request for debugging
            error_log("AjaxRequest=6 called with parameters: " . print_r($_GET, true));

            // Get parameters from URL
            $comid = isset($_REQUEST['comid']) ? $_REQUEST['comid'] : '';
            $locid = isset($_REQUEST['locid']) ? $_REQUEST['locid'] : '';
            $salesmanId = isset($_REQUEST['salesmanId']) ? $_REQUEST['salesmanId'] : '';
            $startDate = isset($_REQUEST['startDate']) ? $_REQUEST['startDate'] : '';
            $endDate = isset($_REQUEST['endDate']) ? $_REQUEST['endDate'] : '';

            // Validate required parameters
            if (!isset($_REQUEST['comid']) || !isset($_REQUEST['locid']) || !isset($_REQUEST['salesmanId']) || empty($startDate) || empty($endDate)) {
                throw new Exception("Missing required parameters: comid=$comid, locid=$locid, startDate=$startDate, endDate=$endDate");
            }

            // Fetch salesman commission report summary
            $salesmanSummary = $clsfunreq->GetSalesManDetailReport($comid, $locid, $salesmanId, $startDate, $endDate);
            $arr = array();
            if ($salesmanSummary) {
                while ($row = mysqli_fetch_assoc($salesmanSummary)) {
                    $arr[] = $row;
                }
                error_log("Salesman commission summary fetched successfully. Records found: " . count($arr));
                echo json_encode(array("Success" => true, "Data" => $arr));
            } else {
                echo json_encode(array("Success" => false, "Msg" => "No salesman commission summary found", "Data" => ""));
            }
        } catch (Exception $e) {
            error_log("Request 5 Error: " . $e->getMessage());
            echo json_encode(array("Success" => false, "Msg" => "Request 5 Error: " . $e->getMessage(), "Data" => ""));
        }
        exit;
    }

    if ((int) $_REQUEST['AjaxRequest'] == 7) { //get salesman commission report - all records
        try {
            // Log the incoming request for debugging
            error_log("AjaxRequest=7 called with parameters: " . print_r($_GET, true));

            // Get parameters from URL
            $comid = isset($_GET['comid']) ? $_GET['comid'] : '';
            $locid = isset($_GET['locid']) ? $_GET['locid'] : '';

            // Validate required parameters
            if (empty($comid) || empty($locid)) {
                throw new Exception("Missing required parameters: comid=$comid, locid=$locid");
            }

            // Fetch all salesman commission data
            $salesmanAll = $clsfunreq->GetSalesManReportAll($comid, $locid);
            $arr = array();
            if ($salesmanAll) {
                while ($row = mysqli_fetch_assoc($salesmanAll)) {
                    $arr[] = $row;
                }
                error_log("All salesman commission data fetched successfully. Records found: " . count($arr));
                echo json_encode(array("Success" => true, "Data" => $arr));
            } else {
                echo json_encode(array("Success" => false, "Msg" => "No salesman commission data found", "Data" => ""));
            }
        } catch (Exception $e) {
            error_log("Request 6 Error: " . $e->getMessage());
            echo json_encode(array("Success" => false, "Msg" => "Request 6 Error: " . $e->getMessage(), "Data" => ""));
        }
        exit;
    }
    //payout Details
    if ((int)$_REQUEST['AjaxRequest'] == 8) { //PayoutDetails Delete And Save
        try {
            // Log the incoming request for debugging
            error_log("AjaxRequest=8 called with POST parameters: " . print_r($_POST, true));

            $comid = isset($_POST['comid']) ? $_POST['comid'] : (isset($_REQUEST['comid']) ? $_REQUEST['comid'] : '');
            $locid = isset($_POST['locid']) ? $_POST['locid'] : (isset($_REQUEST['locid']) ? $_REQUEST['locid'] : '');
            $pm_id = isset($_POST['pm_id']) ? $_POST['pm_id'] : (isset($_REQUEST['pm_id']) ? $_REQUEST['pm_id'] : '');
            $payoutdata = isset($_POST['payoutdata']) ? $_POST['payoutdata'] : '';

            // Validate required parameters - use isset() for numeric fields to allow 0 values
            if (!isset($_POST['comid']) && !isset($_REQUEST['comid'])) {
                throw new Exception("Missing required parameter: comid");
            }
            if (!isset($_POST['locid']) && !isset($_REQUEST['locid'])) {
                throw new Exception("Missing required parameter: locid");
            }
            if (empty($payoutdata)) {
                throw new Exception("Missing required parameter: payoutdata");
            }
            if (!isset($_POST['pm_id']) && !isset($_REQUEST['pm_id'])) {
                throw new Exception("Missing required parameter: pm_id");
            }

            // URL decode the data (VB.NET sends it URL-encoded)
            $payoutdata = urldecode($payoutdata);

            // Decode JSON data
            $payoutArray = json_decode($payoutdata, true);

            if (!$payoutArray) {
                $payoutError = json_last_error_msg();
                throw new Exception("Invalid JSON data - Payout: $payoutError");
            }

            // Prepare data structure for SavePayoutData
            $data = array(
                'payout_dtl' => $payoutArray
            );
            $result = $clsfunreq->SavePayoutData($data, $comid, $locid, $pm_id);
            if ($result['success']) {
                echo json_encode(array("Success" => true, "Msg" => $result['message'], "Data" => "Payout data saved successfully."));
            } else {
                echo json_encode(array("Success" => false, "Msg" => $result['message'], "Data" => "Failed to save payout data."));
            }
        } catch (Exception $e) {
            echo json_encode(array("Success" => false, "Msg" => "Request 8 Error: " . $e->getMessage(), "Data" => ""));
        }
        exit;
    }
    if ((int)$_REQUEST['AjaxRequest'] == 9) { //get advance report with parameters
        try {
            // Log the incoming request for debugging
            error_log("AjaxRequest=9 called with GET parameters: " . print_r($_GET, true));
            error_log("AjaxRequest=9 called with POST parameters: " . print_r($_POST, true));

            // Get parameters from REQUEST (works for both GET and POST)
            $comid = isset($_REQUEST['comid']) ? $_REQUEST['comid'] : '';
            $locid = isset($_REQUEST['locid']) ? $_REQUEST['locid'] : '';
            $startDate = isset($_REQUEST['startDate']) ? $_REQUEST['startDate'] : '';
            $endDate = isset($_REQUEST['endDate']) ? $_REQUEST['endDate'] : '';
            $salesmanId = isset($_REQUEST['salesmanId']) ? $_REQUEST['salesmanId'] : '';
            $OperationType = isset($_REQUEST['OperationType']) ? $_REQUEST['OperationType'] : '';
            $OptionsSalesMan = isset($_REQUEST['OptionsSalesMan']) ? $_REQUEST['OptionsSalesMan'] : '';
            $OptionComidLocid = isset($_REQUEST['OptionComidLocid']) ? $_REQUEST['OptionComidLocid'] : '';

            // Validate required parameters
            if (empty($comid) || empty($locid) || empty($startDate) || empty($endDate)) {
                throw new Exception("Missing required parameters: comid=$comid, locid=$locid, startDate=$startDate, endDate=$endDate");
            }

            // Fetch advance report data
            $advanceReportResult = $clsfunreq->GetAdvanceReport($comid, $locid, $startDate, $endDate, $salesmanId, $OperationType, $OptionsSalesMan, $OptionComidLocid);

            if ($advanceReportResult['success']) {
                $arr = $advanceReportResult['data'];
                error_log("Advance report data fetched successfully. Records found: " . count($arr));
                echo json_encode(array("Success" => true, "Data" => $arr));
            } else {
                error_log("Advance report error: " . $advanceReportResult['message']);
                echo json_encode(array("Success" => false, "Msg" => $advanceReportResult['message'], "Data" => ""));
            }
        } catch (Exception $e) {
            error_log("Request 9 Error: " . $e->getMessage());
            echo json_encode(array("Success" => false, "Msg" => "Request 9 Error: " . $e->getMessage(), "Data" => ""));
        }
        exit;
    }
    if ((int)$_REQUEST['AjaxRequest'] == 10) { // Monthly Summary Report using Stored Procedure
        try {
            // Log the incoming request for debugging
            error_log("AjaxRequest=10 called with GET parameters: " . print_r($_GET, true));
            error_log("AjaxRequest=10 called with POST parameters: " . print_r($_POST, true));

            // Get parameters from REQUEST (works for both GET and POST)
            $month = isset($_REQUEST['month']) ? (int)$_REQUEST['month'] : 0;
            $year = isset($_REQUEST['year']) ? (int)$_REQUEST['year'] : 0;

            // Validate required parameters
            if (empty($month) || empty($year) || $month < 1 || $month > 12 || $year < 2020) {
                throw new Exception("Invalid parameters: month=$month, year=$year. Month must be 1-12, year must be >= 2020");
            }

            // Call stored procedure for monthly summary report - get all result sets at once
            $monthlySummaryResult = $clsfunreq->GetMonthlySummaryReportAll($year, $month);

            if ($monthlySummaryResult['success']) {
                $data = $monthlySummaryResult['data'];
                error_log("Monthly summary report data fetched successfully. SalesmanData: " . count($data['SalesmanData']) .
                    ", ItemwiseData: " . count($data['ItemwiseData']) .
                    ", AdvanceData: " . count($data['AdvanceData']));

                echo json_encode(array(
                    "Success" => true,
                    "Data" => array(
                        "SalesmanData" => $data['SalesmanData'],
                        "ItemwiseData" => $data['ItemwiseData'],
                        "AdvanceData" => $data['AdvanceData'],
                        "Month" => $month,
                        "Year" => $year,
                        "MonthYear" => date('M-Y', mktime(0, 0, 0, $month, 1, $year))
                    )
                ));
            } else {
                error_log("Monthly summary report error: " . $monthlySummaryResult['message']);
                echo json_encode(array("Success" => false, "Msg" => $monthlySummaryResult['message'], "Data" => ""));
            }
        } catch (Exception $e) {
            error_log("Request 10 Error: " . $e->getMessage());
            echo json_encode(array("Success" => false, "Msg" => "Request 10 Error: " . $e->getMessage(), "Data" => ""));
        }
        exit;
    }
}

// Fallback for invalid requests
echo json_encode(array("Success" => false, "Msg" => "Invalid or missing AjaxRequest parameter", "Data" => ""));
