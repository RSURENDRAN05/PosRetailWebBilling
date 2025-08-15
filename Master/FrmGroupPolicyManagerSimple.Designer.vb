<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGroupPolicyManagerSimple
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClearGroup = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDeleteGroup = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSavePermissions = New DevExpress.XtraEditors.SimpleButton()
        Me.dgvPermissions = New DevExpress.XtraGrid.GridControl()
        Me.gvPermissions = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colMenuName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMenuType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMenuActive = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chkMenuActive = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.dgvGroups = New DevExpress.XtraGrid.GridControl()
        Me.gvGroups = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colGroupId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colGroupName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colGroupDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colGroupActive = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chkGroupActive = New DevExpress.XtraEditors.CheckEdit()
        Me.txtGroupDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.txtGroupName = New DevExpress.XtraEditors.TextEdit()
        Me.btnSaveGroup = New DevExpress.XtraEditors.SimpleButton()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.dgvPermissions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPermissions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMenuActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvGroups, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvGroups, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGroupActive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGroupDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGroupName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.btnRefresh)
        Me.LayoutControl1.Controls.Add(Me.btnClearGroup)
        Me.LayoutControl1.Controls.Add(Me.btnDeleteGroup)
        Me.LayoutControl1.Controls.Add(Me.btnSavePermissions)
        Me.LayoutControl1.Controls.Add(Me.dgvPermissions)
        Me.LayoutControl1.Controls.Add(Me.dgvGroups)
        Me.LayoutControl1.Controls.Add(Me.chkGroupActive)
        Me.LayoutControl1.Controls.Add(Me.txtGroupDescription)
        Me.LayoutControl1.Controls.Add(Me.txtGroupName)
        Me.LayoutControl1.Controls.Add(Me.btnSaveGroup)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2149, 152, 250, 350)
        Me.LayoutControl1.Root = Me.Root
        Me.LayoutControl1.Size = New System.Drawing.Size(1200, 800)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(36, 175)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(79, 22)
        Me.btnRefresh.StyleController = Me.LayoutControl1
        Me.btnRefresh.TabIndex = 13
        Me.btnRefresh.Text = "Refresh"
        '
        'btnClearGroup
        '
        Me.btnClearGroup.Location = New System.Drawing.Point(238, 175)
        Me.btnClearGroup.Name = "btnClearGroup"
        Me.btnClearGroup.Size = New System.Drawing.Size(80, 22)
        Me.btnClearGroup.StyleController = Me.LayoutControl1
        Me.btnClearGroup.TabIndex = 12
        Me.btnClearGroup.Text = "Clear"
        '
        'btnDeleteGroup
        '
        Me.btnDeleteGroup.Location = New System.Drawing.Point(322, 175)
        Me.btnDeleteGroup.Name = "btnDeleteGroup"
        Me.btnDeleteGroup.Size = New System.Drawing.Size(134, 22)
        Me.btnDeleteGroup.StyleController = Me.LayoutControl1
        Me.btnDeleteGroup.TabIndex = 11
        Me.btnDeleteGroup.Text = "Delete"
        '
        'btnSavePermissions
        '
        Me.btnSavePermissions.Location = New System.Drawing.Point(1040, 175)
        Me.btnSavePermissions.Name = "btnSavePermissions"
        Me.btnSavePermissions.Size = New System.Drawing.Size(124, 22)
        Me.btnSavePermissions.StyleController = Me.LayoutControl1
        Me.btnSavePermissions.TabIndex = 10
        Me.btnSavePermissions.Text = "Save Permissions"
        '
        'dgvPermissions
        '
        Me.dgvPermissions.Location = New System.Drawing.Point(623, 217)
        Me.dgvPermissions.MainView = Me.gvPermissions
        Me.dgvPermissions.Name = "dgvPermissions"
        Me.dgvPermissions.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.chkMenuActive})
        Me.dgvPermissions.Size = New System.Drawing.Size(541, 547)
        Me.dgvPermissions.TabIndex = 9
        Me.dgvPermissions.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvPermissions})
        '
        'gvPermissions
        '
        Me.gvPermissions.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvPermissions.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvPermissions.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colMenuName, Me.colMenuType, Me.colMenuActive})
        Me.gvPermissions.GridControl = Me.dgvPermissions
        Me.gvPermissions.Name = "gvPermissions"
        Me.gvPermissions.OptionsView.ShowGroupPanel = False
        '
        'colMenuName
        '
        Me.colMenuName.Caption = "Menu Name"
        Me.colMenuName.FieldName = "menu_name"
        Me.colMenuName.Name = "colMenuName"
        Me.colMenuName.OptionsColumn.AllowEdit = False
        Me.colMenuName.OptionsColumn.ReadOnly = True
        Me.colMenuName.Visible = True
        Me.colMenuName.VisibleIndex = 0
        Me.colMenuName.Width = 200
        '
        'colMenuType
        '
        Me.colMenuType.Caption = "Type"
        Me.colMenuType.FieldName = "menu_type"
        Me.colMenuType.Name = "colMenuType"
        Me.colMenuType.OptionsColumn.AllowEdit = False
        Me.colMenuType.OptionsColumn.ReadOnly = True
        Me.colMenuType.Visible = True
        Me.colMenuType.VisibleIndex = 1
        Me.colMenuType.Width = 80
        '
        'colMenuActive
        '
        Me.colMenuActive.Caption = "Active"
        Me.colMenuActive.ColumnEdit = Me.chkMenuActive
        Me.colMenuActive.FieldName = "menu_active"
        Me.colMenuActive.Name = "colMenuActive"
        Me.colMenuActive.Visible = True
        Me.colMenuActive.VisibleIndex = 2
        Me.colMenuActive.Width = 80
        '
        'chkMenuActive
        '
        Me.chkMenuActive.AutoHeight = False
        Me.chkMenuActive.Caption = "Check"
        Me.chkMenuActive.Name = "chkMenuActive"
        '
        'dgvGroups
        '
        Me.dgvGroups.Location = New System.Drawing.Point(36, 217)
        Me.dgvGroups.MainView = Me.gvGroups
        Me.dgvGroups.Name = "dgvGroups"
        Me.dgvGroups.Size = New System.Drawing.Size(559, 547)
        Me.dgvGroups.TabIndex = 8
        Me.dgvGroups.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvGroups})
        '
        'gvGroups
        '
        Me.gvGroups.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colGroupId, Me.colGroupName, Me.colGroupDescription, Me.colGroupActive})
        Me.gvGroups.GridControl = Me.dgvGroups
        Me.gvGroups.Name = "gvGroups"
        Me.gvGroups.OptionsBehavior.Editable = False
        Me.gvGroups.OptionsBehavior.ReadOnly = True
        Me.gvGroups.OptionsView.ShowGroupPanel = False
        '
        'colGroupId
        '
        Me.colGroupId.FieldName = "pug_id"
        Me.colGroupId.Name = "colGroupId"
        '
        'colGroupName
        '
        Me.colGroupName.Caption = "Group Name"
        Me.colGroupName.FieldName = "pug_name"
        Me.colGroupName.Name = "colGroupName"
        Me.colGroupName.Visible = True
        Me.colGroupName.VisibleIndex = 0
        Me.colGroupName.Width = 150
        '
        'colGroupDescription
        '
        Me.colGroupDescription.Caption = "Description"
        Me.colGroupDescription.FieldName = "pug_description"
        Me.colGroupDescription.Name = "colGroupDescription"
        Me.colGroupDescription.Visible = True
        Me.colGroupDescription.VisibleIndex = 1
        Me.colGroupDescription.Width = 250
        '
        'colGroupActive
        '
        Me.colGroupActive.Caption = "Active"
        Me.colGroupActive.FieldName = "pug_active"
        Me.colGroupActive.Name = "colGroupActive"
        Me.colGroupActive.Visible = True
        Me.colGroupActive.VisibleIndex = 2
        Me.colGroupActive.Width = 80
        '
        'chkGroupActive
        '
        Me.chkGroupActive.EditValue = True
        Me.chkGroupActive.Location = New System.Drawing.Point(611, 43)
        Me.chkGroupActive.Name = "chkGroupActive"
        Me.chkGroupActive.Properties.Caption = "Active"
        Me.chkGroupActive.Size = New System.Drawing.Size(565, 19)
        Me.chkGroupActive.StyleController = Me.LayoutControl1
        Me.chkGroupActive.TabIndex = 7
        '
        'txtGroupDescription
        '
        Me.txtGroupDescription.Location = New System.Drawing.Point(111, 67)
        Me.txtGroupDescription.Name = "txtGroupDescription"
        Me.txtGroupDescription.Size = New System.Drawing.Size(1065, 73)
        Me.txtGroupDescription.StyleController = Me.LayoutControl1
        Me.txtGroupDescription.TabIndex = 6
        '
        'txtGroupName
        '
        Me.txtGroupName.Location = New System.Drawing.Point(111, 43)
        Me.txtGroupName.Name = "txtGroupName"
        Me.txtGroupName.Size = New System.Drawing.Size(496, 20)
        Me.txtGroupName.StyleController = Me.LayoutControl1
        Me.txtGroupName.TabIndex = 5
        '
        'btnSaveGroup
        '
        Me.btnSaveGroup.Location = New System.Drawing.Point(119, 175)
        Me.btnSaveGroup.Name = "btnSaveGroup"
        Me.btnSaveGroup.Size = New System.Drawing.Size(115, 22)
        Me.btnSaveGroup.StyleController = Me.LayoutControl1
        Me.btnSaveGroup.TabIndex = 4
        Me.btnSaveGroup.Text = "Add Group"
        '
        'Root
        '
        Me.Root.CustomizationFormText = "Root"
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1})
        Me.Root.Location = New System.Drawing.Point(0, 0)
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1200, 800)
        Me.Root.Text = "Root"
        Me.Root.TextVisible = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Group Policy Management"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlGroup2, Me.LayoutControlGroup3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1180, 780)
        Me.LayoutControlGroup1.Text = "Group Policy Management"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.txtGroupName
        Me.LayoutControlItem1.CustomizationFormText = "Group Name:"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(587, 24)
        Me.LayoutControlItem1.Text = "Group Name:"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(84, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.txtGroupDescription
        Me.LayoutControlItem2.CustomizationFormText = "Description:"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(1156, 77)
        Me.LayoutControlItem2.Text = "Description:"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(84, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.chkGroupActive
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(587, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(569, 24)
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.CustomizationFormText = "Groups Management"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem10, Me.LayoutControlItem4, Me.LayoutControlItem9, Me.LayoutControlItem8, Me.LayoutControlItem5, Me.EmptySpaceItem1})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 101)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(587, 636)
        Me.LayoutControlGroup2.Text = "Groups Management"
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.btnRefresh
        Me.LayoutControlItem10.CustomizationFormText = "LayoutControlItem10"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(83, 26)
        Me.LayoutControlItem10.Text = "LayoutControlItem10"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem10.TextToControlDistance = 0
        Me.LayoutControlItem10.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.btnSaveGroup
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(83, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(119, 26)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.btnClearGroup
        Me.LayoutControlItem9.CustomizationFormText = "LayoutControlItem9"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(202, 0)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(84, 26)
        Me.LayoutControlItem9.Text = "LayoutControlItem9"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem9.TextToControlDistance = 0
        Me.LayoutControlItem9.TextVisible = False
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.btnDeleteGroup
        Me.LayoutControlItem8.CustomizationFormText = "LayoutControlItem8"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(286, 0)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(138, 26)
        Me.LayoutControlItem8.Text = "LayoutControlItem8"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextToControlDistance = 0
        Me.LayoutControlItem8.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.dgvGroups
        Me.LayoutControlItem5.CustomizationFormText = "User Groups"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 26)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(563, 567)
        Me.LayoutControlItem5.Text = "User Groups"
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(84, 13)
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.CustomizationFormText = "Permissions Management"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7, Me.LayoutControlItem6, Me.EmptySpaceItem2})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(587, 101)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(569, 636)
        Me.LayoutControlGroup3.Text = "Permissions Management"
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.btnSavePermissions
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(417, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(128, 26)
        Me.LayoutControlItem7.Text = "LayoutControlItem7"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextToControlDistance = 0
        Me.LayoutControlItem7.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.dgvPermissions
        Me.LayoutControlItem6.CustomizationFormText = "Menu Permissions"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 26)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(545, 567)
        Me.LayoutControlItem6.Text = "Menu Permissions"
        Me.LayoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(84, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(424, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(139, 26)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(417, 26)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'FrmGroupPolicyManagerSimple
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 800)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "FrmGroupPolicyManagerSimple"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Group Policy Manager"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.dgvPermissions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPermissions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMenuActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvGroups, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvGroups, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGroupActive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGroupDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGroupName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents btnSaveGroup As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtGroupName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtGroupDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkGroupActive As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents dgvGroups As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvGroups As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents dgvPermissions As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvPermissions As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnSavePermissions As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents colGroupId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colGroupName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colGroupDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colGroupActive As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMenuName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMenuType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMenuActive As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chkMenuActive As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents btnDeleteGroup As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnClearGroup As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
End Class
