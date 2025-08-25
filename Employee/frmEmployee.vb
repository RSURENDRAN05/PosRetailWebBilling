Imports Newtonsoft.Json
Imports System.IO
Imports System.Net
Imports Newtonsoft.Json.Linq
Imports DevExpress.XtraEditors
Imports System.Text
Imports System.Collections.Specialized

Public Class frmEmployee
    Dim _DsEmp As New DataTable
    Dim _rtData As New DataTable
    Dim _emp_oldsalary As String = ""
    Private Sub frmEmployee_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            endisform(False)
            LoadData()
            _rtData.Columns.Add("Link", GetType(String))
            _rtData.Columns.Add("FileName", GetType(String))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnexit_Click(sender As Object, e As EventArgs) Handles btnexit.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Function ValidationProcess() As Boolean
        Try
            If txtfirstname.EditValue Is Nothing OrElse txtfirstname.Text = "" Then
                MessageBox.Show("FirstName Missing", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf txtlastname.EditValue Is Nothing OrElse txtlastname.Text = "" Then
                MessageBox.Show("LastName Missing", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf txtprintname.EditValue Is Nothing OrElse txtprintname.Text = "" Then
                MessageBox.Show("PrintName Missing", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf txtpassport.EditValue Is Nothing OrElse txtpassport.Text = "" Then
                MessageBox.Show("PassportNo/NRIC Missing", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf txtpassportexpire.EditValue Is Nothing OrElse txtpassportexpire.Text = "" Then
                MessageBox.Show("Passport Date Missing", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf txtvisaexpire.EditValue Is Nothing OrElse txtvisaexpire.Text = "" Then
                MessageBox.Show("Visa Expiry Date Missing", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf txtphoneno.EditValue Is Nothing OrElse txtphoneno.Text = "" Then
                MessageBox.Show("PhoneNo Missing", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf txtcontactname.EditValue Is Nothing OrElse txtcontactname.Text = "" Then
                MessageBox.Show("ContactName Missing", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf txtemergencyno.EditValue Is Nothing OrElse txtemergencyno.Text = "" Then
                MessageBox.Show("EmergencyNo Missing", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf txtaccountname.EditValue Is Nothing OrElse txtaccountname.Text = "" Then
                MessageBox.Show("AccountName Missing", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf txtbankname.EditValue Is Nothing OrElse txtbankname.Text = "" Then
                MessageBox.Show("BankName Missing", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf txtaccountno.EditValue Is Nothing OrElse txtaccountno.Text = "" Then
                MessageBox.Show("AccountNo Missing", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf txtcompany.EditValue Is Nothing OrElse txtcompany.Text = "" Then
                MessageBox.Show("CompanyName Missing", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf txtlocation.EditValue Is Nothing OrElse txtlocation.Text = "" Then
                MessageBox.Show("LocationName Missing", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf txtepf.EditValue Is Nothing OrElse txtepf.Text = "" Then
                txtepf.Text = "0.00"
                MessageBox.Show("Epf Missing", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf txtsocso.EditValue Is Nothing OrElse txtsocso.Text = "" Then
                txtsocso.Text = "0.00"
                MessageBox.Show("Socso Missing", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub btnclear_Click(sender As Object, e As EventArgs) Handles btnclear.Click
        Try
            clear()
            endisform(False)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadData()
        Try
            If getComapnyInfo() = True Then
                If _JsonData.CompanyTable.Rows.Count > 0 Then
                    txtcompany.Properties.DataSource = _JsonData.CompanyTable
                End If
            End If
            If getLocationInfo() = True Then
                If _JsonData.LocationTable.Rows.Count > 0 Then
                    txtlocation.Properties.DataSource = _JsonData.LocationTable
                End If
            End If
            loadEmployee()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub clear()
        Try
            btnsave.Text = "Save"
            chkactive.Checked = True
            txtid.Text = ""
            txtfirstname.Text = ""
            txtlastname.Text = ""
            txtprintname.Text = ""

            cmbidtype.SelectedIndex = 0
            txtnationality.SelectedIndex = 0
            txtpassport.Text = ""
            txtdob.Text = Date.Now.ToString("dd-MM-yyyy")
            txtpassportexpire.Text = Date.Now.ToString("dd-MM-yyyy")
            txtvisaexpire.Text = Date.Now.ToString("dd-MM-yyyy")
            txtjoindate.Text = Date.Now.ToString("dd-MM-yyyy")

            txtcurrentpermit.Text = "-"
            txtnextpermit.Text = "-"
            txtmonthexpire.Text = "-"

            txtphoneno.Text = "-"
            txtcontactname.Text = "-"
            txtemergencyno.Text = "-"
            txtaccountname.Text = "-"
            txtbankname.Text = "-"
            txtaccountno.Text = "0"
            txtremarks.Text = "-"

            txtdesignation.SelectedIndex = 0
            txtcompany.EditValue = 1
            txtlocation.EditValue = 1
            txtcurrentstatus.SelectedIndex = 0

            txtbasicsalary.EditValue = 0.0
            txtperday.EditValue = 0.0
            txtotperdayrate.EditValue = 0.0
            txtotperhrsrate.EditValue = 0.0
            txtallowance.EditValue = 0.0
            txtepf.Text = "00.0"
            txtsocso.Text = "00.0"
            _emp_oldsalary = ""
            btnBrowse.Text = ""
            _rtData.Rows.Clear()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub endisform(ByRef vt As Boolean)
        Try
            chkactive.Enabled = vt
            txtid.Enabled = vt
            txtfirstname.Enabled = vt
            txtlastname.Enabled = vt
            txtprintname.Enabled = vt

            cmbidtype.Enabled = vt
            txtnationality.Enabled = vt
            txtpassport.Enabled = vt

            txtpassportexpire.Enabled = vt
            txtvisaexpire.Enabled = vt
            txtjoindate.Enabled = vt

            txtphoneno.Enabled = vt
            txtcontactname.Enabled = vt
            txtemergencyno.Enabled = vt
            txtaccountname.Enabled = vt
            txtbankname.Enabled = vt
            txtaccountno.Enabled = vt
            txtremarks.Enabled = vt
            txtdesignation.Enabled = vt
            txtcompany.Enabled = vt
            txtlocation.Enabled = vt
            txtcurrentstatus.Enabled = vt

            txtbasicsalary.Enabled = vt
            txtperday.Enabled = vt
            txtotperdayrate.Enabled = vt
            txtotperhrsrate.Enabled = vt
            txtallowance.Enabled = vt
            txtepf.Enabled = vt
            txtsocso.Enabled = vt
            btnBrowse.Enabled = vt
            btnuploadphoto.Enabled = vt
            txtdob.Enabled = vt
            txtmonthexpire.Enabled = vt
            txtcurrentpermit.Enabled = vt
            txtnextpermit.Enabled = vt
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnaddnew_Click(sender As Object, e As EventArgs) Handles btnaddnew.Click
        Try
            endisform(True)
            clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            Dim emp As New employeeinfo
            If Not _emp_oldsalary = txtbasicsalary.Text Then
                If txtbasicsalary.Text <> "0.00" Then
                    dialog.Close()
                    Dim dialogResulst = XtraMessageBox.Show("Do You Want To Update Old Salary : " & _emp_oldsalary & vbNewLine & " New Salary : " & txtbasicsalary.Text, "Salary Update Msg", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If dialogResulst = Windows.Forms.DialogResult.Yes Then
                        If _SaveEmployeeNewSalary() = True Then
                            XtraMessageBox.Show("Salary Updated Success", "Msg", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            XtraMessageBox.Show("Salary Not Updated", "Msg", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If
            End If
            If btnsave.Text = "Save" Then
                If ValidationProcess() = True Then
                    dialog.Caption = "Data Is Upload.."
                    emp.emp_id = 0
                    emp.emp_firstname = txtfirstname.Text
                    emp.emp_lastname = txtlastname.Text
                    emp.emp_printname = txtprintname.Text
                    emp.emp_idtype = cmbidtype.Text
                    emp.emp_passportic = txtpassport.Text
                    emp.emp_nationality = txtnationality.Text
                    Dim _date1 As String = ""
                    _DateConversion(txtpassportexpire.EditValue, _date1)
                    emp.emp_passexpire = _date1
                    Dim _date2 As String = ""
                    _DateConversion(txtvisaexpire.EditValue, _date2)
                    emp.emp_visaexpire = _date2
                    Dim _date3 As String = ""
                    _DateConversion(txtjoindate.EditValue, _date3)
                    emp.emp_joindate = _date3
                    Dim _date4 As String = ""
                    If txtresigndate.Text.Length > 0 Then
                        _DateConversion(txtresigndate.EditValue, _date4)
                        emp.emp_resigndate = _date4
                    Else
                        emp.emp_resigndate = "1111-11-11"
                    End If
                    emp.emp_contactno = txtphoneno.Text
                    emp.emp_contactname = txtcontactname.Text
                    emp.emp_emergencyno = txtemergencyno.Text
                    emp.emp_compid = txtcompany.EditValue
                    emp.emp_locid = txtlocation.EditValue
                    emp.emp_designation = txtdesignation.Text
                    emp.emp_bankname = txtbankname.Text
                    emp.emp_accountname = txtaccountname.Text
                    emp.emp_accountno = txtaccountno.Text
                    Dim amt As Double = 0.0
                    _RemoveCommaFormat(txtbasicsalary.Text, amt)
                    emp.emp_basicsalary = Format(amt, "###0.00")
                    emp.emp_basicrate = txtperday.Text
                    emp.emp_otrate = txtotperdayrate.Text
                    emp.emp_othrsrate = txtotperhrsrate.Text
                    emp.emp_allowance = txtallowance.Text
                    emp.emp_epf = txtepf.Text
                    emp.emp_socso = txtsocso.Text
                    emp.emp_currentstatus = txtcurrentstatus.Text
                    emp.emp_remarks = txtremarks.Text
                    emp.emp_active = chkactive.CheckState
                    Dim _date5 As String = ""
                    _DateConversion(txtdob.EditValue, _date5)
                    emp.emp_dob = _date5
                    emp.emp_monthexpire = txtmonthexpire.Text
                    emp.emp_curpermit = txtcurrentpermit.Text
                    emp.emp_nextpermit = txtnextpermit.Text
                    'Dim base64String = ConvertImageToBase64String()
                    emp.emp_image = "X"
                    Dim PostString As String = JsonConvert.SerializeObject(emp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "EmployeeReq=1&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        endisform(False)
                        clear()
                        loadEmployee()
                    Else
                        dialog.Caption = "Data Not Saved"
                    End If
                End If
            ElseIf btnsave.Text = "Update" Then
                If ValidationProcess() = True Then
                    dialog.Caption = "Data Is Upload.."
                    emp.emp_id = txtid.Text
                    emp.emp_firstname = txtfirstname.Text
                    emp.emp_lastname = txtlastname.Text
                    emp.emp_printname = txtprintname.Text
                    emp.emp_idtype = cmbidtype.Text
                    emp.emp_passportic = txtpassport.Text
                    emp.emp_nationality = txtnationality.Text
                    Dim _date1 As String = ""
                    _DateConversion(txtpassportexpire.EditValue, _date1)
                    emp.emp_passexpire = _date1
                    Dim _date2 As String = ""
                    _DateConversion(txtvisaexpire.EditValue, _date2)
                    emp.emp_visaexpire = _date2
                    Dim _date3 As String = ""
                    _DateConversion(txtjoindate.EditValue, _date3)
                    emp.emp_joindate = _date3
                    Dim _date4 As String = ""
                    If txtresigndate.Text.Length > 0 Then
                        _DateConversion(txtresigndate.EditValue, _date4)
                        emp.emp_resigndate = _date4
                    Else
                        emp.emp_resigndate = "1111-11-11"
                    End If
                    emp.emp_contactno = txtphoneno.Text
                    emp.emp_contactname = txtcontactname.Text
                    emp.emp_emergencyno = txtemergencyno.Text
                    emp.emp_compid = txtcompany.EditValue
                    emp.emp_locid = txtlocation.EditValue
                    emp.emp_designation = txtdesignation.Text
                    emp.emp_bankname = txtbankname.Text
                    emp.emp_accountname = txtaccountname.Text
                    emp.emp_accountno = txtaccountno.Text
                    Dim amt As Double = 0.0
                    _RemoveCommaFormat(txtbasicsalary.Text, amt)
                    emp.emp_basicsalary = Format(amt, "###0.00")
                    emp.emp_basicrate = txtperday.Text
                    emp.emp_otrate = txtotperdayrate.Text
                    emp.emp_othrsrate = txtotperhrsrate.Text
                    emp.emp_allowance = txtallowance.Text
                    emp.emp_epf = txtepf.Text
                    emp.emp_socso = txtsocso.Text
                    emp.emp_currentstatus = txtcurrentstatus.Text
                    emp.emp_remarks = txtremarks.Text
                    emp.emp_active = chkactive.CheckState
                    'Dim base64String = ConvertImageToBase64String()
                    Dim _date5 As String = ""
                    _DateConversion(txtdob.EditValue, _date5)
                    emp.emp_dob = _date5
                    emp.emp_monthexpire = txtmonthexpire.Text
                    emp.emp_curpermit = txtcurrentpermit.Text
                    emp.emp_nextpermit = txtnextpermit.Text
                    emp.emp_image = "X"
                    Dim PostString As String = JsonConvert.SerializeObject(emp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "EmployeeReq=2&json=" & PostString) = True Then
                        btnsave.Text = "Save"
                        dialog.Caption = "Data Saved Success.."
                        endisform(False)
                        clear()
                        loadEmployee()
                    Else
                        dialog.Caption = "Data Not Saved"
                    End If
                End If

            End If
        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub
    Public Function _SaveEmployeeNewSalary() As Boolean
        Try
            Dim empnewsalary As New employeeSalaryHistory
            empnewsalary.pes_empid = txtid.Text
            empnewsalary.pes_oldsalary = _emp_oldsalary
            empnewsalary.pes_newsalary = txtbasicsalary.Text
            empnewsalary.pes_remarks = txtremarks.Text
            empnewsalary.pes_userid = _companyInfo.UserId
            Dim PostString As String = JsonConvert.SerializeObject(empnewsalary)
            If _JsonSend(M_Details.LinkAjaxRequest & "EmployeeReq=10&json=" & PostString) = True Then

                Return True
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    'Public Function ConvertImageToBase64String() As String
    '    Using ms As New MemoryStream()
    '        txtpicture.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png) 'We load the image from first PictureBox in the MemoryStream
    '        Dim obyte = ms.ToArray() 'We tranform it to byte array..

    '        Return Convert.ToBase64String(obyte) 'We then convert the byte array to base 64 string.
    '    End Using
    'End Function

    'Public Function ConvertBase64ToByteArray(base64 As String) As Byte()
    '    Return Convert.FromBase64String(base64) 'Convert the base64 back to byte array.
    'End Function

    ''Here's the part of your code (which works)
    'Private Function convertbytetoimage(ByVal BA As Byte())
    '    Dim ms As MemoryStream = New MemoryStream(BA)
    '    Dim image = System.Drawing.Image.FromStream(ms)
    '    Return image
    'End Function

    'Private Sub txtpicture_EditValueChanged(sender As Object, e As EventArgs)
    '    Dim base64String = ConvertImageToBase64String() 'Using Functions To Make the code tidier
    '    Dim byteArray = ConvertBase64ToByteArray(base64String) 'Using Functions To Make the code tidier
    '    Dim image = convertbytetoimage(byteArray) 'Using Functions To Make the code tidier
    '    txtpicture.Image = image 'since we're using a small windows form app, we'll set back the image to a second picture box.
    'End Sub
    'Public Function ImageToStream() As Byte()
    '    Dim stream As New MemoryStream()
    '    Try
    '        txtpicture.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png)
    '        Dim content As Byte() = stream.ToArray
    '        txtpicture.EditValue = content
    '    Catch ex As Exception
    '        ' alMsg.altmsg.Show(Me, "Product Master Ver 22.01", "ImageToStream", ex.Message, alMsg.img.Images(1))
    '    End Try

    '    Return stream.ToArray()
    'End Function
    Private Sub txtbasicsalary_EditValueChanged(sender As Object, e As EventArgs) Handles txtbasicsalary.EditValueChanged
        Try
            txtperday.EditValue = txtbasicsalary.EditValue / 30
            txtotperdayrate.EditValue = txtperday.EditValue
            txtotperhrsrate.EditValue = txtperday.EditValue / 12
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub btnfindid_Click(sender As Object, e As EventArgs) Handles btnfindid.Click
    '    Try
    '        If frmEmployeeView.ShowDialog() = Windows.Forms.DialogResult.OK Then

    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub loadEmployee()
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            dialog.Caption = "Get Data From Server.."
            If getEmployeeView() = False Then
                dialog.Caption = "Connection Error.."
            Else
                dialog.Caption = "Data Received.."
                If _DsEmp.Rows.Count > 0 Then
                    GridControl1.DataSource = _DsEmp.DefaultView
                Else
                    GridControl1.DataSource = Nothing
                End If
            End If
        Catch ex As Exception
            GridControl1.DataSource = Nothing
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub


    Private Sub GridControl1_DoubleClick(sender As Object, e As EventArgs) Handles GridControl1.DoubleClick
        Try
            E_EmployeeId = GridView1.GetFocusedRowCellValue("Id")
            If getEmployeeViewByID(E_EmployeeId) = True Then
                btnsave.Text = "Update"
                endisform(True)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function getEmployeeView() As Boolean
        Try
            _DsEmp = New DataTable
            _DsEmp.TableName = "EmployeeView"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "EmployeeReq=3&json=1")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _DsEmp = Userparsejson("Data").ToObject(Of DataTable)()
            If _DsEmp.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getEmployeeViewByID(ByRef Id As String) As Boolean
        Try
            _DsEmp = New DataTable
            _DsEmp.TableName = "EmployeeView"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "EmployeeReq=4&json=" & Id)
            Dim Userparsejson As JObject = JObject.Parse(json)
            Dim jsonString As String = Userparsejson("Data").ToString()
            'Dim employeeList As List(Of employeeinfo) = JsonConvert.DeserializeObject(Of List(Of employeeinfo))(jsonString)
            'If employeeList.Count > 0 Then
            '    Dim img = employeeList(0).emp_image
            '    Dim str = img.ToString.Replace(" ", "")
            '    Dim byteArray = ConvertBase64ToByteArray(img) 'Using Functions To Make the code tidier
            '    Dim image = convertbytetoimage(byteArray) 'Using Functions To Make the code tidier
            '    txtpicture.Image = image
            'End If
            _DsEmp = Userparsejson("Data").ToObject(Of DataTable)()
            If _DsEmp.Rows.Count > 0 Then

                txtid.Text = _DsEmp.Rows(0)("emp_id").ToString
                txtfirstname.Text = _DsEmp.Rows(0)("emp_firstname").ToString
                txtlastname.Text = _DsEmp.Rows(0)("emp_lastname").ToString
                txtprintname.Text = _DsEmp.Rows(0)("emp_printname").ToString

                cmbidtype.Text = _DsEmp.Rows(0)("emp_idtype").ToString
                txtnationality.Text = _DsEmp.Rows(0)("emp_nationality").ToString
                txtpassport.Text = _DsEmp.Rows(0)("emp_passportic").ToString

                txtpassportexpire.Text = _DsEmp.Rows(0)("emp_passexpire").ToString
                txtvisaexpire.Text = _DsEmp.Rows(0)("emp_visaexpire").ToString
                txtjoindate.Text = _DsEmp.Rows(0)("emp_joindate").ToString
                txtresigndate.Text = _DsEmp.Rows(0)("emp_resigndate").ToString

                txtphoneno.Text = _DsEmp.Rows(0)("emp_contactno").ToString
                txtcontactname.Text = _DsEmp.Rows(0)("emp_contactname").ToString
                txtemergencyno.Text = _DsEmp.Rows(0)("emp_emergencyno").ToString
                txtaccountname.Text = _DsEmp.Rows(0)("emp_accountname").ToString
                txtbankname.Text = _DsEmp.Rows(0)("emp_bankname").ToString
                txtaccountno.Text = _DsEmp.Rows(0)("emp_accountno").ToString
                txtremarks.Text = _DsEmp.Rows(0)("emp_remarks").ToString

                txtdesignation.Text = _DsEmp.Rows(0)("emp_designation").ToString
                txtcompany.EditValue = _DsEmp.Rows(0)("emp_compid").ToString
                txtlocation.EditValue = _DsEmp.Rows(0)("emp_locid").ToString
                txtcurrentstatus.Text = _DsEmp.Rows(0)("emp_currentstatus").ToString
                'Dim str = _DsEmp.Rows(0)("emp_image").ToString.Replace(" ", "")
                'Dim byteArray = ConvertBase64ToByteArray(str) 'Using Functions To Make the code tidier
                'Dim image = convertbytetoimage(byteArray) 'Using Functions To Make the code tidier
                'txtpicture.Image = image 'since we're using a small windows form app, we'll set back the image to a second picture box.
                txtbasicsalary.EditValue = _DsEmp.Rows(0)("emp_basicsalary").ToString
                txtperday.EditValue = _DsEmp.Rows(0)("emp_basicrate").ToString
                txtotperdayrate.EditValue = _DsEmp.Rows(0)("emp_otrate").ToString
                txtotperhrsrate.EditValue = _DsEmp.Rows(0)("emp_othrsrate").ToString
                txtallowance.EditValue = _DsEmp.Rows(0)("emp_allowance").ToString
                txtsocso.EditValue = _DsEmp.Rows(0)("emp_socso").ToString
                txtepf.EditValue = _DsEmp.Rows(0)("emp_epf").ToString
                chkactive.Checked = _DsEmp.Rows(0)("emp_active").ToString
                txtdob.Text = _DsEmp.Rows(0)("emp_dob").ToString
                txtmonthexpire.Text = _DsEmp.Rows(0)("emp_monthexpire").ToString
                txtcurrentpermit.Text = _DsEmp.Rows(0)("emp_curpermit").ToString
                txtnextpermit.Text = _DsEmp.Rows(0)("emp_nextpermit").ToString
                _emp_oldsalary = txtbasicsalary.EditValue
                ReloadFiles()
                Return True
            End If
            Return True
        Catch ex As Exception
            GridControlDownLoad.DataSource = Nothing
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Private Function UnicodeStringToBytes(ByVal str As String) As Byte()
        Return System.Text.Encoding.Unicode.GetBytes(str)
    End Function
    Private Sub btnuploadphoto_Click(sender As Object, e As EventArgs) Handles btnuploadphoto.Click
        Dim filePath As String = btnBrowse.Text ' Path to your local file
        Dim serverUrl As String = M_Details.LinkAjaxRequest & "EmployeeReq=6" ' Replace with your server script URL
        Dim errmsg As String = ""
        Try
            If String.IsNullOrEmpty(filePath) OrElse String.IsNullOrEmpty(txtid.Text) Then
                XtraMessageBox.Show("Please Select Employee Id Or Select File", "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Try
            Else
                If UploadFile(filePath, serverUrl, txtid.Text, errmsg) = True Then
                    btnBrowse.Text = ""
                    ReloadFiles()
                    XtraMessageBox.Show(errmsg, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    XtraMessageBox.Show(errmsg, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try
         
    End Sub

    Private Sub btnsalaryhistory_Click(sender As Object, e As EventArgs) Handles btnsalaryhistory.Click
        Try
            If Not String.IsNullOrEmpty(txtid.Text) Then
                frmEmployeeSalaryHis.ShowDialog(txtid.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Dim OpFile As New OpenFileDialog
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Try
            If _FileLoad() = True Then

            End If
        Catch ex As Exception

        End Try
    End Sub
    Function _FileLoad() As Boolean
        Try
            OpFile.InitialDirectory = "c:\"
            OpFile.Filter = "All Files (*.*)|*.*"
            If OpFile.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                Dim _con As String = String.Empty
                Dim fi As New IO.FileInfo(OpFile.FileName)
                Dim fileName As String = OpFile.FileName
                Dim FileNameTemp As String = Path.GetFileName(fileName)
                _btnBrowse.Text = fileName
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error On Loading")
            Return False
        End Try
    End Function
    Private Sub DownFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DownFileToolStripMenuItem.Click
        Try
            Dim link As String = GridViewDownLoad.GetFocusedRowCellValue("Link")
            Dim FileName As String = GridViewDownLoad.GetFocusedRowCellValue("FileName")
            If Not System.IO.Directory.Exists(M_Details._appPath & "\Downloads\" & txtid.Text) Then
                System.IO.Directory.CreateDirectory(M_Details._appPath & "\Downloads\" & txtid.Text)
            End If
            If DownloadFiles(FileName, link) = True Then
                MessageBox.Show("Download Path" & M_Details._appPath & "\Downloads\" & txtid.Text, "Download", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("No File", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DeleteFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteFileToolStripMenuItem.Click
        Try
            Dim FileName As String = GridViewDownLoad.GetFocusedRowCellValue("FileName")
            Dim path As String = ""
            Dim errMsg As String = ""
            path = txtid.Text & "/" & FileName
            If _JsonSend(M_Details.LinkAjaxRequest & "&EmployeeReq=8&path=" & path, errMsg) = True Then
                ReloadFiles()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ReloadFiles()
        Try
            Dim Tab As New DataTable
            Tab = GetFiles(txtid.Text)
            If Tab.Rows.Count > 0 Then
                GridControlDownLoad.DataSource = Tab
                popupcondownloadlist.Show()
            Else
                GridControlDownLoad.DataSource = Nothing
                popupcondownloadlist.Hide()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Function GetFiles(EmpId As String) As DataTable
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "EmployeeReq=7&EmployeeId=" & EmpId)
            Dim Userparsejson As JObject = JObject.Parse(json)
            Dim Msg = Userparsejson("Success").ToString
            If Msg.ToString = "True" Then
                Dim dataArray As JArray = Userparsejson("Data")
                If dataArray IsNot Nothing Then
                    Dim values As String() = dataArray.ToObject(Of String())()
                    Dim link = "https://myposqr.com/sam/employee"
                    ' Add columns to DataTable based on extracted values (modify as needed)
                   
                    _rtData.Rows.Clear()
                    ' Add rows to DataTable
                    For Each value In values
                        Dim filenameParts = value.Split("/")  ' Split by "/"
                        Dim st1 As String = link & filenameParts(0) & "/" & EmpId & "/" & filenameParts(2)
                        Dim st2 As String = filenameParts(2)
                        _rtData.BeginInit()
                        _rtData.Rows.Add(st1, st2)  ' Extract filename and extension
                        _rtData.EndInit()
                        _rtData.AcceptChanges()
                    Next
                Else
                    Console.WriteLine("Data key not found or not an array")
                End If
            End If
            Return _rtData
        Catch ex As Exception
            Return Nothing
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Function
    Function DownloadFiles(strFileName As String, strURL As String) As Boolean
        Dim HttpReq As HttpWebRequest = DirectCast(WebRequest.Create(strURL), HttpWebRequest)
        Try
            Using HttpResponse As HttpWebResponse = DirectCast(HttpReq.GetResponse(), HttpWebResponse)
                Using Reader As New BinaryReader(HttpResponse.GetResponseStream())
                    Dim RdByte As Byte() = Reader.ReadBytes(1 * 1024 * 1024 * 10)
                    Using FStream As New FileStream(M_Details._appPath & "\Downloads\" & txtid.Text & "\" & strFileName, FileMode.Create)
                        FStream.Write(RdByte, 0, RdByte.Length)
                        Return True
                    End Using
                End Using
            End Using
            Return True
        Catch ex As Exception
            Return False

        End Try
    End Function
    Public Function Json(ByVal url As String, ByVal method As String, ByVal data As String)
        Try

            Dim request As System.Net.WebRequest = System.Net.WebRequest.Create(url)
            request.Method = method
            Dim postData = data
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
            request.ContentType = "application/json"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            Dim response As WebResponse = request.GetResponse()
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            'Dim responseFromServer As String = reader.ReadToEnd()
            Dim Userparsejson As JObject = JObject.Parse(reader.ReadToEnd())
            reader.Close()
            dataStream.Close()
            response.Close()
            Dim MsgResultMsg = Userparsejson("Data")
            Dim dtresults = Userparsejson("Success")
            If dtresults.ToString = "True" Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Dim error1 As String = ErrorToString()
            If error1 = "Invalid URI: The format of the URI could not be determined." Then
                MsgBox("ERROR! Must have HTTP:// before the URL.")
            Else
                MsgBox(error1)
            End If
            Return ("ERROR")
        End Try
    End Function
    Dim jsonPost As New JsonPost("http://192.168.1.17:8888")
    Dim dictData As New Dictionary(Of String, Object)

    Private Sub btnPayment_Click(sender As Object, e As EventArgs)
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            Dim responseData As Object = ""
            dictData.Clear()
            dialog.Caption = "Conntecting Server.."
            dictData.Add("TransactionType", "1")
            dictData.Add("TransactionAmount", "1.00")
            dialog.Caption = "Conntecting Server.."
            If jsonPost.postData(dictData, responseData) = True Then
                dialog.Caption = "Waiting For Aproval.."
                If responseData("ResponseCode").ToString = "00" Then
                    dialog.Caption = "Payment Success"
                Else
                    dialog.Caption = "Payment Declined"
                    Exit Try
                End If
            Else
                dialog.Caption = "Payment Not Success.."
            End If

        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)

        Dim imageUrl As String = "C:\path\to\your\local\image.jpg" ' Path to your local image
        Dim serverUrl As String = "http://yourserver.com/upload.php" ' Replace with your server script URL

        ' Read the image bytes from the local file
        Dim imageBytes As Byte() = System.IO.File.ReadAllBytes(imageUrl)

        ' Prepare the request
        Dim client As New WebClient()
        client.Headers.Add("Content-Type", "application/json")

        ' Prepare the upload data as a JSON object
        Dim uploadData As New Dictionary(Of String, Object)()
        uploadData.Add("image", Convert.ToBase64String(imageBytes)) ' Encode image to Base64 for JSON
        uploadData.Add("filename", "uploaded_image.jpg") ' Name for the uploaded file

        ' Serialize the dictionary to JSON
        Dim postData As String = JsonConvert.SerializeObject(uploadData)

        ' Upload the data as JSON
        Dim responseBytes As Byte() = client.UploadData(serverUrl, "POST", Encoding.UTF8.GetBytes(postData))

        ' Get response as string
        Dim response As String = Encoding.UTF8.GetString(responseBytes)
        Console.WriteLine("Server Response: " & response)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs)

        Dim imageUrl As String = "https://www.example.com/image.jpg" ' Replace with actual URL

        Dim imageBytes As Byte() = DownloadImage(imageUrl)
        Dim image As Image = GetImageFromByteArray(imageBytes)

        If image Is Nothing Then
            MsgBox("Failed to load image.")
        Else
            'PictureBox1.Image = image
        End If
    End Sub
    ' Functions defined earlier...
    Function GetImageFromByteArray(byteArray As Byte()) As Image
        If byteArray Is Nothing Then Return Nothing
        Using memoryStream As New MemoryStream(byteArray)
            Return Image.FromStream(memoryStream)
        End Using
    End Function
    Function DownloadImage(url As String) As Byte()
        Dim client As New WebClient()
        Try
            Dim imageBytes As Byte() = client.DownloadData(url)
            Return imageBytes
        Catch ex As Exception
            ' Handle download errors (optional)
            Console.WriteLine("Error downloading image: " & ex.Message)
            Return Nothing
        End Try
    End Function

   
    Private Sub txtvisaexpire_EditValueChanged(sender As Object, e As EventArgs) Handles txtvisaexpire.EditValueChanged
        Try
            Dim DateMontyear As String = ""
            _DateConversionMonthYear(txtvisaexpire.Text, DateMontyear)
            txtmonthexpire.Text = DateMontyear
        Catch ex As Exception

        End Try
    End Sub
 
    Private Sub BarBtnaddnew_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnaddnew.ItemClick
        Try
            btnaddnew_Click(Nothing, Nothing)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BarBtnclear_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnclear.ItemClick
        Try
            btnclear_Click(Nothing, Nothing)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BarBtnhistory_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnhistory.ItemClick
        Try
            btnsalaryhistory_Click(Nothing, Nothing)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BarBtnsave_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarBtnsave.ItemClick
        Try
            btnsave_Click(Nothing, Nothing)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ExportDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportDataToolStripMenuItem.Click
        Try
            GridControl1.ShowRibbonPrintPreview()
        Catch ex As Exception

        End Try
    End Sub
End Class

    ' Function to convert byte array to image
 
Public Class JsonPost
    Private urlToPost As String = ""
    Public Sub New(ByVal urlToPost As String)
        Me.urlToPost = urlToPost
    End Sub

    Public Function postData(ByVal dictData As Dictionary(Of String, Object), ByRef responseData As Object) As Boolean
        Dim webClient As New WebClient()
        Dim resByte As Byte()
        Dim resString As String
        Dim reqString() As Byte

        Try
            webClient.Headers("content-type") = "application/json"
            reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData, Formatting.Indented))
            resByte = webClient.UploadData(Me.urlToPost, "post", reqString)
            resString = Encoding.Default.GetString(resByte)
            Dim Userparsejson As JObject = JObject.Parse(resString)
            Console.WriteLine(resString)
            responseData = Userparsejson
            webClient.Dispose()
            Return True
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return False
    End Function

End Class
Public Class employeeinfo
    Public Property emp_id As String
    Public Property emp_firstname As String
    Public Property emp_lastname As String
    Public Property emp_printname As String
    Public Property emp_idtype As String
    Public Property emp_passportic As String
    Public Property emp_nationality As String
    Public Property emp_passexpire As String
    Public Property emp_visaexpire As String
    Public Property emp_joindate As String
    Public Property emp_resigndate As String
    Public Property emp_contactno As String
    Public Property emp_contactname As String
    Public Property emp_emergencyno As String
    Public Property emp_compid As String
    Public Property emp_locid As String
    Public Property emp_designation As String
    Public Property emp_bankname As String
    Public Property emp_accountname As String
    Public Property emp_accountno As String
    Public Property emp_image As String
    Public Property emp_basicsalary As String
    Public Property emp_basicrate As String
    Public Property emp_otrate As String
    Public Property emp_othrsrate As String
    Public Property emp_allowance As String
    Public Property emp_currentstatus As String
    Public Property emp_remarks As String
    Public Property emp_active As String
    Public Property emp_epf As String
    Public Property emp_socso As String
    Public Property emp_dob As String
    Public Property emp_monthexpire As String
    Public Property emp_curpermit As String
    Public Property emp_nextpermit As String



End Class
Public Class employeeSalaryHistory
    Public Property pes_empid As String
    Public Property pes_oldsalary As String
    Public Property pes_newsalary As String
    Public Property pes_userid As String
    Public Property pes_remarks As String

End Class
Module Module1
    Function UploadFile(filePath As String, serverUrl As String, employeeId As String, ByRef errMsg As String) As Boolean
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            ' Read the file bytes from the local file
            Dim fileBytes As Byte() = File.ReadAllBytes(filePath)
            dialog.Caption = "Reading Files"
            ' Prepare the request
            Dim request As HttpWebRequest = CType(WebRequest.Create(serverUrl), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"

            ' Prepare the upload data as a JSON object
            Dim uploadData As New Dictionary(Of String, Object)()
            uploadData.Add("file", Convert.ToBase64String(fileBytes)) ' Encode file to Base64 for JSON
            uploadData.Add("filename", Path.GetFileName(filePath)) ' Name for the uploaded file
            uploadData.Add("EmployeeId", employeeId) ' Employee ID
            dialog.Caption = "Generating Json Data"

            ' Serialize the dictionary to JSON
            Dim postData As String = JsonConvert.SerializeObject(uploadData)

            ' Write the data to the request stream
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
            request.ContentLength = byteArray.Length
            dialog.Caption = "Transfering File To Server"
            Using dataStream As Stream = request.GetRequestStream()
                dataStream.Write(byteArray, 0, byteArray.Length)
            End Using

            ' Get the response
            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using dataStream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(dataStream)
                        Dim responseFromServer As String = reader.ReadToEnd()
                        Dim Userparsejson As JObject = JObject.Parse(responseFromServer)
                        Dim dtresults = Userparsejson("success")
                        Dim mesg = Userparsejson("message")
                        errMsg = mesg.ToString.ToLower
                        If dtresults.ToString().ToLower() = "true" Then
                            dialog.Caption = "File Transfered Success"
                            Return True
                        Else
                            dialog.Caption = "File Transfered Failed"
                            Return False
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            dialog.Close()
            errMsg = ex.Message
            Return False
        Finally
            dialog.Close()
        End Try
    End Function
End Module
