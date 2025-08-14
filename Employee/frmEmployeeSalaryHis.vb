Imports System.Net
Imports Newtonsoft.Json.Linq
Imports DevExpress.XtraEditors
Imports Newtonsoft.Json
Public Class frmEmployeeSalaryHis
    Dim _DsEmp As DataTable
    Dim emp_id As String = ""
    Public Overloads Sub ShowDialog(ByVal EmpId As String)
        emp_id = EmpId
        MyBase.ShowDialog()
    End Sub

    Private Sub frmEmployeeSalaryHis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            dialog.Caption = "Get Data From Server.."
            If getEmployeeView(emp_id) = False Then
                dialog.Caption = "Connection Error.."
            Else
                dialog.Caption = "Data Received.."
                If _DsEmp.Rows.Count > 0 Then
                    GridControl1.DataSource = _DsEmp.DefaultView
                Else
                    GridControl1.DataSource = Nothing
                    'Me.Close()
                End If
            End If
        Catch ex As Exception
            GridControl1.DataSource = Nothing
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub
    Public Function getEmployeeView(ByVal _EmpId As String) As Boolean
        Try
            _DsEmp = New DataTable
            _DsEmp.TableName = "EmployeeView"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "EmployeeReq=9&EmployeeId=" & _EmpId)
            Dim Userparsejson As JObject = JObject.Parse(json)
            _DsEmp = Userparsejson("Data").ToObject(Of DataTable)()
            If _DsEmp.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
End Class