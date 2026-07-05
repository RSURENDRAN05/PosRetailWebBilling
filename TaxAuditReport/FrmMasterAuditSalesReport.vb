Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.BrickAlignment
Imports DevExpress.XtraPrinting.PageHeaderFooter

Public Class FrmMasterAuditSalesReport

    ' ----------------------------------------------------------------
    '  Build the URL for AjaxRequest 9 (taxauditreport)
    ' ----------------------------------------------------------------
    Private Function BuildTaxAuditUrl(ByVal jsonPayload As String) As String
        Dim baseUrl As String = M_Details.LinkTaxAuditRequest
        If String.IsNullOrWhiteSpace(baseUrl) Then
            baseUrl = M_Details.LinkAjaxRequest
        End If
        If String.IsNullOrWhiteSpace(baseUrl) Then Return String.Empty

        If Not baseUrl.EndsWith("?") AndAlso Not baseUrl.EndsWith("&") Then
            baseUrl &= If(baseUrl.Contains("?"), "&", "?")
        End If
        Return baseUrl & "AjaxRequest=9&json=" & Uri.EscapeDataString(jsonPayload)
    End Function

    ' ----------------------------------------------------------------
    '  Form Load
    ' ----------------------------------------------------------------
    Private Sub FrmMasterAuditSalesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FromDate.EditValue = Date.Today
            ToDate.EditValue = Date.Today
            InitGrids()
        Catch ex As Exception
        End Try
    End Sub

    ' ----------------------------------------------------------------
    '  Initialise all three grids — read-only, font 10, header bold, row height
    ' ----------------------------------------------------------------
    Private Sub InitGrids()
        Dim rowFont As New System.Drawing.Font("Tahoma", 10)
        Dim headerFont As New System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold)

        For Each gv As DevExpress.XtraGrid.Views.Grid.GridView In {GridViewHeader, GridViewDetail, GridViewPaymode}
            gv.OptionsBehavior.Editable = False
            gv.OptionsView.ShowGroupPanel = True
            gv.OptionsView.ShowAutoFilterRow = True
            gv.OptionsView.ShowFooter = True
            gv.OptionsView.EnableAppearanceEvenRow = True
            gv.OptionsView.EnableAppearanceOddRow = True
            gv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            gv.RowHeight = 24

            ' Row text — font + center alignment
            gv.Appearance.Row.Font = rowFont
            gv.Appearance.Row.Options.UseFont = True
            gv.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gv.Appearance.Row.Options.UseTextOptions = True

            ' Even / odd rows inherit center alignment
            gv.Appearance.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gv.Appearance.EvenRow.Options.UseTextOptions = True
            gv.Appearance.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gv.Appearance.OddRow.Options.UseTextOptions = True

            ' Column header — bold + center
            gv.Appearance.HeaderPanel.Font = headerFont
            gv.Appearance.HeaderPanel.Options.UseFont = True
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gv.Appearance.HeaderPanel.Options.UseTextOptions = True

            ' Footer — bold so totals stand out
            gv.Appearance.FooterPanel.Font = headerFont
            gv.Appearance.FooterPanel.Options.UseFont = True
        Next
    End Sub

    ' ----------------------------------------------------------------
    '  Add Sum summary to the named amount columns on the given GridView
    ' ----------------------------------------------------------------
    Private Sub ApplyTotalAmtSummary(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView,
                                     ByVal ParamArray amtColumns As String())
        For Each colName As String In amtColumns
            Dim col As DevExpress.XtraGrid.Columns.GridColumn = gv.Columns(colName)
            If col IsNot Nothing Then
                col.Summary.Clear()
                col.Summary.Add(DevExpress.Data.SummaryItemType.Sum, colName, "Total: {0:n2}")
                col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                col.DisplayFormat.FormatString = "n2"
                ' Amount columns right-aligned
                col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                col.AppearanceCell.Options.UseTextOptions = True
                col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                col.AppearanceHeader.Options.UseTextOptions = True
            End If
        Next
    End Sub

    ' ----------------------------------------------------------------
    '  Core data-fetch: calls AjaxRequest 9
    ' ----------------------------------------------------------------
    Private Function FetchData(ByVal mode As String) As DataTable
        Dim dtFrom As DateTime = DateTime.Today
        Dim dtTo As DateTime = DateTime.Today
        Try
            dtFrom = Convert.ToDateTime(FromDate.EditValue)
            dtTo = Convert.ToDateTime(ToDate.EditValue)
        Catch
        End Try

        Dim payload As String = JsonConvert.SerializeObject(New With {
            .Mode = mode,
            .FromDate = dtFrom.ToString("yyyy-MM-dd"),
            .ToDate = dtTo.ToString("yyyy-MM-dd"),
            .ComId = _companyInfo.ComId,
            .LocId = _companyInfo.LocId
        })

        Dim url As String = BuildTaxAuditUrl(payload)
        If String.IsNullOrWhiteSpace(url) Then
            MessageBox.Show("Tax audit URL is not configured.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End If

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim response As String = New WebClient().DownloadString(url)
        Dim obj As JObject = JObject.Parse(response)

        Dim isOk As Boolean = False
        If obj("Success") IsNot Nothing Then Boolean.TryParse(obj("Success").ToString(), isOk)

        If Not isOk Then
            Dim msg As String = If(obj("Msg") IsNot Nothing, obj("Msg").ToString(), "Failed to load data.")
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End If

        Dim dataArr As JArray = Nothing
        If obj("Data") IsNot Nothing AndAlso obj("Data").Type = JTokenType.Array Then
            dataArr = CType(obj("Data"), JArray)
        End If

        If dataArr Is Nothing OrElse dataArr.Count = 0 Then Return New DataTable()

        ' Convert JArray → DataTable
        Dim dt As New DataTable()
        Dim first As JObject = CType(dataArr(0), JObject)
        For Each prop As JProperty In first.Properties()
            dt.Columns.Add(prop.Name)
        Next

        For Each item As JObject In dataArr
            Dim row As DataRow = dt.NewRow()
            For Each col As DataColumn In dt.Columns
                row(col.ColumnName) = If(item(col.ColumnName) IsNot Nothing, item(col.ColumnName).ToString(), "")
            Next
            dt.Rows.Add(row)
        Next

        Return dt
    End Function

    ' ----------------------------------------------------------------
    '  Load Header (AjaxRequest 9, Mode=Header)
    ' ----------------------------------------------------------------
    Private Sub barbtnHeader_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnHeader.ItemClick
        Try
            Cursor = Cursors.WaitCursor
            Dim dt As DataTable = FetchData("Header")
            If dt IsNot Nothing Then
                GridControlHeader.DataSource = dt
                ApplyTotalAmtSummary(GridViewHeader, "TotalAmt")
                GridViewHeader.BestFitColumns()
                XtraTabControl1.SelectedTabPage = tabHeader
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ' ----------------------------------------------------------------
    '  Load Detail (AjaxRequest 9, Mode=Detail)
    ' ----------------------------------------------------------------
    Private Sub barbtnDetail_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnDetail.ItemClick
        Try
            Cursor = Cursors.WaitCursor
            Dim dt As DataTable = FetchData("Detail")
            If dt IsNot Nothing Then
                GridControlDetail.DataSource = dt
                ApplyTotalAmtSummary(GridViewDetail, "TotalAmt")
                GridViewDetail.BestFitColumns()
                XtraTabControl1.SelectedTabPage = tabDetail
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ' ----------------------------------------------------------------
    '  Load Paymode (AjaxRequest 9, Mode=Paymode)
    ' ----------------------------------------------------------------
    Private Sub barbtnPaymode_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnPaymode.ItemClick
        Try
            Cursor = Cursors.WaitCursor
            Dim dt As DataTable = FetchData("Paymode")
            If dt IsNot Nothing Then
                GridControlPaymode.DataSource = dt
                ApplyTotalAmtSummary(GridViewPaymode, "Amount")
                GridViewPaymode.BestFitColumns()
                XtraTabControl1.SelectedTabPage = tabPaymode
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ' ----------------------------------------------------------------
    '  Export — exports the active tab's grid to xlsx / csv
    ' ----------------------------------------------------------------
    Private Sub barbtnExport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnExport.ItemClick
        Try
            Dim activeView As DevExpress.XtraGrid.Views.Grid.GridView = ActiveGridView()
            If activeView Is Nothing Then Exit Sub

            Dim dlg As New SaveFileDialog()
            dlg.Filter = "Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
            dlg.DefaultExt = "xlsx"
            dlg.FileName = "TaxAuditReport_" & ActiveTabName() & "_" & DateTime.Now.ToString("yyyyMMdd_HHmmss")

            If dlg.ShowDialog() = DialogResult.OK Then
                Dim ext As String = System.IO.Path.GetExtension(dlg.FileName).ToLower()
                If ext = ".xlsx" Then
                    activeView.ExportToXlsx(dlg.FileName)
                ElseIf ext = ".csv" Then
                    activeView.ExportToCsv(dlg.FileName)
                End If
                MessageBox.Show("Exported successfully to:" & vbCrLf & dlg.FileName,
                                "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Export error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ----------------------------------------------------------------
    '  Print — print-preview of the active tab's grid
    ' ----------------------------------------------------------------
    Private Sub barbtnPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnPrint.ItemClick
        Try
            Dim activeControl As DevExpress.XtraGrid.GridControl = ActiveGridControl()
            If activeControl Is Nothing Then Exit Sub

            Dim ps As New PrintingSystem()
            Dim link As New PrintableComponentLink(ps)
            link.Component = activeControl

            Dim phf As PageHeaderFooter = CType(link.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()
            phf.Footer.Content.Clear()

            Dim fromDt As String = ""
            Dim toDt As String = ""
            Try
                fromDt = Convert.ToDateTime(FromDate.EditValue).ToString("dd/MM/yyyy")
                toDt = Convert.ToDateTime(ToDate.EditValue).ToString("dd/MM/yyyy")
            Catch
            End Try

            phf.Header.Content.AddRange(New String() {
                "",
                M_Details._shopName & vbCrLf & "TAX AUDIT REPORT - " & ActiveTabName().ToUpper() &
                vbCrLf & "From: " & fromDt & "  To: " & toDt,
                "Page [Page # of Pages #]"
            })
            phf.Header.Font = New System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold)
            phf.Footer.Content.AddRange(New String() {
                "",
                "Printed on: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                ""
            })

            link.CreateDocument()
            link.ShowPreviewDialog()
        Catch ex As Exception
            MessageBox.Show("Print error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ----------------------------------------------------------------
    '  Helpers: return the active tab's GridView / GridControl / name
    ' ----------------------------------------------------------------
    Private Function ActiveGridView() As DevExpress.XtraGrid.Views.Grid.GridView
        If XtraTabControl1.SelectedTabPage Is tabHeader Then Return GridViewHeader
        If XtraTabControl1.SelectedTabPage Is tabDetail Then Return GridViewDetail
        If XtraTabControl1.SelectedTabPage Is tabPaymode Then Return GridViewPaymode
        Return Nothing
    End Function

    Private Function ActiveGridControl() As DevExpress.XtraGrid.GridControl
        If XtraTabControl1.SelectedTabPage Is tabHeader Then Return GridControlHeader
        If XtraTabControl1.SelectedTabPage Is tabDetail Then Return GridControlDetail
        If XtraTabControl1.SelectedTabPage Is tabPaymode Then Return GridControlPaymode
        Return Nothing
    End Function

    Private Function ActiveTabName() As String
        If XtraTabControl1.SelectedTabPage Is tabHeader Then Return "Header"
        If XtraTabControl1.SelectedTabPage Is tabDetail Then Return "Detail"
        If XtraTabControl1.SelectedTabPage Is tabPaymode Then Return "Paymode"
        Return "Report"
    End Function

End Class
