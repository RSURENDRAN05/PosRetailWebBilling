Imports System.Data
Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class FrmGroupPolicyManagerSimple
    Inherits DevExpress.XtraEditors.XtraForm

    ' Data
    Private permissionsTable As DataTable
    Private selectedGroupId As Integer = 0

    Public Sub New()
        InitializeComponent()
        InitializeForm()
    End Sub

    Private Sub InitializeForm()
        Try
            SetupPermissionsDataTable()
            LoadUserGroups()
            LoadMenusForPermissions()
        Catch ex As Exception
            MessageBox.Show("Error initializing form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetupPermissionsDataTable()
        permissionsTable = New DataTable()
        permissionsTable.Columns.Add("pgmp_id", GetType(Integer))
        permissionsTable.Columns.Add("menu_name", GetType(String))
        permissionsTable.Columns.Add("menu_type", GetType(String))
        permissionsTable.Columns.Add("header_menu_id", GetType(Integer))
        permissionsTable.Columns.Add("sub_menu_id", GetType(Object))
        permissionsTable.Columns.Add("menu_active", GetType(Boolean))

        dgvPermissions.DataSource = permissionsTable
    End Sub

    Private Sub LoadUserGroups()
        Try
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "GroupPolicyRequest=1")
            Dim parsedJson As JObject = JObject.Parse(json)

            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")
                Dim groupsTable As New DataTable()

                groupsTable.Columns.Add("pug_id", GetType(Integer))
                groupsTable.Columns.Add("pug_name", GetType(String))
                groupsTable.Columns.Add("pug_description", GetType(String))
                groupsTable.Columns.Add("pug_active", GetType(Boolean))

                For Each item In dataArray
                    groupsTable.Rows.Add(
                        Convert.ToInt32(item("pug_id")),
                        item("pug_name").ToString(),
                        item("pug_description").ToString(),
                        ConvertToBoolean(item("pug_active"))
                    )
                Next

                dgvGroups.DataSource = groupsTable
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading groups: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadMenusForPermissions()
        Try
            permissionsTable.Clear()
            LoadHeaderMenusForPermissions()
            LoadSubMenusForPermissions()

            ' Debug: Check if data was loaded
            MessageBox.Show("Loaded " & permissionsTable.Rows.Count.ToString() & " menu items", "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error loading menus: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub LoadHeaderMenusForPermissions()
        Try
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "MenuRequest=8")
            Dim parsedJson As JObject = JObject.Parse(json)

            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")
                Dim headerCount As Integer = 0
                For Each item In dataArray
                    If ConvertToBoolean(item("ph_active")) Then
                        permissionsTable.Rows.Add(
                            0,
                            item("ph_name").ToString(),
                            "Header",
                            Convert.ToInt32(item("phid")),
                            DBNull.Value,
                            False
                        )
                        headerCount += 1
                    End If
                Next
                MessageBox.Show("Loaded " & headerCount.ToString() & " header menus", "Debug - Headers", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to load header menus: " & parsedJson("Msg").ToString(), "Debug - Headers Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error in LoadHeaderMenusForPermissions: " & ex.Message, "Debug Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        End Try
    End Sub

    Private Sub LoadSubMenusForPermissions()
        Try
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "MenuRequest=9&headerMenuId=0")
            Dim parsedJson As JObject = JObject.Parse(json)

            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")
                Dim subCount As Integer = 0
                For Each item In dataArray
                    If ConvertToBoolean(item("ps_active")) Then
                        permissionsTable.Rows.Add(
                            0,
                            item("ps_name").ToString(),
                            "Sub Menu",
                            Convert.ToInt32(item("ph_id")),
                            Convert.ToInt32(item("psid")),
                            False
                        )
                        subCount += 1
                    End If
                Next

            Else
                MessageBox.Show("Failed to load sub menus: " & parsedJson("Msg").ToString(), "Debug - Sub Menus Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error in LoadSubMenusForPermissions: " & ex.Message, "Debug Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        End Try
    End Sub

    Private Sub gvGroups_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gvGroups.FocusedRowChanged
        Try
            If gvGroups.FocusedRowHandle >= 0 Then
                selectedGroupId = Convert.ToInt32(gvGroups.GetFocusedRowCellValue("pug_id"))
                txtGroupName.Text = gvGroups.GetFocusedRowCellValue("pug_name").ToString()
                txtGroupDescription.Text = gvGroups.GetFocusedRowCellValue("pug_description").ToString()
                chkGroupActive.Checked = Convert.ToBoolean(gvGroups.GetFocusedRowCellValue("pug_active"))
                btnSaveGroup.Text = "Update Group"

                LoadGroupPermissions()
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading group details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadGroupPermissions()
        Try
            If selectedGroupId > 0 Then
                Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "GroupPolicyRequest=4&group_id=" & selectedGroupId)
                Dim parsedJson As JObject = JObject.Parse(json)

                If parsedJson("Success").ToString() = "True" Then
                    Dim dataArray = parsedJson("Data")

                    ' Reset all permissions to false
                    For Each row As DataRow In permissionsTable.Rows
                        row("menu_active") = False
                    Next

                    ' Set permissions from database
                    For Each item In dataArray
                        Dim headerMenuId As Integer = If(item("header_menu_id") Is Nothing OrElse IsDBNull(item("header_menu_id")), 0, Convert.ToInt32(item("header_menu_id")))
                        Dim subMenuId As Object = If(item("sub_menu_id") Is Nothing OrElse IsDBNull(item("sub_menu_id")), DBNull.Value, Convert.ToInt32(item("sub_menu_id")))

                        For Each row As DataRow In permissionsTable.Rows
                            Dim rowHeaderId As Integer = Convert.ToInt32(row("header_menu_id"))
                            Dim rowSubId As Object = row("sub_menu_id")

                            If rowHeaderId = headerMenuId AndAlso
                               ((IsDBNull(rowSubId) AndAlso IsDBNull(subMenuId)) OrElse
                                (Not IsDBNull(rowSubId) AndAlso Not IsDBNull(subMenuId) AndAlso Convert.ToInt32(rowSubId) = Convert.ToInt32(subMenuId))) Then

                                row("pgmp_id") = Convert.ToInt32(item("pgmp_id"))
                                row("menu_active") = ConvertToBoolean(item("menu_active"))
                                Exit For
                            End If
                        Next
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading group permissions: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSaveGroup_Click(sender As Object, e As EventArgs) Handles btnSaveGroup.Click
        Try
            If String.IsNullOrWhiteSpace(txtGroupName.Text) Then
                MessageBox.Show("Please enter a group name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim groupData = New With {
                .group_id = selectedGroupId,
                .group_name = txtGroupName.Text.Trim(),
                .group_description = txtGroupDescription.Text.Trim(),
                .group_active = If(chkGroupActive.Checked, 1, 0)
            }

            Dim jsonString As String = JsonConvert.SerializeObject(groupData)
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "GroupPolicyRequest=2&json=" & jsonString)
            Dim parsedJson As JObject = JObject.Parse(json)

            If parsedJson("Success").ToString() = "True" Then
                MessageBox.Show(parsedJson("Msg").ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadUserGroups()
                ClearGroupInputs()
            Else
                MessageBox.Show(parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error saving group: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSavePermissions_Click(sender As Object, e As EventArgs) Handles btnSavePermissions.Click
        Try
            If selectedGroupId = 0 Then
                MessageBox.Show("Please select a group first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim permissionsList As New List(Of Object)

            For Each row As DataRow In permissionsTable.Rows
                If ConvertToBoolean(row("menu_active")) Then

                    Dim permission = New With {
                        .pgmp_id = Convert.ToInt32(row("pgmp_id")),
                        .header_menu_id = If(IsDBNull(row("header_menu_id")), Nothing, Convert.ToInt32(row("header_menu_id"))),
                        .sub_menu_id = If(IsDBNull(row("sub_menu_id")), Nothing, Convert.ToInt32(row("sub_menu_id"))),
                        .menu_active = If(ConvertToBoolean(row("menu_active")), 1, 0)
                    }
                    permissionsList.Add(permission)
                End If
            Next

            Dim permissionData = New With {
                .group_id = selectedGroupId,
                .permissions = permissionsList
            }

            Dim jsonString As String = JsonConvert.SerializeObject(permissionData)
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "GroupPolicyRequest=5&json=" & jsonString)
            Dim parsedJson As JObject = JObject.Parse(json)

            If parsedJson("Success").ToString() = "True" Then
                MessageBox.Show("Permissions saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show(parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error saving permissions: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearGroupInputs()
        txtGroupName.Text = ""
        txtGroupDescription.Text = ""
        chkGroupActive.Checked = True
        btnSaveGroup.Text = "Add Group"
        selectedGroupId = 0
    End Sub

    Private Sub btnDeleteGroup_Click(sender As Object, e As EventArgs) Handles btnDeleteGroup.Click
        Try
            If selectedGroupId > 0 Then
                If MessageBox.Show("Are you sure you want to delete this group?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim groupData = New With {.group_id = selectedGroupId}
                    Dim jsonString As String = JsonConvert.SerializeObject(groupData)
                    Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "GroupPolicyRequest=3&json=" & jsonString)
                    Dim parsedJson As JObject = JObject.Parse(json)

                    If parsedJson("Success").ToString() = "True" Then
                        MessageBox.Show(parsedJson("Msg").ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadUserGroups()
                        ClearGroupInputs()
                    Else
                        MessageBox.Show(parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            Else
                MessageBox.Show("Please select a group to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error deleting group: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearGroup_Click(sender As Object, e As EventArgs) Handles btnClearGroup.Click
        ClearGroupInputs()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            LoadUserGroups()
            LoadMenusForPermissions()
        Catch ex As Exception
            MessageBox.Show("Error refreshing data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ConvertToBoolean(value As Object) As Boolean
        If value Is Nothing Then Return False
        Dim strValue As String = value.ToString().Trim()
        If IsNumeric(strValue) Then
            Return Convert.ToInt32(strValue) <> 0
        End If
        Select Case strValue.ToLower()
            Case "true", "yes", "y", "1"
                Return True
            Case "false", "no", "n", "0"
                Return False
            Case Else
                Return False
        End Select
    End Function
End Class
