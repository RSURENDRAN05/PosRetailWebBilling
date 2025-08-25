Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.DataTableExtensions
Imports DevExpress.XtraEditors
Imports DevExpress.XtraBars
Imports PdfSharp
Imports PdfSharp.Drawing
Imports PdfSharp.Pdf
Imports System.Runtime.InteropServices

Public Class _clsdotprinter
    Implements IDisposable
    Dim rtb As New RichTextBox
    Private _err As String
    Private _DATEFROM_1, _DATETO_1 As String
    Private _DSS, _DSS1, _dsshiftClose As New DataSet
    Dim readFile As TextReader
    Dim pdf As PdfDocument
    Protected Overridable Overloads Sub Dispose(disposing As Boolean)
        If disposing Then
            GC.Collect()
            GC.WaitForPendingFinalizers()
            If Environment.OSVersion.Platform = PlatformID.Win32NT Then
                NativeMethods.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1)
            End If
            GC.SuppressFinalize(Me)
        End If
        ' free native resources
    End Sub 'D
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub 'Dispose
    Public Sub New()
        Dispose()
    End Sub
    Friend NotInheritable Class NativeMethods
        <DllImport("kernel32.dll", CharSet:=CharSet.Auto, ExactSpelling:=True)>
        Friend Shared Function SetProcessWorkingSetSize(ByVal hProcess As IntPtr, ByVal dwMinimumWorkingSetSize As Int32, ByVal dwMaximumWorkingSetSize As Int32) As Integer

        End Function
    End Class
    Public Function _checkPrintOption(ByRef code As String, ByRef Mode As String) As Boolean
        Try
            If Mode = "S" OrElse Mode = "D" Then
                Dim _reCode As String = code
                Dim _getBoolean As String = ""
                Dim tabreturn As New DataTable
                Dim _ms As New DataSet
                _ms = _sqlDataAdapter("sp_printmenu_select", Nothing, _err)
                If _ms.Tables(0).Rows.Count > 0 Then
                    Dim _query = From _rows In _ms.Tables(0).AsEnumerable() Where _rows.Field(Of String)("Code") = _reCode And _rows.Field(Of Integer)("menuRights") = 1 Select _rows

                    If _query.Count = 1 Then
                        tabreturn = _query.CopyToDataTable
                        _getBoolean = tabreturn.Rows(0)("MenuRights").ToString
                        If _getBoolean = "1" Then
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return True
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function _shiftCloseDesign(ByRef shiftno As Integer, ByRef Mode As String, ByRef _dateTime As DateTime) As Boolean
        Try

            rtb.Text = ""
            _dsshiftClose = New DataSet
            'If _saleSetting._shiftclose = True Then
            Dim _SqlShitClosePrint(1) As SqlParameter
            _SqlShitClosePrint(0) = New SqlParameter("@mode", "S")
            _SqlShitClosePrint(1) = New SqlParameter("@shiftno", shiftno)
            _dsshiftClose = _sqlDataAdapter2("sp_shiftclose_print", _SqlShitClosePrint)
            _dsshiftClose.Tables(0).TableName = "ShiftClose"
            _dsshiftClose.Tables(1).TableName = "InvoiceHdr"
            _dsshiftClose.Tables(2).TableName = "InvoiceDtl"
            _dsshiftClose.Tables(3).TableName = "MainGroup"
            _dsshiftClose.Tables(4).TableName = "SubGroup"
            _dsshiftClose.Tables(5).TableName = "SalesMethod"
            _dsshiftClose.Tables(6).TableName = "Payouts"
            _dsshiftClose.Tables(7).TableName = "DeleteItem"
            _dsshiftClose.Tables(8).TableName = "Staffwise"
            _dsshiftClose.Tables(9).TableName = "Tablewise"
            _dsshiftClose.Tables(10).TableName = "Counter"
            _dsshiftClose.Tables(11).TableName = "PendingTable"
            _dsshiftClose.Tables(12).TableName = "HourlySales"
            _dsshiftClose.Tables(13).TableName = "PrintNotes"
            _dsshiftClose.Tables(14).TableName = "CurrentStock"
            If _dsshiftClose.Tables(0).Rows.Count > 0 Then
                Dim _content As New StringBuilder
                Dim _ItemList As New StringBuilder
                Dim _Tablecaption As String = String.Empty
                Dim _EMPTY As String = String.Empty
                Dim _TXT1 As String = "Shift Close Report"
                Dim _TXT2 As String = Environment.MachineName
                Dim _dot1 As String = "--------------------------------------"
                Dim _dot2 As String = "**************************************"
                Dim _dot3 As String = "++++++++++++++++++++++++++++++++++++++"
                Dim _dot4 As String = "======================================"
                Dim _ShopName As String = M_Details._shopName & vbNewLine & _printHeaderDesign._address
                Dim _sumManagment As Double = 0

                'Dim _shiftcloseDate As String = _dsshiftClose.Tables("ShiftClose").Rows(0)("psc_updated").ToString
                'Dim _sales As Double = _dsshiftClose.Tables("ShiftClose").Rows(0)("psc_todaysales")
                'Dim _discount As Double = _dsshiftClose.Tables("ShiftClose").Rows(0)("psc_totdiscount")
                'Dim _taxsales As Double = _dsshiftClose.Tables("ShiftClose").Rows(0)("psc_tottax")
                'Dim _totalsales As Double = _dsshiftClose.Tables("ShiftClose").Rows(0)("psc_netamt")
                Dim _shiftcloseDate As String = _dsshiftClose.Tables("ShiftClose").Rows(0)("psc_updated").ToString
                Dim _shiftDate As String = _dsshiftClose.Tables("ShiftClose").Rows(0)("psc_curdate").ToString
                Dim _shiftOpenDate As String = _dsshiftClose.Tables("ShiftClose").Rows(0)("psc_created").ToString
                Dim _sales As Double = _dsshiftClose.Tables("ShiftClose").AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("psc_todaysales"))
                Dim _discount As Double = _dsshiftClose.Tables("ShiftClose").AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("psc_totdiscount"))
                Dim _taxsales As Double = _dsshiftClose.Tables("ShiftClose").AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("psc_tottax"))
                Dim _totalsales As Double = _dsshiftClose.Tables("ShiftClose").AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("psc_netamt"))
                Dim _serviceTax As Double = _dsshiftClose.Tables("ShiftClose").AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("psc_servicetax"))
                Dim CashSales As Decimal = _dsshiftClose.Tables("InvoiceHdr").AsEnumerable().Where(Function(row) row.Field(Of String)("psih_invoice_paymode") = "CASH").Sum(Function(row) row.Field(Of Decimal)("psih_invoice_tnetamt"))
                _sumManagment = _dsshiftClose.Tables("InvoiceHdr").AsEnumerable().Where(Function(row) row.Field(Of String)("psih_invoice_paymode") = "MGMT").Sum(Function(row) row.Field(Of Decimal)("psih_invoice_tnetamt"))
                Dim _sumManagmentTaxamt As Double = 0
                _sumManagmentTaxamt = _dsshiftClose.Tables("InvoiceHdr").AsEnumerable().Where(Function(row) row.Field(Of String)("psih_invoice_paymode") = "MGMT").Sum(Function(row) row.Field(Of Decimal)("psih_invoice_ttaxamt"))
                '------------------------------Payout Summary--------------------------------
                Dim _sumPayout As Double = 0
                If _dsshiftClose.Tables("Payouts").Rows.Count > 0 Then
                    For Each _rowPayout As DataRow In _dsshiftClose.Tables("Payouts").Rows
                        _sumPayout += _rowPayout("payd_amount")
                    Next
                End If
                '------------------------------Denomination Summary--------------------------------
                Dim _sumOfPrintNote As Double = 0
                If _dsshiftClose.Tables("PrintNotes").Rows.Count > 0 Then
                    Dim _sumPrintNotes As Double = 0
                    For Each _rowPrintNotes As DataRow In _dsshiftClose.Tables("PrintNotes").Rows
                        _sumPrintNotes = _rowPrintNotes("pns_amount")
                        _sumOfPrintNote += _sumPrintNotes
                    Next
                End If
                '------------------------------Net Sales--------------------------------
                Dim _sumNetSales As Double = 0.0
                _sumNetSales = CashSales - _sumPayout - _sumManagment

                Dim _sumGrossSales As Double = 0.0
                _sumGrossSales = _sales - (_discount + _sumManagment - _sumManagmentTaxamt)

                Dim _sumTotalSales As Double = 0.0
                _sumTotalSales = _sumGrossSales + (_taxsales - _sumManagmentTaxamt) + _serviceTax
                '------------------------------Header Design--------------------------------
                _content.AppendLine(_dot2)
                _content.AppendLine(_TXT1.ToString.PadRight(30))
                _content.AppendLine(_ShopName.ToString.PadRight(20))
                _content.AppendLine(_dot2)
                _content.AppendLine(_EMPTY)
                _content.AppendLine("Shift Open  :" & _shiftOpenDate)
                _content.AppendLine("Shift Close :" & _shiftcloseDate)
                _content.AppendLine("Print Date  :" & Date.Now)
                _dateTime = _shiftcloseDate
                _content.AppendLine(_dot4)
                _content.AppendLine("Shift No :" & shiftno)
                _content.AppendLine("CurDate  :" & _shiftDate)
                _content.AppendLine("Sales                :   " & Format(_sales, "###0.00").ToString.PadLeft(10))
                _content.AppendLine("Discount         (-) :   " & Format(_discount + _sumManagment - _sumManagmentTaxamt, "###0.00").ToString.PadLeft(10))
                _content.AppendLine("                     ----------------------")
                _content.AppendLine("TotalGrossAmt    (=) :   " & Format(_sumGrossSales, "###0.00").ToString.PadLeft(10))
                _content.AppendLine("Tax Sales        (+) :   " & Format(_taxsales - _sumManagmentTaxamt, "###0.00").ToString.PadLeft(10))
                _content.AppendLine("Service Charge   (+) :   " & Format(_serviceTax, "###0.00").ToString.PadLeft(10))
                _content.AppendLine(_dot1)
                _content.AppendLine("Total Sales          :   " & Format(_RoundOff(_sumTotalSales), "###0.00").ToString.PadLeft(10))
                _content.AppendLine(_dot4)
                _content.AppendLine("Total Cash Sales     :   " & Format(CashSales, "###0.00").ToString.PadLeft(10))
                _content.AppendLine("Payout Amount    (-) :   " & Format(_sumPayout, "###0.00").ToString.PadLeft(10))
                _content.AppendLine("Managment/Run    (-) :   " & Format(_sumManagment, "###0.00").ToString.PadLeft(10))
                _content.AppendLine("Net Cash Sales       :   " & Format(_sumNetSales, "###0.00").ToString.PadLeft(10))
                _content.AppendLine(_dot4)
                _content.AppendLine(_EMPTY)
                _content.AppendLine("       ------Details----")
                Dim _totalCount As Decimal = _dsshiftClose.Tables("InvoiceHdr").Rows.Count
                _content.AppendLine("Total BillCount      :   " & Format(_totalCount, "###0.00").ToString.PadLeft(10))
                _content.AppendLine("Average Bill         :   " & Format((_totalsales / _totalCount), "###0.00").ToString.PadLeft(10))
                If _sumOfPrintNote <> 0 Then
                    _content.AppendLine("Tot Cash Sales       :   " & Format(_sumNetSales, "###0.00").ToString.PadLeft(10))
                    _content.AppendLine("Cash On Hand (-)     :   " & Format(_sumOfPrintNote, "###0.00").ToString.PadLeft(10))
                    _content.AppendLine(_dot4)
                    _content.AppendLine("Balance  (+/-)       :   " & Format(_sumOfPrintNote - _sumNetSales, "###0.00").ToString.PadLeft(10))
                Else
                    _content.AppendLine("Cash On Hand         :   " & Format(_sumNetSales, "###0.00").ToString.PadLeft(10))
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------Sales Method 1--------------------------------
                If _checkPrintOption("S011", Mode) = True Then
                    _content.AppendLine("       Sales Method")
                    _content.AppendLine(_dot4)
                    Dim _sumSalemethod As Double = 0.0
                    Dim _RowsSaleslist As String = ""
                    If _dsshiftClose.Tables("SalesMethod").Rows.Count > 0 Then
                        For Each _rowSalesmethod As DataRow In _dsshiftClose.Tables("SalesMethod").Rows
                            _RowsSaleslist = _rowSalesmethod("psih_invoice_description").ToString.PadRight(20) & " :  " & _rowSalesmethod("netamt").ToString.PadLeft(10)
                            _content.AppendLine(_RowsSaleslist.ToString)
                            _sumSalemethod += _rowSalesmethod("netamt")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Sales  :      " & _sumSalemethod.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------Payout Method 2--------------------------------
                If _checkPrintOption("S010", Mode) = True Then
                    'Payout Salary
                    Dim _sumPayouts As Double = 0
                    _content.AppendLine("       Payout Details")
                    _content.AppendLine(_dot4)
                    Dim _RowsPayoutlist As String = ""
                    If _dsshiftClose.Tables("Payouts").Rows.Count > 0 Then
                        Dim dt As New DataTable
                        If _dsshiftClose.Tables("Payouts").Select("pcus_supcust = 'SUPPLIER'").Count > 0 Then
                            dt = _dsshiftClose.Tables("Payouts").Select("pcus_supcust = 'SUPPLIER'").CopyToDataTable
                            For Each _rowPayout As DataRow In dt.Rows
                                _RowsPayoutlist = _rowPayout("payd_name").ToString.PadRight(20) & " :   " & _rowPayout("payd_amount").ToString.PadLeft(10)
                                _sumPayouts = _sumPayouts + _rowPayout("payd_amount")
                                _content.AppendLine(_RowsPayoutlist.ToString)
                            Next
                            _content.AppendLine(_dot4)
                            _content.AppendLine("Total Amount Paid :           " & _sumPayouts.ToString("0.00"))
                            _content.AppendLine(_dot4)
                        End If
                        'Payout Salary
                        _content.AppendLine(_EMPTY)
                        Dim dts As New DataTable
                        If _dsshiftClose.Tables("Payouts").Select("pcus_supcust = 'STAFF'").Count > 0 Then
                            dts = _dsshiftClose.Tables("Payouts").Select("pcus_supcust = 'STAFF'").CopyToDataTable
                            If dts.Rows.Count > 0 Then
                                _sumPayouts = 0
                                _content.AppendLine("       Payout Salary")
                                _content.AppendLine(_dot4)
                                Dim _RowsPayoutSalarylist As String = ""
                                For Each _rowPayout As DataRow In dts.Rows
                                    _RowsPayoutSalarylist = _rowPayout("payd_name").ToString.PadRight(20) & " :  " & _rowPayout("payd_amount").ToString.PadLeft(10)
                                    _sumPayouts = _sumPayouts + _rowPayout("payd_amount")
                                    _content.AppendLine(_RowsPayoutSalarylist.ToString)
                                Next
                                _content.AppendLine(_dot4)
                                _content.AppendLine("Total Amount Paid :         " & _sumPayouts.ToString("0.00"))
                                _content.AppendLine(_dot4)
                            End If
                        End If
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------Main Group Sales 3--------------------------------
                If _checkPrintOption("S012", Mode) = True Then
                    _content.AppendLine("       Main Group")
                    _content.AppendLine(_dot4)
                    Dim _RowsGrouplist As String = ""
                    Dim _sumgrpnet As Double = 0
                    Dim _sumgrptax As Double = 0
                    If _dsshiftClose.Tables("MainGroup").Rows.Count > 0 Then
                        For Each _rowGroup As DataRow In _dsshiftClose.Tables("MainGroup").Rows
                            _RowsGrouplist = _rowGroup("pbm_name").ToString.PadRight(20) & "" & _rowGroup("netamt").ToString.PadLeft(8) & " " & _rowGroup("taxamt").ToString.PadLeft(7)
                            _content.AppendLine(_RowsGrouplist.ToString)
                            _sumgrpnet += _rowGroup("netamt")
                            _sumgrptax += _rowGroup("taxamt")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Amount  :    " & _sumgrpnet.ToString("0.00") & "  " & _sumgrptax.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------Sub Group Sales 4--------------------------------
                If _checkPrintOption("S002", Mode) = True Then
                    _content.AppendLine("       Sub Group")
                    _content.AppendLine(_dot4)
                    Dim _RowsSubGrouplist As String = ""
                    Dim _sumsubgrpnet As Double = 0
                    Dim _sumsubgrptax As Double = 0
                    If _dsshiftClose.Tables("SubGroup").Rows.Count > 0 Then
                        For Each _rowsubGroup As DataRow In _dsshiftClose.Tables("SubGroup").Rows
                            _RowsSubGrouplist = _rowsubGroup("pcam_name").ToString.PadRight(20) & "" & _rowsubGroup("netamt").ToString.PadLeft(8) & "" & _rowsubGroup("taxamt").ToString.PadLeft(8)
                            _content.AppendLine(_RowsSubGrouplist.ToString)
                            _sumsubgrpnet += _rowsubGroup("netamt")
                            _sumsubgrptax += _rowsubGroup("taxamt")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Amount  :    " & _sumsubgrpnet.ToString("0.00") & "    " & _sumsubgrptax.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------ItemWise Sales 5--------------------------------
                If _checkPrintOption("S003", Mode) = True Then
                    _content.AppendLine("       Itemwise Sales")
                    _content.AppendLine(_dot4)
                    Dim _RowsItemlist As String = ""
                    Dim _sumItemnet As Double = 0
                    Dim _sumItemtax As Double = 0
                    Dim _sumItemQty As Double = 0
                    If _dsshiftClose.Tables("InvoiceDtl").Rows.Count > 0 Then
                        For Each _rowItem As DataRow In _dsshiftClose.Tables("InvoiceDtl").Rows
                            Dim itemName As String = ""
                            itemName = _rowItem("psid_invoice_description").ToString
                            _RowsItemlist = itemName.ToString.PadRight(25).Substring(0, 20) & "  " & CInt(CDbl(_rowItem("qty"))).ToString.PadLeft(2) & "  " & _rowItem("netamt").ToString.PadLeft(10) '& "    " & _rowItem("taxamt").ToString.PadLeft(7)

                            _content.AppendLine(_RowsItemlist.ToString)
                            If _rowItem("psid_invoice_description").ToString.Length > 19 Then
                                Dim len = itemName.ToString.Length - 1
                                If len <> 20 Then
                                    len = len - 19
                                    _content.AppendLine(itemName.ToString.PadRight(25).Substring(20, len))
                                End If
                            End If
                            _sumItemnet += _rowItem("netamt")
                            _sumItemtax += _rowItem("taxamt")
                            _sumItemQty += _rowItem("qty")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Sales     :   " & _sumItemQty.ToString("0.00") & " " & _sumItemnet.ToString("0.00")) ' & "  " & _sumItemtax.ToString("0.00"))
                        _content.AppendLine(_dot3)

                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------StaffWise Sales 6--------------------------------
                If _checkPrintOption("S004", Mode) = True Then
                    _content.AppendLine("       Staffwise Sales")
                    _content.AppendLine(_dot4)
                    Dim _RowsStafflist As String = ""
                    Dim _sumStaffnet As Double = 0
                    Dim _sumStafftax As Double = 0
                    Dim _sumStaffQty As Double = 0
                    If _dsshiftClose.Tables("Staffwise").Rows.Count > 0 Then
                        For Each _rowStaff As DataRow In _dsshiftClose.Tables("Staffwise").Rows
                            _RowsStafflist = _rowStaff("pusm_name").ToString.PadRight(25) & "  " & _rowStaff("netamt").ToString.PadLeft(8)
                            _content.AppendLine(_RowsStafflist.ToString)
                            _sumStaffnet += _rowStaff("netamt")
                            _sumStafftax += _rowStaff("taxamt")
                            _sumStaffQty += _rowStaff("qty")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Sales              :" & _sumStaffnet.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------Tablewise Sales 7--------------------------------
                If _checkPrintOption("S013", Mode) = True Then
                    _content.AppendLine("       Tablewise Sales")
                    _content.AppendLine(_dot4)
                    Dim _RowsTablewiselist As String = ""
                    Dim _sumTablewisenet As Double = 0
                    Dim _sumTablewisetax As Double = 0
                    Dim _sumTablewiseQty As Double = 0
                    If _dsshiftClose.Tables("Tablewise").Rows.Count > 0 Then
                        For Each _rowTablewise As DataRow In _dsshiftClose.Tables("Tablewise").Rows
                            _RowsTablewiselist = _rowTablewise("psid_invoice_tableno").ToString.PadRight(25) & "  " & _rowTablewise("netamt").ToString.PadLeft(8)
                            _content.AppendLine(_RowsTablewiselist.ToString)
                            _sumTablewisenet += _rowTablewise("netamt")
                            _sumTablewisetax += _rowTablewise("taxamt")
                            _sumTablewiseQty += _rowTablewise("qty")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Sales               :" & _sumTablewisenet.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------Counter Sales 8--------------------------------
                If _checkPrintOption("S006", Mode) = True Then
                    _content.AppendLine("       Counter Sales")
                    _content.AppendLine(_dot4)
                    Dim _RowsCounterlist As String = ""
                    Dim _sumCounternet As Double = 0
                    Dim _sumCountertax As Double = 0
                    Dim _sumCounterQty As Double = 0
                    If _dsshiftClose.Tables("Counter").Rows.Count > 0 Then
                        For Each _rowCounter As DataRow In _dsshiftClose.Tables("Counter").Rows
                            _RowsCounterlist = _rowCounter("psid_invoice_countername").ToString.PadRight(25) & "  " & _rowCounter("netamt").ToString.PadLeft(8)
                            _content.AppendLine(_RowsCounterlist.ToString)
                            _sumCounternet += _rowCounter("netamt")
                            _sumCountertax += _rowCounter("taxamt")
                            _sumCounterQty += _rowCounter("qty")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Sales              :" & _sumCounternet.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------Deleted Item 9--------------------------------
                If _checkPrintOption("S005", Mode) = True Then
                    _content.AppendLine("       Deleted Details")
                    _content.AppendLine(_dot4)
                    Dim _RowsDellist As String = ""
                    Dim _sumDel As Double = 0
                    If _dsshiftClose.Tables("DeleteItem").Rows.Count > 0 Then
                        For Each _rowDel As DataRow In _dsshiftClose.Tables("DeleteItem").Rows
                            _RowsDellist = _rowDel("psdl_invoice_description").ToString.PadRight(20) & " " & _rowDel("pusm_name").ToString.PadRight(8) & "" & _rowDel("psdl_invoice_netamt").ToString.PadLeft(8)
                            _content.AppendLine(_RowsDellist.ToString)
                            _sumDel += _rowDel("psdl_invoice_netamt")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Amount               :" & _sumDel.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------PendingTable--------------------------------
                If _checkPrintOption("S014", Mode) = True Then
                    _content.AppendLine("       Pending Bill Details")
                    _content.AppendLine(_dot4)
                    Dim _RowsPendinglist As String = ""
                    Dim _sumPending As Double = 0
                    If _dsshiftClose.Tables("PendingTable").Rows.Count > 0 Then
                        For Each _rowPending As DataRow In _dsshiftClose.Tables("PendingTable").Rows
                            _RowsPendinglist = _rowPending("shiftno").ToString.PadRight(5) & "    " & _rowPending("tableno").ToString.PadRight(10) & "    " & _rowPending("grossamt").ToString.PadLeft(8)
                            _content.AppendLine(_RowsPendinglist.ToString)
                            _sumPending += _rowPending("grossamt")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Amount Pending               :" & _sumPending.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)

                '------------------------------CurrentStock--------------------------------
                If _checkPrintOption("S009", Mode) = True Then
                    _content.AppendLine("       CurrentStock Details")
                    _content.AppendLine(_dot4)
                    Dim _RowsCurrentStocklist As String = ""
                    Dim _sumCurrentStock As Double = 0
                    If _dsshiftClose.Tables("CurrentStock").Rows.Count > 0 Then
                        For Each _rowCurr As DataRow In _dsshiftClose.Tables("CurrentStock").Rows
                            _RowsCurrentStocklist = _rowCurr("ppm_name").ToString.PadRight(25) & "  " & _rowCurr("pss_curstock").ToString.PadLeft(10)
                            _content.AppendLine(_RowsCurrentStocklist.ToString)
                            _sumCurrentStock += _rowCurr("pss_curstock")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Current Stock            :" & _sumCurrentStock.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)

                '------------------------------HourLy Sales--------------------------------
                If _checkPrintOption("S007", Mode) = True Then
                    _content.AppendLine("       Hourly Bill Details")
                    _content.AppendLine(_dot4)

                    Dim result As DataTable = _dsshiftClose.Tables("HourlySales").AsEnumerable().GroupBy(Function(r) r.Field(Of String)("HourTime"), Function(key, rows) rows.OrderByDescending(Function(r) r.Field(Of String)("HourTime")).First()).CopyToDataTable()

                    If result.Rows.Count > 0 Then
                        For Each _rowHourTimer In result.Rows
                            Dim _RowsHourlylist As String = ""
                            Dim _sumHoruly As Double = 0
                            Dim linqPrint = From itemlist In _dsshiftClose.Tables("HourlySales").AsEnumerable Where itemlist.Field(Of String)("HourTime") = _rowHourTimer("HourTime") Select itemlist

                            Dim copyTable As DataTable
                            If linqPrint.Count > 0 Then
                                _content.AppendLine(" Hour By   " & _rowHourTimer("HourTime"))
                                _content.AppendLine(_dot1)
                                copyTable = linqPrint.CopyToDataTable
                                For Each _rowHourly In copyTable.Rows
                                    _RowsHourlylist = _rowHourly("bname").ToString.PadRight(20) & "" & _rowHourly("shiftno").ToString.PadRight(5) & " " & _rowHourly("netamt").ToString.PadLeft(8)
                                    _content.AppendLine(_RowsHourlylist.ToString)
                                    _sumHoruly += _rowHourly("netamt")
                                Next
                                _content.AppendLine(_dot4)
                                _content.AppendLine("Total Amount Hourly     :" & _sumHoruly.ToString("0.00"))
                                _content.AppendLine(_dot3)

                            End If
                        Next
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------PrintNotes Sales--------------------------------
                If _checkPrintOption("S008", Mode) = True Then
                    _content.AppendLine("       PrintNotes Details")
                    _content.AppendLine(_dot4)
                    Dim _RowsPrintNotelist As String = ""
                    Dim _sumPrintNote As Double = 0
                    Dim _sumNoteNet As Double = 0
                    If _dsshiftClose.Tables("PrintNotes").Rows.Count > 0 Then
                        For Each _rowPrintNotes As DataRow In _dsshiftClose.Tables("PrintNotes").Rows
                            _sumPrintNote = 0
                            _RowsPrintNotelist = "|" & _rowPrintNotes("pns_note_type").ToString.PadLeft(5) & "| * |     " & _rowPrintNotes("pns_count").ToString.PadLeft(5) & " = " & _rowPrintNotes("pns_amount").ToString.PadLeft(10) & " |"
                            _content.AppendLine(_RowsPrintNotelist.ToString)
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Note         :" & _sumOfPrintNote.ToString("0.00").PadLeft(10))
                        _content.AppendLine("Tot Cash Sales (-) :" & CashSales.ToString("0.00").PadLeft(10))
                        _content.AppendLine("Balance In Hand    :" & (_sumOfPrintNote - _sumNetSales).ToString("0.00").PadLeft(10))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)
                _content.AppendLine(_EMPTY)
                _content.AppendLine(".")
                _content.AppendLine(".")
                _content.AppendLine(_EMPTY)
                _content.AppendLine(".")
                rtb.AppendText(_content.ToString)
                rtb.SaveFile(Path.Combine(M_Details._appPath, "Reports\PrintShiftClose.txt"), RichTextBoxStreamType.PlainText)
                If Mode = "F" Then
                    pdfConverter("PrintShiftClose", Path.Combine(M_Details._appPath, "Reports\"), shiftno)
                End If


                '    Rokok()
                '    Groupwise Sales-
                '    Itemwise Sales-
                '    Staff Wise-
                '    Tablewise-
                '    Pending Table
                '    Deleted Item-
                '    Machinewise Sales-
                '    Hourly Sales
                '    Print(Notes)
                '    Current Stock-
                '    Paid Out Details-
                '    Sales Method-
            End If
            Return True
        Catch ex As Exception
            WriteErroLog(ex.Message)
            Return False
        End Try
    End Function

    Public Function _dayCloseDesign(ByRef dayNo As Integer, ByRef Mode As String) As Boolean
        Try

            rtb.Text = ""
            _dsshiftClose = New DataSet
            'If _saleSetting._shiftclose = True Then
            Dim _SqlShitClosePrint(1) As SqlParameter
            _SqlShitClosePrint(0) = New SqlParameter("@mode", "D")
            _SqlShitClosePrint(1) = New SqlParameter("@shiftno", dayNo)
            _dsshiftClose = _sqlDataAdapter2("sp_shiftclose_print", _SqlShitClosePrint)
            _dsshiftClose.Tables(0).TableName = "ShiftClose"
            _dsshiftClose.Tables(1).TableName = "InvoiceHdr"
            _dsshiftClose.Tables(2).TableName = "InvoiceDtl"
            _dsshiftClose.Tables(3).TableName = "MainGroup"
            _dsshiftClose.Tables(4).TableName = "SubGroup"
            _dsshiftClose.Tables(5).TableName = "SalesMethod"
            _dsshiftClose.Tables(6).TableName = "Payouts"
            _dsshiftClose.Tables(7).TableName = "DeleteItem"
            _dsshiftClose.Tables(8).TableName = "Staffwise"
            _dsshiftClose.Tables(9).TableName = "Tablewise"
            _dsshiftClose.Tables(10).TableName = "Counter"
            _dsshiftClose.Tables(11).TableName = "PendingTable"
            _dsshiftClose.Tables(12).TableName = "HourlySales"
            _dsshiftClose.Tables(13).TableName = "PrintNotes"
            _dsshiftClose.Tables(14).TableName = "CurrentStock"
            If _dsshiftClose.Tables(0).Rows.Count > 0 Then
                Dim _content As New StringBuilder
                Dim _ItemList As New StringBuilder
                Dim _Tablecaption As String = String.Empty
                Dim _EMPTY As String = String.Empty
                Dim _TXT1 As String = "Day Close Report"
                Dim _TXT2 As String = Environment.MachineName
                Dim _dot1 As String = "--------------------------------------"
                Dim _dot2 As String = "**************************************"
                Dim _dot3 As String = "++++++++++++++++++++++++++++++++++++++"
                Dim _dot4 As String = "======================================"
                Dim _ShopName As String = M_Details._shopName & vbNewLine & _printHeaderDesign._address
                Dim _sumManagment As Double = 0.0
                Dim _shiftcloseDate As String = _dsshiftClose.Tables("ShiftClose").Rows(0)("psd_updated").ToString
                Dim _shiftDate As String = _dsshiftClose.Tables("ShiftClose").Rows(0)("psd_curdate").ToString
                Dim _shiftOpenDate As String = _dsshiftClose.Tables("ShiftClose").Rows(0)("psd_created").ToString
                Dim _sales As Double = _dsshiftClose.Tables("ShiftClose").AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("psd_todaysales"))
                Dim _discount As Double = _dsshiftClose.Tables("ShiftClose").AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("psd_totdiscount"))
                Dim _taxsales As Double = _dsshiftClose.Tables("ShiftClose").AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("psd_tottax"))
                Dim _totalsales As Double = _dsshiftClose.Tables("ShiftClose").AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("psd_netamt"))
                Dim _serviceTax As Double = _dsshiftClose.Tables("ShiftClose").AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("psd_servicetax"))
                Dim CashSales As Decimal = _dsshiftClose.Tables("InvoiceHdr").AsEnumerable().Where(Function(row) row.Field(Of String)("psih_invoice_paymode") = "CASH").Sum(Function(row) row.Field(Of Decimal)("psih_invoice_tnetamt"))
                _sumManagment = _dsshiftClose.Tables("InvoiceHdr").AsEnumerable().Where(Function(row) row.Field(Of String)("psih_invoice_paymode") = "MGMT").Sum(Function(row) row.Field(Of Decimal)("psih_invoice_tnetamt"))
                Dim _sumManagmentTaxamt As Double = 0
                _sumManagmentTaxamt = _dsshiftClose.Tables("InvoiceHdr").AsEnumerable().Where(Function(row) row.Field(Of String)("psih_invoice_paymode") = "MGMT").Sum(Function(row) row.Field(Of Decimal)("psih_invoice_ttaxamt"))

                '------------------------------Payout Summary--------------------------------
                Dim _sumPayout As Double = 0
                If _dsshiftClose.Tables("Payouts").Rows.Count > 0 Then
                    For Each _rowPayout As DataRow In _dsshiftClose.Tables("Payouts").Rows
                        _sumPayout += _rowPayout("payd_amount")
                    Next
                End If
                '------------------------------Denomination Summary--------------------------------
                Dim _sumOfPrintNote As Double = 0
                If _dsshiftClose.Tables("PrintNotes").Rows.Count > 0 Then
                    Dim _sumPrintNotes As Double = 0
                    For Each _rowPrintNotes As DataRow In _dsshiftClose.Tables("PrintNotes").Rows
                        _sumPrintNotes = _rowPrintNotes("pns_amount")
                        _sumOfPrintNote += _sumPrintNotes
                    Next
                End If

                '------------------------------Net Sales--------------------------------
                Dim _sumNetSales As Double = 0.0
                _sumNetSales = CashSales - _sumPayout - _sumManagment

                Dim _sumGrossSales As Double = 0.0
                _sumGrossSales = _sales - (_discount + _sumManagment - _sumManagmentTaxamt)

                Dim _sumTotalSales As Double = 0.0
                _sumTotalSales = _sumGrossSales + (_taxsales - _sumManagmentTaxamt) + _serviceTax
                '------------------------------Header Design--------------------------------
                Dim _dateTime As DateTime
                _datetime = _shiftcloseDate
                _content.AppendLine(_dot2)
                _content.AppendLine(_TXT1.ToString.PadRight(30))
                _content.AppendLine(_ShopName.ToString.PadRight(20))
                _content.AppendLine(_dot2)
                _content.AppendLine(_EMPTY)
                _content.AppendLine("Day Open  :" & _shiftOpenDate)
                _content.AppendLine("Day Close :" & _shiftcloseDate)
                _content.AppendLine("Print Date  :" & Date.Now)
                _content.AppendLine(_dot4)
                _content.AppendLine("Day No :" & dayNo)
                _content.AppendLine("CurDate  :" & _shiftDate)
                _content.AppendLine("Sales                :   " & Format(_sales, "###0.00").ToString.PadLeft(10))
                _content.AppendLine("Discount         (-) :   " & Format(_discount + _sumManagment - _sumManagmentTaxamt, "###0.00").ToString.PadLeft(10))
                _content.AppendLine("                     ----------------------")
                _content.AppendLine("TotalGrossAmt    (=) :   " & Format(_sumGrossSales, "###0.00").ToString.PadLeft(10))
                _content.AppendLine("Tax Sales        (+) :   " & Format(_taxsales - _sumManagmentTaxamt, "###0.00").ToString.PadLeft(10))
                _content.AppendLine("Service Charge   (+) :   " & Format(_serviceTax, "###0.00").ToString.PadLeft(10))
                _content.AppendLine(_dot1)
                _content.AppendLine("Total Sales          :   " & Format(_RoundOff(_sumTotalSales), "###0.00").ToString.PadLeft(10))
                _content.AppendLine(_dot4)
                _content.AppendLine("Total Cash Sales     :   " & Format(CashSales, "###0.00").ToString.PadLeft(10))
                _content.AppendLine("Payout Amount    (-) :   " & Format(_sumPayout, "###0.00").ToString.PadLeft(10))
                _content.AppendLine("Managment/Run    (-) :   " & Format(_sumManagment, "###0.00").ToString.PadLeft(10))
                _content.AppendLine("Net Cash Sales       :   " & Format(_sumNetSales, "###0.00").ToString.PadLeft(10))
                _content.AppendLine(_dot4)
                _content.AppendLine(_EMPTY)
                _content.AppendLine("       ------Details----")
                Dim _totalCount As Decimal = _dsshiftClose.Tables("InvoiceHdr").Rows.Count
                _content.AppendLine("Total BillCount      :   " & Format(_totalCount, "###0.00").ToString.PadLeft(10))
                _content.AppendLine("Average Bill         :   " & Format((_totalsales / _totalCount), "###0.00").ToString.PadLeft(10))
                If _sumOfPrintNote <> 0 Then
                    _content.AppendLine("Tot Cash Sales       :   " & Format(_sumNetSales, "###0.00").ToString.PadLeft(10))
                    _content.AppendLine("Cash On Hand (-)     :   " & Format(_sumOfPrintNote, "###0.00").ToString.PadLeft(10))
                    _content.AppendLine(_dot4)
                    _content.AppendLine("Balance  (+/-)       :   " & Format(_sumOfPrintNote - _sumNetSales, "###0.00").ToString.PadLeft(10))
                Else
                    _content.AppendLine("Cash On Hand         :   " & Format(_sumNetSales, "###0.00").ToString.PadLeft(10))
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------Sales Method 1--------------------------------
                If _checkPrintOption("S011", Mode) = True Then
                    _content.AppendLine("       Sales Method")
                    _content.AppendLine(_dot4)
                    Dim _sumSalemethod As Double = 0.0
                    Dim _RowsSaleslist As String = ""
                    If _dsshiftClose.Tables("SalesMethod").Rows.Count > 0 Then
                        For Each _rowSalesmethod As DataRow In _dsshiftClose.Tables("SalesMethod").Rows
                            _RowsSaleslist = _rowSalesmethod("psih_invoice_description").ToString.PadRight(20) & " :  " & _rowSalesmethod("netamt").ToString.PadLeft(10)
                            _content.AppendLine(_RowsSaleslist.ToString)
                            _sumSalemethod += _rowSalesmethod("netamt")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Sales  :      " & _sumSalemethod.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------Payout Method 2--------------------------------
                If _checkPrintOption("S010", Mode) = True Then
                    'Payout Salary
                    Dim _sumPayouts As Double = 0
                    _content.AppendLine("       Payout Details")
                    _content.AppendLine(_dot4)
                    Dim _RowsPayoutlist As String = ""
                    If _dsshiftClose.Tables("Payouts").Rows.Count > 0 Then
                        Dim dt As New DataTable
                        If _dsshiftClose.Tables("Payouts").Select("pcus_supcust = 'SUPPLIER'").Count > 0 Then
                            dt = _dsshiftClose.Tables("Payouts").Select("pcus_supcust = 'SUPPLIER'").CopyToDataTable
                            For Each _rowPayout As DataRow In dt.Rows
                                _RowsPayoutlist = _rowPayout("payd_name").ToString.PadRight(20) & " :   " & _rowPayout("payd_amount").ToString.PadLeft(10)
                                _sumPayouts = _sumPayouts + _rowPayout("payd_amount")
                                _content.AppendLine(_RowsPayoutlist.ToString)
                            Next
                            _content.AppendLine(_dot4)
                            _content.AppendLine("Total Amount Paid :           " & _sumPayouts.ToString("0.00"))
                            _content.AppendLine(_dot4)
                        End If
                        'Payout Salary
                        _content.AppendLine(_EMPTY)
                        Dim dts As New DataTable
                        If _dsshiftClose.Tables("Payouts").Select("pcus_supcust = 'STAFF'").Count > 0 Then
                            dts = _dsshiftClose.Tables("Payouts").Select("pcus_supcust = 'STAFF'").CopyToDataTable
                            If dts.Rows.Count > 0 Then
                                _sumPayouts = 0
                                _content.AppendLine("       Payout Salary")
                                _content.AppendLine(_dot4)
                                Dim _RowsPayoutSalarylist As String = ""
                                For Each _rowPayout As DataRow In dts.Rows
                                    _RowsPayoutSalarylist = _rowPayout("payd_name").ToString.PadRight(20) & " :  " & _rowPayout("payd_amount").ToString.PadLeft(10)
                                    _sumPayouts = _sumPayouts + _rowPayout("payd_amount")
                                    _content.AppendLine(_RowsPayoutSalarylist.ToString)
                                Next
                                _content.AppendLine(_dot4)
                                _content.AppendLine("Total Amount Paid :         " & _sumPayouts.ToString("0.00"))
                                _content.AppendLine(_dot4)
                            End If
                        End If
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------Main Group Sales 3--------------------------------
                If _checkPrintOption("S012", Mode) = True Then
                    _content.AppendLine("       Main Group")
                    _content.AppendLine(_dot4)
                    Dim _RowsGrouplist As String = ""
                    Dim _sumgrpnet As Double = 0
                    Dim _sumgrptax As Double = 0
                    If _dsshiftClose.Tables("MainGroup").Rows.Count > 0 Then
                        For Each _rowGroup As DataRow In _dsshiftClose.Tables("MainGroup").Rows
                            _RowsGrouplist = _rowGroup("pbm_name").ToString.PadRight(20) & "" & _rowGroup("netamt").ToString.PadLeft(8) & " " & _rowGroup("taxamt").ToString.PadLeft(7)
                            _content.AppendLine(_RowsGrouplist.ToString)
                            _sumgrpnet += _rowGroup("netamt")
                            _sumgrptax += _rowGroup("taxamt")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Amount  :    " & _sumgrpnet.ToString("0.00") & "  " & _sumgrptax.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------Sub Group Sales 4--------------------------------
                If _checkPrintOption("S002", Mode) = True Then
                    _content.AppendLine("       Sub Group")
                    _content.AppendLine(_dot4)
                    Dim _RowsSubGrouplist As String = ""
                    Dim _sumsubgrpnet As Double = 0
                    Dim _sumsubgrptax As Double = 0
                    If _dsshiftClose.Tables("SubGroup").Rows.Count > 0 Then
                        For Each _rowsubGroup As DataRow In _dsshiftClose.Tables("SubGroup").Rows
                            _RowsSubGrouplist = _rowsubGroup("pcam_name").ToString.PadRight(20) & "" & _rowsubGroup("netamt").ToString.PadLeft(8) & "" & _rowsubGroup("taxamt").ToString.PadLeft(8)
                            _content.AppendLine(_RowsSubGrouplist.ToString)
                            _sumsubgrpnet += _rowsubGroup("netamt")
                            _sumsubgrptax += _rowsubGroup("taxamt")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Amount  :    " & _sumsubgrpnet.ToString("0.00") & "    " & _sumsubgrptax.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------ItemWise Sales 5--------------------------------
                If _checkPrintOption("S003", Mode) = True Then
                    _content.AppendLine("       Itemwise Sales")
                    _content.AppendLine(_dot4)
                    Dim _RowsItemlist As String = ""
                    Dim _sumItemnet As Double = 0
                    Dim _sumItemtax As Double = 0
                    Dim _sumItemQty As Double = 0
                    If _dsshiftClose.Tables("InvoiceDtl").Rows.Count > 0 Then
                        For Each _rowItem As DataRow In _dsshiftClose.Tables("InvoiceDtl").Rows
                            Dim itemName As String = ""
                            itemName = _rowItem("psid_invoice_description").ToString
                            _RowsItemlist = itemName.ToString.PadRight(25).Substring(0, 20) & "  " & CInt(CDbl(_rowItem("qty"))).ToString.PadLeft(2) & "  " & _rowItem("netamt").ToString.PadLeft(10) '& "    " & _rowItem("taxamt").ToString.PadLeft(7)

                            _content.AppendLine(_RowsItemlist.ToString)
                            If _rowItem("psid_invoice_description").ToString.Length > 19 Then
                                Dim len = itemName.ToString.Length - 1
                                If len <> 20 Then
                                    len = len - 19
                                    _content.AppendLine(itemName.ToString.PadRight(25).Substring(20, len))
                                End If
                            End If
                            _sumItemnet += _rowItem("netamt")
                            _sumItemtax += _rowItem("taxamt")
                            _sumItemQty += _rowItem("qty")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Sales     :   " & _sumItemQty.ToString("0.00") & " " & _sumItemnet.ToString("0.00")) ' & "  " & _sumItemtax.ToString("0.00"))
                        _content.AppendLine(_dot3)

                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------StaffWise Sales 6--------------------------------
                If _checkPrintOption("S004", Mode) = True Then
                    _content.AppendLine("       Staffwise Sales")
                    _content.AppendLine(_dot4)
                    Dim _RowsStafflist As String = ""
                    Dim _sumStaffnet As Double = 0
                    Dim _sumStafftax As Double = 0
                    Dim _sumStaffQty As Double = 0
                    If _dsshiftClose.Tables("Staffwise").Rows.Count > 0 Then
                        For Each _rowStaff As DataRow In _dsshiftClose.Tables("Staffwise").Rows
                            _RowsStafflist = _rowStaff("pusm_name").ToString.PadRight(25) & "  " & _rowStaff("netamt").ToString.PadLeft(8)
                            _content.AppendLine(_RowsStafflist.ToString)
                            _sumStaffnet += _rowStaff("netamt")
                            _sumStafftax += _rowStaff("taxamt")
                            _sumStaffQty += _rowStaff("qty")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Sales              :" & _sumStaffnet.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------Tablewise Sales 7--------------------------------
                If _checkPrintOption("S013", Mode) = True Then
                    _content.AppendLine("       Tablewise Sales")
                    _content.AppendLine(_dot4)
                    Dim _RowsTablewiselist As String = ""
                    Dim _sumTablewisenet As Double = 0
                    Dim _sumTablewisetax As Double = 0
                    Dim _sumTablewiseQty As Double = 0
                    If _dsshiftClose.Tables("Tablewise").Rows.Count > 0 Then
                        For Each _rowTablewise As DataRow In _dsshiftClose.Tables("Tablewise").Rows
                            _RowsTablewiselist = _rowTablewise("psid_invoice_tableno").ToString.PadRight(25) & "  " & _rowTablewise("netamt").ToString.PadLeft(8)
                            _content.AppendLine(_RowsTablewiselist.ToString)
                            _sumTablewisenet += _rowTablewise("netamt")
                            _sumTablewisetax += _rowTablewise("taxamt")
                            _sumTablewiseQty += _rowTablewise("qty")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Sales               :" & _sumTablewisenet.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------Counter Sales 8--------------------------------
                If _checkPrintOption("S006", Mode) = True Then
                    _content.AppendLine("       Counter Sales")
                    _content.AppendLine(_dot4)
                    Dim _RowsCounterlist As String = ""
                    Dim _sumCounternet As Double = 0
                    Dim _sumCountertax As Double = 0
                    Dim _sumCounterQty As Double = 0
                    If _dsshiftClose.Tables("Counter").Rows.Count > 0 Then
                        For Each _rowCounter As DataRow In _dsshiftClose.Tables("Counter").Rows
                            _RowsCounterlist = _rowCounter("psid_invoice_countername").ToString.PadRight(25) & "  " & _rowCounter("netamt").ToString.PadLeft(8)
                            _content.AppendLine(_RowsCounterlist.ToString)
                            _sumCounternet += _rowCounter("netamt")
                            _sumCountertax += _rowCounter("taxamt")
                            _sumCounterQty += _rowCounter("qty")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Sales              :" & _sumCounternet.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------Deleted Item 9--------------------------------
                If _checkPrintOption("S005", Mode) = True Then
                    _content.AppendLine("       Deleted Details")
                    _content.AppendLine(_dot4)
                    Dim _RowsDellist As String = ""
                    Dim _sumDel As Double = 0
                    If _dsshiftClose.Tables("DeleteItem").Rows.Count > 0 Then
                        For Each _rowDel As DataRow In _dsshiftClose.Tables("DeleteItem").Rows
                            _RowsDellist = _rowDel("psdl_invoice_description").ToString.PadRight(20) & " " & _rowDel("pusm_name").ToString.PadRight(8) & "" & _rowDel("psdl_invoice_netamt").ToString.PadLeft(8)
                            _content.AppendLine(_RowsDellist.ToString)
                            _sumDel += _rowDel("psdl_invoice_netamt")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Amount               :" & _sumDel.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------PendingTable--------------------------------
                If _checkPrintOption("S014", Mode) = True Then
                    _content.AppendLine("       Pending Bill Details")
                    _content.AppendLine(_dot4)
                    Dim _RowsPendinglist As String = ""
                    Dim _sumPending As Double = 0
                    If _dsshiftClose.Tables("PendingTable").Rows.Count > 0 Then
                        For Each _rowPending As DataRow In _dsshiftClose.Tables("PendingTable").Rows
                            _RowsPendinglist = _rowPending("shiftno").ToString.PadRight(5) & "    " & _rowPending("tableno").ToString.PadRight(10) & "    " & _rowPending("grossamt").ToString.PadLeft(8)
                            _content.AppendLine(_RowsPendinglist.ToString)
                            _sumPending += _rowPending("grossamt")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Amount Pending               :" & _sumPending.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)

                '------------------------------CurrentStock--------------------------------
                If _checkPrintOption("S009", Mode) = True Then
                    _content.AppendLine("       CurrentStock Details")
                    _content.AppendLine(_dot4)
                    Dim _RowsCurrentStocklist As String = ""
                    Dim _sumCurrentStock As Double = 0
                    If _dsshiftClose.Tables("CurrentStock").Rows.Count > 0 Then
                        For Each _rowCurr As DataRow In _dsshiftClose.Tables("CurrentStock").Rows
                            _RowsCurrentStocklist = _rowCurr("ppm_name").ToString.PadRight(25) & "  " & _rowCurr("pss_curstock").ToString.PadLeft(10)
                            _content.AppendLine(_RowsCurrentStocklist.ToString)
                            _sumCurrentStock += _rowCurr("pss_curstock")
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Current Stock            :" & _sumCurrentStock.ToString("0.00"))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)

                '------------------------------HourLy Sales--------------------------------
                If _checkPrintOption("S007", Mode) = True Then
                    _content.AppendLine("       Hourly Bill Details")
                    _content.AppendLine(_dot4)

                    Dim result As DataTable = _dsshiftClose.Tables("HourlySales").AsEnumerable().GroupBy(Function(r) r.Field(Of String)("HourTime"), Function(key, rows) rows.OrderByDescending(Function(r) r.Field(Of String)("HourTime")).First()).CopyToDataTable()

                    If result.Rows.Count > 0 Then
                        For Each _rowHourTimer In result.Rows
                            Dim _RowsHourlylist As String = ""
                            Dim _sumHoruly As Double = 0
                            Dim linqPrint = From itemlist In _dsshiftClose.Tables("HourlySales").AsEnumerable Where itemlist.Field(Of String)("HourTime") = _rowHourTimer("HourTime") Select itemlist

                            Dim copyTable As DataTable
                            If linqPrint.Count > 0 Then
                                _content.AppendLine(" Hour By   " & _rowHourTimer("HourTime"))
                                _content.AppendLine(_dot1)
                                copyTable = linqPrint.CopyToDataTable
                                For Each _rowHourly In copyTable.Rows
                                    _RowsHourlylist = _rowHourly("bname").ToString.PadRight(20) & "" & _rowHourly("shiftno").ToString.PadRight(5) & " " & _rowHourly("netamt").ToString.PadLeft(8)
                                    _content.AppendLine(_RowsHourlylist.ToString)
                                    _sumHoruly += _rowHourly("netamt")
                                Next
                                _content.AppendLine(_dot4)
                                _content.AppendLine("Total Amount Hourly     :" & _sumHoruly.ToString("0.00"))
                                _content.AppendLine(_dot3)

                            End If
                        Next
                    End If
                End If
                _content.AppendLine(_EMPTY)
                '------------------------------PrintNotes Sales--------------------------------
                If _checkPrintOption("S008", Mode) = True Then
                    _content.AppendLine("       PrintNotes Details")
                    _content.AppendLine(_dot4)
                    Dim _RowsPrintNotelist As String = ""
                    Dim _sumPrintNote As Double = 0
                    Dim _sumNoteNet As Double = 0
                    If _dsshiftClose.Tables("PrintNotes").Rows.Count > 0 Then
                        For Each _rowPrintNotes As DataRow In _dsshiftClose.Tables("PrintNotes").Rows
                            _sumPrintNote = 0
                            _RowsPrintNotelist = "|" & _rowPrintNotes("pns_note_type").ToString.PadLeft(5) & "| * |     " & _rowPrintNotes("pns_count").ToString.PadLeft(5) & " = " & _rowPrintNotes("pns_amount").ToString.PadLeft(10) & " |"
                            _content.AppendLine(_RowsPrintNotelist.ToString)
                        Next
                        _content.AppendLine(_dot4)
                        _content.AppendLine("Total Note         :" & _sumOfPrintNote.ToString("0.00").PadLeft(10))
                        _content.AppendLine("Tot Cash Sales (-) :" & CashSales.ToString("0.00").PadLeft(10))
                        _content.AppendLine("Balance In Hand    :" & (_sumOfPrintNote - _sumNetSales).ToString("0.00").PadLeft(10))
                        _content.AppendLine(_dot4)
                    End If
                End If
                _content.AppendLine(_EMPTY)
                _content.AppendLine(_EMPTY)
                _content.AppendLine(".")
                _content.AppendLine(".")
                _content.AppendLine(_EMPTY)
                _content.AppendLine(".")
                rtb.AppendText(_content.ToString)
                rtb.SaveFile(Path.Combine(M_Details._appPath, "Reports\PrintDayClose.txt"), RichTextBoxStreamType.PlainText)
                If Mode = "F" Then
                    pdfConverter("PrintDayClose", Path.Combine(M_Details._appPath, "Reports\"), dayNo)
                End If


                '    Rokok()
                '    Groupwise Sales-
                '    Itemwise Sales-
                '    Staff Wise-
                '    Tablewise-
                '    Pending Table
                '    Deleted Item-
                '    Machinewise Sales-
                '    Hourly Sales
                '    Print(Notes)
                '    Current Stock-
                '    Paid Out Details-
                '    Sales Method-
            End If
            Return True
        Catch ex As Exception
            WriteErroLog(ex.Message)
            Return False
        End Try
    End Function
    Public Function _dotdesign(ByRef _ds As DataSet, ByRef _printxt As String, ByRef erromsg As String) As Boolean
        Try

            Dim _content As New StringBuilder
            Dim _ItemList As New StringBuilder
            Dim _Tablecaption As String = String.Empty
            Dim _EMPTY As String = String.Empty
            Dim _dotline1 As String = "------------------------------------------------"
            Dim _title As String = ""
            Dim _cATION As String
            Dim _HEAD As String

            If _printxt = "PR" Then
                _cATION = _ds.Tables(0).Rows(0).Item("MYD_MSH_PMODE").ToString
                Select Case _cATION
                    Case _saleSetting._modeDefalueSales
                        _HEAD = "CASH PAYMENT"
                    Case "CD"
                        _HEAD = "CARD PAYMENT"
                End Select
            Else
                _cATION = "ORDER PRINT NOT VALID FOR TAX"
            End If
            'If _Readsetting(_err) = True Then
            '    '_title = _DotmatrixTemp.shopadress
            'End If
            Dim _prefix As String = _ds.Tables(0).Rows(0).Item("myd_msh_prefix").ToString
            Dim _bilno As String = _ds.Tables(0).Rows(0).Item("myd_msh_number").ToString
            Dim _date As String = DateTime.Now().ToString
            Dim _casier As String = ""
            Dim _Tableno As String = _ds.Tables(0).Rows(0).Item("myd_msh_table").ToString
            Dim _custome As String = _ds.Tables(0).Rows(0).Item("myd_cust_name").ToString & "-" & _ds.Tables(0).Rows(0).Item("myd_cust_mem").ToString
            Dim _counter As String = _ds.Tables(0).Rows(0).Item("myd_msh_countername").ToString
            Dim _TOTAMT As String = _ds.Tables(1).Rows(0).Item("SUBTOT").ToString
            Dim _GROSS As String = _ds.Tables(1).Rows(0).Item("GROSS").ToString
            Dim _TOTDISCAMT As String = _ds.Tables(1).Rows(0).Item("DIST").ToString
            Dim _TOTTAXAMT As String = _ds.Tables(1).Rows(0).Item("TAX").ToString
            Dim _NETAMT As String = _ds.Tables(1).Rows(0).Item("NET").ToString
            Dim _totqt As String = _ds.Tables(0).Rows(0).Item("myd_msh_tqty").ToString
            Dim _hsno As String = "S.No"
            Dim _hitemname As String = "ItemName"
            Dim _hqty As String = "Qty"
            Dim _hamt As String = "Amt"
            Dim _dsno As String = String.Empty
            Dim _ditemname As String = String.Empty
            Dim _dqty As String = String.Empty
            Dim _damt As String = String.Empty
            Dim _GIVEAMT As String = _ds.Tables(0).Rows(0).Item("MYD_MSH_GIVENAMT").ToString
            Dim _BAL As String = _ds.Tables(0).Rows(0).Item("MYD_MSH_BALAMT").ToString
            Dim _ROUNDOFF As String = _ds.Tables(0).Rows(0).Item("MYD_MSH_ROUNDOFF").ToString

            _Tablecaption = (_hsno & "  " & _hitemname & "                " & _hqty & "           " & _hamt)
            '---------------------------------------------------Bill Design------------------------------------------------

            _content.AppendLine(_title)
            _content.AppendLine("    " & _cATION)
            _content.AppendLine(_dotline1)
            _content.AppendLine("BILL NO:" & _prefix.ToString().Substring(0, 3) & _bilno)
            _content.AppendLine("DATE:" & _date)
            _content.AppendLine("Counter: " & _counter.ToString().Substring(0, 10) & "             " & "Table No: " & _Tableno)
            _content.AppendLine("Customer Name:" & _custome.ToUpper)
            _content.AppendLine(_dotline1)
            _content.AppendLine(_Tablecaption)
            _content.AppendLine(_dotline1)
            Dim _Rowlis As String = String.Empty
            For Each row As DataRow In _ds.Tables(0).Rows
                _dsno = row("myd_sale_sno").ToString

                If row("myd_pro_name").ToString.Length > 15 Then
                    _ditemname = row("myd_pro_name").ToString.Substring(0, 20)
                Else
                    _ditemname = row("myd_pro_name").ToString
                End If
                _dqty = row("myd_sale_proqty").ToString()
                _damt = row("myd_sale_amt").ToString
                _Rowlis = (_dsno.ToString.PadRight(3) & _ditemname.ToString.PadRight(20) & "  " & _dqty.ToString.PadLeft(7) & "        " & _damt.ToString.PadLeft(7))
                _ItemList.AppendLine(_Rowlis)
            Next
            _content.AppendLine(_ItemList.ToString.Trim)
            _content.AppendLine(_dotline1)
            _content.AppendLine("Tot Qty:" & _totqt & "                  " & ("Sub Tot:" & "   " & _GROSS.ToString.PadLeft(8)))
            If _TOTDISCAMT <> "0.00" Then
                _content.AppendLine("                            " & ("Tot Disc:" & "  " & _TOTDISCAMT.ToString.PadLeft(8)))
                _content.AppendLine(_dotline1)
                _content.AppendLine("                            " & ("Gross Tot:" & "  " & _TOTAMT.ToString.PadLeft(8)))
            End If
            _content.AppendLine("                            " & ("GST 6% :" & "   " & _TOTTAXAMT.ToString.PadLeft(8)))
            If _ROUNDOFF <> "0.00" Then
                _content.AppendLine("                           " & ("ROUND OFF:" & "  " & _ROUNDOFF.ToString.PadLeft(8)))
            End If
            _content.AppendLine("                             " & ("Tot Amt:" & "  " & _NETAMT.ToString.PadLeft(8)))
            _content.AppendLine(_dotline1)
            _content.AppendLine("AMOUNT RECEIVED :" & _GIVEAMT)
            _content.AppendLine("BALANCE         :" & _BAL)
            _content.AppendLine("            THANK YOU COME AGAIN")
            _content.AppendLine(_EMPTY)
            _content.AppendLine(_EMPTY)
            _content.AppendLine(_EMPTY)
            _content.AppendLine(_EMPTY & ".")
            _printxt = _content.ToString
            rtb.AppendText(_content.ToString)
            rtb.SaveFile(Path.Combine(M_Details._appPath, "Reports\DOTPRINT.txt"), RichTextBoxStreamType.PlainText)

            Return True
            'MessageBox.Show(_AppPath)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function _dotDAILYdesign(ByRef _ds As DataSet, ByRef _printxt As String, ByRef erromsg As String) As Boolean
        Try
            Dim _content As New StringBuilder
            Dim _ItemList As New StringBuilder
            Dim _Tablecaption As String = String.Empty
            Dim _EMPTY As String = String.Empty
            Dim _TXT1 As String = "DAILY SALES ON: " & Date.Now
            Dim _TXT2 As String = Environment.MachineName
            Dim _dotline1 As String = "------------------------------------------------"
            Dim _SHOPNAME As String
            Dim _ATOTNETMAT As String
            Dim _CTOTNETMAT As String
            Dim _CATOTNETAMT As String
            Dim _CDTOTNETAMT As String
            If _ds.Tables(0).Rows.Count = 0 Then
                _SHOPNAME = "-----"
            Else
                _SHOPNAME = _ds.Tables(0).Rows(0).Item("COMPANY").ToString
            End If
            If _ds.Tables(1).Rows.Count = 0 Then
                _ATOTNETMAT = "0.00"
            Else
                _ATOTNETMAT = _ds.Tables(1).Rows(0).Item("ANETSALES").ToString
            End If
            If _ds.Tables(2).Rows.Count = 0 Then
                _CTOTNETMAT = "0.00"
            Else
                _CTOTNETMAT = _ds.Tables(2).Rows(0).Item("CANNETSALES").ToString
            End If
            If _ds.Tables(3).Rows.Count = 0 Then
                _CATOTNETAMT = "0.00"
            Else
                _CATOTNETAMT = _ds.Tables(3).Rows(0).Item("CANETSALES").ToString()
            End If
            If _ds.Tables(4).Rows.Count = 0 Then
                _CDTOTNETAMT = "0.00"
            Else
                _CDTOTNETAMT = _ds.Tables(4).Rows(0).Item("CDNETSALES").ToString
            End If
            Dim _TOTSALES As String = _ds.Tables(0).Rows(0).Item("TOT_SALES").ToString
            Dim _TOTDISCAMT As String = _ds.Tables(0).Rows(0).Item("TOT_DISCAMT").ToString
            Dim _TOTGROSSAMT As String = _ds.Tables(0).Rows(0).Item("TOT_GROSSAMT").ToString
            Dim _TOTAXAMT As String = _ds.Tables(0).Rows(0).Item("TOT_TAXAMT").ToString
            Dim _TOTNETMAT As String = _ds.Tables(0).Rows(0).Item("NETSALES").ToString
            _content.AppendLine("COMPANY :" & _SHOPNAME)
            _content.AppendLine(_TXT1)
            _content.AppendLine(_TXT2)
            _content.AppendLine(_dotline1)
            _content.AppendLine("TOTAL SALES :                          " & _TOTSALES.ToString)
            _content.AppendLine("TOTAL DISCAMT :                           " & _TOTDISCAMT.ToString)
            ' _content.AppendLine(Environment.NewLine)
            _content.AppendLine(_dotline1)
            _content.AppendLine("TOTAL GROSSAMT :                       " & _TOTGROSSAMT.ToString)
            _content.AppendLine("TOTAL TAXAMT  :                         " & _TOTAXAMT.ToString)
            '_content.AppendLine(Environment.NewLine)
            _content.AppendLine(_dotline1)
            _content.AppendLine("NET AMOUNT :                           " & _TOTNETMAT.ToString)
            _content.AppendLine(_dotline1)
            _content.AppendLine("SALES SUMMARY")
            _content.AppendLine(_dotline1)
            _content.AppendLine("TODAY SALES :                          " & _ATOTNETMAT.ToString)
            _content.AppendLine("CANCEL AMOUNT :                        " & _CTOTNETMAT.ToString)
            _content.AppendLine("CASH SALES :                           " & _CATOTNETAMT.ToString)
            _content.AppendLine("CARD SALES :                           " & _CDTOTNETAMT.ToString)
            _content.AppendLine(_dotline1)
            _content.AppendLine("PRINTING ON " & Date.Today)
            _content.AppendLine(_EMPTY)
            _content.AppendLine(_EMPTY)
            _content.AppendLine(_EMPTY)
            _content.AppendLine(_EMPTY & ".")
            rtb.AppendText(_content.ToString)
            rtb.SaveFile(Path.Combine(M_Details._appPath, "Reports\DOTDAILYPRINT.txt"), RichTextBoxStreamType.PlainText)
            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function
    Public Function _dotcumdesign(ByRef erromsg As String) As Boolean
        Try

            Dim _CONTENT As New StringBuilder
            Dim _ITELIST As New StringBuilder
            Dim _tAB As New DataTable
            Dim _DATE As String = ""
            Dim _TITLE As String = ""
            Dim _CATE As String = ""
            Dim _PNAME As String = ""
            Dim _QTY As String = ""
            Dim _AMT As String = ""
            Dim _TXT1 As String = "DAILY ITEMWISE SALES ON: " & Date.Now
            Dim _TXT2 As String = Environment.MachineName
            Dim _DOT As String = "------------------------------------------------"
            Dim _EMPTY As String = String.Empty
            Dim _Rowlis As String = String.Empty
            Dim _AMTTOT, _QTYTOT As String
            Dim _HEADER As String = ""
            Dim _NETAMT As String = ""
            'If _Readsetting(_err) = False Then
            '    _HEADER = _DotmatrixTemp._DAILYSALE
            'End If
            ' _CONTENT.AppendLine(_HEADER)
            ' _CONTENT.AppendLine(_EMPTY)
            _CONTENT.AppendLine(_DOT)
            _CONTENT.AppendLine(_TXT1)
            _CONTENT.AppendLine(_TXT2)
            _CONTENT.AppendLine(_DOT)
            _DATEFROM_1 = Date.Today.ToString("yyyy-MM-dd")
            _DATETO_1 = Date.Today.ToString("yyyy-MM-dd")
            Dim _sQLPAR(3) As SqlParameter
            _sQLPAR(0) = New SqlParameter("@MODE", "W")
            _sQLPAR(1) = New SqlParameter("@DATE1", _DATEFROM_1)
            _sQLPAR(2) = New SqlParameter("@DATE2", _DATETO_1)
            _sQLPAR(3) = New SqlParameter("@CATE", "0")
            _DSS = _sqlDataAdapter("SP_CA_SALESREPORT", _sQLPAR, erromsg)

            For Each _ROWDA As DataRow In _DSS.Tables(0).Rows
                _CATE = _ROWDA("BRANDNAME").ToString
                _CONTENT.AppendLine("SECTION :" & _CATE)
                _CONTENT.AppendLine(_EMPTY)
                _PNAME = String.Empty
                _QTY = String.Empty
                _AMT = String.Empty
                Dim _sQLPAR1(3) As SqlParameter
                _sQLPAR1(0) = New SqlParameter("@MODE", "WW")
                _sQLPAR1(1) = New SqlParameter("@DATE1", _DATEFROM_1)
                _sQLPAR1(2) = New SqlParameter("@DATE2", _DATETO_1)
                _sQLPAR1(3) = New SqlParameter("@CATE", _CATE)
                _DSS1 = _sqlDataAdapter("SP_CA_SALESREPORT", _sQLPAR1, erromsg)
                _DSS1.Tables(0).TableName = "PRO"
                _DSS1.Tables(1).TableName = "TOT"
                _DSS1.Tables(2).TableName = "GROS"
                For Each _ROW As DataRow In _DSS1.Tables(0).Rows
                    _PNAME = _ROW("PRODUCTNAME").ToString.Substring(0, 20).ToUpper
                    _QTY = _ROW("QTY").ToString
                    _AMT = _ROW("NETAMT").ToString
                    _Rowlis = (_PNAME.ToString.PadRight(20) & "      " & _QTY.ToString.PadLeft(7) & "      " & _AMT.ToString.PadLeft(7))
                    _CONTENT.AppendLine(_Rowlis.ToString)
                Next

                '_CONTENT.AppendLine(_ITELIST.ToString().Trim)
                _AMTTOT = _DSS1.Tables(1).Rows(0).Item("NETAMT").ToString
                _QTYTOT = _DSS1.Tables(1).Rows(0).Item("QTY").ToString
                _CONTENT.AppendLine(_EMPTY)
                _CONTENT.AppendLine(_DOT)
                _CONTENT.AppendLine("NET TOTAL :                     " & _QTYTOT & "       " & _AMTTOT)
                _CONTENT.AppendLine(_DOT)
                '_CONTENT.AppendLine(_EMPTY)

            Next
            _NETAMT = _DSS1.Tables(2).Rows(0).Item("NETSALES").ToString
            ' _CONTENT.AppendLine(_DOT)
            _CONTENT.AppendLine("NET SALE :                              " & _NETAMT)
            _CONTENT.AppendLine(_DOT)
            _CONTENT.AppendLine(_EMPTY)
            _CONTENT.AppendLine(_EMPTY)
            _CONTENT.AppendLine(_EMPTY)
            _CONTENT.AppendLine(_EMPTY & ".")
            rtb.AppendText(_CONTENT.ToString)
            rtb.SaveFile(Path.Combine(M_Details._appPath, "Reports\PRTWISE.txt"), RichTextBoxStreamType.PlainText)



            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function
    Public Sub pdfConverter(ByRef filename As String, ByRef path As String, ByRef dayOrShiftno As Integer)
        Try
            Dim line As String
            Dim readFile As System.IO.TextReader = New StreamReader(path & filename & ".txt")
            Dim yPoint As Integer = 0

            Dim pdf As PdfDocument = New PdfDocument
            pdf.Info.Title = filename
            Dim pdfPage As PdfPage = pdf.AddPage
            pdfPage.Height = "9999"
            pdfPage.Width = "400"
            Dim graph As XGraphics = XGraphics.FromPdfPage(pdfPage)
            Dim font As XFont = New XFont("Consolas", 11, XFontStyle.Regular)


            While True
                line = readFile.ReadLine()

                If line Is Nothing Then
                    Exit While
                Else
                    graph.DrawString(line, font, XBrushes.Black, New XRect(30, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormat.TopLeft)
                    yPoint = yPoint + 10
                End If
            End While


            Dim pdfFilename As String = path & dayOrShiftno & "_" & Now.ToString("ddMMyyyy") & filename & ".pdf"
            pdf.Save(pdfFilename)
            readFile.Close()
            readFile = Nothing
            ' Process.Start(path & filename & ".txt")
            ',Process.Start(pdfFilename)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

End Class
