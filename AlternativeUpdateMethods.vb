' Alternative approach - calling UpdatePosSetting directly:

Private Sub barSearchProductCode_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barSearchProductCode.ItemClick
    Try
        ' Toggle the checked state
        barSearchProductCode.Checked = Not barSearchProductCode.Checked

        ' Convert Boolean to Integer (1 for True, 0 for False)
        Dim statusValue As Integer = If(barSearchProductCode.Checked, 1, 0)

        ' Update the setting - note the correct parameters:
        ' UpdatePosSetting(settingName, settingStatus, settingValue, settingType)
        If UpdatePosSetting("SearchProductCode", statusValue, "0", 0) Then
            ' Update successful - update global setting
            _globalSetting.SearchProductCode = barSearchProductCode.Checked
        Else
            ' Update failed - revert the UI change
            barSearchProductCode.Checked = Not barSearchProductCode.Checked
        End If

    Catch ex As Exception
        ' If error occurs, revert the UI change
        barSearchProductCode.Checked = Not barSearchProductCode.Checked
        DevExpress.XtraEditors.XtraMessageBox.Show("Error updating SearchProductCode setting: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
End Sub

' Or using the simpler UpdatePosSettingByName function:

Private Sub barSearchProductCode_ItemClick_Alternative(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barSearchProductCode.ItemClick
    Try
        ' Toggle the checked state
        barSearchProductCode.Checked = Not barSearchProductCode.Checked

        ' Convert Boolean to Integer
        Dim statusValue As Integer = If(barSearchProductCode.Checked, 1, 0)

        ' Update using the simpler function
        If UpdatePosSettingByName("SearchProductCode", statusValue) Then
            ' Success - the function already updates _globalSetting.SearchProductCode
        Else
            ' Failed - revert UI
            barSearchProductCode.Checked = Not barSearchProductCode.Checked
        End If

    Catch ex As Exception
        barSearchProductCode.Checked = Not barSearchProductCode.Checked
        DevExpress.XtraEditors.XtraMessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
End Sub
