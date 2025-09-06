Imports System.Data
Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class FrmMainGroupPolicy

    ' Data variables
    Private mainGroupsTable As DataTable ' Available main groups (left side)
    Private policyTable As DataTable     ' Policy data (right side)
    Private selectedMainGroupId As Integer = 0

    Public Sub New()
        InitializeComponent()
        InitializeForm()
    End Sub

    Private Sub InitializeForm()
        Try
            ' Set form properties
            Me.Text = "Main Group Policy Management"
            Me.WindowState = FormWindowState.Maximized

            ' Initialize data tables
            SetupDataTables()

            ' Load initial data
            LoadAvailableMainGroups()
            LoadMainGroupPolicies()

            ' Setup form controls
            SetupFormControls()

        Catch ex As Exception
            MessageBox.Show("Error initializing form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetupDataTables()
        ' Left side: Available main groups
        mainGroupsTable = New DataTable()
        mainGroupsTable.Columns.Add("mainid", GetType(Integer))
        mainGroupsTable.Columns.Add("mainname", GetType(String))
        mainGroupsTable.Columns.Add("mainstatus", GetType(Boolean))

        ' Right side: Policy data
        policyTable = New DataTable()
        policyTable.Columns.Add("mainpolicyid", GetType(Integer))
        policyTable.Columns.Add("mainid", GetType(Integer))
        policyTable.Columns.Add("mainname", GetType(String))
        policyTable.Columns.Add("mainstatus", GetType(Boolean))
        policyTable.Columns.Add("comid", GetType(Integer))
        policyTable.Columns.Add("companyname", GetType(String))
        policyTable.Columns.Add("locid", GetType(Integer))
        policyTable.Columns.Add("locationname", GetType(String))
        policyTable.Columns.Add("created_date", GetType(DateTime))
    End Sub

    Private Sub SetupFormControls()
        ' Setup left grid (available main groups)
        dgvMainGroups.DataSource = mainGroupsTable
        gvMainGroups.Columns("mainid").Caption = "ID"
        gvMainGroups.Columns("mainid").Width = 60
        gvMainGroups.Columns("mainname").Caption = "Main Group Name"
        gvMainGroups.Columns("mainname").Width = 200
        gvMainGroups.Columns("mainstatus").Caption = "Status"
        gvMainGroups.Columns("mainstatus").Width = 80

        ' Setup right grid (policy data)
        dgvPolicies.DataSource = policyTable
        gvPolicies.Columns("mainpolicyid").Caption = "Policy ID"
        gvPolicies.Columns("mainpolicyid").Width = 80
        gvPolicies.Columns("mainid").Caption = "Group ID"
        gvPolicies.Columns("mainid").Width = 80
        gvPolicies.Columns("mainname").Caption = "Main Group Name"
        gvPolicies.Columns("mainname").Width = 200
        gvPolicies.Columns("mainstatus").Caption = "Status"
        gvPolicies.Columns("mainstatus").Width = 80
        gvPolicies.Columns("comid").Caption = "Company ID"
        gvPolicies.Columns("comid").Width = 80
        gvPolicies.Columns("companyname").Caption = "Company Name"
        gvPolicies.Columns("companyname").Width = 150
        gvPolicies.Columns("locid").Caption = "Location ID"
        gvPolicies.Columns("locid").Width = 80
        gvPolicies.Columns("locationname").Caption = "Location Name"
        gvPolicies.Columns("locationname").Width = 150
        gvPolicies.Columns("created_date").Caption = "Created Date"
        gvPolicies.Columns("created_date").Width = 120

        ' Enable/disable controls
        EnableControls(True)
    End Sub

    Private Sub LoadAvailableMainGroups()
        Try
            mainGroupsTable.Rows.Clear()
            If _JsonData.MainGroupTable.Rows.Count > 0 Then
                For Each row In _JsonData.MainGroupTable.Rows
                    Dim newRow As DataRow = mainGroupsTable.NewRow()
                    newRow("mainid") = Convert.ToInt32(row("MainId"))
                    newRow("mainname") = row("MainName").ToString()
                    ' Convert Active field: 1 = True, 0 = False
                    newRow("mainstatus") = If(Convert.ToInt32(row("Active")) = 1, True, False)
                    mainGroupsTable.Rows.Add(newRow)
                Next
            End If
            dgvMainGroups.DataSource = mainGroupsTable
            dgvMainGroups.RefreshDataSource()

        Catch ex As Exception
            MessageBox.Show("Error loading available main groups: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadDefaultMainGroups()
        ' Alias for LoadAvailableMainGroups - used for refresh
        LoadAvailableMainGroups()
    End Sub

    Private Sub LoadMainGroupPolicies()
        Try
            Dim url As String = M_Details.LinkAjaxRequest & "MgmtRequest=9&Operation=GET&Comid=" & _companyInfo.ComId & "&Locid=" & _companyInfo.LocId

            Dim json As String = New WebClient().DownloadString(url)
            Dim parsedJson As JObject = JObject.Parse(json)

            policyTable.Clear()

            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")

                For Each item In dataArray
                    Dim row As DataRow = policyTable.NewRow()
                    row("mainpolicyid") = If(item("mainpolicyid") IsNot Nothing, Convert.ToInt32(item("mainpolicyid")), 0)
                    row("mainid") = If(item("mainid") IsNot Nothing, Convert.ToInt32(item("mainid")), 0)
                    row("mainname") = If(item("mainname") IsNot Nothing, item("mainname").ToString(), "")
                    ' Convert mainstatus field: 1 = True, 0 = False
                    row("mainstatus") = If(item("mainstatus") IsNot Nothing, If(Convert.ToInt32(item("mainstatus")) = 1, True, False), True)
                    row("comid") = If(item("comid") IsNot Nothing, Convert.ToInt32(item("comid")), _companyInfo.ComId)
                    row("companyname") = If(item("companyname") IsNot Nothing, item("companyname").ToString(), "")
                    row("locid") = If(item("locid") IsNot Nothing, Convert.ToInt32(item("locid")), _companyInfo.LocId)
                    row("locationname") = If(item("locationname") IsNot Nothing, item("locationname").ToString(), "")
                    row("created_date") = If(item("created_date") IsNot Nothing, Convert.ToDateTime(item("created_date")), DateTime.Now)
                    policyTable.Rows.Add(row)
                Next
                dgvPolicies.DataSource = policyTable
                dgvPolicies.RefreshDataSource()
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading main group policies: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddToPolicyFromSelectedGroup()
        Try
            If selectedMainGroupId = 0 Then
                MessageBox.Show("Please select a main group from the left list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get selected main group details
            Dim selectedRow As DataRow = Nothing
            For Each row As DataRow In mainGroupsTable.Rows
                If Convert.ToInt32(row("mainid")) = selectedMainGroupId Then
                    selectedRow = row
                    Exit For
                End If
            Next

            If selectedRow Is Nothing Then
                MessageBox.Show("Selected main group not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Check if already exists in policy
            For Each row As DataRow In policyTable.Rows
                If Convert.ToInt32(row("mainid")) = selectedMainGroupId Then
                    MessageBox.Show("This main group is already added to the policy.", "Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If
            Next

            ' Add to policy via API
            Dim mainName As String = selectedRow("mainname").ToString()
            Dim mainStatus As Integer = If(Convert.ToBoolean(selectedRow("mainstatus")), 1, 0)

            Dim url As String = M_Details.LinkAjaxRequest & "MgmtRequest=9" &
                "&Operation=INSERT" &
                "&Comid=" & _companyInfo.ComId &
                "&Locid=" & _companyInfo.LocId &
                "&MainName=" & Uri.EscapeDataString(mainName) &
                "&MainStatus=" & mainStatus

            Dim json As String = New WebClient().DownloadString(url)
            Dim parsedJson As JObject = JObject.Parse(json)

            If parsedJson("Success").ToString() = "True" Then
                MessageBox.Show("Main group added to policy successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadMainGroupPolicies() ' Refresh the policy list
            Else
                MessageBox.Show("Error: " & parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error adding to policy: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RemoveFromPolicy()
        Try
            If gvPolicies.FocusedRowHandle < 0 Then
                MessageBox.Show("Please select a policy record to remove.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim selectedMainId As Integer = Convert.ToInt32(gvPolicies.GetRowCellValue(gvPolicies.FocusedRowHandle, "mainid"))
            Dim mainName As String = gvPolicies.GetRowCellValue(gvPolicies.FocusedRowHandle, "mainname").ToString()

            Dim result As DialogResult = MessageBox.Show("Are you sure you want to remove '" & mainName & "' from the policy?", "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                Dim url As String = M_Details.LinkAjaxRequest & "MgmtRequest=9" &
                    "&Operation=DELETE" &
                    "&Comid=" & _companyInfo.ComId &
                    "&Locid=" & _companyInfo.LocId &
                    "&MainId=" & selectedMainId

                Dim json As String = New WebClient().DownloadString(url)
                Dim parsedJson As JObject = JObject.Parse(json)

                If parsedJson("Success").ToString() = "True" Then
                    MessageBox.Show("Main group removed from policy successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadMainGroupPolicies() ' Refresh the policy list
                Else
                    MessageBox.Show("Error: " & parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Error removing from policy: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdatePolicyStatus()
        Try
            If gvPolicies.FocusedRowHandle < 0 Then
                MessageBox.Show("Please select a policy record to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim selectedMainId As Integer = Convert.ToInt32(gvPolicies.GetRowCellValue(gvPolicies.FocusedRowHandle, "mainid"))
            Dim mainName As String = gvPolicies.GetRowCellValue(gvPolicies.FocusedRowHandle, "mainname").ToString()
            Dim currentStatus As Boolean = Convert.ToBoolean(gvPolicies.GetRowCellValue(gvPolicies.FocusedRowHandle, "mainstatus"))

            ' Toggle status
            Dim newStatus As Integer = If(currentStatus, 0, 1)

            Dim url As String = M_Details.LinkAjaxRequest & "MgmtRequest=9" &
                "&Operation=UPDATE" &
                "&Comid=" & _companyInfo.ComId &
                "&Locid=" & _companyInfo.LocId &
                "&MainId=" & selectedMainId &
                "&MainName=" & Uri.EscapeDataString(mainName) &
                "&MainStatus=" & newStatus

            Dim json As String = New WebClient().DownloadString(url)
            Dim parsedJson As JObject = JObject.Parse(json)

            If parsedJson("Success").ToString() = "True" Then
                MessageBox.Show("Policy status updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadMainGroupPolicies() ' Refresh the policy list
            Else
                MessageBox.Show("Error: " & parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error updating policy: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub ClearForm()
        selectedMainGroupId = 0
        txtMainName.Text = ""
        EnableControls(True)
    End Sub


    Private Sub EnableControls(enabled As Boolean)
        txtMainName.Enabled = enabled
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    ' Event Handlers
    Private Sub btnAddToPolicy_Click(sender As Object, e As EventArgs) Handles btnAddToPolicy.Click
        AddToPolicyFromSelectedGroup()
    End Sub

    Private Sub btnRemoveFromPolicy_Click(sender As Object, e As EventArgs) Handles btnRemoveFromPolicy.Click
        RemoveFromPolicy()
    End Sub

    Private Sub btnUpdateStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
        UpdatePolicyStatus()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadAvailableMainGroups()
        LoadMainGroupPolicies()
    End Sub

    Private Sub gvMainGroups_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles gvMainGroups.RowClick
        Try
            If e.RowHandle >= 0 Then
                Dim view As GridView = TryCast(sender, GridView)
                selectedMainGroupId = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "mainid"))
                txtMainName.Text = view.GetRowCellValue(e.RowHandle, "mainname").ToString()
            End If
        Catch ex As Exception
            MessageBox.Show("Error selecting main group: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub gvMainGroups_DoubleClick(sender As Object, e As EventArgs) Handles gvMainGroups.DoubleClick
        ' Double-click to add to policy
        AddToPolicyFromSelectedGroup()
    End Sub

    Private Sub gvPolicies_DoubleClick(sender As Object, e As EventArgs) Handles gvPolicies.DoubleClick
        ' Double-click to toggle status
        UpdatePolicyStatus()
    End Sub
    Private Sub btnLoadByContext_Click(sender As Object, e As EventArgs)
        LoadMainGroupPolicies()
    End Sub

End Class
