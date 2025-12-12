Imports DevExpress.XtraEditors

Namespace My

    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active.
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.

    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            ' Initialize the function module first to avoid circular reference issues
            Try
                InitializeModule()
            Catch ex As Exception
                MsgBox("Error initializing application modules: " & ex.Message & vbCrLf & "Application may not function properly.", MsgBoxStyle.Exclamation)
            End Try

            ' Initialize DevExpress Skins and load saved skin setting
            Try
                DevExpress.Skins.SkinManager.EnableFormSkins()

                ' Load the saved skin setting and apply it
                Dim savedSkin As String = SkinManager.GetSavedSkin()

                ' Set the UserLookAndFeel to the saved skin
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(savedSkin)
                DevExpress.LookAndFeel.UserLookAndFeel.Default.UseDefaultLookAndFeel = False
            Catch ex As Exception
                ' Handle skin initialization errors silently
            End Try

            ' Check database connection
            Try
                If Not CheckForInternetConnection() Then
                    MsgBox("Internet connection failed. Application will continue in offline mode.", MsgBoxStyle.Information)
                    ' Don't exit - allow offline operation
                    ' e.Cancel = True
                    ' Return
                End If
            Catch ex As Exception
                ' Handle initialization errors gracefully
                MsgBox("Error checking internet connection: " & ex.Message & vbCrLf & "Application will continue.", MsgBoxStyle.Exclamation)
            End Try

            ' Set culture/localization
            System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US")

            ' Load settings early in the startup process
            Try
                ReadUserSettings()
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("Error loading settings in startup: " & ex.Message)
            End Try
        End Sub

        Protected Overrides Sub OnCreateMainForm()
            ' This overrides the method in Application.Designer.vb
            ' At this point, settings should already be loaded from Startup event
            Try
                Me.MainForm = New Login()
                'If _globalSetting.PosBillScreenActive = False Then
                '    Me.MainForm = New PosLogin() 'PosLogin() 'FrmUploadSalesAutoSync() frmTest
                'Else
                '    Me.MainForm = New Login()
                'End If
            Catch ex As Exception
                ' Fallback to Login if there's any error
                Me.MainForm = New Login()
            End Try
        End Sub

        Private Sub DetermineStartupForm()
            ' This method now just reads settings - the MainForm is set in Application.Designer.vb
            Try
                ReadUserSettings()
                ' MainForm is already set by OnCreateMainForm in Application.Designer.vb
                ' No need to set it again here
            Catch ex As Exception
                ' Handle errors in reading settings
                System.Diagnostics.Debug.WriteLine("Error in DetermineStartupForm: " & ex.Message)
            End Try
        End Sub

        Private Sub MyApplication_Shutdown(sender As Object, e As EventArgs) Handles Me.Shutdown
            Try
                ' Save application settings - handled automatically by SaveMySettingsOnExit = true
                ' No need to manually call save here
            Catch ex As Exception
                ' Handle save errors silently
            End Try

            ' Close database connections
            CleanupResources()

            ' Log application shutdown
            LogApplicationEvent("Application shutdown completed")
        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            ' Log the error
            LogError("Unhandled Exception", e.Exception.Message)

            ' Show user-friendly error message
            MsgBox("An unexpected error occurred. Please contact support.", MsgBoxStyle.Critical)

            ' Don't exit application for non-critical errors
            e.ExitApplication = False
        End Sub

        Private Sub InitializeDevExpressSkins()
            Try
                ' Enable DevExpress skins
                DevExpress.Skins.SkinManager.EnableFormSkins()
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Blue") ' Set default skin

                ' Alternative skin options you can use:
                ' DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful")
                ' DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2016 Colorful")
                ' DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("DevExpress Style")
                ' DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Visual Studio 2013 Blue")
                ' DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Metropolis")
                ' DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2013")

                ' Apply to all forms automatically
                DevExpress.LookAndFeel.UserLookAndFeel.Default.UseDefaultLookAndFeel = False

            Catch ex As Exception
                ' Handle any initialization errors
                System.Diagnostics.Debug.WriteLine("Error initializing DevExpress skins: " & ex.Message)
            End Try
        End Sub

        ' Method to change skin dynamically at runtime
        Public Sub ChangeSkin(skinName As String)
            Try
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName)
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("Error changing skin: " & ex.Message)
            End Try
        End Sub


        Private Sub CheckForUpdates()
            ' Your update checking logic here
        End Sub

        Private Sub InitializeLogging()
            ' Initialize your logging system here
        End Sub

        Private Sub CleanupResources()
            ' Cleanup code here
        End Sub

        Private Sub LogApplicationEvent(message As String)
            ' Your logging code here
        End Sub

        Private Sub LogError(title As String, message As String)
            ' Your error logging code here
        End Sub




        Dim LocationId As Integer = 0
        Dim CompanyId As Integer = 0
        Dim BillScreen As String = "0"
        Private Function ReadUserSettings() As Boolean
            'Dim dta As New DataTable
            'dta = MySqlDataAdapter("select * from user_table")
            Dim dialog As New DevExpress.Utils.WaitDialogForm()
            Try
                ' Initialize settings safely
                Try
                    M_Details.LinkAjaxRequest = ini.ReadValue("Profile", "UrlLink")
                    M_Details.LinkAjaxRequestCheque = ini.ReadValue("Profile", "UrlLinkCheque")
                    M_Details.licenceServerCleint = ini.ReadValue("Profile", "ServerClient")
                    LocationId = CInt(ini.ReadValue("Bank", "LocationId"))
                    CompanyId = CInt(ini.ReadValue("Bank", "CompanyId"))
                    BillScreen = ini.ReadValue("Bill", "PosBillScreen")
                Catch iniEx As Exception
                    ' Handle INI file reading errors
                    System.Diagnostics.Debug.WriteLine("INI file error: " & iniEx.Message)
                    ' Set default values
                    M_Details.LinkAjaxRequest = "http://localhost/api/"
                    LocationId = 1
                    CompanyId = 1
                    BillScreen = "0"
                End Try

                If BillScreen.ToString = "1" Then
                    _globalSetting.PosBillScreenActive = False
                Else
                    _globalSetting.PosBillScreenActive = True
                End If

                Try
                    If chkRegistryKey(M_Details.licenceActive) = False Then
                        End
                    End If
                Catch regEx As Exception
                    ' Handle registry key check errors
                    System.Diagnostics.Debug.WriteLine("Registry key error: " & regEx.Message)
                    ' Continue without ending application for now
                End Try

                Return True
            Catch ex As Exception
                dialog.Close()
                System.Diagnostics.Debug.WriteLine("ReadUserSettings error: " & ex.Message)
                Return False
            Finally
                dialog.Close()
            End Try
        End Function
    End Class

End Namespace
