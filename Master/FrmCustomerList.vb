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
            ' Debug: Show the URL being called
            Dim url As String = M_Details.LinkAjaxRequest & "AjaxRequest=68" ' Get all customers
            Console.WriteLine("Loading customers from URL: " & url)

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(url)

            ' Debug: Show the raw JSON response
            Console.WriteLine("Raw JSON Response: " & json)

            Dim parseJson As JObject = JObject.Parse(json)
            Dim success = parseJson("Success").ToString()

            If success = "True" Then
                _customerTable = parseJson("Data").ToObject(Of DataTable)()

                ' Debug: Show data table info
                Console.WriteLine("DataTable created with " & _customerTable.Rows.Count & " rows")
                Console.WriteLine("DataTable columns: " & String.Join(", ", _customerTable.Columns.Cast(Of DataColumn).Select(Function(c) c.ColumnName)))

                GridControlCustomers.DataSource = _customerTable

                ' Setup grid after data is loaded
                SetupGrid()

                ' Debug: Show grid info
                Console.WriteLine("GridView row count: " & GridViewCustomers.RowCount)
            Else
                Dim errorMsg As String = If(parseJson("Msg") IsNot Nothing, parseJson("Msg").ToString(), "Failed to load customer data")
                MessageBox.Show("API Error: " & errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading customers: " & ex.Message & vbCrLf & "Stack Trace: " & ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetupGrid()
        Try
            If _customerTable Is Nothing OrElse _customerTable.Rows.Count = 0 Then
                Console.WriteLine("No data to setup grid with")
                Return
            End If

            ' Configure grid columns safely
            If GridViewCustomers.Columns("CustomerId") IsNot Nothing Then
                GridViewCustomers.Columns("CustomerId").Visible = False
            End If

            If GridViewCustomers.Columns("CustomerName") IsNot Nothing Then
                GridViewCustomers.Columns("CustomerName").Caption = "Customer Name"
                GridViewCustomers.Columns("CustomerName").Width = 200
            End If

            If GridViewCustomers.Columns("CustomerPhone") IsNot Nothing Then
                GridViewCustomers.Columns("CustomerPhone").Caption = "Phone"
                GridViewCustomers.Columns("CustomerPhone").Width = 150
            End If

            If GridViewCustomers.Columns("CustomerPointsEarned") IsNot Nothing Then
                GridViewCustomers.Columns("CustomerPointsEarned").Caption = "Points"
                GridViewCustomers.Columns("CustomerPointsEarned").Width = 80
                GridViewCustomers.Columns("CustomerPointsEarned").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridViewCustomers.Columns("CustomerPointsEarned").DisplayFormat.FormatString = "0"
            End If

            If GridViewCustomers.Columns("Status") IsNot Nothing Then
                GridViewCustomers.Columns("Status").Caption = "Active"
                GridViewCustomers.Columns("Status").Width = 60
            End If

            If GridViewCustomers.Columns("Created") IsNot Nothing Then
                GridViewCustomers.Columns("Created").Caption = "Created Date"
                GridViewCustomers.Columns("Created").Width = 120
                GridViewCustomers.Columns("Created").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridViewCustomers.Columns("Created").DisplayFormat.FormatString = "dd/MM/yyyy"
            End If

            ' Set grid options
            GridViewCustomers.OptionsSelection.EnableAppearanceFocusedCell = False
            GridViewCustomers.OptionsSelection.MultiSelect = False
            GridViewCustomers.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            GridViewCustomers.OptionsView.ShowAutoFilterRow = True

            ' Focus first row
            If GridViewCustomers.RowCount > 0 Then
                GridViewCustomers.FocusedRowHandle = 0
            End If

            ' Debug: List all available columns
            Console.WriteLine("Available GridView columns:")
            For Each col In GridViewCustomers.Columns
                Console.WriteLine("- " & col.FieldName & " (" & col.Caption & ")")
            Next

        Catch ex As Exception
            MessageBox.Show("Error setting up grid: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            Dim url As String = M_Details.LinkAjaxRequest & "AjaxRequest=71&customerid=" & customerId

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
