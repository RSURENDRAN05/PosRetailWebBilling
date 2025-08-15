Imports Newtonsoft.Json
Imports System.Data
Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class FrmNewUser
    Dim _modeOfSave As String = "New"
    Private groupsTable As DataTable
    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Me.Close()
    End Sub
    Private Sub new_load()
        Try
            _modeOfSave = "New"
            btnsave.Text = "Save"
            txtpassword.Text = ""
            txtuserid.Text = ""
            txtusername.Text = ""
            chkuseractive.Checked = True
            LoadUserGroups()
            loadData()
        Catch ex As Exception
            MessageBox.Show("Error in new_load: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub loadData()
        Try
            getUserInfo()
            GridControl1.DataSource = Nothing
            If _JsonData.USerTable.Rows.Count > 0 Then
                GridControl1.DataSource = _JsonData.USerTable
                'If _companyInfo.UserRole = "Admin" Then
                '    GridControl1.DataSource = _JsonData.USerTable.Select("Id =" & _companyInfo.UserId).CopyToDataTable
                'Else
                '    Dim dataView As DataView = _JsonData.USerTable.DefaultView
                '    dataView.RowFilter = ("Role like '" & _companyInfo.UserRole & "'")
                '    GridControl1.DataSource = dataView
                '    cmbrole.Properties.ReadOnly = True
                'End If
            Else
                GridControl1.DataSource = Nothing
            End If
        Catch ex As Exception
            GridControl1.DataSource = Nothing
        End Try
    End Sub

    Private Sub LoadUserGroups()
        Try
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "GroupPolicyRequest=1")
            Dim parsedJson As JObject = JObject.Parse(json)

            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")
                groupsTable = New DataTable()

                groupsTable.Columns.Add("pug_id", GetType(Integer))
                groupsTable.Columns.Add("pug_name", GetType(String))
                groupsTable.Columns.Add("pug_description", GetType(String))
                groupsTable.Columns.Add("pug_active", GetType(Boolean))

                ' Clear existing items
                cmbrole.Properties.Items.Clear()

                For Each item In dataArray
                    If ConvertToBoolean(item("pug_active")) Then
                        groupsTable.Rows.Add(
                            Convert.ToInt32(item("pug_id")),
                            item("pug_name").ToString(),
                            item("pug_description").ToString(),
                            ConvertToBoolean(item("pug_active"))
                        )
                        ' Add group name to combobox
                        cmbrole.Properties.Items.Add(item("pug_name").ToString())
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading user groups: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Function GetGroupIdByName(groupName As String) As Integer
        If groupsTable IsNot Nothing Then
            For Each row As DataRow In groupsTable.Rows
                If row("pug_name").ToString() = groupName Then
                    Return Convert.ToInt32(row("pug_id"))
                End If
            Next
        End If
        Return 0
    End Function

    Private Function GetGroupNameById(groupId As Integer) As String
        If groupsTable IsNot Nothing Then
            For Each row As DataRow In groupsTable.Rows
                If Convert.ToInt32(row("pug_id")) = groupId Then
                    Return row("pug_name").ToString()
                End If
            Next
        End If
        Return ""
    End Function
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            dialog.Caption = "Verfiying Data"
            Dim jsonUserInfo As New UserInfo
            If _modeOfSave = "New" Then
                jsonUserInfo.userid = 0
                jsonUserInfo.username = txtusername.Text
                jsonUserInfo.userpass = txtpassword.Text
                jsonUserInfo.usercomid = _companyInfo.ComId
                jsonUserInfo.userrestid = _companyInfo.LocId

                ' Get group ID from selected group name
                Dim selectedGroupId As Integer = GetGroupIdByName(cmbrole.SelectedItem.ToString())
                jsonUserInfo.userrole = selectedGroupId

                If chkuseractive.CheckState = CheckState.Checked Then
                    jsonUserInfo.useractive = 1
                Else
                    jsonUserInfo.useractive = 0
                End If
                Dim PostString As String = JsonConvert.SerializeObject(jsonUserInfo)
                If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=21&json=" & PostString) = True Then
                    new_load()
                    dialog.Caption = "New Data Created"
                End If

            ElseIf _modeOfSave = "Update" Then
                jsonUserInfo.userid = txtuserid.Text
                jsonUserInfo.username = txtusername.Text
                jsonUserInfo.userpass = Trim(txtpassword.Text)
                jsonUserInfo.usercomid = _companyInfo.ComId
                jsonUserInfo.userrestid = _companyInfo.LocId


                ' Get group ID from selected group name
                Dim selectedGroupId As Integer = GetGroupIdByName(cmbrole.SelectedItem.ToString())
                jsonUserInfo.userrole = selectedGroupId

                If chkuseractive.CheckState = CheckState.Checked Then
                    jsonUserInfo.useractive = 1
                Else
                    jsonUserInfo.useractive = 0
                End If
                Dim PostString As String = JsonConvert.SerializeObject(jsonUserInfo)
                If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=22&json=" & PostString) = True Then
                    new_load()
                    dialog.Caption = "Data Updated"
                End If
            End If
        Catch ex As Exception
            dialog.Caption = ex.Message
        Finally
            dialog.Close()
        End Try
    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click
        Try
            _modeOfSave = "Update"
            btnsave.Text = "Update"
            txtuserid.Text = GridView1.GetFocusedRowCellValue("Id").ToString
            txtusername.Text = GridView1.GetFocusedRowCellValue("UserName").ToString
            'txtpassword.Text = GridView1.GetFocusedRowCellValue("Password").ToString

            ' Get role as group ID and convert to group name
            Dim roleValue = GridView1.GetFocusedRowCellValue("GroupId")
            If IsNumeric(roleValue.ToString()) Then
                ' If it's a group ID, convert to group name
                Dim groupName As String = GetGroupNameById(Convert.ToInt32(roleValue))
                If Not String.IsNullOrEmpty(groupName) Then
                    cmbrole.SelectedItem = groupName
                Else
                    cmbrole.SelectedItem = roleValue.ToString()
                End If
            Else
                ' If it's already a group name
                cmbrole.SelectedItem = roleValue.ToString()
            End If

            Dim _status = GridView1.GetFocusedRowCellValue("Status").ToString
            If _status = "Active" Then
                chkuseractive.Checked = True
            Else
                chkuseractive.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading user data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmUser_Load(sender As Object, e As EventArgs) Handles Me.Load
        new_load()
    End Sub
End Class
Public Class UserInfo
    Public Property userid As Integer
    Public Property username As String
    Public Property userpass As String
    Public Property userrole As Integer
    Public Property useractive As Integer
    Public Property usercomid As Integer
    Public Property userrestid As Integer
End Class
