Imports System.Data.SqlClient
Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class FrmGenerateTaxReport
    ' Private dbcls As New dbClass
    Private progressForm As Form
    Private progressLabel As Label
    Private progressBar As ProgressBar
    Private progressTotal As Integer
    Private _detailNetAmtMap As Dictionary(Of String, Decimal) = New Dictionary(Of String, Decimal)(StringComparer.OrdinalIgnoreCase)

    Private Sub StartProgressDialog(ByVal title As String, ByVal total As Integer)
        CloseProgressDialog()

        progressTotal = Math.Max(0, total)
        progressForm = New Form()
        progressForm.Text = title
        progressForm.FormBorderStyle = FormBorderStyle.FixedDialog
        progressForm.StartPosition = FormStartPosition.CenterParent
        progressForm.MinimizeBox = False
        progressForm.MaximizeBox = False
        progressForm.ControlBox = False
        progressForm.TopMost = True
        progressForm.Width = 420
        progressForm.Height = 130

        progressLabel = New Label()
        progressLabel.Left = 12
        progressLabel.Top = 10
        progressLabel.Width = 380
        progressLabel.Text = "Starting..."

        progressBar = New ProgressBar()
        progressBar.Left = 12
        progressBar.Top = 38
        progressBar.Width = 380
        progressBar.Height = 24

        If progressTotal > 0 Then
            progressBar.Style = ProgressBarStyle.Continuous
            progressBar.Minimum = 0
            progressBar.Maximum = progressTotal
            progressBar.Value = 0
        Else
            progressBar.Style = ProgressBarStyle.Marquee
        End If

        progressForm.Controls.Add(progressLabel)
        progressForm.Controls.Add(progressBar)
        progressForm.Show(Me)
        progressForm.Refresh()
        Application.DoEvents()
    End Sub

    Private Sub UpdateProgressDialog(ByVal current As Integer, ByVal message As String)
        If progressForm Is Nothing OrElse progressForm.IsDisposed Then
            Return
        End If

        progressLabel.Text = message

        If progressBar.Style <> ProgressBarStyle.Marquee Then
            Dim safeValue As Integer = Math.Min(Math.Max(current, progressBar.Minimum), progressBar.Maximum)
            progressBar.Value = safeValue
        End If

        progressForm.Refresh()
        Application.DoEvents()
    End Sub

    Private Sub CloseProgressDialog()
        If progressForm IsNot Nothing Then
            If Not progressForm.IsDisposed Then
                progressForm.Close()
                progressForm.Dispose()
            End If
        End If
        progressForm = Nothing
        progressLabel = Nothing
        progressBar = Nothing
        progressTotal = 0
    End Sub

    Private Sub GetPaymentList()
        ' Code to retrieve payment list
        'Try
        '    Dim Ds As New DataSet
        '    Dim sql(2) As SqlParameter
        '    sql(0) = New SqlParameter("@mode", "paymode")
        '    sql(1) = New SqlParameter("@ComIds", 0)
        '    sql(2) = New SqlParameter("@LocIds", 0)
        '    Ds = dbcls._sqlDataAdapter("sp_general_query", sql, "0")
        '    If Ds.Tables(0).Rows.Count > 0 Then
        '        For Each dr As DataRow In Ds.Tables(0).Rows
        '            Dim pph_id As Integer = Convert.ToInt32(dr("pph_id"))
        '            Dim pph_name As String = dr("pph_name").ToString()
        '            ListBoxControlPayment.Items.Add(New KeyValuePair(Of String, Integer)(pph_name, pph_id))
        '        Next
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Private Function GetNoOfRowsValue() As Integer
        Dim value As Integer = 0
        Integer.TryParse(txtNoofRow.Text, value)
        Return value
    End Function

    Private Function FormatApiDate(ByVal value As Object) As String
        Try
            If value Is Nothing OrElse Convert.IsDBNull(value) Then
                Return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            End If
            Dim dt As DateTime = Convert.ToDateTime(value)
            Return dt.ToString("yyyy-MM-dd HH:mm:ss")
        Catch
            Return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        End Try
    End Function

    Private Function BuildTaxAuditUrl(ByVal ajaxRequest As Integer, ByVal jsonPayload As String) As String
        Dim baseUrl As String = M_Details.LinkTaxAuditRequest
        If String.IsNullOrWhiteSpace(baseUrl) Then
            baseUrl = M_Details.LinkAjaxRequest
        End If

        If String.IsNullOrWhiteSpace(baseUrl) Then
            Return String.Empty
        End If

        If baseUrl.Contains("?") Then
            If Not baseUrl.EndsWith("?") AndAlso Not baseUrl.EndsWith("&") Then
                baseUrl &= "&"
            End If
        Else
            If Not baseUrl.EndsWith("?") Then
                baseUrl &= "?"
            End If
        End If

        Return baseUrl & "AjaxRequest=" & ajaxRequest.ToString() & "&json=" & Uri.EscapeDataString(jsonPayload)
    End Function

    Private Function ExecuteTaxAuditApi(ByVal mode As String,
                                        ByVal noOfRows As Integer,
                                        ByVal billNo As Integer,
                                        ByRef isSuccess As Boolean,
                                        ByRef message As String) As DataTable
        Dim dt As New DataTable()
        isSuccess = False
        message = String.Empty

        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim payload = New With {
                .Mode = mode,
                .FromDate = FormatApiDate(FromDateEdit.EditValue),
                .ToDate = FormatApiDate(ToDateEdit.EditValue),
                .NoOfRows = noOfRows,
                .BillNo = billNo,
                .ComId = _companyInfo.ComId,
                .LocId = _companyInfo.LocId
            }

            Dim jsonPayload As String = JsonConvert.SerializeObject(payload)
            Dim url As String = BuildTaxAuditUrl(4, jsonPayload)
            If String.IsNullOrWhiteSpace(url) Then
                message = "Tax audit URL is empty. Configure UrlLinkTaxAudit in settings."
                Return dt
            End If

            Dim response As String = New WebClient().DownloadString(url)
            Dim responseObj As JObject = JObject.Parse(response)

            If responseObj("Success") IsNot Nothing Then
                Boolean.TryParse(responseObj("Success").ToString(), isSuccess)
            End If

            message = If(responseObj("Msg") IsNot Nothing, responseObj("Msg").ToString(), String.Empty)

            If isSuccess AndAlso responseObj("Data") IsNot Nothing AndAlso responseObj("Data").Type <> JTokenType.Null Then
                dt = responseObj("Data").ToObject(Of DataTable)()
            End If
        Catch ex As Exception
            isSuccess = False
            message = ex.Message
        End Try

        Return dt
    End Function

    Private Sub GetHeaderInfo(Optional ByVal showProgress As Boolean = False)
        ' Code to retrieve header information
        Try
            Dim apiOk As Boolean = False
            Dim apiMsg As String = String.Empty
            Dim dtHeader As DataTable = ExecuteTaxAuditApi("Header", GetNoOfRowsValue(), 0, apiOk, apiMsg)
            If apiOk AndAlso dtHeader IsNot Nothing AndAlso dtHeader.Rows.Count > 0 Then
                If Not dtHeader.Columns.Contains("Selected") Then
                    dtHeader.Columns.Add("Selected", GetType(Boolean))
                End If

                Dim minRows As Integer = 0
                Integer.TryParse(txtNoofRow.Text, minRows)

                ' Performance fix: load all detail rows once and count in memory.
                Dim detailCountByInvoice = GetDetailCountMap()
                Dim totalRows As Integer = dtHeader.Rows.Count
                Dim eligibleRows As Integer = 0
                Dim processedRows As Integer = 0
                Dim maxDeleteAmount As Decimal = GetMaximumDeleteAmount()

                If showProgress Then
                    StartProgressDialog("Search Progress", totalRows)
                End If

                ' Check each row for processing eligibility
                For Each dr As DataRow In dtHeader.Rows
                    dr("Selected") = False
                    Dim invoiceNo As String = dr("Trno").ToString().Trim()
                    Dim itemCount As Integer = 0
                    If detailCountByInvoice.ContainsKey(invoiceNo) Then
                        itemCount = detailCountByInvoice(invoiceNo)
                    End If

                    Dim paymentValue As String = dr("Payment").ToString().Trim()
                    Dim printStatusValue As String = "No Print"
                    If dtHeader.Columns.Contains("PrintStatus") Then
                        printStatusValue = dr("PrintStatus").ToString().Trim()
                    End If
                    Dim netAmount As Decimal = ParseDecimalValue(dr("NetAmt").ToString(), 0D)

                    If IsEligibleForProcess(paymentValue, printStatusValue, itemCount, minRows, netAmount, maxDeleteAmount) Then
                        dr("CanProcess") = "Yes"
                        eligibleRows += 1
                    Else
                        dr("CanProcess") = "No"
                    End If

                    processedRows += 1
                    If showProgress AndAlso (processedRows Mod 10 = 0 OrElse processedRows = totalRows) Then
                        UpdateProgressDialog(processedRows, "Preparing search results... " & processedRows.ToString() & "/" & totalRows.ToString())
                    End If
                Next

                lblstatusgroup.Text = "Details : Total Records " & totalRows.ToString() & " | Processable " & eligibleRows.ToString()

                GridControlHeader.DataSource = dtHeader
                GridViewHeader.BestFitColumns()
                GetHeaderNetAmt()
                GetDetailsNetAmt()
            Else
                lblHeaderNetAmt.Text = "Net Amount: 0"
                GridControlHeader.DataSource = Nothing
                lblstatusgroup.Text = "Details : Total Records 0 | Processable 0 | No Data Found Process the Data Selected Date"
                If Not apiOk AndAlso Not String.IsNullOrWhiteSpace(apiMsg) Then
                    MessageBox.Show(apiMsg, "Tax Audit", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, "Err", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If showProgress Then
                CloseProgressDialog()
            End If
        End Try
    End Sub
    Private Function GetDetailCountMap() As Dictionary(Of String, Integer)
        Dim counts As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)

        Try
            Dim apiOk As Boolean = False
            Dim apiMsg As String = String.Empty
            Dim dtDetails As DataTable = ExecuteTaxAuditApi("Detail", GetNoOfRowsValue(), 0, apiOk, apiMsg)

            If Not apiOk OrElse dtDetails Is Nothing OrElse dtDetails.Rows.Count = 0 Then
                _detailNetAmtMap = New Dictionary(Of String, Decimal)(StringComparer.OrdinalIgnoreCase)
                Return counts
            End If

            Dim netAmtMap As New Dictionary(Of String, Decimal)(StringComparer.OrdinalIgnoreCase)

            For Each dr As DataRow In dtDetails.Rows
                Dim invoiceNo As String = dr("Trno").ToString().Trim()
                If invoiceNo = String.Empty Then
                    Continue For
                End If

                If counts.ContainsKey(invoiceNo) Then
                    counts(invoiceNo) += 1
                Else
                    counts.Add(invoiceNo, 1)
                End If

                Dim rowNetAmt As Decimal = 0D
                If dtDetails.Columns.Contains("NetAmt") Then
                    rowNetAmt = ParseDecimalValue(dr("NetAmt").ToString(), 0D)
                End If

                If netAmtMap.ContainsKey(invoiceNo) Then
                    netAmtMap(invoiceNo) += rowNetAmt
                Else
                    netAmtMap.Add(invoiceNo, rowNetAmt)
                End If
            Next

            _detailNetAmtMap = netAmtMap
        Catch ex As Exception
            _detailNetAmtMap = New Dictionary(Of String, Decimal)(StringComparer.OrdinalIgnoreCase)
            Return counts
        End Try

        Return counts
    End Function
    Private Sub GetDetailsNetAmt()

        ' Code to retrieve detail net amount
        Try
            Dim apiOk As Boolean = False
            Dim apiMsg As String = String.Empty
            Dim dtNet As DataTable = ExecuteTaxAuditApi("DetNetAmt", GetNoOfRowsValue(), 0, apiOk, apiMsg)
            If apiOk AndAlso dtNet IsNot Nothing AndAlso dtNet.Rows.Count > 0 Then
                Dim totalNetAmount As Decimal = 0D
                For Each dr As DataRow In dtNet.Rows
                    totalNetAmount += ParseDecimalValue(dr("NetAmt").ToString(), 0D)
                Next
                lblDetailNetAmt.Text = "Net Amount: " & totalNetAmount.ToString("0.00")

                ' If the API returns a per-Trno breakdown, bulk-populate the mismatch map
                If dtNet.Columns.Contains("Trno") AndAlso dtNet.Columns.Contains("NetAmt") Then
                    For Each dr As DataRow In dtNet.Rows
                        Dim trnoKey As String = dr("Trno").ToString().Trim()
                        If Not String.IsNullOrWhiteSpace(trnoKey) Then
                            _detailNetAmtMap(trnoKey) = ParseDecimalValue(dr("NetAmt").ToString(), 0D)
                        End If
                    Next
                    GridViewHeader.RefreshData()
                End If
            Else
                lblDetailNetAmt.Text = "Net Amount: 0"
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GetHeaderNetAmt()

        ' Code to retrieve detail net amount
        Try
            Dim apiOk As Boolean = False
            Dim apiMsg As String = String.Empty
            Dim dtNet As DataTable = ExecuteTaxAuditApi("HeadNetAmt", GetNoOfRowsValue(), 0, apiOk, apiMsg)
            If apiOk AndAlso dtNet IsNot Nothing AndAlso dtNet.Rows.Count > 0 Then
                Dim totalNetAmount As Decimal = 0D
                For Each dr As DataRow In dtNet.Rows
                    totalNetAmount += ParseDecimalValue(dr("NetAmt").ToString(), 0D)
                Next
                lblHeaderNetAmt.Text = "Net Amount: " & totalNetAmount.ToString("0.00")
            Else
                lblHeaderNetAmt.Text = "Net Amount: 0"
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function GetDetailItemCount(ByVal InvoiceNo As String) As Integer
        ' Function to get detail item count for a specific invoice
        Try
            Dim billNo As Integer = 0
            Integer.TryParse(InvoiceNo, billNo)

            Dim apiOk As Boolean = False
            Dim apiMsg As String = String.Empty
            Dim dtDetails As DataTable = ExecuteTaxAuditApi("Detail", GetNoOfRowsValue(), billNo, apiOk, apiMsg)
            If apiOk AndAlso dtDetails IsNot Nothing AndAlso dtDetails.Rows.Count > 0 Then
                Return dtDetails.Rows.Count
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ' Rows the user has ticked via the "Selected" checkbox column, independent of native row highlighting.
    Private Function GetCheckedRowHandles() As Integer()
        Dim checkedHandles As New List(Of Integer)
        For i As Integer = 0 To GridViewHeader.RowCount - 1
            Dim cellValue As Object = GridViewHeader.GetRowCellValue(i, "Selected")
            If cellValue IsNot Nothing AndAlso Not Convert.IsDBNull(cellValue) AndAlso Convert.ToBoolean(cellValue) Then
                checkedHandles.Add(i)
            End If
        Next
        Return checkedHandles.ToArray()
    End Function

    Private Function GetHeaderFieldValue(ByVal rowHandle As Integer, ByVal fieldName As String, Optional ByVal defaultValue As String = "") As String
        Try
            If GridViewHeader.Columns.ColumnByFieldName(fieldName) Is Nothing Then
                Return defaultValue
            End If

            Dim valueObj As Object = GridViewHeader.GetRowCellValue(rowHandle, fieldName)
            If valueObj Is Nothing OrElse Convert.IsDBNull(valueObj) Then
                Return defaultValue
            End If

            Return valueObj.ToString().Trim()
        Catch
            Return defaultValue
        End Try
    End Function

    Private Function ParseDecimalValue(ByVal valueText As String, Optional ByVal defaultValue As Decimal = 0D) As Decimal
        If String.IsNullOrWhiteSpace(valueText) Then
            Return defaultValue
        End If

        Dim parsed As Decimal
        Dim normalized As String = valueText.Replace(",", "").Trim()
        If Decimal.TryParse(normalized, parsed) Then
            Return parsed
        End If

        Return defaultValue
    End Function

    Private Function GetMaximumDeleteAmount() As Decimal
        Return ParseDecimalValue(txtsetlimit.Text, 0D)
    End Function

    Private Function GetCurrentHeaderNetAmount() As Decimal
        Try
            Dim raw As String = lblHeaderNetAmt.Text
            If String.IsNullOrWhiteSpace(raw) Then
                Return 0D
            End If

            Dim valueText As String = raw
            If raw.StartsWith("Net Amount:", StringComparison.OrdinalIgnoreCase) Then
                valueText = raw.Substring("Net Amount:".Length).Trim()
            End If
            Return ParseDecimalValue(valueText, 0D)
        Catch
            Return 0D
        End Try
    End Function

    Private Function GetCurrentDetailNetAmount() As Decimal
        Try
            Dim raw As String = lblDetailNetAmt.Text
            If String.IsNullOrWhiteSpace(raw) Then
                Return 0D
            End If
            Dim valueText As String = raw
            If raw.StartsWith("Net Amount:", StringComparison.OrdinalIgnoreCase) Then
                valueText = raw.Substring("Net Amount:".Length).Trim()
            End If
            Return ParseDecimalValue(valueText, 0D)
        Catch
            Return 0D
        End Try
    End Function

    Private Function GetEligibilityFailureReason(ByVal paymentValue As String, ByVal printStatusValue As String, ByVal itemCount As Integer, ByVal minRows As Integer, ByVal netAmount As Decimal, ByVal maxDeleteAmount As Decimal) As String
        Dim normalizedPayment As String = paymentValue.Trim()

        Dim hasMultiplePayment As Boolean = normalizedPayment.Contains(",") OrElse normalizedPayment.Contains("+") OrElse normalizedPayment.Contains("/") OrElse normalizedPayment.Contains("&")
        If hasMultiplePayment Then
            Return "Multiple payment mode transaction cannot be processed."
        End If

        Dim isCashOnly As Boolean = normalizedPayment.Equals("cash", StringComparison.OrdinalIgnoreCase) OrElse normalizedPayment.Equals("Cash", StringComparison.OrdinalIgnoreCase)
        If Not isCashOnly Then
            Return "Only cash bills can be processed."
        End If

        If printStatusValue.Equals("Printed", StringComparison.OrdinalIgnoreCase) Then
            Return "This record is already printed."
        End If

        If itemCount <= minRows Then
            Return "Detail item count must be greater than " & minRows.ToString() & "."
        End If

        If maxDeleteAmount > 0D AndAlso netAmount > maxDeleteAmount Then
            Return "NetAmt " & netAmount.ToString("0.##") & " is greater than Maximum Amt " & maxDeleteAmount.ToString("0.##") & "."
        End If

        Return String.Empty
    End Function

    Private Function GetRowEligibilityFailureReason(ByVal rowIndex As Integer) As String
        Dim invoiceNo As String = GetHeaderFieldValue(rowIndex, "Trno", "")
        Dim payment As String = GetHeaderFieldValue(rowIndex, "Payment", "")
        Dim printStatus As String = GetHeaderFieldValue(rowIndex, "PrintStatus", "No Print")
        Dim minRows As Integer = 0
        Integer.TryParse(txtNoofRow.Text, minRows)
        Dim netAmount As Decimal = ParseDecimalValue(GetHeaderFieldValue(rowIndex, "NetAmt", "0"), 0D)
        Dim maxDeleteAmount As Decimal = GetMaximumDeleteAmount()

        Dim itemCount As Integer = 0
        If Not String.IsNullOrWhiteSpace(invoiceNo) Then
            itemCount = GetDetailItemCount(invoiceNo)
        End If

        Return GetEligibilityFailureReason(payment, printStatus, itemCount, minRows, netAmount, maxDeleteAmount)
    End Function

    Private Function IsEligibleForProcess(ByVal paymentValue As String, ByVal printStatusValue As String, ByVal itemCount As Integer, ByVal minRows As Integer, ByVal netAmount As Decimal, ByVal maxDeleteAmount As Decimal) As Boolean
        Return String.IsNullOrEmpty(GetEligibilityFailureReason(paymentValue, printStatusValue, itemCount, minRows, netAmount, maxDeleteAmount))
    End Function

    Private Sub RefreshCurrentFilteredData(ByVal currentRowIndex As Integer, ByVal currentInvoiceNo As String)
        ' Method to refresh data while maintaining current filter and user position
        Try
            ' Store current filter criteria and position
            Dim currentFilter As String = GridViewHeader.ActiveFilterString
            Dim currentSortInfo = GridViewHeader.SortInfo

            ' Just refresh the grid display without reloading data
            GridViewHeader.RefreshData()

            ' Restore filter if it existed
            If Not String.IsNullOrEmpty(currentFilter) Then
                GridViewHeader.ActiveFilterString = currentFilter
            End If

            ' Ensure the current row remains focused
            If currentRowIndex >= 0 And currentRowIndex < GridViewHeader.RowCount Then
                GridViewHeader.FocusedRowHandle = currentRowIndex
            End If
            Dim InvoiceNo As String = GetHeaderFieldValue(GridViewHeader.FocusedRowHandle, "Trno", "")
            If Not String.IsNullOrWhiteSpace(InvoiceNo) Then
                GetItemList(InvoiceNo)
            End If
        Catch ex As Exception
            ' If refresh fails, maintain basic functionality
            If GridViewHeader.RowCount > 0 Then
                GridViewHeader.FocusedRowHandle = currentRowIndex
            End If
        End Try
    End Sub

    Private Sub GetItemList(ByRef InvoiceNo As String)
        ' Code to retrieve item list
        Try
            Dim billNo As Integer = 0
            Integer.TryParse(InvoiceNo, billNo)

            Dim apiOk As Boolean = False
            Dim apiMsg As String = String.Empty
            Dim dtDetails As DataTable = ExecuteTaxAuditApi("Detail", GetNoOfRowsValue(), billNo, apiOk, apiMsg)

            If apiOk AndAlso dtDetails IsNot Nothing AndAlso dtDetails.Rows.Count > 0 Then
                GridControlItemList.DataSource = dtDetails
                GridViewItemList.BestFitColumns()

                ' Sum detail NetAmt for this invoice and store in mismatch map
                Dim detailSum As Decimal = 0D
                If dtDetails.Columns.Contains("NetAmt") Then
                    For Each dr As DataRow In dtDetails.Rows
                        detailSum += ParseDecimalValue(dr("NetAmt").ToString(), 0D)
                    Next
                End If

                If Not String.IsNullOrWhiteSpace(InvoiceNo) Then
                    _detailNetAmtMap(InvoiceNo) = detailSum
                    GridControlHeader.Refresh()
                End If
            Else
                GridControlItemList.DataSource = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Dataload()
        ' Code to load data into the report
        FromDateEdit.EditValue = Now.Date.ToString("yyyy-MM-dd")
        ToDateEdit.EditValue = Now.Date.ToString("yyyy-MM-dd")
        GetPaymentList()
        lblHeaderNetAmt.Text = "Net Amount: 0"
        lblDetailNetAmt.Text = "Net Amount: 0"
        txtNoofRow.Text = "2"
        ' Allow RowStyle colours (red mismatch, etc.) to show even on the focused/selected row
        GridViewHeader.OptionsSelection.EnableAppearanceFocusedRow = False
        GridViewHeader.OptionsSelection.EnableAppearanceFocusedCell = False
        GridColumnSelected.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        GridColumnSelected.Caption = "Select All"
    End Sub

    Private Sub FrmGenerateTaxReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dataload()
        Catch ex As Exception

        End Try
    End Sub


#Region "Process"
    ' Process single selected row
    Private Sub btnProcessSelected_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Try
            If GridViewHeader.RowCount = 0 Then
                MessageBox.Show("No Rows Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' Check if current row can be processed
            Dim canProcess As String = GetHeaderFieldValue(GridViewHeader.FocusedRowHandle, "CanProcess", "No")
            If canProcess = "No" Then
                Dim reason As String = GetRowEligibilityFailureReason(GridViewHeader.FocusedRowHandle)
                If String.IsNullOrWhiteSpace(reason) Then
                    reason = "This record is not eligible for processing."
                End If
                MessageBox.Show(reason, "Processing Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Check if already printed (Security check)
            Dim printStatus As String = GetHeaderFieldValue(GridViewHeader.FocusedRowHandle, "PrintStatus", "No Print")
            If printStatus = "Printed" Then
                MessageBox.Show("This record cannot be processed. It has already been printed.", "Processing Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim dialogResults As DialogResult
            dialogResults = MessageBox.Show("Do you want to process the selected record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If dialogResults = DialogResult.Yes Then
                Dim currentRowIndex As Integer = GridViewHeader.FocusedRowHandle
                Dim InvoiceNo As String = GetHeaderFieldValue(currentRowIndex, "Trno", "")
                If String.IsNullOrWhiteSpace(InvoiceNo) Then
                    MessageBox.Show("Selected row has empty bill number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim processError As String = String.Empty
                If ProcessSingleRecord(InvoiceNo, currentRowIndex, processError) Then
                    MessageBox.Show("Process completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ElseIf Not String.IsNullOrWhiteSpace(processError) Then
                    MessageBox.Show("Error: " & processError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Process all eligible rows (bulk processing)
    Private Sub btnProcessAll_Click(sender As Object, e As EventArgs) Handles btnProcessAll.Click
        Try
            If GridViewHeader.RowCount = 0 Then
                MessageBox.Show("No Rows Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' Ensure current header total is refreshed before limit check.
            GetHeaderNetAmt()
            Dim setLimitAmount As Decimal = GetMaximumDeleteAmount()
            Dim useSetLimit As Boolean = setLimitAmount > 0D
            Dim currentTotalNet As Decimal = GetCurrentHeaderNetAmount()

            If useSetLimit AndAlso currentTotalNet <= setLimitAmount Then
                MessageBox.Show("Current NetAmt " & currentTotalNet.ToString("0.##") & " is already within set limit " & setLimitAmount.ToString("0.##") & ".", "Set Limit", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' Count eligible rows
            Dim eligibleCount As Integer = 0
            For i As Integer = 0 To GridViewHeader.RowCount - 1
                Dim canProcess As String = GetHeaderFieldValue(i, "CanProcess", "No")
                Dim printStatus As String = GetHeaderFieldValue(i, "PrintStatus", "No Print")

                ' Check both CanProcess and PrintStatus
                If canProcess = "Yes" AndAlso printStatus <> "Printed" Then
                    eligibleCount += 1
                End If
            Next

            If eligibleCount = 0 Then
                MessageBox.Show("No records eligible for processing.  Detail item count must be greater than " & txtNoofRow.Text & " and record must not be printed.", "Processing Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim dialogResults As DialogResult
            Dim confirmMsg As String = "Do you want to process all " & eligibleCount & " eligible record(s)?"
            If useSetLimit Then
                confirmMsg = "Current NetAmt: " & currentTotalNet.ToString("0.##") & vbCrLf &
                             "Set Limit: " & setLimitAmount.ToString("0.##") & vbCrLf &
                             "Processing will stop once NetAmt reaches or goes below set limit." & vbCrLf & vbCrLf &
                             "Continue?"
            End If
            dialogResults = MessageBox.Show(confirmMsg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If dialogResults = DialogResult.Yes Then
                Dim processedCount As Integer = 0
                Dim failedCount As Integer = 0
                Dim skippedCount As Integer = 0
                Dim processedEligible As Integer = 0
                Dim errorDetails As New List(Of String)
                Dim stoppedBySetLimit As Boolean = False

                ' Show progress
                Cursor.Current = Cursors.WaitCursor
                StartProgressDialog("Process All Progress", eligibleCount)

                For i As Integer = 0 To GridViewHeader.RowCount - 1
                    If useSetLimit AndAlso currentTotalNet <= setLimitAmount Then
                        stoppedBySetLimit = True
                        Exit For
                    End If

                    Dim canProcess As String = GetHeaderFieldValue(i, "CanProcess", "No")
                    Dim printStatus As String = GetHeaderFieldValue(i, "PrintStatus", "No Print")

                    ' Check both conditions before processing
                    If canProcess = "Yes" AndAlso printStatus <> "Printed" Then
                        Dim InvoiceNo As String = GetHeaderFieldValue(i, "Trno", "")
                        Dim processError As String = String.Empty
                        processedEligible += 1
                        If String.IsNullOrWhiteSpace(InvoiceNo) Then
                            failedCount += 1
                            errorDetails.Add("Row " & i.ToString() & ": Empty bill number")
                            UpdateProgressDialog(processedEligible, "Skipping row " & i.ToString() & " (empty bill no)")
                            Continue For
                        End If

                        UpdateProgressDialog(processedEligible, "Processing bill " & InvoiceNo & " (" & processedEligible.ToString() & "/" & eligibleCount.ToString() & ")")

                        If ProcessSingleRecord(InvoiceNo, i, processError) Then
                            processedCount += 1
                            currentTotalNet = GetCurrentHeaderNetAmount()
                        Else
                            failedCount += 1
                            If String.IsNullOrWhiteSpace(processError) Then
                                errorDetails.Add("Bill " & InvoiceNo & ": Failed")
                            Else
                                errorDetails.Add("Bill " & InvoiceNo & ": " & processError)
                            End If
                        End If
                    ElseIf printStatus = "Printed" Then
                        skippedCount += 1 ' Count skipped printed records
                    End If
                Next

                Cursor.Current = Cursors.Default
                CloseProgressDialog()

                ' Show final summary
                Dim summaryMsg As String = "Bulk Processing Complete!" & vbCrLf & vbCrLf &
                                          "Successfully Processed: " & processedCount & vbCrLf &
                                          "Failed: " & failedCount & vbCrLf &
                                          "Skipped (Already Printed): " & skippedCount & vbCrLf &
                                          "Total Eligible: " & eligibleCount

                If useSetLimit Then
                    summaryMsg &= vbCrLf & "Set Limit: " & setLimitAmount.ToString("0.##") & vbCrLf &
                                  "Current NetAmt: " & currentTotalNet.ToString("0.##")
                    If stoppedBySetLimit Then
                        summaryMsg &= vbCrLf & "Stopped: Reached set limit."
                    End If
                End If

                If errorDetails.Count > 0 Then
                    summaryMsg &= vbCrLf & vbCrLf & "Errors:" & vbCrLf & String.Join(vbCrLf, errorDetails.ToArray())
                End If

                MessageBox.Show(summaryMsg, "Bulk Process Summary", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Final refresh
                GetDetailsNetAmt()
                GetHeaderNetAmt()
            End If
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            CloseProgressDialog()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Common method to process a single record
    Private Function ProcessSingleRecord(InvoiceNo As String, rowIndex As Integer, Optional ByRef errorMessage As String = "") As Boolean
        Try
            errorMessage = String.Empty
            ' Additional security check before processing
            Dim printStatus As String = GetHeaderFieldValue(rowIndex, "PrintStatus", "No Print")
            If printStatus = "Printed" Then
                errorMessage = "Already printed"
                Return False ' Cannot process already printed records
            End If

            ' Final gate check before DB update/delete so latest txtmaximumamt is always enforced.
            Dim payment As String = GetHeaderFieldValue(rowIndex, "Payment", "")
            Dim minRows As Integer = 0
            Integer.TryParse(txtNoofRow.Text, minRows)
            Dim netAmount As Decimal = ParseDecimalValue(GetHeaderFieldValue(rowIndex, "NetAmt", "0"), 0D)
            Dim maxDeleteAmount As Decimal = GetMaximumDeleteAmount()
            Dim currentItemCount As Integer = GetDetailItemCount(InvoiceNo)
            Dim eligibilityReason As String = GetEligibilityFailureReason(payment, printStatus, currentItemCount, minRows, netAmount, maxDeleteAmount)
            If Not String.IsNullOrWhiteSpace(eligibilityReason) Then
                errorMessage = eligibilityReason
                GridViewHeader.SetRowCellValue(rowIndex, "CanProcess", "No")
                Return False
            End If

            Dim billNo As Integer = 0
            Integer.TryParse(InvoiceNo, billNo)
            Dim apiOk As Boolean = False
            Dim apiMsg As String = String.Empty
            ExecuteTaxAuditApi("Process", GetNoOfRowsValue(), billNo, apiOk, apiMsg)
            If apiOk Then
                ' Fast update:  Only update the processed row's status
                Dim newDetailCount As Integer = GetDetailItemCount(InvoiceNo)
                Dim printStatusAfter As String = GetHeaderFieldValue(rowIndex, "PrintStatus", "No Print")
                netAmount = ParseDecimalValue(GetHeaderFieldValue(rowIndex, "NetAmt", "0"), 0D)
                maxDeleteAmount = GetMaximumDeleteAmount()

                If IsEligibleForProcess(payment, printStatusAfter, newDetailCount, minRows, netAmount, maxDeleteAmount) Then
                    GridViewHeader.SetRowCellValue(rowIndex, "CanProcess", "Yes")
                Else
                    GridViewHeader.SetRowCellValue(rowIndex, "CanProcess", "No")
                End If

                ' Refresh item list for current row
                GetItemList(InvoiceNo)

                ' Update net amounts without full reload
                GetDetailsNetAmt()
                GetHeaderNetAmt()

                ' Final refresh while maintaining current filter and position
                RefreshCurrentFilteredData(rowIndex, InvoiceNo)

                Return True
            Else
                errorMessage = If(String.IsNullOrWhiteSpace(apiMsg), "Cloud process returned false", apiMsg)
                Return False
            End If
        Catch ex As Exception
            errorMessage = ex.Message
            Return False
        End Try
    End Function


    Private Sub GridViewHeader_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridViewHeader.RowStyle
        Try
            If e.RowHandle >= 0 Then
                ' Get cell values for the current row
                Dim payment As String = GetHeaderFieldValue(e.RowHandle, "Payment", "")
                Dim canProcess As String = GetHeaderFieldValue(e.RowHandle, "CanProcess", "No")
                Dim printStatus As String = GetHeaderFieldValue(e.RowHandle, "PrintStatus", "No Print")
                Dim trno As String = GetHeaderFieldValue(e.RowHandle, "Trno", "")
                Dim headerNetAmt As Decimal = ParseDecimalValue(GetHeaderFieldValue(e.RowHandle, "NetAmt", "0"), 0D)

                ' Check if header NetAmt mismatches sum of detail NetAmt for this invoice
                Dim isNetAmtMismatch As Boolean = False
                If Not String.IsNullOrWhiteSpace(trno) AndAlso _detailNetAmtMap IsNot Nothing AndAlso _detailNetAmtMap.ContainsKey(trno) Then
                    Dim detailSum As Decimal = _detailNetAmtMap(trno)
                    isNetAmtMismatch = (headerNetAmt <> detailSum)
                End If

                ' Red for NetAmt mismatch (highest priority)
                If isNetAmtMismatch Then
                    e.Appearance.BackColor = Color.Red
                    e.Appearance.ForeColor = Color.White

                    ' Red for Printed status
                ElseIf printStatus = "Printed" Then
                    e.Appearance.BackColor = Color.LightCoral ' Light red
                    e.Appearance.ForeColor = Color.DarkRed

                    ' Green for CASH BILL that can be processed and not printed
                ElseIf payment = "CASH BILL" AndAlso canProcess = "Yes" AndAlso printStatus = "No Print" Then
                    e.Appearance.BackColor = Color.LightGreen
                    e.Appearance.ForeColor = Color.Black
                End If
            End If
        Catch ex As Exception
            ' Handle exceptions
        End Try
    End Sub
#End Region
    Private Sub GridViewHeader_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridViewHeader.RowClick
        Try
            If GridViewHeader.RowCount > 0 Then
                Dim InvoiceNo As String = GetHeaderFieldValue(GridViewHeader.FocusedRowHandle, "Trno", "")
                If String.IsNullOrWhiteSpace(InvoiceNo) Then
                    Exit Sub
                End If
                GetItemList(InvoiceNo)

                ' Update CanProcess status based on current detail row count
                Dim detailItemCount As Integer = GetDetailItemCount(InvoiceNo)
                Dim currentRowIndex As Integer = GridViewHeader.FocusedRowHandle

                Dim payment As String = GetHeaderFieldValue(currentRowIndex, "Payment", "")
                Dim printStatus As String = GetHeaderFieldValue(currentRowIndex, "PrintStatus", "No Print")
                Dim minRows As Integer = 0
                Integer.TryParse(txtNoofRow.Text, minRows)
                Dim netAmount As Decimal = ParseDecimalValue(GetHeaderFieldValue(currentRowIndex, "NetAmt", "0"), 0D)
                Dim maxDeleteAmount As Decimal = GetMaximumDeleteAmount()

                If IsEligibleForProcess(payment, printStatus, detailItemCount, minRows, netAmount, maxDeleteAmount) Then
                    GridViewHeader.SetRowCellValue(currentRowIndex, "CanProcess", "Yes")
                Else
                    GridViewHeader.SetRowCellValue(currentRowIndex, "CanProcess", "No")
                End If

                ' Refresh the grid view to show updated status
                GridViewHeader.RefreshData()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CheckAllMismatchRows()
        If GridViewHeader.RowCount = 0 Then Exit Sub
        Try
            Dim totalRows As Integer = GridViewHeader.RowCount
            _detailNetAmtMap = New Dictionary(Of String, Decimal)(StringComparer.OrdinalIgnoreCase)
            StartProgressDialog("Checking NetAmt Mismatch...", totalRows)

            For i As Integer = 0 To totalRows - 1
                Dim trno As String = GetHeaderFieldValue(i, "Trno", "")
                If String.IsNullOrWhiteSpace(trno) Then
                    UpdateProgressDialog(i + 1, "Row " & (i + 1).ToString() & "/" & totalRows.ToString())
                    Continue For
                End If

                UpdateProgressDialog(i + 1, "Checking Trno " & trno & " (" & (i + 1).ToString() & "/" & totalRows.ToString() & ")")

                Dim billNo As Integer = 0
                Integer.TryParse(trno, billNo)
                Dim apiOk As Boolean = False
                Dim apiMsg As String = String.Empty
                Dim dtDetails As DataTable = ExecuteTaxAuditApi("Detail", GetNoOfRowsValue(), billNo, apiOk, apiMsg)

                Dim detailSum As Decimal = 0D
                If apiOk AndAlso dtDetails IsNot Nothing AndAlso dtDetails.Rows.Count > 0 AndAlso dtDetails.Columns.Contains("NetAmt") Then
                    For Each dr As DataRow In dtDetails.Rows
                        detailSum += ParseDecimalValue(dr("NetAmt").ToString(), 0D)
                    Next
                End If
                _detailNetAmtMap(trno) = detailSum
            Next

            CloseProgressDialog()
            GridControlHeader.Refresh()
        Catch ex As Exception
            CloseProgressDialog()
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            GetHeaderInfo(True)
            ' Set focus to first row and load its details after search
            If GridViewHeader.RowCount > 0 Then
                GridViewHeader.FocusedRowHandle = 0
                Dim firstRowInvoiceNo As String = GetHeaderFieldValue(0, "Trno", "")
                If Not String.IsNullOrWhiteSpace(firstRowInvoiceNo) Then
                    GetItemList(firstRowInvoiceNo)
                End If
                ' Only scan individual rows when the overall totals already show a mismatch
                Dim headerTotal As Decimal = GetCurrentHeaderNetAmount()
                Dim detailTotal As Decimal = GetCurrentDetailNetAmount()
                If headerTotal <> detailTotal Then
                    CheckAllMismatchRows()
                End If
            End If
        Catch ex As Exception
            CloseProgressDialog()
            MessageBox.Show("Search Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnfinalprocess_Click(sender As Object, e As EventArgs) Handles btnfinalprocess.Click
        Try
            If GridViewHeader.RowCount = 0 Then
                MessageBox.Show("No Rows Found", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Dim dialogResults As DialogResult
            dialogResults = MessageBox.Show("Do you want to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If dialogResults = DialogResult.Yes Then
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim fromDate As String = FormatApiDate(FromDateEdit.EditValue)
                Dim toDate As String = FormatApiDate(ToDateEdit.EditValue)

                Dim payload = New With {
                    .FromDate = fromDate,
                    .ToDate = toDate,
                    .ComId = _companyInfo.ComId,
                    .LocId = _companyInfo.LocId
                }

                Dim jsonPayload As String = JsonConvert.SerializeObject(payload)
                Dim url As String = BuildTaxAuditUrl(10, jsonPayload)

                If String.IsNullOrWhiteSpace(url) Then
                    MessageBox.Show("Tax audit URL is empty. Configure UrlLinkTaxAudit in settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Dim response As String = New WebClient().DownloadString(url)
                Dim responseObj As JObject = JObject.Parse(response)

                Dim isSuccess As Boolean = False
                If responseObj("Success") IsNot Nothing Then
                    Boolean.TryParse(responseObj("Success").ToString(), isSuccess)
                End If

                Dim msg As String = If(responseObj("Msg") IsNot Nothing, responseObj("Msg").ToString(), String.Empty)

                If isSuccess Then
                    MessageBox.Show(If(String.IsNullOrWhiteSpace(msg), "Final process completed successfully.", msg), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show(If(String.IsNullOrWhiteSpace(msg), "Final process failed.", msg), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub barbtnClose_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnClose.ItemClick
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSelectedItemDelete_Click(sender As Object, e As EventArgs) Handles btnSelectedItemDelete.Click
        Try
            If GridViewHeader.RowCount = 0 OrElse GridViewHeader.FocusedRowHandle < 0 Then
                MessageBox.Show("Please select a bill in header list.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If GridViewItemList.RowCount = 0 OrElse GridViewItemList.FocusedRowHandle < 0 Then
                MessageBox.Show("Please select an item in item list.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim headerBillNo As String = GetHeaderFieldValue(GridViewHeader.FocusedRowHandle, "Trno", "")
            Dim itemBillNo As String = String.Empty
            Dim itemBillObj As Object = GridViewItemList.GetRowCellValue(GridViewItemList.FocusedRowHandle, "Trno")
            If itemBillObj IsNot Nothing AndAlso Not Convert.IsDBNull(itemBillObj) Then
                itemBillNo = itemBillObj.ToString().Trim()
            End If

            If String.IsNullOrWhiteSpace(headerBillNo) Then
                MessageBox.Show("Selected header bill number is empty.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If String.IsNullOrWhiteSpace(itemBillNo) Then
                MessageBox.Show("Selected item bill number is empty.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Not headerBillNo.Equals(itemBillNo, StringComparison.OrdinalIgnoreCase) Then
                MessageBox.Show("Selected item does not belong to selected header bill.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim itemRowIdObj As Object = GridViewItemList.GetRowCellValue(GridViewItemList.FocusedRowHandle, "RowId")
            If itemRowIdObj Is Nothing OrElse Convert.IsDBNull(itemRowIdObj) Then
                MessageBox.Show("Selected item RowId is empty.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim itemRowId As Integer
            If Not Integer.TryParse(itemRowIdObj.ToString().Trim(), itemRowId) Then
                MessageBox.Show("Selected item RowId is invalid.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim itemName As String = String.Empty
            Dim itemNameObj As Object = GridViewItemList.GetRowCellValue(GridViewItemList.FocusedRowHandle, "ItemName")
            If itemNameObj IsNot Nothing AndAlso Not Convert.IsDBNull(itemNameObj) Then
                itemName = itemNameObj.ToString().Trim()
            End If

            Dim confirmMsg As String = "Do you want to delete selected item?" & vbCrLf & vbCrLf &
                                       "Bill No: " & headerBillNo & vbCrLf &
                                       "RowId: " & itemRowId.ToString()
            If Not String.IsNullOrWhiteSpace(itemName) Then
                confirmMsg &= vbCrLf & "Item: " & itemName
            End If

            Dim dialogResult As DialogResult = MessageBox.Show(confirmMsg, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If dialogResult <> dialogResult.Yes Then
                Exit Sub
            End If

            Dim billNo As Integer = 0
            Integer.TryParse(headerBillNo, billNo)
            Dim apiOk As Boolean = False
            Dim apiMsg As String = String.Empty
            ExecuteTaxAuditApi("DeleteSelectedItem", itemRowId, billNo, apiOk, apiMsg)

            If apiOk Then
                MessageBox.Show("Selected item deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GetItemList(headerBillNo)
                GetHeaderInfo()
                GetHeaderNetAmt()
                GetDetailsNetAmt()
            Else
                Dim err As String = If(String.IsNullOrWhiteSpace(apiMsg), "Delete failed. Please verify stored procedure mode DeleteSelectedItem.", apiMsg)
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Validation error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnTransferDataDuplicateProcess_Click(sender As Object, e As EventArgs) Handles btnTransferDataDuplicateProcess.Click
        Try
            Dim fromDate As String = FormatApiDate(FromDateEdit.EditValue)
            Dim toDate As String = FormatApiDate(ToDateEdit.EditValue)

            Dim pmId As Integer = 0
            ' If ListBoxControlPayment.SelectedItem IsNot Nothing Then
            '     If TypeOf ListBoxControlPayment.SelectedItem Is KeyValuePair(Of String, Integer) Then
            '         pmId = DirectCast(ListBoxControlPayment.SelectedItem, KeyValuePair(Of String, Integer)).Value
            '     End If
            ' End If

            Dim confirmMsg As String = "Do you want to transfer sale data to tax?" & vbCrLf & vbCrLf &
                                       "From: " & fromDate & vbCrLf &
                                       "To: " & toDate
            Dim dialogResult As DialogResult = MessageBox.Show(confirmMsg, "Confirm Transfer", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If dialogResult <> dialogResult.Yes Then
                Exit Sub
            End If

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim payload = New With {
                .ComId = _companyInfo.ComId,
                .LocId = _companyInfo.LocId,
                .PmId = pmId,
                .FromDate = fromDate,
                .ToDate = toDate
            }

            Dim jsonPayload As String = JsonConvert.SerializeObject(payload)
            Dim url As String = BuildTaxAuditUrl(1, jsonPayload)

            If String.IsNullOrWhiteSpace(url) Then
                MessageBox.Show("Tax audit URL is empty. Configure UrlLinkTaxAudit in settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim response As String = New WebClient().DownloadString(url)
            Dim responseObj As JObject = JObject.Parse(response)

            Dim isSuccess As Boolean = False
            If responseObj("Success") IsNot Nothing Then
                Boolean.TryParse(responseObj("Success").ToString(), isSuccess)
            End If

            Dim msg As String = If(responseObj("Msg") IsNot Nothing, responseObj("Msg").ToString(), String.Empty)

            If isSuccess Then
                MessageBox.Show(If(String.IsNullOrWhiteSpace(msg), "Data transferred successfully.", msg), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show(If(String.IsNullOrWhiteSpace(msg), "Transfer failed.", msg), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelSelectedTrno_Click(sender As Object, e As EventArgs) Handles btnDelSelectedTrno.Click
        Try
            Dim selectedRows() As Integer = GetCheckedRowHandles()
            If selectedRows Is Nothing OrElse selectedRows.Length = 0 Then
                MessageBox.Show("Please select one or more bills in the header list.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Collect eligible invoices and check restrictions
            Dim eligibleInvoices As New List(Of String)
            Dim skippedCardBank As New List(Of String)

            For Each rowHandle As Integer In selectedRows
                Dim invoiceNo As String = GetHeaderFieldValue(rowHandle, "Trno", "")
                If String.IsNullOrWhiteSpace(invoiceNo) Then Continue For

                Dim payment As String = GetHeaderFieldValue(rowHandle, "Payment", "")
                Dim upperPayment As String = payment.ToUpper().Trim()

                If upperPayment.Contains("CARD") OrElse upperPayment.Contains("BANK") Then
                    skippedCardBank.Add(invoiceNo & " (" & payment & ")")
                Else
                    eligibleInvoices.Add(invoiceNo)
                End If
            Next

            If eligibleInvoices.Count = 0 Then
                Dim msg As String = "No eligible invoices to delete."
                If skippedCardBank.Count > 0 Then
                    msg &= vbCrLf & vbCrLf & "Skipped (Card/Bank):" & vbCrLf & String.Join(vbCrLf, skippedCardBank.ToArray())
                End If
                MessageBox.Show(msg, "Delete Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Build confirmation message
            Dim confirmMsg As String = "Do you want to delete " & eligibleInvoices.Count.ToString() & " selected invoice(s)?" & vbCrLf & vbCrLf &
                                       "Bill No(s): " & String.Join(", ", eligibleInvoices.ToArray())
            If skippedCardBank.Count > 0 Then
                confirmMsg &= vbCrLf & vbCrLf & "Skipped (Card/Bank): " & skippedCardBank.Count.ToString() & " invoice(s)"
            End If

            Dim dialogResult As DialogResult = MessageBox.Show(confirmMsg, "Confirm Delete Invoice(s)", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If dialogResult <> DialogResult.Yes Then
                Exit Sub
            End If

            Dim deletedCount As Integer = 0
            Dim failedCount As Integer = 0
            Dim errorDetails As New List(Of String)

            Cursor.Current = Cursors.WaitCursor
            StartProgressDialog("Deleting Invoices", eligibleInvoices.Count)

            For idx As Integer = 0 To eligibleInvoices.Count - 1
                Dim inv As String = eligibleInvoices(idx)
                UpdateProgressDialog(idx + 1, "Deleting " & inv & " (" & (idx + 1).ToString() & "/" & eligibleInvoices.Count.ToString() & ")")

                Dim billNo As Integer = 0
                Integer.TryParse(inv, billNo)

                Dim apiOk As Boolean = False
                Dim apiMsg As String = String.Empty
                ExecuteTaxAuditApi("DelInvoice", 0, billNo, apiOk, apiMsg)

                If apiOk Then
                    deletedCount += 1
                Else
                    failedCount += 1
                    errorDetails.Add(inv & ": " & If(String.IsNullOrWhiteSpace(apiMsg), "Delete failed", apiMsg))
                End If
            Next

            Cursor.Current = Cursors.Default
            CloseProgressDialog()

            ' Show summary
            Dim summaryMsg As String = "Delete Complete!" & vbCrLf & vbCrLf &
                                       "Deleted: " & deletedCount.ToString() & vbCrLf &
                                       "Failed: " & failedCount.ToString()
            If skippedCardBank.Count > 0 Then
                summaryMsg &= vbCrLf & "Skipped (Card/Bank): " & skippedCardBank.Count.ToString()
            End If
            If errorDetails.Count > 0 Then
                summaryMsg &= vbCrLf & vbCrLf & "Errors:" & vbCrLf & String.Join(vbCrLf, errorDetails.ToArray())
            End If

            MessageBox.Show(summaryMsg, "Delete Summary", MessageBoxButtons.OK, MessageBoxIcon.Information)

            GetHeaderInfo()
            GetHeaderNetAmt()
            GetDetailsNetAmt()
            GridControlItemList.DataSource = Nothing

        Catch ex As Exception
            Cursor.Current = Cursors.Default
            CloseProgressDialog()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub barbtnpurgebydate_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnpurgebydate.ItemClick
        Try
            Dim fromDate As String = FormatApiDate(FromDateEdit.EditValue)
            Dim toDate As String = FormatApiDate(ToDateEdit.EditValue)

            Dim confirmMsg As String = "Do you want to purge (delete) all tax data for the selected date range?" & vbCrLf & vbCrLf &
                                       "From: " & fromDate & vbCrLf &
                                       "To: " & toDate & vbCrLf & vbCrLf &
                                       "This action cannot be undone."
            Dim dialogResult As DialogResult = MessageBox.Show(confirmMsg, "Confirm Purge By Date", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If dialogResult <> DialogResult.Yes Then
                Exit Sub
            End If

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim pmId As Integer = 0
            Dim payload = New With {
                .ComId = _companyInfo.ComId,
                .LocId = _companyInfo.LocId,
                .PmId = pmId,
                .FromDate = fromDate,
                .ToDate = toDate
            }

            Dim jsonPayload As String = JsonConvert.SerializeObject(payload)
            Dim url As String = BuildTaxAuditUrl(2, jsonPayload)

            If String.IsNullOrWhiteSpace(url) Then
                MessageBox.Show("Tax audit URL is empty. Configure UrlLinkTaxAudit in settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim response As String = New WebClient().DownloadString(url)
            Dim responseObj As JObject = JObject.Parse(response)

            Dim isSuccess As Boolean = False
            If responseObj("Success") IsNot Nothing Then
                Boolean.TryParse(responseObj("Success").ToString(), isSuccess)
            End If

            Dim msg As String = If(responseObj("Msg") IsNot Nothing, responseObj("Msg").ToString(), String.Empty)

            If isSuccess Then
                MessageBox.Show(If(String.IsNullOrWhiteSpace(msg), "Tax data purged successfully.", msg), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GetHeaderInfo()
                GetHeaderNetAmt()
                GetDetailsNetAmt()
                GridControlItemList.DataSource = Nothing
            Else
                MessageBox.Show(If(String.IsNullOrWhiteSpace(msg), "Purge failed.", msg), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub barbtnpurgebymonth_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnpurgebymonth.ItemClick
        Try
            Dim selectedDate As DateTime = DateTime.Now
            Try
                selectedDate = Convert.ToDateTime(FromDateEdit.EditValue)
            Catch
            End Try

            Dim selectedYear As Integer = selectedDate.Year
            Dim selectedMonth As Integer = selectedDate.Month
            Dim monthName As String = selectedDate.ToString("MMMM yyyy")

            Dim confirmMsg As String = "Do you want to purge (delete) all tax data for the selected month?" & vbCrLf & vbCrLf &
                                       "Month: " & monthName & vbCrLf &
                                       "Year: " & selectedYear.ToString() & vbCrLf &
                                       "Month: " & selectedMonth.ToString() & vbCrLf & vbCrLf &
                                       "This action cannot be undone."
            Dim dialogResult As DialogResult = MessageBox.Show(confirmMsg, "Confirm Purge By Month", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If dialogResult <> DialogResult.Yes Then
                Exit Sub
            End If

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim pmId As Integer = 0
            Dim payload = New With {
                .ComId = _companyInfo.ComId,
                .LocId = _companyInfo.LocId,
                .PmId = pmId,
                .Year = selectedYear,
                .Month = selectedMonth
            }

            Dim jsonPayload As String = JsonConvert.SerializeObject(payload)
            Dim url As String = BuildTaxAuditUrl(3, jsonPayload)

            If String.IsNullOrWhiteSpace(url) Then
                MessageBox.Show("Tax audit URL is empty. Configure UrlLinkTaxAudit in settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim response As String = New WebClient().DownloadString(url)
            Dim responseObj As JObject = JObject.Parse(response)

            Dim isSuccess As Boolean = False
            If responseObj("Success") IsNot Nothing Then
                Boolean.TryParse(responseObj("Success").ToString(), isSuccess)
            End If

            Dim msg As String = If(responseObj("Msg") IsNot Nothing, responseObj("Msg").ToString(), String.Empty)

            If isSuccess Then
                MessageBox.Show(If(String.IsNullOrWhiteSpace(msg), "Tax data for " & monthName & " purged successfully.", msg), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GetHeaderInfo()
                GetHeaderNetAmt()
                GetDetailsNetAmt()
                GridControlItemList.DataSource = Nothing
            Else
                MessageBox.Show(If(String.IsNullOrWhiteSpace(msg), "Purge failed.", msg), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Fires IMMEDIATELY when the checkbox is clicked — before DevExpress commits the row.
    ' CellValueChanged fires only on row EXIT (commit), causing the one-step-behind delay.
    Private Sub chkSelectHeader_EditValueChanged(sender As Object, e As EventArgs) Handles chkSelectHeader.EditValueChanged
        Try
            Me.BeginInvoke(New Action(AddressOf UpdateSelectedNetAmtStatus))
        Catch
        End Try
    End Sub

    ' Kept as a backup so programmatic value changes (not via user click) also update the total.
    Private Sub GridViewHeader_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridViewHeader.CellValueChanged
        Try
            If e.Column IsNot Nothing AndAlso e.Column.FieldName = "Selected" Then
                Me.BeginInvoke(New Action(AddressOf UpdateSelectedNetAmtStatus))
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GridViewHeader_MouseDown(sender As Object, e As MouseEventArgs) Handles GridViewHeader.MouseDown
        If e.Button <> MouseButtons.Left Then Exit Sub
        Dim hitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = GridViewHeader.CalcHitInfo(e.Location)
        If Not hitInfo.InColumnPanel OrElse hitInfo.Column Is Nothing OrElse hitInfo.Column.FieldName <> "Selected" Then Exit Sub

        Try
            GridViewHeader.CloseEditor()
            GridViewHeader.UpdateCurrentRow()

            Dim selectAll As Boolean = False
            For i As Integer = 0 To GridViewHeader.DataRowCount - 1
                Dim selectedValue As Object = GridViewHeader.GetRowCellValue(i, "Selected")
                If selectedValue Is Nothing OrElse Convert.IsDBNull(selectedValue) OrElse Not Convert.ToBoolean(selectedValue) Then
                    selectAll = True
                    Exit For
                End If
            Next

            GridViewHeader.BeginDataUpdate()
            Try
                For i As Integer = 0 To GridViewHeader.DataRowCount - 1
                    GridViewHeader.SetRowCellValue(i, "Selected", selectAll)
                Next
            Finally
                GridViewHeader.EndDataUpdate()
            End Try

            UpdateSelectedNetAmtStatus()
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Recalculates the selected-rows NetAmt total by reading directly from the GridView
    ''' cell values (which include the editing buffer for any in-edit row).
    ''' Called via BeginInvoke so DevExpress has fully committed the value before we read.
    ''' </summary>
    Private Sub UpdateSelectedNetAmtStatus()
        Dim total As Decimal = 0D
        Dim count As Integer = 0
        Dim rowCount As Integer = GridViewHeader.DataRowCount
        Try
            For i As Integer = 0 To rowCount - 1
                Dim selObj As Object = GridViewHeader.GetRowCellValue(i, "Selected")
                If selObj IsNot Nothing AndAlso Not Convert.IsDBNull(selObj) AndAlso Convert.ToBoolean(selObj) Then
                    Dim netObj As Object = GridViewHeader.GetRowCellValue(i, "NetAmt")
                    Dim rawAmt As String = If(netObj Is Nothing OrElse Convert.IsDBNull(netObj), "0", netObj.ToString())
                    total += ParseDecimalValue(rawAmt, 0D)
                    count += 1
                End If
            Next
        Catch ex As Exception
        End Try
        If count > 0 Then
            barSelectedNetAmt.Caption = "Selected: " & count.ToString() & " row(s)  |  RM " & total.ToString("0.00")
        Else
            barSelectedNetAmt.Caption = "Selected: RM 0.00"
        End If
        GridColumnSelected.Caption = If(rowCount > 0 AndAlso count = rowCount, "Clear All", "Select All")
    End Sub

    Private Sub btnBulkDelete_Click(sender As Object, e As EventArgs) Handles btnBulkDelete.Click
        Try
            Dim selectedRows() As Integer = GetCheckedRowHandles()
            If selectedRows Is Nothing OrElse selectedRows.Length = 0 Then
                MessageBox.Show("Please check one or more rows in the header list.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim eligibleInvoices As New List(Of String)
            Dim skippedCardBank As New List(Of String)

            For Each rowHandle As Integer In selectedRows
                Dim invoiceNo As String = GetHeaderFieldValue(rowHandle, "Trno", "")
                If String.IsNullOrWhiteSpace(invoiceNo) Then Continue For

                Dim payment As String = GetHeaderFieldValue(rowHandle, "Payment", "")
                Dim upperPayment As String = payment.ToUpper().Trim()

                If upperPayment.Contains("CARD") OrElse upperPayment.Contains("BANK") Then
                    skippedCardBank.Add(invoiceNo & " (" & payment & ")")
                Else
                    eligibleInvoices.Add(invoiceNo)
                End If
            Next

            If eligibleInvoices.Count = 0 Then
                Dim msg As String = "No eligible invoices to delete."
                If skippedCardBank.Count > 0 Then
                    msg &= vbCrLf & vbCrLf & "Skipped (Card/Bank):" & vbCrLf & String.Join(vbCrLf, skippedCardBank.ToArray())
                End If
                MessageBox.Show(msg, "Delete Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim confirmMsg As String = "Do you want to delete " & eligibleInvoices.Count.ToString() & " selected invoice(s)?" & vbCrLf & vbCrLf &
                                       "Bill No(s): " & String.Join(", ", eligibleInvoices.ToArray())
            If skippedCardBank.Count > 0 Then
                confirmMsg &= vbCrLf & vbCrLf & "Skipped (Card/Bank): " & skippedCardBank.Count.ToString() & " invoice(s)"
            End If

            Dim dialogResult As DialogResult = MessageBox.Show(confirmMsg, "Confirm Bulk Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If dialogResult <> DialogResult.Yes Then Exit Sub

            Dim deletedCount As Integer = 0
            Dim failedCount As Integer = 0
            Dim errorDetails As New List(Of String)

            Cursor.Current = Cursors.WaitCursor
            StartProgressDialog("Bulk Deleting Invoices", eligibleInvoices.Count)

            For idx As Integer = 0 To eligibleInvoices.Count - 1
                Dim inv As String = eligibleInvoices(idx)
                UpdateProgressDialog(idx + 1, "Deleting " & inv & " (" & (idx + 1).ToString() & "/" & eligibleInvoices.Count.ToString() & ")")

                Dim billNo As Integer = 0
                Integer.TryParse(inv, billNo)

                Dim apiOk As Boolean = False
                Dim apiMsg As String = String.Empty
                ExecuteTaxAuditApi("DelInvoice", 0, billNo, apiOk, apiMsg)

                If apiOk Then
                    deletedCount += 1
                Else
                    failedCount += 1
                    errorDetails.Add(inv & ": " & If(String.IsNullOrWhiteSpace(apiMsg), "Delete failed", apiMsg))
                End If
            Next

            Cursor.Current = Cursors.Default
            CloseProgressDialog()

            Dim summaryMsg As String = "Bulk Delete Complete!" & vbCrLf & vbCrLf &
                                       "Deleted: " & deletedCount.ToString() & vbCrLf &
                                       "Failed: " & failedCount.ToString()
            If skippedCardBank.Count > 0 Then
                summaryMsg &= vbCrLf & "Skipped (Card/Bank): " & skippedCardBank.Count.ToString()
            End If
            If errorDetails.Count > 0 Then
                summaryMsg &= vbCrLf & vbCrLf & "Errors:" & vbCrLf & String.Join(vbCrLf, errorDetails.ToArray())
            End If

            MessageBox.Show(summaryMsg, "Bulk Delete Summary", MessageBoxButtons.OK, MessageBoxIcon.Information)

            barSelectedNetAmt.Caption = "Selected: RM 0.00"
            GetHeaderInfo()
            GetHeaderNetAmt()
            GetDetailsNetAmt()
            GridControlItemList.DataSource = Nothing

        Catch ex As Exception
            Cursor.Current = Cursors.Default
            CloseProgressDialog()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ─── AjaxRequest=11 wrapper ──────────────────────────────────────
    Private Function CallMismatchDataApi(ByVal pMode As String,
                                         ByVal pPmdId As Integer,
                                         ByVal pPmdTrno As String,
                                         ByVal pComId As Integer,
                                         ByVal pLocId As Integer,
                                         ByVal pPmdStatus As Integer) As JObject
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim payload = New With {
                .Mode = pMode,
                .PmdId = pPmdId,
                .PmdTrno = pPmdTrno,
                .ComId = pComId,
                .LocId = pLocId,
                .PmdStatus = pPmdStatus
            }

            Dim jsonPayload As String = JsonConvert.SerializeObject(payload)
            Dim url As String = BuildTaxAuditUrl(11, jsonPayload)
            If String.IsNullOrWhiteSpace(url) Then Return Nothing

            Dim response As String = New WebClient().DownloadString(url)
            Return JObject.Parse(response)
        Catch
            Return Nothing
        End Try
    End Function

    ' ─── Open the Mismatch Data form ─────────────────────────────────
    Private Sub btnReupdateMismatchData_Click(sender As Object, e As EventArgs) Handles btnReupdateMismatchData.Click
        Try
            ' Collect mismatched rows from the in-memory map vs header grid
            Dim mismatchRows As New DataTable()
            mismatchRows.Columns.Add("Trno", GetType(String))
            mismatchRows.Columns.Add("Header NetAmt", GetType(String))
            mismatchRows.Columns.Add("Detail Sum", GetType(String))
            mismatchRows.Columns.Add("Difference", GetType(String))

            For i As Integer = 0 To GridViewHeader.RowCount - 1
                Dim trno As String = GetHeaderFieldValue(i, "Trno", "")
                If String.IsNullOrWhiteSpace(trno) Then Continue For

                Dim headerAmt As Decimal = ParseDecimalValue(GetHeaderFieldValue(i, "NetAmt", "0"), 0D)

                If _detailNetAmtMap.ContainsKey(trno) Then
                    Dim detailSum As Decimal = _detailNetAmtMap(trno)
                    If headerAmt <> detailSum Then
                        mismatchRows.Rows.Add(trno,
                                              headerAmt.ToString("0.00"),
                                              detailSum.ToString("0.00"),
                                              (headerAmt - detailSum).ToString("0.00"))
                    End If
                End If
            Next

            ' ── Write log to RichTextBoxMismatDetails ────────────────
            RichTextBoxMismatDetails.Clear()
            Dim sb As New System.Text.StringBuilder()
            Dim divider As String = New String("="c, 62)
            Dim dashes As String = New String("-"c, 62)

            sb.AppendLine(divider)
            sb.AppendLine(" Mismatch Report — " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sb.AppendLine(" Detected Mismatches : " & mismatchRows.Rows.Count.ToString())
            sb.AppendLine(divider)

            If mismatchRows.Rows.Count > 0 Then
                sb.AppendLine(String.Format(" {0,-10} {1,14} {2,12} {3,12}",
                                            "Trno", "Header NetAmt", "Detail Sum", "Difference"))
                sb.AppendLine(dashes)

                Dim totalDiff As Decimal = 0D
                For Each dr As DataRow In mismatchRows.Rows
                    Dim diff As Decimal = ParseDecimalValue(dr("Difference").ToString(), 0D)
                    totalDiff += diff
                    sb.AppendLine(String.Format(" {0,-10} {1,14} {2,12} {3,12}",
                                                dr("Trno"),
                                                dr("Header NetAmt"),
                                                dr("Detail Sum"),
                                                dr("Difference")))
                Next

                sb.AppendLine(dashes)
                sb.AppendLine(String.Format(" {0,-10} {1,14} {2,12} {3,12}",
                                            "TOTAL", "", "", totalDiff.ToString("0.00")))
            Else
                sb.AppendLine(" No mismatches detected.")
                sb.AppendLine(" Run Search first so mismatches can be identified.")
            End If

            sb.AppendLine(divider)
            RichTextBoxMismatDetails.Font = New System.Drawing.Font("Courier New", 8.5F)
            RichTextBoxMismatDetails.Text = sb.ToString()

            If mismatchRows.Rows.Count = 0 Then Exit Sub

            ShowMismatchDataForm(mismatchRows)

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ShowMismatchDataForm(ByVal mismatchRows As DataTable)
        Dim frm As New Form()
        frm.Text = "Mismatch Data — Save & Review"
        frm.Size = New Size(960, 640)
        frm.StartPosition = FormStartPosition.CenterParent
        frm.MinimizeBox = False

        ' ── Top label ────────────────────────────────────────────────
        Dim lblTop As New Label()
        lblTop.Text = "Detected Mismatches: " & mismatchRows.Rows.Count.ToString() &
                      "   |   Click [Save Mismatch Data] to record them, then [Load Saved Records] to view."
        lblTop.AutoSize = False
        lblTop.Dock = DockStyle.Top
        lblTop.Height = 26
        lblTop.TextAlign = ContentAlignment.MiddleLeft
        lblTop.Padding = New Padding(6, 0, 0, 0)
        lblTop.Font = New Font(lblTop.Font, FontStyle.Bold)

        ' ── Button panel ─────────────────────────────────────────────
        Dim pnlBtn As New FlowLayoutPanel()
        pnlBtn.Dock = DockStyle.Top
        pnlBtn.Height = 44
        pnlBtn.Padding = New Padding(6, 6, 6, 0)
        pnlBtn.FlowDirection = FlowDirection.LeftToRight

        Dim btnSave As New Button() With {.Text = "Save Mismatch Data", .Width = 160, .Height = 30,
                                          .BackColor = Color.DodgerBlue, .ForeColor = Color.White,
                                          .FlatStyle = FlatStyle.Flat}
        Dim btnLoadDb As New Button() With {.Text = "Load Saved Records", .Width = 150, .Height = 30}
        Dim btnClose As New Button() With {.Text = "Close", .Width = 80, .Height = 30}
        pnlBtn.Controls.AddRange(New Control() {btnSave, btnLoadDb, btnClose})

        ' ── Split: top=current mismatches, bottom=DB records ─────────
        Dim split As New SplitContainer()
        split.Dock = DockStyle.Fill
        split.Orientation = Orientation.Horizontal
        split.SplitterDistance = 240

        ' Top grid — current detected mismatches
        Dim lblMismatch As New Label() With {.Text = " Current Detected Mismatches",
                                             .Dock = DockStyle.Top, .Height = 22,
                                             .Font = New Font("Segoe UI", 8, FontStyle.Bold),
                                             .BackColor = Color.LightSalmon}
        Dim dgvMismatch As New DataGridView()
        dgvMismatch.Dock = DockStyle.Fill
        dgvMismatch.ReadOnly = True
        dgvMismatch.AllowUserToAddRows = False
        dgvMismatch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvMismatch.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvMismatch.RowHeadersVisible = False
        dgvMismatch.DataSource = mismatchRows

        ' Colour difference column red
        AddHandler dgvMismatch.CellFormatting,
            Sub(s2 As Object, ev As DataGridViewCellFormattingEventArgs)
                If ev.RowIndex < 0 Then Return
                If dgvMismatch.Columns(ev.ColumnIndex).Name = "Difference" Then
                    ev.CellStyle.BackColor = Color.LightCoral
                    ev.CellStyle.ForeColor = Color.DarkRed
                End If
            End Sub

        split.Panel1.Controls.Add(dgvMismatch)
        split.Panel1.Controls.Add(lblMismatch)

        ' Bottom grid — saved DB records
        Dim lblSaved As New Label() With {.Text = " Saved Mismatch Records  (Status: 0 = Not Processed | 1 = Processed)",
                                          .Dock = DockStyle.Top, .Height = 22,
                                          .Font = New Font("Segoe UI", 8, FontStyle.Bold),
                                          .BackColor = Color.LightSteelBlue}
        Dim dgvSaved As New DataGridView()
        dgvSaved.Dock = DockStyle.Fill
        dgvSaved.ReadOnly = True
        dgvSaved.AllowUserToAddRows = False
        dgvSaved.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvSaved.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvSaved.RowHeadersVisible = False

        split.Panel2.Controls.Add(dgvSaved)
        split.Panel2.Controls.Add(lblSaved)

        frm.Controls.Add(split)
        frm.Controls.Add(pnlBtn)
        frm.Controls.Add(lblTop)

        ' ── Helper: load saved records from DB ───────────────────────
        Dim loadSavedRecords As Action =
            Sub()
                Try
                    Dim dtDb As New DataTable()
                    dtDb.Columns.Add("pmd_id", GetType(String))
                    dtDb.Columns.Add("Trno", GetType(String))
                    dtDb.Columns.Add("Status", GetType(String))
                    dtDb.Columns.Add("Status Label", GetType(String))
                    dtDb.Columns.Add("Created", GetType(String))
                    dtDb.Columns.Add("Updated", GetType(String))

                    ' Load status=0 (not processed) and status=1 (processed)
                    For Each statusVal As Integer In New Integer() {0, 1}
                        Dim res As JObject = CallMismatchDataApi("SELECT", 0, Nothing,
                                                                 _companyInfo.ComId, _companyInfo.LocId, statusVal)
                        If res Is Nothing Then Continue For
                        Dim ok As Boolean = False
                        Boolean.TryParse(res("Success").ToString(), ok)
                        If Not ok OrElse res("Data") Is Nothing OrElse res("Data").Type <> JTokenType.Array Then Continue For

                        For Each item As JObject In CType(res("Data"), JArray)
                            Dim stRaw As String = item("pmd_status").ToString()
                            Dim stInt As Integer = 0
                            Integer.TryParse(stRaw, stInt)
                            Dim stLabel As String = If(stInt = 1, "Processed", "Not Processed")
                            Dim st As String = stInt.ToString()
                            dtDb.Rows.Add(
                                item("pmd_id").ToString(),
                                item("pmd_trno").ToString(),
                                st,
                                stLabel,
                                item("pmd_created").ToString(),
                                item("pmd_updated").ToString())
                        Next
                    Next

                    dgvSaved.DataSource = dtDb

                    ' Colour by status
                    For Each row As DataGridViewRow In dgvSaved.Rows
                        Dim stLbl As String = row.Cells("Status Label").Value.ToString()
                        If stLbl = "Processed" Then
                            row.DefaultCellStyle.BackColor = Color.LightGreen
                            row.DefaultCellStyle.ForeColor = Color.DarkGreen
                        ElseIf stLbl = "Not Processed" Then
                            row.DefaultCellStyle.BackColor = Color.LightYellow
                            row.DefaultCellStyle.ForeColor = Color.DarkOrange
                        End If
                    Next
                Catch ex As Exception
                    MessageBox.Show("Load error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Sub

        ' ── Save button ───────────────────────────────────────────────
        AddHandler btnSave.Click,
            Sub(s2 As Object, ev As EventArgs)
                Try
                    Dim saved As Integer = 0
                    Dim failed As Integer = 0

                    For Each dr As DataRow In mismatchRows.Rows
                        Dim trno As String = dr("Trno").ToString()
                        Dim res As JObject = CallMismatchDataApi("INSERT", 0, trno,
                                                                 _companyInfo.ComId, _companyInfo.LocId, 0)
                        Dim ok As Boolean = False
                        If res IsNot Nothing Then Boolean.TryParse(res("Success").ToString(), ok)
                        If ok Then saved += 1 Else failed += 1
                    Next

                    MessageBox.Show("Saved: " & saved & "   Failed: " & failed,
                                    "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    loadSavedRecords()
                Catch ex As Exception
                    MessageBox.Show("Save error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Sub

        ' ── Load button ───────────────────────────────────────────────
        AddHandler btnLoadDb.Click, Sub(s2 As Object, ev As EventArgs) loadSavedRecords()

        ' ── Close button ──────────────────────────────────────────────
        AddHandler btnClose.Click, Sub(s2 As Object, ev As EventArgs) frm.Close()

        frm.ShowDialog(Me)
    End Sub

End Class
