Public Class frmstatusoption
    Dim _Selindex As Integer = 0
    Public Overloads Sub ShowDialog(ByRef selectIndex As Integer)
        Try
            _Selindex = selectIndex
            RadioGroup1.SelectedIndex = _Selindex
            MyBase.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmstatusoption_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            RadioGroup1.SelectedIndex = 2
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class