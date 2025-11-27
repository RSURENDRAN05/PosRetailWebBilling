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



Public Class frmSalesMasterReport
    Private dtSalesData As DataTable
    Dim Errstr As String = ""
    Private Sub frmSalesMasterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize company info first


        ' Set default dates
        dtStartDate.DateTime = DateTime.Today
        dtEndDate.DateTime = DateTime.Today

        ' Initialize status
        lblStatusResults.Text = "Ready to load data..."
        lblFillterDetails.Text = "Filter Details: No data loaded"

        ' Initialize grid
        InitializeGrid()

        ' Load initial data
        LoadSalesData()
        LoadGridLayout()
    End Sub

    Private Sub InitializeGrid()
        ' Set grid appearance
        GridView1.OptionsView.ShowIndicator = True
        GridView1.OptionsBehavior.Editable = False
        GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

        ' Set alternate row colors
        GridView1.OptionsView.EnableAppearanceEvenRow = True
        GridView1.OptionsView.EnableAppearanceOddRow = True

        ' Enable grouping and filtering
        GridView1.OptionsView.ShowGroupPanel = True
        GridView1.OptionsView.ShowAutoFilterRow = True

        ' Enable footer for calculations
        GridView1.OptionsView.ShowFooter = True

        ' Set column formats
        SetColumnFormats()
    End Sub

    Private Sub SetColumnFormats()
        ' Set numeric columns format
        colTotalQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colTotalQty.DisplayFormat.FormatString = "n2"

        colTotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colTotalAmount.DisplayFormat.FormatString = "n2"

        colItemDiscAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colItemDiscAmt.DisplayFormat.FormatString = "n2"

        colBillDiscAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colBillDiscAmt.DisplayFormat.FormatString = "n2"

        colTotDiscAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colTotDiscAmt.DisplayFormat.FormatString = "n2"

        colGrossAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colGrossAmt.DisplayFormat.FormatString = "n2"

        colTaxAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colTaxAmt.DisplayFormat.FormatString = "n2"

        colNetAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colNetAmt.DisplayFormat.FormatString = "n2"

        ' Set date columns format
        colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        colDate.DisplayFormat.FormatString = "dd/MM/yyyy"

        colCreated.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        colCreated.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"

        colModified.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        colModified.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"
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

            ' Update filter details label with company, location and date range
            lblFillterDetails.Text = "Filter Details: " & _companyInfo.CompanyName & " (" & _companyInfo.ComId & ") - " & _companyInfo.LocationName & " (" & _companyInfo.LocId & ") | From: " & dtStartDate.DateTime.ToString("dd/MM/yyyy") & " To: " & dtEndDate.DateTime.ToString("dd/MM/yyyy")

            ' Validate parameters
            If String.IsNullOrEmpty(comId) OrElse String.IsNullOrEmpty(locId) Then
                lblStatusResults.Text = "Error: Company ID and Location ID are required."
                MessageBox.Show("Company ID and Location ID are required.", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            ' 4️⃣ Fetch dataset (Header, Details, Payment)
            Dim ds As DataSet = GetSalesMasterDataFromAPI(14, comId, locId, startDate, endDate)
            If ds Is Nothing OrElse ds.Tables.Count < 3 Then
                lblStatusResults.Text = "No data returned from server."
                MessageBox.Show("No data available for the selected period.", "No Data",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' 5️⃣ Assign table names
            ds.Tables(0).TableName = "Header"
            ds.Tables(1).TableName = "Details"
            ds.Tables(2).TableName = "Payment"

            ' 6️⃣ Define relations
            ds.Relations.Clear()

            ' Disable constraint enforcement
            ds.EnforceConstraints = False

            ' Relations
            Dim relHeaderDetails As New DataRelation("HeaderDetails",
                ds.Tables("Header").Columns("psih_invoice_trno"),
                ds.Tables("Details").Columns("psid_invoice_trno"), False)
            ds.Relations.Add(relHeaderDetails)

            Dim relHeaderPayment As New DataRelation("HeaderPayment",
                ds.Tables("Header").Columns("psih_invoice_trno"),
                ds.Tables("Payment").Columns("Sal_ID"), False)
            ds.Relations.Add(relHeaderPayment)


            ' 7️⃣ Bind main grid
            GridControl1.DataSource = ds.Tables("Header")

            ' 8️⃣ Clear old nodes and add both detail levels
            GridControl1.LevelTree.Nodes.Clear()
            GridControl1.LevelTree.Nodes.Add("HeaderDetails", BandedGridView1)
            GridControl1.LevelTree.Nodes.Add("HeaderPayment", BandedGridView2)

            ' 9️⃣ Assign main view
            GridControl1.MainView = GridView1

            ' 🔟 Auto-fit columns
            GridView1.BestFitColumns()
            BandedGridView1.BestFitColumns()
            BandedGridView2.BestFitColumns()

            lblStatusResults.Text = "Data loaded successfully."
            UpdateSummaryLabels()
            lblStatusResults.Text = "Fetching data from server..."

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

    Private Sub UpdateSummaryLabels()
        Try
            If dtSalesData IsNot Nothing AndAlso dtSalesData.Rows.Count > 0 Then
                Dim totalRecords As Integer = dtSalesData.Rows.Count
                Dim totalAmount As Decimal = 0

                ' Calculate total net amount
                For Each row As DataRow In dtSalesData.Rows
                    If Not IsDBNull(row("psih_invoice_tnetamt")) Then
                        totalAmount += Convert.ToDecimal(row("psih_invoice_tnetamt"))
                    End If
                Next

                ' Update labels
                lblTotalRecords.Text = "Total Records: " & totalRecords.ToString("n0")
                lblTotalAmount.Text = "Total Amount: " & totalAmount.ToString("n2")
            Else
                lblTotalRecords.Text = "Total Records: 0"
                lblTotalAmount.Text = "Total Amount: 0.00"
            End If
        Catch ex As Exception
            ' Handle any calculation errors silently
        End Try
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

    Private Sub GridView1_CustomSummaryCalculate(sender As Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles GridView1.CustomSummaryCalculate
        ' Custom summary calculations can be added here if needed
    End Sub

    'Private Sub GridView1_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView1.RowCellStyle
    '    ' Conditional formatting based on bill status
    '    If e.Column.FieldName = "psih_invoice_billstatus" Then
    '        Dim status As String = e.CellValue.ToString()
    '        Select Case status
    '            Case "Completed"
    '                e.Appearance.ForeColor = Color.Green
    '                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
    '            Case "Cancelled"
    '                e.Appearance.ForeColor = Color.Red
    '                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
    '            Case "Pending"
    '                e.Appearance.ForeColor = Color.Orange
    '                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
    '        End Select
    '    End If
    'End Sub

    Private Sub dtStartDate_EditValueChanged(sender As Object, e As EventArgs) Handles dtStartDate.EditValueChanged
        ' Auto-update end date if it's before start date
        If dtEndDate.DateTime < dtStartDate.DateTime Then
            dtEndDate.DateTime = dtStartDate.DateTime
        End If
    End Sub

    ' Private Sub frmSalesReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    '     ' Add keyboard shortcuts
    '     Select Case e.KeyCode
    '         Case Keys.F5
    '             btnRefresh_Click(Nothing, Nothing)
    '         Case Keys.F3
    '             btnSearch_Click(Nothing, Nothing)
    '         Case Keys.F4
    '             btnExport_Click(Nothing, Nothing)
    '         Case Keys.Escape
    '             Me.Close()
    '     End Select
    ' End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
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

    ' ======================================================
    ' 🟢 Create Report Header (Company Profile Section)
    ' ======================================================
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
            Dim headerCenter As String = vbCrLf & M_Details._shopName & vbCrLf & "SALES SUMMARY REPORT" & vbCrLf & lblFillterDetails.Text
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
            printableComponentLink.ShowPreview()

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
                Dim amountColumnName As String = "psih_invoice_tnetamt"

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

    Private Sub SetBasicPrintSettings()
        Try
            ' Very basic, universally compatible settings
            With GridView1.OptionsPrint
                .PrintHeader = True
                .PrintFooter = True
            End With

            ' Simple title that should work in all DevExpress versions
            GridView1.ViewCaption = "SALES REPORT - " & _companyInfo.CompanyName & " - " &
                                  dtStartDate.DateTime.ToString("dd/MM/yyyy") & " to " &
                                  dtEndDate.DateTime.ToString("dd/MM/yyyy")

        Catch ex As Exception
            MessageBox.Show("Error setting print options: " & ex.Message, "Configuration Error")
        End Try
    End Sub


    ' ======================================================
    ' 🟢 Helper Function (Address)
    ' ======================================================
    Private Function GetCompanyAddress() As String
        Try
            ' Replace with actual data source
            Return "123 Business Street, Business City, State 12345, Phone: (123) 456-7890"
        Catch ex As Exception
            Return "Address Not Available"
        End Try
    End Function

    Private Sub btnPrintPreview_Click(sender As Object, e As EventArgs) Handles btnPrintPreview.Click
        Try
            printReportDesign()
        Catch ex As Exception

        End Try
    End Sub
#Region "Load Report Data"
    Private Sub printReportDesign()
        Try
            Dim _receDs As New DataSet
            Dim copiedTable As DataTable = GetSalesByAllBranch()
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
                _receDs.WriteXml(M_Details._appPath & "\Reports\ReportByAllBrachSales.xml", Data.XmlWriteMode.WriteSchema)
            End If
            If clsBillPrint.GenrateReportA4Print(_receDs, Errstr, "ReportByAllBrachSales.repx") = False Then
                lblStatusResults.Text = "Error: " & "ReportByAllBrachSales.repx"
            End If
        Catch ex As Exception
            lblStatusResults.Text = "Error: " & ex.Message
            MessageBox.Show("Error loading sales data: " & ex.Message, "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    
    Private Function GetSalesByAllBranch() As DataTable
        Try
            Dim startDate As String = dtStartDate.DateTime.ToString("yyyy-MM-dd")
            Dim endDate As String = dtEndDate.DateTime.ToString("yyyy-MM-dd")
            Dim url As String = M_Details.LinkAjaxRequestSyncLocalCloud & "AjaxRequest=15"
            Dim postData As String = String.Format("startDate={0}&endDate={1}", startDate, endDate)
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
#End Region

    Private Sub btnSaveLayout_Click(sender As Object, e As EventArgs) Handles btnSaveLayout.Click
        Try
            Try
                SaveGridLayout()
                MessageBox.Show("Grid layout saved successfully!", "Save Layout", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error saving grid layout: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SaveGridLayout()
        Try
            Dim layoutPath As String = GetLayoutFilePath()
            ' Create directory if it doesn't exist
            Dim layoutDir As String = System.IO.Path.GetDirectoryName(layoutPath)
            If Not System.IO.Directory.Exists(layoutDir) Then
                System.IO.Directory.CreateDirectory(layoutDir)
            End If

            ' Save the grid view layout
            GridView1.SaveLayoutToXml(layoutPath)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LoadGridLayout()
        Try
            Dim layoutPath As String = GetLayoutFilePath()
            If System.IO.File.Exists(layoutPath) Then
                GridView1.RestoreLayoutFromXml(layoutPath)
            End If
        Catch ex As Exception
            ' If there's an error loading the layout, just continue with default layout
            ' This prevents the form from failing to load if the layout file is corrupted
        End Try
    End Sub

    Private Function GetLayoutFilePath() As String
        ' Create a layout file path in the application's folder
        Dim appPath As String = Application.StartupPath
        Dim layoutFolder As String = System.IO.Path.Combine(appPath, "Layout")
        Return System.IO.Path.Combine(layoutFolder, "MasterSalesReport.xml")
    End Function
End Class
