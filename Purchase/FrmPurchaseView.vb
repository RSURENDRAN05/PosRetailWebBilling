Public Class FrmPurchaseView 

    Private Sub FrmPurchaseView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            getPurchaseView()
            If _JsonData.PurchaseViewTable.Rows.Count > 0 Then
                GridControlGRNSelector.DataSource = _JsonData.PurchaseViewTable
            Else
                GridControlGRNSelector.DataSource = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BTNCANCEL_Click(sender As Object, e As EventArgs) Handles BTNCANCEL.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BTNoK_Click(sender As Object, e As EventArgs) Handles BTNoK.Click
        Try
            G_GRNNo = GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "GRNNo")
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class