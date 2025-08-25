Imports System.Net
Imports Newtonsoft.Json.Linq
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports System.IO

Public Class PosSales
    Dim Errstr As String
    Dim _SnoCount As Integer = 0
    Dim GridDataTble_Insert As DataTable
    Dim modeOfSale As String = "New"
    Public Function CreateTable() As DataTable
        Try
            GridDataTble_Insert = New DataTable
            GridDataTble_Insert.TableName = "SalesData"
            GridDataTble_Insert.Columns.Add("SNO", GetType(Integer)) '0
            GridDataTble_Insert.Columns.Add("UID", GetType(Integer)) '0
            GridDataTble_Insert.Columns.Add("CODE", GetType(Integer)) '1
            GridDataTble_Insert.Columns.Add("ITEMNAME", GetType(String)) '2
            GridDataTble_Insert.Columns.Add("RATE", GetType(Double)) '3
            GridDataTble_Insert.Columns.Add("QTY", GetType(Double)) '4
            GridDataTble_Insert.Columns.Add("TAMOUNT", GetType(Double)) '5
            GridDataTble_Insert.Columns.Add("DPER", GetType(Double)).DefaultValue = 0 '6
            GridDataTble_Insert.Columns.Add("DAMT", GetType(Double)).DefaultValue = 0 '7
            GridDataTble_Insert.Columns.Add("BPER", GetType(Double)).DefaultValue = 0 '6
            GridDataTble_Insert.Columns.Add("BAMT", GetType(Double)).DefaultValue = 0 '7
            GridDataTble_Insert.Columns.Add("GAMOUNT", GetType(Double)) '8
            GridDataTble_Insert.Columns.Add("TAXVALUE", GetType(Integer)) '9
            GridDataTble_Insert.Columns.Add("TAXINEX", GetType(Integer)) '10
            GridDataTble_Insert.Columns.Add("TAXAMT", GetType(Double)) '11
            GridDataTble_Insert.Columns.Add("NETAMT", GetType(Decimal)) '12
            Return GridDataTble_Insert
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub PosSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialLoad()
        CreateTable()
        GridControl1.DataSource = GridDataTble_Insert
        cmbMaterialSearch.Focus()
        txtclientname.EditValue = _JsonData.LedgerListTable.Rows(0)(0)
        barbtnposstatus.Caption = _companyInfo.ComId & "-" & _companyInfo.CompanyName & "-" & _companyInfo.LocId & "-" & _companyInfo.LocationName
    End Sub
    Private Sub InitialLoad()
        Try
            barbtnstatus.Caption = "Sales Mode : " & modeOfSale
            txtinvoicedate.EditValue = Date.Now.ToString("dd-MM-yyyy")
            txtinvoiceno.Text = "0"
            'getClientInfo()
            'If _JsonData.ClientTable.Rows.Count > 0 Then
            '    GridControl1.DataSource = _JsonData.ClientTable
            'End If
            'If getLedgerListTable() = True Then
            '    If _JsonData.LedgerListTable.Rows.Count > 0 Then
            '        txtclientname.Properties.DataSource = _JsonData.LedgerListTable

            '    End If
            'End If
            If _getBillno() = False Then

            End If
            LoadItemMaster()
            If _printProfileLoad() = False Then

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
                    txtclientname.Properties.DataSource = _JsonData.LedgerListTable

                End If
            End If
            '' getClientInfo()
            'If _JsonData.ClientTable.Rows.Count > 0 Then
            '    GridControl1.DataSource = _JsonData.ClientTable
            'End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function _printProfileLoad() As Boolean
        Try
            If File.Exists(M_Details._appPath & "\Settings\PrintProfileSetting.xml") Then
                Dim str() As String = {"Sales"}
                Dim _dsPrintProfile As New DataSet
                _dsPrintProfile.ReadXml(M_Details._appPath & "\Settings\PrintProfileSetting.xml")
                Dim sas = From profile In _dsPrintProfile.Tables(0).AsEnumerable Where profile.Field(Of String)("ProfileType") = "Sales" Select profile

                Dim tabl As New DataTable
                If sas.Count > 0 Then
                    txtprintprofile.Properties.Items.Clear()
                    tabl = sas.CopyToDataTable
                    For Each _Drows As DataRow In tabl.Rows
                        txtprintprofile.Properties.Items.Add(_Drows("FileName"))
                    Next
                End If
                txtprintprofile.SelectedIndex = 0
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            If _getBillno() = False Then

            End If
            modeOfSale = "New"
            barbtnstatus.Caption = "Sales Mode : " & modeOfSale
            txttaxamt.Text = "0.00"
            txtnetamt.Text = "0.00"
            txttotamount.Text = "0.00"
            txtdiscamt.Text = "0.00"
            _SnoCount = 0
            GridDataTble_Insert.Rows.Clear()
            cmbMaterialSearch.Focus()
        Catch ex As Exception

        End Try
    End Sub
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
                txtinvoiceno.Text = billno + 1 'prefix & ("0000" & billno + 1)
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

                If SalesGrandtotal("er") = False Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
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
            If My.Computer.Keyboard.CtrlKeyDown AndAlso e.KeyCode = Keys.Down Then

                GridViewPOS.Focus()
                GridViewPOS.FocusedColumn = GCItemRate
                cmbMaterialSearch.Focus()
            ElseIf e.KeyCode = Keys.Down Then

                If GridViewPOS.RowCount > 0 Then

                    GridViewPOS.FocusedColumn = GCItemRate
                    GridViewPOS.FocusedRowHandle = 0
                    'GCItemqty.OptionsColumn.AllowEdit = True
                    'GCItemqty.OptionsColumn.AllowFocus = True
                    'GCItemRate.OptionsColumn.AllowEdit = True
                    'GCItemRate.OptionsColumn.AllowFocus = True

                End If
            ElseIf e.KeyCode = Keys.Enter Then

                If String.IsNullOrWhiteSpace(cmbMaterialSearch.Text.ToString) Then
                    cmbMaterialSearch.ShowPopup()
                    txtsearch2.Select()
                    txtsearch2.Text = ""
                    txtMqty.EditValue = 1
                    Exit Sub
                Else
                    Dim str As String
                    str = cmbMaterialSearch.Text
                    If Get_product_info(str, Qty, "ItemCode", Errstr) = False Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        cmbMaterialSearch.Text = ""
                        cmbMaterialSearch.Focus()
                        cmbMaterialSearch.EditValue = Nothing
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
            Dim dtrows As System.Data.EnumerableRowCollection(Of DataRow)

            If _Mode = "ItemCode" Then
                ReceivedProCode = _productCode
                dtrows = From dtrow As DataRow In _JsonData.ItemMasterTable Where String.Equals(dtrow("BARCODE"), ReceivedProCode, StringComparison.CurrentCultureIgnoreCase) 'OrElse String.Equals(dtrow("ItemCode"), ReceivedProCode, StringComparison.CurrentCultureIgnoreCase)
                'dtrow("MBarcode") = Barcode'dtrow.Field(Of String)("MN_ID") = ReceivedProCode Select dtrow   
                If dtrows.Any = False Then
                    cmbMaterialSearch.EditValue = Nothing
                    ErrorMsg = "This Barcode is not Exists" & Environment.NewLine & "Kindly Refresh(F5) the Windows and Try Again."
                    Return False
                Else
                    Dim _Code As String = ""
                    Dim _item As String = ""
                    Dim _barcode As String = ""
                    Dim _wholesalerate As String = ""
                    Dim _srate As String = ""
                    Dim _taxId As String = ""
                    Dim _taxValue As String = ""
                    Dim TaxInEx As Integer = 0
                    If _globalSetting.TaxExculsive = False Then
                        TaxInEx = 0
                    Else
                        TaxInEx = 1
                    End If


                    Dim _dsMaterial As New DataTable
                    _dsMaterial = dtrows.CopyToDataTable
                    If _dsMaterial.Rows.Count > 0 Then
                        For Each _rows In _dsMaterial.Rows
                            _Code = _rows("ITEMCODE")
                            _item = _rows("ITEMNAME")
                            _barcode = _rows("BARCODE")
                            _srate = _rows("SELL")
                            _taxValue = _rows("TAXVALUE")
                            txtnotes.Text = _rows("Remarks")
                        Next
                        _InsertDt(_Code, _item, _srate, _taxValue, TaxInEx)
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
            'eLog.WriteErroLog("Get_product_info" & ex.Message)
        End Try

    End Function
    Public Sub _InsertDt(ByRef _code As Integer, ByRef _items As String, ByRef _srate As Double, ByRef _taxValue As Integer, ByRef _taxInEx As Integer)
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
            Dim _mountProCode As Integer = _code
            Dim _item As String = String.Empty
            If txtnotes.Text <> "-" Then
                _item = _items & vbNewLine & txtnotes.Text
            Else
                _item = _items
            End If

            GridDataTble_Insert.NewRow()
            GridDataTble_Insert.BeginInit()
            _SnoCount = _SnoCount + 1
            TAmount = Qty * _srate
            GAmount = Qty * _srate
            TaxRetunAmt = _ReturnGst(_taxInEx, _taxValue, TAmount)
            If _globalSetting.TaxExculsive = True Then
                NetAmount = TAmount + TaxRetunAmt
            Else
                NetAmount = TAmount
            End If
            GridDataTble_Insert.Rows.Add(_SnoCount, 0, _code, Trim(_item), _srate, Qty, TAmount, DisPer, DisAmt, DisBPer, DisBAmt, GAmount, _taxValue, _taxInEx, TaxRetunAmt, _RoundOff(NetAmount))
            GridDataTble_Insert.AcceptChanges()
            GridDataTble_Insert.EndInit()
            GridControl1.DataSource = GridDataTble_Insert
            GridViewPOS.MoveNext()
            ' GridDataTble_Insert.WriteXml(M_Details._appPath & "Layout\SaleRecentData.xml", True, System.Data.XmlWriteMode.WriteSchema)
            txtnotes.Text = "-"
            If SalesGrandtotal("ER") = False Then

            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function _ReturnGst(ByRef GstType As Integer, ByRef GstValue As Double, ByRef Amount As Double)
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
    Private Sub RepositoryItemTextEditQty_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryItemTextEditQty.KeyDown
        Try
            If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Right Then

                Dim iQty As Decimal = DirectCast(sender, DevExpress.XtraEditors.TextEdit).EditValue
                GridViewPOS.SetFocusedRowCellValue(GCItemqty, iQty)
                QtyEditUpdate(iQty, False)
                GridViewPOS.FocusedColumn = GCItemRate

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub RepositoryItemTextEditRate_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryItemTextEditRate.KeyDown
        Try
            If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Right Then
                Dim iSaleRate As Decimal = DirectCast(sender, DevExpress.XtraEditors.TextEdit).EditValue
                GridViewPOS.SetFocusedRowCellValue(GCItemRate, iSaleRate)
                QtyEditUpdate(iSaleRate, True)
                'GCItemRate.OptionsColumn.AllowFocus = False
                'GCItemRate.OptionsColumn.AllowEdit = False
                GridViewPOS.CloseEditor()
                cmbMaterialSearch.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub RepositoryItemTextEditQty_MouseLeave(sender As Object, e As EventArgs) Handles RepositoryItemTextEditQty.MouseLeave
        Try
            Dim iQty As Decimal = DirectCast(sender, DevExpress.XtraEditors.TextEdit).EditValue
            GridViewPOS.SetFocusedRowCellValue(GCItemqty, iQty)
            QtyEditUpdate(iQty, False)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub RepositoryItemTextEditRate_MouseLeave(sender As Object, e As EventArgs) Handles RepositoryItemTextEditRate.MouseLeave
        Try
            Dim iSaleRate As Decimal = DirectCast(sender, DevExpress.XtraEditors.TextEdit).EditValue
            GridViewPOS.SetFocusedRowCellValue(GCItemRate, iSaleRate)
            QtyEditUpdate(iSaleRate, True)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub barbtnDiscount_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnDiscount.ItemClick
        Try
            frmDiscount.ShowDialog()
            If frmDiscount.DialogResult = Windows.Forms.DialogResult.OK Then
                Dim result As Decimal = 0.0
                Dim amount As Decimal = GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("TAMOUNT")
                GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("DAMT") = 0
                If _discount.DiscountPer = True Then
                    CalPercentage(amount, _discount.discountValue, result, Tax.Percentage2Price, RoundType.DefaultValue, Errstr)
                    GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("DPER") = _discount.discountValue
                Else
                    CalPercentage(amount, _discount.discountValue, result, Tax.Price2Percentage, RoundType.DefaultValue, Errstr)
                End If
                GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("DPER") = result

                DiscountItemWise()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnBilldiscount_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnBilldiscount.ItemClick
        Try
            Dim _disCountItemPer As Double
            frmDiscount.ShowDialog()
            If frmDiscount.DialogResult = Windows.Forms.DialogResult.OK Then
                Dim result As Decimal = 0.0
                Dim amount As Decimal = GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("TAMOUNT")
                GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("BAMT") = 0
                If _discount.DiscountPer = True Then
                    _disCountItemPer = _discount.discountValue / 100
                    Dim count As Integer = GridDataTble_Insert.Rows.Count
                    GridDataTble_Insert.BeginInit()
                    For i As Integer = 0 To count - 1
                        GridDataTble_Insert.Rows(i)("TAMOUNT") = GridDataTble_Insert.Rows(i)("RATE") * GridDataTble_Insert.Rows(i)("QTY")
                        GridDataTble_Insert.Rows(i)("BAMT") = GridDataTble_Insert.Rows(i)("TAMOUNT") * _disCountItemPer
                        GridDataTble_Insert.Rows(i)("BPER") = _disCountItemPer
                        GridDataTble_Insert.Rows(i)("GAMOUNT") = GridDataTble_Insert.Rows(i)("TAMOUNT") - GridDataTble_Insert.Rows(i)("BAMT")
                        GridDataTble_Insert.Rows(i)("TAXAMT") = _ReturnGst(GridDataTble_Insert.Rows(i)("TAXINEX"), GridDataTble_Insert.Rows(i)("TAXVALUE"), GridDataTble_Insert.Rows(i)("GAMOUNT"))
                        If _globalSetting.TaxExculsive = True Then
                            GridDataTble_Insert.Rows(i)("NETAMT") = GridDataTble_Insert.Rows(i)("GAMOUNT") + GridDataTble_Insert.Rows(i)("TAXAMT")
                        Else
                            GridDataTble_Insert.Rows(i)("NETAMT") = GridDataTble_Insert.Rows(i)("GAMOUNT")
                        End If
                    Next
                Else
                    Dim TAmount = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("TAMOUNT"))
                    Dim _DPer = (_discount.discountValue / TAmount)
                    Dim count As Integer = GridDataTble_Insert.Rows.Count
                    GridDataTble_Insert.BeginInit()
                    For i As Integer = 0 To count - 1
                        Dim _TAmtRatexQty = GridDataTble_Insert.Rows(i)("RATE") * GridDataTble_Insert.Rows(i)("QTY")
                        GridDataTble_Insert.Rows(i)("TAMOUNT") = _TAmtRatexQty
                        GridDataTble_Insert.Rows(i)("BAMT") = _TAmtRatexQty * _DPer
                        GridDataTble_Insert.Rows(i)("BPER") = _DPer
                        Dim _DAmt = GridDataTble_Insert.Rows(i)("BAMT")
                        Dim _Grosamt = _TAmtRatexQty - _DAmt
                        GridDataTble_Insert.Rows(i)("GAMOUNT") = _Grosamt
                        Dim _TAxamt = _ReturnGst(GridDataTble_Insert.Rows(i)("TAXINEX"), GridDataTble_Insert.Rows(i)("TAXVALUE"), _Grosamt)
                        GridDataTble_Insert.Rows(i)("TAXAMT") = _TAxamt
                        If _globalSetting.TaxExculsive = True Then
                            GridDataTble_Insert.Rows(i)("NETAMT") = _Grosamt + _TAxamt
                        Else
                            GridDataTble_Insert.Rows(i)("NETAMT") = _Grosamt
                        End If
                    Next
                End If
                GridDataTble_Insert.AcceptChanges()
                GridDataTble_Insert.EndInit()
                SalesGrandtotal(Errstr)
                '_salesDiscount.R_disper = 0.0
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub barbtndiscountclear_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtndiscountclear.ItemClick
        Try
            Dim count As Integer = GridDataTble_Insert.Rows.Count
            For i As Integer = 0 To count - 1
                GridDataTble_Insert.Rows(i)("TAMOUNT") = GridDataTble_Insert.Rows(i)("RATE") * GridDataTble_Insert.Rows(i)("QTY")
                GridDataTble_Insert.Rows(i)("BAMT") = 0
                GridDataTble_Insert.Rows(i)("DAMT") = 0
                GridDataTble_Insert.Rows(i)("DPER") = 0
                GridDataTble_Insert.Rows(i)("BPER") = 0
                GridDataTble_Insert.Rows(i)("GAMOUNT") = GridDataTble_Insert.Rows(i)("TAMOUNT")
                GridDataTble_Insert.Rows(i)("TAXAMT") = _ReturnGst(GridDataTble_Insert.Rows(i)("TAXINEX"), GridDataTble_Insert.Rows(i)("TAXVALUE"), GridDataTble_Insert.Rows(i)("GAMOUNT"))
                If _globalSetting.TaxExculsive = True Then
                    GridDataTble_Insert.Rows(i)("NETAMT") = GridDataTble_Insert.Rows(i)("GAMOUNT") + GridDataTble_Insert.Rows(i)("TAXAMT")
                Else
                    GridDataTble_Insert.Rows(i)("NETAMT") = GridDataTble_Insert.Rows(i)("GAMOUNT")
                End If
            Next
            GridDataTble_Insert.AcceptChanges()
            GridDataTble_Insert.EndInit()
            SalesGrandtotal(Errstr)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub QtyEditUpdate(ByRef _IqtyEdit As Decimal, ByRef _Edit As Boolean)
        Try
            Dim salesrate As Decimal
            If _Edit = True Then
                salesrate = GridViewPOS.GetFocusedRowCellValue("QTY") * _IqtyEdit
            Else
                salesrate = GridViewPOS.GetFocusedRowCellValue("RATE") * _IqtyEdit
            End If

            GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("TAMOUNT") = salesrate
            GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("GAMOUNT") = salesrate

            Dim DValue As Decimal = 0.0
            Dim TValue As Decimal = 0.0
            Dim v As Decimal = 0.0
            Dim j As Decimal = 0.0
            'GridViewPOS.GetFocusedRowCellValue("FixedPrice")
            If DiscountAddTax(GridViewPOS.GetFocusedRowCellValue("RATE"), GridViewPOS.GetFocusedRowCellValue("DPER"), GridViewPOS.GetFocusedRowCellValue("TAXVALUE"), GridViewPOS.GetFocusedRowCellValue("TAXINEX"), v, TValue, DValue, Errstr) = False Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

            Else

                'GridViewPOS.SetFocusedRowCellValue("RATE", v)
                GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("DAMT") = Format((Format(DValue, "0.00") * GridViewPOS.GetFocusedRowCellValue("QTY")), "0.00")
                GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("GAMOUNT") = GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("GAMOUNT") - GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("DAMT")
                GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("TAXAMT") = Format((Format(TValue, "0.00") * GridViewPOS.GetFocusedRowCellValue("QTY")), "0.00")
                If _globalSetting.TaxExculsive = True Then
                    GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("NETAMT") = GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("GAMOUNT") + GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("TAXAMT")
                Else
                    GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("NETAMT") = GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("GAMOUNT")
                End If
            End If

            GridViewPOS.UpdateCurrentRow()
            GridDataTble_Insert.AcceptChanges()

            SalesGrandtotal(Errstr)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DiscountItemWise()
        Try

            Dim DValue As Decimal = 0.0
            Dim TValue As Decimal = 0.0
            Dim v As Decimal = 0.0
            Dim j As Decimal = 0.0
            Dim DPerDisc As Decimal = 0.0

            'GridViewPOS.GetFocusedRowCellValue("FixedPrice")
            If DiscountAddTax(GridViewPOS.GetFocusedRowCellValue("RATE"), GridViewPOS.GetFocusedRowCellValue("DPER"), GridViewPOS.GetFocusedRowCellValue("TAXVALUE"), GridViewPOS.GetFocusedRowCellValue("TAXINEX"), v, TValue, DValue, Errstr) = False Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

            Else

                'GridViewPOS.SetFocusedRowCellValue("RATE", v)
                GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("DAMT") = Format((Format(DValue, "0.00") * GridViewPOS.GetFocusedRowCellValue("QTY")), "0.00")
                GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("GAMOUNT") = v 'GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("GAMOUNT") - GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("DAMT")
                GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("TAXAMT") = Format((Format(TValue, "0.00") * GridViewPOS.GetFocusedRowCellValue("QTY")), "0.00")
                If _globalSetting.TaxExculsive = True Then
                    GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("NETAMT") = GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("GAMOUNT") + GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("TAXAMT")
                Else
                    GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("NETAMT") = GridDataTble_Insert.Rows(GridViewPOS.FocusedRowHandle)("GAMOUNT")
                End If
            End If

            GridViewPOS.UpdateCurrentRow()
            GridDataTble_Insert.AcceptChanges()

            SalesGrandtotal(Errstr)
        Catch ex As Exception

        End Try
    End Sub
    Public Function SalesGrandtotal(ByRef ERR As String) As Boolean
        Try

            Dim TAmount As Double = 0.0
            Dim DAMT As Double = 0.0
            Dim BAMT As Double = 0.0
            Dim GAmount As Double = 0.0
            Dim GST As Double = 0.0
            Dim NetTot As Decimal = 0.0
            TAmount = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("TAMOUNT"))
            'DPER = GridDataTble_Insert.AsEnumerable().Average(Function(row) row.Field(Of Double)("DPER"))
            DAMT = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("DAMT"))
            BAMT = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("BAMT"))
            GAmount = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("GAMOUNT"))
            GST = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("TAXAMT"))
            If _globalSetting.TaxExculsive = True Then
                NetTot = GAmount + GST
            Else
                lbltax.Text = "SST: " & Format(GAmount - GST, "#####.00")
                NetTot = GAmount
            End If
            txttotamount.Text = TAmount.ToString("0.00")
            txtdiscamt.Text = (DAMT + BAMT).ToString("0.00")
            txttaxamt.Text = GST.ToString("0.00")
            txtnetamt.Text = _RoundOff(NetTot).ToString("0.00")
            'GridViewPOS.MoveLast()
            If GridDataTble_Insert.Rows.Count <> 0 Then
                GridDataTble_Insert.WriteXml(M_Details._appPath & "Layout\SaleRecentData.xml", True, System.Data.XmlWriteMode.WriteSchema)
            End If
            Return True
        Catch ex As Exception
            Return False
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
#Region "Sales Payment"
    Private Sub btnCash_Click(sender As Object, e As EventArgs) Handles btnCash.Click
        Try

            If modeOfSale = "New" OrElse modeOfSale = "Quote" Then
                If GridViewPOS.RowCount > 0 Then
                    frmPaymore.ShowDialog(txtnetamt.Text, txtclientname.EditValue)
                    Dim _givenAmt As Decimal = 0.0
                    Dim _BalanceAmt As Decimal = 0.0
                    If frmPaymore.DialogResult = Windows.Forms.DialogResult.OK Then
                        Dim _saleData As New SalesHeader
                        If txtclientname.EditValue Is Nothing OrElse txtclientname.Text = "" Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("Client Not Selected", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else
                            _saleData.psih_invoice_customerid = txtclientname.EditValue
                            _saleData.psih_invoice_description = txtclientname.Text
                        End If

                        _saleData.psih_invoice_tqty = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("QTY"))
                        _saleData.psih_invoice_tamount = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("TAMOUNT"))
                        _saleData.psih_invoice_titemdisper = GridDataTble_Insert.AsEnumerable().Average(Function(row) row.Field(Of Double)("DPER"))
                        _saleData.psih_invoice_titemdisamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("DAMT"))
                        _saleData.psih_invoice_tbilldiscper = GridDataTble_Insert.AsEnumerable().Average(Function(row) row.Field(Of Double)("BPER"))
                        _saleData.psih_invoice_tbilldiscamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("BAMT"))
                        _saleData.psih_invoice_tgrossamt = txttotamount.Text
                        _saleData.psih_invoice_ttaxamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("TAXAMT"))
                        _saleData.psih_invoice_tnetamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("NETAMT"))
                        _saleData.psih_invoice_saletype = "Invoice"
                        _saleData.psih_invoice_billtype = _PaymentDtl.paymentModeSelection
                        _saleData.psih_invoice_paymode = _PaymentDtl.paymentMode
                        Dim invoicedate As String = ""
                        _DateConversion(txtinvoicedate.EditValue, invoicedate)
                        _saleData.psih_invoice_date = invoicedate
                        If _PaymentDtl.paymentModeSelection = "" Then
                            _PaymentDtl.paymentModeSelection = "Cash Bill"
                            _saleData.psih_invoice_billtype = _PaymentDtl.paymentModeSelection
                        End If
                        If _PaymentDtl.paymentMode = "cash" Then
                            _saleData.psih_invoice_billstatus = "Closed"
                        ElseIf _PaymentDtl.paymentMode = "credit" Then
                            _saleData.psih_invoice_billstatus = "Open"
                        ElseIf _PaymentDtl.paymentMode = "card" Then
                            _saleData.psih_invoice_billstatus = "Closed"
                        End If
                        If txtattenname.Text = "" Then
                            _saleData.psih_invoice_billremarks = "-"
                        Else
                            _saleData.psih_invoice_billremarks = txtattenname.Text
                        End If

                        If frmPaymore.txtadvanceamt.EditValue > 0 And _PaymentDtl.paymentMode = "cash" Then
                            _saleData.psih_invoice_advamt = 0
                            DevExpress.XtraEditors.XtraMessageBox.Show("Advance Amount Can't Be Accepted For Cash Bill,", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else
                            If frmPaymore.txtadvanceamt.EditValue > 0 Then
                                _saleData.psih_invoice_advamt = frmPaymore.txtadvanceamt.EditValue
                                _saleData.psih_invoice_outstanding = _saleData.psih_invoice_tnetamt - _saleData.psih_invoice_advamt
                            ElseIf _PaymentDtl.paymentMode = "credit" Then
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
                        _saleData.psih_invoice_userid = _companyInfo.UserId
                        _saleData.psih_invoice_comid = _companyInfo.ComId
                        _saleData.psih_invoice_locid = _companyInfo.LocId
                        Dim jsondtl As String = JsonConvert.SerializeObject(GridDataTble_Insert)
                        Dim jsonhdr As String = JsonConvert.SerializeObject(_saleData)
                        Dim _errMsgResult As String = ""
                        If _JsonSend(M_Details.LinkAjaxRequest & "SalesRequest=4&dtl=" & jsondtl & "&hdr=" & jsonhdr, _errMsgResult) = True Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & "Bill Saved", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            btnNew_Click(Nothing, Nothing)
                            btnPrint_Click(Nothing, Nothing)
                        Else
                            DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & "Bill Not Saved", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If
            ElseIf modeOfSale = "Edit" Then
                If GridViewPOS.RowCount > 0 Then
                    frmPaymore.ShowDialog(txtnetamt.Text, txtclientname.EditValue)
                    Dim _givenAmt As Decimal = 0.0
                    Dim _BalanceAmt As Decimal = 0.0
                    If frmPaymore.DialogResult = Windows.Forms.DialogResult.OK Then
                        Dim _saleData As New SalesHeader
                        If txtclientname.EditValue Is Nothing OrElse txtclientname.Text = "" Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("Client Not Selected", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else
                            _saleData.psih_invoice_customerid = txtclientname.EditValue
                            _saleData.psih_invoice_description = txtclientname.Text
                        End If
                        _saleData.psih_invoice_id = G_SalID
                        _saleData.psih_invoice_trno = txtinvoiceno.Text
                        _saleData.psih_invoice_tqty = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("QTY"))
                        _saleData.psih_invoice_tamount = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("TAMOUNT"))
                        _saleData.psih_invoice_titemdisper = GridDataTble_Insert.AsEnumerable().Average(Function(row) row.Field(Of Double)("DPER"))
                        _saleData.psih_invoice_titemdisamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("DAMT"))
                        _saleData.psih_invoice_tbilldiscper = GridDataTble_Insert.AsEnumerable().Average(Function(row) row.Field(Of Double)("BPER"))
                        _saleData.psih_invoice_tbilldiscamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("BAMT"))
                        _saleData.psih_invoice_tgrossamt = txttotamount.Text
                        _saleData.psih_invoice_ttaxamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("TAXAMT"))
                        _saleData.psih_invoice_tnetamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("NETAMT"))
                        _saleData.psih_invoice_saletype = "Invoice"
                        _saleData.psih_invoice_paymode = _PaymentDtl.paymentMode
                        _saleData.psih_invoice_billtype = _PaymentDtl.paymentModeSelection
                        Dim invoicedate As String = ""
                        _DateConversion(txtinvoicedate.EditValue, invoicedate)
                        _saleData.psih_invoice_date = invoicedate
                        If _PaymentDtl.paymentMode = "cash" Then
                            _saleData.psih_invoice_billstatus = "Closed"
                        ElseIf _PaymentDtl.paymentMode = "credit" Then
                            _saleData.psih_invoice_billstatus = "Open"
                        ElseIf _PaymentDtl.paymentMode = "card" Then
                            _saleData.psih_invoice_billstatus = "Closed"
                        End If
                        _saleData.psih_invoice_billremarks = txtattenname.Text
                        If frmPaymore.txtadvanceamt.EditValue > 0 And _PaymentDtl.paymentMode = "cash" Then
                            _saleData.psih_invoice_advamt = 0
                            DevExpress.XtraEditors.XtraMessageBox.Show("Advance Amount Can't Be Accepted For Cash Bill,", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else
                            If frmPaymore.txtadvanceamt.EditValue > 0 Then
                                _saleData.psih_invoice_advamt = frmPaymore.txtadvanceamt.EditValue
                                _saleData.psih_invoice_outstanding = _saleData.psih_invoice_tnetamt - _saleData.psih_invoice_advamt
                            ElseIf _PaymentDtl.paymentMode = "credit" Then
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
                        _saleData.psih_invoice_userid = _companyInfo.UserId
                        _saleData.psih_invoice_comid = _companyInfo.ComId
                        _saleData.psih_invoice_locid = _companyInfo.LocId
                        Dim jsondtl As String = JsonConvert.SerializeObject(GridDataTble_Insert)
                        Dim jsonhdr As String = JsonConvert.SerializeObject(_saleData)
                        Dim _errMsgResult As String = ""
                        If _JsonSend(M_Details.LinkAjaxRequest & "SalesRequest=7&dtl=" & jsondtl & "&hdr=" & jsonhdr, _errMsgResult) = True Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & "Bill Saved", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            btnNew_Click(Nothing, Nothing)
                            btnPrint_Click(Nothing, Nothing)
                        Else
                            DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & "Bill Not Saved", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
    Private Sub btnexit_Click(sender As Object, e As EventArgs) Handles btnexit.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Try
            Dim modeofbill As String = ""
            frmSelectBill.ShowDialog()
            If frmSelectBill.DialogResult = Windows.Forms.DialogResult.OK Then
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
                    txtinvoiceno.Text = billno
                    Dim CustomerId = billHdr.Rows(0)("psih_invoice_customerid")
                    txtclientname.EditValue = CustomerId
                    Dim Remarks = billHdr.Rows(0)("psih_invoice_billremarks")
                    txtattenname.Text = Remarks
                    barbtnbilltype.Caption = "Bill Type : " & billHdr.Rows(0)("psih_invoice_billtype")
                End If
                If billDtl.Rows.Count > 0 Then
                    _SnoCount = 0
                    GridDataTble_Insert.Rows.Clear()
                    GridDataTble_Insert.NewRow()
                    GridDataTble_Insert.BeginInit()
                    For Each rData In billDtl.Rows
                        _SnoCount = _SnoCount + 1
                        GridDataTble_Insert.Rows.Add(_SnoCount, rData("psid_invoice_id"), rData("psid_invoice_procode"), Trim(rData("psid_invoice_description")), rData("psid_invoice_rate"), rData("psid_invoice_proqty"), rData("psid_invoice_amt"), rData("psid_invoice_itemdisp"), rData("psid_invoice_itemdisamt"), rData("psid_invoice_billdisp"), rData("psid_invoice_billdisamt"), rData("psid_invoice_gross"), rData("psid_invoice_taxvalue"), rData("psid_invoice_taxinex"), rData("psid_invoice_taxamt"), _RoundOff(rData("psid_invoice_netamt")))
                    Next
                    GridDataTble_Insert.AcceptChanges()
                    GridDataTble_Insert.EndInit()
                    GridControl1.DataSource = GridDataTble_Insert
                    GridViewPOS.MoveNext()

                    ' GridDataTble_Insert.WriteXml(M_Details._appPath & "Layout\SaleRecentData.xml", True, System.Data.XmlWriteMode.WriteSchema)
                    txtnotes.Text = "-"
                    If SalesGrandtotal("ER") = False Then

                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
   
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            If modeOfSale = "View" Then
                modeOfSale = "Edit"
                barbtnstatus.Caption = "Sales Mode : " & modeOfSale
            ElseIf modeOfSale = "Quote" Then
                modeOfSale = "New"
                barbtnstatus.Caption = "Sales Mode : " & modeOfSale
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show("New Mode Cant Be Save Bill", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
           
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            btnNew_Click(e, e)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim _receDs As New DataSet
            If (GetSalesByBill(txtinvoiceno.EditValue - 1, _receDs)) = True Then
                If (_receDs.Tables(0).Rows.Count > 0) Then
                    _receDs.WriteXml(M_Details._appPath & "\Reports\Sales.xml", Data.XmlWriteMode.WriteSchema)
                End If

                If clsBillPrint.Billa4Print(_receDs, Errstr, txtprintprofile.Text) = False Then

                End If
                End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnbarrefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnbarrefresh.ItemClick
        Try
            LoadItemMaster()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub barprintprofile_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barprintprofile.ItemClick
        Try
            frmPrintProfile.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Try
            'FrmNewCustomer.ShowDialog()
            LoadItemMaster()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Try
            FrmItemMaster.ShowDialog()
            LoadItemMaster()
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "Quotation"
    Private Sub btnQuotations_Click(sender As Object, e As EventArgs) Handles btnQuotations.Click
        Try
            If modeOfSale = "New" Then
                If GridViewPOS.RowCount > 0 Then
                    Dim dialogResult = MessageBox.Show("Save As Quote Bill", "Quote", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    Dim _givenAmt As Decimal = 0.0
                    Dim _BalanceAmt As Decimal = 0.0
                    If dialogResult = Windows.Forms.DialogResult.Yes Then
                        Dim _saleData As New SalesHeader
                        If txtclientname.EditValue Is Nothing OrElse txtclientname.Text = "" Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("Client Not Selected", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else
                            _saleData.psih_invoice_customerid = txtclientname.EditValue
                            _saleData.psih_invoice_description = txtclientname.Text
                        End If
                        _saleData.psih_invoice_tqty = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("QTY"))
                        _saleData.psih_invoice_tamount = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("TAMOUNT"))
                        _saleData.psih_invoice_titemdisper = GridDataTble_Insert.AsEnumerable().Average(Function(row) row.Field(Of Double)("DPER"))
                        _saleData.psih_invoice_titemdisamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("DAMT"))
                        _saleData.psih_invoice_tbilldiscper = GridDataTble_Insert.AsEnumerable().Average(Function(row) row.Field(Of Double)("BPER"))
                        _saleData.psih_invoice_tbilldiscamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("BAMT"))
                        _saleData.psih_invoice_tgrossamt = txttotamount.Text
                        _saleData.psih_invoice_ttaxamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("TAXAMT"))
                        _saleData.psih_invoice_tnetamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("NETAMT"))
                        _saleData.psih_invoice_saletype = "Quotation"
                        _saleData.psih_invoice_billtype = "Quote"
                        _saleData.psih_invoice_paymode = "Quotation"
                        Dim invoicedate As String = ""
                        _DateConversion(txtinvoicedate.EditValue, invoicedate)
                        _saleData.psih_invoice_date = invoicedate
                        _saleData.psih_invoice_billstatus = "Open"
                        If txtattenname.Text = "" Then
                            _saleData.psih_invoice_billremarks = "-"
                        Else
                            _saleData.psih_invoice_billremarks = txtattenname.Text
                        End If
                        _saleData.psih_invoice_advamt = 0
                        _saleData.psih_invoice_outstanding = 0
                        _saleData.psih_invoice_givenamt = 0
                        _saleData.psih_invoice_balamt = 0
                        _saleData.psih_invoice_userid = _companyInfo.UserId
                        _saleData.psih_invoice_comid = _companyInfo.ComId
                        _saleData.psih_invoice_locid = _companyInfo.LocId
                        Dim jsondtl As String = JsonConvert.SerializeObject(GridDataTble_Insert)
                        Dim jsonhdr As String = JsonConvert.SerializeObject(_saleData)
                        Dim _errMsgResult As String = ""
                        If _JsonSend(M_Details.LinkAjaxRequest & "SalesRequest=9&dtl=" & jsondtl & "&hdr=" & jsonhdr, _errMsgResult) = True Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & "Quotation Saved", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            btnNew_Click(Nothing, Nothing)
                            btnPrint_Click(Nothing, Nothing)
                        Else
                            DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & "Quote Not Saved", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If
            ElseIf modeOfSale = "Edit" Then
                If GridViewPOS.RowCount > 0 Then
                    Dim dialogResult = MessageBox.Show("Update As Quote Bill", "Quote", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    Dim _givenAmt As Decimal = 0.0
                    Dim _BalanceAmt As Decimal = 0.0
                    If dialogResult = Windows.Forms.DialogResult.Yes Then
                        Dim _saleData As New SalesHeader
                        If txtclientname.EditValue Is Nothing OrElse txtclientname.Text = "" Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("Client Not Selected", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else
                            _saleData.psih_invoice_customerid = txtclientname.EditValue
                            _saleData.psih_invoice_description = txtclientname.Text
                        End If
                        _saleData.psih_invoice_id = G_SalID
                        _saleData.psih_invoice_trno = txtinvoiceno.Text
                        _saleData.psih_invoice_tqty = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("QTY"))
                        _saleData.psih_invoice_tamount = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("TAMOUNT"))
                        _saleData.psih_invoice_titemdisper = GridDataTble_Insert.AsEnumerable().Average(Function(row) row.Field(Of Double)("DPER"))
                        _saleData.psih_invoice_titemdisamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("DAMT"))
                        _saleData.psih_invoice_tbilldiscper = GridDataTble_Insert.AsEnumerable().Average(Function(row) row.Field(Of Double)("BPER"))
                        _saleData.psih_invoice_tbilldiscamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("BAMT"))
                        _saleData.psih_invoice_tgrossamt = txttotamount.Text
                        _saleData.psih_invoice_ttaxamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Double)("TAXAMT"))
                        _saleData.psih_invoice_tnetamt = GridDataTble_Insert.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("NETAMT"))
                        _saleData.psih_invoice_saletype = "Quotation"
                        _saleData.psih_invoice_billtype = "Quote"
                        _saleData.psih_invoice_paymode = "Quotation"
                        Dim invoicedate As String = ""
                        _DateConversion(txtinvoicedate.EditValue, invoicedate)
                        _saleData.psih_invoice_date = invoicedate
                        _saleData.psih_invoice_billstatus = "Open"
                        If txtattenname.Text = "" Then
                            _saleData.psih_invoice_billremarks = "-"
                        Else
                            _saleData.psih_invoice_billremarks = txtattenname.Text
                        End If
                        _saleData.psih_invoice_advamt = 0
                        _saleData.psih_invoice_outstanding = 0
                        _saleData.psih_invoice_givenamt = 0
                        _saleData.psih_invoice_balamt = 0
                        _saleData.psih_invoice_userid = _companyInfo.UserId
                        _saleData.psih_invoice_comid = _companyInfo.ComId
                        _saleData.psih_invoice_locid = _companyInfo.LocId
                        Dim jsondtl As String = JsonConvert.SerializeObject(GridDataTble_Insert)
                        Dim jsonhdr As String = JsonConvert.SerializeObject(_saleData)
                        Dim _errMsgResult As String = ""
                        If _JsonSend(M_Details.LinkAjaxRequest & "SalesRequest=10&dtl=" & jsondtl & "&hdr=" & jsonhdr, _errMsgResult) = True Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & "Bill Saved", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            btnNew_Click(Nothing, Nothing)
                            btnPrint_Click(Nothing, Nothing)
                        Else
                            DevExpress.XtraEditors.XtraMessageBox.Show(_errMsgResult & "Bill Not Saved", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show("View Mode Cant Be Quote Bill", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message & "Quote Not Processed", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
#End Region
End Class
