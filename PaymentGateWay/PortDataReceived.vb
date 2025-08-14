Imports System.IO.Ports
Imports Newtonsoft.Json

'Class PortDataReceived
'    Public Shared Sub Main()
'        Dim mySerialPort As New SerialPort("COM10")

'        mySerialPort.BaudRate = 115200
'        mySerialPort.Parity = Parity.None
'        mySerialPort.StopBits = StopBits.One
'        mySerialPort.DataBits = 8
'        mySerialPort.Handshake = Handshake.None
'        mySerialPort.RtsEnable = True
'        Dim jsonData As New PaymentJson
'        jsonData.TransactionType = 1
'        jsonData.TransactionAmount = "1.00"
'        Dim PostString As String = JsonConvert.SerializeObject(jsonData)
'        AddHandler mySerialPort.DataReceived, AddressOf DataReceivedHandler

'        mySerialPort.Open()
'        mySerialPort.Write(PostString)
'        mySerialPort.Close()
'    End Sub

'    Private Shared Sub DataReceivedHandler(
'                        sender As Object,
'                        e As SerialDataReceivedEventArgs)
'        Dim sp As SerialPort = CType(sender, SerialPort)
'        Dim indata As String = sp.ReadExisting()
'        Console.WriteLine("Data Received:")
'        Console.Write(indata)
'    End Sub
'End Class

Public Class Measure2x_COM
    Dim mySerialPort As New SerialPort
    ' Dim CMD As String = "M" & vbCr 'statement telling instrument to measure
    Dim measureNo As Integer = 0 'counts the number of measure commands sent to the instrument

    Private Delegate Sub UpdateFormDeligate()
    Private UpdateFormDeligate1 As UpdateFormDeligate

    Dim sngReading As Single 'this is the reading received from the instrument as a single data type

    Public Sub setupConnectCOM()
        'Open COM and send measure command - this part works correctly
        'first, check if serial port is open
        Dim jsonData As New PaymentJson
        jsonData.TransactionType = 1
        jsonData.TransactionAmount = "1.00"
        Dim PostString As String = JsonConvert.SerializeObject(jsonData)
        If mySerialPort.IsOpen Then 'send measure command
            mySerialPort.Write(PostString) 'the instrument will generally take 15.1 sec to perform a measurement before sending the result back
        Else
            'if serial port is not open, set it up, then open, then send command
            'Setup COM --this part works correctly
            With mySerialPort
                .PortName = "COM10"
                .BaudRate = 115200
                .DataBits = 8
                .Parity = Parity.None
                .StopBits = StopBits.One
                .Handshake = Handshake.None
                .ReadTimeout = 16000
            End With
            AddHandler mySerialPort.DataReceived, AddressOf mySerialPort_DataReceived
            Try
                mySerialPort.Open()
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(ex.Message)
                Exit Sub 'exit sub if the connection fails
            End Try
            Threading.Thread.Sleep(200) 'wait 0.2 sec for port to open

            mySerialPort.Write(PostString) 'send measure command after serial port is open

        End If
        measureNo = 1
    End Sub
    Private Sub mySerialPort_DataReceived(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)

        Dim sp As SerialPort = CType(sender, SerialPort)
        Dim responseData As Object = sp.ReadExisting()
        If String.IsNullOrWhiteSpace(responseData) Then
         
        Else


        End If

        ''Handles serial port data received events
        ''UpdateFormDeligate1 = New UpdateFormDeligate(AddressOf UpdateDisplay)

        ''Read data as it comes back from serial port
        ''I had to do this in two steps because it, for some reason needs to read
        ''the +/- symbol as a Byte, then needs to read the ASCII measurement number
        ''the third part concatenates the data and converts it to a single type

        ''part 1 - read +/- symbol
        'Dim comBuffer As Byte()
        'Dim n As Integer = mySerialPort.BytesToRead 'find number of bytes in buff
        'comBuffer = New Byte(n - 1) {} 're-dimension storage buffer (n - 1)
        'mySerialPort.Read(comBuffer, 0, n) 'read data from the buffer

        ''part 2 - read ASCII measurement number
        'Dim comBuffer2 As String
        'comBuffer2 = mySerialPort.ReadTo(vbCr)

        ''part 3 - concatenate read data and convert to single type
        'Dim txtReading As String = Nothing
        'txtReading = System.Text.ASCIIEncoding.ASCII.GetString(comBuffer) & CStr(CInt(comBuffer2) / 10)
        'sngReading = CSng(txtReading)

        ''Call the update form deligate
        ''Visual Studio slightly changed this from the example on Microsoft's website that used a Windows Form
        ''I tried the code in a windows form and I get the same results
        '' Me.Invoke(UpdateFormDeligate1) 'call the deligate
    End Sub

    'Private Sub Invoke(updateFormDeligate1 As UpdateFormDeligate)
    '    Dim dsr As String = sngReading 'set the Result label in the ribbon to equal the received data value

    '    'now place the data received in the active cell in the worksheet
    '    Dim myApp As Excel.Application = Globals.ThisAddIn.Application
    '    Dim currentCell = myApp.ActiveCell
    '    currentCell.Value = sngReading

    '    'advance cell to the next cell
    '    Dim newCell = currentCell
    '    newCell = myApp.ActiveCell.Offset(1, 0)
    '    newCell.Select()
    '    currentCell = newCell

    '    'check if this was the first reading from the instrument
    '    'if it was the first reading, then send a second read command
    '    If measureNo = 1 Then
    '        measureNo = 2 'make sure to change measurement number to 2 to avoid infinite loop
    '        mySerialPort.Write(CMD) 'send command to measure to instrument
    '    End If
    'End Sub

    'the usage of this section changed from the Microsoft Windows Form example
    'in function, the mySerialPort_DataREceived(), Invoke(), and UpdateDisplay() functions do appear to be
    'working with the same results and same hangups
    Private Sub UpdateDisplay()

    End Sub

    'Private Sub btnMeasure_Click(sender As Object, e As RibbonControlEventArgs) Handles btnMeasure.Click
    '    setupConnectCOM() 'connect to COM and send first measure command
    'End Sub
End Class