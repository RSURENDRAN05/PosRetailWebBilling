
Imports Newtonsoft.Json
Public Class FrmSupplier
    
    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            Dim comp As New clssupplier
            If btnsave.Text = "Save" Then
                dialog.Caption = "Connecting To Server"
                If txtcompanyname.Text.Length > 0 Then
                    comp.supplierid = 0
                    comp.suppliername = txtcompanyname.Text
                    comp.cmbactive = chkactive.CheckState
                    Dim PostString As String = JsonConvert.SerializeObject(comp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=38&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _DataLoad()
                    End If
                    txtid.Text = ""
                    txtcompanyname.Text = ""
                    btnsave.Text = "Save"
                End If
            Else
                dialog.Caption = "Connecting To Server"
                If txtcompanyname.Text.Length > 0 Then
                    comp.supplierid = txtid.Text
                    comp.suppliername = txtcompanyname.Text
                    comp.cmbactive = chkactive.CheckState
                    Dim PostString As String = JsonConvert.SerializeObject(comp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=39&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _DataLoad()
                    End If
                End If
                txtid.Text = ""
                txtcompanyname.Text = ""
                btnsave.Text = "Save"
            End If

        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub

    Private Sub FrmCreateComp_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            btnsave.Text = "Save"
            chkactive.CheckState = CheckState.Checked
            _DataLoad()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Clear()
        Try
            txtid.Text = ""
            txtcompanyname.Text = ""
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _DataLoad()
        Try
            getSupplierTableMaster()
            If _JsonData.SupplierTable.Rows.Count > 0 Then
                GridControl1.DataSource = _JsonData.SupplierTable
            Else
                GridControl1.DataSource = Nothing
            End If
        Catch ex As Exception
            GridControl1.DataSource = Nothing
        End Try
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            Dim id = GridView1.GetFocusedRowCellValue("Id")
            Dim Name = GridView1.GetFocusedRowCellValue("SupplierName")
            Dim status = GridView1.GetFocusedRowCellValue("Active")

            txtid.Text = id
            txtcompanyname.Text = Name
            If status = 1 Then
                chkactive.CheckState = CheckState.Checked
            Else
                chkactive.CheckState = CheckState.Unchecked
            End If
            btnsave.Text = "Update"
        Catch ex As Exception

        End Try
    End Sub
End Class
Public Class clssupplier
    Public Property supplierid As String
    Public Property suppliername As String
    Public Property cmbactive As String
End Class