Imports System.Net
Imports Newtonsoft.Json.Linq
Imports System.IO

Public Class frmSelectBill
    Dim SaleTabale As DataSet
    Dim dats As String = ""
    Dim Errstr As String = ""
    Private Sub frmSelectBill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SaleTabale = New DataSet
            _DateConversion(Date.Now, dats)
            If GetAllSales(dats, SaleTabale) = True Then
                GridControlGRNSelector.DataSource = SaleTabale.Tables(0)
            Else
                GridControlGRNSelector.DataSource = Nothing
            End If
            If _printProfileLoad() = False Then

            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function _printProfileLoad() As Boolean
        Try
            If File.Exists(M_Details._appPath & "\Settings\PrintProfileSetting.xml") Then
                Dim str() As String = {"Sales"}
                Dim _dsPrintProfile As New DataSet
                _dsPrintProfile.ReadXml(M_Details._appPath & "\Settings\PrintProfileSetting.xml")
                Dim sas = From profile In _dsPrintProfile.Tables(0).AsEnumerable Where profile.Field(Of String)("ProfileType") = "Sales" Select profile

                Dim tabl As New DataTable
                If sas.Count > 0 Then
                    txtprintprofile.Properties.Items.Clear()
                    tabl = sas.CopyToDataTable
                    For Each _Drows As DataRow In tabl.Rows
                        txtprintprofile.Properties.Items.Add(_Drows("FileName"))
                    Next
                End If
                txtprintprofile.SelectedIndex = 0
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    
    'Public Function GetSalesQuoteBill(ByVal getdate As String) As Boolean
    '    Try
    '        SaleTabale.TableName = "SaleTabale"
    '        SaleTabale.Rows.Clear()
    '        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
    '        Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesRequest=11&date=" & getdate)
    '        Dim Userparsejson As JObject = JObject.Parse(json)
    '        SaleTabale = Userparsejson("GetSalesBill").ToObject(Of DataTable)()
    '        If SaleTabale.Rows.Count > 0 Then
    '            Dim dtrows As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In SaleTabale Where dtrow("COMID") = _companyInfo.ComId And dtrow("LOCID") = _companyInfo.LocId
    '            If dtrows.Any Then
    '                GridControlGRNSelector.DataSource = dtrows.CopyToDataTable
    '            End If
    '        End If
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function
    Private Sub btnget_Click(sender As Object, e As EventArgs) Handles btnget.Click
        Try
            _DateConversion(txtdatetimer.Text, dats)
            If GetAllSales(dats, SaleTabale) = False Then
                GridControlGRNSelector.DataSource = SaleTabale.Tables(0)
            Else
                GridControlGRNSelector.DataSource = Nothing
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmSelectbill_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then 'My.Computer.Keyboard.CtrlKeyDown AndAlso 
                G_SalID = SelectRowValue()
                Me.Close()
                Me.DialogResult = Windows.Forms.DialogResult.OK
            ElseIf e.KeyCode = Keys.Escape Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub
    Private Function SelectRowValue() As Integer
        Try
            Dim i As Integer = 0
            Dim j As Integer = 0
            i = GridView1.FocusedRowHandle
            j = GridView1.GetRowCellValue(i, "Sal_ID")
            Return j
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Try
            G_SalID = SelectRowValue()
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btna4print_Click(sender As Object, e As EventArgs) Handles btna4print.Click
        Try
            Dim _receDs As New DataSet
            'If _globalSetting.QuoteBill = True Then
            '    If (GetSalesByQuoteBill(GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "Sal_BillNo"), _receDs)) = True Then
            '        If (_receDs.Tables(0).Rows.Count > 0) Then
            '            _receDs.WriteXml(M_Details._appPath & "\Reports\Sales.xml", Data.XmlWriteMode.WriteSchema)
            '            _globalSetting.QuoteBill = False
            '        End If
            '        If clsBillPrint.Billa4Print(_receDs, "er", txtprintprofile.Text) = False Then

            '        End If
            '    End If
            'Else
            If (GetSalesByBillLocal(GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "Sal_BillNo"), _receDs, "P")) = True Then
                If (_receDs.Tables(0).Rows.Count > 0) Then
                    _receDs.WriteXml(M_Details._appPath & "\Reports\Sales.xml", Data.XmlWriteMode.WriteSchema)
                End If
                If clsBillPrint.Billa4Print(_receDs, "er", txtprintprofile.Text) = False Then

                End If
            End If
            'End If

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub btngetquotebill_Click(sender As Object, e As EventArgs) Handles btngetquotebill.Click
    '    Try
    '        _globalSetting.QuoteBill = True
    '        _DateConversion(txtdatetimer.Text, txtdatetimer.Text, dats)
    '        If GetSalesQuoteBill(dats) = False Then

    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim _receDs As New DataSet
            If (GetSalesByBillLocal(GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "Sal_BillNo"), _receDs, "P")) = True Then
                If (_receDs.Tables(0).Rows.Count > 0) Then
                    _receDs.WriteXml(M_Details._appPath & "\Reports\Sales.xml", Data.XmlWriteMode.WriteSchema)
                End If

                If clsBillPrint.BillPrintMin(_receDs, Errstr, txtprintprofile.Text) = False Then

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            Dim _receDs As New DataSet
            GetSalesByBillLocal(GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "Sal_BillNo"), _receDs, "P")
            If _receDs.Tables(0).Rows.Count > 0 Then
                GridControl1.DataSource = _receDs.Tables(1)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class