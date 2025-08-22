<?php
// Enhanced error log viewer for POS system debugging
header('Content-Type: text/html; charset=UTF-8');

?>
<!DOCTYPE html>
<html>

<head>
    <title>POS System Debug Console</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
            background-color: #f5f5f5;
        }

        .container {
            max-width: 1200px;
            margin: 0 auto;
            background: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .header {
            background: #343a40;
            color: white;
            padding: 15px;
            border-radius: 5px;
            margin-bottom: 20px;
        }

        .status-ok {
            color: #28a745;
            background: #d4edda;
            padding: 10px;
            border: 1px solid #c3e6cb;
            border-radius: 4px;
        }

        .status-error {
            color: #721c24;
            background: #f8d7da;
            padding: 10px;
            border: 1px solid #f5c6cb;
            border-radius: 4px;
        }

        .status-info {
            color: #0c5460;
            background: #d1ecf1;
            padding: 10px;
            border: 1px solid #bee5eb;
            border-radius: 4px;
        }

        .log-box {
            background-color: #f8f9fa;
            padding: 15px;
            border: 1px solid #dee2e6;
            border-radius: 4px;
            max-height: 500px;
            overflow-y: scroll;
            font-family: 'Courier New', monospace;
            font-size: 12px;
            white-space: pre-wrap;
        }

        .php-log {
            background-color: #fff5f5;
            border-color: #fed7d7;
        }

        .sales-log {
            background-color: #f0f8ff;
            border-color: #bee3f8;
        }

        .btn {
            display: inline-block;
            padding: 8px 16px;
            margin: 5px;
            text-decoration: none;
            border-radius: 4px;
            font-weight: bold;
        }

        .btn-primary {
            background: #007bff;
            color: white;
        }

        .btn-success {
            background: #28a745;
            color: white;
        }

        .btn-danger {
            background: #dc3545;
            color: white;
        }

        .btn-secondary {
            background: #6c757d;
            color: white;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin: 10px 0;
        }

        th,
        td {
            padding: 8px;
            border: 1px solid #dee2e6;
            text-align: left;
        }

        th {
            background-color: #e9ecef;
            font-weight: bold;
        }

        .section {
            margin: 20px 0;
            padding: 15px;
            border: 1px solid #dee2e6;
            border-radius: 5px;
        }

        .timestamp {
            color: #6c757d;
            font-size: 11px;
        }
    </style>
</head>

<body>

    <div class="container">
        <div class="header">
            <h1>🔧 POS System Debug Console</h1>
            <p>Last Updated: <?php echo date('Y-m-d H:i:s'); ?> |
                <a href="?" class="btn btn-primary">🔄 Refresh</a>
            </p>
        </div>

        <?php
        // Define log file paths
        $phpErrorLog = dirname(__FILE__) . '/php_error.log';
        $salesErrorLog = dirname(__FILE__) . '/sales_error.log';

        // Handle log clearing actions
        if (isset($_GET['action'])) {
            $action = $_GET['action'];
            switch ($action) {
                case 'clear_php':
                    if (file_exists($phpErrorLog)) {
                        file_put_contents($phpErrorLog, '');
                        echo "<div class='status-ok'>✅ PHP error log cleared successfully.</div>";
                    }
                    break;
                case 'clear_sales':
                    if (file_exists($salesErrorLog)) {
                        file_put_contents($salesErrorLog, '');
                        echo "<div class='status-ok'>✅ Sales error log cleared successfully.</div>";
                    }
                    break;
                case 'clear_all':
                    if (file_exists($phpErrorLog)) file_put_contents($phpErrorLog, '');
                    if (file_exists($salesErrorLog)) file_put_contents($salesErrorLog, '');
                    echo "<div class='status-ok'>✅ All error logs cleared successfully.</div>";
                    break;
            }
        }

        // Helper function to format file sizes
        function formatBytes($size, $precision = 2)
        {
            if ($size == 0) return '0 B';
            $base = log($size, 1024);
            $suffixes = array('B', 'KB', 'MB', 'GB', 'TB');
            return round(pow(1024, $base - floor($base)), $precision) . ' ' . $suffixes[floor($base)];
        }

        // Function to get recent log lines
        function getRecentLines($filePath, $maxLines = 100)
        {
            if (!file_exists($filePath)) return array();
            $lines = file($filePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
            return array_slice($lines, -$maxLines);
        }

        // Function to count error types
        function analyzeLogContent($lines)
        {
            $analysis = array(
                'total_lines' => count($lines),
                'fatal_errors' => 0,
                'warnings' => 0,
                'notices' => 0,
                'database_errors' => 0,
                'json_errors' => 0
            );

            foreach ($lines as $line) {
                $line = strtolower($line);
                if (strpos($line, 'fatal error') !== false) $analysis['fatal_errors']++;
                if (strpos($line, 'warning') !== false) $analysis['warnings']++;
                if (strpos($line, 'notice') !== false) $analysis['notices']++;
                if (strpos($line, 'mysql') !== false || strpos($line, 'database') !== false) $analysis['database_errors']++;
                if (strpos($line, 'json') !== false) $analysis['json_errors']++;
            }

            return $analysis;
        }
        ?>

        <!-- System Status Overview -->
        <div class="section">
            <h2>📊 System Status Overview</h2>
            <table>
                <tr>
                    <th>Component</th>
                    <th>Status</th>
                    <th>Log File Size</th>
                    <th>Last Modified</th>
                </tr>
                <tr>
                    <td>PHP Error Log</td>
                    <td>
                        <?php
                        if (file_exists($phpErrorLog) && filesize($phpErrorLog) > 0) {
                            echo "<span style='color: #dc3545;'>❌ Has Errors</span>";
                        } else {
                            echo "<span style='color: #28a745;'>✅ Clean</span>";
                        }
                        ?>
                    </td>
                    <td><?php echo file_exists($phpErrorLog) ? formatBytes(filesize($phpErrorLog)) : 'N/A'; ?></td>
                    <td><?php echo file_exists($phpErrorLog) ? date('Y-m-d H:i:s', filemtime($phpErrorLog)) : 'N/A'; ?></td>
                </tr>
                <tr>
                    <td>Sales Process Log</td>
                    <td>
                        <?php
                        if (file_exists($salesErrorLog) && filesize($salesErrorLog) > 0) {
                            echo "<span style='color: #dc3545;'>❌ Has Errors</span>";
                        } else {
                            echo "<span style='color: #28a745;'>✅ Clean</span>";
                        }
                        ?>
                    </td>
                    <td><?php echo file_exists($salesErrorLog) ? formatBytes(filesize($salesErrorLog)) : 'N/A'; ?></td>
                    <td><?php echo file_exists($salesErrorLog) ? date('Y-m-d H:i:s', filemtime($salesErrorLog)) : 'N/A'; ?></td>
                </tr>
            </table>
        </div>

        <!-- Quick Actions -->
        <div class="section">
            <h2>🛠️ Quick Actions</h2>
            <a href="?action=clear_php" class="btn btn-danger">Clear PHP Log</a>
            <a href="?action=clear_sales" class="btn btn-danger">Clear Sales Log</a>
            <a href="?action=clear_all" class="btn btn-danger">Clear All Logs</a>
            <a href="getfunctionmgmt.php?SalesRequest=test" class="btn btn-secondary" target="_blank">Test Sales API</a>
        </div>

        <!-- PHP Error Log Section -->
        <div class="section">
            <h2>🔴 PHP Error Log Analysis</h2>
            <?php
            $phpLines = getRecentLines($phpErrorLog, 50);
            if (count($phpLines) > 0) {
                $phpAnalysis = analyzeLogContent($phpLines);
                echo "<div class='status-error'>";
                echo "<strong>Analysis:</strong> {$phpAnalysis['total_lines']} total entries | ";
                echo "Fatal: {$phpAnalysis['fatal_errors']} | Warnings: {$phpAnalysis['warnings']} | ";
                echo "Notices: {$phpAnalysis['notices']} | DB Errors: {$phpAnalysis['database_errors']}";
                echo "</div>";
                echo "<div class='log-box php-log'>" . htmlspecialchars(implode("\n", $phpLines)) . "</div>";
            } else {
                echo "<div class='status-ok'>✅ No PHP errors found. System is running cleanly.</div>";
            }
            ?>
        </div>

        <!-- Sales Process Error Log Section -->
        <div class="section">
            <h2>🔵 Sales Process Error Log Analysis</h2>
            <?php
            $salesLines = getRecentLines($salesErrorLog, 100);
            if (count($salesLines) > 0) {
                $salesAnalysis = analyzeLogContent($salesLines);
                echo "<div class='status-error'>";
                echo "<strong>Analysis:</strong> {$salesAnalysis['total_lines']} total entries | ";
                echo "Database Issues: {$salesAnalysis['database_errors']} | JSON Issues: {$salesAnalysis['json_errors']}";
                echo "</div>";
                echo "<div class='log-box sales-log'>" . htmlspecialchars(implode("\n", $salesLines)) . "</div>";
            } else {
                echo "<div class='status-ok'>✅ No sales process errors found. All transactions are processing correctly.</div>";
            }
            ?>
        </div>

        <!-- Debugging Guide -->
        <div class="section">
            <h2>📋 Debugging Workflow</h2>
            <div class="status-info">
                <h3>Step-by-Step Debugging Process:</h3>
                <ol>
                    <li><strong>Trigger the Issue:</strong> Perform the problematic POS operation in your application</li>
                    <li><strong>Refresh This Page:</strong> Click the refresh button to see new error entries</li>
                    <li><strong>Analyze Errors:</strong> Look for patterns in timestamps and error messages</li>
                    <li><strong>Check Error Types:</strong> Focus on fatal errors first, then warnings</li>
                    <li><strong>Test Fix:</strong> Clear logs, implement fix, test again</li>
                    <li><strong>Verify:</strong> Confirm no new errors appear after your fix</li>
                </ol>

                <h3>🔍 Common Error Patterns to Look For:</h3>
                <ul>
                    <li><strong>MySQL/Database Errors:</strong> Connection timeouts, syntax errors, missing tables</li>
                    <li><strong>JSON Decode Errors:</strong> Malformed data from VB.NET frontend</li>
                    <li><strong>Function Parameter Errors:</strong> Wrong argument counts in SaveSaleHdr/SaveSaleDtl</li>
                    <li><strong>Missing Required Fields:</strong> Null or empty values for required parameters</li>
                    <li><strong>PHP Fatal Errors:</strong> Undefined functions, class instantiation issues</li>
                </ul>
            </div>
        </div>

        <!-- Recent Activity Monitor -->
        <div class="section">
            <h2>⏱️ Recent Activity Monitor</h2>
            <div class="status-info">
                <p><strong>Monitoring Status:</strong> This page shows the most recent 50 PHP errors and 100 sales process entries.</p>
                <p><strong>Auto-Refresh:</strong> Manually refresh this page after testing to see new errors.</p>
                <p><strong>Log Rotation:</strong> Consider clearing logs periodically to maintain performance.</p>
                <p class="timestamp">Page loaded at: <?php echo date('Y-m-d H:i:s'); ?></p>
            </div>
        </div>

    </div>

</body>

</html>
