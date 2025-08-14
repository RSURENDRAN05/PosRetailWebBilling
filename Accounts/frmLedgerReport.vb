
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraBars.Ribbon
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.DataTable
Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class frmLedgerReport
    Private r As Ledgerreport
    Private _dataSet As DataTable
    Private Errorstr As String = String.Empty
    Private caption As String = "Journal Report"
    Private Sub frmLedgerReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            r = New Ledgerreport
            If File.Exists(M_Details.AppPath & "\Reports\ledgerReport.repx") Then
                r.LoadLayout(M_Details.AppPath & "\Reports\ledgerReport.repx")
            End If
            PrintControlJournalrpt.PrintingSystem = r.PrintingSystem
            r.CreateDocument()
            frmDate.EditValue = Date.Now
            ToDate.EditValue = Date.Now
            Dim dsex As New DataTable
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxPayRec=4")
            Dim Userparsejson As JObject = JObject.Parse(json)
            dsex = Userparsejson("BankListTable").ToObject(Of DataTable)()
            If dsex.Rows.Count > 0 Then
                CmbHeader.Properties.DataSource = dsex
                CmbHeader.Properties.ValueMember = "HEAD_ID"
                CmbHeader.Properties.DisplayMember = "HEAD_NAME"
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Function Getdata(ByRef ledgerId As String) As DataTable
        Try
            Dim fromdate As String = ""
            Dim todated As String = ""
            _DateConversion(frmDate.EditValue, frmDate.EditValue, fromdate)
            _DateConversion(ToDate.EditValue, ToDate.EditValue, todated)

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxPayRec=5&fromdate=" & fromdate & "&todate=" & todated & "&ledgerid=" & CmbHeader.EditValue)
            Dim Userparsejson As JObject = JObject.Parse(json)
            _dataSet = Userparsejson("BankListTable").ToObject(Of DataTable)()
            If _dataSet.Rows.Count > 0 Then
                Return _dataSet
            End If
            Return _dataSet
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If ToDate.EditValue Is Nothing Then
                ToDate.Focus()
                Exit Sub
            ElseIf frmDate.EditValue Is Nothing Then

                frmDate.Focus()
                Exit Sub
            End If
            Dim dt As New DataTable

            If Getdata(Errorstr) IsNot Nothing Then
                r = New Ledgerreport
                If File.Exists(M_Details.AppPath & "\Reports\ledgerReport.repx") Then
                    r.LoadLayout(M_Details.AppPath & "\Reports\ledgerReport.repx")
                End If
                Dim DatTab As New DataTable

                DatTab = _dataSet.Copy
                DatTab.TableName = "DataTable1"
                If File.Exists(M_Details.AppPath & "\Reports\ledgerReport.xml") Then

                    DatTab.WriteXml(M_Details.AppPath & "\Reports\ledgerReport.xml", XmlWriteMode.WriteSchema, True)
                Else
                    DatTab.WriteXml(M_Details.AppPath & "\Reports\ledgerReport.xml", XmlWriteMode.WriteSchema, True)
                End If
                r.DataSource = _dataSet '.Tables(0)
                PrintControlJournalrpt.PrintingSystem = r.PrintingSystem
                r.CreateDocument()
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show(Errorstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class