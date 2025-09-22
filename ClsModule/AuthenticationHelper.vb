Public Class AuthenticationHelper

    ''' <summary>
    ''' Authenticate a specific employee using fingerprint or password
    ''' </summary>
    ''' <param name="empId">Employee ID to authenticate</param>
    ''' <param name="empName">Employee name for display</param>
    ''' <param name="allowPassword">Allow password fallback</param>
    ''' <param name="passwordHash">Optional password hash for verification</param>
    ''' <returns>Authentication result</returns>
    Public Shared Function AuthenticateEmployee(empId As Integer, empName As String, Optional allowPassword As Boolean = True, Optional passwordHash As String = "") As AuthenticationResult
        Try
            Dim authForm As FrmFingerprintAuth = FrmFingerprintAuth.ShowAuthenticationDialog(empId, empName, allowPassword, passwordHash)

            Dim result As New AuthenticationResult()
            result.Success = authForm.IsAuthenticated
            result.EmpId = authForm.AuthenticatedEmpId
            result.EmpName = authForm.AuthenticatedEmpName
            result.Method = authForm.AuthenticationMethod

            Return result

        Catch ex As Exception
            Return New AuthenticationResult() With {
                .Success = False,
                .ErrorMessage = "Authentication error: " & ex.Message
            }
        End Try
    End Function

    ''' <summary>
    ''' Show login dialog that requires either fingerprint or password authentication
    ''' </summary>
    ''' <param name="allowAnyEmployee">If true, allows any registered employee. If false, requires specific employee</param>
    ''' <param name="requiredEmpId">If allowAnyEmployee is false, this employee must authenticate</param>
    ''' <returns>Authentication result</returns>
    Public Shared Function ShowLoginDialog(Optional allowAnyEmployee As Boolean = True, Optional requiredEmpId As Integer = 0) As AuthenticationResult
        Try
            If allowAnyEmployee Then
                ' Show employee selection dialog first, then authenticate
                Dim empSelectForm As New FrmEmployeeSelect()
                If empSelectForm.ShowDialog() = DialogResult.OK Then
                    Return AuthenticateEmployee(empSelectForm.SelectedEmpId, empSelectForm.SelectedEmpName, True)
                Else
                    Return New AuthenticationResult() With {.Success = False, .ErrorMessage = "No employee selected"}
                End If
            Else
                ' Authenticate specific employee
                Dim empName As String = GetEmployeeNameById(requiredEmpId)
                Return AuthenticateEmployee(requiredEmpId, empName, True)
            End If

        Catch ex As Exception
            Return New AuthenticationResult() With {
                .Success = False,
                .ErrorMessage = "Login error: " & ex.Message
            }
        End Try
    End Function

    ''' <summary>
    ''' Quick fingerprint verification without password option
    ''' </summary>
    ''' <param name="empId">Employee ID to verify</param>
    ''' <param name="empName">Employee name</param>
    ''' <returns>True if fingerprint verified successfully</returns>
    Public Shared Function QuickFingerprintVerify(empId As Integer, empName As String) As Boolean
        Try
            Dim result As AuthenticationResult = AuthenticateEmployee(empId, empName, False)
            Return result.Success AndAlso result.Method = "Fingerprint"
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Verify current user for sensitive operations
    ''' </summary>
    ''' <param name="currentUserId">Current logged in user ID</param>
    ''' <param name="operationName">Name of operation requiring verification</param>
    ''' <returns>True if user verified successfully</returns>
    Public Shared Function VerifyCurrentUser(currentUserId As Integer, operationName As String) As Boolean
        Try
            Dim empName As String = GetEmployeeNameById(currentUserId)
            Dim message As String = "Please verify your identity to perform: " & operationName

            MessageBox.Show(message, "Identity Verification Required", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Dim result As AuthenticationResult = AuthenticateEmployee(currentUserId, empName, True)
            Return result.Success

        Catch ex As Exception
            MessageBox.Show("Verification error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Check if employee has fingerprint registered
    ''' </summary>
    ''' <param name="empId">Employee ID to check</param>
    ''' <returns>True if fingerprint is registered</returns>
    Public Shared Async Function HasFingerprintRegistered(empId As Integer) As Task(Of Boolean)
        Try
            Using client As New System.Net.WebClient()
                client.Headers(System.Net.HttpRequestHeader.ContentType) = "application/json"
                Dim postData As String = "{""EmpId"":" & empId.ToString() & "}"
                Dim response As String = Await Task.Run(Function()
                                                            Return client.UploadString(M_Details.LinkAjaxRequest & "AttRequest=2", postData)
                                                        End Function)

                Dim parsedResponse As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.Linq.JObject.Parse(response)
                Return parsedResponse("Success").ToString().ToLower() = "true"
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Get employee name by ID (implement based on your data source)
    ''' </summary>
    Private Shared Function GetEmployeeNameById(empId As Integer) As String
        Try
            ' Implement this based on your employee data source
            ' This could query database, call API, etc.

            ' Example implementation:
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=1")
            Dim parsedJson As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.Linq.JObject.Parse(json)

            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")
                For Each item In dataArray
                    Dim currentEmpId As Integer = 0
                    If item("emp_id") IsNot Nothing Then
                        currentEmpId = Convert.ToInt32(item("emp_id"))
                        If currentEmpId = empId Then
                            Return If(item("emp_printname").ToString(), "Employee " & empId.ToString())
                        End If
                    End If
                Next
            End If

            Return "Employee " & empId.ToString()

        Catch ex As Exception
            Return "Employee " & empId.ToString()
        End Try
    End Function

End Class

''' <summary>
''' Result of authentication attempt
''' </summary>
Public Class AuthenticationResult
    Public Property Success As Boolean = False
    Public Property EmpId As Integer = 0
    Public Property EmpName As String = ""
    Public Property Method As String = "" ' "Fingerprint", "Password", or "Failed"
    Public Property ErrorMessage As String = ""

    Public ReadOnly Property IsFingerprintAuth As Boolean
        Get
            Return Success AndAlso Method = "Fingerprint"
        End Get
    End Property

    Public ReadOnly Property IsPasswordAuth As Boolean
        Get
            Return Success AndAlso Method = "Password"
        End Get
    End Property
End Class
