Imports DevExpress.XtraEditors
Imports PosRetailWebBilling.clssalesProperty

Public Class frmDeno
    Dim _TxtKeyStatus As String
    Dim _netamt As Double = 0.0
    Dim _finam As Double = 0.0
    Dim _str(9) As Double
    Private Sub btn_7_Click(sender As Object, e As EventArgs) Handles btn_7.Click, btn_8.Click, btn_9.Click, btn_6.Click, btn_5.Click, btn_4.Click, btn_3.Click, btn_2.Click, btn_1.Click, btn_0.Click
        Try
            Dim num As New SimpleButton
            num = CType(sender, SimpleButton)
            txtnum.Text = txtnum.Text + num.Text
            txtnum.Select()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btn_ok_Click(sender As Object, e As EventArgs) Handles btn_ok.Click
        Try
            Select Case _TxtKeyStatus
                Case "100"
                    TextEdit1.Text = txtnum.Text
                    txtnum.Text = ""
                Case "50"
                    TextEdit2.Text = txtnum.Text
                    txtnum.Text = ""
                Case "20"
                    TextEdit3.Text = txtnum.Text
                    txtnum.Text = ""
                Case "10"
                    TextEdit4.Text = txtnum.Text
                    txtnum.Text = ""
                Case "05"
                    TextEdit5.Text = txtnum.Text
                    txtnum.Text = ""
                Case "0.50"
                    TextEdit9.Text = txtnum.Text
                    txtnum.Text = ""
                Case "0.20"
                    TextEdit8.Text = txtnum.Text
                    txtnum.Text = ""
                Case "0.10"
                    TextEdit7.Text = txtnum.Text
                    txtnum.Text = ""
                Case "0.05"
                    TextEdit6.Text = txtnum.Text
                    txtnum.Text = ""
                Case "1.00"
                    TextEdit10.Text = txtnum.Text
                    txtnum.Text = ""
                Case Nothing
                Case ""


            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextEdit1_Click(sender As Object, e As EventArgs) Handles TextEdit1.Click
        Try
            _TxtKeyStatus = "100"
            txtnum.Select()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit2_Click(sender As Object, e As EventArgs) Handles TextEdit2.Click
        Try
            _TxtKeyStatus = "50"
            txtnum.Select()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit3_Click(sender As Object, e As EventArgs) Handles TextEdit3.Click
        Try
            _TxtKeyStatus = "20"
            txtnum.Select()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit4_Click(sender As Object, e As EventArgs) Handles TextEdit4.Click
        Try
            _TxtKeyStatus = "10"
            txtnum.Select()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit5_Click(sender As Object, e As EventArgs) Handles TextEdit5.Click
        Try
            _TxtKeyStatus = "05"
            txtnum.Select()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit6_Click(sender As Object, e As EventArgs) Handles TextEdit6.Click
        Try
            _TxtKeyStatus = "0.05"
            txtnum.Select()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit7_Click(sender As Object, e As EventArgs) Handles TextEdit7.Click
        Try
            _TxtKeyStatus = "0.10"
            txtnum.Select()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit8_Click(sender As Object, e As EventArgs) Handles TextEdit8.Click
        Try
            _TxtKeyStatus = "0.20"
            txtnum.Select()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit9_Click(sender As Object, e As EventArgs) Handles TextEdit9.Click
        Try
            _TxtKeyStatus = "0.50"
            txtnum.Select()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit10_Click(sender As Object, e As EventArgs) Handles TextEdit10.Click
        Try
            _TxtKeyStatus = "1.00"
            txtnum.Select()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged
        Try
            _netamt = Convert.ToDouble(TextEdit1.EditValue)
            _netamt = Convert.ToDouble(_TxtKeyStatus) * _netamt
            _str(0) = _netamt
            Label7.Text = "= $" + _netamt.ToString("0.00").PadLeft(10)
            _fucSum()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextEdit2_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit2.EditValueChanged
        Try
            _netamt = Convert.ToDouble(TextEdit2.EditValue)
            _netamt = Convert.ToDouble(_TxtKeyStatus) * _netamt
            _str(1) = _netamt
            Label6.Text = "= $" + _netamt.ToString("0.00").PadLeft(10)
            _fucSum()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit3_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit3.EditValueChanged
        Try
            _netamt = Convert.ToDouble(TextEdit3.EditValue)
            _netamt = Convert.ToDouble(_TxtKeyStatus) * _netamt
            _str(2) = _netamt
            Label11.Text = "= $" + _netamt.ToString("0.00").PadLeft(10)
            _fucSum()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit4_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit4.EditValueChanged
        Try
            _netamt = Convert.ToDouble(TextEdit4.EditValue)
            _netamt = Convert.ToDouble(_TxtKeyStatus) * _netamt
            _str(3) = _netamt
            Label12.Text = "= $" + _netamt.ToString("0.00").PadLeft(10)
            _fucSum()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit5_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit5.EditValueChanged
        Try
            _netamt = Convert.ToDouble(TextEdit5.EditValue)
            _netamt = Convert.ToDouble(_TxtKeyStatus) * _netamt
            _str(4) = _netamt
            Label13.Text = "= $" + _netamt.ToString("0.00").PadLeft(10)
            _fucSum()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit6_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit6.EditValueChanged
        Try
            _netamt = Convert.ToDouble(TextEdit6.EditValue)
            _netamt = Convert.ToDouble(_TxtKeyStatus) * _netamt
            _str(5) = _netamt
            Label8.Text = "= $" + _netamt.ToString("0.00").PadLeft(10)
            _fucSum()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit7_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit7.EditValueChanged
        Try
            _netamt = Convert.ToDouble(TextEdit7.EditValue)
            _netamt = Convert.ToDouble(_TxtKeyStatus) * _netamt
            _str(6) = _netamt
            Label9.Text = "= $" + _netamt.ToString("0.00").PadLeft(10)
            _fucSum()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit8_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit8.EditValueChanged
        Try
            _netamt = Convert.ToDouble(TextEdit8.EditValue)
            _netamt = Convert.ToDouble(_TxtKeyStatus) * _netamt
            _str(7) = _netamt
            Label10.Text = "= $" + _netamt.ToString("0.00").PadLeft(10)
            _fucSum()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit9_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit9.EditValueChanged
        Try
            _netamt = Convert.ToDouble(TextEdit9.EditValue)
            _netamt = Convert.ToDouble(_TxtKeyStatus) * _netamt
            _str(8) = _netamt
            Label14.Text = "= $" + _netamt.ToString("0.00").PadLeft(10)
            _fucSum()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextEdit10_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit10.EditValueChanged
        Try
            _netamt = Convert.ToDouble(TextEdit10.EditValue)
            _netamt = Convert.ToDouble(_TxtKeyStatus) * _netamt
            _str(9) = _netamt
            Label15.Text = "= $" + _netamt.ToString("0.00").PadLeft(10)
            _fucSum()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _fucSum()
        Try
            _finam = 0.0
            For Each _va In _str
                _finam = _finam + _va
            Next
            txtPettyCash.Text = ""
            txtPettyCash.Text = _finam.ToString("0.00")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtPettyCash_EditValueChanged(sender As Object, e As EventArgs) Handles txtPettyCash.EditValueChanged

    End Sub

    Private Sub btn_numclear_Click(sender As Object, e As EventArgs) Handles btn_numclear.Click
        Try
            txtnum.Text = ""
            txtnum.Select()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_proceed_Click(sender As Object, e As EventArgs) Handles btn_proceed.Click
        Try
            If txtPettyCash.Text = "" OrElse txtPettyCash.EditValue Is Nothing Then
                properClass.R_denoamt = "0"
                Me.Close()
                'Me.Dispose()
            Else
                properClass.R_denoamt = txtPettyCash.Text
                Me.Close()
                ' Me.Dispose()
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class