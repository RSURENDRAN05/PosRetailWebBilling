Imports System.Data
Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class FrmSalesCommission


    ' Data
    Private commissionsTable As DataTable
    Private salesmenTable As DataTable
    Private subGroupsTable As DataTable
    Private itemsTable As DataTable
    Private selectedCommissionId As Integer = 0
    Private selectedEmpId As Integer = 0

    ' Bulk update variables
    Private bulkItemsTable As DataTable
    Private selectedItemIds As New List(Of Integer)

    Public Sub New()
        InitializeComponent()
        ' Set form properties
        Me.Text = "Sales Commission Management"
        Me.WindowState = FormWindowState.Maximized
        InitializeForm()
    End Sub

    Private Function GetSelectedItemIds() As List(Of Integer)
        Dim selectedItems As New List(Of Integer)
        Try
            For Each row As DataRow In bulkItemsTable.Rows
                If Convert.ToBoolean(row("Selected")) Then
                    selectedItems.Add(Convert.ToInt32(row("ItemId")))
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Error collecting selected items: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return selectedItems
    End Function

    Private Function ValidateBulkInput(empId As Integer, subGroupId As Integer, selectedItems As List(Of Integer)) As Boolean
        If empId = 0 Then
            MessageBox.Show("Please select an employee for bulk update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If subGroupId = 0 Then
            MessageBox.Show("Please select a sub group for bulk update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If selectedItems.Count = 0 Then
            MessageBox.Show("Please select at least one item for bulk update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    Private Sub BulkUpdateCommissions(empId As Integer, subGroupId As Integer, selectedItems As List(Of Integer),
                                      commissionType As String, commissionPercentage As Decimal, fixedAmount As Decimal, status As Boolean)
        Try
            Dim successCount As Integer = 0
            Dim errorCount As Integer = 0
            Dim errors As New List(Of String)

            For Each itemId As Integer In selectedItems
                Try
                    Dim commissionData As New Dictionary(Of String, Object)
                    commissionData("emp_id") = empId
                    commissionData("item_id") = itemId
                    commissionData("sub_group_id") = subGroupId
                    commissionData("commission_percentage") = commissionPercentage
                    commissionData("commission_type") = commissionType
                    commissionData("fixed_amount") = fixedAmount
                    commissionData("status") = If(status, 1, 0)

                    Dim jsonString As String = JsonConvert.SerializeObject(commissionData)
                    Dim url As String = M_Details.LinkAjaxRequest & "SalesManCommission=4&json=" & Uri.EscapeDataString(jsonString)

                    Dim json As String = New WebClient().DownloadString(url)
                    Dim parsedJson As JObject = JObject.Parse(json)

                    If parsedJson("Success").ToString() = "True" Then
                        successCount += 1
                    Else
                        errorCount += 1
                        errors.Add("Item ID " & itemId & ": " & parsedJson("Msg").ToString())
                    End If

                Catch ex As Exception
                    errorCount += 1
                    errors.Add("Item ID " & itemId & ": " & ex.Message)
                End Try
            Next

            ' Show result summary
            Dim message As String = String.Format("Bulk Update Complete!{0}Success: {1}{0}Errors: {2}",
                                                  vbCrLf, successCount, errorCount)

            If errors.Count > 0 AndAlso errors.Count <= 5 Then
                message += vbCrLf & vbCrLf & "Errors:" & vbCrLf & String.Join(vbCrLf, errors)
            ElseIf errors.Count > 5 Then
                message += vbCrLf & vbCrLf & "First 5 errors:" & vbCrLf & String.Join(vbCrLf, errors.Take(5))
            End If

            MessageBox.Show(message, "Bulk Update Results", MessageBoxButtons.OK,
                           If(errorCount = 0, MessageBoxIcon.Information, MessageBoxIcon.Warning))

            ' Refresh the commissions grid
            LoadAllCommissions()

        Catch ex As Exception
            MessageBox.Show("Error during bulk update: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InitializeForm()
        Try
            ' Show loading message
            Me.Text = "Sales Commission Management - Loading..."

            SetupDataTables()
            LoadSalesmen()
            LoadSubGroups()
            LoadAllCommissions()
            SetupFormControls()

            ' Refresh ComboBox bindings after all data is loaded
            RefreshComboBoxBindings()

            ' Update title when loaded
            Me.Text = "Sales Commission Management - Ready"
        Catch ex As Exception
            MessageBox.Show("Error initializing form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Text = "Sales Commission Management - Error"
        End Try
    End Sub

    Private Sub SetupDataTables()
        ' Setup Commissions DataTable
        commissionsTable = New DataTable()
        commissionsTable.Columns.Add("commission_id", GetType(Integer))
        commissionsTable.Columns.Add("emp_id", GetType(Integer))
        commissionsTable.Columns.Add("emp_name", GetType(String))
        commissionsTable.Columns.Add("item_id", GetType(Integer))
        commissionsTable.Columns.Add("item_name", GetType(String))
        commissionsTable.Columns.Add("sub_group_id", GetType(Integer))
        commissionsTable.Columns.Add("sub_group_name", GetType(String))
        commissionsTable.Columns.Add("commission_percentage", GetType(Decimal))
        commissionsTable.Columns.Add("commission_type", GetType(String))
        commissionsTable.Columns.Add("fixed_amount", GetType(Decimal))
        commissionsTable.Columns.Add("status", GetType(Boolean))

        dgvCommissions.DataSource = commissionsTable

        ' Setup Salesmen DataTable
        salesmenTable = New DataTable()
        salesmenTable.Columns.Add("emp_id", GetType(Integer))
        salesmenTable.Columns.Add("emp_printname", GetType(String))

        dgvSalesmen.DataSource = salesmenTable

        ' Setup Sub Groups DataTable
        subGroupsTable = New DataTable()
        subGroupsTable.Columns.Add("SubGroupId", GetType(Integer))
        subGroupsTable.Columns.Add("SubGroupName", GetType(String))

        ' Setup Items DataTable
        itemsTable = New DataTable()
        itemsTable.Columns.Add("ItemId", GetType(Integer))
        itemsTable.Columns.Add("ItemName", GetType(String))

        ' Setup Bulk Items DataTable with checkbox support
        bulkItemsTable = New DataTable()
        bulkItemsTable.Columns.Add("Selected", GetType(Boolean))
        bulkItemsTable.Columns.Add("ItemId", GetType(Integer))
        bulkItemsTable.Columns.Add("ItemName", GetType(String))
    End Sub

    Private Sub SetupFormControls()
        ' Setup Commission Type ComboBox
        cmbCommissionType.Properties.Items.Clear()
        cmbCommissionType.Properties.Items.Add("Percentage")
        cmbCommissionType.Properties.Items.Add("Fixed Amount")
        cmbCommissionType.SelectedIndex = 0

        ' Setup Salesman ComboBox
        cmbSalesman.Properties.DataSource = salesmenTable
        cmbSalesman.Properties.DisplayMember = "emp_printname"
        cmbSalesman.Properties.ValueMember = "emp_id"

        ' Setup Sub Group ComboBox
        cmbSubGroup.Properties.DataSource = subGroupsTable
        cmbSubGroup.Properties.DisplayMember = "SubGroupName"
        cmbSubGroup.Properties.ValueMember = "SubGroupId"

        ' Setup Item ComboBox
        cmbItem.Properties.DataSource = itemsTable
        cmbItem.Properties.DisplayMember = "ItemName"
        cmbItem.Properties.ValueMember = "ItemId"

        ' Setup Bulk Controls
        SetupBulkControls()

        ' Set default values
        txtCommissionPercentage.Text = "0.00"
        txtFixedAmount.Text = "0.00"
        chkStatus.Checked = True

        ' Enable/disable fields based on commission type
        UpdateFieldsBasedOnCommissionType()
    End Sub

    Private Sub SetupBulkControls()
        ' Setup Bulk Items Grid
        dgvBulkItems.DataSource = bulkItemsTable

        ' Setup Bulk Salesman ComboBox
        cmbBulkSalesman.Properties.DataSource = salesmenTable
        cmbBulkSalesman.Properties.DisplayMember = "emp_printname"
        cmbBulkSalesman.Properties.ValueMember = "emp_id"

        ' Setup Bulk Sub Group ComboBox
        cmbBulkSubGroup.Properties.DataSource = subGroupsTable
        cmbBulkSubGroup.Properties.DisplayMember = "SubGroupName"
        cmbBulkSubGroup.Properties.ValueMember = "SubGroupId"

        ' Setup Bulk Commission Type ComboBox
        cmbBulkCommissionType.Properties.Items.Clear()
        cmbBulkCommissionType.Properties.Items.Add("Percentage")
        cmbBulkCommissionType.Properties.Items.Add("Fixed Amount")
        cmbBulkCommissionType.SelectedIndex = 0

        ' Set bulk default values
        txtBulkCommissionPercentage.Text = "0.00"
        txtBulkFixedAmount.Text = "0.00"
        chkBulkStatus.Checked = True
    End Sub

    Private Sub RefreshComboBoxBindings()
        ' Refresh all ComboBox data bindings after data is loaded
        cmbSalesman.Properties.DataSource = Nothing
        cmbSalesman.Properties.DataSource = salesmenTable
        cmbSalesman.Properties.DisplayMember = "emp_printname"
        cmbSalesman.Properties.ValueMember = "emp_id"

        cmbSubGroup.Properties.DataSource = Nothing
        cmbSubGroup.Properties.DataSource = subGroupsTable
        cmbSubGroup.Properties.DisplayMember = "SubGroupName"
        cmbSubGroup.Properties.ValueMember = "SubGroupId"

        cmbItem.Properties.DataSource = Nothing
        cmbItem.Properties.DataSource = itemsTable
        cmbItem.Properties.DisplayMember = "ItemName"
        cmbItem.Properties.ValueMember = "ItemId"

        cmbFilterSalesman.Properties.DataSource = Nothing
        cmbFilterSalesman.Properties.DataSource = salesmenTable
        cmbFilterSalesman.Properties.DisplayMember = "emp_printname"
        cmbFilterSalesman.Properties.ValueMember = "emp_id"
    End Sub

    Private Function GetEmployeeNameById(empId As Integer) As String
        Try
            For Each row As DataRow In salesmenTable.Rows
                If Convert.ToInt32(row("emp_id")) = empId Then
                    Return row("emp_printname").ToString()
                End If
            Next
            Return "Unknown Employee"
        Catch ex As Exception
            Return "Error Loading Name"
        End Try
    End Function

    Private Sub LoadSalesmen()
        Try
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=1")
            Dim parsedJson As JObject = JObject.Parse(json)

            salesmenTable.Clear()
            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")


                For Each item In dataArray
                    Dim empId As Integer = 0
                    Dim empName As String = ""

                    ' Try multiple possible field name variations for emp_id
                    If item("emp_id") IsNot Nothing AndAlso Not IsDBNull(item("emp_id")) Then
                        empId = Convert.ToInt32(item("emp_id"))
                    ElseIf item("EmpId") IsNot Nothing AndAlso Not IsDBNull(item("EmpId")) Then
                        empId = Convert.ToInt32(item("EmpId"))
                    ElseIf item("empid") IsNot Nothing AndAlso Not IsDBNull(item("empid")) Then
                        empId = Convert.ToInt32(item("empid"))
                    End If

                    ' Try multiple possible field name variations for employee name
                    If item("emp_printname") IsNot Nothing AndAlso item("emp_printname").ToString() <> "" Then
                        empName = item("emp_printname").ToString()
                    ElseIf item("EmpPrintName") IsNot Nothing AndAlso item("EmpPrintName").ToString() <> "" Then
                        empName = item("EmpPrintName").ToString()
                    ElseIf item("emp_firstname") IsNot Nothing AndAlso item("emp_firstname").ToString() <> "" Then
                        empName = item("emp_firstname").ToString()
                    ElseIf item("EmpFirstName") IsNot Nothing AndAlso item("EmpFirstName").ToString() <> "" Then
                        empName = item("EmpFirstName").ToString()
                    ElseIf item("emp_name") IsNot Nothing AndAlso item("emp_name").ToString() <> "" Then
                        empName = item("emp_name").ToString()
                    ElseIf item("EmpName") IsNot Nothing AndAlso item("EmpName").ToString() <> "" Then
                        empName = item("EmpName").ToString()
                    End If

                    salesmenTable.Rows.Add(empId, empName)
                Next

            Else
                MessageBox.Show("Failed to load salesmen: " & parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            ' Refresh the Salesmen GridControl
            dgvSalesmen.RefreshDataSource()
            dgvSalesmen.Refresh()
        Catch ex As Exception
            MessageBox.Show("Error loading salesmen: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadSubGroups()
        Try
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=2")
            Dim parsedJson As JObject = JObject.Parse(json)

            subGroupsTable.Clear()
            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")


                For Each item In dataArray
                    Dim subGroupId As Integer = 0
                    Dim subGroupName As String = ""

                    ' Try multiple possible field name variations
                    If item("SubGroupId") IsNot Nothing AndAlso Not IsDBNull(item("SubGroupId")) Then
                        subGroupId = Convert.ToInt32(item("SubGroupId"))
                    ElseIf item("subgroupid") IsNot Nothing AndAlso Not IsDBNull(item("subgroupid")) Then
                        subGroupId = Convert.ToInt32(item("subgroupid"))
                    ElseIf item("sub_group_id") IsNot Nothing AndAlso Not IsDBNull(item("sub_group_id")) Then
                        subGroupId = Convert.ToInt32(item("sub_group_id"))
                    ElseIf item("dcm_id") IsNot Nothing AndAlso Not IsDBNull(item("dcm_id")) Then
                        subGroupId = Convert.ToInt32(item("dcm_id"))
                    End If

                    If item("SubGroupName") IsNot Nothing AndAlso item("SubGroupName").ToString() <> "" Then
                        subGroupName = item("SubGroupName").ToString()
                    ElseIf item("subgroupname") IsNot Nothing AndAlso item("subgroupname").ToString() <> "" Then
                        subGroupName = item("subgroupname").ToString()
                    ElseIf item("sub_group_name") IsNot Nothing AndAlso item("sub_group_name").ToString() <> "" Then
                        subGroupName = item("sub_group_name").ToString()
                    ElseIf item("dcm_name") IsNot Nothing AndAlso item("dcm_name").ToString() <> "" Then
                        subGroupName = item("dcm_name").ToString()
                    End If

                    subGroupsTable.Rows.Add(subGroupId, subGroupName)
                Next
            Else
                MessageBox.Show("Failed to load sub groups: " & parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading sub groups: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadItemsBySubGroup(subGroupId As Integer)
        Try
            If subGroupId > 0 Then
                Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=3&sub_group_id=" & subGroupId)
                Dim parsedJson As JObject = JObject.Parse(json)

                itemsTable.Clear()
                If parsedJson("Success").ToString() = "True" Then
                    Dim dataArray = parsedJson("Data")
                    For Each item In dataArray
                        itemsTable.Rows.Add(
                            If(item("ItemId") Is Nothing OrElse IsDBNull(item("ItemId")), 0, Convert.ToInt32(item("ItemId"))),
                            If(item("ItemName") Is Nothing, "", item("ItemName").ToString())
                        )
                    Next
                End If
            Else
                itemsTable.Clear()
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading items: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadBulkItemsBySubGroup(subGroupId As Integer)
        Try
            If subGroupId > 0 Then
                Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=3&sub_group_id=" & subGroupId)
                Dim parsedJson As JObject = JObject.Parse(json)

                bulkItemsTable.Clear()
                If parsedJson("Success").ToString() = "True" Then
                    Dim dataArray = parsedJson("Data")

                    For Each item In dataArray
                        Dim itemId As Integer = 0
                        Dim itemName As String = ""

                        ' Try multiple possible field name variations for ItemId
                        If item("ItemId") IsNot Nothing AndAlso Not IsDBNull(item("ItemId")) Then
                            itemId = Convert.ToInt32(item("ItemId"))
                        ElseIf item("itemid") IsNot Nothing AndAlso Not IsDBNull(item("itemid")) Then
                            itemId = Convert.ToInt32(item("itemid"))
                        ElseIf item("item_id") IsNot Nothing AndAlso Not IsDBNull(item("item_id")) Then
                            itemId = Convert.ToInt32(item("item_id"))
                        End If

                        ' Try multiple possible field name variations for ItemName
                        If item("ItemName") IsNot Nothing Then
                            itemName = item("ItemName").ToString()
                        ElseIf item("itemname") IsNot Nothing Then
                            itemName = item("itemname").ToString()
                        ElseIf item("item_name") IsNot Nothing Then
                            itemName = item("item_name").ToString()
                        End If

                        bulkItemsTable.Rows.Add(False, itemId, itemName)
                    Next

                    MessageBox.Show("Loaded " & bulkItemsTable.Rows.Count & " items for bulk selection.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Failed to load bulk items: " & parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

                ' Refresh the grid to display the updated data
                dgvBulkItems.RefreshDataSource()
                dgvBulkItems.Refresh()
            Else
                bulkItemsTable.Clear()
                dgvBulkItems.RefreshDataSource()
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading bulk items: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadAllCommissions()
        Try
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=7")
            Dim parsedJson As JObject = JObject.Parse(json)

            commissionsTable.Clear()
            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")

                For Each item In dataArray
                    Dim commissionId As Integer = 0
                    Dim empId As Integer = 0
                    Dim empName As String = ""
                    Dim itemId As Integer = 0
                    Dim itemName As String = ""
                    Dim subGroupId As Integer = 0
                    Dim subGroupName As String = ""
                    Dim commissionPercentage As Decimal = 0
                    Dim commissionType As String = ""
                    Dim fixedAmount As Decimal = 0
                    Dim status As Boolean = False

                    ' Try multiple field name variations for commission_id
                    If item("commission_id") IsNot Nothing AndAlso Not IsDBNull(item("commission_id")) Then
                        commissionId = Convert.ToInt32(item("commission_id"))
                    ElseIf item("CommissionId") IsNot Nothing AndAlso Not IsDBNull(item("CommissionId")) Then
                        commissionId = Convert.ToInt32(item("CommissionId"))
                    End If

                    ' Try multiple field name variations for emp_id
                    If item("emp_id") IsNot Nothing AndAlso Not IsDBNull(item("emp_id")) Then
                        empId = Convert.ToInt32(item("emp_id"))
                    ElseIf item("EmpId") IsNot Nothing AndAlso Not IsDBNull(item("EmpId")) Then
                        empId = Convert.ToInt32(item("EmpId"))
                    End If

                    ' Try multiple field name variations for emp_name
                    If item("emp_name") IsNot Nothing AndAlso item("emp_name").ToString() <> "" Then
                        empName = item("emp_name").ToString()
                    ElseIf item("EmpName") IsNot Nothing AndAlso item("EmpName").ToString() <> "" Then
                        empName = item("EmpName").ToString()
                    ElseIf item("emp_printname") IsNot Nothing AndAlso item("emp_printname").ToString() <> "" Then
                        empName = item("emp_printname").ToString()
                    ElseIf item("EmpPrintName") IsNot Nothing AndAlso item("EmpPrintName").ToString() <> "" Then
                        empName = item("EmpPrintName").ToString()
                    Else
                        ' If emp_name is not in the API response, lookup from salesmenTable using emp_id
                        If empId > 0 Then
                            empName = GetEmployeeNameById(empId)
                        End If
                    End If

                    ' Try multiple field name variations for item_id
                    If item("item_id") IsNot Nothing AndAlso Not IsDBNull(item("item_id")) Then
                        itemId = Convert.ToInt32(item("item_id"))
                    ElseIf item("ItemId") IsNot Nothing AndAlso Not IsDBNull(item("ItemId")) Then
                        itemId = Convert.ToInt32(item("ItemId"))
                    End If

                    ' Try multiple field name variations for item_name
                    If item("item_name") IsNot Nothing AndAlso item("item_name").ToString() <> "" Then
                        itemName = item("item_name").ToString()
                    ElseIf item("ItemName") IsNot Nothing AndAlso item("ItemName").ToString() <> "" Then
                        itemName = item("ItemName").ToString()
                    End If

                    ' Try multiple field name variations for sub_group_id
                    If item("sub_group_id") IsNot Nothing AndAlso Not IsDBNull(item("sub_group_id")) Then
                        subGroupId = Convert.ToInt32(item("sub_group_id"))
                    ElseIf item("SubGroupId") IsNot Nothing AndAlso Not IsDBNull(item("SubGroupId")) Then
                        subGroupId = Convert.ToInt32(item("SubGroupId"))
                    End If

                    ' Try multiple field name variations for sub_group_name
                    If item("sub_group_name") IsNot Nothing AndAlso item("sub_group_name").ToString() <> "" Then
                        subGroupName = item("sub_group_name").ToString()
                    ElseIf item("SubGroupName") IsNot Nothing AndAlso item("SubGroupName").ToString() <> "" Then
                        subGroupName = item("SubGroupName").ToString()
                    End If

                    ' Handle commission_percentage
                    If item("commission_percentage") IsNot Nothing AndAlso Not IsDBNull(item("commission_percentage")) Then
                        commissionPercentage = Convert.ToDecimal(item("commission_percentage"))
                    ElseIf item("CommissionPercentage") IsNot Nothing AndAlso Not IsDBNull(item("CommissionPercentage")) Then
                        commissionPercentage = Convert.ToDecimal(item("CommissionPercentage"))
                    End If

                    ' Handle commission_type
                    If item("commission_type") IsNot Nothing AndAlso item("commission_type").ToString() <> "" Then
                        commissionType = item("commission_type").ToString()
                    ElseIf item("CommissionType") IsNot Nothing AndAlso item("CommissionType").ToString() <> "" Then
                        commissionType = item("CommissionType").ToString()
                    End If

                    ' Handle fixed_amount
                    If item("fixed_amount") IsNot Nothing AndAlso Not IsDBNull(item("fixed_amount")) Then
                        fixedAmount = Convert.ToDecimal(item("fixed_amount"))
                    ElseIf item("FixedAmount") IsNot Nothing AndAlso Not IsDBNull(item("FixedAmount")) Then
                        fixedAmount = Convert.ToDecimal(item("FixedAmount"))
                    End If

                    ' Handle status - support multiple data types (boolean, integer, string)
                    If item("status") IsNot Nothing AndAlso Not IsDBNull(item("status")) Then
                        Dim statusValue = item("status").ToString().ToLower()
                        status = (statusValue = "1" OrElse statusValue = "true" OrElse statusValue = "yes" OrElse statusValue = "active")
                    ElseIf item("Status") IsNot Nothing AndAlso Not IsDBNull(item("Status")) Then
                        Dim statusValue = item("Status").ToString().ToLower()
                        status = (statusValue = "1" OrElse statusValue = "true" OrElse statusValue = "yes" OrElse statusValue = "active")
                    End If

                    commissionsTable.Rows.Add(commissionId, empId, empName, itemId, itemName, subGroupId, subGroupName, commissionPercentage, commissionType, fixedAmount, status)
                Next
            Else
                MessageBox.Show("Failed to load commissions: " & parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            ' Refresh the GridControl to ensure it displays the updated data
            dgvCommissions.RefreshDataSource()
            dgvCommissions.Refresh()
        Catch ex As Exception
            MessageBox.Show("Error loading commissions: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadCommissionsBySalesman(empIds As Integer)
        Try
            If empIds > 0 Then
                Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=8&emp_id=" & empIds)
                Dim parsedJson As JObject = JObject.Parse(json)

                commissionsTable.Clear()
                If parsedJson("Success").ToString() = "True" Then
                    Dim dataArray = parsedJson("Data")

                    For Each item In dataArray
                        Dim commissionId As Integer = 0
                        Dim empId As Integer = empIds
                        Dim empName As String = ""
                        Dim itemId As Integer = 0
                        Dim itemName As String = ""
                        Dim subGroupId As Integer = 0
                        Dim subGroupName As String = ""
                        Dim commissionPercentage As Decimal = 0
                        Dim commissionType As String = ""
                        Dim fixedAmount As Decimal = 0
                        Dim status As Boolean = False

                        ' Try multiple field name variations for commission_id
                        If item("commission_id") IsNot Nothing AndAlso Not IsDBNull(item("commission_id")) Then
                            commissionId = Convert.ToInt32(item("commission_id"))
                        ElseIf item("CommissionId") IsNot Nothing AndAlso Not IsDBNull(item("CommissionId")) Then
                            commissionId = Convert.ToInt32(item("CommissionId"))
                        End If

                        ' Try multiple field name variations for emp_id
                        If item("emp_id") IsNot Nothing AndAlso Not IsDBNull(item("emp_id")) Then
                            empId = Convert.ToInt32(item("emp_id"))
                        ElseIf item("EmpId") IsNot Nothing AndAlso Not IsDBNull(item("EmpId")) Then
                            empId = Convert.ToInt32(item("EmpId"))
                        End If

                        ' Try multiple field name variations for emp_name
                        If item("emp_name") IsNot Nothing AndAlso item("emp_name").ToString() <> "" Then
                            empName = item("emp_name").ToString()
                        ElseIf item("EmpName") IsNot Nothing AndAlso item("EmpName").ToString() <> "" Then
                            empName = item("EmpName").ToString()
                        ElseIf item("emp_printname") IsNot Nothing AndAlso item("emp_printname").ToString() <> "" Then
                            empName = item("emp_printname").ToString()
                        ElseIf item("EmpPrintName") IsNot Nothing AndAlso item("EmpPrintName").ToString() <> "" Then
                            empName = item("EmpPrintName").ToString()
                        Else
                            ' If emp_name is not in the API response, lookup from salesmenTable using emp_id
                            If empId > 0 Then
                                empName = GetEmployeeNameById(empId)
                            End If
                        End If

                        ' Try multiple field name variations for item_id
                        If item("item_id") IsNot Nothing AndAlso Not IsDBNull(item("item_id")) Then
                            itemId = Convert.ToInt32(item("item_id"))
                        ElseIf item("ItemId") IsNot Nothing AndAlso Not IsDBNull(item("ItemId")) Then
                            itemId = Convert.ToInt32(item("ItemId"))
                        End If

                        ' Try multiple field name variations for item_name
                        If item("item_name") IsNot Nothing AndAlso item("item_name").ToString() <> "" Then
                            itemName = item("item_name").ToString()
                        ElseIf item("ItemName") IsNot Nothing AndAlso item("ItemName").ToString() <> "" Then
                            itemName = item("ItemName").ToString()
                        End If

                        ' Try multiple field name variations for sub_group_id
                        If item("sub_group_id") IsNot Nothing AndAlso Not IsDBNull(item("sub_group_id")) Then
                            subGroupId = Convert.ToInt32(item("sub_group_id"))
                        ElseIf item("SubGroupId") IsNot Nothing AndAlso Not IsDBNull(item("SubGroupId")) Then
                            subGroupId = Convert.ToInt32(item("SubGroupId"))
                        End If

                        ' Try multiple field name variations for sub_group_name
                        If item("sub_group_name") IsNot Nothing AndAlso item("sub_group_name").ToString() <> "" Then
                            subGroupName = item("sub_group_name").ToString()
                        ElseIf item("SubGroupName") IsNot Nothing AndAlso item("SubGroupName").ToString() <> "" Then
                            subGroupName = item("SubGroupName").ToString()
                        End If

                        ' Handle commission_percentage
                        If item("commission_percentage") IsNot Nothing AndAlso Not IsDBNull(item("commission_percentage")) Then
                            commissionPercentage = Convert.ToDecimal(item("commission_percentage"))
                        ElseIf item("CommissionPercentage") IsNot Nothing AndAlso Not IsDBNull(item("CommissionPercentage")) Then
                            commissionPercentage = Convert.ToDecimal(item("CommissionPercentage"))
                        End If

                        ' Handle commission_type
                        If item("commission_type") IsNot Nothing AndAlso item("commission_type").ToString() <> "" Then
                            commissionType = item("commission_type").ToString()
                        ElseIf item("CommissionType") IsNot Nothing AndAlso item("CommissionType").ToString() <> "" Then
                            commissionType = item("CommissionType").ToString()
                        End If

                        ' Handle fixed_amount
                        If item("fixed_amount") IsNot Nothing AndAlso Not IsDBNull(item("fixed_amount")) Then
                            fixedAmount = Convert.ToDecimal(item("fixed_amount"))
                        ElseIf item("FixedAmount") IsNot Nothing AndAlso Not IsDBNull(item("FixedAmount")) Then
                            fixedAmount = Convert.ToDecimal(item("FixedAmount"))
                        End If

                        ' Handle status - support multiple data types (boolean, integer, string)
                        If item("status") IsNot Nothing AndAlso Not IsDBNull(item("status")) Then
                            Dim statusValue = item("status").ToString().ToLower()
                            status = (statusValue = "1" OrElse statusValue = "true" OrElse statusValue = "yes" OrElse statusValue = "active")
                        ElseIf item("Status") IsNot Nothing AndAlso Not IsDBNull(item("Status")) Then
                            Dim statusValue = item("Status").ToString().ToLower()
                            status = (statusValue = "1" OrElse statusValue = "true" OrElse statusValue = "yes" OrElse statusValue = "active")
                        End If

                        commissionsTable.Rows.Add(commissionId, empId, empName, itemId, itemName, subGroupId, subGroupName, commissionPercentage, commissionType, fixedAmount, status)
                    Next
                Else
                    MessageBox.Show("Failed to load commissions: " & parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

                ' Refresh the GridControl to ensure it displays the updated data
                dgvCommissions.RefreshDataSource()
                dgvCommissions.Refresh()
            Else
                LoadAllCommissions()
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading commissions by salesman: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbSubGroup_EditValueChanged(sender As Object, e As EventArgs) Handles cmbSubGroup.EditValueChanged
        Try
            If cmbSubGroup.EditValue IsNot Nothing AndAlso Not IsDBNull(cmbSubGroup.EditValue) Then
                Dim subGroupId As Integer = Convert.ToInt32(cmbSubGroup.EditValue)
                LoadItemsBySubGroup(subGroupId)
            Else
                itemsTable.Clear()
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading items for sub group: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbCommissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCommissionType.SelectedIndexChanged
        UpdateFieldsBasedOnCommissionType()
    End Sub

    Private Sub UpdateFieldsBasedOnCommissionType()
        If cmbCommissionType.Text = "Percentage" Then
            txtCommissionPercentage.Enabled = True
            txtFixedAmount.Enabled = False
            txtFixedAmount.Text = "0.00"
        ElseIf cmbCommissionType.Text = "Fixed Amount" Then
            txtCommissionPercentage.Enabled = False
            txtFixedAmount.Enabled = True
            txtCommissionPercentage.Text = "0.00"
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If ValidateInput() Then
                Dim commissionData As New Dictionary(Of String, Object)
                commissionData("emp_id") = Convert.ToInt32(cmbSalesman.EditValue)
                commissionData("item_id") = Convert.ToInt32(cmbItem.EditValue)
                commissionData("sub_group_id") = Convert.ToInt32(cmbSubGroup.EditValue)
                commissionData("commission_percentage") = Convert.ToDecimal(txtCommissionPercentage.Text)
                commissionData("commission_type") = cmbCommissionType.Text
                commissionData("fixed_amount") = Convert.ToDecimal(txtFixedAmount.Text)
                commissionData("status") = If(chkStatus.Checked, 1, 0)

                Dim jsonString As String = JsonConvert.SerializeObject(commissionData)

                Dim url As String
                If selectedCommissionId = 0 Then
                    ' Insert new commission
                    url = M_Details.LinkAjaxRequest & "SalesManCommission=4&json=" & Uri.EscapeDataString(jsonString)
                Else
                    ' Update existing commission
                    commissionData("commission_id") = selectedCommissionId
                    jsonString = JsonConvert.SerializeObject(commissionData)
                    url = M_Details.LinkAjaxRequest & "SalesManCommission=5&json=" & Uri.EscapeDataString(jsonString)
                End If

                Dim json As String = New WebClient().DownloadString(url)
                Dim parsedJson As JObject = JObject.Parse(json)

                If parsedJson("Success").ToString() = "True" Then
                    MessageBox.Show(parsedJson("Msg").ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ClearForm()
                    LoadAllCommissions()
                Else
                    MessageBox.Show(parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error saving commission: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ValidateInput() As Boolean
        If cmbSalesman.EditValue Is Nothing OrElse Convert.ToInt32(cmbSalesman.EditValue) = 0 Then
            MessageBox.Show("Please select a salesman.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If cmbSubGroup.EditValue Is Nothing OrElse Convert.ToInt32(cmbSubGroup.EditValue) = 0 Then
            MessageBox.Show("Please select a sub group.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If cmbItem.EditValue Is Nothing OrElse Convert.ToInt32(cmbItem.EditValue) = 0 Then
            MessageBox.Show("Please select an item.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If String.IsNullOrEmpty(cmbCommissionType.Text) Then
            MessageBox.Show("Please select a commission type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If cmbCommissionType.Text = "Percentage" AndAlso (String.IsNullOrEmpty(txtCommissionPercentage.Text) OrElse Convert.ToDecimal(txtCommissionPercentage.Text) <= 0) Then
            MessageBox.Show("Please enter a valid commission percentage.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If cmbCommissionType.Text = "Fixed Amount" AndAlso (String.IsNullOrEmpty(txtFixedAmount.Text) OrElse Convert.ToDecimal(txtFixedAmount.Text) <= 0) Then
            MessageBox.Show("Please enter a valid fixed amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    Private Sub ClearForm()
        selectedCommissionId = 0
        cmbSalesman.EditValue = Nothing
        cmbSubGroup.EditValue = Nothing
        cmbItem.EditValue = Nothing
        cmbCommissionType.SelectedIndex = 0
        txtCommissionPercentage.Text = "0.00"
        txtFixedAmount.Text = "0.00"
        chkStatus.Checked = True
        btnSave.Text = "Save Commission"
        UpdateFieldsBasedOnCommissionType()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If selectedCommissionId > 0 Then
                Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this commission?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    Dim url As String = M_Details.LinkAjaxRequest & "SalesManCommission=6&commission_id=" & selectedCommissionId
                    Dim json As String = New WebClient().DownloadString(url)
                    Dim parsedJson As JObject = JObject.Parse(json)

                    If parsedJson("Success").ToString() = "True" Then
                        MessageBox.Show(parsedJson("Msg").ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ClearForm()
                        LoadAllCommissions()
                    Else
                        MessageBox.Show(parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            Else
                MessageBox.Show("Please select a commission to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error deleting commission: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            LoadSalesmen()
            LoadSubGroups()
            If selectedEmpId > 0 Then
                LoadCommissionsBySalesman(selectedEmpId)
            Else
                LoadAllCommissions()
            End If
        Catch ex As Exception
            MessageBox.Show("Error refreshing data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        Try
            If cmbFilterSalesman.EditValue IsNot Nothing AndAlso Convert.ToInt32(cmbFilterSalesman.EditValue) > 0 Then
                selectedEmpId = Convert.ToInt32(cmbFilterSalesman.EditValue)
                LoadCommissionsBySalesman(selectedEmpId)
            Else
                selectedEmpId = 0
                LoadAllCommissions()
            End If
        Catch ex As Exception
            MessageBox.Show("Error filtering commissions: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub gvCommissions_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles gvCommissions.RowClick
        Try
            If gvCommissions.FocusedRowHandle >= 0 Then
                selectedCommissionId = If(gvCommissions.GetFocusedRowCellValue("commission_id") Is Nothing OrElse IsDBNull(gvCommissions.GetFocusedRowCellValue("commission_id")), 0, Convert.ToInt32(gvCommissions.GetFocusedRowCellValue("commission_id")))

                If selectedCommissionId > 0 Then
                    cmbSalesman.EditValue = Convert.ToInt32(gvCommissions.GetFocusedRowCellValue("emp_id"))
                    cmbSubGroup.EditValue = Convert.ToInt32(gvCommissions.GetFocusedRowCellValue("sub_group_id"))

                    ' Load items for the selected sub group first
                    LoadItemsBySubGroup(Convert.ToInt32(gvCommissions.GetFocusedRowCellValue("sub_group_id")))

                    cmbItem.EditValue = Convert.ToInt32(gvCommissions.GetFocusedRowCellValue("item_id"))
                    cmbCommissionType.Text = gvCommissions.GetFocusedRowCellValue("commission_type").ToString()
                    txtCommissionPercentage.Text = gvCommissions.GetFocusedRowCellValue("commission_percentage").ToString()
                    txtFixedAmount.Text = gvCommissions.GetFocusedRowCellValue("fixed_amount").ToString()
                    chkStatus.Checked = Convert.ToBoolean(gvCommissions.GetFocusedRowCellValue("status"))

                    btnSave.Text = "Update Commission"
                    UpdateFieldsBasedOnCommissionType()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading commission details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub gvSalesmen_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gvSalesmen.FocusedRowChanged
        Try
            If gvSalesmen.FocusedRowHandle >= 0 Then
                selectedEmpId = If(gvSalesmen.GetFocusedRowCellValue("emp_id") Is Nothing OrElse IsDBNull(gvSalesmen.GetFocusedRowCellValue("emp_id")), 0, Convert.ToInt32(gvSalesmen.GetFocusedRowCellValue("emp_id")))

                If selectedEmpId > 0 Then
                    ' Load commissions for the selected salesman
                    LoadCommissionsBySalesman(selectedEmpId)

                    ' Update filter combobox to match selection
                    cmbFilterSalesman.EditValue = selectedEmpId
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading commissions for selected salesman: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FrmSalesCommission_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Setup Filter Salesman ComboBox
            cmbFilterSalesman.Properties.DataSource = salesmenTable
            cmbFilterSalesman.Properties.DisplayMember = "emp_printname"
            cmbFilterSalesman.Properties.ValueMember = "emp_id"
        Catch ex As Exception
            MessageBox.Show("Error loading form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Bulk Update Event Handlers
    Private Sub cmbBulkSubGroup_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBulkSubGroup.EditValueChanged
        Try
            If cmbBulkSubGroup.EditValue IsNot Nothing AndAlso Not IsDBNull(cmbBulkSubGroup.EditValue) Then
                Dim subGroupId As Integer = Convert.ToInt32(cmbBulkSubGroup.EditValue)
                LoadBulkItemsBySubGroup(subGroupId)
            Else
                bulkItemsTable.Clear()
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading bulk items for sub group: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBulkSelectAll_Click(sender As Object, e As EventArgs) Handles btnBulkSelectAll.Click
        Try
            For Each row As DataRow In bulkItemsTable.Rows
                row("Selected") = True
            Next
            dgvBulkItems.RefreshDataSource()
        Catch ex As Exception
            MessageBox.Show("Error selecting all items: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBulkUnselectAll_Click(sender As Object, e As EventArgs) Handles btnBulkUnselectAll.Click
        Try
            For Each row As DataRow In bulkItemsTable.Rows
                row("Selected") = False
            Next
            dgvBulkItems.RefreshDataSource()
        Catch ex As Exception
            MessageBox.Show("Error unselecting all items: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBulkUpdate_Click(sender As Object, e As EventArgs) Handles btnBulkUpdate.Click
        Try
            ' Validate bulk input
            Dim empId As Integer = If(cmbBulkSalesman.EditValue Is Nothing, 0, Convert.ToInt32(cmbBulkSalesman.EditValue))
            Dim subGroupId As Integer = If(cmbBulkSubGroup.EditValue Is Nothing, 0, Convert.ToInt32(cmbBulkSubGroup.EditValue))
            Dim selectedItems As List(Of Integer) = GetSelectedItemIds()

            If ValidateBulkInput(empId, subGroupId, selectedItems) Then
                Dim result As DialogResult = MessageBox.Show(
                    String.Format("Are you sure you want to update commission for {0} items?", selectedItems.Count),
                    "Confirm Bulk Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If result = DialogResult.Yes Then
                    BulkUpdateCommissions(
                        empId,
                        subGroupId,
                        selectedItems,
                        cmbBulkCommissionType.Text,
                        Convert.ToDecimal(txtBulkCommissionPercentage.Text),
                        Convert.ToDecimal(txtBulkFixedAmount.Text),
                        chkBulkStatus.Checked
                    )
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error during bulk update: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbBulkCommissionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBulkCommissionType.SelectedIndexChanged
        UpdateBulkFieldsBasedOnCommissionType()
    End Sub

    Private Sub UpdateBulkFieldsBasedOnCommissionType()
        If cmbBulkCommissionType.Text = "Percentage" Then
            txtBulkCommissionPercentage.Enabled = True
            txtBulkFixedAmount.Enabled = False
            txtBulkFixedAmount.Text = "0.00"
        ElseIf cmbBulkCommissionType.Text = "Fixed Amount" Then
            txtBulkCommissionPercentage.Enabled = False
            txtBulkFixedAmount.Enabled = True
            txtBulkCommissionPercentage.Text = "0.00"
        End If
    End Sub

End Class
