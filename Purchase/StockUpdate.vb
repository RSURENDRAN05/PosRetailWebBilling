Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class StockUpdate
    Dim _ds As DataTable
    Private Sub btnGenerateStcokList_Click(sender As Object, e As EventArgs) Handles btnGenerateStcokList.Click
        Try
            RecreateLiveStock()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub RecreateLiveStock()
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            If CheckForInternetConnection() Then
                dialog.Caption = "Recreating Live Stock Data"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=76&comid=" & _companyInfo.ComId & "&locid=" & _companyInfo.LocId)
                Dim Userparsejson As JObject = JObject.Parse(json)
                Dim status As Boolean = Userparsejson("Status").ToObject(Of Boolean)()
                If status = True Then
                    MsgBox("Live Stock Recreated Successfully", MsgBoxStyle.Information, M_Details.SoftwareVersion)
                Else
                    MsgBox("Error in Recreating Live Stock", MsgBoxStyle.Critical, M_Details.SoftwareVersion)
                End If
            End If
        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub
    Private Sub GetStockData()
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            If CheckForInternetConnection() Then
                _ds = New DataTable
                dialog.Caption = "Connecting Data"
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=75&comid=" & _companyInfo.ComId & "&locid=" & _companyInfo.LocId)
                Dim Userparsejson As JObject = JObject.Parse(json)
                _ds = Userparsejson("Data").ToObject(Of DataTable)()
                If _ds.Rows.Count > 0 Then
                    GridControl1.DataSource = _ds
                    dialog.Caption = "Getting Data"
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

    Private Sub btnFetchStcok_Click(sender As Object, e As EventArgs) Handles btnFetchStcok.Click
        Try
            GetStockData()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnUpdateStock_Click(sender As Object, e As EventArgs) Handles btnUpdateStcok.Click
        Try
            ' Only collect rows with RequiredQty > 0
            Dim stockList As New List(Of StockItem)
            For Each row As DataRow In _ds.Rows
                If Convert.ToDecimal(row("RequiredQty")) > 0D Then
                    Dim stockItem As New StockItem With {
                        .Id = Convert.ToInt32(row("Id")),
                        .BarCode = row("BarCode").ToString(),
                        .ItemName = row("ItemName").ToString(),
                        .CompanyName = row("CompanyName").ToString(),
                        .LocationName = row("LocationName").ToString(),
                        .OpStock = Convert.ToDecimal(row("OpStock")),
                        .StockIn = Convert.ToDecimal(row("StockIn")),
                        .StockOut = Convert.ToDecimal(row("StockOut")),
                        .CurStock = Convert.ToDecimal(row("CurStock")),
                        .RequiredQty = Convert.ToDecimal(row("RequiredQty"))
                    }
                    stockList.Add(stockItem)
                End If
            Next

            If stockList.Count = 0 Then
                MsgBox("No stock to update.", MsgBoxStyle.Information)
                Return
            End If

            ' Serialize to JSON
            Dim jsonData As String = Newtonsoft.Json.JsonConvert.SerializeObject(stockList)

            ' Send POST request
            Dim postData As New System.Collections.Specialized.NameValueCollection()
            postData.Add("AjaxRequest", "77")
            postData.Add("comid", _companyInfo.ComId)
            postData.Add("locid", _companyInfo.LocId)
            postData.Add("stockdata", jsonData)

            Using dialog As New DevExpress.Utils.WaitDialogForm("Updating Live Stock Data...", "Please wait")
                Using client As New System.Net.WebClient()
                    client.Headers(HttpRequestHeader.ContentType) = "application/x-www-form-urlencoded"
                    Dim responseBytes As Byte() = client.UploadValues(M_Details.LinkAjaxRequest, "POST", postData)
                    Dim responseJson As String = System.Text.Encoding.UTF8.GetString(responseBytes)
                    Dim jsonResp As JObject = JObject.Parse(responseJson)
                    Dim status As Boolean = jsonResp("Success").ToObject(Of Boolean)()
                    If status Then
                        MsgBox("Live Stock Updated Successfully", MsgBoxStyle.Information, M_Details.SoftwareVersion)
                    Else
                        MsgBox("Error in Updating Live Stock: " & jsonResp("Msg").ToString(), MsgBoxStyle.Critical, M_Details.SoftwareVersion)
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

End Class
Public Class StockItem
    Public Property Id As Integer
    Public Property BarCode As String
    Public Property ItemName As String
    Public Property CompanyName As String
    Public Property LocationName As String
    Public Property OpStock As Decimal
    Public Property StockIn As Decimal
    Public Property StockOut As Decimal
    Public Property CurStock As Decimal
    Public Property RequiredQty As Decimal = 0D  ' default 0
End Class
