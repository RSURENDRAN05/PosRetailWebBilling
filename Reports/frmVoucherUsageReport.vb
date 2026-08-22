Imports System.Data
Imports System.Net
Imports Newtonsoft.Json.Linq

''' <summary>
''' Voucher Usage report - tracks voucher redemptions against sales (bill no, bill amount,
''' shift/day, company/location) for a selectable date range. Pulls from the cloud
''' voucher_sales table via AjaxRequest=93 (getfunctionmgmt.php / clsfunctionmgmt.php,
''' _GetVoucherUsageReport), filtered by From/To date for the logged-in company/location
''' (ComId/LocId come from _companyInfo, same as frmSalesMasterReport - no manual entry).
''' </summary>
Public Class frmVoucherUsageReport

    Private Sub frmVoucherUsageReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            dtStartDate.DateTime = DateTime.Today
            dtEndDate.DateTime = DateTime.Today
            InitializeGrid()
            LoadVoucherUsageData()
        Catch ex As Exception
            lblStatusResults.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub InitializeGrid()
        GridView1.OptionsBehavior.Editable = False
        GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        GridView1.OptionsView.EnableAppearanceEvenRow = True
        GridView1.OptionsView.EnableAppearanceOddRow = True
        GridView1.OptionsView.ShowGroupPanel = False
        GridView1.OptionsView.ShowAutoFilterRow = True
        GridView1.OptionsView.ShowFooter = True

        colBillAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        colBillAmount.DisplayFormat.FormatString = "n2"

        colCreated.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        colCreated.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"
    End Sub

    Private Sub LoadVoucherUsageData()
        Try
            Cursor = Cursors.WaitCursor
            lblStatusResults.Text = "Loading voucher usage..."

            Dim fromDate As DateTime = dtStartDate.DateTime.Date
            Dim toDate As DateTime = dtEndDate.DateTime.Date
            Dim comId As Integer = CInt(_companyInfo.ComId)
            Dim locId As Integer = CInt(_companyInfo.LocId)

            lblFillterDetails.Text = "Filter Details: " & _companyInfo.CompanyName & " (" & comId & ") - " &
                _companyInfo.LocationName & " (" & locId & ") | From: " & fromDate.ToString("dd/MM/yyyy") &
                " To: " & toDate.ToString("dd/MM/yyyy")

            Dim dt As DataTable = GetVoucherUsageFromCloud(fromDate, toDate, comId, locId)
            If dt Is Nothing Then dt = New DataTable

            GridControl1.DataSource = dt
            GridView1.BestFitColumns()

            UpdateSummaryLabels(dt)
            lblStatusResults.Text = "Data loaded successfully."
        Catch ex As Exception
            lblStatusResults.Text = "Error: " & ex.Message
            DevExpress.XtraEditors.XtraMessageBox.Show("Error loading voucher usage: " & ex.Message, "Error",
                                                         MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' AjaxRequest=93 - voucher_sales rows for the given date range + company/location.
    ''' </summary>
    Private Function GetVoucherUsageFromCloud(fromDate As DateTime, toDate As DateTime, comId As Integer, locId As Integer) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("Id", GetType(Integer))
        dt.Columns.Add("VoucherId", GetType(Integer))
        dt.Columns.Add("VoucherNo", GetType(Integer))
        dt.Columns.Add("VoucherCode", GetType(String))
        dt.Columns.Add("BillNo", GetType(String))
        dt.Columns.Add("CompanyName", GetType(String))
        dt.Columns.Add("LocationName", GetType(String))
        dt.Columns.Add("ShiftNo", GetType(Integer))
        dt.Columns.Add("DayNo", GetType(Integer))
        dt.Columns.Add("BillAmount", GetType(Decimal))
        dt.Columns.Add("Created", GetType(String))

        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim url As String = M_Details.LinkAjaxRequest & "AjaxRequest=93" &
                "&fromdate=" & fromDate.ToString("yyyy-MM-dd") &
                "&todate=" & toDate.ToString("yyyy-MM-dd") &
                "&comid=" & comId.ToString() &
                "&locid=" & locId.ToString()

            Dim response As String = New WebClient().DownloadString(url)
            Dim obj As JObject = JObject.Parse(response)

            Dim isSuccess As Boolean = False
            If obj("Success") IsNot Nothing Then Boolean.TryParse(obj("Success").ToString(), isSuccess)
            If Not isSuccess Then
                Dim msg As String = If(obj("Msg") IsNot Nothing, obj("Msg").ToString(), "Failed to load voucher usage.")
                DevExpress.XtraEditors.XtraMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return dt
            End If

            Dim arr As JArray = Nothing
            If obj("Data") IsNot Nothing AndAlso obj("Data").Type = JTokenType.Array Then
                arr = CType(obj("Data"), JArray)
            End If

            If arr IsNot Nothing Then
                For Each item As JObject In arr
                    Dim dr As DataRow = dt.NewRow()
                    dr("Id") = If(item("Id") IsNot Nothing, item("Id").ToObject(Of Integer)(), 0)
                    dr("VoucherId") = If(item("VoucherId") IsNot Nothing, item("VoucherId").ToObject(Of Integer)(), 0)
                    dr("VoucherNo") = If(item("VoucherNo") IsNot Nothing, item("VoucherNo").ToObject(Of Integer)(), 0)
                    dr("VoucherCode") = If(item("VoucherCode") IsNot Nothing, item("VoucherCode").ToString(), "")
                    dr("BillNo") = If(item("BillNo") IsNot Nothing, item("BillNo").ToString(), "")
                    dr("CompanyName") = If(item("CompanyName") IsNot Nothing, item("CompanyName").ToString(), "")
                    dr("LocationName") = If(item("LocationName") IsNot Nothing, item("LocationName").ToString(), "")
                    dr("ShiftNo") = If(item("ShiftNo") IsNot Nothing, item("ShiftNo").ToObject(Of Integer)(), 0)
                    dr("DayNo") = If(item("DayNo") IsNot Nothing, item("DayNo").ToObject(Of Integer)(), 0)
                    dr("BillAmount") = If(item("BillAmount") IsNot Nothing, item("BillAmount").ToObject(Of Decimal)(), 0D)
                    dr("Created") = If(item("Created") IsNot Nothing, item("Created").ToString(), "")
                    dt.Rows.Add(dr)
                Next
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show("Error contacting server: " & ex.Message, "Error",
                                                         MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dt
    End Function

    Private Sub UpdateSummaryLabels(dt As DataTable)
        Try
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim totalRecords As Integer = dt.Rows.Count
                Dim totalAmount As Decimal = 0
                For Each row As DataRow In dt.Rows
                    If Not IsDBNull(row("BillAmount")) Then
                        totalAmount += Convert.ToDecimal(row("BillAmount"))
                    End If
                Next
                lblTotalRecords.Text = "Total Vouchers Used: " & totalRecords.ToString("n0")
                lblTotalAmount.Text = "Total Bill Amount: " & totalAmount.ToString("n2")
            Else
                lblTotalRecords.Text = "Total Vouchers Used: 0"
                lblTotalAmount.Text = "Total Bill Amount: 0.00"
            End If
        Catch ex As Exception
            ' Ignore summary calculation errors
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadVoucherUsageData()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadVoucherUsageData()
    End Sub

    Private Sub dtStartDate_EditValueChanged(sender As Object, e As EventArgs) Handles dtStartDate.EditValueChanged
        If dtEndDate.DateTime < dtStartDate.DateTime Then
            dtEndDate.DateTime = dtStartDate.DateTime
        End If
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "Excel Files|*.xlsx|CSV Files|*.csv|All Files|*.*"
            saveDialog.DefaultExt = "xlsx"
            saveDialog.FileName = "VoucherUsageReport_" & DateTime.Now.ToString("yyyyMMdd_HHmmss")

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Dim extension As String = System.IO.Path.GetExtension(saveDialog.FileName).ToLower()
                If extension = ".xlsx" Then
                    GridView1.ExportToXlsx(saveDialog.FileName)
                ElseIf extension = ".csv" Then
                    GridView1.ExportToCsv(saveDialog.FileName)
                End If
                DevExpress.XtraEditors.XtraMessageBox.Show("Report exported successfully!", "Export Complete",
                                                             MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show("Error exporting report: " & ex.Message, "Export Error",
                                                         MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GridControl1.ShowPrintPreview()
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show("Error printing report: " & ex.Message, "Print Error",
                                                         MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
