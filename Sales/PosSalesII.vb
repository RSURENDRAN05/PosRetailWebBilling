Imports System.Net
Imports Newtonsoft.Json.Linq
Imports System.Drawing
Imports Newtonsoft.Json
Imports System.IO
Imports System.Data.SqlClient

' PosSalesII Form - Enhanced with Grid Layout Management
' Features:
' - Automatic layout save on form close
' - Automatic layout load on form open
' - Manual save layout via btnSaveLayout button
' - Reset to default layout functionality
' - Export/Import layout to/from external files
' - Layout files stored in: %AppData%\PosRetailWebBilling\Layouts\
Public Class PosSalesII
    Dim GridDataTble_Insert As DataTable
    Dim Errstr As String
    Dim _SnoCount As Integer = 0
    Dim modeOfSale As String = "New"
    ' Customer Selection Variables
    Private selectedCustomerId As Integer = 0
    Private selectedCustomerName As String = String.Empty
    Private selectedCustomerPhone As String = String.Empty
    Private customerDisplayTable As DataTable
    Private _CashDraw As New RawPrinter
    Dim salesHelper As New SalesDBHelper(M_Details._Conn)
#Region "InialLoad"
    Public Function CreateSalesDataTable() As DataTable
        Try
            GridDataTble_Insert = New DataTable
            GridDataTble_Insert.TableName = "SalesData"
            GridDataTble_Insert.Columns.Add("SNO", GetType(Integer)).AutoIncrement = True  '0
            GridDataTble_Insert.Columns.Add("BARCODE", GetType(Integer)).DefaultValue = 0 '0
            GridDataTble_Insert.Columns.Add("ITEMCODE", GetType(Integer)) '1
            GridDataTble_Insert.Columns.Add("ITEMNAME", GetType(String)) '2
            GridDataTble_Insert.Columns.Add("SERIALNO", GetType(String)).DefaultValue = 0 '2
            GridDataTble_Insert.Columns.Add("UOM", GetType(String)) '2
            GridDataTble_Insert.Columns.Add("RATE", GetType(Decimal)) '3
            GridDataTble_Insert.Columns.Add("QTY", GetType(Decimal)) '4
            GridDataTble_Insert.Columns.Add("TAMOUNT", GetType(Decimal)) '5

            ' Item Discount Columns
            GridDataTble_Insert.Columns.Add("ITEM_DPER", GetType(Decimal)).DefaultValue = 0 '6 - Item Discount Percentage
            GridDataTble_Insert.Columns.Add("ITEM_DAMT", GetType(Decimal)).DefaultValue = 0 '7 - Item Discount Amount

            ' Bill Discount Columns
            GridDataTble_Insert.Columns.Add("BILL_DPER", GetType(Decimal)).DefaultValue = 0 '8 - Bill Discount Percentage
            GridDataTble_Insert.Columns.Add("BILL_DAMT", GetType(Decimal)).DefaultValue = 0 '9 - Bill Discount Amount

            ' Total Discount Columns (Combined Item + Bill)
            GridDataTble_Insert.Columns.Add("TOTAL_DPER", GetType(Decimal)).DefaultValue = 0 '10 - Total Discount Percentage
            GridDataTble_Insert.Columns.Add("TOTAL_DAMT", GetType(Decimal)).DefaultValue = 0 '11 - Total Discount Amount

            GridDataTble_Insert.Columns.Add("GAMOUNT", GetType(Decimal)) '12
            GridDataTble_Insert.Columns.Add("TAXVALUE", GetType(Integer)).DefaultValue = 0 '13
            GridDataTble_Insert.Columns.Add("TAXAMT", GetType(Decimal)).DefaultValue = 0 '14
            GridDataTble_Insert.Columns.Add("NETAMT", GetType(Decimal)) '15
            GridDataTble_Insert.Columns.Add("ITEMREMARS", GetType(String)).DefaultValue = "Notes" '16
            GridDataTble_Insert.Columns.Add("BATCHNO", GetType(Integer)).DefaultValue = 0 '17
            GridDataTble_Insert.Columns.Add("SALESPERSONID", GetType(Integer)).DefaultValue = 0 '18
            GridDataTble_Insert.Columns.Add("SALESPERSON", GetType(String)).DefaultValue = "SP" '19
            GridDataTble_Insert.Columns.Add("SALESMANPER", GetType(Decimal)).DefaultValue = 0 '20 - Salesman Commission Percentage
            GridDataTble_Insert.Columns.Add("DELETE", GetType(Integer)).DefaultValue = 1 '21
            GridDataTble_Insert.Columns.Add("ITEMLOCK", GetType(Integer)).DefaultValue = 1 '22 - Item Lock Status
            GridDataTble_Insert.Columns.Add("PSID", GetType(Integer)).DefaultValue = 0 '23 - psID Database Autoid for recall after insert

            Return GridDataTble_Insert
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub PosSalesII_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            GridControlSalesData.DataSource = CreateSalesDataTable()
            ' Load grid layout after setting data source
            LoadGridLayout()
            ' Load complete form layout
            RestoreFormLayout()
            InitializeCustomerGrid()
            InitialLoad()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub InitialLoad()
        Try
            barbtnposstatus.Caption = "PMID-" & _companyInfo.CompanyPMID & "-" & _companyInfo.ComId & "-" & _companyInfo.CompanyName & "-" & _companyInfo.LocId & "-" & _companyInfo.LocationName
            BarDate.Caption = DateTime.Now
            Barshiftno.Caption = "ShiftNo : " & _saleSetting._curShiftno
            Bardayno.Caption = "DayNo : " & _saleSetting._curDayno
            baruserinfomation.Caption = "UserName : " & _companyInfo.UserId & " - " & _companyInfo.UserName
            lblinvoicedate.Text = DateTime.Now.ToString("dd-MM-yyyy")
            ClearCurrentBill()
            If _JsonData.ItemMasterTable.Rows.Count > 0 Then
                GridControl2.DataSource = _JsonData.ItemMasterTable.DefaultView
                dtview = _JsonData.ItemMasterTable.DefaultView
            End If
            LoadButtonStyles()
            mainMenu()
            If _globalSetting.SearchProductCode = True Then
                barSearchProductCode.Checked = True
            Else
                barSearchProductCode.Checked = False
            End If
            If _globalSetting.TaxExculsive = True Then
                barstatustaxtype.Caption = "Tax Exclusive"
            Else
                barstatustaxtype.Caption = "Tax Inclusive"
            End If
            'If _JsonData.PaymentTermTable.Rows.Count = 0 Then
            '    getPaymentTermTable()
            'Else
            '    CmbPaymentTerm.Properties.Items.Clear()
            '    For Each cmbpayment In _JsonData.PaymentTermTable.Rows
            '        Dim values = cmbpayment("Name")
            '        CmbPaymentTerm.Properties.Items.Add(values)
            '    Next
            '    CmbPaymentTerm.SelectedIndex = 0
            'End If
            'If _printProfileLoad() = False Then

            'End If

        Catch ex As Exception

        End Try
    End Sub

    'Public Function _printProfileLoad() As Boolean
    '    Try
    '        If File.Exists(M_Details._appPath & "\Settings\PrintProfileSetting.xml") Then
    '            Dim str() As String = {"Sales"}
    '            Dim _dsPrintProfile As New DataSet
    '            _dsPrintProfile.ReadXml(M_Details._appPath & "\Settings\PrintProfileSetting.xml")
    '            Dim sas = From profile In _dsPrintProfile.Tables(0).AsEnumerable Where profile.Field(Of String)("ProfileType") = "Sales" Select profile

    '            Dim tabl As New DataTable
    '            If sas.Count > 0 Then
    '                txtprintprofile.Properties.Items.Clear()
    '                tabl = sas.CopyToDataTable
    '                For Each _Drows As DataRow In tabl.Rows
    '                    txtprintprofile.Properties.Items.Add(_Drows("FileName"))
    '                Next
    '            End If
    '            txtprintprofile.SelectedIndex = 0
    '        End If
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function
    'Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
    '    Try
    '        If _getBillno() = False Then

    '        End If
    '        modeOfSale = "New"
    '        barbtnstatus.Caption = "Sales Mode : " & modeOfSale
    '        txttaxamt.Text = "0.00"
    '        txtnetamt.Text = "0.00"
    '        txttotamount.Text = "0.00"
    '        txtdiscamt.Text = "0.00"
    '        _SnoCount = 0
    '        GridDataTble_Insert.Rows.Clear()
    '        cmbMaterialSearch.Focus()
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Public Function _getBillno() As Boolean
    '    Try
    '        Dim dt As New DataTable
    '        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
    '        Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesRequest=2&BillType=SAL")
    '        Dim Userparsejson As JObject = JObject.Parse(json)
    '        Dim dtresults = Userparsejson("Success")
    '        If dtresults.ToString = "True" Then
    '            dt = Userparsejson("Data").ToObject(Of DataTable)()
    '            Dim billno As Integer = 0
    '            Dim prefix As String = ""
    '            billno = dt.Rows(0)(1)
    '            prefix = dt.Rows(0)(2)
    '            'txtinvoiceno.Text = billno + 1 'prefix & ("0000" & billno + 1)
    '            Return True
    '        Else
    '            Return False
    '        End If
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Function
#End Region
#Region "SaveLayOut"

    Private Sub btnSaveLayout_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSaveLayout.ItemClick
        Try
            SaveGridLayout()
            MessageBox.Show("Grid layout saved successfully!", "Save Layout", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error saving grid layout: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveGridLayout()
        Try
            Dim layoutPath As String = GetLayoutFilePath()
            ' Create directory if it doesn't exist
            Dim layoutDir As String = System.IO.Path.GetDirectoryName(layoutPath)
            If Not System.IO.Directory.Exists(layoutDir) Then
                System.IO.Directory.CreateDirectory(layoutDir)
            End If

            ' Save the grid view layout
            GridViewPOS.SaveLayoutToXml(layoutPath)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LoadGridLayout()
        Try
            Dim layoutPath As String = GetLayoutFilePath()
            If System.IO.File.Exists(layoutPath) Then
                GridViewPOS.RestoreLayoutFromXml(layoutPath)
            End If
        Catch ex As Exception
            ' If there's an error loading the layout, just continue with default layout
            ' This prevents the form from failing to load if the layout file is corrupted
        End Try
    End Sub

    Private Function GetLayoutFilePath() As String
        ' Create a layout file path in the application's folder
        Dim appPath As String = Application.StartupPath
        Dim layoutFolder As String = System.IO.Path.Combine(appPath, "Layout")
        Return System.IO.Path.Combine(layoutFolder, "PosSalesII_GridLayout.xml")
    End Function

    ' Auto-save layout when form is closing
    Private Sub PosSalesII_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            SaveGridLayout()
        Catch ex As Exception
            ' Ignore errors during auto-save to prevent form closing issues
        End Try
    End Sub

    ' Method to reset grid layout to default
    Public Sub ResetGridLayoutToDefault()
        Try
            Dim layoutPath As String = GetLayoutFilePath()
            If System.IO.File.Exists(layoutPath) Then
                System.IO.File.Delete(layoutPath)
            End If
            ' Reset to default layout
            GridViewPOS.BestFitColumns()
            MessageBox.Show("Grid layout reset to default!", "Reset Layout", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error resetting grid layout: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Method to manually load layout (can be called from a button or menu)
    Public Sub LoadGridLayoutManually()
        Try
            LoadGridLayout()
            MessageBox.Show("Grid layout loaded successfully!", "Load Layout", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error loading grid layout: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Method to export layout to a file
    Public Sub ExportGridLayout()
        Try
            Dim saveFileDialog As New SaveFileDialog()
            saveFileDialog.Filter = "XML files (*.xml)|*.xml"
            saveFileDialog.Title = "Export Grid Layout"
            saveFileDialog.FileName = "PosSalesII_GridLayout_Export.xml"

            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                GridViewPOS.SaveLayoutToXml(saveFileDialog.FileName)
                MessageBox.Show("Grid layout exported successfully!", "Export Layout", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error exporting grid layout: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Method to import layout from a file
    Public Sub ImportGridLayout()
        Try
            Dim openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "XML files (*.xml)|*.xml"
            openFileDialog.Title = "Import Grid Layout"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                GridViewPOS.RestoreLayoutFromXml(openFileDialog.FileName)
                ' Also save this as the current layout
                SaveGridLayout()
                MessageBox.Show("Grid layout imported successfully!", "Import Layout", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error importing grid layout: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Simplified method to load button styles from ButtonStyleTable
    ''' </summary>
    Private Sub LoadButtonStyles()
        Try
            If _JsonData.ButtonStyleTable Is Nothing OrElse _JsonData.ButtonStyleTable.Rows.Count = 0 Then
                Exit Sub
            End If

            ' Create a dictionary for faster lookups
            Dim styleDict As New Dictionary(Of String, DataRow)

            For Each row As DataRow In _JsonData.ButtonStyleTable.Rows
                Dim styleName As String = row("Name").ToString().ToUpper()
                If Not styleDict.ContainsKey(styleName) Then
                    styleDict.Add(styleName, row)
                End If
            Next

            ' Apply button styles using the dictionary - access shared properties directly
            If styleDict.ContainsKey("MAINH") Then
                ButtonStyleWH.MAINH = GetStyleValue(styleDict("MAINH"), "Value", 70).ToString() ' Default height 70
            End If

            If styleDict.ContainsKey("MAINW") Then
                ButtonStyleWH.MAINW = GetStyleValue(styleDict("MAINW"), "Value", 150).ToString() ' Default width 150
            End If

            If styleDict.ContainsKey("SUBH") Then
                ButtonStyleWH.SUBH = GetStyleValue(styleDict("SUBH"), "Value", 60).ToString() ' Default height 60
            End If

            If styleDict.ContainsKey("SUBW") Then
                ButtonStyleWH.SUBW = GetStyleValue(styleDict("SUBW"), "Value", 120).ToString() ' Default width 120
            End If

            If styleDict.ContainsKey("ITEMH") Then
                ButtonStyleWH.ITEMH = GetStyleValue(styleDict("ITEMH"), "Value", 80).ToString() ' Default height 80
            End If

            If styleDict.ContainsKey("ITEMW") Then
                ButtonStyleWH.ITEMW = GetStyleValue(styleDict("ITEMW"), "Value", 200).ToString() ' Default width 200
            End If

            If styleDict.ContainsKey("MAINLOAD") Then
                ButtonStyleWH.MAINLOAD = GetStyleValue(styleDict("MAINLOAD"), "Value", 10).ToString() ' Default load count
            End If

            If styleDict.ContainsKey("SUBLOAD") Then
                ButtonStyleWH.SUBLOAD = GetStyleValue(styleDict("SUBLOAD"), "Value", 15).ToString() ' Default load count
            End If

            If styleDict.ContainsKey("ITEMLOAD") Then
                ButtonStyleWH.ITEMLOAD = GetStyleValue(styleDict("ITEMLOAD"), "Value", 20).ToString() ' Default load count
            End If

        Catch ex As Exception
            ' Log error or handle silently - don't break the form loading
            System.Diagnostics.Debug.WriteLine("Error loading button styles: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Helper method to safely get style values with default fallback
    ''' </summary>
    Private Function GetStyleValue(row As DataRow, columnName As String, defaultValue As Integer) As Integer
        Try
            If row Is Nothing Then Return defaultValue

            Dim value As Object = row(columnName)
            If value Is Nothing OrElse value Is DBNull.Value Then Return defaultValue

            Dim result As Integer
            If Integer.TryParse(value.ToString(), result) Then
                Return If(result > 0, result, defaultValue)
            Else
                Return defaultValue
            End If
        Catch
            Return defaultValue
        End Try
    End Function

    ''' <summary>
    ''' Helper method to safely convert database color values to System.Drawing.Color with fallback
    ''' Supports both hex codes (#FF0000, FF0000) and color names (Red, Blue, Green, etc.)
    ''' </summary>
    Private Function GetSafeColor(row As DataRow, columnName As String, defaultColor As Color) As Color
        Try
            If row Is Nothing Then Return defaultColor

            Dim colorValue As Object = row(columnName)
            If colorValue Is Nothing OrElse colorValue Is DBNull.Value Then Return defaultColor

            Dim colorString As String = colorValue.ToString().Trim()
            If String.IsNullOrWhiteSpace(colorString) Then Return defaultColor

            ' First, try to handle hex colors
            If colorString.StartsWith("#") OrElse System.Text.RegularExpressions.Regex.IsMatch(colorString, "^[0-9A-Fa-f]{6}$") Then
                ' Ensure color string starts with # for hex colors
                If Not colorString.StartsWith("#") Then
                    ' Try to add # if it looks like hex without it
                    If colorString.Length = 6 AndAlso System.Text.RegularExpressions.Regex.IsMatch(colorString, "^[0-9A-Fa-f]{6}$") Then
                        colorString = "#" & colorString
                    Else
                        Return defaultColor
                    End If
                End If

                ' Try to convert the hex color
                Return ColorTranslator.FromHtml(colorString)
            Else
                ' Try to handle named colors (Red, Blue, Green, etc.)
                Try
                    ' Use Color.FromName to convert color names
                    Dim namedColor As Color = Color.FromName(colorString)

                    ' Check if the color name is valid (not a system color with ARGB = 0)
                    If namedColor.IsKnownColor OrElse namedColor.ToArgb() <> 0 Then
                        Return namedColor
                    Else
                        ' If it's not a valid color name, return default
                        Return defaultColor
                    End If
                Catch ex As Exception
                    ' If named color conversion fails, return default
                    Return defaultColor
                End Try
            End If

        Catch ex As Exception
            ' Return default color if any conversion fails
            Return defaultColor
        End Try
    End Function
#End Region
#Region "LoadMenu"
    Private Sub mainMenu()
        Try
            Try

                FlowLayoutPanelMain.Controls.Clear()
                ' Set FlowLayoutPanel properties for perfect layout
                FlowLayoutPanelMain.FlowDirection = FlowDirection.LeftToRight
                FlowLayoutPanelMain.WrapContents = True
                FlowLayoutPanelMain.AutoScroll = True
                ' Add dynamic SimpleButtons for each category with perfect fit styling

                ' For initial load, use ButtonStyleWH.SUBLOAD as default index
                Dim firstMainGroupId As Integer = Convert.ToInt32(ButtonStyleWH.SUBLOAD)
                If _JsonData.MainGroupTable.Rows.Count > 0 Then
                    For Each row As DataRow In _JsonData.MainGroupTable.Rows
                        Dim btn As New DevExpress.XtraEditors.SimpleButton()

                        ' Button text and data
                        btn.Text = row("MainName").ToString()
                        btn.Tag = row("MainId")

                        ' Button styling and dimensions for perfect fit with even padding
                        btn.Size = New Size(ButtonStyleWH.SUBW, ButtonStyleWH.SUBH)
                        btn.Font = New Font("Segoe UI", 10, FontStyle.Bold) ' Larger font for better readability



                        ' DevExpress SimpleButton specific properties with enhanced styling
                        btn.Appearance.BackColor = GetSafeColor(row, "Color", Color.FromArgb(52, 152, 219)) ' Blue default for MainGroup
                        btn.Appearance.ForeColor = Color.Black
                        btn.Appearance.Font = New Font("Segoe UI", 10, FontStyle.Bold) ' Consistent larger font
                        btn.Appearance.Options.UseBackColor = True
                        btn.Appearance.Options.UseForeColor = True
                        btn.Appearance.Options.UseFont = True
                        btn.Appearance.Options.UseTextOptions = True
                        btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        btn.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                        btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        btn.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter ' Handle long text gracefully


                        btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
                        btn.LookAndFeel.UseDefaultLookAndFeel = False

                        ' Add border for better visual separation
                        btn.Appearance.BorderColor = Color.FromArgb(200, 200, 200)
                        btn.Appearance.Options.UseBorderColor = True

                        ' Event handler
                        AddHandler btn.Click, AddressOf MainGroupButton_Click

                        ' Add to panel
                        FlowLayoutPanelMain.Controls.Add(btn)

                    Next

                    ' Force layout update to ensure proper sizing
                    FlowLayoutPanelMain.PerformLayout()

                    ' Load submenu for the initial default main group ID
                    If firstMainGroupId > 0 Then
                        subMenu(firstMainGroupId, True) ' True indicates initial load
                    End If

                End If

            Catch ex As Exception
                ' Handle error silently or log if needed
            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub subMenu(ByRef DefaultMainId As Integer, Optional ByVal isInitialLoad As Boolean = False)
        Try

            FlowLayoutPanelSubMenu.Controls.Clear()
            ' Set FlowLayoutPanel properties for perfect layout
            FlowLayoutPanelSubMenu.FlowDirection = FlowDirection.TopDown
            FlowLayoutPanelSubMenu.WrapContents = True
            FlowLayoutPanelSubMenu.AutoScroll = True
            ' Add dynamic SimpleButtons for each category with perfect fit styling

            Dim selectedMainId As Integer = DefaultMainId ' Store selected main group ID
            Dim firstCategoryId As Integer = 0 ' Will store the first category ID for product loading

            If _JsonData.CategoryTable.Rows.Count > 0 Then
                Dim filteredCate = From item In _JsonData.CategoryTable.AsEnumerable() _
                                  Where IsNumeric(item("MainId")) AndAlso Convert.ToInt32(item("MainId")) = selectedMainId _
                                  Select item

                If filteredCate.Any() Then
                    Dim filteredTable As DataTable = filteredCate.CopyToDataTable()

                    ' Determine which category to load for products
                    If isInitialLoad Then
                        ' For initial load, use ButtonStyleWH.ITEMLOAD as index
                        Dim itemLoadIndex As Integer = Convert.ToInt32(ButtonStyleWH.ITEMLOAD)
                        If itemLoadIndex < filteredTable.Rows.Count Then
                            firstCategoryId = Convert.ToInt32(filteredTable.Rows(itemLoadIndex)("CateID"))
                        Else
                            ' If index is out of range, use first category
                            firstCategoryId = Convert.ToInt32(filteredTable.Rows(0)("CateID"))
                        End If
                    Else
                        ' For user clicks, always use first category (index 0)
                        firstCategoryId = Convert.ToInt32(filteredTable.Rows(0)("CateID"))
                    End If
                    For Each row As DataRow In filteredTable.Rows
                        Dim btn As New DevExpress.XtraEditors.SimpleButton()

                        ' Button text and data
                        btn.Text = row("CateName").ToString()
                        btn.Tag = row("CateID")


                        ' Button styling and dimensions for perfect fit with even padding
                        btn.Size = New Size(ButtonStyleWH.SUBW, ButtonStyleWH.SUBH)
                        btn.Font = New Font("Segoe UI", 10, FontStyle.Bold) ' Larger font for better readability



                        ' DevExpress SimpleButton specific properties with enhanced styling
                        btn.Appearance.BackColor = GetSafeColor(row, "Color", Color.FromArgb(46, 204, 113)) ' Green default for SubMenu
                        btn.Appearance.ForeColor = Color.Black
                        btn.Appearance.Font = New Font("Segoe UI", 10, FontStyle.Bold) ' Consistent larger font
                        btn.Appearance.Options.UseBackColor = True
                        btn.Appearance.Options.UseForeColor = True
                        btn.Appearance.Options.UseFont = True
                        btn.Appearance.Options.UseTextOptions = True
                        btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        btn.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                        btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        btn.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter ' Handle long text gracefully


                        btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
                        btn.LookAndFeel.UseDefaultLookAndFeel = False

                        ' Add border for better visual separation
                        btn.Appearance.BorderColor = Color.FromArgb(200, 200, 200)
                        btn.Appearance.Options.UseBorderColor = True

                        ' Event handler
                        AddHandler btn.Click, AddressOf CategoryButton_Click

                        ' Add to panel
                        FlowLayoutPanelSubMenu.Controls.Add(btn)

                        ' Increment color index for next button

                    Next
                End If

                ' Force layout update to ensure proper sizing
                FlowLayoutPanelSubMenu.PerformLayout()

                LoadProductMenu(firstCategoryId)

            End If
        Catch ex As Exception
            ' Handle error silently or log if needed
        End Try
    End Sub
    Private Sub LoadProductMenu(ByRef DefaultCateId As Integer)
        Try
            Dim Cateid As Integer = 0
            Cateid = DefaultCateId

            ' Clear existing product buttons
            FlowLayoutPanelProduct.Controls.Clear()

            If _JsonData.ItemTouchMasterTable.Rows.Count > 0 Then
                ' Filter items by category ID and update the item grid
                Dim filteredItems = From item In _JsonData.ItemTouchMasterTable.AsEnumerable() _
                                   Where IsNumeric(item("CateId")) AndAlso Convert.ToInt32(item("CateId")) = Cateid _
                                   Select item

                If filteredItems.Any() Then
                    ' Create a new DataTable with filtered items
                    Dim filteredTable As DataTable = filteredItems.CopyToDataTable()
                    For Each row As DataRow In filteredTable.Rows
                        Dim btn As New DevExpress.XtraEditors.SimpleButton()

                        ' Button content - Item name and price
                        Dim itemName As String = If(row("ItemName") IsNot Nothing, row("ItemName").ToString(), "Unknown Item")
                        Dim sellPrice As Decimal = 0
                        If IsNumeric(row("SellPrice")) Then
                            sellPrice = Convert.ToDecimal(row("SellPrice"))
                        End If
                        btn.Text = itemName & Environment.NewLine & sellPrice.ToString("C2")

                        ' Store ItemCode in Tag with safety check
                        If IsNumeric(row("Id")) Then
                            btn.Tag = Convert.ToInt32(row("Id"))
                        Else
                            btn.Tag = 0 ' Default value if conversion fails
                        End If

                        ' Enhanced button styling
                        btn.Size = New Size(ButtonStyleWH.ITEMW, ButtonStyleWH.ITEMH)
                        btn.Font = New Font("Segoe UI", 9, FontStyle.Bold)

                        ' Safe color handling using helper function
                        btn.Appearance.BackColor = GetSafeColor(row, "Color", Color.FromArgb(70, 130, 180)) ' Steel Blue default for Products
                        btn.Appearance.ForeColor = Color.Black
                        btn.Appearance.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                        btn.Appearance.Options.UseBackColor = True
                        btn.Appearance.Options.UseForeColor = True
                        btn.Appearance.Options.UseFont = True
                        btn.Appearance.Options.UseTextOptions = True
                        btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        btn.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                        btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap


                        btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
                        btn.LookAndFeel.UseDefaultLookAndFeel = False

                        ' Border styling
                        btn.Appearance.BorderColor = Color.FromArgb(180, 180, 180)
                        btn.Appearance.Options.UseBorderColor = True

                        ' Event handler
                        AddHandler btn.Click, AddressOf ItemButton_Click
                        FlowLayoutPanelProduct.Controls.Add(btn)
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub MainGroupButton_Click(sender As Object, e As EventArgs)
        Try
            ' Get the clicked button and extract the category ID from its Tag
            Dim clickedButton As DevExpress.XtraEditors.SimpleButton = CType(sender, DevExpress.XtraEditors.SimpleButton)
            Dim selectedMainGroupId As Integer = 0

            ' Safely convert the Tag to Integer
            If IsNumeric(clickedButton.Tag) Then
                selectedMainGroupId = Convert.ToInt32(clickedButton.Tag)
            End If

            ' Load submenu for the selected main group (user click - use first index)
            If selectedMainGroupId > 0 Then
                subMenu(selectedMainGroupId, False) ' False indicates user click, not initial load
            End If

        Catch ex As Exception
            ' Handle error silently or log if needed
            DevExpress.XtraEditors.XtraMessageBox.Show("Error loading category: " & ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub CategoryButton_Click(sender As Object, e As EventArgs)
        Try
            ' Get the clicked button and extract the category ID from its Tag
            Dim clickedButton As DevExpress.XtraEditors.SimpleButton = CType(sender, DevExpress.XtraEditors.SimpleButton)
            Dim selectedCategoryId As Integer = 0

            ' Safely convert the Tag to Integer
            If IsNumeric(clickedButton.Tag) Then
                selectedCategoryId = Convert.ToInt32(clickedButton.Tag)
            End If

            ' Load products for the selected category
            If selectedCategoryId > 0 Then
                LoadProductMenu(selectedCategoryId)
            End If

        Catch ex As Exception
            ' Handle error silently or log if needed
            DevExpress.XtraEditors.XtraMessageBox.Show("Error loading category: " & ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ItemButton_Click(sender As Object, e As EventArgs)
        Try
            ' Get the clicked button and extract the item ID from its Tag
            Dim clickedButton As DevExpress.XtraEditors.SimpleButton = CType(sender, DevExpress.XtraEditors.SimpleButton)
            Dim selectedItemId As Integer = 0

            ' Safely convert the Tag to Integer
            If IsNumeric(clickedButton.Tag) Then
                selectedItemId = Convert.ToInt32(clickedButton.Tag)
            End If

            ' Validate that we have a valid item ID
            If selectedItemId <= 0 Then
                DevExpress.XtraEditors.XtraMessageBox.Show("Invalid item selected. Please try again.", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Convert ItemId to string for the Get_product_info function
            Dim itemCodeStr As String = selectedItemId.ToString()
            Dim qty As Decimal = 1 ' Default quantity
            Dim errStr As String = String.Empty

            ' Call Get_product_info with ItemCode mode
            If Get_product_info(itemCodeStr, qty, "ItemCode", errStr) = False Then
                ' Show error message if product info retrieval failed
                DevExpress.XtraEditors.XtraMessageBox.Show(errStr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                ' Success - product added to grid
                ' Focus back to search for potential next item
                cmbMaterialSearch.Focus()
            End If

        Catch ex As Exception
            ' Handle error silently or show error message
            DevExpress.XtraEditors.XtraMessageBox.Show("Error adding product: " & ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region
#Region "KeyDown"
    Private dtview As New DataView
    Private Sub txtsearch2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtsearch2.KeyDown
        Try
            If My.Computer.Keyboard.CtrlKeyDown AndAlso e.KeyCode = Keys.Down Then
                GridView2.Focus()
            ElseIf e.KeyCode = Keys.Down Then
                GridView2.FocusedRowHandle = 0
                GridView2.Focus()
            ElseIf e.KeyCode = Keys.Left Then
                txtMqty.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtsearch2_KeyUp(sender As Object, e As KeyEventArgs) Handles txtsearch2.KeyUp
        Try

            If cmbMaterialSearch.IsPopupOpen Then

                cmbMaterialSearch.EditValue = txtsearch2.EditValue
                If cmbMaterialSearch.Text <> "" Then
                    dtview.RowFilter = ("ITEMNAME like '%" & cmbMaterialSearch.EditValue & "%'")
                    GridControl2.DataSource = dtview
                Else
                    dtview.RowFilter = ("")
                    GridControl2.DataSource = dtview
                End If
            End If
        Catch ex As Exception
            GridControl2.DataSource = Nothing
        End Try
    End Sub

    Private Sub cmbMaterialSearch_QueryCloseUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmbMaterialSearch.QueryCloseUp
        txtsearch2.Focus()
    End Sub

    Private Sub cmbMaterialSearch_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmbMaterialSearch.QueryPopUp
        txtsearch2.Focus()
    End Sub
    Private Sub GridViewPOS_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridViewPOS.RowClick
        Try
            Dim focusedRowHandle As Integer = GridViewPOS.FocusedRowHandle
            If GridViewPOS.FocusedColumn IsNot Nothing AndAlso GridViewPOS.FocusedColumn.FieldName = "DELETE" Then
                DeleteItem(focusedRowHandle)
            End If
            If GridViewPOS.FocusedColumn IsNot Nothing AndAlso GridViewPOS.FocusedColumn.FieldName = "SALESPERSON" Then
                If focusedRowHandle >= 0 AndAlso focusedRowHandle < GridDataTble_Insert.Rows.Count Then
                    barselectsalesman_ItemClick(Nothing, Nothing)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DeleteItem(ByRef focusedRowHandle As Integer)
        Try
            If focusedRowHandle >= 0 AndAlso focusedRowHandle < GridDataTble_Insert.Rows.Count Then
                ' Show confirmation dialog
                Dim itemName = GridDataTble_Insert.Rows(focusedRowHandle)("ITEMNAME")
                Dim itemLock = GridDataTble_Insert.Rows(focusedRowHandle)("ITEMLOCK")
                If modeOfSale = "View" Then
                    MessageBox.Show("You Can't Delete This Item Or Count = 1 -> " & itemName & "?", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Question)
                    Return
                ElseIf modeOfSale = "Edit" AndAlso GridDataTble_Insert.Rows.Count = 1 Then
                    MessageBox.Show("You Can't Delete This Item Or Count = 1 -> " & itemName & "?", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Question)
                    Return
                End If
                If itemLock.ToString = "2" Then
                    If _globalSetting.ItemDeleteActive = True Then

                        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete '" & itemName & "'?", _
                                                                   "Confirm Delete", _
                                                                   MessageBoxButtons.YesNo, _
                                                                   MessageBoxIcon.Question)
                        If result = DialogResult.Yes Then
                            DeleteSelectedRow(focusedRowHandle)
                        End If
                    Else
                        If _companyInfo.UserRoleId = 1 OrElse _companyInfo.UserRoleId = 2 Then

                            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete '" & itemName & "'?", _
                                                                       "Confirm Delete", _
                                                                       MessageBoxButtons.YesNo, _
                                                                       MessageBoxIcon.Question)

                            If result = DialogResult.Yes Then
                                DeleteSelectedRow(focusedRowHandle)
                            End If
                        End If
                    End If
                Else
                    Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete '" & itemName & "'?", _
                                                               "Confirm Delete", _
                                                               MessageBoxButtons.YesNo, _
                                                               MessageBoxIcon.Question)
                    If result = DialogResult.Yes Then
                        DeleteSelectedRow(focusedRowHandle)
                    End If
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GridViewPOS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridViewPOS.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Dim focusedRowHandle As Integer = GridViewPOS.FocusedRowHandle
                If focusedRowHandle >= 0 AndAlso focusedRowHandle < GridDataTble_Insert.Rows.Count Then
                    DeleteItem(focusedRowHandle)
                End If
            ElseIf My.Computer.Keyboard.CtrlKeyDown AndAlso e.KeyCode = Keys.Up Then
                cmbMaterialSearch.Focus()
                '' txtbarcodeNo.Focus()
            ElseIf e.KeyCode = Keys.Up AndAlso GridViewPOS.FocusedRowHandle = 0 Then
                cmbMaterialSearch.Focus()
                ''txtbarcodeNo.Focus()
            ElseIf e.KeyCode = Keys.Escape Then
                cmbMaterialSearch.Focus()
            ElseIf e.KeyCode = Keys.Enter Then
                ' Check if focused column is DELETE column
                If GridViewPOS.FocusedColumn IsNot Nothing AndAlso GridViewPOS.FocusedColumn.FieldName = "DELETE" Then
                    ' Delete focused row
                    Dim focusedRowHandle As Integer = GridViewPOS.FocusedRowHandle
                    If focusedRowHandle >= 0 AndAlso focusedRowHandle < GridDataTble_Insert.Rows.Count Then
                        DeleteItem(focusedRowHandle)
                    End If
                Else
                    GridViewPOS.CloseEditor()
                    cmbMaterialSearch.Focus()
                End If
                ' Quantity control shortcuts
                'ElseIf e.KeyCode = Keys.Add OrElse (My.Computer.Keyboard.ShiftKeyDown AndAlso e.KeyCode = Keys.Oemplus) Then
                '    ' + key: Add 1 to quantity
                '    AddQuantityToItem(1)
                '    e.Handled = True
                'ElseIf e.KeyCode = Keys.Subtract OrElse e.KeyCode = Keys.OemMinus Then
                '    ' - key: Subtract 1 from quantity
                '    SubtractQuantityFromItem(1)
                '    e.Handled = True
                'ElseIf My.Computer.Keyboard.CtrlKeyDown AndAlso e.KeyCode = Keys.Q Then
                '    ' Ctrl+Q: Open quantity dialog
                '    btnqty_Click(Nothing, Nothing)
                '    e.Handled = True
                'ElseIf My.Computer.Keyboard.CtrlKeyDown AndAlso e.KeyCode = Keys.P Then
                '    ' Ctrl+P: Open multiple price selection dialog
                '    ShowMultiplePriceSelection()
                '    e.Handled = True
                'ElseIf e.KeyCode = Keys.F2 Then
                '    ' F2: Open multiple price selection dialog
                '    ShowMultiplePriceSelection()
                '    e.Handled = True
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    ' Method to delete a specific row
    Private Sub DeleteSelectedRow(rowHandle As Integer)
        Try

            ' Validate row handle
            If rowHandle < 0 OrElse rowHandle >= GridDataTble_Insert.Rows.Count Then
                Exit Sub
            End If

            ' Get item information for confirmation/logging
            Dim itemCode = GridDataTble_Insert.Rows(rowHandle)("ITEMCODE").ToString()
            Dim itemName = GridDataTble_Insert.Rows(rowHandle)("ITEMNAME").ToString()
            Dim netAmount = Convert.ToDecimal(GridDataTble_Insert.Rows(rowHandle)("NETAMT"))
            Dim rate = Convert.ToDecimal(GridDataTble_Insert.Rows(rowHandle)("RATE"))
            Dim qty = Convert.ToDecimal(GridDataTble_Insert.Rows(rowHandle)("QTY"))
            Dim psid = Convert.ToDecimal(GridDataTble_Insert.Rows(rowHandle)("PSID"))
            Dim trno = lblinvoiceno.Text
            ' Send delete log to server before removing from grid
            Try
                Dim trnoInt As Integer = 0
                Integer.TryParse(trno, trnoInt)
                If psid > 0 AndAlso trnoInt > 0 Then
                    Dim SqlDel(2) As SqlParameter
                    SqlDel(0) = New SqlParameter("@mode", "D")
                    SqlDel(1) = New SqlParameter("@psid", psid)
                    SqlDel(2) = New SqlParameter("@trno", trno)
                    If _ExecuteNonQuery("sp_DeleteSaleDetails", SqlDel, Errstr) = True Then
                        SendDeleteLogToServer(itemCode, itemName, qty, netAmount, "Item deleted by user", psid)
                    End If
                End If
                ' Only remove the row if server logging was successful
                ' Remove the row from DataTable
                GridDataTble_Insert.Rows.RemoveAt(rowHandle)
                GridDataTble_Insert.AcceptChanges()

                ' Renumber the SNO column
                RenumberSerialNumbers()

                ' Refresh the grid
                GridControlSalesData.DataSource = GridDataTble_Insert

                ' Update grand totals
                SalesGrandtotal("OK")

                ' Set focus back to search for next item entry
                cmbMaterialSearch.Focus()

            Catch ex As Exception
                ' Show error message and do not delete the row
                DevExpress.XtraEditors.XtraMessageBox.Show("Error: Could not log delete action to server: " & ex.Message & vbNewLine & vbNewLine & "Item was NOT deleted from the bill.",
                                                         "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ' Exit without deleting the row
                Exit Sub
            End Try

        Catch ex As Exception
            Throw New Exception("Error deleting row: " & ex.Message)
        End Try
    End Sub

    ' Method to send delete log to server
    Private Sub SendDeleteLogToServer(itemCode As String, itemName As String, qty As String, netAmount As Decimal, reason As String, psid As String)
        Try
            ' Prepare parameters for delete log
            Dim billno As String = If(String.IsNullOrEmpty(lblinvoiceno.Text), "TEMP-" & DateTime.Now.ToString("yyyyMMddHHmmss"), lblinvoiceno.Text)
            Dim pcname As String = Environment.MachineName
            Dim shiftno As Integer = _saleSetting._curShiftno
            Dim dayno As Integer = _saleSetting._curDayno
            Dim comid As Integer = _companyInfo.ComId
            Dim locid As Integer = _companyInfo.LocId
            Dim userid As Integer = _companyInfo.UserId

            ' Build URL parameters
            Dim url As String = M_Details.LinkAjaxRequest & "SalesRequest=14" &
                               "&billno=" & Uri.EscapeDataString(billno) &
                               "&procode=" & Uri.EscapeDataString(itemCode) &
                               "&description=" & Uri.EscapeDataString(itemName) &
                               "&netamt=" & netAmount.ToString("0.00") &
                               "&reason=" & Uri.EscapeDataString(reason) &
                               "&pcname=" & Uri.EscapeDataString(pcname) &
                               "&shiftno=" & shiftno.ToString() &
                               "&dayno=" & dayno.ToString() &
                               "&comid=" & comid.ToString() &
                               "&locid=" & locid.ToString() &
                               "&userid=" & userid.ToString() &
                               "&qty=" & qty &
                               "&psid=" & psid

            ' Send request to server
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Using webClient As New System.Net.WebClient()
                webClient.Encoding = System.Text.Encoding.UTF8
                Dim response As String = webClient.DownloadString(url)

                ' Parse response
                Dim parsedResponse As JObject = JObject.Parse(response)
                Dim success As Boolean = Convert.ToBoolean(parsedResponse("Success"))

                If Not success Then
                    Dim errorMessage As String = parsedResponse("Data").ToString()
                    Throw New Exception("Server returned error: " & errorMessage)
                End If
            End Using

        Catch ex As Exception
            ' Re-throw with more context
            Throw New Exception("Failed to send delete log to server: " & ex.Message)
        End Try
    End Sub

    ' Method to renumber serial numbers after deletion
    Private Sub RenumberSerialNumbers()
        Try
            _SnoCount = 0
            For Each row As DataRow In GridDataTble_Insert.Rows
                _SnoCount = _SnoCount + 1
                row("SNO") = _SnoCount
            Next
            GridDataTble_Insert.AcceptChanges()
        Catch ex As Exception
            ' Handle renumbering errors silently
        End Try
    End Sub


    Private Sub cmbMaterialSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbMaterialSearch.KeyDown
        Try
            Dim Qty As Decimal = 1.0
            'If My.Computer.Keyboard.CtrlKeyDown AndAlso e.KeyCode = Keys.Down Then

            '    GridViewPOS.Focus()
            '    GridViewPOS.FocusedColumn = GCItemRate
            '    cmbMaterialSearch.Focus()
            'ElseIf e.KeyCode = Keys.Down Then

            '    If GridViewPOS.RowCount > 0 Then

            '        GridViewPOS.FocusedColumn = GCItemRate
            '        GridViewPOS.FocusedRowHandle = 0
            '        'GCItemqty.OptionsColumn.AllowEdit = True
            '        'GCItemqty.OptionsColumn.AllowFocus = True
            '        'GCItemRate.OptionsColumn.AllowEdit = True
            '        'GCItemRate.OptionsColumn.AllowFocus = True

            '    End If
            'Else
            If e.KeyCode = Keys.Enter Then

                If String.IsNullOrWhiteSpace(cmbMaterialSearch.Text.ToString) Then
                    cmbMaterialSearch.ShowPopup()
                    txtsearch2.Select()
                    txtsearch2.Text = ""
                    txtMqty.EditValue = 1
                    Exit Sub
                Else
                    Dim str As String
                    str = cmbMaterialSearch.Text
                    If _globalSetting.SearchProductCode = True Then
                        If Get_product_info(str, Qty, "ItemCode", Errstr) = False Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else
                            cmbMaterialSearch.Text = ""
                            cmbMaterialSearch.Focus()
                            cmbMaterialSearch.EditValue = Nothing
                        End If
                    Else
                        If Get_product_info(str, Qty, "BarCode", Errstr) = False Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else
                            cmbMaterialSearch.Text = ""
                            cmbMaterialSearch.Focus()
                            cmbMaterialSearch.EditValue = Nothing
                        End If
                    End If

                End If
            ElseIf e.KeyCode = Keys.Escape Then
                If GridViewPOS.RowCount = 0 Then
                    Me.Close()
                End If
            End If

        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    Private Sub GridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView2.KeyDown
        Try

            If e.KeyCode = Keys.Enter Then
                txtsearch2.Text = ""
                Dim _productCode As Integer = 0
                Dim errstr As String = String.Empty
                _productCode = GridView2.GetRowCellValue(GridView2.FocusedRowHandle, "ITEMCODE")
                If OnSearch(GridView2.GetRowCellValue(GridView2.FocusedRowHandle, "BARCODE"), _productCode, errstr) = False Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    cmbMaterialSearch.Focus()
                    cmbMaterialSearch.ClosePopup()
                    cmbMaterialSearch.Text = ""
                End If
            ElseIf My.Computer.Keyboard.CtrlKeyDown AndAlso e.KeyCode = Keys.Up Then
                txtsearch2.Focus()
                txtsearch2.Select(txtsearch2.Text.Length, 1)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Function OnSearch(ByVal _Barcode As String, ByVal _M_TRID As Integer, ByRef ErrorMsg As String) As Boolean
        Try
            Dim Qty As Decimal = 1.0
            If txtMqty.EditValue <> 0 Then
                Qty = txtMqty.EditValue
            End If
            Dim PresentQty As Decimal = 0.0
            Dim M As Integer = 0
            If Get_product_info(_Barcode, Qty, "ItemCode", Errstr) = False Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                cmbMaterialSearch.Text = ""
                cmbMaterialSearch.Focus()
                cmbMaterialSearch.EditValue = Nothing

            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Function Get_product_info(ByRef _productCode As String, ByRef itemQty As Decimal, ByRef _Mode As String, ByRef ErrorMsg As String) As Boolean  'Product Select from Table _dsPro.table
        Try
            Dim ReceivedProCode As String = _productCode
            If ReceivedProCode Is Nothing Then
                Return False
            End If
            Dim dtrows As System.Data.EnumerableRowCollection(Of DataRow) = Nothing

            If _Mode = "BarCode" Then
                dtrows = From dtrow As DataRow In _JsonData.ItemMasterTable Where String.Equals(dtrow("BARCODE"), ReceivedProCode, StringComparison.CurrentCultureIgnoreCase)
            ElseIf _Mode = "ItemCode" Then
                dtrows = From dtrow As DataRow In _JsonData.ItemMasterTable Where String.Equals(dtrow("ITEMCODE"), ReceivedProCode, StringComparison.CurrentCultureIgnoreCase)
            Else
                ' Handle invalid mode
                ErrorMsg = "Invalid search mode: " & _Mode
                Return False
            End If

            If dtrows Is Nothing OrElse dtrows.Any = False Then
                cmbMaterialSearch.EditValue = Nothing
                ErrorMsg = "This " & _Mode & " is not Exists" & Environment.NewLine & "Kindly Refresh(F5) the Windows and Try Again."
                Return False
            Else
                Dim _Code As String = ""
                Dim _item As String = ""
                Dim _barcode As String = ""
                Dim _wholesalerate As String = ""
                Dim _srate As String = ""
                Dim _taxId As String = ""
                Dim _taxValue As String = ""
                Dim _serialno As String = ""
                Dim _uom As String = ""
                Dim _dsMaterial As New DataTable
                _dsMaterial = dtrows.CopyToDataTable
                If _dsMaterial.Rows.Count > 0 Then
                    For Each _rows In _dsMaterial.Rows
                        _barcode = _rows("BARCODE")
                        _Code = _rows("ITEMCODE")
                        _item = _rows("ITEMNAME")
                        _srate = _rows("SELL")
                        _taxValue = _rows("TAXVALUE")
                        _serialno = 1
                        _uom = 1

                    Next
                    _InsertDt(_barcode, _Code, _item, _serialno, _uom, _srate, _taxValue)
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
            'eLog.WriteErroLog("Get_product_info" & ex.Message)
        End Try

    End Function
    Public Sub _InsertDt(ByRef _barcode As String, ByRef _itemcode As Integer, ByRef _itemname As String, ByRef _serialno As String, ByRef _uom As String, ByRef _srate As Double, ByRef _taxValue As Integer)
        Try
            ' Get quantity from txtMqty control, default to 1 if invalid
            Dim Qty As Decimal = 1
            If txtMqty.EditValue IsNot Nothing Then
                Dim tempQty As Decimal
                If Decimal.TryParse(txtMqty.EditValue.ToString(), tempQty) AndAlso tempQty > 0 Then
                    Qty = tempQty
                End If
            End If

            Dim PrintItem As Integer = 1
            Dim DisPer As Double = 0.0
            Dim DisAmt As Double = 0.0
            Dim DisBPer As Double = 0.0
            Dim DisBAmt As Double = 0.0
            Dim TAmount As Double = 0.0
            Dim GAmount As Double = 0.0
            Dim TaxRetunAmt As Double = 0.0
            Dim NetAmount As Double = 0.0
            Dim _mountProCode As Integer = _itemcode
            Dim _item As String = _itemname
            Dim _batchno As String = 0

            ' Check if bill discount is already applied to existing items
            Dim billDiscountExists As Boolean = False
            Dim existingBillDiscountPer As Decimal = 0

            If GridDataTble_Insert.Rows.Count > 0 Then
                ' Check if any existing item has bill discount
                For Each row As DataRow In GridDataTble_Insert.Rows
                    If Convert.ToDecimal(row("BILL_DAMT")) > 0 Then
                        billDiscountExists = True
                        existingBillDiscountPer = Convert.ToDecimal(row("BILL_DPER"))
                        Exit For
                    End If
                Next
            End If

            GridDataTble_Insert.NewRow()
            GridDataTble_Insert.BeginInit()
            _SnoCount = _SnoCount + 1
            TAmount = Qty * _srate
            GAmount = Qty * _srate
            TaxRetunAmt = _ReturnGst(_taxValue, TAmount)

            ' Apply existing bill discount to new item if it exists
            If billDiscountExists Then
                Dim billDiscountAmount As Decimal = (TAmount * existingBillDiscountPer) / 100
                DisBPer = existingBillDiscountPer
                DisBAmt = billDiscountAmount

                ' Update totals for discount calculations
                Dim totalDiscountAmt As Decimal = billDiscountAmount
                Dim totalDiscountPer As Decimal = existingBillDiscountPer

                ' Recalculate GAmount after discount
                GAmount = TAmount - totalDiscountAmt
                TaxRetunAmt = _ReturnGst(_taxValue, GAmount)
            End If

            If _globalSetting.TaxExculsive = True Then
                NetAmount = GAmount + TaxRetunAmt
            Else
                NetAmount = GAmount
            End If

            ' Salesman Selection Variables
            Dim salesmanId As Integer = 1
            Dim salesmanName As String = "Default"
            Dim salesmanPer As Decimal = 0



            ' Add row with new column structure including separate discount columns and salesman info
            ' Column order: SNO, BARCODE, ITEMCODE, ITEMNAME, SERIALNO, UOM, RATE, QTY, TAMOUNT,
            '              ITEM_DPER, ITEM_DAMT, BILL_DPER, BILL_DAMT, TOTAL_DPER, TOTAL_DAMT,
            '              GAMOUNT, TAXVALUE, TAXAMT, NETAMT, ITEMREMARS, BATCHNO, SALESPERSONID, SALESPERSON, SALESMANPER, DELETE, ITEMLOCK, PSID
            GridDataTble_Insert.Rows.Add(_SnoCount, _barcode, _itemcode, Trim(_item), _serialno, _uom, _srate, Qty, TAmount, _
                                        0, 0, _
                                        DisBPer, DisBAmt, _
                                        DisBPer, DisBAmt, _
                                        GAmount, _taxValue, TaxRetunAmt, _RoundOff(NetAmount), "Remarks", _batchno, salesmanId, salesmanName, salesmanPer, 1, 1, 0)
            GridDataTble_Insert.AcceptChanges()
            GridDataTble_Insert.EndInit()
            GridControlSalesData.DataSource = GridDataTble_Insert
            GridViewPOS.MoveNext()

            ' Reset quantity input to 1 for next item
            txtMqty.EditValue = 1
            If SalesGrandtotal("ER") = False Then

            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function _ReturnGst(ByRef GstValue As Double, ByRef Amount As Double)
        Try
            Dim _RtAmt As Decimal = 0.0
            Dim _taxvalue As Double = GstValue
            If _globalSetting.TaxExculsive = True Then
                _taxvalue = (_taxvalue / 100)
                _RtAmt = (Amount * _taxvalue)
            Else
                _RtAmt = ((Amount * GstValue) / 100)
                _taxvalue = (_taxvalue / 100) + 1
                _RtAmt = (Amount - (Amount / _taxvalue))
            End If
            Return _RtAmt
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SalesGrandtotal(ByRef ERR As String) As Boolean
        Try

            Dim TAmount As Decimal = 0.0
            Dim ItemDiscountAmt As Decimal = 0.0
            Dim BillDiscountAmt As Decimal = 0.0
            Dim TotalDiscountAmt As Decimal = 0.0
            Dim GAmount As Decimal = 0.0
            Dim GST As Decimal = 0.0
            Dim ServiceChargeAmount As Decimal = 0.0
            Dim NetTot As Decimal = 0.0

            TAmount = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("TAMOUNT"))

            ' Calculate separate discount totals
            ItemDiscountAmt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("ITEM_DAMT"))
            BillDiscountAmt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("BILL_DAMT"))
            TotalDiscountAmt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("TOTAL_DAMT"))

            GAmount = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("GAMOUNT"))
            GST = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("TAXAMT"))

            ' Calculate Service Charge if enabled
            If _globalSetting.ServiceTaxActive = True Then
                Dim ServiceTaxPercentage As Decimal = 0.0
                ' Convert string to decimal safely
                If Decimal.TryParse(_globalSettingValues.ServiceTaxValue, ServiceTaxPercentage) Then
                    ' Calculate service charge on gross amount (after discount, before tax)
                    ServiceChargeAmount = (GAmount * ServiceTaxPercentage) / 100
                End If
            End If

            ' Calculate final net total
            If _globalSetting.TaxExculsive = True Then
                ' Tax Exclusive: Add tax and service charge to gross amount
                NetTot = GAmount + GST + ServiceChargeAmount
            Else
                ' Tax Inclusive: Add only service charge (tax already included in gross amount)
                NetTot = GAmount + ServiceChargeAmount
            End If

            ' Update UI labels with detailed discount breakdown
            lblsubtotal.Text = TAmount.ToString("0.00")
            lblitemdisctotal.Text = ItemDiscountAmt.ToString("0.00")
            lblbilldisctotal.Text = BillDiscountAmt.ToString("0.00")
            lblssttotal.Text = GST.ToString("0.00")
            lblservchargetotal.Text = ServiceChargeAmount.ToString("0.00")
            lblnetamt.Text = _RoundOff(NetTot).ToString("0.00")
            lblnoofitems.Text = GridDataTble_Insert.Rows.Count.ToString()
            lblnoofqty.Text = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("QTY")).ToString("0.00")
            ' Update any additional discount breakdown labels if they exist
            ' You can add these labels to show separate item and bill discounts
            ' lblitemdiscountonly.Text = ItemDiscountAmt.ToString("0.00")
            ' lblbilldiscountonly.Text = BillDiscountAmt.ToString("0.00")
            lblservcharge.Text = _globalSettingValues.ServiceTaxValue
            If modeOfSale = "New" Then
                barselectsalesman_ItemClick(Nothing, Nothing)
            End If
            Return True
        Catch ex As Exception
            ERR = "Error in SalesGrandtotal: " & ex.Message
            Return False
        End Try
    End Function


    Public Function CalculateServiceCharge(grossAmount As Decimal) As Decimal
        Try
            If _globalSetting.ServiceTaxActive = True Then
                Dim ServiceTaxPercentage As Decimal = 0.0
                ' Convert string to decimal safely
                If Decimal.TryParse(_globalSettingValues.ServiceTaxValue, ServiceTaxPercentage) Then
                    Return (grossAmount * ServiceTaxPercentage) / 100
                End If
            End If
            Return 0.0
        Catch ex As Exception
            Return 0.0
        End Try
    End Function

    Public Function _RoundOff(ByRef AMT As Decimal) As Decimal
        Try
            Dim _roundoffs As Double = 0.0
            _roundoffs = Math.Round(AMT * 2, 1) / 2
            Return _roundoffs
        Catch ex As Exception
            '.WriteErroLog(ex.Message.ToString)
            Return 0.0
        End Try
    End Function

#End Region
#Region "OptionButton"

    Private Sub barSearchProductCode_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barSearchProductCode.ItemClick
        Try

            ' Convert Boolean to Integer (1 for True, 0 for False)
            Dim statusValue As Integer = If(barSearchProductCode.Checked, 1, 0)

            ' Update the setting using the helper function
            UpdatePosSetting("SearchProductCode", statusValue, 0, 0)

        Catch ex As Exception
            ' If update fails, revert the UI change
            barSearchProductCode.Checked = Not barSearchProductCode.Checked
        End Try
    End Sub

    ' Add this event handler for Service Tax toggle (if you have a button for it)
    'Private Sub barServiceTaxActive_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barServiceTaxActive.ItemClick
    '    Try
    '        ' Toggle the service tax setting
    '        barServiceTaxActive.Checked = Not barServiceTaxActive.Checked
    '
    '        ' Update the setting
    '        UpdateServiceTaxActive(barServiceTaxActive.Checked)
    '
    '        ' Recalculate totals
    '        If GridDataTble_Insert.Rows.Count > 0 Then
    '            SalesGrandtotal("ER")
    '        End If
    '
    '    Catch ex As Exception
    '        ' If update fails, revert the UI change
    '        barServiceTaxActive.Checked = Not barServiceTaxActive.Checked
    '        DevExpress.XtraEditors.XtraMessageBox.Show("Error updating Service Tax setting: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
#End Region
#Region "Discount Management"
    ' Bill Discount Button Click Handler
    Private Sub BarButtonItem7_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbilldiscount.ItemClick
        Try
            If _globalSetting.BillDiscountAcitve = False Then
                MessageBox.Show("You do not have rights apply billdiscount: ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            ' Show discount selection form
            Dim discountForm As New FrmDiscountSelection()
            If discountForm.ShowDialog() = DialogResult.OK Then
                Dim selectedDiscount = discountForm.SelectedDiscountInfo

                If selectedDiscount IsNot Nothing Then
                    ' Apply bill-level discount
                    _ApplyBillDiscount(selectedDiscount)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error applying bill discount: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Item Discount Button Click Handler
    Private Sub BarButtonItem8_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles baritemdiscount.ItemClick
        Try
            If _globalSetting.ItemDiscountActive = False Then
                MessageBox.Show("You do not have rights apply item discount: ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            ' Check if any item is selected in the grid
            If GridViewPOS.FocusedRowHandle < 0 Then
                MessageBox.Show("Please select an item to apply discount.", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' Show discount selection form
            Dim discountForm As New FrmDiscountSelection()
            If discountForm.ShowDialog() = DialogResult.OK Then
                Dim selectedDiscount = discountForm.SelectedDiscountInfo

                If selectedDiscount IsNot Nothing Then
                    ' Apply item-level discount
                    _ApplyItemDiscount(selectedDiscount)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error applying item discount: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Apply Bill Level Discount
    Private Sub _ApplyBillDiscount(discountInfo As Object)
        Try
            Dim discountType As String = discountInfo.Type.ToString()
            Dim discountValue As Decimal = Convert.ToDecimal(discountInfo.Value)
            Dim discountName As String = discountInfo.Name.ToString()

            ' Calculate total amount before discount
            Dim totalAmount As Decimal = 0
            For i As Integer = 0 To GridDataTble_Insert.Rows.Count - 1
                Dim rowAmount As Decimal = 0
                If Decimal.TryParse(GridDataTble_Insert.Rows(i)("TAMOUNT").ToString(), rowAmount) Then
                    totalAmount += rowAmount
                End If
            Next

            ' Calculate discount amount
            Dim discountAmount As Decimal = 0
            If discountType = "percentage" Then
                discountAmount = (totalAmount * discountValue) / 100
            Else
                discountAmount = discountValue
            End If

            ' Apply discount to each item proportionally
            For i As Integer = 0 To GridDataTble_Insert.Rows.Count - 1
                Dim itemAmount As Decimal = 0
                If Decimal.TryParse(GridDataTble_Insert.Rows(i)("TAMOUNT").ToString(), itemAmount) Then
                    Dim itemDiscountAmount As Decimal = (itemAmount / totalAmount) * discountAmount
                    Dim itemDiscountPercent As Decimal = (itemDiscountAmount / itemAmount) * 100

                    ' Update the DataTable directly with BILL discount
                    GridDataTble_Insert.Rows(i)("BILL_DPER") = itemDiscountPercent
                    GridDataTble_Insert.Rows(i)("BILL_DAMT") = itemDiscountAmount

                    ' Update total discount in DataTable
                    Dim existingItemDiscountAmt As Decimal = Convert.ToDecimal(GridDataTble_Insert.Rows(i)("ITEM_DAMT"))
                    Dim totalDiscountAmt As Decimal = existingItemDiscountAmt + itemDiscountAmount
                    Dim totalDiscountPer As Decimal = If(itemAmount > 0, (totalDiscountAmt / itemAmount) * 100, 0)

                    GridDataTble_Insert.Rows(i)("TOTAL_DAMT") = totalDiscountAmt
                    GridDataTble_Insert.Rows(i)("TOTAL_DPER") = totalDiscountPer

                    ' Recalculate the row totals
                    _RecalculateRowTotals(i)
                End If
            Next

            ' Accept changes to DataTable and refresh grid
            GridDataTble_Insert.AcceptChanges()
            GridControlSalesData.DataSource = GridDataTble_Insert

            ' Update grand totals
            SalesGrandtotal("OK")

            MessageBox.Show("Bill discount of " & discountName & " applied successfully." & Environment.NewLine &
                          "Total Bill Discount: " & discountAmount.ToString("0.00"),
                          "Discount Applied", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            Throw New Exception("Error applying bill discount: " & ex.Message)
        End Try
    End Sub

    ' Apply Item Level Discount
    Private Sub _ApplyItemDiscount(discountInfo As Object)
        Try
            Dim discountType As String = discountInfo.Type.ToString()
            Dim discountValue As Decimal = Convert.ToDecimal(discountInfo.Value)
            Dim discountName As String = discountInfo.Name.ToString()
            Dim selectedRow As Integer = GridViewPOS.FocusedRowHandle

            ' Get item amount
            Dim itemAmount As Decimal = 0
            If selectedRow < 0 OrElse selectedRow >= GridDataTble_Insert.Rows.Count Then
                Throw New Exception("Invalid item selection")
            End If
            If Not Decimal.TryParse(GridDataTble_Insert.Rows(selectedRow)("TAMOUNT").ToString(), itemAmount) Then
                Throw New Exception("Invalid item amount")
            End If

            ' Calculate discount amount
            Dim discountAmount As Decimal = 0
            If discountType = "percentage" Then
                discountAmount = (itemAmount * discountValue) / 100
            Else
                discountAmount = discountValue
            End If

            ' Validate discount doesn't exceed item amount
            If discountAmount > itemAmount Then
                MessageBox.Show("Discount amount cannot exceed item amount.", "Invalid Discount", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Calculate discount percentage for storage
            Dim discountPercent As Decimal = (discountAmount / itemAmount) * 100

            ' Update the DataTable directly with ITEM discount
            GridDataTble_Insert.Rows(selectedRow)("ITEM_DPER") = discountPercent
            GridDataTble_Insert.Rows(selectedRow)("ITEM_DAMT") = discountAmount

            ' Update total discount in DataTable (item + bill discount if any)
            Dim existingBillDiscountAmt As Decimal = Convert.ToDecimal(GridDataTble_Insert.Rows(selectedRow)("BILL_DAMT"))
            Dim totalDiscountAmt As Decimal = discountAmount + existingBillDiscountAmt
            Dim totalDiscountPer As Decimal = If(itemAmount > 0, (totalDiscountAmt / itemAmount) * 100, 0)

            GridDataTble_Insert.Rows(selectedRow)("TOTAL_DAMT") = totalDiscountAmt
            GridDataTble_Insert.Rows(selectedRow)("TOTAL_DPER") = totalDiscountPer

            ' Accept changes to DataTable
            GridDataTble_Insert.AcceptChanges()

            ' Recalculate the row totals
            _RecalculateRowTotals(selectedRow)

            ' Update grand totals
            SalesGrandtotal("OK")

            MessageBox.Show("Item discount of " & discountName & " applied successfully." & Environment.NewLine &
                          "Item Discount: " & discountAmount.ToString("0.00") & Environment.NewLine &
                          "Total Discount: " & totalDiscountAmt.ToString("0.00"),
                          "Discount Applied", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            Throw New Exception("Error applying item discount: " & ex.Message)
        End Try
    End Sub

    ' Recalculate Row Totals after discount application
    Private Sub _RecalculateRowTotals(rowIndex As Integer)
        Try
            Dim rate As Decimal = Convert.ToDecimal(GridDataTble_Insert.Rows(rowIndex)("RATE"))
            Dim qty As Decimal = Convert.ToDecimal(GridDataTble_Insert.Rows(rowIndex)("QTY"))
            Dim totalDiscountAmount As Decimal = Convert.ToDecimal(GridDataTble_Insert.Rows(rowIndex)("TOTAL_DAMT"))
            Dim taxValue As Integer = Convert.ToInt32(GridDataTble_Insert.Rows(rowIndex)("TAXVALUE"))

            ' Calculate totals
            Dim totalAmount As Decimal = rate * qty
            Dim grossAmount As Decimal = totalAmount - totalDiscountAmount

            ' Calculate tax amount based on tax setting
            Dim taxAmount As Decimal = 0
            Dim netAmount As Decimal = 0

            If _globalSetting.TaxExculsive = True Then
                ' Tax Exclusive: Calculate tax on gross amount and add to get net amount
                taxAmount = (grossAmount * taxValue) / 100
                netAmount = grossAmount + taxAmount
            Else
                ' Tax Inclusive: Extract tax from gross amount
                Dim taxMultiplier As Decimal = (taxValue / 100) + 1
                taxAmount = grossAmount - (grossAmount / taxMultiplier)
                netAmount = grossAmount
            End If

            ' Update calculated fields in DataTable
            GridDataTble_Insert.Rows(rowIndex)("TAMOUNT") = totalAmount
            GridDataTble_Insert.Rows(rowIndex)("GAMOUNT") = grossAmount
            GridDataTble_Insert.Rows(rowIndex)("TAXAMT") = taxAmount
            GridDataTble_Insert.Rows(rowIndex)("NETAMT") = netAmount

            ' Accept changes
            GridDataTble_Insert.AcceptChanges()

        Catch ex As Exception
            ' Handle calculation errors silently
        End Try
    End Sub

    ' Get discount summary for reporting/analysis
    Public Function GetDiscountSummary() As Object
        Try
            Dim itemDiscountTotal As Decimal = 0
            Dim billDiscountTotal As Decimal = 0
            Dim totalDiscountTotal As Decimal = 0

            For i As Integer = 0 To GridDataTble_Insert.Rows.Count - 1
                itemDiscountTotal += Convert.ToDecimal(GridDataTble_Insert.Rows(i)("ITEM_DAMT"))
                billDiscountTotal += Convert.ToDecimal(GridDataTble_Insert.Rows(i)("BILL_DAMT"))
                totalDiscountTotal += Convert.ToDecimal(GridDataTble_Insert.Rows(i)("TOTAL_DAMT"))
            Next

            Return New With {
                .ItemDiscountTotal = itemDiscountTotal,
                .BillDiscountTotal = billDiscountTotal,
                .TotalDiscountAmount = totalDiscountTotal,
                .ItemDiscountCount = GridDataTble_Insert.AsEnumerable().Count(Function(row) row.Field(Of Decimal)("ITEM_DAMT") > 0),
                .BillDiscountApplied = GridDataTble_Insert.AsEnumerable().Any(Function(row) row.Field(Of Decimal)("BILL_DAMT") > 0)
            }

        Catch ex As Exception
            Return New With {
                .ItemDiscountTotal = 0,
                .BillDiscountTotal = 0,
                .TotalDiscountAmount = 0,
                .ItemDiscountCount = 0,
                .BillDiscountApplied = False
            }
        End Try
    End Function

    ' Clear all discounts (both item and bill)
    Public Sub ClearAllDiscounts()
        Try
            For i As Integer = 0 To GridDataTble_Insert.Rows.Count - 1
                ' Clear item discounts in DataTable
                GridDataTble_Insert.Rows(i)("ITEM_DPER") = 0
                GridDataTble_Insert.Rows(i)("ITEM_DAMT") = 0

                ' Clear bill discounts in DataTable
                GridDataTble_Insert.Rows(i)("BILL_DPER") = 0
                GridDataTble_Insert.Rows(i)("BILL_DAMT") = 0

                ' Clear total discounts in DataTable
                GridDataTble_Insert.Rows(i)("TOTAL_DPER") = 0
                GridDataTble_Insert.Rows(i)("TOTAL_DAMT") = 0

                ' Recalculate row totals
                _RecalculateRowTotals(i)
            Next

            ' Accept changes and refresh grid
            GridDataTble_Insert.AcceptChanges()
            GridControlSalesData.DataSource = GridDataTble_Insert

            ' Update grand totals
            SalesGrandtotal("OK")

            MessageBox.Show("All discounts cleared successfully.", "Discounts Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error clearing discounts: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Apply existing bill discount to newly added items
    Public Sub ApplyExistingBillDiscountToNewItems()
        Try
            ' Find existing bill discount rate
            Dim billDiscountPer As Decimal = 0
            Dim hasExistingBillDiscount As Boolean = False

            For i As Integer = 0 To GridDataTble_Insert.Rows.Count - 1
                Dim billDiscAmt As Decimal = Convert.ToDecimal(GridDataTble_Insert.Rows(i)("BILL_DAMT"))
                If billDiscAmt > 0 Then
                    billDiscountPer = Convert.ToDecimal(GridDataTble_Insert.Rows(i)("BILL_DPER"))
                    hasExistingBillDiscount = True
                    Exit For
                End If
            Next

            If hasExistingBillDiscount Then
                ' Apply to items that don't have bill discount
                For i As Integer = 0 To GridDataTble_Insert.Rows.Count - 1
                    Dim currentBillDiscAmt As Decimal = Convert.ToDecimal(GridDataTble_Insert.Rows(i)("BILL_DAMT"))
                    If currentBillDiscAmt = 0 Then
                        Dim itemAmount As Decimal = Convert.ToDecimal(GridDataTble_Insert.Rows(i)("TAMOUNT"))
                        Dim discountAmount As Decimal = (itemAmount * billDiscountPer) / 100

                        ' Apply bill discount using DataTable
                        GridDataTble_Insert.Rows(i)("BILL_DPER") = billDiscountPer
                        GridDataTble_Insert.Rows(i)("BILL_DAMT") = discountAmount

                        ' Update total discount
                        Dim existingItemDiscountAmt As Decimal = Convert.ToDecimal(GridDataTble_Insert.Rows(i)("ITEM_DAMT"))
                        Dim totalDiscountAmt As Decimal = existingItemDiscountAmt + discountAmount
                        Dim totalDiscountPer As Decimal = If(itemAmount > 0, (totalDiscountAmt / itemAmount) * 100, 0)

                        GridDataTble_Insert.Rows(i)("TOTAL_DAMT") = totalDiscountAmt
                        GridDataTble_Insert.Rows(i)("TOTAL_DPER") = totalDiscountPer

                        ' Recalculate totals
                        _RecalculateRowTotals(i)
                    End If
                Next

                ' Accept changes to DataTable and refresh grid
                GridDataTble_Insert.AcceptChanges()
                GridControlSalesData.DataSource = GridDataTble_Insert

                ' Update grand totals
                SalesGrandtotal("OK")

                'MessageBox.Show("Existing bill discount of " & billDiscountPer.ToString("F2") & "% applied to new items.", _
                '              "Bill Discount Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error applying bill discount to new items: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
#Region "New Bill"
    Private Sub barbtnNewBill_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnNewBill.ItemClick
        Try
            '' Confirm with user if there are items in the current bill
            'If GridDataTble_Insert.Rows.Count > 0 Then
            '    Dim result As DialogResult = MessageBox.Show("Are you sure you want to start a new bill? All current items will be cleared.", _
            '                                               "New Bill Confirmation", _
            '                                               MessageBoxButtons.YesNo, _
            '                                               MessageBoxIcon.Question)
            '    If result = DialogResult.No Then
            '        Exit Sub
            '    End If
            'End If

            ' Clear the current bill and start fresh
            ClearCurrentBill()

            ' Set focus to search field for quick item entry
            cmbMaterialSearch.Focus()

        Catch ex As Exception
            MessageBox.Show("Error starting new bill: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Dim billPrefix As String = "Inv"
    ' Helper method to clear current bill and reset form
    Private Sub ClearCurrentBill()
        Try
            ' Clear the sales data table
            GridDataTble_Insert.Clear()
            GridDataTble_Insert.AcceptChanges()

            ' Reset counters
            _SnoCount = 0

            ' Set mode to new
            modeOfSale = "New"
            barbtnstatus.Caption = "Sales Mode : " & modeOfSale
            ' Clear selected customer
            ClearSelectedCustomer()

            ' Get new bill number
            'If _getBillno() = False Then
            '    ' Handle error getting bill number if needed
            'End If
            Dim dt As DataTable
            dt = New DataTable
            dt = GetPosMasterByID(_companyInfo.CompanyPMID, _companyInfo.ComId, _companyInfo.LocId)
            If dt.Rows.Count > 0 Then
                lblinvoiceno.Text = dt.Rows(0)("PSR_BILL_NUMBER").ToString
                billPrefix = dt.Rows(0)("PM_PREFIX").ToString
            End If
            ' Clear all totals by recalculating with empty data
            SalesGrandtotal("OK")

            ' Clear search fields
            cmbMaterialSearch.Text = ""
            cmbMaterialSearch.EditValue = Nothing
            txtsearch2.Text = ""
            txtMqty.EditValue = 1

            ' Update data source
            GridControlSalesData.DataSource = GridDataTble_Insert
            'payment term
            'CmbPaymentTerm.SelectedIndex = 0

        Catch ex As Exception
            Throw New Exception("Error clearing current bill: " & ex.Message)
        End Try
    End Sub
#End Region
#Region "QtyControll"
    ' Current quantity being entered via number buttons
    Private currentQtyInput As String = ""

    Private Sub btn_1_Click(sender As Object, e As EventArgs)
        Try
            Dim focusedRowHandle = GetTargetRowIndex()
            Dim NUMBTN As Label = CType(sender, Label)
            Dim buttonText As String = NUMBTN.Text.Trim()
            Dim itemLock = GridDataTble_Insert.Rows(focusedRowHandle)("ITEMLOCK")
            If itemLock.ToString = "2" Then
                If _globalSetting.QtyChangeActive = False Then
                    Exit Sub
                Else


                    ' Numeric input - directly add to current input and apply
                    Dim numValue As Integer
                    If Integer.TryParse(buttonText, numValue) Then
                        currentQtyInput += buttonText
                        ' Update quantity display
                        Dim qty As Decimal
                        If Decimal.TryParse(currentQtyInput, qty) AndAlso qty > 0 Then
                            txtMqty.EditValue = qty
                            ' Directly apply the quantity to selected/last item
                            ApplyQuantityInput()
                        End If
                    End If
                End If
            Else
                ' Numeric input - directly add to current input and apply
                Dim numValue As Integer
                If Integer.TryParse(buttonText, numValue) Then
                    currentQtyInput += buttonText
                    ' Update quantity display
                    Dim qty As Decimal
                    If Decimal.TryParse(currentQtyInput, qty) AndAlso qty > 0 Then
                        txtMqty.EditValue = qty
                        ' Directly apply the quantity to selected/last item
                        ApplyQuantityInput()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error in quantity button click: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '' Separate event handlers for specific function buttons
    'Private Sub btnPlus_Click(sender As Object, e As EventArgs) ' Add this handler to your + button
    '    Try
    '        AddQuantityToItem(1)
    '    Catch ex As Exception
    '        MessageBox.Show("Error adding quantity: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub btnMinus_Click(sender As Object, e As EventArgs) ' Add this handler to your - button
    '    Try
    '        SubtractQuantityFromItem(1)
    '    Catch ex As Exception
    '        MessageBox.Show("Error subtracting quantity: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) ' Add this handler to your Clear button
        Try
            currentQtyInput = ""
            txtMqty.EditValue = 1
        Catch ex As Exception
            MessageBox.Show("Error clearing quantity: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnqty_Click(sender As Object, e As EventArgs)
        Try
            Dim focusedRowHandle = GetTargetRowIndex()
            Dim itemLock = GridDataTble_Insert.Rows(focusedRowHandle)("ITEMLOCK")
            If itemLock.ToString = "2" Then
                If _globalSetting.QtyChangeActive = False Then
                    Exit Sub
                Else
                    frmKeyQty.ShowDialog("Enter Qty")
                    If frmKeyQty.DialogResult = Windows.Forms.DialogResult.OK Then
                        Dim Qty = _FunctionKeyBoardModule.gs_keyboardValueInteger
                        If Qty > 0 Then
                            UpdateQuantityForSelectedItem(Qty)
                        End If
                    End If
                End If
            Else
                frmKeyQty.ShowDialog("Enter Qty")
                If frmKeyQty.DialogResult = Windows.Forms.DialogResult.OK Then
                    Dim Qty = _FunctionKeyBoardModule.gs_keyboardValueInteger
                    If Qty > 0 Then
                        UpdateQuantityForSelectedItem(Qty)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error in quantity entry: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '' Add quantity to selected item or last item
    'Private Sub AddQuantityToItem(addQty As Decimal)
    '    Try
    '        Dim targetRowIndex As Integer = GetTargetRowIndex()
    '        If targetRowIndex >= 0 Then
    '            UpdateRowQuantity(targetRowIndex, addQty, "ADD")
    '        Else
    '            MessageBox.Show("No item available to update quantity.", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("Error adding quantity: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    '' Subtract quantity from selected item or last item
    'Private Sub SubtractQuantityFromItem(subtractQty As Decimal)
    '    Try
    '        Dim targetRowIndex As Integer = GetTargetRowIndex()
    '        If targetRowIndex >= 0 Then
    '            UpdateRowQuantity(targetRowIndex, subtractQty, "SUBTRACT")
    '        Else
    '            MessageBox.Show("No item available to update quantity.", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("Error subtracting quantity: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    ' Apply the current quantity input to selected or last item
    Private Sub ApplyQuantityInput()
        Try
            If String.IsNullOrEmpty(currentQtyInput) Then
                Exit Sub
            End If

            Dim qty As Decimal
            If Decimal.TryParse(currentQtyInput, qty) AndAlso qty > 0 Then
                Dim targetRowIndex As Integer = GetTargetRowIndex()
                If targetRowIndex >= 0 Then
                    UpdateRowQuantity(targetRowIndex, qty, "SET")
                Else
                    MessageBox.Show("No item available to set quantity.", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                ' Reset input after applying
                currentQtyInput = ""
                txtMqty.EditValue = 1
            End If
        Catch ex As Exception
            MessageBox.Show("Error applying quantity: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Get the target row index (selected row or last row)
    Private Function GetTargetRowIndex() As Integer
        Try
            ' First try to get focused/selected row
            If GridViewPOS.FocusedRowHandle >= 0 AndAlso GridViewPOS.FocusedRowHandle < GridDataTble_Insert.Rows.Count Then
                Return GridViewPOS.FocusedRowHandle
            End If

            ' If no focused row, use last row
            If GridDataTble_Insert.Rows.Count > 0 Then
                Return GridDataTble_Insert.Rows.Count - 1
            End If

            Return -1 ' No rows available
        Catch ex As Exception
            Return -1
        End Try
    End Function

    ' Update quantity for specific row
    Private Sub UpdateRowQuantity(rowIndex As Integer, qtyValue As Decimal, operation As String)
        Try
            If rowIndex < 0 OrElse rowIndex >= GridDataTble_Insert.Rows.Count Then
                Exit Sub
            End If

            Dim currentQty As Decimal = Convert.ToDecimal(GridDataTble_Insert.Rows(rowIndex)("QTY"))
            Dim newQty As Decimal

            Select Case operation.ToUpper()
                Case "ADD"
                    newQty = currentQty + qtyValue
                Case "SUBTRACT"
                    newQty = currentQty - qtyValue
                    If newQty < 1 Then newQty = 1 ' Minimum quantity is 1
                Case "SET"
                    newQty = qtyValue
                Case Else
                    Exit Sub
            End Select

            ' Update the quantity in DataTable
            GridDataTble_Insert.Rows(rowIndex)("QTY") = newQty

            ' Recalculate all amounts for this row
            RecalculateRowAmounts(rowIndex)

            ' Accept changes and refresh
            GridDataTble_Insert.AcceptChanges()
            GridControlSalesData.DataSource = GridDataTble_Insert

            ' Update grand totals
            SalesGrandtotal("OK")

            ' Focus on the updated row
            GridViewPOS.FocusedRowHandle = rowIndex

        Catch ex As Exception
            Throw New Exception("Error updating row quantity: " & ex.Message)
        End Try
    End Sub

    ' Update quantity for selected item directly (from txtMqty or external input)
    Private Sub UpdateQuantityForSelectedItem(newQty As Decimal)
        Try
            If newQty <= 0 Then
                MessageBox.Show("Quantity must be greater than zero.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim targetRowIndex As Integer = GetTargetRowIndex()
            If targetRowIndex >= 0 Then
                UpdateRowQuantity(targetRowIndex, newQty, "SET")
                txtMqty.EditValue = 1 ' Reset quantity input
            Else
                ' If no existing item, store the quantity for next item addition
                txtMqty.EditValue = newQty
            End If
        Catch ex As Exception
            MessageBox.Show("Error updating quantity: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Recalculate all amounts for a specific row after quantity change
    Private Sub RecalculateRowAmounts(rowIndex As Integer)
        Try
            Dim rate As Decimal = Convert.ToDecimal(GridDataTble_Insert.Rows(rowIndex)("RATE"))
            Dim qty As Decimal = Convert.ToDecimal(GridDataTble_Insert.Rows(rowIndex)("QTY"))
            Dim itemDiscountPer As Decimal = Convert.ToDecimal(GridDataTble_Insert.Rows(rowIndex)("ITEM_DPER"))
            Dim billDiscountPer As Decimal = Convert.ToDecimal(GridDataTble_Insert.Rows(rowIndex)("BILL_DPER"))
            Dim taxValue As Integer = Convert.ToInt32(GridDataTble_Insert.Rows(rowIndex)("TAXVALUE"))

            ' Calculate basic amount
            Dim totalAmount As Decimal = rate * qty

            ' Calculate discounts
            Dim itemDiscountAmount As Decimal = (totalAmount * itemDiscountPer) / 100
            Dim billDiscountAmount As Decimal = (totalAmount * billDiscountPer) / 100
            Dim totalDiscountAmount As Decimal = itemDiscountAmount + billDiscountAmount
            Dim totalDiscountPer As Decimal = If(totalAmount > 0, (totalDiscountAmount / totalAmount) * 100, 0)

            ' Calculate gross amount after discount
            Dim grossAmount As Decimal = totalAmount - totalDiscountAmount

            ' Calculate tax
            Dim taxAmount As Decimal = 0
            Dim netAmount As Decimal = 0

            If _globalSetting.TaxExculsive = True Then
                ' Tax Exclusive: Calculate tax on gross amount and add to get net amount
                taxAmount = (grossAmount * taxValue) / 100
                netAmount = grossAmount + taxAmount
            Else
                ' Tax Inclusive: Extract tax from gross amount
                Dim taxMultiplier As Decimal = (taxValue / 100) + 1
                taxAmount = grossAmount - (grossAmount / taxMultiplier)
                netAmount = grossAmount
            End If

            ' Update all calculated fields
            GridDataTble_Insert.Rows(rowIndex)("TAMOUNT") = totalAmount
            GridDataTble_Insert.Rows(rowIndex)("ITEM_DAMT") = itemDiscountAmount
            GridDataTble_Insert.Rows(rowIndex)("BILL_DAMT") = billDiscountAmount
            GridDataTble_Insert.Rows(rowIndex)("TOTAL_DAMT") = totalDiscountAmount
            GridDataTble_Insert.Rows(rowIndex)("TOTAL_DPER") = totalDiscountPer
            GridDataTble_Insert.Rows(rowIndex)("GAMOUNT") = grossAmount
            GridDataTble_Insert.Rows(rowIndex)("TAXAMT") = taxAmount
            GridDataTble_Insert.Rows(rowIndex)("NETAMT") = netAmount

        Catch ex As Exception
            ' Handle calculation errors silently
        End Try
    End Sub

    '' Quick quantity buttons (can be added to form if needed)
    'Public Sub QuickAddQty1()
    '    AddQuantityToItem(1)
    'End Sub

    'Public Sub QuickAddQty5()
    '    AddQuantityToItem(5)
    'End Sub

    'Public Sub QuickAddQty10()
    '    AddQuantityToItem(10)
    'End Sub

    'Public Sub QuickSubtractQty1()
    '    SubtractQuantityFromItem(1)
    'End Sub
#End Region
#Region "SelectMultiplePrice"
    Private Sub barbtnselectprice_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnselectprice.ItemClick
        Try
            If _globalSetting.SelectMultiplePriceActive = False Then
                MessageBox.Show("You Dont Have Rights To opening multiple price selection: ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            ShowMultiplePriceSelection()
        Catch ex As Exception
            MessageBox.Show("Error opening multiple price selection: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Show multiple price selection dialog
    Private Sub ShowMultiplePriceSelection()
        Try
            ' Check if any item is selected in the grid
            If GridViewPOS.FocusedRowHandle < 0 Then
                MessageBox.Show("Please select an item to change price.", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' Get selected item information
            Dim rowHandle As Integer = GridViewPOS.FocusedRowHandle
            If rowHandle >= GridDataTble_Insert.Rows.Count Then
                MessageBox.Show("Invalid item selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim itemId As Integer = Convert.ToInt32(GridDataTble_Insert.Rows(rowHandle)("ITEMCODE"))
            Dim itemName As String = GridDataTble_Insert.Rows(rowHandle)("ITEMNAME").ToString()

            ' Show multiple price selection form
            Dim priceSelectionForm As New FrmMultiplePriceSelection(itemId, itemName)
            If priceSelectionForm.ShowDialog() = DialogResult.OK Then
                Dim selectedPriceInfo = priceSelectionForm.SelectedPriceInfo

                If selectedPriceInfo IsNot Nothing Then
                    ' Apply the selected price to the item
                    ApplyMultiplePrice(rowHandle, selectedPriceInfo)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Error showing multiple price selection: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Apply selected multiple price to item
    Private Sub ApplyMultiplePrice(rowIndex As Integer, priceInfo As Object)
        Try
            If rowIndex < 0 OrElse rowIndex >= GridDataTble_Insert.Rows.Count Then
                Exit Sub
            End If

            Dim newPrice As Decimal = Convert.ToDecimal(priceInfo.PriceValue)
            Dim priceName As String = priceInfo.PriceName.ToString()

            ' Update the price in DataTable
            GridDataTble_Insert.Rows(rowIndex)("RATE") = newPrice

            ' Recalculate all amounts for this row
            RecalculateRowAmounts(rowIndex)

            ' Accept changes and refresh
            GridDataTble_Insert.AcceptChanges()
            GridControlSalesData.DataSource = GridDataTble_Insert

            ' Update grand totals
            SalesGrandtotal("OK")

            ' Show confirmation message
            MessageBox.Show("Price updated to " & priceName & ": " & newPrice.ToString("0.00"), _
                          "Price Applied", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Keep focus on the updated row
            GridViewPOS.FocusedRowHandle = rowIndex

        Catch ex As Exception
            MessageBox.Show("Error applying multiple price: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Public method to apply specific price by price ID
    Public Sub ApplyPriceById(itemId As Integer, priceId As Integer)
        Try
            ' Find the row with the specified item
            For i As Integer = 0 To GridDataTble_Insert.Rows.Count - 1
                If Convert.ToInt32(GridDataTble_Insert.Rows(i)("ITEMCODE")) = itemId Then
                    ' Get price information for this price ID
                    ' This would typically come from server or cache
                    ' For now, we'll show the selection dialog
                    ShowMultiplePriceSelection()
                    Exit Sub
                End If
            Next

            MessageBox.Show("Item not found in current bill.", "Item Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error applying price by ID: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
#Region "CustomerSelect"
    Private Sub barselectcustomer1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barselectcustomer1.ItemClick
        Try
            ' Open customer selection dialog
            Dim customerListForm As New FrmCustomerList(True) ' True for selection mode

            If customerListForm.ShowDialog() = DialogResult.OK Then
                ' Customer was selected
                selectedCustomerId = customerListForm.SelectedCustomerId
                selectedCustomerName = customerListForm.SelectedCustomerName
                selectedCustomerPhone = customerListForm.SelectedCustomerPhone
                SetSelectedCustomer(selectedCustomerId, selectedCustomerName, selectedCustomerPhone)
            End If

        Catch ex As Exception
            MessageBox.Show("Error selecting customer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Method to get selected customer information
    Public Function GetSelectedCustomer() As Object
        Return New With {
            .CustomerId = selectedCustomerId,
            .CustomerName = selectedCustomerName,
            .CustomerPhone = selectedCustomerPhone
        }
    End Function
    Private Sub barclearcustomer_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barclearcustomer.ItemClick
        Try
            ClearSelectedCustomer()
        Catch ex As Exception

        End Try
    End Sub
    ' Method to clear selected customer
    Public Sub ClearSelectedCustomer()
        Try
            selectedCustomerId = 0
            selectedCustomerName = String.Empty
            selectedCustomerPhone = String.Empty
            barselectcustomer1.Caption = "Select Customer"
            barselectcustomer1.Hint = "Click to select a customer for this bill"

            ' Reset button appearance
            barselectcustomer1.ItemAppearance.Normal.ForeColor = Color.Black
            barselectcustomer1.ItemAppearance.Normal.Font = New Font(barselectcustomer1.ItemAppearance.Normal.Font, FontStyle.Regular)

            ' Update customer grid to clear display
            UpdateCustomerGrid()

        Catch ex As Exception
            MessageBox.Show("Error clearing customer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Method to set customer programmatically
    Public Sub SetSelectedCustomer(customerId As Integer, customerName As String, customerPhone As String)
        Try
            selectedCustomerId = customerId
            selectedCustomerName = customerName
            selectedCustomerPhone = customerPhone

            ' Update customer grid to show selected customer
            UpdateCustomerGrid()

        Catch ex As Exception
            MessageBox.Show("Error setting customer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Method to validate if customer selection is required
    Public Function ValidateCustomerSelection(Optional showMessage As Boolean = True) As Boolean
        Try
            If selectedCustomerId = 0 Then
                If showMessage Then
                    MessageBox.Show("Please select a customer before proceeding with this bill.", _
                                  "Customer Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
                Return False
            End If
            Return True
        Catch ex As Exception
            If showMessage Then
                MessageBox.Show("Error validating customer selection: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        End Try
    End Function

    ' Method to check if a customer is selected
    Public Function IsCustomerSelected() As Boolean
        Return selectedCustomerId > 0
    End Function

    ' Customer Grid Management Methods
    Private Sub InitializeCustomerGrid()
        Try
            ' Create customer display DataTable
            customerDisplayTable = New DataTable()
            customerDisplayTable.Columns.Add("Id", GetType(Integer))
            customerDisplayTable.Columns.Add("Name", GetType(String))
            customerDisplayTable.Columns.Add("Phone", GetType(String))

            ' Bind to GridControl
            GridControlCustomer.DataSource = customerDisplayTable

            ' Configure grid appearance
            GridViewCustomer.OptionsBehavior.ReadOnly = True
            GridViewCustomer.OptionsBehavior.Editable = False
            GridViewCustomer.OptionsSelection.EnableAppearanceFocusedCell = False
            GridViewCustomer.OptionsView.ShowGroupPanel = False
            GridViewCustomer.OptionsView.ShowIndicator = False

        Catch ex As Exception
            MessageBox.Show("Error initializing customer grid: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateCustomerGrid()
        Try
            ' Clear existing data
            customerDisplayTable.Clear()

            ' Add selected customer to grid if available
            If selectedCustomerId > 0 Then
                Dim row As DataRow = customerDisplayTable.NewRow()
                row("Id") = selectedCustomerId
                row("Name") = selectedCustomerName
                row("Phone") = selectedCustomerPhone
                customerDisplayTable.Rows.Add(row)
            End If

            ' Refresh the grid
            GridControlCustomer.RefreshDataSource()

        Catch ex As Exception
            MessageBox.Show("Error updating customer grid: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
#Region "SalesMan"
    Private Sub barselectsalesman_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barselectsalesman.ItemClick
        Try
            If _globalSetting.SalesManEachItemActive = False Then
                MessageBox.Show("Salesman selection for each item is not active.", "Feature Disabled", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' Check if any item is selected in the grid
            If GridViewPOS.FocusedRowHandle < 0 Then
                'MessageBox.Show("Please select an item to assign a salesman.", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' Show salesman selection dialog
            Dim salesmanForm As New FrmSalesmanList(True) ' True for selection mode
            If salesmanForm.ShowDialog() = DialogResult.OK Then
                ' Update the selected item with salesman information
                UpdateItemSalesman(GridViewPOS.FocusedRowHandle,
                                 salesmanForm.SelectedSalesmanId,
                                 salesmanForm.SelectedSalesmanName)

            End If
        Catch ex As Exception
            MessageBox.Show("Error selecting salesman: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Method to update salesman information for a specific item
    Private Sub UpdateItemSalesman(rowIndex As Integer, salesmanId As Integer, salesmanName As String)
        Try
            If rowIndex < 0 OrElse rowIndex >= GridDataTble_Insert.Rows.Count Then
                Exit Sub
            End If
            If _JsonData.SalesManCommissionTable.Rows.Count = 0 Then
                getSalesManCommissionInfo()
            End If
            Dim ItemId As Integer = Convert.ToInt32(GridDataTble_Insert.Rows(rowIndex)("ITEMCODE"))
            Dim commissionPercentage As Decimal = 0

            ' Find commission for specific salesman and item using safe type conversion
            Dim commissionRow = _JsonData.SalesManCommissionTable.AsEnumerable().
                               FirstOrDefault(Function(row) Convert.ToInt32(row("EmpId")) = salesmanId AndAlso
                                                           Convert.ToInt32(row("ItemId")) = ItemId)

            If commissionRow IsNot Nothing Then
                commissionPercentage = Convert.ToDecimal(commissionRow("CommissionPercentage"))
            Else
                commissionPercentage = 0
            End If

            ' Update the salesman information in DataTable (note: column names with spaces)
            GridDataTble_Insert.Rows(rowIndex)("SALESPERSONID") = salesmanId
            GridDataTble_Insert.Rows(rowIndex)("SALESPERSON") = salesmanName
            GridDataTble_Insert.Rows(rowIndex)("SALESMANPER") = commissionPercentage


            ' Accept changes
            GridDataTble_Insert.AcceptChanges()

            ' Refresh the grid
            GridControlSalesData.DataSource = GridDataTble_Insert

        Catch ex As Exception
            MessageBox.Show("Error updating salesman: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    '' Method to get salesman summary for reporting
    'Public Function GetSalesmanSummary() As Object
    '    Try
    '        Dim salesmanSummary As New Dictionary(Of Integer, Object)

    '        For i As Integer = 0 To GridDataTble_Insert.Rows.Count - 1
    '            Dim salesmanId As Integer = Convert.ToInt32(GridDataTble_Insert.Rows(i)("SALESPERSONID "))
    '            Dim salesmanName As String = GridDataTble_Insert.Rows(i)("SALESPERSON").ToString()
    '            Dim salesmanPer As Decimal = Convert.ToDecimal(GridDataTble_Insert.Rows(i)("SALESMANPER"))
    '            Dim itemAmount As Decimal = Convert.ToDecimal(GridDataTble_Insert.Rows(i)("NETAMT"))
    '            Dim commissionAmount As Decimal = (itemAmount * salesmanPer) / 100

    '            If salesmanSummary.ContainsKey(salesmanId) Then
    '                Dim existing = salesmanSummary(salesmanId)
    '                salesmanSummary(salesmanId) = New With {
    '                    .SalesmanId = salesmanId,
    '                    .SalesmanName = salesmanName,
    '                    .TotalSalesAmount = existing.TotalSalesAmount + itemAmount,
    '                    .TotalCommissionAmount = existing.TotalCommissionAmount + commissionAmount,
    '                    .ItemCount = existing.ItemCount + 1
    '                }
    '            Else
    '                salesmanSummary.Add(salesmanId, New With {
    '                    .SalesmanId = salesmanId,
    '                    .SalesmanName = salesmanName,
    '                    .TotalSalesAmount = itemAmount,
    '                    .TotalCommissionAmount = commissionAmount,
    '                    .ItemCount = 1
    '                })
    '            End If
    '        Next

    '        Return salesmanSummary.Values.ToList()

    '    Catch ex As Exception
    '        MessageBox.Show("Error getting salesman summary: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return New List(Of Object)()
    '    End Try
    'End Function

    '' Method to assign default salesman to all items
    'Public Sub AssignDefaultSalesmanToAllItems(salesmanId As Integer, salesmanName As String, salesmanPercentage As Decimal)
    '    Try
    '        For i As Integer = 0 To GridDataTble_Insert.Rows.Count - 1
    '            GridDataTble_Insert.Rows(i)("SALESPERSONID ") = salesmanId
    '            GridDataTble_Insert.Rows(i)("SALESPERSON") = salesmanName
    '            GridDataTble_Insert.Rows(i)("SALESMANPER") = salesmanPercentage
    '        Next

    '        ' Accept changes and refresh grid
    '        GridDataTble_Insert.AcceptChanges()
    '        GridControlSalesData.DataSource = GridDataTble_Insert

    '        MessageBox.Show("Default salesman assigned to all items successfully!", "Default Salesman Assigned", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '    Catch ex As Exception
    '        MessageBox.Show("Error assigning default salesman: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
#End Region
#Region "Form Layout Management"
    Private Sub Barfromlayoutsave_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles Barfromlayoutsave.ItemClick
        Try
            SaveFormLayout()
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show("Error saving form layout: " & ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Save complete form layout
    Private Sub SaveFormLayout()
        Try
            Dim layoutPath As String = GetFormLayoutFilePath()
            ' Create directory if it doesn't exist
            Dim layoutDir As String = System.IO.Path.GetDirectoryName(layoutPath)
            If Not System.IO.Directory.Exists(layoutDir) Then
                System.IO.Directory.CreateDirectory(layoutDir)
            End If
            LayoutControl1.SaveLayoutToXml(layoutPath)

            DevExpress.XtraEditors.XtraMessageBox.Show("Form layout saved successfully!", "Layout Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            Throw New Exception("Error saving form layout: " & ex.Message)
        End Try
    End Sub

    ' Restore complete form layout
    Public Sub RestoreFormLayout()
        Try
            Dim layoutPath As String = GetFormLayoutFilePath()
            If Not System.IO.File.Exists(layoutPath) Then
                Exit Sub ' No saved layout exists
            End If
            LayoutControl1.RestoreLayoutFromXml(layoutPath)
            'LayoutControl2.RestoreLayoutFromXml(layoutPath)
            'LayoutControl3.RestoreLayoutFromXml(layoutPath)
        Catch ex As Exception
            ' If restore fails, continue with default layout
            DevExpress.XtraEditors.XtraMessageBox.Show("Error restoring form layout. Using default layout.", "Layout Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    ' Get form layout file path
    Private Function GetFormLayoutFilePath() As String
        Dim appPath As String = Application.StartupPath
        Dim layoutFolder As String = System.IO.Path.Combine(appPath, "Layout")
        Return System.IO.Path.Combine(layoutFolder, "PosSalesII_FormLayout.xml")
    End Function

#End Region
#Region "Sales Data Conversion"

    Private Function GetSalesDetailJson() As String
        Try
            Dim salesList As New List(Of SalesDetails)

            For Each row As DataRow In GridDataTble_Insert.Rows
                Dim detail As New SalesDetails(row)
                ' Set basic fields
                detail.psid_invoice_date = DateTime.Now
                detail.psid_invoice_trno = lblinvoiceno.Text
                detail.psid_invoice_shiftno = _saleSetting._curShiftno
                detail.psid_invoice_dayno = _saleSetting._curDayno
                detail.psid_invoice_salid = 0
                detail.psid_invoice_pmid = _companyInfo.CompanyPMID
                salesList.Add(detail)
            Next

            Return JsonConvert.SerializeObject(salesList)
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show("Error creating JSON: " & ex.Message)
            Return "[]"
        End Try
    End Function

#End Region
#Region "PaymentProcess"
    Private Sub btnpayment_Click(sender As Object, e As EventArgs) Handles btnpayment.Click
        Try
            If GridDataTble_Insert.Rows.Count > 0 Then
                For Each SalesData In GridDataTble_Insert.Rows
                    SalesData("ITEMLOCK") = 2
                Next
                GridDataTble_Insert.AcceptChanges()

                PaymentProcess()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PaymentProcess()
        Try

            If modeOfSale = "New" Then
                If GridViewPOS.RowCount > 0 Then
                    frmPaymore.ShowDialog(lblnetamt.Text, selectedCustomerName)
                    Dim _givenAmt As Decimal = 0.0
                    Dim _BalanceAmt As Decimal = 0.0
                    If frmPaymore.DialogResult = Windows.Forms.DialogResult.OK Then
                        ' Get payment details from PaymentDetailTable
                        Dim paymentModeSelections As New List(Of String)()
                        Dim paymentModes As New List(Of String)()

                        ' Check if multiple payments or single payment
                        If frmPaymore.PaymentDetailTable.Rows.Count > 1 Then
                            ' Multiple payments - combine all payment modes
                            For Each paymentRow As DataRow In frmPaymore.PaymentDetailTable.Rows
                                Dim paymentName As String = paymentRow("pmode_name").ToString()
                                Dim paymentType As String = paymentRow("pmode_type").ToString()
                                paymentModeSelections.Add(paymentName)

                                ' Map payment names to payment modes
                                Select Case paymentType.ToLower()
                                    Case "cash"
                                        paymentModes.Add("cash")
                                    Case "credit card", "debit card", "card", "bank card"
                                        paymentModes.Add("card")
                                    Case "bank transfer", "upi", "online", "bank"
                                        paymentModes.Add("bank")
                                    Case "credit"
                                        paymentModes.Add("credit")
                                    Case Else
                                        paymentModes.Add("cash") ' Default to cash
                                End Select
                            Next

                            ' Set combined payment modes
                            _PaymentDtl.paymentModeSelection = String.Join(",", paymentModeSelections)
                            _PaymentDtl.paymentMode = String.Join(",", paymentModes.Distinct())

                        ElseIf frmPaymore.PaymentDetailTable.Rows.Count = 1 Then
                            ' Single payment mode
                            Dim paymentRow As DataRow = frmPaymore.PaymentDetailTable.Rows(0)
                            Dim paymentName As String = paymentRow("pmode_name").ToString()
                            _PaymentDtl.paymentModeSelection = paymentName

                            ' Map payment name to payment mode
                            Select Case paymentName.ToLower()
                                Case "cash"
                                    _PaymentDtl.paymentMode = "cash"
                                Case "credit card", "debit card", "card", "bank card"
                                    _PaymentDtl.paymentMode = "card"
                                Case "bank transfer", "upi", "online", "bank"
                                    _PaymentDtl.paymentMode = "bank"
                                Case "credit"
                                    _PaymentDtl.paymentMode = "credit"
                                Case Else
                                    _PaymentDtl.paymentMode = "cash" ' Default to cash
                            End Select
                        Else
                            ' No payments in table, use default cash
                            _PaymentDtl.paymentModeSelection = "Cash Bill"
                            _PaymentDtl.paymentMode = "cash"
                        End If

                        Dim _saleData As New SalesHeader
                        _saleData.psih_invoice_pmid = _companyInfo.CompanyPMID
                        _saleData.psih_invoice_trno = 0
                        Dim invoicedate As String = ""
                        _DateConversion(lblinvoicedate.Text, invoicedate)
                        _saleData.psih_invoice_date = invoicedate
                        _saleData.psih_invoice_prefix = billPrefix.ToString
                        _saleData.psih_invoice_tqty = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("QTY"))
                        _saleData.psih_invoice_tamount = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("TAMOUNT"))
                        _saleData.psih_invoice_titemdisper = GridDataTble_Insert.AsEnumerable().Average(Function(row) row.Field(Of Decimal)("ITEM_DPER"))
                        _saleData.psih_invoice_titemdisamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("ITEM_DAMT"))
                        _saleData.psih_invoice_tbilldiscper = GridDataTble_Insert.AsEnumerable().Average(Function(row) row.Field(Of Decimal)("BILL_DPER"))
                        _saleData.psih_invoice_tbilldiscamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("BILL_DAMT"))
                        _saleData.psih_invoice_totdiscper = GridDataTble_Insert.AsEnumerable().Average(Function(row) row.Field(Of Decimal)("TOTAL_DPER"))
                        _saleData.psih_invoice_totdiscamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("TOTAL_DAMT"))
                        _saleData.psih_invoice_tgrossamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("GAMOUNT"))
                        _saleData.psih_invoice_ttaxamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("TAXAMT"))
                        _saleData.psih_invoice_sercharge = lblservchargetotal.Text
                        _saleData.psih_invoice_roundoff = 0
                        Dim nettotal As Decimal = 0
                        Dim serviecharge As Decimal = 0
                        If _globalSetting.ServiceTaxActive = True Then
                            serviecharge = ConvertDecimal(lblservchargetotal.Text)
                            nettotal = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("NETAMT"))
                            nettotal = nettotal + serviecharge
                        Else
                            nettotal = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("NETAMT"))
                        End If
                        _saleData.psih_invoice_tnetamt = nettotal
                        _saleData.psih_invoice_saletype = "Invoice"
                        _saleData.psih_invoice_billtype = _PaymentDtl.paymentModeSelection

                        ' Handle both single and multiple payment modes for bill status
                        If _PaymentDtl.paymentMode.Contains("credit") Then
                            ' If any payment includes credit, bill remains open
                            _saleData.psih_invoice_billstatus = "Open"
                        Else
                            ' If no credit payment (cash, card, bank, etc.), bill is closed
                            _saleData.psih_invoice_billstatus = "Closed"
                        End If

                        _saleData.psih_invoice_paymode = _PaymentDtl.paymentMode
                        If String.IsNullOrEmpty(selectedCustomerName) Then
                            _saleData.psih_invoice_customerid = 1
                            _saleData.psih_invoice_description = "Default Customer"
                        Else
                            _saleData.psih_invoice_customerid = selectedCustomerId
                            _saleData.psih_invoice_description = selectedCustomerName
                        End If
                        _saleData.psih_invoice_userid = _companyInfo.UserId
                        _saleData.psih_invoice_comid = _companyInfo.ComId
                        _saleData.psih_invoice_locid = _companyInfo.LocId
                        _saleData.psih_invoice_countername = Environment.MachineName
                        _saleData.psih_invoice_billremarks = "-"

                        ' Advance amount validation - only allow advance for credit bills
                        If frmPaymore.txtadvanceamt.EditValue > 0 And Not _PaymentDtl.paymentMode.Contains("credit") Then
                            _saleData.psih_invoice_advamt = 0
                            DevExpress.XtraEditors.XtraMessageBox.Show("Advance Amount Can Only Be Accepted For Credit Bills.", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else
                            If frmPaymore.txtadvanceamt.EditValue > 0 Then
                                _saleData.psih_invoice_advamt = frmPaymore.txtadvanceamt.EditValue
                                _saleData.psih_invoice_outstanding = _saleData.psih_invoice_tnetamt - _saleData.psih_invoice_advamt
                            ElseIf _PaymentDtl.paymentMode.Contains("credit") Then
                                _saleData.psih_invoice_outstanding = _saleData.psih_invoice_tnetamt
                            Else
                                _saleData.psih_invoice_advamt = 0
                            End If
                        End If

                        If frmPaymore.txttpopenamt.EditValue Is Nothing Then
                            _saleData.psih_invoice_givenamt = 0
                        Else
                            _saleData.psih_invoice_givenamt = frmPaymore.txttpopenamt.EditValue
                        End If
                        If frmPaymore.txtpopbalamt.EditValue Is Nothing Then
                            _saleData.psih_invoice_balamt = 0
                        Else
                            _saleData.psih_invoice_balamt = frmPaymore.txtpopbalamt.EditValue
                        End If
                        _saleData.psih_invoice_shiftno = _saleSetting._curShiftno
                        _saleData.psih_invoice_dayno = _saleSetting._curDayno
                        _saleData.psih_invoice_countername = Environment.MachineName
                        _saleData.psih_invoice_print = "0"
                        _saleData.psih_invoice_webhost = "0"
                        ' Basic validation - check if we have items
                        If GridDataTble_Insert.Rows.Count = 0 Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("No items to save", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        ' Save to database using stored procedure with payment modes
                        Dim salesHelper As New SalesDBHelper(M_Details._Conn)
                        Dim salesDetailsList As List(Of SalesDetails) = salesHelper.ConvertDataTableToSalesDetails(GridDataTble_Insert)
                        Dim _errMsgResult As String = ""
                        Dim ReturnBill As String = "0"

                        ' Always use Dictionary method for both single and multiple payments
                        If salesHelper.SaveSalesBill(_saleData, salesDetailsList, GetPaymentModesDictionary(), _errMsgResult, ReturnBill) = True Then
                            barstatuslastbillno.Caption = ReturnBill
                            _CashDraw.OpenCashdrawer(True)
                            DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & " Bill Saved", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            barbtnNewBill_ItemClick(Nothing, Nothing)
                        Else
                            DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & " Bill Not Saved", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If

            ElseIf modeOfSale = "Edit" Then
                If GridViewPOS.RowCount > 0 Then
                    frmPaymore.ShowDialog(lblnetamt.Text, selectedCustomerName)
                    Dim _givenAmt As Decimal = 0.0
                    Dim _BalanceAmt As Decimal = 0.0
                    If frmPaymore.DialogResult = Windows.Forms.DialogResult.OK Then
                        ' Get payment details from PaymentDetailTable
                        Dim paymentModeSelections As New List(Of String)()
                        Dim paymentModes As New List(Of String)()

                        ' Check if multiple payments or single payment
                        If frmPaymore.PaymentDetailTable.Rows.Count > 1 Then
                            ' Multiple payments - combine all payment modes
                            For Each paymentRow As DataRow In frmPaymore.PaymentDetailTable.Rows
                                Dim paymentName As String = paymentRow("pmode_name").ToString()
                                Dim paymentType As String = paymentRow("pmode_type").ToString()
                                paymentModeSelections.Add(paymentName)

                                ' Map payment types to payment modes
                                Select Case paymentType.ToLower()
                                    Case "cash"
                                        paymentModes.Add("cash")
                                    Case "credit card", "debit card", "card", "bank card"
                                        paymentModes.Add("card")
                                    Case "bank transfer", "upi", "online", "bank"
                                        paymentModes.Add("bank")
                                    Case "credit"
                                        paymentModes.Add("credit")
                                    Case Else
                                        paymentModes.Add("cash") ' Default to cash
                                End Select
                            Next

                            ' Set combined payment modes
                            _PaymentDtl.paymentModeSelection = String.Join(",", paymentModeSelections)
                            _PaymentDtl.paymentMode = String.Join(",", paymentModes.Distinct())

                        ElseIf frmPaymore.PaymentDetailTable.Rows.Count = 1 Then
                            ' Single payment mode
                            Dim paymentRow As DataRow = frmPaymore.PaymentDetailTable.Rows(0)
                            Dim paymentName As String = paymentRow("pmode_name").ToString()
                            Dim paymentType As String = paymentRow("pmode_type").ToString()
                            _PaymentDtl.paymentModeSelection = paymentName

                            ' Map payment type to payment mode
                            Select Case paymentType.ToLower()
                                Case "cash"
                                    _PaymentDtl.paymentMode = "cash"
                                Case "credit card", "debit card", "card", "bank card"
                                    _PaymentDtl.paymentMode = "card"
                                Case "bank transfer", "upi", "online", "bank"
                                    _PaymentDtl.paymentMode = "bank"
                                Case "credit"
                                    _PaymentDtl.paymentMode = "credit"
                                Case Else
                                    _PaymentDtl.paymentMode = "cash" ' Default to cash
                            End Select
                        Else
                            ' No payments in table, use default cash
                            _PaymentDtl.paymentModeSelection = "Cash Bill"
                            _PaymentDtl.paymentMode = "cash"
                        End If

                        Dim _saleData As New SalesHeader
                        _saleData.psih_invoice_pmid = _companyInfo.CompanyPMID
                        Dim invoicedate As String = ""
                        _DateConversion(lblinvoicedate.Text, invoicedate)
                        _saleData.psih_invoice_date = invoicedate
                        _saleData.psih_invoice_id = G_SalID
                        _saleData.psih_invoice_trno = lblinvoiceno.Text
                        _saleData.psih_invoice_prefix = billPrefix.ToString
                        _saleData.psih_invoice_tqty = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("QTY"))
                        _saleData.psih_invoice_tamount = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("TAMOUNT"))
                        _saleData.psih_invoice_titemdisper = GridDataTble_Insert.AsEnumerable().Average(Function(row) row.Field(Of Decimal)("ITEM_DPER"))
                        _saleData.psih_invoice_titemdisamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("ITEM_DAMT"))
                        _saleData.psih_invoice_tbilldiscper = GridDataTble_Insert.AsEnumerable().Average(Function(row) row.Field(Of Decimal)("BILL_DPER"))
                        _saleData.psih_invoice_tbilldiscamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("BILL_DAMT"))
                        _saleData.psih_invoice_totdiscper = GridDataTble_Insert.AsEnumerable().Average(Function(row) row.Field(Of Decimal)("TOTAL_DPER"))
                        _saleData.psih_invoice_totdiscamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("TOTAL_DAMT"))
                        _saleData.psih_invoice_tgrossamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("GAMOUNT"))
                        _saleData.psih_invoice_ttaxamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("TAXAMT"))
                        _saleData.psih_invoice_sercharge = lblservchargetotal.Text
                        _saleData.psih_invoice_roundoff = 0
                        Dim nettotal As Decimal = 0
                        Dim serviecharge As Decimal = 0
                        If _globalSetting.ServiceTaxActive = True Then
                            serviecharge = ConvertDecimal(lblservchargetotal.Text)
                            nettotal = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("NETAMT"))
                            nettotal = nettotal + serviecharge
                        Else
                            nettotal = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("NETAMT"))
                        End If
                        _saleData.psih_invoice_tnetamt = nettotal
                        _saleData.psih_invoice_saletype = "Invoice"
                        _saleData.psih_invoice_billtype = _PaymentDtl.paymentModeSelection

                        ' Handle both single and multiple payment modes for bill status
                        If _PaymentDtl.paymentMode.Contains("credit") Then
                            ' If any payment includes credit, bill remains open
                            _saleData.psih_invoice_billstatus = "Open"
                        Else
                            ' If no credit payment (cash, card, bank, etc.), bill is closed
                            _saleData.psih_invoice_billstatus = "Closed"
                        End If

                        _saleData.psih_invoice_paymode = _PaymentDtl.paymentMode
                        If String.IsNullOrEmpty(selectedCustomerName) Then
                            _saleData.psih_invoice_customerid = 1
                            _saleData.psih_invoice_description = "Default Customer"
                        Else
                            _saleData.psih_invoice_customerid = selectedCustomerId
                            _saleData.psih_invoice_description = selectedCustomerName
                        End If
                        _saleData.psih_invoice_userid = _companyInfo.UserId
                        _saleData.psih_invoice_comid = _companyInfo.ComId
                        _saleData.psih_invoice_locid = _companyInfo.LocId
                        _saleData.psih_invoice_billremarks = "-"

                        ' Advance amount validation - only allow advance for credit bills
                        If frmPaymore.txtadvanceamt.EditValue > 0 And Not _PaymentDtl.paymentMode.Contains("credit") Then
                            _saleData.psih_invoice_advamt = 0
                            DevExpress.XtraEditors.XtraMessageBox.Show("Advance Amount Can Only Be Accepted For Credit Bills.", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else
                            If frmPaymore.txtadvanceamt.EditValue > 0 Then
                                _saleData.psih_invoice_advamt = frmPaymore.txtadvanceamt.EditValue
                                _saleData.psih_invoice_outstanding = _saleData.psih_invoice_tnetamt - _saleData.psih_invoice_advamt
                            ElseIf _PaymentDtl.paymentMode.Contains("credit") Then
                                _saleData.psih_invoice_outstanding = _saleData.psih_invoice_tnetamt
                            Else
                                _saleData.psih_invoice_advamt = 0
                            End If
                        End If

                        If frmPaymore.txttpopenamt.EditValue Is Nothing Then
                            _saleData.psih_invoice_givenamt = 0
                        Else
                            _saleData.psih_invoice_givenamt = frmPaymore.txttpopenamt.EditValue
                        End If
                        If frmPaymore.txtpopbalamt.EditValue Is Nothing Then
                            _saleData.psih_invoice_balamt = 0
                        Else
                            _saleData.psih_invoice_balamt = frmPaymore.txtpopbalamt.EditValue
                        End If
                        _saleData.psih_invoice_shiftno = _saleSetting._curShiftno
                        _saleData.psih_invoice_dayno = _saleSetting._curDayno
                        _saleData.psih_invoice_countername = Environment.MachineName
                        _saleData.psih_invoice_print = "0"
                        _saleData.psih_invoice_webhost = "0"
                        ' Basic validation - check if we have items
                        If GridDataTble_Insert.Rows.Count = 0 Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("No items to update", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        ' Update to database using stored procedure with payment modes

                        Dim salesDetailsList As List(Of SalesDetails) = salesHelper.ConvertDataTableToSalesDetails(GridDataTble_Insert)
                        Dim _errMsgResult As String = ""
                        Dim ReturnBill As String = "0"

                        ' Always use Dictionary method for both single and multiple payments
                        If salesHelper.UpdateSalesBill(_saleData, salesDetailsList, GetPaymentModesDictionary(), _errMsgResult, ReturnBill) = True Then
                            barstatuslastbillno.Caption = ReturnBill
                            _CashDraw.OpenCashdrawer(True)
                            DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & " Bill Updated", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            barbtnNewBill_ItemClick(Nothing, Nothing)
                        Else
                            DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & " Bill Not Updated", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show("View Mode Cant Be Save Bill", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message & "Bill Not Processed", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    ''' <summary>
    ''' Convert PaymentDetailTable to Dictionary for database operations
    ''' </summary>
    Private Function GetPaymentModesDictionary() As Dictionary(Of Integer, Decimal)
        Try
            Dim paymentModes As New Dictionary(Of Integer, Decimal)()

            For Each paymentRow As DataRow In frmPaymore.PaymentDetailTable.Rows
                Dim paymentId As Integer = Convert.ToInt32(paymentRow("pmode_id"))
                Dim amount As Decimal = ConvertDecimal(paymentRow("pmode_amount"))
                paymentModes.Add(paymentId, amount)
            Next

            Return paymentModes
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show("Error creating payment modes: " & ex.Message, "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return New Dictionary(Of Integer, Decimal)()
        End Try
    End Function
    ''' <summary>
    ''' Safely convert object to decimal, handling DBNull and empty values
    ''' </summary>
    Private Function ConvertDecimal(value As Object) As Decimal
        Try
            If value Is Nothing OrElse value Is DBNull.Value OrElse String.IsNullOrEmpty(value.ToString()) Then
                Return 0D
            End If
            Return Convert.ToDecimal(value)
        Catch ex As Exception
            Return 0D
        End Try
    End Function
#End Region
#Region "ViewEdit"
    Private Sub barbtnviewbill_Click(sender As Object, e As EventArgs) Handles barbtnviewbill.ItemClick
        Try
            Dim modeofbill As String = ""
            frmSelectBill.ShowDialog()
            If frmSelectBill.DialogResult = Windows.Forms.DialogResult.OK Then
                modeOfSale = "View"
                If GetSalesBySalID(G_SalID, modeofbill) = True Then
                    modeOfSale = modeofbill
                    barbtnstatus.Caption = "Sales Mode : " & modeOfSale
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function GetSalesBySalID(ByVal Sal_id As Integer, ByRef ModeOfBill As String) As Boolean
        Try
            Dim resData As New DataSet
            If GetSalesByBillLocal(Sal_id, resData, "V") = True Then

                ModeOfBill = "View"
                If resData.Tables(0).Rows.Count > 0 Then
                    Dim billno = resData.Tables(0).Rows(0)("psih_invoice_trno")
                    lblinvoiceno.Text = billno
                    Dim CustomerId = resData.Tables(0).Rows(0)("psih_invoice_customerid")
                    selectedCustomerId = CustomerId
                    If _JsonData.CustomerTable.Rows.Count = 0 Then
                        getCustomerMaster()
                    End If
                    If _JsonData.CustomerTable.Rows.Count > 0 Then
                        Dim custonerRow = _JsonData.CustomerTable.AsEnumerable().
                                           FirstOrDefault(Function(row) Convert.ToInt32(row("CustomerId")) = CustomerId)

                        If custonerRow IsNot Nothing Then
                            selectedCustomerName = custonerRow("CustomerName").ToString()
                            selectedCustomerPhone = custonerRow("CustomerPhone").ToString()
                            UpdateCustomerGrid()
                        End If
                    End If
                    Dim Remarks = resData.Tables(0).Rows(0)("psih_invoice_billremarks")
                    barbtnbilltype.Caption = "Bill Type : " & resData.Tables(0).Rows(0)("psih_invoice_billtype")
                End If
                If resData.Tables(1).Rows.Count > 0 Then
                    _SnoCount = 0
                    GridDataTble_Insert.Rows.Clear()
                    GridDataTble_Insert.NewRow()
                    GridDataTble_Insert.BeginInit()
                    For Each rData In resData.Tables(1).Rows
                        _SnoCount = _SnoCount + 1
                        ' Declare variables for data mapping
                        Dim barcode As Object = If(rData.Table.Columns.Contains("psid_invoice_barcode"), rData("psid_invoice_barcode"), 0)
                        Dim itemCode As Object = rData("psid_invoice_procode")
                        Dim itemName As String = Trim(rData("psid_invoice_description"))
                        Dim serialNo As String = If(rData.Table.Columns.Contains("psid_invoice_serialno"), rData("psid_invoice_serialno"), "0")
                        Dim uom As String = If(rData.Table.Columns.Contains("psid_invoice_uom"), rData("psid_invoice_uom"), "PCS")
                        Dim rate As Object = rData("psid_invoice_rate")
                        Dim qty As Object = rData("psid_invoice_proqty")
                        Dim amount As Object = rData("psid_invoice_amt")
                        Dim itemDiscPer As Object = rData("psid_invoice_itemdisp")
                        Dim itemDiscAmt As Object = rData("psid_invoice_itemdisamt")
                        Dim billDiscPer As Object = rData("psid_invoice_billdisp")
                        Dim billDiscAmt As Object = rData("psid_invoice_billdisamt")
                        Dim totalDiscPer As Object = rData("psid_invoice_totdper")
                        Dim totalDiscAmt As Object = rData("psid_invoice_totdamt")
                        Dim grossAmount As Object = rData("psid_invoice_gross")
                        Dim taxValue As Object = rData("psid_invoice_taxvalue")
                        Dim taxAmount As Object = rData("psid_invoice_taxamt")
                        Dim netAmount As Decimal = _RoundOff(rData("psid_invoice_netamt"))
                        Dim remarks As String = If(rData.Table.Columns.Contains("psid_invoice_remarks"), rData("psid_invoice_remarks"), "Remarks")
                        Dim batchNo As Object = If(rData.Table.Columns.Contains("psid_invoice_batchno"), rData("psid_invoice_batchno"), 0)
                        Dim salesPersonId As Integer = If(rData.Table.Columns.Contains("psid_invoice_salespersonid"), Convert.ToInt32(rData("psid_invoice_salespersonid")), 1)
                        Dim salesManPer As Object = If(rData.Table.Columns.Contains("psid_invoice_salesmanper"), rData("psid_invoice_salesmanper"), 0)
                        Dim deleteFlag As Object = If(rData.Table.Columns.Contains("psid_invoice_delete"), rData("psid_invoice_delete"), 1)
                        Dim itemLock As Object = If(rData.Table.Columns.Contains("psid_invoice_itemlock"), rData("psid_invoice_itemlock"), 2)
                        Dim psid As Object = If(rData.Table.Columns.Contains("psid_invoice_id"), rData("psid_invoice_id"), 0)

                        ' Get salesperson name from _JsonData.SalesManCommissionTable using LINQ
                        Dim salesPersonName As String = "Default"
                        Try
                            If _JsonData.SalesManCommissionTable.Rows.Count = 0 Then
                                getSalesManCommissionInfo()
                            End If

                            If _JsonData.SalesManCommissionTable.Rows.Count > 0 Then
                                Dim salesPersonRow = _JsonData.SalesManCommissionTable.AsEnumerable().
                                                   FirstOrDefault(Function(row) Convert.ToInt32(row("EmpId")) = salesPersonId)

                                If salesPersonRow IsNot Nothing Then
                                    salesPersonName = salesPersonRow("SalesManName").ToString()
                                End If
                            End If
                        Catch ex As Exception
                            ' If error getting salesperson name, use default or from database if available
                            If rData.Table.Columns.Contains("psid_invoice_salesperson") AndAlso rData("psid_invoice_salesperson") IsNot DBNull.Value Then
                                salesPersonName = rData("psid_invoice_salesperson").ToString()
                            End If
                        End Try

                        ' Map database fields to DataTable columns with PSID support
                        ' Column order: SNO, BARCODE, ITEMCODE, ITEMNAME, SERIALNO, UOM, RATE, QTY, TAMOUNT,
                        '              ITEM_DPER, ITEM_DAMT, BILL_DPER, BILL_DAMT, TOTAL_DPER, TOTAL_DAMT,
                        '              GAMOUNT, TAXVALUE, TAXAMT, NETAMT, ITEMREMARS, BATCHNO, SALESPERSONID, SALESPERSON, SALESMANPER, DELETE, ITEMLOCK, PSID
                        GridDataTble_Insert.Rows.Add(_SnoCount, barcode, itemCode, itemName, serialNo, uom, rate, qty, amount, _
                                                    itemDiscPer, itemDiscAmt, billDiscPer, billDiscAmt, totalDiscPer, totalDiscAmt, _
                                                    grossAmount, taxValue, taxAmount, netAmount, remarks, batchNo, _
                                                    salesPersonId, salesPersonName, salesManPer, deleteFlag, itemLock, psid)
                    Next
                    GridDataTble_Insert.AcceptChanges()
                    GridDataTble_Insert.EndInit()
                    GridControlSalesData.DataSource = GridDataTble_Insert
                    GridViewPOS.MoveNext()

                    ' GridDataTble_Insert.WriteXml(M_Details._appPath & "Layout\SaleRecentData.xml", True, System.Data.XmlWriteMode.WriteSchema)
                    'txtnotes.Text = "-"
                    If SalesGrandtotal("ER") = False Then

                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetSalesBySalIDWeb(ByVal Sal_id As Integer, ByRef ModeOfBill As String) As Boolean
        Try
            Dim billDtl As New DataTable
            Dim billHdr As New DataTable
            billDtl.TableName = "billDtl"
            billHdr.TableName = "billHdr"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = ""
            If _globalSetting.QuoteBill = True Then
                json = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesRequest=12&sal_id=" & Sal_id)
                _globalSetting.QuoteBill = False
                ModeOfBill = "Quote"
            Else
                json = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesRequest=6&sal_id=" & Sal_id)
                ModeOfBill = "View"
            End If

            Dim Userparsejson As JObject = JObject.Parse(json)
            Dim Msg = Userparsejson("Success").ToString
            If Msg.ToString = "True" Then
                billDtl = Userparsejson("DTL").ToObject(Of DataTable)()
                billHdr = Userparsejson("HDR").ToObject(Of DataTable)()
                If billHdr.Rows.Count > 0 Then
                    Dim billno = billHdr.Rows(0)("psih_invoice_trno")
                    lblinvoiceno.Text = billno
                    Dim CustomerId = billHdr.Rows(0)("psih_invoice_customerid")
                    selectedCustomerId = CustomerId
                    Dim Remarks = billHdr.Rows(0)("psih_invoice_billremarks")
                    barbtnbilltype.Caption = "Bill Type : " & billHdr.Rows(0)("psih_invoice_billtype")
                End If
                If billDtl.Rows.Count > 0 Then
                    _SnoCount = 0
                    GridDataTble_Insert.Rows.Clear()
                    GridDataTble_Insert.NewRow()
                    GridDataTble_Insert.BeginInit()
                    For Each rData In billDtl.Rows
                        _SnoCount = _SnoCount + 1

                        ' Declare variables for data mapping
                        Dim barcode As Object = If(rData.Table.Columns.Contains("psid_invoice_barcode"), rData("psid_invoice_barcode"), 0)
                        Dim itemCode As Object = rData("psid_invoice_procode")
                        Dim itemName As String = Trim(rData("psid_invoice_description"))
                        Dim serialNo As String = If(rData.Table.Columns.Contains("psid_invoice_serialno"), rData("psid_invoice_serialno"), "0")
                        Dim uom As String = If(rData.Table.Columns.Contains("psid_invoice_uom"), rData("psid_invoice_uom"), "PCS")
                        Dim rate As Object = rData("psid_invoice_rate")
                        Dim qty As Object = rData("psid_invoice_proqty")
                        Dim amount As Object = rData("psid_invoice_amt")
                        Dim itemDiscPer As Object = rData("psid_invoice_itemdisp")
                        Dim itemDiscAmt As Object = rData("psid_invoice_itemdisamt")
                        Dim billDiscPer As Object = rData("psid_invoice_billdisp")
                        Dim billDiscAmt As Object = rData("psid_invoice_billdisamt")
                        Dim totalDiscPer As Object = rData("psid_invoice_totdper")
                        Dim totalDiscAmt As Object = rData("psid_invoice_totdamt")
                        Dim grossAmount As Object = rData("psid_invoice_gross")
                        Dim taxValue As Object = rData("psid_invoice_taxvalue")
                        Dim taxAmount As Object = rData("psid_invoice_taxamt")
                        Dim netAmount As Decimal = _RoundOff(rData("psid_invoice_netamt"))
                        Dim remarks As String = If(rData.Table.Columns.Contains("psid_invoice_remarks"), rData("psid_invoice_remarks"), "Remarks")
                        Dim batchNo As Object = If(rData.Table.Columns.Contains("psid_invoice_batchno"), rData("psid_invoice_batchno"), 0)
                        Dim salesPersonId As Integer = If(rData.Table.Columns.Contains("psid_invoice_salespersonid"), Convert.ToInt32(rData("psid_invoice_salespersonid")), 1)
                        Dim salesManPer As Object = If(rData.Table.Columns.Contains("psid_invoice_salesmanper"), rData("psid_invoice_salesmanper"), 0)
                        Dim deleteFlag As Object = If(rData.Table.Columns.Contains("psid_invoice_delete"), rData("psid_invoice_delete"), 1)
                        Dim itemLock As Object = If(rData.Table.Columns.Contains("psid_invoice_itemlock"), rData("psid_invoice_itemlock"), 2)
                        Dim psid As Object = If(rData.Table.Columns.Contains("psid_invoice_id"), rData("psid_invoice_id"), 0)

                        ' Get salesperson name from _JsonData.SalesManCommissionTable using LINQ
                        Dim salesPersonName As String = "Default"
                        Try
                            If _JsonData.SalesManCommissionTable.Rows.Count = 0 Then
                                getSalesManCommissionInfo()
                            End If

                            If _JsonData.SalesManCommissionTable.Rows.Count > 0 Then
                                Dim salesPersonRow = _JsonData.SalesManCommissionTable.AsEnumerable().
                                                   FirstOrDefault(Function(row) Convert.ToInt32(row("EmpId")) = salesPersonId)

                                If salesPersonRow IsNot Nothing Then
                                    salesPersonName = salesPersonRow("SalesManName").ToString()
                                End If
                            End If
                        Catch ex As Exception
                            ' If error getting salesperson name, use default or from database if available
                            If rData.Table.Columns.Contains("psid_invoice_salesperson") AndAlso rData("psid_invoice_salesperson") IsNot DBNull.Value Then
                                salesPersonName = rData("psid_invoice_salesperson").ToString()
                            End If
                        End Try

                        ' Map database fields to DataTable columns with PSID support
                        ' Column order: SNO, BARCODE, ITEMCODE, ITEMNAME, SERIALNO, UOM, RATE, QTY, TAMOUNT,
                        '              ITEM_DPER, ITEM_DAMT, BILL_DPER, BILL_DAMT, TOTAL_DPER, TOTAL_DAMT,
                        '              GAMOUNT, TAXVALUE, TAXAMT, NETAMT, ITEMREMARS, BATCHNO, SALESPERSONID, SALESPERSON, SALESMANPER, DELETE, ITEMLOCK, PSID
                        GridDataTble_Insert.Rows.Add(_SnoCount, barcode, itemCode, itemName, serialNo, uom, rate, qty, amount, _
                                                    itemDiscPer, itemDiscAmt, billDiscPer, billDiscAmt, totalDiscPer, totalDiscAmt, _
                                                    grossAmount, taxValue, taxAmount, netAmount, remarks, batchNo, _
                                                    salesPersonId, salesPersonName, salesManPer, deleteFlag, itemLock, psid)
                    Next
                    GridDataTble_Insert.AcceptChanges()
                    GridDataTble_Insert.EndInit()
                    GridControlSalesData.DataSource = GridDataTble_Insert
                    GridViewPOS.MoveNext()

                    ' GridDataTble_Insert.WriteXml(M_Details._appPath & "Layout\SaleRecentData.xml", True, System.Data.XmlWriteMode.WriteSchema)
                    'txtnotes.Text = "-"
                    If SalesGrandtotal("ER") = False Then

                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub barbtnbilledit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnbilledit.ItemClick
        Try
            frmKeyPassIIIMaster.ShowDialog()
            If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
                If modeOfSale = "View" Then
                    modeOfSale = "Edit"
                    barbtnstatus.Caption = "Sales Mode : " & modeOfSale
                ElseIf modeOfSale = "Quote" Then
                    modeOfSale = "New"
                    barbtnstatus.Caption = "Sales Mode : " & modeOfSale
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show("New Mode Cant Be Save Bill", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show("You Dont Have Rights Edit Bill", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "Print/Home/Print"
    Private Sub barbtnhome_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnhome.ItemClick
        Try
            Me.Hide()
            PosLogin.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnlastprint_Click(sender As Object, e As EventArgs) Handles btnlastprint.ItemClick
        Try
            Dim _receDs As New DataSet
            If (GetSalesByBillLocal(barstatuslastbillno.Caption, _receDs, "P")) = True Then
                If (_receDs.Tables(0).Rows.Count > 0) Then
                    _receDs.WriteXml(M_Details._appPath & "\Reports\Sales.xml", Data.XmlWriteMode.WriteSchema)
                End If

                If clsBillPrint.BillPrintMin(_receDs, Errstr, "SalesMinPrint.repx") = True Then
                    _CashDraw._paperCut(True)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub




    Private Sub barbtnprintprofiledesign_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnprintprofiledesign.ItemClick
        Try
            frmPrintProfile.ShowDialog()

        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "ShiftClose/Staff/CashDrawer"
    Private Sub barbtncounterclose_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtncounterclose.ItemClick
        Try
            If CheckSalesBeforeCounterClose() = False Then
                Return
            End If
            frmKeyPassIIIMaster.ShowDialog()
            If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
                frmShiftcloseII.ShowDialog()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnstaffadvance_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnstaffadvance.ItemClick
        Try
            frmKeyPassIIIMaster.ShowDialog()
            If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
                frmPayouts.Show()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnrefreshdata_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnrefreshdata.ItemClick
        Try
            If _ReadDefaultLocalData() = False Then
                Return
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtncashdraweropen_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtncashdraweropen.ItemClick
        Try
            frmKeyPassUser.ShowDialog()
            If frmKeyPassUser.DialogResult = Windows.Forms.DialogResult.OK Then
                _CashDraw.OpenCashdrawer(True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnPrintShiftClose_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnPrintShiftClose.ItemClick
        Try
            Dim printcmd As New PrintCommand
            Dim input As Integer = InputBox("Enter Shift No")
            If input > 0 Then
                printcmd._printShiftClose(input, "S", Date.Now)
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class
