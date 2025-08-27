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
            sw = New StreamWriter(M_Details._logPath & "ErrorOnLog.txt", True)
            sw.WriteLine(DateTime.Now.ToString() + ":" + ex.Source.ToString().Trim() + ";" + ex.Message.ToString().Trim())
            sw.Flush()
            sw.Close()
        Catch

        End Try
    End Sub

    Public Sub WriteErroLog(ByRef ex As String)
        Dim sw As StreamWriter
        Try
            sw = New StreamWriter(M_Details._logPath & "ErrorOnLog.txt", True)
            sw.WriteLine(DateTime.Now.ToString() + ":" + ex.ToString().Trim())
            sw.Flush()
            sw.Close()
        Catch
        End Try
    End Sub
    Sub WriteErroLog(p1 As String, p2 As String)
        Dim sw As StreamWriter
        Try
            If Not File.Exists(Path.Combine(M_Details._logpath & "\ErrorOnLog.txt")) Then
                File.Create(Path.Combine(M_Details._logpath & "\ErrorOnLog.txt"))
            End If
            sw = New StreamWriter(Path.Combine(M_Details._logpath & "\ErrorOnLog.txt"), True)
            sw.WriteLine(DateTime.Now.ToString() + ":" + p1.ToString().Trim() + ";" + p2.ToString().Trim())
            sw.Flush()
            sw.Close()
        Catch
        End Try
    End Sub
End Module
