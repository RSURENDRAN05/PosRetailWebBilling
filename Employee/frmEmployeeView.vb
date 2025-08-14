Imports System.Net
Imports Newtonsoft.Json.Linq
Imports DevExpress.XtraEditors
Imports Newtonsoft.Json

Public Class frmEmployeeView
    Dim _DsEmp As DataTable
    Public Overloads Sub ShowDialog(ByVal _title As String)
        MyBase.ShowDialog()
    End Sub
    Private Sub frmEmployeeView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            dialog.Caption = "Get Data From Server.."
            If getEmployeeView() = False Then
                dialog.Caption = "Connection Error.."
            Else
                dialog.Caption = "Data Received.."
                If _DsEmp.Rows.Count > 0 Then
                    GridControl1.DataSource = _DsEmp.DefaultView
                Else
                    GridControl1.DataSource = Nothing
                End If
            End If
        Catch ex As Exception
            GridControl1.DataSource = Nothing
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub
    Public Function getEmployeeView() As Boolean
        Try
            _DsEmp = New DataTable
            _DsEmp.TableName = "EmployeeView"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "EmployeeReq=3&json=1")
            Dim Userparsejson As JObject = JObject.Parse(json)
            _DsEmp = Userparsejson("Data").ToObject(Of DataTable)()
            If _DsEmp.Rows.Count > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Msg", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

   
    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnok_Click(sender As Object, e As EventArgs) Handles btnok.Click
        Try
            If E_EmployeeId < 0 Then
                MessageBox.Show("Select Employee Id", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            E_EmployeeId = GridView1.GetFocusedRowCellValue("Id")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnshowpreview_Click(sender As Object, e As EventArgs) Handles btnshowpreview.Click
        Try
            GridControl1.ShowPrintPreview()
        Catch ex As Exception

        End Try
    End Sub
End Class