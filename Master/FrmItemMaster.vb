Imports Newtonsoft.Json
Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class FrmItemMaster

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Me.Close()
    End Sub
    Private Sub _Clear()
        Try
            txtitemid.Text = ""
            txtitemname.Text = ""
            txtitembarcode.Text = ""
            txtremarks.Text = "-"
            txtbusinesstype.EditValue = 1
            txttaxtype.EditValue = 1
            txtmaingroup.EditValue = 1
            txtsubgroup.EditValue = 1
            txtcostprice.EditValue = 0.0
            txtsellprice.EditValue = 0.0
            txtcompany.EditValue = _companyInfo.ComId
            txtlocation.EditValue = _companyInfo.LocId
            chkactive.CheckState = CheckState.Checked
            btnsave.Text = "Save"
            txtitemname.Select()
            txtbusinesstype.EditValue = 70
            txttaxtype.EditValue = 1
            txtcompany.EditValue = 1
            txtlocation.EditValue = 1
            txtopeningstock.EditValue = 0.0

            _DataLoad()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        Try
            _Clear()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            If txtitemname.Text = "" OrElse txtitemname.EditValue Is Nothing Then
                txtitemname.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtitemname.Properties.Appearance.BackColor = Color.White
            End If
            If txtitembarcode.Text = "" OrElse txtitembarcode.EditValue Is Nothing Then
                txtitembarcode.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtitembarcode.Properties.Appearance.BackColor = Color.White
            End If
            If txtremarks.Text = "" OrElse txtremarks.EditValue Is Nothing Then
                txtremarks.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtremarks.Properties.Appearance.BackColor = Color.White
            End If
            If txtbusinesstype.Text = "" OrElse txtitemname.EditValue Is Nothing Then
                txtbusinesstype.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtbusinesstype.Properties.Appearance.BackColor = Color.White
            End If
            If txttaxtype.Text = "" OrElse txttaxtype.EditValue Is Nothing Then
                txttaxtype.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txttaxtype.Properties.Appearance.BackColor = Color.White
            End If
            If txtmaingroup.Text = "" OrElse txtmaingroup.EditValue Is Nothing Then
                txtmaingroup.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtmaingroup.Properties.Appearance.BackColor = Color.White
            End If
            If txtsubgroup.Text = "" OrElse txtsubgroup.EditValue Is Nothing Then
                txtsubgroup.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtsubgroup.Properties.Appearance.BackColor = Color.White
            End If
            If txtcompany.Text = "" OrElse txtcompany.EditValue Is Nothing Then
                txtcompany.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtcompany.Properties.Appearance.BackColor = Color.White
            End If
            If txtlocation.Text = "" OrElse txtlocation.EditValue Is Nothing Then
                txtlocation.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtlocation.Properties.Appearance.BackColor = Color.White
            End If
            Dim itemdata As New itemmaster
            If btnsave.Text = "Save" Then
                dialog.Caption = "Connecting To Server"
                itemdata.itemid = 0
                itemdata.itemname = txtitemname.Text
                itemdata.itembarcode = txtitembarcode.Text
                itemdata.itemremakrs = txtremarks.Text
                itemdata.itembusinesstypeid = txtbusinesstype.EditValue
                itemdata.itemtaxtypeid = txttaxtype.EditValue
                itemdata.itemmaingroupid = txtmaingroup.EditValue
                itemdata.itemsubgroupid = txtsubgroup.EditValue
                itemdata.itemsellprice = txtsellprice.EditValue
                itemdata.itemcostprice = txtcostprice.EditValue
                itemdata.itemcompid = txtcompany.EditValue
                itemdata.itemlocid = txtlocation.EditValue
                itemdata.itemactive = chkactive.CheckState
                itemdata.itemopstock = txtopeningstock.EditValue
                Dim PostString As String = JsonConvert.SerializeObject(itemdata)
                If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=23&json=" & PostString) = True Then
                    dialog.Caption = "Data Saved Success.."
                    _Clear()
                End If
            Else
                dialog.Caption = "Connecting To Server"
                itemdata.itemid = txtitemid.Text
                itemdata.itemname = txtitemname.Text
                itemdata.itembarcode = txtitembarcode.Text
                itemdata.itemremakrs = txtremarks.Text
                itemdata.itembusinesstypeid = txtbusinesstype.EditValue
                itemdata.itemtaxtypeid = txttaxtype.EditValue
                itemdata.itemmaingroupid = txtmaingroup.EditValue
                itemdata.itemsubgroupid = txtsubgroup.EditValue
                itemdata.itemsellprice = txtsellprice.EditValue
                itemdata.itemcostprice = txtcostprice.EditValue
                itemdata.itemcompid = txtcompany.EditValue
                itemdata.itemlocid = txtlocation.EditValue
                itemdata.itemactive = chkactive.CheckState
                itemdata.itemopstock = txtopeningstock.EditValue
                Dim PostString As String = JsonConvert.SerializeObject(itemdata)
                If _JsonSend(M_Details.LinkAjaxRequest & "AjaxRequest=24&json=" & PostString) = True Then
                    dialog.Caption = "Data Saved Success.."
                    _Clear()
                End If
            End If
        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            btnsave.Text = "Update"
            Dim id = GridView1.GetFocusedRowCellValue("Id")
            Dim itembarcode = GridView1.GetFocusedRowCellValue("BarCode")
            Dim itemname = GridView1.GetFocusedRowCellValue("ItemName")
            Dim remarks = GridView1.GetFocusedRowCellValue("Remarks")
            Dim tax = GridView1.GetFocusedRowCellValue("TaxName")
            Dim main = GridView1.GetFocusedRowCellValue("MainName")
            Dim subg = GridView1.GetFocusedRowCellValue("CateName")
            Dim sell = GridView1.GetFocusedRowCellValue("SellPrice")
            Dim cost = GridView1.GetFocusedRowCellValue("CostPrice")
            Dim comp = GridView1.GetFocusedRowCellValue("CompanyName")
            Dim loc = GridView1.GetFocusedRowCellValue("LocationName")
            Dim chk = GridView1.GetFocusedRowCellValue("Active")
            If chk = 1 Then
                chkactive.CheckState = CheckState.Checked
            Else
                chkactive.CheckState = CheckState.Unchecked
            End If
            txtitemid.Text = id
            txtitemname.Text = itemname
            txtitembarcode.Text = itembarcode
            txtremarks.Text = remarks
            txttaxtype.Text = tax
            txtmaingroup.Text = main
            txtsubgroup.Text = subg
            txtcostprice.EditValue = cost
            txtsellprice.EditValue = sell
            txtbusinesstype.Text = "Retail"
            txtcompany.Text = comp
            txtlocation.Text = loc
            txtopeningstock.EditValue = 0
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _DataLoad()
        Try
            Dim ItemTable As DataTable
            ItemTable = New DataTable
            ItemTable.TableName = "ItemTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=27")
            Dim Userparsejson As JObject = JObject.Parse(json)
            ItemTable = Userparsejson("Data").ToObject(Of DataTable)()
            If ItemTable.Rows.Count > 0 Then
                Dim dtrows As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In ItemTable Where dtrow("LocationName") = _companyInfo.LocationName
                If dtrows.Any Then
                    GridControl1.DataSource = dtrows.CopyToDataTable
                    txtitembarcode.Text = ItemTable.Rows.Count + 1
                End If
            Else
                GridControl1.DataSource = Nothing
            End If
        Catch ex As Exception
            GridControl1.DataSource = Nothing
        End Try
    End Sub
    Private Sub _DataLoadAll()
        Try
            'BusinessType
            Dim businessTable As DataTable
            businessTable = New DataTable
            businessTable.TableName = "BusinessTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=26&groupid=012")
            Dim Userparsejson As JObject = JObject.Parse(json)
            businessTable = Userparsejson("Data").ToObject(Of DataTable)()
            If businessTable.Rows.Count > 0 Then
                txtbusinesstype.Properties.DataSource = businessTable.DefaultView
            End If
            'TaxType
            Dim TaxTable As DataTable
            TaxTable = New DataTable
            TaxTable.TableName = "TaxTable"
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim jsonTaxTable As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=12")
            Dim UserparsejsonTaxTable As JObject = JObject.Parse(jsonTaxTable)
            TaxTable = UserparsejsonTaxTable("Data").ToObject(Of DataTable)()
            If TaxTable.Rows.Count > 0 Then
                txttaxtype.Properties.DataSource = TaxTable.DefaultView
            End If
            'CompanyInfo
            'getComapnyLocationInfo()
            'If _JsonData.CompanyTable.Rows.Count > 0 Then
            '    txtcompany.Properties.DataSource = _JsonData.CompanyLocationTable.Select("Active = 1").CopyToDataTable
            '    txtlocation.Properties.DataSource = _JsonData.CompanyLocationTable.Select("Active = 1").CopyToDataTable
            'End If
            If getComapnyInfo() = True Then
                If _JsonData.CompanyTable.Rows.Count > 0 Then
                    txtcompany.Properties.DataSource = _JsonData.CompanyTable
                End If
            End If
            If getLocationInfo() = True Then
                If _JsonData.LocationTable.Rows.Count > 0 Then
                    txtlocation.Properties.DataSource = _JsonData.LocationTable
                End If
            End If
            getMainMaster()
            If _JsonData.MainGroupTable.Rows.Count > 0 Then
                txtmaingroup.Properties.DataSource = _JsonData.MainGroupTable.DefaultView
            End If
            _Clear()
        Catch ex As Exception

        End Try
    End Sub
     
    Private Sub FrmItemMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            _DataLoadAll()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtmaingroup_EditValueChanged(sender As Object, e As EventArgs) Handles txtmaingroup.EditValueChanged
        Try
            Dim mainid As Integer = txtmaingroup.EditValue
            getCategoryMaster()
            If _JsonData.CategoryTable.Rows.Count > 0 Then
                Dim datTable As DataTable = _JsonData.CategoryTable.Select("MainId = " & mainid).CopyToDataTable()
                If datTable.Rows.Count > 0 Then
                    txtsubgroup.Properties.DataSource = datTable
                Else
                    txtsubgroup.Properties.DataSource = Nothing
                End If
            End If
        Catch ex As Exception
            txtsubgroup.Properties.DataSource = Nothing
        End Try
    End Sub


End Class

Public Class itemmaster
    Public Property itemid As String
    Public Property itemname As String
    Public Property itembarcode As String
    Public Property itemremakrs As String
    Public Property itembusinesstypeid As String
    Public Property itemtaxtypeid As String
    Public Property itemmaingroupid As String
    Public Property itemsubgroupid As String
    Public Property itemcostprice As String
    Public Property itemsellprice As String
    Public Property itemcompid As String
    Public Property itemlocid As String
    Public Property itemactive As String
    Public Property itemopstock As String
End Class