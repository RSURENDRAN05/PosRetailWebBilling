Imports DevExpress.XtraEditors

Public Class frmKeyQty
    Dim numstr As String = ""
    Dim Title As String = "Enter Qty"
    Public Overloads Sub ShowDialog(ByVal _title As String)
        Title = _title
        _TXTPASS.Text = ""
        _TXTPASS.Select()
        MyBase.ShowDialog()
    End Sub
    Private Sub btn_7_Click(sender As Object, e As EventArgs) Handles btn_dot.Click, btn_7.Click, btn_8.Click, btn_9.Click, btn_6.Click, btn_5.Click, btn_4.Click, btn_3.Click, btn_2.Click, btn_1.Click, btn_0.Click
        Try
            Dim _btnnum As New SimpleButton
            _btnnum = CType(sender, SimpleButton)
            _TXTPASS.EditValue = _TXTPASS.EditValue + _btnnum.Text.ToString
            '    numstr = numstr + _btnnum.Text
            '    _TXTPASS.EditValue = Format(Val(numstr) / 100, "#####.00")
        Catch ex As Exception

        End Try
    End Sub
 
  

    Private Sub btn_numclear_Click(sender As Object, e As EventArgs) Handles btn_numclear.Click
        Try
            _TXTPASS.Text = ""
        Catch ex As Exception

        End Try
    End Sub

   

    Private Sub _TXTPASS_KeyDown(sender As Object, e As KeyEventArgs) Handles _TXTPASS.KeyDown
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

  

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
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
 
End Class