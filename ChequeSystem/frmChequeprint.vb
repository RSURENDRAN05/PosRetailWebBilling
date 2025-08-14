Imports System.IO
Imports DevExpress.XtraReports.UI
Imports Newtonsoft.Json
Imports System.Globalization

Public Class frmChequeprint
    Dim PrintProfileDS As DataSet
    Private r As rpta5print
    Dim pt As ReportPrintTool
    Dim ini As New IniFile(M_Details.AppPath & "\Settings\" & "Settings.ini")
    Dim banksta As New bankstate
    '    Dim ri As  = New CultureInfo("ms-MY")
    Dim lblentryMode As String = "Out"
    Dim bankId As String = ""
    Dim profileId As String = ""
    Dim companyId As String = ""
    Public Sub New()
        'MySqlConnection()
        ' This call is required by the designer.
        InitializeComponent()

        If CheckForInternetConnection() = True Then
            'If chkRegistryKey(licenceVar) = False Then
            '    End
            'Else
            '    If licenceVar = "T" Then
            '        barstatustrial.Caption = "Trial Version"
            '    Else
            '        barstatustrial.Caption = "Licence Activated"
            '    End If
            'End If
            If M_Details.licenceActive = "T" Then
                barstatustrial.Caption = "Trial Version"
            Else
                barstatustrial.Caption = "Licence Activated"
            End If
            bankId = ini.ReadValue("Bank", "BankId")
            profileId = ini.ReadValue("Profile", "ProfileId")
            companyId = ini.ReadValue("Bank", "CompanyId")

            txtbanklist.EditValue = bankId
            txtprintprofile.SelectedIndex = profileId
            txtcomapny.EditValue = companyId
            barStatus.Caption = M_Details.SoftwareVersion
            baruserStatus.Caption = _companyInfo.UserId & "-" & _companyInfo.UserName
        Else
            MessageBox.Show("No Internet Connection", "Exit")
            End
        End If

        CreateTableBankName()
        CreateTablePayeeTable()
        CreateTableBankTableStatement()
        'CreateTableBankTableSave()
        CreateCompanyTable()
        txtdate.EditValue = Date.Now.ToString("dd-MM-yyyy")
        If ChqJson.BankTable.Rows.Count > 0 Then
            txtbanklist.Properties.DataSource = ChqJson.BankTable.Select("Active =" & True).CopyToDataTable
        End If
        If ChqJson.CompanyTable.Rows.Count > 0 Then
            txtcomapny.Properties.DataSource = ChqJson.CompanyTable.Select("Active = 1").CopyToDataTable
        End If

        txtpayee.Properties.DataSource = ChqJson.PayeeTable
        If File.Exists(M_Details.AppPath & "\Settings\PrintProfileSetting.xml") Then
            PrintProfileDS = New DataSet
            PrintProfileDS.ReadXml(M_Details.AppPath & "\Settings\PrintProfileSetting.xml")
            'Dim da As DataTable = PrintProfileDS.Tables(0).Select("ProfileActive =" & "Active").CopyToDataTable
            For Each rs In PrintProfileDS.Tables(0).Rows
                txtprintprofile.Properties.Items.Add(rs("FileName"))
            Next
        End If
        txtprintprofile.SelectedIndex = 0


        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub txtamount_EditValueChanged(sender As Object, e As EventArgs) Handles txtamount.EditValueChanged
        Try
            Dim _double As Double = 0.0
            _double = txtamount.EditValue
            txtamount.EditValue = Format(Val(_double), "#####0.00")
            txtamtinwords.Text = NumberInWords(_double.ToString)
        Catch ex As Exception

        End Try
    End Sub


    Private Sub barbtnBankList_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnBankList.ItemClick
        Try

            frmBankList.ShowDialog()
            barbtnrefresh_ItemClick(e, e)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnrefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnrefresh.ItemClick
        Try
            ChqJson.BankTable.Rows.Clear()
            If File.Exists(M_Details.AppPath & "\Settings\BankList.xml") Then
                ChqJson.BankTable.ReadXml(M_Details.AppPath & "\Settings\BankList.xml")
                txtbanklist.Properties.DataSource = ChqJson.BankTable.Select("Active =" & True).CopyToDataTable
            End If
            If File.Exists(M_Details.AppPath & "\Settings\PrintProfileSetting.xml") Then
                PrintProfileDS = New DataSet
                PrintProfileDS.ReadXml(M_Details.AppPath & "\Settings\PrintProfileSetting.xml")
                'Dim da As DataTable = PrintProfileDS.Tables(0).Select("ProfileActive =" & "Active").CopyToDataTable
                For Each rs In PrintProfileDS.Tables(0).Rows
                    txtprintprofile.Properties.Items.Add(rs("FileName"))
                Next
            End If
            ChqJson.CompanyTable.Rows.Clear()
            txtcomapny.Properties.DataSource = ChqJson.CompanyTable.Select("active =" & True).CopyToDataTable
            'If File.Exists(M_Details.AppPath & "\settings\companytable.xml") Then
            '    ChqJson.CompanyTable.ReadXml(M_Details.AppPath & "\settings\companytable.xml")
            '    txtcomapny.Properties.DataSource = ChqJson.CompanyTable.Select("active =" & True).CopyToDataTable
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        Try

            frmpayeelist.ShowDialog()
            DataLoad()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnlinkprofile_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnlinkprofile.ItemClick
        Try
            frmprintprofile.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnprintdesign_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnprintdesign.ItemClick
        Try
            frmreportdesign.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            Dim payeename As String = ""
            If txtcomapny.EditValue Is Nothing Then
                MessageBox.Show("Select Company", "Error")
                Exit Sub
            End If
            dialog.Caption = "Generating Print Preview"
            If chkalteritem.CheckState = CheckState.Checked Then
                payeename = "**" & txtaltername.Text & "**"
            Else
                payeename = ""
            End If
            Dim ds As New DataSet
            ChqJson.BankTableStatement.Rows.Clear()
            ChqJson.BankTableStatement.BeginInit()
            Dim datformart As String = ""
            Dim refdatformart As String = ""
            _DateConversion(txtdate.EditValue, datformart)
            _Addspace(datformart, refdatformart)
            Dim CurrentCury As String = FormatCurrency(txtamount.EditValue, 2, TriState.False)
            Dim curformated As String = ""
            _CurrencyFormat(CurrentCury, curformated)
            ChqJson.BankTableStatement.Rows.Add(Nothing, txtbanklist.Text, payeename, refdatformart, "**" & curformated & "**", "**" & txtamtinwords.Text & "**")
            ChqJson.BankTableStatement.EndInit()
            ChqJson.BankTableStatement.AcceptChanges()
            If File.Exists(M_Details.AppPath & "\Settings\BankTableStatement.xml") Then
                ChqJson.BankTableStatement.WriteXml(M_Details.AppPath & "\Settings\BankTableStatement.xml")
            End If
            dialog.Close()
            ds.Merge(ChqJson.BankTableStatement)
            If lblentryMode = "Out" Then
                If Gen_Report(ds) = True Then
                    ds.Tables.Clear()
                    Dim s As DialogResult = MessageBox.Show("Are you Want to Save Payment Entry?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                    If s = Windows.Forms.DialogResult.Yes Then

                        banksta.CompanyName = txtcomapny.EditValue
                        banksta.PayeeChq = txtchqno.Text
                        banksta.BankName = txtbanklist.Text
                        banksta.PayeeName = txtpayee.Text
                        Dim col As String = ""
                        Dim frmdate As String = ""
                        _DateConversion(txtdate.EditValue, col, frmdate)
                        banksta.PayeeDate = frmdate
                        banksta.PayeeAmountDr = txtamount.EditValue
                        banksta.PayeeAmountCr = "0.00"
                        banksta.PayeeMode = lblentryMode
                        banksta.PayeeStatus = 2
                        banksta.UserId = _companyInfo.UserId
                        Dim PostString As String = JsonConvert.SerializeObject(banksta)
                        If _JsonSendChq(M_Details.LinkAjaxRequestCheque & "AjaxRequest=10&json=" & PostString) = True Then
                            dialog.Caption = "Data Saved Success.."
                            'DataLoad()
                        End If
                        'ChqJson.BankTableSave.BeginInit()
                        'ChqJson.BankTableSave.Rows.Add(Nothing, txtcomapny.Text, txtchqno.Text, txtbanklist.Text, txtpayee.Text, txtdate.EditValue, txtamount.EditValue)
                        'ChqJson.BankTableSave.EndInit()
                        'ChqJson.BankTableSave.AcceptChanges()
                        'If File.Exists(M_Details.AppPath & "\Settings\BankTableSave.xml") Then
                        '    ChqJson.BankTableSave.WriteXml(M_Details.AppPath & "\Settings\BankTableSave.xml")
                        'End If
                    End If

                End If
            Else
                Dim s As DialogResult = MessageBox.Show("Are you Want to Save Receipt Entry?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                If s = Windows.Forms.DialogResult.Yes Then

                    banksta.CompanyName = txtcomapny.EditValue
                    banksta.PayeeChq = txtchqno.Text
                    banksta.BankName = txtbanklist.Text
                    banksta.PayeeName = txtpayee.Text
                    Dim col As String = ""
                    Dim frmdate As String = ""
                    _DateConversion(txtdate.EditValue, col, frmdate)
                    banksta.PayeeDate = frmdate
                    banksta.PayeeAmountCr = txtamount.EditValue
                    banksta.PayeeAmountDr = "0.00"
                    banksta.PayeeMode = lblentryMode
                    banksta.PayeeStatus = 2
                    banksta.UserId = _companyInfo.UserId
                    Dim PostString As String = JsonConvert.SerializeObject(banksta)
                    If _JsonSendChq(M_Details.LinkAjaxRequestCheque & "AjaxRequest=10&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        'DataLoad()
                    End If
                    'ChqJson.BankTableSave.BeginInit()
                    'ChqJson.BankTableSave.Rows.Add(Nothing, txtcomapny.Text, txtchqno.Text, txtbanklist.Text, txtpayee.Text, txtdate.EditValue, txtamount.EditValue)
                    'ChqJson.BankTableSave.EndInit()
                    'ChqJson.BankTableSave.AcceptChanges()
                    'If File.Exists(M_Details.AppPath & "\Settings\BankTableSave.xml") Then
                    '    ChqJson.BankTableSave.WriteXml(M_Details.AppPath & "\Settings\BankTableSave.xml")
                    'End If
                End If


            End If
            txtamount.Text = ""
            txtchqno.Text = ""
            txtamtinwords.Text = ""
            txtaltername.Text = ""
            chkalteritem.CheckState = CheckState.Checked
        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub
    Public Function Gen_Report(ByVal ds As DataSet) As Boolean
        Try

            r = New rpta5print
            If File.Exists(M_Details.AppPath & "\Reports\" & txtprintprofile.Text) Then
                r.LoadLayout(M_Details.AppPath & "\Reports\" & txtprintprofile.Text)
            End If
            'dt.Tables(0).TableName = "Table1"
            r.DataSource = ds '.Tables(0)
            'PrintControlJournalrpt.PrintingSystem = r.PrintingSystem
            r.CreateDocument()
            pt = New ReportPrintTool(r)
            pt.ShowPreviewDialog()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub barbtnStatement_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnStatement.ItemClick
        Try
            frmBankStatement.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnpreview_Click(sender As Object, e As EventArgs) Handles btnpreview.Click
        Try
            Dim payeename As String = ""
            If chkalteritem.CheckState = CheckState.Checked Then
                payeename = "**" & txtaltername.Text & "**"
            Else
                payeename = ""
            End If

            Dim ds As New DataSet
            ChqJson.BankTableStatement.Rows.Clear()
            ChqJson.BankTableStatement.BeginInit()
            Dim datformart As String = ""
            Dim refdatformart As String = ""
            _DateConversion(txtdate.EditValue, datformart)
            _Addspace(datformart, refdatformart)
            Dim CurrentCury As String = FormatCurrency(txtamount.EditValue, 2, TriState.False)
            Dim curformated As String = ""
            _CurrencyFormat(CurrentCury, curformated)
            ChqJson.BankTableStatement.Rows.Add(Nothing, txtbanklist.Text, payeename, refdatformart, "**" & curformated & "**", "**" & txtamtinwords.Text & "**")
            ChqJson.BankTableStatement.EndInit()
            ChqJson.BankTableStatement.AcceptChanges()
            If File.Exists(M_Details.AppPath & "\Settings\BankTableStatement.xml") Then
                ChqJson.BankTableStatement.WriteXml(M_Details.AppPath & "\Settings\BankTableStatement.xml")
            End If
            ds.Merge(ChqJson.BankTableStatement)
            If Gen_Report(ds) = True Then
                ds.Tables.Clear()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fd_Click(sender As Object, e As EventArgs) Handles fd.Click
        Try

            ini.WriteValue("Bank", "BankId", txtbanklist.EditValue)
            ini.WriteValue("Profile", "ProfileId", txtprintprofile.SelectedIndex)
            ini.WriteValue("Bank", "CompanyId", txtcomapny.EditValue)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnaddCompany_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnaddCompany.ItemClick
        Try
            frmCompany.ShowDialog()
            DataLoad()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DataLoad()
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            dialog.Caption = "Loading Data..."
            CreateCompanyTable()
            getChqComapnyInfo()
            If ChqJson.CompanyTable.Rows.Count > 0 Then
                txtcomapny.Properties.DataSource = ChqJson.CompanyTable.Select("Active = 1").CopyToDataTable
                'txtcomapny.Properties.DataSource = ChqJson.CompanyTable
                dialog.Caption = "Company Data Loading..."
            End If
            CreateTablePayeeTable()
            getPayeeInfo()
            dialog.Caption = "Payee Data Loading..."
            If ChqJson.PayeeTable.Rows.Count > 0 Then
                txtpayee.Properties.DataSource = ChqJson.PayeeTable.Select("Active = 1").CopyToDataTable
            End If
            chkalteritem.CheckState = CheckState.Checked
        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub
    Private Sub frmChequeprint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DataLoad()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnproductdetails_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnproductdetails.ItemClick
        Try
            frmAbout.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub btnCheckPayRec_CheckedChanged_1(sender As Object, e As EventArgs) Handles btnCheckPayRec.CheckedChanged
        Try
            If btnCheckPayRec.CheckState = CheckState.Checked Then
                lblMode.Text = "Receipt Mode"
                btnCheckPayRec.Text = "Receipt"
                lblentryMode = "In"
                btnprint.Text = "Save"

            Else
                lblMode.Text = "Payment Mode"
                btnCheckPayRec.Text = "Payment"
                lblentryMode = "Out"
                btnprint.Text = "Print && Save"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnToday_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnToday.ItemClick
        Try
            frmTodayCheque.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmChequeprint_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnexit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnexit.ItemClick
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtpayee_EditValueChanged(sender As Object, e As EventArgs) Handles txtpayee.EditValueChanged
        Try
            txtaltername.Text = txtpayee.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnsetting_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnsetting.ItemClick

    End Sub
End Class
Class bankstate
    Public Property CompanyName As String
    Public Property PayeeChq As String
    Public Property BankName As String
    Public Property PayeeName As String
    Public Property PayeeDate As String
    Public Property PayeeAmountDr As String
    Public Property PayeeAmountCr As String
    Public Property PayeeStatus As String
    Public Property PayeeMode As String
    Public Property UserId As String
End Class

