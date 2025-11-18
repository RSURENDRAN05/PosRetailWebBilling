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

Public Class frmAdvancePaymentReport
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
        ListBoxControlReportType.Items.Add("Summary")
        ListBoxControlReportType.Items.Add("Detailed")

        ' Set default selection
        If ListBoxControlReportType.Items.Count > 0 Then
            ListBoxControlReportType.SelectedIndex = 0 ' Select "Summary" by default
        End If
    End Sub

    Private Sub InitializeReportList()
        ' Clear existing items
        ListBoxControlReportList.Items.Clear()

        ' Add Advance Payment Report options
        ListBoxControlReportList.Items.Add("Advance Payment by Employee")
        ListBoxControlReportList.Items.Add("Advance Payment by All Branches")

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
            LoadSalesManData()
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
            GetSalesmanData()
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
    Private Sub ConfigureGridColumns(reportType As String, reportName As String)
        Try
            ' Clear existing columns
            GridView1.Columns.Clear()

            ' Configure columns based on report type for Advance Payment Report
            If reportType.ToUpper() = "SUMMARY" Then
                ConfigureAdvancePaymentSummaryColumns()
            Else
                ConfigureAdvancePaymentDetailedColumns()
            End If

            ' Auto size all columns
            GridView1.BestFitColumns()

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error configuring grid columns: " & ex.Message)
        End Try
    End Sub

    Private Sub ConfigureAdvancePaymentSummaryColumns()
        ' Configure columns for Advance Payment Summary Report
        ' Based on PHP query: ID, Name, CompanyName, LocationName, TotalTransactions, TotalAmount, FirstAdvance, LastAdvance

        ' Employee ID Column
        Dim colID As New DevExpress.XtraGrid.Columns.GridColumn()
        colID.FieldName = "ID"
        colID.Caption = "Employee ID"
        colID.Visible = True
        colID.Width = 100
        colID.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colID.DisplayFormat.FormatString = "d"
        GridView1.Columns.Add(colID)

        ' Employee Name Column
        Dim colName As New DevExpress.XtraGrid.Columns.GridColumn()
        colName.FieldName = "Name"
        colName.Caption = "Employee Name"
        colName.Visible = True
        colName.Width = 180
        GridView1.Columns.Add(colName)

        ' Company Name Column
        Dim colCompanyName As New DevExpress.XtraGrid.Columns.GridColumn()
        colCompanyName.FieldName = "CompanyName"
        colCompanyName.Caption = "Company"
        colCompanyName.Visible = True
        colCompanyName.Width = 150
        GridView1.Columns.Add(colCompanyName)

        ' Location Name Column
        Dim colLocationName As New DevExpress.XtraGrid.Columns.GridColumn()
        colLocationName.FieldName = "LocationName"
        colLocationName.Caption = "Location"
        colLocationName.Visible = True
        colLocationName.Width = 150
        GridView1.Columns.Add(colLocationName)

        ' Total Transactions Column
        Dim colTotalTransactions As New DevExpress.XtraGrid.Columns.GridColumn()
        colTotalTransactions.FieldName = "TotalTransactions"
        colTotalTransactions.Caption = "Total Advances"
        colTotalTransactions.Visible = True
        colTotalTransactions.Width = 120
        colTotalTransactions.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colTotalTransactions.DisplayFormat.FormatString = "n0"
        colTotalTransactions.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colTotalTransactions.SummaryItem.DisplayFormat = "Total: {0:n0}"
        GridView1.Columns.Add(colTotalTransactions)

        ' Total Amount Column
        Dim colTotalAmount As New DevExpress.XtraGrid.Columns.GridColumn()
        colTotalAmount.FieldName = "TotalAmount"
        colTotalAmount.Caption = "Total Amount"
        colTotalAmount.Visible = True
        colTotalAmount.Width = 130
        colTotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colTotalAmount.DisplayFormat.FormatString = "n2"
        colTotalAmount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colTotalAmount.SummaryItem.DisplayFormat = "Total: {0:n2}"
        GridView1.Columns.Add(colTotalAmount)

        ' First Advance Date Column
        Dim colFirstAdvance As New DevExpress.XtraGrid.Columns.GridColumn()
        colFirstAdvance.FieldName = "FirstAdvance"
        colFirstAdvance.Caption = "First Advance"
        colFirstAdvance.Visible = True
        colFirstAdvance.Width = 120
        colFirstAdvance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        colFirstAdvance.DisplayFormat.FormatString = "dd/MM/yyyy"
        GridView1.Columns.Add(colFirstAdvance)

        ' Last Advance Date Column
        Dim colLastAdvance As New DevExpress.XtraGrid.Columns.GridColumn()
        colLastAdvance.FieldName = "LastAdvance"
        colLastAdvance.Caption = "Last Advance"
        colLastAdvance.Visible = True
        colLastAdvance.Width = 120
        colLastAdvance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        colLastAdvance.DisplayFormat.FormatString = "dd/MM/yyyy"
        GridView1.Columns.Add(colLastAdvance)
    End Sub

    Private Sub ConfigureAdvancePaymentDetailedColumns()
        ' Configure columns for Advance Payment Detailed Report
        ' Based on PHP query: ID, EmployeeID, Name, Amount, Remarks, DateTime, ShiftNo, DayNo, CompanyName, LocationName, PmId, ComId, LocId

        ' Advance ID Column
        Dim colID As New DevExpress.XtraGrid.Columns.GridColumn()
        colID.FieldName = "ID"
        colID.Caption = "Advance ID"
        colID.Visible = True
        colID.Width = 100
        GridView1.Columns.Add(colID)

        ' Employee ID Column
        Dim colEmployeeID As New DevExpress.XtraGrid.Columns.GridColumn()
        colEmployeeID.FieldName = "EmployeeID"
        colEmployeeID.Caption = "Employee ID"
        colEmployeeID.Visible = True
        colEmployeeID.Width = 100
        GridView1.Columns.Add(colEmployeeID)

        ' Employee Name Column
        Dim colName As New DevExpress.XtraGrid.Columns.GridColumn()
        colName.FieldName = "Name"
        colName.Caption = "Employee Name"
        colName.Visible = True
        colName.Width = 180
        GridView1.Columns.Add(colName)

        ' Amount Column
        Dim colAmount As New DevExpress.XtraGrid.Columns.GridColumn()
        colAmount.FieldName = "Amount"
        colAmount.Caption = "Amount"
        colAmount.Visible = True
        colAmount.Width = 120
        colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colAmount.DisplayFormat.FormatString = "n2"
        colAmount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colAmount.SummaryItem.DisplayFormat = "Total: {0:n2}"
        GridView1.Columns.Add(colAmount)

        ' Remarks Column
        Dim colRemarks As New DevExpress.XtraGrid.Columns.GridColumn()
        colRemarks.FieldName = "Remarks"
        colRemarks.Caption = "Remarks"
        colRemarks.Visible = True
        colRemarks.Width = 200
        GridView1.Columns.Add(colRemarks)

        ' Date Time Column
        Dim colDateTime As New DevExpress.XtraGrid.Columns.GridColumn()
        colDateTime.FieldName = "DateTime"
        colDateTime.Caption = "Date & Time"
        colDateTime.Visible = True
        colDateTime.Width = 140
        colDateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        colDateTime.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        GridView1.Columns.Add(colDateTime)

        ' Shift Number Column
        Dim colShiftNo As New DevExpress.XtraGrid.Columns.GridColumn()
        colShiftNo.FieldName = "ShiftNo"
        colShiftNo.Caption = "Shift"
        colShiftNo.Visible = True
        colShiftNo.Width = 80
        GridView1.Columns.Add(colShiftNo)

        ' Day Number Column
        Dim colDayNo As New DevExpress.XtraGrid.Columns.GridColumn()
        colDayNo.FieldName = "DayNo"
        colDayNo.Caption = "Day"
        colDayNo.Visible = True
        colDayNo.Width = 80
        GridView1.Columns.Add(colDayNo)

        ' Company Name Column
        Dim colCompanyName As New DevExpress.XtraGrid.Columns.GridColumn()
        colCompanyName.FieldName = "CompanyName"
        colCompanyName.Caption = "Company"
        colCompanyName.Visible = True
        colCompanyName.Width = 150
        GridView1.Columns.Add(colCompanyName)

        ' Location Name Column
        Dim colLocationName As New DevExpress.XtraGrid.Columns.GridColumn()
        colLocationName.FieldName = "LocationName"
        colLocationName.Caption = "Location"
        colLocationName.Visible = True
        colLocationName.Width = 150
        GridView1.Columns.Add(colLocationName)
    End Sub
    Private Sub RearrangeSalesManSummaryColumns()
        Try
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In GridView1.Columns
                If col.FieldName = "ID" OrElse col.FieldName = "EmployeeID" OrElse col.FieldName = "Remarks" _
                 OrElse col.FieldName = "DateTime" OrElse col.FieldName = "TotalTransactions" OrElse col.FieldName = "CompanyName" _
                  OrElse col.FieldName = "LocationName" OrElse col.FieldName = "FirstAdvance" OrElse col.FieldName = "LastAdvance" _
                  OrElse col.FieldName = "ShiftNo" OrElse col.FieldName = "DayNo" Then
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center
                End If
                If col.FieldName = "TotalAmount" OrElse col.FieldName = "Amount" Then
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far
                End If
            Next
        Catch ex As Exception

        End Try
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
        Return "Advance Payment by Employee" ' Default
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
    Private Sub UpdateSummaryLabels()
        Try
            If dtSalesData IsNot Nothing AndAlso dtSalesData.Rows.Count > 0 Then
                Dim totalRecords As Integer = dtSalesData.Rows.Count
                Dim totalAmount As Decimal = 0

                ' Get selected report info to determine which column to sum
                Dim reportType As String = GetSelectedReportType()

                ' Determine the amount column based on report type for Advance Payment Report
                Dim amountColumnName As String = ""

                If reportType.ToUpper() = "SUMMARY" Then
                    amountColumnName = "TotalAmount" ' For summary report
                Else
                    amountColumnName = "Amount" ' For detailed report
                End If

                ' Calculate total amount using the appropriate column
                If dtSalesData.Columns.Contains(amountColumnName) Then
                    For Each row As DataRow In dtSalesData.Rows
                        If Not IsDBNull(row(amountColumnName)) Then
                            totalAmount += Convert.ToDecimal(row(amountColumnName))
                        End If
                    Next
                End If

                ' Update labels
                lblTotalRecords.Text = "Total Records: " & totalRecords.ToString("n0")
                lblTotalAmount.Text = "Total Amount: " & totalAmount.ToString("n2")
                lblStatusResults.Text = String.Format("Records: {0:n0} | Total: {1:c2}", totalRecords, totalAmount)
            Else
                ' No data case
                lblTotalRecords.Text = "Total Records: 0"
                lblTotalAmount.Text = "Total Amount: 0.00"
                lblStatusResults.Text = "No advance payment records found"
            End If
        Catch ex As Exception
            lblTotalAmount.Text = "Total Amount: Error"
            lblStatusResults.Text = "Error calculating totals"
        End Try
    End Sub
     
#End Region
#Region "Load Report Data"
    Private Sub print(reportType As String, reportName As String)
        Try
            Dim _receDs As New DataSet
            Dim copiedTable As DataTable = dtSalesData.Copy()
            copiedTable.TableName = "SalesData" ' Give it a meaningful name
            _receDs.Tables.Add(copiedTable)
            If (_receDs.Tables(0).Rows.Count > 0) Then
                _receDs.WriteXml(M_Details._appPath & "\Reports\ReportAdvance.xml", Data.XmlWriteMode.WriteSchema)
            End If
            Select Case reportName
                Case "Advance Payment by Employee"
                    If reportType.ToUpper() = "SUMMARY" Then
                        If clsBillPrint.GenrateReportA4Print(_receDs, Errstr, "ReportAdvanceSummary.repx") = False Then
                            lblStatusResults.Text = "Error: " & "ReportAdvanceSummary.repx"
                        End If
                    Else
                        If clsBillPrint.GenrateReportA4Print(_receDs, Errstr, "ReportAdvanceDetails.repx") = False Then
                            lblStatusResults.Text = "Error: " & "ReportAdvanceDetails.repx"
                        End If
                    End If
                Case "Advance Payment by All Branches"
                    If reportType.ToUpper() = "SUMMARY" Then
                        If clsBillPrint.GenrateReportA4Print(_receDs, Errstr, "ReportAdvanceSummary.repx") = False Then
                            lblStatusResults.Text = "Error: " & "ReportAdvanceSummary.repx"
                        End If
                    Else
                        If clsBillPrint.GenrateReportA4Print(_receDs, Errstr, "ReportAdvanceDetails.repx") = False Then
                            lblStatusResults.Text = "Error: " & "ReportAdvanceDetails.repx"
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
                GridControl1.DataSource = DataTable
                dtSalesData = DataTable ' Store for summary calculations

                ' Configure grid columns based on report type
                ConfigureGridColumns(reportType, reportName)
                RearrangeSalesManSummaryColumns()
                lblStatusResults.Text = "Data loaded successfully. Records: " & DataTable.Rows.Count.ToString()
            Else
                GridControl1.DataSource = Nothing
                dtSalesData = Nothing
                lblStatusResults.Text = "No data found for the selected criteria."
            End If

            ' Update summary labels
            UpdateSummaryLabels()

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
            ' For Advance Payment Report, use the GetAdvanceReport API (AjaxRequest=9)
            Dim operationType As String = If(reportType.ToUpper() = "SUMMARY", "1", "2")
            Dim optionsSalesMan As String = If(salesManId = "0", "1", "2")
            Dim optionComidLocid As String = If(reportName = "Advance Payment by Employee", "1", "2") ' Always filter by ComId and LocId

            ' Call the GetAdvanceReport API
            Return GetAdvanceReportFromAPI(comId, locId, startDate, endDate, salesManId, operationType, optionsSalesMan, optionComidLocid)

        Catch ex As Exception
            Throw New Exception("Error loading advance payment report data: " & ex.Message)
        End Try
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
        InitializeSalesManList()
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
        Try
            lblStatusResults.Text = "Preparing print preview..."
            CreatePrintableComponentLinkPreview()
            lblStatusResults.Text = "Print preview opened successfully."
        Catch ex As Exception
            lblStatusResults.Text = "Error: Failed to open print preview."
            MessageBox.Show("Error printing report: " & ex.Message, "Print Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
#Region "PrintViewReport"

    Private Sub CreatePrintableComponentLinkPreview()
        Try
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
            Dim headerCenter As String = vbCrLf & M_Details._shopName & vbCrLf & "ADVANCE PAYMENT REPORT" & vbCrLf & lblFillterDetails.Text
            Dim headerRight As String = "Page [Page # of Pages #]"
            phf.Header.Content.AddRange(New String() {headerLeft, headerCenter, headerRight})
            phf.Header.LineAlignment = BrickAlignment.Near ' Can be changed as needed
            phf.Header.Font = New Font("Arial", 10, FontStyle.Bold)

            ' Setup Footer with detailed information
            Dim footerLeft As String = "Period: " & dtStartDate.DateTime.ToString("dd/MM/yyyy") & " - " & dtEndDate.DateTime.ToString("dd/MM/yyyy")
            Dim footerCenter As String = "Generated: [Date Printed] by " & _companyInfo.UserName
            Dim footerRight As String = GetSummaryInfo()
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
    Private Function GetSummaryInfo() As String
        Try
            If dtSalesData IsNot Nothing AndAlso dtSalesData.Rows.Count > 0 Then
                Dim totalAmount As Decimal = 0
                Dim amountColumnName As String = "TotalAmount"
                ' Get selected report info to determine which column to sum
                Dim reportType As String = GetSelectedReportType()
                Dim reportName As String = GetSelectedReportName()




                If reportType.ToUpper() = "SUMMARY" Then
                    amountColumnName = "TotalAmount" ' For summary report
                Else
                    amountColumnName = "Amount" ' For detailed report
                End If


                ' Ensure the column exists before trying to access it
                If dtSalesData.Columns.Contains(amountColumnName) Then
                    For Each row As DataRow In dtSalesData.Rows
                        If Not IsDBNull(row(amountColumnName)) Then
                            totalAmount += Convert.ToDecimal(row(amountColumnName))
                        End If
                    Next
                    Return String.Format("Records: {0:n0} | Total: {1:c}", dtSalesData.Rows.Count, totalAmount)
                Else
                    Return "Summary unavailable (Column missing)"
                End If
            Else
                Return "No data available"
            End If
        Catch ex As Exception
            Return "Summary unavailable (Error)"
        End Try
    End Function
    Private Sub btnprintreport_Click(sender As Object, e As EventArgs) Handles btnprintreport.Click
        Try
            Dim reportType As String = GetSelectedReportType()
            Dim reportName As String = GetSelectedReportName()
            print(reportType, reportName)
        Catch ex As Exception

        End Try
    End Sub

#End Region
#Region "Api Get Data"
    Private Function GetAdvanceReportFromAPI(comId As String, locId As String, startDate As String, endDate As String, salesmanId As String, operationType As String, optionsSalesMan As String, optionComidLocid As String) As DataTable
        Try
            ' Build API URL for AjaxRequest=9 (GetAdvanceReport)
            Dim url As String = M_Details.LinkAjaxRequestSyncLocalCloud & "AjaxRequest=9"
            Dim postData As String = String.Format("comid={0}&locid={1}&startDate={2}&endDate={3}&salesmanId={4}&OperationType={5}&OptionsSalesMan={6}&OptionComidLocid={7}",
                                                  comId, locId, startDate, endDate, salesmanId, operationType, optionsSalesMan, optionComidLocid)

            Dim AdvanceData As New DataTable()
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New WebClient()
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                Dim jsonResponse As String = client.UploadString(url, postData)

                If Not String.IsNullOrEmpty(jsonResponse) Then

                    Dim Userparsejson As JObject = JObject.Parse(jsonResponse)

                    If Userparsejson("Success").ToString().ToLower() = "true" Then
                        If Userparsejson("Data") IsNot Nothing Then
                            Dim dataArray As JArray = CType(Userparsejson("Data"), JArray)
                            If dataArray IsNot Nothing AndAlso dataArray.Count > 0 Then
                                Return JsonArrayToDataTable(dataArray)
                            Else
                                Return New DataTable() ' Return empty DataTable if no data
                            End If
                        End If
                    Else
                        ' Show error message from API
                        Dim errorMsg As String = If(Userparsejson("Msg") IsNot Nothing, Userparsejson("Msg").ToString(), "Unknown API error")
                        System.Diagnostics.Debug.WriteLine("Advance Report API Error: " & errorMsg)
                        Return New DataTable() ' Return empty DataTable on error
                    End If
                End If
            End Using
            Return New DataTable()
        Catch ex As Exception
            lblStatusResults.Text = "Error: " & ex.Message
            MessageBox.Show("Error loading sales data: " & ex.Message, "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return New DataTable() ' Return empty DataTable on exception
        End Try
    End Function

   
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
                            row(prop.Name) = If(prop.Value.ToString(), "")
                        End If
                    Next
                    dt.Rows.Add(row)
                Next
            End If

            Return dt

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error converting JSON array to DataTable: " & ex.Message)
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

            ' Remove any HTML or PHP error content that might be mixed with JSON
            Dim jsonStart As Integer = Math.Max(cleaned.IndexOf("{"c), cleaned.IndexOf("["c))
            If jsonStart > 0 Then
                cleaned = cleaned.Substring(jsonStart)
            End If

            ' Remove any content after the last JSON closing brace/bracket
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


    Private Sub btnAdvance_Click(sender As Object, e As EventArgs) Handles btnAdvance.Click
        Try
            frmPayouts.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
End Class
