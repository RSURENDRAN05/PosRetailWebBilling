Imports System.Net
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class StockUpdate
    Dim _ds As DataTable
    Dim OfflineMode As Boolean = False
#Region "ButtonFunction"
    Private Sub chkWebmode_CheckedChanged(sender As Object, e As EventArgs) Handles chkWebmode.CheckedChanged
        Try
            If chkWebmode.Checked = True Then
                OfflineMode = True
            Else
                OfflineMode = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnGenerateStcokList_Click(sender As Object, e As EventArgs) Handles btnGenerateStcokList.Click
        Try
            If OfflineMode Then 'Web Mode
                RecreateLiveStock()
            Else
                If RecreateStockOffline() Then
                    MessageBox.Show("Stock Recreated Successfully", "Stock Create", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                End If
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnresetstock_Click(sender As Object, e As EventArgs) Handles btnresetstock.Click
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnFetchStcok_Click(sender As Object, e As EventArgs) Handles btnFetchStcok.Click
        Try
            If OfflineMode Then 'Web Mode
                GetStockData()
            Else
                Dim dst As New DataSet
                dst = GetStockDataOffline()
                If dst IsNot Nothing AndAlso dst.Tables.Count > 0 AndAlso dst.Tables(0).Rows.Count > 0 AndAlso _JsonData.ItemMasterTable IsNot Nothing Then
                    For Each stockRow As DataRow In dst.Tables(0).Rows
                        For Each itemRow As DataRow In _JsonData.ItemMasterTable.Rows
                            If String.Equals(Convert.ToString(itemRow("ITEMCODE")), Convert.ToString(stockRow("Id")), StringComparison.OrdinalIgnoreCase) Then
                                stockRow("BarCode") = itemRow("BARCODE")
                                stockRow("ItemName") = itemRow("ITEMNAME")
                                stockRow("CompanyName") = _companyInfo.CompanyName
                                stockRow("LocationName") = _companyInfo.LocationName
                                Exit For
                            End If
                        Next
                    Next
                    GridControl1.DataSource = dst.Tables(0)
                Else
                    GridControl1.DataSource = Nothing
                End If
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnUpdateStock_Click(sender As Object, e As EventArgs) Handles btnUpdateStcok.Click
        Try
            If OfflineMode Then 'Web Mode
                UpdateStock()
            Else

            End If
        Catch ex As Exception
            MSgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
#End Region
#Region "WebStock"
    Private Sub UpdateStock()
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
#End Region
#Region "OfflineStock"
    Public Function RecreateStockOffline() As Boolean
        Try
            If _JsonData.ItemMasterTable.Rows.Count > 0 Then
                For Each rows In _JsonData.ItemMasterTable.Rows
                    ' Process each row
                    Dim stockupdate As New StockUpdateParams
                    stockupdate.Mode = "Recreate"
                    stockupdate.ItemCode = rows("ITEMCODE").ToString()
                    stockupdate.ComId = _companyInfo.ComId
                    stockupdate.LocId = _companyInfo.LocId
                    stockupdate.OpStock = 0D
                    stockupdate.StockIn = rows("LIVESTOCK").ToString()
                    stockupdate.StockOut = 0D
                    stockupdate.LiveStock = rows("LIVESTOCK").ToString()
                    ' Call the stock update function    
                    UpdateStockOffline(stockupdate)
                Next
            End If
            Return True
        Catch ex As Exception
            MsgBox("Error in Recreating Stock Offline: " & ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try
    End Function
    Public Function UpdateStockOffline(stockupdate As StockUpdateParams) As Boolean
        Try
            ' Implement the logic to update stock offline
            ' This is a placeholder for the actual implementation
            ' You would typically update a local database or data structure here
            Using SqlConnection As New SqlConnection(M_Details._Conn)
                Dim cmd As New SqlClient.SqlCommand("sp_upsert_livestock", SqlConnection)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Mode", stockupdate.Mode)
                cmd.Parameters.AddWithValue("@pl_itemcode", stockupdate.ItemCode)
                cmd.Parameters.AddWithValue("@pl_comid", stockupdate.ComId)
                cmd.Parameters.AddWithValue("@pl_locid", stockupdate.LocId)
                cmd.Parameters.AddWithValue("@pl_opstok", stockupdate.OpStock)
                cmd.Parameters.AddWithValue("@pl_stockin", stockupdate.StockIn)
                cmd.Parameters.AddWithValue("@pl_stockout", stockupdate.StockOut)
                cmd.Parameters.AddWithValue("@pl_livestock", stockupdate.LiveStock)
                SqlConnection.Open()
                cmd.ExecuteNonQuery()
                SqlConnection.Close()
            End Using
            Return True
        Catch ex As Exception
            MsgBox("Error in Updating Stock Offline: " & ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try
    End Function
    Public Function GetStockDataOffline() As DataSet
        Try
            Dim _DsStockTable As New DataSet
            Dim stockupdate As New StockUpdateParams
            stockupdate.Mode = "GetStock"
            stockupdate.ItemCode = String.Empty
            stockupdate.ComId = _companyInfo.ComId
            stockupdate.LocId = _companyInfo.LocId
            stockupdate.OpStock = 0D
            stockupdate.StockIn = 0D
            stockupdate.StockOut = 0D
            stockupdate.LiveStock = 0D
            ' Call the stock update function    
            _DsStockTable = GetStockDatasetOffline(stockupdate)
            Return _DsStockTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GetStockDatasetOffline(stockupdate As StockUpdateParams) As DataSet
        Try
            Dim ds As New DataSet
            Dim sql(7) As SqlParameter
            sql(0) = New SqlParameter("@Mode", stockupdate.Mode)
            sql(1) = New SqlParameter("@pl_itemcode", stockupdate.ItemCode)
            sql(2) = New SqlParameter("@pl_comid", stockupdate.ComId)
            sql(3) = New SqlParameter("@pl_locid", stockupdate.LocId)
            sql(4) = New SqlParameter("@pl_opstok", stockupdate.OpStock)
            sql(5) = New SqlParameter("@pl_stockin", stockupdate.StockIn)
            sql(6) = New SqlParameter("@pl_stockout", stockupdate.StockOut)
            sql(7) = New SqlParameter("@pl_livestock", stockupdate.LiveStock)
            ds = _sqlDataAdapter2("sp_upsert_livestock", sql)
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region



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

Public Class StockUpdateParams
    Public Property Mode As String
    Public Property ItemCode As String
    Public Property ComId As Integer
    Public Property LocId As Integer
    Public Property OpStock As Decimal = 0D
    Public Property StockIn As Decimal = 0D
    Public Property StockOut As Decimal = 0D
    Public Property LiveStock As Decimal = 0D
End Class
