Imports System.Runtime.InteropServices
Imports System.Drawing.Printing
Imports System.Security

Module _cashdraw

    Public Class RawPrinter
        ' ----- Define the data type that supplies basic print job information to the spooler.
        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
        Public Structure DOCINFO
            <MarshalAs(UnmanagedType.LPWStr)> _
            Public pDocName As String
            <MarshalAs(UnmanagedType.LPWStr)> _
            Public pOutputFile As String
            <MarshalAs(UnmanagedType.LPWStr)> _
            Public pDataType As String
        End Structure
        Public Sub New()
        End Sub
        ' ----- Define interfaces to the functions supplied in the DLL.
        <DllImport("winspool.drv", EntryPoint:="OpenPrinterW", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Friend Shared Function OpenPrinter(ByVal printerName As String, ByRef hPrinter As IntPtr, ByVal printerDefaults As Integer) As Boolean
        End Function

        <DllImport("winspool.drv", EntryPoint:="ClosePrinter", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Friend Shared Function ClosePrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.drv", EntryPoint:="StartDocPrinterW", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Friend Shared Function StartDocPrinter(ByVal hPrinter As IntPtr, ByVal level As Integer, ByRef documentInfo As DOCINFO) As Boolean
        End Function

        <DllImport("winspool.drv", EntryPoint:="EndDocPrinter", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Friend Shared Function EndDocPrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.drv", EntryPoint:="StartPagePrinter", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Friend Shared Function StartPagePrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.drv", EntryPoint:="EndPagePrinter", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Friend Shared Function EndPagePrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.drv", EntryPoint:="WritePrinter", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Friend Shared Function WritePrinter(ByVal hPrinter As IntPtr, ByVal buffer As IntPtr, ByVal bufferLength As Integer, ByRef bytesWritten As Integer) As Boolean
        End Function

        Public Shared Function PrintRaw(ByVal printerName As String, ByVal origString As String) As Boolean
            ' ----- Send a string of  raw data to  the printer.
            Dim hPrinter As IntPtr
            Dim spoolData As New DOCINFO
            Dim dataToSend As IntPtr
            Dim dataSize As Integer
            Dim bytesWritten As Integer

            ' ----- The internal format of a .NET String is just
            '       different enough from what the printer expects
            '       that there will be a problem if we send it
            '       directly. Convert it to ANSI format before
            '       sending.
            dataSize = origString.Length()
            dataToSend = Marshal.StringToCoTaskMemAnsi(origString)

            ' ----- Prepare information for the spooler.
            spoolData.pDocName = "OpenDrawer" ' class='highlight'
            spoolData.pDataType = "RAW"

            Try
                ' ----- Open a channel to  the printer or spooler.
                Call OpenPrinter(printerName, hPrinter, 0)

                ' ----- Start a new document and Section 1.1.
                Call StartDocPrinter(hPrinter, 1, spoolData)
                Call StartPagePrinter(hPrinter)

                ' ----- Send the data to the printer.

                Call WritePrinter(hPrinter, dataToSend, _
                   dataSize, bytesWritten)

                ' ----- Close everything that we opened.
                EndPagePrinter(hPrinter)
                EndDocPrinter(hPrinter)
                ClosePrinter(hPrinter)
                PrintRaw = True
            Catch ex As Exception
                MsgBox("Error occurred: " & ex.ToString)
                PrintRaw = False
            Finally
                ' ----- Get rid of the special ANSI version.
                Marshal.FreeCoTaskMem(dataToSend)
            End Try
        End Function
        Public Sub OpenCashdrawer(ByRef a As Boolean)

            If a = True Then
                'Modify DrawerCode to your receipt printer open drawer code
                Dim DrawerCode As String = Chr(27) & Chr(112) & Chr(48) & Chr(64) & Chr(64)

                'Modify PrinterName to your receipt printer name
                Dim PrinterName As String = "MYPOS"
                RawPrinter.PrintRaw(PrinterName, DrawerCode)

                Dim cashDrawerCmd() As Byte = {27, 112, 0, 25, 250}
                ' sets up comPort as SerialPort and opens the port

                'Using comPort As System.IO.Ports.SerialPort = My.Computer.Ports.OpenSerialPort("COM3")

                '    comPort.BaudRate = 9600 'setup the com port with the required parameters. (Set these to suit)
                '    comPort.DataBits = 8
                '    comPort.Parity = System.IO.Ports.Parity.None
                '    comPort.StopBits = System.IO.Ports.StopBits.One
                '    comPort.Handshake = System.IO.Ports.Handshake.RequestToSend
                '    comPort.Write(cashDrawerCmd, 0, cashDrawerCmd.Length) ' writes the whole of the byte array containg the open drawer command codes to the port
                '    comPort.Close()
                'End Using

            End If
        End Sub
        Public Sub _paperCut(ByRef a As Boolean)
            If a = True Then

                'Modify DrawerCode to your receipt printer open drawer code
                Dim mstrPartialCutCode As String = Chr(27) & Chr(109)
                'Modify PrinterName to your receipt printer name
                Dim PrinterName As String = "MYPOS"
                RawPrinter.PrintRaw(PrinterName, mstrPartialCutCode)


                '==================================================================================================
                'Dim ADport As System.IO.Ports.SerialPort = My.Computer.Ports.OpenSerialPort("COM3")
                'ADport.BaudRate = 9600 'setup the com port with the required parameters. (Set these to suit)
                'ADport.DataBits = 8
                'ADport.Parity = System.IO.Ports.Parity.None
                'ADport.StopBits = System.IO.Ports.StopBits.One
                'ADport.Handshake = System.IO.Ports.Handshake.RequestToSend
                'Dim PartialCutCode As String = Chr(&H1D) & "V" & Chr(66) & Chr(0)
                'ADport.Write(PartialCutCode)


                'USB

                'Dim GS As String = Chr(29)
                'Dim Esc As String = Chr(27)

                'Dim comand As String = ""
                'comand = Esc + "@"
                'comand += GS + "V" + Chr(1)

                'Modify DrawerCode to your receipt printer open drawer code
                'Dim mPartialCutCode As String = Chr(&H1D) & "V" & Chr(66) & Chr(0)
                'Modify PrinterName to your receipt printer name
                'Dim PrinterName As String = "Generic / Text Only"
                'RawPrinter.PrintRaw(PrinterName, mPartialCutCode)
                '===========================================================================





                'Dim pd As New PrintDialog()
                'pd.PrinterSettings = New PrinterSettings()
                'If DialogResult.OK = pd.ShowDialog() Then
                '    Dim Errprint As String = "error"

                '    RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, mPartialCutCode, Errprint)
                'End If
            End If

        End Sub
    End Class
End Module





