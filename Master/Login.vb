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
                XtraMessageBox.Show("User Name Not Valied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    
End Class
