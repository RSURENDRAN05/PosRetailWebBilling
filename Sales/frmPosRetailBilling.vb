Imports System.Data
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmPosRetailBilling
    Inherits DevExpress.XtraEditors.XtraForm

    ' Private variables
    Private dtItems As DataTable
    Private currentTotal As Decimal = 0
    Private currentSubTotal As Decimal = 39.0

    Private Sub frmPosRetailBilling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize form components and data
        InitializeData()
        SetupGridData()
        SetupKeyboardShortcuts()
    End Sub

    Private Sub InitializeData()
        ' Initialize branch combo box
        With cmbBranch.Properties.Items
            .Clear()
            .Add("Main Branch")
            .Add("Branch 01")
            .Add("Branch 02")
        End With
        cmbBranch.SelectedIndex = 0

        ' Initialize options combo box
        With cmbOptions.Properties.Items
            .Clear()
            .Add("Retail")
            .Add("Wholesale")
            .Add("Special")
        End With
        cmbOptions.SelectedIndex = 0

        ' Initialize price level
        cmbPriceLevel.SelectedIndex = 0

        ' Set initial values
        txtSubTotal.Text = "39.00"
        txtDiscount.Text = "0.00"
        txtTax.Text = "0.00"
        txtRounding.Text = "0.00"
        lblTotal.Text = "3900"

        ' Set focus to barcode field
        txtBarcode.Focus()
    End Sub

    Private Sub SetupGridData()
        ' Create data table for items
        dtItems = New DataTable()
        With dtItems.Columns
            .Add("No", GetType(Integer))
            .Add("ItemNo", GetType(String))
            .Add("Description", GetType(String))
            .Add("UOM", GetType(String))
            .Add("Qty", GetType(Integer))
            .Add("Price", GetType(Decimal))
            .Add("Discount", GetType(Decimal))
            .Add("UPrice", GetType(Decimal))
            .Add("Amount", GetType(Decimal))
            .Add("Picture", GetType(String))
        End With

        ' Add sample data (matching the image)
        dtItems.Rows.Add(1, "1100010000...", "420ML THINNER 6011(HP)", "*", 1, 3.00, 0.00, 3.00, 3.00, "")
        dtItems.Rows.Add(2, "5002010000...", "10MM CHINA SPANNER", "*", 1, 2.30, 0.00, 2.30, 2.30, "")
        dtItems.Rows.Add(3, "0100011237...", "1L A345-12370 ICI SATINWOOD", "TIN", 1, 32.50, 0.00, 32.50, 32.50, "")
        dtItems.Rows.Add(4, "0120410000...", "2I/2C TH1008", "", 1, 1.20, 0.00, 1.20, 1.20, "")

        ' Bind to grid
        GridControl1.DataSource = dtItems
    End Sub

    Private Sub SetupKeyboardShortcuts()
        ' Set up keyboard shortcuts
        Me.KeyPreview = True
        AddHandler Me.KeyDown, AddressOf frmPosRetailBilling_KeyDown
    End Sub

    Private Sub frmPosRetailBilling_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                btnClearF1.PerformClick()
            Case Keys.F4
                btnExit.PerformClick()
            Case Keys.F5
                txtSalesOrder.Focus()
            Case Keys.F6
                txtSalesperson.Focus()
            Case Keys.F7
                txtBarcode.Focus()
            Case Keys.F8
                btnHoldBill.PerformClick()
            Case Keys.F9
                btnDrawer.PerformClick()
            Case Keys.F10
                btnPayment.PerformClick()
            Case Keys.F11
                txtCustomer.Focus()
        End Select
    End Sub

    ' Button Events
    Private Sub btnPayment_Click(sender As Object, e As EventArgs) Handles btnPayment.Click
        ' Handle payment process
        MessageBox.Show("Payment Processing - F10", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnClearF1_Click(sender As Object, e As EventArgs) Handles btnClearF1.Click
        ' Clear current transaction
        If MessageBox.Show("Clear current transaction?", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            ClearTransaction()
        End If
    End Sub

    Private Sub btnHoldBill_Click(sender As Object, e As EventArgs) Handles btnHoldBill.Click
        ' Hold current bill
        MessageBox.Show("Bill held successfully - F8", "Hold Bill", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnDrawer_Click(sender As Object, e As EventArgs) Handles btnDrawer.Click
        ' Open cash drawer
        MessageBox.Show("Cash drawer opened - F9", "Drawer", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        ' Exit application
        If MessageBox.Show("Exit application?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub txtBarcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBarcode.KeyPress
        ' Handle barcode entry
        If e.KeyChar = Chr(13) Then ' Enter key
            ProcessBarcode()
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        ' Edit selected item
        If GridView1.FocusedRowHandle >= 0 Then
            MessageBox.Show("Edit item functionality", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        ' Delete selected item
        If GridView1.FocusedRowHandle >= 0 Then
            If MessageBox.Show("Delete selected item?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                dtItems.Rows.RemoveAt(GridView1.FocusedRowHandle)
                CalculateTotal()
            End If
        End If
    End Sub

    Private Sub btnDiscount50_Click(sender As Object, e As EventArgs) Handles btnDiscount50.Click
        ' Apply 50% discount to selected item
        If GridView1.FocusedRowHandle >= 0 Then
            Dim row As DataRow = dtItems.Rows(GridView1.FocusedRowHandle)
            row("Discount") = 50.0
            CalculateRowTotal(row)
            CalculateTotal()
        End If
    End Sub

    ' Helper methods
    Private Sub ClearTransaction()
        dtItems.Clear()
        txtBarcode.Text = ""
        txtSubTotal.Text = "0.00"
        txtDiscount.Text = "0.00"
        txtTax.Text = "0.00"
        txtRounding.Text = "0.00"
        lblTotal.Text = "0"
        currentTotal = 0
        currentSubTotal = 0
    End Sub

    Private Sub ProcessBarcode()
        Dim barcode As String = txtBarcode.Text.Trim()
        If Not String.IsNullOrEmpty(barcode) Then
            ' Add item to grid (mock implementation)
            MessageBox.Show("Processing barcode:" & barcode, "Barcode", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtBarcode.Text = ""
        End If
    End Sub

    Private Sub CalculateRowTotal(row As DataRow)
        Dim price As Decimal = Convert.ToDecimal(row("Price"))
        Dim qty As Integer = Convert.ToInt32(row("Qty"))
        Dim discount As Decimal = Convert.ToDecimal(row("Discount"))

        Dim unitPrice As Decimal = price * (1 - discount / 100)
        Dim amount As Decimal = unitPrice * qty

        row("UPrice") = Math.Round(unitPrice, 2)
        row("Amount") = Math.Round(amount, 2)
    End Sub

    Private Sub CalculateTotal()
        currentSubTotal = 0
        For Each row As DataRow In dtItems.Rows
            currentSubTotal += Convert.ToDecimal(row("Amount"))
        Next

        Dim discount As Decimal = Convert.ToDecimal(txtDiscount.Text)
        Dim tax As Decimal = Convert.ToDecimal(txtTax.Text)
        Dim rounding As Decimal = Convert.ToDecimal(txtRounding.Text)

        currentTotal = currentSubTotal * (1 - discount / 100) * (1 + tax / 100) + rounding

        txtSubTotal.Text = Math.Round(currentSubTotal, 2).ToString("F2")
        lblTotal.Text = Math.Round(currentTotal * 100, 0).ToString() ' Display in cents format like the image
    End Sub

End Class
