
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Data.DataTableExtensions
Imports DevExpress.XtraEditors
Imports DevExpress.XtraBars
Imports Newtonsoft.Json
Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class frmLedgerEntry
    Dim _pro As New _ProperityUacVoucher
    Dim _DS As New DataSet
    Dim js As New journalEntry
    Public G_RefID As String = String.Empty
#Region "Verification"
    Public Overloads Sub ShowDialog(ByRef ModeofPay As String)
        Try
            lblpay.Text = ModeofPay
            If ModeofPay = "PAYMENT" Then
                _pro.M_frmName = "PAY"
                LayoutControlGroup1.AppearanceGroup.BackColor = Color.Coral
            ElseIf ModeofPay = "RECEIPT" Then
                _pro.M_frmName = "REC"
                LayoutControlGroup1.AppearanceGroup.BackColor = Color.Blue
            Else
                _pro.M_frmName = "JUR"
                LayoutControlGroup1.AppearanceGroup.BackColor = Color.BurlyWood
            End If
            txtfromledger.EditValue = 1
            MyBase.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
    Private Function ShowData(ByRef ErrorMsg As String) As Boolean
        Try
            Dim st As String = ""
            _DateConversion(Date.Now, Date.Now, st)
            Dim dsex As New DataTable
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxPayRec=2&date=" & st)
            Dim Userparsejson As JObject = JObject.Parse(json)
            dsex = Userparsejson("Journal").ToObject(Of DataTable)()
            If dsex.Rows.Count > 0 Then
                GridControl1.DataSource = dsex
            Else
                GridControl1.DataSource = Nothing
            End If
            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return False
        End Try
    End Function
    Public Sub _Clear()

        txttoledger.SelectedText = ""
        txtchqno.Text = ""
        txtamount.Text = ""
        txtcompayment.SelectedIndex = 0
        If txtcompayment.SelectedIndex = 0 Then
            txtbanknamelist.Enabled = False
            txtchqno.Enabled = False
            txtchqdate.Enabled = False
        Else
            txtbanknamelist.Enabled = True
            txtchqno.Enabled = True
            txtchqdate.Enabled = True
        End If
        txtnarration.Text = ""
        txtnetamt.Text = ""
        txtvoucherCode.Text = ""
        ShowData("er")
    End Sub
    Public Sub _EnaDis(ByRef T As Boolean)

        txtbanknamelist.Enabled = T
        txttoledger.Enabled = T
        txtchqno.Enabled = T
        txtamount.Enabled = T
        txtcompayment.Enabled = T
        txtnarration.Enabled = T
        txtnetamt.Enabled = T
        txtvoucherCode.Enabled = T
        txtvoucherDate.Enabled = T
        txtchqdate.Enabled = T

    End Sub
    Public Sub _Msg(ByRef _str As String, ByRef _cap As String)
        Try
            MessageBox.Show(_str, _cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub _Msg(ByRef _str As Exception, ByRef _cap As String)
        Try
            MessageBox.Show(_str, _cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        Catch ex As Exception

        End Try
    End Sub
    Public Function _FiledVerify() As Boolean
        Try
            If txtamount.Text = "" OrElse txtamount.Text Is String.Empty Then
                _Msg("Amount Can't Be Empty", "Amount")
                Return False
            ElseIf txtnetamt.Text = "" OrElse txtnetamt.Text Is String.Empty Then
                _Msg("Amount Can't Be Empty", "Amount")
                Return False
            ElseIf txtcompayment.SelectedIndex = -1 OrElse txtcompayment.Text = "" Then
                _Msg("Payment Mode", "Amount")
                Return False
            ElseIf txtfromledger.Text = " " OrElse txtfromledger.Text.Length < 0 Then
                _Msg("Select From Ledger Name", "Ledger")
                Return False
            ElseIf txttoledger.Text = "" OrElse txttoledger.Text.Length < 0 Then
                _Msg("Select To Ledger Name", "Ledger")
                Return False
            ElseIf txtnarration.Text = "" Then
                txtnarration.Text = "-"
                Return True
            Else
                Return True
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Sub _CheckMade(ByRef _Str As String)
        Try
            Select Case _Str
                Case "New"
                    _SaveMethod("Save")
                Case "Edit"
                    _SaveMethod("Update")
                Case "Delete"
            End Select
        Catch ex As Exception

        End Try
    End Sub
    Public Sub _SaveMethod(ByRef _str As String)
        Try
            Dim entryDate As String = ""
            Select Case lblpay.Text
                Case "PAYMENT"
                    If txtcompayment.SelectedIndex = 0 Then
                        _DateConversion(txtvoucherDate.Text, entryDate)
                        js.jid = 0
                        If _str = "Update" Then
                            js.billno = txtvoucherCode.Text
                        Else
                            js.billno = "0"
                        End If

                        js.ledgerid = txtfromledger.EditValue
                        js.description = txtfromledger.Text & " Debit to " & txttoledger.Text
                        js.dr = txtamount.Text
                        js.cr = 0
                        js.jstatus = "Dr"
                        js.entrydate = entryDate
                        js.actype = "PAY"
                        js.modetype = "CA"
                        js.narration = txtnarration.Text
                        js.status = "A"
                        js.username = _companyInfo.UserName
                        js.description2 = txtfromledger.Text & " To " & txttoledger.Text
                        js.ledgerid2 = txttoledger.EditValue
                        js.comid = _companyInfo.ComId
                        js.locid = _companyInfo.LocId
                        js.bankname = "0"
                        js.chqno = "0"
                        js.chqdate = entryDate
                    Else
                        _DateConversion(txtvoucherDate.Text, txtvoucherDate.Text, entryDate)
                        If txtchqno.Text = "" Then
                            _Msg("Cheque No Missing", "Cheque")
                            Exit Sub
                        ElseIf txtbanknamelist.Text.Length < 0 Then
                            _Msg("Please Select Bank  Missing", "Bank")
                            Exit Sub
                        End If
                        js.jid = 0
                        If _str = "Update" Then
                            js.billno = txtvoucherCode.Text
                        Else
                            js.billno = "0"
                        End If
                        js.ledgerid = txtfromledger.EditValue
                        js.description = txtfromledger.Text & " Debit to " & txttoledger.Text
                        js.dr = txtamount.Text
                        js.cr = 0
                        js.jstatus = "Dr"
                        js.entrydate = entryDate
                        js.actype = "PAY"
                        js.modetype = "CQ"
                        js.narration = txtnarration.Text

                        js.status = "I"
                        js.username = _companyInfo.UserName
                        js.description2 = txtfromledger.Text & " To " & txttoledger.Text
                        js.ledgerid2 = txttoledger.EditValue
                        js.comid = _companyInfo.ComId
                        js.locid = _companyInfo.LocId
                        js.bankname = txtbanknamelist.Text
                        js.chqno = txtchqno.Text
                        _DateConversion(txtchqdate.Text, txtchqdate.Text, entryDate)
                        js.chqdate = entryDate
                    End If
                Case "RECEIPT"
                    If txtcompayment.SelectedIndex = 0 Then
                        _DateConversion(txtvoucherDate.Text, txtvoucherDate.Text, entryDate)
                        js.jid = 0
                        If _str = "Update" Then
                            js.billno = txtvoucherCode.Text
                        Else
                            js.billno = "0"
                        End If
                        js.ledgerid = txttoledger.EditValue
                        js.description = txttoledger.Text & " To " & txtfromledger.Text
                        js.dr = 0
                        js.cr = txtamount.Text
                        js.jstatus = "Cr"
                        js.entrydate = entryDate
                        js.actype = "REC"
                        js.modetype = "CA"
                        js.narration = txtnarration.Text

                        js.status = "A"
                        js.username = _companyInfo.UserName
                        js.description2 = txttoledger.Text & " Debit to " & txtfromledger.Text
                        js.ledgerid2 = txtfromledger.EditValue
                        js.comid = _companyInfo.ComId
                        js.locid = _companyInfo.LocId
                        js.bankname = "0"
                        js.chqno = "0"
                        js.chqdate = Date.Now.ToString("yyyy-MM-dd")
                    Else
                        _DateConversion(txtchqdate.Text, txtchqdate.Text, entryDate)
                        If txtchqno.Text = "" Then
                            _Msg("Cheque No Missing", "Cheque")
                            Exit Sub
                        ElseIf txtbanknamelist.Text.Length < 0 Then
                            _Msg("Please Select Bank  Missing", "Bank")
                            Exit Sub
                        End If
                        js.jid = 0
                        If _str = "Update" Then
                            js.billno = txtvoucherCode.Text
                        Else
                            js.billno = "0"
                        End If
                        js.ledgerid = txttoledger.EditValue
                        js.description = txttoledger.Text & " To " & txtfromledger.Text
                        js.dr = 0
                        js.cr = txtamount.Text
                        js.jstatus = "Cr"
                        js.entrydate = txtvoucherDate.Text
                        js.actype = "REC"
                        js.modetype = "CQ"
                        js.narration = txtnarration.Text

                        js.status = "I"
                        js.username = _companyInfo.UserName
                        js.description2 = txttoledger.Text & " Debit to " & txtfromledger.Text
                        js.ledgerid2 = txtfromledger.EditValue
                        js.comid = _companyInfo.ComId
                        js.locid = _companyInfo.LocId
                        js.bankname = txtbanknamelist.Text
                        js.chqno = txtchqno.Text
                        js.chqdate = entryDate
                    End If
                Case "JOURNAL"
                    If txtcompayment.SelectedIndex = 0 Then
                        _DateConversion(txtvoucherDate.Text, txtvoucherDate.Text, entryDate)
                        js.jid = 0
                        If _str = "Update" Then
                            js.billno = txtvoucherCode.Text
                        Else
                            js.billno = "0"
                        End If
                        js.ledgerid = txtfromledger.EditValue
                        js.description = txtfromledger.Text & " Debit to " & txttoledger.Text
                        js.dr = txtamount.Text
                        js.cr = 0
                        js.jstatus = "Dr"
                        js.entrydate = entryDate
                        js.actype = "JUR"
                        js.modetype = "CA"
                        js.narration = txtnarration.Text
                        js.status = "A"
                        js.username = _companyInfo.UserName
                        js.description2 = txtfromledger.Text & " To " & txttoledger.Text
                        js.ledgerid2 = txttoledger.EditValue
                        js.comid = _companyInfo.ComId
                        js.locid = _companyInfo.LocId
                        js.bankname = "0"
                        js.chqno = "0"
                        js.chqdate = entryDate
                    Else
                        _DateConversion(txtvoucherDate.Text, txtvoucherDate.Text, entryDate)
                        If txtchqno.Text = "" Then
                            _Msg("Cheque No Missing", "Cheque")
                            Exit Sub
                        ElseIf txtbanknamelist.Text.Length < 0 Then
                            _Msg("Please Select Bank  Missing", "Bank")
                            Exit Sub
                        End If
                        js.jid = 0
                        js.billno = "0"
                        js.ledgerid = txtfromledger.EditValue
                        js.description = txtfromledger.Text & " Debit to " & txttoledger.Text
                        js.dr = txtamount.Text
                        js.cr = 0
                        js.jstatus = "Dr"
                        js.entrydate = entryDate
                        js.actype = "JUR"
                        js.modetype = "CQ"
                        js.narration = txtnarration.Text

                        js.status = "I"
                        js.username = _companyInfo.UserName
                        js.description2 = txtfromledger.Text & " To " & txttoledger.Text
                        js.ledgerid2 = txttoledger.EditValue
                        js.comid = _companyInfo.ComId
                        js.locid = _companyInfo.LocId
                        js.bankname = txtbanknamelist.Text
                        js.chqno = txtchqno.Text
                        _DateConversion(txtchqdate.Text, txtchqdate.Text, entryDate)
                        js.chqdate = entryDate
                    End If
            End Select


            Select Case _str
                Case "Save"
                    If (_InsertDB("I", js) = True) Then
                        _SelectDB("B")
                        _Clear()
                        _EnaDis(False)
                        lblmode.Text = "Mode"
                        ShowData("er")
                        _Msg("Voucher Saved", "Voucher")
                    End If
                Case "Update"
                    If txtvoucherCode.Text = "" Then
                        _Msg("VOUCHER CODE NOT FOUND", "VOUCHER")
                        Exit Select
                        Exit Sub
                    End If
                    '  _pro.M_ACC_VOUCHER_ID = txtvoucherCode.Text
                    If (_InsertDB("U", js) = True) Then
                        _SelectDB("B")
                        _Clear()
                        _EnaDis(False)
                        ShowData("er")
                        '  _pro.M_LabelMode = "Mode"
                        lblmode.Text = "Mode"
                    End If
                Case "Delete"
                    If (_InsertDB("D", js) = True) Then
                        _SelectDB("B")
                        _Clear()
                        _EnaDis(False)
                        ShowData("er")
                        ' _pro.M_LabelMode = "Mode"
                        lblmode.Text = "Mode"
                    End If
                Case "Mode"
                    _Msg("Mode Not Selected", "Mode")
            End Select

        Catch ex As Exception

        End Try
    End Sub
    Function _InsertDB(ByRef _string As String, ByRef _pros As journalEntry) As Boolean
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            dialog.Caption = "Verfiying Data"
            Dim PostString As String = JsonConvert.SerializeObject(_pros)
            If _JsonSend(M_Details.LinkAjaxRequest & "AjaxPayRec=1&json=" & PostString) = True Then
                dialog.Caption = "New Data Created"
            End If
            Return True
        Catch ex As Exception
            dialog.Caption = ex.Message
            Return False
        Finally
            dialog.Close()
        End Try
    End Function
    Public Sub _Print(ByRef _str As String, ByRef _datset As DataTable)
        Try
            Select Case _str
                Case "A"
                    Dim rptvoucher As New rptStaffProfile
                    rptvoucher.LoadLayout(M_Details.AppPath & "\Reports\payvoucher.repx")
                    rptvoucher.DataSource = _datset
                    Dim pt As New DevExpress.XtraReports.UI.ReportPrintTool(rptvoucher)
                    pt.ShowPreviewDialog()
                Case "B"

            End Select
        Catch ex As Exception

        End Try
    End Sub
    Public Sub _SelectDB(ByRef _mode As String)
        'Try
        '    Select Case _mode
        '        Case "A"
        '            Dim _Sql(1) As SqlParameter
        '            _Sql(0) = New SqlParameter("@mode", _mode)
        '            _Sql(1) = New SqlParameter("@VoucNo", "0")
        '            _DS = _dbls._sqlDataAdapter("sp_accvoucherselect", _Sql)
        '            If _DS.Tables(0).Rows.Count > 0 Then
        '                txtledger.Properties.DataSource = _DS.Tables(0)
        '                ' txtcompayment.DisplayMember = "LNA"
        '                ' txtcompayment.ValueMember = "LID"

        '            Else
        '                _Msg("No Record Found", "Record")
        '            End If
        '        Case "B"
        '            Dim _Sql(1) As SqlParameter
        '            _Sql(0) = New SqlParameter("@mode", _mode)
        '            _Sql(1) = New SqlParameter("@VoucNo", lblpay.Text)
        '            _DS = _dbls._sqlDataAdapter("sp_accvoucherselect", _Sql)
        '            If _DS.Tables(0).Rows.Count > 0 Then

        '                GridControl1.DataSource = _DS.Tables(0)
        '                _LOADLAYOUT()
        '            Else
        '                _Msg("No Record Found", "Record")
        '            End If
        '        Case "C"
        '            Dim _Sql(1) As SqlParameter
        '            _Sql(0) = New SqlParameter("@mode", _mode)
        '            _Sql(1) = New SqlParameter("@VoucNo", _pro.M_AccNo)
        '            _DS = _dbls._sqlDataAdapter("sp_accvoucherselect", _Sql)
        '            Dim _dt As DataTable
        '            If _DS.Tables(0).Rows.Count > 0 Then
        '                _dt = _DS.Tables(0)
        '                _dt.WriteXml(AppDomain.CurrentDomain.BaseDirectory & "\UacVoucher.xml", True, True)
        '                _Print("A", _DS)
        '            Else
        '                _Msg("No Record Found", "Record")
        '            End If
        '        Case "Mode"
        '            _Msg("Mode Not Selected", "Record")
        '    End Select
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub _LOADLAYOUT()
        Try
            GridView1.RestoreLayoutFromXml(AppDomain.CurrentDomain.BaseDirectory & "\UacPayment.xml")
        Catch ex As Exception

        End Try
    End Sub
#End Region
    Public Sub _TEST()
        Try
            _EnaDis(True)
            _Clear()
            lblmode.Text = "New"
            ' _pro.M_LabelMode = lblmode.Text
            txtvoucherDate.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        Try
            _EnaDis(True)
            _Clear()
            lblmode.Text = "New"
            '_pro.M_LabelMode = lblmode.Text
            txtvoucherDate.Select()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub UacVPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            If getBankList() = True Then
                If _JsonData.BankListTable.Rows.Count > 0 Then


                    Dim linq = From _Row In _JsonData.BankListTable.AsEnumerable() Where _Row.Field(Of String)("LedgerType") = "D" Select _Row

                    If linq.Any Then
                        Dim taba As DataTable
                        taba = linq.CopyToDataTable
                        txtfromledger.Properties.DataSource = taba
                        txtfromledger.EditValue = taba.Rows(0)(0)
                    End If

                    Dim linq1 = From _Row In _JsonData.BankListTable.AsEnumerable() Where _Row.Field(Of String)("LedgerType") <> "D" Select _Row

                    If linq1.Any Then
                        Dim taba As DataTable
                        taba = linq1.CopyToDataTable
                        txttoledger.Properties.DataSource = taba
                        txttoledger.EditValue = taba.Rows(0)(0)
                    End If
                End If
            End If
            'If getLedgerListTable() = True Then
            '    If _JsonData.LedgerListTable.Rows.Count > 0 Then
            '        txttoledger.Properties.DataSource = _JsonData.LedgerListTable
            '    End If
            'End If
            lblpay.Text = "PAYMENT"
            _pro.M_frmName = "PAY"
            _Clear()
            _EnaDis(False)


        Catch ex As Exception

        End Try

    End Sub


    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        Try
            _EnaDis(True)
            lblmode.Text = "Edit"
            '_pro.M_LabelMode = lblmode.Text
            txtvoucherDate.Select()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            If lblmode.Text = "New" Or lblmode.Text = "Edit" Then

                If (_FiledVerify() = True) Then
                    _CheckMade(lblmode.Text)
                    btnnew.Select()
                End If
            Else
                '    _Msg("Mode Not Selected", "Mode")
            End If
        Catch ex As Exception

        End Try
    End Sub



    Private Sub txtcompayment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtcompayment.SelectedIndexChanged
        Try
            If txtcompayment.SelectedIndex = 0 Then 'cash
                txtchqno.Enabled = False
                txtchqdate.Enabled = False
                txtbanknamelist.Enabled = False

            ElseIf txtcompayment.SelectedIndex = 1 Then 'cq
                txtchqno.Enabled = True
                txtchqdate.Enabled = True
                txtbanknamelist.Enabled = True

            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub txtamount_EditValueChanged(sender As Object, e As EventArgs) Handles txtamount.EditValueChanged
        Try
            txtnetamt.Text = txtamount.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            Dim modeofpayment As String = ""
            lblmode.Text = "Mode"
            Dim PMODE As String

            txtvoucherCode.Text = GridView1.GetFocusedRowCellValue("BillNo").ToString
            txtvoucherDate.Text = Convert.ToDateTime(GridView1.GetFocusedRowCellValue("ENTRY_DATE"))
            txttoledger.Text = GridView1.GetFocusedRowCellValue("HEAD_NAME").ToString
            modeofpayment = GridView1.GetFocusedRowCellValue("JModeStatus").ToString
            PMODE = GridView1.GetFocusedRowCellValue("PayMode").ToString
            If PMODE = "CA" Then
                txtcompayment.SelectedIndex = 0
            Else
                txtcompayment.SelectedIndex = 1
            End If

            If GridView1.GetFocusedRowCellValue("DR").ToString <> "0.00" Then
                txtamount.Text = GridView1.GetFocusedRowCellValue("DR").ToString
            Else
                txtamount.Text = GridView1.GetFocusedRowCellValue("CR").ToString
            End If
            'txtchqdate.Text = Convert.ToDateTime(GridView1.GetFocusedRowCellValue("ACC_VOUCHER_CHDATE"))
            'txtchqno.Text = GridView1.GetFocusedRowCellValue("ACC_VOUCHER_CHEQNO").ToString
            'txtnarration.Text = GridView1.GetFocusedRowCellValue("ACC_VOUCHER_NAR").ToString
            Select Case modeofpayment
                Case "PAY"
                    lblpay.Text = "PAYMENT"
                Case "REC"
                    lblpay.Text = "RECEIPT"
                Case "JUR"
                    lblpay.Text = "JOURNAL"
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnSaveLay_Click(sender As Object, e As EventArgs) Handles BtnSaveLay.Click
        Try
            GridView1.SaveLayoutToXml(M_Details.AppPath & "\Layout\UacPayment.xml")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PrintVoucherToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintVoucherToolStripMenuItem.Click
        Try
            If GridView1.IsGroupRow(GridView1.FocusedRowHandle) = True Then
                G_RefID = GridView1.GetGroupRowValue(GridView1.FocusedRowHandle)
            Else
                G_RefID = GridView1.GetFocusedRowCellValue("BillNo")
            End If
            Dim dsex As New DataTable
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxPayRec=3&billno=" & G_RefID)
            Dim Userparsejson As JObject = JObject.Parse(json)
            dsex = Userparsejson("Journal").ToObject(Of DataTable)()
            Dim _dt As New DataTable
            If dsex.Rows.Count > 0 Then
                dsex.TableName = "Voucher"
                _dt = dsex
                _dt.WriteXml(M_Details.AppPath & "\Reports\UacVoucher.xml", True, True)
                _Print("A", dsex)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        Try
            '_pro.M_AccNo = GridView1.GetFocusedRowCellValue("ACC_VOUCHER_ID").ToString
            'If _pro.M_AccNo Is Nothing Then
            '    _Msg("Select Voucher No", "Voucher")
            '    Exit Sub
            'Else
            _SelectDB("C")
            'End If
            GridControl1.ShowPrintPreview()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PrintPreviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintPreviewToolStripMenuItem.Click
        Try
            GridControl1.ShowPrintPreview()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub txtvoucherDate_KeyDown(sender As Object, e As KeyEventArgs) Handles txtvoucherDate.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txttoledger.Select()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtledger_KeyDown(sender As Object, e As KeyEventArgs) Handles txttoledger.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txtcompayment.Select()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtcompayment_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcompayment.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txtamount.Select()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtamount_KeyDown(sender As Object, e As KeyEventArgs) Handles txtamount.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txtnarration.Select()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtchqno_KeyDown(sender As Object, e As KeyEventArgs) Handles txtchqno.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txtchqdate.Select()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtchqdate_KeyDown(sender As Object, e As KeyEventArgs) Handles txtchqdate.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txtnarration.Select()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtnarration_KeyDown(sender As Object, e As KeyEventArgs) Handles txtnarration.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                btnsave.Select()
            End If
        Catch ex As Exception

        End Try
    End Sub



    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnpayment_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnpayment.ItemClick
        Try
            If DevExpress.XtraEditors.XtraMessageBox.Show("Are you sure to change the PAYMENT Mode?", "Ver1.0.0", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                lblpay.Text = "PAYMENT"
                _pro.M_frmName = "PAY"
                LayoutControlGroup1.AppearanceGroup.BackColor = Color.LightSteelBlue

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnreceipt_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnreceipt.ItemClick
        Try
            If DevExpress.XtraEditors.XtraMessageBox.Show("Are you sure to change the RECEIPT Mode?", "Ver1.0.0", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                lblpay.Text = "RECEIPT"
                _pro.M_frmName = "REC"
                LayoutControlGroup1.AppearanceGroup.BackColor = Color.LightSteelBlue

            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnJournal_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnJournal.ItemClick
        Try
            If DevExpress.XtraEditors.XtraMessageBox.Show("Are you sure to change the JOURNAL Mode?", "Ver1.0.0", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                lblpay.Text = "JOURNAL"
                _pro.M_frmName = "JUR"
                LayoutControlGroup1.AppearanceGroup.BackColor = Color.LightSteelBlue
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtfromledger_EditValueChanged(sender As Object, e As EventArgs) Handles txtfromledger.EditValueChanged
        Try
            Dim _data = txtfromledger.GetSelectedDataRow
            Dim id = _data(0).ToString
            Dim name = _data(1).ToString
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxPayRec=7&ledgerId=" & id)
            Dim Userparsejson As JObject = JObject.Parse(json)
            Dim sucesss = Userparsejson("Success")
            Dim Msg = Userparsejson("Data")
            If sucesss.ToString = "True" Then
                txtfromcurrent.Text = Msg.ToString
            End If

            'MessageBox.Show(id & "-" & name)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txttoledger_EditValueChanged(sender As Object, e As EventArgs) Handles txttoledger.EditValueChanged
        Try
            Dim _data = txtfromledger.GetSelectedDataRow
            Dim id = _data(0).ToString
            Dim name = _data(1).ToString
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxPayRec=7&ledgerId=" & id)
            Dim Userparsejson As JObject = JObject.Parse(json)
            Dim sucesss = Userparsejson("Success")
            Dim Msg = Userparsejson("Data")
            If sucesss.ToString = "True" Then
                txttocurrent.Text = Msg.ToString
            End If

            'MessageBox.Show(id & "-" & name)
        Catch ex As Exception

        End Try
    End Sub
    Public Property MODE As String

    Private Sub btnopenledger_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnopenledger.ItemClick
        Try
            frmLedgerLoad.ShowDialog(Date.Now)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DeleteVoucherToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteVoucherToolStripMenuItem.Click
        Try
            If GridView1.IsGroupRow(GridView1.FocusedRowHandle) = True Then
                G_RefID = GridView1.GetGroupRowValue(GridView1.FocusedRowHandle)
            Else
                G_RefID = GridView1.GetFocusedRowCellValue("BillNo")
            End If
            Dim dsex As New DataTable
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxPayRec=6&billno=" & G_RefID)
            Dim Userparsejson As JObject = JObject.Parse(json)
            Dim sucesss = Userparsejson("Success")
            Dim Msg = Userparsejson("Msg")
            If sucesss.ToString = "True" Then
                MessageBox.Show(Msg.ToString, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ShowData("ER")
            End If

        Catch ex As Exception

        End Try
    End Sub

  
   
End Class

#Region "Methods"
Public Class _ProperityUacVoucher
    Public Shared _FrmName As String
    Public Property M_frmName As String
        Get
            Return _FrmName

        End Get
        Set(value As String)
            _FrmName = value
        End Set
    End Property
End Class
Public Class journalEntry
    Public Property jid As Integer
    Public Property ledgerid As Integer
    Public Property description As String
    Public Property dr As Decimal
    Public Property cr As Decimal
    Public Property jstatus As String
    Public Property billno As String
    Public Property entrydate As Date
    Public Property actype As String
    Public Property modetype As String
    Public Property narration As String
    Public Property status As String
    Public Property username As String
    Public Property ledgerid2 As String
    Public Property description2 As String
    Public Property comid As Integer
    Public Property locid As Integer
    Public Property chqno As String
    Public Property chqdate As Date
    Public Property bankname As String
End Class
#End Region