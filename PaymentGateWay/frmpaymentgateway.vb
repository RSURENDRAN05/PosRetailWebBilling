Imports System.IO.Ports
Imports Newtonsoft.Json
Imports System.Threading
Imports Newtonsoft.Json.Linq

Public Class frmpaymentgateway

    Public Shared Sub Main()
        Dim mySerialPort As New SerialPort("COM10")

        mySerialPort.BaudRate = 115200
        mySerialPort.Parity = Parity.None
        mySerialPort.StopBits = StopBits.One
        mySerialPort.DataBits = 8
        AddHandler mySerialPort.DataReceived, AddressOf DataReceivedHandler

        mySerialPort.Open()
        Dim jsonData As New PaymentJson
        jsonData.TransactionType = 1
        jsonData.TransactionAmount = "1.00"
        Dim PostString As String = JsonConvert.SerializeObject(jsonData)
        mySerialPort.Write(PostString)

        mySerialPort.Close()
    End Sub

    Private Shared Sub DataReceivedHandler(sender As Object, e As SerialDataReceivedEventArgs)
        Dim sp As SerialPort = CType(sender, SerialPort)
        Dim indata As String = sp.ReadExisting()
        Console.WriteLine("Data Received:")
        Console.Write(indata)
    End Sub

    Private isOpened As Boolean = False
    Dim dictData As New Dictionary(Of String, Object)

    'Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
    '    If Not isOpened Then
    '        SerialPort1.PortName = "COM10"
    '        SerialPort1.BaudRate = 115200
    '        SerialPort1.DataBits = 8
    '        SerialPort1.Parity = 0
    '        SerialPort1.StopBits = 1

    '        Try
    '            ' Open the serial port.   
    '            SerialPort1.Open()
    '            isOpened = True
    '            AddHandler SerialPort1.DataReceived, AddressOf ttest_DataReceived
    '            ' ttest_DataReceived(sender, e)
    '        Catch
    '            MessageBox.Show("Failed to open serial port!")
    '        End Try
    '    Else

    '        Try
    '            ' Close the serial port.  
    '            SerialPort1.Close()
    '            isOpened = False
    '        Catch
    '            MessageBox.Show("Failed to close the serial port!")
    '        End Try
    '    End If
    'End Sub
    Private Sub ttest_DataReceived(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            Dim responseData As Object = ""

            dictData.Clear()
            dialog.Caption = "Conntecting Server.."
            'Dim jsonData As New PaymentJson
            'jsonData.TransactionType = 1
            'jsonData.TransactionAmount = "1.00"
            'Dim PostString As String = JsonConvert.SerializeObject(jsonData)
            'SerialPort1.Write(PostString)
            If e.EventType = SerialData.Eof Then
                responseData = serialPort.ReadLine
            End If
            'If JsonPost.postData(dictData, responseData) = True Then
            '    dialog.Caption = "Waiting For Aproval.."
            '    If responseData("ResponseCode").ToString = "00" Then
            '        dialog.Caption = "Payment Success"
            '    Else
            '        dialog.Caption = "Payment Declined"
            '        Exit Try
            '    End If
            'Else
            '    dialog.Caption = "Payment Not Success.."
            'End If

        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub



    Private serialPort As New SerialPort()
    ''Private Sub Button1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
    ''    Dim dialog As New DevExpress.Utils.WaitDialogForm()
    ''    Try
    ''        Dim responseData As Object = ""
    ''        If Not isOpened Then
    ''            ' Set serial port properties

    ''            serialPort.PortName = "COM10"
    ''            serialPort.BaudRate = 115200
    ''            serialPort.Parity = Parity.None
    ''            serialPort.DataBits = 8
    ''            serialPort.StopBits = StopBits.One
    ''            ' Open the serial port
    ''            Try
    ''                dialog.Caption = "Connecting To USB Port"
    ''                serialPort.Open()
    ''                isOpened = True
    ''                Dim jsonData As New PaymentJson
    ''                jsonData.TransactionType = 1
    ''                jsonData.TransactionAmount = "1.00"
    ''                Dim PostString As String = JsonConvert.SerializeObject(jsonData)
    ''                ' Write data to the serial port
    ''                serialPort.Write(PostString)
    ''                ' Read data from the serial port
    ''                responseData = serialPort.ReadLine()
    ''                dialog.Caption = "Reading Data.."
    ''            Catch ex As Exception
    ''                dialog.Caption = "Failed to open serial port!"
    ''                Exit Sub
    ''            End Try
    ''            ' Close the serial port
    ''            If String.IsNullOrWhiteSpace(responseData) Then
    ''                dialog.Caption = "Data Received.."
    ''                serialPort.Close()
    ''            Else
    ''                dialog.Caption = "Port Is Closing.."
    ''                serialPort.Close()
    ''            End If
    ''        End If
    ''    Catch ex As Exception
    ''        dialog.Close()
    ''    Finally
    ''        dialog.Close()
    ''    End Try


    ''End Sub
    ''Private Async Sub btnSubMain_Click(sender As Object, e As EventArgs) Handles btnSubMain.Click
    ''    Dim answer As Integer
    ''    Try
    ''        btnSubMain.Enabled = False
    ''        ' async call, UI continues to run
    ''        answer = Await SomeIntegerAsync()
    ''    Finally
    ''        btnSubMain.Enabled = True
    ''    End Try
    ''    MessageBox.Show(answer.ToString())

    ''    Try
    ''        btnSubMain.Enabled = False
    ''        ' synchronous call, UI is blocked
    ''        answer = SomeInteger()
    ''    Finally
    ''        btnSubMain.Enabled = True
    ''    End Try
    ''    MessageBox.Show(answer.ToString())
    ''End Sub

    ''Function SomeIntegerAsync() As Task(Of Integer)
    ''    Return Task.Run(AddressOf SomeInteger)
    ''End Function

    ''Function SomeInteger() As Integer
    ''    Thread.Sleep(5000)
    ''    Return 34
    ''End Function

    'Private Async Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
    '    Dim resdata As String
    '    Try
    '        resdata = Await SerialPortDataAsync()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Function SerialPortDataAsync() As Task(Of String)
    '    Return Task.Run(AddressOf Data_Received)
    'End Function

    'Function Data_Received() As String
    '    Dim dialog As New DevExpress.Utils.WaitDialogForm()
    '    Try
    '        Dim responseData As String = ""
    '        If Not isOpened Then
    '            ' Set serial port properties

    '            serialPort.PortName = "COM10"
    '            serialPort.BaudRate = 115200
    '            serialPort.Parity = Parity.None
    '            serialPort.DataBits = 8
    '            serialPort.StopBits = StopBits.One
    '            ' Open the serial port
    '            Try
    '                dialog.Caption = "Connecting To USB Port"
    '                serialPort.Open()
    '                isOpened = True
    '                Dim jsonData As New PaymentJson
    '                jsonData.TransactionType = 1
    '                jsonData.TransactionAmount = "1.00"
    '                Dim PostString As String = JsonConvert.SerializeObject(jsonData)
    '                ' Write data to the serial port
    '                serialPort.Write(PostString)
    '                Thread.Sleep(5000)
    '                ' Read data from the serial port
    '                responseData = serialPort.ReadLine()
    '                If String.IsNullOrWhiteSpace(responseData) Then
    '                    dialog.Caption = "Data Received.."
    '                    serialPort.Close()
    '                    Return responseData
    '                Else
    '                    dialog.Caption = "Port Is Closing.."
    '                    serialPort.Close()
    '                End If
    '                dialog.Caption = "Reading Data.."
    '            Catch ex As Exception
    '                dialog.Caption = "Failed to open serial port!"
    '            End Try
    '            ' Close the serial port

    '        End If
    '        Return responseData
    '    Catch ex As Exception
    '        Return 0
    '        dialog.Close()
    '    Finally
    '        dialog.Close()
    '    End Try
    'End Function
    Function ReceiveSerialData() As String
        ' Receive strings from a serial port.
        Dim returnStr As String = ""
        serialPort.PortName = "COM10"
        serialPort.BaudRate = 115200
        serialPort.Parity = Parity.None
        serialPort.DataBits = 8
        serialPort.StopBits = StopBits.One
        serialPort.Open()
        ' isOpened = True
        Dim jsonData As New PaymentJson
        jsonData.TransactionType = 1
        jsonData.TransactionAmount = "1.00"
        Dim PostString As String = JsonConvert.SerializeObject(jsonData)
        ' Write data to the serial port
        serialPort.Write(PostString)
        AddHandler serialPort.DataReceived, AddressOf DataReceivedHandler
        ''Try
        ''    'serialPort = My.Computer.Ports.OpenSerialPort("COM10")
        ''    serialPort.ReadTimeout = 20000
        ''    Do
        ''        Dim Incoming As String = serialPort.ReadLine()
        ''        If Incoming Is Nothing Then
        ''            Exit Do
        ''        Else
        ''            returnStr &= Incoming & vbCrLf
        ''        End If
        ''    Loop
        'Catch ex As TimeoutException
        '    returnStr = "Error: Serial Port read timed out."
        'Finally
        '    If serialPort IsNot Nothing Then serialPort.Close()
        'End Try

        Return returnStr
    End Function

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Try
            Dim strs As String
            strs = ReceiveSerialData()
        Catch ex As Exception

        End Try
    End Sub
    Dim btnclick As Boolean = False
    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            If btnclick = True Then
                dialog.Caption = "Already Proceessing.."
            Else
                btnclick = False
            End If
            Dim str As Object = ""
            Dim jsonData As New PaymentJson
            jsonData.TransactionType = 7
            jsonData.TransactionAmount = "1.50"
            Dim PostString As String = JsonConvert.SerializeObject(jsonData)
            Dim d As New SerialPortData

            If d.setupConnectCOM(PostString) = True Then
                dialog.Caption = "Connecting to USB.."
                btnclick = True
                Threading.Thread.Sleep(6000)
                dialog.Caption = "Proceessing.."
                Threading.Thread.Sleep(6000)
                If _globalSetting.PaymentMachine = True Then
                    dialog.Caption = "Data Received"
                    If Not String.IsNullOrEmpty(_globalSetting.ResponseData) Then
                        Dim Userparsejson As JObject = JObject.Parse(_globalSetting.ResponseData)
                        If Userparsejson("ResponseCode").ToString = "SHC005" Then
                            dialog.Caption = Userparsejson("ResponseDescription").ToString
                            'MessageBox.Show(Userparsejson("ResponseDescription").ToString)
                        ElseIf Userparsejson("ResponseCode").ToString = "00" Then
                            dialog.Caption = Userparsejson("ResponseDescription").ToString
                            'MessageBox.Show("QR Pay")
                        ElseIf Userparsejson("ResponseCode").ToString = "0000" Then
                            dialog.Caption = Userparsejson("ResponseDescription").ToString
                            'MessageBox.Show("QR Pay")
                        ElseIf Userparsejson("ResponseCode").ToString = "XX" Then
                            dialog.Caption = Userparsejson("ResponseDescription").ToString
                            'MessageBox.Show(Userparsejson("ResponseDescription").ToString)
                        Else
                            Exit Sub
                        End If
                    End If
                    btnclick = False
                Else
                    btnclick = False
                End If
            End If
        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
       
    End Sub
End Class

Public Class PaymentJson
    Public Property TransactionType As String
    Public Property TransactionAmount As String
    Public Property ResponseCode As String
    Public Property ResponseDescription As String
End Class
