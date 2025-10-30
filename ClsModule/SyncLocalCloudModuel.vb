Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports System.Net
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json

Module SyncLocalCloudModuel
    Public Function _ReadSyncLocalCloud() As Boolean
        Try

            M_Details.LinkAjaxRequest = ini.ReadValue("Profile", "UrlLink")
            M_Details.LinkAjaxRequestCheque = ini.ReadValue("Profile", "UrlLinkCheque")
            M_Details.LinkAjaxRequestSyncLocalCloud = ini.ReadValue("Profile", "UrlLinkSyncLocalCloud")
            M_Details.licenceServerCleint = ini.ReadValue("Profile", "ServerClient")
            M_Details.LocationId = ini.ReadValue("Bank", "LocationId")
            M_Details.CompanyId = ini.ReadValue("Bank", "CompanyId")
            M_Details.LocationName = ini.ReadValue("Bank", "LocationName")
            M_Details.CompanyName = ini.ReadValue("Bank", "CompanyName")
            M_Details.PMID = ini.ReadValue("Bank", "PMID")
            _companyInfo.ComId = M_Details.CompanyId
            _companyInfo.CompanyName = M_Details.CompanyName
            _companyInfo.LocId = M_Details.LocationId
            _companyInfo.LocationName = M_Details.LocationName
            _companyInfo.CompanyPMID = M_Details.PMID
            If chkRegistryKey(M_Details.licenceActive) = False Then
                End
            End If
            If _ReadDefaultLocalData() = False Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function _ReadSyncLocalCloudWebService() As Boolean
        Try

            M_Details.LinkAjaxRequest = ini.ReadValue("Profile", "UrlLink")
            M_Details.LinkAjaxRequestCheque = ini.ReadValue("Profile", "UrlLinkCheque")
            M_Details.LinkAjaxRequestSyncLocalCloud = ini.ReadValue("Profile", "UrlLinkSyncLocalCloud")
            M_Details.licenceServerCleint = ini.ReadValue("Profile", "ServerClient")
            M_Details.LocationId = ini.ReadValue("Bank", "LocationId")
            M_Details.CompanyId = ini.ReadValue("Bank", "CompanyId")
            M_Details.LocationName = ini.ReadValue("Bank", "LocationName")
            M_Details.CompanyName = ini.ReadValue("Bank", "CompanyName")
            M_Details.PMID = ini.ReadValue("Bank", "PMID")
            _companyInfo.ComId = M_Details.CompanyId
            _companyInfo.CompanyName = M_Details.CompanyName
            _companyInfo.LocId = M_Details.LocationId
            _companyInfo.LocationName = M_Details.LocationName
            _companyInfo.CompanyPMID = M_Details.PMID
            If chkRegistryKey(M_Details.licenceActive) = False Then
                End
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function _ReadDefaultLocalData() As Boolean
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            dialog.Caption = "_ReadSyncLocalCloud"

            If File.Exists(M_Details._appPath & "\LayOut\Printer.xml") Then
                _posSettingDs = New DataSet
                _posSettingDs.ReadXml(M_Details._appPath & "\LayOut\Printer.xml")
                _DotmatrixTemp._PrinterName = _posSettingDs.Tables(0).Rows(2).Item("Value").ToString
            End If
            Dim localDbPath As String = M_Details._appPath & "\LocalDataBase\"
            If Not Directory.Exists(localDbPath) Then
                Directory.CreateDirectory(localDbPath)
            End If
            If getUserInfo() = True Then
                dialog.Caption = "User Data Received"
            Else
                dialog.Caption = "User  Not Received"
            End If
            If getComapnyLocationInfo() = True Then
                dialog.Caption = "Company Data Received"
            Else
                dialog.Caption = "Company  Not Received"
            End If
            If getComapnyInfo() = True Then
                dialog.Caption = "Company Data Received"

            Else
                dialog.Caption = "Company  Not Received"
            End If
            If getLocationInfo() = True Then
                dialog.Caption = "Location Data Received"
            Else
                dialog.Caption = "Location Not Received"
            End If
            If getPosSettingsInfo() = True Then
                LoadPosSettings()
                dialog.Caption = "Loading Pos Settings"
            Else
                dialog.Caption = "Pos Setting Not Received"
            End If
            If getSalesManCommissionInfo() = True Then

                dialog.Caption = "Loading SalesCommission"
            Else
                dialog.Caption = "SalesCommission Not Received"
            End If
            If getPaymentTermTable() = True Then
                dialog.Caption = "Loading Payment Term"
            Else
                dialog.Caption = "PaymentTerm  Not Received"
            End If
            If getCategoryMaster() = True Then
                dialog.Caption = "Loading CategoryMaster"
            Else
                dialog.Caption = "CategoryMaster Not Received"
            End If
            If getMainMaster() = True Then
                dialog.Caption = "Loading MainMaster"
            Else
                dialog.Caption = "MainMaster Not Received"
            End If
            If getItemMaster() = True Then
                dialog.Caption = "Loading ItemMaster"
            Else
                dialog.Caption = "ItemMaster Not Received"
            End If
            If getTouchItemMaster() = True Then
                dialog.Caption = "Loading TouchItemMaster"
            Else
                dialog.Caption = "TouchItemMaster Not Received"
            End If
            If getButtonStyleTable() = True Then
                dialog.Caption = "Loading ButtonStyleTable"
            Else
                dialog.Caption = "ButtonStyleTable Not Received"
            End If
            If getPaymentList() = True Then
                dialog.Caption = "Loading PaymentList"
            Else
                dialog.Caption = "PaymentList Not Received"
            End If
            If getCustomerMaster() = True Then
                dialog.Caption = "Loading CustomerMaster"
            Else
                dialog.Caption = "CustomerMaster Not Received"
            End If
            If GetSalesmanData() = True Then
                dialog.Caption = "Loading SalesmanData"
            Else
                dialog.Caption = "SalesmanData Not Received"
            End If
            If GetMultiplePricesFromServer() = True Then
                dialog.Caption = "Loading Multiprice"
            Else
                dialog.Caption = "Multiprice Not Received"
            End If
            If _funMailConfiguration("ER") = True Then
                dialog.Caption = "Loading Mail"
            Else
                dialog.Caption = "Mail Not Received"
            End If
            If getMainGroupPolicy() = True Then
                dialog.Caption = "Loading MainGroupPolicy"
            Else
                dialog.Caption = "MainGroupPolicy Not Received"
            End If
            If ReadCustomerDisplayPole("r") = True Then
                dialog.Caption = "Loading PoleDisplay"
            Else
                dialog.Caption = "PoleDisplay Not Received"
            End If
            If getFingerPrintData() = True Then
                dialog.Caption = "Loading FingerPrintData"
            Else
                dialog.Caption = "FingerPrintData Not Received"
            End If
            If getPackageData() = True Then
                dialog.Caption = "Loading FingerPrintData"
            Else
                dialog.Caption = "FingerPrintData Not Received"
            End If
            Return True
        Catch ex As Exception
            dialog.Close()
            Return False
        Finally
            dialog.Close()
        End Try
    End Function
    Public Function _funMailConfiguration(ByRef ERR As String) As Boolean
        Try
            If File.Exists(M_Details._appPath & "\LayOut\MailConfiguration.xml") Then
                'Dim str() As String = {"Sales", "Guest Print", "Purchase", "Inventory", "Shift Close", "Day Close", "Tax Report", "Payouts"}
                MailProfile._dsMailPrintProfile = New DataSet
                MailProfile._dsMailPrintProfile.ReadXml(M_Details._appPath & "\LayOut\MailConfiguration.xml")
                'For Each _Drows As DataRow In MailProfile._dsMailPrintProfile.Tables(0).Rows
                '    MailConfiguration._myMailID = _Drows("MYMAILID")
                '    MailConfiguration._myPassword = _Drows("MYPASS")
                '    MailConfiguration._myHostID = _Drows("MYHOST")
                '    MailConfiguration._custMailID1 = _Drows("CUSTMAIL1")
                '    MailConfiguration._custMailID2 = _Drows("CUSTMAIL2")
                '    MailConfiguration._custAddress = _Drows("CUSTADDR")
                '    MailConfiguration._custPhone = _Drows("CUSTPHONE")
                'Next
            End If

            Dim link As String = "http://sam.myposqr.com/model/webcrm.php?"
            Dim MailConfig As New DataTable
            MailConfig.TableName = "MailConfiguration"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(link & "WebCrmRequest=204")
            Dim Userparsejson As JObject = JObject.Parse(json)
            Dim dtresults = Userparsejson("Success")
            If dtresults.ToString = "True" Then
                MailConfig = Userparsejson("Data").ToObject(Of DataTable)()
                If MailConfig.Rows.Count > 0 Then
                    MailProfile._dsMailPrintProfile.Tables(0).Rows(0)("MYMAILID") = MailConfig.Rows(0)(0).ToString
                    MailProfile._dsMailPrintProfile.Tables(0).Rows(0)("MYPASS") = MailConfig.Rows(0)(1).ToString
                    MailProfile._dsMailPrintProfile.Tables(0).Rows(0)("MYHOST") = MailConfig.Rows(0)(2).ToString
                    MailProfile._dsMailPrintProfile.AcceptChanges()
                    MailProfile._dsMailPrintProfile.Tables(0).WriteXml(M_Details._appPath & "\LayOut\MailConfiguration.xml", Data.XmlWriteMode.WriteSchema, True)
                End If
            End If
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
    ''' <summary>
    ''' Generates MD5 hash of the input string (same as PHP's md5() function)
    ''' </summary>
    ''' <param name="input">The string to hash</param>
    ''' <returns>MD5 hash as lowercase hex string</returns>
    Public Function GenerateMD5Hash(ByVal input As String) As String
        Try
            Using md5 As MD5 = md5.Create()
                Dim inputBytes As Byte() = Encoding.UTF8.GetBytes(input)
                Dim hashBytes As Byte() = md5.ComputeHash(inputBytes)

                ' Convert byte array to hex string
                Dim sb As New StringBuilder()
                For i As Integer = 0 To hashBytes.Length - 1
                    sb.Append(hashBytes(i).ToString("x2"))
                Next
                Return sb.ToString()
            End Using
        Catch ex As Exception
            MessageBox.Show("Error generating MD5 hash: " & ex.Message)
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Verifies password against stored MD5 hash
    ''' </summary>
    ''' <param name="password">Plain text password to verify</param>
    ''' <param name="storedHash">Stored MD5 hash from database</param>
    ''' <returns>True if password matches, False otherwise</returns>
    Public Function VerifyMD5Password(ByVal password As String, ByVal storedHash As String) As Boolean
        Try
            Dim passwordHash As String = GenerateMD5Hash(password)
            Return String.Equals(passwordHash, storedHash, StringComparison.OrdinalIgnoreCase)
        Catch ex As Exception
            MessageBox.Show("Error verifying password: " & ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Authenticates user with username and password using MD5 hash comparison
    ''' </summary>
    ''' <param name="username">Username</param>
    ''' <param name="password">Plain text password</param>
    ''' <returns>True if authentication successful, False otherwise</returns>
    Public Function AuthenticateUser(ByVal username As String, ByVal password As String) As Boolean
        Try
            If _JsonData.UserTable.Rows.Count > 0 Then
                ' Find user by username using LINQ
                Dim userRows = From row In _JsonData.UserTable.AsEnumerable()
                              Where row.Field(Of String)("UserName").Equals(username, StringComparison.OrdinalIgnoreCase)
                              Select row

                If userRows.Any Then
                    Dim userRow As DataRow = userRows.First()
                    Dim storedPassword As String = userRow.Field(Of String)("Password")
                    _companyInfo.UserId = userRow.Field(Of String)("Id")
                    _companyInfo.UserName = userRow.Field(Of String)("UserName")
                    ' Check if stored password is already hashed (32 characters = MD5 hash)
                    If storedPassword.Length = 32 Then
                        ' Stored password is MD5 hash - compare with hashed input
                        Return VerifyMD5Password(password, storedPassword)
                    Else
                        ' Stored password is plain text - direct comparison
                        Return String.Equals(password, storedPassword, StringComparison.Ordinal)
                    End If
                Else
                    ' User not found
                    Return False
                End If
            End If

            Return False

        Catch ex As Exception
            MessageBox.Show("Error authenticating user: " & ex.Message)
            Return False
        End Try
    End Function
    Public Function AuthenticateUser(ByVal password As String) As Boolean
        Try
            If _JsonData.UserTable.Rows.Count > 0 Then
                ' Find user by username using LINQ
                Dim userRows = From row In _JsonData.UserTable.AsEnumerable()
                              Where row.Field(Of String)("GroupId").Equals("3", StringComparison.OrdinalIgnoreCase) OrElse row.Field(Of String)("GroupId").Equals("4", StringComparison.OrdinalIgnoreCase)
                              Select row

                If userRows.Any Then
                    Dim userRow As DataRow = userRows.First()
                    Dim storedPassword As String = userRow.Field(Of String)("Password")
                    ' Check if stored password is already hashed (32 characters = MD5 hash)
                    If storedPassword.Length = 32 Then
                        ' Stored password is MD5 hash - compare with hashed input
                        Return VerifyMD5Password(password, storedPassword)
                    Else
                        ' Stored password is plain text - direct comparison
                        Return String.Equals(password, storedPassword, StringComparison.Ordinal)
                    End If
                Else
                    ' User not found
                    Return False
                End If
            End If

            Return False

        Catch ex As Exception
            MessageBox.Show("Error authenticating user: " & ex.Message)
            Return False
        End Try
    End Function
    Public Function AuthenticateMasterAdmin(ByVal password As String) As Boolean
        Try
            If _JsonData.UserTable.Rows.Count > 0 Then
                ' Find all users with GroupId "1" or "2" using LINQ
                Dim userRows = From row In _JsonData.UserTable.AsEnumerable()
                              Where row.Field(Of String)("GroupId").Equals("1", StringComparison.OrdinalIgnoreCase) OrElse row.Field(Of String)("GroupId").Equals("2", StringComparison.OrdinalIgnoreCase)
                              Select row

                If userRows.Any Then
                    ' Check password against each user until a match is found
                    For Each userRow As DataRow In userRows
                        Dim storedPassword As String = userRow.Field(Of String)("Password")
                        Dim passwordMatches As Boolean = False

                        ' Check if stored password is already hashed (32 characters = MD5 hash)
                        If storedPassword.Length = 32 Then
                            ' Stored password is MD5 hash - compare with hashed input
                            passwordMatches = VerifyMD5Password(password, storedPassword)
                        Else
                            ' Stored password is plain text - direct comparison
                            passwordMatches = String.Equals(password, storedPassword, StringComparison.Ordinal)
                        End If

                        ' If password matches, return True immediately
                        If passwordMatches Then
                            Return True
                        End If
                    Next

                    ' No matching password found among all admin users
                    Return False
                Else
                    ' No admin users found
                    Return False
                End If
            End If

            Return False

        Catch ex As Exception
            MessageBox.Show("Error authenticating user: " & ex.Message)
            Return False
        End Try
    End Function
    Public Function GetSalesDataFromAPI(AjaxRequest As String, comId As String, locId As String, startDate As String, endDate As String) As DataTable
        Try
            Dim SalesData As New DataTable
            If CheckForInternetConnection() Then
                ' Internet available - fetch from server
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                ' Build the URL properly - check if base URL already has parameters
                Dim baseUrl As String = M_Details.LinkAjaxRequestSyncLocalCloud.TrimEnd("/"c)

                Dim fullUrl As String = baseUrl & "AjaxRequest=" & AjaxRequest & "&comid=" & comId & "&locid=" & locId & "&startDate=" & startDate & "&endDate=" & endDate


                Dim json As String = New System.Net.WebClient().DownloadString(fullUrl)

                ' Handle concatenated JSON responses
                If json.Contains("}{") Then
                    ' Multiple JSON responses concatenated - take the first one
                    Dim firstJsonEnd As Integer = json.IndexOf("}{") + 1
                    json = json.Substring(0, firstJsonEnd)
                End If

                Dim Userparsejson As JObject = JObject.Parse(json)

                ' Check if the response was successful
                If Userparsejson("Success").ToString().ToLower() = "true" Then
                    If Userparsejson("Data") IsNot Nothing Then
                        SalesData = Userparsejson("Data").ToObject(Of DataTable)()
                        Return SalesData
                    End If
                Else
                    ' Show error message from API
                    Dim errorMsg As String = If(Userparsejson("Msg") IsNot Nothing, Userparsejson("Msg").ToString(), "Unknown API error")
                    MessageBox.Show("API Error: " & errorMsg, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
            Return SalesData
        Catch jex As JsonException
            MessageBox.Show("JSON parsing error: " & jex.Message & vbCrLf & "Raw response might be malformed.", "JSON Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show("API call error: " & ex.Message, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function
    Public Function GetSalesMasterDataFromAPI(AjaxRequest As String, comId As String, locId As String, startDate As String, endDate As String) As DataSet
        Try
            Dim dsSales As New DataSet("SalesReport")
            If CheckForInternetConnection() Then
                ' Internet available - fetch from server
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                ' Build the URL properly - check if base URL already has parameters
                Dim baseUrl As String = M_Details.LinkAjaxRequestSyncLocalCloud.TrimEnd("/"c)

                Dim fullUrl As String = baseUrl & "AjaxRequest=" & AjaxRequest & "&comid=" & comId & "&locid=" & locId & "&startDate=" & startDate & "&endDate=" & endDate


                Dim json As String = New System.Net.WebClient().DownloadString(fullUrl)

                ' Handle concatenated JSON responses
                If json.Contains("}{") Then
                    ' Multiple JSON responses concatenated - take the first one
                    Dim firstJsonEnd As Integer = json.IndexOf("}{") + 1
                    json = json.Substring(0, firstJsonEnd)
                End If

                Dim Userparsejson As JObject = JObject.Parse(json)

                ' Check if the response was successful
                If Userparsejson("Success").ToString().ToLower() = "true" Then
                    If Userparsejson("Data") IsNot Nothing Then
                        Dim dataObj As JObject = Userparsejson("Data")

                        Dim dtHeader As New DataTable()
                        Dim dtDetails As New DataTable()
                        Dim dtPayment As New DataTable()

                        If dataObj("Header") IsNot Nothing Then
                            dtHeader = dataObj("Header").ToObject(Of DataTable)()
                        End If

                        If dataObj("Details") IsNot Nothing Then
                            dtDetails = dataObj("Details").ToObject(Of DataTable)()
                        End If

                        If dataObj("Payment") IsNot Nothing Then
                            dtPayment = dataObj("Payment").ToObject(Of DataTable)()
                        End If

                        ' Optional: combine into a single dataset if needed

                        dsSales.Tables.Add(dtHeader)
                        dsSales.Tables.Add(dtDetails)
                        dsSales.Tables.Add(dtPayment)
                    End If
                Else
                    ' Show error message from API
                    Dim errorMsg As String = If(Userparsejson("Msg") IsNot Nothing, Userparsejson("Msg").ToString(), "Unknown API error")
                    MessageBox.Show("API Error: " & errorMsg, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
            Return dsSales
        Catch jex As JsonException
            MessageBox.Show("JSON parsing error: " & jex.Message & vbCrLf & "Raw response might be malformed.", "JSON Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show("API call error: " & ex.Message, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function
End Module
