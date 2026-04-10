Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.DataTable
Public Class frmBarcodeSelector
    Private _dataSet As DataSet
    Dim G_BarcodeName As String = String.Empty
    Private Billamt As Decimal = 0.0
    Private Sub frmBarcodeSelector_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            _dataSet = New DataSet
            '_dataSet = clsdb._sqlDataAdapter("[BarcodeLabel_load]", Nothing)
            cmbBarcodeLabel.Properties.DataSource = _dataSet.Tables(0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        G_BarcodeName = cmbBarcodeLabel.Text
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub frmBarcodeSelector_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class