'Imports System.Net
'Imports Newtonsoft.Json.Linq
'Imports DPFP
'Imports DPFP.Capture
'Imports DPFP.Processing
'Imports DPFP.Verification

'Public Class FingerprintAuthenticationModule
'    Implements DPFP.Capture.EventHandler

'    ' Events for communication with calling forms
'    Public Event VerificationComplete(success As Boolean, empId As Integer, empName As String)
'    Public Event StatusUpdate(message As String)
'    Public Event ShowError(message As String, title As String)

'    ' Private members
'    Private Capturer As Capture
'    Private Verifier As Verification
'    Private storedTemplate As DPFP.Template
'    Private isCapturing As Boolean = False
'    Private isVerifying As Boolean = False
'    Private targetEmpId As Integer = 0
'    Private targetEmpName As String = ""
'    Private verificationTimeout As Timer
'    Private Const VERIFICATION_TIMEOUT_SECONDS As Integer = 30

'    Public Sub New()
'        ' Initialize verification timeout timer
'        verificationTimeout = New Timer()
'        verificationTimeout.Interval = VERIFICATION_TIMEOUT_SECONDS * 1000
'        AddHandler verificationTimeout.Tick, AddressOf OnVerificationTimeout
'    End Sub

'#Region "Public Methods"

'    ''' <summary>
'    ''' Start fingerprint verification for a specific employee
'    ''' </summary>
'    ''' <param name="empId">Employee ID to verify</param>
'    ''' <param name="empName">Employee name for display</param>
'    ''' <returns>True if verification started successfully</returns>
'    Public Async Function StartVerification(empId As Integer, empName As String) As Task(Of Boolean)
'        Try
'            If isVerifying Then
'                RaiseEvent ShowError("Fingerprint verification already in progress.", "Verification Active")
'                Return False
'            End If

'            targetEmpId = empId
'            targetEmpName = empName

'            RaiseEvent StatusUpdate("Retrieving fingerprint template for " & empName & "...")

'            ' Get stored fingerprint template from server
'            storedTemplate = Await GetFingerprintFromServer(empId)

'            If storedTemplate IsNot Nothing Then
'                ' Start verification process
'                Return StartFingerprintCapture()
'            Else
'                RaiseEvent ShowError("No fingerprint template found for " & empName & ". Please register fingerprint first.", "No Template Found")
'                Return False
'            End If

'        Catch ex As Exception
'            RaiseEvent ShowError("Error starting verification: " & ex.Message, "Verification Error")
'            Return False
'        End Try
'    End Function

'    ''' <summary>
'    ''' Stop any active verification process
'    ''' </summary>
'    Public Sub StopVerification()
'        Try
'            ' Stop timeout timer
'            verificationTimeout.Stop()

'            ' Stop capture
'            If Capturer IsNot Nothing AndAlso isCapturing Then
'                Try
'                    Capturer.StopCapture()
'                    Threading.Thread.Sleep(100)
'                Catch
'                    ' Ignore errors during stop
'                End Try
'            End If

'            ' Reset states
'            isCapturing = False
'            isVerifying = False
'            storedTemplate = Nothing
'            targetEmpId = 0
'            targetEmpName = ""

'            RaiseEvent StatusUpdate("Verification stopped.")

'        Catch ex As Exception
'            RaiseEvent ShowError("Error stopping verification: " & ex.Message, "Stop Error")
'        End Try
'    End Sub

'    ''' <summary>
'    ''' Check if verification is currently active
'    ''' </summary>
'    Public ReadOnly Property IsActive As Boolean
'        Get
'            Return isVerifying
'        End Get
'    End Property

'    ''' <summary>
'    ''' Cleanup resources when done
'    ''' </summary>
'    Public Sub Dispose()
'        Try
'            StopVerification()

'            If Capturer IsNot Nothing Then
'                Try
'                    Capturer.Dispose()
'                Catch
'                    ' Ignore disposal errors
'                End Try
'                Capturer = Nothing
'            End If

'            If verificationTimeout IsNot Nothing Then
'                verificationTimeout.Dispose()
'                verificationTimeout = Nothing
'            End If

'        Catch ex As Exception
'            ' Ignore cleanup errors
'        End Try
'    End Sub

'#End Region

'#Region "Private Methods"

'    Private Function StartFingerprintCapture() As Boolean
'        Try
'            RaiseEvent StatusUpdate("Initializing fingerprint device...")

'            ' Initialize capturer
'            If Capturer IsNot Nothing Then
'                Try
'                    Capturer.Dispose()
'                    Threading.Thread.Sleep(200)
'                Catch
'                    ' Ignore disposal errors
'                End Try
'            End If

'            ' Create fresh capturer
'            Capturer = New Capture()
'            Capturer.EventHandler = Me

'            ' Initialize verifier
'            Verifier = New Verification()

'            ' Start capture
'            Capturer.StartCapture()
'            isCapturing = True
'            isVerifying = True

'            ' Start timeout timer
'            verificationTimeout.Start()

'            RaiseEvent StatusUpdate("Place finger on scanner for verification...")
'            Return True

'        Catch dpfpEx As System.Runtime.InteropServices.COMException
'            RaiseEvent ShowError("Device communication error:" & vbCrLf & vbCrLf &
'                               "Please check:" & vbCrLf &
'                               "1. Fingerprint device is connected" & vbCrLf &
'                               "2. Device drivers are installed" & vbCrLf &
'                               "3. No other fingerprint applications are running" & vbCrLf & vbCrLf &
'                               "Error: " & dpfpEx.Message,
'                               "Device Error")
'            Return False
'        Catch ex As Exception
'            RaiseEvent ShowError("Error starting fingerprint capture: " & ex.Message, "Capture Error")
'            Return False
'        End Try
'    End Function

'    Private Async Function GetFingerprintFromServer(empId As Integer) As Task(Of DPFP.Template)
'        Try
'            Using client As New WebClient()
'                client.Headers(HttpRequestHeader.ContentType) = "application/json"

'                Dim postData As String = "{""EmpId"":" & empId.ToString() & "}"
'                Dim response As String = Await Task.Run(Function()
'                                                            Return client.UploadString(M_Details.LinkAjaxRequest & "AttRequest=2", postData)
'                                                        End Function)

'                ' Clean up response if needed
'                response = response.Trim()
'                If response.Contains("}{") Then
'                    Dim firstBrace As Integer = response.IndexOf("}")
'                    If firstBrace > 0 Then
'                        response = response.Substring(0, firstBrace + 1)
'                    End If
'                End If

'                Dim parsedResponse As JObject = JObject.Parse(response)

'                If parsedResponse("Success").ToString().ToLower() = "true" Then
'                    Dim templateBase64 As String = parsedResponse("Template").ToString()
'                    If Not String.IsNullOrEmpty(templateBase64) Then
'                        ' Convert Base64 back to template
'                        Dim templateBytes() As Byte = Convert.FromBase64String(templateBase64)
'                        Dim template As New DPFP.Template()
'                        Using stream As New IO.MemoryStream(templateBytes)
'                            template.DeSerialize(stream)
'                            If template.Size > 0 Then
'                                Return template
'                            End If
'                        End Using
'                    End If
'                End If

'                Return Nothing
'            End Using
'        Catch ex As Exception
'            RaiseEvent ShowError("Network error retrieving fingerprint: " & ex.Message, "Network Error")
'            Return Nothing
'        End Try
'    End Function

'    Private Function ExtractFeatures(sample As Sample) As DPFP.FeatureSet
'        Try
'            Dim extractor As New DPFP.Processing.FeatureExtraction()
'            Dim feedback As DPFP.Capture.CaptureFeedback = Nothing
'            Dim features As New DPFP.FeatureSet()

'            extractor.CreateFeatureSet(sample, DPFP.Processing.DataPurpose.Verification, feedback, features)

'            If feedback = DPFP.Capture.CaptureFeedback.Good Then
'                Return features
'            Else
'                ' Provide feedback based on quality
'                Select Case feedback
'                    Case DPFP.Capture.CaptureFeedback.TooLight
'                        RaiseEvent StatusUpdate("Press harder on the scanner...")
'                    Case DPFP.Capture.CaptureFeedback.TooDark
'                        RaiseEvent StatusUpdate("Press lighter on the scanner...")
'                    Case DPFP.Capture.CaptureFeedback.TooLeft, DPFP.Capture.CaptureFeedback.TooRight, DPFP.Capture.CaptureFeedback.TooHigh, DPFP.Capture.CaptureFeedback.TooLow
'                        RaiseEvent StatusUpdate("Center finger on the scanner...")
'                    Case DPFP.Capture.CaptureFeedback.TooFast
'                        RaiseEvent StatusUpdate("Hold finger steady...")
'                    Case DPFP.Capture.CaptureFeedback.TooSlow
'                        RaiseEvent StatusUpdate("Place finger more quickly...")
'                    Case DPFP.Capture.CaptureFeedback.TooSkewed
'                        RaiseEvent StatusUpdate("Place finger straight on scanner...")
'                    Case DPFP.Capture.CaptureFeedback.TooShort
'                        RaiseEvent StatusUpdate("Place whole finger on scanner...")
'                    Case Else
'                        RaiseEvent StatusUpdate("Adjust finger position and try again...")
'                End Select
'                Return Nothing
'            End If
'        Catch ex As Exception
'            RaiseEvent StatusUpdate("Error extracting fingerprint features...")
'            Return Nothing
'        End Try
'    End Function

'    Private Sub OnVerificationTimeout(sender As Object, e As EventArgs)
'        verificationTimeout.Stop()
'        StopVerification()
'        RaiseEvent ShowError("Verification timeout. Please try again.", "Timeout")
'    End Sub

'#End Region

'#Region "DPFP Event Handlers"

'    Public Sub OnComplete(Capture As Object, ReaderSerialNumber As String, Sample As Sample) Implements EventHandler.OnComplete
'        Try
'            If Not isVerifying OrElse storedTemplate Is Nothing Then
'                Return
'            End If

'            ' Extract features for verification
'            Dim features As DPFP.FeatureSet = ExtractFeatures(Sample)

'            If features IsNot Nothing Then
'                ' Perform verification
'                Dim result As Verification.Result = Verification.Verify(features, storedTemplate)

'                If result.Verified Then
'                    ' Verification successful
'                    verificationTimeout.Stop()
'                    StopVerification()
'                    RaiseEvent VerificationComplete(True, targetEmpId, targetEmpName)
'                Else
'                    ' Verification failed - continue trying
'                    RaiseEvent StatusUpdate("Verification failed. Please try again with same finger...")
'                End If
'            End If

'        Catch ex As Exception
'            verificationTimeout.Stop()
'            StopVerification()
'            RaiseEvent ShowError("Error during verification: " & ex.Message, "Verification Error")
'        End Try
'    End Sub

'    Public Sub OnFingerTouch(Capture As Object, ReaderSerialNumber As String) Implements EventHandler.OnFingerTouch
'        RaiseEvent StatusUpdate("Finger detected. Hold steady...")
'    End Sub

'    Public Sub OnFingerGone(Capture As Object, ReaderSerialNumber As String) Implements EventHandler.OnFingerGone
'        RaiseEvent StatusUpdate("Finger removed. Place finger again...")
'    End Sub

'    Public Sub OnReaderConnect(Capture As Object, ReaderSerialNumber As String) Implements EventHandler.OnReaderConnect
'        RaiseEvent StatusUpdate("Fingerprint reader connected.")
'    End Sub

'    Public Sub OnReaderDisconnect(Capture As Object, ReaderSerialNumber As String) Implements EventHandler.OnReaderDisconnect
'        verificationTimeout.Stop()
'        StopVerification()
'        RaiseEvent ShowError("Fingerprint reader disconnected!", "Device Disconnected")
'    End Sub

'    Public Sub OnSampleQuality(Capture As Object, ReaderSerialNumber As String, Feedback As CaptureFeedback) Implements EventHandler.OnSampleQuality
'        ' Feedback is handled in ExtractFeatures method
'    End Sub

'#End Region

'End Class
