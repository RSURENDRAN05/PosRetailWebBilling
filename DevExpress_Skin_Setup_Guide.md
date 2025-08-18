# DevExpress Skin Setup Guide

## Overview
This guide shows how to set up DevExpress skins that apply to all forms in your VB.NET application.

## Files Created/Modified

### 1. My Project\Application.vb
- Handles application startup and shutdown events
- Initializes skins on application start
- Saves skin settings on application close

### 2. ClsModule\SkinManager.vb
- Utility class for managing skins
- Provides methods to set, save, and load skins
- Includes helper methods for UI controls

### 3. My Project\Settings.settings
- Added ApplicationSkin setting to store user preference

### 4. Project File Updates
- Added references to new files

## How to Use

### Method 1: Application-wide Default Skin
The skin is automatically applied to all forms when the application starts via the `Application.vb` file.

### Method 2: Change Skin Programmatically
```vb
' Change skin for entire application
SkinManager.SetSkin("Office 2019 Colorful")

' Or directly using DevExpress API
DevExpress.UserAccess.UserLookAndFeel.Default.SetSkinStyle("Blue")
```

### Method 3: Add Skin Selection to Your Forms
Add a ComboBox to any form and populate it with available skins:

```vb
' In your form's Load event
Private Sub MyForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    SkinManager.PopulateSkinComboBox(cmbSkins)
End Sub

' Handle skin selection
Private Sub cmbSkins_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSkins.SelectedIndexChanged
    If cmbSkins.SelectedItem IsNot Nothing Then
        SkinManager.SetSkin(cmbSkins.SelectedItem.ToString())
    End If
End Sub
```

## Available Skins for DevExpress v13.1

### Popular Skins:
- **Blue** (Default)
- **Office 2010 Blue**
- **Office 2007 Blue**
- **DevExpress Style**
- **Metropolis**
- **Seven Classic**
- **VS2010**

### Dark Themes:
- **Dark Side**
- **DevExpress Dark Style**
- **The Asphalt World**

### Colorful Themes:
- **Caramel**
- **Coffee**
- **Liquid Sky**
- **Money Twins**
- **Stardust**
- **Summer 2008**

## Adding Skin Selection to MainMaster Form

To add a skin selector to your main form, you can:

1. Add a BarButtonItem to the ribbon with a dropdown
2. Add a ComboBox control to the status bar
3. Create a Settings/Preferences dialog

### Example: Add to Ribbon Menu

```vb
' Add this to your MainMaster form
Private Sub AddSkinMenuToRibbon()
    ' This would require adding controls to the designer
    ' Or you can add it programmatically
End Sub
```

## Automatic Features

1. **Auto-Initialize**: Skins are initialized when the application starts
2. **Auto-Save**: Current skin selection is saved when application closes
3. **Auto-Load**: Last used skin is loaded when application starts
4. **Error Handling**: Graceful fallback if skin loading fails

## Benefits

- ✅ **Consistent Look**: All forms use the same skin automatically
- ✅ **User Preference**: Users can choose their preferred skin
- ✅ **Persistent**: Skin choice is remembered between sessions
- ✅ **Professional**: Modern, professional appearance
- ✅ **Easy to Use**: Simple API for changing skins

## Form Requirements

For forms to use skins automatically:
1. Inherit from `DevExpress.XtraEditors.XtraForm` (not `System.Windows.Forms.Form`)
2. Use DevExpress controls where possible
3. No additional code required in individual forms

## Testing

Build and run your application. The skin will be applied automatically to all DevExpress forms and controls.

## Troubleshooting

1. **Skins not applying**: Ensure forms inherit from `XtraForm`
2. **Build errors**: Check that all DevExpress references are properly loaded
3. **Settings not saving**: Verify the Settings.settings file is properly configured
4. **Performance issues**: Some skins may render slower on older hardware

## Future Enhancements

You can extend this system to:
- Add theme previews
- Create custom skins
- Implement per-user skin preferences
- Add skin import/export functionality
- Create a skin editor interface
