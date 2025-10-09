Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class FrmPackageItem
    Dim dtPackageItems As DataTable
    Dim dtSellItems As DataTable
    Dim itemId As Integer = 0
    Dim itemName As String = String.Empty
    Public Overloads Sub ShowDialogData(ByRef PackId As Integer, ByRef PackName As String)
        Try
            itemId = PackId
            itemName = PackName
            txtpackid.Text = itemId
            txtpackname.Text = itemName
            MyBase.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
    Private Function GetPackageItemDataTable() As DataTable
        Dim dt As New DataTable()

        dt.Columns.Add("Id", GetType(Integer))
        dt.Columns.Add("PackageId", GetType(Integer))
        dt.Columns.Add("ItemId", GetType(Integer))
        dt.Columns.Add("ItemName", GetType(String))
        dt.Columns.Add("ItemPrice", GetType(Decimal))
        dt.Columns.Add("ItemActive", GetType(Boolean))

        Return dt
    End Function
    Private Function GetSellItemDataTable() As DataTable
        Dim dt As New DataTable()

        dt.Columns.Add("Id", GetType(Integer))
        dt.Columns.Add("ItemName", GetType(String))
        dt.Columns.Add("SellPrice", GetType(Decimal))

        Return dt
    End Function

    Private Sub FrmPackageItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            dtPackageItems = GetPackageItemDataTable()
            dtSellItems = GetSellItemDataTable()
            GridControlItemList.DataSource = dtSellItems
            GridControlPackageList.DataSource = dtPackageItems
            Dataload()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Dataload()
        Try
            If _JsonData.ItemMasterTable.Rows.Count > 0 Then
                dtSellItems = New DataTable
                dtSellItems = _JsonData.ItemTouchMasterTable
                GridControlItemList.DataSource = dtSellItems
                dtSellItems.AcceptChanges()
                GetPackageItems(txtpackid.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridControlItemList_DoubleClick(sender As Object, e As EventArgs) Handles GridControlItemList.DoubleClick
        Try

            Dim _itemId = GridViewItemList.GetFocusedRowCellValue("Id")
            Dim _itemName = GridViewItemList.GetFocusedRowCellValue("ItemName")
            Dim _itemPrice = GridViewItemList.GetFocusedRowCellValue("SellPrice")
            dtPackageItems.BeginInit()
            dtPackageItems.Rows.Add(0, txtpackid.Text, _itemId, _itemName, _itemPrice, True)
            dtPackageItems.AcceptChanges()
            GridControlPackageList.DataSource = dtPackageItems
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim packageItems As New List(Of PackageItem)()
            If dtPackageItems.Rows.Count > 0 Then
                For Each row As DataRow In dtPackageItems.Rows
                    Dim item As New PackageItem() With {
                        .Id = Convert.ToInt32(row("Id")),
                        .PackageId = Convert.ToInt32(row("PackageId")),
                        .ItemId = Convert.ToInt32(row("ItemId")),
                        .ItemName = Convert.ToString(row("ItemName")),
                        .ItemPrice = Convert.ToDecimal(row("ItemPrice")),
                        .ItemActive = Convert.ToBoolean(row("ItemActive"))
                    }
                    packageItems.Add(item)
                Next

                ' Serialize the list to JSON
                Dim jsonData As String = Newtonsoft.Json.JsonConvert.SerializeObject(packageItems)

                ' Send the JSON data to the server
                If PostDataToServer(M_Details.LinkAjaxRequest & "MenuRequest=14", jsonData) Then
                    ' Handle the server response
                    ' Assuming the server returns a JSON response like:
                    MessageBox.Show("Package items saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    GetPackageItems(txtpackid.Text)
                Else
                    MessageBox.Show("No package items to save.  add some items.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function PostDataToServer(url As String, jsonData As String) As Boolean
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            ' Create WebClient instance
            Using client As New System.Net.WebClient()
                ' Set content type header
                client.Headers(HttpRequestHeader.ContentType) = "application/json"

                ' Post data and get response
                Dim response As String = client.UploadString(url, jsonData)

                ' Parse response
                Dim responseObj As JObject = JObject.Parse(response)

                ' Return success status
                Return responseObj("Success").ToObject(Of Boolean)()
            End Using
        Catch ex As Exception
            MessageBox.Show("Error posting data to server: " & ex.Message)
            Return False  ' Return false on error, not String.Empty
        End Try
    End Function
    Private Sub GetPackageItems(ByVal itemId As Integer)
        Try
            Dim url As String = M_Details.LinkAjaxRequest & "MenuRequest=13&PackageId=" & itemId
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            ' Create WebClient instance
            Using client As New System.Net.WebClient()
                ' Set content type header
                client.Headers(HttpRequestHeader.ContentType) = "application/json"

                ' Get data from server
                Dim response As String = client.DownloadString(url)

                ' Parse response
                Dim responseObj As JObject = JObject.Parse(response)

                ' Check if the response indicates success
                If responseObj("Success").ToObject(Of Boolean)() Then
                    ' Create JSON serializer settings with custom converters
                    Dim settings As New Newtonsoft.Json.JsonSerializerSettings()
                    settings.Converters.Add(New BooleanConverter())

                    ' Deserialize the package items from the response with custom settings
                    Dim packageItems As List(Of PackageItem) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of PackageItem))(responseObj("Data").ToString(), settings)

                    ' Clear existing rows in the DataTable
                    dtPackageItems.Rows.Clear()

                    ' Populate the DataTable with the retrieved package items
                    For Each item As PackageItem In packageItems
                        dtPackageItems.Rows.Add(item.Id, item.PackageId, item.ItemId, item.ItemName, item.ItemPrice, item.ItemActive)
                    Next

                    dtPackageItems.AcceptChanges()
                    GridControlPackageList.DataSource = dtPackageItems
                Else
                    MessageBox.Show("Failed to retrieve package items from server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving package items: " & ex.Message)
        End Try
    End Sub
End Class
'Public Property itemid As String
Public Class PackageItem
    Public Property Id As Integer
    Public Property PackageId As Integer
    Public Property ItemId As Integer
    Public Property ItemName As String
    Public Property ItemPrice As Decimal
    <Newtonsoft.Json.JsonConverter(GetType(BooleanConverter))>
    Public Property ItemActive As Boolean
End Class

''' <summary>
''' Custom JSON converter for handling integer values (0/1) as booleans
''' </summary>
Public Class BooleanConverter
    Inherits Newtonsoft.Json.JsonConverter

    Public Overrides Function CanConvert(objectType As Type) As Boolean
        Return objectType = GetType(Boolean)
    End Function

    Public Overrides Function ReadJson(reader As Newtonsoft.Json.JsonReader, objectType As Type, existingValue As Object, serializer As Newtonsoft.Json.JsonSerializer) As Object
        Dim token = Newtonsoft.Json.Linq.JToken.Load(reader)
        If token.Type = Newtonsoft.Json.Linq.JTokenType.Integer Then
            Return Convert.ToInt32(token.ToString()) <> 0
        ElseIf token.Type = Newtonsoft.Json.Linq.JTokenType.String Then
            Dim stringValue As String = token.ToString().ToLower()
            Return stringValue = "true" OrElse stringValue = "1"
        ElseIf token.Type = Newtonsoft.Json.Linq.JTokenType.Boolean Then
            Return CBool(token.ToString())
        End If
        Return False
    End Function

    Public Overrides Sub WriteJson(writer As Newtonsoft.Json.JsonWriter, value As Object, serializer As Newtonsoft.Json.JsonSerializer)
        writer.WriteValue(CBool(value))
    End Sub
End Class
