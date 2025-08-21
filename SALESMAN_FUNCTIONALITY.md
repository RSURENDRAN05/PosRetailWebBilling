# Salesman Selection Functionality Documentation

## Overview
This document describes the implementation of salesman selection functionality for individual items in the POS Sales system. Each item can be assigned to a specific salesman with their commission percentage.

## Features Implemented

### 1. Database Schema Updates
- **SALESMANPER Column**: Added to GridDataTble_Insert to store commission percentage for each item
- Updated column order: SNO, BARCODE, ITEMCODE, ITEMNAME, SERIALNO, UOM, RATE, QTY, TAMOUNT, ITEM_DPER, ITEM_DAMT, BILL_DPER, BILL_DAMT, TOTAL_DPER, TOTAL_DAMT, GAMOUNT, TAXVALUE, TAXAMT, NETAMT, ITEMREMARS, BATCHNO, SALESPERSONID, SALESPERSON, SALESMANPER, DELETE

### 2. FrmSalesmanList Form
**Purpose**: Salesman selection and management dialog

**Features**:
- Grid display of all salesmen with ID, Name, Commission %, Phone, Active status
- Selection mode for choosing salesman during item addition
- Management mode for salesman administration
- Double-click selection functionality
- Dedicated Select/Cancel buttons in selection mode

**Properties**:
```vb
Public ReadOnly Property SelectedSalesmanId As Integer
Public ReadOnly Property SelectedSalesmanName As String
Public ReadOnly Property SelectedSalesmanPercentage As Decimal
```

### 3. Item Addition Process (_InsertDt Method)

**Conditional Salesman Selection**:
```vb
If _globalSetting.SalesManEachItemActive = True Then
    ' Open salesman selection dialog for each new item
    Dim salesmanForm As New FrmSalesmanList(True)
    If salesmanForm.ShowDialog() = DialogResult.OK Then
        ' Assign selected salesman to item
    End If
End If
```

**Data Storage**:
- SALESPERSONID: Selected salesman's unique identifier
- SALESPERSON: Selected salesman's name
- SALESMANPER: Selected salesman's commission percentage

### 4. Salesman Management Methods

#### UpdateItemSalesman()
- Updates salesman information for a specific grid item
- Parameters: rowIndex, salesmanId, salesmanName, salesmanPercentage
- Refreshes grid display after update

#### GetSalesmanSummary()
- Returns comprehensive salesman performance summary
- Groups by salesman ID with total sales, commission, and item count
- Used for reporting and commission calculations

#### AssignDefaultSalesmanToAllItems()
- Assigns same salesman to all items in current bill
- Useful for bulk assignment scenarios
- Updates entire DataTable and refreshes grid

### 5. User Interface Integration

**Salesman Selection Button** (`barselectsalesman`):
- Available when `_globalSetting.SalesManEachItemActive = True`
- Allows changing salesman for selected grid item
- Shows confirmation message after successful assignment

**Grid Integration**:
- Salesman information displayed in grid columns
- Real-time updates when salesman is changed
- Visual feedback for salesman assignments

### 6. Business Logic Flow

#### New Item Addition:
1. Item scanned/selected through normal process
2. If SalesManEachItemActive = True:
   - FrmSalesmanList dialog opens
   - User selects salesman
   - Item added with salesman information
3. If SalesManEachItemActive = False:
   - Item added with default salesman (ID=1, Name="Default", Percentage=0)

#### Existing Item Modification:
1. User selects item in grid
2. Clicks salesman selection button
3. FrmSalesmanList dialog opens
4. User selects new salesman
5. Grid item updated with new salesman information

### 7. Error Handling
- Comprehensive try-catch blocks in all methods
- User-friendly error messages
- Graceful handling of selection cancellation
- Validation of grid selection before salesman assignment

### 8. Sample Data Structure

**Salesman Data**:
```vb
ID | Name        | Percentage | Phone       | Active
1  | John Smith  | 5.0        | 123-456-7890| True
2  | Jane Doe    | 7.5        | 098-765-4321| True
3  | Mike Johnson| 6.0        | 555-123-4567| True
```

**Grid Data with Salesman Info**:
```
ItemCode | ItemName | Rate | Qty | SalesPersonID | SalesPerson | SalesManPer
101      | Widget A | 10.00| 2   | 2            | Jane Doe    | 7.5
102      | Widget B | 15.00| 1   | 1            | John Smith  | 5.0
```

### 9. Commission Calculation
```vb
For Each item in GridData:
    ItemAmount = Rate * Qty (after discounts and tax)
    CommissionAmount = ItemAmount * (SalesManPer / 100)
```

### 10. Integration Points

**Settings Integration**:
- Controlled by `_globalSetting.SalesManEachItemActive` flag
- Automatic/Manual salesman assignment modes

**Reporting Integration**:
- GetSalesmanSummary() provides data for commission reports
- Individual item commission tracking
- Salesman performance metrics

**Data Persistence**:
- Salesman information saved with each transaction
- Historical commission tracking capability
- Audit trail for salesman assignments

### 11. Future Enhancements
- API integration for salesman data loading
- Advanced commission calculation rules
- Salesman-specific pricing tiers
- Commission approval workflow
- Real-time commission dashboard

### 12. Testing Scenarios
1. **New Item with Active Setting**: Verify salesman dialog opens
2. **New Item with Inactive Setting**: Verify default salesman assigned
3. **Existing Item Update**: Verify salesman can be changed
4. **Grid Display**: Verify salesman information shows correctly
5. **Summary Calculation**: Verify commission calculations are accurate
6. **Error Handling**: Verify graceful handling of cancellations and errors

## Usage Instructions

### For Users:
1. **Adding Items**: When SalesManEachItemActive is enabled, select salesman for each new item
2. **Changing Salesman**: Select item in grid, click salesman button, choose new salesman
3. **Viewing Commission**: Check grid columns for salesman and percentage information

### For Administrators:
1. **Enable Feature**: Set SalesManEachItemActive = True in global settings
2. **Manage Salesmen**: Use FrmSalesmanList in management mode
3. **View Reports**: Use GetSalesmanSummary() for commission reporting

This implementation provides comprehensive salesman tracking with flexible assignment options and detailed reporting capabilities.
