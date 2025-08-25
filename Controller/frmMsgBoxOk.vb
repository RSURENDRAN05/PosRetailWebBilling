Imports PosRetailWebBilling.clssalesProperty

Public Class frmMsgBoxOk

    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Try
            properClass.R_YesOrNo = "Ok"
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmMsgBox_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            lblmsg.Text = properClass.R_Msgstring
        Catch ex As Exception

        End Try
    End Sub

    Private Sub timerClose_Tick(sender As Object, e As EventArgs) Handles timerClose.Tick
        Try
            properClass.R_Msgstring = ""
            properClass.R_YesOrNo = "Ok"
            Me.Close()
            timerClose.Enabled = False
        Catch ex As Exception

        End Try
    End Sub
End Class