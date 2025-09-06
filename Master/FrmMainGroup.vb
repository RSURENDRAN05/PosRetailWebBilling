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

                        ' Save default button settings after successful update
                        SaveDefaultButtonSettings()
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

    ' Save default button settings for the newly created/updated main group
    Private Sub SaveDefaultButtonSettings()
        Try
            ' Get the MainId (either from txtmainid for updates or get the latest for new records)
            Dim mainId As Integer = 0

            If btnsave.Text = "Update" AndAlso Not String.IsNullOrEmpty(txtmainid.Text) Then
                ' For updates, use the existing MainId
                mainId = Convert.ToInt32(txtmainid.Text)
            Else
                ' For new records, get the latest MainId from the data
                If _JsonData.MainGroupTable IsNot Nothing AndAlso _JsonData.MainGroupTable.Rows.Count > 0 Then
                    ' Get the maximum MainId (assuming it's the newly inserted one)
                    mainId = _JsonData.MainGroupTable.AsEnumerable().Max(Function(row) Convert.ToInt32(row("MainId")))
                End If
            End If

            If mainId > 0 Then
                ' Create default button properties for Main menu type
                Dim buttonProperties As New ButtonPropertiesData()
                buttonProperties.item_id = mainId
                buttonProperties.menu_type = "Main"
                buttonProperties.font_size = 10.0
                buttonProperties.font_name = "Segoe UI"
                buttonProperties.font_style = "Bold"
                buttonProperties.text_color = String.Format("Argb({0},{1},{2},{3})", 255, 255, 255, 255) ' White text
                buttonProperties.back_color = String.Format("Argb({0},{1},{2},{3})", 255, 72, 61, 139) ' DarkSlateBlue background
                buttonProperties.position = 0
                buttonProperties.button_width = 120
                buttonProperties.button_height = 60

                ' Send to PHP API
                Dim postData As String = CreateButtonPostData(buttonProperties)
                Dim phpUrl As String = M_Details.LinkAjaxRequest & "MenuRequest=11&" & postData

                ' Send GET request to save button properties
                Try
                    Using client As New System.Net.WebClient()
                        Dim response As String = client.DownloadString(phpUrl)
                        ' Show success message to user
                        MessageBox.Show("Default button settings saved for MainId: " & mainId, "Button Settings", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
Public Class clsmainmaster
    Public Property id As String
    Public Property mainname As String
    Public Property active As String
    Public Property groupcolor As String
End Class
