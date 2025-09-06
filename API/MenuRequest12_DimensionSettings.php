<?php
// Add this to your getfunctionmgmt.php file for MenuRequest=12

if ($MenuRequest == "12") {
    // Button Dimension Settings API
    $operation = isset($_GET['operation']) ? $_GET['operation'] : '';

    if ($operation == "SAVE_DIMENSION") {
        $groupName = isset($_GET['group_name']) ? $_GET['group_name'] : '';
        $groupValue = isset($_GET['group_value']) ? $_GET['group_value'] : '';

        if (!empty($groupName) && !empty($groupValue)) {
            $result = saveDimensionSetting($groupName, $groupValue);

            if ($result) {
                echo json_encode([
                    "Success" => true,
                    "Message" => "Dimension setting saved successfully",
                    "GroupName" => $groupName,
                    "GroupValue" => $groupValue
                ]);
            } else {
                echo json_encode([
                    "Success" => false,
                    "Message" => "Failed to save dimension setting"
                ]);
            }
        } else {
            echo json_encode([
                "Success" => false,
                "Message" => "Invalid parameters"
            ]);
        }
    }
}

// Function to save dimension settings
function saveDimensionSetting($groupName, $groupValue)
{
    global $connection; // Your database connection

    try {
        // Validate group name
        $validGroupNames = [
            'MAINH',
            'MAINW',
            'MAINCOL',
            'SUBH',
            'SUBW',
            'SUBMENUCOL',
            'ITEMH',
            'ITEMW',
            'ITEMMENUCOL'
        ];

        if (!in_array($groupName, $validGroupNames)) {
            return false;
        }

        // Update the dimension setting
        $sqlUpdate = "UPDATE `tb_master_all` SET `tma_group_value` = ? WHERE `tma_group_name` = ?";
        $stmt = $connection->prepare($sqlUpdate);
        $stmt->bind_param("ss", $groupValue, $groupName);

        if ($stmt->execute()) {
            $stmt->close();
            return true;
        } else {
            $stmt->close();
            return false;
        }
    } catch (Exception $e) {
        error_log("Error saving dimension setting: " . $e->getMessage());
        return false;
    }
}
