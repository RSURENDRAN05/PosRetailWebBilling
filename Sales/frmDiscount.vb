Public Class frmDiscount 

    Private Sub frmDiscount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtamount.Text = ""
            txtpercentage.Text = ""
            If RadioGroup1.SelectedIndex = 0 Then
                _discount.DiscountPer = True
                txtpercentage.Properties.ReadOnly = False
                txtpercentage.Focus()
                txtamount.Properties.ReadOnly = True
            Else
                _discount.DiscountPer = False
                txtpercentage.Properties.ReadOnly = True
                txtamount.Focus()
                txtamount.Properties.ReadOnly = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
            If RadioGroup1.SelectedIndex = 0 Then
                _discount.discountValue = txtpercentage.EditValue
            Else
                _discount.discountValue = txtamount.EditValue
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadioGroup1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioGroup1.SelectedIndexChanged
        Try
            If RadioGroup1.SelectedIndex = 0 Then
                _discount.DiscountPer = True
                txtpercentage.Properties.ReadOnly = False
                txtpercentage.Focus()
                txtamount.Properties.ReadOnly = True
            Else
                _discount.DiscountPer = False
                txtpercentage.Properties.ReadOnly = True
                txtamount.Focus()
                txtamount.Properties.ReadOnly = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
End Class