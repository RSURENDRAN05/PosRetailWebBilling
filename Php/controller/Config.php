<?php

//define("DB_HOST", "localhost");
//define('DB_USER', 'root');
//define('DB_PASSWORD', '');
//define('DB_DATABASE', 'myposaccts');
//  define('DB_HOST', 'localhost');
//  define('DB_USER', 'myposqrc_accts');
//  define('DB_PASSWORD', 'Ruthram@1986');
//  define('DB_DATABASE', 'myposqrc_accts');
//define('DB_HOST', 'localhost');
//define('DB_USER', 'dinainas_acctsuser');
//define('DB_PASSWORD', 'Ruthr@m1986');
//define('DB_DATABASE', 'dinainas_accts');
define('DB_HOST', 'localhost');
define('DB_USER', 'myposqrc_accts');
define('DB_PASSWORD', 'Ruthram@1986');
define('DB_DATABASE', 'myposqrc_retail');
//Qrconnection
define("DBQR_HOST", 'localhost');
define("DBQR_USER", 'myposqrc_db');
define("DBQR_PASSWORD", 'AgvO~ieQw}G1');
define("DBQR_DATABASE", 'myposqrc_qr');
// COMPANY INFORMATION
define('COMPANY_LOGO', '../images/mypos.jpg');
define('COMPANY_LOGO_WIDTH', '150');
define('COMPANY_LOGO_HEIGHT', '90');
define('COMPANY_NAME', 'MURIFA SYSTEM');
define('COMPANY_ADDRESS_1', 'No.14444B ,Taman PaikSiong');
define('COMPANY_ADDRESS_2', 'Batu 7 ½ Jalan Puchong');
define('COMPANY_ADDRESS_3', 'Selangor-54000');
define('COMPANY_COUNTY', 'Kuala Lumpur');
define('COMPANY_POSTCODE', '54000');

define('COMPANY_NUMBER', '(002506233-H)'); // Company registration number
define('COMPANY_VAT', 'Company VAT: 00000'); // Company TAX/VAT number
// EMAIL DETAILS
define('EMAIL_FROM', 'murifasystem@gmail.com'); // Email address invoice emails will be sent from
define('EMAIL_NAME', 'Murifa System'); // Email from address
define('EMAIL_SUBJECT', 'Invoice default email subject'); // Invoice email subject
define('EMAIL_BODY_INVOICE', 'Invoice default body'); // Invoice email body
define('EMAIL_BODY_QUOTE', 'Quote default body'); // Invoice email body
define('EMAIL_BODY_RECEIPT', 'Receipt default body'); // Invoice email body
// OTHER SETTINFS
define('INVOICE_PREFIX', 'MD'); // Prefix at start of invoice - leave empty '' for no prefix
define('INVOICE_INITIAL_VALUE', '1'); // Initial invoice order number (start of increment)
define('INVOICE_THEME', '#fc4e03'); // Theme colour, this sets a colour theme for the PDF generate invoice
define('TIMEZONE', 'Asia/Kuala_Lumpur'); // Timezone - See for list of Timezone's http://php.net/manual/en/function.date-default-timezone-set.php
define('DATE_FORMAT', 'DD/MM/YYYY'); // DD/MM/YYYY or MM/DD/YYYY
define('CURRENCY', 'RM'); // Currency symbol
define('ENABLE_VAT', false); // Enable TAX/VAT
define('VAT_INCLUDED', false); // Is VAT included or excluded?
define('VAT_RATE', '0'); // This is the percentage value

define('PAYMENT_DETAILS', 'Payable To : MURIFA SYSTEM <br>MAYBANK Number:564230330898 <br>MUBARAK:016 3220445, ARUN:0169144557, SUREN:016 306 4557'); // Payment information
define('FOOTER_NOTE', 'MUBARAK:016 3220445, ARUN:0169144557, SUREN:016 306 4557');
