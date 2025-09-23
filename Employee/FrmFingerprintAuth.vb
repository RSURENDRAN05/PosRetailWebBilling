'Public Class FrmFingerprintAuth
'    Private fingerprintAuth As FingerprintAuthenticationModule
'    Private authResult As Boolean = False
'    Private verifiedEmpId As Integer = 0
'    Private verifiedEmpName As String = ""

'    Public Property EmpId As Integer = 0
'    Public Property EmpName As String = ""
'    Public Property AllowPasswordFallback As Boolean = True
'    Public Property PasswordHash As String = "" ' For password verification

'    ' Results
'    Public ReadOnly Property IsAuthenticated As Boolean
'        Get
'            Return authResult
'        End Get
'    End Property

'    Public ReadOnly Property AuthenticatedEmpId As Integer
'        Get
'            Return verifiedEmpId
'        End Get
'    End Property

'    Public ReadOnly Property AuthenticatedEmpName As String
'        Get
'            Return verifiedEmpName
'        End Get
'    End Property

'    Public ReadOnly Property AuthenticationMethod As String
'        Get
'            If authResult Then
'                Return If(verifiedEmpId > 0, "Fingerprint", "Password")
'            Else
'                Return "Failed"
'            End If
'        End Get
'    End Property

'    Private Sub FrmFingerprintAuth_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        Try
'            ' Initialize UI
'            lblEmployeeName.Text = If(String.IsNullOrEmpty(EmpName), "Employee Authentication", EmpName)
'            lblStatus.Text = "Initializing..."
'            btnUsePassword.Visible = AllowPasswordFallback
'            txtPassword.Visible = AllowPasswordFallback
'            lblPassword.Visible = AllowPasswordFallback

'            ' Initialize fingerprint module
'            fingerprintAuth = New FingerprintAuthenticationModule()
'            AddHandler fingerprintAuth.VerificationComplete, AddressOf OnVerificationComplete
'            AddHandler fingerprintAuth.StatusUpdate, AddressOf OnStatusUpdate
'            AddHandler fingerprintAuth.ShowError, AddressOf OnShowError

'            ' Start fingerprint verification if EmpId is provided
'            If EmpId > 0 Then
'                StartFingerprintAuth()
'            Else
'                lblStatus.Text = "Please provide employee ID for verification."
'            End If

'        Catch ex As Exception
'            MessageBox.Show("Error initializing authentication: " & ex.Message, "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'            Me.Close()
'        End Try
'    End Sub

'    Private Async Sub StartFingerprintAuth()
'        Try
'            lblStatus.Text = "Starting fingerprint verification..."
'            btnStartFingerprint.Enabled = False
'            btnCancel.Text = "Cancel"

'            Dim started As Boolean = Await fingerprintAuth.StartVerification(EmpId, EmpName)
'            If Not started Then
'                btnStartFingerprint.Enabled = True
'                btnCancel.Text = "Close"
'            End If

'        Catch ex As Exception
'            MessageBox.Show("Error starting fingerprint authentication: " & ex.Message, "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'            btnStartFingerprint.Enabled = True
'            btnCancel.Text = "Close"
'        End Try
'    End Sub

'    Private Sub OnVerificationComplete(success As Boolean, empId As Integer, empName As String)
'        Try
'            If Me.InvokeRequired Then
'                Me.Invoke(New Action(Of Boolean, Integer, String)(AddressOf OnVerificationComplete), success, empId, empName)
'                Return
'            End If

'            If success Then
'                authResult = True
'                verifiedEmpId = empId
'                verifiedEmpName = empName
'                lblStatus.Text = "Authentication successful!"
'                lblStatus.ForeColor = Color.Green

'                ' Auto-close after success
'                Threading.Thread.Sleep(1000)
'                Me.DialogResult = DialogResult.OK
'                Me.Close()
'            Else
'                lblStatus.Text = "Authentication failed. Please try again."
'                lblStatus.ForeColor = Color.Red
'                btnStartFingerprint.Enabled = True
'                btnCancel.Text = "Close"
'            End If

'        Catch ex As Exception
'            MessageBox.Show("Error processing verification result: " & ex.Message, "Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End Try
'    End Sub

'    Private Sub OnStatusUpdate(message As String)
'        Try
'            If Me.InvokeRequired Then
'                Me.Invoke(New Action(Of String)(AddressOf OnStatusUpdate), message)
'                Return
'            End If

'            lblStatus.Text = message
'            lblStatus.ForeColor = Color.Blue

'        Catch ex As Exception
'            ' Ignore UI update errors
'        End Try
'    End Sub

'    Private Sub OnShowError(message As String, title As String)
'        Try
'            If Me.InvokeRequired Then
'                Me.Invoke(New Action(Of String, String)(AddressOf OnShowError), message, title)
'                Return
'            End If

'            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
'            btnStartFingerprint.Enabled = True
'            btnCancel.Text = "Close"

'        Catch ex As Exception
'            ' Ignore error display issues
'        End Try
'    End Sub

'    Private Sub btnStartFingerprint_Click(sender As Object, e As EventArgs) Handles btnStartFingerprint.Click
'        If EmpId > 0 Then
'            StartFingerprintAuth()
'        Else
'            MessageBox.Show("Please provide employee ID for verification.", "Employee ID Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
'        End If
'    End Sub

'    Private Sub btnUsePassword_Click(sender As Object, e As EventArgs) Handles btnUsePassword.Click
'        Try
'            If String.IsNullOrEmpty(txtPassword.Text) Then
'                MessageBox.Show("Please enter password.", "Password Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
'                Return
'            End If

'            ' Verify password (implement your password verification logic here)
'            If VerifyPassword(txtPassword.Text) Then
'                authResult = True
'                verifiedEmpId = 0 ' 0 indicates password authentication
'                verifiedEmpName = EmpName
'                lblStatus.Text = "Password authentication successful!"
'                lblStatus.ForeColor = Color.Green

'                Me.DialogResult = DialogResult.OK
'                Me.Close()
'            Else
'                MessageBox.Show("Invalid password. Please try again.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
'                txtPassword.SelectAll()
'                txtPassword.Focus()
'            End If

'        Catch ex As Exception
'            MessageBox.Show("Error verifying password: " & ex.Message, "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End Try
'    End Sub

'    Private Function VerifyPassword(password As String) As Boolean
'        Try
'            ' Implement your password verification logic here
'            ' This could check against database, hash comparison, etc.

'            If Not String.IsNullOrEmpty(PasswordHash) Then
'                ' If password hash is provided, verify against it
'                Dim inputHash As String = ComputePasswordHash(password)
'                Return inputHash.Equals(PasswordHash, StringComparison.OrdinalIgnoreCase)
'            Else
'                ' Default simple check (replace with your logic)
'                Return password = "admin" ' Replace with actual verification
'            End If

'        Catch ex As Exception
'            Return False
'        End Try
'    End Function

'    Private Function ComputePasswordHash(password As String) As String
'        Try
'            ' Implement your password hashing logic here
'            ' Example using MD5 (use stronger hashing in production)
'            Using md5 As System.Security.Cryptography.MD5 = System.Security.Cryptography.MD5.Create()
'                Dim inputBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(password)
'                Dim hashBytes As Byte() = md5.ComputeHash(inputBytes)
'                Return Convert.ToBase64String(hashBytes)
'            End Using
'        Catch ex As Exception
'            Return ""
'        End Try
'    End Function

'    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
'        Try
'            If fingerprintAuth IsNot Nothing AndAlso fingerprintAuth.IsActive Then
'                fingerprintAuth.StopVerification()
'            End If

'            authResult = False
'            Me.DialogResult = DialogResult.Cancel
'            Me.Close()

'        Catch ex As Exception
'            Me.Close()
'        End Try
'    End Sub

'    Private Sub FrmFingerprintAuth_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
'        Try
'            If fingerprintAuth IsNot Nothing Then
'                fingerprintAuth.Dispose()
'            End If
'        Catch ex As Exception
'            ' Ignore cleanup errors
'        End Try
'    End Sub

'    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
'        If e.KeyChar = Chr(13) Then ' Enter key
'            btnUsePassword_Click(sender, e)
'        End If
'    End Sub

'    ''' <summary>
'    ''' Static method to easily call fingerprint authentication from anywhere
'    ''' </summary>
'    Public Shared Function ShowAuthenticationDialog(empId As Integer, empName As String, Optional allowPassword As Boolean = True, Optional passwordHash As String = "") As FrmFingerprintAuth
'        Dim authForm As New FrmFingerprintAuth()
'        authForm.EmpId = empId
'        authForm.EmpName = empName
'        authForm.AllowPasswordFallback = allowPassword
'        authForm.PasswordHash = passwordHash

'        authForm.ShowDialog()
'        Return authForm
'    End Function
'End Class
