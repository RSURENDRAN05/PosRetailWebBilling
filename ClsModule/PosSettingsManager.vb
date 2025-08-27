Imports System.Net
Imports Newtonsoft.Json.Linq
Imports DevExpress.XtraEditors
Imports Newtonsoft.Json
Imports System.Data

Module PosSettingsManager
    Public Structure _globalSetting
        Public Shared TaxExculsive As Boolean = False   'true exclusive  or false  inclusive
        Public Shared QuoteBill As Boolean = False
        Public Shared PaymentMachine As Boolean = False
        Public Shared ResponseData As Object = ""
        Public Shared PriceEdit As Boolean = False
        Public Shared ServiceTaxActive As Boolean = False
        Public Shared SearchProductCode As Boolean = True 'SearchByProductcode,SearchByBarcode
        Public Shared BillDiscountAcitve As Boolean = False
        Public Shared ItemDiscountActive As Boolean = False
        Public Shared SelectMultiplePriceActive As Boolean = False
        Public Shared SalesManEachItemActive As Boolean = False
        Public Shared PosBillScreenActive As Boolean = True
        Public Shared ItemDeleteActive As Boolean = False
        Public Shared QtyChangeActive As Boolean = False
        Public Shared CurrencySimple As String = "RM"
        Public Shared _SalesItemMount As Boolean = False
        Public Shared _AutoShiftClose As Boolean = False
        Public Shared _AutoDayClose As Boolean = False
        Public Shared _AutoShiftOpen As Boolean = True
        Public Shared _AutoDayOpen As Boolean = True
        Public Shared _ShiftPrintDos As Boolean = True
        Public Shared ProductWithBarcode As Boolean = False
        Public Shared PrintShiftClose As Boolean = True
        Public Shared PrintDayClose As Boolean = True
        Public Shared SalePriceOnSales As Boolean = False
        Public Shared SuperUserPassword As Boolean = False
        Public Shared BackDisplayClear As Boolean = False
        Public Shared DayTaxPrint As Boolean = False
        Public Shared ServiceChargeActive As Boolean = False
        Public Shared TakeawayChargeActive As Boolean = False
        Public Shared ServiceChargeValue As Integer = 0
        Public Shared TakeawayChargeValue As Integer = 0
        Public Shared MultiplePayment As Boolean = False
        Public Shared _dualscreenoption As Boolean = False
        Public Shared ItemCancelPrint As Boolean = False
        Public Shared ChangeToCash As Boolean = False
        Public Shared AutoSyncSales As Boolean = True
    End Structure
    Public Structure _globalSettingValues
        Public Shared ServiceTaxValue As String = "0"
    End Structure
    Public Sub LoadPosSettings()
        Try
            If _JsonData.PosSettingsTable.Rows.Count = 0 Then
                getPosSettingsInfo()
            End If

            If _JsonData.PosSettingsTable.Rows.Count > 0 Then

                For Each _setRow In _JsonData.PosSettingsTable.Select("Type = '0'") 'Status
                    Select Case _setRow("Name")
                        Case "GlobalTaxType"
                            If _setRow("Status").ToString = "0" Then
                                _globalSetting.TaxExculsive = False 'InclusiveTax
                            Else
                                _globalSetting.TaxExculsive = True  'ExclusiveTax
                            End If
                        Case "PriceEdit"
                            If _setRow("Status").ToString = "0" Then
                                _globalSetting.PriceEdit = False
                            Else
                                _globalSetting.PriceEdit = True
                            End If
                        Case "ServiceTaxActive"
                            If _setRow("Status").ToString = "0" Then
                                _globalSetting.ServiceTaxActive = False
                            Else
                                _globalSetting.ServiceTaxActive = True
                            End If
                        Case "SearchProductCode"
                            If _setRow("Status").ToString = "0" Then
                                _globalSetting.SearchProductCode = False
                            Else
                                _globalSetting.SearchProductCode = True
                            End If
                        Case "BillDiscountAcitve"
                            If _setRow("Status").ToString = "0" Then
                                _globalSetting.BillDiscountAcitve = False
                            Else
                                _globalSetting.BillDiscountAcitve = True
                            End If
                        Case "ItemDiscountActive"
                            If _setRow("Status").ToString = "0" Then
                                _globalSetting.ItemDiscountActive = False
                            Else
                                _globalSetting.ItemDiscountActive = True
                            End If
                        Case "SelectMultiplePriceActive"
                            If _setRow("Status").ToString = "0" Then
                                _globalSetting.SelectMultiplePriceActive = False
                            Else
                                _globalSetting.SelectMultiplePriceActive = True
                            End If
                        Case "SalesManEachItemActive"
                            If _setRow("Status").ToString = "0" Then
                                _globalSetting.SalesManEachItemActive = False
                            Else
                                _globalSetting.SalesManEachItemActive = True
                            End If
                        Case "ItemDeleteActive"
                            If _setRow("Status").ToString = "0" Then
                                _globalSetting.ItemDeleteActive = False
                            Else
                                _globalSetting.ItemDeleteActive = True
                            End If
                        Case "QtyChangeActive"
                            If _setRow("Status").ToString = "0" Then
                                _globalSetting.QtyChangeActive = False
                            Else
                                _globalSetting.QtyChangeActive = True
                            End If
                    End Select
                Next
                For Each _setRow In _JsonData.PosSettingsTable.Select("Type = '1'") 'Values
                    Select Case _setRow("Name")
                        Case "ServiceTaxValue"
                            _globalSettingValues.ServiceTaxValue = _setRow("Value").ToString
                    End Select
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function UpdatePosSetting(settingName As String, settingStatus As Integer, settingValue As String, settingType As Integer) As Boolean
        Try
            ' Find the setting ID from the PosSettingsTable
            Dim settingRow() As DataRow = _JsonData.PosSettingsTable.Select("Name = '" & settingName & "'")

            If settingRow.Length = 0 Then
                XtraMessageBox.Show("Setting '" & settingName & "' not found in PosSettingsTable.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            Dim settingId As Integer = Convert.ToInt32(settingRow(0)("Id"))

            ' Create JSON data for update
            Dim updateData As New Dictionary(Of String, Object) From {
                {"operation", "UPDATE"},
                {"Id", settingId},
                {"Name", settingName},
                {"Status", settingStatus},
                {"Value", settingValue},
                {"Type", settingType}
            }

            Dim jsonString As String = JsonConvert.SerializeObject(updateData)

            ' Make HTTP request to update the setting
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim url As String = M_Details.LinkAjaxRequest & "GroupPolicyRequest=8&json=" & Uri.EscapeDataString(jsonString)

            Dim json As String = New System.Net.WebClient().DownloadString(url)
            Dim responseJson As JObject = JObject.Parse(json)

            ' Check if update was successful
            If responseJson("Success") IsNot Nothing AndAlso CBool(responseJson("Success")) Then
                ' Update the local table
                settingRow(0)("Status") = settingStatus
                settingRow(0)("Value") = settingValue
                settingRow(0)("Type") = settingType
                _JsonData.PosSettingsTable.AcceptChanges()

                ' Reload settings to apply changes to global variables
                LoadPosSettings()

                Return True
            Else
                XtraMessageBox.Show("Failed to update setting on server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error updating POS setting: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function


End Module
