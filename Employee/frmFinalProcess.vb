Imports DevExpress.XtraEditors
Imports Newtonsoft.Json
Imports System.Net
Imports Newtonsoft.Json.Linq
Imports System.Globalization
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
            If System.IO.File.Exists(M_Details._appPath & "\Layout\FinalProcess.xml") Then
                GridView1.RestoreLayoutFromXml(M_Details._appPath & "\Layout\FinalProcess.xml")
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
                                Dim EmpBasic = _rs("EmpBasic") 'ok
                                Dim EmpBasicRate = _rs("EmpBasicRate") 'Ok
                                Dim EmpBasicOTRate = _rs("EmpOtRate") 'Ok
                                Dim EmpOTHrsRate = _rs("EmpOtHrsRate")
                                Dim EmpAllowance = _rs("EmpAllowance")
                                Dim EmpSalesAllowance = _rs("EmpSalesAllowance")
                                Dim EmpSalesCommission = _rs("EmpSalesCommission")
                                Dim EmpEpf = _rs("EmpEpf")
                                Dim EmpSocso = _rs("EmpSocso")
                                Dim EmpNoOfDays = "30" '_rs("EmpNoOfDays") 'ok 'get days based on month and year
                                If DateTime.TryParseExact(EmpMonth, "MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, Nothing) Then
                                    Dim month As Integer = DateTime.ParseExact(EmpMonth, "MMM-yyyy", CultureInfo.InvariantCulture).Month
                                    Dim year As Integer = DateTime.ParseExact(EmpMonth, "MMM-yyyy", CultureInfo.InvariantCulture).Year
                                    EmpNoOfDays = DateTime.DaysInMonth(year, month)
                                Else
                                    EmpNoOfDays = 30 ' Default to 30 if parsing fails
                                End If
                                Dim EmpExtraDays = _rs("EmpExtraDays")
                                Dim EmpExtraOtHrs = _rs("EmpExtraOtHrs")
                                Dim EmpAdvance = _rs("EmpAdvance")
                                Dim EmpDeduction = _rs("EmpDeduction")
                                Dim EmpBank = _rs("EmpBankIn")
                                TotWages = Val(EmpNoOfDays * EmpBasicRate)
                                TotExtraDayAmt = Val(EmpExtraDays * EmpBasicOTRate)
                                TotExtraOTAmt = Val(EmpExtraOtHrs * EmpOTHrsRate)
                                TotGrossAmt = TotWages + TotExtraDayAmt + TotExtraOTAmt + Val(EmpAllowance) + Val(EmpSalesAllowance) + Val(EmpSalesCommission)
                                TotNetPay = TotGrossAmt - EmpAdvance - EmpEpf - EmpSocso - EmpDeduction
                                TotNetCash = TotNetPay - EmpBank
                                _FinalMonthProcessTable.Rows.Add(Sno, 0, EmpRefId, EmpName, EmpMonth, EmpComId, EmpComName, EmpLocId, EmpLocName, EmpBasic, EmpNoOfDays, Math.Round(TotWages, 2), EmpExtraDays, Math.Round(TotExtraDayAmt, 2), EmpExtraOtHrs, Math.Round(TotExtraOTAmt, 2), EmpAllowance, EmpSalesAllowance, EmpSalesCommission, Math.Round(TotGrossAmt, 2), EmpAdvance, EmpEpf, EmpSocso, EmpDeduction, Math.Ceiling(TotNetPay), EmpBank, Math.Ceiling(TotNetCash))
                                Sno = Sno + 1
                            Next
                            _FinalMonthProcessTable.AcceptChanges()
                            _FinalMonthProcessTable.EndInit()
                            GridControl1.DataSource = _FinalMonthProcessTable
                            UpdateEmpSalesCommissionFromMonthlyReport(_selectedMonth, _selectedCompany, _selectedLocation)
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
                            Dim _sno As Integer = 0
                            For Each rs As DataRow In _FinalMonthProcessTable.Rows
                                _sno += 1
                                rs("SNo") = _sno
                            Next
                            'For Each _rs In _dataTable.Rows
                            '    _FinalMonthProcessTable.ImportRow(_dataTable.Rows)
                            'Next
                            _FinalMonthProcessTable.AcceptChanges()
                            _FinalMonthProcessTable.EndInit()
                            GridControl1.DataSource = _FinalMonthProcessTable
                            UpdateEmpSalesCommissionFromMonthlyReport(_selectedMonth, _selectedCompany, _selectedLocation)
                        Else

                            GridControl1.DataSource = Nothing
                            MsgBox("No Data Found", MsgBoxStyle.Information, "Msg")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, "Error Loading", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateEmpSalesCommissionFromMonthlyReport(selectedMonth As String, selectedCompany As String, selectedLocation As String)
        Try
            Dim payoutDate As String = ConvertMonthToFirstDate(selectedMonth)
            If String.IsNullOrWhiteSpace(payoutDate) OrElse _FinalMonthProcessTable Is Nothing OrElse _FinalMonthProcessTable.Rows.Count = 0 Then
                Exit Sub
            End If

            Dim url As String = M_Details.LinkAjaxRequestSyncLocalCloud & "AjaxRequest=18&Date=" & payoutDate & "&ComId=" & selectedCompany & "&LocId=" & selectedLocation
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New WebClient().DownloadString(url)
            Dim parsedJson As JObject = JObject.Parse(json)
            Dim success As String = If(parsedJson("Success") IsNot Nothing, parsedJson("Success").ToString(), "False")
            If success <> "True" Then Exit Sub

            Dim dataToken As JToken = parsedJson("Data")
            If dataToken Is Nothing OrElse dataToken.Type <> JTokenType.Array Then Exit Sub

            Dim commissionByEmpId As New Dictionary(Of String, Double)(StringComparer.OrdinalIgnoreCase)
            Dim netAmtByEmpId As New Dictionary(Of String, Double)(StringComparer.OrdinalIgnoreCase)
            For Each item As JObject In CType(dataToken, JArray)
                Dim empId As String = GetFirstNonEmptyValue(item, New String() {"EmpRefId", "emp_id", "EmpId", "StaffId", "ID"})
                If String.IsNullOrWhiteSpace(empId) Then Continue For

                Dim amountText As String = GetFirstNonEmptyValue(item, New String() {"EmpSalesCommission", "TotalCommission", "Commission", "Amount"})
                Dim amount As Double = 0
                Double.TryParse(amountText, NumberStyles.Any, CultureInfo.InvariantCulture, amount)

                Dim netAmtText As String = GetFirstNonEmptyValue(item, New String() {"TotalNetAmt", "NetAmt", "TotalSales"})
                Dim netAmt As Double = 0
                Double.TryParse(netAmtText, NumberStyles.Any, CultureInfo.InvariantCulture, netAmt)

                If commissionByEmpId.ContainsKey(empId) Then
                    commissionByEmpId(empId) += amount
                    netAmtByEmpId(empId) += netAmt
                Else
                    commissionByEmpId(empId) = amount
                    netAmtByEmpId(empId) = netAmt
                End If
            Next

            For Each row As DataRow In _FinalMonthProcessTable.Rows
                Dim rowEmpId As String = row("EmpRefId").ToString().Trim()
                If commissionByEmpId.ContainsKey(rowEmpId) Then
                    row("EmpSalesCommission") = commissionByEmpId(rowEmpId)
                Else
                    row("EmpSalesCommission") = 0.0
                End If

                If netAmtByEmpId.ContainsKey(rowEmpId) Then
                    row("EmpSalesAllowance") = GetSalesAllowanceByNetAmt(netAmtByEmpId(rowEmpId))
                Else
                    row("EmpSalesAllowance") = 0.0
                End If

                RecalculateFinalProcessAmounts(row)
            Next

            _FinalMonthProcessTable.AcceptChanges()
            GridControl1.RefreshDataSource()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RecalculateFinalProcessAmounts(row As DataRow)
        Dim basicSalary As Double = Val(row("EmpBasic"))
        Dim noOfDays As Double = Val(row("EmpNoOfDays"))
        Dim wages As Double = Val(row("EmpWages"))
        Dim extraDays As Double = Val(row("EmpExtraDays"))
        Dim extraDayAmt As Double = Val(row("EmpExtraDayAmt"))
        Dim extraOtHrs As Double = Val(row("EmpExtraOtHrs"))
        Dim extraOtAmt As Double = Val(row("EmpExtraOtAmt"))
        Dim allowance As Double = Val(row("EmpAllowance"))
        Dim salesAllowance As Double = Val(row("EmpSalesAllowance"))
        Dim salesCommission As Double = Val(row("EmpSalesCommission"))
        Dim advance As Double = Val(row("EmpAdvance"))
        Dim epf As Double = Val(row("EmpEpf"))
        Dim socso As Double = Val(row("EmpSocso"))
        Dim deduction As Double = Val(row("EmpDeduction"))
        Dim bank As Double = Val(row("EmpBank"))

        If wages = 0 AndAlso noOfDays > 0 Then
            wages = noOfDays * (basicSalary / noOfDays)
        End If
        If extraDayAmt = 0 AndAlso extraDays > 0 AndAlso noOfDays > 0 Then
            extraDayAmt = extraDays * (basicSalary / noOfDays)
        End If

        Dim grossAmt As Double = wages + extraDayAmt + extraOtAmt + allowance + salesAllowance + salesCommission
        Dim netPay As Double = grossAmt - advance - epf - socso - deduction
        Dim netCash As Double = netPay - bank

        row("EmpWages") = Math.Round(wages, 2)
        row("EmpExtraDayAmt") = Math.Round(extraDayAmt, 2)
        row("EmpExtraOtAmt") = Math.Round(extraOtAmt, 2)
        row("EmpGrossAmt") = Math.Round(grossAmt, 2)
        row("EmpNetPay") = Math.Ceiling(netPay)
        row("EmpNetCash") = Math.Ceiling(netCash)
    End Sub

    Private Function ConvertMonthToFirstDate(monthText As String) As String
        Try
            If String.IsNullOrWhiteSpace(monthText) Then Return ""

            Dim parsed As DateTime
            If DateTime.TryParseExact(monthText.Trim(), "MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, parsed) Then
                Return New DateTime(parsed.Year, parsed.Month, 1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            End If

            If DateTime.TryParse(monthText.Trim(), parsed) Then
                Return New DateTime(parsed.Year, parsed.Month, 1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            End If
        Catch ex As Exception

        End Try
        Return ""
    End Function

    Private Function GetFirstNonEmptyValue(item As JObject, keys As String()) As String
        For Each key As String In keys
            If item(key) IsNot Nothing Then
                Dim value As String = item(key).ToString().Trim()
                If Not String.IsNullOrWhiteSpace(value) Then
                    Return value
                End If
            End If
        Next
        Return ""
    End Function

    ' Sales allowance tier table based on monthly TotalNetAmt (sales amount)
    ' < 7,000          =   0  |  7,000 - 8,000  = 100  |  8,001 - 9,000  = 200
    ' 9,001 - 10,000   = 300  | 10,001 - 11,000 = 400  | 11,001 - 12,000 = 500
    ' 12,001 - 13,000  = 600  |        > 13,000 = 600
    Private Function GetSalesAllowanceByNetAmt(totalNetAmt As Double) As Double
        If totalNetAmt >= 12001 Then Return 600
        If totalNetAmt >= 11001 Then Return 500
        If totalNetAmt >= 10001 Then Return 400
        If totalNetAmt >= 9001 Then Return 300
        If totalNetAmt >= 8001 Then Return 200
        If totalNetAmt >= 7000 Then Return 100
        Return 0
    End Function

    Private Sub BarBtnSave_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnSave.ItemClick
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            If _FinalMonthProcessTable.Rows.Count > 0 Then
                Dim PostString As String = JsonConvert.SerializeObject(_FinalMonthProcessTable)
                If _JsonSend(M_Details.LinkAjaxRequest & "EmployeeReq=16&json=" & PostString) = True Then
                    _FinalMonthProcessTable.Rows.Clear()
                    dialog.Caption = "Data Saved Success.."
                    MessageBox.Show("Data Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    dialog.Caption = "Data Not Saved"
                    MessageBox.Show("Data Not Saved", "Failed to save", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            If Not System.IO.File.Exists(M_Details._appPath & "\Layout\FinalProcess.xml") Then
                System.IO.Directory.CreateDirectory(M_Details._appPath & "\Layout")
                GridView1.SaveLayoutToXml(M_Details._appPath & "\Layout\FinalProcess.xml")
            Else
                GridView1.SaveLayoutToXml(M_Details._appPath & "\Layout\FinalProcess.xml")
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
            _FinalMonthProcessTable.WriteXml(M_Details._appPath & "\Print\PrintSlip.xml", True)
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
        If cmbmonth IsNot Nothing Then
            _monthofsalary = cmbmonth.Text.Trim()
        End If

    End Sub
    Private Sub CmbBoxPrintProfile(sender As Object, e As EventArgs)
        Dim printProfile As New ComboBoxEdit
        printProfile = TryCast(sender, ComboBoxEdit)
        _printProfile = printProfile.SelectedText.ToString
    End Sub

End Class