Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.LookAndFeel
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net

Public Class FrmMenuManager
    Private headerMenuTable As New DataTable
    Private subMenuTable As New DataTable

    ' Helper function to convert database boolean values (0/1, "0"/"1") to Boolean
    Private Function ConvertToBoolean(value As Object) As Boolean
        If value Is Nothing Then Return False

        Dim strValue As String = value.ToString().Trim()

        ' Handle numeric values (0, 1)
        If IsNumeric(strValue) Then
            Return Convert.ToInt32(strValue) <> 0
        End If

        ' Handle string boolean values
        Select Case strValue.ToLower()
            Case "true", "yes", "y", "1"
                Return True
            Case "false", "no", "n", "0"
                Return False
            Case Else
                Return False
        End Select
    End Function

    Public Sub New()
        InitializeComponent()
        InitializeDataTables()
        LoadHeaderMenus()
        LoadHeaderMenusToComboBox()
        StyleGridView()
    End Sub

    Private Sub InitializeDataTables()
        Try
            ' Initialize Header Menu DataTable
            headerMenuTable.Columns.Add("phid", GetType(Integer))
            headerMenuTable.Columns.Add("ph_name", GetType(String))
            headerMenuTable.Columns.Add("ph_menucode", GetType(String))
            headerMenuTable.Columns.Add("ph_active", GetType(Boolean))

            ' Initialize Sub Menu DataTable
            subMenuTable.Columns.Add("psid", GetType(Integer))
            subMenuTable.Columns.Add("ps_name", GetType(String))
            subMenuTable.Columns.Add("ps_menucode", GetType(String))
            subMenuTable.Columns.Add("ph_id", GetType(Integer))
            subMenuTable.Columns.Add("ps_active", GetType(Boolean))
            subMenuTable.Columns.Add("HeaderMenuName", GetType(String))

            ' Set data sources
            dgvHeaderMenu.DataSource = headerMenuTable
            dgvSubMenu.DataSource = subMenuTable

        Catch ex As Exception
            XtraMessageBox.Show("Error initializing data tables: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub StyleGridView()
        Try
            ' Apply modern styling to header menu grid
            dgvHeaderMenu.EmbeddedNavigator.Buttons.Append.Visible = False
            dgvHeaderMenu.EmbeddedNavigator.Buttons.Remove.Visible = False
            dgvHeaderMenu.EmbeddedNavigator.Buttons.Edit.Visible = False

            ' Style the header menu grid view
            Dim headerView As GridView = TryCast(dgvHeaderMenu.MainView, GridView)
            If headerView IsNot Nothing Then
                headerView.OptionsView.ShowGroupPanel = False
                headerView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
                headerView.OptionsSelection.EnableAppearanceFocusedCell = False
                headerView.OptionsSelection.EnableAppearanceFocusedRow = True
                headerView.Appearance.FocusedRow.BackColor = Color.FromArgb(49, 106, 197)
                headerView.Appearance.FocusedRow.ForeColor = Color.White
                headerView.Appearance.SelectedRow.BackColor = Color.FromArgb(49, 106, 197)
                headerView.Appearance.SelectedRow.ForeColor = Color.White
                headerView.OptionsView.EnableAppearanceEvenRow = True
                headerView.Appearance.EvenRow.BackColor = Color.FromArgb(250, 250, 250)
                headerView.OptionsView.RowAutoHeight = True
                headerView.OptionsCustomization.AllowColumnMoving = False
                headerView.OptionsCustomization.AllowColumnResizing = True

                ' Set column headers
                If headerView.Columns.Count > 0 Then
                    headerView.Columns("phid").Caption = "ID"
                    headerView.Columns("phid").Width = 50
                    headerView.Columns("ph_name").Caption = "Header Menu Name"
                    headerView.Columns("ph_name").Width = 200
                    headerView.Columns("ph_menucode").Caption = "Menu Code"
                    headerView.Columns("ph_menucode").Width = 120
                    headerView.Columns("ph_active").Caption = "Active"
                    headerView.Columns("ph_active").Width = 80
                End If
            End If

            ' Style the sub menu grid view
            Dim subView As GridView = TryCast(dgvSubMenu.MainView, GridView)
            If subView IsNot Nothing Then
                subView.OptionsView.ShowGroupPanel = False
                subView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
                subView.OptionsSelection.EnableAppearanceFocusedCell = False
                subView.OptionsSelection.EnableAppearanceFocusedRow = True
                subView.Appearance.FocusedRow.BackColor = Color.FromArgb(49, 106, 197)
                subView.Appearance.FocusedRow.ForeColor = Color.White
                subView.Appearance.SelectedRow.BackColor = Color.FromArgb(49, 106, 197)
                subView.Appearance.SelectedRow.ForeColor = Color.White
                subView.OptionsView.EnableAppearanceEvenRow = True
                subView.Appearance.EvenRow.BackColor = Color.FromArgb(250, 250, 250)
                subView.OptionsView.RowAutoHeight = True
                subView.OptionsCustomization.AllowColumnMoving = False
                subView.OptionsCustomization.AllowColumnResizing = True

                ' Set column headers
                If subView.Columns.Count > 0 Then
                    subView.Columns("psid").Caption = "ID"
                    subView.Columns("psid").Width = 50
                    subView.Columns("ps_name").Caption = "Sub Menu Name"
                    subView.Columns("ps_name").Width = 200
                    subView.Columns("ps_menucode").Caption = "Menu Code"
                    subView.Columns("ps_menucode").Width = 120
                    subView.Columns("ph_id").Caption = "Header ID"
                    subView.Columns("ph_id").Width = 80
                    subView.Columns("ps_active").Caption = "Active"
                    subView.Columns("ps_active").Width = 80
                    subView.Columns("HeaderMenuName").Caption = "Header Menu"
                    subView.Columns("HeaderMenuName").Width = 150
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error styling grids: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadHeaderMenus()
        Dim json As String = ""
        Try
            headerMenuTable.Clear()
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            ' Simple GET request - now works since PHP API accepts both GET and POST
            json = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "MenuRequest=6&json=" & "{}")

            ' Debug: Show raw JSON response
            If String.IsNullOrEmpty(json) Then
                XtraMessageBox.Show("Empty response from server", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Debug: Show first 500 characters of JSON
            Dim debugJson As String = If(json.Length > 500, json.Substring(0, 500) & "...", json)
            Console.WriteLine("Raw JSON Response: " & debugJson)

            Dim parsedJson As JObject = JObject.Parse(json)

            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")
                For Each item In dataArray
                    headerMenuTable.Rows.Add(
                        Convert.ToInt32(item("phid")),
                        item("ph_name").ToString(),
                        item("ph_menucode").ToString(),
                        ConvertToBoolean(item("ph_active"))
                    )
                Next
            End If
        Catch jsonEx As JsonReaderException
            XtraMessageBox.Show("JSON Parse Error: " & jsonEx.Message & vbCrLf & "Raw response: " & json, "JSON Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            XtraMessageBox.Show("Error loading header menus: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadHeaderMenusToComboBox()
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            ' Simple GET request - now works since PHP API accepts both GET and POST
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "MenuRequest=6")
            Dim parsedJson As JObject = JObject.Parse(json)

            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")
                Dim dt As New DataTable()
                dt.Columns.Add("phid", GetType(Integer))
                dt.Columns.Add("ph_name", GetType(String))

                For Each item In dataArray
                    If ConvertToBoolean(item("ph_active")) Then
                        dt.Rows.Add(Convert.ToInt32(item("phid")), item("ph_name").ToString())
                    End If
                Next

                cmbHeaderMenu.Properties.DataSource = dt
                cmbHeaderMenu.Properties.DisplayMember = "ph_name"
                cmbHeaderMenu.Properties.ValueMember = "phid"
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Error loading header menus to combo box: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadSubMenus(Optional headerMenuId As Integer = 0)
        Dim json As String = ""
        Try
            subMenuTable.Clear()
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            ' Simple GET request with headerMenuId parameter
            json = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "MenuRequest=7&headerMenuId=" & headerMenuId)

            ' Debug: Show raw JSON response
            If String.IsNullOrEmpty(json) Then
                XtraMessageBox.Show("Empty response from server for sub menus", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Debug: Show first 500 characters of JSON
            Dim debugJson As String = If(json.Length > 500, json.Substring(0, 500) & "...", json)
            Console.WriteLine("Raw Sub Menu JSON Response: " & debugJson)

            Dim parsedJson As JObject = JObject.Parse(json)

            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")
                For Each item In dataArray
                    ' Handle the header_name field that comes from the API
                    Dim headerName As String = ""
                    If item("header_name") IsNot Nothing Then
                        headerName = item("header_name").ToString()
                    End If

                    subMenuTable.Rows.Add(
                        Convert.ToInt32(item("psid")),
                        item("ps_name").ToString(),
                        item("ps_menucode").ToString(),
                        Convert.ToInt32(item("ph_id")),
                        ConvertToBoolean(item("ps_active")),
                        headerName
                    )
                Next
            End If
        Catch jsonEx As JsonReaderException
            XtraMessageBox.Show("JSON Parse Error in LoadSubMenus: " & jsonEx.Message & vbCrLf & "Raw response: " & json, "JSON Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            XtraMessageBox.Show("Error loading sub menus: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
       
    Private Sub btnAddHeader_Click(sender As Object, e As EventArgs) Handles btnAddHeader.Click
        Try
            If ValidateHeaderInput() Then
                If isEditingHeader Then
                    ' Update existing header menu
                    Dim menuData = New With {
                        .phid = selectedHeaderId,
                        .ph_name = txtHeaderName.Text.Trim(),
                        .ph_menucode = txtHeaderCode.Text.Trim(),
                        .ph_active = If(chkHeaderActive.Checked, 1, 0),
                        .ph_projectid = 1
                    }

                    Dim jsonData As String = JsonConvert.SerializeObject(menuData)

                    If _JsonSend(M_Details.LinkAjaxRequest & "MenuRequest=1&json=" & jsonData) = True Then
                        XtraMessageBox.Show("Header Menu updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ResetHeaderForm()
                        LoadHeaderMenus()
                        LoadHeaderMenusToComboBox()
                    Else
                        XtraMessageBox.Show("Failed to update Header Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    ' Add new header menu
                    Dim menuData = New With {
                        .phid = 0,
                        .ph_name = txtHeaderName.Text.Trim(),
                        .ph_menucode = txtHeaderCode.Text.Trim(),
                        .ph_active = If(chkHeaderActive.Checked, 1, 0),
                        .ph_projectid = 1
                    }

                    Dim jsonData As String = JsonConvert.SerializeObject(menuData)

                    If _JsonSend(M_Details.LinkAjaxRequest & "MenuRequest=1&json=" & jsonData) = True Then
                        XtraMessageBox.Show("Header Menu added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ClearHeaderInputs()
                        LoadHeaderMenus()
                        LoadHeaderMenusToComboBox()
                    Else
                        XtraMessageBox.Show("Failed to add Header Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Error processing header menu: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAddSubMenu_Click(sender As Object, e As EventArgs) Handles btnAddSubMenu.Click
        Try
            If ValidateSubMenuInput() Then
                If isEditingSubMenu Then
                    ' Update existing sub menu
                    Dim subMenuData = New With {
                        .psid = selectedSubMenuId,
                        .ps_name = txtSubMenuName.Text.Trim(),
                        .ps_menucode = txtSubMenuCode.Text.Trim(),
                        .ph_id = Convert.ToInt32(cmbHeaderMenu.EditValue),
                        .ps_active = If(chkSubMenuActive.Checked, 1, 0)
                    }

                    Dim jsonData As String = JsonConvert.SerializeObject(subMenuData)

                    If _JsonSend(M_Details.LinkAjaxRequest & "MenuRequest=3&json=" & jsonData) = True Then
                        XtraMessageBox.Show("Sub Menu updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ResetSubMenuForm()
                        LoadSubMenus(Convert.ToInt32(cmbHeaderMenu.EditValue))
                    Else
                        XtraMessageBox.Show("Failed to update Sub Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    ' Add new sub menu
                    Dim subMenuData = New With {
                        .psid = 0,
                        .ps_name = txtSubMenuName.Text.Trim(),
                        .ps_menucode = txtSubMenuCode.Text.Trim(),
                        .ph_id = Convert.ToInt32(cmbHeaderMenu.EditValue),
                        .ps_active = If(chkSubMenuActive.Checked, 1, 0)
                    }

                    Dim jsonData As String = JsonConvert.SerializeObject(subMenuData)

                    If _JsonSend(M_Details.LinkAjaxRequest & "MenuRequest=3&json=" & jsonData) = True Then
                        XtraMessageBox.Show("Sub Menu added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ClearSubMenuInputs()
                        LoadSubMenus(Convert.ToInt32(cmbHeaderMenu.EditValue))
                    Else
                        XtraMessageBox.Show("Failed to add Sub Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Error processing sub menu: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ValidateHeaderInput() As Boolean
        If String.IsNullOrEmpty(txtHeaderName.Text.Trim()) Then
            XtraMessageBox.Show("Please enter Header Menu Name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtHeaderName.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(txtHeaderCode.Text.Trim()) Then
            XtraMessageBox.Show("Please enter Header Menu Code.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtHeaderCode.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function ValidateSubMenuInput() As Boolean
        If cmbHeaderMenu.EditValue Is Nothing Then
            XtraMessageBox.Show("Please select a Header Menu.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbHeaderMenu.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(txtSubMenuName.Text.Trim()) Then
            XtraMessageBox.Show("Please enter Sub Menu Name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSubMenuName.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(txtSubMenuCode.Text.Trim()) Then
            XtraMessageBox.Show("Please enter Sub Menu Code.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSubMenuCode.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub ClearHeaderInputs()
        txtHeadId.Text = ""
        txtHeaderName.Text = ""
        txtHeaderCode.Text = ""
        chkHeaderActive.Checked = True
    End Sub

    Private Sub ClearSubMenuInputs()
        txtSubmenuId.Text = ""
        txtSubMenuName.Text = ""
        txtSubMenuCode.Text = ""
        chkSubMenuActive.Checked = True
    End Sub

    Private Sub ResetHeaderForm()
        ClearHeaderInputs()
        isEditingHeader = False
        selectedHeaderId = 0
        btnAddHeader.Text = "Add Header"
    End Sub

    Private Sub ResetSubMenuForm()
        ClearSubMenuInputs()
        isEditingSubMenu = False
        selectedSubMenuId = 0
        btnAddSubMenu.Text = "Add Sub Menu"
    End Sub

    ' Delete Header Menu - Uncomment when btnDeleteHeader is added to the form
    'Private Sub btnDeleteHeader_Click(sender As Object, e As EventArgs) Handles btnDeleteHeader.Click
    Private Sub DeleteHeaderMenu()
        Try
            If selectedHeaderId > 0 Then
                If XtraMessageBox.Show("Are you sure you want to delete this Header Menu?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim deleteData = New With {.phid = selectedHeaderId}
                    Dim jsonData As String = JsonConvert.SerializeObject(deleteData)

                    If _JsonSend(M_Details.LinkAjaxRequest & "MenuRequest=2&json=" & jsonData) = True Then
                        XtraMessageBox.Show("Header Menu deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ResetHeaderForm()
                        LoadHeaderMenus()
                        LoadHeaderMenusToComboBox()
                    Else
                        XtraMessageBox.Show("Failed to delete Header Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            Else
                XtraMessageBox.Show("Please select a header menu to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Error deleting header menu: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Delete Sub Menu - Uncomment when btnDeleteSubMenu is added to the form
    'Private Sub btnDeleteSubMenu_Click(sender As Object, e As EventArgs) Handles btnDeleteSubMenu.Click
    Private Sub DeleteSubMenu()
        Try
            If selectedSubMenuId > 0 Then
                If XtraMessageBox.Show("Are you sure you want to delete this Sub Menu?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim deleteData = New With {.psid = selectedSubMenuId}
                    Dim jsonData As String = JsonConvert.SerializeObject(deleteData)

                    If _JsonSend(M_Details.LinkAjaxRequest & "MenuRequest=4&json=" & jsonData) = True Then
                        XtraMessageBox.Show("Sub Menu deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ResetSubMenuForm()
                        LoadSubMenus()
                    Else
                        XtraMessageBox.Show("Failed to delete Sub Menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            Else
                XtraMessageBox.Show("Please select a sub menu to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Error deleting sub menu: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbHeaderMenu_EditValueChanged(sender As Object, e As EventArgs) Handles cmbHeaderMenu.EditValueChanged
        Try
            If cmbHeaderMenu.EditValue IsNot Nothing Then
                LoadSubMenus(Convert.ToInt32(cmbHeaderMenu.EditValue))
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Error loading sub menus: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private selectedHeaderId As Integer = 0
    Private selectedSubMenuId As Integer = 0
    Private isEditingHeader As Boolean = False
    Private isEditingSubMenu As Boolean = False

    Private Sub dgvHeaderMenu_DoubleClick(sender As Object, e As EventArgs) Handles dgvHeaderMenu.DoubleClick
        Try
            Dim view As GridView = TryCast(dgvHeaderMenu.MainView, GridView)
            If view IsNot Nothing AndAlso view.FocusedRowHandle >= 0 Then
                Dim selectedRow As DataRowView = DirectCast(view.GetRow(view.FocusedRowHandle), DataRowView)
                If selectedRow IsNot Nothing Then
                    selectedHeaderId = Convert.ToInt32(selectedRow("phid"))
                    txtHeaderName.Text = selectedRow("ph_name").ToString()
                    txtHeaderCode.Text = selectedRow("ph_menucode").ToString()
                    chkHeaderActive.Checked = ConvertToBoolean(selectedRow("ph_active"))
                    isEditingHeader = True
                    btnAddHeader.Text = "Update Header"
                    txtHeadId.Text = selectedHeaderId
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Error loading header menu details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvSubMenu_DoubleClick(sender As Object, e As EventArgs) Handles dgvSubMenu.DoubleClick
        Try
            Dim view As GridView = TryCast(dgvSubMenu.MainView, GridView)
            If view IsNot Nothing AndAlso view.FocusedRowHandle >= 0 Then
                Dim selectedRow As DataRowView = DirectCast(view.GetRow(view.FocusedRowHandle), DataRowView)
                If selectedRow IsNot Nothing Then
                    selectedSubMenuId = Convert.ToInt32(selectedRow("psid"))
                    cmbHeaderMenu.EditValue = Convert.ToInt32(selectedRow("ph_id"))
                    txtSubMenuName.Text = selectedRow("ps_name").ToString()
                    txtSubMenuCode.Text = selectedRow("ps_menucode").ToString()
                    chkSubMenuActive.Checked = ConvertToBoolean(selectedRow("ps_active"))
                    isEditingSubMenu = True
                    btnAddSubMenu.Text = "Update Sub Menu"
                    txtSubmenuId.Text = selectedSubMenuId
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Error loading sub menu details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            LoadHeaderMenus()
            LoadHeaderMenusToComboBox()
            LoadSubMenus()
            ResetHeaderForm()
            ResetSubMenuForm()
        Catch ex As Exception
            XtraMessageBox.Show("Error refreshing data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Clear Header Form - Uncomment when btnClearHeader is added to the form
    'Private Sub btnClearHeader_Click(sender As Object, e As EventArgs) Handles btnClearHeader.Click
    Private Sub ClearHeaderForm()
        ResetHeaderForm()
    End Sub

    ' Clear Sub Menu Form - Uncomment when btnClearSubMenu is added to the form
    'Private Sub btnClearSubMenu_Click(sender As Object, e As EventArgs) Handles btnClearSubMenu.Click
    Private Sub ClearSubMenuForm()
        ResetSubMenuForm()
    End Sub

    Private Sub FrmMenuManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = "Menu Manager - Header & Sub Menu Management"
            LoadSubMenus() ' Load all sub menus initially
        Catch ex As Exception
            XtraMessageBox.Show("Error loading form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Handle keyboard shortcuts
    Private Sub FrmMenuManager_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                ' Check which grid has focus and delete accordingly
                If dgvHeaderMenu.Focused AndAlso selectedHeaderId > 0 Then
                    DeleteHeaderMenu()
                ElseIf dgvSubMenu.Focused AndAlso selectedSubMenuId > 0 Then
                    DeleteSubMenu()
                End If
            ElseIf e.KeyCode = Keys.F5 Then
                ' Refresh data
                btnRefresh_Click(sender, e)
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Error handling keyboard shortcut: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


End Class
