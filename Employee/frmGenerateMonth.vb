Imports DevExpress.XtraEditors
Imports Newtonsoft.Json
Imports System.Net
Imports Newtonsoft.Json.Linq
Imports System.Globalization

Public Class frmGenerateMonth
    Dim _monthProcessTable As New DataTable
    Dim _monthofsalary As String = ""
    Dim dictData As New Dictionary(Of String, Object)
    Private Sub frmGenerateMonth_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _DataLoad()
            _monthProcessTable = CreateMonthTable()
            GridControl1.DataSource = _monthProcessTable
            AddHandler RepositoryItemComboBoxMonth.SelectedIndexChanged, AddressOf MonthItemCmbBox

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
                        RepositoryItemComboBoxMonth.Items.Add(row("MonthName"))
                    Next

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnbarsearch_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnbarsearch.ItemClick
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
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "EmployeeReq=14&json=" & PostString)
                Dim Userparsejson As JObject = JObject.Parse(json)
                Dim Success = Userparsejson("Success").ToString
                Dim Msg = Userparsejson("Msg").ToString
                _dataTable = Userparsejson("Data").ToObject(Of DataTable)()
                If Success.ToString = "True" Then
                    If Msg.ToString = "0" Then
                        Dim Sno As Integer = 1
                        If _dataTable.Rows.Count > 0 Then
                            _monthProcessTable.Rows.Clear()
                            _monthProcessTable.BeginInit()
                            For Each _rs In _dataTable.Rows
                                _monthProcessTable.Rows.Add(Sno, 0, _rs("emp_id"), _rs("emp_printname"), _selectedMonth, _selectedCompany, _companyInfo.CompanyName, _selectedLocation, _companyInfo.LocationName, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0)
                                Sno = Sno + 1
                            Next
                            _monthProcessTable.AcceptChanges()
                            _monthProcessTable.EndInit()
                            GridControl1.DataSource = _monthProcessTable
                            UpdateEmpAdvanceFromMonthlyPayout(_selectedMonth, _selectedCompany, _selectedLocation)
                        Else

                            GridControl1.DataSource = Nothing
                            MsgBox("No Data Found", MsgBoxStyle.Information, "Msg")
                        End If
                    Else
                        Dim Sno As Integer = 1
                        If _dataTable.Rows.Count > 0 Then
                            _monthProcessTable.Rows.Clear()
                            _monthProcessTable.BeginInit()
                            For Each _rs In _dataTable.Rows
                                _monthProcessTable.Rows.Add(Sno, _rs("EmpTrId"), _rs("EmpRefId"), _rs("EmpName"), _rs("EmpMonth"), _rs("EmpComId"), _rs("EmpComName"), _rs("EmpLocId"), _rs("EmpLocName"), _rs("EmpNoOfDays"), _rs("EmpExtraDays"), _rs("EmpExtraOtHrs"), _rs("EmpAdvance"), _rs("EmpDeduction"), _rs("EmpBAnkIn"))
                                Sno = Sno + 1
                            Next
                            _monthProcessTable.AcceptChanges()
                            _monthProcessTable.EndInit()
                            GridControl1.DataSource = _monthProcessTable
                            UpdateEmpAdvanceFromMonthlyPayout(_selectedMonth, _selectedCompany, _selectedLocation)
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

    Private Sub UpdateEmpAdvanceFromMonthlyPayout(selectedMonth As String, selectedCompany As String, selectedLocation As String)
        Try
            Dim payoutDate As String = ConvertMonthToFirstDate(selectedMonth)
            If String.IsNullOrWhiteSpace(payoutDate) OrElse _monthProcessTable Is Nothing OrElse _monthProcessTable.Rows.Count = 0 Then
                Exit Sub
            End If

            Dim url As String = M_Details.LinkAjaxRequestSyncLocalCloud & "AjaxRequest=17&Date=" & payoutDate & "&ComId=" & selectedCompany & "&LocId=" & selectedLocation
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New WebClient().DownloadString(url)
            Dim parsedJson As JObject = JObject.Parse(json)
            Dim success As String = If(parsedJson("Success") IsNot Nothing, parsedJson("Success").ToString(), "False")
            If success <> "True" Then Exit Sub

            Dim dataToken As JToken = parsedJson("Data")
            If dataToken Is Nothing OrElse dataToken.Type <> JTokenType.Array Then Exit Sub

            Dim advanceByEmpId As New Dictionary(Of String, Double)(StringComparer.OrdinalIgnoreCase)
            For Each item As JObject In CType(dataToken, JArray)
                Dim empId As String = GetFirstNonEmptyValue(item, New String() {"EmpRefId", "emp_id", "EmpId", "StaffId", "payd_refid", "ID"})
                If String.IsNullOrWhiteSpace(empId) Then Continue For

                Dim amountText As String = GetFirstNonEmptyValue(item, New String() {"EmpAdvance", "advance", "Advance", "AdvanceAmount", "Amount", "payd_amount", "total_advance", "TotalAmount"})
                Dim amount As Double = 0
                Double.TryParse(amountText, NumberStyles.Any, CultureInfo.InvariantCulture, amount)

                If advanceByEmpId.ContainsKey(empId) Then
                    advanceByEmpId(empId) += amount
                Else
                    advanceByEmpId(empId) = amount
                End If
            Next

            For Each row As DataRow In _monthProcessTable.Rows
                Dim rowEmpId As String = row("EmpRefId").ToString().Trim()
                If advanceByEmpId.ContainsKey(rowEmpId) Then
                    row("EmpAdvance") = advanceByEmpId(rowEmpId)
                Else
                    row("EmpAdvance") = 0.0
                End If
            Next

            _monthProcessTable.AcceptChanges()
            GridControl1.RefreshDataSource()
        Catch ex As Exception

        End Try
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

    Private Sub MonthItemCmbBox(sender As Object, e As EventArgs)
        Dim cmbmonth As New ComboBoxEdit
        cmbmonth = TryCast(sender, ComboBoxEdit)
        If cmbmonth IsNot Nothing Then
            _monthofsalary = cmbmonth.Text.Trim()
        End If

    End Sub


    Private Sub BarBtnSave_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnSave.ItemClick
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            If _monthProcessTable.Rows.Count > 0 Then
                Dim PostString As String = JsonConvert.SerializeObject(_monthProcessTable)
                If _JsonSend(M_Details.LinkAjaxRequest & "EmployeeReq=15&json=" & PostString) = True Then
                    _monthProcessTable.Rows.Clear()
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

    Private Sub BarBtnReset_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnReset.ItemClick
        Try
            _monthProcessTable.Rows.Clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TranferLocationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TranferLocationToolStripMenuItem.Click
        Try
            Dim _selectedCompany As String = _companyInfo.ComId
            Dim _selectedLocation As String = _companyInfo.LocId
            Dim _selectedMonth As String = _monthofsalary
            Dim EmpTrid As String = GridView1.GetFocusedRowCellValue("EmpTrId")
            Dim EmpId As String = GridView1.GetFocusedRowCellValue("EmpRefId")
            Dim EmpName As String = GridView1.GetFocusedRowCellValue("EmpName")
            If String.IsNullOrEmpty(_selectedMonth) OrElse String.IsNullOrEmpty(EmpTrid) OrElse String.IsNullOrEmpty(EmpId) Then
                MessageBox.Show("Data Select Cant Be Empty " & EmpName, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Try
            End If
            Dim monofsalary As New monthofsalarycheck
            monofsalary.pemp_comid = _selectedCompany
            monofsalary.pemp_locid = _selectedLocation
            monofsalary.pemp_month = _selectedMonth
            monofsalary.pemp_trid = EmpTrid
            monofsalary.pemp_empid = EmpId
            Dim dialresult = MessageBox.Show("Do you want Transfer this name " & EmpName, "Msg", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If dialresult = Windows.Forms.DialogResult.Yes Then
                Dim PostString As String = JsonConvert.SerializeObject(monofsalary)
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "EmployeeReq=19&json=" & PostString)
                Dim Userparsejson As JObject = JObject.Parse(json)
                Dim Success = Userparsejson("Success").ToString
                Dim Msg = Userparsejson("Msg").ToString
                If Success.ToString = "True" Then
                    MsgBox(Msg, MsgBoxStyle.OkOnly, "Msg")
                    GridView1.DeleteRow(GridView1.FocusedRowHandle)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ExportDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportDataToolStripMenuItem.Click

        Try
            If GridView1.RowCount > 0 Then
                GridControl1.ShowPrintPreview()
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class

Public Class monthofsalarycheck
    Public Property pemp_comid As String
    Public Property pemp_locid As String
    Public Property pemp_month As String
    Public Property pemp_trid As String
    Public Property pemp_empid As String
End Class