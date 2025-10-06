
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.DataTable
Imports System.Text.RegularExpressions
Imports System.Reflection
Imports Microsoft.Win32
Imports DevExpress.XtraEditors
Public Class frmResendMail
    Dim trno As Integer
    Dim dsLoad As DataSet
    Dim grid1click As Boolean = False
    Dim grid2click As Boolean = False
    Private Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        Try
            If grid1click = True Then
                _shiftClosePrint(trno, False, True)
                Me.Close()
                Me.Dispose()
            ElseIf grid2click = True Then
                _DayClosePrint(trno, False, True)
                Me.Close()
                Me.Dispose()
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If grid1click = True Then
                _shiftClosePrint(trno, True, False)
                Me.Close()
            ElseIf grid2click = True Then
                _DayClosePrint(trno, True, False)
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            trno = 0
            grid1click = True
            trno = GridView1.GetFocusedRowCellValue("ShiftNo")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView2_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView2.RowClick
        Try
            trno = 0
            grid2click = True
            trno = GridView2.GetFocusedRowCellValue("DayNo")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _loadData()
        Try
            dsLoad = New DataSet
            Dim _Sql(3) As SqlParameter
            _Sql(0) = New SqlParameter("@mode", "DSprint")
            _Sql(1) = New SqlParameter("@ComIds", 1)
            _Sql(2) = New SqlParameter("@LocIds", 1)
            _Sql(3) = New SqlParameter("@PcNames", 1)
            dsLoad = _sqlDataAdapter2("[sp_general_query]", _Sql)
            If dsLoad.Tables(0).Rows.Count > 0 Then
                dgShiftClose.DataSource = dsLoad.Tables(0)
                dgDayclose.DataSource = dsLoad.Tables(1)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmResendMail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _loadData()
        Catch ex As Exception

        End Try
    End Sub
End Class