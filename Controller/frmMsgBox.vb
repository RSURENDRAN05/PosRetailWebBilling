Imports PosRetailWebBilling.clssalesProperty

Public Class frmMsgBox

    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Try
            properClass.R_YesOrNo = "Yes"
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        Try
            properClass.R_YesOrNo = "No"
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmMsgBox_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            lblmsg.Text = properClass.R_Msgstring
            properClass.R_Msgstring = ""
        Catch ex As Exception

        End Try
    End Sub
End Class