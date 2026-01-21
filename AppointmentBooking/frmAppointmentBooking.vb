Imports System
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

''' <summary>
''' Simple Appointment Booking Form for advance bookings with email reminders
''' Compatible with DevExpress 13.1
''' </summary>
Public Class frmAppointmentBooking

    ' Collection to store appointments (in a real application, this would be saved to a database)
    Private appointments As New List(Of AppointmentData)
    Private currentAppointmentId As Integer = 0 ' Track current appointment being edited

    Public Sub New()
        InitializeComponent()
        InitializeForm()
    End Sub

    ''' <summary>
    ''' Initialize form settings and default values
    ''' </summary>
    Private Sub InitializeForm()
        Try
            ' Set default values
            dateAppointment.EditValue = DateTime.Today
            timeStart.EditValue = DateTime.Today.AddHours(9) ' 9:00 AM
            timeEnd.EditValue = DateTime.Today.AddHours(10) ' 10:00 AM
            spinReminderValue.EditValue = 30
            cboReminderUnit.SelectedIndex = 0 ' Minutes

            ' Set button initial state
            btnSave.Text = "Save New"
            btnSave.Enabled = True

            ' Load appointments on form load
            LoadAppointments()

        Catch ex As Exception
            XtraMessageBox.Show("Error initializing form: " & ex.Message, "Error", _
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Save Appointment button click event
    ''' </summary>
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            ' Validate inputs
            If Not ValidateInputs() Then
                Return
            End If
            If btnSave.Text = "Update" Then
                BtnUpdate_Click(Nothing, Nothing)
            Else
                ' Create appointment
                Dim appointment As AppointmentData = CreateAppointment()

                ' Save to database
                Dim newAppointmentId As Integer = AppointmentDBHelper.SaveAppointment(appointment)

                If newAppointmentId > 0 Then
                    ' Set the returned ID
                    appointment.AppointmentId = newAppointmentId

                    ' Add to collection
                    appointments.Add(appointment)

                    ' Show success message
                    XtraMessageBox.Show( _
                        String.Format("Appointment saved successfully!{0}{0}" & _
                                     "Appointment ID: {1}{0}" & _
                                     "Customer: {2}{0}" & _
                                     "Date: {3:dd/MM/yyyy}{0}" & _
                                     "Time: {4:HH:mm} - {5:HH:mm}{0}" & _
                                     "Reminder: {6} {7} before appointment{0}{0}" & _
                                     "A reminder email will be sent to: {8}", _
                                     Environment.NewLine, _
                                     appointment.AppointmentId, _
                                     appointment.CustomerName, _
                                     appointment.AppointmentDate, _
                                     appointment.StartTime, _
                                     appointment.EndTime, _
                                     appointment.ReminderValue, _
                                     appointment.ReminderUnit, _
                                     appointment.EmailAddress), _
                        "Success", _
                        MessageBoxButtons.OK, _
                        MessageBoxIcon.Information)

                    ' Clear form after successful save
                    ClearForm()
                Else
                    XtraMessageBox.Show("Failed to save appointment. Please try again.", "Error", _
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
           

        Catch ex As Exception
            XtraMessageBox.Show("Error saving appointment: " & ex.Message, "Error", _
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Clear button click event
    ''' </summary>
    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    ''' <summary>
    ''' Validate all input fields
    ''' </summary>
    Private Function ValidateInputs() As Boolean
        ' Check customer name
        If String.IsNullOrWhiteSpace(txtCustomerName.Text) Then
            XtraMessageBox.Show("Please enter customer name.", "Validation", _
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCustomerName.Focus()
            Return False
        End If

        ' Check email address
        If String.IsNullOrWhiteSpace(txtEmailAddress.Text) Then
            XtraMessageBox.Show("Please enter email address.", "Validation", _
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtEmailAddress.Focus()
            Return False
        End If

        ' Validate email format
        If Not IsValidEmail(txtEmailAddress.Text) Then
            XtraMessageBox.Show("Please enter a valid email address.", "Validation", _
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtEmailAddress.Focus()
            Return False
        End If

        ' Check appointment date
        If dateAppointment.EditValue Is Nothing Then
            XtraMessageBox.Show("Please select appointment date.", "Validation", _
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
            dateAppointment.Focus()
            Return False
        End If

        ' Check if date is in the past
        Dim selectedDate As DateTime = CType(dateAppointment.EditValue, DateTime)
        If selectedDate.Date < DateTime.Today Then
            XtraMessageBox.Show("Appointment date cannot be in the past.", "Validation", _
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
            dateAppointment.Focus()
            Return False
        End If

        ' Check time values
        If timeStart.EditValue Is Nothing Then
            XtraMessageBox.Show("Please select start time.", "Validation", _
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
            timeStart.Focus()
            Return False
        End If

        If timeEnd.EditValue Is Nothing Then
            XtraMessageBox.Show("Please select end time.", "Validation", _
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
            timeEnd.Focus()
            Return False
        End If

        ' Validate end time is after start time
        Dim startTime As DateTime = CType(timeStart.EditValue, DateTime)
        Dim endTime As DateTime = CType(timeEnd.EditValue, DateTime)

        If endTime <= startTime Then
            XtraMessageBox.Show("End time must be after start time.", "Validation", _
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
            timeEnd.Focus()
            Return False
        End If

        ' Check reminder unit
        If cboReminderUnit.SelectedIndex < 0 Then
            XtraMessageBox.Show("Please select reminder unit.", "Validation", _
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cboReminderUnit.Focus()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Validate email address format
    ''' </summary>
    Private Function IsValidEmail(email As String) As Boolean
        Try
            Dim addr = New System.Net.Mail.MailAddress(email)
            Return addr.Address = email
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Create appointment object from form data
    ''' </summary>
    Private Function CreateAppointment() As AppointmentData
        Dim appointment As New AppointmentData()

        ' Basic information
        appointment.CustomerId = txtCustomerId.Text.Trim()
        appointment.CustomerName = txtCustomerName.Text.Trim()
        appointment.EmailAddress = txtEmailAddress.Text.Trim()
        appointment.Notes = memoNotes.Text.Trim()

        ' Date and time
        Dim appointmentDate As DateTime = CType(dateAppointment.EditValue, DateTime)
        Dim startTime As DateTime = CType(timeStart.EditValue, DateTime)
        Dim endTime As DateTime = CType(timeEnd.EditValue, DateTime)

        ' Combine date with start and end times
        appointment.AppointmentDate = appointmentDate.Date
        appointment.StartTime = appointmentDate.Date.Add(startTime.TimeOfDay)
        appointment.EndTime = appointmentDate.Date.Add(endTime.TimeOfDay)

        ' Reminder settings
        appointment.ReminderValue = CInt(spinReminderValue.Value)
        appointment.ReminderUnit = cboReminderUnit.Text

        ' Calculate reminder date/time
        appointment.ReminderDateTime = CalculateReminderDateTime( _
            appointment.StartTime, _
            appointment.ReminderValue, _
            appointment.ReminderUnit)

        ' Set creation time
        appointment.CreatedDateTime = DateTime.Now

        Return appointment
    End Function

    ''' <summary>
    ''' Calculate when the reminder should be triggered
    ''' </summary>
    Private Function CalculateReminderDateTime(appointmentStart As DateTime, _
                                              reminderValue As Integer, _
                                              reminderUnit As String) As DateTime
        Select Case reminderUnit.ToLower()
            Case "minutes"
                Return appointmentStart.AddMinutes(-reminderValue)
            Case "hours"
                Return appointmentStart.AddHours(-reminderValue)
            Case "days"
                Return appointmentStart.AddDays(-reminderValue)
            Case Else
                Return appointmentStart.AddMinutes(-30) ' Default to 30 minutes
        End Select
    End Function

    ''' <summary>
    ''' Clear all form fields
    ''' </summary>
    Private Sub ClearForm()
        currentAppointmentId = 0
        txtCustomerId.Text = String.Empty
        txtCustomerName.Text = String.Empty
        txtEmailAddress.Text = String.Empty
        dateAppointment.EditValue = DateTime.Today
        timeStart.EditValue = DateTime.Today.AddHours(9)
        timeEnd.EditValue = DateTime.Today.AddHours(10)
        spinReminderValue.EditValue = 30
        cboReminderUnit.SelectedIndex = 0
        memoNotes.Text = String.Empty
        txtCustomerName.Focus()
        btnSave.Text = "Save New"
        btnSave.Enabled = True
    End Sub

    ''' <summary>
    ''' Load all appointments from database into grid
    ''' </summary>
    Private Sub LoadAppointments()
        Try
            Dim dt As DataTable = AppointmentDBHelper.ViewAppointments()

            ' Clear existing data source
            gridAppointments.DataSource = Nothing
            gridAppointments.DataSource = dt

            ' Note: Uncomment above lines when grid is added to designer
            ' For now, just load into collection
            appointments.Clear()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    Dim appt As New AppointmentData()
                    appt.AppointmentId = If(IsDBNull(row("AppointmentId")), 0, Convert.ToInt32(row("AppointmentId")))
                    appt.CustomerId = If(IsDBNull(row("CustomerId")), Nothing, CType(Convert.ToInt32(row("CustomerId")), Integer?))
                    appt.CustomerName = If(IsDBNull(row("CustomerName")), String.Empty, row("CustomerName").ToString())
                    appt.EmailAddress = If(IsDBNull(row("EmailAddress")), String.Empty, row("EmailAddress").ToString())
                    appt.AppointmentDate = If(IsDBNull(row("AppointmentDate")), DateTime.Today, Convert.ToDateTime(row("AppointmentDate")))
                    appt.StartTime = If(IsDBNull(row("StartTime")), DateTime.Now, Convert.ToDateTime(row("StartTime")))
                    appt.EndTime = If(IsDBNull(row("EndTime")), DateTime.Now, Convert.ToDateTime(row("EndTime")))
                    appt.ReminderValue = If(IsDBNull(row("ReminderValue")), 30, Convert.ToInt32(row("ReminderValue")))
                    appt.ReminderUnit = If(IsDBNull(row("ReminderUnit")), "Minutes", row("ReminderUnit").ToString())
                    appt.ReminderDateTime = If(IsDBNull(row("ReminderDateTime")), DateTime.Now, Convert.ToDateTime(row("ReminderDateTime")))
                    appt.Notes = If(IsDBNull(row("Notes")), String.Empty, row("Notes").ToString())
                    appt.CreatedDateTime = If(IsDBNull(row("CreatedDateTime")), DateTime.Now, Convert.ToDateTime(row("CreatedDateTime")))

                    appointments.Add(appt)
                Next
            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error loading appointments: " & ex.Message, "Error", _
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Update existing appointment
    ''' </summary>
    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs)
        Try
            If currentAppointmentId = 0 Then
                XtraMessageBox.Show("Please select an appointment to update.", "Information", _
                                  MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Validate inputs
            If Not ValidateInputs() Then
                Return
            End If

            ' Confirm update
            If XtraMessageBox.Show("Are you sure you want to update this appointment?", "Confirm Update", _
                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Return
            End If

            ' Create appointment with existing ID
            Dim appointment As AppointmentData = CreateAppointment()
            appointment.AppointmentId = currentAppointmentId

            ' Update in database
            If AppointmentDBHelper.UpdateAppointment(appointment) Then
                XtraMessageBox.Show("Appointment updated successfully!", "Success", _
                                  MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Refresh list and clear form
                LoadAppointments()
                ClearForm()
            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error updating appointment: " & ex.Message, "Error", _
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Delete selected appointment
    ''' </summary>
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If currentAppointmentId = 0 Then
                XtraMessageBox.Show("Please select an appointment to delete.", "Information", _
                                  MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Confirm deletion
            If XtraMessageBox.Show("Are you sure you want to delete this appointment?" & Environment.NewLine & _
                                  "This action cannot be undone.", "Confirm Delete", _
                                  MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
                Return
            End If

            ' Delete from database
            If AppointmentDBHelper.DeleteAppointment(currentAppointmentId) Then
                XtraMessageBox.Show("Appointment deleted successfully!", "Success", _
                                  MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Refresh list and clear form
                LoadAppointments()
                ClearForm()
            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error deleting appointment: " & ex.Message, "Error", _
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Refresh appointments list
    ''' </summary>
    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadAppointments()
        XtraMessageBox.Show("Appointments list refreshed.", "Information", _
                          MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' Load selected appointment into form for editing
    ''' </summary>
    Private Sub LoadAppointmentToForm(appointment As AppointmentData)
        Try
            currentAppointmentId = appointment.AppointmentId
            txtCustomerName.Text = appointment.CustomerName
            txtEmailAddress.Text = appointment.EmailAddress
            dateAppointment.EditValue = appointment.AppointmentDate
            timeStart.EditValue = appointment.StartTime
            timeEnd.EditValue = appointment.EndTime
            spinReminderValue.EditValue = appointment.ReminderValue
            cboReminderUnit.Text = appointment.ReminderUnit
            memoNotes.Text = appointment.Notes

            btnSave.Text = "Update"
            btnSave.Enabled = True

        Catch ex As Exception
            XtraMessageBox.Show("Error loading appointment: " & ex.Message, "Error", _
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Get all appointments (for external use)
    ''' </summary>
    Public Function GetAppointments() As List(Of AppointmentData)
        Return appointments
    End Function

    Private Sub gridViewAppointments_DoubleClick(sender As Object, e As EventArgs) Handles gridViewAppointments.DoubleClick
        Try
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
            If view.FocusedRowHandle >= 0 Then
                Dim appointmentId As Integer = CInt(view.GetRowCellValue(view.FocusedRowHandle, "AppointmentId"))
                Dim appointment As AppointmentData = AppointmentDBHelper.GetAppointmentById(appointmentId)
                If appointment IsNot Nothing Then
                    LoadAppointmentToForm(appointment)
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCustomerId_DoubleClick(sender As Object, e As EventArgs) Handles txtCustomerId.DoubleClick
        Try
            Dim customerListForm As New FrmCustomerList(True) ' True for selection mode

            If customerListForm.ShowDialog() = DialogResult.OK Then
                ' Customer was selected
                txtCustomerId.Text = customerListForm.SelectedCustomerId
                txtCustomerName.Text = customerListForm.SelectedCustomerName
                txtEmailAddress.Text = customerListForm.SelectedCustomerPhone
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class

''' <summary>
''' Data class to store appointment information
''' </summary>
Public Class AppointmentData
    Public Property AppointmentId As Integer
    Public Property CustomerId As Integer?
    Public Property CustomerName As String
    Public Property EmailAddress As String
    Public Property AppointmentDate As DateTime
    Public Property StartTime As DateTime
    Public Property EndTime As DateTime
    Public Property ReminderValue As Integer
    Public Property ReminderUnit As String
    Public Property ReminderDateTime As DateTime
    Public Property Notes As String
    Public Property CreatedDateTime As DateTime

    Public Sub New()
        AppointmentId = 0
        CustomerId = Nothing
        CustomerName = String.Empty
        EmailAddress = String.Empty
        AppointmentDate = DateTime.Today
        StartTime = DateTime.Now
        EndTime = DateTime.Now
        ReminderValue = 30
        ReminderUnit = "Minutes"
        ReminderDateTime = DateTime.Now
        Notes = String.Empty
        CreatedDateTime = DateTime.Now
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0} - {1:dd/MM/yyyy HH:mm}", CustomerName, StartTime)
    End Function
End Class
