<?php
// ============================================================
// Locations API
// GET    /api/locations.php               - List locations
// POST   /api/locations.php               - Create location
// PUT    /api/locations.php?id=LOC001     - Update + change PIN
// DELETE /api/locations.php?id=LOC001     - Deactivate
// ============================================================

require_once __DIR__ . '/../config/cors.php';
require_once __DIR__ . '/../config/database.php';
require_once __DIR__ . '/../config/auth.php';

$auth = requireApiKeyOrAuth();
$db   = Database::getInstance()->getConnection();
$id   = isset($_GET['id']) ? sanitize($_GET['id']) : null;

switch ($_SERVER['REQUEST_METHOD']) {

    case 'GET':
        $where  = "WHERE l.status='active'";
        $params = [];
        if (!empty($auth['com_id'])) {
            $where   .= " AND l.com_id = ?";
            $params[] = $auth['com_id'];
        }
        if (!empty($auth['branch_id'])) {
            $where   .= " AND l.branch_id = ?";
            $params[] = $auth['branch_id'];
        }
        if (isset($_GET['branch'])) {
            $where   .= " AND l.branch_id = ?";
            $params[] = sanitize($_GET['branch']);
        }
        if ($id) {
            $where   .= " AND l.loc_id = ?";
            $params[] = $id;
        }
        $stmt = $db->prepare(
            "SELECT l.*, b.name AS branch_name, c.name AS company_name,
                    (SELECT COUNT(*) FROM employees e WHERE e.loc_id = l.loc_id AND e.status='active') AS employee_count
             FROM locations l
             JOIN branches b  ON b.branch_id = l.branch_id
             JOIN companies c ON c.com_id    = l.com_id
             $where ORDER BY l.name"
        );
        $stmt->execute($params);
        $rows = $stmt->fetchAll();
        // Hide PIN from non-admin
        if (($auth['role'] ?? '') === 'device') {
            foreach ($rows as &$r) unset($r['device_pin']);
        }
        sendResponse(true, 'OK', $id ? ($rows[0] ?? null) : $rows);
        break;

    case 'POST':
        $body = getRequestBody();
        foreach (['loc_id','branch_id','com_id','name'] as $f) {
            if (empty($body[$f])) sendResponse(false, "Missing: $f", null, 400);
        }
        $stmt = $db->prepare(
            "INSERT INTO locations (loc_id, branch_id, com_id, name, description, device_pin)
             VALUES (?,?,?,?,?,?)"
        );
        $stmt->execute([
            sanitize($body['loc_id']),   sanitize($body['branch_id']),
            sanitize($body['com_id']),   sanitize($body['name']),
            sanitize($body['description'] ?? ''),
            sanitize($body['device_pin'] ?? '1234'),
        ]);
        sendResponse(true, 'Location created', null, 201);
        break;

    case 'PUT':
        if (!$id) sendResponse(false, 'loc_id required', null, 400);
        $body   = getRequestBody();
        $fields = []; $params = [];
        foreach (['name','description','device_pin','status'] as $f) {
            if (isset($body[$f])) { $fields[] = "$f=?"; $params[] = sanitize($body[$f]); }
        }
        if (empty($fields)) sendResponse(false, 'Nothing to update', null, 400);
        $params[] = $id;
        $db->prepare("UPDATE locations SET ".implode(',',$fields)." WHERE loc_id=?")->execute($params);
        sendResponse(true, 'Location updated');
        break;

    case 'DELETE':
        if (!$id) sendResponse(false, 'loc_id required', null, 400);
        $db->prepare("UPDATE locations SET status='inactive' WHERE loc_id=?")->execute([$id]);
        sendResponse(true, 'Location deactivated');
        break;

    default: sendResponse(false, 'Method not allowed', null, 405);
}
