Imports Newtonsoft.Json
Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class FrmDiscountSelection

    Private selectedDiscount As Object = Nothing

    Private Sub FrmDiscountSelection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _LoadPredefinedDiscounts()
            _SetupCustomDiscountSection()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _LoadPredefinedDiscounts()
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=70")
            Dim responseObj As JObject = JObject.Parse(response)

            If responseObj("Success").ToObject(Of Boolean)() = True Then
                Dim discountsData As JArray = responseObj("Data")

                ' Create DataTable for discount list
                Dim dt As New DataTable()
                dt.Columns.Add("Id", GetType(Integer))
                dt.Columns.Add("Name", GetType(String))
                dt.Columns.Add("Type", GetType(String))
                dt.Columns.Add("Value", GetType(String))
                dt.Columns.Add("Description", GetType(String))

                ' Populate DataTable
                For Each discount In discountsData
                    Dim newRow As DataRow = dt.NewRow()
                    newRow("Id") = discount("Id").ToObject(Of Integer)()
                    newRow("Name") = discount("Name").ToString()
                    newRow("Type") = If(discount("Type").ToString() = "percentage", "Percentage", "Amount")
                    newRow("Value") = If(discount("Type").ToString() = "percentage",
                                       discount("Value").ToString() & "%",
                                       "Rs." & discount("Value").ToString())
                    newRow("Description") = discount("Description").ToString()
                    dt.Rows.Add(newRow)
                Next

                GridControlDiscounts.DataSource = dt
            End If
        Catch ex As Exception
            ' Handle error
        End Try
    End Sub

    Private Sub _SetupCustomDiscountSection()
        Try
            ' Setup custom discount radio group
            RadioGroupCustom.SelectedIndex = 0
            txtCustomPercentage.Properties.ReadOnly = False
            txtCustomAmount.Properties.ReadOnly = True
            txtCustomPercentage.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridViewDiscounts_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridViewDiscounts.RowClick
        Try
            If GridViewDiscounts.FocusedRowHandle >= 0 Then
                ' Get selected discount data
                Dim discountId As Integer = GridViewDiscounts.GetFocusedRowCellValue("Id")
                Dim discountName As String = GridViewDiscounts.GetFocusedRowCellValue("Name").ToString()
                Dim discountType As String = GridViewDiscounts.GetFocusedRowCellValue("Type").ToString()
                Dim discountValueText As String = GridViewDiscounts.GetFocusedRowCellValue("Value").ToString()

                ' Extract numeric value
                Dim discountValue As Decimal = 0
                If discountType = "Percentage" Then
                    Decimal.TryParse(discountValueText.Replace("%", ""), discountValue)
                Else
                    Decimal.TryParse(discountValueText.Replace("Rs.", ""), discountValue)
                End If

                ' Create discount object
                selectedDiscount = New With {
                    .Id = discountId,
                    .Name = discountName,
                    .Type = discountType.ToLower(),
                    .Value = discountValue,
                    .IsCustom = False
                }

                ' Update global discount object
                _discount.DiscountPer = (discountType = "Percentage")
                _discount.discountValue = discountValue

                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnApplyCustom_Click(sender As Object, e As EventArgs) Handles btnApplyCustom.Click
        Try
            Dim discountValue As Decimal = 0
            Dim isPercentage As Boolean = (RadioGroupCustom.SelectedIndex = 0)

            If isPercentage Then
                If Not Decimal.TryParse(txtCustomPercentage.Text, discountValue) OrElse discountValue <= 0 Then
                    MessageBox.Show("Please enter a valid percentage value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtCustomPercentage.Focus()
                    Exit Sub
                End If
                If discountValue > 100 Then
                    MessageBox.Show("Percentage cannot be greater than 100%.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtCustomPercentage.Focus()
                    Exit Sub
                End If
            Else
                If Not Decimal.TryParse(txtCustomAmount.Text, discountValue) OrElse discountValue <= 0 Then
                    MessageBox.Show("Please enter a valid amount.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtCustomAmount.Focus()
                    Exit Sub
                End If
            End If

            ' Create custom discount object
            selectedDiscount = New With {
                .Id = 0,
                .Name = "Custom Discount",
                .Type = If(isPercentage, "percentage", "amount"),
                .Value = discountValue,
                .IsCustom = True
            }

            ' Update global discount object
            _discount.DiscountPer = isPercentage
            _discount.discountValue = discountValue

            Me.DialogResult = Windows.Forms.DialogResult.OK

        Catch ex As Exception
            MessageBox.Show("Error applying custom discount: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RadioGroupCustom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioGroupCustom.SelectedIndexChanged
        Try
            If RadioGroupCustom.SelectedIndex = 0 Then
                ' Percentage selected
                txtCustomPercentage.Properties.ReadOnly = False
                txtCustomAmount.Properties.ReadOnly = True
                txtCustomPercentage.Focus()
            Else
                ' Amount selected
                txtCustomPercentage.Properties.ReadOnly = True
                txtCustomAmount.Properties.ReadOnly = False
                txtCustomAmount.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    ' Property to get selected discount
    Public ReadOnly Property SelectedDiscountInfo() As Object
        Get
            Return selectedDiscount
        End Get
    End Property
End Class
