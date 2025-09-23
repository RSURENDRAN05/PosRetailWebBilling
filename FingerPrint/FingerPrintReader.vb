Imports System.Threading
Imports System.Collections
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Imaging
Imports DPUruNet
Imports DPUruNet.Constants

Module FingerPrintReader
 
    'Public Property Fmds() As Dictionary(Of Int16, Fmd)
    '    Get
    '        Return _fmds
    '    End Get
    '    Set(ByVal value As Dictionary(Of Int16, Fmd))
    '        _fmds = value
    '    End Set
    'End Property
    'Private _fmds As Dictionary(Of Int16, Fmd) = New Dictionary(Of Int16, Fmd)
    

    ' Class to hold employee fingerprint info
    Public Class EmployeeFinger
        Public Property FingerTemplate As Fmd
        Public Property EmpId As Integer
        Public Property EmpName As String
        Public Property FingerName As String
    End Class

    ' Dictionary to store finger index → EmployeeFinger
    Private _fmds As Dictionary(Of Int16, EmployeeFinger) = New Dictionary(Of Int16, EmployeeFinger)

    Public Property Fmds() As Dictionary(Of Int16, EmployeeFinger)
        Get
            Return _fmds
        End Get
        Set(ByVal value As Dictionary(Of Int16, EmployeeFinger))
            _fmds = value
        End Set
    End Property

    ''' <summary>
    ''' Reset the UI causing the user to reselect a reader.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Reset() As Boolean
        Get
            Return _reset
        End Get
        Set(ByVal value As Boolean)
            _reset = value
        End Set
    End Property
    Private _reset As Boolean
    Private _readers As ReaderCollection
    ' When set by child forms, shows s/n and enables buttons.
    Public Property CurrentReader() As Reader
        Get
            Return _currentReader
        End Get
        Set(ByVal value As Reader)
            _currentReader = value
            ' Here you can notify UI if you want
            'SendMessage(Action.UpdateReaderState, value)
        End Set
    End Property
    Private _currentReader As Reader

    Function CurrentReaderData() As Boolean
        Try
            _readers = ReaderCollection.GetReaders()
            If _readers IsNot Nothing AndAlso _readers.Count > 0 Then
                ' ✅ pick the first available Reader object
                _currentReader = _readers(0)

                ' ✅ if you want the serial number as text:
                Dim serial As String = _currentReader.Description.SerialNumber
                'txtReaderSelected.Text = serial   ' show in UI
                Return True
            Else
                MessageBox.Show("No fingerprint readers detected.")
            End If
            Return True
        Catch ex As Exception
            Return False
            MessageBox.Show("Error getting reader: " & ex.Message)
        End Try
    End Function
     



    ''' <summary>
    ''' Open a device and check result for errors.
    ''' </summary>
    ''' <returns>Returns true if successful; false if unsuccessful</returns>
    Public Function OpenReader() As Boolean
        Reset = False
        Dim result As Constants.ResultCode = Constants.ResultCode.DP_DEVICE_FAILURE

        result = _currentReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE)

        If result <> Constants.ResultCode.DP_SUCCESS Then
            MessageBox.Show("Error:  " & result.ToString())
            Reset = True
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Hookup capture handler and start capture.
    ''' </summary>
    ''' <param name="OnCaptured">Delegate to hookup as handler of the On_Captured event</param>
    ''' <returns>Returns true if successful; false if unsuccessful</returns>
    Public Function StartCaptureAsync(ByVal OnCaptured As Reader.CaptureCallback) As Boolean
        AddHandler _currentReader.On_Captured, OnCaptured

        If Not CaptureFingerAsync() Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Cancel the capture and then close the reader.
    ''' </summary>
    ''' <param name="OnCaptured">Delegate to unhook as handler of the On_Captured event </param>
    Public Sub CancelCaptureAndCloseReader(ByVal OnCaptured As Reader.CaptureCallback)
        If _currentReader IsNot Nothing Then
            ' Dispose of reader handle and unhook reader events.
            CurrentReader.Dispose()

            If (Reset) Then
                CurrentReader = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Check the device status before starting capture.
    ''' </summary>
    ''' <returns></returns>
    Public Sub GetStatus()
        Dim result = _currentReader.GetStatus()

        If (result <> ResultCode.DP_SUCCESS) Then
            If CurrentReader IsNot Nothing Then
                Reset = True
                Throw New Exception("" & result.ToString())
            End If
        End If

        If (_currentReader.Status.Status = ReaderStatuses.DP_STATUS_BUSY) Then
            Thread.Sleep(50)
        ElseIf (_currentReader.Status.Status = ReaderStatuses.DP_STATUS_NEED_CALIBRATION) Then
            _currentReader.Calibrate()
        ElseIf (_currentReader.Status.Status <> ReaderStatuses.DP_STATUS_READY) Then
            Throw New Exception("Reader Status - " & CurrentReader.Status.Status.ToString())
        End If
    End Sub

    ''' <summary>
    ''' Check quality of the resulting capture.
    ''' </summary>
    Public Function CheckCaptureResult(ByVal captureResult As CaptureResult) As Boolean
        If captureResult.Data Is Nothing Then
            If captureResult.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
                Reset = True
                Throw New Exception("" & captureResult.ResultCode.ToString())
            End If

            If captureResult.Quality <> Constants.CaptureQuality.DP_QUALITY_CANCELED Then
                Throw New Exception("Quality - " & captureResult.Quality.ToString())
            End If
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Function to capture a finger. Always get status first and calibrate or wait if necessary.  Always check status and capture errors.
    ''' </summary>
    ''' <param name="fid"></param>
    ''' <returns></returns>
    Public Function CaptureFingerAsync() As Boolean
        Try
            GetStatus()

            Dim captureResult = _currentReader.CaptureAsync(Formats.Fid.ANSI, _
                                                   CaptureProcessing.DP_IMG_PROC_DEFAULT, _
                                                    _currentReader.Capabilities.Resolutions(0))

            If captureResult <> ResultCode.DP_SUCCESS Then
                Reset = True
                Throw New Exception("" + captureResult.ToString())
            End If

            Return True
        Catch ex As Exception
            MessageBox.Show("Error:  " & ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Create a bitmap from raw data in row/column format.
    ''' </summary>
    ''' <param name="bytes"></param>
    ''' <param name="width"></param>
    ''' <param name="height"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateBitmap(ByVal bytes As [Byte](), ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim rgbBytes As Byte() = New Byte(bytes.Length * 3 - 1) {}

        For i As Integer = 0 To bytes.Length - 1
            rgbBytes((i * 3)) = bytes(i)
            rgbBytes((i * 3) + 1) = bytes(i)
            rgbBytes((i * 3) + 2) = bytes(i)
        Next
        Dim bmp As New Bitmap(width, height, PixelFormat.Format24bppRgb)

        Dim data As BitmapData = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.[WriteOnly], PixelFormat.Format24bppRgb)

        For i As Integer = 0 To bmp.Height - 1
            Dim p As New IntPtr(data.Scan0.ToInt64() + data.Stride * i)
            System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3)
        Next

        bmp.UnlockBits(data)

        Return bmp
    End Function
    'Private Sub Serilize()
    '    Try
    '        For Each kvp As KeyValuePair(Of Int16, Fmd) In FingerPrintReader.Fmds
    '            Dim fingerPosition As Int16 = kvp.Key
    '            Dim fmd As Fmd = kvp.Value
    '            Dim base64 As String = Convert.ToBase64String(fmd.Bytes) ' Get bytes, then Base64

    '            ' Pass emp_id, fingerPosition, base64 to PHP/MySQL
    '            ' e.g., Using WebClient or HttpClient to POST to PHP endpoint
    '        Next
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub Deserlize()
    '    Try
    '        Dim _fmds As New Dictionary(Of Int16, Fmd)

    '        For Each row In _JsonData.FingerPrintDataTable.Rows ' dbRows is your data from MySQL
    '            Dim fingerPosition As Int16 = Convert.ToInt16(row("finger_position"))
    '            Dim base64 As String = row("fmd_data")
    '            Dim bytes As Byte() = Convert.FromBase64String(base64)

    '            ' Re-create Fmd object; if constructor is available as shown:
    '            Dim fmd As Fmd = New Fmd(bytes, Constants.Formats.Fmd.ANSI)

    '            _fmds.Add(fingerPosition, fmd)
    '        Next
    '        FingerPrintReader.Fmds.Clear()
    '        ' Now set property if needed
    '        FingerPrintReader.Fmds = _fmds
    '    Catch ex As Exception

    '    End Try
    'End Sub
#Region "SendMessage"
    Private Enum Action
        UpdateReaderState
    End Enum
    Private Delegate Sub SendMessageCallback(ByVal state As Action, ByVal payload As Object)
    'Private Sub SendMessage(ByVal state As Action, ByVal payload As Object)
    '    On Error Resume Next

    '    If Me.txtReaderSelected.InvokeRequired Then
    '        Dim d As New SendMessageCallback(AddressOf SendMessage)
    '        Me.Invoke(d, New Object() {state, payload})
    '    Else

    '        Select Case state
    '            Case Action.UpdateReaderState
    '                Dim _reader As Reader = (DirectCast(payload, Reader))
    '                If (_reader IsNot Nothing) Then
    '                    txtReaderSelected.Text = _reader.Description.SerialNumber
    '                    btnCapture.Enabled = True
    '                    btnStreaming.Enabled = True
    '                    btnVerify.Enabled = True
    '                    btnIdentify.Enabled = True
    '                    btnEnroll.Enabled = True
    '                    btnEnrollmentControl.Enabled = True
    '                    If _fmds.Count > 0 Then
    '                        btnIdentificationControl.Enabled = True
    '                    End If
    '                ElseIf (_reader Is Nothing) Then
    '                    _currentReader.Dispose()
    '                    _currentReader = Nothing
    '                    txtReaderSelected.Text = String.Empty
    '                    btnCapture.Enabled = False
    '                    btnStreaming.Enabled = False
    '                    btnVerify.Enabled = False
    '                    btnIdentify.Enabled = False
    '                    btnEnroll.Enabled = False
    '                    btnEnrollmentControl.Enabled = False
    '                    btnIdentificationControl.Enabled = False
    '                End If
    '        End Select

    '    End If
    'End Sub
#End Region

End Module
