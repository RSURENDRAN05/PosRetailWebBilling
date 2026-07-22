# Multi-Tenant Database Routing (SyncId based)

Each client has its own MySQL database. The connection is selected
dynamically from the `SyncId` in the API URL via the master database
`myposqrc_master`. **Adding a new client requires only one INSERT into
`client_connection` — no code changes.**

## Folder structure

```
retailbilling/
├── sql/
│   └── master_database.sql          # Creates myposqrc_master + tables + samples
└── controller/
    ├── config/
    │   └── master.config.php        # Master DB credentials + encryption key
    ├── lib/
    │   ├── TenantConnection.php     # Core resolver (validate, rate-limit, log, connect)
    │   └── encrypt_password.php     # CLI tool to encrypt client DB passwords
    ├── dbconnect.php                # Drop-in include used by every API
    ├── dbconnect.php.bak            # Backup of the old single-tenant version
    ├── getfunctionmgmt.php          # Existing APIs (unchanged)
    ├── getsynctocloudlocal.php
    ├── getChqModule.php
    └── getfunctiontaxaudit.php
```

## Setup

1. **Run the SQL script** (as root / privileged user):
   ```
   mysql -u root -p < retailbilling/sql/master_database.sql
   ```
2. **Create the least-privilege master API user** (uncomment section 4 of
   the SQL script and set a strong password).
3. **Edit `controller/config/master.config.php`:**
   - Set `MASTER_DB_USER` / `MASTER_DB_PASSWORD`.
   - Generate an encryption key and set `MYPOS_ENC_KEY`:
     ```
     php -r "echo bin2hex(random_bytes(32));"
     ```
4. **Encrypt each client DB password** and store it:
   ```
   php retailbilling/controller/lib/encrypt_password.php 'TheRealPassword'
   # -> enc:v1:BASE64...
   ```
   ```sql
   UPDATE client_connection SET DBPassword = 'enc:v1:BASE64...' WHERE SyncId = '123';
   ```

## Adding a new client (no code changes)

```sql
INSERT INTO client_connection
    (ClientID, ClientName, SyncId, DBHost, DBUser, DBPassword, DBName, Status)
VALUES
    ('CL0003', 'New Shop Sdn Bhd', '789', 'localhost',
     'myposqrc_newshop', 'enc:v1:BASE64...', 'myposqrc_newshopdb', 1);
```

## Usage in APIs

Every API just includes `dbconnect.php` and immediately has the client
connection:

```php
<?php
require_once 'dbconnect.php';

$conn = db();                       // tenant mysqli connection

$stmt = mysqli_prepare($conn, 'SELECT id, username FROM users WHERE status = ?');
$status = 1;
mysqli_stmt_bind_param($stmt, 'i', $status);
mysqli_stmt_execute($stmt);
$result = mysqli_stmt_get_result($stmt);

$rows = array();
while ($row = mysqli_fetch_assoc($result)) {
    $rows[] = $row;
}
header('Content-Type: application/json; charset=utf-8');
echo json_encode(array('Success' => true, 'Data' => $rows));
```

The legacy pattern used by the existing `cls*` classes keeps working
unchanged:

```php
require_once 'dbconnect.php';
$db = new database();
$conn = $db->connect();             // same tenant connection
```

The legacy `DB_HOST`, `DB_USER`, `DB_PASSWORD`, `DB_DATABASE` constants
and the `Db_Connect` class alias are also defined from the resolved
tenant, so `clsfunsynctocloudlocal.php` and `clsfunctionchqmodule.php`
work without modification.

## Example API requests

```
https://myposqr.com/retailbilling/controller/getfunctionmgmt.php?SyncId=123&AjaxRequest=1
https://myposqr.com/retailbilling/controller/getsynctocloudlocal.php?SyncId=123&AjaxRequest=5
https://myposqr.com/retailbilling/controller/getChqModule.php?SyncId=456&AjaxRequest=2
https://myposqr.com/retailbilling/controller/getfunctiontaxaudit.php?SyncId=456&AjaxRequest=1
```

## Example JSON responses

Success (shape unchanged — produced by the existing endpoints):
```json
{"Success": true, "Data": [{"Id": "1", "UserName": "admin"}]}
```

Errors (produced by `dbconnect.php` before any endpoint code runs):

| Situation                  | HTTP | Body |
|----------------------------|------|------|
| SyncId missing             | 400  | `{"Success":false,"ErrorCode":"MISSING_SYNCID","Msg":"SyncId parameter is required."}` |
| SyncId bad format          | 400  | `{"Success":false,"ErrorCode":"INVALID_SYNCID","Msg":"SyncId format is invalid."}` |
| SyncId not registered      | 404  | `{"Success":false,"ErrorCode":"UNKNOWN_SYNCID","Msg":"SyncId is not registered."}` |
| Client disabled (Status=0) | 403  | `{"Success":false,"ErrorCode":"CLIENT_DISABLED","Msg":"This client account is disabled."}` |
| Too many invalid attempts  | 429  | `{"Success":false,"ErrorCode":"RATE_LIMITED","Msg":"Too many invalid requests. Try again later."}` |
| Master DB down             | 500  | `{"Success":false,"ErrorCode":"MASTER_DB_ERROR","Msg":"Service temporarily unavailable."}` |
| Client DB unreachable      | 500  | `{"Success":false,"ErrorCode":"TENANT_DB_ERROR","Msg":"Unable to connect to the client database."}` |

## Security features

- **SQL injection** — master lookups use MySQLi prepared statements only.
- **SyncId validation** — alphanumeric, max 32 chars (`ctype_alnum`).
- **Password encryption** — AES-256-GCM (`enc:v1:` prefix) with a key
  kept in `master.config.php`; plain values still work for migration.
- **Logging** — every invalid attempt (missing / malformed / unknown /
  disabled) is stored in `api_access_log` with IP and URI.
- **Rate limiting** — an IP with ≥ 10 invalid attempts in 5 minutes
  (configurable) receives HTTP 429 with `Retry-After`.
- **JSON-only errors** — no HTML, no stack traces; internals go to the
  PHP error log only.
- **Least privilege** — the master API user only needs `SELECT` on
  `client_connection` and `SELECT, INSERT` on `api_access_log`.

## Best practices / notes

- Move `config/master.config.php` outside the web root if hosting
  allows, and update the `require_once` path in
  `lib/TenantConnection.php`.
- Deny web access to `config/` and `lib/` (e.g. `.htaccess`:
  `Require all denied`); `encrypt_password.php` refuses non-CLI use.
- Rotate `MYPOS_ENC_KEY` by re-encrypting all `DBPassword` values.
- `Config.php` is now only needed for company/email constants; the
  hard-coded `DB_*` / `DBQR_*` credentials there are obsolete and
  should be removed once verified nothing else uses them.
- Scales to thousands of clients: one indexed unique-key lookup per
  request; add APCu/Redis caching of the `client_connection` row later
  if needed.
