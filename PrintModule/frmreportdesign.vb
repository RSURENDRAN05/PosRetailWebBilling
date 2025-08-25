Imports DevExpress.XtraReports.UI
Public Class frmReportDesign

    Private Sub XrSubreport1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrSubreport1.BeforePrint
        Dim subreport As XRSubreport = DirectCast(sender, XRSubreport)
        Dim report As XtraReport = subreport.ReportSource
    End Sub
End Class