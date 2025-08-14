Imports System.IO
Imports Newtonsoft.Json

Public Class frmpayeelist
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Dim payees As New payee
    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            'ChqJson.PayeeTable.BeginInit()
            'ChqJson.PayeeTable.Rows.Add(Nothing, txtpayeename.Text, 1)
            'ChqJson.PayeeTable.AcceptChanges()
            'ChqJson.PayeeTable.EndInit()
            'If File.Exists(M_Details.AppPath & "\Settings\PayeeName.xml") Then
            '    ChqJson.PayeeTable.WriteXml(M_Details.AppPath & "\Settings\PayeeName.xml")
            'End If
            payees.id = 0
            payees.payeename = txtpayeename.Text
            payees.active = 1
            dialog.Caption = "Connecting To Server"
            Dim PostString As String = JsonConvert.SerializeObject(payees)
            If _JsonSendChq(M_Details.LinkAjaxRequestCheque & "AjaxRequest=7&json=" & PostString) = True Then
                dialog.Caption = "Data Saved Success.."
                _DataLoad()
            End If
            txtpayeename.Text = ""
        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub


    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            If GridView1.FocusedColumn.ColumnHandle = 2 Then
                Dim id = GridView1.GetFocusedRowCellValue("Id")
                payees.id = GridView1.GetFocusedRowCellValue("Id")
                payees.payeename = GridView1.GetFocusedRowCellValue("PayeeName")
                payees.active = 0
                Dim s As DialogResult = MessageBox.Show("Are you Want to delete? " & payees.payeename, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                If s = Windows.Forms.DialogResult.Yes Then
                   
                    Dim PostString As String = JsonConvert.SerializeObject(payees)
                    If _JsonSendChq(M_Details.LinkAjaxRequestCheque & "AjaxRequest=8&json=" & PostString) = True Then
                        _DataLoad()
                    End If
                    'If File.Exists(M_Details.AppPath & "\Settings\PayeeName.xml") Then
                    '    ChqJson.PayeeTable.WriteXml(M_Details.AppPath & "\Settings\PayeeName.xml")
                    'End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _DataLoad()
        Try
            getPayeeInfo()
            If ChqJson.PayeeTable.Rows.Count > 0 Then
                GridControl1.DataSource = ChqJson.PayeeTable
            Else
                GridControl1.DataSource = Nothing
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmpayeelist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _DataLoad()
        Catch ex As Exception

        End Try
    End Sub
End Class
Class payee
    Public Property id As String
    Public Property payeename As String
    Public Property active As String
End Class