Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO

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
            dialog.Caption = "Getting User Information"
            If getUserInfo() = True Then
                dialog.Caption = "User Data Received"
            Else
                dialog.Caption = "User Data Not Received"
            End If
            dialog.Caption = "Getting Company Information"
            If getComapnyLocationInfo() = True Then
                dialog.Caption = "Company Data Received"
            Else
                dialog.Caption = "Company Data Not Received"
            End If
            If getComapnyInfo() = True Then
                dialog.Caption = "Company Data Received"

            Else
                dialog.Caption = "Company Data Not Received"
            End If
            dialog.Caption = "Getting Location Information"
            If getLocationInfo() = True Then
                dialog.Caption = "Location Data Received"
            Else
                dialog.Caption = "Location Data Not Received"
            End If
            If getPosSettingsInfo() = True Then
                LoadPosSettings()
                dialog.Caption = "Loading Pos Settings"
            Else
                dialog.Caption = "Pos Setting Data Not Received"
            End If
            If getSalesManCommissionInfo() = True Then

                dialog.Caption = "Loading SalesCommission"
            Else
                dialog.Caption = "SalesCommission Data Not Received"
            End If
            If getPaymentTermTable() = True Then
                dialog.Caption = "Loading Payment Term"
            Else
                dialog.Caption = "Payment Term Data Not Received"
            End If
            If getCategoryMaster() = True Then
                dialog.Caption = "Loading CategoryMaster"
            Else
                dialog.Caption = "CategoryMaster Data Not Received"
            End If
            If getMainMaster() = True Then
                dialog.Caption = "Loading MainMaster"
            Else
                dialog.Caption = "MainMaster Data Not Received"
            End If
            If getItemMaster() = True Then
                dialog.Caption = "Loading ItemMaster"
            Else
                dialog.Caption = "ItemMaster Data Not Received"
            End If
            If getTouchItemMaster() = True Then
                dialog.Caption = "Loading TouchItemMaster"
            Else
                dialog.Caption = "TouchItemMaster Data Not Received"
            End If
            Return True
        Catch ex As Exception
            dialog.Close()
            Return False
        Finally
            dialog.Close()
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

End Module
