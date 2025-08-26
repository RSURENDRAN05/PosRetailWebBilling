Imports System.Data.SqlClient

Public Class frmDeleteReason
    Dim Title As String = "Delete Reason"
    Public Overloads Sub ShowDialog(ByVal _title As String)
        Title = _title
        MyBase.ShowDialog()
    End Sub
    Private Sub frmDeleteReason_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lstBoxControll.Items.Clear()
            Dim _dstM As New DataSet
            Dim _sqlpara(2) As SqlParameter
            _sqlpara(0) = New SqlParameter("@mode", "DEL")
            _sqlpara(1) = New SqlParameter("@ComIds", _companyInfo.ComId)
            _sqlpara(2) = New SqlParameter("@LocIds", _companyInfo.LocId)
            _dstM = _sqlDataAdapter2("sp_general_data", _sqlpara)
            If _dstM.Tables(0).Rows.Count > 0 Then
                For Each _rows In _dstM.Tables(0).Rows
                    lstBoxControll.Items.Add(_rows("po_name").ToString)
                Next
            End If
             
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.OK
            '_deleteReason.selectedReason = lstBoxControll.SelectedItem
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnHide_Click(sender As Object, e As EventArgs) Handles btnHide.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class