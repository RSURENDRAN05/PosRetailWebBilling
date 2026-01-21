Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors

''' <summary>
''' Database Helper class for Appointment Booking operations
''' Uses sp_AppointmentBooking stored procedure
''' </summary>
Public Class AppointmentDBHelper

    ''' <summary>
    ''' Save new appointment to database
    ''' </summary>
    Public Shared Function SaveAppointment(appointment As AppointmentData) As Integer
        Try
            Dim cmd As New SqlCommand("sp_AppointmentBooking", M_CONNECT.conn)
            cmd.CommandType = CommandType.StoredProcedure

            ' Add parameters
            cmd.Parameters.AddWithValue("@Mode", "SAVE")
            cmd.Parameters.AddWithValue("@CustomerId", If(appointment.CustomerId.HasValue, CObj(appointment.CustomerId.Value), DBNull.Value))
            cmd.Parameters.AddWithValue("@CustomerName", appointment.CustomerName)
            cmd.Parameters.AddWithValue("@EmailAddress", appointment.EmailAddress)
            cmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate)
            cmd.Parameters.AddWithValue("@StartTime", appointment.StartTime)
            cmd.Parameters.AddWithValue("@EndTime", appointment.EndTime)
            cmd.Parameters.AddWithValue("@ReminderValue", appointment.ReminderValue)
            cmd.Parameters.AddWithValue("@ReminderUnit", appointment.ReminderUnit)
            cmd.Parameters.AddWithValue("@ReminderDateTime", appointment.ReminderDateTime)
            cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(appointment.Notes), DBNull.Value, CObj(appointment.Notes)))

            ' Open connection if closed
            If M_CONNECT.conn.State = ConnectionState.Closed Then
                M_CONNECT.conn.Open()
            End If

            ' Execute and get new AppointmentId
            Dim result As Object = cmd.ExecuteScalar()
            Dim appointmentId As Integer = 0

            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                appointmentId = Convert.ToInt32(result)
            End If

            Return appointmentId

        Catch ex As Exception
            XtraMessageBox.Show("Error saving appointment: " & ex.Message, "Database Error", _
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            If M_CONNECT.conn.State = ConnectionState.Open Then
                M_CONNECT.conn.Close()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Update existing appointment in database
    ''' </summary>
    Public Shared Function UpdateAppointment(appointment As AppointmentData) As Boolean
        Try
            Dim cmd As New SqlCommand("sp_AppointmentBooking", M_CONNECT.conn)
            cmd.CommandType = CommandType.StoredProcedure

            ' Add parameters
            cmd.Parameters.AddWithValue("@Mode", "UPDATE")
            cmd.Parameters.AddWithValue("@AppointmentId", appointment.AppointmentId)
            cmd.Parameters.AddWithValue("@CustomerId", If(appointment.CustomerId.HasValue, CObj(appointment.CustomerId.Value), DBNull.Value))
            cmd.Parameters.AddWithValue("@CustomerName", appointment.CustomerName)
            cmd.Parameters.AddWithValue("@EmailAddress", appointment.EmailAddress)
            cmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate)
            cmd.Parameters.AddWithValue("@StartTime", appointment.StartTime)
            cmd.Parameters.AddWithValue("@EndTime", appointment.EndTime)
            cmd.Parameters.AddWithValue("@ReminderValue", appointment.ReminderValue)
            cmd.Parameters.AddWithValue("@ReminderUnit", appointment.ReminderUnit)
            cmd.Parameters.AddWithValue("@ReminderDateTime", appointment.ReminderDateTime)
            cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(appointment.Notes), DBNull.Value, CObj(appointment.Notes)))

            ' Open connection if closed
            If M_CONNECT.conn.State = ConnectionState.Closed Then
                M_CONNECT.conn.Open()
            End If

            ' Execute update
            cmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            XtraMessageBox.Show("Error updating appointment: " & ex.Message, "Database Error", _
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If M_CONNECT.conn.State = ConnectionState.Open Then
                M_CONNECT.conn.Close()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Delete appointment from database
    ''' </summary>
    Public Shared Function DeleteAppointment(appointmentId As Integer) As Boolean
        Try
            Dim cmd As New SqlCommand("sp_AppointmentBooking", M_CONNECT.conn)
            cmd.CommandType = CommandType.StoredProcedure

            ' Add parameters
            cmd.Parameters.AddWithValue("@Mode", "DELETE")
            cmd.Parameters.AddWithValue("@AppointmentId", appointmentId)

            ' Open connection if closed
            If M_CONNECT.conn.State = ConnectionState.Closed Then
                M_CONNECT.conn.Open()
            End If

            ' Execute delete
            cmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            XtraMessageBox.Show("Error deleting appointment: " & ex.Message, "Database Error", _
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If M_CONNECT.conn.State = ConnectionState.Open Then
                M_CONNECT.conn.Close()
            End If
        End Try
    End Function

    ''' <summary>
    ''' View all appointments or specific appointment
    ''' </summary>
    Public Shared Function ViewAppointments(Optional appointmentId As Integer? = Nothing, _
                                           Optional customerId As Integer? = Nothing) As DataTable
        Dim dt As New DataTable()

        Try
            Dim cmd As New SqlCommand("sp_AppointmentBooking", M_CONNECT.conn)
            cmd.CommandType = CommandType.StoredProcedure

            ' Add parameters
            cmd.Parameters.AddWithValue("@Mode", "VIEW")
            cmd.Parameters.AddWithValue("@AppointmentId", If(appointmentId.HasValue, CObj(appointmentId.Value), DBNull.Value))
            cmd.Parameters.AddWithValue("@CustomerId", If(customerId.HasValue, CObj(customerId.Value), DBNull.Value))

            ' Open connection if closed
            If M_CONNECT.conn.State = ConnectionState.Closed Then
                M_CONNECT.conn.Open()
            End If

            ' Fill data table
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(dt)

        Catch ex As Exception
            XtraMessageBox.Show("Error viewing appointments: " & ex.Message, "Database Error", _
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If M_CONNECT.conn.State = ConnectionState.Open Then
                M_CONNECT.conn.Close()
            End If
        End Try

        Return dt
    End Function

    ''' <summary>
    ''' Get single appointment by ID
    ''' </summary>
    Public Shared Function GetAppointmentById(appointmentId As Integer) As AppointmentData
        Try
            Dim dt As DataTable = ViewAppointments(appointmentId)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Return MapDataRowToAppointment(row)
            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error getting appointment: " & ex.Message, "Database Error", _
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return Nothing
    End Function

    ''' <summary>
    ''' Map DataRow to AppointmentData object
    ''' </summary>
    Private Shared Function MapDataRowToAppointment(row As DataRow) As AppointmentData
        Dim appointment As New AppointmentData()

        appointment.AppointmentId = If(IsDBNull(row("AppointmentId")), 0, Convert.ToInt32(row("AppointmentId")))
        appointment.CustomerId = If(IsDBNull(row("CustomerId")), Nothing, CType(Convert.ToInt32(row("CustomerId")), Integer?))
        appointment.CustomerName = If(IsDBNull(row("CustomerName")), String.Empty, row("CustomerName").ToString())
        appointment.EmailAddress = If(IsDBNull(row("EmailAddress")), String.Empty, row("EmailAddress").ToString())
        appointment.AppointmentDate = If(IsDBNull(row("AppointmentDate")), DateTime.Today, Convert.ToDateTime(row("AppointmentDate")))
        appointment.StartTime = If(IsDBNull(row("StartTime")), DateTime.Now, Convert.ToDateTime(row("StartTime")))
        appointment.EndTime = If(IsDBNull(row("EndTime")), DateTime.Now, Convert.ToDateTime(row("EndTime")))
        appointment.ReminderValue = If(IsDBNull(row("ReminderValue")), 30, Convert.ToInt32(row("ReminderValue")))
        appointment.ReminderUnit = If(IsDBNull(row("ReminderUnit")), "Minutes", row("ReminderUnit").ToString())
        appointment.ReminderDateTime = If(IsDBNull(row("ReminderDateTime")), DateTime.Now, Convert.ToDateTime(row("ReminderDateTime")))
        appointment.Notes = If(IsDBNull(row("Notes")), String.Empty, row("Notes").ToString())
        appointment.CreatedDateTime = If(IsDBNull(row("CreatedDateTime")), DateTime.Now, Convert.ToDateTime(row("CreatedDateTime")))

        Return appointment
    End Function

End Class
