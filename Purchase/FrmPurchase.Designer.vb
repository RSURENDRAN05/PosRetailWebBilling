<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPurchase
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPurchase))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.txtsell = New DevExpress.XtraEditors.TextEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.barbtnExit = New DevExpress.XtraBars.BarButtonItem()
        Me.BarSubItem1 = New DevExpress.XtraBars.BarSubItem()
        Me.btnViewPurchase = New DevExpress.XtraBars.BarButtonItem()
        Me.btnsavePurchase = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSaveGridViewPosLayout = New DevExpress.XtraBars.BarButtonItem()
        Me.btnreset = New DevExpress.XtraBars.BarButtonItem()
        Me.Bar3 = New DevExpress.XtraBars.Bar()
        Me.BarAndDockingController1 = New DevExpress.XtraBars.BarAndDockingController(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.txtroundoff = New DevExpress.XtraEditors.TextEdit()
        Me.PopupContainerControl1 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.txtMqty = New DevExpress.XtraEditors.TextEdit()
        Me.GridControl2 = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumnProduct = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnSalesPriceSearch = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnWholesaleRate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnProductCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtsearch2 = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem37 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem38 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem39 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.btnclear = New DevExpress.XtraEditors.SimpleButton()
        Me.txtcostprice = New DevExpress.XtraEditors.TextEdit()
        Me.txtnetamt = New DevExpress.XtraEditors.TextEdit()
        Me.txttottax = New DevExpress.XtraEditors.TextEdit()
        Me.txttotdiscamt = New DevExpress.XtraEditors.TextEdit()
        Me.txttotdiscper = New DevExpress.XtraEditors.TextEdit()
        Me.txttaxexcamt = New DevExpress.XtraEditors.TextEdit()
        Me.txttaxincamt = New DevExpress.XtraEditors.TextEdit()
        Me.txttotitem = New DevExpress.XtraEditors.TextEdit()
        Me.txtgrossamt = New DevExpress.XtraEditors.TextEdit()
        Me.txttotqty = New DevExpress.XtraEditors.TextEdit()
        Me.txtitemnetamt = New DevExpress.XtraEditors.TextEdit()
        Me.txtamount = New DevExpress.XtraEditors.TextEdit()
        Me.txtqty = New DevExpress.XtraEditors.TextEdit()
        Me.txtpurchaserate = New DevExpress.XtraEditors.TextEdit()
        Me.txtbatch = New DevExpress.XtraEditors.TextEdit()
        Me.txttaxamt = New DevExpress.XtraEditors.TextEdit()
        Me.txtdisamt = New DevExpress.XtraEditors.TextEdit()
        Me.txtdiscper = New DevExpress.XtraEditors.TextEdit()
        Me.txtserialno = New DevExpress.XtraEditors.TextEdit()
        Me.txtbarcode = New DevExpress.XtraEditors.TextEdit()
        Me.txtitemname = New DevExpress.XtraEditors.TextEdit()
        Me.txtitemcode = New DevExpress.XtraEditors.TextEdit()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridViewPOS = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumnSNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnPURID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnITEMCODE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnBARCODE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnITEMNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnITEMSERIALNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnPURRATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnPURQTY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnPURAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnPURDISPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnPURDISAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnPURTOTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnPURCOST = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnPURSELL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnPURTAXID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnPURTAXAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnPURGROSSAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnPURROUNDOFF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnPURNETAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtrefno = New DevExpress.XtraEditors.TextEdit()
        Me.txtinvoiceno = New DevExpress.XtraEditors.TextEdit()
        Me.txtpaymenttype = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtinvoicedate = New DevExpress.XtraEditors.DateEdit()
        Me.txtpurchasedate = New DevExpress.XtraEditors.DateEdit()
        Me.txtexpire = New DevExpress.XtraEditors.DateEdit()
        Me.txtsupplier = New DevExpress.XtraEditors.LookUpEdit()
        Me.txttaxid = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtunit = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbMaterialSearch = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem20 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem21 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem22 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem23 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem24 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem25 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem26 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem27 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem28 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem29 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem30 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem31 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem33 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem34 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem32 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem35 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem40 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem41 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem36 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtsell.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarAndDockingController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtroundoff.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PopupContainerControl1.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.txtMqty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsearch2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcostprice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtnetamt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttottax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttotdiscamt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttotdiscper.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttaxexcamt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttaxincamt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttotitem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtgrossamt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttotqty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtitemnetamt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtamount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtqty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpurchaserate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbatch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttaxamt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdisamt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdiscper.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtserialno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbarcode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtitemname.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtitemcode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewPOS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtrefno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtinvoiceno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpaymenttype.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtinvoicedate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtinvoicedate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpurchasedate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpurchasedate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtexpire.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtexpire.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsupplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttaxid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtunit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbMaterialSearch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem36, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.txtsell)
        Me.LayoutControl1.Controls.Add(Me.txtroundoff)
        Me.LayoutControl1.Controls.Add(Me.PopupContainerControl1)
        Me.LayoutControl1.Controls.Add(Me.btnAdd)
        Me.LayoutControl1.Controls.Add(Me.btnclear)
        Me.LayoutControl1.Controls.Add(Me.txtcostprice)
        Me.LayoutControl1.Controls.Add(Me.txtnetamt)
        Me.LayoutControl1.Controls.Add(Me.txttottax)
        Me.LayoutControl1.Controls.Add(Me.txttotdiscamt)
        Me.LayoutControl1.Controls.Add(Me.txttotdiscper)
        Me.LayoutControl1.Controls.Add(Me.txttaxexcamt)
        Me.LayoutControl1.Controls.Add(Me.txttaxincamt)
        Me.LayoutControl1.Controls.Add(Me.txttotitem)
        Me.LayoutControl1.Controls.Add(Me.txtgrossamt)
        Me.LayoutControl1.Controls.Add(Me.txttotqty)
        Me.LayoutControl1.Controls.Add(Me.txtitemnetamt)
        Me.LayoutControl1.Controls.Add(Me.txtamount)
        Me.LayoutControl1.Controls.Add(Me.txtqty)
        Me.LayoutControl1.Controls.Add(Me.txtpurchaserate)
        Me.LayoutControl1.Controls.Add(Me.txtbatch)
        Me.LayoutControl1.Controls.Add(Me.txttaxamt)
        Me.LayoutControl1.Controls.Add(Me.txtdisamt)
        Me.LayoutControl1.Controls.Add(Me.txtdiscper)
        Me.LayoutControl1.Controls.Add(Me.txtserialno)
        Me.LayoutControl1.Controls.Add(Me.txtbarcode)
        Me.LayoutControl1.Controls.Add(Me.txtitemname)
        Me.LayoutControl1.Controls.Add(Me.txtitemcode)
        Me.LayoutControl1.Controls.Add(Me.GridControl1)
        Me.LayoutControl1.Controls.Add(Me.txtrefno)
        Me.LayoutControl1.Controls.Add(Me.txtinvoiceno)
        Me.LayoutControl1.Controls.Add(Me.txtpaymenttype)
        Me.LayoutControl1.Controls.Add(Me.txtinvoicedate)
        Me.LayoutControl1.Controls.Add(Me.txtpurchasedate)
        Me.LayoutControl1.Controls.Add(Me.txtexpire)
        Me.LayoutControl1.Controls.Add(Me.txtsupplier)
        Me.LayoutControl1.Controls.Add(Me.txttaxid)
        Me.LayoutControl1.Controls.Add(Me.txtunit)
        Me.LayoutControl1.Controls.Add(Me.cmbMaterialSearch)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(499, 486, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1203, 681)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'txtsell
        '
        Me.txtsell.EditValue = "0.00"
        Me.txtsell.Location = New System.Drawing.Point(444, 86)
        Me.txtsell.MenuManager = Me.BarManager1
        Me.txtsell.Name = "txtsell"
        Me.txtsell.Properties.Appearance.Options.UseTextOptions = True
        Me.txtsell.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtsell.Properties.DisplayFormat.FormatString = "n2"
        Me.txtsell.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtsell.Properties.EditFormat.FormatString = "n2"
        Me.txtsell.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtsell.Properties.Mask.EditMask = "n2"
        Me.txtsell.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtsell.Size = New System.Drawing.Size(63, 20)
        Me.txtsell.StyleController = Me.LayoutControl1
        Me.txtsell.TabIndex = 41
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1, Me.Bar3})
        Me.BarManager1.Controller = Me.BarAndDockingController1
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.barbtnExit, Me.BarSubItem1, Me.btnSaveGridViewPosLayout, Me.btnsavePurchase, Me.btnViewPurchase, Me.btnreset})
        Me.BarManager1.MaxItemId = 6
        Me.BarManager1.StatusBar = Me.Bar3
        '
        'Bar1
        '
        Me.Bar1.BarName = "Tools"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barbtnExit, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarSubItem1, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Tools"
        '
        'barbtnExit
        '
        Me.barbtnExit.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.barbtnExit.Caption = "Exit"
        Me.barbtnExit.Glyph = CType(resources.GetObject("barbtnExit.Glyph"), System.Drawing.Image)
        Me.barbtnExit.Id = 0
        Me.barbtnExit.ItemAppearance.Normal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.barbtnExit.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Black
        Me.barbtnExit.ItemAppearance.Normal.Options.UseFont = True
        Me.barbtnExit.ItemAppearance.Normal.Options.UseForeColor = True
        Me.barbtnExit.LargeGlyph = CType(resources.GetObject("barbtnExit.LargeGlyph"), System.Drawing.Image)
        Me.barbtnExit.Name = "barbtnExit"
        '
        'BarSubItem1
        '
        Me.BarSubItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.BarSubItem1.Caption = "File"
        Me.BarSubItem1.Glyph = CType(resources.GetObject("BarSubItem1.Glyph"), System.Drawing.Image)
        Me.BarSubItem1.Id = 1
        Me.BarSubItem1.LargeGlyph = CType(resources.GetObject("BarSubItem1.LargeGlyph"), System.Drawing.Image)
        Me.BarSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnViewPurchase, True), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnsavePurchase, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnSaveGridViewPosLayout, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnreset, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.BarSubItem1.Name = "BarSubItem1"
        '
        'btnViewPurchase
        '
        Me.btnViewPurchase.Caption = "View"
        Me.btnViewPurchase.Glyph = CType(resources.GetObject("btnViewPurchase.Glyph"), System.Drawing.Image)
        Me.btnViewPurchase.Id = 4
        Me.btnViewPurchase.Name = "btnViewPurchase"
        '
        'btnsavePurchase
        '
        Me.btnsavePurchase.Caption = "Save"
        Me.btnsavePurchase.Glyph = CType(resources.GetObject("btnsavePurchase.Glyph"), System.Drawing.Image)
        Me.btnsavePurchase.Id = 3
        Me.btnsavePurchase.Name = "btnsavePurchase"
        '
        'btnSaveGridViewPosLayout
        '
        Me.btnSaveGridViewPosLayout.Caption = "SaveLayOut"
        Me.btnSaveGridViewPosLayout.Glyph = CType(resources.GetObject("btnSaveGridViewPosLayout.Glyph"), System.Drawing.Image)
        Me.btnSaveGridViewPosLayout.Id = 2
        Me.btnSaveGridViewPosLayout.Name = "btnSaveGridViewPosLayout"
        '
        'btnreset
        '
        Me.btnreset.Caption = "Reset"
        Me.btnreset.Glyph = CType(resources.GetObject("btnreset.Glyph"), System.Drawing.Image)
        Me.btnreset.Id = 5
        Me.btnreset.Name = "btnreset"
        '
        'Bar3
        '
        Me.Bar3.BarName = "Status bar"
        Me.Bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.Bar3.DockCol = 0
        Me.Bar3.DockRow = 0
        Me.Bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.Bar3.OptionsBar.AllowQuickCustomization = False
        Me.Bar3.OptionsBar.DrawDragBorder = False
        Me.Bar3.OptionsBar.UseWholeRow = True
        Me.Bar3.Text = "Status bar"
        '
        'BarAndDockingController1
        '
        Me.BarAndDockingController1.AppearancesBar.BarAppearance.Normal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BarAndDockingController1.AppearancesBar.BarAppearance.Normal.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BarAndDockingController1.AppearancesBar.BarAppearance.Normal.Options.UseBackColor = True
        Me.BarAndDockingController1.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.BarAndDockingController1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.BarAndDockingController1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BarAndDockingController1.PropertiesBar.AllowLinkLighting = False
        Me.BarAndDockingController1.PropertiesBar.DefaultGlyphSize = New System.Drawing.Size(16, 16)
        Me.BarAndDockingController1.PropertiesBar.DefaultLargeGlyphSize = New System.Drawing.Size(32, 32)
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1203, 30)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 711)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1203, 18)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 30)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 681)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1203, 30)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 681)
        '
        'txtroundoff
        '
        Me.txtroundoff.EditValue = "0.00"
        Me.txtroundoff.Location = New System.Drawing.Point(808, 571)
        Me.txtroundoff.MenuManager = Me.BarManager1
        Me.txtroundoff.Name = "txtroundoff"
        Me.txtroundoff.Properties.Appearance.Options.UseTextOptions = True
        Me.txtroundoff.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtroundoff.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtroundoff.Properties.DisplayFormat.FormatString = "n2"
        Me.txtroundoff.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtroundoff.Properties.EditFormat.FormatString = "n2"
        Me.txtroundoff.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtroundoff.Properties.Mask.EditMask = "n2"
        Me.txtroundoff.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtroundoff.Size = New System.Drawing.Size(93, 20)
        Me.txtroundoff.StyleController = Me.LayoutControl1
        Me.txtroundoff.TabIndex = 40
        '
        'PopupContainerControl1
        '
        Me.PopupContainerControl1.Controls.Add(Me.LayoutControl2)
        Me.PopupContainerControl1.Location = New System.Drawing.Point(138, 37)
        Me.PopupContainerControl1.Name = "PopupContainerControl1"
        Me.PopupContainerControl1.Size = New System.Drawing.Size(846, 474)
        Me.PopupContainerControl1.TabIndex = 39
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.txtMqty)
        Me.LayoutControl2.Controls.Add(Me.GridControl2)
        Me.LayoutControl2.Controls.Add(Me.txtsearch2)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        Me.LayoutControl2.Size = New System.Drawing.Size(846, 474)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'txtMqty
        '
        Me.txtMqty.EditValue = "1.000"
        Me.txtMqty.Location = New System.Drawing.Point(32, 2)
        Me.txtMqty.Name = "txtMqty"
        Me.txtMqty.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.txtMqty.Properties.Appearance.Options.UseFont = True
        Me.txtMqty.Properties.Appearance.Options.UseTextOptions = True
        Me.txtMqty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtMqty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtMqty.Properties.ReadOnly = True
        Me.txtMqty.Size = New System.Drawing.Size(100, 30)
        Me.txtMqty.StyleController = Me.LayoutControl2
        Me.txtMqty.TabIndex = 9
        '
        'GridControl2
        '
        Me.GridControl2.Location = New System.Drawing.Point(2, 36)
        Me.GridControl2.MainView = Me.GridView2
        Me.GridControl2.Name = "GridControl2"
        Me.GridControl2.Size = New System.Drawing.Size(842, 436)
        Me.GridControl2.TabIndex = 8
        Me.GridControl2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Tahoma", 13.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.GridView2.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.GridView2.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView2.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridView2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridView2.Appearance.SelectedRow.Options.UseBackColor = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumnProduct, Me.GridColumnSalesPriceSearch, Me.GridColumnWholesaleRate, Me.GridColumnProductCode, Me.GridColumn2, Me.GridColumn4})
        Me.GridView2.GridControl = Me.GridControl2
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.Editable = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.GridView2.RowHeight = 25
        '
        'GridColumnProduct
        '
        Me.GridColumnProduct.Caption = "Product Name"
        Me.GridColumnProduct.FieldName = "ITEMNAME"
        Me.GridColumnProduct.Name = "GridColumnProduct"
        Me.GridColumnProduct.Visible = True
        Me.GridColumnProduct.VisibleIndex = 1
        Me.GridColumnProduct.Width = 225
        '
        'GridColumnSalesPriceSearch
        '
        Me.GridColumnSalesPriceSearch.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnSalesPriceSearch.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumnSalesPriceSearch.Caption = "Sales Price"
        Me.GridColumnSalesPriceSearch.FieldName = "SELL"
        Me.GridColumnSalesPriceSearch.Name = "GridColumnSalesPriceSearch"
        Me.GridColumnSalesPriceSearch.Visible = True
        Me.GridColumnSalesPriceSearch.VisibleIndex = 2
        Me.GridColumnSalesPriceSearch.Width = 73
        '
        'GridColumnWholesaleRate
        '
        Me.GridColumnWholesaleRate.Caption = "Wholesale"
        Me.GridColumnWholesaleRate.FieldName = "SELL"
        Me.GridColumnWholesaleRate.Name = "GridColumnWholesaleRate"
        Me.GridColumnWholesaleRate.Width = 71
        '
        'GridColumnProductCode
        '
        Me.GridColumnProductCode.Caption = "Product Code"
        Me.GridColumnProductCode.FieldName = "ITEMCODE"
        Me.GridColumnProductCode.Name = "GridColumnProductCode"
        Me.GridColumnProductCode.Visible = True
        Me.GridColumnProductCode.VisibleIndex = 0
        Me.GridColumnProductCode.Width = 47
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Stock"
        Me.GridColumn2.FieldName = "LIVESTOCK"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Width = 55
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic)
        Me.GridColumn4.AppearanceCell.ForeColor = System.Drawing.Color.Blue
        Me.GridColumn4.AppearanceCell.Options.UseFont = True
        Me.GridColumn4.AppearanceCell.Options.UseForeColor = True
        Me.GridColumn4.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn4.Caption = "Cost"
        Me.GridColumn4.FieldName = "COST"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'txtsearch2
        '
        Me.txtsearch2.Location = New System.Drawing.Point(136, 2)
        Me.txtsearch2.Name = "txtsearch2"
        Me.txtsearch2.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearch2.Properties.Appearance.Options.UseFont = True
        Me.txtsearch2.Size = New System.Drawing.Size(708, 30)
        Me.txtsearch2.StyleController = Me.LayoutControl2
        Me.txtsearch2.TabIndex = 7
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LayoutControlGroup2.AppearanceGroup.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseBackColor = True
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[False]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem37, Me.LayoutControlItem38, Me.LayoutControlItem39})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(846, 474)
        Me.LayoutControlGroup2.Text = "LayoutControlGroup2"
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlItem37
        '
        Me.LayoutControlItem37.Control = Me.GridControl2
        Me.LayoutControlItem37.CustomizationFormText = "LayoutControlItem37"
        Me.LayoutControlItem37.Location = New System.Drawing.Point(0, 34)
        Me.LayoutControlItem37.Name = "LayoutControlItem37"
        Me.LayoutControlItem37.Size = New System.Drawing.Size(846, 440)
        Me.LayoutControlItem37.Text = "LayoutControlItem37"
        Me.LayoutControlItem37.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem37.TextToControlDistance = 0
        Me.LayoutControlItem37.TextVisible = False
        '
        'LayoutControlItem38
        '
        Me.LayoutControlItem38.Control = Me.txtsearch2
        Me.LayoutControlItem38.CustomizationFormText = "LayoutControlItem38"
        Me.LayoutControlItem38.Location = New System.Drawing.Point(134, 0)
        Me.LayoutControlItem38.Name = "LayoutControlItem38"
        Me.LayoutControlItem38.Size = New System.Drawing.Size(712, 34)
        Me.LayoutControlItem38.Text = "LayoutControlItem38"
        Me.LayoutControlItem38.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem38.TextToControlDistance = 0
        Me.LayoutControlItem38.TextVisible = False
        '
        'LayoutControlItem39
        '
        Me.LayoutControlItem39.Control = Me.txtMqty
        Me.LayoutControlItem39.CustomizationFormText = "Qty :"
        Me.LayoutControlItem39.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem39.Name = "LayoutControlItem39"
        Me.LayoutControlItem39.Size = New System.Drawing.Size(134, 34)
        Me.LayoutControlItem39.Text = "Qty :"
        Me.LayoutControlItem39.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem39.TextSize = New System.Drawing.Size(25, 13)
        Me.LayoutControlItem39.TextToControlDistance = 5
        '
        'btnAdd
        '
        Me.btnAdd.Appearance.BackColor = System.Drawing.Color.Purple
        Me.btnAdd.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnAdd.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnAdd.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnAdd.Appearance.Options.UseBackColor = True
        Me.btnAdd.Appearance.Options.UseFont = True
        Me.btnAdd.Appearance.Options.UseForeColor = True
        Me.btnAdd.Location = New System.Drawing.Point(576, 36)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(48, 22)
        Me.btnAdd.StyleController = Me.LayoutControl1
        Me.btnAdd.TabIndex = 38
        Me.btnAdd.Text = "Add"
        '
        'btnclear
        '
        Me.btnclear.Appearance.BackColor = System.Drawing.Color.Purple
        Me.btnclear.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnclear.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnclear.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnclear.Appearance.Options.UseBackColor = True
        Me.btnclear.Appearance.Options.UseFont = True
        Me.btnclear.Appearance.Options.UseForeColor = True
        Me.btnclear.Location = New System.Drawing.Point(517, 36)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(55, 22)
        Me.btnclear.StyleController = Me.LayoutControl1
        Me.btnclear.TabIndex = 37
        Me.btnclear.Text = "Clear"
        '
        'txtcostprice
        '
        Me.txtcostprice.EditValue = "0.00"
        Me.txtcostprice.Location = New System.Drawing.Point(351, 86)
        Me.txtcostprice.MenuManager = Me.BarManager1
        Me.txtcostprice.Name = "txtcostprice"
        Me.txtcostprice.Properties.Appearance.Options.UseTextOptions = True
        Me.txtcostprice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtcostprice.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtcostprice.Properties.DisplayFormat.FormatString = "n2"
        Me.txtcostprice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtcostprice.Properties.EditFormat.FormatString = "n2"
        Me.txtcostprice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtcostprice.Properties.Mask.EditMask = "n2"
        Me.txtcostprice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtcostprice.Size = New System.Drawing.Size(61, 20)
        Me.txtcostprice.StyleController = Me.LayoutControl1
        Me.txtcostprice.TabIndex = 34
        '
        'txtnetamt
        '
        Me.txtnetamt.EditValue = "0.00"
        Me.txtnetamt.Location = New System.Drawing.Point(1031, 639)
        Me.txtnetamt.MenuManager = Me.BarManager1
        Me.txtnetamt.Name = "txtnetamt"
        Me.txtnetamt.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.txtnetamt.Properties.Appearance.Options.UseFont = True
        Me.txtnetamt.Properties.Appearance.Options.UseTextOptions = True
        Me.txtnetamt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtnetamt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtnetamt.Properties.DisplayFormat.FormatString = "n2"
        Me.txtnetamt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtnetamt.Properties.EditFormat.FormatString = "n2"
        Me.txtnetamt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtnetamt.Properties.ReadOnly = True
        Me.txtnetamt.Size = New System.Drawing.Size(160, 30)
        Me.txtnetamt.StyleController = Me.LayoutControl1
        Me.txtnetamt.TabIndex = 33
        '
        'txttottax
        '
        Me.txttottax.EditValue = "0.00"
        Me.txttottax.Location = New System.Drawing.Point(1031, 605)
        Me.txttottax.MenuManager = Me.BarManager1
        Me.txttottax.Name = "txttottax"
        Me.txttottax.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.txttottax.Properties.Appearance.Options.UseFont = True
        Me.txttottax.Properties.Appearance.Options.UseTextOptions = True
        Me.txttottax.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txttottax.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txttottax.Properties.DisplayFormat.FormatString = "n2"
        Me.txttottax.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txttottax.Properties.EditFormat.FormatString = "n2"
        Me.txttottax.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txttottax.Properties.ReadOnly = True
        Me.txttottax.Size = New System.Drawing.Size(160, 30)
        Me.txttottax.StyleController = Me.LayoutControl1
        Me.txttottax.TabIndex = 32
        '
        'txttotdiscamt
        '
        Me.txttotdiscamt.EditValue = "0.00"
        Me.txttotdiscamt.Location = New System.Drawing.Point(362, 595)
        Me.txttotdiscamt.MenuManager = Me.BarManager1
        Me.txttotdiscamt.Name = "txttotdiscamt"
        Me.txttotdiscamt.Properties.Appearance.Options.UseTextOptions = True
        Me.txttotdiscamt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txttotdiscamt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txttotdiscamt.Properties.DisplayFormat.FormatString = "n2"
        Me.txttotdiscamt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txttotdiscamt.Properties.EditFormat.FormatString = "n2"
        Me.txttotdiscamt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txttotdiscamt.Properties.Mask.EditMask = "n2"
        Me.txttotdiscamt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txttotdiscamt.Size = New System.Drawing.Size(67, 20)
        Me.txttotdiscamt.StyleController = Me.LayoutControl1
        Me.txttotdiscamt.TabIndex = 31
        '
        'txttotdiscper
        '
        Me.txttotdiscper.EditValue = "0%"
        Me.txttotdiscper.Location = New System.Drawing.Point(363, 571)
        Me.txttotdiscper.MenuManager = Me.BarManager1
        Me.txttotdiscper.Name = "txttotdiscper"
        Me.txttotdiscper.Properties.Appearance.Options.UseTextOptions = True
        Me.txttotdiscper.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txttotdiscper.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txttotdiscper.Properties.DisplayFormat.FormatString = "n"
        Me.txttotdiscper.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txttotdiscper.Properties.EditFormat.FormatString = "n"
        Me.txttotdiscper.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txttotdiscper.Properties.Mask.EditMask = "n"
        Me.txttotdiscper.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txttotdiscper.Size = New System.Drawing.Size(66, 20)
        Me.txttotdiscper.StyleController = Me.LayoutControl1
        Me.txttotdiscper.TabIndex = 30
        '
        'txttaxexcamt
        '
        Me.txttaxexcamt.EditValue = "0.00"
        Me.txttaxexcamt.Location = New System.Drawing.Point(210, 595)
        Me.txttaxexcamt.MenuManager = Me.BarManager1
        Me.txttaxexcamt.Name = "txttaxexcamt"
        Me.txttaxexcamt.Properties.Appearance.Options.UseTextOptions = True
        Me.txttaxexcamt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txttaxexcamt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txttaxexcamt.Properties.DisplayFormat.FormatString = "n2"
        Me.txttaxexcamt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txttaxexcamt.Properties.EditFormat.FormatString = "n2"
        Me.txttaxexcamt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txttaxexcamt.Properties.ReadOnly = True
        Me.txttaxexcamt.Size = New System.Drawing.Size(73, 20)
        Me.txttaxexcamt.StyleController = Me.LayoutControl1
        Me.txttaxexcamt.TabIndex = 29
        '
        'txttaxincamt
        '
        Me.txttaxincamt.EditValue = "0.00"
        Me.txttaxincamt.Location = New System.Drawing.Point(212, 571)
        Me.txttaxincamt.MenuManager = Me.BarManager1
        Me.txttaxincamt.Name = "txttaxincamt"
        Me.txttaxincamt.Properties.Appearance.Options.UseTextOptions = True
        Me.txttaxincamt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txttaxincamt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txttaxincamt.Properties.DisplayFormat.FormatString = "n2"
        Me.txttaxincamt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txttaxincamt.Properties.EditFormat.FormatString = "n2"
        Me.txttaxincamt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txttaxincamt.Properties.ReadOnly = True
        Me.txttaxincamt.Size = New System.Drawing.Size(71, 20)
        Me.txttaxincamt.StyleController = Me.LayoutControl1
        Me.txttaxincamt.TabIndex = 28
        '
        'txttotitem
        '
        Me.txttotitem.EditValue = "0.00"
        Me.txttotitem.Location = New System.Drawing.Point(83, 595)
        Me.txttotitem.MenuManager = Me.BarManager1
        Me.txttotitem.Name = "txttotitem"
        Me.txttotitem.Properties.Appearance.Options.UseTextOptions = True
        Me.txttotitem.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txttotitem.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txttotitem.Properties.DisplayFormat.FormatString = "n2"
        Me.txttotitem.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txttotitem.Properties.EditFormat.FormatString = "n2"
        Me.txttotitem.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txttotitem.Properties.ReadOnly = True
        Me.txttotitem.Size = New System.Drawing.Size(73, 20)
        Me.txttotitem.StyleController = Me.LayoutControl1
        Me.txttotitem.TabIndex = 27
        '
        'txtgrossamt
        '
        Me.txtgrossamt.EditValue = "0.00"
        Me.txtgrossamt.Location = New System.Drawing.Point(1031, 571)
        Me.txtgrossamt.MenuManager = Me.BarManager1
        Me.txtgrossamt.Name = "txtgrossamt"
        Me.txtgrossamt.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.txtgrossamt.Properties.Appearance.Options.UseFont = True
        Me.txtgrossamt.Properties.Appearance.Options.UseTextOptions = True
        Me.txtgrossamt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtgrossamt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtgrossamt.Properties.DisplayFormat.FormatString = "n2"
        Me.txtgrossamt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtgrossamt.Properties.EditFormat.FormatString = "n2"
        Me.txtgrossamt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtgrossamt.Properties.ReadOnly = True
        Me.txtgrossamt.Size = New System.Drawing.Size(160, 30)
        Me.txtgrossamt.StyleController = Me.LayoutControl1
        Me.txtgrossamt.TabIndex = 26
        '
        'txttotqty
        '
        Me.txttotqty.EditValue = "0.00"
        Me.txttotqty.Location = New System.Drawing.Point(82, 571)
        Me.txttotqty.MenuManager = Me.BarManager1
        Me.txttotqty.Name = "txttotqty"
        Me.txttotqty.Properties.Appearance.Options.UseTextOptions = True
        Me.txttotqty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txttotqty.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txttotqty.Properties.DisplayFormat.FormatString = "n2"
        Me.txttotqty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txttotqty.Properties.EditFormat.FormatString = "n2"
        Me.txttotqty.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txttotqty.Properties.ReadOnly = True
        Me.txttotqty.Size = New System.Drawing.Size(74, 20)
        Me.txttotqty.StyleController = Me.LayoutControl1
        Me.txttotqty.TabIndex = 25
        '
        'txtitemnetamt
        '
        Me.txtitemnetamt.EditValue = "0.00"
        Me.txtitemnetamt.Location = New System.Drawing.Point(798, 86)
        Me.txtitemnetamt.MenuManager = Me.BarManager1
        Me.txtitemnetamt.Name = "txtitemnetamt"
        Me.txtitemnetamt.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtitemnetamt.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.txtitemnetamt.Properties.Appearance.Options.UseBackColor = True
        Me.txtitemnetamt.Properties.Appearance.Options.UseForeColor = True
        Me.txtitemnetamt.Properties.Appearance.Options.UseTextOptions = True
        Me.txtitemnetamt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtitemnetamt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtitemnetamt.Properties.DisplayFormat.FormatString = "n2"
        Me.txtitemnetamt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtitemnetamt.Properties.EditFormat.FormatString = "n2"
        Me.txtitemnetamt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtitemnetamt.Properties.Mask.EditMask = "n2"
        Me.txtitemnetamt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtitemnetamt.Size = New System.Drawing.Size(50, 20)
        Me.txtitemnetamt.StyleController = Me.LayoutControl1
        Me.txtitemnetamt.TabIndex = 24
        '
        'txtamount
        '
        Me.txtamount.EditValue = "0.00"
        Me.txtamount.Location = New System.Drawing.Point(1066, 62)
        Me.txtamount.MenuManager = Me.BarManager1
        Me.txtamount.Name = "txtamount"
        Me.txtamount.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtamount.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.txtamount.Properties.Appearance.Options.UseBackColor = True
        Me.txtamount.Properties.Appearance.Options.UseForeColor = True
        Me.txtamount.Properties.Appearance.Options.UseTextOptions = True
        Me.txtamount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtamount.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtamount.Properties.DisplayFormat.FormatString = "n2"
        Me.txtamount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtamount.Properties.EditFormat.FormatString = "n2"
        Me.txtamount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtamount.Properties.Mask.EditMask = "n2"
        Me.txtamount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtamount.Size = New System.Drawing.Size(125, 20)
        Me.txtamount.StyleController = Me.LayoutControl1
        Me.txtamount.TabIndex = 23
        '
        'txtqty
        '
        Me.txtqty.EditValue = "0.00"
        Me.txtqty.Location = New System.Drawing.Point(925, 62)
        Me.txtqty.MenuManager = Me.BarManager1
        Me.txtqty.Name = "txtqty"
        Me.txtqty.Properties.Appearance.Options.UseTextOptions = True
        Me.txtqty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtqty.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtqty.Properties.DisplayFormat.FormatString = "n2"
        Me.txtqty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtqty.Properties.EditFormat.FormatString = "n2"
        Me.txtqty.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtqty.Properties.Mask.EditMask = "n2"
        Me.txtqty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtqty.Size = New System.Drawing.Size(88, 20)
        Me.txtqty.StyleController = Me.LayoutControl1
        Me.txtqty.TabIndex = 22
        '
        'txtpurchaserate
        '
        Me.txtpurchaserate.EditValue = "0.00"
        Me.txtpurchaserate.Location = New System.Drawing.Point(773, 62)
        Me.txtpurchaserate.MenuManager = Me.BarManager1
        Me.txtpurchaserate.Name = "txtpurchaserate"
        Me.txtpurchaserate.Properties.Appearance.Options.UseTextOptions = True
        Me.txtpurchaserate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtpurchaserate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtpurchaserate.Properties.DisplayFormat.FormatString = "n2"
        Me.txtpurchaserate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtpurchaserate.Properties.EditFormat.FormatString = "n2"
        Me.txtpurchaserate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtpurchaserate.Properties.Mask.EditMask = "n2"
        Me.txtpurchaserate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtpurchaserate.Size = New System.Drawing.Size(109, 20)
        Me.txtpurchaserate.StyleController = Me.LayoutControl1
        Me.txtpurchaserate.TabIndex = 21
        '
        'txtbatch
        '
        Me.txtbatch.EditValue = "-"
        Me.txtbatch.Location = New System.Drawing.Point(990, 86)
        Me.txtbatch.MenuManager = Me.BarManager1
        Me.txtbatch.Name = "txtbatch"
        Me.txtbatch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtbatch.Size = New System.Drawing.Size(68, 20)
        Me.txtbatch.StyleController = Me.LayoutControl1
        Me.txtbatch.TabIndex = 20
        '
        'txttaxamt
        '
        Me.txttaxamt.EditValue = "0.00"
        Me.txttaxamt.Location = New System.Drawing.Point(675, 86)
        Me.txttaxamt.MenuManager = Me.BarManager1
        Me.txttaxamt.Name = "txttaxamt"
        Me.txttaxamt.Properties.Appearance.Options.UseTextOptions = True
        Me.txttaxamt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txttaxamt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txttaxamt.Properties.DisplayFormat.FormatString = "n2"
        Me.txttaxamt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txttaxamt.Properties.EditFormat.FormatString = "n2"
        Me.txttaxamt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txttaxamt.Properties.Mask.EditMask = "n2"
        Me.txttaxamt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txttaxamt.Size = New System.Drawing.Size(50, 20)
        Me.txttaxamt.StyleController = Me.LayoutControl1
        Me.txttaxamt.TabIndex = 17
        '
        'txtdisamt
        '
        Me.txtdisamt.EditValue = "0.00"
        Me.txtdisamt.Location = New System.Drawing.Point(245, 86)
        Me.txtdisamt.MenuManager = Me.BarManager1
        Me.txtdisamt.Name = "txtdisamt"
        Me.txtdisamt.Properties.Appearance.Options.UseTextOptions = True
        Me.txtdisamt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtdisamt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtdisamt.Properties.DisplayFormat.FormatString = "n2"
        Me.txtdisamt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtdisamt.Properties.EditFormat.FormatString = "n2"
        Me.txtdisamt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtdisamt.Properties.Mask.EditMask = "n2"
        Me.txtdisamt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtdisamt.Size = New System.Drawing.Size(68, 20)
        Me.txtdisamt.StyleController = Me.LayoutControl1
        Me.txtdisamt.TabIndex = 16
        '
        'txtdiscper
        '
        Me.txtdiscper.EditValue = "0.00"
        Me.txtdiscper.Location = New System.Drawing.Point(138, 86)
        Me.txtdiscper.MenuManager = Me.BarManager1
        Me.txtdiscper.Name = "txtdiscper"
        Me.txtdiscper.Properties.Appearance.Options.UseTextOptions = True
        Me.txtdiscper.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtdiscper.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtdiscper.Properties.DisplayFormat.FormatString = "n"
        Me.txtdiscper.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtdiscper.Properties.EditFormat.FormatString = "n"
        Me.txtdiscper.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtdiscper.Properties.Mask.EditMask = "n"
        Me.txtdiscper.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtdiscper.Size = New System.Drawing.Size(50, 20)
        Me.txtdiscper.StyleController = Me.LayoutControl1
        Me.txtdiscper.TabIndex = 15
        '
        'txtserialno
        '
        Me.txtserialno.Location = New System.Drawing.Point(964, 36)
        Me.txtserialno.MenuManager = Me.BarManager1
        Me.txtserialno.Name = "txtserialno"
        Me.txtserialno.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtserialno.Size = New System.Drawing.Size(227, 20)
        Me.txtserialno.StyleController = Me.LayoutControl1
        Me.txtserialno.TabIndex = 14
        '
        'txtbarcode
        '
        Me.txtbarcode.Location = New System.Drawing.Point(684, 36)
        Me.txtbarcode.MenuManager = Me.BarManager1
        Me.txtbarcode.Name = "txtbarcode"
        Me.txtbarcode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtbarcode.Size = New System.Drawing.Size(222, 20)
        Me.txtbarcode.StyleController = Me.LayoutControl1
        Me.txtbarcode.TabIndex = 13
        '
        'txtitemname
        '
        Me.txtitemname.Location = New System.Drawing.Point(275, 62)
        Me.txtitemname.MenuManager = Me.BarManager1
        Me.txtitemname.Name = "txtitemname"
        Me.txtitemname.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtitemname.Size = New System.Drawing.Size(412, 20)
        Me.txtitemname.StyleController = Me.LayoutControl1
        Me.txtitemname.TabIndex = 12
        '
        'txtitemcode
        '
        Me.txtitemcode.Location = New System.Drawing.Point(138, 62)
        Me.txtitemcode.MenuManager = Me.BarManager1
        Me.txtitemcode.Name = "txtitemcode"
        Me.txtitemcode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtitemcode.Size = New System.Drawing.Size(69, 20)
        Me.txtitemcode.StyleController = Me.LayoutControl1
        Me.txtitemcode.TabIndex = 11
        '
        'GridControl1
        '
        Me.GridControl1.Location = New System.Drawing.Point(12, 126)
        Me.GridControl1.MainView = Me.GridViewPOS
        Me.GridControl1.MenuManager = Me.BarManager1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(1179, 441)
        Me.GridControl1.TabIndex = 9
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewPOS})
        '
        'GridViewPOS
        '
        Me.GridViewPOS.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridViewPOS.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewPOS.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridViewPOS.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewPOS.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumnSNO, Me.GridColumnPURID, Me.GridColumnITEMCODE, Me.GridColumnBARCODE, Me.GridColumnITEMNAME, Me.GridColumnITEMSERIALNO, Me.GridColumnPURRATE, Me.GridColumnPURQTY, Me.GridColumnPURAMT, Me.GridColumnPURDISPER, Me.GridColumnPURDISAMT, Me.GridColumnPURTOTAMT, Me.GridColumnPURCOST, Me.GridColumnPURSELL, Me.GridColumnPURTAXID, Me.GridColumnPURTAXAMT, Me.GridColumnPURGROSSAMT, Me.GridColumnPURROUNDOFF, Me.GridColumnPURNETAMT})
        Me.GridViewPOS.GridControl = Me.GridControl1
        Me.GridViewPOS.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always
        Me.GridViewPOS.Name = "GridViewPOS"
        Me.GridViewPOS.OptionsBehavior.Editable = False
        Me.GridViewPOS.OptionsBehavior.ReadOnly = True
        Me.GridViewPOS.OptionsCustomization.AllowFilter = False
        Me.GridViewPOS.OptionsCustomization.AllowGroup = False
        Me.GridViewPOS.OptionsCustomization.AllowSort = False
        Me.GridViewPOS.OptionsView.ShowGroupPanel = False
        Me.GridViewPOS.RowHeight = 30
        '
        'GridColumnSNO
        '
        Me.GridColumnSNO.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnSNO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumnSNO.Caption = "SNO"
        Me.GridColumnSNO.FieldName = "SNO"
        Me.GridColumnSNO.Name = "GridColumnSNO"
        Me.GridColumnSNO.Visible = True
        Me.GridColumnSNO.VisibleIndex = 0
        '
        'GridColumnPURID
        '
        Me.GridColumnPURID.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnPURID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumnPURID.Caption = "PURID"
        Me.GridColumnPURID.FieldName = "PURID"
        Me.GridColumnPURID.Name = "GridColumnPURID"
        Me.GridColumnPURID.Visible = True
        Me.GridColumnPURID.VisibleIndex = 1
        '
        'GridColumnITEMCODE
        '
        Me.GridColumnITEMCODE.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnITEMCODE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumnITEMCODE.Caption = "ITEMCODE"
        Me.GridColumnITEMCODE.FieldName = "ITEMCODE"
        Me.GridColumnITEMCODE.Name = "GridColumnITEMCODE"
        Me.GridColumnITEMCODE.Visible = True
        Me.GridColumnITEMCODE.VisibleIndex = 2
        '
        'GridColumnBARCODE
        '
        Me.GridColumnBARCODE.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnBARCODE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumnBARCODE.Caption = "BARCODE"
        Me.GridColumnBARCODE.FieldName = "BARCODE"
        Me.GridColumnBARCODE.Name = "GridColumnBARCODE"
        Me.GridColumnBARCODE.Visible = True
        Me.GridColumnBARCODE.VisibleIndex = 3
        '
        'GridColumnITEMNAME
        '
        Me.GridColumnITEMNAME.Caption = "ITEMNAME"
        Me.GridColumnITEMNAME.FieldName = "ITEMNAME"
        Me.GridColumnITEMNAME.Name = "GridColumnITEMNAME"
        Me.GridColumnITEMNAME.Visible = True
        Me.GridColumnITEMNAME.VisibleIndex = 4
        '
        'GridColumnITEMSERIALNO
        '
        Me.GridColumnITEMSERIALNO.Caption = "ITEMSERIALNO"
        Me.GridColumnITEMSERIALNO.FieldName = "ITEMSERIALNO"
        Me.GridColumnITEMSERIALNO.Name = "GridColumnITEMSERIALNO"
        Me.GridColumnITEMSERIALNO.Visible = True
        Me.GridColumnITEMSERIALNO.VisibleIndex = 5
        '
        'GridColumnPURRATE
        '
        Me.GridColumnPURRATE.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnPURRATE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumnPURRATE.Caption = "PURRATE"
        Me.GridColumnPURRATE.FieldName = "PURRATE"
        Me.GridColumnPURRATE.Name = "GridColumnPURRATE"
        Me.GridColumnPURRATE.Visible = True
        Me.GridColumnPURRATE.VisibleIndex = 6
        '
        'GridColumnPURQTY
        '
        Me.GridColumnPURQTY.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnPURQTY.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumnPURQTY.Caption = "PURQTY"
        Me.GridColumnPURQTY.FieldName = "PURQTY"
        Me.GridColumnPURQTY.Name = "GridColumnPURQTY"
        Me.GridColumnPURQTY.Visible = True
        Me.GridColumnPURQTY.VisibleIndex = 7
        '
        'GridColumnPURAMT
        '
        Me.GridColumnPURAMT.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnPURAMT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumnPURAMT.Caption = "PURAMT"
        Me.GridColumnPURAMT.FieldName = "PURAMT"
        Me.GridColumnPURAMT.Name = "GridColumnPURAMT"
        Me.GridColumnPURAMT.Visible = True
        Me.GridColumnPURAMT.VisibleIndex = 8
        '
        'GridColumnPURDISPER
        '
        Me.GridColumnPURDISPER.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnPURDISPER.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumnPURDISPER.Caption = "PURDISPER"
        Me.GridColumnPURDISPER.FieldName = "PURDISPER"
        Me.GridColumnPURDISPER.Name = "GridColumnPURDISPER"
        Me.GridColumnPURDISPER.Visible = True
        Me.GridColumnPURDISPER.VisibleIndex = 9
        '
        'GridColumnPURDISAMT
        '
        Me.GridColumnPURDISAMT.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnPURDISAMT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumnPURDISAMT.Caption = "PURDISAMT"
        Me.GridColumnPURDISAMT.FieldName = "PURDISAMT"
        Me.GridColumnPURDISAMT.Name = "GridColumnPURDISAMT"
        Me.GridColumnPURDISAMT.Visible = True
        Me.GridColumnPURDISAMT.VisibleIndex = 10
        '
        'GridColumnPURTOTAMT
        '
        Me.GridColumnPURTOTAMT.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnPURTOTAMT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumnPURTOTAMT.Caption = "PURTOTAMT"
        Me.GridColumnPURTOTAMT.FieldName = "PURTOTAMT"
        Me.GridColumnPURTOTAMT.Name = "GridColumnPURTOTAMT"
        Me.GridColumnPURTOTAMT.Visible = True
        Me.GridColumnPURTOTAMT.VisibleIndex = 11
        '
        'GridColumnPURCOST
        '
        Me.GridColumnPURCOST.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnPURCOST.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumnPURCOST.Caption = "PURCOST"
        Me.GridColumnPURCOST.FieldName = "PURCOST"
        Me.GridColumnPURCOST.Name = "GridColumnPURCOST"
        Me.GridColumnPURCOST.Visible = True
        Me.GridColumnPURCOST.VisibleIndex = 12
        '
        'GridColumnPURSELL
        '
        Me.GridColumnPURSELL.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnPURSELL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumnPURSELL.Caption = "PURSELL"
        Me.GridColumnPURSELL.FieldName = "PURSELL"
        Me.GridColumnPURSELL.Name = "GridColumnPURSELL"
        Me.GridColumnPURSELL.Visible = True
        Me.GridColumnPURSELL.VisibleIndex = 13
        '
        'GridColumnPURTAXID
        '
        Me.GridColumnPURTAXID.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnPURTAXID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumnPURTAXID.Caption = "PURTAXID"
        Me.GridColumnPURTAXID.FieldName = "PURTAXID"
        Me.GridColumnPURTAXID.Name = "GridColumnPURTAXID"
        Me.GridColumnPURTAXID.Visible = True
        Me.GridColumnPURTAXID.VisibleIndex = 14
        '
        'GridColumnPURTAXAMT
        '
        Me.GridColumnPURTAXAMT.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnPURTAXAMT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumnPURTAXAMT.Caption = "PURTAXAMT"
        Me.GridColumnPURTAXAMT.FieldName = "PURTAXAMT"
        Me.GridColumnPURTAXAMT.Name = "GridColumnPURTAXAMT"
        Me.GridColumnPURTAXAMT.Visible = True
        Me.GridColumnPURTAXAMT.VisibleIndex = 15
        '
        'GridColumnPURGROSSAMT
        '
        Me.GridColumnPURGROSSAMT.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnPURGROSSAMT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumnPURGROSSAMT.Caption = "PURGROSSAMT"
        Me.GridColumnPURGROSSAMT.FieldName = "PURGROSSAMT"
        Me.GridColumnPURGROSSAMT.Name = "GridColumnPURGROSSAMT"
        Me.GridColumnPURGROSSAMT.Visible = True
        Me.GridColumnPURGROSSAMT.VisibleIndex = 16
        '
        'GridColumnPURROUNDOFF
        '
        Me.GridColumnPURROUNDOFF.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnPURROUNDOFF.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumnPURROUNDOFF.Caption = "PURROUNDOFF"
        Me.GridColumnPURROUNDOFF.FieldName = "PURROUNDOFF"
        Me.GridColumnPURROUNDOFF.Name = "GridColumnPURROUNDOFF"
        Me.GridColumnPURROUNDOFF.Visible = True
        Me.GridColumnPURROUNDOFF.VisibleIndex = 17
        '
        'GridColumnPURNETAMT
        '
        Me.GridColumnPURNETAMT.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnPURNETAMT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumnPURNETAMT.Caption = "PURNETAMT"
        Me.GridColumnPURNETAMT.FieldName = "PURNETAMT"
        Me.GridColumnPURNETAMT.Name = "GridColumnPURNETAMT"
        Me.GridColumnPURNETAMT.Visible = True
        Me.GridColumnPURNETAMT.VisibleIndex = 18
        '
        'txtrefno
        '
        Me.txtrefno.Location = New System.Drawing.Point(1115, 12)
        Me.txtrefno.MenuManager = Me.BarManager1
        Me.txtrefno.Name = "txtrefno"
        Me.txtrefno.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtrefno.Size = New System.Drawing.Size(76, 20)
        Me.txtrefno.StyleController = Me.LayoutControl1
        Me.txtrefno.TabIndex = 8
        '
        'txtinvoiceno
        '
        Me.txtinvoiceno.Location = New System.Drawing.Point(822, 12)
        Me.txtinvoiceno.MenuManager = Me.BarManager1
        Me.txtinvoiceno.Name = "txtinvoiceno"
        Me.txtinvoiceno.Properties.Appearance.Options.UseTextOptions = True
        Me.txtinvoiceno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtinvoiceno.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtinvoiceno.Properties.ReadOnly = True
        Me.txtinvoiceno.Size = New System.Drawing.Size(84, 20)
        Me.txtinvoiceno.StyleController = Me.LayoutControl1
        Me.txtinvoiceno.TabIndex = 7
        '
        'txtpaymenttype
        '
        Me.txtpaymenttype.Location = New System.Drawing.Point(513, 12)
        Me.txtpaymenttype.MenuManager = Me.BarManager1
        Me.txtpaymenttype.Name = "txtpaymenttype"
        Me.txtpaymenttype.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtpaymenttype.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtpaymenttype.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", 80, "Name")})
        Me.txtpaymenttype.Properties.DisplayMember = "Name"
        Me.txtpaymenttype.Properties.NullText = ""
        Me.txtpaymenttype.Properties.ValueMember = "Id"
        Me.txtpaymenttype.Size = New System.Drawing.Size(80, 20)
        Me.txtpaymenttype.StyleController = Me.LayoutControl1
        Me.txtpaymenttype.TabIndex = 5
        '
        'txtinvoicedate
        '
        Me.txtinvoicedate.EditValue = Nothing
        Me.txtinvoicedate.Location = New System.Drawing.Point(670, 12)
        Me.txtinvoicedate.MenuManager = Me.BarManager1
        Me.txtinvoicedate.Name = "txtinvoicedate"
        Me.txtinvoicedate.Properties.Appearance.Options.UseTextOptions = True
        Me.txtinvoicedate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtinvoicedate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtinvoicedate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtinvoicedate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtinvoicedate.Properties.CalendarTimeProperties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
        Me.txtinvoicedate.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.[Default]
        Me.txtinvoicedate.Properties.Mask.EditMask = ""
        Me.txtinvoicedate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.txtinvoicedate.Size = New System.Drawing.Size(85, 20)
        Me.txtinvoicedate.StyleController = Me.LayoutControl1
        Me.txtinvoicedate.TabIndex = 6
        '
        'txtpurchasedate
        '
        Me.txtpurchasedate.EditValue = Nothing
        Me.txtpurchasedate.Location = New System.Drawing.Point(992, 12)
        Me.txtpurchasedate.MenuManager = Me.BarManager1
        Me.txtpurchasedate.Name = "txtpurchasedate"
        Me.txtpurchasedate.Properties.Appearance.Options.UseTextOptions = True
        Me.txtpurchasedate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtpurchasedate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtpurchasedate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtpurchasedate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtpurchasedate.Properties.CalendarTimeProperties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
        Me.txtpurchasedate.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.[Default]
        Me.txtpurchasedate.Properties.Mask.EditMask = ""
        Me.txtpurchasedate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.txtpurchasedate.Size = New System.Drawing.Size(74, 20)
        Me.txtpurchasedate.StyleController = Me.LayoutControl1
        Me.txtpurchasedate.TabIndex = 35
        '
        'txtexpire
        '
        Me.txtexpire.EditValue = Nothing
        Me.txtexpire.Location = New System.Drawing.Point(1112, 86)
        Me.txtexpire.MenuManager = Me.BarManager1
        Me.txtexpire.Name = "txtexpire"
        Me.txtexpire.Properties.Appearance.Options.UseTextOptions = True
        Me.txtexpire.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtexpire.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtexpire.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtexpire.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtexpire.Properties.CalendarTimeProperties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
        Me.txtexpire.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.[Default]
        Me.txtexpire.Properties.Mask.EditMask = ""
        Me.txtexpire.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.txtexpire.Size = New System.Drawing.Size(79, 20)
        Me.txtexpire.StyleController = Me.LayoutControl1
        Me.txtexpire.TabIndex = 19
        '
        'txtsupplier
        '
        Me.txtsupplier.Location = New System.Drawing.Point(138, 12)
        Me.txtsupplier.MenuManager = Me.BarManager1
        Me.txtsupplier.Name = "txtsupplier"
        Me.txtsupplier.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtsupplier.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtsupplier.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierName", 80, "SupplierName")})
        Me.txtsupplier.Properties.DisplayMember = "SupplierName"
        Me.txtsupplier.Properties.NullText = ""
        Me.txtsupplier.Properties.ValueMember = "Id"
        Me.txtsupplier.Size = New System.Drawing.Size(290, 20)
        Me.txtsupplier.StyleController = Me.LayoutControl1
        Me.txtsupplier.TabIndex = 4
        '
        'txttaxid
        '
        Me.txttaxid.Location = New System.Drawing.Point(554, 86)
        Me.txttaxid.MenuManager = Me.BarManager1
        Me.txttaxid.Name = "txttaxid"
        Me.txttaxid.Properties.Appearance.Options.UseTextOptions = True
        Me.txttaxid.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txttaxid.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txttaxid.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txttaxid.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("TaxId", "TaxId"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("TaxName", 80, "TaxName")})
        Me.txttaxid.Properties.DisplayMember = "TaxName"
        Me.txttaxid.Properties.NullText = ""
        Me.txttaxid.Properties.ValueMember = "TaxId"
        Me.txttaxid.Size = New System.Drawing.Size(65, 20)
        Me.txttaxid.StyleController = Me.LayoutControl1
        Me.txttaxid.TabIndex = 36
        '
        'txtunit
        '
        Me.txtunit.Location = New System.Drawing.Point(883, 86)
        Me.txtunit.MenuManager = Me.BarManager1
        Me.txtunit.Name = "txtunit"
        Me.txtunit.Properties.Appearance.Options.UseTextOptions = True
        Me.txtunit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtunit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtunit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtunit.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("UnitName", 80, "UnitName")})
        Me.txtunit.Properties.DisplayMember = "UnitName"
        Me.txtunit.Properties.NullText = ""
        Me.txtunit.Properties.PopupSizeable = False
        Me.txtunit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.txtunit.Properties.ValueMember = "Id"
        Me.txtunit.Size = New System.Drawing.Size(64, 20)
        Me.txtunit.StyleController = Me.LayoutControl1
        Me.txtunit.TabIndex = 18
        '
        'cmbMaterialSearch
        '
        Me.cmbMaterialSearch.Location = New System.Drawing.Point(138, 36)
        Me.cmbMaterialSearch.MenuManager = Me.BarManager1
        Me.cmbMaterialSearch.Name = "cmbMaterialSearch"
        Me.cmbMaterialSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbMaterialSearch.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMaterialSearch.Properties.PopupControl = Me.PopupContainerControl1
        Me.cmbMaterialSearch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cmbMaterialSearch.Size = New System.Drawing.Size(375, 20)
        Me.cmbMaterialSearch.StyleController = Me.LayoutControl1
        Me.cmbMaterialSearch.TabIndex = 10
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseBackColor = True
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem6, Me.LayoutControlItem7, Me.LayoutControlItem8, Me.LayoutControlItem9, Me.LayoutControlItem12, Me.LayoutControlItem10, Me.LayoutControlItem11, Me.LayoutControlItem18, Me.LayoutControlItem19, Me.LayoutControlItem20, Me.LayoutControlItem13, Me.LayoutControlItem14, Me.LayoutControlItem21, Me.LayoutControlItem15, Me.LayoutControlItem17, Me.LayoutControlItem16, Me.LayoutControlItem22, Me.LayoutControlItem23, Me.LayoutControlItem24, Me.LayoutControlItem25, Me.LayoutControlItem26, Me.LayoutControlItem27, Me.LayoutControlItem28, Me.LayoutControlItem29, Me.LayoutControlItem30, Me.LayoutControlItem31, Me.EmptySpaceItem2, Me.LayoutControlItem33, Me.LayoutControlItem34, Me.EmptySpaceItem1, Me.EmptySpaceItem4, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem32, Me.LayoutControlItem5, Me.LayoutControlItem35, Me.LayoutControlItem40, Me.EmptySpaceItem3, Me.LayoutControlItem41})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1203, 681)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.txtsupplier
        Me.LayoutControlItem1.CustomizationFormText = "Supplier Name :"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(420, 24)
        Me.LayoutControlItem1.Text = "Supplier Name :"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(123, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.GridControl1
        Me.LayoutControlItem6.CustomizationFormText = "Purchase Details "
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 98)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(1183, 461)
        Me.LayoutControlItem6.Text = "Purchase Details "
        Me.LayoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(123, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.cmbMaterialSearch
        Me.LayoutControlItem7.CustomizationFormText = "Seach Item"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(505, 26)
        Me.LayoutControlItem7.Text = "Seach Item :"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(123, 13)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.txtitemcode
        Me.LayoutControlItem8.CustomizationFormText = "Item Code :"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 50)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(199, 24)
        Me.LayoutControlItem8.Text = "Item Code :"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(123, 13)
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem9.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem9.Control = Me.txtitemname
        Me.LayoutControlItem9.CustomizationFormText = "Item Name :"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(199, 50)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(480, 24)
        Me.LayoutControlItem9.Text = "Item Name :"
        Me.LayoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(59, 13)
        Me.LayoutControlItem9.TextToControlDistance = 5
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.txtdiscper
        Me.LayoutControlItem12.CustomizationFormText = "Purchase Rate :"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(0, 74)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(180, 24)
        Me.LayoutControlItem12.Text = "Disc % :"
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(123, 13)
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem10.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem10.Control = Me.txtbarcode
        Me.LayoutControlItem10.CustomizationFormText = "Bar Code :"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(616, 24)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(282, 26)
        Me.LayoutControlItem10.Text = "Bar Code :"
        Me.LayoutControlItem10.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(51, 13)
        Me.LayoutControlItem10.TextToControlDistance = 5
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem11.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem11.Control = Me.txtserialno
        Me.LayoutControlItem11.CustomizationFormText = "Serial No :"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(898, 24)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(285, 26)
        Me.LayoutControlItem11.Text = "Serial No :"
        Me.LayoutControlItem11.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(49, 13)
        Me.LayoutControlItem11.TextToControlDistance = 5
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem18.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem18.Control = Me.txtpurchaserate
        Me.LayoutControlItem18.CustomizationFormText = "Purchase Rate :"
        Me.LayoutControlItem18.Location = New System.Drawing.Point(679, 50)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Size = New System.Drawing.Size(195, 24)
        Me.LayoutControlItem18.Text = "Purchase Rate :"
        Me.LayoutControlItem18.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(77, 13)
        Me.LayoutControlItem18.TextToControlDistance = 5
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem19.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem19.Control = Me.txtqty
        Me.LayoutControlItem19.CustomizationFormText = "Qty :"
        Me.LayoutControlItem19.Location = New System.Drawing.Point(874, 50)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(131, 24)
        Me.LayoutControlItem19.Text = "Qty    :"
        Me.LayoutControlItem19.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(34, 13)
        Me.LayoutControlItem19.TextToControlDistance = 5
        '
        'LayoutControlItem20
        '
        Me.LayoutControlItem20.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem20.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem20.Control = Me.txtamount
        Me.LayoutControlItem20.CustomizationFormText = "Amount :"
        Me.LayoutControlItem20.Location = New System.Drawing.Point(1005, 50)
        Me.LayoutControlItem20.Name = "LayoutControlItem20"
        Me.LayoutControlItem20.Size = New System.Drawing.Size(178, 24)
        Me.LayoutControlItem20.Text = "Amount :"
        Me.LayoutControlItem20.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem20.TextSize = New System.Drawing.Size(44, 13)
        Me.LayoutControlItem20.TextToControlDistance = 5
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem13.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem13.Control = Me.txtdisamt
        Me.LayoutControlItem13.CustomizationFormText = "Qty :"
        Me.LayoutControlItem13.Location = New System.Drawing.Point(180, 74)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(125, 24)
        Me.LayoutControlItem13.Text = "Disc Amt :"
        Me.LayoutControlItem13.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(48, 13)
        Me.LayoutControlItem13.TextToControlDistance = 5
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem14.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem14.Control = Me.txttaxamt
        Me.LayoutControlItem14.CustomizationFormText = "Total Amount :"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(611, 74)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(106, 24)
        Me.LayoutControlItem14.Text = "Tax Amt :"
        Me.LayoutControlItem14.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(47, 13)
        Me.LayoutControlItem14.TextToControlDistance = 5
        '
        'LayoutControlItem21
        '
        Me.LayoutControlItem21.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem21.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem21.Control = Me.txtitemnetamt
        Me.LayoutControlItem21.CustomizationFormText = "Net Amount :"
        Me.LayoutControlItem21.Location = New System.Drawing.Point(717, 74)
        Me.LayoutControlItem21.Name = "LayoutControlItem21"
        Me.LayoutControlItem21.Size = New System.Drawing.Size(123, 24)
        Me.LayoutControlItem21.Text = "Net Amount :"
        Me.LayoutControlItem21.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem21.TextSize = New System.Drawing.Size(64, 13)
        Me.LayoutControlItem21.TextToControlDistance = 5
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem15.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem15.Control = Me.txtunit
        Me.LayoutControlItem15.CustomizationFormText = "Unit :"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(840, 74)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(99, 24)
        Me.LayoutControlItem15.Text = "Unit :"
        Me.LayoutControlItem15.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(26, 13)
        Me.LayoutControlItem15.TextToControlDistance = 5
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem17.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem17.Control = Me.txtbatch
        Me.LayoutControlItem17.CustomizationFormText = "Batch :"
        Me.LayoutControlItem17.Location = New System.Drawing.Point(939, 74)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(111, 24)
        Me.LayoutControlItem17.Text = "Batch :"
        Me.LayoutControlItem17.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(34, 13)
        Me.LayoutControlItem17.TextToControlDistance = 5
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem16.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem16.Control = Me.txtexpire
        Me.LayoutControlItem16.CustomizationFormText = "Expiry Date :"
        Me.LayoutControlItem16.Location = New System.Drawing.Point(1050, 74)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(133, 24)
        Me.LayoutControlItem16.Text = "ExpDate:"
        Me.LayoutControlItem16.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(45, 13)
        Me.LayoutControlItem16.TextToControlDistance = 5
        '
        'LayoutControlItem22
        '
        Me.LayoutControlItem22.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem22.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem22.Control = Me.txttotqty
        Me.LayoutControlItem22.CustomizationFormText = "Total Qty :"
        Me.LayoutControlItem22.Location = New System.Drawing.Point(10, 559)
        Me.LayoutControlItem22.Name = "LayoutControlItem22"
        Me.LayoutControlItem22.Size = New System.Drawing.Size(138, 24)
        Me.LayoutControlItem22.Text = "Total Qty  :"
        Me.LayoutControlItem22.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem22.TextSize = New System.Drawing.Size(55, 13)
        Me.LayoutControlItem22.TextToControlDistance = 5
        '
        'LayoutControlItem23
        '
        Me.LayoutControlItem23.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LayoutControlItem23.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem23.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem23.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem23.Control = Me.txtgrossamt
        Me.LayoutControlItem23.ControlAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.LayoutControlItem23.CustomizationFormText = "Total Amount :"
        Me.LayoutControlItem23.Location = New System.Drawing.Point(893, 559)
        Me.LayoutControlItem23.Name = "LayoutControlItem23"
        Me.LayoutControlItem23.Size = New System.Drawing.Size(290, 34)
        Me.LayoutControlItem23.Text = "Total Amount :"
        Me.LayoutControlItem23.TextLocation = DevExpress.Utils.Locations.[Default]
        Me.LayoutControlItem23.TextSize = New System.Drawing.Size(123, 19)
        '
        'LayoutControlItem24
        '
        Me.LayoutControlItem24.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem24.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem24.Control = Me.txttotitem
        Me.LayoutControlItem24.CustomizationFormText = "Total Item :"
        Me.LayoutControlItem24.Location = New System.Drawing.Point(10, 583)
        Me.LayoutControlItem24.Name = "LayoutControlItem24"
        Me.LayoutControlItem24.Size = New System.Drawing.Size(138, 24)
        Me.LayoutControlItem24.Text = "Total Item :"
        Me.LayoutControlItem24.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem24.TextSize = New System.Drawing.Size(56, 13)
        Me.LayoutControlItem24.TextToControlDistance = 5
        '
        'LayoutControlItem25
        '
        Me.LayoutControlItem25.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem25.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem25.Control = Me.txttaxincamt
        Me.LayoutControlItem25.CustomizationFormText = "Tax Inc."
        Me.LayoutControlItem25.Location = New System.Drawing.Point(148, 559)
        Me.LayoutControlItem25.Name = "LayoutControlItem25"
        Me.LayoutControlItem25.Size = New System.Drawing.Size(127, 24)
        Me.LayoutControlItem25.Text = "Tax Inc. :"
        Me.LayoutControlItem25.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem25.TextSize = New System.Drawing.Size(47, 13)
        Me.LayoutControlItem25.TextToControlDistance = 5
        '
        'LayoutControlItem26
        '
        Me.LayoutControlItem26.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem26.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem26.Control = Me.txttaxexcamt
        Me.LayoutControlItem26.CustomizationFormText = "Tax Exc."
        Me.LayoutControlItem26.Location = New System.Drawing.Point(148, 583)
        Me.LayoutControlItem26.Name = "LayoutControlItem26"
        Me.LayoutControlItem26.Size = New System.Drawing.Size(127, 24)
        Me.LayoutControlItem26.Text = "Tax Exc: "
        Me.LayoutControlItem26.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem26.TextSize = New System.Drawing.Size(45, 13)
        Me.LayoutControlItem26.TextToControlDistance = 5
        '
        'LayoutControlItem27
        '
        Me.LayoutControlItem27.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem27.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem27.Control = Me.txttotdiscper
        Me.LayoutControlItem27.CustomizationFormText = "Tot Disc :"
        Me.LayoutControlItem27.Location = New System.Drawing.Point(275, 559)
        Me.LayoutControlItem27.Name = "LayoutControlItem27"
        Me.LayoutControlItem27.Size = New System.Drawing.Size(146, 24)
        Me.LayoutControlItem27.Text = "Tot Disc Per %"
        Me.LayoutControlItem27.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem27.TextSize = New System.Drawing.Size(71, 13)
        Me.LayoutControlItem27.TextToControlDistance = 5
        '
        'LayoutControlItem28
        '
        Me.LayoutControlItem28.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem28.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem28.Control = Me.txttotdiscamt
        Me.LayoutControlItem28.CustomizationFormText = "Tot Disc Amt :"
        Me.LayoutControlItem28.Location = New System.Drawing.Point(275, 583)
        Me.LayoutControlItem28.Name = "LayoutControlItem28"
        Me.LayoutControlItem28.Size = New System.Drawing.Size(146, 24)
        Me.LayoutControlItem28.Text = "Tot Disc Amt  :"
        Me.LayoutControlItem28.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem28.TextSize = New System.Drawing.Size(70, 13)
        Me.LayoutControlItem28.TextToControlDistance = 5
        '
        'LayoutControlItem29
        '
        Me.LayoutControlItem29.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LayoutControlItem29.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem29.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem29.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem29.Control = Me.txttottax
        Me.LayoutControlItem29.CustomizationFormText = "Total Tax :"
        Me.LayoutControlItem29.Location = New System.Drawing.Point(893, 593)
        Me.LayoutControlItem29.Name = "LayoutControlItem29"
        Me.LayoutControlItem29.Size = New System.Drawing.Size(290, 34)
        Me.LayoutControlItem29.Text = "Total Tax :"
        Me.LayoutControlItem29.TextSize = New System.Drawing.Size(123, 19)
        '
        'LayoutControlItem30
        '
        Me.LayoutControlItem30.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LayoutControlItem30.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem30.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem30.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem30.Control = Me.txtnetamt
        Me.LayoutControlItem30.CustomizationFormText = "Net Amount :"
        Me.LayoutControlItem30.Location = New System.Drawing.Point(893, 627)
        Me.LayoutControlItem30.Name = "LayoutControlItem30"
        Me.LayoutControlItem30.Size = New System.Drawing.Size(290, 34)
        Me.LayoutControlItem30.Text = "Net Amount :"
        Me.LayoutControlItem30.TextSize = New System.Drawing.Size(123, 19)
        '
        'LayoutControlItem31
        '
        Me.LayoutControlItem31.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem31.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem31.Control = Me.txtcostprice
        Me.LayoutControlItem31.CustomizationFormText = "Cost :"
        Me.LayoutControlItem31.Location = New System.Drawing.Point(305, 74)
        Me.LayoutControlItem31.Name = "LayoutControlItem31"
        Me.LayoutControlItem31.Size = New System.Drawing.Size(99, 24)
        Me.LayoutControlItem31.Text = "Cost :"
        Me.LayoutControlItem31.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem31.TextSize = New System.Drawing.Size(29, 13)
        Me.LayoutControlItem31.TextToControlDistance = 5
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(10, 607)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(411, 54)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem33
        '
        Me.LayoutControlItem33.Control = Me.txttaxid
        Me.LayoutControlItem33.CustomizationFormText = "Tax Id :"
        Me.LayoutControlItem33.Location = New System.Drawing.Point(499, 74)
        Me.LayoutControlItem33.Name = "LayoutControlItem33"
        Me.LayoutControlItem33.Size = New System.Drawing.Size(112, 24)
        Me.LayoutControlItem33.Text = "Tax Id :"
        Me.LayoutControlItem33.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem33.TextSize = New System.Drawing.Size(38, 13)
        Me.LayoutControlItem33.TextToControlDistance = 5
        '
        'LayoutControlItem34
        '
        Me.LayoutControlItem34.Control = Me.btnclear
        Me.LayoutControlItem34.CustomizationFormText = "LayoutControlItem34"
        Me.LayoutControlItem34.Location = New System.Drawing.Point(505, 24)
        Me.LayoutControlItem34.Name = "LayoutControlItem34"
        Me.LayoutControlItem34.Size = New System.Drawing.Size(59, 26)
        Me.LayoutControlItem34.Text = "LayoutControlItem34"
        Me.LayoutControlItem34.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem34.TextToControlDistance = 0
        Me.LayoutControlItem34.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 559)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(10, 102)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.AllowHotTrack = False
        Me.EmptySpaceItem4.CustomizationFormText = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(421, 559)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(313, 102)
        Me.EmptySpaceItem4.Text = "EmptySpaceItem4"
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.txtpaymenttype
        Me.LayoutControlItem2.CustomizationFormText = "Payment Type :"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(420, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(165, 24)
        Me.LayoutControlItem2.Text = "Payment Type :"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(76, 13)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem3.Control = Me.txtinvoicedate
        Me.LayoutControlItem3.CustomizationFormText = "Invoice Date :"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(585, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(162, 24)
        Me.LayoutControlItem3.Text = "Invoice Date :"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(68, 13)
        Me.LayoutControlItem3.TextToControlDistance = 5
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem4.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem4.Control = Me.txtinvoiceno
        Me.LayoutControlItem4.CustomizationFormText = "Invoice No :"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(747, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(151, 24)
        Me.LayoutControlItem4.Text = "Invoice No :"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(58, 13)
        Me.LayoutControlItem4.TextToControlDistance = 5
        '
        'LayoutControlItem32
        '
        Me.LayoutControlItem32.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem32.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem32.Control = Me.txtpurchasedate
        Me.LayoutControlItem32.CustomizationFormText = "Purchase Date :"
        Me.LayoutControlItem32.Location = New System.Drawing.Point(898, 0)
        Me.LayoutControlItem32.Name = "LayoutControlItem32"
        Me.LayoutControlItem32.Size = New System.Drawing.Size(160, 24)
        Me.LayoutControlItem32.Text = "Purchase Date :"
        Me.LayoutControlItem32.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem32.TextSize = New System.Drawing.Size(77, 13)
        Me.LayoutControlItem32.TextToControlDistance = 5
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem5.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem5.Control = Me.txtrefno
        Me.LayoutControlItem5.CustomizationFormText = "Ref No :"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(1058, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(125, 24)
        Me.LayoutControlItem5.Text = "Ref No :"
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(40, 13)
        Me.LayoutControlItem5.TextToControlDistance = 5
        '
        'LayoutControlItem35
        '
        Me.LayoutControlItem35.Control = Me.btnAdd
        Me.LayoutControlItem35.CustomizationFormText = "LayoutControlItem35"
        Me.LayoutControlItem35.Location = New System.Drawing.Point(564, 24)
        Me.LayoutControlItem35.Name = "LayoutControlItem35"
        Me.LayoutControlItem35.Size = New System.Drawing.Size(52, 26)
        Me.LayoutControlItem35.Text = "LayoutControlItem35"
        Me.LayoutControlItem35.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem35.TextToControlDistance = 0
        Me.LayoutControlItem35.TextVisible = False
        '
        'LayoutControlItem40
        '
        Me.LayoutControlItem40.Control = Me.txtroundoff
        Me.LayoutControlItem40.CustomizationFormText = "Round Off :"
        Me.LayoutControlItem40.Location = New System.Drawing.Point(734, 559)
        Me.LayoutControlItem40.Name = "LayoutControlItem40"
        Me.LayoutControlItem40.Size = New System.Drawing.Size(159, 24)
        Me.LayoutControlItem40.Text = "Round Off :"
        Me.LayoutControlItem40.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem40.TextSize = New System.Drawing.Size(57, 13)
        Me.LayoutControlItem40.TextToControlDistance = 5
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(734, 583)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(159, 78)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem41
        '
        Me.LayoutControlItem41.Control = Me.txtsell
        Me.LayoutControlItem41.CustomizationFormText = "Sell :"
        Me.LayoutControlItem41.Location = New System.Drawing.Point(404, 74)
        Me.LayoutControlItem41.Name = "LayoutControlItem41"
        Me.LayoutControlItem41.Size = New System.Drawing.Size(95, 24)
        Me.LayoutControlItem41.Text = "Sell :"
        Me.LayoutControlItem41.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem41.TextSize = New System.Drawing.Size(23, 13)
        Me.LayoutControlItem41.TextToControlDistance = 5
        '
        'LayoutControlItem36
        '
        Me.LayoutControlItem36.AppearanceItemCaption.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LayoutControlItem36.AppearanceItemCaption.Options.UseBackColor = True
        Me.LayoutControlItem36.Control = Me.txtMqty
        Me.LayoutControlItem36.CustomizationFormText = "Qty :"
        Me.LayoutControlItem36.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem36.Name = "LayoutControlItem18"
        Me.LayoutControlItem36.Size = New System.Drawing.Size(121, 34)
        Me.LayoutControlItem36.Text = "Qty :"
        Me.LayoutControlItem36.TextSize = New System.Drawing.Size(25, 13)
        Me.LayoutControlItem36.TextToControlDistance = 5
        '
        'FrmPurchase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1203, 729)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmPurchase"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FrmPurchase"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtsell.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarAndDockingController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtroundoff.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PopupContainerControl1.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.txtMqty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsearch2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcostprice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtnetamt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttottax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttotdiscamt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttotdiscper.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttaxexcamt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttaxincamt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttotitem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtgrossamt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttotqty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtitemnetamt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtamount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtqty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpurchaserate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbatch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttaxamt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdisamt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdiscper.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtserialno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbarcode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtitemname.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtitemcode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewPOS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtrefno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtinvoiceno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpaymenttype.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtinvoicedate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtinvoicedate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpurchasedate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpurchasedate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtexpire.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtexpire.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsupplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttaxid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtunit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbMaterialSearch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem36, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents barbtnExit As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Bar3 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewPOS As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtrefno As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtinvoiceno As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtserialno As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtbarcode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtitemname As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtitemcode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtbatch As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txttaxamt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtdisamt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtdiscper As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtitemnetamt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtamount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtqty As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtpurchaserate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem20 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem21 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtcostprice As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtnetamt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txttottax As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txttotdiscamt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txttotdiscper As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txttaxexcamt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txttaxincamt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txttotitem As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtgrossamt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txttotqty As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem22 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem23 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem24 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem25 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem26 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem27 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem28 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem29 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem30 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem31 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem32 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtpaymenttype As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtinvoicedate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtpurchasedate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtexpire As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtsupplier As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents btnclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem33 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem34 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txttaxid As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents BarAndDockingController1 As DevExpress.XtraBars.BarAndDockingController
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents txtunit As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem35 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PopupContainerControl1 As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents cmbMaterialSearch As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents txtMqty As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GridControl2 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumnProduct As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnSalesPriceSearch As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnWholesaleRate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnProductCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtsearch2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem37 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem38 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem39 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem36 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtroundoff As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem40 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents GridColumnSNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnPURID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnITEMCODE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnBARCODE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnITEMNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnITEMSERIALNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnPURRATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnPURQTY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnPURAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnPURDISPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnPURDISAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnPURTOTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnPURCOST As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnPURSELL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnPURTAXID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnPURTAXAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnPURGROSSAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnPURROUNDOFF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnPURNETAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtsell As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem41 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BarSubItem1 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents btnSaveGridViewPosLayout As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnsavePurchase As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnViewPurchase As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnreset As DevExpress.XtraBars.BarButtonItem
End Class
