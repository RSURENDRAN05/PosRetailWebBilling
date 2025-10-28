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
    Public Function BillPrintMinPreivew(ByVal ds As DataSet, ByRef ErrorMsg As String, ByRef SalesProfile As String) As Boolean
        Try
            Dim billrpt As New rpta5print

            If File.Exists(M_Details._appPath & "\Reports\" & SalesProfile) Then
                billrpt.LoadLayout(M_Details._appPath & "\Reports\" & SalesProfile)
            End If
            billrpt.DataSource = ds
            billrpt.ShowPrintMarginsWarning = False
            Dim pt As New DevExpress.XtraReports.UI.ReportPrintTool(billrpt)
            pt.ShowPreview()
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
            GetSalesmanData()
            If _JsonData.SalesManDataTable.Rows.Count > 0 Then
                Dim empIds = _JsonData.SalesManDataTable.AsEnumerable().Select(Function(r) Convert.ToString(r("Id"))).Distinct().ToList()
                ' this will hold the final combined report
                Dim finalReport As New XtraReport()
                finalReport.CreateDocument() ' initialize empty document

                For Each empId In empIds
                    ' === Filter SalesmanData ===
                    Dim dtSalesman = dsAdvanceData.Tables("SalesmanData")
                    Dim filtered = dtSalesman.AsEnumerable().Where(Function(r) r.Field(Of Object)("EmpId").ToString() = empId.ToString())
                    If filtered.Any() Then
                        dtSalesman = filtered.CopyToDataTable()
                    Else
                        dtSalesman = dsAdvanceData.Tables("SalesmanData").Clone()
                    End If
                    '' --- NEW FILTER: skip Commission = 0 or NULL ---
                    'If dtSalesman.Rows.Count > 0 Then
                    '    Dim rowsToKeep = dtSalesman.AsEnumerable().
                    '        Where(Function(r)
                    '                  Dim val As Decimal = 0D
                    '                  ' Try safely converting Commission to decimal
                    '                  If Not IsDBNull(r("Commission")) AndAlso Decimal.TryParse(r("Commission").ToString(), val) Then
                    '                      Return val <> 0D  ' Keep only rows with non-zero Commission
                    '                  End If
                    '                  Return False ' Skip NULL or non-numeric
                    '              End Function)

                    '    If rowsToKeep.Any() Then
                    '        dtSalesman = rowsToKeep.CopyToDataTable()
                    '    Else
                    '        dtSalesman = dtSalesman.Clone() ' empty table with same structure
                    '    End If
                    'End If

                    dtSalesman.TableName = "SalesmanData"

                    ' === Filter ItemwiseData ===
                    Dim dtItemwise = dsAdvanceData.Tables("ItemwiseData")
                    Dim filteredItem = dtItemwise.AsEnumerable().
                        Where(Function(r) r.Field(Of Object)("EmpId").ToString() = empId.ToString())

                    If filteredItem.Any() Then
                        dtItemwise = filteredItem.CopyToDataTable()
                    Else
                        dtItemwise = dsAdvanceData.Tables("ItemwiseData").Clone()
                    End If

                    ' --- NEW FILTER: skip Commission = 0 or NULL ---
                    If dtItemwise.Rows.Count > 0 Then
                        Dim rowsToKeep = dtItemwise.AsEnumerable().
                            Where(Function(r)
                                      Dim val As Decimal = 0
                                      If Not IsDBNull(r("Commission")) Then
                                          Decimal.TryParse(r("Commission").ToString(), val)
                                      End If
                                      Return val <> 0D
                                  End Function)

                        If rowsToKeep.Any() Then
                            dtItemwise = rowsToKeep.CopyToDataTable()
                        Else
                            dtItemwise = dtItemwise.Clone()
                        End If
                    End If

                    dtItemwise.TableName = "ItemwiseData"


                    ' === Filter AdvanceData ===
                    Dim dtAdvance = dsAdvanceData.Tables("AdvanceData")
                    Dim filteredAdvance = dtAdvance.AsEnumerable().Where(Function(r) r.Field(Of Object)("EmpId").ToString() = empId.ToString())
                    If filteredAdvance.Any() Then
                        dtAdvance = filteredAdvance.CopyToDataTable()
                    Else
                        dtAdvance = dsAdvanceData.Tables("ItemwiseData").Clone()
                    End If
                    dtAdvance.TableName = "AdvanceData"

                    ' === Filter AdvanceData ===
                    Dim dtAdvancedtl = dsAdvanceData.Tables("AdvanceDataDtl")
                    Dim filteredAdvanceDtl = dtAdvancedtl.AsEnumerable().Where(Function(r) r.Field(Of Object)("EmpId").ToString() = empId.ToString())
                    If filteredAdvanceDtl.Any() Then
                        dtAdvancedtl = filteredAdvanceDtl.CopyToDataTable()
                    Else
                        dtAdvancedtl = dsAdvanceData.Tables("ItemwiseData").Clone()
                    End If
                    dtAdvancedtl.TableName = "AdvanceDataDtl"
                     
                    ' === Create new dataset for this salesman ===
                    Dim dsFiltered As New DataSet()
                    dsFiltered.Tables.Add(dtSalesman)
                    dsFiltered.Tables.Add(dtItemwise)
                    dsFiltered.Tables.Add(dtAdvance)
                    dsFiltered.Tables.Add(dtAdvancedtl)

                    ' === Check if any data exists before creating report ===
                    Dim hasData As Boolean = dsFiltered.Tables.Cast(Of DataTable)().Any(Function(t) t.Rows.Count > 0)

                    If hasData Then
                        ' Optional: Save XML for debugging
                        dsFiltered.WriteXml(M_Details._appPath & "\Reports\ReportMonthlyIndv.xml", Data.XmlWriteMode.WriteSchema)

                        ' === Load report ===
                        Dim rpt As New Billing_rpt()
                        rpt.DataSource = dsFiltered
                        rpt.DataMember = "SalesmanData" '"ReportData"

                        If File.Exists(M_Details._appPath & "\Reports\" & SalesProfile) Then
                            rpt.LoadLayout(M_Details._appPath & "\Reports\" & SalesProfile)
                        End If

                        ' === Generate pages for this salesman ===
                        rpt.CreateDocument()
                        finalReport.Pages.AddRange(rpt.Pages)
                    Else
            
                    End If
                Next

                ' === Show final combined report ===
                If finalReport.Pages.Count > 0 Then
                    Dim printTool As New ReportPrintTool(finalReport)
                    printTool.ShowPreviewDialog()
                Else
                    MessageBox.Show("No data found for any employee.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
            Return True
        Catch ex As Exception
            MsgBox("GetInvoiceProblem : " & ex.Message)
            Return False
        End Try
    End Function
End Module
