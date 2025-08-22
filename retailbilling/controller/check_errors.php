<?php
// Simple script to check for error logs and display recent errors
header('Content-Type: text/html; charset=UTF-8');

echo "<h2>POS Sales Error Log Checker</h2>";

// Check for PHP error log
$phpErrorLog = dirname(__FILE__) . '/php_error.log';
$salesErrorLog = dirname(__FILE__) . '/sales_error.log';

echo "<h3>PHP Error Log:</h3>";
if (file_exists($phpErrorLog)) {
    $phpErrors = file_get_contents($phpErrorLog);
    if (!empty($phpErrors)) {
        echo "<pre style='background-color: #ffe6e6; padding: 10px; border: 1px solid #ff9999; max-height: 400px; overflow-y: scroll;'>";
        echo htmlspecialchars($phpErrors);
        echo "</pre>";
    } else {
        echo "<p style='color: green;'>No PHP errors found.</p>";
    }
} else {
    echo "<p>PHP error log file does not exist.</p>";
}

echo "<h3>Sales Process Error Log:</h3>";
if (file_exists($salesErrorLog)) {
    $salesErrors = file_get_contents($salesErrorLog);
    if (!empty($salesErrors)) {
        echo "<pre style='background-color: #e6f3ff; padding: 10px; border: 1px solid #99ccff; max-height: 400px; overflow-y: scroll;'>";
        echo htmlspecialchars($salesErrors);
        echo "</pre>";
    } else {
        echo "<p style='color: green;'>No sales process errors found.</p>";
    }
} else {
    echo "<p>Sales error log file does not exist.</p>";
}

echo "<h3>Actions:</h3>";
echo "<a href='?clear=php' style='margin-right: 10px;'>Clear PHP Log</a>";
echo "<a href='?clear=sales' style='margin-right: 10px;'>Clear Sales Log</a>";
echo "<a href='?clear=all'>Clear All Logs</a>";

// Handle log clearing
if (isset($_GET['clear'])) {
    $clear = $_GET['clear'];
    if ($clear == 'php' && file_exists($phpErrorLog)) {
        file_put_contents($phpErrorLog, '');
        echo "<p style='color: green;'>PHP error log cleared.</p>";
    } elseif ($clear == 'sales' && file_exists($salesErrorLog)) {
        file_put_contents($salesErrorLog, '');
        echo "<p style='color: green;'>Sales error log cleared.</p>";
    } elseif ($clear == 'all') {
        if (file_exists($phpErrorLog)) file_put_contents($phpErrorLog, '');
        if (file_exists($salesErrorLog)) file_put_contents($salesErrorLog, '');
        echo "<p style='color: green;'>All logs cleared.</p>";
    }
}

echo "<hr>";
echo "<p><strong>Instructions:</strong></p>";
echo "<ul>";
echo "<li>Run your POS sales operation that's causing errors</li>";
echo "<li>Refresh this page to see any new error messages</li>";
echo "<li>The logs will show you exactly what's going wrong</li>";
echo "</ul>";
