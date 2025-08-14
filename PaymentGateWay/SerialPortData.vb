Imports System.IO.Ports
Imports Newtonsoft.Json
Imports System.Text
Imports System.Threading.Tasks
Public Class SerialPortData
    Dim mySerialPort As New SerialPort
    Dim responseData As Object = ""
    Function setupConnectCOM(ByRef postjson As String) As Boolean
        'Open COM and send measure command - this part works correctly
        'first, check if serial port is open
        Try

        
        If mySerialPort.IsOpen Then 'send measure command
            mySerialPort.Write(postjson) 'the instrument will generally take 15.1 sec to perform a measurement before sending the result back
        Else
            'if serial port is not open, set it up, then open, then send command
            'Setup COM --this part works correctly
            With mySerialPort
                .PortName = "COM10"
                .BaudRate = 115200
                .DataBits = 8
                .Parity = Parity.None
                .StopBits = StopBits.One
                    '.Handshake = Handshake.None
                .ReadTimeout = 16000
            End With
            AddHandler mySerialPort.DataReceived, AddressOf mySerialPort_DataReceived
            Try
                mySerialPort.Open()
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(ex.Message)
                Return False
                'Exit Function 'exit sub if the connection fails
                End Try
                Threading.Thread.Sleep(2000) 'wait 0.2 sec for port to open
                mySerialPort.Write(postjson) 'send measure command after serial port is open
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
   
    Private Sub mySerialPort_DataReceived(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)
        responseData = ""
        Dim jsonData As New PaymentJson
        jsonData.ResponseCode = "XX"
        jsonData.ResponseDescription = "No Data Transaction"
        Dim PostString As String = JsonConvert.SerializeObject(jsonData)
        Dim sp As SerialPort = CType(sender, SerialPort)
        responseData = sp.ReadExisting()
        sp.Close()
        Threading.Thread.Sleep(5000)
        If Not String.IsNullOrEmpty(responseData) Then
            _globalSetting.ResponseData = responseData
            _globalSetting.PaymentMachine = True
        Else
            _globalSetting.ResponseData = PostString
            _globalSetting.PaymentMachine = False
        End If
    End Sub
End Class
