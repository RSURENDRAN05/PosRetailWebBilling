Imports System.IO
Imports System.Drawing.Printing
Imports System.Runtime.InteropServices
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Microsoft.Win32


'Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6
Public Class PrintCommand
    Inherits System.Windows.Forms.Form
    Private WithEvents printButton As System.Windows.Forms.Button
    Private printFont As Font = New Font("Consolas", 10, FontStyle.Regular)
    Private printFontB As Font = New Font("Consolas", 10, FontStyle.Bold)
    Private streamToPrint As StreamReader
    Private Printername As String
    Dim _STR As String = M_Details._appPath & "\Reports"
    Dim _logpath As String = Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("POSAPP").GetValue("LogPath")
    Dim _WINCALL As New WindowsPrinter
    Dim _casdr As New RawPrinter
    Dim _dosPrintDesign As New _clsdotprinter

    Public Function _PrintItemMaster() As Boolean
        Try
            
            streamToPrint = New StreamReader(M_Details._appPath & "\Reports\ItemMaster.TXT")
            Try
                '    printFont = New Font("Lucida Console", 9, FontStyle.Bold)
                Dim pd As New PrintDocument()
                Dim printControl = New StandardPrintController
                AddHandler pd.PrintPage, AddressOf Me.pd_PrintPage1
                pd.PrinterSettings.PrinterName = _DotmatrixTemp._PrinterName
                WriteErroLog(_DotmatrixTemp._PrinterName)
                pd.PrintController = printControl
                pd.Print()
            Finally
                streamToPrint.Close()
                _casdr._paperCut(True)
            End Try
            Return True
        Catch ex As Exception
            WriteErroLog(ex.ToString)
            Return False
        End Try
    End Function
    Public Function _PayoutSingleEntry() As Boolean
        Try
            
            streamToPrint = New StreamReader(M_Details._appPath & "\Reports\WINPayOutSingle.TXT")
            Try
                '    printFont = New Font("Lucida Console", 9, FontStyle.Bold)
                Dim pd As New PrintDocument()
                Dim printControl = New StandardPrintController
                AddHandler pd.PrintPage, AddressOf Me.pd_PrintPage1
                pd.PrinterSettings.PrinterName = _DotmatrixTemp._PrinterName
                WriteErroLog(_DotmatrixTemp._PrinterName)
                pd.PrintController = printControl
                pd.Print()
            Finally
                streamToPrint.Close()
                _casdr._paperCut(True)
            End Try
            Return True
        Catch ex As Exception
            WriteErroLog(ex.ToString)
            Return False
        End Try
    End Function
    Public Function _biltoprint() As Boolean
        Try
           
            streamToPrint = New StreamReader(M_Details._appPath & "\Reports\WINPRINT.TXT")
            Try
                '    printFont = New Font("Lucida Console", 9, FontStyle.Bold)
                Dim pd As New PrintDocument()
                Dim printControl = New StandardPrintController
                AddHandler pd.PrintPage, AddressOf Me.pd_PrintPage
                pd.PrinterSettings.PrinterName = _DotmatrixTemp._PrinterName
                WriteErroLog(_DotmatrixTemp._PrinterName)
                pd.PrintController = printControl
                pd.Print()
            Finally
                streamToPrint.Close()
                _casdr._paperCut(True)
            End Try
            Return True
        Catch ex As Exception
            WriteErroLog(ex.ToString)
            Return False
        End Try
    End Function
    Public Function _Guestbiltoprint() As Boolean
        Try
            
            streamToPrint = New StreamReader(M_Details._appPath & "\Reports\GuestWINPRINT.TXT")
            Try
                '    printFont = New Font("Lucida Console", 9, FontStyle.Bold)

                Dim pd As New PrintDocument()
                Dim printControl = New StandardPrintController
                AddHandler pd.PrintPage, AddressOf Me.pd_PrintPage
                pd.PrinterSettings.PrinterName = _DotmatrixTemp._PrinterName
                WriteErroLog(_DotmatrixTemp._PrinterName)
                pd.PrintController = printControl
                pd.Print()
            Finally
                streamToPrint.Close()
                _casdr._paperCut(True)
                _casdr.OpenCashdrawer(False)
            End Try
            Return True
        Catch ex As Exception
            WriteErroLog(ex.ToString)
            Return False
        End Try
    End Function
    Public Function _PrintKotCopy() As Boolean
        Try
             
            streamToPrint = New StreamReader(M_Details._appPath & "\Reports\WINKOTPRINT.TXT")
            Try
                '    printFont = New Font("Lucida Console", 9, FontStyle.Bold)

                Dim pd As New PrintDocument()
                Dim printControl = New StandardPrintController
                AddHandler pd.PrintPage, AddressOf Me.pd_PrintPage
                pd.PrinterSettings.PrinterName = _DotmatrixTemp._PrinterName
                WriteErroLog(_DotmatrixTemp._PrinterName)
                pd.PrintController = printControl
                pd.Print()
            Finally
                streamToPrint.Close()
                _casdr._paperCut(True)
                _casdr.OpenCashdrawer(False)
            End Try
            Return True
        Catch ex As Exception
            WriteErroLog(ex.ToString)
            Return False
        End Try
    End Function

    Public Function _printCurrentStock(ByRef FileName As String) As Boolean
        Try

            
            streamToPrint = New StreamReader(M_Details._appPath & "\Reports\" & FileName & ".txt")
            Try
                '    printFont = New Font("Lucida Console", 9, FontStyle.Bold)
                Dim pd As New PrintDocument()
                Dim printControl = New StandardPrintController
                AddHandler pd.PrintPage, AddressOf Me.pd_PrintPage1
                pd.PrinterSettings.PrinterName = _DotmatrixTemp._PrinterName
                WriteErroLog(_DotmatrixTemp._PrinterName)
                pd.PrintController = printControl
                pd.Print()
            Finally
                streamToPrint.Close()
                _casdr._paperCut(True)
                _casdr.OpenCashdrawer(False)
            End Try
            Return True
        Catch ex As Exception
            WriteErroLog(ex.ToString)
            Return False
        End Try
    End Function
    Public Function _printShiftClose(ByRef ShiftNo As String, ByRef mode As String, ByRef _dateTime As DateTime) As Boolean
        Try
            If _dosPrintDesign._shiftCloseDesign(ShiftNo, mode, _dateTime) = True Then
                If mode = "F" Then
                    Return True
                End If
                If _GlobalSettings.PrintShiftClose = False Then
                    Return True
                End If
                
                streamToPrint = New StreamReader(M_Details._appPath & "\Reports\PrintShiftClose.txt")
                Try
                    '    printFont = New Font("Lucida Console", 9, FontStyle.Bold)
                    Dim pd As New PrintDocument()
                    Dim printControl = New StandardPrintController
                    AddHandler pd.PrintPage, AddressOf Me.pd_ReportPrint
                    pd.PrinterSettings.PrinterName = _DotmatrixTemp._PrinterName
                    WriteErroLog(_DotmatrixTemp._PrinterName)
                    pd.PrintController = printControl
                    pd.Print()
                Finally
                    streamToPrint.Close()
                    _casdr._paperCut(True)
                End Try
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteErroLog(ex.ToString)
            Return False
        End Try
    End Function
    Public Function _printDayClose(ByRef mode As String, ByRef dno As String) As Boolean
        Try

            If _dosPrintDesign._dayCloseDesign(dno, mode) = True Then
                If mode = "F" Then
                    Return True
                End If
                If _GlobalSettings.PrintShiftClose = False Then
                    Return True
                End If
                 
                streamToPrint = New StreamReader(M_Details._appPath & "\Reports\PrintDayClose.txt")
                Try
                    '    printFont = New Font("Lucida Console", 9, FontStyle.Bold)
                    Dim pd As New PrintDocument()
                    Dim printControl = New StandardPrintController
                    AddHandler pd.PrintPage, AddressOf Me.pd_ReportPrint
                    pd.PrinterSettings.PrinterName = _DotmatrixTemp._PrinterName
                    WriteErroLog(_DotmatrixTemp._PrinterName)
                    pd.PrintController = printControl
                    pd.Print()
                Finally
                    streamToPrint.Close()
                    _casdr._paperCut(True)
                End Try
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteErroLog(ex.ToString)
            Return False
        End Try
    End Function
    Private Sub pd_PrintPage(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        Dim linesPerPage As Single = 0.0F
        Dim sb As New UriBuilder
        Dim yPos As Single = 0
        Dim count As Integer = 0
        Dim leftMargin As Single = 0.5 'ev.MarginBounds.Left
        Dim topMargin As Single = 0.5 'ev.MarginBounds.Top
        Dim line As String = Nothing
        Dim offset As Integer = 5
        Dim _pictbox As New PictureBox

        ' Calculate the number of lines per page.
        linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics)
        ' Print each line of the file.

        ' Check for logo file (JPG first, then PNG as fallback)
        Dim logoPath As String = M_Details._appPath & "\Reports\1.jpg"
        If Not File.Exists(logoPath) Then
            logoPath = M_Details._appPath & "\Reports\1.png"
        End If
        _printHeaderDesign._logoPath = logoPath

        If _printHeaderDesign._logoState = "1" AndAlso File.Exists(_printHeaderDesign._logoPath) Then
            _pictbox.Load(_printHeaderDesign._logoPath)
            ' Calculate centered position and stretch to fit
            Dim logoWidth As Integer = 200
            Dim logoHeight As Integer = 80
            Dim logoX As Integer = (ev.PageBounds.Width - logoWidth) \ 2
            Dim logoY As Integer = 5
            ev.Graphics.DrawImage(_pictbox.Image, logoX, logoY, logoWidth, logoHeight)
        End If

        ' Old logo implementation (commented out)
        ' _printHeaderDesign._logoPath = M_Details._appPath & "\Reports\1.png"
        ' If _printHeaderDesign._logoState = "1" Then
        '     _pictbox.Load(_printHeaderDesign._logoPath)
        '     _pictbox.Size = New Size(300, 250)
        '     ev.Graphics.DrawImage(_pictbox.Image, 64, 0, 200, 150)
        ' End If

        While count < linesPerPage
            line = streamToPrint.ReadLine()
            If line Is Nothing Then
                Exit While
            End If
            If _printHeaderDesign._logoState = "1" Then
                yPos = topMargin + count * printFont.GetHeight(ev.Graphics) + 85
            Else
                yPos = topMargin + count * printFont.GetHeight(ev.Graphics)
            End If
            If count = 0 Then
                ev.Graphics.DrawString(line, New Font("Consolas", 12, FontStyle.Bold), Brushes.Black, leftMargin, yPos, New StringFormat())
            ElseIf count > 0 And count < 6 Then
                ev.Graphics.DrawString(line, New Font("Consolas", 11, FontStyle.Bold), Brushes.Black, leftMargin, yPos, New StringFormat())
            Else
                ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, New StringFormat())
            End If
            count += 1
        End While
        'count = 0
        'While count < 2
        '    ev.Graphics.DrawString(".", printFont, Brushes.Black, leftMargin, yPos, New StringFormat())
        '    count += 1
        'End While
        ' If more lines exist, print another page.
        If (line IsNot Nothing) Then
            ev.HasMorePages = True
        Else
            ev.HasMorePages = False
        End If
    End Sub
    Private Sub pd_PrintPage1(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        Dim linesPerPage As Single = 0.0F
        Dim yPos As Single = 0
        Dim count As Integer = 0
        Dim leftMargin As Single = 0.5 ' ev.MarginBounds.Left
        Dim topMargin As Single = 0.5 'ev.MarginBounds.Top
        Dim line As String = Nothing
        Dim offset As Integer = 5


        ' Calculate the number of lines per page.
        linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics)
        ' Print each line of the file.

        While count < linesPerPage
            line = streamToPrint.ReadLine()
            If line Is Nothing Then
                Exit While
            End If

            yPos = topMargin + count * printFont.GetHeight(ev.Graphics) + offset

            If count <= 1 Then
                ev.Graphics.DrawString(line, New Font("Consolas", 10, FontStyle.Bold), Brushes.Black, leftMargin, yPos, New StringFormat())
            Else
                ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, New StringFormat())
            End If
            count += 1
        End While
        ' If more lines exist, print another page.
        If (line IsNot Nothing) Then
            ev.HasMorePages = True
        Else
            ev.HasMorePages = False
        End If
    End Sub
    Private Sub pd_ReportPrint(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        Dim linesPerPage As Single = 0.0F
        Dim yPos As Single = 0
        Dim count As Integer = 0
        Dim leftMargin As Single = 0.5 ' ev.MarginBounds.Left
        Dim topMargin As Single = 0.5 'ev.MarginBounds.Top
        Dim line As String = Nothing
        Dim offset As Integer = 5


        ' Calculate the number of lines per page.
        linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics)
        ' Print each line of the file.

        While count < linesPerPage
            line = streamToPrint.ReadLine()
            If line Is Nothing Then
                Exit While
            End If
            yPos = topMargin + count * printFont.GetHeight(ev.Graphics) + offset
            If line.Length > 0 Then
                If line.ToString.Substring(0, 1) = "*" Then 'OrElse line.ToString.Substring(0, 1) = "=" OrElse line.ToString.Substring(0, 1) = "T" Then
                    ev.Graphics.DrawString(line, printFontB, Brushes.Black, leftMargin, yPos, New StringFormat())
                Else
                    ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, New StringFormat())
                End If
            End If

            '
            count += 1
        End While
        ' If more lines exist, print another page.
        If (line IsNot Nothing) Then
            ev.HasMorePages = True
        Else
            ev.HasMorePages = False
        End If
    End Sub
    'Public Function _biltoprint(ByVal _ds As DataSet, ByRef Erromsg As String, ByVal _PRINTMODE As String) As Boolean
    '    Try



    '        If _SETTINGOPT._PRINTMODE = "0" Then '0 DOS PRINT
    '            If _DOTCALL._dotdesign(_ds, _PRINTMODE, Erromsg) = False Then
    '                Return False
    '            Else
    '                If M_Details._AFTPRINT = "1" Then
    '                    Dim P As New Process
    '                    P.StartInfo.WorkingDirectory = _STR
    '                    P.StartInfo.FileName = "PT.BAT"
    '                    P.Start()
    '                    P.WaitForExit()
    '                    _casdr._paperCut(True)
    '                End If
    '            End If
    '        Else

    '            If _WINCALL._dotdesign(_ds, _PRINTMODE, Erromsg) = False Then
    '                Return False
    '            Else
    '                Try
    '                    If M_Details._AFTPRINT = "1" Then '
    '                        streamToPrint = New StreamReader(_STR & "\WINPRINT.TXT")
    '                        Try
    '                            printFont = New Font("Consolas", 10, FontStyle.Bold)
    '                            Dim pd As New PrintDocument()
    '                            AddHandler pd.PrintPage, AddressOf Me.pd_PrintPage
    '                            pd.PrinterSettings.PrinterName = _DotmatrixTemp._PrinterName
    '                            pd.Print()

    '                        Finally
    '                            streamToPrint.Close()
    '                            _casdr._paperCut(True)
    '                        End Try
    '                    End If

    '                Catch ex As Exception
    '                    MessageBox.Show(ex.Message)
    '                End Try

    '            End If
    '            'Dim _frmPrin As New rptSalePrint
    '            'If File.Exists(_AppPath & "\Reports\SalesPrint.repx") Then
    '            '    _frmPrin.LoadLayout(_AppPath & "\Reports\SalesPrint.repx")
    '            'End If
    '            '_frmPrin.DataSource = _ds.Tables(0)
    '            '_frmPrin.ShowPreviewMarginLines = False
    '            'Dim pt As New DevExpress.XtraReports.UI.ReportPrintTool(_frmPrin)
    '            'Printername = _posSettingDs.Tables(0).Rows(2).Item("value").ToString
    '            'pt.Print(Printername)
    '        End If
    '        Return True
    '    Catch ex As Exception
    '        Erromsg = ex.Message
    '        Return False
    '    End Try
    'End Function
    'Public Function _biltoDAILYprint(ByVal _ds As DataSet) As Boolean
    '    Try
    '        'If _SETTINGOPT._PRINTMODE = "0" Then
    '        'If _DOTCALL._dotDAILYdesign(_ds) = False Then
    '        Return False
    '        '  Else

    '        Dim p As New Process
    '        ' Dim _STR As String = _AppPath & "\Reports"
    '        P.StartInfo.WorkingDirectory = _STR
    '        P.StartInfo.FileName = "DAILY.BAT"
    '        P.Start()
    '        P.WaitForExit()
    '        ' End If
    '        'Else
    '        If _WINCALL._dotDAILYdesign(_ds) = False Then
    '            Return False
    '        Else
    '            Try
    '                'If M_Details._AFTPRINT = "1" Then '1 means windows printer
    '                streamToPrint = New StreamReader(_STR & "\WINDAILYPRINT.TXT")
    '                Try
    '                    printFont = New Font("Consolas", 10, FontStyle.Bold)
    '                    Dim pd As New PrintDocument()
    '                    AddHandler pd.PrintPage, AddressOf Me.pd_ReportPrint
    '                    pd.PrinterSettings.PrinterName = 0 '_DotmatrixTemp._PrinterName
    '                    pd.Print()
    '                Finally
    '                    streamToPrint.Close()
    '                End Try
    '                ' End If
    '            Catch ex2 As Exception
    '                MessageBox.Show(ex2.Message)
    '            End Try
    '        End If
    '        '  End If

    '        Return True
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Return False
    '    End Try
    'End Function
    'Public Function _COUNTERCLOSE(ByVal _ds As DataSet, ByRef Erromsg As String, ByVal _PRINTMODE As String) As Boolean
    '    Try




    '        If _SETTINGOPT._PRINTMODE = "0" Then
    '            If _DOTCALL._dotDAILYdesign(_ds) = False Then
    '                Return False
    '            Else

    '                Dim P As New Process
    '                Dim _STR As String = _AppPath & "\Reports"
    '                P.StartInfo.WorkingDirectory = _STR
    '                P.StartInfo.FileName = "DAILY.BAT"
    '                P.Start()
    '                P.WaitForExit()
    '            End If
    '        Else
    '            If _WINCALL._dotDAILYdesign(_ds) = False Then
    '                Return False
    '            Else
    '                Try
    '                    'If M_Details._AFTPRINT = "1" Then '1 means windows printer
    '                    streamToPrint = New StreamReader(_STR & "\WINDAILYPRINT.TXT")
    '                    Try
    '                        printFont = New Font("Consolas", 10, FontStyle.Bold)
    '                        Dim pd As New PrintDocument()
    '                        AddHandler pd.PrintPage, AddressOf Me.pd_ReportPrint
    '                        pd.PrinterSettings.PrinterName = _DotmatrixTemp._PrinterName
    '                        pd.Print()
    '                    Finally
    '                        streamToPrint.Close()
    '                    End Try
    '                    ' End If
    '                Catch ex As Exception
    '                    MessageBox.Show(ex.Message)
    '                End Try
    '            End If
    '        End If

    '        Return True
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Return False
    '    End Try
    'End Function
    'Public Function _biltoCUMprint(ByRef Erromsg As String, ByVal _PRINTMODE As String, ByRef _dtf As String, ByRef _dtTo As String) As Boolean
    '    Try

    '        If _SETTINGOPT._PRINTMODE = "0" Then
    '            If _DOTCALL._dotcumdesign(Erromsg) = False Then
    '                Return False
    '            Else
    '                Dim P As New Process
    '                Dim _STR As String = M_Details._appPath & "\Reports"
    '                P.StartInfo.WorkingDirectory = _STR
    '                P.StartInfo.FileName = "PRTWISE.BAT"
    '                P.Start()
    '                P.WaitForExit()
    '            End If
    '        Else
    '            If _WINCALL._dotcumdesign(Erromsg, _dtf, _dtTo) = False Then
    '                Return False
    '            Else
    '                Try
    '                    'streamToPrint.DiscardBufferedData()
    '                    ' If M_Details._AFTPRINT = "1" Then '1 means windows printer
    '                    streamToPrint = New StreamReader(_STR & "\WINPRODUCTPRINT.TXT")
    '                    Try
    '                        printFont = New Font("Consolas", 10, FontStyle.Bold)
    '                        Dim pd As New PrintDocument()
    '                        AddHandler pd.PrintPage, AddressOf Me.pd_ReportPrint
    '                        pd.PrinterSettings.PrinterName = _DotmatrixTemp._PrinterName
    '                        pd.Print()
    '                    Finally
    '                        streamToPrint.Close()
    '                    End Try
    '                    'End If
    '                Catch ex As Exception
    '                    MessageBox.Show(ex.Message)
    '                End Try
    '            End If
    '        End If
    '        Return True
    '    Catch ex As Exception
    '        Erromsg = ex.Message
    '        Return False
    '    End Try
    'End Function
    'Public Function _biltosummary(ByRef Erromsg As String, ByVal _PRINTMODE As String) As Boolean
    '    Try

    '        If _PRINTMODE = "DS" Then
    '            If _SETTINGOPT._PRINTMODE = "0" Then
    '                If _DOTCALL._dotcumdesign(" erromsg ", "0", "0") = False Then
    '                    Return False
    '                Else
    '                    Dim P As New Process
    '                    Dim _STR As String = _AppPath & "\Reports"
    '                    P.StartInfo.WorkingDirectory = _STR
    '                    P.StartInfo.FileName = "PRTWISE.BAT"
    '                    P.Start()
    '                    P.WaitForExit()
    '                End If
    '            Else
    '                If _WINCALL._dotsummaryDesign(Erromsg) = False Then
    '                    Return False
    '                Else
    '                    Try
    '                        'If M_Details._AFTPRINT = "1" Then '1 means windows printer
    '                        streamToPrint = New StreamReader(_STR & "\WINSUMMARY.TXT")
    '                        Try
    '                            printFont = New Font("Consolas", 10, FontStyle.Bold)
    '                            Dim pd As New PrintDocument()
    '                            AddHandler pd.PrintPage, AddressOf Me.pd_ReportPrint
    '                            pd.PrinterSettings.PrinterName = _DotmatrixTemp._PrinterName
    '                            pd.Print()
    '                        Finally
    '                            streamToPrint.Close()
    '                        End Try
    '                        'End If
    '                    Catch ex As Exception
    '                        MessageBox.Show(ex.Message)
    '                    End Try
    '                End If
    '            End If
    '        End If
    '        Return True
    '    Catch ex As Exception
    '        Erromsg = ex.Message
    '        Return False
    '    End Try

    'End Function
    Private Sub PrintCommand_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'PrintCommand
        '
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Name = "PrintCommand"
        Me.ResumeLayout(False)

    End Sub
    'Public Function _billTaxSummary(ByVal _dsrpt As DataSet, ByRef _FRMDATE As String, ByRef _TODATE As String) As Boolean
    '    Try

    '        If _SETTINGOPT._PRINTMODE = "0" Then
    '            'If _DOTCALL._dotcumdesign(" erromsg ", "0", "0") = False Then
    '            '    Return False
    '            'Else
    '            '    Dim P As New Process
    '            '    Dim _STR As String = _AppPath & "\Reports"
    '            '    P.StartInfo.WorkingDirectory = _STR
    '            '    P.StartInfo.FileName = "PRTWISE.BAT"
    '            '    P.Start()
    '            '    P.WaitForExit()
    '            'End If
    '        Else
    '            If _WINCALL._rptTaxsummary(_dsrpt, _FRMDATE, _TODATE) = False Then
    '                Return False
    '            Else
    '                Try
    '                    'If M_Details._AFTPRINT = "1" Then '1 means windows printer
    '                    streamToPrint = New StreamReader(_STR & "\WINTAXSUMMARY.TXT")
    '                    Try
    '                        printFont = New Font("Consolas", 10, FontStyle.Bold)
    '                        Dim pd As New PrintDocument()
    '                        AddHandler pd.PrintPage, AddressOf Me.pd_ReportPrint
    '                        pd.PrinterSettings.PrinterName = _DotmatrixTemp._PrinterName
    '                        pd.Print()
    '                    Finally
    '                        streamToPrint.Close()
    '                    End Try
    '                    'End If
    '                Catch ex As Exception
    '                    MessageBox.Show(ex.Message)
    '                End Try
    '            End If
    '        End If

    '        Return True
    '    Catch ex As Exception

    '        Return False
    '    End Try

    'End Function


    Friend TextToBePrinted As String

    Public Sub PrintText(ByVal text As String)
        Try
            TextToBePrinted = text
            Dim prn As New Printing.PrintDocument
            Dim printControl = New StandardPrintController
            Using (prn)
                prn.PrinterSettings.PrinterName = _DotmatrixTemp._PrinterName
                AddHandler prn.PrintPage, AddressOf Me.PrintPageHandler
                prn.PrintController = printControl
                prn.Print()
                'RemoveHandler prn.PrintPage, AddressOf Me.PrintPageHandler
            End Using
        Catch ex As Exception
            WriteErroLog(ex.ToString)
        End Try

    End Sub
    Private Sub PrintPageHandler(ByVal sender As Object, ByVal ev As Printing.PrintPageEventArgs)
        Dim linesPerPage As Single = 0.0F
        Dim sb As New UriBuilder
        Dim yPos As Single = 0
        Dim count As Integer = 0
        Dim leftMargin As Single = 0.5 'ev.MarginBounds.Left
        Dim topMargin As Single = 0.5 'ev.MarginBounds.Top
        Dim line As String = Nothing
        Dim offset As Integer = 5
        Dim _pictbox As New PictureBox

        ' Calculate the number of lines per page.
        linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics)
        ' Print each line of the file.
        '_printHeaderDesign._logoPath = M_Details._appPath & "\Reports\1.png"
        'If _printHeaderDesign._logoState = "1" Then
        '    _pictbox.Load(_printHeaderDesign._logoPath)
        '    _pictbox.Size = New Size(150, 100)
        '    EV.Graphics.DrawImage(_pictbox.Image, 64, 0, 160, 60)
        'End If

        'ev.Graphics.DrawString(TextToBePrinted, New Font("Consolas", 20, FontStyle.Bold), Brushes.Black, leftMargin, yPos, New StringFormat())

        ev.Graphics.DrawString(TextToBePrinted, New Font("Consolas", 10, FontStyle.Regular), Brushes.Black, leftMargin, yPos, New StringFormat())
        'count = 0
        'While count < 2
        '    ev.Graphics.DrawString(".", printFont, Brushes.Black, leftMargin, yPos, New StringFormat())
        '    count += 1
        'End While
        ' If more lines exist, print another page.
        If (line IsNot Nothing) Then
            ev.HasMorePages = True
        Else
            ev.HasMorePages = False
        End If
    End Sub
End Class
