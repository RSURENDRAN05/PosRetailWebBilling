Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataTable
Imports DevExpress.XtraEditors
Imports System.Threading
Imports PosRetailWebBilling.clssalesProperty

Public Class PosLogin
    Dim richtext As New RichTextBox
    Dim errMsg As String
    Dim strMsg As String
    Dim STEPS As String
    Dim info As New ProcessStartInfo()
    Dim verfication As Boolean = False


    Public Sub New()
        Try
            InitializeComponent()
            ' Apply the current skin to Login form (don't reload, just apply what's already set)
            SkinManager.LoadSkinSetting()
            If _ReadSyncLocalCloud() Then
                txtusername.Properties.DataSource = _JsonData.UserTable
            End If

        Catch ex As Exception

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
            TSoftwareVersion.Text = M_Details.SoftwareVersion '& ":" & M_Details._softVersion & ":" & M_Details._dbVersion & " " & M_Details._servicePack
            TSCompany.Text = _companyInfo.ComId & "-" & _companyInfo.CompanyName
            TSLoc.Text = _companyInfo.LocId & "-" & _companyInfo.LocationName
            TSDate.Text = Date.Now.ToString("dd-MM-yyyy")
            TSMachine.Text = RegistrationDetails._machineId & ":" & RegistrationDetails._localPcname
            TSdayno.Text = "Current Day No :" & _saleSetting._curDayno
            TSshiftno.Text = "Current Shift No:" & _saleSetting._curShiftno
            TSUser.Text = _companyInfo.UserId & "-" & _companyInfo.UserName
            TSdbName.Text = M_Details._dataBasName
            RestWebId.Text = "WebId :" & M_Details._WebId
            If TSCompany.Text = "0-" Or TSLoc.Text = "0-" Or TSdayno.Text = "0-" Or TSdayno.Text = "" Or TSshiftno.Text = "" Or TSshiftno.Text = "0" Then
                Application.Exit()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub logForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            Dim _RegSettingDs = New DataSet
            If File.Exists(M_Details._appPath & "\LayOut\SerSettings.xml") Then
                _RegSettingDs.ReadXml(M_Details._appPath & "\LayOut\SerSettings.xml")
                RegistrationDetails._machineId = _RegSettingDs.Tables(0).Rows(0)(0).ToString
                RegistrationDetails._registerdCounterName = _RegSettingDs.Tables(0).Rows(0)(1).ToString
                RegistrationDetails._localPcname = _RegSettingDs.Tables(0).Rows(0)(1).ToString
                RegistrationDetails._serverClient = _RegSettingDs.Tables(0).Rows(0)(2).ToString
                RegistrationDetails._localcompname = _RegSettingDs.Tables(0).Rows(0)(3).ToString
                RegistrationDetails._active = _RegSettingDs.Tables(0).Rows(0)(6).ToString
                If RegistrationDetails._serverClient = "ORDER" Then
                    RegistrationDetails._paymentPopupActive = False
                Else
                    RegistrationDetails._paymentPopupActive = True
                End If
                If timer_trick() = True Then
                    Me.BeginInvoke(Sub() _processStart())

                Else
                    Dim sw As StreamWriter
                    sw = New StreamWriter(M_Details._logPath & "ApplicationLog.txt", True)
                    sw.WriteLine(LogFileText._richTextBox.Text)
                    sw.Flush()
                    sw.Close()
                    MessageBox.Show("Please Check Error Lock")
                    Application.Exit()
                End If
            Else
                updateStr("Please Check File Name \LoyOut\SerSettings.xml")
                Me.BeginInvoke(Sub() btn_close_Click(Nothing, Nothing))
            End If
             

            lblDateTime.Text = Date.Now
            If File.Exists(M_Details._appPath & "mypos.jpg") Then
                PictureEdit1.Image = Image.FromFile(M_Details._appPath & "mypos.jpg")
            End If
            If _getCurrentShiftDayno(errMsg) = False Then
                EventlogModule.WriteErroLog("_getCurrentShiftDayno Error")
            Else
                TSdayno.Text = "Current Day No :" & _saleSetting._curDayno
                TSshiftno.Text = "Current Shift No:" & _saleSetting._curShiftno
            End If
            WriteAuditLog(_companyInfo.UserId, "Login", "frmLogin")

            If _readSetting(errMsg) Then
                updateStr(errMsg & "profile Read")
            End If
            'MemberCard Sync

            lblCurrency.Text = "RM"

            If _globalSetting._dualscreenoption = True And RegistrationDetails._serverClient = "SERVER" OrElse RegistrationDetails._serverClient = "CLIENT" Then
                If _saleSetting.frmdualClose = 0 Then
                    'frmDualScreen.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(1008, 729)
                    'frmDualScreen.Show()
                    Dim _sercre() As Screen = Screen.AllScreens
                    'Dim _DualScreen As New frmDualScreen
                    Dim bounts As Rectangle
                    If _sercre.Count > 1 Then
                        bounts = _sercre(1).Bounds
                        'frmDualScreen.SetBounds(bounts.X, bounts.Y, bounts.Width, bounts.Height)
                        'frmDualScreen.StartPosition = FormStartPosition.Manual
                        'frmDualScreen.WindowState = FormWindowState.Maximized
                        'frmDualScreen.Show()
                    Else
                        EventlogModule.WriteErroLog(":Second Screen Not Found")
                    End If

                End If
            Else
                EventlogModule.WriteErroLog("Dual Screen Not Found")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function timer_trick() As Boolean
        Try
            _loadStatuBar()

            STEPS = "System Register 1"
            If RegistrationDetails._active <> 1 Then
                updateStr("System Not Registered - Please Contact Admin")
                Return False
            ElseIf M_Details._activationCode.Length = 0 Then
                updateStr("Activation Not Found - Please Contact Admin")
                Return False
            End If

            STEPS = "SP_SYSTEMREGISTER 2"
            Dim _dscheckPc As DataSet
            _dscheckPc = New DataSet
            Dim _Sqlpara(8) As SqlParameter
            _Sqlpara(0) = New SqlParameter("@MODE", "C")
            _Sqlpara(1) = New SqlParameter("@PSR_ID", "0")
            _Sqlpara(2) = New SqlParameter("@PSR_COUNTERNAME", RegistrationDetails._registerdCounterName)
            _Sqlpara(3) = New SqlParameter("@PSR_SERVERCLIENT", "0")
            _Sqlpara(4) = New SqlParameter("@PSR_PCNAME", "0")
            _Sqlpara(5) = New SqlParameter("@PSR_COMPANYID", "0") 'nvarchar
            _Sqlpara(6) = New SqlParameter("@PSR_LOCATIONID", "0") 'nvarchar
            _Sqlpara(7) = New SqlParameter("@PSR_MULTICOMPANY", "0")
            _Sqlpara(8) = New SqlParameter("@PSR_STATUS", "0")
            _dscheckPc = _sqlDataAdapter("[SP_SYSTEMREGISTER]", _Sqlpara, errMsg)
            If _dscheckPc.Tables(0).Rows.Count > 0 Then
                Dim recValue = _dscheckPc.Tables(0).Rows(0)(0).ToString
                Dim serverClient = _dscheckPc.Tables(0).Rows(0)(1).ToString
                Dim countername = _dscheckPc.Tables(0).Rows(0)(2).ToString
                If recValue = 1 Then 'pc availble

                    Select Case serverClient
                        Case "SERVER"
                            If RegistrationDetails._serverClient <> serverClient Then
                                updateStr("Database Name : " & RegistrationDetails._serverClient & ",Register Name:" & serverClient & "Server Not matched")
                                Return False
                            End If
                            If RegistrationDetails._localPcname <> countername Then
                                updateStr("Counter Name Mismatch Please Check sysregister and serSet")
                                Return False
                            End If
                        Case "CLIENT"
                            If RegistrationDetails._serverClient <> serverClient Then
                                updateStr("Database Name : " & RegistrationDetails._serverClient & ",Register Name:" & serverClient & "Client Not matched")
                                Return False
                            ElseIf RegistrationDetails._active = 1 Then
                                updateStr(serverClient)
                                updateStr("Client Verfication done")
                            Else
                                updateStr(serverClient)
                                updateStr("Client Not Activated Please Contact Admin")
                                Return False
                            End If
                            If RegistrationDetails._localPcname <> countername Then
                                updateStr("Counter Name Mismatch Please Check sysregister and serSet")
                                Return False
                            End If
                        Case "ORDER"
                            If RegistrationDetails._serverClient <> serverClient Then
                                updateStr("Database Name : " & RegistrationDetails._serverClient & ",Register Name:" & serverClient & "OrderPc Not matched")
                                Return False
                            ElseIf RegistrationDetails._active = 1 Then
                                updateStr(serverClient)
                                updateStr("Order PC Verfication done")

                            Else
                                updateStr(serverClient)
                                updateStr("Order Not Activated Please Contact Admin")
                                Return False
                            End If
                            If RegistrationDetails._localPcname <> countername Then
                                updateStr("Counter Name Mismatch Please Check sysregister and serSet")
                                Return False
                            End If
                    End Select
                End If
            Else
                'pc not avialble
                'Dim frmregshow As New frmRegistration
                'frmregshow.ShowDialog()
                updateStr("Please Register Your Pc Or Contact Murifa System  Cell : 016 306 4557")
                Return False
            End If



            'STEPS = "sp_general_query2 3"
            'Dim _DsDefaultComLoc As New DataSet
            'Dim _sqlpar(1) As SqlParameter
            '_sqlpar(0) = New SqlParameter("@mode", "DC")
            '_sqlpar(1) = New SqlParameter("@str", RegistrationDetails._localPcname) 'Checking With pos_system_Register Table
            '_DsDefaultComLoc = _sqlDataAdapter2("sp_general_query2", _sqlpar)
            '_DsDefaultComLoc.Tables(0).TableName = "Com"
            '_DsDefaultComLoc.Tables(1).TableName = "Loc"
            'If _DsDefaultComLoc.Tables(0).Rows.Count > 0 Then
            '    _companyInfo.ComId = _DsDefaultComLoc.Tables(0).Rows(0).Item(0).ToString
            '    _companyInfo.CompanyName = _DsDefaultComLoc.Tables(0).Rows(0).Item(1).ToString
            '    _companyInfo.LocId = _DsDefaultComLoc.Tables(1).Rows(0).Item(0).ToString
            '    _companyInfo.LocationName = _DsDefaultComLoc.Tables(1).Rows(0).Item(1).ToString
            'Else
            '    Return False
            '    updateStr("sp_general_query2,DC Company not found")
            'End If

            updateStr("Checking With pos_system_Register Table" & _companyInfo.ComId & " : " & _companyInfo.CompanyName)
            updateStr("Checking With pos_system_Register Table" & _companyInfo.LocId & " : " & _companyInfo.LocationName)

            If (encryDecry.deCry(M_Details._dbStatus) = 0) Then

                updateStr("Software not registred Reg - Please Contact Admin")
                Return False
            End If
            STEPS = "Application 4"
            richtext.Text = ""
            Dim dayst As String = ""
            Dim shiftst As String = ""
            'Threading.Thread.Sleep(500)
            updateStr("Application process started.....")
            updateStr("Software Version=" & M_Details.SoftwareVersion)

            updateStr("Pc Name=" & RegistrationDetails._localPcname)
            updateStr("Application Path=" & M_Details._appPath)
            updateStr("Log Path=" & M_Details._logPath)
            STEPS = "DB Check 5"
            If dbconnectioncls(errMsg) = False Then
                If (M_Details._dbStatus = 0) Then
                    Return False
                    updateStr("DB Setup Not Done")

                End If
                'Else

                '    Dim frmlogin As New frmLogin
                '    frmlogin.ShowDialog()
                '    Me.Close()
                '    Me.Dispose()
            Else
                updateStr("DB Setup Being Verified..")
            End If
            Dim serverType As String = ""
            'If verficatin = True Then
            '    serverType = enDecrpt.deCry(M_Details._severclient)
            '    If enDecrpt.deCry(M_Details._severclient) = RegistrationDetails._serverClient Then
            '        If dblcs.dbconnection(errMsg) = False Then
            '            verficatin = False
            '            updateStr(errMsg & "- ServerType :" & serverType & "-" & RegistrationDetails._localPcname)
            '            Exit Try
            '        Else
            '            verficatin = True
            '        End If
            '    Else
            '        If dblcs.dbconnection(errMsg) = False Then
            '            verficatin = False
            '            updateStr(errMsg & "-" & serverType & "-" & RegistrationDetails._localPcname)
            '            Exit Try
            '        Else
            '            verficatin = True
            '        End If
            '    End If
            'End If
            'STEPS = "Software Verison 5"
            'Dim _dsMypos As DataSet
            '_dsMypos = New DataSet
            'Dim _sqlCheckMypos(0) As SqlParameter
            '_sqlCheckMypos(0) = New SqlParameter("@mode", "S")
            '_dsMypos = _sqlDataAdapter2("sp_mypos", _sqlCheckMypos)
            'If _dsMypos.Tables(0).Rows.Count > 0 Then
            '    Dim _sofVer = _dsMypos.Tables(0).Rows(0)("pm_softversion").ToString
            '    Dim _dbVer = _dsMypos.Tables(0).Rows(0)("pm_dbversion").ToString
            '    Dim _spack = _dsMypos.Tables(0).Rows(0)("pm_patchversion").ToString
            '    Dim _slock = _dsMypos.Tables(0).Rows(0)("pm_lock").ToString
            '    'If _sofVer <> M_Details._softVersion Then
            '    '    updateStr("Software Version Not match")
            '    '    updateStr("SF : " & _sofVer)
            '    '    Return False
            '    'ElseIf _dbVer <> M_Details._dbVersion Then
            '    '    updateStr("Database Version Not match")
            '    '    updateStr("DB : " & _dbVer)
            '    '    Return False
            '    'ElseIf _spack <> M_Details._servicePack Then
            '    '    updateStr("ServicePack Version Not match")
            '    '    updateStr("SP : " & _spack)
            '    '    Return False
            '    'Else
            '    If _slock = 0 Then
            '        updateStr("System Lock")
            '        Return False
            '    End If
            'End If

            STEPS = "Activation Check 6"
            If RegistrationDetails._serverClient = "SERVER" Then

                If M_Details._activationCode.Length > 0 Then
                    Dim rsDecode As String = encryDecry.deCry(M_Details._activationCode)
                    Dim rs() As String = SplitString(rsDecode)
                    updateStr("Activation Code :" & rsDecode)
                    _decryptCode._deCodeServer = rs(0).ToString
                    _decryptCode._deCodeClient = rs(1).ToString
                    _decryptCode._deCodeMainHD = rs(2).ToString
                    _decryptCode._deCodeClienHd = rs(3).ToString
                    _decryptCode._deCodeActivation = rs(4).ToString
                    _decryptCode._deCodeExpireDays = rs(5).ToString

                    Dim endatestr As String = encryDecry.deCry(M_Details._registerDate)
                    Dim endate As DateTime = DateTime.Parse(endatestr)
                    updateStr("End Date" & endatestr)
                    If _decryptCode._deCodeMainHD = _decryptCode._deCodeClienHd Then
                        updateStr("Activation falied for CLID=MAID same..Please Contact Murifa System")
                        verfication = False

                    End If

                    If RegistrationDetails._serverClient = "SERVER" Then
                        Dim locpchdid As String = GetDriveSerialNumber()
                        If _decryptCode._deCodeClienHd = locpchdid Then
                            updateStr(_decryptCode._deCodeServer)
                            updateStr("Activation Code Being Verified..")

                        Else
                            updateStr(_decryptCode._deCodeServer)
                            updateStr("Activation falied for server Pc..Please Contact Murifa System")
                            Return False
                        End If
                        If _decryptCode._deCodeMainHD <> "P@ssw0rd05" Then
                            updateStr("Activation Failed MainHd")
                            Return False
                        Else
                            updateStr("Activation MainHd Verified")

                        End If

                        Select Case _decryptCode._deCodeActivation
                            Case "Trial"
                                Dim days As Integer = DateDiff(DateInterval.Day, endate, Date.Now)
                                If days <= _decryptCode._deCodeExpireDays Then
                                    updateStr("Your Trail Days" & days - _decryptCode._deCodeExpireDays)

                                Else
                                    updateStr("Your Trail Version Expired")
                                    Return False
                                End If
                            Case "Enterprise"
                                Dim days As Integer = DateDiff(DateInterval.Day, endate, Date.Now)
                                If days <= 0 Then
                                    updateStr("Your Enterprise Days" & days - _decryptCode._deCodeExpireDays)
                                Else
                                    updateStr("Your Enterprise Version Expired")
                                    Return False
                                End If
                        End Select
                    End If

                End If
            End If
            STEPS = "Final 7"
            lblmypos.Text = "Please Contact Support: Surendran Developer @ 016 306 4557 Mubarak @ 016 322 0445 " & " Licence Details: " & _decryptCode._deCodeActivation & ";" & _decryptCode._deCodeExpireDays & ":" & RegistrationDetails._serverClient

            Return True
        Catch ex As Exception

            updateStr("SP_SYSTEMREGISTER-" & STEPS & ex.ToString & ":" & errMsg)
            Return False
        End Try
    End Function
    Public Sub _processStart()
        Try
            If Running("AutoSyncSales") = False Then
                If File.Exists(M_Details._appPath & "\AutoSyncSales.exe") Then
                    info.FileName = M_Details._appPath & "\AutoSyncSales.exe"
                    info.WorkingDirectory = M_Details._appPath & "\"
                    Process.Start(info)
                Else
                    properClass.R_Msgstring = "File Not Found :" & M_Details._appPath & "\AutoSyncSales.exe"
                    Dim frmsgOk As New frmMsgBoxOk
                    frmsgOk.ShowDialog()
                End If
            End If

            'If Running("Mail Server") = False Then
            '    If _checkSystemSetting("EXE002") = True Then
            '        If File.Exists(M_Details._appPath & "Mail Server.exe") Then
            '            info.FileName = M_Details._appPath & "Mail Server.exe"
            '            info.WorkingDirectory = M_Details._appPath
            '            Process.Start(info)
            '        Else
            '            properClass.R_Msgstring = "File Not Found :" & M_Details._appPath & "\Mail Server.exe"
            '            Dim frmsgOk As New frmMsgBoxOk
            '            frmsgOk.ShowDialog()
            '        End If
            '    End If
            'End If
            'If Running("MyPosQrWebServiceVS1") = False Then
            '    If _checkSystemSetting("QR001") = True Then
            '        If File.Exists(M_Details._appPath & "MyPosQrWebServiceVS1.exe") Then
            '            info.FileName = M_Details._appPath & "MyPosQrWebServiceVS1.exe"
            '            info.WorkingDirectory = M_Details._appPath
            '            Process.Start(info)
            '        Else
            '            properClass.R_Msgstring = "File Not Found :" & M_Details._appPath & "\MyPosQrWebServiceVS1.exe"
            '            Dim frmsgOk As New frmMsgBoxOk
            '            frmsgOk.ShowDialog()
            '        End If
            '    End If
            'End If
            'If Running("OnlineSalesUpload") = False Then
            '    If _checkSystemSetting("ON001") = True Then
            '        If File.Exists(M_Details._appPath & "OnlineSalesUpload.exe") Then
            '            info.FileName = M_Details._appPath & "OnlineSalesUpload.exe"
            '            info.WorkingDirectory = M_Details._appPath
            '            Process.Start(info)
            '        Else
            '            properClass.R_Msgstring = "File Not Found :" & M_Details._appPath & "\OnlineSalesUpload.exe"
            '            Dim frmsgOk As New frmMsgBoxOk
            '            frmsgOk.ShowDialog()
            '        End If
            '    End If
            'End If
            'If IsSingleInstance(appName) = False Then
            '    Application.Exit()
            'End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function Running(ByRef GetProcessesByName As String) As Boolean
        Try
            Dim Run As Integer = Process.GetProcessesByName(GetProcessesByName).Count
            If Run = 1 Then
                Return True
            Else
                errMsg = "Application Already Open"
                Dim _asname As String = GetProcessesByName
                For Each prog As Process In Process.GetProcessesByName(_asname)
                    If prog.ProcessName.Contains(_asname) = True Then
                        prog.Kill()
                    End If
                Next
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
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

            If txtpassword.Text = "" OrElse String.IsNullOrEmpty(txtpassword.Text) Then
                txtpassword.Select()
                Exit Try
            End If
            
            If AuthenticateUser(txtusername.Text, txtpassword.Text) = True Then
                txtpassword.Text = ""
                TSUser.Text = _companyInfo.UserId & "-" & _companyInfo.UserName
                'txtusername.Text = _companyInfo.UserName
                If _beforeValidationCheck() = True Then
                    If _beforeDayCheck() = True Then
                        Me.Hide()
                        Timer1.Enabled = False
                        GC.SuppressFinalize(Me)
                        PosSalesII.Show()
                    End If
                Else
                    '    'altMsg.altmsg.Show(Me, "Login Check Ver 22.01", "btn_ok_Click", "Validation Problem", altMsg.img.Images(1))
                End If
                'End If
            Else
                txtpassword.Text = ""
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