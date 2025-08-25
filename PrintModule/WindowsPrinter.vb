Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.DataTableExtensions
Imports DevExpress.XtraEditors
Imports DevExpress.XtraBars

Public Class WindowsPrinter
    Dim rtb As New RichTextBox
    Dim rtbORDER As New RichTextBox
    Dim RTBSUM As New RichTextBox
    Private _err As String
    Private _DATEFROM_1, _DATETO_1 As String
    Private _DSS, _DSS1, _dst As New DataSet
    Private components As System.ComponentModel.IContainer
    Public Function _SalesPrint(ByRef _ds As DataSet) As Boolean
        Try
            rtb.Text = ""
            _ds.WriteXmlSchema(M_Details._appPath + "\Report.xml")
            Dim _tav As Integer = 0
            Dim _taxin As String = String.Empty
            Dim _content As New StringBuilder
            Dim _ItemList As New StringBuilder
            Dim _taxlist As New StringBuilder
            Dim _Tablecaption As String = String.Empty
            Dim _EMPTY As String = String.Empty
            Dim _dotline1 As String = "-------------------------------------"
            Dim _title As String = ""
            Dim _Shopname As String = 0 ' _printHeaderDesign._shopname
            Dim _cATION As String
            Dim _HEAD As String = String.Empty
            Dim _pax As String = _ds.Tables(0).Rows(0).Item("myd_msh_pax").ToString
            Dim _dt As New DataTable
            _cATION = _ds.Tables(0).Rows(0).Item("MYD_MSH_PMODE").ToString
            Select Case _cATION
                Case _saleSetting._modeDefalueSales
                    _HEAD = "RECEIPT"
                Case "CD"
                    _HEAD = "CARD PAYMENT"
                Case "CR"
                    _HEAD = "CREDIT BILL"
            End Select
            ' _title = _printHeaderDesign._address
            ' Dim sum As Integer = dt.AsEnumerable().Sum(Function(row) row.Field(Of Integer)("Salary"))
            Dim _prefix As String = _ds.Tables(0).Rows(0).Item("myd_msh_prefix").ToString
            Dim _bilno As String = _ds.Tables(0).Rows(0).Item("myd_msh_number").ToString
            Dim _date As String = DateTime.Now().ToString
            Dim _casier As String = _ds.Tables(0).Rows(0).Item("myd_user_StaffName").ToString
            Dim _Tableno As String = _ds.Tables(0).Rows(0).Item("myd_msh_table").ToString
            Dim _custome As String = _ds.Tables(0).Rows(0).Item("myd_cust_name").ToString & "-" & _ds.Tables(0).Rows(0).Item("myd_cust_mem").ToString
            Dim _counter As String = _ds.Tables(0).Rows(0).Item("myd_sale_countername").ToString
            Dim _TOTAMT As String = _ds.Tables(0).Rows(0).Item("myd_msh_tbnetamt").ToString
            Dim _GROSS As String = _ds.Tables(0).Rows(0).Item("myd_msh_tamount").ToString
            Dim _TOTDISCAMT As String = _ds.Tables(0).Rows(0).Item("myd_msh_titemdisamt").ToString
            Dim _TOTTAXAMT As String = _ds.Tables(0).Rows(0).Item("myd_msh_ttaxamt").ToString
            Dim _NETAMT As String = _ds.Tables(0).Rows(0).Item("myd_msh_tnetamt").ToString
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
            _Tablecaption = (_hitemname & "               " & _hqty & "      " & _hamt)
            '---------------------------------------------------Bill Design------------------------------------------------
            _content.AppendLine(_Shopname)
            _content.AppendLine(_title)
            _content.AppendLine("        " & _HEAD)
            _content.AppendLine(_dotline1)
            _content.AppendLine("BILL NO:" & _prefix.ToString().Substring(0, 3) & _bilno)
            _content.AppendLine("DATE:" & _date & " Pax:" & _pax)
            ' _content.AppendLine("Cashier:" & _casier)
            '_content.AppendLine("Counter: " & _counter.ToString() & " " & "Table No: " & _Tableno)
            '_content.AppendLine("Customer Name:" & _custome.ToUpper)
            _content.AppendLine(_dotline1)
            _content.AppendLine(_Tablecaption)
            _content.AppendLine(_dotline1)
            Dim _Rowlis As String = String.Empty
            Dim _talis As String = String.Empty
            _dt = _ds.Tables(0)
            ' Dim names = From row In _dt.AsEnumerable() Select row.Field(Of String)("myd_sale_taxvalue") Distinct
            ' Dim a = (From row In _dt.AsEnumerable() Select New With {.myd_sale_taxvalue = row.Field(Of Integer)("myd_sale_taxvalue")}).ToList()

            Dim distinctDT As DataTable = _dt.DefaultView.ToTable(True, "myd_sale_taxvalue", "myd_pro_taxin")
            For Each _Row As DataRow In distinctDT.Rows
                _taxin = _Row("myd_pro_taxin")
                '    _functionModule.GSTCODE = _taxin
                _tav = Convert.ToInt32(_Row("myd_sale_taxvalue"))
                If _taxin = "0" Then
                    Dim Taxamt As Decimal = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("myd_sale_taxvalue") = _tav).Sum(Function(row) row.Field(Of Decimal)("myd_sale_taxamt"))
                    Dim Amount As Decimal = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("myd_sale_taxvalue") = _tav).Sum(Function(row) row.Field(Of Decimal)("myd_sale_bnetamt")) 'no gst 
                    _talis = (_tav & "%          " & Amount & "     " & Taxamt)
                    _taxlist.AppendLine(_talis)
                Else
                    Dim Taxamt As Decimal = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("myd_sale_taxvalue") = _tav).Sum(Function(row) row.Field(Of Decimal)("myd_sale_taxamt"))
                    Dim Amount As Decimal = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("myd_sale_taxvalue") = _tav).Sum(Function(row) row.Field(Of Decimal)("myd_sale_gross")) 'with gst
                    _talis = (_tav & "%          " & Amount & "     " & Taxamt)
                    _taxlist.AppendLine(_talis)
                End If

            Next
            For Each row As DataRow In _ds.Tables(0).Rows
                '_dsno = row("myd_sale_sno").ToString

                If row("myd_pro_name").ToString.Length >= 19 Then
                    _ditemname = row("myd_pro_name").ToString.Substring(0, 19)
                Else
                    _ditemname = row("myd_pro_name").ToString
                End If
                _dqty = row("myd_sale_proqty").ToString()
                _damt = row("myd_sale_amt").ToString
                _Rowlis = (_ditemname.ToString.PadRight(20).ToUpper() & " " & _dqty.ToString.PadLeft(5) & "  " & _damt.ToString.PadLeft(7))
                _ItemList.AppendLine(_Rowlis)
            Next
            _content.AppendLine(_ItemList.ToString.Trim)
            _content.AppendLine(_dotline1)
            _content.AppendLine("Tot Qty:" & _totqt & "         " & ("Sub Tot:" & "" & _GROSS.ToString.PadLeft(8)))
            '_content.AppendLine("Tot Qty:" & _totqt & "         " & ("Sub Tot:" & " " & _TOTAMT.ToString.PadLeft(8)))
            If _TOTDISCAMT <> "0.00" Then
                ' _content.AppendLine("Tot Qty:" & _totqt & "         " & ("Sub Tot:" & " " & _GROSS.ToString.PadLeft(8)))
                _content.AppendLine("                 " & ("Tot Disc:" & " " & _TOTDISCAMT.ToString.PadLeft(7)))
                _content.AppendLine(_dotline1)
                '_content.AppendLine("               " & ("Gross Tot:" & "  " & _TOTAMT.ToString.PadLeft(8)))
            End If
            _content.AppendLine("                  " & ("SST 6% :" & " " & _TOTTAXAMT.ToString.PadLeft(7)))
            If _ROUNDOFF <> "0.00" Then
                _content.AppendLine("               " & ("ROUND OFF:" & " " & _ROUNDOFF.ToString.PadLeft(8)))
            End If
            _content.AppendLine("                " & ("Tot Amt:" & "  " & _NETAMT.ToString.PadLeft(8)))
            _content.AppendLine(_dotline1)
            _content.AppendLine("AMOUNT RECEIVED :" & _GIVEAMT)
            _content.AppendLine("BALANCE         :" & _BAL)
            _content.AppendLine("            TERIMA KASIH")
            _content.AppendLine("          SILA DATANG LAGI")
            _content.AppendLine(_EMPTY)
            _content.AppendLine(_EMPTY & "---SST Summary------")
            _content.AppendLine("SSType" & "     " & "Amount" & "   " & "SSTAmt")
            _content.AppendLine(_taxlist.ToString)
            _content.AppendLine(".")
            _content.AppendLine(_EMPTY)
            ' _content.AppendLine(_EMPTY)
            ' _content.AppendLine(".")
            _content.AppendLine(_EMPTY)
            '_content.AppendLine(_EMPTY & Environment.NewLine)
            '_content.AppendLine(_EMPTY & Environment.NewLine)
            ' _printxt = _content.ToString
            rtb.AppendText(_content.ToString)
            rtb.SaveFile(Path.Combine(M_Details._appPath, "Reports\WINPRINT.txt"), RichTextBoxStreamType.PlainText)

            Return True
            'MessageBox.Show(_AppPath)
        Catch ex As Exception
            WriteErroLog(ex.ToString())
            Return False
        End Try
    End Function
    Public Function _SaleGuestPrint(ByRef _ds As DataSet) As Boolean
        Try
            rtb.Text = ""
            Dim _tav As Integer = 0
            Dim _taxin As String = String.Empty
            Dim _content As New StringBuilder
            Dim _ItemList As New StringBuilder
            Dim _taxlist As New StringBuilder
            Dim _Tablecaption As String = String.Empty
            Dim _EMPTY As String = String.Empty
            Dim _dotline1 As String = "-------------------------------------"
            Dim _title As String = ""
            Dim _HEAD As String = String.Empty
            Dim _dt As New DataTable
            _HEAD = "GUEST PRINT"
            '  Dim _Shopname As String = _printHeaderDesign._shopname
            ' _title = _printHeaderDesign._address
            '  Dim sum As Integer = dt.AsEnumerable().Sum(Function(row) row.Field(Of Integer)("Salary"))
            Dim _prefix As String = _ds.Tables(0).Rows(0).Item("myd_msh_prefix").ToString
            Dim _bilno As String = _ds.Tables(0).Rows(0).Item("myd_msh_number").ToString
            Dim _date As String = DateTime.Now().ToString
            Dim _casier As String = _ds.Tables(0).Rows(0).Item("myd_user_StaffName").ToString
            Dim _Tableno As String = _ds.Tables(0).Rows(0).Item("myd_msh_table").ToString
            Dim _custome As String = _ds.Tables(0).Rows(0).Item("myd_cust_name").ToString & "-" & _ds.Tables(0).Rows(0).Item("myd_cust_mem").ToString
            Dim _counter As String = _ds.Tables(0).Rows(0).Item("myd_sale_countername").ToString
            Dim _TOTAMT As String = _ds.Tables(0).Rows(0).Item("myd_msh_tbnetamt").ToString
            Dim _GROSS As String = _ds.Tables(0).Rows(0).Item("myd_msh_tamount").ToString
            Dim _TOTDISCAMT As String = _ds.Tables(0).Rows(0).Item("myd_msh_titemdisamt").ToString
            Dim _TOTTAXAMT As String = _ds.Tables(0).Rows(0).Item("myd_msh_ttaxamt").ToString
            Dim _NETAMT As String = _ds.Tables(0).Rows(0).Item("myd_msh_tnetamt").ToString
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

            _Tablecaption = (_hitemname & "               " & _hqty & "      " & _hamt)
            '---------------------------------------------------Bill Design------------------------------------------------
            '  _content.AppendLine(_Shopname)
            _content.AppendLine(_title)
            _content.AppendLine("    " & _HEAD)
            _content.AppendLine(_dotline1)
            ' _content.AppendLine("BILL NO:" & _prefix.ToString().Substring(0, 3) & _bilno)
            _content.AppendLine("DATE:" & _date)
            '_content.AppendLine("Cashier:" & _casier)
            ' _content.AppendLine("Counter: " & _counter.ToString() & " " & "Table No: " & _Tableno)
            ' _content.AppendLine("Customer Name:" & _custome.ToUpper)
            _content.AppendLine(_dotline1)
            _content.AppendLine(_Tablecaption)
            _content.AppendLine(_dotline1)
            Dim _Rowlis As String = String.Empty
            Dim _talis As String = String.Empty
            _dt = _ds.Tables(0)
            ' Dim names = From row In _dt.AsEnumerable() Select row.Field(Of String)("myd_sale_taxvalue") Distinct
            ' Dim a = (From row In _dt.AsEnumerable() Select New With {.myd_sale_taxvalue = row.Field(Of Integer)("myd_sale_taxvalue")}).ToList()
            Dim distinctDT As DataTable = _dt.DefaultView.ToTable(True, "myd_sale_taxvalue", "myd_pro_taxin")
            For Each _Row As DataRow In distinctDT.Rows
                _taxin = _Row("myd_pro_taxin")
                _tav = Convert.ToInt32(_Row("myd_sale_taxvalue"))
                If _taxin = "0" Then
                    Dim Taxamt As Decimal = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("myd_sale_taxvalue") = _tav).Sum(Function(row) row.Field(Of Decimal)("myd_sale_taxamt"))
                    Dim Amount As Decimal = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("myd_sale_taxvalue") = _tav).Sum(Function(row) row.Field(Of Decimal)("myd_sale_bnetamt"))
                    _talis = (_tav & "%          " & Amount & "     " & Taxamt)
                    _taxlist.AppendLine(_talis)
                Else
                    Dim Taxamt As Decimal = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("myd_sale_taxvalue") = _tav).Sum(Function(row) row.Field(Of Decimal)("myd_sale_taxamt"))
                    Dim Amount As Decimal = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("myd_sale_taxvalue") = _tav).Sum(Function(row) row.Field(Of Decimal)("myd_sale_gross"))
                    _talis = (_tav & "%          " & Amount & "     " & Taxamt)
                    _taxlist.AppendLine(_talis)
                End If

            Next
            For Each row As DataRow In _ds.Tables(0).Rows
                '_dsno = row("myd_sale_sno").ToString

                If row("myd_pro_name").ToString.Length >= 19 Then
                    _ditemname = row("myd_pro_name").ToString.Substring(0, 19)
                Else
                    _ditemname = row("myd_pro_name").ToString
                End If
                _dqty = row("myd_sale_proqty").ToString()
                _damt = row("myd_sale_amt").ToString
                _Rowlis = (_ditemname.ToString.PadRight(20).ToUpper() & " " & _dqty.ToString.PadLeft(5) & "  " & _damt.ToString.PadLeft(7))
                _ItemList.AppendLine(_Rowlis)
            Next
            _content.AppendLine(_ItemList.ToString.Trim)
            _content.AppendLine(_dotline1)
            _content.AppendLine("Tot Qty:" & _totqt & "         " & ("Sub Tot:" & " " & _GROSS.ToString.PadLeft(8)))
            '_content.AppendLine("Tot Qty:" & _totqt & "         " & ("Sub Tot:" & " " & _TOTAMT.ToString.PadLeft(8)))
            If _TOTDISCAMT <> "0.00" Then
                '    _content.AppendLine("Tot Qty:" & _totqt & "         " & ("Sub Tot:" & " " & _GROSS.ToString.PadLeft(8)))
                _content.AppendLine("                 " & ("Tot Disc:" & "  " & _TOTDISCAMT.ToString.PadLeft(7)))
                _content.AppendLine(_dotline1)
                '_content.AppendLine("               " & ("Gross Tot:" & "  " & _TOTAMT.ToString.PadLeft(8)))
            End If
            _content.AppendLine("                  " & ("SST 6% :" & "  " & _TOTTAXAMT.ToString.PadLeft(7)))
            If _ROUNDOFF <> "0.00" Then
                _content.AppendLine("               " & ("ROUND OFF:" & "  " & _ROUNDOFF.ToString.PadLeft(8)))
            End If
            'If _TOTDISCAMT <> "0.00" Then
            '    '_content.AppendLine("Tot Qty:" & _totqt & "         " & ("Gross Tot:" & " " & _TOTAMT.ToString.PadLeft(8)))
            '    _content.AppendLine("                 " & ("Tot Disc:" & "  " & _TOTDISCAMT.ToString.PadLeft(7)))
            '    _content.AppendLine(_dotline1)
            '    _content.AppendLine("               " & ("Gross Tot:" & "  " & _TOTAMT.ToString.PadLeft(8)))
            'End If
            '_content.AppendLine("                  " & ("GST 6% :" & "  " & _TOTTAXAMT.ToString.PadLeft(7)))
            'If _ROUNDOFF <> "0.00" Then
            '    _content.AppendLine("               " & ("ROUND OFF:" & "  " & _ROUNDOFF.ToString.PadLeft(8)))
            'End If
            _content.AppendLine("                " & ("Tot Amt:" & "   " & _NETAMT.ToString.PadLeft(8)))
            _content.AppendLine(_dotline1)
            '   _content.AppendLine("AMOUNT RECEIVED :" & _GIVEAMT)
            '    _content.AppendLine("BALANCE         :" & _BAL)
            _content.AppendLine("            TERIMA KASIH")
            _content.AppendLine("          SILA DATANG LAGI")
            _content.AppendLine(_EMPTY)
            _content.AppendLine(_EMPTY & "---SST Summary------")
            _content.AppendLine("SSType" & "     " & "Amount" & "   " & "SSTAmt")
            _content.AppendLine(_taxlist.ToString)
            _content.AppendLine(".")
            _content.AppendLine(_EMPTY)
            _content.AppendLine(_EMPTY)
            _content.AppendLine(".")
            _content.AppendLine(_EMPTY)
            _content.AppendLine(_EMPTY & Environment.NewLine)
            _content.AppendLine(_EMPTY & Environment.NewLine)
            rtb.AppendText(_content.ToString)
            rtb.SaveFile(Path.Combine(M_Details._appPath, "Reports\WINPRINT.txt"), RichTextBoxStreamType.PlainText)

            Return True
            'MessageBox.Show(_AppPath)
        Catch ex As Exception
            WriteErroLog(ex.ToString())
            Return False
        End Try
    End Function
    Public Function _kotPrint(ByVal kotPrintTable As DataTable, ByVal _DSKOTdtlCOPY As DataTable, ByVal _DSKOTHDRCOPY As DataTable, ByVal _dtaddon As DataTable) As Boolean
        Try
            rtb.Text = ""
            Dim _tav As Integer = 0
            Dim _content As New StringBuilder
            Dim _ItemList As New StringBuilder
            Dim _taxlist As New StringBuilder
            Dim _Tablecaption As String = String.Empty
            Dim _EMPTY As String = String.Empty
            Dim _dotline1 As String = "-------------------------------------"
            Dim _title As String = ""
            Dim _cATION As String
            Dim _HEAD As String = String.Empty
            Dim _dt As New DataTable
            Dim _PrintHead As String = _DSKOTHDRCOPY.Rows(0)("myd_msh_salemode").ToString

            _cATION = _PrintHead
            Select Case _cATION
                Case "Din"
                    _HEAD = "Kot Order" + Environment.NewLine + "Din In"
                Case "Tak"
                    _HEAD = "Kot Order" + Environment.NewLine + "Takeaway"

            End Select
            Dim I As Integer = 0
            '  Dim sum As Integer = dt.AsEnumerable().Sum(Function(row) row.Field(Of Integer)("Salary"))
            Dim _prefix As String = "KOT"
            Dim _bilno As String = _DSKOTHDRCOPY.Rows(0)("myd_msh_number").ToString
            Dim _date As String = DateTime.Now().ToString
            ' Dim _casier As String = _ds.Tables(0).Rows(0).Item("myd_user_StaffName").ToString
            Dim _Tableno As String = _DSKOTHDRCOPY.Rows(0)("myd_msh_table").ToString
            ' Dim _custome As String = _ds.Tables(0).Rows(0).Item("myd_cust_name").ToString & "-" & _ds.Tables(0).Rows(0).Item("myd_cust_mem").ToString
            Dim _counter As String = _DSKOTHDRCOPY.Rows(0)("myd_msh_PCname").ToString
            Dim _hsno As String = "S.No"
            Dim _hitemname As String = "ItemName"
            Dim _hqty As String = "Qty"
            Dim _hamt As String = "Amt"
            Dim _dsno As Integer
            Dim _dsPROCODE As Integer
            Dim _dsName As String = String.Empty
            Dim _dqty As String = String.Empty
            Dim _DTADNAME As String = String.Empty
            Dim _KOTPROCODE As Integer

            _Tablecaption = (_hitemname & "                     " & _hqty)
            '---------------------------------------------------Bill Design------------------------------------------------
            _content.AppendLine("        " & _HEAD.ToString().PadLeft(15))
            _content.AppendLine(_dotline1)
            _content.AppendLine("KOT NO:" & _prefix.ToString() & _bilno)
            _content.AppendLine("Table No: " & _Tableno)
            _content.AppendLine("DATE:" & _date)
            '_content.AppendLine("Cashier:" & _casier)
            _content.AppendLine("Counter: " & _counter.ToString())
            _content.AppendLine(_dotline1)
            _content.AppendLine(_Tablecaption)
            _content.AppendLine(_dotline1)
            Dim _Rowlis As String = String.Empty
            Dim _talis As String = String.Empty
            For Each row As DataRow In kotPrintTable.Rows
                _KOTPROCODE = row("pos_dpro_code").ToString
                Dim _QUERY = From _rs In _DSKOTdtlCOPY.AsEnumerable() Where _rs.Field(Of Integer)("myd_sale_procode") = _KOTPROCODE And _rs.Field(Of Integer)("myd_sale_print") = 1 Select _rs
                If _QUERY.Count > 0 Then
                    If row("pos_dpro_name").ToString.Length >= 15 Then
                        _dsName = row("pos_dpro_name").ToString.Substring(0, 15)
                    Else
                        _dsName = row("pos_dpro_name").ToString
                    End If
                    I = I + 1
                    Dim _DSKOTTAB As New DataTable
                    Dim _DTADDCOPY As New DataTable
                    _DSKOTTAB = _QUERY.CopyToDataTable
                    For Each _ROSS In _DSKOTTAB.Rows
                        _dsno = _ROSS("myd_sale_sno").ToString
                        _dsPROCODE = _ROSS("myd_sale_procode").ToString
                        _dqty = _ROSS("myd_sale_proqty").ToString
                        _content.AppendLine(Trim(_dsName).ToString.PadRight(20).ToUpper() & "        " & _dqty.ToString().PadLeft(3))
                        Dim _QUERY1 = From _rs In _dtaddon.AsEnumerable() Where _rs.Field(Of Integer)("SNO") = _dsno And _rs.Field(Of Integer)("PROCODE") = _dsPROCODE Select _rs
                        If _QUERY1.Count > 0 Then
                            _DTADDCOPY = _QUERY1.CopyToDataTable
                            For Each _RADD In _DTADDCOPY.Rows
                                _DTADNAME = _RADD("NAME").ToString
                                _content.AppendLine(_DTADNAME.ToString().PadLeft(0))
                            Next
                        End If
                    Next
                End If
            Next
            If I = 0 Then
                Exit Try
            End If
            _content.AppendLine(_dotline1)
            _content.AppendLine(".")
            _content.AppendLine(_EMPTY)
            _content.AppendLine(_EMPTY)
            _content.AppendLine(".")
            _content.AppendLine(_EMPTY)
            _content.AppendLine(_EMPTY & Environment.NewLine)
            _content.AppendLine(_EMPTY & Environment.NewLine)
            rtb.AppendText(_content.ToString)
            rtb.SaveFile(Path.Combine(M_Details._appPath, "Reports\KOTPRINT.txt"), RichTextBoxStreamType.PlainText)
            Return True
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function
    Public Function _dotdesign(ByRef _ds As DataSet, ByRef _printxt As String, ByRef erromsg As String) As Boolean
        Try
            rtb.Text = ""
            Dim _content As New StringBuilder
            Dim _ItemList As New StringBuilder
            Dim _taxlist As New StringBuilder
            Dim _Tablecaption As String = String.Empty
            Dim _EMPTY As String = String.Empty
            Dim _dotline1 As String = "-------------------------------------"
            Dim _title As String = ""
            Dim _cATION As String
            Dim _HEAD As String = String.Empty
            Dim _dt As New DataTable


            If _printxt = "PR" Then
                _cATION = _ds.Tables(0).Rows(0).Item("MYD_MSH_PMODE").ToString
                Select Case _cATION
                    Case _saleSetting._modeDefalueSales
                        _HEAD = "CASH PAYMENT"
                    Case "CD"
                        _HEAD = "CARD PAYMENT"
                    Case "CR"
                        _HEAD = "CREDIT BILL"
                End Select

            Else
                _HEAD = "***GUEST PRINT*****"
            End If
            ' If _Readsetting(_err) = True Then
            '_title = _DotmatrixTemp.shopadress
            '   End If
            '  _ds.WriteXmlSchema(_AppPath + "Report.xsd")
            '  Dim sum As Integer = dt.AsEnumerable().Sum(Function(row) row.Field(Of Integer)("Salary"))
            Dim _prefix As String = _ds.Tables(0).Rows(0).Item("myd_msh_prefix").ToString
            Dim _bilno As String = _ds.Tables(0).Rows(0).Item("myd_msh_number").ToString
            Dim _date As String = DateTime.Now().ToString
            Dim _casier As String = _ds.Tables(0).Rows(0).Item("myd_user_StaffName").ToString
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

            _Tablecaption = (_hitemname & "               " & _hqty & "      " & _hamt)
            '---------------------------------------------------Bill Design------------------------------------------------

            _content.AppendLine(_title)
            _content.AppendLine("    " & _HEAD)
            _content.AppendLine(_dotline1)
            _content.AppendLine("BILL NO:" & _prefix.ToString().Substring(0, 3) & _bilno)
            _content.AppendLine("DATE:" & _date)
            _content.AppendLine("Cashier:" & _casier)
            _content.AppendLine("Counter: " & _counter.ToString().Substring(0, 10) & " " & "Table No: " & _Tableno)
            _content.AppendLine("Customer Name:" & _custome.ToUpper)
            _content.AppendLine(_dotline1)
            _content.AppendLine(_Tablecaption)
            _content.AppendLine(_dotline1)
            Dim _Rowlis As String = String.Empty
            Dim _talis As String = String.Empty
            _dt = _ds.Tables(0)
            ' Dim names = From row In _dt.AsEnumerable() Select row.Field(Of String)("myd_sale_taxvalue") Distinct
            ' Dim a = (From row In _dt.AsEnumerable() Select New With {.myd_sale_taxvalue = row.Field(Of Integer)("myd_sale_taxvalue")}).ToList()
            Dim _tav As Integer = 0
            Dim distinctDT As DataTable = _dt.DefaultView.ToTable(True, "myd_sale_taxvalue")
            For Each _Row As DataRow In distinctDT.Rows
                _tav = Convert.ToInt32(_Row("myd_sale_taxvalue"))
                Dim Taxamt As Decimal = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("myd_sale_taxvalue") = _tav).Sum(Function(row) row.Field(Of Decimal)("myd_sale_taxamt"))
                Dim Amount As Decimal = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("myd_sale_taxvalue") = _tav).Sum(Function(row) row.Field(Of Decimal)("myd_sale_gross"))
                _talis = (_tav & "%          " & Amount & "     " & Taxamt)
                _taxlist.AppendLine(_talis)
            Next
            For Each row As DataRow In _ds.Tables(0).Rows
                '_dsno = row("myd_sale_sno").ToString

                If row("myd_pro_name").ToString.Length >= 15 Then
                    _ditemname = row("myd_pro_name").ToString.Substring(0, 15)
                Else
                    _ditemname = row("myd_pro_name").ToString
                End If
                _dqty = row("myd_sale_proqty").ToString()
                _damt = row("myd_sale_amt").ToString
                _Rowlis = (_ditemname.ToString.PadRight(20) & " " & _dqty.ToString.PadLeft(5) & "  " & _damt.ToString.PadLeft(7))
                _ItemList.AppendLine(_Rowlis)
            Next
            _content.AppendLine(_ItemList.ToString.Trim)
            _content.AppendLine(_dotline1)
            _content.AppendLine("Tot Qty:" & _totqt & "         " & ("Sub Tot:" & " " & _GROSS.ToString.PadLeft(8)))
            If _TOTDISCAMT <> "0.00" Then
                '_content.AppendLine("Tot Qty:" & _totqt & "         " & ("Gross Tot:" & " " & _TOTAMT.ToString.PadLeft(8)))
                _content.AppendLine("                 " & ("Tot Disc:" & "  " & _TOTDISCAMT.ToString.PadLeft(7)))
                _content.AppendLine(_dotline1)
                _content.AppendLine("               " & ("Gross Tot:" & "  " & _TOTAMT.ToString.PadLeft(8)))
            End If
            _content.AppendLine("                  " & ("SST 6% :" & "  " & _TOTTAXAMT.ToString.PadLeft(7)))
            If _ROUNDOFF <> "0.00" Then
                _content.AppendLine("               " & ("ROUND OFF:" & "  " & _ROUNDOFF.ToString.PadLeft(8)))
            End If
            _content.AppendLine("                " & ("Tot Amt:" & "   " & _NETAMT.ToString.PadLeft(8)))
            _content.AppendLine(_dotline1)
            _content.AppendLine("AMOUNT RECEIVED :" & _GIVEAMT)
            _content.AppendLine("BALANCE         :" & _BAL)
            _content.AppendLine("            THANK YOU COME AGAIN")
            _content.AppendLine(_EMPTY & "---SST Summary------")
            _content.AppendLine("SSType" & "     " & "Amount" & "   " & "SSTAmt")
            _content.AppendLine(_taxlist.ToString)
            _content.AppendLine(_EMPTY)
            _content.AppendLine(".")
            _content.AppendLine(_EMPTY)
            _content.AppendLine(".")
            _content.AppendLine(_EMPTY & Environment.NewLine)
            _content.AppendLine(_EMPTY & Environment.NewLine)
            _content.AppendLine(".")
            _printxt = _content.ToString
            rtb.AppendText(_content.ToString)
            rtb.SaveFile(Path.Combine(M_Details._appPath, "Reports\WINPRINT.txt"), RichTextBoxStreamType.PlainText)

            Return True
            'MessageBox.Show(_AppPath)
        Catch ex As Exception
            'Throw ex
            Return False
        End Try
    End Function
    Public Function _dotDAILYdesign(ByRef _ds As DataSet) As Boolean
        Try
            Dim _content As New StringBuilder
            Dim _ItemList As New StringBuilder
            Dim _Tablecaption As String = String.Empty
            Dim _EMPTY As String = String.Empty
            Dim _TXT1 As String = "DAILY SALES ON: " & Date.Now
            Dim _TXT2 As String = Environment.MachineName
            Dim _dotline1 As String = "--------------------------------------"
            Dim _dotline2 As String = "======================================"
            Dim _DOTSTAR As String = "**************************************"
            Dim _SHOPNAME As String
            Dim _ATOTNETMAT As String
            Dim _CTOTNETMAT As String
            Dim _CATOTNETAMT As String
            Dim _CDTOTNETAMT As String
            Dim _CRTOTNETAMT As String
            Dim _CACOUNT As String = String.Empty
            Dim _CDCOUNT As String = String.Empty
            Dim _CRCOUNT As String = String.Empty
            Dim _CNCOUNT As String = String.Empty
            If _ds.Tables(0).Rows.Count = 0 Then
                _SHOPNAME = "---"
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
                _CNCOUNT = _ds.Tables(2).Rows(0).Item("BILLCOUNT").ToString
            End If
            If _ds.Tables(3).Rows.Count = 0 Then
                _CATOTNETAMT = "0.00"
            Else
                _CATOTNETAMT = _ds.Tables(3).Rows(0).Item("CANETSALES").ToString()
                _CACOUNT = _ds.Tables(3).Rows(0).Item("BILLCOUNT").ToString

            End If
            If _ds.Tables(4).Rows.Count = 0 Then
                _CDTOTNETAMT = "0.00"
            Else
                _CDTOTNETAMT = _ds.Tables(4).Rows(0).Item("CDNETSALES").ToString
                _CDCOUNT = _ds.Tables(4).Rows(0).Item("BILLCOUNT").ToString

            End If
            If _ds.Tables(5).Rows.Count = 0 Then
                _CRTOTNETAMT = "0.00"
            Else
                _CRTOTNETAMT = _ds.Tables(5).Rows(0).Item("CRNETSALES").ToString
                _CRCOUNT = _ds.Tables(5).Rows(0).Item("BILLCOUNT").ToString
            End If
            Dim _TOTSALES As String = _ds.Tables(0).Rows(0).Item("TOT_SALES").ToString
            Dim _TOTDISCAMT As String = _ds.Tables(0).Rows(0).Item("TOT_DISCAMT").ToString
            Dim _TOTGROSSAMT As String = _ds.Tables(0).Rows(0).Item("TOT_GROSSAMT").ToString
            Dim _TOTAXAMT As String = _ds.Tables(0).Rows(0).Item("TOT_TAXAMT").ToString
            Dim _TOTNETMAT As String = _ds.Tables(0).Rows(0).Item("NETSALES").ToString
            _content.AppendLine(_TXT1)
            _content.AppendLine(_TXT2)
            _content.AppendLine(_DOTSTAR)
            _content.AppendLine("COMPANY :" & _SHOPNAME)
            _content.AppendLine("TOTAL SALES :              " & _TOTSALES.ToString.PadLeft(8))
            _content.AppendLine("TOTAL DISCAMT :            " & _TOTDISCAMT.ToString.PadLeft(8))
            ' _content.AppendLine(Environment.NewLine)
            _content.AppendLine(_dotline1)
            _content.AppendLine("TOTAL GROSSAMT :           " & _TOTGROSSAMT.ToString.PadLeft(8))
            _content.AppendLine("TOTAL TAXAMT  :            " & _TOTAXAMT.ToString.PadLeft(8))
            '_content.AppendLine(Environment.NewLine)
            _content.AppendLine(_dotline1)
            _content.AppendLine("NET AMOUNT :               " & _TOTNETMAT.ToString.PadLeft(8))
            _content.AppendLine(_dotline1)
            _content.AppendLine("SALES SUMMARY")
            _content.AppendLine(_dotline1)
            _content.AppendLine("TODAY SALES :              " & _ATOTNETMAT.ToString.PadLeft(8))
            _content.AppendLine(_DOTSTAR)
            _content.AppendLine("CANCEL AMOUNT :            " & _CTOTNETMAT.ToString.PadLeft(8))
            _content.AppendLine("CASH SALES :               " & _CATOTNETAMT.ToString.PadLeft(8))
            _content.AppendLine("CARD SALES :               " & _CDTOTNETAMT.ToString.PadLeft(8))
            _content.AppendLine("CREDIT SALES :             " & _CRTOTNETAMT.ToString.PadLeft(8))
            _content.AppendLine(_dotline2)
            _content.AppendLine("TOT CASH BILLS :(" & _Check(_CACOUNT) & ")")
            _content.AppendLine("TOT VISA BILLS :(" & _Check(_CDCOUNT) & ")")
            _content.AppendLine("TOT CREDIT BILLS :(" & _Check(_CRCOUNT) & ")")
            _content.AppendLine("TOT CANCEL BILLS :(" & _Check(_CNCOUNT) & ")")
            _content.AppendLine(".")
            _content.AppendLine(_EMPTY & Environment.NewLine)
            _content.AppendLine(_EMPTY & Environment.NewLine)
            _content.AppendLine(".")
            '_content.AppendLine(_dotline1)
            ' _content.AppendLine("PRINTING ON " & Date.Today)
            '  rtb.AppendText("")
            '  rtb.SaveFile(Path.Combine(_AppPath, "Reports\WINDAILYPRINT.txt"), RichTextBoxStreamType.PlainText)
            rtb.AppendText(_content.ToString)
            If File.Exists(Path.Combine(M_Details._appPath, "Reports\WINDAILYPRINT.txt")) = True Then
                File.Delete(Path.Combine(M_Details._appPath, "Reports\WINDAILYPRINT.txt"))
                rtb.SaveFile(Path.Combine(M_Details._appPath, "Reports\WINDAILYPRINT.txt"), RichTextBoxStreamType.PlainText)
            Else
                rtb.SaveFile(Path.Combine(M_Details._appPath, "Reports\WINDAILYPRINT.txt"), RichTextBoxStreamType.PlainText)
            End If

            Return True
        Catch ex As Exception
            Throw
            Return False
        End Try
    End Function
    Public Function _Check(ByVal _sta As String) As String
        Try
            If _sta = String.Empty OrElse _sta Is Nothing Then
                _sta = "0"
            End If
            Return _sta
        Catch ex As Exception
            Return _sta
        End Try
    End Function
    Public Function _dotcumdesign(ByRef erromsg As String, ByRef _frmdate As String, ByRef _todate As String) As Boolean
        Try

            Dim _CONTENT As New StringBuilder
            Dim _ITELIST As New StringBuilder
            Dim _CATLIST As New StringBuilder
            Dim _CATROWLIST As String = String.Empty
            Dim _tAB As New DataTable
            Dim _SHOPNAME As String = ""
            Dim _DATE As String = ""
            Dim _TITLE As String = ""
            Dim _CATE As String = ""
            Dim _PNAME As String = ""
            Dim _QTY As String = ""
            Dim _AMT As String = ""
            Dim _TXT1 As String = "DAILY ITEMWISE SALES ON: " & Date.Now.ToShortDateString()
            Dim _TXT2 As String = Environment.MachineName
            Dim _DOT As String = "--------------------------------------"
            Dim _EMPTY As String = String.Empty
            Dim _Rowlis As String = String.Empty
            Dim _AMTTOT, _QTYTOT As String
            Dim _HEADER As String = ""
            Dim _NETAMT As String = ""
            Dim _CATAMT As Decimal = 0
            Dim _CATQTY As Integer = 0

            'If _Readsetting(_err) = False Then
            '    _HEADER = _DotmatrixTemp._DAILYSALE
            'End If
            ' _CONTENT.AppendLine(_HEADER)
            ' _CONTENT.AppendLine(_EMPTY)
            _CONTENT.AppendLine(_DOT)
            _CONTENT.AppendLine(_TXT1)
            _CONTENT.AppendLine(_TXT2)
            _CONTENT.AppendLine(_DOT)
            _DATEFROM_1 = _frmdate 'Date.Today.ToString("yyyy-MM-dd")
            _DATETO_1 = _todate 'Date.Today.ToString("yyyy-MM-dd")
            Dim _sQLPAR(3) As SqlParameter
            _sQLPAR(0) = New SqlParameter("@MODE", "W")
            _sQLPAR(1) = New SqlParameter("@DATE1", _DATEFROM_1)
            _sQLPAR(2) = New SqlParameter("@DATE2", _DATETO_1)
            _sQLPAR(3) = New SqlParameter("@CATE", "0")
            _DSS = _sqlDataAdapter("SP_CA_SALESREPORT", _sQLPAR, "r")
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
                _DSS1 = _sqlDataAdapter("SP_CA_SALESREPORT", _sQLPAR1, "r")
                _DSS1.Tables(0).TableName = "PRO"
                _DSS1.Tables(1).TableName = "TOT"
                _DSS1.Tables(2).TableName = "GROS"

                For Each _ROW As DataRow In _DSS1.Tables(0).Rows
                    _PNAME = _ROW("PRODUCTNAME").ToString.ToUpper
                    _QTY = _ROW("QTY").ToString
                    _AMT = _ROW("NETAMT").ToString
                    _Rowlis = (_PNAME.ToString.PadRight(16) & "   " & _QTY.ToString.PadLeft(6) & "    " & _AMT.ToString.PadLeft(6))
                    _CONTENT.AppendLine(_Rowlis.ToString)
                Next

                '_CONTENT.AppendLine(_ITELIST.ToString().Trim)
                _AMTTOT = _DSS1.Tables(1).Rows(0).Item("NETAMT").ToString
                _QTYTOT = _DSS1.Tables(1).Rows(0).Item("QTY").ToString
                _CONTENT.AppendLine(_EMPTY)
                _CONTENT.AppendLine(_DOT)
                _CONTENT.AppendLine("NET TOTAL :        " & _QTYTOT.PadLeft(6) & "    " & _AMTTOT.PadLeft(6))
                _CONTENT.AppendLine(_DOT)
                '_CONTENT.AppendLine(_EMPTY)

            Next
            _NETAMT = _DSS1.Tables(2).Rows(0).Item("NETSALES").ToString
            ' _CONTENT.AppendLine(_DOT)
            _CONTENT.AppendLine("NET SALE :                 " & _NETAMT.PadLeft(6))
            _CONTENT.AppendLine(_DOT)
            _SHOPNAME = _DSS1.Tables(0).Rows(0).Item("COMPANY").ToString
            _CONTENT.AppendLine("COMAPANY: " & _SHOPNAME)
            _CONTENT.AppendLine(_EMPTY)
            _CONTENT.AppendLine(_EMPTY)
            _CONTENT.AppendLine(_EMPTY)
            _CONTENT.AppendLine(_EMPTY & "=====================================")
            _CONTENT.AppendLine("DEPTNAME             QTY        AMT")
            _CONTENT.AppendLine(_DOT)
            _dst = _GETDATASUMMARY(_DATEFROM_1, _DATETO_1, "S", "SP_SUMMARTANALYSIS")

            Dim _dt As DataTable
            '   For Each _Darow As DataRow In _dst.Tables(0).Rows
            '  Next
            _dt = _dst.Tables(0)
            Dim sting() As String = {"BRCODE", "BRNAME"}

            Dim _distable As DataTable = _dt.DefaultView.ToTable(True, sting)

            For Each _RO As DataRow In _distable.Rows
                Dim _CatCODE As Integer = Convert.ToInt16(_RO("BRCODE"))
                Dim _CatName As String = _RO("BRNAME").ToString
                'Dim _CatName As String = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("BRCODE") = Convert.ToInt16(_CatCODE)).Single.Field(Of String)("BRNAME")
                Dim AMT As Decimal = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("BRCODE") = _CatCODE).Sum(Function(row) row.Field(Of Decimal)("BILAMT"))
                Dim QTY As Integer = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("BRCODE") = _CatCODE).Sum(Function(row) row.Field(Of Integer)("BILQTY"))
                _CATAMT += AMT
                _CATQTY += QTY
                _CATROWLIST = (_CatName.ToString().PadRight(16) & " " & QTY.ToString().PadLeft(6) & "      " & AMT.ToString().PadLeft(7))
                _CATLIST.AppendLine(_CATROWLIST)
            Next
            _CONTENT.AppendLine(_CATLIST.ToString())
            _CONTENT.AppendLine(_EMPTY & "=====================================")
            _CONTENT.AppendLine("NETAMT" & "" & _CATQTY.ToString().PadLeft(18) & "     " & _CATAMT.ToString().PadLeft(6))
            _CONTENT.AppendLine(_EMPTY & "=====================================")
            _CONTENT.AppendLine(".")
            _CONTENT.AppendLine(_EMPTY & Environment.NewLine)
            _CONTENT.AppendLine(_EMPTY & Environment.NewLine)
            _CONTENT.AppendLine(".")
            rtb.AppendText(_CONTENT.ToString)
            rtb.SaveFile(Path.Combine(M_Details._appPath, "Reports\WINPRODUCTPRINT.txt"), RichTextBoxStreamType.PlainText)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function _GETDATASUMMARY(ByRef _frmdate As String, ByRef _todate As String, ByRef Mode As String, ByRef SPNAME As String) As DataSet
        Try
            Dim _DS = New DataSet
            Dim _Sqlpar(2) As SqlParameter
            _Sqlpar(0) = New SqlParameter("@MODE", Mode)
            _Sqlpar(1) = New SqlParameter("@FRDATE", _frmdate)
            _Sqlpar(2) = New SqlParameter("@TODATE", _todate)
            _DS = _sqlDataAdapter2(SPNAME, _Sqlpar)
            Return _DS
        Catch ex As Exception
            Return _DSS
        End Try
    End Function
    Public Function _dotsummaryDesign(ByRef erromsg As String) As Boolean
        Try
            Dim _CONTENT As New StringBuilder
            Dim _ITELIST As New StringBuilder
            Dim _CATLIST, _CATLIST1, _purlist, _CANCELLIST, _taxlist As New StringBuilder
            Dim _CATROWLIST, _CATROWLIST1 As String
            Dim _PURROWLIST As String = String.Empty
            Dim _PURSPNAME As String = String.Empty
            Dim _PURINVO As String = String.Empty
            Dim _PURAMT As String = String.Empty
            Dim _CANCBILNO As String
            Dim _CANCAMT As String = String.Empty
            Dim _CANCELROWLIST As String = String.Empty
            Dim _tAB As New DataTable
            Dim _SHOPNAME As String = ""
            Dim _DATE As String = ""
            Dim _TITLE As String = ""
            Dim _CATE As String = ""
            Dim _PNAME As String = ""
            Dim _QTY As String = ""
            Dim _AMT As String = ""
            Dim _TXT1 As String = "BILL SUMMARY REPORT STARTED"
            Dim _TXT2 As String = Environment.MachineName
            Dim _DOT2 As String = "======================================"
            Dim _DOT3 As String = "**************************************"
            Dim _DOT1 As String = "--------------------------------------"
            Dim _EMPTY As String = String.Empty
            Dim _Rowlis As String = String.Empty
            Dim _HEADER As String = ""
            Dim _NETAMT As String = ""
            Dim _CATAMT As Decimal = 0
            Dim _CATQTY As Integer = 0
            Dim _MinBillNumber As Integer = 0
            Dim _Countername As String = ""
            Dim _MaxBillNumber As Integer = 0
            Dim TOTBILNUM As Double = 0.0
            Dim _NETSALES As Decimal = 0
            Dim _AVGPERRECE As Double = 0.0
            Dim _PURAMTS As Double = 0.0
            Dim _taxin As String = String.Empty
            _dst = New DataSet
            ' _CONTENT.AppendLine(_HEADER)
            ' _CONTENT.AppendLine(_EMPTY)
            _CONTENT.AppendLine(_DOT2)
            _CONTENT.AppendLine(_TXT1)
            '_CONTENT.AppendLine(_TXT2)
            _CONTENT.AppendLine(_DOT1)
            _DATEFROM_1 = Date.Today.ToString("yyyy-MM-dd")
            _DATETO_1 = Date.Today.ToString("yyyy-MM-dd")
            _dst = _GETDATASUMMARY(_DATEFROM_1, _DATETO_1, "SI", "SP_SUMMARTANALYSIS")
            Dim _dt, _dtt As New DataTable
            Dim strg() As String = {"myd_sale_countername"}
            _dt = _dst.Tables(0)
            _dtt = _dt.DefaultView.ToTable(True, strg)
            _CONTENT.AppendLine("SUMMARY DETAILS RECEIPT")
            _CONTENT.AppendLine(_DOT2)
            For Each _drows As DataRow In _dtt.Rows
                _Countername = _drows("myd_sale_countername").ToString
                _MaxBillNumber = _dt.AsEnumerable().Where(Function(row) row.Field(Of String)("myd_sale_countername") = _Countername).Max(Function(row) row.Field(Of Integer)("myd_sale_billno"))
                _MinBillNumber = _dt.AsEnumerable().Where(Function(row) row.Field(Of String)("myd_sale_countername") = _Countername).Min(Function(row) row.Field(Of Integer)("myd_sale_billno"))
                _NETSALES = _dt.AsEnumerable().Where(Function(row) row.Field(Of String)("myd_sale_status") = "P").Sum(Function(row) row.Field(Of Decimal)("myd_sale_netamt"))
                _CATROWLIST = (_Countername & ":" & "FIRST:" & _MaxBillNumber.ToString & "/" & "LAST:" & _MinBillNumber.ToString)
                _CATLIST.AppendLine(_CATROWLIST.ToString())
                'Dim min = _dt.Compute("min(myd_sale_billno)", String.Empty)
                'Dim max = _dt.Compute("max(myd_sale_billno)", String.Empty)
            Next
            _CONTENT.AppendLine(_CATLIST.ToString)
            TOTBILNUM = Val(_MaxBillNumber + 1) - Val(_MinBillNumber)
            _AVGPERRECE = Convert.ToDouble(_NETSALES / TOTBILNUM)
            _CONTENT.AppendLine("TOTAL RECEIPT :        " & Val(_MaxBillNumber + 1) - Val(_MinBillNumber))
            _CONTENT.AppendLine("AVERAGE PER RECEIPT :  " & _AVGPERRECE.ToString("0.00").PadLeft(6))
            _dst = New DataSet
            _dst = _GETDATASUMMARY(_DATEFROM_1, _DATETO_1, "DE", "SP_SUMMARTANALYSIS")
            _CONTENT.AppendLine(_DOT2)
            _CONTENT.AppendLine("VOID SUMMARY REPORT")
            _CONTENT.AppendLine(_DOT1)
            For Each _DROWS As DataRow In _dst.Tables(0).Rows
                Dim BILNUM = _DROWS("BILNUM").ToString
                Dim PNAME = _DROWS("PNAME").ToString
                Dim DELCOUNT = _DROWS("DELCOUNT").ToString
                _CATROWLIST1 = (BILNUM & ":  " & PNAME.ToString & "  " & DELCOUNT.ToString().PadLeft(6))
                _CATLIST1.AppendLine(_CATROWLIST1.ToString())
            Next
            _CONTENT.AppendLine(_CATLIST1.ToString)
            _CONTENT.AppendLine(_DOT2.ToString())
            _CONTENT.AppendLine("PURCHASE DETAILS").ToString()
            _CONTENT.AppendLine(_DOT1.ToString)
            _dst = New DataSet
            _dst = _getPurchase("SS", "SP_POENTRY")
            Dim cou As Integer = _dst.Tables(0).Rows.Count
            Dim _NETPURAMT As Decimal = 0
            If cou > 0 Then
                For Each _rOWS As DataRow In _dst.Tables(0).Rows
                    _PURSPNAME = _rOWS("SPNAME").ToString
                    _PURAMT = _rOWS("INV_AMT").ToString()
                    _PURINVO = _rOWS("INV_NO").ToString
                    _PURAMTS += Convert.ToDouble(_PURAMT)
                    _PURROWLIST = ("INV_NO:" & _PURINVO & Environment.NewLine & _PURSPNAME.ToString.PadLeft(6).ToUpper & " :            " & _globalSetting.CurrencySimple & Convert.ToDouble(_PURAMTS.ToString().PadLeft(20)).ToString("0.00"))
                    _purlist.AppendLine(_PURROWLIST.ToString())
                    _PURAMTS = 0.0
                    _NETPURAMT += Convert.ToDecimal(_PURAMT)
                Next
            End If
            Dim _PROFIT As Double = (Val(_NETSALES - _PURAMTS) / Val(_NETSALES) * 100)
            ' Dim _COSTING As Double = (Val(_NETSALES - _PURAMTS) / Val(_PURAMTS) * 100)
            _CONTENT.AppendLine(_purlist.ToString())
            _CONTENT.AppendLine(_DOT2)
            _CONTENT.AppendLine("NET PURCHASE:          " & _NETPURAMT.ToString("0.00").PadLeft(6))
            _CONTENT.AppendLine(_DOT1.ToString)
            _CONTENT.AppendLine("NET PROFIT:                " & _PROFIT.ToString("0.00") & "%")
            _CONTENT.AppendLine(_DOT1.ToString)
            '_CONTENT.AppendLine("NET COSTING:                   " & _COSTING.ToString("0.00"))
            _CONTENT.AppendLine("CANCEL BILL DETAILS (HOLD BILL)")
            _CONTENT.AppendLine(_DOT2.ToString())
            _dst = New DataSet
            _dst = _getCancel("CAN", "SP_POENTRY")
            Dim count As Integer = _dst.Tables(0).Rows.Count

            Dim _LOOPCANAMT As Decimal = 0
            If count > 0 Then
                For Each _DROW As DataRow In _dst.Tables(0).Rows
                    _CANCBILNO = _DROW("BILLNO").ToString
                    _CANCAMT = _DROW("NETAMT").ToString
                    _CANCELROWLIST = ("BILL NO: " & _CANCBILNO.ToString() & "        " & _globalSetting.CurrencySimple & _CANCAMT.ToString().PadLeft(6))
                    _CANCELLIST.AppendLine(_CANCELROWLIST.ToString())
                    _LOOPCANAMT += Convert.ToDecimal(_CANCAMT)
                Next

            End If
            _CONTENT.AppendLine(_CANCELLIST.ToString())
            _CONTENT.AppendLine(_DOT2)
            _CONTENT.AppendLine("NET CANCEL AMT :           " & _LOOPCANAMT.ToString("0.00").PadLeft(6))
            'Dim _Rowlis As String = String.Empty
            _CONTENT.AppendLine(_DOT2)
            _CONTENT.AppendLine("TAX SUMMARY")
            _CONTENT.AppendLine(_DOT1)
            Dim _talis As String = String.Empty
            _dst = New DataSet
            _dst = _getCancel("SAL", "SP_POENTRY")
            _dt = _dst.Tables(0)
            Dim _tav As Integer = 0
            Dim distinctDT As DataTable = _dt.DefaultView.ToTable(True, "myd_sale_taxvalue")
            _CONTENT.AppendLine("SSType" & "        " & "Amount" & "        " & "SSTAmt")
            _CONTENT.AppendLine(_DOT1)
            For Each _Row As DataRow In distinctDT.Rows
                '  _taxin = _functionModule.GSTCODE
                _tav = Convert.ToInt32(_Row("myd_sale_taxvalue"))
                If _taxin = "0" Then
                    Dim Taxamt As Decimal = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("myd_sale_taxvalue") = _tav).Sum(Function(row) row.Field(Of Decimal)("myd_sale_taxamt"))
                    Dim Amount As Decimal = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("myd_sale_taxvalue") = _tav).Sum(Function(row) row.Field(Of Decimal)("myd_sale_bnetamt"))
                    _talis = (_tav & "%              " & Amount & "       " & Taxamt)
                    _taxlist.AppendLine(_talis)
                Else
                    Dim Taxamt As Decimal = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("myd_sale_taxvalue") = _tav).Sum(Function(row) row.Field(Of Decimal)("myd_sale_taxamt"))
                    Dim Amount As Decimal = _dt.AsEnumerable().Where(Function(row) row.Field(Of Integer)("myd_sale_taxvalue") = _tav).Sum(Function(row) row.Field(Of Decimal)("myd_sale_gross"))
                    _talis = (_tav & "%              " & Amount & "       " & Taxamt)
                    _taxlist.AppendLine(_talis)
                End If

            Next
            _CONTENT.AppendLine(_taxlist.ToString())
            _CONTENT.AppendLine("***************END REPORT*************")
            _CONTENT.AppendLine(".")
            _CONTENT.AppendLine(_EMPTY & Environment.NewLine)
            _CONTENT.AppendLine(_EMPTY & Environment.NewLine)
            _CONTENT.AppendLine(".")
            RTBSUM.AppendText(_CONTENT.ToString)
            RTBSUM.SaveFile(Path.Combine(M_Details._appPath, "Reports\WINSUMMARY.txt"), RichTextBoxStreamType.PlainText)
            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function
    Public Function _getPurchase(ByRef Mode As String, ByRef SPNAME As String) As DataSet
        Try
            Dim _DS = New DataSet
            Dim _Sqlpar(0) As SqlParameter
            _Sqlpar(0) = New SqlParameter("@MODE", Mode)

            _DS = _sqlDataAdapter2(SPNAME, _Sqlpar)
            Return _DS
        Catch ex As Exception
            Return _DSS
        End Try
    End Function
    Public Function _getCancel(ByRef Mode As String, ByRef SPNAME As String) As DataSet
        Try
            Dim _DS = New DataSet
            Dim _Sqlpar(0) As SqlParameter
            _Sqlpar(0) = New SqlParameter("@MODE", Mode)

            _DS = _sqlDataAdapter2(SPNAME, _Sqlpar)
            Return _DS
        Catch ex As Exception
            Return _DSS
        End Try
    End Function
    Public Function _rptTaxsummary(ByRef _Dsrpt As DataSet, ByRef _FRMDATE As String, ByRef _TODATE As String)
        Try
            Dim _CONTENT As New StringBuilder
            Dim _ITELIST As New StringBuilder
            Dim _CATLIST As New StringBuilder
            Dim _CATROWLIST As String = String.Empty
            Dim _tAB As New DataTable
            Dim _datecheck1 As Date = _FRMDATE
            Dim _datecheck2 As Date = _TODATE
            Dim _SHOPNAME As String = _Dsrpt.Tables(0).Rows(0)("COMPANY").ToString
            Dim _DATE As String = _datecheck1.ToString("dd-MM-yyyy") & " To " & _datecheck2.ToString("dd-MM-yyyy")
            Dim _TITLE As String = "Tax Summary Report"
            Dim _SDATE As Date
            Dim _PNAME As String = ""
            Dim _SAMT As String = ""
            Dim _STAXAMT As String = ""
            Dim _TXT1 As String = "DAILY SALES ON: " & Date.Now.ToShortDateString()
            Dim _TXT2 As String = Environment.MachineName
            Dim _DOT As String = "--------------------------------------"
            Dim _dot2 As String = "**************************************"
            Dim _dot3 As String = "++++++++++++++++++++++++++++++++++++++"
            Dim _EMPTY As String = String.Empty
            Dim _Rowlis As String = String.Empty
            Dim _SumSamt As Double = 0
            Dim _SumTaxAmt As Double = 0
            Dim _sumNetAmt As Double = 0
            Dim _HEADER As String = ""
            Dim _SNETAMT As String = ""
            Dim _NET As Decimal = 0
            'If _Readsetting(_err) = False Then
            '    _HEADER = _DotmatrixTemp._DAILYSALE
            'End If
            _CONTENT.AppendLine(_TITLE.PadRight(15))
            _CONTENT.AppendLine(_dot2)
            _CONTENT.AppendLine(_TXT1)
            _CONTENT.AppendLine("Counter:" & _TXT2)
            _CONTENT.AppendLine(_DATE)
            _CONTENT.AppendLine(_SHOPNAME)
            _CONTENT.AppendLine(_DOT)
            _CONTENT.AppendLine("DATE       SALES      SST    AMOUNT")
            _CONTENT.AppendLine(_dot3)
            For Each _drow As DataRow In _Dsrpt.Tables(0).Rows
                _SDATE = _drow("BILLDATE")
                _SAMT = _drow("GROSS_AMT")
                _STAXAMT = _drow("TAX_AMT")
                _SNETAMT = _drow("NETAMT")
                _Rowlis = (_SDATE.ToString("dd-MM-yy").PadRight(8) & " " & _SAMT.ToString.PadLeft(8) & " " & _STAXAMT.ToString.PadLeft(8) & "  " & _SNETAMT.ToString.PadLeft(8))
                _CONTENT.AppendLine(_Rowlis.ToString)
                _SumSamt += _drow("GROSS_AMT")
                _SumTaxAmt += _drow("TAX_AMT")
                _sumNetAmt += _drow("NETAMT")
            Next

            _CONTENT.AppendLine(_dot2)
            _CONTENT.AppendLine("Net :    " & _SumSamt.ToString("0.00").PadLeft(8) & " " & _SumTaxAmt.ToString("0.00").PadLeft(8) & "  " & _sumNetAmt.ToString("0.00").PadLeft(8))
            _CONTENT.AppendLine(_dot2)
            ' _CONTENT.AppendLine(_EMPTY & "=====================================")
            _CONTENT.AppendLine(".")
            _CONTENT.AppendLine(_EMPTY & Environment.NewLine)
            _CONTENT.AppendLine(_EMPTY & Environment.NewLine)
            _CONTENT.AppendLine(".")
            rtb.AppendText(_CONTENT.ToString)
            rtb.SaveFile(Path.Combine(M_Details._appPath, "Reports\WINTAXSUMMARY.txt"), RichTextBoxStreamType.PlainText)
            Return True
        Catch ex As Exception
            Return False

        End Try
    End Function
    Public Function _PayOutSinglePrint() As Boolean
        Try
            Dim _dsDataLoad As New DataSet
            Dim _sQLCUSTSup(0) As SqlParameter
            _sQLCUSTSup(0) = New SqlParameter("@mode", "Pr") '"SUP" /"STA"
            _dsDataLoad = _sqlDataAdapter2("sp_custsupp_select", _sQLCUSTSup)
            If _dsDataLoad.Tables(0).Rows.Count < 0 Then
                Return False
            End If
            Dim _CONTENT As New StringBuilder
            Dim _ITELIST As New StringBuilder
            Dim _CATLIST As New StringBuilder
            Dim _CATROWLIST As String = String.Empty
            Dim _tAB As New DataTable
            Dim _datecheck1 As Date = Date.Now
            Dim ShiftNo As String = _saleSetting._curShiftno
            Dim _SHOPNAME As String = _printHeaderDesign._address
            Dim _DATE As String = _datecheck1.ToString '("dd-MM-yyyy")
            Dim _TITLE As String = "PayOut Report"
            Dim _SDATE As Date
            Dim _PNAME As String = ""
            Dim _SAMT As String = ""
            Dim _Supplier As String = ""
            Dim _TXT2 As String = RegistrationDetails._localcompname
            Dim _DOT As String = "--------------------------------------"
            Dim _dot2 As String = "**************************************"
            Dim _dot3 As String = "++++++++++++++++++++++++++++++++++++++"
            Dim _EMPTY As String = String.Empty
            Dim _Rowlis As String = String.Empty
            Dim _SumSamt As Double = 0
            Dim _SumTaxAmt As Double = 0
            Dim _sumNetAmt As Double = 0
            Dim _HEADER As String = ""
            Dim _SRemarks As String = ""
            Dim _NET As Decimal = 0
            'If _Readsetting(_err) = False Then
            '    _HEADER = _DotmatrixTemp._DAILYSALE
            'End If
            _CONTENT.AppendLine(_TITLE.PadRight(15))
            _CONTENT.AppendLine(_dot2)
            _CONTENT.AppendLine("Counter:" & _TXT2)
            _CONTENT.AppendLine(_DATE)
            _CONTENT.AppendLine(_SHOPNAME)
            _CONTENT.AppendLine(_DOT)
            '_CONTENT.AppendLine("DATE       SALES      SST    AMOUNT")
            _CONTENT.AppendLine(_dot3)
            For Each _drow As DataRow In _dsDataLoad.Tables(0).Rows
                _SDATE = _drow("payd_datetime")
                _SAMT = _drow("payd_amount")
                _Supplier = _drow("payd_name")
                _SRemarks = _drow("payd_remarks")
                _Rowlis = (_SDATE.ToString("dd-MM-yy").PadRight(8) & vbNewLine & _Supplier.ToString.PadLeft(1) & " " & _SAMT.ToString.PadLeft(8) & vbNewLine & _SRemarks.ToString.PadLeft(1))
                _CONTENT.AppendLine(_Rowlis.ToString)
            Next

            _CONTENT.AppendLine(_dot2)
            '  _CONTENT.AppendLine("Net :    " & _SumSamt.ToString("0.00").PadLeft(8) & " " & _SumTaxAmt.ToString("0.00").PadLeft(8) & "  " & _sumNetAmt.ToString("0.00").PadLeft(8))
            '  _CONTENT.AppendLine(_dot2)
            ' _CONTENT.AppendLine(_EMPTY & "=====================================")
            _CONTENT.AppendLine(".")
            _CONTENT.AppendLine(_EMPTY & Environment.NewLine)
            _CONTENT.AppendLine(_EMPTY & Environment.NewLine)
            _CONTENT.AppendLine(".")
            rtb.AppendText(_CONTENT.ToString)
            rtb.SaveFile(Path.Combine(M_Details._appPath, "Reports\WINPayOutSingle.txt"), RichTextBoxStreamType.PlainText)
            'Dim clsfun As New clsfunction
            'clsfun.pdf(M_Details._appPath & "\Reports\", "WINPayOutSingle", 1, Date.Now)
            Return True
        Catch ex As Exception
            Return False

        End Try
    End Function
End Class
