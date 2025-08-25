Imports System.IO

Public Class frmBankList
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        GridControl1.DataSource = CreateTableBankName()
        CheckEdit1.CheckState = CheckState.Checked
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If btnSave.Text = "Save" Then
                If txtbankname.Text.Length > 0 Then
                    ChqJson.BankTable.BeginInit()
                    ChqJson.BankTable.Rows.Add(Nothing, txtbankname.Text, CheckEdit1.CheckState)
                    ChqJson.BankTable.AcceptChanges()
                    ChqJson.BankTable.EndInit()
                    If File.Exists(M_Details._appPath & "\Settings\BankList.xml") Then
                        ChqJson.BankTable.WriteXml(M_Details._appPath & "\Settings\BankList.xml")
                    End If
                    txtid.Text = ""
                    txtbankname.Text = ""
                    btnSave.Text = "Save"
                End If
            Else
                UpdateTable(ChqJson.BankTable, txtid.Text, txtbankname.Text, CheckEdit1.CheckState)
                txtid.Text = ""
                txtbankname.Text = ""
                btnSave.Text = "Save"
            End If
           
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            btnSave.Text = "Update"
            Dim id = GridView1.GetFocusedRowCellValue("Id")
            Dim bname = GridView1.GetFocusedRowCellValue("BankName")
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
                dtrow(0)("BankName") = value
                dtrow(0)("Active") = active
            Else
                dt.Rows.Add(Nothing, value, active)
            End If

            dt.AcceptChanges()
            dt.WriteXml(M_Details._appPath & "\Settings\BankList.xml", Data.XmlWriteMode.WriteSchema, True)
            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function
End Class