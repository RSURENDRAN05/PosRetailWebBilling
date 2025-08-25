Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Data
Imports System
Imports PosRetailWebBilling.clssalesProperty

Module shiftclosecheck
    Dim errMsg As String = ""
    Public Function _beforeDayCheck() As Boolean
        Try
            Dim _dstbeforevalid As New DataSet
            Dim _Sqlpara1(5) As SqlParameter
            _Sqlpara1(0) = New SqlParameter("@mode", "bdaycheck")
            _Sqlpara1(1) = New SqlParameter("@comid", "0")
            _Sqlpara1(2) = New SqlParameter("@locid", "0")
            _Sqlpara1(3) = New SqlParameter("@pcname", RegistrationDetails._localPcname)
            _Sqlpara1(4) = New SqlParameter("@userid", "0")
            _Sqlpara1(5) = New SqlParameter("@bdate", "2022/02/28")
            _dstbeforevalid = _sqlDataAdapter("sp_beforeValidation", _Sqlpara1, errMsg)
            Dim statu = _dstbeforevalid.Tables(0).Rows(0)(0).ToString
            Dim msg = _dstbeforevalid.Tables(0).Rows(0)(1).ToString
            Select Case statu
                Case "2" 'Current Date Open
                    Return True
                Case "3" 'ask new shift
                    If _globalSetting._AutoShiftOpen = True Then
                        GoTo AutoshiftOpen
                    End If
                    properClass.R_Msgstring = "Do you want to create New Shift?"
                    Dim frmmsgbox As New frmMsgBox
                    frmmsgbox.ShowDialog()
                    If properClass.R_YesOrNo = "Yes" Then
                        Dim frmdeno As New frmDeno
                        frmdeno.ShowDialog()
                        If properClass.R_denoamt > 0 Then
AutoshiftOpen:
                            Dim _SqlparaShift(4) As SqlParameter
                            _SqlparaShift(0) = New SqlParameter("@mode", "I")
                            _SqlparaShift(1) = New SqlParameter("@psc_opbalance", properClass.R_denoamt)
                            _SqlparaShift(2) = New SqlParameter("@psc_pcname", RegistrationDetails._localcompname)
                            _SqlparaShift(3) = New SqlParameter("@psc_machineid", RegistrationDetails._machineId)
                            _SqlparaShift(4) = New SqlParameter("@psc_machinename", RegistrationDetails._localPcname)
                            If _ExecuteNonQuery("sp_createshiftclose", _SqlparaShift, "er") = True Then
                                ' altMsg.altmsg.Show(Me, "Login Check Ver 22.01", "_beforeValidationCheck" & statu & "," & msg, altMsg.img.Images(1))
                                Return True
                            End If
                        End If
                    Else
                        Return False
                    End If
                Case "5"
                    If _globalSetting._AutoDayOpen = True Then
                        properClass.R_BusinessDate = Date.Now.ToString("yyyy-MM-dd")
                        GoTo AUTODayOPEN
                    End If
                    properClass.R_Msgstring = "Do you want to create New Business?"
                    Dim frmmsgbox As New frmMsgBox
                    frmmsgbox.ShowDialog()
                    If properClass.R_YesOrNo = "Yes" Then
                        Dim frmnewbusiness As New frmbusinessDate
                        frmnewbusiness.ShowDialog()
                        If properClass.R_YesOrNo = "Ok" Then
AUTODayOPEN:
                            Dim _Sqlpara2(5) As SqlParameter
                            _Sqlpara2(0) = New SqlParameter("@mode", "ubusiness")
                            _Sqlpara2(1) = New SqlParameter("@comid", "0")
                            _Sqlpara2(2) = New SqlParameter("@locid", "0")
                            _Sqlpara2(3) = New SqlParameter("@pcname", RegistrationDetails._localPcname)
                            _Sqlpara2(4) = New SqlParameter("@userid", "0")
                            _Sqlpara2(5) = New SqlParameter("@bdate", properClass.R_BusinessDate)
                            If _ExecuteNonQuery("sp_beforeValidation", _Sqlpara2, "er") = True Then
                                ' altMsg.altmsg.Show(Me, "Login Check Ver 22.01", "New Business Date Updated -" & properClass.R_BusinessDate, altMsg.img.Images(1))
                            End If
                            GoTo AutoshiftOpen
                        Else
                            Return False
                        End If
                    Else
                        Return False
                    End If
                Case "6"
                    If _globalSetting._AutoDayOpen = True Then
                        properClass.R_BusinessDate = Date.Now.ToString("yyyy-MM-dd")
                        GoTo AUTODayOPEN6
                    End If
                    properClass.R_Msgstring = "Do you want to create New Business?"
                    Dim frmmsgbox As New frmMsgBox
                    frmmsgbox.ShowDialog()
                    If properClass.R_YesOrNo = "Yes" Then
                        Dim frmnewbusiness As New frmbusinessDate
                        frmnewbusiness.ShowDialog()
                        If properClass.R_YesOrNo = "Ok" Then
AUTODayOPEN6:
                            Dim _Sqlpara2(5) As SqlParameter
                            _Sqlpara2(0) = New SqlParameter("@mode", "ubusiness")
                            _Sqlpara2(1) = New SqlParameter("@comid", "0")
                            _Sqlpara2(2) = New SqlParameter("@locid", "0")
                            _Sqlpara2(3) = New SqlParameter("@pcname", RegistrationDetails._localPcname)
                            _Sqlpara2(4) = New SqlParameter("@userid", "0")
                            _Sqlpara2(5) = New SqlParameter("@bdate", properClass.R_BusinessDate)
                            If _ExecuteNonQuery("sp_beforeValidation", _Sqlpara2, "er") = False Then
                                ' altMsg.altmsg.Show(Me, "Login Check Ver 22.01", "New Business Date Updated -" & properClass.R_BusinessDate, altMsg.img.Images(1))
                            Else
                                Return True
                            End If
                            ' GoTo L3
                        Else
                            Return False
                        End If
                    Else
                        Return False
                    End If

            End Select
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function _beforeValidationCheck() As Boolean
        Try
            Dim _dstbeforevalid As New DataSet
            Dim _Sqlpara1(5) As SqlParameter
            _Sqlpara1(0) = New SqlParameter("@mode", "bcheck")
            _Sqlpara1(1) = New SqlParameter("@comid", "0")
            _Sqlpara1(2) = New SqlParameter("@locid", "0")
            _Sqlpara1(3) = New SqlParameter("@pcname", RegistrationDetails._localPcname)
            _Sqlpara1(4) = New SqlParameter("@userid", "0")
            _Sqlpara1(5) = New SqlParameter("@bdate", "2022/02/28")
            _dstbeforevalid = _sqlDataAdapter("sp_beforeValidation", _Sqlpara1, errMsg)
            Dim statu = _dstbeforevalid.Tables(0).Rows(0)(0).ToString
            Dim msg = _dstbeforevalid.Tables(0).Rows(0)(1).ToString
            Select Case statu

                Case "1" 'Previous day Open
                    properClass.R_BusinessDate = Date.Now.ToString("yyyy-MM-dd")
                    Dim _Sqlpara2(5) As SqlParameter
                    _Sqlpara2(0) = New SqlParameter("@mode", "usclosedt")
                    _Sqlpara2(1) = New SqlParameter("@comid", "0")
                    _Sqlpara2(2) = New SqlParameter("@locid", "0")
                    _Sqlpara2(3) = New SqlParameter("@pcname", RegistrationDetails._localPcname)
                    _Sqlpara2(4) = New SqlParameter("@userid", "0")
                    _Sqlpara2(5) = New SqlParameter("@bdate", properClass.R_BusinessDate)
                    If _ExecuteNonQuery("sp_beforeValidation", _Sqlpara2, "er") = True Then
                        ' altMsg.altmsg.Show(Me, "Login Check Ver 22.01", "New Business Date Updated -" & properClass.R_BusinessDate, altMsg.img.Images(1))
                        Return True
                    End If
                    'properClass.R_Msgstring = "Please Check Shift Already Open On Yesterday Date?"
                    'Dim frmmsgbox As New frmMsgBoxOk
                    'frmmsgbox.ShowDialog()
                Case "2" 'Current Date Open
                    Return True
                Case "3" 'ask new shift
                    If _globalSetting._AutoShiftOpen = True Then
                        GoTo AutoshiftOpen
                    End If
                    properClass.R_Msgstring = "Do you want to create New Shift?"
                    Dim frmmsgbox As New frmMsgBox
                    frmmsgbox.ShowDialog()
                    If properClass.R_YesOrNo = "Yes" Then
                        Dim frmdeno As New frmDeno
                        frmdeno.ShowDialog()
                        If properClass.R_denoamt > 0 Then
AutoshiftOpen:
                            Dim _SqlparaShift(4) As SqlParameter
                            _SqlparaShift(0) = New SqlParameter("@mode", "I")
                            _SqlparaShift(1) = New SqlParameter("@psc_opbalance", properClass.R_denoamt)
                            _SqlparaShift(2) = New SqlParameter("@psc_pcname", RegistrationDetails._localcompname)
                            _SqlparaShift(3) = New SqlParameter("@psc_machineid", RegistrationDetails._machineId)
                            _SqlparaShift(4) = New SqlParameter("@psc_machinename", RegistrationDetails._localPcname)
                            If _ExecuteNonQuery("sp_createshiftclose", _SqlparaShift, "er") = True Then
                                ' altMsg.altmsg.Show(Me, "Login Check Ver 22.01", "_beforeValidationCheck" & statu & "," & msg, altMsg.img.Images(1))
                                Return True
                            End If
                        End If
                    Else
                        Return False
                    End If
                Case "4" 'shift not found on this pc
                    If _globalSetting._AutoShiftOpen = True Then
                        GoTo AutoshiftOpen
                    End If
                Case "5"
                    If _globalSetting._AutoDayOpen = True Then
                        properClass.R_BusinessDate = Date.Now.ToString("yyyy-MM-dd")
                        GoTo AUTODayOPEN
                    End If
                    properClass.R_Msgstring = "Do you want to create New Business?"
                    Dim frmmsgbox As New frmMsgBox
                    frmmsgbox.ShowDialog()
                    If properClass.R_YesOrNo = "Yes" Then
                        Dim frmnewbusiness As New frmbusinessDate
                        frmnewbusiness.ShowDialog()
                        If properClass.R_YesOrNo = "Ok" Then
AUTODayOPEN:
                            Dim _Sqlpara2(5) As SqlParameter
                            _Sqlpara2(0) = New SqlParameter("@mode", "ubusiness")
                            _Sqlpara2(1) = New SqlParameter("@comid", "0")
                            _Sqlpara2(2) = New SqlParameter("@locid", "0")
                            _Sqlpara2(3) = New SqlParameter("@pcname", RegistrationDetails._localPcname)
                            _Sqlpara2(4) = New SqlParameter("@userid", "0")
                            _Sqlpara2(5) = New SqlParameter("@bdate", properClass.R_BusinessDate)
                            If _ExecuteNonQuery("sp_beforeValidation", _Sqlpara2, "er") = True Then
                                ' altMsg.altmsg.Show(Me, "Login Check Ver 22.01", "New Business Date Updated -" & properClass.R_BusinessDate, altMsg.img.Images(1))
                            End If
                            GoTo AutoshiftOpen
                        Else
                            Return False
                        End If
                    Else
                        Return False
                    End If
                Case "6"
                    If _globalSetting._AutoDayOpen = True Then
                        properClass.R_BusinessDate = Date.Now.ToString("yyyy-MM-dd")
                        GoTo AUTODayOPEN6
                    End If
                    properClass.R_Msgstring = "Do you want to create New Business?"
                    Dim frmmsgbox As New frmMsgBox
                    frmmsgbox.ShowDialog()
                    If properClass.R_YesOrNo = "Yes" Then
                        Dim frmnewbusiness As New frmbusinessDate
                        frmnewbusiness.ShowDialog()
                        If properClass.R_YesOrNo = "Ok" Then
AUTODayOPEN6:
                            Dim _Sqlpara2(5) As SqlParameter
                            _Sqlpara2(0) = New SqlParameter("@mode", "ubusiness")
                            _Sqlpara2(1) = New SqlParameter("@comid", "0")
                            _Sqlpara2(2) = New SqlParameter("@locid", "0")
                            _Sqlpara2(3) = New SqlParameter("@pcname", RegistrationDetails._localPcname)
                            _Sqlpara2(4) = New SqlParameter("@userid", "0")
                            _Sqlpara2(5) = New SqlParameter("@bdate", properClass.R_BusinessDate)
                            If _ExecuteNonQuery("sp_beforeValidation", _Sqlpara2, "er") = False Then
                                ' altMsg.altmsg.Show(Me, "Login Check Ver 22.01", "New Business Date Updated -" & properClass.R_BusinessDate, altMsg.img.Images(1))
                            Else
                                Return True
                            End If
                            ' GoTo L3
                        Else
                            Return False
                        End If
                    Else
                        Return False
                    End If
                Case "7"
                    properClass.R_Msgstring = "Please Contact Admin Previous Date Mismatch"
                    Dim frmmsgbox As New frmMsgBoxOk
                    frmmsgbox.ShowDialog()
                    Return False
            End Select
            Return True
        Catch ex As Exception
            EventlogModule.WriteErroLog(ex.Message)
            Return False
        End Try
    End Function
End Module
