Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class FrmMultiplePriceSelection
    Private _itemId As Integer
    Private _itemName As String
    Private _selectedPriceInfo As Object
    Private _multiplePriceTable As DataTable

    Public Property SelectedPriceInfo As Object
        Get
            Return _selectedPriceInfo
        End Get
        Set(value As Object)
            _selectedPriceInfo = value
        End Set
    End Property

    Public Sub New(itemId As Integer, itemName As String)
        InitializeComponent()
        _itemId = itemId
        _itemName = itemName
    End Sub

    Private Sub FrmMultiplePriceSelection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = "Select Price for: " & _itemName
            lblItemName.Text = _itemName

            ' Load multiple prices for this item
            LoadMultiplePrices()

        Catch ex As Exception
            MessageBox.Show("Error loading price selection: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadMultiplePrices()
        Try
            ' Get multiple prices from server
            If GetMultiplePricesFromServer() Then
                If _multiplePriceTable IsNot Nothing AndAlso _multiplePriceTable.Rows.Count > 0 Then
                    GridControlPrices.DataSource = _multiplePriceTable
                    SetupPriceGrid()
                Else
                    MessageBox.Show("No multiple prices found for this item.", "No Prices", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.Cancel
                    Me.Close()
                End If
            Else
                MessageBox.Show("Failed to load multiple prices.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading multiple prices: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetMultiplePricesFromServer() As Boolean
        Try
            Dim url As String = M_Details.LinkAjaxRequest & "AjaxRequest=66&itemid=" & _itemId

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(url)
            Dim parseJson As JObject = JObject.Parse(json)
            Dim success = parseJson("Success").ToString()

            If success = "True" Then
                _multiplePriceTable = parseJson("Data").ToObject(Of DataTable)()
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub SetupPriceGrid()
        Try
            ' Configure grid columns
            GridViewPrices.Columns("Id").Visible = False
            GridViewPrices.Columns("RefId").Visible = False
            GridViewPrices.Columns("Status").Visible = False
            GridViewPrices.Columns("Created").Visible = False

            GridViewPrices.Columns("Name").Caption = "Price Type"
            GridViewPrices.Columns("Name").Width = 200

            GridViewPrices.Columns("Price").Caption = "Price"
            GridViewPrices.Columns("Price").Width = 100
            GridViewPrices.Columns("Price").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            GridViewPrices.Columns("Price").DisplayFormat.FormatString = "0.00"

            ' Set grid options
            GridViewPrices.OptionsSelection.EnableAppearanceFocusedCell = False
            GridViewPrices.OptionsSelection.MultiSelect = False
            GridViewPrices.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            ' Focus first row
            If GridViewPrices.RowCount > 0 Then
                GridViewPrices.FocusedRowHandle = 0
            End If

        Catch ex As Exception
            MessageBox.Show("Error setting up price grid: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSelectPrice_Click(sender As Object, e As EventArgs) Handles btnSelectPrice.Click
        Try
            If GridViewPrices.FocusedRowHandle < 0 Then
                MessageBox.Show("Please select a price option.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim priceId As Integer = GridViewPrices.GetFocusedRowCellValue("Id")
            Dim priceName As String = GridViewPrices.GetFocusedRowCellValue("Name").ToString()
            Dim priceValue As Decimal = Convert.ToDecimal(GridViewPrices.GetFocusedRowCellValue("Price"))

            _selectedPriceInfo = New With {
                .PriceId = priceId,
                .PriceName = priceName,
                .PriceValue = priceValue,
                .ItemId = _itemId
            }

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error selecting price: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            _selectedPriceInfo = Nothing
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error canceling: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridViewPrices_DoubleClick(sender As Object, e As EventArgs) Handles GridViewPrices.DoubleClick
        Try
            ' Double-click to select price
            btnSelectPrice_Click(sender, e)
        Catch ex As Exception
            MessageBox.Show("Error in double-click: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridViewPrices_KeyDown(sender As Object, e As KeyEventArgs) Handles GridViewPrices.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                btnSelectPrice_Click(sender, Nothing)
            ElseIf e.KeyCode = Keys.Escape Then
                btnCancel_Click(sender, Nothing)
            End If
        Catch ex As Exception
            MessageBox.Show("Error in key down: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
