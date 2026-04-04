Imports DevExpress.XtraEditors
Public Class Login
    'Public ini As New IniFile(M_Details.AppPath & "\Settings\" & "Settings.ini")
   

    Public Sub New()
        'Dim dta As New DataTable
        'dta = MySqlDataAdapter("select * from user_table")
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            InitializeComponent()
            ' Apply the current skin to Login form (don't reload, just apply what's already set)
            SkinManager.LoadSkinSetting()
            If _ReadSyncLocalCloud() Then
                txtusername.Properties.DataSource = _JsonData.UserTable
                GridLookUuCompany.Properties.DataSource = _JsonData.CompanyLocationTable
                GridLookUuCompany.EditValue = _companyInfo.LocId
            End If
        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try


    End Sub
    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Try
            Application.Exit()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        Try
            If txtusername.Text = "" OrElse txtusername.Text Is Nothing Then
                XtraMessageBox.Show("User Name Not Valied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ElseIf txtpassword.Text = "" OrElse txtpassword.Text Is Nothing Then
                XtraMessageBox.Show("User Password Not Valied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                If _validateUserLogin(Trim(txtusername.Text), Trim(txtpassword.Text)) = True Then
                    'XtraMessageBox.Show("Welcome " & txtusername.Text & "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Dim _SELECTEDROW = GridLookUuCompany.GetSelectedDataRow

                    _companyInfo.CompanyName = GridLookUuCompany.GetSelectedDataRow(1).ToString

                    _companyInfo.LocationName = GridLookUuCompany.GetSelectedDataRow(3).ToString
                    'ValidationProcess()
                    Me.Hide()
                    MainMaster.Show()

                Else
                    XtraMessageBox.Show("User Id Or Password Is Wrong " & txtusername.Text & "!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtpassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpassword.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                btnlogin_Click(Nothing, Nothing)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnFingerprint_Click(sender As Object, e As EventArgs) Handles btnFingerprint.Click
        Try
            ' Ensure fingerprint data is loaded
            If _JsonData.FingerPrintDataTable Is Nothing OrElse _JsonData.FingerPrintDataTable.Rows.Count = 0 Then
                If Not getFingerPrintData() Then
                    MessageBox.Show("No fingerprint data available. Please register fingerprints first.", "Fingerprint Login", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End If

            ' Show fingerprint capture dialog
            Using scannerForm As New FrmFingerLoginCapture()
                If scannerForm.ShowDialog() = DialogResult.OK AndAlso Not String.IsNullOrEmpty(scannerForm.MatchedFeatureSet) Then
                    Dim matchedUserId As String = scannerForm.MatchedUserId
                    If _JsonData.UserTable.Rows.Count > 0 Then
                        Dim safeUserId As String = matchedUserId.Replace("'", "''")
                        Dim userRow = _JsonData.UserTable.Select("Id = '" & safeUserId & "' AND GroupId IN (1, 2, 3)")
                        If userRow.Length > 0 Then
                            matchedUserId = userRow(0)("Id").ToString()
                        Else
                            matchedUserId = String.Empty
                            MessageBox.Show("Fingerprint matched but user does not have access rights. Please contact administrator.", "Fingerprint Login", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If
                    End If
                    If Not String.IsNullOrEmpty(matchedUserId) Then
                        ' Fingerprint matched - proceed with login
                        Dim _SELECTEDROW = GridLookUuCompany.GetSelectedDataRow
                        _companyInfo.CompanyName = GridLookUuCompany.GetSelectedDataRow(1).ToString
                        _companyInfo.LocationName = GridLookUuCompany.GetSelectedDataRow(3).ToString
                        Me.Hide()
                        MainMaster.Show()
                    Else
                        XtraMessageBox.Show("Fingerprint not recognized. Please try again or use password.", "Fingerprint Login", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End If
            End Using
        Catch ex As Exception
            XtraMessageBox.Show("Fingerprint login error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    
End Class
