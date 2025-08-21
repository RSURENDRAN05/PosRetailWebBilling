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
            SetupForm()
            LoadSalesmanData()
            LoadSalesmanDataFromAPI()
        Catch ex As Exception
            MessageBox.Show("Error loading salesman list: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetupForm()
        Try
            If _isSelectionMode Then
                Me.Text = "Select Salesman"
                ' Add Select and Cancel buttons if in selection mode
                Dim btnSelect As New Button()
                btnSelect.Text = "Select"
                btnSelect.Size = New Size(80, 30)
                btnSelect.Location = New Point(Me.Width - 180, Me.Height - 60)
                btnSelect.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
                AddHandler btnSelect.Click, AddressOf btnSelect_Click
                Me.Controls.Add(btnSelect)

                Dim btnCancel As New Button()
                btnCancel.Text = "Cancel"
                btnCancel.Size = New Size(80, 30)
                btnCancel.Location = New Point(Me.Width - 90, Me.Height - 60)
                btnCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
                AddHandler btnCancel.Click, AddressOf btnCancel_Click
                Me.Controls.Add(btnCancel)
            Else
                Me.Text = "Salesman Management"
            End If
        Catch ex As Exception
            MessageBox.Show("Error setting up form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadSalesmanData()
        Try
            ' Create simplified salesman data structure for selection
            salesmanDataTable = New DataTable()
            salesmanDataTable.Columns.Add("Id", GetType(Integer))
            salesmanDataTable.Columns.Add("SalesMan", GetType(String))
          
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
                        salesman("SalesMan").ToString())
                Next

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

    ' Private Function LoadUniqueSalesmenFromCommissions() As Boolean
    '     Try
    '         ' Load commission data and extract unique salesmen
    '         Dim commissionUrl As String = M_Details.LinkAjaxRequest & "SalesManCommission=1" ' Get all commissions
    '         ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
    '         Dim json As String = New System.Net.WebClient().DownloadString(commissionUrl)
    '         Dim parsejson As JObject = JObject.Parse(json)
    '         Dim success = parsejson("Success")

    '         If success.ToString = "True" Then
    '             Dim commissionsData As JArray = CType(parsejson("Data"), JArray)
    '             Dim uniqueSalesmen As New Dictionary(Of Integer, Object)

    '             For Each commission As JObject In commissionsData
    '                 Dim empId As Integer = Convert.ToInt32(commission("EmpId"))

    '                 If Not uniqueSalesmen.ContainsKey(empId) Then
    '                     ' Add unique salesman with average commission percentage
    '                     uniqueSalesmen.Add(empId, New With {
    '                         .EmpId = empId,
    '                         .SalesmanName = commission("SalesmanName").ToString(),
    '                         .CommissionPercentage = Convert.ToDecimal(commission("CommissionPercentage"))
    '                     })
    '                 End If
    '             Next

    '             ' Add unique salesmen to DataTable with simplified structure
    '             For Each kvp As KeyValuePair(Of Integer, Object) In uniqueSalesmen
    '                 Dim salesman = kvp.Value
    '                 salesmanDataTable.Rows.Add(
    '                     salesman.EmpId,
    '                     salesman.SalesmanName,
    '                     salesman.CommissionPercentage
    '                 )
    '             Next

    '             Return True
    '         Else
    '             Return False
    '         End If

    '     Catch ex As Exception
    '         Return False
    '     End Try
    ' End Function


 

    Private Sub btnSelect_Click(sender As Object, e As EventArgs)
        Try
            SelectCurrentSalesman()
        Catch ex As Exception
            MessageBox.Show("Error selecting salesman: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error canceling selection: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
 
    Private Sub GridViewSalesman_DoubleClick(sender As Object, e As EventArgs) Handles GridViewSalesman.DoubleClick
        Try
            If _isSelectionMode Then
                SelectCurrentSalesman()
            End If
        Catch ex As Exception
            MessageBox.Show("Error on double-click: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    ' ' Method to add new salesman (for management mode)
    ' Public Sub AddNewSalesman(empId As Integer, name As String, percentage As Decimal)
    '     Try
    '         If salesmanDataTable IsNot Nothing Then
    '             salesmanDataTable.Rows.Add(empId, name, percentage)
    '             salesmanDataTable.AcceptChanges()
    '         End If
    '     Catch ex As Exception
    '         MessageBox.Show("Error adding new salesman: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '     End Try
    ' End Sub

    ' ' Method to update salesman information
    ' Public Sub UpdateSalesman(empId As Integer, name As String, percentage As Decimal)
    '     Try
    '         If salesmanDataTable IsNot Nothing Then
    '             Dim row As DataRow = salesmanDataTable.Select("EmpId = " & empId).FirstOrDefault()
    '             If row IsNot Nothing Then
    '                 row("SalesmanName") = name
    '                 row("CommissionPercentage") = percentage
    '                 salesmanDataTable.AcceptChanges()
    '             End If
    '         End If
    '     Catch ex As Exception
    '         MessageBox.Show("Error updating salesman: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '     End Try
    ' End Sub

   
End Class
