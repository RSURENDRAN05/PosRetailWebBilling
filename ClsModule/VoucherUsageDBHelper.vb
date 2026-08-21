Imports System.Data.SqlClient
Imports System.Data

''' <summary>
''' Records voucher redemptions locally (MSSQL) and tracks whether each record has
''' been pushed/confirmed to the cloud (MySQL voucher_sales via AjaxRequest=92).
''' One row is written per voucher applied to a successfully-paid bill:
''' BillNo, BillAmount (net, after discount), ComId, LocId, ShiftNo, DayNo, Created.
''' The BillNo doubles as the reference number for the redemption.
''' </summary>
Public Class VoucherUsageDBHelper
    Private _connectionString As String = M_Details._Conn

    Public Sub New(connectionString As String)
        _connectionString = connectionString
    End Sub

    ''' <summary>
    ''' Insert a local voucher usage record right after the bill is saved successfully.
    ''' Returns True and the new local row id (vu_id) via newUsageId.
    ''' </summary>
    Public Function SaveVoucherUsage(voucherId As Integer, voucherNo As Integer, voucherCode As String, billNo As String,
                                      billAmount As Decimal, comId As Integer, locId As Integer,
                                      shiftNo As Integer, dayNo As Integer,
                                      ByRef errorMessage As String, ByRef newUsageId As Integer) As Boolean
        newUsageId = 0
        Try
            Using connection As New SqlConnection(_connectionString)
                Using command As New SqlCommand("sp_voucherusage", connection)
                    command.CommandType = CommandType.StoredProcedure
                    command.Parameters.AddWithValue("@mode", "I")
                    command.Parameters.AddWithValue("@vu_voucherid", voucherId)
                    command.Parameters.AddWithValue("@vu_voucherno", voucherNo)
                    command.Parameters.AddWithValue("@vu_vouchercode", If(String.IsNullOrEmpty(voucherCode), "", voucherCode))
                    command.Parameters.AddWithValue("@vu_billno", If(String.IsNullOrEmpty(billNo), "0", billNo))
                    command.Parameters.AddWithValue("@vu_billamount", billAmount)
                    command.Parameters.AddWithValue("@vu_comid", comId)
                    command.Parameters.AddWithValue("@vu_locid", locId)
                    command.Parameters.AddWithValue("@vu_shiftno", shiftNo)
                    command.Parameters.AddWithValue("@vu_dayno", dayNo)

                    connection.Open()
                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                        newUsageId = Convert.ToInt32(result)
                    End If
                End Using
            End Using
            Return newUsageId > 0
        Catch ex As Exception
            errorMessage = "Error saving voucher usage: " & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Rows not yet confirmed to the cloud (vu_pushstatus = 0), for the auto-sync timer to retry.
    ''' </summary>
    Public Function GetPendingVoucherUsage() As DataTable
        Dim dt As New DataTable
        Try
            Using connection As New SqlConnection(_connectionString)
                Using command As New SqlCommand("sp_voucherusage", connection)
                    command.CommandType = CommandType.StoredProcedure
                    command.Parameters.AddWithValue("@mode", "S")
                    Using adapter As New SqlDataAdapter(command)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("GetPendingVoucherUsage Error: " & ex.Message)
        End Try
        Return dt
    End Function

    ''' <summary>
    ''' Marks a local usage row as pushed/confirmed once AjaxRequest=92 succeeds.
    ''' </summary>
    Public Function MarkVoucherUsagePushed(usageId As Integer) As Boolean
        Try
            Using connection As New SqlConnection(_connectionString)
                Using command As New SqlCommand("sp_voucherusage", connection)
                    command.CommandType = CommandType.StoredProcedure
                    command.Parameters.AddWithValue("@mode", "U")
                    command.Parameters.AddWithValue("@vu_id", usageId)
                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("MarkVoucherUsagePushed Error: " & ex.Message)
            Return False
        End Try
    End Function
End Class
