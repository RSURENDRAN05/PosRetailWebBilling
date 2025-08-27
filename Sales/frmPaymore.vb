Imports DevExpress.XtraEditors

Public Class frmPaymore
    Private Billamt As Decimal = 0.0
    Private _AccName As String = String.Empty
    Private _Caption As String = "Paymode"
    Private cust As String = "CU000"
    Private _txt As String = "PayMode"
    Public PaymentDetailTable As DataTable
    Dim numstr As String = ""
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Overloads Sub ShowDialog(ByVal _BillAmt As Decimal, ByVal custom As String)
        Try
            If _BillAmt > 0 Then
                Billamt = _BillAmt
            End If

            cust = custom

            ' Set default payment mode to cash
            _PaymentDtl.paymentMode = "cash"
            _PaymentDtl.paymentModeSelection = "Cash"
            _PaymentDtl.paymentId = 1 ' Assuming Cash payment mode ID is 1

            If _PaymentDtl.paymentMode = "credit" Then
                txtadvanceamt.Enabled = True
                txtclientname.Enabled = True
            Else
                txtadvanceamt.Enabled = False
                txtclientname.Enabled = False
            End If

            lblpaymode.Text = _PaymentDtl.paymentModeSelection

            If custom <> "" OrElse custom IsNot Nothing Then
                txtclientname.EditValue = custom
            End If

            txtPopBillamt.Text = Billamt.ToString("###0.00")

            txtpopbalamt.EditValue = "0.00" ' Initialize balance as 0 since amount equals bill amount
            MyBase.ShowDialog()
            'store param for later user
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmPaymore_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            txtpopbalamt.EditValue = 0.0
            txttpopenamt.EditValue = 0.0
            txtadvanceamt.EditValue = 0.0
            txtclientname.Enabled = False
            txtadvanceamt.Enabled = False
            txtentermultipleamount.EditValue = 0
            If _JsonData.PaymodeList.Rows.Count > 0 Then
                GridControl3.DataSource = _JsonData.PaymodeList.DefaultView
            End If
            PaymentDetailTable = CreateTablePaymore()
            GridControlPaymore.DataSource = PaymentDetailTable
            txttpopenamt.EditValue = Billamt.ToString("###0.00") ' Auto-fill with bill amount for default cash payment
            lblpaymode.Text = "Cash"
            numstr = ""
            boolMultiplePayment = False
            Label2.Text = "MultiplePayment-No"
            txtentermultipleamount.Enabled = False
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "Form Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnpopHome_Click(sender As Object, e As EventArgs) Handles btnpopHome.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnpopCash_Click(sender As Object, e As EventArgs) Handles btnpopCash.Click
        Try
            ' Initialize values if null
            If txtadvanceamt.EditValue Is Nothing Then
                txtadvanceamt.EditValue = 0
            End If
            If txtpopbalamt.EditValue Is Nothing OrElse String.IsNullOrEmpty(txtpopbalamt.EditValue) Then
                txtpopbalamt.EditValue = 0
            End If

            ' Check if balance amount is negative and return if so
            If ConvertDecimal(txtpopbalamt.EditValue) < 0 Then
                txttpopenamt.EditValue = 0
                Return
            End If
            If txttpopenamt.EditValue Is Nothing OrElse String.IsNullOrEmpty(txttpopenamt.EditValue) Then
                txttpopenamt.EditValue = 0
            End If

            ' Check if PaymentDetailTable has any payments
            If PaymentDetailTable.Rows.Count = 0 Then
                ' No payments in table, check if user has entered an amount for default cash payment
                Dim enteredAmount As Decimal = ConvertDecimal(txtPopBillamt.EditValue)

                If enteredAmount > 0 Then
                    ' Add default cash payment to the table
                    Dim cashPaymentId As Integer = 1 ' Assuming Cash payment mode ID is 1
                    Dim cashPaymentName As String = "Cash"
                    Dim cashPaymentMode As String = "cash"

                    ' Add cash payment to PaymentDetailTable
                    PaymentDetailTable.BeginInit()
                    PaymentDetailTable.Rows.Add(cashPaymentId, cashPaymentName, cashPaymentMode, enteredAmount)
                    PaymentDetailTable.EndInit()
                    PaymentDetailTable.AcceptChanges()
 
                ElseIf Billamt > 0 Then
                    ' Auto-fill with full bill amount for cash payment
                    Dim cashPaymentId As Integer = 1 ' Assuming Cash payment mode ID is 1
                    Dim cashPaymentName As String = "Cash"
                    Dim cashPaymentMode As String = "cash"

                    ' Add full bill amount as cash payment
                    PaymentDetailTable.BeginInit()
                    PaymentDetailTable.Rows.Add(cashPaymentId, cashPaymentName, cashPaymentMode, Billamt)
                    PaymentDetailTable.EndInit()
                    PaymentDetailTable.AcceptChanges()

                   
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show("Please enter a payment amount or select payment modes", "No Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txttpopenamt.Focus()
                    Return
                End If
            End If

            ' Validate multiple payment total if being used
            If ConvertDecimal(txtmultipletotal.EditValue) > 0 Then
                If Not ValidateMultiplePaymentTotal() Then
                    Return
                End If
            End If

            ' Validate that payment is sufficient
            Dim totalPayments As Decimal = GetTotalPaymentAmount()
            If totalPayments < Billamt Then
                Dim remainingBalance As Decimal = Billamt - totalPayments
                If DevExpress.XtraEditors.XtraMessageBox.Show("Payment incomplete. Balance remaining: ₹" & remainingBalance.ToString("N2") & vbCrLf & "Do you want to proceed anyway?",
                                                             "Incomplete Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning) Then
                    Return
                End If
            End If

            ' All validations passed, close the form with OK result
            Me.DialogResult = Windows.Forms.DialogResult.OK

        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "Payment Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub GridControl3_Click(sender As Object, e As EventArgs) Handles GridControl3.Click
        Try
            Dim balanceamt As Decimal = 0.0
            Dim paidamt As Decimal = 0.0
            paidamt = GetTotalPaymentAmount()
            If paidamt = ConvertDecimal(txtPopBillamt.EditValue) Then
                txtpopbalamt.EditValue = 0
                Return
            End If
            _PaymentDtl.paymentId = GridView3.GetFocusedRowCellValue("pmode_id")
            _PaymentDtl.paymentModeSelection = GridView3.GetFocusedRowCellValue("pmode_name")
            _PaymentDtl.paymentMode = GridView3.GetFocusedRowCellValue("pmode_type")
            lblpaymode.Text = _PaymentDtl.paymentModeSelection
            If _PaymentDtl.paymentMode = "credit" Then
                txtadvanceamt.Enabled = True
                txtclientname.Enabled = True
            Else
                txtadvanceamt.Enabled = False
                txtclientname.Enabled = False
            End If
            Dim existingRow As DataRow = Nothing
            Dim existingPaymentAmount As Decimal = 0
            For Each row As DataRow In PaymentDetailTable.Rows
                If CInt(row("pmode_id")) = CInt(_PaymentDtl.paymentId) Then
                    existingRow = row
                    existingPaymentAmount = ConvertDecimal(row("pmode_amount"))
                    Exit For
                End If
            Next
            ' Payment validation passed, proceed with adding/updating
            PaymentDetailTable.BeginInit()
            If existingRow IsNot Nothing Then
                ' Update existing payment mode
                existingRow("pmode_amount") = txtPopBillamt.EditValue
            Else
                ' Add new payment mode
                If boolMultiplePayment = False Then
                    PaymentDetailTable.Rows.Add(_PaymentDtl.paymentId, _PaymentDtl.paymentModeSelection, _PaymentDtl.paymentMode, txtPopBillamt.EditValue)
                Else
                    Dim enteredAmount As Decimal = ConvertDecimal(txtentermultipleamount.EditValue)
                    ' Validate amount
                    If enteredAmount <= 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Please enter a valid amount", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    Else
                        PaymentDetailTable.Rows.Add(_PaymentDtl.paymentId, _PaymentDtl.paymentModeSelection, _PaymentDtl.paymentMode, enteredAmount)
                        ' Clear the entry field for next amount
                        txttpopenamt.EditValue = 0.0
                        txtmultipletotal.EditValue = GetTotalPaymentAmount()
                        txtpopbalamt.EditValue = ConvertDecimal(txtPopBillamt.EditValue) - txtmultipletotal.EditValue
                        txtentermultipleamount.EditValue = 0
                        txtentermultipleamount.Focus()
                    End If
                End If

            End If
            PaymentDetailTable.EndInit()
            PaymentDetailTable.AcceptChanges()
            paidamt = GetTotalPaymentAmount()
            If paidamt = ConvertDecimal(txtPopBillamt.EditValue) Then
                txtpopbalamt.EditValue = 0
                Return
            End If
            '' Get the current entered amount
            'Dim enteredAmount As Decimal = ConvertDecimal(txtentermultipleamount.EditValue)
            'If String.IsNullOrEmpty(enteredAmount) Or enteredAmount = 0 Then
            '    txtentermultipleamount.EditValue = txtPopBillamt.EditValue
            '    enteredAmount = txtPopBillamt.EditValue
            'Else
            '    enteredAmount = txtPopBillamt.EditValue
            'End If
            '' Validate amount
            'If enteredAmount <= 0 Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show("Please enter a valid amount", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '    Return
            'End If

            '' Allow overpayment for cash mode only, restrict others
            '' Check for duplicate payment ID and get existing amount first


            'For Each row As DataRow In PaymentDetailTable.Rows
            '    If CInt(row("pmode_id")) = CInt(_PaymentDtl.paymentId) Then
            '        existingRow = row
            '        existingPaymentAmount = ConvertDecimal(row("pmode_amount"))
            '        Exit For
            '    End If
            'Next

            'If _PaymentDtl.paymentMode.ToLower() <> "cash" Then
            '    ' Check if single payment exceeds bill amount (for non-cash payment modes)
            '    If enteredAmount > Billamt Then
            '        Dim excessAmount As Decimal = enteredAmount - Billamt
            '        DevExpress.XtraEditors.XtraMessageBox.Show("Single payment cannot exceed bill amount." & vbCrLf &
            '                                                 "Bill Amount: ₹" & Billamt.ToString("N2") & vbCrLf &
            '                                                 "Entered Amount: ₹" & enteredAmount.ToString("N2") & vbCrLf &
            '                                                 "Excess Amount: ₹" & excessAmount.ToString("N2") & vbCrLf &
            '                                                 "Please adjust your payment amount.",
            '                                                 "Payment Exceeds Bill Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '        txttpopenamt.EditValue = 0
            '        txttpopenamt.Focus()
            '        Return
            '    End If

            '    ' Check if multiple payments would exceed bill amount (for non-cash modes)
            '    Dim currentTotalPayments As Decimal = GetTotalPaymentAmount()
            '    If currentTotalPayments = ConvertDecimal(txtPopBillamt.EditValue) Then
            '        Return
            '    End If
            '    ' Calculate what the new total would be for multiple payments
            '    Dim newTotalPayments As Decimal = currentTotalPayments - existingPaymentAmount + enteredAmount
            '    txtmultipletotal.EditValue = newTotalPayments

            'Else
            '    ' For cash mode, allow overpayment and show change amount
            '    If enteredAmount > Billamt Then
            '        Dim changeAmount As Decimal = enteredAmount - Billamt
            '        ' Show change amount in balance field (negative value indicates change due)
            '        txtpopbalamt.EditValue = (-changeAmount).ToString("###0.00")
            '        txtpopbalamt.ForeColor = Color.Red ' Red indicates change to be given
            '    End If
            'End If

            '' Payment validation passed, proceed with adding/updating
            'PaymentDetailTable.BeginInit()
            'If existingRow IsNot Nothing Then
            '    ' Update existing payment mode
            '    existingRow("pmode_amount") = enteredAmount
            'Else
            '    ' Add new payment mode
            '    PaymentDetailTable.Rows.Add(_PaymentDtl.paymentId, _PaymentDtl.paymentModeSelection, _PaymentDtl.paymentMode, enteredAmount)
            'End If
            'PaymentDetailTable.EndInit()
            'PaymentDetailTable.AcceptChanges()

            ' Clear the amount field for next entry
           



           
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnpopClear_Click(sender As Object, e As EventArgs) Handles btnpopClear.Click
        Try
            PaymentDetailTable.Rows.Clear()
            numstr = ""
            txttpopenamt.EditValue = 0
            txtpopbalamt.EditValue = -Billamt.ToString("###0.00")
            ' Clear multiple payment fields
            ClearMultiplePaymentFields()
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "Clear Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Safely convert object to decimal, handling DBNull and empty values
    ''' </summary>
    Private Function ConvertDecimal(value As Object) As Decimal
        Try
            If value Is Nothing OrElse value Is DBNull.Value OrElse String.IsNullOrEmpty(value.ToString()) Then
                Return 0D
            End If
            Return Convert.ToDecimal(value)
        Catch ex As Exception
            Return 0D
        End Try
    End Function

    Private Sub btnpop7_Click(sender As Object, e As EventArgs) Handles btnpop7.Click, btnpop8.Click, btnpop9.Click, btnpop4.Click, btnpopn5.Click, btnpop6.Click, btnpopn1.Click, btnpop2.Click, btnpop3.Click, btnpop0.Click, btnpop00.Click
        Try
            Dim _btnnum As New SimpleButton
            _btnnum = CType(sender, SimpleButton)
            txttpopenamt.EditValue = txttpopenamt.EditValue + Convert.ToDouble(_btnnum.Tag).ToString("####0.00")
            numstr = numstr + _btnnum.Text
            txttpopenamt.EditValue = Format(Val(numstr) / 100, "#####.00")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txttpopenamt_EditValueChanged(sender As Object, e As EventArgs) Handles txttpopenamt.EditValueChanged
        Try
            ' Calculate balance based on entered amount minus bill amount
            Dim currentEnteredAmount As Decimal = 0
            If txttpopenamt.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(txttpopenamt.Text) Then
                currentEnteredAmount = ConvertDecimal(txttpopenamt.EditValue)
            End If
            'Dim total As Decimal = 0.0
            'total = Billamt - GetTotalPaymentAmount()
            'If total > 0 Then
            '    Dim balanceAmount As Decimal = currentEnteredAmount - total
            '    txtpopbalamt.EditValue = balanceAmount.ToString("###0.00")
            'Else
            ' Calculate balance: Entered Amount - Bill Amount
            Dim balanceAmount As Decimal = currentEnteredAmount - Billamt
            txtpopbalamt.EditValue = balanceAmount.ToString("###0.00")
            'End If


        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnpopNumClear_Click(sender As Object, e As EventArgs) Handles btnpopNumClear.Click
        Try
            Dim totals As Decimal = 0.0
            totals = GetTotalPaymentAmount()
            txtpopbalamt.EditValue = ConvertDecimal(txtPopBillamt.EditValue - totals)
            txttpopenamt.Text = ""
            numstr = ""
        Catch ex As Exception

        End Try
    End Sub
 

    ''' <summary>
    ''' Validate multiple payment total matches balance before proceeding
    ''' </summary>
    Private Function ValidateMultiplePaymentTotal() As Boolean
        Try
            Dim multipleTotal As Decimal = ConvertDecimal(txtmultipletotal.EditValue)
            Dim balanceAmount As Decimal = ConvertDecimal(txtPopBillamt.EditValue)

            ' Check if multiple payment total matches balance amount
            If Math.Abs(multipleTotal - balanceAmount) > 0.01D Then
                DevExpress.XtraEditors.XtraMessageBox.Show("Multiple payment total does not match balance amount." & vbCrLf &
                                                         "Multiple Total: ₹" & multipleTotal.ToString("N2") & vbCrLf &
                                                         "Balance Amount: ₹" & balanceAmount.ToString("N2") & vbCrLf &
                                                         "Please adjust your multiple payments.",
                                                         "Payment Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            Return True
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Clear multiple payment fields
    ''' </summary>
    Private Sub ClearMultiplePaymentFields()
        Try
            txtentermultipleamount.EditValue = 0
            txtmultipletotal.EditValue = 0
            boolMultiplePayment = False
            txtentermultipleamount.Enabled = False
            Label2.Text = "MultiplePayment-No"
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "Clear Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Handle multiple payment total change
    ''' </summary>
    Private Sub txtmultipletotal_EditValueChanged(sender As Object, e As EventArgs) Handles txtmultipletotal.EditValueChanged
        Try
            ' Update UI to show if multiple total matches balance
            Dim multipleTotal As Decimal = ConvertDecimal(txtmultipletotal.EditValue)
            Dim balanceAmount As Decimal = ConvertDecimal(txtpopbalamt.EditValue)
 

        Catch ex As Exception
            txtmultipletotal.ForeColor = Color.Black
        End Try
    End Sub
 

    ''' <summary>
    ''' Get total selected payment amount
    ''' </summary>
    Public Function GetTotalPaymentAmount() As Decimal
        Try
            Dim total As Decimal = 0
            For Each row As DataRow In PaymentDetailTable.Rows
                total += ConvertDecimal(row("pmode_amount"))

            Next
            Return total
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Get selected payment modes as dictionary for saving
    ''' </summary>
    Public Function GetSelectedPaymentModes() As Dictionary(Of Integer, Decimal)
        Dim paymentModes As New Dictionary(Of Integer, Decimal)

        Try
            For Each row As DataRow In PaymentDetailTable.Rows
                Dim paymentId As Integer = CInt(row("pmode_id"))
                Dim amount As Decimal = ConvertDecimal(row("pmode_amount"))
                If amount > 0 Then
                    paymentModes.Add(paymentId, amount)
                End If
            Next
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "Get Payment Modes Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return paymentModes
    End Function

    ''' <summary>
    ''' Check if payment is complete
    ''' </summary>
    Public Function IsPaymentComplete() As Boolean
        Try
            Return Math.Abs(GetTotalPaymentAmount() - Billamt) < 0.01D
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Handle double-click on payment detail grid to edit amount
    ''' </summary>
    Private Sub GridControlPaymore_DoubleClick(sender As Object, e As EventArgs) Handles GridControlPaymore.DoubleClick
        Try
            If GridControlPaymore.MainView IsNot Nothing Then
                Dim gridView = CType(GridControlPaymore.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                If gridView.FocusedRowHandle >= 0 Then
                    ' Get the selected payment details
                    Dim paymentId As Integer = CInt(gridView.GetFocusedRowCellValue("pmode_id"))
                    Dim paymentName As String = gridView.GetFocusedRowCellValue("pmode_name").ToString()
                    Dim currentAmount As Decimal = ConvertDecimal(gridView.GetFocusedRowCellValue("pmode_amount"))

                    ' Set the current values for editing
                    _PaymentDtl.paymentId = paymentId
                    _PaymentDtl.paymentModeSelection = paymentName
                    lblpaymode.Text = paymentName
                    txttpopenamt.EditValue = currentAmount

                    ' Remove the row (will be re-added when user clicks grid again)
                    PaymentDetailTable.BeginInit()
                    gridView.DeleteSelectedRows()
                    PaymentDetailTable.EndInit()
                    PaymentDetailTable.AcceptChanges()

                    
                    txttpopenamt.Focus()
                End If
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "Edit Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtentermultipleamount_Click(sender As Object, e As EventArgs) Handles txtentermultipleamount.Click
        Try
            frmKeyAmt.ShowDialog()
            frmKeyAmt.DialogResult = Windows.Forms.DialogResult.OK
            txtentermultipleamount.EditValue = _FunctionKeyBoardModule.gs_keyboardValueDecimal
             
        Catch ex As Exception

        End Try
    End Sub
    Dim boolMultiplePayment As Boolean = False
    Private Sub btnmultipayment_Click(sender As Object, e As EventArgs) Handles btnmultipayment.Click
        Try
            If boolMultiplePayment = False Then
                boolMultiplePayment = True
                Label2.Text = "MultiplePayment-Yes"
                txtentermultipleamount.Enabled = True
            Else
                boolMultiplePayment = False
                txtentermultipleamount.Enabled = False
                Label2.Text = "MultiplePayment-No"
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
