<?php

//include_once 'conn.php';
class database
{
    /*
      public $host = "localhost";
      public $user = "root";
      public $pass = "";
      public $db = "myposacct";
      public $host = "localhost";
      public $user = "myposqrc_accts";
      public $pass = "Ruthram@1986";
      public $db = "myposqrc_accts";

      public $host = "localhost";
      public $user = "root";
      public $pass = "";
      public $db = "myposacct";
      public $result;
      public $conn;
      public $login_user;
      public $ip;
      public $browser;
      public $logo;
      public $main_logo;
      public $site_name;
      public $sort_name;
      public $address;
      public $phone;
      public $email;
      public $msg; */

    public $login_user;
    public $login_userid;
    public $title = 'Mypos Portal';
    public $Version = 'Admin Ver 1.0';
    private $conn;


    // constructor
    public function __construct()
    {
        require_once 'Config.php';
        date_default_timezone_set('Asia/Kuala_Lumpur');
        // Database constants are now loaded from Config.php
        // Connection will be established when connect() is called
    }

    public function connect()
    {
        $conn = mysqli_connect(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);
        if ($conn) {
            $this->conn = $conn;
        }
        return $conn;
    }

    // destructor
    function __destruct() {}

    public function date()
    {
        return $this->get_now_time();
    }

    public function get_now_time()
    {


        $now = date("Y-m-d H:i:s", time());
        return $now;
    }

    public function set_login_user($username, $uid)
    {
        $this->login_user = $username;
        $this->login_userid = $uid;
        //$this->ip=$ip;
        // $this->browser=$browser;
    }

    public function select($query)
    {
        return $this->result = mysqli_query($this->conn, $query);
    }

    //User Log
    public function _SelectUserLog()
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT usr.rid,cs.customername,usr.username,tul.time FROM `tb_user_log` as tul INNER JOIN `users` as usr on tul.userid=usr.id INNER JOIN `customermaster` as cs on cs.customerId =usr.rid WHERE usr.rid <> 1  ORDER BY tul.time DESC");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _login($user, $pass)
    {
        $conn = $this->connect();
        $sqlQuery = ("SELECT * FROM `users` WHERE  `username`= '" . $user . "' and  `password`= '" . $pass . "' and `status`='1'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _InsertUserLog($id, $rid)
    {
        $conn = $this->connect();
        $sqlSelect = ("INSERT INTO `tb_user_log`(`userid`,`rid`) VALUES('" . $id . "','" . $rid . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _tablegroupmaster()
    {
        $conn = $this->connect();
        $sqlQuery = ("SELECT tma_group_value FROM tb_master_all WHERE tma_group_code='011'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _tablegroupmasterbycode()
    {
        $conn = $this->connect();
        $sqlQuery = ("SELECT tma_group_id,tma_group_value FROM tb_master_all WHERE tma_group_code=012");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _loginUSer($user)
    {
        $conn = $this->connect();
        $sqlQuery = ("SELECT * FROM `users` WHERE `username`='" . $user . "' and `status`='1'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _userwiseform()
    {
        $conn = $this->connect();
        $sqlQuery = ("SELECT `tb_usergroup_id`,`tb_usergroup_name` FROM `tb_usergroup_master` WHERE 1");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    //Product Save
    public function _SelectProductMaster()
    {
        $conn = $this->connect();
        $sqlQuery = ("SELECT * FROM `di_item_mast` WHERE 1");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _SelectProductBySalesScreen($comid)
    {
        $conn = $this->connect();
        $sqlQuery = ("SELECT dim.dim_item_id,dim.dim_item_barcode,dim_item_name,dmg.mainname,dcm.dcm_name,tx.taxname,dim.dim_sell_price,dcs.customerName,dbm.branchname,dim.dim_status FROM `di_item_mast`as dim INNER JOIN `di_main_group` as dmg ON dim.dim_main_id=dmg.mainid INNER JOIN `di_category_master` as dcm ON dim.dim_cate_id=dcm.dcm_id INNER JOIN `taxmaster` as tx ON dim.dim_tax_id=tx.taxid INNER JOIN `customermaster` as dcs ON dim.dim_com_id=dcs.customerId INNER JOIN `di_branch_mast` as dbm ON dim.dim_loc_id=dbm.branchid WHERE dim.dim_com_id='" . $comid . "'");
        $result = mysqli_query($conn, $sqlQuery);
        while ($row = mysqli_fetch_row($result)) {
            $rest .= '<option value=' . $row[0] . '>';
            $rest .= $row[2];
            $rest .= '</option>';
        }

        return $rest;
    }

    public function _SelectProductMasterByID($id)
    {
        $conn = $this->connect();
        $sqlQuery = ("SELECT * FROM `di_item_mast` WHERE dim_item_id='" . $id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return mysqli_fetch_assoc($result);
    }

    public function _SelectProductJoin()
    {
        $conn = $this->connect();
        $sqlQuery = ("SELECT dim.dim_item_id,dim.dim_item_barcode,dim_item_name,dmg.mainname,dcm.dcm_name,tx.taxname,dim.dim_sell_price,dcs.customerName,dbm.branchname,dim.dim_status FROM `di_item_mast`as dim INNER JOIN `di_main_group` as dmg ON dim.dim_main_id=dmg.mainid INNER JOIN `di_category_master` as dcm ON dim.dim_cate_id=dcm.dcm_id INNER JOIN `taxmaster` as tx ON dim.dim_tax_id=tx.taxid INNER JOIN `customermaster` as dcs ON dim.dim_com_id=dcs.customerId INNER JOIN `di_branch_mast` as dbm ON dim.dim_loc_id=dbm.branchid WHERE 1;");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _SelectProductJoinByID($id)
    {
        $conn = $this->connect();
        $sqlQuery = ("SELECT * FROM `di_item_mast`as dim INNER JOIN `di_main_group` as dmg ON dim.dim_main_id=dmg.mainid INNER JOIN `di_category_master` as dcm ON dim.dim_cate_id=dcm.dcm_id INNER JOIN `taxmaster` as tx ON dim.dim_tax_id=tx.taxid INNER JOIN `customermaster` as dcs ON dim.dim_com_id=dcs.customerId INNER JOIN `di_branch_mast` as dbm ON dim.dim_loc_id=dbm.branchid WHERE dim.dim_item_id='" . $id . "'");
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
        $dim_op_stock,
        $dim_stock_in,
        $dim_stock_out,
        $dim_stock_cur,
        $dim_com_id,
        $dim_loc_id,
        $dim_status
    ) {
        $conn = $this->connect();
        $sqlQuery = ("INSERT INTO `di_item_mast`( `dim_item_barcode`, `dim_item_name`, `dim_business_type`, `dim_main_id`, `dim_cate_id`, `dim_tax_id`, `dim_cost_price`"
            . ", `dim_sell_price`, `dim_op_stock`, `dim_stock_in`, `dim_stock_out`, `dim_stock_cur`, `dim_com_id`, `dim_loc_id`, `dim_status`) VALUES "
            . " ('" . $dim_item_barcode . "','" . $dim_item_name . "','" . $dim_business_type . "','" . $dim_main_id . "','" . $dim_cate_id . "','" . $dim_tax_id . "','" . $dim_cost_price . "'"
            . ",'" . $dim_sell_price . "','" . $dim_op_stock . "','" . $dim_stock_in . "','" . $dim_stock_out . "','" . $dim_stock_cur . "','" . $dim_com_id . "','" . $dim_loc_id . "','" . $dim_status . "')");
        $result = mysqli_query($conn, $sqlQuery);
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
        $dim_op_stock,
        $dim_stock_in,
        $dim_stock_out,
        $dim_stock_cur,
        $dim_com_id,
        $dim_loc_id,
        $dim_status
    ) {
        $conn = $this->connect();
        $sqlQuery = ("UPDATE `di_item_mast` SET `dim_item_barcode`='" . $dim_item_barcode . "',`dim_item_name`='" . $dim_item_name . "',`dim_business_type`='" . $dim_business_type . "'"
            . ",`dim_main_id`='" . $dim_main_id . "',`dim_cate_id`='" . $dim_cate_id . "',`dim_tax_id`='" . $dim_tax_id . "',`dim_cost_price`='" . $dim_cost_price . "',`dim_sell_price`='" . $dim_sell_price . "'"
            . ",`dim_op_stock`='" . $dim_op_stock . "',`dim_stock_in`='" . $dim_stock_in . "',`dim_stock_out`='" . $dim_stock_out . "',`dim_stock_cur`='" . $dim_stock_cur . "',`dim_com_id`='" . $dim_com_id . "'"
            . ",`dim_loc_id`='" . $dim_loc_id . "',`dim_status`='" . $dim_status . "' WHERE `dim_item_id`='" . $dim_item_id . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    //Branch Save
    public function _selectbranchbycompany2()
    {
        $conn = $this->connect();
        $sqlQuery = ("SELECT customerId,customerName FROM `customermaster`");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _selectbranchbycompany3()
    {
        $conn = $this->connect();
        $sqlQuery = ("SELECT branchid,branchname FROM `di_branch_mast`WHERE 1");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _branchSave($branchcustomerid, $branchname, $branchaddress, $branchemail, $branchcontact, $branchanydesk, $branchserver, $branchclient, $branchtab, $branchlock, $branchactivationcode, $branchstatus, $branchmessage)
    {
        $conn = $this->connect();
        $sqlQuery = ("INSERT INTO `di_branch_mast`(`branchcustomerid`, `branchname`, `branchaddress`, `branchemail`, `branchcontact`,`branchanydesk`, `branchserver`, `branchclient`, `branchtab`, `branchlock`, `branchactivationcode`, `branchmessage`,`branchstatus`) VALUES"
            . "('" . $branchcustomerid . "','" . $branchname . "','" . $branchaddress . "','" . $branchemail . "','" . $branchcontact . "','" . $branchanydesk . "','" . $branchserver . "','" . $branchclient . "','" . $branchtab . "','" . $branchlock . "','" . $branchactivationcode . "','" . $branchmessage . "','" . $branchstatus . "')");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _branchselectById($id)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT dbm.branchid,cm.customerId,cm.customerName,dbm.branchname,dbm.branchaddress,dbm.branchemail,dbm.branchcontact,dbm.branchanydesk,dbm.branchserver,dbm.branchclient,dbm.branchtab,dbm.branchlock,dbm.branchactivationcode,dbm.branchstatus,dbm.branchmessage FROM `di_branch_mast` as dbm INNER join `customermaster` as cm on dbm.branchcustomerid=cm.customerId WHERE dbm.branchid='" . $id . "'");
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return mysqli_fetch_assoc($result);
    }

    public function _branchselectByWebId($id)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT dbm.branchid,cm.customerId,cm.customerName,dbm.branchname,dbm.branchaddress,dbm.branchemail,dbm.branchcontact,dbm.branchanydesk,dbm.branchserver,dbm.branchclient,dbm.branchtab,dbm.branchlock,dbm.branchactivationcode,dbm.branchstatus,dbm.branchmessage FROM `di_branch_mast` as dbm INNER join `customermaster` as cm on dbm.branchcustomerid=cm.customerId WHERE dbm.branchid='" . $id . "'");
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function _branchselectActiveByWebId($id)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT `dba_id`, `dba_bid`, `dba_systemname`, `dba_systemtype`, `dba_licentype`, `dba_registerdate`, `dba_hddid`, `dba_countername`, `dba_orderno`, `dba_active`, `dba_created`, `dba_modified` FROM `di_branch_activation` WHERE  dba_bid='" . $id . "'");
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function _branchselectByMainId($id)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT branchid,branchname FROM `di_branch_mast` WHERE branchcustomerid='" . $id . "'");
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function mkdirbranch()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT MAX(branchid) FROM `di_branch_mast` WHERE 1;");
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function _branchSelect()
    {
        $conn = $this->connect();
        $sqlQuery = ("SELECT dbm.branchid,cm.customerName,dbm.branchname,dbm.branchaddress,dbm.branchemail,dbm.branchcontact,dbm.branchanydesk,dbm.branchserver,dbm.branchclient,dbm.branchtab,dbm.branchlock,dbm.branchactivationcode,dbm.branchstatus,dbm.branchmessage FROM `di_branch_mast` as dbm INNER join `customermaster` as cm on dbm.branchcustomerid=cm.customerId WHERE 1;");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _branchUpdate($branchid, $branchcustomerid, $branchname, $branchaddress, $branchemail, $branchcontact, $branchanydesk, $branchserver, $branchclient, $branchtab, $branchlock, $branchactivationcode, $branchstatus, $branchmessage)
    {
        $conn = $this->connect();
        $sqlQuery = ("UPDATE `di_branch_mast` SET `branchcustomerid`='" . $branchcustomerid . "', `branchname`='" . $branchname . "', `branchaddress`='" . $branchaddress . "', `branchemail`='" . $branchemail . "', `branchcontact`='" . $branchcontact . "',`branchanydesk`='" . $branchanydesk . "', `branchserver`='" . $branchserver . "', `branchclient`='" . $branchclient . "', `branchtab`='" . $branchtab . "', `branchlock`='" . $branchlock . "', `branchactivationcode`='" . $branchactivationcode . "',`branchmessage`='" . $branchmessage . "', `branchstatus`='" . $branchstatus . "' WHERE `branchid`='" . $branchid . "'");

        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _branchUpdateWeb($branchid, $branchcustomerid, $branchname, $branchaddress, $branchemail, $branchcontact, $branchanydesk, $branchactivationcode, $branchserver, $branchclient, $branchtab)
    {
        $conn = $this->connect();
        $sqlQuery = ("UPDATE `di_branch_mast` SET `branchcustomerid`='" . $branchcustomerid . "', `branchname`='" . $branchname . "', `branchaddress`='" . $branchaddress . "', `branchemail`='" . $branchemail . "', `branchcontact`='" . $branchcontact . "',`branchanydesk`='" . $branchanydesk . "', `branchactivationcode`='" . $branchactivationcode . "',`branchserver`='" . $branchserver . "',`branchclient`='" . $branchclient . "',`branchtab`='" . $branchtab . "' WHERE `branchid`='" . $branchid . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _branchLicenceDetails($dba_id, $dba_bid, $dba_systemname, $dba_systemtype, $dba_licentype, $dba_registerdate, $dba_hddid, $dba_countername, $dba_orderno, $dba_active, $created, $modified)
    {

        $conn = $this->connect();
        if ($dba_id <> '0') {
            $sqlQuery = ("UPDATE `di_branch_activation` SET `dba_bid`='" . $dba_bid . "',`dba_systemname`='" . $dba_systemname . "',`dba_systemtype`='" . $dba_systemtype . "',`dba_licentype`='" . $dba_licentype . "',`dba_registerdate`='" . $dba_registerdate . "',`dba_hddid`='" . $dba_hddid . "',`dba_countername`='" . $dba_countername . "',"
                . "`dba_orderno`='" . $dba_orderno . "',`dba_active`='0',`dba_modified`='" . $modified . "' WHERE `dba_id`='" . $dba_id . "'");
        } else {
            $sqlQuery = ("INSERT INTO `di_branch_activation`(`dba_bid`, `dba_systemname`, `dba_systemtype`, `dba_licentype`, `dba_registerdate`, `dba_hddid`, `dba_countername`, `dba_orderno`, `dba_active`, `dba_created`, `dba_modified`) VALUES "
                . "('" . $dba_bid . "','" . $dba_systemname . "','" . $dba_systemtype . "','" . $dba_licentype . "','" . $dba_registerdate . "','" . $dba_hddid . "','" . $dba_countername . "','" . $dba_orderno . "','" . $dba_active . "','" . $created . "','" . $modified . "')");
        }

        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    //User and group
    public function _RptMasterByGroup()
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT `tma_group_id`, `tma_group_name`, `tma_group_code`, `tma_group_value`, `tma_group_status` FROM `tb_master_all`  ORDER BY `tma_group_name`");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _RptMasterByUser()
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT namast.id,namast.username,namast.password,umast.tb_usergroup_name,namast.rid,namast.status FROM `users` as namast INNER JOIN tb_usergroup_master as umast on namast.tum_id=umast.tb_usergroup_id");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _RequestCmbSelected($Code)
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT  `dbm_name` FROM `di_branch_mast` WHERE `dbm_active`=1 and `dbm_id`=" . $Code . "");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _RptMasterSearchById($Reg_Id)
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT `tma_group_id`, `tma_group_name`, `tma_group_code`, `tma_group_value`, `tma_group_status` FROM `tb_master_all` WHERE `tma_group_id`='" . $Reg_Id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return mysqli_fetch_assoc($result);
    }

    public function _RptMasterByUserGroup()
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT `tb_usergroup_id`, `tb_usergroup_name`, `tb_usergroup_active` FROM `tb_usergroup_master` WHERE 1");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    //Tax Master
    public function _SelectTaxMastrer()
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT `taxid`, `taxname`, `taxvalue`, `taxstatus` FROM `taxmaster` WHERE 1");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _SelectTaxMastrerById($id)
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT `taxid`, `taxname`, `taxvalue`, `taxstatus` FROM `taxmaster` WHERE `taxid`='" . $id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return mysqli_fetch_assoc($result);
    }

    public function _InsertTaxMastrer($taxname, $taxvalue, $taxstatus)
    {
        $conn = $this->connect();
        $sqlSelect = ("INSERT INTO `taxmaster`(`taxname`, `taxvalue`, `taxstatus`) VALUES ('" . $taxname . "','" . $taxvalue . "','" . $taxstatus . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateTaxMastrer($taxid, $taxname, $taxvalue, $taxstatus)
    {
        $conn = $this->connect();
        $sqlSelect = ("UPDATE `taxmaster` SET `taxid`='" . $taxid . "',`taxname`='" . $taxname . "',`taxvalue`='" . $taxvalue . "',`taxstatus`='" . $taxstatus . "' WHERE `taxid`='" . $taxid . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    //MainGroup
    public function _SelectMainMastrer()
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT * FROM `di_main_group` WHERE 1");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _SelectMainMastrerById($id)
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT `mainid`, `mainname`, `mainstatus` FROM `di_main_group` WHERE `mainid`='" . $id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return mysqli_fetch_assoc($result);
    }

    public function _InsertMainMastrer($mainname, $mainstatus)
    {
        $conn = $this->connect();
        $sqlSelect = ("INSERT INTO `di_main_group`(`mainname`, `mainstatus`)VALUES ('" . $mainname . "','" . $mainstatus . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateMainMastrer($mainid, $mainname, $mainstatus)
    {
        $conn = $this->connect();
        $sqlSelect = ("UPDATE `di_main_group` SET `mainid`='" . $mainid . "',`mainname`='" . $mainname . "',`mainstatus`='" . $mainstatus . "' WHERE `mainid`='" . $mainid . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    //SubCateGroup
    public function _SelectCateMastrer()
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT sg.dcm_id,sg.dcm_name,mg.mainname,sg.dcm_active FROM `di_category_master` as sg INNER JOIN `di_main_group` as mg ON sg.di_main_id=mg.mainid WHERE 1");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _SelectCateMastrerById($id)
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT `dcm_id`, `dcm_name`, `di_main_id`, `dcm_active` FROM `di_category_master` WHERE  `dcm_id`='" . $id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return mysqli_fetch_assoc($result);
    }

    public function _SelectCateMastrerByMainId($id)
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT `dcm_id`, `dcm_name` FROM `di_category_master` WHERE  `di_main_id`='" . $id . "' AND `dcm_active`=1");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _InsertCateMastrer($catename, $mainid, $catestatus)
    {
        $conn = $this->connect();
        $sqlSelect = ("INSERT INTO `di_category_master`(`dcm_name`, `di_main_id`, `dcm_active`) VALUES  ('" . $catename . "','.$mainid.','" . $catestatus . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateCateMastrer($cateid, $catename, $mainid, $catestatus)
    {
        $conn = $this->connect();
        $sqlSelect = ("UPDATE `di_category_master` SET `dcm_name`='" . $catename . "',`di_main_id`='" . $mainid . "',`dcm_active`='" . $catestatus . "' WHERE `dcm_id`='" . $cateid . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    //User Group
    public function _RptMasterSearchByUserGrpId($Reg_Id)
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT `tb_usergroup_id`, `tb_usergroup_name`, `tb_usergroup_active` FROM `tb_usergroup_master` WHERE `tb_usergroup_id`='" . $Reg_Id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return mysqli_fetch_assoc($result);
    }

    public function _InsertMasterGroup($tma_group_name, $tma_group_code, $tma_group_value, $tma_group_status)
    {
        $conn = $this->connect();
        $sqlSelect = ("INSERT INTO `tb_master_all`(`tma_group_name`, `tma_group_code`, `tma_group_value`, `tma_group_status`) VALUES ('" . $tma_group_name . "','" . $tma_group_code . "','" . $tma_group_value . "','" . $tma_group_status . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateMasterGroup($tma_group_id, $tma_group_name, $tma_group_code, $tma_group_value, $tma_group_status)
    {
        $conn = $this->connect();
        $sqlSelect = ("UPDATE `tb_master_all` SET `tma_group_name`='" . $tma_group_name . "',`tma_group_code`='" . $tma_group_code . "',`tma_group_value`='" . $tma_group_value . "',`tma_group_status`='" . $tma_group_status . "' WHERE `tma_group_id`='" . $tma_group_id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _InsertMasterUserGroup($tma_group_name, $tma_group_status)
    {
        $conn = $this->connect();
        $sqlSelect = ("INSERT INTO `tb_usergroup_master`(`tb_usergroup_name`, `tb_usergroup_active`) VALUES ('" . $tma_group_name . "','" . $tma_group_status . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateMasterUserGroup($tma_group_id, $tma_group_name, $tma_group_status)
    {
        $conn = $this->connect();
        $sqlSelect = ("UPDATE `tb_usergroup_master` SET `tb_usergroup_name`='" . $tma_group_name . "',`tb_usergroup_active`='" . $tma_group_status . "' WHERE `tb_usergroup_id`='" . $tma_group_id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _RptMasterSearchByUserId($Reg_Id)
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT namast.id,namast.username,namast.password,namast.tum_id,namast.status,namast.rid FROM `users` as namast INNER JOIN tb_usergroup_master as umast on namast.tum_id=umast.tb_usergroup_id where namast.id='" . $Reg_Id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return mysqli_fetch_assoc($result);
    }

    public function _InsertUser($nl_username, $nl_password, $nl_usergroup, $nl_status, $rid)
    {
        $conn = $this->connect();
        $sqlSelect = ("INSERT INTO `users`(`username`, `password`,`tum_id`,`status`,`rid`) VALUES ('" . $nl_username . "','" . md5($nl_password) . "','" . $nl_usergroup . "','" . $nl_status . "','" . $rid . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _CreateUserForm()
    {
        $conn = $this->connect();
        $sqlSelect = ("INSERT INTO tb_user_form(tuf_form_uid,tuf_form_name,tuf_form_code,tuf_form_active) SELECT (SELECT MAX(id) from users),tb_form_name,tb_form_code,'0' FROM tb_form_mast where tb_form_active='1'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateUser($nl_userid, $nl_username, $nl_password, $nl_usergroup, $nl_status, $rid)
    {
        $conn = $this->connect();
        $sqlSelect = ("UPDATE `users` SET `username`='" . $nl_username . "',`password`='" . md5($nl_password) . "',`tum_id`='" . $nl_usergroup . "',`status`='" . $nl_status . "',`rid`='" . $rid . "' WHERE `id`='" . $nl_userid . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _RptGetUserCode($number)
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT `id`, `username` FROM `users` WHERE `tum_id` ='" . $number . "' AND `status`='1'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _RptMasterByUserForm($UserCode)
    {
        $conn = $this->connect();
        $sqlSelect = ("SELECT `tuf_form_Id`, `tuf_form_name`,`tuf_form_uid`,users.username,`tuf_form_code`,`tuf_form_active` FROM `tb_user_form` INNER JOIN `users` ON users.id=tb_user_form.tuf_form_uid WHERE tb_user_form.tuf_form_uid='" . $UserCode . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateFormActive($number)
    {
        $conn = $this->connect();
        $sqlSelect = ("UPDATE `tb_user_form` SET `tuf_form_active`='1'  WHERE `tuf_form_Id`='" . $number . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateFormDeActive($number)
    {
        $conn = $this->connect();
        $sqlSelect = ("UPDATE `tb_user_form` SET `tuf_form_active`='0'  WHERE `tuf_form_Id`='" . $number . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return $result;
    }

    public function _UpdateFileIC($regid, $job_reg_fileic)
    {
        $msg = "";
        $conn = $this->connect();
        $sqlQuery = ("update job_register_mast set job_reg_fileic='" . $job_reg_fileic . "'where job_reg_id='" . $regid . "'");
        if (mysqli_query($conn, $sqlQuery)) {
            $msg = "Successfully Updated";
        } else {
            $msg = "Data Not Saved: " . $sqlQuery . "<br>" . mysqli_error($conn);
        }
        return $msg;
    }

    public function _UpdateFileLIC($regid, $job_reg_filelic)
    {
        $msg = "";
        $conn = $this->connect();
        $sqlQuery = ("update job_register_mast set job_reg_filelic='" . $job_reg_filelic . "'where job_reg_id='" . $regid . "'");
        if (mysqli_query($conn, $sqlQuery)) {
            $msg = "Successfully Updated";
        } else {
            $msg = "Data Not Saved: " . $sqlQuery . "<br>" . mysqli_error($conn);
        }
        return $msg;
    }

    public function _GetServersettings()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT `mailid`, `mailpass`, `mailhost` FROM `serversettings` WHERE `mailactive`='1'");
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //serversettings
    public function _UpdateFileOTH($regid, $job_reg_fileoth)
    {
        $msg = "";
        $conn = $this->connect();
        $sqlQuery = ("update job_register_mast set job_reg_fileoth='" . $job_reg_fileoth . "'where job_reg_id='" . $regid . "'");
        if (mysqli_query($conn, $sqlQuery)) {
            $msg = "Successfully Updated";
        } else {
            $msg = "Data Not Saved: " . $sqlQuery . "<br>" . mysqli_error($conn);
        }
        return $msg;
    }

    public function _Encrpt($str)
    {
        $simple_string = $str;

        // Displaying the original string
        //echo "Original String: " . $simple_string;
        // Storingthe cipher method
        $ciphering = "AES-128-CTR";

        // Using OpenSSl Encryption method
        $iv_length = openssl_cipher_iv_length($ciphering);
        $options = 0;

        // Non-NULL Initialization Vector for encryption
        $encryption_iv = '1234567891011121';

        // Storing the encryption key
        $encryption_key = "ruthram";

        // Using openssl_encrypt() function to encrypt the data
        $encryption = openssl_encrypt($simple_string, $ciphering, $encryption_key, $options, $encryption_iv);

        // Displaying the encrypted string
        //  echo "Encrypted String: " . $encryption . "\n";
        return $encryption;
    }

    public function _Decrpt($str)
    {
        $simple_string = $str;
        $ciphering = "AES-128-CTR";

        // Using OpenSSl Encryption method
        $iv_length = openssl_cipher_iv_length($ciphering);
        $options = 0;
        // Displaying the encrypted string
        // echo "Encrypted String: " . $simple_string . "\n";
        // Non-NULL Initialization Vector for decryption
        $decryption_iv = '1234567891011121';

        // Storing the decryption key
        $decryption_key = "ruthram";

        // Using openssl_decrypt() function to decrypt the data
        $decryption = openssl_decrypt($simple_string, $ciphering, $decryption_key, $options, $decryption_iv);

        // Displaying the decrypted string
        // echo "Decrypted String: " . $decryption;
        return $decryption;
    }

    /* public function set_institute_info(){
      $sql="select * from setting";
      $info=$this->get_sql_array($sql);
      $img="upload/custom_content/";
      $this->site_name=$info[0]['option_value'];
      $this->sort_name=$info[1]['option_value'];
      $this->address=$info[2]['option_value'];
      $this->phone=$info[5]['option_value'];
      $this->email=$info[6]['option_value'];
      $this->logo=$img.$info[4]['option_value'];
      $this->main_logo=$img.$info[3]['option_value'];
      $this->msg="@".$info[1]['option_value'];

      } */

    public function date_to_string($date)
    {
        return date("d M Y h:i:A", strtotime($date));
    }

    //Customer Entry
    public function storeCustomerData($txtCustomerName, $status)
    {
        $conn = $this->connect();
        $sqlquery = ("INSERT INTO `customermaster`(`customerName`,`status`) VALUES ('" . $txtCustomerName . "','" . $status . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updateCustomerData($id, $txtCustomerName, $status)
    {
        $conn = $this->connect();
        $sqlquery = ("UPDATE `customermaster` SET `customerName`='" . $txtCustomerName . "',`status`='" . $status . "' WHERE `customerId`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updateCustomerDataAll($id, $customerName, $email, $address_1, $address_2, $town, $county, $postcode, $phone, $name_ship, $address_1_ship, $address_2_ship, $town_ship, $county_ship, $postcode_ship)
    {
        $conn = $this->connect();
        $sqlquery = ("UPDATE `customermaster` SET `customerName`='" . $customerName . "',`email`='" . $email . "',`address_1`='" . $address_1 . "',`address_2`='" . $address_2 . "',`town`='" . $town . "'"
            . ",`county`='" . $county . "',`postcode`='" . $postcode . "',`phone`='" . $phone . "',`name_ship`='" . $name_ship . "',`address_1_ship`='" . $address_1_ship . "' "
            . ",`address_2_ship`='" . $address_2_ship . "',`town_ship`='" . $town_ship . "',`county_ship`='" . $county_ship . "',`postcode_ship`='" . $postcode_ship . "'"
            . "WHERE `customerId`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectCustomer()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `customermaster` ORDER BY customerName ASC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectBranch()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `di_branch_mast`");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectCustomerById($id)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `customermaster` WHERE `customerId`=" . $id);
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return mysqli_fetch_assoc($result);
    }

    public function selectCustomerByEdit($id)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `di_branch_mast` WHERE `branchid`=" . $id);
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //Supplier Entry
    public function storesupplierData($supplierName, $status)
    {
        $conn = $this->connect();
        $sqlquery = ("INSERT INTO `suppliermaster`(`supplierName`, `status`) VALUES ('" . $supplierName . "','" . $status . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updatesupplierData($id, $supplierName, $supplierEmail, $supplierMobile)
    {
        $conn = $this->connect();
        $sqlquery = ("UPDATE `suppliermaster` SET `supplierName`='" . $supplierName . "',`supplierEmail`='" . $supplierEmail . "',`supplierMobile`='" . $supplierMobile . "' WHERE `supplierId`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectsupplier()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `suppliermaster`");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectsupplierById($id)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `suppliermaster` WHERE `supplierId`=" . $id);
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return mysqli_fetch_assoc($result);
    }

    //Parent Entry
    public function storeparentData($parentName)
    {
        $conn = $this->connect();
        $sqlquery = ("INSERT INTO `parentmaster`(`parentName`) VALUES ('" . $parentName . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updateparentData($id, $parentName)
    {
        $conn = $this->connect();
        $sqlquery = ("UPDATE `parentmaster` SET `parentName`='" . $parentName . "' WHERE `parentId`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectparent()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `parentmaster`");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectparentById($id)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `parentmaster` WHERE `parentId`=" . $id);
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return mysqli_fetch_assoc($result);
    }

    //Ledger Group Entry
    public function storegroupData($groupName)
    {
        $conn = $this->connect();
        $sqlquery = ("INSERT INTO `groupmaster`(`groupName`) VALUES ('" . $groupName . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updategroupData($id, $groupName)
    {
        $conn = $this->connect();
        $sqlquery = ("UPDATE `groupmaster` SET `groupName`='" . $groupName . "' WHERE `groupId`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectgroup()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `groupmaster`");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectgroupById($id)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `groupmaster` WHERE `groupId`=" . $id);
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return mysqli_fetch_assoc($result);
    }

    public function parentList()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `parentmaster` WHERE 1");
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function groupList()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `groupmaster` WHERE 1");
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function bankList()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT `ledgermaster`.`ledgerId`,`ledgermaster`.`ledgerName` FROM `groupmaster` inner join `ledgermaster` on groupmaster.groupId =ledgermaster.ledgergroupId WHERE groupmaster.groupType='B'");
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //Ledger Entry


    public function storeLedgerData($ledgerrefId, $ledgerName, $ledgerparenId, $ledgergroupId, $ledgeropenDate, $ledgeropenbal, $ledgerdrcr, $ledgerActive)
    {
        $conn = $this->connect();
        $sqlquery = (" INSERT INTO `ledgermaster`(`ledgerrefId`, `ledgerName`, `ledgerparenId`, `ledgergroupId`, `ledgeropenDate`, `ledgeropenbal`, `ledgerdrcr`, `ledgerActive`) VALUES"
            . "(" . $ledgerrefId . ",'" . $ledgerName . "'," . $ledgerparenId . "," . $ledgergroupId . ",'" . $ledgeropenDate . "'," . $ledgeropenbal . ",'" . $ledgerdrcr . "','" . $ledgerActive . "')");
        //return $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function storeCustomerLedgerData($customer)
    {
        $conn = $this->connect();
        $sqlCustomer = ("SELECT branchid FROM `di_branch_mast` WHERE `branchname`='" . $customer . "'");
        $resCutomer = mysqli_query($conn, $sqlCustomer);
        $rowCusId = (mysqli_fetch_assoc($resCutomer));
        $rowCusIds = $rowCusId;
        $sqlquery = (" INSERT INTO `ledgermaster`(`ledgerrefId`, `ledgerName`, `ledgerparenId`, `ledgergroupId`, `ledgeropenDate`, `ledgeropenbal`, `ledgerdrcr`, `ledgerActive`) VALUES"
            . "(" . $rowCusIds['branchid'] . ",'" . $customer . "',1,4,'" . date("Y/m/d") . "',0.00,'Dr','Active')");
        //  print_r($rowCusIds);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updateCustomerLedgerData($ledgerrefId, $ledgerName)
    {
        $conn = $this->connect();
        $sqlCheck = ("SELECT count(*) as counts FROM `ledgermaster` WHERE `ledgerrefId`='" . $ledgerrefId . "'");
        $resultCheck = mysqli_query($conn, $sqlCheck);
        $count = (mysqli_fetch_assoc($resultCheck));
        $rowCusIds = $count;
        if ($rowCusIds['counts'] == 0) {
            $sqlquery = (" INSERT INTO `ledgermaster`(`ledgerrefId`, `ledgerName`, `ledgerparenId`, `ledgergroupId`, `ledgeropenDate`, `ledgeropenbal`, `ledgerdrcr`, `ledgerActive`) VALUES"
                . "('" . $ledgerrefId . "','" . $ledgerName . "',1,4,'" . date("Y/m/d") . "',0.00,'Dr','Active')");
            //  print_r($rowCusIds);
            $result = mysqli_query($conn, $sqlquery);
        } else {
            $sqlquery = ("UPDATE `ledgermaster` SET `ledgerName`='" . $ledgerName . "' WHERE `ledgerrefId`=" . $ledgerrefId . "");
            // echo $sqlquery;
            $result = mysqli_query($conn, $sqlquery);
        }
        return $result;
    }

    public function storeSupplierLedgerData($customer)
    {
        $conn = $this->connect();
        $sqlCustomer = ("SELECT supplierId FROM `suppliermaster` WHERE `supplierName`='" . $customer . "'");
        $resCutomer = mysqli_query($conn, $sqlCustomer);
        $rowCusId = (mysqli_fetch_assoc($resCutomer));
        $rowCusIds = $rowCusId;
        $sqlquery = (" INSERT INTO `ledgermaster`(`ledgerrefId`, `ledgerName`, `ledgerparenId`, `ledgergroupId`, `ledgeropenDate`, `ledgeropenbal`, `ledgerdrcr`, `ledgerActive`) VALUES"
            . "(" . $rowCusIds['supplierId'] . ",'" . $customer . "',2,3,'" . date("Y/m/d") . "',0.00,'Cr','Active')");
        //  print_r($rowCusIds);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function updateLedgerData($ledgerId, $ledgerrefId, $ledgerName, $ledgerparenId, $ledgergroupId, $ledgeropenDate, $ledgeropenbal, $ledgerdrcr, $ledgerActive)
    {
        $conn = $this->connect();
        $sqlquery = ("UPDATE `ledgermaster` SET `ledgerrefId`=" . $ledgerrefId . ""
            . ",`ledgerName`='" . $ledgerName . "',`ledgerparenId`=" . $ledgerparenId . ",`ledgergroupId`=" . $ledgergroupId . ""
            . ",`ledgeropenDate`='" . $ledgeropenDate . "',`ledgeropenbal`=" . $ledgeropenbal . ",`ledgerdrcr`='" . $ledgerdrcr . "'"
            . ",`ledgerActive`='" . $ledgerActive . "' WHERE `ledgerId`=" . $ledgerId . "");
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectledger()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT lg.ledgerId,lg.ledgerName,gpm.groupName,pm.parentName,lg.ledgerdrcr,lg.ledgerActive FROM `ledgermaster` lg INNER JOIN `groupmaster` gpm ON lg.ledgergroupId=gpm.groupId INNER JOIN `parentmaster` pm ON lg.ledgerparenId=pm.parentID WHERE lg.ledgerActive='Active' ORDER BY ledgerName ASC;");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectledgerall()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `ledgermaster` WHERE `ledgergroupId` <> 1 AND `ledgerActive` ='Active' ORDER BY ledgerName ASC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectledgerById($id)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `ledgermaster` INNER JOIN  parentmaster ON parentmaster.`parentID`=ledgermaster.`ledgerparenId` INNER JOIN groupmaster ON groupmaster.`groupId`=ledgermaster.`ledgergroupId` WHERE ledgermaster.`ledgerId`=" . $id);
        // echo $sqlquery;
        $result = mysqli_query($conn, $sqlquery);
        return mysqli_fetch_assoc($result);
    }

    public function selectMaxVoucherNo()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `invoiceautono` WHERE `autoname`='pay'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectMaxBillNo($BillType)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `invoiceautono` WHERE `autoname`='" . $BillType . "'");
        $result = mysqli_query($conn, $sqlquery);
        while ($row = mysqli_fetch_row($result)) {
            $Maxno = $row[1];
        }
        return $Maxno;
    }

    public function updateVoucherNo($autoname)
    {
        $conn = $this->connect();
        $sqlquery = ("UPDATE `invoiceautono` SET `autono`=`autono` + 1 WHERE `autoname`='" . $autoname . "'"); //SALES//pay
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //journal Entry
    public function DeleteJournalEntry($billno)
    {
        $conn = $this->connect();
        $sqlquery = ("DELETE FROM `journaldetails` WHERE `billno`=" . $billno); //SALES//pay
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function storeJournalpayments($cmbledgername, $txtvoucheramount, $drCrMode, $txtvoucherno, $txtdatepicker, $cmbactype, $cmbmodename, $txtnarration, $cmbbankname, $txtchqdate, $txtchqno, $txtchqamount, $txthidden)
    {
        $conn = $this->connect();
        $sqlQuery1 = ("SELECT `ledgerName` FROM `ledgermaster` WHERE `ledgerid`=" . $cmbactype);
        $result1 = mysqli_query($conn, $sqlQuery1);
        $row1 = mysqli_fetch_assoc($result1);
        $ledgerName1 = $row1;

        $sqlQuery3 = ("SELECT `ledgerName` FROM `ledgermaster` WHERE `ledgerid`=" . $cmbledgername);
        $result3 = mysqli_query($conn, $sqlQuery3);
        $row3 = mysqli_fetch_assoc($result3);
        $ledgerName3 = $row3;
        $status = "A";
        if ($cmbmodename == 'CQ') {
            $status = 'I';
        }
        $sqlquery4 = ("INSERT INTO `journaldetails`(`ledgerid`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`)"
            . "VALUES ('" . $cmbledgername . "','" . $ledgerName3['ledgerName'] . "'"
            . ",$txtvoucheramount,0.00,'Cr',$txtvoucherno,'" . $txtdatepicker . "','" . date('Y/m/d') . "','PAY','" . $cmbmodename . "','" . $txtnarration . "',0.00,'" . $status . "','" . $txthidden . "','" . $ledgerName1['ledgerName'] . "','" . $cmbactype . "')");
        $result = mysqli_query($conn, $sqlquery4);
        $sqlquery2 = ("INSERT INTO `journaldetails`(`ledgerid`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`)"
            . "VALUES ('" . $cmbactype . "','" . $ledgerName1['ledgerName'] . "'"
            . ",0.00,$txtvoucheramount,'Dr',$txtvoucherno,'" . $txtdatepicker . "','" . date('Y/m/d') . "','PAY','" . $cmbmodename . "','" . $txtnarration . "',0.00,'" . $status . "','" . $txthidden . "','" . $ledgerName3['ledgerName'] . "','" . $cmbledgername . "')");
        $result = mysqli_query($conn, $sqlquery2);

        if ($cmbmodename == 'CQ') {
            $conn = $this->connect();
            $sqlMode = ("INSERT INTO `chequedetails`(`jorunalrefid`, `ledgerid`, `bankname`, `chequeamt`, `chequedate`,`chequeno`, `chequestatus`)"
                . " VALUES ($txtvoucherno,$cmbledgername,'" . $cmbbankname . "',$txtchqamount,'" . $txtchqdate . "','" . $txtchqno . "','I')");
            $result = mysqli_query($conn, $sqlMode);
        }
        $this->updateVoucherNo('PAY');
        return $result;
    }

    public function storeJournalreceipt($cmbledgername, $txtvoucheramount, $drCrMode, $txtvoucherno, $txtdatepicker, $cmbactype, $cmbmodename, $txtnarration, $cmbbankname, $txtchqdate, $txtchqno, $txtchqamount, $txthidden)
    {
        $conn = $this->connect();
        $status = "A";
        $sqlQuery3 = ("SELECT `ledgerName` FROM `ledgermaster` WHERE `ledgerid`=" . $cmbledgername);
        $result3 = mysqli_query($conn, $sqlQuery3);
        $row3 = mysqli_fetch_assoc($result3);
        $ledgerName3 = $row3;

        $sqlQuery1 = ("SELECT `ledgerName` FROM `ledgermaster` WHERE `ledgerid`=" . $cmbactype);
        $result1 = mysqli_query($conn, $sqlQuery1);
        $row1 = mysqli_fetch_assoc($result1);
        $ledgerName1 = $row1;
        if ($cmbmodename == 'CQ') {
            $status = 'I';
        }
        $sqlquery2 = ("INSERT INTO `journaldetails`(`ledgerid`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`)"
            . "VALUES ('" . $cmbactype . "','" . $ledgerName1['ledgerName'] . "'"
            . ",$txtvoucheramount,0.00,'Dr',$txtvoucherno,'" . $txtdatepicker . "','" . date('Y/m/d') . "','REC','" . $cmbmodename . "','" . $txtnarration . "',0.00,'" . $status . "','" . $txthidden . "','" . $ledgerName3['ledgerName'] . "','" . $cmbledgername . "')");
        $result = mysqli_query($conn, $sqlquery2);

        $sqlquery4 = ("INSERT INTO `journaldetails`(`ledgerid`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`)"
            . "VALUES ('" . $cmbledgername . "','" . $ledgerName3['ledgerName'] . "'"
            . ",0.00,$txtvoucheramount,'Cr',$txtvoucherno,'" . $txtdatepicker . "','" . date('Y/m/d') . "','REC','" . $cmbmodename . "','" . $txtnarration . "',0.00,'" . $status . "','" . $txthidden . "','" . $ledgerName1['ledgerName'] . "','" . $cmbactype . "')");
        $result = mysqli_query($conn, $sqlquery4);

        if ($cmbmodename == 'CQ') {
            $conn = $this->connect();
            $sqlMode = ("INSERT INTO `chequedetails`(`jorunalrefid`, `ledgerid`, `bankname`, `chequeamt`, `chequedate`,`chequeno`, `chequestatus`)"
                . " VALUES ($txtvoucherno,$cmbledgername,'" . $cmbbankname . "',$txtchqamount,'" . $txtchqdate . "','" . $txtchqno . "','I')");
            $result = mysqli_query($conn, $sqlMode);
        }
        $this->updateVoucherNo('PAY');
        return $result;
    }

    public function UpdateChequeClear($ledgerid, $date, $user, $narration, $status)
    {
        $conn = $this->connect();
        $sqlquery = ("UPDATE `chequedetails` SET `chequestatus`='" . $status . "',`chequepassdate`='" . $date . "',`narrations`='" . $narration . "' WHERE `jorunalrefid`='" . $ledgerid . "'");
        $result1 = mysqli_query($conn, $sqlquery);
        $sqlquery2 = ("UPDATE `journaldetails` SET  `narration`='" . $narration . "',`status`='" . $status . "',`username`='" . $user . "' WHERE `billno`='" . $ledgerid . "'");
        $result2 = mysqli_query($conn, $sqlquery2);
        return $result2;
    }

    public function printpaymentspdf($id, $paymode, $drcr)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `journaldetails` WHERE `billno` =" . $id . " AND `actype`='" . $paymode . "' AND `jstatus`='" . $drcr . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function paymentrpt()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `journaldetails` WHERE  `actype`='PAY' AND jstatus='Cr' AND `status`='A' ORDER BY `billno` DESC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function chequedetails($billno)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `chequedetails` WHERE `jorunalrefid`=" . $billno);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function chequedetailsWithJournal()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT cq.jorunalrefid,js.description,cq.chequeamt,0.00,js.actype,cq.bankname,cq.chequedate,js.actype,js.entrydate,js.jstatus,js.modetype,cq.chequeno,cq.chequestatus,cq.chequepassdate, cq.narrations FROM `chequedetails` as cq INNER JOIN `journaldetails` as js ON js.billno =cq.jorunalrefid WHERE js.modetype='CQ' AND js.jstatus='Cr' GROUP BY js.description,js.actype,js.entrydate,cq.jorunalrefid,cq.chequeamt,cq.bankname,cq.chequedate,cq.chequeno,cq.chequestatus,cq.chequepassdate,cq.narrations ");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function receiptrpt()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `journaldetails` WHERE  `actype`='REC' AND jstatus='Dr' AND `status`='A' ORDER BY `billno` DESC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function delete($id)
    {
        $conn = $this->connect();
        $sqlquery = ("UPDATE  `journaldetails` SET `status`='D' WHERE  billno=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        if ($result) {
            $sqlquery1 = ("UPDATE  `chequedetails` SET `chequestatus`='D' WHERE  `jorunalrefid`=" . $id);
            $result1 = mysqli_query($conn, $sqlquery1);
        }
        return $result1;
    }

    public function dashboard($date, $dateto)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT description as lg, sum(Dr)as dr,sum(cr) as cr,username,narration,actype,entrydate,jstatus,description2 FROM `journaldetails` WHERE `modetype`='CA' AND `jstatus`='Dr' AND ledgerid = 1 AND `status`='A' AND `entrydate` between '" . $date . "' AND '" . $dateto . "' GROUP BY description,username,narration,actype,entrydate,description2 UNION ALL SELECT description as lg, sum(Dr)as dr,sum(cr) as cr,username,narration,actype,entrydate,jstatus,description2 FROM `journaldetails` WHERE `modetype`='CQ' AND `jstatus`='Dr' AND `status`='A' AND `entrydate` between '" . $date . "' AND '" . $dateto . "' GROUP BY description,username,narration,actype,entrydate,description2 ORDER BY entrydate DESC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function dashboardcqInActive($date, $dateto)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT  js.description,sum(cr) as cr,sum(Dr)as dr,js.actype,cq.bankname,cq.chequedate,js.actype,js.entrydate,js.jstatus,js.modetype,cq.chequeno,cq.chequestatus FROM `chequedetails` as cq INNER JOIN `journaldetails` as js ON js.billno =cq.jorunalrefid WHERE js.modetype='CQ' AND js.jstatus='Cr' AND js.status='I'  AND js.entrydate between '" . $date . "' AND '" . $dateto . "'  GROUP BY js.description,js.actype,js.entrydate,js.jstatus,js.modetype,cq.chequeno,cq.chequestatus,cq.bankname,cq.chequedate");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function ledgerrpt($dates, $datesto, $ledger)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT  description, sum(Dr),sum(cr) ,username,narration,actype,entrydate,jstatus,status FROM `journaldetails` WHERE  ledgerid ='" . $ledger . "' AND `entrydate` between '" . $dates . "' AND '" . $datesto . "' AND `modetype`='CA'  AND `status`='A'  GROUP BY description,username,narration,actype,entrydate,jstatus,status UNION ALL SELECT  description, sum(cr),sum(dr) ,username,narration,actype,entrydate,jstatus,status FROM `journaldetails` WHERE  ledgerid ='" . $ledger . "' AND `entrydate` between '" . $dates . "' AND '" . $datesto . "' AND `modetype`='CR'   GROUP BY description,username,narration,actype,entrydate,jstatus,status ORDER BY entrydate ASC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function groupwiserpt($dates, $datesto, $groupid)
    {
        $conn = $this->connect();
        $sqlquery = ("select description, sum(Dr),sum(cr) ,username,narration,actype,entrydate,jstatus from `journaldetails` as j inner join `ledgermaster` as l on j.ledgerid= l.`ledgerId` inner join `groupmaster` as g on g.`groupId`=l.`ledgergroupId` WHERE g.`groupId` = " . $groupid . " AND j.`entrydate` between '" . $dates . "' AND '" . $datesto . "' AND j.`status` ='A' GROUP BY description,username,narration,actype,entrydate,jstatus ORDER BY description ASC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function cashbal()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT (sum(dr)-sum(cr)) as bal FROM `journaldetails` WHERE `ledgerid`=1 AND `status` ='A' GROUP BY `ledgerid`");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function cashbalcq()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT  sum(chequeamt)  FROM `chequedetails` WHERE `chequestatus`='I'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    /**
     * Storing new user
     * returns user details
     */
    public function storeUser($name, $email, $password)
    {
        $uuid = uniqid('', true);
        $hash = $this->hashSSHA($password);
        $encrypted_password = $hash["encrypted"]; // encrypted password
        $salt = $hash["salt"]; // salt
        $conn = $this->conn;
        $sqlQuery = "INSERT INTO users(`unique_id`, `name`, `email`, `encrypted_password`, `salt`, `created_at`) "
            . "VALUES('" . $uuid . "', '" . $name . "', '" . $email . "', '" . $encrypted_password . "', '" . $salt . "', NOW())";
        $result = mysqli_query($conn, $sqlQuery);
        if ($result) {
            $sqlQuery_t = "SELECT * FROM `users` WHERE `email` ='" . $email . "'";
            $result_t = mysqli_query($conn, $sqlQuery_t);
            return $result_t;
        } else {
            return false;
        }
    }

    /**
     * Get user by email and password
     */
    public function getUserByEmailAndPassword($email, $password)
    {
        $conn = $this->conn;
        $sqlQuery_t = "SELECT * FROM `users` WHERE `email` ='" . $email . "'";
        $result_t = mysqli_query($conn, $sqlQuery_t);
        if ($result_t) {
            $user = mysqli_fetch_assoc($result_t);
            $salt = $user['salt'];
            $encrypted_password = $user['encrypted_password'];
            $hash = $this->checkhashSSHA($salt, $password);
            // check for password equality
            if ($encrypted_password == $hash) {
                // user authentication details are correct
                return $user;
            } else {
                return NULL;
            }
        }
    }

    /**
     * Check user is existed or not
     */
    public function isUserExisted($email)
    {
        $conn = $this->conn;
        $sqlQuery_t = "SELECT * FROM `users` WHERE `email` ='" . $email . "'";
        $result_t = mysqli_query($conn, $sqlQuery_t);
        //echo $sqlQuery_t;
        if (mysqli_num_rows($result_t) > 0) {
            // user existed
            return true;
        } else {
            // user not existed
            //echo $this->conn;
            return false;
        }
    }

    /**
     * Encrypting password
     * @param password
     * returns salt and encrypted password
     */
    public function hashSSHA($password)
    {

        $salt = sha1(rand());
        $salt = substr($salt, 0, 10);
        $encrypted = base64_encode(sha1($password . $salt, true) . $salt);
        $hash = array("salt" => $salt, "encrypted" => $encrypted);
        return $hash;
    }

    /**
     * Decrypting password
     * @param salt, password
     * returns hash string
     */
    public function checkhashSSHA($salt, $password)
    {

        $hash = base64_encode(sha1($password . $salt, true) . $salt);

        return $hash;
    }

    //Store Sales
    public function SaveInvoiceDtl(
        $psid_invoice_sno,
        $psid_invoice_prf,
        $psid_invoice_date,
        $psid_invoice_trno,
        $psid_invoice_description,
        $psid_invoice_procode,
        $psid_invoice_proqty,
        $psid_invoice_rate,
        $psid_invoice_amt,
        $psid_invoice_itemdisp,
        $psid_invoice_itemdisamt,
        $psid_invoice_gross,
        $psid_invoice_taxinex,
        $psid_invoice_taxvalue,
        $psid_invoice_taxamt,
        $psid_invoice_netamt,
        $psid_invoice_billtype,
        $psid_invoice_saletype,
        $psid_invoice_status,
        $psid_invoice_userid
    ) {
        $conn = $this->connect();
        $sqlquery = ("INSERT INTO `pos_sale_invoicedtl`(`psid_invoice_sno`, `psid_invoice_prf`, `psid_invoice_date`, `psid_invoice_trno`, `psid_invoice_description`"
            . ", `psid_invoice_procode`,`psid_invoice_proqty`, `psid_invoice_rate`, `psid_invoice_amt`, `psid_invoice_itemdisp`, `psid_invoice_itemdisamt`, `psid_invoice_gross`"
            . ", `psid_invoice_taxinex`, `psid_invoice_taxvalue`, `psid_invoice_taxamt`, `psid_invoice_netamt`, `psid_invoice_billtype`, `psid_invoice_saletype`, `psid_invoice_status`"
            . ", `psid_invoice_userid`) "
            . "VALUES ('" . $psid_invoice_sno . "','" . $psid_invoice_prf . "','" . $psid_invoice_date . "','" . $psid_invoice_trno . "','" . $psid_invoice_description . "'"
            . ",'" . $psid_invoice_procode . "','" . $psid_invoice_proqty . "','" . $psid_invoice_rate . "','" . $psid_invoice_amt . "','" . $psid_invoice_itemdisp . "','" . $psid_invoice_itemdisamt . "','" . $psid_invoice_gross . "'"
            . ",'" . $psid_invoice_taxinex . "','" . $psid_invoice_taxvalue . "','" . $psid_invoice_taxamt . "','" . $psid_invoice_netamt . "','" . $psid_invoice_billtype . "','" . $psid_invoice_saletype . "','" . $psid_invoice_status . "'"
            . ",'" . $psid_invoice_userid . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function SaveInvoiceHdr(
        $psih_invoice_trno,
        $psih_invoice_date,
        $psih_invoice_description,
        $psih_invoice_prefix,
        $psih_invoice_tqty,
        $psih_invoice_tamount,
        $psih_invoice_titemdisper,
        $psih_invoice_titemdisamt,
        $psih_invoice_tgrossamt,
        $psih_invoice_ttaxamt,
        $psih_invoice_tnetamt,
        $psih_invoice_saletype,
        $psih_invoice_billtype,
        $psih_invoice_billstatus,
        $psih_invoice_userid,
        $psih_invoice_billremarks,
        $psih_invoice_advamt,
        $psih_invoice_balamt,
        $psih_invoice_customerid
    ) {
        $conn = $this->connect();
        $sqlquery = ("INSERT INTO `pos_sale_invoicehdr`(`psih_invoice_trno`, `psih_invoice_date`, `psih_invoice_description`, `psih_invoice_prefix`, `psih_invoice_tqty`, `psih_invoice_tamount`,"
            . " `psih_invoice_titemdisper`, `psih_invoice_titemdisamt`, `psih_invoice_tgrossamt`, `psih_invoice_ttaxamt`, `psih_invoice_tnetamt`, `psih_invoice_saletype`, `psih_invoice_billtype`,"
            . " `psih_invoice_billstatus`, `psih_invoice_userid`, `psih_invoice_billremarks`, `psih_invoice_advamt`, `psih_invoice_balamt`, `psih_invoice_customerid`) "
            . "VALUES ('" . $psih_invoice_trno . "','" . $psih_invoice_date . "','" . $psih_invoice_description . "','" . $psih_invoice_prefix . "','" . $psih_invoice_tqty . "','" . $psih_invoice_tamount . "'"
            . ",'" . $psih_invoice_titemdisper . "','" . $psih_invoice_titemdisamt . "','" . $psih_invoice_tgrossamt . "','" . $psih_invoice_ttaxamt . "','" . $psih_invoice_tnetamt . "','" . $psih_invoice_saletype . "','" . $psih_invoice_billtype . "'"
            . ",'" . $psih_invoice_billstatus . "','" . $psih_invoice_userid . "','" . $psih_invoice_billremarks . "','" . $psih_invoice_advamt . "','" . $psih_invoice_balamt . "','" . $psih_invoice_customerid . "')");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectInvoice()
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT psih.psih_invoice_date,psih.psih_invoice_trno,cs.branchname,psih.psih_invoice_tnetamt,psih.psih_invoice_saletype,psih.psih_invoice_billtype,psih.psih_invoice_billstatus,psih.psih_invoice_advamt,psih.psih_invoice_balamt FROM `pos_sale_invoicehdr` as psih INNER JOIN di_branch_mast as cs ON psih.psih_invoice_customerid=cs.branchid GROUP BY psih.psih_invoice_date,psih.psih_invoice_trno,cs.branchname,psih.psih_invoice_tnetamt,psih.psih_invoice_saletype,psih.psih_invoice_billtype,psih.psih_invoice_billstatus,psih.psih_invoice_advamt,psih.psih_invoice_balamt ORDER BY psih.psih_invoice_date DESC");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectInvoiceByHDR($invoice)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `pos_sale_invoicehdr` as psih INNER JOIN di_branch_mast as cs ON psih.psih_invoice_customerid=cs.branchid WHERE psih.psih_invoice_trno='" . $invoice . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function selectInvoiceByDTL($invoice)
    {
        $conn = $this->connect();
        $sqlquery = ("SELECT * FROM `pos_sale_invoicedtl` as psdtl INNER JOIN `di_item_mast` as dtm ON psdtl.psid_invoice_procode=dtm.dim_item_id WHERE psdtl.psid_invoice_trno='" . $invoice . "' ORDER BY psdtl.psid_invoice_id");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function deleteInvoiceByDTL($invoice)
    {
        $conn = $this->connect();
        $sqlquery = ("DELETE FROM `pos_sale_invoicedtl` WHERE psid_invoice_trno='" . $invoice . "'");
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    public function deleteInvoiceByHDR($invoice)
    {
        $conn = $this->connect();
        $sqlquery = ("DELETE FROM `pos_sale_invoicehdr` WHERE psih_invoice_trno='" . $invoice . "'");
        $result = mysqli_query($conn, $sqlquery);
        unlink('../invoices/0' . $invoice . 'sales.pdf');
        return $result;
    }

    public function deleteJourEntry($id)
    {
        $conn = $this->connect();
        $sqlquery = ("DELETE FROM `journaldetails` WHERE `refinvoiceno`=" . $id);
        $result = mysqli_query($conn, $sqlquery);
        return $result;
    }

    //journal Entry

    public function storeJournalPurchaseEntry($cmbledgername, $txtvoucheramount, $drCrMode, $txtvoucherno, $txtdatepicker, $cmbactype, $cmbmodename, $txtnarration, $cmbbankname, $txtchqdate, $txtchqno, $txtchqamount, $txthidden)
    {
        $conn = $this->connect();
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
            . ",$txtvoucheramount,0.00,'Dr',$txtvoucherno,'" . $txtdatepicker . "','" . date('Y/m/d') . "','PAY','" . $cmbmodename . "','" . $txtnarration . "',0.00,'A','" . $txthidden . "','" . $ledgerName3['ledgerName'] . "','" . $cmbledgername . "')");
        $result = mysqli_query($conn, $sqlquery2);

        $sqlquery4 = ("INSERT INTO `journaldetails`(`ledgerid`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`)"
            . "VALUES ('" . $cmbledgername . "','" . $ledgerName3['ledgerName'] . "'"
            . ",0.00,$txtvoucheramount,'Cr',$txtvoucherno,'" . $txtdatepicker . "','" . date('Y/m/d') . "','PAY','" . $cmbmodename . "','" . $txtnarration . "',0.00,'A','" . $txthidden . "','" . $ledgerName1['ledgerName'] . "','" . $cmbactype . "')");
        $result = mysqli_query($conn, $sqlquery4);

        $this->updateVoucherNo('PAY');
        return $result;
    }

    public function storeJournalSalesEntry($ledgerid, $branchid, $vocheramt, $accttype, $modetype, $txtdatepicker, $statuAcct, $userid, $txtnarration, $refinvoiceno)
    {
        $conn = $this->connect();

        $sqlQuery1 = ("SELECT `ledgerName` FROM `ledgermaster` WHERE `ledgerid`=" . $ledgerid);
        $result1 = mysqli_query($conn, $sqlQuery1);
        $row1 = mysqli_fetch_assoc($result1);
        $ledgerName1 = $row1;

        $sqlQuery2 = ("SELECT `ledgerid`,`ledgerName` FROM `ledgermaster` WHERE `ledgerrefId`=" . $branchid);
        $result2 = mysqli_query($conn, $sqlQuery2);
        $row2 = mysqli_fetch_assoc($result2);
        $ledgerName2 = $row2;
        $txtvoucherno = $this->selectMaxBillNo("PAY");
        $sqlquery1 = ("INSERT INTO `journaldetails`(`ledgerid`,`refinvoiceno`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`)"
            . "VALUES ('" . $ledgerid . "','" . $refinvoiceno . "','" . $ledgerName1['ledgerName'] . "'"
            . ",'" . $vocheramt . "',0.00,'Dr',$txtvoucherno,'" . $txtdatepicker . "','" . date('Y/m/d') . "','" . $accttype . "','" . $modetype . "','" . $txtnarration . "',0.00,'" . $statuAcct . "','" . $userid . "','" . $ledgerName2['ledgerName'] . "','" . $ledgerName2['ledgerid'] . "')");
        $result = mysqli_query($conn, $sqlquery1);

        $sqlquery2 = ("INSERT INTO `journaldetails`(`ledgerid`,`refinvoiceno`, `description`, `dr`, `cr`, `jstatus`, `billno`, `entrydate`, `modifydate`, `actype`, `modetype`, `narration`, `closingbal`,`status`,`username`,`description2`,`ledgerid2`)"
            . "VALUES ('" . $ledgerName2['ledgerid'] . "','" . $refinvoiceno . "','" . $ledgerName2['ledgerName'] . "'"
            . ",0.00,'" . $vocheramt . "','Cr',$txtvoucherno,'" . $txtdatepicker . "','" . date('Y/m/d') . "','" . $accttype . "','" . $modetype . "','" . $txtnarration . "',0.00,'" . $statuAcct . "','" . $userid . "','" . $ledgerName1['ledgerName'] . "','" . $ledgerid . "')");
        $result = mysqli_query($conn, $sqlquery2);
        $this->updateVoucherNo('PAY');
        return $result;
    }

    //Employee Registration
    public function EmployeeInsert(
        $emp_firstname,
        $emp_lastname,
        $emp_shortname,
        $emp_idtype,
        $emp_passportic,
        $emp_nationality,
        $emp_passexpire,
        $emp_visaexpire,
        $emp_contactno,
        $emp_contactname,
        $emp_emergencyno,
        $emp_compid,
        $emp_branchid,
        $emp_position,
        $emp_bankname,
        $emp_accountname,
        $emp_accountno,
        $emp_image,
        $emp_joindate,
        $emp_basicsalary,
        $emp_basicrate,
        $emp_otrate,
        $emp_othrsrate,
        $emp_active,
        $emp_allowance
    ) {
        $conn = $this->connect();
        $sqlQuery = ("INSERT INTO `emp_register`(`emp_firstname`, `emp_lastname`, `emp_shortname`, `emp_idtype`, `emp_passportic`, `emp_nationality`,
        `emp_passexpire`, `emp_visaexpire`, `emp_contactno`, `emp_contactname`, `emp_emergencyno`, `emp_compid`, `emp_branchid`, `emp_position`,
        `emp_bankname`, `emp_accountname`, `emp_accountno`, `emp_image`, `emp_joindate`, `emp_basicsalary`, `emp_basicrate`, `emp_otrate`, `emp_othrsrate`,
        `emp_active`,`emp_allowance`) VALUES ('" . $emp_firstname . "','" . $emp_lastname . "','" . $emp_shortname . "','" . $emp_idtype . "','" . $emp_passportic . "','" . $emp_nationality . "',
        '" . $emp_passexpire . "','" . $emp_visaexpire . "','" . $emp_contactno . "','" . $emp_contactname . "','" . $emp_emergencyno . "','" . $emp_compid . "','" . $emp_branchid . "','" . $emp_position . "','" . $emp_bankname . "','" . $emp_accountname . "',
        '" . $emp_accountno . "','" . $emp_image . "','" . $emp_joindate . "','" . $emp_basicsalary . "','" . $emp_basicrate . "','" . $emp_otrate . "','" . $emp_othrsrate . "','" . $emp_active . "','" . $emp_allowance . "')");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function EmployeeUpdate(
        $emp_registerid,
        $emp_firstname,
        $emp_lastname,
        $emp_shortname,
        $emp_idtype,
        $emp_passportic,
        $emp_nationality,
        $emp_passexpire,
        $emp_visaexpire,
        $emp_contactno,
        $emp_contactname,
        $emp_emergencyno,
        $emp_compid,
        $emp_branchid,
        $emp_position,
        $emp_bankname,
        $emp_accountname,
        $emp_accountno,
        $emp_image,
        $emp_joindate,
        $emp_basicsalary,
        $emp_basicrate,
        $emp_otrate,
        $emp_othrsrate,
        $emp_active,
        $emp_allowance
    ) {
        $conn = $this->connect();
        $sqlQuery = ("UPDATE `emp_register` SET `emp_firstname`='" . $emp_firstname . "',`emp_lastname`='" . $emp_lastname . "',`emp_shortname`='" . $emp_shortname . "',`emp_idtype`='" . $emp_idtype . "',
        `emp_passportic`='" . $emp_passportic . "', `emp_nationality`='" . $emp_nationality . "',`emp_passexpire`='" . $emp_passexpire . "', `emp_visaexpire`='" . $emp_visaexpire . "',
        `emp_contactno`='" . $emp_contactno . "', `emp_contactname`='" . $emp_contactname . "', `emp_emergencyno`='" . $emp_emergencyno . "',`emp_compid`='" . $emp_compid . "',
        `emp_branchid`='" . $emp_branchid . "', `emp_position`='" . $emp_position . "',`emp_bankname`='" . $emp_bankname . "', `emp_accountname`='" . $emp_accountname . "',
        `emp_accountno`='" . $emp_accountno . "',`emp_image`='" . $emp_image . "',`emp_joindate`='" . $emp_joindate . "', `emp_basicsalary`='" . $emp_basicsalary . "',
        `emp_basicrate`='" . $emp_basicrate . "',`emp_otrate`='" . $emp_otrate . "',`emp_othrsrate`='" . $emp_othrsrate . "',`emp_active`='" . $emp_active . "',`emp_allowance`='" . $emp_allowance . "'
        WHERE `emp_registerid`='" . $emp_registerid . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function EmployeeSelect()
    {
        $conn = $this->connect();
        $sqlQuery = "SELECT * FROM `emp_register` WHERE 1";
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function EmployeeSelectById($emp_registerid)
    {
        $conn = $this->connect();
        $sqlQuery = ("SELECT * FROM `emp_register` WHERE `emp_registerid`= " . $emp_registerid);
        $result = mysqli_query($conn, $sqlQuery);
        return mysqli_fetch_assoc($result);
    }
}
