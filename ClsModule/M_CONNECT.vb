Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Data.DataTableExtensions
Imports System.Runtime.InteropServices

Module M_CONNECT
    'Implements IDisposable
    'ReadOnly SCsrdr As New StreamReader(Application.StartupPath & "\M_CONNECT.dll")
    'Dim conn As New SqlConnection(SCsrdr.ReadLine())
    Dim conn As New SqlConnection(M_Details._Conn)
    Dim cmd As New SqlCommand
    Dim ds As DataSet
    Dim da As New SqlDataAdapter
    Dim _dataset As New DataSet
    Dim _Sqldareader As SqlDataReader
    Dim errstr As String = String.Empty

    Public Function dbconnectioncls(ByRef ErrorMsg As String) As Boolean
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function dbconnection(ByRef ErrorMsg As String) As Boolean
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            conn.Close()
            Return False
        End Try
    End Function
    Public Function dbconnectioncls2(ByRef ErrorMsg As String) As Boolean
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function dbconnection2(ByRef ErrorMsg As String) As Boolean
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Return True
        Catch ex As Exception
            conn.Close()
            Return False
        End Try
    End Function
    Public Function _ExecuteNonQuery(ByVal _Spname As String, ByVal _Sqlparameter As SqlParameter(), ByRef ErrorMsg As String) As Boolean
        Try
            cmd.Parameters.Clear()
            If Not _Sqlparameter Is Nothing Then
                For Each _Sqlpar In _Sqlparameter
                    cmd.Parameters.Add(_Sqlpar)
                Next
            End If
            If dbconnection(errstr) = False Then

            End If
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = _Spname
            cmd.ExecuteNonQuery()
            Return True
            ErrorMsg = _Spname & ":" & Environment.NewLine
            Return False

        Finally
            If dbconnectioncls(errstr) = False Then

            End If
        End Try
    End Function

    Public Function _sqlDataAdapter(ByVal spname As String, ByVal _SqlParameter As SqlParameter(), ByRef ErrorMsg As String) As DataSet
        Try
            _dataset = New DataSet
            cmd.Parameters.Clear()
            If Not _SqlParameter Is Nothing Then
                For Each spparameter In _SqlParameter
                    cmd.Parameters.Add(spparameter)
                Next
            End If
            If dbconnection(errstr) = False Then

            End If
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = spname
            cmd.CommandTimeout = 300
            da = New SqlDataAdapter(cmd)
            da.Fill(_dataset)
            Return _dataset
        Catch ex As Exception

            ErrorMsg = "_ExecuteNonQuery" & Environment.NewLine & ex.Message
            Return Nothing
        Finally
            If dbconnectioncls(errstr) = False Then
            End If
        End Try
    End Function
    Public Function _ExecuteScalar(ByVal spName As String, ByVal _SqlParameter As SqlParameter()) As Integer
        Try
            cmd.Parameters.Clear()
            Dim returnstr As Integer
            If Not _SqlParameter Is Nothing Then

                For Each spparameter In _SqlParameter
                    cmd.Parameters.Add(spparameter)
                Next
            End If
            If dbconnection(errstr) = False Then
                MessageBox.Show(errstr, "CON", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = spName
            returnstr = Convert.ToInt32(cmd.ExecuteScalar())

            Return returnstr
        Catch ex As Exception

            Return 0
        Finally
            If dbconnectioncls(errstr) = False Then
                MessageBox.Show(errstr, "CON", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Function
    Public Function _lOGINCHECK(ByVal spname As String, ByVal _SqlParameter As SqlParameter(), ByRef ERR As String) As DataSet
        Try
            _dataset = New DataSet
            cmd.Parameters.Clear()
            If Not _SqlParameter Is Nothing Then
                For Each spparameter In _SqlParameter
                    cmd.Parameters.Add(spparameter)
                Next
            End If
            If dbconnection(errstr) = False Then

            End If
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = spname
            cmd.CommandTimeout = 300
            da = New SqlDataAdapter(cmd)
            da.Fill(_dataset)
            Return _dataset
        Catch ex As Exception
            ERR = "USER NOT FOUND"
            Return _dataset
        Finally
            If dbconnectioncls(errstr) = False Then

            End If

        End Try
    End Function
    Public Function _sqlDataAdapter2(ByVal spname As String, ByVal _SqlParameter As SqlParameter()) As DataSet
        Try
            _dataset = New DataSet
            cmd.Parameters.Clear()
            If Not _SqlParameter Is Nothing Then
                For Each spparameter In _SqlParameter
                    cmd.Parameters.Add(spparameter)
                Next
            End If
            If dbconnection2(errstr) = False Then

            End If
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = spname
            cmd.CommandTimeout = 300
            da = New SqlDataAdapter(cmd)
            da.Fill(_dataset)
            Return _dataset
        Catch ex As Exception
            Return _dataset
        Finally
            If dbconnectioncls2(errstr) = False Then

            End If
        End Try
    End Function
    Public Function _SqlDataReader(ByVal spname As String, ByVal _SqlParameter As SqlParameter()) As SqlDataReader
        Try

            cmd.Parameters.Clear()
            If Not _SqlParameter Is Nothing Then
                For Each spparameter In _SqlParameter
                    cmd.Parameters.Add(spparameter)
                Next
            End If
            If dbconnection(errstr) = False Then

            End If
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = spname
            cmd.CommandTimeout = 300
            _Sqldareader = cmd.ExecuteReader
            Return _Sqldareader
        Catch ex As Exception
            Return _Sqldareader
        End Try
    End Function

End Module