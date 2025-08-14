Imports Newtonsoft.Json

Public Class FrmUnitMaster

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            Dim unit As New unitmaster
            If btnSave.Text = "Save" Then
                If txtunitname.Text.Length > 0 Then
                    dialog.Caption = "Connecting To Server"
                    unit.dum_id = 0
                    unit.dum_name = txtunitname.Text
                    unit.dum_active = chkactive.CheckState
                    Dim PostString As String = JsonConvert.SerializeObject(unit)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=36&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _clear()
                    End If
                End If

            Else
                If txtunitname.Text.Length > 0 AndAlso txtunitid.Text.Length > 0 Then
                    dialog.Caption = "Connecting To Server"
                    unit.dum_id = txtunitid.Text
                    unit.dum_name = txtunitname.Text
                    unit.dum_active = chkactive.CheckState
                    Dim PostString As String = JsonConvert.SerializeObject(unit)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=37&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _clear()
                    End If
                End If
            End If
        Catch ex As Exception
            dialog.Caption = ex.Message
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _clear()
        Try
            txtunitid.Text = ""
            txtunitname.Text = ""
            chkactive.CheckState = CheckState.Checked
            btnSave.Text = "Save"
            _DataLoad()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _DataLoad()
        Try
            getUnitMasterMaster()
            If _JsonData.UnitMasterTable.Rows.Count > 0 Then
                GridControl1.DataSource = _JsonData.UnitMasterTable.DefaultView
            Else
                GridControl1.DataSource = Nothing
            End If
        Catch ex As Exception
            GridControl1.DataSource = Nothing
        End Try
    End Sub
    Private Sub FrmUnitMaster_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            _clear()
        Catch ex As Exception
            GridControl1.DataSource = Nothing
        End Try
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            btnSave.Text = "Update"
            Dim id = GridView1.GetFocusedRowCellValue("Id")
            Dim name = GridView1.GetFocusedRowCellValue("UnitName")
            Dim active = GridView1.GetFocusedRowCellValue("Active")

            txtunitid.Text = id
            txtunitname.Text = name
            chkactive.CheckState = active
        Catch ex As Exception

        End Try
    End Sub
End Class
Public Class unitmaster
    Public Property dum_id As String
    Public Property dum_name As String
    Public Property dum_active As String
End Class