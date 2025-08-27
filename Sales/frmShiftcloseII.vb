Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports PosRetailWebBilling.clssalesProperty

Public Class frmShiftcloseII
    Dim _numstr As String = ""
    Dim PrintNoteTable As DataTable
    Dim errMsg As String
    Dim _dateTime As DateTime
    Dim _validShiftnoPrintNote As String = ""
    Private Sub frmShiftcloseII_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Initialize the PrintNote DataTable
            InitializePrintNoteTable()
            GridControl1.DataSource = PrintNoteTable
            _getCurrentShiftDayno("er")
            If validatePrintNotesII() = True Then
                btnsubmit.Enabled = True
            Else
                btnsubmit.Enabled = False
            End If
            lblstatus.Text = _saleSetting._curDayno & "/" & _saleSetting._curShiftno & vbNewLine & "Current Shift No Yet Close Is :" & _validShiftnoPrintNote
            'If _setitemDisp._paymentNotesTable.Rows.Count > 0 Then
            '    ' Populate the PrintNoteTable with data from _setitemDisp._paymentNotesTable
            '    For Each row As DataRow In _setitemDisp._paymentNotesTable.Rows
            '        Dim newRow As DataRow = PrintNoteTable.NewRow()
            '        newRow("PrintNote") = row("po_name")
            '        newRow("Count") = 0
            '        newRow("Amount") = 0
            '        PrintNoteTable.Rows.Add(newRow)
            '    Next
            'End If

            ' Add Coins row at runtime
            Dim coinsRow As DataRow = PrintNoteTable.NewRow()
            coinsRow("PrintNote") = "Coins"  ' Keep the name as Coins
            coinsRow("Count") = 0
            coinsRow("Amount") = 0
            PrintNoteTable.Rows.Add(coinsRow)
        Catch ex As Exception
            WriteErroLog("frmShiftcloseII_Load: " & ex.Message)
        End Try
    End Sub
    Function validatePrintNotesII() As Boolean
        Try
            Dim _prnds As New DataSet
            Dim _sqlShitclose(4) As SqlParameter
            _sqlShitclose(0) = New SqlParameter("@mode", "Prn")
            _sqlShitclose(1) = New SqlParameter("@psc_opbalance", "0")
            _sqlShitclose(2) = New SqlParameter("@psc_pcname", RegistrationDetails._localcompname)
            _sqlShitclose(3) = New SqlParameter("@psc_machineid", RegistrationDetails._machineId)
            _sqlShitclose(4) = New SqlParameter("@psc_machinename", RegistrationDetails._localPcname)
            _prnds = _sqlDataAdapter("sp_createshiftclose", _sqlShitclose, "er")
            If _prnds.Tables(0).Rows.Count > 0 Then
                Dim st = _prnds.Tables(0).Rows(0)(0).ToString
                If st = "1" Then
                    _validShiftnoPrintNote = _prnds.Tables(0).Rows(0)(1).ToString
                    Return True
                Else
                    _validShiftnoPrintNote = 0
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub InitializePrintNoteTable()
        Try
            ' Create the PrintNote DataTable
            PrintNoteTable = New DataTable("PrintNote")

            ' Add columns to the DataTable
            PrintNoteTable.Columns.Add("PrintNote", GetType(String))
            PrintNoteTable.Columns.Add("Count", GetType(Integer))
            PrintNoteTable.Columns.Add("Amount", GetType(Decimal))

        Catch ex As Exception
            WriteErroLog("InitializePrintNoteTable: " & ex.Message)
        End Try
    End Sub

    Private Sub btn_ok_Click(sender As Object, e As EventArgs) Handles btn_ok.Click
        Try
            Dim focustedRowHandle As Integer = 0
            Dim printnoteValue As Object
            Dim printnote As Integer

            printnoteValue = GridView1.GetFocusedRowCellValue("PrintNote")

            ' Check if the row is "Coins", if so use value 1 for calculation
            If printnoteValue.ToString().ToUpper() = "COINS" Then
                printnote = 1
            Else
                printnote = CInt(printnoteValue)
            End If

            Dim Amount As Double = 0.0
            Amount = printnote * txtnum.EditValue
            focustedRowHandle = GridView1.FocusedRowHandle
            PrintNoteTable.Rows(focustedRowHandle)("Count") = txtnum.EditValue
            PrintNoteTable.Rows(focustedRowHandle)("Amount") = Amount
            PrintNoteTable.AcceptChanges()
            txtnum.Text = ""
        Catch ex As Exception

        End Try

    End Sub
    Private Sub btn_7_Click(sender As Object, e As EventArgs) Handles btn_7.Click, btn_0.Click, btn_1.Click, btn_2.Click, btn_3.Click, btn_4.Click, btn_5.Click, btn_6.Click, btn_8.Click, btn_9.Click
        Try
            Dim btnValue As SimpleButton
            btnValue = CType(sender, SimpleButton)
            txtnum.EditValue = txtnum.EditValue + btnValue.Text
            '_numstr = _numstr + btnValue.Text
            'txtnum.EditValue = Format(Val(_numstr) / 100, "#####.00")
            txtnum.Select()
        Catch ex As Exception

        End Try


    End Sub

    Private Sub btn_numclear_Click(sender As Object, e As EventArgs) Handles btn_numclear.Click
        Try
            txtnum.Text = ""
            _numstr = ""
            txtnum.Select()
        Catch ex As Exception
            WriteErroLog(ex.Message)
        End Try
    End Sub

    Private Sub RepPrintNoteCount_Click(sender As Object, e As EventArgs) Handles RepPrintNoteCount.Click
        Try
            Dim focustedColumn As Integer = 0
            focustedColumn = GridView1.FocusedColumn.ColumnHandle
            If focustedColumn = 1 Then
                txtnum.Focus()
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnsubmit_Click(sender As Object, e As EventArgs) Handles btnsubmit.Click
        Try
            If DevExpress.XtraEditors.XtraMessageBox.Show("Do you want to proceed shift close", "Shiftclose", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                If btnshiftcloses() = True Then
                    ' Save print notes to database
                    If SavePrintNotesToDatabase() = True Then
                        'MessageBox.Show("Print notes saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        WriteErroLog("ShiftClose " & "Print notes saved successfully!" & _saleSetting._curShiftno - 1)
                        Me.Close()
                    Else
                        WriteErroLog("ShiftClose Faild " & "Failed to save print notes. Please check the error log.")
                    End If
                End If
            Else
                ' User clicked No, do nothing or show cancellation message
                WriteErroLog("ShiftClose Cancelled by user")
            End If
        Catch ex As Exception
            WriteErroLog("ShiftClose Faild btnsubmit_Click: " & ex.Message)
        End Try
    End Sub
    Function btnshiftcloses() As Boolean
        Dim _dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            _dialog.Caption = "Shift Close Processing.."
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
            'If validatePrintNotes() = True Then
            '    properClass.R_Msgstring = "Print Note Not Yet Done, Please Do Print Note."
            '    WriteErroLog("Print Note Not Yet Done")
            '    Dim frmsgOk As New frmMsgBoxOk
            '    frmsgOk.ShowDialog()
            '    Exit Try
            'End If
            If _globalSetting._AutoShiftClose = False Then 'Manual Close
                If _ShiftCloseProcess(errMsg) = False Then
                    WriteErroLog(errMsg)
                    properClass.R_Msgstring = "Shift Closing Problem " & errMsg
                    WriteErroLog("Print Note Not Yet Done")
                    Dim frmsgOk As New frmMsgBoxOk
                    frmsgOk.ShowDialog()
                    Return False
                Else
                    _dialog.Caption = "New Shift Processing.."
                    If _globalSetting._AutoShiftOpen = True Then
                        Dim _SqlparaShift(4) As SqlParameter
                        _SqlparaShift(0) = New SqlParameter("@mode", "I")
                        _SqlparaShift(1) = New SqlParameter("@psc_opbalance", "0.00")
                        _SqlparaShift(2) = New SqlParameter("@psc_pcname", RegistrationDetails._localcompname)
                        _SqlparaShift(3) = New SqlParameter("@psc_machineid", RegistrationDetails._machineId)
                        _SqlparaShift(4) = New SqlParameter("@psc_machinename", RegistrationDetails._localPcname)
                        If _ExecuteNonQuery("sp_createshiftclose", _SqlparaShift, "er") = False Then
                            ' altMsg.altmsg.Show(Me, "Login Check Ver 22.01", "_beforeValidationCheck" & statu & "," & msg, altMsg.img.Images(1))
                            WriteErroLog("Auto Shift Open Is Problem frmshiftclose")
                        Else
                            properClass.R_Msgstring = "Shift Close Done"
                            WriteErroLog("Shift Close Done")
                            Dim frmsgOk As New frmMsgBoxOk
                            frmsgOk.ShowDialog()
                            Return True
                        End If
                    End If
                End If
            Else
                properClass.R_Msgstring = "Auto ShiftClose Is On"
                WriteErroLog("Auto ShiftClose Is On")
                Dim frmsgOk As New frmMsgBoxOk
                frmsgOk.ShowDialog()
                Return False
            End If
            _dialog.Caption = "Shift Close Process End..."
            Return True
        Catch ex As Exception
            Return False
            _dialog.Close()
            WriteErroLog(ex.Message)
        Finally
            _dialog.Close()
        End Try
    End Function
    Private Function SavePrintNotesToDatabase() As Boolean
        Dim _boolPrintClose As Boolean = False
        Dim _dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            _dialog.Caption = "Shift Print Note Processing.."
            For Each row As DataRow In PrintNoteTable.Rows
                Dim noteValue As String = row("PrintNote").ToString()
                Dim count As Integer = CInt(row("Count"))
                Dim amount As Decimal = CDec(row("Amount"))
                Dim _Sqlprinnotes(4) As SqlParameter
                _Sqlprinnotes(0) = New SqlParameter("@mode", "I")
                _Sqlprinnotes(1) = New SqlParameter("@PrintNote", noteValue)
                _Sqlprinnotes(2) = New SqlParameter("@Count", count)
                _Sqlprinnotes(3) = New SqlParameter("@Amount", amount)
                _Sqlprinnotes(4) = New SqlParameter("@ShiftNo", _validShiftnoPrintNote)
                If _ExecuteNonQuery("sp_notesshiftII", _Sqlprinnotes, errMsg) = True Then
                    _dialog.Caption = "Shift Print Note Saved.."
                    _boolPrintClose = True
                Else
                    _boolPrintClose = False
                    WriteErroLog(errMsg)
                    _dialog.Close()
                    Return False
                End If
            Next
            If _boolPrintClose = True Then
                If _globalSetting._ShiftPrintDos = True Then
                    Dim printCommand As New PrintCommand
                    If printCommand._printShiftClose(_validShiftnoPrintNote, "S", _dateTime) = True Then
                        _dialog.Caption = "Printing..."
                        If printCommand._printShiftClose(_validShiftnoPrintNote, "F", _dateTime) = True Then
                            _dialog.Caption = "Preparing Pdf File.."
                            sendMailShiftDosMode(_validShiftnoPrintNote, True, _dateTime)
                            _dialog.Caption = "Mail Sent Processing.."
                            Application.Exit()
                            'Me.Close()
                            'Me.Dispose()
                        End If
                    End If
                Else
                    _shiftClosePrint(_validShiftnoPrintNote, True, True)
                End If
            End If
            _getCurrentShiftDayno("e")
            Return True
        Catch ex As Exception
            WriteErroLog(ex.Message)
            _dialog.Close()
            Return False
        Finally
            _dialog.Close()
        End Try
    End Function

End Class
