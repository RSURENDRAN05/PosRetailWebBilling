Imports Newtonsoft.Json
Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class FrmDiscountMaster

    Private editingDiscountId As String = ""

    Private Sub FrmDiscountMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _LoadDiscountData()
            _Clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _Clear()
        Try
            editingDiscountId = ""
            txtName.Text = ""
            txtValue.EditValue = 0.0
            txtDescription.Text = ""
            cmbType.SelectedIndex = 0 ' Default to percentage
            chkStatus.CheckState = CheckState.Checked
            editingDiscountId = ""
            btnSave.Text = "Save"
            txtName.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _LoadDiscountData()
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=70")
            Dim responseObj As JObject = JObject.Parse(response)

            If responseObj("Success").ToObject(Of Boolean)() = True Then
                Dim discountsData As JArray = responseObj("Data")

                ' Create DataTable for grid
                Dim dt As New DataTable()
                dt.Columns.Add("Id", GetType(Integer))
                dt.Columns.Add("Name", GetType(String))
                dt.Columns.Add("Type", GetType(String))
                dt.Columns.Add("Value", GetType(Decimal))
                dt.Columns.Add("Description", GetType(String))
                dt.Columns.Add("Status", GetType(String))

                ' Populate DataTable
                For Each discount In discountsData
                    Dim newRow As DataRow = dt.NewRow()
                    newRow("Id") = discount("Id").ToObject(Of Integer)()
                    newRow("Name") = discount("Name").ToString()
                    newRow("Type") = If(discount("Type").ToString() = "percentage", "Percentage", "Amount")
                    newRow("Value") = discount("Value").ToObject(Of Decimal)()
                    newRow("Description") = discount("Description").ToString()
                    newRow("Status") = If(discount("Status").ToString() = "1", "Active", "Inactive")
                    dt.Rows.Add(newRow)
                Next

                GridControlDiscounts.DataSource = dt
            Else
                GridControlDiscounts.DataSource = Nothing
            End If
        Catch ex As Exception
            GridControlDiscounts.DataSource = Nothing
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            ' Validation
            If String.IsNullOrEmpty(txtName.Text.Trim()) Then
                txtName.Properties.Appearance.BackColor = Color.Red
                MessageBox.Show("Please enter discount name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtName.Focus()
                Exit Sub
            Else
                txtName.Properties.Appearance.BackColor = Color.White
            End If

            Dim discountValue As Decimal = 0
            If Not Decimal.TryParse(txtValue.Text, discountValue) OrElse discountValue <= 0 Then
                txtValue.Properties.Appearance.BackColor = Color.Red
                MessageBox.Show("Please enter a valid discount value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtValue.Focus()
                Exit Sub
            Else
                txtValue.Properties.Appearance.BackColor = Color.White
            End If

            ' Validate percentage range
            If cmbType.SelectedIndex = 0 AndAlso discountValue > 100 Then
                txtValue.Properties.Appearance.BackColor = Color.Red
                MessageBox.Show("Percentage discount cannot be greater than 100%.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtValue.Focus()
                Exit Sub
            End If

            ' Create discount object
            Dim discountData As New Object
            If String.IsNullOrEmpty(editingDiscountId) Then
                ' Insert new discount
                discountData = New With {
                    .discountname = txtName.Text.Trim(),
                    .discounttype = If(cmbType.SelectedIndex = 0, "percentage", "amount"),
                    .discountvalue = discountValue,
                    .discountdescription = txtDescription.Text.Trim()
                }
            Else
                ' Update existing discount
                discountData = New With {
                    .discountid = editingDiscountId,
                    .discountname = txtName.Text.Trim(),
                    .discounttype = If(cmbType.SelectedIndex = 0, "percentage", "amount"),
                    .discountvalue = discountValue,
                    .discountdescription = txtDescription.Text.Trim(),
                    .discountstatus = If(chkStatus.CheckState = CheckState.Checked, 1, 0)
                }
            End If

            ' Determine endpoint
            Dim ajaxRequest As String = If(String.IsNullOrEmpty(editingDiscountId), "67", "68")

            ' Serialize and send
            Dim PostString As String = JsonConvert.SerializeObject(discountData)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=" & ajaxRequest & "&json=" & Uri.EscapeDataString(PostString))
            Dim responseObj As JObject = JObject.Parse(response)

            If responseObj("Success").ToObject(Of Boolean)() = True Then
                Dim operation As String = If(String.IsNullOrEmpty(editingDiscountId), "saved", "updated")
                MessageBox.Show("Discount " & operation & " successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                _Clear()
                _LoadDiscountData()
            Else
                MessageBox.Show("Failed to save discount: " & responseObj("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error saving discount: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridViewDiscounts_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridViewDiscounts.RowClick
        Try
            If GridViewDiscounts.FocusedRowHandle >= 0 Then
                ' Load selected discount for editing
                editingDiscountId = GridViewDiscounts.GetFocusedRowCellValue("Id").ToString()
                txtName.Text = GridViewDiscounts.GetFocusedRowCellValue("Name").ToString()
                txtValue.EditValue = GridViewDiscounts.GetFocusedRowCellValue("Value")
                txtDescription.Text = GridViewDiscounts.GetFocusedRowCellValue("Description").ToString()

                Dim discountType As String = GridViewDiscounts.GetFocusedRowCellValue("Type").ToString()
                cmbType.SelectedIndex = If(discountType = "Percentage", 0, 1)

                Dim status As String = GridViewDiscounts.GetFocusedRowCellValue("Status").ToString()
                chkStatus.CheckState = If(status = "Active", CheckState.Checked, CheckState.Unchecked)

                btnSave.Text = "Update"
                txtName.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If GridViewDiscounts.FocusedRowHandle < 0 Then
                MessageBox.Show("Please select a discount to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim selectedDiscountId As String = GridViewDiscounts.GetFocusedRowCellValue("Id").ToString()
            Dim selectedDiscountName As String = GridViewDiscounts.GetFocusedRowCellValue("Name").ToString()

            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete the discount '" & selectedDiscountName & "'?",
                                                       "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then
                Exit Sub
            End If

            ' Delete from server
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=69&discountid=" & selectedDiscountId)
            Dim responseObj As JObject = JObject.Parse(response)

            If responseObj("Success").ToObject(Of Boolean)() = True Then
                MessageBox.Show("Discount deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                _Clear()
                _LoadDiscountData()
            Else
                MessageBox.Show("Failed to delete discount: " & responseObj("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error deleting discount: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        _Clear()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class
