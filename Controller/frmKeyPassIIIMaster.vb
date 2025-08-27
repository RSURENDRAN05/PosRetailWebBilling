Imports DevExpress.XtraEditors
Imports PosRetailWebBilling.clssalesProperty

Public Class frmKeyPassIIIMaster
    Dim errMsg As String
    Dim menuCode As Integer
    Dim _Title As String = "Admin"

    Private Sub frmKeyPass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        menuCode = properClass.R_MenuCode
        properClass.R_BooleanStatus = False
        lbltitle.Text = _Title
        _TXTPASS.Text = ""
    End Sub

    Private Sub frmKeyPass_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        Try
            Me.Dispose()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btn_7_Click(sender As Object, e As EventArgs) Handles btn_7.Click, btn_8.Click, btn_9.Click, btn_6.Click, btn_5.Click, btn_4.Click, btn_3.Click, btn_2.Click, btn_1.Click, btn_0.Click
        Try
            Dim smbtn As SimpleButton
            smbtn = CType(sender, SimpleButton)
            _TXTPASS.Text = _TXTPASS.Text + smbtn.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_ok_Click(sender As Object, e As EventArgs) Handles btn_ok.Click
        Try
            If String.IsNullOrEmpty(_TXTPASS.Text) OrElse _TXTPASS.Text = "" Then
                Exit Sub
            End If
            If AuthenticateMasterAdmin(_TXTPASS.Text) = True Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                WriteErroLog("Master - Password Wrong")
            End If
            'If _TXTPASS.Text = "786786" Then
            '    Dim frmregis As New frmRegistration
            '    frmregis.ShowDialog()
            '    Me.Close()
            'End If
            'If clfun._userMenuRightsCheckWithPass(_TXTPASS.EditValue, menuCode, errMsg) = True Then 'Company Master Menu ID :2
            '    properClass.R_BooleanStatus = True
            '    Me.Close()
            'Else
            '    properClass.R_BooleanStatus = False
            '    eLog.WriteErroLog(errMsg.ToString)
            '    Me.Close()
            'End If
        Catch ex As Exception
            WriteErroLog("btn_ok_Click" & ex.Message)
        End Try
    End Sub

    Private Sub btn_numclear_Click(sender As Object, e As EventArgs) Handles btn_numclear.Click
        Try
            _TXTPASS.Text = ""
            _TXTPASS.Select()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
 
End Class