Imports System.Net
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.Xml
Imports System.Text
Imports System.Security.Cryptography
Imports DevExpress.XtraEditors
Imports System.Globalization
Imports Microsoft.Win32
Imports System.IO

Module chequeModule
   
    Public Structure ChqJson
        Public Shared BankTable As DataTable
        Public Shared BankTableStatement As DataTable
        Public Shared BankTableSave As DataTable
        Public Shared CompanyTable As DataTable
        Public Shared DefaultTableSave As DataTable
        Public Shared PayeeTable As DataTable
    End Structure

    Public Function CreateTablePayeeTable() As DataTable
        Try
            ChqJson.PayeeTable = New DataTable
            ChqJson.PayeeTable.TableName = "PayeeName"
            ChqJson.PayeeTable.Columns.Add("Id", GetType(Integer))
            ChqJson.PayeeTable.Columns("Id").AutoIncrement = True

            'Set the Starting or Seed value.
            ChqJson.PayeeTable.Columns("Id").AutoIncrementSeed = 1

            'Set the Increment value.
            ChqJson.PayeeTable.Columns("Id").AutoIncrementStep = 1
            ChqJson.PayeeTable.Columns.Add("PayeeName", GetType(String))
            ChqJson.PayeeTable.Columns.Add("Active", GetType(String))
            'If File.Exists(M_Details.AppPath & "\Settings\PayeeName.xml") Then
            '    ChqJson.PayeeTable.ReadXml(M_Details.AppPath & "\Settings\PayeeName.xml")
            'Else
            '    ChqJson.PayeeTable.WriteXml(M_Details.AppPath & "\Settings\PayeeName.xml")
            'End If
            Return ChqJson.PayeeTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function CreateCompanyTable() As DataTable
        Try

            ChqJson.CompanyTable = New DataTable
            ChqJson.CompanyTable.TableName = "CompanyTable"
            ChqJson.CompanyTable.Columns.Add("COID", GetType(Integer))
            ChqJson.CompanyTable.Columns("COID").AutoIncrement = True

            'Set the Starting or Seed value.
            ChqJson.CompanyTable.Columns("COID").AutoIncrementSeed = 1

            'Set the Increment value.
            ChqJson.CompanyTable.Columns("COID").AutoIncrementStep = 1
            ChqJson.CompanyTable.Columns.Add("CompanyName", GetType(String))
            ChqJson.CompanyTable.Columns.Add("Active", GetType(Boolean))
            'If File.Exists(M_Details.AppPath & "\Settings\CompanyTable.xml") Then
            '    ChqJson.CompanyTable.ReadXml(M_Details.AppPath & "\Settings\CompanyTable.xml")
            'Else
            '    ChqJson.CompanyTable.WriteXml(M_Details.AppPath & "\Settings\CompanyTable.xml")
            'End If
            Return ChqJson.CompanyTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function CreateTableBankName() As DataTable
        Try
            ChqJson.BankTable = New DataTable
            ChqJson.BankTable.TableName = "BankName"
            ChqJson.BankTable.Columns.Add("Id", GetType(Integer))
            ChqJson.BankTable.Columns("Id").AutoIncrement = True

            'Set the Starting or Seed value.
            ChqJson.BankTable.Columns("Id").AutoIncrementSeed = 1

            'Set the Increment value.
            ChqJson.BankTable.Columns("Id").AutoIncrementStep = 1
            ChqJson.BankTable.Columns.Add("BankName", GetType(String))
            ChqJson.BankTable.Columns.Add("Active", GetType(Boolean))
            If File.Exists(M_Details.AppPath & "\Settings\BankList.xml") Then
                ChqJson.BankTable.ReadXml(M_Details.AppPath & "\Settings\BankList.xml")
            Else
                ChqJson.BankTable.WriteXml(M_Details.AppPath & "\Settings\BankList.xml")
            End If
            Return ChqJson.BankTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CreateTableBankTableStatement() As DataTable
        Try
            ChqJson.BankTableStatement = New DataTable
            ChqJson.BankTableStatement.TableName = "BankNameStatement"
            ChqJson.BankTableStatement.Columns.Add("Id", GetType(Integer))
            ChqJson.BankTableStatement.Columns("Id").AutoIncrement = True

            'Set the Starting or Seed value.
            ChqJson.BankTableStatement.Columns("Id").AutoIncrementSeed = 1

            'Set the Increment value.
            ChqJson.BankTableStatement.Columns("Id").AutoIncrementStep = 1
            ChqJson.BankTableStatement.Columns.Add("BankName", GetType(String))
            ChqJson.BankTableStatement.Columns.Add("PayeeName", GetType(String))
            ChqJson.BankTableStatement.Columns.Add("PayeeDate", GetType(String))
            ChqJson.BankTableStatement.Columns.Add("PayeeAmount", GetType(String))
            ChqJson.BankTableStatement.Columns.Add("PayeeWords", GetType(String))
            If File.Exists(M_Details.AppPath & "\Settings\BankTableStatement.xml") Then
                ChqJson.BankTableStatement.ReadXml(M_Details.AppPath & "\Settings\BankTableStatement.xml")
            Else
                ChqJson.BankTableStatement.WriteXml(M_Details.AppPath & "\Settings\BankTableStatement.xml")
            End If
            Return ChqJson.BankTableStatement
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'Public Function CreateTableBankTableSave() As DataTable
    '    Try
    '        ChqJson.BankTableSave = New DataTable
    '        ChqJson.BankTableSave.TableName = "BankNameStatement"
    '        ChqJson.BankTableSave.Columns.Add("Id", GetType(Integer))
    '        ChqJson.BankTableSave.Columns("Id").AutoIncrement = True

    '        'Set the Starting or Seed value.
    '        ChqJson.BankTableSave.Columns("Id").AutoIncrementSeed = 1

    '        'Set the Increment value.
    '        ChqJson.BankTableSave.Columns("Id").AutoIncrementStep = 1
    '        ChqJson.BankTableSave.Columns.Add("CompanyName", GetType(String))
    '        ChqJson.BankTableSave.Columns.Add("PayeeChq", GetType(String))
    '        ChqJson.BankTableSave.Columns.Add("BankName", GetType(String))
    '        ChqJson.BankTableSave.Columns.Add("PayeeName", GetType(String))
    '        ChqJson.BankTableSave.Columns.Add("PayeeDate", GetType(Date))
    '        ChqJson.BankTableSave.Columns.Add("PayeeAmount", GetType(String))
    '        ChqJson.BankTableSave.Columns.Add("PayeeStatus", GetType(String)).DefaultValue = 2
    '        'If File.Exists(M_Details.AppPath & "\Settings\BankTableSave.xml") Then
    '        '    ChqJson.BankTableSave.ReadXml(M_Details.AppPath & "\Settings\BankTableSave.xml")
    '        'Else
    '        '    ChqJson.BankTableSave.WriteXml(M_Details.AppPath & "\Settings\BankTableSave.xml")
    '        'End If
    '        Return ChqJson.BankTableSave
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function
    Public Function getChqComapnyInfo() As Boolean
        Try
            ChqJson.CompanyTable.TableName = "ComapnyTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequestCheque & "AjaxRequest=2")
            Dim Userparsejson As JObject = JObject.Parse(json)
            ChqJson.CompanyTable = Userparsejson("Company").ToObject(Of DataTable)()
            If ChqJson.CompanyTable.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function _JsonSendChq(ByRef _val As String) As Boolean
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(_val)
            Dim Userparsejson As JObject = JObject.Parse(json)
            Dim dtresults = Userparsejson("Success")
            Dim res = Userparsejson("Data")
            If dtresults.ToString = "True" Then
                ' XtraMessageBox.Show("Data Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return True
            Else
                '  XtraMessageBox.Show("Data Not Saved", "Msg", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function
    Public Function getPayeeInfo() As Boolean
        Try
            ChqJson.PayeeTable.TableName = "PayeeTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequestCheque & "AjaxRequest=6")
            Dim Userparsejson As JObject = JObject.Parse(json)

            Dim Msg = Userparsejson("Success").ToString
            If Msg.ToString = "True" Then
                ChqJson.PayeeTable = Userparsejson("Data").ToObject(Of DataTable)()
                If ChqJson.PayeeTable.Rows.Count > 0 Then
                    Return True
                End If
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function getBankSatement() As Boolean
        Try
            ChqJson.BankTableSave = New DataTable
            ChqJson.BankTableSave.TableName = "BankSatement"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequestCheque & "AjaxRequest=9")
            Dim Userparsejson As JObject = JObject.Parse(json)

            Dim Msg = Userparsejson("Success").ToString
            If Msg.ToString = "True" Then
                ChqJson.BankTableSave = Userparsejson("Data").ToObject(Of DataTable)()
                If ChqJson.BankTableSave.Rows.Count > 0 Then
                    Return True
                End If
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
End Module
