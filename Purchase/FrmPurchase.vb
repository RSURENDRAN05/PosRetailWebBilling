Imports System.Net
Imports Newtonsoft.Json.Linq
Imports DevExpress.XtraEditors
Imports Newtonsoft.Json

Public Class FrmPurchase
    Private dtview As New DataView
    Private _dsM As DataTable
    Dim _sno As Integer = 0
    Dim purchaseTaxType As Integer = 1
    Dim GridPurchaseTable As DataTable
    Dim GridHdrTable As DataTable
    Dim _dsDtl As DataTable
    Dim _dsHdr As DataTable
    Private Sub FrmPurchase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.StartPosition = FormStartPosition.Manual
            Me.Size = New System.Drawing.Size(PanelScreenWith - 300, PanelScreenHeight - 100)
            Me.Location = New Point(250, 50)
            GridPurchaseTable = New DataTable
            GridPurchaseTable = purchase.CreateTable()
            GridControl1.DataSource = GridPurchaseTable
            GridHdrTable = purchase.CreateHdrTable()
            _DataLoad()
            _Clear()
            _LoadInvoiceNo()
            _ScanClear()
            GridViewPOS.RestoreLayoutFromXml(M_Details.AppPath & "\LayOut\GridPurchaseLayout.xml")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnExit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnExit.ItemClick
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _Clear()
        Try
            txtinvoicedate.Text = Date.Now.ToString("dd/MM/yyyy")
            txtpurchasedate.Text = Date.Now.ToString("dd/MM/yyyy")
            txtitemcode.Text = ""
            txtitemname.Text = ""
            txtrefno.Text = "-"
            txtsell.EditValue = 0.0
            txtdiscper.EditValue = 0.0
            txtdisamt.EditValue = 0.0
            txttotitem.EditValue = 0
            txttotqty.EditValue = 0
            txttotdiscper.EditValue = 0.0
            txttotdiscamt.EditValue = 0.0
            txttaxincamt.EditValue = 0.0
            txttaxexcamt.EditValue = 0.0
            txtgrossamt.EditValue = 0.0
            txttottax.EditValue = 0.0
            txtnetamt.EditValue = 0.0
            txtunit.Text = "Pcs"
            txttaxid.ItemIndex = 0
            GridPurchaseTable.Rows.Clear()
            GridHdrTable.Rows.Clear()
            btnsavePurchase.Caption = "Save"
            
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _ScanClear()
        Try
            cmbMaterialSearch.Text = ""
            txtbarcode.Text = ""
            txtbatch.Text = "-"
            txtitemcode.Text = ""
            txtitemname.Text = ""
            txtsell.EditValue = 0.0
            txtexpire.Text = Date.Now.ToString("dd/MM/yyyy")
            txtpurchaserate.EditValue = 0.0
            txtqty.EditValue = 0.0
            txtamount.EditValue = 0.0
            txtdisamt.EditValue = 0.0
            txtdiscper.EditValue = 0.0
            txtitemnetamt.EditValue = 0.0
            txtcostprice.EditValue = 0.0
            txttaxamt.EditValue = 0.0
            txtunit.Text = "Pcs"
            txttaxid.ItemIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _LoadInvoiceNo()
        Try
            Dim businessTable As DataTable
            businessTable = New DataTable
            businessTable.TableName = "BusinessTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=41&BillType=PUR")
            Dim Userparsejson As JObject = JObject.Parse(json)
            businessTable = Userparsejson("Data").ToObject(Of DataTable)()
            If businessTable.Rows.Count > 0 Then
                Dim ibillno = businessTable.Rows(0)("autono").ToString
                If ibillno = "0" Then
                    txtinvoiceno.Text = Format((ibillno + 1), "0000").ToString
                Else
                    txtinvoiceno.Text = Format((ibillno), "0000").ToString
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub _DataLoad()
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            dialog.Caption = "Connecting To Server"
            getSupplierLedgerMaster()
            dialog.Caption = "Collectiong Supplier Data"
            If _JsonData.SupplierTable.Rows.Count > 0 Then
                txtsupplier.Properties.DataSource = _JsonData.SupplierTable.DefaultView
                txtsupplier.ItemIndex = 0
            Else
                txtsupplier.Properties.DataSource = Nothing
            End If
            Dim businessTable As DataTable
            businessTable = New DataTable
            businessTable.TableName = "BusinessTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=26&groupid=013")
            Dim Userparsejson As JObject = JObject.Parse(json)
            businessTable = Userparsejson("Data").ToObject(Of DataTable)()
            If businessTable.Rows.Count > 0 Then
                dialog.Caption = "Collectiong Payment Type"
                txtpaymenttype.Properties.DataSource = businessTable.DefaultView
                txtpaymenttype.Text = "CASH"
            End If
            Dim TableTax As DataTable
            TableTax = New DataTable
            TableTax.TableName = "TableTax"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim TableTaxjson As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=12")
            Dim TableTaxUserparsejson As JObject = JObject.Parse(TableTaxjson)
            TableTax = TableTaxUserparsejson("Data").ToObject(Of DataTable)()
            If TableTax.Rows.Count > 0 Then
                dialog.Caption = "Collectiong TableTax"
                txttaxid.Properties.DataSource = TableTax.DefaultView
            End If
            Dim UnitTaable As DataTable
            TableTax = New DataTable
            TableTax.TableName = "UnitTaable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim UnitTaablejson As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=35")
            Dim UnitTaableuserparsejson As JObject = JObject.Parse(UnitTaablejson)
            UnitTaable = UnitTaableuserparsejson("Data").ToObject(Of DataTable)()
            If UnitTaable.Rows.Count > 0 Then
                dialog.Caption = "Collectiong TableTax"
                txtunit.Properties.DataSource = UnitTaable.DefaultView
            End If
            If getItemMaster() = True Then
                If _JsonData.ItemMasterTable.Rows.Count > 0 Then
                    GridControl2.DataSource = _JsonData.ItemMasterTable.DefaultView
                    dtview = _JsonData.ItemMasterTable.DefaultView
                End If
            End If
        Catch ex As Exception
            dialog.Caption = ex.Message.ToString
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub

    Private Sub btnclear_Click(sender As Object, e As EventArgs) Handles btnclear.Click
        Try
            _ScanClear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbMaterialSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbMaterialSearch.KeyDown
        Try
            Dim Qty As Decimal = 1.0
            Dim errstr As String = String.Empty
            If My.Computer.Keyboard.CtrlKeyDown AndAlso e.KeyCode = Keys.Down Then

                GridViewPOS.Focus()
                GridViewPOS.FocusedColumn = GridColumnPURRATE
                cmbMaterialSearch.Focus()
            ElseIf e.KeyCode = Keys.Down Then

                If GridViewPOS.RowCount > 0 Then

                    GridViewPOS.FocusedColumn = GridColumnPURRATE
                    GridViewPOS.FocusedRowHandle = 0
                    GridColumnPURQTY.OptionsColumn.AllowEdit = True
                    GridColumnPURQTY.OptionsColumn.AllowFocus = True
                    GridColumnPURRATE.OptionsColumn.AllowEdit = True
                    GridColumnPURRATE.OptionsColumn.AllowFocus = True
                    GridViewPOS.Focus()

                End If
            ElseIf e.KeyCode = Keys.Enter Then

                If String.IsNullOrWhiteSpace(cmbMaterialSearch.Text.ToString) Then
                    cmbMaterialSearch.ShowPopup()
                    txtMqty.EditValue = 1
                    txtsearch2.Text = ""
                    txtsearch2.SelectAll()
                    Exit Sub
                Else
                    Dim str As String
                    str = cmbMaterialSearch.Text
                    If Get_product_info(str, Qty, "BarcodeSplitQty", errstr) = False Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub cmbMaterialSearch_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmbMaterialSearch.QueryPopUp
        txtsearch2.Focus()
    End Sub

    Private Sub cmbMaterialSearch_QueryCloseUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmbMaterialSearch.QueryCloseUp
        txtsearch2.Focus()
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
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GridViewPOS_KeyDown(sender As Object, e As KeyEventArgs) Handles GridViewPOS.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Dim delDtl As New DeleteDtl
                delDtl.ppd_id = GridViewPOS.GetFocusedRowCellValue("PURID")
                delDtl.ppd_trno = txtinvoiceno.EditValue
                delDtl.ppd_itemcode = GridViewPOS.GetFocusedRowCellValue("ITEMCODE")
                delDtl.ppd_barcode = GridViewPOS.GetFocusedRowCellValue("BARCODE")
                delDtl.ppd_qty = GridViewPOS.GetFocusedRowCellValue("PURQTY")
                delDtl.ppd_costprice = GridViewPOS.GetFocusedRowCellValue("PURCOST")
                delDtl.ppd_sellprice = GridViewPOS.GetFocusedRowCellValue("PURSELL")
                delDtl.ppd_comid = _companyInfo.ComId
                delDtl.ppd_locid = _companyInfo.LocId
                Dim postString As String = JsonConvert.SerializeObject(delDtl)
                If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=49&jsonDel=" & postString) = True Then
                    MessageBox.Show("Data Deleted", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    GridViewPOS.DeleteSelectedRows()
                    GridPurchaseTable.AcceptChanges()
                    If SalesGrandtotal("er") = True Then
                        _ScanClear()
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Function OnSearch(ByVal _Barcode As String, ByVal _productCode As Integer, ByRef ErrorMsg As String) As Boolean
        Try
            Dim Qty As Decimal = 1.0
            If txtMqty.EditValue <> 0 Then
                Qty = txtMqty.EditValue
            End If

            Dim PresentQty As Decimal = 0.0
            Dim M As Integer = 0
            If Get_product_info(_Barcode, Qty, "ItemCode", ErrorMsg) = False Then
                DevExpress.XtraEditors.XtraMessageBox.Show(ErrorMsg, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            Dim _M_Trid As Integer
            If _Mode = "BarcodeSplitQty" Then
                dtrows = From dtrow As DataRow In _JsonData.ItemMasterTable Where String.Equals(dtrow("BARCODE"), ReceivedProCode, StringComparison.CurrentCultureIgnoreCase)
                If dtrows.Any = False Then
                    cmbMaterialSearch.EditValue = Nothing
                    ErrorMsg = "This Barcode is not Exists" & Environment.NewLine & "Kindly Refresh(F5) the Windows and Try Again."
                    Return False
                Else
                    _M_Trid = dtrows(0)("ITEMCODE")
                End If
            ElseIf _Mode = "ItemCode" Then
                ReceivedProCode = _productCode
                dtrows = From dtrow As DataRow In _JsonData.ItemMasterTable Where String.Equals(dtrow("ITEMCODE"), ReceivedProCode, StringComparison.CurrentCultureIgnoreCase)
                'dtrow("MBarcode") = Barcode'dtrow.Field(Of String)("MN_ID") = ReceivedProCode Select dtrow   
                If dtrows.Any = False Then
                    cmbMaterialSearch.EditValue = Nothing
                    ErrorMsg = "This Barcode is not Exists" & Environment.NewLine & "Kindly Refresh(F5) the Windows and Try Again."
                    Return False
                Else
                    _M_Trid = dtrows(0)("ITEMCODE")
                    'G_Barcode = dtrows(0)("MBarcode")
                    'GMN_ID = dtrows(0)("MN_ID")
                End If
            End If
            If AddMaterial(_M_Trid) = True Then
                Return True
            Else
                Return False
            End If
            '_dsM = clsdb.GetMeterial(_M_Trid, 1, itemQty)
            'If _dsM IsNot Nothing Then
            '    If _dsM.Tables(0).Rows.Count > 0 Then
            '        If barreturnsales.Checked = True Then
            '            itemQty = itemQty * -1
            '        End If
            'If AddMaterial(itemQty, ErrorMsg) = False Then
            '    Return False
            'End If
            '    End If
            'Else
            '    ErrorMsg = "Error Find on Error in OnSearch." & Environment.NewLine & "Please Check it."
            '    Return False
            'End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function AddMaterial(ByRef _productCode As Integer, Optional ByVal Qty As Decimal = 1.0, Optional ByRef ErrorMsg As String = "") As Boolean
        Try
            Dim purchaseNetamt As Decimal = 0
            Dim purchaseTaxPer As Integer = 0
            Dim purchaseTaxAmt As Decimal = 0

            _dsM = New DataTable
            _dsM.TableName = "ItemTable"
            _dsM = getItemDetails(_productCode)
            txtqty.EditValue = Qty
            If _dsM.Rows.Count > 0 Then

                txtbarcode.Text = _dsM.Rows(0)("BARCODE").ToString
                txtitemcode.Text = _dsM.Rows(0)("ITEMCODE").ToString
                txtitemname.Text = _dsM.Rows(0)("ITEMNAME").ToString
                txtpurchaserate.EditValue = _dsM.Rows(0)("COST").ToString
                txttaxid.EditValue = _dsM.Rows(0)("TAXID").ToString
                purchaseTaxPer = _dsM.Rows(0)("TAXVALUE").ToString
                txtsell.EditValue = _dsM.Rows(0)("SELL").ToString
                txtdiscper.EditValue = 0
                txtdisamt.EditValue = 0
                txtamount.EditValue = Convert.ToDecimal(txtpurchaserate.EditValue) * Qty
                SetPPNetPrice(txtpurchaserate.EditValue, 0, purchaseTaxPer, purchaseTaxType, purchaseNetamt, RoundType.RoundValue, purchaseTaxAmt)
                txttaxamt.EditValue = purchaseTaxAmt
                If purchaseNetamt > 0 Then
                    txtitemnetamt.EditValue = purchaseNetamt
                End If
                Return True
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function getItemDetails(ByRef _productCode As Integer) As DataTable
        Try
            Dim dsM As New DataTable
            dsM.TableName = "ItemDetails"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=43&ItemCode=" & _productCode)
            Dim Userparsejson As JObject = JObject.Parse(json)
            dsM = Userparsejson("Data").ToObject(Of DataTable)()
            Return dsM
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
#Region "QtyAndPurchaseRate"
    Private Sub txtpurchaserate_EditValueChanged(sender As Object, e As EventArgs) Handles txtpurchaserate.EditValueChanged
        Try
            If Calculation() = False Then

            End If
            'Dim purchaseNetamt As Decimal = 0
            'Dim purchaseTaxamt As Decimal = 0
            'Dim purchaseTax As String = ""
            'Dim j As System.Data.DataRowView = txttaxid.GetSelectedDataRow
            'If j Is Nothing Then
            '    purchaseTax = 0
            'Else
            '    Dim r As DataRow = j.Row
            '    purchaseTax = r("TAXVALUE")
            'End If
            'If Convert.ToDecimal(txtpurchaserate.EditValue) > 0 Then
            '    txtamount.EditValue = Convert.ToDecimal(txtqty.EditValue) * Convert.ToDecimal(txtpurchaserate.EditValue)
            'Else
            '    txtamount.EditValue = 0
            'End If
            'SetPPNetPrice(txtpurchaserate.EditValue, txtdiscper.EditValue, purchaseTax, purchaseTaxType, purchaseNetamt, RoundType.RoundValue, purchaseTaxamt)
            'txttaxamt.EditValue = purchaseTaxamt
            'txtcostprice.EditValue = txtpurchaserate.EditValue
            'If purchaseNetamt > 0 Then
            '    txtitemnetamt.EditValue = purchaseNetamt * txtqty.EditValue
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtqty_EditValueChanged(sender As Object, e As EventArgs) Handles txtqty.EditValueChanged
        Try
            If Calculation() = False Then

            End If
            'Dim purchaseNetamt As Decimal = 0
            'Dim purchaseTaxamt As Decimal = 0
            'Dim purchaseDisAmt As Decimal = 0
            'Dim CostPrice As Decimal = 0
            'Dim purchaseTax As String = ""
            'Dim ErrorMsg As String = ""
            'Dim j As System.Data.DataRowView = txttaxid.GetSelectedDataRow
            'If j Is Nothing Then
            '    purchaseTax = 0
            'Else
            '    Dim r As DataRow = j.Row
            '    purchaseTax = r("TAXVALUE")
            'End If
            'If Convert.ToDecimal(txtpurchaserate.EditValue) And Convert.ToDecimal(txtqty.EditValue) > 0 Then
            '    txtamount.EditValue = Convert.ToDecimal(txtqty.EditValue) * Convert.ToDecimal(txtpurchaserate.EditValue)
            'Else
            '    txtamount.EditValue = 0
            'End If
            ''SetPPNetPrice(txtpurchaserate.EditValue, txtdiscper.EditValue, purchaseTax, purchaseTaxType, purchaseNetamt, RoundType.RoundValue, purchaseTaxamt)
            'DiscountAddTax(txtpurchaserate.EditValue, txtdiscper.EditValue, purchaseTax, purchaseTaxType, CostPrice, purchaseTaxamt, purchaseDisAmt, ErrorMsg, Tax.Percentage2Price)
            'txtcostprice.EditValue = CostPrice
            'txtdisamt.EditValue = purchaseDisAmt * txtqty.EditValue
            'txttaxamt.EditValue = purchaseTaxamt * txtqty.EditValue
            'If purchaseTaxType = 1 Then
            '    txtitemnetamt.EditValue = Convert.ToDecimal(CostPrice + purchaseTaxamt) * txtqty.EditValue
            'Else
            '    txtitemnetamt.EditValue = Convert.ToDecimal(CostPrice) * txtqty.EditValue
            'End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "DiscountPerAndDiscountAmt"
    Private Sub txtdiscper_EditValueChanged(sender As Object, e As EventArgs) Handles txtdiscper.EditValueChanged
        Try
            If Calculation() = False Then

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtdisamt_EditValueChanged(sender As Object, e As EventArgs) Handles txtdisamt.EditValueChanged
        Try
            Dim disper As Decimal = 0.0
            disper = (txtdisamt.EditValue / txtamount.EditValue) * 100
            If disper = 0.0 Then
                txtdiscper.EditValue = 0.0
            End If
            'If Calculation() = False Then

            'End If
            'Dim purchaseTaxValue As String = ""
            'Dim CostPrice As Decimal = 0
            'Dim purchaseTaxAmt As Decimal = 0
            'Dim purchaseDisAmt As Decimal = 0
            'Dim ErrorMsg As String = ""
            'Dim j As System.Data.DataRowView = txttaxid.GetSelectedDataRow
            'If j Is Nothing Then
            '    purchaseTaxValue = 0
            'Else
            '    Dim r As DataRow = j.Row
            '    purchaseTaxValue = r("TAXVALUE")
            'End If
            'DiscountAddTax(txtpurchaserate.EditValue, txtdiscper.EditValue, purchaseTaxValue, purchaseTaxType, CostPrice, purchaseTaxAmt, purchaseDisAmt, ErrorMsg, Tax.Price2Percentage)
            'txtcostprice.EditValue = CostPrice
            ''txtdisamt.EditValue = purchaseDisAmt * txtqty.EditValue
            'txttaxamt.EditValue = purchaseTaxAmt * txtqty.EditValue
            'If purchaseTaxType = 1 Then
            '    txtitemnetamt.EditValue = Convert.ToDecimal(CostPrice + purchaseTaxAmt) * txtqty.EditValue
            'Else
            '    txtitemnetamt.EditValue = Convert.ToDecimal(CostPrice) * txtqty.EditValue
            'End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txttaxid_EditValueChanged(sender As Object, e As EventArgs) Handles txttaxid.EditValueChanged
        Try
            If Calculation() = False Then

            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "CalucationOFNetamt"
    Public Function Calculation() As Boolean
        Try
            Dim purchaseTaxValue As Decimal = 0.0
            Dim CostPrice As Decimal = 0.0
            Dim purchaseTaxAmt As Decimal = 0.0
            Dim purchaseDisAmt As Decimal = 0.0
            Dim ErrorMsg As String = ""
            Dim j As System.Data.DataRowView = txttaxid.GetSelectedDataRow
            If j Is Nothing Then
                purchaseTaxValue = 0
            Else
                Dim r As DataRow = j.Row
                purchaseTaxValue = r("TAXVALUE")
            End If
            DiscountAddTax(txtpurchaserate.EditValue, txtdiscper.EditValue, purchaseTaxValue, purchaseTaxType, CostPrice, purchaseTaxAmt, purchaseDisAmt, ErrorMsg, Tax.Percentage2Price)
            txtcostprice.EditValue = CostPrice
            txtdisamt.EditValue = purchaseDisAmt * txtqty.EditValue
            txttaxamt.EditValue = purchaseTaxAmt * txtqty.EditValue
            If Convert.ToDecimal(txtpurchaserate.EditValue) > 0 Then
                txtamount.EditValue = Convert.ToDecimal(txtqty.EditValue) * Convert.ToDecimal(txtpurchaserate.EditValue)
            Else
                txtamount.EditValue = 0
            End If
            If purchaseTaxType = 1 Then
                txtitemnetamt.EditValue = Convert.ToDecimal(CostPrice + purchaseTaxAmt) * txtqty.EditValue
            Else
                txtitemnetamt.EditValue = Convert.ToDecimal(CostPrice) * txtqty.EditValue
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If txtsell.EditValue = 0.0 OrElse txtsell.Text = "0.00" Then
                MessageBox.Show("Sales Rate ", "Warrning")
                Exit Try
            End If
            If (txtsell.EditValue = txtcostprice.EditValue) Then
                MessageBox.Show("Sales Rate Can Not Be Equal", "Warrning")
                Exit Try
            End If
            If (txtsell.EditValue < txtcostprice.EditValue) Then
                MessageBox.Show("Sales Rate Can Not Be Less CostPrice", "Warrning")
                Exit Try
            End If

            If GridViewPOS.RowCount > 0 Then
                _sno = GridViewPOS.RowCount + 1
            Else
                _sno = 1
            End If
            Dim totalamt As Decimal = 0.0
            Dim txt_qty As Decimal = 0.0
            Dim purchaseamt As Decimal = 0.0
            Dim purchaserate As Decimal = 0.0
            Dim purchaseSell As Decimal = 0.0
            Dim purchaseItemNetAmt As Decimal = 0.0
            Dim purchaseCostPrince As Decimal = 0.0
            Dim purchaseTaxAmt As Decimal = 0.0
            Dim purchaseDate As String = ""
            Dim purchaseUnit As String = ""
            Dim purchaseRoundOff As Decimal = 0.0
            Dim purchaseSerialNo As String = ""
            _DateConversion(txtpurchasedate.EditValue, purchaseDate)
            txt_qty = txtqty.EditValue
            totalamt = txtcostprice.EditValue * txtqty.EditValue
            purchaserate = txtpurchaserate.EditValue
            purchaseamt = txtamount.EditValue
            purchaseSell = txtsell.EditValue
            purchaseItemNetAmt = txtitemnetamt.EditValue
            purchaseCostPrince = txtcostprice.EditValue
            purchaseTaxAmt = txttaxamt.EditValue
            purchaseUnit = txtunit.Text
            purchaseRoundOff = purchaseItemNetAmt - _RoundOff(purchaseItemNetAmt)
            If purchaseSerialNo = "" Then
                purchaseSerialNo = "-"
            Else
                txtserialno.Text = purchaseSerialNo
            End If
            GridPurchaseTable.BeginInit()
            GridPurchaseTable.Rows.Add(_sno, 0, txtitemcode.EditValue, txtbarcode.Text, txtitemname.Text, purchaseSerialNo, Format(purchaserate, "####0.00"), Format(txt_qty, "###0.00"), Format(purchaseamt, "####0.00"),
                                       txtdiscper.EditValue, txtdisamt.EditValue, Format(totalamt, "###0.00"), Format(purchaseCostPrince, "####0.00"), Format(purchaseSell, "###0.00"), txttaxid.EditValue, Format(purchaseTaxAmt, "####0.00"),
                                       Format(purchaseItemNetAmt, "####0.00"), Format(purchaseRoundOff, "##0.00"), Format(purchaseItemNetAmt, "####0.00"), purchaseUnit, txtbatch.Text, purchaseDate)
            GridPurchaseTable.EndInit()
            GridPurchaseTable.AcceptChanges()
            If GridPurchaseTable.Rows.Count > 0 Then
                If SalesGrandtotal("er") = True Then
                    _ScanClear()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function SalesGrandtotal(ByRef ErrorMsg As String) As Boolean
        Try
            'Dim purchasetot = (From dtrow As DataRow In dtGRN Select dtrow("")).Sum(
            'Dim numbers() As Integer = {5, 4, 1, 3, 9, 8, 6, 7, 2, 0}

            Dim totalquantity As Integer = 0
            Dim totalitem As Integer = 0
            Dim totdiscper As Decimal = 0
            Dim totdiscamt As Decimal = 0
            Dim totalamount As Decimal = 0.0
            Dim tottaxamount As Decimal = 0
            Dim grossAmount As Decimal = 0.0
            Dim roundOff As Decimal = 0.0
            Dim netAmount As Decimal = 0.0
            Dim Qty As Decimal = 0.0
            'ptot = (From ac1 As DataRow In dtPOS _
            '           Select ac1("TotalSales")).Sum(Function(ac1) ac1)

            'othertot = (From ac1 As DataRow In dtoHeader _
            '           Select ac1("Amt")).Sum(Function(ac1) ac1)


            'DigitalGauge1.Text = txtNetAmount.Text
            totalitem = GridPurchaseTable.Rows.Count
            totalquantity = (From ac1 As DataRow In GridPurchaseTable Select ac1("PURQTY")).Sum(Function(ac1) ac1)
            totdiscper = (From ac1 As DataRow In GridPurchaseTable Select ac1("PURDISPER")).Average(Function(ac1) ac1)
            totdiscamt = (From ac1 As DataRow In GridPurchaseTable Select ac1("PURDISAMT")).Sum(Function(ac1) ac1)
            totalamount = (From ac1 As DataRow In GridPurchaseTable Select ac1("PURTOTAMT")).Sum(Function(ac1) ac1)
            tottaxamount = (From ac1 As DataRow In GridPurchaseTable Select ac1("PURTAXAMT")).Sum(Function(ac1) ac1)
            grossAmount = (From ac1 As DataRow In GridPurchaseTable Select ac1("PURGROSSAMT")).Sum(Function(ac1) ac1)
            roundOff = (From ac1 As DataRow In GridPurchaseTable Select ac1("PURROUNDOFF")).Sum(Function(ac1) ac1)
            netAmount = (From ac1 As DataRow In GridPurchaseTable Select ac1("PURNETAMT")).Sum(Function(ac1) ac1)
            Dim dtrows As System.Data.EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In GridPurchaseTable Order By dtrow("SNO") Descending

            'Dim count As Integer = 0
            'For Each dt As DataRow In dtrows
            '    count += 1
            '    dt("SNO") = count
            'Next
            GridPurchaseTable.AcceptChanges()
            GridViewPOS.BestFitColumns()
            GridViewPOS.MoveFirst()


            txttotitem.EditValue = totalitem
            txttotqty.EditValue = totalquantity
            txttotdiscper.EditValue = totdiscper
            txttotdiscamt.EditValue = totdiscamt
            If purchaseTaxType = 1 Then
                txttaxexcamt.EditValue = Format(tottaxamount, "####0.00")
            Else
                txttaxincamt.EditValue = Format(tottaxamount, "####0.00")
            End If
            txtgrossamt.EditValue = Format(totalamount, "####0.00")
            txtroundoff.EditValue = Format(roundOff, "####0.00")
            txttottax.EditValue = Format(tottaxamount, "####0.00")
            txtnetamt.EditValue = Format(netAmount, "####0.00")

            'dtPOS.WriteXml(AppPath & "\Reports\POSRecentData.xml", True, System.Data.XmlWriteMode.WriteSchema)
            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return False
        End Try
    End Function
#End Region

    Private Sub btnSaveGridViewPosLayout_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSaveGridViewPosLayout.ItemClick
        Try
            GridViewPOS.SaveLayoutToXml(M_Details.AppPath & "\LayOut\GridPurchaseLayout.xml")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnsavePurchase_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnsavePurchase.ItemClick
        Try
            GridHdrTable.Rows.Clear()
            GridHdrTable.BeginInit()

            Dim invoiceNo As Integer = 0
            Dim invoiceRefno As String = ""
            Dim invoiceDate As String = ""
            Dim invoicePurDate As String = ""
            Dim invoiceSuppId As String = ""
            Dim invoiceBillDiscPer As Decimal = 0.0
            Dim invoiceBillDiscAmt As Decimal = 0.0
            Dim invoiceNetAmt As Decimal = 0.0
            Dim invoicePaymentType As String = ""
            Dim invoiceBalOutStanding As Decimal = 0.0
            Dim invoiceComId As Integer = 0
            Dim invoiceLocId As Integer = 0
            Dim invoiceUserId As Integer = 0
            invoiceNo = txtinvoiceno.Text
            invoiceRefno = txtrefno.Text
            _DateConversion(txtinvoicedate.EditValue, invoiceDate)
            _DateConversion(txtpurchasedate.EditValue, invoicePurDate)
            invoiceSuppId = txtsupplier.EditValue
            invoiceBillDiscPer = 0
            invoiceBillDiscAmt = 0
            invoiceNetAmt = txtnetamt.EditValue
            invoicePaymentType = txtpaymenttype.Text
            If invoicePaymentType = "CASH" Then
                invoiceBalOutStanding = 0
            Else
                invoiceBalOutStanding = txtnetamt.EditValue
            End If
            invoiceComId = _companyInfo.ComId
            invoiceLocId = _companyInfo.LocId
            invoiceUserId = _companyInfo.UserId

            GridHdrTable.Rows.Add(0, invoiceNo, invoiceRefno, invoiceDate, invoicePurDate, invoiceSuppId, invoiceBillDiscPer, invoiceBillDiscAmt, invoiceNetAmt, invoicePaymentType, invoiceBalOutStanding, invoiceComId, invoiceLocId, invoiceUserId)

            GridHdrTable.EndInit()
            GridHdrTable.AcceptChanges()

            If btnsavePurchase.Caption = "Save" Then
                Dim PostStringHdr As String = JsonConvert.SerializeObject(GridHdrTable)
                Dim PostStringDtl As String = JsonConvert.SerializeObject(GridPurchaseTable)
                If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=44&jsonHdr=" & PostStringHdr & "&jsonDtl=" & PostStringDtl) = True Then
                    MessageBox.Show("Data Saved", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _Clear()
                End If
            ElseIf btnsavePurchase.Caption = "Update" Then
                Dim PostStringHdr As String = JsonConvert.SerializeObject(GridHdrTable)
                Dim PostStringDtl As String = JsonConvert.SerializeObject(GridPurchaseTable)
                If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=46&jsonHdr=" & PostStringHdr & "&jsonDtl=" & PostStringDtl) = True Then
                    MessageBox.Show("Data Saved", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _Clear()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnViewPurchase_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnViewPurchase.ItemClick
       
        Try
            If FrmPurchaseView.ShowDialog = Windows.Forms.DialogResult.OK Then
                If GRNSelect(G_GRNNo) = True Then
                    If _dsHdr.Rows.Count > 0 Then
                        Dim invoiceNo As Integer = 0
                        Dim invoiceRefno As String = ""
                        Dim invoiceDate As String = ""
                        Dim invoicePurDate As String = ""
                        Dim invoiceSuppId As String = ""
                        Dim invoiceBillDiscPer As Decimal = 0.0
                        Dim invoiceBillDiscAmt As Decimal = 0.0
                        Dim invoiceNetAmt As Decimal = 0.0
                        Dim invoicePaymentType As String = ""
                        Dim invoiceBalOutStanding As Decimal = 0.0
                        Dim invoiceComId As Integer = 0
                        Dim invoiceLocId As Integer = 0
                        Dim invoiceUserId As Integer = 0
                        ' SELECT `pph_id`, `pph_trno`, `pph_refno`, `pph_invdate`, `pph_purdate`,
                        ' `pph_suppid`, `pph_billdiscper`, `pph_billdiscamt`, `pph_netamt`, `pph_paymenttype`, `pph_baloutamt`,
                        '`pph_comid`, `pph_locid`, `pph_userid`, `pph_created`, `pph_modified` FROM `pos_pur_hdr` WHERE 1

                        For Each _dtrow In _dsHdr.Rows
                            invoiceNo = _dtrow("pph_trno")
                            invoiceRefno = _dtrow("pph_refno")
                            invoiceDate = _dtrow("pph_invdate")
                            invoicePurDate = _dtrow("pph_purdate")
                            invoiceSuppId = _dtrow("pph_suppid")
                            invoiceBillDiscPer = _dtrow("pph_billdiscper")
                            invoiceBillDiscAmt = _dtrow("pph_billdiscamt")
                            invoiceNetAmt = _dtrow("pph_netamt")
                            invoicePaymentType = _dtrow("pph_paymenttype")
                        Next
                        txtinvoiceno.Text = invoiceNo
                        txtrefno.Text = invoiceRefno
                        txtinvoicedate.Text = invoiceDate
                        txtpurchasedate.Text = invoicePurDate
                        txtsupplier.EditValue = invoiceSuppId
                        txtdiscper.EditValue = invoiceBillDiscPer
                        txtdisamt.EditValue = invoiceBillDiscAmt
                        txtnetamt.EditValue = invoiceNetAmt
                        txtpaymenttype.Text = invoicePaymentType
                    End If
                    Dim purchaseId As Integer = 0
                    Dim purchaseItemCode As Integer = 0
                    Dim purchaseBarcode As String = ""
                    Dim purchaseItemName As String = ""
                    Dim purchaseSerialNo As String = ""
                    Dim purchaseQty As Decimal = 0.0
                    Dim purchaseAmt As Decimal = 0.0
                    Dim purchaseRate As Decimal = 0.0
                    Dim purchaseDiscPer As Integer = 0
                    Dim purchaseDiscAmt As Decimal = 0.0
                    Dim purchaseTotalAmt As Decimal = 0.0
                    Dim purchaseTaxId As Integer = 0
                    Dim purchaseTaxAmt As Decimal = 0.0
                    Dim purchaseGrossAmt As Decimal = 0.0
                    Dim purchaseRoundOff As Decimal = 0.0
                    Dim purchaseItemNetAmt As Decimal = 0.0
                    Dim purchaseExpiry As String = ""
                    Dim purchaseCostPrice As Decimal = 0.0
                    Dim purchaseSellPrice As Decimal = 0.0
                    Dim purchaseUnit As String = "Pcs"
                    Dim purchaseBatch As String = ""
                    If _dsDtl.Rows.Count > 0 Then
                        For Each _DtrowDtl In _dsDtl.Rows
                            _sno = _DtrowDtl("ppd_sno")
                            purchaseId = _DtrowDtl("ppd_id")
                            purchaseItemCode = _DtrowDtl("ppd_itemcode")
                            purchaseItemName = _DtrowDtl("ppd_itemname")
                            purchaseBarcode = _DtrowDtl("ppd_barcode")
                            purchaseSerialNo = _DtrowDtl("ppd_serialno")
                            purchaseRate = _DtrowDtl("ppd_prate")
                            purchaseQty = _DtrowDtl("ppd_qty")
                            purchaseAmt = _DtrowDtl("ppd_amount")
                            purchaseDiscPer = _DtrowDtl("ppd_discper")
                            purchaseDiscAmt = _DtrowDtl("ppd_discamt")
                            purchaseTotalAmt = _DtrowDtl("ppd_totalamt")
                            purchaseTaxId = _DtrowDtl("ppd_taxid")
                            purchaseTaxAmt = _DtrowDtl("ppd_taxamt")
                            purchaseGrossAmt = _DtrowDtl("ppd_grossamt")
                            purchaseRoundOff = _DtrowDtl("ppd_roundoff")
                            purchaseItemNetAmt = _DtrowDtl("ppd_netamt")
                            purchaseExpiry = _DtrowDtl("ppd_expiry")
                            purchaseCostPrice = _DtrowDtl("ppd_costprice")
                            purchaseSellPrice = _DtrowDtl("ppd_sellprice")
                            purchaseBatch = _DtrowDtl("ppd_batch")
                            GridPurchaseTable.BeginInit()
                            GridPurchaseTable.Rows.Add(_sno, purchaseId, purchaseItemCode, purchaseBarcode, purchaseItemName, purchaseSerialNo, Format(purchaseRate, "####0.00"),
                                                       Format(purchaseQty, "###0.00"), Format(purchaseAmt, "####0.00"), purchaseDiscPer, Format(purchaseDiscAmt, "####0.00"),
                                                       Format(purchaseTotalAmt, "###0.00"), Format(purchaseCostPrice, "####0.00"), Format(purchaseSellPrice, "###0.00"),
                                                       purchaseTaxId, Format(purchaseTaxAmt, "####0.00"), Format(purchaseGrossAmt, "####0.00"),
                                                       Format(purchaseRoundOff, "##0.00"), Format(purchaseItemNetAmt, "####0.00"), purchaseUnit, purchaseBatch, purchaseExpiry)
                            GridPurchaseTable.EndInit()
                            GridPurchaseTable.AcceptChanges()
                           
                        Next
                        If GridPurchaseTable.Rows.Count > 0 Then
                            If SalesGrandtotal("er") = True Then

                            End If
                        End If
                        btnsavePurchase.Caption = "Update"

                        'SELECT `ppd_id`, `ppd_trno`, `ppd_sno`, `ppd_itemcode`, `ppd_barcode`, `ppd_serialno`, `ppd_batch`,
                        '`ppd_prate`, `ppd_qty`, `ppd_amount`, `ppd_discper`, `ppd_discamt`, `ppd_totalamt`, `ppd_taxid`,
                        '`ppd_taxamt`, `ppd_grossamt`, `ppd_roundoff`, `ppd_netamt`, `ppd_expiry`, `ppd_costprice`, `ppd_sellprice`, 
                        '`ppd_comid`, `ppd_locid`, `ppd_created`, `ppd_modified` FROM `pos_pur_dtl` WHERE 1
                       
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Function GRNSelect(ByRef trno As Integer) As Boolean
        Try
            _dsDtl = New DataTable
            _dsDtl.TableName = "DtlTable"
            _dsHdr = New DataTable
            _dsHdr.TableName = "HdrTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=48&trno=" & trno)
            Dim Userparsejson As JObject = JObject.Parse(json)
            _dsDtl = Userparsejson("DTL").ToObject(Of DataTable)()
            _dsHdr = Userparsejson("HDR").ToObject(Of DataTable)()
            If _dsDtl.Rows.Count > 0 AndAlso _dsHdr.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub btnreset_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnreset.ItemClick
        Try
            btnsavePurchase.Caption = "Save"
            _Clear()
        Catch ex As Exception

        End Try
    End Sub

  
End Class

Module purchase
    Dim TablePurchaseDtl As DataTable
    Dim TablePurchaseHdr As DataTable
    Public Function CreateTable() As DataTable
        Try
            TablePurchaseDtl = New DataTable
            TablePurchaseDtl.TableName = "PurchaseData"
            TablePurchaseDtl.Columns.Add("SNO", GetType(Integer))
            TablePurchaseDtl.Columns.Add("PURID", GetType(Integer)).DefaultValue = 0
            TablePurchaseDtl.Columns.Add("ITEMCODE", GetType(Integer))
            TablePurchaseDtl.Columns.Add("BARCODE", GetType(String))
            TablePurchaseDtl.Columns.Add("ITEMNAME", GetType(String))
            TablePurchaseDtl.Columns.Add("ITEMSERIALNO", GetType(String)).DefaultValue = 0
            TablePurchaseDtl.Columns.Add("PURRATE", GetType(Decimal)).DefaultValue = 0.0
            TablePurchaseDtl.Columns.Add("PURQTY", GetType(Decimal)).DefaultValue = 0.0
            TablePurchaseDtl.Columns.Add("PURAMT", GetType(Decimal)).DefaultValue = 0.0
            TablePurchaseDtl.Columns.Add("PURDISPER", GetType(Decimal)).DefaultValue = 0
            TablePurchaseDtl.Columns.Add("PURDISAMT", GetType(Decimal)).DefaultValue = 0.0
            TablePurchaseDtl.Columns.Add("PURTOTAMT", GetType(Decimal)).DefaultValue = 0.0
            TablePurchaseDtl.Columns.Add("PURCOST", GetType(Decimal)).DefaultValue = 0.0
            TablePurchaseDtl.Columns.Add("PURSELL", GetType(Decimal)).DefaultValue = 0.0
            TablePurchaseDtl.Columns.Add("PURTAXID", GetType(Integer)).DefaultValue = 0
            TablePurchaseDtl.Columns.Add("PURTAXAMT", GetType(Decimal)).DefaultValue = 0.0
            TablePurchaseDtl.Columns.Add("PURGROSSAMT", GetType(Decimal)).DefaultValue = 0.0
            TablePurchaseDtl.Columns.Add("PURROUNDOFF", GetType(Decimal)).DefaultValue = 0.0
            TablePurchaseDtl.Columns.Add("PURNETAMT", GetType(Decimal)).DefaultValue = 0.0
            TablePurchaseDtl.Columns.Add("PURUNIT", GetType(String)).DefaultValue = "-"
            TablePurchaseDtl.Columns.Add("PURBATCH", GetType(String)).DefaultValue = "-"
            TablePurchaseDtl.Columns.Add("PUREXPIRE", GetType(String))
            TablePurchaseDtl.Columns.Add("PURCOMID", GetType(Integer)).DefaultValue = _companyInfo.ComId
            TablePurchaseDtl.Columns.Add("PURLOCID", GetType(Integer)).DefaultValue = _companyInfo.LocId
            Return TablePurchaseDtl
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function CreateHdrTable() As DataTable
        Try
            TablePurchaseHdr = New DataTable
            TablePurchaseHdr.TableName = "PurchaseDataHdr"
            TablePurchaseHdr.Columns.Add("PURHDRID", GetType(Integer)).DefaultValue = 0
            TablePurchaseHdr.Columns.Add("PURHDRTRNO", GetType(Integer))
            TablePurchaseHdr.Columns.Add("PURHDRREFNO", GetType(String))
            TablePurchaseHdr.Columns.Add("PURHDRINVDATE", GetType(String))
            TablePurchaseHdr.Columns.Add("PURHDRPURDATE", GetType(String)).DefaultValue = 0
            TablePurchaseHdr.Columns.Add("PURHDRSUPPID", GetType(Integer)).DefaultValue = 0
            TablePurchaseHdr.Columns.Add("PURHDRBDISCPER", GetType(Decimal)).DefaultValue = 0.0
            TablePurchaseHdr.Columns.Add("PURHDRBDISCAMT", GetType(Decimal)).DefaultValue = 0.0
            TablePurchaseHdr.Columns.Add("PURHDRNETAMT", GetType(Decimal)).DefaultValue = 0
            TablePurchaseHdr.Columns.Add("PURHDRPAYMENTTYPE", GetType(String)).DefaultValue = ""
            TablePurchaseHdr.Columns.Add("PURHDRBALOUT", GetType(Decimal)).DefaultValue = 0.0
            TablePurchaseHdr.Columns.Add("PURHDRCOMID", GetType(Integer)).DefaultValue = 0
            TablePurchaseHdr.Columns.Add("PURHDRLOCID", GetType(Integer)).DefaultValue = 0
            TablePurchaseHdr.Columns.Add("PURHDRUSERID", GetType(Integer)).DefaultValue = 0
            Return TablePurchaseHdr
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Class DeleteDtl
        Public Property ppd_id As String
        Public Property ppd_trno As String
        Public Property ppd_itemcode As String
        Public Property ppd_barcode As String
        Public Property ppd_qty As String
        Public Property ppd_costprice As String
        Public Property ppd_sellprice As String
        Public Property ppd_comid As String
        Public Property ppd_locid As String
    End Class
End Module