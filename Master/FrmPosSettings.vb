Imports System.Net
Imports Newtonsoft.Json.Linq
Imports DevExpress.XtraEditors
Imports Newtonsoft.Json
Imports System.Data

Public Class FrmPosSettings
    Dim _posSettingsTable As DataTable
    Dim _currentMode As String = "NEW"

    Private Sub FrmPosSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = "POS Settings Manager - " & M_Details.SoftwareVersion
            Me.WindowState = FormWindowState.Maximized
            _InitializeForm()
            _ConfigureGridColumns()  ' Configure grid column display
            _LoadData()
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub _InitializeForm()
        Try
            ' Initialize form state
            _ClearInputs()
            btnSave.Text = "Save"
            cmbType.Properties.Items.Clear()
            ' Using simple string array for DevExpress ComboBox
            cmbType.Properties.Items.AddRange(New String() {"Values", "Status"})
            cmbType.EditValue = "Values"

            cmbStatus.Properties.Items.Clear()
            cmbStatus.Properties.Items.AddRange(New String() {"Active", "Inactive"})
            cmbStatus.EditValue = "Active"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub _ClearInputs()
        Try
            txtId.Text = ""
            txtName.Text = ""
            txtValue.Text = ""
            cmbType.EditValue = "Values"  ' Default to Values type
            cmbStatus.EditValue = "Active"  ' Default to Active
            btnSave.Text = "Save"
            _currentMode = "NEW"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub _LoadData()
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            dialog.Caption = "Loading POS Settings..."
            dialog.Show()

            _posSettingsTable = New DataTable()
            _posSettingsTable.TableName = "PosSettingsTable"

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "GroupPolicyRequest=8&operation=SELECT")

            If String.IsNullOrWhiteSpace(json) Then
                GridControl1.DataSource = Nothing
                XtraMessageBox.Show("No data received from server", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim parsedJson As JObject = JObject.Parse(json)

            If parsedJson("Success").ToString = "True" Then
                dialog.Caption = "Data received..."
                Dim dataArray As JArray = parsedJson("Data")
                If dataArray IsNot Nothing AndAlso dataArray.Count > 0 Then
                    Try
                        _posSettingsTable = dataArray.ToObject(Of DataTable)()

                        ' Ensure required columns exist
                        If Not _ValidateDataTableStructure(_posSettingsTable) Then
                            GridControl1.DataSource = Nothing
                            XtraMessageBox.Show("Invalid data structure received from server", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return
                        End If

                        GridControl1.DataSource = _posSettingsTable

                        ' Configure custom column display
                        _ConfigureGridColumns()
                    Catch convertEx As Exception
                        GridControl1.DataSource = Nothing
                        XtraMessageBox.Show("Error converting data: " & convertEx.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                Else
                    GridControl1.DataSource = Nothing
                    XtraMessageBox.Show("No POS Settings data found", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                GridControl1.DataSource = Nothing
                XtraMessageBox.Show("Failed to load POS Settings: " & parsedJson("Msg").ToString, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            GridControl1.DataSource = Nothing
            XtraMessageBox.Show("Error loading data: " & ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dialog.Close()
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            If Not _ValidateInputs() Then Return

            dialog.Caption = "Saving POS Setting..."
            dialog.Show()

            Dim settingData As New PosSettingData()
            settingData.Name = txtName.Text.Trim()
            settingData.Value = txtValue.Text.Trim()
            ' Convert string value to integer for Type field
            Dim typeValue As Integer = 0
            If cmbType.EditValue IsNot Nothing Then
                Select Case cmbType.EditValue.ToString()
                    Case "Values"
                        typeValue = 1
                    Case "Status"
                        typeValue = 0
                    Case Else
                        typeValue = 0
                End Select
            End If
            settingData.Type = typeValue

            ' Convert string value to integer for Status field
            Dim statusValue As Integer = 1
            If cmbStatus.EditValue IsNot Nothing Then
                statusValue = If(cmbStatus.EditValue.ToString() = "Active", 1, 0)
            End If
            settingData.Status = statusValue

            Dim operation As String = "INSERT"
            If _currentMode = "UPDATE" Then
                settingData.Id = Integer.Parse(txtId.Text)
                operation = "UPDATE"
            End If

            Dim requestData As New Dictionary(Of String, Object)()
            requestData.Add("operation", operation)
            requestData.Add("Id", settingData.Id)
            requestData.Add("Name", settingData.Name)
            requestData.Add("Value", settingData.Value)
            requestData.Add("Type", settingData.Type)
            requestData.Add("Status", settingData.Status)

            Dim postString As String = JsonConvert.SerializeObject(requestData)
            Dim url As String = M_Details.LinkAjaxRequest & "GroupPolicyRequest=8&json=" & Uri.EscapeDataString(postString)

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New System.Net.WebClient().DownloadString(url)
            Dim responseJson As JObject = JObject.Parse(response)

            If responseJson("Success").ToString = "True" Then
                dialog.Caption = "Data saved successfully..."
                XtraMessageBox.Show(responseJson("Msg").ToString, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _ClearInputs()
                _LoadData()
            Else
                XtraMessageBox.Show("Failed to save: " & responseJson("Msg").ToString, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error saving data: " & ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dialog.Close()
        End Try
    End Sub

    Private Function _ValidateInputs() As Boolean
        Try
            If String.IsNullOrWhiteSpace(txtName.Text) Then
                XtraMessageBox.Show("Name is required", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtName.Focus()
                Return False
            End If

            If String.IsNullOrWhiteSpace(txtValue.Text) Then
                XtraMessageBox.Show("Value is required", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtValue.Focus()
                Return False
            End If

            If cmbType.EditValue Is Nothing OrElse String.IsNullOrWhiteSpace(cmbType.EditValue.ToString()) Then
                XtraMessageBox.Show("Type is required", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbType.Focus()
                Return False
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function _HasRequiredColumns() As Boolean
        Try
            If GridView1.Columns Is Nothing OrElse GridView1.Columns.Count = 0 Then
                Return False
            End If

            Dim requiredColumns() As String = {"Id", "Name", "Value", "Type", "Status"}
            For Each columnName As String In requiredColumns
                If GridView1.Columns(columnName) Is Nothing Then
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            If String.IsNullOrWhiteSpace(txtId.Text) Then
                XtraMessageBox.Show("Please select a setting to delete", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim result As DialogResult = XtraMessageBox.Show("Are you sure you want to delete this setting?", M_Details.SoftwareVersion, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then Return

            dialog.Caption = "Deleting POS Setting..."
            dialog.Show()

            Dim requestData As New Dictionary(Of String, Object)()
            requestData.Add("operation", "DELETE")
            requestData.Add("Id", Integer.Parse(txtId.Text))

            Dim postString As String = JsonConvert.SerializeObject(requestData)
            Dim url As String = M_Details.LinkAjaxRequest & "GroupPolicyRequest=8&json=" & Uri.EscapeDataString(postString)

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New System.Net.WebClient().DownloadString(url)
            Dim responseJson As JObject = JObject.Parse(response)

            If responseJson("Success").ToString = "True" Then
                dialog.Caption = "Setting deleted successfully..."
                XtraMessageBox.Show(responseJson("Msg").ToString, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _ClearInputs()
                _LoadData()
            Else
                XtraMessageBox.Show("Failed to delete: " & responseJson("Msg").ToString, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error deleting data: " & ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dialog.Close()
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            _ClearInputs()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            _LoadData()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            If e.RowHandle >= 0 AndAlso GridView1.DataRowCount > 0 AndAlso e.RowHandle < GridView1.DataRowCount Then
                ' Check if the required columns exist
                If Not _HasRequiredColumns() Then
                    XtraMessageBox.Show("Data structure is invalid. Please refresh the data.", M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                _currentMode = "UPDATE"
                btnSave.Text = "Update"

                ' Safely get cell values with null checks
                Dim idValue = GridView1.GetRowCellValue(e.RowHandle, "Id")
                txtId.Text = If(idValue IsNot Nothing, idValue.ToString(), "")

                Dim nameValue = GridView1.GetRowCellValue(e.RowHandle, "Name")
                txtName.Text = If(nameValue IsNot Nothing, nameValue.ToString(), "")

                Dim valueValue = GridView1.GetRowCellValue(e.RowHandle, "Value")
                txtValue.Text = If(valueValue IsNot Nothing, valueValue.ToString(), "")

                Dim typeValue = GridView1.GetRowCellValue(e.RowHandle, "Type")
                If typeValue IsNot Nothing Then
                    Dim typeInt As Integer = 0
                    Integer.TryParse(typeValue.ToString(), typeInt)
                    Select Case typeInt
                        Case 1
                            cmbType.EditValue = "Values"
                        Case 0
                            cmbType.EditValue = "Status"
                        Case Else
                            cmbType.EditValue = "Values"
                    End Select
                Else
                    cmbType.EditValue = "Values"  ' Default to Values type
                End If

                Dim statusValue = GridView1.GetRowCellValue(e.RowHandle, "Status")
                If statusValue IsNot Nothing Then
                    Dim status As Integer = 1
                    Integer.TryParse(statusValue.ToString(), status)
                    cmbStatus.EditValue = If(status = 1, "Active", "Inactive")
                Else
                    cmbStatus.EditValue = "Active"  ' Default to Active
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Error selecting row: " & ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        Try

        Catch ex As Exception
        End Try
    End Sub

    Private Sub FrmPosSettings_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.F5 Then
                _LoadData()
            ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
                btnSave_Click(sender, e)
            ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
                btnNew_Click(sender, e)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function _ValidateDataTableStructure(dt As DataTable) As Boolean
        Try
            If dt Is Nothing Then Return False

            Dim requiredColumns() As String = {"Id", "Name", "Value", "Type", "Status"}
            For Each columnName As String In requiredColumns
                If Not dt.Columns.Contains(columnName) Then
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub _ConfigureGridColumns()
        Try
            ' Add event handler for custom column display
            AddHandler GridView1.CustomColumnDisplayText, AddressOf GridView1_CustomColumnDisplayText
        Catch ex As Exception
            ' Ignore errors in grid configuration
        End Try
    End Sub

    Private Sub GridView1_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs)
        Try
            If e.Column.FieldName = "Type" AndAlso e.Value IsNot Nothing Then
                Dim typeValue As Integer = 0
                If Integer.TryParse(e.Value.ToString(), typeValue) Then
                    e.DisplayText = _GetTypeDisplayName(typeValue)
                End If
            ElseIf e.Column.FieldName = "Status" AndAlso e.Value IsNot Nothing Then
                Dim statusValue As Integer = 0
                If Integer.TryParse(e.Value.ToString(), statusValue) Then
                    e.DisplayText = _GetStatusDisplayName(statusValue)
                End If
            End If
        Catch ex As Exception
            ' Ignore display errors
        End Try
    End Sub

    Private Function _GetTypeDisplayName(typeValue As Integer) As String
        Try
            Select Case typeValue
                Case 1
                    Return "Values"
                Case 0
                    Return "Status"
                Case Else
                    Return "Values"
            End Select
        Catch ex As Exception
            Return "Values"
        End Try
    End Function

    Private Function _GetStatusDisplayName(statusValue As Integer) As String
        Try
            Select Case statusValue
                Case 1
                    Return "Active"
                Case 0
                    Return "Inactive"
                Case Else
                    Return "Active"
            End Select
        Catch ex As Exception
            Return "Active"
        End Try
    End Function
End Class

Public Class PosSettingData
    Public Property Id As Integer = 0
    Public Property Name As String = ""
    Public Property Status As Integer = 1
    Public Property Value As String = ""
    Public Property Type As Integer = 0  ' Changed from String to Integer to match backend
    Public Property Created As DateTime = DateTime.Now
End Class
