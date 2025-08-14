 
Imports Newtonsoft.Json

Public Class frmGroup

    Private Sub frmCompany_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            txtgroupname.Select()
            chkactive.CheckState = CheckState.Checked
            _DataLoad()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            Dim comp As New GroupMaster
            If btnsave.Text = "Save" Then
                dialog.Caption = "Connecting To Server"
                If txtgroupname.Text.Length > 0 Then
                    comp.id = 0
                    comp.groupname = txtgroupname.Text

                    Dim PostString As String = JsonConvert.SerializeObject(comp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AccountRequest=1&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _DataLoad()
                    End If
                    txtid.Text = ""
                    txtgroupname.Text = ""
                    btnsave.Text = "Save"
                End If
            Else
                dialog.Caption = "Connecting To Server"
                If txtgroupname.Text.Length > 0 Then
                    comp.id = txtid.Text
                    comp.groupname = txtgroupname.Text

                    Dim PostString As String = JsonConvert.SerializeObject(comp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AccountRequest=2&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _DataLoad()
                    End If
                End If
                txtid.Text = ""
                txtgroupname.Text = ""
                btnsave.Text = "Save"
            End If

        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub
    Private Sub _DataLoad()
        Try
            getGroupInfo()
            If _JsonData.AccountGroup.Rows.Count > 0 Then
                GridControl1.DataSource = _JsonData.AccountGroup
            End If
            chkactive.CheckState = CheckState.Checked
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            btnsave.Text = "Update"
            Dim id = GridView1.GetFocusedRowCellValue("Id")
            Dim bname = GridView1.GetFocusedRowCellValue("Name")
            'Dim chkvalue = GridView1.GetFocusedRowCellValue("Active")

            txtid.Text = id
            txtgroupname.Text = bname
            'chkactive.Checked = chkvalue
        Catch ex As Exception

        End Try
    End Sub
End Class
Class GroupMaster
    Public Property id As String
    Public Property groupname As String

End Class