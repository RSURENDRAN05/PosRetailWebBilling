Imports System.Data
Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class FrmSalesmanList
    Private _isSelectionMode As Boolean = False
    Private _selectedSalesmanId As Integer = 0
    Private _selectedSalesmanName As String = ""
    Private _selectedSalesmanPercentage As Decimal = 0
    Private salesmanDataTable As DataTable



    ' Properties to get selected salesman information
    Public ReadOnly Property SelectedSalesmanId As Integer
        Get
            Return _selectedSalesmanId
        End Get
    End Property

    Public ReadOnly Property SelectedSalesmanName As String
        Get
            Return _selectedSalesmanName
        End Get
    End Property

    Public ReadOnly Property SelectedSalesmanPercentage As Decimal
        Get
            Return _selectedSalesmanPercentage
        End Get
    End Property

    ' Constructor
    Public Sub New(Optional selectionMode As Boolean = False)
        InitializeComponent()
        _isSelectionMode = selectionMode
    End Sub

    Private Sub FrmSalesmanList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Initialize the form
            InitializeRepositoryItems()
            LoadSalesmanData()
            LoadSalesmanDataFromAPI()

            '' Debug message to check if we're in selection mode
            'If _isSelectionMode Then
            '    Me.Text = "Select Salesman (Selection Mode ON)"
            'Else
            '    Me.Text = "Salesman Management (Selection Mode OFF)"
            'End If
        Catch ex As Exception
            MessageBox.Show("Error loading salesman list: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InitializeRepositoryItems()
        Try
            ' Create and configure the Select button repository item
            RepositoryItemButtonEditSelect = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()

            ' Configure the repository item
            With RepositoryItemButtonEditSelect
                .Name = "RepositoryItemButtonEditSelect"
                .TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
                .Buttons.Clear()

                ' Create the Select button
                Dim selectButton As New DevExpress.XtraEditors.Controls.EditorButton()
                selectButton.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.OK
                selectButton.Caption = "Select"
                .Buttons.Add(selectButton)
            End With

        Catch ex As Exception
            MessageBox.Show("Error initializing repository items: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub LoadSalesmanData()
        Try
            ' Create simplified salesman data structure for selection
            salesmanDataTable = New DataTable()
            salesmanDataTable.Columns.Add("Id", GetType(Integer))
            salesmanDataTable.Columns.Add("SalesMan", GetType(String))
            ' Add a select column for button display
            salesmanDataTable.Columns.Add("Select", GetType(String))

            ' Bind to grid
            If Me.Controls.ContainsKey("GridControlSalesman") Then
                Dim gridControl As DevExpress.XtraGrid.GridControl = CType(Me.Controls("GridControlSalesman"), DevExpress.XtraGrid.GridControl)
                gridControl.DataSource = salesmanDataTable
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading salesman data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function LoadSalesmanDataFromAPI() As Boolean
        Try
            ' Use the new endpoint to get salesmen by company and location
            Dim salesmanListUrl As String = M_Details.LinkAjaxRequest & "SalesManCommission=13&Comid=" & _companyInfo.ComId & "&Locid=" & _companyInfo.LocId
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(salesmanListUrl)
            Dim parsejson As JObject = JObject.Parse(json)
            Dim success = parsejson("Success")

            If success.ToString = "True" Then
                Dim salesmenData As JArray = CType(parsejson("Data"), JArray)

                ' Process each salesman record
                For Each salesman As JObject In salesmenData
                    ' Add salesman with simplified structure
                    salesmanDataTable.Rows.Add(
                        Convert.ToInt32(salesman("Id")),
                        salesman("SalesMan").ToString(),
                        If(_isSelectionMode, "Select", "")) ' Add Select text only in selection mode
                Next

                ' Setup grid columns after data is loaded
                If Me.Controls.ContainsKey("GridControlSalesman") Then
                    Dim gridControl As DevExpress.XtraGrid.GridControl = CType(Me.Controls("GridControlSalesman"), DevExpress.XtraGrid.GridControl)
                    SetupGridViewColumns(gridControl)
                    gridControl.RefreshDataSource()
                End If

                Return True
            Else
                ' If the new endpoint doesn't exist, fall back to loading commission data
                ' and extracting unique salesmen
                'Return LoadUniqueSalesmenFromCommissions()
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Error loading salesman data from API: " & ex.Message, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try
    End Function

    Private Sub SetupGridViewColumns(gridControl As DevExpress.XtraGrid.GridControl)
        Try
            Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = CType(gridControl.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

            ' Configure columns
            If gridView.Columns("Id") IsNot Nothing Then
                gridView.Columns("Id").Visible = False ' Hide ID column
            End If

            If gridView.Columns("SalesMan") IsNot Nothing Then
                gridView.Columns("SalesMan").Caption = "Salesman Name"
                gridView.Columns("SalesMan").Width = 200
                gridView.Columns("SalesMan").OptionsColumn.AllowEdit = False
                gridView.Columns("SalesMan").OptionsColumn.AllowFocus = False
            End If

            ' Setup Select button column only in selection mode
            If _isSelectionMode AndAlso gridView.Columns("Select") IsNot Nothing Then
                gridView.Columns("Select").Caption = "Action"
                gridView.Columns("Select").Width = 80
                gridView.Columns("Select").Visible = True
                gridView.Columns("Select").OptionsColumn.AllowEdit = True
                gridView.Columns("Select").OptionsColumn.AllowFocus = True

                ' Use the pre-created repository item
                gridView.Columns("Select").ColumnEdit = RepositoryItemButtonEditSelect
                gridControl.RepositoryItems.Add(RepositoryItemButtonEditSelect)

                ' Move Select column to the end
                gridView.Columns("Select").VisibleIndex = gridView.Columns.Count - 1
            Else
                ' Hide select column if not in selection mode
                If gridView.Columns("Select") IsNot Nothing Then
                    gridView.Columns("Select").Visible = False
                End If
            End If

            ' General grid settings
            gridView.OptionsView.ShowGroupPanel = False
            gridView.OptionsBehavior.Editable = _isSelectionMode ' Allow editing only in selection mode for buttons
            gridView.OptionsSelection.EnableAppearanceFocusedCell = False
            gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

        Catch ex As Exception
            MessageBox.Show("Error setting up grid columns: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs)
        Try
            SelectCurrentSalesman()
        Catch ex As Exception
            MessageBox.Show("Error selecting salesman: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error canceling selection: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub SelectCurrentSalesmanFromRow(rowHandle As Integer)
        Try
            If Me.Controls.ContainsKey("GridControlSalesman") Then
                Dim gridControl As DevExpress.XtraGrid.GridControl = CType(Me.Controls("GridControlSalesman"), DevExpress.XtraGrid.GridControl)
                Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = CType(gridControl.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                If rowHandle >= 0 Then
                    ' Updated to use new column names from API
                    _selectedSalesmanId = Convert.ToInt32(gridView.GetRowCellValue(rowHandle, "Id"))
                    _selectedSalesmanName = gridView.GetRowCellValue(rowHandle, "SalesMan").ToString()

                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show("Please select a salesman from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error selecting salesman: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub SelectCurrentSalesman()
        Try
            If Me.Controls.ContainsKey("GridControlSalesman") Then
                Dim gridControl As DevExpress.XtraGrid.GridControl = CType(Me.Controls("GridControlSalesman"), DevExpress.XtraGrid.GridControl)
                Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = CType(gridControl.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                If gridView.FocusedRowHandle >= 0 Then
                    ' Updated to use new column names from API
                    _selectedSalesmanId = Convert.ToInt32(gridView.GetRowCellValue(gridView.FocusedRowHandle, "Id"))
                    _selectedSalesmanName = gridView.GetRowCellValue(gridView.FocusedRowHandle, "SalesMan").ToString()

                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show("Please select a salesman from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error selecting salesman: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Method to refresh salesman data
    Public Sub RefreshData()
        Try
            LoadSalesmanData()
        Catch ex As Exception
            MessageBox.Show("Error refreshing data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Repository button click event - this is the correct event for DevExpress repository buttons
    Private Sub RepositoryItemButtonEditSelect_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemButtonEditSelect.ButtonClick
        Try
            ' Debug message to confirm button click
            ' MessageBox.Show("Button clicked! Selection mode: " & _isSelectionMode.ToString(), "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information)

            If _isSelectionMode Then
                ' Get the grid view from the focused control
                If Me.Controls.ContainsKey("GridControlSalesman") Then
                    Dim gridControl As DevExpress.XtraGrid.GridControl = CType(Me.Controls("GridControlSalesman"), DevExpress.XtraGrid.GridControl)
                    Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = CType(gridControl.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                    ' Use the focused row handle to select the salesman
                    If gridView.FocusedRowHandle >= 0 Then
                        SelectCurrentSalesmanFromRow(gridView.FocusedRowHandle)
                    Else
                        MessageBox.Show("Please select a salesman from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error on repository button click: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
