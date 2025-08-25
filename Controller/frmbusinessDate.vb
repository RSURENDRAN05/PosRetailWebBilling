Imports PosRetailWebBilling.clssalesProperty

Public Class frmbusinessDate

    Private Sub z_Click(sender As Object, e As EventArgs) Handles z.Click
        Try
            Dim FORMDATE As String = DTE_BEND.Text
            Dim _DATE As DateTime = Convert.ToDateTime(FORMDATE)
            ' MessageBox.Show(_DATE.ToString("yyyy-MM-dd"))
            properClass.R_BusinessDate = _DATE.ToString("yyyy-MM-dd")
            _saleSetting._BusinessDate = _DATE.ToString("yyyy-MM-dd")
            properClass.R_YesOrNo = "Ok"
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BTN_CANCEL_Click(sender As Object, e As EventArgs) Handles BTN_CANCEL.Click
        Try
            properClass.R_BusinessDate = "No"
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmBusinessDate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DTE_BEND.Text = Date.Today.ToString("dd-MM-yyyy")
            'Me.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class