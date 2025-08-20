Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class FrmTestCustomerAPI

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmTestCustomerAPI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtResults.Text = "Testing Customer API..." & vbCrLf
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnTestAPI_Click(sender As Object, e As EventArgs) Handles btnTestAPI.Click
        Try
            txtResults.Text = "Testing Customer API..." & vbCrLf

            ' Test the customer API endpoint
            Dim url As String = M_Details.LinkAjaxRequest & "AjaxRequest=68"
            txtResults.Text &= "URL: " & url & vbCrLf & vbCrLf

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(url)

            txtResults.Text &= "Raw JSON Response:" & vbCrLf
            txtResults.Text &= json & vbCrLf & vbCrLf

            ' Parse JSON
            Dim parseJson As JObject = JObject.Parse(json)
            Dim success = parseJson("Success").ToString()

            txtResults.Text &= "Success: " & success & vbCrLf

            If success = "True" Then
                Dim dataArray = parseJson("Data")
                txtResults.Text &= "Data Array Count: " & dataArray.Count() & vbCrLf

                If dataArray.Count() > 0 Then
                    txtResults.Text &= "First Record:" & vbCrLf
                    txtResults.Text &= dataArray(0).ToString() & vbCrLf
                End If

                ' Test DataTable conversion
                Try
                    Dim customerTable As DataTable = dataArray.ToObject(Of DataTable)()
                    txtResults.Text &= vbCrLf & "DataTable Conversion:" & vbCrLf
                    txtResults.Text &= "Rows: " & customerTable.Rows.Count & vbCrLf
                    txtResults.Text &= "Columns: " & customerTable.Columns.Count & vbCrLf
                    txtResults.Text &= "Column Names: " & String.Join(", ", customerTable.Columns.Cast(Of DataColumn).Select(Function(c) c.ColumnName)) & vbCrLf
                Catch dtEx As Exception
                    txtResults.Text &= vbCrLf & "DataTable Error: " & dtEx.Message & vbCrLf
                End Try
            Else
                txtResults.Text &= "Error Message: " & parseJson("Msg")?.ToString() & vbCrLf
            End If

        Catch ex As Exception
            txtResults.Text &= vbCrLf & "Exception: " & ex.Message & vbCrLf
            txtResults.Text &= "Stack Trace: " & ex.StackTrace & vbCrLf
        End Try
    End Sub

    Private Sub btnLoadToGrid_Click(sender As Object, e As EventArgs) Handles btnLoadToGrid.Click
        Try
            txtResults.Text &= vbCrLf & "Testing Grid Loading..." & vbCrLf

            Dim url As String = M_Details.LinkAjaxRequest & "AjaxRequest=68"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(url)
            Dim parseJson As JObject = JObject.Parse(json)
            Dim success = parseJson("Success").ToString()

            If success = "True" Then
                Dim customerTable As DataTable = parseJson("Data").ToObject(Of DataTable)()
                GridControlTest.DataSource = customerTable

                txtResults.Text &= "Grid loaded with " & GridViewTest.RowCount & " rows" & vbCrLf
                txtResults.Text &= "Grid columns: " & GridViewTest.Columns.Count & vbCrLf

                For Each col In GridViewTest.Columns
                    txtResults.Text &= "  - " & col.FieldName & vbCrLf
                Next
            Else
                txtResults.Text &= "API returned error" & vbCrLf
            End If

        Catch ex As Exception
            txtResults.Text &= "Grid Error: " & ex.Message & vbCrLf
        End Try
    End Sub
End Class
