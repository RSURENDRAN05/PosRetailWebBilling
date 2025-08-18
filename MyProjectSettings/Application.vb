Namespace My

    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active.
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.

    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            ' Initialize DevExpress Skins and load saved skin setting
            Try
                DevExpress.Skins.SkinManager.EnableFormSkins()

                ' Load the saved skin setting and apply it
                Dim savedSkin As String = SkinManager.GetSavedSkin()
                System.Diagnostics.Debug.WriteLine("Application Startup - Saved skin: " & savedSkin)

                ' Set the UserLookAndFeel to the saved skin
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(savedSkin)
                DevExpress.LookAndFeel.UserLookAndFeel.Default.UseDefaultLookAndFeel = False

                System.Diagnostics.Debug.WriteLine("Application Startup - Applied skin: " & DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName)
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("Error in Application Startup skin loading: " & ex.Message)
            End Try
        End Sub

        Private Sub MyApplication_Shutdown(sender As Object, e As EventArgs) Handles Me.Shutdown
            ' Save skin setting when application closes
            SkinManager.SaveSkinSetting()
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

        ' Method to get available skins
        Public Function GetAvailableSkins() As List(Of String)
            Dim skins As New List(Of String)
            Try
                For Each skin As DevExpress.Skins.SkinContainer In DevExpress.Skins.SkinManager.Default.Skins
                    skins.Add(skin.SkinName)
                Next
            Catch ex As Exception
                ' Fallback list of common skins
                skins.AddRange({"Blue", "Office 2019 Colorful", "Office 2016 Colorful", "DevExpress Style",
                               "Visual Studio 2013 Blue", "Metropolis", "Office 2013", "Black", "Caramel",
                               "Coffee", "Dark Side", "Foggy", "High Contrast", "Liquid Sky", "London Liquid Sky",
                               "McSkin", "Money Twins", "Seven Classic", "Sharp", "Sharp Plus", "Stardust",
                               "Summer 2008", "The Asphalt World", "Valentine", "Xmas 2008 Blue"})
            End Try
            Return skins
        End Function

    End Class

End Namespace
