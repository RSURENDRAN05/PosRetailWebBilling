Imports System.Net
Imports Newtonsoft.Json.Linq

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
            GridDataTble_Insert.Columns.Add("DPER", GetType(Decimal)).DefaultValue = 0 '6
            GridDataTble_Insert.Columns.Add("DAMT", GetType(Decimal)).DefaultValue = 0 '7
            GridDataTble_Insert.Columns.Add("GAMOUNT", GetType(Decimal)) '8
            GridDataTble_Insert.Columns.Add("TAXVALUE", GetType(Integer)).DefaultValue = 0 '9
            GridDataTble_Insert.Columns.Add("TAXAMT", GetType(Decimal)).DefaultValue = 0 '11
            GridDataTble_Insert.Columns.Add("NETAMT", GetType(Decimal)) '12
            GridDataTble_Insert.Columns.Add("ITEMREMARS", GetType(String)).DefaultValue = "Notes" '2
            GridDataTble_Insert.Columns.Add("BATCHNO ", GetType(Integer)).DefaultValue = 0 '10
            GridDataTble_Insert.Columns.Add("SALESPERSONID ", GetType(Integer)).DefaultValue = 0 '10
            GridDataTble_Insert.Columns.Add("SALESPERSON", GetType(String)).DefaultValue = "SP" '9
            GridDataTble_Insert.Columns.Add("DELETE ", GetType(Integer)).DefaultValue = 1 '10

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
            InitialLoad()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub InitialLoad()
        Try
            barbtnposstatus.Caption = _companyInfo.ComId & "-" & _companyInfo.CompanyName & "-" & _companyInfo.LocId & "-" & _companyInfo.LocationName
            If _getBillno() = False Then

            End If
            LoadItemMaster()
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
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadItemMaster()
        Try
            If getItemMaster() = True Then
                If _JsonData.ItemMasterTable.Rows.Count > 0 Then
                    GridControl2.DataSource = _JsonData.ItemMasterTable.DefaultView
                    dtview = _JsonData.ItemMasterTable.DefaultView
                End If
            End If
            If getLedgerListTable() = True Then
                If _JsonData.LedgerListTable.Rows.Count > 0 Then
                    'txtclientname.Properties.DataSource = _JsonData.LedgerListTable

                End If
            End If
            '' getClientInfo()
            'If _JsonData.ClientTable.Rows.Count > 0 Then
            '    GridControl1.DataSource = _JsonData.ClientTable
            'End If
        Catch ex As Exception

        End Try
    End Sub
    'Public Function _printProfileLoad() As Boolean
    '    Try
    '        If File.Exists(M_Details.AppPath & "\Settings\PrintProfileSetting.xml") Then
    '            Dim str() As String = {"Sales"}
    '            Dim _dsPrintProfile As New DataSet
    '            _dsPrintProfile.ReadXml(M_Details.AppPath & "\Settings\PrintProfileSetting.xml")
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
    Public Function _getBillno() As Boolean
        Try
            Dim dt As New DataTable
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesRequest=2&BillType=SAL")
            Dim Userparsejson As JObject = JObject.Parse(json)
            Dim dtresults = Userparsejson("Success")
            If dtresults.ToString = "True" Then
                dt = Userparsejson("Data").ToObject(Of DataTable)()
                Dim billno As Integer = 0
                Dim prefix As String = ""
                billno = dt.Rows(0)(1)
                prefix = dt.Rows(0)(2)
                'txtinvoiceno.Text = billno + 1 'prefix & ("0000" & billno + 1)
                Return True
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
            MessageBox.Show(ex.Message)
        End Try
    End Function
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
    Private Sub subMenu()
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub mainMenu()
        Try

        Catch ex As Exception

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


    Private Sub GridViewPOS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridViewPOS.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Dim M_trid = GridViewPOS.GetFocusedRowCellValue("ITEMCODE")
                Dim MaterialeName = GridViewPOS.GetFocusedRowCellValue("ITEMNAME")

                GridViewPOS.DeleteSelectedRows()
                GridDataTble_Insert.AcceptChanges()
                _SnoCount = 0
                For Each _rData In GridDataTble_Insert.Rows
                    _SnoCount = _SnoCount + 1
                    _rData("SNO") = _SnoCount
                Next

                'If SalesGrandtotal("er") = False Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                'End If
                'End If
                'GridViewPOS.DeleteRow(GridViewPOS.FocusedRowHandle)
                ' dtPOS.AcceptChanges()
                'SalesGrandtotal(Errstr)
                cmbMaterialSearch.Focus()
            ElseIf My.Computer.Keyboard.CtrlKeyDown AndAlso e.KeyCode = Keys.Up Then
                cmbMaterialSearch.Focus()
                '' txtbarcodeNo.Focus()
            ElseIf e.KeyCode = Keys.Up AndAlso GridViewPOS.FocusedRowHandle = 0 Then
                cmbMaterialSearch.Focus()
                ''txtbarcodeNo.Focus()
            ElseIf e.KeyCode = Keys.Escape Then
                cmbMaterialSearch.Focus()
            ElseIf e.KeyCode = Keys.Enter Then
                GridViewPOS.CloseEditor()
                cmbMaterialSearch.Focus()
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

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

            If _Mode = "ItemCode" Then

                dtrows = From dtrow As DataRow In _JsonData.ItemMasterTable Where String.Equals(dtrow("BARCODE"), ReceivedProCode, StringComparison.CurrentCultureIgnoreCase)
            ElseIf _Mode = "BarCode" Then

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
            Dim Qty As Integer = 1
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


            GridDataTble_Insert.NewRow()
            GridDataTble_Insert.BeginInit()
            _SnoCount = _SnoCount + 1
            TAmount = Qty * _srate
            GAmount = Qty * _srate
            TaxRetunAmt = _ReturnGst(_taxValue, TAmount)
            If _globalSetting.TaxExculsive = True Then
                NetAmount = TAmount + TaxRetunAmt
            Else
                NetAmount = TAmount
            End If
            GridDataTble_Insert.Rows.Add(_SnoCount, _barcode, _itemcode, Trim(_item), _serialno, _uom, _srate, Qty, TAmount, DisPer, DisAmt, GAmount, _taxValue, TaxRetunAmt, _RoundOff(NetAmount), "Remarks", _batchno, 1, "SaleMan", 1)
            GridDataTble_Insert.AcceptChanges()
            GridDataTble_Insert.EndInit()
            GridControlSalesData.DataSource = GridDataTble_Insert
            GridViewPOS.MoveNext()
            ' GridDataTble_Insert.WriteXml(M_Details._appPath & "Layout\SaleRecentData.xml", True, System.Data.XmlWriteMode.WriteSchema)
            'txtnotes.Text = "-"
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
            Dim DAMT As Decimal = 0.0
            Dim BAMT As Decimal = 0.0
            Dim GAmount As Decimal = 0.0
            Dim GST As Decimal = 0.0
            Dim ServiceChargeAmount As Decimal = 0.0
            Dim NetTot As Decimal = 0.0

            TAmount = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("TAMOUNT"))
            'DPER = GridDataTble_Insert.AsEnumerable().Average(Function(row) row.Field(Of Double)("DPER"))
            DAMT = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("DAMT"))

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

            ' Update UI labels
            lblsubtotal.Text = TAmount.ToString("0.00")
            lblitemdisctotal.Text = (DAMT).ToString("0.00")
            lblssttotal.Text = GST.ToString("0.00")
            lblservchargetotal.Text = ServiceChargeAmount.ToString("0.00")
            ' Display service charge if there's a label for it
            ' If you have a service charge label, uncomment and modify this line:
            ' lblservicecharge.Text = ServiceChargeAmount.ToString("0.00")

            lblnetamt.Text = _RoundOff(NetTot).ToString("0.00")

            'GridViewPOS.MoveLast()
            If GridDataTble_Insert.Rows.Count <> 0 Then
                GridDataTble_Insert.WriteXml(M_Details.AppPath & "Layout\SaleRecentData.xml", True, System.Data.XmlWriteMode.WriteSchema)
            End If
            Return True
        Catch ex As Exception
            ERR = ex.Message
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
    Private k As Decimal = 0.0
    Private KL As Decimal = 0.0
    Private min As Decimal = 0.0
    Private distxtper As Decimal = 0.0
    Private Function DiscountAddTax(ByVal BasicRate As Decimal, ByVal DisPerValue As Decimal, ByVal TaxPerValue As Decimal, ByVal TaxType As Integer, ByRef SalesValue As Decimal, ByRef TaxValue As Decimal, ByRef DiscountValue As Decimal, ByRef ErrorMsg As String) As Boolean
        Try
            Dim j As Decimal = 0.0
            Dim k As Decimal = 0.0
            If TaxType = 1 Then



                'Here Calculate the Discount
                If CalPercentage(BasicRate, DisPerValue, KL, Tax.Percentage2Price, RoundType.DefaultValue, ErrorMsg) = False Then
                    Return False
                End If

                DiscountValue = Format(KL, "0.00")

                j = Format((BasicRate - Format(KL, "0.00")), "0.00")

                KL = j - (j / (TaxPerValue / 100 + 1))

                ''Here Calculate the Tax
                'If CalPercentage(j, TaxPerValue, KL, Tax.Percentage2Price, RoundType.DefaultValue, ErrorMsg) = False Then
                '    Return False
                'End If

                TaxValue = KL
                SalesValue = j 'Format((j + KL), "0.00")

            Else

                'Here Calculate the Discount
                If CalPercentage(BasicRate, DisPerValue, KL, Tax.Percentage2Price, RoundType.DefaultValue, ErrorMsg) = False Then
                    Return False
                End If

                DiscountValue = Format(KL, "0.00")
                j = Format((BasicRate - Format(KL, "0.00")), "0.00")
                'Here Calculate the Tax
                If CalPercentage(j, TaxPerValue, KL, Tax.Percentage2Price, RoundType.DefaultValue, ErrorMsg) = False Then
                    Return False
                End If

                TaxValue = Format(KL, "0.00")
                SalesValue = Format((j), "0.00")

            End If



            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return False
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


End Class
