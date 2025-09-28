Imports DevExpress.XtraEditors

Public Class frmkeytablescaner

    Private Sub btn_7_Click(sender As Object, e As EventArgs) Handles btn_dot.Click, btn_7.Click, btn_8.Click, btn_9.Click, btn_6.Click, btn_5.Click, btn_4.Click, btn_3.Click, btn_2.Click, btn_1.Click, btn_0.Click
        Try
            Dim _btnnum As New SimpleButton
            _btnnum = CType(sender, SimpleButton)
            _TXTPASS.EditValue = _TXTPASS.EditValue + _btnnum.Text.ToString

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_Go_Click(sender As Object, e As EventArgs) Handles btn_Go.Click
        Try
            If _TXTPASS.EditValue Is Nothing OrElse _TXTPASS.Text = "" Then
                _FunctionKeyBoardModule.gs_keyboardValueInteger = 0
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
            Else
                _FunctionKeyBoardModule.gs_keyboardValueInteger = _TXTPASS.EditValue
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_numclear_Click(sender As Object, e As EventArgs) Handles btn_numclear.Click
        Try
            _TXTPASS.Text = ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try

            Me.Hide()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub frmkeytablescaner_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            _TXTPASS.Text = ""
            _TXTPASS.Select()
            ' _TXTPASS.EditValue = _functionModule.MKeyQtyAmt
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _TXTPASS_KeyDown(sender As Object, e As KeyEventArgs) Handles _TXTPASS.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If _TXTPASS.EditValue Is Nothing OrElse _TXTPASS.Text = "" Then
                    _FunctionKeyBoardModule.gs_keyboardValueInteger = 0
                    Me.DialogResult = Windows.Forms.DialogResult.Cancel
                    Me.Close()
                Else
                    _FunctionKeyBoardModule.gs_keyboardValueInteger = _TXTPASS.EditValue
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class