'Imports MySql.Data.MySqlClient
'Module mySqlConnection
'    Dim db_con As New MySql.Data.MySqlClient.MySqlConnection
'    Dim dbname As String = "users"
'    Dim dbhost As String = "localhost"
'    Dim user As String = "root"
'    Dim pass As String = "root"
'    Dim port As String = "3307"
'    Dim cb As New MySqlConnectionStringBuilder

'    Function ConString() As Boolean
'        Try
'            cb.Database = dbname
'            cb.Server = dbhost
'            cb.UserID = user
'            cb.Password = pass
'            cb.Port = port
'            Return True
'        Catch ex As Exception
'            Return False
'        End Try
'    End Function
'    'Public Sub connect()
'    '    If Not db_con Is Nothing Then db_con.Close()
'    '    db_con.ConnectionString = String.Format("Server={0}; Uid={1}; Pwd={2}; Database={3}; pooling=false; Port={4};", dbhost, user, pass, dbname, port)
'    '    Try
'    '        db_con.Open()
'    '        MsgBox("Connected!")

'    '    Catch ex As MySqlException
'    '        MsgBox("Database Error:[" & ex.Message & "]")
'    '    End Try
'    'End Sub
'    Function MySqlDataAdapter(ByRef QueryStr As String) As DataTable
'        Try
'            ConString()
'            Dim table As New DataTable
'            Dim adpter As New MySqlDataAdapter
'            Dim conn As New MySql.Data.MySqlClient.MySqlConnection(cb.ConnectionString)
'            Dim cmd As MySqlCommand
'            cmd = New MySqlCommand(QueryStr, conn)
'            If Not conn Is Nothing Then conn.Close()
'            conn.Open()
'            adpter = New MySqlDataAdapter(cmd)
'            adpter.Fill(table)
'            If table.Rows.Count > 0 Then
'                Return table
'            Else
'                Return Nothing
'            End If
'            Return table
'        Catch ex As Exception
'            Return Nothing
'        End Try
'    End Function
'    Function InsertMySqlData(ByRef QueryStr As String) As Boolean
'        Try
'            ConString()
'            Dim table As New DataTable
'            Dim adpter As New MySqlDataAdapter
'            Dim conn As New MySql.Data.MySqlClient.MySqlConnection(cb.ConnectionString)
'            Dim cmd As MySqlCommand
'            cmd = New MySqlCommand(QueryStr, conn)
'            If Not conn Is Nothing Then conn.Close()
'            conn.Open()
'            cmd.ExecuteNonQuery()
'            Return True
'        Catch ex As Exception
'            Return False
'        End Try
'    End Function
'End Module
