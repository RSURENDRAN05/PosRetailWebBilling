Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class frmLedgerLoad
    Private JDate As Date
    Private errstr As String = String.Empty
    Public G_RefID As String = String.Empty
    Public Overloads Sub ShowDialog(ByVal _JDate As Date)
        JDate = _JDate
        MyBase.ShowDialog()
    End Sub
    Private Function ShowData(ByRef ErrorMsg As String) As Boolean
        Try
            Dim st As String = ""
            _DateConversion(JDate, JDate, st)
            Dim dsex As New DataTable
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxPayRec=2&date=" & st)
            Dim Userparsejson As JObject = JObject.Parse(json)
            dsex = Userparsejson("Journal").ToObject(Of DataTable)()
            If dsex.Rows.Count > 0 Then
                GridControlExists.DataSource = dsex
            End If
            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return False
        End Try
    End Function

    Private Sub frmLedgerLoad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If ShowData(errstr) = False Then
                DevExpress.XtraEditors.XtraMessageBox.Show(errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                GridControlExists.DataSource = Nothing
            Else
                'GridControlExists.ex
            End If
            GridControlExists.Focus()
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.OK
            If GridView1.IsGroupRow(GridView1.FocusedRowHandle) = True Then
                G_RefID = GridView1.GetGroupRowValue(GridView1.FocusedRowHandle)
            Else
                G_RefID = GridView1.GetFocusedRowCellValue("BillNo")
            End If
            GridControlExists.Focus()
            Me.Close()
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            GridControlExists.Focus()
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    Private Sub frmLedgerLoad_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                GridControlExists.Focus()
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                If GridView1.IsGroupRow(GridView1.FocusedRowHandle) = True Then
                    G_RefID = GridView1.GetGroupRowValue(GridView1.FocusedRowHandle)
                Else
                    G_RefID = GridView1.GetFocusedRowCellValue("BillNo")
                End If
                GridControlExists.Focus()
                Me.Close()
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If GridView1.IsGroupRow(GridView1.FocusedRowHandle) = True Then
                G_RefID = GridView1.GetGroupRowValue(GridView1.FocusedRowHandle)
            Else
                G_RefID = GridView1.GetFocusedRowCellValue("BillNo")
            End If
            Dim dsex As New DataTable
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxPayRec=3&billno=" & G_RefID)
            Dim Userparsejson As JObject = JObject.Parse(json)
            dsex = Userparsejson("Journal").ToObject(Of DataTable)()
            Dim _dt As New DataTable
            If dsex.Rows.Count > 0 Then
                dsex.TableName = "Voucher"
                _dt = dsex
                _dt.WriteXml(M_Details._appPath & "\Reports\UacVoucher.xml", True, True)
                _Print("A", dsex)
            End If
            Me.Hide()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub _Print(ByRef _str As String, ByRef _datset As DataTable)
        Try
            Select Case _str
                Case "A"
                    Dim rptvoucher As New rptStaffProfile
                    rptvoucher.LoadLayout(M_Details._appPath & "\Reports\payvoucher.repx")
                    rptvoucher.DataSource = _datset
                    Dim pt As New DevExpress.XtraReports.UI.ReportPrintTool(rptvoucher)
                    pt.ShowPreviewDialog()
                Case "B"

            End Select
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DateEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles DateEdit1.EditValueChanged
        Try
            JDate = DateEdit1.Text
            ShowData("er")
        Catch ex As Exception

        End Try
    End Sub
End Class