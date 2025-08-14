Imports Newtonsoft.Json

Public Class FrmTaxMaster

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FrmTaxMaster_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            txttaxname.Select()
            chkactive.CheckState = CheckState.Checked
            _DataLoad()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _DataLoad()
        Try
            getTaxMaster()
            If _JsonData.TaxTable.Rows.Count > 0 Then
                GridControl1.DataSource = _JsonData.TaxTable
            End If
            chkactive.CheckState = CheckState.Checked
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            Dim comp As New clstaxmaster
            If btnsave.Text = "Save" Then
                dialog.Caption = "Connecting To Server"
                If txttaxname.Text.Length > 0 And txttaxvalue.Text.Length > 0 Then
                    comp.taxid = 0
                    comp.taxname = txttaxname.Text
                    comp.taxvalue = txttaxvalue.Text
                    comp.taxactive = chkactive.CheckState
                    Dim PostString As String = JsonConvert.SerializeObject(comp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=9&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _DataLoad()
                    End If
                    txttaxid.Text = ""
                    txttaxname.Text = ""
                    txttaxvalue.Text = ""
                    btnsave.Text = "Save"
                End If
            Else
                dialog.Caption = "Connecting To Server"
                If txttaxname.Text.Length > 0 And txttaxvalue.Text.Length > 0 Then
                    comp.taxid = txttaxid.Text
                    comp.taxname = txttaxname.Text
                    comp.taxvalue = txttaxvalue.Text
                    comp.taxactive = chkactive.CheckState
                    Dim PostString As String = JsonConvert.SerializeObject(comp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=10&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _DataLoad()
                    End If
                End If
                txttaxid.Text = ""
                txttaxname.Text = ""
                txttaxvalue.Text = ""
                btnsave.Text = "Save"
            End If

        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click
        Try
            btnsave.Text = "Update"
            Dim id = GridView1.GetFocusedRowCellValue("TaxId")
            Dim taxname = GridView1.GetFocusedRowCellValue("TaxName")
            Dim taxvalue = GridView1.GetFocusedRowCellValue("TaxValue")
            Dim chkvalue = GridView1.GetFocusedRowCellValue("Active")

            txttaxid.Text = id
            txttaxname.Text = taxname
            txttaxvalue.Text = taxvalue
            chkactive.Checked = chkvalue
        Catch ex As Exception

        End Try
    End Sub
End Class
Public Class clstaxmaster
    Public Property taxid As String
    Public Property taxname As String
    Public Property taxvalue As String
    Public Property taxactive As String
End Class