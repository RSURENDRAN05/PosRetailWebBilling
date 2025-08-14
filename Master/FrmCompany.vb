Imports Newtonsoft.Json

Public Class FrmCompany

    Private Sub frmCompany_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            txtcompany.Select()
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
            Dim comp As New Companys
            If btnsave.Text = "Save" Then
                dialog.Caption = "Connecting To Server"
                If txtcompany.Text.Length > 0 Then
                    comp.id = 0
                    comp.companyname = txtcompany.Text
                    comp.active = chkactive.CheckState
                    Dim PostString As String = JsonConvert.SerializeObject(comp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=5&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _DataLoad()
                    End If
                    txtid.Text = ""
                    txtcompany.Text = ""
                    btnsave.Text = "Save"
                End If
            Else
                dialog.Caption = "Connecting To Server"
                If txtcompany.Text.Length > 0 Then
                    comp.id = txtid.Text
                    comp.companyname = txtcompany.Text
                    comp.active = chkactive.CheckState
                    Dim PostString As String = JsonConvert.SerializeObject(comp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=6&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _DataLoad()
                    End If
                End If
                txtid.Text = ""
                txtcompany.Text = ""
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
            getComapnyInfo()
            If _JsonData.CompanyTable.Rows.Count > 0 Then
                GridControl1.DataSource = _JsonData.CompanyTable
            End If
            chkactive.CheckState = CheckState.Checked
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            btnsave.Text = "Update"
            Dim id = GridView1.GetFocusedRowCellValue("COID")
            Dim bname = GridView1.GetFocusedRowCellValue("CompanyName")
            Dim chkvalue = GridView1.GetFocusedRowCellValue("Active")

            txtid.Text = id
            txtcompany.Text = bname
            chkactive.Checked = chkvalue
        Catch ex As Exception

        End Try
    End Sub
End Class
Class Companys
    Public Property id As String
    Public Property companyname As String
    Public Property active As String
End Class