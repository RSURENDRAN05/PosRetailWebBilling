Imports System.Data.SqlClient
Imports Newtonsoft.Json
Imports System.Text
Imports System.Net
Imports System.IO
Imports System.Timers
Imports Newtonsoft.Json.Linq
Imports PosRetailWebBilling.clssalesProperty

Public Class FrmUploadSalesAutoSync
    Dim errMsg As String = ""
#Region "InitailProcess"
    Private Sub FrmUploadSalesAutoSync_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = "Web Ver 25.0.0.2 300825"
            Dim clientinfo As New pos_branch_systemstatus
            clientinfo = GetClientSystemStatus()
            If clientinfo.OnSalesActive = 0 Then
                properClass.R_Msgstring = "Online Sales Service Not Active"
                Dim frmmsgOk As New frmMsgBoxOk
                frmmsgOk.ShowDialog()
                Application.Exit()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TimerAutoSyncSales_Tick(sender As Object, e As EventArgs) Handles TimerAutoSyncSales.Tick
        Try
            If CheckForInternetConnection() Then
                _ReadSyncLocalCloud(callLocalData:=False, callWebCheck:=False)
                LoadPosSettings()
                If _globalSetting.AutoSyncSales = True Then
                    UploadSalesToCloud()
                    Threading.Thread.Sleep(5000)
                    UploadPayoutToCloud()
                End If
            End If
        Catch ex As Exception
            WriteErroLog("UploadSalesToCloud()", ex.Message)
        End Try
    End Sub
#End Region
#Region "UploadSalesData"
    Private Sub UploadSalesToCloud()
        Try
            Dim _results As Boolean
            Dim _msg As String = ""
            Dim _data As String = ""
            Dim _ds As New DataSet
            Dim _SqlPar(2) As SqlParameter
            _SqlPar(0) = New SqlParameter("@mode", "S")
            _SqlPar(1) = New SqlParameter("@trno", "0")
            _SqlPar(2) = New SqlParameter("@shiftno", "0")
            _ds = _sqlDataAdapter("sp_postsalestocloud", _SqlPar, errMsg)

            If _ds.Tables(0).Rows.Count > 0 Then
                ' Log sync start with total count
                LogSyncStart(_ds.Tables(0).Rows.Count)
                For Each _rowHdr In _ds.Tables(0).Rows
                    Dim trno = _rowHdr("psih_invoice_trno")

                    ' Log that we're starting to process this transaction
                    LogTransactionProcessing(trno)

                    Dim _dsBill As New DataSet
                    Dim _SqlPar1(2) As SqlParameter
                    _SqlPar1(0) = New SqlParameter("@mode", "GetTrno")
                    _SqlPar1(1) = New SqlParameter("@trno", trno)
                    _SqlPar1(2) = New SqlParameter("@shiftno", "0")
                    _dsBill = _sqlDataAdapter("sp_postsalestocloud", _SqlPar1, errMsg)
                    If _dsBill.Tables(0).Rows.Count > 0 AndAlso _dsBill.Tables(1).Rows.Count > 0 Then
                        ' Clean data before JSON serialization
                        CleanDataSetForJson(_dsBill)

                        Dim HdrData As String = JsonConvert.SerializeObject(_dsBill.Tables(0))
                        Dim DtlData As String = JsonConvert.SerializeObject(_dsBill.Tables(1))
                        Dim PaymodeData As String = JsonConvert.SerializeObject(_dsBill.Tables(2))
                        ' Additional cleaning of JSON strings to remove control characters
                        HdrData = CleanJsonString(HdrData)
                        DtlData = CleanJsonString(DtlData)
                        PaymodeData = CleanJsonString(PaymodeData)

                        Dim postData As String = String.Format("hdrdata={0}&dtldata={1}&paymodedata={2}",
                                                             Uri.EscapeDataString(HdrData),
                                                             Uri.EscapeDataString(DtlData),
                                                             Uri.EscapeDataString(PaymodeData))
                        If JsonPostSales(M_Details.LinkAjaxRequestSyncLocalCloud & "AjaxRequest=" & 2 & "&pm_id=" & _companyInfo.CompanyPMID & "&trno=" & trno & "&comid=" & _companyInfo.ComId & "&locid=" & _companyInfo.LocId, "POST", postData, _results, _msg, _data) = True Then
                            Dim _SqlPar2(2) As SqlParameter
                            _SqlPar2(0) = New SqlParameter("@mode", "TrnoUpdate")
                            _SqlPar2(1) = New SqlParameter("@trno", trno)
                            _SqlPar2(2) = New SqlParameter("@shiftno", "0")
                            If _ExecuteNonQuery("sp_postsalestocloud", _SqlPar2, errMsg) = True Then
                                WriteErroLog("Post Sales :" & "Data Saved Trno Updated : " & trno)
                                LogTransactionSuccess(trno, "Transaction uploaded and updated successfully")
                            Else
                                WriteErroLog("Post Sales :" & "Data Saved Not Trno Updated : " & trno)
                                LogTransactionFailure(trno, "Transaction uploaded but failed to update status")
                            End If

                        Else
                            If _msg = "Exists" Then
                                Dim _SqlPar2(2) As SqlParameter
                                _SqlPar2(0) = New SqlParameter("@mode", "TrnoUpdate")
                                _SqlPar2(1) = New SqlParameter("@trno", trno)
                                _SqlPar2(2) = New SqlParameter("@shiftno", "0")
                                If _ExecuteNonQuery("sp_postsalestocloud", _SqlPar2, errMsg) = True Then
                                    WriteErroLog("Post Sales :" & "Data Saved Trno Updated : " & trno)
                                    LogTransactionInfo(trno, "Transaction already exists - marked as processed")
                                Else
                                    WriteErroLog("Post Sales :" & "Data Saved Not Trno Updated : " & trno)
                                    LogTransactionFailure(trno, "Transaction exists but failed to update status")
                                End If
                            Else
                                LogTransactionFailure(trno, "Upload failed: " & _msg)
                            End If
                            WriteErroLog("Post Sales :" & "Data Not Saved,Trno Already Exists : " & trno)
                        End If
                    Else
                        ' Log when no data found for transaction
                        LogTransactionFailure(trno, "No data found for transaction")
                    End If
                Next

                ' Log sync completion
                LogSyncComplete()
            End If
            'otherTable Upload
            If _getCurrentShiftDayno(errMsg) = True Then

            End If

        Catch ex As Exception
            WriteErroLog("UploadSalesToCloud() Main Error", ex.Message)
            ' Log the error to RichTextBox as well
            LogTransactionFailure("SYSTEM", "UploadSalesToCloud Error: " & ex.Message)
        End Try
    End Sub
#End Region
#Region "JsonConversion"
    ''' <summary>
    ''' Clean JSON string to remove control characters that cause parsing errors
    ''' </summary>
    Private Function CleanJsonString(jsonString As String) As String
        If String.IsNullOrEmpty(jsonString) Then Return jsonString

        ' Remove control characters (ASCII 0-31 and 127) except allowed ones
        Dim cleanString As String = System.Text.RegularExpressions.Regex.Replace(jsonString, "[\x00-\x08\x0B\x0C\x0E-\x1F\x7F]", "")

        ' Replace common problematic characters
        cleanString = cleanString.Replace(vbCrLf, " ").Replace(vbCr, " ").Replace(vbLf, " ").Replace(vbTab, " ")

        ' Remove extra spaces
        cleanString = System.Text.RegularExpressions.Regex.Replace(cleanString, "\s+", " ")

        Return cleanString.Trim()
    End Function

    ''' <summary>
    ''' Clean DataSet string fields to remove control characters before JSON serialization
    ''' </summary>
    Private Sub CleanDataSetForJson(ds As DataSet)
        Try
            For Each table As DataTable In ds.Tables
                For Each row As DataRow In table.Rows
                    For Each column As DataColumn In table.Columns
                        If column.DataType Is GetType(String) AndAlso Not IsDBNull(row(column)) Then
                            Dim originalValue As String = row(column).ToString()
                            If Not String.IsNullOrEmpty(originalValue) Then
                                ' Clean the string value
                                Dim cleanValue As String = CleanStringField(originalValue)
                                row(column) = cleanValue
                            End If
                        End If
                    Next
                Next
            Next
        Catch ex As Exception
            WriteErroLog("CleanDataSetForJson Error", ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Clean individual string field to remove control characters
    ''' </summary>
    Private Function CleanStringField(value As String) As String
        If String.IsNullOrEmpty(value) Then Return value

        ' Remove control characters except allowed ones (tab, newline, carriage return will be converted to spaces)
        Dim cleanValue As String = System.Text.RegularExpressions.Regex.Replace(value, "[\x00-\x08\x0B\x0C\x0E-\x1F\x7F]", "")

        ' Convert newlines and tabs to spaces
        cleanValue = cleanValue.Replace(vbCrLf, " ").Replace(vbCr, " ").Replace(vbLf, " ").Replace(vbTab, " ")

        ' Remove extra spaces and trim
        cleanValue = System.Text.RegularExpressions.Regex.Replace(cleanValue, "\s+", " ").Trim()

        Return cleanValue
    End Function
#End Region
#Region "PostDataCloud"
    Public Function JsonPostSales(ByVal url As String, ByVal method As String, ByVal data As String, ByRef _results As Boolean, ByRef _msg As String, ByRef _data As String) As Boolean
        Try
            LogTransactionFailure("Post Sales Url :", url)
            Dim request As System.Net.WebRequest = System.Net.WebRequest.Create(url)
            request.Method = method
            Dim postData = data
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
            request.ContentType = "application/x-www-form-urlencoded" ' "application/json" '
            request.ContentLength = byteArray.Length
            request.Timeout = 30000 ' 30 seconds timeout

            Dim dataStream As System.IO.Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()

            Dim response As WebResponse = request.GetResponse()
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseText As String = reader.ReadToEnd()
            reader.Close()
            dataStream.Close()
            response.Close()

            ' Log the response for debugging
            WriteErroLog("Post Sales Response", "URL: " & url)
            WriteErroLog("Post Sales Response", "Response Length: " & responseText.Length.ToString())
            LogTransactionInfo("Post Sales Response", "Response Content: " & responseText)

            ' Check if response is empty
            If String.IsNullOrWhiteSpace(responseText) Then
                WriteErroLog("Post Sales Error", "Empty response received from server")
                _results = False
                _msg = "Empty response"
                _data = "No data received from server"
                Return False
            End If

            ' Check if response starts with expected JSON format
            If Not responseText.Trim().StartsWith("{") Then
                WriteErroLog("Post Sales Error", "Response is not JSON format. First 200 chars: " & If(responseText.Length > 200, responseText.Substring(0, 200), responseText))
                _results = False
                _msg = "Invalid response format"
                _data = "Response is not valid JSON"
                Return False
            End If

            ' Parse JSON response
            Dim Userparsejson As JObject = JObject.Parse(responseText)
            _data = If(Userparsejson("Data") IsNot Nothing, Userparsejson("Data").ToString, "")
            _results = If(Userparsejson("Success") IsNot Nothing, CBool(Userparsejson("Success")), False)
            _msg = If(Userparsejson("Msg") IsNot Nothing, Userparsejson("Msg").ToString, "")

            Return _results

        Catch jsonEx As Newtonsoft.Json.JsonReaderException
            WriteErroLog("Post Sales JSON Error", "Failed to parse response JSON: " & jsonEx.Message)
            _results = False
            _msg = "JSON parsing error"
            _data = jsonEx.Message
            Return False
        Catch webEx As WebException
            Dim errorResponse As String = ""
            If webEx.Response IsNot Nothing Then
                Try
                    Using responseStream As System.IO.Stream = webEx.Response.GetResponseStream()
                        Using errorReader As New StreamReader(responseStream)
                            errorResponse = errorReader.ReadToEnd()
                        End Using
                    End Using
                Catch
                    ' Ignore error reading response
                End Try
            End If
            WriteErroLog("Post Sales Web Error", webEx.Message & " - Response: " & errorResponse)
            _results = False
            _msg = "Network error"
            _data = webEx.Message
            Return False
        Catch ex As Exception
            Dim error1 As String = ex.Message
            If error1.Contains("Invalid URI") Then
                WriteErroLog("Post Sales Error", "ERROR! Must have HTTP:// before the URL.")
            Else
                WriteErroLog("Post Sales Error", error1)
            End If
            _results = False
            _msg = "General error"
            _data = error1
            Return False
        End Try
    End Function

#End Region
#Region "ErrorLog"
    ''' <summary>
    ''' Log successful transaction to RichTextBox with real-time display
    ''' </summary>
    Private Sub LogTransactionSuccess(trno As String, message As String)
        Try
            Dim timestamp As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            Dim logEntry As String = "[" & timestamp & "] SUCCESS - Trno: " & trno & " - " & message & vbCrLf

            ' Thread-safe UI update
            If RichTextBoxErrorLoadReader.InvokeRequired Then
                RichTextBoxErrorLoadReader.Invoke(Sub()
                                                      RichTextBoxErrorLoadReader.AppendText(logEntry)
                                                      RichTextBoxErrorLoadReader.SelectionStart = RichTextBoxErrorLoadReader.TextLength
                                                      RichTextBoxErrorLoadReader.ScrollToCaret()

                                                      ' Color the success entry green
                                                      Dim startIndex As Integer = RichTextBoxErrorLoadReader.TextLength - logEntry.Length
                                                      RichTextBoxErrorLoadReader.Select(startIndex, logEntry.Length)
                                                      RichTextBoxErrorLoadReader.SelectionColor = Color.Green
                                                      RichTextBoxErrorLoadReader.Select(RichTextBoxErrorLoadReader.TextLength, 0)
                                                  End Sub)
            Else
                RichTextBoxErrorLoadReader.AppendText(logEntry)
                RichTextBoxErrorLoadReader.SelectionStart = RichTextBoxErrorLoadReader.TextLength
                RichTextBoxErrorLoadReader.ScrollToCaret()

                ' Color the success entry green
                Dim startIndex As Integer = RichTextBoxErrorLoadReader.TextLength - logEntry.Length
                RichTextBoxErrorLoadReader.Select(startIndex, logEntry.Length)
                RichTextBoxErrorLoadReader.SelectionColor = Color.Green
                RichTextBoxErrorLoadReader.Select(RichTextBoxErrorLoadReader.TextLength, 0)
            End If
        Catch ex As Exception
            ' Fallback to regular error logging if UI update fails
            WriteErroLog("LogTransactionSuccess Error", ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Log failed transaction to RichTextBox with real-time display
    ''' </summary>
    Private Sub LogTransactionFailure(trno As String, message As String)
        Try
            Dim timestamp As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            Dim logEntry As String = "[" & timestamp & "] FAILED - Trno: " & trno & " - " & message & vbCrLf

            ' Thread-safe UI update
            If RichTextBoxErrorLoadReader.InvokeRequired Then
                RichTextBoxErrorLoadReader.Invoke(Sub()
                                                      RichTextBoxErrorLoadReader.AppendText(logEntry)
                                                      RichTextBoxErrorLoadReader.SelectionStart = RichTextBoxErrorLoadReader.TextLength
                                                      RichTextBoxErrorLoadReader.ScrollToCaret()

                                                      ' Color the failure entry red
                                                      Dim startIndex As Integer = RichTextBoxErrorLoadReader.TextLength - logEntry.Length
                                                      RichTextBoxErrorLoadReader.Select(startIndex, logEntry.Length)
                                                      RichTextBoxErrorLoadReader.SelectionColor = Color.Red
                                                      RichTextBoxErrorLoadReader.Select(RichTextBoxErrorLoadReader.TextLength, 0)
                                                  End Sub)
            Else
                RichTextBoxErrorLoadReader.AppendText(logEntry)
                RichTextBoxErrorLoadReader.SelectionStart = RichTextBoxErrorLoadReader.TextLength
                RichTextBoxErrorLoadReader.ScrollToCaret()

                ' Color the failure entry red
                Dim startIndex As Integer = RichTextBoxErrorLoadReader.TextLength - logEntry.Length
                RichTextBoxErrorLoadReader.Select(startIndex, logEntry.Length)
                RichTextBoxErrorLoadReader.SelectionColor = Color.Red
                RichTextBoxErrorLoadReader.Select(RichTextBoxErrorLoadReader.TextLength, 0)
            End If
        Catch ex As Exception
            ' Fallback to regular error logging if UI update fails
            WriteErroLog("LogTransactionFailure Error", ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Log informational transaction message to RichTextBox with real-time display
    ''' </summary>
    Private Sub LogTransactionInfo(trno As String, message As String)
        Try
            Dim timestamp As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            Dim logEntry As String = "[" & timestamp & "] INFO - Trno: " & trno & " - " & message & vbCrLf

            ' Thread-safe UI update
            If RichTextBoxErrorLoadReader.InvokeRequired Then
                RichTextBoxErrorLoadReader.Invoke(Sub()
                                                      RichTextBoxErrorLoadReader.AppendText(logEntry)
                                                      RichTextBoxErrorLoadReader.SelectionStart = RichTextBoxErrorLoadReader.TextLength
                                                      RichTextBoxErrorLoadReader.ScrollToCaret()

                                                      ' Color the info entry blue
                                                      Dim startIndex As Integer = RichTextBoxErrorLoadReader.TextLength - logEntry.Length
                                                      RichTextBoxErrorLoadReader.Select(startIndex, logEntry.Length)
                                                      RichTextBoxErrorLoadReader.SelectionColor = Color.Blue
                                                      RichTextBoxErrorLoadReader.Select(RichTextBoxErrorLoadReader.TextLength, 0)
                                                  End Sub)
            Else
                RichTextBoxErrorLoadReader.AppendText(logEntry)
                RichTextBoxErrorLoadReader.SelectionStart = RichTextBoxErrorLoadReader.TextLength
                RichTextBoxErrorLoadReader.ScrollToCaret()

                ' Color the info entry blue
                Dim startIndex As Integer = RichTextBoxErrorLoadReader.TextLength - logEntry.Length
                RichTextBoxErrorLoadReader.Select(startIndex, logEntry.Length)
                RichTextBoxErrorLoadReader.SelectionColor = Color.Blue
                RichTextBoxErrorLoadReader.Select(RichTextBoxErrorLoadReader.TextLength, 0)
            End If
        Catch ex As Exception
            ' Fallback to regular error logging if UI update fails
            WriteErroLog("LogTransactionInfo Error", ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Log transaction processing start to RichTextBox
    ''' </summary>
    Private Sub LogTransactionProcessing(trno As String)
        Try
            Dim timestamp As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            Dim logEntry As String = "[" & timestamp & "] PROCESSING - Trno: " & trno & " - Starting upload..." & vbCrLf

            ' Thread-safe UI update
            If RichTextBoxErrorLoadReader.InvokeRequired Then
                RichTextBoxErrorLoadReader.Invoke(Sub()
                                                      RichTextBoxErrorLoadReader.AppendText(logEntry)
                                                      RichTextBoxErrorLoadReader.SelectionStart = RichTextBoxErrorLoadReader.TextLength
                                                      RichTextBoxErrorLoadReader.ScrollToCaret()

                                                      ' Color the processing entry orange
                                                      Dim startIndex As Integer = RichTextBoxErrorLoadReader.TextLength - logEntry.Length
                                                      RichTextBoxErrorLoadReader.Select(startIndex, logEntry.Length)
                                                      RichTextBoxErrorLoadReader.SelectionColor = Color.Orange
                                                      RichTextBoxErrorLoadReader.Select(RichTextBoxErrorLoadReader.TextLength, 0)
                                                  End Sub)
            Else
                RichTextBoxErrorLoadReader.AppendText(logEntry)
                RichTextBoxErrorLoadReader.SelectionStart = RichTextBoxErrorLoadReader.TextLength
                RichTextBoxErrorLoadReader.ScrollToCaret()

                ' Color the processing entry orange
                Dim startIndex As Integer = RichTextBoxErrorLoadReader.TextLength - logEntry.Length
                RichTextBoxErrorLoadReader.Select(startIndex, logEntry.Length)
                RichTextBoxErrorLoadReader.SelectionColor = Color.Orange
                RichTextBoxErrorLoadReader.Select(RichTextBoxErrorLoadReader.TextLength, 0)
            End If
        Catch ex As Exception
            ' Fallback to regular error logging if UI update fails
            WriteErroLog("LogTransactionProcessing Error", ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Log sync process start
    ''' </summary>
    Private Sub LogSyncStart(totalTransactions As Integer)
        Try
            Dim timestamp As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            Dim separator As String = String.Empty.PadRight(60, "="c)
            Dim logEntry As String = "[" & timestamp & "] SYNC START - Found " & totalTransactions.ToString() & " transactions to process" & vbCrLf & separator & vbCrLf

            ' Thread-safe UI update
            If RichTextBoxErrorLoadReader.InvokeRequired Then
                RichTextBoxErrorLoadReader.Invoke(Sub()
                                                      RichTextBoxErrorLoadReader.AppendText(logEntry)
                                                      RichTextBoxErrorLoadReader.SelectionStart = RichTextBoxErrorLoadReader.TextLength
                                                      RichTextBoxErrorLoadReader.ScrollToCaret()

                                                      ' Color the sync start entry purple
                                                      Dim startIndex As Integer = RichTextBoxErrorLoadReader.TextLength - logEntry.Length
                                                      RichTextBoxErrorLoadReader.Select(startIndex, logEntry.Length)
                                                      RichTextBoxErrorLoadReader.SelectionColor = Color.Purple
                                                      RichTextBoxErrorLoadReader.Select(RichTextBoxErrorLoadReader.TextLength, 0)
                                                  End Sub)
            Else
                RichTextBoxErrorLoadReader.AppendText(logEntry)
                RichTextBoxErrorLoadReader.SelectionStart = RichTextBoxErrorLoadReader.TextLength
                RichTextBoxErrorLoadReader.ScrollToCaret()

                ' Color the sync start entry purple
                Dim startIndex As Integer = RichTextBoxErrorLoadReader.TextLength - logEntry.Length
                RichTextBoxErrorLoadReader.Select(startIndex, logEntry.Length)
                RichTextBoxErrorLoadReader.SelectionColor = Color.Purple
                RichTextBoxErrorLoadReader.Select(RichTextBoxErrorLoadReader.TextLength, 0)
            End If
        Catch ex As Exception
            WriteErroLog("LogSyncStart Error", ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Log sync process completion
    ''' </summary>
    Private Sub LogSyncComplete()
        Try
            Dim timestamp As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            Dim separator As String = String.Empty.PadRight(60, "="c)
            Dim logEntry As String = separator & vbCrLf & "[" & timestamp & "] SYNC COMPLETE - Batch processing finished" & vbCrLf & vbCrLf

            ' Thread-safe UI update
            If RichTextBoxErrorLoadReader.InvokeRequired Then
                RichTextBoxErrorLoadReader.Invoke(Sub()
                                                      RichTextBoxErrorLoadReader.AppendText(logEntry)
                                                      RichTextBoxErrorLoadReader.SelectionStart = RichTextBoxErrorLoadReader.TextLength
                                                      RichTextBoxErrorLoadReader.ScrollToCaret()

                                                      ' Color the sync complete entry purple
                                                      Dim startIndex As Integer = RichTextBoxErrorLoadReader.TextLength - logEntry.Length
                                                      RichTextBoxErrorLoadReader.Select(startIndex, logEntry.Length)
                                                      RichTextBoxErrorLoadReader.SelectionColor = Color.Purple
                                                      RichTextBoxErrorLoadReader.Select(RichTextBoxErrorLoadReader.TextLength, 0)
                                                  End Sub)
            Else
                RichTextBoxErrorLoadReader.AppendText(logEntry)
                RichTextBoxErrorLoadReader.SelectionStart = RichTextBoxErrorLoadReader.TextLength
                RichTextBoxErrorLoadReader.ScrollToCaret()

                ' Color the sync complete entry purple
                Dim startIndex As Integer = RichTextBoxErrorLoadReader.TextLength - logEntry.Length
                RichTextBoxErrorLoadReader.Select(startIndex, logEntry.Length)
                RichTextBoxErrorLoadReader.SelectionColor = Color.Purple
                RichTextBoxErrorLoadReader.Select(RichTextBoxErrorLoadReader.TextLength, 0)
            End If
        Catch ex As Exception
            WriteErroLog("LogSyncComplete Error", ex.Message)
        End Try
    End Sub


#End Region
#Region "ButtonAction"
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            RichTextBoxErrorLoadReader.Text = ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles btnstop.Click
        Try
            TimerAutoSyncSales.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnstart_Click(sender As Object, e As EventArgs) Handles btnstart.Click
        Try
            TimerAutoSyncSales.Enabled = True
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "UploadPayoutData"
    Private Function UploadPayoutToCloud() As Boolean
        Try
            Dim _results As Boolean
            Dim _msg As String = ""
            Dim _data As String = ""
            Dim _ds As New DataSet
            Dim _SqlPar(2) As SqlParameter
            _SqlPar(0) = New SqlParameter("@mode", "SD")
            _SqlPar(1) = New SqlParameter("@deleteSatus", "0")
            _SqlPar(2) = New SqlParameter("@payd_id", "0")
            _ds = _sqlDataAdapter("sp_uploadpayout", _SqlPar, errMsg)
            LogTransactionInfo("Getting The Payout Details", errMsg)
            If _ds.Tables(0).Rows.Count > 0 Then
                LogTransactionInfo("Get Record The Payout Details", _ds.Tables(0).Rows.Count)
                Dim HdrData As String = JsonConvert.SerializeObject(_ds.Tables(0))
                HdrData = CleanJsonString(HdrData)
                Dim postData As String = String.Format("payoutdata={0}", Uri.EscapeDataString(HdrData))
                For Each _rowHdr In _ds.Tables(0).Rows
                    Dim trno = _rowHdr("payd_id")
                    Dim deleteSt = _rowHdr("payd_deletestatus")
                    If JsonPostSales(M_Details.LinkAjaxRequestSyncLocalCloud & "AjaxRequest=" & 8 & "&pm_id=" & _companyInfo.CompanyPMID & "&comid=" & _companyInfo.ComId & "&locid=" & _companyInfo.LocId, "POST", postData, _results, _msg, _data) = True Then
                        Dim _SqlPar2(2) As SqlParameter
                        _SqlPar2(0) = New SqlParameter("@mode", "UD")
                        _SqlPar2(1) = New SqlParameter("@deleteSatus", deleteSt) ' if "D" delete record
                        _SqlPar2(2) = New SqlParameter("@payd_id", trno)
                        If _ExecuteNonQuery("sp_uploadpayout", _SqlPar2, errMsg) = True Then
                            LogTransactionSuccess("Post Payout :" & "Data Saved Trno Updated : ", trno)

                        Else
                            LogTransactionFailure("Post Payout :" & "Data Saved Not Trno Updated : ", trno)
                        End If

                    Else
                        LogTransactionFailure("Post Payout :" & "Data Not Saved,Trno Already Exists : ", _msg)
                    End If
                Next
            End If
            ' Log sync completion
            LogSyncComplete()
            Return True
        Catch ex As Exception
            LogTransactionFailure("UploadPayoutToCloud Error", ex.Message)
        End Try
        Return False
    End Function
#End Region
End Class
