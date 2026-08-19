Imports System.Net
Imports Newtonsoft.Json.Linq
Imports System.Drawing
Imports Newtonsoft.Json
Imports System.IO
Imports System.Data.SqlClient
Imports PosRetailWebBilling.clssalesProperty

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
    Dim modeBillHold As String = "New"
    ' Customer Selection Variables
    Private selectedCustomerId As Integer = 0
    Private selectedCustomerName As String = String.Empty
    Private selectedCustomerPhone As String = String.Empty
    Private customerDisplayTable As DataTable
    Private _CashDraw As New RawPrinter
    Dim salesHelper As New SalesDBHelper(M_Details._Conn)
    Dim billHoldHelper As New BillHoldDBHelper(M_Details._Conn)
    Private _isSelectionMode As Boolean = False
    Dim stpole1 As String = M_Details._shopName
    Dim NetAmountGlobal As Decimal = 0.0
    Private _recallHoldBill As Boolean = False
    Private _pendingVoucherId As Integer = 0
    Private _pendingVoucherNo As Integer = 0
   
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
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
            LoadMainMenu()
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

            If CustomerDisplaySettings.Startup = 0 Then
                barchkcustomerpole.Checked = True
            Else
                barchkcustomerpole.Checked = False
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
            If RegistrationDetails._paymentPopupActive = False Then
                btnpayment.Text = "Send Order"
            End If
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

#End Region
#Region "LoadMenu"
    'Private Sub mainMenu()
    '    Try
    '        Try

    '            FlowLayoutPanelMain.Controls.Clear()
    '            ' Set FlowLayoutPanel properties for perfect layout

    '            FlowLayoutPanelMain.WrapContents = True
    '            FlowLayoutPanelMain.AutoScroll = True
    '            ' Add dynamic SimpleButtons for each category with perfect fit styling

    '            ' For initial load, use ButtonStyleWH.SUBLOAD as default index
    '            Dim firstMainGroupId As Integer = Convert.ToInt32(ButtonStyleWH.SUBLOAD)
    '            If _JsonData.MainGroupPolicyTable.Rows.Count > 0 Then
    '                For Each row As DataRow In _JsonData.MainGroupPolicyTable.Rows
    '                    Dim btn As New DevExpress.XtraEditors.SimpleButton()

    '                    ' Button text and data
    '                    btn.Text = row("MainName").ToString()
    '                    btn.Tag = row("MainId")

    '                    ' Button styling and dimensions for perfect fit with even padding
    '                    btn.Size = New Size(ButtonStyleWH.MAINW, ButtonStyleWH.MAINH)


    '                    ' DevExpress SimpleButton specific properties with enhanced styling
    '                    btn.Appearance.BackColor = GetSafeColor(row, "Color", Color.FromArgb(52, 152, 219)) ' Blue default for MainGroup
    '                    btn.Appearance.ForeColor = Color.Black
    '                    btn.Appearance.Font = New Font("Segoe UI", 14, FontStyle.Bold) ' Consistent larger font
    '                    btn.Appearance.Options.UseBackColor = True
    '                    btn.Appearance.Options.UseForeColor = True
    '                    btn.Appearance.Options.UseFont = True
    '                    btn.Appearance.Options.UseTextOptions = True
    '                    btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                    btn.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    '                    btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '                    btn.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter ' Handle long text gracefully


    '                    btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    '                    btn.LookAndFeel.UseDefaultLookAndFeel = False

    '                    ' Add border for better visual separation
    '                    btn.Appearance.BorderColor = Color.FromArgb(200, 200, 200)
    '                    btn.Appearance.Options.UseBorderColor = True

    '                    ' Event handler
    '                    AddHandler btn.Click, AddressOf MainGroupButton_Click

    '                    ' Add to panel
    '                    FlowLayoutPanelMain.Controls.Add(btn)

    '                Next

    '                ' Force layout update to ensure proper sizing
    '                FlowLayoutPanelMain.PerformLayout()

    '                ' Load submenu for the initial default main group ID
    '                If firstMainGroupId > 0 Then
    '                    subMenu(firstMainGroupId, True) ' True indicates initial load
    '                End If

    '            End If

    '        Catch ex As Exception
    '            ' Handle error silently or log if needed
    '        End Try
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub subMenu(ByRef DefaultMainId As Integer, Optional ByVal isInitialLoad As Boolean = False)
    '    Try

    '        FlowLayoutPanelSubMenu.Controls.Clear()

    '        Dim selectedMainId As Integer = DefaultMainId ' Store selected main group ID
    '        Dim firstCategoryId As Integer = 0 ' Will store the first category ID for product loading

    '        If _JsonData.CategoryTable.Rows.Count > 0 Then
    '            Dim filteredCate = From item In _JsonData.CategoryTable.AsEnumerable() _
    '                              Where IsNumeric(item("MainId")) AndAlso Convert.ToInt32(item("MainId")) = selectedMainId _
    '                              Select item

    '            If filteredCate.Any() Then
    '                Dim filteredTable As DataTable = filteredCate.CopyToDataTable()

    '                ' Determine which category to load for products
    '                If isInitialLoad Then
    '                    ' For initial load, use ButtonStyleWH.ITEMLOAD as index
    '                    Dim itemLoadIndex As Integer = Convert.ToInt32(ButtonStyleWH.ITEMLOAD)
    '                    If itemLoadIndex < filteredTable.Rows.Count Then
    '                        firstCategoryId = Convert.ToInt32(filteredTable.Rows(itemLoadIndex)("CateID"))
    '                    Else
    '                        ' If index is out of range, use first category
    '                        firstCategoryId = Convert.ToInt32(filteredTable.Rows(0)("CateID"))
    '                    End If
    '                Else
    '                    ' For user clicks, always use first category (index 0)
    '                    firstCategoryId = Convert.ToInt32(filteredTable.Rows(0)("CateID"))
    '                End If
    '                For Each row As DataRow In filteredTable.Rows
    '                    Dim btn As New DevExpress.XtraEditors.SimpleButton()

    '                    ' Button text and data
    '                    btn.Text = row("CateName").ToString()
    '                    btn.Tag = row("CateID")


    '                    ' Button styling and dimensions for perfect fit with even padding
    '                    btn.Size = New Size(ButtonStyleWH.SUBW, ButtonStyleWH.SUBH)

    '                    ' DevExpress SimpleButton specific properties with enhanced styling
    '                    btn.Appearance.BackColor = GetSafeColor(row, "Color", Color.FromArgb(46, 204, 113)) ' Green default for SubMenu
    '                    btn.Appearance.ForeColor = Color.Black
    '                    btn.Appearance.Font = New Font("Segoe UI", 14, FontStyle.Bold) ' Consistent larger font
    '                    btn.Appearance.Options.UseBackColor = True
    '                    btn.Appearance.Options.UseForeColor = True
    '                    btn.Appearance.Options.UseFont = True
    '                    btn.Appearance.Options.UseTextOptions = True
    '                    btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                    btn.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    '                    btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '                    btn.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter ' Handle long text gracefully


    '                    btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    '                    btn.LookAndFeel.UseDefaultLookAndFeel = False

    '                    ' Add border for better visual separation
    '                    btn.Appearance.BorderColor = Color.FromArgb(200, 200, 200)
    '                    btn.Appearance.Options.UseBorderColor = True

    '                    ' Event handler
    '                    AddHandler btn.Click, AddressOf CategoryButton_Click

    '                    ' Add to panel
    '                    FlowLayoutPanelSubMenu.Controls.Add(btn)

    '                    ' Increment color index for next button

    '                Next
    '            End If

    '            ' Force layout update to ensure proper sizing
    '            FlowLayoutPanelSubMenu.PerformLayout()

    '            LoadProductMenu(firstCategoryId)

    '        End If
    '    Catch ex As Exception
    '        ' Handle error silently or log if needed
    '    End Try
    'End Sub
    'Private Sub LoadProductMenu(ByRef DefaultCateId As Integer)
    '    Try
    '        Dim Cateid As Integer = 0
    '        Cateid = DefaultCateId

    '        ' Clear existing product buttons
    '        FlowLayoutPanelProduct.Controls.Clear()

    '        If _JsonData.ItemTouchMasterTable.Rows.Count > 0 Then
    '            ' Filter items by category ID and update the item grid
    '            Dim filteredItems = From item In _JsonData.ItemTouchMasterTable.AsEnumerable() _
    '                               Where IsNumeric(item("CateId")) AndAlso Convert.ToInt32(item("CateId")) = Cateid _
    '                               Select item

    '            If filteredItems.Any() Then
    '                ' Create a new DataTable with filtered items
    '                Dim filteredTable As DataTable = filteredItems.CopyToDataTable()
    '                For Each row As DataRow In filteredTable.Rows
    '                    Dim btn As New DevExpress.XtraEditors.SimpleButton()

    '                    ' Button content - Item name and price
    '                    Dim itemName As String = If(row("ItemName") IsNot Nothing, row("ItemName").ToString(), "Unknown Item")
    '                    Dim sellPrice As Decimal = 0
    '                    If IsNumeric(row("SellPrice")) Then
    '                        sellPrice = Convert.ToDecimal(row("SellPrice"))
    '                    End If
    '                    btn.Text = itemName & Environment.NewLine & sellPrice.ToString("C2")

    '                    ' Store ItemCode in Tag with safety check
    '                    If IsNumeric(row("Id")) Then
    '                        btn.Tag = Convert.ToInt32(row("Id"))
    '                    Else
    '                        btn.Tag = 0 ' Default value if conversion fails
    '                    End If

    '                    ' Enhanced button styling
    '                    btn.Size = New Size(ButtonStyleWH.ITEMW, ButtonStyleWH.ITEMH)

    '                    ' Safe color handling using helper function
    '                    btn.Appearance.BackColor = GetSafeColor(row, "Color", Color.FromArgb(70, 130, 180)) ' Steel Blue default for Products
    '                    btn.Appearance.ForeColor = Color.Black
    '                    btn.Appearance.Font = New Font("Segoe UI", 14, FontStyle.Bold)
    '                    btn.Appearance.Options.UseBackColor = True
    '                    btn.Appearance.Options.UseForeColor = True
    '                    btn.Appearance.Options.UseFont = True
    '                    btn.Appearance.Options.UseTextOptions = True
    '                    btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                    btn.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    '                    btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap


    '                    btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    '                    btn.LookAndFeel.UseDefaultLookAndFeel = False

    '                    ' Border styling
    '                    btn.Appearance.BorderColor = Color.FromArgb(180, 180, 180)
    '                    btn.Appearance.Options.UseBorderColor = True

    '                    ' Event handler
    '                    AddHandler btn.Click, AddressOf ItemButton_Click
    '                    FlowLayoutPanelProduct.Controls.Add(btn)
    '                Next
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    ' ============ Load Main Menu ============
    Private Sub LoadMainMenu()
        Try
            PanelMainMenu.Controls.Clear()

            Dim btnWidth As Integer = ButtonStyleWH.MAINW
            Dim btnHeight As Integer = ButtonStyleWH.MAINH
            Dim spacing As Integer = ButtonStyleWH.ITEMSPACING
            Dim marginLeft As Integer = ButtonStyleWH.ITEMMARGINLEFT
            Dim marginTop As Integer = ButtonStyleWH.ITEMMARGINRIGHT
            Dim cols As Integer = ButtonStyleWH.MAINCOL

            For i As Integer = 0 To _JsonData.MainGroupPolicyTable.Rows.Count - 1
                Dim row As Integer = i \ cols
                Dim col As Integer = i Mod cols

                ' Get current row data
                Dim currentRow As DataRow = _JsonData.MainGroupPolicyTable.Rows(i)

                ' Count how many buttons in this row
                Dim countInRow As Integer =
                    If(i + cols < _JsonData.MainGroupPolicyTable.Rows.Count, cols, _JsonData.MainGroupPolicyTable.Rows.Count - row * cols)

                ' Total width of this row
                Dim rowWidth As Integer = (countInRow * btnWidth) + ((countInRow - 1) * spacing)

                ' Center horizontally
                Dim startX As Integer = Math.Max(0, (PanelMainMenu.Width - rowWidth) \ 2)

                ' Create button
                Dim btn As New DevExpress.XtraEditors.SimpleButton()
                btn.Text = currentRow("MainName").ToString()
                btn.Tag = currentRow("MainId").ToString()
                btn.Size = New Size(btnWidth, btnHeight)
                btn.Location = New Point(marginLeft + col * (btnWidth + spacing),
                             marginTop + row * (btnHeight + spacing))

                ' Get properties from API data with defaults
                Dim fontSize As Single = GetSafeValue(currentRow, "font_size", 14.0F)
                Dim fontName As String = GetSafeValue(currentRow, "font_name", "Segoe UI")
                Dim fontStyleString As String = GetSafeValue(currentRow, "font_style", "Bold")
                Dim textColor As Color = ParseARGBColor(GetSafeValue(currentRow, "text_color", ""), Color.White)
                Dim backColor As Color = ParseARGBColor(GetSafeValue(currentRow, "back_color", ""), Color.FromArgb(52, 152, 219))

                ' Convert font style string to FontStyle enum
                Dim fontStyleEnum As FontStyle = FontStyle.Regular
                Dim fontStyleLower As String = fontStyleString.ToLower()
                If fontStyleLower = "bold" Then
                    fontStyleEnum = FontStyle.Bold
                ElseIf fontStyleLower = "italic" Then
                    fontStyleEnum = FontStyle.Italic
                ElseIf fontStyleLower = "underline" Then
                    fontStyleEnum = FontStyle.Underline
                ElseIf fontStyleLower = "strikeout" Then
                    fontStyleEnum = FontStyle.Strikeout
                Else
                    fontStyleEnum = FontStyle.Regular
                End If

                ' Apply properties to button
                btn.Appearance.Font = New Font(fontName, fontSize, fontStyleEnum)
                btn.Appearance.ForeColor = textColor
                btn.Appearance.BackColor = backColor
                btn.Appearance.Options.UseBackColor = True
                btn.Appearance.Options.UseForeColor = True
                btn.Appearance.Options.UseFont = True
                btn.Appearance.Options.UseTextOptions = True
                btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                btn.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                btn.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter

                btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
                btn.LookAndFeel.UseDefaultLookAndFeel = False

                ' Add border for better visual separation
                btn.Appearance.BorderColor = Color.FromArgb(200, 200, 200)
                btn.Appearance.Options.UseBorderColor = True
                ' Try to load saved properties from PHP
                'Try
                '    LoadButtonPropertiesFromPHP(mains(i).MainId, "Main")
                '    If selectedButtonProperties IsNot Nothing Then
                '        ApplyPropertiesFromObjectToButton(btn, selectedButtonProperties)
                '    End If
                'Catch
                '    ' Use default if loading fails
                'End Try

                '' Add context menu for properties
                'AddContextMenuToButton(btn)

                AddHandler btn.Click, AddressOf MainMenu_Click
                PanelMainMenu.Controls.Add(btn)
            Next

            PanelMainMenu.AutoScroll = True
            PanelMainMenu.AllowTouchScroll = True

            ' Auto-load first MainId
            If _JsonData.MainGroupPolicyTable.Rows.Count > 0 Then
                LoadSubMenu(Convert.ToInt32(_JsonData.MainGroupPolicyTable.Rows(0)("MainId")))
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading Main Menu: " & ex.Message)
        End Try
    End Sub


    ' ============ Load Sub Menu ============
    Private Sub LoadSubMenu(mainId As Integer)
        Try
            PanelSubMenu.Controls.Clear()

            Dim btnWidth As Integer = ButtonStyleWH.SUBW
            Dim btnHeight As Integer = ButtonStyleWH.SUBH
            Dim cols As Integer = ButtonStyleWH.SUBMENUCOL
            Dim spacing As Integer = ButtonStyleWH.ITEMSPACING
            Dim marginLeft As Integer = ButtonStyleWH.ITEMMARGINLEFT
            Dim marginTop As Integer = ButtonStyleWH.ITEMMARGINRIGHT
            ' Group by CateId, CateName under selected MainId
            Dim subs = _JsonData.CategoryTable.AsEnumerable().
                Where(Function(r) Convert.ToInt32(r("MainId")) = mainId).
                GroupBy(Function(r) New With {
                    Key .CateId = Convert.ToInt32(r("CateId")),
                    Key .CateName = r("CateName").ToString(),
                    Key .Position = Convert.ToInt32(r("Position"))
                }).
                Select(Function(g) g.Key).
                OrderBy(Function(x) x.Position).ToList()

            For i As Integer = 0 To subs.Count - 1
                Dim row As Integer = i \ cols
                Dim col As Integer = i Mod cols

                Dim btn As New DevExpress.XtraEditors.SimpleButton()
                btn.Text = subs(i).CateName
                btn.Tag = subs(i).CateId
                btn.Size = New Size(btnWidth, btnHeight)
                btn.Location = New Point(marginLeft + col * (btnWidth + spacing),
                              marginTop + row * (btnHeight + spacing))

                ' Try to find matching row in CategoryTable for this CateId
                Dim matchingRow As DataRow = Nothing
                Try
                    Dim currentCateId As Integer = subs(i).CateId
                    matchingRow = _JsonData.CategoryTable.AsEnumerable().
                        Where(Function(r) Convert.ToInt32(r("CateId")) = currentCateId).
                        FirstOrDefault()
                Catch
                    ' Continue with defaults if no matching row found
                End Try

                ' Get properties from API data with defaults for Sub menu
                Dim fontSize As Single = 9.0F
                Dim fontName As String = "Segoe UI"
                Dim fontStyleString As String = "Regular"
                Dim textColor As Color = Color.Black
                Dim backColor As Color = Color.LightSteelBlue

                ' If we found a matching row, try to get custom properties
                If matchingRow IsNot Nothing Then
                    fontSize = GetSafeValue(matchingRow, "font_size", 9.0F)
                    fontName = GetSafeValue(matchingRow, "font_name", "Segoe UI")
                    fontStyleString = GetSafeValue(matchingRow, "font_style", "Regular")
                    textColor = ParseARGBColor(GetSafeValue(matchingRow, "text_color", ""), Color.Black)
                    backColor = ParseARGBColor(GetSafeValue(matchingRow, "back_color", ""), Color.LightSteelBlue)
                End If

                ' Convert font style string to FontStyle enum
                Dim fontStyleEnum As FontStyle = FontStyle.Regular
                Dim fontStyleLower As String = fontStyleString.ToLower()
                If fontStyleLower = "bold" Then
                    fontStyleEnum = FontStyle.Bold
                ElseIf fontStyleLower = "italic" Then
                    fontStyleEnum = FontStyle.Italic
                ElseIf fontStyleLower = "underline" Then
                    fontStyleEnum = FontStyle.Underline
                ElseIf fontStyleLower = "strikeout" Then
                    fontStyleEnum = FontStyle.Strikeout
                Else
                    fontStyleEnum = FontStyle.Regular
                End If

                ' Enable DevExpress appearance options
                btn.Appearance.Options.UseBackColor = True
                btn.Appearance.Options.UseForeColor = True
                btn.Appearance.Options.UseFont = True
                btn.Appearance.Options.UseTextOptions = True
                btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                btn.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                btn.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter

                ' Apply properties to button
                btn.Appearance.Font = New Font(fontName, fontSize, fontStyleEnum)
                btn.Appearance.ForeColor = textColor
                btn.Appearance.BackColor = backColor

                btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
                btn.LookAndFeel.UseDefaultLookAndFeel = False

                ' Add border for better visual separation
                btn.Appearance.BorderColor = Color.FromArgb(200, 200, 200)
                btn.Appearance.Options.UseBorderColor = True

                '' Try to load saved properties from PHP
                'Try
                '    LoadButtonPropertiesFromPHP(subs(i).CateId, "Sub")
                '    If selectedButtonProperties IsNot Nothing Then
                '        ApplyPropertiesFromObjectToButton(btn, selectedButtonProperties)
                '    End If
                'Catch
                '    ' Use default if loading fails
                'End Try



                AddHandler btn.Click, AddressOf SubMenu_Click
                PanelSubMenu.Controls.Add(btn)
            Next

            PanelSubMenu.AutoScroll = True
            PanelSubMenu.AllowTouchScroll = True

            ' ✅ Auto-load first CateId
            If subs.Count > 0 Then
                LoadItemMenu(subs(0).CateId)
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading Sub Menu: " & ex.Message)
        End Try
    End Sub

    ' ============ Load Item Menu ============
    Private Sub LoadItemMenu(cateId As Integer)
        Try
            PanelItemMenu.Controls.Clear()

            Dim btnWidth As Integer = ButtonStyleWH.ITEMW
            Dim btnHeight As Integer = ButtonStyleWH.ITEMH
            Dim cols As Integer = ButtonStyleWH.ITEMMENUCOL
            Dim spacing As Integer = ButtonStyleWH.ITEMSPACING
            Dim marginLeft As Integer = ButtonStyleWH.ITEMMARGINLEFT
            Dim marginTop As Integer = ButtonStyleWH.ITEMMARGINRIGHT


            ' Items filtered by CateId
            Dim items = _JsonData.ItemTouchMasterTable.AsEnumerable().
                Where(Function(r) Convert.ToInt32(r("CateId")) = cateId).
                Select(Function(r) New With {
                    .Id = Convert.ToInt32(r("Id")),
                    .ItemName = r("ItemName").ToString(),
                    .Price = r("SellPrice").ToString,
                    .Position = Convert.ToInt32(r("Position"))
                }).OrderBy(Function(x) x.Position).ToList() ' Ascending order by Position

            For i As Integer = 0 To items.Count - 1
                Dim row As Integer = i \ cols
                Dim col As Integer = i Mod cols

                Dim btn As New DevExpress.XtraEditors.SimpleButton()
                btn.Text = items(i).ItemName & vbNewLine & items(i).Price
                btn.Tag = items(i).Id
                btn.Size = New Size(btnWidth, btnHeight)
                btn.Location = New Point(marginLeft + col * (btnWidth + spacing),
                              marginTop + row * (btnHeight + spacing))

                ' Try to find matching row in ItemTouchMasterTable for this Id
                Dim matchingRow As DataRow = Nothing
                Try
                    Dim currentItemId As Integer = items(i).Id
                    matchingRow = _JsonData.ItemTouchMasterTable.AsEnumerable().
                        Where(Function(r) Convert.ToInt32(r("item_id")) = currentItemId).
                        FirstOrDefault()
                Catch
                    ' Continue with defaults if no matching row found
                End Try

                ' Get properties from API data with defaults for Item menu
                Dim fontSize As Single = 8.0F
                Dim fontName As String = "Segoe UI"
                Dim fontStyleString As String = "Regular"
                Dim textColor As Color = Color.DarkBlue
                Dim backColor As Color = Color.Beige

                ' If we found a matching row, try to get custom properties
                If matchingRow IsNot Nothing Then
                    fontSize = GetSafeValue(matchingRow, "font_size", 8.0F)
                    fontName = GetSafeValue(matchingRow, "font_name", "Segoe UI")
                    fontStyleString = GetSafeValue(matchingRow, "font_style", "Regular")
                    textColor = ParseARGBColor(GetSafeValue(matchingRow, "text_color", ""), Color.DarkBlue)
                    backColor = ParseARGBColor(GetSafeValue(matchingRow, "back_color", ""), Color.Beige)
                End If

                ' Convert font style string to FontStyle enum
                Dim fontStyleEnum As FontStyle = FontStyle.Regular
                Dim fontStyleLower As String = fontStyleString.ToLower()
                If fontStyleLower = "bold" Then
                    fontStyleEnum = FontStyle.Bold
                ElseIf fontStyleLower = "italic" Then
                    fontStyleEnum = FontStyle.Italic
                ElseIf fontStyleLower = "underline" Then
                    fontStyleEnum = FontStyle.Underline
                ElseIf fontStyleLower = "strikeout" Then
                    fontStyleEnum = FontStyle.Strikeout
                Else
                    fontStyleEnum = FontStyle.Regular
                End If

                ' Enable DevExpress appearance options
                btn.Appearance.Options.UseBackColor = True
                btn.Appearance.Options.UseForeColor = True
                btn.Appearance.Options.UseFont = True
                btn.Appearance.Options.UseTextOptions = True
                btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                btn.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                btn.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter

                ' Apply properties to button
                btn.Appearance.Font = New Font(fontName, fontSize, fontStyleEnum)
                btn.Appearance.ForeColor = textColor
                btn.Appearance.BackColor = backColor

                btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
                btn.LookAndFeel.UseDefaultLookAndFeel = False

                ' Add border for better visual separation
                btn.Appearance.BorderColor = Color.FromArgb(200, 200, 200)
                btn.Appearance.Options.UseBorderColor = True

                '' Try to load saved properties from PHP
                'Try
                '    LoadButtonPropertiesFromPHP(items(i).Id, "Item")
                '    If selectedButtonProperties IsNot Nothing Then
                '        ApplyPropertiesFromObjectToButton(btn, selectedButtonProperties)
                '    End If
                'Catch
                '    ' Use default if loading fails
                'End Try


                AddHandler btn.Click, AddressOf ItemMenu_Click
                PanelItemMenu.Controls.Add(btn)
            Next

            PanelItemMenu.AutoScroll = True
            PanelItemMenu.AllowTouchScroll = True
            '' ✅ Auto-select first item
            'If items.Count > 0 Then
            '    ItemMenu_Click(PanelItemMenu.Controls(0), EventArgs.Empty)
            'End If

        Catch ex As Exception
            MessageBox.Show("Error loading Item Menu: " & ex.Message)
        End Try
    End Sub

    Private Sub MainMenu_Click(sender As Object, e As EventArgs)
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
                LoadSubMenu(selectedMainGroupId) ' False indicates user click, not initial load
            End If

        Catch ex As Exception
            ' Handle error silently or log if needed
            DevExpress.XtraEditors.XtraMessageBox.Show("Error loading category: " & ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub SubMenu_Click(sender As Object, e As EventArgs)
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
                LoadItemMenu(selectedCategoryId)
            End If

        Catch ex As Exception
            ' Handle error silently or log if needed
            DevExpress.XtraEditors.XtraMessageBox.Show("Error loading category: " & ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ItemMenu_Click(sender As Object, e As EventArgs)
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
    ' Helper method to parse ARGB color format: Argb(255,255,255,255)
    Private Function ParseARGBColor(argbString As String) As Color
        Try
            ' Remove "Argb(" and ")" and split by comma
            Dim cleanString As String = argbString.Replace("Argb(", "").Replace(")", "").Trim()
            Dim values() As String = cleanString.Split(","c)

            If values.Length = 4 Then
                Dim a As Integer = Convert.ToInt32(values(0).Trim())
                Dim r As Integer = Convert.ToInt32(values(1).Trim())
                Dim g As Integer = Convert.ToInt32(values(2).Trim())
                Dim b As Integer = Convert.ToInt32(values(3).Trim())

                Return Color.FromArgb(a, r, g, b)
            End If

            ' Return default color if parsing fails
            Return Color.Black
        Catch ex As Exception
            ' Return default color if parsing fails
            Return Color.Black
        End Try
    End Function

    ' Helper function to safely get values from DataRow with default fallback
    Private Function GetSafeValue(Of T)(row As DataRow, columnName As String, defaultValue As T) As T
        Try
            If row.Table.Columns.Contains(columnName) AndAlso Not IsDBNull(row(columnName)) Then
                Dim value As Object = row(columnName)
                If value IsNot Nothing Then
                    Return DirectCast(Convert.ChangeType(value, GetType(T)), T)
                End If
            End If
            Return defaultValue
        Catch ex As Exception
            Return defaultValue
        End Try
    End Function

    ' Helper function to parse ARGB color with fallback
    Private Function ParseArgbColor(argbString As String, defaultColor As Color) As Color
        If String.IsNullOrEmpty(argbString) Then
            Return defaultColor
        End If

        Dim result As Color = ParseArgbColor(argbString)
        If result = Color.Black AndAlso argbString <> "Argb(255,0,0,0)" Then
            Return defaultColor
        End If
        Return result
    End Function
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
                    'lock when recal mode only allowed to admin to changes
                    If RegistrationDetails._serverClient = "ORDER" Then
                        barselectsalesman_ItemClick(Nothing, Nothing)
                    ElseIf _recallHoldBill = False AndAlso RegistrationDetails._serverClient = "SERVER" Then
                        barselectsalesman_ItemClick(Nothing, Nothing)
                    End If
                End If
            End If

            If GridViewPOS.FocusedColumn IsNot Nothing AndAlso GridViewPOS.FocusedColumn.FieldName = "NETAMT" Then
                If _globalSetting.SelectMultiplePriceActive = False Then
                    MessageBox.Show("You Don't Have Rights To opening multiple price selection", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                    'ElseIf _companyInfo.UserRoleId = 1 OrElse _companyInfo.UserRoleId = 2 Then
                    '    ShowMultiplePriceSelection()
                    'Else
                    '    ShowMultiplePriceSelection()
                    'End If
                ElseIf RegistrationDetails._serverClient = "ORDER" Then
                    ShowMultiplePriceSelection()
                ElseIf _recallHoldBill = False AndAlso RegistrationDetails._serverClient = "SERVER" Then
                    ShowMultiplePriceSelection()
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
                    frmKeyPassIIIMaster.ShowDialog()
                    If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
                        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete '" & itemName & "'?", _
                                                                  "Confirm Delete", _
                                                                      MessageBoxButtons.YesNo, _
                                                                      MessageBoxIcon.Question)
                        If result = DialogResult.Yes Then
                            DeleteSelectedRow(focusedRowHandle)
                        End If
                    End If
                    'MessageBox.Show("You Can't Delete This Item ,No Rights '" & itemName & "'?", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'Return

                    'If _globalSetting.ItemDeleteActive = True Then

                    '    Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete '" & itemName & "'?", _
                    '                                               "Confirm Delete", _
                    '                                               MessageBoxButtons.YesNo, _
                    '                                               MessageBoxIcon.Question)
                    '    If result = DialogResult.Yes Then
                    '        DeleteSelectedRow(focusedRowHandle)
                    '    End If
                    'Else
                    '    If _companyInfo.UserRoleId = 1 OrElse _companyInfo.UserRoleId = 2 Then

                    '        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete '" & itemName & "'?", _
                    '                                                   "Confirm Delete", _
                    '                                                   MessageBoxButtons.YesNo, _
                    '                                                   MessageBoxIcon.Question)

                    '        If result = DialogResult.Yes Then
                    '            DeleteSelectedRow(focusedRowHandle)
                    '        End If
                    '    End If
                    'End If
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
                        StockUpdateOffline(itemCode, qty)
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
                SalesGrandtotal(False)

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
            If modeOfSale = "View" Then
                ErrorMsg = "You can't able to add new item when its View Mode"
                Return False
            End If
            Dim ReceivedProCode As String = _productCode
            If ReceivedProCode Is Nothing Then
                Return False
            End If
            Dim dtrows As System.Data.EnumerableRowCollection(Of DataRow) = Nothing

            If _Mode = "BarCode" Then
                dtrows = From dtrow As DataRow In _JsonData.ItemTouchMasterTable Where String.Equals(dtrow("BARCODE"), ReceivedProCode, StringComparison.CurrentCultureIgnoreCase)
            ElseIf _Mode = "ItemCode" Then
                dtrows = From dtrow As DataRow In _JsonData.ItemTouchMasterTable Where String.Equals(dtrow("Id"), ReceivedProCode, StringComparison.CurrentCultureIgnoreCase)
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
                Dim _businessType As String = ""
                Dim _allownegativestock As Boolean = True
                Dim _dsMaterial As New DataTable
                _dsMaterial = dtrows.CopyToDataTable
                If _dsMaterial.Rows.Count > 0 Then
                    For Each _rows In _dsMaterial.Rows
                        _barcode = _rows("BarCode")
                        _Code = _rows("Id")
                        _item = _rows("ItemName")
                        _srate = _rows("SellPrice")
                        _taxValue = _rows("TaxValue")
                        _serialno = 1
                        _uom = 1
                        _businessType = _rows("BusinessType")
                        _allownegativestock = CBool(_rows("AllowNegStock"))
                    Next
                    If Not _allownegativestock Then
                        Dim stockqty As Integer = 0
                        GetstockDataOffline(_Code, stockqty)
                        If stockqty < 1 Then
                            ErrorMsg = "Insufficient stock for the item: " & _item
                            Return False
                        End If

                    End If
                    If _businessType = "71" Then
                        If _JsonData.PackageDataTable.Rows.Count > 0 Then
                            Dim packageRows = From pkgRow As DataRow In _JsonData.PackageDataTable Where String.Equals(pkgRow("PackageId"), _Code, StringComparison.CurrentCultureIgnoreCase)
                            If packageRows IsNot Nothing AndAlso packageRows.Any Then
                                For Each pkgRow In packageRows
                                    Dim pkgItemCode As Integer = pkgRow("ItemId")
                                    Dim pkgSellPrice As String = pkgRow("ItemPrice")
                                    ' Create a DataTable view, filter, and convert result to DataRow array
                                    Dim itemTable As DataTable = _JsonData.ItemTouchMasterTable

                                    ' Try multiple filter approaches to find the matching rows
                                    Dim filteredRows As DataRow() = Nothing
                                    Try
                                        ' Approach 1: Direct comparison with integer
                                        'filteredRows = itemTable.Select("Id = " & pkgItemCode)

                                        ' If no rows found, try alternative approaches
                                        If filteredRows Is Nothing OrElse filteredRows.Length = 0 Then
                                            ' Approach 2: Try with string comparison
                                            filteredRows = itemTable.Select("Id = '" & pkgItemCode.ToString() & "'")
                                        End If

                                        ' Approach 3: Case insensitive comparison
                                        If filteredRows Is Nothing OrElse filteredRows.Length = 0 Then
                                            filteredRows = itemTable.Select("LOWER(CONVERT(Id, 'System.String')) = '" & pkgItemCode.ToString().ToLower() & "'")
                                        End If

                                        ' If still no match, try a more direct approach with LINQ
                                        If filteredRows Is Nothing OrElse filteredRows.Length = 0 Then
                                            Dim linqFiltered = From row In itemTable.AsEnumerable()
                                                              Where row.Field(Of Object)("Id").ToString() = pkgItemCode.ToString()
                                                              Select row

                                            If linqFiltered.Any() Then
                                                filteredRows = linqFiltered.ToArray()
                                            End If
                                        End If
                                    Catch ex As Exception
                                        filteredRows = New DataRow() {} ' Empty array to avoid null reference
                                    End Try

                                    For Each itemRow As DataRow In filteredRows
                                        Dim pkgBarcode As String = itemRow("BarCode")
                                        Dim pkgItemName As String = itemRow("ItemName")
                                        Dim pkgTaxValue As Integer = itemRow("TaxValue")
                                        Dim pkgSerialNo As String = 1
                                        Dim pkgUOM As String = 1
                                        ' Insert each package item into the grid
                                        _InsertDt(pkgBarcode, pkgItemCode, pkgItemName, pkgSerialNo, pkgUOM, pkgSellPrice, pkgTaxValue)
                                    Next
                                Next
                            Else
                                ' No package items found for this code
                                ErrorMsg = "No package items found for the selected product."
                                Return False
                            End If
                        Else
                            ' Package table is empty
                            ErrorMsg = "Package data is not available."
                            Return False
                        End If
                    Else
                        _InsertDt(_barcode, _Code, _item, _serialno, _uom, _srate, _taxValue)
                    End If

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
            GridViewPOS.MoveLast()

            ' Reset quantity input to 1 for next item
            txtMqty.EditValue = 1
            If SalesGrandtotal(True) = False Then

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

    Public Function SalesGrandtotal(SalesManModeShow As Boolean) As Boolean
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
            NetAmountGlobal = 0
            ' Update UI labels with detailed discount breakdown
            lblsubtotal.Text = TAmount.ToString("0.00")
            lblitemdisctotal.Text = ItemDiscountAmt.ToString("0.00")
            lblbilldisctotal.Text = BillDiscountAmt.ToString("0.00")
            lblssttotal.Text = GST.ToString("0.00")
            lblservchargetotal.Text = ServiceChargeAmount.ToString("0.00")
            lblnetamt.Text = _RoundOff(NetTot).ToString("0.00")
            lblnoofitems.Text = GridDataTble_Insert.Rows.Count.ToString()
            lblnoofqty.Text = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("QTY")).ToString("0.00")
            NetAmountGlobal = _RoundOff(NetTot).ToString("0.00")
            ' Update any additional discount breakdown labels if they exist
            ' You can add these labels to show separate item and bill discounts
            ' lblitemdiscountonly.Text = ItemDiscountAmt.ToString("0.00")
            ' lblbilldiscountonly.Text = BillDiscountAmt.ToString("0.00")
            lblservcharge.Text = _globalSettingValues.ServiceTaxValue
            If SalesManModeShow = True Then
                barselectsalesman_ItemClick(Nothing, Nothing)
            End If

            Return True
        Catch ex As Exception
            MsgBox("Error in SalesGrandtotal: " & ex.Message)
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
#Region "StockManagement"
    Dim _ds As DataTable
    Private Sub GetStockData(ItemCode As String, ByRef RtnStock As Integer)
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            If CheckForInternetConnection() Then
                _ds = New DataTable
                dialog.Caption = "Connecting Data"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=78&comid=" & _companyInfo.ComId & "&locid=" & _companyInfo.LocId & "&itemcode=" & ItemCode)
                Dim Userparsejson As JObject = JObject.Parse(json)
                _ds = Userparsejson("Data").ToObject(Of DataTable)()
                If _ds.Rows.Count > 0 Then
                    RtnStock = _ds.Rows(0)("CurStock").ToString()
                    dialog.Caption = "Getting Data"
                Else
                    RtnStock = 0
                End If
            End If
        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub
    Private Sub GetstockDataOffline(ItemCode As String, ByRef RtnStock As Integer)
        Try
            Dim _DsStockTable As New DataSet
            Dim stockupdate As New StockUpdateParams
            stockupdate.Mode = "GetStockById"
            stockupdate.ItemCode = ItemCode
            stockupdate.ComId = _companyInfo.ComId
            stockupdate.LocId = _companyInfo.LocId
            stockupdate.OpStock = 0D
            stockupdate.StockIn = 0D
            stockupdate.StockOut = 0D
            stockupdate.LiveStock = 0D

            Dim sql(7) As SqlParameter
            sql(0) = New SqlParameter("@Mode", stockupdate.Mode)
            sql(1) = New SqlParameter("@pl_itemcode", stockupdate.ItemCode)
            sql(2) = New SqlParameter("@pl_comid", stockupdate.ComId)
            sql(3) = New SqlParameter("@pl_locid", stockupdate.LocId)
            sql(4) = New SqlParameter("@pl_opstok", stockupdate.OpStock)
            sql(5) = New SqlParameter("@pl_stockin", stockupdate.StockIn)
            sql(6) = New SqlParameter("@pl_stockout", stockupdate.StockOut)
            sql(7) = New SqlParameter("@pl_livestock", stockupdate.LiveStock)
            _DsStockTable = _sqlDataAdapter2("sp_upsert_livestock", sql)
            If _DsStockTable.Tables(0).Rows.Count > 0 Then
                RtnStock = _DsStockTable.Tables(0).Rows(0)("pl_livestock").ToString
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub StockUpdateOffline(ByRef ItemCode As Integer, ByRef qty As Decimal)
        Try
            Using SqlConnection As New SqlConnection(M_Details._Conn)
                Dim cmd As New SqlClient.SqlCommand("sp_upsert_livestock", SqlConnection)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Mode", "Update")
                cmd.Parameters.AddWithValue("@pl_itemcode", ItemCode)
                cmd.Parameters.AddWithValue("@pl_comid", _companyInfo.ComId)
                cmd.Parameters.AddWithValue("@pl_locid", _companyInfo.LocId)
                cmd.Parameters.AddWithValue("@pl_opstok", 0)
                cmd.Parameters.AddWithValue("@pl_stockin", 0)
                cmd.Parameters.AddWithValue("@pl_stockout", -qty)
                cmd.Parameters.AddWithValue("@pl_livestock", qty)
                SqlConnection.Open()
                cmd.ExecuteNonQuery()
                SqlConnection.Close()
            End Using
        Catch ex As Exception

        End Try
    End Sub
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
            frmKeyPassIIIMaster.ShowDialog()
            If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
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
            End If
        Catch ex As Exception
            MessageBox.Show("Error applying bill discount: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Item Discount Button Click Handler
    Private Sub BarButtonItem8_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles baritemdiscount.ItemClick
        Try
            frmKeyPassIIIMaster.ShowDialog()
            If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
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
            SalesGrandtotal(False)

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
            SalesGrandtotal(False)

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
            SalesGrandtotal(False)

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
                SalesGrandtotal(False)

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
            frmKeyPassIIIMaster.ShowDialog()
            If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
                ClearCurrentBill()

                ' Set focus to search field for quick item entry
                cmbMaterialSearch.Focus()

            End If

        Catch ex As Exception
            MessageBox.Show("Error starting new bill: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ClearNewBill()
        Try
            ClearCurrentBill()

            ' Set focus to search field for quick item entry
            cmbMaterialSearch.Focus()

        Catch ex As Exception

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
            modeBillHold = "New"
            BillHoldTokenNo = 0
            barstatustoken.Caption = 0
            barbtnstatus.Caption = "Sales Mode : " & modeOfSale
            barbtnbilltype.Caption = "Sales"
            'Reset Bill
            _recallHoldBill = False
            BillHoldTrno = 0
            G_SalID = 0
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
            SalesGrandtotal(False)

            ' Clear search fields
            cmbMaterialSearch.Text = ""
            cmbMaterialSearch.EditValue = Nothing
            txtsearch2.Text = ""
            txtMqty.EditValue = 1

            ' Update data source
            GridControlSalesData.DataSource = GridDataTble_Insert
            'payment term
            'CmbPaymentTerm.SelectedIndex = 0
            NetAmountGlobal = 0
            If barchkcustomerpole.Checked = True Then
                If CustomerPoleOpen(Errstr) = True Then

                    Dim stpole2 As String = ""
                    stpole2 = "Welcome,Thank You!"
                    If stpole1.Length > 19 Then
                        If SetCustomerPole(stpole1.Substring(0, 19), stpole2, Errstr) = False Then

                        End If
                    Else
                        If SetCustomerPole(stpole1, stpole2, Errstr) = False Then

                        End If
                    End If

                    CustomerPoleClose(Errstr)
                End If
            End If
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
            SalesGrandtotal(False)

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
            ElseIf _companyInfo.UserRoleId = 1 OrElse _companyInfo.UserRoleId = 2 Then
                ShowMultiplePriceSelection()
            Else
                ShowMultiplePriceSelection()
            End If

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
            If _JsonData.ItemTouchMasterTable.Rows.Count > 0 Then
                Dim dt As DataTable = Nothing
                Dim query = _JsonData.ItemTouchMasterTable.AsEnumerable().
                 Where(Function(rs) Convert.ToInt32(If(rs("Id"), 0)) = itemId)


                If query.Any() Then
                    Dim row = query.First()

                    Dim minPrice As Decimal = If(IsDBNull(row("MinPrice")), 0D, Convert.ToDecimal(row("MinPrice")))
                    Dim maxPrice As Decimal = If(IsDBNull(row("MaxPrice")), 0D, Convert.ToDecimal(row("MaxPrice")))

                    ' Show multiple price selection form
                    Dim priceSelectionForm As New FrmMultiplePriceSelection(itemId, itemName)
                    If priceSelectionForm.ShowDialog() = DialogResult.OK Then
                        Dim selectedPriceInfo = priceSelectionForm.SelectedPriceInfo

                        If selectedPriceInfo IsNot Nothing Then
                            ' Apply the selected price to the item
                            ApplyMultiplePrice(rowHandle, selectedPriceInfo, minPrice, maxPrice)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error showing multiple price selection: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Apply selected multiple price to item
    Private Sub ApplyMultiplePrice(rowIndex As Integer, priceInfo As Object, minPrice As Decimal, maxPrice As Decimal)
        Try
            If rowIndex < 0 OrElse rowIndex >= GridDataTble_Insert.Rows.Count Then
                Exit Sub
            End If

            ' Basic validation
            If priceInfo Is Nothing OrElse _
               priceInfo.PriceValue Is Nothing OrElse _
               Not IsNumeric(priceInfo.PriceValue) OrElse _
               Convert.ToDecimal(priceInfo.PriceValue) <= 0 Then
                MessageBox.Show("Invalid price. Please select a valid price greater than zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim newPrice As Decimal = Convert.ToDecimal(priceInfo.PriceValue)
            Dim priceName As String = priceInfo.PriceName.ToString()

            ' If SelectedPrice is False, then check min/max range
            If priceInfo.SelectedPrice IsNot Nothing AndAlso priceInfo.SelectedPrice = False Then

                ' Always check minimum
                If newPrice < minPrice Then
                    MessageBox.Show(String.Format("Price must be between {0:0.00} and {1:0.00}.", minPrice, maxPrice), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                ' Only check maximum if it's not zero
                If maxPrice > 0 AndAlso newPrice > maxPrice Then
                    MessageBox.Show(String.Format("Price must be between {0:0.00} and {1:0.00}.", minPrice, maxPrice), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If

            ' ✅ Passed all checks → update the price
            GridDataTble_Insert.Rows(rowIndex)("RATE") = newPrice

            ' Recalculate all amounts for this row
            RecalculateRowAmounts(rowIndex)

            ' Accept changes and refresh
            GridDataTble_Insert.AcceptChanges()
            GridControlSalesData.DataSource = GridDataTble_Insert

            ' Update grand totals
            SalesGrandtotal(False)

            ' Keep focus on the updated row
            GridViewPOS.FocusedRowHandle = rowIndex

        Catch ex As Exception
            MessageBox.Show("Error applying multiple price: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub barbtnmaualprice_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnmaualprice.ItemClick
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

            Dim newPrice As Decimal = 0D

            ' Only allow admin/manager
            If _companyInfo.UserRoleId = 1 OrElse _companyInfo.UserRoleId = 2 Then
                frmKeyPassIIIMaster.ShowDialog()
                If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
                    frmKeyQtyAmt.ShowDialog()
                    If frmKeyQtyAmt.DialogResult = Windows.Forms.DialogResult.OK Then

                        ' Safer parsing
                        If Decimal.TryParse(frmKeyQtyAmt._TXTPASS.EditValue.ToString(), newPrice) AndAlso newPrice > 0D Then
                            ' Update the price in DataTable
                            GridDataTble_Insert.Rows(rowHandle)("RATE") = newPrice
                            'If _JsonData.ItemTouchMasterTable.Rows.Count > 0 Then

                            'End If
                            ' Recalculate all amounts for this row
                            RecalculateRowAmounts(rowHandle)

                            ' Accept changes
                            GridDataTble_Insert.AcceptChanges()

                            ' Refresh grid without resetting DataSource
                            GridViewPOS.RefreshData()

                            ' Update grand totals
                            SalesGrandtotal(False)

                            ' Keep focus on the updated row
                            GridViewPOS.FocusedRowHandle = rowHandle
                        Else
                            MessageBox.Show("Invalid price entered. Please enter a valid numeric value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                    Else
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            Else
                MessageBox.Show("You don't have rights to edit the price.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

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
            Dim salesmanForm As New FrmSelectSalesman(True) ' True for selection mode
            If salesmanForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                UpdateItemSalesman(GridViewPOS.FocusedRowHandle,
                                 salesmanForm.SelectedSalesmanId,
                                 salesmanForm.SelectedSalesmanName)
            End If
            '' Show salesman selection dialog
            'Dim salesmanForm As New FrmSalesmanList(True) ' True for selection mode
            'If salesmanForm.ShowDialog() = DialogResult.OK Then
            '    ' Update the selected item with salesman information
            '    UpdateItemSalesman(GridViewPOS.FocusedRowHandle,
            '                     salesmanForm.SelectedSalesmanId,
            '                     salesmanForm.SelectedSalesmanName)

            'End If
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
    Dim BillHoldTokenNo As Integer = 0
    Dim BillHoldTrno As Integer = 0
    Private Sub btnpayment_Click(sender As Object, e As EventArgs) Handles btnpayment.Click
        Try
            If RegistrationDetails._paymentPopupActive = True Then
                If GridDataTble_Insert.Rows.Count > 0 Then
                    For Each SalesData In GridDataTble_Insert.Rows
                        SalesData("ITEMLOCK") = 2
                    Next
                    GridDataTble_Insert.AcceptChanges()
                    If PaymentProcess() Then
                        BillHoldTokenNo = 0
                        BillHoldTrno = 0
                        G_SalID = 0
                    End If
                End If
            Else
                If btnpayment.Text = "Send Order" Then
                    If BillHoldProcess() Then
                        BillHoldTokenNo = 0
                        BillHoldTrno = 0
                        G_SalID = 0
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnSendOrderServer_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnSendOrderServer.ItemClick
        Try
            If GridViewPOS.RowCount > 0 Then
                If BillHoldProcess() Then
                    BillHoldTokenNo = 0
                    BillHoldTrno = 0
                    G_SalID = 0
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function BillHoldProcess() As Boolean
        Try
            If BillHoldTokenNo = 0 Then
                frmkeytablescaner.ShowDialog()
                frmkeytablescaner.DialogResult = Windows.Forms.DialogResult.OK
                BillHoldTokenNo = _FunctionKeyBoardModule.gs_keyboardValueInteger
            End If
            Dim _givenAmt As Decimal = 0.0
            Dim _BalanceAmt As Decimal = 0.0
            If modeBillHold = "New" Then
                If GridViewPOS.RowCount > 0 Then

                    ' No payments in table, use default cash
                    _PaymentDtl.paymentModeSelection = "Hold Bill"
                    _PaymentDtl.paymentMode = "hold"
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
                    _saleData.psih_invoice_token = BillHoldTokenNo
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
                    _saleData.psih_invoice_saletype = "BillHold"
                    _saleData.psih_invoice_billtype = _PaymentDtl.paymentModeSelection

                    ' Handle both single and multiple payment modes for bill status
                    If _PaymentDtl.paymentMode.Contains("hold") Then
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
                    If frmPaymoreII.txtadvanceamt.EditValue > 0 And Not _PaymentDtl.paymentMode.Contains("credit") Then
                        _saleData.psih_invoice_advamt = 0
                        DevExpress.XtraEditors.XtraMessageBox.Show("Advance Amount Can Only Be Accepted For Credit Bills.", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    Else
                        If frmPaymoreII.txtadvanceamt.EditValue > 0 Then
                            _saleData.psih_invoice_advamt = frmPaymoreII.txtadvanceamt.EditValue
                            _saleData.psih_invoice_outstanding = _saleData.psih_invoice_tnetamt - _saleData.psih_invoice_advamt
                        ElseIf _PaymentDtl.paymentMode.Contains("credit") Then
                            _saleData.psih_invoice_outstanding = _saleData.psih_invoice_tnetamt
                        Else
                            _saleData.psih_invoice_advamt = 0
                        End If
                    End If

                    If frmPaymoreII.txttpopenamt.EditValue Is Nothing Then
                        _saleData.psih_invoice_givenamt = 0
                    Else
                        _saleData.psih_invoice_givenamt = frmPaymoreII.txttpopenamt.EditValue
                        _givenAmt = _saleData.psih_invoice_givenamt
                    End If
                    If frmPaymoreII.txtpopbalamt.EditValue Is Nothing Then
                        _saleData.psih_invoice_balamt = 0
                    Else
                        _saleData.psih_invoice_balamt = frmPaymoreII.txtpopbalamt.EditValue
                        _BalanceAmt = _saleData.psih_invoice_balamt
                    End If
                    _saleData.psih_invoice_shiftno = _saleSetting._curShiftno
                    _saleData.psih_invoice_dayno = _saleSetting._curDayno
                    _saleData.psih_invoice_countername = Environment.MachineName
                    _saleData.psih_invoice_print = "0"
                    _saleData.psih_invoice_webhost = "0"
                    ' Basic validation - check if we have items
                    If GridDataTble_Insert.Rows.Count = 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("No items to save", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End If


                    Dim salesDetailsList As List(Of SalesDetails) = salesHelper.ConvertDataTableToSalesDetails(GridDataTble_Insert)
                    Dim _errMsgResult As String = ""
                    Dim ReturnBill As String = "0"

                    ' Always use Dictionary method for both single and multiple payments
                    If billHoldHelper.SaveHoldBill(_saleData, salesDetailsList, _errMsgResult, ReturnBill) = True Then
                        barstatuslastbillno.Caption = ReturnBill
                        Dim frmMsgBox As New frmMsgBoxOkOnly
                        Dim msgData = "Total Bill Amount : " & lblnetamt.Text & " Bill Hold Saved - " & ReturnBill
                        frmMsgBox.ShowDialogData(msgData.ToString)
                        ' _CashDraw.OpenCashdrawer(True)
                        'DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & " Order Saved Success.", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ClearNewBill()
                    Else
                        DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & " Order Not Saved", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                End If

            ElseIf modeBillHold = "Edit" Then
                If GridViewPOS.RowCount > 0 Then
                    ' No payments in table, use default cash
                    _PaymentDtl.paymentModeSelection = "Hold Bill"
                    _PaymentDtl.paymentMode = "hold"

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
                    _saleData.psih_invoice_token = BillHoldTokenNo
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
                    _saleData.psih_invoice_saletype = "BillHold"
                    _saleData.psih_invoice_billtype = _PaymentDtl.paymentModeSelection

                    ' Handle both single and multiple payment modes for bill status
                    If _PaymentDtl.paymentMode.Contains("hold") Then
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
                    If frmPaymoreII.txtadvanceamt.EditValue > 0 And Not _PaymentDtl.paymentMode.Contains("credit") Then
                        _saleData.psih_invoice_advamt = 0
                        DevExpress.XtraEditors.XtraMessageBox.Show("Advance Amount Can Only Be Accepted For Credit Bills.", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    Else
                        If frmPaymoreII.txtadvanceamt.EditValue > 0 Then
                            _saleData.psih_invoice_advamt = frmPaymoreII.txtadvanceamt.EditValue
                            _saleData.psih_invoice_outstanding = _saleData.psih_invoice_tnetamt - _saleData.psih_invoice_advamt
                        ElseIf _PaymentDtl.paymentMode.Contains("credit") Then
                            _saleData.psih_invoice_outstanding = _saleData.psih_invoice_tnetamt
                        Else
                            _saleData.psih_invoice_advamt = 0
                        End If
                    End If

                    If frmPaymoreII.txttpopenamt.EditValue Is Nothing Then
                        _saleData.psih_invoice_givenamt = 0
                    Else
                        _saleData.psih_invoice_givenamt = frmPaymoreII.txttpopenamt.EditValue
                        _givenAmt = _saleData.psih_invoice_givenamt
                    End If
                    If frmPaymoreII.txtpopbalamt.EditValue Is Nothing Then
                        _saleData.psih_invoice_balamt = 0
                    Else
                        _saleData.psih_invoice_balamt = frmPaymoreII.txtpopbalamt.EditValue
                        _BalanceAmt = _saleData.psih_invoice_balamt
                    End If
                    _saleData.psih_invoice_shiftno = _saleSetting._curShiftno
                    _saleData.psih_invoice_dayno = _saleSetting._curDayno
                    _saleData.psih_invoice_countername = Environment.MachineName
                    _saleData.psih_invoice_print = "0"
                    _saleData.psih_invoice_webhost = "0"
                    ' Basic validation - check if we have items
                    If GridDataTble_Insert.Rows.Count = 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("No items to update", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End If

                    ' Update to database using stored procedure with payment modes

                    Dim salesDetailsList As List(Of SalesDetails) = salesHelper.ConvertDataTableToSalesDetails(GridDataTble_Insert)
                    Dim _errMsgResult As String = ""
                    Dim ReturnBill As String = "0"

                    ' Always use Dictionary method for both single and multiple payments
                    If billHoldHelper.UpdateHoldBill(_saleData, salesDetailsList, _errMsgResult, ReturnBill) = True Then
                        barstatuslastbillno.Caption = ReturnBill
                        Dim frmMsgBox As New frmMsgBoxOkOnly
                        Dim msgData = "Total Bill Amount : " & lblnetamt.Text & " Bill Hold Updated - " & ReturnBill
                        frmMsgBox.ShowDialogData(msgData.ToString)
                        ClearNewBill()
                    Else
                        DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & " Bill Hold Not Updated", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                End If
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show("View Mode Cant Be Save Bill", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
            'If barchkcustomerpole.Checked = True Then
            '    If CustomerPoleOpen(Errstr) = True Then
            '        Dim stpole1 As String = ""
            '        Dim stpole2 As String = ""
            '        stpole1 = "Received RM " & Format(_givenAmt, "###0.00")
            '        stpole2 = "Balance RM " & Format(_BalanceAmt, "###0.00")
            '        If SetCustomerPole(stpole1, stpole2, Errstr) = False Then

            '        End If
            '        CustomerPoleClose(Errstr)
            '    End If
            'End If
            Return True
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message & "Bill Hold Not Processed", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End Try
    End Function
    Private Function IsAutoDiscountAllowed(ByVal value As Object) As Boolean
        If value Is Nothing OrElse Convert.IsDBNull(value) Then Return False

        Dim normalizedValue As String = value.ToString().Trim()
        Return normalizedValue = "1" OrElse normalizedValue.Equals("true", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Function CanApplyAutoDiscountForCurrentBill(Optional ByVal followMainGroupOnly As Boolean = False) As Boolean
        Try
            If _JsonData.ItemTouchMasterTable.Rows.Count = 0 OrElse _JsonData.MainGroupTable.Rows.Count = 0 Then
                Return False
            End If
            If Not _JsonData.MainGroupTable.Columns.Contains("AllowDiscount") Then
                Return False
            End If
            If Not followMainGroupOnly AndAlso Not _JsonData.ItemTouchMasterTable.Columns.Contains("AllowDiscount") Then
                Return False
            End If

            Dim itemMap As New Dictionary(Of String, DataRow)
            For Each masterRow As DataRow In _JsonData.ItemTouchMasterTable.Rows
                Dim itemCode As String = masterRow("Id").ToString()
                If Not itemMap.ContainsKey(itemCode) Then
                    itemMap.Add(itemCode, masterRow)
                End If
            Next

            For Each salesRow As DataRow In GridDataTble_Insert.Rows
                Dim itemCode As String = salesRow("ITEMCODE").ToString()
                If Not itemMap.ContainsKey(itemCode) Then Continue For

                Dim itemMasterRow As DataRow = itemMap(itemCode)
                If Not followMainGroupOnly AndAlso Not IsAutoDiscountAllowed(itemMasterRow("AllowDiscount")) Then Continue For

                Dim mainId As String = itemMasterRow("MainId").ToString()
                If String.IsNullOrEmpty(mainId) Then Continue For

                Dim mainRows() As DataRow = _JsonData.MainGroupTable.Select("MainId = '" & mainId.Replace("'", "''") & "'")
                If mainRows.Length = 0 Then Continue For
                Dim mainGroupAllowsDiscount As Boolean = True
                For Each mainRow As DataRow In mainRows
                    If Not IsAutoDiscountAllowed(mainRow("AllowDiscount")) Then
                        mainGroupAllowsDiscount = False
                        Exit For
                    End If
                Next
                If mainGroupAllowsDiscount Then Return True
            Next

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ApplyAutoDiscOfferBeforePayment()
        Try
            If _JsonData.DiscountPolicyTable.Rows.Count = 0 Then Exit Try
            If _JsonData.ItemTouchMasterTable.Rows.Count = 0 Then Exit Try
            If GridDataTble_Insert.Rows.Count = 0 Then Exit Try

            ' --- Step 1: Build ITEMCODE -> MainId lookup ---
            Dim itemMainMap As New Dictionary(Of String, String)
            For Each masterRow As DataRow In _JsonData.ItemTouchMasterTable.Rows
                Dim idKey As String = masterRow("Id").ToString()
                If Not itemMainMap.ContainsKey(idKey) Then
                    itemMainMap.Add(idKey, masterRow("MainId").ToString())
                End If
            Next

            ' --- Step 2: Clear existing bill-level discounts on all rows ---
            For i As Integer = 0 To GridDataTble_Insert.Rows.Count - 1
                Dim r As DataRow = GridDataTble_Insert.Rows(i)
                r("BILL_DPER") = 0
                r("BILL_DAMT") = 0
                r("TOTAL_DAMT") = Convert.ToDecimal(r("ITEM_DAMT"))
                r("TOTAL_DPER") = Convert.ToDecimal(r("ITEM_DPER"))
                _RecalculateRowTotals(i)
            Next

            ' --- Step 3: Calculate TOTAL bill amount (all rows) ---
            Dim totalBillAmt As Decimal = 0D
            For i As Integer = 0 To GridDataTble_Insert.Rows.Count - 1
                totalBillAmt += Convert.ToDecimal(GridDataTble_Insert.Rows(i)("TAMOUNT"))
            Next
            If totalBillAmt <= 0D Then Exit Try

            ' --- Step 4: Match tier against total bill amount (highest qualifying tier) ---
            Dim today As Date = Date.Today
            Dim matchedPolicy As DataRow = Nothing
            Dim highestMin As Decimal = -1D
            For Each pol As DataRow In _JsonData.DiscountPolicyTable.Rows
                If pol("Active").ToString() <> "1" Then Continue For
                Dim minAmt As Decimal = Convert.ToDecimal(pol("MinAmount"))
                If totalBillAmt < minAmt Then Continue For
                Dim maxAmtStr As String = pol("MaxAmount").ToString()
                If Not String.IsNullOrEmpty(maxAmtStr) Then
                    Dim maxAmt As Decimal
                    If Decimal.TryParse(maxAmtStr, maxAmt) AndAlso maxAmt > 0D _
                       AndAlso totalBillAmt > maxAmt Then Continue For
                End If
                Dim validDateStr As String = pol("ValidDate").ToString()
                If Not String.IsNullOrEmpty(validDateStr) Then
                    Dim dtValid As Date
                    If Date.TryParse(validDateStr, dtValid) AndAlso dtValid < today Then Continue For
                End If
                If minAmt > highestMin Then
                    highestMin = minAmt
                    matchedPolicy = pol
                End If
            Next
            If matchedPolicy Is Nothing Then Exit Try

            ' --- Step 5: Voucher gate if required ---
            _pendingVoucherId = 0
            _pendingVoucherNo = 0
            If matchedPolicy("RequireVoucher").ToString() = "1" Then
                properClass.R_TextNumKey = ""
                Dim kb As New xkeyboard
                kb.Text = "Voucher Code – " & matchedPolicy("Name").ToString()
                kb.ShowDialog()
                Dim voucher As String = properClass.R_TextNumKey
                If String.IsNullOrWhiteSpace(voucher) Then Exit Try

                ' Validate voucher via server (AjaxRequest=91)
                Dim voucherIsValid As Boolean = False
                Try
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                    Dim vResponse As String = New WebClient().DownloadString(
                        M_Details.LinkAjaxRequest & "AjaxRequest=91&vouchercode=" &
                        Uri.EscapeDataString(voucher.Trim()))
                    Dim vObj As JObject = JObject.Parse(vResponse)
                    If Not vObj("Success").ToObject(Of Boolean)() Then
                        Dim errMsg As String = If(vObj("Msg") IsNot Nothing,
                            vObj("Msg").ToString(), "Invalid voucher code.")
                        DevExpress.XtraEditors.XtraMessageBox.Show(
                            errMsg, "Voucher Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Try
                    End If
                    _pendingVoucherId = vObj("VoucherId").ToObject(Of Integer)()
                    _pendingVoucherNo = vObj("VoucherNo").ToObject(Of Integer)()
                    voucherIsValid = True
                Catch exv As Exception
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Voucher validation error: " & exv.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Try
                End Try
                If Not voucherIsValid Then Exit Try
            End If

            ' --- Step 6: Calculate discount amount based on total bill ---
            Dim discType As String = matchedPolicy("DiscountType").ToString().ToLower().Trim()
            Dim discValue As Decimal = Convert.ToDecimal(matchedPolicy("DiscountValue"))
            Dim discAmt As Decimal = 0D
            If discType = "percentage" Then
                discAmt = Math.Round((totalBillAmt * discValue) / 100D, 2)
            Else
                discAmt = discValue
            End If
            If discAmt <= 0D Then Exit Try

            ' --- Step 7: Collect rows whose MainGroup allows discounts ---
            Dim eligibleRows As New List(Of Integer)
            Dim eligibleTotal As Decimal = 0D
            For i As Integer = 0 To GridDataTble_Insert.Rows.Count - 1
                Dim r As DataRow = GridDataTble_Insert.Rows(i)
                Dim code As String = r("ITEMCODE").ToString()
                If Not itemMainMap.ContainsKey(code) Then Continue For
                Dim mId As String = itemMainMap(code)
                If String.IsNullOrEmpty(mId) Then Continue For
                Dim mainRows() As DataRow = _JsonData.MainGroupTable.Select("MainId = '" & mId & "'")
                If mainRows.Length = 0 Then Continue For
                Dim mainGroupAllowsDiscount As Boolean = True
                For Each mainRow As DataRow In mainRows
                    If Not IsAutoDiscountAllowed(mainRow("AllowDiscount")) Then
                        mainGroupAllowsDiscount = False
                        Exit For
                    End If
                Next
                If Not mainGroupAllowsDiscount Then Continue For
                eligibleRows.Add(i)
                eligibleTotal += Convert.ToDecimal(r("TAMOUNT"))
            Next
            If eligibleRows.Count = 0 OrElse eligibleTotal <= 0D Then Exit Try

            ' --- Step 8: Distribute discount proportionally across eligible items only ---
            For Each ri As Integer In eligibleRows
                Dim br As DataRow = GridDataTble_Insert.Rows(ri)
                Dim itemAmt As Decimal = Convert.ToDecimal(br("TAMOUNT"))
                Dim billDiscAmt As Decimal = Math.Round((itemAmt / eligibleTotal) * discAmt, 2)
                Dim itemDiscAmt As Decimal = Convert.ToDecimal(br("ITEM_DAMT"))
                br("BILL_DAMT") = billDiscAmt
                br("BILL_DPER") = If(itemAmt > 0D, Math.Round((billDiscAmt / itemAmt) * 100D, 2), 0D)
                Dim totalDisc As Decimal = itemDiscAmt + billDiscAmt
                br("TOTAL_DAMT") = totalDisc
                br("TOTAL_DPER") = If(itemAmt > 0D, Math.Round((totalDisc / itemAmt) * 100D, 2), 0D)
                _RecalculateRowTotals(ri)
            Next

            ' --- Step 9: Refresh grid and totals ---
            GridDataTble_Insert.AcceptChanges()
            GridControlSalesData.DataSource = GridDataTble_Insert
            SalesGrandtotal(False)

            Dim discDisplay As String = If(discType = "percentage",
                                          discValue.ToString("0.##") & "%",
                                          "RM " & discValue.ToString("0.00"))
            DevExpress.XtraEditors.XtraMessageBox.Show(
                "Auto discount applied: " & matchedPolicy("Name").ToString() & " (" & discDisplay & ")" &
                vbCrLf & "Bill Total: RM " & totalBillAmt.ToString("0.00") &
                vbCrLf & "Discount: RM " & discAmt.ToString("0.00"),
                M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show("Error applying auto discount/offer before payment: " & ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Function PaymentProcess() As Boolean
        Try
            'procesing of auto discount and offer before payment calculation based on discount policy
            If _globalSetting.AutoDiscountSchemes = True Then
                Dim followMainGroupOnly As Boolean = True
                If GridDataTble_Insert.Rows.Count > 0 AndAlso CanApplyAutoDiscountForCurrentBill(followMainGroupOnly) Then
                    ApplyAutoDiscOfferBeforePayment()
                End If
            End If
            If barchkcustomerpole.Checked = True Then
                If CustomerPoleOpen(Errstr) = True Then

                    Dim stpole2 As String = ""
                    stpole2 = "Total RM " & Format(NetAmountGlobal, "0.00")
                    If stpole1.Length > 19 Then
                        If SetCustomerPole(stpole1.Substring(0, 19), stpole2, Errstr) = False Then

                        End If
                    Else
                        If SetCustomerPole(stpole1, stpole2, Errstr) = False Then

                        End If
                    End If

                    CustomerPoleClose(Errstr)
                End If
            End If
            Dim _givenAmt As Decimal = 0.0
            Dim _BalanceAmt As Decimal = 0.0
            If modeOfSale = "New" Then
                If GridViewPOS.RowCount > 0 Then
                    frmPaymoreII.ShowDialog(lblnetamt.Text, selectedCustomerName)
                    If frmPaymoreII.DialogResult = Windows.Forms.DialogResult.OK Then
                        ' Get payment details from PaymentDetailTable
                        Dim paymentModeSelections As New List(Of String)()
                        Dim paymentModes As New List(Of String)()

                        ' Check if multiple payments or single payment
                        If frmPaymoreII.PaymentDetailTable.Rows.Count > 1 Then
                            ' Multiple payments - combine all payment modes
                            For Each paymentRow As DataRow In frmPaymoreII.PaymentDetailTable.Rows
                                Dim paymentName As String = paymentRow("pmode_name").ToString()
                                Dim paymentType As String = paymentRow("pmode_type").ToString()
                                paymentModeSelections.Add(paymentName)

                                ' Map payment names to payment modes
                                Select Case paymentType.ToLower()
                                    Case "cash", "rm"
                                        paymentModes.Add("cash")
                                    Case "credit card", "debit card", "card", "bank card", "debit/credit card"
                                        paymentModes.Add("card")
                                    Case "bank transfer", "upi", "online", "bank", "qr pay"
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

                        ElseIf frmPaymoreII.PaymentDetailTable.Rows.Count = 1 Then
                            ' Single payment mode
                            Dim paymentRow As DataRow = frmPaymoreII.PaymentDetailTable.Rows(0)
                            Dim paymentName As String = paymentRow("pmode_name").ToString()
                            _PaymentDtl.paymentModeSelection = paymentName

                            ' Map payment name to payment mode
                            Select Case paymentName.ToLower()
                                Case "cash", "rm"
                                    _PaymentDtl.paymentMode = "cash"
                                Case "credit card", "debit card", "card", "bank card", "debit/credit card"
                                    _PaymentDtl.paymentMode = "card"
                                Case "bank transfer", "upi", "online", "bank", "qr pay"
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
                        _saleData.psih_invoice_token = BillHoldTokenNo
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
                        If frmPaymoreII.txtadvanceamt.EditValue > 0 And Not _PaymentDtl.paymentMode.Contains("credit") Then
                            _saleData.psih_invoice_advamt = 0
                            DevExpress.XtraEditors.XtraMessageBox.Show("Advance Amount Can Only Be Accepted For Credit Bills.", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return False
                        Else
                            If frmPaymoreII.txtadvanceamt.EditValue > 0 Then
                                _saleData.psih_invoice_advamt = frmPaymoreII.txtadvanceamt.EditValue
                                _saleData.psih_invoice_outstanding = _saleData.psih_invoice_tnetamt - _saleData.psih_invoice_advamt
                            ElseIf _PaymentDtl.paymentMode.Contains("credit") Then
                                _saleData.psih_invoice_outstanding = _saleData.psih_invoice_tnetamt
                            Else
                                _saleData.psih_invoice_advamt = 0
                            End If
                        End If

                        If frmPaymoreII.txttpopenamt.EditValue Is Nothing Then
                            _saleData.psih_invoice_givenamt = 0
                        Else
                            _saleData.psih_invoice_givenamt = frmPaymoreII.txttpopenamt.EditValue
                            _givenAmt = _saleData.psih_invoice_givenamt
                        End If
                        If frmPaymoreII.txtpopbalamt.EditValue Is Nothing Then
                            _saleData.psih_invoice_balamt = 0
                        Else
                            _saleData.psih_invoice_balamt = frmPaymoreII.txtpopbalamt.EditValue
                            _BalanceAmt = _saleData.psih_invoice_balamt
                        End If
                        _saleData.psih_invoice_shiftno = _saleSetting._curShiftno
                        _saleData.psih_invoice_dayno = _saleSetting._curDayno
                        _saleData.psih_invoice_countername = Environment.MachineName
                        _saleData.psih_invoice_print = "0"
                        _saleData.psih_invoice_webhost = "0"
                        ' Basic validation - check if we have items
                        If GridDataTble_Insert.Rows.Count = 0 Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("No items to save", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return False
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

                            If BillHoldTokenNo > 0 And BillHoldTrno > 0 Then
                                billHoldHelper.UpdateHoldBillStatus(BillHoldTokenNo, BillHoldTrno)
                            End If
                            Dim frmMsgBox As New frmMsgBoxOkOnly
                            Dim msgData = "Total Bill Amount : " & lblnetamt.Text & " Bill Saved - " & ReturnBill
                            ClearNewBill()
                            frmMsgBox.ShowDialogData(msgData.ToString)


                            'DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & " Bill Saved", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & " Bill Not Saved", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If

            ElseIf modeOfSale = "Edit" Then
                If GridViewPOS.RowCount > 0 Then
                    frmPaymoreII.ShowDialog(lblnetamt.Text, selectedCustomerName)

                    If frmPaymoreII.DialogResult = Windows.Forms.DialogResult.OK Then
                        ' Get payment details from PaymentDetailTable
                        Dim paymentModeSelections As New List(Of String)()
                        Dim paymentModes As New List(Of String)()

                        ' Check if multiple payments or single payment
                        If frmPaymoreII.PaymentDetailTable.Rows.Count > 1 Then
                            ' Multiple payments - combine all payment modes
                            For Each paymentRow As DataRow In frmPaymoreII.PaymentDetailTable.Rows
                                Dim paymentName As String = paymentRow("pmode_name").ToString()
                                Dim paymentType As String = paymentRow("pmode_type").ToString()
                                paymentModeSelections.Add(paymentName)

                                ' Map payment types to payment modes
                                Select Case paymentType.ToLower()
                                    Case "cash", "rm"
                                        paymentModes.Add("cash")
                                    Case "credit card", "debit card", "card", "bank card", "debit/credit card"
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

                        ElseIf frmPaymoreII.PaymentDetailTable.Rows.Count = 1 Then
                            ' Single payment mode
                            Dim paymentRow As DataRow = frmPaymoreII.PaymentDetailTable.Rows(0)
                            Dim paymentName As String = paymentRow("pmode_name").ToString()
                            Dim paymentType As String = paymentRow("pmode_type").ToString()
                            _PaymentDtl.paymentModeSelection = paymentName

                            ' Map payment type to payment mode
                            Select Case paymentType.ToLower()
                                Case "cash", "rm"
                                    _PaymentDtl.paymentMode = "cash"
                                Case "credit card", "debit card", "card", "bank card", "debit/credit card"
                                    _PaymentDtl.paymentMode = "card"
                                Case "bank transfer", "upi", "online", "bank", "qr pay"
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
                        _saleData.psih_invoice_token = BillHoldTokenNo
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
                        If frmPaymoreII.txtadvanceamt.EditValue > 0 And Not _PaymentDtl.paymentMode.Contains("credit") Then
                            _saleData.psih_invoice_advamt = 0
                            DevExpress.XtraEditors.XtraMessageBox.Show("Advance Amount Can Only Be Accepted For Credit Bills.", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return False
                        Else
                            If frmPaymoreII.txtadvanceamt.EditValue > 0 Then
                                _saleData.psih_invoice_advamt = frmPaymoreII.txtadvanceamt.EditValue
                                _saleData.psih_invoice_outstanding = _saleData.psih_invoice_tnetamt - _saleData.psih_invoice_advamt
                            ElseIf _PaymentDtl.paymentMode.Contains("credit") Then
                                _saleData.psih_invoice_outstanding = _saleData.psih_invoice_tnetamt
                            Else
                                _saleData.psih_invoice_advamt = 0
                            End If
                        End If

                        If frmPaymoreII.txttpopenamt.EditValue Is Nothing Then
                            _saleData.psih_invoice_givenamt = 0
                        Else
                            _saleData.psih_invoice_givenamt = frmPaymoreII.txttpopenamt.EditValue
                            _givenAmt = _saleData.psih_invoice_givenamt
                        End If
                        If frmPaymoreII.txtpopbalamt.EditValue Is Nothing Then
                            _saleData.psih_invoice_balamt = 0
                        Else
                            _saleData.psih_invoice_balamt = frmPaymoreII.txtpopbalamt.EditValue
                            _BalanceAmt = _saleData.psih_invoice_balamt
                        End If
                        _saleData.psih_invoice_shiftno = _saleSetting._curShiftno
                        _saleData.psih_invoice_dayno = _saleSetting._curDayno
                        _saleData.psih_invoice_countername = Environment.MachineName
                        _saleData.psih_invoice_print = "0"
                        _saleData.psih_invoice_webhost = "0"
                        ' Basic validation - check if we have items
                        If GridDataTble_Insert.Rows.Count = 0 Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("No items to update", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return False
                        End If

                        ' Update to database using stored procedure with payment modes

                        Dim salesDetailsList As List(Of SalesDetails) = salesHelper.ConvertDataTableToSalesDetails(GridDataTble_Insert)
                        Dim _errMsgResult As String = ""
                        Dim ReturnBill As String = "0"

                        ' Always use Dictionary method for both single and multiple payments
                        If salesHelper.UpdateSalesBill(_saleData, salesDetailsList, GetPaymentModesDictionary(), _errMsgResult, ReturnBill) = True Then
                            barstatuslastbillno.Caption = ReturnBill
                            _CashDraw.OpenCashdrawer(True)
                            'DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & " Bill Updated", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Dim frmMsgBox As New frmMsgBoxOkOnly
                            Dim msgData = "Total Bill Amount : " & lblnetamt.Text & " Bill Updated - " & ReturnBill
                            frmMsgBox.ShowDialogData(msgData.ToString)
                            ClearNewBill()
                        Else
                            DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & " Bill Not Updated", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If
                    End If
                End If
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show("View Mode Cant Be Save Bill", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
            'If barchkcustomerpole.Checked = True Then
            '    If CustomerPoleOpen(Errstr) = True Then
            '        Dim stpole1 As String = ""
            '        Dim stpole2 As String = ""
            '        stpole1 = "Received RM " & Format(_givenAmt, "###0.00")
            '        stpole2 = "Balance RM " & Format(_BalanceAmt, "###0.00")
            '        If SetCustomerPole(stpole1, stpole2, Errstr) = False Then

            '        End If
            '        CustomerPoleClose(Errstr)
            '    End If
            'End If
            Return True
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message & "Bill Not Processed", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Convert PaymentDetailTable to Dictionary for database operations
    ''' </summary>
    Private Function GetPaymentModesDictionary() As Dictionary(Of Integer, Decimal)
        Try
            Dim paymentModes As New Dictionary(Of Integer, Decimal)()

            For Each paymentRow As DataRow In frmPaymoreII.PaymentDetailTable.Rows
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
#Region "ViewHold"
    Private Sub barbtnViewPendingOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnViewPendingOrder.ItemClick
        Try
            Dim FrmViewPendingOrder As New FrmViewPendingOrder
            FrmViewPendingOrder.ShowDialog()
            If FrmViewPendingOrder.DialogResult = Windows.Forms.DialogResult.OK Then
                If _FunctionKeyBoardModule.gs_keyboardValueInteger > 0 Then
                    BillHoldTokenNo = _FunctionKeyBoardModule.gs_keyboardValueInteger
                    Dim trno As Integer = 0
                    If billHoldHelper.GetHoldDetails(BillHoldTokenNo, trno) Then
                        GetHoldBySalID(trno, "Edit")
                    End If
                End If
                Return
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub barbtntokenno_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtntokenno.ItemClick
        Try
            frmkeytablescaner.ShowDialog()
            If frmkeytablescaner.DialogResult = Windows.Forms.DialogResult.OK Then
                If _FunctionKeyBoardModule.gs_keyboardValueInteger > 0 Then
                    BillHoldTokenNo = _FunctionKeyBoardModule.gs_keyboardValueInteger
                    Dim trno As Integer = 0
                    If billHoldHelper.GetHoldDetails(BillHoldTokenNo, trno) Then
                        GetHoldBySalID(trno, "Edit")
                    End If
                End If
                Return
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function GetHoldBySalID(ByVal Sal_id As Integer, ByRef ModeOfBill As String) As Boolean
        Try
            Dim resData As New DataSet
            If GetHoldByBillLocal(Sal_id, resData, "H") = True Then

                modeBillHold = "Edit"
                _recallHoldBill = True
                If resData.Tables(0).Rows.Count > 0 Then
                    Dim billno = resData.Tables(0).Rows(0)("psih_invoice_trno")
                    lblinvoiceno.Text = billno
                    BillHoldTrno = billno
                    G_SalID = resData.Tables(0).Rows(0)("psih_invoice_id")
                    Dim tokenno = resData.Tables(0).Rows(0)("psih_invoice_token")
                    BillHoldTokenNo = tokenno
                    barstatustoken.Caption = BillHoldTokenNo
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
                        Dim salesPersonId As Integer = If(rData.Table.Columns.Contains("psid_invoice_salesmanid"), Convert.ToInt32(rData("psid_invoice_salesmanid")), 1)
                        Dim salesManPer As Decimal = If(rData.Table.Columns.Contains("psid_invoice_salemanper"), Convert.ToDecimal(rData("psid_invoice_salemanper")), 0)
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
                    If SalesGrandtotal(False) = False Then

                    End If
                End If
            Else
                _recallHoldBill = False
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region
#Region "ViewEdit"
    Private Sub barbtnstockupdate_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnstockupdate.ItemClick
        Try
            frmKeyPassIIIMaster.ShowDialog()
            If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
                Dim stockupdatefrm As New StockUpdate
                stockupdatefrm.ShowDialog()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub barbtnviewbill_Click(sender As Object, e As EventArgs) Handles barbtnviewbill.ItemClick
        Try
            frmKeyPassIIIMaster.ShowDialog()
            If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
                Dim modeofbill As String = ""
                frmSelectBillII.ShowDialog()
                If frmSelectBillII.DialogResult = Windows.Forms.DialogResult.OK Then
                    modeOfSale = "View"
                    If GetSalesBySalID(G_SalID, modeofbill) = True Then
                        modeOfSale = modeofbill
                        barbtnstatus.Caption = "Sales Mode : " & modeOfSale
                    End If
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
                        Dim salesPersonId As Integer = If(rData.Table.Columns.Contains("psid_invoice_salesmanid"), Convert.ToInt32(rData("psid_invoice_salesmanid")), 1)
                        Dim salesManPer As Object = If(rData.Table.Columns.Contains("psid_invoice_salemanper"), rData("psid_invoice_salemanper"), 0)
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
                    If SalesGrandtotal(False) = False Then

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
                    If SalesGrandtotal(False) = False Then

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
            Application.Exit()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub barbtnback_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnback.ItemClick
        Try
            Me.Hide()
            PosLogin.Show()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnlastprint_Click(sender As Object, e As EventArgs) Handles btnlastprint.ItemClick
        Try
            Dim frmMsgBox As New frmMsgBox
            Dim msgData = "Do you to show preview? This Bill - " & barstatuslastbillno.Caption & " ' Preview - Yes '" & vbNewLine & "'Print'"
            frmMsgBox.ShowDialogData(msgData.ToString, True)
            If frmMsgBox.DialogResult = Windows.Forms.DialogResult.Yes Then
                PrintPreview()
            ElseIf frmMsgBox.DialogResult = Windows.Forms.DialogResult.OK Then
                Dim _receDs As New DataSet
                If (GetSalesByBillLocal(barstatuslastbillno.Caption, _receDs, "P")) = True Then
                    If (_receDs.Tables(0).Rows.Count > 0) Then
                        _receDs.WriteXml(M_Details._appPath & "\Reports\Sales.xml", Data.XmlWriteMode.WriteSchema)
                    End If

                    If clsBillPrint.BillPrintMin(_receDs, Errstr, "SalesMinPrint.repx") = True Then
                        '_CashDraw._paperCut(True)
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PrintPreview()
        Try
            Dim _receDs As New DataSet
            If (GetSalesByBillLocal(barstatuslastbillno.Caption, _receDs, "P")) = True Then
                If (_receDs.Tables(0).Rows.Count > 0) Then
                    _receDs.WriteXml(M_Details._appPath & "\Reports\Sales.xml", Data.XmlWriteMode.WriteSchema)
                End If

                If clsBillPrint.BillPrintMinPreivew(_receDs, Errstr, "SalesMinPrint.repx") = True Then

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub barbtnprintprofiledesign_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnprintprofiledesign.ItemClick
        Try
            frmKeyPassIIIMaster.ShowDialog()
            If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
                frmPrintProfile.ShowDialog()
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "ShiftClose/Staff/CashDrawer"
    Private Sub barbtnstafflocationchanges_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnstafflocationchanges.ItemClick
        Try
            frmKeyPassIIIMaster.ShowDialog()
            If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
                FrmStaffLocationChanges.ShowDialog()
            End If
        Catch ex As Exception

        End Try
    End Sub
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
            Else
                LoadButtonStyles()
                LoadMainMenu()
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
            frmKeyPassIIIMaster.ShowDialog()
            If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
                'Dim printcmd As New PrintCommand
                'Dim input As Integer = InputBox("Enter Shift No")
                'If input > 0 Then
                '    printcmd._printShiftClose(input, "F", Date.Now)
                '    sendMailShiftDosMode(input, True, Date.Now)
                'End If
                frmResendMail.ShowDialog()
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "CustomerSerial Port"

    Private Rexstr As String = String.Empty
    Delegate Sub SetTextCallback(ByVal [text] As String) 'Added to prevent threading errors during receiveing of data

    Private Sub barchkcustomerpole_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barchkcustomerpole.ItemClick
        Try
            frmKeyPassIIIMaster.ShowDialog()
            If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
                If CustomerDisplaySettings.Startup = 2 Then
                    barchkcustomerpole.Checked = False
                    DevExpress.XtraEditors.XtraMessageBox.Show("Customer Display Pole is Disable Status on Settings.", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    If barchkcustomerpole.Checked = True Then
                        barchkcustomerpole.Checked = False
                    ElseIf barchkcustomerpole.Checked = False Then
                        barchkcustomerpole.Checked = True
                    End If
                End If
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function CustomerPoleOpen(ByRef ErrorMsg As String) As Boolean
        Try

            If SerialPortCustomerPole.IsOpen Then
                ErrorMsg = "Aready Opened."
                Return False
            End If
            SerialPortCustomerPole.PortName = CustomerDisplaySettings.PortName
            SerialPortCustomerPole.BaudRate = CustomerDisplaySettings.BaudRate

            Select Case CustomerDisplaySettings.Parity
                Case "Even"
                    SerialPortCustomerPole.Parity = Ports.Parity.Even
                Case "None"
                    SerialPortCustomerPole.Parity = Ports.Parity.None
                Case "Mark"
                    SerialPortCustomerPole.Parity = Ports.Parity.Mark
                Case "Odd"
                    SerialPortCustomerPole.Parity = Ports.Parity.Odd
                Case "Space"
                    SerialPortCustomerPole.Parity = Ports.Parity.Space
            End Select

            SerialPortCustomerPole.DataBits = CustomerDisplaySettings.DataBits

            Select Case CustomerDisplaySettings.StopBits
                Case "None"
                    SerialPortCustomerPole.StopBits = 0 ' Ports.StopBits.None
                Case "One"
                    SerialPortCustomerPole.StopBits = 1 ' Ports.StopBits.One
                Case "OnePointFive"
                    SerialPortCustomerPole.StopBits = 2.5 'Ports.StopBits.OnePointFive
                Case "Two"
                    SerialPortCustomerPole.StopBits = 2 ' Ports.StopBits.Two
            End Select
            SerialPortCustomerPole.Open()
            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return False
        End Try
    End Function

    Private Function CustomerPoleClose(ByRef ErrorMsg As String) As Boolean
        Try
            If SerialPortCustomerPole.IsOpen Then
                SerialPortCustomerPole.DtrEnable = True
                SerialPortCustomerPole.Close()
                SerialPortCustomerPole.Dispose()

            End If
            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return False
        End Try
    End Function

    Private Function SetCustomerPole(ByVal Txt1 As String, ByVal Txt2 As String, ByRef ErrorMsg As String) As Boolean
        Try
            'SerialPortCustomerPole.Write(" " & vbCr)
            'SerialPortCustomerPole.WriteLine(_GlobalSettings._ShopName)
            'SerialPortCustomerPole.WriteLine(Txt)

            ''SerialPortCustomerPole.Write(Convert.ToString(ChrW(11)))
            ''SerialPortCustomerPole.WriteLine(_GlobalSettings._ShopName)
            ''SerialPortCustomerPole.WriteLine(ChrW(13) & Txt)

            '' SerialPortCustomerPole.Open()
            SerialPortCustomerPole.Write(Convert.ToString(ChrW(12)))
            SerialPortCustomerPole.WriteLine(Txt1)
            SerialPortCustomerPole.WriteLine(ChrW(13) & Txt2)
            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return False
        End Try
    End Function

    Private Sub btnPoledisplaysetting_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPoledisplaysetting.ItemClick
        Try
            frmKeyPassIIIMaster.ShowDialog()
            If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
                frmcustomerpole.ShowDialog()
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "FingerAttendance"
    Private Sub barbtnfingernewregister_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnfingernewregister.ItemClick
        Try
            frmKeyPassIIIMaster.ShowDialog()
            If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
                FrmFingerRegister.ShowDialog()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnattendance_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnattendance.ItemClick
        Try
            FrmFingerScanner.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "Customer History"
    Private Sub btnCustomerHistory_Click(sender As Object, e As EventArgs) Handles btnCustomerHistory.Click
        Try
            If selectedCustomerId <> 0 AndAlso selectedCustomerId <> 0 Then
                FrmCustomerSalesHis.ShowDialog(selectedCustomerId)
            End If
        Catch ex As Exception
            ' Handle exception if needed
        End Try
    End Sub

#End Region
#Region "Appointment"

    Private Sub barbtnsetappointment_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnsetappointment.ItemClick
        Try
            frmAppointmentBooking.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
#End Region


End Class
