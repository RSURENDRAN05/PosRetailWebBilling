Imports System.IO

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
End Module
