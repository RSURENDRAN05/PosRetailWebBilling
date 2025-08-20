&lt;?php
// Multiple Price Management API
// This handles requests for multiple pricing options

header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: GET, POST, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type');

// Database connection (adjust according to your config)
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "your_database_name";

try {
$pdo = new PDO("mysql:host=$servername;dbname=$dbname;charset=utf8mb4", $username, $password);
$pdo-&gt;setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
} catch(PDOException $e) {
echo json_encode(array("Success" =&gt; "False", "Message" =&gt; "Connection failed: " . $e-&gt;getMessage()));
exit;
}

// Handle Multiple Price Requests
if (isset($_GET['MultiplePriceRequest'])) {
$requestType = $_GET['MultiplePriceRequest'];

switch ($requestType) {
case '1': // Get multiple prices for an item
getMultiplePrices();
break;
case '2': // Add new multiple price
addMultiplePrice();
break;
case '3': // Update multiple price
updateMultiplePrice();
break;
case '4': // Delete multiple price
deleteMultiplePrice();
break;
case '5': // Get all multiple prices (for management)
getAllMultiplePrices();
break;
default:
echo json_encode(array("Success" =&gt; "False", "Message" =&gt; "Invalid request type"));
break;
}
}

// Get multiple prices for a specific item
function getMultiplePrices() {
global $pdo;

try {
$itemId = isset($_GET['ItemId']) ? intval($_GET['ItemId']) : 0;
$comId = isset($_GET['ComId']) ? intval($_GET['ComId']) : 1;
$locId = isset($_GET['LocId']) ? intval($_GET['LocId']) : 1;

if ($itemId == 0) {
echo json_encode(array("Success" =&gt; "False", "Message" =&gt; "Item ID is required"));
return;
}

$sql = "SELECT price_id, item_id, price_name, price_value, com_id, loc_id, status, created, updated
FROM item_multiple_price
WHERE item_id = :item_id AND com_id = :com_id AND loc_id = :loc_id AND status = 1
ORDER BY price_name";

$stmt = $pdo-&gt;prepare($sql);
$stmt-&gt;bindParam(':item_id', $itemId, PDO::PARAM_INT);
$stmt-&gt;bindParam(':com_id', $comId, PDO::PARAM_INT);
$stmt-&gt;bindParam(':loc_id', $locId, PDO::PARAM_INT);
$stmt-&gt;execute();

$result = $stmt-&gt;fetchAll(PDO::FETCH_ASSOC);

if (count($result) &gt; 0) {
echo json_encode(array("Success" =&gt; "True", "Data" =&gt; $result));
} else {
echo json_encode(array("Success" =&gt; "False", "Message" =&gt; "No multiple prices found for this item"));
}

} catch(PDOException $e) {
echo json_encode(array("Success" =&gt; "False", "Message" =&gt; "Error: " . $e-&gt;getMessage()));
}
}

// Add new multiple price
function addMultiplePrice() {
global $pdo;

try {
$itemId = isset($_POST['ItemId']) ? intval($_POST['ItemId']) : 0;
$priceName = isset($_POST['PriceName']) ? trim($_POST['PriceName']) : '';
$priceValue = isset($_POST['PriceValue']) ? floatval($_POST['PriceValue']) : 0.00;
$comId = isset($_POST['ComId']) ? intval($_POST['ComId']) : 1;
$locId = isset($_POST['LocId']) ? intval($_POST['LocId']) : 1;

if ($itemId == 0 || empty($priceName) || $priceValue &lt;= 0) {
echo json_encode(array("Success" =&gt; "False", "Message" =&gt; "Item ID, Price Name, and Price Value are required"));
return;
}

// Check if price name already exists for this item
$checkSql = "SELECT COUNT(*) FROM item_multiple_price
WHERE item_id = :item_id AND price_name = :price_name AND com_id = :com_id AND loc_id = :loc_id";
$checkStmt = $pdo-&gt;prepare($checkSql);
$checkStmt-&gt;bindParam(':item_id', $itemId, PDO::PARAM_INT);
$checkStmt-&gt;bindParam(':price_name', $priceName, PDO::PARAM_STR);
$checkStmt-&gt;bindParam(':com_id', $comId, PDO::PARAM_INT);
$checkStmt-&gt;bindParam(':loc_id', $locId, PDO::PARAM_INT);
$checkStmt-&gt;execute();

if ($checkStmt-&gt;fetchColumn() &gt; 0) {
echo json_encode(array("Success" =&gt; "False", "Message" =&gt; "Price name already exists for this item"));
return;
}

$sql = "INSERT INTO item_multiple_price (item_id, price_name, price_value, com_id, loc_id, status)
VALUES (:item_id, :price_name, :price_value, :com_id, :loc_id, 1)";

$stmt = $pdo-&gt;prepare($sql);
$stmt-&gt;bindParam(':item_id', $itemId, PDO::PARAM_INT);
$stmt-&gt;bindParam(':price_name', $priceName, PDO::PARAM_STR);
$stmt-&gt;bindParam(':price_value', $priceValue, PDO::PARAM_STR);
$stmt-&gt;bindParam(':com_id', $comId, PDO::PARAM_INT);
$stmt-&gt;bindParam(':loc_id', $locId, PDO::PARAM_INT);

if ($stmt-&gt;execute()) {
$priceId = $pdo-&gt;lastInsertId();
echo json_encode(array("Success" =&gt; "True", "Message" =&gt; "Multiple price added successfully", "PriceId" =&gt; $priceId));
} else {
echo json_encode(array("Success" =&gt; "False", "Message" =&gt; "Failed to add multiple price"));
}

} catch(PDOException $e) {
echo json_encode(array("Success" =&gt; "False", "Message" =&gt; "Error: " . $e-&gt;getMessage()));
}
}

// Update multiple price
function updateMultiplePrice() {
global $pdo;

try {
$priceId = isset($_POST['PriceId']) ? intval($_POST['PriceId']) : 0;
$priceName = isset($_POST['PriceName']) ? trim($_POST['PriceName']) : '';
$priceValue = isset($_POST['PriceValue']) ? floatval($_POST['PriceValue']) : 0.00;

if ($priceId == 0 || empty($priceName) || $priceValue &lt;= 0) {
echo json_encode(array("Success" =&gt; "False", "Message" =&gt; "Price ID, Price Name, and Price Value are required"));
return;
}

$sql = "UPDATE item_multiple_price
SET price_name = :price_name, price_value = :price_value, updated = CURRENT_TIMESTAMP
WHERE price_id = :price_id";

$stmt = $pdo-&gt;prepare($sql);
$stmt-&gt;bindParam(':price_id', $priceId, PDO::PARAM_INT);
$stmt-&gt;bindParam(':price_name', $priceName, PDO::PARAM_STR);
$stmt-&gt;bindParam(':price_value', $priceValue, PDO::PARAM_STR);

if ($stmt-&gt;execute()) {
echo json_encode(array("Success" =&gt; "True", "Message" =&gt; "Multiple price updated successfully"));
} else {
echo json_encode(array("Success" =&gt; "False", "Message" =&gt; "Failed to update multiple price"));
}

} catch(PDOException $e) {
echo json_encode(array("Success" =&gt; "False", "Message" =&gt; "Error: " . $e-&gt;getMessage()));
}
}

// Delete multiple price (soft delete)
function deleteMultiplePrice() {
global $pdo;

try {
$priceId = isset($_POST['PriceId']) ? intval($_POST['PriceId']) : 0;

if ($priceId == 0) {
echo json_encode(array("Success" =&gt; "False", "Message" =&gt; "Price ID is required"));
return;
}

$sql = "UPDATE item_multiple_price SET status = 0, updated = CURRENT_TIMESTAMP WHERE price_id = :price_id";

$stmt = $pdo-&gt;prepare($sql);
$stmt-&gt;bindParam(':price_id', $priceId, PDO::PARAM_INT);

if ($stmt-&gt;execute()) {
echo json_encode(array("Success" =&gt; "True", "Message" =&gt; "Multiple price deleted successfully"));
} else {
echo json_encode(array("Success" =&gt; "False", "Message" =&gt; "Failed to delete multiple price"));
}

} catch(PDOException $e) {
echo json_encode(array("Success" =&gt; "False", "Message" =&gt; "Error: " . $e-&gt;getMessage()));
}
}

// Get all multiple prices for management
function getAllMultiplePrices() {
global $pdo;

try {
$comId = isset($_GET['ComId']) ? intval($_GET['ComId']) : 1;
$locId = isset($_GET['LocId']) ? intval($_GET['LocId']) : 1;

$sql = "SELECT imp.price_id, imp.item_id, im.ITEMNAME, imp.price_name, imp.price_value,
imp.com_id, imp.loc_id, imp.status, imp.created, imp.updated
FROM item_multiple_price imp
LEFT JOIN itemmaster im ON imp.item_id = im.ITEMCODE
WHERE imp.com_id = :com_id AND imp.loc_id = :loc_id AND imp.status = 1
ORDER BY im.ITEMNAME, imp.price_name";

$stmt = $pdo-&gt;prepare($sql);
$stmt-&gt;bindParam(':com_id', $comId, PDO::PARAM_INT);
$stmt-&gt;bindParam(':loc_id', $locId, PDO::PARAM_INT);
$stmt-&gt;execute();

$result = $stmt-&gt;fetchAll(PDO::FETCH_ASSOC);

echo json_encode(array("Success" =&gt; "True", "Data" =&gt; $result));

} catch(PDOException $e) {
echo json_encode(array("Success" =&gt; "False", "Message" =&gt; "Error: " . $e-&gt;getMessage()));
}
}

?&gt;
