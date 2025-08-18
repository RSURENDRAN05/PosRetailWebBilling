Imports System.Windows.Forms

Module TestPosForm

    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        ' Create and show the POS form
        Dim frmPos As New frmPosRetailBilling()
        Application.Run(frmPos)
    End Sub

End Module
