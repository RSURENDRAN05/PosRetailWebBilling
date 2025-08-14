<?php

class funcProcessMgmt {

    public function __construct() {
        require_once 'DB_Connect.php';
        $db = new Db_Connect();
        $this->conn = $db->connect();
    }

//    public function connect() {
//        $conn = mysqli_connect(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);
//        if ($conn) {
//            $this->conn = $conn;
//        }
//        return $conn;
//    }

    public function _login($user, $pass) {
        $conn = $this->conn;
        $sqlQuery = ("SELECT * FROM `users` WHERE  `username`= '" . $user . "' and  `password`= '" . md5($pass) . "' and `status`='1'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function GetUser() {
        $conn = $this->conn;
        $sqlSelect = ("SELECT `id` as Id, `username` as UserName, `password` as Password, case `role` when 1 then 'Admin' when 2 then 'User' end as Role, case `status` when 1 then 'Active' when 1 then 'InActive' End as Status,`comid` as ComId,`rid` as RestId FROM `users` ");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function _InsertUser($nl_username, $nl_password, $nl_usergroup, $nl_status, $rid, $comid) {
        $conn = $this->conn;
        $sqlSelect = ("INSERT INTO `users`(`username`, `password`,`tum_id`,`status`,`comid`,`rid`) VALUES ('" . $nl_username . "','" . md5($nl_password) . "','" . $nl_usergroup . "','" . $nl_status . "','" . $comid . "','" . $rid . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateUser($nl_userid, $nl_username, $nl_password, $nl_usergroup, $nl_status, $rid, $comid) {
        $conn = $this->conn;
        $sqlSelect = ("UPDATE `users` SET `username`='" . $nl_username . "',`password`='" . md5($nl_password) . "',`tum_id`='" . $nl_usergroup . "',`status`='" . $nl_status . "',`comid`='" . $comid . "',`rid`='" . $rid . "' WHERE `id`='" . $nl_userid . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _RptMasterByUser() {
        $conn = $this->conn;
        $sqlSelect = ("SELECT namast.id as ID,namast.username as UserName,namast.password as Password,umast.tb_usergroup_name as UserRole,namast.rid as ComId,namast.status as Active FROM `users` as namast INNER JOIN tb_usergroup_master as umast on namast.tum_id=umast.tb_usergroup_id");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function GetComapanyLocation() {
        $conn = $this->conn;
        $sqlSelect = ("SELECT pcm_id as COID,pcm_name as CompanyName,plm_id as LID,plm_name as LocationName, pcm_active  as Active FROM pos_company_mast,pos_location_mast WHERE 1;");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function GetComapany() {
        $conn = $this->conn;
        $sqlSelect = ("SELECT pcm_id as COID,pcm_name as CompanyName,pcm_active  as Active FROM pos_company_mast WHERE 1;");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function GetLocation() {
        $conn = $this->conn;
        $sqlSelect = ("SELECT * FROM `pos_location_mast`");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function SaveCompany($pcm_name, $pcm_active) {
        $conn = $this->conn;
        $sqlSelect = ("INSERT INTO `pos_company_mast`(`pcm_name`, `pcm_shortname`, `pcm_sst`, `pcm_active`, `pcm_address`, `pcm_default`)"
                . " VALUES ('" . $pcm_name . "','M','M','" . $pcm_active . "','M','1')");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function UpdateCompany($pcm_id, $pcm_name, $pcm_active) {
        $conn = $this->conn;
        $sqlSelect = ("UPDATE `pos_company_mast` SET  `pcm_name`='" . $pcm_name . "' ,`pcm_active`='" . $pcm_active . "'  WHERE  `pcm_id`='" . $pcm_id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function SaveLocation($plm_name, $plm_active) {
        $conn = $this->conn;
        $sqlSelect = ("INSERT INTO `pos_location_mast`( `plm_name`, `plm_active`,`plm_default`) VALUES ('" . $plm_name . "','" . $plm_active . "','1')");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function UpdateLocation($plm_id, $plm_name, $plm_active) {
        $conn = $this->conn;
        $sqlSelect = ("UPDATE `pos_location_mast` SET `plm_name`='" . $plm_name . "',`plm_active`='" . $plm_active . "' ,`plm_default`='1' WHERE `plm_id`='" . $plm_id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function SaveUser($username, $password, $status, $rid, $tum_id, $role, $comid) {
        $conn = $this->conn;
        $sqlSelect = ("INSERT INTO `users`(`username`, `password`, `status`, `rid`, `tum_id`, `role`,`comid`) VALUES ('" . $username . "','" . md5($password) . "',"
                . "'" . $status . "','" . $rid . "','" . $tum_id . "','" . $role . "','" . $comid . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function UpdateUser($id, $username, $password, $status, $rid, $tum_id, $role, $comid) {
        $conn = $this->conn;
        $sqlSelect = ("UPDATE `users` SET `username`='" . $username . "',`status`='" . $status . "',"
                . "`rid`='" . $rid . "',`tum_id`='" . $tum_id . "',`role`='" . $role . "',`comid` = '" . $comid . "' WHERE `id`='" . $id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
        //,`password`='" . md5($password) . "'
    }

    //Tax Master
    public function _SelectTaxMastrer() {
        $conn = $this->conn;
        $sqlSelect = ("SELECT `taxid` as TaxId, `taxname` as TaxName, `taxvalue` as TaxValue, `taxstatus` as Active FROM `taxmaster` WHERE 1");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _SelectTaxMastrerById($id) {
        $conn = $this->conn;
        $sqlSelect = ("SELECT `taxid`, `taxname`, `taxvalue`, `taxstatus` FROM `taxmaster` WHERE `taxid`='" . $id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return mysqli_fetch_assoc($result);
    }

    public function _InsertTaxMastrer($taxname, $taxvalue, $taxstatus) {
        $conn = $this->conn;
        $sqlSelect = ("INSERT INTO `taxmaster`(`taxname`, `taxvalue`, `taxstatus`) VALUES ('" . $taxname . "','" . $taxvalue . "','" . $taxstatus . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateTaxMastrer($taxid, $taxname, $taxvalue, $taxstatus) {
        $conn = $this->conn;
        $sqlSelect = ("UPDATE `taxmaster` SET `taxid`='" . $taxid . "',`taxname`='" . $taxname . "',`taxvalue`='" . $taxvalue . "',`taxstatus`='" . $taxstatus . "' WHERE `taxid`='" . $taxid . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    //MainGroup
    public function _SelectMainMastrer() {
        $conn = $this->conn;
        $sqlSelect = ("SELECT `mainid` as MainId, `mainname` as MainName, `mainstatus` as Active FROM `di_main_group` WHERE 1");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _SelectMainMastrerById($id) {
        $conn = $this->conn;
        $sqlSelect = ("SELECT `mainid` as MainId, `mainname` as MainName, `mainstatus` as Active FROM `di_main_group` WHERE `mainid`='" . $id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return mysqli_fetch_assoc($result);
    }

    public function _InsertMainMastrer($mainname, $mainstatus) {
        $conn = $this->conn;
        $sqlSelect = ("INSERT INTO `di_main_group`(`mainname`, `mainstatus`)VALUES ('" . $mainname . "','" . $mainstatus . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateMainMastrer($mainid, $mainname, $mainstatus) {
        $conn = $this->conn;
        $sqlSelect = ("UPDATE `di_main_group` SET `mainid`='" . $mainid . "',`mainname`='" . $mainname . "',`mainstatus`='" . $mainstatus . "' WHERE `mainid`='" . $mainid . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    //SubCateGroup
    public function _SelectCateMastrer() {
        $conn = $this->conn;
        $sqlSelect = ("SELECT sg.dcm_id as CateId,sg.dcm_name as CateName,mg.mainid as MainId,mg.mainname as MainName,sg.dcm_active as Active FROM `di_category_master` as sg INNER JOIN `di_main_group` as mg ON sg.di_main_id=mg.mainid WHERE 1");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _SelectCateMastrerById($id) {
        $conn = $this->conn;
        $sqlSelect = ("SELECT `dcm_id`, `dcm_name`, `di_main_id`, `dcm_active` FROM `di_category_master` WHERE  `dcm_id`='" . $id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return mysqli_fetch_assoc($result);
    }

    public function _InsertCateMastrer($catename, $mainid, $catestatus) {
        $conn = $this->conn;
        $sqlSelect = ("INSERT INTO `di_category_master`(`dcm_name`, `di_main_id`, `dcm_active`) VALUES  ('" . $catename . "','" . $mainid . "','" . $catestatus . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateCateMastrer($cateid, $catename, $mainid, $catestatus) {
        $conn = $this->conn;
        $sqlSelect = ("UPDATE `di_category_master` SET `dcm_name`='" . $catename . "',`di_main_id`='" . $mainid . "',`dcm_active`='" . $catestatus . "' WHERE `dcm_id`='" . $cateid . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    //Product Master
    public function _tablegroupmasterbycode($groupid) {
        $conn = $this->conn;
        $sqlQuery = ("SELECT tma_group_id as Id,tma_group_value as Name FROM tb_master_all WHERE tma_group_code='" . $groupid . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _SelectProductJoin() {
        $conn = $this->conn;
        $sqlQuery = ("SELECT dim.dim_item_id as Id,dim.dim_item_barcode as BarCode,dim_item_name as ItemName,dim.dim_remark as Remarks,dmg.mainname as MainName,dcm.dcm_name as CateName,tx.taxname as TaxName,dim.dim_sell_price as SellPrice,dim.dim_cost_price as CostPrice,pcm.pcm_name as CompanyName,plm.plm_name as LocationName,dim.dim_status as Active FROM `di_item_mast`as dim INNER JOIN `di_main_group` as dmg ON dim.dim_main_id=dmg.mainid INNER JOIN `di_category_master` as dcm ON dim.dim_cate_id=dcm.dcm_id INNER JOIN `taxmaster` as tx ON dim.dim_tax_id=tx.taxid INNER JOIN `pos_company_mast` as pcm   ON dim.dim_com_id=pcm.pcm_id INNER JOIN `pos_location_mast` as plm ON dim.dim_loc_id=plm.plm_id WHERE 1;");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _InsertProductMaster($dim_item_barcode, $dim_item_name, $dim_business_type, $dim_main_id, $dim_cate_id, $dim_tax_id, $dim_cost_price, $dim_sell_price, $dim_op_stock, $dim_stock_in,
            $dim_stock_out, $dim_stock_cur, $dim_com_id, $dim_loc_id, $dim_status, $dim_remark) {
        $conn = $this->conn;
        $sqlQuery = ("INSERT INTO `di_item_mast`( `dim_item_barcode`, `dim_item_name`, `dim_business_type`, `dim_main_id`, `dim_cate_id`, `dim_tax_id`, `dim_cost_price`"
                . ", `dim_sell_price`, `dim_op_stock`, `dim_stock_in`, `dim_stock_out`, `dim_stock_cur`, `dim_com_id`, `dim_loc_id`, `dim_status`,`dim_remark`,`created`) VALUES "
                . " ('" . $dim_item_barcode . "','" . $dim_item_name . "','" . $dim_business_type . "','" . $dim_main_id . "','" . $dim_cate_id . "','" . $dim_tax_id . "','" . $dim_cost_price . "'"
                . ",'" . $dim_sell_price . "','" . $dim_op_stock . "','" . $dim_stock_in . "','" . $dim_stock_out . "','" . $dim_stock_cur . "','" . $dim_com_id . "','" . $dim_loc_id . "','" . $dim_status . "'"
                . ",'" . $dim_remark . "','" . date('Y/m/d') . "')");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _GetProductCode($dim_item_barcode, $dim_item_name, $dim_com_id, $dim_loc_id) {
        $conn = $this->conn;
        $sqlQuery = ("SELECT `dim_item_id` FROM `di_item_mast` WHERE `dim_item_name` ='" . $dim_item_name . "' AND `dim_item_barcode` ='" . $dim_item_barcode . "' AND `dim_com_id`='" . $dim_com_id . "' AND  `dim_loc_id`='" . $dim_loc_id . "' ");
        $result = mysqli_query($conn, $sqlQuery);
        $rowProductCode1 = (mysqli_fetch_assoc($result));
        $rowProductCode2 = $rowProductCode1['dim_item_id'];
        return $rowProductCode2;
    }

    public function _InsertLiveStock($dim_item_id, $dim_item_barcode, $dim_cost_price, $dim_sell_price, $dim_op_stock, $dim_stock_cur, $dim_com_id, $dim_loc_id) {
        $conn = $this->conn;
        $sqlQueryCheckItemcode = ("SELECT count(*) as counts FROM `pos_livestock` WHERE `pl_itemcode`='" . $dim_item_id . "'");
        $resultCheck = mysqli_query($conn, $sqlQueryCheckItemcode);
        $count = (mysqli_fetch_assoc($resultCheck));
        $rowitemcount = $count['counts'];
        if ($rowitemcount == 0) {
            $sqlQuery = ("INSERT INTO `pos_livestock`(`pl_itemcode`, `pl_barcode`, `pl_serialno`, `pl_batch`, `pl_expiry`, `pl_cost`, `pl_sell`,"
                    . " `pl_opstok`, `pl_stockin`, `pl_stockout`, `pl_livestock`, `pl_comid`, `pl_locid`, `pl_created`) "
                    . "VALUES ('" . $dim_item_id . "','" . $dim_item_barcode . "','0','0','" . date('Y/m/d') . "',"
                    . "'" . $dim_cost_price . "','" . $dim_sell_price . "','" . $dim_op_stock . "','0','0',"
                    . "'" . $dim_op_stock . "','" . $dim_com_id . "','" . $dim_loc_id . "','" . date('Y/m/d') . "')");
            $result = mysqli_query($conn, $sqlQuery);
        } else {
            $sqlQuery = ("UPDATE `pos_livestock` SET  `pl_barcode`='" . $dim_item_barcode . "',`pl_cost`='" . $dim_cost_price . "',`pl_sell`='" . $dim_sell_price . "',"
                    . "`pl_opstok`='" . $dim_op_stock . "',`pl_livestock`= pl_opstok + pl_stockin -pl_stockout,"
                    . "`pl_comid`='" . $dim_com_id . "',`pl_locid`='" . $dim_loc_id . "' WHERE  `pl_itemcode`='" . $dim_item_id . "'");
            $result = mysqli_query($conn, $sqlQuery);
        }
        return $result;
    }

    public function _UpdateProductMaster($dim_item_id, $dim_item_barcode, $dim_item_name, $dim_business_type, $dim_main_id, $dim_cate_id, $dim_tax_id, $dim_cost_price, $dim_sell_price, $dim_op_stock, $dim_stock_in,
            $dim_stock_out, $dim_stock_cur, $dim_com_id, $dim_loc_id, $dim_status, $dim_remark) {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `di_item_mast` SET `dim_item_barcode`='" . $dim_item_barcode . "',`dim_item_name`='" . $dim_item_name . "',`dim_business_type`='" . $dim_business_type . "'"
                . ",`dim_main_id`='" . $dim_main_id . "',`dim_cate_id`='" . $dim_cate_id . "',`dim_tax_id`='" . $dim_tax_id . "',`dim_cost_price`='" . $dim_cost_price . "',`dim_sell_price`='" . $dim_sell_price . "'"
                . ",`dim_op_stock`='" . $dim_op_stock . "',`dim_stock_in`='" . $dim_stock_in . "',`dim_stock_out`='" . $dim_stock_out . "',`dim_stock_cur`='" . $dim_stock_cur . "',`dim_com_id`='" . $dim_com_id . "'"
                . ",`dim_loc_id`='" . $dim_loc_id . "',`dim_status`='" . $dim_status . "',`dim_remark`='" . $dim_remark . "' WHERE `dim_item_id`='" . $dim_item_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    //Company Save
    public function storeCustomerData($txtCustomerName, $status) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `customermaster`(`customerName`,`status`) VALUES ('" . $txtCustomerName . "','" . $status . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updateCustomerData($id, $txtCustomerName, $status) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `customermaster` SET `customerName`='" . $txtCustomerName . "',`status`='" . $status . "' WHERE `customerId`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectCustomer() {
        $conn = $this->conn;
        $sqlquery = ("SELECT `customerId` as CustomerId,`customerName` CustomerName,`status` as Active FROM `customermaster` ORDER BY customerName ASC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function _branchSelect() {
        $conn = $this->conn;
        $sqlQuery = ("SELECT dbm.branchid as Id,dbm.branchname as BranchName,dbm.branchcustomerid as CompanyId,cm.customerName as CompanyName,dbm.branchaddress as Address,dbm.branchemail as Email, dbm.branchcontact as Contact,dbm.branchanydesk as AnyDesk,dbm.branchserver as Server,dbm.branchclient as Client,dbm.branchtab as Tab,branchinstalldate as InstallDate, dbm.branchlock as Locks,dbm.branchactivationcode as Activation,dbm.branchstatus as Active,dbm.branchmessage as Message FROM `di_branch_mast` as dbm INNER join `customermaster` as cm on dbm.branchcustomerid=cm.customerId WHERE 1;");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _branchselectById($id) {
        $conn = $this->conn;
        $sqlquery = ("SELECT dbm.branchid as Id,cm.customerName as CompanyName,dbm.branchname as BranchName,dbm.branchaddress as Address,dbm.branchemail as Email,"
                . "dbm.branchcontact as Contact,dbm.branchanydesk as AnyDesk,dbm.branchserver as Server,dbm.branchclient as Client,dbm.branchtab as Tab,branchinstalldate as InstallDate,"
                . "dbm.branchlock as Locks,dbm.branchactivationcode as Activation,dbm.branchstatus as Active,dbm.branchmessage as Message FROM `di_branch_mast` as dbm INNER join `customermaster` as cm on dbm.branchcustomerid=cm.customerId WHERE dbm.branchid='" . $id . "'");
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function _branchSave($branchcustomerid, $branchname, $branchaddress, $branchemail, $branchcontact, $branchanydesk, $branchserver, $branchclient, $branchtab, $branchlock, $branchactivationcode, $branchstatus, $branchmessage, $branchinstalldate) {
        $conn = $this->conn;
        $sqlQuery = ("INSERT INTO `di_branch_mast`(`branchcustomerid`, `branchname`, `branchaddress`, `branchemail`, `branchcontact`,`branchanydesk`, `branchserver`, `branchclient`, `branchtab`, `branchlock`, `branchactivationcode`, `branchmessage`,`branchstatus`,`branchinstalldate`) VALUES"
                . "('" . $branchcustomerid . "','" . $branchname . "','" . $branchaddress . "','" . $branchemail . "','" . $branchcontact . "','" . $branchanydesk . "','" . $branchserver . "','" . $branchclient . "','" . $branchtab . "','" . $branchlock . "','" . $branchactivationcode . "','" . $branchmessage . "','" . $branchstatus . "','" . $branchinstalldate . "')");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function storeCustomerLedgerData($customer) {
        $conn = $this->conn;
        $sqlCustomer = ("SELECT branchid FROM `di_branch_mast` WHERE `branchname`='" . $customer . "' ");
        $resCutomer = mysqli_query($conn, $sqlCustomer);
        $rowCusId = (mysqli_fetch_assoc($resCutomer));
        $rowCusIds = $rowCusId;
        $sqlquery = (" INSERT INTO `ledgermaster`(`ledgerrefId`, `ledgerName`, `ledgerparenId`, `ledgergroupId`,`ledgerType`,`ledgeropenDate`, `ledgeropenbal`, `ledgerdrcr`, `ledgerActive`) VALUES"
                . "(" . $rowCusIds['branchid'] . ",'" . $customer . "',1,4,'CUS','" . date("Y/m/d") . "',0.00,'Dr','Active')");
        //  print_r($rowCusIds);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function _branchUpdate($branchid, $branchcustomerid, $branchname, $branchaddress, $branchemail, $branchcontact, $branchanydesk, $branchserver, $branchclient, $branchtab, $branchlock, $branchactivationcode, $branchstatus, $branchmessage, $branchinstalldate) {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `di_branch_mast` SET `branchcustomerid`='" . $branchcustomerid . "', `branchname`='" . $branchname . "', `branchaddress`='" . $branchaddress . "', `branchemail`='" . $branchemail . "', `branchcontact`='" . $branchcontact . "',`branchanydesk`='" . $branchanydesk . "', `branchserver`='" . $branchserver . "', `branchclient`='" . $branchclient . "', `branchtab`='" . $branchtab . "', `branchlock`='" . $branchlock . "', `branchactivationcode`='" . $branchactivationcode . "',`branchmessage`='" . $branchmessage . "', `branchstatus`='" . $branchstatus . "',`branchinstalldate`='" . $branchinstalldate . "' WHERE `branchid`='" . $branchid . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function updateCustomerLedgerData($ledgerrefId, $ledgerName) {
        $conn = $this->conn;
        $sqlCheck = ("SELECT count(*) as counts FROM `ledgermaster` WHERE `ledgerrefId`='" . $ledgerrefId . "' AND `ledgerType`='CUS'");
        $resultCheck = mysqli_query($conn, $sqlCheck);
        $count = (mysqli_fetch_assoc($resultCheck));
        $rowCusIds = $count;
        if ($rowCusIds['counts'] == 0) {
            $sqlquery = (" INSERT INTO `ledgermaster`(`ledgerrefId`, `ledgerName`, `ledgerparenId`, `ledgergroupId`,`ledgerType`,`ledgeropenDate`, `ledgeropenbal`, `ledgerdrcr`, `ledgerActive`) VALUES"
                    . "('" . $ledgerrefId . "','" . $ledgerName . "',1,4,'CUS','" . date("Y/m/d") . "',0.00,'Dr','Active')");
            //  print_r($rowCusIds);
            $result = mysqli_query($conn, $sqlquery);
        } else {
            $sqlquery = ("UPDATE `ledgermaster` SET `ledgerName`='" . $ledgerName . "' WHERE `ledgerrefId`=" . $ledgerrefId . " AND `ledgerType`='CUS'");
            // echo $sqlquery;
            $result = mysqli_query($conn, $sqlquery);
        }
        return $result;
    }

    //Unit Save
    public function UnitSave($dum_name, $dum_active) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `di_unit_mast`(`dum_name`, `dum_active`) VALUES ('" . $dum_name . "','" . $dum_active . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //Unit Update
    public function UnitUpdate($dum_id, $dum_name, $dum_active) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `di_unit_mast` SET  `dum_name`='" . $dum_name . "',`dum_active`='" . $dum_active . "' WHERE `dum_id`='" . $dum_id . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //Select Unit
    public function UnitSelect() {
        $conn = $this->conn;
        $sqlquery = ("SELECT `dum_id` as Id, `dum_name` as UnitName, `dum_active` as Active FROM `di_unit_mast` WHERE 1");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //Supplier Entry 
    public function storesupplierData($supplierName, $status) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `suppliermaster`(`supplierName`, `status`) VALUES ('" . $supplierName . "','" . $status . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updatesupplierData($id, $supplierName, $status) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `suppliermaster` SET `supplierName`='" . $supplierName . "',`status`='" . $status . "' WHERE `supplierId`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectsupplier() {
        $conn = $this->conn;
        $sqlquery = ("SELECT supplierId as Id,`supplierName` as SupplierName,`status` as Active FROM `suppliermaster`");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectLedgersupplier() {
        $conn = $this->conn;
        $sqlquery = ("SELECT ledgermaster.ledgerId as Id,`supplierName` as SupplierName,`status` as Active FROM `suppliermaster` INNER JOIN ledgermaster ON suppliermaster.supplierId = ledgermaster.ledgerrefId WHERE ledgermaster.ledgerType='SUP'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function storeSupplierLedgerData($customer) {
        $conn = $this->conn;
        $sqlCustomer = ("SELECT supplierId FROM `suppliermaster` WHERE `supplierName`='" . $customer . "'");
        $resCutomer = mysqli_query($conn, $sqlCustomer);
        $rowCusId = (mysqli_fetch_assoc($resCutomer));
        $rowCusIds = $rowCusId;
        $sqlquery = (" INSERT INTO `ledgermaster`(`ledgerrefId`, `ledgerName`, `ledgerparenId`, `ledgergroupId`,`ledgerType`,`ledgeropenDate`, `ledgeropenbal`, `ledgerdrcr`, `ledgerActive`) VALUES"
                . "(" . $rowCusIds['supplierId'] . ",'" . $customer . "',2,3,'SUP'," . date("Y/m/d") . "',0.00,'Cr','Active')");
        //  print_r($rowCusIds);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updateSupplierLedgerData($ledgerrefId, $ledgerName) {
        $conn = $this->conn;
        $sqlCheck = ("SELECT count(*) as counts FROM `ledgermaster` WHERE `ledgerrefId`='" . $ledgerrefId . "' AND `ledgerType`='SUP'");
        $resultCheck = mysqli_query($conn, $sqlCheck);
        $count = (mysqli_fetch_assoc($resultCheck));
        $rowCusIds = $count;
        if ($rowCusIds['counts'] == 0) {
            $sqlquery = (" INSERT INTO `ledgermaster`(`ledgerrefId`, `ledgerName`, `ledgerparenId`, `ledgergroupId`,`ledgerType`,`ledgeropenDate`, `ledgeropenbal`, `ledgerdrcr`, `ledgerActive`) VALUES"
                    . "('" . $ledgerrefId . "','" . $ledgerName . "',1,4,'SUP','" . date("Y/m/d") . "',0.00,'Dr','Active')");
            //  print_r($rowCusIds);
            $result = mysqli_query($conn, $sqlquery);
        } else {
            $sqlquery = ("UPDATE `ledgermaster` SET `ledgerName`='" . $ledgerName . "' WHERE `ledgerrefId`=" . $ledgerrefId . " AND `ledgerType`='SUP'");
            // echo $sqlquery;
            $result = mysqli_query($conn, $sqlquery);
        }
        return $result;
    }

//Invoice Update
    public function selectMaxBillNoByType($BillType) {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM `invoiceautono` WHERE `autoname`='" . $BillType . "'");
        $result = mysqli_query($conn, $sqlquery);
        while ($row = mysqli_fetch_row($result)) {
            $Maxno = $row[1];
        }
        return $Maxno;
    }

    public function selectMaxBillNo($BillType) {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM `invoiceautono` WHERE `autoname`='" . $BillType . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updateVoucherNo($autoname) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `invoiceautono` SET `autono`=`autono` + 1 WHERE `autoname`='" . $autoname . "'"); //SALES//pay
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //Menu List
    public function selectMenuList($pmrtype) {
        $conn = $this->conn;
        $sqlquery = ("SELECT `pmr_id`, `pmr_name`, `pmr_groupname`, `pmr_type`, `pmr_active` FROM `pos_menu_rights` WHERE `pmr_type`='" . $pmrtype . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //Purchase Save  
    public function GetProductList() {
        $conn = $this->conn;
        $sqlquery = ("SELECT dim.dim_item_id as ITEMCODE,pls.pl_barcode as BARCODE,dim.dim_item_name as ITEMNAME,taxs.taxid as TAXID,taxs.taxname as TAXNAME,taxs.taxvalue as TAXVALUE,
                      pls.pl_cost as COST,pls.pl_sell as SELL,(pls.pl_opstok + pls.pl_stockin - pls.pl_stockout) as LIVESTOCK,pls.pl_comid as COMID,pls.pl_locid as LOCID, dim.dim_remark as Remarks
                      FROM `di_item_mast` as dim 
                      INNER JOIN `pos_livestock` as pls ON dim.dim_item_id =pls.pl_itemcode INNER JOIN `taxmaster` AS taxs ON taxs.taxid=dim.dim_tax_id");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetProductListByItemCode($ItemCode) {
        $conn = $this->conn;
        $sqlquery = ("SELECT dim.dim_item_id as ITEMCODE,pls.pl_barcode as BARCODE,dim.dim_item_name as ITEMNAME,taxs.taxid as TAXID,taxs.taxname as TAXNAME,taxs.taxvalue as TAXVALUE,
                      pls.pl_cost as COST,pls.pl_sell as SELL,(pls.pl_opstok + pls.pl_stockin - pls.pl_stockout) as LIVESTOCK,pls.pl_comid as COMID,pls.pl_locid as LOCID FROM `di_item_mast` as dim 
                      INNER JOIN `pos_livestock` as pls ON dim.dim_item_id =pls.pl_itemcode INNER JOIN `taxmaster` AS taxs ON taxs.taxid=dim.dim_tax_id WHERE dim.dim_item_id='" . $ItemCode . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SavePurchaseDataDtl($ppd_trno, $ppd_sno, $ppd_itemcode, $ppd_barcode, $ppd_serialno, $ppd_batch, $ppd_prate, $ppd_qty,
            $ppd_amount, $ppd_discper, $ppd_discamt, $ppd_totalamt, $ppd_taxid, $ppd_taxamt, $ppd_grossamt, $ppd_roundoff, $ppd_netamt, $ppd_expiry,
            $ppd_costprice, $ppd_sellprice, $ppd_comid, $ppd_locid) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_pur_dtl`(`ppd_trno`,`ppd_sno`, `ppd_itemcode`, `ppd_barcode`, `ppd_serialno`, `ppd_batch`, `ppd_prate`, `ppd_qty`,"
                . " `ppd_amount`, `ppd_discper`, `ppd_discamt`, `ppd_totalamt`, `ppd_taxid`, `ppd_taxamt`, `ppd_grossamt`, `ppd_roundoff`, `ppd_netamt`, "
                . "`ppd_expiry`, `ppd_costprice`,`ppd_sellprice`, `ppd_comid`, `ppd_locid`, `ppd_created`)"
                . " VALUES ('" . $ppd_trno . "','" . $ppd_sno . "','" . $ppd_itemcode . "','" . $ppd_barcode . "','" . $ppd_serialno . "','" . $ppd_batch . "','" . $ppd_prate . "','" . $ppd_qty . "',"
                . "'" . $ppd_amount . "','" . $ppd_discper . "','" . $ppd_discamt . "','" . $ppd_totalamt . "','" . $ppd_taxid . "','" . $ppd_taxamt . "','" . $ppd_grossamt . "',"
                . "'" . $ppd_roundoff . "','" . $ppd_netamt . "','" . $ppd_expiry . "','" . $ppd_costprice . "','" . $ppd_sellprice . "','" . $ppd_comid . "','" . $ppd_locid . "','" . date("Y-m-d") . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function UpdatePurchaseDataDtl($ppd_id, $ppd_trno, $ppd_sno, $ppd_itemcode, $ppd_barcode, $ppd_serialno, $ppd_batch, $ppd_prate, $ppd_qty,
            $ppd_amount, $ppd_discper, $ppd_discamt, $ppd_totalamt, $ppd_taxid, $ppd_taxamt, $ppd_grossamt, $ppd_roundoff, $ppd_netamt, $ppd_expiry,
            $ppd_costprice, $ppd_sellprice, $ppd_comid, $ppd_locid) {
        $conn = $this->conn;
        $result = "";
        $sqlQueryCheck = ("SELECT count(*) as counts FROM `pos_pur_dtl` WHERE `ppd_id` ='" . $ppd_id . "'");
        $resultCheck = mysqli_query($conn, $sqlQueryCheck);
        $count = (mysqli_fetch_assoc($resultCheck));
        $rowCusIds = $count;
        if ($rowCusIds['counts'] == 0) {
            $sqlquery = ("INSERT INTO `pos_pur_dtl`(`ppd_trno`,`ppd_sno`, `ppd_itemcode`, `ppd_barcode`, `ppd_serialno`, `ppd_batch`, `ppd_prate`, `ppd_qty`,"
                    . " `ppd_amount`, `ppd_discper`, `ppd_discamt`, `ppd_totalamt`, `ppd_taxid`, `ppd_taxamt`, `ppd_grossamt`, `ppd_roundoff`, `ppd_netamt`, "
                    . "`ppd_expiry`, `ppd_costprice`,`ppd_sellprice`, `ppd_comid`, `ppd_locid`, `ppd_created`)"
                    . " VALUES ('" . $ppd_trno . "','" . $ppd_sno . "','" . $ppd_itemcode . "','" . $ppd_barcode . "','" . $ppd_serialno . "','" . $ppd_batch . "','" . $ppd_prate . "','" . $ppd_qty . "',"
                    . "'" . $ppd_amount . "','" . $ppd_discper . "','" . $ppd_discamt . "','" . $ppd_totalamt . "','" . $ppd_taxid . "','" . $ppd_taxamt . "','" . $ppd_grossamt . "',"
                    . "'" . $ppd_roundoff . "','" . $ppd_netamt . "','" . $ppd_expiry . "','" . $ppd_costprice . "','" . $ppd_sellprice . "','" . $ppd_comid . "','" . $ppd_locid . "','" . date("Y-m-d") . "')");
            $result .= mysqli_query($conn, $sqlquery);
            $result .= $this->_UpdateLiveStock($ppd_itemcode, $ppd_barcode, $ppd_costprice, $ppd_sellprice, $ppd_qty, $ppd_comid, $ppd_locid);
        } elseif ($rowCusIds['counts'] == 1) {
            $result .= $this->_UpdateEditLiveStock($ppd_itemcode, $ppd_barcode, $ppd_costprice, $ppd_sellprice, $ppd_qty, $ppd_comid, $ppd_locid);
            $sqlDeleteQuery = ("DELETE FROM `pos_pur_dtl` WHERE `ppd_id` ='" . $ppd_id . "' AND `ppd_trno`='" . $ppd_trno . "' AND `ppd_itemcode`='" . $ppd_itemcode . "'");

            $result .= mysqli_query($conn, $sqlDeleteQuery);
            $sqlquery .= ("INSERT INTO `pos_pur_dtl`(`ppd_trno`,`ppd_sno`, `ppd_itemcode`, `ppd_barcode`, `ppd_serialno`, `ppd_batch`, `ppd_prate`, `ppd_qty`,"
                    . " `ppd_amount`, `ppd_discper`, `ppd_discamt`, `ppd_totalamt`, `ppd_taxid`, `ppd_taxamt`, `ppd_grossamt`, `ppd_roundoff`, `ppd_netamt`, "
                    . "`ppd_expiry`, `ppd_costprice`,`ppd_sellprice`, `ppd_comid`, `ppd_locid`, `ppd_created`)"
                    . " VALUES ('" . $ppd_trno . "','" . $ppd_sno . "','" . $ppd_itemcode . "','" . $ppd_barcode . "','" . $ppd_serialno . "','" . $ppd_batch . "','" . $ppd_prate . "','" . $ppd_qty . "',"
                    . "'" . $ppd_amount . "','" . $ppd_discper . "','" . $ppd_discamt . "','" . $ppd_totalamt . "','" . $ppd_taxid . "','" . $ppd_taxamt . "','" . $ppd_grossamt . "',"
                    . "'" . $ppd_roundoff . "','" . $ppd_netamt . "','" . $ppd_expiry . "','" . $ppd_costprice . "','" . $ppd_sellprice . "','" . $ppd_comid . "','" . $ppd_locid . "','" . date("Y-m-d") . "')");
            $result .= mysqli_query($conn, $sqlquery);
            $result .= $this->_UpdateLiveStock($ppd_itemcode, $ppd_barcode, $ppd_costprice, $ppd_sellprice, $ppd_qty, $ppd_comid, $ppd_locid);
        }
        return $result; // . ',' . $sqlDeleteQuery;
    }

    public function DeletePurchaseDataDtl($ppd_id, $ppd_trno, $ppd_itemcode, $ppd_barcode, $ppd_qty, $ppd_costprice, $ppd_sellprice, $ppd_comid, $ppd_locid) {
        $conn = $this->conn;
        $result = "";
        $sqlQueryCheck = ("SELECT count(*) as counts FROM `pos_pur_dtl` WHERE `ppd_id` ='" . $ppd_id . "'");
        $resultCheck = mysqli_query($conn, $sqlQueryCheck);
        $count = (mysqli_fetch_assoc($resultCheck));
        $rowCusIds = $count;
        if ($rowCusIds['counts'] == 1) {
            $result .= $this->_UpdateEditLiveStock($ppd_itemcode, $ppd_barcode, $ppd_costprice, $ppd_sellprice, $ppd_qty, $ppd_comid, $ppd_locid);
            $sqlDeleteQuery = ("DELETE FROM `pos_pur_dtl` WHERE `ppd_id` ='" . $ppd_id . "' AND `ppd_trno`='" . $ppd_trno . "' AND `ppd_itemcode`='" . $ppd_itemcode . "'");
            $result = mysqli_query($conn, $sqlDeleteQuery);
        }
        return $result;
    }

    public function SavePurchaseDataHdr($pph_trno, $pph_refno, $pph_invdate, $pph_purdate, $pph_suppid, $pph_billdiscper, $pph_billdiscamt, $pph_netamt, $pph_paymenttype,
            $pph_baloutamt, $pph_comid, $pph_locid, $pph_userid) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_pur_hdr`(`pph_trno`, `pph_refno`, `pph_invdate`, `pph_purdate`, `pph_suppid`, `pph_billdiscper`, `pph_billdiscamt`,"
                . " `pph_netamt`, `pph_paymenttype`, `pph_baloutamt`, `pph_comid`, `pph_locid`,`pph_userid`,`pph_created`)"
                . " VALUES ('" . $pph_trno . "','" . $pph_refno . "','" . $pph_invdate . "','" . $pph_purdate . "','" . $pph_suppid . "','" . $pph_billdiscper . "','" . $pph_billdiscamt . "',"
                . "'" . $pph_netamt . "','" . $pph_paymenttype . "','" . $pph_baloutamt . "','" . $pph_comid . "','" . $pph_locid . "','" . $pph_userid . "','" . date("Y-m-d") . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function deleteInvoiceByHDR($invoice) {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `pos_pur_hdr` WHERE pph_trno='" . $invoice . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function deleteJourEntry($id) {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `journaldetails` WHERE `refinvoiceno`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //Live Stock
    public function _UpdateLiveStock($dim_item_id, $dim_item_barcode, $dim_cost_price, $dim_sell_price, $dim_stock_cur, $dim_com_id, $dim_loc_id) {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `pos_livestock` SET  `pl_barcode`='" . $dim_item_barcode . "',`pl_cost`='" . $dim_cost_price . "',`pl_sell`='" . $dim_sell_price . "',"
                . "`pl_stockin`= pl_stockin + '" . $dim_stock_cur . "',`pl_livestock`= pl_opstok + pl_stockin - pl_stockout,"
                . "`pl_comid`='" . $dim_com_id . "',`pl_locid`='" . $dim_loc_id . "' WHERE  `pl_itemcode`='" . $dim_item_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _UpdateLiveStockSales($dim_item_id, $dim_stock_cur, $dim_com_id, $dim_loc_id) {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `pos_livestock` SET   `pl_stockout`= pl_stockout + '" . $dim_stock_cur . "',`pl_livestock`= pl_opstok + pl_stockin - pl_stockout,"
                . "`pl_comid`='" . $dim_com_id . "',`pl_locid`='" . $dim_loc_id . "' WHERE  `pl_itemcode`='" . $dim_item_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _UpdateLiveStockEditSales($dim_item_id, $dim_stock_cur, $dim_com_id, $dim_loc_id) {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `pos_livestock` SET   `pl_stockout`= pl_stockout - '" . $dim_stock_cur . "',`pl_livestock`= pl_opstok + pl_stockin + pl_stockout,"
                . "`pl_comid`='" . $dim_com_id . "',`pl_locid`='" . $dim_loc_id . "' WHERE  `pl_itemcode`='" . $dim_item_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _UpdateEditLiveStock($dim_item_id, $dim_item_barcode, $dim_cost_price, $dim_sell_price, $dim_stock_cur, $dim_com_id, $dim_loc_id) {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `pos_livestock` SET  `pl_barcode`='" . $dim_item_barcode . "',`pl_cost`='" . $dim_cost_price . "',`pl_sell`='" . $dim_sell_price . "',"
                . "`pl_stockin`= pl_stockin - '" . $dim_stock_cur . "',`pl_livestock`= pl_livestock - '" . $dim_stock_cur . "',"
                . "`pl_comid`='" . $dim_com_id . "',`pl_locid`='" . $dim_loc_id . "' WHERE  `pl_itemcode`='" . $dim_item_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    //journal Entry

    public function storeJournalPurchaseEntry($ledgerid, $branchid, $vocheramt, $accttype, $modetype, $txtdatepicker, $statuAcct, $userid, $txtnarration, $refinvoiceno) {
        $conn = $this->conn;

        $sqlQuery1 = ("SELECT `ledgerName` FROM `ledgermaster` WHERE `ledgerid`='" . $ledgerid . "'");
        $result1 = mysqli_query($conn, $sqlQuery1);
        $row1 = mysqli_fetch_assoc($result1);
        $ledgerName1 = $row1['ledgerName'];

        $sqlQuery2 = ("SELECT `ledgerid`,`ledgerName` FROM `ledgermaster` WHERE `ledgerid`='" . $branchid . "' AND `ledgerType`='SUP'");
        $result2 = mysqli_query($conn, $sqlQuery2);
        $row2 = mysqli_fetch_assoc($result2);
        $ledgerNameId2 = $row2['ledgerid'];
        $ledgerName2 = $row2['ledgerName'];
        $txtvoucherno = $this->selectMaxBillNoByType("PAY");
        $sqlquery1 = ("INSERT INTO `journaldetails`(`ledgerid`,`refinvoiceno`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`)"
                . "VALUES ('" . $ledgerid . "','" . $refinvoiceno . "','" . $ledgerName1 . "'"
                . ",'" . $vocheramt . "',0.00,'Dr',$txtvoucherno,'" . $txtdatepicker . "','" . date('Y-m-d') . "','" . $accttype . "','" . $modetype . "','" . $txtnarration . "',0.00,'" . $statuAcct . "','" . $userid . "','" . $ledgerName2 . "','" . $ledgerNameId2 . "')");
        $result = mysqli_query($conn, $sqlquery1);
        $sqlquery2 = ("INSERT INTO `journaldetails`(`ledgerid`,`refinvoiceno`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`)"
                . "VALUES ('" . $ledgerNameId2 . "','" . $refinvoiceno . "','" . $ledgerName2 . "'"
                . ",0.00,'" . $vocheramt . "','Cr',$txtvoucherno,'" . $txtdatepicker . "','" . date('Y-m-d') . "','" . $accttype . "','" . $modetype . "','" . $txtnarration . "',0.00,'" . $statuAcct . "','" . $userid . "','" . $ledgerName1 . "','" . $ledgerid . "')");
        $result = mysqli_query($conn, $sqlquery2);
        $this->updateVoucherNo('PAY');
        return $result; //$ledgerName1 . ', ' . $ledgerName2 . ',' . $txtvoucherno . ',' .$sqlquery1 . ',' .$sqlquery2;
    }

    public function storeJournalSalesEntry($ledgerid, $branchid, $vocheramt, $accttype, $modetype, $txtdatepicker, $statuAcct, $userid, $txtnarration, $refinvoiceno) {
        $conn = $this->conn;

        $sqlQuery1 = ("SELECT `ledgerName` FROM `ledgermaster` WHERE `ledgerid`='" . $ledgerid . "'");
        $result1 = mysqli_query($conn, $sqlQuery1);
        $row1 = mysqli_fetch_assoc($result1);
        $ledgerName1 = $row1['ledgerName'];

        $sqlQuery2 = ("SELECT `ledgerid`,`ledgerName` FROM `ledgermaster` WHERE `ledgerid`='" . $branchid . "' AND `ledgerType`='CUS'");
        $result2 = mysqli_query($conn, $sqlQuery2);
        $row2 = mysqli_fetch_assoc($result2);
        $ledgerNameId2 = $row2['ledgerid'];
        $ledgerName2 = $row2['ledgerName'];
        $txtvoucherno = $this->selectMaxBillNoByType("PAY");
        $sqlquery1 = ("INSERT INTO `journaldetails`(`ledgerid`,`refinvoiceno`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`)"
                . "VALUES ('" . $ledgerid . "','" . $refinvoiceno . "','" . $ledgerName1 . "'"
                . ",'" . $vocheramt . "',0.00,'Dr',$txtvoucherno,'" . $txtdatepicker . "','" . date('Y-m-d') . "','" . $accttype . "','" . $modetype . "','" . $txtnarration . "',0.00,'" . $statuAcct . "','" . $userid . "','" . $ledgerName2 . "','" . $ledgerNameId2 . "')");
        $result = mysqli_query($conn, $sqlquery1);
        $sqlquery2 = ("INSERT INTO `journaldetails`(`ledgerid`,`refinvoiceno`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`)"
                . "VALUES ('" . $ledgerNameId2 . "','" . $refinvoiceno . "','" . $ledgerName2 . "'"
                . ",0.00,'" . $vocheramt . "','Cr',$txtvoucherno,'" . $txtdatepicker . "','" . date('Y-m-d') . "','" . $accttype . "','" . $modetype . "','" . $txtnarration . "',0.00,'" . $statuAcct . "','" . $userid . "','" . $ledgerName1 . "','" . $ledgerid . "')");
        $result = mysqli_query($conn, $sqlquery2);
        $this->updateVoucherNo('PAY');
        return $result;
    }

    public function storeJournalPaymentEntry($cmbledgername, $txtvoucheramount, $drCrMode, $txtvoucherno, $txtdatepicker, $cmbactype, $cmbmodename, $txtnarration, $cmbbankname, $txtchqdate, $txtchqno, $txtchqamount, $txthidden) {
        $conn = $this->conn;
        $sqlQuery1 = ("SELECT `ledgerName` FROM `ledgermaster` WHERE `ledgerid`=" . $cmbactype);
        $result1 = mysqli_query($conn, $sqlQuery1);
        $row1 = mysqli_fetch_assoc($result1);
        $ledgerName1 = $row1;

        $sqlQuery3 = ("SELECT `ledgerName` FROM `ledgermaster` WHERE `ledgerid`=" . $cmbledgername);
        $result3 = mysqli_query($conn, $sqlQuery3);
        $row3 = mysqli_fetch_assoc($result3);
        $ledgerName3 = $row3;

        $sqlquery2 = ("INSERT INTO `journaldetails`(`ledgerid`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`)"
                . "VALUES ('" . $cmbactype . "','" . $ledgerName1['ledgerName'] . "'"
                . ",$txtvoucheramount,0.00,'Dr',$txtvoucherno,'" . $txtdatepicker . "','" . date('Y/m/d') . "','PUR','" . $cmbmodename . "','" . $txtnarration . "',0.00,'A','" . $txthidden . "','" . $ledgerName3['ledgerName'] . "','" . $cmbledgername . "')");
        $result = mysqli_query($conn, $sqlquery2);

        $sqlquery4 = ("INSERT INTO `journaldetails`(`ledgerid`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`)"
                . "VALUES ('" . $cmbledgername . "','" . $ledgerName3['ledgerName'] . "'"
                . ",0.00,$txtvoucheramount,'Cr',$txtvoucherno,'" . $txtdatepicker . "','" . date('Y/m/d') . "','PUR','" . $cmbmodename . "','" . $txtnarration . "',0.00,'A','" . $txthidden . "','" . $ledgerName1['ledgerName'] . "','" . $cmbactype . "')");
        $result = mysqli_query($conn, $sqlquery4);

        $this->updateVoucherNo('PAY');
        return $result;
    }

    public function selectPurchaseInvoice() {
        $conn = $this->conn;
        $sqlquery = ("SELECT `pph_id` as PM_ID, `pph_trno` as GRNNo, `pph_refno` as BillNo, `pph_invdate` as PurchaseDate, ldg.ledgerName as S_SupplierName,'PI' as StatusPR,0 as GivenTotal,`pph_netamt` as BillAmount, `pph_paymenttype` as PaymentType,`pph_userid` as St_UserID,U.username as St_StaffName FROM `pos_pur_hdr` AS HDR INNER JOIN users AS U ON HDR.pph_userid = U.id INNER JOIN ledgermaster as ldg ON ldg.ledgerId= HDR.pph_suppid WHERE 1");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectPurchaseDtlById($trno) {
        $conn = $this->conn;
        $sqlquery = ("SELECT `ppd_id`, `ppd_trno`, `ppd_sno`, `ppd_itemcode`, `ppd_barcode`, `ppd_serialno`, `ppd_batch`, `ppd_prate`, `ppd_qty`, `ppd_amount`, `ppd_discper`, `ppd_discamt`, `ppd_totalamt`, `ppd_taxid`, `ppd_taxamt`, `ppd_grossamt`, `ppd_roundoff`, `ppd_netamt`, `ppd_expiry`, `ppd_costprice`, `ppd_sellprice`, `ppd_comid`, `ppd_locid`, `ppd_created`, `ppd_modified`,dim.dim_item_name as ppd_itemname FROM `pos_pur_dtl` as dtl INNER JOIN di_item_mast as dim ON dtl.ppd_itemcode = dim.dim_item_id WHERE dtl.ppd_trno = '" . $trno . "' ");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectPurchaseHdrById($trno) {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM `pos_pur_hdr` WHERE `pph_trno` = '" . $trno . "' ");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //Sales Process
    public function GetClientInfo() {
        $conn = $this->conn;
        $sqlquery = ("SELECT ledgermaster.ledgerId as Id,`branchname` as Name,`branchstatus` as Active FROM `di_branch_mast` INNER JOIN ledgermaster ON di_branch_mast.branchid = ledgermaster.ledgerrefId WHERE ledgermaster.ledgerType='CUS'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetPaymodeList() {
        $conn = $this->conn;
        $sqlquery = ("SELECT `pmode_id`,`pmode_name`,`pmode_type` FROM `di_paymode_mast` WHERE  `pmode_status`=1");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function deleteJourEntryBySales($id) {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `journaldetails` WHERE `refinvoiceno`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectMaxVoucherNo() {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `invoiceautono` SET `autono`= `autono` + 1  WHERE `autoname`='PAY'");
        mysqli_query($conn, $sqlquery);
        $sqlquery1 = ("SELECT `autono` FROM `invoiceautono` WHERE `autoname`='PAY'");
        $result = mysqli_query($conn, $sqlquery1);
        while ($row = mysqli_fetch_row($result)) {
            $Maxno = $row[0];
        }
        return $Maxno;
    }

    public function SaveSaleDtl($psid_invoice_sno, $psid_invoice_salid, $psid_invoice_date, $psid_invoice_trno, $psid_invoice_description, $psid_invoice_procode,
            $psid_invoice_proqty, $psid_invoice_rate, $psid_invoice_amt, $psid_invoice_itemdisp, $psid_invoice_itemdisamt,
            $psid_invoice_billdisp, $psid_invoice_billdisamt, $psid_invoice_gross, $psid_invoice_taxinex,
            $psid_invoice_taxvalue, $psid_invoice_taxamt, $psid_invoice_netamt) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_sale_invoicedtl`(`psid_invoice_sno`,`psid_invoice_salid`, `psid_invoice_prf`, `psid_invoice_date`,`psid_invoice_trno`,"
                . " `psid_invoice_description`, `psid_invoice_procode`, `psid_invoice_proqty`, `psid_invoice_rate`,`psid_invoice_amt`,"
                . " `psid_invoice_itemdisp`, `psid_invoice_itemdisamt`,`psid_invoice_billdisp`,`psid_invoice_billdisamt` ,`psid_invoice_gross`, `psid_invoice_taxinex`,"
                . " `psid_invoice_taxvalue`, `psid_invoice_taxamt`, `psid_invoice_netamt`,`psid_invoice_created`)"
                . " VALUES ('" . $psid_invoice_sno . "','" . $psid_invoice_salid . "','SH','" . $psid_invoice_date . "','" . $psid_invoice_trno . "',"
                . "'" . $psid_invoice_description . "','" . $psid_invoice_procode . "','" . $psid_invoice_proqty . "','" . $psid_invoice_rate . "','" . $psid_invoice_amt . "',"
                . "'" . $psid_invoice_itemdisp . "','" . $psid_invoice_itemdisamt . "','" . $psid_invoice_billdisp . "','" . $psid_invoice_billdisamt . "',"
                . "'" . $psid_invoice_gross . "','" . $psid_invoice_taxinex . "','" . $psid_invoice_taxvalue . "','" . $psid_invoice_taxamt . "','" . $psid_invoice_netamt . "',"
                . "'" . date("Y/m/d H:i:s") . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveSaleQuoteDtl($psid_invoice_sno, $psid_invoice_salid, $psid_invoice_date, $psid_invoice_trno, $psid_invoice_description, $psid_invoice_procode,
            $psid_invoice_proqty, $psid_invoice_rate, $psid_invoice_amt, $psid_invoice_itemdisp, $psid_invoice_itemdisamt,
            $psid_invoice_billdisp, $psid_invoice_billdisamt, $psid_invoice_gross, $psid_invoice_taxinex,
            $psid_invoice_taxvalue, $psid_invoice_taxamt, $psid_invoice_netamt) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_quote_invoicedtl`(`psid_invoice_sno`,`psid_invoice_salid`, `psid_invoice_prf`, `psid_invoice_date`,`psid_invoice_trno`,"
                . " `psid_invoice_description`, `psid_invoice_procode`, `psid_invoice_proqty`, `psid_invoice_rate`,`psid_invoice_amt`,"
                . " `psid_invoice_itemdisp`, `psid_invoice_itemdisamt`,`psid_invoice_billdisp`,`psid_invoice_billdisamt` ,`psid_invoice_gross`, `psid_invoice_taxinex`,"
                . " `psid_invoice_taxvalue`, `psid_invoice_taxamt`, `psid_invoice_netamt`,`psid_invoice_created`)"
                . " VALUES ('" . $psid_invoice_sno . "','" . $psid_invoice_salid . "','SH','" . $psid_invoice_date . "','" . $psid_invoice_trno . "',"
                . "'" . $psid_invoice_description . "','" . $psid_invoice_procode . "','" . $psid_invoice_proqty . "','" . $psid_invoice_rate . "','" . $psid_invoice_amt . "',"
                . "'" . $psid_invoice_itemdisp . "','" . $psid_invoice_itemdisamt . "','" . $psid_invoice_billdisp . "','" . $psid_invoice_billdisamt . "',"
                . "'" . $psid_invoice_gross . "','" . $psid_invoice_taxinex . "','" . $psid_invoice_taxvalue . "','" . $psid_invoice_taxamt . "','" . $psid_invoice_netamt . "',"
                . "'" . date("Y/m/d H:i:s") . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveSaleDtlUpdate($psid_invoice_id, $psid_invoice_sno, $psid_invoice_salid, $psid_invoice_date, $psid_invoice_trno, $psid_invoice_description, $psid_invoice_procode,
            $psid_invoice_proqty, $psid_invoice_rate, $psid_invoice_amt, $psid_invoice_itemdisp, $psid_invoice_itemdisamt,
            $psid_invoice_billdisp, $psid_invoice_billdisamt, $psid_invoice_gross, $psid_invoice_taxinex,
            $psid_invoice_taxvalue, $psid_invoice_taxamt, $psid_invoice_netamt, $comid, $locid) {
        $conn = $this->conn;
        $result = "";
        if ($psid_invoice_id == 0) {
            $sqlquery1 = ("INSERT INTO `pos_sale_invoicedtl`(`psid_invoice_sno`,`psid_invoice_salid`, `psid_invoice_prf`, `psid_invoice_date`,`psid_invoice_trno`,"
                    . " `psid_invoice_description`, `psid_invoice_procode`, `psid_invoice_proqty`, `psid_invoice_rate`,`psid_invoice_amt`,"
                    . " `psid_invoice_itemdisp`, `psid_invoice_itemdisamt`,`psid_invoice_billdisp`,`psid_invoice_billdisamt` ,`psid_invoice_gross`, `psid_invoice_taxinex`,"
                    . " `psid_invoice_taxvalue`, `psid_invoice_taxamt`, `psid_invoice_netamt`,`psid_invoice_created`)"
                    . " VALUES ('" . $psid_invoice_sno . "','" . $psid_invoice_salid . "','SH','" . $psid_invoice_date . "','" . $psid_invoice_trno . "',"
                    . "'" . $psid_invoice_description . "','" . $psid_invoice_procode . "','" . $psid_invoice_proqty . "','" . $psid_invoice_rate . "','" . $psid_invoice_amt . "',"
                    . "'" . $psid_invoice_itemdisp . "','" . $psid_invoice_itemdisamt . "','" . $psid_invoice_billdisp . "','" . $psid_invoice_billdisamt . "',"
                    . "'" . $psid_invoice_gross . "','" . $psid_invoice_taxinex . "','" . $psid_invoice_taxvalue . "','" . $psid_invoice_taxamt . "','" . $psid_invoice_netamt . "',"
                    . "'" . date("Y/m/d H:i:s") . "')");
            $result = mysqli_query($conn, $sqlquery1);
            $this->_UpdateLiveStockSales($psid_invoice_procode, $psid_invoice_proqty, $comid, $locid);
        } else {
            $result .= $this->_UpdateLiveStockEditSales($psid_invoice_procode, $psid_invoice_proqty, $comid, $locid);
            $sqlDeleteQuery = ("DELETE FROM `pos_sale_invoicedtl` WHERE `psid_invoice_id` ='" . $psid_invoice_id . "' AND `psid_invoice_trno`='" . $psid_invoice_trno . "'");
            $result .= mysqli_query($conn, $sqlDeleteQuery);
            $sqlquery2 = ("INSERT INTO `pos_sale_invoicedtl`(`psid_invoice_sno`,`psid_invoice_salid`, `psid_invoice_prf`, `psid_invoice_date`,`psid_invoice_trno`,"
                    . " `psid_invoice_description`, `psid_invoice_procode`, `psid_invoice_proqty`, `psid_invoice_rate`,`psid_invoice_amt`,"
                    . " `psid_invoice_itemdisp`, `psid_invoice_itemdisamt`,`psid_invoice_billdisp`,`psid_invoice_billdisamt` ,`psid_invoice_gross`, `psid_invoice_taxinex`,"
                    . " `psid_invoice_taxvalue`, `psid_invoice_taxamt`, `psid_invoice_netamt`,`psid_invoice_created`)"
                    . " VALUES ('" . $psid_invoice_sno . "','" . $psid_invoice_salid . "','SH','" . $psid_invoice_date . "','" . $psid_invoice_trno . "',"
                    . "'" . $psid_invoice_description . "','" . $psid_invoice_procode . "','" . $psid_invoice_proqty . "','" . $psid_invoice_rate . "','" . $psid_invoice_amt . "',"
                    . "'" . $psid_invoice_itemdisp . "','" . $psid_invoice_itemdisamt . "','" . $psid_invoice_billdisp . "','" . $psid_invoice_billdisamt . "',"
                    . "'" . $psid_invoice_gross . "','" . $psid_invoice_taxinex . "','" . $psid_invoice_taxvalue . "','" . $psid_invoice_taxamt . "','" . $psid_invoice_netamt . "',"
                    . "'" . date("Y/m/d H:i:s") . "')");
            $result .= mysqli_query($conn, $sqlquery2);
            $this->_UpdateLiveStockSales($psid_invoice_procode, $psid_invoice_proqty, $comid, $locid);
        }
        return $result;
    }

    public function SaveSaleDtlQuoteUpdate($psid_invoice_id, $psid_invoice_sno, $psid_invoice_salid, $psid_invoice_date, $psid_invoice_trno, $psid_invoice_description, $psid_invoice_procode,
            $psid_invoice_proqty, $psid_invoice_rate, $psid_invoice_amt, $psid_invoice_itemdisp, $psid_invoice_itemdisamt,
            $psid_invoice_billdisp, $psid_invoice_billdisamt, $psid_invoice_gross, $psid_invoice_taxinex,
            $psid_invoice_taxvalue, $psid_invoice_taxamt, $psid_invoice_netamt, $comid, $locid) {
        $conn = $this->conn;
        $result = "";
        if ($psid_invoice_id == 0) {
            $sqlquery1 = ("INSERT INTO `pos_quote_invoicedtl`(`psid_invoice_sno`,`psid_invoice_salid`, `psid_invoice_prf`, `psid_invoice_date`,`psid_invoice_trno`,"
                    . " `psid_invoice_description`, `psid_invoice_procode`, `psid_invoice_proqty`, `psid_invoice_rate`,`psid_invoice_amt`,"
                    . " `psid_invoice_itemdisp`, `psid_invoice_itemdisamt`,`psid_invoice_billdisp`,`psid_invoice_billdisamt` ,`psid_invoice_gross`, `psid_invoice_taxinex`,"
                    . " `psid_invoice_taxvalue`, `psid_invoice_taxamt`, `psid_invoice_netamt`,`psid_invoice_created`)"
                    . " VALUES ('" . $psid_invoice_sno . "','" . $psid_invoice_salid . "','SH','" . $psid_invoice_date . "','" . $psid_invoice_trno . "',"
                    . "'" . $psid_invoice_description . "','" . $psid_invoice_procode . "','" . $psid_invoice_proqty . "','" . $psid_invoice_rate . "','" . $psid_invoice_amt . "',"
                    . "'" . $psid_invoice_itemdisp . "','" . $psid_invoice_itemdisamt . "','" . $psid_invoice_billdisp . "','" . $psid_invoice_billdisamt . "',"
                    . "'" . $psid_invoice_gross . "','" . $psid_invoice_taxinex . "','" . $psid_invoice_taxvalue . "','" . $psid_invoice_taxamt . "','" . $psid_invoice_netamt . "',"
                    . "'" . date("Y/m/d H:i:s") . "')");
            $result = mysqli_query($conn, $sqlquery1);
        } else {
            $sqlDeleteQuery = ("DELETE FROM `pos_quote_invoicedtl` WHERE `psid_invoice_id` ='" . $psid_invoice_id . "' AND `psid_invoice_trno`='" . $psid_invoice_trno . "'");
            $result .= mysqli_query($conn, $sqlDeleteQuery);
            $sqlquery2 = ("INSERT INTO `pos_quote_invoicedtl`(`psid_invoice_sno`,`psid_invoice_salid`, `psid_invoice_prf`, `psid_invoice_date`,`psid_invoice_trno`,"
                    . " `psid_invoice_description`, `psid_invoice_procode`, `psid_invoice_proqty`, `psid_invoice_rate`,`psid_invoice_amt`,"
                    . " `psid_invoice_itemdisp`, `psid_invoice_itemdisamt`,`psid_invoice_billdisp`,`psid_invoice_billdisamt` ,`psid_invoice_gross`, `psid_invoice_taxinex`,"
                    . " `psid_invoice_taxvalue`, `psid_invoice_taxamt`, `psid_invoice_netamt`,`psid_invoice_created`)"
                    . " VALUES ('" . $psid_invoice_sno . "','" . $psid_invoice_salid . "','SH','" . $psid_invoice_date . "','" . $psid_invoice_trno . "',"
                    . "'" . $psid_invoice_description . "','" . $psid_invoice_procode . "','" . $psid_invoice_proqty . "','" . $psid_invoice_rate . "','" . $psid_invoice_amt . "',"
                    . "'" . $psid_invoice_itemdisp . "','" . $psid_invoice_itemdisamt . "','" . $psid_invoice_billdisp . "','" . $psid_invoice_billdisamt . "',"
                    . "'" . $psid_invoice_gross . "','" . $psid_invoice_taxinex . "','" . $psid_invoice_taxvalue . "','" . $psid_invoice_taxamt . "','" . $psid_invoice_netamt . "',"
                    . "'" . date("Y/m/d H:i:s") . "')");
            $result .= mysqli_query($conn, $sqlquery2);
        }
        return $result;
    }

    public function SaveSaleHdr($psih_invoice_trno, $psih_invoice_date, $psih_invoice_description, $psih_invoice_tqty, $psih_invoice_tamount,
            $psih_invoice_titemdisper, $psih_invoice_titemdisamt, $psih_invoice_tbilldiscper, $psih_invoice_tbilldiscamt, $psih_invoice_tgrossamt,
            $psih_invoice_ttaxamt, $psih_invoice_tnetamt, $psih_invoice_saletype, $psih_invoice_billtype, $psih_invoice_billstatus, $psih_invoice_customerid,
            $psih_invoice_userid, $psih_invoice_comid, $psih_invoice_locid, $psih_invoice_billremarks, $psih_invoice_advamt, $psih_invoice_outstanding,
            $psih_invoice_givenamt, $psih_invoice_balamt) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_sale_invoicehdr`(`psih_invoice_trno`, `psih_invoice_date`, `psih_invoice_description`, `psih_invoice_prefix`,"
                . "`psih_invoice_tqty`, `psih_invoice_tamount`, `psih_invoice_titemdisper`, `psih_invoice_titemdisamt`,  `psih_invoice_tbilldiscper`,"
                . " `psih_invoice_tbilldiscamt`,`psih_invoice_tgrossamt`, `psih_invoice_ttaxamt`, `psih_invoice_tnetamt`, `psih_invoice_saletype`, "
                . "`psih_invoice_billtype`, `psih_invoice_billstatus`, `psih_invoice_customerid`, `psih_invoice_userid`, `psih_invoice_comid`, "
                . "`psih_invoice_locid`, `psih_invoice_billremarks`, `psih_invoice_advamt`, `psih_invoice_outstanding`, `psih_invoice_givenamt`, "
                . "`psih_invoice_balamt`, `psih_invoice_created`)"
                . " VALUES (  '" . $psih_invoice_trno . "','" . $psih_invoice_date . "','" . $psih_invoice_description . "','SH','" . $psih_invoice_tqty . "',"
                . "'" . $psih_invoice_tamount . "','" . $psih_invoice_titemdisper . "','" . $psih_invoice_titemdisamt . "','" . $psih_invoice_tbilldiscper . "',"
                . "'" . $psih_invoice_tbilldiscamt . "','" . $psih_invoice_tgrossamt . "','" . $psih_invoice_ttaxamt . "','" . $psih_invoice_tnetamt . "',"
                . "'" . $psih_invoice_saletype . "','" . $psih_invoice_billtype . "','" . $psih_invoice_billstatus . "','" . $psih_invoice_customerid . "',"
                . "'" . $psih_invoice_userid . "','" . $psih_invoice_comid . "','" . $psih_invoice_locid . "','" . $psih_invoice_billremarks . "','" . $psih_invoice_advamt . "',"
                . "'" . $psih_invoice_outstanding . "','" . $psih_invoice_givenamt . "','" . $psih_invoice_balamt . "','" . date("Y-m-d H:i:s") . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveSaleQuoteHdr($psih_invoice_trno, $psih_invoice_date, $psih_invoice_description, $psih_invoice_tqty, $psih_invoice_tamount,
            $psih_invoice_titemdisper, $psih_invoice_titemdisamt, $psih_invoice_tbilldiscper, $psih_invoice_tbilldiscamt, $psih_invoice_tgrossamt,
            $psih_invoice_ttaxamt, $psih_invoice_tnetamt, $psih_invoice_saletype, $psih_invoice_billtype, $psih_invoice_billstatus, $psih_invoice_customerid,
            $psih_invoice_userid, $psih_invoice_comid, $psih_invoice_locid, $psih_invoice_billremarks, $psih_invoice_advamt, $psih_invoice_outstanding,
            $psih_invoice_givenamt, $psih_invoice_balamt) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_quote_invoicehdr`(`psih_invoice_trno`, `psih_invoice_date`, `psih_invoice_description`, `psih_invoice_prefix`,"
                . "`psih_invoice_tqty`, `psih_invoice_tamount`, `psih_invoice_titemdisper`, `psih_invoice_titemdisamt`,  `psih_invoice_tbilldiscper`,"
                . " `psih_invoice_tbilldiscamt`,`psih_invoice_tgrossamt`, `psih_invoice_ttaxamt`, `psih_invoice_tnetamt`, `psih_invoice_saletype`, "
                . "`psih_invoice_billtype`, `psih_invoice_billstatus`, `psih_invoice_customerid`, `psih_invoice_userid`, `psih_invoice_comid`, "
                . "`psih_invoice_locid`, `psih_invoice_billremarks`, `psih_invoice_advamt`, `psih_invoice_outstanding`, `psih_invoice_givenamt`, "
                . "`psih_invoice_balamt`, `psih_invoice_created`)"
                . " VALUES (  '" . $psih_invoice_trno . "','" . $psih_invoice_date . "','" . $psih_invoice_description . "','SH','" . $psih_invoice_tqty . "',"
                . "'" . $psih_invoice_tamount . "','" . $psih_invoice_titemdisper . "','" . $psih_invoice_titemdisamt . "','" . $psih_invoice_tbilldiscper . "',"
                . "'" . $psih_invoice_tbilldiscamt . "','" . $psih_invoice_tgrossamt . "','" . $psih_invoice_ttaxamt . "','" . $psih_invoice_tnetamt . "',"
                . "'" . $psih_invoice_saletype . "','" . $psih_invoice_billtype . "','" . $psih_invoice_billstatus . "','" . $psih_invoice_customerid . "',"
                . "'" . $psih_invoice_userid . "','" . $psih_invoice_comid . "','" . $psih_invoice_locid . "','" . $psih_invoice_billremarks . "','" . $psih_invoice_advamt . "',"
                . "'" . $psih_invoice_outstanding . "','" . $psih_invoice_givenamt . "','" . $psih_invoice_balamt . "','" . date("Y-m-d H:i:s") . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveSaleUpdate($psih_invoice_trno, $psih_invoice_date, $psih_invoice_description, $psih_invoice_tqty, $psih_invoice_tamount,
            $psih_invoice_titemdisper, $psih_invoice_titemdisamt, $psih_invoice_tbilldiscper, $psih_invoice_tbilldiscamt, $psih_invoice_tgrossamt,
            $psih_invoice_ttaxamt, $psih_invoice_tnetamt, $psih_invoice_saletype, $psih_invoice_billtype, $psih_invoice_billstatus, $psih_invoice_customerid,
            $psih_invoice_userid, $psih_invoice_comid, $psih_invoice_locid, $psih_invoice_billremarks, $psih_invoice_advamt, $psih_invoice_outstanding,
            $psih_invoice_givenamt, $psih_invoice_balamt) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `pos_sale_invoicehdr` SET  `psih_invoice_date`='" . $psih_invoice_date . "',`psih_invoice_description`='" . $psih_invoice_description . "',"
                . "`psih_invoice_prefix`='SH',`psih_invoice_tqty`='" . $psih_invoice_tqty . "',`psih_invoice_tamount`='" . $psih_invoice_tamount . "',"
                . "`psih_invoice_titemdisper`='" . $psih_invoice_titemdisper . "',`psih_invoice_titemdisamt`='" . $psih_invoice_titemdisamt . "',"
                . "`psih_invoice_tgrossamt`='" . $psih_invoice_tgrossamt . "',`psih_invoice_tbilldiscper`='" . $psih_invoice_tbilldiscper . "',"
                . "`psih_invoice_tbilldiscamt`='" . $psih_invoice_tbilldiscamt . "',`psih_invoice_ttaxamt`='" . $psih_invoice_ttaxamt . "',"
                . "`psih_invoice_tnetamt`='" . $psih_invoice_tnetamt . "',`psih_invoice_saletype`='" . $psih_invoice_saletype . "',`psih_invoice_billtype`='" . $psih_invoice_billtype . "',"
                . "`psih_invoice_billstatus`='" . $psih_invoice_billstatus . "',`psih_invoice_customerid`='" . $psih_invoice_customerid . "',`psih_invoice_userid`='" . $psih_invoice_userid . "',"
                . "`psih_invoice_comid`='" . $psih_invoice_comid . "',`psih_invoice_locid`='" . $psih_invoice_locid . "',`psih_invoice_billremarks`='" . $psih_invoice_billremarks . "',"
                . "`psih_invoice_advamt`='" . $psih_invoice_advamt . "',`psih_invoice_outstanding`='" . $psih_invoice_outstanding . "',`psih_invoice_givenamt`='" . $psih_invoice_givenamt . "',"
                . "`psih_invoice_balamt`='" . $psih_invoice_balamt . "'  WHERE `psih_invoice_trno`='" . $psih_invoice_trno . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveSaleQuoteUpdate($psih_invoice_trno, $psih_invoice_date, $psih_invoice_description, $psih_invoice_tqty, $psih_invoice_tamount,
            $psih_invoice_titemdisper, $psih_invoice_titemdisamt, $psih_invoice_tbilldiscper, $psih_invoice_tbilldiscamt, $psih_invoice_tgrossamt,
            $psih_invoice_ttaxamt, $psih_invoice_tnetamt, $psih_invoice_saletype, $psih_invoice_billtype, $psih_invoice_billstatus, $psih_invoice_customerid,
            $psih_invoice_userid, $psih_invoice_comid, $psih_invoice_locid, $psih_invoice_billremarks, $psih_invoice_advamt, $psih_invoice_outstanding,
            $psih_invoice_givenamt, $psih_invoice_balamt) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `pos_quote_invoicehdr` SET  `psih_invoice_date`='" . $psih_invoice_date . "',`psih_invoice_description`='" . $psih_invoice_description . "',"
                . "`psih_invoice_prefix`='SH',`psih_invoice_tqty`='" . $psih_invoice_tqty . "',`psih_invoice_tamount`='" . $psih_invoice_tamount . "',"
                . "`psih_invoice_titemdisper`='" . $psih_invoice_titemdisper . "',`psih_invoice_titemdisamt`='" . $psih_invoice_titemdisamt . "',"
                . "`psih_invoice_tgrossamt`='" . $psih_invoice_tgrossamt . "',`psih_invoice_tbilldiscper`='" . $psih_invoice_tbilldiscper . "',"
                . "`psih_invoice_tbilldiscamt`='" . $psih_invoice_tbilldiscamt . "',`psih_invoice_ttaxamt`='" . $psih_invoice_ttaxamt . "',"
                . "`psih_invoice_tnetamt`='" . $psih_invoice_tnetamt . "',`psih_invoice_saletype`='" . $psih_invoice_saletype . "',`psih_invoice_billtype`='" . $psih_invoice_billtype . "',"
                . "`psih_invoice_billstatus`='" . $psih_invoice_billstatus . "',`psih_invoice_customerid`='" . $psih_invoice_customerid . "',`psih_invoice_userid`='" . $psih_invoice_userid . "',"
                . "`psih_invoice_comid`='" . $psih_invoice_comid . "',`psih_invoice_locid`='" . $psih_invoice_locid . "',`psih_invoice_billremarks`='" . $psih_invoice_billremarks . "',"
                . "`psih_invoice_advamt`='" . $psih_invoice_advamt . "',`psih_invoice_outstanding`='" . $psih_invoice_outstanding . "',`psih_invoice_givenamt`='" . $psih_invoice_givenamt . "',"
                . "`psih_invoice_balamt`='" . $psih_invoice_balamt . "'  WHERE `psih_invoice_trno`='" . $psih_invoice_trno . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesBill($date) {
        $conn = $this->conn;
        $sqlquery = ("SELECT `psih_invoice_id` as Sal_ID, `psih_invoice_trno` as Sal_BillNo, `psih_invoice_date` as Sal_Date,"
                . " `psih_invoice_description` as Customer,`psih_invoice_tqty` as Sal_Qty,"
                . "  `psih_invoice_tgrossamt` as Sal_TotAmt,  `psih_invoice_tnetamt` as Sal_NetAmt,"
                . " `psih_invoice_billtype` as PaymentType,psih_invoice_comid as COMID,psih_invoice_locid as LOCID    FROM `pos_sale_invoicehdr` WHERE  `psih_invoice_date`='" . $date . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesQuoteBill($date) {
        $conn = $this->conn;
        $sqlquery = ("SELECT `psih_invoice_id` as Sal_ID, `psih_invoice_trno` as Sal_BillNo, `psih_invoice_date` as Sal_Date,"
                . " `psih_invoice_description` as Customer,`psih_invoice_tqty` as Sal_Qty,"
                . "  `psih_invoice_tgrossamt` as Sal_TotAmt,  `psih_invoice_tnetamt` as Sal_NetAmt,"
                . " `psih_invoice_billtype` as PaymentType , psih_invoice_comid as COMID,psih_invoice_locid as LOCID   FROM `pos_quote_invoicehdr` WHERE  `psih_invoice_date`='" . $date . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesBySalID_HDR($Sal_ID) {
        $conn = $this->conn;
        $sqlquery = ("SELECT *  FROM `pos_sale_invoicehdr` WHERE  `psih_invoice_id`='" . $Sal_ID . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesBySalID_DTL($Sal_ID) {
        $conn = $this->conn;
        $sqlquery = ("SELECT *  FROM `pos_sale_invoicedtl` WHERE  `psid_invoice_salid`='" . $Sal_ID . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesByQuoteSalID_HDR($Sal_ID) {
        $conn = $this->conn;
        $sqlquery = ("SELECT *  FROM `pos_quote_invoicehdr` WHERE  `psih_invoice_id`='" . $Sal_ID . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesByQuoteSalID_DTL($Sal_ID) {
        $conn = $this->conn;
        $sqlquery = ("SELECT *  FROM `pos_quote_invoicedtl` WHERE  `psid_invoice_salid`='" . $Sal_ID . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesId($billno) {
        $conn = $this->conn;
        $sqlquery = ("SELECT `psih_invoice_id` FROM `pos_sale_invoicehdr` WHERE `psih_invoice_trno`='" . $billno . "'");
        $result = mysqli_query($conn, $sqlquery);
        while ($row = mysqli_fetch_row($result)) {
            $Maxno = $row[0];
        }
        return $Maxno;
    }

    public function GetSalesQuoteId($billno) {
        $conn = $this->conn;
        $sqlquery = ("SELECT `psih_invoice_id` FROM `pos_quote_invoicehdr` WHERE `psih_invoice_trno`='" . $billno . "'");
        $result = mysqli_query($conn, $sqlquery);
        while ($row = mysqli_fetch_row($result)) {
            $Maxno = $row[0];
        }
        return $Maxno;
    }

    public function selectMaxInvoice() {
        $conn = $this->conn;
        $sqlquery = ("SELECT `autono` FROM `invoiceautono` WHERE `autoname`='SAL'");
        $result = mysqli_query($conn, $sqlquery);
        while ($row = mysqli_fetch_row($result)) {
            $Maxno = $row[0];
        }
        return $Maxno;
    }

    public function UpdateInvoiceNo() {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `invoiceautono` SET `autono`= `autono` + 1  WHERE `autoname`='SAL'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function UpdateQuoteNo() {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `invoiceautono` SET `autono`= `autono` + 1  WHERE `autoname`='QUO'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteBySalID_DTL($Sal_ID) {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `pos_sale_invoicedtl` WHERE  `psid_invoice_salid`='" . $Sal_ID . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesByBillno_HDR($billno) {
        $conn = $this->conn;
        $sqlquery = ("SELECT *  FROM `pos_sale_invoicehdr` WHERE  `psih_invoice_trno`='" . $billno . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesByBillno_DTL($billno) {
        $conn = $this->conn;
        $sqlquery = ("SELECT *  FROM `pos_sale_invoicedtl` WHERE  `psid_invoice_trno`='" . $billno . "' ORDER BY `psid_invoice_id` ASC ");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesByQuoteBillno_HDR($billno) {
        $conn = $this->conn;
        $sqlquery = ("SELECT *  FROM `pos_quote_invoicehdr` WHERE  `psih_invoice_trno`='" . $billno . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesByQuoteBillno_DTL($billno) {
        $conn = $this->conn;
        $sqlquery = ("SELECT *  FROM `pos_quote_invoicedtl` WHERE  `psid_invoice_trno`='" . $billno . "' ORDER BY `psid_invoice_id` ASC ");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //Ledger Group Entry 
    public function storegroupData($groupName) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `groupmaster`(`groupName`) VALUES ('" . $groupName . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updategroupData($id, $groupName) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `groupmaster` SET `groupName`='" . $groupName . "' WHERE `groupId`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectgroup() {
        $conn = $this->conn;
        $sqlquery = ("SELECT groupid as Id,groupname as Name FROM `groupmaster`");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectgroupById($id) {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM `groupmaster` WHERE `groupId`=" . $id);
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return mysqli_fetch_assoc($result);
    }

    //Parent Entry 
    public function storeparentData($parentName) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `parentmaster`(`parentName`) VALUES ('" . $parentName . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updateparentData($id, $parentName) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `parentmaster` SET `parentName`='" . $parentName . "' WHERE `parentId`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectparent() {
        $conn = $this->conn;
        $sqlquery = ("SELECT `parentId` as Id,`parentName` as Name FROM `parentmaster`");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectparentById($id) {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM `parentmaster` WHERE `parentId`=" . $id);
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return mysqli_fetch_assoc($result);
    }

    //Ledger Entry
    public function storeLedgerData($ledgerrefId, $ledgerName, $ledgerparenId, $ledgergroupId, $ledgerType, $ledgeropenDate, $ledgeropenbal, $ledgerdrcr, $ledgerActive) {
        $conn = $this->conn;
        $sqlquery = (" INSERT INTO `ledgermaster`(`ledgerrefId`, `ledgerName`, `ledgerparenId`, `ledgergroupId`,`ledgerType`, `ledgeropenDate`, `ledgeropenbal`, `ledgerdrcr`, `ledgerActive`) VALUES"
                . "(" . $ledgerrefId . ",'" . $ledgerName . "'," . $ledgerparenId . "," . $ledgergroupId . ",'" . $ledgerType . "','" . $ledgeropenDate . "'," . $ledgeropenbal . ",'" . $ledgerdrcr . "','" . $ledgerActive . "')");
        //return $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updateLedgerData($ledgerId, $ledgerrefId, $ledgerName, $ledgerparenId, $ledgergroupId, $ledgerType, $ledgeropenDate, $ledgeropenbal, $ledgerdrcr, $ledgerActive) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `ledgermaster` SET `ledgerrefId`=" . $ledgerrefId . ""
                . ",`ledgerName`='" . $ledgerName . "',`ledgerparenId`=" . $ledgerparenId . ",`ledgergroupId`=" . $ledgergroupId . ",`ledgerType`='" . $ledgerType . "'"
                . ",`ledgeropenDate`='" . $ledgeropenDate . "',`ledgeropenbal`=" . $ledgeropenbal . ",`ledgerdrcr`='" . $ledgerdrcr . "'"
                . ",`ledgerActive`='" . $ledgerActive . "' WHERE `ledgerId`=" . $ledgerId . "");
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectledger() {
        $conn = $this->conn;
        $sqlquery = ("SELECT lg.ledgerId as Id,lg.ledgerName as LedgerName,gpm.groupName as GroupName,pm.parentName as ParentName,lg.ledgerdrcr as DrCr, case lg.ledgerActive when 'Active' then '1' when 'InActive' then '0' end as Active,lg.ledgerType as LedgerType,lg.ledgeropenDate as OpenDate,lg.ledgeropenbal as OpeningBalance FROM `ledgermaster` lg INNER JOIN `groupmaster` gpm ON lg.ledgergroupId=gpm.groupId INNER JOIN `parentmaster` pm ON lg.ledgerparenId=pm.parentID WHERE 1 ORDER BY ledgerName ASC;");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectledgerall() {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM `ledgermaster` WHERE `ledgergroupId` <> 1 AND `ledgerActive` ='Active' ORDER BY ledgerName ASC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectledgerById($id) {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM `ledgermaster` INNER JOIN  parentmaster ON parentmaster.`parentID`=ledgermaster.`ledgerparenId` INNER JOIN groupmaster ON groupmaster.`groupId`=ledgermaster.`ledgergroupId` WHERE ledgermaster.`ledgerId`=" . $id);
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return mysqli_query($result);
    }

    public function bankList() {
        $conn = $this->conn;
        $sqlquery = ("SELECT lg.ledgerId,lg.ledgerName,gpm.groupName,pm.parentName,lg.ledgerdrcr,lg.ledgerActive,lg.ledgerType as LedgerType FROM `ledgermaster` lg INNER JOIN `groupmaster` gpm ON lg.ledgergroupId=gpm.groupId INNER JOIN `parentmaster` pm ON lg.ledgerparenId=pm.parentID WHERE lg.ledgerActive='Active' ORDER BY ledgerName ASC");
// echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteJournalEntry($billno) {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `journaldetails` WHERE `billno`=" . $billno); //SALES//pay
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GETJournalEntry($date) {
        $conn = $this->conn;
        $sqlquery = ("SELECT `JOUR_ENTRY`.`description` as DESCRIP, `JOUR_ENTRY`.`dr` AS DR, `JOUR_ENTRY`.`cr` CR,`HEAD_MASTER`.`ledgerName` AS HEAD_NAME,`JOUR_ENTRY`.`entrydate` AS ENTRY_DATE,`JOUR_ENTRY`.`billno` AS BillNo, `modifydate`,  `JOUR_ENTRY`.`modetype` as PayMode,  `JOUR_ENTRY`.`actype`  as JModeStatus,`JOUR_ENTRY`.`jid` FROM `journaldetails` AS JOUR_ENTRY INNER JOIN `ledgermaster` as HEAD_MASTER ON `JOUR_ENTRY`.`ledgerid`=`HEAD_MASTER`.`ledgerId` WHERE `JOUR_ENTRY`.`entrydate`='" . $date . "' AND `JOUR_ENTRY`.`actype` IN ('PAY','REC','JUR')"); //SALES//pay
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GETJournalEntryByBillNo($billno) {
        $conn = $this->conn;
        $sqlquery = ("SELECT `HEAD_MASTER`.`ledgerName` AS HEAD_NAME, `JOUR_ENTRY`.`description` as DESCRIP,CASE WHEN `JOUR_ENTRY`.`dr`=0.00 THEN NULL ELSE `JOUR_ENTRY`.`dr` END AS Debit, CASE WHEN `JOUR_ENTRY`.`cr`=0.00 THEN NULL ELSE `JOUR_ENTRY`.`cr` END AS Credit,`JOUR_ENTRY`.`entrydate` AS ENTRY_DATE,'0.00' AS Openingbalance,'0.00' AS Closingbalance, `HEAD_MASTER`.`ledgerId` AS HEAD_ID FROM `journaldetails` AS JOUR_ENTRY INNER JOIN `ledgermaster` as HEAD_MASTER ON `JOUR_ENTRY`.`ledgerid`=`HEAD_MASTER`.`ledgerId` WHERE `JOUR_ENTRY`.`billno`='" . $billno . "'"); //SALES//pay
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetLedgerAll() {
        $conn = $this->conn;
        $sqlquery = ("SELECT lg.ledgerId AS HEAD_ID,lg.ledgerName AS HEAD_NAME FROM `ledgermaster` lg INNER JOIN `groupmaster` gpm ON lg.ledgergroupId=gpm.groupId INNER JOIN `parentmaster` pm ON lg.ledgerparenId=pm.parentID WHERE lg.ledgerActive='Active'  ORDER BY ledgerName ASC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GETJournalEntryByID($fromdate, $todate, $ledgerid) {
        $conn = $this->conn;
        //Opening Balance start
        $drledgeramt = 0.0;
        $crledgeramt = 0.0;
        $clstxt = "";
        $sqlDRCRAMT = ("SELECT IFNULL(sum(`dr`),'0.00') AS DEBIT,IFNULL(sum(`cr`),'0.00') AS CREDIT FROM `journaldetails` WHERE `ledgerid`= '" . $ledgerid . "' AND `entrydate` < '" . $fromdate . "';");
        $resultDRCRAMT = mysqli_query($conn, $sqlDRCRAMT);
        while ($row = mysqli_fetch_row($resultDRCRAMT)) {
            $dramt = $row[0];
            $cramt = $row[1];
        }
        $sqlLEDGERDRCRAMT = ("SELECT CASE WHEN `ledgermaster`.`ledgerdrcr`='Dr' THEN sum(`ledgermaster`.`ledgeropenbal`) ELSE 0.00 END AS DEBIT,CASE WHEN `ledgermaster`.`ledgerdrcr`='Cr' THEN sum(`ledgermaster`.`ledgeropenbal`) ELSE 0.00 END AS CREDIT FROM `ledgermaster` WHERE `ledgermaster`.`ledgerId`='" . $ledgerid . "';");
        $resultLEDGERDRCRAMT = mysqli_query($conn, $sqlLEDGERDRCRAMT);
        while ($row = mysqli_fetch_row($resultLEDGERDRCRAMT)) {
            $drledgeramt = $row[0];
            $crledgeramt = $row[1];
        }
        $dramt = $dramt + $drledgeramt;
        $cramt = $cramt + $crledgeramt;
        $openingBalance = $cramt - $dramt;
        if ($openingBalance < 0) {
            $openingBalance = $dramt - $cramt;
            $clstxt = 'Dr';
        } else {
            $clstxt = 'Cr';
        }
        $netamt = number_format((float) $openingBalance, 2, '.', '');
        $opBalance = $netamt . ' ' . $clstxt;
        //Opening Balance End
        //ClosingBalance start
        $clstxtCls = "";
        $sqlclsDRCRAMT = ("SELECT sum(`dr`) AS DEBIT,sum(`cr`) AS CREDIT FROM `journaldetails` WHERE `ledgerid`= '" . $ledgerid . "' AND `entrydate` <= '" . $todate . "';");
        $resultclsDRCRAMT = mysqli_query($conn, $sqlclsDRCRAMT);
        while ($row = mysqli_fetch_row($resultclsDRCRAMT)) {
            $dramtcls = $row[0];
            $cramtcls = $row[1];
        }
        $dramtcls = $dramtcls + $drledgeramt;
        $cramtcls = $cramtcls + $crledgeramt;
        $ClosingBalance = $cramtcls - $dramtcls;
        if ($ClosingBalance < 0) {
            $ClosingBalance = $dramtcls - $cramtcls;
            $clstxtCls = 'Dr';
        } else {
            $clstxtCls = 'Cr';
        }
        $netamtcls = number_format((float) $ClosingBalance + $openingBalance, 2, '.', '');
        $clsBalance = $netamtcls . ' ' . $clstxtCls;

        $sqlquery = ("SELECT `HEAD_MASTER`.`ledgerName` AS HEAD_NAME, CONCAT(`JOUR_ENTRY`.`description`,' ',`JOUR_ENTRY`.narration) as DESCRIP,CASE WHEN `JOUR_ENTRY`.`dr`=0.00 THEN NULL ELSE `JOUR_ENTRY`.`dr` END AS Debit, CASE WHEN `JOUR_ENTRY`.`cr`=0.00 THEN NULL ELSE `JOUR_ENTRY`.`cr` END AS Credit,`JOUR_ENTRY`.`entrydate` AS ENTRY_DATE,'" . $opBalance . "' AS Openingbalance,'" . $clsBalance . "' AS Closingbalance, `HEAD_MASTER`.`ledgerId` AS HEAD_ID,`JOUR_ENTRY`.`actype`  as JModeStatus,`JOUR_ENTRY`.`billno` AS BillNo FROM `journaldetails` AS JOUR_ENTRY INNER JOIN `ledgermaster` as HEAD_MASTER ON `JOUR_ENTRY`.`ledgerid`=`HEAD_MASTER`.`ledgerId` WHERE `JOUR_ENTRY`.`entrydate`>='" . $fromdate . "' AND `JOUR_ENTRY`.`entrydate`<='" . $todate . "' AND `JOUR_ENTRY`.`ledgerid`='" . $ledgerid . "'"); //SALES//pay
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetClsBalance($ledgerId) {
        $conn = $this->conn;
        $drledgeramt = 0.0;
        $crledgeramt = 0.0;
        $clstxt = "";
        $sqlDRCRAMT = ("SELECT sum(`dr`) AS DEBIT,sum(`cr`) AS CREDIT FROM `journaldetails` WHERE `ledgerid`='" . $ledgerId . "';");
        $resultDRCRAMT = mysqli_query($conn, $sqlDRCRAMT);
        while ($row = mysqli_fetch_row($resultDRCRAMT)) {
            $dramt = $row[0];
            $cramt = $row[1];
        }
        $sqlLEDGERDRCRAMT = ("SELECT CASE WHEN `ledgermaster`.`ledgerdrcr`='Dr' THEN sum(`ledgermaster`.`ledgeropenbal`) ELSE 0.00 END AS DEBIT,CASE WHEN `ledgermaster`.`ledgerdrcr`='Cr' THEN sum(`ledgermaster`.`ledgeropenbal`) ELSE 0.00 END AS CREDIT FROM `ledgermaster` WHERE `ledgermaster`.`ledgerId`='" . $ledgerId . "';");
        $resultLEDGERDRCRAMT = mysqli_query($conn, $sqlLEDGERDRCRAMT);
        while ($row = mysqli_fetch_row($resultLEDGERDRCRAMT)) {
            $drledgeramt = $row[0];
            $crledgeramt = $row[1];
        }
        $dramt = $dramt + $drledgeramt;
        $cramt = $cramt + $crledgeramt;
        $closingBalance = $cramt - $dramt;
        if ($closingBalance < 0) {
            $closingBalance = $dramt - $cramt;
            $clstxt = 'Dr';
        } else {
            $clstxt = 'Cr';
        }
        $netamt = number_format((float) $closingBalance, 2, '.', '');
        return $netamt . ' ' . $clstxt;
    }

    public function storeJournalpayments($ledgerid, $description, $dr, $cr, $jstatus, $billno, $entrydate, $actype, $modetype, $narration, $status, $username, $description2, $ledgerid2, $bankname, $chequeamt, $chequedate, $chequeno, $comid, $locid) {
        $conn = $this->conn;
        $sqlquery4 = ("INSERT INTO `journaldetails`(`ledgerid`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`,`comid`,`locid`)"
                . "VALUES ('" . $ledgerid . "','" . $description . "'"
                . ",'" . $dr . "','" . $cr . "','" . $jstatus . "','" . $billno . "','" . $entrydate . "','" . date('Y/m/d') . "','" . $actype . "','" . $modetype . "','" . $narration . "',0.00,'" . $status . "','" . $username . "','" . $description2 . "','" . $ledgerid2 . "','" . $comid . "','" . $locid . "')");
        $result = mysqli_query($conn, $sqlquery4);

        $sqlquery2 = ("INSERT INTO `journaldetails`(`ledgerid`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`,`comid`,`locid`)"
                . "VALUES ('" . $ledgerid2 . "','" . $description2 . "'"
                . ",'" . $cr . "','" . $dr . "','Cr','" . $billno . "','" . $entrydate . "','" . date('Y/m/d') . "','" . $actype . "','" . $modetype . "','" . $narration . "',0.00,'" . $status . "','" . $username . "','" . $description . "','" . $ledgerid . "','" . $comid . "','" . $locid . "')");
        $result = mysqli_query($conn, $sqlquery2);

        if ($modetype == 'CQ') {
            $conn = $this->conn;
            $sqlMode = ("INSERT INTO `chequedetails`(`jorunalrefid`, `ledgerid`, `bankname`, `chequeamt`, `chequedate`,`chequeno`, `chequestatus`)"
                    . " VALUES ('" . $billno . "','" . $ledgerid2 . "','" . $bankname . "',$chequeamt,'" . $chequedate . "','" . $chequeno . "','I')");
            $result = mysqli_query($conn, $sqlMode);
            return $result;
        }
        return $result; //$sqlquery4 . '' . $sqlquery2;
    }

    public function storeJournalReceipt($ledgerid, $description, $dr, $cr, $jstatus, $billno, $entrydate, $actype, $modetype, $narration, $status, $username, $description2, $ledgerid2, $bankname, $chequeamt, $chequedate, $chequeno, $comid, $locid) {
        $conn = $this->conn;
        $sqlquery4 = ("INSERT INTO `journaldetails`(`ledgerid`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`,`comid`,`locid`)"
                . "VALUES ('" . $ledgerid . "','" . $description . "'"
                . ",'" . $cr . "','" . $dr . "','Dr','" . $billno . "','" . $entrydate . "','" . date('Y/m/d') . "','" . $actype . "','" . $modetype . "','" . $narration . "',0.00,'" . $status . "','" . $username . "','" . $description2 . "','" . $ledgerid2 . "','" . $comid . "','" . $locid . "')");
        $resultdr = mysqli_query($conn, $sqlquery4);
        if ($resultdr) {
            $sqlquery2 = ("INSERT INTO `journaldetails`(`ledgerid`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`,`comid`,`locid`)"
                    . "VALUES ('" . $ledgerid2 . "','" . $description2 . "'"
                    . ",'" . $dr . "','" . $cr . "','Cr','" . $billno . "','" . $entrydate . "','" . date('Y/m/d') . "','" . $actype . "','" . $modetype . "','" . $narration . "',0.00,'" . $status . "','" . $username . "','" . $description . "','" . $ledgerid . "','" . $comid . "','" . $locid . "')");
            mysqli_query($conn, $sqlquery2);
        }


        if ($modetype == 'CQ') {
            $conn = $this->conn;
            $sqlMode = ("INSERT INTO `chequedetails`(`jorunalrefid`, `ledgerid`, `bankname`, `chequeamt`, `chequedate`,`chequeno`, `chequestatus`)"
                    . " VALUES ('" . $billno . "','" . $ledgerid2 . "','" . $bankname . "',$chequeamt,'" . $chequedate . "','" . $chequeno . "','I')");
            $result = mysqli_query($conn, $sqlMode);
            return $result;
        } else {
            return $sqlquery2;
        }
    }

    public function storeJournal($ledgerid, $description, $dr, $cr, $jstatus, $billno, $entrydate, $actype, $modetype, $narration, $status, $username, $description2, $ledgerid2, $bankname, $chequeamt, $chequedate, $chequeno, $comid, $locid) {
        $conn = $this->conn;
        $sqlquery4 = ("INSERT INTO `journaldetails`(`ledgerid`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`,`comid`,`locid`)"
                . "VALUES ('" . $ledgerid . "','" . $description . "'"
                . ",'" . $dr . "','" . $cr . "','" . $jstatus . "','" . $billno . "','" . $entrydate . "','" . date('Y/m/d') . "','" . $actype . "','" . $modetype . "','" . $narration . "',0.00,'" . $status . "','" . $username . "','" . $description2 . "','" . $ledgerid2 . "','" . $comid . "','" . $locid . "')");
        $resultdr = mysqli_query($conn, $sqlquery4);
        if ($resultdr) {
            $sqlquery2 = ("INSERT INTO `journaldetails`(`ledgerid`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`,`comid`,`locid`)"
                    . "VALUES ('" . $ledgerid2 . "','" . $description2 . "'"
                    . ",'" . $cr . "','" . $dr . "','Cr','" . $billno . "','" . $entrydate . "','" . date('Y/m/d') . "','" . $actype . "','" . $modetype . "','" . $narration . "',0.00,'" . $status . "','" . $username . "','" . $description . "','" . $ledgerid . "','" . $comid . "','" . $locid . "')");
            mysqli_query($conn, $sqlquery2);
        }
        if ($modetype == 'CQ') {
            $conn = $this->conn;
            $sqlMode = ("INSERT INTO `chequedetails`(`jorunalrefid`, `ledgerid`, `bankname`, `chequeamt`, `chequedate`,`chequeno`, `chequestatus`)"
                    . " VALUES ('" . $billno . "','" . $ledgerid2 . "','" . $bankname . "',$chequeamt,'" . $chequedate . "','" . $chequeno . "','I')");
            $result = mysqli_query($conn, $sqlMode);
            return $result;
        } else {
            return $sqlquery2;
        }
    }

    //employee
    public function SaveEmpData($emp_firstname, $emp_lastname, $emp_printname, $emp_idtype, $emp_passportic, $emp_nationality,
            $emp_passexpire, $emp_visaexpire, $emp_joindate, $emp_resigndate, $emp_contactno, $emp_contactname,
            $emp_emergencyno, $emp_compid, $emp_locid, $emp_designation, $emp_bankname, $emp_accountname,
            $emp_accountno, $emp_image, $emp_basicsalary, $emp_basicrate, $emp_otrate, $emp_othrsrate, $emp_allowance,
            $emp_currentstatus, $emp_remarks, $emp_active, $emp_epf, $emp_socso, $emp_dob, $emp_monthexpire, $emp_curpermit, $emp_nextpermit) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_employeeinfo`(`emp_firstname`, `emp_lastname`, `emp_printname`, `emp_idtype`, `emp_passportic`, "
                . " `emp_nationality`, `emp_passexpire`, `emp_visaexpire`, `emp_joindate`, `emp_resigndate`,"
                . " `emp_contactno`, `emp_contactname`, `emp_emergencyno`, `emp_compid`, `emp_locid`,"
                . " `emp_designation`, `emp_bankname`, `emp_accountname`, `emp_accountno`,"
                . " `emp_image`, `emp_basicsalary`, `emp_basicrate`, `emp_otrate`, `emp_othrsrate`,"
                . " `emp_allowance`, `emp_currentstatus`, `emp_remarks`, `emp_active`,`emp_epf`,`emp_socso`,`emp_dob`, `emp_monthexpire`, `emp_curpermit`, `emp_nextpermit`) "
                . " VALUES ('" . $emp_firstname . "','" . $emp_lastname . "','" . $emp_printname . "','" . $emp_idtype . "','" . $emp_passportic . "','" . $emp_nationality . "',"
                . " '" . $emp_passexpire . "','" . $emp_visaexpire . "','" . $emp_joindate . "','" . $emp_resigndate . "','" . $emp_contactno . "','" . $emp_contactname . "',"
                . " '" . $emp_emergencyno . "','" . $emp_compid . "','" . $emp_locid . "','" . $emp_designation . "','" . $emp_bankname . "','" . $emp_accountname . "',"
                . " '" . $emp_accountno . "','" . $emp_image . "','" . $emp_basicsalary . "','" . $emp_basicrate . "','" . $emp_otrate . "' ,'" . $emp_othrsrate . "','" . $emp_allowance . "',"
                . " '" . $emp_currentstatus . "','" . $emp_remarks . "','" . $emp_active . "','" . $emp_epf . "','" . $emp_socso . "','" . $emp_dob . "','" . $emp_monthexpire . "','" . $emp_curpermit . "','" . $emp_nextpermit . "')");
        $result1 = mysqli_query($conn, $sqlquery);
        $query2 = ("SELECT max(emp_id) as EmpId FROM `pos_employeeinfo` WHERE 1;");
        $result2 = mysqli_query($conn, $query2);
        $rowData = mysqli_fetch_assoc($result2);
        return $rowData['EmpId'];
    }

    //employee
    public function UpdateEmpData($emp_id, $emp_firstname, $emp_lastname, $emp_printname, $emp_idtype, $emp_passportic, $emp_nationality,
            $emp_passexpire, $emp_visaexpire, $emp_joindate, $emp_resigndate, $emp_contactno,
            $emp_contactname, $emp_emergencyno, $emp_compid, $emp_locid, $emp_designation, $emp_bankname, $emp_accountname, $emp_accountno,
            $emp_image, $emp_basicsalary, $emp_basicrate, $emp_otrate, $emp_othrsrate, $emp_allowance,
            $emp_currentstatus, $emp_remarks, $emp_active, $emp_epf, $emp_socso, $emp_dob, $emp_monthexpire, $emp_curpermit, $emp_nextpermit) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `pos_employeeinfo` SET `emp_firstname`='" . $emp_firstname . "',`emp_lastname`='" . $emp_lastname . "',`emp_printname`='" . $emp_printname . "',"
                . "`emp_idtype`='" . $emp_idtype . "',`emp_passportic`='" . $emp_passportic . "',`emp_nationality`='" . $emp_nationality . "',`emp_passexpire`='" . $emp_passexpire . "',"
                . "`emp_visaexpire`='" . $emp_visaexpire . "',`emp_joindate`='" . $emp_joindate . "',`emp_resigndate`='" . $emp_resigndate . "',`emp_contactno`='" . $emp_contactno . "',"
                . "`emp_contactname`='" . $emp_contactname . "',`emp_emergencyno`='" . $emp_emergencyno . "',`emp_compid`='" . $emp_compid . "',`emp_locid`='" . $emp_locid . "',"
                . "`emp_designation`='" . $emp_designation . "',`emp_bankname`='" . $emp_bankname . "',`emp_accountname`='" . $emp_accountname . "',`emp_accountno`='" . $emp_accountno . "',"
                . "`emp_image`='" . $emp_image . "',`emp_basicsalary`='" . $emp_basicsalary . "',`emp_basicrate`='" . $emp_basicrate . "',`emp_otrate`='" . $emp_otrate . "',"
                . "`emp_othrsrate`='" . $emp_othrsrate . "',`emp_allowance`='" . $emp_allowance . "',`emp_currentstatus`='" . $emp_currentstatus . "',`emp_remarks`='" . $emp_remarks . "',"
                . "`emp_active`='" . $emp_active . "',`emp_epf`='" . $emp_epf . "',`emp_socso`='" . $emp_socso . "',`emp_dob`='" . $emp_dob . "', `emp_monthexpire`='" . $emp_monthexpire . "',"
                . " `emp_curpermit`='" . $emp_curpermit . "', `emp_nextpermit`='" . $emp_nextpermit . "'  WHERE `emp_id`='" . $emp_id . "'");
        $result = mysqli_query($conn, $sqlquery);
        $validCheckQuery = ("SELECT count(*) as Counts FROM `ledgermaster` WHERE ledgerrefId='" . $emp_id . "'  and ledgerType='EMP';");
        $validResults = mysqli_query($conn, $validCheckQuery);
        $row = mysqli_fetch_assoc($validResults);
        $rowitemcount = $row['Counts'];
        if ($rowitemcount == 0) {
            $this->saveEmployeeLedgerData($emp_id);
        } else {
            //return 'Update' .$rowitemcount;
            $this->updateEmployeeLedgerData($emp_id);
        }
        return $result;
    }

    //employee
    public function SelectEmpData() {
        $conn = $this->conn;
        $sqlquery = ("SELECT `emp_id`, `emp_firstname`, `emp_lastname`, `emp_printname`, `emp_idtype`, `emp_passportic`, `emp_nationality`,"
                . " `emp_passexpire`, `emp_visaexpire`, `emp_joindate`, `emp_resigndate`, `emp_contactno`, `emp_contactname`, `emp_emergencyno`,"
                . " `emp_compid`, `emp_locid`, `emp_designation`, `emp_bankname`, `emp_accountname`, `emp_accountno`, `emp_image`, `emp_basicsalary`,"
                . " `emp_basicrate`, `emp_otrate`, `emp_othrsrate`, `emp_allowance`, `emp_currentstatus`, `emp_remarks`, `emp_active`, `emp_created`,`emp_epf`,`emp_socso`,`emp_dob`, `emp_monthexpire`, `emp_curpermit`, `emp_nextpermit`"
                . "  FROM `pos_employeeinfo` WHERE 1");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SelectEmpDataByView() {
        $conn = $this->conn;
        $sqlquery = ("SELECT `emp_id` Id, `emp_firstname` as FirstName, `emp_lastname` as LastName, `emp_printname` as PrintName,`emp_passportic` as Passport,"
                . "`emp_nationality` as National, `emp_passexpire` as PPExpire, `emp_visaexpire` as VisaExpire, `emp_joindate` as JoinDate, `emp_resigndate` as ResignDate,"
                . "loc.plm_name as Location,`emp_currentstatus` as CurStatus,`emp_monthexpire` as MonthExpire,`emp_remarks` as Notes  FROM `pos_employeeinfo` as pe inner JOIN `pos_location_mast` as loc ON pe.emp_locid= loc.plm_id ORDER BY `emp_id` ASC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

//employee
    public function SelectEmpDataByID($emp_id) {
        $conn = $this->conn;
        $sqlquery = ("SELECT `emp_id`, `emp_firstname`, `emp_lastname`, `emp_printname`, `emp_idtype`, `emp_passportic`, `emp_nationality`,"
                . " `emp_passexpire`, `emp_visaexpire`, `emp_joindate`, `emp_resigndate`, `emp_contactno`, `emp_contactname`, `emp_emergencyno`,"
                . " `emp_compid`, `emp_locid`, `emp_designation`, `emp_bankname`, `emp_accountname`, `emp_accountno`,emp_image, `emp_basicsalary`,"
                . " `emp_basicrate`, `emp_otrate`, `emp_othrsrate`, `emp_allowance`, `emp_currentstatus`, `emp_remarks`, `emp_active`, `emp_created`,`emp_epf`,`emp_socso`,`emp_dob`, `emp_monthexpire`, `emp_curpermit`, `emp_nextpermit`"
                . "  FROM `pos_employeeinfo` WHERE `emp_id`='" . $emp_id . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveEmpImage($ImageData, $EmpId) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `pos_employeeinfo` SET `emp_image`='" . $ImageData . "'   WHERE `emp_id`='" . $EmpId . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetEmployeeSalaryHistoryById($EmployeeId) {
        $conn = $this->conn;
        $sqlquery = ("SELECT `pes_id` as LogId,Pee.emp_printname as PrintName, `pes_oldsalary` as OldSalary, `pes_newsalary` as NewSalary, `pes_currenttime` as DateTimes,"
                . "Usr.username as UserName, `pes_remarks` as Remarks FROM `pos_emp_salaryhistory` as Pes INNER JOIN `users` as Usr ON Pes.pes_userid =Usr.id "
                . "INNER JOIN `pos_employeeinfo` as  Pee ON Pee.emp_id=Pes.pes_empid  WHERE Pes.pes_empid='" . $EmployeeId . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveEmployeeSalaryHistory($pes_empid, $pes_oldsalary, $pes_newsalary, $pes_userid, $pes_remarks) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_emp_salaryhistory`( `pes_empid`, `pes_oldsalary`, `pes_newsalary` , `pes_userid`, `pes_remarks`) VALUES "
                . "('" . $pes_empid . "','" . $pes_oldsalary . "','" . $pes_newsalary . "', '" . $pes_userid . "','" . $pes_remarks . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //employee Accounts
    public function saveEmployeeLedgerData($SaveEmpDataId) {
        $conn = $this->conn;
        $sqlCustomer = ("SELECT emp_firstname,emp_lastname FROM `pos_employeeinfo` WHERE emp_id='" . $SaveEmpDataId . "'");
        $resCutomer = mysqli_query($conn, $sqlCustomer);
        $rowCusId = (mysqli_fetch_assoc($resCutomer));
        $rowEmpName = $rowCusId['emp_firstname'] . ' ' . $rowCusId['emp_lastname'];
        $sqlquery = (" INSERT INTO `ledgermaster`(`ledgerrefId`, `ledgerName`, `ledgerparenId`, `ledgergroupId`,`ledgerType`,`ledgeropenDate`, `ledgeropenbal`, `ledgerdrcr`, `ledgerActive`) VALUES"
                . "(" . $SaveEmpDataId . ",'" . $rowEmpName . "',2,7,'EMP','" . date("Y/m/d") . "',0.00,'Dr','Active')");
        //  print_r($rowCusIds);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //employee Accounts
    public function updateEmployeeLedgerData($SaveEmpDataId) {
        $conn = $this->conn;
        $sqlCustomer = ("SELECT emp_firstname,emp_lastname FROM `pos_employeeinfo` WHERE emp_id='" . $SaveEmpDataId . "'");
        $resCutomer = mysqli_query($conn, $sqlCustomer);
        $rowCusId = (mysqli_fetch_assoc($resCutomer));
        $rowEmpName = $rowCusId['emp_firstname'] . ' ' . $rowCusId['emp_lastname'];
        $sqlquery = ("UPDATE `ledgermaster` SET  `ledgerName`='" . $rowEmpName . "'  WHERE `ledgerrefId`='" . $SaveEmpDataId . "' AND `ledgerType`='EMP'");
        //  print_r($rowCusIds);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetMonthofsalary() {
        $conn = $this->conn;
        $sqlquery = ("SELECT `pems_id` as Id, `pems_monthname` as MonthName, CASE `pems_active` WHEN '1' THEN 'Active' WHEN '0' THEN 'InActive' END as Active, `pems_datetime` as Created FROM `pos_emp_month` WHERE 1");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveMonthofsalary($pems_monthname, $pems_active) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_emp_month`( `pems_monthname`, `pems_active`) VALUES ('" . $pems_monthname . "','" . $pems_active . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function UpdateMonthofsalary($pems_id, $pems_monthname, $pems_active) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `pos_emp_month` SET `pems_monthname`='" . $pems_monthname . "',`pems_active`='" . $pems_active . "' WHERE `pems_id`='" . $pems_id . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function CheckMonthofsalary($pemp_month, $pemp_comid, $pemp_locid) {
        $conn = $this->conn;
        $sqlquery = ("SELECT count(*) as Counts FROM `pos_emp_monthprocess` WHERE `pemp_month`='" . $pemp_month . "' AND `pemp_comid`='" . $pemp_comid . "' AND `pemp_locid`='" . $pemp_locid . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetEmpMonthofsalary($pemp_comid, $pemp_locid) {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM pos_employeeinfo as pe WHERE pe.emp_compid='" . $pemp_comid . "' AND pe.emp_locid='" . $pemp_locid . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetEmpOldMonthofsalary($pemp_comid, $pemp_locid, $pem_month) {
        $conn = $this->conn;
        $sqlquery = ("SELECT pem.pemp_id as EmpTrId,pem.pemp_refid as EmpRefId,pe.emp_printname as EmpName,pem.pemp_month as EmpMonth,pem.pemp_comid AS EmpComId, pcm.pcm_name as EmpComName,"
                . "pem.pemp_locid as EmpLocId,plm.plm_name as EmpLocName,pem.pemp_noofdays as EmpNoOfDays,pem.pemp_extradays as EmpExtraDays,pem.pemp_extrahrs as EmpExtraOtHrs,"
                . "pem.pemp_advance as EmpAdvance,pem.`pemp_deduction` as EmpDeduction,pem.`pemp_bankin` as EmpBankIn FROM `pos_emp_monthprocess` as pem  "
                . "INNER JOIN  pos_employeeinfo as pe ON pe.emp_id = pem.pemp_refid "
                . "INNER JOIN pos_company_mast as pcm ON pem.pemp_comid = pcm.pcm_id "
                . "INNER JOIN pos_location_mast as plm ON pem.pemp_locid = plm.plm_id WHERE pem.pemp_comid='" . $pemp_comid . "' AND pem.pemp_locid='" . $pemp_locid . "' AND pem.pemp_month='" . $pem_month . "' AND pe.emp_active=1");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveMonthEmpAttendance($pemp_refid, $pemp_month, $pemp_comid, $pemp_locid, $pemp_noofdays, $pemp_extradays, $pemp_extrahrs, $pemp_advance, $pemp_deduction, $pemp_bankin) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_emp_monthprocess`(`pemp_refid`, `pemp_month`, `pemp_comid`, `pemp_locid`, `pemp_noofdays`, `pemp_extradays`, `pemp_extrahrs`, `pemp_advance`,`pemp_deduction`,`pemp_bankin`)"
                . " VALUES ( '" . $pemp_refid . "','" . $pemp_month . "','" . $pemp_comid . "','" . $pemp_locid . "','" . $pemp_noofdays . "','" . $pemp_extradays . "','" . $pemp_extrahrs . "','" . $pemp_advance . "','" . $pemp_deduction . "','" . $pemp_bankin . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteMonthEmpAttendance($pemp_id) {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `pos_emp_monthprocess` WHERE `pemp_id`='" . $pemp_id . "' ");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function UpdateMonthofProcess($pemp_month, $pemp_comid, $pemp_locid, $pemp_trid, $pemp_refid) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `pos_emp_monthprocess` SET  `pemp_comid`='" . $pemp_comid . "',`pemp_locid`='" . $pemp_locid . "'  WHERE `pemp_id`='" . $pemp_trid . "' AND `pemp_refid`='" . $pemp_refid . "' AND `pemp_month`='" . $pemp_month . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $sqlquery;
    }

    //emp Final Process
    public function SelectFinalProcess($pef_month, $pef_comid, $pef_locid) {
        $conn = $this->conn;
        $sqlquery = ("SELECT pem.pemp_id as EmpTrId,pem.pemp_refid as EmpRefId,pe.emp_printname as EmpName,pem.pemp_month as EmpMonth,pem.pemp_comid AS EmpComId,"
                . "pcm.pcm_name as EmpComName,pem.pemp_locid as EmpLocId,plm.plm_name as EmpLocName,pe.emp_basicsalary as EmpBasic,pem.pemp_noofdays as EmpNoOfDays,"
                . "pe.emp_basicrate as EmpBasicRate,pem.pemp_extradays as EmpExtraDays,pe.emp_otrate as ExtraDayRate,pem.pemp_extrahrs as EmpExtraOtHrs,pe.emp_othrsrate as EmpHrsRate,"
                . "pem.pemp_advance as EmpAdvance,pe.emp_allowance as EmpAllowance,pe.emp_epf as EmpEpf,pe.emp_socso as EmpSocso,pem.`pemp_deduction` as EmpDeduction,pem.`pemp_bankin` as EmpBank"
                . " FROM `pos_emp_monthprocess` as pem "
                . "INNER JOIN  pos_employeeinfo as pe ON pe.emp_id = pem.pemp_refid "
                . "INNER JOIN pos_company_mast as pcm ON pem.pemp_comid = pcm.pcm_id "
                . "INNER JOIN pos_location_mast as plm ON pem.pemp_locid = plm.plm_id"
                . " WHERE pem.pemp_comid='" . $pef_comid . "' AND pem.pemp_locid='" . $pef_locid . "' AND pe.emp_active=1 AND pem.pemp_month='" . $pef_month . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SelectOldFinalProcess($pef_month, $pef_comid, $pef_locid) {
        $conn = $this->conn;
        $sqlquery = ("SELECT pef.`pef_id` as EmpTrId, pef.`pef_refid` as EmpRefId,pe.emp_printname as EmpName,pef.`pef_comid` as EmpComId,pcm.pcm_name as EmpComName, "
                . " pef.`pef_locid` as EmpLocId,plm.plm_name as EmpLocName,pef.`pef_month` as EmpMonth, pef.`pef_basicsalary` as EmpBasic, pef.`pef_workingdays` as EmpNoOfDays,"
                . " pef.`pef_wages` as EmpWages, pef.`pef_extraday` as EmpExtraDays, pef.`pef_extradayamt` as EmpExtraDayAmt, pef.`pef_extrahours` as EmpExtraOtHrs, "
                . " pef.`pef_extrahrsamt` as EmpExtraOtAmt, pef.`pef_allowance` as EmpAllowance, pef.`pef_grossamt` as EmpGrossAmt, pef.`pef_advance` as EmpAdvance, "
                . " pef.`pef_epf` as EmpEpf, pef.`pef_socso` as EmpSocso, pef.`pef_deduction` as EmpDeduction, pef.`pef_netpay` as EmpNetPay, pef.`pef_bank` as EmpBank,"
                . " pef.`pef_netcash` as EmpNetCash FROM `pos_emp_finalprocess` as pef INNER JOIN  pos_employeeinfo as pe ON pe.emp_id = pef.pef_refid "
                . " INNER JOIN pos_company_mast as pcm ON pcm.pcm_id = pef.pef_comid "
                . " INNER JOIN pos_location_mast as plm ON plm.plm_id = pef.pef_locid "
                . " WHERE   pef.`pef_month` ='" . $pef_month . "' AND  pef.`pef_comid`='" . $pef_comid . "' AND  pef.`pef_locid` ='" . $pef_locid . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveFinalProcess($pef_refid, $pef_comid, $pef_locid, $pef_month, $pef_basicsalary, $pef_workingdays, $pef_wages, $pef_extraday, $pef_extradayamt,
            $pef_extrahours, $pef_extrahrsamt, $pef_allowance, $pef_grossamt, $pef_advance, $pef_epf, $pef_socso, $pef_deduction, $pef_netpay, $pef_bank, $pef_netcash) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_emp_finalprocess`(`pef_refid`, `pef_comid`, `pef_locid`, `pef_month`, `pef_basicsalary`,"
                . " `pef_workingdays`, `pef_wages`, `pef_extraday`, `pef_extradayamt`, `pef_extrahours`, `pef_extrahrsamt`, `pef_allowance`,"
                . " `pef_grossamt`, `pef_advance`, `pef_epf`, `pef_socso`, `pef_deduction`, `pef_netpay`, `pef_bank`, `pef_netcash`) "
                . " VALUES ('" . $pef_refid . "','" . $pef_comid . "','" . $pef_locid . "','" . $pef_month . "','" . $pef_basicsalary . "','" . $pef_workingdays . "',"
                . " '" . $pef_wages . "','" . $pef_extraday . "','" . $pef_extradayamt . "','" . $pef_extrahours . "','" . $pef_extrahrsamt . "','" . $pef_allowance . "',"
                . " '" . $pef_grossamt . "','" . $pef_advance . "','" . $pef_epf . "','" . $pef_socso . "','" . $pef_deduction . "','" . $pef_netpay . "',"
                . " '" . $pef_bank . "','" . $pef_netcash . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteFinalProcess($pef_id) {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `pos_emp_finalprocess` WHERE  `pef_id`='" . $pef_id . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function CheckMonthofFinal($pemp_month, $pemp_comid, $pemp_locid) {
        $conn = $this->conn;
        $sqlquery = ("SELECT count(*) as Counts FROM `pos_emp_finalprocess` WHERE `pef_month`='" . $pemp_month . "' AND  `pef_comid`='" . $pemp_comid . "' AND `pef_locid`='" . $pemp_locid . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteMonthofFinal($pemp_month, $pemp_comid, $pemp_locid) {
        $conn = $this->conn;
        $sqlquery = ("DELETE  FROM `pos_emp_finalprocess` WHERE `pef_month`='" . $pemp_month . "' AND  `pef_comid`='" . $pemp_comid . "' AND `pef_locid`='" . $pemp_locid . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //branchaccesslist
    public function SelectBranchRightsList() {
        $conn = $this->conn;
        $sqlquery = ("SELECT `mbid` as Id, `comid` as ComId, mbr.`branchid` as BranchId, `userid` as UserId,phm.ph_name as HmMenu,phm.ph_menucode as HmCode,psm.ps_name as SubMenu,psm.ps_menucode as SmCode,mbr.active as Active FROM `mgmt_branchrights` as mbr INNER JOIN pos_headermenu as phm ON phm.ph_menucode=mbr.hmcode INNER JOIN pos_submenu as psm ON psm.ps_menucode =mbr.smcode;");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveBranchRightsList($comid, $branchid, $userid, $hmcode, $smcode, $active) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `mgmt_branchrights`(`comid`, `branchid`, `userid`,`hmcode`,`smcode`,`active`) VALUES ( '" . $comid . "','" . $branchid . "','" . $userid . "','" . $hmcode . "','" . $smcode . "','" . $active . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteBranchRightsList($UserId) {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `mgmt_branchrights` WHERE `userid`='" . $UserId . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function InsertHeaderMenu($ph_name, $ph_projectid, $ph_active, $ph_menucode) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_headermenu`(`ph_name`, `ph_projectid`, `ph_active`, `ph_menucode`) VALUES ('" . $ph_name . "','" . $ph_projectid . "','" . $ph_active . "','" . $ph_menucode . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function InsertSubMenu($ps_name, $ph_id, $ps_active, $ps_menucode) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_submenu`( `ps_name`, `ph_id`, `ps_active`, `ps_menucode`) VALUES ('" . $ps_name . "','" . $ph_id . "','" . $ps_active . "','" . $ps_menucode . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function UpdateHeaderMenu($phid, $ph_name, $ph_projectid, $ph_active, $ph_menucode) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `pos_headermenu` SET `ph_name`='" . $ph_name . "',`ph_projectid`='" . $ph_projectid . "',`ph_active`='" . $ph_active . "',`ph_menucode`='" . $ph_menucode . "' WHERE `ph_menucode`='" . $ph_menucode . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function UpdateSubMenu($psid, $ps_name, $ph_id, $ps_active, $ps_menucode) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `pos_submenu` SET `ps_name`='" . $ps_name . "',`ph_id`='" . $ph_id . "',`ps_active`='" . $ps_active . "',`ps_menucode`='" . $ps_menucode . "' WHERE `ps_menucode`='" . $ps_menucode . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteHeaderMenu($phid) {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `pos_headermenu` WHERE `phid`='" . $phid . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteSubMenu($psid) {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `pos_submenu` WHERE `psid`='" . $psid . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SelectHeadAndSubMenu() {
        $conn = $this->conn;
        $sqlquery = ("SELECT `phid`, `ph_name`, `ph_projectid`, `ph_active`, `ph_menucode`,`psid`, `ps_name`, `ph_id`, `ps_active`, `ps_menucode` FROM `pos_headermenu` as phm INNER JOIN `pos_submenu` as psm ON phm.ph_menucode=psm.ph_id;");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SelectHeadMenu() {
        $conn = $this->conn;
        $sqlquery = ("SELECT `phid`, `ph_name`, `ph_projectid`, `ph_active`, `ph_menucode`  FROM `pos_headermenu`");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SelectSubMenu() {
        $conn = $this->conn;
        $sqlquery = ("SELECT `psid`, `ps_name`, `ph_id`, `ps_active`, `ps_menucode` FROM `pos_submenu`");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteTruncateMenu() {
        $conn = $this->conn;
        $query1 = "TRUNCATE TABLE pos_headermenu";
        $query2 = "TRUNCATE TABLE pos_submenu";
        $result1 = mysqli_query($conn, $query1);
        $result2 = mysqli_query($conn, $query2);
        return ($result1 && $result2);
    }

    public function ValidCheckHMenu($HMenuCode) {
        $conn = $this->conn;
        $sqlquery = ("SELECT count(*) as Counts FROM `pos_headermenu` WHERE ph_menucode='" . $HMenuCode . "'");
        $result = mysqli_query($conn, $sqlquery);
        while ($row = mysqli_fetch_row($result)) {
            $Maxno = $row[0];
        }
        return $Maxno;
    }

    public function ValidCheckSMenu($SMenuCode) {
        $conn = $this->conn;
        $sqlquery = ("SELECT count(*) as Counts FROM `pos_submenu` WHERE ps_menucode='" . $SMenuCode . "'");
        $result = mysqli_query($conn, $sqlquery);
        while ($row = mysqli_fetch_row($result)) {
            $Maxno = $row[0];
        }
        return $Maxno;
    }

    public function _branchSelectIp($comid) {
        $conn = $this->conn;
        $sqlQuery = ("SELECT dbm.branchid as Id,dbm.branchname as BranchName,dbm.branchcustomerid as CompanyId,cm.customerName as CompanyName,dbm.branchaddress as Address,dbm.branchemail as Email, dbm.branchcontact as Contact,dbm.branchanydesk as AnyDesk,dbm.branchserver as Server,dbm.branchclient as Client,dbm.branchtab as Tab,branchinstalldate as InstallDate, dbm.branchlock as Locks,dbm.branchactivationcode as Activation,dbm.branchstatus as Active,dbm.branchmessage as Message,dbm.branchip as IpAddress,dbm.branchpassword as BrPassword FROM `di_branch_mast` as dbm INNER join `customermaster` as cm on dbm.branchcustomerid=cm.customerId WHERE dbm.branchcustomerid = '" . $comid . "';");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _branchUpdateIp($branchid, $ip) {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `di_branch_mast` SET  `branchip`='" . $ip . "'  WHERE `branchid`='" . $branchid . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    function base64_url_encode($input) {
        return strtr(base64_encode($input), '+ /= ', '-_.');
    }

    function base64_url_decode($input) {
        return base64_decode(strtr($input, '-_.', '+ /= '));
    }
}
