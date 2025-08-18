<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPosRetailBilling
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPosRetailBilling))

        ' Main Layout Control
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()

        ' Welcome Banner
        Me.lblWelcome = New DevExpress.XtraEditors.LabelControl()
        Me.layoutItemWelcome = New DevExpress.XtraLayout.LayoutControlItem()

        ' Top Section Controls
        Me.txtCustomer = New DevExpress.XtraEditors.ButtonEdit()
        Me.btnCustomerLookup = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbBranch = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbOptions = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtSalesperson = New DevExpress.XtraEditors.ButtonEdit()
        Me.btnSalespersonLookup = New DevExpress.XtraEditors.SimpleButton()
        Me.txtPaymentTerm = New DevExpress.XtraEditors.TextEdit()
        Me.btnCash = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSales = New DevExpress.XtraEditors.SimpleButton()

        ' Barcode Section
        Me.txtBarcode = New DevExpress.XtraEditors.ButtonEdit()
        Me.btnBarcodeSearch = New DevExpress.XtraEditors.SimpleButton()

        ' Grid Control for Items
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colItemNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colUOM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDiscount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colUPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPicture = New DevExpress.XtraGrid.Columns.GridColumn()

        ' Action Buttons
        Me.btnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDiscount50 = New DevExpress.XtraEditors.SimpleButton()
        Me.btnUPrice = New DevExpress.XtraEditors.SimpleButton()

        ' Bottom Section Controls
        Me.txtSalesOrder = New DevExpress.XtraEditors.ButtonEdit()
        Me.cmbPriceLevel = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtSubTotal = New DevExpress.XtraEditors.TextEdit()
        Me.txtRounding = New DevExpress.XtraEditors.TextEdit()
        Me.txtDiscount = New DevExpress.XtraEditors.ButtonEdit()
        Me.txtTax = New DevExpress.XtraEditors.ButtonEdit()
        Me.lblTotal = New DevExpress.XtraEditors.LabelControl()

        ' Right Side Function Buttons
        Me.btnPayment = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClearF1 = New DevExpress.XtraEditors.SimpleButton()
        Me.btnUpDown = New DevExpress.XtraEditors.SimpleButton()
        Me.btnHoldBill = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSalesPerson = New DevExpress.XtraEditors.SimpleButton()
        Me.btnFOC = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDeposit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnLastBill = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDelOrder = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDrawer = New DevExpress.XtraEditors.SimpleButton()
        Me.btnExit = New DevExpress.XtraEditors.SimpleButton()

        ' Layout Items
        Me.layoutGroupTop = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.layoutGroupMain = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.layoutGroupBottom = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.layoutGroupButtons = New DevExpress.XtraLayout.LayoutControlGroup()

        Me.layoutItemCustomer = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutItemBranch = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutItemOptions = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutItemSalesperson = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutItemPaymentTerm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutItemCash = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutItemSales = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutItemBarcode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutItemGrid = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutItemSalesOrder = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutItemPriceLevel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutItemSubTotal = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutItemRounding = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutItemDiscount = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutItemTax = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutItemTotal = New DevExpress.XtraLayout.LayoutControlItem()

        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBranch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOptions.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalesperson.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPaymentTerm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBarcode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalesOrder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPriceLevel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRounding.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiscount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        '
        ' LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.lblWelcome)
        Me.LayoutControl1.Controls.Add(Me.txtCustomer)
        Me.LayoutControl1.Controls.Add(Me.btnCustomerLookup)
        Me.LayoutControl1.Controls.Add(Me.cmbBranch)
        Me.LayoutControl1.Controls.Add(Me.cmbOptions)
        Me.LayoutControl1.Controls.Add(Me.txtSalesperson)
        Me.LayoutControl1.Controls.Add(Me.btnSalespersonLookup)
        Me.LayoutControl1.Controls.Add(Me.txtPaymentTerm)
        Me.LayoutControl1.Controls.Add(Me.btnCash)
        Me.LayoutControl1.Controls.Add(Me.btnSales)
        Me.LayoutControl1.Controls.Add(Me.txtBarcode)
        Me.LayoutControl1.Controls.Add(Me.btnBarcodeSearch)
        Me.LayoutControl1.Controls.Add(Me.GridControl1)
        Me.LayoutControl1.Controls.Add(Me.btnEdit)
        Me.LayoutControl1.Controls.Add(Me.btnDelete)
        Me.LayoutControl1.Controls.Add(Me.btnDiscount50)
        Me.LayoutControl1.Controls.Add(Me.btnUPrice)
        Me.LayoutControl1.Controls.Add(Me.txtSalesOrder)
        Me.LayoutControl1.Controls.Add(Me.cmbPriceLevel)
        Me.LayoutControl1.Controls.Add(Me.txtSubTotal)
        Me.LayoutControl1.Controls.Add(Me.txtRounding)
        Me.LayoutControl1.Controls.Add(Me.txtDiscount)
        Me.LayoutControl1.Controls.Add(Me.txtTax)
        Me.LayoutControl1.Controls.Add(Me.lblTotal)
        Me.LayoutControl1.Controls.Add(Me.btnPayment)
        Me.LayoutControl1.Controls.Add(Me.btnClearF1)
        Me.LayoutControl1.Controls.Add(Me.btnUpDown)
        Me.LayoutControl1.Controls.Add(Me.btnHoldBill)
        Me.LayoutControl1.Controls.Add(Me.btnSalesPerson)
        Me.LayoutControl1.Controls.Add(Me.btnFOC)
        Me.LayoutControl1.Controls.Add(Me.btnDeposit)
        Me.LayoutControl1.Controls.Add(Me.btnLastBill)
        Me.LayoutControl1.Controls.Add(Me.btnDelOrder)
        Me.LayoutControl1.Controls.Add(Me.btnDrawer)
        Me.LayoutControl1.Controls.Add(Me.btnExit)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.Root
        Me.LayoutControl1.Size = New System.Drawing.Size(1200, 800)
        Me.LayoutControl1.TabIndex = 0

        '
        ' Root
        '
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.layoutItemWelcome, Me.layoutGroupTop, Me.layoutGroupMain, Me.layoutGroupBottom, Me.layoutGroupButtons})
        Me.Root.Location = New System.Drawing.Point(0, 0)
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1200, 800)
        Me.Root.TextVisible = False

        '
        ' Welcome Banner
        '
        Me.lblWelcome.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(19, Byte), Integer))
        Me.lblWelcome.Appearance.Font = New System.Drawing.Font("Arial", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblWelcome.Appearance.ForeColor = System.Drawing.Color.Orange
        Me.lblWelcome.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblWelcome.Location = New System.Drawing.Point(12, 12)
        Me.lblWelcome.Name = "lblWelcome"
        Me.lblWelcome.Size = New System.Drawing.Size(1176, 30)
        Me.lblWelcome.StyleController = Me.LayoutControl1
        Me.lblWelcome.TabIndex = 4
        Me.lblWelcome.Text = "WELCOME TO IRS"

        '
        ' layoutItemWelcome
        '
        Me.layoutItemWelcome.Control = Me.lblWelcome
        Me.layoutItemWelcome.Location = New System.Drawing.Point(0, 0)
        Me.layoutItemWelcome.Name = "layoutItemWelcome"
        Me.layoutItemWelcome.Size = New System.Drawing.Size(1180, 34)
        Me.layoutItemWelcome.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutItemWelcome.TextVisible = False

        '
        ' Top Group Layout
        '
        Me.layoutGroupTop.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.layoutItemCustomer, Me.layoutItemBranch, Me.layoutItemOptions, Me.layoutItemSalesperson, Me.layoutItemPaymentTerm, Me.layoutItemCash, Me.layoutItemSales})
        Me.layoutGroupTop.Location = New System.Drawing.Point(0, 34)
        Me.layoutGroupTop.Name = "layoutGroupTop"
        Me.layoutGroupTop.Size = New System.Drawing.Size(1180, 80)
        Me.layoutGroupTop.Text = "Customer Information"

        '
        ' Customer Controls
        '
        Me.txtCustomer.Location = New System.Drawing.Point(70, 62)
        Me.txtCustomer.Name = "txtCustomer"
        'Me.txtCustomer.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.F11)})
        Me.txtCustomer.Size = New System.Drawing.Size(120, 20)
        Me.txtCustomer.StyleController = Me.LayoutControl1
        Me.txtCustomer.TabIndex = 5

        Me.btnCustomerLookup.Location = New System.Drawing.Point(194, 62)
        Me.btnCustomerLookup.Name = "btnCustomerLookup"
        Me.btnCustomerLookup.Size = New System.Drawing.Size(30, 20)
        Me.btnCustomerLookup.StyleController = Me.LayoutControl1
        Me.btnCustomerLookup.TabIndex = 6
        Me.btnCustomerLookup.Text = "..."

        '
        ' Branch Control
        '
        Me.cmbBranch.Location = New System.Drawing.Point(270, 62)
        Me.cmbBranch.Name = "cmbBranch"
        Me.cmbBranch.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBranch.Size = New System.Drawing.Size(100, 20)
        Me.cmbBranch.StyleController = Me.LayoutControl1
        Me.cmbBranch.TabIndex = 7

        '
        ' Options Control
        '
        Me.cmbOptions.Location = New System.Drawing.Point(418, 62)
        Me.cmbOptions.Name = "cmbOptions"
        Me.cmbOptions.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOptions.Size = New System.Drawing.Size(100, 20)
        Me.cmbOptions.StyleController = Me.LayoutControl1
        Me.cmbOptions.TabIndex = 8

        '
        ' Salesperson Controls
        '
        Me.txtSalesperson.Location = New System.Drawing.Point(580, 62)
        Me.txtSalesperson.Name = "txtSalesperson"
        'Me.txtSalesperson.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.F6)})
        Me.txtSalesperson.Size = New System.Drawing.Size(100, 20)
        Me.txtSalesperson.StyleController = Me.LayoutControl1
        Me.txtSalesperson.TabIndex = 9

        Me.btnSalespersonLookup.Location = New System.Drawing.Point(684, 62)
        Me.btnSalespersonLookup.Name = "btnSalespersonLookup"
        Me.btnSalespersonLookup.Size = New System.Drawing.Size(30, 20)
        Me.btnSalespersonLookup.StyleController = Me.LayoutControl1
        Me.btnSalespersonLookup.TabIndex = 10
        Me.btnSalespersonLookup.Text = "..."

        '
        ' Payment Term
        '
        Me.txtPaymentTerm.Location = New System.Drawing.Point(770, 62)
        Me.txtPaymentTerm.Name = "txtPaymentTerm"
        Me.txtPaymentTerm.Size = New System.Drawing.Size(100, 20)
        Me.txtPaymentTerm.StyleController = Me.LayoutControl1
        Me.txtPaymentTerm.TabIndex = 11

        '
        ' Cash and Sales Buttons
        '
        Me.btnCash.Appearance.BackColor = System.Drawing.Color.Green
        Me.btnCash.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnCash.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnCash.Location = New System.Drawing.Point(884, 62)
        Me.btnCash.Name = "btnCash"
        Me.btnCash.Size = New System.Drawing.Size(50, 20)
        Me.btnCash.StyleController = Me.LayoutControl1
        Me.btnCash.TabIndex = 12
        Me.btnCash.Text = "Cash"

        Me.btnSales.Appearance.BackColor = System.Drawing.Color.Blue
        Me.btnSales.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnSales.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnSales.Location = New System.Drawing.Point(948, 62)
        Me.btnSales.Name = "btnSales"
        Me.btnSales.Size = New System.Drawing.Size(50, 20)
        Me.btnSales.StyleController = Me.LayoutControl1
        Me.btnSales.TabIndex = 13
        Me.btnSales.Text = "SALES"

        '
        ' Barcode Section
        '
        Me.txtBarcode.Location = New System.Drawing.Point(70, 126)
        Me.txtBarcode.Name = "txtBarcode"
        'Me.txtBarcode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.F7)})
        Me.txtBarcode.Size = New System.Drawing.Size(150, 20)
        Me.txtBarcode.StyleController = Me.LayoutControl1
        Me.txtBarcode.TabIndex = 14

        Me.btnBarcodeSearch.Location = New System.Drawing.Point(224, 126)
        Me.btnBarcodeSearch.Name = "btnBarcodeSearch"
        Me.btnBarcodeSearch.Size = New System.Drawing.Size(30, 20)
        Me.btnBarcodeSearch.StyleController = Me.LayoutControl1
        Me.btnBarcodeSearch.TabIndex = 15
        Me.btnBarcodeSearch.Text = "..."

        '
        ' Main Grid Control
        '
        Me.GridControl1.Location = New System.Drawing.Point(24, 170)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(850, 350)
        Me.GridControl1.TabIndex = 16
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})

        '
        ' GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colNo, Me.colItemNo, Me.colDescription, Me.colUOM, Me.colQty, Me.colPrice, Me.colDiscount, Me.colUPrice, Me.colAmount, Me.colPicture})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsView.ShowGroupPanel = False

        '
        ' Grid Columns
        '
        Me.colNo.Caption = "No"
        Me.colNo.FieldName = "No"
        Me.colNo.Name = "colNo"
        Me.colNo.Visible = True
        Me.colNo.VisibleIndex = 0
        Me.colNo.Width = 40

        Me.colItemNo.Caption = "Item No"
        Me.colItemNo.FieldName = "ItemNo"
        Me.colItemNo.Name = "colItemNo"
        Me.colItemNo.Visible = True
        Me.colItemNo.VisibleIndex = 1
        Me.colItemNo.Width = 120

        Me.colDescription.Caption = "Description"
        Me.colDescription.FieldName = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.Visible = True
        Me.colDescription.VisibleIndex = 2
        Me.colDescription.Width = 200

        Me.colUOM.Caption = "UOM"
        Me.colUOM.FieldName = "UOM"
        Me.colUOM.Name = "colUOM"
        Me.colUOM.Visible = True
        Me.colUOM.VisibleIndex = 3
        Me.colUOM.Width = 50

        Me.colQty.Caption = "Qty"
        Me.colQty.FieldName = "Qty"
        Me.colQty.Name = "colQty"
        Me.colQty.Visible = True
        Me.colQty.VisibleIndex = 4
        Me.colQty.Width = 60

        Me.colPrice.Caption = "Price"
        Me.colPrice.FieldName = "Price"
        Me.colPrice.Name = "colPrice"
        Me.colPrice.Visible = True
        Me.colPrice.VisibleIndex = 5
        Me.colPrice.Width = 80

        Me.colDiscount.Caption = "%"
        Me.colDiscount.FieldName = "Discount"
        Me.colDiscount.Name = "colDiscount"
        Me.colDiscount.Visible = True
        Me.colDiscount.VisibleIndex = 6
        Me.colDiscount.Width = 50

        Me.colUPrice.Caption = "U/Price"
        Me.colUPrice.FieldName = "UPrice"
        Me.colUPrice.Name = "colUPrice"
        Me.colUPrice.Visible = True
        Me.colUPrice.VisibleIndex = 7
        Me.colUPrice.Width = 80

        Me.colAmount.Caption = "Amount"
        Me.colAmount.FieldName = "Amount"
        Me.colAmount.Name = "colAmount"
        Me.colAmount.Visible = True
        Me.colAmount.VisibleIndex = 8
        Me.colAmount.Width = 100

        Me.colPicture.Caption = "Picture"
        Me.colPicture.FieldName = "Picture"
        Me.colPicture.Name = "colPicture"
        Me.colPicture.Visible = True
        Me.colPicture.VisibleIndex = 9
        Me.colPicture.Width = 80

        '
        ' Action Buttons
        '
        Me.btnEdit.Location = New System.Drawing.Point(100, 530)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(60, 30)
        Me.btnEdit.StyleController = Me.LayoutControl1
        Me.btnEdit.TabIndex = 17
        Me.btnEdit.Text = "Edit"

        Me.btnDelete.Location = New System.Drawing.Point(170, 530)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 30)
        Me.btnDelete.StyleController = Me.LayoutControl1
        Me.btnDelete.TabIndex = 18
        Me.btnDelete.Text = "Delete"

        Me.btnDiscount50.Appearance.BackColor = System.Drawing.Color.Red
        Me.btnDiscount50.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnDiscount50.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnDiscount50.Location = New System.Drawing.Point(450, 530)
        Me.btnDiscount50.Name = "btnDiscount50"
        Me.btnDiscount50.Size = New System.Drawing.Size(60, 30)
        Me.btnDiscount50.StyleController = Me.LayoutControl1
        Me.btnDiscount50.TabIndex = 19
        Me.btnDiscount50.Text = "50%"

        Me.btnUPrice.Location = New System.Drawing.Point(520, 530)
        Me.btnUPrice.Name = "btnUPrice"
        Me.btnUPrice.Size = New System.Drawing.Size(60, 30)
        Me.btnUPrice.StyleController = Me.LayoutControl1
        Me.btnUPrice.TabIndex = 20
        Me.btnUPrice.Text = "U/Price"

        '
        ' Bottom Section Controls
        '
        Me.txtSalesOrder.Location = New System.Drawing.Point(100, 580)
        Me.txtSalesOrder.Name = "txtSalesOrder"
        'Me.txtSalesOrder.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.F5)})
        Me.txtSalesOrder.Size = New System.Drawing.Size(120, 20)
        Me.txtSalesOrder.StyleController = Me.LayoutControl1
        Me.txtSalesOrder.TabIndex = 21

        Me.cmbPriceLevel.EditValue = "Normal"
        Me.cmbPriceLevel.Location = New System.Drawing.Point(340, 580)
        Me.cmbPriceLevel.Name = "cmbPriceLevel"
        Me.cmbPriceLevel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPriceLevel.Properties.Items.AddRange(New Object() {"Normal", "Wholesale", "Retail"})
        Me.cmbPriceLevel.Size = New System.Drawing.Size(100, 20)
        Me.cmbPriceLevel.StyleController = Me.LayoutControl1
        Me.cmbPriceLevel.TabIndex = 22

        Me.txtSubTotal.EditValue = "39.00"
        Me.txtSubTotal.Location = New System.Drawing.Point(600, 580)
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.Properties.ReadOnly = True
        Me.txtSubTotal.Size = New System.Drawing.Size(80, 20)
        Me.txtSubTotal.StyleController = Me.LayoutControl1
        Me.txtSubTotal.TabIndex = 23

        Me.txtRounding.EditValue = "0.00"
        Me.txtRounding.Location = New System.Drawing.Point(750, 580)
        Me.txtRounding.Name = "txtRounding"
        Me.txtRounding.Properties.ReadOnly = True
        Me.txtRounding.Size = New System.Drawing.Size(80, 20)
        Me.txtRounding.StyleController = Me.LayoutControl1
        Me.txtRounding.TabIndex = 24

        Me.txtDiscount.EditValue = "0.00"
        Me.txtDiscount.Location = New System.Drawing.Point(100, 610)
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtDiscount.Size = New System.Drawing.Size(80, 20)
        Me.txtDiscount.StyleController = Me.LayoutControl1
        Me.txtDiscount.TabIndex = 25

        Me.txtTax.EditValue = "0.00"
        Me.txtTax.Location = New System.Drawing.Point(280, 610)
        Me.txtTax.Name = "txtTax"
        Me.txtTax.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtTax.Size = New System.Drawing.Size(80, 20)
        Me.txtTax.StyleController = Me.LayoutControl1
        Me.txtTax.TabIndex = 26

        Me.lblTotal.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(19, Byte), Integer))
        Me.lblTotal.Appearance.Font = New System.Drawing.Font("Digital-7", 36.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotal.Appearance.ForeColor = System.Drawing.Color.Orange
        Me.lblTotal.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblTotal.Location = New System.Drawing.Point(700, 610)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(150, 60)
        Me.lblTotal.StyleController = Me.LayoutControl1
        Me.lblTotal.TabIndex = 27
        Me.lblTotal.Text = "3900"

        '
        ' Right Side Function Buttons
        '
        Me.btnPayment.Appearance.BackColor = System.Drawing.Color.Orange
        Me.btnPayment.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnPayment.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnPayment.Location = New System.Drawing.Point(890, 170)
        Me.btnPayment.Name = "btnPayment"
        Me.btnPayment.Size = New System.Drawing.Size(80, 40)
        Me.btnPayment.StyleController = Me.LayoutControl1
        Me.btnPayment.TabIndex = 28
        Me.btnPayment.Text = "Payment" & vbCrLf & "(F10)"

        Me.btnClearF1.Appearance.BackColor = System.Drawing.Color.Green
        Me.btnClearF1.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnClearF1.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnClearF1.Location = New System.Drawing.Point(980, 170)
        Me.btnClearF1.Name = "btnClearF1"
        Me.btnClearF1.Size = New System.Drawing.Size(80, 40)
        Me.btnClearF1.StyleController = Me.LayoutControl1
        Me.btnClearF1.TabIndex = 29
        Me.btnClearF1.Text = "Clear (F1)"

        Me.btnUpDown.Appearance.BackColor = System.Drawing.Color.Green
        Me.btnUpDown.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnUpDown.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnUpDown.Location = New System.Drawing.Point(1070, 170)
        Me.btnUpDown.Name = "btnUpDown"
        Me.btnUpDown.Size = New System.Drawing.Size(80, 40)
        Me.btnUpDown.StyleController = Me.LayoutControl1
        Me.btnUpDown.TabIndex = 30
        Me.btnUpDown.Text = "Up/Down"

        Me.btnHoldBill.Appearance.BackColor = System.Drawing.Color.Blue
        Me.btnHoldBill.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnHoldBill.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnHoldBill.Location = New System.Drawing.Point(890, 220)
        Me.btnHoldBill.Name = "btnHoldBill"
        Me.btnHoldBill.Size = New System.Drawing.Size(80, 40)
        Me.btnHoldBill.StyleController = Me.LayoutControl1
        Me.btnHoldBill.TabIndex = 31
        Me.btnHoldBill.Text = "Hold Bill" & vbCrLf & "(F8)"

        Me.btnSalesPerson.Appearance.BackColor = System.Drawing.Color.Blue
        Me.btnSalesPerson.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSalesPerson.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnSalesPerson.Location = New System.Drawing.Point(980, 220)
        Me.btnSalesPerson.Name = "btnSalesPerson"
        Me.btnSalesPerson.Size = New System.Drawing.Size(80, 40)
        Me.btnSalesPerson.StyleController = Me.LayoutControl1
        Me.btnSalesPerson.TabIndex = 32
        Me.btnSalesPerson.Text = "S.Person"

        Me.btnFOC.Appearance.BackColor = System.Drawing.Color.Blue
        Me.btnFOC.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnFOC.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnFOC.Location = New System.Drawing.Point(1070, 220)
        Me.btnFOC.Name = "btnFOC"
        Me.btnFOC.Size = New System.Drawing.Size(80, 40)
        Me.btnFOC.StyleController = Me.LayoutControl1
        Me.btnFOC.TabIndex = 33
        Me.btnFOC.Text = "F.O.C"

        Me.btnDeposit.Appearance.BackColor = System.Drawing.Color.Orange
        Me.btnDeposit.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDeposit.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnDeposit.Location = New System.Drawing.Point(890, 270)
        Me.btnDeposit.Name = "btnDeposit"
        Me.btnDeposit.Size = New System.Drawing.Size(80, 40)
        Me.btnDeposit.StyleController = Me.LayoutControl1
        Me.btnDeposit.TabIndex = 34
        Me.btnDeposit.Text = "Deposit"

        Me.btnLastBill.Appearance.BackColor = System.Drawing.Color.Blue
        Me.btnLastBill.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnLastBill.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnLastBill.Location = New System.Drawing.Point(980, 270)
        Me.btnLastBill.Name = "btnLastBill"
        Me.btnLastBill.Size = New System.Drawing.Size(80, 40)
        Me.btnLastBill.StyleController = Me.LayoutControl1
        Me.btnLastBill.TabIndex = 35
        Me.btnLastBill.Text = "Last Bill" & vbCrLf & "(Ctrl+L)"

        Me.btnDelOrder.Appearance.BackColor = System.Drawing.Color.Red
        Me.btnDelOrder.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDelOrder.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnDelOrder.Location = New System.Drawing.Point(1070, 270)
        Me.btnDelOrder.Name = "btnDelOrder"
        Me.btnDelOrder.Size = New System.Drawing.Size(80, 40)
        Me.btnDelOrder.StyleController = Me.LayoutControl1
        Me.btnDelOrder.TabIndex = 36
        Me.btnDelOrder.Text = "Del Order"

        Me.btnDrawer.Appearance.BackColor = System.Drawing.Color.Orange
        Me.btnDrawer.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDrawer.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnDrawer.Location = New System.Drawing.Point(890, 320)
        Me.btnDrawer.Name = "btnDrawer"
        Me.btnDrawer.Size = New System.Drawing.Size(80, 40)
        Me.btnDrawer.StyleController = Me.LayoutControl1
        Me.btnDrawer.TabIndex = 37
        Me.btnDrawer.Text = "Drawer" & vbCrLf & "(F9)"

        Me.btnExit.Appearance.BackColor = System.Drawing.Color.Blue
        Me.btnExit.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnExit.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(1070, 320)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(80, 40)
        Me.btnExit.StyleController = Me.LayoutControl1
        Me.btnExit.TabIndex = 38
        Me.btnExit.Text = "Exit (F4)"

        ' Layout Items Configuration
        Me.layoutItemCustomer.Control = Me.txtCustomer
        Me.layoutItemCustomer.Location = New System.Drawing.Point(0, 28)
        Me.layoutItemCustomer.Name = "layoutItemCustomer"
        Me.layoutItemCustomer.Size = New System.Drawing.Size(182, 24)
        Me.layoutItemCustomer.Text = "Customer"
        Me.layoutItemCustomer.TextSize = New System.Drawing.Size(58, 13)

        Me.layoutItemBranch.Control = Me.cmbBranch
        Me.layoutItemBranch.Location = New System.Drawing.Point(228, 28)
        Me.layoutItemBranch.Name = "layoutItemBranch"
        Me.layoutItemBranch.Size = New System.Drawing.Size(146, 24)
        Me.layoutItemBranch.Text = "Branch"
        Me.layoutItemBranch.TextSize = New System.Drawing.Size(42, 13)

        Me.layoutItemOptions.Control = Me.cmbOptions
        Me.layoutItemOptions.Location = New System.Drawing.Point(374, 28)
        Me.layoutItemOptions.Name = "layoutItemOptions"
        Me.layoutItemOptions.Size = New System.Drawing.Size(146, 24)
        Me.layoutItemOptions.Text = "Options"
        Me.layoutItemOptions.TextSize = New System.Drawing.Size(42, 13)

        Me.layoutItemSalesperson.Control = Me.txtSalesperson
        Me.layoutItemSalesperson.Location = New System.Drawing.Point(520, 28)
        Me.layoutItemSalesperson.Name = "layoutItemSalesperson"
        Me.layoutItemSalesperson.Size = New System.Drawing.Size(146, 24)
        Me.layoutItemSalesperson.Text = "Salesperson"
        Me.layoutItemSalesperson.TextSize = New System.Drawing.Size(66, 13)

        Me.layoutItemPaymentTerm.Control = Me.txtPaymentTerm
        Me.layoutItemPaymentTerm.Location = New System.Drawing.Point(716, 28)
        Me.layoutItemPaymentTerm.Name = "layoutItemPaymentTerm"
        Me.layoutItemPaymentTerm.Size = New System.Drawing.Size(146, 24)
        Me.layoutItemPaymentTerm.Text = "Payment Term"
        Me.layoutItemPaymentTerm.TextSize = New System.Drawing.Size(72, 13)

        Me.layoutItemCash.Control = Me.btnCash
        Me.layoutItemCash.Location = New System.Drawing.Point(872, 28)
        Me.layoutItemCash.Name = "layoutItemCash"
        Me.layoutItemCash.Size = New System.Drawing.Size(54, 24)
        Me.layoutItemCash.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutItemCash.TextVisible = False

        Me.layoutItemSales.Control = Me.btnSales
        Me.layoutItemSales.Location = New System.Drawing.Point(926, 28)
        Me.layoutItemSales.Name = "layoutItemSales"
        Me.layoutItemSales.Size = New System.Drawing.Size(54, 24)
        Me.layoutItemSales.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutItemSales.TextVisible = False

        Me.layoutItemBarcode.Control = Me.txtBarcode
        Me.layoutItemBarcode.Location = New System.Drawing.Point(0, 92)
        Me.layoutItemBarcode.Name = "layoutItemBarcode"
        Me.layoutItemBarcode.Size = New System.Drawing.Size(212, 24)
        Me.layoutItemBarcode.Text = "Barcode"
        Me.layoutItemBarcode.TextSize = New System.Drawing.Size(58, 13)

        Me.layoutItemGrid.Control = Me.GridControl1
        Me.layoutItemGrid.Location = New System.Drawing.Point(0, 136)
        Me.layoutItemGrid.Name = "layoutItemGrid"
        Me.layoutItemGrid.Size = New System.Drawing.Size(854, 354)
        Me.layoutItemGrid.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutItemGrid.TextVisible = False

        Me.layoutItemSalesOrder.Control = Me.txtSalesOrder
        Me.layoutItemSalesOrder.Location = New System.Drawing.Point(30, 546)
        Me.layoutItemSalesOrder.Name = "layoutItemSalesOrder"
        Me.layoutItemSalesOrder.Size = New System.Drawing.Size(182, 24)
        Me.layoutItemSalesOrder.Text = "Sales Order"
        Me.layoutItemSalesOrder.TextSize = New System.Drawing.Size(58, 13)

        Me.layoutItemPriceLevel.Control = Me.cmbPriceLevel
        Me.layoutItemPriceLevel.Location = New System.Drawing.Point(254, 546)
        Me.layoutItemPriceLevel.Name = "layoutItemPriceLevel"
        Me.layoutItemPriceLevel.Size = New System.Drawing.Size(162, 24)
        Me.layoutItemPriceLevel.Text = "Price Level"
        Me.layoutItemPriceLevel.TextSize = New System.Drawing.Size(58, 13)

        Me.layoutItemSubTotal.Control = Me.txtSubTotal
        Me.layoutItemSubTotal.Location = New System.Drawing.Point(530, 546)
        Me.layoutItemSubTotal.Name = "layoutItemSubTotal"
        Me.layoutItemSubTotal.Size = New System.Drawing.Size(142, 24)
        Me.layoutItemSubTotal.Text = "Sub-Total"
        Me.layoutItemSubTotal.TextSize = New System.Drawing.Size(58, 13)

        Me.layoutItemRounding.Control = Me.txtRounding
        Me.layoutItemRounding.Location = New System.Drawing.Point(672, 546)
        Me.layoutItemRounding.Name = "layoutItemRounding"
        Me.layoutItemRounding.Size = New System.Drawing.Size(142, 24)
        Me.layoutItemRounding.Text = "Rounding"
        Me.layoutItemRounding.TextSize = New System.Drawing.Size(58, 13)

        Me.layoutItemDiscount.Control = Me.txtDiscount
        Me.layoutItemDiscount.Location = New System.Drawing.Point(30, 576)
        Me.layoutItemDiscount.Name = "layoutItemDiscount"
        Me.layoutItemDiscount.Size = New System.Drawing.Size(142, 24)
        Me.layoutItemDiscount.Text = "Disc%"
        Me.layoutItemDiscount.TextSize = New System.Drawing.Size(58, 13)

        Me.layoutItemTax.Control = Me.txtTax
        Me.layoutItemTax.Location = New System.Drawing.Point(214, 576)
        Me.layoutItemTax.Name = "layoutItemTax"
        Me.layoutItemTax.Size = New System.Drawing.Size(142, 24)
        Me.layoutItemTax.Text = "Tax%"
        Me.layoutItemTax.TextSize = New System.Drawing.Size(58, 13)

        Me.layoutItemTotal.Control = Me.lblTotal
        Me.layoutItemTotal.Location = New System.Drawing.Point(630, 576)
        Me.layoutItemTotal.Name = "layoutItemTotal"
        Me.layoutItemTotal.Size = New System.Drawing.Size(212, 64)
        Me.layoutItemTotal.Text = "Total"
        Me.layoutItemTotal.TextLocation = DevExpress.Utils.Locations.Top
        Me.layoutItemTotal.TextSize = New System.Drawing.Size(58, 13)

        '
        ' frmPosRetailBilling
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 800)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "frmPosRetailBilling"
        Me.Text = "POS Retail Billing - IRS System"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBranch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOptions.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalesperson.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPaymentTerm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBarcode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalesOrder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPriceLevel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRounding.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiscount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    ' Control declarations
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lblWelcome As DevExpress.XtraEditors.LabelControl
    Friend WithEvents layoutItemWelcome As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCustomer As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents btnCustomerLookup As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbBranch As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbOptions As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtSalesperson As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents btnSalespersonLookup As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtPaymentTerm As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnCash As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSales As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtBarcode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents btnBarcodeSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colItemNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUOM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDiscount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPicture As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDiscount50 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnUPrice As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtSalesOrder As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents cmbPriceLevel As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtSubTotal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtRounding As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDiscount As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents txtTax As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents lblTotal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnPayment As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnClearF1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnUpDown As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnHoldBill As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSalesPerson As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFOC As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeposit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnLastBill As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDelOrder As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDrawer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents layoutGroupTop As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents layoutGroupMain As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents layoutGroupBottom As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents layoutGroupButtons As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents layoutItemCustomer As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutItemBranch As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutItemOptions As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutItemSalesperson As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutItemPaymentTerm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutItemCash As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutItemSales As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutItemBarcode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutItemGrid As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutItemSalesOrder As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutItemPriceLevel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutItemSubTotal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutItemRounding As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutItemDiscount As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutItemTax As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutItemTotal As DevExpress.XtraLayout.LayoutControlItem

End Class
