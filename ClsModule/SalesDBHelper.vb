Imports System.Data.SqlClient
Imports System.Data
Imports PosRetailWebBilling.clssalesProperty

Public Class SalesDBHelper
    Private _connectionString As String = M_Details._Conn

    Public Sub New(connectionString As String)
        _connectionString = connectionString
    End Sub

    ''' <summary>
    ''' Safe conversion helper to handle DBNull and empty strings for Integer values
    ''' </summary>
    Private Function SafeToInt32(value As Object, defaultValue As Integer) As Integer
        If value Is Nothing OrElse value Is DBNull.Value Then
            Return defaultValue
        End If

        Dim stringValue As String = value.ToString().Trim()
        If String.IsNullOrEmpty(stringValue) Then
            Return defaultValue
        End If

        Dim result As Integer
        If Integer.TryParse(stringValue, result) Then
            Return result
        Else
            Return defaultValue
        End If
    End Function

    ''' <summary>
    ''' Safe conversion helper to handle DBNull and empty strings for Decimal values
    ''' </summary>
    Private Function SafeToDecimal(value As Object, defaultValue As Decimal) As Decimal
        If value Is Nothing OrElse value Is DBNull.Value Then
            Return defaultValue
        End If

        Dim stringValue As String = value.ToString().Trim()
        If String.IsNullOrEmpty(stringValue) Then
            Return defaultValue
        End If

        Dim result As Decimal
        If Decimal.TryParse(stringValue, result) Then
            Return result
        Else
            Return defaultValue
        End If
    End Function

    ''' <summary>
    ''' Safe string conversion to handle DBNull values
    ''' </summary>
    Public Function SafeToString(value As Object, defaultValue As String) As String
        If value Is Nothing OrElse value Is DBNull.Value Then
            Return defaultValue
        End If
        Return value.ToString().Trim()
    End Function

    ''' <summary>
    ''' Safe date conversion to handle DBNull and invalid date strings
    ''' </summary>
    Private Function SafeToDateTime(value As Object, defaultValue As DateTime) As DateTime
        If value Is Nothing OrElse value Is DBNull.Value Then
            Return defaultValue
        End If

        Dim stringValue As String = value.ToString().Trim()
        If String.IsNullOrEmpty(stringValue) Then
            Return defaultValue
        End If

        Dim result As DateTime
        If DateTime.TryParse(stringValue, result) Then
            Return result
        Else
            Return defaultValue
        End If
    End Function


    'Public Function SaveSalesBill(salesHeader As SalesHeader, salesDetailsList As List(Of SalesDetails), ByRef errorMessage As String, ByRef returnBillNo As String) As Boolean
    '    Dim connection As SqlConnection = Nothing
    '    Dim transaction As SqlTransaction = Nothing

    '    Try
    '        connection = New SqlConnection(_connectionString)
    '        connection.Open()
    '        transaction = connection.BeginTransaction()

    '        ' Save header and get transaction number
    '        Dim headerId As Integer = 0
    '        Dim transactionNumber As Integer = 0

    '        If Not SaveSalesHeader(salesHeader, connection, transaction, transactionNumber, headerId) Then
    '            transaction.Rollback()
    '            errorMessage = "Failed to save sales header. Transaction Number: " & transactionNumber.ToString() & ", Header ID: " & headerId.ToString()
    '            returnBillNo = "0"
    '            Return False
    '        End If

    '        ' Validate that we got valid IDs
    '        If transactionNumber <= 0 Then
    '            transaction.Rollback()
    '            errorMessage = "Invalid transaction number generated: " & transactionNumber.ToString()
    '            returnBillNo = "0"
    '            Return False
    '        End If

    '        If headerId <= 0 Then
    '            transaction.Rollback()
    '            errorMessage = "Invalid header ID generated: " & headerId.ToString()
    '            returnBillNo = "0"
    '            Return False
    '        End If

    '        ' Save all details
    '        For Each detail In salesDetailsList
    '            detail.psid_invoice_salid = headerId
    '            detail.psid_invoice_trno = transactionNumber.ToString()
    '            detail.psid_invoice_date = SafeToDateTime(salesHeader.psih_invoice_date, DateTime.Now)
    '            detail.psid_invoice_shiftno = salesHeader.psih_invoice_shiftno
    '            detail.psid_invoice_dayno = salesHeader.psih_invoice_dayno
    '            detail.psid_invoice_pmid = salesHeader.psih_invoice_pmid
    '            detail.psid_invoice_locid = salesHeader.psih_invoice_locid
    '            detail.psid_invoice_comid = salesHeader.psih_invoice_comid
    '            Dim detailId As Integer = 0
    '            If Not SaveSalesDetail(detail, connection, transaction, detailId) Then
    '                transaction.Rollback()
    '                errorMessage = "Failed to save sales detail"
    '                returnBillNo = "0"
    '                Return False
    '            End If
    '        Next

    '        transaction.Commit()

    '        errorMessage = "Sales bill saved successfully"
    '        returnBillNo = transactionNumber.ToString()
    '        Return True

    '    Catch ex As Exception
    '        If transaction IsNot Nothing Then
    '            transaction.Rollback()
    '        End If

    '        errorMessage = "Error saving sales bill: " & ex.Message & " | Stack Trace: " & ex.StackTrace
    '        returnBillNo = "0"
    '        Return False
    '    Finally
    '        If connection IsNot Nothing Then
    '            connection.Close()
    '        End If
    '    End Try
    'End Function

    ''' <summary>
    ''' Save sales bill with payment mode information
    ''' </summary>
    ''' <param name="salesHeader">Sales header data</param>
    ''' <param name="salesDetailsList">List of sales details</param>
    ''' <param name="paymentModes">Dictionary of payment modes (PaymodeID, Amount)</param>
    ''' <param name="errorMessage">Error message output</param>
    ''' <param name="returnBillNo">Generated bill number output</param>
    ''' <returns>True if successful, False if failed</returns>
    Public Function SaveSalesBill(salesHeader As SalesHeader, salesDetailsList As List(Of SalesDetails), paymentModes As Dictionary(Of Integer, Decimal), ByRef errorMessage As String, ByRef returnBillNo As String) As Boolean
        Dim connection As SqlConnection = Nothing
        Dim transaction As SqlTransaction = Nothing

        Try
            connection = New SqlConnection(_connectionString)
            connection.Open()
            transaction = connection.BeginTransaction()

            ' Save header and get transaction number
            Dim headerId As Integer = 0
            Dim transactionNumber As Integer = 0

            If Not SaveSalesHeader(salesHeader, connection, transaction, transactionNumber, headerId) Then
                transaction.Rollback()
                errorMessage = "Failed to save sales header. Transaction Number: " & transactionNumber.ToString() & ", Header ID: " & headerId.ToString()
                returnBillNo = "0"
                Return False
            End If

            ' Validate that we got valid IDs
            If transactionNumber <= 0 Then
                transaction.Rollback()
                errorMessage = "Invalid transaction number generated: " & transactionNumber.ToString()
                returnBillNo = "0"
                Return False
            End If

            If headerId <= 0 Then
                transaction.Rollback()
                errorMessage = "Invalid header ID generated: " & headerId.ToString()
                returnBillNo = "0"
                Return False
            End If

            ' Save all details
            For Each detail In salesDetailsList
                detail.psid_invoice_salid = headerId
                detail.psid_invoice_trno = transactionNumber.ToString()
                detail.psid_invoice_date = SafeToDateTime(salesHeader.psih_invoice_date, DateTime.Now)
                detail.psid_invoice_shiftno = salesHeader.psih_invoice_shiftno
                detail.psid_invoice_dayno = salesHeader.psih_invoice_dayno
                detail.psid_invoice_pmid = salesHeader.psih_invoice_pmid
                detail.psid_invoice_locid = salesHeader.psih_invoice_locid
                detail.psid_invoice_comid = salesHeader.psih_invoice_comid
                Dim detailId As Integer = 0
                If Not SaveSalesDetail(detail, connection, transaction, detailId) Then
                    transaction.Rollback()
                    errorMessage = "Failed to save sales detail"
                    returnBillNo = "0"
                    Return False
                End If
            Next

            ' Save payment modes if provided
            If paymentModes IsNot Nothing AndAlso paymentModes.Count > 0 Then
                If Not SavePaymentModesWithTransaction(headerId, paymentModes, SafeToInt32(salesHeader.psih_invoice_shiftno, 1), SafeToInt32(salesHeader.psih_invoice_dayno, 1), connection, transaction) Then
                    transaction.Rollback()
                    errorMessage = "Failed to save payment modes"
                    returnBillNo = "0"
                    Return False
                End If
            End If

            transaction.Commit()

            errorMessage = "Sales bill saved successfully"
            returnBillNo = transactionNumber.ToString()
            Return True

        Catch ex As Exception
            If transaction IsNot Nothing Then
                transaction.Rollback()
            End If

            errorMessage = "Error saving sales bill: " & ex.Message & " | Stack Trace: " & ex.StackTrace
            returnBillNo = "0"
            Return False
        Finally
            If connection IsNot Nothing Then
                connection.Close()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Save payment modes within a transaction
    ''' </summary>
    Private Function SavePaymentModesWithTransaction(salId As Integer, paymentModes As Dictionary(Of Integer, Decimal), shiftNo As Integer, dayNo As Integer, connection As SqlConnection, transaction As SqlTransaction) As Boolean
        Try
            ' Delete existing payment modes for this sale first
            Using deleteCommand As New SqlCommand("DELETE FROM [dbo].[Sal_PayMode] WHERE [Sal_ID] = @Sal_ID", connection, transaction)
                deleteCommand.Parameters.AddWithValue("@Sal_ID", salId)
                deleteCommand.ExecuteNonQuery()
            End Using

            ' Insert new payment modes
            For Each paymentMode In paymentModes
                Using command As New SqlCommand("SP_SavePaymentMode", connection, transaction)
                    command.CommandType = CommandType.StoredProcedure

                    command.Parameters.AddWithValue("@Sal_ID", salId)
                    command.Parameters.AddWithValue("@Paymode", paymentMode.Key)
                    command.Parameters.AddWithValue("@Amount", paymentMode.Value)
                    command.Parameters.AddWithValue("@ShiftNo", shiftNo)
                    command.Parameters.AddWithValue("@Dayno", dayNo)
                    command.Parameters.AddWithValue("@Webhost", 0)
                    command.Parameters.AddWithValue("@ComId", _companyInfo.ComId)
                    command.Parameters.AddWithValue("@LocId", _companyInfo.LocId)
                    command.ExecuteNonQuery()
                End Using
            Next

            Return True
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("SavePaymentModesWithTransaction Error: " & ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Update existing sales bill (for edit mode)
    ''' </summary>
    Public Function UpdateSalesBill(salesHeader As SalesHeader, salesDetailsList As List(Of SalesDetails), ByRef errorMessage As String, ByRef returnBillNo As String) As Boolean
        Dim connection As SqlConnection = Nothing
        Dim transaction As SqlTransaction = Nothing

        Try
            connection = New SqlConnection(_connectionString)
            connection.Open()
            transaction = connection.BeginTransaction()

            ' Update header
            If Not UpdateSalesHeader(salesHeader, connection, transaction) Then
                transaction.Rollback()
                errorMessage = "Failed to update sales header"
                returnBillNo = "0"
                Return False
            End If

            ' Delete existing details
            If Not DeleteSalesDetails(SafeToInt32(salesHeader.psih_invoice_trno, 0), connection, transaction) Then
                transaction.Rollback()
                errorMessage = "Failed to delete existing details"
                returnBillNo = "0"
                Return False
            End If

            ' Insert updated details
            For Each detail In salesDetailsList
                detail.psid_invoice_salid = SafeToInt32(salesHeader.psih_invoice_id, 0)
                detail.psid_invoice_trno = salesHeader.psih_invoice_trno
                detail.psid_invoice_date = SafeToDateTime(salesHeader.psih_invoice_date, DateTime.Now)
                detail.psid_invoice_shiftno = salesHeader.psih_invoice_shiftno
                detail.psid_invoice_dayno = salesHeader.psih_invoice_dayno
                detail.psid_invoice_pmid = salesHeader.psih_invoice_pmid
                detail.psid_invoice_locid = salesHeader.psih_invoice_locid
                detail.psid_invoice_comid = salesHeader.psih_invoice_comid
                Dim detailId As Integer = 0
                If Not SaveSalesDetail(detail, connection, transaction, detailId) Then
                    transaction.Rollback()
                    errorMessage = "Failed to save updated detail"
                    returnBillNo = "0"
                    Return False
                End If
            Next

            transaction.Commit()

            errorMessage = "Sales bill updated successfully"
            returnBillNo = salesHeader.psih_invoice_trno
            Return True

        Catch ex As Exception
            If transaction IsNot Nothing Then
                transaction.Rollback()
            End If

            errorMessage = "Error updating sales bill: " & ex.Message
            returnBillNo = "0"
            Return False
        Finally
            If connection IsNot Nothing Then
                connection.Close()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Update existing sales bill with payment mode information (for edit mode)
    ''' </summary>
    ''' <param name="salesHeader">Sales header data</param>
    ''' <param name="salesDetailsList">List of sales details</param>
    ''' <param name="paymentModes">Dictionary of payment modes (PaymodeID, Amount)</param>
    ''' <param name="errorMessage">Error message output</param>
    ''' <param name="returnBillNo">Generated bill number output</param>
    ''' <returns>True if successful, False if failed</returns>
    Public Function UpdateSalesBill(salesHeader As SalesHeader, salesDetailsList As List(Of SalesDetails), paymentModes As Dictionary(Of Integer, Decimal), ByRef errorMessage As String, ByRef returnBillNo As String) As Boolean
        Dim connection As SqlConnection = Nothing
        Dim transaction As SqlTransaction = Nothing

        Try
            connection = New SqlConnection(_connectionString)
            connection.Open()
            transaction = connection.BeginTransaction()

            ' Update header
            If Not UpdateSalesHeader(salesHeader, connection, transaction) Then
                transaction.Rollback()
                errorMessage = "Failed to update sales header"
                returnBillNo = "0"
                Return False
            End If

            ' Delete existing details
            If Not DeleteSalesDetails(SafeToInt32(salesHeader.psih_invoice_trno, 0), connection, transaction) Then
                transaction.Rollback()
                errorMessage = "Failed to delete existing details"
                returnBillNo = "0"
                Return False
            End If

            ' Insert updated details
            For Each detail In salesDetailsList
                detail.psid_invoice_salid = SafeToInt32(salesHeader.psih_invoice_id, 0)
                detail.psid_invoice_trno = salesHeader.psih_invoice_trno
                detail.psid_invoice_date = SafeToDateTime(salesHeader.psih_invoice_date, DateTime.Now)
                detail.psid_invoice_shiftno = salesHeader.psih_invoice_shiftno
                detail.psid_invoice_dayno = salesHeader.psih_invoice_dayno
                detail.psid_invoice_pmid = salesHeader.psih_invoice_pmid
                detail.psid_invoice_locid = salesHeader.psih_invoice_locid
                detail.psid_invoice_comid = salesHeader.psih_invoice_comid
                Dim detailId As Integer = 0
                If Not SaveSalesDetail(detail, connection, transaction, detailId) Then
                    transaction.Rollback()
                    errorMessage = "Failed to save updated detail"
                    returnBillNo = "0"
                    Return False
                End If
            Next

            ' Save payment modes if provided
            If paymentModes IsNot Nothing AndAlso paymentModes.Count > 0 Then
                If Not SavePaymentModesWithTransaction(SafeToInt32(salesHeader.psih_invoice_trno, 0), paymentModes, SafeToInt32(salesHeader.psih_invoice_shiftno, 1), SafeToInt32(salesHeader.psih_invoice_dayno, 1), connection, transaction) Then
                    transaction.Rollback()
                    errorMessage = "Failed to save payment modes"
                    returnBillNo = "0"
                    Return False
                End If
            End If

            transaction.Commit()

            errorMessage = "Sales bill updated successfully with payment modes"
            returnBillNo = salesHeader.psih_invoice_trno
            Return True

        Catch ex As Exception
            If transaction IsNot Nothing Then
                transaction.Rollback()
            End If

            errorMessage = "Error updating sales bill: " & ex.Message
            returnBillNo = "0"
            Return False
        Finally
            If connection IsNot Nothing Then
                connection.Close()
            End If
        End Try
    End Function

    Private Function SaveSalesHeader(salesHeader As SalesHeader, connection As SqlConnection, transaction As SqlTransaction, ByRef transactionNumber As Integer, ByRef headerId As Integer) As Boolean
        Try
            Using command As New SqlCommand("SP_SaveSalesHeader", connection, transaction)
                command.CommandType = CommandType.StoredProcedure

                ' Add parameters
                command.Parameters.AddWithValue("@psih_invoice_pmid", SafeToInt32(salesHeader.psih_invoice_pmid, 1))
                command.Parameters.AddWithValue("@psih_invoice_date", SafeToDateTime(salesHeader.psih_invoice_date, DateTime.Now))
                command.Parameters.AddWithValue("@psih_invoice_prefix", If(String.IsNullOrEmpty(salesHeader.psih_invoice_prefix), "INV", salesHeader.psih_invoice_prefix))
                command.Parameters.AddWithValue("@psih_invoice_tqty", salesHeader.psih_invoice_tqty)
                command.Parameters.AddWithValue("@psih_invoice_tamount", salesHeader.psih_invoice_tamount)
                command.Parameters.AddWithValue("@psih_invoice_titemdisper", salesHeader.psih_invoice_titemdisper)
                command.Parameters.AddWithValue("@psih_invoice_titemdisamt", Convert.ToDecimal(If(String.IsNullOrEmpty(salesHeader.psih_invoice_titemdisamt), "0", salesHeader.psih_invoice_titemdisamt)))
                command.Parameters.AddWithValue("@psih_invoice_tbilldiscper", salesHeader.psih_invoice_tbilldiscper)
                command.Parameters.AddWithValue("@psih_invoice_tbilldiscamt", salesHeader.psih_invoice_tbilldiscamt)
                command.Parameters.AddWithValue("@psih_invoice_totdiscper", salesHeader.psih_invoice_totdiscper)
                command.Parameters.AddWithValue("@psih_invoice_totdiscamt", salesHeader.psih_invoice_totdiscamt)
                command.Parameters.AddWithValue("@psih_invoice_tgrossamt", SafeToDecimal(salesHeader.psih_invoice_tgrossamt, 0))
                command.Parameters.AddWithValue("@psih_invoice_ttaxamt", salesHeader.psih_invoice_ttaxamt)
                command.Parameters.AddWithValue("@psih_invoice_sercharge", salesHeader.psih_invoice_sercharge)
                command.Parameters.AddWithValue("@psih_invoice_roundoff", salesHeader.psih_invoice_roundoff)
                command.Parameters.AddWithValue("@psih_invoice_tnetamt", salesHeader.psih_invoice_tnetamt)
                command.Parameters.AddWithValue("@psih_invoice_saletype", If(String.IsNullOrEmpty(salesHeader.psih_invoice_saletype), "Invoice", salesHeader.psih_invoice_saletype))
                command.Parameters.AddWithValue("@psih_invoice_billtype", If(String.IsNullOrEmpty(salesHeader.psih_invoice_billtype), "Cash Bill", salesHeader.psih_invoice_billtype))
                command.Parameters.AddWithValue("@psih_invoice_billstatus", If(String.IsNullOrEmpty(salesHeader.psih_invoice_billstatus), "Closed", salesHeader.psih_invoice_billstatus))
                command.Parameters.AddWithValue("@psih_invoice_paymode", If(String.IsNullOrEmpty(salesHeader.psih_invoice_paymode), "cash", salesHeader.psih_invoice_paymode))
                command.Parameters.AddWithValue("@psih_invoice_customerid", If(String.IsNullOrEmpty(salesHeader.psih_invoice_customerid), "1", salesHeader.psih_invoice_customerid))
                command.Parameters.AddWithValue("@psih_invoice_description", If(String.IsNullOrEmpty(salesHeader.psih_invoice_description), "", salesHeader.psih_invoice_description))
                command.Parameters.AddWithValue("@psih_invoice_countername", If(String.IsNullOrEmpty(salesHeader.psih_invoice_countername), "", salesHeader.psih_invoice_countername))
                command.Parameters.AddWithValue("@psih_invoice_userid", SafeToInt32(salesHeader.psih_invoice_userid, 1))
                command.Parameters.AddWithValue("@psih_invoice_comid", SafeToInt32(salesHeader.psih_invoice_comid, 1))
                command.Parameters.AddWithValue("@psih_invoice_locid", SafeToInt32(salesHeader.psih_invoice_locid, 1))
                command.Parameters.AddWithValue("@psih_invoice_print", 0)
                command.Parameters.AddWithValue("@psih_invoice_billremarks", If(String.IsNullOrEmpty(salesHeader.psih_invoice_billremarks), "", salesHeader.psih_invoice_billremarks))
                command.Parameters.AddWithValue("@psih_invoice_advamt", salesHeader.psih_invoice_advamt)
                command.Parameters.AddWithValue("@psih_invoice_outstanding", salesHeader.psih_invoice_outstanding)
                command.Parameters.AddWithValue("@psih_invoice_givenamt", salesHeader.psih_invoice_givenamt)
                command.Parameters.AddWithValue("@psih_invoice_balamt", salesHeader.psih_invoice_balamt)
                command.Parameters.AddWithValue("@psih_invoice_shiftno", SafeToInt32(salesHeader.psih_invoice_shiftno, 1))
                command.Parameters.AddWithValue("@psih_invoice_dayno", SafeToInt32(salesHeader.psih_invoice_dayno, 1))
                command.Parameters.AddWithValue("@psih_invoice_webhost", 0)
                command.Parameters.AddWithValue("@psih_invoice_token", SafeToInt32(salesHeader.psih_invoice_token, 0))

                ' Input/Output parameter for transaction number
                Dim prmTransNo As New SqlParameter("@psih_invoice_trno", SqlDbType.Int)
                prmTransNo.Direction = ParameterDirection.InputOutput
                prmTransNo.Value = 0  ' Initialize to 0 to trigger generation
                command.Parameters.Add(prmTransNo)

                ' Output parameter for header ID
                Dim prmHeaderId As New SqlParameter("@psih_invoice_id", SqlDbType.Int)
                prmHeaderId.Direction = ParameterDirection.Output
                command.Parameters.Add(prmHeaderId)

                command.ExecuteNonQuery()

                ' Safe conversion from output parameters with validation
                transactionNumber = If(prmTransNo.Value Is DBNull.Value OrElse prmTransNo.Value Is Nothing, 0, Convert.ToInt32(prmTransNo.Value))
                headerId = If(prmHeaderId.Value Is DBNull.Value OrElse prmHeaderId.Value Is Nothing, 0, Convert.ToInt32(prmHeaderId.Value))

                ' Ensure we got valid values
                If transactionNumber <= 0 Then
                    Throw New Exception("Failed to generate transaction number")
                End If

                If headerId <= 0 Then
                    Throw New Exception("Failed to get header ID")
                End If

                Return True
            End Using
        Catch ex As Exception
            ' Log the specific error for debugging
            MsgBox("SaveSalesHeader Error: " & ex.Message)
            ' Reset output parameters on error
            transactionNumber = 0
            headerId = 0
            Return False
        End Try
    End Function

    Private Function SaveSalesDetail(salesDetail As SalesDetails, connection As SqlConnection, transaction As SqlTransaction, ByRef detailId As Integer) As Boolean
        Try
            Using command As New SqlCommand("SP_SaveSalesDetail", connection, transaction)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("@psid_invoice_salid", SafeToInt32(salesDetail.psid_invoice_salid, 0))
                command.Parameters.AddWithValue("@psid_invoice_sno", SafeToInt32(salesDetail.psid_invoice_sno, 1))
                command.Parameters.AddWithValue("@psid_invoice_date", salesDetail.psid_invoice_date)
                command.Parameters.AddWithValue("@psid_invoice_trno", SafeToInt32(salesDetail.psid_invoice_trno, 0))
                command.Parameters.AddWithValue("@psid_invoice_barcode", If(String.IsNullOrEmpty(salesDetail.psid_invoice_barcode), "", salesDetail.psid_invoice_barcode))
                command.Parameters.AddWithValue("@psid_invoice_procode", SafeToInt32(salesDetail.psid_invoice_procode, 0))
                command.Parameters.AddWithValue("@psid_invoice_description", If(String.IsNullOrEmpty(salesDetail.psid_invoice_description), "", salesDetail.psid_invoice_description))
                command.Parameters.AddWithValue("@psid_invoice_serialno", If(String.IsNullOrEmpty(salesDetail.psid_invoice_serialno), "", salesDetail.psid_invoice_serialno))
                command.Parameters.AddWithValue("@psid_invoice_uom", If(String.IsNullOrEmpty(salesDetail.psid_invoice_uom), "", salesDetail.psid_invoice_uom))
                command.Parameters.AddWithValue("@psid_invoice_proqty", salesDetail.psid_invoice_proqty)
                command.Parameters.AddWithValue("@psid_invoice_rate", salesDetail.psid_invoice_rate)
                command.Parameters.AddWithValue("@psid_invoice_amt", salesDetail.psid_invoice_amt)
                command.Parameters.AddWithValue("@psid_invoice_itemdisp", salesDetail.psid_invoice_itemdisp)
                command.Parameters.AddWithValue("@psid_invoice_itemdisamt", salesDetail.psid_invoice_itemdisamt)
                command.Parameters.AddWithValue("@psid_invoice_billdisp", salesDetail.psid_invoice_billdisp)
                command.Parameters.AddWithValue("@psid_invoice_billdisamt", salesDetail.psid_invoice_billdisamt)
                command.Parameters.AddWithValue("@psid_invoice_totdper", salesDetail.psid_invoice_totdper)
                command.Parameters.AddWithValue("@psid_invoice_totdamt", salesDetail.psid_invoice_totdamt)
                command.Parameters.AddWithValue("@psid_invoice_gross", salesDetail.psid_invoice_gross)
                command.Parameters.AddWithValue("@psid_invoice_taxinex", SafeToInt32(salesDetail.psid_invoice_taxinex, 0))
                command.Parameters.AddWithValue("@psid_invoice_taxvalue", salesDetail.psid_invoice_taxvalue)
                command.Parameters.AddWithValue("@psid_invoice_taxamt", salesDetail.psid_invoice_taxamt)
                command.Parameters.AddWithValue("@psid_invoice_netamt", salesDetail.psid_invoice_netamt)
                command.Parameters.AddWithValue("@psid_invoice_remarks", If(String.IsNullOrEmpty(salesDetail.psid_invoice_remarks), "", salesDetail.psid_invoice_remarks))
                command.Parameters.AddWithValue("@psid_invoice_batchno", If(String.IsNullOrEmpty(salesDetail.psid_invoice_batchno), "", salesDetail.psid_invoice_batchno))
                command.Parameters.AddWithValue("@psid_invoice_salesmanid", SafeToInt32(salesDetail.psid_invoice_salesmanid, 0))
                command.Parameters.AddWithValue("@psid_invoice_salemanper", salesDetail.psid_invoice_salemanper)
                command.Parameters.AddWithValue("@psid_invoice_shiftno", SafeToInt32(salesDetail.psid_invoice_shiftno, 1))
                command.Parameters.AddWithValue("@psid_invoice_dayno", SafeToInt32(salesDetail.psid_invoice_dayno, 1))
                command.Parameters.AddWithValue("@psid_invoice_webhost", 0)
                command.Parameters.AddWithValue("@psid_invoice_pmid", SafeToInt32(salesDetail.psid_invoice_pmid, 0))
                command.Parameters.AddWithValue("@psid_invoice_comid", SafeToInt32(salesDetail.psid_invoice_comid, 0))
                command.Parameters.AddWithValue("@psid_invoice_locid", SafeToInt32(salesDetail.psid_invoice_locid, 0))
                Dim prmDetailId As New SqlParameter("@psid_invoice_id", SqlDbType.Int)
                prmDetailId.Direction = ParameterDirection.Output
                command.Parameters.Add(prmDetailId)

                command.ExecuteNonQuery()

                ' Safe conversion from output parameter
                detailId = If(prmDetailId.Value Is DBNull.Value, 0, Convert.ToInt32(prmDetailId.Value))

                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function UpdateSalesHeader(salesHeader As SalesHeader, connection As SqlConnection, transaction As SqlTransaction) As Boolean
        Try
            Using command As New SqlCommand("SP_UpdateSalesHeader", connection, transaction)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("@psih_invoice_id", SafeToInt32(salesHeader.psih_invoice_id, 0))
                command.Parameters.AddWithValue("@psih_invoice_tqty", salesHeader.psih_invoice_tqty)
                command.Parameters.AddWithValue("@psih_invoice_tamount", salesHeader.psih_invoice_tamount)
                command.Parameters.AddWithValue("@psih_invoice_titemdisper", salesHeader.psih_invoice_titemdisper)
                command.Parameters.AddWithValue("@psih_invoice_titemdisamt", SafeToDecimal(salesHeader.psih_invoice_titemdisamt, 0))
                command.Parameters.AddWithValue("@psih_invoice_tbilldiscper", salesHeader.psih_invoice_tbilldiscper)
                command.Parameters.AddWithValue("@psih_invoice_tbilldiscamt", salesHeader.psih_invoice_tbilldiscamt)
                command.Parameters.AddWithValue("@psih_invoice_totdiscper", salesHeader.psih_invoice_totdiscper)
                command.Parameters.AddWithValue("@psih_invoice_totdiscamt", salesHeader.psih_invoice_totdiscamt)
                command.Parameters.AddWithValue("@psih_invoice_tgrossamt", SafeToDecimal(salesHeader.psih_invoice_tgrossamt, 0))
                command.Parameters.AddWithValue("@psih_invoice_ttaxamt", salesHeader.psih_invoice_ttaxamt)
                command.Parameters.AddWithValue("@psih_invoice_sercharge", salesHeader.psih_invoice_sercharge)
                command.Parameters.AddWithValue("@psih_invoice_roundoff", salesHeader.psih_invoice_roundoff)
                command.Parameters.AddWithValue("@psih_invoice_tnetamt", salesHeader.psih_invoice_tnetamt)
                command.Parameters.AddWithValue("@psih_invoice_billtype", If(String.IsNullOrEmpty(salesHeader.psih_invoice_billtype), "Cash Bill", salesHeader.psih_invoice_billtype))
                command.Parameters.AddWithValue("@psih_invoice_billstatus", If(String.IsNullOrEmpty(salesHeader.psih_invoice_billstatus), "Closed", salesHeader.psih_invoice_billstatus))
                command.Parameters.AddWithValue("@psih_invoice_paymode", If(String.IsNullOrEmpty(salesHeader.psih_invoice_paymode), "cash", salesHeader.psih_invoice_paymode))
                command.Parameters.AddWithValue("@psih_invoice_customerid", If(String.IsNullOrEmpty(salesHeader.psih_invoice_customerid), "1", salesHeader.psih_invoice_customerid))
                command.Parameters.AddWithValue("@psih_invoice_description", If(String.IsNullOrEmpty(salesHeader.psih_invoice_description), "", salesHeader.psih_invoice_description))
                command.Parameters.AddWithValue("@psih_invoice_billremarks", If(String.IsNullOrEmpty(salesHeader.psih_invoice_billremarks), "", salesHeader.psih_invoice_billremarks))
                command.Parameters.AddWithValue("@psih_invoice_advamt", salesHeader.psih_invoice_advamt)
                command.Parameters.AddWithValue("@psih_invoice_outstanding", salesHeader.psih_invoice_outstanding)
                command.Parameters.AddWithValue("@psih_invoice_givenamt", salesHeader.psih_invoice_givenamt)
                command.Parameters.AddWithValue("@psih_invoice_balamt", salesHeader.psih_invoice_balamt)
                command.Parameters.AddWithValue("@psih_invoice_webhost", salesHeader.psih_invoice_webhost)
                command.ExecuteNonQuery()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function DeleteSalesDetails(transactionNumber As Integer, connection As SqlConnection, transaction As SqlTransaction) As Boolean
        Try
            Using command As New SqlCommand("DELETE FROM pos_sale_invoicedtl WHERE psid_invoice_trno = @psid_invoice_trno", connection, transaction)
                command.Parameters.AddWithValue("@psid_invoice_trno", transactionNumber)
                command.ExecuteNonQuery()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function


    ''' <summary>
    ''' Convert DataTable to List of SalesDetails
    ''' </summary>
    Public Function ConvertDataTableToSalesDetails(dataTable As DataTable) As List(Of SalesDetails)
        Dim salesDetailsList As New List(Of SalesDetails)

        For Each row As DataRow In dataTable.Rows
            salesDetailsList.Add(New SalesDetails(row))
        Next

        Return salesDetailsList
    End Function
End Class

Module PrintViewReport
#Region "Print Or View Bill"
    ''' <summary>
    ''' Safe string conversion helper for the module
    ''' </summary>
    Private Function SafeToString(value As Object, defaultValue As String) As String
        If value Is Nothing OrElse value Is DBNull.Value Then
            Return defaultValue
        End If
        Return value.ToString().Trim()
    End Function

    ''' <summary>
    ''' Get sales data by bill number for printing/viewing
    ''' </summary>
    ''' <param name="BillNo">Bill number to retrieve</param>
    ''' <param name="receDs">DataSet to populate with bill data</param>
    ''' <returns>True if bill data found and populated, False otherwise</returns>
    Public Function GetSalesByBillLocal(BillNo As String, ByRef receDs As DataSet, ByRef Mode As String) As Boolean
        Try
            ' Validate input parameters
            If String.IsNullOrEmpty(BillNo) Then
                Return False
            End If

            ' Prepare parameters for stored procedure
            Dim SqlViewPrint(2) As SqlParameter
            SqlViewPrint(0) = New SqlParameter("@mode", Mode)
            SqlViewPrint(1) = New SqlParameter("@trno", BillNo)
            SqlViewPrint(2) = New SqlParameter("@date", Date.Now)
            ' Initialize dataset
            receDs = New DataSet

            ' Execute stored procedure to get bill data
            receDs = _sqlDataAdapter2("sp_printtaxinvoice", SqlViewPrint)

            ' Check if dataset has data
            If receDs IsNot Nothing AndAlso receDs.Tables.Count > 0 AndAlso receDs.Tables(0).Rows.Count > 0 Then

                ' Add username based on user ID matching customer ID
                If _JsonData.UserTable.Rows.Count > 0 Then
                    ' Add username column if it doesn't exist
                    If Not receDs.Tables(0).Columns.Contains("UserName") Then
                        receDs.Tables(0).Columns.Add("UserName", GetType(String))
                    End If

                    ' Update each row with matching username
                    For Each billRow As DataRow In receDs.Tables(0).Rows
                        Dim UserId As String = SafeToString(billRow("psih_invoice_userid"), "")
                        If Not String.IsNullOrEmpty(UserId) Then
                            For Each userRow As DataRow In _JsonData.UserTable.Rows
                                If SafeToString(userRow("Id"), "") = UserId Then
                                    billRow("UserName") = SafeToString(userRow("UserName"), "")
                                    Exit For
                                End If
                            Next
                        End If
                    Next
                End If

                ' Add customer name based on customer ID
                If _JsonData.CustomerTable.Rows.Count > 0 Then
                    ' Add customername column if it doesn't exist
                    If Not receDs.Tables(0).Columns.Contains("Customer") Then
                        receDs.Tables(0).Columns.Add("Customer", GetType(String))
                    End If

                    ' Update each row with matching customer name
                    For Each billRow As DataRow In receDs.Tables(0).Rows
                        Dim customerId As String = SafeToString(billRow("psih_invoice_customerid"), "")
                        If Not String.IsNullOrEmpty(customerId) Then
                            For Each customerRow As DataRow In _JsonData.CustomerTable.Rows
                                If SafeToString(customerRow("CustomerId"), "") = customerId Then
                                    billRow("Customer") = SafeToString(customerRow("CustomerName"), "")
                                    Exit For
                                End If
                            Next
                        End If
                    Next
                End If

                ' Add salesman name based on salesman ID
                If _JsonData.SalesManCommissionTable.Rows.Count > 0 Then
                    ' Add empname column if it doesn't exist
                    If Not receDs.Tables(1).Columns.Contains("SalesmanName") Then
                        receDs.Tables(1).Columns.Add("SalesmanName", GetType(String))
                    End If

                    ' Check if we have detail records or need to get from detail table
                    If receDs.Tables.Count > 1 AndAlso receDs.Tables(1).Rows.Count > 0 Then
                        ' Update detail rows with matching salesman name
                        For Each detailRow As DataRow In receDs.Tables(1).Rows
                            Dim salesmanId As String = SafeToString(detailRow("psid_invoice_salesmanid"), "")
                            If Not String.IsNullOrEmpty(salesmanId) AndAlso salesmanId <> "0" Then
                                For Each salesmanRow As DataRow In _JsonData.SalesManDataTable.Rows
                                    If SafeToString(salesmanRow("Id"), "") = salesmanId Then
                                        detailRow("SalesmanName") = SafeToString(salesmanRow("SalesMan"), "")
                                        Exit For
                                    End If
                                Next
                            End If
                        Next
                    Else
                        ' If no detail table, try to match with header data
                        For Each billRow As DataRow In receDs.Tables(1).Rows
                            ' Assuming there might be a salesman ID in header, otherwise leave empty
                            billRow("SalesmanName") = ""
                        Next
                    End If
                End If
                If _JsonData.PaymodeList.Rows.Count > 0 Then
                    ' Add empname column if it doesn't exist
                    If Not receDs.Tables(2).Columns.Contains("PaymodeName") Then
                        receDs.Tables(2).Columns.Add("PaymodeName", GetType(String))
                    End If
                    ' Check if we have detail records or need to get from detail table
                    If receDs.Tables.Count > 1 AndAlso receDs.Tables(2).Rows.Count > 0 Then
                        ' Update detail rows with matching salesman name
                        For Each detailRow As DataRow In receDs.Tables(2).Rows
                            Dim PmodeId As String = SafeToString(detailRow("Paymode"), "")
                            If Not String.IsNullOrEmpty(PmodeId) AndAlso PmodeId <> "0" Then
                                For Each paymodeRow As DataRow In _JsonData.PaymodeList.Rows
                                    If SafeToString(paymodeRow("pmode_id"), "") = PmodeId Then
                                        detailRow("PaymodeName") = SafeToString(paymodeRow("pmode_name"), "")
                                        Exit For
                                    End If
                                Next
                            End If
                        Next
                    Else
                        ' If no detail table, try to match with header data
                        For Each billRow As DataRow In receDs.Tables(2).Rows
                            ' Assuming there might be a salesman ID in header, otherwise leave empty
                            billRow("PaymodeName") = ""
                        Next
                    End If
                End If

                Return True
            Else
                ' No data found for the bill number
                Return False
            End If

        Catch ex As Exception
            ' Log error for debugging
            System.Diagnostics.Debug.WriteLine("GetSalesByBill Error: " & ex.Message)

            ' Initialize empty dataset on error
            receDs = New DataSet
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Get all sales data for a specific date
    ''' </summary>
    ''' <param name="getdate">Date to retrieve sales for</param>
    ''' <param name="receDs">DataSet to populate with sales data</param>
    ''' <returns>True if sales data found and populated, False otherwise</returns>
    Public Function GetAllSales(ByVal getdate As String, ByRef receDs As DataSet) As Boolean
        Try
            ' Validate input parameters
            If String.IsNullOrEmpty(getdate) Then
                Return False
            End If

            ' Prepare parameters for stored procedure
            Dim SqlViewPrint(2) As SqlParameter
            SqlViewPrint(0) = New SqlParameter("@mode", "D")
            SqlViewPrint(1) = New SqlParameter("@trno", "0")
            SqlViewPrint(2) = New SqlParameter("@date", getdate)

            ' Initialize dataset
            receDs = New DataSet

            ' Execute stored procedure to get sales data
            receDs = _sqlDataAdapter2("sp_printtaxinvoice", SqlViewPrint)

            ' Check if dataset has data
            If receDs IsNot Nothing AndAlso receDs.Tables.Count > 0 AndAlso receDs.Tables(0).Rows.Count > 0 Then
                ' Add customer name based on customer ID
                If _JsonData.CustomerTable.Rows.Count > 0 Then
                    ' Add customername column if it doesn't exist
                    If Not receDs.Tables(0).Columns.Contains("Customer") Then
                        receDs.Tables(0).Columns.Add("Customer", GetType(String))
                    End If

                    ' Update each row with matching customer name
                    For Each billRow As DataRow In receDs.Tables(0).Rows
                        Dim customerId As String = SafeToString(billRow("psih_invoice_customerid"), "")
                        If Not String.IsNullOrEmpty(customerId) Then
                            For Each customerRow As DataRow In _JsonData.CustomerTable.Rows
                                If SafeToString(customerRow("CustomerId"), "") = customerId Then
                                    billRow("Customer") = SafeToString(customerRow("CustomerName"), "")
                                    Exit For
                                End If
                            Next
                        End If
                    Next
                End If
                Return True
            Else
                ' No data found for the date
                Return False
            End If

        Catch ex As Exception
            ' Log error for debugging
            System.Diagnostics.Debug.WriteLine("GetAllSales Error: " & ex.Message)

            ' Initialize empty dataset on error
            receDs = New DataSet
            Return False
        End Try
    End Function
    Public Function BillCancel(ByRef trno As Integer) As Boolean
        Try
            Dim _Sql(1) As SqlParameter
            _Sql(0) = New SqlParameter("@mode", "C")
            _Sql(1) = New SqlParameter("@trno", trno)
            If _ExecuteNonQuery("sp_billcancel", _Sql, "er") = True Then
                Return True
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CheckSalesBeforeCounterClose() As Boolean
        Try
            Dim dds As New DataSet
            Dim _sqlShitclose(4) As SqlParameter
            _sqlShitclose(0) = New SqlParameter("@mode", "s")
            _sqlShitclose(1) = New SqlParameter("@psc_opbalance", "0")
            _sqlShitclose(2) = New SqlParameter("@psc_pcname", RegistrationDetails._localPcname)
            _sqlShitclose(3) = New SqlParameter("@psc_machineid", RegistrationDetails._machineId)
            _sqlShitclose(4) = New SqlParameter("@psc_machinename", RegistrationDetails._localPcname)
            dds = _sqlDataAdapter2("sp_createshiftclose", _sqlShitclose)
            If dds.Tables(0).Rows.Count > 0 Then
                Dim countBill = dds.Tables(0).Rows(0)(1).ToString
                If String.IsNullOrEmpty(countBill) OrElse countBill = "0.00" Then
                    properClass.R_Msgstring = "No Sales Found"
                    Dim frmmsgOk As New frmMsgBoxOk
                    frmmsgOk.ShowDialog()
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region
End Module
