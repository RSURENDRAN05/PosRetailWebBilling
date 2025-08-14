Imports Newtonsoft.Json
Public Class FrmNewUser
    Dim _modeOfSave As String = "New"
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
            If _companyInfo.UserRole = "Admin" Then
                cmbrole.SelectedIndex = 0
            End If
            chkuseractive.Checked = True
            loadData()
        Catch ex As Exception

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
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try

            dialog.Caption = "Verfiying Data"
            Dim jsonUserInfo As New UserInfo
            If _modeOfSave = "New" Then
                jsonUserInfo.userid = 0
                jsonUserInfo.username = txtusername.Text
                jsonUserInfo.userpass = txtpassword.Text
                jsonUserInfo.usercomid = txtcomid.Text
                jsonUserInfo.userrestid = txtrestid.Text
                If cmbrole.SelectedItem = "Admin" Then
                    jsonUserInfo.userrole = 1
                Else
                    jsonUserInfo.userrole = 2
                End If
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
                jsonUserInfo.usercomid = txtcomid.Text
                jsonUserInfo.userrestid = txtrestid.Text
                If cmbrole.SelectedItem = "Admin" Then
                    jsonUserInfo.userrole = 1
                Else
                    jsonUserInfo.userrole = 2
                End If
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
            cmbrole.SelectedItem = GridView1.GetFocusedRowCellValue("Role").ToString
            Dim _status = GridView1.GetFocusedRowCellValue("Status").ToString
            If _status = "Active" Then
                chkuseractive.Checked = True
            Else
                chkuseractive.Checked = False
            End If

        Catch ex As Exception

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