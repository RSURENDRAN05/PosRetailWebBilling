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
        $plm_address = $row['address'];
        $saveCompay = $clsfunreq->SaveLocation($plm_name, $plm_active, $plm_address);
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
        $plm_address = $row['address'];
        $saveCompay = $clsfunreq->UpdateLocation($plm_id, $plm_name, $plm_active, $plm_address);
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
        $groupcolor = $row['groupcolor'];
        $mainstatus = $row['active'];
        $RequestInsert = $clsfunreq->_InsertMainMastrer($mainname, $mainstatus, $groupcolor);
        if ($RequestInsert) {
            echo json_encode(array("Success" => true, "Data" => $RequestInsert));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 14) {

        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $mainid = $row['id'];
        $mainname = $row['mainname'];
        $groupcolor = $row['groupcolor'];
        $mainstatus = $row['active'];
        $RequestInsert = $clsfunreq->_UpdateMainMastrer($mainid, $mainname, $mainstatus, $groupcolor);
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
        if ($ResulQuery instanceof mysqli_result) {
            while ($rows = mysqli_fetch_assoc($ResulQuery)) {
                $GetDataRes[] = $rows;
            }
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
        $color = $row['color'];
        $position = $row['position'];
        $catestatus = $row['active'];
        $RequestInsert = $clsfunreq->_InsertCateMastrer($catename, $mainid, $catestatus, $color, $position);
        if ($RequestInsert) {
            echo json_encode(array("Success" => true, "Msg" => "Data Saved", "Data" => $RequestInsert));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Data Saved'));
        }
    }
    if ((int) $_REQUEST['AjaxRequest'] == 18) {
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $cateid = $row['cateid'];
        $catename = $row['catename'];
        $color = $row['color'];
        $position = $row['position'];
        $mainid = $row['mainid'];
        $catestatus = $row['active'];
        $RequestInsert = $clsfunreq->_UpdateCateMastrer($cateid, $catename, $mainid, $catestatus, $color, $position);
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
        if ($ResulQuery instanceof mysqli_result) {
            while ($rows = mysqli_fetch_assoc($ResulQuery)) {
                $GetDataRes[] = $rows;
            }
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
        $color = $row['itemcolor'];
        $position = $row['itemposition'];
        $RequestInsert = $clsfunreq->_InsertProductMaster($dim_item_barcode, $dim_item_name, $dim_business_type, $dim_main_id, $dim_cate_id, $dim_tax_id, $dim_cost_price, $dim_sell_price, $dim_min_price, $dim_max_price, $dim_allow_disc, $dim_allow_negstock, $dim_allow_multiprice, $dim_op_stock, $dim_com_id, $dim_loc_id, $dim_status, $dim_remark, $color, $position);
        //   $RequestInsert = $clsfunreq->_InsertProductMaster($dim_item_barcode, $dim_item_name, $dim_business_type, $dim_main_id, $dim_cate_id, $dim_tax_id, $dim_cost_price, $dim_sell_price, $dim_min_price, $dim_max_price, $dim_allow_disc, $dim_allow_negstock, $dim_allow_multiprice, $dim_op_stock, $dim_com_id, $dim_loc_id, $dim_status, $dim_remark);
        if ($RequestInsert) {
            $productCode = $clsfunreq->_GetProductCode($dim_item_barcode, $dim_item_name, $dim_com_id, $dim_loc_id);
            if (strlen($productCode) > 0) {
                $RequestLiveStock = $clsfunreq->_InsertLiveStock($productCode, $dim_item_barcode, $dim_cost_price, $dim_sell_price, $dim_op_stock, $dim_op_stock, $dim_com_id, $dim_loc_id);
                if ($RequestLiveStock) {
                    echo json_encode(array("Success" => true, "Msg" => 'Item saved successfully', "Data" => $productCode));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'No Data Saved', "Data" => $RequestLiveStock));
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
        $color = $row['itemcolor'];
        $position = $row['itemposition'];
        $RequestInsert = $clsfunreq->_UpdateProductMaster($dim_item_id, $dim_item_barcode, $dim_item_name, $dim_business_type, $dim_main_id, $dim_cate_id, $dim_tax_id, $dim_cost_price, $dim_sell_price, $dim_min_price, $dim_max_price, $dim_allow_disc, $dim_allow_negstock, $dim_allow_multiprice, $dim_op_stock, $dim_com_id, $dim_loc_id, $dim_status, $dim_remark, $color, $position);
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
        $comId = $row['comId'];
        $locId = $row['locId'];
        $CustomerEmail = isset($row['customeremail']) ? $row['customeremail'] : '';
        $res = $clsfunreq->storeCustomerData($CustomerName, $CustomerPhone, $ActiveStatus, $comId, $locId, $CustomerEmail);
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
        $comId = $row['comId'];
        $locId = $row['locId'];
        $CustomerEmail = isset($row['customeremail']) ? $row['customeremail'] : '';
        $res = $clsfunreq->updateCustomerData($id, $txtCustomerName, $txtCustomerPhone, $status, $comId, $locId, $CustomerEmail);
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
                    "Created" => $rows['created'] ?? '',
                    "CustomerEmail" => $rows['customerEmail'] ?? '',
                    "ComId" => $rows['comId'],
                    "LocId" => $rows['locId']

                );
            }
            echo json_encode(array("Success" => true, "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Customer Data Found'));
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
        $comid = $_GET['comid'];
        $locid = $_GET['locid'];
        $ResulQuery = $clsfunreq->GetProductList($comid, $locid);
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
            $pph_trno = $trno;
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
        // $dim_item_id = $_GET['itemid'];

        $RequestSelect = $clsfunreq->_GetMultiplePricesByItem();
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

    // Get All Customers (including inactive)
    if ((int) $_REQUEST['AjaxRequest'] == 71) {
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
                    "Created" => $rows['created'] ?? '',
                    "ComId" => $rows['comId'] ?? '',
                    "LocId" => $rows['locId'] ?? '',
                    "CustomerEmail" => $rows['customerEmail'] ?? ''
                );
            }
            echo json_encode(array("Success" => true, "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Customer Data Found'));
        }
    }

    // Get Customer by ID
    if ((int) $_REQUEST['AjaxRequest'] == 72) {
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
                "Created" => $rows['created'] ?? '',
                "ComId" => $rows['comId'] ?? '',
                "LocId" => $rows['locId'] ?? '',
                "CustomerEmail" => $rows['customerEmail'] ?? ''
            );
            echo json_encode(array("Success" => true, "Data" => $GetDataRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Customer Not Found'));
        }
    }

    // Update Customer Points
    if ((int) $_REQUEST['AjaxRequest'] == 73) {
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
    if ((int) $_REQUEST['AjaxRequest'] == 74) {
        $customerId = $_GET['customerid'];
        $res = $clsfunreq->deleteCustomer($customerId);
        if ($res) {
            echo json_encode(array("Success" => true, "Msg" => 'Customer deleted successfully'));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Failed to delete customer'));
        }
    }
    //GetStock
    if ((int) $_REQUEST['AjaxRequest'] == 75) {
        $comid = $_GET['comid'];
        $locid = $_GET['locid'];
        $ResulQuery = $clsfunreq->_SelectStockProductBylocId($comid, $locid);
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
    //Recreate Stock
    if ((int) $_REQUEST['AjaxRequest'] == 76) {
        $comid = $_GET['comid'];
        $locid = $_GET['locid'];
        $ResulQuery = $clsfunreq->_ReCreateLiveStock($comid, $locid);
        if ($ResulQuery) {
            echo json_encode(array("Success" => true, "Msg" => 'Stock Recreated Successfully'));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Stock Recreate Failed'));
        }
    }
    //Livestock update 
    if ((int) $_REQUEST['AjaxRequest'] == 77) {
        $comid = $_POST['comid'];
        $locid = $_POST['locid'];
        $stockDataJson = $_POST['stockdata'];
        $stockRows = json_decode($stockDataJson, true);

        $success = $clsfunreq->_UpdateMultipleLiveStock($comid, $locid, $stockRows);

        if ($success) {
            echo json_encode(["Success" => true, "Msg" => "Stock Updated Successfully"]);
        } else {
            echo json_encode(["Success" => false, "Msg" => "Stock Update Failed"]);
        }
    }
    //Get Stock By Item Code
    if ((int) $_REQUEST['AjaxRequest'] == 78) {
        $comid = $_GET['comid'];
        $locid = $_GET['locid'];
        $itemcode = $_GET['itemcode'];
        $ResulQuery = $clsfunreq->_SelectStockByItemCode($comid, $locid, $itemcode);
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
}
//Sales
elseif (isset($_REQUEST['SalesRequest'])) {
    if ((int) $_REQUEST['SalesRequest'] == 1) { // Get Client Info

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
    if ((int) $_REQUEST['SalesRequest'] == 2) { // Get Max Bill No
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
    if ((int) $_REQUEST['SalesRequest'] == 3) { // Get Paymode List
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
        try {


            // Support both GET and POST to handle large data
            if ($_SERVER['REQUEST_METHOD'] === 'POST') {
                // Use POST data for large requests
                $getdtl = isset($_POST['dtl']) ? $_POST['dtl'] : '';
                $gethdr = isset($_POST['hdr']) ? $_POST['hdr'] : '';
                $pm_id = isset($_POST['pm_id']) ? $_POST['pm_id'] : 0;
                $comid = isset($_POST['comid']) ? $_POST['comid'] : 0;
                $locid = isset($_POST['locid']) ? $_POST['locid'] : 0;
            } else {
                // Fallback to GET for smaller requests
                $getdtl = $_GET['dtl'];
                $gethdr = $_GET['hdr'];
                $pm_id = isset($_GET['pm_id']) ? $_GET['pm_id'] : 0;
                $comid = isset($_GET['comid']) ? $_GET['comid'] : 0;
                $locid = isset($_GET['locid']) ? $_GET['locid'] : 0;


                // Check for potential URI too long issue
                if (strlen($_SERVER['REQUEST_URI']) > 2000) {
                }
            }

            $datadtl = json_decode($getdtl, true);
            $datahdr = json_decode($gethdr, true);

            if (json_last_error() !== JSON_ERROR_NONE) {

                echo json_encode(array("Success" => false, "Data" => "Invalid JSON data"));
                exit;
            }



            //Save Hdr

            $invoiceno = "";


            $updatePurTrno = $clsfunreq->UpdateSalesTransNo($pm_id, $comid, $locid);


            if ($updatePurTrno) {
                $invoiceno = $clsfunreq->GetSaleTransNo($pm_id, $comid, $locid);
            } else {
                $invoiceno = 0;
            }

            $psih_invoice_trno = $invoiceno;


            // Validate required header fields
            if (!isset($datahdr["psih_invoice_date"]) || empty($datahdr["psih_invoice_date"])) {
            }
            if (!isset($datahdr["psih_invoice_customerid"]) || empty($datahdr["psih_invoice_customerid"])) {
            }

            $psih_invoice_date = $datahdr["psih_invoice_date"];
            $psih_invoice_prefix = isset($datahdr["psih_invoice_prefix"]) ? $datahdr["psih_invoice_prefix"] : "";
            $psih_invoice_customerid = $datahdr["psih_invoice_customerid"];
            $psih_invoice_description = $datahdr["psih_invoice_description"];
            $psih_invoice_tqty = $datahdr["psih_invoice_tqty"];
            $psih_invoice_tamount = $datahdr["psih_invoice_tamount"];
            $psih_invoice_titemdisper = $datahdr["psih_invoice_titemdisper"];
            $psih_invoice_titemdisamt = $datahdr["psih_invoice_titemdisamt"];
            $psih_invoice_tbilldiscper = $datahdr["psih_invoice_tbilldiscper"];
            $psih_invoice_tbilldiscamt = $datahdr["psih_invoice_tbilldiscamt"];
            $psih_invoice_totdiscper = isset($datahdr["psih_invoice_totdiscper"]) ? $datahdr["psih_invoice_totdiscper"] : 0;
            $psih_invoice_totdiscamt = isset($datahdr["psih_invoice_totdiscamt"]) ? $datahdr["psih_invoice_totdiscamt"] : 0;
            $psih_invoice_tgrossamt = $datahdr["psih_invoice_tgrossamt"];
            $psih_invoice_ttaxamt = $datahdr["psih_invoice_ttaxamt"];
            $psih_invoice_sercharge = isset($datahdr["psih_invoice_sercharge"]) ? $datahdr["psih_invoice_sercharge"] : 0;
            $psih_invoice_roundoff = isset($datahdr["psih_invoice_roundoff"]) ? $datahdr["psih_invoice_roundoff"] : 0;
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
            $psih_invoice_shiftno = isset($datahdr["psih_invoice_shiftno"]) ? $datahdr["psih_invoice_shiftno"] : "";
            $psih_invoice_dayno = isset($datahdr["psih_invoice_dayno"]) ? $datahdr["psih_invoice_dayno"] : "";
            $psih_invoice_countername = isset($datahdr["psih_invoice_countername"]) ? $datahdr["psih_invoice_countername"] : "";
            $psih_invoice_pmid = $pm_id;
            $psih_invoice_print = isset($datahdr["psih_invoice_print"]) ? (int)$datahdr["psih_invoice_print"] : 0;
            $psih_invoice_refid = isset($datahdr["psih_invoice_refid"]) ? (int)$datahdr["psih_invoice_refid"] : 0;
            $InvoiceGUID = (isset($datahdr["InvoiceGUID"]) && preg_match('/^[0-9a-f\-]{36}$/i', $datahdr["InvoiceGUID"])) ? $datahdr["InvoiceGUID"] : sprintf('%04x%04x-%04x-%04x-%04x-%04x%04x%04x', mt_rand(0, 0xffff), mt_rand(0, 0xffff), mt_rand(0, 0xffff), mt_rand(0, 0x0fff) | 0x4000, mt_rand(0, 0x3fff) | 0x8000, mt_rand(0, 0xffff), mt_rand(0, 0xffff), mt_rand(0, 0xffff));

            $saveHdr = $clsfunreq->SaveSaleHdr(
                $psih_invoice_trno,
                $psih_invoice_date,
                $psih_invoice_prefix,
                $psih_invoice_description,
                $psih_invoice_tqty,
                $psih_invoice_tamount,
                $psih_invoice_titemdisper,
                $psih_invoice_titemdisamt,
                $psih_invoice_tbilldiscper,
                $psih_invoice_tbilldiscamt,
                $psih_invoice_totdiscper,
                $psih_invoice_totdiscamt,
                $psih_invoice_tgrossamt,
                $psih_invoice_ttaxamt,
                $psih_invoice_sercharge,
                $psih_invoice_roundoff,
                $psih_invoice_tnetamt,
                $psih_invoice_saletype,
                $psih_invoice_billtype,
                $psih_invoice_billstatus,
                $psih_invoice_paymode,
                $psih_invoice_customerid,
                $psih_invoice_userid,
                $psih_invoice_comid,
                $psih_invoice_locid,
                $psih_invoice_billremarks,
                $psih_invoice_advamt,
                $psih_invoice_outstanding,
                $psih_invoice_givenamt,
                $psih_invoice_balamt,
                $psih_invoice_shiftno,
                $psih_invoice_dayno,
                $psih_invoice_countername,
                $psih_invoice_pmid,
                $psih_invoice_print,
                $psih_invoice_refid,
                $InvoiceGUID
            );



            if ($saveHdr) {

                //Save Dtl
                $Sa_id = $clsfunreq->GetSalesId($invoiceno);


                foreach ($datadtl as $index => $row) {

                    $psid_invoice_sno = $row['psid_invoice_sno'];
                    $psid_invoice_salid = $Sa_id;
                    $psid_invoice_date = $psih_invoice_date;
                    $psid_invoice_trno = $invoiceno;
                    $psid_invoice_id = isset($row['psid_invoice_id']) ? $row['psid_invoice_id'] : "0";
                    $psid_invoice_description = $row['psid_invoice_description'];
                    $psid_invoice_procode = $row['psid_invoice_procode'];
                    $psid_invoice_barcode = isset($row['psid_invoice_barcode']) ? $row['psid_invoice_barcode'] : "";
                    $psid_invoice_serialno = isset($row['psid_invoice_serialno']) ? $row['psid_invoice_serialno'] : "";
                    $psid_invoice_uom = isset($row['psid_invoice_uom']) ? $row['psid_invoice_uom'] : "";
                    $psid_invoice_proqty = $row['psid_invoice_proqty'];
                    $psid_invoice_rate = $row['psid_invoice_rate'];
                    $psid_invoice_amt = $row['psid_invoice_amt'];
                    $psid_invoice_itemdisp = $row['psid_invoice_itemdisp'];
                    $psid_invoice_itemdisamt = $row['psid_invoice_itemdisamt'];
                    $psid_invoice_billdisp = $row['psid_invoice_billdisp'];
                    $psid_invoice_billdisamt = $row['psid_invoice_billdisamt'];
                    $psid_invoice_totdper = isset($row['psid_invoice_totdper']) ? $row['psid_invoice_totdper'] : 0;
                    $psid_invoice_totdamt = isset($row['psid_invoice_totdamt']) ? $row['psid_invoice_totdamt'] : 0;
                    $psid_invoice_gross = isset($row['psid_invoice_gross']) ? $row['psid_invoice_gross'] : 0;
                    $psid_invoice_taxinex =  isset($row['psid_invoice_taxinex']) ? $row['psid_invoice_taxinex'] : 0;
                    $psid_invoice_taxvalue = isset($row['psid_invoice_taxvalue']) ? $row['psid_invoice_taxvalue'] : 0;
                    $psid_invoice_taxamt = isset($row['psid_invoice_taxamt']) ? $row['psid_invoice_taxamt'] : 0;
                    $psid_invoice_netamt = isset($row['psid_invoice_netamt']) ? $row['psid_invoice_netamt'] : 0;
                    $psid_invoice_remarks = isset($row['psid_invoice_remarks']) ? $row['psid_invoice_remarks'] : "0";
                    $psid_invoice_batchno = isset($row['psid_invoice_batchno']) ? $row['psid_invoice_batchno'] : "0";
                    $psid_invoice_salesmanid = isset($row['psid_invoice_salesmanid']) ? $row['psid_invoice_salesmanid'] : "0";
                    $psid_invoice_salemanper = isset($row['psid_invoice_salemanper']) ? $row['psid_invoice_salemanper'] : 0;
                    $psid_invoice_shiftno = isset($row['psid_invoice_shiftno']) ? $row['psid_invoice_shiftno'] : "0";
                    $psid_invoice_dayno = isset($row['psid_invoice_dayno']) ? $row['psid_invoice_dayno'] : "0";
                    $saveDtl = $clsfunreq->SaveSaleDtl(
                        $psid_invoice_sno,
                        $psid_invoice_salid,
                        $psih_invoice_date,
                        $psid_invoice_trno,
                        $psid_invoice_description,
                        $psid_invoice_procode,
                        $psid_invoice_barcode,
                        $psid_invoice_serialno,
                        $psid_invoice_uom,
                        $psid_invoice_proqty,
                        $psid_invoice_rate,
                        $psid_invoice_amt,
                        $psid_invoice_itemdisp,
                        $psid_invoice_itemdisamt,
                        $psid_invoice_billdisp,
                        $psid_invoice_billdisamt,
                        $psid_invoice_totdper,
                        $psid_invoice_totdamt,
                        $psid_invoice_gross,
                        $psid_invoice_taxinex,
                        $psid_invoice_taxvalue,
                        $psid_invoice_taxamt,
                        $psid_invoice_netamt,
                        $psid_invoice_remarks,
                        $psid_invoice_batchno,
                        $psid_invoice_salesmanid,
                        $psid_invoice_salemanper,
                        $psid_invoice_shiftno,
                        $psid_invoice_dayno,
                        $psih_invoice_comid,
                        $psih_invoice_locid,
                        $psih_invoice_pmid,
                        0,
                        $InvoiceGUID
                    );



                    if (!$saveDtl) {
                    }

                    $resultLiveStock = $clsfunreq->_UpdateLiveStockSales($psid_invoice_procode, $psid_invoice_proqty, $psih_invoice_comid, $psih_invoice_locid);

                    if (!$resultLiveStock) {
                    }
                }
            } else {
                echo json_encode(array("Success" => false, "Data" => "Sales Not Saved", "InvoiceNo" => '0'));
                exit;
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

                echo json_encode(array("Success" => true, "Data" => 'Sales Saved InvoiceNo: ', "InvoiceNo" => $invoiceno));
            } else {

                echo json_encode(array("Success" => false, "Data" => "Voucher Not Updated", "InvoiceNo" => '0'));
            }
        } catch (Exception $e) {

            echo json_encode(array("Success" => false, "Data" => "Error occurred: " . $e->getMessage()));
        }
    }
    if ((int) $_REQUEST['SalesRequest'] == 5) { //Get Sales Bill by Date
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
    if ((int) $_REQUEST['SalesRequest'] == 6) { //Get Sales Bill by ID
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
    if ((int) $_REQUEST['SalesRequest'] == 7) { //Sales Update

        // Support both GET and POST to handle large data
        if ($_SERVER['REQUEST_METHOD'] === 'POST') {
            $getdtl = isset($_POST['dtl']) ? $_POST['dtl'] : '';
            $gethdr = isset($_POST['hdr']) ? $_POST['hdr'] : '';
            $pm_id = isset($_POST['pm_id']) ? $_POST['pm_id'] : 0;
            $comid = isset($_POST['comid']) ? $_POST['comid'] : 0;
            $locid = isset($_POST['locid']) ? $_POST['locid'] : 0;
        } else {
            $getdtl = isset($_GET['dtl']) ? $_GET['dtl'] : '';
            $gethdr = isset($_GET['hdr']) ? $_GET['hdr'] : '';
            $pm_id = isset($_GET['pm_id']) ? $_GET['pm_id'] : 0;
            $comid = isset($_GET['comid']) ? $_GET['comid'] : 0;
            $locid = isset($_GET['locid']) ? $_GET['locid'] : 0;
        }

        // JSON decode with error handling
        $datadtl = json_decode($getdtl, true);
        $datahdr = json_decode($gethdr, true);

        // Check for JSON decode errors
        if (json_last_error() !== JSON_ERROR_NONE) {
            $jsonError = json_last_error_msg();
            echo json_encode(array("Success" => false, "Data" => "JSON Decode Error: " . $jsonError));
            return;
        }

        // Validate decoded data
        if ($datadtl === null || $datahdr === null) {
            echo json_encode(array("Success" => false, "Data" => "Invalid JSON data received"));
            return;
        }

        if (empty($datadtl) || empty($datahdr)) {
            echo json_encode(array("Success" => false, "Data" => "Empty data arrays received"));
            return;
        }

        // Get parameters from URL
        $pm_id = isset($_GET['pm_id']) ? $_GET['pm_id'] : 0;
        $comid = isset($_GET['comid']) ? $_GET['comid'] : 0;
        $locid = isset($_GET['locid']) ? $_GET['locid'] : 0;

        //        echo print_r($datadtl);
        //        echo print_r($datahdr);

        // Validate required header fields
        $requiredHdrFields = [
            'psih_invoice_trno',
            'psih_invoice_id',
            'psih_invoice_date',
            'psih_invoice_customerid',
            'psih_invoice_description',
            'psih_invoice_tqty',
            'psih_invoice_tamount',
            'psih_invoice_tnetamt',
            'psih_invoice_saletype',
            'psih_invoice_billtype',
            'psih_invoice_billstatus',
            'psih_invoice_userid',
            'psih_invoice_comid',
            'psih_invoice_locid'
        ];

        $missingFields = array();
        foreach ($requiredHdrFields as $field) {
            if (!isset($datahdr[$field]) || $datahdr[$field] === '') {
                $missingFields[] = $field;
            }
        }

        if (!empty($missingFields)) {
            echo json_encode(array("Success" => false, "Data" => "Missing required header fields: " . implode(', ', $missingFields)));
            return;
        }

        //Save Hdr
        $invoiceno = $datahdr["psih_invoice_trno"];
        $psih_invoice_trno = $invoiceno;
        $psih_invoice_id = $datahdr["psih_invoice_id"];
        $psih_invoice_date = $datahdr["psih_invoice_date"];
        $psih_invoice_prefix = isset($datahdr["psih_invoice_prefix"]) ? $datahdr["psih_invoice_prefix"] : "";
        $psih_invoice_customerid = $datahdr["psih_invoice_customerid"];
        $psih_invoice_description = $datahdr["psih_invoice_description"];
        $psih_invoice_tqty = $datahdr["psih_invoice_tqty"];
        $psih_invoice_tamount = $datahdr["psih_invoice_tamount"];
        $psih_invoice_titemdisper = $datahdr["psih_invoice_titemdisper"];
        $psih_invoice_titemdisamt = $datahdr["psih_invoice_titemdisamt"];
        $psih_invoice_tbilldiscper = $datahdr["psih_invoice_tbilldiscper"];
        $psih_invoice_tbilldiscamt = $datahdr["psih_invoice_tbilldiscamt"];
        $psih_invoice_totdiscper = isset($datahdr["psih_invoice_totdiscper"]) ? $datahdr["psih_invoice_totdiscper"] : 0;
        $psih_invoice_totdiscamt = isset($datahdr["psih_invoice_totdiscamt"]) ? $datahdr["psih_invoice_totdiscamt"] : 0;
        $psih_invoice_tgrossamt = $datahdr["psih_invoice_tgrossamt"];
        $psih_invoice_ttaxamt = $datahdr["psih_invoice_ttaxamt"];
        $psih_invoice_sercharge = isset($datahdr["psih_invoice_sercharge"]) ? $datahdr["psih_invoice_sercharge"] : 0;
        $psih_invoice_roundoff = isset($datahdr["psih_invoice_roundoff"]) ? $datahdr["psih_invoice_roundoff"] : 0;
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
        $psih_invoice_shiftno = isset($datahdr["psih_invoice_shiftno"]) ? $datahdr["psih_invoice_shiftno"] : "";
        $psih_invoice_dayno = isset($datahdr["psih_invoice_dayno"]) ? $datahdr["psih_invoice_dayno"] : "";
        $psih_invoice_countername = isset($datahdr["psih_invoice_countername"]) ? $datahdr["psih_invoice_countername"] : "";

        // Extract payment mode - this was missing but used later in the code
        $psih_invoice_paymode = isset($datahdr["psih_invoice_paymode"]) ? $datahdr["psih_invoice_paymode"] : "cash";


        // Log header data before SaveSaleUpdate call


        $saveHdr = $clsfunreq->SaveSaleUpdate(
            $psih_invoice_trno,
            $psih_invoice_id,
            $psih_invoice_date,
            $psih_invoice_prefix,
            $psih_invoice_description,
            $psih_invoice_tqty,
            $psih_invoice_tamount,
            $psih_invoice_titemdisper,
            $psih_invoice_titemdisamt,
            $psih_invoice_tbilldiscper,
            $psih_invoice_tbilldiscamt,
            $psih_invoice_totdiscper,
            $psih_invoice_totdiscamt,
            $psih_invoice_tgrossamt,
            $psih_invoice_ttaxamt,
            $psih_invoice_sercharge,
            $psih_invoice_roundoff,
            $psih_invoice_tnetamt,
            $psih_invoice_saletype,
            $psih_invoice_billtype,
            $psih_invoice_billstatus,
            $psih_invoice_paymode,
            $psih_invoice_customerid,
            $psih_invoice_userid,
            $psih_invoice_comid,
            $psih_invoice_locid,
            $psih_invoice_billremarks,
            $psih_invoice_advamt,
            $psih_invoice_outstanding,
            $psih_invoice_givenamt,
            $psih_invoice_balamt,
            $psih_invoice_shiftno,
            $psih_invoice_dayno,
            $psih_invoice_countername
        );

        // Check SaveSaleUpdate result
        if ($saveHdr) {
        } else {
            // Log the database error - SaveSaleUpdate returned false


            echo json_encode(array("Success" => false, "Data" => "Failed to save invoice header - check logs for details"));
            return;
        }

        //Save Dtl
        $Sal_ID = $psih_invoice_id;
        $dtlSaveCount = 0;
        $dtlFailCount = 0;

        foreach ($datadtl as $index => $row) {
            // Validate required detail fields
            $requiredDtlFields = [
                'psid_invoice_sno',
                'psid_invoice_description',
                'psid_invoice_procode',
                'psid_invoice_proqty',
                'psid_invoice_rate',
                'psid_invoice_amt',
                'psid_invoice_gross',
                'psid_invoice_netamt'
            ];

            $missingDtlFields = array();
            foreach ($requiredDtlFields as $field) {
                if (!isset($row[$field]) || $row[$field] === '') {
                    $missingDtlFields[] = $field;
                }
            }

            if (!empty($missingDtlFields)) {

                $dtlFailCount++;
                continue;
            }

            $psid_invoice_sno = $row['psid_invoice_sno'];
            $psid_invoice_salid = $Sal_ID;
            $psid_invoice_trno = $invoiceno;
            $psid_invoice_id = isset($row['psid_invoice_id']) ? $row['psid_invoice_id'] : "";
            $psid_invoice_description = $row['psid_invoice_description'];
            $psid_invoice_procode = $row['psid_invoice_procode'];


            $psid_invoice_barcode = isset($row['psid_invoice_barcode']) ? $row['psid_invoice_barcode'] : "";
            $psid_invoice_serialno = isset($row['psid_invoice_serialno']) ? $row['psid_invoice_serialno'] : "";
            $psid_invoice_uom = isset($row['psid_invoice_uom']) ? $row['psid_invoice_uom'] : "";
            $psid_invoice_proqty = $row['psid_invoice_proqty'];
            $psid_invoice_rate = $row['psid_invoice_rate'];
            $psid_invoice_amt = $row['psid_invoice_amt'];
            $psid_invoice_itemdisp = $row['psid_invoice_itemdisp'];
            $psid_invoice_itemdisamt = $row['psid_invoice_itemdisamt'];
            $psid_invoice_billdisp = $row['psid_invoice_billdisp'];
            $psid_invoice_billdisamt = $row['psid_invoice_billdisamt'];
            $psid_invoice_totdper = isset($row['psid_invoice_totdper']) ? $row['psid_invoice_totdper'] : 0;
            $psid_invoice_totdamt = isset($row['psid_invoice_totdamt']) ? $row['psid_invoice_totdamt'] : 0;
            $psid_invoice_gross = $row['psid_invoice_gross'];
            $psid_invoice_taxinex = $row['psid_invoice_taxinex'];
            $psid_invoice_taxvalue = $row['psid_invoice_taxvalue'];
            $psid_invoice_taxamt = $row['psid_invoice_taxamt'];
            $psid_invoice_netamt = $row['psid_invoice_netamt'];
            $psid_invoice_remarks = isset($row['psid_invoice_remarks']) ? $row['psid_invoice_remarks'] : "";
            $psid_invoice_batchno = isset($row['psid_invoice_batchno']) ? $row['psid_invoice_batchno'] : "";
            $psid_invoice_salesmanid = isset($row['psid_invoice_salesmanid']) ? $row['psid_invoice_salesmanid'] : "";
            $psid_invoice_salemanper = isset($row['psid_invoice_salemanper']) ? $row['psid_invoice_salemanper'] : 0;
            $psid_invoice_shiftno = isset($row['psid_invoice_shiftno']) ? $row['psid_invoice_shiftno'] : "";
            $psid_invoice_dayno = isset($row['psid_invoice_dayno']) ? $row['psid_invoice_dayno'] : "";
            $saveDtl = $clsfunreq->SaveSaleDtlUpdate(
                $psid_invoice_id,
                $psid_invoice_sno,
                $psid_invoice_salid,
                $psih_invoice_date,
                $psid_invoice_trno,
                $psid_invoice_description,
                $psid_invoice_procode,
                $psid_invoice_barcode,
                $psid_invoice_serialno,
                $psid_invoice_uom,
                $psid_invoice_proqty,
                $psid_invoice_rate,
                $psid_invoice_amt,
                $psid_invoice_itemdisp,
                $psid_invoice_itemdisamt,
                $psid_invoice_billdisp,
                $psid_invoice_billdisamt,
                $psid_invoice_totdper,
                $psid_invoice_totdamt,
                $psid_invoice_gross,
                $psid_invoice_taxinex,
                $psid_invoice_taxvalue,
                $psid_invoice_taxamt,
                $psid_invoice_netamt,
                $psid_invoice_remarks,
                $psid_invoice_batchno,
                $psid_invoice_salesmanid,
                $psid_invoice_salemanper,
                $psid_invoice_shiftno,
                $psid_invoice_dayno,
                $psih_invoice_comid,
                $psih_invoice_locid
            );

            // Check SaveSaleDtlUpdate result
            if ($saveDtl) {
                $dtlSaveCount++;
            } else {
                $dtlFailCount++;
            }
        }



        // Note: $saveDtl will only contain the result of the last detail save, this logic needs review
        if ($dtlSaveCount > 0) { // Changed from if ($saveDtl) to check if any details were saved

            if ($psih_invoice_saletype == 'Invoice') {


                $userid = $psih_invoice_userid;
                $customerId = $psih_invoice_customerid;
                $cr = $psih_invoice_tnetamt;
                $refinvoiceno = $psih_invoice_trno;
                $comid = $psih_invoice_comid;
                $locid = $psih_invoice_locid;
                $customername = $psih_invoice_description;
                $pph_invdate = $psih_invoice_date;

                // Delete existing journal entries

                $deleteJourEntry = $clsfunreq->deleteJourEntryBySales($psih_invoice_trno);

                if (!$deleteJourEntry) {
                }

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

                    if ($ressalesentry) {
                    } else {
                    }
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

                    if ($ressalesentry) {
                    } else {
                    }
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

                    if ($ressalesentry) {
                    } else {
                    }
                } else {
                }
            } else {
            }
        } else {
        }

        // Final result evaluation and logging
        if ($dtlSaveCount > 0) {

            echo json_encode(array("Success" => true, "Data" => 'Sales Saved InvoiceNo: ', "InvoiceNo" => $invoiceno));
        } else {

            echo json_encode(array("Success" => false, "Data" => "Sales Not Saved - No details saved"));
        }
    }
    if ((int) $_REQUEST['SalesRequest'] == 8) { //Get Sales Bill by BillNo
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
    if ((int) $_REQUEST['SalesRequest'] == 9) { //Quote Save Sales
        // Support both GET and POST to handle large data
        if ($_SERVER['REQUEST_METHOD'] === 'POST') {
            $getdtl = isset($_POST['dtl']) ? $_POST['dtl'] : '';
            $gethdr = isset($_POST['hdr']) ? $_POST['hdr'] : '';
            $pm_id = isset($_POST['pm_id']) ? $_POST['pm_id'] : 0;
            $comid = isset($_POST['comid']) ? $_POST['comid'] : 0;
            $locid = isset($_POST['locid']) ? $_POST['locid'] : 0;
        } else {
            $getdtl = $_GET['dtl'];
            $gethdr = $_GET['hdr'];
            $pm_id = isset($_GET['pm_id']) ? $_GET['pm_id'] : 0;
            $comid = isset($_GET['comid']) ? $_GET['comid'] : 0;
            $locid = isset($_GET['locid']) ? $_GET['locid'] : 0;
            if (strlen($_SERVER['REQUEST_URI']) > 2000) {
            }
        }
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
        $psih_invoice_pmid = isset($datahdr["psih_invoice_pmid"]) ? $datahdr["psih_invoice_pmid"] : "";
        $psih_invoice_id = isset($datahdr["psih_invoice_id"]) ? $datahdr["psih_invoice_id"] : "";
        $psih_invoice_trno = $invoiceno;
        $psih_invoice_date = $datahdr["psih_invoice_date"];
        $psih_invoice_prefix = isset($datahdr["psih_invoice_prefix"]) ? $datahdr["psih_invoice_prefix"] : "";
        $psih_invoice_customerid = $datahdr["psih_invoice_customerid"];
        $psih_invoice_description = $datahdr["psih_invoice_description"];
        $psih_invoice_tqty = $datahdr["psih_invoice_tqty"];
        $psih_invoice_tamount = $datahdr["psih_invoice_tamount"];
        $psih_invoice_titemdisper = $datahdr["psih_invoice_titemdisper"];
        $psih_invoice_titemdisamt = $datahdr["psih_invoice_titemdisamt"];
        $psih_invoice_tbilldiscper = $datahdr["psih_invoice_tbilldiscper"];
        $psih_invoice_tbilldiscamt = $datahdr["psih_invoice_tbilldiscamt"];
        $psih_invoice_totdiscper = isset($datahdr["psih_invoice_totdiscper"]) ? $datahdr["psih_invoice_totdiscper"] : 0;
        $psih_invoice_totdiscamt = isset($datahdr["psih_invoice_totdiscamt"]) ? $datahdr["psih_invoice_totdiscamt"] : 0;
        $psih_invoice_tgrossamt = $datahdr["psih_invoice_tgrossamt"];
        $psih_invoice_ttaxamt = $datahdr["psih_invoice_ttaxamt"];
        $psih_invoice_sercharge = isset($datahdr["psih_invoice_sercharge"]) ? $datahdr["psih_invoice_sercharge"] : 0;
        $psih_invoice_roundoff = isset($datahdr["psih_invoice_roundoff"]) ? $datahdr["psih_invoice_roundoff"] : 0;
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
        $psih_invoice_shiftno = isset($datahdr["psih_invoice_shiftno"]) ? $datahdr["psih_invoice_shiftno"] : "";
        $psih_invoice_dayno = isset($datahdr["psih_invoice_dayno"]) ? $datahdr["psih_invoice_dayno"] : "";
        $psih_invoice_pmid = isset($datahdr["psih_invoice_pmid"]) ? (int)$datahdr["psih_invoice_pmid"] : 0;
        $psih_invoice_print = isset($datahdr["psih_invoice_print"]) ? (int)$datahdr["psih_invoice_print"] : 0;
        $psih_invoice_refid = isset($datahdr["psih_invoice_refid"]) ? (int)$datahdr["psih_invoice_refid"] : 0;
        $InvoiceGUID = (isset($datahdr["InvoiceGUID"]) && preg_match('/^[0-9a-f\-]{36}$/i', $datahdr["InvoiceGUID"])) ? $datahdr["InvoiceGUID"] : sprintf('%04x%04x-%04x-%04x-%04x-%04x%04x%04x', mt_rand(0, 0xffff), mt_rand(0, 0xffff), mt_rand(0, 0xffff), mt_rand(0, 0x0fff) | 0x4000, mt_rand(0, 0x3fff) | 0x8000, mt_rand(0, 0xffff), mt_rand(0, 0xffff), mt_rand(0, 0xffff));
        //        $saveHdr = $psih_invoice_trno . ',' . $psih_invoice_description . ',' . $psih_invoice_tqty . ',' . $psih_invoice_tamount . ',' .
        //                $psih_invoice_titemdisper . ',' . $psih_invoice_titemdisamt . ',' . $psih_invoice_tbilldiscper . ',' . $psih_invoice_tbilldiscamt . ',' . $psih_invoice_tgrossamt . ',' .
        //                $psih_invoice_ttaxamt . ',' . $psih_invoice_tnetamt . ',' . $psih_invoice_saletype . ',' . $psih_invoice_billtype . ',' . $psih_invoice_billstatus . ',' . $psih_invoice_customerid . ',' .
        //                $psih_invoice_userid . ',' . $psih_invoice_comid . ',' . $psih_invoice_locid . ',' . $psih_invoice_billremarks . ',' . $psih_invoice_advamt . ',' . $psih_invoice_outstanding . ',' .
        //                $psih_invoice_givenamt . ',' . $psih_invoice_balamt;


        $saveHdr = $clsfunreq->SaveSaleQuoteHdr(
            $psih_invoice_trno,
            $psih_invoice_date,
            $psih_invoice_prefix,
            $psih_invoice_description,
            $psih_invoice_tqty,
            $psih_invoice_tamount,
            $psih_invoice_titemdisper,
            $psih_invoice_titemdisamt,
            $psih_invoice_tbilldiscper,
            $psih_invoice_tbilldiscamt,
            $psih_invoice_totdiscper,
            $psih_invoice_totdiscamt,
            $psih_invoice_tgrossamt,
            $psih_invoice_ttaxamt,
            $psih_invoice_sercharge,
            $psih_invoice_roundoff,
            $psih_invoice_tnetamt,
            $psih_invoice_saletype,
            $psih_invoice_billtype,
            $psih_invoice_billstatus,
            $psih_invoice_paymode,
            $psih_invoice_customerid,
            $psih_invoice_userid,
            $psih_invoice_comid,
            $psih_invoice_locid,
            $psih_invoice_billremarks,
            $psih_invoice_advamt,
            $psih_invoice_outstanding,
            $psih_invoice_givenamt,
            $psih_invoice_balamt,
            $psih_invoice_shiftno,
            $psih_invoice_dayno,
            $psih_invoice_pmid,
            $psih_invoice_print,
            $psih_invoice_refid,
            $InvoiceGUID
        );

        if ($saveHdr) {
            //Save Dtl
            $Sa_id = $clsfunreq->GetSalesQuoteId($invoiceno);
            foreach ($datadtl as $row) {
                $psid_invoice_sno = $row['psid_invoice_sno'];
                $psid_invoice_salid = $Sa_id;
                $psid_invoice_trno = $invoiceno;
                $psid_invoice_id = isset($row['psid_invoice_id']) ? $row['psid_invoice_id'] : "";
                $psid_invoice_description = $row['psid_invoice_description'];
                $psid_invoice_procode = $row['psid_invoice_procode'];
                $psid_invoice_barcode = isset($row['psid_invoice_barcode']) ? $row['psid_invoice_barcode'] : "";
                $psid_invoice_serialno = isset($row['psid_invoice_serialno']) ? $row['psid_invoice_serialno'] : "";
                $psid_invoice_uom = isset($row['psid_invoice_uom']) ? $row['psid_invoice_uom'] : "";
                $psid_invoice_proqty = $row['psid_invoice_proqty'];
                $psid_invoice_rate = $row['psid_invoice_rate'];
                $psid_invoice_amt = $row['psid_invoice_amt'];
                $psid_invoice_itemdisp = $row['psid_invoice_itemdisp'];
                $psid_invoice_itemdisamt = $row['psid_invoice_itemdisamt'];
                $psid_invoice_billdisp = $row['psid_invoice_billdisp'];
                $psid_invoice_billdisamt = $row['psid_invoice_billdisamt'];
                $psid_invoice_totdper = isset($row['psid_invoice_totdper']) ? $row['psid_invoice_totdper'] : 0;
                $psid_invoice_totdamt = isset($row['psid_invoice_totdamt']) ? $row['psid_invoice_totdamt'] : 0;
                $psid_invoice_gross = $row['psid_invoice_gross'];
                $psid_invoice_taxinex = $row['psid_invoice_taxinex'];
                $psid_invoice_taxvalue = $row['psid_invoice_taxvalue'];
                $psid_invoice_taxamt = $row['psid_invoice_taxamt'];
                $psid_invoice_netamt = $row['psid_invoice_netamt'];
                $psid_invoice_remarks = isset($row['psid_invoice_remarks']) ? $row['psid_invoice_remarks'] : "";
                $psid_invoice_batchno = isset($row['psid_invoice_batchno']) ? $row['psid_invoice_batchno'] : "";
                $psid_invoice_salesmanid = isset($row['psid_invoice_salesmanid']) ? $row['psid_invoice_salesmanid'] : "";
                $psid_invoice_salemanper = isset($row['psid_invoice_salemanper']) ? $row['psid_invoice_salemanper'] : 0;
                $psid_invoice_shiftno = isset($row['psid_invoice_shiftno']) ? $row['psid_invoice_shiftno'] : "";
                $psid_invoice_dayno = isset($row['psid_invoice_dayno']) ? $row['psid_invoice_dayno'] : "";
                $saveDtl = $clsfunreq->SaveSaleQuoteDtl(
                    $psid_invoice_sno,
                    $psid_invoice_salid,
                    $psih_invoice_date,
                    $psid_invoice_trno,
                    $psid_invoice_id,
                    $psid_invoice_description,
                    $psid_invoice_procode,
                    $psid_invoice_barcode,
                    $psid_invoice_serialno,
                    $psid_invoice_uom,
                    $psid_invoice_proqty,
                    $psid_invoice_rate,
                    $psid_invoice_amt,
                    $psid_invoice_itemdisp,
                    $psid_invoice_itemdisamt,
                    $psid_invoice_billdisp,
                    $psid_invoice_billdisamt,
                    $psid_invoice_totdper,
                    $psid_invoice_totdamt,
                    $psid_invoice_gross,
                    $psid_invoice_taxinex,
                    $psid_invoice_taxvalue,
                    $psid_invoice_taxamt,
                    $psid_invoice_netamt,
                    $psid_invoice_remarks,
                    $psid_invoice_batchno,
                    $psid_invoice_salesmanid,
                    $psid_invoice_salemanper,
                    $psid_invoice_shiftno,
                    $psid_invoice_dayno,
                    $psih_invoice_comid,
                    $psih_invoice_locid,
                    $psih_invoice_pmid,
                    0,
                    $InvoiceGUID
                );
            }
        }
        if ($saveDtl) {
            echo json_encode(array("Success" => true, "Data" => 'Sales Saved InvoiceNo: ' . $invoiceno));
        } else {
            echo json_encode(array("Success" => false, "Data" => "Voucher Not Updated"));
        }
    }
    if ((int) $_REQUEST['SalesRequest'] == 10) { //Quote Update Sales
        // Support both GET and POST to handle large data
        if ($_SERVER['REQUEST_METHOD'] === 'POST') {
            $getdtl = isset($_POST['dtl']) ? $_POST['dtl'] : '';
            $gethdr = isset($_POST['hdr']) ? $_POST['hdr'] : '';
            $pm_id = isset($_POST['pm_id']) ? $_POST['pm_id'] : 0;
            $comid = isset($_POST['comid']) ? $_POST['comid'] : 0;
            $locid = isset($_POST['locid']) ? $_POST['locid'] : 0;
        } else {
            $getdtl = $_GET['dtl'];
            $gethdr = $_GET['hdr'];
            $pm_id = isset($_GET['pm_id']) ? $_GET['pm_id'] : 0;
            $comid = isset($_GET['comid']) ? $_GET['comid'] : 0;
            $locid = isset($_GET['locid']) ? $_GET['locid'] : 0;
            if (strlen($_SERVER['REQUEST_URI']) > 2000) {
            }
        }
        $datadtl = json_decode($getdtl, true);
        $datahdr = json_decode($gethdr, true);



        //        echo print_r($datadtl);
        //        echo print_r($datahdr);
        //Save Hdr
        $invoiceno = $datahdr["psih_invoice_trno"];
        $psih_invoice_pmid = isset($datahdr["psih_invoice_pmid"]) ? $datahdr["psih_invoice_pmid"] : "";
        $psih_invoice_id = isset($datahdr["psih_invoice_id"]) ? $datahdr["psih_invoice_id"] : "";
        $psih_invoice_trno = $invoiceno;
        $psih_invoice_date = $datahdr["psih_invoice_date"];
        $psih_invoice_prefix = isset($datahdr["psih_invoice_prefix"]) ? $datahdr["psih_invoice_prefix"] : "";
        $psih_invoice_customerid = $datahdr["psih_invoice_customerid"];
        $psih_invoice_description = $datahdr["psih_invoice_description"];
        $psih_invoice_tqty = $datahdr["psih_invoice_tqty"];
        $psih_invoice_tamount = $datahdr["psih_invoice_tamount"];
        $psih_invoice_titemdisper = $datahdr["psih_invoice_titemdisper"];
        $psih_invoice_titemdisamt = $datahdr["psih_invoice_titemdisamt"];
        $psih_invoice_tbilldiscper = $datahdr["psih_invoice_tbilldiscper"];
        $psih_invoice_tbilldiscamt = $datahdr["psih_invoice_tbilldiscamt"];
        $psih_invoice_totdiscper = isset($datahdr["psih_invoice_totdiscper"]) ? $datahdr["psih_invoice_totdiscper"] : 0;
        $psih_invoice_totdiscamt = isset($datahdr["psih_invoice_totdiscamt"]) ? $datahdr["psih_invoice_totdiscamt"] : 0;
        $psih_invoice_tgrossamt = $datahdr["psih_invoice_tgrossamt"];
        $psih_invoice_ttaxamt = $datahdr["psih_invoice_ttaxamt"];
        $psih_invoice_sercharge = isset($datahdr["psih_invoice_sercharge"]) ? $datahdr["psih_invoice_sercharge"] : 0;
        $psih_invoice_roundoff = isset($datahdr["psih_invoice_roundoff"]) ? $datahdr["psih_invoice_roundoff"] : 0;
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
        $psih_invoice_shiftno = isset($datahdr["psih_invoice_shiftno"]) ? $datahdr["psih_invoice_shiftno"] : "";
        $psih_invoice_dayno = isset($datahdr["psih_invoice_dayno"]) ? $datahdr["psih_invoice_dayno"] : "";
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
            $psih_invoice_balamt,
            $psih_invoice_pmid,
            $psih_invoice_id,
            $psih_invoice_prefix,
            $psih_invoice_totdiscper,
            $psih_invoice_totdiscamt,
            $psih_invoice_sercharge,
            $psih_invoice_roundoff,
            $psih_invoice_shiftno,
            $psih_invoice_dayno
        );
        if ($saveHdr) {
            //Save Dtl
            $Sal_ID = $clsfunreq->GetSalesQuoteId($invoiceno);

            foreach ($datadtl as $row) {
                $psid_invoice_sno = $row['psid_invoice_sno'];
                $psid_invoice_salid = $Sal_ID;
                $psid_invoice_trno = $invoiceno;
                $psid_invoice_id = isset($row['psid_invoice_id']) ? $row['psid_invoice_id'] : "";
                $psid_invoice_description = $row['psid_invoice_description'];
                $psid_invoice_procode = $row['psid_invoice_procode'];
                $psid_invoice_barcode = isset($row['psid_invoice_barcode']) ? $row['psid_invoice_barcode'] : "";
                $psid_invoice_serialno = isset($row['psid_invoice_serialno']) ? $row['psid_invoice_serialno'] : "";
                $psid_invoice_uom = isset($row['psid_invoice_uom']) ? $row['psid_invoice_uom'] : "";
                $psid_invoice_proqty = $row['psid_invoice_proqty'];
                $psid_invoice_rate = $row['psid_invoice_rate'];
                $psid_invoice_amt = $row['psid_invoice_amt'];
                $psid_invoice_itemdisp = $row['psid_invoice_itemdisp'];
                $psid_invoice_itemdisamt = $row['psid_invoice_itemdisamt'];
                $psid_invoice_billdisp = $row['psid_invoice_billdisp'];
                $psid_invoice_billdisamt = $row['psid_invoice_billdisamt'];
                $psid_invoice_totdper = isset($row['psid_invoice_totdper']) ? $row['psid_invoice_totdper'] : 0;
                $psid_invoice_totdamt = isset($row['psid_invoice_totdamt']) ? $row['psid_invoice_totdamt'] : 0;
                $psid_invoice_gross = $row['psid_invoice_gross'];
                $psid_invoice_taxinex = $row['psid_invoice_taxinex'];
                $psid_invoice_taxvalue = $row['psid_invoice_taxvalue'];
                $psid_invoice_taxamt = $row['psid_invoice_taxamt'];
                $psid_invoice_netamt = $row['psid_invoice_netamt'];
                $psid_invoice_remarks = isset($row['psid_invoice_remarks']) ? $row['psid_invoice_remarks'] : "";
                $psid_invoice_batchno = isset($row['psid_invoice_batchno']) ? $row['psid_invoice_batchno'] : "";
                $psid_invoice_salesmanid = isset($row['psid_invoice_salesmanid']) ? $row['psid_invoice_salesmanid'] : "";
                $psid_invoice_salemanper = isset($row['psid_invoice_salemanper']) ? $row['psid_invoice_salemanper'] : 0;
                $psid_invoice_shiftno = isset($row['psid_invoice_shiftno']) ? $row['psid_invoice_shiftno'] : "";
                $psid_invoice_dayno = isset($row['psid_invoice_dayno']) ? $row['psid_invoice_dayno'] : "";
                $saveDtl = $clsfunreq->SaveSaleDtlQuoteUpdate(
                    $psid_invoice_id,
                    $psid_invoice_sno,
                    $psid_invoice_salid,
                    $psih_invoice_date,
                    $psid_invoice_trno,
                    $psid_invoice_description,
                    $psid_invoice_procode,
                    $psid_invoice_barcode,
                    $psid_invoice_serialno,
                    $psid_invoice_uom,
                    $psid_invoice_proqty,
                    $psid_invoice_rate,
                    $psid_invoice_amt,
                    $psid_invoice_itemdisp,
                    $psid_invoice_itemdisamt,
                    $psid_invoice_billdisp,
                    $psid_invoice_billdisamt,
                    $psid_invoice_totdper,
                    $psid_invoice_totdamt,
                    $psid_invoice_gross,
                    $psid_invoice_taxinex,
                    $psid_invoice_taxvalue,
                    $psid_invoice_taxamt,
                    $psid_invoice_netamt,
                    $psid_invoice_remarks,
                    $psid_invoice_batchno,
                    $psid_invoice_salesmanid,
                    $psid_invoice_salemanper,
                    $psid_invoice_shiftno,
                    $psid_invoice_dayno,
                    $psih_invoice_comid,
                    $psih_invoice_locid
                );
            }
        }
        if ($saveDtl) {
            echo json_encode(array("Success" => true, "Data" => "Voucher Updated"));
        } else {
            echo json_encode(array("Success" => false, "Data" => "Voucher Not Updated"));
        }
    }
    if ((int) $_REQUEST['SalesRequest'] == 11) { //Get Sales Bill by Date
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
    if ((int) $_REQUEST['SalesRequest'] == 12) { //Get Sales Bill by QuoteSalID
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
    if ((int) $_REQUEST['SalesRequest'] == 13) { //Get Sales Bill by BillNo
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
    if ((int)$_REQUEST['SalesRequest'] == 14) { //Save Delete Item
        try {
            // Get parameters from request
            $billno = isset($_GET['billno']) ? $_GET['billno'] : '';
            $procode = isset($_GET['procode']) ? $_GET['procode'] : '';
            $description = isset($_GET['description']) ? $_GET['description'] : '';
            $netamt = isset($_GET['netamt']) ? floatval($_GET['netamt']) : 0.00;
            $reason = isset($_GET['reason']) ? $_GET['reason'] : 'Item deleted by user';
            $pcname = isset($_GET['pcname']) ? $_GET['pcname'] : '';
            $shiftno = isset($_GET['shiftno']) ? intval($_GET['shiftno']) : 0;
            $dayno = isset($_GET['dayno']) ? intval($_GET['dayno']) : 0;
            $comid = isset($_GET['comid']) ? intval($_GET['comid']) : 0;
            $locid = isset($_GET['locid']) ? intval($_GET['locid']) : 0;
            $userid = isset($_GET['userid']) ? intval($_GET['userid']) : 0;
            $qty = isset($_GET['qty']) ? floatval($_GET['qty']) : 0.0;
            $psid = isset($_GET['psid']) ? intval($_GET['psid']) : 0;

            // Validate required fields - allow "0" for billno but not empty string
            if ((empty(trim($billno)) && trim($billno) !== '0') || empty(trim($procode)) || empty(trim($description))) {
                echo json_encode(array("Success" => false, "Data" => "Missing required fields: billno='" . $billno . "', procode='" . $procode . "', description='" . $description . "'"));
                exit;
            }

            // Call the SaveDeleteRecord method with new parameters
            $res = $clsfunreq->SaveDeleteRecord($billno, $procode, $description, $netamt, $reason, $pcname, $shiftno, $dayno, $comid, $locid, $userid, $qty, $psid);

            if ($res) {
                echo json_encode(array("Success" => true, "Data" => "Delete record saved successfully"));
            } else {
                echo json_encode(array("Success" => false, "Data" => "Failed to save delete record"));
            }
        } catch (Exception $e) {
            echo json_encode(array("Success" => false, "Data" => "Error saving delete record: " . $e->getMessage()));
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
            $pef_salesallowance = $row['EmpSalesAllowance'];
            $pef_salescommission = $row['EmpSalesCommission'];
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
            //                    $pef_extrahours . ',' . $pef_extrahrsamt . ',' . $pef_allowance . ',' . $pef_salesallowance . ',' . $pef_salescommission . ',' . $pef_grossamt . ',' . $pef_advance . ',' .
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
                    $pef_salesallowance,
                    $pef_salescommission,
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
                        $pef_salesallowance,
                        $pef_salescommission,
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

    if ((int) $_REQUEST['MenuRequest'] == 11) { //Button Properties Management - Accept both GET and POST
        $data = null;

        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            // Handle POST request
            $jsonData = file_get_contents("php://input");
            $data = json_decode($jsonData, true);
        } else {
            // Handle GET request
            if (isset($_GET['json'])) {
                $data = json_decode($_GET['json'], true);
            } else {
                // Handle direct GET parameters
                $data = $_GET;
            }
        }

        if ($data && isset($data['operation'])) {
            $operation = strtoupper($data['operation']);

            switch ($operation) {
                case 'SAVE':
                    // Universal save operation - insert if new, update if exists
                    $requiredFields = ['item_id', 'menu_type'];
                    foreach ($requiredFields as $field) {
                        if (!isset($data[$field]) || empty($data[$field])) {
                            echo json_encode(array("Success" => false, "Msg" => "Required field missing: $field"));
                            return;
                        }
                    }
                    $item_id = $data['item_id'];
                    $item_name = isset($data['item_name']) ? $data['item_name'] : '';
                    $menu_type = $data['menu_type'];
                    $font_size = isset($data['font_size']) ? $data['font_size'] : 10.0;
                    $font_name = isset($data['font_name']) ? $data['font_name'] : 'Segoe UI';
                    $font_style = isset($data['font_style']) ? $data['font_style'] : 'Regular';
                    $text_color = isset($data['text_color']) ? $data['text_color'] : 'Argb(255,255,255,255)';
                    $back_color = isset($data['back_color']) ? $data['back_color'] : 'Argb(255,72,61,139)';
                    $position = isset($data['position']) ? $data['position'] : 0;
                    $button_width = isset($data['button_width']) ? $data['button_width'] : 100;
                    $button_height = isset($data['button_height']) ? $data['button_height'] : 50;
                    // Check if button properties already exist for this item and menu type
                    $exists = $clsfunreq->CheckButtonPropertiesExists($item_id, $menu_type);
                    if ($exists) {
                        // Update existing record
                        $RequestUpdate = $clsfunreq->UpdateButtonProperties($item_id, $menu_type, $font_size, $font_name, $font_style, $text_color, $back_color, $position, $item_name);
                        if ($RequestUpdate) {
                            echo json_encode(array("Success" => true, "Msg" => 'Button Properties Updated Successfully'));
                        } else {
                            echo json_encode(array("Success" => false, "Msg" => 'Failed to Update Button Properties'));
                        }
                    } else {
                        // Insert new record
                        $RequestInsert = $clsfunreq->InsertButtonProperties($item_id, $menu_type, $font_size, $font_name, $font_style, $text_color, $back_color, $position);
                        if ($RequestInsert) {
                            echo json_encode(array("Success" => true, "Msg" => 'Button Properties Created Successfully'));
                        } else {
                            echo json_encode(array("Success" => false, "Msg" => 'Failed to Create Button Properties'));
                        }
                    }
                    break;
                default:
                    echo json_encode(array("Success" => false, "Msg" => 'Invalid operation. Use SELECT, SAVE, or DELETE'));
                    break;
            }
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Operation parameter is required'));
        }
    }

    if ((int) $_REQUEST['MenuRequest'] == 12) { //Button Dimension Settings - Accept both GET and POST
        $operation = isset($_GET['operation']) ? $_GET['operation'] : '';

        if ($operation == "SAVE_DIMENSION") {
            $groupName = isset($_GET['group_name']) ? $_GET['group_name'] : '';
            $groupValue = isset($_GET['group_value']) ? $_GET['group_value'] : '';

            if (!empty($groupName) && !empty($groupValue)) {
                $result = $clsfunreq->saveDimensionSetting($groupName, $groupValue);

                if ($result) {
                    echo json_encode([
                        "Success" => true,
                        "Message" => "Dimension setting saved successfully",
                        "GroupName" => $groupName,
                        "GroupValue" => $groupValue
                    ]);
                } else {
                    echo json_encode([
                        "Success" => false,
                        "Message" => "Failed to save dimension setting"
                    ]);
                }
            } else {
                echo json_encode([
                    "Success" => false,
                    "Message" => "Invalid parameters"
                ]);
            }
        } else {
            echo json_encode([
                "Success" => false,
                "Message" => "Invalid operation"
            ]);
        }
    }
    //Package Items
    if ((int) $_REQUEST['MenuRequest'] == 13) { //Get Package Items
        $packageId = isset($_GET['PackageId']) ? $_GET['PackageId'] : 0;
        $GetQueryData = $clsfunreq->GetPackageItems($packageId);
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
    //Insert Package Items
    //Id,ItemId, ItemName, ItemPrice, ItemActive
    if ((int) $_REQUEST['MenuRequest'] == 14) { //Insert Package Items
        try {
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

            // Log received data for debugging
            error_log("Received data: " . print_r($data, true));

            if (!$data) {
                echo json_encode(array("Success" => false, "Msg" => 'Invalid JSON data received'));
                exit;
            }

            // Check if data is an array of items or a single item
            if (isset($data[0])) {
                // Handle array of items
                $successCount = 0;
                $failCount = 0;

                foreach ($data as $item) {
                    $result = $clsfunreq->processPackageItem($item);
                    if ($result) {
                        $successCount++;
                    } else {
                        $failCount++;
                    }
                }

                if ($successCount > 0) {
                    echo json_encode(array(
                        "Success" => true,
                        "Msg" => "Processed $successCount package items successfully" .
                            ($failCount > 0 ? ", $failCount failed" : "")
                    ));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => "Failed to process package items"));
                }
            } else {
                // Handle single item
                $result = $clsfunreq->processPackageItem($data);

                if ($result) {
                    echo json_encode(array("Success" => true, "Msg" => 'Package Item ' . ($result == 'insert' ? 'Inserted' : 'Updated')));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Failed to process package item'));
                }
            }
        } catch (Exception $e) {
            error_log("Package Item Error: " . $e->getMessage());
            echo json_encode(array("Success" => false, "Msg" => 'Server error: ' . $e->getMessage()));
        }
    }

    if ((int) $_REQUEST['MenuRequest'] == 15) { //Delete Package Item
        $jsonData = file_get_contents("php://input");
        $data = json_decode($jsonData, true);
        if ($data) {
            $id = $data['id'];
            $RequestInsert = $clsfunreq->DeletePackageItem($id);
            if ($RequestInsert) {
                echo json_encode(array("Success" => true, "Msg" => 'Package Item Deleted'));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'No Data Updated'));
            }
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
//SalesManCommission
elseif (isset($_REQUEST['SalesManCommission'])) {
    if ((int) $_REQUEST['SalesManCommission'] == 1) { // Get Salesmen List
        $GetSalesmen = $clsfunreq->GetSalesmanList();
        $GetSalesmenRes = array();
        while ($rows = mysqli_fetch_assoc($GetSalesmen)) {
            $GetSalesmenRes[] = $rows;
        }
        if ($GetSalesmen) {
            echo json_encode(array("Success" => true, "Data" => $GetSalesmenRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Salesmen Found'));
        }
    }

    if ((int) $_REQUEST['SalesManCommission'] == 2) { // Get Sub Groups List
        $GetSubGroups = $clsfunreq->GetSubGroupList();
        $GetSubGroupsRes = array();
        while ($rows = mysqli_fetch_assoc($GetSubGroups)) {
            $GetSubGroupsRes[] = $rows;
        }
        if ($GetSubGroups) {
            echo json_encode(array("Success" => true, "Data" => $GetSubGroupsRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Sub Groups Found'));
        }
    }

    if ((int) $_REQUEST['SalesManCommission'] == 3) { // Get Items by Sub Group
        $sub_group_id = $_GET['sub_group_id'];
        $GetItems = $clsfunreq->GetItemsBySubGroup($sub_group_id);
        $GetItemsRes = array();
        while ($rows = mysqli_fetch_assoc($GetItems)) {
            $GetItemsRes[] = $rows;
        }
        if ($GetItems) {
            echo json_encode(array("Success" => true, "Data" => $GetItemsRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Items Found'));
        }
    }

    if ((int) $_REQUEST['SalesManCommission'] == 4) { // Insert Commission
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $emp_id = $row['emp_id'];
        $item_id = $row['item_id'];
        $sub_group_id = $row['sub_group_id'];
        $commission_percentage = $row['commission_percentage'];
        $commission_type = $row['commission_type'];
        $fixed_amount = isset($row['fixed_amount']) ? $row['fixed_amount'] : 0;
        $status = isset($row['status']) ? $row['status'] : 1;

        // Check if commission already exists
        $exists = $clsfunreq->CheckCommissionExists($emp_id, $item_id, $sub_group_id);
        if ($exists) {
            echo json_encode(array("Success" => false, "Msg" => 'Commission already exists for this item and salesman'));
        } else {
            $RequestInsert = $clsfunreq->InsertSalesmanCommission($emp_id, $item_id, $sub_group_id, $commission_percentage, $commission_type, $fixed_amount, $status);
            if ($RequestInsert) {
                echo json_encode(array("Success" => true, "Msg" => 'Commission created successfully'));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Failed to create commission'));
            }
        }
    }

    if ((int) $_REQUEST['SalesManCommission'] == 5) { // Update Commission
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $commission_id = $row['commission_id'];
        $emp_id = $row['emp_id'];
        $item_id = $row['item_id'];
        $sub_group_id = $row['sub_group_id'];
        $commission_percentage = $row['commission_percentage'];
        $commission_type = $row['commission_type'];
        $fixed_amount = isset($row['fixed_amount']) ? $row['fixed_amount'] : 0;
        $status = isset($row['status']) ? $row['status'] : 1;

        $RequestUpdate = $clsfunreq->UpdateSalesmanCommission($commission_id, $emp_id, $item_id, $sub_group_id, $commission_percentage, $commission_type, $fixed_amount, $status);
        if ($RequestUpdate) {
            echo json_encode(array("Success" => true, "Msg" => 'Commission updated successfully'));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Failed to update commission'));
        }
    }

    if ((int) $_REQUEST['SalesManCommission'] == 6) { // Delete Commission
        $commission_id = $_GET['commission_id'];

        $RequestDelete = $clsfunreq->DeleteSalesmanCommission($commission_id);
        if ($RequestDelete) {
            echo json_encode(array("Success" => true, "Msg" => 'Commission deleted successfully'));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Failed to delete commission'));
        }
    }

    if ((int) $_REQUEST['SalesManCommission'] == 7) { // Get All Commissions
        $GetCommissions = $clsfunreq->GetAllSalesmanCommissions();
        $GetCommissionsRes = array();
        while ($rows = mysqli_fetch_assoc($GetCommissions)) {
            $GetCommissionsRes[] = $rows;
        }
        if ($GetCommissions) {
            echo json_encode(array("Success" => true, "Data" => $GetCommissionsRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Commissions Found'));
        }
    }

    if ((int) $_REQUEST['SalesManCommission'] == 8) { // Get Commissions by Salesman
        $emp_id = $_GET['emp_id'];

        $GetCommissions = $clsfunreq->GetCommissionsBySalesman($emp_id);
        $GetCommissionsRes = array();
        while ($rows = mysqli_fetch_assoc($GetCommissions)) {
            $GetCommissionsRes[] = $rows;
        }
        if ($GetCommissions) {
            echo json_encode(array("Success" => true, "Data" => $GetCommissionsRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Commissions Found'));
        }
    }

    if ((int) $_REQUEST['SalesManCommission'] == 9) { // Get Commission Report
        $emp_id = isset($_GET['emp_id']) ? $_GET['emp_id'] : '';
        $from_date = $_GET['from_date'];
        $to_date = $_GET['to_date'];

        $GetReport = $clsfunreq->GetCommissionReport($emp_id, $from_date, $to_date);
        $GetReportRes = array();
        while ($rows = mysqli_fetch_assoc($GetReport)) {
            $GetReportRes[] = $rows;
        }
        if ($GetReport) {
            echo json_encode(array("Success" => true, "Data" => $GetReportRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Commission Data Found'));
        }
    }

    if ((int) $_REQUEST['SalesManCommission'] == 10) { // Get Commission Summary
        $from_date = $_GET['from_date'];
        $to_date = $_GET['to_date'];

        $GetSummary = $clsfunreq->GetCommissionSummary($from_date, $to_date);
        $GetSummaryRes = array();
        while ($rows = mysqli_fetch_assoc($GetSummary)) {
            $GetSummaryRes[] = $rows;
        }
        if ($GetSummary) {
            echo json_encode(array("Success" => true, "Data" => $GetSummaryRes));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'No Commission Summary Found'));
        }
    }

    if ((int) $_REQUEST['SalesManCommission'] == 11) { // Mark Commission as Paid
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $log_ids = $row['log_ids']; // Array of log IDs

        $RequestUpdate = $clsfunreq->MarkCommissionAsPaid($log_ids);
        if ($RequestUpdate) {
            echo json_encode(array("Success" => true, "Msg" => 'Commission marked as paid successfully'));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Failed to mark commission as paid'));
        }
    }

    if ((int) $_REQUEST['SalesManCommission'] == 12) { // Process Sales Commission (called from sales save)
        $getjson = $_GET['json'];
        $row = json_decode($getjson, true);
        $sale_invoice_id = $row['sale_invoice_id'];
        $emp_id = $row['emp_id'];
        $sale_date = $row['sale_date'];
        $sale_items = $row['sale_items']; // Array of sold items with amounts

        $totalCommission = 0;
        $processedCount = 0;

        foreach ($sale_items as $item) {
            $item_id = $item['item_id'];
            $sale_amount = $item['sale_amount'];

            // Get commission details for this item and salesman
            $commissionQuery = $clsfunreq->GetCommissionByItemAndSalesman($item_id, $emp_id);
            if ($commissionQuery && mysqli_num_rows($commissionQuery) > 0) {
                $commission = mysqli_fetch_assoc($commissionQuery);

                // Calculate commission amount
                $commission_amount = $clsfunreq->CalculateCommissionAmount(
                    $sale_amount,
                    $commission['CommissionPercentage'],
                    $commission['CommissionType'],
                    $commission['FixedAmount']
                );

                // Log commission transaction
                $logResult = $clsfunreq->LogCommissionTransaction(
                    $commission['CommissionId'],
                    $emp_id,
                    $sale_invoice_id,
                    $item_id,
                    $sale_amount,
                    $commission_amount,
                    $commission['CommissionPercentage'],
                    $sale_date
                );

                if ($logResult) {
                    $totalCommission += $commission_amount;
                    $processedCount++;
                }
            }
        }

        if ($processedCount > 0) {
            echo json_encode(array(
                "Success" => true,
                "Msg" => 'Commission processed successfully',
                "TotalCommission" => $totalCommission,
                "ProcessedItems" => $processedCount
            ));
        } else {
            echo json_encode(array("Success" => true, "Msg" => 'No commission applicable for this sale'));
        }
    }
    if ((int) $_REQUEST['SalesManCommission'] == 13) { // Get SalesMan by Comid,Locid
        {
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];
            $GetSalesMan = $clsfunreq->GetSalesManByComidLocid($comid, $locid);
            $GetSalesManRes = array();
            while ($rows = mysqli_fetch_assoc($GetSalesMan)) {
                $GetSalesManRes[] = $rows;
            }
            if ($GetSalesMan) {
                echo json_encode(array("Success" => true, "Data" => $GetSalesManRes));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'No SalesMan Found'));
            }
        }
    }
    if ((int) $_REQUEST['SalesManCommission'] == 14) { // Delete Commission
        $salesman_id = $_GET['salesman_id'];

        $RequestDelete = $clsfunreq->DeleteSalesmanById($salesman_id);
        if ($RequestDelete) {
            echo json_encode(array("Success" => true, "Msg" => 'Commission deleted successfully'));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Failed to delete commission'));
        }
    }

    if ((int) $_REQUEST['SalesManCommission'] == 15) { // Update Employee Location
        $getjson = file_get_contents("php://input");
        $row = json_decode($getjson, true);
        $emp_id = $row['EmployeeId'];
        $new_loc_id = $row['NewLocId'];

        $RequestUpdate = $clsfunreq->UpdateEmployeeLocation($emp_id, $new_loc_id);
        if ($RequestUpdate) {
            echo json_encode(array("Success" => true, "Msg" => 'Employee location updated successfully'));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Failed to update employee location'));
        }
    }
    if ((int) $_REQUEST['SalesManCommission'] == 16) { // Bulk Update Employee Positions
        $jsonData = file_get_contents("php://input");
        $data = json_decode($jsonData, true);

        // Validate input data
        if (!$data || empty($data)) {
            echo json_encode(array("Success" => false, "Msg" => 'No data received'));
            exit;
        }

        $result = $clsfunreq->BulkUpdateEmployeePositions($data);
        if ($result) {
            echo json_encode(array("Success" => true, "Msg" => 'Employee positions updated successfully for ' . count($data) . ' employees'));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Failed to update employee positions'));
        }
    }
    if ((int) $_REQUEST['SalesManCommission'] == 17) { // Update Commission On Sales
        $salesman_id = $_GET['salesman_id'];
        $RequestUpdate = $clsfunreq->SpUpdateSalesmanCommission($salesman_id);
        if ($RequestUpdate) {
            echo json_encode(array("Success" => true, "Msg" => 'Commission Updated successfully'));
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Failed to Updated commission'));
        }
    }
}
//Mgmt Request
elseif (isset($_REQUEST['MgmtRequest'])) {

    // Temporary error handling for debugging
    try {
        // Log the request for debugging
        error_log("MgmtRequest received: " . $_REQUEST['MgmtRequest']);
        error_log("Full request: " . print_r($_REQUEST, true));

        if ((int) $_REQUEST['MgmtRequest'] == 1) { // Get Management Process Related
            $comid = $_REQUEST['Comid'] ?? 0;
            $locid = $_REQUEST['Locid'] ?? 0;
            $PMID = $_REQUEST['PmId'] ?? 0;
            error_log("Processing MgmtRequest=1 with Comid: $comid, Locid: $locid");

            // Check if this is a POS Master operation
            if (isset($_REQUEST['Action']) && $_REQUEST['Action'] == 'POS_MASTER') {
                error_log("POS_MASTER operation detected");

                if (isset($_REQUEST['Operation']) && $_REQUEST['Operation'] == 'INSERT') {
                    error_log("INSERT operation requested");
                    // Validate company and location exists before insert
                    if (!$clsfunreq->ValidateCompanyLocationExists($comid, $locid)) {
                        echo json_encode(array("Success" => false, "Msg" => 'Invalid Company ID or Location ID. Please verify the company and location exist and are active.'));
                        return;
                    }

                    // Insert new POS Master record
                    $pm_machine_name = $_REQUEST['PM_MACHINE_NAME'] ?? '';
                    $pm_business_date = $_REQUEST['PM_BUINESS_DATE'] ?? '';
                    $pm_trans_no = $_REQUEST['PM_TRANS_NO'] ?? '';
                    $pm_user_id = $_REQUEST['PM_USER_ID'] ?? 0;
                    $psr_bill_number = $_REQUEST['PSR_BILL_NUMBER'] ?? '';
                    $pm_day_no = $_REQUEST['PM_DAY_NO'] ?? 0;
                    $pm_shift_no = $_REQUEST['PM_SHIFT_NO'] ?? 0;
                    $pm_day_st = $_REQUEST['PM_DAY_ST'] ?? '';
                    $pm_shift_st = $_REQUEST['PM_SHIFT_ST'] ?? '';
                    $pm_prefix = $_REQUEST['PM_PREFIX'] ?? '';
                    $pm_batch_number = $_REQUEST['PM_BATCH_NUMBER'] ?? '';
                    $pm_mailstatus = $_REQUEST['PM_MAILSTATUS'] ?? '';
                    $pm_monthdate = $_REQUEST['PM_MONTHDATE'] ?? '';
                    $pm_autoupdate = $_REQUEST['PM_AUTOUPDATE'] ?? '';
                    $pm_webid = $_REQUEST['PM_WEBID'] ?? '';
                    $pm_restid = $_REQUEST['PM_RESTID'] ?? '';

                    $insertResult = $clsfunreq->InsertPosMaster(
                        $pm_machine_name,
                        $pm_business_date,
                        $pm_trans_no,
                        $pm_user_id,
                        $psr_bill_number,
                        $pm_day_no,
                        $pm_shift_no,
                        $pm_day_st,
                        $pm_shift_st,
                        $pm_prefix,
                        $comid,
                        $locid,
                        $pm_batch_number,
                        $pm_mailstatus,
                        $pm_monthdate,
                        $pm_autoupdate,
                        $pm_webid,
                        $pm_restid
                    );

                    if ($insertResult) {
                        echo json_encode(array("Success" => true, "Msg" => 'POS Master record inserted successfully'));
                    } else {
                        echo json_encode(array("Success" => false, "Msg" => 'Failed to insert POS Master record'));
                    }
                } elseif (isset($_REQUEST['Operation']) && $_REQUEST['Operation'] == 'UPDATE') {
                    // Update existing POS Master record
                    $pm_id = $_REQUEST['PM_ID'] ?? 0;
                    $pm_machine_name = $_REQUEST['PM_MACHINE_NAME'] ?? '';
                    $pm_business_date = $_REQUEST['PM_BUINESS_DATE'] ?? '';
                    $pm_trans_no = $_REQUEST['PM_TRANS_NO'] ?? '';
                    $pm_user_id = $_REQUEST['PM_USER_ID'] ?? 0;
                    $psr_bill_number = $_REQUEST['PSR_BILL_NUMBER'] ?? '';
                    $pm_day_no = $_REQUEST['PM_DAY_NO'] ?? 0;
                    $pm_shift_no = $_REQUEST['PM_SHIFT_NO'] ?? 0;
                    $pm_day_st = $_REQUEST['PM_DAY_ST'] ?? '';
                    $pm_shift_st = $_REQUEST['PM_SHIFT_ST'] ?? '';
                    $pm_prefix = $_REQUEST['PM_PREFIX'] ?? '';
                    $pm_batch_number = $_REQUEST['PM_BATCH_NUMBER'] ?? '';
                    $pm_mailstatus = $_REQUEST['PM_MAILSTATUS'] ?? '';
                    $pm_monthdate = $_REQUEST['PM_MONTHDATE'] ?? '';
                    $pm_autoupdate = $_REQUEST['PM_AUTOUPDATE'] ?? '';
                    $pm_webid = $_REQUEST['PM_WEBID'] ?? '';
                    $pm_restid = $_REQUEST['PM_RESTID'] ?? '';

                    // Check if record exists before updating
                    if ($clsfunreq->CheckPosMasterExists($pm_id, $comid, $locid)) {
                        $updateResult = $clsfunreq->UpdatePosMaster(
                            $pm_id,
                            $pm_machine_name,
                            $pm_business_date,
                            $pm_trans_no,
                            $pm_user_id,
                            $psr_bill_number,
                            $pm_day_no,
                            $pm_shift_no,
                            $pm_day_st,
                            $pm_shift_st,
                            $pm_prefix,
                            $comid,
                            $locid,
                            $pm_batch_number,
                            $pm_mailstatus,
                            $pm_monthdate,
                            $pm_autoupdate,
                            $pm_webid,
                            $pm_restid
                        );

                        if ($updateResult) {
                            echo json_encode(array("Success" => true, "Msg" => 'POS Master record updated successfully'));
                        } else {
                            echo json_encode(array("Success" => false, "Msg" => 'Failed to update POS Master record'));
                        }
                    } else {
                        echo json_encode(array("Success" => false, "Msg" => 'POS Master record not found or access denied'));
                    }
                } elseif (isset($_REQUEST['Operation']) && $_REQUEST['Operation'] == 'GET') {
                    // Get POS Master records by company and location
                    $GetPosMaster = $clsfunreq->GetPosMasterByComidLocid($comid, $locid, $PMID);
                    $GetPosMasterRes = array();

                    if ($GetPosMaster && mysqli_num_rows($GetPosMaster) > 0) {
                        while ($rows = mysqli_fetch_assoc($GetPosMaster)) {
                            $GetPosMasterRes[] = $rows;
                        }
                        echo json_encode(array("Success" => true, "Data" => $GetPosMasterRes, "Msg" => "Data retrieved successfully"));
                    } else {
                        echo json_encode(array("Success" => false, "Msg" => 'No POS Master records found', "Data" => array()));
                    }
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Invalid Operation specified'));
                }
            }
        }
        if ((int) $_REQUEST['MgmtRequest'] === 2) {
            // Update PM_TRANS_NO management request
            $pm_id = $_REQUEST['PM_ID'] ?? 0;
            $pm_trans_no = $_REQUEST['PM_TRANS_NO'] ?? 0;
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];
            // Check if record exists before updating
            if ($clsfunreq->CheckPosMasterExists($pm_id, $comid, $locid)) {
                $updateResult = $clsfunreq->UpdateTransNo($pm_id, $pm_trans_no, $comid, $locid);

                if ($updateResult) {
                    echo json_encode(array("Success" => true, "Msg" => 'Transaction Number updated successfully'));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Failed to update Transaction Number'));
                }
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'POS Master record not found or access denied'));
            }
        }
        if ((int) $_REQUEST['MgmtRequest'] === 3) {
            // Update PSR_BILL_NUMBER management request
            $pm_id = $_REQUEST['PM_ID'] ?? 0;
            $pm_bill_number = $_REQUEST['PM_BILL_NUMBER'] ?? 0;
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];
            // Check if record exists before updating
            if ($clsfunreq->CheckPosMasterExists($pm_id, $comid, $locid)) {
                $updateResult = $clsfunreq->UpdateBillNumber($pm_id, $pm_bill_number, $comid, $locid);

                if ($updateResult) {
                    echo json_encode(array("Success" => true, "Msg" => 'Bill Number updated successfully'));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Failed to update Bill Number'));
                }
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'POS Master record not found or access denied'));
            }
        }
        if ((int) $_REQUEST['MgmtRequest'] === 4) {
            // Update PM_BATCH_NUMBER management request
            $pm_id = $_REQUEST['PM_ID'] ?? 0;
            $pm_batch_number = $_REQUEST['PM_BATCH_NUMBER'] ?? 0;
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];
            // Check if record exists before updating
            if ($clsfunreq->CheckPosMasterExists($pm_id, $comid, $locid)) {
                $updateResult = $clsfunreq->UpdateBatchNumber($pm_id, $pm_batch_number, $comid, $locid);

                if ($updateResult) {
                    echo json_encode(array("Success" => true, "Msg" => 'Batch Number updated successfully'));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Failed to update Batch Number'));
                }
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'POS Master record not found or access denied'));
            }
        }
        if ((int) $_REQUEST['MgmtRequest'] === 5) {
            // Update PM_AUTOUPDATE management request
            $pm_id = $_REQUEST['PM_ID'] ?? 0;
            $pm_auto_update = $_REQUEST['PM_AUTOUPDATE'] ?? 0;
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];
            // Check if record exists before updating
            if ($clsfunreq->CheckPosMasterExists($pm_id, $comid, $locid)) {
                $updateResult = $clsfunreq->UpdateAutoUpdate($pm_id, $pm_auto_update, $comid, $locid);

                if ($updateResult) {
                    echo json_encode(array("Success" => true, "Msg" => 'Auto Update setting updated successfully'));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Failed to update Auto Update setting'));
                }
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'POS Master record not found or access denied'));
            }
        }
        if ((int) $_REQUEST['MgmtRequest'] === 6) {
            // Update PM_MAILSTATUS management request
            $pm_id = $_REQUEST['PM_ID'] ?? 0;
            $pm_mail_status = $_REQUEST['PM_MAILSTATUS'] ?? 0;
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];
            // Check if record exists before updating
            if ($clsfunreq->CheckPosMasterExists($pm_id, $comid, $locid)) {
                $updateResult = $clsfunreq->UpdateMailStatus($pm_id, $pm_mail_status, $comid, $locid);

                if ($updateResult) {
                    echo json_encode(array("Success" => true, "Msg" => 'Mail Status updated successfully'));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Failed to update Mail Status'));
                }
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'POS Master record not found or access denied'));
            }
        }
        if ((int) $_REQUEST['MgmtRequest'] === 7) {
            // Update PM_MONTHDATE management request
            $pm_id = $_REQUEST['PM_ID'] ?? 0;
            $pm_month_date = $_REQUEST['PM_MONTHDATE'] ?? 0;
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];
            // Check if record exists before updating
            if ($clsfunreq->CheckPosMasterExists($pm_id, $comid, $locid)) {
                $updateResult = $clsfunreq->UpdateMonthDate($pm_id, $pm_month_date, $comid, $locid);

                if ($updateResult) {
                    echo json_encode(array("Success" => true, "Msg" => 'Month Date updated successfully'));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Failed to update Month Date'));
                }
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'POS Master record not found or access denied'));
            }
        }
        if ((int) $_REQUEST['MgmtRequest'] === 8) {
            //Get All Data management request
            $pm_id = $_REQUEST['PM_ID'] ?? 0;
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];
            // Check if record exists before retrieving
            if ($clsfunreq->CheckPosMasterExists($pm_id, $comid, $locid)) {
                $getData = $clsfunreq->GetAllData($pm_id, $comid, $locid);

                if ($getData) {
                    // Wrap single record in array for DataTable conversion
                    $dataArray = array($getData);
                    echo json_encode(array("Success" => true, "Data" => $dataArray, "Msg" => 'Data retrieved successfully'));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Failed to retrieve data'));
                }
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'POS Master record not found or access denied'));
            }
        }
        if ((int) $_REQUEST['MgmtRequest'] === 9) {
            // Main Group Policy management request
            $comid = $_REQUEST['Comid'] ?? 0;
            $locid = $_REQUEST['Locid'] ?? 0;
            $operation = $_REQUEST['Operation'] ?? '';

            switch (strtoupper($operation)) {
                case 'GET':
                    // Get Main Groups by ComId, LocId
                    $mainid = $_REQUEST['MainId'] ?? 0;
                    $GetMainGroups = $clsfunreq->GetMainGroupsByComidLocid($comid, $locid, $mainid);
                    $GetMainGroupsRes = array();

                    if ($GetMainGroups && mysqli_num_rows($GetMainGroups) > 0) {
                        while ($rows = mysqli_fetch_assoc($GetMainGroups)) {
                            $GetMainGroupsRes[] = $rows;
                        }
                        echo json_encode(array("Success" => true, "Data" => $GetMainGroupsRes, "Msg" => "Main Groups retrieved successfully"));
                    } else {
                        echo json_encode(array("Success" => false, "Msg" => 'No Main Groups found', "Data" => array()));
                    }
                    break;

                case 'INSERT':
                    // Insert new Main Group
                    $mainname = $_REQUEST['MainName'] ?? '';
                    $mainstatus = $_REQUEST['MainStatus'] ?? 1;

                    if (empty($mainname)) {
                        echo json_encode(array("Success" => false, "Msg" => 'Main Group Name is required'));
                        break;
                    }

                    // Check if main group already exists
                    if ($clsfunreq->CheckMainGroupExists($mainname, $comid, $locid)) {
                        echo json_encode(array("Success" => false, "Msg" => 'Main Group with this name already exists'));
                    } else {
                        $insertResult = $clsfunreq->InsertMainGroup($mainname, $mainstatus, $comid, $locid);
                        if ($insertResult) {
                            echo json_encode(array("Success" => true, "Msg" => 'Main Group created successfully', "Data" => $insertResult));
                        } else {
                            echo json_encode(array("Success" => false, "Msg" => 'Failed to create Main Group'));
                        }
                    }
                    break;

                case 'UPDATE':
                    // Update existing Main Group
                    $mainid = $_REQUEST['MainId'] ?? 0;
                    $mainname = $_REQUEST['MainName'] ?? '';
                    $mainstatus = $_REQUEST['MainStatus'] ?? 1;

                    if ($mainid <= 0) {
                        echo json_encode(array("Success" => false, "Msg" => 'Main Group ID is required'));
                        break;
                    }

                    if (empty($mainname)) {
                        echo json_encode(array("Success" => false, "Msg" => 'Main Group Name is required'));
                        break;
                    }

                    // Check if record exists and belongs to the specified ComId, LocId
                    if ($clsfunreq->CheckMainGroupOwnership($mainid, $comid, $locid)) {
                        $updateResult = $clsfunreq->UpdateMainGroup($mainid, $mainname, $mainstatus, $comid, $locid);
                        if ($updateResult) {
                            echo json_encode(array("Success" => true, "Msg" => 'Main Group updated successfully'));
                        } else {
                            echo json_encode(array("Success" => false, "Msg" => 'Failed to update Main Group'));
                        }
                    } else {
                        echo json_encode(array("Success" => false, "Msg" => 'Main Group not found or access denied'));
                    }
                    break;

                case 'DELETE':
                    // Delete Main Group
                    $mainid = $_REQUEST['MainId'] ?? 0;

                    if ($mainid <= 0) {
                        echo json_encode(array("Success" => false, "Msg" => 'Main Group ID is required'));
                        break;
                    }

                    // Check if record exists and belongs to the specified ComId, LocId
                    if ($clsfunreq->CheckMainGroupOwnership($mainid, $comid, $locid)) {
                        $deleteResult = $clsfunreq->DeleteMainGroup($mainid, $comid, $locid);
                        if ($deleteResult) {
                            echo json_encode(array("Success" => true, "Msg" => 'Main Group deleted successfully'));
                        } else {
                            echo json_encode(array("Success" => false, "Msg" => 'Failed to delete Main Group'));
                        }
                    } else {
                        echo json_encode(array("Success" => false, "Msg" => 'Main Group not found or access denied'));
                    }
                    break;
                case 'SELECTACTIVE':
                    // Get Main Groups for Select2 Dropdown by ComId, LocId
                    $GetMainGroups = $clsfunreq->GetMainGroupsForSelectActive($comid, $locid);
                    $GetMainGroupsRes = array();

                    if ($GetMainGroups && mysqli_num_rows($GetMainGroups) > 0) {
                        while ($rows = mysqli_fetch_assoc($GetMainGroups)) {
                            $GetMainGroupsRes[] = $rows;
                        }
                        echo json_encode(array("Success" => true, "Data" => $GetMainGroupsRes, "Msg" => "Main Groups retrieved successfully"));
                    } else {
                        echo json_encode(array("Success" => false, "Msg" => 'No Main Groups found', "Data" => array()));
                    }
                    break;
                default:
                    echo json_encode(array("Success" => false, "Msg" => 'Invalid Operation specified. Use GET, INSERT, UPDATE, or DELETE'));
                    break;
            }
        }
    } catch (Exception $e) {
        error_log("MgmtRequest Exception: " . $e->getMessage());
        error_log("Stack trace: " . $e->getTraceAsString());
        echo json_encode(array("Success" => false, "Msg" => 'Server error: ' . $e->getMessage()));
    }
}
// ShiftCloseRequest
elseif (isset($_REQUEST['ShiftCloseRequest'])) {
    try {
        if ((int) $_REQUEST['ShiftCloseRequest'] === 1) {
            // Create New Shift
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];
            $psc_opbalance = $_REQUEST['PSC_OPBALANCE'] ?? 0;
            $psc_shiftno = $_REQUEST['PSC_SHIFTNO'] ?? 1;
            $psc_dayno = $_REQUEST['PSC_DAYNO'] ?? 1;
            $psc_pcname = $_REQUEST['PSC_PCNAME'] ?? '';
            $psc_userid = $_REQUEST['PSC_USERID'] ?? '';

            error_log("Creating new shift - Comid: $comid, Locid: $locid, ShiftNo: $psc_shiftno");

            $result = $clsfunreq->CreateNewShift($comid, $locid, $psc_opbalance, $psc_shiftno, $psc_dayno, $psc_pcname, $psc_userid);

            if ($result) {
                echo json_encode(array(
                    "Success" => true,
                    "Data" => $result,
                    "Msg" => "New shift created successfully"
                ));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Failed to create new shift'));
            }
        }

        if ((int) $_REQUEST['ShiftCloseRequest'] === 2) {
            // Update Shift Close and increment shift number in POS_MASTER
            $psc_id = $_REQUEST['PSC_ID'];
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];
            $pm_id = $_REQUEST['PM_ID'] ?? 0; // POS Master ID for updating shift number

            // Validate shift before closing
            $validation = $clsfunreq->ValidateShiftBeforeClose($psc_id, $comid, $locid);

            if (!$validation['exists']) {
                echo json_encode(array("Success" => false, "Msg" => $validation['message']));
                return;
            }

            if (!$validation['can_close']) {
                echo json_encode(array(
                    "Success" => false,
                    "Msg" => $validation['message'],
                    "ShiftState" => $validation['state'],
                    "ShiftNo" => $validation['shift_no']
                ));
                return;
            }

            // Collect shift close data
            $shiftCloseData = array();
            $fields = array(
                'psc_todaysales',
                'psc_totdiscount',
                'psc_tottax',
                'psc_netamt',
                'psc_servicetax',
                'psc_todayin',
                'psc_todayout',
                'psc_todaybanking',
                'psc_clsbalance',
                'psc_totbills',
                'psc_cancelamt',
                'psc_creditsales',
                'psc_opendrawer',
                'psc_clsdrawer',
                'psc_smail',
                'psc_print',
                'psc_state' // Set to 'Close'
            );

            foreach ($fields as $field) {
                if (isset($_REQUEST[strtoupper($field)])) {
                    $shiftCloseData[$field] = $_REQUEST[strtoupper($field)];
                }
            }

            // Set state to 'Close'
            $shiftCloseData['psc_state'] = 'Close';

            error_log("Closing shift and updating POS Master - PSC_ID: $psc_id, PM_ID: $pm_id, Comid: $comid, Locid: $locid");

            // Close the shift
            $closeResult = $clsfunreq->UpdateShiftClose($psc_id, $comid, $locid, $shiftCloseData);

            if ($closeResult && $pm_id > 0) {
                // Increment shift number and set shift status to 'Close' in POS_MASTER
                $incrementResult = $clsfunreq->CloseShiftAndIncrement($pm_id, $comid, $locid);

                if ($incrementResult) {
                    echo json_encode(array(
                        "Success" => true,
                        "Msg" => "Shift #" . $validation['shift_no'] . " closed successfully and shift number incremented"
                    ));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Shift closed but failed to update POS Master shift number'));
                }
            } elseif ($closeResult) {
                echo json_encode(array("Success" => true, "Msg" => "Shift #" . $validation['shift_no'] . " closed successfully"));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Failed to close shift'));
            }
        }

        if ((int) $_REQUEST['ShiftCloseRequest'] === 3) {
            // Get Current Open Shift
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];

            error_log("Getting current open shift - Comid: $comid, Locid: $locid");

            $result = $clsfunreq->GetCurrentOpenShift($comid, $locid);

            if ($result) {
                echo json_encode(array(
                    "Success" => true,
                    "Data" => $result,
                    "Msg" => "Current open shift retrieved successfully"
                ));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'No open shift found'));
            }
        }

        if ((int) $_REQUEST['ShiftCloseRequest'] === 4) {
            // Get All Shifts for Company/Location
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];
            $limit = $_REQUEST['LIMIT'] ?? 50; // Default limit

            error_log("Getting shift data - Comid: $comid, Locid: $locid, Limit: $limit");

            $result = $clsfunreq->GetShiftData($comid, $locid, $limit);

            if ($result) {
                $shiftData = array();
                while ($row = mysqli_fetch_assoc($result)) {
                    $shiftData[] = $row;
                }

                if (!empty($shiftData)) {
                    echo json_encode(array(
                        "Success" => true,
                        "Data" => $shiftData,
                        "Msg" => "Shift data retrieved successfully"
                    ));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'No shift data found'));
                }
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Failed to retrieve shift data'));
            }
        }

        if ((int) $_REQUEST['ShiftCloseRequest'] === 5) {
            // Validate Shift Status (Check if shift can be closed)
            $psc_id = $_REQUEST['PSC_ID'];
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];

            error_log("Validating shift status - PSC_ID: $psc_id, Comid: $comid, Locid: $locid");

            $validation = $clsfunreq->ValidateShiftBeforeClose($psc_id, $comid, $locid);

            if ($validation['exists']) {
                echo json_encode(array(
                    "Success" => true,
                    "CanClose" => $validation['can_close'],
                    "ShiftState" => $validation['state'],
                    "ShiftNo" => $validation['shift_no'],
                    "Msg" => $validation['message']
                ));
            } else {
                echo json_encode(array(
                    "Success" => false,
                    "CanClose" => false,
                    "Msg" => $validation['message']
                ));
            }
        }

        if ((int) $_REQUEST['ShiftCloseRequest'] === 6) {
            // Validate Current Shift and Create if Needed
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];
            $userid = $_REQUEST['USERID'] ?? '';
            $pcname = $_REQUEST['PCNAME'] ?? '';

            error_log("Validating current shift and auto-create - Comid: $comid, Locid: $locid, UserID: $userid, PCName: $pcname");

            $result = $clsfunreq->ValidateCurrentShiftAndCreate($comid, $locid, $userid, $pcname);

            if ($result['success']) {
                echo json_encode(array(
                    "Success" => true,
                    "Action" => $result['action'],
                    "Message" => $result['message'],
                    "Data" => isset($result['shift_data']) ? $result['shift_data'] : null,
                    "ShiftNo" => isset($result['shift_no']) ? $result['shift_no'] : null,
                    "DayNo" => isset($result['day_no']) ? $result['day_no'] : null,
                    "PM_ID" => isset($result['pm_id']) ? $result['pm_id'] : null
                ));
            } else {
                echo json_encode(array(
                    "Success" => false,
                    "Action" => $result['action'],
                    "Msg" => $result['message']
                ));
            }
        }
    } catch (Exception $e) {
        error_log("ShiftCloseRequest Exception: " . $e->getMessage());
        error_log("Stack trace: " . $e->getTraceAsString());
        echo json_encode(array("Success" => false, "Msg" => 'Server error: ' . $e->getMessage()));
    }
} elseif (isset($_REQUEST["DayCloseRequest"])) {
    try {
        if ((int) $_REQUEST['DayCloseRequest'] === 1) {
            // Create New Day
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];
            $psd_opbalance = $_REQUEST['PSD_OPBALANCE'] ?? 0;
            $psd_shiftno = $_REQUEST['PSD_SHIFTNO'] ?? 1;
            $psd_dayno = $_REQUEST['PSD_DAYNO'] ?? 1;
            $psd_pcname = $_REQUEST['PSD_PCNAME'] ?? '';
            $psd_userid = $_REQUEST['PSD_USERID'] ?? '';

            error_log("Creating new day - Comid: $comid, Locid: $locid, DayNo: $psd_dayno");

            $result = $clsfunreq->CreateNewDay($comid, $locid, $psd_opbalance, $psd_shiftno, $psd_dayno, $psd_pcname, $psd_userid);

            if ($result) {
                echo json_encode(array(
                    "Success" => true,
                    "Data" => $result,
                    "Msg" => "New day created successfully"
                ));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Failed to create new day'));
            }
        }

        if ((int) $_REQUEST['DayCloseRequest'] === 2) {
            // Update Day Close and increment day number in POS_MASTER
            $psd_id = $_REQUEST['PSD_ID'];
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];
            $pm_id = $_REQUEST['PM_ID'] ?? 0; // POS Master ID for updating day number

            // Validate day before closing
            $validation = $clsfunreq->ValidateDayBeforeClose($psd_id, $comid, $locid);

            if (!$validation['exists']) {
                echo json_encode(array("Success" => false, "Msg" => $validation['message']));
                return;
            }

            if (!$validation['can_close']) {
                echo json_encode(array(
                    "Success" => false,
                    "Msg" => $validation['message'],
                    "DayState" => $validation['state'],
                    "DayNo" => $validation['day_no']
                ));
                return;
            }

            // Collect day close data
            $dayCloseData = array();
            $fields = array(
                'psd_todaysales',
                'psd_totdiscount',
                'psd_tottax',
                'psd_netamt',
                'psd_servicetax',
                'psd_todayin',
                'psd_todayout',
                'psd_todaybanking',
                'psd_clsbalance',
                'psd_totbills',
                'psd_cancelamt',
                'psd_creditsales',
                'psd_opendrawer',
                'psd_clsdrawer',
                'psd_mail',
                'psd_print',
                'psd_state' // Set to 'Close'
            );

            foreach ($fields as $field) {
                if (isset($_REQUEST[strtoupper($field)])) {
                    $dayCloseData[$field] = $_REQUEST[strtoupper($field)];
                }
            }

            // Set state to 'Close'
            $dayCloseData['psd_state'] = 'Close';

            error_log("Closing day and updating POS Master - PSD_ID: $psd_id, PM_ID: $pm_id, Comid: $comid, Locid: $locid");

            // Close the day
            $closeResult = $clsfunreq->UpdateDayClose($psd_id, $comid, $locid, $dayCloseData);

            if ($closeResult && $pm_id > 0) {
                // Increment day number and set day status to 'Close' in POS_MASTER
                $incrementResult = $clsfunreq->CloseDayAndIncrement($pm_id, $comid, $locid);

                if ($incrementResult) {
                    echo json_encode(array(
                        "Success" => true,
                        "Msg" => "Day #" . $validation['day_no'] . " closed successfully and day number incremented"
                    ));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Day closed but failed to update POS Master day number'));
                }
            } elseif ($closeResult) {
                echo json_encode(array("Success" => true, "Msg" => "Day #" . $validation['day_no'] . " closed successfully"));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Failed to close day'));
            }
        }

        if ((int) $_REQUEST['DayCloseRequest'] === 3) {
            // Get Current Open Day
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];

            error_log("Getting current open day - Comid: $comid, Locid: $locid");

            $result = $clsfunreq->GetCurrentOpenDay($comid, $locid);

            if ($result) {
                echo json_encode(array(
                    "Success" => true,
                    "Data" => $result,
                    "Msg" => "Current open day retrieved successfully"
                ));
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'No open day found'));
            }
        }

        if ((int) $_REQUEST['DayCloseRequest'] === 4) {
            // Get All Days for Company/Location
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];
            $limit = $_REQUEST['LIMIT'] ?? 50; // Default limit

            error_log("Getting day data - Comid: $comid, Locid: $locid, Limit: $limit");

            $result = $clsfunreq->GetDayData($comid, $locid, $limit);

            if ($result) {
                $dayData = array();
                while ($row = mysqli_fetch_assoc($result)) {
                    $dayData[] = $row;
                }

                if (!empty($dayData)) {
                    echo json_encode(array(
                        "Success" => true,
                        "Data" => $dayData,
                        "Msg" => "Day data retrieved successfully"
                    ));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'No day data found'));
                }
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Failed to retrieve day data'));
            }
        }

        if ((int) $_REQUEST['DayCloseRequest'] === 5) {
            // Validate Day Status (Check if day can be closed)
            $psd_id = $_REQUEST['PSD_ID'];
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];

            error_log("Validating day status - PSD_ID: $psd_id, Comid: $comid, Locid: $locid");

            $validation = $clsfunreq->ValidateDayBeforeClose($psd_id, $comid, $locid);

            if ($validation['exists']) {
                echo json_encode(array(
                    "Success" => true,
                    "CanClose" => $validation['can_close'],
                    "DayState" => $validation['state'],
                    "DayNo" => $validation['day_no'],
                    "Msg" => $validation['message']
                ));
            } else {
                echo json_encode(array(
                    "Success" => false,
                    "CanClose" => false,
                    "Msg" => $validation['message']
                ));
            }
        }

        if ((int) $_REQUEST['DayCloseRequest'] === 6) {
            // Validate Current Day and Create if Needed
            $comid = $_REQUEST['Comid'];
            $locid = $_REQUEST['Locid'];
            $userid = $_REQUEST['USERID'] ?? '';
            $pcname = $_REQUEST['PCNAME'] ?? '';

            error_log("Validating current day and auto-create - Comid: $comid, Locid: $locid, UserID: $userid, PCName: $pcname");

            $result = $clsfunreq->ValidateCurrentDayAndCreate($comid, $locid, $userid, $pcname);

            if ($result['success']) {
                echo json_encode(array(
                    "Success" => true,
                    "Action" => $result['action'],
                    "Message" => $result['message'],
                    "Data" => isset($result['day_data']) ? $result['day_data'] : null,
                    "DayNo" => isset($result['day_no']) ? $result['day_no'] : null,
                    "ShiftNo" => isset($result['shift_no']) ? $result['shift_no'] : null,
                    "PM_ID" => isset($result['pm_id']) ? $result['pm_id'] : null
                ));
            } else {
                echo json_encode(array(
                    "Success" => false,
                    "Action" => $result['action'],
                    "Msg" => $result['message']
                ));
            }
        }
    } catch (Exception $e) {
        error_log("DayCloseRequest Exception: " . $e->getMessage());
        error_log("Stack trace: " . $e->getTraceAsString());
        echo json_encode(array("Success" => false, "Msg" => 'Server error: ' . $e->getMessage()));
    }
} elseif (isset($_REQUEST["AttRequest"])) {
    /*
     * Employee Fingerprint Management API
     * AttRequest=1: Register/Save new fingerprint template
     * AttRequest=2: Get fingerprint template for verification
     * AttRequest=3: Get existing fingerprints for an employee
     * AttRequest=4: Get all employees with fingerprint templates
     * AttRequest=5: Delete all fingerprint templates for an employee
     */
    try {
        if ((int) $_REQUEST['AttRequest'] === 1) {
            $rawInput = file_get_contents("php://input");
            $data = json_decode($rawInput, true);

            if ($data === null) {
                echo json_encode([
                    "Success" => false,
                    "Msg" => "Failed to parse JSON. Check Content-Type and JSON format.",
                    "RawInput" => $rawInput
                ]);
                exit;
            }

            if (!isset($data["EmpId"]) || !isset($data["Template"])) {
                echo json_encode([
                    "Success" => false,
                    "Msg" => "Invalid data: EmpId and Template (Base64) are required"
                ]);
                exit;
            }

            $empId = (int)$data["EmpId"];
            $templateBase64 = trim($data["Template"]);
            $fingerName = isset($data["FingerName"]) ? $data["FingerName"] : 'Finger1';
            $fingerType = isset($data["FingerType"]) ? $data["FingerType"] : 'Employee';

            if ($empId <= 0) {
                echo json_encode([
                    "Success" => false,
                    "Msg" => "Invalid EmpId value"
                ]);
                exit;
            }

            // Decode Base64 → binary fingerprint template
            $templateBinary = base64_decode($templateBase64, true);
            if ($templateBinary === false) {
                echo json_encode([
                    "Success" => false,
                    "Msg" => "Failed to decode Base64 Template"
                ]);
                exit;
            }

            // Store binary data in DB (LONGBLOB)
            try {
                $register = $clsfunreq->RegisterEmpFinger($empId, $templateBinary, $fingerName, $fingerType);

                if ($register) {
                    echo json_encode([
                        "Success" => true,
                        "Msg" => "Fingerprint template registered successfully for $fingerType - $fingerName",
                        "FingerprintId" => $register
                    ]);
                } else {
                    echo json_encode([
                        "Success" => false,
                        "Msg" => "Failed to register fingerprint in database"
                    ]);
                }
            } catch (Exception $e) {
                error_log("RegisterEmpFinger API Error: " . $e->getMessage());
                echo json_encode([
                    "Success" => false,
                    "Msg" => "Registration error: " . $e->getMessage()
                ]);
            }
        } elseif ((int) $_REQUEST['AttRequest'] === 2) {
            $data = json_decode(file_get_contents("php://input"), true);

            if ($data && isset($data["EmpId"])) {
                $empId = (int)$data["EmpId"];
                $fingerName = isset($data["FingerName"]) ? $data["FingerName"] : null;
                $fingerType = isset($data["FingerType"]) ? $data["FingerType"] : null;

                // Get binary fingerprint template from DB (LONGBLOB)
                $templateBinary = $clsfunreq->GetEmpFingerTemplate($empId, $fingerType, $fingerName);

                if ($templateBinary) {
                    // Convert binary to Base64 for JSON transport
                    $templateBase64 = base64_encode($templateBinary);

                    $msg = 'Fingerprint template retrieved successfully';
                    if ($fingerName && $fingerType) {
                        $msg .= " for {$fingerType} - {$fingerName}";
                    }

                    echo json_encode([
                        "Success" => true,
                        "Template" => $templateBase64,
                        "Msg" => $msg
                    ]);
                } else {
                    $msg = 'No fingerprint template found';
                    if ($fingerName && $fingerType) {
                        $msg .= " for {$fingerType} - {$fingerName}";
                    }
                    echo json_encode([
                        "Success" => false,
                        "Msg" => $msg
                    ]);
                }
            } else {
                echo json_encode([
                    "Success" => false,
                    "Msg" => "Invalid data: EmpId is required"
                ]);
            }
        } elseif ((int) $_REQUEST['AttRequest'] === 3) {
            $data = json_decode(file_get_contents("php://input"), true);

            // Default EmpId to 0 if missing/null
            $empId = isset($data["EmpId"]) ? (int)$data["EmpId"] : 0;
            $fingerType = isset($data["FingerType"]) ? $data["FingerType"] : null;
            // Call function (0 = all employees, >0 = specific employee)
            $getData = $clsfunreq->GetAllEmpFingerprints($empId, $fingerType);

            if ($getData && is_array($getData)) {
                $dataArray = [];

                foreach ($getData as $row) {
                    if (isset($row['finger_template']) && $row['finger_template'] !== null) {
                        // Convert BLOB to Base64 for JSON
                        $row['finger_template'] = base64_encode($row['finger_template']);
                    }
                    $dataArray[] = $row;
                }

                echo json_encode([
                    "Success" => true,
                    "Data" => $dataArray,
                    "Msg" => "Data retrieved successfully"
                ]);
            } else {
                echo json_encode([
                    "Success" => false,
                    "Msg" => "No fingerprint records found"
                ]);
            }
        }
        //Delete Employee Fingerprint Template(s)
        elseif ((int) $_REQUEST['AttRequest'] === 4) {
            $data = json_decode(file_get_contents("php://input"), true);

            if ($data && isset($data["EmpId"])) {
                $empId = $data["EmpId"];
                $delete = $clsfunreq->DeleteEmpFingerprint($empId);

                if ($delete) {
                    echo json_encode(array("Success" => true, "Msg" => 'All fingerprint templates deleted successfully for employee ID: ' . $empId));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Failed to delete fingerprint templates or no templates found for employee ID: ' . $empId));
                }
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Invalid data: EmpId is required for delete operation'));
            }
        }
        //Update Employee Fingerprint time
        elseif ((int) $_REQUEST['AttRequest'] === 5) {
            $data = json_decode(file_get_contents("php://input"), true);
            if ($data && isset($data["EmpId"]) && isset($data["ComId"]) && isset($data["LocId"]) && isset($data["PM_ID"]) && isset($data["Action"]) && isset($data["PunchTime"])) {
                $empId = (int)$data["EmpId"];
                $comId = (int)$data["ComId"];
                $locId = (int)$data["LocId"];
                $pmId = (int)$data["PM_ID"];
                $action = trim($data["Action"]);
                $punchTime = trim($data["PunchTime"]);

                // Validate punch time format
                $dateTime = date_create($punchTime);
                if (!$dateTime) {
                    echo json_encode(array("Success" => false, "Msg" => 'Invalid PunchTime format. Use YYYY-MM-DD HH:MM:SS'));
                    exit;
                }
                $formattedPunchTime = $dateTime->format('Y-m-d H:i:s');

                try {
                    $markAttendance = $clsfunreq->MarkEmployeeAttendance(
                        $empId,
                        $comId,
                        $locId,
                        $pmId,
                        $action,
                        $formattedPunchTime
                    );

                    if (is_array($markAttendance) && isset($markAttendance['status'])) {
                        switch ($markAttendance['status']) {
                            case 'success':
                                echo json_encode([
                                    "Success" => true,
                                    "Msg" => "Attendance marked successfully for employee ID: $empId",
                                    "Data" => $markAttendance
                                ]);
                                break;

                            case 'already punched':
                                echo json_encode([
                                    "Success" => false,
                                    "Msg" => "Attendance already punched for employee ID: $empId",
                                    "Data" => $markAttendance
                                ]);
                                break;

                            case 'error':
                                echo json_encode([
                                    "Success" => false,
                                    "Msg" => "Invalid punch sequence for employee ID: $empId",
                                    "Data" => $markAttendance
                                ]);
                                break;

                            default:
                                echo json_encode([
                                    "Success" => false,
                                    "Msg" => "Unexpected status: " . $markAttendance['status'],
                                    "Data" => $markAttendance
                                ]);
                        }
                    } else {
                        echo json_encode([
                            "Success" => false,
                            "Msg" => "Failed to mark attendance for employee ID: $empId",
                            "Data" => $markAttendance
                        ]);
                    }
                } catch (Exception $e) {
                    error_log("MarkEmployeeAttendance API Error: " . $e->getMessage());
                    error_log("Stack trace: " . $e->getTraceAsString());
                    echo json_encode([
                        "Success" => false,
                        "Msg" => "Error marking attendance: " . $e->getMessage()
                    ]);
                }
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Invalid data: EmpId, ComId, LocId, PM_ID, Action, and PunchTime are required'));
            }
        } elseif ((int)$_REQUEST['AttRequest'] === 6) {
            // Get Attendance Report
            $data = json_decode(file_get_contents("php://input"), true);
            if ($data && isset($data["Mode"]) && isset($data["Date"]) && isset($data["ComId"]) && isset($data["LocId"])) {
                $mode = trim($data["Mode"]);
                $date = trim($data["Date"]);
                $comId = (int)$data["ComId"];
                $locId = (int)$data["LocId"];
                $empId = isset($data["EmpId"]) ? (int)$data["EmpId"] : 0;

                // Validate date format
                $dateObj = date_create($date);
                if (!$dateObj) {
                    echo json_encode(array("Success" => false, "Msg" => 'Invalid Date format. Use YYYY-MM-DD'));
                    exit;
                }
                $formattedDate = $dateObj->format('Y-m-d');

                try {
                    $reportData = $clsfunreq->GetAttendanceReport(
                        $mode,
                        $formattedDate,
                        $comId,
                        $locId,
                        $empId
                    );

                    if ($reportData && is_array($reportData)) {
                        echo json_encode([
                            "Success" => true,
                            "Msg" => "Attendance report retrieved successfully",
                            "Data" => $reportData
                        ]);
                    } else {
                        echo json_encode([
                            "Success" => false,
                            "Msg" => "No attendance records found",
                            "Data" => []
                        ]);
                    }
                } catch (Exception $e) {
                    error_log("GetAttendanceReport API Error: " . $e->getMessage());
                    error_log("Stack trace: " . $e->getTraceAsString());
                    echo json_encode([
                        "Success" => false,
                        "Msg" => "Error retrieving attendance report: " . $e->getMessage()
                    ]);
                }
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Invalid data: Mode, Date, ComId, and LocId are required'));
            }
        } elseif ((int)$_REQUEST['AttRequest'] === 7) {

            $getData = $clsfunreq->GetAllTimeProfiles();
            if ($getData && is_array($getData)) {
                echo json_encode([
                    "Success" => true,
                    "Msg" => "Time profiles retrieved successfully",
                    "Data" => $getData
                ]);
            } else {
                echo json_encode([
                    "Success" => false,
                    "Msg" => "No time profiles found",
                    "Data" => []
                ]);
            }
        } elseif ((int)$_REQUEST['AttRequest'] === 8) {
            $data = null;

            // Handle both POST and GET requests
            if ($_SERVER['REQUEST_METHOD'] == 'POST') {
                // Handle POST request
                $jsonData = file_get_contents("php://input");
                error_log("AttRequest=8 raw POST data: " . $jsonData);
                $data = json_decode($jsonData, true);
            } else {
                // Handle GET request - json parameter in URL
                if (isset($_GET['json'])) {
                    error_log("AttRequest=8 raw GET data: " . $_GET['json']);
                    $data = json_decode($_GET['json'], true);
                }
            }

            // Check for JSON decode errors
            if ($data === null && json_last_error() !== JSON_ERROR_NONE) {
                $jsonError = json_last_error_msg();
                error_log("AttRequest=8 JSON decode error: " . $jsonError);
                echo json_encode(array("Success" => false, "Msg" => "JSON decode error: " . $jsonError));
                exit;
            }

            // Log the received data for debugging
            error_log("AttRequest=8 received data: " . print_r($data, true));

            if ($data && isset($data["ProfileName"]) && isset($data["CheckInStart"]) && isset($data["CheckInEnd"]) && isset($data["CheckOutStart"]) && isset($data["CheckOutEnd"]) && isset($data["BreakInStart"]) && isset($data["BreakInEnd"]) && isset($data["BreakOutStart"]) && isset($data["BreakOutEnd"]) && isset($data["WorkingHours"])) {

                // Handle ProfileId - it can be 0 or string "0" for new records
                $profile_id = isset($data["ProfileId"]) ? trim($data["ProfileId"]) : "0";

                $profile_name = trim($data["ProfileName"]);
                $check_in_start = trim($data["CheckInStart"]);
                $check_in_end = trim($data["CheckInEnd"]);
                $check_out_start = trim($data["CheckOutStart"]);
                $check_out_end = trim($data["CheckOutEnd"]);
                $break_in_start = trim($data["BreakInStart"]);
                $break_in_end = trim($data["BreakInEnd"]);
                $break_out_start = trim($data["BreakOutStart"]);
                $break_out_end = trim($data["BreakOutEnd"]);
                $working_hours = (float)$data["WorkingHours"];

                try {
                    $createProfile = $clsfunreq->createTimeProfile(
                        $profile_id,
                        $profile_name,
                        $check_in_start,
                        $check_in_end,
                        $check_out_start,
                        $check_out_end,
                        $break_in_start,
                        $break_in_end,
                        $break_out_start,
                        $break_out_end,
                        $working_hours
                    );

                    if ($createProfile) {
                        $action = ($profile_id == "0" || $profile_id == 0) ? "created" : "updated";
                        echo json_encode([
                            "Success" => true,
                            "Msg" => "Time profile $action successfully",
                            "ProfileId" => $createProfile
                        ]);
                    } else {
                        echo json_encode([
                            "Success" => false,
                            "Msg" => "Failed to save time profile"
                        ]);
                    }
                } catch (Exception $e) {
                    error_log("createTimeProfile API Error: " . $e->getMessage());
                    error_log("Stack trace: " . $e->getTraceAsString());
                    echo json_encode([
                        "Success" => false,
                        "Msg" => "Error saving time profile: " . $e->getMessage()
                    ]);
                }
            } else {
                // Enhanced debugging - show what was actually received
                if ($data === null) {
                    echo json_encode(array("Success" => false, "Msg" => "Invalid JSON data received - data is null"));
                } else {
                    // List missing fields for better debugging
                    $requiredFields = ["ProfileName", "CheckInStart", "CheckInEnd", "CheckOutStart", "CheckOutEnd", "BreakInStart", "BreakInEnd", "BreakOutStart", "BreakOutEnd", "WorkingHours"];
                    $missingFields = [];

                    foreach ($requiredFields as $field) {
                        if (!isset($data[$field]) || (is_string($data[$field]) && trim($data[$field]) === '')) {
                            $missingFields[] = $field;
                        }
                    }

                    $errorMsg = !empty($missingFields) ? "Missing required fields: " . implode(", ", $missingFields) : "Invalid data structure received";
                    error_log("AttRequest=8 validation error: " . $errorMsg);
                    error_log("Received data keys: " . implode(", ", array_keys($data)));
                    echo json_encode(array("Success" => false, "Msg" => $errorMsg, "ReceivedKeys" => array_keys($data)));
                }
            }
        } elseif ((int)$_REQUEST['AttRequest'] === 9) {
            // Delete Time Profile
            $data = json_decode(file_get_contents("php://input"), true);

            if ($data && isset($data["ProfileId"])) {
                $profileId = trim($data["ProfileId"]);
                $delete = $clsfunreq->DeleteTimeProfile($profileId);

                if ($delete) {
                    echo json_encode(array("Success" => true, "Msg" => 'Time profile deleted successfully for Profile ID: ' . $profileId));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Failed to delete time profile or profile not found for Profile ID: ' . $profileId));
                }
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Invalid data: ProfileId is required for delete operation'));
            }
        } elseif ((int) $_REQUEST['AttRequest'] === 10) {
            //assignTimeProfileToEmployee($employee_id, $time_profile_id, $effective_date = null)
            $data = json_decode(file_get_contents("php://input"), true);
            if ($data && isset($data["EmployeeId"]) && isset($data["TimeProfileId"])) {
                $employeeId = (int)$data["EmployeeId"];
                $timeProfileId = trim($data["TimeProfileId"]);
                $effectiveDate = isset($data["EffectiveDate"]) ? trim($data["EffectiveDate"]) : null;

                $assign = $clsfunreq->assignTimeProfileToEmployee($employeeId, $timeProfileId, $effectiveDate);

                if ($assign) {
                    echo json_encode(array("Success" => true, "Msg" => 'Time profile assigned successfully to Employee ID: ' . $employeeId));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Failed to assign time profile to Employee ID: ' . $employeeId));
                }
            } else {
                echo json_encode(array("Success" => false, "Msg" => 'Invalid AttRequest value'));
            }
        } elseif ((int) $_REQUEST['AttRequest'] === 11) {
            //getEmployeeTimeProfile($employee_id, $date = null)
            $data = json_decode(file_get_contents("php://input"), true);
            if ($data && isset($data["EmployeeId"])) {
                $employeeId = (int)$data["EmployeeId"];
                $date = isset($data["Date"]) ? trim($data["Date"]) : null;

                $timeProfile = $clsfunreq->getEmployeeTimeProfile($employeeId, $date);

                if ($timeProfile) {
                    echo json_encode(array("Success" => true, "Msg" => 'Time profile retrieved successfully for Employee ID: ' . $employeeId, "Data" => $timeProfile));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'No time profile found for Employee ID: ' . $employeeId));
                }
            }
        } elseif ((int) $_REQUEST['AttRequest'] === 12) {
            $getData = $clsfunreq->getEmployeeTimeProfilesAll();
            if ($getData && is_array($getData)) {
                echo json_encode([
                    "Success" => true,
                    "Msg" => "Employee time profiles retrieved successfully",
                    "Data" => $getData
                ]);
            } else {
                echo json_encode([
                    "Success" => false,
                    "Msg" => "No employee time profiles found",
                    "Data" => []
                ]);
            }
        } elseif ((int)$_REQUEST['AttRequest'] === 13) {
            //DeleteEmployeeTimeProfileAssignment($id)
            $data = json_decode(file_get_contents("php://input"), true);
            if ($data && isset($data["AssignmentId"])) {
                $assignmentId = (int)$data["AssignmentId"];
                $remove = $clsfunreq->DeleteEmployeeTimeProfileAssignment($assignmentId);
                if ($remove) {
                    echo json_encode(array("Success" => true, "Msg" => 'Time profile removed successfully from Employee ID: ' . $assignmentId));
                } else {
                    echo json_encode(array("Success" => false, "Msg" => 'Failed to remove time profile from Employee ID: ' . $assignmentId));
                }
            }
        } else {
            echo json_encode(array("Success" => false, "Msg" => 'Invalid AttRequest value'));
        }
    } catch (Exception $e) {
        error_log("AttRequest Exception: " . $e->getMessage());
        error_log("Stack trace: " . $e->getTraceAsString());
        echo json_encode(array("Success" => false, "Msg" => 'Server error: ' . $e->getMessage()));
    }
} else {
    echo json_encode(array("Success" => false, "Msg" => 'No valid request specified'));
}
