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
            chkallowitemdiscount.CheckState = CheckState.Checked
            chkallowmultipleprice.CheckState = CheckState.Checked
            chkallownegativestock.CheckState = CheckState.Checked
            btnsave.Text = "Save"
            txtitemname.Select()
            txtbusinesstype.EditValue = 70
            txttaxtype.EditValue = 1
            txtcompany.EditValue = 1
            txtlocation.EditValue = 1
            txtopeningstock.EditValue = 0.0
            txtminprice.EditValue = 0.0
            txtmaxprice.EditValue = 0.0
            txtposition.Text = "0"
            colorEdit1.EditValue = Color.LightBlue
            _ClearMultiplePriceGrid()
            _ClearPriceInputs()
            _DataLoad()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _ClearMultiplePriceGrid()
        Try
            ' Clear the multiple price grid
            GridControlMultiplePrice.DataSource = Nothing
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _LoadMultiplePricesForItem(itemId As String)
        Try
            ' Load multiple prices from server
            _LoadMultiplePricesFromServer(itemId)
        Catch ex As Exception
            _ClearMultiplePriceGrid()
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
            If txtposition.Text = "" OrElse txtposition.EditValue Is Nothing Then
                txtposition.Properties.Appearance.BackColor = Color.Red
                Exit Sub
            Else
                txtposition.Properties.Appearance.BackColor = Color.White
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
                itemdata.itemminprice = txtminprice.EditValue
                itemdata.itemmaxprice = txtmaxprice.EditValue
                itemdata.itemallowdiscount = chkallowitemdiscount.CheckState
                itemdata.itemallownegativestock = chkallownegativestock.CheckState
                itemdata.itemallowmultipleprice = chkallowmultipleprice.CheckState
                itemdata.itemcolor = ColorTranslator.ToHtml(colorEdit1.Color)
                itemdata.itemposition = txtposition.Text
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
                itemdata.itemminprice = txtminprice.EditValue
                itemdata.itemmaxprice = txtmaxprice.EditValue
                itemdata.itemallowdiscount = chkallowitemdiscount.CheckState
                itemdata.itemallownegativestock = chkallownegativestock.CheckState
                itemdata.itemallowmultipleprice = chkallowmultipleprice.CheckState
                itemdata.itemcolor = ColorTranslator.ToHtml(colorEdit1.Color)
                itemdata.itemposition = txtposition.Text
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
            Dim minsell = GridView1.GetFocusedRowCellValue("MinPrice")
            Dim maxsell = GridView1.GetFocusedRowCellValue("MaxPrice")
            Dim cost = GridView1.GetFocusedRowCellValue("CostPrice")
            Dim comp = GridView1.GetFocusedRowCellValue("CompanyName")
            Dim loc = GridView1.GetFocusedRowCellValue("LocationName")
            Dim chk = GridView1.GetFocusedRowCellValue("Active")
            Dim chkdiscount = GridView1.GetFocusedRowCellValue("AllowDiscount")
            Dim chknegstock = GridView1.GetFocusedRowCellValue("AllowNegStock")
            Dim chkmultiprice = GridView1.GetFocusedRowCellValue("AllowMultiPrice")
            Dim color = GridView1.GetFocusedRowCellValue("Color")
            Dim position = GridView1.GetFocusedRowCellValue("Positioin")
            If chk = 1 Then
                chkactive.CheckState = CheckState.Checked
            Else
                chkactive.CheckState = CheckState.Unchecked
            End If
            If chkdiscount = 1 Then
                chkallowitemdiscount.CheckState = CheckState.Checked
            Else
                chkallowitemdiscount.CheckState = CheckState.Unchecked
            End If
            If chknegstock = 1 Then
                chkallownegativestock.CheckState = CheckState.Checked
            Else
                chkallownegativestock.CheckState = CheckState.Unchecked
            End If
            If chkmultiprice = 1 Then
                chkallowmultipleprice.CheckState = CheckState.Checked
            Else
                chkallowmultipleprice.CheckState = CheckState.Unchecked
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
            txtminprice.EditValue = minsell
            txtmaxprice.EditValue = maxsell
            txtbusinesstype.Text = "Retail"
            txtcompany.Text = comp
            txtlocation.Text = loc
            txtopeningstock.EditValue = 0
            txtposition.Text = position
            If color IsNot Nothing AndAlso color.ToString() <> "" Then
                Try
                    colorEdit1.EditValue = ColorTranslator.FromHtml(color.ToString())
                Catch
                    colorEdit1.EditValue = color.LightBlue
                End Try
            Else
                colorEdit1.EditValue = color.LightBlue
            End If
            ' Load multiple prices for this item
            _LoadMultiplePricesForItem(id.ToString())
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
            _EnableMultiplePriceButtons()
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

    Private Sub chkallowmultipleprice_CheckStateChanged(sender As Object, e As EventArgs) Handles chkallowmultipleprice.CheckStateChanged
        _EnableMultiplePriceButtons()
    End Sub


    Private Sub btnmultipleadd_Click(sender As Object, e As EventArgs) Handles btnmultipleadd.Click
        Try
            ' Use the same save functionality as btnSavePrice_Click
            btnSavePrice_Click()
        Catch ex As Exception
            MessageBox.Show("Error adding multiple price: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnmultipledelete_Click(sender As Object, e As EventArgs) Handles btnmultipledelete.Click
        Try
            ' Check if any row is selected
            If GridView2.FocusedRowHandle < 0 Then
                MessageBox.Show("Please select a row to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' Get the selected row data
            Dim selectedPriceId As String = GridView2.GetFocusedRowCellValue("Id").ToString()
            Dim selectedPriceName As String = GridView2.GetFocusedRowCellValue("Name").ToString()

            ' Confirm deletion
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete the price '" & selectedPriceName & "'?",
                                                       "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then
                Exit Sub
            End If

            ' Delete from server
            _DeleteMultiplePriceFromServer(selectedPriceId)

        Catch ex As Exception
            MessageBox.Show("Error deleting multiple price: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function _GetMultiplePricesData() As List(Of multiprice)
        Dim multiplePricesList As New List(Of multiprice)()
        Try
            If GridControlMultiplePrice.DataSource IsNot Nothing Then
                Dim dt As DataTable = DirectCast(GridControlMultiplePrice.DataSource, DataTable)
                For Each row As DataRow In dt.Rows
                    If row.RowState <> DataRowState.Deleted Then
                        Dim priceItem As New multiprice()
                        priceItem.itemid = txtitemid.Text
                        priceItem.itempriceid = row("Id").ToString()
                        priceItem.itempricename = row("Name").ToString()
                        priceItem.itemprice = row("Price").ToString()
                        multiplePricesList.Add(priceItem)
                    End If
                Next
            End If
        Catch ex As Exception
            ' Return empty list on error
        End Try
        Return multiplePricesList
    End Function

    Private Sub _EnableMultiplePriceButtons()
        Try
            ' Enable/disable multiple price buttons based on allow multiple price setting
            Dim allowMultiplePrice As Boolean = chkallowmultipleprice.CheckState = CheckState.Checked
            btnmultipleadd.Enabled = allowMultiplePrice
            btnmultipledelete.Enabled = allowMultiplePrice
            GridControlMultiplePrice.Enabled = allowMultiplePrice
        Catch ex As Exception

        End Try
    End Sub

    ' Variable to track if we're editing an existing price
    Private editingPriceId As String = ""

    Private Sub btnSavePrice_Click()
        Try
            ' Validate that an item is selected
            If String.IsNullOrEmpty(txtitemid.Text) Then
                MessageBox.Show("Please select an item first.", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Validate price name from textbox
            If String.IsNullOrEmpty(txtpricename.Text.Trim()) Then
                txtpricename.Properties.Appearance.BackColor = Color.Red
                MessageBox.Show("Please enter a price name.", "Price Name Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtpricename.Focus()
                Exit Sub
            Else
                txtpricename.Properties.Appearance.BackColor = Color.White
            End If

            ' Validate price value from textbox
            Dim priceAmount As Decimal = 0
            If Not Decimal.TryParse(txtmultipleprice.Text, priceAmount) OrElse priceAmount <= 0 Then
                txtmultipleprice.Properties.Appearance.BackColor = Color.Red
                MessageBox.Show("Please enter a valid price value.", "Invalid Price", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtmultipleprice.Focus()
                Exit Sub
            Else
                txtmultipleprice.Properties.Appearance.BackColor = Color.White
            End If

            ' Validate price against selling price, min price, and max price
            Dim sellPrice As Decimal = If(txtsellprice.EditValue IsNot Nothing, Convert.ToDecimal(txtsellprice.EditValue), 0)
            Dim minPrice As Decimal = If(txtminprice.EditValue IsNot Nothing, Convert.ToDecimal(txtminprice.EditValue), 0)
            Dim maxPrice As Decimal = If(txtmaxprice.EditValue IsNot Nothing, Convert.ToDecimal(txtmaxprice.EditValue), 0)

            ' Check if multiple price is greater than selling price
            If sellPrice > 0 AndAlso priceAmount > sellPrice Then
                txtmultipleprice.Properties.Appearance.BackColor = Color.Red
                MessageBox.Show("Multiple price (" & priceAmount.ToString("F2") & ") cannot be greater than selling price (" & sellPrice.ToString("F2") & ").", "Price Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtmultipleprice.Focus()
                Exit Sub
            End If

            ' Check if multiple price is less than minimum price (if min price is set)
            If minPrice > 0 AndAlso priceAmount < minPrice Then
                txtmultipleprice.Properties.Appearance.BackColor = Color.Red
                MessageBox.Show("Multiple price (" & priceAmount.ToString("F2") & ") cannot be less than minimum price (" & minPrice.ToString("F2") & ").", "Price Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtmultipleprice.Focus()
                Exit Sub
            End If

            ' Check if multiple price is greater than maximum price (if max price is set)
            If maxPrice > 0 AndAlso priceAmount > maxPrice Then
                txtmultipleprice.Properties.Appearance.BackColor = Color.Red
                MessageBox.Show("Multiple price (" & priceAmount.ToString("F2") & ") cannot be greater than maximum price (" & maxPrice.ToString("F2") & ").", "Price Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtmultipleprice.Focus()
                Exit Sub
            End If

            ' Create multiprice object
            Dim priceData As New multiprice()
            priceData.itemid = txtitemid.Text
            priceData.itempricename = txtpricename.Text.Trim()
            priceData.itemprice = priceAmount.ToString()

            ' Determine if we're updating or inserting
            Dim ajaxRequestType As String
            Dim operationType As String

            If Not String.IsNullOrEmpty(editingPriceId) Then
                ' Updating existing price
                priceData.itempriceid = editingPriceId
                ajaxRequestType = "64" ' Update
                operationType = "updated"
            Else
                ' Inserting new price
                ajaxRequestType = "63" ' Insert
                operationType = "saved"
            End If

            ' Serialize to JSON
            Dim PostString As String = JsonConvert.SerializeObject(priceData)

            ' Save/Update to backend
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=" & ajaxRequestType & "&json=" & Uri.EscapeDataString(PostString))
            Dim responseObj As JObject = JObject.Parse(response)

            If responseObj("Success").ToObject(Of Boolean)() = True Then
                MessageBox.Show("Multiple price " & operationType & " successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Clear input fields and reset editing state
                txtpricename.Text = ""
                txtmultipleprice.EditValue = 0.0
                editingPriceId = ""
                txtpricename.Focus()

                ' Reload multiple prices for this item
                _LoadMultiplePricesFromServer(txtitemid.Text)
            Else
                MessageBox.Show("Failed to " & operationType.Replace("d", "") & " multiple price: " & responseObj("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error saving multiple price: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub _LoadMultiplePricesFromServer(itemId As String)
        Try
            If Not String.IsNullOrEmpty(itemId) Then
                ' Load multiple prices from server using AjaxRequest 66
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim response As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=66&itemid=" & itemId)
                Dim responseObj As JObject = JObject.Parse(response)

                If responseObj("Success").ToObject(Of Boolean)() = True Then
                    Dim multiplePricesData As JArray = responseObj("Data")

                    ' Create DataTable for grid
                    Dim dt As New DataTable()
                    dt.Columns.Add("Id", GetType(Integer))
                    dt.Columns.Add("RefId", GetType(Integer))
                    dt.Columns.Add("Name", GetType(String))
                    dt.Columns.Add("Price", GetType(Decimal))

                    ' Populate DataTable with server data
                    For Each priceItem In multiplePricesData
                        Dim newRow As DataRow = dt.NewRow()
                        newRow("Id") = priceItem("Id").ToObject(Of Integer)()
                        newRow("RefId") = priceItem("RefId").ToObject(Of Integer)()
                        newRow("Name") = priceItem("Name").ToString()
                        newRow("Price") = priceItem("Price").ToObject(Of Decimal)()
                        dt.Rows.Add(newRow)
                    Next

                    GridControlMultiplePrice.DataSource = dt
                Else
                    ' No data found, create empty table
                    Dim dt As New DataTable()
                    dt.Columns.Add("Id", GetType(Integer))
                    dt.Columns.Add("RefId", GetType(Integer))
                    dt.Columns.Add("Name", GetType(String))
                    dt.Columns.Add("Price", GetType(Decimal))
                    GridControlMultiplePrice.DataSource = dt
                End If
            Else
                _ClearMultiplePriceGrid()
            End If
        Catch ex As Exception
            _ClearMultiplePriceGrid()
        End Try
    End Sub

    Private Sub _DeleteMultiplePriceFromServer(priceId As String)
        Try
            If Not String.IsNullOrEmpty(priceId) Then
                ' Delete from server using AjaxRequest 65
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim response As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "AjaxRequest=65&priceid=" & priceId)
                Dim responseObj As JObject = JObject.Parse(response)

                If responseObj("Success").ToObject(Of Boolean)() = True Then
                    MessageBox.Show("Multiple price deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Reload multiple prices for this item
                    _LoadMultiplePricesFromServer(txtitemid.Text)
                Else
                    MessageBox.Show("Failed to delete multiple price: " & responseObj("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error deleting multiple price: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView2_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView2.RowClick
        Try
            ' Load selected price data into input fields for editing
            If GridView2.FocusedRowHandle >= 0 Then
                Dim selectedPriceId As String = GridView2.GetFocusedRowCellValue("Id").ToString()
                Dim selectedPriceName As String = GridView2.GetFocusedRowCellValue("Name").ToString()
                Dim selectedPriceValue As Decimal = Convert.ToDecimal(GridView2.GetFocusedRowCellValue("Price"))

                ' Set values for editing
                editingPriceId = selectedPriceId
                txtpricename.Text = selectedPriceName
                txtmultipleprice.EditValue = selectedPriceValue
                txtpricename.Focus()
            End If
        Catch ex As Exception
            ' Handle any errors silently
        End Try
    End Sub

    Private Sub _ClearPriceInputs()
        Try
            ' Clear price input fields and reset editing state
            txtpricename.Text = ""
            txtmultipleprice.EditValue = 0.0
            editingPriceId = ""
            txtpricename.Focus()
        Catch ex As Exception

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
    Public Property itemminprice As String
    Public Property itemmaxprice As String
    Public Property itemallowdiscount As String
    Public Property itemallownegativestock As String
    Public Property itemallowmultipleprice As String
    Public Property itemcolor As String
    Public Property itemposition As String
End Class
Public Class multiprice
    Public Property itemid As String
    Public Property itempriceid As String
    Public Property itempricename As String
    Public Property itemprice As String
End Class
