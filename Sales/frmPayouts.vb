
'bs.Filter = "Field LIKE '%test%', "
Imports DevExpress.XtraGrid
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Data
Imports System
Imports PosRetailWebBilling.clssalesProperty
Imports System.Net
Imports Newtonsoft.Json.Linq
Imports System.Text

Public Class frmPayouts
    Dim StaffTable As DataTable
    Dim ledgerType As Integer = 1
    Dim _dsDataLoad As DataSet
    Dim errMsg As String
    Dim keyTextNum As New xkeyboard
    Dim _newSave As Boolean = False
    Dim _casdr As New RawPrinter
    Dim ModeOfUpload As String = "Local"
    Public Function CreateStaffTable() As DataTable
        Try
            StaffTable = New DataTable
            StaffTable.TableName = "StaffTable"
            StaffTable.Columns.Add("Id", GetType(Integer)).AutoIncrement = True  '0
            StaffTable.Columns.Add("Name", GetType(String)).DefaultValue = "SP" '19
            Return StaffTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub _DsLoad(ByRef _mode As String)
        Try
            _dsDataLoad = New DataSet
            If _JsonData.SalesManDataTable.Rows.Count > 0 Then
                StaffTable.BeginInit()
                StaffTable.Rows.Add(0, "SelectAll")
                For Each rowStaff In _JsonData.SalesManDataTable.Rows
                    Dim id = rowStaff("Id")
                    Dim name = rowStaff("SalesMan")
                    StaffTable.Rows.Add(id, name)
                Next
                StaffTable.AcceptChanges()
                GridControl1.DataSource = StaffTable
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            _newSave = True
            txtId.Text = GridView1.GetFocusedRowCellValue("Id").ToString
            txtName.Text = GridView1.GetFocusedRowCellValue("Name").ToString
            txtRemarks.Text = "Advance To " & txtName.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmPayouts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If CheckForInternetConnection() = True Then
                ModeOfUpload = "Web"
            End If
            GridControl1.DataSource = CreateStaffTable()
            _DsLoad("STA")
            If ModeOfUpload = "Local" Then
                _PayoutDetailsLoad()
            Else
                GetPayoutDataCloud()
            End If

            PayDateFrom.EditValue = Date.Now
            PayDateTo.EditValue = Date.Now
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtName_Click(sender As Object, e As EventArgs)
        Try
            Dim keyTextNum As New xkeyboard
            properClass.R_TextNumKey = txtName.Text
            keyTextNum.ShowDialog()
            txtName.Text = properClass.R_TextNumKey
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtAmount_Click(sender As Object, e As EventArgs) Handles txtAmount.Click
        Try
            Dim keyTextNum As New frmKeyQtyAmt
            keyTextNum.ShowDialog()
            txtAmount.Text = Format(properClass.MKeyQtyAmt, "####0.00")

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtRemarks_Click(sender As Object, e As EventArgs) Handles txtRemarks.Click
        Try
            Dim keyTextNum As New xkeyboard
            properClass.R_TextNumKey = txtRemarks.Text
            keyTextNum.ShowDialog()
            txtRemarks.Text = properClass.R_TextNumKey
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim _results As Boolean
            Dim _msg As String = ""
            Dim _data As String = ""
            If Not String.IsNullOrEmpty(txtId.Text) OrElse String.IsNullOrEmpty(txtAmount.Text) Then
                Dim remas As String = ""
                If String.IsNullOrEmpty(txtRemarks.Text) Then
                    remas = txtName.Text
                Else
                    remas = txtRemarks.Text
                End If
                If Not IsNumeric(txtAmount.Text) Then
                    MessageBox.Show("Please Enter Valid Amount", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
                If Val(txtAmount.Text) <= 0 Then
                    MessageBox.Show("Amount Should Be Greater Than Zero", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
                If Val(txtAmount.Text) > 1000000 Then
                    MessageBox.Show("Amount Should Be Less Than 1,000,000", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
                If String.IsNullOrEmpty(txtName.Text) Then
                    MessageBox.Show("Please Select Name", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
                If ModeOfUpload = "Local" Then
                    If _newSave = True Then
                        Dim _SqlpayoutSave(11) As SqlParameter
                        _SqlpayoutSave(0) = New SqlParameter("@mode", "I")
                        _SqlpayoutSave(1) = New SqlParameter("@payd_id", "0")
                        _SqlpayoutSave(2) = New SqlParameter("@payd_ledgerid", txtId.Text)
                        _SqlpayoutSave(3) = New SqlParameter("@payd_name", txtName.Text)
                        _SqlpayoutSave(4) = New SqlParameter("@payd_amount", txtAmount.Text)
                        _SqlpayoutSave(5) = New SqlParameter("@payd_remarks", remas)
                        _SqlpayoutSave(6) = New SqlParameter("@payd_shiftno", _saleSetting._curShiftno)
                        _SqlpayoutSave(7) = New SqlParameter("@payd_dayno", _saleSetting._curDayno)
                        _SqlpayoutSave(8) = New SqlParameter("@payd_user", _companyInfo.UserId)
                        _SqlpayoutSave(9) = New SqlParameter("@payd_ledgertype", ledgerType)
                        _SqlpayoutSave(10) = New SqlParameter("@POS_MACHINEID", RegistrationDetails._machineId)
                        _SqlpayoutSave(11) = New SqlParameter("@POS_MACHINENAME", RegistrationDetails._localPcname)
                        If _ExecuteNonQuery("sp_payout_save", _SqlpayoutSave, errMsg) = False Then
                            WriteErroLog(errMsg)
                        Else
                            _newSave = False
                            txtAmount.Text = ""
                            txtId.Text = ""
                            txtName.Text = ""
                            _PayoutDetailsLoad()
                            Dim wSalesPrint As New WindowsPrinter
                            If wSalesPrint._PayOutSinglePrint = False Then
                                WriteErroLog(errMsg)
                            Else
                                Dim windprint As New PrintCommand
                                If windprint._PayoutSingleEntry() = False Then
                                    'MessageBox.Show("Not Print")
                                End If
                                _casdr.OpenCashdrawer(True)

                            End If
                        End If
                    End If
                Else
                    'web
                    Dim payoutcls As New Payout
                    payoutcls.payd_refid = txtId.Text
                    payoutcls.payd_ledgerid = txtId.Text
                    payoutcls.payd_name = txtName.Text
                    payoutcls.payd_amount = txtAmount.Text
                    payoutcls.payd_remarks = remas
                    payoutcls.payd_shiftno = _saleSetting._curShiftno
                    payoutcls.payd_dayno = _saleSetting._curDayno
                    payoutcls.payd_user = _companyInfo.UserId
                    Dim fromDate As Date = Convert.ToDateTime(PayDateFrom.EditValue).Date
                    payoutcls.payd_datetime = fromDate.ToString("yyyy-MM-dd")
                    Dim HdrData As String = Newtonsoft.Json.JsonConvert.SerializeObject(payoutcls)
                    HdrData = CleanJsonString(HdrData)
                    Dim postData As String = String.Format("payoutdata={0}", Uri.EscapeDataString(HdrData))
                    If JsonPostSales(M_Details.LinkAjaxRequestSyncLocalCloud & "AjaxRequest=" & 11 & "&comid=" & _companyInfo.ComId & "&locid=" & _companyInfo.LocId, "POST", postData, _results, _msg, _data) = True Then
                        _newSave = False
                        txtAmount.Text = ""
                        txtId.Text = ""
                        txtName.Text = ""
                        GetPayoutDataCloud()
                        Dim wSalesPrint As New WindowsPrinter
                        If wSalesPrint._PayOutSinglePrint = False Then
                            WriteErroLog(errMsg)
                        Else
                            Dim windprint As New PrintCommand
                            If windprint._PayoutSingleEntry() = False Then
                                'MessageBox.Show("Not Print")
                            End If
                            _casdr.OpenCashdrawer(True)

                        End If
                    End If

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _PayoutDetailsLoad()
        Try
            Dim _DSpayoyut As New DataSet
            Dim _SqlpayoutSave(11) As SqlParameter
            _SqlpayoutSave(0) = New SqlParameter("@mode", "S") 'S supplier / 'Staff
            _SqlpayoutSave(1) = New SqlParameter("@payd_id", "0")
            _SqlpayoutSave(2) = New SqlParameter("@payd_ledgerid", "0")
            _SqlpayoutSave(3) = New SqlParameter("@payd_name", "0")
            _SqlpayoutSave(4) = New SqlParameter("@payd_amount", "0")
            _SqlpayoutSave(5) = New SqlParameter("@payd_remarks", "0")
            _SqlpayoutSave(6) = New SqlParameter("@payd_shiftno", _saleSetting._curShiftno)
            _SqlpayoutSave(7) = New SqlParameter("@payd_dayno", "0")
            _SqlpayoutSave(8) = New SqlParameter("@payd_user", "0")
            _SqlpayoutSave(9) = New SqlParameter("@payd_ledgertype", ledgerType)
            _SqlpayoutSave(10) = New SqlParameter("@POS_MACHINEID", RegistrationDetails._machineId)
            _SqlpayoutSave(11) = New SqlParameter("@POS_MACHINENAME", RegistrationDetails._localPcname)
            _DSpayoyut = _sqlDataAdapter2("sp_payout_save", _SqlpayoutSave)
            If _DSpayoyut.Tables(0).Rows.Count > 0 Then
                GridControl2.DataSource = _DSpayoyut.Tables(0)
            Else
                GridControl2.DataSource = _DSpayoyut.Tables(0)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView2_RowClick(sender As Object, e As Views.Grid.RowClickEventArgs) Handles GridView2.RowClick
        Try
            Try
                _newSave = False
                txtId.Text = GridView2.GetFocusedRowCellValue("ID").ToString
                txtName.Text = GridView2.GetFocusedRowCellValue("Name").ToString
                txtAmount.Text = GridView2.GetFocusedRowCellValue("Amount").ToString
                txtRemarks.Text = GridView2.GetFocusedRowCellValue("Remarks").ToString
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click

        Try
            Dim _results As Boolean
            Dim _msg As String = ""
            Dim _data As String = ""
            frmKeyPassIIIMaster.ShowDialog()
            If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
                If ModeOfUpload = "Local" Then
                    properClass.R_Msgstring = txtId.Text & " -" & txtName.Text & vbNewLine & "Do You Want To Delete?"
                    Dim frmmsg As New frmMsgBox
                    frmmsg.ShowDialogData(properClass.R_Msgstring, False)
                    If frmmsg.DialogResult = Windows.Forms.DialogResult.Yes Then
                        Dim _DSpayoyut As New DataSet
                        Dim _SqlpayoutSave(11) As SqlParameter
                        _SqlpayoutSave(0) = New SqlParameter("@mode", "D")
                        _SqlpayoutSave(1) = New SqlParameter("@payd_id", txtId.Text)
                        _SqlpayoutSave(2) = New SqlParameter("@payd_ledgerid", "0")
                        _SqlpayoutSave(3) = New SqlParameter("@payd_name", "0")
                        _SqlpayoutSave(4) = New SqlParameter("@payd_amount", "0")
                        _SqlpayoutSave(5) = New SqlParameter("@payd_remarks", "0")
                        _SqlpayoutSave(6) = New SqlParameter("@payd_shiftno", _saleSetting._curShiftno)
                        _SqlpayoutSave(7) = New SqlParameter("@payd_dayno", "0")
                        _SqlpayoutSave(8) = New SqlParameter("@payd_user", "0")
                        _SqlpayoutSave(9) = New SqlParameter("@payd_ledgertype", ledgerType)
                        _SqlpayoutSave(10) = New SqlParameter("@POS_MACHINEID", RegistrationDetails._machineId)
                        _SqlpayoutSave(11) = New SqlParameter("@POS_MACHINENAME", RegistrationDetails._localPcname)
                        If _ExecuteNonQuery("sp_payout_save", _SqlpayoutSave, errMsg) = True Then
                            WriteAuditLog(_companyInfo.UserId, "PayOutDelete", "PayoutName :" & txtName.Text & "- Amount" & txtAmount.Text)
                            _PayoutDetailsLoad()

                            txtAmount.Text = ""
                            txtId.Text = ""
                            txtName.Text = ""
                        End If
                    End If
                Else
                    'web
                    properClass.R_Msgstring = txtId.Text & " -" & txtName.Text & vbNewLine & "Do You Want To Delete?"
                    Dim frmmsg As New frmMsgBox 'YesNo
                    frmmsg.ShowDialogData(properClass.R_Msgstring.ToString, False) 'YesNo
                    If frmmsg.DialogResult = Windows.Forms.DialogResult.Yes Then
                        Dim payoutcls As New Payout
                        payoutcls.payd_id = txtId.Text
                        Dim HdrData As String = Newtonsoft.Json.JsonConvert.SerializeObject(payoutcls)
                        HdrData = CleanJsonString(HdrData)
                        Dim postData As String = String.Format("payoutdata={0}", Uri.EscapeDataString(HdrData))
                        If JsonPostSales(M_Details.LinkAjaxRequestSyncLocalCloud & "AjaxRequest=" & 12 & "&comid=" & _companyInfo.ComId & "&locid=" & _companyInfo.LocId, "POST", postData, _results, _msg, _data) = True Then
                            WriteAuditLog(_companyInfo.UserId, "PayOutDelete", "PayoutName :" & txtName.Text & "- Amount" & txtAmount.Text)
                            GetPayoutDataCloud()
                            txtAmount.Text = ""
                            txtId.Text = ""
                            txtName.Text = ""
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnStaff_Click(sender As Object, e As EventArgs) Handles btnStaff.Click
        ledgerType = 2
        lblHead.Text = "PayOut " & ledgerType & " Staff "
        _DsLoad("STA")
    End Sub

    Private Sub btnSupp_Click(sender As Object, e As EventArgs) Handles btnSupp.Click
        ledgerType = 1
        lblHead.Text = "PayOut " & ledgerType & " Supplier "
        _DsLoad("SUP")
    End Sub


    Private Sub btnModeofWeb_Click(sender As Object, e As EventArgs) Handles btnModeofWeb.Click
        Try
            If ModeOfUpload = "Web" Then
                ModeOfUpload = "Local"
                btnModeofWeb.Text = "Local"
                lblHead.Text = "Payout - Local Mode"
                _PayoutDetailsLoad()

            Else
                ModeOfUpload = "Web"
                btnModeofWeb.Text = "Web"
                lblHead.Text = "Payout - Web Mode"
                'GetPayoutDataCloud()
            End If

        Catch ex As Exception

        End Try
    End Sub
#Region "PostDataCloud"
    Public Function JsonPostSales(ByVal url As String, ByVal method As String, ByVal data As String, ByRef _results As Boolean, ByRef _msg As String, ByRef _data As String) As Boolean
        Try

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

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            GetPayoutDataCloud()
            GetCommissionDataCloud()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetPayoutDataCloud()
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            'Get date range from UI
            dialog.Caption = "Loading Payout Data..."
            Dim Dts As DataTable
            Dim empId = GridView1.GetFocusedRowCellValue("Id")
            Dim fromDate As Date = Convert.ToDateTime(PayDateFrom.EditValue).Date
            Dim toDate As Date = Convert.ToDateTime(PayDateTo.EditValue).Date
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequestSyncLocalCloud & "AjaxRequest=13&comid=" & _companyInfo.ComId & "&locid=" & _companyInfo.LocId & "&fromdate=" & fromDate.ToString("yyyy-MM-dd") & "&todate=" & toDate.ToString("yyyy-MM-dd") & "&empid=" & empId.ToString)
            Dim Userparsejson As JObject = JObject.Parse(json)
            Dts = Userparsejson("Data").ToObject(Of DataTable)()
            If Dts.Rows.Count > 0 Then
                GridControl2.DataSource = Dts
                dialog.Close()
            Else
                GridControl2.DataSource = Nothing
            End If
        Catch ex As Exception
            dialog.Close()
            WriteErroLog("Payout Search Error", ex.Message)
        Finally
            dialog.Close()
        End Try
    End Sub
    Private Sub GetCommissionDataCloud()
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            'Get date range from UI
            dialog.Caption = "Loading Sales Data..."
            Dim Dts As DataTable
            Dim empId = GridView1.GetFocusedRowCellValue("Id")
            Dim fromDate As Date = Convert.ToDateTime(PayDateFrom.EditValue).Date
            Dim toDate As Date = Convert.ToDateTime(PayDateTo.EditValue).Date
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequestSyncLocalCloud & "AjaxRequest=5&comid=" & _companyInfo.ComId & "&locid=" & _companyInfo.LocId & "&startDate=" & fromDate.ToString("yyyy-MM-dd") & "&endDate=" & toDate.ToString("yyyy-MM-dd") & "&salesmanId=" & empId)
            Dim Userparsejson As JObject = JObject.Parse(json)
            Dts = Userparsejson("Data").ToObject(Of DataTable)()
            If Dts.Rows.Count > 0 Then
                ' Using LINQ to filter DataTable
                ' Using LINQ with decimal conversion
               
                GridControl3.DataSource = Dts
               
            dialog.Close()
            Else
            GridControl3.DataSource = Nothing
            End If
        Catch ex As Exception
            dialog.Close()
            WriteErroLog("Payout Search Error", ex.Message)
        Finally
            dialog.Close()
        End Try
    End Sub
    Private Sub btnAdvance_Click(sender As Object, e As EventArgs) Handles btnAdvance.Click
        Try
            GridControl3.Visible = False
            GridControl2.Visible = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSales_Click(sender As Object, e As EventArgs) Handles btnSales.Click
        Try
            GridControl2.Visible = False
            GridControl3.Visible = True
        Catch ex As Exception

        End Try
    End Sub
End Class
Public Class Payout
    Public Property payd_id As Integer
    Public Property payd_refid As Integer
    Public Property payd_ledgerid As Integer
    Public Property payd_name As String
    Public Property payd_amount As Decimal
    Public Property payd_remarks As String
    Public Property payd_shiftno As Integer
    Public Property payd_dayno As Integer
    Public Property payd_user As Integer
    Public Property payd_datetime As DateTime
End Class
