Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Text
Imports DevExpress.XtraPrinting

Public Class FrmAudit
    Dim rtb As New RichTextBox
    Private Sub FrmAudit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FromDate.EditValue = Date.Now
            ToDate.EditValue = Date.Now
            txtrichreport.Font = New System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold)
        Catch ex As Exception

        End Try
    End Sub

    Private Function BuildTaxAuditUrl(ByVal ajaxRequest As Integer, ByVal jsonPayload As String) As String
        Dim baseUrl As String = M_Details.LinkTaxAuditRequest
        If String.IsNullOrWhiteSpace(baseUrl) Then
            baseUrl = M_Details.LinkAjaxRequest
        End If
        If String.IsNullOrWhiteSpace(baseUrl) Then
            Return String.Empty
        End If
        If baseUrl.Contains("?") Then
            If Not baseUrl.EndsWith("?") AndAlso Not baseUrl.EndsWith("&") Then
                baseUrl &= "&"
            End If
        Else
            If Not baseUrl.EndsWith("?") Then
                baseUrl &= "?"
            End If
        End If
        Return baseUrl & "AjaxRequest=" & ajaxRequest.ToString() & "&json=" & Uri.EscapeDataString(jsonPayload)
    End Function


    Private Sub barbtnbyDay_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnbyDay.ItemClick
        Try
            Dim selectedDate As DateTime = DateTime.Now
            Try
                selectedDate = Convert.ToDateTime(FromDate.EditValue)
            Catch
            End Try

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim payload = New With {
                .Date = selectedDate.ToString("yyyy-MM-dd"),
                .ComId = _companyInfo.ComId,
                .LocId = _companyInfo.LocId
            }

            Dim jsonPayload As String = JsonConvert.SerializeObject(payload)
            Dim url As String = BuildTaxAuditUrl(7, jsonPayload)

            If String.IsNullOrWhiteSpace(url) Then
                MessageBox.Show("Tax audit URL is empty. Configure UrlLinkTaxAudit in settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim response As String = New WebClient().DownloadString(url)
            Dim responseObj As JObject = JObject.Parse(response)

            Dim isSuccess As Boolean = False
            If responseObj("Success") IsNot Nothing Then
                Boolean.TryParse(responseObj("Success").ToString(), isSuccess)
            End If

            If Not isSuccess Then
                Dim msg As String = If(responseObj("Msg") IsNot Nothing, responseObj("Msg").ToString(), "Failed to load data.")
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtrichreport.Text = ""
                Exit Sub
            End If

            Dim payModeArr As JArray = Nothing
            If responseObj("PayMode") IsNot Nothing AndAlso responseObj("PayMode").Type = JTokenType.Array Then
                payModeArr = CType(responseObj("PayMode"), JArray)
            End If

            Dim dataArr As JArray = Nothing
            If responseObj("Data") IsNot Nothing AndAlso responseObj("Data").Type = JTokenType.Array Then
                dataArr = CType(responseObj("Data"), JArray)
            End If

            BuildReportDay(selectedDate, dataArr, payModeArr)

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub barbtnbymonth_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnbymonth.ItemClick
        Try
            Dim selectedDate As DateTime = DateTime.Now
            Try
                selectedDate = Convert.ToDateTime(FromDate.EditValue)
            Catch
            End Try

            Dim selectedYear As Integer = selectedDate.Year
            Dim selectedMonth As Integer = selectedDate.Month

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim payload = New With {
                .Year = selectedYear,
                .Month = selectedMonth,
                .ComId = _companyInfo.ComId,
                .LocId = _companyInfo.LocId
            }

            Dim jsonPayload As String = JsonConvert.SerializeObject(payload)
            Dim url As String = BuildTaxAuditUrl(8, jsonPayload)

            If String.IsNullOrWhiteSpace(url) Then
                MessageBox.Show("Tax audit URL is empty. Configure UrlLinkTaxAudit in settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim response As String = New WebClient().DownloadString(url)
            Dim responseObj As JObject = JObject.Parse(response)

            Dim isSuccess As Boolean = False
            If responseObj("Success") IsNot Nothing Then
                Boolean.TryParse(responseObj("Success").ToString(), isSuccess)
            End If

            If Not isSuccess Then
                Dim msg As String = If(responseObj("Msg") IsNot Nothing, responseObj("Msg").ToString(), "Failed to load data.")
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtrichreport.Text = ""
                Exit Sub
            End If
            Dim payModeArr As JArray = Nothing
            If responseObj("PayMode") IsNot Nothing AndAlso responseObj("PayMode").Type = JTokenType.Array Then
                payModeArr = CType(responseObj("PayMode"), JArray)
            End If

            Dim dataArr As JArray = Nothing
            If responseObj("Data") IsNot Nothing AndAlso responseObj("Data").Type = JTokenType.Array Then
                dataArr = CType(responseObj("Data"), JArray)
            End If
            BuildReportMonth(selectedDate, dataArr, payModeArr)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Function BuildReportMonth(selectedDate As DateTime, DataFinal As JArray, SalesMethod As JArray) As Boolean
        Try
            Return BuildReport(selectedDate, DataFinal, SalesMethod, True)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function BuildReport(selectedDate As DateTime, DataFinal As JArray, SalesMethod As JArray, isMonthReport As Boolean) As Boolean
        Try
            rtb.Text = ""

            Dim _content As New StringBuilder()

            ' SAFE: keep DateTime as DateTime
            Dim _dates As DateTime = selectedDate.Date

            Dim MontYear As String = MonthName(Month(_dates)) & "-" & Year(_dates).ToString()

            Dim _ItemList As New StringBuilder()
            Dim _EMPTY As String = String.Empty
            Dim _dotline2 As String = New String("="c, 40)
            Dim _title As String = "DATE".PadRight(25) & "TOTSALES(RM)".PadLeft(15)
            Dim _HEAD As String = If(isMonthReport, "Tax Report Month Of- " & MontYear, "Tax Report Date Of- " & _dates.ToString("yyyy-MM-dd"))

            Dim _ShopName As String = _companyInfo.CompanyName
            Dim _LocationName As String = _companyInfo.LocationName
            Dim _Address As String = _companyInfo.LocationAddress

            _content.AppendLine(_HEAD)
            _content.AppendLine(_ShopName)
            _content.AppendLine(_LocationName)
            _content.AppendLine(_Address)
            _content.AppendLine(_EMPTY)
            _content.AppendLine(_dotline2)
            _content.AppendLine(_title)
            _content.AppendLine(_dotline2)

            Dim totTodaySales As Decimal = 0D
            Dim totTaxSales As Decimal = 0D
            Dim totTaxAmt As Decimal = 0D
            Dim totZeroSales As Decimal = 0D
            Dim totFoSales As Decimal = 0D

            If DataFinal IsNot Nothing AndAlso DataFinal.Count > 0 Then
                For Each item As JObject In DataFinal
                    Dim ptfDate As String = ""
                    If item("ptf_date") IsNot Nothing Then
                        Try
                            ptfDate = Convert.ToDateTime(item("ptf_date").ToString()).ToString("yyyy-MM-dd")
                        Catch
                            ptfDate = item("ptf_date").ToString()
                        End Try
                    End If

                    Dim todaySales As Decimal = If(item("ptf_todaysales") IsNot Nothing, CDec(item("ptf_todaysales")), 0D)
                    Dim taxSales As Decimal = If(item("ptf_taxsales") IsNot Nothing, CDec(item("ptf_taxsales")), 0D)
                    Dim taxAmt As Decimal = If(item("ptf_taxamt") IsNot Nothing, CDec(item("ptf_taxamt")), 0D)
                    Dim zeroSales As Decimal = If(item("ptf_zerosales") IsNot Nothing, CDec(item("ptf_zerosales")), 0D)
                    Dim foSales As Decimal = If(item("ptf_fosales") IsNot Nothing, CDec(item("ptf_fosales")), 0D)

                    totTodaySales += todaySales
                    totTaxSales += taxSales
                    totTaxAmt += taxAmt
                    totZeroSales += zeroSales
                    totFoSales += foSales

                    Dim rowLine As String =
                        ptfDate.PadRight(25) & todaySales.ToString("0.00").PadLeft(15)

                    _ItemList.AppendLine(rowLine)
                Next
            End If

            _content.AppendLine(_ItemList.ToString())
            _content.AppendLine(_dotline2)
            _content.AppendLine("TOTAL AMOUNT:".PadRight(25) & totTodaySales.ToString("0.00").PadLeft(15))
            _content.AppendLine(_dotline2)
            _content.AppendLine(_EMPTY)

            _content.AppendLine("**********Groupwise Sales Method *******")
            _content.AppendLine(_dotline2)
            Dim _itemRowList As String = String.Empty
            Dim _sumSalemethod As Decimal = 0D

            If SalesMethod IsNot Nothing AndAlso SalesMethod.Count > 0 Then
                For Each item As JObject In SalesMethod
                    Dim payModeName As String = If(item("PayModeType") IsNot Nothing, item("PayModeType").ToString(), "")
                    Dim netAmt As Decimal = If(item("NetAmt") IsNot Nothing, CDec(item("NetAmt")), 0D)
                    _itemRowList = payModeName.PadRight(25) & netAmt.ToString("0.00").PadLeft(15)
                    _content.AppendLine(_itemRowList)
                    _sumSalemethod += netAmt
                Next

                _content.AppendLine(_dotline2)
                _content.AppendLine("Total Sales Amount :".PadRight(25) & _sumSalemethod.ToString("0.00").PadLeft(15))
                _content.AppendLine(_dotline2)
            End If

            rtb.AppendText(_content.ToString())
            txtrichreport.Text = ""
            txtrichreport.Text = rtb.Text

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function BuildReportDay(selectedDate As DateTime, DataFinal As JArray, SalesMethod As JArray) As Boolean
        Try
            Return BuildReport(selectedDate, DataFinal, SalesMethod, False)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub barbtnExport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnExport.ItemClick
        Try
            If String.IsNullOrWhiteSpace(txtrichreport.Text) Then
                MessageBox.Show("No report data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim dlg As New SaveFileDialog()
            dlg.Filter = "PDF Files (*.pdf)|*.pdf"
            dlg.DefaultExt = "pdf"
            dlg.FileName = "TaxReport_" & DateTime.Now.ToString("yyyyMMdd_HHmmss")

            If dlg.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim ps As New PrintingSystem()
            ps.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4
            ps.PageSettings.Landscape = False
            ps.PageSettings.LeftMargin = 30
            ps.PageSettings.RightMargin = 30
            ps.PageSettings.TopMargin = 30
            ps.PageSettings.BottomMargin = 30

            Dim link As New Link(ps)

            Dim reportLines As String() = txtrichreport.Lines

            AddHandler link.CreateDetailArea, Sub(s2 As Object, ev As CreateAreaEventArgs)
                                                  Dim monoFont As New System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold)
                                                  Dim rowHeight As Single = 12.0F
                                                  Dim colWidth As Single = ev.Graph.ClientPageSize.Width
                                                  Dim yPos As Single = 0

                                                  For Each line As String In reportLines
                                                      Dim tb As New TextBrick()
                                                      tb.Text = If(String.IsNullOrEmpty(line), " ", line)
                                                      tb.Font = monoFont
                                                      tb.BackColor = System.Drawing.Color.White
                                                      tb.ForeColor = System.Drawing.Color.Black
                                                      tb.Style.BorderWidth = 0
                                                      tb.Style.Sides = BorderSide.None
                                                      ev.Graph.DrawBrick(tb, New System.Drawing.RectangleF(0, yPos, colWidth, rowHeight))
                                                      yPos += rowHeight
                                                  Next
                                              End Sub

            link.CreateDocument()
            ps.ExportToPdf(dlg.FileName)

            MessageBox.Show("Exported successfully:" & vbCrLf & dlg.FileName,
                            "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Export error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub barbtnprint_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnprint.ItemClick
        Try
            If String.IsNullOrWhiteSpace(txtrichreport.Text) Then
                MessageBox.Show("No report data to print.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim printLines() As String = txtrichreport.Lines
            Dim currentLine As Integer = 0

            Dim pd As New System.Drawing.Printing.PrintDocument()
            ' Narrow margins suit 80 mm / 58 mm thermal rolls
            pd.DefaultPageSettings.Margins =
                New System.Drawing.Printing.Margins(10, 10, 10, 10)

            AddHandler pd.PrintPage,
                Sub(s2 As Object, ev As System.Drawing.Printing.PrintPageEventArgs)
                    Dim printFont As New System.Drawing.Font(
                        "Arial", 9, System.Drawing.FontStyle.Bold)
                    Dim lineH As Single = printFont.GetHeight(ev.Graphics)
                    Dim yPos As Single = ev.MarginBounds.Top
                    Dim xPos As Single = ev.MarginBounds.Left

                    Do While currentLine < printLines.Length
                        Dim text As String =
                            If(String.IsNullOrEmpty(printLines(currentLine)), " ", printLines(currentLine))
                        ev.Graphics.DrawString(text, printFont,
                                               System.Drawing.Brushes.Black, xPos, yPos)
                        yPos += lineH
                        currentLine += 1

                        ' If next line would overflow the page, signal more pages
                        If yPos + lineH > ev.MarginBounds.Bottom Then
                            ev.HasMorePages = True
                            Return
                        End If
                    Loop

                    ev.HasMorePages = False
                End Sub

            Dim dlg As New PrintDialog()
            dlg.Document = pd
            dlg.AllowSomePages = False
            dlg.AllowPrintToFile = False

            If dlg.ShowDialog() = DialogResult.OK Then
                pd.Print()
            End If

        Catch ex As Exception
            MessageBox.Show("Print error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class