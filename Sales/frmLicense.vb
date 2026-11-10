Imports System.Drawing
Imports System.Windows.Forms

''' <summary>Activation screen: license key (online) or request / activation code (offline). Built in code, no designer.</summary>
Public Class frmLicense
    Inherits Form

    Private ReadOnly _product As String
    Private ReadOnly lblMsg As New Label()
    Private ReadOnly txtKey As New TextBox()
    Private ReadOnly btnActivate As New Button()
    Private ReadOnly btnOffline As New Button()
    Private ReadOnly pnlOffline As New Panel()
    Private ReadOnly txtRequest As New TextBox()
    Private ReadOnly txtCode As New TextBox()
    Private ReadOnly btnCopy As New Button()
    Private ReadOnly btnApply As New Button()
    Private ReadOnly btnExit As New Button()
    Private ReadOnly lblError As New Label()

    Public Sub New(product As String, message As String)
        _product = product
        Text = "Activate " & product
        FormBorderStyle = FormBorderStyle.FixedDialog
        StartPosition = FormStartPosition.CenterScreen
        MaximizeBox = False
        MinimizeBox = False
        ClientSize = New Size(520, 190)
        Font = New Font("Segoe UI", 10.0F)

        Dim title As New Label() With {.Text = "Activate this system (" & product & ")", .Font = New Font("Segoe UI", 13.0F, FontStyle.Bold),
                                       .Location = New Point(16, 12), .AutoSize = True}
        lblMsg.Text = If(String.IsNullOrEmpty(message), "Enter the license key given by your provider.", message)
        lblMsg.Location = New Point(16, 44)
        lblMsg.Size = New Size(488, 40)
        Dim lblKey As New Label() With {.Text = "License key", .Location = New Point(16, 90), .AutoSize = True}
        txtKey.Location = New Point(16, 112)
        txtKey.Size = New Size(300, 27)
        txtKey.CharacterCasing = CharacterCasing.Upper
        btnActivate.Text = "Activate"
        btnActivate.Location = New Point(326, 110)
        btnActivate.Size = New Size(86, 30)
        btnOffline.Text = "Offline code..."
        btnOffline.Location = New Point(418, 110)
        btnOffline.Size = New Size(90, 30)
        lblError.ForeColor = Color.Firebrick
        lblError.Location = New Point(16, 146)
        lblError.Size = New Size(488, 36)
        btnExit.Text = "Exit"
        btnExit.DialogResult = DialogResult.Cancel
        btnExit.Location = New Point(430, 150)
        btnExit.Size = New Size(78, 28)

        pnlOffline.Visible = False
        pnlOffline.Location = New Point(0, 190)
        pnlOffline.Size = New Size(520, 210)
        Dim l1 As New Label() With {.Text = "1. Send this request code to your provider:", .Location = New Point(16, 4), .AutoSize = True}
        txtRequest.Location = New Point(16, 26)
        txtRequest.Size = New Size(396, 27)
        txtRequest.ReadOnly = True
        txtRequest.Text = ClsLicense.RequestCode(product)
        btnCopy.Text = "Copy"
        btnCopy.Location = New Point(418, 25)
        btnCopy.Size = New Size(90, 29)
        Dim l2 As New Label() With {.Text = "2. Paste the activation code you receive:", .Location = New Point(16, 64), .AutoSize = True}
        txtCode.Location = New Point(16, 86)
        txtCode.Size = New Size(488, 80)
        txtCode.Multiline = True
        txtCode.ScrollBars = ScrollBars.Vertical
        btnApply.Text = "Apply code"
        btnApply.Location = New Point(404, 172)
        btnApply.Size = New Size(100, 30)
        pnlOffline.Controls.AddRange(New Control() {l1, txtRequest, btnCopy, l2, txtCode, btnApply})

        Controls.AddRange(New Control() {title, lblMsg, lblKey, txtKey, btnActivate, btnOffline, lblError, btnExit, pnlOffline})
        AcceptButton = btnActivate
        CancelButton = btnExit

        AddHandler btnActivate.Click, AddressOf OnActivate
        AddHandler btnOffline.Click, AddressOf OnToggleOffline
        AddHandler btnCopy.Click, Sub() Clipboard.SetText(txtRequest.Text)
        AddHandler btnApply.Click, AddressOf OnApply
    End Sub

    Private Sub Finish(err As String)
        If err = "" Then
            DialogResult = DialogResult.OK
            Close()
        Else
            lblError.Text = err
        End If
    End Sub

    Private Sub OnActivate(sender As Object, e As EventArgs)
        lblError.Text = ""
        UseWaitCursor = True
        btnActivate.Enabled = False
        Try
            Finish(ClsLicense.ActivateOnline(txtKey.Text, _product))
        Finally
            UseWaitCursor = False
            btnActivate.Enabled = True
        End Try
    End Sub

    Private Sub OnApply(sender As Object, e As EventArgs)
        lblError.Text = ""
        Finish(ClsLicense.ApplyCode(txtCode.Text, _product))
    End Sub

    Private Sub OnToggleOffline(sender As Object, e As EventArgs)
        pnlOffline.Visible = Not pnlOffline.Visible
        ClientSize = New Size(520, If(pnlOffline.Visible, 400, 190))
        ' keep the error line and Exit button above the offline panel
        lblError.Width = 400
    End Sub
End Class
