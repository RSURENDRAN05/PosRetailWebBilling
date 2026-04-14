Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net

Public Class FrmVoucherMaster

    Private _editingId As Integer = 0

    ' ─────────────────────────────── Form Load ────────────────────────────────

    Private Sub FrmVoucherMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _LoadGridData()
            _Clear()
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(
                "Load error: " & ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ─────────────────────────────── Grid Data ────────────────────────────────

    Private Sub _LoadGridData()
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New WebClient().DownloadString(
                M_Details.LinkAjaxRequest & "AjaxRequest=89")

            Dim arr As JArray = JArray.Parse(response)
            Dim dt As New DataTable()
            dt.Columns.Add("Id", GetType(Integer))
            dt.Columns.Add("Prefix", GetType(String))
            dt.Columns.Add("BookNo", GetType(Integer))
            dt.Columns.Add("StartNo", GetType(Integer))
            dt.Columns.Add("EndNo", GetType(Integer))
            dt.Columns.Add("Active", GetType(String))
            dt.Columns.Add("UsedCount", GetType(Integer))
            dt.Columns.Add("CreatedDate", GetType(String))

            For Each row As JToken In arr
                Dim dr As DataRow = dt.NewRow()
                dr("Id") = row("Id").ToObject(Of Integer)()
                dr("Prefix") = row("Prefix").ToString()
                dr("BookNo") = row("BookNo").ToObject(Of Integer)()
                dr("StartNo") = row("StartNo").ToObject(Of Integer)()
                dr("EndNo") = row("EndNo").ToObject(Of Integer)()
                dr("Active") = If(row("Active").ToObject(Of Integer)() = 1, "Active", "Inactive")
                dr("UsedCount") = row("UsedCount").ToObject(Of Integer)()
                dr("CreatedDate") = row("CreatedDate").ToString()
                dt.Rows.Add(dr)
            Next

            GridControlVM.DataSource = dt
        Catch ex As Exception
            GridControlVM.DataSource = Nothing
        End Try
    End Sub

    Private Sub _LoadSingleRecord(vid As Integer)
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New WebClient().DownloadString(
                M_Details.LinkAjaxRequest & "AjaxRequest=90&id=" & vid.ToString())

            Dim obj As JObject = JObject.Parse(response)
            If obj Is Nothing OrElse obj.Type = JTokenType.Null Then Return

            txtId.Text = obj("Id").ToString()
            txtPrefix.Text = obj("Prefix").ToString()
            spinBookNo.Value = obj("BookNo").ToObject(Of Decimal)()
            spinStartNo.Value = obj("StartNo").ToObject(Of Decimal)()
            spinEndNo.Value = obj("EndNo").ToObject(Of Decimal)()
            chkActive.Checked = (obj("Active").ToObject(Of Integer)() = 1)
        Catch ex As Exception
        End Try
    End Sub

    ' ─────────────────────────────── Grid Row Click ───────────────────────────

    Private Sub GridViewVM_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) _
        Handles GridViewVM.RowClick
        Try
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView =
                CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
            Dim row As DataRowView = CType(view.GetRow(e.RowHandle), DataRowView)
            If row Is Nothing Then Return

            _editingId = Convert.ToInt32(row("Id"))
            _LoadSingleRecord(_editingId)
            btnSave.Text = "Update"
            btnDelete.Enabled = True
        Catch ex As Exception
        End Try
    End Sub

    ' ─────────────────────────────── Clear / Reset ────────────────────────────

    Private Sub _Clear()
        _editingId = 0
        txtId.Text = ""
        txtPrefix.Text = ""
        spinBookNo.Value = 1
        spinStartNo.Value = 1
        spinEndNo.Value = 100
        chkActive.Checked = True
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        _ClearHighlights()
        txtPrefix.Focus()
    End Sub

    Private Sub _ClearHighlights()
        txtPrefix.Properties.Appearance.BackColor = Color.White
        spinBookNo.Properties.Appearance.BackColor = Color.White
        spinStartNo.Properties.Appearance.BackColor = Color.White
        spinEndNo.Properties.Appearance.BackColor = Color.White
    End Sub

    ' ─────────────────────────────── Validation ───────────────────────────────

    Private Function _Validate() As Boolean
        _ClearHighlights()
        Dim ok As Boolean = True

        If String.IsNullOrWhiteSpace(txtPrefix.Text) Then
            txtPrefix.Properties.Appearance.BackColor = Color.LightCoral
            ok = False
        End If

        If spinBookNo.Value < 1 Then
            spinBookNo.Properties.Appearance.BackColor = Color.LightCoral
            ok = False
        End If

        If spinStartNo.Value < 1 Then
            spinStartNo.Properties.Appearance.BackColor = Color.LightCoral
            ok = False
        End If

        If spinEndNo.Value < spinStartNo.Value Then
            spinEndNo.Properties.Appearance.BackColor = Color.LightCoral
            ok = False
        End If

        If Not ok Then
            DevExpress.XtraEditors.XtraMessageBox.Show(
                "Please fill in all required fields. End No must be >= Start No.",
                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        Return ok
    End Function

    ' ─────────────────────────────── Save / Update ────────────────────────────

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Not _Validate() Then Exit Sub

            Dim ajaxNum As String
            Dim postObj As Object

            If _editingId = 0 Then
                ajaxNum = "86"
                postObj = New With {
                    .voucher_prefix = txtPrefix.Text.Trim().ToUpper(),
                    .voucher_book_no = CInt(spinBookNo.Value),
                    .voucher_startno = CInt(spinStartNo.Value),
                    .voucher_endno = CInt(spinEndNo.Value)
                }
            Else
                ajaxNum = "87"
                postObj = New With {
                    .voucher_id = _editingId,
                    .voucher_prefix = txtPrefix.Text.Trim().ToUpper(),
                    .voucher_book_no = CInt(spinBookNo.Value),
                    .voucher_startno = CInt(spinStartNo.Value),
                    .voucher_endno = CInt(spinEndNo.Value),
                    .voucher_active = If(chkActive.Checked, 1, 0)
                }
            End If

            Dim json As String = JsonConvert.SerializeObject(postObj)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New WebClient().DownloadString(
                M_Details.LinkAjaxRequest & "AjaxRequest=" & ajaxNum &
                "&json=" & Uri.EscapeDataString(json))

            Dim obj As JObject = JObject.Parse(response)
            If obj("Success").ToObject(Of Boolean)() Then
                Dim op As String = If(_editingId = 0, "created", "updated")
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Voucher book " & op & " successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                _LoadGridData()
                _Clear()
            Else
                Dim msg As String = If(obj("Msg") IsNot Nothing, obj("Msg").ToString(), "Operation failed.")
                DevExpress.XtraEditors.XtraMessageBox.Show(msg, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(
                "Save error: " & ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ─────────────────────────────── Delete ───────────────────────────────────

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If _editingId = 0 Then Exit Sub

            Dim confirm As DialogResult = DevExpress.XtraEditors.XtraMessageBox.Show(
                "Delete this voucher book? All voucher ranges in it will be removed.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If confirm <> DialogResult.Yes Then Exit Sub

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New WebClient().DownloadString(
                M_Details.LinkAjaxRequest & "AjaxRequest=88&voucher_id=" & _editingId.ToString())

            Dim obj As JObject = JObject.Parse(response)
            If obj("Success").ToObject(Of Boolean)() Then
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Voucher book deleted.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                _LoadGridData()
                _Clear()
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Delete failed.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(
                "Delete error: " & ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error)
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
