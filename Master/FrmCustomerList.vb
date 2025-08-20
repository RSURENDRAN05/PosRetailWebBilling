Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class FrmCustomerList
    Private _customerTable As DataTable
    Private _companyInfo As Object

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmCustomerList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Load customers (this will also setup the grid)
            LoadCustomers()

        Catch ex As Exception
            MessageBox.Show("Error loading form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadCustomers()
        Try
            Dim url As String = M_Details.LinkAjaxRequest & "AjaxRequest=71" ' Get all customers
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(url)

            Dim parseJson As JObject = JObject.Parse(json)
            Dim success = parseJson("Success").ToString()

            If success = "True" Then
                _customerTable = parseJson("Data").ToObject(Of DataTable)()
                GridControlCustomers.DataSource = _customerTable

            Else
                Dim errorMsg As String = If(parseJson("Msg") IsNot Nothing, parseJson("Msg").ToString(), "Failed to load customer data")
                MessageBox.Show("API Error: " & errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading customers: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

  

    Private Sub btnNewCustomer_Click(sender As Object, e As EventArgs) Handles btnNewCustomer.Click
        Try
            Dim frmNew As New FrmNewCustomer()
            If frmNew.ShowDialog() = DialogResult.OK Then
                LoadCustomers() ' Refresh list
            End If
        Catch ex As Exception
            MessageBox.Show("Error opening new customer form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnEditCustomer_Click(sender As Object, e As EventArgs) Handles btnEditCustomer.Click
        Try
            If GridViewCustomers.FocusedRowHandle < 0 Then
                MessageBox.Show("Please select a customer to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim customerId As Integer = GridViewCustomers.GetFocusedRowCellValue("CustomerId")
            Dim frmEdit As New FrmNewCustomer(customerId)
            If frmEdit.ShowDialog() = DialogResult.OK Then
                LoadCustomers() ' Refresh list
            End If

        Catch ex As Exception
            MessageBox.Show("Error editing customer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDeleteCustomer_Click(sender As Object, e As EventArgs) Handles btnDeleteCustomer.Click
        Try
            If GridViewCustomers.FocusedRowHandle < 0 Then
                MessageBox.Show("Please select a customer to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim customerId As Integer = GridViewCustomers.GetFocusedRowCellValue("CustomerId")
            Dim customerName As String = GridViewCustomers.GetFocusedRowCellValue("CustomerName").ToString()

            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete customer '" & customerName & "'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                DeleteCustomer(customerId)
            End If

        Catch ex As Exception
            MessageBox.Show("Error deleting customer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DeleteCustomer(customerId As Integer)
        Try
            Dim url As String = M_Details.LinkAjaxRequest & "AjaxRequest=74&customerid=" & customerId
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(url)

            Dim parseJson As JObject = JObject.Parse(json)
            Dim success = parseJson("Success").ToString()

            If success = "True" Then
                MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadCustomers() ' Refresh list
            Else
                Dim errorMsg As String = If(parseJson("Msg") IsNot Nothing, parseJson("Msg").ToString(), "Failed to delete customer")
                MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error deleting customer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            LoadCustomers()
        Catch ex As Exception
            MessageBox.Show("Error refreshing data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridViewCustomers_DoubleClick(sender As Object, e As EventArgs) Handles GridViewCustomers.DoubleClick
        Try
            ' Double-click to edit customer
            btnEditCustomer_Click(sender, e)
        Catch ex As Exception
            MessageBox.Show("Error in double-click: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridViewCustomers_KeyDown(sender As Object, e As KeyEventArgs) Handles GridViewCustomers.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                btnEditCustomer_Click(sender, Nothing)
            ElseIf e.KeyCode = Keys.Delete Then
                btnDeleteCustomer_Click(sender, Nothing)
            ElseIf e.KeyCode = Keys.Insert Then
                btnNewCustomer_Click(sender, Nothing)
            ElseIf e.KeyCode = Keys.F5 Then
                btnRefresh_Click(sender, Nothing)
            End If
        Catch ex As Exception
            MessageBox.Show("Error in key press: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
