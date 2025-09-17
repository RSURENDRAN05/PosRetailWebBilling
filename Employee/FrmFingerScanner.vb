Public Class FrmFingerScanner 
    Private Sub DataLoad()
        Try
            lblimagestatus.Image = Img.Images(0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FrmFingerScanner_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DataLoad()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TimerAtten_Tick(sender As Object, e As EventArgs) Handles TimerAtten.Tick
        Try
            lblTimer.Text = DateTime.Now
        Catch ex As Exception

        End Try
    End Sub
 
    Private Sub btnmorningin_Click(sender As Object, e As EventArgs) Handles btnmorningin.Click
        Try
            lblmade.Text = btnmorningin.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnbreakout_Click(sender As Object, e As EventArgs) Handles btnbreakout.Click
        Try
            lblmade.Text = btnbreakout.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnbreakin_Click(sender As Object, e As EventArgs) Handles btnbreakin.Click
        Try
            lblmade.Text = btnbreakin.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btneveningout_Click(sender As Object, e As EventArgs) Handles btneveningout.Click
        Try
            lblmade.Text = btneveningout.Text
        Catch ex As Exception

        End Try
    End Sub
End Class