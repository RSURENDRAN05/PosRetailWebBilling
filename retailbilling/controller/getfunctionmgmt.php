<?php

include_once 'clsfunctionmgmt.php';
$clsfunreq = new funcProcessMgmt();
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Credentials:true");
header("Access-Control-Allow-Methods: GET, POST, OPTIONS");
header("Access-Control-Allow-Headers:Origin,Content-Type,X-Amz-Date,Authorization,X-Api-Key,X-Amz-Security-Token,locale");
header("Content-Type:application/json");
header('Content-Type: application/json; charset=utf-8');
if (isset($_REQUEST['AjaxRequest'])) {
    if ((int) $_REQUEST['AjaxRequest'] == 1) { //Get User
        $GetUser = $clsfunreq->GetUser();
        $GetUserRes = array();
        while ($rows = mysqli_fetch_assoc($GetUser)) {
            $GetUserRes[] = $rows;
        }
        if ($GetUser) {
            echo json_encode(array("Data" => $GetUserRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 2) { //Get CompanyLocation
        $GetComapany = $clsfunreq->GetComapanyLocation();
        $GetComapanyRes = array();
        while ($rows = mysqli_fetch_assoc($GetComapany)) {
            $GetComapanyRes[] = $rows;
        }
        if ($GetComapany) {
            echo json_encode(array("Success" => true, "Data" => $GetComapanyRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 3) {

        $GetLocation = $clsfunreq->GetLocation();
        $GetLocationRes = array();
        while ($rows = mysqli_fetch_assoc($GetLocation)) {
            $GetLocationRes[] = $rows;
        }
        if ($GetLocation) {
            echo json_encode(array("Data" => $GetLocationRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 4) {
        $username = $_GET['name'];
        $password = trim($_GET['pass']);
        $checkUser = $clsfunreq->_login($username, $password);
        $row = mysqli_num_rows($checkUser);
        if ($row == 1) {
            $rows = mysqli_fetch_assoc($checkUser);
            $data = array(
                "UserId" => $rows["id"],
                "UserRole" => $rows["role"],
                "UserName" => $rows["username"],
                "ComId" => $rows["comid"],
                "BranchID" => $rows["rid"],
                "GroupId" => $rows["group_id"]  // Add this line
            );
            echo json_encode(array("Success" => true, "Data" => $data));
        } else {
            echo json_encode(array("Success" => false, "Data" => $row . ',' . $username . ',' . $password));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 5) { //Save Company
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $pcm_name = $row['companyname'];
        $pcm_active = $row['active'];
        $saveCompay = $clsfunreq->SaveCompany($pcm_name, $pcm_active);
        if ($saveCompay) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 6) { //Update Company
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $pcm_id = $row['id'];
        $pcm_name = $row['companyname'];
        $pcm_active = $row['active'];
        $saveCompay = $clsfunreq->UpdateCompany($pcm_id, $pcm_name, $pcm_active);
        if ($saveCompay) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 7) { //Save Location
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $plm_name = $row['locationname'];
        $plm_active = $row['active'];
        $saveCompay = $clsfunreq->SaveLocation($plm_name, $plm_active);
        if ($saveCompay) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 8) { //Update Location
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $plm_id = $row['id'];
        $plm_name = $row['locationname'];
        $plm_active = $row['active'];
        $saveCompay = $clsfunreq->UpdateLocation($plm_id, $plm_name, $plm_active);
        if ($saveCompay) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    //Tax Group Create
    if ((int) $_REQUEST['AjaxRequest'] == 9) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $taxname = $row['taxname'];
        $taxvalue = $row['taxvalue'];
        $taxstatus = $row['taxactive'];
        $RequestInsert = $clsfunreq->_InsertTaxMastrer($taxname, $taxvalue, $taxstatus);
        if ($RequestInsert) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 10) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $taxid = $row['taxid'];
        $taxname = $row['taxname'];
        $taxvalue = $row['taxvalue'];
        $taxstatus = $row['taxactive'];
        $RequestInsert = $clsfunreq->_UpdateTaxMastrer($taxid, $taxname, $taxvalue, $taxstatus);
        if ($RequestInsert) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 11) {
        $Reg_Id = $_POST['Tax_ID'];
        $ResulQuery = $clsfunreq->_SelectTaxMastrerById($Reg_Id);
        if ($ResulQuery) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 12) {
        $ResulQuery = $clsfunreq->_SelectTaxMastrer();
        $GetDataRes = array();
        if ($ResulQuery instanceof mysqli_result) {
            while ($rows = mysqli_fetch_assoc($ResulQuery)) {
                $GetDataRes[] = $rows;
            }
            echo json_encode(array("Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    //Main Group Create
    if ((int) $_REQUEST['AjaxRequest'] == 13) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $mainname = $row['mainname'];
        $mainstatus = $row['active'];
        $RequestInsert = $clsfunreq->_InsertMainMastrer($mainname, $mainstatus);
        if ($RequestInsert) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 14) {

        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $mainid = $row['id'];
        $mainname = $row['mainname'];
        $mainstatus = $row['active'];
        $RequestInsert = $clsfunreq->_UpdateMainMastrer($mainid, $mainname, $mainstatus);
        if ($RequestInsert) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 15) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $id = $row['id'];
        $ResulQuery = $clsfunreq->_SelectMainMastrerById($id);
        $GetDataRes = array();
        if ($ResulQuery instanceof mysqli_result) {
            while ($rows = mysqli_fetch_assoc($ResulQuery)) {
                $GetDataRes[] = $rows;
            }
            echo json_encode(array("Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 16) {
        $ResulQuery = $clsfunreq->_SelectMainMastrer();
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($ResulQuery)) {
            $GetDataRes[] = $rows;
        }
        if ($ResulQuery) {
            echo json_encode(array("Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    //Sub Group Create
    if ((int) $_REQUEST['AjaxRequest'] == 17) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $catename = $row['catename'];
        $mainid = $row['mainid'];
        $catestatus = $row['active'];
        $RequestInsert = $clsfunreq->_InsertCateMastrer($catename, $mainid, $catestatus);
        if ($RequestInsert) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 18) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $cateid = $row['cateid'];
        $catename = $row['catename'];
        $mainid = $row['mainid'];
        $catestatus = $row['active'];
        $RequestInsert = $clsfunreq->_UpdateCateMastrer($cateid, $catename, $mainid, $catestatus);
        if ($RequestInsert) {
            echo json_encode(array("Success" => true));
        } else {
            echo json_encode(array("Data Not Saved" => false));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 19) {
        $id = $_POST['id'];
        $ResulQuery = $clsfunreq->_SelectCateMastrerById($id);
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($ResulQuery)) {
            $GetDataRes[] = $rows;
        }
        if ($ResulQuery) {
            echo json_encode(array("Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 20) {
        $ResulQuery = $clsfunreq->_SelectCateMastrer();
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($ResulQuery)) {
            $GetDataRes[] = $rows;
        }
        if ($ResulQuery) {
            echo json_encode(array("Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    //Save User
    if ((int) $_REQUEST['AjaxRequest'] == 21) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $username = $row["username"];
        $password = trim($row["userpass"]);
        $group_id = $row["userrole"];  // This is now the group_id from the Group Policy system
        $status = $row["useractive"];
        $locid = $row["userrestid"];
        $comid = $row["usercomid"];

        $saveResults = $clsfunreq->SaveUser($username, $password, $status, $locid, $group_id, $comid);
        if ($saveResults) {
            echo json_encode(array("Success" => true, "Data" => $saveResults));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    //Update User
    if ((int) $_REQUEST['AjaxRequest'] == 22) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $id = $row["userid"];
        $username = $row["username"];
        $password = trim($row["userpass"]);
        $group_id = $row["userrole"];  // This is now the group_id from the Group Policy system
        $status = $row["useractive"];
        $locid = $row["userrestid"];
        $comid = $row["usercomid"];

        $saveResults = $clsfunreq->UpdateUser($id, $username, $password, $status, $locid, $group_id, $comid);
        if ($saveResults) {
            echo json_encode(array("Success" => true, "Data" => $saveResults));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    //Item Create
    if ((int) $_REQUEST['AjaxRequest'] == 23) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $dim_item_barcode = $row['itembarcode'];
        $dim_item_name = $row['itemname'];
        $dim_remark = $row['itemremakrs'];
        $dim_business_type = $row['itembusinesstypeid'];
        $dim_tax_id = $row['itemtaxtypeid'];
        $dim_main_id = $row['itemmaingroupid'];
        $dim_cate_id = $row['itemsubgroupid'];
        $dim_cost_price = $row['itemcostprice'];
        $dim_sell_price = $row['itemsellprice'];
        $dim_min_price = $row['itemminprice'];
        $dim_max_price = $row['itemmaxprice'];
        $dim_allow_disc = $row['itemallowdiscount'];
        $dim_allow_negstock = $row['itemallownegativestock'];
        $dim_allow_multiprice = $row['itemallowmultipleprice'];
        $dim_com_id = $row['itemcompid'];
        $dim_loc_id = $row['itemlocid'];
        $dim_status = $row['itemactive'];
        $dim_op_stock = $row['itemopstock'];
        $RequestInsert = $clsfunreq->_InsertProductMaster($dim_item_barcode, $dim_item_name, $dim_business_type, $dim_main_id, $dim_cate_id, $dim_tax_id, $dim_cost_price, $dim_sell_price, $dim_min_price, $dim_max_price, $dim_allow_disc, $dim_allow_negstock, $dim_allow_multiprice, $dim_op_stock, $dim_com_id, $dim_loc_id, $dim_status, $dim_remark);
        if ($RequestInsert) {
            $productCode = $clsfunreq->_GetProductCode($dim_item_barcode, $dim_item_name, $dim_com_id, $dim_loc_id);
            if (strlen($productCode) > 0) {
                $RequestLiveStock = $clsfunreq->_InsertLiveStock($productCode, $dim_item_barcode, $dim_cost_price, $dim_sell_price, $dim_op_stock, $dim_op_stock, $dim_com_id, $dim_loc_id);
                if ($RequestLiveStock) {
                    echo json_encode(array("Success" => true));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
                }
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
            }
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 24) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $dim_item_id = $row['itemid'];
        $dim_item_barcode = $row['itembarcode'];
        $dim_item_name = $row['itemname'];
        $dim_remark = $row['itemremakrs'];
        $dim_business_type = $row['itembusinesstypeid'];
        $dim_tax_id = $row['itemtaxtypeid'];
        $dim_main_id = $row['itemmaingroupid'];
        $dim_cate_id = $row['itemsubgroupid'];
        $dim_cost_price = $row['itemcostprice'];
        $dim_sell_price = $row['itemsellprice'];
        $dim_min_price = $row['itemminprice'];
        $dim_max_price = $row['itemmaxprice'];
        $dim_allow_disc = $row['itemallowdiscount'];
        $dim_allow_negstock = $row['itemallownegativestock'];
        $dim_allow_multiprice = $row['itemallowmultipleprice'];
        $dim_com_id = $row['itemcompid'];
        $dim_loc_id = $row['itemlocid'];
        $dim_status = $row['itemactive'];
        $dim_op_stock = $row['itemopstock'];
        $RequestInsert = $clsfunreq->_UpdateProductMaster($dim_item_id, $dim_item_barcode, $dim_item_name, $dim_business_type, $dim_main_id, $dim_cate_id, $dim_tax_id, $dim_cost_price, $dim_sell_price, $dim_min_price, $dim_max_price, $dim_allow_disc, $dim_allow_negstock, $dim_allow_multiprice, $dim_op_stock, $dim_com_id, $dim_loc_id, $dim_status, $dim_remark);
        if ($RequestInsert) {
            $RequestLiveStock = $clsfunreq->_InsertLiveStock($dim_item_id, $dim_item_barcode, $dim_cost_price, $dim_sell_price, $dim_op_stock, $dim_op_stock, $dim_com_id, $dim_loc_id);
            if ($RequestLiveStock) {
                echo json_encode(array("Success" => true, "Data" => $RequestLiveStock));
            } else {
                echo json_encode(array("Success" => false, "Data" => $RequestLiveStock));
            }
            // echo json_encode(array("Success" => true, "Data" => $RequestInsert));
        } else {
            echo json_encode(array("Success" => false, "Data" => $RequestInsert));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 25) {
        $id = $_POST['txtitemid'];
        $ResulQuery = $RptQuery->_SelectProductMasterByID($id);
        if ($ResulQuery) {
            print_r(json_encode($ResulQuery));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 26) {
        $id = $_GET['groupid'];
        $ResulQuery = $clsfunreq->_tablegroupmasterbycode($id);
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($ResulQuery)) {
            $GetDataRes[] = $rows;
        }
        if ($ResulQuery) {
            echo json_encode(array("Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 27) {
        $ResulQuery = $clsfunreq->_SelectProductJoin();
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($ResulQuery)) {
            $GetDataRes[] = $rows;
        }
        if ($ResulQuery) {
            echo json_encode(array("Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 28) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $Customerid = $row['customerid'];
        $CustomerName = $row['customername'];
        $CustomerPhone = isset($row['customerphone']) ? $row['customerphone'] : '';
        $ActiveStatus = $row['cmbstatus'];
        $res = $clsfunreq->storeCustomerData($CustomerName, $CustomerPhone, $ActiveStatus);
        if ($res) {
            echo json_encode(array("Success" => true, "Msg" => 'Customer saved successfully'));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Failed to save customer data'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 29) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $id = $row['customerid'];
        $txtCustomerName = $row['customername'];
        $txtCustomerPhone = isset($row['customerphone']) ? $row['customerphone'] : '';
        $status = $row['cmbstatus'];
        $res = $clsfunreq->updateCustomerData($id, $txtCustomerName, $txtCustomerPhone, $status);
        if ($res) {
            echo json_encode(array("Success" => true, "Msg" => 'Customer updated successfully'));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Failed to update customer data'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 30) {
        $ResulQuery = $clsfunreq->selectCustomer();
        $GetDataRes = array();
        if ($ResulQuery) {
            while ($rows = mysqli_fetch_assoc($ResulQuery)) {
                $GetDataRes[] = array(
                    "CustomerId" => $rows['customerId'],
                    "CustomerName" => $rows['customerName'],
                    "CustomerPhone" => $rows['customerPhone'] ?? '',
                    "CustomerPointsEarned" => $rows['customerPointsEarned'] ?? 0,
                    "Status" => $rows['status'],
                    "Created" => $rows['created'] ?? ''
                );
            }
            echo json_encode(array("Success" => true, "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Customer Data Found'));
        }
    }

    // Get All Customers (including inactive)
    if ((int) $_REQUEST['AjaxRequest'] == 68) {
        $ResulQuery = $clsfunreq->selectAllCustomers();
        $GetDataRes = array();
        if ($ResulQuery) {
            while ($rows = mysqli_fetch_assoc($ResulQuery)) {
                $GetDataRes[] = array(
                    "CustomerId" => $rows['customerId'],
                    "CustomerName" => $rows['customerName'],
                    "CustomerPhone" => $rows['customerPhone'] ?? '',
                    "CustomerPointsEarned" => $rows['customerPointsEarned'] ?? 0,
                    "Status" => $rows['status'],
                    "Created" => $rows['created'] ?? ''
                );
            }
            echo json_encode(array("Success" => true, "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Customer Data Found'));
        }
    }

    // Get Customer by ID
    if ((int) $_REQUEST['AjaxRequest'] == 69) {
        $customerId = $_GET['customerid'];
        $ResulQuery = $clsfunreq->selectCustomerById($customerId);
        $GetDataRes = array();
        if ($ResulQuery && mysqli_num_rows($ResulQuery) > 0) {
            $rows = mysqli_fetch_assoc($ResulQuery);
            $GetDataRes = array(
                "CustomerId" => $rows['customerId'],
                "CustomerName" => $rows['customerName'],
                "CustomerPhone" => $rows['customerPhone'] ?? '',
                "CustomerPointsEarned" => $rows['customerPointsEarned'] ?? 0,
                "Status" => $rows['status'],
                "Created" => $rows['created'] ?? ''
            );
            echo json_encode(array("Success" => true, "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Customer Not Found'));
        }
    }

    // Update Customer Points
    if ((int) $_REQUEST['AjaxRequest'] == 70) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $customerId = $row['customerid'];
        $pointsToAdd = $row['points'];
        $res = $clsfunreq->updateCustomerPoints($customerId, $pointsToAdd);
        if ($res) {
            echo json_encode(array("Success" => true, "Msg" => 'Customer points updated successfully'));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Failed to update customer points'));
        }
    }

    // Delete Customer (Soft Delete)
    if ((int) $_REQUEST['AjaxRequest'] == 71) {
        $customerId = $_GET['customerid'];
        $res = $clsfunreq->deleteCustomer($customerId);
        if ($res) {
            echo json_encode(array("Success" => true, "Msg" => 'Customer deleted successfully'));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Failed to delete customer'));
        }
    }
    //Branch Entry
    if ((int) $_REQUEST['AjaxRequest'] == 31) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $branchcustomerid = $row['branchcustomerid'];
        $branchname = $row['branchname'];
        $branchemail = $row['branchemail'];
        $branchcontact = $row['branchcontact'];
        $branchaddress = $row['branchaddress'];
        $branchanydesk = $row['branchanydesk'];
        $branchserver = $row['branchserver'];
        $branchclient = $row['branchclient'];
        $branchtab = $row['branchtab'];
        $branchlock = $row['branchlock'];
        $branchactivationcode = $row['branchactivationcode'];
        $branchmessage = $row['branchWarrningmsg'];
        $branchstatus = $row['branchstatus'];
        $branchinstalldate = $row['branchinstalldate'];
        //        $strl = $branchcustomerid . ',' . $branchname . ',' . $branchemail . ',' . $branchcontact . ',' . $branchaddress . ',' . $branchanydesk . ',' . $branchserver . ',' . $branchclient . ',' . $branchtab . ',' . $branchlock . ',' . $branchactivationcode . ',' . $branchstatus;
        $res = $clsfunreq->_branchSave($branchcustomerid, $branchname, $branchaddress, $branchemail, $branchcontact, $branchanydesk, $branchserver, $branchclient, $branchtab, $branchlock, $branchactivationcode, $branchstatus, $branchmessage, $branchinstalldate);
        $cusSave = $clsfunreq->storeCustomerLedgerData($branchname);
        if ($res) {
            echo json_encode(array("Success" => true, "Data" => $res));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 32) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $branchid = $row['branchid'];
        $branchcustomerid = $row['branchcustomerid'];
        $branchname = $row['branchname'];
        $branchemail = $row['branchemail'];
        $branchcontact = $row['branchcontact'];
        $branchaddress = $row['branchaddress'];
        $branchanydesk = $row['branchanydesk'];
        $branchserver = $row['branchserver'];
        $branchclient = $row['branchclient'];
        $branchtab = $row['branchtab'];
        $branchlock = $row['branchlock'];
        $branchactivationcode = $row['branchactivationcode'];
        $branchmessage = $row['branchWarrningmsg'];
        $branchstatus = $row['branchstatus'];
        $branchinstalldate = $row['branchinstalldate'];
        //        $strl = $branchid . ',' . $branchcustomerid . ',' . $branchname . ',' . $branchemail . ',' . $branchcontact . ',' . $branchaddress . ',' . $branchanydesk . ',' . $branchserver . ',' . $branchclient . ',' . $branchtab . ',' . $branchlock . ',' . $branchactivationcode . ',' . $branchstatus;
        $res = $clsfunreq->_branchUpdate($branchid, $branchcustomerid, $branchname, $branchaddress, $branchemail, $branchcontact, $branchanydesk, $branchserver, $branchclient, $branchtab, $branchlock, $branchactivationcode, $branchstatus, $branchmessage, $branchinstalldate);
        $cusSave = $clsfunreq->updateCustomerLedgerData($branchid, $branchname);
        if ($cusSave) {
            echo json_encode(array("Success" => true, "Data" => $res));
        } else {
            echo json_encode(array("Success" => false, "Data" => 'Data Not Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 33) {
        $ResulQuery = $clsfunreq->_branchSelect();
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($ResulQuery)) {
            $GetDataRes[] = $rows;
        }
        if ($ResulQuery) {
            echo json_encode(array("Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 34) {
        $id = $_GET['id'];
        $ResulQuery = $clsfunreq->_branchselectById($id);
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($ResulQuery)) {
            $GetDataRes[] = $rows;
        }
        if ($ResulQuery) {
            echo json_encode(array("Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }

    //Save Unit
    if ((int) $_REQUEST['AjaxRequest'] == 35) {
        $ResulQuery = $clsfunreq->UnitSelect();
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($ResulQuery)) {
            $GetDataRes[] = $rows;
        }
        if ($ResulQuery) {
            echo json_encode(array("Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 36) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $dum_name = $row["dum_name"];
        $dum_active = trim($row["dum_active"]);
        $saveResults = $clsfunreq->UnitSave($dum_name, $dum_active);
        if ($saveResults) {
            echo json_encode(array("Success" => true, "Data" => $saveResults));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 37) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $dum_id = $row["dum_id"];
        $dum_name = $row["dum_name"];
        $dum_active = trim($row["dum_active"]);
        $saveResults = $clsfunreq->UnitUpdate($dum_id, $dum_name, $dum_active);
        if ($saveResults) {
            echo json_encode(array("Success" => true, "Data" => $saveResults));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    //supplier entry start 20
    if ((int) $_REQUEST['AjaxRequest'] == 38) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $supplierid = $row['supplierid'];
        $supplierName = $row['suppliername'];
        $Status = $row['cmbactive'];
        //$supplierEmail = $_POST['supplieremail'];
        // $supplierMobile = $_POST['suppliermobile'];
        $strval = $supplierName; // . ',' . $supplierEmail . ',' . $supplierMobile;
        $res = $clsfunreq->storesupplierData($supplierName, $Status); //, $supplierEmail, $supplierMobile);
        // $response = array("type" => "0","message" =>$strval);
        // echo json_encode($response);
        if ($res) {
            $cusSave = $clsfunreq->storeSupplierLedgerData($supplierName);
            if ($cusSave) {
                echo json_encode(array("Success" => true, "Data" => $res));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
            }
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 39) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $supplierid = $row['supplierid'];
        $supplierName = $row['suppliername'];
        $Status = $row['cmbactive'];
        $strval = $supplierName; // . ',' . $supplierEmail . ',' . $supplierMobile;
        $res1 = $clsfunreq->updatesupplierData($supplierid, $supplierName, $Status);
        // $response = array("type" => "0","message" =>$strval);
        // echo json_encode($response);
        if ($res1) {
            $res = $clsfunreq->updateSupplierLedgerData($supplierid, $supplierName);
            if ($res == true) {
                echo json_encode(array("Success" => true, "Data" => $res));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
            }
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 40) {
        $ResulQuery = $clsfunreq->selectsupplier();
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($ResulQuery)) {
            $GetDataRes[] = $rows;
        }
        if ($ResulQuery) {
            echo json_encode(array("Success" => true, "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }

    //Purchase
    if ((int) $_REQUEST['AjaxRequest'] == 41) {
        $BillType = $_GET['BillType'];
        $ResulQuery = $clsfunreq->selectMaxBillNo($BillType);
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($ResulQuery)) {
            $GetDataRes[] = $rows;
        }
        if ($ResulQuery) {
            echo json_encode(array("Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 42) {
        $ResulQuery = $clsfunreq->GetProductList();
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($ResulQuery)) {
            $GetDataRes[] = $rows;
        }
        if ($ResulQuery) {
            echo json_encode(array("Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 43) {
        $ItemCode = $_GET['ItemCode'];
        $ResulQuery = $clsfunreq->GetProductListByItemCode($ItemCode);
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($ResulQuery)) {
            $GetDataRes[] = $rows;
        }
        if ($ResulQuery) {
            echo json_encode(array("Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 44) {
        $getjsonDtl = $_GET['jsonDtl'];
        $rowDtl = json_decode($getjsonDtl, true);
        $getjsonHdr = $_GET['jsonHdr'];
        $rowHdr = json_decode($getjsonHdr, true);
        //echo print_r($rowDtl);
        //echo print_r($rowHdr);

        $updatePurTrno = $clsfunreq->updateVoucherNo("PUR");
        if ($updatePurTrno) {
            $trno = $clsfunreq->selectMaxBillNoByType("PUR");
        } else {
            $trno = 0;
        }

        $ppd_trno = $trno;
        $resultsPurDtl = "";
        $resultsPurHdr = "";
        $resultLiveStock = "";
        $pph_paymenttype = "";
        $pph_invdate = "";
        $pph_suppid = "";
        $pph_netamt = "";
        $pph_userid = "";
        foreach ($rowDtl as $row) {
            $ppd_sno = $row['SNO'];
            $ppd_itemcode = $row['ITEMCODE'];
            $ppd_barcode = $row['BARCODE'];
            $ppd_serialno = $row['ITEMSERIALNO'];
            $ppd_batch = $row['PURBATCH'];
            $ppd_prate = $row['PURRATE'];
            $ppd_qty = $row['PURQTY'];
            $ppd_amount = $row['PURAMT'];
            $ppd_discper = $row['PURDISPER'];
            $ppd_discamt = $row['PURDISAMT'];
            $ppd_totalamt = $row['PURTOTAMT'];
            $ppd_taxid = $row['PURTAXID'];
            $ppd_taxamt = $row['PURTAXAMT'];
            $ppd_grossamt = $row['PURGROSSAMT'];
            $ppd_roundoff = $row['PURROUNDOFF'];
            $ppd_netamt = $row['PURNETAMT'];
            $ppd_expiry = $row['PUREXPIRE'];
            $ppd_costprice = $row['PURCOST'];
            $ppd_sellprice = $row['PURSELL'];
            $ppd_comid = $row['PURCOMID'];
            $ppd_locid = $row['PURLOCID'];

            //            $resultsPurDtl = $ppd_trno . ',' . $ppd_sno . ',' . $ppd_itemcode . ',' . $ppd_barcode . ',' . $ppd_serialno . ',' . $ppd_batch . ',' . $ppd_prate . ',' . $ppd_qty . ',' . $ppd_amount . ',' .
            //                    $ppd_discper . ',' . $ppd_discamt . ',' . $ppd_totalamt . ',' . $ppd_taxid . ',' . $ppd_taxamt . ',' . $ppd_grossamt . ',' . $ppd_roundoff . ',' . $ppd_netamt . ',' . $ppd_expiry . ',' .
            //                    $ppd_costprice . ',' . $ppd_sellprice . ',' . $ppd_comid . ',' . $ppd_locid;

            $resultsPurDtl = $clsfunreq->SavePurchaseDataDtl(
                $ppd_trno,
                $ppd_sno,
                $ppd_itemcode,
                $ppd_barcode,
                $ppd_serialno,
                $ppd_batch,
                $ppd_prate,
                $ppd_qty,
                $ppd_amount,
                $ppd_discper,
                $ppd_discamt,
                $ppd_totalamt,
                $ppd_taxid,
                $ppd_taxamt,
                $ppd_grossamt,
                $ppd_roundoff,
                $ppd_netamt,
                $ppd_expiry,
                $ppd_costprice,
                $ppd_sellprice,
                $ppd_comid,
                $ppd_locid
            );
            //LiveStockUpdate
            $dim_item_id = $ppd_itemcode;
            $dim_item_barcode = $ppd_barcode;
            $dim_cost_price = $ppd_costprice;
            $dim_sell_price = $ppd_sellprice;
            $dim_stock_cur = $ppd_qty;
            $dim_com_id = $ppd_comid;
            $dim_loc_id = $ppd_locid;
            $resultLiveStock = $clsfunreq->_UpdateLiveStock($dim_item_id, $dim_item_barcode, $dim_cost_price, $dim_sell_price, $dim_stock_cur, $dim_com_id, $dim_loc_id);
        }
        if ($resultsPurDtl) {
            $pph_trno = $trno;
            foreach ($rowHdr as $row) {
                $pph_refno = $row['PURHDRREFNO'];
                $pph_invdate = $row['PURHDRINVDATE'];
                $pph_purdate = $row['PURHDRPURDATE'];
                $pph_suppid = $row['PURHDRSUPPID'];
                $pph_billdiscper = $row['PURHDRBDISCPER'];
                $pph_billdiscamt = $row['PURHDRBDISCAMT'];
                $pph_netamt = $row['PURHDRNETAMT'];
                $pph_paymenttype = $row['PURHDRPAYMENTTYPE'];
                $pph_baloutamt = $row['PURHDRBALOUT'];
                $pph_comid = $row['PURHDRCOMID'];
                $pph_locid = $row['PURHDRLOCID'];
                $pph_userid = $row['PURHDRUSERID'];
                $resultsPurHdr = $clsfunreq->SavePurchaseDataHdr(
                    $pph_trno,
                    $pph_refno,
                    $pph_invdate,
                    $pph_purdate,
                    $pph_suppid,
                    $pph_billdiscper,
                    $pph_billdiscamt,
                    $pph_netamt,
                    $pph_paymenttype,
                    $pph_baloutamt,
                    $pph_comid,
                    $pph_locid,
                    $pph_userid
                );
            }
        }

        //accoutsLegder Posting
        $accttype = "";
        $modetype = "";
        $statuAcct = "";
        $ledgerid = "";
        if ($pph_paymenttype == 'CASH') {
            $ledgerid = "1";
            $branchid = $pph_suppid;
            $vocheramt = $pph_netamt;
            $accttype = "PUR";
            $modetype = "CA";
            $txtdatepicker = $pph_invdate;
            $statuAcct = "A";
            $userid = $pph_userid;
            $txtnarration = 'Purchase :' . $trno;
            $refinvoiceno = $trno;
            //$ressalesentry = $ledgerid . ',' . $branchid . ',' . $vocheramt . ',' . $accttype . ',' . $modetype . ',' . $txtdatepicker . ',' . $statuAcct . ',' . $userid . ',' . $txtnarration . ',' . $refinvoiceno;
            $ressalesentry = $clsfunreq->storeJournalPurchaseEntry($ledgerid, $branchid, $vocheramt, $accttype, $modetype, $txtdatepicker, $statuAcct, $userid, $txtnarration, $refinvoiceno);
        }
        if ($pph_paymenttype == 'CREDIT') {
            $ledgerid = "2";
            $branchid = $pph_suppid;
            $vocheramt = $pph_netamt;
            $accttype = "PUR";
            $modetype = "CR";
            $txtdatepicker = $pph_invdate;
            $statuAcct = "NP";
            $userid = $pph_userid;
            $txtnarration = 'Purchase :' . $trno;
            $refinvoiceno = $trno;
            $ressalesentry = $ledgerid . ',' . $branchid . ',' . $vocheramt . ',' . $accttype . ',' . $modetype . ',' . $txtdatepicker . ',' . $statuAcct . ',' . $userid . ',' . $txtnarration . ',' . $refinvoiceno;
            $ressalesentry = $clsfunreq->storeJournalPurchaseEntry($ledgerid, $branchid, $vocheramt, $accttype, $modetype, $txtdatepicker, $statuAcct, $userid, $txtnarration, $refinvoiceno);
        }

        if ($ressalesentry) {
            echo json_encode(array("Success" => true, "Data" => $resultsPurDtl . ',' . $resultsPurHdr . ',' . $resultLiveStock . ',' . $ressalesentry));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 45) {
        $ResulQuery = $clsfunreq->selectLedgersupplier();
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($ResulQuery)) {
            $GetDataRes[] = $rows;
        }
        if ($ResulQuery) {
            echo json_encode(array("Success" => true, "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 46) {
        $getjsonDtl = $_GET['jsonDtl'];
        $rowDtl = json_decode($getjsonDtl, true);
        $getjsonHdr = $_GET['jsonHdr'];
        $rowHdr = json_decode($getjsonHdr, true);
        //echo print_r($rowDtl);
        //echo print_r($rowHdr);

        $accttype = "";
        $modetype = "";
        $statuAcct = "";
        $ledgerid = "";
        $resultsPurDtl = "";
        $resultsPurHdr = "";
        $resultLiveStock = "";
        $pph_paymenttype = "";
        $pph_invdate = "";
        $pph_suppid = "";
        $pph_netamt = "";
        $pph_userid = "";
        foreach ($rowHdr as $row) {
            $pph_trno = $row['PURHDRTRNO'];
            $pph_refno = $row['PURHDRREFNO'];
            $pph_invdate = $row['PURHDRINVDATE'];
            $pph_purdate = $row['PURHDRPURDATE'];
            $pph_suppid = $row['PURHDRSUPPID'];
            $pph_billdiscper = $row['PURHDRBDISCPER'];
            $pph_billdiscamt = $row['PURHDRBDISCAMT'];
            $pph_netamt = $row['PURHDRNETAMT'];
            $pph_paymenttype = $row['PURHDRPAYMENTTYPE'];
            $pph_baloutamt = $row['PURHDRBALOUT'];
            $pph_comid = $row['PURHDRCOMID'];
            $pph_locid = $row['PURHDRLOCID'];
            $pph_userid = $row['PURHDRUSERID'];
            $resultsPurHdr = $clsfunreq->SavePurchaseDataHdr(
                $pph_trno,
                $pph_refno,
                $pph_invdate,
                $pph_purdate,
                $pph_suppid,
                $pph_billdiscper,
                $pph_billdiscamt,
                $pph_netamt,
                $pph_paymenttype,
                $pph_baloutamt,
                $pph_comid,
                $pph_locid,
                $pph_userid
            );
        }
        if ($resultsPurDtl) {
            $pph_trno = $trno;
            foreach ($rowHdr as $row) {
                $pph_refno = $row['PURHDRREFNO'];
                $pph_invdate = $row['PURHDRINVDATE'];
                $pph_purdate = $row['PURHDRPURDATE'];
                $pph_suppid = $row['PURHDRSUPPID'];
                $pph_billdiscper = $row['PURHDRBDISCPER'];
                $pph_billdiscamt = $row['PURHDRBDISCAMT'];
                $pph_netamt = $row['PURHDRNETAMT'];
                $pph_paymenttype = $row['PURHDRPAYMENTTYPE'];
                $pph_baloutamt = $row['PURHDRBALOUT'];
                $pph_comid = $row['PURHDRCOMID'];
                $pph_locid = $row['PURHDRLOCID'];
                $pph_userid = $row['PURHDRUSERID'];
                $resultsPurHdr = $clsfunreq->SavePurchaseDataHdr(
                    $pph_trno,
                    $pph_refno,
                    $pph_invdate,
                    $pph_purdate,
                    $pph_suppid,
                    $pph_billdiscper,
                    $pph_billdiscamt,
                    $pph_netamt,
                    $pph_paymenttype,
                    $pph_baloutamt,
                    $pph_comid,
                    $pph_locid,
                    $pph_userid
                );
            }
        }

        //accoutsLegder Posting
        $accttype = "";
        $modetype = "";
        $statuAcct = "";
        $ledgerid = "";
        if ($pph_paymenttype == 'CASH') {
            $ledgerid = "1";
            $branchid = $pph_suppid;
            $vocheramt = $pph_netamt;
            $accttype = "PUR";
            $modetype = "CA";
            $txtdatepicker = $pph_invdate;
            $statuAcct = "A";
            $userid = $pph_userid;
            $txtnarration = 'Purchase :' . $trno;
            $refinvoiceno = $trno;
            //$ressalesentry = $ledgerid . ',' . $branchid . ',' . $vocheramt . ',' . $accttype . ',' . $modetype . ',' . $txtdatepicker . ',' . $statuAcct . ',' . $userid . ',' . $txtnarration . ',' . $refinvoiceno;
            $ressalesentry = $clsfunreq->storeJournalPurchaseEntry($ledgerid, $branchid, $vocheramt, $accttype, $modetype, $txtdatepicker, $statuAcct, $userid, $txtnarration, $refinvoiceno);
        }
        if ($pph_paymenttype == 'CREDIT') {
            $ledgerid = "2";
            $branchid = $pph_suppid;
            $vocheramt = $pph_netamt;
            $accttype = "PUR";
            $modetype = "CR";
            $txtdatepicker = $pph_invdate;
            $statuAcct = "NP";
            $userid = $pph_userid;
            $txtnarration = 'Purchase :' . $trno;
            $refinvoiceno = $trno;
            $ressalesentry = $ledgerid . ',' . $branchid . ',' . $vocheramt . ',' . $accttype . ',' . $modetype . ',' . $txtdatepicker . ',' . $statuAcct . ',' . $userid . ',' . $txtnarration . ',' . $refinvoiceno;
            $ressalesentry = $clsfunreq->storeJournalPurchaseEntry($ledgerid, $branchid, $vocheramt, $accttype, $modetype, $txtdatepicker, $statuAcct, $userid, $txtnarration, $refinvoiceno);
        }

        if ($ressalesentry) {
            echo json_encode(array("Success" => true, "Data" => $resultsPurDtl . ',' . $resultsPurHdr . ',' . $resultLiveStock . ',' . $ressalesentry));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 47) {
        $ResulQuery = $clsfunreq->selectPurchaseInvoice();
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($ResulQuery)) {
            $GetDataRes[] = $rows;
        }
        if ($ResulQuery) {
            echo json_encode(array("Success" => true, "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 48) {
        $trno = $_GET['trno'];
        $GetPurBillHDR = $clsfunreq->selectPurchaseHdrById($trno);
        $GetPurBillHDRRes = array();
        while ($rowsHDR = mysqli_fetch_assoc($GetPurBillHDR)) {
            $GetPurBillHDRRes[] = $rowsHDR;
        }
        $GetPurBillDTL = $clsfunreq->selectPurchaseDtlById($trno);
        $GetPurBillDTLRes = array();
        while ($rowsDTL = mysqli_fetch_assoc($GetPurBillDTL)) {
            $GetPurBillDTLRes[] = $rowsDTL;
        }
        if ($GetPurBillHDR && $GetPurBillDTL) {
            echo json_encode(array("Success" => true, "HDR" => $GetPurBillHDRRes, "DTL" => $GetPurBillDTLRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 49) {
        $getdtl = $_GET['jsonDel'];
        $datadtl = json_decode($getdtl, true);
        $ppd_id = $datadtl['ppd_id'];
        $ppd_trno = $datadtl['ppd_trno'];
        $ppd_itemcode = $datadtl['ppd_itemcode'];
        $ppd_barcode = $datadtl['ppd_barcode'];
        $ppd_qty = $datadtl['ppd_qty'];
        $ppd_costprice = $datadtl['ppd_costprice'];
        $ppd_sellprice = $datadtl['ppd_sellprice'];
        $ppd_comid = $datadtl['ppd_comid'];
        $ppd_locid = $datadtl['ppd_locid'];

        $Getsupplier = $clsfunreq->DeletePurchaseDataDtl($ppd_id, $ppd_trno, $ppd_itemcode, $ppd_barcode, $ppd_qty, $ppd_costprice, $ppd_sellprice, $ppd_comid, $ppd_locid);
        if ($Getsupplier) {
            echo json_encode(array("Success" => true, "Msg" => 'Item Deleted'));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }

    if ((int) $_REQUEST['AjaxRequest'] == 51) { //Get Company
        $GetComapany = $clsfunreq->GetComapany();
        $GetComapanyRes = array();
        while ($rows = mysqli_fetch_assoc($GetComapany)) {
            $GetComapanyRes[] = $rows;
        }
        if ($GetComapany) {
            echo json_encode(array("Data" => $GetComapanyRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 52) { //getfiles
        $RestId = $_GET['Id'];
        $arrFiles = array();
        $dirPath = '../slider/' . $RestId;
        $files = scandir($dirPath);
        $GetPurRes = array();
        foreach ($files as $file) {
            $filePath = $dirPath . '/' . $file;
            if (is_file($filePath)) {
                $strPass = "https://myposqr.com/sam/" . $filePath;
                $GetPurRes[] = $strPass;
            }
        }
        if (true) {
            echo json_encode(array("Success" => true, "Msg" => 'Data Received', "Data" => $GetPurRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Found'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 53) { //UploadImage
        //echo 'Test Page' . $_POST['EmployeeReq'];
        $data = json_decode(file_get_contents("php://input"), true);
        if (isset($data['file']) && isset($data['filename']) && isset($data['Id'])) {
            // Get the Base64 string and decode it
            $fileData = $data['file'];
            $filename = $data['filename'];
            $RestId = $data['Id'];

            // Decode the Base64 string
            $decodedData = base64_decode($fileData);

            // Set the upload directory (make sure it has write permissions)
            $uploadDir = '../slider/' . $RestId . '/';
            if (!is_dir($uploadDir)) {
                mkdir($uploadDir, 0755, true);
            }

            // Save the file
            $filePath = $uploadDir . basename($filename);
            if (file_put_contents($filePath, $decodedData)) {
                echo json_encode(["success" => true, "message" => "File uploaded successfully.", "filePath" => $filePath]);
            } else {
                echo json_encode(["success" => false, "message" => "Failed to save the file."]);
            }
        } else {
            echo json_encode(["success" => false, "message" => "Invalid input data."]);
        }
    }

    if ((int) $_REQUEST['AjaxRequest'] == 54) { //Delete Files
        $path = $_GET["path"];
        $dirPath = '../slider/' . $path;
        if (unlink($dirPath)) {
            echo json_encode(array("Success" => true, "Msg" => 'File Deleted', "Data" => $dirPath));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'File Not Found'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 55 && $_SERVER['REQUEST_METHOD'] == 'POST') { //Save User
        $jsonData = file_get_contents("php://input");
        $data = json_decode($jsonData, true);
        if ($data) {
            $nl_username = $data['RegName'];
            $nl_password = $data['RegPass'];
            $comid = $data['RegComId'];
            $rid = $data['RegBranchId'];
            $nl_usergroup = $data['RegRole'];
            $nl_status = $data['RegActive'];
            $RequestInsert = $clsfunreq->_InsertUser($nl_username, $nl_password, $nl_usergroup, $nl_status, $rid, $comid);
            if ($RequestInsert) {
                echo json_encode(array("Success" => true, "Msg" => 'User Saved'));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
            }
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 56 && $_SERVER['REQUEST_METHOD'] == 'POST') { //Update User
        $jsonData = file_get_contents("php://input");
        $data = json_decode($jsonData, true);
        if ($data) {
            $nl_userid = $data['RegId'];
            $nl_username = $data['RegName'];
            $nl_password = $data['RegPass'];
            $comid = $data['RegComId'];
            $rid = $data['RegBranchId'];
            $nl_usergroup = $data['RegRole'];
            $nl_status = $data['RegActive'];
            $RequestInsert = $clsfunreq->_UpdateUser($nl_userid, $nl_username, $nl_password, $nl_usergroup, $nl_status, $rid, $comid);
            if ($RequestInsert) {
                echo json_encode(array("Success" => true, "Msg" => 'User Updated'));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'No Data Updated'));
            }
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 57 && $_SERVER['REQUEST_METHOD'] == 'POST') { //GetUserList
        $GetQueryData = $clsfunreq->_RptMasterByUser();
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($GetQueryData)) {
            $GetDataRes[] = $rows;
        }
        if ($GetQueryData) {
            echo json_encode(array("Success" => true, "Msg" => 'Data Saved', "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved', "Data" => "No Data"));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 58 && $_SERVER['REQUEST_METHOD'] == 'POST') { //Save UserBranchRightList
        $jsonData = file_get_contents("php://input");
        $data = json_decode($jsonData, true);
        if ($data) {
            $comid = $data['RegComId'];
            $branchid = $data['RegBranchId'];
            $userid = $data['RegUserId'];
            $hmcode = $data['RegHMCode'];
            $smcode = $data['RegSMCode'];
            $active = $data['RegActive'];
            $RequestInsert = $clsfunreq->SaveBranchRightsList($comid, $branchid, $userid, $hmcode, $smcode, $active);
            if ($RequestInsert) {
                echo json_encode(array("Success" => true, "Msg" => 'Branch Saved'));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
            }
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 59 && $_SERVER['REQUEST_METHOD'] == 'POST') { //Delete UserBranchRightList
        $jsonData = file_get_contents("php://input");
        $data = json_decode($jsonData, true);
        if ($data) {
            $UserId = $data['RegUserId'];
            $RequestInsert = $clsfunreq->DeleteBranchRightsList($UserId);
            if ($RequestInsert) {
                echo json_encode(array("Success" => true, "Msg" => 'Branch Updated'));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'No Data Updated'));
            }
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 60 && $_SERVER['REQUEST_METHOD'] == 'POST') { //select UserBranchRightList
        $GetQueryData = $clsfunreq->SelectBranchRightsList();
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($GetQueryData)) {
            $GetDataRes[] = $rows;
        }
        if ($GetQueryData) {
            echo json_encode(array("Success" => true, "Msg" => 'Data Saved', "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved', "Data" => "No Data"));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 61 && $_SERVER['REQUEST_METHOD'] === 'GET') {
        $comid = $_GET['comid'];
        $ResulQuery = $clsfunreq->_branchSelectIp($comid);
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($ResulQuery)) {
            $GetDataRes[] = $rows;
        }
        if ($ResulQuery) {
            echo json_encode(array("Success" => true, "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }

    if ((int) $_REQUEST['AjaxRequest'] == 62 && $_SERVER['REQUEST_METHOD'] === 'POST') { // UpdateIp
        $jsonData = file_get_contents("php://input");
        $data = json_decode($jsonData, true);

        if (is_array($data) && isset($data['ip'], $data['branchid'])) {
            $ip = $data['ip'];
            $branchid = $data['branchid'];
            $GetComapany = $clsfunreq->_branchUpdateIp($branchid, $ip);
            if ($GetComapany) {
                echo json_encode([
                    "Success" => true,
                    "Msg" => "Updated",
                    "Data" => "Ip Address :" . $ip
                ]);
            } else {
                echo json_encode([
                    "Success" => false,
                    "Msg" => "No Data Saved",
                    "Data" => null
                ]);
            }
        } else {
            echo json_encode([
                "Success" => false,
                "Msg" => "Invalid or missing data",
                "Data" => $jsonData
            ]);
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 63) {
        // Multiple Price Management - Save/Insert
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $dim_item_id = $row['itemid'];
        $dim_price_name = $row['itempricename'];
        $dim_price_value = $row['itemprice'];
        $dim_com_id = 1; // Default company ID
        $dim_loc_id = 1; // Default location ID
        $dim_status = 1; // Active status

        $RequestInsert = $clsfunreq->_InsertMultiplePrice($dim_item_id, $dim_price_name, $dim_price_value, $dim_com_id, $dim_loc_id, $dim_status);
        if ($RequestInsert) {
            echo json_encode(array("Success" => true, "Data" => $RequestInsert, "Msg" => "Multiple price saved successfully"));
        } else {
            echo json_encode(array("Success" => false, "Data" => $RequestInsert, "Msg" => "Failed to save multiple price"));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 64) {
        // Multiple Price Management - Update
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $dim_price_id = $row['itempriceid'];
        $dim_item_id = $row['itemid'];
        $dim_price_name = $row['itempricename'];
        $dim_price_value = $row['itemprice'];
        $dim_status = 1; // Active status

        $RequestUpdate = $clsfunreq->_UpdateMultiplePrice($dim_price_id, $dim_item_id, $dim_price_name, $dim_price_value, $dim_status);
        if ($RequestUpdate) {
            echo json_encode(array("Success" => true, "Data" => $RequestUpdate, "Msg" => "Multiple price updated successfully"));
        } else {
            echo json_encode(array("Success" => false, "Data" => $RequestUpdate, "Msg" => "Failed to update multiple price"));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 65) {
        // Multiple Price Management - Delete
        $dim_price_id = $_GET['priceid'];

        $RequestDelete = $clsfunreq->_DeleteMultiplePrice($dim_price_id);
        if ($RequestDelete) {
            echo json_encode(array("Success" => true, "Data" => $RequestDelete, "Msg" => "Multiple price deleted successfully"));
        } else {
            echo json_encode(array("Success" => false, "Data" => $RequestDelete, "Msg" => "Failed to delete multiple price"));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 66) {
        // Multiple Price Management - Get by Item ID
        $dim_item_id = $_GET['itemid'];

        $RequestSelect = $clsfunreq->_GetMultiplePricesByItem($dim_item_id);
        $MultiplePricesRes = array();
        if ($RequestSelect) {
            while ($rows = mysqli_fetch_assoc($RequestSelect)) {
                $MultiplePricesRes[] = array(
                    "Id" => $rows['price_id'],
                    "RefId" => $rows['item_id'],
                    "Name" => $rows['price_name'],
                    "Price" => $rows['price_value'],
                    "Status" => $rows['status'],
                    "Created" => $rows['created']
                );
            }
            echo json_encode(array("Success" => true, "Data" => $MultiplePricesRes));
        } else {
            echo json_encode(array("Success" => false, "Data" => array()));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 67) {
        // Discount Management - Save/Insert
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $disc_name = $row['discountname'];
        $disc_type = $row['discounttype']; // 'percentage' or 'amount'
        $disc_value = $row['discountvalue'];
        $disc_description = isset($row['discountdescription']) ? $row['discountdescription'] : '';
        $disc_status = 1; // Active
        $disc_com_id = 1; // Default company ID
        $disc_loc_id = 1; // Default location ID

        $RequestInsert = $clsfunreq->_InsertDiscount($disc_name, $disc_type, $disc_value, $disc_description, $disc_status, $disc_com_id, $disc_loc_id);
        if ($RequestInsert) {
            echo json_encode(array("Success" => true, "Data" => $RequestInsert, "Msg" => "Discount saved successfully"));
        } else {
            echo json_encode(array("Success" => false, "Data" => $RequestInsert, "Msg" => "Failed to save discount"));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 68) {
        // Discount Management - Update
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $disc_id = $row['discountid'];
        $disc_name = $row['discountname'];
        $disc_type = $row['discounttype'];
        $disc_value = $row['discountvalue'];
        $disc_description = isset($row['discountdescription']) ? $row['discountdescription'] : '';
        $disc_status = $row['discountstatus'];

        $RequestUpdate = $clsfunreq->_UpdateDiscount($disc_id, $disc_name, $disc_type, $disc_value, $disc_description, $disc_status);
        if ($RequestUpdate) {
            echo json_encode(array("Success" => true, "Data" => $RequestUpdate, "Msg" => "Discount updated successfully"));
        } else {
            echo json_encode(array("Success" => false, "Data" => $RequestUpdate, "Msg" => "Failed to update discount"));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 69) {
        // Discount Management - Delete
        $disc_id = $_GET['discountid'];

        $RequestDelete = $clsfunreq->_DeleteDiscount($disc_id);
        if ($RequestDelete) {
            echo json_encode(array("Success" => true, "Data" => $RequestDelete, "Msg" => "Discount deleted successfully"));
        } else {
            echo json_encode(array("Success" => false, "Data" => $RequestDelete, "Msg" => "Failed to delete discount"));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 70) {
        // Discount Management - Get All Active Discounts
        $RequestSelect = $clsfunreq->_GetAllActiveDiscounts();
        $DiscountsRes = array();
        if ($RequestSelect) {
            while ($rows = mysqli_fetch_assoc($RequestSelect)) {
                $DiscountsRes[] = array(
                    "Id" => $rows['discount_id'],
                    "Name" => $rows['discount_name'],
                    "Type" => $rows['discount_type'],
                    "Value" => $rows['discount_value'],
                    "Description" => $rows['discount_description'],
                    "Status" => $rows['status'],
                    "Created" => $rows['created']
                );
            }
            echo json_encode(array("Success" => true, "Data" => $DiscountsRes));
        } else {
            echo json_encode(array("Success" => false, "Data" => array()));
        }
    }
}
//Sales
elseif (isset($_REQUEST['SalesRequest'])) {
    if ((int) $_REQUEST['SalesRequest'] == 1) {

        $GetClientInfo = $clsfunreq->GetClientInfo();
        $GetClientInfoRes = array();
        while ($rows = mysqli_fetch_assoc($GetClientInfo)) {
            $GetClientInfoRes[] = $rows;
        }
        if ($GetClientInfo) {
            echo json_encode(array("Success" => true, "Data" => $GetClientInfoRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['SalesRequest'] == 2) {
        $BillType = $_GET['BillType'];
        $ResulQuery = $clsfunreq->selectMaxBillNo($BillType);
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($ResulQuery)) {
            $GetDataRes[] = $rows;
        }
        if ($ResulQuery) {
            echo json_encode(array("Success" => true, "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['SalesRequest'] == 3) {
        $GetMenuInfo = $clsfunreq->GetPaymodeList();
        $GetMenuInfoRes = array();
        while ($rows = mysqli_fetch_assoc($GetMenuInfo)) {
            $GetMenuInfoRes[] = $rows;
        }
        if ($GetMenuInfo) {
            echo json_encode(array("Success" => true, "Data" => $GetMenuInfoRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['SalesRequest'] == 4) { //Save Sales
        $getdtl = $_GET['dtl'];
        $gethdr = $_GET['hdr'];
        $datadtl = json_decode($getdtl, true);
        $datahdr = json_decode($gethdr, true);
        //Save Hdr

        $invoiceno = "";
        $updatePurTrno = $clsfunreq->UpdateInvoiceNo();
        if ($updatePurTrno) {
            $invoiceno = $clsfunreq->selectMaxBillNoByType("SAL");
        } else {
            $invoiceno = 0;
        }
        $psih_invoice_trno = $invoiceno;
        $psih_invoice_date = $datahdr["psih_invoice_date"];
        $psih_invoice_customerid = $datahdr["psih_invoice_customerid"];
        $psih_invoice_description = $datahdr["psih_invoice_description"];
        $psih_invoice_tqty = $datahdr["psih_invoice_tqty"];
        $psih_invoice_tamount = $datahdr["psih_invoice_tamount"];
        $psih_invoice_titemdisper = $datahdr["psih_invoice_titemdisper"];
        $psih_invoice_titemdisamt = $datahdr["psih_invoice_titemdisamt"];
        $psih_invoice_tbilldiscper = $datahdr["psih_invoice_tbilldiscper"];
        $psih_invoice_tbilldiscamt = $datahdr["psih_invoice_tbilldiscamt"];
        $psih_invoice_tgrossamt = $datahdr["psih_invoice_tgrossamt"];
        $psih_invoice_ttaxamt = $datahdr["psih_invoice_ttaxamt"];
        $psih_invoice_tnetamt = $datahdr["psih_invoice_tnetamt"];
        $psih_invoice_saletype = $datahdr["psih_invoice_saletype"];
        $psih_invoice_billtype = $datahdr["psih_invoice_billtype"];
        $psih_invoice_paymode = $datahdr["psih_invoice_paymode"];
        $psih_invoice_billstatus = $datahdr["psih_invoice_billstatus"];
        $psih_invoice_userid = $datahdr["psih_invoice_userid"];
        $psih_invoice_comid = $datahdr["psih_invoice_comid"];
        $psih_invoice_locid = $datahdr["psih_invoice_locid"];
        $psih_invoice_billremarks = $datahdr["psih_invoice_billremarks"];
        $psih_invoice_advamt = $datahdr["psih_invoice_advamt"];
        $psih_invoice_outstanding = $datahdr["psih_invoice_outstanding"];
        $psih_invoice_givenamt = $datahdr["psih_invoice_givenamt"];
        $psih_invoice_balamt = $datahdr["psih_invoice_balamt"];
        //        $saveHdr = $psih_invoice_trno . ',' . $psih_invoice_description . ',' . $psih_invoice_tqty . ',' . $psih_invoice_tamount . ',' .
        //                $psih_invoice_titemdisper . ',' . $psih_invoice_titemdisamt . ',' . $psih_invoice_tbilldiscper . ',' . $psih_invoice_tbilldiscamt . ',' . $psih_invoice_tgrossamt . ',' .
        //                $psih_invoice_ttaxamt . ',' . $psih_invoice_tnetamt . ',' . $psih_invoice_saletype . ',' . $psih_invoice_billtype . ',' . $psih_invoice_billstatus . ',' . $psih_invoice_customerid . ',' .
        //                $psih_invoice_userid . ',' . $psih_invoice_comid . ',' . $psih_invoice_locid . ',' . $psih_invoice_billremarks . ',' . $psih_invoice_advamt . ',' . $psih_invoice_outstanding . ',' .
        //                $psih_invoice_givenamt . ',' . $psih_invoice_balamt;


        $saveHdr = $clsfunreq->SaveSaleHdr(
            $psih_invoice_trno,
            $psih_invoice_date,
            $psih_invoice_description,
            $psih_invoice_tqty,
            $psih_invoice_tamount,
            $psih_invoice_titemdisper,
            $psih_invoice_titemdisamt,
            $psih_invoice_tbilldiscper,
            $psih_invoice_tbilldiscamt,
            $psih_invoice_tgrossamt,
            $psih_invoice_ttaxamt,
            $psih_invoice_tnetamt,
            $psih_invoice_saletype,
            $psih_invoice_billtype,
            $psih_invoice_billstatus,
            $psih_invoice_customerid,
            $psih_invoice_userid,
            $psih_invoice_comid,
            $psih_invoice_locid,
            $psih_invoice_billremarks,
            $psih_invoice_advamt,
            $psih_invoice_outstanding,
            $psih_invoice_givenamt,
            $psih_invoice_balamt
        );

        if ($saveHdr) {
            //Save Dtl
            $Sa_id = $clsfunreq->GetSalesId($invoiceno);
            foreach ($datadtl as $row) {
                $psid_invoice_sno = $row['SNO'];
                $psid_invoice_salid = $Sa_id;
                $psid_invoice_trno = $invoiceno;
                $psid_invoice_description = $row['ITEMNAME'];
                $psid_invoice_procode = $row['CODE'];
                $psid_invoice_proqty = $row['QTY'];
                $psid_invoice_rate = $row['RATE'];
                $psid_invoice_amt = $row['TAMOUNT'];
                $psid_invoice_itemdisp = $row['DPER'];
                $psid_invoice_itemdisamt = $row['DAMT'];
                $psid_invoice_billdisp = $row['BPER'];
                $psid_invoice_billdisamt = $row['BAMT'];
                $psid_invoice_gross = $row['GAMOUNT'];
                $psid_invoice_taxinex = $row['TAXINEX'];
                $psid_invoice_taxvalue = $row['TAXVALUE'];
                $psid_invoice_taxamt = $row['TAXAMT'];
                $psid_invoice_netamt = $row['NETAMT'];
                $saveDtl = $clsfunreq->SaveSaleDtl(
                    $psid_invoice_sno,
                    $psid_invoice_salid,
                    $psih_invoice_date,
                    $psid_invoice_trno,
                    $psid_invoice_description,
                    $psid_invoice_procode,
                    $psid_invoice_proqty,
                    $psid_invoice_rate,
                    $psid_invoice_amt,
                    $psid_invoice_itemdisp,
                    $psid_invoice_itemdisamt,
                    $psid_invoice_billdisp,
                    $psid_invoice_billdisamt,
                    $psid_invoice_gross,
                    $psid_invoice_taxinex,
                    $psid_invoice_taxvalue,
                    $psid_invoice_taxamt,
                    $psid_invoice_netamt
                );
                $resultLiveStock = $clsfunreq->_UpdateLiveStockSales($psid_invoice_procode, $psid_invoice_proqty, $psih_invoice_comid, $psih_invoice_locid);
            }
        }
        if ($saveDtl) {
            if ($psih_invoice_saletype == 'Invoice') {

                $userid = $psih_invoice_userid;
                $customerId = $psih_invoice_customerid;
                $cr = $psih_invoice_tnetamt;
                $refinvoiceno = $psih_invoice_trno;
                $billno = $clsfunreq->selectMaxVoucherNo();
                $comid = $psih_invoice_comid;
                $locid = $psih_invoice_locid;
                $customername = $psih_invoice_description;
                $pph_invdate = $psih_invoice_date;
                $deleteJourEntry = $clsfunreq->deleteJourEntryBySales($psih_invoice_trno); //$dbFunction->deleteJourEntry($psih_invoice_trno);
                if ($psih_invoice_paymode == 'cash') {
                    $ledgerid = "1";
                    $branchid = $customerId;
                    $vocheramt = $cr;
                    $accttype = "SAL";
                    $modetype = "CA";
                    $txtdatepicker = $pph_invdate;
                    $statuAcct = "A";
                    $userid = $userid;
                    $txtnarration = 'Sales :' . $refinvoiceno;
                    $ressalesentry = $clsfunreq->storeJournalSalesEntry($ledgerid, $branchid, $vocheramt, $accttype, $modetype, $txtdatepicker, $statuAcct, $userid, $txtnarration, $refinvoiceno);
                } elseif ($psih_invoice_paymode == 'credit') {
                    $ledgerid = "3";
                    $branchid = $customerId;
                    $vocheramt = $cr;
                    $accttype = "SAL";
                    $modetype = "CR";
                    $txtdatepicker = $pph_invdate;
                    $statuAcct = "NP";
                    $userid = $userid;
                    $txtnarration = 'Sales :' . $refinvoiceno;
                    $ressalesentry = $clsfunreq->storeJournalSalesEntry($ledgerid, $branchid, $vocheramt, $accttype, $modetype, $txtdatepicker, $statuAcct, $userid, $txtnarration, $refinvoiceno);
                } elseif ($psih_invoice_paymode == 'card') {
                    $ledgerid = "6";
                    $branchid = $customerId;
                    $vocheramt = $cr;
                    $accttype = "SAL";
                    $modetype = "CD";
                    $txtdatepicker = $pph_invdate;
                    $statuAcct = "P";
                    $userid = $userid;
                    $txtnarration = 'Sales :' . $refinvoiceno;
                    $ressalesentry = $clsfunreq->storeJournalSalesEntry($ledgerid, $branchid, $vocheramt, $accttype, $modetype, $txtdatepicker, $statuAcct, $userid, $txtnarration, $refinvoiceno);
                }
            }
        }

        if ($ressalesentry) {
            echo json_encode(array("Success" => true, "Data" => 'Sales Saved InvoiceNo: ' . $invoiceno));
        } else {
            echo json_encode(array("Success" => false, "Data" => "Voucher Not Updated"));
        }
    }
    if ((int) $_REQUEST['SalesRequest'] == 5) {
        $date = $_GET['date'];
        $GetSalesBill = $clsfunreq->GetSalesBill($date);
        $GetSalesBillRes = array();
        while ($rows = mysqli_fetch_assoc($GetSalesBill)) {
            $GetSalesBillRes[] = $rows;
        }
        if ($GetSalesBill) {
            echo json_encode(array("GetSalesBill" => $GetSalesBillRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['SalesRequest'] == 6) {
        $Sal_ID = $_GET['sal_id'];
        $GetSalesBillHDR = $clsfunreq->GetSalesBySalID_HDR($Sal_ID);
        $GetSalesBillHDRRes = array();
        while ($rowsHDR = mysqli_fetch_assoc($GetSalesBillHDR)) {
            $GetSalesBillHDRRes[] = $rowsHDR;
        }
        $GetSalesBillDTL = $clsfunreq->GetSalesBySalID_DTL($Sal_ID);
        $GetSalesBillDTLRes = array();
        while ($rowsDTL = mysqli_fetch_assoc($GetSalesBillDTL)) {
            $GetSalesBillDTLRes[] = $rowsDTL;
        }
        if ($GetSalesBillHDR && $GetSalesBillDTL) {
            echo json_encode(array("Success" => true, "HDR" => $GetSalesBillHDRRes, "DTL" => $GetSalesBillDTLRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['SalesRequest'] == 7) { //Update
        $getdtl = $_GET['dtl'];
        $gethdr = $_GET['hdr'];
        $datadtl = json_decode($getdtl, true);
        $datahdr = json_decode($gethdr, true);
        //        echo print_r($datadtl);
        //        echo print_r($datahdr);
        //Save Hdr
        $invoiceno = $datahdr["psih_invoice_trno"];
        $psih_invoice_trno = $invoiceno;
        $psih_invoice_date = $datahdr["psih_invoice_date"];
        $psih_invoice_customerid = $datahdr["psih_invoice_customerid"];
        $psih_invoice_description = $datahdr["psih_invoice_description"];
        $psih_invoice_tqty = $datahdr["psih_invoice_tqty"];
        $psih_invoice_tamount = $datahdr["psih_invoice_tamount"];
        $psih_invoice_titemdisper = $datahdr["psih_invoice_titemdisper"];
        $psih_invoice_titemdisamt = $datahdr["psih_invoice_titemdisamt"];
        $psih_invoice_tbilldiscper = $datahdr["psih_invoice_tbilldiscper"];
        $psih_invoice_tbilldiscamt = $datahdr["psih_invoice_tbilldiscamt"];
        $psih_invoice_tgrossamt = $datahdr["psih_invoice_tgrossamt"];
        $psih_invoice_ttaxamt = $datahdr["psih_invoice_ttaxamt"];
        $psih_invoice_tnetamt = $datahdr["psih_invoice_tnetamt"];
        $psih_invoice_saletype = $datahdr["psih_invoice_saletype"];
        $psih_invoice_billtype = $datahdr["psih_invoice_billtype"];
        $psih_invoice_billstatus = $datahdr["psih_invoice_billstatus"];
        $psih_invoice_userid = $datahdr["psih_invoice_userid"];
        $psih_invoice_comid = $datahdr["psih_invoice_comid"];
        $psih_invoice_locid = $datahdr["psih_invoice_locid"];
        $psih_invoice_billremarks = $datahdr["psih_invoice_billremarks"];
        $psih_invoice_advamt = $datahdr["psih_invoice_advamt"];
        $psih_invoice_outstanding = $datahdr["psih_invoice_outstanding"];
        $psih_invoice_givenamt = $datahdr["psih_invoice_givenamt"];
        $psih_invoice_balamt = $datahdr["psih_invoice_balamt"];
        $saveHdr = $clsfunreq->SaveSaleUpdate(
            $psih_invoice_trno,
            $psih_invoice_date,
            $psih_invoice_description,
            $psih_invoice_tqty,
            $psih_invoice_tamount,
            $psih_invoice_titemdisper,
            $psih_invoice_titemdisamt,
            $psih_invoice_tbilldiscper,
            $psih_invoice_tbilldiscamt,
            $psih_invoice_tgrossamt,
            $psih_invoice_ttaxamt,
            $psih_invoice_tnetamt,
            $psih_invoice_saletype,
            $psih_invoice_billtype,
            $psih_invoice_billstatus,
            $psih_invoice_customerid,
            $psih_invoice_userid,
            $psih_invoice_comid,
            $psih_invoice_locid,
            $psih_invoice_billremarks,
            $psih_invoice_advamt,
            $psih_invoice_outstanding,
            $psih_invoice_givenamt,
            $psih_invoice_balamt
        );
        if ($saveHdr) {
            //Save Dtl
            $Sal_ID = $clsfunreq->GetSalesId($invoiceno);
            //$clsfunreq->DeleteBySalID_DTL($Sal_ID);
            foreach ($datadtl as $row) {
                $psid_invoice_sno = $row['SNO'];
                $psid_invoice_salid = $Sal_ID;
                $psid_invoice_trno = $invoiceno;
                $psid_invoice_id = $row['UID'];
                $psid_invoice_description = $row['ITEMNAME'];
                $psid_invoice_procode = $row['CODE'];
                $psid_invoice_proqty = $row['QTY'];
                $psid_invoice_rate = $row['RATE'];
                $psid_invoice_amt = $row['TAMOUNT'];
                $psid_invoice_itemdisp = $row['DPER'];
                $psid_invoice_itemdisamt = $row['DAMT'];
                $psid_invoice_billdisp = $row['BPER'];
                $psid_invoice_billdisamt = $row['BAMT'];
                $psid_invoice_gross = $row['GAMOUNT'];
                $psid_invoice_taxinex = $row['TAXINEX'];
                $psid_invoice_taxvalue = $row['TAXVALUE'];
                $psid_invoice_taxamt = $row['TAXAMT'];
                $psid_invoice_netamt = $row['NETAMT'];
                $saveDtl = $clsfunreq->SaveSaleDtlUpdate(
                    $psid_invoice_id,
                    $psid_invoice_sno,
                    $psid_invoice_salid,
                    $psih_invoice_date,
                    $psid_invoice_trno,
                    $psid_invoice_description,
                    $psid_invoice_procode,
                    $psid_invoice_proqty,
                    $psid_invoice_rate,
                    $psid_invoice_amt,
                    $psid_invoice_itemdisp,
                    $psid_invoice_itemdisamt,
                    $psid_invoice_billdisp,
                    $psid_invoice_billdisamt,
                    $psid_invoice_gross,
                    $psid_invoice_taxinex,
                    $psid_invoice_taxvalue,
                    $psid_invoice_taxamt,
                    $psid_invoice_netamt,
                    $psih_invoice_comid,
                    $psih_invoice_locid
                );
            }
        }
        if ($saveDtl) {
            if ($psih_invoice_saletype == 'Invoice') {

                $userid = $psih_invoice_userid;
                $customerId = $psih_invoice_customerid;
                $cr = $psih_invoice_tnetamt;
                $refinvoiceno = $psih_invoice_trno;
                $comid = $psih_invoice_comid;
                $locid = $psih_invoice_locid;
                $customername = $psih_invoice_description;
                $pph_invdate = $psih_invoice_date;
                $deleteJourEntry = $clsfunreq->deleteJourEntryBySales($psih_invoice_trno); //$dbFunction->deleteJourEntry($psih_invoice_trno);

                if ($psih_invoice_paymode == 'cash') {
                    $ledgerid = "1";
                    $branchid = $customerId;
                    $vocheramt = $cr;
                    $accttype = "SAL";
                    $modetype = "CA";
                    $txtdatepicker = $pph_invdate;
                    $statuAcct = "A";
                    $userid = $userid;
                    $txtnarration = 'Sales :' . $refinvoiceno;
                    $ressalesentry = $clsfunreq->storeJournalSalesEntry($ledgerid, $branchid, $vocheramt, $accttype, $modetype, $txtdatepicker, $statuAcct, $userid, $txtnarration, $refinvoiceno);
                } elseif ($psih_invoice_paymode == 'credit') {
                    $ledgerid = "3";
                    $branchid = $customerId;
                    $vocheramt = $cr;
                    $accttype = "SAL";
                    $modetype = "CR";
                    $txtdatepicker = $pph_invdate;
                    $statuAcct = "NP";
                    $userid = $userid;
                    $txtnarration = 'Sales :' . $refinvoiceno;
                    $ressalesentry = $clsfunreq->storeJournalSalesEntry($ledgerid, $branchid, $vocheramt, $accttype, $modetype, $txtdatepicker, $statuAcct, $userid, $txtnarration, $refinvoiceno);
                } elseif ($psih_invoice_paymode == 'card') {
                    $ledgerid = "6";
                    $branchid = $customerId;
                    $vocheramt = $cr;
                    $accttype = "SAL";
                    $modetype = "CD";
                    $txtdatepicker = $pph_invdate;
                    $statuAcct = "P";
                    $userid = $userid;
                    $txtnarration = 'Sales :' . $refinvoiceno;
                    $ressalesentry = $clsfunreq->storeJournalSalesEntry($ledgerid, $branchid, $vocheramt, $accttype, $modetype, $txtdatepicker, $statuAcct, $userid, $txtnarration, $refinvoiceno);
                }
            }
        }
        if (true) {
            echo json_encode(array("Success" => true, "Data" => "Voucher Updated" . $saveDtl . ',' . $refinvoiceno . ',' . $deleteJourEntry)); //"hdr" => $saveHdr, "dtl" => $saveDtl));
        } else {
            echo json_encode(array("Success" => false, "Data" => "Voucher Not Updated" . $refinvoiceno . ',' . $deleteJourEntry)); // "hdr" => $saveHdr, "dtl" => $saveDtl));
        }
    }
    if ((int) $_REQUEST['SalesRequest'] == 8) {
        $Sal_ID = $_GET['billno'];
        $GetSalesBillHDR = $clsfunreq->GetSalesByBillno_HDR($Sal_ID);
        $GetSalesBillHDRRes = array();
        while ($rowsHDR = mysqli_fetch_assoc($GetSalesBillHDR)) {
            $GetSalesBillHDRRes[] = $rowsHDR;
        }
        $GetSalesBillDTL = $clsfunreq->GetSalesByBillno_DTL($Sal_ID);
        $GetSalesBillDTLRes = array();
        while ($rowsDTL = mysqli_fetch_assoc($GetSalesBillDTL)) {
            $GetSalesBillDTLRes[] = $rowsDTL;
        }
        if ($GetSalesBillHDR && $GetSalesBillDTL) {
            echo json_encode(array("Success" => true, "HDR" => $GetSalesBillHDRRes, "DTL" => $GetSalesBillDTLRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['SalesRequest'] == 9) { //QuoteSales
        $getdtl = $_GET['dtl'];
        $gethdr = $_GET['hdr'];
        $datadtl = json_decode($getdtl, true);
        $datahdr = json_decode($gethdr, true);
        //Save Hdr

        $invoiceno = "";
        $updatePurTrno = $clsfunreq->UpdateQuoteNo();
        if ($updatePurTrno) {
            $invoiceno = $clsfunreq->selectMaxBillNoByType("QUO");
        } else {
            $invoiceno = 0;
        }
        $psih_invoice_trno = $invoiceno;
        $psih_invoice_date = $datahdr["psih_invoice_date"];
        $psih_invoice_customerid = $datahdr["psih_invoice_customerid"];
        $psih_invoice_description = $datahdr["psih_invoice_description"];
        $psih_invoice_tqty = $datahdr["psih_invoice_tqty"];
        $psih_invoice_tamount = $datahdr["psih_invoice_tamount"];
        $psih_invoice_titemdisper = $datahdr["psih_invoice_titemdisper"];
        $psih_invoice_titemdisamt = $datahdr["psih_invoice_titemdisamt"];
        $psih_invoice_tbilldiscper = $datahdr["psih_invoice_tbilldiscper"];
        $psih_invoice_tbilldiscamt = $datahdr["psih_invoice_tbilldiscamt"];
        $psih_invoice_tgrossamt = $datahdr["psih_invoice_tgrossamt"];
        $psih_invoice_ttaxamt = $datahdr["psih_invoice_ttaxamt"];
        $psih_invoice_tnetamt = $datahdr["psih_invoice_tnetamt"];
        $psih_invoice_saletype = $datahdr["psih_invoice_saletype"];
        $psih_invoice_billtype = $datahdr["psih_invoice_billtype"];
        $psih_invoice_paymode = $datahdr["psih_invoice_paymode"];
        $psih_invoice_billstatus = $datahdr["psih_invoice_billstatus"];
        $psih_invoice_userid = $datahdr["psih_invoice_userid"];
        $psih_invoice_comid = $datahdr["psih_invoice_comid"];
        $psih_invoice_locid = $datahdr["psih_invoice_locid"];
        $psih_invoice_billremarks = $datahdr["psih_invoice_billremarks"];
        $psih_invoice_advamt = $datahdr["psih_invoice_advamt"];
        $psih_invoice_outstanding = $datahdr["psih_invoice_outstanding"];
        $psih_invoice_givenamt = $datahdr["psih_invoice_givenamt"];
        $psih_invoice_balamt = $datahdr["psih_invoice_balamt"];
        //        $saveHdr = $psih_invoice_trno . ',' . $psih_invoice_description . ',' . $psih_invoice_tqty . ',' . $psih_invoice_tamount . ',' .
        //                $psih_invoice_titemdisper . ',' . $psih_invoice_titemdisamt . ',' . $psih_invoice_tbilldiscper . ',' . $psih_invoice_tbilldiscamt . ',' . $psih_invoice_tgrossamt . ',' .
        //                $psih_invoice_ttaxamt . ',' . $psih_invoice_tnetamt . ',' . $psih_invoice_saletype . ',' . $psih_invoice_billtype . ',' . $psih_invoice_billstatus . ',' . $psih_invoice_customerid . ',' .
        //                $psih_invoice_userid . ',' . $psih_invoice_comid . ',' . $psih_invoice_locid . ',' . $psih_invoice_billremarks . ',' . $psih_invoice_advamt . ',' . $psih_invoice_outstanding . ',' .
        //                $psih_invoice_givenamt . ',' . $psih_invoice_balamt;


        $saveHdr = $clsfunreq->SaveSaleQuoteHdr(
            $psih_invoice_trno,
            $psih_invoice_date,
            $psih_invoice_description,
            $psih_invoice_tqty,
            $psih_invoice_tamount,
            $psih_invoice_titemdisper,
            $psih_invoice_titemdisamt,
            $psih_invoice_tbilldiscper,
            $psih_invoice_tbilldiscamt,
            $psih_invoice_tgrossamt,
            $psih_invoice_ttaxamt,
            $psih_invoice_tnetamt,
            $psih_invoice_saletype,
            $psih_invoice_billtype,
            $psih_invoice_billstatus,
            $psih_invoice_customerid,
            $psih_invoice_userid,
            $psih_invoice_comid,
            $psih_invoice_locid,
            $psih_invoice_billremarks,
            $psih_invoice_advamt,
            $psih_invoice_outstanding,
            $psih_invoice_givenamt,
            $psih_invoice_balamt
        );

        if ($saveHdr) {
            //Save Dtl
            $Sa_id = $clsfunreq->GetSalesQuoteId($invoiceno);
            foreach ($datadtl as $row) {
                $psid_invoice_sno = $row['SNO'];
                $psid_invoice_salid = $Sa_id;
                $psid_invoice_trno = $invoiceno;
                $psid_invoice_description = $row['ITEMNAME'];
                $psid_invoice_procode = $row['CODE'];
                $psid_invoice_proqty = $row['QTY'];
                $psid_invoice_rate = $row['RATE'];
                $psid_invoice_amt = $row['TAMOUNT'];
                $psid_invoice_itemdisp = $row['DPER'];
                $psid_invoice_itemdisamt = $row['DAMT'];
                $psid_invoice_billdisp = $row['BPER'];
                $psid_invoice_billdisamt = $row['BAMT'];
                $psid_invoice_gross = $row['GAMOUNT'];
                $psid_invoice_taxinex = $row['TAXINEX'];
                $psid_invoice_taxvalue = $row['TAXVALUE'];
                $psid_invoice_taxamt = $row['TAXAMT'];
                $psid_invoice_netamt = $row['NETAMT'];
                $saveDtl = $clsfunreq->SaveSaleQuoteDtl(
                    $psid_invoice_sno,
                    $psid_invoice_salid,
                    $psih_invoice_date,
                    $psid_invoice_trno,
                    $psid_invoice_description,
                    $psid_invoice_procode,
                    $psid_invoice_proqty,
                    $psid_invoice_rate,
                    $psid_invoice_amt,
                    $psid_invoice_itemdisp,
                    $psid_invoice_itemdisamt,
                    $psid_invoice_billdisp,
                    $psid_invoice_billdisamt,
                    $psid_invoice_gross,
                    $psid_invoice_taxinex,
                    $psid_invoice_taxvalue,
                    $psid_invoice_taxamt,
                    $psid_invoice_netamt
                );
            }
        }
        if ($saveDtl) {
            echo json_encode(array("Success" => true, "Data" => 'Sales Saved InvoiceNo: ' . $invoiceno));
        } else {
            echo json_encode(array("Success" => false, "Data" => "Voucher Not Updated"));
        }
    }
    if ((int) $_REQUEST['SalesRequest'] == 10) { //QuoteUpdate
        $getdtl = $_GET['dtl'];
        $gethdr = $_GET['hdr'];
        $datadtl = json_decode($getdtl, true);
        $datahdr = json_decode($gethdr, true);
        //        echo print_r($datadtl);
        //        echo print_r($datahdr);
        //Save Hdr
        $invoiceno = $datahdr["psih_invoice_trno"];
        $psih_invoice_trno = $invoiceno;
        $psih_invoice_date = $datahdr["psih_invoice_date"];
        $psih_invoice_customerid = $datahdr["psih_invoice_customerid"];
        $psih_invoice_description = $datahdr["psih_invoice_description"];
        $psih_invoice_tqty = $datahdr["psih_invoice_tqty"];
        $psih_invoice_tamount = $datahdr["psih_invoice_tamount"];
        $psih_invoice_titemdisper = $datahdr["psih_invoice_titemdisper"];
        $psih_invoice_titemdisamt = $datahdr["psih_invoice_titemdisamt"];
        $psih_invoice_tbilldiscper = $datahdr["psih_invoice_tbilldiscper"];
        $psih_invoice_tbilldiscamt = $datahdr["psih_invoice_tbilldiscamt"];
        $psih_invoice_tgrossamt = $datahdr["psih_invoice_tgrossamt"];
        $psih_invoice_ttaxamt = $datahdr["psih_invoice_ttaxamt"];
        $psih_invoice_tnetamt = $datahdr["psih_invoice_tnetamt"];
        $psih_invoice_saletype = $datahdr["psih_invoice_saletype"];
        $psih_invoice_billtype = $datahdr["psih_invoice_billtype"];
        $psih_invoice_billstatus = $datahdr["psih_invoice_billstatus"];
        $psih_invoice_userid = $datahdr["psih_invoice_userid"];
        $psih_invoice_comid = $datahdr["psih_invoice_comid"];
        $psih_invoice_locid = $datahdr["psih_invoice_locid"];
        $psih_invoice_billremarks = $datahdr["psih_invoice_billremarks"];
        $psih_invoice_advamt = $datahdr["psih_invoice_advamt"];
        $psih_invoice_outstanding = $datahdr["psih_invoice_outstanding"];
        $psih_invoice_givenamt = $datahdr["psih_invoice_givenamt"];
        $psih_invoice_balamt = $datahdr["psih_invoice_balamt"];
        $saveHdr = $clsfunreq->SaveSaleQuoteUpdate(
            $psih_invoice_trno,
            $psih_invoice_date,
            $psih_invoice_description,
            $psih_invoice_tqty,
            $psih_invoice_tamount,
            $psih_invoice_titemdisper,
            $psih_invoice_titemdisamt,
            $psih_invoice_tbilldiscper,
            $psih_invoice_tbilldiscamt,
            $psih_invoice_tgrossamt,
            $psih_invoice_ttaxamt,
            $psih_invoice_tnetamt,
            $psih_invoice_saletype,
            $psih_invoice_billtype,
            $psih_invoice_billstatus,
            $psih_invoice_customerid,
            $psih_invoice_userid,
            $psih_invoice_comid,
            $psih_invoice_locid,
            $psih_invoice_billremarks,
            $psih_invoice_advamt,
            $psih_invoice_outstanding,
            $psih_invoice_givenamt,
            $psih_invoice_balamt
        );
        if ($saveHdr) {
            //Save Dtl
            $Sal_ID = $clsfunreq->GetSalesQuoteId($invoiceno);
            //$clsfunreq->DeleteBySalID_DTL($Sal_ID);
            foreach ($datadtl as $row) {
                $psid_invoice_sno = $row['SNO'];
                $psid_invoice_salid = $Sal_ID;
                $psid_invoice_trno = $invoiceno;
                $psid_invoice_id = $row['UID'];
                $psid_invoice_description = $row['ITEMNAME'];
                $psid_invoice_procode = $row['CODE'];
                $psid_invoice_proqty = $row['QTY'];
                $psid_invoice_rate = $row['RATE'];
                $psid_invoice_amt = $row['TAMOUNT'];
                $psid_invoice_itemdisp = $row['DPER'];
                $psid_invoice_itemdisamt = $row['DAMT'];
                $psid_invoice_billdisp = $row['BPER'];
                $psid_invoice_billdisamt = $row['BAMT'];
                $psid_invoice_gross = $row['GAMOUNT'];
                $psid_invoice_taxinex = $row['TAXINEX'];
                $psid_invoice_taxvalue = $row['TAXVALUE'];
                $psid_invoice_taxamt = $row['TAXAMT'];
                $psid_invoice_netamt = $row['NETAMT'];
                $saveDtl = $clsfunreq->SaveSaleDtlQuoteUpdate(
                    $psid_invoice_id,
                    $psid_invoice_sno,
                    $psid_invoice_salid,
                    $psih_invoice_date,
                    $psid_invoice_trno,
                    $psid_invoice_description,
                    $psid_invoice_procode,
                    $psid_invoice_proqty,
                    $psid_invoice_rate,
                    $psid_invoice_amt,
                    $psid_invoice_itemdisp,
                    $psid_invoice_itemdisamt,
                    $psid_invoice_billdisp,
                    $psid_invoice_billdisamt,
                    $psid_invoice_gross,
                    $psid_invoice_taxinex,
                    $psid_invoice_taxvalue,
                    $psid_invoice_taxamt,
                    $psid_invoice_netamt,
                    $psih_invoice_comid,
                    $psih_invoice_locid
                );
            }
        }
        if ($saveDtl) {
            echo json_encode(array("Success" => true, "Data" => "Voucher Updated" . $saveDtl . ',' . $refinvoiceno . ',' . $deleteJourEntry)); //"hdr" => $saveHdr, "dtl" => $saveDtl));
        } else {
            echo json_encode(array("Success" => false, "Data" => "Voucher Not Updated" . $refinvoiceno . ',' . $deleteJourEntry)); // "hdr" => $saveHdr, "dtl" => $saveDtl));
        }
    }
    if ((int) $_REQUEST['SalesRequest'] == 11) {
        $date = $_GET['date'];
        $GetSalesBill = $clsfunreq->GetSalesQuoteBill($date);
        $GetSalesBillRes = array();
        while ($rows = mysqli_fetch_assoc($GetSalesBill)) {
            $GetSalesBillRes[] = $rows;
        }
        if ($GetSalesBill) {
            echo json_encode(array("GetSalesBill" => $GetSalesBillRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['SalesRequest'] == 12) {
        $Sal_ID = $_GET['sal_id'];
        $GetSalesBillHDR = $clsfunreq->GetSalesByQuoteSalID_HDR($Sal_ID);
        $GetSalesBillHDRRes = array();
        while ($rowsHDR = mysqli_fetch_assoc($GetSalesBillHDR)) {
            $GetSalesBillHDRRes[] = $rowsHDR;
        }
        $GetSalesBillDTL = $clsfunreq->GetSalesByQuoteSalID_DTL($Sal_ID);
        $GetSalesBillDTLRes = array();
        while ($rowsDTL = mysqli_fetch_assoc($GetSalesBillDTL)) {
            $GetSalesBillDTLRes[] = $rowsDTL;
        }
        if ($GetSalesBillHDR && $GetSalesBillDTL) {
            echo json_encode(array("Success" => true, "HDR" => $GetSalesBillHDRRes, "DTL" => $GetSalesBillDTLRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['SalesRequest'] == 13) {
        $Sal_ID = $_GET['billno'];
        $GetSalesBillHDR = $clsfunreq->GetSalesByQuoteBillno_HDR($Sal_ID);
        $GetSalesBillHDRRes = array();
        while ($rowsHDR = mysqli_fetch_assoc($GetSalesBillHDR)) {
            $GetSalesBillHDRRes[] = $rowsHDR;
        }
        $GetSalesBillDTL = $clsfunreq->GetSalesByQuoteBillno_DTL($Sal_ID);
        $GetSalesBillDTLRes = array();
        while ($rowsDTL = mysqli_fetch_assoc($GetSalesBillDTL)) {
            $GetSalesBillDTLRes[] = $rowsDTL;
        }
        if ($GetSalesBillHDR && $GetSalesBillDTL) {
            echo json_encode(array("Success" => true, "HDR" => $GetSalesBillHDRRes, "DTL" => $GetSalesBillDTLRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
}
//Account
elseif (isset($_REQUEST['AccountRequest'])) {
    //Group entry start 30
    if ((int) $_REQUEST['AccountRequest'] == 1) {
        $getdtl = $_GET['json'];
        $datadtl = json_decode($getdtl, true);
        $groupid = $datadtl['id'];
        $groupName = $datadtl['groupname'];
        $strval = $groupName;
        $res = $clsfunreq->storegroupData($groupName);
        // $response = array("type" => "0","message" =>$strval);
        // echo json_encode($response);
        if ($res) {
            $response = array("Success" => true, "message" => 'Data Saved');
            echo json_encode($response);
        } else {
            $response = array("Success" => false, "message" => "Data Not Saved");
            echo json_encode($response);
        }
    }
    if ((int) $_REQUEST['AccountRequest'] == 2) {
        $getdtl = $_GET['json'];
        $datadtl = json_decode($getdtl, true);
        $groupid = $datadtl['id'];
        $groupName = $datadtl['groupname'];
        $strval = $groupName;
        $res = $clsfunreq->updategroupData($groupid, $groupName);
        // $response = array("type" => "0","message" =>$strval);
        // echo json_encode($response);
        if ($res) {
            $response = array("Success" => true, "message" => 'Data Saved');
            echo json_encode($response);
        } else {
            $response = array("Success" => false, "message" => "Data Not Saved");
            echo json_encode($response);
        }
    }
    if ((int) $_REQUEST['AccountRequest'] == 3) {
        $id = $_GET['id'];
        $res = $clsfunreq->selectgroupById($id);
        if ($res) {
            $response = array("Success" => true, "message" => 'Data Saved');
            echo json_encode($response);
        } else {
            $response = array("Success" => false, "message" => "Data Not Saved");
            echo json_encode($response);
        }
    }
    if ((int) $_REQUEST['AccountRequest'] == 4) {
        $res = $clsfunreq->selectgroup();
        $GetSalesBillHDRRes = array();
        while ($rowsHDR = mysqli_fetch_assoc($res)) {
            $GetSalesBillHDRRes[] = $rowsHDR;
        }

        if ($res) {
            $response = array("Success" => true, "Data" => $GetSalesBillHDRRes);
            echo json_encode($response);
        } else {
            $response = array("Success" => false, "message" => "Data Not Saved");
            echo json_encode($response);
        }
    }

    //Parent Group
    if ((int) $_REQUEST['AccountRequest'] == 5) {
        $getdtl = $_GET['json'];
        $datadtl = json_decode($getdtl, true);
        $groupid = $datadtl['id'];
        $parentName = $datadtl['parentname'];
        $strval = $parentName;
        $res = $clsfunreq->storeparentData($parentName);
        // $response = array("type" => "0","message" =>$strval);
        // echo json_encode($response);
        if ($res) {
            $response = array("Success" => true, "message" => 'Data Saved');
            echo json_encode($response);
        } else {
            $response = array("Success" => false, "message" => "Data Not Saved");
            echo json_encode($response);
        }
    }
    if ((int) $_REQUEST['AccountRequest'] == 6) {
        $getdtl = $_GET['json'];
        $datadtl = json_decode($getdtl, true);
        $id = $datadtl['id'];
        $parentName = $datadtl['parentname'];
        $strval = $parentName;
        $res = $clsfunreq->updateparentData($id, $parentName);
        // $response = array("type" => "0","message" =>$strval);
        // echo json_encode($response);
        if ($res) {
            $response = array("Success" => true, "message" => 'Data Saved');
            echo json_encode($response);
        } else {
            $response = array("Success" => false, "message" => "Data Not Saved");
            echo json_encode($response);
        }
    }
    if ((int) $_REQUEST['AccountRequest'] == 7) {
        $id = $_GET['id'];
        $res = $clsfunreq->selectparentById($id);
        if ($res) {
            $response = array("Success" => true, "message" => 'Data Saved');
            echo json_encode($response);
        } else {
            $response = array("Success" => false, "message" => "Data Not Saved");
            echo json_encode($response);
        }
    }
    if ((int) $_REQUEST['AccountRequest'] == 8) {
        $res = $clsfunreq->selectparent();
        $GetSalesBillHDRRes = array();
        while ($rowsHDR = mysqli_fetch_assoc($res)) {
            $GetSalesBillHDRRes[] = $rowsHDR;
        }

        if ($res) {
            $response = array("Success" => true, "Data" => $GetSalesBillHDRRes);
            echo json_encode($response);
        } else {
            $response = array("Success" => false, "message" => "Data Not Saved");
            echo json_encode($response);
        }
    }
    //Ledger Master
    if ((int) $_REQUEST['AccountRequest'] == 9) {
        $getdtl = $_GET['json'];
        $datadtl = json_decode($getdtl, true);
        $ledgerrefId = 0;
        $ledgerName = $datadtl['ledgerName'];
        $ledgerparenId = $datadtl['ledgerparenId'];
        $ledgergroupId = $datadtl['ledgergroupId'];
        $ledgerType = $datadtl['ledgerType'];
        $ledgeropenDate = $datadtl['ledgeropenDate'];
        $ledgeropenbal = $datadtl['ledgeropenbal'];
        $ledgerdrcr = $datadtl['ledgerdrcr'];
        $ledgerActive = $datadtl['ledgerActive'];
        $res = $clsfunreq->storeLedgerData($ledgerrefId, $ledgerName, $ledgerparenId, $ledgergroupId, $ledgerType, $ledgeropenDate, $ledgeropenbal, $ledgerdrcr, $ledgerActive);
        // $response = array("type" => "0","message" =>$strval);
        // echo json_encode($response);
        if ($res) {
            $response = array("Success" => true, "Data" => 'Data Saved');
            echo json_encode($response);
        } else {
            $response = array("Success" => false, "message" => "Data Not Saved");
            echo json_encode($response);
        }
    }
    if ((int) $_REQUEST['AccountRequest'] == 10) {
        $getdtl = $_GET['json'];
        $datadtl = json_decode($getdtl, true);
        $ledgerId = $datadtl['ledgerId'];
        $ledgerrefId = 0;
        $ledgerName = $datadtl['ledgerName'];
        $ledgerparenId = $datadtl['ledgerparenId'];
        $ledgergroupId = $datadtl['ledgergroupId'];
        $ledgerType = $datadtl['ledgerType'];
        $ledgeropenDate = $datadtl['ledgeropenDate'];
        $ledgeropenbal = $datadtl['ledgeropenbal'];
        $ledgerdrcr = $datadtl['ledgerdrcr'];
        $ledgerActive = $datadtl['ledgerActive'];
        $res = $clsfunreq->updateLedgerData($ledgerId, $ledgerrefId, $ledgerName, $ledgerparenId, $ledgergroupId, $ledgerType, $ledgeropenDate, $ledgeropenbal, $ledgerdrcr, $ledgerActive);
        // $response = array("type" => "0","message" =>$strval);
        // echo json_encode($response);
        if ($res) {
            $response = array("Success" => true, "Data" => 'Data Saved');
            echo json_encode($response);
        } else {
            $response = array("Success" => false, "message" => "Data Not Saved");
            echo json_encode($response);
        }
    }
    if ((int) $_REQUEST['AccountRequest'] == 11) {
        $res = $clsfunreq->selectledger();
        $GetSalesBillHDRRes = array();
        while ($rows = mysqli_fetch_assoc($res)) {
            $GetSalesBillHDRRes[] = $rows;
        }

        if ($res) {
            $response = array("Success" => true, "Data" => $GetSalesBillHDRRes);
            echo json_encode($response);
        } else {
            $response = array("Success" => false, "message" => "Data Not Saved");
            echo json_encode($response);
        }
    }
    if ((int) $_REQUEST['AccountRequest'] == 12) {
        $Getsupplier = $clsfunreq->bankList();
        $GetsupplierRes = array();
        while ($rows = mysqli_fetch_assoc($Getsupplier)) {
            $GetsupplierRes[] = $rows;
        }
        if ($Getsupplier) {
            echo json_encode(array("Success" => true, "Data" => $GetsupplierRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
}
//Account Payment //Receipts
elseif (isset($_REQUEST['AjaxPayRec'])) {
    if ((int) $_REQUEST['AjaxPayRec'] == 1) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $jid = $row["jid"];
        $ledgerid = $row["ledgerid"];
        $description = $row["description"];
        $dr = $row["dr"];
        $cr = $row["cr"];
        $jstatus = $row["jstatus"];
        $billno = $row["billno"];
        $entrydate = $row["entrydate"];
        $actype = $row["actype"];
        $modetype = $row["modetype"];
        $narration = $row["narration"];
        $status = $row["status"];
        $username = $row["username"];
        $ledgerid2 = $row["ledgerid2"];
        $description2 = $row["description2"];
        $comid = $row["comid"];
        $locid = $row["locid"];
        $bankname = $row["bankname"];
        $chequeamt = $row["dr"];
        $chequedate = $row["chqdate"];
        $chequeno = $row["chqno"];
        if ($billno == 0) {
            $autobillno = $clsfunreq->selectMaxVoucherNo();
        } else {
            $autobillno = $billno;
            $del = $clsfunreq->DeleteJournalEntry($autobillno);
        }

        //echo json_encode(array("Success" => true, "BillNo" => $autobillno));
        if ($actype == "PAY") {
            //New
            $res = $clsfunreq->storeJournalpayments(
                $ledgerid,
                $description,
                $dr,
                $cr,
                $jstatus,
                $autobillno,
                $entrydate,
                $actype,
                $modetype,
                $narration,
                $status,
                $username,
                $description2,
                $ledgerid2,
                $bankname,
                $chequeamt,
                $chequedate,
                $chequeno,
                $comid,
                $locid
            );
            if ($res) {
                echo json_encode(array("Success" => true, "Data" => $res));
            } else {
                echo json_encode(array("Success" => false, "Data" => $autobillno));
            }
            //echo json_encode(array("Success" => true,"BillNo"=>$res));
        } elseif ($actype == "JUR") {
            //New
            $res = $clsfunreq->storeJournal(
                $ledgerid,
                $description,
                $dr,
                $cr,
                $jstatus,
                $autobillno,
                $entrydate,
                $actype,
                $modetype,
                $narration,
                $status,
                $username,
                $description2,
                $ledgerid2,
                $bankname,
                $chequeamt,
                $chequedate,
                $chequeno,
                $comid,
                $locid
            );
            if ($res) {
                echo json_encode(array("Success" => true, "Data" => $autobillno));
            } else {
                echo json_encode(array("Success" => false, "Data" => $autobillno));
            }
            //echo json_encode(array("Success" => true,"BillNo"=>$res));
        } elseif ($actype == "REC") {
            //New
            $res = $clsfunreq->storeJournalReceipt(
                $ledgerid,
                $description,
                $dr,
                $cr,
                $jstatus,
                $autobillno,
                $entrydate,
                $actype,
                $modetype,
                $narration,
                $status,
                $username,
                $description2,
                $ledgerid2,
                $bankname,
                $chequeamt,
                $chequedate,
                $chequeno,
                $comid,
                $locid
            );
            if ($res) {
                echo json_encode(array("Success" => true, "Data" => $autobillno));
            } else {
                echo json_encode(array("Success" => false, "Data" => $autobillno));
            }
            //echo json_encode(array("Success" => true,"BillNo"=>$res));
        }
    }
    if ((int) $_REQUEST['AjaxPayRec'] == 2) {
        $date = $_GET['date'];
        $GetMenuInfo = $clsfunreq->GETJournalEntry($date);
        $GetMenuInfoRes = array();
        while ($rows = mysqli_fetch_assoc($GetMenuInfo)) {
            $GetMenuInfoRes[] = $rows;
        }
        if ($GetMenuInfo) {
            echo json_encode(array("Journal" => $GetMenuInfoRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxPayRec'] == 3) {
        $billno = $_GET['billno'];
        $GetMenuInfo = $clsfunreq->GETJournalEntryByBillNo($billno);
        $GetMenuInfoRes = array();
        while ($rows = mysqli_fetch_assoc($GetMenuInfo)) {
            $GetMenuInfoRes[] = $rows;
        }
        if ($GetMenuInfo) {
            echo json_encode(array("Journal" => $GetMenuInfoRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxPayRec'] == 4) {
        $Getsupplier = $clsfunreq->GetLedgerAll();
        $GetsupplierRes = array();
        while ($rows = mysqli_fetch_assoc($Getsupplier)) {
            $GetsupplierRes[] = $rows;
        }
        if ($Getsupplier) {
            echo json_encode(array("BankListTable" => $GetsupplierRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxPayRec'] == 5) {
        $fromdate = $_GET['fromdate'];
        $todate = $_GET['todate'];
        $ledgerid = $_GET['ledgerid'];
        $Getsupplier = $clsfunreq->GETJournalEntryByID($fromdate, $todate, $ledgerid);
        $GetsupplierRes = array();
        while ($rows = mysqli_fetch_assoc($Getsupplier)) {
            $GetsupplierRes[] = $rows;
        }
        if ($Getsupplier) {
            echo json_encode(array("BankListTable" => $GetsupplierRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxPayRec'] == 6) {
        $billno = $_GET['billno'];
        $Getsupplier = $clsfunreq->DeleteJournalEntry($billno);
        if ($Getsupplier) {
            echo json_encode(array("Success" => true, "Msg" => 'Voucher No Has Been Deleted : ' . $billno));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxPayRec'] == 7) {
        $ledgerId = $_GET['ledgerId'];
        $Getsupplier = $clsfunreq->GetClsBalance($ledgerId);
        if ($Getsupplier) {
            echo json_encode(array("Success" => true, "Data" => $Getsupplier));
        } else {
            echo json_encode(array("Success" => false, "Data" => 'No Data Found'));
        }
    }
}
//EmployeeReq
elseif (isset($_REQUEST['EmployeeReq'])) {
    if ((int) $_REQUEST['EmployeeReq'] == 1) { //Get Company
        $getdtl = $_GET['json'];
        $datadtl = json_decode($getdtl, true);
        $emp_firstname = $datadtl['emp_firstname'];
        $emp_lastname = $datadtl['emp_lastname'];
        $emp_printname = $datadtl['emp_printname'];
        $emp_idtype = $datadtl['emp_idtype'];
        $emp_passportic = $datadtl['emp_passportic'];
        $emp_nationality = $datadtl['emp_nationality'];
        $emp_passexpire = $datadtl['emp_passexpire'];
        $emp_visaexpire = $datadtl['emp_visaexpire'];
        $emp_joindate = $datadtl['emp_joindate'];
        $emp_resigndate = $datadtl['emp_resigndate'];
        $emp_contactno = $datadtl['emp_contactno'];
        $emp_contactname = $datadtl['emp_contactname'];
        $emp_emergencyno = $datadtl['emp_emergencyno'];
        $emp_compid = $datadtl['emp_compid'];
        $emp_locid = $datadtl['emp_locid'];
        $emp_designation = $datadtl['emp_designation'];
        $emp_bankname = $datadtl['emp_bankname'];
        $emp_accountname = $datadtl['emp_accountname'];
        $emp_accountno = $datadtl['emp_accountno'];
        $emp_image = $datadtl['emp_image'];
        $emp_basicsalary = $datadtl['emp_basicsalary'];
        $emp_basicrate = $datadtl['emp_basicrate'];
        $emp_otrate = $datadtl['emp_otrate'];
        $emp_othrsrate = $datadtl['emp_othrsrate'];
        $emp_allowance = $datadtl['emp_allowance'];
        $emp_currentstatus = $datadtl['emp_currentstatus'];
        $emp_remarks = $datadtl['emp_remarks'];
        $emp_active = $datadtl['emp_active'];
        $emp_epf = $datadtl['emp_epf'];
        $emp_socso = $datadtl['emp_socso'];
        $emp_dob = $datadtl['emp_dob'];
        $emp_monthexpire = $datadtl['emp_monthexpire'];
        $emp_curpermit = $datadtl['emp_curpermit'];
        $emp_nextpermit = $datadtl['emp_nextpermit'];
        $SaveEmpDataId = $clsfunreq->SaveEmpData(
            $emp_firstname,
            $emp_lastname,
            $emp_printname,
            $emp_idtype,
            $emp_passportic,
            $emp_nationality,
            $emp_passexpire,
            $emp_visaexpire,
            $emp_joindate,
            $emp_resigndate,
            $emp_contactno,
            $emp_contactname,
            $emp_emergencyno,
            $emp_compid,
            $emp_locid,
            $emp_designation,
            $emp_bankname,
            $emp_accountname,
            $emp_accountno,
            $emp_image,
            $emp_basicsalary,
            $emp_basicrate,
            $emp_otrate,
            $emp_othrsrate,
            $emp_allowance,
            $emp_currentstatus,
            $emp_remarks,
            $emp_active,
            $emp_epf,
            $emp_socso,
            $emp_dob,
            $emp_monthexpire,
            $emp_curpermit,
            $emp_nextpermit
        );
        $createLedgerEmployee = $clsfunreq->saveEmployeeLedgerData($SaveEmpDataId);
        if ($createLedgerEmployee) {
            echo json_encode(array("Success" => true, "Msg" => "Data Saved", "Data" => "New Record Id:" . $SaveEmpDataId));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['EmployeeReq'] == 2) { //Get Company
        $getdtl = $_GET['json'];
        $datadtl = json_decode($getdtl, true);
        $emp_id = $datadtl['emp_id'];
        $emp_firstname = $datadtl['emp_firstname'];
        $emp_lastname = $datadtl['emp_lastname'];
        $emp_printname = $datadtl['emp_printname'];
        $emp_idtype = $datadtl['emp_idtype'];
        $emp_passportic = $datadtl['emp_passportic'];
        $emp_nationality = $datadtl['emp_nationality'];
        $emp_passexpire = $datadtl['emp_passexpire'];
        $emp_visaexpire = $datadtl['emp_visaexpire'];
        $emp_joindate = $datadtl['emp_joindate'];
        $emp_resigndate = $datadtl['emp_resigndate'];
        $emp_contactno = $datadtl['emp_contactno'];
        $emp_contactname = $datadtl['emp_contactname'];
        $emp_emergencyno = $datadtl['emp_emergencyno'];
        $emp_compid = $datadtl['emp_compid'];
        $emp_locid = $datadtl['emp_locid'];
        $emp_designation = $datadtl['emp_designation'];
        $emp_bankname = $datadtl['emp_bankname'];
        $emp_accountname = $datadtl['emp_accountname'];
        $emp_accountno = $datadtl['emp_accountno'];
        $emp_image = $datadtl['emp_image'];
        $emp_basicsalary = $datadtl['emp_basicsalary'];
        $emp_basicrate = $datadtl['emp_basicrate'];
        $emp_otrate = $datadtl['emp_otrate'];
        $emp_othrsrate = $datadtl['emp_othrsrate'];
        $emp_allowance = $datadtl['emp_allowance'];
        $emp_currentstatus = $datadtl['emp_currentstatus'];
        $emp_remarks = $datadtl['emp_remarks'];
        $emp_active = $datadtl['emp_active'];
        $emp_epf = $datadtl['emp_epf'];
        $emp_socso = $datadtl['emp_socso'];
        $emp_dob = $datadtl['emp_dob'];
        $emp_monthexpire = $datadtl['emp_monthexpire'];
        $emp_curpermit = $datadtl['emp_curpermit'];
        $emp_nextpermit = $datadtl['emp_nextpermit'];
        $SaveEmpData = $clsfunreq->UpdateEmpData(
            $emp_id,
            $emp_firstname,
            $emp_lastname,
            $emp_printname,
            $emp_idtype,
            $emp_passportic,
            $emp_nationality,
            $emp_passexpire,
            $emp_visaexpire,
            $emp_joindate,
            $emp_resigndate,
            $emp_contactno,
            $emp_contactname,
            $emp_emergencyno,
            $emp_compid,
            $emp_locid,
            $emp_designation,
            $emp_bankname,
            $emp_accountname,
            $emp_accountno,
            $emp_image,
            $emp_basicsalary,
            $emp_basicrate,
            $emp_otrate,
            $emp_othrsrate,
            $emp_allowance,
            $emp_currentstatus,
            $emp_remarks,
            $emp_active,
            $emp_epf,
            $emp_socso,
            $emp_dob,
            $emp_monthexpire,
            $emp_curpermit,
            $emp_nextpermit
        );
        if ($SaveEmpData) {
            echo json_encode(array("Success" => true, "Msg" => $SaveEmpData));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['EmployeeReq'] == 3) {
        $Data = $_GET['json'];
        $GetEmployeeView = $clsfunreq->SelectEmpDataByView();
        $GetEmployeeViewRes = array();
        while ($rows = mysqli_fetch_assoc($GetEmployeeView)) {
            $GetEmployeeViewRes[] = $rows;
        }
        if ($GetEmployeeView) {
            echo json_encode(array("Success" => true, "Data" => $GetEmployeeViewRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['EmployeeReq'] == 4) {
        $emp_id = $_GET['json'];
        $GetEmployeeView = $clsfunreq->SelectEmpDataByID($emp_id);
        $GetEmployeeViewRes = array();
        while ($rows = mysqli_fetch_assoc($GetEmployeeView)) {
            $GetEmployeeViewRes[] = $rows;
        }
        if ($GetEmployeeView) {
            echo json_encode(array("Success" => true, "Data" => $GetEmployeeViewRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['EmployeeReq'] == 5) {
        $ImageData = $_POST['Image'];
        $EmpId = $_POST['EmpId'];
        $SaveEmpData = $clsfunreq->SaveEmpImage($ImageData, $EmpId);
        if ($SaveEmpData) {
            echo json_encode(array("Success" => true, "Data" => $SaveEmpData));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['EmployeeReq'] == 6) { //UploadImage
        //echo 'Test Page' . $_POST['EmployeeReq'];
        $data = json_decode(file_get_contents("php://input"), true);
        if (isset($data['file']) && isset($data['filename']) && isset($data['EmployeeId'])) {
            // Get the Base64 string and decode it
            $fileData = $data['file'];
            $filename = $data['filename'];
            $EmployeeId = $data['EmployeeId'];

            // Decode the Base64 string
            $decodedData = base64_decode($fileData);

            // Set the upload directory (make sure it has write permissions)
            $uploadDir = '../employee/' . $EmployeeId . '/';
            if (!is_dir($uploadDir)) {
                mkdir($uploadDir, 0755, true);
            }

            // Save the file
            $filePath = $uploadDir . basename($filename);
            if (file_put_contents($filePath, $decodedData)) {
                echo json_encode(["success" => true, "message" => "File uploaded successfully.", "filePath" => $filePath]);
            } else {
                echo json_encode(["success" => false, "message" => "Failed to save the file."]);
            }
        } else {
            echo json_encode(["success" => false, "message" => "Invalid input data."]);
        }
    }
    if ((int) $_REQUEST['EmployeeReq'] == 7) { //getfiles
        $EmployeeId = $_GET['EmployeeId'];
        $arrFiles = array();
        $dirPath = '../employee/' . $EmployeeId;
        $files = scandir($dirPath);
        $GetPurRes = array();
        foreach ($files as $file) {
            $filePath = $dirPath . '/' . $file;
            if (is_file($filePath)) {
                $strPass = $SuppCode . '/' . $TrId . '/' . $file;
                $GetPurRes[] = $strPass;
            }
        }
        if (true) {
            echo json_encode(array("Success" => true, "Msg" => 'Data Received', "Data" => $GetPurRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Found'));
        }
    }
    if ((int) $_REQUEST['EmployeeReq'] == 8) { //Delete Files
        $path = $_GET["path"];
        $dirPath = '../employee/' . $path;
        if (unlink($dirPath)) {
            echo json_encode(array("Success" => true, "Msg" => 'File Deleted', "Data" => $dirPath));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'File Not Found'));
        }
    }
    if ((int) $_REQUEST['EmployeeReq'] == 9) { //get employee salary history
        $EmployeeId = $_GET['EmployeeId'];
        $GetEmployeeView = $clsfunreq->GetEmployeeSalaryHistoryById($EmployeeId);
        $GetEmployeeViewRes = array();
        while ($rows = mysqli_fetch_assoc($GetEmployeeView)) {
            $GetEmployeeViewRes[] = $rows;
        }
        if ($GetEmployeeView) {
            echo json_encode(array("Success" => true, "Data" => $GetEmployeeViewRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }

    if ((int) $_REQUEST['EmployeeReq'] == 10) { //Save employee salary history
        $getdtl = $_GET['json'];
        $datadtl = json_decode($getdtl, true);
        $pes_empid = $datadtl['pes_empid'];
        $pes_oldsalary = $datadtl['pes_oldsalary'];
        $pes_newsalary = $datadtl['pes_newsalary'];
        $pes_userid = $datadtl['pes_userid'];
        $pes_remarks = $datadtl['pes_remarks'];
        $GetEmployeeView = $clsfunreq->SaveEmployeeSalaryHistory($pes_empid, $pes_oldsalary, $pes_newsalary, $pes_userid, $pes_remarks);
        if ($GetEmployeeView) {
            echo json_encode(array("Success" => true, "Data" => "Salary Updated"));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['EmployeeReq'] == 11) { //select month
        $GetEmployee = $clsfunreq->GetMonthofsalary();
        $GetEmployeeRes = array();
        while ($rows = mysqli_fetch_assoc($GetEmployee)) {
            $GetEmployeeRes[] = $rows;
        }
        if ($GetEmployee) {
            echo json_encode(array("Success" => true, "Msg" => 'Data Received', "Data" => $GetEmployeeRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Received'));
        }
    }
    if ((int) $_REQUEST['EmployeeReq'] == 12) { //Save month
        $getdtl = $_GET['json'];
        $datadtl = json_decode($getdtl, true);
        $pems_monthname = $datadtl['pems_monthname'];
        $pems_active = $datadtl['pems_active'];
        $GetEmployeeView = $clsfunreq->SaveMonthofsalary($pems_monthname, $pems_active);
        if ($GetEmployeeView) {
            echo json_encode(array("Success" => true, "Data" => "Month Updated"));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['EmployeeReq'] == 13) { //Update month
        $getdtl = $_GET['json'];
        $datadtl = json_decode($getdtl, true);
        $pems_id = $datadtl['pems_id'];
        $pems_monthname = $datadtl['pems_monthname'];
        $pems_active = $datadtl['pems_active'];
        $GetEmployeeView = $clsfunreq->UpdateMonthofsalary($pems_id, $pems_monthname, $pems_active);
        if ($GetEmployeeView) {
            echo json_encode(array("Success" => true, "Data" => "Month Updated"));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['EmployeeReq'] == 14) { //Check month
        $getdtl = $_GET['json'];
        $datadtl = json_decode($getdtl, true);
        $pemp_month = $datadtl['pemp_month'];
        $pemp_comid = $datadtl['pemp_comid'];
        $pemp_locid = $datadtl['pemp_locid'];
        $GetResults = $clsfunreq->CheckMonthofsalary($pemp_month, $pemp_comid, $pemp_locid);
        $row = (mysqli_fetch_assoc($GetResults));
        $DataRow = $row['Counts'];
        if ($DataRow == 0) {
            $GetEmployee = $clsfunreq->GetEmpMonthofsalary($pemp_comid, $pemp_locid);
            $GetEmployeeRes = array();
            while ($rows = mysqli_fetch_assoc($GetEmployee)) {
                $GetEmployeeRes[] = $rows;
            }
            if ($GetEmployee) {
                echo json_encode(array("Success" => true, "Msg" => $DataRow, "Data" => $GetEmployeeRes));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Data Not Found', "Data" => $DataRow));
            }
        } else {
            $GetEmployee = $clsfunreq->GetEmpOldMonthofsalary($pemp_comid, $pemp_locid, $pemp_month);
            $GetEmployeeRes = array();
            while ($rows = mysqli_fetch_assoc($GetEmployee)) {
                $GetEmployeeRes[] = $rows;
            }
            if ($GetEmployee) {
                echo json_encode(array("Success" => true, "Msg" => $DataRow, "Data" => $GetEmployeeRes));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Data Not Found', "Data" => $DataRow));
            }
        }
    }

    if ((int) $_REQUEST['EmployeeReq'] == 15) { //Save Month
        $getdtl = $_GET['json'];
        $rowDtl = json_decode($getdtl, true);
        foreach ($rowDtl as $row) {
            $pemp_id = $row['EmpTrId'];
            $pemp_refid = $row['EmpRefId'];
            $pemp_month = $row['EmpMonth'];
            $pemp_comid = $row['EmpComId'];
            $pemp_locid = $row['EmpLocId'];
            $pemp_noofdays = $row['EmpNoOfDays'];
            $pemp_extradays = $row['EmpExtraDays'];
            $pemp_extrahrs = $row['EmpExtraOtHrs'];
            $pemp_advance = $row['EmpAdvance'];
            $pemp_deduction = $row['EmpDeduction'];
            $pemp_bankin = $row['EmpBankIn'];
            //$data .= $pemp_refid.','. $pemp_month.','. $pemp_comid.','. $pemp_locid.','. $pemp_noofdays.','. $pemp_extradays.','. $pemp_extrahrs.','.$pemp_advance;
            //1,Oct-2024,1,1,30,1,2,1002,Oct-2024,1,1,30,2,5,200
            if ($pemp_id == 0) {
                $savemonthempatt = $clsfunreq->SaveMonthEmpAttendance($pemp_refid, $pemp_month, $pemp_comid, $pemp_locid, $pemp_noofdays, $pemp_extradays, $pemp_extrahrs, $pemp_advance, $pemp_deduction, $pemp_bankin);
            } else {
                $deleteOld = $clsfunreq->DeleteMonthEmpAttendance($pemp_id);
                if ($deleteOld) {
                    $savemonthempatt = $clsfunreq->SaveMonthEmpAttendance($pemp_refid, $pemp_month, $pemp_comid, $pemp_locid, $pemp_noofdays, $pemp_extradays, $pemp_extrahrs, $pemp_advance, $pemp_deduction, $pemp_bankin);
                }
            }
        }
        if (true) {
            echo json_encode(array("Success" => true, "Msg" => "Saved", "Data" => $data));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Data Not Found', "Data" => $savemonthempatt));
        }
    }
    if ((int) $_REQUEST['EmployeeReq'] == 16) { //Final Save Month
        $getdtl = $_GET['json'];
        $rowDtl = json_decode($getdtl, true);
        foreach ($rowDtl as $row) {
            $pef_id = $row['EmpTrId'];
            $pef_refid = $row['EmpRefId'];
            $pef_month = $row['EmpMonth'];
            $pef_comid = $row['EmpComId'];
            $pef_locid = $row['EmpLocId'];
            $pef_basicsalary = $row['EmpBasic'];
            $pef_workingdays = $row['EmpNoOfDays'];
            $pef_wages = $row['EmpWages'];
            $pef_extraday = $row['EmpExtraDays'];
            $pef_extradayamt = $row['EmpExtraDayAmt'];
            $pef_extrahours = $row['EmpExtraOtHrs'];
            $pef_extrahrsamt = $row['EmpExtraOtAmt'];
            $pef_allowance = $row['EmpAllowance'];
            $pef_grossamt = $row['EmpGrossAmt'];
            $pef_advance = $row['EmpAdvance'];
            $pef_epf = $row['EmpEpf'];
            $pef_socso = $row['EmpSocso'];
            $pef_deduction = $row['EmpDeduction'];
            $pef_netpay = $row['EmpNetPay'];
            $pef_bank = $row['EmpBank'];
            $pef_netcash = $row['EmpNetCash'];

            //            $data = $pef_refid . ',' . $pef_comid . ',' . $pef_locid . ',' . $pef_month . ',' .
            //                    $pef_basicsalary . ',' . $pef_workingdays . ',' . $pef_wages . ',' . $pef_extraday . ',' . $pef_extradayamt . ',' .
            //                    $pef_extrahours . ',' . $pef_extrahrsamt . ',' . $pef_allowance . ',' . $pef_grossamt . ',' . $pef_advance . ',' .
            //                    $pef_epf . ',' . $pef_socso . ',' . $pef_deduction . ',' . $pef_netpay . ',' . $pef_bank . ',' . $pef_netcash;
            //2,1,1,m,10000,30,9999.9,1,333.33,,138.9,200,10672.13,,10672.13,200,18,10254.13,0,10254.13
            if ($pef_id == 0) {
                $savemonthempatt = $clsfunreq->SaveFinalProcess(
                    $pef_refid,
                    $pef_comid,
                    $pef_locid,
                    $pef_month,
                    $pef_basicsalary,
                    $pef_workingdays,
                    $pef_wages,
                    $pef_extraday,
                    $pef_extradayamt,
                    $pef_extrahours,
                    $pef_extrahrsamt,
                    $pef_allowance,
                    $pef_grossamt,
                    $pef_advance,
                    $pef_epf,
                    $pef_socso,
                    $pef_deduction,
                    $pef_netpay,
                    $pef_bank,
                    $pef_netcash
                );
            } else {
                $deleteOld = $clsfunreq->DeleteFinalProcess($pef_id);
                if ($deleteOld) {
                    $savemonthempatt = $clsfunreq->SaveFinalProcess(
                        $pef_refid,
                        $pef_comid,
                        $pef_locid,
                        $pef_month,
                        $pef_basicsalary,
                        $pef_workingdays,
                        $pef_wages,
                        $pef_extraday,
                        $pef_extradayamt,
                        $pef_extrahours,
                        $pef_extrahrsamt,
                        $pef_allowance,
                        $pef_grossamt,
                        $pef_advance,
                        $pef_epf,
                        $pef_socso,
                        $pef_deduction,
                        $pef_netpay,
                        $pef_bank,
                        $pef_netcash
                    );
                }
            }
        }
        if ($savemonthempatt) {
            echo json_encode(array("Success" => true, "Msg" => "Saved", "Data" => $savemonthempatt));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Data Not Found', "Data" => $savemonthempatt));
        }
    }
    if ((int) $_REQUEST['EmployeeReq'] == 17) { //Select Final Process
        $getdtl = $_GET['json'];
        $datadtl = json_decode($getdtl, true);
        $pemp_month = $datadtl['pemp_month'];
        $pemp_comid = $datadtl['pemp_comid'];
        $pemp_locid = $datadtl['pemp_locid'];
        $GetResults = $clsfunreq->CheckMonthofFinal($pemp_month, $pemp_comid, $pemp_locid);
        $row = (mysqli_fetch_assoc($GetResults));
        $DataRow = $row['Counts'];
        if ($DataRow == 0) {
            $GetEmployee = $clsfunreq->SelectFinalProcess($pemp_month, $pemp_comid, $pemp_locid);
            $GetEmployeeRes = array();
            while ($rows = mysqli_fetch_assoc($GetEmployee)) {
                $GetEmployeeRes[] = $rows;
            }
            if ($GetEmployee) {
                echo json_encode(array("Success" => true, "Msg" => $DataRow, "Data" => $GetEmployeeRes));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Data Not Found', "Data" => $DataRow));
            }
        } else {
            $GetEmployee = $clsfunreq->SelectOldFinalProcess($pemp_month, $pemp_comid, $pemp_locid);
            $GetEmployeeRes = array();
            while ($rows = mysqli_fetch_assoc($GetEmployee)) {
                $GetEmployeeRes[] = $rows;
            }
            if ($GetEmployee) {
                echo json_encode(array("Success" => true, "Msg" => $DataRow, "Data" => $GetEmployeeRes));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Data Not Found', "Data" => $DataRow));
            }
        }
    }
    if ((int) $_REQUEST['EmployeeReq'] == 18) { //Delete Final Process
        $getdtl = $_GET['json'];
        $datadtl = json_decode($getdtl, true);
        $pemp_month = $datadtl['pemp_month'];
        $pemp_comid = $datadtl['pemp_comid'];
        $pemp_locid = $datadtl['pemp_locid'];
        $GetEmployee = $clsfunreq->DeleteMonthofFinal($pemp_month, $pemp_comid, $pemp_locid);
        if ($GetEmployee) {
            echo json_encode(array("Success" => true, "Msg" => "Deleted Successfully", "Data" => $GetEmployee));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Data Not Found', "Data" => $GetEmployee));
        }
    }
    if ((int) $_REQUEST['EmployeeReq'] == 19) { //Transfer Final Process
        $getdtl = $_GET['json'];
        $datadtl = json_decode($getdtl, true);
        $pemp_month = $datadtl['pemp_month'];
        $pemp_comid = $datadtl['pemp_comid'];
        $pemp_locid = $datadtl['pemp_locid'];
        $pemp_trid = $datadtl['pemp_trid'];
        $pemp_refid = $datadtl['pemp_empid'];
        $GetEmployee = $clsfunreq->UpdateMonthofProcess($pemp_month, $pemp_comid, $pemp_locid, $pemp_trid, $pemp_refid);
        if ($GetEmployee) {
            echo json_encode(array("Success" => true, "Msg" => "Tranfered Successfully", "Data" => $GetEmployee));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Data Not Found', "Data" => $GetEmployee));
        }
    }
}
//MenuRequest
elseif (isset($_REQUEST['MenuRequest'])) {

    if ((int) $_REQUEST['MenuRequest'] == 1) { //Insert Header/Update - Accept both GET and POST
        $data = null;

        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            // Handle POST request
            $jsonData = file_get_contents("php://input");
            $data = json_decode($jsonData, true);
        } else {
            // Handle GET request - json parameter in URL
            if (isset($_GET['json'])) {
                $data = json_decode($_GET['json'], true);
            }
        }

        if ($data) {
            $phid = isset($data['phid']) ? $data['phid'] : 0;
            $ph_name = $data['ph_name'];
            $ph_projectid = $data['ph_projectid'];
            $ph_active = $data['ph_active'];
            $ph_menucode = $data['ph_menucode'];

            // Use phid to determine insert vs update, not menu code validation
            if ($phid == 0 || $phid == null) {
                // Insert new header menu
                $RequestInsert = $clsfunreq->InsertHeaderMenu($ph_name, $ph_projectid, $ph_active, $ph_menucode);
                if ($RequestInsert) {
                    echo json_encode(array("Success" => true, "Msg" => 'Menu Inserted'));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'No Data Inserted'));
                }
            } else {
                // Update existing header menu
                $RequestInsert = $clsfunreq->UpdateHeaderMenu($phid, $ph_name, $ph_projectid, $ph_active, $ph_menucode);
                if ($RequestInsert) {
                    echo json_encode(array("Success" => true, "Msg" => 'Menu Updated'));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'No Data Updated'));
                }
            }
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Invalid JSON data received'));
        }
    }
    if ((int) $_REQUEST['MenuRequest'] == 2) { //Delete Header Menu
        $jsonData = file_get_contents("php://input");
        $data = json_decode($jsonData, true);
        if ($data) {
            $phid = $data['phid'];
            $RequestInsert = $clsfunreq->DeleteHeaderMenu($phid);
            if ($RequestInsert) {
                echo json_encode(array("Success" => true, "Msg" => 'Menu Deleted'));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'No Data Updated'));
            }
        }
    }
    if ((int) $_REQUEST['MenuRequest'] == 3) { //Insert SubMenu - Accept both GET and POST
        $data = null;

        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            // Handle POST request
            $jsonData = file_get_contents("php://input");
            $data = json_decode($jsonData, true);
        } else {
            // Handle GET request - json parameter in URL
            if (isset($_GET['json'])) {
                $data = json_decode($_GET['json'], true);
            }
        }

        if ($data) {
            $psid = isset($data['psid']) ? $data['psid'] : 0;
            $ps_name = $data['ps_name'];
            $ph_id = $data['ph_id'];
            $ps_active = $data['ps_active'];
            $ps_menucode = $data['ps_menucode'];

            // Use psid to determine insert vs update
            if ($psid == 0 || $psid == null) {
                // Insert new sub menu
                $RequestInsert = $clsfunreq->InsertSubMenu($ps_name, $ph_id, $ps_active, $ps_menucode);
                if ($RequestInsert) {
                    echo json_encode(array("Success" => true, "Msg" => 'Sub Menu Inserted'));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'No Data Inserted'));
                }
            } else {
                // Update existing sub menu
                $RequestInsert = $clsfunreq->UpdateSubMenu($psid, $ps_name, $ph_id, $ps_active, $ps_menucode);
                if ($RequestInsert) {
                    echo json_encode(array("Success" => true, "Msg" => 'Sub Menu Updated'));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'No Data Updated'));
                }
            }
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Invalid JSON data received'));
        }
    }
    if ((int) $_REQUEST['MenuRequest'] == 4) { //Delete SubMenu - Accept both GET and POST
        $data = null;

        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            // Handle POST request
            $jsonData = file_get_contents("php://input");
            $data = json_decode($jsonData, true);
        } else {
            // Handle GET request - json parameter in URL
            if (isset($_GET['json'])) {
                $data = json_decode($_GET['json'], true);
            }
        }

        if ($data) {
            $psid = $data['psid'];
            $RequestInsert = $clsfunreq->DeleteSubMenu($psid);
            if ($RequestInsert) {
                echo json_encode(array("Success" => true, "Msg" => 'Sub Menu Deleted'));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'No Data Deleted'));
            }
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Invalid JSON data received'));
        }
    }
    if ((int) $_REQUEST['MenuRequest'] == 5) { //Select Header Sub Menu
        $GetQueryData = $clsfunreq->SelectHeadAndSubMenu();
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($GetQueryData)) {
            $GetDataRes[] = $rows;
        }
        if ($GetQueryData) {
            echo json_encode(array("Success" => true, "Msg" => 'Data Received', "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved', "Data" => "No Data"));
        }
    }
    if ((int) $_REQUEST['MenuRequest'] == 6) { //Get Header Menu List - Accept both GET and POST
        $GetQueryData = $clsfunreq->SelectHeadMenu();
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($GetQueryData)) {
            $GetDataRes[] = $rows;
        }
        if ($GetQueryData) {
            echo json_encode(array("Success" => true, "Msg" => 'Data Received', "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved', "Data" => "No Data"));
        }
    }
    if ((int) $_REQUEST['MenuRequest'] == 7) { //Get SubMenu with optional header filter - Accept both GET and POST
        $headerMenuId = 0;

        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            $jsonData = file_get_contents("php://input");
            $data = json_decode($jsonData, true);
            if ($data && isset($data['headerMenuId'])) {
                $headerMenuId = (int)$data['headerMenuId'];
            }
        } else {
            // Handle GET request - check for headerMenuId in URL parameters
            if (isset($_GET['headerMenuId'])) {
                $headerMenuId = (int)$_GET['headerMenuId'];
            }
        }

        if ($headerMenuId == 0) {
            // Get all sub menus with header menu names
            $GetQueryData = $clsfunreq->SelectSubMenuWithHeader();
        } else {
            // Get sub menus filtered by header menu ID
            $GetQueryData = $clsfunreq->SelectSubMenuByHeaderId($headerMenuId);
        }

        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($GetQueryData)) {
            $GetDataRes[] = $rows;
        }
        if ($GetQueryData) {
            echo json_encode(array("Success" => true, "Msg" => 'Data Received', "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved', "Data" => "No Data"));
        }
    }
    if ((int) $_REQUEST['MenuRequest'] == 8) { //Get Header Menu List for Permissions - Accept both GET and POST
        $GetQueryData = $clsfunreq->SelectHeadMenu();
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($GetQueryData)) {
            $GetDataRes[] = $rows;
        }
        if ($GetQueryData) {
            echo json_encode(array("Success" => true, "Msg" => 'Data Received', "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved', "Data" => array()));
        }
    }

    if ((int) $_REQUEST['MenuRequest'] == 9) { //Get SubMenu List for Permissions - Accept both GET and POST
        $headerMenuId = 0;

        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            $jsonData = file_get_contents("php://input");
            $data = json_decode($jsonData, true);
            if ($data && isset($data['headerMenuId'])) {
                $headerMenuId = (int)$data['headerMenuId'];
            }
        } else {
            // Handle GET request - check for headerMenuId in URL parameters
            if (isset($_GET['headerMenuId'])) {
                $headerMenuId = (int)$_GET['headerMenuId'];
            }
        }

        if ($headerMenuId == 0) {
            // Get all sub menus with header menu names
            $GetQueryData = $clsfunreq->SelectSubMenuWithHeader();
        } else {
            // Get sub menus filtered by header menu ID
            $GetQueryData = $clsfunreq->SelectSubMenuByHeaderId($headerMenuId);
        }

        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($GetQueryData)) {
            $GetDataRes[] = $rows;
        }
        if ($GetQueryData) {
            echo json_encode(array("Success" => true, "Msg" => 'Data Received', "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved', "Data" => array()));
        }
    }

    if ((int) $_REQUEST['MenuRequest'] == 10) { //Truncate Table (moved from 8 to 10)
        $GetQueryData = $clsfunreq->DeleteTruncateMenu(); //Table Trucate
        if ($GetQueryData) {
            echo json_encode(array("Success" => true, "Msg" => 'Data Received', "Data" => "Deleted Successfully"));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved', "Data" => "No Data"));
        }
    }
}
//GroupPolicyRequest
elseif (isset($_REQUEST['GroupPolicyRequest'])) {

    if ((int) $_REQUEST['GroupPolicyRequest'] == 1) { //Get all user groups
        $GetQueryData = $clsfunreq->GetAllUserGroups();
        $GetDataRes = array();
        while ($rows = mysqli_fetch_assoc($GetQueryData)) {
            $GetDataRes[] = $rows;
        }
        if ($GetQueryData) {
            echo json_encode(array("Success" => true, "Msg" => 'User Groups Retrieved', "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No User Groups Found', "Data" => array()));
        }
    }

    if ((int) $_REQUEST['GroupPolicyRequest'] == 2) { //Create/Update user group
        $data = null;

        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            $jsonData = file_get_contents("php://input");
            $data = json_decode($jsonData, true);
        } else {
            if (isset($_GET['json'])) {
                $data = json_decode($_GET['json'], true);
            }
        }

        if ($data) {
            $group_id = isset($data['group_id']) ? $data['group_id'] : 0;
            $group_name = $data['group_name'];
            $group_description = isset($data['group_description']) ? $data['group_description'] : '';
            $group_active = isset($data['group_active']) ? $data['group_active'] : 1;

            if ($group_id == 0 || $group_id == null) {
                // Create new user group
                $RequestInsert = $clsfunreq->CreateUserGroup($group_name, $group_description, $group_active);
                if ($RequestInsert) {
                    echo json_encode(array("Success" => true, "Msg" => 'User Group Created'));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Failed to Create User Group'));
                }
            } else {
                // Update existing user group
                $RequestUpdate = $clsfunreq->UpdateUserGroup($group_id, $group_name, $group_description, $group_active);
                if ($RequestUpdate) {
                    echo json_encode(array("Success" => true, "Msg" => 'User Group Updated'));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Failed to Update User Group'));
                }
            }
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Invalid JSON data received'));
        }
    }

    if ((int) $_REQUEST['GroupPolicyRequest'] == 3) { //Delete user group
        $data = null;

        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            $jsonData = file_get_contents("php://input");
            $data = json_decode($jsonData, true);
        } else {
            if (isset($_GET['group_id'])) {
                $data = array('group_id' => $_GET['group_id']);
            }
        }

        if ($data && isset($data['group_id'])) {
            $group_id = $data['group_id'];
            $RequestDelete = $clsfunreq->DeleteUserGroup($group_id);
            if ($RequestDelete) {
                echo json_encode(array("Success" => true, "Msg" => 'User Group Deleted'));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Failed to Delete User Group'));
            }
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Group ID not provided'));
        }
    }

    if ((int) $_REQUEST['GroupPolicyRequest'] == 4) { //Get group menu permissions
        $group_id = 0;

        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            $jsonData = file_get_contents("php://input");
            $data = json_decode($jsonData, true);
            if ($data && isset($data['group_id'])) {
                $group_id = (int)$data['group_id'];
            }
        } else {
            if (isset($_GET['group_id'])) {
                $group_id = (int)$_GET['group_id'];
            }
        }

        if ($group_id > 0) {
            $GetQueryData = $clsfunreq->GetGroupMenuPermissions($group_id);
            $GetDataRes = array();
            while ($rows = mysqli_fetch_assoc($GetQueryData)) {
                $GetDataRes[] = $rows;
            }
            if ($GetQueryData) {
                echo json_encode(array("Success" => true, "Msg" => 'Group Menu Permissions Retrieved', "Data" => $GetDataRes));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'No Permissions Found', "Data" => array()));
            }
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Invalid Group ID'));
        }
    }

    if ((int) $_REQUEST['GroupPolicyRequest'] == 5) { //Save group menu permissions
        $data = null;

        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            $jsonData = file_get_contents("php://input");
            $data = json_decode($jsonData, true);
        } else {
            if (isset($_GET['json'])) {
                $data = json_decode($_GET['json'], true);
            }
        }

        if ($data) {
            // Debug: Log the received data
            error_log("GroupPolicyRequest=5 received data: " . print_r($data, true));

            $group_id = $data['group_id'];
            $permissions = $data['permissions']; // Array of menu permissions

            // Debug: Log the group_id
            error_log("Extracted group_id: " . $group_id);

            // First delete existing permissions for this group
            $DeleteExisting = $clsfunreq->DeleteGroupMenuPermissions($group_id);

            $allSaved = true;
            foreach ($permissions as $permission) {
                // Extract header_menu_id and sub_menu_id
                $header_menu_id = isset($permission['header_menu_id']) ? $permission['header_menu_id'] : null;
                $sub_menu_id = isset($permission['sub_menu_id']) ? $permission['sub_menu_id'] : null;
                $menu_active = isset($permission['menu_active']) ? $permission['menu_active'] : 1;

                $SaveResult = $clsfunreq->SaveGroupMenuPermission($group_id, $header_menu_id, $sub_menu_id, $menu_active);
                if (!$SaveResult) {
                    $allSaved = false;
                }
            }

            if ($allSaved) {
                echo json_encode(array("Success" => true, "Msg" => 'Group Menu Permissions Saved'));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Failed to Save Some Permissions'));
            }
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Invalid JSON data received'));
        }
    }

    if ((int) $_REQUEST['GroupPolicyRequest'] == 6) { //Get user menu permissions
        $user_id = 0;

        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            $jsonData = file_get_contents("php://input");
            $data = json_decode($jsonData, true);
            if ($data && isset($data['user_id'])) {
                $user_id = (int)$data['user_id'];
            }
        } else {
            if (isset($_GET['user_id'])) {
                $user_id = (int)$_GET['user_id'];
            }
        }

        if ($user_id > 0) {
            $GetQueryData = $clsfunreq->GetUserMenuPermissions($user_id);
            $GetDataRes = array();
            while ($rows = mysqli_fetch_assoc($GetQueryData)) {
                $GetDataRes[] = $rows;
            }
            if ($GetQueryData) {
                echo json_encode(array("Success" => true, "Msg" => 'User Menu Permissions Retrieved', "Data" => $GetDataRes));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'No Permissions Found', "Data" => array()));
            }
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Invalid User ID'));
        }
    }

    if ((int) $_REQUEST['GroupPolicyRequest'] == 7) { //Check specific user permission
        $data = null;

        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            $jsonData = file_get_contents("php://input");
            $data = json_decode($jsonData, true);
        } else {
            if (isset($_GET['user_id']) && isset($_GET['menu_code'])) {
                $data = array(
                    'user_id' => $_GET['user_id'],
                    'menu_code' => $_GET['menu_code'],
                    'permission_type' => isset($_GET['permission_type']) ? $_GET['permission_type'] : 'view'
                );
            }
        }

        if ($data && isset($data['user_id']) && isset($data['menu_code'])) {
            $user_id = $data['user_id'];
            $menu_code = $data['menu_code'];

            $HasPermission = $clsfunreq->CheckUserPermission($user_id, $menu_code);

            if ($HasPermission) {
                echo json_encode(array("Success" => true, "Msg" => 'User has permission', "HasPermission" => true));
            } else {
                echo json_encode(array("Success" => true, "Msg" => 'User does not have permission', "HasPermission" => false));
            }
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'User ID and Menu Code are required'));
        }
    }

    if ((int) $_REQUEST['GroupPolicyRequest'] == 8) { //POS Settings CRUD operations
        $data = null;

        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            $jsonData = file_get_contents("php://input");
            $data = json_decode($jsonData, true);
        } else {
            if (isset($_GET['json'])) {
                $data = json_decode($_GET['json'], true);
            } else if (isset($_GET['operation'])) {
                $data = $_GET;
            }
        }

        if ($data && isset($data['operation'])) {
            $operation = strtoupper($data['operation']);

            switch ($operation) {
                case 'SELECT':
                    // Get all settings or filter by specific criteria
                    $whereClause = '';
                    $params = array();

                    if (isset($data['Id']) && !empty($data['Id'])) {
                        $whereClause = ' AND Id = ?';
                        $params[] = $data['Id'];
                    }
                    if (isset($data['Name']) && !empty($data['Name'])) {
                        $whereClause .= ' AND Name = ?';
                        $params[] = $data['Name'];
                    }
                    if (isset($data['Type']) && !empty($data['Type'])) {
                        $whereClause .= ' AND Type = ?';
                        $params[] = $data['Type'];
                    }

                    $GetQueryData = $clsfunreq->GetPosSettings($whereClause, $params);
                    $GetDataRes = array();
                    if ($GetQueryData) {
                        while ($rows = mysqli_fetch_assoc($GetQueryData)) {
                            $GetDataRes[] = $rows;
                        }
                        echo json_encode(array("Success" => true, "Msg" => 'Settings Retrieved', "Data" => $GetDataRes));
                    } else {
                        echo json_encode(array("Success" => false, "Msg" => 'No Settings Found', "Data" => array()));
                    }
                    break;

                case 'INSERT':
                    if (isset($data['Name']) && isset($data['Value'])) {
                        $Name = $data['Name'];
                        $Value = $data['Value'];
                        $Status = isset($data['Status']) ? $data['Status'] : 1;
                        $Type = isset($data['Type']) ? $data['Type'] : '0';

                        $RequestInsert = $clsfunreq->InsertPosSetting($Name, $Status, $Value, $Type);
                        if ($RequestInsert) {
                            echo json_encode(array("Success" => true, "Msg" => 'Setting Created Successfully'));
                        } else {
                            echo json_encode(array("Success" => false, "Msg" => 'Failed to Create Setting'));
                        }
                    } else {
                        echo json_encode(array("Success" => false, "Msg" => 'Name and Value are required'));
                    }
                    break;

                case 'UPDATE':
                    if (isset($data['Id']) && isset($data['Name']) && isset($data['Value'])) {
                        $Id = $data['Id'];
                        $Name = $data['Name'];
                        $Value = $data['Value'];
                        $Status = isset($data['Status']) ? $data['Status'] : 1;
                        $Type = isset($data['Type']) ? $data['Type'] : '0';

                        $RequestUpdate = $clsfunreq->UpdatePosSetting($Id, $Name, $Status, $Value, $Type);
                        if ($RequestUpdate) {
                            echo json_encode(array("Success" => true, "Msg" => 'Setting Updated Successfully'));
                        } else {
                            echo json_encode(array("Success" => false, "Msg" => 'Failed to Update Setting'));
                        }
                    } else {
                        echo json_encode(array("Success" => false, "Msg" => 'Id, Name and Value are required'));
                    }
                    break;

                case 'DELETE':
                    if (isset($data['Id'])) {
                        $Id = $data['Id'];

                        $RequestDelete = $clsfunreq->DeletePosSetting($Id);
                        if ($RequestDelete) {
                            echo json_encode(array("Success" => true, "Msg" => 'Setting Deleted Successfully'));
                        } else {
                            echo json_encode(array("Success" => false, "Msg" => 'Failed to Delete Setting'));
                        }
                    } else {
                        echo json_encode(array("Success" => false, "Msg" => 'Id is required for delete operation'));
                    }
                    break;

                default:
                    echo json_encode(array("Success" => false, "Msg" => 'Invalid operation. Use SELECT, INSERT, UPDATE, or DELETE'));
                    break;
            }
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Operation parameter is required'));
        }
    }
}
