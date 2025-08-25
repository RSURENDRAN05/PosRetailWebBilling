Imports Newtonsoft.Json

Public Class FrmMainGroup

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FrmTaxMaster_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            txtmainname.Select()
            chkactive.CheckState = CheckState.Checked
            ' Set default color
            colorEdit1.EditValue = Color.LightBlue
            _DataLoad()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _DataLoad()
        Try
            getMainMaster()
            If _JsonData.MainGroupTable.Rows.Count > 0 Then
                GridControl1.DataSource = _JsonData.MainGroupTable
            End If
            chkactive.CheckState = CheckState.Checked
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            If txtmainname.Text = "" OrElse txtmainname.EditValue Is Nothing Then
                txtmainname.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtmainname.Properties.Appearance.BackColor = Color.White
            End If
           
            Dim comp As New clsmainmaster
            If btnsave.Text = "Save" Then
                dialog.Caption = "Connecting To Server"
                If txtmainname.Text.Length > 0 Then
                    comp.id = 0
                    comp.mainname = txtmainname.Text
                    comp.active = chkactive.CheckState
                    comp.groupcolor = ColorTranslator.ToHtml(colorEdit1.Color)
                    Dim PostString As String = JsonConvert.SerializeObject(comp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=13&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _DataLoad()
                    End If
                    txtmainid.Text = ""
                    txtmainname.Text = ""
                    colorEdit1.EditValue = Color.LightBlue
                    btnsave.Text = "Save"
                End If
            Else
                dialog.Caption = "Connecting To Server"
                If txtmainname.Text.Length > 0 Then
                    comp.id = txtmainid.Text
                    comp.mainname = txtmainname.Text
                    comp.active = chkactive.CheckState
                    comp.groupcolor = ColorTranslator.ToHtml(colorEdit1.Color)
                    Dim PostString As String = JsonConvert.SerializeObject(comp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=14&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _DataLoad()
                    End If
                End If
                txtmainid.Text = ""
                txtmainname.Text = ""
                colorEdit1.EditValue = Color.LightBlue
                btnsave.Text = "Save"
            End If

        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click
        Try
            btnsave.Text = "Update"
            Dim id = GridView1.GetFocusedRowCellValue("MainId")
            Dim mainname = GridView1.GetFocusedRowCellValue("MainName")
            Dim chkvalue = GridView1.GetFocusedRowCellValue("Active")
            Dim groupColor = GridView1.GetFocusedRowCellValue("GroupColor")

            txtmainid.Text = id
            txtmainname.Text = mainname
            chkactive.Checked = chkvalue

            ' Load color if available
            If groupColor IsNot Nothing AndAlso groupColor.ToString() <> "" Then
                Try
                    colorEdit1.EditValue = ColorTranslator.FromHtml(groupColor.ToString())
                Catch
                    colorEdit1.EditValue = Color.LightBlue
                End Try
            Else
                colorEdit1.EditValue = Color.LightBlue
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub colorEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles colorEdit1.EditValueChanged
        Try
            ' Optional: You can add code here to respond to color changes
            ' For example, update the form's appearance or validate the color selection
        Catch ex As Exception
        End Try
    End Sub
End Class
Public Class clsmainmaster
    Public Property id As String
    Public Property mainname As String
    Public Property active As String
    Public Property groupcolor As String
End Class
