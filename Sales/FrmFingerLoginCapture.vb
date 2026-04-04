Imports System.IO
Imports DPFP
Imports DPFP.Capture
Imports DPFP.Processing
Imports DPFP.Verification

Public Class FrmFingerLoginCapture
    Inherits Form
    Implements DPFP.Capture.EventHandler

    Private Capturer As Capture

    Public Property MatchedUserId As String = ""
    Public Property MatchedFeatureSet As String = ""

    Private lblStatus As Label
    Private btnCancel As Button

    Public Sub New()
        Me.Text = "Fingerprint Login"
        Me.Size = New Size(350, 200)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        lblStatus = New Label()
        lblStatus.Text = "Place your finger on the reader..."
        lblStatus.Dock = DockStyle.Top
        lblStatus.Height = 80
        lblStatus.TextAlign = ContentAlignment.MiddleCenter
        lblStatus.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Me.Controls.Add(lblStatus)

        btnCancel = New Button()
        btnCancel.Text = "Cancel"
        btnCancel.Dock = DockStyle.Bottom
        btnCancel.Height = 40
        AddHandler btnCancel.Click, Sub(s, ev)
                                        Me.DialogResult = DialogResult.Cancel
                                        Me.Close()
                                    End Sub
        Me.Controls.Add(btnCancel)
    End Sub

    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
        Try
            Capturer = New Capture()
            Capturer.EventHandler = Me
            Capturer.StartCapture()
        Catch ex As Exception
            MessageBox.Show("Failed to start fingerprint capture: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Try
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)
        Try
            If Capturer IsNot Nothing Then
                Capturer.StopCapture()
            End If
        Catch
        End Try
    End Sub

    Private Sub UpdateStatus(text As String)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() lblStatus.Text = text)
        Else
            lblStatus.Text = text
        End If
    End Sub

    Public Sub OnComplete(ByVal capture As Object, ByVal ReaderSerialNumber As String, ByVal sample As Sample) Implements DPFP.Capture.EventHandler.OnComplete
        Try
            ' Extract features for verification
            Dim extractor As New FeatureExtraction()
            Dim feedBack As New DPFP.Capture.CaptureFeedback()
            Dim features As New FeatureSet()
            extractor.CreateFeatureSet(sample, DataPurpose.Verification, feedBack, features)

            If feedBack = CaptureFeedback.Good AndAlso features IsNot Nothing Then
                ' Authenticate using the shared method
                Dim userId As String = AuthenticateByFingerprint(features)

                If Not String.IsNullOrEmpty(userId) Then
                    MatchedUserId = userId
                    MatchedFeatureSet = "OK"
                    Me.Invoke(Sub()
                                  Me.DialogResult = DialogResult.OK
                                  Me.Close()
                              End Sub)
                Else
                    UpdateStatus("Fingerprint not recognized. Try again...")
                End If
            Else
                UpdateStatus("Poor quality. Place finger again...")
            End If
        Catch ex As Exception
            UpdateStatus("Error: " & ex.Message)
        End Try
    End Sub

    Public Sub OnFingerGone(ByVal capture As Object, ByVal ReaderSerialNumber As String) Implements DPFP.Capture.EventHandler.OnFingerGone
    End Sub

    Public Sub OnFingerTouch(ByVal capture As Object, ByVal ReaderSerialNumber As String) Implements DPFP.Capture.EventHandler.OnFingerTouch
        UpdateStatus("Scanning fingerprint...")
    End Sub

    Public Sub OnReaderConnect(ByVal capture As Object, ByVal ReaderSerialNumber As String) Implements DPFP.Capture.EventHandler.OnReaderConnect
    End Sub

    Public Sub OnReaderDisconnect(ByVal capture As Object, ByVal ReaderSerialNumber As String) Implements DPFP.Capture.EventHandler.OnReaderDisconnect
        UpdateStatus("Reader disconnected!")
    End Sub

    Public Sub OnSampleQuality(ByVal capture As Object, ByVal ReaderSerialNumber As String, ByVal feedback As CaptureFeedback) Implements DPFP.Capture.EventHandler.OnSampleQuality
        If feedback <> CaptureFeedback.Good Then
            UpdateStatus("Poor quality. Place finger again...")
        End If
    End Sub
End Class
