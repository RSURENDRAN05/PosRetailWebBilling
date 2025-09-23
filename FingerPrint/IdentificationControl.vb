Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Text
Imports System.Windows.Forms
Imports DPUruNet
Imports DPUruNet.Constants

Partial Public Class IdentificationControl
    Inherits Form

    Private Const DPFJ_PROBABILITY_ONE As Integer = &H7FFFFFFF
    Dim WithEvents identificationControl As DPCtlUruNet.IdentificationControl
    Dim dbhelper As New FingerprintHelper()
    Dim EmpIds As String
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Overloads Sub ShowDailogData(ByRef EmpId As String)
        Try
            EmpIds = EmpId
            ' reset flag each time dialog opens
            MyBase.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Error opening dialog: " & ex.Message)
        End Try
    End Sub

    Private Sub IdentificationControl_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        'MessageBox.Show("Form Load - initializing fingerprint identification")
        StoreFmdFile()
        'Dim count = _sender.Fmds.Count
        FingerPrintReader.CurrentReaderData()
        ' Get first EmployeeFinger object
        ' Extract all Fmd templates from EmployeeFinger dictionary
        Dim allTemplates As List(Of Fmd) = FingerPrintReader.Fmds.Values.Select(Function(emp) emp.FingerTemplate).ToList()

        If identificationControl IsNot Nothing Then
            identificationControl.Reader = FingerPrintReader.CurrentReader
        End If
        ' See the SDK documentation for an explanation on threshold scores.
        Dim thresholdScore As Integer = DPFJ_PROBABILITY_ONE * 1 / 100000
        identificationControl = New DPCtlUruNet.IdentificationControl(FingerPrintReader.CurrentReader, allTemplates, thresholdScore, 10, Constants.CapturePriority.DP_PRIORITY_COOPERATIVE)
        identificationControl.Location = New System.Drawing.Point(3, 3)
        identificationControl.Name = "identificationControl"
        identificationControl.Size = New System.Drawing.Size(397, 128)
        identificationControl.TabIndex = 0

        ' Be sure to set the maximum number of matches you want returned.
        identificationControl.MaximumResult = 10

        Me.Controls.Add(identificationControl)


        identificationControl.StartIdentification()
    End Sub

    Private Sub IdentificationControl_FormClosed(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Closed
        identificationControl.StopIdentification()
    End Sub
    Private Sub identificationControl_OnIdentify(ByVal IdentificationControl As DPCtlUruNet.IdentificationControl, ByVal IdentificationResult As IdentifyResult) Handles identificationControl.OnIdentify
        Try
            ' Check if identification failed
            If IdentificationResult.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
                ' Handle failure
                If IdentificationResult.Indexes Is Nothing Then
                    Select Case IdentificationResult.ResultCode
                        Case Constants.ResultCode.DP_INVALID_PARAMETER
                            MessageBox.Show("Warning: Fake finger detected.", "Identification Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Case Constants.ResultCode.DP_NO_DATA
                            MessageBox.Show("Warning: No finger detected.", "Identification Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Case Else
                            MessageBox.Show("Error: " & IdentificationResult.ResultCode.ToString(), "Identification Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Select

                    ' Dispose reader on failure
                    If FingerPrintReader.CurrentReader IsNot Nothing Then
                        FingerPrintReader.CurrentReader.Dispose()
                        FingerPrintReader.CurrentReader = Nothing
                    End If
                Else
                    If FingerPrintReader.CurrentReader IsNot Nothing Then
                        FingerPrintReader.CurrentReader.Dispose()
                        FingerPrintReader.CurrentReader = Nothing
                    End If
                    MessageBox.Show("Error:  " & IdentificationResult.ResultCode.ToString())
                End If
            End If


            ' Identification succeeded
            FingerPrintReader.CurrentReader = IdentificationControl.Reader
            Dim matchMessage As String = ""

            ' Check if there are actual matches
            If IdentificationResult.Indexes IsNot Nothing AndAlso IdentificationResult.Indexes.Length > 0 Then
                For i As Integer = 0 To IdentificationResult.Indexes.Length - 1
                    'If FingerPrintReader.Fmds.ContainsKey(i) Then
                    '    Dim empInfo As EmployeeFinger = FingerPrintReader.Fmds(i)
                    '    matchMessage &= "Employee: " & empInfo.EmpName & " (ID: " & empInfo.EmpId & "), Finger: " & empInfo.FingerName & vbCrLf
                    'Else
                    '    matchMessage &= "Matched index: " & i.ToString() & vbCrLf
                    'End If
                Next

                ' Append to textbox
                txtMessage.AppendText("OnIdentify: " & matchMessage & vbCrLf)

                ' Show success message
                MessageBox.Show("Fingerprint identified successfully!" & vbCrLf & matchMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ' No matches found
                txtMessage.AppendText("OnIdentify: No matches found." & vbCrLf)
                MessageBox.Show("No fingerprint matches found.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


            ' Scroll textbox
            txtMessage.SelectionStart = txtMessage.TextLength
            txtMessage.ScrollToCaret()

        Catch ex As Exception
            MessageBox.Show("Error in OnIdentify: " & ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




    'Private Sub identificationControl1_OnIdentify(ByVal IdentificationControl As DPCtlUruNet.IdentificationControl, ByVal IdentificationResult As IdentifyResult) Handles identificationControl.OnIdentify
    '    Try
    '        MessageBox.Show("OnIdentify event fired")
    '        ' 1. Check SDK result
    '        If IdentificationResult.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
    '            MessageBox.Show("Fingerprint capture failed: " & IdentificationResult.ResultCode.ToString())
    '            Exit Sub
    '        End If

    '        ' 2. Capture probe FMD from reader
    '        Dim probeFmd As Fmd = CaptureProbeFmd()
    '        If probeFmd Is Nothing Then
    '            MessageBox.Show("Failed to generate probe FMD.")
    '            Exit Sub
    '        End If

    '        ' 3. Verify against stored FMD(s) via API
    '        Dim dbhelper As New FingerprintHelper()
    '        Dim empId As String = EmpIds ' 🔹 replace with dynamic employeeId

    '        If dbhelper.VerifyFingerprint(empId, probeFmd, "", "") Then
    '            MessageBox.Show("✅ Fingerprint verified for empId=" & empId)
    '        Else
    '            MessageBox.Show("❌ No match found for empId=" & empId)
    '        End If

    '        ' 4. Log to UI
    '        txtMessage.Text &= "OnIdentify completed for empId=" & empId & vbCrLf & vbCrLf
    '        txtMessage.SelectionStart = txtMessage.TextLength
    '        txtMessage.ScrollToCaret()

    '    Catch ex As Exception
    '        MessageBox.Show("Error during identification: " & ex.Message)
    '    End Try

    'End Sub
    'Private Function CaptureProbeFmd() As Fmd
    '    Try
    '        ' ✅ Make sure reader is ready
    '        If FingerPrintReader.CurrentReader Is Nothing Then
    '            MessageBox.Show("No reader available.")
    '            Return Nothing
    '        End If

    '        ' ✅ Capture fingerprint (timeout 5000 ms)
    '        Dim captureResult As CaptureResult = FingerPrintReader.CurrentReader.Capture( _
    '            Formats.Fid.ANSI, _
    '            CaptureProcessing.DP_IMG_PROC_DEFAULT, _
    '            5000, _
    '            FingerPrintReader.CurrentReader.Capabilities.Resolutions(0) _
    '        )

    '        ' ✅ Check capture result
    '        If captureResult.ResultCode <> Constants.ResultCode.DP_SUCCESS OrElse captureResult.Data Is Nothing Then
    '            MessageBox.Show("Failed to capture fingerprint. Result: " & captureResult.ResultCode.ToString())
    '            Return Nothing
    '        End If

    '        ' ✅ Convert FID → FMD (this is static, no `New FeatureExtraction()`)
    '        Dim fmdResult As DataResult(Of Fmd) = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI)

    '        ' ✅ Verify conversion success
    '        If fmdResult.ResultCode = Constants.ResultCode.DP_SUCCESS AndAlso fmdResult.Data IsNot Nothing Then
    '            Return fmdResult.Data   ' return the probe FMD
    '        Else
    '            MessageBox.Show("Error converting FID to FMD: " & fmdResult.ResultCode.ToString())
    '            Return Nothing
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show("Capture error: " & ex.Message)
    '        Return Nothing
    '    End Try
    'End Function




    'Private Sub identificationControl_OnIdentify(ByVal IdentificationControl As DPCtlUruNet.IdentificationControl, ByVal IdentificationResult As IdentifyResult) Handles identificationControl.OnIdentify
    '    Dim dbhelper As New FingerprintHelper()
    '    Dim probeFmd As Fmd = CaptureProbeFmd()  ' from reader

    '    If dbhelper.VerifyFingerprint("123", probeFmd) Then
    '        MessageBox.Show("Fingerprint verified via API for empId=123")
    '    Else
    '        MessageBox.Show("No match found for empId=123")
    '    End If

    '    'If IdentificationResult.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
    '    '    If IdentificationResult.Indexes Is Nothing Then
    '    '        If IdentificationResult.ResultCode = Constants.ResultCode.DP_INVALID_PARAMETER Then
    '    '            MessageBox.Show("Warning: Fake finger was detected.")
    '    '        ElseIf IdentificationResult.ResultCode = Constants.ResultCode.DP_NO_DATA Then
    '    '            MessageBox.Show("Warning: No finger was detected.")
    '    '        Else
    '    '            If _sender.CurrentReader IsNot Nothing Then
    '    '                _sender.CurrentReader.Dispose()
    '    '                _sender.CurrentReader = Nothing
    '    '            End If
    '    '        End If
    '    '    Else
    '    '        If _sender.CurrentReader IsNot Nothing Then
    '    '            _sender.CurrentReader.Dispose()
    '    '            _sender.CurrentReader = Nothing
    '    '        End If
    '    '        MessageBox.Show("Error:  " & IdentificationResult.ResultCode.ToString())
    '    '    End If
    '    'Else
    '    '    _sender.CurrentReader = IdentificationControl.Reader
    '    '    txtMessage.Text = txtMessage.Text + "OnIdentify:  " & (If(IdentificationResult.Indexes.Length.Equals(0), "No ", "One or more ")) & "matches.  Try another finger." & vbCr & vbLf & vbCr & vbLf
    '    'End If

    '    '    txtMessage.SelectionStart = txtMessage.TextLength
    '    '    txtMessage.ScrollToCaret()
    'End Sub
    'Private Function CaptureProbeFmd() As Fmd
    '    Try
    '        ' make sure reader exists
    '        If _sender.CurrentReader Is Nothing Then
    '            MessageBox.Show("No reader available.")
    '            Return Nothing
    '        End If

    '        ' check reader status
    '        Dim status = _sender.CurrentReader.GetStatus()
    '        If status <> Constants.ResultCode.DP_SUCCESS Then
    '            MessageBox.Show("Reader status error: " & status.ToString())
    '            Return Nothing
    '        End If

    '        ' capture finger (timeout 5000 ms)
    '        Dim captureResult As CaptureResult = _sender.CurrentReader.Capture(Formats.Fid.ANSI, CaptureProcessing.DP_IMG_PROC_DEFAULT, 5000, _sender.CurrentReader.Capabilities.Resolutions(0))

    '        If captureResult.ResultCode <> Constants.ResultCode.DP_SUCCESS OrElse captureResult.Data Is Nothing Then
    '            MessageBox.Show("Failed to capture fingerprint.")
    '            Return Nothing
    '        End If

    '        ' convert capture (FID) to FMD
    '        Dim fmdResult As DataResult(Of Fmd) = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Fmd.Format.ANSI)

    '        If fmdResult.ResultCode = Constants.ResultCode.DP_SUCCESS AndAlso fmdResult.Data IsNot Nothing Then
    '            Return fmdResult.Data
    '        Else
    '            MessageBox.Show("Error converting to FMD: " & fmdResult.ResultCode.ToString())
    '            Return Nothing
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show("Capture error: " & ex.Message)
    '        Return Nothing
    '    End Try
    'End Function

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class
'! @endcond
