Public Class FrmViewPendingOrder 
    Dim billholdCls As New BillHoldDBHelper(M_Details._Conn)
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DataLoad()
        Try
            Dim dt As New DataTable
            dt = billholdCls.GetHoldHdrDetails()
            If dt.Rows.Count > 0 Then
                GridControlPendingOrder.DataSource = dt
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FrmViewPendingOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            DataLoad()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridControlPendingOrder_DoubleClick(sender As Object, e As EventArgs) Handles GridControlPendingOrder.DoubleClick
        Try
            _FunctionKeyBoardModule.gs_keyboardValueInteger = 0
            Dim token = GridView1.GetFocusedRowCellValue("Token")
            If token > 0 Then
                _FunctionKeyBoardModule.gs_keyboardValueInteger = token
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnDeleteOrder_Click(sender As Object, e As EventArgs) Handles btnDeleteOrder.Click
        Try
            Dim trno = GridView1.GetFocusedRowCellValue("Trno")
            If trno > 0 Then
                frmKeyPassIIIMaster.ShowDialog()
                If frmKeyPassIIIMaster.DialogResult = Windows.Forms.DialogResult.OK Then
                    If billholdCls.DeleteHoldHdrDetails(trno) = True Then
                        DataLoad()
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class