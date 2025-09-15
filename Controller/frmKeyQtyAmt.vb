Imports DevExpress.XtraEditors
Imports PosRetailWebBilling.clssalesProperty

Public Class frmKeyQtyAmt
    Dim numstr As String = ""
    Private Sub btn_7_Click(sender As Object, e As EventArgs) Handles btn_dot.Click, btn_7.Click, btn_8.Click, btn_9.Click, btn_6.Click, btn_5.Click, btn_4.Click, btn_3.Click, btn_2.Click, btn_1.Click, btn_0.Click
        Try
            Dim _btnnum As New SimpleButton
            _btnnum = CType(sender, SimpleButton)
            _TXTPASS.EditValue = _TXTPASS.EditValue + _btnnum.Text.ToString
            numstr = numstr + _btnnum.Text
            '_TXTPASS.EditValue = Format(Val(numstr) / 100, "#####.00")
            _TXTPASS.EditValue = Format(Val(numstr), "#####.00")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_Go_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Try
            If _TXTPASS.EditValue Is Nothing OrElse _TXTPASS.Text = "" Then
                properClass.MKeyQtyAmt = 0.0
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Hide()
            Else
                properClass.MKeyQtyAmt = _TXTPASS.EditValue
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Hide()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_numclear_Click(sender As Object, e As EventArgs) Handles btn_numclear.Click
        Try
            _TXTPASS.Text = ""
            numstr = ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            properClass.MKeyQtyAmt = 0.0
            Me.Hide()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmKeyQtyAmt_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            _TXTPASS.Text = ""
            numstr = ""
            _TXTPASS.SelectAll()
        Catch ex As Exception

        End Try
    End Sub

End Class