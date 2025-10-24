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

Public Class frmSalesChronicalReport
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

        ' Add available report options
        ListBoxControlReportList.Items.Add("Sales by Salesman")
        ListBoxControlReportList.Items.Add("Sales by All Branch")

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
            Select Case reportName.ToUpper()
                Case "SALES BY SALESMAN", "SALES BY ALL BRANCH"
                    If reportType.ToUpper() = "SUMMARY" Then
                        ConfigureSalesManSummaryColumns()
                    Else
                        ConfigureSalesManDetailedColumns()
                    End If
            End Select
            RearrangeSalesManSummaryColumns()
            ' Auto size all columns
            GridView1.BestFitColumns()

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error configuring grid columns: " & ex.Message)
        End Try
    End Sub
    Private Sub RearrangeSalesManSummaryColumns()
        Try
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In GridView1.Columns
                If col.FieldName = "ID" OrElse col.FieldName = "TotalItems" OrElse col.FieldName = "Percentage" _
                 OrElse col.FieldName = "Date" OrElse col.FieldName = "TransactionNo" OrElse col.FieldName = "CompanyName" _
                  OrElse col.FieldName = "LocationName" Then
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center
                End If
                If col.FieldName = "TotalNetAmt" OrElse col.FieldName = "AvgPercentage" OrElse col.FieldName = "TotalCommission" OrElse col.FieldName = "NetAmt" _
                    OrElse col.FieldName = "Commission" OrElse col.FieldName = "Qty" Then
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ConfigureSalesManSummaryColumns()
        ' Configure columns for SalesMan Summary Report
        ' Based on SQL: emp_id AS ID, emp_printname AS Name, TotalItems, TotalNetAmt, AvgPercentage, TotalCommission

        ' ID Column
        Dim colID As New DevExpress.XtraGrid.Columns.GridColumn()
        colID.FieldName = "ID"
        colID.Caption = "Sales ID"
        colID.Visible = True
        colID.Width = 80
        colID.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colID.DisplayFormat.FormatString = "d"
        GridView1.Columns.Add(colID)

        ' Name Column
        Dim colName As New DevExpress.XtraGrid.Columns.GridColumn()
        colName.FieldName = "Name"
        colName.Caption = "Salesman Name"
        colName.Visible = True
        colName.Width = 150
        GridView1.Columns.Add(colName)

        ' Total Items Column
        Dim colTotalItems As New DevExpress.XtraGrid.Columns.GridColumn()
        colTotalItems.FieldName = "TotalItems"
        colTotalItems.Caption = "Total Items"
        colTotalItems.Visible = True
        colTotalItems.Width = 100
        colTotalItems.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colTotalItems.DisplayFormat.FormatString = "n0"
        colTotalItems.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colTotalItems.SummaryItem.DisplayFormat = "Total: {0:n0}"
        GridView1.Columns.Add(colTotalItems)

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

        ' Total Net Amount Column
        Dim colTotalNetAmt As New DevExpress.XtraGrid.Columns.GridColumn()
        colTotalNetAmt.FieldName = "TotalNetAmt"
        colTotalNetAmt.Caption = "Total Net Amount"
        colTotalNetAmt.Visible = True
        colTotalNetAmt.Width = 120
        colTotalNetAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colTotalNetAmt.DisplayFormat.FormatString = "n2"
        colTotalNetAmt.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colTotalNetAmt.SummaryItem.DisplayFormat = "Total: {0:n2}"
        GridView1.Columns.Add(colTotalNetAmt)

        ' Average Percentage Column
        Dim colAvgPercentage As New DevExpress.XtraGrid.Columns.GridColumn()
        colAvgPercentage.FieldName = "AvgPercentage"
        colAvgPercentage.Caption = "Avg Commission %"
        colAvgPercentage.Visible = True
        colAvgPercentage.Width = 120
        colAvgPercentage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colAvgPercentage.DisplayFormat.FormatString = "n2"
        colAvgPercentage.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average
        colAvgPercentage.SummaryItem.DisplayFormat = "Avg: {0:n2}%"
        GridView1.Columns.Add(colAvgPercentage)

        ' Total Commission Column
        Dim colTotalCommission As New DevExpress.XtraGrid.Columns.GridColumn()
        colTotalCommission.FieldName = "TotalCommission"
        colTotalCommission.Caption = "Total Commission"
        colTotalCommission.Visible = True
        colTotalCommission.Width = 130
        colTotalCommission.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colTotalCommission.DisplayFormat.FormatString = "n2"
        colTotalCommission.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colTotalCommission.SummaryItem.DisplayFormat = "Total: {0:n2}"
        GridView1.Columns.Add(colTotalCommission)

        Dim colCompanyName As New DevExpress.XtraGrid.Columns.GridColumn()
        colCompanyName.FieldName = "CompanyName"
        colCompanyName.Caption = "Company Name"
        colCompanyName.Visible = True
        colCompanyName.Width = 150
        GridView1.Columns.Add(colCompanyName)

        Dim colLocationName As New DevExpress.XtraGrid.Columns.GridColumn()
        colLocationName.FieldName = "LocationName"
        colLocationName.Caption = "Location Name"
        colLocationName.Visible = True
        colLocationName.Width = 150
        GridView1.Columns.Add(colLocationName)




    End Sub

    Private Sub ConfigureSalesManDetailedColumns()
        ' Configure columns for SalesMan Detailed Report
        ' Based on detailed commission report structure

        Dim colDate As New DevExpress.XtraGrid.Columns.GridColumn()
        colDate.FieldName = "Date"
        colDate.Caption = "Date"
        colDate.Width = 100
        colDate.Visible = True
        colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        colDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        GridView1.Columns.Add(colDate)

        Dim colTransactionNo As New DevExpress.XtraGrid.Columns.GridColumn()
        colTransactionNo.FieldName = "TransactionNo"
        colTransactionNo.Caption = "Transaction No"
        colTransactionNo.Width = 120
        colTransactionNo.Visible = True
        GridView1.Columns.Add(colTransactionNo)
        Dim colID As New DevExpress.XtraGrid.Columns.GridColumn()
        colID.FieldName = "ID"
        colID.Caption = "Sales ID"
        colID.Width = 80
        colID.Visible = True
        GridView1.Columns.Add(colID)

        Dim colName As New DevExpress.XtraGrid.Columns.GridColumn()
        colName.FieldName = "Name"
        colName.Caption = "Salesman Name"
        colName.Width = 150
        colName.Visible = True
        GridView1.Columns.Add(colName)

        Dim colItemName As New DevExpress.XtraGrid.Columns.GridColumn()
        colItemName.FieldName = "ItemName"
        colItemName.Caption = "Item Name"
        colItemName.Width = 200
        colItemName.Visible = True
        GridView1.Columns.Add(colItemName)
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

        Dim colNetAmt As New DevExpress.XtraGrid.Columns.GridColumn()
        colNetAmt.FieldName = "NetAmt"
        colNetAmt.Caption = "Net Amount"
        colNetAmt.Width = 100
        colNetAmt.Visible = True
        colNetAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colNetAmt.DisplayFormat.FormatString = "n2"
        colNetAmt.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colNetAmt.SummaryItem.DisplayFormat = "Total: {0:n2}"
        GridView1.Columns.Add(colNetAmt)

        Dim colPercentage As New DevExpress.XtraGrid.Columns.GridColumn()
        colPercentage.FieldName = "Percentage"
        colPercentage.Caption = "Commission %"
        colPercentage.Width = 100
        colPercentage.Visible = True
        colPercentage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colPercentage.DisplayFormat.FormatString = "n2"
        colPercentage.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average
        colPercentage.SummaryItem.DisplayFormat = "Avg: {0:n2}%"
        GridView1.Columns.Add(colPercentage)

        Dim colCommission As New DevExpress.XtraGrid.Columns.GridColumn()
        colCommission.FieldName = "Commission"
        colCommission.Caption = "Commission"
        colCommission.Width = 100
        colCommission.Visible = True
        colCommission.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colCommission.DisplayFormat.FormatString = "n2"
        colCommission.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colCommission.SummaryItem.DisplayFormat = "Total: {0:n2}"
        GridView1.Columns.Add(colCommission)

        Dim colCompanyName As New DevExpress.XtraGrid.Columns.GridColumn()
        colCompanyName.FieldName = "CompanyName"
        colCompanyName.Caption = "Company Name"
        colCompanyName.Visible = True
        colCompanyName.Width = 150
        GridView1.Columns.Add(colCompanyName)

        Dim colLocationName As New DevExpress.XtraGrid.Columns.GridColumn()
        colLocationName.FieldName = "LocationName"
        colLocationName.Caption = "Location Name"
        colLocationName.Visible = True
        colLocationName.Width = 150
        GridView1.Columns.Add(colLocationName)

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
    Private Sub UpdateSummaryLabels()
        Try
            If dtSalesData IsNot Nothing AndAlso dtSalesData.Rows.Count > 0 Then
                Dim totalRecords As Integer = dtSalesData.Rows.Count
                Dim totalAmount As Decimal = 0

                ' Get selected report info to determine which column to sum
                Dim reportType As String = GetSelectedReportType()
                Dim reportName As String = GetSelectedReportName()

                ' Determine the amount column based on report type
                Dim amountColumnName As String = ""

                Select Case reportName.ToUpper()
                    Case "SALES BY SALESMAN", "SALES BY ALL BRANCH"
                        If reportType.ToUpper() = "SUMMARY" Then
                            amountColumnName = "TotalNetAmt" ' For summary report
                        Else
                            amountColumnName = "NetAmt" ' For detailed report
                        End If
                    Case Else
                        amountColumnName = "psid_invoice_netamt" ' Default for other reports
                End Select

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
                lblTotalAmount.Text = "Total Amount: " & totalAmount.ToString("c2")
            Else
                lblTotalRecords.Text = "Total Records: 0"
                lblTotalAmount.Text = "Total Amount: 0.00"
            End If
        Catch ex As Exception
            ' Handle any calculation errors silently
            lblTotalRecords.Text = "Total Records: 0"
            lblTotalAmount.Text = "Total Amount: Error"
        End Try
    End Sub
#End Region
#Region "Load Report Data"
    Private Sub printReportDesign(reportType As String, reportName As String)
        Try
            Dim _receDs As New DataSet
            Dim copiedTable As DataTable = dtSalesData.Copy()
            copiedTable.TableName = "SalesData" ' Give it a meaningful name

            ' Create DateFilter table
            Dim dateFilterTable As New DataTable("DateFilter")
            dateFilterTable.Columns.Add("FromDate", GetType(String))
            dateFilterTable.Columns.Add("ToDate", GetType(String))

            ' Add date filter values
            Dim dateRow As DataRow = dateFilterTable.NewRow()
            dateRow("FromDate") = dtStartDate.DateTime.ToString("yyyy-MM-dd")
            dateRow("ToDate") = dtEndDate.DateTime.ToString("yyyy-MM-dd")
            dateFilterTable.Rows.Add(dateRow)

            ' Add tables to dataset
            _receDs.Tables.Add(copiedTable)
            _receDs.Tables.Add(dateFilterTable)

            If (_receDs.Tables(0).Rows.Count > 0) Then
                _receDs.WriteXml(M_Details._appPath & "\Reports\ReportChronical.xml", Data.XmlWriteMode.WriteSchema)
            End If
            Select Case reportName.ToUpper()
                Case "SALES BY SALESMAN"
                    If reportType.ToUpper() = "SUMMARY" Then
                        If clsBillPrint.GenrateReportA4Print(_receDs, Errstr, "ReportChronicalSummary.repx") = False Then
                            lblStatusResults.Text = "Error: " & "ReportChronicalSummary.repx"
                        End If
                    Else
                        If clsBillPrint.GenrateReportA4Print(_receDs, Errstr, "ReportChronicalDetailed.repx") = False Then
                            lblStatusResults.Text = "Error: " & "ReportChronicalDetailed.repx"
                        End If
                    End If
                Case "SALES BY ALL BRANCH"
                    If reportType.ToUpper() = "SUMMARY" Then
                        If clsBillPrint.GenrateReportA4Print(_receDs, Errstr, "ReportChronicalSummary.repx") = False Then
                            lblStatusResults.Text = "Error: " & "ReportChronicalSummary.repx"
                        End If
                    Else
                        If clsBillPrint.GenrateReportA4Print(_receDs, Errstr, "ReportChronicalDetailed.repx") = False Then
                            lblStatusResults.Text = "Error: " & "ReportChronicalDetailed.repx"
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
            ' Determine API endpoint based on report type and name
            Select Case reportName.ToUpper()
                Case "SALES BY SALESMAN"
                    If reportType.ToUpper() = "SUMMARY" Then
                        Return GetSalesManReportSummaryFromAPI(comId, locId, salesManId, startDate, endDate)
                    Else
                        Return GetSalesManReportDetailedFromAPI(comId, locId, salesManId, startDate, endDate)
                    End If
                Case "SALES BY ALL BRANCH"
                    If reportType.ToUpper() = "SUMMARY" Then
                        Return GetSalesManReportSummaryFromAPI(0, 0, salesManId, startDate, endDate)
                    Else
                        Return GetSalesManReportDetailedFromAPI(0, 0, salesManId, startDate, endDate)
                    End If
                Case Else
                    ' Default to detailed sales data
                    Return GetSalesDataFromAPI(4, comId, locId, startDate, endDate)
            End Select

        Catch ex As Exception
            Throw New Exception("Error loading report data: " & ex.Message)
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
            Dim headerCenter As String = vbCrLf & M_Details._shopName & vbCrLf & "SALES DETAILS BY SALESMAN REPORT" & vbCrLf & lblFillterDetails.Text
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
                Dim amountColumnName As String = "psid_invoice_netamt"
                ' Get selected report info to determine which column to sum
                Dim reportType As String = GetSelectedReportType()
                Dim reportName As String = GetSelectedReportName()


                Select Case reportName.ToUpper()
                    Case "SALES BY SALESMAN", "SALES BY ALL BRANCH"
                        If reportType.ToUpper() = "SUMMARY" Then
                            amountColumnName = "TotalNetAmt" ' For summary report
                        Else
                            amountColumnName = "NetAmt" ' For detailed report
                        End If
                    Case Else
                        amountColumnName = "psid_invoice_netamt" ' Default for other reports
                End Select

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
            printReportDesign(reportType, reportName)
        Catch ex As Exception

        End Try
    End Sub

#End Region
#Region "Api Get Data"
    Private Function GetSalesManReportSummaryFromAPI(comId As String, locId As String, salesManId As String, startDate As String, endDate As String) As DataTable
        Try
            Dim url As String = M_Details.LinkAjaxRequestSyncLocalCloud & "AjaxRequest=5"
            Dim postData As String = String.Format("comid={0}&locid={1}&salesmanId={2}&startDate={3}&endDate={4}",
                                                  comId, locId, salesManId, startDate, endDate)
            Dim SalesData As New DataTable
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Using client As New WebClient()
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                Dim jsonResponse As String = client.UploadString(url, postData)
                Dim Userparsejson As JObject = JObject.Parse(jsonResponse)
                If Not String.IsNullOrEmpty(jsonResponse) Then
                    If Userparsejson("Success").ToString().ToLower() = "true" Then
                        If Userparsejson("Data") IsNot Nothing Then
                            SalesData = Userparsejson("Data").ToObject(Of DataTable)()
                            Return SalesData
                        End If
                    Else
                        ' Show error message from API
                        Dim errorMsg As String = If(Userparsejson("Msg") IsNot Nothing, Userparsejson("Msg").ToString(), "Unknown API error")
                        MessageBox.Show("API Error: " & errorMsg, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If

                End If
            End Using
            ' Check if the response was successful


        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error fetching salesman summary report: " & ex.Message)
        End Try

        Return Nothing
    End Function

    Private Function GetSalesManReportDetailedFromAPI(comId As String, locId As String, salesManId As String, startDate As String, endDate As String) As DataTable
        Try
            Dim url As String = M_Details.LinkAjaxRequestSyncLocalCloud & "AjaxRequest=6"
            Dim postData As String = String.Format("comid={0}&locid={1}&salesmanId={2}&startDate={3}&endDate={4}",
                                                  comId, locId, salesManId, startDate, endDate)
            Dim SalesData As New DataTable
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Using client As New WebClient()
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                Dim jsonResponse As String = client.UploadString(url, postData)
                Dim Userparsejson As JObject = JObject.Parse(jsonResponse)
                If Not String.IsNullOrEmpty(jsonResponse) Then
                    If Userparsejson("Success").ToString().ToLower() = "true" Then
                        If Userparsejson("Data") IsNot Nothing Then
                            SalesData = Userparsejson("Data").ToObject(Of DataTable)()
                            Return SalesData
                        End If
                    Else
                        ' Show error message from API
                        Dim errorMsg As String = If(Userparsejson("Msg") IsNot Nothing, Userparsejson("Msg").ToString(), "Unknown API error")
                        MessageBox.Show("API Error: " & errorMsg, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If

                End If
            End Using
            ' Check if the response was successful


        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error fetching salesman summary report: " & ex.Message)
        End Try

        Return Nothing
    End Function

    Private Function GetSalesSummaryReportFromAPI(comId As String, locId As String, startDate As String, endDate As String) As DataTable
        Try
            Dim url As String = "http://localhost/retailbilling/controller/getsynctocloudlocal.php"
            Dim postData As String = String.Format("comid={0}&locid={1}&startdate={2}&enddate={3}&reporttype=salessummary",
                                                  comId, locId, startDate, endDate)

            Using client As New WebClient()
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                Dim jsonResponse As String = client.UploadString(url, postData)

                If Not String.IsNullOrEmpty(jsonResponse) Then
                    ' Return ConvertJsonToDataTable(jsonResponse)
                End If
            End Using

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error fetching sales summary report: " & ex.Message)
        End Try

        Return Nothing
    End Function

    Private Function GetSalesDetailedReportFromAPI(comId As String, locId As String, startDate As String, endDate As String) As DataTable
        Try
            Dim url As String = "http://localhost/retailbilling/controller/getsynctocloudlocal.php"
            Dim postData As String = String.Format("comid={0}&locid={1}&startdate={2}&enddate={3}&reporttype=salesdetailed",
                                                  comId, locId, startDate, endDate)

            Using client As New WebClient()
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                Dim jsonResponse As String = client.UploadString(url, postData)

                If Not String.IsNullOrEmpty(jsonResponse) Then
                    '  Return ConvertJsonToDataTable(jsonResponse)
                End If
            End Using

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error fetching sales detailed report: " & ex.Message)
        End Try

        Return Nothing
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


End Class
