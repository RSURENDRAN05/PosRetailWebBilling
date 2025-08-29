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

Public Class frmSalesDetailsReport
    Private dtSalesData As DataTable

    Private Sub frmSalesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

            lblStatusResults.Text = "Fetching data from server..."

            ' Call API to get sales data
            Dim DataTable As DataTable = GetSalesDataFromAPI(4, comId, locId, startDate, endDate)

            If DataTable IsNot Nothing AndAlso DataTable.Rows.Count > 0 Then
                GridControl1.DataSource = DataTable
                dtSalesData = DataTable ' Store for summary calculations
                lblStatusResults.Text = "Data loaded successfully. Records: " & DataTable.Rows.Count.ToString()
            Else
                GridControl1.DataSource = Nothing
                dtSalesData = Nothing
                lblStatusResults.Text = "No sales data found for the selected criteria."
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

    Private Sub UpdateSummaryLabels()
        Try
            If dtSalesData IsNot Nothing AndAlso dtSalesData.Rows.Count > 0 Then
                Dim totalRecords As Integer = dtSalesData.Rows.Count
                Dim totalAmount As Decimal = 0

                ' Calculate total net amount
                For Each row As DataRow In dtSalesData.Rows
                    If Not IsDBNull(row("psid_invoice_netamt")) Then
                        totalAmount += Convert.ToDecimal(row("psid_invoice_netamt"))
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
            Dim headerCenter As String = vbCrLf & M_Details._shopName & vbCrLf & "SALES DETAILS REPORT" & vbCrLf & lblFillterDetails.Text
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
                Dim amountColumnName As String = "psid_invoice_netamt"

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
End Class
