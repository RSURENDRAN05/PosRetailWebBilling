<?php

class clsfunctionchqModule {

    public $login_user;
    public $login_userid;
    public $title = 'Mypos Portal';
    public $Version = 'Admin Ver 2.0';
    private $conn;

//constructor
    public function __construct() {
        require_once 'DB_Connect.php';
//require_once '../config/Config.php';
//date_default_timezone_set('Asia/Kuala_Lumpur');
// connecting to database
        $db = new Db_Connect();
        $this->conn = $db->connect();
    }

    public function connect() {
        $conn = mysqli_connect(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);
        if ($conn) {
            $this->conn = $conn;
        }
        return $conn;
    }

    public function _login($user, $pass) {
        $conn = $this->connect();
        $sqlQuery = ("SELECT * FROM `users` WHERE  `username`= '" . $user . "' and  `password`= '" . md5($pass) . "' and `status`='1'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function GetUser() {
        $conn = $this->connect();
        $sqlSelect = ("SELECT `id` as Id, `username` as UserName, `password` as Password, case `role` when 1 then 'Admin' when 2 then 'User' end as Role, case `status` when 1 then 'Active' when 1 then 'InActive' End as Status FROM `users` WHERE `role`=1");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function GetComapany() {
        $conn = $this->connect();
        $sqlSelect = ("SELECT pcm_id as COID,pcm_name as CompanyName,plm_id as LID,plm_name as LocationName, pcm_active  as Active FROM pos_company_mast,pos_location_mast WHERE 1;");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function GetLocation() {
        $conn = $this->connect();
        $sqlSelect = ("SELECT * FROM `pos_location_mast`");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function SaveCompany($pcm_name, $pcm_active) {
        $conn = $this->connect();
        $sqlSelect = ("INSERT INTO `pos_company_mast`(`pcm_name`, `pcm_shortname`, `pcm_sst`, `pcm_active`, `pcm_address`, `pcm_default`)"
                . " VALUES ('" . $pcm_name . "','M','M','" . $pcm_active . "','M','1')");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function UpdateCompany($pcm_id, $pcm_name, $pcm_active) {
        $conn = $this->connect();
        $sqlSelect = ("UPDATE `pos_company_mast` SET  `pcm_name`='" . $pcm_name . "' ,`pcm_active`='" . $pcm_active . "'  WHERE  `pcm_id`='" . $pcm_id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function getPayee() {
        $conn = $this->connect();
        $sqlSelect = ("SELECT `id` as Id, `payeename` as PayeeName, `active` as Active FROM `pos_payee` ORDER BY `payeename` ASC");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function SavePayee($payeename, $active) {
        $conn = $this->connect();
        $payeenames = mysqli_real_escape_string($this->conn, $payeename);
        $sqlSelect = ("INSERT INTO `pos_payee`(`payeename`, `active`) VALUES ( '" . $payeenames . "','" . $active . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function DeletePayee($id) {
        $conn = $this->connect();
        $sqlSelect = ("DELETE FROM `pos_payee`  WHERE  `id`='" . $id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function getBankStatement() {
        $conn = $this->connect();
        $sqlSelect = ("SELECT `Id`, `pcm`.`pcm_name` as CompanyName, `PayeeChq`, `BankName`, `PayeeName`, DATE_FORMAT(`Payeedate`,'%d/%m/%Y') as PayeeDate, `PayeeMode`, `PayeeAmountDr`, `PayeeAmountCr`, `PayeeStatus`,DATE_FORMAT(`created`,'%d/%m/%Y') as IssueDate FROM `pos_bankstatement` as pb INNER JOIN pos_company_mast as pcm ON `pb`.`CompanyName`=`pcm`.`pcm_id`   ORDER BY `ID` DESC");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function getBankStatementByDate($frdate, $todate) {
        $conn = $this->connect();
        $sqlSelect = ("SELECT `Id`, `pcm`.`pcm_name` as CompanyName, `PayeeChq`, `BankName`, `PayeeName`, DATE_FORMAT(`Payeedate`,'%d/%m/%Y') as PayeeDate, `PayeeMode`, `PayeeAmountDr`, `PayeeAmountCr`, `PayeeStatus`,DATE_FORMAT(`created`,'%d/%m/%Y') as IssueDate  FROM `pos_bankstatement` as pb INNER JOIN pos_company_mast as pcm ON `pb`.`CompanyName`=`pcm`.`pcm_id` "
                . "WHERE `PayeeDate` >= '" . $frdate . "' AND `PayeeDate` <= '" . $todate . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function getBankPendingCheque($frdate) {
        $conn = $this->connect();
        $sqlSelect = ("SELECT  `pcm`.`pcm_name` as CompanyName,   `BankName`,  `PayeeMode`, sum( `PayeeAmountDr`) as PayeeAmountDr,  `PayeeStatus` FROM `pos_bankstatement` as pb INNER JOIN pos_company_mast as pcm ON `pb`.`CompanyName`=`pcm`.`pcm_id`"
                . " WHERE `PayeeDate` <= '" . $frdate . "' AND `PayeeMode`='Out' AND   `PayeeStatus` =2 GROUP BY `pcm`.`pcm_name` , `BankName`, `PayeeMode`,`PayeeStatus`");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function SaveBankSatement($CompanyName, $PayeeChq, $BankName, $PayeeName, $PayeeDate, $PayeeMode, $PayeeAmountDr, $PayeeAmountCr, $PayeeStatus, $useridcreated) {
        $conn = $this->connect();
        $payeenames = mysqli_real_escape_string($this->conn, $PayeeName);
        $sqlSelect = ("INSERT INTO `pos_bankstatement`(`CompanyName`, `PayeeChq`, `BankName`, `PayeeName`, `PayeeDate`, `PayeeMode`, `PayeeAmountDr`, `PayeeAmountCr`, `PayeeStatus`,`useridcreated`  )"
                . " VALUES ('" . $CompanyName . "','" . $PayeeChq . "','" . $BankName . "','" . $payeenames . "','" . $PayeeDate . " ','" . $PayeeMode . "','" . $PayeeAmountDr . "','" . $PayeeAmountCr . "','" . $PayeeStatus . "','" . $useridcreated . "')");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function UpdateBankSatement($id, $res, $useridmodified) {
        $conn = $this->connect();
        $sqlSelect = ("UPDATE `pos_bankstatement` SET  `PayeeStatus`='" . $res . "',`useridmodified`='" . $useridmodified . "'  WHERE  `Id`='" . $id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function DeleteBankSatement($id) {
        $conn = $this->connect();
        $sqlSelect = ("DELETE FROM `pos_bankstatement`  WHERE  `Id`='" . $id . "'");
        $result = mysqli_query($conn, $sqlSelect);
        return ($result);
    }

    public function _branchSelect($comid) {
        $conn = $this->conn;
        $sqlQuery = ("SELECT dbm.branchid as Id,dbm.branchname as BranchName,dbm.branchcustomerid as CompanyId,cm.customerName as CompanyName,dbm.branchaddress as Address,dbm.branchemail as Email, dbm.branchcontact as Contact,dbm.branchanydesk as AnyDesk,dbm.branchserver as Server,dbm.branchclient as Client,dbm.branchtab as Tab,branchinstalldate as InstallDate, dbm.branchlock as Locks,dbm.branchactivationcode as Activation,dbm.branchstatus as Active,dbm.branchmessage as Message,dbm.branchip as IpAddress,dbm.branchpassword as BrPassword FROM `di_branch_mast` as dbm INNER join `customermaster` as cm on dbm.branchcustomerid=cm.customerId WHERE dbm.branchcustomerid = '".$comid."';");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }

    public function _branchUpdateIp($branchid, $ip) {
        $conn = $this->conn;
        $sqlQuery = ("UPDATE `di_branch_mast` SET , `branchip`='" . $ip . "'  WHERE `branchid`='" . $branchid . "'");
        $result = mysqli_query($conn, $sqlQuery);
        return $result;
    }
}
?>

