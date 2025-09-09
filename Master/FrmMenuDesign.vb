Public Class FrmMenuDesign
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

            LoadButtonStyles()
            LoadMainMenu()
        Catch ex As Exception

        End Try
    End Sub


    ' ============ Load Main Menu ============
    Private Sub LoadMainMenu()
        Try
            PanelMainMenu.Controls.Clear()

            Dim btnWidth As Integer = ButtonStyleWH.MAINW
            Dim btnHeight As Integer = ButtonStyleWH.MAINH
            Dim cols As Integer = ButtonStyleWH.MAINCOL
            Dim spacing As Integer = ButtonStyleWH.ITEMSPACING
            Dim marginLeft As Integer = ButtonStyleWH.ITEMMARGINLEFT
            Dim marginTop As Integer = ButtonStyleWH.ITEMMARGINRIGHT

            For i As Integer = 0 To _JsonData.MainGroupTable.Rows.Count - 1
                Dim row As Integer = i \ cols
                Dim col As Integer = i Mod cols

                ' Get current row data
                Dim currentRow As DataRow = _JsonData.MainGroupTable.Rows(i)

                ' Count how many buttons in this row
                Dim countInRow As Integer =
                    If(i + cols < _JsonData.MainGroupTable.Rows.Count, cols, _JsonData.MainGroupTable.Rows.Count - row * cols)

                ' Total width of this row
                Dim rowWidth As Integer = (countInRow * btnWidth) + ((countInRow - 1) * spacing)

                ' Center horizontally
                Dim startX As Integer = Math.Max(0, (PanelMainMenu.Width - rowWidth) \ 2)

                ' Create button
                Dim btn As New DevExpress.XtraEditors.SimpleButton()
                btn.Text = currentRow("MainName").ToString()
                btn.Tag = currentRow("MainId").ToString()
                btn.Size = New Size(btnWidth, btnHeight)
                btn.Location = New Point(marginLeft + col * (btnWidth + spacing),
                             marginTop + row * (btnHeight + spacing))

                ' Get properties from API data with defaults
                Dim fontSize As Single = GetSafeValue(currentRow, "font_size", 14.0F)
                Dim fontName As String = GetSafeValue(currentRow, "font_name", "Segoe UI")
                Dim fontStyleString As String = GetSafeValue(currentRow, "font_style", "Bold")
                Dim textColor As Color = ParseARGBColor(GetSafeValue(currentRow, "text_color", ""), Color.White)
                Dim backColor As Color = ParseARGBColor(GetSafeValue(currentRow, "back_color", ""), Color.FromArgb(52, 152, 219))

                ' Convert font style string to FontStyle enum
                Dim fontStyleEnum As FontStyle = FontStyle.Regular
                Dim fontStyleLower As String = fontStyleString.ToLower()
                If fontStyleLower = "bold" Then
                    fontStyleEnum = FontStyle.Bold
                ElseIf fontStyleLower = "italic" Then
                    fontStyleEnum = FontStyle.Italic
                ElseIf fontStyleLower = "underline" Then
                    fontStyleEnum = FontStyle.Underline
                ElseIf fontStyleLower = "strikeout" Then
                    fontStyleEnum = FontStyle.Strikeout
                Else
                    fontStyleEnum = FontStyle.Regular
                End If

                ' Apply properties to button
                btn.Appearance.Font = New Font(fontName, fontSize, fontStyleEnum)
                btn.Appearance.ForeColor = textColor
                btn.Appearance.BackColor = backColor
                btn.Appearance.Options.UseBackColor = True
                btn.Appearance.Options.UseForeColor = True
                btn.Appearance.Options.UseFont = True
                btn.Appearance.Options.UseTextOptions = True
                btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                btn.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                btn.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter

                btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
                btn.LookAndFeel.UseDefaultLookAndFeel = False

                ' Add border for better visual separation
                btn.Appearance.BorderColor = Color.FromArgb(200, 200, 200)
                btn.Appearance.Options.UseBorderColor = True
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
            If _JsonData.MainGroupTable.Rows.Count > 0 Then
                LoadSubMenu(Convert.ToInt32(_JsonData.MainGroupTable.Rows(0)("MainId")))
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading Main Menu: " & ex.Message)
        End Try
    End Sub


    ' ============ Load Sub Menu ============
    Private Sub LoadSubMenu(mainId As Integer)
        Try
            PanelSubMenu.Controls.Clear()

            Dim btnWidth As Integer = ButtonStyleWH.SUBW
            Dim btnHeight As Integer = ButtonStyleWH.SUBH
            Dim cols As Integer = ButtonStyleWH.SUBMENUCOL
            Dim spacing As Integer = ButtonStyleWH.ITEMSPACING
            Dim marginLeft As Integer = ButtonStyleWH.ITEMMARGINLEFT
            Dim marginTop As Integer = ButtonStyleWH.ITEMMARGINRIGHT
            ' Group by CateId, CateName under selected MainId
            Dim subs = _JsonData.CategoryTable.AsEnumerable().
                Where(Function(r) Convert.ToInt32(r("MainId")) = mainId).
                GroupBy(Function(r) New With {
                    Key .CateId = Convert.ToInt32(r("CateId")),
                    Key .CateName = r("CateName").ToString(),
                    Key .Position = Convert.ToInt32(r("Position"))
                }).
                Select(Function(g) g.Key).
                OrderBy(Function(x) x.Position).ToList()

            For i As Integer = 0 To subs.Count - 1
                Dim row As Integer = i \ cols
                Dim col As Integer = i Mod cols

                Dim btn As New DevExpress.XtraEditors.SimpleButton()
                btn.Text = subs(i).CateName
                btn.Tag = subs(i).CateId
                btn.Size = New Size(btnWidth, btnHeight)
                btn.Location = New Point(marginLeft + col * (btnWidth + spacing),
                              marginTop + row * (btnHeight + spacing))

                ' Try to find matching row in CategoryTable for this CateId
                Dim matchingRow As DataRow = Nothing
                Try
                    Dim currentCateId As Integer = subs(i).CateId
                    matchingRow = _JsonData.CategoryTable.AsEnumerable().
                        Where(Function(r) Convert.ToInt32(r("CateId")) = currentCateId).
                        FirstOrDefault()
                Catch
                    ' Continue with defaults if no matching row found
                End Try

                ' Get properties from API data with defaults for Sub menu
                Dim fontSize As Single = 9.0F
                Dim fontName As String = "Segoe UI"
                Dim fontStyleString As String = "Regular"
                Dim textColor As Color = Color.Black
                Dim backColor As Color = Color.LightSteelBlue

                ' If we found a matching row, try to get custom properties
                If matchingRow IsNot Nothing Then
                    fontSize = GetSafeValue(matchingRow, "font_size", 9.0F)
                    fontName = GetSafeValue(matchingRow, "font_name", "Segoe UI")
                    fontStyleString = GetSafeValue(matchingRow, "font_style", "Regular")
                    textColor = ParseArgbColor(GetSafeValue(matchingRow, "text_color", ""), Color.Black)
                    backColor = ParseArgbColor(GetSafeValue(matchingRow, "back_color", ""), Color.LightSteelBlue)
                End If

                ' Convert font style string to FontStyle enum
                Dim fontStyleEnum As FontStyle = FontStyle.Regular
                Dim fontStyleLower As String = fontStyleString.ToLower()
                If fontStyleLower = "bold" Then
                    fontStyleEnum = FontStyle.Bold
                ElseIf fontStyleLower = "italic" Then
                    fontStyleEnum = FontStyle.Italic
                ElseIf fontStyleLower = "underline" Then
                    fontStyleEnum = FontStyle.Underline
                ElseIf fontStyleLower = "strikeout" Then
                    fontStyleEnum = FontStyle.Strikeout
                Else
                    fontStyleEnum = FontStyle.Regular
                End If

                ' Enable DevExpress appearance options
                btn.Appearance.Options.UseBackColor = True
                btn.Appearance.Options.UseForeColor = True
                btn.Appearance.Options.UseFont = True
                btn.Appearance.Options.UseTextOptions = True
                btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                btn.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                btn.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter

                ' Apply properties to button
                btn.Appearance.Font = New Font(fontName, fontSize, fontStyleEnum)
                btn.Appearance.ForeColor = textColor
                btn.Appearance.BackColor = backColor

                btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
                btn.LookAndFeel.UseDefaultLookAndFeel = False

                ' Add border for better visual separation
                btn.Appearance.BorderColor = Color.FromArgb(200, 200, 200)
                btn.Appearance.Options.UseBorderColor = True

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

            Dim btnWidth As Integer = ButtonStyleWH.ITEMW
            Dim btnHeight As Integer = ButtonStyleWH.ITEMH
            Dim cols As Integer = ButtonStyleWH.ITEMMENUCOL
            Dim spacing As Integer = ButtonStyleWH.ITEMSPACING
            Dim marginLeft As Integer = ButtonStyleWH.ITEMMARGINLEFT
            Dim marginTop As Integer = ButtonStyleWH.ITEMMARGINRIGHT
            ' Items filtered by CateId
            Dim items = _JsonData.ItemTouchMasterTable.AsEnumerable().
                Where(Function(r) Convert.ToInt32(r("CateId")) = cateId).
                Select(Function(r) New With {
                    .Id = Convert.ToInt32(r("Id")),
                    .ItemName = r("ItemName").ToString(),
                    .Position = Convert.ToInt32(r("Position"))
                }).OrderBy(Function(x) x.Position).ToList() ' Ascending order by Position

            For i As Integer = 0 To items.Count - 1
                Dim row As Integer = i \ cols
                Dim col As Integer = i Mod cols

                Dim btn As New DevExpress.XtraEditors.SimpleButton()
                btn.Text = items(i).ItemName
                btn.Tag = items(i).Id
                btn.Size = New Size(btnWidth, btnHeight)
                btn.Location = New Point(marginLeft + col * (btnWidth + spacing),
                              marginTop + row * (btnHeight + spacing))

                ' Try to find matching row in ItemTouchMasterTable for this Id
                Dim matchingRow As DataRow = Nothing
                Try
                    Dim currentItemId As Integer = items(i).Id
                    matchingRow = _JsonData.ItemTouchMasterTable.AsEnumerable().
                        Where(Function(r) Convert.ToInt32(r("item_id")) = currentItemId).
                        FirstOrDefault()
                Catch
                    ' Continue with defaults if no matching row found
                End Try

                ' Get properties from API data with defaults for Item menu
                Dim fontSize As Single = 8.0F
                Dim fontName As String = "Segoe UI"
                Dim fontStyleString As String = "Regular"
                Dim textColor As Color = Color.DarkBlue
                Dim backColor As Color = Color.Beige

                ' If we found a matching row, try to get custom properties
                If matchingRow IsNot Nothing Then
                    fontSize = GetSafeValue(matchingRow, "font_size", 8.0F)
                    fontName = GetSafeValue(matchingRow, "font_name", "Segoe UI")
                    fontStyleString = GetSafeValue(matchingRow, "font_style", "Regular")
                    textColor = ParseARGBColor(GetSafeValue(matchingRow, "text_color", ""), Color.DarkBlue)
                    backColor = ParseARGBColor(GetSafeValue(matchingRow, "back_color", ""), Color.Beige)
                End If

                ' Convert font style string to FontStyle enum
                Dim fontStyleEnum As FontStyle = FontStyle.Regular
                Dim fontStyleLower As String = fontStyleString.ToLower()
                If fontStyleLower = "bold" Then
                    fontStyleEnum = FontStyle.Bold
                ElseIf fontStyleLower = "italic" Then
                    fontStyleEnum = FontStyle.Italic
                ElseIf fontStyleLower = "underline" Then
                    fontStyleEnum = FontStyle.Underline
                ElseIf fontStyleLower = "strikeout" Then
                    fontStyleEnum = FontStyle.Strikeout
                Else
                    fontStyleEnum = FontStyle.Regular
                End If

                ' Enable DevExpress appearance options
                btn.Appearance.Options.UseBackColor = True
                btn.Appearance.Options.UseForeColor = True
                btn.Appearance.Options.UseFont = True
                btn.Appearance.Options.UseTextOptions = True
                btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                btn.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                btn.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter

                ' Apply properties to button
                btn.Appearance.Font = New Font(fontName, fontSize, fontStyleEnum)
                btn.Appearance.ForeColor = textColor
                btn.Appearance.BackColor = backColor

                btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
                btn.LookAndFeel.UseDefaultLookAndFeel = False

                ' Add border for better visual separation
                btn.Appearance.BorderColor = Color.FromArgb(200, 200, 200)
                btn.Appearance.Options.UseBorderColor = True

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
                LoadButtonPropertiesFromPHP(mainId, "Main")
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


                '' --- Apply to selected button (if any) ---
                'If selectedButton IsNot Nothing Then
                '    selectedButton.Text = txtItemName.Text
                '    selectedButton.Size = New Size(CInt(numWidth.Value), CInt(numHeight.Value))

                '    selectedButton.LookAndFeel.UseDefaultLookAndFeel = False
                '    selectedButton.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat

                '    selectedButton.Appearance.Options.UseBackColor = True
                '    selectedButton.Appearance.Options.UseForeColor = True
                '    selectedButton.Appearance.Options.UseFont = True

                '    selectedButton.Appearance.BackColor = backColor
                '    selectedButton.Appearance.ForeColor = textColor
                '    selectedButton.Appearance.Font = New Font(cmbFontName.Text, CSng(numFontSize.Value), fontStyle)

                '    selectedButton.Refresh()
                '    selectedButton.Invalidate()

                '    ' --- Update property object if available ---
                '    If selectedButtonProperties IsNot Nothing Then
                '        selectedButtonProperties.SetTextColor(textColor)
                '        selectedButtonProperties.SetBackColor(backColor)
                '    End If
                'End If
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
#Region "RefreshTable"
    Private Sub RefreshTable()
        Try
            getMainMaster()
            getCategoryMaster()
            getTouchItemMaster()
            getButtonStyleTable()
            LoadButtonStyles()
            LoadMainMenu()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnrefreshtable_Click(sender As Object, e As EventArgs) Handles btnrefreshtable.Click
        Try
            RefreshTable()
        Catch ex As Exception

        End Try
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

                'MessageBox.Show("Properties saved successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

            ' Prepare PHP API URL with parameters for GET request
            Dim postData As String = CreatePostData()
            Dim phpUrl As String = M_Details.LinkAjaxRequest & "MenuRequest=11&" & postData

            ' Send GET request to PHP
            Dim response As String = SendGetToPHP(phpUrl)

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
            postData &= "operation=SAVE"
            postData &= "&item_id=" & Uri.EscapeDataString(selectedButtonProperties.Id.ToString())
            postData &= "&menu_type=" & Uri.EscapeDataString(selectedButtonProperties.MenuType)
            postData &= "&item_name=" & Uri.EscapeDataString(selectedButtonProperties.ItemName)
            postData &= "&button_width=" & Uri.EscapeDataString(selectedButtonProperties.ButtonWidth.ToString())
            postData &= "&button_height=" & Uri.EscapeDataString(selectedButtonProperties.ButtonHeight.ToString())
            postData &= "&font_size=" & Uri.EscapeDataString(selectedButtonProperties.FontSize.ToString())
            postData &= "&font_name=" & Uri.EscapeDataString(selectedButtonProperties.FontName)
            postData &= "&font_style=" & Uri.EscapeDataString(selectedButtonProperties.FontStyle)

            ' Create ARGB format for colors
            Dim textColor As Color = selectedButtonProperties.GetTextColor()
            Dim backColor As Color = selectedButtonProperties.GetBackColor()

            postData &= "&text_color=" & Uri.EscapeDataString(String.Format("Argb({0},{1},{2},{3})", textColor.A, textColor.R, textColor.G, textColor.B))
            postData &= "&back_color=" & Uri.EscapeDataString(String.Format("Argb({0},{1},{2},{3})", backColor.A, backColor.R, backColor.G, backColor.B))
            postData &= "&position=" & Uri.EscapeDataString(txtposition.Text)

            Return postData
        Catch ex As Exception
            Throw New Exception("Error creating POST data: " & ex.Message)
        End Try
    End Function

    ' Send GET request to PHP server
    Private Function SendGetToPHP(url As String) As String
        Try
            Using client As New System.Net.WebClient()
                ' Send GET request
                Dim response As String = client.DownloadString(url)
                Return response
            End Using
        Catch ex As Exception
            Throw New Exception("Error sending GET to PHP: " & ex.Message)
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
                If response.Contains("Success") AndAlso response.Contains("true") Then
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

            ' Parse basic properties
            If response.Contains("item_name") Then
                Dim nameMatch As String = ExtractValueFromResponse(response, "item_name")
                If Not String.IsNullOrEmpty(nameMatch) Then
                    selectedButtonProperties.ItemName = nameMatch
                End If
            End If

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

            If response.Contains("font_size") Then
                Dim fontSizeMatch As String = ExtractValueFromResponse(response, "font_size")
                If Not String.IsNullOrEmpty(fontSizeMatch) Then
                    selectedButtonProperties.FontSize = Convert.ToSingle(fontSizeMatch)
                End If
            End If

            If response.Contains("font_name") Then
                Dim fontNameMatch As String = ExtractValueFromResponse(response, "font_name")
                If Not String.IsNullOrEmpty(fontNameMatch) Then
                    selectedButtonProperties.FontName = fontNameMatch
                End If
            End If

            If response.Contains("font_style") Then
                Dim fontStyleMatch As String = ExtractValueFromResponse(response, "font_style")
                If Not String.IsNullOrEmpty(fontStyleMatch) Then
                    selectedButtonProperties.FontStyle = fontStyleMatch
                End If
            End If

            If response.Contains("position") Then
                Dim positionMatch As String = ExtractValueFromResponse(response, "position")
                If Not String.IsNullOrEmpty(positionMatch) Then
                    selectedButtonProperties.Position = Convert.ToInt32(positionMatch)
                End If
            End If

            ' Parse ARGB color format: Argb(255,255,255,255)
            If response.Contains("text_color") Then
                Dim textColorMatch As String = ExtractValueFromResponse(response, "text_color")
                If Not String.IsNullOrEmpty(textColorMatch) Then
                    Dim textColor As Color = ParseARGBColor(textColorMatch)
                    selectedButtonProperties.SetTextColor(textColor)
                End If
            End If

            If response.Contains("back_color") Then
                Dim backColorMatch As String = ExtractValueFromResponse(response, "back_color")
                If Not String.IsNullOrEmpty(backColorMatch) Then
                    Dim backColor As Color = ParseARGBColor(backColorMatch)
                    selectedButtonProperties.SetBackColor(backColor)
                End If
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error parsing PHP response: " & ex.Message)
        End Try
    End Sub

    ' Helper method to parse ARGB color format: Argb(255,255,255,255)
    Private Function ParseARGBColor(argbString As String) As Color
        Try
            ' Remove "Argb(" and ")" and split by comma
            Dim cleanString As String = argbString.Replace("Argb(", "").Replace(")", "").Trim()
            Dim values() As String = cleanString.Split(","c)

            If values.Length = 4 Then
                Dim a As Integer = Convert.ToInt32(values(0).Trim())
                Dim r As Integer = Convert.ToInt32(values(1).Trim())
                Dim g As Integer = Convert.ToInt32(values(2).Trim())
                Dim b As Integer = Convert.ToInt32(values(3).Trim())

                Return Color.FromArgb(a, r, g, b)
            End If

            ' Return default color if parsing fails
            Return Color.Black
        Catch ex As Exception
            ' Return default color if parsing fails
            Return Color.Black
        End Try
    End Function

    ' Helper function to safely get values from DataRow with default fallback
    Private Function GetSafeValue(Of T)(row As DataRow, columnName As String, defaultValue As T) As T
        Try
            If row.Table.Columns.Contains(columnName) AndAlso Not IsDBNull(row(columnName)) Then
                Dim value As Object = row(columnName)
                If value IsNot Nothing Then
                    Return DirectCast(Convert.ChangeType(value, GetType(T)), T)
                End If
            End If
            Return defaultValue
        Catch ex As Exception
            Return defaultValue
        End Try
    End Function

    ' Helper function to parse ARGB color with fallback
    Private Function ParseArgbColor(argbString As String, defaultColor As Color) As Color
        If String.IsNullOrEmpty(argbString) Then
            Return defaultColor
        End If

        Dim result As Color = ParseARGBColor(argbString)
        If result = Color.Black AndAlso argbString <> "Argb(255,0,0,0)" Then
            Return defaultColor
        End If
        Return result
    End Function

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
#Region "Save/Height/Width/Cols"
    Private Sub btnsavefordefault_Click(sender As Object, e As EventArgs) Handles btnsavefordefault.Click
        Try
            Dim Width As String = ""
            Dim Height As String = ""
            Dim Columns As String = ""
            Dim GroupNameH As String = ""
            Dim GroupNameW As String = ""
            Dim GroupNameCol As String = ""

            If RadioGroupSettings.SelectedIndex = 0 Then
                GroupNameH = "MAINH"
                Height = numHeight.Value.ToString()
                GroupNameW = "MAINW"
                Width = numWidth.Value.ToString()
                GroupNameCol = "MAINCOL"
                Columns = txtcolumns.EditValue.ToString()
            ElseIf RadioGroupSettings.SelectedIndex = 1 Then
                GroupNameH = "SUBH"
                Height = numHeight.Value.ToString()
                GroupNameW = "SUBW"
                Width = numWidth.Value.ToString()
                GroupNameCol = "SUBMENUCOL"
                Columns = txtcolumns.EditValue.ToString()
            Else
                GroupNameH = "ITEMH"
                Height = numHeight.Value.ToString()
                GroupNameW = "ITEMW"
                Width = numWidth.Value.ToString()
                GroupNameCol = "ITEMMENUCOL"
                Columns = txtcolumns.EditValue.ToString()
            End If

            ' Save Height setting
            SaveButtonDimensionToPHP(GroupNameH, Height)

            ' Save Width setting
            SaveButtonDimensionToPHP(GroupNameW, Width)

            ' Save Columns setting
            SaveButtonDimensionToPHP(GroupNameCol, Columns)

            MessageBox.Show("Default button settings saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error saving default settings: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Helper method to save button dimensions to PHP
    Private Sub SaveButtonDimensionToPHP(groupName As String, groupValue As String)
        Try
            ' Create URL with parameters for GET request
            Dim phpUrl As String = M_Details.LinkAjaxRequest & "MenuRequest=12&" &
                                  "operation=SAVE_DIMENSION&" &
                                  "group_name=" & Uri.EscapeDataString(groupName) &
                                  "&group_value=" & Uri.EscapeDataString(groupValue)

            ' Send GET request to PHP
            Dim response As String = SendGetToPHP(phpUrl)

            ' Show message box with response
            MessageBox.Show("Response for " & groupName & ": " & response, "Dimension Save Response", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error saving dimension " & groupName & ": " & ex.Message)
            MessageBox.Show("Error saving dimension " & groupName & ": " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

End Class
