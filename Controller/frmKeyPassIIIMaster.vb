Imports DevExpress.XtraEditors
Imports PosRetailWebBilling.clssalesProperty

Public Class frmKeyPassIIIMaster
    Dim errMsg As String
    Dim menuCode As Integer
    Dim _Title As String = "SuperAdmin"

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
 
    Private Sub btnfingerprint_Click(sender As Object, e As EventArgs) Handles btnfingerprint.Click
        Try
            ' Ensure fingerprint data is loaded
            If _JsonData.FingerPrintDataTable Is Nothing OrElse _JsonData.FingerPrintDataTable.Rows.Count = 0 Then
                If Not getFingerPrintData() Then
                    MessageBox.Show("No fingerprint data available. Please register fingerprints first.", "Fingerprint Login", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End If

            ' Show fingerprint capture dialog
            Using scannerForm As New FrmFingerLoginCapture()
                If scannerForm.ShowDialog() = DialogResult.OK AndAlso Not String.IsNullOrEmpty(scannerForm.MatchedFeatureSet) Then
                    Dim matchedUserId As String = scannerForm.MatchedUserId
                    If _JsonData.UserTable.Rows.Count > 0 Then
                        Dim safeUserId As String = matchedUserId.Replace("'", "''")
                        Dim userRow = _JsonData.UserTable.Select("Id = '" & safeUserId & "' AND GroupId IN (1, 2, 3)")
                        If userRow.Length > 0 Then
                            matchedUserId = userRow(0)("Id").ToString()
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                            Me.Close()
                        Else
                            matchedUserId = String.Empty
                            MessageBox.Show("Fingerprint matched but user does not have access rights. Please contact administrator.", "Fingerprint Login", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            XtraMessageBox.Show("Fingerprint login error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class