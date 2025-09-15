
Imports Newtonsoft.Json

Public Class FrmCateMaster

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
            _DataLoad()
            _LoadMainMaster()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _DataLoad()
        Try
            getCategoryMaster()
            If _JsonData.CategoryTable.Rows.Count > 0 Then
                GridControl1.DataSource = _JsonData.CategoryTable
            End If
            chkactive.CheckState = CheckState.Checked
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _LoadMainMaster()
        Try
            getMainMaster()
            If _JsonData.MainGroupTable.Rows.Count > 0 Then
                txtmainname.Properties.DataSource = _JsonData.MainGroupTable
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            If txtcatename.Text = "" OrElse txtcatename.EditValue Is Nothing Then
                txtcatename.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtcatename.Properties.Appearance.BackColor = Color.White
            End If
            If txtposition.Text = "" OrElse txtposition.EditValue Is Nothing Then
                txtposition.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtposition.Properties.Appearance.BackColor = Color.White
            End If
            Dim comp As New clscatemaster
            Dim btnprop As New ButtonPropertiesData
            If btnsave.Text = "Save" Then
                dialog.Caption = "Connecting To Server"
                If txtmainname.Text.Length > 0 Then
                    comp.cateid = 0
                    comp.catename = txtcatename.Text
                    comp.mainid = txtmainname.EditValue
                    comp.color = ColorTranslator.ToHtml(colorEdit1.Color)
                    comp.position = txtposition.Text
                    comp.active = chkactive.CheckState
                    Dim RturnId As String = "0"
                    Dim PostString As String = JsonConvert.SerializeObject(comp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=17&json=" & PostString, RturnId, "0") = True Then
                        dialog.Caption = "Data Saved Success.."
                        ' Save default button settings after successful update
                        btnprop.SaveDefaultButtonSettings(RturnId, "Sub")
                        _DataLoad()
                    End If
                    txtcateid.Text = ""
                    txtcatename.Text = ""
                    txtmainname.Text = ""
                    txtposition.Text = "0"
                    btnsave.Text = "Save"
                End If
            Else
                dialog.Caption = "Connecting To Server"
                If txtcatename.Text.Length > 0 Then
                    comp.cateid = txtcateid.Text
                    comp.catename = txtcatename.Text
                    comp.color = ColorTranslator.ToHtml(colorEdit1.Color)
                    comp.position = txtposition.Text
                    comp.mainid = txtmainname.EditValue
                    comp.active = chkactive.CheckState
                    Dim PostString As String = JsonConvert.SerializeObject(comp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=18&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _DataLoad()
                    End If
                End If
                txtcateid.Text = ""
                txtcatename.Text = ""
                txtmainname.Text = ""
                txtposition.Text = "0"
                btnsave.Text = "Save"
            End If

        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub
    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            btnsave.Text = "Update"
            Dim id = GridView1.GetFocusedRowCellValue("CateId")
            Dim catename = GridView1.GetFocusedRowCellValue("CateName")
            Dim mainname = GridView1.GetFocusedRowCellValue("MainName")
            Dim mainposition = GridView1.GetFocusedRowCellValue("Position")
            Dim maincolor = GridView1.GetFocusedRowCellValue("Color")
            Dim chkvalue = GridView1.GetFocusedRowCellValue("Active")

            txtcateid.Text = id
            txtcatename.Text = catename
            txtmainname.Text = mainname
            txtposition.Text = mainposition
            chkactive.Checked = chkvalue
            If maincolor IsNot Nothing AndAlso maincolor.ToString() <> "" Then
                Try
                    colorEdit1.EditValue = ColorTranslator.FromHtml(maincolor.ToString())
                Catch
                    colorEdit1.EditValue = Color.LightBlue
                End Try
            Else
                colorEdit1.EditValue = Color.LightBlue
            End If
        Catch ex As Exception

        End Try
    End Sub
    
End Class
Public Class clscatemaster
    Public Property cateid As String
    Public Property catename As String
    Public Property mainid As String
    Public Property mainname As String
    Public Property color As String
    Public Property position As String
    Public Property active As String
End Class

 