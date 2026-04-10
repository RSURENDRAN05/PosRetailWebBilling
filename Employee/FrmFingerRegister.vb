

Imports System.Net
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports DPFP
Imports DPFP.Capture
Imports DPFP.Processing
Imports DPFP.Verification
Imports System.IO
Imports Newtonsoft.Json

Public Class FrmFingerRegister
    Implements DPFP.Capture.EventHandler
    Private salesmenTable As DataTable
    Private Capturer As Capture
    Private Enroller As Enrollment   ' For registration
    Private Verifier As Verification ' For verification
    Private isCapturing As Boolean = False
    Private selectedEmpId As Integer = 0
    Private fingerprintCount As Integer = 0
    Private ReadOnly maxFingerprints As Integer = 5 ' Increased to 5 for better accuracy
    Private qualityAttempts As Integer = 0
    Private ReadOnly maxQualityAttempts As Integer = 10 ' Allow more attempts for quality samples

    ' New properties for enhanced fingerprint management
    Private selectedFingerType As String = "Employee" ' "Employee" or "User"
    Private selectedFingerName As String = "Finger1" ' "Finger1", "Finger2", etc.
    Private registeredFingerprints As DataTable ' To track existing fingerprints
    Private selectedEmployeeId As Integer = 0 ' Track selected employee ID
    Private selectedEmployeeName As String = "" ' Track selected employee name
    ' Single fingerprint verification settings
    Private ReadOnly customFARThreshold As Double = 0.001 ' More lenient FAR threshold
    Private qualityFailureCount As Integer = 0 ' Track consecutive quality failures
#Region "ErrorLog"
    ' Logging method to display messages in RichTextBox
    Private Sub LogMessage(message As String)
        Try
            If errorRichBox IsNot Nothing Then
                If errorRichBox.InvokeRequired Then
                    errorRichBox.Invoke(Sub()
                                            Dim timestamp As String = DateTime.Now.ToString("HH:mm:ss.fff")
                                            errorRichBox.AppendText("[" & timestamp & "] " & message & vbCrLf)
                                            errorRichBox.ScrollToCaret()
                                        End Sub)
                Else
                    Dim timestamp As String = DateTime.Now.ToString("HH:mm:ss.fff")
                    errorRichBox.AppendText("[" & timestamp & "] " & message & vbCrLf)
                    errorRichBox.ScrollToCaret()
                End If
            End If
        Catch ex As Exception
            ' Fallback to debug output if RichTextBox fails
            System.Diagnostics.Debug.WriteLine("Log Error: " & ex.Message & " - Original: " & message)
        End Try
    End Sub

    ''' <summary>
    ''' Logs messages to the error RichTextBox with color coding and timestamps
    ''' </summary>
    ''' <param name="message">The message to log</param>
    ''' <param name="messageType">The type of message: "Error", "Info", "Success"</param>
    Private Sub LogToErrorBox(message As String, messageType As String)
        Try
            If errorRichBox IsNot Nothing Then
                If errorRichBox.InvokeRequired Then
                    errorRichBox.Invoke(Sub() WriteToErrorBox(message, messageType))
                Else
                    WriteToErrorBox(message, messageType)
                End If
            End If
        Catch ex As Exception
            ' Fallback to debug output if RichTextBox fails
            System.Diagnostics.Debug.WriteLine("LogToErrorBox Error: " & ex.Message & " - Original: " & message)
        End Try
    End Sub

    ''' <summary>
    ''' Helper method to write to the error RichTextBox with formatting
    ''' </summary>
    Private Sub WriteToErrorBox(message As String, messageType As String)
        Try
            Dim timestamp As String = DateTime.Now.ToString("HH:mm:ss.fff")
            Dim fullMessage As String = "[" & timestamp & "] " & message & vbCrLf

            ' Set color based on message type
            Dim color As Color = color.Black
            Select Case messageType.ToUpper()
                Case "ERROR"
                    color = color.Red
                Case "INFO"
                    color = color.Blue
                Case "SUCCESS"
                    color = color.Green
                Case Else
                    color = color.Black
            End Select

            ' Add colored text
            errorRichBox.SelectionStart = errorRichBox.TextLength
            errorRichBox.SelectionLength = 0
            errorRichBox.SelectionColor = color
            errorRichBox.AppendText(fullMessage)
            errorRichBox.SelectionColor = errorRichBox.ForeColor ' Reset to default color

            ' Auto-scroll to bottom
            errorRichBox.ScrollToCaret()

            ' Limit text length to prevent memory issues (keep last 10000 characters)
            If errorRichBox.TextLength > 10000 Then
                Dim textToKeep As String = errorRichBox.Text.Substring(errorRichBox.TextLength - 8000)
                errorRichBox.Clear()
                errorRichBox.AppendText("... (previous messages truncated) ..." & vbCrLf & textToKeep)
                errorRichBox.ScrollToCaret()
            End If

        Catch ex As Exception
            ' Last resort fallback
            System.Diagnostics.Debug.WriteLine("WriteToErrorBox Error: " & ex.Message)
        End Try
    End Sub
    Private Sub UpdateStatus(message As String)
        ' Add a status label or use existing UI element to show current status
        ' You might want to add a Label control for this
        Me.Text = "Fingerprint Registration - " & message
    End Sub
#End Region
#Region "ImageConversion"
    Private Function ConvertSampleToBitmap(Sample As Sample) As Bitmap
        Dim convertor As New DPFP.Capture.SampleConversion()
        Dim bitmap As Bitmap = Nothing
        convertor.ConvertToPicture(Sample, bitmap)
        Return bitmap
    End Function
    ' Helper method to safely create a template from byte array using multiple approaches
    Private Function CreateTemplateFromByteArray(templateData As Byte()) As DPFP.Template
        If templateData Is Nothing OrElse templateData.Length = 0 Then
            Return Nothing
        End If

        ' Method 1: Try direct memory stream deserialization
        Try
            Using ms As New System.IO.MemoryStream(templateData)
                Dim template As New DPFP.Template()
                template.DeSerialize(ms)
                Return template
            End Using
        Catch ex As Exception
            LogMessage("Template creation method 1 failed: " & ex.Message)
        End Try

        ' Method 2: Try fresh template with direct data assignment
        Try
            Dim template As New DPFP.Template()
            ' Validate template data size and format
            If templateData.Length > 16 Then ' Minimum template size
                Using ms As New System.IO.MemoryStream(templateData)
                    template.DeSerialize(ms)
                    Return template
                End Using
            End If
        Catch ex As Exception
            LogMessage("Template creation method 2 failed: " & ex.Message)
        End Try

        ' Method 3: Try reconstructing from raw bytes
        Try
            Dim template As New DPFP.Template()
            Return template
        Catch ex As Exception
            LogMessage("Template creation method 3 failed: " & ex.Message)
        End Try

        LogMessage("All template creation methods failed for data length: " & templateData.Length)
        Return Nothing
    End Function
#End Region
#Region "InitalLoad"

    Private Sub SetupDataTables()
        ' Setup Salesmen DataTable
        salesmenTable = New DataTable()
        salesmenTable.Columns.Add("emp_id", GetType(Integer))
        salesmenTable.Columns.Add("emp_printname", GetType(String))
        salesmenTable.Columns.Add("emp_type", GetType(String))
        GridControlEmpHeader.DataSource = salesmenTable

        ' Setup Registered Fingerprints DataTable
        registeredFingerprints = New DataTable()
        registeredFingerprints.Columns.Add("id", GetType(Integer))
        registeredFingerprints.Columns.Add("emp_id", GetType(Integer))
        registeredFingerprints.Columns.Add("emp_printname", GetType(String))
        registeredFingerprints.Columns.Add("finger_name", GetType(String))
        registeredFingerprints.Columns.Add("fingertype", GetType(String))
        registeredFingerprints.Columns.Add("created_at", GetType(DateTime))
        GridControlEmpFingerDtl.DataSource = registeredFingerprints

    End Sub

    Private Sub LoadExistingFingerprints()
        Try
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "AttRequest=3&EmpId=0")
            Dim parsedJson As JObject = JObject.Parse(json)

            registeredFingerprints.Clear()
            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")


                For Each item In dataArray
                    Dim Id As Integer = 0
                    Dim EmpId As Integer = 0
                    Dim EmpName As String = ""
                    Dim FingerName As String = ""
                    Dim FingerType As String = ""
                    Id = item("id")
                    EmpId = item("emp_id")
                    EmpName = item("emp_printname")
                    FingerName = item("finger_name")
                    FingerType = item("fingertype")
                    registeredFingerprints.Rows.Add(Id, EmpId, EmpName, FingerName, FingerType)
                Next
            End If

        Catch ex As Exception
            ' Handle errors silently for now
        End Try
    End Sub


    Private Function GetEmployeeNameById(empId As Integer) As String
        Try
            For Each row As DataRow In salesmenTable.Rows
                If Convert.ToInt32(row("emp_id")) = empId Then
                    Return row("emp_printname").ToString()
                End If
            Next
            Return "Unknown Employee"
        Catch ex As Exception
            Return "Error Loading Name"
        End Try
    End Function

    Private Sub LoadSalesmen()
        Try
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=1")
            Dim parsedJson As JObject = JObject.Parse(json)
            Dim currentCompanyId As String = _companyInfo.ComId.ToString()
            Dim currentLocationId As String = _companyInfo.LocId.ToString()
            salesmenTable.Clear()
            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")


                For Each item In dataArray
                    Dim empId As Integer = 0
                    Dim empName As String = ""
                    Dim empType As String = ""
                    empType = "Employee"
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
                    Dim employeeCompanyId As String = GetJsonFieldValue(item, "emp_compid", "EmpComId", "comid", "ComId")
                    Dim employeeLocationId As String = GetJsonFieldValue(item, "emp_locid", "EmpLocId", "locid", "LocId")

                    If ValuesMatch(employeeCompanyId, currentCompanyId) AndAlso ValuesMatch(employeeLocationId, currentLocationId) Then
                        salesmenTable.Rows.Add(empId, empName, empType)
                    End If
                Next
                If _JsonData.UserTable.Rows.Count > 0 Then
                    For Each rs In _JsonData.UserTable.Rows
                        Dim Id As Integer = 0
                        Dim UserName As String = ""
                        Dim Type As String = "User"
                        Id = rs("Id")
                        UserName = rs("UserName")
                        'Type = rs("GroupName")
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

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            LoadSalesmen()
            LoadExistingFingerprints()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FrmFingerRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetupDataTables()
            LoadSalesmen()
            LoadExistingFingerprints()
            ResetEnrollment()
        Catch ex As Exception

        End Try
    End Sub


#End Region
#Region "FingerPrintSample"
    Private Sub ProcessSample(sample As DPFP.Sample)
        Try
            ' ==========================
            ' VERIFICATION MODE
            ' ==========================
            If isVerifying Then
                VerifyFingerprint(sample)
                Return
            End If

            ' ==========================
            ' ENROLLMENT MODE
            ' ==========================
            Dim features As DPFP.FeatureSet = ExtractFeatures(sample, DPFP.Processing.DataPurpose.Enrollment)

            If features IsNot Nothing Then
                If Enroller Is Nothing Then
                    Enroller = New Enrollment()
                End If

                Try
                    Dim previousFeaturesNeeded As Integer = Enroller.FeaturesNeeded

                    ' Add features to enrollment
                    Enroller.AddFeatures(features)

                    If Enroller.TemplateStatus <> Enrollment.Status.Failed AndAlso
                       (Enroller.TemplateStatus = Enrollment.Status.Ready OrElse
                        Enroller.FeaturesNeeded < previousFeaturesNeeded) Then

                        fingerprintCount += 1
                        UpdateStatus("Sample " & fingerprintCount & " of " & maxFingerprints & " captured successfully.")
                    Else
                        UpdateStatus("Poor quality sample detected. Please try again.")
                        Return
                    End If

                    ' Check final enrollment status
                    Select Case Enroller.TemplateStatus
                        Case Enrollment.Status.Ready

                            UpdateStatus("Fingerprint template created successfully!")
                            SaveFingerprintTemplate()

                        Case Enrollment.Status.Failed
                            MessageBox.Show("Enrollment failed due to poor quality samples. Please try again.", "Enrollment Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            ResetEnrollment()

                        Case Else
                            If fingerprintCount < maxFingerprints Then
                                UpdateStatus("Sample " & fingerprintCount & " captured. Please place finger again for sample " & (fingerprintCount + 1) & ".")
                            Else
                                Dim result As DialogResult = MessageBox.Show(
                                    "Unable to create a reliable template with current samples." & vbCrLf &
                                    "Possible causes:" & vbCrLf &
                                    "• Dry/damaged finger" & vbCrLf &
                                    "• Inconsistent placement" & vbCrLf &
                                    "• Dirty scanner surface" & vbCrLf & vbCrLf &
                                    "Would you like to retry enrollment?",
                                    "Enrollment Retry Required",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question)

                                If result = DialogResult.Yes Then
                                    ResetEnrollment()
                                    UpdateStatus("Ready to start enrollment again.")
                                Else
                                    StopCapture()
                                    UpdateStatus("Enrollment cancelled.")
                                End If
                            End If
                    End Select

                Catch enrollEx As Exception
                    MessageBox.Show("Error adding fingerprint features: " & enrollEx.Message, "Enrollment Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ResetEnrollment()
                End Try
            Else
                UpdateStatus("Poor quality sample detected. Please clean your finger and scanner, then try again.")
            End If

        Catch ex As Exception
            MessageBox.Show("Error processing sample: " & ex.Message, "Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
#Region "StopStartCapture"

    Private Sub StartCapture()
        Try
            If selectedEmpId = 0 Then
                MessageBox.Show("Please select an employee first.", "No Employee Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If isCapturing Then
                StopCapture()
                Return
            End If

            ' Initialize capturer if not already done
            If Capturer Is Nothing Then
                Try
                    Capturer = New Capture()
                    Capturer.EventHandler = Me
                Catch dpfpEx As System.Runtime.InteropServices.COMException
                    MessageBox.Show("Failed to initialize fingerprint reader. Please:" & vbCrLf &
                                  "1. Check if the fingerprint device is connected" & vbCrLf &
                                  "2. Install/reinstall fingerprint device drivers" & vbCrLf &
                                  "3. Restart the application" & vbCrLf & vbCrLf &
                                  "Error: " & dpfpEx.Message,
                                  "Device Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Catch ex As Exception
                    MessageBox.Show("Unexpected error initializing fingerprint reader: " & ex.Message,
                                  "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End Try
            End If

            ' Start capture in background
            Try
                Capturer.StartCapture()
                isCapturing = True

                ' Update UI
                btnstartCapture.Text = "Stop Capture"
                UpdateStatus("Capturing fingerprint for " & lblempname.Text & ". Place finger " & (fingerprintCount + 1).ToString() & " of " & maxFingerprints.ToString())
            Catch dpfpEx As System.Runtime.InteropServices.COMException
                MessageBox.Show("Failed to start fingerprint capture. Device may be in use by another application." & vbCrLf &
                              "Error: " & dpfpEx.Message,
                              "Capture Start Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                isCapturing = False
                btnstartCapture.Text = "Start Capture"
            End Try

        Catch ex As Exception
            MessageBox.Show("Error starting capture: " & ex.Message, "Capture Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            isCapturing = False
            btnstartCapture.Text = "Start Capture"
        End Try
    End Sub

    Private Sub StopCapture()
        Try
            ' Set flags first to prevent new operations
            isCapturing = False
            isVerifying = False

            ' Stop the capturer with error handling
            If Capturer IsNot Nothing Then
                Try
                    Capturer.StopCapture()
                    Threading.Thread.Sleep(100) ' Give device time to stop
                Catch comEx As System.Runtime.InteropServices.COMException
                    ' Log COM exceptions but don't show to user during normal stop
                    LogToErrorBox("COM exception during StopCapture: " & comEx.Message, "Error")
                Catch ex As Exception
                    LogToErrorBox("Exception during StopCapture: " & ex.Message, "Error")
                End Try
            End If

            ' Clear verification state
            storedTemplateForVerification = Nothing

            ' Update UI
            btnstartCapture.Text = "Start Capture"
            UpdateStatus("Capture stopped.")

        Catch ex As Exception
            MessageBox.Show("Error stopping capture: " & ex.Message)
            ' Ensure states are reset even if error occurs
            isCapturing = False
            isVerifying = False
            btnstartCapture.Text = "Start Capture"
        End Try
    End Sub




#End Region
#Region "ResetProcess"
    Private Sub ResetEnrollment()
        Try
            ' Only reset enrollment-specific variables, not verification state
            fingerprintCount = 0
            qualityAttempts = 0
            nudFingerIndex.Minimum = 1
            nudFingerIndex.Maximum = 9
            ' Reset enrollment object only if not in verification mode
            If Not isVerifying Then
                If Enroller IsNot Nothing Then
                    Enroller = Nothing
                End If
                Enroller = New Enrollment()
                ' Update UI to show progress
                UpdateStatus("Employee selected. Ready to capture fingerprint. Please ensure finger is clean and dry.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error resetting enrollment: " & ex.Message)
        End Try
    End Sub
#End Region
#Region "EnrollUser"
    Private Sub GridViewEmpHeader_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridViewEmpHeader.RowClick
        Try
            Dim foucustedRow As Integer = 0
            foucustedRow = GridViewEmpHeader.FocusedRowHandle
            Dim empId = GridViewEmpHeader.GetFocusedRowCellValue("emp_id")
            Dim empName = GridViewEmpHeader.GetFocusedRowCellValue("emp_printname")
            Dim empType = GridViewEmpHeader.GetFocusedRowCellValue("emp_type")
            lblempid.Text = empId
            lblempname.Text = empName
            lbltype.Text = empType
            selectedEmpId = Convert.ToInt32(empId)

            ' Update new tracking variables
            selectedEmployeeId = selectedEmpId
            selectedEmployeeName = empName.ToString()
            selectedFingerType = empType
            selectedFingerName = nudFingerIndex.Value
            ' Enable enrollment button
            btnstartCapture.Enabled = True

            ' Reset enrollment when selecting new employee
            ResetEnrollment()
        Catch ex As Exception
            MessageBox.Show("Error selecting employee: " & ex.Message)
        End Try
    End Sub
    Private Sub GridViewEmpFingerDtl_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridViewEmpFingerDtl.RowClick
        Try
            Dim foucustedRow As Integer = 0
            foucustedRow = GridViewEmpFingerDtl.FocusedRowHandle
            Dim FingerId = GridViewEmpFingerDtl.GetFocusedRowCellValue("id")
            Dim empId = GridViewEmpFingerDtl.GetFocusedRowCellValue("emp_id")
            Dim empName = GridViewEmpFingerDtl.GetFocusedRowCellValue("emp_printname")
            Dim empType = GridViewEmpFingerDtl.GetFocusedRowCellValue("fingertype")
            lblempid.Text = empId
            lblempname.Text = empName
            lbltype.Text = empType
            selectedEmpId = Convert.ToInt32(empId)

            ' Update new tracking variables
            selectedEmployeeId = selectedEmpId
            selectedEmployeeName = empName.ToString()
            selectedFingerType = empType
            selectedFingerName = nudFingerIndex.Value
            ' Enable enrollment button
            btnstartCapture.Enabled = True

            ' Reset enrollment when selecting new employee
            ResetEnrollment()
        Catch ex As Exception

        End Try
    End Sub
    Private Async Sub SaveFingerprintTemplate()
        Try
            If Enroller.TemplateStatus = Enrollment.Status.Ready Then
                ' Get template with enhanced validation
                Dim template As DPFP.Template = Enroller.Template

                ' Validate template before serialization
                If template Is Nothing OrElse template.Size = 0 Then
                    MessageBox.Show("Invalid template generated. Please try capturing fingerprint again.", "Template Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' Enhanced serialization with validation
                Dim templateBytes() As Byte = Nothing
                Using stream As New IO.MemoryStream()
                    Try
                        template.Serialize(stream)
                        templateBytes = stream.ToArray()

                        ' Validate serialized data
                        If templateBytes Is Nothing OrElse templateBytes.Length = 0 Then
                            MessageBox.Show("Failed to serialize fingerprint template. Please try again.", "Serialization Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return
                        End If

                        LogToErrorBox("Template serialized successfully. Size: " & templateBytes.Length & " bytes", "Success")

                    Catch serEx As Exception
                        MessageBox.Show("Error serializing template: " & serEx.Message, "Serialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End Try
                End Using

                ' Convert to Base64 with validation
                Dim templateBase64 As String = Convert.ToBase64String(templateBytes)

                ' Additional validation - verify round-trip conversion
                Try
                    Dim testBytes() As Byte = Convert.FromBase64String(templateBase64)
                    If testBytes.Length <> templateBytes.Length Then
                        MessageBox.Show("Template encoding validation failed. Please try again.", "Encoding Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                Catch b64Ex As Exception
                    MessageBox.Show("Base64 encoding failed: " & b64Ex.Message, "Encoding Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End Try

                UpdateStatus("Saving fingerprint template...")

                ' Send to server
                Dim success As Boolean = Await SaveFingerprintToServer(selectedEmpId, templateBase64, selectedFingerType, selectedFingerName)

                If success Then
                    MessageBox.Show("Fingerprint registered successfully for " & lblempname.Text & "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    StopCapture()
                    ResetEnrollment()

                    ' Refresh the fingerprint display
                    LoadExistingFingerprints()

                Else
                    MessageBox.Show("Failed to save fingerprint to server. Please try again.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error saving fingerprint template: " & ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Async Function SaveFingerprintToServer(empId As Integer, templateBase64 As String, fingerType As String, fingerName As String) As Task(Of Boolean)
        Try
            ' Validate input parameters
            If empId <= 0 Then
                MessageBox.Show("Invalid employee ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If String.IsNullOrEmpty(templateBase64) Then
                MessageBox.Show("Template data is empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If


            Dim postData As String = "{""EmpId"":" & empId.ToString() & ",""Template"":""" & templateBase64 & """,""FingerType"":""" & fingerType & """,""FingerName"":""" & fingerName & """}"

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/json"

                Dim response As String = Await Task.Run(Function()
                                                            Return client.UploadString(M_Details.LinkAjaxRequest & "AttRequest=1", postData)
                                                        End Function)

                If String.IsNullOrEmpty(response) Then
                    MessageBox.Show("Empty response from server.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If

                Dim parsedResponse As JObject = JObject.Parse(response)
                Dim success As Boolean = parsedResponse("Success").ToString().ToLower() = "true"

                If Not success Then
                    Dim errorMsg As String = "Unknown error"
                    If parsedResponse("Msg") IsNot Nothing Then
                        errorMsg = parsedResponse("Msg").ToString()
                    End If
                    LogToErrorBox("Server save error: " & errorMsg, "Error")
                End If

                Return success
            End Using
        Catch ex As Exception
            MessageBox.Show("Network error saving fingerprint: " & ex.Message, "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Private Function ExtractFeatures(Sample As Sample, purpose As DPFP.Processing.DataPurpose) As DPFP.FeatureSet
        Try
            Dim extractor As New DPFP.Processing.FeatureExtraction()
            Dim feedback As DPFP.Capture.CaptureFeedback = Nothing
            Dim features As New DPFP.FeatureSet()

            ' Extract features from the sample
            extractor.CreateFeatureSet(Sample, purpose, feedback, features)

            ' Log feedback for verification troubleshooting
            If purpose = DPFP.Processing.DataPurpose.Verification Then
                LogToErrorBox("Feature extraction feedback: " & feedback.ToString(), "Info")
            End If

            ' Check the quality feedback
            Select Case feedback
                Case DPFP.Capture.CaptureFeedback.Good
                    ' Good quality - return the features
                    If purpose = DPFP.Processing.DataPurpose.Verification Then
                        LogToErrorBox("Feature extraction successful - Good quality sample", "Success")
                    End If
                    Return features

                Case DPFP.Capture.CaptureFeedback.NoFinger
                    UpdateStatus("No finger detected. Please place finger on scanner.")
                    Return Nothing

                Case DPFP.Capture.CaptureFeedback.TooLight
                    UpdateStatus("Press harder on the scanner.")
                    If purpose = DPFP.Processing.DataPurpose.Verification Then
                        LogToErrorBox("Sample too light - need more pressure", "Warning")
                    End If
                    Return Nothing

                Case DPFP.Capture.CaptureFeedback.TooDark
                    UpdateStatus("Press lighter on the scanner.")
                    If purpose = DPFP.Processing.DataPurpose.Verification Then
                        LogToErrorBox("Sample too dark - need less pressure", "Warning")
                    End If
                    Return Nothing

                Case DPFP.Capture.CaptureFeedback.TooLeft, DPFP.Capture.CaptureFeedback.TooRight,
                     DPFP.Capture.CaptureFeedback.TooHigh, DPFP.Capture.CaptureFeedback.TooLow
                    UpdateStatus("Center your finger on the scanner.")
                    If purpose = DPFP.Processing.DataPurpose.Verification Then
                        LogToErrorBox("Finger positioning issue: " & feedback.ToString(), "Warning")
                    End If
                    Return Nothing

                Case DPFP.Capture.CaptureFeedback.TooFast
                    UpdateStatus("Place finger more slowly and hold steady.")
                    Return Nothing

                Case DPFP.Capture.CaptureFeedback.TooSlow
                    UpdateStatus("Place finger more quickly on the scanner.")
                    Return Nothing

                Case DPFP.Capture.CaptureFeedback.TooSkewed
                    UpdateStatus("Place finger straight on the scanner.")
                    If purpose = DPFP.Processing.DataPurpose.Verification Then
                        LogToErrorBox("Finger angle issue - place finger flat", "Warning")
                    End If
                    Return Nothing

                Case DPFP.Capture.CaptureFeedback.TooShort
                    UpdateStatus("Place more of your finger on the scanner.")
                    Return Nothing

                Case Else
                    UpdateStatus("Poor quality sample. Please clean finger and scanner, then try again.")
                    Return Nothing
            End Select

        Catch ex As Exception
            UpdateStatus("Error extracting fingerprint features: " & ex.Message)
            Return Nothing
        End Try
    End Function
    Private Sub btnstartCapture_Click(sender As Object, e As EventArgs) Handles btnstartCapture.Click
        Try
            If isVerifying Then
                StopVerification()
                UpdateStatus("Verification cancelled.")
            ElseIf isCapturing Then
                StopCapture()
            Else
                StartCapture()
            End If
        Catch ex As Exception
            MessageBox.Show("Error managing capture: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FrmFingerRegister_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            ' Ensure capture is stopped when form closes
            StopCapture()

            ' Clean up resources with better error handling
            If Capturer IsNot Nothing Then
                Try
                    Capturer.Dispose()
                Catch comEx As System.Runtime.InteropServices.COMException
                    ' Ignore COM exceptions during disposal
                    LogToErrorBox("COM exception during Capturer disposal: " & comEx.Message, "Error")
                Catch ex As Exception
                    ' Log other exceptions but don't show to user during form closing
                    LogToErrorBox("Exception during Capturer disposal: " & ex.Message, "Error")
                End Try
                Capturer = Nothing
            End If

            If Enroller IsNot Nothing Then
                Enroller = Nothing
            End If

            If Verifier IsNot Nothing Then
                Verifier = Nothing
            End If

            ' Clear template reference
            storedTemplateForVerification = Nothing

        Catch ex As Exception
            ' Handle cleanup errors silently during form closing
            LogToErrorBox("Error during form closing cleanup: " & ex.Message, "Error")
        End Try
    End Sub
    Private Sub ShowEnrollmentTips()
        Dim tips As String = "Fingerprint Enrollment Tips:" & vbCrLf & vbCrLf &
                           "1. Clean your finger and the scanner surface" & vbCrLf &
                           "2. Place your finger flat on the center of the scanner" & vbCrLf &
                           "3. Apply moderate pressure - not too light, not too hard" & vbCrLf &
                           "4. Keep your finger still during the scan" & vbCrLf &
                           "5. Use the same finger for all samples" & vbCrLf &
                           "6. Avoid using injured or damaged fingers" & vbCrLf & vbCrLf &
                           "The system will collect " & maxFingerprints.ToString() & " samples for best accuracy."

        MessageBox.Show(tips, "Enrollment Tips", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub ShowFingerprintQualityTips()
        Dim qualityTips As String = "Fingerprint Quality Improvement Tips:" & vbCrLf & vbCrLf &
                                  "📋 PREPARATION:" & vbCrLf &
                                  "• Clean your finger with a dry cloth" & vbCrLf &
                                  "• Clean the scanner surface with a soft cloth" & vbCrLf &
                                  "• Ensure your finger is completely dry" & vbCrLf & vbCrLf &
                                  "👆 FINGER PLACEMENT:" & vbCrLf &
                                  "• Place the center of your fingerprint on the scanner" & vbCrLf &
                                  "• Press down firmly but not too hard" & vbCrLf &
                                  "• Keep your finger flat (avoid tilting)" & vbCrLf &
                                  "• Hold completely still until the scan completes" & vbCrLf & vbCrLf &
                                  "⚠️ AVOID:" & vbCrLf &
                                  "• Wet or oily fingers" & vbCrLf &
                                  "• Moving during the scan" & vbCrLf &
                                  "• Too light or too heavy pressure" & vbCrLf &
                                  "• Using injured or bandaged fingers" & vbCrLf & vbCrLf &
                                  "🔄 If verification continues to fail, try:" & vbCrLf &
                                  "• Different finger pressure" & vbCrLf &
                                  "• Slightly different finger position" & vbCrLf &
                                  "• Re-registering the fingerprint"

        MessageBox.Show(qualityTips, "Fingerprint Quality Tips", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
#End Region
#Region "EventHandler"
    Public Sub OnComplete(Capture As Object, ReaderSerialNumber As String, Sample As Sample) _
  Implements EventHandler.OnComplete
        Try
            ' This runs on background thread - use Invoke for UI updates
            If Me.InvokeRequired Then
                Me.Invoke(New Action(Of Object, String, Sample)(AddressOf OnComplete), Capture, ReaderSerialNumber, Sample)
                Return
            End If

            ' Convert the sample to an image and display
            Dim bmp As Bitmap = ConvertSampleToBitmap(Sample)
            lblfingerimage.Image = bmp  ' Show fingerprint image

            ' Process the fingerprint sample
            ProcessSample(Sample)
        Catch ex As Exception
            MessageBox.Show("Error in OnComplete: " & ex.Message)
        End Try
    End Sub

    Public Sub OnFingerTouch(Capture As Object, ReaderSerialNumber As String) _
        Implements EventHandler.OnFingerTouch
        Try
            ' This runs on background thread - use Invoke for UI updates
            If Me.InvokeRequired Then
                Me.Invoke(New Action(Of Object, String)(AddressOf OnFingerTouch), Capture, ReaderSerialNumber)
                Return
            End If

            UpdateStatus("Finger detected. Hold steady...")
        Catch ex As Exception
            ' Handle silently to avoid interrupting capture
        End Try
    End Sub

    Public Sub OnFingerGone(Capture As Object, ReaderSerialNumber As String) _
        Implements EventHandler.OnFingerGone
        Try
            ' This runs on background thread - use Invoke for UI updates
            If Me.InvokeRequired Then
                Me.Invoke(New Action(Of Object, String)(AddressOf OnFingerGone), Capture, ReaderSerialNumber)
                Return
            End If

            UpdateStatus("Finger removed. Place finger again when ready...")
        Catch ex As Exception
            ' Handle silently to avoid interrupting capture
        End Try
    End Sub

    Public Sub OnReaderConnect(Capture As Object, ReaderSerialNumber As String) _
        Implements EventHandler.OnReaderConnect
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New Action(Of Object, String)(AddressOf OnReaderConnect), Capture, ReaderSerialNumber)
                Return
            End If

            UpdateStatus("Fingerprint reader connected: " & ReaderSerialNumber)
        Catch ex As Exception
            MessageBox.Show("Error in OnReaderConnect: " & ex.Message)
        End Try
    End Sub

    Public Sub OnReaderDisconnect(Capture As Object, ReaderSerialNumber As String) _
        Implements EventHandler.OnReaderDisconnect
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New Action(Of Object, String)(AddressOf OnReaderDisconnect), Capture, ReaderSerialNumber)
                Return
            End If

            UpdateStatus("Fingerprint reader disconnected!")
            StopCapture()
        Catch ex As Exception
            MessageBox.Show("Error in OnReaderDisconnect: " & ex.Message)
        End Try
    End Sub
    Public Sub OnSampleQuality(Capture As Object, ReaderSerialNumber As String, Feedback As CaptureFeedback) _
        Implements EventHandler.OnSampleQuality
        Try
            ' This runs on background thread - use Invoke for UI updates
            If Me.InvokeRequired Then
                Me.Invoke(New Action(Of Object, String, CaptureFeedback)(AddressOf OnSampleQuality), Capture, ReaderSerialNumber, Feedback)
                Return
            End If

            ' Provide feedback based on sample quality
            Select Case Feedback
                Case CaptureFeedback.Good
                    UpdateStatus("Good quality sample detected...")
                Case CaptureFeedback.NoFinger
                    UpdateStatus("Place finger on reader...")
                Case CaptureFeedback.TooLight
                    UpdateStatus("Press harder on the reader...")
                Case CaptureFeedback.TooDark
                    UpdateStatus("Press lighter on the reader...")
                Case CaptureFeedback.TooLeft, CaptureFeedback.TooRight, CaptureFeedback.TooHigh, CaptureFeedback.TooLow
                    UpdateStatus("Center finger on the reader...")
                Case CaptureFeedback.TooFast
                    UpdateStatus("Hold finger steady...")
                Case CaptureFeedback.TooSlow
                    UpdateStatus("Place finger more quickly...")
                Case CaptureFeedback.TooSkewed
                    UpdateStatus("Place finger straight on reader...")
                Case CaptureFeedback.TooShort
                    UpdateStatus("Place whole finger on reader...")
                Case Else
                    UpdateStatus("Adjust finger position and try again...")
            End Select
        Catch ex As Exception
            ' Handle silently to avoid interrupting capture
        End Try
    End Sub
#End Region
#Region "FingerPrintVerfication"
    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Try
            If selectedEmpId = 0 Then
                MessageBox.Show("Please select an employee first.", "No Employee Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Test device connectivity before starting verification
            If Not TestDeviceConnectivity() Then
                Return
            End If

            ' Get employee fingerprint template from server
            GetFingerprintDetailsAndVerify()
        Catch ex As Exception
            MessageBox.Show("Error in test verification: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function TestDeviceConnectivity() As Boolean
        Try
            UpdateStatus("Testing device connectivity...")

            ' Create a test capturer to check device
            Dim testCapturer As Capture = Nothing
            Try
                testCapturer = New Capture()

                ' If we can create the capturer, device is available
                testCapturer.Dispose()
                UpdateStatus("Device connectivity test passed.")
                Return True

            Catch dpfpEx As System.Runtime.InteropServices.COMException
                MessageBox.Show("Device connectivity test failed:" & vbCrLf & vbCrLf &
                              "The fingerprint device is not responding properly." & vbCrLf &
                              "Please try the following:" & vbCrLf &
                              "1. Unplug and reconnect the fingerprint device" & vbCrLf &
                              "2. Wait 5 seconds, then try again" & vbCrLf &
                              "3. Restart this application" & vbCrLf &
                              "4. Restart your computer if problems persist" & vbCrLf & vbCrLf &
                              "Technical Error: " & dpfpEx.Message,
                              "Device Communication Test Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            Finally
                If testCapturer IsNot Nothing Then
                    Try
                        testCapturer.Dispose()
                    Catch
                        ' Ignore disposal errors
                    End Try
                End If
            End Try

        Catch ex As Exception
            MessageBox.Show("Error testing device connectivity: " & ex.Message, "Test Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Dim FingerTable As DataTable
    Private Async Sub GetFingerprintDetailsAndVerify()
        Try
            UpdateStatus("Retrieving fingerprint template for verification...")
            FingerTable = New DataTable
            ' Get fingerprint template from server for verification
            FingerTable = Await GetFingerprintFromServerbyIdAllFinger(selectedEmpId)
            If FingerTable.Rows.Count > 0 Then
                UpdateStatus("Found fingerprint template. Please place your finger for verification...")
                ' Start single fingerprint verification process
                StartVerificationCapture()
            Else
                MessageBox.Show("No fingerprint template found for " & lblempname.Text & ". Please register fingerprint first.", "No Template Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                lblstatusimage.Image = Img.Images(0) ' Set to failure image
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving fingerprint details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            lblstatusimage.Image = Img.Images(0) ' Set to failure image
        End Try
    End Sub

    Private Async Function GetFingerprintFromServer(empId As Integer) As Task(Of DPFP.Template)
        Try
            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/json"

                Dim postData As String = "{""EmpId"":" & empId.ToString() & "}"
                Dim response As String = Await Task.Run(Function()
                                                            Return client.UploadString(M_Details.LinkAjaxRequest & "AttRequest=2", postData)
                                                        End Function)

                ' Debug: Log the raw response
                'LogToErrorBox("Raw server response: " & response, "Info")

                ' Clean up response if needed (remove any extra content)
                response = response.Trim()
                If response.Contains("}{") Then
                    ' Multiple JSON objects detected, take only the first one
                    Dim firstBrace As Integer = response.IndexOf("}")
                    If firstBrace > 0 Then
                        response = response.Substring(0, firstBrace + 1)
                    End If
                End If

                If String.IsNullOrEmpty(response) Then
                    MessageBox.Show("Empty response from server.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End If

                Dim parsedResponse As JObject = JObject.Parse(response)

                If parsedResponse("Success").ToString().ToLower() = "true" Then
                    Dim templateBase64 As String = parsedResponse("Template").ToString()
                    If Not String.IsNullOrEmpty(templateBase64) Then
                        ' Validate Base64 format before conversion
                        Try
                            ' Convert Base64 back to template with validation
                            Dim templateBytes() As Byte = Convert.FromBase64String(templateBase64)

                            ' Validate template data size
                            If templateBytes Is Nothing OrElse templateBytes.Length < 100 Then
                                MessageBox.Show("Invalid template data received from server (too small). Please re-register fingerprint.",
                                              "Invalid Template", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Return Nothing
                            End If

                            LogToErrorBox("Template data retrieved. Size: " & templateBytes.Length & " bytes", "Info")

                            ' Use helper method to safely create template
                            Dim template As DPFP.Template = CreateTemplateFromByteArray(templateBytes)

                            ' Verify template is valid before returning
                            If template IsNot Nothing AndAlso template.Size > 0 Then
                                LogToErrorBox("Template created successfully. DPFP Size: " & template.Size, "Success")

                                ' Additional validation: Recreate template in current context to ensure compatibility
                                Dim localTemplate As DPFP.Template = RecreateTemplateInCurrentContext(template)
                                If localTemplate IsNot Nothing Then
                                    LogToErrorBox("Template validated and recreated in current context successfully", "Success")
                                    Return localTemplate
                                Else
                                    LogToErrorBox("Template recreation in current context failed", "Warning")
                                    Return template ' Return original if recreation fails
                                End If
                            Else
                                LogToErrorBox("Template created but has zero size or is null", "Error")
                                MessageBox.Show("Invalid fingerprint template retrieved from server. Please re-register the fingerprint.",
                                              "Invalid Template", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Return Nothing
                            End If

                        Catch b64Ex As Exception
                            MessageBox.Show("Invalid Base64 template data from server: " & b64Ex.Message,
                                          "Data Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return Nothing
                        End Try
                    Else
                        MessageBox.Show("Empty template data received from server.", "No Template Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return Nothing
                    End If
                Else
                    Dim errorMsg As String = "Failed to retrieve fingerprint template"
                    If parsedResponse("Msg") IsNot Nothing Then
                        errorMsg = parsedResponse("Msg").ToString()
                    End If
                    MessageBox.Show(errorMsg, "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return Nothing
                End If

                Return Nothing
            End Using
        Catch jsonEx As Newtonsoft.Json.JsonReaderException
            MessageBox.Show("Server returned invalid JSON response. Please check server configuration." & vbCrLf & "Error: " & jsonEx.Message, "JSON Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show("Network error retrieving fingerprint: " & ex.Message, "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Private Async Function GetFingerprintFromServerbyIdAllFinger(empId As Integer) As Task(Of DataTable)
        Try
            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/json"

                Dim payload = New With {
                     .EmpId = lblempid.Text,
                     .FingerType = lbltype.Text
                 }
                Dim postData As String = JsonConvert.SerializeObject(payload)
                client.Headers(HttpRequestHeader.ContentType) = "application/json"

                ' Dim postData As String = "{""EmpId"":" & lblempid.Text.ToString() & "FingerType"":" & lbltype.Text.ToString & "}"
                Dim response As String = Await Task.Run(Function()
                                                            Return client.UploadString(M_Details.LinkAjaxRequest & "AttRequest=3", postData)
                                                        End Function)

                ' Debug: Log the raw response
                'LogToErrorBox("Raw server response: " & response, "Info")

                ' Clean up response if needed (remove any extra content)
                response = response.Trim()
                If response.Contains("}{") Then
                    ' Multiple JSON objects detected, take only the first one
                    Dim firstBrace As Integer = response.IndexOf("}")
                    If firstBrace > 0 Then
                        response = response.Substring(0, firstBrace + 1)
                    End If
                End If

                If String.IsNullOrEmpty(response) Then
                    MessageBox.Show("Empty response from server.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End If

                Dim parsedResponse As JObject = JObject.Parse(response)
                Dim dt As New DataTable
                If parsedResponse("Success").ToString().ToLower() = "true" Then
                    dt = parsedResponse("Data").ToObject(Of DataTable)()
                    If dt.Rows.Count > 0 Then
                        Return dt
                        'For Each rs In dt.Rows
                        '    Dim FingerId = rs("id")
                        '    Dim EmpIds = rs("emp_id")
                        '    Dim FingerIndex = rs("finger_name")
                        '    Dim FingerTemplate = rs("finger_template")
                        '    Dim CreatedAt = rs("created_at")
                        'Next
                    End If
                End If
                Return Nothing
            End Using
        Catch jsonEx As Newtonsoft.Json.JsonReaderException
            MessageBox.Show("Server returned invalid JSON response. Please check server configuration." & vbCrLf & "Error: " & jsonEx.Message, "JSON Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show("Network error retrieving fingerprint: " & ex.Message, "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Private storedTemplateForVerification As DPFP.Template = Nothing
    Private isVerifying As Boolean = False

    Private Sub StartVerificationCapture()
        Try
            ' Ensure we're not in capture mode first
            If isCapturing Then
                StopCapture()
                ' Longer delay to ensure capture is fully stopped
                Threading.Thread.Sleep(500)
            End If

            ' Store template for verification
            'storedTemplateForVerification = template
            isVerifying = True
            qualityFailureCount = 0 ' Reset quality failure counter for new verification session
            ' Force reinitialize capturer for verification to avoid device conflicts
            If Capturer IsNot Nothing Then
                Try
                    Capturer.Dispose()
                    Threading.Thread.Sleep(200)
                Catch
                    ' Ignore disposal errors
                End Try
                Capturer = Nothing
            End If

            ' Create fresh capturer instance for verification
            Try
                UpdateStatus("Initializing device for verification...")
                Capturer = New Capture()
                Capturer.EventHandler = Me
                Threading.Thread.Sleep(300) ' Give device time to initialize

                UpdateStatus("Device initialized. Starting verification capture...")
                Capturer.StartCapture()
                isCapturing = True
                btnstartCapture.Text = "Stop Verification"
                UpdateStatus("Place finger for verification...")

                ' Clear any previous status image
                lblstatusimage.Image = Nothing

            Catch dpfpEx As System.Runtime.InteropServices.COMException
                MessageBox.Show("Device communication error during verification startup:" & vbCrLf & vbCrLf &
                              "Possible solutions:" & vbCrLf &
                              "1. Unplug and reconnect the fingerprint device" & vbCrLf &
                              "2. Close all fingerprint applications and restart this app" & vbCrLf &
                              "3. Restart your computer if the problem persists" & vbCrLf & vbCrLf &
                              "Technical Error: " & dpfpEx.Message,
                              "Device Communication Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                isVerifying = False
                lblstatusimage.Image = Img.Images(0)

                ' Try to reinitialize components
                ReinitializeDPFPComponents()

            Catch captureEx As Exception
                MessageBox.Show("Failed to start verification capture:" & vbCrLf &
                              captureEx.Message & vbCrLf & vbCrLf &
                              "Please try again. If the problem persists, restart the application.",
                              "Capture Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                isVerifying = False
                lblstatusimage.Image = Img.Images(0)
            End Try

        Catch ex As Exception
            MessageBox.Show("Error starting verification capture: " & ex.Message, "Verification Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            isVerifying = False
            lblstatusimage.Image = Img.Images(0) ' Set to failure image
        End Try
    End Sub
    Private Sub VerifyFingerprint(sample As DPFP.Sample)
        Try
            If Not isVerifying OrElse FingerTable.Rows.Count = 0 Then
                LogMessage("Verification aborted: isVerifying=" & isVerifying & ", templateExists=" & (storedTemplateForVerification IsNot Nothing))
                Return
            End If

            ' Extract features for verification
            Dim features As DPFP.FeatureSet = ExtractFeatures(sample, DataPurpose.Verification)

            If features IsNot Nothing Then
                Try
                    ' Log sample quality information
                    LogToErrorBox("Sample Quality - Size: " & sample.Bytes.Length & " bytes", "Info")

                    ' Validate template before verification
                    If FingerTable.Rows.Count = 0 Then
                        LogToErrorBox("Invalid template detected: Size = 0", "Error")
                        MessageBox.Show("Invalid template detected. Please re-register the fingerprint.", "Invalid Template", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        StopVerification()
                        Return
                    End If
                    LogToErrorBox("Starting verification - Template Size: " & FingerTable.Rows.Count & ", FAR Threshold: " & customFARThreshold, "Info")

                    Dim verificationSuccessful As Boolean = False

                    Verifier = New Verification
                    ' Try verification with multiple fallback strategies
                    Try
                        For Each rs In FingerTable.Rows
                            Dim templateBase64 As String = rs("finger_template")
                            Dim templateBytes() As Byte = Convert.FromBase64String(templateBase64)

                            ' Validate template data size
                            If templateBytes Is Nothing OrElse templateBytes.Length < 100 Then
                                MessageBox.Show("Invalid template data received from server (too small). Please re-register fingerprint.",
                                              "Invalid Template", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Return
                            End If
                            LogToErrorBox("Verification completed with standard threshold", "Info")
                            Using ms As New MemoryStream(templateBytes)
                                Dim tpl = New Template(ms)
                                Dim res As New Verification.Result()
                                Verifier.Verify(features, tpl, res)
                                If res.Verified Then
                                    verificationSuccessful = True
                                    Exit For
                                End If
                            End Using
                        Next
                    Catch verifyEx As Exception
                        LogToErrorBox("Template Compatibility Issue Detected:", "Error")
                    End Try

                    ' Process results only if verification was successful
                    If verificationSuccessful Then
                        ' Simplified debugging information
                        Dim currentFingerName As String = selectedFingerName & " - " & selectedEmployeeName
                        LogToErrorBox("Verification Result for " & currentFingerName & ":", "Success")
                        lblstatusimage.Image = Img.Images(1)
                    Else
                        Dim currentFingerName As String = selectedFingerName & " - " & selectedEmployeeName
                        LogToErrorBox("Verification Result for " & currentFingerName & ":", "Failed")
                        lblstatusimage.Image = Img.Images(0)
                    End If

                Catch dpfpEx As Exception When dpfpEx.Message.Contains("local")
                    LogToErrorBox("DPFP Local Error: " & dpfpEx.Message, "Error")
                    LogToErrorBox("Template Compatibility Issue Detected:", "Error")
                    LogToErrorBox("- This template was created with a different DPFP configuration", "Info")
                    LogToErrorBox("- Template may need re-registration for compatibility", "Info")
                    LogToErrorBox("- Attempting template refresh...", "Info")

                    ' Try to refresh the template from server with better compatibility
                    Try
                        MessageBox.Show("Template compatibility issue detected. Please re-register the fingerprint.", "Template Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        StopVerification()
                    Catch refreshEx As Exception
                        LogToErrorBox("Template refresh failed: " & refreshEx.Message, "Error")
                        MessageBox.Show("Template compatibility issue detected. Please re-register the fingerprint.", "Template Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        StopVerification()
                    End Try
                Catch dpfpEx As Exception When dpfpEx.Message.Contains("0xFFFFFFFE")
                    LogToErrorBox("DPFP Sample Quality Error (HRESULT: 0xFFFFFFFE): " & dpfpEx.Message, "Error")
                    LogToErrorBox("This error typically indicates:", "Info")
                    LogToErrorBox("1. Sample quality too poor for verification", "Info")
                    LogToErrorBox("2. Finger placement or pressure issues", "Info")
                    LogToErrorBox("3. Scanner surface needs cleaning", "Info")
                    UpdateStatus("Sample quality too poor. Please clean finger and scanner, then try again...")

                    ' Show quality improvement tips after a few failures
                    MessageBox.Show("Sample quality too poor. Please clean your finger and scanner surface, then try again.", "Poor Quality", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Catch dpfpEx As Exception
                    LogToErrorBox("DPFP General Error: " & dpfpEx.Message, "Error")
                    MessageBox.Show("Verification error: " & dpfpEx.Message, "Verification Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    StopVerification()
                End Try
            Else
                LogToErrorBox("Feature extraction failed - poor quality sample", "Error")
                UpdateStatus("Poor quality sample for verification. Please clean finger and try again...")
            End If

        Catch ex As Exception
            MessageBox.Show("Error during verification: " & ex.Message & vbCrLf &
                          "Error Type: " & ex.GetType().Name & vbCrLf &
                          "Please restart the application if this persists.",
                          "Verification Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            lblstatusimage.Image = Img.Images(0)
            StopVerification()
        End Try
    End Sub


    Private Sub StopVerification()
        Try
            isVerifying = False
            storedTemplateForVerification = Nothing
            StopCapture()
        Catch ex As Exception
            MessageBox.Show("Error stopping verification: " & ex.Message)
        End Try
    End Sub

    Private Sub ReinitializeDPFPComponents()
        Try
            UpdateStatus("Reinitializing fingerprint components...")

            ' Stop any active capture first
            isCapturing = False
            isVerifying = False

            ' Clean up existing components with proper delay
            If Capturer IsNot Nothing Then
                Try
                    If isCapturing Then
                        Capturer.StopCapture()
                        Threading.Thread.Sleep(500) ' Give time for device to stop
                    End If
                    Capturer.Dispose()
                    Threading.Thread.Sleep(200) ' Additional delay for cleanup
                Catch ex As Exception
                    ' Log but ignore disposal errors
                    LogToErrorBox("Capturer disposal error: " & ex.Message, "Error")
                End Try
                Capturer = Nothing
            End If

            If Enroller IsNot Nothing Then
                Enroller = Nothing
            End If

            If Verifier IsNot Nothing Then
                Verifier = Nothing
            End If

            ' Reset all states
            isCapturing = False
            isVerifying = False
            storedTemplateForVerification = Nothing
            fingerprintCount = 0

            ' Force garbage collection to ensure proper cleanup
            GC.Collect()
            GC.WaitForPendingFinalizers()

            ' Additional delay before allowing new operations
            Threading.Thread.Sleep(300)

            ' Update UI
            btnstartCapture.Text = "Start Capture"
            UpdateStatus("DPFP components reinitialized. Device ready for new operations.")

        Catch ex As Exception
            MessageBox.Show("Error reinitializing DPFP components: " & ex.Message & vbCrLf &
                          "Please restart the application.", "Reinitialization Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Recreates a template in the current DPFP context to resolve "Expecting object to be local" errors
    ''' </summary>
    Private Function RecreateTemplateInCurrentContext(originalTemplate As DPFP.Template) As DPFP.Template
        Try
            If originalTemplate Is Nothing OrElse originalTemplate.Size = 0 Then
                LogToErrorBox("Cannot recreate template: Original template is null or empty", "Error")
                Return Nothing
            End If

            ' Serialize the original template to bytes
            Dim templateBytes() As Byte = Nothing
            Using stream As New IO.MemoryStream()
                originalTemplate.Serialize(stream)
                templateBytes = stream.ToArray()
            End Using

            If templateBytes Is Nothing OrElse templateBytes.Length = 0 Then
                LogToErrorBox("Failed to serialize original template", "Error")
                Return Nothing
            End If

            ' Create a new template instance in current context
            Dim newTemplate As New DPFP.Template()
            Using stream As New IO.MemoryStream(templateBytes)
                newTemplate.DeSerialize(stream)
            End Using

            ' Validate the recreated template
            If newTemplate.Size > 0 Then
                LogToErrorBox("Template successfully recreated in current context. Size: " & newTemplate.Size, "Success")
                Return newTemplate
            Else
                LogToErrorBox("Recreated template has zero size", "Error")
                Return Nothing
            End If

        Catch ex As Exception
            LogToErrorBox("Error recreating template in current context: " & ex.Message, "Error")
            Return Nothing
        End Try
    End Function

#End Region
#Region "DeleteFinger"
    Private Async Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Dim EmpId As Integer = 0
            Dim EmpName As String = "'"
            If GridViewEmpFingerDtl.RowCount > 0 Then
                EmpId = GridViewEmpFingerDtl.GetFocusedRowCellValue("id")
                EmpName = GridViewEmpFingerDtl.GetFocusedRowCellValue("emp_printname")
            End If
            ' Check if an employee is selected
            If EmpId = 0 Then
                MessageBox.Show("Please select an employee first.", "No Employee Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Confirm deletion
            Dim result As DialogResult = MessageBox.Show(
                "Are you sure you want to delete all fingerprint data for " & EmpName & "?" & vbCrLf & vbCrLf &
                "This action cannot be undone.",
                "Confirm Delete Fingerprints",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

            If result <> DialogResult.Yes Then
                Return
            End If

            ' Prepare delete request - using only EmpId since we want to delete all fingerprints for the employee
            Dim postData As String = "{""EmpId"":" & EmpId & "}"

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/json"

                Dim response As String = Await Task.Run(Function()
                                                            Return client.UploadString(M_Details.LinkAjaxRequest & "AttRequest=4", postData)
                                                        End Function)

                Dim parsedResponse As JObject = JObject.Parse(response)
                Dim success As Boolean = parsedResponse("Success").ToString().ToLower() = "true"

                If success Then
                    MessageBox.Show("Fingerprint data deleted successfully for " & selectedEmployeeName & "!", "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Refresh the fingerprint display
                    LoadExistingFingerprints()

                    ' Clear status image
                    lblstatusimage.Image = Nothing

                    ' Reset enrollment state
                    ResetEnrollment()
                Else
                    Dim errorMsg As String = "Failed to delete fingerprint data."
                    If parsedResponse("Msg") IsNot Nothing Then
                        errorMsg = parsedResponse("Msg").ToString()
                    End If
                    MessageBox.Show(errorMsg, "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Error deleting fingerprint: " & ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            ResetEnrollment()
        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class
Public Class Fingerprint
    Public Property FingerId As Integer
    Public Property UserId As Integer
    Public Property FingerIndex As Integer
    Public Property Template As Byte()
    Public Property CreatedAt As DateTime
End Class
'Imports DPUruNet
'Imports System.Net
'Imports Newtonsoft.Json.Linq

'Public Class FrmFingerRegister
'    Private salesmenTable As DataTable
'    Private registeredFingerprints As DataTable
'    Private selectedEmployeeId As Integer = 0 ' Track selected employee ID
'    Private selectedEmployeeName As String = "" ' Track selected employee name
'    Private selectedFingerType As String = ""
'#Region "Error Log"
'    'Logging method to display messages in RichTextBox
'    Private Sub LogMessage(message As String)
'        Try
'            If errorRichBox IsNot Nothing Then
'                If errorRichBox.InvokeRequired Then
'                    errorRichBox.Invoke(Sub()
'                                            Dim timestamp As String = DateTime.Now.ToString("HH:mm:ss.fff")
'                                            errorRichBox.AppendText("[" & timestamp & "] " & message & vbCrLf)
'                                            errorRichBox.ScrollToCaret()
'                                        End Sub)
'                Else
'                    Dim timestamp As String = DateTime.Now.ToString("HH:mm:ss.fff")
'                    errorRichBox.AppendText("[" & timestamp & "] " & message & vbCrLf)
'                    errorRichBox.ScrollToCaret()
'                End If
'            End If
'        Catch ex As Exception
'            ' Fallback to debug output if RichTextBox fails
'            System.Diagnostics.Debug.WriteLine("Log Error: " & ex.Message & " - Original: " & message)
'        End Try
'    End Sub

'    ''' <summary>
'    ''' Logs messages to the error RichTextBox with color coding and timestamps
'    ''' </summary>
'    ''' <param name="message">The message to log</param>
'    ''' <param name="messageType">The type of message: "Error", "Info", "Success"</param>
'    Private Sub LogToErrorBox(message As String, messageType As String)
'        Try
'            If errorRichBox IsNot Nothing Then
'                If errorRichBox.InvokeRequired Then
'                    errorRichBox.Invoke(Sub() WriteToErrorBox(message, messageType))
'                Else
'                    WriteToErrorBox(message, messageType)
'                End If
'            End If
'        Catch ex As Exception
'            ' Fallback to debug output if RichTextBox fails
'            System.Diagnostics.Debug.WriteLine("LogToErrorBox Error: " & ex.Message & " - Original: " & message)
'        End Try
'    End Sub

'    ''' <summary>
'    ''' Helper method to write to the error RichTextBox with formatting
'    ''' </summary>
'    Private Sub WriteToErrorBox(message As String, messageType As String)
'        Try
'            Dim timestamp As String = DateTime.Now.ToString("HH:mm:ss.fff")
'            Dim fullMessage As String = "[" & timestamp & "] " & message & vbCrLf

'            ' Set color based on message type
'            Dim color As Color = color.Black
'            Select Case messageType.ToUpper()
'                Case "ERROR"
'                    color = color.Red
'                Case "INFO"
'                    color = color.Blue
'                Case "SUCCESS"
'                    color = color.Green
'                Case Else
'                    color = color.Black
'            End Select

'            ' Add colored text
'            errorRichBox.SelectionStart = errorRichBox.TextLength
'            errorRichBox.SelectionLength = 0
'            errorRichBox.SelectionColor = color
'            errorRichBox.AppendText(fullMessage)
'            errorRichBox.SelectionColor = errorRichBox.ForeColor ' Reset to default color

'            ' Auto-scroll to bottom
'            errorRichBox.ScrollToCaret()

'            ' Limit text length to prevent memory issues (keep last 10000 characters)
'            If errorRichBox.TextLength > 10000 Then
'                Dim textToKeep As String = errorRichBox.Text.Substring(errorRichBox.TextLength - 8000)
'                errorRichBox.Clear()
'                errorRichBox.AppendText("... (previous messages truncated) ..." & vbCrLf & textToKeep)
'                errorRichBox.ScrollToCaret()
'            End If

'        Catch ex As Exception
'            ' Last resort fallback
'            System.Diagnostics.Debug.WriteLine("WriteToErrorBox Error: " & ex.Message)
'        End Try
'    End Sub
'#End Region
'#Region "InitalLoad"

'    Private Sub SetupDataTables()
'        ' Setup Salesmen DataTable
'        salesmenTable = New DataTable()
'        salesmenTable.Columns.Add("emp_id", GetType(Integer))
'        salesmenTable.Columns.Add("emp_printname", GetType(String))
'        salesmenTable.Columns.Add("emp_type", GetType(String))
'        GridControlEmpHeader.DataSource = salesmenTable

'        ' Setup Registered Fingerprints DataTable
'        registeredFingerprints = New DataTable()
'        registeredFingerprints.Columns.Add("id", GetType(Integer))
'        registeredFingerprints.Columns.Add("emp_id", GetType(Integer))
'        registeredFingerprints.Columns.Add("emp_printname", GetType(String))
'        registeredFingerprints.Columns.Add("finger_name", GetType(String))
'        registeredFingerprints.Columns.Add("fingertype", GetType(String))
'        registeredFingerprints.Columns.Add("created_at", GetType(DateTime))
'        GridControlEmpFingerHeader.DataSource = registeredFingerprints

'    End Sub

'    Private Sub LoadExistingFingerprints()
'        Try
'            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "AttRequest=3")
'            Dim parsedJson As JObject = JObject.Parse(json)

'            registeredFingerprints.Clear()
'            If parsedJson("Success").ToString() = "True" Then
'                Dim dataArray = parsedJson("Data")


'                For Each item In dataArray
'                    Dim Id As Integer = 0
'                    Dim EmpId As Integer = 0
'                    Dim EmpName As String = ""
'                    Dim FingerName As String = ""
'                    Dim FingerType As String = ""
'                    Id = item("id")
'                    EmpId = item("emp_id")
'                    EmpName = item("emp_printname")
'                    FingerName = item("finger_name")
'                    FingerType = item("fingertype")
'                    registeredFingerprints.Rows.Add(Id, EmpId, EmpName, FingerName, FingerType)
'                Next
'            End If

'        Catch ex As Exception
'            ' Handle errors silently for now
'        End Try
'    End Sub


'    Private Function GetEmployeeNameById(empId As Integer) As String
'        Try
'            For Each row As DataRow In salesmenTable.Rows
'                If Convert.ToInt32(row("emp_id")) = empId Then
'                    Return row("emp_printname").ToString()
'                End If
'            Next
'            Return "Unknown Employee"
'        Catch ex As Exception
'            Return "Error Loading Name"
'        End Try
'    End Function

'    Private Sub LoadSalesmen()
'        Try
'            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=1")
'            Dim parsedJson As JObject = JObject.Parse(json)

'            salesmenTable.Clear()
'            If parsedJson("Success").ToString() = "True" Then
'                Dim dataArray = parsedJson("Data")


'                For Each item In dataArray
'                    Dim empId As Integer = 0
'                    Dim empName As String = ""
'                    Dim empType As String = ""
'                    empType = "Employee"
'                    ' Try multiple possible field name variations for emp_id
'                    If item("emp_id") IsNot Nothing AndAlso Not IsDBNull(item("emp_id")) Then
'                        empId = Convert.ToInt32(item("emp_id"))
'                    ElseIf item("EmpId") IsNot Nothing AndAlso Not IsDBNull(item("EmpId")) Then
'                        empId = Convert.ToInt32(item("EmpId"))
'                    ElseIf item("empid") IsNot Nothing AndAlso Not IsDBNull(item("empid")) Then
'                        empId = Convert.ToInt32(item("empid"))
'                    End If

'                    ' Try multiple possible field name variations for employee name
'                    If item("emp_printname") IsNot Nothing AndAlso item("emp_printname").ToString() <> "" Then
'                        empName = item("emp_printname").ToString()
'                    ElseIf item("EmpPrintName") IsNot Nothing AndAlso item("EmpPrintName").ToString() <> "" Then
'                        empName = item("EmpPrintName").ToString()
'                    ElseIf item("emp_firstname") IsNot Nothing AndAlso item("emp_firstname").ToString() <> "" Then
'                        empName = item("emp_firstname").ToString()
'                    ElseIf item("EmpFirstName") IsNot Nothing AndAlso item("EmpFirstName").ToString() <> "" Then
'                        empName = item("EmpFirstName").ToString()
'                    ElseIf item("emp_name") IsNot Nothing AndAlso item("emp_name").ToString() <> "" Then
'                        empName = item("emp_name").ToString()
'                    ElseIf item("EmpName") IsNot Nothing AndAlso item("EmpName").ToString() <> "" Then
'                        empName = item("EmpName").ToString()
'                    End If

'                    salesmenTable.Rows.Add(empId, empName, empType)
'                Next
'                If _JsonData.UserTable.Rows.Count > 0 Then
'                    For Each rs In _JsonData.UserTable.Rows
'                        Dim Id As Integer = 0
'                        Dim UserName As String = ""
'                        Dim Type As String = "User"
'                        Id = rs("Id")
'                        UserName = rs("UserName")
'                        salesmenTable.Rows.Add(Id, UserName, Type)
'                    Next
'                End If

'            Else
'                MessageBox.Show("Failed to load salesmen: " & parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
'            End If

'            ' Refresh the Salesmen GridControl
'            GridControlEmpHeader.RefreshDataSource()
'            GridControlEmpHeader.Refresh()
'        Catch ex As Exception
'            MessageBox.Show("Error loading salesmen: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End Try
'    End Sub

'    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
'        Try
'            LoadSalesmen()
'            LoadExistingFingerprints()
'        Catch ex As Exception

'        End Try
'    End Sub

'    Private Sub FrmFingerRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        Try
'            SetupDataTables()
'            LoadSalesmen()
'            LoadExistingFingerprints()
'        Catch ex As Exception

'        End Try
'    End Sub


'#End Region
'#Region "ScanFingerPrintModule"
'    Private Sub GridViewEmpHeader_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridViewEmpHeader.RowClick
'        Try
'            Dim foucustedRow As Integer = 0
'            foucustedRow = GridViewEmpHeader.FocusedRowHandle
'            Dim empId = GridViewEmpHeader.GetFocusedRowCellValue("emp_id")
'            Dim empName = GridViewEmpHeader.GetFocusedRowCellValue("emp_printname")
'            Dim empType = GridViewEmpHeader.GetFocusedRowCellValue("emp_type")
'            lblempid.Text = empId
'            lblempname.Text = empName
'            lbltype.Text = empType

'            ' Update new tracking variables
'            selectedEmployeeId = Convert.ToInt32(empId)
'            selectedEmployeeName = empName.ToString()
'            selectedFingerType = empType
'            ' Enable enrollment button
'            btnstartCapture.Enabled = True

'        Catch ex As Exception
'            MessageBox.Show("Error selecting employee: " & ex.Message)
'        End Try
'    End Sub

'    Private Sub UpdateStatus(message As String)
'        ' Add a status label or use existing UI element to show current status
'        ' You might want to add a Label control for this
'        Me.Text = "Fingerprint Registration - " & message
'    End Sub

'#End Region

'    Private Sub btnstartCapture_Click(sender As Object, e As EventArgs) Handles btnstartCapture.Click
'        Try
'            If selectedEmployeeId = 0 Then
'                MessageBox.Show("Please select an employee first.", "No Employee Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
'                Return
'            End If
'            EnrollmentControl.ShowDailogData(selectedEmployeeId, lbltype.Text)
'            StoreFmdFile(selectedEmployeeId)
'        Catch ex As Exception

'        End Try
'    End Sub

'    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
'        Try
'            If selectedEmployeeId = 0 Then
'                MessageBox.Show("Please select an employee first.", "No Employee Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
'                Return
'            End If
'            IdentificationControl.ShowDailogData(selectedEmployeeId)
'        Catch ex As Exception

'        End Try
'    End Sub
'End Class
