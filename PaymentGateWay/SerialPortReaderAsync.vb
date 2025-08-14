Imports System.IO.Ports
Imports System.Text

Public Class SerialPortReaderAsync
    Private WithEvents serialPort As New SerialPort
    Dim responseData As Object = ""
    Public Function ReadFromSerialPortAsync(ByVal jsonData As String) As Object
        Dim receivedData As Object = ""
        Try

            If serialPort.IsOpen Then 'send measure command
                serialPort.Write(jsonData) 'the instrument will generally take 15.1 sec to perform a measurement before sending the result back
            Else
                'if serial port is not open, set it up, then open, then send command
                'Setup COM --this part works correctly
                With serialPort
                    .PortName = "COM10"
                    .BaudRate = 115200
                    .DataBits = 8
                    .Parity = Parity.None
                    .StopBits = StopBits.One
                    .Handshake = Handshake.None
                    .ReadTimeout = 16000
                End With
                AddHandler serialPort.DataReceived, AddressOf mySerialPort_DataReceived
                Try
                    serialPort.Open()
                Catch ex As Exception
                    System.Windows.Forms.MessageBox.Show(ex.Message)
                End Try
                Threading.Thread.Sleep(200) 'wait 0.2 sec for port to open

                serialPort.Write(jsonData) 'send measure command after serial port is open

            End If
            ' Read data asynchronously
            While True
                If Not String.IsNullOrEmpty(responseData) Then
                    receivedData = responseData
                End If
            End While
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        Finally
            If serialPort.IsOpen Then
                serialPort.Close()
            End If
        End Try

        Return receivedData.ToString()
    End Function

    Private Sub mySerialPort_DataReceived(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)
        Dim sp As SerialPort = CType(sender, SerialPort)
        responseData = sp.ReadExisting()
    End Sub
    ' Call this function to start reading
    Async Function StartReading(ByVal jsonData As String) As Task(Of Object)
        Dim receivedData As String = ""
        Dim responseData As Object = ""
        receivedData = jsonData
        responseData = Await ReadFromSerialPortAsync(receivedData)
        Return responseData
    End Function
End Class

