<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSalesCommission
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSalesCommission))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.btnFilter = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbFilterSalesman = New DevExpress.XtraEditors.LookUpEdit()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClear = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.dgvCommissions = New DevExpress.XtraGrid.GridControl()
        Me.gvCommissions = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colCommissionId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEmpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colItemId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colItemName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSubGroupId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSubGroupName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCommissionPercentage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCommissionType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFixedAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chkCommissionStatus = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.dgvSalesmen = New DevExpress.XtraGrid.GridControl()
        Me.gvSalesmen = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSalesmanId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSalesmanName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chkStatus = New DevExpress.XtraEditors.CheckEdit()
        Me.txtFixedAmount = New DevExpress.XtraEditors.TextEdit()
        Me.txtCommissionPercentage = New DevExpress.XtraEditors.TextEdit()
        Me.cmbCommissionType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbItem = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbSubGroup = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbSalesman = New DevExpress.XtraEditors.LookUpEdit()
        Me.dgvBulkItems = New DevExpress.XtraGrid.GridControl()
        Me.gvBulkItems = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colBulkSelected = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chkBulkSelected = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colBulkItemId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBulkItemName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmbBulkCommissionType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtBulkCommissionPercentage = New DevExpress.XtraEditors.TextEdit()
        Me.txtBulkFixedAmount = New DevExpress.XtraEditors.TextEdit()
        Me.chkBulkStatus = New DevExpress.XtraEditors.CheckEdit()
        Me.btnBulkUpdate = New DevExpress.XtraEditors.SimpleButton()
        Me.btnBulkSelectAll = New DevExpress.XtraEditors.SimpleButton()
        Me.btnBulkUnselectAll = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbBulkSalesman = New DevExpress.XtraEditors.CheckedComboBoxEdit()
        Me.cmbBulkSubGroup = New DevExpress.XtraEditors.CheckedComboBoxEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem20 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem21 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem22 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem23 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem24 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem25 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.btnbulkdeletebysalesmanid = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlItem26 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.SimpleSeparator1 = New DevExpress.XtraLayout.SimpleSeparator()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.cmbFilterSalesman.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCommissions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCommissions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCommissionStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSalesmen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSalesmen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFixedAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCommissionPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCommissionType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbItem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSubGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSalesman.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvBulkItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvBulkItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBulkSelected, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBulkCommissionType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBulkCommissionPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBulkFixedAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBulkStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBulkSalesman.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBulkSubGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleSeparator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.btnbulkdeletebysalesmanid)
        Me.LayoutControl1.Controls.Add(Me.btnRefresh)
        Me.LayoutControl1.Controls.Add(Me.btnFilter)
        Me.LayoutControl1.Controls.Add(Me.cmbFilterSalesman)
        Me.LayoutControl1.Controls.Add(Me.btnDelete)
        Me.LayoutControl1.Controls.Add(Me.btnClear)
        Me.LayoutControl1.Controls.Add(Me.btnSave)
        Me.LayoutControl1.Controls.Add(Me.dgvCommissions)
        Me.LayoutControl1.Controls.Add(Me.dgvSalesmen)
        Me.LayoutControl1.Controls.Add(Me.chkStatus)
        Me.LayoutControl1.Controls.Add(Me.txtFixedAmount)
        Me.LayoutControl1.Controls.Add(Me.txtCommissionPercentage)
        Me.LayoutControl1.Controls.Add(Me.cmbCommissionType)
        Me.LayoutControl1.Controls.Add(Me.cmbItem)
        Me.LayoutControl1.Controls.Add(Me.cmbSubGroup)
        Me.LayoutControl1.Controls.Add(Me.cmbSalesman)
        Me.LayoutControl1.Controls.Add(Me.dgvBulkItems)
        Me.LayoutControl1.Controls.Add(Me.cmbBulkCommissionType)
        Me.LayoutControl1.Controls.Add(Me.txtBulkCommissionPercentage)
        Me.LayoutControl1.Controls.Add(Me.txtBulkFixedAmount)
        Me.LayoutControl1.Controls.Add(Me.chkBulkStatus)
        Me.LayoutControl1.Controls.Add(Me.btnBulkUpdate)
        Me.LayoutControl1.Controls.Add(Me.btnBulkSelectAll)
        Me.LayoutControl1.Controls.Add(Me.btnBulkUnselectAll)
        Me.LayoutControl1.Controls.Add(Me.cmbBulkSalesman)
        Me.LayoutControl1.Controls.Add(Me.cmbBulkSubGroup)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2640, 502, 250, 350)
        Me.LayoutControl1.Root = Me.Root
        Me.LayoutControl1.Size = New System.Drawing.Size(1200, 700)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(430, 654)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(166, 22)
        Me.btnRefresh.StyleController = Me.LayoutControl1
        Me.btnRefresh.TabIndex = 15
        Me.btnRefresh.Text = "Refresh"
        '
        'btnFilter
        '
        Me.btnFilter.Location = New System.Drawing.Point(351, 654)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(75, 22)
        Me.btnFilter.StyleController = Me.LayoutControl1
        Me.btnFilter.TabIndex = 17
        Me.btnFilter.Text = "Filter"
        '
        'cmbFilterSalesman
        '
        Me.cmbFilterSalesman.Location = New System.Drawing.Point(118, 654)
        Me.cmbFilterSalesman.Name = "cmbFilterSalesman"
        Me.cmbFilterSalesman.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbFilterSalesman.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("emp_printname", "Name")})
        Me.cmbFilterSalesman.Properties.NullText = "Select Salesman..."
        Me.cmbFilterSalesman.Size = New System.Drawing.Size(229, 20)
        Me.cmbFilterSalesman.StyleController = Me.LayoutControl1
        Me.cmbFilterSalesman.TabIndex = 16
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Location = New System.Drawing.Point(221, 301)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(87, 22)
        Me.btnDelete.StyleController = Me.LayoutControl1
        Me.btnDelete.TabIndex = 14
        Me.btnDelete.Text = "Delete"
        '
        'btnClear
        '
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(152, 301)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(65, 22)
        Me.btnClear.StyleController = Me.LayoutControl1
        Me.btnClear.TabIndex = 13
        Me.btnClear.Text = "Clear"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(36, 301)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(112, 22)
        Me.btnSave.StyleController = Me.LayoutControl1
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "Save Commission"
        '
        'dgvCommissions
        '
        Me.dgvCommissions.Location = New System.Drawing.Point(435, 28)
        Me.dgvCommissions.MainView = Me.gvCommissions
        Me.dgvCommissions.Name = "dgvCommissions"
        Me.dgvCommissions.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.chkCommissionStatus})
        Me.dgvCommissions.Size = New System.Drawing.Size(753, 319)
        Me.dgvCommissions.TabIndex = 11
        Me.dgvCommissions.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCommissions})
        '
        'gvCommissions
        '
        Me.gvCommissions.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gvCommissions.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvCommissions.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvCommissions.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvCommissions.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gvCommissions.Appearance.Row.Options.UseFont = True
        Me.gvCommissions.Appearance.Row.Options.UseTextOptions = True
        Me.gvCommissions.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvCommissions.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colCommissionId, Me.colEmpId, Me.colEmpName, Me.colItemId, Me.colItemName, Me.colSubGroupId, Me.colSubGroupName, Me.colCommissionPercentage, Me.colCommissionType, Me.colFixedAmount, Me.colStatus})
        Me.gvCommissions.GridControl = Me.dgvCommissions
        Me.gvCommissions.Name = "gvCommissions"
        Me.gvCommissions.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCommissions.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvCommissions.OptionsBehavior.Editable = False
        Me.gvCommissions.OptionsView.ShowGroupPanel = False
        Me.gvCommissions.RowHeight = 25
        '
        'colCommissionId
        '
        Me.colCommissionId.Caption = "Commission ID"
        Me.colCommissionId.FieldName = "commission_id"
        Me.colCommissionId.Name = "colCommissionId"
        Me.colCommissionId.Visible = True
        Me.colCommissionId.VisibleIndex = 0
        Me.colCommissionId.Width = 80
        '
        'colEmpId
        '
        Me.colEmpId.Caption = "Emp ID"
        Me.colEmpId.FieldName = "emp_id"
        Me.colEmpId.Name = "colEmpId"
        Me.colEmpId.Width = 50
        '
        'colEmpName
        '
        Me.colEmpName.Caption = "Salesman"
        Me.colEmpName.FieldName = "emp_name"
        Me.colEmpName.Name = "colEmpName"
        Me.colEmpName.Visible = True
        Me.colEmpName.VisibleIndex = 1
        Me.colEmpName.Width = 120
        '
        'colItemId
        '
        Me.colItemId.Caption = "Item ID"
        Me.colItemId.FieldName = "item_id"
        Me.colItemId.Name = "colItemId"
        Me.colItemId.Width = 50
        '
        'colItemName
        '
        Me.colItemName.Caption = "Item"
        Me.colItemName.FieldName = "item_name"
        Me.colItemName.Name = "colItemName"
        Me.colItemName.Visible = True
        Me.colItemName.VisibleIndex = 2
        Me.colItemName.Width = 120
        '
        'colSubGroupId
        '
        Me.colSubGroupId.Caption = "Sub Group ID"
        Me.colSubGroupId.FieldName = "sub_group_id"
        Me.colSubGroupId.Name = "colSubGroupId"
        Me.colSubGroupId.Width = 50
        '
        'colSubGroupName
        '
        Me.colSubGroupName.Caption = "Sub Group"
        Me.colSubGroupName.FieldName = "sub_group_name"
        Me.colSubGroupName.Name = "colSubGroupName"
        Me.colSubGroupName.Visible = True
        Me.colSubGroupName.VisibleIndex = 3
        Me.colSubGroupName.Width = 100
        '
        'colCommissionPercentage
        '
        Me.colCommissionPercentage.Caption = "Percentage"
        Me.colCommissionPercentage.FieldName = "commission_percentage"
        Me.colCommissionPercentage.Name = "colCommissionPercentage"
        Me.colCommissionPercentage.Visible = True
        Me.colCommissionPercentage.VisibleIndex = 4
        Me.colCommissionPercentage.Width = 80
        '
        'colCommissionType
        '
        Me.colCommissionType.Caption = "Type"
        Me.colCommissionType.FieldName = "commission_type"
        Me.colCommissionType.Name = "colCommissionType"
        Me.colCommissionType.Visible = True
        Me.colCommissionType.VisibleIndex = 5
        Me.colCommissionType.Width = 80
        '
        'colFixedAmount
        '
        Me.colFixedAmount.Caption = "Fixed Amount"
        Me.colFixedAmount.FieldName = "fixed_amount"
        Me.colFixedAmount.Name = "colFixedAmount"
        Me.colFixedAmount.Visible = True
        Me.colFixedAmount.VisibleIndex = 6
        Me.colFixedAmount.Width = 80
        '
        'colStatus
        '
        Me.colStatus.Caption = "Active"
        Me.colStatus.ColumnEdit = Me.chkCommissionStatus
        Me.colStatus.FieldName = "status"
        Me.colStatus.Name = "colStatus"
        Me.colStatus.Visible = True
        Me.colStatus.VisibleIndex = 7
        Me.colStatus.Width = 60
        '
        'chkCommissionStatus
        '
        Me.chkCommissionStatus.AutoHeight = False
        Me.chkCommissionStatus.Caption = "Check"
        Me.chkCommissionStatus.Name = "chkCommissionStatus"
        '
        'dgvSalesmen
        '
        Me.dgvSalesmen.Location = New System.Drawing.Point(12, 28)
        Me.dgvSalesmen.MainView = Me.gvSalesmen
        Me.dgvSalesmen.Name = "dgvSalesmen"
        Me.dgvSalesmen.Size = New System.Drawing.Size(419, 64)
        Me.dgvSalesmen.TabIndex = 12
        Me.dgvSalesmen.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvSalesmen})
        '
        'gvSalesmen
        '
        Me.gvSalesmen.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gvSalesmen.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvSalesmen.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvSalesmen.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvSalesmen.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gvSalesmen.Appearance.Row.Options.UseFont = True
        Me.gvSalesmen.Appearance.Row.Options.UseTextOptions = True
        Me.gvSalesmen.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvSalesmen.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSalesmanId, Me.colSalesmanName})
        Me.gvSalesmen.GridControl = Me.dgvSalesmen
        Me.gvSalesmen.Name = "gvSalesmen"
        Me.gvSalesmen.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvSalesmen.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvSalesmen.OptionsBehavior.Editable = False
        Me.gvSalesmen.OptionsView.ShowGroupPanel = False
        '
        'colSalesmanId
        '
        Me.colSalesmanId.Caption = "ID"
        Me.colSalesmanId.FieldName = "emp_id"
        Me.colSalesmanId.Name = "colSalesmanId"
        Me.colSalesmanId.Visible = True
        Me.colSalesmanId.VisibleIndex = 0
        Me.colSalesmanId.Width = 50
        '
        'colSalesmanName
        '
        Me.colSalesmanName.Caption = "Salesman Name"
        Me.colSalesmanName.FieldName = "emp_printname"
        Me.colSalesmanName.Name = "colSalesmanName"
        Me.colSalesmanName.Visible = True
        Me.colSalesmanName.VisibleIndex = 1
        Me.colSalesmanName.Width = 300
        '
        'chkStatus
        '
        Me.chkStatus.Location = New System.Drawing.Point(118, 247)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.Properties.Caption = "Active"
        Me.chkStatus.Size = New System.Drawing.Size(301, 19)
        Me.chkStatus.StyleController = Me.LayoutControl1
        Me.chkStatus.TabIndex = 10
        '
        'txtFixedAmount
        '
        Me.txtFixedAmount.Location = New System.Drawing.Point(118, 223)
        Me.txtFixedAmount.Name = "txtFixedAmount"
        Me.txtFixedAmount.Properties.Mask.EditMask = "n2"
        Me.txtFixedAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtFixedAmount.Size = New System.Drawing.Size(301, 20)
        Me.txtFixedAmount.StyleController = Me.LayoutControl1
        Me.txtFixedAmount.TabIndex = 9
        '
        'txtCommissionPercentage
        '
        Me.txtCommissionPercentage.Location = New System.Drawing.Point(280, 199)
        Me.txtCommissionPercentage.Name = "txtCommissionPercentage"
        Me.txtCommissionPercentage.Properties.Mask.EditMask = "n2"
        Me.txtCommissionPercentage.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtCommissionPercentage.Size = New System.Drawing.Size(139, 20)
        Me.txtCommissionPercentage.StyleController = Me.LayoutControl1
        Me.txtCommissionPercentage.TabIndex = 8
        '
        'cmbCommissionType
        '
        Me.cmbCommissionType.Location = New System.Drawing.Point(118, 199)
        Me.cmbCommissionType.Name = "cmbCommissionType"
        Me.cmbCommissionType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCommissionType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cmbCommissionType.Size = New System.Drawing.Size(64, 20)
        Me.cmbCommissionType.StyleController = Me.LayoutControl1
        Me.cmbCommissionType.TabIndex = 7
        '
        'cmbItem
        '
        Me.cmbItem.Location = New System.Drawing.Point(118, 175)
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbItem.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ItemName", "Item Name")})
        Me.cmbItem.Properties.NullText = "Select Item..."
        Me.cmbItem.Size = New System.Drawing.Size(301, 20)
        Me.cmbItem.StyleController = Me.LayoutControl1
        Me.cmbItem.TabIndex = 6
        '
        'cmbSubGroup
        '
        Me.cmbSubGroup.Location = New System.Drawing.Point(118, 151)
        Me.cmbSubGroup.Name = "cmbSubGroup"
        Me.cmbSubGroup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbSubGroup.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubGroupName", "Sub Group Name")})
        Me.cmbSubGroup.Properties.NullText = "Select Sub Group..."
        Me.cmbSubGroup.Size = New System.Drawing.Size(301, 20)
        Me.cmbSubGroup.StyleController = Me.LayoutControl1
        Me.cmbSubGroup.TabIndex = 5
        '
        'cmbSalesman
        '
        Me.cmbSalesman.Location = New System.Drawing.Point(118, 127)
        Me.cmbSalesman.Name = "cmbSalesman"
        Me.cmbSalesman.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbSalesman.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("emp_printname", "Employee Name")})
        Me.cmbSalesman.Properties.NullText = "Select Salesman..."
        Me.cmbSalesman.Size = New System.Drawing.Size(301, 20)
        Me.cmbSalesman.StyleController = Me.LayoutControl1
        Me.cmbSalesman.TabIndex = 4
        '
        'dgvBulkItems
        '
        Me.dgvBulkItems.Location = New System.Drawing.Point(24, 442)
        Me.dgvBulkItems.MainView = Me.gvBulkItems
        Me.dgvBulkItems.Name = "dgvBulkItems"
        Me.dgvBulkItems.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.chkBulkSelected})
        Me.dgvBulkItems.Size = New System.Drawing.Size(690, 165)
        Me.dgvBulkItems.TabIndex = 20
        Me.dgvBulkItems.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvBulkItems})
        '
        'gvBulkItems
        '
        Me.gvBulkItems.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colBulkSelected, Me.colBulkItemId, Me.colBulkItemName})
        Me.gvBulkItems.GridControl = Me.dgvBulkItems
        Me.gvBulkItems.Name = "gvBulkItems"
        Me.gvBulkItems.OptionsView.ShowGroupPanel = False
        '
        'colBulkSelected
        '
        Me.colBulkSelected.Caption = "Select"
        Me.colBulkSelected.ColumnEdit = Me.chkBulkSelected
        Me.colBulkSelected.FieldName = "Selected"
        Me.colBulkSelected.Name = "colBulkSelected"
        Me.colBulkSelected.Visible = True
        Me.colBulkSelected.VisibleIndex = 0
        Me.colBulkSelected.Width = 60
        '
        'chkBulkSelected
        '
        Me.chkBulkSelected.Caption = "Check"
        Me.chkBulkSelected.Name = "chkBulkSelected"
        '
        'colBulkItemId
        '
        Me.colBulkItemId.Caption = "Item ID"
        Me.colBulkItemId.FieldName = "ItemId"
        Me.colBulkItemId.Name = "colBulkItemId"
        Me.colBulkItemId.Visible = True
        Me.colBulkItemId.VisibleIndex = 1
        Me.colBulkItemId.Width = 80
        '
        'colBulkItemName
        '
        Me.colBulkItemName.Caption = "Item Name"
        Me.colBulkItemName.FieldName = "ItemName"
        Me.colBulkItemName.Name = "colBulkItemName"
        Me.colBulkItemName.Visible = True
        Me.colBulkItemName.VisibleIndex = 2
        Me.colBulkItemName.Width = 200
        '
        'cmbBulkCommissionType
        '
        Me.cmbBulkCommissionType.Location = New System.Drawing.Point(812, 474)
        Me.cmbBulkCommissionType.Name = "cmbBulkCommissionType"
        Me.cmbBulkCommissionType.Properties.Items.AddRange(New Object() {"Percentage", "Fixed Amount"})
        Me.cmbBulkCommissionType.Size = New System.Drawing.Size(364, 20)
        Me.cmbBulkCommissionType.StyleController = Me.LayoutControl1
        Me.cmbBulkCommissionType.TabIndex = 23
        '
        'txtBulkCommissionPercentage
        '
        Me.txtBulkCommissionPercentage.Location = New System.Drawing.Point(812, 498)
        Me.txtBulkCommissionPercentage.Name = "txtBulkCommissionPercentage"
        Me.txtBulkCommissionPercentage.Size = New System.Drawing.Size(364, 20)
        Me.txtBulkCommissionPercentage.StyleController = Me.LayoutControl1
        Me.txtBulkCommissionPercentage.TabIndex = 24
        '
        'txtBulkFixedAmount
        '
        Me.txtBulkFixedAmount.Location = New System.Drawing.Point(812, 522)
        Me.txtBulkFixedAmount.Name = "txtBulkFixedAmount"
        Me.txtBulkFixedAmount.Size = New System.Drawing.Size(364, 20)
        Me.txtBulkFixedAmount.StyleController = Me.LayoutControl1
        Me.txtBulkFixedAmount.TabIndex = 25
        '
        'chkBulkStatus
        '
        Me.chkBulkStatus.Location = New System.Drawing.Point(812, 546)
        Me.chkBulkStatus.Name = "chkBulkStatus"
        Me.chkBulkStatus.Properties.Caption = "Active"
        Me.chkBulkStatus.Size = New System.Drawing.Size(364, 19)
        Me.chkBulkStatus.StyleController = Me.LayoutControl1
        Me.chkBulkStatus.TabIndex = 26
        '
        'btnBulkUpdate
        '
        Me.btnBulkUpdate.Image = CType(resources.GetObject("btnBulkUpdate.Image"), System.Drawing.Image)
        Me.btnBulkUpdate.Location = New System.Drawing.Point(718, 569)
        Me.btnBulkUpdate.Name = "btnBulkUpdate"
        Me.btnBulkUpdate.Size = New System.Drawing.Size(227, 38)
        Me.btnBulkUpdate.StyleController = Me.LayoutControl1
        Me.btnBulkUpdate.TabIndex = 27
        Me.btnBulkUpdate.Text = "Bulk Update"
        '
        'btnBulkSelectAll
        '
        Me.btnBulkSelectAll.Image = CType(resources.GetObject("btnBulkSelectAll.Image"), System.Drawing.Image)
        Me.btnBulkSelectAll.Location = New System.Drawing.Point(24, 384)
        Me.btnBulkSelectAll.Name = "btnBulkSelectAll"
        Me.btnBulkSelectAll.Size = New System.Drawing.Size(126, 38)
        Me.btnBulkSelectAll.StyleController = Me.LayoutControl1
        Me.btnBulkSelectAll.TabIndex = 28
        Me.btnBulkSelectAll.Text = "Select All"
        '
        'btnBulkUnselectAll
        '
        Me.btnBulkUnselectAll.Image = CType(resources.GetObject("btnBulkUnselectAll.Image"), System.Drawing.Image)
        Me.btnBulkUnselectAll.Location = New System.Drawing.Point(154, 384)
        Me.btnBulkUnselectAll.Name = "btnBulkUnselectAll"
        Me.btnBulkUnselectAll.Size = New System.Drawing.Size(132, 38)
        Me.btnBulkUnselectAll.StyleController = Me.LayoutControl1
        Me.btnBulkUnselectAll.TabIndex = 29
        Me.btnBulkUnselectAll.Text = "Unselect All"
        '
        'cmbBulkSalesman
        '
        Me.cmbBulkSalesman.Location = New System.Drawing.Point(812, 426)
        Me.cmbBulkSalesman.Name = "cmbBulkSalesman"
        Me.cmbBulkSalesman.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBulkSalesman.Properties.NullText = "Select Salesman..."
        Me.cmbBulkSalesman.Size = New System.Drawing.Size(364, 20)
        Me.cmbBulkSalesman.StyleController = Me.LayoutControl1
        Me.cmbBulkSalesman.TabIndex = 21
        '
        'cmbBulkSubGroup
        '
        Me.cmbBulkSubGroup.Location = New System.Drawing.Point(812, 450)
        Me.cmbBulkSubGroup.Name = "cmbBulkSubGroup"
        Me.cmbBulkSubGroup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBulkSubGroup.Properties.NullText = "Select Sub Group..."
        Me.cmbBulkSubGroup.Size = New System.Drawing.Size(364, 20)
        Me.cmbBulkSubGroup.StyleController = Me.LayoutControl1
        Me.cmbBulkSubGroup.TabIndex = 22
        '
        'Root
        '
        Me.Root.CustomizationFormText = "Root"
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1, Me.LayoutControlItem15, Me.LayoutControlItem12, Me.LayoutControlGroup3, Me.LayoutControlGroup4, Me.SimpleSeparator1})
        Me.Root.Location = New System.Drawing.Point(0, 0)
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1200, 700)
        Me.Root.Text = "Root"
        Me.Root.TextVisible = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Commission Details"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem7, Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 84)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(423, 255)
        Me.LayoutControlGroup1.Text = "Commission Details"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.cmbSalesman
        Me.LayoutControlItem1.CustomizationFormText = "Salesman:"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(399, 24)
        Me.LayoutControlItem1.Text = "Salesman:"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.cmbSubGroup
        Me.LayoutControlItem2.CustomizationFormText = "Sub Group:"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(399, 24)
        Me.LayoutControlItem2.Text = "Sub Group:"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.cmbItem
        Me.LayoutControlItem3.CustomizationFormText = "Item:"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(399, 24)
        Me.LayoutControlItem3.Text = "Item:"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.cmbCommissionType
        Me.LayoutControlItem4.CustomizationFormText = "Commission Type:"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(162, 24)
        Me.LayoutControlItem4.Text = "Commission Type:"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.txtCommissionPercentage
        Me.LayoutControlItem5.CustomizationFormText = "Percentage (%):"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(162, 72)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(237, 24)
        Me.LayoutControlItem5.Text = "Percentage (%):"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.txtFixedAmount
        Me.LayoutControlItem6.CustomizationFormText = "Fixed Amount:"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(399, 24)
        Me.LayoutControlItem6.Text = "Fixed Amount:"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.chkStatus
        Me.LayoutControlItem7.CustomizationFormText = "Status:"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(399, 23)
        Me.LayoutControlItem7.Text = "Status:"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.CustomizationFormText = "Actions"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem8, Me.LayoutControlItem9, Me.LayoutControlItem10, Me.LayoutControlItem26})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 143)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(399, 69)
        Me.LayoutControlGroup2.Text = "Actions"
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.btnSave
        Me.LayoutControlItem8.CustomizationFormText = "LayoutControlItem8"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(116, 26)
        Me.LayoutControlItem8.Text = "LayoutControlItem8"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextToControlDistance = 0
        Me.LayoutControlItem8.TextVisible = False
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.btnClear
        Me.LayoutControlItem9.CustomizationFormText = "LayoutControlItem9"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(116, 0)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(69, 26)
        Me.LayoutControlItem9.Text = "LayoutControlItem9"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem9.TextToControlDistance = 0
        Me.LayoutControlItem9.TextVisible = False
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.btnDelete
        Me.LayoutControlItem10.CustomizationFormText = "LayoutControlItem10"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(185, 0)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(91, 26)
        Me.LayoutControlItem10.Text = "LayoutControlItem10"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem10.TextToControlDistance = 0
        Me.LayoutControlItem10.TextVisible = False
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.Control = Me.dgvSalesmen
        Me.LayoutControlItem15.CustomizationFormText = "Salesmen:"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(423, 84)
        Me.LayoutControlItem15.Text = "Salesmen:"
        Me.LayoutControlItem15.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.dgvCommissions
        Me.LayoutControlItem12.CustomizationFormText = "Commissions:"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(423, 0)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(757, 339)
        Me.LayoutControlItem12.Text = "Commissions:"
        Me.LayoutControlItem12.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.CustomizationFormText = "Filter & Actions"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem13, Me.LayoutControlItem14, Me.LayoutControlItem11, Me.EmptySpaceItem2})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 611)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(1180, 69)
        Me.LayoutControlGroup3.Text = "Filter & Actions"
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.cmbFilterSalesman
        Me.LayoutControlItem13.CustomizationFormText = "Filter by Salesman:"
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(327, 26)
        Me.LayoutControlItem13.Text = "Filter by Salesman:"
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.Control = Me.btnFilter
        Me.LayoutControlItem14.CustomizationFormText = "LayoutControlItem14"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(327, 0)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(79, 26)
        Me.LayoutControlItem14.Text = "LayoutControlItem14"
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem14.TextToControlDistance = 0
        Me.LayoutControlItem14.TextVisible = False
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.btnRefresh
        Me.LayoutControlItem11.CustomizationFormText = "LayoutControlItem11"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(406, 0)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(170, 26)
        Me.LayoutControlItem11.Text = "LayoutControlItem11"
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem11.TextToControlDistance = 0
        Me.LayoutControlItem11.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(576, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(580, 26)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.CustomizationFormText = "Bulk Commission Update"
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem16, Me.LayoutControlItem17, Me.LayoutControlItem18, Me.LayoutControlItem19, Me.LayoutControlItem20, Me.LayoutControlItem21, Me.LayoutControlItem22, Me.LayoutControlItem23, Me.LayoutControlItem24, Me.LayoutControlItem25, Me.EmptySpaceItem3, Me.EmptySpaceItem4})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 341)
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(1180, 270)
        Me.LayoutControlGroup4.Text = "Bulk Commission Update"
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.Control = Me.cmbBulkSalesman
        Me.LayoutControlItem16.CustomizationFormText = "Salesman:"
        Me.LayoutControlItem16.Location = New System.Drawing.Point(694, 42)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(462, 24)
        Me.LayoutControlItem16.Text = "Salesman:"
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.Control = Me.cmbBulkSubGroup
        Me.LayoutControlItem17.CustomizationFormText = "Sub Group:"
        Me.LayoutControlItem17.Location = New System.Drawing.Point(694, 66)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(462, 24)
        Me.LayoutControlItem17.Text = "Sub Group:"
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.Control = Me.dgvBulkItems
        Me.LayoutControlItem18.CustomizationFormText = "Items:"
        Me.LayoutControlItem18.Location = New System.Drawing.Point(0, 42)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Size = New System.Drawing.Size(694, 185)
        Me.LayoutControlItem18.Text = "Items to Select:"
        Me.LayoutControlItem18.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.Control = Me.cmbBulkCommissionType
        Me.LayoutControlItem19.CustomizationFormText = "Commission Type:"
        Me.LayoutControlItem19.Location = New System.Drawing.Point(694, 90)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(462, 24)
        Me.LayoutControlItem19.Text = "Type:"
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlItem20
        '
        Me.LayoutControlItem20.Control = Me.txtBulkCommissionPercentage
        Me.LayoutControlItem20.CustomizationFormText = "Percentage:"
        Me.LayoutControlItem20.Location = New System.Drawing.Point(694, 114)
        Me.LayoutControlItem20.Name = "LayoutControlItem20"
        Me.LayoutControlItem20.Size = New System.Drawing.Size(462, 24)
        Me.LayoutControlItem20.Text = "Percentage:"
        Me.LayoutControlItem20.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlItem21
        '
        Me.LayoutControlItem21.Control = Me.txtBulkFixedAmount
        Me.LayoutControlItem21.CustomizationFormText = "Fixed Amount:"
        Me.LayoutControlItem21.Location = New System.Drawing.Point(694, 138)
        Me.LayoutControlItem21.Name = "LayoutControlItem21"
        Me.LayoutControlItem21.Size = New System.Drawing.Size(462, 24)
        Me.LayoutControlItem21.Text = "Fixed Amount:"
        Me.LayoutControlItem21.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlItem22
        '
        Me.LayoutControlItem22.Control = Me.chkBulkStatus
        Me.LayoutControlItem22.CustomizationFormText = "Status:"
        Me.LayoutControlItem22.Location = New System.Drawing.Point(694, 162)
        Me.LayoutControlItem22.Name = "LayoutControlItem22"
        Me.LayoutControlItem22.Size = New System.Drawing.Size(462, 23)
        Me.LayoutControlItem22.Text = "Status:"
        Me.LayoutControlItem22.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlItem23
        '
        Me.LayoutControlItem23.Control = Me.btnBulkUpdate
        Me.LayoutControlItem23.CustomizationFormText = "LayoutControlItem23"
        Me.LayoutControlItem23.Location = New System.Drawing.Point(694, 185)
        Me.LayoutControlItem23.Name = "LayoutControlItem23"
        Me.LayoutControlItem23.Size = New System.Drawing.Size(231, 42)
        Me.LayoutControlItem23.Text = "LayoutControlItem23"
        Me.LayoutControlItem23.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem23.TextToControlDistance = 0
        Me.LayoutControlItem23.TextVisible = False
        '
        'LayoutControlItem24
        '
        Me.LayoutControlItem24.Control = Me.btnBulkSelectAll
        Me.LayoutControlItem24.CustomizationFormText = "LayoutControlItem24"
        Me.LayoutControlItem24.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem24.Name = "LayoutControlItem24"
        Me.LayoutControlItem24.Size = New System.Drawing.Size(130, 42)
        Me.LayoutControlItem24.Text = "LayoutControlItem24"
        Me.LayoutControlItem24.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem24.TextToControlDistance = 0
        Me.LayoutControlItem24.TextVisible = False
        '
        'LayoutControlItem25
        '
        Me.LayoutControlItem25.Control = Me.btnBulkUnselectAll
        Me.LayoutControlItem25.CustomizationFormText = "LayoutControlItem25"
        Me.LayoutControlItem25.Location = New System.Drawing.Point(130, 0)
        Me.LayoutControlItem25.Name = "LayoutControlItem25"
        Me.LayoutControlItem25.Size = New System.Drawing.Size(136, 42)
        Me.LayoutControlItem25.Text = "LayoutControlItem25"
        Me.LayoutControlItem25.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem25.TextToControlDistance = 0
        Me.LayoutControlItem25.TextVisible = False
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(266, 0)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(890, 42)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'btnbulkdeletebysalesmanid
        '
        Me.btnbulkdeletebysalesmanid.Image = CType(resources.GetObject("btnbulkdeletebysalesmanid.Image"), System.Drawing.Image)
        Me.btnbulkdeletebysalesmanid.Location = New System.Drawing.Point(312, 301)
        Me.btnbulkdeletebysalesmanid.Name = "btnbulkdeletebysalesmanid"
        Me.btnbulkdeletebysalesmanid.Size = New System.Drawing.Size(95, 22)
        Me.btnbulkdeletebysalesmanid.StyleController = Me.LayoutControl1
        Me.btnbulkdeletebysalesmanid.TabIndex = 30
        Me.btnbulkdeletebysalesmanid.Text = "Bulk Delete"
        '
        'LayoutControlItem26
        '
        Me.LayoutControlItem26.Control = Me.btnbulkdeletebysalesmanid
        Me.LayoutControlItem26.CustomizationFormText = "LayoutControlItem26"
        Me.LayoutControlItem26.Location = New System.Drawing.Point(276, 0)
        Me.LayoutControlItem26.Name = "LayoutControlItem26"
        Me.LayoutControlItem26.Size = New System.Drawing.Size(99, 26)
        Me.LayoutControlItem26.Text = "LayoutControlItem26"
        Me.LayoutControlItem26.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem26.TextToControlDistance = 0
        Me.LayoutControlItem26.TextVisible = False
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.AllowHotTrack = False
        Me.EmptySpaceItem4.CustomizationFormText = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(925, 185)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(231, 42)
        Me.EmptySpaceItem4.Text = "EmptySpaceItem4"
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'SimpleSeparator1
        '
        Me.SimpleSeparator1.AllowHotTrack = False
        Me.SimpleSeparator1.CustomizationFormText = "SimpleSeparator1"
        Me.SimpleSeparator1.Location = New System.Drawing.Point(0, 339)
        Me.SimpleSeparator1.Name = "SimpleSeparator1"
        Me.SimpleSeparator1.Size = New System.Drawing.Size(1180, 2)
        Me.SimpleSeparator1.Text = "SimpleSeparator1"
        '
        'FrmSalesCommission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 700)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "FrmSalesCommission"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sales Commission Management"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.cmbFilterSalesman.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvCommissions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCommissions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCommissionStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSalesmen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSalesmen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFixedAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCommissionPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCommissionType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbItem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSubGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSalesman.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvBulkItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvBulkItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBulkSelected, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBulkCommissionType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBulkCommissionPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBulkFixedAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBulkStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBulkSalesman.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBulkSubGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleSeparator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents btnFilter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbFilterSalesman As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnClear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents dgvCommissions As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCommissions As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colCommissionId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colEmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colEmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colItemId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colItemName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSubGroupId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSubGroupName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCommissionPercentage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCommissionType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFixedAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chkCommissionStatus As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents chkStatus As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtFixedAmount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCommissionPercentage As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbCommissionType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbItem As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbSubGroup As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbSalesman As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents dgvSalesmen As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvSalesmen As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colSalesmanId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSalesmanName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    ' Bulk Update Controls Friend WithEvents
    Friend WithEvents dgvBulkItems As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvBulkItems As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colBulkSelected As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chkBulkSelected As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colBulkItemId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBulkItemName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmbBulkCommissionType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtBulkCommissionPercentage As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtBulkFixedAmount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkBulkStatus As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnBulkUpdate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnBulkSelectAll As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnBulkUnselectAll As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem20 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem21 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem22 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem23 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem24 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem25 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents cmbBulkSalesman As DevExpress.XtraEditors.CheckedComboBoxEdit
    Friend WithEvents cmbBulkSubGroup As DevExpress.XtraEditors.CheckedComboBoxEdit
    Friend WithEvents btnbulkdeletebysalesmanid As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem26 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents SimpleSeparator1 As DevExpress.XtraLayout.SimpleSeparator
End Class
