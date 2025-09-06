Imports System.Data
Imports System.Net
Imports System.Text
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.BrickAlignment
Imports DevExpress.XtraPrinting.PageHeaderFooter

Public Class frmMonthlySummaryReport
    Private dtSalesmanData As DataTable
    Private dtItemwiseData As DataTable
    Private dtAdvanceData As DataTable
    Private dtCurrentData As DataTable
    Dim Errstr As String
    Dim dsReport As DataSet
#Region "Form Load & Initialization"
    Private Sub frmMonthlySummaryReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Initialize form
            InitializeForm()

            ' Set default date to current month
            dtMonthYear.DateTime = DateTime.Today

            ' Initialize status
            lblStatus.Text = "Ready to load monthly summary data..."
            lblFilterInfo.Text = "Filter: No data loaded"

            ' Initialize report type options
            InitializeReportTypes()

            ' Load initial data
            LoadMonthlySummaryData()

        Catch ex As Exception
            MessageBox.Show("Error initializing form: " & ex.Message, "Initialization Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InitializeForm()
        Try
            ' Set form title
            Me.Text = "Monthly Summary Report - " & DateTime.Now.ToString("MMM yyyy")

            ' Initialize data tables
            dtSalesmanData = New DataTable()
            dtItemwiseData = New DataTable()
            dtAdvanceData = New DataTable()

        Catch ex As Exception
            Throw New Exception("Error in form initialization: " & ex.Message)
        End Try
    End Sub

    Private Sub InitializeReportTypes()
        Try
            ' Clear existing items
            ListBoxControlReportType.Items.Clear()

            ' Add report type options based on stored procedure results
            ListBoxControlReportType.Items.Add("Sales by Salesman")
            ListBoxControlReportType.Items.Add("Itemwise Sales & Commission")
            ListBoxControlReportType.Items.Add("Advance Payments")

            ' Set default selection
            If ListBoxControlReportType.Items.Count > 0 Then
                ListBoxControlReportType.SelectedIndex = 0
            End If

        Catch ex As Exception
            MessageBox.Show("Error initializing report types: " & ex.Message, "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
#End Region

#Region "Grid Column Configuration"
    Private Sub ConfigureGridColumns(reportType As String)
        Try
            ' Clear existing columns
            GridView1.Columns.Clear()

            Select Case reportType.ToUpper()
                Case "SALES BY SALESMAN"
                    ConfigureSalesmanColumns()
                Case "ITEMWISE SALES & COMMISSION"
                    ConfigureItemwiseColumns()
                Case "ADVANCE PAYMENTS"
                    ConfigureAdvanceColumns()
            End Select

            ' Auto size columns
            GridView1.BestFitColumns()

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error configuring grid columns: " & ex.Message)
        End Try
    End Sub

    Private Sub ConfigureSalesmanColumns()
        ' Configure columns for Sales by Salesman report

        ' Month-Year Column
        Dim colMonYear As New DevExpress.XtraGrid.Columns.GridColumn()
        colMonYear.FieldName = "MonYear"
        colMonYear.Caption = "Month-Year"
        colMonYear.Visible = True
        colMonYear.Width = 100
        GridView1.Columns.Add(colMonYear)

        ' Salesman Name Column
        Dim colSalesman As New DevExpress.XtraGrid.Columns.GridColumn()
        colSalesman.FieldName = "Salesman"
        colSalesman.Caption = "Salesman"
        colSalesman.Visible = True
        colSalesman.Width = 180
        GridView1.Columns.Add(colSalesman)

        ' Company Name Column
        Dim colCompany As New DevExpress.XtraGrid.Columns.GridColumn()
        colCompany.FieldName = "CompanyName"
        colCompany.Caption = "Company"
        colCompany.Visible = True
        colCompany.Width = 150
        GridView1.Columns.Add(colCompany)

        ' Location Name Column
        Dim colLocation As New DevExpress.XtraGrid.Columns.GridColumn()
        colLocation.FieldName = "LocationName"
        colLocation.Caption = "Location"
        colLocation.Visible = True
        colLocation.Width = 150
        GridView1.Columns.Add(colLocation)

        ' Net Amount Column
        Dim colNetAmt As New DevExpress.XtraGrid.Columns.GridColumn()
        colNetAmt.FieldName = "NetAmt"
        colNetAmt.Caption = "Net Amount"
        colNetAmt.Visible = True
        colNetAmt.Width = 120
        colNetAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colNetAmt.DisplayFormat.FormatString = "n2"
        colNetAmt.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colNetAmt.SummaryItem.DisplayFormat = "Total: {0:n2}"
        GridView1.Columns.Add(colNetAmt)

        ' Commission Column
        Dim colCommission As New DevExpress.XtraGrid.Columns.GridColumn()
        colCommission.FieldName = "Commission"
        colCommission.Caption = "Commission"
        colCommission.Visible = True
        colCommission.Width = 120
        colCommission.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colCommission.DisplayFormat.FormatString = "n2"
        colCommission.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colCommission.SummaryItem.DisplayFormat = "Total: {0:n2}"
        GridView1.Columns.Add(colCommission)

        ' TotalQty Column
        Dim colQty As New DevExpress.XtraGrid.Columns.GridColumn()
        colQty.FieldName = "Qty"
        colQty.Caption = "Total Qty"
        colQty.Visible = True
        colQty.Width = 100
        colQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colQty.DisplayFormat.FormatString = "n2"
        colQty.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colQty.SummaryItem.DisplayFormat = "Total: {0:n2}"
        GridView1.Columns.Add(colQty)

    End Sub

    Private Sub ConfigureItemwiseColumns()
        ' Configure columns for Itemwise Sales & Commission report

        ' Month-Year Column
        Dim colMonYear As New DevExpress.XtraGrid.Columns.GridColumn()
        colMonYear.FieldName = "MonYear"
        colMonYear.Caption = "Month-Year"
        colMonYear.Visible = True
        colMonYear.Width = 100
        GridView1.Columns.Add(colMonYear)

        ' Salesman Name Column
        Dim colSalesman As New DevExpress.XtraGrid.Columns.GridColumn()
        colSalesman.FieldName = "Salesman"
        colSalesman.Caption = "Salesman"
        colSalesman.Visible = True
        colSalesman.Width = 150
        GridView1.Columns.Add(colSalesman)

        ' Item Name Column
        Dim colItemName As New DevExpress.XtraGrid.Columns.GridColumn()
        colItemName.FieldName = "ItemName"
        colItemName.Caption = "Item Name"
        colItemName.Visible = True
        colItemName.Width = 200
        GridView1.Columns.Add(colItemName)

        ' Company Name Column
        Dim colCompany As New DevExpress.XtraGrid.Columns.GridColumn()
        colCompany.FieldName = "CompanyName"
        colCompany.Caption = "Company"
        colCompany.Visible = True
        colCompany.Width = 140
        GridView1.Columns.Add(colCompany)

        ' Location Name Column
        Dim colLocation As New DevExpress.XtraGrid.Columns.GridColumn()
        colLocation.FieldName = "LocationName"
        colLocation.Caption = "Location"
        colLocation.Visible = True
        colLocation.Width = 140
        GridView1.Columns.Add(colLocation)

        ' Net Amount Column
        Dim colNetAmt As New DevExpress.XtraGrid.Columns.GridColumn()
        colNetAmt.FieldName = "NetAmt"
        colNetAmt.Caption = "Net Amount"
        colNetAmt.Visible = True
        colNetAmt.Width = 120
        colNetAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colNetAmt.DisplayFormat.FormatString = "n2"
        colNetAmt.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colNetAmt.SummaryItem.DisplayFormat = "Total: {0:n2}"
        GridView1.Columns.Add(colNetAmt)

        ' Commission Column
        Dim colCommission As New DevExpress.XtraGrid.Columns.GridColumn()
        colCommission.FieldName = "Commission"
        colCommission.Caption = "Commission"
        colCommission.Visible = True
        colCommission.Width = 120
        colCommission.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colCommission.DisplayFormat.FormatString = "n2"
        colCommission.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colCommission.SummaryItem.DisplayFormat = "Total: {0:n2}"
        GridView1.Columns.Add(colCommission)

        ' TotalQty Column
        Dim colQty As New DevExpress.XtraGrid.Columns.GridColumn()
        colQty.FieldName = "Qty"
        colQty.Caption = "Total Qty"
        colQty.Visible = True
        colQty.Width = 100
        colQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colQty.DisplayFormat.FormatString = "n2"
        colQty.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colQty.SummaryItem.DisplayFormat = "Total: {0:n2}"
        GridView1.Columns.Add(colQty)
    End Sub

    Private Sub ConfigureAdvanceColumns()
        ' Configure columns for Advance Payments report

        ' Month-Year Column
        Dim colMonYear As New DevExpress.XtraGrid.Columns.GridColumn()
        colMonYear.FieldName = "MonYear"
        colMonYear.Caption = "Month-Year"
        colMonYear.Visible = True
        colMonYear.Width = 100
        GridView1.Columns.Add(colMonYear)

        ' Employee ID Column
        Dim colEmpID As New DevExpress.XtraGrid.Columns.GridColumn()
        colEmpID.FieldName = "EmpID"
        colEmpID.Caption = "Emp ID"
        colEmpID.Visible = True
        colEmpID.Width = 80
        GridView1.Columns.Add(colEmpID)

        ' Salesman Name Column
        Dim colSalesman As New DevExpress.XtraGrid.Columns.GridColumn()
        colSalesman.FieldName = "Salesman"
        colSalesman.Caption = "Salesman"
        colSalesman.Visible = True
        colSalesman.Width = 180
        GridView1.Columns.Add(colSalesman)

        ' Company Name Column
        Dim colCompany As New DevExpress.XtraGrid.Columns.GridColumn()
        colCompany.FieldName = "CompanyName"
        colCompany.Caption = "Company"
        colCompany.Visible = True
        colCompany.Width = 150
        GridView1.Columns.Add(colCompany)

        ' Location Name Column
        Dim colLocation As New DevExpress.XtraGrid.Columns.GridColumn()
        colLocation.FieldName = "LocationName"
        colLocation.Caption = "Location"
        colLocation.Visible = True
        colLocation.Width = 150
        GridView1.Columns.Add(colLocation)

        ' Total Advance Column
        Dim colTotalAdvance As New DevExpress.XtraGrid.Columns.GridColumn()
        colTotalAdvance.FieldName = "TotalAdvance"
        colTotalAdvance.Caption = "Total Advance"
        colTotalAdvance.Visible = True
        colTotalAdvance.Width = 130
        colTotalAdvance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colTotalAdvance.DisplayFormat.FormatString = "n2"
        colTotalAdvance.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colTotalAdvance.SummaryItem.DisplayFormat = "Total: {0:n2}"
        GridView1.Columns.Add(colTotalAdvance)
    End Sub
    Private Sub RearrangeSalesManSummaryColumns()
        Try
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In GridView1.Columns
                If col.FieldName = "TotalAdvance" OrElse col.FieldName = "Commission" OrElse col.FieldName = "NetAmt" OrElse col.FieldName = "Qty" Then
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Data Loading"
    Private Sub LoadMonthlySummaryData()
        Try
            ' Show loading cursor
            Me.Cursor = Cursors.WaitCursor
            lblStatus.Text = "Loading monthly summary data..."

            ' Get selected month and year
            Dim selectedDate As DateTime = dtMonthYear.DateTime
            Dim selectedMonth As Integer = selectedDate.Month
            Dim selectedYear As Integer = selectedDate.Year

            ' Get selected report type
            Dim reportType As String = GetSelectedReportType()

            ' Configure grid columns
            ConfigureGridColumns(reportType)

            ' Load data from stored procedure
            Dim resultData As DataTable = GetMonthlySummaryFromAPI(selectedYear, selectedMonth, reportType)

            If resultData IsNot Nothing AndAlso resultData.Rows.Count > 0 Then
                ' Store current data and bind to grid
                dtCurrentData = resultData
                GridControl1.DataSource = dtCurrentData
                RearrangeSalesManSummaryColumns()
                ' Update status
                lblStatus.Text = String.Format("Loaded {0} records for {1}", dtCurrentData.Rows.Count, selectedDate.ToString("MMM yyyy"))
                UpdateSummaryLabels()
            Else
                ' No data found
                GridControl1.DataSource = Nothing
                dtCurrentData = Nothing
                lblStatus.Text = String.Format("No data found for {0}", selectedDate.ToString("MMM yyyy"))
                lblTotalAmount.Text = "Total Amount: 0.00"
            End If

            ' Update filter information
            UpdateFilterInfo()

        Catch ex As Exception
            MessageBox.Show("Error loading monthly summary data: " & ex.Message, "Data Load Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
            lblStatus.Text = "Error loading data: " & ex.Message
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function GetMonthlySummaryFromAPI(year As Integer, month As Integer, reportType As String) As DataTable
        Try
            ' Build API URL for monthly summary stored procedure call
            Dim apiUrl As String = M_Details.LinkAjaxRequestSyncLocalCloud & "AjaxRequest=10"

            ' Build post data for API call - now gets all result sets at once
            Dim postData As String = String.Format("AjaxRequest=10&year={0}&month={1}", year, month)

            Using client As New WebClient()
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim jsonResponse As String = client.UploadString(apiUrl, postData)

                If Not String.IsNullOrEmpty(jsonResponse) Then

                    Dim Userparsejson As JObject = JObject.Parse(jsonResponse)

                    If Userparsejson("Success").ToString().ToLower() = "true" Then
                        If Userparsejson("Data") IsNot Nothing Then
                            ' Parse the consolidated response with all three result sets
                            Dim dataObj As JObject = CType(Userparsejson("Data"), JObject)

                            ' Store all three datasets
                            If dataObj("SalesmanData") IsNot Nothing Then
                                dtSalesmanData = JsonArrayToDataTable(CType(dataObj("SalesmanData"), JArray))
                            End If

                            If dataObj("ItemwiseData") IsNot Nothing Then
                                dtItemwiseData = JsonArrayToDataTable(CType(dataObj("ItemwiseData"), JArray))
                            End If

                            If dataObj("AdvanceData") IsNot Nothing Then
                                dtAdvanceData = JsonArrayToDataTable(CType(dataObj("AdvanceData"), JArray))
                            End If
                            ' Create a new DataSet
                            dsReport = New DataSet("ReportData")

                            ' Add SalesmanData if it exists
                            If dtSalesmanData IsNot Nothing Then
                                Dim copiedSalesman As DataTable = dtSalesmanData.Copy()
                                copiedSalesman.TableName = "SalesmanData"
                                dsReport.Tables.Add(copiedSalesman)
                            End If

                            ' Add ItemwiseData if it exists
                            If dtItemwiseData IsNot Nothing Then
                                Dim copiedItemwise As DataTable = dtItemwiseData.Copy()
                                copiedItemwise.TableName = "ItemwiseData"
                                dsReport.Tables.Add(copiedItemwise)
                            End If

                            ' Add AdvanceData if it exists
                            If dtAdvanceData IsNot Nothing Then
                                Dim copiedAdvance As DataTable = dtAdvanceData.Copy()
                                copiedAdvance.TableName = "AdvanceData"
                                dsReport.Tables.Add(copiedAdvance)
                            End If
                            If (dsReport.Tables(0).Rows.Count > 0) Then
                                dsReport.WriteXml(M_Details._appPath & "\Reports\ReportMonthlyIndv.xml", Data.XmlWriteMode.WriteSchema)
                            End If
                            ' Return the appropriate dataset based on report type
                            Select Case reportType.ToUpper()
                                Case "SALES BY SALESMAN"
                                    Return If(dtSalesmanData, New DataTable())
                                Case "ITEMWISE SALES & COMMISSION"
                                    Return If(dtItemwiseData, New DataTable())
                                Case "ADVANCE PAYMENTS"
                                    Return If(dtAdvanceData, New DataTable())
                                Case Else
                                    Return If(dtSalesmanData, New DataTable())
                            End Select
                        End If
                    Else
                        ' Show error message from API
                        Dim errorMsg As String = If(Userparsejson("Msg") IsNot Nothing, Userparsejson("Msg").ToString(), "Unknown API error")
                        Throw New Exception(String.Format("API Error: {0}", errorMsg))
                    End If
                End If

                Return New DataTable()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("Error fetching monthly summary data: {0}", ex.Message))
        End Try
    End Function
#End Region

#Region "Helper Functions"
    Private Function GetSelectedReportType() As String
        Try
            If ListBoxControlReportType.SelectedItem IsNot Nothing Then
                Return ListBoxControlReportType.SelectedItem.ToString()
            End If
        Catch ex As Exception
            ' Handle error silently
        End Try
        Return "Sales by Salesman" ' Default
    End Function

    Private Sub UpdateFilterInfo()
        Try
            Dim selectedDate As DateTime = dtMonthYear.DateTime
            Dim reportType As String = GetSelectedReportType()

            lblFilterInfo.Text = String.Format("Filter: {0} | Period: {1:MMM yyyy} | Company: {2}", reportType, selectedDate, _companyInfo.CompanyName)
        Catch ex As Exception
            lblFilterInfo.Text = "Filter: Error updating filter information"
        End Try
    End Sub

    Private Sub UpdateSummaryLabels()
        Try
            If dtCurrentData IsNot Nothing AndAlso dtCurrentData.Rows.Count > 0 Then
                Dim totalRecords As Integer = dtCurrentData.Rows.Count
                Dim totalAmount As Decimal = 0

                ' Determine amount column based on report type
                Dim amountColumn As String = ""
                Dim reportType As String = GetSelectedReportType()

                Select Case reportType.ToUpper()
                    Case "SALES BY SALESMAN", "ITEMWISE SALES & COMMISSION"
                        amountColumn = "NetAmt"
                    Case "ADVANCE PAYMENTS"
                        amountColumn = "TotalAdvance"
                End Select

                ' Calculate total amount
                If Not String.IsNullOrEmpty(amountColumn) AndAlso dtCurrentData.Columns.Contains(amountColumn) Then
                    For Each row As DataRow In dtCurrentData.Rows
                        If Not IsDBNull(row(amountColumn)) Then
                            totalAmount += Convert.ToDecimal(row(amountColumn))
                        End If
                    Next
                End If

                ' Update labels
                lblStatus.Text = String.Format("Records: {0:n0} | Total: {1:c2}", totalRecords, totalAmount)
                lblTotalAmount.Text = String.Format("Total Amount: {0:n2}", totalAmount)

            Else
                lblStatus.Text = "No records found"
                lblTotalAmount.Text = "Total Amount: 0.00"
            End If

        Catch ex As Exception
            lblTotalAmount.Text = "Total Amount: Error"
        End Try
    End Sub

    Private Function JsonArrayToDataTable(jsonArray As JArray) As DataTable
        Try
            Dim dt As New DataTable()

            If jsonArray.Count > 0 Then
                ' Create columns from first object
                Dim firstItem As JObject = CType(jsonArray(0), JObject)
                For Each prop As JProperty In firstItem.Properties()
                    dt.Columns.Add(prop.Name, GetType(String))
                Next

                ' Add rows
                For Each item As JObject In jsonArray
                    Dim row As DataRow = dt.NewRow()
                    For Each prop As JProperty In item.Properties()
                        If dt.Columns.Contains(prop.Name) Then
                            row(prop.Name) = If(prop.Value IsNot Nothing, prop.Value.ToString(), "")
                        End If
                    Next
                    dt.Rows.Add(row)
                Next
            End If

            Return dt

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error converting JSON to DataTable: " & ex.Message)
            Return Nothing
        End Try
    End Function

    Private Function CleanJsonString(jsonString As String) As String
        Try
            If String.IsNullOrEmpty(jsonString) Then
                Return jsonString
            End If

            ' Remove BOM and control characters
            Dim cleaned As String = jsonString.Trim()

            ' Find JSON start and end
            Dim jsonStart As Integer = Math.Max(cleaned.IndexOf("{"c), cleaned.IndexOf("["c))
            If jsonStart > 0 Then
                cleaned = cleaned.Substring(jsonStart)
            End If

            Dim jsonEnd As Integer = Math.Max(cleaned.LastIndexOf("}"c), cleaned.LastIndexOf("]"c))
            If jsonEnd > 0 AndAlso jsonEnd < cleaned.Length - 1 Then
                cleaned = cleaned.Substring(0, jsonEnd + 1)
            End If

            Return cleaned

        Catch ex As Exception
            Return jsonString
        End Try
    End Function
#End Region

#Region "Event Handlers"
    Private Sub dtMonthYear_EditValueChanged(sender As Object, e As EventArgs) Handles dtMonthYear.EditValueChanged
        ' Reload data when month/year changes
        If Not Me.Disposing AndAlso Me.Visible Then
            'LoadMonthlySummaryData()
        End If
    End Sub

    Private Sub ListBoxControlReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxControlReportType.SelectedIndexChanged
        ' Reload data when report type changes
        If Not Me.Disposing AndAlso Me.Visible Then
            ' LoadMonthlySummaryData()
        End If
    End Sub


    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
        Try
            If dtCurrentData IsNot Nothing AndAlso dtCurrentData.Rows.Count > 0 Then
                ' Print preview
                GridControl1.ShowPrintPreview()
                lblStatus.Text = "Print preview opened"
            Else
                MessageBox.Show("No data available to print", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error printing data: " & ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region
#Region "PrintViewReport"

    Private Sub CreatePrintableComponentLinkPreview()
        Try
            Dim selectedDate As DateTime = dtMonthYear.DateTime
            Dim selectedMonth As Integer = selectedDate.Month
            Dim selectedYear As Integer = selectedDate.Year
            ' Create PrintableComponentLink for advanced printing
            Dim ps As New PrintingSystem
            Dim printableComponentLink As New PrintableComponentLink(ps)

            ' Set the component to print
            printableComponentLink.Component = GridControl1

            ' Configure the page header and footer
            Dim phf As PageHeaderFooter = CType(printableComponentLink.PageHeaderFooter, PageHeaderFooter)

            ' Clear the existing content before adding new strings
            phf.Header.Content.Clear()
            phf.Footer.Content.Clear()

            ' Setup Header
            Dim headerLeft As String = "Report"
            Dim headerCenter As String = vbCrLf & M_Details._shopName & vbCrLf & "SALES DETAILS BY SALESMAN REPORT"
            Dim headerRight As String = "Page [Page # of Pages #]"
            phf.Header.Content.AddRange(New String() {headerLeft, headerCenter, headerRight})
            phf.Header.LineAlignment = BrickAlignment.Near ' Can be changed as needed
            phf.Header.Font = New Font("Arial", 10, FontStyle.Bold)

            ' Setup Footer with detailed information
            Dim footerLeft As String = "Period: " & selectedMonth & " - " & selectedYear
            Dim footerCenter As String = "Generated: [Date Printed] by " & _companyInfo.UserName
            Dim footerRight As String = "" ' GetSummaryInfo()
            phf.Footer.Content.AddRange(New String() {footerLeft, footerCenter, footerRight})
            phf.Footer.LineAlignment = BrickAlignment.Far ' Can be changed as needed
            phf.Footer.Font = New Font("Arial", 8, FontStyle.Regular)

            ' Create the document and show preview
            printableComponentLink.CreateDocument(ps)
            printableComponentLink.ShowPreviewDialog()

        Catch ex As Exception
            ' Fallback to basic print if PrintableComponentLink fails
            MessageBox.Show("Advanced printing not available. Using basic print preview." & vbCrLf & "Error: " & ex.Message,
                          "Print Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Try
    End Sub
    'Private Function GetSummaryInfo() As String
    '    Try
    '        If dsReport IsNot Nothing AndAlso dsReport.Tables(0).Rows.Count > 0 Then
    '            Dim totalAmount As Decimal = 0
    '            Dim amountColumnName As String = "psid_invoice_netamt"
    '            ' Get selected report info to determine which column to sum
    '            Dim reportType As String = GetSelectedReportType()
    '            Dim reportName As String = GetSelectedReportName()


    '            Select Case reportName.ToUpper()
    '                Case "SALES BY SALESMAN", "SALES BY ALL BRANCH"
    '                    If reportType.ToUpper() = "SUMMARY" Then
    '                        amountColumnName = "TotalNetAmt" ' For summary report
    '                    Else
    '                        amountColumnName = "NetAmt" ' For detailed report
    '                    End If
    '                Case Else
    '                    amountColumnName = "psid_invoice_netamt" ' Default for other reports
    '            End Select

    '            ' Ensure the column exists before trying to access it
    '            If dsReport.Columns.Contains(amountColumnName) Then
    '                For Each row As DataRow In dtSalesData.Rows
    '                    If Not IsDBNull(row(amountColumnName)) Then
    '                        totalAmount += Convert.ToDecimal(row(amountColumnName))
    '                    End If
    '                Next
    '                Return String.Format("Records: {0:n0} | Total: {1:c}", dtSalesData.Rows.Count, totalAmount)
    '            Else
    '                Return "Summary unavailable (Column missing)"
    '            End If
    '        Else
    '            Return "No data available"
    '        End If
    '    Catch ex As Exception
    '        Return "Summary unavailable (Error)"
    '    End Try
    'End Function
    Private Sub btnprintreport_Click(sender As Object, e As EventArgs) Handles btnprintreport.Click
        Try
            printReportDesign()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub printReportDesign()
        Try
            If dsReport.Tables(0).Rows.Count > 0 Then
                If clsBillPrint.GenrateMonthlyReportA4Print(dsReport, Errstr, "ReportMonthlyIndv.repx") = False Then
                    lblStatus.Text = "Error: " & "ReportMonthlyIndv.repx"
                End If
            End If

        Catch ex As Exception
            lblStatus.Text = "Error: " & ex.Message
            MessageBox.Show("Error loading sales data: " & ex.Message, "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles btnRefreshData.Click
        Try
            lblStatus.Text = "Refreshing data..."
            LoadMonthlySummaryData()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            lblStatus.Text = "Search data..."
            LoadMonthlySummaryData()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnPrintPreview_Click(sender As Object, e As EventArgs) Handles btnPrintPreview.Click
        Try
            If dtCurrentData IsNot Nothing AndAlso dtCurrentData.Rows.Count > 0 Then
                ' Export grid to Excel
                CreatePrintableComponentLinkPreview()
                lblStatus.Text = "Data exported successfully"
            Else
                MessageBox.Show("No data available to export", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error exporting data: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
