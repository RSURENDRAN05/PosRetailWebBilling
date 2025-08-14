Imports Newtonsoft.Json
Imports System.Net
Imports Newtonsoft.Json.Linq
Imports System.IO

Public Class frmBankStatement
    Private dtview As New DataView
    Dim ds As DataTable

    Private Sub frmBankStatement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'GridControl1.DataSource = CreateTableBankTableSave()
        Barfromdate.EditValue = Date.Now
        BartoDate.EditValue = Date.Now
        _DataLoad()
    End Sub

    Private Sub barbtn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtn.ItemClick
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            ''dtview = BankTableSave.AsDataView
            ''dtview.RowFilter = " (PayeeDate >= #" & Barfromdate.EditValue & "# And PayeeDate <= #" & BartoDate.EditValue & "# ) "
            ''dtview.RowFilter = "([PayeeDate] >= '13/07/2012') AND ([PayeeDate] <= '17/07/2012')"
            ''GridControl1.DataSource = dtview



            'Dim filtered = From row In ChqJson.BankTableSave.AsEnumerable()
            '    Where row.Field(Of DateTime)("PayeeDate") >= Convert.ToDateTime(Barfromdate.EditValue).ToString("dd/MM/yyyy") AndAlso row.Field(Of DateTime)("PayeeDate") <= Convert.ToDateTime(BartoDate.EditValue).ToString("dd/MM/yyyy")


            dialog.Caption = "Connecting To Database"
            'Dim tblFiltered As DataTable = filtered.CopyToDataTable()
            'GridControl1.DataSource = tblFiltered
            Dim retfrdate As String = ""
            Dim rettodate As String = ""
            _DateConversion(Barfromdate.EditValue, BartoDate.EditValue, retfrdate, rettodate)
            ds = New DataTable
            ds.TableName = "DataLoad"
            dialog.Caption = "Collecting Data From Server"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequestCheque & "AjaxRequest=12&frdate=" & retfrdate & "&todate=" & rettodate)
            Dim Userparsejson As JObject = JObject.Parse(json)

            Dim Msg = Userparsejson("Success").ToString
            If Msg.ToString = "True" Then
                dialog.Caption = "Data Received.."
                ds = Userparsejson("Data").ToObject(Of DataTable)()
                If ds.Rows.Count > 0 Then
                    GridControl1.DataSource = ds.DefaultView
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

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            If GridView1.FocusedColumn.ColumnHandle = 9 Then
                'MessageBox.Show("Te")
                Dim res As String = ""
                Dim selecindex As Integer = GridView1.GetFocusedRowCellValue("PayeeStatus")
                frmstatusoption.ShowDialog(selecindex)
                If frmstatusoption.DialogResult = Windows.Forms.DialogResult.OK Then
                    res = frmstatusoption.RadioGroup1.SelectedIndex
                    Dim fid = GridView1.GetFocusedRowCellValue("Id")
                    If res = 3 Then
                        If _JsonSendChq(M_Details.LinkAjaxRequestCheque & "AjaxRequest=14&id=" & fid & "&res=" & res & "&UserId=" & _companyInfo.UserId) = True Then
                            _DataLoad()
                        End If
                    Else
                        If _JsonSendChq(M_Details.LinkAjaxRequestCheque & "AjaxRequest=11&id=" & fid & "&res=" & res & "&UserId=" & _companyInfo.UserId) = True Then
                            _DataLoad()
                        End If
                    End If


                End If
            Else

                'UpdateBankTableSave(fid, res, "er")
                'GridView1.UpdateCurrentRow()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function UpdateBankTableSave(ByVal caption As String, ByVal value As String, ByRef ErrorMsg As String) As Boolean
        Try

            Dim dtrow As Data.EnumerableRowCollection(Of DataRow) = From dtrows As DataRow In ChqJson.BankTableSave Where dtrows("Id") = caption

            If dtrow.Any Then
                dtrow(0)("PayeeStatus") = value
            Else
                ChqJson.BankTableSave.Rows.Add(caption, value)
            End If

            ChqJson.BankTableSave.AcceptChanges()
            ChqJson.BankTableSave.WriteXml(M_Details.AppPath & "\Settings\BankTableSave.xml")
            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return False
        End Try
    End Function
    Private Sub _DataLoad()
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            dialog.Caption = "Connecting To Database"
            getBankSatement()
            dialog.Caption = "Collecting Data From Server"
            If ChqJson.BankTableSave.Rows.Count > 0 Then
                dialog.Caption = "Data Received.."
                GridControl1.DataSource = ChqJson.BankTableSave
            Else
                GridControl1.DataSource = Nothing
            End If
        Catch ex As Exception
            GridControl1.DataSource = Nothing
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub

    Private Sub barbtnexportxls_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnexportxls.ItemClick
        Try
            GridView1.ShowRibbonPrintPreview()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub barbtnrefere_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnrefere.ItemClick
        Try
            _DataLoad()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PrintPreview_Click(sender As Object, e As EventArgs) Handles PrintPreview.Click
        Try
            
            Dim ds As New DataSet
            Dim payeeamount As String = ""
                Dim payeename = GridView1.GetFocusedRowCellValue("PayeeName")
            Dim payeedate = GridView1.GetFocusedRowCellValue("PayeeDate")
            Dim payeemode = GridView1.GetFocusedRowCellValue("PayeeMode")
            If payeemode = "Out" Then
                payeeamount = GridView1.GetFocusedRowCellValue("PayeeAmountDr")
            Else
                payeeamount = GridView1.GetFocusedRowCellValue("PayeeAmountCr")
            End If

                Dim payeewords = NumberInWords(payeeamount)
                ChqJson.BankTableStatement.Rows.Clear()
                ChqJson.BankTableStatement.BeginInit()
                Dim datformart As String = ""
                Dim refdatformart As String = ""
                _DateConversion(payeedate, datformart)
                _Addspace(datformart, refdatformart)
                ChqJson.BankTableStatement.Rows.Add(Nothing, "-", "**" & payeename & "**", refdatformart, "**" & payeeamount & "**", "**" & payeewords & "**")
                ChqJson.BankTableStatement.EndInit()
                ChqJson.BankTableStatement.AcceptChanges()
                If File.Exists(M_Details.AppPath & "\Settings\BankTableStatement.xml") Then
                    ChqJson.BankTableStatement.WriteXml(M_Details.AppPath & "\Settings\BankTableStatement.xml")
                End If
                ds.Merge(ChqJson.BankTableStatement)
                If frmChequeprint.Gen_Report(ds) = True Then
                    ds.Tables.Clear()
                End If


        Catch ex As Exception

        End Try
    End Sub
     
End Class