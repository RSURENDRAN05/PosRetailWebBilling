Imports System.Data
Imports System.Net
Imports System.Text
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class frmSalesReport
    Private dtSalesData As DataTable

    Private Sub frmSalesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set default dates
        dtStartDate.DateTime = DateTime.Today
        dtEndDate.DateTime = DateTime.Today

        ' Set default values
        txtComId.Text = "1"
        txtLocId.Text = "1"

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

            ' Get parameters
            Dim comId As String = txtComId.Text.Trim()
            Dim locId As String = txtLocId.Text.Trim()
            Dim startDate As String = dtStartDate.DateTime.ToString("yyyy-MM-dd")
            Dim endDate As String = dtEndDate.DateTime.ToString("yyyy-MM-dd")

            ' Validate parameters
            If String.IsNullOrEmpty(comId) OrElse String.IsNullOrEmpty(locId) Then
                MessageBox.Show("Company ID and Location ID are required.", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            ' Call API to get sales data
            Dim DataTable As DataTable = GetSalesDataFromAPI(comId, locId, startDate, endDate)
            GridControl1.DataSource = DataTable

            ' Update summary labels
            UpdateSummaryLabels()

            ' Auto size columns for better visibility
            GridView1.BestFitColumns()

        Catch ex As Exception
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
        LoadSalesData()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadSalesData()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GridControl1.ShowPrintPreview()
        Catch ex As Exception
            MessageBox.Show("Error printing report: " & ex.Message, "Print Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "Excel Files|*.xlsx|CSV Files|*.csv|All Files|*.*"
            saveDialog.DefaultExt = "xlsx"
            saveDialog.FileName = "SalesReport_" & DateTime.Now.ToString("yyyyMMdd_HHmmss")

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Dim extension As String = System.IO.Path.GetExtension(saveDialog.FileName).ToLower()

                If extension = ".xlsx" Then
                    GridView1.ExportToXlsx(saveDialog.FileName)
                ElseIf extension = ".csv" Then
                    GridView1.ExportToCsv(saveDialog.FileName)
                End If

                MessageBox.Show("Report exported successfully!", "Export Complete",
                               MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
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

    Private Sub frmSalesReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        ' Add keyboard shortcuts
        Select Case e.KeyCode
            Case Keys.F5
                btnRefresh_Click(Nothing, Nothing)
            Case Keys.F3
                btnSearch_Click(Nothing, Nothing)
            Case Keys.F4
                btnExport_Click(Nothing, Nothing)
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub
End Class
