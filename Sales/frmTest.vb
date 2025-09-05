Public Class frmTest
#Region "ButtonProperties"
    ' ================================
    ' === Button Properties Class ===
    ' ================================
    Public Class ButtonProperties
        Public Property Id As Integer
        Public Property MenuType As String ' "Main", "Sub", "Item"
        Public Property ItemName As String
        Public Property ButtonWidth As Integer = 120
        Public Property ButtonHeight As Integer = 60
        Public Property FontSize As Single = 10.0F
        Public Property FontName As String = "Segoe UI"
        Public Property FontStyle As String = "Regular"

        ' Text Color ARGB
        Public Property TextColorA As Integer = 255
        Public Property TextColorR As Integer = 0
        Public Property TextColorG As Integer = 0
        Public Property TextColorB As Integer = 0

        ' Background Color ARGB
        Public Property BackColorA As Integer = 255
        Public Property BackColorR As Integer = 240
        Public Property BackColorG As Integer = 240
        Public Property BackColorB As Integer = 240

        Public Property Position As Integer = 0

        ' === Helper Methods ===
        Public Sub SetTextColor(color As Color)
            TextColorA = color.A
            TextColorR = color.R
            TextColorG = color.G
            TextColorB = color.B
        End Sub

        Public Sub SetBackColor(color As Color)
            BackColorA = color.A
            BackColorR = color.R
            BackColorG = color.G
            BackColorB = color.B
        End Sub

        Public Function GetTextColor() As Color
            Return Color.FromArgb(TextColorA, TextColorR, TextColorG, TextColorB)
        End Function

        Public Function GetBackColor() As Color
            Return Color.FromArgb(BackColorA, BackColorR, BackColorG, BackColorB)
        End Function
    End Class

#End Region
    ' Current selected button for properties
    Private selectedButton As DevExpress.XtraEditors.SimpleButton
    Private selectedButtonProperties As ButtonProperties
#Region "InitalLoad"
    Private Sub frmTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _ReadDefaultLocalData()
            LoadMainMenu()
        Catch ex As Exception

        End Try
    End Sub


    ' ============ Load Main Menu ============
    Private Sub LoadMainMenu()
        Try
            PanelMainMenu.Controls.Clear()

            Dim btnWidth As Integer = 120
            Dim btnHeight As Integer = 60
            Dim spacing As Integer = 5
            Dim cols As Integer = 4
            Dim marginLeft As Integer = 10
            Dim marginTop As Integer = 10

            ' Group by MainId, MainName
            Dim mains = _JsonData.ItemTouchMasterTable.AsEnumerable().
                GroupBy(Function(r) New With {
                    Key .MainId = Convert.ToInt32(r("MainId")),
                    Key .MainName = r("MainName").ToString()
                }).
                Select(Function(g) g.Key).ToList()

            For i As Integer = 0 To mains.Count - 1
                Dim row As Integer = i \ cols
                Dim col As Integer = i Mod cols

                ' Count how many buttons in this row
                Dim countInRow As Integer =
                    If(i + cols < mains.Count, cols, mains.Count - row * cols)

                ' Total width of this row
                Dim rowWidth As Integer = (countInRow * btnWidth) + ((countInRow - 1) * spacing)

                ' Center horizontally
                Dim startX As Integer = Math.Max(0, (PanelMainMenu.Width - rowWidth) \ 2)

                ' Create button
                Dim btn As New DevExpress.XtraEditors.SimpleButton()
                btn.Text = mains(i).MainName
                btn.Tag = mains(i).MainId
                btn.Size = New Size(btnWidth, btnHeight)
                btn.Location = New Point(marginLeft + col * (btnWidth + spacing),
                             marginTop + row * (btnHeight + spacing))

                ' Enable DevExpress appearance options
                btn.Appearance.Options.UseBackColor = True
                btn.Appearance.Options.UseForeColor = True
                btn.Appearance.Options.UseFont = True

                ' Apply default style
                btn.Appearance.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                btn.Appearance.ForeColor = Color.White
                btn.Appearance.BackColor = Color.DarkSlateBlue

                ' Try to load saved properties from PHP
                'Try
                '    LoadButtonPropertiesFromPHP(mains(i).MainId, "Main")
                '    If selectedButtonProperties IsNot Nothing Then
                '        ApplyPropertiesFromObjectToButton(btn, selectedButtonProperties)
                '    End If
                'Catch
                '    ' Use default if loading fails
                'End Try

                '' Add context menu for properties
                'AddContextMenuToButton(btn)

                AddHandler btn.Click, AddressOf MainMenu_Click
                PanelMainMenu.Controls.Add(btn)
            Next

            PanelMainMenu.AutoScroll = True

            ' Auto-load first MainId
            If mains.Count > 0 Then
                LoadSubMenu(mains(0).MainId)
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading Main Menu: " & ex.Message)
        End Try
    End Sub


    ' ============ Load Sub Menu ============
    Private Sub LoadSubMenu(mainId As Integer)
        Try
            PanelSubMenu.Controls.Clear()

            Dim btnWidth As Integer = 110
            Dim btnHeight As Integer = 50
            Dim spacing As Integer = 5
            Dim cols As Integer = 1
            Dim marginLeft As Integer = 10
            Dim marginTop As Integer = 10
            ' Group by CateId, CateName under selected MainId
            Dim subs = _JsonData.ItemTouchMasterTable.AsEnumerable().
                Where(Function(r) Convert.ToInt32(r("MainId")) = mainId).
                GroupBy(Function(r) New With {
                    Key .CateId = Convert.ToInt32(r("CateId")),
                    Key .CateName = r("CateName").ToString()
                }).
                Select(Function(g) g.Key).ToList()

            For i As Integer = 0 To subs.Count - 1
                Dim row As Integer = i \ cols
                Dim col As Integer = i Mod cols

                Dim btn As New DevExpress.XtraEditors.SimpleButton()
                btn.Text = subs(i).CateName
                btn.Tag = subs(i).CateId
                btn.Size = New Size(btnWidth, btnHeight)
                btn.Location = New Point(marginLeft + col * (btnWidth + spacing),
                              marginTop + row * (btnHeight + spacing))

                ' Enable DevExpress appearance options
                btn.Appearance.Options.UseBackColor = True
                btn.Appearance.Options.UseForeColor = True
                btn.Appearance.Options.UseFont = True

                ' Apply default style
                btn.Appearance.Font = New Font("Segoe UI", 9, FontStyle.Regular)
                btn.Appearance.ForeColor = Color.Black
                btn.Appearance.BackColor = Color.LightSteelBlue

                '' Try to load saved properties from PHP
                'Try
                '    LoadButtonPropertiesFromPHP(subs(i).CateId, "Sub")
                '    If selectedButtonProperties IsNot Nothing Then
                '        ApplyPropertiesFromObjectToButton(btn, selectedButtonProperties)
                '    End If
                'Catch
                '    ' Use default if loading fails
                'End Try



                AddHandler btn.Click, AddressOf SubMenu_Click
                PanelSubMenu.Controls.Add(btn)
            Next

            PanelSubMenu.AutoScroll = True

            ' ✅ Auto-load first CateId
            If subs.Count > 0 Then
                LoadItemMenu(subs(0).CateId)
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading Sub Menu: " & ex.Message)
        End Try
    End Sub

    ' ============ Load Item Menu ============
    Private Sub LoadItemMenu(cateId As Integer)
        Try
            PanelItemMenu.Controls.Clear()

            Dim btnWidth As Integer = 100
            Dim btnHeight As Integer = 45
            Dim spacing As Integer = 5
            Dim cols As Integer = 5
            Dim marginLeft As Integer = 10
            Dim marginTop As Integer = 10
            ' Items filtered by CateId
            Dim items = _JsonData.ItemTouchMasterTable.AsEnumerable().
                Where(Function(r) Convert.ToInt32(r("CateId")) = cateId).
                Select(Function(r) New With {
                    .Id = Convert.ToInt32(r("Id")),
                    .ItemName = r("ItemName").ToString(),
                    .Position = Convert.ToInt32(r("Position"))
                }).OrderBy(Function(x) x.Position).ToList()

            For i As Integer = 0 To items.Count - 1
                Dim row As Integer = i \ cols
                Dim col As Integer = i Mod cols

                Dim btn As New DevExpress.XtraEditors.SimpleButton()
                btn.Text = items(i).ItemName
                btn.Tag = items(i).Id
                btn.Size = New Size(btnWidth, btnHeight)
                btn.Location = New Point(marginLeft + col * (btnWidth + spacing),
                              marginTop + row * (btnHeight + spacing))

                ' Enable DevExpress appearance options
                btn.Appearance.Options.UseBackColor = True
                btn.Appearance.Options.UseForeColor = True
                btn.Appearance.Options.UseFont = True

                ' Apply default style
                btn.Appearance.Font = New Font("Segoe UI", 8, FontStyle.Regular)
                btn.Appearance.ForeColor = Color.DarkBlue
                btn.Appearance.BackColor = Color.Beige

                '' Try to load saved properties from PHP
                'Try
                '    LoadButtonPropertiesFromPHP(items(i).Id, "Item")
                '    If selectedButtonProperties IsNot Nothing Then
                '        ApplyPropertiesFromObjectToButton(btn, selectedButtonProperties)
                '    End If
                'Catch
                '    ' Use default if loading fails
                'End Try


                AddHandler btn.Click, AddressOf ItemMenu_Click
                PanelItemMenu.Controls.Add(btn)
            Next

            PanelItemMenu.AutoScroll = True

            ' ✅ Auto-select first item
            If items.Count > 0 Then
                ItemMenu_Click(PanelItemMenu.Controls(0), EventArgs.Empty)
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading Item Menu: " & ex.Message)
        End Try
    End Sub


#End Region
#Region "MenuClickEvent"
    ' ================= Event Handlers =================
    Private Sub MainMenu_Click(sender As Object, e As EventArgs)
        Try
            Dim btn As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
            Dim mainId As Integer = Convert.ToInt32(btn.Tag)

            If RadioGroupSettings.SelectedIndex = 0 Then
                ' Load properties for selected main menu button
                LoadButtonProperties(btn, "Main")

                ' Load properties from PHP if available
                'LoadButtonPropertiesFromPHP(mainId, "Main")
            End If

            LoadSubMenu(mainId)
        Catch ex As Exception
            MessageBox.Show("Error in MainMenu_Click: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SubMenu_Click(sender As Object, e As EventArgs)
        Try
            Dim btn As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
            Dim cateId As Integer = Convert.ToInt32(btn.Tag)

            If RadioGroupSettings.SelectedIndex = 1 Then
                ' Load properties for selected sub menu button
                LoadButtonProperties(btn, "Sub")

                ' Load properties from PHP if available
                ' LoadButtonPropertiesFromPHP(cateId, "Sub")
            End If

            LoadItemMenu(cateId)
        Catch ex As Exception
            MessageBox.Show("Error in SubMenu_Click: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ItemMenu_Click(sender As Object, e As EventArgs)
        Try
            Dim btn As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
            Dim itemId As Integer = Convert.ToInt32(btn.Tag)

            If RadioGroupSettings.SelectedIndex = 2 Then
                ' Load properties for selected item menu button
                LoadButtonProperties(btn, "Item")

                ' Load properties from PHP if available
                'LoadButtonPropertiesFromPHP(itemId, "Item")
            End If
        Catch ex As Exception
            MessageBox.Show("Error in ItemMenu_Click: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ' ================= Properties Management =================

    ' Load button properties from controls
    Private Sub LoadButtonProperties(btn As DevExpress.XtraEditors.SimpleButton, menuType As String)
        Try
            selectedButton = btn

            ' Create or update properties object
            If selectedButtonProperties Is Nothing Then
                selectedButtonProperties = New ButtonProperties()
            End If

            selectedButtonProperties.Id = Convert.ToInt32(btn.Tag)
            selectedButtonProperties.MenuType = menuType
            selectedButtonProperties.ItemName = btn.Text
            selectedButtonProperties.ButtonWidth = btn.Width
            selectedButtonProperties.ButtonHeight = btn.Height
            selectedButtonProperties.FontSize = btn.Appearance.Font.Size
            selectedButtonProperties.FontName = btn.Appearance.Font.Name
            selectedButtonProperties.FontStyle = btn.Appearance.Font.Style.ToString()
            selectedButtonProperties.SetTextColor(btn.Appearance.ForeColor)
            selectedButtonProperties.SetBackColor(btn.Appearance.BackColor)

            ' Update properties UI
            UpdatePropertiesUI()

        Catch ex As Exception
            MessageBox.Show("Error loading properties: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================
    ' === Update UI from Properties Object ===
    ' =======================================
    Private Sub UpdatePropertiesUI()
        Try
            If selectedButtonProperties IsNot Nothing Then
                txtid.Text = selectedButtonProperties.Id.ToString()
                txtItemName.Text = selectedButtonProperties.ItemName
                numWidth.Value = selectedButtonProperties.ButtonWidth
                numHeight.Value = selectedButtonProperties.ButtonHeight
                numFontSize.Value = CDec(selectedButtonProperties.FontSize)
                cmbFontName.Text = selectedButtonProperties.FontName
                cmbFontStyle.Text = selectedButtonProperties.FontStyle

                ' Colors
                Dim txtColor As Color = selectedButtonProperties.GetTextColor()
                btnTextColor.ForeColor = txtColor
                btnTextColor.Text = String.Format("ARGB({0},{1},{2},{3})", txtColor.A, txtColor.R, txtColor.G, txtColor.B)


                Dim backColor As Color = selectedButtonProperties.GetBackColor()
                btnBackColor.Appearance.BackColor = backColor
                btnBackColor.Text = String.Format("ARGB({0},{1},{2},{3})", backColor.A, backColor.R, backColor.G, backColor.B)
                UpdatePreview()
            End If
        Catch ex As Exception
            MessageBox.Show("Error updating UI: " & ex.Message)
        End Try
    End Sub


    ' =======================================
    ' === Apply Properties to DevExpress Button ===
    ' =======================================
    Private Sub ApplyPropertiesToButton()
        Try
            If selectedButton IsNot Nothing AndAlso selectedButtonProperties IsNot Nothing Then
                selectedButton.LookAndFeel.UseDefaultLookAndFeel = False
                selectedButton.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat

                selectedButton.Text = selectedButtonProperties.ItemName
                selectedButton.Size = New Size(selectedButtonProperties.ButtonWidth, selectedButtonProperties.ButtonHeight)

                ' Font style
                Dim style As FontStyle = FontStyle.Regular
                Select Case selectedButtonProperties.FontStyle.ToLower()
                    Case "bold" : style = FontStyle.Bold
                    Case "italic" : style = FontStyle.Italic
                    Case "underline" : style = FontStyle.Underline
                End Select

                ' Apply ARGB colors
                selectedButton.Appearance.Font = New Font(selectedButtonProperties.FontName, selectedButtonProperties.FontSize, style)
                selectedButton.Appearance.ForeColor = selectedButtonProperties.GetTextColor()
                selectedButton.Appearance.BackColor = selectedButtonProperties.GetBackColor()

                selectedButton.Refresh()
            End If
        Catch ex As Exception
            MessageBox.Show("Error applying properties: " & ex.Message)
        End Try
    End Sub




    ' Update preview button and apply to selected button (DevExpress SimpleButton workaround)
    Private Sub UpdatePreview()
        Try
            If btnPreview IsNot Nothing Then
                ' --- Text, Size, Style ---
                btnPreview.Text = txtItemName.Text
                btnPreview.Size = New Size(CInt(numWidth.Value), CInt(numHeight.Value))
                btnPreview.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat

                ' --- Font Style ---
                Dim fontStyle As FontStyle = fontStyle.Regular
                Select Case cmbFontStyle.Text.ToLower()
                    Case "bold"
                        fontStyle = fontStyle.Bold
                    Case "italic"
                        fontStyle = fontStyle.Italic
                    Case "bold, italic"
                        fontStyle = fontStyle.Bold Or fontStyle.Italic
                End Select

                ' --- Colors from selector ---
                Dim textColor As Color = selectedButtonProperties.GetTextColor()
                Dim backColor As Color = selectedButtonProperties.GetBackColor()


                ' --- Apply appearance (DevExpress way) ---
                btnPreview.LookAndFeel.UseDefaultLookAndFeel = False
                btnPreview.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat

                btnPreview.Appearance.Options.UseBackColor = True
                btnPreview.Appearance.Options.UseForeColor = True
                btnPreview.Appearance.Options.UseFont = True

                btnPreview.Appearance.BackColor = backColor
                btnPreview.Appearance.ForeColor = textColor
                btnPreview.Appearance.Font = New Font(cmbFontName.Text, CSng(numFontSize.Value), fontStyle)

                btnPreview.Refresh()
                btnPreview.Invalidate()


                ' --- Apply to selected button (if any) ---
                If selectedButton IsNot Nothing Then
                    selectedButton.Text = txtItemName.Text
                    selectedButton.Size = New Size(CInt(numWidth.Value), CInt(numHeight.Value))

                    selectedButton.LookAndFeel.UseDefaultLookAndFeel = False
                    selectedButton.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat

                    selectedButton.Appearance.Options.UseBackColor = True
                    selectedButton.Appearance.Options.UseForeColor = True
                    selectedButton.Appearance.Options.UseFont = True

                    selectedButton.Appearance.BackColor = backColor
                    selectedButton.Appearance.ForeColor = textColor
                    selectedButton.Appearance.Font = New Font(cmbFontName.Text, CSng(numFontSize.Value), fontStyle)

                    selectedButton.Refresh()
                    selectedButton.Invalidate()

                    ' --- Update property object if available ---
                    If selectedButtonProperties IsNot Nothing Then
                        selectedButtonProperties.SetTextColor(textColor)
                        selectedButtonProperties.SetBackColor(backColor)
                    End If
                End If
            End If
        Catch ex As Exception
            ' Ignore preview errors
        End Try
    End Sub
  ' =======================================
    ' === Color Picker Events ===
    ' =======================================
    Private Sub colorEditTextColor_EditValueChanged(sender As Object, e As EventArgs) Handles colorEditTextColor.EditValueChanged
        Try
            Dim c As Color = colorEditTextColor.Color
            btnTextColor.ForeColor = c
            btnTextColor.Text = String.Format("ARGB({0},{1},{2},{3})", c.A, c.R, c.G, c.B)
            If selectedButtonProperties IsNot Nothing Then selectedButtonProperties.SetTextColor(c)
            UpdatePreview()
        Catch ex As Exception
            MessageBox.Show("Error setting text color: " & ex.Message)
        End Try
    End Sub

    Private Sub colorEditBackColor_EditValueChanged(sender As Object, e As EventArgs) Handles colorEditBackColor.EditValueChanged
        Try
            Dim c As Color = colorEditBackColor.Color
            btnBackColor.Appearance.BackColor = c
            btnBackColor.Text = String.Format("ARGB({0},{1},{2},{3})", c.A, c.R, c.G, c.B)
            If selectedButtonProperties IsNot Nothing Then selectedButtonProperties.SetBackColor(c)
            UpdatePreview()
        Catch ex As Exception
            MessageBox.Show("Error setting back color: " & ex.Message)
        End Try
    End Sub
    Private Sub Control_Changed(sender As Object, e As EventArgs) _
    Handles numWidth.ValueChanged,
            numHeight.ValueChanged,
            numFontSize.ValueChanged,
            cmbFontName.SelectedIndexChanged,
            cmbFontStyle.SelectedIndexChanged

        UpdatePreview()
    End Sub

#End Region
   
#Region "ApplySave"
    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Try
            If selectedButtonProperties IsNot Nothing Then
                ' Update properties from controls
                UpdatePropertiesFromControls()

                ' Apply to the selected button
                ApplyPropertiesToButton()

                MessageBox.Show("Properties applied successfully!", "Applied", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error applying properties: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If selectedButtonProperties IsNot Nothing Then
                ' Update properties from controls
                UpdatePropertiesFromControls()

                ' Apply to the selected button
                ApplyPropertiesToButton()

                ' Save to PHP
                SaveButtonPropertiesToPHP()

                MessageBox.Show("Properties saved successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error saving properties: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================
    ' === Update Properties from Controls ===
    ' =======================================
    Private Sub UpdatePropertiesFromControls()
        Try
            If selectedButtonProperties IsNot Nothing Then
                selectedButtonProperties.ItemName = txtItemName.Text
                selectedButtonProperties.ButtonWidth = CInt(numWidth.Value)
                selectedButtonProperties.ButtonHeight = CInt(numHeight.Value)
                selectedButtonProperties.FontSize = CSng(numFontSize.Value)
                selectedButtonProperties.FontName = cmbFontName.Text
                selectedButtonProperties.FontStyle = cmbFontStyle.Text

                ' Save ARGB colors
                selectedButtonProperties.SetTextColor(btnTextColor.ForeColor)
                selectedButtonProperties.SetBackColor(btnBackColor.Appearance.BackColor)
            End If
        Catch ex As Exception
            MessageBox.Show("Error saving UI to properties: " & ex.Message)
        End Try
    End Sub

#End Region
#Region "ResetSetting"
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            ResetButtonToDefault(selectedButton)
        Catch ex As Exception

        End Try
    End Sub
    ' Helper to determine menu type from button location
    Private Function GetMenuTypeFromButton(btn As DevExpress.XtraEditors.SimpleButton) As String
        Try
            If PanelMainMenu.Controls.Contains(btn) Then
                Return "Main"
            ElseIf PanelSubMenu.Controls.Contains(btn) Then
                Return "Sub"
            ElseIf PanelItemMenu.Controls.Contains(btn) Then
                Return "Item"
            Else
                Return "Unknown"
            End If
        Catch ex As Exception
            Return "Unknown"
        End Try
    End Function

    ' Reset button to default appearance + properties
    Private Sub ResetButtonToDefault(btn As DevExpress.XtraEditors.SimpleButton)
        Try
            Dim menuType As String = GetMenuTypeFromButton(btn)

            ' Enable DevExpress appearance options
            btn.Appearance.Options.UseBackColor = True
            btn.Appearance.Options.UseForeColor = True
            btn.Appearance.Options.UseFont = True
            btn.LookAndFeel.UseDefaultLookAndFeel = False
            btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat

            ' Apply default styles based on menu type
            Select Case menuType
                Case "Main"
                    btn.Appearance.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                    btn.Appearance.ForeColor = Color.White
                    btn.Appearance.BackColor = Color.DarkSlateBlue
                    btn.Size = New Size(120, 60)

                    ' reset property object also
                    selectedButtonProperties.FontName = "Segoe UI"
                    selectedButtonProperties.FontSize = 10
                    selectedButtonProperties.FontStyle = "Bold"
                    selectedButtonProperties.TextColorR = Color.White.R
                    selectedButtonProperties.TextColorG = Color.White.G
                    selectedButtonProperties.TextColorB = Color.White.B
                    selectedButtonProperties.BackColorR = Color.DarkSlateBlue.R
                    selectedButtonProperties.BackColorG = Color.DarkSlateBlue.G
                    selectedButtonProperties.BackColorB = Color.DarkSlateBlue.B
                    selectedButtonProperties.ButtonWidth = 120
                    selectedButtonProperties.ButtonHeight = 60

                Case "Sub"
                    btn.Appearance.Font = New Font("Segoe UI", 9, FontStyle.Regular)
                    btn.Appearance.ForeColor = Color.Black
                    btn.Appearance.BackColor = Color.LightSteelBlue
                    btn.Size = New Size(110, 50)

                    selectedButtonProperties.FontName = "Segoe UI"
                    selectedButtonProperties.FontSize = 9
                    selectedButtonProperties.FontStyle = "Regular"
                    selectedButtonProperties.TextColorR = Color.Black.R
                    selectedButtonProperties.TextColorG = Color.Black.G
                    selectedButtonProperties.TextColorB = Color.Black.B
                    selectedButtonProperties.BackColorR = Color.LightSteelBlue.R
                    selectedButtonProperties.BackColorG = Color.LightSteelBlue.G
                    selectedButtonProperties.BackColorB = Color.LightSteelBlue.B
                    selectedButtonProperties.ButtonWidth = 110
                    selectedButtonProperties.ButtonHeight = 50

                Case "Item"
                    btn.Appearance.Font = New Font("Segoe UI", 8, FontStyle.Regular)
                    btn.Appearance.ForeColor = Color.DarkBlue
                    btn.Appearance.BackColor = Color.Beige
                    btn.Size = New Size(100, 45)

                    selectedButtonProperties.FontName = "Segoe UI"
                    selectedButtonProperties.FontSize = 8
                    selectedButtonProperties.FontStyle = "Regular"
                    selectedButtonProperties.TextColorR = Color.DarkBlue.R
                    selectedButtonProperties.TextColorG = Color.DarkBlue.G
                    selectedButtonProperties.TextColorB = Color.DarkBlue.B
                    selectedButtonProperties.BackColorR = Color.Beige.R
                    selectedButtonProperties.BackColorG = Color.Beige.G
                    selectedButtonProperties.BackColorB = Color.Beige.B
                    selectedButtonProperties.ButtonWidth = 100
                    selectedButtonProperties.ButtonHeight = 45
            End Select

            ' update UI with new properties
            UpdatePropertiesUI()

        Catch ex As Exception
            MessageBox.Show("Error resetting button: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


#End Region
#Region "PHP"

    ' Apply properties from ButtonProperties object to any button
    Private Sub ApplyPropertiesFromObjectToButton(btn As DevExpress.XtraEditors.SimpleButton, props As ButtonProperties)
        Try
            If btn IsNot Nothing AndAlso props IsNot Nothing Then
                btn.Text = props.ItemName
                btn.Size = New Size(props.ButtonWidth, props.ButtonHeight)

                Dim fontStyle As FontStyle = fontStyle.Regular
                Select Case props.FontStyle.ToLower()
                    Case "bold"
                        fontStyle = fontStyle.Bold
                    Case "italic"
                        fontStyle = fontStyle.Italic
                    Case "underline"
                        fontStyle = fontStyle.Underline
                    Case "bold, italic"
                        fontStyle = fontStyle.Bold Or fontStyle.Italic
                End Select

                ' Enable DevExpress appearance options
                btn.Appearance.Options.UseBackColor = True
                btn.Appearance.Options.UseForeColor = True
                btn.Appearance.Options.UseFont = True

                ' Apply appearance properties
                btn.Appearance.Font = New Font(props.FontName, props.FontSize, fontStyle)
                
            End If
        Catch ex As Exception
            ' Ignore errors and use default appearance
        End Try
    End Sub


    ' ================= PHP Integration =================

    ' Save button properties to PHP
    Private Sub SaveButtonPropertiesToPHP()
        Try
            If selectedButtonProperties Is Nothing Then
                MessageBox.Show("No button properties to save.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Prepare PHP API URL
            Dim phpUrl As String = "http://yourserver.com/api/save_button_properties.php"

            ' Create POST data
            Dim postData As String = CreatePostData()

            ' Send data to PHP
            Dim response As String = SendToPHP(phpUrl, postData)

            ' Handle response
            HandlePHPResponse(response)

        Catch ex As Exception
            MessageBox.Show("Error saving to PHP: " & ex.Message, "PHP Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Create POST data for PHP
    Private Function CreatePostData() As String
        Try
            Dim postData As String = ""
            postData &= "action=save_button_properties"
            postData &= "&id=" & Uri.EscapeDataString(selectedButtonProperties.Id.ToString())
            postData &= "&menu_type=" & Uri.EscapeDataString(selectedButtonProperties.MenuType)
            postData &= "&item_name=" & Uri.EscapeDataString(selectedButtonProperties.ItemName)
            postData &= "&button_width=" & Uri.EscapeDataString(selectedButtonProperties.ButtonWidth.ToString())
            postData &= "&button_height=" & Uri.EscapeDataString(selectedButtonProperties.ButtonHeight.ToString())
            postData &= "&font_size=" & Uri.EscapeDataString(selectedButtonProperties.FontSize.ToString())
            postData &= "&font_name=" & Uri.EscapeDataString(selectedButtonProperties.FontName)
            postData &= "&font_style=" & Uri.EscapeDataString(selectedButtonProperties.FontStyle)
            postData &= "&text_color_r=" & Uri.EscapeDataString(selectedButtonProperties.TextColorR.ToString())
            postData &= "&text_color_g=" & Uri.EscapeDataString(selectedButtonProperties.TextColorG.ToString())
            postData &= "&text_color_b=" & Uri.EscapeDataString(selectedButtonProperties.TextColorB.ToString())
            postData &= "&back_color_r=" & Uri.EscapeDataString(selectedButtonProperties.BackColorR.ToString())
            postData &= "&back_color_g=" & Uri.EscapeDataString(selectedButtonProperties.BackColorG.ToString())
            postData &= "&back_color_b=" & Uri.EscapeDataString(selectedButtonProperties.BackColorB.ToString())
            postData &= "&position=" & Uri.EscapeDataString(selectedButtonProperties.Position.ToString())

            Return postData
        Catch ex As Exception
            Throw New Exception("Error creating POST data: " & ex.Message)
        End Try
    End Function

    ' Send data to PHP server
    Private Function SendToPHP(url As String, postData As String) As String
        Try
            Using client As New System.Net.WebClient()
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")

                ' Convert string to byte array
                Dim dataBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)

                ' Send POST request
                Dim responseBytes As Byte() = client.UploadData(url, "POST", dataBytes)

                ' Convert response to string
                Dim response As String = System.Text.Encoding.UTF8.GetString(responseBytes)

                Return response
            End Using
        Catch ex As Exception
            Throw New Exception("Error sending to PHP: " & ex.Message)
        End Try
    End Function

    ' Handle PHP response
    Private Sub HandlePHPResponse(response As String)
        Try
            ' Parse JSON response (assuming PHP returns JSON)
            If Not String.IsNullOrEmpty(response) Then
                ' Simple response parsing - you can use Newtonsoft.Json for complex parsing
                If response.Contains("success") AndAlso response.Contains("true") Then
                    MessageBox.Show("Button properties saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ElseIf response.Contains("error") Then
                    MessageBox.Show("Server error: " & response, "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    MessageBox.Show("Unknown response from server: " & response, "Unknown Response", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                MessageBox.Show("Empty response from server.", "Empty Response", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error handling PHP response: " & ex.Message, "Response Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Load button properties from PHP
    Private Sub LoadButtonPropertiesFromPHP(id As Integer, menuType As String)
        Try
            Dim phpUrl As String = "http://yourserver.com/api/get_button_properties.php"
            Dim postData As String = "action=get_button_properties&id=" & id & "&menu_type=" & Uri.EscapeDataString(menuType)

            Dim response As String = SendToPHP(phpUrl, postData)

            ' Parse response and update properties
            ' This is a simplified example - you should use proper JSON parsing
            If Not String.IsNullOrEmpty(response) AndAlso Not response.Contains("error") Then
                ' Parse the response and update selectedButtonProperties
                ' Implementation depends on your PHP response format
                ParsePHPResponseToProperties(response)
            End If

        Catch ex As Exception
            ' Ignore errors when loading - use defaults
            System.Diagnostics.Debug.WriteLine("Error loading from PHP: " & ex.Message)
        End Try
    End Sub

    ' Parse PHP response to properties object
    Private Sub ParsePHPResponseToProperties(response As String)
        Try
            ' This is a simplified parser - replace with proper JSON parsing
            ' Example response format: {"button_width":"120","button_height":"60",...}

            If selectedButtonProperties Is Nothing Then
                selectedButtonProperties = New ButtonProperties()
            End If

            ' Simple parsing example - use Newtonsoft.Json for production
            If response.Contains("button_width") Then
                Dim widthMatch As String = ExtractValueFromResponse(response, "button_width")
                If Not String.IsNullOrEmpty(widthMatch) Then
                    selectedButtonProperties.ButtonWidth = Convert.ToInt32(widthMatch)
                End If
            End If

            If response.Contains("button_height") Then
                Dim heightMatch As String = ExtractValueFromResponse(response, "button_height")
                If Not String.IsNullOrEmpty(heightMatch) Then
                    selectedButtonProperties.ButtonHeight = Convert.ToInt32(heightMatch)
                End If
            End If

            ' Add more parsing as needed...

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error parsing PHP response: " & ex.Message)
        End Try
    End Sub

    ' Helper method to extract values from response
    Private Function ExtractValueFromResponse(response As String, key As String) As String
        Try
            ' Simple extraction - replace with proper JSON parsing
            Dim searchPattern As String = """" & key & """:"""
            Dim startIndex As Integer = response.IndexOf(searchPattern)
            If startIndex >= 0 Then
                startIndex += searchPattern.Length
                Dim endIndex As Integer = response.IndexOf("""", startIndex)
                If endIndex > startIndex Then
                    Return response.Substring(startIndex, endIndex - startIndex)
                End If
            End If
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function


#End Region

  
End Class
