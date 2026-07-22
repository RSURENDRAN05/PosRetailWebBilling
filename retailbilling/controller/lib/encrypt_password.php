<?php

/**
 * CLI utility - encrypt a client database password for storage in
 * myposqrc_master.client_connection.DBPassword.
 *
 * Usage:
 *   php encrypt_password.php 'ThePlainPassword'
 *
 * Output is the "enc:v1:..." string to store in the DBPassword column.
 * Uses MYPOS_ENC_KEY from ../config/master.config.php.
 */

if (PHP_SAPI !== 'cli') {
    http_response_code(403);
    header('Content-Type: application/json; charset=utf-8');
    echo json_encode(array('Success' => false, 'ErrorCode' => 'FORBIDDEN', 'Msg' => 'CLI only.'));
    exit;
}

require_once __DIR__ . '/TenantConnection.php';

if ($argc < 2 || $argv[1] === '') {
    fwrite(STDERR, "Usage: php encrypt_password.php 'ThePlainPassword'\n");
    exit(1);
}

$encrypted = \TenantConnection::encryptPassword($argv[1]);

echo "Encrypted DBPassword value:\n{$encrypted}\n\n";
echo "SQL example:\n";
echo "UPDATE client_connection SET DBPassword = '{$encrypted}' WHERE SyncId = 'XXX';\n";

// Round-trip self check
if (\TenantConnection::decryptPassword($encrypted) !== $argv[1]) {
    fwrite(STDERR, "WARNING: round-trip verification failed!\n");
    exit(1);
}
echo "Round-trip verification: OK\n";
