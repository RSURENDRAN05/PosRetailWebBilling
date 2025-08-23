Imports DevExpress.XtraGrid
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Data
Imports System
Module EventlogModule
    Dim errMsg As String
    Public Sub WriteErroLog(ByRef ex As Exception)

        Dim sw As StreamWriter
        Try
            sw = New StreamWriter(M_Details._logPath & "ErrorLogException.txt", True)
            sw.WriteLine(DateTime.Now.ToString() + ":" + ex.Source.ToString().Trim() + ";" + ex.Message.ToString().Trim())
            sw.Flush()
            sw.Close()
        Catch

        End Try
    End Sub

    Public Sub WriteErroLog(ByRef ex As String)
        Dim sw As StreamWriter
        Try
            sw = New StreamWriter(M_Details._logPath & "ErrorLogString.txt", True)
            sw.WriteLine(DateTime.Now.ToString() + ":" + ex.ToString().Trim())
            sw.Flush()
            sw.Close()
        Catch
        End Try
    End Sub
End Module
