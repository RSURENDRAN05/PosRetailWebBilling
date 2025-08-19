' Example usage of the UpdatePosSetting function
' Replace your existing barSearchProductCode_CheckedChanged event handler with this:

Private Sub barSearchProductCode_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barSearchProductCode.CheckedChanged
    Try
        ' Toggle the checked state
        barSearchProductCode.Checked = Not barSearchProductCode.Checked

        ' Determine the new status value (1 for checked/true, 0 for unchecked/false)
        Dim newStatus As Integer = If(barSearchProductCode.Checked, 1, 0)

        ' Update the setting in the database
        If UpdatePosSettingByName("SearchProductCode", newStatus) Then
            ' Update was successful
            _globalSetting.SearchProductCode = barSearchProductCode.Checked
            XtraMessageBox.Show("Search Product Code setting updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            ' Update failed, revert the UI change
            barSearchProductCode.Checked = Not barSearchProductCode.Checked
            XtraMessageBox.Show("Failed to update Search Product Code setting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    Catch ex As Exception
        XtraMessageBox.Show($"Error in barSearchProductCode_CheckedChanged: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
End Sub

' You can also create specific update functions for other settings:

Public Sub UpdateGlobalTaxType(isExclusive As Boolean)
    Try
        Dim newStatus As Integer = If(isExclusive, 1, 0)
        If UpdatePosSettingByName("GlobalTaxType", newStatus) Then
            _globalSetting.TaxExculsive = isExclusive
        End If
    Catch ex As Exception
        XtraMessageBox.Show($"Error updating GlobalTaxType: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
End Sub

Public Sub UpdatePriceEditSetting(allowEdit As Boolean)
    Try
        Dim newStatus As Integer = If(allowEdit, 1, 0)
        If UpdatePosSettingByName("PriceEdit", newStatus) Then
            _globalSetting.PriceEdit = allowEdit
        End If
    Catch ex As Exception
        XtraMessageBox.Show($"Error updating PriceEdit: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
End Sub

Public Sub UpdateServiceTaxActive(isActive As Boolean)
    Try
        Dim newStatus As Integer = If(isActive, 1, 0)
        If UpdatePosSettingByName("ServiceTaxActive", newStatus) Then
            _globalSetting.ServiceTaxActive = isActive
        End If
    Catch ex As Exception
        XtraMessageBox.Show($"Error updating ServiceTaxActive: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
End Sub

Public Sub UpdateServiceTaxValue(taxValue As String)
    Try
        ' For value-type settings (Type = 1), we need to find the existing setting first
        Dim settingRow() As DataRow = _JsonData.PosSettingsTable.Select("Name = 'ServiceTaxValue'")

        If settingRow.Length > 0 Then
            Dim existingStatus As Integer = Convert.ToInt32(settingRow(0)("Status"))
            If UpdatePosSetting("ServiceTaxValue", existingStatus, taxValue, 1) Then
                _globalSettingValues.ServiceTaxValue = taxValue
            End If
        End If
    Catch ex As Exception
        XtraMessageBox.Show($"Error updating ServiceTaxValue: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
End Sub
