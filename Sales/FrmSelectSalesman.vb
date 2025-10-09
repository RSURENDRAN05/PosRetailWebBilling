Public Class FrmSelectSalesman 
    Private _selectedSalesmanId As Integer = 0
    Private _selectedSalesmanName As String = ""
    Private _isSelectionMode As Boolean = False
    ' Properties to get selected salesman information
    Public ReadOnly Property SelectedSalesmanId As Integer
        Get
            Return _selectedSalesmanId
        End Get
    End Property

    Public ReadOnly Property SelectedSalesmanName As String
        Get
            Return _selectedSalesmanName
        End Get
    End Property
    ' Constructor
    Public Sub New(Optional selectionMode As Boolean = False)
        InitializeComponent()
        _isSelectionMode = selectionMode
    End Sub
    Private Sub FrmSelectSalesman_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadSalesmanDataFromAPI()
            LoadMainMenu()
        Catch ex As Exception

        End Try
    End Sub
   Private Sub LoadMainMenu()
        Try
            PanelControlSalesMan.Controls.Clear()

            ' Create sorted DataView ordered by SalesMan Asc
            Dim sortedView As DataView = New DataView(_JsonData.SalesManDataTable)
            sortedView.Sort = "SalesMan ASC"

            Dim btnWidth As Integer = ButtonStyleWH.MAINW
            Dim btnHeight As Integer = ButtonStyleWH.MAINH
            Dim spacing As Integer = ButtonStyleWH.ITEMSPACING
            Dim marginLeft As Integer = ButtonStyleWH.ITEMMARGINLEFT
            Dim marginTop As Integer = ButtonStyleWH.ITEMMARGINRIGHT
            Dim cols As Integer = 2

            ' Loop through the sorted DataView instead of the original DataTable
            For i As Integer = 0 To sortedView.Count - 1
                Dim row As Integer = i \ cols
                Dim col As Integer = i Mod cols

                ' Get current row data from sorted view
                Dim currentRow As DataRowView = sortedView(i)
                ' If you need the actual DataRow, use: currentRow.Row

                ' Count how many buttons in this row
                Dim countInRow As Integer =
                    If(i + cols < sortedView.Count, cols, sortedView.Count - row * cols)

                ' Total width of this row
                Dim rowWidth As Integer = (countInRow * btnWidth) + ((countInRow - 1) * spacing)

                ' Center horizontally
                Dim startX As Integer = Math.Max(0, (PanelControlSalesMan.Width - rowWidth) \ 2)

                ' Create button
                Dim btn As New DevExpress.XtraEditors.SimpleButton()
                btn.Text = currentRow("SalesMan").ToString()
                btn.Tag = currentRow("Id").ToString()
                btn.Size = New Size(btnWidth, btnHeight)
                btn.Location = New Point(marginLeft + col * (btnWidth + spacing),
                             marginTop + row * (btnHeight + spacing))

                ' Get properties from API data with defaults
                Dim fontSize As Single = GetSafeValue(currentRow.Row, "font_size", 14.0F)
                Dim fontName As String = GetSafeValue(currentRow.Row, "font_name", "Segoe UI")
                Dim fontStyleString As String = GetSafeValue(currentRow.Row, "font_style", "Bold")
                Dim textColor As Color = ParseArgbColor(GetSafeValue(currentRow.Row, "text_color", ""), Color.White)
                Dim backColor As Color = ParseArgbColor(GetSafeValue(currentRow.Row, "back_color", ""), Color.DarkBlue)

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

                AddHandler btn.Click, AddressOf SalesMan_Click
                PanelControlSalesMan.Controls.Add(btn)
            Next

            PanelControlSalesMan.AutoScroll = True
            PanelControlSalesMan.AllowTouchScroll = True

        Catch ex As Exception
            MessageBox.Show("Error loading Main Menu: " & ex.Message)
        End Try
    End Sub

    Private Sub SalesMan_Click(sender As Object, e As EventArgs)
        Try
            ' Get the clicked button and extract the category ID from its Tag
            Dim clickedButton As DevExpress.XtraEditors.SimpleButton = CType(sender, DevExpress.XtraEditors.SimpleButton)
            Dim selectedMainGroupId As Integer = 0

            ' Safely convert the Tag to Integer
            If IsNumeric(clickedButton.Tag) Then
                _selectedSalesmanId = Convert.ToInt32(clickedButton.Tag)
                _selectedSalesmanName = clickedButton.Text
            End If
            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            ' Handle error silently or log if needed
            DevExpress.XtraEditors.XtraMessageBox.Show("Error loading category: " & ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Function LoadSalesmanDataFromAPI() As Boolean
        Try
            If _JsonData.SalesManDataTable.Rows.Count = 0 Then
                GetSalesmanData()
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Error loading salesman data from API: " & ex.Message, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
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
End Class