Imports DevExpress.Skins
Imports DevExpress.LookAndFeel
Imports DevExpress.Utils
Public Class SkinManager

    Public Shared Sub SaveSkinSetting(Optional skinName As String = "")
        Try
            Dim skinToSave As String = If(String.IsNullOrEmpty(skinName), UserLookAndFeel.Default.SkinName, skinName)
            System.Diagnostics.Debug.WriteLine("SaveSkinSetting - Saving skin: " & skinToSave)

            ' Primary storage: My.Settings (this should work for user settings)
            Try
                My.Settings.Default.ApplicationSkin = skinToSave
                My.Settings.Default.Save()
                System.Diagnostics.Debug.WriteLine("SaveSkinSetting - My.Settings saved successfully: " & skinToSave)
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("SaveSkinSetting - My.Settings failed: " & ex.Message)
            End Try

            ' Backup storage: INI file (always save here too for reliability)
            Try
                Dim iniFile As New IniFile(Application.StartupPath & "\Settings.ini")
                iniFile.WriteValue("Appearance", "ApplicationSkin", skinToSave)
                System.Diagnostics.Debug.WriteLine("SaveSkinSetting - INI file saved successfully: " & skinToSave)
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("SaveSkinSetting - INI file failed: " & ex.Message)
            End Try

            ' Verify the save worked by reading it back
            Dim verifyRead As String = GetSavedSkin()
            System.Diagnostics.Debug.WriteLine("SaveSkinSetting - Verification read: " & verifyRead)
            If verifyRead <> skinToSave Then
                System.Diagnostics.Debug.WriteLine("SaveSkinSetting - WARNING: Verification failed!")
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("SaveSkinSetting - General error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Fallback skins for DevExpress v13.1 when SkinManager enumeration fails
    ''' </summary>
    Public Shared ReadOnly Property PopularSkins() As String()
        Get
            ' Core skins that are definitely available in DevExpress v13.1
            Return {"Blue", "Caramel", "Coffee", "Foggy", "Glass Oceans",
                   "London Liquid Sky", "Metropolis", "Money Twins",
                   "Office 2007 Blue", "Office 2007 Black", "Office 2007 Silver",
                   "Office 2010 Blue", "Office 2010 Black", "Office 2010 Silver",
                   "Seven", "Sharp", "Stardust", "Valentine", "VS2010"}
        End Get
    End Property

    ''' <summary>
    ''' Initialize skins for the entire application
    ''' </summary>
    Public Shared Sub Initialize(Optional defaultSkin As String = "Blue")
        Try
            ' Enable form skins
            DevExpress.Skins.SkinManager.EnableFormSkins()

            ' Set default skin
            SetSkin(defaultSkin)

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error initializing skins: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Set the application skin - Simplified approach for DevExpress v13.1
    ''' </summary>
    ''' <param name="skinName">Name of the skin to apply</param>
    Public Shared Sub SetSkin(skinName As String)
        Try
            System.Diagnostics.Debug.WriteLine("SetSkin called with: " & skinName)

            ' Step 1: Enable form skins (essential for DevExpress v13.1)
            DevExpress.Skins.SkinManager.EnableFormSkins()
            System.Diagnostics.Debug.WriteLine("Form skins enabled")

            ' Step 2: Set the default UserLookAndFeel
            UserLookAndFeel.Default.SetSkinStyle(skinName)
            UserLookAndFeel.Default.UseDefaultLookAndFeel = False
            System.Diagnostics.Debug.WriteLine("UserLookAndFeel set to: " & skinName)

            ' Step 3: Apply to all open forms (critical step)
            For Each form As Form In Application.OpenForms
                Try
                    If TypeOf form Is DevExpress.XtraBars.Ribbon.RibbonForm Then
                        Dim ribbonForm As DevExpress.XtraBars.Ribbon.RibbonForm = DirectCast(form, DevExpress.XtraBars.Ribbon.RibbonForm)
                        ribbonForm.LookAndFeel.SetSkinStyle(skinName)
                        ribbonForm.LookAndFeel.UseDefaultLookAndFeel = False
                        ribbonForm.Update()
                        System.Diagnostics.Debug.WriteLine("Applied skin to RibbonForm: " & form.Name)
                    ElseIf TypeOf form Is DevExpress.XtraEditors.XtraForm Then
                        Dim xtraForm As DevExpress.XtraEditors.XtraForm = DirectCast(form, DevExpress.XtraEditors.XtraForm)
                        xtraForm.LookAndFeel.SetSkinStyle(skinName)
                        xtraForm.LookAndFeel.UseDefaultLookAndFeel = False
                        xtraForm.Update()
                        System.Diagnostics.Debug.WriteLine("Applied skin to XtraForm: " & form.Name)
                    End If
                Catch formEx As Exception
                    System.Diagnostics.Debug.WriteLine("Error applying skin to form " & form.Name & ": " & formEx.Message)
                End Try
            Next

            ' Step 4: Save the setting
            SaveSkinSetting(skinName)

            ' Step 5: Force application refresh
            Application.DoEvents()

            System.Diagnostics.Debug.WriteLine("Skin application completed: " & skinName)
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error in SetSkin: " & ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    ''' <summary>
    ''' Get all available skins from the SkinManager
    ''' </summary>
    ''' <returns>List of available skin names</returns>
    Public Shared Function GetAvailableSkins() As List(Of String)
        Dim skins As New List(Of String)
        Try
            For Each skin As SkinContainer In DevExpress.Skins.SkinManager.Default.Skins
                skins.Add(skin.SkinName)
            Next
        Catch ex As Exception
            ' Fallback to popular skins if unable to enumerate
            skins.AddRange(PopularSkins)
        End Try
        Return skins
    End Function

    ''' <summary>
    ''' Create a skin selection combo box for forms
    ''' </summary>
    ''' <param name="comboBox">ComboBox control to populate</param>
    Public Shared Sub PopulateSkinComboBox(comboBox As DevExpress.XtraEditors.ComboBoxEdit)
        Try
            With comboBox.Properties
                .Items.Clear()
                .Items.AddRange(GetAvailableSkins().ToArray())
                .TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            End With
            comboBox.SelectedItem = UserLookAndFeel.Default.SkinName
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error populating skin combo: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Save current skin setting to application settings
    ''' </summary>
    Public Shared Sub SaveSkinSetting()
        Try
            ' Use INI file instead of My.Settings for more reliable storage
            Dim iniFile As New IniFile(Application.StartupPath & "\Settings.ini")
            iniFile.WriteValue("Appearance", "ApplicationSkin", UserLookAndFeel.Default.SkinName)
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error saving skin setting: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Load skin setting from application settings and apply it
    ''' </summary>
    Public Shared Sub LoadSkinSetting()
        Try
            Dim savedSkin As String = GetSavedSkin()

            ' Always apply the saved skin (or default "Blue" if none saved)
            If Not String.IsNullOrEmpty(savedSkin) Then
                System.Diagnostics.Debug.WriteLine("LoadSkinSetting - Applying saved skin: " & savedSkin)

                ' Enable form skins first
                DevExpress.Skins.SkinManager.EnableFormSkins()

                ' Set the UserLookAndFeel default
                UserLookAndFeel.Default.SetSkinStyle(savedSkin)
                UserLookAndFeel.Default.UseDefaultLookAndFeel = False

                System.Diagnostics.Debug.WriteLine("LoadSkinSetting completed - Current skin: " & UserLookAndFeel.Default.SkinName)
            Else
                System.Diagnostics.Debug.WriteLine("LoadSkinSetting - No saved skin found, using default")
                SetSkin("Blue") ' Ensure we have a default skin
            End If
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error loading skin setting: " & ex.Message)
            ' Fallback to default skin
            Try
                SetSkin("Blue")
            Catch
                ' Silent fallback
            End Try
        End Try
    End Sub

    ''' <summary>
    ''' Get the saved skin setting from application settings
    ''' </summary>
    ''' <returns>The saved skin name, or default "Blue" if not found</returns>
    Public Shared Function GetSavedSkin() As String
        Try
            System.Diagnostics.Debug.WriteLine("GetSavedSkin - Starting...")

            ' Try My.Settings first - this is the primary storage
            Dim savedSkin As String = ""
            Try
                savedSkin = My.Settings.Default.ApplicationSkin
                System.Diagnostics.Debug.WriteLine("GetSavedSkin - My.Settings returned: '" & savedSkin & "'")

                If Not String.IsNullOrEmpty(savedSkin) AndAlso savedSkin <> "Blue" Then
                    System.Diagnostics.Debug.WriteLine("GetSavedSkin - Using My.Settings skin: " & savedSkin)
                    Return savedSkin
                End If
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("GetSavedSkin - My.Settings error: " & ex.Message)
            End Try

            ' Fallback to INI file if My.Settings is empty or default
            Try
                Dim iniFile As New IniFile(Application.StartupPath & "\Settings.ini")
                savedSkin = iniFile.ReadValue("Appearance", "ApplicationSkin")
                System.Diagnostics.Debug.WriteLine("GetSavedSkin - INI file returned: '" & savedSkin & "'")

                If Not String.IsNullOrEmpty(savedSkin) Then
                    System.Diagnostics.Debug.WriteLine("GetSavedSkin - Using INI file skin: " & savedSkin)
                    Return savedSkin
                End If
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("GetSavedSkin - INI file error: " & ex.Message)
            End Try

            ' If nothing found, return default
            System.Diagnostics.Debug.WriteLine("GetSavedSkin - No saved skin found, returning default: Blue")
            Return "Blue"

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("GetSavedSkin - General error: " & ex.Message)
            Return "Blue"
        End Try
    End Function

    ''' <summary>
    ''' Debug method to show current skin status
    ''' </summary>
    Public Shared Function GetCurrentSkinStatus() As String
        Try
            Dim status As New System.Text.StringBuilder()
            status.AppendLine("=== Current Skin Status ===")
            status.AppendLine("UserLookAndFeel.Default.SkinName: " & UserLookAndFeel.Default.SkinName)
            status.AppendLine("UserLookAndFeel.Default.UseDefaultLookAndFeel: " & UserLookAndFeel.Default.UseDefaultLookAndFeel.ToString())
            status.AppendLine("Form Skins Enabled: " & DevExpress.Skins.SkinManager.AllowFormSkins.ToString())
            status.AppendLine("Total Open Forms: " & Application.OpenForms.Count.ToString())
            status.AppendLine("Saved Skin: " & GetSavedSkin())
            Return status.ToString()
        Catch ex As Exception
            Return "Error getting skin status: " & ex.Message
        End Try
    End Function

    ''' <summary>
    ''' Simple test to verify skin persistence is working correctly
    ''' </summary>
    Public Shared Sub TestPersistence(testSkin As String)
        Try
            System.Diagnostics.Debug.WriteLine("=== SIMPLE PERSISTENCE TEST ===")
            System.Diagnostics.Debug.WriteLine("1. Current skin: " & UserLookAndFeel.Default.SkinName)
            System.Diagnostics.Debug.WriteLine("2. Current saved skin: " & GetSavedSkin())

            ' Save the test skin
            SaveSkinSetting(testSkin)

            ' Read it back immediately
            Dim readBack As String = GetSavedSkin()
            System.Diagnostics.Debug.WriteLine("3. After save, read back: " & readBack)

            ' Check if it matches
            If readBack = testSkin Then
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "PERSISTENCE TEST PASSED!" & vbCrLf & vbCrLf &
                    "Saved: " & testSkin & vbCrLf &
                    "Read back: " & readBack & vbCrLf & vbCrLf &
                    "Skin persistence is working correctly.",
                    "Test Result",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "PERSISTENCE TEST FAILED!" & vbCrLf & vbCrLf &
                    "Saved: " & testSkin & vbCrLf &
                    "Read back: " & readBack & vbCrLf & vbCrLf &
                    "Check Debug Output for details.",
                    "Test Result",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)
            End If

            System.Diagnostics.Debug.WriteLine("=== TEST COMPLETE ===")
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("TestPersistence error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Comprehensive test method to verify skin persistence - Call this to debug saving/loading issues
    ''' </summary>
    Public Shared Sub TestSkinPersistence(testSkinName As String)
        Try
            System.Diagnostics.Debug.WriteLine("=== SKIN PERSISTENCE TEST START ===")
            System.Diagnostics.Debug.WriteLine("Testing with skin: " & testSkinName)

            Dim testResults As New System.Text.StringBuilder()
            testResults.AppendLine("=== SKIN PERSISTENCE TEST RESULTS ===")
            testResults.AppendLine("Testing skin: " & testSkinName)
            testResults.AppendLine()

            ' Step 1: Show current state
            System.Diagnostics.Debug.WriteLine("BEFORE TEST:")
            System.Diagnostics.Debug.WriteLine(GetCurrentSkinStatus())

            ' Step 2: Test saving to My.Settings
            Try
                My.Settings.Default.ApplicationSkin = testSkinName
                My.Settings.Default.Save()
                System.Diagnostics.Debug.WriteLine("[OK] Successfully saved to My.Settings: " & testSkinName)
                testResults.AppendLine("[OK] My.Settings Save: SUCCESS")
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("[ERROR] Error saving to My.Settings: " & ex.Message)
                testResults.AppendLine("[ERROR] My.Settings Save: FAILED - " & ex.Message)
            End Try

            ' Step 3: Test saving to INI file fallback
            Try
                Dim iniFile As New IniFile(Application.StartupPath & "\Settings.ini")
                iniFile.WriteValue("Appearance", "ApplicationSkin", testSkinName)
                System.Diagnostics.Debug.WriteLine("[OK] Successfully saved to INI file: " & testSkinName)
                testResults.AppendLine("[OK] INI File Save: SUCCESS")
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("[ERROR] Error saving to INI file: " & ex.Message)
                testResults.AppendLine("[ERROR] INI File Save: FAILED - " & ex.Message)
            End Try

            ' Step 4: Test reading from My.Settings
            Try
                Dim readFromSettings As String = My.Settings.Default.ApplicationSkin
                System.Diagnostics.Debug.WriteLine("[OK] Read from My.Settings: " & readFromSettings)
                If readFromSettings = testSkinName Then
                    testResults.AppendLine("[OK] My.Settings Read: SUCCESS - " & readFromSettings)
                Else
                    testResults.AppendLine("[ERROR] My.Settings Read: MISMATCH - Expected: " & testSkinName & ", Got: " & readFromSettings)
                End If
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("[ERROR] Error reading from My.Settings: " & ex.Message)
                testResults.AppendLine("[ERROR] My.Settings Read: FAILED - " & ex.Message)
            End Try

            ' Step 5: Test reading from INI file
            Try
                Dim iniFile As New IniFile(Application.StartupPath & "\Settings.ini")
                Dim readFromIni As String = iniFile.ReadValue("Appearance", "ApplicationSkin")
                System.Diagnostics.Debug.WriteLine("[OK] Read from INI file: " & readFromIni)
                If readFromIni = testSkinName Then
                    testResults.AppendLine("[OK] INI File Read: SUCCESS - " & readFromIni)
                Else
                    testResults.AppendLine("[ERROR] INI File Read: MISMATCH - Expected: " & testSkinName & ", Got: " & readFromIni)
                End If
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("[ERROR] Error reading from INI file: " & ex.Message)
                testResults.AppendLine("[ERROR] INI File Read: FAILED - " & ex.Message)
            End Try

            ' Step 6: Test GetSavedSkin method
            Try
                Dim retrievedSkin As String = GetSavedSkin()
                System.Diagnostics.Debug.WriteLine("[OK] GetSavedSkin() returned: " & retrievedSkin)
                If retrievedSkin = testSkinName Then
                    testResults.AppendLine("[OK] GetSavedSkin(): SUCCESS - " & retrievedSkin)
                Else
                    testResults.AppendLine("[ERROR] GetSavedSkin(): MISMATCH - Expected: " & testSkinName & ", Got: " & retrievedSkin)
                End If
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("[ERROR] Error in GetSavedSkin(): " & ex.Message)
                testResults.AppendLine("[ERROR] GetSavedSkin(): FAILED - " & ex.Message)
            End Try

            ' Step 7: File system check
            Try
                Dim settingsPath As String = Application.StartupPath & "\Settings.ini"
                If IO.File.Exists(settingsPath) Then
                    System.Diagnostics.Debug.WriteLine("[OK] Settings.ini file exists at: " & settingsPath)
                    testResults.AppendLine("[OK] Settings.ini file exists")
                Else
                    System.Diagnostics.Debug.WriteLine("[ERROR] Settings.ini file NOT found at: " & settingsPath)
                    testResults.AppendLine("[ERROR] Settings.ini file NOT found")
                End If
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("Error checking Settings.ini file: " & ex.Message)
                testResults.AppendLine("[ERROR] File check error: " & ex.Message)
            End Try

            ' Show final state
            System.Diagnostics.Debug.WriteLine("AFTER TEST:")
            System.Diagnostics.Debug.WriteLine(GetCurrentSkinStatus())
            System.Diagnostics.Debug.WriteLine("=== SKIN PERSISTENCE TEST END ===")

            ' Show results in message box
            testResults.AppendLine()
            testResults.AppendLine("Current skin: " & UserLookAndFeel.Default.SkinName)
            testResults.AppendLine("Saved skin: " & GetSavedSkin())

            DevExpress.XtraEditors.XtraMessageBox.Show(
                testResults.ToString(),
                "Skin Persistence Test Results",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error in TestSkinPersistence: " & ex.Message)
            DevExpress.XtraEditors.XtraMessageBox.Show(
                "Error in TestSkinPersistence: " & ex.Message,
                "Test Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Test method to verify skin functionality - Call this for troubleshooting
    ''' </summary>
    Public Shared Sub TestSkinApplication(skinName As String)
        Try
            System.Diagnostics.Debug.WriteLine("=== SKIN TEST START ===")
            System.Diagnostics.Debug.WriteLine("Testing skin: " & skinName)

            ' Test 1: Check if skin exists
            Dim availableSkins = GetAvailableSkins()
            If availableSkins.Contains(skinName) Then
                System.Diagnostics.Debug.WriteLine(" Skin exists in available skins")
            Else
                System.Diagnostics.Debug.WriteLine("  Skin NOT found in available skins")
                System.Diagnostics.Debug.WriteLine("Available skins: " & String.Join(", ", availableSkins.ToArray()))
            End If

            ' Test 2: Try to set the skin
            DevExpress.Skins.SkinManager.EnableFormSkins()
            UserLookAndFeel.Default.SetSkinStyle(skinName)
            System.Diagnostics.Debug.WriteLine(" SetSkinStyle completed")

            ' Test 3: Check if it was applied
            System.Diagnostics.Debug.WriteLine("Current skin after setting: " & UserLookAndFeel.Default.SkinName)

            ' Test 4: Show form information
            For Each form As Form In Application.OpenForms
                System.Diagnostics.Debug.WriteLine("Form: " & form.Name & " Type: " & form.GetType().Name)
            Next

            System.Diagnostics.Debug.WriteLine("=== SKIN TEST END ===")
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error in TestSkinApplication: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Apply skin to all currently open forms
    ''' </summary>
    ''' <param name="skinName">Name of the skin to apply</param>
    Private Shared Sub ApplySkinToAllForms(skinName As String)
        Try
            For Each form As Form In Application.OpenForms
                ' Apply to DevExpress forms
                If TypeOf form Is DevExpress.XtraEditors.XtraForm Then
                    Dim xtraForm As DevExpress.XtraEditors.XtraForm = DirectCast(form, DevExpress.XtraEditors.XtraForm)
                    xtraForm.LookAndFeel.SetSkinStyle(skinName)
                    xtraForm.LookAndFeel.UseDefaultLookAndFeel = False
                ElseIf TypeOf form Is DevExpress.XtraBars.Ribbon.RibbonForm Then
                    Dim ribbonForm As DevExpress.XtraBars.Ribbon.RibbonForm = DirectCast(form, DevExpress.XtraBars.Ribbon.RibbonForm)
                    ribbonForm.LookAndFeel.SetSkinStyle(skinName)
                    ribbonForm.LookAndFeel.UseDefaultLookAndFeel = False
                End If

                ' Apply skin to all DevExpress controls on the form
                ApplySkinToControls(form, skinName)

                ' Force complete form refresh
                form.Invalidate(True)
                form.Update()
                form.Refresh()
            Next

            System.Diagnostics.Debug.WriteLine("Applied skin to " & Application.OpenForms.Count & " forms")
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error applying skin to forms: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Apply skin to all DevExpress controls on a form recursively
    ''' </summary>
    Private Shared Sub ApplySkinToControls(container As Control, skinName As String)
        Try
            For Each ctrl As Control In container.Controls
                ' Apply to specific DevExpress control types
                If TypeOf ctrl Is DevExpress.XtraEditors.BaseEdit Then
                    Dim baseEdit As DevExpress.XtraEditors.BaseEdit = DirectCast(ctrl, DevExpress.XtraEditors.BaseEdit)
                    baseEdit.Properties.LookAndFeel.SetSkinStyle(skinName)
                    baseEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = False
                ElseIf TypeOf ctrl Is DevExpress.XtraGrid.GridControl Then
                    Dim gridControl As DevExpress.XtraGrid.GridControl = DirectCast(ctrl, DevExpress.XtraGrid.GridControl)
                    gridControl.LookAndFeel.SetSkinStyle(skinName)
                    gridControl.LookAndFeel.UseDefaultLookAndFeel = False
                ElseIf TypeOf ctrl Is DevExpress.XtraLayout.LayoutControl Then
                    Dim layoutControl As DevExpress.XtraLayout.LayoutControl = DirectCast(ctrl, DevExpress.XtraLayout.LayoutControl)
                    layoutControl.LookAndFeel.SetSkinStyle(skinName)
                    layoutControl.LookAndFeel.UseDefaultLookAndFeel = False
                ElseIf TypeOf ctrl Is DevExpress.XtraNavBar.NavBarControl Then
                    Dim navBarControl As DevExpress.XtraNavBar.NavBarControl = DirectCast(ctrl, DevExpress.XtraNavBar.NavBarControl)
                    navBarControl.LookAndFeel.SetSkinStyle(skinName)
                    navBarControl.LookAndFeel.UseDefaultLookAndFeel = False
                End If

                ' Recursively apply to child controls
                If ctrl.HasChildren Then
                    ApplySkinToControls(ctrl, skinName)
                End If
            Next
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error applying skin to controls: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Apply skin to a newly created form - Call this in form constructors or Load events
    ''' </summary>
    ''' <param name="form">The form to apply skin to</param>
    Public Shared Sub ApplySkinToNewForm(form As Form)
        Try
            If form Is Nothing Then Return

            ' Get the current application skin (from UserLookAndFeel or saved settings)
            Dim currentSkin As String = UserLookAndFeel.Default.SkinName
            If String.IsNullOrEmpty(currentSkin) OrElse currentSkin = "Default" Then
                currentSkin = GetSavedSkin()
                If String.IsNullOrEmpty(currentSkin) Then currentSkin = "Blue"
            End If

            System.Diagnostics.Debug.WriteLine("ApplySkinToNewForm - Applying skin '" & currentSkin & "' to form: " & form.Name)

            ' Apply skin based on form type
            If TypeOf form Is DevExpress.XtraEditors.XtraForm Then
                Dim xtraForm As DevExpress.XtraEditors.XtraForm = DirectCast(form, DevExpress.XtraEditors.XtraForm)
                xtraForm.LookAndFeel.SetSkinStyle(currentSkin)
                xtraForm.LookAndFeel.UseDefaultLookAndFeel = False
                System.Diagnostics.Debug.WriteLine("Applied skin to XtraForm: " & form.Name & " (" & currentSkin & ")")
            ElseIf TypeOf form Is DevExpress.XtraBars.Ribbon.RibbonForm Then
                Dim ribbonForm As DevExpress.XtraBars.Ribbon.RibbonForm = DirectCast(form, DevExpress.XtraBars.Ribbon.RibbonForm)
                ribbonForm.LookAndFeel.SetSkinStyle(currentSkin)
                ribbonForm.LookAndFeel.UseDefaultLookAndFeel = False
                System.Diagnostics.Debug.WriteLine("Applied skin to RibbonForm: " & form.Name & " (" & currentSkin & ")")
            End If

            ' Apply skin to all DevExpress controls on the form
            ApplySkinToControls(form, currentSkin)

            ' Force form refresh to ensure skin is visible
            form.Invalidate(True)
            form.Refresh()
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error applying skin to new form: " & ex.Message)
        End Try
    End Sub

End Class
