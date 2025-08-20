Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class FrmNewCustomer
    Private _customerId As Integer = 0
    Private _isEditMode As Boolean = False
    Private _companyInfo As Object

    Public Property CustomerId As Integer
        Get
            Return _customerId
        End Get
        Set(value As Integer)
            _customerId = value
        End Set
    End Property

    Public Property IsEditMode As Boolean
        Get
            Return _isEditMode
        End Get
        Set(value As Boolean)
            _isEditMode = value
        End Set
    End Property

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(customerId As Integer)
        InitializeComponent()
        _customerId = customerId
        _isEditMode = True
    End Sub

    Private Sub FrmNewCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Get company info from global variables
            _companyInfo = M_Details.SoftwareVersion

            ' Setup form based on mode
            If _isEditMode Then
                Me.Text = "Edit Customer"
                btnSave.Text = "Update Customer"
                LoadCustomerData()
            Else
                Me.Text = "New Customer"
                btnSave.Text = "Save Customer"
                ClearForm()
            End If

            ' Set default status
            cmbStatus.Properties.Items.Clear()
            cmbStatus.Properties.Items.Add("1")
            cmbStatus.Properties.Items.Add("0")
            cmbStatus.Text = "1"

        Catch ex As Exception
            MessageBox.Show("Error loading form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadCustomerData()
        Try
            If _customerId <= 0 Then Return

            Dim url As String = M_Details.LinkAjaxRequest & "AjaxRequest=69&customerid=" & _customerId

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(url)
            Dim parseJson As JObject = JObject.Parse(json)
            Dim success = parseJson("Success").ToString()

            If success = "True" Then
                Dim customerData = parseJson("Data")

                txtCustomerName.Text = customerData("CustomerName").ToString()
                txtCustomerPhone.Text = If(customerData("CustomerPhone") IsNot Nothing, customerData("CustomerPhone").ToString(), "")
                lblPointsEarned.Text = If(customerData("CustomerPointsEarned") IsNot Nothing, customerData("CustomerPointsEarned").ToString(), "0")
                cmbStatus.Text = customerData("Status").ToString()
                lblCreatedDate.Text = If(customerData("Created") IsNot Nothing, customerData("Created").ToString(), "")
            Else
                MessageBox.Show("Customer not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading customer data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearForm()
        txtCustomerName.Text = ""
        txtCustomerPhone.Text = ""
        lblPointsEarned.Text = "0"
        cmbStatus.Text = "1"
        lblCreatedDate.Text = ""
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            ' Validate required fields
            If String.IsNullOrEmpty(txtCustomerName.Text.Trim()) Then
                MessageBox.Show("Customer name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCustomerName.Focus()
                Return
            End If

            ' Prepare data
            Dim customerData As New Dictionary(Of String, Object)
            customerData("customername") = txtCustomerName.Text.Trim()
            customerData("customerphone") = txtCustomerPhone.Text.Trim()
            customerData("cmbstatus") = cmbStatus.Text

            If _isEditMode Then
                customerData("customerid") = _customerId
                UpdateCustomer(customerData)
            Else
                customerData("customerid") = 0
                SaveNewCustomer(customerData)
            End If

        Catch ex As Exception
            MessageBox.Show("Error saving customer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveNewCustomer(customerData As Dictionary(Of String, Object))
        Try
            Dim jsonData As String = Newtonsoft.Json.JsonConvert.SerializeObject(customerData)
            Dim url As String = M_Details.LinkAjaxRequest & "AjaxRequest=28&json=" & jsonData

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(url)
            Dim parseJson As JObject = JObject.Parse(json)
            Dim success = parseJson("Success").ToString()

            If success = "True" Then
                MessageBox.Show("Customer saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                Dim errorMsg As String = If(parseJson("Msg") IsNot Nothing, parseJson("Msg").ToString(), "Failed to save customer")
                MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error saving customer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateCustomer(customerData As Dictionary(Of String, Object))
        Try
            Dim jsonData As String = Newtonsoft.Json.JsonConvert.SerializeObject(customerData)
            Dim url As String = M_Details.LinkAjaxRequest & "AjaxRequest=29&json=" & jsonData

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(url)
            Dim parseJson As JObject = JObject.Parse(json)
            Dim success = parseJson("Success").ToString()

            If success = "True" Then
                MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                Dim errorMsg As String = If(parseJson("Msg") IsNot Nothing, parseJson("Msg").ToString(), "Failed to update customer")
                MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error updating customer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error closing form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAddPoints_Click(sender As Object, e As EventArgs) Handles btnAddPoints.Click
        Try
            If Not _isEditMode OrElse _customerId <= 0 Then
                MessageBox.Show("Please save the customer first before adding points.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim pointsToAdd As String = InputBox("Enter points to add:", "Add Customer Points", "0")
            If String.IsNullOrEmpty(pointsToAdd) OrElse Not IsNumeric(pointsToAdd) Then Return

            Dim pointsData As New Dictionary(Of String, Object)
            pointsData("customerid") = _customerId
            pointsData("points") = Convert.ToInt32(pointsToAdd)

            Dim jsonData As String = Newtonsoft.Json.JsonConvert.SerializeObject(pointsData)
            Dim url As String = M_Details.LinkAjaxRequest & "AjaxRequest=70&json=" & jsonData

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(url)
            Dim parseJson As JObject = JObject.Parse(json)
            Dim success = parseJson("Success").ToString()

            If success = "True" Then
                MessageBox.Show("Customer points updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadCustomerData() ' Refresh data
            Else
                Dim errorMsg As String = If(parseJson("Msg") IsNot Nothing, parseJson("Msg").ToString(), "Failed to update points")
                MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error updating points: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCustomerName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCustomerName.KeyPress
        If e.KeyChar = Chr(13) Then ' Enter key
            txtCustomerPhone.Focus()
        End If
    End Sub

    Private Sub txtCustomerPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCustomerPhone.KeyPress
        If e.KeyChar = Chr(13) Then ' Enter key
            cmbStatus.Focus()
        End If
    End Sub

    Private Sub cmbStatus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbStatus.KeyPress
        If e.KeyChar = Chr(13) Then ' Enter key
            btnSave_Click(Nothing, Nothing)
        End If
    End Sub
End Class
