<?php
session_start();

mysqli_report(MYSQLI_REPORT_OFF);
require_once dirname(__DIR__) . '/config/master.config.php';

/*
==================================================
 MYPOS Multi Tenant Client Management
 Master Database Connection
==================================================
*/


/*
==================================================
 Database Connection
==================================================
*/

function masterDB()
{
    static $conn = null;

    if ($conn === null) {

        $conn = new mysqli(
            MASTER_DB_HOST,
            MASTER_DB_USER,
            MASTER_DB_PASSWORD,
            MASTER_DB_NAME
        );


        if ($conn->connect_error) {

            die(
                "Master Database Connection Failed : "
                .$conn->connect_error
            );
        }


        $conn->set_charset("utf8mb4");
    }


    return $conn;
}



/*
==================================================
 Generate 8 Character SyncId
 Example:
 A8K9P2XZ
==================================================
*/

function generateSyncId($length = 8)
{

    $characters =
    "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";


    do {

        $syncId = "";

        for($i=0;$i<$length;$i++)
        {
            $syncId .=
            $characters[
                random_int(
                    0,
                    strlen($characters)-1
                )
            ];
        }


        // Check duplicate

        $db = masterDB();


        $stmt = $db->prepare(
            "SELECT Id 
             FROM client_connection 
             WHERE SyncId=?"
        );


        $stmt->bind_param(
            "s",
            $syncId
        );


        $stmt->execute();


        $result=$stmt->get_result();


    } while($result->num_rows > 0);



    return $syncId;
}




/*
==================================================
 Encrypt Password AES-256-GCM
 Output:
 enc:v1:xxxxxxxx
==================================================
*/

function encryptPassword($plain)
{

    $key = hex2bin(MYPOS_ENC_KEY);


    $iv = random_bytes(12);


    $tag = "";


    $encrypted = openssl_encrypt(
        $plain,
        "aes-256-gcm",
        $key,
        OPENSSL_RAW_DATA,
        $iv,
        $tag
    );


    return "enc:v1:"
        .base64_encode(
            $iv.$tag.$encrypted
        );
}



/*
==================================================
 Decrypt Password
==================================================
*/

function decryptPassword($value)
{

    if(
        strpos($value,"enc:v1:") !== 0
    )
    {
        return $value; // legacy password
    }


    $key = hex2bin(MYPOS_ENC_KEY);


    $data =
    base64_decode(
        substr($value,7)
    );


    $iv =
    substr($data,0,12);


    $tag =
    substr($data,12,16);


    $encrypted =
    substr($data,28);



    return openssl_decrypt(
        $encrypted,
        "aes-256-gcm",
        $key,
        OPENSSL_RAW_DATA,
        $iv,
        $tag
    );

}




/*
==================================================
 Test Client Database Connection
==================================================
*/

function testDatabase(
    $host,
    $user,
    $password,
    $database,
    &$errorMessage = null
)
{
    $errorMessage = null;

    try {
        $conn = mysqli_init();

        if(!$conn)
        {
            $errorMessage = "MySQL connection could not be initialized.";
            return false;
        }

        $connected = @$conn->real_connect(
            $host,
            $user,
            $password,
            $database
        );

        if(!$connected || $conn->connect_errno)
        {
            $errorMessage = $conn->connect_error ?: "Unknown MySQL connection error.";
            return false;
        }

        $conn->close();
        return true;
    } catch (Throwable $error) {
        error_log('Client database connection test failed: '.$error->getMessage());
        $errorMessage = $error->getMessage();
        return false;
    }
}

function clientDatabaseError($error, $user, $database)
{
    $error = trim((string)$error);

    if(stripos($error, 'Access denied') !== false)
    {
        return "Access denied: database user '".$user."' does not have permission to access database '".$database."'.";
    }

    if(stripos($error, 'Unknown database') !== false)
    {
        return "Database '".$database."' does not exist.";
    }

    if(stripos($error, "Connection refused") !== false || stripos($error, "Can't connect") !== false)
    {
        return "Unable to reach the MySQL server. Check the database host.";
    }

    return $error !== '' ? "Client database connection failed: ".$error : "Client database connection failed.";
}





/*
==================================================
 Escape Output
==================================================
*/

function e($value)
{
    return htmlspecialchars(
        $value ?? '',
        ENT_QUOTES,
        'UTF-8'
    );
}



/*
==================================================
 Flash Message
==================================================
*/

function message($text,$type="success")
{

    $_SESSION['msg'] =
    [
        "text"=>$text,
        "type"=>$type
    ];

}

function redirectToIndex()
{
    $path = strtok($_SERVER['REQUEST_URI'], '?');
    header('Location: '.$path, true, 303);
    exit;
}

/*
==================================================
 HANDLE ACTIONS
==================================================
*/

$db = masterDB();


/*
==================================================
 ADD NEW CLIENT
==================================================
*/

if(isset($_POST['save_client']))
{

    $clientID   = trim($_POST['ClientID']);
    $clientName = trim($_POST['ClientName']);

    $syncId     = trim($_POST['SyncId']);

    if($syncId=="")
    {
        $syncId = generateSyncId(8);
    }


    $host     = trim($_POST['DBHost']);
    $dbUser   = trim($_POST['DBUser']);
    $dbPass   = trim($_POST['DBPassword']);
    $dbName   = trim($_POST['DBName']);

    $status   = intval($_POST['Status']);


    /*
       Test Database Connection
    */

    $connectionError = null;

    if(!testDatabase(
        $host,
        $dbUser,
        $dbPass,
        $dbName,
        $connectionError
    ))
    {

        message(
            clientDatabaseError($connectionError, $dbUser, $dbName),
            "danger"
        );

        redirectToIndex();

    }



    /*
       Encrypt Password
    */

    $encryptedPassword =
    encryptPassword($dbPass);



    $stmt=$db->prepare(
    "
    INSERT INTO client_connection
    (
        ClientID,
        ClientName,
        SyncId,
        DBHost,
        DBUser,
        DBPassword,
        DBName,
        Status
    )
    VALUES
    (?,?,?,?,?,?,?,?)
    "
    );

    if(!$stmt)
    {
        message(
            "Unable to save client: ".$db->error,
            "danger"
        );
        redirectToIndex();
    }

    $stmt->bind_param(
        "sssssssi",
        $clientID,
        $clientName,
        $syncId,
        $host,
        $dbUser,
        $encryptedPassword,
        $dbName,
        $status
    );



    if($stmt->execute())
    {
        message(
            "Client Added Successfully"
        );
    }
    else
    {
        message(
            $stmt->error,
            "danger"
        );
    }


    redirectToIndex();

}





/*
==================================================
 UPDATE CLIENT
==================================================
*/

if(isset($_POST['update_client']))
{

    $id=intval($_POST['Id']);


    $clientName =
    trim($_POST['ClientName']);


    $host =
    trim($_POST['DBHost']);


    $dbUser =
    trim($_POST['DBUser']);


    $dbName =
    trim($_POST['DBName']);


    $status =
    intval($_POST['Status']);



    /*
       Password optional during edit
    */

    if($_POST['DBPassword']!="")
    {

        $enc =
        encryptPassword(
            $_POST['DBPassword']
        );


        $stmt=$db->prepare(
        "
        UPDATE client_connection SET

        ClientName=?,
        DBHost=?,
        DBUser=?,
        DBPassword=?,
        DBName=?,
        Status=?

        WHERE Id=?
        "
        );


        $stmt->bind_param(
            "sssssii",
            $clientName,
            $host,
            $dbUser,
            $enc,
            $dbName,
            $status,
            $id
        );


    }
    else
    {

        $stmt=$db->prepare(
        "
        UPDATE client_connection SET

        ClientName=?,
        DBHost=?,
        DBUser=?,
        DBName=?,
        Status=?

        WHERE Id=?
        "
        );


        $stmt->bind_param(
            "ssssii",
            $clientName,
            $host,
            $dbUser,
            $dbName,
            $status,
            $id
        );

    }



    if($stmt->execute())
    {
        message(
            "Client Updated Successfully"
        );
    }
    else
    {
        message(
            $stmt->error,
            "danger"
        );
    }


    redirectToIndex();

}






/*
==================================================
 DISABLE CLIENT
==================================================
*/

if(isset($_GET['disable']))
{

    $id=intval($_GET['disable']);


    $stmt=$db->prepare(
    "
    UPDATE client_connection
    SET Status=0
    WHERE Id=?
    "
    );


    $stmt->bind_param(
        "i",
        $id
    );


    $stmt->execute();


    message(
        "Client Disabled"
    );


    redirectToIndex();

}





/*
==================================================
 DELETE CLIENT
==================================================
*/

if(isset($_GET['delete']))
{

    $id=intval($_GET['delete']);


    $stmt=$db->prepare(
    "
    DELETE FROM client_connection
    WHERE Id=?
    "
    );


    $stmt->bind_param(
        "i",
        $id
    );


    $stmt->execute();


    message(
        "Client Deleted",
        "warning"
    );


    redirectToIndex();

}





/*
==================================================
 EDIT DATA LOAD
==================================================
*/

$editData=null;


if(isset($_GET['edit']))
{

    $id=intval($_GET['edit']);


    $stmt=$db->prepare(
    "
    SELECT *
    FROM client_connection
    WHERE Id=?
    "
    );


    $stmt->bind_param(
        "i",
        $id
    );


    $stmt->execute();


    $editData =
    $stmt
    ->get_result()
    ->fetch_assoc();

}





/*
==================================================
 SEARCH CLIENT LIST
==================================================
*/

$search="";


if(isset($_GET['search']))
{
    $search =
    trim($_GET['search']);
}



if($search!="")
{

    $like="%".$search."%";


    $stmt=$db->prepare(
    "
    SELECT *
    FROM client_connection

    WHERE
    ClientID LIKE ?
    OR
    ClientName LIKE ?
    OR
    SyncId LIKE ?

    ORDER BY Id DESC
    "
    );


    $stmt->bind_param(
        "sss",
        $like,
        $like,
        $like
    );


}
else
{

    $stmt=$db->prepare(
    "
    SELECT *
    FROM client_connection
    ORDER BY Id DESC
    "
    );

}


$stmt->execute();


$clients =
$stmt
->get_result();



?>

<?php
$msg = $_SESSION['msg'] ?? null;
unset($_SESSION['msg']);

?>

<!DOCTYPE html>
<html>
<head>

<meta charset="UTF-8">

<title>MYPOS Client Management</title>

<meta name="viewport" content="width=device-width, initial-scale=1">


<!-- Bootstrap -->
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">


<style>

body
{
    background:#f4f6f9;
    font-family:Segoe UI,Arial;
}


.header
{
    background:#1f2937;
    color:white;
    padding:20px;
    border-radius:10px;
    margin-bottom:20px;
}


.card
{
    border:none;
    border-radius:15px;
    box-shadow:0 5px 20px rgba(0,0,0,.08);
}


.card-header
{
    background:#2563eb;
    color:white;
    border-radius:15px 15px 0 0 !important;
}


.form-control,
.form-select
{
    border-radius:8px;
}


.btn
{
    border-radius:8px;
}


.table
{
    background:white;
}


.badge
{
    padding:8px;
}


.sync-box
{
    background:#eef2ff;
    padding:10px;
    border-radius:10px;
    font-weight:bold;
    color:#1d4ed8;
}


</style>


<script>


function generateSync()
{

    let chars =
    "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";


    let result="";


    for(let i=0;i<8;i++)
    {
        result +=
        chars[
            Math.floor(
                Math.random()*chars.length
            )
        ];
    }


    document.getElementById("SyncId").value=result;

}



function viewClient(data)
{

    document.getElementById("viewData").innerHTML=data;

}



</script>


</head>


<body>


<div class="container-fluid p-4">



<div class="header">

<h2>
MYPOS Multi Tenant Client Management
</h2>

<p class="mb-0">
SyncId Based Database Routing System
</p>

</div>




<?php if($msg): ?>


<div class="alert alert-<?php echo e($msg['type']); ?>">

<?php echo e($msg['text']); ?>

</div>


<?php endif; ?>





<div class="row">



<!-- FORM -->

<div class="col-lg-4">



<div class="card mb-4">


<div class="card-header">

<h5>
<?php echo $editData ? 
"Edit Client":"Add New Client"; ?>
</h5>

</div>


<div class="card-body">


<form method="post">



<?php if($editData): ?>

<input type="hidden"
name="Id"
value="<?php echo $editData['Id']; ?>">

<?php endif; ?>



<label>
Client ID
</label>

<input class="form-control mb-3"
name="ClientID"
required
value="<?php echo e($editData['ClientID'] ?? ''); ?>">





<label>
Client Name
</label>

<input class="form-control mb-3"
name="ClientName"
required
value="<?php echo e($editData['ClientName'] ?? ''); ?>">





<label>
Sync ID
</label>


<div class="input-group mb-3">


<input 
id="SyncId"
class="form-control sync-box"
name="SyncId"
readonly
value="<?php echo e($editData['SyncId'] ?? ''); ?>">


<button 
type="button"
onclick="generateSync()"
class="btn btn-primary">

Generate

</button>


</div>





<label>
Database Host
</label>

<input class="form-control mb-3"
name="DBHost"
value="<?php echo e($editData['DBHost'] ?? 'localhost'); ?>">





<label>
Database User
</label>

<input class="form-control mb-3"
name="DBUser"
value="<?php echo e($editData['DBUser'] ?? ''); ?>">






<label>
Database Password
</label>

<input 
type="password"
class="form-control mb-3"
name="DBPassword"
placeholder="Leave empty to keep old password">






<label>
Database Name
</label>


<input class="form-control mb-3"
name="DBName"
value="<?php echo e($editData['DBName'] ?? ''); ?>">





<label>
Status
</label>


<select class="form-select mb-3"
name="Status">


<option value="1"
<?php 
if(($editData['Status']??1)==1)
echo "selected";
?>>

Active

</option>


<option value="0"
<?php 
if(($editData['Status']??1)==0)
echo "selected";
?>>

Disabled

</option>


</select>




<?php if($editData): ?>


<button 
class="btn btn-success w-100"
name="update_client">

Update Client

</button>


<?php else: ?>


<button 
class="btn btn-success w-100"
name="save_client">

Save Client

</button>


<?php endif; ?>



</form>


</div>


</div>



</div>





<!-- LIST -->

<div class="col-lg-8">



<div class="card">


<div class="card-header">

Client List


</div>


<div class="card-body">



<form class="mb-3">


<div class="input-group">


<input 
class="form-control"
name="search"
placeholder="Search Client / SyncId">


<button class="btn btn-dark">

Search

</button>


</div>


</form>






<div class="table-responsive">


<table class="table table-hover">


<thead>

<tr>

<th>ID</th>

<th>Client</th>

<th>SyncId</th>

<th>Database</th>

<th>Status</th>

<th>Action</th>


</tr>


</thead>


<tbody>



<?php while($c=$clients->fetch_assoc()): ?>


<tr>


<td>
<?php echo $c['Id']; ?>
</td>



<td>

<b>
<?php echo e($c['ClientName']); ?>
</b>

<br>

<small>
<?php echo e($c['ClientID']); ?>
</small>

</td>




<td>

<span class="badge bg-primary">

<?php echo $c['SyncId']; ?>

</span>

</td>




<td>

<?php echo e($c['DBName']); ?>

<br>

<small>
<?php echo e($c['DBUser']); ?>
</small>


</td>




<td>


<?php if($c['Status']==1): ?>

<span class="badge bg-success">
Active
</span>


<?php else: ?>


<span class="badge bg-danger">
Disabled
</span>


<?php endif; ?>


</td>




<td>


<a 
href="?edit=<?php echo $c['Id']; ?>"
class="btn btn-sm btn-warning">

Edit

</a>



<a
href="?disable=<?php echo $c['Id']; ?>"
class="btn btn-sm btn-secondary">

Disable

</a>



<a
onclick="return confirm('Delete Client?')"
href="?delete=<?php echo $c['Id']; ?>"
class="btn btn-sm btn-danger">

Delete

</a>



</td>



</tr>



<?php endwhile; ?>



</tbody>


</table>


</div>



</div>


</div>


</div>


</div>



</div>


</body>

</html>