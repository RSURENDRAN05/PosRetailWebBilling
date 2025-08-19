Imports DevExpress.Skins
Imports DevExpress.LookAndFeel
Imports DevExpress.Utils

Public Class SkinManager


    ''' <summary>
    ''' Initialize skins for the entire application
    ''' </summary>
    Public Shared Sub Initialize(Optional defaultSkin As String = "Blue")
        Try
            DevExpress.Skins.SkinManager.EnableFormSkins()
            LoadSkinSetting()
        Catch ex As Exception
            ' Silent error handling
        End Try
    End Sub

    ''' <summary>
    ''' Set the application skin and save the setting (for user-initiated changes)
    ''' </summary>
    Public Shared Sub SetSkin(skinName As String)
        Try
            ' Enable form skins
            DevExpress.Skins.SkinManager.EnableFormSkins()

            ' Set the default UserLookAndFeel
            UserLookAndFeel.Default.SetSkinStyle(skinName)
            UserLookAndFeel.Default.UseDefaultLookAndFeel = False

            ' Apply to all open forms
            ApplySkinToForms(skinName)

            ' Save the setting
            SaveSkinSetting(skinName)

            ' Force application refresh
            Application.DoEvents()

        Catch ex As Exception
            ' Silent error handling
        End Try
    End Sub

    ''' <summary>
    ''' Apply skin without saving (for loading saved settings)
    ''' </summary>
    Public Shared Sub ApplySkin(skinName As String)
        Try
            ' Enable form skins
            DevExpress.Skins.SkinManager.EnableFormSkins()

            ' Set the default UserLookAndFeel
            UserLookAndFeel.Default.SetSkinStyle(skinName)
            UserLookAndFeel.Default.UseDefaultLookAndFeel = False

            ' Apply to all open forms
            ApplySkinToForms(skinName)

            ' Force application refresh
            Application.DoEvents()

        Catch ex As Exception
            ' Silent error handling
        End Try
    End Sub

    ''' <summary>
    ''' Get all available skins from the SkinManager
    ''' </summary>
    Public Shared Function GetAvailableSkins() As List(Of String)
        Dim skins As New List(Of String)
        Try
            For Each skin As SkinContainer In DevExpress.Skins.SkinManager.Default.Skins
                skins.Add(skin.SkinName)
            Next
        Catch ex As Exception

        End Try
        Return skins
    End Function



    ''' <summary>
    ''' Save skin setting to both My.Settings and INI file
    ''' </summary>
    Public Shared Sub SaveSkinSetting(skinName As String)
        Try
            ' Primary storage: My.Settings
            Try
                My.Settings.Default.ApplicationSkin = skinName
                My.Settings.Default.Save()
            Catch ex As Exception
                ' Silent error handling
            End Try

            ' Backup storage: INI file
            Try
                Dim iniFile As New IniFile(Application.StartupPath & "\Settings.ini")
                iniFile.WriteValue("Appearance", "ApplicationSkin", skinName)
            Catch ex As Exception
                ' Silent error handling
            End Try

        Catch ex As Exception
            ' Silent error handling
        End Try
    End Sub

    ''' <summary>
    ''' Load skin setting from application settings and apply it
    ''' </summary>
    Public Shared Sub LoadSkinSetting()
        Try
            Dim savedSkin As String = GetSavedSkin()

            If Not String.IsNullOrEmpty(savedSkin) Then
                ApplySkin(savedSkin)
            Else
                ApplySkin("Blue") ' Default skin
            End If
        Catch ex As Exception
            ' Silent error handling
            Try
                ApplySkin("Blue")
            Catch
                ' Silent fallback
            End Try
        End Try
    End Sub

    ''' <summary>
    ''' Get the saved skin setting from application settings
    ''' </summary>
    Public Shared Function GetSavedSkin() As String
        Try
            ' Try My.Settings first
            Dim savedSkin As String = ""
            Try
                savedSkin = My.Settings.Default.ApplicationSkin
                If Not String.IsNullOrEmpty(savedSkin) AndAlso savedSkin <> "Blue" Then
                    Return savedSkin
                End If
            Catch ex As Exception
                ' Silent error handling
            End Try

            ' Fallback to INI file
            Try
                Dim iniFile As New IniFile(Application.StartupPath & "\Settings.ini")
                savedSkin = iniFile.ReadValue("Appearance", "ApplicationSkin")
                If Not String.IsNullOrEmpty(savedSkin) Then
                    Return savedSkin
                End If
            Catch ex As Exception
                ' Silent error handling
            End Try

            Return "Blue" ' Default fallback

        Catch ex As Exception
            ' Silent error handling
            Return "Blue"
        End Try
    End Function

    ''' <summary>
    ''' Apply skin to forms and controls - handles both single form and all open forms
    ''' </summary>
    Public Shared Sub ApplySkinToForms(skinName As String, Optional specificForm As Form = Nothing)
        Try
            Dim formsToProcess As New List(Of Form)

            If specificForm IsNot Nothing Then
                formsToProcess.Add(specificForm)
            Else
                ' Process all open forms
                For Each form As Form In Application.OpenForms
                    formsToProcess.Add(form)
                Next
            End If

            For Each form As Form In formsToProcess
                ' Apply to DevExpress forms
                If TypeOf form Is DevExpress.XtraEditors.XtraForm Then
                    DirectCast(form, DevExpress.XtraEditors.XtraForm).LookAndFeel.SetSkinStyle(skinName)
                    DirectCast(form, DevExpress.XtraEditors.XtraForm).LookAndFeel.UseDefaultLookAndFeel = False
                ElseIf TypeOf form Is DevExpress.XtraBars.Ribbon.RibbonForm Then
                    DirectCast(form, DevExpress.XtraBars.Ribbon.RibbonForm).LookAndFeel.SetSkinStyle(skinName)
                    DirectCast(form, DevExpress.XtraBars.Ribbon.RibbonForm).LookAndFeel.UseDefaultLookAndFeel = False
                End If

                ' Apply skin to all DevExpress controls recursively
                ApplyToControls(form, skinName)

                ' Refresh form
                form.Invalidate(True)
                form.Update()
            Next
        Catch ex As Exception
            ' Silent error handling for production
        End Try
    End Sub

    ''' <summary>
    ''' Apply skin to DevExpress controls recursively
    ''' </summary>
    Private Shared Sub ApplyToControls(container As Control, skinName As String)
        For Each ctrl As Control In container.Controls
            ' Apply to DevExpress controls
            If TypeOf ctrl Is DevExpress.XtraEditors.BaseEdit Then
                DirectCast(ctrl, DevExpress.XtraEditors.BaseEdit).Properties.LookAndFeel.SetSkinStyle(skinName)
                DirectCast(ctrl, DevExpress.XtraEditors.BaseEdit).Properties.LookAndFeel.UseDefaultLookAndFeel = False
            ElseIf TypeOf ctrl Is DevExpress.XtraGrid.GridControl Then
                DirectCast(ctrl, DevExpress.XtraGrid.GridControl).LookAndFeel.SetSkinStyle(skinName)
                DirectCast(ctrl, DevExpress.XtraGrid.GridControl).LookAndFeel.UseDefaultLookAndFeel = False
            ElseIf TypeOf ctrl Is DevExpress.XtraLayout.LayoutControl Then
                DirectCast(ctrl, DevExpress.XtraLayout.LayoutControl).LookAndFeel.SetSkinStyle(skinName)
                DirectCast(ctrl, DevExpress.XtraLayout.LayoutControl).LookAndFeel.UseDefaultLookAndFeel = False
            ElseIf TypeOf ctrl Is DevExpress.XtraNavBar.NavBarControl Then
                DirectCast(ctrl, DevExpress.XtraNavBar.NavBarControl).LookAndFeel.SetSkinStyle(skinName)
                DirectCast(ctrl, DevExpress.XtraNavBar.NavBarControl).LookAndFeel.UseDefaultLookAndFeel = False
            End If

            ' Process child controls
            If ctrl.HasChildren Then
                ApplyToControls(ctrl, skinName)
            End If
        Next
    End Sub

End Class
