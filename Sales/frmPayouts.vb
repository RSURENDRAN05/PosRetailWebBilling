
'bs.Filter = "Field LIKE '%test%', "
Imports DevExpress.XtraGrid
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Data
Imports System
Imports PosRetailWebBilling.clssalesProperty

Public Class frmPayouts
    Dim StaffTable As DataTable
    Dim ledgerType As Integer = 1
    Dim _dsDataLoad As DataSet
    Dim errMsg As String
    Dim keyTextNum As New xkeyboard
    Dim _newSave As Boolean = False
    Dim _casdr As New RawPrinter
    Dim ModeOfUpload As String = "Local"
    Public Function CreateStaffTable() As DataTable
        Try
            StaffTable = New DataTable
            StaffTable.TableName = "StaffTable"
            StaffTable.Columns.Add("Id", GetType(Integer)).AutoIncrement = True  '0
            StaffTable.Columns.Add("Name", GetType(String)).DefaultValue = "SP" '19
            Return StaffTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Sub _DsLoad(ByRef _mode As String)
        Try
            _dsDataLoad = New DataSet
            If _JsonData.SalesManDataTable.Rows.Count > 0 Then
                StaffTable.BeginInit()
                For Each rowStaff In _JsonData.SalesManDataTable.Rows
                    Dim id = rowStaff("Id")
                    Dim name = rowStaff("SalesMan")
                    StaffTable.Rows.Add(id, name)
                Next
                StaffTable.AcceptChanges()
                GridControl1.DataSource = StaffTable
            End If
       
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            _newSave = True
            txtId.Text = GridView1.GetFocusedRowCellValue("Id").ToString
            txtName.Text = GridView1.GetFocusedRowCellValue("Name").ToString
            txtRemarks.Text = "Advance To " & txtName.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmPayouts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GridControl1.DataSource = CreateStaffTable()
            _DsLoad("STA")
            _PayoutDetailsLoad()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtName_Click(sender As Object, e As EventArgs)
        Try
            Dim keyTextNum As New xkeyboard
            properClass.R_TextNumKey = txtName.Text
            keyTextNum.ShowDialog()
            txtName.Text = properClass.R_TextNumKey
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtAmount_Click(sender As Object, e As EventArgs) Handles txtAmount.Click
        Try
            Dim keyTextNum As New frmKeyQtyAmt
            keyTextNum.ShowDialog()
            txtAmount.Text = Format(properClass.MKeyQtyAmt, "####0.00")

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtRemarks_Click(sender As Object, e As EventArgs) Handles txtRemarks.Click
        Try
            Dim keyTextNum As New xkeyboard
            properClass.R_TextNumKey = txtRemarks.Text
            keyTextNum.ShowDialog()
            txtRemarks.Text = properClass.R_TextNumKey
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Not String.IsNullOrEmpty(txtId.Text) OrElse String.IsNullOrEmpty(txtAmount.Text) Then
                Dim remas As String = ""
                If String.IsNullOrEmpty(txtRemarks.Text) Then
                    remas = txtName.Text
                Else
                    remas = txtRemarks.Text
                End If
                If ModeOfUpload = "Local" Then
                    If _newSave = True Then
                        Dim _SqlpayoutSave(11) As SqlParameter
                        _SqlpayoutSave(0) = New SqlParameter("@mode", "I")
                        _SqlpayoutSave(1) = New SqlParameter("@payd_id", "0")
                        _SqlpayoutSave(2) = New SqlParameter("@payd_ledgerid", txtId.Text)
                        _SqlpayoutSave(3) = New SqlParameter("@payd_name", txtName.Text)
                        _SqlpayoutSave(4) = New SqlParameter("@payd_amount", txtAmount.Text)
                        _SqlpayoutSave(5) = New SqlParameter("@payd_remarks", remas)
                        _SqlpayoutSave(6) = New SqlParameter("@payd_shiftno", _saleSetting._curShiftno)
                        _SqlpayoutSave(7) = New SqlParameter("@payd_dayno", _saleSetting._curDayno)
                        _SqlpayoutSave(8) = New SqlParameter("@payd_user", _companyInfo.UserId)
                        _SqlpayoutSave(9) = New SqlParameter("@payd_ledgertype", ledgerType)
                        _SqlpayoutSave(10) = New SqlParameter("@POS_MACHINEID", RegistrationDetails._machineId)
                        _SqlpayoutSave(11) = New SqlParameter("@POS_MACHINENAME", RegistrationDetails._localPcname)
                        If _ExecuteNonQuery("sp_payout_save", _SqlpayoutSave, errMsg) = False Then
                            WriteErroLog(errMsg)
                        Else
                            _newSave = False
                            txtAmount.Text = ""
                            txtId.Text = ""
                            txtName.Text = ""
                            _PayoutDetailsLoad()
                            Dim wSalesPrint As New WindowsPrinter
                            If wSalesPrint._PayOutSinglePrint = False Then
                                WriteErroLog(errMsg)
                            Else
                                Dim windprint As New PrintCommand
                                If windprint._PayoutSingleEntry() = False Then
                                    'MessageBox.Show("Not Print")
                                End If
                                _casdr.OpenCashdrawer(True)

                            End If
                        End If
                    End If
                Else
                    'web
                    Dim payoutcls As New Payout

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _PayoutDetailsLoad()
        Try
            Dim _DSpayoyut As New DataSet
            Dim _SqlpayoutSave(11) As SqlParameter
            _SqlpayoutSave(0) = New SqlParameter("@mode", "S") 'S supplier / 'Staff 
            _SqlpayoutSave(1) = New SqlParameter("@payd_id", "0")
            _SqlpayoutSave(2) = New SqlParameter("@payd_ledgerid", "0")
            _SqlpayoutSave(3) = New SqlParameter("@payd_name", "0")
            _SqlpayoutSave(4) = New SqlParameter("@payd_amount", "0")
            _SqlpayoutSave(5) = New SqlParameter("@payd_remarks", "0")
            _SqlpayoutSave(6) = New SqlParameter("@payd_shiftno", _saleSetting._curShiftno)
            _SqlpayoutSave(7) = New SqlParameter("@payd_dayno", "0")
            _SqlpayoutSave(8) = New SqlParameter("@payd_user", "0")
            _SqlpayoutSave(9) = New SqlParameter("@payd_ledgertype", ledgerType)
            _SqlpayoutSave(10) = New SqlParameter("@POS_MACHINEID", RegistrationDetails._machineId)
            _SqlpayoutSave(11) = New SqlParameter("@POS_MACHINENAME", RegistrationDetails._localPcname)
            _DSpayoyut = _sqlDataAdapter2("sp_payout_save", _SqlpayoutSave)
            If _DSpayoyut.Tables(0).Rows.Count > 0 Then
                GridControl2.DataSource = _DSpayoyut.Tables(0)
            Else
                GridControl2.DataSource = _DSpayoyut.Tables(0)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView2_RowClick(sender As Object, e As Views.Grid.RowClickEventArgs) Handles GridView2.RowClick
        Try
            Try
                _newSave = False
                txtId.Text = GridView2.GetFocusedRowCellValue("ID").ToString
                txtName.Text = GridView2.GetFocusedRowCellValue("Name").ToString
                txtAmount.Text = GridView2.GetFocusedRowCellValue("Amount").ToString
                txtRemarks.Text = GridView2.GetFocusedRowCellValue("Remarks").ToString
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click

        Try
            properClass.R_Msgstring = txtId.Text & " -" & txtName.Text & vbNewLine & "Do You Want To Delete?"
            Dim frmmsg As New frmMsgBox
            frmmsg.ShowDialog()
            If properClass.R_YesOrNo = "Yes" Then
                Dim _DSpayoyut As New DataSet
                Dim _SqlpayoutSave(11) As SqlParameter
                _SqlpayoutSave(0) = New SqlParameter("@mode", "D")
                _SqlpayoutSave(1) = New SqlParameter("@payd_id", txtId.Text)
                _SqlpayoutSave(2) = New SqlParameter("@payd_ledgerid", "0")
                _SqlpayoutSave(3) = New SqlParameter("@payd_name", "0")
                _SqlpayoutSave(4) = New SqlParameter("@payd_amount", "0")
                _SqlpayoutSave(5) = New SqlParameter("@payd_remarks", "0")
                _SqlpayoutSave(6) = New SqlParameter("@payd_shiftno", _saleSetting._curShiftno)
                _SqlpayoutSave(7) = New SqlParameter("@payd_dayno", "0")
                _SqlpayoutSave(8) = New SqlParameter("@payd_user", "0")
                _SqlpayoutSave(9) = New SqlParameter("@payd_ledgertype", ledgerType)
                _SqlpayoutSave(10) = New SqlParameter("@POS_MACHINEID", RegistrationDetails._machineId)
                _SqlpayoutSave(11) = New SqlParameter("@POS_MACHINENAME", RegistrationDetails._localPcname)
                If _ExecuteNonQuery("sp_payout_save", _SqlpayoutSave, errMsg) = True Then
                    WriteAuditLog(_companyInfo.UserId, "PayOutDelete", "PayoutName :" & txtName.Text & "- Amount" & txtAmount.Text)
                    _PayoutDetailsLoad()
                    _MailSentMsg("PayOutDelete => UserName :" & _companyInfo.UserName & " ShiftNo : " & _saleSetting._curShiftno & vbNewLine & " PayoutName :" & txtName.Text & vbNewLine & " Amount: " & txtAmount.Text & vbNewLine & "Date :" & Date.Now, MailConfiguration._custMailID1)
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnStaff_Click(sender As Object, e As EventArgs) Handles btnStaff.Click
        ledgerType = 2
        lblHead.Text = "PayOut " & ledgerType & " Staff "
        _DsLoad("STA")
    End Sub

    Private Sub btnSupp_Click(sender As Object, e As EventArgs) Handles btnSupp.Click
        ledgerType = 1
        lblHead.Text = "PayOut " & ledgerType & " Supplier "
        _DsLoad("SUP")
    End Sub


    Private Sub btnModeofWeb_Click(sender As Object, e As EventArgs) Handles btnModeofWeb.Click
        Try
            If ModeOfUpload = "Web" Then
                btnModeofWeb.Text = ModeOfUpload
            Else
                btnModeofWeb.Text = "Local"
                ModeOfUpload = "Local"
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
Public Class Payout
    Public Property Mode As String
    Public Property Payd_Id As Integer
    Public Property Payd_LedgerId As Integer
    Public Property Payd_Name As String
    Public Property Payd_Amount As Decimal
    Public Property Payd_Remarks As String
    Public Property Payd_ShiftNo As Integer
    Public Property Payd_DayNo As Integer
    Public Property Payd_User As Integer
    Public Property Payd_LedgerType As String
    Public Property POS_MachineId As String
    Public Property POS_MachineName As String
    Public Property payd_deletestatus As String
End Class
