Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.DataTable
Imports System.Drawing.Printing
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports BarcodeLib
Imports DevExpress.XtraEditors
 
Public Class frmBarcodeMateriallabel
    Dim _DataSet As DataSet

    Private _dsM As DataTable
    Private Errstr As String = String.Empty
    Private Barcode As String = String.Empty
    Private btnAll As New DevExpress.Utils.Menu.DXMenuItem
    Private btnUnAll As New DevExpress.Utils.Menu.DXMenuItem
    Private btnSelectAll As New DevExpress.Utils.Menu.DXMenuItem
    Private btnSelectUnAll As New DevExpress.Utils.Menu.DXMenuItem
    Private _M_Trid As Integer = 0
    Private _dsMM As DataSet
    Dim info As New ProcessStartInfo()
    Dim G_BarcodeName As String = String.Empty
    Dim _labelSettings As LabelSettings
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        AddHandler btnAll.Click, AddressOf AllrecordSelected
        AddHandler btnUnAll.Click, AddressOf AllrecordUnSelected

        AddHandler btnSelectAll.Click, AddressOf SelectedSelectRecord
        AddHandler btnSelectUnAll.Click, AddressOf SelectedUnSelectRecord

 
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private dtview As New DataView
    Private Sub cmbMaterialSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbMaterialSearch.KeyDown
        Try
            Dim Qty As Decimal = 1.0
            Dim errstr As String = String.Empty

            If e.KeyCode = Keys.Enter Then

                If String.IsNullOrWhiteSpace(cmbMaterialSearch.Text.ToString) Then
                    cmbMaterialSearch.ShowPopup()
                    txtnooflabel.EditValue = 1
                    txtsearch2.Text = ""
                    txtsearch2.SelectAll()
                    Exit Sub
                Else
                    Dim str As String
                    str = cmbMaterialSearch.Text
                    If barbtnsearchbybarcode.Checked = True Then
                        If Get_product_info(str, Qty, "BarcodeSplitQty", errstr) = False Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else

                            cmbMaterialSearch.Focus()

                            txtnooflabel.Focus()
                        End If
                    Else
                        If Get_product_info(str, Qty, "ItemCodeSplitQty", errstr) = False Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else

                            cmbMaterialSearch.Focus()

                            txtnooflabel.Focus()
                        End If
                    End If

                End If
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
                'ClientPosMdi.Show()
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub
    Private Sub txtsearch2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtsearch2.KeyDown
        Try
            If My.Computer.Keyboard.CtrlKeyDown AndAlso e.KeyCode = Keys.Down Then
                GridView2.Focus()
            ElseIf e.KeyCode = Keys.Down Then
                GridView2.FocusedRowHandle = 0
                GridView2.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub



    Private Sub txtsearch2_KeyUp(sender As Object, e As KeyEventArgs) Handles txtsearch2.KeyUp
        Try
            If cmbMaterialSearch.IsPopupOpen Then
                cmbMaterialSearch.EditValue = txtsearch2.EditValue
                If cmbMaterialSearch.Text <> "" Then

                    cmbMaterialSearch.EditValue = txtsearch2.EditValue
                    If cmbMaterialSearch.Text <> "" Then
                        dtview.RowFilter = ("ITEMNAME like '%" & cmbMaterialSearch.EditValue & "%'")
                        GridControl2.DataSource = dtview
                    Else
                        dtview.RowFilter = ("")
                        GridControl2.DataSource = dtview
                    End If
                Else
                    dtview.RowFilter = ("")
                    GridControl2.DataSource = dtview
                End If
            End If
        Catch ex As Exception
            GridControl2.DataSource = Nothing
        End Try
    End Sub

    Private Sub cmbMaterialSearch_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmbMaterialSearch.QueryPopUp
        txtsearch2.Focus()
    End Sub

    Private Sub cmbMaterialSearch_QueryCloseUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmbMaterialSearch.QueryCloseUp
        txtsearch2.Focus()
    End Sub
    Private Sub GridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView2.KeyDown
        Try

            If e.KeyCode = Keys.Enter Then
                txtsearch2.Text = ""
                Dim _productCode As Integer = 0
                Dim errstr As String = String.Empty
                _productCode = GridView2.GetRowCellValue(GridView2.FocusedRowHandle, "ITEMCODE")
                If OnSearch(GridView2.GetRowCellValue(GridView2.FocusedRowHandle, "BARCODE"), _productCode, errstr) = False Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    cmbMaterialSearch.Focus()
                    cmbMaterialSearch.ClosePopup()
                    'cmbMaterialSearch.Text = ""
                End If
            End If
        Catch ex As Exception
            'MDIForm.AlertControl1.Show(Me, Version, ex.Message, MDIForm.ImageCollection1.Images(0))
        End Try
    End Sub
    Function OnSearch(ByVal _Barcode As String, ByVal _M_TRID As Integer, ByRef ErrorMsg As String) As Boolean
        Try
            Dim Qty As Decimal = 1.0
            _dsMM = New DataSet
            Dim PresentQty As Decimal = 0.0
            Dim M As Integer = 0
            If barbtnsearchbybarcode.Checked = True Then
                If Get_product_info(_Barcode, Qty, "BarcodeSplitQty", Errstr) = False Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    txtnooflabel.Focus()
                End If
            Else
                If Get_product_info(_M_TRID, Qty, "ItemCodeSplitQty", Errstr) = False Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    txtnooflabel.Focus()
                End If
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#Region "Selection Window"
    Private Function SelectedAllRceord(ByVal V As Integer, ByRef ErrorMsg As String) As Boolean
        Try
            For i As Integer = 0 To GridView1.RowCount - 1
                GridView1.SetRowCellValue(i, "Check", V)
            Next
            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return False
        End Try
    End Function

    Private Function SelectedRceord(ByVal V As Integer, ByRef ErrorMsg As String) As Boolean
        Try
            Dim j = GridView1.GetSelectedRows
            Dim i As Integer = 0
            For Each i In j
                GridView1.SetRowCellValue(i, "Check", V)
            Next
            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return False
        End Try
    End Function

    Private Sub SelectedSelectRecord()
        Try
            If SelectedRceord(1, Errstr) = False Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub SelectedUnSelectRecord()
        Try
            If SelectedRceord(0, Errstr) = False Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub AllrecordSelected()
        Try
            If SelectedAllRceord(1, Errstr) = False Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub AllrecordUnSelected()
        Try
            If SelectedAllRceord(0, Errstr) = False Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
#End Region


    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            Dim row = From dtrow As DataRow In _JsonData.ItemMasterTable Where dtrow("ITEMCODE") = _M_Trid

            _dsM.Rows.Add(row(0)("ITEMCODE"), row(0)("BARCODE"), 0, row(0)("ITEMNAME"), 0, _
                          0, 0, 0, 0, 0, row(0)("SELL") _
                          , txtnooflabel.EditValue, Now.Date, Now.Date, 1)
            cmbMaterialSearch.Focus()
            cmbMaterialSearch.EditValue = Nothing
            txtnooflabel.EditValue = 0
            GridView1.BestFitColumns()
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Function TableCreate() As Boolean
        Try
            _dsM = New DataTable
            _dsM.TableName = "Barcode"
            '_dsM.Columns.Add("Sal_MTRID", GetType(Integer))
            '_dsM.Columns.Add("Sal_DisAmt", GetType(Decimal()))
            '_dsM.Columns.Add("Sal_DisPer", GetType(Decimal()))
            '_dsM.Columns.Add("Sal_Tax", GetType(Decimal()))
            '_dsM.Columns.Add("Sal_TaxID", GetType(Integer))

            _dsM.Columns.Add("M_TRID", GetType(Integer))
            _dsM.Columns.Add("MBarcode", GetType(String))
            _dsM.Columns.Add("BatchCode", GetType(String))
            _dsM.Columns.Add("MaterialName", GetType(String))
            _dsM.Columns.Add("MaterialCategory", GetType(String))
            _dsM.Columns.Add("DesignName", GetType(String))
            _dsM.Columns.Add("C_Colorname", GetType(String))
            _dsM.Columns.Add("BrandName", GetType(String))
            _dsM.Columns.Add("Size", GetType(String))
            _dsM.Columns.Add("Sal_MRP", GetType(Decimal))
            _dsM.Columns.Add("Sal_SaleRate", GetType(Decimal))
            _dsM.Columns.Add("NoLabel", GetType(Integer))
            _dsM.Columns.Add("PackDate", GetType(Date))
            _dsM.Columns.Add("ExpireDate", GetType(Date))
            _dsM.Columns.Add("Check", GetType(Integer)).DefaultValue = 1

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub ComboLoadMethod()
        Try
            If getItemMaster() = True Then
                If _JsonData.ItemMasterTable.Rows.Count > 0 Then
                    GridControl2.DataSource = _JsonData.ItemMasterTable.DefaultView
                    dtview = _JsonData.ItemMasterTable.DefaultView
                End If
            End If

        Catch ex As Exception

            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try



    End Sub
    Private Sub frmBarcodeMateriallabel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboLoadMethod()
        TableCreate()
        GridControlGRN.DataSource = _dsM
    End Sub

    Private Sub frmBarcodeMateriallabel_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.F5 Then
            ComboLoadMethod()
        End If

    End Sub



    Function Get_product_info(ByRef _productCode As String, ByRef itemQty As Decimal, ByRef _Mode As String, ByRef ErrorMsg As String) As Boolean  'Product Select from Table _dsPro.table
        Try
            Dim ReceivedProCode As String = _productCode
            If ReceivedProCode Is Nothing Then
                Return False
            End If
            Dim dtrows As System.Data.EnumerableRowCollection(Of DataRow)

            If _Mode = "BarcodeSplitQty" Then
                dtrows = From dtrow As DataRow In _JsonData.ItemMasterTable Where String.Equals(dtrow("BARCODE"), ReceivedProCode, StringComparison.CurrentCultureIgnoreCase)
                If dtrows.Any = False Then
                    cmbMaterialSearch.EditValue = Nothing
                    _M_Trid = 0
                    ErrorMsg = "This Barcode is not Exists" & Environment.NewLine & "Kindly Refresh(F5) the Windows and Try Again."
                    Return False
                Else
                    _M_Trid = dtrows(0)("ITEMCODE")
                    cmbMaterialSearch.EditValue = ReceivedProCode
                End If

            End If
            If _Mode = "ItemCodeSplitQty" Then
                dtrows = From dtrow As DataRow In _JsonData.ItemMasterTable Where String.Equals(dtrow("ITEMCODE"), ReceivedProCode, StringComparison.CurrentCultureIgnoreCase)
                If dtrows.Any = False Then
                    cmbMaterialSearch.EditValue = Nothing
                    _M_Trid = 0
                    ErrorMsg = "This Barcode is not Exists" & Environment.NewLine & "Kindly Refresh(F5) the Windows and Try Again."
                    Return False
                Else
                    _M_Trid = dtrows(0)("ITEMCODE")
                    cmbMaterialSearch.EditValue = ReceivedProCode
                End If

            End If
            Return True
        Catch ex As Exception
            Return False
            'eLog.WriteErroLog("Get_product_info" & ex.Message)
        End Try

    End Function



    Private Sub txtnooflabel_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtnooflabel.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnadd.Focus()
        End If
    End Sub

    Private Sub btnBarcodeprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBarcodeprint.Click
        Try

            ' Dim _WaitDialog As DevExpress.Utils.WaitDialogForm = Nothing
            Dim _WaitDialog = New DevExpress.Utils.WaitDialogForm
            Dim pckdate As String = String.Empty
            Dim expdate As String = String.Empty
            Try



                Dim datetxt As String = String.Empty

                If GridView1.RowCount > 0 Then

                    _WaitDialog.Caption = "Kindly Select the Barcode"

                    If frmBarcodeSelector.ShowDialog = Windows.Forms.DialogResult.OK Then

                        pckdate = frmBarcodeSelector.txtpackingdate.Text
                        expdate = frmBarcodeSelector.txtexpdate.Text

                        Dim Txt As String = String.Empty
                        For i As Integer = 0 To GridView1.RowCount - 1

                            If GridView1.GetRowCellValue(i, "Check") = 1 Then

                                Txt &= GridView1.GetRowCellValue(i, "M_TRID") & ":" & GridView1.GetRowCellValue(i, "NoLabel").ToString & ":]"
                                datetxt = GridView1.GetRowCellDisplayText(i, "PackDate")

                            End If


                        Next

                        Dim pd As New PrintDialog()
                        pd.PrinterSettings = New PrinterSettings()
                        If (pd.ShowDialog() = DialogResult.OK) Then


                            _WaitDialog.Caption = "Barcode Printing..."
                            If Print2PRN(Txt, G_BarcodeName, pd.PrinterSettings.PrinterName, Errstr, pckdate, expdate) = False Then
                                Exit Sub

                            End If

                        End If


                    End If


                End If
            Catch ex As Exception

            Finally
                _WaitDialog.Close()
            End Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView1.PopupMenuShowing
        Try
            btnAll.Caption = "Selected AllRecord"
            e.Menu.Items.Add(btnAll)

            btnUnAll.Caption = "UnSelected AllRecord"
            e.Menu.Items.Add(btnUnAll)

            btnSelectAll.BeginGroup = True
            btnSelectAll.Caption = "Selected Record"
            e.Menu.Items.Add(btnSelectAll)

            btnSelectUnAll.Caption = "UnSelected Record"
            e.Menu.Items.Add(btnSelectUnAll)
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
            ' frmcustombarcodeprint.MdiParent = MDIForm
            ' frmcustombarcodeprint.Show()
            If File.Exists(M_Details._appPath & "barc.bat") Then
                info.FileName = M_Details._appPath & "barc.bat"
                info.WorkingDirectory = M_Details._appPath
                Process.Start(info)
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub



    Private Sub btnSaveData_Click(sender As Object, e As EventArgs) Handles btnSaveData.Click
        Try

            If Not File.Exists(M_Details._appPath & "\Reports\BarcodeRecentData.xml") Then
                _dsM.WriteXml(M_Details._appPath & "\Reports\BarcodeRecentData.xml", True, System.Data.XmlWriteMode.WriteSchema)
            Else
                _dsM.WriteXml(M_Details._appPath & "\Reports\BarcodeRecentData.xml", True, System.Data.XmlWriteMode.WriteSchema)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnReloadData_Click(sender As Object, e As EventArgs) Handles btnReloadData.Click
        Try
            _dsM.ReadXml(M_Details._appPath & "\Reports\BarcodeRecentData.xml")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                GridView1.DeleteSelectedRows()
                _dsM.AcceptChanges()
            End If
        Catch ex As Exception

        End Try
    End Sub


#Region "PrintBarcodeInsideDesign"
    Dim barcodes As New BarcodeLib.Barcode()

    ' Barcode data
    Dim BarcodeData As String = "A1234567890"
    Dim barcodeType As TYPE = TYPE.CODE39

    ' PrintDocument object
    Dim printDoc As New PrintDocument()

    ' Label details
    Dim SalesRate As String = "RM 0.00"
    Dim ExpiryDate As String = "10/05/2025"
    Dim PackDate As String = "10/05/2025"
    Dim _ProductName As String = "C Powder 1Kg"
    Dim ShopName As String = _companyInfo.LocationName
    Dim PrintQty As String = "1"
    Dim _PictureEdit As New PictureEdit
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click

        Dim _WaitDialog = New DevExpress.Utils.WaitDialogForm
        'frmBarcodePrintTest.ShowDialog()
        Try
            LoadBarcoodesettings()
            _WaitDialog.Caption = "Reading Barcode Settings"
            If GridView1.RowCount > 0 Then
                Dim pd As New PrintDialog()
                pd.PrinterSettings = New PrinterSettings()
                If (pd.ShowDialog() = DialogResult.OK) Then
                    _WaitDialog.Caption = "Barcode Printing..."
                    Dim Txt As String = String.Empty
                    For i As Integer = 0 To GridView1.RowCount - 1
                        BarcodeData = ""
                        SalesRate = ""
                        _ProductName = ""
                        PackDate = ""
                        PrintQty = ""
                        ExpiryDate = ""
                        If GridView1.GetRowCellValue(i, "Check") = 1 Then
                            BarcodeData = GridView1.GetRowCellValue(i, "MBarcode")
                            SalesRate = GridView1.GetRowCellValue(i, "Sal_SaleRate")
                            _ProductName = GridView1.GetRowCellValue(i, "MaterialName")
                            PrintQty = GridView1.GetRowCellValue(i, "NoLabel")
                            PackDate = GridView1.GetRowCellValue(i, "PackDate")
                            ExpiryDate = GridView1.GetRowCellValue(i, "ExpireDate")
                            barcodes.Encode(barcodeType, BarcodeData)
                            ' _PictureEdit.Image = barcodes.EncodedImage
                        End If
                        AddHandler printDoc.PrintPage, AddressOf Me.PrintPage
                        printDoc.PrinterSettings.PrinterName = pd.PrinterSettings.PrinterName
                        Dim customSize As New Printing.PaperSize("CustomLabel", CInt(_labelSettings.LabelWidthMm / 25.4 * 100), CInt(_labelSettings.LabelHeightMm / 25.4 * 100))
                        printDoc.DefaultPageSettings.PaperSize = customSize
                        printDoc.PrinterSettings.Copies = PrintQty
                        printDoc.Print()
                    Next
                End If

            End If

        Catch ex As Exception

        Finally
            _WaitDialog.Close()
        End Try
    End Sub
 
    'Private Sub PrintPage(sender As Object, e As PrintPageEventArgs)

    '    ' Get margins from settings (convert to pixels at 100 DPI)
    '    Dim marginLeft As Integer = CInt(_labelSettings.MarginLeft * 100)
    '    Dim marginTop As Integer = CInt(_labelSettings.MarginTop * 100)
    '    Dim marginRight As Integer = CInt(_labelSettings.MarginRight * 100)
    '    Dim marginBottom As Integer = CInt(_labelSettings.MarginBottom * 100)
    '    ' Scale barcode to fit within the label
    '    Dim barcodeWidth As Integer = 120
    '    Dim barcodeHeight As Integer = 40

    '    ' Define font and brush
    '    Using font As New Font("Arial", 8, FontStyle.Bold)
    '        Using brush As New SolidBrush(Color.Black)
    '            Dim currentY As Integer = marginTop

    '            ' --- Shop Name ---
    '            If _labelSettings.ShowShopName.Enabled Then
    '                currentY += _labelSettings.ShowShopName.OffsetY
    '                e.Graphics.DrawString(ShopName, font, brush, marginLeft, currentY)
    '            End If

    '            ' --- Barcode ---
    '            If _labelSettings.ShowBarcode.Enabled Then
    '                currentY += _labelSettings.ShowBarcode.OffsetY
    '                e.Graphics.DrawImage(barcodes.EncodedImage, marginLeft, currentY, barcodeWidth, barcodeHeight)
    '            End If

    '            ' --- Barcode Number ---
    '            If _labelSettings.ShowBarcodeNumber.Enabled Then
    '                currentY += _labelSettings.ShowBarcodeNumber.OffsetY
    '                e.Graphics.DrawString(BarcodeData, font, brush, marginLeft + 25, currentY)
    '            End If

    '            ' --- Product Name ---
    '            If _labelSettings.ShowProductName.Enabled Then
    '                currentY += _labelSettings.ShowProductName.OffsetY
    '                e.Graphics.DrawString(_ProductName, font, brush, marginLeft, currentY)
    '            End If

    '            ' --- Price ---
    '            If _labelSettings.ShowPrice.Enabled Then
    '                currentY += _labelSettings.ShowPrice.OffsetY
    '                e.Graphics.DrawString("RM: " & SalesRate, font, brush, marginLeft, currentY)
    '            End If

    '            ' --- Expiry Date ---
    '            If _labelSettings.ShowExpiryDate.Enabled Then
    '                currentY += _labelSettings.ShowExpiryDate.OffsetY
    '                e.Graphics.DrawString("Expiry: " & ExpiryDate, font, brush, marginLeft, currentY)
    '            End If

    '            ' --- Pack Date ---
    '            If _labelSettings.ShowPackDate.Enabled Then
    '                currentY += _labelSettings.ShowPackDate.OffsetY
    '                e.Graphics.DrawString("Packed: " & PackDate, font, brush, marginLeft, currentY)
    '            End If

    '        End Using
    '    End Using

    '    ' Only one page
    '    e.HasMorePages = False
    'End Sub
    Private Sub PrintPage(sender As Object, e As PrintPageEventArgs)

        ' --- Convert margins (100 DPI assumed) ---
        Dim marginLeft As Integer = CInt(_labelSettings.MarginLeft * 100)
        Dim marginTop As Integer = CInt(_labelSettings.MarginTop * 100)
        Dim marginRight As Integer = CInt(_labelSettings.MarginRight * 100)
        Dim marginBottom As Integer = CInt(_labelSettings.MarginBottom * 100)

        ' --- Define printable area ---
        Dim printableWidth As Integer = e.PageBounds.Width - marginLeft - marginRight
        Dim printableHeight As Integer = e.PageBounds.Height - marginTop - marginBottom
        Dim printableRect As New Rectangle(marginLeft, marginTop, printableWidth, printableHeight)

        ' --- Barcode size ---
        Dim barcodeWidth As Integer = _labelSettings.BarcodeWidth
        Dim barcodeHeight As Integer = _labelSettings.BarcodeHeight

        Using font As New Font("Arial", 8, FontStyle.Bold)
            Using brush As New SolidBrush(Color.Black)

                ' ----------- DEBUG OUTLINE -----------
                Using pen As New Pen(Color.Red, 1)
                    e.Graphics.DrawRectangle(pen, printableRect)
                End Using

                ' ----------- FLOW FROM TOP (LEFT ALIGNED) -----------
                Dim currentY As Integer = printableRect.Top

                ' Shop Name
                If _labelSettings.ShowShopName.Enabled Then
                    currentY += _labelSettings.ShowShopName.OffsetY
                    e.Graphics.DrawString(ShopName, font, brush, printableRect.Left, currentY)
                    currentY += font.Height
                End If

                ' Barcode
                If _labelSettings.ShowBarcode.Enabled Then
                    currentY += _labelSettings.ShowBarcode.OffsetY
                    e.Graphics.DrawImage(barcodes.EncodedImage, printableRect.Left, currentY, barcodeWidth, barcodeHeight)
                    currentY += barcodeHeight
                End If

                ' Barcode Number
                If _labelSettings.ShowBarcodeNumber.Enabled Then
                    currentY += _labelSettings.ShowBarcodeNumber.OffsetY
                    e.Graphics.DrawString(BarcodeData, font, brush, printableRect.Left, currentY)
                    currentY += font.Height
                End If

                ' Product Name
                If _labelSettings.ShowProductName.Enabled Then
                    currentY += _labelSettings.ShowProductName.OffsetY
                    e.Graphics.DrawString(_ProductName, font, brush, printableRect.Left, currentY)
                    currentY += font.Height
                End If

                ' Price
                If _labelSettings.ShowPrice.Enabled Then
                    currentY += _labelSettings.ShowPrice.OffsetY
                    e.Graphics.DrawString("RM: " & SalesRate, font, brush, printableRect.Left, currentY)
                    currentY += font.Height
                End If

                ' Expiry Date
                If _labelSettings.ShowExpiryDate.Enabled Then
                    currentY += _labelSettings.ShowExpiryDate.OffsetY
                    e.Graphics.DrawString("Expiry: " & ExpiryDate, font, brush, printableRect.Left, currentY)
                    currentY += font.Height
                End If

                ' Pack Date
                If _labelSettings.ShowPackDate.Enabled Then
                    currentY += _labelSettings.ShowPackDate.OffsetY
                    e.Graphics.DrawString("Packed: " & PackDate, font, brush, printableRect.Left, currentY)
                    currentY += font.Height
                End If

            End Using
        End Using

        e.HasMorePages = False
    End Sub


#End Region
    Private Sub LoadBarcoodesettings()
        Try
            If LoadSettings(M_Details._appPath & "\Labelini.ini") Then

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub barbtnlabelsettings_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnlabelsettings.ItemClick
        Try
            LoadBarcoodesettings()
        Catch ex As Exception

        End Try
    End Sub
    Public Function LoadSettings(filePath As String) As Boolean
        Try
            Dim ini As New IniFile(filePath)
            _labelSettings = New LabelSettings
            ' Margins
            _labelSettings.MarginLeft = Decimal.Parse(ini.ReadValue("Margins", "Left"))
            _labelSettings.MarginTop = Decimal.Parse(ini.ReadValue("Margins", "Top"))
            _labelSettings.MarginRight = Decimal.Parse(ini.ReadValue("Margins", "Right"))
            _labelSettings.MarginBottom = Decimal.Parse(ini.ReadValue("Margins", "Bottom"))
            'Label
            _labelSettings.LabelWidthMm = Decimal.Parse(ini.ReadValue("Label", "LabelWidthMm"))
            _labelSettings.LabelHeightMm = Decimal.Parse(ini.ReadValue("Label", "LabelHeightMm"))
            'Barcode
            _labelSettings.BarcodeHeight = Integer.Parse(ini.ReadValue("Barcode", "BarcodeHieght"))
            _labelSettings.BarcodeWidth = Integer.Parse(ini.ReadValue("Barcode", "BarcodeWidth"))

            ' Fields
            _labelSettings.ShowShopName.Enabled = Boolean.Parse(ini.ReadValue("Fields", "ShopNameEnabled"))
            _labelSettings.ShowShopName.OffsetY = Integer.Parse(ini.ReadValue("Fields", "ShopNameY"))

            _labelSettings.ShowBarcode.Enabled = Boolean.Parse(ini.ReadValue("Fields", "BarcodeEnabled"))
            _labelSettings.ShowBarcode.OffsetY = Integer.Parse(ini.ReadValue("Fields", "BarcodeY"))

            _labelSettings.ShowBarcodeNumber.Enabled = Boolean.Parse(ini.ReadValue("Fields", "BarcodeNumberEnabled"))
            _labelSettings.ShowBarcodeNumber.OffsetY = Integer.Parse(ini.ReadValue("Fields", "BarcodeNumberY"))

            _labelSettings.ShowProductName.Enabled = Boolean.Parse(ini.ReadValue("Fields", "ProductNameEnabled"))
            _labelSettings.ShowProductName.OffsetY = Integer.Parse(ini.ReadValue("Fields", "ProductNameY"))

            _labelSettings.ShowPrice.Enabled = Boolean.Parse(ini.ReadValue("Fields", "PriceEnabled"))
            _labelSettings.ShowPrice.OffsetY = Integer.Parse(ini.ReadValue("Fields", "PriceY"))

            _labelSettings.ShowExpiryDate.Enabled = Boolean.Parse(ini.ReadValue("Fields", "ExpiryDateEnabled"))
            _labelSettings.ShowExpiryDate.OffsetY = Integer.Parse(ini.ReadValue("Fields", "ExpiryDateY"))

            _labelSettings.ShowPackDate.Enabled = Boolean.Parse(ini.ReadValue("Fields", "PackDateEnabled"))
            _labelSettings.ShowPackDate.OffsetY = Integer.Parse(ini.ReadValue("Fields", "PackDateY"))

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class
Public Class FieldPosition
    Public Property Enabled As Boolean = True
    Public Property OffsetY As Integer = 0
End Class
Public Class LabelSettings
    ' Margins
    Public Property MarginLeft As Decimal = 0.01
    Public Property MarginTop As Decimal = 0.01
    Public Property MarginRight As Decimal = 0.01
    Public Property MarginBottom As Decimal = 0.01
    Public Property LabelWidthMm As Decimal = 50   ' default 50mm
    Public Property LabelHeightMm As Decimal = 30  ' default 30mm
    'Barcode
    Public Property BarcodeWidth As Integer = 120
    Public Property BarcodeHeight As Integer = 40
    ' Field settings (enabled + Y-offsets)
    Public Property ShowShopName As New FieldPosition With {.Enabled = True, .OffsetY = 0}
    Public Property ShowBarcode As New FieldPosition With {.Enabled = True, .OffsetY = 0}
    Public Property ShowBarcodeNumber As New FieldPosition With {.Enabled = True, .OffsetY = 0}
    Public Property ShowProductName As New FieldPosition With {.Enabled = True, .OffsetY = 0}
    Public Property ShowPrice As New FieldPosition With {.Enabled = True, .OffsetY = 0}
    Public Property ShowExpiryDate As New FieldPosition With {.Enabled = True, .OffsetY = 0}
    Public Property ShowPackDate As New FieldPosition With {.Enabled = True, .OffsetY = 0}
    Public Sub SaveSettings(filePath As String, settings As LabelSettings)
        Dim ini As New IniFile(filePath)

        ' Margins
        ini.WriteValue("Margins", "Left", settings.MarginLeft.ToString())
        ini.WriteValue("Margins", "Top", settings.MarginTop.ToString())
        ini.WriteValue("Margins", "Right", settings.MarginRight.ToString())
        ini.WriteValue("Margins", "Bottom", settings.MarginBottom.ToString())

        ' Fields
        ini.WriteValue("Fields", "ShopNameEnabled", settings.ShowShopName.Enabled.ToString())
        ini.WriteValue("Fields", "ShopNameY", settings.ShowShopName.OffsetY.ToString())

        ini.WriteValue("Fields", "BarcodeEnabled", settings.ShowBarcode.Enabled.ToString())
        ini.WriteValue("Fields", "BarcodeY", settings.ShowBarcode.OffsetY.ToString())

        ini.WriteValue("Fields", "BarcodeNumberEnabled", settings.ShowBarcodeNumber.Enabled.ToString())
        ini.WriteValue("Fields", "BarcodeNumberY", settings.ShowBarcodeNumber.OffsetY.ToString())

        ini.WriteValue("Fields", "ProductNameEnabled", settings.ShowProductName.Enabled.ToString())
        ini.WriteValue("Fields", "ProductNameY", settings.ShowProductName.OffsetY.ToString())

        ini.WriteValue("Fields", "PriceEnabled", settings.ShowPrice.Enabled.ToString())
        ini.WriteValue("Fields", "PriceY", settings.ShowPrice.OffsetY.ToString())

        ini.WriteValue("Fields", "ExpiryDateEnabled", settings.ShowExpiryDate.Enabled.ToString())
        ini.WriteValue("Fields", "ExpiryDateY", settings.ShowExpiryDate.OffsetY.ToString())

        ini.WriteValue("Fields", "PackDateEnabled", settings.ShowPackDate.Enabled.ToString())
        ini.WriteValue("Fields", "PackDateY", settings.ShowPackDate.OffsetY.ToString())
    End Sub

End Class

