# Multiple Price Selection System Documentation

## Overview
The Multiple Price Selection System allows users to select from different pricing options for each item in the POS system. This includes wholesale prices, VIP customer rates, staff discounts, bulk order prices, and more.

## Database Schema

### Table: `item_multiple_price`
```sql
CREATE TABLE IF NOT EXISTS `item_multiple_price` (
  `price_id` int(11) NOT NULL AUTO_INCREMENT,
  `item_id` int(11) NOT NULL,
  `price_name` varchar(100) NOT NULL,
  `price_value` decimal(10,2) NOT NULL DEFAULT '0.00',
  `com_id` int(11) NOT NULL DEFAULT '1',
  `loc_id` int(11) NOT NULL DEFAULT '1',
  `status` tinyint(1) NOT NULL DEFAULT '1',
  `created` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`price_id`),
  KEY `idx_item_id` (`item_id`),
  KEY `idx_status` (`status`),
  KEY `idx_company_location` (`com_id`,`loc_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
```

## Features

### 1. Multiple Price Selection Form (`FrmMultiplePriceSelection`)
- **Purpose**: Display available price options for a selected item
- **Features**:
  - Shows all active price options for the selected item
  - Grid display with price name and value
  - Double-click or Enter key selection
  - Cancel option available
  - Company/Location filtered results

### 2. Integration with POS Sales Form (`PosSalesII`)
- **Keyboard Shortcuts**:
  - `Ctrl+P`: Open multiple price selection for focused item
  - `F2`: Open multiple price selection for focused item
- **Button**: `barbtnselectprice` button click handler
- **Methods**:
  - `ShowMultiplePriceSelection()`: Main method to display price selection
  - `ApplyMultiplePrice()`: Apply selected price to item
  - `HasMultiplePrices()`: Check if item has multiple prices available

### 3. API Endpoints (`multiple_price_api.php`)
- **Request Types**:
  - `MultiplePriceRequest=1`: Get multiple prices for an item
  - `MultiplePriceRequest=2`: Add new multiple price
  - `MultiplePriceRequest=3`: Update existing multiple price
  - `MultiplePriceRequest=4`: Delete multiple price
  - `MultiplePriceRequest=5`: Get all multiple prices for management

## Usage Instructions

### For End Users (POS Operation):

1. **Add items to the bill** using normal product search process
2. **Select an item** in the grid that you want to change the price for
3. **Open price selection** using one of these methods:
   - Press `Ctrl+P`
   - Press `F2`
   - Click the "Select Price" button (if available)
4. **Choose desired price** from the list:
   - Double-click on the price option, OR
   - Select and press Enter, OR
   - Select and click "Select Price" button
5. **Price is applied** automatically with recalculated totals
6. **Confirmation message** shows the applied price

### For Administrators (Price Management):

#### Adding Multiple Prices via Database:
```sql
INSERT INTO `item_multiple_price` (`item_id`, `price_name`, `price_value`, `com_id`, `loc_id`, `status`)
VALUES (1, 'Wholesale', 85.00, 1, 1, 1);
```

#### Adding Multiple Prices via API:
```
POST: /API/multiple_price_api.php?MultiplePriceRequest=2
Parameters:
- ItemId: 1
- PriceName: "Wholesale"
- PriceValue: 85.00
- ComId: 1
- LocId: 1
```

## Sample Price Types

### Common Price Categories:
1. **Regular Price** - Standard retail price
2. **Wholesale** - Bulk/wholesale discount price
3. **VIP Customer** - Premium customer pricing
4. **Staff Discount** - Employee discount price
5. **Bulk Order** - Large quantity discount
6. **Student Discount** - Educational institution pricing
7. **Corporate Rate** - Business customer pricing
8. **Member Price** - Loyalty program pricing
9. **Seasonal Offer** - Promotional/seasonal pricing
10. **Quick Sale** - Clearance/quick sale pricing

## Technical Implementation

### Key Classes and Methods:

1. **FrmMultiplePriceSelection.vb**
   - `LoadMultiplePrices()`: Load prices from server
   - `GetMultiplePricesFromServer()`: API call to get prices
   - `SetupPriceGrid()`: Configure grid display
   - `btnSelectPrice_Click()`: Handle price selection

2. **PosSalesII.vb**
   - `ShowMultiplePriceSelection()`: Display price selection form
   - `ApplyMultiplePrice()`: Apply selected price to item
   - `RecalculateRowAmounts()`: Recalculate item totals after price change
   - `HasMultiplePrices()`: Check if item has multiple prices

### Data Flow:
1. User selects item and triggers price selection
2. System gets item ID and name from selected row
3. API call fetches all available prices for the item
4. Price selection form displays options
5. User selects desired price
6. Selected price is applied to the item row
7. All amounts are recalculated (tax, discount, totals)
8. Grid and grand totals are refreshed

## Configuration

### Required Settings:
- Ensure `M_Details.LinkAjaxRequest` points to your API endpoint
- Database connection configured in `multiple_price_api.php`
- Company ID (`_companyInfo.ComId`) and Location ID (`_companyInfo.LocId`) properly set

### Security Considerations:
- API validates company and location access
- Only active prices (`status = 1`) are returned
- Input validation on all price operations

## Benefits

1. **Flexible Pricing**: Support for different customer types and scenarios
2. **Easy Selection**: Intuitive interface for price selection
3. **Real-time Updates**: Immediate recalculation of totals
4. **Audit Trail**: Created/updated timestamps for price changes
5. **Multi-tenant**: Support for multiple companies and locations
6. **Performance**: Efficient database queries with proper indexing

## Troubleshooting

### Common Issues:
1. **No prices shown**: Check if item has multiple prices in database
2. **API errors**: Verify database connection and API endpoint
3. **Wrong prices**: Ensure company/location IDs are correct
4. **Permission issues**: Check user access rights for price selection

### Debug Steps:
1. Check database for item_multiple_price records
2. Verify API response in browser/tools
3. Check application logs for exceptions
4. Validate company/location context

## Future Enhancements

### Possible Additions:
1. **Price History**: Track price change history
2. **Conditional Pricing**: Time-based or quantity-based pricing
3. **Customer-specific Pricing**: Prices based on customer type
4. **Approval Workflow**: Require approval for certain price selections
5. **Bulk Price Updates**: Update multiple item prices simultaneously
6. **Price Templates**: Predefined price sets for quick application
