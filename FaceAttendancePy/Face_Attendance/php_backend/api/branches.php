<?php
// ============================================================
// Branches API
// GET    /api/branches.php               - List branches
// POST   /api/branches.php               - Create branch
// PUT    /api/branches.php?id=BR001      - Update branch
// DELETE /api/branches.php?id=BR001      - Delete branch
// ============================================================

require_once __DIR__ . '/../config/cors.php';
require_once __DIR__ . '/../config/database.php';
require_once __DIR__ . '/../config/auth.php';

$auth = requireApiKeyOrAuth();
$db   = Database::getInstance()->getConnection();
$id   = isset($_GET['id']) ? sanitize($_GET['id']) : null;

switch ($_SERVER['REQUEST_METHOD']) {

    case 'GET':
        $where  = "WHERE b.status != 'deleted'";
        $params = [];
        if (!empty($auth['com_id'])) {
            $where   .= " AND b.com_id = ?";
            $params[] = $auth['com_id'];
        }
        if ($id) {
            $where   .= " AND b.branch_id = ?";
            $params[] = $id;
        }
        $stmt = $db->prepare(
            "SELECT b.*, c.name AS company_name,
                    (SELECT COUNT(*) FROM locations l WHERE l.branch_id = b.branch_id) AS location_count,
                    (SELECT COUNT(*) FROM employees e WHERE e.branch_id = b.branch_id AND e.status='active') AS employee_count
             FROM branches b JOIN companies c ON c.com_id = b.com_id $where ORDER BY b.name"
        );
        $stmt->execute($params);
        sendResponse(true, 'OK', $id ? $stmt->fetch() : $stmt->fetchAll());
        break;

    case 'POST':
        $body = getRequestBody();
        foreach (['branch_id','com_id','name'] as $f) {
            if (empty($body[$f])) sendResponse(false, "Missing: $f", null, 400);
        }
        $stmt = $db->prepare(
            "INSERT INTO branches (branch_id, com_id, name, address, manager_name, phone)
             VALUES (?,?,?,?,?,?)"
        );
        $stmt->execute([
            sanitize($body['branch_id']), sanitize($body['com_id']),
            sanitize($body['name']),      sanitize($body['address']      ?? ''),
            sanitize($body['manager_name'] ?? ''), sanitize($body['phone'] ?? ''),
        ]);
        sendResponse(true, 'Branch created', null, 201);
        break;

    case 'PUT':
        if (!$id) sendResponse(false, 'branch_id required', null, 400);
        $body   = getRequestBody();
        $fields = []; $params = [];
        foreach (['name','address','manager_name','phone','status'] as $f) {
            if (isset($body[$f])) { $fields[] = "$f=?"; $params[] = sanitize($body[$f]); }
        }
        $params[] = $id;
        $db->prepare("UPDATE branches SET ".implode(',',$fields)." WHERE branch_id=?")->execute($params);
        sendResponse(true, 'Branch updated');
        break;

    case 'DELETE':
        if (!$id) sendResponse(false, 'branch_id required', null, 400);
        $db->prepare("UPDATE branches SET status='inactive' WHERE branch_id=?")->execute([$id]);
        sendResponse(true, 'Branch deactivated');
        break;

    default: sendResponse(false, 'Method not allowed', null, 405);
}
