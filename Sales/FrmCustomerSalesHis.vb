Imports System.Net
Imports Newtonsoft.Json.Linq
Imports System.Drawing
Imports Newtonsoft.Json
Imports System.IO
Imports System.Data.SqlClient

Public Class FrmCustomerSalesHis

    Dim _customerId As Integer

    Public Overloads Sub ShowDialog(ByVal CustomerId As Integer)
        Try
            _customerId = CustomerId
            MyBase.ShowDialog()
        Catch ex As Exception
            ' Handle exception if needed
        End Try
    End Sub

    Private Sub DataLoad()
        Try
            Dim ds As New DataSet
            Dim _sql(1) As SqlParameter

            _sql(0) = New SqlParameter("@mode", "S")
            _sql(1) = New SqlParameter("@customerid", _customerId)

            ds = _sqlDataAdapter2("sp_customersaleshis", _sql)

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                GridControl1.DataSource = ds.Tables(0)
            Else
                GridControl1.DataSource = Nothing
            End If

        Catch ex As Exception
            ' Handle exception if needed
        End Try
    End Sub

    Private Sub FrmCustomerSalesHis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DataLoad()
        Catch ex As Exception
            ' Handle exception if needed
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class
