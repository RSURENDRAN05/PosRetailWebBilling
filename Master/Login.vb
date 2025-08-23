Imports DevExpress.XtraEditors
Public Class Login
    'Public ini As New IniFile(M_Details.AppPath & "\Settings\" & "Settings.ini")
    Dim LocationId As Integer = 0
    Dim CompanyId As Integer = 0

    Public Sub New()
        'Dim dta As New DataTable
        'dta = MySqlDataAdapter("select * from user_table")
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            InitializeComponent()

            ' Apply the current skin to Login form (don't reload, just apply what's already set)
            SkinManager.LoadSkinSetting()

            M_Details.LinkAjaxRequest = ini.ReadValue("Profile", "UrlLink")
            M_Details.LinkAjaxRequestCheque = ini.ReadValue("Profile", "UrlLinkCheque")
            M_Details.licenceServerCleint = ini.ReadValue("Profile", "ServerClient")
            LocationId = ini.ReadValue("Bank", "LocationId")
            CompanyId = ini.ReadValue("Bank", "CompanyId")
           
            If chkRegistryKey(M_Details.licenceActive) = False Then
                End
            End If
            If CheckForInternetConnection() = False Then
                XtraMessageBox.Show("No Internet Connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            Else
                dialog.Caption = "Getting User Information"
                If getUserInfo() = True Then
                    txtusername.Properties.DataSource = _JsonData.USerTable
                    dialog.Caption = "User Data Received"
                Else
                    dialog.Caption = "User Data Not Received"
                End If
                dialog.Caption = "Getting Company Information"
                If getComapnyLocationInfo() = True Then
                    dialog.Caption = "Company Data Received"
                    GridLookUuCompany.Properties.DataSource = _JsonData.CompanyLocationTable
                Else
                    dialog.Caption = "Company Data Not Received"
                End If
                If getComapnyInfo() = True Then
                    dialog.Caption = "Company Data Received"

                Else
                    dialog.Caption = "Company Data Not Received"
                End If
                dialog.Caption = "Getting Location Information"
                If getLocationInfo() = True Then
                    dialog.Caption = "Location Data Received"
                Else
                    dialog.Caption = "Location Data Not Received"
                End If
                If getPosSettingsInfo() = True Then
                    LoadPosSettings()
                    dialog.Caption = "Loading Pos Settings"
                Else
                    dialog.Caption = "Pos Setting Data Not Received"
                End If
                If getSalesManCommissionInfo() = True Then

                    dialog.Caption = "Loading SalesCommission"
                Else
                    dialog.Caption = "SalesCommission Data Not Received"
                End If
                If getPaymentTermTable() = True Then
                    dialog.Caption = "Loading Payment Term"
                Else
                    dialog.Caption = "Payment Term Data Not Received"
                End If
                GridLookUuCompany.EditValue = LocationId


            End If
        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try


    End Sub
    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Try
            Application.Exit()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        Try
            If txtusername.Text = "" OrElse txtusername.Text Is Nothing Then
                XtraMessageBox.Show("User Name Not Valied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ElseIf txtpassword.Text = "" OrElse txtpassword.Text Is Nothing Then
                XtraMessageBox.Show("User Name Not Valied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                If _validateUserLogin(Trim(txtusername.Text), Trim(txtpassword.Text)) = True Then
                    'XtraMessageBox.Show("Welcome " & txtusername.Text & "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Dim _SELECTEDROW = GridLookUuCompany.GetSelectedDataRow
                    _companyInfo.ComId = CompanyId
                    _companyInfo.CompanyName = GridLookUuCompany.GetSelectedDataRow(1).ToString
                    _companyInfo.LocId = LocationId
                    _companyInfo.LocationName = GridLookUuCompany.GetSelectedDataRow(3).ToString
                    ValidationProcess()
                    Me.Hide()
                    MainMaster.Show()

                Else
                    XtraMessageBox.Show("User Id Or Password Is Wrong " & txtusername.Text & "!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtpassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpassword.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                btnlogin_Click(Nothing, Nothing)
            End If
        Catch ex As Exception

        End Try
    End Sub
#Region "Process For PM_Master"
    'Now Start Validation Process
    Private Sub ValidationProcess()
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            dialog.Caption = "Validating POS Master data..."
            dialog.Show()

            ' Validate Company and Location - this checks if POS Master data exists
            Dim validationResult = managementModule.ValidateCompanyLocation(_companyInfo.ComId, _companyInfo.LocId)

            If validationResult.Item1 = False Then
                ' No POS Master data found - create new record
                dialog.Caption = "Creating new POS Master record..."

                ' Create new POS Master data
                Dim newPosMasterData = managementModule.CreateNewPosMasterData()

                ' Insert the new POS Master data
                Dim insertResult = managementModule.InsertPosMaster(newPosMasterData, _companyInfo.ComId, _companyInfo.LocId)

                If insertResult.Item1 Then
                    ' Successfully created POS Master data - now get the PM_ID
                    dialog.Caption = "Retrieving POS Master ID..."
                    Try
                        Dim posRecords = managementModule.GetPosMasterRecords(_companyInfo.ComId, _companyInfo.LocId)
                        If posRecords IsNot Nothing AndAlso posRecords.Rows.Count > 0 Then
                            ' Get the PM_ID from the first record (most recently created)
                            _companyInfo.CompanyPMId = Convert.ToInt32(posRecords.Rows(0)("PM_ID"))
                            dialog.Caption = "POS Master created successfully"
                        Else
                            _companyInfo.CompanyPMId = 0
                            dialog.Caption = "POS Master created but PM_ID could not be retrieved"
                        End If
                    Catch ex As Exception
                        _companyInfo.CompanyPMId = 0
                        dialog.Caption = "Error retrieving PM_ID: " & ex.Message
                    End Try
                Else
                    ' Failed to create POS Master data - show error and exit
                    dialog.Caption = "Failed to create POS Master data"
                    XtraMessageBox.Show("Error: Failed to create POS Master data: " & insertResult.Item2,
                                      "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Application.Exit()
                    Return
                End If
            Else
                ' POS Master data already exists - get the PM_ID only
                dialog.Caption = "Loading existing POS Master data..."
                Try
                    Dim posRecords = managementModule.GetPosMasterRecords(_companyInfo.ComId, _companyInfo.LocId)
                    If posRecords IsNot Nothing AndAlso posRecords.Rows.Count > 0 Then
                        ' Get only the PM_ID from the first record
                        ' Note: ShiftNo and DayNo will be set by shift validation process
                        _companyInfo.CompanyPMId = Convert.ToInt32(posRecords.Rows(0)("PM_ID"))
                        dialog.Caption = "POS Master data loaded successfully"
                    Else
                        _companyInfo.CompanyPMId = 0
                        dialog.Caption = "POS Master validation successful but no records found"
                    End If
                Catch ex As Exception
                    _companyInfo.CompanyPMId = 0
                    dialog.Caption = "Error retrieving PM_ID: " & ex.Message
                End Try
            End If

            ' After POS Master validation, validate shift status
            If ValidationForShiftClose() Then
                dialog.Caption = "Shift validation completed successfully"
            Else
                dialog.Caption = "Shift validation failed"
            End If

            ' After shift validation, validate day status
            If ValidationForDayClose() Then
                dialog.Caption = "Day validation completed successfully"
            Else
                dialog.Caption = "Day validation failed"
            End If

        Catch ex As Exception
            dialog.Caption = "Error during validation process"
            XtraMessageBox.Show("Error during validation process: " & ex.Message,
                              "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
        Finally
            dialog.Close()
        End Try
    End Sub
    Private Function ValidationForShiftClose() As Boolean
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            dialog.Caption = "Validating shift status..."
            dialog.Show()

            ' Get current user and PC name for shift creation
            Dim currentUser As String = If(_companyInfo.UserId > 0, _companyInfo.UserId.ToString(), "")
            Dim pcName As String = Environment.MachineName

            dialog.Caption = "Checking current shift status..."

            ' Validate current shift status and create if needed
            Dim validationResult = managementModule.ValidateCurrentShiftAndCreate(_companyInfo.ComId, _companyInfo.LocId, currentUser, pcName)

            If validationResult.Item1 Then
                ' Success - handle different actions
                Dim action As String = validationResult.Item2
                Dim message As String = validationResult.Item3
                Dim resultData = validationResult.Item4

                Select Case action.ToLower()
                    Case "validated"
                        ' Shift exists and is properly set up
                        dialog.Caption = "Shift validated successfully"

                    Case "created"
                        ' New shift was created
                        dialog.Caption = "New shift created successfully"

                    Case "updated"
                        ' POS Master was updated to Open
                        dialog.Caption = "Shift status synchronized successfully"

                    Case Else
                        ' Other successful actions
                        dialog.Caption = "Shift validation completed"
                End Select

                ' Update global shift information if available
                If resultData IsNot Nothing Then
                    UpdateShiftInfo(resultData)
                End If

                Return True

            Else
                ' Validation failed
                Dim errorMessage As String = validationResult.Item3
                dialog.Caption = "Shift validation failed: " & errorMessage
                Return False
            End If

        Catch ex As Exception
            dialog.Caption = "Error during shift validation: " & ex.Message
            Return False
        Finally
            dialog.Close()
        End Try
    End Function

    ''' <summary>
    ''' Centralized method to update shift information from JSON response
    ''' This ensures consistent handling and avoids duplicate code
    ''' </summary>
    Private Sub UpdateShiftInfo(resultData As Object)
        Try
            ' Update ShiftNo
            If resultData.ShiftNo IsNot Nothing Then
                If IsNumeric(resultData.ShiftNo.ToString()) Then
                    _companyInfo.CurShiftNo = Convert.ToInt32(resultData.ShiftNo.ToString())
                End If
            End If

            ' Update DayNo
            If resultData.DayNo IsNot Nothing Then
                If IsNumeric(resultData.DayNo.ToString()) Then
                    _companyInfo.CurDayNo = Convert.ToInt32(resultData.DayNo.ToString())
                End If
            End If
        Catch ex As Exception
            ' Log error but don't fail the process
            ' Values will remain as previously set or default
        End Try
    End Sub

    ''' <summary>
    ''' Centralized method to update day information from JSON response
    ''' This ensures consistent handling and avoids duplicate code
    ''' </summary>
    Private Sub UpdateDayInfo(resultData As Object)
        Try
            ' Update DayNo
            If resultData.DayNo IsNot Nothing Then
                If IsNumeric(resultData.DayNo.ToString()) Then
                    _companyInfo.CurDayNo = Convert.ToInt32(resultData.DayNo.ToString())
                End If
            End If

            ' Update ShiftNo (days may also contain shift information)
            If resultData.ShiftNo IsNot Nothing Then
                If IsNumeric(resultData.ShiftNo.ToString()) Then
                    _companyInfo.CurShiftNo = Convert.ToInt32(resultData.ShiftNo.ToString())
                End If
            End If

            ' Update PM_ID if provided
            If resultData.PM_ID IsNot Nothing Then
                If IsNumeric(resultData.PM_ID.ToString()) Then
                    _companyInfo.CompanyPMId = Convert.ToInt32(resultData.PM_ID.ToString())
                End If
            End If
        Catch ex As Exception
            ' Log error but don't fail the process
            ' Values will remain as previously set or default
        End Try
    End Sub
    Private Function ValidationForDayClose() As Boolean
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            dialog.Caption = "Validating day status..."
            dialog.Show()

            ' Get current user and PC name for day creation
            Dim currentUser As String = If(_companyInfo.UserId > 0, _companyInfo.UserId.ToString(), "")
            Dim pcName As String = Environment.MachineName

            dialog.Caption = "Checking current day status..."

            ' Validate current day status and create if needed
            Dim validationResult = managementModule.ValidateCurrentDayAndCreate(_companyInfo.ComId, _companyInfo.LocId, currentUser, pcName)

            If validationResult.Item1 Then
                ' Success - handle different actions
                Dim action As String = validationResult.Item2
                Dim message As String = validationResult.Item3
                Dim resultData = validationResult.Item4

                Select Case action.ToLower()
                    Case "validated"
                        ' Day exists and is properly set up
                        dialog.Caption = "Day validated successfully"

                    Case "created"
                        ' New day was created
                        dialog.Caption = "New day created successfully"

                    Case "updated"
                        ' POS Master was updated to Open
                        dialog.Caption = "Day status synchronized successfully"

                    Case Else
                        ' Other successful actions
                        dialog.Caption = "Day validation completed"
                End Select

                ' Update global day information if available
                If resultData IsNot Nothing Then
                    UpdateDayInfo(resultData)
                End If

                Return True

            Else
                ' Validation failed
                Dim errorMessage As String = validationResult.Item3
                dialog.Caption = "Day validation failed: " & errorMessage
                Return False
            End If

        Catch ex As Exception
            dialog.Caption = "Error during day validation: " & ex.Message
            Return False
        Finally
            dialog.Close()
        End Try
    End Function
#End Region



End Class
