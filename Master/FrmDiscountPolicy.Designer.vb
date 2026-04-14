<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDiscountPolicy
    Inherits DevExpress.XtraEditors.XtraForm

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.GridControlDP = New DevExpress.XtraGrid.GridControl()
        Me.GridViewDP = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colDpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDiscountName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMinAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMaxAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRequireVoucher = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colValidDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colActive = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtId = New DevExpress.XtraEditors.TextEdit()
        Me.txtName = New DevExpress.XtraEditors.TextEdit()
        Me.lkpDiscount = New DevExpress.XtraEditors.LookUpEdit()
        Me.spinMinAmount = New DevExpress.XtraEditors.SpinEdit()
        Me.spinMaxAmount = New DevExpress.XtraEditors.SpinEdit()
        Me.chkRequireVoucher = New DevExpress.XtraEditors.CheckEdit()
        Me.dtValidDate = New DevExpress.XtraEditors.DateEdit()
        Me.chkActive = New DevExpress.XtraEditors.CheckEdit()
        Me.btnNew = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lciGrid = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciDiscount = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciMinAmount = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciMaxAmount = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciRequireVoucher = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciValidDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciActive = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciBtnNew = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciBtnSave = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciBtnDelete = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciBtnCancel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.GridControlDP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewDP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lkpDiscount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spinMinAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spinMaxAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRequireVoucher.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtValidDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtValidDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciMinAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciMaxAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciRequireVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciValidDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciBtnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciBtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciBtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciBtnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.GridControlDP)
        Me.LayoutControl1.Controls.Add(Me.txtId)
        Me.LayoutControl1.Controls.Add(Me.txtName)
        Me.LayoutControl1.Controls.Add(Me.lkpDiscount)
        Me.LayoutControl1.Controls.Add(Me.spinMinAmount)
        Me.LayoutControl1.Controls.Add(Me.spinMaxAmount)
        Me.LayoutControl1.Controls.Add(Me.chkRequireVoucher)
        Me.LayoutControl1.Controls.Add(Me.dtValidDate)
        Me.LayoutControl1.Controls.Add(Me.chkActive)
        Me.LayoutControl1.Controls.Add(Me.btnNew)
        Me.LayoutControl1.Controls.Add(Me.btnSave)
        Me.LayoutControl1.Controls.Add(Me.btnDelete)
        Me.LayoutControl1.Controls.Add(Me.btnCancel)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2675, 432, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(771, 659)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'GridControlDP
        '
        Me.GridControlDP.Location = New System.Drawing.Point(12, 28)
        Me.GridControlDP.MainView = Me.GridViewDP
        Me.GridControlDP.Name = "GridControlDP"
        Me.GridControlDP.Size = New System.Drawing.Size(747, 489)
        Me.GridControlDP.TabIndex = 0
        Me.GridControlDP.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewDP})
        '
        'GridViewDP
        '
        Me.GridViewDP.Appearance.FocusedRow.Options.UseTextOptions = True
        Me.GridViewDP.Appearance.FocusedRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewDP.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridViewDP.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewDP.Appearance.Row.Options.UseTextOptions = True
        Me.GridViewDP.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewDP.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colDpId, Me.colDpName, Me.colDiscountName, Me.colMinAmount, Me.colMaxAmount, Me.colRequireVoucher, Me.colValidDate, Me.colActive})
        Me.GridViewDP.GridControl = Me.GridControlDP
        Me.GridViewDP.Name = "GridViewDP"
        Me.GridViewDP.OptionsBehavior.Editable = False
        Me.GridViewDP.OptionsBehavior.ReadOnly = True
        Me.GridViewDP.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridViewDP.OptionsView.ShowGroupPanel = False
        '
        'colDpId
        '
        Me.colDpId.FieldName = "dp_id"
        Me.colDpId.Name = "colDpId"
        '
        'colDpName
        '
        Me.colDpName.Caption = "Policy Name"
        Me.colDpName.FieldName = "dp_name"
        Me.colDpName.Name = "colDpName"
        Me.colDpName.Visible = True
        Me.colDpName.VisibleIndex = 0
        Me.colDpName.Width = 160
        '
        'colDiscountName
        '
        Me.colDiscountName.Caption = "Discount"
        Me.colDiscountName.FieldName = "discount_name"
        Me.colDiscountName.Name = "colDiscountName"
        Me.colDiscountName.Visible = True
        Me.colDiscountName.VisibleIndex = 1
        Me.colDiscountName.Width = 140
        '
        'colMinAmount
        '
        Me.colMinAmount.Caption = "Min Amount"
        Me.colMinAmount.FieldName = "dp_min_amount"
        Me.colMinAmount.Name = "colMinAmount"
        Me.colMinAmount.Visible = True
        Me.colMinAmount.VisibleIndex = 2
        Me.colMinAmount.Width = 100
        '
        'colMaxAmount
        '
        Me.colMaxAmount.Caption = "Max Amount"
        Me.colMaxAmount.FieldName = "dp_max_amount"
        Me.colMaxAmount.Name = "colMaxAmount"
        Me.colMaxAmount.Visible = True
        Me.colMaxAmount.VisibleIndex = 3
        Me.colMaxAmount.Width = 100
        '
        'colRequireVoucher
        '
        Me.colRequireVoucher.Caption = "Req. Voucher"
        Me.colRequireVoucher.FieldName = "dp_require_voucher"
        Me.colRequireVoucher.Name = "colRequireVoucher"
        Me.colRequireVoucher.Visible = True
        Me.colRequireVoucher.VisibleIndex = 4
        Me.colRequireVoucher.Width = 90
        '
        'colValidDate
        '
        Me.colValidDate.Caption = "Valid Date"
        Me.colValidDate.FieldName = "dp_validdate"
        Me.colValidDate.Name = "colValidDate"
        Me.colValidDate.Visible = True
        Me.colValidDate.VisibleIndex = 5
        Me.colValidDate.Width = 130
        '
        'colActive
        '
        Me.colActive.Caption = "Status"
        Me.colActive.FieldName = "dp_active"
        Me.colActive.Name = "colActive"
        Me.colActive.Visible = True
        Me.colActive.VisibleIndex = 6
        Me.colActive.Width = 80
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(105, 521)
        Me.txtId.Name = "txtId"
        Me.txtId.Properties.Appearance.Options.UseTextOptions = True
        Me.txtId.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtId.Properties.ReadOnly = True
        Me.txtId.Size = New System.Drawing.Size(55, 20)
        Me.txtId.StyleController = Me.LayoutControl1
        Me.txtId.TabIndex = 1
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(257, 521)
        Me.txtName.Name = "txtName"
        Me.txtName.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 11.0!)
        Me.txtName.Properties.Appearance.Options.UseFont = True
        Me.txtName.Properties.Appearance.Options.UseTextOptions = True
        Me.txtName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtName.Size = New System.Drawing.Size(252, 24)
        Me.txtName.StyleController = Me.LayoutControl1
        Me.txtName.TabIndex = 2
        '
        'lkpDiscount
        '
        Me.lkpDiscount.Location = New System.Drawing.Point(105, 549)
        Me.lkpDiscount.Name = "lkpDiscount"
        Me.lkpDiscount.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 11.0!)
        Me.lkpDiscount.Properties.Appearance.Options.UseFont = True
        Me.lkpDiscount.Properties.Appearance.Options.UseTextOptions = True
        Me.lkpDiscount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lkpDiscount.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lkpDiscount.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("discount_id", 40, "ID"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("discount_name", 160, "Discount Name")})
        Me.lkpDiscount.Properties.DisplayMember = "discount_name"
        Me.lkpDiscount.Properties.NullText = "-- Select Discount --"
        Me.lkpDiscount.Properties.ValueMember = "discount_id"
        Me.lkpDiscount.Size = New System.Drawing.Size(139, 24)
        Me.lkpDiscount.StyleController = Me.LayoutControl1
        Me.lkpDiscount.TabIndex = 3
        '
        
        'spinMinAmount
        '
        Me.spinMinAmount.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.spinMinAmount.Location = New System.Drawing.Point(105, 577)
        Me.spinMinAmount.Name = "spinMinAmount"
        Me.spinMinAmount.Properties.Appearance.Options.UseTextOptions = True
        Me.spinMinAmount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.spinMinAmount.Properties.DisplayFormat.FormatString = "N2"
        Me.spinMinAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.spinMinAmount.Properties.EditFormat.FormatString = "N2"
        Me.spinMinAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.spinMinAmount.Properties.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.spinMinAmount.Properties.MaxValue = New Decimal(New Integer() {99999999, 0, 0, 131072})
        Me.spinMinAmount.Size = New System.Drawing.Size(84, 20)
        Me.spinMinAmount.StyleController = Me.LayoutControl1
        Me.spinMinAmount.TabIndex = 5
        '
        'spinMaxAmount
        '
        Me.spinMaxAmount.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.spinMaxAmount.Location = New System.Drawing.Point(286, 577)
        Me.spinMaxAmount.Name = "spinMaxAmount"
        Me.spinMaxAmount.Properties.Appearance.Options.UseTextOptions = True
        Me.spinMaxAmount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.spinMaxAmount.Properties.DisplayFormat.FormatString = "N2"
        Me.spinMaxAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.spinMaxAmount.Properties.EditFormat.FormatString = "N2"
        Me.spinMaxAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.spinMaxAmount.Properties.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.spinMaxAmount.Properties.MaxValue = New Decimal(New Integer() {99999999, 0, 0, 131072})
        Me.spinMaxAmount.Size = New System.Drawing.Size(79, 20)
        Me.spinMaxAmount.StyleController = Me.LayoutControl1
        Me.spinMaxAmount.TabIndex = 6
        '
        'chkRequireVoucher
        '
        Me.chkRequireVoucher.Location = New System.Drawing.Point(369, 577)
        Me.chkRequireVoucher.Name = "chkRequireVoucher"
        Me.chkRequireVoucher.Properties.Appearance.Options.UseTextOptions = True
        Me.chkRequireVoucher.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.chkRequireVoucher.Properties.Caption = "Require Voucher"
        Me.chkRequireVoucher.Size = New System.Drawing.Size(140, 19)
        Me.chkRequireVoucher.StyleController = Me.LayoutControl1
        Me.chkRequireVoucher.TabIndex = 7
        '
        'dtValidDate
        '
        Me.dtValidDate.EditValue = New Date(2026, 4, 14, 0, 0, 0, 0)
        Me.dtValidDate.Location = New System.Drawing.Point(105, 601)
        Me.dtValidDate.Name = "dtValidDate"
        Me.dtValidDate.Properties.Appearance.Options.UseTextOptions = True
        Me.dtValidDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.dtValidDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtValidDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtValidDate.Properties.CalendarTimeProperties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
        Me.dtValidDate.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.[Default]
        Me.dtValidDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.dtValidDate.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.dtValidDate.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.dtValidDate.Size = New System.Drawing.Size(180, 20)
        Me.dtValidDate.StyleController = Me.LayoutControl1
        Me.dtValidDate.TabIndex = 8
        '
        'chkActive
        '
        Me.chkActive.Location = New System.Drawing.Point(289, 601)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Properties.Appearance.Options.UseTextOptions = True
        Me.chkActive.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.chkActive.Properties.Caption = "Active"
        Me.chkActive.Size = New System.Drawing.Size(220, 19)
        Me.chkActive.StyleController = Me.LayoutControl1
        Me.chkActive.TabIndex = 9
        '
        'btnNew
        '
        Me.btnNew.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnNew.Appearance.Options.UseBackColor = True
        Me.btnNew.Location = New System.Drawing.Point(12, 625)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(129, 22)
        Me.btnNew.StyleController = Me.LayoutControl1
        Me.btnNew.TabIndex = 10
        Me.btnNew.Text = "New"
        '
        'btnSave
        '
        Me.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnSave.Appearance.Options.UseBackColor = True
        Me.btnSave.Location = New System.Drawing.Point(145, 625)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(112, 22)
        Me.btnSave.StyleController = Me.LayoutControl1
        Me.btnSave.TabIndex = 11
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnDelete.Appearance.Options.UseBackColor = True
        Me.btnDelete.Enabled = False
        Me.btnDelete.Location = New System.Drawing.Point(261, 625)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(116, 22)
        Me.btnDelete.StyleController = Me.LayoutControl1
        Me.btnDelete.TabIndex = 12
        Me.btnDelete.Text = "Delete"
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnCancel.Appearance.Options.UseBackColor = True
        Me.btnCancel.Location = New System.Drawing.Point(381, 625)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(128, 22)
        Me.btnCancel.StyleController = Me.LayoutControl1
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "Cancel"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lciGrid, Me.lciId, Me.lciName, Me.lciDiscount, Me.lciMinAmount, Me.lciMaxAmount, Me.lciRequireVoucher, Me.lciValidDate, Me.lciActive, Me.lciBtnNew, Me.lciBtnSave, Me.lciBtnDelete, Me.lciBtnCancel, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(771, 659)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lciGrid
        '
        Me.lciGrid.Control = Me.GridControlDP
        Me.lciGrid.CustomizationFormText = "Discount Policy List"
        Me.lciGrid.Location = New System.Drawing.Point(0, 0)
        Me.lciGrid.Name = "lciGrid"
        Me.lciGrid.Size = New System.Drawing.Size(751, 509)
        Me.lciGrid.Text = "Discount Policy List"
        Me.lciGrid.TextLocation = DevExpress.Utils.Locations.Top
        Me.lciGrid.TextSize = New System.Drawing.Size(90, 13)
        '
        'lciId
        '
        Me.lciId.Control = Me.txtId
        Me.lciId.CustomizationFormText = "ID :"
        Me.lciId.Location = New System.Drawing.Point(0, 509)
        Me.lciId.Name = "lciId"
        Me.lciId.Size = New System.Drawing.Size(152, 28)
        Me.lciId.Text = "ID :"
        Me.lciId.TextSize = New System.Drawing.Size(90, 13)
        '
        'lciName
        '
        Me.lciName.Control = Me.txtName
        Me.lciName.CustomizationFormText = "Policy Name :"
        Me.lciName.Location = New System.Drawing.Point(152, 509)
        Me.lciName.Name = "lciName"
        Me.lciName.Size = New System.Drawing.Size(349, 28)
        Me.lciName.Text = "Policy Name :"
        Me.lciName.TextSize = New System.Drawing.Size(90, 13)
        '
        'lciDiscount
        '
        Me.lciDiscount.Control = Me.lkpDiscount
        Me.lciDiscount.CustomizationFormText = "Discount :"
        Me.lciDiscount.Location = New System.Drawing.Point(0, 537)
        Me.lciDiscount.Name = "lciDiscount"
        Me.lciDiscount.Size = New System.Drawing.Size(236, 28)
        Me.lciDiscount.Text = "Discount :"
        Me.lciDiscount.TextSize = New System.Drawing.Size(90, 13)
        '
        'lciMinAmount
        '
        Me.lciMinAmount.Control = Me.spinMinAmount
        Me.lciMinAmount.CustomizationFormText = "Min Amount :"
        Me.lciMinAmount.Location = New System.Drawing.Point(0, 565)
        Me.lciMinAmount.Name = "lciMinAmount"
        Me.lciMinAmount.Size = New System.Drawing.Size(181, 24)
        Me.lciMinAmount.Text = "Min Amount :"
        Me.lciMinAmount.TextSize = New System.Drawing.Size(90, 13)
        '
        'lciMaxAmount
        '
        Me.lciMaxAmount.Control = Me.spinMaxAmount
        Me.lciMaxAmount.CustomizationFormText = "Max Amount :"
        Me.lciMaxAmount.Location = New System.Drawing.Point(181, 565)
        Me.lciMaxAmount.Name = "lciMaxAmount"
        Me.lciMaxAmount.Size = New System.Drawing.Size(176, 24)
        Me.lciMaxAmount.Text = "Max Amount :"
        Me.lciMaxAmount.TextSize = New System.Drawing.Size(90, 13)
        '
        'lciRequireVoucher
        '
        Me.lciRequireVoucher.Control = Me.chkRequireVoucher
        Me.lciRequireVoucher.CustomizationFormText = "lciRequireVoucher"
        Me.lciRequireVoucher.Location = New System.Drawing.Point(357, 565)
        Me.lciRequireVoucher.Name = "lciRequireVoucher"
        Me.lciRequireVoucher.Size = New System.Drawing.Size(144, 24)
        Me.lciRequireVoucher.Text = "lciRequireVoucher"
        Me.lciRequireVoucher.TextSize = New System.Drawing.Size(0, 0)
        Me.lciRequireVoucher.TextToControlDistance = 0
        Me.lciRequireVoucher.TextVisible = False
        '
        'lciValidDate
        '
        Me.lciValidDate.Control = Me.dtValidDate
        Me.lciValidDate.CustomizationFormText = "Valid Date :"
        Me.lciValidDate.Location = New System.Drawing.Point(0, 589)
        Me.lciValidDate.Name = "lciValidDate"
        Me.lciValidDate.Size = New System.Drawing.Size(277, 24)
        Me.lciValidDate.Text = "Valid Date :"
        Me.lciValidDate.TextSize = New System.Drawing.Size(90, 13)
        '
        'lciActive
        '
        Me.lciActive.Control = Me.chkActive
        Me.lciActive.CustomizationFormText = "lciActive"
        Me.lciActive.Location = New System.Drawing.Point(277, 589)
        Me.lciActive.Name = "lciActive"
        Me.lciActive.Size = New System.Drawing.Size(224, 24)
        Me.lciActive.Text = "lciActive"
        Me.lciActive.TextSize = New System.Drawing.Size(0, 0)
        Me.lciActive.TextToControlDistance = 0
        Me.lciActive.TextVisible = False
        '
        'lciBtnNew
        '
        Me.lciBtnNew.Control = Me.btnNew
        Me.lciBtnNew.CustomizationFormText = "lciBtnNew"
        Me.lciBtnNew.Location = New System.Drawing.Point(0, 613)
        Me.lciBtnNew.Name = "lciBtnNew"
        Me.lciBtnNew.Size = New System.Drawing.Size(133, 26)
        Me.lciBtnNew.Text = "lciBtnNew"
        Me.lciBtnNew.TextSize = New System.Drawing.Size(0, 0)
        Me.lciBtnNew.TextToControlDistance = 0
        Me.lciBtnNew.TextVisible = False
        '
        'lciBtnSave
        '
        Me.lciBtnSave.Control = Me.btnSave
        Me.lciBtnSave.CustomizationFormText = "lciBtnSave"
        Me.lciBtnSave.Location = New System.Drawing.Point(133, 613)
        Me.lciBtnSave.Name = "lciBtnSave"
        Me.lciBtnSave.Size = New System.Drawing.Size(116, 26)
        Me.lciBtnSave.Text = "lciBtnSave"
        Me.lciBtnSave.TextSize = New System.Drawing.Size(0, 0)
        Me.lciBtnSave.TextToControlDistance = 0
        Me.lciBtnSave.TextVisible = False
        '
        'lciBtnDelete
        '
        Me.lciBtnDelete.Control = Me.btnDelete
        Me.lciBtnDelete.CustomizationFormText = "lciBtnDelete"
        Me.lciBtnDelete.Location = New System.Drawing.Point(249, 613)
        Me.lciBtnDelete.Name = "lciBtnDelete"
        Me.lciBtnDelete.Size = New System.Drawing.Size(120, 26)
        Me.lciBtnDelete.Text = "lciBtnDelete"
        Me.lciBtnDelete.TextSize = New System.Drawing.Size(0, 0)
        Me.lciBtnDelete.TextToControlDistance = 0
        Me.lciBtnDelete.TextVisible = False
        '
        'lciBtnCancel
        '
        Me.lciBtnCancel.Control = Me.btnCancel
        Me.lciBtnCancel.CustomizationFormText = "lciBtnCancel"
        Me.lciBtnCancel.Location = New System.Drawing.Point(369, 613)
        Me.lciBtnCancel.Name = "lciBtnCancel"
        Me.lciBtnCancel.Size = New System.Drawing.Size(132, 26)
        Me.lciBtnCancel.Text = "lciBtnCancel"
        Me.lciBtnCancel.TextSize = New System.Drawing.Size(0, 0)
        Me.lciBtnCancel.TextToControlDistance = 0
        Me.lciBtnCancel.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(501, 509)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(250, 130)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'FrmDiscountPolicy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(771, 659)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "FrmDiscountPolicy"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Discount Policy"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.GridControlDP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewDP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lkpDiscount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spinMinAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spinMaxAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRequireVoucher.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtValidDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtValidDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciMinAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciMaxAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciRequireVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciValidDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciBtnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciBtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciBtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciBtnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    ' ── Control Declarations ──
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup

    Friend WithEvents GridControlDP As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewDP As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colDpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDiscountName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMinAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaxAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRequireVoucher As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colValidDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colActive As DevExpress.XtraGrid.Columns.GridColumn

    Friend WithEvents txtId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lkpDiscount As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents spinMinAmount As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents spinMaxAmount As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents chkRequireVoucher As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dtValidDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents chkActive As DevExpress.XtraEditors.CheckEdit

    Friend WithEvents btnNew As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton

    Friend WithEvents lciGrid As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciDiscount As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciMinAmount As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciMaxAmount As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciRequireVoucher As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciValidDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciActive As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciBtnNew As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciBtnSave As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciBtnDelete As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciBtnCancel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem

End Class
