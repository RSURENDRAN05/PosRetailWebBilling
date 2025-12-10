Imports PosRetailWebBilling.clssalesProperty

Public Class frmMsgBox
    Dim _msg As String = ""
    Public Overloads Sub ShowDialogData(ByVal Msg As String, ByRef printview As Boolean)
        Try
            _msg = Msg
            lblmsg.Text = _msg
            If printview = False Then
                btnprint.Visible = printview
            End If
            MyBase.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Yes
            'properClass.R_YesOrNo = "Yes"
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.No
            'properClass.R_YesOrNo = "No"
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub frmMsgBox_Load(sender As Object, e As EventArgs) Handles Me.Load
    '    Try
    '        lblmsg.Text = properClass.R_Msgstring
    '        properClass.R_Msgstring = ""
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class