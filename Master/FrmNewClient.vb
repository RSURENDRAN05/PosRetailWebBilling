Imports Newtonsoft.Json
Imports System.Net
Imports Newtonsoft.Json.Linq
Imports System.IO

Public Class FrmNewClient

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        Try
            _clear()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _clear()
        Try
            txtbranchid.Text = ""
            txtbranchname.Text = ""
            txtemail.Text = ""
            txtcontact.Text = ""
            txtaddress.Text = ""
            txtanydesk.Text = ""
            txtnoofserver.Text = ""
            txtnoofclient.Text = ""
            txtnooftab.Text = ""
            txtactivationcode.Text = "-"
            txtmessage.Text = "-"
            txtinstalldate.Text = Date.Now.ToString("dd-MM-yyyy")
            chklock.CheckState = CheckState.Unchecked
            chkactive.CheckState = CheckState.Checked
            btnsave.Text = "Save"
            _DataLoad()
            _CustomerDataLoad()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _DataLoad()
        Try
            getBranchTableMaster()
            If _JsonData.BranchTable.Rows.Count > 0 Then
                GridControl1.DataSource = _JsonData.BranchTable
            Else
                GridControl1.DataSource = Nothing
            End If
        Catch ex As Exception
            GridControl1.DataSource = Nothing
        End Try
    End Sub
    Private Sub btnaddcompany_Click(sender As Object, e As EventArgs) Handles btnaddcompany.Click
        Try
            FrmCreateComp.ShowDialog()
            _CustomerDataLoad()
        Catch ex As Exception

        End Try

    End Sub
    Private Sub _CustomerDataLoad()
        Try
            getCustomerMaster()
            If _JsonData.CustomerTable.Rows.Count > 0 Then
                txtcompany.Properties.DataSource = _JsonData.CustomerTable.Select("Active = 1").CopyToDataTable
            Else
                txtcompany.Properties.DataSource = Nothing
            End If
        Catch ex As Exception
            txtcompany.Properties.DataSource = Nothing
        End Try
    End Sub

    Private Sub FrmNewClient_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            _clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            btnsave.Text = "Update"
            Dim id As Integer = GridView1.GetFocusedRowCellValue("Id")
            Dim _BranchTable As DataTable
            _BranchTable = New DataTable
            _BranchTable.TableName = "_BranchTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=34&id=" & id)
            Dim Userparsejson As JObject = JObject.Parse(json)
            _BranchTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _BranchTable.Rows.Count > 0 Then
                For Each _rows In _BranchTable.Rows
                    txtcompany.Text = _rows("CompanyName")
                    txtbranchid.Text = _rows("Id")
                    txtbranchname.Text = _rows("BranchName")
                    txtemail.Text = _rows("Email")
                    txtcontact.Text = _rows("Contact")
                    txtaddress.Text = _rows("Address")
                    txtanydesk.Text = _rows("AnyDesk")
                    txtnoofserver.Text = _rows("Server")
                    txtnoofclient.Text = _rows("Client")
                    txtnooftab.Text = _rows("Tab")
                    txtactivationcode.Text = _rows("Activation")
                    If _rows("Message") = "" Then
                        txtmessage.Text = "-"
                    Else
                        txtmessage.Text = _rows("Message")
                    End If
                    txtinstalldate.Text = _rows("InstallDate")
                    If _rows("Locks") = 1 Then
                        chklock.CheckState = CheckState.Checked
                    Else
                        chklock.CheckState = CheckState.Unchecked
                    End If
                    If _rows("Active") = 1 Then
                        chkactive.CheckState = CheckState.Checked
                    Else
                        chkactive.CheckState = CheckState.Unchecked
                    End If
                Next
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            If txtcompany.Text = "" OrElse txtcompany.EditValue Is Nothing Then
                txtcompany.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtcompany.Properties.Appearance.BackColor = Color.White
            End If
            If txtbranchname.Text = "" OrElse txtbranchname.EditValue Is Nothing Then
                txtbranchname.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtbranchname.Properties.Appearance.BackColor = Color.White
            End If
            If txtemail.Text = "" OrElse txtemail.EditValue Is Nothing Then
                txtemail.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtemail.Properties.Appearance.BackColor = Color.White
            End If
            If txtcontact.Text = "" OrElse txtcontact.EditValue Is Nothing Then
                txtcontact.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtcontact.Properties.Appearance.BackColor = Color.White
            End If
            If txtaddress.Text = "" OrElse txtaddress.EditValue Is Nothing Then
                txtaddress.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtaddress.Properties.Appearance.BackColor = Color.White
            End If
            If txtanydesk.Text = "" OrElse txtanydesk.EditValue Is Nothing Then
                txtanydesk.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtanydesk.Properties.Appearance.BackColor = Color.White
            End If
            If txtnoofserver.Text = "" OrElse txtnoofserver.EditValue Is Nothing Then
                txtnoofserver.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtnoofserver.Properties.Appearance.BackColor = Color.White
            End If
            If txtcompany.Text = "" OrElse txtcompany.EditValue Is Nothing Then
                txtcompany.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtcompany.Properties.Appearance.BackColor = Color.White
            End If
            If txtnoofclient.Text = "" OrElse txtnoofclient.EditValue Is Nothing Then
                txtnoofclient.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtnoofclient.Properties.Appearance.BackColor = Color.White
            End If
            If txtnooftab.Text = "" OrElse txtnoofclient.EditValue Is Nothing Then
                txtnooftab.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtnooftab.Properties.Appearance.BackColor = Color.White
            End If
            If txtactivationcode.Text = "" OrElse txtnoofclient.EditValue Is Nothing Then
                txtactivationcode.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtactivationcode.Properties.Appearance.BackColor = Color.White
            End If
            If txtmessage.Text = "" OrElse txtnoofclient.EditValue Is Nothing Then
                txtmessage.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtmessage.Properties.Appearance.BackColor = Color.White
            End If
            Dim comp As New clsbranch
            If btnsave.Text = "Save" Then
                dialog.Caption = "Connecting To Server"
                comp.branchid = 0
                comp.branchcustomerid = txtcompany.EditValue
                comp.branchname = txtbranchname.Text
                comp.branchemail = txtemail.Text
                comp.branchcontact = txtcontact.Text
                comp.branchaddress = txtaddress.Text
                comp.branchanydesk = txtanydesk.Text
                comp.branchserver = txtnoofserver.Text
                comp.branchclient = txtnoofclient.Text
                comp.branchtab = txtnooftab.Text
                comp.branchWarrningmsg = txtmessage.Text
                comp.branchactivationcode = txtactivationcode.Text
                Dim Coldate As String = ""
                _DateConversion(txtinstalldate.Text, Coldate)
                comp.branchinstalldate = Coldate
                comp.branchlock = chklock.CheckState
                comp.branchstatus = chkactive.CheckState
                Dim PostString As String = JsonConvert.SerializeObject(comp)
                If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=31&json=" & PostString) = True Then
                    dialog.Caption = "Data Saved Success.."
                    _clear()
                Else
                    dialog.Caption = "Data not Saved.."
                End If
                btnsave.Text = "Save"
            Else
                dialog.Caption = "Connecting To Server"
                comp.branchid = txtbranchid.Text
                comp.branchcustomerid = txtcompany.EditValue
                comp.branchname = txtbranchname.Text
                comp.branchemail = txtemail.Text
                comp.branchcontact = txtcontact.Text
                comp.branchaddress = txtaddress.Text
                comp.branchanydesk = txtanydesk.Text
                comp.branchserver = txtnoofserver.Text
                comp.branchclient = txtnoofclient.Text
                comp.branchtab = txtnooftab.Text
                comp.branchWarrningmsg = txtmessage.Text
                comp.branchactivationcode = txtactivationcode.Text
                Dim Coldate As String = ""
                _DateConversion(txtinstalldate.Text, Coldate)
                comp.branchinstalldate = Coldate.ToString
                comp.branchlock = chklock.CheckState
                comp.branchstatus = chkactive.CheckState
                Dim PostString As String = JsonConvert.SerializeObject(comp)
                If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=32&json=" & PostString) = True Then
                    dialog.Caption = "Updated Saved Success.."
                    _clear()
                End If
                btnsave.Text = "Save"
            End If
        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub

    Private Sub btncreatefloder_Click(sender As Object, e As EventArgs) Handles btncreatefloder.Click
        Try
            If MakeCloudDirectory(txtbranchid.Text) Then
                MsgBox("Cloud folder created successfully!")
            Else
                MsgBox("Floder Available")
            End If
        Catch ex As Exception

        End Try
    End Sub



   Public Function MakeCloudDirectory(mkdirValue As String) As Boolean
        Try
            Dim url As String = "http://myposqr.com/sam/on/sales/mkdir.php?mkdir=" & mkdirValue
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "GET"
            request.Timeout = 10000

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    Dim json As String = reader.ReadToEnd()

                    ' Deserialize JSON to Dictionary
                    Dim result As Dictionary(Of String, String) = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(json)

                    If result.ContainsKey("type") AndAlso result("type") = "1" Then
                        Return True
                    Else
                        'MsgBox(result("message"))
                        Return False
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
            Return False
        End Try
    End Function
End Class
Public Class clsbranch
    Public Property branchid As String
    Public Property branchcustomerid As String
    Public Property branchname As String
    Public Property branchemail As String
    Public Property branchcontact As String
    Public Property branchaddress As String
    Public Property branchanydesk As String
    Public Property branchserver As String
    Public Property branchclient As String
    Public Property branchtab As String
    Public Property branchlock As String
    Public Property branchactivationcode As String
    Public Property branchWarrningmsg As String
    Public Property branchstatus As String
    Public Property branchinstalldate As String
End Class