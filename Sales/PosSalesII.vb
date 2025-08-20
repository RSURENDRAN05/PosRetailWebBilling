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
            GridDataTble_Insert.Columns.Add("BATCHNO ", GetType(Integer)).DefaultValue = 0 '17
            GridDataTble_Insert.Columns.Add("SALESPERSONID ", GetType(Integer)).DefaultValue = 0 '18
            GridDataTble_Insert.Columns.Add("SALESPERSON", GetType(String)).DefaultValue = "SP" '19
            GridDataTble_Insert.Columns.Add("DELETE ", GetType(Integer)).DefaultValue = 1 '20

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
                Dim focusedRowHandle As Integer = GridViewPOS.FocusedRowHandle
                If focusedRowHandle >= 0 AndAlso focusedRowHandle < GridDataTble_Insert.Rows.Count Then
                    ' Show confirmation dialog
                    Dim itemName = GridDataTble_Insert.Rows(focusedRowHandle)("ITEMNAME")
                    Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete '" & itemName & "'?", _
                                                               "Confirm Delete", _
                                                               MessageBoxButtons.YesNo, _
                                                               MessageBoxIcon.Question)
                    If result = DialogResult.Yes Then
                        DeleteSelectedRow(focusedRowHandle)
                    End If
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
                If GridViewPOS.FocusedColumn IsNot Nothing AndAlso GridViewPOS.FocusedColumn.FieldName = "DELETE " Then
                    ' Delete focused row
                    Dim focusedRowHandle As Integer = GridViewPOS.FocusedRowHandle
                    If focusedRowHandle >= 0 AndAlso focusedRowHandle < GridDataTble_Insert.Rows.Count Then
                        Dim itemName = GridDataTble_Insert.Rows(focusedRowHandle)("ITEMNAME")
                        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete '" & itemName & "'?", _
                                                                   "Confirm Delete", _
                                                                   MessageBoxButtons.YesNo, _
                                                                   MessageBoxIcon.Question)
                        If result = DialogResult.Yes Then
                            DeleteSelectedRow(focusedRowHandle)
                        End If
                    End If
                Else
                    GridViewPOS.CloseEditor()
                    cmbMaterialSearch.Focus()
                End If
                ' Quantity control shortcuts
            ElseIf e.KeyCode = Keys.Add OrElse (My.Computer.Keyboard.ShiftKeyDown AndAlso e.KeyCode = Keys.Oemplus) Then
                ' + key: Add 1 to quantity
                AddQuantityToItem(1)
                e.Handled = True
            ElseIf e.KeyCode = Keys.Subtract OrElse e.KeyCode = Keys.OemMinus Then
                ' - key: Subtract 1 from quantity
                SubtractQuantityFromItem(1)
                e.Handled = True
            ElseIf My.Computer.Keyboard.CtrlKeyDown AndAlso e.KeyCode = Keys.Q Then
                ' Ctrl+Q: Open quantity dialog
                btnqty_Click(Nothing, Nothing)
                e.Handled = True
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    ' Handle DELETE column click to delete row
    Private Sub GridViewPOS_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridViewPOS.CellValueChanged
        Try
            ' Check if the changed column is the DELETE column
            If e.Column.FieldName = "DELETE " AndAlso e.Value IsNot Nothing Then
                ' If user clicked/changed the DELETE column value, delete the row
                If Convert.ToInt32(e.Value) = 0 OrElse Convert.ToInt32(e.Value) = 1 Then
                    DeleteSelectedRow(e.RowHandle)
                End If
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show("Error in delete operation: " & ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Alternative method - Handle mouse click on DELETE column
    Private Sub GridViewPOS_MouseDown(sender As Object, e As MouseEventArgs) Handles GridViewPOS.MouseDown
        Try
            If e.Button = MouseButtons.Left Then
                Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                Dim hi As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = view.CalcHitInfo(e.Location)

                ' Check if click is on a cell in the DELETE column
                If hi.InRowCell AndAlso hi.Column IsNot Nothing AndAlso hi.Column.FieldName = "DELETE " Then
                    ' Show confirmation dialog
                    Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this item?", _
                                                               "Confirm Delete", _
                                                               MessageBoxButtons.YesNo, _
                                                               MessageBoxIcon.Question)
                    If result = DialogResult.Yes Then
                        DeleteSelectedRow(hi.RowHandle)
                    End If
                End If
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show("Error in delete click: " & ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Handle double-click on DELETE column for quick deletion
    Private Sub GridViewPOS_DoubleClick(sender As Object, e As EventArgs) Handles GridViewPOS.DoubleClick
        Try
            ' Check if focused column is DELETE column
            If GridViewPOS.FocusedColumn IsNot Nothing AndAlso GridViewPOS.FocusedColumn.FieldName = "DELETE " Then
                Dim focusedRowHandle As Integer = GridViewPOS.FocusedRowHandle
                If focusedRowHandle >= 0 AndAlso focusedRowHandle < GridDataTble_Insert.Rows.Count Then
                    Dim itemName = GridDataTble_Insert.Rows(focusedRowHandle)("ITEMNAME")
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
            DevExpress.XtraEditors.XtraMessageBox.Show("Error in delete double-click: " & ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            Dim itemCode = GridDataTble_Insert.Rows(rowHandle)("ITEMCODE")
            Dim itemName = GridDataTble_Insert.Rows(rowHandle)("ITEMNAME")

            ' Remove the row from DataTable
            GridDataTble_Insert.Rows.RemoveAt(rowHandle)
            GridDataTble_Insert.AcceptChanges()

            ' Renumber the SNO column
            RenumberSerialNumbers()

            ' Refresh the grid
            GridControlSalesData.DataSource = GridDataTble_Insert

            ' Update grand totals
            SalesGrandtotal("OK")

            ' Optional: Show success message (uncomment if needed)
            ' MessageBox.Show($"Item '{itemName}' deleted successfully.", "Item Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Set focus back to search for next item entry
            cmbMaterialSearch.Focus()

        Catch ex As Exception
            Throw New Exception("Error deleting row: " & ex.Message)
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

    ' Public method to delete current selected row (can be called externally)
    Public Sub DeleteCurrentSelectedRow()
        Try
            Dim focusedRowHandle As Integer = GridViewPOS.FocusedRowHandle
            If focusedRowHandle >= 0 AndAlso focusedRowHandle < GridDataTble_Insert.Rows.Count Then
                Dim itemName = GridDataTble_Insert.Rows(focusedRowHandle)("ITEMNAME")
                Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete '" & itemName & "'?", _
                                                           "Confirm Delete", _
                                                           MessageBoxButtons.YesNo, _
                                                           MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    DeleteSelectedRow(focusedRowHandle)
                End If
            Else
                MessageBox.Show("Please select an item to delete.", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error deleting item: " & ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Public method to delete row by item code
    Public Function DeleteItemByCode(itemCode As Integer) As Boolean
        Try
            For i As Integer = GridDataTble_Insert.Rows.Count - 1 To 0 Step -1
                If Convert.ToInt32(GridDataTble_Insert.Rows(i)("ITEMCODE")) = itemCode Then
                    DeleteSelectedRow(i)
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            MessageBox.Show("Error deleting item by code: " & ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function



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

            ' Add row with new column structure including separate discount columns
            ' Column order: SNO, BARCODE, ITEMCODE, ITEMNAME, SERIALNO, UOM, RATE, QTY, TAMOUNT,
            '              ITEM_DPER, ITEM_DAMT, BILL_DPER, BILL_DAMT, TOTAL_DPER, TOTAL_DAMT,
            '              GAMOUNT, TAXVALUE, TAXAMT, NETAMT, ITEMREMARS, BATCHNO, SALESPERSONID, SALESPERSON, DELETE
            GridDataTble_Insert.Rows.Add(_SnoCount, _barcode, _itemcode, Trim(_item), _serialno, _uom, _srate, Qty, TAmount, _
                                        0, 0, _
                                        DisBPer, DisBAmt, _
                                        DisBPer, DisBAmt, _
                                        GAmount, _taxValue, TaxRetunAmt, _RoundOff(NetAmount), "Remarks", _batchno, 1, "SaleMan", 1)
            GridDataTble_Insert.AcceptChanges()
            GridDataTble_Insert.EndInit()
            GridControlSalesData.DataSource = GridDataTble_Insert
            GridViewPOS.MoveNext()

            ' Reset quantity input to 1 for next item
            txtMqty.EditValue = 1

            ' Show message if bill discount was automatically applied
            ' If billDiscountExists Then
            '     MessageBox.Show($"New item added with existing bill discount of {existingBillDiscountPer:F2}% applied automatically.", _
            '                   "Bill Discount Applied", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' End If

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

            ' Update any additional discount breakdown labels if they exist
            ' You can add these labels to show separate item and bill discounts
            ' lblitemdiscountonly.Text = ItemDiscountAmt.ToString("0.00")
            ' lblbilldiscountonly.Text = BillDiscountAmt.ToString("0.00")

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

                'MessageBox.Show($"Existing bill discount of {billDiscountPer:F2}% applied to new items.", _
                '              "Bill Discount Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error applying bill discount to new items: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

    Private Sub barbtnNewBill_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnNewBill.ItemClick
        Try
            ' Confirm with user if there are items in the current bill
            If GridDataTble_Insert.Rows.Count > 0 Then
                Dim result As DialogResult = MessageBox.Show("Are you sure you want to start a new bill? All current items will be cleared.", _
                                                           "New Bill Confirmation", _
                                                           MessageBoxButtons.YesNo, _
                                                           MessageBoxIcon.Question)
                If result = DialogResult.No Then
                    Exit Sub
                End If
            End If

            ' Clear the current bill and start fresh
            ClearCurrentBill()

            ' Set focus to search field for quick item entry
            cmbMaterialSearch.Focus()

            ' Update status
            MessageBox.Show("New bill started successfully.", "New Bill", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error starting new bill: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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

            ' Get new bill number
            If _getBillno() = False Then
                ' Handle error getting bill number if needed
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

        Catch ex As Exception
            Throw New Exception("Error clearing current bill: " & ex.Message)
        End Try
    End Sub
#Region "QtyControll"
    ' Current quantity being entered via number buttons
    Private currentQtyInput As String = ""

    Private Sub btn_1_Click(sender As Object, e As EventArgs) Handles btn_7.Click, btn_8.Click, btn_9.Click, btn_6.Click, btn_5.Click, btn_4.Click, btn_3.Click, btn_2.Click, btn_1.Click, btn_10.Click, btn_12.Click
        Try
            Dim NUMBTN As Label = CType(sender, Label)
            Dim buttonText As String = NUMBTN.Text.Trim()

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

        Catch ex As Exception
            MessageBox.Show("Error in quantity button click: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Separate event handlers for specific function buttons
    Private Sub btnPlus_Click(sender As Object, e As EventArgs) ' Add this handler to your + button
        Try
            AddQuantityToItem(1)
        Catch ex As Exception
            MessageBox.Show("Error adding quantity: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnMinus_Click(sender As Object, e As EventArgs) ' Add this handler to your - button
        Try
            SubtractQuantityFromItem(1)
        Catch ex As Exception
            MessageBox.Show("Error subtracting quantity: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) ' Add this handler to your Clear button
        Try
            currentQtyInput = ""
            txtMqty.EditValue = 1
        Catch ex As Exception
            MessageBox.Show("Error clearing quantity: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnqty_Click(sender As Object, e As EventArgs) Handles btnqty.Click
        Try
            frmKeyQty.ShowDialog("Enter Qty")
            If frmKeyQty.DialogResult = Windows.Forms.DialogResult.OK Then
                Dim Qty = _FunctionKeyBoardModule.gs_keyboardValueInteger
                If Qty > 0 Then
                    UpdateQuantityForSelectedItem(Qty)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error in quantity entry: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Add quantity to selected item or last item
    Private Sub AddQuantityToItem(addQty As Decimal)
        Try
            Dim targetRowIndex As Integer = GetTargetRowIndex()
            If targetRowIndex >= 0 Then
                UpdateRowQuantity(targetRowIndex, addQty, "ADD")
            Else
                MessageBox.Show("No item available to update quantity.", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error adding quantity: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Subtract quantity from selected item or last item
    Private Sub SubtractQuantityFromItem(subtractQty As Decimal)
        Try
            Dim targetRowIndex As Integer = GetTargetRowIndex()
            If targetRowIndex >= 0 Then
                UpdateRowQuantity(targetRowIndex, subtractQty, "SUBTRACT")
            Else
                MessageBox.Show("No item available to update quantity.", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error subtracting quantity: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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

    ' Quick quantity buttons (can be added to form if needed)
    Public Sub QuickAddQty1()
        AddQuantityToItem(1)
    End Sub

    Public Sub QuickAddQty5()
        AddQuantityToItem(5)
    End Sub

    Public Sub QuickAddQty10()
        AddQuantityToItem(10)
    End Sub

    Public Sub QuickSubtractQty1()
        SubtractQuantityFromItem(1)
    End Sub
#End Region

End Class
