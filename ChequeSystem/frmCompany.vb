Imports System.IO
Imports Newtonsoft.Json

Public Class frmCompanyCheque
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()


        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            Dim comp As New company
            If btnSave.Text = "Save" Then
                dialog.Caption = "Connecting To Server"
                If txtbankname.Text.Length > 0 Then
                    comp.id = 0
                    comp.companyname = txtbankname.Text
                    comp.active = CheckEdit1.CheckState
                    Dim PostString As String = JsonConvert.SerializeObject(comp)
                    If _JsonSendChq(M_Details.LinkAjaxRequestCheque & "AjaxRequest=4&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _DataLoad()
                    End If

                    txtid.Text = ""
                    txtbankname.Text = ""
                    btnSave.Text = "Save"
                End If
            Else
                dialog.Caption = "Connecting To Server"
                comp.id = txtid.Text
                comp.companyname = txtbankname.Text
                comp.active = CheckEdit1.CheckState
                Dim PostString As String = JsonConvert.SerializeObject(comp)
                If _JsonSendChq(M_Details.LinkAjaxRequestCheque & "AjaxRequest=5&json=" & PostString) = True Then
                    dialog.Caption = "Data Saved Success.."
                    _DataLoad()
                End If

                txtid.Text = ""
                txtbankname.Text = ""
                btnSave.Text = "Save"
            End If

        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            btnSave.Text = "Update"
            Dim id = GridView1.GetFocusedRowCellValue("COID")
            Dim bname = GridView1.GetFocusedRowCellValue("CompanyName")
            Dim chkvalue = GridView1.GetFocusedRowCellValue("Active")

            txtid.Text = id
            txtbankname.Text = bname
            CheckEdit1.Checked = chkvalue
        Catch ex As Exception

        End Try
    End Sub
    Public Function UpdateTable(ByVal dt As DataTable, ByVal caption As String, ByVal value As String, ByVal active As Boolean) As Boolean
        Try

            Dim dtrow As Data.EnumerableRowCollection(Of DataRow) = From dtrows As DataRow In dt Where dtrows("Id") = caption

            If dtrow.Any Then
                dtrow(0)("CompanyName") = value
                dtrow(0)("Active") = active
            Else
                dt.Rows.Add(Nothing, value, active)
            End If

            dt.AcceptChanges()
            dt.WriteXml(M_Details._appPath & "\Settings\CompanyTable.xml", Data.XmlWriteMode.WriteSchema, True)
            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function
    Private Sub _DataLoad()
        Try
            getChqComapnyInfo()
            If ChqJson.CompanyTable.Rows.Count > 0 Then
                GridControl1.DataSource = ChqJson.CompanyTable
            End If
            CheckEdit1.CheckState = CheckState.Checked
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmCompany_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            _DataLoad()
            CheckEdit1.CheckState = CheckState.Checked
        Catch ex As Exception

        End Try
    End Sub
End Class
Class company
    Public Property id As String
    Public Property companyname As String
    Public Property active As String
End Class