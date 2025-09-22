Imports System.Net
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports DPFP
Imports DPFP.Capture
Imports DPFP.Processing
Imports DPFP.Verification
Imports System.IO

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

    ' Multi-finger verification properties
    Private availableTemplates As List(Of DPFP.Template) ' Store all available templates for verification
    Private currentTemplateIndex As Integer = 0 ' Current template being verified
    Private fingerNames As List(Of String) ' Store corresponding finger names
    Private maxVerificationAttempts As Integer = 3 ' Max attempts per finger

    ' Enhanced verification settings
    Private verificationAttempts As Integer = 0 ' Current attempts for current finger
    Private ReadOnly customFARThreshold As Double = 0.001 ' More lenient FAR threshold (increased from 0.0001)
    Private ReadOnly maxAttemptsPerFinger As Integer = 5 ' Max attempts before switching finger

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
        GridControlEmpFingerHeader.DataSource = registeredFingerprints

    End Sub

    Private Sub LoadExistingFingerprints()
        Try
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "AttRequest=3")
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

                    salesmenTable.Rows.Add(empId, empName, empType)
                Next
                If _JsonData.UserTable.Rows.Count > 0 Then
                    For Each rs In _JsonData.UserTable.Rows
                        Dim Id As Integer = 0
                        Dim UserName As String = ""
                        Dim Type As String = "User"
                        Id = rs("Id")
                        UserName = rs("UserName")
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
#Region "ScanFingerPrintModule"
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
            ' Enable enrollment button
            btnstartCapture.Enabled = True

            ' Reset enrollment when selecting new employee
            ResetEnrollment()
        Catch ex As Exception
            MessageBox.Show("Error selecting employee: " & ex.Message)
        End Try
    End Sub

    Private Sub ResetEnrollment()
        Try
            ' Only reset enrollment-specific variables, not verification state
            fingerprintCount = 0
            qualityAttempts = 0

            ' Reset enrollment object only if not in verification mode
            If Not isVerifying Then
                If Enroller IsNot Nothing Then
                    Enroller = Nothing
                End If
                Enroller = New Enrollment()
                cmbFingerName.SelectedIndex = 0
                ' Update UI to show progress
                UpdateStatus("Employee selected. Ready to capture fingerprint. Please ensure finger is clean and dry.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error resetting enrollment: " & ex.Message)
        End Try
    End Sub

    Private Sub UpdateStatus(message As String)
        ' Add a status label or use existing UI element to show current status
        ' You might want to add a Label control for this
        Me.Text = "Fingerprint Registration - " & message
    End Sub

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
    Private Function ConvertSampleToBitmap(Sample As Sample) As Bitmap
        Dim convertor As New DPFP.Capture.SampleConversion()
        Dim bitmap As Bitmap = Nothing
        convertor.ConvertToPicture(Sample, bitmap)
        Return bitmap
    End Function
    Private Sub ProcessSample(Sample As Sample)
        Try
            ' Check if we're in verification mode
            If isVerifying Then
                VerifyFingerprint(Sample)
                Return
            End If

            ' Normal enrollment processing
            Dim features As DPFP.FeatureSet = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Enrollment)

            If features IsNot Nothing Then
                If Enroller Is Nothing Then
                    Enroller = New Enrollment()
                End If

                ' Try to add features to enrollment
                Try
                    ' Store previous features needed count for comparison
                    Dim previousFeaturesNeeded As Integer = Enroller.FeaturesNeeded

                    ' Add features to enrollment
                    Enroller.AddFeatures(features)

                    ' Check if features were successfully added by comparing features needed count
                    If Enroller.TemplateStatus <> Enrollment.Status.Failed AndAlso
                       (Enroller.TemplateStatus = Enrollment.Status.Ready OrElse
                        Enroller.FeaturesNeeded < previousFeaturesNeeded) Then

                        ' Features were added successfully
                        fingerprintCount += 1
                        UpdateStatus("Sample " & fingerprintCount.ToString() & " of " & maxFingerprints.ToString() & " captured successfully.")
                    Else
                        ' Features were not added (poor quality or duplicate)
                        UpdateStatus("Poor quality sample detected. Please try again with better finger placement.")
                        Return ' Don't increment count, try again
                    End If

                    ' Check enrollment status after adding features
                    Select Case Enroller.TemplateStatus
                        Case Enrollment.Status.Ready
                            ' Template is ready - save to database
                            UpdateStatus("Fingerprint template created successfully!")
                            SaveFingerprintTemplate()

                        Case Enrollment.Status.Failed
                            MessageBox.Show("Enrollment failed due to poor quality samples. Please clean your finger and start over.", "Enrollment Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            ResetEnrollment()

                        Case Else
                            ' Need more samples
                            If fingerprintCount < maxFingerprints Then
                                UpdateStatus("Sample " & fingerprintCount.ToString() & " captured. Please place finger again for sample " & (fingerprintCount + 1).ToString() & ".")
                            Else
                                ' We have max samples but still need more features
                                Dim result As DialogResult = MessageBox.Show(
                                    "Unable to create a reliable template with current samples. This could be due to:" & vbCrLf &
                                    "• Dry or damaged finger" & vbCrLf &
                                    "• Inconsistent finger placement" & vbCrLf &
                                    "• Scanner surface needs cleaning" & vbCrLf & vbCrLf &
                                    "Would you like to try again with a different finger or retry with the same finger?",
                                    "Enrollment Retry Required",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question)

                                If result = DialogResult.Yes Then
                                    ResetEnrollment()
                                    UpdateStatus("Ready to start enrollment again. Please ensure finger is clean and dry.")
                                Else
                                    StopCapture()
                                    UpdateStatus("Enrollment cancelled.")
                                End If
                            End If
                    End Select

                Catch enrollEx As Exception
                    MessageBox.Show("Error adding fingerprint features: " & enrollEx.Message & vbCrLf & "Please try again.", "Enrollment Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ResetEnrollment()
                End Try
            Else
                UpdateStatus("Poor quality sample detected. Please clean your finger and scanner, then try again.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error processing sample: " & ex.Message, "Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

                    ' Important: Reset all DPFP states to allow immediate verification
                    ReinitializeDPFPComponents()

                    ' Auto-prompt for verification test
                    Dim testResult As DialogResult = MessageBox.Show("Fingerprint registration successful!" & vbCrLf & vbCrLf &
                                                                   "Would you like to test the verification now?",
                                                                   "Test Verification",
                                                                   MessageBoxButtons.YesNo,
                                                                   MessageBoxIcon.Question)
                    If testResult = DialogResult.Yes Then
                        ' Use the template that was just registered for immediate verification
                        ' This avoids potential server timing issues
                        TestImmediateVerificationWithCurrentTemplate(template)
                    End If
                Else
                    MessageBox.Show("Failed to save fingerprint to server. Please try again.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error saving fingerprint template: " & ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Tests verification immediately after registration using the just-registered template
    ''' This avoids potential server timing issues by using the template from memory
    ''' </summary>
    Private Sub TestImmediateVerificationWithCurrentTemplate(registeredTemplate As DPFP.Template)
        Try
            LogToErrorBox("Starting immediate verification test with registered template...", "Info")

            ' Ensure we have a valid template
            If registeredTemplate Is Nothing OrElse registeredTemplate.Size = 0 Then
                MessageBox.Show("Invalid template for immediate verification test.", "Verification Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            LogToErrorBox("Using registered template for immediate verification. Size: " & registeredTemplate.Size, "Info")

            ' Small delay to ensure DPFP components are ready after reinitialization
            Threading.Thread.Sleep(1000)

            ' Create a list with the single template for verification
            Dim templateList As New List(Of DPFP.Template)()
            templateList.Add(registeredTemplate)

            ' Set up finger names for this template
            fingerNames = New List(Of String)()
            fingerNames.Add(selectedFingerName)

            UpdateStatus("Ready for immediate verification test. Please place the same finger you just registered...")

            ' Start verification with the registered template
            StartMultiFingerVerificationCapture(templateList)

        Catch ex As Exception
            LogToErrorBox("Error in immediate verification test: " & ex.Message, "Error")
            MessageBox.Show("Error starting immediate verification test: " & ex.Message, "Verification Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

            ' Add checksum for integrity validation
            Dim templateChecksum As String = CalculateMD5Hash(templateBase64)

            Dim postData As String = "{""EmpId"":" & empId.ToString() & ",""Template"":""" & templateBase64 & """,""FingerType"":""" & fingerType & """,""FingerName"":""" & fingerName & """,""Checksum"":""" & templateChecksum & """}"

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

    ' Helper function to calculate MD5 hash for integrity validation
    Private Function CalculateMD5Hash(input As String) As String
        Try
            Using md5 As System.Security.Cryptography.MD5 = System.Security.Cryptography.MD5.Create()
                Dim inputBytes() As Byte = System.Text.Encoding.UTF8.GetBytes(input)
                Dim hashBytes() As Byte = md5.ComputeHash(inputBytes)
                Return BitConverter.ToString(hashBytes).Replace("-", "").ToLower()
            End Using
        Catch
            Return ""
        End Try
    End Function

    Private Function ExtractFeatures(Sample As Sample, purpose As DPFP.Processing.DataPurpose) As DPFP.FeatureSet
        Try
            Dim extractor As New DPFP.Processing.FeatureExtraction()
            Dim feedback As DPFP.Capture.CaptureFeedback = Nothing
            Dim features As New DPFP.FeatureSet()

            ' Extract features from the sample
            extractor.CreateFeatureSet(Sample, purpose, feedback, features)

            ' Check the quality feedback
            Select Case feedback
                Case DPFP.Capture.CaptureFeedback.Good
                    ' Good quality - return the features
                    Return features

                Case DPFP.Capture.CaptureFeedback.NoFinger
                    UpdateStatus("No finger detected. Please place finger on scanner.")
                    Return Nothing

                Case DPFP.Capture.CaptureFeedback.TooLight
                    UpdateStatus("Press harder on the scanner.")
                    Return Nothing

                Case DPFP.Capture.CaptureFeedback.TooDark
                    UpdateStatus("Press lighter on the scanner.")
                    Return Nothing

                Case DPFP.Capture.CaptureFeedback.TooLeft, DPFP.Capture.CaptureFeedback.TooRight,
                     DPFP.Capture.CaptureFeedback.TooHigh, DPFP.Capture.CaptureFeedback.TooLow
                    UpdateStatus("Center your finger on the scanner.")
                    Return Nothing

                Case DPFP.Capture.CaptureFeedback.TooFast
                    UpdateStatus("Place finger more slowly and hold steady.")
                    Return Nothing

                Case DPFP.Capture.CaptureFeedback.TooSlow
                    UpdateStatus("Place finger more quickly on the scanner.")
                    Return Nothing

                Case DPFP.Capture.CaptureFeedback.TooSkewed
                    UpdateStatus("Place finger straight on the scanner.")
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

    Private Async Sub GetFingerprintDetailsAndVerify()
        Try
            UpdateStatus("Retrieving all fingerprint templates for verification...")

            ' Get all stored fingerprint templates from server for multi-finger verification
            Dim templates As List(Of DPFP.Template) = Await GetAllFingerprintTemplatesFromServer(selectedEmpId)

            If templates IsNot Nothing AndAlso templates.Count > 0 Then
                ' Validate templates before starting verification
                Dim validTemplates As Integer = 0
                For Each template In templates
                    If template.Size > 0 Then
                        validTemplates += 1
                    End If
                Next

                If validTemplates > 0 Then
                    UpdateStatus("Found " & validTemplates & " fingerprint template(s). Please place your finger for verification...")
                    ' Start multi-finger verification process
                    StartMultiFingerVerificationCapture(templates)
                Else
                    MessageBox.Show("Retrieved templates are invalid (zero size). Please re-register the fingerprints.",
                                  "Invalid Templates", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    lblstatusimage.Image = Img.Images(0)
                End If
            Else
                MessageBox.Show("No fingerprint templates found for " & lblempname.Text & ". Please register fingerprint first.", "No Templates Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
                LogToErrorBox("Raw server response: " & response, "Info")

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

                            ' Create template with proper initialization
                            Dim template As New DPFP.Template()
                            Using stream As New IO.MemoryStream(templateBytes)
                                Try
                                    template.DeSerialize(stream)

                                    ' Verify template is valid before returning
                                    If template.Size > 0 Then
                                        LogToErrorBox("Template deserialized successfully. DPFP Size: " & template.Size, "Success")
                                        Return template
                                    Else
                                        LogToErrorBox("Template deserialized but has zero size", "Error")
                                        MessageBox.Show("Invalid fingerprint template retrieved from server. Please re-register the fingerprint.",
                                                      "Invalid Template", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                        Return Nothing
                                    End If
                                Catch deserializeEx As System.Runtime.InteropServices.COMException
                                    LogToErrorBox("Template deserialization COM error: " & deserializeEx.Message, "Error")
                                    MessageBox.Show("Corrupted fingerprint template. Please re-register the fingerprint." & vbCrLf &
                                                  "Error: " & deserializeEx.Message,
                                                  "Template Corruption", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Return Nothing
                                Catch deserializeEx As Exception
                                    LogToErrorBox("Template deserialization error: " & deserializeEx.Message, "Error")
                                    MessageBox.Show("Failed to load fingerprint template. Please re-register the fingerprint." & vbCrLf &
                                                  "Error: " & deserializeEx.Message,
                                                  "Template Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Return Nothing
                                End Try
                            End Using

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

    Private Async Function GetAllFingerprintTemplatesFromServer(empId As Integer) As Task(Of List(Of DPFP.Template))
        Try
            availableTemplates = New List(Of DPFP.Template)()
            fingerNames = New List(Of String)()

            Using client As New WebClient()
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")

                ' Get all fingerprints for this employee
                Dim postData As String = "AttRequest=3&emp_id=" & empId.ToString() & "&fingertype=" & selectedFingerType
                Dim response As String = client.UploadString(M_Details.LinkAjaxRequest.Replace("getfunctionmgmt.php", "getfunctionmgmt.php"), postData)

                If Not String.IsNullOrEmpty(response) AndAlso response <> "no_data" Then
                    ' Parse JSON response to get fingerprint data
                    Dim parsedResponse As JObject = JObject.Parse(response)

                    If parsedResponse("Success").ToString().ToLower() = "true" Then
                        Dim dataArray = parsedResponse("Data")

                        For Each fingerRecord In dataArray
                            Dim fingerId As String = fingerRecord("id").ToString()
                            Dim fingerName As String = fingerRecord("finger_name").ToString()

                            ' Get the actual template for this finger
                            Dim template As DPFP.Template = Await GetSpecificFingerprintTemplate(empId, fingerName, selectedFingerType)

                            If template IsNot Nothing AndAlso template.Size > 0 Then
                                availableTemplates.Add(template)
                                fingerNames.Add(fingerName)
                                LogToErrorBox("Added template for " & fingerName & " (Size: " & template.Size & ")", "Success")
                            End If
                        Next
                    End If
                End If

                Return availableTemplates

            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving all fingerprint templates: " & ex.Message, "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return New List(Of DPFP.Template)()
        End Try
    End Function

    Private Async Function GetSpecificFingerprintTemplate(empId As Integer, fingerName As String, fingerType As String) As Task(Of DPFP.Template)
        Try
            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/json"

                Dim postData As String = "{""EmpId"":" & empId.ToString() & ",""FingerName"":""" & fingerName & """,""FingerType"":""" & fingerType & """}"
                Dim response As String = Await Task.Run(Function()
                                                            Return client.UploadString(M_Details.LinkAjaxRequest & "AttRequest=2", postData)
                                                        End Function)

                ' Validate response
                If String.IsNullOrEmpty(response) Then
                    LogToErrorBox("Empty response for " & fingerName, "Error")
                    Return Nothing
                End If

                ' Parse response
                response = response.Trim()
                If response.Contains("}{") Then
                    Dim firstBrace As Integer = response.IndexOf("}")
                    If firstBrace > 0 Then
                        response = response.Substring(0, firstBrace + 1)
                    End If
                End If

                Dim parsedResponse As JObject = JObject.Parse(response)

                If parsedResponse("Success").ToString().ToLower() = "true" Then
                    Dim templateBase64 As String = parsedResponse("Template").ToString()
                    If Not String.IsNullOrEmpty(templateBase64) Then
                        Try
                            ' Enhanced validation for specific fingerprint template
                            Dim templateBytes() As Byte = Convert.FromBase64String(templateBase64)

                            ' Validate template data size
                            If templateBytes Is Nothing OrElse templateBytes.Length < 100 Then
                                Dim templateSize As Integer = If(templateBytes IsNot Nothing, templateBytes.Length, 0)
                                LogToErrorBox("Invalid template size for " & fingerName & ": " & templateSize & " bytes", "Error")
                                Return Nothing
                            End If

                            Dim template As New DPFP.Template()
                            Using stream As New IO.MemoryStream(templateBytes)
                                Try
                                    template.DeSerialize(stream)

                                    ' Validate deserialized template
                                    If template.Size > 0 Then
                                        LogToErrorBox("Successfully loaded template for " & fingerName & " (DPFP Size: " & template.Size & ")", "Success")
                                        Return template
                                    Else
                                        LogToErrorBox("Template deserialized for " & fingerName & " but has zero size", "Error")
                                        Return Nothing
                                    End If
                                Catch deserializeEx As Exception
                                    LogToErrorBox("Deserialization error for " & fingerName & ": " & deserializeEx.Message, "Error")
                                    Return Nothing
                                End Try
                            End Using
                        Catch b64Ex As Exception
                            LogToErrorBox("Base64 decode error for " & fingerName & ": " & b64Ex.Message, "Error")
                            Return Nothing
                        End Try
                    Else
                        LogToErrorBox("Empty template data for " & fingerName, "Error")
                        Return Nothing
                    End If
                Else
                    Dim errorMsg As String = "Unknown error"
                    If parsedResponse("Msg") IsNot Nothing Then
                        errorMsg = parsedResponse("Msg").ToString()
                    End If
                    LogToErrorBox("Server error for " & fingerName & ": " & errorMsg, "Error")
                    Return Nothing
                End If

                Return Nothing
            End Using
        Catch ex As Exception
            LogToErrorBox("Error getting specific template for " & fingerName & ": " & ex.Message, "Error")
            Return Nothing
        End Try
    End Function

    Private storedTemplateForVerification As DPFP.Template = Nothing
    Private isVerifying As Boolean = False

    Private Sub StartMultiFingerVerificationCapture(templates As List(Of DPFP.Template))
        Try
            ' Ensure we're not in capture mode first
            If isCapturing Then
                StopCapture()
                Threading.Thread.Sleep(500)
            End If

            ' Store templates for multi-finger verification
            availableTemplates = templates
            currentTemplateIndex = 0
            verificationAttempts = 0 ' Initialize attempts counter

            ' Initialize verifier with fresh instance
            Verifier = New Verification()

            ' Force reinitialize capturer for verification
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
                UpdateStatus("Initializing device for multi-finger verification...")
                Capturer = New Capture()
                Capturer.EventHandler = Me
                Threading.Thread.Sleep(300)

                UpdateStatus("Device initialized. Starting verification - trying " & availableTemplates.Count & " fingerprint(s)...")
                Capturer.StartCapture()
                isCapturing = True
                isVerifying = True
                btnstartCapture.Text = "Stop Verification"

                ' Set the first template for verification
                If availableTemplates.Count > 0 Then
                    storedTemplateForVerification = availableTemplates(0)
                    UpdateStatus("Place finger for verification (Finger: " & fingerNames(0) & ")...")
                End If

                lblstatusimage.Image = Nothing

            Catch dpfpEx As System.Runtime.InteropServices.COMException
                MessageBox.Show("Device communication error during verification startup:" & vbCrLf & vbCrLf &
                              "Error: " & dpfpEx.Message, "Device Communication Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                isVerifying = False
                lblstatusimage.Image = Img.Images(0)
                ReinitializeDPFPComponents()
            Catch captureEx As Exception
                MessageBox.Show("Failed to start verification capture:" & vbCrLf & captureEx.Message,
                              "Capture Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                isVerifying = False
                lblstatusimage.Image = Img.Images(0)
            End Try

        Catch ex As Exception
            MessageBox.Show("Error starting multi-finger verification: " & ex.Message, "Verification Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            isVerifying = False
            lblstatusimage.Image = Img.Images(0)
        End Try
    End Sub

    Private Sub StartVerificationCapture(template As DPFP.Template)
        Try
            ' Ensure we're not in capture mode first
            If isCapturing Then
                StopCapture()
                ' Longer delay to ensure capture is fully stopped
                Threading.Thread.Sleep(500)
            End If

            ' Store template for verification
            storedTemplateForVerification = template
            isVerifying = True

            ' Initialize verifier with fresh instance
            Verifier = New Verification()

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
            If Not isVerifying OrElse storedTemplateForVerification Is Nothing Then
                LogMessage("Verification aborted: isVerifying=" & isVerifying & ", templateExists=" & (storedTemplateForVerification IsNot Nothing))
                Return
            End If

            ' Extract features for verification
            Dim features As DPFP.FeatureSet = ExtractFeatures(sample, DPFP.Processing.DataPurpose.Verification)

            If features IsNot Nothing Then
                Try
                    ' Log sample quality information
                    LogToErrorBox("Sample Quality - Size: " & sample.Bytes.Length & " bytes", "Info")

                    ' Validate template before verification
                    If storedTemplateForVerification.Size = 0 Then
                        LogToErrorBox("Invalid template detected: Size = 0", "Error")
                        TryNextFingerTemplate("Invalid template detected")
                        Return
                    End If

                    LogToErrorBox("Starting verification - Template Size: " & storedTemplateForVerification.Size & ", FAR Threshold: " & customFARThreshold, "Info")

                    ' Perform verification with enhanced settings
                    Dim Verificator As New DPFP.Verification.Verification()
                    Dim result As New DPFP.Verification.Verification.Result()

                    ' Set a more lenient FAR threshold for better matching
                    Verificator.FARRequested = customFARThreshold

                    ' Try verification with current threshold first
                    Try
                        Verificator.Verify(features, storedTemplateForVerification, result)
                    Catch verifyEx As Exception When verifyEx.Message.Contains("0xFFFFFFFE")
                        ' If verification fails with quality error, try with more lenient threshold
                        LogToErrorBox("First verification attempt failed due to quality. Trying with more lenient threshold...", "Info")
                        Verificator.FARRequested = customFARThreshold * 10 ' Make it 10 times more lenient
                        Verificator.Verify(features, storedTemplateForVerification, result)
                        LogToErrorBox("Used fallback FAR threshold: " & (customFARThreshold * 10).ToString("F8"), "Info")
                    End Try

                    ' Enhanced debugging information
                    Dim currentFingerName As String = "Unknown"
                    If fingerNames IsNot Nothing AndAlso currentTemplateIndex < fingerNames.Count Then
                        currentFingerName = fingerNames(currentTemplateIndex)
                    End If

                    LogToErrorBox("Verification Result for " & currentFingerName & ":", "Info")
                    LogToErrorBox("- Verified: " & result.Verified, "Info")
                    LogToErrorBox("- FAR Achieved: " & result.FARAchieved.ToString("F8"), "Info")
                    LogToErrorBox("- FAR Requested: " & customFARThreshold.ToString("F8"), "Info")
                    LogToErrorBox("- Attempt: " & (verificationAttempts + 1) & " of " & maxAttemptsPerFinger, "Info")

                    If result.Verified Then
                        ' Verification successful
                        If fingerNames IsNot Nothing AndAlso currentTemplateIndex < fingerNames.Count Then
                            currentFingerName = fingerNames(currentTemplateIndex)
                        End If

                        UpdateStatus("Fingerprint verification successful with " & currentFingerName & "!")
                        lblstatusimage.Image = Img.Images(1)
                        MessageBox.Show("Fingerprint verified successfully for " & lblempname.Text & " using " & currentFingerName & "!" & vbCrLf &
                                      "Verification Details:" & vbCrLf &
                                      "- Attempts: " & (verificationAttempts + 1).ToString() & vbCrLf &
                                      "- FAR Achieved: " & result.FARAchieved.ToString("F8"),
                                      "Verification Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        StopVerification()
                    Else
                        ' Verification failed - Enhanced failure analysis
                        verificationAttempts += 1

                        ' Detailed failure analysis
                        Dim farDifference As Double = result.FARAchieved - customFARThreshold
                        Dim farRatio As Double = If(customFARThreshold > 0, result.FARAchieved / customFARThreshold, 0)

                        LogToErrorBox("Verification failed analysis:", "Info")
                        LogToErrorBox("- FAR Difference: " & farDifference.ToString("F8"), "Info")
                        LogToErrorBox("- FAR Ratio: " & farRatio.ToString("F2"), "Info")

                        ' Adaptive threshold suggestion
                        If result.FARAchieved < 1.0 AndAlso result.FARAchieved > customFARThreshold * 2 Then
                            LogToErrorBox("SUGGESTION: FAR is close but not quite there. Consider slightly higher threshold.", "Info")
                        ElseIf result.FARAchieved > 1.0 Then
                            LogToErrorBox("SUGGESTION: FAR is very high - fingerprint may not match or template corrupted.", "Info")
                        End If

                        ' Give multiple attempts per finger before switching
                        If verificationAttempts < maxAttemptsPerFinger Then
                            UpdateStatus("Attempt " & verificationAttempts.ToString() & " for " & currentFingerName & " failed (FAR: " & result.FARAchieved.ToString("F8") & "). Try again...")
                        Else
                            TryNextFingerTemplate("Verification failed with " & currentFingerName & " after " & verificationAttempts.ToString() & " attempts (FAR: " & result.FARAchieved.ToString("F8") & ")")
                        End If
                        ' Don’t stop automatically → allow retry
                    End If

                Catch dpfpEx As Exception When dpfpEx.Message.Contains("local")
                    LogToErrorBox("DPFP Local Error: " & dpfpEx.Message, "Error")
                    LogToErrorBox("Template Compatibility Issue Detected:", "Error")
                    LogToErrorBox("- This template was created with a different DPFP configuration", "Info")
                    LogToErrorBox("- Template may need re-registration for compatibility", "Info")
                    LogToErrorBox("- Attempting template refresh...", "Info")

                    ' Try to refresh the template from server with better compatibility
                    Try
                        RefreshTemplateCompatibility()
                    Catch refreshEx As Exception
                        LogToErrorBox("Template refresh failed: " & refreshEx.Message, "Error")
                        TryNextFingerTemplate("Template compatibility issue: " & dpfpEx.Message)
                    End Try
                Catch dpfpEx As Exception When dpfpEx.Message.Contains("0xFFFFFFFE")
                    LogToErrorBox("DPFP Sample Quality Error (HRESULT: 0xFFFFFFFE): " & dpfpEx.Message, "Error")
                    LogToErrorBox("This error typically indicates:", "Info")
                    LogToErrorBox("1. Sample quality too poor for verification", "Info")
                    LogToErrorBox("2. Finger placement or pressure issues", "Info")
                    LogToErrorBox("3. Scanner surface needs cleaning", "Info")
                    UpdateStatus("Sample quality too poor. Please clean finger and scanner, then try again...")

                    ' Show quality improvement tips after a few failures
                    verificationAttempts += 1
                    If verificationAttempts >= 3 Then
                        ShowQualityImprovementTips()
                        verificationAttempts = 0 ' Reset counter
                    End If
                    ' Don't switch templates yet - allow retry with same template
                Catch dpfpEx As Exception
                    LogToErrorBox("DPFP General Error: " & dpfpEx.Message, "Error")
                    TryNextFingerTemplate("DPFP verification error: " & dpfpEx.Message)
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

    Private Sub TryNextFingerTemplate(failureReason As String)
        Try
            ' Check if we have more templates to try
            If availableTemplates IsNot Nothing AndAlso currentTemplateIndex < availableTemplates.Count - 1 Then
                ' Move to next template
                currentTemplateIndex += 1
                verificationAttempts = 0 ' Reset attempts for new finger
                storedTemplateForVerification = availableTemplates(currentTemplateIndex)

                Dim nextFingerName As String = "Unknown"
                If fingerNames IsNot Nothing AndAlso currentTemplateIndex < fingerNames.Count Then
                    nextFingerName = fingerNames(currentTemplateIndex)
                End If

                UpdateStatus(failureReason & ". Trying next finger: " & nextFingerName & ". Please place finger again...")
                LogToErrorBox("Switching to template " & (currentTemplateIndex + 1) & " of " & availableTemplates.Count & " (" & nextFingerName & ")", "Info")

            Else
                ' No more templates to try - verification completely failed
                UpdateStatus("Fingerprint verification failed with all registered fingers.")
                lblstatusimage.Image = Img.Images(0)

                Dim totalFingers As Integer = If(availableTemplates IsNot Nothing, availableTemplates.Count, 0)

                ' Show enhanced failure message with diagnostic option
                Dim failureMessage As String = "Fingerprint verification failed!" & vbCrLf & vbCrLf &
                              "Tried " & totalFingers & " registered finger(s) for " & lblempname.Text & "." & vbCrLf &
                              "Last failure reason: " & failureReason & vbCrLf & vbCrLf &
                              "Please ensure:" & vbCrLf &
                              "1. You are using a registered finger" & vbCrLf &
                              "2. Your finger is clean and dry" & vbCrLf &
                              "3. Place finger flat on the scanner" & vbCrLf & vbCrLf &
                              "Would you like to see detailed diagnostic information?"

                Dim result As DialogResult = MessageBox.Show(failureMessage, "Verification Failed", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

                If result = DialogResult.Yes Then
                    DiagnoseVerificationIssues()
                End If

                StopVerification()
            End If

        Catch ex As Exception
            MessageBox.Show("Error trying next finger template: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            StopVerification()
        End Try
    End Sub

    ' Diagnostic method to help troubleshoot verification failures
    Private Sub DiagnoseVerificationIssues()
        Try
            Dim diagnosticReport As New System.Text.StringBuilder()
            diagnosticReport.AppendLine("=== FINGERPRINT VERIFICATION DIAGNOSTIC REPORT ===")
            diagnosticReport.AppendLine("Timestamp: " & DateTime.Now.ToString())
            diagnosticReport.AppendLine()

            ' System Information
            diagnosticReport.AppendLine("--- SYSTEM INFORMATION ---")
            diagnosticReport.AppendLine("Selected Employee ID: " & selectedEmpId)
            diagnosticReport.AppendLine("Selected Employee Name: " & lblempname.Text)
            diagnosticReport.AppendLine("Selected Finger Type: " & selectedFingerType)
            diagnosticReport.AppendLine("Selected Finger Name: " & selectedFingerName)
            diagnosticReport.AppendLine()

            ' Verification Settings
            diagnosticReport.AppendLine("--- VERIFICATION SETTINGS ---")
            diagnosticReport.AppendLine("Custom FAR Threshold: " & customFARThreshold.ToString("F8"))
            diagnosticReport.AppendLine("Max Attempts Per Finger: " & maxAttemptsPerFinger)
            diagnosticReport.AppendLine("Current Verification Attempts: " & verificationAttempts)
            diagnosticReport.AppendLine("Is Verifying: " & isVerifying)
            diagnosticReport.AppendLine()

            ' Template Information
            diagnosticReport.AppendLine("--- TEMPLATE INFORMATION ---")
            If availableTemplates IsNot Nothing Then
                diagnosticReport.AppendLine("Available Templates Count: " & availableTemplates.Count)
                For i As Integer = 0 To availableTemplates.Count - 1
                    If availableTemplates(i) IsNot Nothing Then
                        Dim fingerName As String = "Unknown"
                        If fingerNames IsNot Nothing AndAlso i < fingerNames.Count Then
                            fingerName = fingerNames(i)
                        End If
                        diagnosticReport.AppendLine("  Template " & (i + 1) & " (" & fingerName & "): Size = " & availableTemplates(i).Size)
                    Else
                        diagnosticReport.AppendLine("  Template " & (i + 1) & ": NULL")
                    End If
                Next
                diagnosticReport.AppendLine("Current Template Index: " & currentTemplateIndex)
            Else
                diagnosticReport.AppendLine("Available Templates: NULL")
            End If

            If storedTemplateForVerification IsNot Nothing Then
                diagnosticReport.AppendLine("Current Template Size: " & storedTemplateForVerification.Size)
            Else
                diagnosticReport.AppendLine("Current Template: NULL")
            End If
            diagnosticReport.AppendLine()

            ' Suggestions
            diagnosticReport.AppendLine("--- TROUBLESHOOTING SUGGESTIONS ---")
            If availableTemplates Is Nothing OrElse availableTemplates.Count = 0 Then
                diagnosticReport.AppendLine("❌ No templates found - Register fingerprints first")
            ElseIf storedTemplateForVerification Is Nothing Then
                diagnosticReport.AppendLine("❌ No current template loaded - Check template retrieval")
            ElseIf storedTemplateForVerification.Size = 0 Then
                diagnosticReport.AppendLine("❌ Template size is 0 - Template may be corrupted, re-register")
            Else
                diagnosticReport.AppendLine("✅ Templates appear valid")
                diagnosticReport.AppendLine("💡 Try these solutions:")
                diagnosticReport.AppendLine("  1. Clean your finger and scanner surface")
                diagnosticReport.AppendLine("  2. Press firmly but not too hard")
                diagnosticReport.AppendLine("  3. Try a different finger if registered")
                diagnosticReport.AppendLine("  4. Consider re-registering fingerprint if FAR values are consistently high")
                diagnosticReport.AppendLine("  5. If getting 'Expecting object to be local' errors:")
                diagnosticReport.AppendLine("     - Template compatibility issue detected")
                diagnosticReport.AppendLine("     - System will attempt automatic template refresh")
                diagnosticReport.AppendLine("     - May need fingerprint re-registration for full compatibility")
            End If

            ' Display the report
            LogToErrorBox(diagnosticReport.ToString(), "Info")

            ' Also show a summary to the user
            Dim userMessage As String = "Diagnostic Information:" & vbCrLf & vbCrLf
            userMessage += "Templates Available: " & If(availableTemplates IsNot Nothing, availableTemplates.Count, 0) & vbCrLf
            userMessage += "FAR Threshold: " & customFARThreshold.ToString("F8") & vbCrLf
            userMessage += "Current Attempts: " & verificationAttempts & "/" & maxAttemptsPerFinger & vbCrLf & vbCrLf
            userMessage += "Check Debug Output for detailed diagnostic report."

            MessageBox.Show(userMessage, "Verification Diagnostic", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            LogToErrorBox("Error generating diagnostic report: " & ex.Message, "Error")
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

    ''' <summary>
    ''' Provides user guidance for improving fingerprint sample quality
    ''' </summary>
    Private Sub ShowQualityImprovementTips()
        Dim tipMessage As String = "Fingerprint Sample Quality Tips:" & vbCrLf & vbCrLf &
                                  "To improve verification success:" & vbCrLf &
                                  "1. Clean your finger with a dry cloth" & vbCrLf &
                                  "2. Clean the scanner surface gently" & vbCrLf &
                                  "3. Place finger flat and centered on scanner" & vbCrLf &
                                  "4. Apply firm but gentle pressure" & vbCrLf &
                                  "5. Keep finger still during scanning" & vbCrLf &
                                  "6. Try a different registered finger if available" & vbCrLf & vbCrLf &
                                  "If problems persist, the fingerprint template may need to be re-registered."

        MessageBox.Show(tipMessage, "Quality Improvement Tips", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' Attempts to refresh the template compatibility by re-deserializing from server data
    ''' </summary>
    Private Sub RefreshTemplateCompatibility()
        Try
            LogToErrorBox("Attempting template compatibility refresh...", "Info")

            ' Get the current finger name
            Dim currentFingerName As String = "Unknown"
            If fingerNames IsNot Nothing AndAlso currentTemplateIndex < fingerNames.Count Then
                currentFingerName = fingerNames(currentTemplateIndex)
            End If

            ' Re-fetch template from server with fresh deserialization
            Dim refreshedTemplate As DPFP.Template = GetSpecificTemplateFromServer(selectedEmpId, currentFingerName)

            If refreshedTemplate IsNot Nothing AndAlso refreshedTemplate.Size > 0 Then
                LogToErrorBox("Template refreshed successfully. New size: " & refreshedTemplate.Size, "Success")

                ' Update the stored template and available templates
                storedTemplateForVerification = refreshedTemplate
                If availableTemplates IsNot Nothing AndAlso currentTemplateIndex < availableTemplates.Count Then
                    availableTemplates(currentTemplateIndex) = refreshedTemplate
                End If

                LogToErrorBox("Template compatibility refresh completed. Ready for retry.", "Success")
                UpdateStatus("Template refreshed. Please place finger again for verification...")
            Else
                Throw New Exception("Unable to refresh template - server returned invalid data")
            End If

        Catch ex As Exception
            LogToErrorBox("Template refresh error: " & ex.Message, "Error")
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Gets a specific template from server for compatibility refresh
    ''' </summary>
    Private Function GetSpecificTemplateFromServer(empId As Integer, fingerName As String) As DPFP.Template
        Try
            LogToErrorBox("Fetching template from server for " & fingerName & "...", "Info")

            ' Create JSON data as expected by the server
            Dim jsonData As String = "{""EmpId"":" & empId & ",""FingerName"":""" & fingerName & """,""FingerType"":""Employee""}"
            LogToErrorBox("Sending JSON data: " & jsonData, "Info")

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/json"
                Dim response As String = client.UploadString(M_Details.LinkAjaxRequest & "AttRequest=2", jsonData)
                LogToErrorBox("Raw server response: " & response, "Info")

                Dim parsedResponse As JObject = JObject.Parse(response)

                If parsedResponse("Success").ToString().ToLower() = "true" Then
                    ' Server returns "Template" not "template"
                    Dim templateData As String = parsedResponse("Template").ToString()

                    If Not String.IsNullOrEmpty(templateData) Then
                        Try
                            Dim templateBytes() As Byte = Convert.FromBase64String(templateData)
                            LogToErrorBox("Template data retrieved for refresh. Size: " & templateBytes.Length & " bytes", "Info")

                            Using stream As New MemoryStream(templateBytes)
                                Dim template As New DPFP.Template()
                                Try
                                    ' Enhanced template deserialization with compatibility checks
                                    template.DeSerialize(stream)

                                    If template.Size > 0 Then
                                        LogToErrorBox("Template deserialized successfully for refresh. DPFP Size: " & template.Size, "Success")
                                        Return template
                                    Else
                                        LogToErrorBox("Refreshed template has zero size", "Error")
                                        Return Nothing
                                    End If

                                Catch deserializeEx As Runtime.InteropServices.COMException
                                    LogToErrorBox("Template refresh deserialization COM error: " & deserializeEx.Message, "Error")
                                    Return Nothing
                                Catch deserializeEx As Exception
                                    LogToErrorBox("Template refresh deserialization error: " & deserializeEx.Message, "Error")
                                    Return Nothing
                                End Try
                            End Using
                        Catch b64Ex As Exception
                            LogToErrorBox("Base64 decode error during refresh for " & fingerName & ": " & b64Ex.Message, "Error")
                            Return Nothing
                        End Try
                    Else
                        LogToErrorBox("Empty template data during refresh for " & fingerName, "Error")
                        Return Nothing
                    End If
                Else
                    Dim errorMsg As String = "Unknown error"
                    If parsedResponse("Msg") IsNot Nothing Then
                        errorMsg = parsedResponse("Msg").ToString()
                    End If
                    LogToErrorBox("Server error during refresh for " & fingerName & ": " & errorMsg, "Error")
                    Return Nothing
                End If

            End Using
        Catch ex As Exception
            LogToErrorBox("Error during template refresh for " & fingerName & ": " & ex.Message, "Error")
            Return Nothing
        End Try
    End Function

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
#End Region
#Region "DeleteFinger"
    Private Sub cmbFingerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFingerName.SelectedIndexChanged
        Try
            If cmbFingerName.SelectedIndex = 0 Then
                selectedFingerName = "Finger1"
            Else
                selectedFingerName = "Finger2"
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Async Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Dim EmpId As Integer = 0
            Dim EmpName As String = "'"
            If GridViewEmpFingerHeader.RowCount > 0 Then
                EmpId = GridViewEmpFingerHeader.GetFocusedRowCellValue("id")
                EmpName = GridViewEmpFingerHeader.GetFocusedRowCellValue("emp_printname")
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
