Imports DevExpress.XtraGrid
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Data
Imports System
Imports System.Net.Mail
Imports System.Globalization
Imports System.Net
Imports System.Runtime.InteropServices
Imports PosRetailWebBilling.clssalesProperty

'Imports PDFCOVERTER
Module clsfunction

    Dim _ds As DataSet
    Dim errMsg As String
    Dim splitComId As Integer
    Dim splitLocId As Integer
    Dim _Mode As Integer
    Dim _TabCardNo As DataTable
    Dim _ReadCardNo As String
    Dim _dsCardCheck As DataSet
    Dim _fromCardno As String
    Dim _toCardno As String
    Dim _tablNo As String
    Dim counter As Integer = 0
    Dim _dsshiftClose As DataSet
    Dim _dsDayClose As DataSet
    Dim printCommand As New PrintCommand
    Dim POSsettingsdt As DataTable
    Dim _dateTime As DateTime

    'Public Sub pdf(ByRef _path As String, ByRef _filename As String, ByRef _dayOrShiftno As Integer, ByRef _dateTime As DateTime)
    '    clspdfConverter.pdfConverter(_path, _filename, _dayOrShiftno, _dateTime)
    'End Sub
    Public Function _readSetting(ByRef errMsg As String) As Boolean
        Try
            If File.Exists(M_Details._appPath & "\Reports\ShopAddress.txt") Then
                _DotmatrixTemp.shopadress = File.ReadAllText(M_Details._appPath & "\Reports\ShopAddress.txt")
            End If

            'If File.Exists(_AppPath & "\Reports\DOTDAILYPRINT") Then
            '    _DotmatrixTemp._DAILYSALE = File.ReadAllText(_AppPath & "\Reports\DOTDAILYPRINT")
            'End If
            Dim _RegSettingDs = New DataSet
            If File.Exists(M_Details._appPath & "\Reports\_RegSettingDs.xml") Then
                _RegSettingDs.ReadXml(M_Details._appPath & "\Reports\_RegSettingDs.xml")
                RegistrationDetails._localPcname = _RegSettingDs.Tables(0).Rows(0)("MACHINENAME").ToString
            End If
            _posSettingDs = New DataSet

            _posPrintHeadDesign = New DataSet
            If File.Exists(M_Details._appPath & "\LayOut\PrintHeaderDesign.xml") Then
                _posPrintHeadDesign.ReadXml(M_Details._appPath & "\LayOut\PrintHeaderDesign.xml")
                For Each _Drows As DataRow In _posPrintHeadDesign.Tables(0).Rows
                    _printHeaderDesign._logoPath = _Drows("LogoPath")
                    _printHeaderDesign._logoState = _Drows("LogoState")
                    _printHeaderDesign._shopname = _Drows("ShopName")
                    _printHeaderDesign._address = _Drows("Address")
                    _printHeaderDesign._bottomMsg = _Drows("BottomMsg")
                    _printHeaderDesign._emptyrow = _Drows("Empty")
                Next
            End If


            If File.Exists(M_Details._appPath & "\LayOut\PrintProfileSetting.xml") Then
                Dim str() As String = {"Sales", "Guest Print", "Purchase", "Inventory", "Shift Close", "Day Close", "Tax Report", "Payouts", "Tax Cut Report"}
                PrintProfile._dsPrintProfile = New DataSet
                PrintProfile._dsPrintProfile.ReadXml(M_Details._appPath & "\LayOut\PrintProfileSetting.xml")
                For Each _Drows As DataRow In PrintProfile._dsPrintProfile.Tables(0).Rows
                    Dim ProfileType = _Drows("ProfileType")
                    Dim ProfileSingle = _Drows("ProfileSingle")
                    If ProfileSingle = "Print" Then
                        Select Case ProfileType
                            Case str(0)
                                PrintProfile._salesFilename = _Drows("FileName")
                                'PrintProfile._Mulfile = {{_Drows("FileName"), _Drows("ProfileType")}}
                            Case str(1)
                                PrintProfile._salesGuestprintName = _Drows("FileName")
                            Case str(2)
                                PrintProfile._purchaseFilename = _Drows("FileName")
                            Case str(3)
                                PrintProfile._inventoryprintName = _Drows("FileName")
                            Case str(4)
                                PrintProfile._shiftclosename = _Drows("FileName")
                            Case str(5)
                                PrintProfile._dayclosename = _Drows("FileName")
                            Case str(6)
                                PrintProfile._taxprintname = _Drows("FileName")
                            Case str(7)
                                PrintProfile._payouts = _Drows("FileName")
                            Case str(8)
                                PrintProfile._taxCutprintname = _Drows("FileName")
                        End Select
                    ElseIf ProfileSingle = "Mail" Then
                        Select Case ProfileType
                            Case str(0)
                                MailProfile._MailsalesFilename = _Drows("FileName")
                                'PrintProfile._Mulfile = {{_Drows("FileName"), _Drows("ProfileType")}}
                            Case str(1)
                                'MailProfile._MailsalesGuestprintName = _Drows("FileName")
                            Case str(2)
                                ' MailProfile._Mailshiftclosename = _Drows("FileName")
                            Case str(3)
                                'MailProfile._Maildayclosename = _Drows("FileName")
                            Case str(4)
                                MailProfile._Mailshiftclosename = _Drows("FileName")
                            Case str(5)
                                MailProfile._Maildayclosename = _Drows("FileName")
                            Case str(6)
                                MailProfile._Mailtaxprintname = _Drows("FileName")
                            Case str(7)
                                'MailProfile._MailTaxReportCut = _Drows("FileName")
                            Case str(8)
                                MailProfile._MailTaxReportCut = _Drows("FileName")
                        End Select
                    End If

                Next
            End If

            If _funMailConfiguration(errMsg) = False Then
                WriteErroLog(errMsg)
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteErroLog(ex.Message)
            Return False
        End Try
    End Function

    Public Function _funMailConfiguration(ByRef ERR As String) As Boolean
        Try
            If File.Exists(M_Details._appPath & "\LayOut\MailConfiguration.xml") Then
                'Dim str() As String = {"Sales", "Guest Print", "Purchase", "Inventory", "Shift Close", "Day Close", "Tax Report", "Payouts"}
                MailProfile._dsMailPrintProfile = New DataSet
                MailProfile._dsMailPrintProfile.ReadXml(M_Details._appPath & "\LayOut\MailConfiguration.xml")
                For Each _Drows As DataRow In MailProfile._dsMailPrintProfile.Tables(0).Rows
                    MailConfiguration._myMailID = _Drows("MYMAILID")
                    MailConfiguration._myPassword = _Drows("MYPASS")
                    MailConfiguration._myHostID = _Drows("MYHOST")
                    MailConfiguration._custMailID1 = _Drows("CUSTMAIL1")
                    MailConfiguration._custMailID2 = _Drows("CUSTMAIL2")
                    MailConfiguration._custAddress = _Drows("CUSTADDR")
                    MailConfiguration._custPhone = _Drows("CUSTPHONE")
                Next
            End If
            Return True
        Catch ex As Exception
            ERR = ex.Message
            Return False

        End Try
    End Function
    Public Function _DateConversion(ByRef recDate As DateTime) As DateTime
        Try
            Dim reformatted As String = ""
            Dim _dateTime As DateTime = DateTime.Parse(recDate)
            reformatted = _dateTime.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)
            Return reformatted
        Catch ex As Exception
            Return "Null"
        End Try
    End Function
    Public Sub _DateConversion(ByRef recDate As DateTime, ByRef ColDate As String)
        Try
            Dim reformatted As String = ""
            Dim _dateTime As DateTime = DateTime.Parse(recDate)
            ColDate = _dateTime.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub _DateConversion(ByRef recDate As DateTime, ByRef ColDate As String, ByRef dateFormat As String)
        Try
            Dim reformatted As String = ""
            Dim _dateTime As DateTime = DateTime.Parse(recDate)
            ColDate = _dateTime.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture)
            dateFormat = _dateTime.ToString("ddMMyyyy", CultureInfo.InvariantCulture)
        Catch ex As Exception

        End Try
    End Sub
    Public Function _DateConversion(ByRef fromDate As DateTime, ByRef toDate As DateTime, ByRef retfromdate As String, ByRef rettodate As String) As Boolean
        Try
            Dim reformatted As String = ""
            Dim _dateTimefrom As DateTime = DateTime.Parse(fromDate)
            Dim _dateTimeto As DateTime = DateTime.Parse(toDate)
            retfromdate = _dateTimefrom.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            rettodate = _dateTimeto.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function _checkSystemSetting(ByRef _MenuCode As String) As Boolean
        Try
            If File.Exists(M_Details._appPath & "\LayOut\SystemSetting.xml") Then
                'Dim str() As String = {"CS001"}
                Dim menucode = _MenuCode
                SystemSettings._dsSystemSettings = New DataSet
                SystemSettings._dsSystemSettings.ReadXml(M_Details._appPath & "\LayOut\SystemSetting.xml")
                Dim state = From _SS In SystemSettings._dsSystemSettings.Tables(0).AsEnumerable() Where _SS.Field(Of String)("SYSTEMCODE") = menucode And _SS.Field(Of String)("SYSTEMNAME") = RegistrationDetails._localPcname Select _SS.Field(Of String)("ACTIVE")


                If state(0).ToString = "ACTIVE" Then
                    Return True
                ElseIf state(0).ToString = "IN-ACTIVE" Then
                    Return False
                End If
                'For Each _Drows As DataRow In SystemSettings._dsSystemSettings.Tables(0).Rows
                '    SystemSettings._sysCode = _Drows("SYSTEMCODE")
                '    SystemSettings._pcname = _Drows("SYSTEMNAME")
                '    SystemSettings._filename = _Drows("FILENAME")
                '    SystemSettings._activation = _Drows("ACTIVE")
                '    Select Case SystemSettings._sysCode
                '        Case str(0)
                '            If SystemSettings._pcname = RegistrationDetails._localPcname AndAlso SystemSettings._activation = "ACTIVE" Then
                '                SystemSettingStatus._CardSystem = True
                '                Return True
                '            Else
                '                SystemSettingStatus._CardSystem = False
                '                Return False
                '            End If
                '    End Select
                'Next
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Sub _checkSubGate(ByRef _MenuCode As String, ByRef _SubCode As String)
        Try
            If File.Exists(M_Details._appPath & "\LayOut\SystemSetting.xml") Then
                'Dim str() As String = {"CS001"}
                Dim menucode = _MenuCode
                SystemSettings._dsSystemSettings = New DataSet
                SystemSettings._dsSystemSettings.ReadXml(M_Details._appPath & "\LayOut\SystemSetting.xml")
                Dim state = From _SS As DataRow In SystemSettings._dsSystemSettings.Tables(0).AsEnumerable() Where _SS.Field(Of String)("SYSTEMCODE") = menucode Select _SS.Field(Of String)("FILENAME")



                If state.Any Then
                    _SubCode = state(0).ToString
                End If
                'If state(0).ToString = "ACTIVE" Then
                '    Return True
                'ElseIf state(0).ToString = "IN-ACTIVE" Then
                '    Return False
                'End If
                'For Each _Drows As DataRow In SystemSettings._dsSystemSettings.Tables(0).Rows
                '    SystemSettings._sysCode = _Drows("SYSTEMCODE")
                '    SystemSettings._pcname = _Drows("SYSTEMNAME")
                '    SystemSettings._filename = _Drows("FILENAME")
                '    SystemSettings._activation = _Drows("ACTIVE")
                '    Select Case SystemSettings._sysCode
                '        Case str(0)
                '            If SystemSettings._pcname = RegistrationDetails._localPcname AndAlso SystemSettings._activation = "ACTIVE" Then
                '                SystemSettingStatus._CardSystem = True
                '                Return True
                '            Else
                '                SystemSettingStatus._CardSystem = False
                '                Return False
                '            End If
                '    End Select
                'Next
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Function _getCompId(ByRef Compname As String) As Integer
        Try
            Dim _retValue As Integer = 0


            Return _retValue
        Catch ex As Exception
            Return 0
        End Try

    End Function
    Public Function _usernormalCheck(ByRef passcode As String, ByRef mode As String) As Boolean
        Try
            _ds = New DataSet
            Dim _sqlPara(1) As SqlParameter
            _sqlPara(0) = New SqlParameter("@mode", mode)
            _sqlPara(1) = New SqlParameter("@str", passcode)
            _ds = _sqlDataAdapter2("sp_general_query2", _sqlPara)
            If _ds.Tables(0).Rows.Count > 0 Then
                _companyInfo.UserId = _ds.Tables(0).Rows(0)(0).ToString
                _companyInfo.UserName = _ds.Tables(0).Rows(0)(1).ToString
                Return True
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function _userMenuRightsCheck(ByRef UserId As Integer, ByRef MenuCode As Integer, ByRef ErrMsg As String) As Boolean
        Try
            _ds = New DataSet
            Dim _sqlPara(3) As SqlParameter
            _sqlPara(0) = New SqlParameter("@mode", "frm")
            _sqlPara(1) = New SqlParameter("@userid", UserId)
            _sqlPara(2) = New SqlParameter("@userpass", "0")
            _sqlPara(3) = New SqlParameter("@menurid", MenuCode)
            _ds = _sqlDataAdapter2("sp_userValidate", _sqlPara)
            If _ds.Tables(0).Rows.Count > 0 Then
                Dim _StCode = _ds.Tables(0).Rows(0)(0).ToString
                Dim _Status = _ds.Tables(0).Rows(0)(1).ToString
                If _StCode = "1" Then
                    Return True
                Else
                    ErrMsg = _Status
                    Return False
                End If

            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function _userMenuRightsCheckWithPass(ByRef password As String, ByRef MenuCode As Integer, ByRef ErrMsg As String) As Boolean
        Try
            _ds = New DataSet
            Dim _sqlPara(3) As SqlParameter
            _sqlPara(0) = New SqlParameter("@mode", "chk")
            _sqlPara(1) = New SqlParameter("@userid", "0")
            _sqlPara(2) = New SqlParameter("@userpass", password)
            _sqlPara(3) = New SqlParameter("@menurid", MenuCode)
            _ds = _sqlDataAdapter2("sp_userValidate", _sqlPara)
            If _ds.Tables(0).Rows.Count > 0 Then
                Dim _StCode = _ds.Tables(0).Rows(0)(0).ToString
                Dim _Status = _ds.Tables(0).Rows(0)(1).ToString
                If _StCode = "1" Then
                    _companyInfo.UserId = _ds.Tables(0).Rows(0)(2).ToString
                    Return True
                Else
                    ErrMsg = _Status
                    Return False
                End If

            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function _userOrderSystem(ByRef password As String, ByRef MenuCode As Integer, ByRef ErrMsg As String) As Boolean
        Try
            _ds = New DataSet
            Dim _sqlPara(3) As SqlParameter
            _sqlPara(0) = New SqlParameter("@mode", "Order")
            _sqlPara(1) = New SqlParameter("@userid", "0")
            _sqlPara(2) = New SqlParameter("@userpass", password)
            _sqlPara(3) = New SqlParameter("@menurid", MenuCode)
            _ds = _sqlDataAdapter2("sp_userValidate", _sqlPara)
            If _ds.Tables(0).Rows.Count > 0 Then
                Dim _StCode = _ds.Tables(0).Rows(0)(0).ToString
                Dim _Status = _ds.Tables(0).Rows(0)(1).ToString
                If _StCode = "1" Then
                    _companyInfo.UserId = _ds.Tables(0).Rows(0)(2).ToString
                    _companyInfo.UserName = _ds.Tables(0).Rows(0)(3).ToString
                    Return True
                Else
                    ErrMsg = _Status
                    Return False
                End If

            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function _userCheck(ByRef passcode As String, ByRef mode As String) As Boolean
        Try
            Dim _rescal As New DataSet
            Dim _sqlPara(1) As SqlParameter
            _sqlPara(0) = New SqlParameter("@mode", mode)
            _sqlPara(1) = New SqlParameter("@str", passcode)
            _rescal = _sqlDataAdapter("sp_general_query2", _sqlPara, errMsg)
            If _rescal.Tables(0).Rows.Count > 0 Then
                Dim _stCode = _rescal.Tables(0).Rows(0)(0).ToString
                If _stCode = "1" Then
                    _companyInfo.UserId = _rescal.Tables(0).Rows(0)(1).ToString
                    Return True
                Else
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function _getCurrentShiftDayno(ByRef errMsg As String) As Boolean
        Try
            _ds = New DataSet
            Dim _sqlPara(1) As SqlParameter
            _sqlPara(0) = New SqlParameter("@mode", "curSD") 'current shift and day no
            _sqlPara(1) = New SqlParameter("@str", RegistrationDetails._localPcname)
            _ds = _sqlDataAdapter2("sp_general_query2", _sqlPara)
            If _ds.Tables(0).Rows.Count > 0 Then
                _saleSetting._curDayno = _ds.Tables(0).Rows(0).Item(0).ToString
                _saleSetting._curShiftno = _ds.Tables(0).Rows(0).Item(1).ToString
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteErroLog(ex.Message)
            errMsg = ex.Message
            Return False
        End Try

    End Function
    'Public Function _getCurrentShiftDayno1(ByRef errMsg As String) As Boolean ' Order Pc
    '    Try
    '        _ds = New DataSet
    '        Dim _sqlPara(1) As SqlParameter
    '        _sqlPara(0) = New SqlParameter("@mode", "curSD1") 'current shift and day no
    '        _sqlPara(1) = New SqlParameter("@str", RegistrationDetails._localPcname)
    '        _ds = dbcls._sqlDataAdapter2("sp_general_query2", _sqlPara)
    '        If _ds.Tables(0).Rows.Count > 0 Then
    '            _saleSetting._curDayno = _ds.Tables(0).Rows(0).Item(0).ToString
    '            _saleSetting._curShiftno = _ds.Tables(0).Rows(0).Item(1).ToString
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        eLog.WriteErroLog(ex.Message)
    '        errMsg = ex.Message
    '        Return False
    '    End Try

    'End Function
    Public Function _getClsShiftDayno(ByRef errMsg As String, ByRef _State As String) As Boolean
        Try
            Dim work_table As New DataTable
            Dim _states As String
            _states = 1 '_State ' 0 means shift open , 1 means shift close per day
            _ds = New DataSet
            Dim _sqlPara(1) As SqlParameter
            _sqlPara(0) = New SqlParameter("@mode", "clsshift") 'current shift and day no
            _sqlPara(1) = New SqlParameter("@str", RegistrationDetails._localPcname)
            _ds = _sqlDataAdapter2("sp_general_query2", _sqlPara)
            If _ds.Tables(0).Rows.Count > 0 Then
                work_table = _ds.Tables(0).Copy
                Dim qry = From dr As DataRow In work_table.AsEnumerable() Where dr.Field(Of String)("states") = _states Select dr 'And dr.Field(Of String)("psc_pcname") = RegistrationDetails._localPcname 
                'Dim linkQuery = From _row In _dSubtable.AsEnumerable() Where _row.Field(Of Integer)("pcam_brandid") = _mainCateids And _row.Field(Of String)("pcam_active") = "A" And _row.Field(Of String)("pcam_layout") = _layouts Select _row
                If qry.Count >= 0 Then
                    For Each rs In qry
                        _saleSetting._clscountshiftno = rs("counts")
                        _saleSetting._clsShiftno = rs("PM_SHIFT_NO")
                        _saleSetting._clsDayno = rs("PM_DAY_NO")
                    Next

                End If
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteErroLog(ex.Message)
            errMsg = ex.Message
            Return False
        End Try

    End Function
    Public Function ImageToStream(ByVal fileName As String) As Byte()
        Dim stream As New MemoryStream()
        Try
            Dim image As New Bitmap(fileName)
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg)
        Catch ex As Exception
            ' alMsg.altmsg.Show(Me, "Product Master Ver 22.01", "ImageToStream", ex.Message, alMsg.img.Images(1))
        End Try

        Return stream.ToArray()
    End Function
    Function _TableValiCheckTrue(ByVal _Table As String) As Boolean
        Try
            _dsCardCheck = New DataSet
            Dim _Sql(1) As SqlParameter
            _Sql(0) = New SqlParameter("@mode", "DC")
            _Sql(1) = New SqlParameter("@tableno", _Table)
            _dsCardCheck = _sqlDataAdapter("sp_check_table", _Sql, "r")
            If _dsCardCheck.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Function _CardValiCheckTrue(ByVal _cardno As String) As Boolean
        Try
            _dsCardCheck = New DataSet
            Dim _Sql(1) As SqlParameter
            _Sql(0) = New SqlParameter("@mode", "C")
            _Sql(1) = New SqlParameter("@tableno", _cardno)
            _dsCardCheck = _sqlDataAdapter("sp_check_table", _Sql, "r")
            If _dsCardCheck.Tables(0).Rows.Count > 0 Then
                _tablNo = _dsCardCheck.Tables(0).Rows(0)("ppt_table_name").ToString
                _fromCardno = _cardno
                Return True
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Function _CardValiCheckReturnTable(ByVal _cardno As String) As String
        Try
            _dsCardCheck = New DataSet
            Dim _Sql(1) As SqlParameter
            _Sql(0) = New SqlParameter("@mode", "C")
            _Sql(1) = New SqlParameter("@tableno", _cardno)
            _dsCardCheck = _sqlDataAdapter("sp_check_table", _Sql, "r")
            If _dsCardCheck.Tables(0).Rows.Count > 0 Then
                _tablNo = _dsCardCheck.Tables(0).Rows(0)("ppt_table_name").ToString
                _fromCardno = _cardno
                Return _tablNo
            Else
                Return 0
            End If
            Return _tablNo
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Function _CardValiCheckCard(ByVal _cardno As String) As Boolean
        Try
            _dsCardCheck = New DataSet
            Dim _Sql(1) As SqlParameter
            _Sql(0) = New SqlParameter("@mode", "C")
            _Sql(1) = New SqlParameter("@tableno", _cardno)
            _dsCardCheck = _sqlDataAdapter("sp_check_table", _Sql, "r")
            If _dsCardCheck.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Function _CardValiCheckFalse(ByVal _cardno As String) As Boolean
        Try
            _dsCardCheck = New DataSet
            Dim _Sql(1) As SqlParameter
            _Sql(0) = New SqlParameter("@mode", "T")
            _Sql(1) = New SqlParameter("@tableno", _cardno)
            _dsCardCheck = _sqlDataAdapter("sp_check_table", _Sql, "r")
            If _dsCardCheck.Tables(0).Rows.Count > 0 Then
                _tablNo = _dsCardCheck.Tables(0).Rows(0)("ppt_table_name").ToString
                _toCardno = _cardno
                Return True
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Function _CheckTableAndReturn(ByRef _retTable As String, ByRef _retCard As String) As String
        Try
            Return _retTable
        Catch ex As Exception
            Return "0"
        End Try
    End Function

    Public Function UpdateTablePOSSettings(ByVal caption As String, ByVal value As String) As Boolean
        Try
            Dim POSsettingsdt As New DataTable
            POSsettingsdt.TableName = "PosSetting"
            If File.Exists(M_Details._appPath & "Layout\POSSettingsdt.xml") Then
                POSsettingsdt.ReadXml(M_Details._appPath & "Layout\POSSettingsdt.xml")
                GoTo L
            Else
                POSsettingsdt.Columns.Add("Caption", GetType(String))
                POSsettingsdt.Columns.Add("Value", GetType(String))
                POSsettingsdt.Columns.Add("AdmID", GetType(Integer)).AutoIncrement = True
            End If
L:          Dim dtrow As Data.EnumerableRowCollection(Of DataRow) = From dtrows As DataRow In POSsettingsdt Where dtrows("Caption") = caption

            If dtrow.Any Then
                dtrow(0)("Value") = value
            Else
                POSsettingsdt.Rows.Add(caption, value)
            End If

            POSsettingsdt.AcceptChanges()
            POSsettingsdt.WriteXml(M_Details._appPath & "Layout\POSSettingsdt.xml", Data.XmlWriteMode.WriteSchema, True)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    'Public Sub _loadBillNumer(ByRef mode As String)
    '    Try
    '        Dim _ds As DataSet
    '        _ds = New DataSet
    '        Dim _Sqlpara1(5) As SqlParameter
    '        _Sqlpara1(0) = New SqlParameter("@mode", mode)
    '        _Sqlpara1(1) = New SqlParameter("@comid", "0")
    '        _Sqlpara1(2) = New SqlParameter("@locid", "0")
    '        _Sqlpara1(3) = New SqlParameter("@pcname", RegistrationDetails._localPcname)
    '        _Sqlpara1(4) = New SqlParameter("@userid", "0")
    '        _Sqlpara1(5) = New SqlParameter("@bdate", "2022/02/28")
    '        Select Case mode
    '            Case "S"
    '                _ds = dbcls._sqlDataAdapter2("sp_billnumber", _Sqlpara1)
    '                If _ds.Tables(0).Rows.Count > 0 Then
    '                    'Dim value As Integer = CInt(Int((generateOTP() * Rnd()) + 1))
    '                    Dim billauto = _ds.Tables(0).Rows(0)(0).ToString
    '                    _saleSetting._curBillnumber = Convert.ToInt16(billauto) & RegistrationDetails._machineId
    '                    _saleSetting._curTransnumber = _ds.Tables(0).Rows(0)(1).ToString
    '                End If
    '            Case "U"
    '                If dbcls._ExecuteNonQuery("sp_billnumber", _Sqlpara1, errMsg) = False Then
    '                    eLog.WriteErroLog("Sp_Billnumber" & errMsg)
    '                End If

    '        End Select


    '    Catch ex As Exception
    '        'altMsg.altmsg.Show(Me, "Warrning !", ex.Message, altMsg.img.Images(0))
    '    End Try
    'End Sub

    Public Sub _loadBillNumer(ByRef mode As String)
        Try
            Dim _ds As DataSet
            _ds = New DataSet
            Dim _Sqlpara1(5) As SqlParameter
            _Sqlpara1(0) = New SqlParameter("@mode", mode)
            _Sqlpara1(1) = New SqlParameter("@comid", "0")
            _Sqlpara1(2) = New SqlParameter("@locid", "0")
            _Sqlpara1(3) = New SqlParameter("@pcname", RegistrationDetails._localPcname)
            _Sqlpara1(4) = New SqlParameter("@userid", "0")
            _Sqlpara1(5) = New SqlParameter("@bdate", "2022/02/28")
            Select Case mode
                Case "S"
                    _ds = _sqlDataAdapter2("sp_billnumber", _Sqlpara1)
                    If _ds.Tables(0).Rows.Count > 0 Then
                        'Dim value As Integer = CInt(Int((generateOTP() * Rnd()) + 1))
                        Dim billauto = _ds.Tables(0).Rows(0)(0).ToString
                        _saleSetting._curBillnumber = Convert.ToInt16(billauto) & RegistrationDetails._machineId
                        _saleSetting._curTransnumber = _ds.Tables(0).Rows(0)(1).ToString
                    End If
                Case "U"
                    If _ExecuteNonQuery("sp_billnumber", _Sqlpara1, errMsg) = False Then
                        WriteErroLog("Sp_Billnumber" & errMsg)
                    End If
                Case "TS" 'Transaction Number
                    _ds = _sqlDataAdapter2("sp_billnumber", _Sqlpara1)
                    If _ds.Tables(0).Rows.Count > 0 Then
                        'Dim value As Integer = CInt(Int((generateOTP() * Rnd()) + 1))
                        'Dim billauto = _ds.Tables(0).Rows(0)(0).ToString
                        ' _saleSetting._curBillnumber = Convert.ToInt16(billauto) & RegistrationDetails._machineId
                        _saleSetting._curTransnumber = _ds.Tables(0).Rows(0)(0).ToString
                    End If
            End Select


        Catch ex As Exception
            'altMsg.altmsg.Show(Me, "Warrning !", ex.Message, altMsg.img.Images(0))
        End Try
    End Sub
    Public Sub _loadBatchNumer(ByRef retBactchNumber As Integer)
        Try

            Dim _ds As DataSet
            _ds = New DataSet
            Dim _Sqlpara1(5) As SqlParameter
            _Sqlpara1(0) = New SqlParameter("@mode", "B")
            _Sqlpara1(1) = New SqlParameter("@comid", "0")
            _Sqlpara1(2) = New SqlParameter("@locid", "0")
            _Sqlpara1(3) = New SqlParameter("@pcname", RegistrationDetails._localPcname)
            _Sqlpara1(4) = New SqlParameter("@userid", "0")
            _Sqlpara1(5) = New SqlParameter("@bdate", "2022/02/28")
            _ds = _sqlDataAdapter2("sp_billnumber", _Sqlpara1)
            If _ds.Tables(0).Rows.Count > 0 Then
                'Dim value As Integer = CInt(Int((generateOTP() * Rnd()) + 1))
                retBactchNumber = _ds.Tables(0).Rows(0)(0).ToString
            Else
                retBactchNumber = 0
            End If
        Catch ex As Exception
            'altMsg.altmsg.Show(Me, "Warrning !", ex.Message, altMsg.img.Images(0))
        End Try
    End Sub
    'Public Function generateOTP() As Integer
    '    Try
    '        Randomize()
    '        ' Generate random value between 1 and 6.
    '        Dim digits = "0123456789"
    '        Dim _Otp As Integer = CInt(Int((digits * Rnd()) + 1))
    '        Return _Otp
    '    Catch ex As Exception
    '        Return "0"
    '    End Try
    'End Function
    'Public Function _getBatchNumber() As Integer
    '    Try
    '        Dim retNumber As String = 0
    '        'counter += 1
    '        'Dim id As String = Guid.NewGuid().ToString("N")
    '        Dim time As String = DateTime.Now.ToString("HHmmss") & RegistrationDetails._machineId
    '        retNumber = DirectCast(time, String)
    '        Return retNumber
    '    Catch ex As Exception
    '        Return "0"
    '    End Try
    'End Function
    Public Sub SettingUpddate(ByRef _statecode As String, ByRef _stvalue As String)
        Try
            Dim _sqlPara(2) As SqlParameter
            _sqlPara(0) = New SqlParameter("@mode", "U")
            _sqlPara(1) = New SqlParameter("@stcode", _statecode)
            ' _sqlPara(2) = New SqlParameter("@stname", "Do You Want to Show Print Dialouge?")
            _sqlPara(2) = New SqlParameter("@stvalue", _stvalue)
            _ExecuteNonQuery("SP_SETTING_OPT", _sqlPara, "ER")

        Catch ex As Exception

        End Try
    End Sub
    Dim printstr As String = ""
    Public Sub _MailSentMsg(ByRef _mailSubject As String, ByRef MAILID As String)
        Try
            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = False
            Smtp_Server.Credentials = New Net.NetworkCredential(MailConfiguration._myMailID, MailConfiguration._myPassword)
            Smtp_Server.Port = 587
            Smtp_Server.EnableSsl = True
            Smtp_Server.Host = MailConfiguration._myHostID
            e_mail = New MailMessage()
            e_mail.From = New MailAddress(MailConfiguration._myMailID)
            'If File.Exists(M_Details._appPath & "Reports\" & _mailFileName) Then
            '    Dim FileName As String = Path.Combine(M_Details._appPath & "Reports\" & _mailFileName)
            '    e_mail.Attachments.Add(New Attachment(FileName))
            'End If
            e_mail.To.Add(MAILID)
            e_mail.CC.Add("mypos.cashier@gmail.com")
            e_mail.Subject = "Payout Delete Report"
            e_mail.IsBodyHtml = False
            e_mail.Body = _mailSubject
            Smtp_Server.Send(e_mail)
            WriteErroLog("Mail Sent Payout Delete Report")
        Catch ex As Exception
            WriteErroLog(ex.Message)
        End Try
    End Sub
    Public Sub _MailSent(ByRef _mailSubject As String, ByRef _mailFileName As String, ByRef MAILID As String)
        Try
            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = False
            Smtp_Server.Credentials = New Net.NetworkCredential(MailConfiguration._myMailID, MailConfiguration._myPassword)
            Smtp_Server.Port = 587
            Smtp_Server.EnableSsl = True
            Smtp_Server.Host = MailConfiguration._myHostID

            e_mail = New MailMessage()
            e_mail.From = New MailAddress(MailConfiguration._myMailID)

            Dim FilePath As String = Path.Combine(M_Details._appPath, "Reports", _mailFileName)
            If File.Exists(FilePath) Then
                e_mail.Attachments.Add(New Attachment(FilePath))
            End If

            e_mail.To.Add(MAILID)
            e_mail.CC.Add("mypos.cashier@gmail.com")
            e_mail.Subject = _mailSubject
            e_mail.IsBodyHtml = False
            e_mail.Body = _mailSubject

            ' Send mail
            Smtp_Server.Send(e_mail)

            ' ✅ Delete file after successful send
            If File.Exists(FilePath) Then
                File.Delete(FilePath)
            End If

            WriteErroLog("Mail Sent " & _mailFileName)
            printstr &= "........................." & vbNewLine
            printstr &= "Mail Sent Successfully " & vbNewLine
            printstr &= _mailFileName & vbNewLine
            printstr &= "........................." & vbNewLine
            printstr &= "." & vbNewLine
            printstr &= "." & vbNewLine
            printstr &= "End Of Report" & vbNewLine
            prn.PrintText(printstr)
            csh._paperCut(True)

        Catch ex As Exception
            WriteErroLog(ex.Message)
            printstr &= "........................." & vbNewLine
            printstr &= "Mail Sent Failed " & vbNewLine
            printstr &= _mailFileName & vbNewLine
            printstr &= "........................." & vbNewLine
            printstr &= "." & vbNewLine
            printstr &= "." & vbNewLine
            printstr &= "End Of Report" & vbNewLine
            prn.PrintText(printstr)
            csh._paperCut(True)
        End Try
    End Sub

    Public Sub _shiftClosePrint(ByRef clsdShiftno As Integer, ByRef print As Boolean, ByRef mail As Boolean)
        Try
            _dsshiftClose = New DataSet
            'If _saleSetting._shiftclose = True Then
            Dim _SqlShitClosePrint(1) As SqlParameter
            _SqlShitClosePrint(0) = New SqlParameter("@mode", "S")
            _SqlShitClosePrint(1) = New SqlParameter("@shiftno", clsdShiftno)
            _dsshiftClose = _sqlDataAdapter2("sp_shiftclose_print", _SqlShitClosePrint)
            _dsshiftClose.Tables(0).TableName = "ShiftClose"
            _dsshiftClose.Tables(1).TableName = "InvoiceHdr"
            _dsshiftClose.Tables(2).TableName = "InvoiceDtl"
            _dsshiftClose.Tables(3).TableName = "MainGroup"
            _dsshiftClose.Tables(4).TableName = "SubGroup"
            _dsshiftClose.Tables(5).TableName = "Payment"
            _dsshiftClose.Tables(6).TableName = "Payouts"
            _dsshiftClose.Tables(7).TableName = "DeleteItem"
            If _dsshiftClose.Tables(0).Rows.Count > 0 Then
                _dsshiftClose.WriteXml(M_Details._appPath & "Reports\ShiftClosePrint.xml", XmlWriteMode.WriteSchema)
                'If printShift(print) = False Then
                '    eLog.WriteErroLog("ShiftClose Print Is Failed")
                'End If
                'If sendMailShift(mail) = False Then
                '    eLog.WriteErroLog("ShiftClose Mail Is Failed")
                'End If
                If _globalSetting._ShiftPrintDos = True Then
                    Dim printCommand As New PrintCommand
                    If printCommand._printShiftClose(clsdShiftno, "S", _dateTime) = True Then
                        If printCommand._printShiftClose(clsdShiftno, "F", _dateTime) = True Then
                            sendMailShiftDosMode(clsdShiftno, True, _dateTime)
                        End If
                    End If
                Else
                    _shiftClosePrint(_saleSetting._curShiftno, True, True)
                End If
            End If

            'End If
        Catch ex As Exception
            WriteErroLog(ex.Message)
        End Try
    End Sub
    Public Sub _DayClosePrint(ByRef DayClose As Integer, ByRef print As Boolean, ByRef mail As Boolean)
        Try

            _dsDayClose = New DataSet
            'If _saleSetting._dayclose = True Then
            Dim _SqlShitClosePrint(1) As SqlParameter
            _SqlShitClosePrint(0) = New SqlParameter("@mode", "D")
            _SqlShitClosePrint(1) = New SqlParameter("@shiftno", DayClose)
            _dsDayClose = _sqlDataAdapter2("sp_shiftclose_print", _SqlShitClosePrint)
            _dsDayClose.Tables(0).TableName = "Dayclose"
            _dsDayClose.Tables(1).TableName = "InvoiceHdr"
            _dsDayClose.Tables(2).TableName = "InvoiceDtl"
            _dsDayClose.Tables(3).TableName = "MainGroup"
            _dsDayClose.Tables(4).TableName = "SubGroup"
            _dsDayClose.Tables(5).TableName = "Payment"
            _dsDayClose.Tables(6).TableName = "Payouts"
            _dsDayClose.Tables(7).TableName = "DeleteItem"
            If _dsDayClose.Tables(0).Rows.Count > 0 Then
                _dsDayClose.WriteXml(M_Details._appPath & "Reports\DayClosePrint.xml", XmlWriteMode.WriteSchema)
                'If printDay(print) = False Then
                '    eLog.WriteErroLog("DayClose Print Is Failed")
                'End If
                'If sendDayMail(mail) = False Then
                '    eLog.WriteErroLog("DayClose Mail Is Failed")
                'End If
                If printCommand._printDayClose("D", DayClose) = True Then

                    If printCommand._printDayClose("F", DayClose) = True Then
                        sendMailDayDosMode(DayClose, True)
                    Else
                        sendMailDayDosMode(DayClose, True)
                    End If
                End If
            End If

            'End If
        Catch ex As Exception
            WriteErroLog(ex.Message)
        End Try
    End Sub
    Public Function printShift(ByVal b As Boolean) As Boolean
        Try
            'Dim filename As String = Now.ToString("ddMMyyyy") & "ShiftClose"

            Dim DtSet As New DataSet
            Dim _rptstaf As New frmReportDesign
            ' DtSet.Tables(0).TableName = "Ram"
            _rptstaf.LoadLayout(M_Details._appPath & "Reports\" & PrintProfile._shiftclosename)
            _rptstaf.DataSource = _dsshiftClose
            Dim pt As New DevExpress.XtraReports.UI.ReportPrintTool(_rptstaf)
            If b = True Then
                '_rptstaf.ExportToPdf(M_Details._appPath & "Reports\" & filename & ".pdf")
                pt.Print(_DotmatrixTemp._PrinterName)

            End If
            Return True
        Catch ex As Exception
            WriteErroLog(ex.Message)
            Return False
        End Try
    End Function
    Public Function sendMailShift(ByVal b As Boolean) As Boolean
        Try
            Dim filename As String = Now.ToString("ddMMyyyy") & "ShiftClose.pdf"

            Dim DtSet As New DataSet
            Dim _rptstaf As New frmReportDesign
            ' DtSet.Tables(0).TableName = "Ram"
            _rptstaf.LoadLayout(M_Details._appPath & "Reports\" & MailProfile._Mailshiftclosename)
            _rptstaf.DataSource = _dsshiftClose
            Dim pt As New DevExpress.XtraReports.UI.ReportPrintTool(_rptstaf)
            If b = True Then
                _rptstaf.ExportToPdf(M_Details._appPath & "Reports\" & filename)
                _MailSent(Now.ToString("ddMMyyyy") & "ShiftClose", filename, MailConfiguration._custMailID1)
                'pt.Print()
                _UploadShiftToPdf(filename)
            End If
            Return True
        Catch ex As Exception
            WriteErroLog(ex.Message)
            Return False
        End Try
    End Function
    Public Function printDay(ByVal b As Boolean) As Boolean
        Try
            'Dim filename As String = Now.ToString("ddMMyyyy") & "ShiftClose"

            Dim DtSet As New DataSet
            Dim _rptstaf As New frmReportDesign
            ' DtSet.Tables(0).TableName = "Ram"
            _rptstaf.LoadLayout(M_Details._appPath & "Reports\" & PrintProfile._dayclosename)
            _rptstaf.DataSource = _dsshiftClose
            Dim pt As New DevExpress.XtraReports.UI.ReportPrintTool(_rptstaf)
            If b = True Then
                '_rptstaf.ExportToPdf(M_Details._appPath & "Reports\" & filename & ".pdf")
                pt.Print(_DotmatrixTemp._PrinterName)

            End If
            Return True
        Catch ex As Exception
            WriteErroLog(ex.Message)
            Return False
        End Try
    End Function
    Public Function printByCutDayTax(ByVal b As Boolean, ByRef _dsCut As DataSet) As Boolean
        Try
            'Dim filename As String = Now.ToString("ddMMyyyy") & "ShiftClose"

            Dim DtSet As New DataSet
            Dim _rptstaf As New frmReportDesign
            ' DtSet.Tables(0).TableName = "Ram"
            _rptstaf.LoadLayout(M_Details._appPath & "Reports\" & PrintProfile._taxCutprintname)
            _rptstaf.DataSource = _dsCut
            Dim pt As New DevExpress.XtraReports.UI.ReportPrintTool(_rptstaf)
            If b = True Then
                '_rptstaf.ExportToPdf(M_Details._appPath & "Reports\" & filename & ".pdf")
                pt.Print(_DotmatrixTemp._PrinterName)

            End If
            Return True
        Catch ex As Exception
            WriteErroLog(ex.Message)
            Return False
        End Try
    End Function
    Public Function sendDayTaxCutMail(ByVal b As Boolean, ByRef _dsCut As DataSet) As Boolean
        Try
            Dim filename As String = Now.ToString("ddMMyyyy") & "DayTaxCutMail.pdf"

            Dim DtSet As New DataSet
            Dim _rptstaf As New frmReportDesign
            ' DtSet.Tables(0).TableName = "Ram"
            _rptstaf.LoadLayout(M_Details._appPath & "Reports\" & MailProfile._MailTaxReportCut)
            _rptstaf.DataSource = _dsCut
            Dim pt As New DevExpress.XtraReports.UI.ReportPrintTool(_rptstaf)
            If b = True Then
                _rptstaf.ExportToPdf(M_Details._appPath & "Reports\" & filename)
                _MailSent(Now.ToString("ddMMyyyy") & "DayTaxCutMail", filename, MailConfiguration._custMailID2)
                'pt.Print()

            End If
            Return True
        Catch ex As Exception
            WriteErroLog(ex.Message)
            Return False
        End Try
    End Function
    Public Function sendDayMail(ByVal b As Boolean) As Boolean
        Try
            Dim filename As String = Now.ToString("ddMMyyyy") & "DayClose.pdf"

            Dim DtSet As New DataSet
            Dim _rptstaf As New frmReportDesign
            ' DtSet.Tables(0).TableName = "Ram"
            _rptstaf.LoadLayout(M_Details._appPath & "Reports\" & MailProfile._Maildayclosename)
            _rptstaf.DataSource = _dsshiftClose
            Dim pt As New DevExpress.XtraReports.UI.ReportPrintTool(_rptstaf)
            If b = True Then
                _rptstaf.ExportToPdf(M_Details._appPath & "Reports\" & filename)
                _MailSent(Now.ToString("ddMMyyyy") & "DayClose", filename, MailConfiguration._custMailID1)

                'pt.Print()

            End If
            Return True
        Catch ex As Exception
            WriteErroLog(ex.Message)
            Return False
        End Try
    End Function

    Public Function _ShiftCloseProcess(ByRef errMsg As String) As Boolean
        Try

            Dim _sqlShitclose(4) As SqlParameter
            _sqlShitclose(0) = New SqlParameter("@mode", "U")
            _sqlShitclose(1) = New SqlParameter("@psc_opbalance", "0")
            _sqlShitclose(2) = New SqlParameter("@psc_pcname", RegistrationDetails._localcompname)
            _sqlShitclose(3) = New SqlParameter("@psc_machineid", RegistrationDetails._machineId)
            _sqlShitclose(4) = New SqlParameter("@psc_machinename", RegistrationDetails._localPcname)
            If _ExecuteNonQuery("sp_createshiftclose", _sqlShitclose, "er") = False Then
                WriteErroLog("Shift Close Problem")
            End If
            If _getClsShiftDayno(errMsg, "clsshift") = False Then
                WriteErroLog(errMsg.ToString)
                'altMsg.altmsg.Show(Me, "Login Check Ver 22.01", erMsg.ToString, "btnsubmit_Click", altMsg.img.Images(1))
                Return False
            End If
            Return True
        Catch ex As Exception
            errMsg = ex.Message
            WriteErroLog(ex.Message)
            Return False
        End Try
    End Function
    Public Function _DayCloseProcess(ByRef erMsg As String) As Boolean
        Try
            Dim _sqlShitclose(2) As SqlParameter
            _sqlShitclose(0) = New SqlParameter("@mode", "U")
            _sqlShitclose(1) = New SqlParameter("@psd_opbalance", "0")
            _sqlShitclose(2) = New SqlParameter("@psd_pcname", RegistrationDetails._localPcname)
            If _ExecuteNonQuery("sp_createdayclose", _sqlShitclose, "er") = True Then
                WriteErroLog("Day Close Updated :")
                '_saleSetting._dayclose = True
                Dim _dsGetDayNo As New DataSet
                Dim _sqlGetDayno(2) As SqlParameter
                _sqlGetDayno(0) = New SqlParameter("@mode", "G")
                _sqlGetDayno(1) = New SqlParameter("@psd_opbalance", "0")
                _sqlGetDayno(2) = New SqlParameter("@psd_pcname", RegistrationDetails._localPcname)
                _dsGetDayNo = _sqlDataAdapter2("sp_createdayclose", _sqlGetDayno)
                If _dsGetDayNo.Tables(0).Rows.Count > 0 Then
                    WriteErroLog("Print Design Day Open:")
                    Dim dayno As String = _dsGetDayNo.Tables(0).Rows(0)(0)
                    '_DayClosePrint(_dsGetDayNo.Tables(0).Rows(0)(0), True, True) 'window printer
                    If String.IsNullOrEmpty(dayno) OrElse dayno = "" Then
                        Dim frmsg As New frmMsgBoxOk
                        properClass.R_Msgstring = "Dayno Is Null for Current Date"
                        frmsg.ShowDialog()
                        Return False
                    End If
                    If printCommand._printDayClose("D", dayno) = True Then
                        If printCommand._printDayClose("F", dayno) = True Then
                            sendMailDayDosMode(dayno, True)
                        Else
                            sendMailDayDosMode(dayno, True)
                        End If
                    End If
                    'Me.Close()
                    'Application.Exit()
                    Return True
                End If
            Else
                ' _saleSetting._dayclose = False
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteErroLog(errMsg.ToString)
            Return False
        End Try
    End Function

    Dim csh As New RawPrinter
    Dim prn As New PrintCommand
    Public Function sendMailShiftDosMode(ByRef ShiftNo As Integer, ByVal b As Boolean, ByRef _dateTime As DateTime) As Boolean
        Try
            Dim filename As String = ShiftNo & "_" & _dateTime.ToString("ddMMyyyy") & "PrintShiftClose.pdf"
            If b = True Then
                _MailSent(M_Details._shopName & _dateTime.ToString("ddMMyyyy") & "PrintShiftClose", filename, MailConfiguration._custMailID1)
                _UploadShiftToPdf(filename)
            End If
            Return True
        Catch ex As Exception
            WriteErroLog(ex.Message)
            Return False
        End Try
    End Function
    Public Function sendMailDayDosMode(ByRef Dayno As Integer, ByVal b As Boolean) As Boolean
        Try
            Dim filename As String = Dayno & "_" & Now.ToString("ddMMyyyy") & "printDayClose.pdf"
            If b = True Then
                _MailSent(M_Details._shopName & Now.ToString("ddMMyyyy") & "DayClose", filename, MailConfiguration._custMailID1)
            End If
            Return True
        Catch ex As Exception
            WriteErroLog(ex.Message)
            Return False
        End Try
    End Function
    Public Function ClosebyMachineId() As Boolean
        Try
            Dim _Sql(2) As SqlParameter
            _Sql(0) = New SqlParameter("@pms_mid", RegistrationDetails._machineId)
            _Sql(1) = New SqlParameter("@pms_shiftno", _saleSetting._curShiftno)
            _Sql(2) = New SqlParameter("@pms_dayno", _saleSetting._curDayno)
            If _ExecuteNonQuery("sp_machineclose", _Sql, errMsg) = True Then
                Return True
            Else
                WriteErroLog(errMsg)
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteErroLog(ex.Message)
            Return False
        End Try
    End Function
    'Public Sub _txtReader()
    '    Try
    '        Using MyReader As New Microsoft.VisualBasic.
    '                  FileIO.TextFieldParser(M_Details._appPath & "Reports\OrderSystem.txt")
    '            MyReader.TextFieldType = FileIO.FieldType.Delimited
    '            MyReader.SetDelimiters(",")
    '            Dim currentRow As String()
    '            While Not MyReader.EndOfData
    '                Try
    '                    currentRow = MyReader.ReadFields()
    '                    M_Details._localOrderId = currentRow(0)
    '                    RegistrationDetails._localPcname = currentRow(1)
    '                    'Dim currentField As String
    '                    'For Each currentField In currentRow
    '                    '    MsgBox(currentField)
    '                    '    M_Details._localOrderId = currentField(0)
    '                    '    RegistrationDetails._localPcname = currentField(1)
    '                    'Next
    '                Catch ex As Microsoft.VisualBasic.
    '                            FileIO.MalformedLineException
    '                    MsgBox("Line " & ex.Message &
    '                    "is not valid and will be skipped.")
    '                End Try
    '            End While
    '        End Using
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Public Sub _JsonSendData(ByRef _val As String, ByVal _result As String)
        Try
            Dim webRequest As WebRequest = webRequest.Create(_val)
            Dim webResponse As WebResponse = webRequest.GetResponse
            Dim webStream As Stream = webResponse.GetResponseStream
            Dim reader As New StreamReader(webStream)
            Dim WebValue As String = reader.ReadToEnd
            Dim trimWebValue As String = WebValue.Replace(vbCrLf, "")
            If LTrim(trimWebValue) = "1" Then
                _result = "Updated"
            ElseIf LTrim(trimWebValue) = "2" Then
                _result = "Saved"
            ElseIf LTrim(trimWebValue) = "3" Then
                _result = "Nothing"
            End If
        Catch ex As Exception
            WriteErroLog(ex.Message)
        End Try
    End Sub
    Public Sub _UploadShiftToPdf(ByRef _pdfFileName As String)
        Try
            'Dim JSONresult As String
            '_JsonSend(URL_Link & "myqr/itemmapping.php?AjaxRequest=102&restid=" & Rest_Id & "&json=" & JSONresult)
            'MessageBox.Show(JSONresult)
            Dim Rest_id As String
            Rest_id = M_Details._WebId
            Dim ftpUserID As String = "myposaccts@myposqr.com"  '"sales@web.dinainas.com"
            Dim ftpPassword As String = "Ruthram@1986"
            Dim ftplink As String = "ftp://ftp.myposqr.com/on/sales/" '"ftp://ftp.dinainas.com/" 
            Dim url = ftplink & Rest_id & "/" + _pdfFileName
            Dim _file = M_Details._appPath & "Reports\" & _pdfFileName
            If UPLOADFFILEFTP(url, ftpUserID, ftpPassword, _file) = True Then
                WriteErroLog("ShiftSales Uploaded")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function UPLOADFFILEFTP(ByVal URL As String, ByVal USER As String, ByVal PWD As String, ByRef locpath As String) As Boolean
        Dim ftpRequest As FtpWebRequest

        'Settings required to establish a connection with the server
        Dim ff As FileInfo = New FileInfo(locpath)
        ftpRequest = CType(FtpWebRequest.Create(New Uri(URL)), FtpWebRequest)
        ftpRequest.Credentials = New NetworkCredential(USER, PWD)
        ftpRequest.KeepAlive = False
        ftpRequest.Timeout = 5000
        ftpRequest.Method = WebRequestMethods.Ftp.UploadFile
        ftpRequest.UseBinary = True
        ftpRequest.ContentLength = ff.Length
        Dim bufferlength As Integer = 60048
        Dim buff(bufferlength - 1) As Byte
        Dim _filestream As System.IO.FileStream = ff.OpenRead()
        Try

            Dim _Stream As System.IO.Stream = ftpRequest.GetRequestStream()
            Dim contentLen As Integer = _filestream.Read(buff, 0, bufferlength)
            Do While contentLen <> 0
                ' Write Content from the file stream to the FTP Upload Stream
                _Stream.Write(buff, 0, contentLen)
                contentLen = _filestream.Read(buff, 0, bufferlength)
            Loop

            ' Close the file stream and the Request Stream
            WriteErroLog("Sales Uploaded On " & locpath)
            _Stream.Close()
            _Stream.Dispose()
            _filestream.Close()
            _filestream.Dispose()
            Return True
        Catch ex As Exception
            WriteErroLog(ex.Message)
            Return False
        End Try
    End Function
    Public Function GetDriveSerialNumber() As String
        Dim DriveSerial As Integer
        'Create a FileSystemObject object
        Dim fso As Object = CreateObject("Scripting.FileSystemObject")
        Dim Drv As Object = fso.GetDrive(fso.GetDriveName(Application.StartupPath))
        With Drv
            If .IsReady Then
                DriveSerial = .SerialNumber
            Else    '"Drive Not Ready!"
                DriveSerial = -1
            End If
        End With
        Return DriveSerial.ToString("X2")
    End Function
    Public Function SplitString(ByRef slStrng As String) As String()

        Dim str As String = ""
        Dim strArr() As String
        'Dim count As Integer
        str = slStrng
        strArr = str.Split("-")
        Return strArr
        'For count = 0 To strArr.Length - 1
        '    MsgBox(strArr(count))
        'Next

    End Function
End Module