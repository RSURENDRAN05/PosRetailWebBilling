# POS Settings Manager Documentation

## Overview
The POS Settings Manager is a comprehensive system for managing application configuration settings in your Point of Sale (POS) system. It provides a user-friendly interface for CRUD operations on settings and a robust backend API for programmatic access.

## Features
- **Full CRUD Operations**: Create, Read, Update, and Delete settings
- **DevExpress UI Components**: Professional-looking form with GridControl and layout controls
- **Type Safety**: Support for different data types (string, int, decimal, boolean, json)
- **Status Management**: Enable/disable settings without deleting them
- **Automatic Data Loading**: Seamless integration with your existing backend
- **Error Handling**: Comprehensive error handling and user feedback
- **Keyboard Shortcuts**: F5 to refresh, Ctrl+S to save, Ctrl+N for new, Esc to close

## Components Created

### 1. FrmPosSettings.vb
Main form for managing POS settings with the following features:
- **Input Fields**: ID (read-only), Name, Value (multi-line), Type (dropdown), Status (dropdown)
- **Action Buttons**: Save, New, Delete, Refresh, Close
- **Data Grid**: Displays all settings with filtering and sorting capabilities
- **Validation**: Ensures required fields are filled before saving

### 2. FrmPosSettings.Designer.vb
DevExpress form designer file containing:
- Layout controls for responsive design
- Grid control with proper column configuration
- Repository items for status display with icons
- Image collection for status indicators

### 3. PosSettingsManager.vb
Core business logic class providing:
- **Static Methods**: For easy access throughout the application
- **Data Loading**: Automatic loading and caching of settings
- **Type Conversion**: Safe conversion to different data types
- **Common Settings**: Predefined properties for common POS settings
- **CRUD Operations**: Programmatic save and update functionality

### 4. TestPosSettingsForm.vb
Test form demonstrating:
- How to load and display settings
- How to save new settings programmatically
- Example usage patterns

## Database Schema
The system expects a `pos_settings` table with the following structure:
```sql
CREATE TABLE pos_settings (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(255) NOT NULL UNIQUE,
    Status TINYINT DEFAULT 1,
    Value TEXT,
    Type VARCHAR(50) DEFAULT 'string',
    Created TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

## PHP Backend Integration
The system integrates with your existing PHP backend using:
- **Endpoint**: `GroupPolicyRequest=8`
- **Operations**: SELECT, INSERT, UPDATE, DELETE
- **JSON Communication**: All data exchange in JSON format

## Usage Examples

### Basic Usage
```vb
' Open the settings form
Dim frmSettings As New FrmPosSettings()
frmSettings.ShowDialog()
```

### Programmatic Access
```vb
' Load settings
PosSettingsManager.LoadSettings()

' Get specific settings
Dim companyName As String = PosSettingsManager.CompanyName
Dim taxRate As Decimal = PosSettingsManager.TaxRate
Dim enableDiscount As Boolean = PosSettingsManager.EnableDiscount

' Save custom settings
PosSettingsManager.SaveSetting("CUSTOM_SETTING", "Custom Value", "string")
```

### Common POS Settings
The system includes predefined properties for common POS settings:
- **CompanyName**: Business name
- **TaxRate**: Default tax percentage
- **Currency**: Currency code (USD, EUR, etc.)
- **EnableDiscount**: Whether discounts are allowed
- **MaxDiscountPercent**: Maximum discount percentage
- **PrintReceipt**: Auto-print receipts
- **ReceiptPrinter**: Default printer name
- **EnableBarcode**: Barcode scanning enabled
- **LowStockAlert**: Minimum stock threshold
- **BackupInterval**: Backup frequency in hours

## Integration Steps

### 1. Add to Main Menu
Add a menu item or button to open the settings form:
```vb
Private Sub mnuSettings_Click(sender As Object, e As EventArgs)
    Dim frmSettings As New FrmPosSettings()
    frmSettings.ShowDialog()
End Sub
```

### 2. Initialize at Startup
Load settings when your application starts:
```vb
Private Sub MainForm_Load(sender As Object, e As EventArgs)
    PosSettingsManager.LoadSettings()
    ' Use settings to configure your application
End Sub
```

### 3. Use Throughout Application
Access settings anywhere in your code:
```vb
' In your sales calculation logic
Dim taxAmount As Decimal = subtotal * (PosSettingsManager.TaxRate / 100)

' In your discount logic
If PosSettingsManager.EnableDiscount Then
    ' Apply discount logic
End If
```

## File Structure
```
Master/
├── FrmPosSettings.vb                    ' Main settings form
├── FrmPosSettings.Designer.vb           ' Form designer
├── TestPosSettingsForm.vb               ' Test/demo form
└── PosSettingsIntegrationExamples.vb    ' Integration examples

ClsModule/
└── PosSettingsManager.vb                ' Core business logic
```

## Error Handling
The system includes comprehensive error handling:
- **Network Errors**: Handled gracefully with user feedback
- **Data Validation**: Required field validation
- **Type Conversion**: Safe conversion with default values
- **User Feedback**: Clear error messages and success confirmations

## Keyboard Shortcuts
- **F5**: Refresh data from server
- **Ctrl+S**: Save current setting
- **Ctrl+N**: Create new setting
- **Esc**: Close form

## Customization
You can easily extend the system by:
1. Adding new setting types in the Type dropdown
2. Creating additional predefined properties in PosSettingsManager
3. Customizing the form layout using DevExpress Layout Control
4. Adding additional validation rules

## Security Considerations
- Settings are loaded through your existing authentication system
- All CRUD operations respect your current user permissions
- No direct database access - all operations go through your PHP backend

## Performance
- Settings are cached in memory after first load
- Automatic refresh after save/update operations
- Minimal network calls through efficient caching strategy

## Troubleshooting

### Common Issues
1. **Settings not loading**: Check PHP backend URL configuration
2. **Save failures**: Verify JSON format and required fields
3. **Display issues**: Ensure DevExpress components are properly referenced

### Debug Steps
1. Test with the provided TestPosSettingsForm
2. Check network connectivity to PHP backend
3. Verify database table structure matches expected schema
4. Review error messages in the application

## Future Enhancements
Potential improvements could include:
- Setting categories/groups
- Setting validation rules
- Backup/restore functionality
- Setting history/audit trail
- Import/export capabilities
- Role-based setting access
