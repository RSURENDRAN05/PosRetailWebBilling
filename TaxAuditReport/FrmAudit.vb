Public Class FrmAudit

    Private Sub FrmAudit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FromDate.EditValue = Date.Now
            ToDate.EditValue = Date.Now
        Catch ex As Exception

        End Try
    End Sub

  
End Class