Imports System.Net
Imports Newtonsoft.Json.Linq
Imports System.IO

Public Class FrmTimeProfile

#Region "Initial Load"
    Private salesmenTable As DataTable

    Private Sub SetupDataTables()
        ' Setup Salesmen DataTable
        salesmenTable = New DataTable()
        salesmenTable.Columns.Add("emp_id", GetType(Integer))
        salesmenTable.Columns.Add("emp_printname", GetType(String))
        salesmenTable.Columns.Add("emp_type", GetType(String))
        GridControlEmpHeader.DataSource = salesmenTable
    End Sub

    Private Sub LoadSalesmen()
        Try
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=1")
            Dim parsedJson As JObject = JObject.Parse(json)

            salesmenTable.Clear()
            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")

                For Each item In dataArray
                    Dim empId As Integer = 0
                    Dim empName As String = ""
                    Dim empType As String = "Employee"

                    ' Try multiple possible field name variations for emp_id
                    If item("emp_id") IsNot Nothing AndAlso Not IsDBNull(item("emp_id")) Then
                        empId = Convert.ToInt32(item("emp_id"))
                    ElseIf item("EmpId") IsNot Nothing AndAlso Not IsDBNull(item("EmpId")) Then
                        empId = Convert.ToInt32(item("EmpId"))
                    ElseIf item("empid") IsNot Nothing AndAlso Not IsDBNull(item("empid")) Then
                        empId = Convert.ToInt32(item("empid"))
                    End If

                    ' Try multiple possible field name variations for employee name
                    If item("emp_printname") IsNot Nothing AndAlso item("emp_printname").ToString() <> "" Then
                        empName = item("emp_printname").ToString()
                    ElseIf item("EmpPrintName") IsNot Nothing AndAlso item("EmpPrintName").ToString() <> "" Then
                        empName = item("EmpPrintName").ToString()
                    ElseIf item("emp_firstname") IsNot Nothing AndAlso item("emp_firstname").ToString() <> "" Then
                        empName = item("emp_firstname").ToString()
                    ElseIf item("EmpFirstName") IsNot Nothing AndAlso item("EmpFirstName").ToString() <> "" Then
                        empName = item("EmpFirstName").ToString()
                    ElseIf item("emp_name") IsNot Nothing AndAlso item("emp_name").ToString() <> "" Then
                        empName = item("emp_name").ToString()
                    ElseIf item("EmpName") IsNot Nothing AndAlso item("EmpName").ToString() <> "" Then
                        empName = item("EmpName").ToString()
                    End If

                    salesmenTable.Rows.Add(empId, empName, empType)
                Next

                If _JsonData.UserTable.Rows.Count > 0 Then
                    For Each rs In _JsonData.UserTable.Rows
                        Dim Id As Integer = rs("Id")
                        Dim UserName As String = rs("UserName")
                        Dim Type As String = "User"
                        salesmenTable.Rows.Add(Id, UserName, Type)
                    Next
                End If
            Else
                MessageBox.Show("Failed to load salesmen: " & parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            ' Refresh the Salesmen GridControl
            GridControlEmpHeader.RefreshDataSource()
            GridControlEmpHeader.Refresh()
        Catch ex As Exception
            MessageBox.Show("Error loading salesmen: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

    Private Sub FrmTimeProfile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DataLoad()
        Catch ex As Exception
            MessageBox.Show("Error loading form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataLoad()
        Try
            SetupDataTables()
            LoadSalesmen()
            GridControlTimeProfile.DataSource = GetTimeProfileData()
            getAllEmployeeTimeProfiles()
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GetTimeProfileData() As DataTable
        Try
            Dim dt As New DataTable
            If CheckForInternetConnection() Then
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AttRequest=7")
                Dim Userparsejson As JObject = JObject.Parse(json)
                dt = Userparsejson("Data").ToObject(Of DataTable)()
            End If
            Return dt
        Catch ex As Exception
            MessageBox.Show("Error loading time profile data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return New DataTable()
        End Try
    End Function

    Public Sub getAllEmployeeTimeProfiles()
        Try
            Dim dt As New DataTable
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "AttRequest=12")
            Dim parsedJson As JObject = JObject.Parse(json)
            If parsedJson("Success").ToString() = "True" Then
                dt = parsedJson("Data").ToObject(Of DataTable)()
                GridControl3.DataSource = dt
            Else
                MessageBox.Show("Failed to retrieve employee time profiles: " & parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            GridControl3.DataSource = Nothing
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            Clear()
        Catch ex As Exception
            MessageBox.Show("Error creating new profile: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            Clear()
        Catch ex As Exception
            MessageBox.Show("Error clearing form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            'Validate required fields
            If (String.IsNullOrWhiteSpace(txtprofilename.Text)) Then
                MessageBox.Show("Profile Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            ElseIf (String.IsNullOrWhiteSpace(txtcheckinstart.Text) Or String.IsNullOrWhiteSpace(txtcheckinend.Text) Or
                   String.IsNullOrWhiteSpace(txtcheckoutstart.Text) Or String.IsNullOrWhiteSpace(txtcheckoutend.Text) Or
                   String.IsNullOrWhiteSpace(txtbreakinstart.Text) Or String.IsNullOrWhiteSpace(txtbreakinend.Text) Or
                   String.IsNullOrWhiteSpace(txtbreakoutstart.Text) Or String.IsNullOrWhiteSpace(txtbreakoutend.Text)) Then
                MessageBox.Show("All time fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim attendanceProfile As New AttendanceProfile() With {
                .ProfileId = If(String.IsNullOrWhiteSpace(txtprofileid.Text), "0", txtprofileid.Text),
                .ProfileName = txtprofilename.Text,
                .CheckInStart = txtcheckinstart.Text,
                .CheckInEnd = txtcheckinend.Text,
                .CheckOutStart = txtcheckoutstart.Text,
                .CheckOutEnd = txtcheckoutend.Text,
                .BreakInStart = txtbreakinstart.Text,
                .BreakInEnd = txtbreakinend.Text,
                .BreakOutStart = txtbreakoutstart.Text,
                .BreakOutEnd = txtbreakoutend.Text,
                .WorkingHours = Convert.ToDecimal(txtworkinghours.Text)
            }

            ' Serialize the object to JSON
            Dim jsonData As String = Newtonsoft.Json.JsonConvert.SerializeObject(attendanceProfile)

            ' Send the JSON data to the server
            If PostDataToServer(M_Details.LinkAjaxRequest & "AttRequest=8", jsonData) Then
                ' MessageBox.Show("Time profile saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GridControlTimeProfile.DataSource = GetTimeProfileData()
                Clear()
            Else
                MessageBox.Show("Failed to save time profile. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error saving time profile: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Clear()
        Try
            txtprofileid.Text = ""
            txtprofilename.Text = ""
            txtcheckinstart.Text = ""
            txtcheckinend.Text = ""
            txtcheckoutstart.Text = ""
            txtcheckoutend.Text = ""
            txtbreakinstart.Text = ""
            txtbreakinend.Text = ""
            txtbreakoutstart.Text = ""
            txtbreakoutend.Text = ""
            txtworkinghours.Text = "8"
        Catch ex As Exception
            MessageBox.Show("Error clearing form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function PostDataToServer(url As String, jsonData As String) As Boolean
        Try
            ' Log the JSON data being sent
            Console.WriteLine("Sending JSON: " & jsonData)

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json;"
            request.Accept = "application/json"
            ' Convert to bytes explicitly
            Dim jsonBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(jsonData)
            request.ContentLength = jsonBytes.Length

            Using requestStream As Stream = request.GetRequestStream()
                requestStream.Write(jsonBytes, 0, jsonBytes.Length)
            End Using

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                If response.StatusCode = HttpStatusCode.OK Then
                    Using streamReader As New StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8)
                        Dim result As String = streamReader.ReadToEnd()
                        Console.WriteLine("Server Response: " & result)

                        If Not String.IsNullOrWhiteSpace(result) Then
                            Dim parsedJson As JObject = JObject.Parse(result)
                            If parsedJson("Success") IsNot Nothing Then
                                Dim success As Boolean = parsedJson("Success").ToObject(Of Boolean)()
                                Dim message As String = parsedJson("Msg").ToString()
                                MessageBox.Show(message, "Server Response", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return success
                            End If
                        End If
                    End Using
                End If
            End Using

        Catch wex As WebException
            If wex.Response IsNot Nothing Then
                Using errorResponse As HttpWebResponse = CType(wex.Response, HttpWebResponse)
                    Using reader As New StreamReader(errorResponse.GetResponseStream())
                        Dim errorContent As String = reader.ReadToEnd()
                        Console.WriteLine("WebException Response: " & errorContent)
                        MessageBox.Show("WebException: " & errorContent, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Using
                End Using
            Else
                MessageBox.Show("WebException: " & wex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False

        Catch ex As Exception
            Console.WriteLine("Exception: " & ex.Message)
            MessageBox.Show("Error: " & ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

        Return False
    End Function

    Private Sub GridView2_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView2.RowClick
        Try
            Dim selectedRow As DataRow = GridView2.GetDataRow(e.RowHandle)
            If selectedRow IsNot Nothing Then
                txtprofileid.Text = selectedRow("ProfileId").ToString()
                txtprofilename.Text = selectedRow("ProfileName").ToString()
                txtcheckinstart.Text = selectedRow("CheckInStart").ToString()
                txtcheckinend.Text = selectedRow("CheckInEnd").ToString()
                txtcheckoutstart.Text = selectedRow("CheckOutStart").ToString()
                txtcheckoutend.Text = selectedRow("CheckOutEnd").ToString()
                txtbreakinstart.Text = selectedRow("BreakInStart").ToString()
                txtbreakinend.Text = selectedRow("BreakInEnd").ToString()
                txtbreakoutstart.Text = selectedRow("BreakOutStart").ToString()
                txtbreakoutend.Text = selectedRow("BreakOutEnd").ToString()
                txtworkinghours.Text = selectedRow("WorkingHours").ToString()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnDeleteprofileid_Click(sender As Object, e As EventArgs) Handles btnDeleteprofileid.Click
        Try
            Dim Id = GridView2.GetFocusedRowCellValue("ProfileId")
            Dim attendanceProfile As New AttendanceProfile() With {
                .ProfileId = Id.ToString()
            }
            ' Serialize the object to JSON
            Dim jsonData As String = Newtonsoft.Json.JsonConvert.SerializeObject(attendanceProfile)
            ' Send the JSON data to the server
            If PostDataToServer(M_Details.LinkAjaxRequest & "AttRequest=9", jsonData) Then
                MessageBox.Show("Time profile deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GridControlTimeProfile.DataSource = GetTimeProfileData()
                Clear()
            Else
                MessageBox.Show("Failed to delete time profile. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAsignProfile_Click(sender As Object, e As EventArgs) Handles btnAsignProfile.Click
        Try
            Dim EmpId = GridView1.GetFocusedRowCellValue("emp_id")
            Dim ProfileID = GridView2.GetFocusedRowCellValue("ProfileId")
            If EmpId Is Nothing Or ProfileID Is Nothing Then
                MessageBox.Show("Please select both an employee and a time profile.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            Dim payload = New With {
                    .EmployeeId = EmpId,
                    .TimeProfileId = ProfileID
                }
            ' Serialize the object to JSON
            Dim jsonData As String = Newtonsoft.Json.JsonConvert.SerializeObject(payload)
            ' Send the JSON data to the server
            If PostDataToServer(M_Details.LinkAjaxRequest & "AttRequest=10", jsonData) Then
                getAllEmployeeTimeProfiles()
                'MessageBox.Show("Time profile assigned to employee successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to assign time profile to employee. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetEmployeeTimeProfile()
        Try
            Dim EmpId = GridView1.GetFocusedRowCellValue("EmpId")
            If EmpId Is Nothing Then
                MessageBox.Show("Please select an employee.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "AttRequest=11&EmployeeId=" & EmpId)
            Dim parsedJson As JObject = JObject.Parse(json)
            If parsedJson("Success").ToString() = "True" Then
                Dim profileId As String = parsedJson("Data")("ProfileId").ToString()
                Dim profileName As String = parsedJson("Data")("ProfileName").ToString()
                MessageBox.Show("Employee is assigned to Profile: " & profileName & " (ID: " & profileId & ")", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to retrieve employee time profile: " & parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving employee time profile: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Private Sub btnGetEmployeeProfile_Click(sender As Object, e As EventArgs) Handles btnGetEmployeeProfile.Click
    '    Try
    '        GetEmployeeTimeProfile()
    '    Catch ex As Exception

    '    End Try
    'End Sub
   
    Private Sub btnDeleteEmployeTimelist_Click(sender As Object, e As EventArgs) Handles btnDeleteEmployeTimelist.Click
        Try
            Dim AssignmentId = GridView3.GetFocusedRowCellValue("AssignmentId")
            If AssignmentId Is Nothing Then
                MessageBox.Show("Please select both an AssignmentId", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            Dim payload = New With {
                    .AssignmentId = AssignmentId
                }
            ' Serialize the object to JSON
            Dim jsonData As String = Newtonsoft.Json.JsonConvert.SerializeObject(payload)
            ' Send the JSON data to the server
            If PostDataToServer(M_Details.LinkAjaxRequest & "AttRequest=13", jsonData) Then
                getAllEmployeeTimeProfiles()
                'MessageBox.Show("Time profile assigned to employee successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to assign time profile to employee. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class

Public Class AttendanceProfile
    Public Property ProfileId As String
    Public Property ProfileName As String
    Public Property CheckInStart As String
    Public Property CheckInEnd As String
    Public Property CheckOutStart As String
    Public Property CheckOutEnd As String
    Public Property BreakInStart As String
    Public Property BreakInEnd As String
    Public Property BreakOutStart As String
    Public Property BreakOutEnd As String
    Public Property WorkingHours As Decimal = 8D
End Class