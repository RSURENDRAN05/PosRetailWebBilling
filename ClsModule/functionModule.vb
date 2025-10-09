Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.DataTable
Imports System.Text.RegularExpressions
Imports Microsoft.Win32
Imports System.Net
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.Xml
Imports System.Text
Imports System.Security.Cryptography
Imports DevExpress.XtraEditors
Imports System.Globalization

Module functionModule
    Public isTrial As Boolean
    Public G_SalID As Integer = 0
    Public ini As IniFile ' Don't initialize here - initialize later
    Public PanelScreenWith As Integer = 0
    Public PanelScreenHeight As Integer = 0
    Public G_GRNNo As String = String.Empty
    Public E_EmployeeId As Integer = 0
    Dim errMsg As String = ""
    Public _PosSettingDt As DataTable
    Public _posSettingDs As DataSet
    Public _PRINTDS, _SETTINGS As New DataSet
    Public _posPrintHeadDesign As DataSet

    Public Structure M_Details
        Public Shared SoftwareVersion As String = "Cli-VER25.0.0.30 R5 061025" '"Web" '"Ser" '"Cli"
        Public Shared AppPathDirectory As String = AppDomain.CurrentDomain.BaseDirectory
        Public Shared _appPath As String = Application.StartupPath
        Public Shared LinkAjaxRequest As String = "" ' Initialize empty, set later
        Public Shared LinkAjaxRequestCheque As String = ""
        Public Shared LinkAjaxRequestSyncLocalCloud As String = ""
        Public Shared licenceActive As String = ""
        Public Shared licenceServerCleint As String = ""
        Public Shared CompanyId As Integer = 0
        Public Shared LocationId As Integer = 0
        Public Shared CompanyName As String = "DefalutCompany"
        Public Shared LocationName As String = "DefalutLocation"
        Public Shared PMID As Integer = 0
        Shared _logPath As String = Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("POSAPP").GetValue("LogPath")
        Shared _Conn As String = Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("POSAPP").GetValue("Conn")
        Shared _shopName As String = Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("POSAPP").GetValue("Rest_Name")
        Shared _activationCode As String = Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("POSAPP").GetValue("ActivationCode")
        Shared _registerDate As String = Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("POSAPP").GetValue("RegisterDate")
        Shared _severclient As String = Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("POSAPP").GetValue("ServerClient")
        Shared _WebId As String = Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("POSAPP").GetValue("WebId")
        Shared _dataBasName As String = Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("POSAPP").GetValue("DataBase")
        Public Shared _dbStatus As String = Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("POSAPP").GetValue("DB")
    End Structure
    Public Structure RegistrationDetails
        Public Shared _machineId As String = ""
        Public Shared _localPcname As String = ""
        Public Shared _serverClient As String = ""
        Public Shared _localcompname As String
        Public Shared _active As String = ""
        Public Shared _MenuActive As Boolean = False
        Public Shared _registerdCounterName As String
        Public Shared _paymentPopupActive As Boolean = False
    End Structure
    Public Structure _saleSetting
        Public Shared _modeSales As String = ""
        Public Shared _modeDefalueSales As String = "XA"
        Public Shared _modeOfCardNo As String = "1000"
        Public Shared _curBillnumber As Integer
        Public Shared _batchNumber As Integer = 0
        Public Shared _curTransnumber As Integer
        Public Shared _curDayno As Integer
        Public Shared _curShiftno As Integer
        Public Shared _clsDayno As Integer
        Public Shared _clsShiftno As Integer
        Public Shared _clscountshiftno As Integer
        Public Shared _BusinessDate As String
        Public Shared _shiftclose As Boolean
        Public Shared _dayclose As Boolean
        Public Shared G_TableNo As String = 0
        Public Shared frmdualClose As Integer = 0
    End Structure
    Public Structure LogFileText
        Public Shared _richTextBox As New RichTextBox
    End Structure
    Public Structure ButtonStyleWH
        Public Shared MAINH As String = "50"
        Public Shared MAINW As String = "50"
        Public Shared SUBH As String = "50"
        Public Shared SUBW As String = "50"
        Public Shared ITEMH As String = "50"
        Public Shared ITEMW As String = "50"
        Public Shared MAINLOAD As String = "1"
        Public Shared SUBLOAD As String = "1"
        Public Shared ITEMLOAD As String = "1"
        Public Shared MAINCOL As String = "5"
        Public Shared SUBMENUCOL As String = "1"
        Public Shared ITEMMENUCOL As String = "5"
        Public Shared ITEMSPACING As String = "5"
        Public Shared ITEMMARGINLEFT As String = "5"
        Public Shared ITEMMARGINRIGHT As String = "5"
    End Structure
    Public Structure CustomerDisplaySettings

        Public Shared PortName As String = String.Empty
        Public Shared Parity As String = String.Empty
        Public Shared StopBits As String = String.Empty
        Public Shared DataBits As String = String.Empty
        Public Shared BaudRate As String = String.Empty
        Public Shared Startup As Integer = 0 '0 is automatic, 1 is manual, 2 is Disable
        Public Shared DefaultDisplay As String = String.Empty

    End Structure
    Public Structure PrintProfile
        Shared _salesFilename As String = ""
        Shared _purchaseFilename As String = ""
        Shared _inventoryprintName As String = ""
        Shared _salesGuestprintName As String = ""
        Shared _shiftclosename As String = ""
        Shared _dayclosename As String = ""
        Shared _taxprintname As String = ""
        Shared _taxCutprintname As String = ""
        Shared _payouts As String = ""
        Shared _dsPrintProfile As DataSet
        Shared _Mulfile(,) As String
    End Structure
    Public Structure MailProfile
        Shared _MailsalesFilename As String = ""
        Shared _MailsalesGuestprintName As String = ""
        Shared _Mailshiftclosename As String = ""
        Shared _Maildayclosename As String = ""
        Shared _Mailtaxprintname As String = ""
        Shared _MailTaxReportCut As String = ""
        Shared _dsMailPrintProfile As DataSet
    End Structure
    Public Structure MailConfiguration
        Shared _myMailID As String = ""
        Shared _myPassword As String = ""
        Shared _myHostID As String = ""
        Shared _custMailID1 As String = ""
        Shared _custMailID2 As String = ""
        Shared _custPhone As String = ""
        Shared _custAddress As String = ""
    End Structure
    Public Structure SystemSettings
        Shared _sysCode As String = ""
        Shared _pcname As String = ""
        Shared _filename As String = ""
        Shared _activation As String = ""
        Shared _dsSystemSettings As DataSet
        Shared _SalesOpenCount As Integer = 0
        Shared _CardSystem As Boolean = False
        Shared _WebPageDualScreen As Boolean = False
    End Structure
    Public Structure _printHeaderDesign
        Public Shared _shopname As String = String.Empty
        Public Shared _address As String = String.Empty
        Public Shared _PrinterName As String = String.Empty
        Public Shared _logoPath As String = _logoPath
        Shared _AFTPRINT As String = String.Empty
        Public Shared _logoState As String = String.Empty
        Public Shared _emptyrow As String = String.Empty
        Public Shared _bottomMsg As String = String.Empty
    End Structure
    Public Structure _DotmatrixTemp
        Public Shared shopadress As String = String.Empty
        Public Shared PrintType As String = String.Empty
        Public Shared _DAILYSALE As String = String.Empty
        Public Shared _PCNAME As String = Environment.MachineName
        Public Shared _PrinterName As String = String.Empty
        Public Shared _Port As String = ""
    End Structure
    Public Structure _decryptCode
        Shared _deCodeServer As String
        Shared _deCodeClient As String
        Shared _deCodeMainHD As String
        Shared _deCodeClienHd As String
        Shared _deCodeActivation As String
        Shared _deCodeExpireDays As Integer
    End Structure

    ' Initialize the module safely
    Public Sub InitializeModule()
        Try
            If ini Is Nothing Then
                ini = New IniFile(M_Details._appPath & "\Settings\" & "Settings.ini")
                ' Now safely read the settings
                M_Details.LinkAjaxRequest = ini.ReadValue("Profile", "UrlLink")
                If String.IsNullOrEmpty(M_Details.LinkAjaxRequest) Then
                    M_Details.LinkAjaxRequest = "http://localhost/api/" ' Default fallback
                End If
            End If
        Catch ex As Exception
            ' Handle initialization errors gracefully
            System.Diagnostics.Debug.WriteLine("Module initialization error: " & ex.Message)
            ' Set default values
            M_Details.LinkAjaxRequest = "http://localhost/api/"
        End Try
    End Sub
    Public Structure _FunctionKeyBoardModule
        Public Shared gs_keyboardValueInteger As Integer = 0
        Public Shared gs_keyboardValueDecimal As Decimal = 0.0
    End Structure
    Public Structure _companyInfo
        Public Shared UserId As Integer = 0
        Public Shared UserName As String = ""
        Public Shared ComId As Integer = 0
        Public Shared LocId As Integer = 0
        Public Shared CompanyName As String = ""
        Public Shared LocationName As String = ""
        Public Shared UserRole As String = ""
        Public Shared UserRoleId As String = "1"
        Public Shared CompanyPMID As Integer = 0
    End Structure
    Public Structure _JsonData
        Public Shared UserTable As New DataTable
        Public Shared UserPolicyTable As New DataTable
        Public Shared CompanyTable As New DataTable
        Public Shared CompanyLocationTable As New DataTable
        Public Shared LocationTable As New DataTable
        Public Shared CurrencyTable As New DataTable
        Public Shared TaxTable As New DataTable
        Public Shared MainGroupTable As New DataTable
        Public Shared CategoryTable As New DataTable
        Public Shared CustomerTable As New DataTable
        Public Shared BranchTable As New DataTable
        Public Shared UnitMasterTable As New DataTable
        Public Shared SupplierTable As New DataTable
        Public Shared ItemMasterTable As New DataTable
        Public Shared ItemTouchMasterTable As New DataTable
        Public Shared PurchaseViewTable As New DataTable
        Public Shared BankListTable As New DataTable
        Public Shared LedgerListTable As New DataTable
        Public Shared PaymodeList As New DataTable
        Public Shared AccountGroup As New DataTable
        Public Shared AccountParent As New DataTable
        Public Shared AccountLedger As New DataTable
        Public Shared MonthOfSalary As New DataTable
        Public Shared PosSettingsTable As New DataTable
        Public Shared SalesManCommissionTable As New DataTable
        Public Shared SalesManDataTable As New DataTable
        Public Shared PaymentTermTable As New DataTable
        Public Shared ButtonStyleTable As New DataTable
        Public Shared MultiPriceTable As New DataTable
        Public Shared MainGroupPolicyTable As New DataTable
        Public Shared FingerPrintDataTable As New DataTable
        Public Shared PackageDataTable As New DataTable
    End Structure
    Public Structure _discount
        Public Shared DiscountPer As Boolean = False
        Public Shared discountValue As Double = 0
    End Structure
    Public Structure _PaymentDtl
        Public Shared paymentModeSelection As String = ""
        Public Shared paymentMode As String = ""
        Public Shared paymentId As String = ""
        Public Shared paymentAmount As Decimal = 0
    End Structure
    Public Function CreateTablePaymore() As DataTable
        Try
            Dim PayMoreTable = New DataTable
            PayMoreTable.Columns.Add("pmode_id", GetType(Integer)) '0
            PayMoreTable.Columns.Add("pmode_name", GetType(String)) '0
            PayMoreTable.Columns.Add("pmode_type", GetType(String)) '0
            PayMoreTable.Columns.Add("pmode_amount", GetType(Decimal))
            Return PayMoreTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Structure saveMode
        Shared _newMode As String = "New"
        Shared _saveMode As String = "Save"
        Shared _updateMode As String = "Update"
        Shared _noneMode As String = "None"
    End Structure
    Public Enum Upd_save
        _save = 1
        _update = 0
        _reset = 3
    End Enum
    Public Sub WriteAuditLog(ByRef userId As Integer, ByRef AuditMenuName As String, ByRef AuditLogMsg As String)
        Try
            Dim _SqlLog(3) As SqlParameter
            _SqlLog(0) = New SqlParameter("mode", "I")
            _SqlLog(1) = New SqlParameter("pal_userid", userId)
            _SqlLog(2) = New SqlParameter("pal_menu", AuditMenuName)
            _SqlLog(3) = New SqlParameter("pal_logname", AuditLogMsg)
            If _ExecuteNonQuery("sp_auditlog_save", _SqlLog, errMsg) = False Then
                WriteErroLog("sp_auditlog_save " & errMsg)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Dim filePath As String = M_Details._appPath & "\LocalDataBase\"
    Public Function ReadCustomerDisplayPole(ByRef ErrorMsg As String) As Boolean
        Try
            Dim dsCusPole As New DataSet

            If File.Exists(M_Details._appPath & "\Layout\CustomerCOMSettings.xml") Then
                dsCusPole.ReadXml(M_Details._appPath & "\Layout\CustomerCOMSettings.xml")
                CustomerDisplaySettings.BaudRate = dsCusPole.Tables(0).Rows(0)("BaudRate")
                CustomerDisplaySettings.DataBits = dsCusPole.Tables(0).Rows(0)("DataBits")
                CustomerDisplaySettings.DefaultDisplay = dsCusPole.Tables(0).Rows(0)("DefaultDisplay")
                CustomerDisplaySettings.Parity = dsCusPole.Tables(0).Rows(0)("Parity")
                CustomerDisplaySettings.PortName = dsCusPole.Tables(0).Rows(0)("PortName")
                CustomerDisplaySettings.Startup = dsCusPole.Tables(0).Rows(0)("Startup")
                CustomerDisplaySettings.StopBits = dsCusPole.Tables(0).Rows(0)("StopBits")
            Else
                File.Create(M_Details._appPath & "\Layout\CustomerCOMSettings.xml")
            End If

            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return False
        End Try
    End Function
    Public Function getUserInfo() As Boolean
        Try

            Dim Path As String = filePath & "UserTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                ' Internet available - fetch from server
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=1")
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.UserTable = Userparsejson("Data").ToObject(Of DataTable)()

                If _JsonData.UserTable.Rows.Count > 0 Then
                    ' Save DataTable to XML file
                    _JsonData.UserTable.TableName = "UserTable"
                    _JsonData.UserTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
                Return True
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.UserTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.UserTable.ReadXml(Path)
                    _JsonData.UserTable.TableName = "UserTable"

                    If _JsonData.UserTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function


    Public Function getUserPolicyInfo(ByRef user_id As String) As Boolean
        Try
            Dim Path As String = filePath & "UserPolicyTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "GroupPolicyRequest=6&user_id=" & user_id)
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.UserPolicyTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.UserPolicyTable.Rows.Count > 0 Then
                    _JsonData.UserPolicyTable.TableName = "UserPolicyTable"
                    _JsonData.UserPolicyTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.UserPolicyTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.UserPolicyTable.ReadXml(Path)
                    _JsonData.UserPolicyTable.TableName = "UserPolicyTable"

                    If _JsonData.UserPolicyTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getFingerPrintData() As Boolean
        Try
            Dim Path As String = filePath & "FingerPrintDataTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AttRequest=3&EmpId=0")
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.FingerPrintDataTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.FingerPrintDataTable.Rows.Count > 0 Then
                    _JsonData.FingerPrintDataTable.TableName = "FingerPrintDataTable"
                    _JsonData.FingerPrintDataTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.FingerPrintDataTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.FingerPrintDataTable.ReadXml(Path)
                    _JsonData.FingerPrintDataTable.TableName = "FingerPrintDataTable"

                    If _JsonData.FingerPrintDataTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getPackageData() As Boolean
        Try
            Dim Path As String = filePath & "PackageDataTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "MenuRequest=13&PackageId=0")
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.PackageDataTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.PackageDataTable.Rows.Count > 0 Then
                    _JsonData.PackageDataTable.TableName = "PackageDataTable"
                    _JsonData.PackageDataTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.PackageDataTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.PackageDataTable.ReadXml(Path)
                    _JsonData.PackageDataTable.TableName = "PackageDataTable"

                    If _JsonData.PackageDataTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    'Public Function StoreFmdFile() As Boolean
    '    Try
    '        getFingerPrintData()
    '        Dim _fingerTemplates As New Dictionary(Of Int16, Fmd)

    '        If _JsonData.FingerPrintDataTable.Rows.Count > 0 Then
    '            For Each row In _JsonData.FingerPrintDataTable.Rows
    '                Dim fingerPosition As Int16 = Convert.ToInt16(row("finger_name"))
    '                Dim base64 As String = row("finger_template")
    '                Dim bytes As Byte() = Convert.FromBase64String(base64)
    '                Dim templateXml As String = Encoding.UTF8.GetString(Convert.FromBase64String(base64))
    '                'Dim fmd As Fmd = Fmd.DeserializeXml(Encoding.UTF8.GetBytes(templateXml))
    '                ' Convert stored XML bytes to Fmd object
    '                Dim fmd As Fmd = fmd.DeserializeXml(templateXml)
    '                _fingerTemplates.Add(fingerPosition, fmd)
    '            Next
    '        End If
    '        FingerPrintReader.Fmds = _fingerTemplates

    '        Return True
    '    Catch ex As Exception
    '        ' Optionally log ex.Message
    '        Return False
    '    End Try
    'End Function
    'Public Function StoreFmdFile(empIds As Integer) As Boolean
    '    Try
    '        ' Clear existing Fmds
    '        ' FingerPrintReader.Fmds.Clear()
    '        getFingerPrintData()
    '        Dim dt As DataTable = Nothing

    '        ' Filter rows by emp_id
    '        Dim filteredRows = _JsonData.FingerPrintDataTable.AsEnumerable() _
    '                           .Where(Function(r) Convert.ToInt32(r("emp_id")) = empIds)

    '        If filteredRows.Any() Then
    '            ' Copy the filtered rows to a new DataTable
    '            dt = filteredRows.CopyToDataTable()
    '        End If
    '        ' Loop through each row in your fingerprint data table
    '        If dt.Rows.Count > 0 Then
    '            For Each row As DataRow In dt.Rows
    '                Dim fingerPosition As Int16 = Convert.ToInt16(row("finger_name"))
    '                Dim empId As Integer = Convert.ToInt32(row("emp_id"))
    '                Dim empName As String = If(row.Table.Columns.Contains("emp_printname"), row("emp_printname").ToString(), "Unknown")
    '                Dim fingerType As String = If(row.Table.Columns.Contains("fingertype"), row("fingertype").ToString(), "")
    '                Dim base64Template As String = row("finger_template").ToString()

    '                ' Convert XML/base64 template into Fmd object
    '                Dim templateXml As String = Encoding.UTF8.GetString(Convert.FromBase64String(base64Template))

    '                Dim fmd As Fmd = fmd.DeserializeXml(templateXml)

    '                ' Create EmployeeFinger object
    '                Dim empFinger As New EmployeeFinger() With {
    '                    .EmpId = empId,
    '                    .EmpName = empName,
    '                    .FingerName = fingerPosition.ToString(),
    '                    .FingerTemplate = fmd
    '                }

    '                ' Add to dictionary
    '                FingerPrintReader.Fmds(fingerPosition) = empFinger
    '            Next
    '        End If

    '        Return True
    '    Catch ex As Exception
    '        ' Optional: log ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function getSalesManCommissionInfo() As Boolean
        Try
            Dim Path As String = filePath & "SalesManCommissionTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                _JsonData.SalesManCommissionTable.TableName = "SalesManCommissionTable"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=7")
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.SalesManCommissionTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.SalesManCommissionTable.Rows.Count > 0 Then
                    _JsonData.SalesManCommissionTable.TableName = "SalesManCommissionTable"
                    _JsonData.SalesManCommissionTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.SalesManCommissionTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.SalesManCommissionTable.ReadXml(Path)
                    _JsonData.SalesManCommissionTable.TableName = "SalesManCommissionTable"
                    If _JsonData.SalesManCommissionTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function GetMultiplePricesFromServer() As Boolean
        Try
            Dim Path As String = filePath & "MultiPriceTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                _JsonData.MultiPriceTable.TableName = "MultiPriceTable"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim url As String = M_Details.LinkAjaxRequest & "AjaxRequest=66"
                Dim json As String = New System.Net.WebClient().DownloadString(url)
                Dim parseJson As JObject = JObject.Parse(json)
                _JsonData.MultiPriceTable = parseJson("Data").ToObject(Of DataTable)()
                If _JsonData.MultiPriceTable.Rows.Count > 0 Then
                    _JsonData.MultiPriceTable.TableName = "MultiPriceTable"
                    _JsonData.MultiPriceTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.MultiPriceTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.MultiPriceTable.ReadXml(Path)
                    _JsonData.MultiPriceTable.TableName = "MultiPriceTable"
                    If _JsonData.MultiPriceTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function getPosSettingsInfo() As Boolean
        Try
            Dim Path As String = filePath & "PosSettingsTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                _JsonData.PosSettingsTable.TableName = "PosSettingsTable"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "GroupPolicyRequest=8&operation=SELECT")
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.PosSettingsTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.PosSettingsTable.Rows.Count > 0 Then
                    _JsonData.PosSettingsTable.TableName = "PosSettingsTable"
                    _JsonData.PosSettingsTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.PosSettingsTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.PosSettingsTable.ReadXml(Path)
                    _JsonData.PosSettingsTable.TableName = "PosSettingsTable"
                    If _JsonData.PosSettingsTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getComapnyInfo() As Boolean
        Try
            Dim Path As String = filePath & "CompanyTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                _JsonData.CompanyTable.TableName = "CompanyTable"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=51")
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.CompanyTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.CompanyTable.Rows.Count > 0 Then
                    _JsonData.CompanyTable.TableName = "CompanyTable"
                    _JsonData.CompanyTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.CompanyTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.CompanyTable.ReadXml(Path)
                    _JsonData.CompanyTable.TableName = "CompanyTable"
                    If _JsonData.CompanyTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getLocationInfo() As Boolean
        Try
            Dim Path As String = filePath & "LocationTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                _JsonData.LocationTable.TableName = "LocationTable"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=3")
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.LocationTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.LocationTable.Rows.Count > 0 Then
                    _JsonData.LocationTable.TableName = "LocationTable"
                    _JsonData.LocationTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.LocationTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.LocationTable.ReadXml(Path)
                    _JsonData.LocationTable.TableName = "LocationTable"
                    If _JsonData.LocationTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getComapnyLocationInfo() As Boolean
        Try
            Dim Path As String = filePath & "CompanyLocationTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                _JsonData.CompanyLocationTable.TableName = "CompanyLocationTable"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=2")
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.CompanyLocationTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.CompanyLocationTable.Rows.Count > 0 Then
                    _JsonData.CompanyLocationTable.TableName = "CompanyLocationTable"
                    _JsonData.CompanyLocationTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.CompanyLocationTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.CompanyLocationTable.ReadXml(Path)
                    _JsonData.CompanyLocationTable.TableName = "CompanyLocationTable"
                    If _JsonData.CompanyLocationTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getCurrencyTable() As Boolean
        Try
            _JsonData.CurrencyTable.TableName = "CurrencyTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=10")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.CurrencyTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.CurrencyTable.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getPaymentTermTable() As Boolean
        Try
            Dim Path As String = filePath & "PaymentTermTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                _JsonData.PaymentTermTable.TableName = "PaymentTermTable"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=26&groupid=013")
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.PaymentTermTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.PaymentTermTable.Rows.Count > 0 Then
                    _JsonData.PaymentTermTable.TableName = "PaymentTermTable"
                    _JsonData.PaymentTermTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.PaymentTermTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.PaymentTermTable.ReadXml(Path)
                    _JsonData.PaymentTermTable.TableName = "PaymentTermTable"
                    If _JsonData.PaymentTermTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getButtonStyleTable() As Boolean
        Try
            Dim Path As String = filePath & "ButtonStyleTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                _JsonData.ButtonStyleTable.TableName = "ButtonStyleTable"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=26&groupid=014")
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.ButtonStyleTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.ButtonStyleTable.Rows.Count > 0 Then
                    _JsonData.ButtonStyleTable.TableName = "ButtonStyleTable"
                    _JsonData.ButtonStyleTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.ButtonStyleTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.ButtonStyleTable.ReadXml(Path)
                    _JsonData.ButtonStyleTable.TableName = "ButtonStyleTable"
                    If _JsonData.ButtonStyleTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getTaxMaster() As Boolean
        Try
            _JsonData.TaxTable.TableName = "TaxTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=12")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.TaxTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.TaxTable.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getItemMaster() As Boolean
        Try
            Dim Path As String = filePath & "ItemMasterTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                _JsonData.ItemMasterTable.TableName = "ItemMasterTable"
                Dim _dt As New DataTable
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=42")
                Dim Userparsejson As JObject = JObject.Parse(json)

                'Dim dtrows As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _dt Where dtrow("COMID") = _companyInfo.ComId And dtrow("LOCID") = _companyInfo.LocId
                'If dtrows.Any Then
                '    _JsonData.ItemMasterTable = dtrows.CopyToDataTable
                '    _JsonData.ItemMasterTable.TableName = "ItemMasterTable"
                '    _JsonData.ItemMasterTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                '    Return True
                'End If

                _JsonData.ItemMasterTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.ItemMasterTable.Rows.Count > 0 Then
                    _JsonData.ItemMasterTable.TableName = "ItemMasterTable"
                    _JsonData.ItemMasterTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.ItemMasterTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.ItemMasterTable.ReadXml(Path)
                    _JsonData.ItemMasterTable.TableName = "ItemMasterTable"
                    If _JsonData.ItemMasterTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Function getTouchItemMaster() As Boolean
        Try
            Dim Path As String = filePath & "ItemTouchMasterTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                Dim _dt As New DataTable
                _JsonData.ItemTouchMasterTable.TableName = "ItemTouchMasterTable"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=27")
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.ItemTouchMasterTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.ItemTouchMasterTable.Rows.Count > 0 Then
                    _JsonData.ItemTouchMasterTable.TableName = "ItemTouchMasterTable"
                    _JsonData.ItemTouchMasterTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.ItemTouchMasterTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.ItemTouchMasterTable.ReadXml(Path)
                    _JsonData.ItemTouchMasterTable.TableName = "ItemTouchMasterTable"
                    If _JsonData.ItemTouchMasterTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function getMainMaster() As Boolean
        Try
            Dim Path As String = filePath & "MainGroupTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                _JsonData.MainGroupTable.TableName = "MainGroupTable"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=16")
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.MainGroupTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.MainGroupTable.Rows.Count > 0 Then
                    _JsonData.MainGroupTable.TableName = "MainGroupTable"
                    _JsonData.MainGroupTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.MainGroupTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.MainGroupTable.ReadXml(Path)
                    _JsonData.MainGroupTable.TableName = "MainGroupTable"
                    If _JsonData.MainGroupTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getMainGroupPolicy() As Boolean
        Try
            Dim Path As String = filePath & "MainGroupPolicyTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                Dim url As String = M_Details.LinkAjaxRequest & "MgmtRequest=9&Operation=SELECTACTIVE&Comid=" & _companyInfo.ComId & "&Locid=" & _companyInfo.LocId
                _JsonData.MainGroupPolicyTable.TableName = "MainGroupPolicyTable"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(url)
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.MainGroupPolicyTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.MainGroupPolicyTable.Rows.Count > 0 Then
                    _JsonData.MainGroupPolicyTable.TableName = "MainGroupPolicyTable"
                    _JsonData.MainGroupPolicyTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.MainGroupPolicyTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.MainGroupPolicyTable.ReadXml(Path)
                    _JsonData.MainGroupPolicyTable.TableName = "MainGroupPolicyTable"
                    If _JsonData.MainGroupPolicyTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getCategoryMaster() As Boolean
        Try
            Dim Path As String = filePath & "CategoryTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                _JsonData.CategoryTable.TableName = "CategoryTable"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=20")
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.CategoryTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.CategoryTable.Rows.Count > 0 Then
                    _JsonData.CategoryTable.TableName = "CategoryTable"
                    _JsonData.CategoryTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.CategoryTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.CategoryTable.ReadXml(Path)
                    _JsonData.CategoryTable.TableName = "CategoryTable"
                    If _JsonData.CategoryTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getCustomerMaster() As Boolean
        Try
            Dim Path As String = filePath & "CustomerTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                _JsonData.CustomerTable.TableName = "CustomerTable"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=71")
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.CustomerTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.CustomerTable.Rows.Count > 0 Then
                    _JsonData.CustomerTable.TableName = "CustomerTable"
                    _JsonData.CustomerTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.CustomerTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.CustomerTable.ReadXml(Path)
                    _JsonData.CustomerTable.TableName = "CustomerTable"
                    If _JsonData.CustomerTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function GetSalesmanData() As Boolean
        Try
            Dim Path As String = filePath & "SalesManDataTable.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                _JsonData.CustomerTable.TableName = "CustomerTable"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=13&Comid=" & _companyInfo.ComId & "&Locid=" & _companyInfo.LocId)
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.SalesManDataTable = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.SalesManDataTable.Rows.Count > 0 Then
                    _JsonData.SalesManDataTable.TableName = "SalesManDataTable"
                    _JsonData.SalesManDataTable.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.SalesManDataTable.Clear()
                    ' Read XML file into DataTable
                    _JsonData.SalesManDataTable.ReadXml(Path)
                    _JsonData.SalesManDataTable.TableName = "SalesManDataTable"
                    If _JsonData.SalesManDataTable.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getBranchTableMaster() As Boolean
        Try
            _JsonData.BranchTable.TableName = "BranchTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=33")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.BranchTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.BranchTable.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getUnitMasterMaster() As Boolean
        Try
            _JsonData.UnitMasterTable.TableName = "UnitMasterTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=35")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.UnitMasterTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.UnitMasterTable.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getSupplierTableMaster() As Boolean
        Try
            _JsonData.SupplierTable.TableName = "SupplierTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=40")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.SupplierTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.SupplierTable.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getSupplierLedgerMaster() As Boolean
        Try

            _JsonData.SupplierTable.TableName = "SupplierTable"
            _JsonData.SupplierTable.Rows.Clear()
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=45")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.SupplierTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.SupplierTable.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function


    Public Function getPurchaseView() As Boolean
        Try
            _JsonData.PurchaseViewTable.TableName = "PurchaseView"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=47")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.PurchaseViewTable = Userparsejson("Data").ToObject(Of DataTable)()
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    'Public Function getClientInfo() As Boolean
    '    Try
    '        _JsonData.ClientTable.TableName = "ClientTable"
    '        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
    '        Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesRequest=1")
    '        Dim Userparsejson As JObject = JObject.Parse(json)
    '        _JsonData.ClientTable = Userparsejson("Data").ToObject(Of DataTable)()
    '        If _JsonData.ClientTable.Rows.Count > 0 Then
    '            Return True
    '        End If
    '        Return True
    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return False
    '    End Try
    'End Function

    Public Function getBankList() As Boolean
        Try
            _JsonData.BankListTable.TableName = "BankListTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AccountRequest=12")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.BankListTable = Userparsejson("Data").ToObject(Of DataTable)()
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getLedgerListTable() As Boolean
        Try
            _JsonData.LedgerListTable.TableName = "LedgerListTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesRequest=1")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.LedgerListTable = Userparsejson("Data").ToObject(Of DataTable)()
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getPaymentList() As Boolean
        Try
            Dim Path As String = filePath & "PaymodeList.xml"
            ' Check if internet is available
            If CheckForInternetConnection() Then
                _JsonData.PaymodeList.TableName = "PaymodeList"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesRequest=3")
                Dim Userparsejson As JObject = JObject.Parse(json)
                _JsonData.PaymodeList = Userparsejson("Data").ToObject(Of DataTable)()
                If _JsonData.PaymodeList.Rows.Count > 0 Then
                    _JsonData.PaymodeList.TableName = "PaymodeList"
                    _JsonData.PaymodeList.WriteXml(Path, XmlWriteMode.WriteSchema)
                    Return True
                End If
            Else
                If File.Exists(Path) Then
                    ' Clear existing data
                    _JsonData.PaymodeList.Clear()
                    ' Read XML file into DataTable
                    _JsonData.PaymodeList.ReadXml(Path)
                    _JsonData.PaymodeList.TableName = "PaymodeList"
                    If _JsonData.PaymodeList.Rows.Count > 0 Then
                        Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function GetSalesByBillWeb(ByVal Billno As Integer, ByRef _ds As DataSet) As Boolean
        Try
            Dim billDtl As New DataTable
            Dim billHdr As New DataTable
            billDtl.TableName = "billDtl"
            billHdr.TableName = "billHdr"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesRequest=8&billno=" & Billno)
            Dim Userparsejson As JObject = JObject.Parse(json)
            Dim Msg = Userparsejson("Success").ToString
            If Msg.ToString = "True" Then
                billDtl = Userparsejson("DTL").ToObject(Of DataTable)()
                billHdr = Userparsejson("HDR").ToObject(Of DataTable)()
                _ds.Tables.Add(billDtl)
                _ds.Tables.Add(billHdr)
                _
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function GetSalesByQuoteBillWeb(ByVal Billno As Integer, ByRef _ds As DataSet) As Boolean
        Try
            Dim billDtl As New DataTable
            Dim billHdr As New DataTable
            billDtl.TableName = "billDtl"
            billHdr.TableName = "billHdr"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesRequest=13&billno=" & Billno)
            Dim Userparsejson As JObject = JObject.Parse(json)
            Dim Msg = Userparsejson("Success").ToString
            If Msg.ToString = "True" Then
                billDtl = Userparsejson("DTL").ToObject(Of DataTable)()
                billHdr = Userparsejson("HDR").ToObject(Of DataTable)()
                _ds.Tables.Add(billDtl)
                _ds.Tables.Add(billHdr)
                _
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getGroupInfo() As Boolean
        Try
            _JsonData.AccountGroup.TableName = "GroupTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AccountRequest=4")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.AccountGroup = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.AccountGroup.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getParentInfo() As Boolean
        Try
            _JsonData.AccountParent.TableName = "AccountParent"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AccountRequest=8")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.AccountParent = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.AccountParent.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getLedgerInfo() As Boolean
        Try
            _JsonData.AccountLedger.TableName = "LedgerTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AccountRequest=11")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.AccountLedger = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.AccountLedger.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getMonthOfSalaryInfo() As Boolean
        Try
            _JsonData.MonthOfSalary.TableName = "MonthOfSalary"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "EmployeeReq=11")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.MonthOfSalary = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.MonthOfSalary.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function CheckForInternetConnection() As Boolean
        Try
            Using client = New WebClient()
                Using stream = client.OpenRead("http://www.google.com")
                    Return True
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function
    Public Function chkRegistryKey(ByRef ver As String) As Boolean
        Try
            Dim sysName As String
            Dim hdd_serial As String
            Dim syshdd_serial As String
            Dim tDate As String
            Dim c As New clsRegisterKey
            Dim isbool As Boolean
            Dim licenseChar As String
            Dim regDcrpt As String
            Dim arreg As String()
            Dim tDate1 As String
            Dim errstr As String = String.Empty
            isbool = True

            sysName = c.getComputerName()
            syshdd_serial = c.GetDriveSerialNumber()
            tDate = c.getInstallDate()




            Dim regVersion As RegistryKey
            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\osbin\\v2", True)
            If regVersion Is Nothing Then
                isbool = False
                'Dim frm As New frmActivation
                'frmActivation.ShowDialog()
                MessageBox.Show("Licence Key Not Found, Please Contact Admin", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
                'Application.Exit()
            Else
                regDcrpt = c.Decrypt(regVersion.GetValue("POSBean License"))
                If regDcrpt Is Nothing OrElse regDcrpt = "" Then
                    isbool = False
                    'frmActivation.ShowDialog()
                    MessageBox.Show("Trial Version Expired, Please Contact Admin", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                arreg = regDcrpt.Split("-")
                licenseChar = arreg(0)
                tDate = arreg(1)
                hdd_serial = arreg(2)
                tDate1 = c.getInstallDate()
                isTrial = False
                If hdd_serial <> syshdd_serial Then
                    MessageBox.Show("Licence Key Not Match", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                    'Application.Exit()
                End If
                If licenseChar = "T" Then
                    isTrial = True
                    'frmChequeprint.barstatustrial.Caption = "Trial Version"
                    MessageBox.Show("Your Are Running Trial Version", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

                If isTrial = True Then
                    If c.generateRegistery(sysName, hdd_serial, tDate, True, errstr) = False Then
                        isbool = False
                    End If
                Else
                    'frmChequeprint.barstatustrial.Caption = "License Activated"
                    If c.generateRegistery(sysName, hdd_serial, tDate, False, errstr) = False Then
                        isbool = False
                    End If
                End If

                'MessageBox.Show(errstr)
                If isbool = False Then
                    'frmActivation.ShowDialog()
                    MessageBox.Show("Trial Version Expired, Please Contact Admin", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'Application.Exit()
                    Return False
                End If

            End If
            ver = licenseChar
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Activation")
            Return False
            ' DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, Version, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    Public Function _validateUserLogin(ByRef user As String, ByRef pass As String) As Boolean
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=4&name=" & user & "&pass=" & pass)
            Dim Userparsejson As JObject = JObject.Parse(json)
            Dim dtresults = Userparsejson("Success")
            Dim data = Userparsejson("Data")
            If dtresults.ToString = "True" Then
                _companyInfo.UserId = data("UserId")
                _companyInfo.UserName = data("UserName")
                _companyInfo.UserRole = data("UserRole")
                _companyInfo.UserRoleId = data("GroupId")
                'Dim dtrows As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.USerTable Where dtrow("Id") = _companyInfo.UserId
                'getUserPolicyInfo(_companyInfo.UserId)
                'If dtrows.Any Then
                '    _companyInfo.UserRole = dtrows(0)("Role")
                'End If
                Return True
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Function _JsonSend(ByRef _val As String) As Boolean
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(_val)
            Dim Userparsejson As JObject = JObject.Parse(json)
            Dim dtresults = Userparsejson("Success")
            ' Dim MsgResultHDR = Userparsejson("hdr")
            'Dim MsgResultDTL = Userparsejson("dtl")
            Dim MsgResultMsg = Userparsejson("Data")
            If dtresults.ToString = "True" Then

                Return True
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function _JsonSend(ByRef _val As String, ByRef ReturnId As String, Optional ByRef ErrMsg As String = "0") As Boolean
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(_val)
            Dim Userparsejson As JObject = JObject.Parse(json)
            Dim dtresults = Userparsejson("Success")
            ' Dim MsgResultHDR = Userparsejson("hdr")
            'Dim MsgResultDTL = Userparsejson("dtl")
            Dim MsgResultMsg = Userparsejson("Data")
            If dtresults.ToString = "True" Then
                ReturnId = MsgResultMsg.ToString
                Return True
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function _JsonSend(ByRef _val As String, ByRef ErrMsg As String) As Boolean
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim json As String = ""
            Dim client As New System.Net.WebClient()

            ' Check URL length - if > 2000 chars, use POST method
            If _val.Length > 2000 Then
                ' Parse URL to separate base URL and parameters
                Dim urlParts As String() = _val.Split("?"c)
                If urlParts.Length = 2 Then
                    Dim baseUrl As String = urlParts(0)
                    Dim queryString As String = urlParts(1)

                    ' Convert query string to POST data
                    Dim postData As String = queryString
                    Dim postBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)

                    ' Set headers for POST request
                    client.Headers("Content-Type") = "application/x-www-form-urlencoded"

                    ' Send POST request
                    Dim responseBytes As Byte() = client.UploadData(baseUrl, "POST", postBytes)
                    json = System.Text.Encoding.UTF8.GetString(responseBytes)
                Else
                    ' Fallback to GET if URL parsing fails
                    json = client.DownloadString(_val)
                End If
            Else
                ' Use GET for smaller requests
                json = client.DownloadString(_val)
            End If

            Dim Userparsejson As JObject = JObject.Parse(json)
            Dim dtresults = Userparsejson("Success")
            Dim Data = Userparsejson("Data")
            If dtresults.ToString = "True" Then
                ErrMsg = Data.ToString
                Return True
            Else
                ErrMsg = Data.ToString
                Return False
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ErrMsg = ex.Message
            Return False
        End Try
    End Function
    Public Function _JsonSendSales(ByRef _val As String, ByRef ErrMsg As String, ByRef BillNo As String) As Boolean
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim json As String = ""
            Dim client As New System.Net.WebClient()

            ' Check URL length - if > 2000 chars, use POST method
            If _val.Length > 2000 Then
                ' Parse URL to separate base URL and parameters
                Dim urlParts As String() = _val.Split("?"c)
                If urlParts.Length = 2 Then
                    Dim baseUrl As String = urlParts(0)
                    Dim queryString As String = urlParts(1)

                    ' Convert query string to POST data
                    Dim postData As String = queryString
                    Dim postBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)

                    ' Set headers for POST request
                    client.Headers("Content-Type") = "application/x-www-form-urlencoded"

                    ' Send POST request
                    Dim responseBytes As Byte() = client.UploadData(baseUrl, "POST", postBytes)
                    json = System.Text.Encoding.UTF8.GetString(responseBytes)
                Else
                    ' Fallback to GET if URL parsing fails
                    json = client.DownloadString(_val)
                End If
            Else
                ' Use GET for smaller requests
                json = client.DownloadString(_val)
            End If

            Dim Userparsejson As JObject = JObject.Parse(json)
            Dim dtresults = Userparsejson("Success")
            Dim Data = Userparsejson("Data")
            Dim InvoiceNo = Userparsejson("InvoiceNo")
            If dtresults.ToString = "True" Then
                ErrMsg = Data.ToString
                BillNo = InvoiceNo.ToString
                Return True
            Else
                ErrMsg = Data.ToString
                BillNo = 0
                Return False
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ErrMsg = ex.Message
            Return False
        End Try
    End Function
    Public Function ImageToStream(ByVal fileName As String) As Byte()
        Dim stream As New MemoryStream()
        Try
            Dim image As New Bitmap(fileName)
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg)
        Catch ex As Exception
        End Try
        Return stream.ToArray()
    End Function
    Public Function img2byte(ByVal bitmap As System.Drawing.Bitmap, Optional ByRef ErrorMsg As String = "") As Byte()
        Try
            Dim MS As New MemoryStream()
            bitmap.Save(MS, System.Drawing.Imaging.ImageFormat.Png)
            Return MS.ToArray()
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return Nothing
        End Try
    End Function
    Public Function _DateConversionMonthYear(ByRef recDate As DateTime, ByRef ColDate As String) As Boolean
        Try
            Dim reformatted As String = ""
            Dim _dateTime As DateTime = DateTime.Parse(recDate)
            ColDate = _dateTime.ToString("MMM-yyyy", CultureInfo.InvariantCulture)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function NumberToText(ByVal n As Double) As String

        Select Case n
            Case 0
                Return ""

            Case 1 To 19
                Dim arr() As String = {"One", "Two", "Three", "Four", "Five", "Six", "Seven", _
                  "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", _
                    "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
                Return arr(n - 1) & " "

            Case 20 To 99
                Dim arr() As String = {"Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}
                Return arr(n \ 10 - 2) & " " & NumberToText(n Mod 10)

            Case 100 To 199
                Return "One Hundred " & NumberToText(n Mod 100)

            Case 200 To 999
                Return NumberToText(n \ 100) & "Hundreds " & NumberToText(n Mod 100)

            Case 1000 To 1999
                Return "One Thousand " & NumberToText(n Mod 1000)

            Case 2000 To 999999
                Return NumberToText(n \ 1000) & "Thousands " & NumberToText(n Mod 1000)

            Case 1000000 To 1999999
                Return "One Million " & NumberToText(n Mod 1000000)

            Case 1000000 To 999999999
                Return NumberToText(n \ 1000000) & "Millions " & NumberToText(n Mod 1000000)

            Case 1000000000 To 1999999999
                Return "One Billion " & NumberToText(n Mod 1000000000)

            Case Else
                Return NumberToText(n \ 1000000000) & "Billion " _
                  & NumberToText(n Mod 1000000000)
        End Select
    End Function
    Function NumberInWords(num As String)
        'Constants are Defined
        Dim digit(100) As String
        digit(0) = ""
        digit(1) = "One "
        digit(2) = "Two "
        digit(3) = "Three "
        digit(4) = "Four "
        digit(5) = "Five "
        digit(6) = "Six "
        digit(7) = "Seven "
        digit(8) = "Eight "
        digit(9) = "Nine "
        digit(10) = "Ten "
        digit(11) = "Eleven "
        digit(12) = "Twelve "
        digit(13) = "Thirteen "
        digit(14) = "Fourteen "
        digit(15) = "Fifteen "
        digit(16) = "Sixteen "
        digit(17) = "Seventeen "
        digit(18) = "Eighteen "
        digit(19) = "Ninteen "
        digit(20) = "Twenty "
        digit(30) = "Thirty "
        digit(40) = "Fourty "
        digit(50) = "Fifty "
        digit(60) = "Sixty "
        digit(70) = "Seventy "
        digit(80) = "Eighty "
        digit(90) = "Ninety "
        digit(100) = "Hundred "
        Dim tt(5) As String
        tt(2) = "Thousand "
        tt(3) = "Hundred Thousand " '"Lakh "
        tt(4) = "Billion " '"Crore "
        tt(5) = "Trillion " '"Hundred Crore "
        'Separating the Whole Number and Digits
        Dim nn As String
        Dim dd As String = ""
        nn = Math.Round(Val(num), 2)
        If InStr(nn, ".") <> 0 Then
            dd = Mid(nn, InStr(nn, ".") + 1)
            nn = Mid(nn, 1, InStr(nn, ".") - 1)
        End If

        'Variable nn stores the whole number and dd stores the digits
        'Finding the Word for numbers

        Dim x As Integer
        Dim y As Integer = 0
        x = nn.Length - 1
        Dim z As String
        Dim str As String = ""
        Dim str1 As String = ""
        If x > 1 Then
            While (x > -1)
                'First Loop Last two digits of Number is evaluated(ones and Tens)
                If y = 0 Then
                    z = Mid(nn, x, 2)
                    If Val(z) < 21 And Val(z) > 0 Then
                        str = digit(Val(z))
                    ElseIf Val(z) > 0 Then
                        str = digit(Val(z(0)) * 10)
                        str = str & digit(Val(z(1)))
                    End If
                    x = x - 1
                End If


                'Second Loop 3rd digits of Number is evaluated(Hundred)

                If y = 1 Then
                    z = Mid(nn, x, 1)
                    If Val(z) <> 0 Then
                        str = digit(Val(z)) & "Hundred " & str
                    End If
                    x = x - 2
                End If

                'Subsequent Loop Next two digits sequence of Number is evaluated(Thousands,Lakhs,Crore,etc)


                If y > 1 Then
                    If x <> 0 Then
                        z = Mid(nn, x, 2)
                        If Val(z) < 21 And Val(z) > 0 Then
                            str = digit(Val(z)) & tt(y) & str
                        ElseIf Val(z) > 0 Then
                            str1 = digit(Val(z(0)) * 10)
                            str = str1 & digit(Val(z(1))) & tt(y) & str
                        End If
                        x = x - 2
                    Else
                        z = Mid(nn, 1, 1)
                        If Val(z) < 21 And Val(z) > 0 Then
                            str = digit(Val(z)) & tt(y) & str
                        ElseIf Val(z) > 0 Then
                            str1 = digit(Val(z(0)) * 10)
                            str = str1 & digit(Val(z(1))) & tt(y) & str
                        End If
                        x = -1
                    End If
                End If
                y = y + 1
            End While
        Else
            If Val(nn) < 21 And Val(nn) > 0 Then
                str = digit(Val(nn))
            ElseIf Val(nn) > 0 Then
                str = digit(Val(nn(0)) * 10)
                str = str & digit(Val(nn(1)))
            End If

            'str = digit(nn)

        End If
        If str = "" Then
            str = "Zero "
        End If
        str = str & "Ringgit "

        'Digits are evaluated(Paise)

        If Val(dd) > 0 Then
            If dd.Length = 1 Then
                z = Val(dd) * 10
            Else
                z = dd
            End If

            If Val(z) < 21 And Val(z) > 0 Then
                str = str & "and " & digit(Val(z)) & "Sen"
            ElseIf Val(z) > 0 Then
                str1 = digit(Val(z(0)) * 10)
                str = str & "and " & str1 & digit(Val(z(1))) & "Sen"
            End If
        End If

        'Word string is returned

        NumberInWords = str & " Only"
    End Function
    Public Sub _CurrencyFormat(ByRef recAmount As String, ByRef retAmount As String)
        Try
            If recAmount.Contains("RM") Then
                retAmount = recAmount.Replace("RM", "")
            Else
                retAmount = recAmount
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub _RemoveCommaFormat(ByRef recAmount As String, ByRef retAmount As String)
        Try
            If recAmount.Contains(",") Then
                retAmount = recAmount.Replace(",", "")
            Else
                retAmount = recAmount
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub _Addspace(ByRef spStr As String, ByRef formated As String)
        Try
            Dim data As String = spStr
            Dim sb As New StringBuilder()
            Dim addSpace As Boolean = True
            For Each c As Char In data
                If addSpace Then
                    sb.Append(c + "    ")
                    'addSpace = False
                Else
                    sb.Append(c)
                    addSpace = True
                End If
            Next
            sb.Length = sb.Length - 1 ''Remove last space on string
            formated = sb.ToString
        Catch ex As Exception

        End Try
    End Sub
    Public Function CreateMonthTable() As DataTable
        Try
            Dim MonthDataTable = New DataTable
            MonthDataTable.TableName = "MonthCalculation"
            MonthDataTable.Columns.Add("SNo", GetType(Integer)).DefaultValue = 0
            MonthDataTable.Columns.Add("EmpTrId", GetType(Integer)) '0
            MonthDataTable.Columns.Add("EmpRefId", GetType(Integer)) '1
            MonthDataTable.Columns.Add("EmpName", GetType(String)) '2
            MonthDataTable.Columns.Add("EmpMonth", GetType(String)) '3
            MonthDataTable.Columns.Add("EmpComId", GetType(Integer)) '4
            MonthDataTable.Columns.Add("EmpComName", GetType(String)) '5
            MonthDataTable.Columns.Add("EmpLocId", GetType(Integer)) '6
            MonthDataTable.Columns.Add("EmpLocName", GetType(String)) '7
            MonthDataTable.Columns.Add("EmpNoOfDays", GetType(Double)) '8
            MonthDataTable.Columns.Add("EmpExtraDays", GetType(Double)) '9
            MonthDataTable.Columns.Add("EmpExtraOtHrs", GetType(Double)) '10
            MonthDataTable.Columns.Add("EmpAdvance", GetType(Double)) '11
            MonthDataTable.Columns.Add("EmpDeduction", GetType(Double)) '12
            MonthDataTable.Columns.Add("EmpBankIn", GetType(Double)) '13
            Return MonthDataTable
        Catch ex As Exception
            Return Nothing

        End Try
    End Function
    Public Function CreateFinalTable() As DataTable
        Try
            Dim FinalMonthDataTable = New DataTable
            FinalMonthDataTable.TableName = "FinalMonth"
            FinalMonthDataTable.Columns.Add("SNo", GetType(Integer)).DefaultValue = 0
            FinalMonthDataTable.Columns.Add("EmpTrId", GetType(Integer)) '0
            FinalMonthDataTable.Columns.Add("EmpRefId", GetType(Integer)) '1
            FinalMonthDataTable.Columns.Add("EmpName", GetType(String)) '2
            FinalMonthDataTable.Columns.Add("EmpMonth", GetType(String)) '3
            FinalMonthDataTable.Columns.Add("EmpComId", GetType(Integer)) '4
            FinalMonthDataTable.Columns.Add("EmpComName", GetType(String)) '5
            FinalMonthDataTable.Columns.Add("EmpLocId", GetType(Integer)) '6
            FinalMonthDataTable.Columns.Add("EmpLocName", GetType(String)) '7
            FinalMonthDataTable.Columns.Add("EmpBasic", GetType(Double)) '8
            FinalMonthDataTable.Columns.Add("EmpNoOfDays", GetType(Double)) '9
            FinalMonthDataTable.Columns.Add("EmpWages", GetType(Double)) '10
            FinalMonthDataTable.Columns.Add("EmpExtraDays", GetType(Double)) '11
            FinalMonthDataTable.Columns.Add("EmpExtraDayAmt", GetType(Double)) '12
            FinalMonthDataTable.Columns.Add("EmpExtraOtHrs", GetType(Double)) '13
            FinalMonthDataTable.Columns.Add("EmpExtraOtAmt", GetType(Double)) '14
            FinalMonthDataTable.Columns.Add("EmpAllowance", GetType(Double)) '16
            FinalMonthDataTable.Columns.Add("EmpGrossAmt", GetType(Double)) '17
            FinalMonthDataTable.Columns.Add("EmpAdvance", GetType(Double)) '18
            FinalMonthDataTable.Columns.Add("EmpEpf", GetType(Double)) '19
            FinalMonthDataTable.Columns.Add("EmpSocso", GetType(Double)) '20
            FinalMonthDataTable.Columns.Add("EmpDeduction", GetType(Double)) '21
            FinalMonthDataTable.Columns.Add("EmpNetPay", GetType(Double)) '22
            FinalMonthDataTable.Columns.Add("EmpBank", GetType(Double)) '23
            FinalMonthDataTable.Columns.Add("EmpNetCash", GetType(Double)) '23
            Return FinalMonthDataTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Dim strMsg As String
    Public Sub updateStr(ByRef str As String)
        Try
            'Threading.Thread.Sleep(1000)
            ' timer.Interval = 1000
            strMsg = DateAndTime.Now.ToString & ": " & str & vbNewLine
            LogFileText._richTextBox.AppendText(strMsg.ToString)
            'timer.Enabled = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

End Module

