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

    ' Bulk selection variables
    Private selectedSalesmenIds As New List(Of Integer)
    Private selectedSubGroupIds As New List(Of Integer)
    Private selectedSalesmenNames As New List(Of String)
    Private selectedSubGroupNames As New List(Of String)

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

    ' New method to get selected items with their sub-group information
    Private Function GetSelectedItemsWithSubGroups() As List(Of Tuple(Of Integer, Integer))
        Dim selectedItems As New List(Of Tuple(Of Integer, Integer)) ' (ItemId, SubGroupId)
        Try
            For Each row As DataRow In bulkItemsTable.Rows
                If Convert.ToBoolean(row("Selected")) Then
                    Dim itemId As Integer = Convert.ToInt32(row("ItemId"))
                    Dim subGroupId As Integer = Convert.ToInt32(row("SubGroupId"))
                    selectedItems.Add(New Tuple(Of Integer, Integer)(itemId, subGroupId))
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Error collecting selected items with sub-groups: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return selectedItems
    End Function

    Private Function ValidateBulkInput(empId As Integer, subGroupId As Integer, selectedItems As List(Of Integer)) As Boolean
        If empId = 0 AndAlso selectedSalesmenIds.Count = 0 Then
            MessageBox.Show("Please select at least one employee for bulk update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If subGroupId = 0 AndAlso selectedSubGroupIds.Count = 0 Then
            MessageBox.Show("Please select at least one sub group for bulk update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If selectedItems.Count = 0 Then
            MessageBox.Show("Please select at least one item for bulk update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    ' Bulk Salesman Selection Methods
    Private Sub AddSelectedSalesman()
        Try
            If cmbBulkSalesman.EditValue IsNot Nothing AndAlso Not IsDBNull(cmbBulkSalesman.EditValue) Then
                Dim empId As Integer = Convert.ToInt32(cmbBulkSalesman.EditValue)
                Dim empName As String = cmbBulkSalesman.Text

                If Not selectedSalesmenIds.Contains(empId) Then
                    selectedSalesmenIds.Add(empId)
                    selectedSalesmenNames.Add(empName)
                    UpdateSelectedSalesmenDisplay()
                    MessageBox.Show("Added: " & empName, "Salesman Added", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Salesman '" & empName & "' is already selected.", "Already Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error adding salesman: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RemoveSelectedSalesman()
        Try
            If cmbBulkSalesman.EditValue IsNot Nothing AndAlso Not IsDBNull(cmbBulkSalesman.EditValue) Then
                Dim empId As Integer = Convert.ToInt32(cmbBulkSalesman.EditValue)
                Dim empName As String = cmbBulkSalesman.Text

                If selectedSalesmenIds.Contains(empId) Then
                    Dim index As Integer = selectedSalesmenIds.IndexOf(empId)
                    selectedSalesmenIds.RemoveAt(index)
                    selectedSalesmenNames.RemoveAt(index)
                    UpdateSelectedSalesmenDisplay()
                    MessageBox.Show("Removed: " & empName, "Salesman Removed", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Salesman '" & empName & "' is not selected.", "Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error removing salesman: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearSelectedSalesmen()
        Try
            selectedSalesmenIds.Clear()
            selectedSalesmenNames.Clear()
            UpdateSelectedSalesmenDisplay()
            MessageBox.Show("All selected salesmen cleared.", "Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error clearing salesmen: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Bulk SubGroup Selection Methods
    Private Sub AddSelectedSubGroup()
        Try
            If cmbBulkSubGroup.EditValue IsNot Nothing AndAlso Not IsDBNull(cmbBulkSubGroup.EditValue) Then
                Dim subGroupId As Integer = Convert.ToInt32(cmbBulkSubGroup.EditValue)
                Dim subGroupName As String = cmbBulkSubGroup.Text

                If Not selectedSubGroupIds.Contains(subGroupId) Then
                    selectedSubGroupIds.Add(subGroupId)
                    selectedSubGroupNames.Add(subGroupName)
                    UpdateSelectedSubGroupsDisplay()
                    MessageBox.Show("Added: " & subGroupName, "Sub Group Added", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Sub Group '" & subGroupName & "' is already selected.", "Already Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error adding sub group: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RemoveSelectedSubGroup()
        Try
            If cmbBulkSubGroup.EditValue IsNot Nothing AndAlso Not IsDBNull(cmbBulkSubGroup.EditValue) Then
                Dim subGroupId As Integer = Convert.ToInt32(cmbBulkSubGroup.EditValue)
                Dim subGroupName As String = cmbBulkSubGroup.Text

                If selectedSubGroupIds.Contains(subGroupId) Then
                    Dim index As Integer = selectedSubGroupIds.IndexOf(subGroupId)
                    selectedSubGroupIds.RemoveAt(index)
                    selectedSubGroupNames.RemoveAt(index)
                    UpdateSelectedSubGroupsDisplay()
                    MessageBox.Show("Removed: " & subGroupName, "Sub Group Removed", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Sub Group '" & subGroupName & "' is not selected.", "Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error removing sub group: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearSelectedSubGroups()
        Try
            selectedSubGroupIds.Clear()
            selectedSubGroupNames.Clear()
            UpdateSelectedSubGroupsDisplay()
            MessageBox.Show("All selected sub groups cleared.", "Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error clearing sub groups: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Update Display Methods (These will need UI controls to display selected items)
    Private Sub UpdateSelectedSalesmenDisplay()
        ' This method will update a label or text box showing selected salesmen
        ' You'll need to add a label control to your form for this
        Try
            Dim displayText As String = If(selectedSalesmenNames.Count > 0,
                                         "Selected (" & selectedSalesmenNames.Count.ToString() & "): " & String.Join(", ", selectedSalesmenNames),
                                         "No salesmen selected")
            ' Assuming you have a label called lblSelectedSalesmen
            ' lblSelectedSalesmen.Text = displayText
            Console.WriteLine("Selected Salesmen: " & displayText)
        Catch ex As Exception
            Console.WriteLine("Error updating salesmen display: " & ex.Message)
        End Try
    End Sub

    Private Sub UpdateSelectedSubGroupsDisplay()
        ' This method will update a label or text box showing selected sub groups
        ' You'll need to add a label control to your form for this
        Try
            Dim displayText As String = If(selectedSubGroupNames.Count > 0,
                                         "Selected (" & selectedSubGroupNames.Count.ToString() & "): " & String.Join(", ", selectedSubGroupNames),
                                         "No sub groups selected")
            ' Assuming you have a label called lblSelectedSubGroups
            ' lblSelectedSubGroups.Text = displayText
            Console.WriteLine("Selected Sub Groups: " & displayText)
        Catch ex As Exception
            Console.WriteLine("Error updating sub groups display: " & ex.Message)
        End Try
    End Sub

    ' Helper methods for CheckedComboBoxEdit checkbox functionality
    Private Sub UpdateSelectedSalesmenFromComboBox()
        ' This method works with CheckedComboBoxEdit controls with checkboxes
        Try
            selectedSalesmenIds.Clear()
            selectedSalesmenNames.Clear()

            ' Get checked values from CheckedComboBoxEdit (using EditValue for multi-select)
            If cmbBulkSalesman.EditValue IsNot Nothing Then
                Dim editValue As String = cmbBulkSalesman.EditValue.ToString()
                If Not String.IsNullOrEmpty(editValue) Then
                    ' Split the comma-separated values
                    Dim valueStrings As String() = editValue.Split(","c)
                    For Each valueStr As String In valueStrings
                        If Not String.IsNullOrEmpty(valueStr.Trim()) Then
                            Dim empId As Integer = Convert.ToInt32(valueStr.Trim())
                            selectedSalesmenIds.Add(empId)

                            ' Find the corresponding name from the data source
                            For Each row As DataRow In salesmenTable.Rows
                                If Convert.ToInt32(row("emp_id")) = empId Then
                                    selectedSalesmenNames.Add(row("emp_printname").ToString())
                                    Exit For
                                End If
                            Next
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Console.WriteLine("Error updating selected salesmen from combo box: " & ex.Message)
        End Try
    End Sub

    Private Sub UpdateSelectedSubGroupsFromComboBox()
        ' This method works with CheckedComboBoxEdit controls with checkboxes
        Try
            selectedSubGroupIds.Clear()
            selectedSubGroupNames.Clear()

            ' Get checked values from CheckedComboBoxEdit (using EditValue for multi-select)
            If cmbBulkSubGroup.EditValue IsNot Nothing Then
                Dim editValue As String = cmbBulkSubGroup.EditValue.ToString()
                If Not String.IsNullOrEmpty(editValue) Then
                    ' Split the comma-separated values
                    Dim valueStrings As String() = editValue.Split(","c)
                    For Each valueStr As String In valueStrings
                        If Not String.IsNullOrEmpty(valueStr.Trim()) Then
                            Dim subGroupId As Integer = Convert.ToInt32(valueStr.Trim())
                            selectedSubGroupIds.Add(subGroupId)

                            ' Find the corresponding name from the data source
                            For Each row As DataRow In subGroupsTable.Rows
                                If Convert.ToInt32(row("SubGroupId")) = subGroupId Then
                                    selectedSubGroupNames.Add(row("SubGroupName").ToString())
                                    Exit For
                                End If
                            Next
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Console.WriteLine("Error updating selected sub groups from combo box: " & ex.Message)
        End Try
    End Sub

    Private Sub BulkUpdateCommissions(empId As Integer, subGroupId As Integer, selectedItems As List(Of Integer),
                                      commissionType As String, commissionPercentage As Decimal, fixedAmount As Decimal, status As Boolean)
        Try
            Dim successCount As Integer = 0
            Dim errorCount As Integer = 0
            Dim errors As New List(Of String)
            Dim totalOperations As Integer = 0

            ' Get selected items with their sub-group information
            Dim selectedItemsWithSubGroups As List(Of Tuple(Of Integer, Integer)) = GetSelectedItemsWithSubGroups()

            ' Determine which salesmen to use
            Dim salesmenToProcess As List(Of Integer) = If(selectedSalesmenIds.Count > 0, selectedSalesmenIds, New List(Of Integer) From {empId})

            ' Calculate total operations for progress tracking
            ' Each item is processed only with its own sub-group, for each selected salesman
            totalOperations = salesmenToProcess.Count * selectedItemsWithSubGroups.Count

            Dim currentOperation As Integer = 0

            ' Process each combination of salesman and item (with its correct sub-group)
            For Each salesmanId As Integer In salesmenToProcess
                For Each itemWithSubGroup In selectedItemsWithSubGroups
                    Try
                        currentOperation += 1

                        Dim itemId As Integer = itemWithSubGroup.Item1
                        Dim itemSubGroupId As Integer = itemWithSubGroup.Item2

                        ' Update progress (optional - you can add a progress bar)
                        Console.WriteLine("Processing " & currentOperation.ToString() & "/" & totalOperations.ToString() & ": Salesman " & salesmanId.ToString() & ", SubGroup " & itemSubGroupId.ToString() & ", Item " & itemId.ToString())

                        Dim commissionData As New Dictionary(Of String, Object)
                        commissionData("emp_id") = salesmanId
                        commissionData("item_id") = itemId
                        commissionData("sub_group_id") = itemSubGroupId
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
                            errors.Add("S:" & salesmanId.ToString() & ", SG:" & itemSubGroupId.ToString() & ", I:" & itemId.ToString() & " - " & parsedJson("Msg").ToString())
                        End If

                    Catch ex As Exception
                        errorCount += 1
                        Dim itemId As Integer = itemWithSubGroup.Item1
                        Dim itemSubGroupId As Integer = itemWithSubGroup.Item2
                        errors.Add("S:" & salesmanId.ToString() & ", SG:" & itemSubGroupId.ToString() & ", I:" & itemId.ToString() & " - " & ex.Message)
                    End Try
                Next
            Next

            ' Show result summary
            Dim message As String = String.Format("Bulk Update Complete!{0}Total Operations: {1}{0}Success: {2}{0}Errors: {3}{0}Salesmen: {4}{0}Items: {5}",
                                                 Environment.NewLine, totalOperations, successCount, errorCount,
                                                 salesmenToProcess.Count, selectedItemsWithSubGroups.Count)

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
        bulkItemsTable.Columns.Add("SubGroupId", GetType(Integer)) ' Track which sub-group each item belongs to
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

        ' Setup Bulk Salesman CheckedComboBoxEdit with checkboxes
        cmbBulkSalesman.Properties.DataSource = salesmenTable
        cmbBulkSalesman.Properties.DisplayMember = "emp_printname"
        cmbBulkSalesman.Properties.ValueMember = "emp_id"
        ' Enable checkbox functionality
        cmbBulkSalesman.Properties.SelectAllItemVisible = True
        cmbBulkSalesman.Properties.AllowMultiSelect = True

        ' Setup Bulk Sub Group CheckedComboBoxEdit with checkboxes
        cmbBulkSubGroup.Properties.DataSource = subGroupsTable
        cmbBulkSubGroup.Properties.DisplayMember = "SubGroupName"
        cmbBulkSubGroup.Properties.ValueMember = "SubGroupId"
        ' Enable checkbox functionality
        cmbBulkSubGroup.Properties.SelectAllItemVisible = True
        cmbBulkSubGroup.Properties.AllowMultiSelect = True

        ' Note: CheckedComboBoxEdit uses EditValueChanged event instead of ItemCheck

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

                        bulkItemsTable.Rows.Add(False, itemId, itemName, subGroupId)
                    Next

                    MessageBox.Show("Loaded " & bulkItemsTable.Rows.Count & " items for bulk selection.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Failed to load bulk items: " & parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

                ' Refresh the grid to display the updated data
                dgvBulkItems.RefreshDataSource()
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading bulk items: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' New method to load items for multiple selected sub-groups
    Private Sub LoadBulkItemsForSelectedSubGroups()
        Try
            If selectedSubGroupIds.Count = 0 Then
                bulkItemsTable.Clear()
                dgvBulkItems.RefreshDataSource()
                Return
            End If

            bulkItemsTable.Clear()
            Dim allItemIds As New HashSet(Of Integer) ' To avoid duplicate items
            Dim totalItemsLoaded As Integer = 0

            For Each subGroupId As Integer In selectedSubGroupIds
                Try
                    Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=3&sub_group_id=" & subGroupId)
                    Dim parsedJson As JObject = JObject.Parse(json)

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

                            ' Only add if not already added (avoid duplicates)
                            If itemId > 0 AndAlso Not allItemIds.Contains(itemId) Then
                                bulkItemsTable.Rows.Add(False, itemId, itemName, subGroupId)
                                allItemIds.Add(itemId)
                                totalItemsLoaded += 1
                            End If
                        Next
                    End If
                Catch subEx As Exception
                    Console.WriteLine("Error loading items for sub group " & subGroupId & ": " & subEx.Message)
                End Try
            Next

            ' Refresh the grid to display the updated data
            dgvBulkItems.RefreshDataSource()

            If totalItemsLoaded > 0 Then
                MessageBox.Show("Loaded " & totalItemsLoaded & " items from " & selectedSubGroupIds.Count & " selected sub-groups for bulk selection.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("No items found for the selected sub-groups.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading bulk items for multiple sub-groups: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Helper method to refresh bulk items display whenever selections change
    Private Sub RefreshBulkItemsDisplay()
        Try
            ' Always reload items based on current sub-group selections
            LoadBulkItemsForSelectedSubGroups()
        Catch ex As Exception
            Console.WriteLine("Error refreshing bulk items display: " & ex.Message)
        End Try
    End Sub

    ' Method to clear bulk items display
    Private Sub ClearBulkItemsDisplay()
        Try
            bulkItemsTable.Clear()
            dgvBulkItems.RefreshDataSource()
        Catch ex As Exception
            Console.WriteLine("Error clearing bulk items display: " & ex.Message)
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

    ' Bulk Selection Button Event Handlers
    ' Note: You'll need to add these buttons to your form design

    ' Helper method to clear all checkbox selections
    Private Sub ClearAllComboBoxSelections()
        Try
            ' Clear CheckedComboBoxEdit selections (reset EditValue to Nothing)
            cmbBulkSalesman.EditValue = Nothing
            cmbBulkSubGroup.EditValue = Nothing

            ' Clear our internal lists
            selectedSalesmenIds.Clear()
            selectedSalesmenNames.Clear()
            selectedSubGroupIds.Clear()
            selectedSubGroupNames.Clear()

            ' Update displays
            UpdateSelectedSalesmenDisplay()
            UpdateSelectedSubGroupsDisplay()
        Catch ex As Exception
            Console.WriteLine("Error clearing selections: " & ex.Message)
        End Try
    End Sub

    ' Helper methods to programmatically check/uncheck specific items
    Private Sub CheckSalesmanById(empId As Integer)
        Try
            ' Add specific salesman to CheckedComboBoxEdit selection
            Dim currentValue As String = If(cmbBulkSalesman.EditValue Is Nothing, "", cmbBulkSalesman.EditValue.ToString())
            Dim values As New List(Of String)

            ' Parse existing values
            If Not String.IsNullOrEmpty(currentValue) Then
                values.AddRange(currentValue.Split(","c).Select(Function(v) v.Trim()).Where(Function(v) Not String.IsNullOrEmpty(v)))
            End If

            ' Add new value if not already present
            Dim empIdStr As String = empId.ToString()
            If Not values.Contains(empIdStr) Then
                values.Add(empIdStr)
                cmbBulkSalesman.EditValue = String.Join(",", values)
            End If

            UpdateSelectedSalesmenFromComboBox()
            UpdateSelectedSalesmenDisplay()
        Catch ex As Exception
            Console.WriteLine("Error checking salesman: " & ex.Message)
        End Try
    End Sub

    Private Sub UncheckSalesmanById(empId As Integer)
        Try
            ' Remove specific salesman from CheckedComboBoxEdit selection
            Dim currentValue As String = If(cmbBulkSalesman.EditValue Is Nothing, "", cmbBulkSalesman.EditValue.ToString())
            If Not String.IsNullOrEmpty(currentValue) Then
                Dim values As New List(Of String)
                values.AddRange(currentValue.Split(","c).Select(Function(v) v.Trim()).Where(Function(v) Not String.IsNullOrEmpty(v)))

                ' Remove the value
                Dim empIdStr As String = empId.ToString()
                values.Remove(empIdStr)

                cmbBulkSalesman.EditValue = If(values.Count > 0, String.Join(",", values), Nothing)
            End If

            UpdateSelectedSalesmenFromComboBox()
            UpdateSelectedSalesmenDisplay()
        Catch ex As Exception
            Console.WriteLine("Error unchecking salesman: " & ex.Message)
        End Try
    End Sub

    Private Sub CheckSubGroupById(subGroupId As Integer)
        Try
            ' Add specific sub group to CheckedComboBoxEdit selection
            Dim currentValue As String = If(cmbBulkSubGroup.EditValue Is Nothing, "", cmbBulkSubGroup.EditValue.ToString())
            Dim values As New List(Of String)

            ' Parse existing values
            If Not String.IsNullOrEmpty(currentValue) Then
                values.AddRange(currentValue.Split(","c).Select(Function(v) v.Trim()).Where(Function(v) Not String.IsNullOrEmpty(v)))
            End If

            ' Add new value if not already present
            Dim subGroupIdStr As String = subGroupId.ToString()
            If Not values.Contains(subGroupIdStr) Then
                values.Add(subGroupIdStr)
                cmbBulkSubGroup.EditValue = String.Join(",", values)
            End If

            UpdateSelectedSubGroupsFromComboBox()
            UpdateSelectedSubGroupsDisplay()
        Catch ex As Exception
            Console.WriteLine("Error checking sub group: " & ex.Message)
        End Try
    End Sub

    Private Sub UncheckSubGroupById(subGroupId As Integer)
        Try
            ' Remove specific sub group from CheckedComboBoxEdit selection
            Dim currentValue As String = If(cmbBulkSubGroup.EditValue Is Nothing, "", cmbBulkSubGroup.EditValue.ToString())
            If Not String.IsNullOrEmpty(currentValue) Then
                Dim values As New List(Of String)
                values.AddRange(currentValue.Split(","c).Select(Function(v) v.Trim()).Where(Function(v) Not String.IsNullOrEmpty(v)))

                ' Remove the value
                Dim subGroupIdStr As String = subGroupId.ToString()
                values.Remove(subGroupIdStr)

                cmbBulkSubGroup.EditValue = If(values.Count > 0, String.Join(",", values), Nothing)
            End If

            UpdateSelectedSubGroupsFromComboBox()
            UpdateSelectedSubGroupsDisplay()
        Catch ex As Exception
            Console.WriteLine("Error unchecking sub group: " & ex.Message)
        End Try
    End Sub

    ' Optional convenience buttons (checkboxes work automatically now)
    ' These buttons are now optional since users can directly check/uncheck in the ComboBoxes
    '
    ' TO ENABLE BUTTONS: Add these button controls to your form design, then uncomment the Handles clauses:
    ' - btnAddSalesman, btnRemoveSalesman, btnClearSalesmen
    ' - btnAddSubGroup, btnRemoveSubGroup, btnClearSubGroups
    '
    Private Sub btnAddSalesman_Click(sender As Object, e As EventArgs) ' Handles btnAddSalesman.Click
        ' This button is now optional - users can directly check items in cmbBulkSalesman
        AddSelectedSalesman()
    End Sub

    Private Sub btnRemoveSalesman_Click(sender As Object, e As EventArgs) ' Handles btnRemoveSalesman.Click
        ' This button is now optional - users can directly uncheck items in cmbBulkSalesman
        RemoveSelectedSalesman()
    End Sub

    Private Sub btnClearSalesmen_Click(sender As Object, e As EventArgs) ' Handles btnClearSalesmen.Click
        ' Clear all selected salesmen checkboxes
        ClearAllComboBoxSelections()
    End Sub

    ' SubGroup bulk selection buttons (optional convenience methods)
    Private Sub btnAddSubGroup_Click(sender As Object, e As EventArgs) ' Handles btnAddSubGroup.Click
        ' This button is now optional - users can directly check items in cmbBulkSubGroup
        AddSelectedSubGroup()
    End Sub

    Private Sub btnRemoveSubGroup_Click(sender As Object, e As EventArgs) ' Handles btnRemoveSubGroup.Click
        ' This button is now optional - users can directly uncheck items in cmbBulkSubGroup
        RemoveSelectedSubGroup()
    End Sub

    Private Sub btnClearSubGroups_Click(sender As Object, e As EventArgs) ' Handles btnClearSubGroups.Click
        ' Clear all selected sub group checkboxes
        ClearAllComboBoxSelections()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            ' Clear all bulk selections when refreshing using the new method
            ClearAllComboBoxSelections()

            ' Refresh data
            LoadAllCommissions()
            LoadSalesmen()
            LoadSubGroups()
            ' Note: LoadItems needs a subGroupId parameter, so we'll skip it here
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

    ' Bulk Update Event Handlers with Checkbox Selection
    Private Sub cmbBulkSalesman_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBulkSalesman.EditValueChanged
        Try
            ' Update selected salesmen based on checked items
            UpdateSelectedSalesmenFromComboBox()
            UpdateSelectedSalesmenDisplay()

            ' Refresh items display - items depend on sub-group selections primarily
            RefreshBulkItemsDisplay()

        Catch ex As Exception
            MessageBox.Show("Error updating selected salesmen: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Note: CheckedComboBoxEdit doesn't support ItemCheck events
    ' Using EditValueChanged event instead for checkbox change detection

    Private Sub cmbBulkSubGroup_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBulkSubGroup.EditValueChanged
        Try
            ' Update selected sub groups based on checked items
            UpdateSelectedSubGroupsFromComboBox()
            UpdateSelectedSubGroupsDisplay()

            ' Load items for ALL selected sub groups to populate the bulk items grid
            RefreshBulkItemsDisplay()

        Catch ex As Exception
            MessageBox.Show("Error updating selected sub groups: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Note: CheckedComboBoxEdit uses EditValueChanged event instead of ItemCheck
    ' The cmbBulkSubGroup_EditValueChanged event handles selection changes for sub groups

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
            ' Get selected items from grid
            Dim selectedItems As List(Of Integer) = GetSelectedItemIds()

            ' Validate bulk input using the new multi-selection system
            If ValidateBulkInput(0, 0, selectedItems) Then
                ' Determine what will be processed based on actual selections
                Dim salesmenCount As Integer = If(selectedSalesmenIds.Count > 0, selectedSalesmenIds.Count, 0)
                Dim subGroupsCount As Integer = If(selectedSubGroupIds.Count > 0, selectedSubGroupIds.Count, 0)
                Dim totalOperations As Integer = salesmenCount * subGroupsCount * selectedItems.Count

                ' Build confirmation message with selected details
                Dim confirmMessage As String = String.Format(
                    "Are you sure you want to update commission for:{0}" &
                    "• {1} Selected Salesmen: {2}{0}" &
                    "• {3} Selected Sub Groups: {4}{0}" &
                    "• {5} Selected Items{0}" &
                    "Total Operations: {6}",
                    Environment.NewLine,
                    salesmenCount, String.Join(", ", selectedSalesmenNames),
                    subGroupsCount, String.Join(", ", selectedSubGroupNames),
                    selectedItems.Count, totalOperations)

                Dim result As DialogResult = MessageBox.Show(confirmMessage,
                    "Confirm Bulk Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If result = DialogResult.Yes Then
                    ' The existing BulkUpdateCommissions method already handles multi-selection
                    ' It checks selectedSalesmenIds and selectedSubGroupIds internally
                    BulkUpdateCommissions(0, 0, selectedItems,
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

    Private Sub btnbulkdeletebysalesmanid_Click(sender As Object, e As EventArgs) Handles btnbulkdeletebysalesmanid.Click
        Try
            If selectedEmpId > 0 Then
                Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this salesman?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    Dim url As String = M_Details.LinkAjaxRequest & "SalesManCommission=14&salesman_id=" & selectedEmpId
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
End Class
