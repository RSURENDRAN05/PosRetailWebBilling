
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
            If btnsave.Text = "Save" Then
                dialog.Caption = "Connecting To Server"
                If txtmainname.Text.Length > 0 Then
                    comp.cateid = 0
                    comp.catename = txtcatename.Text
                    comp.mainid = txtmainname.EditValue
                    comp.color = ColorTranslator.ToHtml(colorEdit1.Color)
                    comp.position = txtposition.Text
                    comp.active = chkactive.CheckState
                    Dim PostString As String = JsonConvert.SerializeObject(comp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=17&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
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

                        ' Save default button settings after successful update
                        SaveDefaultButtonSettings()
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

    ' Save default button settings for the newly created/updated category
    Private Sub SaveDefaultButtonSettings()
        Try
            ' Get the CateId (either from txtcateid for updates or get the latest for new records)
            Dim cateId As Integer = 0

            If btnsave.Text = "Update" AndAlso Not String.IsNullOrEmpty(txtcateid.Text) Then
                ' For updates, use the existing CateId
                cateId = Convert.ToInt32(txtcateid.Text)
            Else
                ' For new records, get the latest CateId from the data
                If _JsonData.CategoryTable IsNot Nothing AndAlso _JsonData.CategoryTable.Rows.Count > 0 Then
                    ' Get the maximum CateId (assuming it's the newly inserted one)
                    cateId = _JsonData.CategoryTable.AsEnumerable().Max(Function(row) Convert.ToInt32(row("CateId")))
                End If
            End If

            If cateId > 0 Then
                ' Create default button properties for Sub menu type
                Dim buttonProperties As New ButtonPropertiesData()
                buttonProperties.item_id = cateId
                buttonProperties.menu_type = "Sub"
                buttonProperties.font_size = 9.0
                buttonProperties.font_name = "Segoe UI"
                buttonProperties.font_style = "Regular"
                buttonProperties.text_color = String.Format("Argb({0},{1},{2},{3})", 255, 0, 0, 0) ' Black text
                buttonProperties.back_color = String.Format("Argb({0},{1},{2},{3})", 255, 176, 196, 222) ' LightSteelBlue background
                buttonProperties.position = 0
                buttonProperties.button_width = 110
                buttonProperties.button_height = 50

                ' Send to PHP API
                Dim postData As String = CreateButtonPostData(buttonProperties)
                Dim phpUrl As String = M_Details.LinkAjaxRequest & "MenuRequest=11&" & postData

                ' Send GET request to save button properties
                Try
                    Using client As New System.Net.WebClient()
                        Dim response As String = client.DownloadString(phpUrl)
                        ' Show success message to user
                        MessageBox.Show("Default button settings saved for CateId: " & cateId, "Button Settings", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Using
                Catch ex As Exception
                    ' Show error message to user
                    MessageBox.Show("Error saving default button settings: " & ex.Message, "Button Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

        Catch ex As Exception
            ' Show error message to user
            MessageBox.Show("Error in SaveDefaultButtonSettings: " & ex.Message, "Button Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Helper method to create POST data for button properties
    Private Function CreateButtonPostData(props As ButtonPropertiesData) As String
        Try
            Dim postData As String = ""
            postData &= "operation=SAVE"
            postData &= "&item_id=" & Uri.EscapeDataString(props.item_id.ToString())
            postData &= "&menu_type=" & Uri.EscapeDataString(props.menu_type)
            postData &= "&font_size=" & Uri.EscapeDataString(props.font_size.ToString())
            postData &= "&font_name=" & Uri.EscapeDataString(props.font_name)
            postData &= "&font_style=" & Uri.EscapeDataString(props.font_style)
            postData &= "&text_color=" & Uri.EscapeDataString(props.text_color)
            postData &= "&back_color=" & Uri.EscapeDataString(props.back_color)
            postData &= "&position=" & Uri.EscapeDataString(props.position.ToString())
            postData &= "&button_width=" & Uri.EscapeDataString(props.button_width.ToString())
            postData &= "&button_height=" & Uri.EscapeDataString(props.button_height.ToString())

            Return postData
        Catch ex As Exception
            Return ""
        End Try
    End Function
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

 