<?php

/**
 * =====================================================================
 * MASTER DATABASE CONFIGURATION  (multi-tenant routing)
 * =====================================================================
 * This file holds ONLY the credentials of the master database
 * (myposqrc_master) and the encryption key used to decrypt the
 * per-client DB passwords stored in client_connection.DBPassword.
 *
 * SECURITY:
 *  - Restrict file permissions (e.g. chmod 600) on the server.
 *  - If possible, move this file OUTSIDE the web root and adjust the
 *    require path in lib/TenantConnection.php.
 *  - Never commit real production secrets to source control.
 *  - MYPOS_ENC_KEY must be a random 32+ char string. Generate one:
 *        php -r "echo bin2hex(random_bytes(32));"
 *    Changing the key invalidates all previously encrypted passwords.
 * =====================================================================
 */

// ---- Master database (holds client_connection + api_access_log) ----
define('MASTER_DB_HOST', 'localhost');
define('MASTER_DB_USER', 'myposqrc_masterapi');      // least-privilege user (see sql/master_database.sql)
define('MASTER_DB_PASSWORD', 'Ruthram@1986');
define('MASTER_DB_NAME', 'myposqrc_master');

// ---- Encryption key for client_connection.DBPassword (AES-256) ----
define('MYPOS_ENC_KEY', 'a91c3e7b5d8f2046c8a7e9d1123f5b6c78a90e4d5f6a7b8c9d0e1f23456789ab');

// ---- SyncId validation ----
define('SYNCID_MAX_LENGTH', 32);                     // matches client_connection.SyncId VARCHAR(32)

// ---- Rate limiting of INVALID requests (per client IP) ----
define('RATE_LIMIT_MAX_ATTEMPTS', 10);               // max invalid attempts ...
define('RATE_LIMIT_WINDOW_SECONDS', 300);            // ... within this window (seconds)

// ---- Misc ----
define('MYPOS_TIMEZONE', 'Asia/Kuala_Lumpur');
