Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Text
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.BrickAlignment
Imports DevExpress.XtraPrinting.PageHeaderFooter

Public Class FrmAttendanceReport
    Private dtSalesData As DataTable
    Private dtSalesManData As DataTable
    Dim Errstr As String
#Region "InitialLoad"
    Private Sub frmSalesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize company info first

        ' Set default dates
        dtStartDate.DateTime = DateTime.Today
        dtEndDate.DateTime = DateTime.Today

        ' Initialize status
        lblStatusResults.Text = "Ready to load data..."
        lblFillterDetails.Text = "Filter Details: No data loaded"

        ' Initialize all components

        InitializeListBoxControls()

        ' Load initial data
        LoadSalesData()
    End Sub

    Private Sub InitializeListBoxControls()
        Try
            ' Initialize Report Type ListBox
            InitializeReportTypeList()

            ' Initialize Report List
            InitializeReportList()

            ' Load SalesMan data and initialize SalesMan ListBox
            LoadSalesManData()
            InitializeSalesManList()

        Catch ex As Exception
            MessageBox.Show("Error initializing controls: " & ex.Message, "Initialization Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InitializeReportTypeList()
        ' Clear existing items
        ListBoxControlReportType.Items.Clear()

        ' Add report type options
        ListBoxControlReportType.Items.Add("DATEWISE")
        ListBoxControlReportType.Items.Add("MONTHLY")

        ' Set default selection
        If ListBoxControlReportType.Items.Count > 0 Then
            ListBoxControlReportType.SelectedIndex = 0 ' Select "Summary" by default
        End If
    End Sub

    Private Sub InitializeReportList()
        ' Clear existing items
        ListBoxControlReportList.Items.Clear()

        ' Add available report options
        ListBoxControlReportList.Items.Add("Attendance by Employee")
        ListBoxControlReportList.Items.Add("Attendance by All")

        ' Set default selection
        If ListBoxControlReportList.Items.Count > 0 Then
            ListBoxControlReportList.SelectedIndex = 0 ' Select first report by default
        End If
    End Sub

    Private Sub LoadSalesManData()
        Try
            ' Get SalesMan data from API or database
            dtSalesManData = GetSalesManDataFromAPI()

        Catch ex As Exception
            ' Create a default empty datatable if API fails
            dtSalesManData = New DataTable()
            dtSalesManData.Columns.Add("emp_id", GetType(String))
            dtSalesManData.Columns.Add("emp_printname", GetType(String))

        End Try
    End Sub

    Private Sub InitializeSalesManList()
        Try
            ' Clear existing items
            ListBoxControlSalemanList.Items.Clear()

            ' Add "Select All" option first
            ListBoxControlSalemanList.Items.Add("0 - Select All")

            ' Add salesman data from the DataTable
            If dtSalesManData IsNot Nothing AndAlso dtSalesManData.Rows.Count > 0 Then
                For Each row As DataRow In dtSalesManData.Rows
                    Dim salesManId As String = row("Id").ToString()
                    Dim salesManName As String = row("SalesMan").ToString()
                    ListBoxControlSalemanList.Items.Add(salesManId & " - " & salesManName)
                Next
            End If

            ' Set default selection to "Select All"
            If ListBoxControlSalemanList.Items.Count > 0 Then
                ListBoxControlSalemanList.SelectedIndex = 0
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading salesman list: " & ex.Message, "Data Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetSalesManDataFromAPI() As DataTable
        Try
            ' Use existing API call pattern
            Dim comId As String = _companyInfo.ComId
            Dim locId As String = _companyInfo.LocId
            Dim dt As New DataTable()
            dt.Columns.Add("Id", GetType(String))
            dt.Columns.Add("SalesMan", GetType(String))
            ' Parse JSON response to DataTable
            If _JsonData.SalesManDataTable.Rows.Count = 0 Then
                GetSalesmanData()
            Else
                For Each item In _JsonData.SalesManDataTable.Rows
                    Dim row As DataRow = dt.NewRow()
                    row("Id") = If(item("Id"), "")
                    row("SalesMan") = If(item("SalesMan"), "")
                    dt.Rows.Add(row)
                Next
                Return dt
            End If
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region
#Region "ConfigureColumn"
    Private Sub ConfigureGridColumns(reportType As String)
        ' First hide ALL columns
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In GridView1.Columns
            col.Visible = False
        Next

        If reportType.ToUpper() = "DATEWISE" Then
            ' DATEWISE columns - ordered logically
            ConfigureColumn("emp_id", "Emp ID", 60, True, 0)
            ConfigureColumn("emp_printname", "Employee Name", 180, True, 1)
            ConfigureColumn("att_date", "Date", 100, True, 2)
            ConfigureColumn("morning_in", "Morning In", 130, True, 3)
            ConfigureColumn("morning_out", "Morning Out", 130, True, 4)
            ConfigureColumn("break_in", "Break In", 130, True, 5)
            ConfigureColumn("evening_out", "Evening Out", 130, True, 6)
            ConfigureColumn("total_morning_hours", "Morning Hrs", 90, True, 7)
            ConfigureColumn("total_break_hours", "Break Hrs", 90, True, 8)
            ConfigureColumn("total_work_hours", "Work Hrs", 90, True, 9)
            ConfigureColumn("pcm_name", "Company", 140, True, 10)
            ConfigureColumn("plm_name", "Location", 140, True, 11)
        ElseIf reportType.ToUpper() = "MONTHLY" Then
            ' MONTHLY summary columns - ordered logically
            ConfigureColumn("emp_id", "Emp ID", 60, True, 0)
            ConfigureColumn("emp_printname", "Employee Name", 200, True, 1)
            ConfigureColumn("total_days", "Total Days", 90, True, 2)
            ConfigureColumn("total_morning_hours", "Total Morning Hrs", 120, True, 3)
            ConfigureColumn("total_break_hours", "Total Break Hrs", 120, True, 4)
            ConfigureColumn("total_work_hours", "Total Work Hrs", 120, True, 5)
            ConfigureColumn("pcm_name", "Company", 140, True, 6)
            ConfigureColumn("plm_name", "Location", 140, True, 7)
        End If
    End Sub

    Private Sub ConfigureColumn(fieldName As String, caption As String, width As Integer, visible As Boolean, Optional visibleIndex As Integer = -1)
        Dim col As DevExpress.XtraGrid.Columns.GridColumn = GridView1.Columns(fieldName)
        If col IsNot Nothing Then
            col.Caption = caption
            col.Width = width
            col.OptionsColumn.AllowEdit = False
            If visible AndAlso visibleIndex >= 0 Then
                col.VisibleIndex = visibleIndex
            ElseIf visible Then
                col.Visible = True
            End If
        End If
    End Sub
#End Region
#Region "SelectedReport/Lable Upate"
    Private Function GetSelectedReportType() As String
        Try
            If ListBoxControlReportType.SelectedItem IsNot Nothing Then
                Return ListBoxControlReportType.SelectedItem.ToString()
            End If
        Catch ex As Exception
            ' Handle error silently
        End Try
        Return "Summary" ' Default
    End Function
    Private Function GetSelectedReportName() As String
        Try
            If ListBoxControlReportList.SelectedItem IsNot Nothing Then
                Return ListBoxControlReportList.SelectedItem.ToString()
            End If
        Catch ex As Exception
            ' Handle error silently
        End Try
        Return "Sales by Salesman" ' Default
    End Function
    Private Function GetSelectedSalesManId() As String
        Try
            If ListBoxControlSalemanList.SelectedItem IsNot Nothing Then
                Dim selectedText As String = ListBoxControlSalemanList.SelectedItem.ToString()
                ' Extract ID from "ID - Name" format
                If selectedText.Contains(" - ") Then
                    Return selectedText.Split(New String() {" - "}, StringSplitOptions.None)(0)
                End If
            End If
        Catch ex As Exception
            ' Handle error silently
        End Try
        Return "0" ' Default (Select All)
    End Function
    Private Function GetSalesManNameById(salesManId As String) As String
        Try
            If salesManId = "0" Then
                Return "All Salesmen"
            End If

            If dtSalesManData IsNot Nothing Then
                For Each row As DataRow In dtSalesManData.Rows
                    If row("Id").ToString() = salesManId Then
                        Return row("SalesMan").ToString()
                    End If
                Next
            End If
        Catch ex As Exception
            ' Handle error silently
        End Try
        Return "Unknown"
    End Function
    Private Sub UpdateFilterDetails()
        Try
            ' Get parameters
            Dim comId As String = _companyInfo.ComId
            Dim locId As String = _companyInfo.LocId
            Dim startDate As String = dtStartDate.DateTime.ToString("yyyy-MM-dd")
            Dim endDate As String = dtEndDate.DateTime.ToString("yyyy-MM-dd")

            ' Get selected report parameters
            Dim reportType As String = GetSelectedReportType()
            Dim reportName As String = GetSelectedReportName()
            Dim selectedSalesManId As String = GetSelectedSalesManId()

            Dim salesManName As String = GetSalesManNameById(selectedSalesManId)

            lblFillterDetails.Text = String.Format("Filter Details: {0} ({1}) - {2} ({3}) | From: {4} To: {5} | Report: {6} ({7}) | SalesMan: {8}",
                                                  _companyInfo.CompanyName, comId,
                                                  _companyInfo.LocationName, locId,
                                                  DateTime.Parse(startDate).ToString("dd/MM/yyyy"),
                                                  DateTime.Parse(endDate).ToString("dd/MM/yyyy"),
                                                  reportName, reportType,
                                                  salesManName)
        Catch ex As Exception
            lblFillterDetails.Text = "Filter Details: Error updating filter information"
        End Try
    End Sub

#End Region
#Region "Load Report Data"
    Private Sub printReportDesign(reportType As String, reportName As String)
        Try
            Dim _receDs As New DataSet
            Dim copiedTable As DataTable = dtSalesData.Copy()
            copiedTable.TableName = "SalesData" ' Give it a meaningful name
            _receDs.Tables.Add(copiedTable)
            If (_receDs.Tables(0).Rows.Count > 0) Then
                _receDs.WriteXml(M_Details._appPath & "\Reports\ReportChronical.xml", Data.XmlWriteMode.WriteSchema)
            End If
            Select Case reportName.ToUpper()
                Case "SALES BY SALESMAN"
                    If reportType.ToUpper() = "SUMMARY" Then
                        If clsBillPrint.GenrateReportA4Print(_receDs, Errstr, "ReportChronicalSummary.repx") = False Then
                            lblStatusResults.Text = "Error: " & "ReportChronical.repx"
                        End If
                    Else
                        If clsBillPrint.GenrateReportA4Print(_receDs, Errstr, "ReportChronicalDetailed.repx") = False Then
                            lblStatusResults.Text = "Error: " & "ReportChronical.repx"
                        End If
                    End If
                Case "SALES BY ALL BRANCH"
                    If reportType.ToUpper() = "SUMMARY" Then
                        If clsBillPrint.GenrateReportA4Print(_receDs, Errstr, "ReportChronicalSummary.repx") = False Then
                            lblStatusResults.Text = "Error: " & "ReportChronical.repx"
                        End If
                    Else
                        If clsBillPrint.GenrateReportA4Print(_receDs, Errstr, "ReportChronicalDetailed.repx") = False Then
                            lblStatusResults.Text = "Error: " & "ReportChronical.repx"
                        End If
                    End If
            End Select

        Catch ex As Exception
            lblStatusResults.Text = "Error: " & ex.Message
            MessageBox.Show("Error loading sales data: " & ex.Message, "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub LoadSalesData()
        Try
            Cursor = Cursors.WaitCursor
            lblStatusResults.Text = "Loading data..."

            ' Get parameters
            Dim comId As String = _companyInfo.ComId
            Dim locId As String = _companyInfo.LocId
            Dim startDate As String = dtStartDate.DateTime.ToString("yyyy-MM-dd")
            Dim endDate As String = dtEndDate.DateTime.ToString("yyyy-MM-dd")

            ' Get selected report parameters
            Dim reportType As String = GetSelectedReportType()
            Dim reportName As String = GetSelectedReportName()
            Dim selectedSalesManId As String = GetSelectedSalesManId()

            ' Update filter details label


            ' Validate parameters
            If String.IsNullOrEmpty(comId) OrElse String.IsNullOrEmpty(locId) Then
                lblStatusResults.Text = "Error: Company ID and Location ID are required."
                MessageBox.Show("Company ID and Location ID are required.", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            lblStatusResults.Text = "Fetching data from server..."

            ' Load data based on selected report type
            Dim DataTable As DataTable = LoadReportData(reportType, reportName, selectedSalesManId, comId, locId, startDate, endDate)

            If DataTable IsNot Nothing AndAlso DataTable.Rows.Count > 0 Then
                dtSalesData = DataTable ' Store for summary calculations

                ' Bind data using DefaultView
                GridControl1.BeginUpdate()
                GridControl1.DataSource = DataTable.DefaultView
                GridControl1.EndUpdate()

                ' Configure grid columns based on report type
                ConfigureGridColumns(reportType)

                ' Force refresh
                GridView1.BestFitColumns()

                lblStatusResults.Text = "Data loaded successfully. Records: " & DataTable.Rows.Count.ToString()
            Else
                GridControl1.DataSource = Nothing
                dtSalesData = Nothing
                lblStatusResults.Text = "No data found for the selected criteria."
            End If

            UpdateFilterDetails()

            ' Update summary labels
            If dtSalesData IsNot Nothing Then
                lblTotalRecords.Text = "Total Records: " & dtSalesData.Rows.Count.ToString()

                ' Calculate total work hours if column exists
                If dtSalesData.Columns.Contains("total_work_hours") Then
                    Dim totalHours As Decimal = 0
                    For Each row As DataRow In dtSalesData.Rows
                        Dim hrs As Decimal = 0
                        Decimal.TryParse(row("total_work_hours").ToString(), hrs)
                        totalHours += hrs
                    Next
                    lblTotalAmount.Text = "Total Work Hours: " & totalHours.ToString("F2")
                ElseIf dtSalesData.Columns.Contains("total_days") Then
                    Dim totalDays As Integer = 0
                    For Each row As DataRow In dtSalesData.Rows
                        Dim d As Integer = 0
                        Integer.TryParse(row("total_days").ToString(), d)
                        totalDays += d
                    Next
                    lblTotalAmount.Text = "Total Days: " & totalDays.ToString()
                Else
                    lblTotalAmount.Text = ""
                End If
            End If

            ' Auto size columns for better visibility
            GridView1.BestFitColumns()

        Catch ex As Exception
            lblStatusResults.Text = "Error: " & ex.Message
            MessageBox.Show("Error loading sales data: " & ex.Message, "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Function LoadReportData(reportType As String, reportName As String, salesManId As String, comId As String, locId As String, startDate As String, endDate As String) As DataTable
        Try
            ' Determine mode from report type selection
            Dim mode As String = reportType.ToUpper() ' DATEWISE or MONTHLY
            Dim empId As Integer = 0

            ' Get selected employee ID
            If salesManId <> "0" AndAlso Not String.IsNullOrEmpty(salesManId) Then
                Integer.TryParse(salesManId, empId)
            End If

            ' Use startDate for the API call
            Return GetAttendanceReportFromAPI(mode, startDate, CInt(comId), CInt(locId), empId)

        Catch ex As Exception
            Throw New Exception("Error loading report data: " & ex.Message)
        End Try
    End Function

    Private Function GetAttendanceReportFromAPI(mode As String, dateStr As String, comId As Integer, locId As Integer, empId As Integer) As DataTable
        Dim dt As New DataTable()
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim url As String = M_Details.LinkAjaxRequest & "AttRequest=6"

            ' Build JSON payload
            Dim jsonObj As New JObject()
            jsonObj("Mode") = mode
            jsonObj("Date") = dateStr
            jsonObj("ComId") = comId
            jsonObj("LocId") = locId
            jsonObj("EmpId") = empId

            Dim jsonData As String = jsonObj.ToString()
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json;"
            request.Accept = "application/json"

            Dim jsonBytes As Byte() = Encoding.UTF8.GetBytes(jsonData)
            request.ContentLength = jsonBytes.Length

            Using requestStream As IO.Stream = request.GetRequestStream()
                requestStream.Write(jsonBytes, 0, jsonBytes.Length)
            End Using

            Dim responseText As String = ""
            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                If response.StatusCode = HttpStatusCode.OK Then
                    Using reader As New IO.StreamReader(response.GetResponseStream(), Encoding.UTF8)
                        responseText = reader.ReadToEnd()
                    End Using
                End If
            End Using

            If String.IsNullOrWhiteSpace(responseText) Then Return dt

            Dim parsedJson As JObject = JObject.Parse(responseText)
            Dim success As Boolean = If(parsedJson("Success") IsNot Nothing, parsedJson("Success").ToObject(Of Boolean)(), False)

            If success AndAlso parsedJson("Data") IsNot Nothing Then
                Dim dataArray As JArray = CType(parsedJson("Data"), JArray)
                If dataArray.Count > 0 Then
                    ' Build columns from first row
                    For Each prop In CType(dataArray(0), JObject).Properties()
                        dt.Columns.Add(prop.Name, GetType(String))
                    Next

                    ' Add rows
                    For Each item As JObject In dataArray
                        Dim row As DataRow = dt.NewRow()
                        For Each prop In item.Properties()
                            row(prop.Name) = If(prop.Value.ToString(), "")
                        Next
                        dt.Rows.Add(row)
                    Next
                End If
            End If

        Catch ex As Exception
            Throw New Exception("Error fetching attendance report: " & ex.Message)
        End Try
        Return dt
    End Function
#End Region
#Region "Button/ListBox"
    ' Event handlers for ListBox selection changes
    Private Sub ListBoxControlReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxControlReportType.SelectedIndexChanged
        ' Reload data when report type changes
        If Not Me.Disposing AndAlso Me.Visible Then
            UpdateFilterDetails()
        End If
    End Sub

    Private Sub ListBoxControlReportList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxControlReportList.SelectedIndexChanged
        ' Reload data when report selection changes
        If Not Me.Disposing AndAlso Me.Visible Then
            UpdateFilterDetails()
        End If
    End Sub

    Private Sub ListBoxControlSalemanList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxControlSalemanList.SelectedIndexChanged
        ' Reload data when salesman selection changes
        If Not Me.Disposing AndAlso Me.Visible Then
            UpdateFilterDetails()
        End If
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        lblStatusResults.Text = "Searching..."
        LoadSalesData()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        lblStatusResults.Text = "Refreshing data..."
        LoadSalesData()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            lblStatusResults.Text = "Preparing export..."
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "Excel Files|*.xlsx|CSV Files|*.csv|All Files|*.*"
            saveDialog.DefaultExt = "xlsx"
            saveDialog.FileName = "SalesReport_" & DateTime.Now.ToString("yyyyMMdd_HHmmss")

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Dim extension As String = System.IO.Path.GetExtension(saveDialog.FileName).ToLower()

                lblStatusResults.Text = "Exporting to " & extension.ToUpper() & "..."

                If extension = ".xlsx" Then
                    GridView1.ExportToXlsx(saveDialog.FileName)
                ElseIf extension = ".csv" Then
                    GridView1.ExportToCsv(saveDialog.FileName)
                End If

                lblStatusResults.Text = "Export completed successfully to " & saveDialog.FileName
                MessageBox.Show("Report exported successfully!", "Export Complete",
                               MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                lblStatusResults.Text = "Export cancelled by user."
            End If

        Catch ex As Exception
            lblStatusResults.Text = "Error: Export failed - " & ex.Message
            MessageBox.Show("Error exporting report: " & ex.Message, "Export Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dtStartDate_EditValueChanged(sender As Object, e As EventArgs) Handles dtStartDate.EditValueChanged
        ' Auto-update end date if it's before start date
        If dtEndDate.DateTime < dtStartDate.DateTime Then
            dtEndDate.DateTime = dtStartDate.DateTime
        End If
    End Sub


    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrintPreview.Click

    End Sub
#End Region
#Region "PrintViewReport"

    Private Sub btnprintreport_Click(sender As Object, e As EventArgs) Handles btnprintreport.Click
        Try
            Dim reportType As String = GetSelectedReportType()
            Dim reportName As String = GetSelectedReportName()
            printReportDesign(reportType, reportName)
        Catch ex As Exception

        End Try
    End Sub

#End Region


End Class
