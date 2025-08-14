Imports System
Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.Security.Cryptography
Imports System.Management
Imports Microsoft.Win32
Imports System.Net

Public Class clsRegisterKey

    Private key() As Byte = {}
    Private IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
    Private Const EncryptionKey As String = "Ruthram@1986"

    Public Function Decrypt(ByVal stringToDecrypt As String) As String
        Try
            Dim inputByteArray(stringToDecrypt.Length) As Byte
            key = System.Text.Encoding.UTF8.GetBytes(Left(EncryptionKey, 8))
            Dim des As New DESCryptoServiceProvider
            inputByteArray = Convert.FromBase64String(stringToDecrypt)
            Dim ms As New MemoryStream
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
            Return encoding.GetString(ms.ToArray())
        Catch ex As Exception
            'oops - add your exception logic
        End Try
        Return Nothing
    End Function

    Public Function Encrypt(ByVal stringToEncrypt As String) As String
        Try
            key = System.Text.Encoding.UTF8.GetBytes(Left(EncryptionKey, 8))
            Dim des As New DESCryptoServiceProvider
            Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(stringToEncrypt)
            Dim ms As New MemoryStream
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Return Convert.ToBase64String(ms.ToArray())
        Catch ex As Exception
            'oops - add your exception logic
        End Try
        Return Nothing
    End Function


    'Public Function getHardDiskSerialNo() As String
    '    Try
    '        Dim HDD_Serial As String
    '        HDD_Serial = ""
    '        Dim hdd As New ManagementObjectSearcher("select * from Win32_DiskDrive")
    '        For Each hd In hdd.Get
    '            HDD_Serial = hd("SerialNumber")
    '        Next
    '        Return HDD_Serial
    '    Catch ex As Exception
    '        DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, Version, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return 0
    '    End Try
    'End Function
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
    Public Function getComputerName() As String
        Return System.Net.Dns.GetHostName()
    End Function


    Public Function getSystemIPAddress() As String
        'Return Convert.ToString(Dns.GetHostByName(getComputerName()).AddressList(0).ToString())
        Return 0
    End Function

    Public Function getInstallDate() As String
        Return String.Format("{0:00}", Now.Date.Day) & String.Format("{0:00}", Now.Date.Month) & String.Format("{0:00}", Now.Date.Year)
    End Function



    Public Function generateRegistery(ByVal _sysName As String, ByVal _hdd_serial As String, ByVal _tDate As String, ByVal isTrial As Boolean, ByRef msgText As String) As Boolean
        Try
            Dim iTrial As Integer
            Dim licenseChar As String
            Dim regEncrpt As String
            Dim regDcrpt As String
            Dim arreg As String()

            Dim sysName As String
            Dim hdd_serial As String
            Dim tDate As String
            Dim tDate1 As String

            Dim regVersion As RegistryKey
            sysName = _sysName
            hdd_serial = _hdd_serial
            tDate = _tDate

            regVersion = Registry.CurrentUser.OpenSubKey("SOFTWARE\\osbin\\v2", True)
            If isTrial = True Then
                licenseChar = "T"
            Else
                licenseChar = "A"
            End If
            regEncrpt = ""
            If regVersion Is Nothing Then
TrailActive:    regVersion = Registry.CurrentUser.CreateSubKey("SOFTWARE\\osbin\\v2")
                If licenseChar = "T" Then
                    iTrial = 15
                    regEncrpt = licenseChar & "-" & tDate & "-" & hdd_serial & "-" & iTrial
                    msgText = "Successfully Trial Version Activated. "
                Else
                    regEncrpt = licenseChar & "-" & tDate & "-" & hdd_serial
                    msgText = "Successfully Activation Completed. "
                End If
                regVersion.SetValue("POSBean License", Encrypt(regEncrpt))
                regVersion.Close()

                Return True
            Else
                regDcrpt = Decrypt(regVersion.GetValue("POSBean License"))
                If regDcrpt Is Nothing Then
                    GoTo TrailActive
                End If
                arreg = regDcrpt.Split("-")
                ' licenseChar = arreg(0)
                tDate = arreg(1)
                hdd_serial = arreg(2)
                tDate1 = getInstallDate()
                If licenseChar = "T" Then
                    iTrial = arreg(3)
                    If setTrialDate(licenseChar, tDate1, hdd_serial, tDate, iTrial, msgText) = False Then
                        Return False
                    End If
                Else
                    If setActiveDate(licenseChar, tDate1, hdd_serial, tDate, msgText) = False Then
                        Return False
                    End If
                End If
                regVersion.Close()
                Return True
            End If
            Return False
        Catch ex As Exception
            msgText = "License Generating Error :" + ex.Message
            Return False
        End Try

    End Function


    Public Function setTrialDate(ByVal licenseChar As String, ByVal tdate As String, ByVal hdd_serial As String, ByVal preDate As String, ByVal iTrial As Integer, ByRef msgText As String) As Boolean
        Try
            Dim regEncrpt As String
            Dim pdate As DateTime
            pdate = New DateTime(Int32.Parse(preDate.Substring(4, 4)), Int32.Parse(preDate.Substring(2, 2)), Int32.Parse(preDate.Substring(0, 2)))
            '  Dim iday As Integer
            ' iday = DateDiff(DateInterval.Day, pdate, Now.Date)
            If pdate <> Now.Date Then
                iTrial = iTrial - 1
            End If

            'If iday > 0 Then
            'iTrial = iTrial - iday
            'End If
            If iTrial < 1 Then
                msgText = "Your trial license has expired."
                Return False
            Else
                regEncrpt = licenseChar & "-" & tdate & "-" & hdd_serial & "-" & iTrial
                Dim regVer As RegistryKey
                regVer = Registry.CurrentUser.OpenSubKey("SOFTWARE\\osbin\\v2", True)
                regVer.SetValue("POSBean License", Encrypt(regEncrpt))
                regVer.Close()
                msgText = "Successfully Activated Trial Version"
                Return True
            End If

        Catch ex As Exception
            msgText = "Trial Updating Error: " + ex.Message.ToString()
            Return False
        End Try
    End Function

    Public Function setActiveDate(ByVal licenseChar As String, ByVal tdate As String, ByVal hdd_serial As String, ByVal preDate As String, ByRef msgText As String) As Boolean
        Dim regEncrpt As String
        Try
            regEncrpt = licenseChar & "-" & tdate & "-" & hdd_serial
            Dim regVer As RegistryKey
            regVer = Registry.CurrentUser.OpenSubKey("SOFTWARE\\osbin\\v2", True)
            regVer.SetValue("POSBean License", Encrypt(regEncrpt))
            regVer.Close()
            msgText = "Successfully Activation Completed."
            Return True
        Catch ex As Exception
            msgText = "Activation Error :" + ex.Message.ToString()
            Return False
        End Try
    End Function

End Class
