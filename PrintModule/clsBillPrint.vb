Imports System.IO
Imports DevExpress.XtraReports.UI

Module clsBillPrint
    Dim _casdr As New RawPrinter
    Public Function Billa4Print(ByVal ds As DataSet, ByRef ErrorMsg As String, ByRef SalesProfile As String) As Boolean
        Try
            Dim billrpt As New Billing_rpt

            If File.Exists(M_Details._appPath & "\Reports\" & SalesProfile) Then
                billrpt.LoadLayout(M_Details._appPath & "\Reports\" & SalesProfile)
            End If
            billrpt.DataSource = ds
            billrpt.ShowPrintMarginsWarning = False
            Dim pt As New DevExpress.XtraReports.UI.ReportPrintTool(billrpt)
            pt.ShowPreviewDialog()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function BillPrintMin(ByVal ds As DataSet, ByRef ErrorMsg As String, ByRef SalesProfile As String) As Boolean
        Try
            Dim billrpt As New rpta5print

            If File.Exists(M_Details._appPath & "\Reports\" & SalesProfile) Then
                billrpt.LoadLayout(M_Details._appPath & "\Reports\" & SalesProfile)
            End If
            billrpt.DataSource = ds
            billrpt.ShowPrintMarginsWarning = False
            Dim pt As New DevExpress.XtraReports.UI.ReportPrintTool(billrpt)
            pt.Print("MYPOS")
            _casdr._paperCut(True)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GenrateReportA4Print(ByVal ds As DataSet, ByRef ErrorMsg As String, ByRef SalesProfile As String) As Boolean
        Try
            Dim billrpt As New Billing_rpt

            If File.Exists(M_Details._appPath & "\Reports\" & SalesProfile) Then
                billrpt.LoadLayout(M_Details._appPath & "\Reports\" & SalesProfile)
            End If
            billrpt.DataSource = ds
            billrpt.ShowPrintMarginsWarning = False
            Dim pt As New DevExpress.XtraReports.UI.ReportPrintTool(billrpt)
            pt.ShowPreviewDialog()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GenrateMonthlyReportA4Print(dsAdvanceData As DataSet, ByRef ErrorMsg As String, ByRef SalesProfile As String) As Boolean
        Try
            ' collect distinct salesman EmpId list
            If _JsonData.SalesManDataTable.Rows.Count > 0 Then
                Dim empIds = _JsonData.SalesManDataTable.AsEnumerable().Select(Function(r) Convert.ToString(r("Id"))).Distinct().ToList()
                ' this will hold the final combined report
                Dim finalReport As New XtraReport()
                finalReport.CreateDocument() ' initialize empty document

                For Each empId In empIds
                    ' === Filter SalesmanData ===
                    Dim dtSalesman As DataTable
                    Dim rowsSalesman = dsAdvanceData.Tables("SalesmanData").Select("EmpId=" & empId)
                    If rowsSalesman.Length > 0 Then
                        dtSalesman = rowsSalesman.CopyToDataTable()
                    Else
                        dtSalesman = dsAdvanceData.Tables("SalesmanData").Clone()
                    End If
                    dtSalesman.TableName = "SalesmanData"

                    ' === Filter ItemwiseData ===
                    Dim dtItemwise As DataTable
                    Dim rowsItemwise = dsAdvanceData.Tables("ItemwiseData").Select("EmpId=" & empId)
                    If rowsItemwise.Length > 0 Then
                        dtItemwise = rowsItemwise.CopyToDataTable()
                    Else
                        dtItemwise = dsAdvanceData.Tables("ItemwiseData").Clone()
                    End If
                    dtItemwise.TableName = "ItemwiseData"

                    ' === Filter AdvanceData ===
                    Dim dtAdvance As DataTable
                    Dim rowsAdvance = dsAdvanceData.Tables("AdvanceData").Select("EmpId=" & empId)
                    If rowsAdvance.Length > 0 Then
                        dtAdvance = rowsAdvance.CopyToDataTable()
                    Else
                        dtAdvance = dsAdvanceData.Tables("AdvanceData").Clone()
                    End If
                    dtAdvance.TableName = "AdvanceData"

                    ' === Create new dataset for this salesman ===
                    Dim dsFiltered As New DataSet()
                    dsFiltered.Tables.Add(dtSalesman)
                    dsFiltered.Tables.Add(dtItemwise)
                    dsFiltered.Tables.Add(dtAdvance)
                    If (dsFiltered.Tables(0).Rows.Count > 0) Then
                        dsFiltered.WriteXml(M_Details._appPath & "\Reports\ReportMonthlyIndv.xml", Data.XmlWriteMode.WriteSchema)
                    End If
                    ' === Load report ===
                    Dim rpt As New Billing_rpt()
                    rpt.DataSource = dsFiltered
                    rpt.DataMember = "SalesmanData" ' main datasource
                    If File.Exists(M_Details._appPath & "\Reports\" & SalesProfile) Then
                        rpt.LoadLayout(M_Details._appPath & "\Reports\" & SalesProfile)
                    End If
                    ' === Generate pages for this salesman ===
                    rpt.CreateDocument()

                    ' append to finalReport
                    finalReport.Pages.AddRange(rpt.Pages)
                Next
                ' === Show in ReportViewer ===
                Dim printTool As New ReportPrintTool(finalReport)
                printTool.ShowPreviewDialog()
            End If
            Return True
        Catch ex As Exception
            Return False
            MsgBox("GetInvoiceProblem : " & ex.Message)
        End Try
    End Function
End Module
