'' Example usage of the Fingerprint Authentication System
'' Place these examples in your forms where you need authentication

'Public Class ExampleUsage

'    ' Example 1: Replace password login with fingerprint or password option
'    Private Sub LoginButton_Click(sender As Object, e As EventArgs)
'        Try
'            ' Show login dialog allowing any employee
'            Dim authResult As AuthenticationResult = AuthenticationHelper.ShowLoginDialog(allowAnyEmployee:=True)

'            If authResult.Success Then
'                MessageBox.Show("Welcome " & authResult.EmpName & "!" & vbCrLf &
'                              "Authentication method: " & authResult.Method,
'                              "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

'                ' Set your global user variables
'                ' GlobalUser.EmpId = authResult.EmpId
'                ' GlobalUser.EmpName = authResult.EmpName
'                ' GlobalUser.AuthMethod = authResult.Method

'                ' Continue with application logic

'                ' Show main form or dashboard

'            Else
'                MessageBox.Show("Authentication failed: " & authResult.ErrorMessage,
'                              "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
'            End If

'        Catch ex As Exception
'            MessageBox.Show("Login error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End Try
'    End Sub

'    ' Example 2: Verify specific employee for sensitive operations
'    Private Sub DeleteRecordButton_Click(sender As Object, e As EventArgs)
'        Try
'            ' Get manager/supervisor employee ID (replace with your logic)
'            Dim managerId As Integer = 1 ' Replace with actual manager ID

'            ' Require specific employee authentication
'            Dim authResult As AuthenticationResult = AuthenticationHelper.AuthenticateEmployee(
'                managerId, "Manager", allowPassword:=True)

'            If authResult.Success Then
'                ' Proceed with delete operation
'                If MessageBox.Show("Confirm deletion of this record?", "Confirm Delete",
'                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
'                    ' Perform delete operation
'                    MessageBox.Show("Record deleted by " & authResult.EmpName, "Delete Successful")
'                End If
'            Else
'                MessageBox.Show("Manager authorization required for delete operations.",
'                              "Authorization Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
'            End If

'        Catch ex As Exception
'            MessageBox.Show("Authorization error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End Try
'    End Sub

'    ' Example 3: Quick fingerprint verification only (no password option)
'    Private Sub ClockInButton_Click(sender As Object, e As EventArgs)
'        Try
'            ' Get employee ID from selection or current context
'            Dim empId As Integer = GetSelectedEmployeeId() ' Replace with your method
'            Dim empName As String = GetEmployeeNameById(empId) ' Replace with your method

'            ' Quick fingerprint verification
'            If AuthenticationHelper.QuickFingerprintVerify(empId, empName) Then
'                ' Record clock-in time
'                MessageBox.Show("Clock-in recorded for " & empName & " at " & DateTime.Now.ToString(),
'                              "Clock-In Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

'                ' Update database with clock-in time
'                ' UpdateClockInTime(empId, DateTime.Now)

'            Else
'                MessageBox.Show("Fingerprint verification failed. Please try again or contact administrator.",
'                              "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
'            End If

'        Catch ex As Exception
'            MessageBox.Show("Clock-in error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End Try
'    End Sub

'    ' Example 4: Verify current user for sensitive operations
'    Private Sub ChangePasswordButton_Click(sender As Object, e As EventArgs)
'        Try
'            ' Get current user ID from your global variables
'            Dim currentUserId As Integer = 1 ' Replace with GlobalUser.EmpId or similar

'            ' Verify current user identity
'            If AuthenticationHelper.VerifyCurrentUser(currentUserId, "Change Password") Then
'                ' Show password change form
'                'Dim passwordForm As New FrmChangePassword()
'                'passwordForm.ShowDialog()
'            End If

'        Catch ex As Exception
'            MessageBox.Show("Verification error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End Try
'    End Sub

'    ' Example 5: Check if employee has fingerprint before offering fingerprint option
'    Private Async Sub CheckFingerprintAvailability()
'        Try
'            Dim empId As Integer = 1 ' Replace with actual employee ID

'            Dim hasFingerprint As Boolean = Await AuthenticationHelper.HasFingerprintRegistered(empId)

'            If hasFingerprint Then
'                ' Show fingerprint option
'                btnFingerprintLogin.Visible = True
'                lblFingerprintStatus.Text = "Fingerprint available"
'            Else
'                ' Hide fingerprint option
'                btnFingerprintLogin.Visible = False
'                lblFingerprintStatus.Text = "Fingerprint not registered"
'            End If

'        Catch ex As Exception
'            ' Handle error silently or show message
'        End Try
'    End Sub

'    ' Example 6: Login form with automatic fingerprint check
'    Private Async Sub LoginForm_Load(sender As Object, e As EventArgs)
'        Try
'            ' Check if any employees have fingerprints registered
'            ' You could maintain a list or check dynamically

'            ' For now, just enable fingerprint option
'            btnFingerprintLogin.Enabled = True
'            btnPasswordLogin.Enabled = True

'            ' Optionally auto-start fingerprint authentication
'            ' Dim authResult As AuthenticationResult = AuthenticationHelper.ShowLoginDialog(True)

'        Catch ex As Exception
'            MessageBox.Show("Login initialization error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End Try
'    End Sub

'    ' Example 7: Time and attendance with both options
'    Private Sub TimeAttendanceButton_Click(sender As Object, e As EventArgs)
'        Try
'            ' Show employee selection first
'            Dim empSelectForm As New FrmEmployeeSelect()
'            If empSelectForm.ShowDialog() = DialogResult.OK Then

'                Dim empId As Integer = empSelectForm.SelectedEmpId
'                Dim empName As String = empSelectForm.SelectedEmpName

'                ' Try fingerprint first, fallback to password
'                Dim authResult As AuthenticationResult = AuthenticationHelper.AuthenticateEmployee(
'                    empId, empName, allowPassword:=True)

'                If authResult.Success Then
'                    ' Record attendance
'                    RecordAttendance(empId, empName, authResult.Method)
'                    MessageBox.Show("Attendance recorded for " & empName &
'                                  " using " & authResult.Method & " authentication",
'                                  "Attendance Recorded", MessageBoxButtons.OK, MessageBoxIcon.Information)
'                End If
'            End If

'        Catch ex As Exception
'            MessageBox.Show("Attendance error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End Try
'    End Sub

'    ' Helper methods (implement these based on your application)
'    Private Function GetSelectedEmployeeId() As Integer
'        ' Return selected employee ID from your UI
'        Return 1
'    End Function

'    Private Function GetEmployeeNameById(empId As Integer) As String
'        ' Return employee name from your data source
'        Return "Employee " & empId.ToString()
'    End Function

'    Private Sub RecordAttendance(empId As Integer, empName As String, method As String)
'        ' Implement attendance recording logic
'        ' Insert into database with timestamp and authentication method
'    End Sub

'    ' Example controls (add these to your forms)
'    Private btnFingerprintLogin As Button
'    Private btnPasswordLogin As Button
'    Private lblFingerprintStatus As Label

'End Class

'' Example of integrating into existing forms:

'' 1. Sales Transaction Form - require manager approval for discounts
'' Private Sub ApplyDiscountButton_Click(sender As Object, e As EventArgs)
''     Dim authResult As AuthenticationResult = AuthenticationHelper.AuthenticateEmployee(
''         managerId, "Manager", allowPassword:=True)
''     If authResult.Success Then
''         ' Apply discount
''     End If
'' End Sub

'' 2. Cash Register - employee login at start of shift
'' Private Sub StartShiftButton_Click(sender As Object, e As EventArgs)
''     Dim authResult As AuthenticationResult = AuthenticationHelper.ShowLoginDialog(False, empId)
''     If authResult.Success Then
''         ' Start shift
''     End If
'' End Sub

'' 3. Inventory Management - verify user for stock adjustments
'' Private Sub AdjustStockButton_Click(sender As Object, e As EventArgs)
''     If AuthenticationHelper.VerifyCurrentUser(currentUserId, "Stock Adjustment") Then
''         ' Allow stock adjustment
''     End If
'' End Sub

'' 4. Reports - authenticate before viewing sensitive reports
'' Private Sub ViewSalesReportButton_Click(sender As Object, e As EventArgs)
''     Dim authResult As AuthenticationResult = AuthenticationHelper.ShowLoginDialog(True)
''     If authResult.Success Then
''         ' Show report
''     End If
'' End Sub
