<?php

class funcProcessMgmt
{
    private $conn;

    public function __construct()
    {
        require_once 'dbconnect.php';
        $db = new database();
        $this->conn = $db->connect();
    }


    public function _login($user, $pass)
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT * FROM `users` WHERE  `username`= '" . $user . "' and  `password`= '" . md5($pass) . "' and `status`='1'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function GetUser()
    {
        $conn = $this->conn;
        $sqlSelect = ("SELECT pusr.id AS Id, pusr.username AS UserName, pusr.password AS Password, pu.pug_name AS GroupName, CASE pusr.status WHEN 1 THEN 'Active' WHEN 0 THEN 'InActive' END AS Status, pusr.comid AS ComId, pusr.locid AS LocId, pusr.group_id AS GroupId, pusr.created AS Created, pu.pug_description AS GroupDescription, pu.pug_active AS GroupActive, pu.pug_created_date AS GroupCreatedDate FROM users AS pusr INNER JOIN pos_usergroups AS pu ON pusr.group_id = pu.pug_id;");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function _InsertUser($nl_username, $nl_password, $nl_usergroup, $nl_status, $rid, $comid)
    {
        $conn = $this->conn;
        $sqlSelect = ("INSERT INTO `users`(`username`, `password`,`tum_id`,`status`,`comid`,`rid`) VALUES ('" . $nl_username . "','" . md5($nl_password) . "','" . $nl_usergroup . "','" . $nl_status . "','" . $comid . "','" . $rid . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateUser($nl_userid, $nl_username, $nl_password, $nl_usergroup, $nl_status, $rid, $comid)
    {
        $conn = $this->conn;
        $sqlSelect = ("UPDATE `users` SET `username`='" . $nl_username . "',`password`='" . md5($nl_password) . "',`tum_id`='" . $nl_usergroup . "',`status`='" . $nl_status . "',`comid`='" . $comid . "',`rid`='" . $rid . "' WHERE `id`='" . $nl_userid . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _RptMasterByUser()
    {
        $conn = $this->conn;
        $sqlSelect = ("SELECT namast.id as ID,namast.username as UserName,namast.password as Password,umast.tb_usergroup_name as UserRole,namast.rid as ComId,namast.status as Active FROM `users` as namast INNER JOIN tb_usergroup_master as umast on namast.tum_id=umast.tb_usergroup_id");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function GetComapanyLocation()
    {
        $conn = $this->conn;
        $sqlSelect = ("SELECT pcm_id as COID,pcm_name as CompanyName,plm_id as LID,plm_name as LocationName, pcm_active  as Active FROM pos_company_mast,pos_location_mast WHERE 1;");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function GetComapany()
    {
        $conn = $this->conn;
        $sqlSelect = ("SELECT pcm_id as COID,pcm_name as CompanyName,pcm_active  as Active FROM pos_company_mast WHERE 1;");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function GetLocation()
    {
        $conn = $this->conn;
        $sqlSelect = ("SELECT * FROM `pos_location_mast`");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function SaveCompany($pcm_name, $pcm_active)
    {
        $conn = $this->conn;
        $sqlSelect = ("INSERT INTO `pos_company_mast`(`pcm_name`, `pcm_shortname`, `pcm_sst`, `pcm_active`, `pcm_address`, `pcm_default`)"
            . " VALUES ('" . $pcm_name . "','M','M','" . $pcm_active . "','M','1')");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function UpdateCompany($pcm_id, $pcm_name, $pcm_active)
    {
        $conn = $this->conn;
        $sqlSelect = ("UPDATE `pos_company_mast` SET  `pcm_name`='" . $pcm_name . "' ,`pcm_active`='" . $pcm_active . "'  WHERE  `pcm_id`='" . $pcm_id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function SaveLocation($plm_name, $plm_active)
    {
        $conn = $this->conn;
        $sqlSelect = ("INSERT INTO `pos_location_mast`( `plm_name`, `plm_active`,`plm_default`) VALUES ('" . $plm_name . "','" . $plm_active . "','1')");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function UpdateLocation($plm_id, $plm_name, $plm_active)
    {
        $conn = $this->conn;
        $sqlSelect = ("UPDATE `pos_location_mast` SET `plm_name`='" . $plm_name . "',`plm_active`='" . $plm_active . "' ,`plm_default`='1' WHERE `plm_id`='" . $plm_id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function SaveUser($username, $password, $status, $locid, $group_id, $comid)
    {
        $conn = $this->conn;
        $sqlSelect = ("INSERT INTO `users`(`username`, `password`, `status`, `locid`, `group_id`, `comid`) VALUES ('" . $username . "','" . md5($password) . "',"
            . "'" . $status . "','" . $locid . "','" . $group_id . "','" . $comid . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function UpdateUser($id, $username, $password, $status, $locid, $group_id, $comid)
    {
        $conn = $this->conn;
        $sqlSelect = ("UPDATE `users` SET `username`='" . $username . "',`status`='" . $status . "',"
            . "`locid`='" . $locid . "',`group_id`='" . $group_id . "',`password`='" . md5($password) . "',`comid` = '" . $comid . "' WHERE `id`='" . $id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
        //,`password`='" . md5($password) . "'
    }

    //Tax Master
    public function _SelectTaxMastrer()
    {
        $conn = $this->conn;
        $sqlSelect = ("SELECT `taxid` as TaxId, `taxname` as TaxName, `taxvalue` as TaxValue, `taxstatus` as Active FROM `taxmaster` WHERE 1");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _SelectTaxMastrerById($id)
    {
        $conn = $this->conn;
        $sqlSelect = ("SELECT `taxid`, `taxname`, `taxvalue`, `taxstatus` FROM `taxmaster` WHERE `taxid`='" . $id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return mysqli_fetch_assoc($result);
    }

    public function _InsertTaxMastrer($taxname, $taxvalue, $taxstatus)
    {
        $conn = $this->conn;
        $sqlSelect = ("INSERT INTO `taxmaster`(`taxname`, `taxvalue`, `taxstatus`) VALUES ('" . $taxname . "','" . $taxvalue . "','" . $taxstatus . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateTaxMastrer($taxid, $taxname, $taxvalue, $taxstatus)
    {
        $conn = $this->conn;
        $sqlSelect = ("UPDATE `taxmaster` SET `taxid`='" . $taxid . "',`taxname`='" . $taxname . "',`taxvalue`='" . $taxvalue . "',`taxstatus`='" . $taxstatus . "' WHERE `taxid`='" . $taxid . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    //MainGroup
    public function _SelectMainMastrer()
    {
        $conn = $this->conn;
        $sqlSelect = ("SELECT `mainid` as MainId, `mainname` as MainName, `mainstatus` as Active FROM `di_main_group` WHERE 1");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _SelectMainMastrerById($id)
    {
        $conn = $this->conn;
        $sqlSelect = ("SELECT `mainid` as MainId, `mainname` as MainName, `mainstatus` as Active FROM `di_main_group` WHERE `mainid`='" . $id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return mysqli_fetch_assoc($result);
    }

    public function _InsertMainMastrer($mainname, $mainstatus)
    {
        $conn = $this->conn;
        $sqlSelect = ("INSERT INTO `di_main_group`(`mainname`, `mainstatus`)VALUES ('" . $mainname . "','" . $mainstatus . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateMainMastrer($mainid, $mainname, $mainstatus)
    {
        $conn = $this->conn;
        $sqlSelect = ("UPDATE `di_main_group` SET `mainid`='" . $mainid . "',`mainname`='" . $mainname . "',`mainstatus`='" . $mainstatus . "' WHERE `mainid`='" . $mainid . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    //SubCateGroup
    public function _SelectCateMastrer()
    {
        $conn = $this->conn;
        $sqlSelect = ("SELECT sg.dcm_id as CateId,sg.dcm_name as CateName,mg.mainid as MainId,mg.mainname as MainName,sg.dcm_active as Active FROM `di_category_master` as sg INNER JOIN `di_main_group` as mg ON sg.di_main_id=mg.mainid WHERE 1");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _SelectCateMastrerById($id)
    {
        $conn = $this->conn;
        $sqlSelect = ("SELECT `dcm_id`, `dcm_name`, `di_main_id`, `dcm_active` FROM `di_category_master` WHERE  `dcm_id`='" . $id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return mysqli_fetch_assoc($result);
    }

    public function _InsertCateMastrer($catename, $mainid, $catestatus)
    {
        $conn = $this->conn;
        $sqlSelect = ("INSERT INTO `di_category_master`(`dcm_name`, `di_main_id`, `dcm_active`) VALUES  ('" . $catename . "','" . $mainid . "','" . $catestatus . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateCateMastrer($cateid, $catename, $mainid, $catestatus)
    {
        $conn = $this->conn;
        $sqlSelect = ("UPDATE `di_category_master` SET `dcm_name`='" . $catename . "',`di_main_id`='" . $mainid . "',`dcm_active`='" . $catestatus . "' WHERE `dcm_id`='" . $cateid . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    //Product Master
    public function _tablegroupmasterbycode($groupid)
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT tma_group_id as Id,tma_group_value as Name FROM tb_master_all WHERE tma_group_code='" . $groupid . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _SelectProductJoin()
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT dim.dim_item_id as Id,dim.dim_item_barcode as BarCode,dim_item_name as ItemName,dim.dim_remark as Remarks,dmg.mainname as MainName,dcm.dcm_id as CateId,dcm.dcm_name as CateName,tx.taxname as TaxName,dim.dim_sell_price as SellPrice,dim.dim_cost_price as CostPrice,dim.dim_min_price as MinPrice,dim.dim_max_price as MaxPrice,dim.dim_allow_disc as AllowDiscount,dim.dim_allow_negstock as AllowNegStock,dim.dim_allow_multiprice as AllowMultiPrice,dim.dim_op_stock as OpeningStock,pcm.pcm_name as CompanyName,plm.plm_name as LocationName,dim.dim_status as Active FROM `di_item_mast`as dim INNER JOIN `di_main_group` as dmg ON dim.dim_main_id=dmg.mainid INNER JOIN `di_category_master` as dcm ON dim.dim_cate_id=dcm.dcm_id INNER JOIN `taxmaster` as tx ON dim.dim_tax_id=tx.taxid INNER JOIN `pos_company_mast` as pcm   ON dim.dim_com_id=pcm.pcm_id INNER JOIN `pos_location_mast` as plm ON dim.dim_loc_id=plm.plm_id WHERE 1;");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _InsertProductMaster(
        $dim_item_barcode,
        $dim_item_name,
        $dim_business_type,
        $dim_main_id,
        $dim_cate_id,
        $dim_tax_id,
        $dim_cost_price,
        $dim_sell_price,
        $dim_min_price,
        $dim_max_price,
        $dim_allow_disc,
        $dim_allow_negstock,
        $dim_allow_multiprice,
        $dim_op_stock,
        $dim_com_id,
        $dim_loc_id,
        $dim_status,
        $dim_remark
    ) {
        $conn = $this->conn;
        $sqlQuery = ("INSERT INTO `di_item_mast`( `dim_item_barcode`, `dim_item_name`, `dim_business_type`, `dim_main_id`, `dim_cate_id`, `dim_tax_id`, `dim_cost_price`"
            . ", `dim_sell_price`, `dim_min_price`, `dim_max_price`, `dim_allow_disc`, `dim_allow_negstock`, `dim_allow_multiprice`, `dim_op_stock`, `dim_com_id`, `dim_loc_id`, `dim_status`,`dim_remark`,`created`) VALUES "
            . " ('" . $dim_item_barcode . "','" . $dim_item_name . "','" . $dim_business_type . "','" . $dim_main_id . "','" . $dim_cate_id . "','" . $dim_tax_id . "','" . $dim_cost_price . "'"
            . ",'" . $dim_sell_price . "','" . $dim_min_price . "','" . $dim_max_price . "','" . $dim_allow_disc . "','" . $dim_allow_negstock . "','" . $dim_allow_multiprice . "','" . $dim_op_stock . "','" . $dim_com_id . "','" . $dim_loc_id . "','" . $dim_status . "'"
            . ",'" . $dim_remark . "','" . date('Y/m/d') . "')");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _GetProductCode($dim_item_barcode, $dim_item_name, $dim_com_id, $dim_loc_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT `dim_item_id` FROM `di_item_mast` WHERE `dim_item_name` ='" . $dim_item_name . "' AND `dim_item_barcode` ='" . $dim_item_barcode . "' AND `dim_com_id`='" . $dim_com_id . "' AND  `dim_loc_id`='" . $dim_loc_id . "' ");
        $result = mysqli_query($conn, $sqlQuery);
        $rowProductCode1 = (mysqli_fetch_assoc($result));
        $rowProductCode2 = $rowProductCode1['dim_item_id'];
        return $rowProductCode2;
    }

    public function _InsertLiveStock($dim_item_id, $dim_item_barcode, $dim_cost_price, $dim_sell_price, $dim_op_stock, $dim_stock_cur, $dim_com_id, $dim_loc_id)
    {
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

    public function _UpdateProductMaster(
        $dim_item_id,
        $dim_item_barcode,
        $dim_item_name,
        $dim_business_type,
        $dim_main_id,
        $dim_cate_id,
        $dim_tax_id,
        $dim_cost_price,
        $dim_sell_price,
        $dim_min_price,
        $dim_max_price,
        $dim_allow_disc,
        $dim_allow_negstock,
        $dim_allow_multiprice,
        $dim_op_stock,
        $dim_com_id,
        $dim_loc_id,
        $dim_status,
        $dim_remark
    ) {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `di_item_mast` SET `dim_item_barcode`='" . $dim_item_barcode . "',`dim_item_name`='" . $dim_item_name . "',`dim_business_type`='" . $dim_business_type . "'"
            . ",`dim_main_id`='" . $dim_main_id . "',`dim_cate_id`='" . $dim_cate_id . "',`dim_tax_id`='" . $dim_tax_id . "',`dim_cost_price`='" . $dim_cost_price . "',`dim_sell_price`='" . $dim_sell_price . "'"
            . ",`dim_min_price`='" . $dim_min_price . "',`dim_max_price`='" . $dim_max_price . "',`dim_allow_disc`='" . $dim_allow_disc . "',`dim_allow_negstock`='" . $dim_allow_negstock . "',`dim_allow_multiprice`='" . $dim_allow_multiprice . "'"
            . ",`dim_op_stock`='" . $dim_op_stock . "',`dim_com_id`='" . $dim_com_id . "'"
            . ",`dim_loc_id`='" . $dim_loc_id . "',`dim_status`='" . $dim_status . "',`dim_remark`='" . $dim_remark . "' WHERE `dim_item_id`='" . $dim_item_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    //Company Save
    public function storeCustomerData($txtCustomerName, $txtCustomerPhone, $status)
    {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `customermaster`(`customerName`, `customerPhone`, `customerPointsEarned`, `status`, `created`) VALUES ('" . $txtCustomerName . "', '" . $txtCustomerPhone . "', '0', '" . $status . "', NOW())");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updateCustomerData($id, $txtCustomerName, $txtCustomerPhone, $status)
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `customermaster` SET `customerName`='" . $txtCustomerName . "', `customerPhone`='" . $txtCustomerPhone . "', `status`='" . $status . "' WHERE `customerId`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updateCustomerPoints($id, $pointsToAdd)
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `customermaster` SET `customerPointsEarned` = `customerPointsEarned` + " . $pointsToAdd . " WHERE `customerId`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function deleteCustomer($id)
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `customermaster` SET `status`='0' WHERE `customerId`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectCustomer()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `customerId`, `customerName`, `customerPhone`, `customerPointsEarned`, `status`, `created` FROM `customermaster` WHERE `status`='1' ORDER BY `customerName` ASC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectAllCustomers()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `customerId`, `customerName`, `customerPhone`, `customerPointsEarned`, `status`, `created` FROM `customermaster` WHERE 1 ORDER BY `customerName` ASC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectCustomerById($customerId)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `customerId`, `customerName`, `customerPhone`, `customerPointsEarned`, `status`, `created` FROM `customermaster` WHERE `customerId`='" . $customerId . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function _branchSelect()
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT dbm.branchid as Id,dbm.branchname as BranchName,dbm.branchcustomerid as CompanyId,cm.customerName as CompanyName,dbm.branchaddress as Address,dbm.branchemail as Email, dbm.branchcontact as Contact,dbm.branchanydesk as AnyDesk,dbm.branchserver as Server,dbm.branchclient as Client,dbm.branchtab as Tab,branchinstalldate as InstallDate, dbm.branchlock as Locks,dbm.branchactivationcode as Activation,dbm.branchstatus as Active,dbm.branchmessage as Message FROM `di_branch_mast` as dbm INNER join `customermaster` as cm on dbm.branchcustomerid=cm.customerId WHERE 1;");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _branchselectById($id)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT dbm.branchid as Id,cm.customerName as CompanyName,dbm.branchname as BranchName,dbm.branchaddress as Address,dbm.branchemail as Email,"
            . "dbm.branchcontact as Contact,dbm.branchanydesk as AnyDesk,dbm.branchserver as Server,dbm.branchclient as Client,dbm.branchtab as Tab,branchinstalldate as InstallDate,"
            . "dbm.branchlock as Locks,dbm.branchactivationcode as Activation,dbm.branchstatus as Active,dbm.branchmessage as Message FROM `di_branch_mast` as dbm INNER join `customermaster` as cm on dbm.branchcustomerid=cm.customerId WHERE dbm.branchid='" . $id . "'");
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function _branchSave($branchcustomerid, $branchname, $branchaddress, $branchemail, $branchcontact, $branchanydesk, $branchserver, $branchclient, $branchtab, $branchlock, $branchactivationcode, $branchstatus, $branchmessage, $branchinstalldate)
    {
        $conn = $this->conn;
        $sqlQuery = ("INSERT INTO `di_branch_mast`(`branchcustomerid`, `branchname`, `branchaddress`, `branchemail`, `branchcontact`,`branchanydesk`, `branchserver`, `branchclient`, `branchtab`, `branchlock`, `branchactivationcode`, `branchmessage`,`branchstatus`,`branchinstalldate`) VALUES"
            . "('" . $branchcustomerid . "','" . $branchname . "','" . $branchaddress . "','" . $branchemail . "','" . $branchcontact . "','" . $branchanydesk . "','" . $branchserver . "','" . $branchclient . "','" . $branchtab . "','" . $branchlock . "','" . $branchactivationcode . "','" . $branchmessage . "','" . $branchstatus . "','" . $branchinstalldate . "')");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function storeCustomerLedgerData($customer)
    {
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

    public function _branchUpdate($branchid, $branchcustomerid, $branchname, $branchaddress, $branchemail, $branchcontact, $branchanydesk, $branchserver, $branchclient, $branchtab, $branchlock, $branchactivationcode, $branchstatus, $branchmessage, $branchinstalldate)
    {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `di_branch_mast` SET `branchcustomerid`='" . $branchcustomerid . "', `branchname`='" . $branchname . "', `branchaddress`='" . $branchaddress . "', `branchemail`='" . $branchemail . "', `branchcontact`='" . $branchcontact . "',`branchanydesk`='" . $branchanydesk . "', `branchserver`='" . $branchserver . "', `branchclient`='" . $branchclient . "', `branchtab`='" . $branchtab . "', `branchlock`='" . $branchlock . "', `branchactivationcode`='" . $branchactivationcode . "',`branchmessage`='" . $branchmessage . "', `branchstatus`='" . $branchstatus . "',`branchinstalldate`='" . $branchinstalldate . "' WHERE `branchid`='" . $branchid . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function updateCustomerLedgerData($ledgerrefId, $ledgerName)
    {
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
    public function UnitSave($dum_name, $dum_active)
    {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `di_unit_mast`(`dum_name`, `dum_active`) VALUES ('" . $dum_name . "','" . $dum_active . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //Unit Update
    public function UnitUpdate($dum_id, $dum_name, $dum_active)
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `di_unit_mast` SET  `dum_name`='" . $dum_name . "',`dum_active`='" . $dum_active . "' WHERE `dum_id`='" . $dum_id . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //Select Unit
    public function UnitSelect()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `dum_id` as Id, `dum_name` as UnitName, `dum_active` as Active FROM `di_unit_mast` WHERE 1");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //Supplier Entry
    public function storesupplierData($supplierName, $status)
    {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `suppliermaster`(`supplierName`, `status`) VALUES ('" . $supplierName . "','" . $status . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updatesupplierData($id, $supplierName, $status)
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `suppliermaster` SET `supplierName`='" . $supplierName . "',`status`='" . $status . "' WHERE `supplierId`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectsupplier()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT supplierId as Id,`supplierName` as SupplierName,`status` as Active FROM `suppliermaster`");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectLedgersupplier()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT ledgermaster.ledgerId as Id,`supplierName` as SupplierName,`status` as Active FROM `suppliermaster` INNER JOIN ledgermaster ON suppliermaster.supplierId = ledgermaster.ledgerrefId WHERE ledgermaster.ledgerType='SUP'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function storeSupplierLedgerData($customer)
    {
        $conn = $this->conn;
        $sqlCustomer = ("SELECT supplierId FROM `suppliermaster` WHERE `supplierName`='" . $customer . "'");
        $resCutomer = mysqli_query($conn, $sqlCustomer);
        $rowCusId = (mysqli_fetch_assoc($resCutomer));
        $rowCusIds = $rowCusId;
        $sqlquery = (" INSERT INTO `ledgermaster`(`ledgerrefId`, `ledgerName`, `ledgerparenId`, `ledgergroupId`,`ledgerType`,`ledgeropenDate`, `ledgeropenbal`, `ledgerdrcr`, `ledgerActive`) VALUES"
            . "(" . $rowCusIds['supplierId'] . ",'" . $customer . "',2,3,'SUP'," . date("Y/m/d") . ",0.00,'Cr','Active')");
        //  print_r($rowCusIds);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updateSupplierLedgerData($ledgerrefId, $ledgerName)
    {
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
    //Sales Invoice Bill Update
    public function UpdateSalesTransNo($pm_id, $comid, $locid)
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `POS_MASTER` SET `PM_TRANS_NO` = `PM_TRANS_NO` + 1 WHERE `PM_COMID`='" . $comid . "' AND `PM_LOCID`='" . $locid . "' AND `PM_ID`='" . $pm_id . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }
    public function GetSaleTransNo($pm_id, $comid, $locid)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `PM_TRANS_NO` FROM `POS_MASTER` WHERE `PM_COMID`='" . $comid . "' AND `PM_LOCID`='" . $locid . "' AND `PM_ID`='" . $pm_id . "'");
        $result = mysqli_query($conn, $sqlquery);

        // Return only PM_TRANS_NO value
        if ($row = mysqli_fetch_assoc($result)) {
            return $row['PM_TRANS_NO'];
        } else {
            return null; // or 0, depending on your preference
        }
    }

    //Invoice Update
    public function selectMaxBillNoByType($BillType)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM `invoiceautono` WHERE `autoname`='" . $BillType . "'");
        $result = mysqli_query($conn, $sqlquery);
        while ($row = mysqli_fetch_row($result)) {
            $Maxno = $row[1];
        }
        return $Maxno;
    }

    public function selectMaxBillNo($BillType)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM `invoiceautono` WHERE `autoname`='" . $BillType . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updateVoucherNo($autoname)
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `invoiceautono` SET `autono`=`autono` + 1 WHERE `autoname`='" . $autoname . "'"); //SALES//pay
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }


    //Purchase Save
    public function GetProductList()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT dim.dim_item_id as ITEMCODE,pls.pl_barcode as BARCODE,dim.dim_item_name as ITEMNAME,taxs.taxid as TAXID,taxs.taxname as TAXNAME,taxs.taxvalue as TAXVALUE,
                      pls.pl_cost as COST,pls.pl_sell as SELL,(pls.pl_opstok + pls.pl_stockin - pls.pl_stockout) as LIVESTOCK,pls.pl_comid as COMID,pls.pl_locid as LOCID, dim.dim_remark as Remarks
                      FROM `di_item_mast` as dim
                      INNER JOIN `pos_livestock` as pls ON dim.dim_item_id =pls.pl_itemcode INNER JOIN `taxmaster` AS taxs ON taxs.taxid=dim.dim_tax_id");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetProductListByItemCode($ItemCode)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT dim.dim_item_id as ITEMCODE,pls.pl_barcode as BARCODE,dim.dim_item_name as ITEMNAME,taxs.taxid as TAXID,taxs.taxname as TAXNAME,taxs.taxvalue as TAXVALUE,
                      pls.pl_cost as COST,pls.pl_sell as SELL,(pls.pl_opstok + pls.pl_stockin - pls.pl_stockout) as LIVESTOCK,pls.pl_comid as COMID,pls.pl_locid as LOCID FROM `di_item_mast` as dim
                      INNER JOIN `pos_livestock` as pls ON dim.dim_item_id =pls.pl_itemcode INNER JOIN `taxmaster` AS taxs ON taxs.taxid=dim.dim_tax_id WHERE dim.dim_item_id='" . $ItemCode . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SavePurchaseDataDtl(
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
    ) {
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

    public function UpdatePurchaseDataDtl(
        $ppd_id,
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
    ) {
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

    public function DeletePurchaseDataDtl($ppd_id, $ppd_trno, $ppd_itemcode, $ppd_barcode, $ppd_qty, $ppd_costprice, $ppd_sellprice, $ppd_comid, $ppd_locid)
    {
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

    public function SavePurchaseDataHdr(
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
    ) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_pur_hdr`(`pph_trno`, `pph_refno`, `pph_invdate`, `pph_purdate`, `pph_suppid`, `pph_billdiscper`, `pph_billdiscamt`,"
            . " `pph_netamt`, `pph_paymenttype`, `pph_baloutamt`, `pph_comid`, `pph_locid`,`pph_userid`,`pph_created`)"
            . " VALUES ('" . $pph_trno . "','" . $pph_refno . "','" . $pph_invdate . "','" . $pph_purdate . "','" . $pph_suppid . "','" . $pph_billdiscper . "','" . $pph_billdiscamt . "',"
            . "'" . $pph_netamt . "','" . $pph_paymenttype . "','" . $pph_baloutamt . "','" . $pph_comid . "','" . $pph_locid . "','" . $pph_userid . "','" . date("Y-m-d") . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function deleteInvoiceByHDR($invoice)
    {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `pos_pur_hdr` WHERE pph_trno='" . $invoice . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function deleteJourEntry($id)
    {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `journaldetails` WHERE `refinvoiceno`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //Live Stock
    public function _UpdateLiveStock($dim_item_id, $dim_item_barcode, $dim_cost_price, $dim_sell_price, $dim_stock_cur, $dim_com_id, $dim_loc_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `pos_livestock` SET  `pl_barcode`='" . $dim_item_barcode . "',`pl_cost`='" . $dim_cost_price . "',`pl_sell`='" . $dim_sell_price . "',"
            . "`pl_stockin`= pl_stockin + '" . $dim_stock_cur . "',`pl_livestock`= pl_opstok + pl_stockin - pl_stockout"
            . "WHERE `pl_comid`='" . $dim_com_id . "' AND `pl_locid`='" . $dim_loc_id . "' AND `pl_itemcode`='" . $dim_item_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _UpdateLiveStockSales($dim_item_id, $dim_stock_cur, $dim_com_id, $dim_loc_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `pos_livestock` SET   `pl_stockout`= pl_stockout + '" . $dim_stock_cur . "',`pl_livestock`= pl_opstok + pl_stockin - pl_stockout"
            . " WHERE `pl_comid`='" . $dim_com_id . "' AND `pl_locid`='" . $dim_loc_id . "' AND `pl_itemcode`='" . $dim_item_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _UpdateLiveStockEditSales($dim_item_id, $dim_stock_cur, $dim_com_id, $dim_loc_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `pos_livestock` SET   `pl_stockout`= pl_stockout - '" . $dim_stock_cur . "',`pl_livestock`= pl_opstok + pl_stockin + pl_stockout"
            . " WHERE `pl_comid`='" . $dim_com_id . "' AND `pl_locid`='" . $dim_loc_id . "' AND `pl_itemcode`='" . $dim_item_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _UpdateEditLiveStock($dim_item_id, $dim_item_barcode, $dim_cost_price, $dim_sell_price, $dim_stock_cur, $dim_com_id, $dim_loc_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `pos_livestock` SET  `pl_barcode`='" . $dim_item_barcode . "',`pl_cost`='" . $dim_cost_price . "',`pl_sell`='" . $dim_sell_price . "'"
            . "`pl_stockin`= pl_stockin - '" . $dim_stock_cur . "',`pl_livestock`= pl_livestock - '" . $dim_stock_cur . "',"
            . "WHERE `pl_comid`='" . $dim_com_id . "' AND `pl_locid`='" . $dim_loc_id . "' AND `pl_itemcode`='" . $dim_item_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    //journal Entry

    public function storeJournalPurchaseEntry($ledgerid, $branchid, $vocheramt, $accttype, $modetype, $txtdatepicker, $statuAcct, $userid, $txtnarration, $refinvoiceno)
    {
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

    public function storeJournalSalesEntry($ledgerid, $branchid, $vocheramt, $accttype, $modetype, $txtdatepicker, $statuAcct, $userid, $txtnarration, $refinvoiceno)
    {
        $conn = $this->conn;

        // Log function entry
        if (function_exists('logError')) {
            logError("storeJournalSalesEntry started", "Parameters: ledgerid=$ledgerid, branchid=$branchid, amount=$vocheramt, type=$accttype, mode=$modetype, invoice=$refinvoiceno");
        }

        $sqlQuery1 = ("SELECT `ledgerName` FROM `ledgermaster` WHERE `ledgerid`=1");
        if (function_exists('logError')) {
            logError("SQL Query 1", $sqlQuery1);
        }
        $result1 = mysqli_query($conn, $sqlQuery1);
        if (!$result1) {
            if (function_exists('logError')) {
                logError("SQL Query 1 FAILED", mysqli_error($conn));
            }
            return false;
        }
        $row1 = mysqli_fetch_assoc($result1);
        $ledgerName1 = $row1['ledgerName'];
        if (function_exists('logError')) {
            logError("SQL Query 1 SUCCESS", "Retrieved ledgerName1: $ledgerName1");
        }

        $sqlQuery2 = ("SELECT `ledgerid`,`ledgerName` FROM `ledgermaster` WHERE `ledgerrefid`= 1 AND `ledgerType`='CUS'");
        if (function_exists('logError')) {
            logError("SQL Query 2", $sqlQuery2);
        }
        $result2 = mysqli_query($conn, $sqlQuery2);
        if (!$result2) {
            if (function_exists('logError')) {
                logError("SQL Query 2 FAILED", mysqli_error($conn));
            }
            return false;
        }
        $row2 = mysqli_fetch_assoc($result2);
        $ledgerNameId2 = $row2['ledgerid'];
        $ledgerName2 = $row2['ledgerName'];
        if (function_exists('logError')) {
            logError("SQL Query 2 SUCCESS", "Retrieved ledgerNameId2: $ledgerNameId2, ledgerName2: $ledgerName2");
        }

        $txtvoucherno = $this->selectMaxBillNoByType("PAY");
        if (function_exists('logError')) {
            logError("Generated voucher number", "txtvoucherno: $txtvoucherno");
        }

        $sqlquery1 = ("INSERT INTO `journaldetails`(`ledgerid`,`refinvoiceno`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`)"
            . "VALUES ('" . $ledgerid . "','" . $refinvoiceno . "','" . $ledgerName1 . "'"
            . ",'" . $vocheramt . "',0.00,'Dr',$txtvoucherno,'" . $txtdatepicker . "','" . date('Y-m-d') . "','" . $accttype . "','" . $modetype . "','" . $txtnarration . "',0.00,'" . $statuAcct . "','" . $userid . "','" . $ledgerName2 . "','" . $ledgerNameId2 . "')");
        if (function_exists('logError')) {
            logError("Journal Entry 1 (Debit)", $sqlquery1);
        }
        $result = mysqli_query($conn, $sqlquery1);
        if (!$result) {
            if (function_exists('logError')) {
                logError("Journal Entry 1 FAILED", mysqli_error($conn));
            }
            return false;
        } else {
            if (function_exists('logError')) {
                logError("Journal Entry 1 SUCCESS", "Debit entry created for amount: $vocheramt");
            }
        }

        $sqlquery2 = ("INSERT INTO `journaldetails`(`ledgerid`,`refinvoiceno`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`)"
            . "VALUES ('" . $ledgerNameId2 . "','" . $refinvoiceno . "','" . $ledgerName2 . "'"
            . ",0.00,'" . $vocheramt . "','Cr',$txtvoucherno,'" . $txtdatepicker . "','" . date('Y-m-d') . "','" . $accttype . "','" . $modetype . "','" . $txtnarration . "',0.00,'" . $statuAcct . "','" . $userid . "','" . $ledgerName1 . "','" . $ledgerid . "')");
        if (function_exists('logError')) {
            logError("Journal Entry 2 (Credit)", $sqlquery2);
        }
        $result = mysqli_query($conn, $sqlquery2);
        if (!$result) {
            if (function_exists('logError')) {
                logError("Journal Entry 2 FAILED", mysqli_error($conn));
            }
            return false;
        } else {
            if (function_exists('logError')) {
                logError("Journal Entry 2 SUCCESS", "Credit entry created for amount: $vocheramt");
            }
        }

        if (function_exists('logError')) {
            logError("Updating voucher number", "Calling updateVoucherNo('PAY')");
        }
        $this->updateVoucherNo('PAY');

        if (function_exists('logError')) {
            logError("storeJournalSalesEntry completed", "Successfully created journal entries for invoice: $refinvoiceno, amount: $vocheramt");
        }
        return $result;
    }

    public function storeJournalPaymentEntry($cmbledgername, $txtvoucheramount, $drCrMode, $txtvoucherno, $txtdatepicker, $cmbactype, $cmbmodename, $txtnarration, $cmbbankname, $txtchqdate, $txtchqno, $txtchqamount, $txthidden)
    {
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

    public function selectPurchaseInvoice()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `pph_id` as PM_ID, `pph_trno` as GRNNo, `pph_refno` as BillNo, `pph_invdate` as PurchaseDate, ldg.ledgerName as S_SupplierName,'PI' as StatusPR,0 as GivenTotal,`pph_netamt` as BillAmount, `pph_paymenttype` as PaymentType,`pph_userid` as St_UserID,U.username as St_StaffName FROM `pos_pur_hdr` AS HDR INNER JOIN users AS U ON HDR.pph_userid = U.id INNER JOIN ledgermaster as ldg ON ldg.ledgerId= HDR.pph_suppid WHERE 1");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectPurchaseDtlById($trno)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `ppd_id`, `ppd_trno`, `ppd_sno`, `ppd_itemcode`, `ppd_barcode`, `ppd_serialno`, `ppd_batch`, `ppd_prate`, `ppd_qty`, `ppd_amount`, `ppd_discper`, `ppd_discamt`, `ppd_totalamt`, `ppd_taxid`, `ppd_taxamt`, `ppd_grossamt`, `ppd_roundoff`, `ppd_netamt`, `ppd_expiry`, `ppd_costprice`, `ppd_sellprice`, `ppd_comid`, `ppd_locid`, `ppd_created`, `ppd_modified`,dim.dim_item_name as ppd_itemname FROM `pos_pur_dtl` as dtl INNER JOIN di_item_mast as dim ON dtl.ppd_itemcode = dim.dim_item_id WHERE dtl.ppd_trno = '" . $trno . "' ");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectPurchaseHdrById($trno)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM `pos_pur_hdr` WHERE `pph_trno` = '" . $trno . "' ");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //Sales Process
    public function GetClientInfo()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT ledgermaster.ledgerId as Id,`branchname` as Name,`branchstatus` as Active FROM `di_branch_mast` INNER JOIN ledgermaster ON di_branch_mast.branchid = ledgermaster.ledgerrefId WHERE ledgermaster.ledgerType='CUS'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetPaymodeList()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `pmode_id`,`pmode_name`,`pmode_type` FROM `di_paymode_mast` WHERE  `pmode_status`=1");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function deleteJourEntryBySales($id)
    {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `journaldetails` WHERE `refinvoiceno`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectMaxVoucherNo()
    {
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

    public function SaveSaleDtl(
        $psid_invoice_sno,
        $psid_invoice_salid,
        $psid_invoice_date,
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
        $psid_invoice_dayno
    ) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_sale_invoicedtl`(`psid_invoice_sno`,`psid_invoice_salid`,  `psid_invoice_date`,`psid_invoice_trno`,"
            . "`psid_invoice_description`, `psid_invoice_procode`, `psid_invoice_barcode`, `psid_invoice_serialno`, `psid_invoice_uom`,"
            . "`psid_invoice_proqty`, `psid_invoice_rate`,`psid_invoice_amt`, `psid_invoice_itemdisp`, `psid_invoice_itemdisamt`,"
            . "`psid_invoice_billdisp`,`psid_invoice_billdisamt`, `psid_invoice_totdper`, `psid_invoice_totdamt`, `psid_invoice_gross`,"
            . "`psid_invoice_taxinex`, `psid_invoice_taxvalue`, `psid_invoice_taxamt`, `psid_invoice_netamt`, `psid_invoice_remarks`,"
            . "`psid_invoice_batchno`, `psid_invoice_salesmanid`, `psid_invoice_salemanper`, `psid_invoice_shiftno`, `psid_invoice_dayno`, `psid_invoice_created`)"
            . " VALUES ('" . $psid_invoice_sno . "','" . $psid_invoice_salid . "','" . $psid_invoice_date . "','" . $psid_invoice_trno . "',"
            . "'" . $psid_invoice_description . "','" . $psid_invoice_procode . "','" . $psid_invoice_barcode . "','" . $psid_invoice_serialno . "','" . $psid_invoice_uom . "',"
            . "'" . $psid_invoice_proqty . "','" . $psid_invoice_rate . "','" . $psid_invoice_amt . "','" . $psid_invoice_itemdisp . "','" . $psid_invoice_itemdisamt . "',"
            . "'" . $psid_invoice_billdisp . "','" . $psid_invoice_billdisamt . "','" . $psid_invoice_totdper . "','" . $psid_invoice_totdamt . "','" . $psid_invoice_gross . "',"
            . "'" . $psid_invoice_taxinex . "','" . $psid_invoice_taxvalue . "','" . $psid_invoice_taxamt . "','" . $psid_invoice_netamt . "','" . $psid_invoice_remarks . "',"
            . "'" . $psid_invoice_batchno . "','" . $psid_invoice_salesmanid . "','" . $psid_invoice_salemanper . "','" . $psid_invoice_shiftno . "','" . $psid_invoice_dayno . "',"
            . "'" . date("Y/m/d H:i:s") . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveSaleQuoteDtl(
        $psid_invoice_sno,
        $psid_invoice_salid,
        $psid_invoice_date,
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
        $psid_invoice_dayno
    ) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_quote_invoicedtl`(`psid_invoice_sno`,`psid_invoice_salid`, `psid_invoice_prf`, `psid_invoice_date`,`psid_invoice_trno`,"
            . "`psid_invoice_id`, `psid_invoice_description`, `psid_invoice_procode`, `psid_invoice_barcode`, `psid_invoice_serialno`, `psid_invoice_uom`,"
            . "`psid_invoice_proqty`, `psid_invoice_rate`,`psid_invoice_amt`, `psid_invoice_itemdisp`, `psid_invoice_itemdisamt`,"
            . "`psid_invoice_billdisp`,`psid_invoice_billdisamt`, `psid_invoice_totdper`, `psid_invoice_totdamt`, `psid_invoice_gross`,"
            . "`psid_invoice_taxinex`, `psid_invoice_taxvalue`, `psid_invoice_taxamt`, `psid_invoice_netamt`, `psid_invoice_remarks`,"
            . "`psid_invoice_batchno`, `psid_invoice_salesmanid`, `psid_invoice_salemanper`, `psid_invoice_shiftno`, `psid_invoice_dayno`, `psid_invoice_created`)"
            . " VALUES ('" . $psid_invoice_sno . "','" . $psid_invoice_salid . "','SH','" . $psid_invoice_date . "','" . $psid_invoice_trno . "',"
            . "'" . $psid_invoice_id . "','" . $psid_invoice_description . "','" . $psid_invoice_procode . "','" . $psid_invoice_barcode . "','" . $psid_invoice_serialno . "','" . $psid_invoice_uom . "',"
            . "'" . $psid_invoice_proqty . "','" . $psid_invoice_rate . "','" . $psid_invoice_amt . "','" . $psid_invoice_itemdisp . "','" . $psid_invoice_itemdisamt . "',"
            . "'" . $psid_invoice_billdisp . "','" . $psid_invoice_billdisamt . "','" . $psid_invoice_totdper . "','" . $psid_invoice_totdamt . "','" . $psid_invoice_gross . "',"
            . "'" . $psid_invoice_taxinex . "','" . $psid_invoice_taxvalue . "','" . $psid_invoice_taxamt . "','" . $psid_invoice_netamt . "','" . $psid_invoice_remarks . "',"
            . "'" . $psid_invoice_batchno . "','" . $psid_invoice_salesmanid . "','" . $psid_invoice_salemanper . "','" . $psid_invoice_shiftno . "','" . $psid_invoice_dayno . "',"
            . "'" . date("Y/m/d H:i:s") . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveSaleDtlUpdate(
        $psid_invoice_id,
        $psid_invoice_sno,
        $psid_invoice_salid,
        $psid_invoice_date,
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
        $comid,
        $locid
    ) {
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

    public function SaveSaleDtlQuoteUpdate(
        $psid_invoice_id,
        $psid_invoice_sno,
        $psid_invoice_salid,
        $psid_invoice_date,
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
        $comid,
        $locid
    ) {
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

    public function SaveSaleHdr(
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
        $psih_invoice_dayno
    ) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_sale_invoicehdr`(`psih_invoice_trno`, `psih_invoice_date`, `psih_invoice_prefix`, `psih_invoice_description`,"
            . "`psih_invoice_tqty`, `psih_invoice_tamount`, `psih_invoice_titemdisper`, `psih_invoice_titemdisamt`, `psih_invoice_tbilldiscper`,"
            . "`psih_invoice_tbilldiscamt`, `psih_invoice_totdiscper`, `psih_invoice_totdiscamt`, `psih_invoice_tgrossamt`, `psih_invoice_ttaxamt`,"
            . "`psih_invoice_sercharge`, `psih_invoice_roundoff`, `psih_invoice_tnetamt`, `psih_invoice_saletype`, `psih_invoice_billtype`,"
            . "`psih_invoice_billstatus`, `psih_invoice_paymode`, `psih_invoice_customerid`, `psih_invoice_userid`, `psih_invoice_comid`,"
            . "`psih_invoice_locid`, `psih_invoice_billremarks`, `psih_invoice_advamt`, `psih_invoice_outstanding`, `psih_invoice_givenamt`,"
            . "`psih_invoice_balamt`, `psih_invoice_shiftno`, `psih_invoice_dayno`, `psih_invoice_created`)"
            . " VALUES ('" . $psih_invoice_trno . "','" . $psih_invoice_date . "','" . $psih_invoice_prefix . "','" . $psih_invoice_description . "',"
            . "'" . $psih_invoice_tqty . "','" . $psih_invoice_tamount . "','" . $psih_invoice_titemdisper . "','" . $psih_invoice_titemdisamt . "','" . $psih_invoice_tbilldiscper . "',"
            . "'" . $psih_invoice_tbilldiscamt . "','" . $psih_invoice_totdiscper . "','" . $psih_invoice_totdiscamt . "','" . $psih_invoice_tgrossamt . "','" . $psih_invoice_ttaxamt . "',"
            . "'" . $psih_invoice_sercharge . "','" . $psih_invoice_roundoff . "','" . $psih_invoice_tnetamt . "','" . $psih_invoice_saletype . "','" . $psih_invoice_billtype . "',"
            . "'" . $psih_invoice_billstatus . "','" . $psih_invoice_paymode . "','" . $psih_invoice_customerid . "','" . $psih_invoice_userid . "','" . $psih_invoice_comid . "',"
            . "'" . $psih_invoice_locid . "','" . $psih_invoice_billremarks . "','" . $psih_invoice_advamt . "','" . $psih_invoice_outstanding . "','" . $psih_invoice_givenamt . "',"
            . "'" . $psih_invoice_balamt . "','" . $psih_invoice_shiftno . "','" . $psih_invoice_dayno . "','" . date("Y-m-d H:i:s") . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveSaleQuoteHdr(
        $psih_invoice_pmid,
        $psih_invoice_id,
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
        $psih_invoice_dayno
    ) {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_quote_invoicehdr`(`psih_invoice_pmid`, `psih_invoice_id`, `psih_invoice_trno`, `psih_invoice_date`, `psih_invoice_prefix`, `psih_invoice_description`,"
            . "`psih_invoice_tqty`, `psih_invoice_tamount`, `psih_invoice_titemdisper`, `psih_invoice_titemdisamt`, `psih_invoice_tbilldiscper`,"
            . "`psih_invoice_tbilldiscamt`, `psih_invoice_totdiscper`, `psih_invoice_totdiscamt`, `psih_invoice_tgrossamt`, `psih_invoice_ttaxamt`,"
            . "`psih_invoice_sercharge`, `psih_invoice_roundoff`, `psih_invoice_tnetamt`, `psih_invoice_saletype`, `psih_invoice_billtype`,"
            . "`psih_invoice_billstatus`, `psih_invoice_paymode`, `psih_invoice_customerid`, `psih_invoice_userid`, `psih_invoice_comid`,"
            . "`psih_invoice_locid`, `psih_invoice_billremarks`, `psih_invoice_advamt`, `psih_invoice_outstanding`, `psih_invoice_givenamt`,"
            . "`psih_invoice_balamt`, `psih_invoice_shiftno`, `psih_invoice_dayno`, `psih_invoice_created`)"
            . " VALUES ('" . $psih_invoice_pmid . "','" . $psih_invoice_id . "','" . $psih_invoice_trno . "','" . $psih_invoice_date . "','" . $psih_invoice_prefix . "','" . $psih_invoice_description . "',"
            . "'" . $psih_invoice_tqty . "','" . $psih_invoice_tamount . "','" . $psih_invoice_titemdisper . "','" . $psih_invoice_titemdisamt . "','" . $psih_invoice_tbilldiscper . "',"
            . "'" . $psih_invoice_tbilldiscamt . "','" . $psih_invoice_totdiscper . "','" . $psih_invoice_totdiscamt . "','" . $psih_invoice_tgrossamt . "','" . $psih_invoice_ttaxamt . "',"
            . "'" . $psih_invoice_sercharge . "','" . $psih_invoice_roundoff . "','" . $psih_invoice_tnetamt . "','" . $psih_invoice_saletype . "','" . $psih_invoice_billtype . "',"
            . "'" . $psih_invoice_billstatus . "','" . $psih_invoice_paymode . "','" . $psih_invoice_customerid . "','" . $psih_invoice_userid . "','" . $psih_invoice_comid . "',"
            . "'" . $psih_invoice_locid . "','" . $psih_invoice_billremarks . "','" . $psih_invoice_advamt . "','" . $psih_invoice_outstanding . "','" . $psih_invoice_givenamt . "',"
            . "'" . $psih_invoice_balamt . "','" . $psih_invoice_shiftno . "','" . $psih_invoice_dayno . "','" . date("Y-m-d H:i:s") . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveSaleUpdate(
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
    ) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `pos_sale_invoicehdr` SET `psih_invoice_pmid`='" . $psih_invoice_pmid . "',`psih_invoice_id`='" . $psih_invoice_id . "',`psih_invoice_date`='" . $psih_invoice_date . "',`psih_invoice_description`='" . $psih_invoice_description . "',"
            . "`psih_invoice_prefix`='" . $psih_invoice_prefix . "',`psih_invoice_tqty`='" . $psih_invoice_tqty . "',`psih_invoice_tamount`='" . $psih_invoice_tamount . "',"
            . "`psih_invoice_titemdisper`='" . $psih_invoice_titemdisper . "',`psih_invoice_titemdisamt`='" . $psih_invoice_titemdisamt . "',"
            . "`psih_invoice_tbilldiscper`='" . $psih_invoice_tbilldiscper . "',`psih_invoice_tbilldiscamt`='" . $psih_invoice_tbilldiscamt . "',"
            . "`psih_invoice_totdiscper`='" . $psih_invoice_totdiscper . "',`psih_invoice_totdiscamt`='" . $psih_invoice_totdiscamt . "',`psih_invoice_tgrossamt`='" . $psih_invoice_tgrossamt . "',"
            . "`psih_invoice_ttaxamt`='" . $psih_invoice_ttaxamt . "',`psih_invoice_sercharge`='" . $psih_invoice_sercharge . "',`psih_invoice_roundoff`='" . $psih_invoice_roundoff . "',"
            . "`psih_invoice_tnetamt`='" . $psih_invoice_tnetamt . "',`psih_invoice_saletype`='" . $psih_invoice_saletype . "',`psih_invoice_billtype`='" . $psih_invoice_billtype . "',"
            . "`psih_invoice_billstatus`='" . $psih_invoice_billstatus . "',`psih_invoice_customerid`='" . $psih_invoice_customerid . "',`psih_invoice_userid`='" . $psih_invoice_userid . "',"
            . "`psih_invoice_comid`='" . $psih_invoice_comid . "',`psih_invoice_locid`='" . $psih_invoice_locid . "',`psih_invoice_billremarks`='" . $psih_invoice_billremarks . "',"
            . "`psih_invoice_advamt`='" . $psih_invoice_advamt . "',`psih_invoice_outstanding`='" . $psih_invoice_outstanding . "',`psih_invoice_givenamt`='" . $psih_invoice_givenamt . "',"
            . "`psih_invoice_balamt`='" . $psih_invoice_balamt . "',`psih_invoice_shiftno`='" . $psih_invoice_shiftno . "',`psih_invoice_dayno`='" . $psih_invoice_dayno . "' WHERE `psih_invoice_trno`='" . $psih_invoice_trno . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveSaleQuoteUpdate(
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
    ) {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `pos_quote_invoicehdr` SET `psih_invoice_pmid`='" . $psih_invoice_pmid . "',`psih_invoice_id`='" . $psih_invoice_id . "',`psih_invoice_date`='" . $psih_invoice_date . "',`psih_invoice_description`='" . $psih_invoice_description . "',"
            . "`psih_invoice_prefix`='" . $psih_invoice_prefix . "',`psih_invoice_tqty`='" . $psih_invoice_tqty . "',`psih_invoice_tamount`='" . $psih_invoice_tamount . "',"
            . "`psih_invoice_titemdisper`='" . $psih_invoice_titemdisper . "',`psih_invoice_titemdisamt`='" . $psih_invoice_titemdisamt . "',"
            . "`psih_invoice_tbilldiscper`='" . $psih_invoice_tbilldiscper . "',`psih_invoice_tbilldiscamt`='" . $psih_invoice_tbilldiscamt . "',"
            . "`psih_invoice_totdiscper`='" . $psih_invoice_totdiscper . "',`psih_invoice_totdiscamt`='" . $psih_invoice_totdiscamt . "',`psih_invoice_tgrossamt`='" . $psih_invoice_tgrossamt . "',"
            . "`psih_invoice_ttaxamt`='" . $psih_invoice_ttaxamt . "',`psih_invoice_sercharge`='" . $psih_invoice_sercharge . "',`psih_invoice_roundoff`='" . $psih_invoice_roundoff . "',"
            . "`psih_invoice_tnetamt`='" . $psih_invoice_tnetamt . "',`psih_invoice_saletype`='" . $psih_invoice_saletype . "',`psih_invoice_billtype`='" . $psih_invoice_billtype . "',"
            . "`psih_invoice_billstatus`='" . $psih_invoice_billstatus . "',`psih_invoice_customerid`='" . $psih_invoice_customerid . "',`psih_invoice_userid`='" . $psih_invoice_userid . "',"
            . "`psih_invoice_comid`='" . $psih_invoice_comid . "',`psih_invoice_locid`='" . $psih_invoice_locid . "',`psih_invoice_billremarks`='" . $psih_invoice_billremarks . "',"
            . "`psih_invoice_advamt`='" . $psih_invoice_advamt . "',`psih_invoice_outstanding`='" . $psih_invoice_outstanding . "',`psih_invoice_givenamt`='" . $psih_invoice_givenamt . "',"
            . "`psih_invoice_balamt`='" . $psih_invoice_balamt . "',`psih_invoice_shiftno`='" . $psih_invoice_shiftno . "',`psih_invoice_dayno`='" . $psih_invoice_dayno . "' WHERE `psih_invoice_trno`='" . $psih_invoice_trno . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesBill($date)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `psih_invoice_id` as Sal_ID, `psih_invoice_trno` as Sal_BillNo, `psih_invoice_date` as Sal_Date,"
            . " `psih_invoice_description` as Customer,`psih_invoice_tqty` as Sal_Qty,"
            . "  `psih_invoice_tgrossamt` as Sal_TotAmt,  `psih_invoice_tnetamt` as Sal_NetAmt,"
            . " `psih_invoice_billtype` as PaymentType,psih_invoice_comid as COMID,psih_invoice_locid as LOCID    FROM `pos_sale_invoicehdr` WHERE  `psih_invoice_date`='" . $date . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesQuoteBill($date)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `psih_invoice_id` as Sal_ID, `psih_invoice_trno` as Sal_BillNo, `psih_invoice_date` as Sal_Date,"
            . " `psih_invoice_description` as Customer,`psih_invoice_tqty` as Sal_Qty,"
            . "  `psih_invoice_tgrossamt` as Sal_TotAmt,  `psih_invoice_tnetamt` as Sal_NetAmt,"
            . " `psih_invoice_billtype` as PaymentType , psih_invoice_comid as COMID,psih_invoice_locid as LOCID   FROM `pos_quote_invoicehdr` WHERE  `psih_invoice_date`='" . $date . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesBySalID_HDR($Sal_ID)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT *  FROM `pos_sale_invoicehdr` WHERE  `psih_invoice_id`='" . $Sal_ID . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesBySalID_DTL($Sal_ID)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT *  FROM `pos_sale_invoicedtl` WHERE  `psid_invoice_salid`='" . $Sal_ID . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesByQuoteSalID_HDR($Sal_ID)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT *  FROM `pos_quote_invoicehdr` WHERE  `psih_invoice_id`='" . $Sal_ID . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesByQuoteSalID_DTL($Sal_ID)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT *  FROM `pos_quote_invoicedtl` WHERE  `psid_invoice_salid`='" . $Sal_ID . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesId($billno)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `psih_invoice_id` FROM `pos_sale_invoicehdr` WHERE `psih_invoice_trno`='" . $billno . "'");
        $result = mysqli_query($conn, $sqlquery);
        while ($row = mysqli_fetch_row($result)) {
            $Maxno = $row[0];
        }
        return $Maxno;
    }

    public function GetSalesQuoteId($billno)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `psih_invoice_id` FROM `pos_quote_invoicehdr` WHERE `psih_invoice_trno`='" . $billno . "'");
        $result = mysqli_query($conn, $sqlquery);
        while ($row = mysqli_fetch_row($result)) {
            $Maxno = $row[0];
        }
        return $Maxno;
    }

    public function selectMaxInvoice()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `autono` FROM `invoiceautono` WHERE `autoname`='SAL'");
        $result = mysqli_query($conn, $sqlquery);
        while ($row = mysqli_fetch_row($result)) {
            $Maxno = $row[0];
        }
        return $Maxno;
    }

    public function UpdateInvoiceNo()
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `invoiceautono` SET `autono`= `autono` + 1  WHERE `autoname`='SAL'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function UpdateQuoteNo()
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `invoiceautono` SET `autono`= `autono` + 1  WHERE `autoname`='QUO'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteBySalID_DTL($Sal_ID)
    {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `pos_sale_invoicedtl` WHERE  `psid_invoice_salid`='" . $Sal_ID . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesByBillno_HDR($billno)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT *  FROM `pos_sale_invoicehdr` WHERE  `psih_invoice_trno`='" . $billno . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesByBillno_DTL($billno)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT *  FROM `pos_sale_invoicedtl` WHERE  `psid_invoice_trno`='" . $billno . "' ORDER BY `psid_invoice_id` ASC ");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesByQuoteBillno_HDR($billno)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT *  FROM `pos_quote_invoicehdr` WHERE  `psih_invoice_trno`='" . $billno . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetSalesByQuoteBillno_DTL($billno)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT *  FROM `pos_quote_invoicedtl` WHERE  `psid_invoice_trno`='" . $billno . "' ORDER BY `psid_invoice_id` ASC ");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //Ledger Group Entry
    public function storegroupData($groupName)
    {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `groupmaster`(`groupName`) VALUES ('" . $groupName . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updategroupData($id, $groupName)
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `groupmaster` SET `groupName`='" . $groupName . "' WHERE `groupId`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectgroup()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT groupid as Id,groupname as Name FROM `groupmaster`");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectgroupById($id)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM `groupmaster` WHERE `groupId`=" . $id);
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return mysqli_fetch_assoc($result);
    }

    //Parent Entry
    public function storeparentData($parentName)
    {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `parentmaster`(`parentName`) VALUES ('" . $parentName . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updateparentData($id, $parentName)
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `parentmaster` SET `parentName`='" . $parentName . "' WHERE `parentId`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectparent()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `parentId` as Id,`parentName` as Name FROM `parentmaster`");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectparentById($id)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM `parentmaster` WHERE `parentId`=" . $id);
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return mysqli_fetch_assoc($result);
    }

    //Ledger Entry
    public function storeLedgerData($ledgerrefId, $ledgerName, $ledgerparenId, $ledgergroupId, $ledgerType, $ledgeropenDate, $ledgeropenbal, $ledgerdrcr, $ledgerActive)
    {
        $conn = $this->conn;
        $sqlquery = (" INSERT INTO `ledgermaster`(`ledgerrefId`, `ledgerName`, `ledgerparenId`, `ledgergroupId`,`ledgerType`, `ledgeropenDate`, `ledgeropenbal`, `ledgerdrcr`, `ledgerActive`) VALUES"
            . "(" . $ledgerrefId . ",'" . $ledgerName . "'," . $ledgerparenId . "," . $ledgergroupId . ",'" . $ledgerType . "','" . $ledgeropenDate . "'," . $ledgeropenbal . ",'" . $ledgerdrcr . "','" . $ledgerActive . "')");
        //return $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updateLedgerData($ledgerId, $ledgerrefId, $ledgerName, $ledgerparenId, $ledgergroupId, $ledgerType, $ledgeropenDate, $ledgeropenbal, $ledgerdrcr, $ledgerActive)
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `ledgermaster` SET `ledgerrefId`=" . $ledgerrefId . ""
            . ",`ledgerName`='" . $ledgerName . "',`ledgerparenId`=" . $ledgerparenId . ",`ledgergroupId`=" . $ledgergroupId . ",`ledgerType`='" . $ledgerType . "'"
            . ",`ledgeropenDate`='" . $ledgeropenDate . "',`ledgeropenbal`=" . $ledgeropenbal . ",`ledgerdrcr`='" . $ledgerdrcr . "'"
            . ",`ledgerActive`='" . $ledgerActive . "' WHERE `ledgerId`=" . $ledgerId . "");
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectledger()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT lg.ledgerId as Id,lg.ledgerName as LedgerName,gpm.groupName as GroupName,pm.parentName as ParentName,lg.ledgerdrcr as DrCr, case lg.ledgerActive when 'Active' then '1' when 'InActive' then '0' end as Active,lg.ledgerType as LedgerType,lg.ledgeropenDate as OpenDate,lg.ledgeropenbal as OpeningBalance FROM `ledgermaster` lg INNER JOIN `groupmaster` gpm ON lg.ledgergroupId=gpm.groupId INNER JOIN `parentmaster` pm ON lg.ledgerparenId=pm.parentID WHERE 1 ORDER BY ledgerName ASC;");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectledgerall()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM `ledgermaster` WHERE `ledgergroupId` <> 1 AND `ledgerActive` ='Active' ORDER BY ledgerName ASC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectledgerById($id)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM `ledgermaster` INNER JOIN  parentmaster ON parentmaster.`parentID`=ledgermaster.`ledgerparenId` INNER JOIN groupmaster ON groupmaster.`groupId`=ledgermaster.`ledgergroupId` WHERE ledgermaster.`ledgerId`=" . $id);
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function bankList()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT lg.ledgerId,lg.ledgerName,gpm.groupName,pm.parentName,lg.ledgerdrcr,lg.ledgerActive,lg.ledgerType as LedgerType FROM `ledgermaster` lg INNER JOIN `groupmaster` gpm ON lg.ledgergroupId=gpm.groupId INNER JOIN `parentmaster` pm ON lg.ledgerparenId=pm.parentID WHERE lg.ledgerActive='Active' ORDER BY ledgerName ASC");
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteJournalEntry($billno)
    {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `journaldetails` WHERE `billno`=" . $billno); //SALES//pay
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GETJournalEntry($date)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `JOUR_ENTRY`.`description` as DESCRIP, `JOUR_ENTRY`.`dr` AS DR, `JOUR_ENTRY`.`cr` CR,`HEAD_MASTER`.`ledgerName` AS HEAD_NAME,`JOUR_ENTRY`.`entrydate` AS ENTRY_DATE,`JOUR_ENTRY`.`billno` AS BillNo, `modifydate`,  `JOUR_ENTRY`.`modetype` as PayMode,  `JOUR_ENTRY`.`actype`  as JModeStatus,`JOUR_ENTRY`.`jid` FROM `journaldetails` AS JOUR_ENTRY INNER JOIN `ledgermaster` as HEAD_MASTER ON `JOUR_ENTRY`.`ledgerid`=`HEAD_MASTER`.`ledgerId` WHERE `JOUR_ENTRY`.`entrydate`='" . $date . "' AND `JOUR_ENTRY`.`actype` IN ('PAY','REC','JUR')"); //SALES//pay
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GETJournalEntryByBillNo($billno)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `HEAD_MASTER`.`ledgerName` AS HEAD_NAME, `JOUR_ENTRY`.`description` as DESCRIP,CASE WHEN `JOUR_ENTRY`.`dr`=0.00 THEN NULL ELSE `JOUR_ENTRY`.`dr` END AS Debit, CASE WHEN `JOUR_ENTRY`.`cr`=0.00 THEN NULL ELSE `JOUR_ENTRY`.`cr` END AS Credit,`JOUR_ENTRY`.`entrydate` AS ENTRY_DATE,'0.00' AS Openingbalance,'0.00' AS Closingbalance, `HEAD_MASTER`.`ledgerId` AS HEAD_ID FROM `journaldetails` AS JOUR_ENTRY INNER JOIN `ledgermaster` as HEAD_MASTER ON `JOUR_ENTRY`.`ledgerid`=`HEAD_MASTER`.`ledgerId` WHERE `JOUR_ENTRY`.`billno`='" . $billno . "'"); //SALES//pay
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetLedgerAll()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT lg.ledgerId AS HEAD_ID,lg.ledgerName AS HEAD_NAME FROM `ledgermaster` lg INNER JOIN `groupmaster` gpm ON lg.ledgergroupId=gpm.groupId INNER JOIN `parentmaster` pm ON lg.ledgerparenId=pm.parentID WHERE lg.ledgerActive='Active'  ORDER BY ledgerName ASC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GETJournalEntryByID($fromdate, $todate, $ledgerid)
    {
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

    public function GetClsBalance($ledgerId)
    {
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

    public function storeJournalpayments($ledgerid, $description, $dr, $cr, $jstatus, $billno, $entrydate, $actype, $modetype, $narration, $status, $username, $description2, $ledgerid2, $bankname, $chequeamt, $chequedate, $chequeno, $comid, $locid)
    {
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
        } else {
            return $sqlquery2;
        }
    }

    public function storeJournalReceipt($ledgerid, $description, $dr, $cr, $jstatus, $billno, $entrydate, $actype, $modetype, $narration, $status, $username, $description2, $ledgerid2, $bankname, $chequeamt, $chequedate, $chequeno, $comid, $locid)
    {
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

    public function storeJournal($ledgerid, $description, $dr, $cr, $jstatus, $billno, $entrydate, $actype, $modetype, $narration, $status, $username, $description2, $ledgerid2, $bankname, $chequeamt, $chequedate, $chequeno, $comid, $locid)
    {
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
    public function SaveEmpData(
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
    ) {
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
    public function UpdateEmpData(
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
    ) {
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
    public function SelectEmpData()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `emp_id`, `emp_firstname`, `emp_lastname`, `emp_printname`, `emp_idtype`, `emp_passportic`, `emp_nationality`,"
            . " `emp_passexpire`, `emp_visaexpire`, `emp_joindate`, `emp_resigndate`, `emp_contactno`, `emp_contactname`, `emp_emergencyno`,"
            . " `emp_compid`, `emp_locid`, `emp_designation`, `emp_bankname`, `emp_accountname`, `emp_accountno`, `emp_image`, `emp_basicsalary`,"
            . " `emp_basicrate`, `emp_otrate`, `emp_othrsrate`, `emp_allowance`, `emp_currentstatus`, `emp_remarks`, `emp_active`, `emp_created`,`emp_epf`,`emp_socso`,`emp_dob`, `emp_monthexpire`, `emp_curpermit`, `emp_nextpermit`"
            . "  FROM `pos_employeeinfo` WHERE 1");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SelectEmpDataByView()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `emp_id` Id, `emp_firstname` as FirstName, `emp_lastname` as LastName, `emp_printname` as PrintName,`emp_passportic` as Passport,"
            . "`emp_nationality` as National, `emp_passexpire` as PPExpire, `emp_visaexpire` as VisaExpire, `emp_joindate` as JoinDate, `emp_resigndate` as ResignDate,"
            . "loc.plm_name as Location,`emp_currentstatus` as CurStatus,`emp_monthexpire` as MonthExpire,`emp_remarks` as Notes  FROM `pos_employeeinfo` as pe inner JOIN `pos_location_mast` as loc ON pe.emp_locid= loc.plm_id ORDER BY `emp_id` ASC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //employee
    public function SelectEmpDataByID($emp_id)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `emp_id`, `emp_firstname`, `emp_lastname`, `emp_printname`, `emp_idtype`, `emp_passportic`, `emp_nationality`,"
            . " `emp_passexpire`, `emp_visaexpire`, `emp_joindate`, `emp_resigndate`, `emp_contactno`, `emp_contactname`, `emp_emergencyno`,"
            . " `emp_compid`, `emp_locid`, `emp_designation`, `emp_bankname`, `emp_accountname`, `emp_accountno`,emp_image, `emp_basicsalary`,"
            . " `emp_basicrate`, `emp_otrate`, `emp_othrsrate`, `emp_allowance`, `emp_currentstatus`, `emp_remarks`, `emp_active`, `emp_created`,`emp_epf`,`emp_socso`,`emp_dob`, `emp_monthexpire`, `emp_curpermit`, `emp_nextpermit`"
            . "  FROM `pos_employeeinfo` WHERE `emp_id`='" . $emp_id . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveEmpImage($ImageData, $EmpId)
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `pos_employeeinfo` SET `emp_image`='" . $ImageData . "'   WHERE `emp_id`='" . $EmpId . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetEmployeeSalaryHistoryById($EmployeeId)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `pes_id` as LogId,Pee.emp_printname as PrintName, `pes_oldsalary` as OldSalary, `pes_newsalary` as NewSalary, `pes_currenttime` as DateTimes,"
            . "Usr.username as UserName, `pes_remarks` as Remarks FROM `pos_emp_salaryhistory` as Pes INNER JOIN `users` as Usr ON Pes.pes_userid =Usr.id "
            . "INNER JOIN `pos_employeeinfo` as  Pee ON Pee.emp_id=Pes.pes_empid  WHERE Pes.pes_empid='" . $EmployeeId . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveEmployeeSalaryHistory($pes_empid, $pes_oldsalary, $pes_newsalary, $pes_userid, $pes_remarks)
    {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_emp_salaryhistory`( `pes_empid`, `pes_oldsalary`, `pes_newsalary` , `pes_userid`, `pes_remarks`) VALUES "
            . "('" . $pes_empid . "','" . $pes_oldsalary . "','" . $pes_newsalary . "', '" . $pes_userid . "','" . $pes_remarks . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //employee Accounts
    public function saveEmployeeLedgerData($SaveEmpDataId)
    {
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
    public function updateEmployeeLedgerData($SaveEmpDataId)
    {
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

    public function GetMonthofsalary()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `pems_id` as Id, `pems_monthname` as MonthName, CASE `pems_active` WHEN '1' THEN 'Active' WHEN '0' THEN 'InActive' END as Active, `pems_datetime` as Created FROM `pos_emp_month` WHERE 1");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveMonthofsalary($pems_monthname, $pems_active)
    {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_emp_month`( `pems_monthname`, `pems_active`) VALUES ('" . $pems_monthname . "','" . $pems_active . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function UpdateMonthofsalary($pems_id, $pems_monthname, $pems_active)
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `pos_emp_month` SET `pems_monthname`='" . $pems_monthname . "',`pems_active`='" . $pems_active . "' WHERE `pems_id`='" . $pems_id . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function CheckMonthofsalary($pemp_month, $pemp_comid, $pemp_locid)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT count(*) as Counts FROM `pos_emp_monthprocess` WHERE `pemp_month`='" . $pemp_month . "' AND `pemp_comid`='" . $pemp_comid . "' AND `pemp_locid`='" . $pemp_locid . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetEmpMonthofsalary($pemp_comid, $pemp_locid)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT * FROM pos_employeeinfo as pe WHERE pe.emp_compid='" . $pemp_comid . "' AND pe.emp_locid='" . $pemp_locid . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function GetEmpOldMonthofsalary($pemp_comid, $pemp_locid, $pem_month)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT pem.pemp_id as EmpTrId,pem.pemp_refid as EmpRefId,pe.emp_printname as EmpName,pem.pemp_month as EmpMonth,pem.pemp_comid AS EmpComId, "
            . "pcm.pcm_name as EmpComName,pem.pemp_locid as EmpLocId,plm.plm_name as EmpLocName,pem.pemp_noofdays as EmpNoOfDays,pem.pemp_extradays as EmpExtraDays,pem.pemp_extrahrs as EmpExtraOtHrs,"
            . "pem.pemp_advance as EmpAdvance,pem.`pemp_deduction` as EmpDeduction,pem.`pemp_bankin` as EmpBankIn FROM `pos_emp_monthprocess` as pem  "
            . "INNER JOIN  pos_employeeinfo as pe ON pe.emp_id = pem.pemp_refid "
            . "INNER JOIN pos_company_mast as pcm ON pem.pemp_comid = pcm.pcm_id "
            . "INNER JOIN pos_location_mast as plm ON pem.pemp_locid = plm.plm_id WHERE pem.pemp_comid='" . $pemp_comid . "' AND pem.pemp_locid='" . $pemp_locid . "' AND pem.pemp_month='" . $pem_month . "' AND pe.emp_active=1");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveMonthEmpAttendance($pemp_refid, $pemp_month, $pemp_comid, $pemp_locid, $pemp_noofdays, $pemp_extradays, $pemp_extrahrs, $pemp_advance, $pemp_deduction, $pemp_bankin)
    {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_emp_monthprocess`(`pemp_refid`, `pemp_month`, `pemp_comid`, `pemp_locid`, `pemp_noofdays`, `pemp_extradays`, `pemp_extrahrs`, `pemp_advance`,`pemp_deduction`,`pemp_bankin`)"
            . " VALUES ( '" . $pemp_refid . "','" . $pemp_month . "','" . $pemp_comid . "','" . $pemp_locid . "','" . $pemp_noofdays . "','" . $pemp_extradays . "','" . $pemp_extrahrs . "','" . $pemp_advance . "','" . $pemp_deduction . "','" . $pemp_bankin . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteMonthEmpAttendance($pemp_id)
    {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `pos_emp_monthprocess` WHERE `pemp_id`='" . $pemp_id . "' ");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function UpdateMonthofProcess($pemp_month, $pemp_comid, $pemp_locid, $pemp_trid, $pemp_refid)
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `pos_emp_monthprocess` SET  `pemp_comid`='" . $pemp_comid . "',`pemp_locid`='" . $pemp_locid . "'  WHERE `pemp_id`='" . $pemp_trid . "' AND `pemp_refid`='" . $pemp_refid . "' AND `pemp_month`='" . $pemp_month . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $sqlquery;
    }

    //emp Final Process
    public function SelectFinalProcess($pef_month, $pef_comid, $pef_locid)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT pem.pemp_id as EmpTrId,pem.pemp_refid as EmpRefId,pe.emp_printname as EmpName,pem.pemp_month as EmpMonth,pem.pemp_comid AS EmpComId,"
            . "pcm.pcm_name as EmpComName,pem.pemp_locid as EmpLocId,plm.plm_name as EmpLocName,pe.emp_basicsalary as EmpBasic,pem.pemp_noofdays as EmpNoOfDays,"
            . " pef.`pef_wages` as EmpWages, pef.`pef_extraday` as EmpExtraDays, pef.`pef_extradayamt` as EmpExtraDayAmt, pef.`pef_extrahours` as EmpExtraOtHrs, "
            . " pef.`pef_extrahrsamt` as EmpExtraOtAmt, pef.`pef_allowance` as EmpAllowance, pef.`pef_grossamt` as EmpGrossAmt, pef.`pef_advance` as EmpAdvance, "
            . " pef.`pef_epf` as EmpEpf, pef.`pef_socso` as EmpSocso, pef.`pef_deduction` as EmpDeduction, pef.`pef_netpay` as EmpNetPay, pef.`pef_bank` as EmpBank,"
            . " pef.`pef_netcash` as EmpNetCash FROM `pos_emp_monthprocess` as pem "
            . "INNER JOIN  pos_employeeinfo as pe ON pe.emp_id = pem.pemp_refid "
            . "INNER JOIN pos_company_mast as pcm ON pem.pemp_comid = pcm.pcm_id "
            . "INNER JOIN pos_location_mast as plm ON pem.pemp_locid = plm.plm_id"
            . " WHERE pem.pemp_comid='" . $pef_comid . "' AND pem.pemp_locid='" . $pef_locid . "' AND pe.emp_active=1 AND pem.pemp_month='" . $pef_month . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SelectOldFinalProcess($pef_month, $pef_comid, $pef_locid)
    {
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

    public function SaveFinalProcess(
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
    ) {
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

    public function DeleteFinalProcess($pef_id)
    {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `pos_emp_finalprocess` WHERE  `pef_id`='" . $pef_id . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function CheckMonthofFinal($pemp_month, $pemp_comid, $pemp_locid)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT count(*) as Counts FROM `pos_emp_finalprocess` WHERE `pef_month`='" . $pemp_month . "' AND  `pef_comid`='" . $pemp_comid . "' AND `pef_locid`='" . $pemp_locid . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteMonthofFinal($pemp_month, $pemp_comid, $pemp_locid)
    {
        $conn = $this->conn;
        $sqlquery = ("DELETE  FROM `pos_emp_finalprocess` WHERE `pef_month`='" . $pemp_month . "' AND  `pef_comid`='" . $pemp_comid . "' AND `pef_locid`='" . $pemp_locid . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //branchaccesslist
    public function SelectBranchRightsList()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `mbid` as Id, `comid` as ComId, mbr.`branchid` as BranchId, `userid` as UserId,phm.ph_name as HmMenu,phm.ph_menucode as HmCode,psm.ps_name as SubMenu,psm.ps_menucode as SmCode,mbr.active as Active FROM `mgmt_branchrights` as mbr INNER JOIN pos_headermenu as phm ON phm.ph_menucode=mbr.hmcode INNER JOIN pos_submenu as psm ON psm.ps_menucode =mbr.smcode;");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveBranchRightsList($comid, $branchid, $userid, $hmcode, $smcode, $active)
    {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `mgmt_branchrights`(`comid`, `branchid`, `userid`,`hmcode`,`smcode`,`active`) VALUES ( '" . $comid . "','" . $branchid . "','" . $userid . "','" . $hmcode . "','" . $smcode . "','" . $active . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteBranchRightsList($UserId)
    {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `mgmt_branchrights` WHERE `userid`='" . $UserId . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function InsertHeaderMenu($ph_name, $ph_projectid, $ph_active, $ph_menucode)
    {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_headermenu`(`ph_name`, `ph_projectid`, `ph_active`, `ph_menucode`) VALUES ('" . $ph_name . "','" . $ph_projectid . "','" . $ph_active . "','" . $ph_menucode . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function InsertSubMenu($ps_name, $ph_id, $ps_active, $ps_menucode)
    {
        $conn = $this->conn;
        $sqlquery = ("INSERT INTO `pos_submenu`( `ps_name`, `ph_id`, `ps_active`, `ps_menucode`) VALUES ('" . $ps_name . "','" . $ph_id . "','" . $ps_active . "','" . $ps_menucode . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function UpdateHeaderMenu($phid, $ph_name, $ph_projectid, $ph_active, $ph_menucode)
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `pos_headermenu` SET `ph_name`='" . $ph_name . "',`ph_projectid`='" . $ph_projectid . "',`ph_active`='" . $ph_active . "',`ph_menucode`='" . $ph_menucode . "' WHERE `phid`='" . $phid . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function UpdateSubMenu($psid, $ps_name, $ph_id, $ps_active, $ps_menucode)
    {
        $conn = $this->conn;
        $sqlquery = ("UPDATE `pos_submenu` SET `ps_name`='" . $ps_name . "',`ph_id`='" . $ph_id . "',`ps_active`='" . $ps_active . "',`ps_menucode`='" . $ps_menucode . "' WHERE `psid`='" . $psid . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteHeaderMenu($phid)
    {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `pos_headermenu` WHERE `phid`='" . $phid . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteSubMenu($psid)
    {
        $conn = $this->conn;
        $sqlquery = ("DELETE FROM `pos_submenu` WHERE `psid`='" . $psid . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SelectHeadAndSubMenu()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `phid`, `ph_name`, `ph_projectid`, `ph_active`, `ph_menucode`,`psid`, `ps_name`, `ph_id`, `ps_active`, `ps_menucode` FROM `pos_headermenu` as phm INNER JOIN `pos_submenu` as psm ON phm.ph_menucode=psm.ph_id;");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SelectHeadMenu()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `phid`, `ph_name`, `ph_projectid`, `ph_active`, `ph_menucode`  FROM `pos_headermenu`");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SelectSubMenu()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT `psid`, `ps_name`, `ph_id`, `ps_active`, `ps_menucode` FROM `pos_submenu`");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SelectSubMenuWithHeader()
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT psm.`psid`, psm.`ps_name`, psm.`ph_id`, psm.`ps_active`, psm.`ps_menucode`, phm.`ph_name` as header_name FROM `pos_submenu` as psm INNER JOIN `pos_headermenu` as phm ON psm.ph_id = phm.phid");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SelectSubMenuByHeaderId($headerMenuId)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT psm.`psid`, psm.`ps_name`, psm.`ph_id`, psm.`ps_active`, psm.`ps_menucode`, phm.`ph_name` as header_name FROM `pos_submenu` as psm INNER JOIN `pos_headermenu` as phm ON psm.ph_id = phm.phid WHERE psm.`ph_id`='" . $headerMenuId . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function DeleteTruncateMenu()
    {
        $conn = $this->conn;
        $query1 = "TRUNCATE TABLE pos_headermenu";
        $query2 = "TRUNCATE TABLE pos_submenu";
        $result1 = mysqli_query($conn, $query1);
        $result2 = mysqli_query($conn, $query2);
        return ($result1 && $result2);
    }

    public function ValidCheckHMenu($HMenuCode)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT count(*) as Counts FROM `pos_headermenu` WHERE ph_menucode='" . $HMenuCode . "'");
        $result = mysqli_query($conn, $sqlquery);
        while ($row = mysqli_fetch_row($result)) {
            $Maxno = $row[0];
        }
        return $Maxno;
    }

    public function ValidCheckSMenu($SMenuCode)
    {
        $conn = $this->conn;
        $sqlquery = ("SELECT count(*) as Counts FROM `pos_submenu` WHERE ps_menucode='" . $SMenuCode . "'");
        $result = mysqli_query($conn, $sqlquery);
        while ($row = mysqli_fetch_row($result)) {
            $Maxno = $row[0];
        }
        return $Maxno;
    }

    public function _branchSelectIp($comid)
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT dbm.branchid as Id,dbm.branchname as BranchName,dbm.branchcustomerid as CompanyId,cm.customerName as CompanyName,dbm.branchaddress as Address,dbm.branchemail as Email, dbm.branchcontact as Contact,dbm.branchanydesk as AnyDesk,dbm.branchserver as Server,dbm.branchclient as Client,dbm.branchtab as Tab,branchinstalldate as InstallDate, dbm.branchlock as Locks,dbm.branchactivationcode as Activation,dbm.branchstatus as Active,dbm.branchmessage as Message,dbm.branchip as IpAddress,dbm.branchpassword as BrPassword FROM `di_branch_mast` as dbm INNER join `customermaster` as cm on dbm.branchcustomerid=cm.customerId WHERE dbm.branchcustomerid = '" . $comid . "';");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _branchUpdateIp($branchid, $ip)
    {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `di_branch_mast` SET  `branchip`='" . $ip . "'  WHERE `branchid`='" . $branchid . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    function base64_url_encode($input)
    {
        return strtr(base64_encode($input), '+ /= ', '-_.');
    }

    function base64_url_decode($input)
    {
        return base64_decode(strtr($input, '-_.', '+ /= '));
    }

    //Group Policy Methods
    public function GetAllUserGroups()
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT `pug_id`, `pug_name`, `pug_description`, `pug_active`, `pug_created_date` FROM `pos_usergroups` WHERE 1 ORDER BY `pug_name` ASC");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function CreateUserGroup($group_name, $group_description, $group_active)
    {
        $conn = $this->conn;
        $sqlQuery = ("INSERT INTO `pos_usergroups`(`pug_name`, `pug_description`, `pug_active`, `pug_created_date`) VALUES ('" . $group_name . "','" . $group_description . "','" . $group_active . "','" . date('Y-m-d H:i:s') . "')");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function UpdateUserGroup($group_id, $group_name, $group_description, $group_active)
    {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `pos_usergroups` SET `pug_name`='" . $group_name . "', `pug_description`='" . $group_description . "', `pug_active`='" . $group_active . "', `pug_modified_date`='" . date('Y-m-d H:i:s') . "' WHERE `pug_id`='" . $group_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function DeleteUserGroup($group_id)
    {
        $conn = $this->conn;
        // First delete all menu permissions for this group
        $sqlQuery1 = ("DELETE FROM `pos_group_menu_permissions` WHERE `pgmp_group_id`='" . $group_id . "'");
        mysqli_query($conn, $sqlQuery1);

        // Then delete the group
        $sqlQuery2 = ("DELETE FROM `pos_usergroups` WHERE `pug_id`='" . $group_id . "'");
        $result = mysqli_query($conn, $sqlQuery2);
        return $result;
    }

    public function GetGroupMenuPermissions($group_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT gmp.`pgmp_id` as permission_id, gmp.`pgmp_group_id` as group_id,
                      CASE WHEN gmp.`pgmp_header_menu_id` IS NOT NULL THEN gmp.`pgmp_header_menu_id` ELSE gmp.`pgmp_sub_menu_id` END as menu_id,
                      CASE WHEN gmp.`pgmp_header_menu_id` IS NOT NULL THEN 'header' ELSE 'sub' END as menu_type,
                      gmp.`pgmp_header_menu_id` as header_menu_id,
                      gmp.`pgmp_sub_menu_id` as sub_menu_id,
                      gmp.`pgmp_active` as menu_active,
                      CASE WHEN gmp.`pgmp_header_menu_id` IS NOT NULL THEN phm.`ph_name` ELSE psm.`ps_name` END as menu_name,
                      CASE WHEN gmp.`pgmp_header_menu_id` IS NOT NULL THEN phm.`ph_menucode` ELSE psm.`ps_menucode` END as menu_code
                      FROM `pos_group_menu_permissions` as gmp
                      LEFT JOIN `pos_headermenu` as phm ON gmp.`pgmp_header_menu_id` = phm.`phid`
                      LEFT JOIN `pos_submenu` as psm ON gmp.`pgmp_sub_menu_id` = psm.`psid`
                      WHERE gmp.`pgmp_group_id`='" . $group_id . "' ORDER BY menu_name ASC");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function DeleteGroupMenuPermissions($group_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("DELETE FROM `pos_group_menu_permissions` WHERE `pgmp_group_id`='" . $group_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function SaveGroupMenuPermission($group_id, $header_menu_id, $sub_menu_id, $menu_active)
    {
        $conn = $this->conn;
        if ($sub_menu_id === null || $sub_menu_id === '' || $sub_menu_id == 0) {
            // Header menu - use NULL for sub_menu_id due to foreign key constraint
            $sqlQuery = ("INSERT INTO `pos_group_menu_permissions`(`pgmp_group_id`, `pgmp_header_menu_id`, `pgmp_sub_menu_id`, `pgmp_active`)
                          VALUES ('" . $group_id . "','" . $header_menu_id . "', NULL, '" . $menu_active . "')");
        } else {
            // Sub menu - store both header_menu_id and sub_menu_id
            $sqlQuery = ("INSERT INTO `pos_group_menu_permissions`(`pgmp_group_id`, `pgmp_header_menu_id`, `pgmp_sub_menu_id`, `pgmp_active`)
                          VALUES ('" . $group_id . "', '" . $header_menu_id . "', '" . $sub_menu_id . "', '" . $menu_active . "')");
        }
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }
    public function GetUserMenuPermissions($user_id)
    {
        $conn = $this->conn;

        // First check if user belongs to group_id = 1 (Admin group)
        $adminCheckQuery = ("SELECT u.group_id, ug.pug_name FROM `users` as u
                            INNER JOIN `pos_usergroups` as ug ON u.`group_id` = ug.`pug_id`
                            WHERE u.`id`='" . $user_id . "' AND u.`status`='1'");
        $adminResult = mysqli_query($conn, $adminCheckQuery);

        if ($adminResult && mysqli_num_rows($adminResult) > 0) {
            $adminRow = mysqli_fetch_assoc($adminResult);
            if ($adminRow['group_id'] == 1) {
                // Group ID 1 gets all menus automatically
                $sqlQuery = ("SELECT
                            0 as permission_id,
                            1 as group_id,
                            phm.`phid` as menu_id,
                            'header' as menu_type,
                            phm.`phid` as header_menu_id,
                            NULL as sub_menu_id,
                            1 as menu_active,
                            phm.`ph_name` as menu_name,
                            phm.`ph_menucode` as menu_code,
                            '" . $adminRow['pug_name'] . "' as group_name
                            FROM `pos_headermenu` as phm
                            WHERE phm.`ph_active`='1'

                            UNION ALL

                            SELECT
                            0 as permission_id,
                            1 as group_id,
                            psm.`psid` as menu_id,
                            'sub' as menu_type,
                            psm.`ph_id` as header_menu_id,
                            psm.`psid` as sub_menu_id,
                            1 as menu_active,
                            psm.`ps_name` as menu_name,
                            psm.`ps_menucode` as menu_code,
                            '" . $adminRow['pug_name'] . "' as group_name
                            FROM `pos_submenu` as psm
                            WHERE psm.`ps_active`='1'

                            ORDER BY menu_name ASC");

                $result = mysqli_query($conn, $sqlQuery);
                return $result;
            }
        }

        // For other groups, get permissions normally
        $sqlQuery = ("SELECT gmp.`pgmp_id` as permission_id, gmp.`pgmp_group_id` as group_id,
                    CASE WHEN gmp.`pgmp_sub_menu_id` IS NULL THEN gmp.`pgmp_header_menu_id` ELSE gmp.`pgmp_sub_menu_id` END as menu_id,
                    CASE WHEN gmp.`pgmp_sub_menu_id` IS NULL THEN 'header' ELSE 'sub' END as menu_type,
                    gmp.`pgmp_header_menu_id` as header_menu_id,
                    gmp.`pgmp_sub_menu_id` as sub_menu_id,
                    gmp.`pgmp_active` as menu_active,
                    CASE WHEN gmp.`pgmp_sub_menu_id` IS NULL THEN phm.`ph_name` ELSE psm.`ps_name` END as menu_name,
                    CASE WHEN gmp.`pgmp_sub_menu_id` IS NULL THEN phm.`ph_menucode` ELSE psm.`ps_menucode` END as menu_code,
                    ug.`pug_name` as group_name
                    FROM `users` as u
                    INNER JOIN `pos_usergroups` as ug ON u.`group_id` = ug.`pug_id`
                    INNER JOIN `pos_group_menu_permissions` as gmp ON ug.`pug_id` = gmp.`pgmp_group_id`
                    LEFT JOIN `pos_headermenu` as phm ON gmp.`pgmp_header_menu_id` = phm.`phid`
                    LEFT JOIN `pos_submenu` as psm ON gmp.`pgmp_sub_menu_id` = psm.`psid`
                    WHERE u.`id`='" . $user_id . "'
                    AND u.`status`='1'
                    AND ug.`pug_active`='1'
                    AND gmp.`pgmp_active`='1'
                    ORDER BY menu_name ASC");

        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function CheckUserPermission($user_id, $menu_code)
    {
        $conn = $this->conn;

        // First check if user belongs to group_id = 1 (Admin group)
        $adminCheckQuery = ("SELECT u.group_id FROM `users` as u WHERE u.`id`='" . $user_id . "' AND u.`status`='1'");
        $adminResult = mysqli_query($conn, $adminCheckQuery);

        if ($adminResult && mysqli_num_rows($adminResult) > 0) {
            $adminRow = mysqli_fetch_assoc($adminResult);
            if ($adminRow['group_id'] == 1) {
                // Group ID 1 always has access to all menus
                return true;
            }
        }

        // For other groups, check permissions normally
        $sqlQuery = ("SELECT COUNT(*) as has_permission
                    FROM `users` as u
                    INNER JOIN `pos_usergroups` as ug ON u.`group_id` = ug.`pug_id`
                    INNER JOIN `pos_group_menu_permissions` as gmp ON ug.`pug_id` = gmp.`pgmp_group_id`
                    LEFT JOIN `pos_headermenu` as phm ON gmp.`pgmp_header_menu_id` = phm.`phid`
                    LEFT JOIN `pos_submenu` as psm ON gmp.`pgmp_sub_menu_id` = psm.`psid`
                    WHERE u.`id`='" . $user_id . "'
                    AND u.`status`='1'
                    AND ug.`pug_active`='1'
                    AND gmp.`pgmp_active`='1'
                    AND (phm.`ph_menucode`='" . $menu_code . "' OR psm.`ps_menucode`='" . $menu_code . "')");

        $result = mysqli_query($conn, $sqlQuery);
        if ($result) {
            $row = mysqli_fetch_assoc($result);
            return $row['has_permission'] > 0;
        }
        return false;
    }
    public function GetAllMenusForPermission()
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT 'header' as menu_type, phm.`phid` as menu_id, phm.`ph_name` as menu_name, phm.`ph_menucode` as menu_code, phm.`ph_active` as active
                      FROM `pos_headermenu` as phm
                      WHERE phm.`ph_active`='1'
                      UNION ALL
                      SELECT 'sub' as menu_type, psm.`psid` as menu_id, CONCAT(phm.`ph_name`, ' -> ', psm.`ps_name`) as menu_name, psm.`ps_menucode` as menu_code, psm.`ps_active` as active
                      FROM `pos_submenu` as psm
                      INNER JOIN `pos_headermenu` as phm ON psm.`ph_id` = phm.`phid`
                      WHERE psm.`ps_active`='1' AND phm.`ph_active`='1'
                      ORDER BY menu_name ASC");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function AssignUserToGroup($user_id, $group_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `users` SET `group_id`='" . $group_id . "' WHERE `id`='" . $user_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    //POS Settings CRUD Functions
    public function GetPosSettings($whereClause = '', $params = array())
    {
        $conn = $this->conn;
        $sqlQuery = "SELECT `Id`, `Name`, `Status`, `Value`, `Type`, `Created` FROM `pos_settings` WHERE 1" . $whereClause . " ORDER BY `Name` ASC";

        // For now, we'll use simple string concatenation since the original codebase doesn't use prepared statements
        // In a production environment, prepared statements should be used for security
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function InsertPosSetting($Name, $Status, $Value, $Type)
    {
        $conn = $this->conn;
        $sqlQuery = ("INSERT INTO `pos_settings`(`Name`, `Status`, `Value`, `Type`, `Created`) VALUES ('" . $Name . "','" . $Status . "','" . $Value . "','" . $Type . "','" . date('Y-m-d H:i:s') . "')");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function UpdatePosSetting($Id, $Name, $Status, $Value, $Type)
    {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `pos_settings` SET `Name`='" . $Name . "',`Status`='" . $Status . "',`Value`='" . $Value . "',`Type`='" . $Type . "'  WHERE `Id`='" . $Id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function DeletePosSetting($Id)
    {
        $conn = $this->conn;
        $sqlQuery = ("DELETE FROM `pos_settings` WHERE `Id`='" . $Id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function GetPosSettingById($Id)
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT `Id`, `Name`, `Status`, `Value`, `Type`, `Created` FROM `pos_settings` WHERE `Id`='" . $Id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function GetPosSettingByName($Name)
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT `Id`, `Name`, `Status`, `Value`, `Type`, `Created` FROM `pos_settings` WHERE `Name`='" . $Name . "' AND `Status`='1'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    // Multiple Price Management Methods
    public function _InsertMultiplePrice($item_id, $price_name, $price_value, $com_id, $loc_id, $status)
    {
        $conn = $this->conn;
        $sqlQuery = ("INSERT INTO `item_multiple_price`(`item_id`, `price_name`, `price_value`, `com_id`, `loc_id`, `status`, `created`) VALUES "
            . "('" . $item_id . "','" . $price_name . "','" . $price_value . "','" . $com_id . "','" . $loc_id . "','" . $status . "','" . date('Y-m-d H:i:s') . "')");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _UpdateMultiplePrice($price_id, $item_id, $price_name, $price_value, $status)
    {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `item_multiple_price` SET `item_id`='" . $item_id . "',`price_name`='" . $price_name . "',`price_value`='" . $price_value . "',`status`='" . $status . "' WHERE `price_id`='" . $price_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _DeleteMultiplePrice($price_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("DELETE FROM `item_multiple_price` WHERE `price_id`='" . $price_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _GetMultiplePricesByItem($item_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT `price_id`, `item_id`, `price_name`, `price_value`, `status`, `created` FROM `item_multiple_price` WHERE `item_id`='" . $item_id . "' AND `status`='1' ORDER BY `price_id` ASC");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _GetAllMultiplePrices()
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT imp.`price_id`, imp.`item_id`, dim.`dim_item_name` as `item_name`, imp.`price_name`, imp.`price_value`, imp.`status`, imp.`created` "
            . "FROM `item_multiple_price` imp "
            . "INNER JOIN `di_item_mast` dim ON imp.`item_id` = dim.`dim_item_id` "
            . "WHERE imp.`status`='1' ORDER BY imp.`item_id`, imp.`price_id`");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    // Discount Management Methods
    public function _InsertDiscount($discount_name, $discount_type, $discount_value, $discount_description, $status, $com_id, $loc_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("INSERT INTO `discount_master`(`discount_name`, `discount_type`, `discount_value`, `discount_description`, `status`, `com_id`, `loc_id`, `created`) VALUES "
            . "('" . $discount_name . "','" . $discount_type . "','" . $discount_value . "','" . $discount_description . "','" . $status . "','" . $com_id . "','" . $loc_id . "','" . date('Y-m-d H:i:s') . "')");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _UpdateDiscount($discount_id, $discount_name, $discount_type, $discount_value, $discount_description, $status)
    {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `discount_master` SET `discount_name`='" . $discount_name . "',`discount_type`='" . $discount_type . "',`discount_value`='" . $discount_value . "',`discount_description`='" . $discount_description . "',`status`='" . $status . "' WHERE `discount_id`='" . $discount_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _DeleteDiscount($discount_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("DELETE FROM `discount_master` WHERE `discount_id`='" . $discount_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _GetAllActiveDiscounts()
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT `discount_id`, `discount_name`, `discount_type`, `discount_value`, `discount_description`, `status`, `created` FROM `discount_master` WHERE `status`='1' ORDER BY `discount_name` ASC");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _GetDiscountById($discount_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT `discount_id`, `discount_name`, `discount_type`, `discount_value`, `discount_description`, `status`, `created` FROM `discount_master` WHERE `discount_id`='" . $discount_id . "' AND `status`='1'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    //SalesMan Commission

    public function GetSalesmanList()
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT `emp_id`, `emp_printname`, `emp_firstname`, `emp_lastname`, `emp_designation`, `emp_active`
                     FROM `pos_employeeinfo`
                     WHERE `emp_active`='1'
                     ORDER BY `emp_printname` ASC");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Get all sub groups (categories) for commission setup
     */
    public function GetSubGroupList()
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT sg.dcm_id as SubGroupId, sg.dcm_name as SubGroupName, mg.mainname as MainGroupName, sg.dcm_active as Active
                     FROM `di_category_master` as sg
                     INNER JOIN `di_main_group` as mg ON sg.di_main_id = mg.mainid
                     WHERE sg.dcm_active = '1'
                     ORDER BY mg.mainname ASC, sg.dcm_name ASC");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Get items by sub group for commission setup
     */
    public function GetItemsBySubGroup($sub_group_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT dim.dim_item_id as ItemId, dim.dim_item_name as ItemName, dim.dim_item_barcode as Barcode,
                     dim.dim_sell_price as SellPrice, dcm.dcm_name as SubGroupName
                     FROM `di_item_mast` as dim
                     INNER JOIN `di_category_master` as dcm ON dim.dim_cate_id = dcm.dcm_id
                     WHERE dim.dim_cate_id = '" . $sub_group_id . "' AND dim.dim_status = '1'
                     ORDER BY dim.dim_item_name ASC");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Insert new salesman commission
     */
    public function InsertSalesmanCommission($emp_id, $item_id, $sub_group_id, $commission_percentage, $commission_type, $fixed_amount, $status)
    {
        $conn = $this->conn;
        $sqlQuery = ("INSERT INTO `salesman_commission`(`emp_id`, `item_id`, `sub_group_id`, `commission_percentage`, `commission_type`, `fixed_amount`, `status`)
                     VALUES ('" . $emp_id . "', '" . $item_id . "', '" . $sub_group_id . "', '" . $commission_percentage . "', '" . $commission_type . "', '" . $fixed_amount . "', '" . $status . "')");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Update existing salesman commission
     */
    public function UpdateSalesmanCommission($commission_id, $emp_id, $item_id, $sub_group_id, $commission_percentage, $commission_type, $fixed_amount, $status)
    {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `salesman_commission` SET
                     `emp_id`='" . $emp_id . "',
                     `item_id`='" . $item_id . "',
                     `sub_group_id`='" . $sub_group_id . "',
                     `commission_percentage`='" . $commission_percentage . "',
                     `commission_type`='" . $commission_type . "',
                     `fixed_amount`='" . $fixed_amount . "',
                     `status`='" . $status . "',
                     `updated`='" . date('Y-m-d H:i:s') . "'
                     WHERE `commission_id`='" . $commission_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Delete salesman commission
     */
    public function DeleteSalesmanCommission($commission_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("DELETE FROM `salesman_commission` WHERE `commission_id`='" . $commission_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Get all salesman commissions with details
     */
    public function GetAllSalesmanCommissions()
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT
                     sc.commission_id as CommissionId,
                     sc.emp_id as EmpId,
                     pe.emp_printname as SalesmanName,
                     pe.emp_designation as Designation,
                     sc.item_id as ItemId,
                     dim.dim_item_name as ItemName,
                     dim.dim_item_barcode as Barcode,
                     sc.sub_group_id as SubGroupId,
                     dcm.dcm_name as SubGroupName,
                     mg.mainname as MainGroupName,
                     sc.commission_percentage as CommissionPercentage,
                     sc.commission_type as CommissionType,
                     sc.fixed_amount as FixedAmount,
                     sc.status as Status,
                     sc.created as Created,
                     sc.updated as Updated
                     FROM `salesman_commission` as sc
                     INNER JOIN `pos_employeeinfo` as pe ON sc.emp_id = pe.emp_id
                     INNER JOIN `di_item_mast` as dim ON sc.item_id = dim.dim_item_id
                     INNER JOIN `di_category_master` as dcm ON sc.sub_group_id = dcm.dcm_id
                     INNER JOIN `di_main_group` as mg ON dcm.di_main_id = mg.mainid
                     WHERE sc.status = '1'
                     ORDER BY pe.emp_printname ASC, dcm.dcm_name ASC, dim.dim_item_name ASC");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Get commissions by salesman
     */
    public function GetCommissionsBySalesman($emp_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT
                     sc.commission_id as CommissionId,
                     sc.emp_id as EmpId,
                     pe.emp_printname as SalesmanName,
                     pe.emp_designation as Designation,
                     sc.item_id as ItemId,
                     dim.dim_item_name as ItemName,
                     dim.dim_item_barcode as Barcode,
                     sc.sub_group_id as SubGroupId,
                     dcm.dcm_name as SubGroupName,
                     mg.mainname as MainGroupName,
                     sc.commission_percentage as CommissionPercentage,
                     sc.commission_type as CommissionType,
                     sc.fixed_amount as FixedAmount,
                     sc.status as Status,
                     sc.created as Created,
                     sc.updated as Updated
                     FROM `salesman_commission` as sc
                     INNER JOIN `pos_employeeinfo` as pe ON sc.emp_id = pe.emp_id
                     INNER JOIN `di_item_mast` as dim ON sc.item_id = dim.dim_item_id
                     INNER JOIN `di_category_master` as dcm ON sc.sub_group_id = dcm.dcm_id
                     INNER JOIN `di_main_group` as mg ON dcm.di_main_id = mg.mainid
                     WHERE sc.emp_id = '" . $emp_id . "' AND sc.status = '1'
                     ORDER BY dcm.dcm_name ASC, dim.dim_item_name ASC");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Get commission by item and salesman
     */
    public function GetCommissionByItemAndSalesman($item_id, $emp_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT
                     sc.commission_id as CommissionId,
                     sc.commission_percentage as CommissionPercentage,
                     sc.commission_type as CommissionType,
                     sc.fixed_amount as FixedAmount
                     FROM `salesman_commission` as sc
                     WHERE sc.item_id = '" . $item_id . "' AND sc.emp_id = '" . $emp_id . "' AND sc.status = '1'
                     LIMIT 1");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Calculate commission amount
     */
    public function CalculateCommissionAmount($sale_amount, $commission_percentage, $commission_type, $fixed_amount)
    {
        if ($commission_type == 'PERCENTAGE') {
            return ($sale_amount * $commission_percentage) / 100;
        } else {
            return $fixed_amount;
        }
    }

    /**
     * Log commission transaction
     */
    public function LogCommissionTransaction($commission_id, $emp_id, $sale_invoice_id, $item_id, $sale_amount, $commission_amount, $commission_percentage, $sale_date)
    {
        $conn = $this->conn;
        $sqlQuery = ("INSERT INTO `salesman_commission_log`(`commission_id`, `emp_id`, `sale_invoice_id`, `item_id`, `sale_amount`, `commission_amount`, `commission_percentage`, `sale_date`)
                     VALUES ('" . $commission_id . "', '" . $emp_id . "', '" . $sale_invoice_id . "', '" . $item_id . "', '" . $sale_amount . "', '" . $commission_amount . "', '" . $commission_percentage . "', '" . $sale_date . "')");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Get commission report by date range
     */
    public function GetCommissionReport($emp_id, $from_date, $to_date)
    {
        $conn = $this->conn;
        $whereClause = "";

        if (!empty($emp_id)) {
            $whereClause .= " AND scl.emp_id = '" . $emp_id . "'";
        }

        $sqlQuery = ("SELECT
                     scl.log_id as LogId,
                     pe.emp_printname as SalesmanName,
                     dim.dim_item_name as ItemName,
                     scl.sale_amount as SaleAmount,
                     scl.commission_percentage as CommissionPercentage,
                     scl.commission_amount as CommissionAmount,
                     scl.sale_date as SaleDate,
                     scl.status as Status
                     FROM `salesman_commission_log` as scl
                     INNER JOIN `pos_employeeinfo` as pe ON scl.emp_id = pe.emp_id
                     INNER JOIN `di_item_mast` as dim ON scl.item_id = dim.dim_item_id
                     WHERE scl.sale_date >= '" . $from_date . "' AND scl.sale_date <= '" . $to_date . "'" . $whereClause . "
                     ORDER BY scl.sale_date DESC, pe.emp_printname ASC");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Get commission summary by salesman
     */
    public function GetCommissionSummary($from_date, $to_date)
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT
                     pe.emp_id as EmpId,
                     pe.emp_printname as SalesmanName,
                     COUNT(scl.log_id) as TotalSales,
                     SUM(scl.sale_amount) as TotalSaleAmount,
                     SUM(scl.commission_amount) as TotalCommissionAmount,
                     AVG(scl.commission_percentage) as AvgCommissionPercentage
                     FROM `salesman_commission_log` as scl
                     INNER JOIN `pos_employeeinfo` as pe ON scl.emp_id = pe.emp_id
                     WHERE scl.sale_date >= '" . $from_date . "' AND scl.sale_date <= '" . $to_date . "' AND scl.status = 'PENDING'
                     GROUP BY pe.emp_id, pe.emp_printname
                     ORDER BY TotalCommissionAmount DESC");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Mark commission as paid
     */
    public function MarkCommissionAsPaid($log_ids)
    {
        $conn = $this->conn;
        $log_ids_string = implode(',', array_map('intval', $log_ids));
        $sqlQuery = ("UPDATE `salesman_commission_log` SET `status`='PAID' WHERE `log_id` IN (" . $log_ids_string . ")");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Check if commission exists for item and salesman
     */
    public function CheckCommissionExists($emp_id, $item_id, $sub_group_id)
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT COUNT(*) as count FROM `salesman_commission`
                     WHERE `emp_id`='" . $emp_id . "' AND `item_id`='" . $item_id . "' AND `sub_group_id`='" . $sub_group_id . "' AND `status`='1'");
        $result = mysqli_query($conn, $sqlQuery);
        $row = mysqli_fetch_assoc($result);
        return $row['count'] > 0;
    }
    public function GetSalesManByComidLocid($comid, $locid)
    {
        $conn = $this->conn;
        $sqlQuery = ("SELECT `emp_id` as Id , `emp_printname` as SalesMan FROM `pos_employeeinfo` WHERE `emp_compid`= " . intval($comid) . " and `emp_locid`= " . intval($locid));
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Insert new POS Master record
     */
    public function InsertPosMaster(
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
        $pm_comid,
        $pm_locid,
        $pm_batch_number,
        $pm_mailstatus,
        $pm_monthdate,
        $pm_autoupdate,
        $pm_webid,
        $pm_restid
    ) {
        $conn = $this->conn;

        // Check if PM_PREFIX column exists
        $checkColumn = mysqli_query($conn, "SHOW COLUMNS FROM `POS_MASTER` LIKE 'PM_PREFIX'");
        $hasPrefix = mysqli_num_rows($checkColumn) > 0;

        if ($hasPrefix) {
            // Include PM_PREFIX in the query
            $sqlQuery = "INSERT INTO `POS_MASTER` (
                `PM_MACHINE_NAME`, `PM_BUINESS_DATE`, `PM_TRANS_NO`, `PM_USER_ID`, `PSR_BILL_NUMBER`,
                `PM_DAY_NO`, `PM_SHIFT_NO`, `PM_DAY_ST`, `PM_SHIFT_ST`, `PM_PREFIX`,
                `PM_COMID`, `PM_LOCID`, `PM_BATCH_NUMBER`, `PM_MAILSTATUS`, `PM_MONTHDATE`,
                `PM_AUTOUPDATE`, `PM_WEBID`, `PM_RESTID`, `PM_CREATED`
            ) VALUES (
                '" . mysqli_real_escape_string($conn, $pm_machine_name) . "',
                '" . mysqli_real_escape_string($conn, $pm_business_date) . "',
                '" . mysqli_real_escape_string($conn, $pm_trans_no) . "',
                '" . intval($pm_user_id) . "',
                '" . mysqli_real_escape_string($conn, $psr_bill_number) . "',
                '" . intval($pm_day_no) . "',
                '" . intval($pm_shift_no) . "',
                '" . mysqli_real_escape_string($conn, $pm_day_st) . "',
                '" . mysqli_real_escape_string($conn, $pm_shift_st) . "',
                '" . mysqli_real_escape_string($conn, $pm_prefix) . "',
                '" . intval($pm_comid) . "',
                '" . intval($pm_locid) . "',
                '" . mysqli_real_escape_string($conn, $pm_batch_number) . "',
                '" . mysqli_real_escape_string($conn, $pm_mailstatus) . "',
                '" . mysqli_real_escape_string($conn, $pm_monthdate) . "',
                '" . mysqli_real_escape_string($conn, $pm_autoupdate) . "',
                '" . mysqli_real_escape_string($conn, $pm_webid) . "',
                '" . mysqli_real_escape_string($conn, $pm_restid) . "',
                NOW()
            )";
        } else {
            // Exclude PM_PREFIX from the query
            $sqlQuery = "INSERT INTO `POS_MASTER` (
                `PM_MACHINE_NAME`, `PM_BUINESS_DATE`, `PM_TRANS_NO`, `PM_USER_ID`, `PSR_BILL_NUMBER`,
                `PM_DAY_NO`, `PM_SHIFT_NO`, `PM_DAY_ST`, `PM_SHIFT_ST`,
                `PM_COMID`, `PM_LOCID`, `PM_BATCH_NUMBER`, `PM_MAILSTATUS`, `PM_MONTHDATE`,
                `PM_AUTOUPDATE`, `PM_WEBID`, `PM_RESTID`, `PM_CREATED`
            ) VALUES (
                '" . mysqli_real_escape_string($conn, $pm_machine_name) . "',
                '" . mysqli_real_escape_string($conn, $pm_business_date) . "',
                '" . mysqli_real_escape_string($conn, $pm_trans_no) . "',
                '" . intval($pm_user_id) . "',
                '" . mysqli_real_escape_string($conn, $psr_bill_number) . "',
                '" . intval($pm_day_no) . "',
                '" . intval($pm_shift_no) . "',
                '" . mysqli_real_escape_string($conn, $pm_day_st) . "',
                '" . mysqli_real_escape_string($conn, $pm_shift_st) . "',
                '" . intval($pm_comid) . "',
                '" . intval($pm_locid) . "',
                '" . mysqli_real_escape_string($conn, $pm_batch_number) . "',
                '" . mysqli_real_escape_string($conn, $pm_mailstatus) . "',
                '" . mysqli_real_escape_string($conn, $pm_monthdate) . "',
                '" . mysqli_real_escape_string($conn, $pm_autoupdate) . "',
                '" . mysqli_real_escape_string($conn, $pm_webid) . "',
                '" . mysqli_real_escape_string($conn, $pm_restid) . "',
                NOW()
            )";
        }
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Update POS Master record based on company and location ID
     */
    public function UpdatePosMaster(
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
        $pm_comid,
        $pm_locid,
        $pm_batch_number,
        $pm_mailstatus,
        $pm_monthdate,
        $pm_autoupdate,
        $pm_webid,
        $pm_restid
    ) {
        $conn = $this->conn;
        $sqlQuery = "UPDATE `POS_MASTER` SET
            `PM_MACHINE_NAME` = '" . mysqli_real_escape_string($conn, $pm_machine_name) . "',
            `PM_BUINESS_DATE` = '" . mysqli_real_escape_string($conn, $pm_business_date) . "',
            `PM_TRANS_NO` = '" . mysqli_real_escape_string($conn, $pm_trans_no) . "',
            `PM_USER_ID` = '" . intval($pm_user_id) . "',
            `PSR_BILL_NUMBER` = '" . mysqli_real_escape_string($conn, $psr_bill_number) . "',
            `PM_DAY_NO` = '" . intval($pm_day_no) . "',
            `PM_SHIFT_NO` = '" . intval($pm_shift_no) . "',
            `PM_DAY_ST` = '" . mysqli_real_escape_string($conn, $pm_day_st) . "',
            `PM_SHIFT_ST` = '" . mysqli_real_escape_string($conn, $pm_shift_st) . "',
            `PM_PREFIX` = '" . mysqli_real_escape_string($conn, $pm_prefix) . "',
            `PM_COMID` = '" . intval($pm_comid) . "',
            `PM_LOCID` = '" . intval($pm_locid) . "',
            `PM_BATCH_NUMBER` = '" . mysqli_real_escape_string($conn, $pm_batch_number) . "',
            `PM_MAILSTATUS` = '" . mysqli_real_escape_string($conn, $pm_mailstatus) . "',
            `PM_MONTHDATE` = '" . mysqli_real_escape_string($conn, $pm_monthdate) . "',
            `PM_AUTOUPDATE` = '" . mysqli_real_escape_string($conn, $pm_autoupdate) . "',
            `PM_WEBID` = '" . mysqli_real_escape_string($conn, $pm_webid) . "',
            `PM_RESTID` = '" . mysqli_real_escape_string($conn, $pm_restid) . "',
            `PM_UPDATED` = NOW()
        WHERE `PM_ID` = '" . intval($pm_id) . "'
        AND `PM_COMID` = '" . intval($pm_comid) . "'
        AND `PM_LOCID` = '" . intval($pm_locid) . "'";
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Get POS Master records by company and location ID
     */
    public function GetPosMasterByComidLocid($comid, $locid)
    {
        $conn = $this->conn;

        // Check if PM_PREFIX column exists
        $checkColumn = mysqli_query($conn, "SHOW COLUMNS FROM `POS_MASTER` LIKE 'PM_PREFIX'");
        $hasPrefix = mysqli_num_rows($checkColumn) > 0;

        if ($hasPrefix) {
            $sqlQuery = "SELECT `PM_ID`, `PM_MACHINE_NAME`, `PM_BUINESS_DATE`, `PM_TRANS_NO`, `PM_USER_ID`,
                        `PSR_BILL_NUMBER`, `PM_DAY_NO`, `PM_SHIFT_NO`, `PM_DAY_ST`, `PM_SHIFT_ST`,
                        `PM_PREFIX`, `PM_COMID`, `PM_LOCID`, `PM_BATCH_NUMBER`, `PM_MAILSTATUS`,
                        `PM_MONTHDATE`, `PM_AUTOUPDATE`, `PM_WEBID`, `PM_RESTID`, `PM_CREATED`, `PM_UPDATED`
                        FROM `POS_MASTER`
                        WHERE `PM_COMID` = '" . intval($comid) . "'
                        AND `PM_LOCID` = '" . intval($locid) . "'
                        ORDER BY `PM_CREATED` DESC";
        } else {
            $sqlQuery = "SELECT `PM_ID`, `PM_MACHINE_NAME`, `PM_BUINESS_DATE`, `PM_TRANS_NO`, `PM_USER_ID`,
                        `PSR_BILL_NUMBER`, `PM_DAY_NO`, `PM_SHIFT_NO`, `PM_DAY_ST`, `PM_SHIFT_ST`,
                        '' as `PM_PREFIX`, `PM_COMID`, `PM_LOCID`, `PM_BATCH_NUMBER`, `PM_MAILSTATUS`,
                        `PM_MONTHDATE`, `PM_AUTOUPDATE`, `PM_WEBID`, `PM_RESTID`, `PM_CREATED`, `PM_UPDATED`
                        FROM `POS_MASTER`
                        WHERE `PM_COMID` = '" . intval($comid) . "'
                        AND `PM_LOCID` = '" . intval($locid) . "'
                        ORDER BY `PM_CREATED` DESC";
        }

        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    /**
     * Check if POS Master record exists
     */
    public function CheckPosMasterExists($pm_id, $comid, $locid)
    {
        $conn = $this->conn;
        $sqlQuery = "SELECT COUNT(*) as count FROM `POS_MASTER`
                    WHERE `PM_ID` = '" . intval($pm_id) . "'
                    AND `PM_COMID` = '" . intval($comid) . "'
                    AND `PM_LOCID` = '" . intval($locid) . "'";
        $result = mysqli_query($conn, $sqlQuery);
        $row = mysqli_fetch_assoc($result);
        return $row['count'] > 0;
    }

    /**
     * Check if company and location combination exists and is active
     */
    public function ValidateCompanyLocationExists($comid, $locid)
    {
        $conn = $this->conn;
        $sqlQuery = "SELECT COUNT(*) as count FROM `pos_company_mast` as pcm
                    INNER JOIN `pos_location_mast` as plm ON 1=1
                    WHERE pcm.`pcm_id` = '" . intval($comid) . "'
                    AND plm.`plm_id` = '" . intval($locid) . "'
                    AND pcm.`pcm_active` = '1'
                    AND plm.`plm_active` = '1'";
        $result = mysqli_query($conn, $sqlQuery);
        $row = mysqli_fetch_assoc($result);
        return $row['count'] > 0;
    }

    public function UpdateTransNo($pm_id, $pm_trans_no, $comid, $locid)
    {
        $conn = $this->conn;
        $sqlQuery = "UPDATE `POS_MASTER`
                    SET `PM_TRANS_NO` = `PM_TRANS_NO` + 1,
                        `PM_UPDATED` = NOW()
                    WHERE `PM_ID` = '" . intval($pm_id) . "'
                    AND `PM_COMID` = '" . intval($comid) . "'
                    AND `PM_LOCID` = '" . intval($locid) . "'";
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }
    public function UpdateBillNumber($pm_id, $pm_bill_number, $comid, $locid)
    {
        $conn = $this->conn;
        $sqlQuery = "UPDATE `POS_MASTER`
                    SET `PM_BILL_NO` = `PM_BILL_NO` + 1,
                        `PM_UPDATED` = NOW()
                    WHERE `PM_ID` = '" . intval($pm_id) . "'
                    AND `PM_COMID` = '" . intval($comid) . "'
                    AND `PM_LOCID` = '" . intval($locid) . "'";
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }
    public function UpdateBatchNumber($pm_id, $pm_batch_number, $comid, $locid)
    {
        $conn = $this->conn;
        $sqlQuery = "UPDATE `POS_MASTER`
                    SET `PM_BATCH_NO` = `PM_BATCH_NO` + 1,
                        `PM_UPDATED` = NOW()
                    WHERE `PM_ID` = '" . intval($pm_id) . "'
                    AND `PM_COMID` = '" . intval($comid) . "'
                    AND `PM_LOCID` = '" . intval($locid) . "'";
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }
    public function UpdateAutoUpdate($pm_id, $pm_auto_update, $comid, $locid)
    {
        $conn = $this->conn;
        $sqlQuery = "UPDATE `POS_MASTER`
                    SET `PM_AUTO_UPDATE` = `PM_AUTO_UPDATE` + 1,
                        `PM_UPDATED` = NOW()
                    WHERE `PM_ID` = '" . intval($pm_id) . "'
                    AND `PM_COMID` = '" . intval($comid) . "'
                    AND `PM_LOCID` = '" . intval($locid) . "'";
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }
    public function UpdateMailStatus($pm_id, $pm_mail_status, $comid, $locid)
    {
        $conn = $this->conn;
        $sqlQuery = "UPDATE `POS_MASTER`
                    SET `PM_MAIL_STATUS` = '" . intval($pm_mail_status) . "',
                        `PM_UPDATED` = NOW()
                    WHERE `PM_ID` = '" . intval($pm_id) . "'
                    AND `PM_COMID` = '" . intval($comid) . "'
                    AND `PM_LOCID` = '" . intval($locid) . "'";
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }
    public function UpdateMonthDate($pm_id, $pm_month_date, $comid, $locid)
    {
        $conn = $this->conn;
        $sqlQuery = "UPDATE `POS_MASTER`
                    SET `PM_MONTHDATE` = '" . intval($pm_month_date) . "',
                        `PM_UPDATED` = NOW()
                    WHERE `PM_ID` = '" . intval($pm_id) . "'
                    AND `PM_COMID` = '" . intval($comid) . "'
                    AND `PM_LOCID` = '" . intval($locid) . "'";
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }
    public function GetAllData($pm_id, $comid, $locid)
    {
        $conn = $this->conn;

        // Check if PM_PREFIX column exists
        $checkColumn = mysqli_query($conn, "SHOW COLUMNS FROM `POS_MASTER` LIKE 'PM_PREFIX'");
        $hasPrefix = mysqli_num_rows($checkColumn) > 0;

        if ($hasPrefix) {
            $sqlQuery = "SELECT `PM_ID`, `PM_MACHINE_NAME`, `PM_BUINESS_DATE`, `PM_TRANS_NO`, `PM_USER_ID`,
                        `PSR_BILL_NUMBER`, `PM_DAY_NO`, `PM_SHIFT_NO`, `PM_DAY_ST`, `PM_SHIFT_ST`,
                        `PM_PREFIX`, `PM_COMID`, `PM_LOCID`, `PM_BATCH_NUMBER`, `PM_MAILSTATUS`,
                        `PM_MONTHDATE`, `PM_AUTOUPDATE`, `PM_WEBID`, `PM_RESTID`, `PM_CREATED`, `PM_UPDATED`
                        FROM `POS_MASTER`
                        WHERE `PM_ID` = '" . intval($pm_id) . "'
                        AND `PM_COMID` = '" . intval($comid) . "'
                        AND `PM_LOCID` = '" . intval($locid) . "'";
        } else {
            $sqlQuery = "SELECT `PM_ID`, `PM_MACHINE_NAME`, `PM_BUINESS_DATE`, `PM_TRANS_NO`, `PM_USER_ID`,
                        `PSR_BILL_NUMBER`, `PM_DAY_NO`, `PM_SHIFT_NO`, `PM_DAY_ST`, `PM_SHIFT_ST`,
                        '' as `PM_PREFIX`, `PM_COMID`, `PM_LOCID`, `PM_BATCH_NUMBER`, `PM_MAILSTATUS`,
                        `PM_MONTHDATE`, `PM_AUTOUPDATE`, `PM_WEBID`, `PM_RESTID`, `PM_CREATED`, `PM_UPDATED`
                        FROM `POS_MASTER`
                        WHERE `PM_ID` = '" . intval($pm_id) . "'
                        AND `PM_COMID` = '" . intval($comid) . "'
                        AND `PM_LOCID` = '" . intval($locid) . "'";
        }

        $result = mysqli_query($conn, $sqlQuery);

        if ($result && mysqli_num_rows($result) > 0) {
            return mysqli_fetch_assoc($result);
        }
        return false;
    }

    // Shift Management Methods

    public function CreateNewShift($comid, $locid, $userid, $pcname, $opbalance = 0)
    {
        $conn = $this->conn;

        // Get current date
        $currentDate = date('Y-m-d');

        // Get next shift number and day number from POS_MASTER
        $shiftQuery = "SELECT `PM_SHIFT_NO`, `PM_DAY_NO` FROM `POS_MASTER`
                      WHERE `PM_COMID` = '" . intval($comid) . "'
                      AND `PM_LOCID` = '" . intval($locid) . "'
                      LIMIT 1";
        $shiftResult = mysqli_query($conn, $shiftQuery);

        $shiftNo = 1;
        $dayNo = 1;

        if ($shiftResult && mysqli_num_rows($shiftResult) > 0) {
            $shiftData = mysqli_fetch_assoc($shiftResult);
            $shiftNo = intval($shiftData['PM_SHIFT_NO']);
            $dayNo = intval($shiftData['PM_DAY_NO']);
        }

        // Insert new shift record
        $insertQuery = "INSERT INTO `pos_shiftclose` (
            `psc_curdate`, `psc_opbalance`, `psc_todaysales`, `psc_totdiscount`,
            `psc_tottax`, `psc_netamt`, `psc_servicetax`, `psc_todayin`,
            `psc_todayout`, `psc_todaybanking`, `psc_clsbalance`, `psc_state`,
            `psc_comid`, `psc_locid`, `psc_shiftno`, `psc_dayno`, `psc_pcname`,
            `psc_totbills`, `psc_cancelamt`, `psc_creditsales`, `psc_opendrawer`,
            `psc_clsdrawer`, `psc_userid`, `psc_smail`, `psc_print`, `psc_created`
        ) VALUES (
            '" . $currentDate . "', '" . floatval($opbalance) . "', 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 'Open',
            '" . intval($comid) . "', '" . intval($locid) . "', '" . intval($shiftNo) . "', '" . intval($dayNo) . "', '" . mysqli_real_escape_string($conn, $pcname) . "',
            0, 0, 0, 0,
            0, '" . intval($userid) . "', 0, 0, NOW()
        )";

        $result = mysqli_query($conn, $insertQuery);

        if ($result) {
            // Update POS_MASTER table to set PM_SHIFT_ST = "Open" after creating new shift
            $updatePosQuery = "UPDATE `POS_MASTER` SET
                              `PM_SHIFT_ST` = 'Open',
                              `PM_UPDATED` = NOW()
                              WHERE `PM_COMID` = '" . intval($comid) . "'
                              AND `PM_LOCID` = '" . intval($locid) . "'";

            mysqli_query($conn, $updatePosQuery);

            return array(
                'shift_id' => mysqli_insert_id($conn),
                'shift_no' => $shiftNo,
                'day_no' => $dayNo
            );
        }
        return false;
    }

    public function UpdateShiftClose($psc_id, $comid, $locid, $shiftCloseData)
    {
        $conn = $this->conn;

        // Build update query
        $updateFields = array();
        $allowedFields = array(
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
            'psc_print'
        );

        foreach ($allowedFields as $field) {
            if (isset($shiftCloseData[$field])) {
                $updateFields[] = "`$field` = '" . mysqli_real_escape_string($conn, $shiftCloseData[$field]) . "'";
            }
        }

        $updateFields[] = "`psc_state` = 'Close'";
        $updateFields[] = "`psc_updated` = NOW()";

        $updateQuery = "UPDATE `pos_shiftclose` SET " . implode(', ', $updateFields) . "
                       WHERE `psc_id` = '" . intval($psc_id) . "'
                       AND `psc_comid` = '" . intval($comid) . "'
                       AND `psc_locid` = '" . intval($locid) . "'";

        $result = mysqli_query($conn, $updateQuery);

        // If shift close successful, update POS_MASTER shift number and status
        if ($result && mysqli_affected_rows($conn) > 0) {
            $this->UpdatePosShiftStatus($comid, $locid);
            return true;
        }
        return false;
    }

    public function UpdatePosShiftStatus($comid, $locid)
    {
        $conn = $this->conn;

        // Update PM_SHIFT_NO +1 and PM_SHIFT_ST = "Close" in POS_MASTER
        $updateQuery = "UPDATE `POS_MASTER` SET
                       `PM_SHIFT_NO` = `PM_SHIFT_NO` + 1,
                       `PM_SHIFT_ST` = 'Close',
                       `PM_UPDATED` = NOW()
                       WHERE `PM_COMID` = '" . intval($comid) . "'
                       AND `PM_LOCID` = '" . intval($locid) . "'";

        return mysqli_query($conn, $updateQuery);
    }

    public function GetShiftData($comid, $locid, $psc_id = null, $state = null)
    {
        $conn = $this->conn;

        $whereConditions = array(
            "`psc_comid` = '" . intval($comid) . "'",
            "`psc_locid` = '" . intval($locid) . "'"
        );

        if ($psc_id !== null) {
            $whereConditions[] = "`psc_id` = '" . intval($psc_id) . "'";
        }

        if ($state !== null) {
            $whereConditions[] = "`psc_state` = '" . mysqli_real_escape_string($conn, $state) . "'";
        }

        $sqlQuery = "SELECT `psc_id`, `psc_curdate`, `psc_opbalance`, `psc_todaysales`,
                    `psc_totdiscount`, `psc_tottax`, `psc_netamt`, `psc_servicetax`,
                    `psc_todayin`, `psc_todayout`, `psc_todaybanking`, `psc_clsbalance`,
                    `psc_state`, `psc_comid`, `psc_locid`, `psc_shiftno`, `psc_dayno`,
                    `psc_pcname`, `psc_totbills`, `psc_cancelamt`, `psc_creditsales`,
                    `psc_opendrawer`, `psc_clsdrawer`, `psc_userid`, `psc_smail`,
                    `psc_print`, `psc_created`, `psc_updated`
                    FROM `pos_shiftclose`
                    WHERE " . implode(' AND ', $whereConditions) . "
                    ORDER BY `psc_created` DESC";

        return mysqli_query($conn, $sqlQuery);
    }

    public function GetCurrentOpenShift($comid, $locid)
    {
        $conn = $this->conn;

        $sqlQuery = "SELECT `psc_id`, `psc_curdate`, `psc_opbalance`, `psc_todaysales`,
                    `psc_totdiscount`, `psc_tottax`, `psc_netamt`, `psc_servicetax`,
                    `psc_todayin`, `psc_todayout`, `psc_todaybanking`, `psc_clsbalance`,
                    `psc_state`, `psc_comid`, `psc_locid`, `psc_shiftno`, `psc_dayno`,
                    `psc_pcname`, `psc_totbills`, `psc_cancelamt`, `psc_creditsales`,
                    `psc_opendrawer`, `psc_clsdrawer`, `psc_userid`, `psc_smail`,
                    `psc_print`, `psc_created`, `psc_updated`
                    FROM `pos_shiftclose`
                    WHERE `psc_comid` = '" . intval($comid) . "'
                    AND `psc_locid` = '" . intval($locid) . "'
                    AND `psc_state` = 'Open'
                    ORDER BY `psc_created` DESC
                    LIMIT 1";

        $result = mysqli_query($conn, $sqlQuery);

        if ($result && mysqli_num_rows($result) > 0) {
            return mysqli_fetch_assoc($result);
        }
        return false;
    }

    public function CloseShiftAndIncrement($pm_id, $comid, $locid)
    {
        $conn = $this->conn;

        // Update PM_SHIFT_NO +1 and PM_SHIFT_ST = "Close" in POS_MASTER
        $updateQuery = "UPDATE `POS_MASTER` SET
                       `PM_SHIFT_NO` = `PM_SHIFT_NO` + 1,
                       `PM_SHIFT_ST` = 'Close',
                       `PM_UPDATED` = NOW()
                       WHERE `PM_ID` = '" . intval($pm_id) . "'
                       AND `PM_COMID` = '" . intval($comid) . "'
                       AND `PM_LOCID` = '" . intval($locid) . "'";

        $result = mysqli_query($conn, $updateQuery);
        return $result && mysqli_affected_rows($conn) > 0;
    }

    public function ValidateShiftBeforeClose($psc_id, $comid, $locid)
    {
        $conn = $this->conn;

        // Check if shift exists and its current state
        $sqlQuery = "SELECT `psc_id`, `psc_state`, `psc_shiftno`
                    FROM `pos_shiftclose`
                    WHERE `psc_id` = '" . intval($psc_id) . "'
                    AND `psc_comid` = '" . intval($comid) . "'
                    AND `psc_locid` = '" . intval($locid) . "'";

        $result = mysqli_query($conn, $sqlQuery);

        if ($result && mysqli_num_rows($result) > 0) {
            $shiftData = mysqli_fetch_assoc($result);

            // Return validation result with status
            return array(
                'exists' => true,
                'state' => $shiftData['psc_state'],
                'shift_no' => $shiftData['psc_shiftno'],
                'can_close' => ($shiftData['psc_state'] === 'Open'),
                'message' => ($shiftData['psc_state'] === 'Close') ?
                    'Shift #' . $shiftData['psc_shiftno'] . ' is already closed' :
                    'Shift #' . $shiftData['psc_shiftno'] . ' is open and can be closed'
            );
        }

        return array(
            'exists' => false,
            'state' => null,
            'shift_no' => null,
            'can_close' => false,
            'message' => 'Shift not found'
        );
    }

    public function ValidateCurrentShiftAndCreate($comid, $locid, $userid, $pcname)
    {
        $conn = $this->conn;

        // Step 1: Get current shift data from POS_MASTER
        $posQuery = "SELECT `PM_ID`, `PM_SHIFT_NO`, `PM_DAY_NO`, `PM_SHIFT_ST`
                    FROM `POS_MASTER`
                    WHERE `PM_COMID` = '" . intval($comid) . "'
                    AND `PM_LOCID` = '" . intval($locid) . "'
                    LIMIT 1";

        $posResult = mysqli_query($conn, $posQuery);

        if (!$posResult || mysqli_num_rows($posResult) == 0) {
            return array(
                'success' => false,
                'message' => 'POS Master record not found',
                'action' => 'none'
            );
        }

        $posData = mysqli_fetch_assoc($posResult);
        $pmId = $posData['PM_ID'];
        $currentShiftNo = intval($posData['PM_SHIFT_NO']);
        $currentDayNo = intval($posData['PM_DAY_NO']);
        $shiftStatus = $posData['PM_SHIFT_ST'];

        // Step 2: Check if current shift is marked as "Open" in POS_MASTER
        if ($shiftStatus !== 'Open') {
            // Shift is marked as closed, check if shift record exists in pos_shiftclose
            $shiftExistsResult = $this->CheckShiftExists($comid, $locid, $currentShiftNo, $currentDayNo);

            if (!$shiftExistsResult['exists']) {
                // Shift doesn't exist, create new shift
                $createResult = $this->CreateNewShift($comid, $locid, $userid, $pcname);

                if ($createResult) {
                    return array(
                        'success' => true,
                        'message' => 'New shift created successfully',
                        'action' => 'created',
                        'shift_data' => $createResult,
                        'shift_no' => $createResult['shift_no'],
                        'day_no' => $createResult['day_no'],
                        'pm_id' => $pmId
                    );
                } else {
                    return array(
                        'success' => false,
                        'message' => 'Failed to create new shift',
                        'action' => 'error'
                    );
                }
            } else {
                // Shift exists but POS_MASTER shows closed - update POS_MASTER to Open
                $updateQuery = "UPDATE `POS_MASTER` SET
                               `PM_SHIFT_ST` = 'Open',
                               `PM_UPDATED` = NOW()
                               WHERE `PM_ID` = '" . intval($pmId) . "'";

                mysqli_query($conn, $updateQuery);

                return array(
                    'success' => true,
                    'message' => 'Shift exists, POS Master updated to Open',
                    'action' => 'updated',
                    'shift_no' => $currentShiftNo,
                    'day_no' => $currentDayNo,
                    'pm_id' => $pmId
                );
            }
        } else {
            // Shift is marked as Open in POS_MASTER, check if shift record exists
            $shiftExistsResult = $this->CheckShiftExists($comid, $locid, $currentShiftNo, $currentDayNo);

            if (!$shiftExistsResult['exists']) {
                // POS_MASTER shows Open but no shift record exists - create shift record
                $createResult = $this->CreateNewShift($comid, $locid, $userid, $pcname);

                if ($createResult) {
                    return array(
                        'success' => true,
                        'message' => 'Shift record created to match POS Master',
                        'action' => 'created',
                        'shift_data' => $createResult,
                        'shift_no' => $createResult['shift_no'],
                        'day_no' => $createResult['day_no'],
                        'pm_id' => $pmId
                    );
                } else {
                    return array(
                        'success' => false,
                        'message' => 'Failed to create shift record',
                        'action' => 'error'
                    );
                }
            } else {
                // Everything is consistent
                return array(
                    'success' => true,
                    'message' => 'Current shift is open and exists',
                    'action' => 'validated',
                    'shift_no' => $currentShiftNo,
                    'day_no' => $currentDayNo,
                    'shift_data' => $shiftExistsResult['data'],
                    'pm_id' => $pmId
                );
            }
        }
    }

    public function CheckShiftExists($comid, $locid, $shiftNo, $dayNo)
    {
        $conn = $this->conn;

        $sqlQuery = "SELECT * FROM `pos_shiftclose`
                    WHERE `psc_comid` = '" . intval($comid) . "'
                    AND `psc_locid` = '" . intval($locid) . "'
                    AND `psc_shiftno` = '" . intval($shiftNo) . "'
                    AND `psc_dayno` = '" . intval($dayNo) . "'
                    LIMIT 1";

        $result = mysqli_query($conn, $sqlQuery);

        if ($result && mysqli_num_rows($result) > 0) {
            $shiftData = mysqli_fetch_assoc($result);
            return array(
                'exists' => true,
                'data' => $shiftData
            );
        }

        return array(
            'exists' => false,
            'data' => null
        );
    }

    // Day Close Management Methods
    public function CreateNewDay($comid, $locid, $psd_opbalance, $psd_shiftno, $psd_dayno, $psd_pcname, $psd_userid)
    {
        $conn = $this->conn;

        try {
            $current_date = date('Y-m-d H:i:s');

            // Get next shift number and day number from POS_MASTER
            $shiftQuery = "SELECT `PM_SHIFT_NO`, `PM_DAY_NO` FROM `POS_MASTER`
                      WHERE `PM_COMID` = '" . intval($comid) . "'
                      AND `PM_LOCID` = '" . intval($locid) . "'
                      LIMIT 1";
            $shiftResult = mysqli_query($conn, $shiftQuery);

            $shiftNo = 1;
            $dayNo = 1;

            if ($shiftResult && mysqli_num_rows($shiftResult) > 0) {
                $shiftData = mysqli_fetch_assoc($shiftResult);
                $shiftNo = intval($shiftData['PM_SHIFT_NO']);
                $dayNo = intval($shiftData['PM_DAY_NO']);
            }

            $sql = "INSERT INTO pos_dayclose (
                psd_curdate, psd_opbalance, psd_shiftno, psd_dayno,
                psd_pcname, psd_userid, psd_state, psd_comid, psd_locid,
                psd_created, psd_updated
            ) VALUES (
                '$current_date', '$psd_opbalance', '$shiftNo', '$dayNo',
                '$psd_pcname', '$psd_userid', 'Open', '$comid', '$locid',
                '$current_date', '$current_date'
            )";

            $result = mysqli_query($conn, $sql);

            if ($result) {
                $new_id = mysqli_insert_id($conn);
                return array(
                    'psd_id' => $new_id,
                    'day_no' => $dayNo,
                    'shift_no' => $shiftNo,
                    'state' => 'Open'
                );
            }

            return false;
        } catch (Exception $e) {
            error_log("CreateNewDay Error: " . $e->getMessage());
            return false;
        }
    }

    public function ValidateDayBeforeClose($psd_id, $comid, $locid)
    {
        $conn = $this->conn;

        $sql = "SELECT psd_id, psd_dayno, psd_state
                FROM pos_dayclose
                WHERE psd_id = '$psd_id' AND psd_comid = '$comid' AND psd_locid = '$locid'";

        $result = mysqli_query($conn, $sql);

        if ($result && mysqli_num_rows($result) > 0) {
            $row = mysqli_fetch_assoc($result);

            if ($row['psd_state'] === 'Close') {
                return array(
                    'exists' => true,
                    'can_close' => false,
                    'state' => $row['psd_state'],
                    'day_no' => $row['psd_dayno'],
                    'message' => 'Day is already closed'
                );
            }

            return array(
                'exists' => true,
                'can_close' => true,
                'state' => $row['psd_state'],
                'day_no' => $row['psd_dayno'],
                'message' => 'Day can be closed'
            );
        }

        return array(
            'exists' => false,
            'can_close' => false,
            'message' => 'Day record not found'
        );
    }

    public function UpdateDayClose($psd_id, $comid, $locid, $dayCloseData)
    {
        $conn = $this->conn;

        try {
            $updateFields = array();
            foreach ($dayCloseData as $field => $value) {
                $updateFields[] = "$field = '$value'";
            }

            $updateFields[] = "psd_updated = '" . date('Y-m-d H:i:s') . "'";

            $sql = "UPDATE pos_dayclose SET " .
                implode(', ', $updateFields) .
                " WHERE psd_id = '$psd_id' AND psd_comid = '$comid' AND psd_locid = '$locid'";

            $result = mysqli_query($conn, $sql);
            return $result;
        } catch (Exception $e) {
            error_log("UpdateDayClose Error: " . $e->getMessage());
            return false;
        }
    }

    public function CloseDayAndIncrement($pm_id, $comid, $locid)
    {
        $conn = $this->conn;

        try {
            // Get current day number
            $getCurrentSql = "SELECT `PM_DAY_NO` FROM `POS_MASTER`
                            WHERE `PM_ID` = '" . intval($pm_id) . "' AND `PM_COMID` = '" . intval($comid) . "' AND `PM_LOCID` = '" . intval($locid) . "'";

            $getCurrentResult = mysqli_query($conn, $getCurrentSql);

            if ($getCurrentResult && mysqli_num_rows($getCurrentResult) > 0) {
                $currentRow = mysqli_fetch_assoc($getCurrentResult);
                $newDayNo = $currentRow['PM_DAY_NO'] + 1;

                // Update POS_MASTER with incremented day number and set day status to Open (for next day)
                $updateSql = "UPDATE `POS_MASTER` SET
                            `PM_DAY_NO` = '" . intval($newDayNo) . "',
                            `PM_DAY_ST` = 'Open'
                            WHERE `PM_ID` = '" . intval($pm_id) . "' AND `PM_COMID` = '" . intval($comid) . "' AND `PM_LOCID` = '" . intval($locid) . "'";

                $result = mysqli_query($conn, $updateSql);
                return $result;
            }

            return false;
        } catch (Exception $e) {
            error_log("CloseDayAndIncrement Error: " . $e->getMessage());
            return false;
        }
    }

    public function GetCurrentOpenDay($comid, $locid)
    {
        $conn = $this->conn;

        $sql = "SELECT * FROM pos_dayclose
                WHERE psd_comid = '$comid' AND psd_locid = '$locid' AND psd_state = 'Open'
                ORDER BY psd_dayno DESC, psd_created DESC
                LIMIT 1";

        $result = mysqli_query($conn, $sql);

        if ($result && mysqli_num_rows($result) > 0) {
            return mysqli_fetch_assoc($result);
        }

        return null;
    }

    public function GetDayData($comid, $locid, $limit = 50)
    {
        $conn = $this->conn;

        $sql = "SELECT * FROM pos_dayclose
                WHERE psd_comid = '$comid' AND psd_locid = '$locid'
                ORDER BY psd_dayno DESC, psd_created DESC
                LIMIT $limit";

        $result = mysqli_query($conn, $sql);
        return $result;
    }

    public function ValidateCurrentDayAndCreate($comid, $locid, $userid, $pcname)
    {
        $conn = $this->conn;

        try {
            // First check POS_MASTER for current day status and numbers
            $posMasterSql = "SELECT `PM_ID`, `PM_DAY_NO`, `PM_SHIFT_NO`, `PM_DAY_ST`
                           FROM `POS_MASTER`
                           WHERE `PM_COMID` = '" . intval($comid) . "' AND `PM_LOCID` = '" . intval($locid) . "'
                           ORDER BY `PM_ID` DESC LIMIT 1";

            $posMasterResult = mysqli_query($conn, $posMasterSql);

            if (!$posMasterResult || mysqli_num_rows($posMasterResult) == 0) {
                return array(
                    'success' => false,
                    'action' => 'error',
                    'message' => 'No POS Master record found'
                );
            }

            $posMasterRow = mysqli_fetch_assoc($posMasterResult);
            $currentDayNo = $posMasterRow['PM_DAY_NO'];
            $currentShiftNo = $posMasterRow['PM_SHIFT_NO'];
            $dayStatus = $posMasterRow['PM_DAY_ST'];
            $pmId = $posMasterRow['PM_ID'];

            // Check if day record exists in pos_dayclose table
            $dayExistsSql = "SELECT `psd_id`, `psd_dayno`, `psd_shiftno`, `psd_state`
                           FROM `pos_dayclose`
                           WHERE `psd_comid` = '" . intval($comid) . "' AND `psd_locid` = '" . intval($locid) . "' AND `psd_dayno` = '" . intval($currentDayNo) . "'
                           ORDER BY `psd_id` DESC LIMIT 1";

            $dayExistsResult = mysqli_query($conn, $dayExistsSql);

            if ($dayExistsResult && mysqli_num_rows($dayExistsResult) > 0) {
                // Day record exists
                $dayRow = mysqli_fetch_assoc($dayExistsResult);

                // Normalize day status values (handle case sensitivity)
                $dayStatus = trim(strtolower($dayStatus));
                $recordStatus = trim(strtolower($dayRow['psd_state']));

                if ($dayStatus === 'close') {
                    // POS Master shows day as closed, check day record status
                    if ($recordStatus === 'close') {
                        // Everything is properly closed, validation successful
                        return array(
                            'success' => true,
                            'action' => 'validated',
                            'message' => 'Day validation successful - all properly closed',
                            'day_no' => $currentDayNo,
                            'shift_no' => $currentShiftNo,
                            'pm_id' => $pmId
                        );
                    } else {
                        // Day record should be closed but isn't - fix the inconsistency
                        // Close the day record to match POS Master status
                        $closeDayRecordSql = "UPDATE `pos_dayclose` SET `psd_state` = 'Close', `psd_updated` = NOW()
                                            WHERE `psd_id` = '" . intval($dayRow['psd_id']) . "'
                                            AND `psd_comid` = '" . intval($comid) . "'
                                            AND `psd_locid` = '" . intval($locid) . "'";

                        mysqli_query($conn, $closeDayRecordSql);

                        return array(
                            'success' => true,
                            'action' => 'synchronized',
                            'message' => 'Day status synchronized - day record closed to match POS Master',
                            'day_no' => $currentDayNo,
                            'shift_no' => $currentShiftNo,
                            'pm_id' => $pmId
                        );
                    }
                } else {
                    // POS Master shows day as open (or other status)
                    if ($recordStatus === 'open') {
                        // Everything is consistent and open
                        return array(
                            'success' => true,
                            'action' => 'validated',
                            'message' => 'Day validation successful - day is open',
                            'day_no' => $currentDayNo,
                            'shift_no' => $currentShiftNo,
                            'pm_id' => $pmId
                        );
                    } else {
                        // Day record is closed but POS Master says open - update day record to open
                        $openDayRecordSql = "UPDATE `pos_dayclose` SET `psd_state` = 'Open', `psd_updated` = NOW()
                                           WHERE `psd_id` = '" . intval($dayRow['psd_id']) . "'
                                           AND `psd_comid` = '" . intval($comid) . "'
                                           AND `psd_locid` = '" . intval($locid) . "'";

                        mysqli_query($conn, $openDayRecordSql);

                        return array(
                            'success' => true,
                            'action' => 'synchronized',
                            'message' => 'Day status synchronized - day record opened to match POS Master',
                            'day_no' => $currentDayNo,
                            'shift_no' => $currentShiftNo,
                            'pm_id' => $pmId
                        );
                    }
                }
            } else {
                // No day record exists - create new day record
                $createResult = $this->CreateNewDay($comid, $locid, 0, $currentShiftNo, $currentDayNo, $pcname, $userid);

                if ($createResult) {
                    // Update POS Master to ensure day status is Open
                    $updatePosMasterSql = "UPDATE `POS_MASTER` SET `PM_DAY_ST` = 'Open'
                                         WHERE `PM_ID` = '" . intval($pmId) . "' AND `PM_COMID` = '" . intval($comid) . "' AND `PM_LOCID` = '" . intval($locid) . "'";
                    mysqli_query($conn, $updatePosMasterSql);

                    return array(
                        'success' => true,
                        'action' => 'created',
                        'message' => 'New day created successfully',
                        'day_data' => $createResult,
                        'day_no' => $createResult['day_no'],
                        'shift_no' => $createResult['shift_no'],
                        'pm_id' => $pmId
                    );
                } else {
                    return array(
                        'success' => false,
                        'action' => 'failed',
                        'message' => 'Failed to create new day record'
                    );
                }
            }
        } catch (Exception $e) {
            error_log("ValidateCurrentDayAndCreate Error: " . $e->getMessage());
            return array(
                'success' => false,
                'action' => 'error',
                'message' => 'Database error: ' . $e->getMessage()
            );
        }
    }

    public function CheckDayExists($comid, $locid, $dayno)
    {
        $conn = $this->conn;

        $sql = "SELECT `psd_id`, `psd_dayno`, `psd_state`
                FROM `pos_dayclose`
                WHERE `psd_comid` = '" . intval($comid) . "' AND `psd_locid` = '" . intval($locid) . "' AND `psd_dayno` = '" . intval($dayno) . "'
                ORDER BY `psd_id` DESC LIMIT 1";

        $result = mysqli_query($conn, $sql);

        if ($result && mysqli_num_rows($result) > 0) {
            $dayData = mysqli_fetch_assoc($result);
            return array(
                'exists' => true,
                'data' => $dayData
            );
        }

        return array(
            'exists' => false,
            'data' => null
        );
    }
}
