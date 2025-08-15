<?php

include_once 'clsfunctionchqmodule.php';
//include_once 'dbconnect.php';
$clschq = new clsfunctionchqModule();

if (isset($_REQUEST['AjaxRequest'])) {
    if ((int) $_REQUEST['AjaxRequest'] == 1) { //Get User
        $GetUser = $clschq->GetUser();
        $GetUserRes = array();
        while ($rows = mysqli_fetch_assoc($GetUser)) {
            $GetUserRes[] = $rows;
        }

        if ($GetUser) {
            echo json_encode(array("User" => $GetUserRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 2) { //Get Company
        $GetComapany = $clschq->GetComapany();
        $GetComapanyRes = array();
        while ($rows = mysqli_fetch_assoc($GetComapany)) {
            $GetComapanyRes[] = $rows;
        }
        if ($GetComapany) {
            echo json_encode(array("Company" => $GetComapanyRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 3) {

        $GetLocation = $clschq->GetLocation();
        $GetLocationRes = array();
        while ($rows = mysqli_fetch_assoc($GetLocation)) {
            $GetLocationRes[] = $rows;
        }
        if ($GetLocation) {
            echo json_encode(array("Location" => $GetLocationRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 45) {
        $username = $_GET['name'];
        $password = trim($_GET['pass']);
        $checkUser = $clschq->_login($username, $password);
        $row = mysqli_num_rows($checkUser);
        if ($row == 1) {
            $rows = mysqli_fetch_assoc($checkUser);
            echo json_encode(array("Success" => true, "Data" => $rows["id"]));
        } else {
            echo json_encode(array("Success" => false, "Data" => $row . ',' . $username . ',' . $password));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 4) { //Save Company
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $pcm_name = $row['companyname'];
        $pcm_active = $row['active'];
        $saveCompay = $clschq->SaveCompany($pcm_name, $pcm_active);
        if ($saveCompay) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Data Not Saved" => false));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 5) { //Update Company
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $pcm_id = $row['id'];
        $pcm_name = $row['companyname'];
        $pcm_active = $row['active'];
        $saveCompay = $clschq->UpdateCompany($pcm_id, $pcm_name, $pcm_active);
        if ($saveCompay) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Data Not Saved" => false));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 6) { //select payee
        $Getpayee = $clschq->getPayee();
        $GetpayeeRes = array();
        while ($rows = mysqli_fetch_assoc($Getpayee)) {
            $GetpayeeRes[] = $rows;
        }
        if ($Getpayee) {
            echo json_encode(array("Success" => true, "Data" => $GetpayeeRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 7) { //Save payee
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $payeename = $row['payeename'];
        $active = $row['active'];
        $savePayee = $clschq->SavePayee($payeename, $active);
        if ($savePayee) {
            echo json_encode(array("Success" => true, "Data" => $savePayee));
        } else {
            echo json_encode(array("Data Not Saved" => false));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 8) { //Update payee
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $id = $row['id'];
        $savePayee = $clschq->DeletePayee($id);
        if ($savePayee) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Data Not Saved" => false));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 9) { //select bankStatement
        $Getpayee = $clschq->getBankStatement();
        $GetpayeeRes = array();
        while ($rows = mysqli_fetch_assoc($Getpayee)) {
            $GetpayeeRes[] = $rows;
        }
        if ($Getpayee) {
            echo json_encode(array("Success" => true, "Data" => $GetpayeeRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 10) { //Save bankstatement
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $CompanyName = $row['CompanyName'];
        $PayeeChq = $row['PayeeChq'];
        $BankName = $row['BankName'];
        $PayeeName = $row['PayeeName'];
        $PayeeDate = $row['PayeeDate'];
        $PayeeMode = $row['PayeeMode'];
        $PayeeAmountDr = $row['PayeeAmountDr'];
        $PayeeAmountCr = $row['PayeeAmountCr'];
        $PayeeStatus = $row['PayeeStatus'];
        $useridcreated = $row['UserId'];
        $savePayee = $clschq->SaveBankSatement($CompanyName, $PayeeChq, $BankName, $PayeeName, $PayeeDate, $PayeeMode, $PayeeAmountDr, $PayeeAmountCr, $PayeeStatus, $useridcreated);
        if ($savePayee) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Data Not Saved" => false));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 11) { //Save bankstat1ement
        $id = $_GET['id'];
        $res = $_GET['res'];
        $useridmodified = $_GET['UserId'];
        $savePayee = $clschq->UpdateBankSatement($id, $res, $useridmodified);
        if ($savePayee) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Data Not Saved" => false));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 12) { //select bankStatement
        $frdate = $_GET['frdate'];
        $todate = $_GET['todate'];
        $Getpayee = $clschq->getBankStatementByDate($frdate, $todate);
        $GetpayeeRes = array();
        while ($rows = mysqli_fetch_assoc($Getpayee)) {
            $GetpayeeRes[] = $rows;
        }
        if ($Getpayee) {
            echo json_encode(array("Success" => true, "Data" => $GetpayeeRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 13) { //select bankStatement
        $frdate = $_GET['frdate'];
        $Getpayee = $clschq->getBankPendingCheque($frdate);
        $GetpayeeRes = array();
        while ($rows = mysqli_fetch_assoc($Getpayee)) {
            $GetpayeeRes[] = $rows;
        }
        if ($Getpayee) {
            echo json_encode(array("Success" => true, "Data" => $GetpayeeRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 14) { //Save bankstat1ement
        $id = $_GET['id'];
        $savePayee = $clschq->DeleteBankSatement($id);
        if ($savePayee) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Data Not Saved" => false));
        }
    }
    
}
?>