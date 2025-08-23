Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataTable
Imports DevExpress.XtraEditors
Imports System.Threading
Public Class PosLogin
    Dim richtext As New RichTextBox
    Dim errMsg As String
    Dim strMsg As String
    Dim STEPS As String
    Dim info As New ProcessStartInfo()
    Dim verfication As Boolean = False
    Dim _menuCode As String = 0
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
    Private Sub btn_close_Click(sender As Object, e As EventArgs) Handles btn_close.Click
        Try
            Application.Exit()
        Catch ex As Exception
            WriteErroLog(errMsg & " btnBackUp_Click " & ex.Message)
        End Try

        Try
            Application.Exit()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _loadStatuBar()
        Try
            Dim _SELECTEDROW = GridLookUuCompany.GetSelectedDataRow
            _companyInfo.ComId = CompanyId
            _companyInfo.CompanyName = GridLookUuCompany.GetSelectedDataRow(1).ToString
            _companyInfo.LocId = LocationId
            _companyInfo.LocationName = GridLookUuCompany.GetSelectedDataRow(3).ToString
            TSoftwareVersion.Text = M_Details.SoftwareVersion '& ":" & M_Details._softVersion & ":" & M_Details._dbVersion & " " & M_Details._servicePack
            TSCompany.Text = _companyInfo.ComId & "-" & _companyInfo.CompanyName
            TSLoc.Text = _companyInfo.LocId & "-" & _companyInfo.LocationName
            TSDate.Text = Date.Now.ToString("dd-MM-yyyy")
            TSMachine.Text = Environment.MachineName
            TSdayno.Text = "Current Day No :" & _companyInfo.CurDayNo
            TSshiftno.Text = "Current Shift No:" & _companyInfo.CurShiftNo
            TSUser.Text = _companyInfo.UserId & "-" & _companyInfo.UserName
            TSdbName.Text = "MyposRetailCloud"
            RestWebId.Text = "WebId :" & 0
            If TSCompany.Text = "0-" Or TSLoc.Text = "0-" Or TSdayno.Text = "0-" Or TSdayno.Text = "" Or TSshiftno.Text = "" Or TSshiftno.Text = "0" Then
                Application.Exit()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub logForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'If OtherSetting._dualscreenoption = True And RegistrationDetails._serverClient = "SERVER" OrElse RegistrationDetails._serverClient = "CLIENT" Then
            '    If _functionModule.frmdualClose = 0 Then
            '        'frmDualScreen.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(1008, 729)
            '        'frmDualScreen.Show()
            '        Dim _sercre() As Screen = Screen.AllScreens
            '        'Dim _DualScreen As New frmDualScreen
            '        Dim bounts As Rectangle
            '        If _sercre.Count > 1 Then
            '            bounts = _sercre(1).Bounds
            '            frmDualScreen.SetBounds(bounts.X, bounts.Y, bounts.Width, bounts.Height)
            '            frmDualScreen.StartPosition = FormStartPosition.Manual
            '            frmDualScreen.WindowState = FormWindowState.Maximized
            '            frmDualScreen.Show()
            '        Else
            '            EventlogModule.WriteErroLog(":Second Screen Not Found")
            '        End If

            '    End If
            'Else
            '    EventlogModule.WriteErroLog("Dual Screen Not Found")
            'End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub btn_7_Click(sender As Object, e As EventArgs) Handles btn_7.Click, btn_8.Click, btn_9.Click, btn_6.Click, btn_5.Click, btn_4.Click, btn_3.Click, btn_2.Click, btn_1.Click, btn_0.Click
        Try
            Dim smbtn As SimpleButton
            smbtn = CType(sender, SimpleButton)
            txtpassword.Text = txtpassword.Text + smbtn.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _txtPass_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpassword.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                btn_ok_Click(sender, e)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnClr_Click(sender As Object, e As EventArgs) Handles btnClr.Click
        Try
            txtpassword.Text = ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_ok_Click(sender As Object, e As EventArgs) Handles btn_ok.Click
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
                    PosSalesII.Show()
                Else
                    txtpassword.Text = ""
                    XtraMessageBox.Show("User Id Or Password Is Wrong " & txtusername.Text & "!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            lblDateTime.Text = "Today Date :" & Date.Now
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnPos_Click(sender As Object, e As EventArgs) Handles btnPos.Click
        Try
            Me.Hide()
            Login.Show()
        Catch ex As Exception

        End Try
    End Sub
End Class