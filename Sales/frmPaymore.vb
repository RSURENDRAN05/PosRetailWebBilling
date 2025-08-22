Public Class frmPaymore
    Private Billamt As Decimal = 0.0
    Private _AccName As String = String.Empty
    Private _Caption As String = "Paymode"
    Private cust As String = "CU000"
    Private _txt As String = "PayMode"
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

            _PaymentDtl.paymentMode = "cash"
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
            txtPopBillamt.Text = Billamt
            MyBase.ShowDialog()
            'store param for later user
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    Private Sub txttpopenamt_EditValueChanged(sender As Object, e As EventArgs) Handles txttpopenamt.EditValueChanged
        Try
            If txttpopenamt.EditValue Is Nothing OrElse txttpopenamt.Text = "" Then
                txtpopbalamt.Text = Format(txttpopenamt.Text, "####0.00")
            Else
                txtpopbalamt.EditValue = Convert.ToDouble(txttpopenamt.EditValue - txtPopBillamt.EditValue).ToString("###0.00")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmPaymore_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            txtpopbalamt.EditValue = 0.0
            txttpopenamt.EditValue = Billamt
            txtadvanceamt.EditValue = 0.0
            txtclientname.Enabled = False
            txtadvanceamt.Enabled = False
            If getPaymentList() = True Then
                If _JsonData.PaymodeList.Rows.Count > 0 Then
                    GridControl3.DataSource = _JsonData.PaymodeList.DefaultView
                End If
            End If
            lblpaymode.Text = "Cash"
        Catch ex As Exception

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
            If txtadvanceamt.EditValue Is Nothing Then
                txtadvanceamt.EditValue = 0
            End If
            If txtpopbalamt.EditValue Is Nothing OrElse String.IsNullOrEmpty(txtpopbalamt.EditValue) Then
                txtpopbalamt.EditValue = 0
            End If
            If txttpopenamt.EditValue Is Nothing OrElse String.IsNullOrEmpty(txttpopenamt.EditValue) Then
                txttpopenamt.EditValue = 0
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception

        End Try
    End Sub

   
    Private Sub GridControl3_Click(sender As Object, e As EventArgs) Handles GridControl3.Click
        Try
            
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
        Catch ex As Exception

        End Try
    End Sub
End Class