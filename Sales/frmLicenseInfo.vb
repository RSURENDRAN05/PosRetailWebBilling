Imports System.Drawing
Imports System.Windows.Forms

''' <summary>License details: client, plan, dates, products, key and device. Built in code, no designer.</summary>
Public Class frmLicenseInfo
    Inherits Form

    Private ReadOnly _product As String
    Private ReadOnly lblState As New Label()
    Private ReadOnly lv As New ListView()
    Private ReadOnly btnCheck As New Button()
    Private ReadOnly btnKey As New Button()
    Private ReadOnly btnClose As New Button()

    Public Sub New(product As String)
        _product = product
        Text = "License - " & product
        FormBorderStyle = FormBorderStyle.FixedDialog
        StartPosition = FormStartPosition.CenterScreen
        MaximizeBox = False
        MinimizeBox = False
        ClientSize = New Size(560, 430)
        Font = New Font("Segoe UI", 10.0F)

        lblState.Font = New Font("Segoe UI", 13.0F, FontStyle.Bold)
        lblState.Location = New Point(16, 12)
        lblState.Size = New Size(528, 28)
        lv.Location = New Point(16, 48)
        lv.Size = New Size(528, 320)
        lv.View = View.Details
        lv.FullRowSelect = True
        lv.GridLines = True
        lv.HeaderStyle = ColumnHeaderStyle.None
        lv.Columns.Add("Item", 160)
        lv.Columns.Add("Value", 350)
        btnCheck.Text = "Check now"
        btnCheck.Location = New Point(16, 382)
        btnCheck.Size = New Size(110, 32)
        btnKey.Text = "Enter new key"
        btnKey.Location = New Point(134, 382)
        btnKey.Size = New Size(130, 32)
        btnClose.Text = "Close"
        btnClose.DialogResult = DialogResult.Cancel
        btnClose.Location = New Point(454, 382)
        btnClose.Size = New Size(90, 32)
        Controls.AddRange(New Control() {lblState, lv, btnCheck, btnKey, btnClose})
        CancelButton = btnClose
        AddHandler btnCheck.Click, AddressOf OnCheck
        AddHandler btnKey.Click, AddressOf OnKey
        Fill(ClsLicense.CheckStored(_product))
    End Sub

    Private Sub Add(k As String, v As String)
        Dim it = lv.Items.Add(k)
        it.SubItems.Add(v)
    End Sub

    Private Shared Function D(x As Date?) As String
        Return If(x.HasValue, x.Value.ToString("dd-MM-yyyy"), "No expiry")
    End Function

    Private Sub Fill(r As LicenseResult)
        lv.Items.Clear()
        Dim label As String
        Dim color As Color = Color.Firebrick
        Select Case r.State
            Case LicenseState.Valid : label = "ACTIVE" : color = Color.DarkGreen
            Case LicenseState.Expiring : label = "ENDING SOON" : color = Color.DarkOrange
            Case LicenseState.Grace : label = "GRACE PERIOD" : color = Color.DarkOrange
            Case LicenseState.Expired : label = "EXPIRED"
            Case Else : label = "NOT ACTIVATED / NOT VALID"
        End Select
        lblState.Text = label
        lblState.ForeColor = color
        Add("Status", r.Message)
        If r.Serial <> "" Then
            Add("Client", r.ClientName)
            Add("License no.", r.Serial)
            Add("Plan", r.Plan)
            Add("Valid from", r.FromDate.ToString("dd-MM-yyyy"))
            Add("Valid until", D(r.UntilDate))
            If r.UntilDate.HasValue Then Add("Grace period", r.GraceDays & " days after the end date")
            If r.DaysLeft.HasValue Then Add("Days left", If(r.DaysLeft.Value >= 0, r.DaysLeft.Value.ToString(), "Expired " & (-r.DaysLeft.Value) & " day(s) ago"))
            For Each kv In r.Products
                Add(If(kv.Key = _product, "Product (this PC)", "Product"), kv.Key & "  x" & kv.Value)
            Next
        End If
        Dim key = ClsLicense.StoredKey()
        If key <> "" Then Add("License key", key)
        Add("This PC product", _product)
        Add("This device", ClsLicense.Fingerprint())
    End Sub

    Private Sub OnCheck(sender As Object, e As EventArgs)
        UseWaitCursor = True
        Try
            Fill(ClsLicense.Startup(_product))
        Finally
            UseWaitCursor = False
        End Try
    End Sub

    Private Sub OnKey(sender As Object, e As EventArgs)
        Using f As New frmLicense(_product, "Enter the new license key, or use an offline code.")
            f.ShowDialog()
        End Using
        Fill(ClsLicense.CheckStored(_product))
    End Sub
End Class
