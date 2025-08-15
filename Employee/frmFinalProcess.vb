Imports DevExpress.XtraEditors
Imports Newtonsoft.Json
Imports System.Net
Imports Newtonsoft.Json.Linq
Public Class frmFinalProcess
    Dim _FinalMonthProcessTable As New DataTable
    Dim _monthofsalary As String = ""
    Dim _printProfile As String = ""
    Dim _DSFORM As New DataSet
    Private Sub frmFinalProcess_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _DataLoad()
            _FinalMonthProcessTable = CreateFinalTable()
            GridControl1.DataSource = _FinalMonthProcessTable
            AddHandler RepositoryItemComboBoxFinalMont.SelectedIndexChanged, AddressOf MonthItemCmbBox
            AddHandler RepositoryItemComboBoxPrintProfile.SelectedIndexChanged, AddressOf CmbBoxPrintProfile
            If System.IO.File.Exists(M_Details.AppPath & "\Layout\FinalProcess.xml") Then
                GridView1.RestoreLayoutFromXml(M_Details.AppPath & "\Layout\FinalProcess.xml")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _DataLoad()
        Try
            getMonthOfSalaryInfo()
            If _JsonData.MonthOfSalary.Rows.Count > 0 Then
                Dim dtrows As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MonthOfSalary Where dtrow("Active") = "Active"
                If dtrows.Any Then
                    For Each row In dtrows
                        RepositoryItemComboBoxFinalMont.Items.Add(row("MonthName"))
                    Next

                End If
            End If
            _DSFORM.ReadXml(AppDomain.CurrentDomain.BaseDirectory & "\Print\PRINTFORMAT.XML")
            Dim tA As DataTable
            tA = _DSFORM.Tables(0)
            For Each _ROW As DataRow In tA.Rows
                RepositoryItemComboBoxPrintProfile.Items.Add(_ROW(1))
            Next
        Catch ex As Exception

        End Try
    End Sub
   

    Private Sub BarBtnSearch_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnSearch.ItemClick
        Try
            If String.IsNullOrEmpty(_monthofsalary) Then
                MsgBox("Select Month", MsgBoxStyle.Information, "Msg")
            Else
                Dim _dataTable As New DataTable
                Dim _selectedCompany As String = _companyInfo.ComId
                Dim _selectedLocation As String = _companyInfo.LocId
                Dim _selectedMonth As String = _monthofsalary
                Dim monofsalary As New monthofsalarycheck
                monofsalary.pemp_comid = _selectedCompany
                monofsalary.pemp_locid = _selectedLocation
                monofsalary.pemp_month = _selectedMonth
                Dim PostString As String = JsonConvert.SerializeObject(monofsalary)
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "EmployeeReq=17&json=" & PostString)
                Dim Userparsejson As JObject = JObject.Parse(json)
                Dim Success = Userparsejson("Success").ToString
                Dim Msg = Userparsejson("Msg").ToString
                _dataTable = Userparsejson("Data").ToObject(Of DataTable)()
                Dim TotWages As Double = 0
                Dim TotExtraDayAmt As Double = 0
                Dim TotExtraOTAmt As Double = 0
                Dim TotGrossAmt As Double = 0
                Dim TotNetPay As Double = 0
                Dim TotNetCash As Double = 0
                If Success.ToString = "True" Then
                    If Msg.ToString = "0" Then

                        If _dataTable.Rows.Count > 0 Then
                            Dim Sno As Integer = 1
                            _FinalMonthProcessTable.Rows.Clear()
                            _FinalMonthProcessTable.BeginInit()
                            For Each _rs In _dataTable.Rows
                                Dim EmpTrId = _rs("EmpTrId")
                                Dim EmpRefId = _rs("EmpRefId")
                                Dim EmpName = _rs("EmpName")
                                Dim EmpMonth = _monthofsalary
                                Dim EmpComId = _rs("EmpComId")
                                Dim EmpComName = _rs("EmpComName")
                                Dim EmpLocId = _rs("EmpLocId")
                                Dim EmpLocName = _rs("EmpLocName")
                                Dim EmpBasic = _rs("EmpBasic")
                                Dim EmpNoOfDays = _rs("EmpNoOfDays")
                                Dim EmpBasicRate = _rs("EmpBasicRate")
                                Dim EmpExtraDays = _rs("EmpExtraDays")
                                Dim ExtraDayRate = _rs("ExtraDayRate")
                                Dim EmpExtraOtHrs = _rs("EmpExtraOtHrs")
                                Dim EmpOTHrsRate = _rs("EmpHrsRate")
                                Dim EmpAdvance = _rs("EmpAdvance")
                                Dim EmpAllowance = _rs("EmpAllowance")
                                Dim EmpEpf = _rs("EmpEpf")
                                Dim EmpSocso = _rs("EmpSocso")
                                Dim EmpDeduction = _rs("EmpDeduction")
                                Dim EmpBank = _rs("EmpBank")
                                TotWages = Val(EmpNoOfDays * EmpBasicRate)
                                TotExtraDayAmt = Val(EmpExtraDays * ExtraDayRate)
                                TotExtraOTAmt = Val(EmpExtraOtHrs * EmpOTHrsRate)
                                TotGrossAmt = TotWages + TotExtraDayAmt + TotExtraOTAmt + Val(EmpAllowance)
                                TotNetPay = TotGrossAmt - EmpAdvance - EmpEpf - EmpSocso - EmpDeduction
                                TotNetCash = TotNetPay - EmpBank
                                _FinalMonthProcessTable.Rows.Add(Sno, 0, EmpRefId, EmpName, EmpMonth, EmpComId, EmpComName, EmpLocId, EmpLocName, EmpBasic, EmpNoOfDays, Math.Round(TotWages, 0), EmpExtraDays, Math.Round(TotExtraDayAmt, 0), EmpExtraOtHrs, Math.Round(TotExtraOTAmt, 0), EmpAllowance, Math.Round(TotGrossAmt, 0), EmpAdvance, EmpEpf, EmpSocso, EmpDeduction, Math.Round(TotNetPay, 0), EmpBank, Math.Round(TotNetCash, 0))
                                Sno = Sno + 1
                            Next
                            _FinalMonthProcessTable.AcceptChanges()
                            _FinalMonthProcessTable.EndInit()
                            GridControl1.DataSource = _FinalMonthProcessTable
                        Else

                            GridControl1.DataSource = Nothing
                            MsgBox("No Data Found", MsgBoxStyle.Information, "Msg")
                        End If
                    Else
                        If _dataTable.Rows.Count > 0 Then
                            _FinalMonthProcessTable.Rows.Clear()
                            _FinalMonthProcessTable.BeginInit()
                            For Each r As DataRow In _dataTable.Rows
                                _FinalMonthProcessTable.ImportRow(r)

                            Next
                            For Each rs In _FinalMonthProcessTable.Rows
                                rs("SNo") = rs("SNo") + 1
                            Next
                            'For Each _rs In _dataTable.Rows
                            '    _FinalMonthProcessTable.ImportRow(_dataTable.Rows)
                            'Next
                            _FinalMonthProcessTable.AcceptChanges()
                            _FinalMonthProcessTable.EndInit()
                            GridControl1.DataSource = _FinalMonthProcessTable
                        Else

                            GridControl1.DataSource = Nothing
                            MsgBox("No Data Found", MsgBoxStyle.Information, "Msg")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BarBtnSave_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnSave.ItemClick
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            If _FinalMonthProcessTable.Rows.Count > 0 Then
                Dim PostString As String = JsonConvert.SerializeObject(_FinalMonthProcessTable)
                If _JsonSend(M_Details.LinkAjaxRequest & "EmployeeReq=16&json=" & PostString) = True Then
                    _FinalMonthProcessTable.Rows.Clear()
                    dialog.Caption = "Data Saved Success.."
                Else
                    dialog.Caption = "Data Not Saved"
                End If
            End If
        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub

    Private Sub BarBtnSaveLayout_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnSaveLayout.ItemClick
        Try
            If Not System.IO.File.Exists(M_Details.AppPath & "\Layout\FinalProcess.xml") Then
                System.IO.Directory.CreateDirectory(M_Details.AppPath & "\Layout")
                GridView1.SaveLayoutToXml(M_Details.AppPath & "\Layout\FinalProcess.xml")
            Else
                GridView1.SaveLayoutToXml(M_Details.AppPath & "\Layout\FinalProcess.xml")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BarBtnReset_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnReset.ItemClick
        Try
            _FinalMonthProcessTable.Rows.Clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BarBtnDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnDelete.ItemClick
        Try
            Dim _selectedCompany As String = _companyInfo.ComId
            Dim _selectedLocation As String = _companyInfo.LocId
            Dim _selectedMonth As String = _monthofsalary
            Dim monofsalary As New monthofsalarycheck
            monofsalary.pemp_comid = _selectedCompany
            monofsalary.pemp_locid = _selectedLocation
            monofsalary.pemp_month = _selectedMonth
            If String.IsNullOrEmpty(_selectedMonth) Then
                MessageBox.Show("Data Select Cant Be Empty " & _selectedMonth, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Try
            End If
            Dim dialresult = MessageBox.Show("Do you want delete this month " & _monthofsalary, "Msg", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If dialresult = Windows.Forms.DialogResult.Yes Then
                Dim PostString As String = JsonConvert.SerializeObject(monofsalary)
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "EmployeeReq=18&json=" & PostString)
                Dim Userparsejson As JObject = JObject.Parse(json)
                Dim Success = Userparsejson("Success").ToString
                Dim Msg = Userparsejson("Msg").ToString
                If Success.ToString = "True" Then
                    MsgBox(Msg, MsgBoxStyle.OkOnly, "Msg")
                    _FinalMonthProcessTable.Rows.Clear()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ExportDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportDataToolStripMenuItem.Click
        Try
            If GridView1.RowCount > 0 Then
                GridControl1.ShowRibbonPrintPreview()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GenerateSlipToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenerateSlipToolStripMenuItem.Click
        printa(True)
    End Sub
    Public Function printa(ByVal b As Boolean) As Boolean
        Try
            Dim ST As String = _printProfile
            If String.IsNullOrEmpty(ST) Then
                MsgBox("Select Print Profile", MsgBoxStyle.OkOnly, "Msg")
                Return False
            End If
            Dim _rptstaf As New rptStaffProfile
            _rptstaf.LoadLayout(AppDomain.CurrentDomain.BaseDirectory & "\Print\" & ST.ToString)
            _rptstaf.DataSource = _FinalMonthProcessTable
            _FinalMonthProcessTable.WriteXml(M_Details.AppPath & "\Print\PrintSlip.xml", True)
            Dim pt As New DevExpress.XtraReports.UI.ReportPrintTool(_rptstaf)
            If b = True Then
                pt.ShowPreviewDialog()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub MonthItemCmbBox(sender As Object, e As EventArgs)
        Dim cmbmonth As New ComboBoxEdit
        cmbmonth = TryCast(sender, ComboBoxEdit)
        _monthofsalary = cmbmonth.SelectedText.ToString

    End Sub
    Private Sub CmbBoxPrintProfile(sender As Object, e As EventArgs)
        Dim printProfile As New ComboBoxEdit
        printProfile = TryCast(sender, ComboBoxEdit)
        _printProfile = printProfile.SelectedText.ToString
    End Sub

End Class