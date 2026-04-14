Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net

Public Class FrmDiscountPolicy

    Private _editingId As String = ""

    ' ─────────────────────────────── Form Load ────────────────────────────────

    Private Sub FrmDiscountPolicy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _LoadDropdowns()
            _LoadGridData()
            _Clear()
        Catch ex As Exception
            MessageBox.Show("Load error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ─────────────────────────────── Dropdowns ────────────────────────────────

    Private Sub _LoadDropdowns()
        Try
            _LoadDiscountDropdown()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _LoadDiscountDropdown()
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New WebClient().DownloadString(
                M_Details.LinkAjaxRequest & "AjaxRequest=70")
            Dim obj As JObject = JObject.Parse(response)
            If obj("Success").ToObject(Of Boolean)() Then
                Dim arr As JArray = obj("Data")
                Dim dt As New DataTable()
                dt.Columns.Add("discount_id", GetType(Integer))
                dt.Columns.Add("discount_name", GetType(String))
                For Each row In arr
                    dt.Rows.Add(
                        row("Id").ToObject(Of Integer)(),
                        row("Name").ToString())
                Next
                lkpDiscount.Properties.DataSource = dt
                lkpDiscount.Properties.DisplayMember = "discount_name"
                lkpDiscount.Properties.ValueMember = "discount_id"
                lkpDiscount.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                lkpDiscount.Properties.NullText = "-- Select Discount --"
            End If
        Catch ex As Exception
        End Try
    End Sub

  

    ' ─────────────────────────────── Grid Data ────────────────────────────────

    Private Sub _LoadGridData()
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New WebClient().DownloadString(
                M_Details.LinkAjaxRequest & "AjaxRequest=81")
            Dim obj As JObject = JObject.Parse(response)

            Dim dt As New DataTable()
            dt.Columns.Add("dp_id", GetType(Integer))
            dt.Columns.Add("dp_name", GetType(String))
            dt.Columns.Add("discount_name", GetType(String))
            dt.Columns.Add("dp_min_amount", GetType(Decimal))
            dt.Columns.Add("dp_max_amount", GetType(String))
            dt.Columns.Add("dp_require_voucher", GetType(String))
            dt.Columns.Add("dp_validdate", GetType(String))
            dt.Columns.Add("dp_active", GetType(String))

            If obj("Success").ToObject(Of Boolean)() Then
                Dim arr As JArray = obj("Data")
                For Each row In arr
                    Dim newRow As DataRow = dt.NewRow()
                    newRow("dp_id") = row("Id").ToObject(Of Integer)()
                    newRow("dp_name") = row("Name").ToString()
                    newRow("discount_name") = row("DiscountName").ToString()
                    newRow("dp_min_amount") = row("MinAmount").ToObject(Of Decimal)()
                    newRow("dp_max_amount") = If(row("MaxAmount").Type = JTokenType.Null, "No Limit",
                                                 row("MaxAmount").ToObject(Of Decimal)().ToString("N2"))
                    newRow("dp_require_voucher") = If(row("RequireVoucher").ToObject(Of Integer)() = 1, "Yes", "No")
                    newRow("dp_validdate") = row("ValidDate").ToString()
                    newRow("dp_active") = If(row("Active").ToObject(Of Integer)() = 1, "Active", "Inactive")
                    dt.Rows.Add(newRow)
                Next
            End If

            GridControlDP.DataSource = dt
        Catch ex As Exception
            GridControlDP.DataSource = Nothing
        End Try
    End Sub

    ' ─────────────────────────────── Clear / Reset ────────────────────────────

    Private Sub _Clear()
        Try
            _editingId = ""
            txtId.Text = ""
            txtName.Text = ""
            lkpDiscount.EditValue = Nothing
            spinMinAmount.Value = 0
            spinMaxAmount.Value = 0
            chkRequireVoucher.CheckState = CheckState.Unchecked
            dtValidDate.EditValue = DateTime.Today
            chkActive.CheckState = CheckState.Checked
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            txtName.Focus()
            _ClearHighlights()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _ClearHighlights()
        txtName.Properties.Appearance.BackColor = Color.White
        lkpDiscount.Properties.Appearance.BackColor = Color.White
        spinMinAmount.Properties.Appearance.BackColor = Color.White
    End Sub

    ' ─────────────────────────────── Validation ───────────────────────────────

    Private Function _Validate() As Boolean
        _ClearHighlights()
        Dim ok As Boolean = True

        If String.IsNullOrWhiteSpace(txtName.Text) Then
            txtName.Properties.Appearance.BackColor = Color.LightCoral
            ok = False
        End If

        If lkpDiscount.EditValue Is Nothing OrElse String.IsNullOrEmpty(lkpDiscount.EditValue.ToString()) Then
            lkpDiscount.Properties.Appearance.BackColor = Color.LightCoral
            ok = False
        End If

        If spinMinAmount.Value < 0 Then
            spinMinAmount.Properties.Appearance.BackColor = Color.LightCoral
            ok = False
        End If

        If Not ok Then
            MessageBox.Show("Please fill in all required fields.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        Return ok
    End Function

    ' ─────────────────────────────── Save / Update ────────────────────────────

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Not _Validate() Then Exit Sub

            Dim maxAmt As Object = If(spinMaxAmount.Value = 0, Nothing, CDec(spinMaxAmount.Value))

            Dim ajaxRequest As String
            Dim postData As Object

            If String.IsNullOrEmpty(_editingId) Then
                ajaxRequest = "82"
                postData = New With {
                    .dp_name = txtName.Text.Trim(),
                    .dp_discount_id = Convert.ToInt32(lkpDiscount.EditValue),
                    .dp_min_amount = CDec(spinMinAmount.Value),
                    .dp_max_amount = maxAmt,
                    .dp_require_voucher = If(chkRequireVoucher.CheckState = CheckState.Checked, 1, 0),
                    .dp_validdate = CDate(dtValidDate.EditValue).ToString("yyyy-MM-dd HH:mm:ss"),
                    .dp_active = 1
                }
            Else
                ajaxRequest = "83"
                postData = New With {
                    .dp_id = Convert.ToInt32(_editingId),
                    .dp_name = txtName.Text.Trim(),
                    .dp_discount_id = Convert.ToInt32(lkpDiscount.EditValue),
                    .dp_min_amount = CDec(spinMinAmount.Value),
                    .dp_max_amount = maxAmt,
                    .dp_require_voucher = If(chkRequireVoucher.CheckState = CheckState.Checked, 1, 0),
                    .dp_validdate = CDate(dtValidDate.EditValue).ToString("yyyy-MM-dd HH:mm:ss"),
                    .dp_active = If(chkActive.CheckState = CheckState.Checked, 1, 0)
                }
            End If

            Dim json As String = JsonConvert.SerializeObject(postData)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New WebClient().DownloadString(
                M_Details.LinkAjaxRequest & "AjaxRequest=" & ajaxRequest & "&json=" & Uri.EscapeDataString(json))
            Dim obj As JObject = JObject.Parse(response)

            If obj("Success").ToObject(Of Boolean)() Then
                Dim op As String = If(String.IsNullOrEmpty(_editingId), "saved", "updated")
                MessageBox.Show("Discount policy " & op & " successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                _Clear()
                _LoadGridData()
            Else
                MessageBox.Show("Failed: " & obj("Msg").ToString(), "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Save error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ─────────────────────────────── Delete ───────────────────────────────────

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If String.IsNullOrEmpty(_editingId) Then Exit Sub

            If MessageBox.Show("Delete this discount policy?", "Confirm Delete",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If

            Dim postData = New With {.dp_id = Convert.ToInt32(_editingId)}
            Dim json As String = JsonConvert.SerializeObject(postData)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New WebClient().DownloadString(
                M_Details.LinkAjaxRequest & "AjaxRequest=84&json=" & Uri.EscapeDataString(json))
            Dim obj As JObject = JObject.Parse(response)

            If obj("Success").ToObject(Of Boolean)() Then
                MessageBox.Show("Discount policy deleted.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                _Clear()
                _LoadGridData()
            Else
                MessageBox.Show("Delete failed: " & obj("Msg").ToString(), "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Delete error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ─────────────────────────────── Grid Row Click ───────────────────────────

    Private Sub GridViewDP_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridViewDP.RowClick
        Try
            If GridViewDP.FocusedRowHandle < 0 Then Exit Sub
            _editingId = GridViewDP.GetRowCellValue(GridViewDP.FocusedRowHandle, "dp_id").ToString()
            btnSave.Text = "Update"
            btnDelete.Enabled = True

            ' Reload dropdown values to ensure lookup syncs
            txtId.Text = _editingId
            txtName.Text = GridViewDP.GetRowCellValue(GridViewDP.FocusedRowHandle, "dp_name").ToString()

            ' Re-request full record to fill lookup IDs
            _LoadSingleRecord(_editingId)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _LoadSingleRecord(id As String)
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New WebClient().DownloadString(
                M_Details.LinkAjaxRequest & "AjaxRequest=85&id=" & Uri.EscapeDataString(id))
            Dim obj As JObject = JObject.Parse(response)
            If obj("Success").ToObject(Of Boolean)() Then
                Dim d As JObject = obj("Data")
                txtName.Text = d("dp_name").ToString()
                lkpDiscount.EditValue = d("dp_discount_id").ToObject(Of Integer)()
                spinMinAmount.Value = d("dp_min_amount").ToObject(Of Decimal)()
                spinMaxAmount.Value = If(d("dp_max_amount").Type = JTokenType.Null, 0D,
                                         d("dp_max_amount").ToObject(Of Decimal)())
                chkRequireVoucher.CheckState = If(d("dp_require_voucher").ToObject(Of Integer)() = 1,
                                                  CheckState.Checked, CheckState.Unchecked)
                dtValidDate.EditValue = DateTime.Parse(d("dp_validdate").ToString())
                chkActive.CheckState = If(d("dp_active").ToObject(Of Integer)() = 1,
                                          CheckState.Checked, CheckState.Unchecked)
            End If
        Catch ex As Exception
        End Try
    End Sub

    ' ─────────────────────────────── Buttons ──────────────────────────────────

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        _Clear()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

End Class
