Imports Newtonsoft.Json
Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class frmDiscount

    Private Sub frmDiscount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtamount.Text = ""
            txtpercentage.Text = ""

            ' Load predefined discounts
            _LoadPredefinedDiscounts()

            If RadioGroup1.SelectedIndex = 0 Then
                _discount.DiscountPer = True
                txtpercentage.Properties.ReadOnly = False
                txtpercentage.Focus()
                txtamount.Properties.ReadOnly = True
            Else
                _discount.DiscountPer = False
                txtpercentage.Properties.ReadOnly = True
                txtamount.Focus()
                txtamount.Properties.ReadOnly = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _LoadPredefinedDiscounts()
        Try
            ' Load discount list from server
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=70")
            Dim responseObj As JObject = JObject.Parse(response)

            If responseObj("Success").ToObject(Of Boolean)() = True Then
                Dim discountsData As JArray = responseObj("Data")

                ' Check if we have a ListBoxControl or similar for displaying discounts
                ' If not available, we'll add the functionality to apply discounts directly

                ' You can uncomment and modify this section if you have a discount list control:
                ' If ListBoxDiscounts IsNot Nothing Then
                '     ListBoxDiscounts.Items.Clear()
                '     For Each discount In discountsData
                '         Dim discountText As String = discount("Name").ToString() & " (" &
                '                                     If(discount("Type").ToString() = "percentage",
                '                                        discount("Value").ToString() & "%",
                '                                        "Rs." & discount("Value").ToString()) & ")"
                '         ListBoxDiscounts.Items.Add(discountText)
                '     Next
                ' End If
            End If
        Catch ex As Exception
            ' Handle error silently for now
        End Try
    End Sub

    Private Sub _ApplyPredefinedDiscount(discountId As String)
        Try
            ' Apply selected predefined discount
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=70")
            Dim responseObj As JObject = JObject.Parse(response)

            If responseObj("Success").ToObject(Of Boolean)() = True Then
                Dim discountsData As JArray = responseObj("Data")

                For Each discount In discountsData
                    If discount("Id").ToString() = discountId Then
                        Dim discountType As String = discount("Type").ToString()
                        Dim discountValue As Decimal = discount("Value").ToObject(Of Decimal)()

                        If discountType = "percentage" Then
                            RadioGroup1.SelectedIndex = 0
                            txtpercentage.EditValue = discountValue
                            _discount.DiscountPer = True
                            _discount.discountValue = discountValue
                        Else
                            RadioGroup1.SelectedIndex = 1
                            txtamount.EditValue = discountValue
                            _discount.DiscountPer = False
                            _discount.discountValue = discountValue
                        End If

                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            ' Handle error
        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
            If RadioGroup1.SelectedIndex = 0 Then
                _discount.discountValue = txtpercentage.EditValue
            Else
                _discount.discountValue = txtamount.EditValue
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadioGroup1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioGroup1.SelectedIndexChanged
        Try
            If RadioGroup1.SelectedIndex = 0 Then
                _discount.DiscountPer = True
                txtpercentage.Properties.ReadOnly = False
                txtpercentage.Focus()
                txtamount.Properties.ReadOnly = True
            Else
                _discount.DiscountPer = False
                txtpercentage.Properties.ReadOnly = True
                txtamount.Focus()
                txtamount.Properties.ReadOnly = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
End Class
