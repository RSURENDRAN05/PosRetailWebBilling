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
    Public ini As New IniFile(M_Details.AppPath & "\Settings\" & "Settings.ini")
    Public PanelScreenWith As Integer = 0
    Public PanelScreenHeight As Integer = 0
    Public G_GRNNo As String = String.Empty
    Public E_EmployeeId As Integer = 0
    Public Structure M_Details
        Public Shared SoftwareVersion As String = "MGMT VER25.0.0.22 R3 03072025"
        Public Shared AppPathDirectory As String = AppDomain.CurrentDomain.BaseDirectory
        Public Shared AppPath As String = Application.StartupPath
        Public Shared LinkAjaxRequest As String = ini.ReadValue("Profile", "UrlLink")
        Public Shared LinkAjaxRequestCheque As String = ""
        Public Shared licenceActive As String = ""
        Public Shared licenceServerCleint As String = ""
    End Structure
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
    End Structure
    Public Structure _JsonData
        Public Shared USerTable As New DataTable
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
        Public Shared PurchaseViewTable As New DataTable
        Public Shared ClientTable As New DataTable
        Public Shared AgentTable As New DataTable
        'Public Shared InsuranceTable As New DataTable
        Public Shared BankListTable As New DataTable
        Public Shared LedgerListTable As New DataTable
        Public Shared PaymodeList As New DataTable
        Public Shared AccountGroup As New DataTable
        Public Shared AccountParent As New DataTable
        Public Shared AccountLedger As New DataTable
        Public Shared MonthOfSalary As New DataTable
        Public Shared PosSettingsTable As New DataTable
        Public Shared SalesManCommissionTable As New DataTable
     
    End Structure
    Public Structure _discount
        Public Shared DiscountPer As Boolean = False
        Public Shared discountValue As Double = 0
    End Structure
    Public Structure _PaymentDtl
        Public Shared paymentModeSelection As String = ""
        Public Shared paymentMode As String = ""
    End Structure
    Public Structure _globalSetting
        Public Shared TaxExculsive As Boolean = False   'true exclusive  or false  inclusive
        Public Shared QuoteBill As Boolean = False
        Public Shared PaymentMachine As Boolean = False
        Public Shared ResponseData As Object = ""
        Public Shared PriceEdit As Boolean = False
        Public Shared ServiceTaxActive As Boolean = False
        Public Shared SearchProductCode As Boolean = True 'SearchByProductcode,SearchByBarcode
        Public Shared BillDiscountAcitve As Boolean = False
        Public Shared ItemDiscountActive As Boolean = False
        Public Shared SelectMultiplePriceActive As Boolean = False
        Public Shared SalesManEachItemActive As Boolean = False
    End Structure
    Public Structure _globalSettingValues
        Public Shared ServiceTaxValue As String = "0"
    End Structure
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
   
    Public Function getUserInfo() As Boolean
        Try
            _JsonData.USerTable.TableName = "UserTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=1")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.USerTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.USerTable.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getUserPolicyInfo(ByRef user_id As String) As Boolean
        Try
            _JsonData.UserPolicyTable.TableName = "UserPolicyTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "GroupPolicyRequest=6&user_id=" & user_id)
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.UserPolicyTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.UserPolicyTable.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getSalesManCommissionInfo() As Boolean
        Try
            _JsonData.SalesManCommissionTable.TableName = "SalesManCommissionTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=7")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.SalesManCommissionTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.SalesManCommissionTable.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getPosSettingsInfo() As Boolean
        Try
            _JsonData.PosSettingsTable.TableName = "PosSettingsTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "GroupPolicyRequest=8&operation=SELECT")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.PosSettingsTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.PosSettingsTable.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getComapnyInfo() As Boolean
        Try
            _JsonData.CompanyTable.TableName = "ComapnyTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=51")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.CompanyTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.CompanyTable.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getLocationInfo() As Boolean
        Try
            _JsonData.LocationTable.TableName = "LocationTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=3")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.LocationTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.LocationTable.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getComapnyLocationInfo() As Boolean
        Try
            _JsonData.CompanyLocationTable.TableName = "ComapnyLocationTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=2")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.CompanyLocationTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.CompanyLocationTable.Rows.Count > 0 Then
                Return True
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
            _JsonData.ItemMasterTable.TableName = "ItemMasterTable"
            Dim _dt As New DataTable
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=42")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _dt = Userparsejson("Data").ToObject(Of DataTable)()
            Dim dtrows As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _dt Where dtrow("COMID") = _companyInfo.ComId And dtrow("LOCID") = _companyInfo.LocId

            If dtrows.Any Then
                _JsonData.ItemMasterTable = dtrows.CopyToDataTable
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getMainMaster() As Boolean
        Try
            _JsonData.MainGroupTable.TableName = "MainTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=16")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.MainGroupTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.MainGroupTable.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getCategoryMaster() As Boolean
        Try
            _JsonData.CategoryTable.TableName = "CateTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=20")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.CategoryTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.CategoryTable.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getCustomerMaster() As Boolean
        Try
            _JsonData.CustomerTable.TableName = "CustomerTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=30")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.CustomerTable = Userparsejson("Data").ToObject(Of DataTable)()
            If _JsonData.CustomerTable.Rows.Count > 0 Then
                Return True
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
            _JsonData.PaymodeList.TableName = "PaymodeList"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesRequest=3")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _JsonData.PaymodeList = Userparsejson("Data").ToObject(Of DataTable)()
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function GetSalesByBill(ByVal Billno As Integer, ByRef _ds As DataSet) As Boolean
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
                _Ds.Tables.Add(billDtl)
                _Ds.Tables.Add(billHdr)
                _
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function GetSalesByQuoteBill(ByVal Billno As Integer, ByRef _ds As DataSet) As Boolean
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
    Public Function _JsonSend(ByRef _val As String, ByRef ErrMsg As String) As Boolean
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(_val)
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
    Public Function _DateConversion(ByRef recDate As DateTime, ByRef ColDate As String) As Boolean
        Try
            Dim reformatted As String = ""
            Dim _dateTime As DateTime = DateTime.Parse(recDate)
            ColDate = _dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            Return True
        Catch ex As Exception
            Return False
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
    Public Function _DateConversion(ByRef recDate As DateTime, ByRef ColDate As String, ByRef dateFormat As String) As Boolean
        Try
            Dim reformatted As String = ""
            Dim _dateTime As DateTime = DateTime.Parse(recDate)
            ColDate = _dateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
            dateFormat = _dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
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

End Module

