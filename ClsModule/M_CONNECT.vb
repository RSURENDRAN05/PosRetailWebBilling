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

    ''' <summary>
    ''' Validates database connection string before login
    ''' Tests connection string format, server accessibility, database existence, and authentication
    ''' </summary>
    ''' <param name="ErrorMsg">Returns detailed error message if validation fails</param>
    ''' <param name="connectionString">Optional custom connection string to test (if empty, uses M_Details._Conn)</param>
    ''' <returns>True if connection is valid and database is accessible, False otherwise</returns>
    Public Function ValidateDBConnectionBeforeLogin(ByRef ErrorMsg As String, Optional connectionString As String = "") As Boolean
        Dim testConnection As SqlConnection = Nothing
        Try
            ErrorMsg = ""

            ' Use provided connection string or default from M_Details
            Dim connStringToTest As String = If(String.IsNullOrEmpty(connectionString), M_Details._Conn, connectionString)

            ' Step 1: Check if connection string is not empty
            If String.IsNullOrEmpty(connStringToTest) Then
                ErrorMsg = "Database connection string is empty or not configured." & Environment.NewLine &
                          "Please check registry settings or application configuration."
                Return False
            End If

            ' Step 2: Validate connection string format
            Try
                Dim builder As New SqlConnectionStringBuilder(connStringToTest)

                ' Check essential connection string components
                If String.IsNullOrEmpty(builder.DataSource) Then
                    ErrorMsg = "Database server name/data source is missing in connection string."
                    Return False
                End If

                If String.IsNullOrEmpty(builder.InitialCatalog) AndAlso String.IsNullOrEmpty(builder.AttachDBFilename) Then
                    ErrorMsg = "Database name (Initial Catalog) is missing in connection string."
                    Return False
                End If

                ' Log connection details for debugging (without sensitive info)
                System.Diagnostics.Debug.WriteLine("=== Database Connection Validation ===")
                System.Diagnostics.Debug.WriteLine("Server: " & builder.DataSource)
                System.Diagnostics.Debug.WriteLine("Database: " & builder.InitialCatalog)
                System.Diagnostics.Debug.WriteLine("Authentication: " & If(builder.IntegratedSecurity, "Windows Authentication", "SQL Server Authentication"))

            Catch ex As Exception
                ErrorMsg = "Invalid connection string format: " & ex.Message
                Return False
            End Try

            ' Step 3: Test actual database connection
            testConnection = New SqlConnection(connStringToTest)

            ' Attempt to open connection
            testConnection.Open()

            ' Step 4: Test database accessibility with a simple query
            Using testCmd As New SqlCommand("SELECT 1 AS TestConnection", testConnection)
                Dim result As Object = testCmd.ExecuteScalar()
                If result Is Nothing OrElse Convert.ToInt32(result) <> 1 Then
                    ErrorMsg = "Database connection established but unable to execute test query."
                    Return False
                End If
            End Using

            ' Step 5: Check if essential tables exist (optional - customize based on your application)
            If Not CheckEssentialTablesExist(testConnection, ErrorMsg) Then
                Return False
            End If

            System.Diagnostics.Debug.WriteLine("Database connection validation successful.")
            Return True

        Catch timeoutEx As SqlException When timeoutEx.Number = -2 ' Connection timeout
            ErrorMsg = "Database connection timeout. Please check:" & Environment.NewLine &
                      "1. Server name/IP address is correct" & Environment.NewLine &
                      "2. SQL Server is running" & Environment.NewLine &
                      "3. Network connectivity" & Environment.NewLine &
                      "4. Firewall settings"
            Return False

        Catch sqlEx As SqlException
            Select Case sqlEx.Number
                Case 2 ' Server not found
                    ErrorMsg = "SQL Server not found or not accessible. Error: " & sqlEx.Message & Environment.NewLine &
                              "Please verify server name and ensure SQL Server is running."
                Case 4 ' Server rejected connection
                    ErrorMsg = "SQL Server rejected the connection. Error: " & sqlEx.Message & Environment.NewLine &
                              "Check authentication credentials and server configuration."
                Case 18456 ' Login failed
                    ErrorMsg = "Login failed for database user. Error: " & sqlEx.Message & Environment.NewLine &
                              "Please check username, password, and database permissions."
                Case 911 ' Database does not exist
                    ErrorMsg = "Database does not exist. Error: " & sqlEx.Message & Environment.NewLine &
                              "Please verify database name in connection string."
                Case Else
                    ErrorMsg = "SQL Server error (Code: " & sqlEx.Number & "): " & sqlEx.Message
            End Select
            Return False

        Catch ex As Exception
            ErrorMsg = "Unexpected error during database connection validation: " & ex.Message
            Return False

        Finally
            ' Always close test connection
            If testConnection IsNot Nothing AndAlso testConnection.State = ConnectionState.Open Then
                Try
                    testConnection.Close()
                    testConnection.Dispose()
                Catch
                    ' Ignore cleanup errors
                End Try
            End If
        End Try
    End Function

    ''' <summary>
    ''' Checks if essential database tables exist
    ''' </summary>
    ''' <param name="connection">Active database connection</param>
    ''' <param name="ErrorMsg">Error message if validation fails</param>
    ''' <returns>True if essential tables exist</returns>
    Private Function CheckEssentialTablesExist(connection As SqlConnection, ByRef ErrorMsg As String) As Boolean
        Try
            ' List of essential tables - customize based on your application requirements
            Dim essentialTables() As String = {
                "pos_shiftclose",
                "pos_sale_invoicedtl",
                "pos_sale_invoicehdr",
                "pos_payout_dtl"
            }

            For Each tableName As String In essentialTables
                Using cmd As New SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName", connection)
                    cmd.Parameters.AddWithValue("@TableName", tableName)
                    Dim tableCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    If tableCount = 0 Then
                        ErrorMsg = "Essential database table '" & tableName & "' does not exist." & Environment.NewLine &
                                  "Database may be incomplete or corrupted."
                        Return False
                    End If
                End Using
            Next

            Return True

        Catch ex As Exception
            ErrorMsg = "Error checking database table structure: " & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Quick connection test - lighter version for frequent checking
    ''' </summary>
    ''' <param name="ErrorMsg">Error message if test fails</param>
    ''' <returns>True if basic connection works</returns>
    Public Function QuickDBConnectionTest(ByRef ErrorMsg As String) As Boolean
        Dim testConnection As SqlConnection = Nothing
        Try
            ErrorMsg = ""

            If String.IsNullOrEmpty(M_Details._Conn) Then
                ErrorMsg = "Database connection string is not configured."
                Return False
            End If

            testConnection = New SqlConnection(M_Details._Conn)
            testConnection.Open()

            Using testCmd As New SqlCommand("SELECT 1", testConnection)
                testCmd.ExecuteScalar()
            End Using

            Return True

        Catch ex As Exception
            ErrorMsg = "Database connection test failed: " & ex.Message
            Return False

        Finally
            If testConnection IsNot Nothing AndAlso testConnection.State = ConnectionState.Open Then
                Try
                    testConnection.Close()
                    testConnection.Dispose()
                Catch
                    ' Ignore cleanup errors
                End Try
            End If
        End Try
    End Function

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

    ''' <summary>
    ''' Refreshes the database connection string from registry and updates the connection object
    ''' Call this before login to ensure latest connection settings are used
    ''' </summary>
    ''' <param name="ErrorMsg">Error message if refresh fails</param>
    ''' <returns>True if connection string was successfully refreshed</returns>
    Public Function RefreshDBConnectionFromRegistry(ByRef ErrorMsg As String) As Boolean
        Try
            ErrorMsg = ""

            ' Read fresh connection string from registry
            Dim registryKey As Microsoft.Win32.RegistryKey = Nothing
            Try
                registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\POSAPP")
                If registryKey Is Nothing Then
                    ErrorMsg = "Registry key 'SOFTWARE\POSAPP' not found. Please ensure the application is properly installed."
                    Return False
                End If

                Dim newConnString As Object = registryKey.GetValue("Conn")
                If newConnString Is Nothing OrElse String.IsNullOrEmpty(newConnString.ToString()) Then
                    ErrorMsg = "Connection string not found in registry. Please reconfigure database settings."
                    Return False
                End If

                ' Update M_Details connection string
                M_Details._Conn = newConnString.ToString()

                ' Close existing connection if open
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If

                ' Create new connection object with refreshed connection string
                conn = New SqlConnection(M_Details._Conn)

                System.Diagnostics.Debug.WriteLine("Database connection string refreshed from registry.")
                Return True

            Finally
                If registryKey IsNot Nothing Then
                    registryKey.Close()
                End If
            End Try

        Catch ex As UnauthorizedAccessException
            ErrorMsg = "Access denied reading registry. Please run application as administrator or check registry permissions."
            Return False
        Catch ex As Exception
            ErrorMsg = "Error refreshing connection string from registry: " & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Gets current database connection information for display/logging purposes
    ''' </summary>
    ''' <returns>Formatted string with connection details (without sensitive information)</returns>
    Public Function GetConnectionInfo() As String
        Try
            If String.IsNullOrEmpty(M_Details._Conn) Then
                Return "Connection string not configured."
            End If

            Dim builder As New SqlConnectionStringBuilder(M_Details._Conn)
            Dim info As String = ""
            info &= "Server: " & builder.DataSource & Environment.NewLine
            info &= "Database: " & builder.InitialCatalog & Environment.NewLine
            info &= "Authentication: " & If(builder.IntegratedSecurity, "Windows Authentication", "SQL Server Authentication") & Environment.NewLine
            info &= "Connection Timeout: " & builder.ConnectTimeout & " seconds" & Environment.NewLine
            info &= "Current State: " & conn.State.ToString()

            Return info

        Catch ex As Exception
            Return "Error retrieving connection information: " & ex.Message
        End Try
    End Function

End Module
