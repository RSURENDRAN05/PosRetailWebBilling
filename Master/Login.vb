Imports DevExpress.XtraEditors
Public Class Login
    'Public ini As New IniFile(M_Details.AppPath & "\Settings\" & "Settings.ini")
    Dim LocationId As Integer = 0
    Dim CompanyId As Integer = 0

    Public Sub New()
        'Dim dta As New DataTable
        'dta = MySqlDataAdapter("select * from user_table")
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            InitializeComponent()

            ' Apply the current skin to Login form (don't reload, just apply what's already set)
            SkinManager.LoadSkinSetting()

            M_Details.LinkAjaxRequest = ini.ReadValue("Profile", "UrlLink")
            M_Details.LinkAjaxRequestCheque = ini.ReadValue("Profile", "UrlLinkCheque")
            M_Details.licenceServerCleint = ini.ReadValue("Profile", "ServerClient")
            LocationId = ini.ReadValue("Bank", "LocationId")
            CompanyId = ini.ReadValue("Bank", "CompanyId")
           
            If chkRegistryKey(M_Details.licenceActive) = False Then
                End
            End If
            If CheckForInternetConnection() = False Then
                XtraMessageBox.Show("No Internet Connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            Else
                dialog.Caption = "Getting User Information"
                If getUserInfo() = True Then
                    txtusername.Properties.DataSource = _JsonData.USerTable
                    dialog.Caption = "User Data Received"
                Else
                    dialog.Caption = "User Data Not Received"
                End If
                dialog.Caption = "Getting Company Information"
                If getComapnyLocationInfo() = True Then
                    dialog.Caption = "Company Data Received"
                    GridLookUuCompany.Properties.DataSource = _JsonData.CompanyLocationTable
                Else
                    dialog.Caption = "Company Data Not Received"
                End If
                If getComapnyInfo() = True Then
                    dialog.Caption = "Company Data Received"

                Else
                    dialog.Caption = "Company Data Not Received"
                End If
                dialog.Caption = "Getting Location Information"
                If getLocationInfo() = True Then
                    dialog.Caption = "Location Data Received"
                Else
                    dialog.Caption = "Location Data Not Received"
                End If
                If getPosSettingsInfo() = True Then
                    LoadPosSettings()
                    dialog.Caption = "Loading Pos Settings"
                Else
                    dialog.Caption = "Pos Setting Data Not Received"
                End If
                If getSalesManCommissionInfo() = True Then

                    dialog.Caption = "Loading SalesCommission"
                Else
                    dialog.Caption = "SalesCommission Data Not Received"
                End If
                If getPaymentTermTable() = True Then
                    dialog.Caption = "Loading Payment Term"
                Else
                    dialog.Caption = "Payment Term Data Not Received"
                End If
                GridLookUuCompany.EditValue = LocationId


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
                    _companyInfo.ComId = CompanyId
                    _companyInfo.CompanyName = GridLookUuCompany.GetSelectedDataRow(1).ToString
                    _companyInfo.LocId = LocationId
                    _companyInfo.LocationName = GridLookUuCompany.GetSelectedDataRow(3).ToString
                    ValidationProcess()
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
