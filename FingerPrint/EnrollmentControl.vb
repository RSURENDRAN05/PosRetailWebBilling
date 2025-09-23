Imports System
Imports System.Windows.Forms
Imports DPUruNet

Partial Public Class EnrollmentControl
    Inherits Form

    Private WithEvents enrollmentControl As DPCtlUruNet.EnrollmentControl
 
    Private EmpIds As String
    Private FingerTypes As String
    Private dbhelper As New FingerprintHelper

    ' 🔹 Guard flag to prevent duplicate save
    Private alreadyEnrolled As Boolean = False

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Overloads Sub ShowDailogData(ByRef EmpId As String, fingerType As String)
        Try
            EmpIds = EmpId
            FingerTypes = fingerType
            alreadyEnrolled = False   ' reset flag each time dialog opens
            MyBase.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Error opening dialog: " & ex.Message)
        End Try
    End Sub

    Private Sub EnrollmentControl_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        FingerPrintReader.CurrentReaderData()

        If enrollmentControl IsNot Nothing Then
            enrollmentControl.Reader = FingerPrintReader.CurrentReader
        Else
            enrollmentControl = New DPCtlUruNet.EnrollmentControl(
                FingerPrintReader.CurrentReader,
                Constants.CapturePriority.DP_PRIORITY_COOPERATIVE
            )
            enrollmentControl.BackColor = System.Drawing.SystemColors.Window
            enrollmentControl.Location = New Drawing.Point(3, 3)
            enrollmentControl.Name = "ctlEnrollmentControl"
            enrollmentControl.Size = New Drawing.Size(482, 346)
            enrollmentControl.TabIndex = 0
        End If

        Me.Controls.Add(enrollmentControl)
    End Sub

#Region "Enrollment Control Events"
    Private Sub enrollment_OnEnroll(ByVal enrollmentControl As DPCtlUruNet.EnrollmentControl,
                                    ByVal result As DataResult(Of Fmd),
                                    ByVal fingerPosition As Integer) Handles EnrollmentControl.OnEnroll

        ' 🔹 prevent duplicate calls
        If alreadyEnrolled Then
            SendMessage("Duplicate OnEnroll ignored (empId=" & EmpIds & ")")
            Return
        End If

        alreadyEnrolled = True  ' mark as processed

        If enrollmentControl.Reader IsNot Nothing Then
            SendMessage("OnEnroll: " & enrollmentControl.Reader.Description.Name & ", finger " & fingerPosition)
        Else
            SendMessage("OnEnroll: No Reader Connected, finger " & fingerPosition)
        End If

        If result IsNot Nothing AndAlso result.Data IsNot Nothing Then
            Dim success = dbhelper.EnrollAndSave(EmpIds, FingerTypes, fingerPosition, result.Data)
            If success Then
                SendMessage("Fingerprint saved successfully for empId=" & EmpIds)
            Else
                SendMessage("Error saving fingerprint.")
            End If
        End If

        btnCancel.Enabled = False
    End Sub

    Private Sub enrollment_OnCancel(ByVal enrollmentControl As DPCtlUruNet.EnrollmentControl,
                                    ByVal result As Constants.ResultCode,
                                    ByVal fingerPosition As Integer) Handles EnrollmentControl.OnCancel
        SendMessage("OnCancel: " & If(enrollmentControl.Reader.Description.Name, "No Reader") & ", finger " & fingerPosition)
        btnCancel.Enabled = False
    End Sub

    Private Sub enrollment_OnCaptured(ByVal enrollmentControl As DPCtlUruNet.EnrollmentControl,
                                      ByVal captureResult As CaptureResult,
                                      ByVal fingerPosition As Integer) Handles EnrollmentControl.OnCaptured
        If enrollmentControl.Reader IsNot Nothing Then
            SendMessage("OnCaptured: " & enrollmentControl.Reader.Description.Name &
                        ", finger " & fingerPosition &
                        ", quality " & captureResult.Quality.ToString())
        Else
            SendMessage("OnCaptured: No Reader Connected, finger " & fingerPosition)
        End If

        If captureResult.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
            If FingerPrintReader.CurrentReader IsNot Nothing Then
                FingerPrintReader.CurrentReader.Dispose()
                FingerPrintReader.CurrentReader = Nothing
            End If

            enrollmentControl.Reader = Nothing
            MessageBox.Show("Error: " & captureResult.ResultCode.ToString())
            btnCancel.Enabled = False
        ElseIf captureResult.Data IsNot Nothing Then
            For Each fiv As Fid.Fiv In captureResult.Data.Views
                pbFingerprint.Image = FingerPrintReader.CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height)
            Next
        End If
    End Sub

    Private Sub enrollment_OnDelete(ByVal enrollmentControl As DPCtlUruNet.EnrollmentControl,
                                    ByVal result As Constants.ResultCode,
                                    ByVal fingerPosition As Integer) Handles EnrollmentControl.OnDelete
        SendMessage("OnDelete: " & If(enrollmentControl.Reader.Description.Name, "No Reader") & ", finger " & fingerPosition)
        FingerPrintReader.Fmds.Remove(fingerPosition)
    End Sub

    Private Sub enrollment_OnStartEnroll(ByVal enrollmentControl As DPCtlUruNet.EnrollmentControl,
                                         ByVal result As Constants.ResultCode,
                                         ByVal fingerPosition As Integer) Handles EnrollmentControl.OnStartEnroll
        SendMessage("OnStartEnroll: " & If(enrollmentControl.Reader.Description.Name, "No Reader") & ", finger " & fingerPosition)
        btnCancel.Enabled = True
        alreadyEnrolled = False ' reset flag when new enrollment starts
    End Sub
#End Region

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        If MessageBox.Show("Are you sure you want to cancel this enrollment?",
                           "Are You Sure?",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.Yes Then
            enrollmentControl.Cancel()
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub EnrollmentControl_FormClosed(ByVal sender As Object,
                                             ByVal e As EventArgs) Handles MyBase.Closed
        enrollmentControl.Cancel()
    End Sub

    Private Sub SendMessage(ByVal message As String)
        txtMessage.Text &= message & vbCrLf & vbCrLf
        txtMessage.SelectionStart = txtMessage.TextLength
        txtMessage.ScrollToCaret()
    End Sub
End Class
