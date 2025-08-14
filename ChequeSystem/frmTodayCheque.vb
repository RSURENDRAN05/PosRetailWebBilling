Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class frmTodayCheque
    Dim ds As DataTable
    Private Sub frmTodayCheque_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            bardate.EditValue = Date.Now.ToString("dd/MM/yyyy")
            barbtntodaycheque_ItemClick(Nothing, Nothing)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtntodaycheque_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtntodaycheque.ItemClick
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            Dim retfrdate As String = ""
            Dim rettodate As String = ""
            _DateConversion(bardate.EditValue, bardate.EditValue, retfrdate, rettodate)
            ds = New DataTable
            ds.TableName = "DataLoad"
            dialog.Caption = "Collecting Data From Server"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequestCheque & "AjaxRequest=13&frdate=" & retfrdate)
            Dim Userparsejson As JObject = JObject.Parse(json)

            Dim Msg = Userparsejson("Success").ToString
            If Msg.ToString = "True" Then
                dialog.Caption = "Data Received.."
                ds = Userparsejson("Data").ToObject(Of DataTable)()
                If ds.Rows.Count > 0 Then
                    GridControl1.DataSource = ds.DefaultView
                Else
                    GridControl1.DataSource = Nothing
                End If

            End If

        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub
End Class