<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMenuManager
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtHeadId = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.btnAddSubMenu = New DevExpress.XtraEditors.SimpleButton()
        Me.chkSubMenuActive = New DevExpress.XtraEditors.CheckEdit()
        Me.txtSubMenuName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.txtSubMenuCode = New DevExpress.XtraEditors.TextEdit()
        Me.chkHeaderActive = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.btnAddHeader = New DevExpress.XtraEditors.SimpleButton()
        Me.txtHeaderCode = New DevExpress.XtraEditors.TextEdit()
        Me.txtHeaderName = New DevExpress.XtraEditors.TextEdit()
        Me.cmbHeaderMenu = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.dgvHeaderMenu = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.dgvSubMenu = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnDeleteSubMenu = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDeleteHeader = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtSubmenuId = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtHeadId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSubMenuActive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubMenuName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubMenuCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkHeaderActive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHeaderCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHeaderName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbHeaderMenu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvHeaderMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSubMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubmenuId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.txtSubmenuId)
        Me.GroupControl1.Controls.Add(Me.LabelControl7)
        Me.GroupControl1.Controls.Add(Me.txtHeadId)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.btnAddSubMenu)
        Me.GroupControl1.Controls.Add(Me.chkSubMenuActive)
        Me.GroupControl1.Controls.Add(Me.txtSubMenuName)
        Me.GroupControl1.Controls.Add(Me.LabelControl5)
        Me.GroupControl1.Controls.Add(Me.txtSubMenuCode)
        Me.GroupControl1.Controls.Add(Me.chkHeaderActive)
        Me.GroupControl1.Controls.Add(Me.LabelControl6)
        Me.GroupControl1.Controls.Add(Me.btnAddHeader)
        Me.GroupControl1.Controls.Add(Me.txtHeaderCode)
        Me.GroupControl1.Controls.Add(Me.txtHeaderName)
        Me.GroupControl1.Controls.Add(Me.cmbHeaderMenu)
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.LabelControl4)
        Me.GroupControl1.Location = New System.Drawing.Point(12, 12)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1180, 127)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Header Menu Management"
        '
        'txtHeadId
        '
        Me.txtHeadId.Location = New System.Drawing.Point(120, 21)
        Me.txtHeadId.Name = "txtHeadId"
        Me.txtHeadId.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.txtHeadId.Properties.Appearance.Options.UseFont = True
        Me.txtHeadId.Size = New System.Drawing.Size(89, 20)
        Me.txtHeadId.TabIndex = 14
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Location = New System.Drawing.Point(35, 24)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(68, 14)
        Me.LabelControl1.TabIndex = 13
        Me.LabelControl1.Text = "Header Id :"
        '
        'btnAddSubMenu
        '
        Me.btnAddSubMenu.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnAddSubMenu.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnAddSubMenu.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnAddSubMenu.Appearance.Options.UseBackColor = True
        Me.btnAddSubMenu.Appearance.Options.UseFont = True
        Me.btnAddSubMenu.Appearance.Options.UseForeColor = True
        Me.btnAddSubMenu.Location = New System.Drawing.Point(953, 83)
        Me.btnAddSubMenu.Name = "btnAddSubMenu"
        Me.btnAddSubMenu.Size = New System.Drawing.Size(120, 35)
        Me.btnAddSubMenu.TabIndex = 11
        Me.btnAddSubMenu.Text = "Add Sub Menu"
        '
        'chkSubMenuActive
        '
        Me.chkSubMenuActive.EditValue = True
        Me.chkSubMenuActive.Location = New System.Drawing.Point(1023, 48)
        Me.chkSubMenuActive.Name = "chkSubMenuActive"
        Me.chkSubMenuActive.Properties.Caption = "Active"
        Me.chkSubMenuActive.Size = New System.Drawing.Size(75, 19)
        Me.chkSubMenuActive.TabIndex = 12
        '
        'txtSubMenuName
        '
        Me.txtSubMenuName.Location = New System.Drawing.Point(767, 72)
        Me.txtSubMenuName.Name = "txtSubMenuName"
        Me.txtSubMenuName.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.txtSubMenuName.Properties.Appearance.Options.UseFont = True
        Me.txtSubMenuName.Size = New System.Drawing.Size(180, 20)
        Me.txtSubMenuName.TabIndex = 9
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl5.Location = New System.Drawing.Point(682, 75)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(65, 14)
        Me.LabelControl5.TabIndex = 6
        Me.LabelControl5.Text = "Sub Name:"
        '
        'txtSubMenuCode
        '
        Me.txtSubMenuCode.Location = New System.Drawing.Point(767, 98)
        Me.txtSubMenuCode.Name = "txtSubMenuCode"
        Me.txtSubMenuCode.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.txtSubMenuCode.Properties.Appearance.Options.UseFont = True
        Me.txtSubMenuCode.Size = New System.Drawing.Size(180, 20)
        Me.txtSubMenuCode.TabIndex = 10
        '
        'chkHeaderActive
        '
        Me.chkHeaderActive.EditValue = True
        Me.chkHeaderActive.Location = New System.Drawing.Point(306, 70)
        Me.chkHeaderActive.Name = "chkHeaderActive"
        Me.chkHeaderActive.Properties.Caption = "Active"
        Me.chkHeaderActive.Size = New System.Drawing.Size(75, 19)
        Me.chkHeaderActive.TabIndex = 6
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Location = New System.Drawing.Point(712, 101)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(35, 14)
        Me.LabelControl6.TabIndex = 7
        Me.LabelControl6.Text = "Code:"
        '
        'btnAddHeader
        '
        Me.btnAddHeader.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.btnAddHeader.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnAddHeader.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnAddHeader.Appearance.Options.UseBackColor = True
        Me.btnAddHeader.Appearance.Options.UseFont = True
        Me.btnAddHeader.Appearance.Options.UseForeColor = True
        Me.btnAddHeader.Location = New System.Drawing.Point(376, 24)
        Me.btnAddHeader.Name = "btnAddHeader"
        Me.btnAddHeader.Size = New System.Drawing.Size(120, 35)
        Me.btnAddHeader.TabIndex = 5
        Me.btnAddHeader.Text = "Add Header"
        '
        'txtHeaderCode
        '
        Me.txtHeaderCode.Location = New System.Drawing.Point(120, 70)
        Me.txtHeaderCode.Name = "txtHeaderCode"
        Me.txtHeaderCode.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.txtHeaderCode.Properties.Appearance.Options.UseFont = True
        Me.txtHeaderCode.Size = New System.Drawing.Size(180, 20)
        Me.txtHeaderCode.TabIndex = 4
        '
        'txtHeaderName
        '
        Me.txtHeaderName.Location = New System.Drawing.Point(120, 44)
        Me.txtHeaderName.Name = "txtHeaderName"
        Me.txtHeaderName.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.txtHeaderName.Properties.Appearance.Options.UseFont = True
        Me.txtHeaderName.Size = New System.Drawing.Size(250, 20)
        Me.txtHeaderName.TabIndex = 3
        '
        'cmbHeaderMenu
        '
        Me.cmbHeaderMenu.Location = New System.Drawing.Point(767, 46)
        Me.cmbHeaderMenu.Name = "cmbHeaderMenu"
        Me.cmbHeaderMenu.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmbHeaderMenu.Properties.Appearance.Options.UseFont = True
        Me.cmbHeaderMenu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbHeaderMenu.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("phid", "ID", 50, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ph_name", 200, "Header Menu")})
        Me.cmbHeaderMenu.Properties.NullText = "Select Header Menu..."
        Me.cmbHeaderMenu.Size = New System.Drawing.Size(250, 20)
        Me.cmbHeaderMenu.TabIndex = 8
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl3.Location = New System.Drawing.Point(64, 73)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(39, 14)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "Code :"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl2.Location = New System.Drawing.Point(15, 47)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(88, 14)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Header Name :"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl4.Location = New System.Drawing.Point(662, 49)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(85, 14)
        Me.LabelControl4.TabIndex = 5
        Me.LabelControl4.Text = "Header Menu:"
        '
        'dgvHeaderMenu
        '
        Me.dgvHeaderMenu.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvHeaderMenu.Location = New System.Drawing.Point(12, 159)
        Me.dgvHeaderMenu.MainView = Me.GridView1
        Me.dgvHeaderMenu.Name = "dgvHeaderMenu"
        Me.dgvHeaderMenu.Size = New System.Drawing.Size(588, 432)
        Me.dgvHeaderMenu.TabIndex = 2
        Me.dgvHeaderMenu.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.dgvHeaderMenu
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsSelection.MultiSelect = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'dgvSubMenu
        '
        Me.dgvSubMenu.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvSubMenu.Location = New System.Drawing.Point(604, 159)
        Me.dgvSubMenu.MainView = Me.GridView2
        Me.dgvSubMenu.Name = "dgvSubMenu"
        Me.dgvSubMenu.Size = New System.Drawing.Size(588, 432)
        Me.dgvSubMenu.TabIndex = 3
        Me.dgvSubMenu.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.dgvSubMenu
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.Editable = False
        Me.GridView2.OptionsBehavior.ReadOnly = True
        Me.GridView2.OptionsSelection.MultiSelect = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl8.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.LabelControl8.Location = New System.Drawing.Point(12, 269)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(113, 16)
        Me.LabelControl8.TabIndex = 4
        Me.LabelControl8.Text = "Header Menu List"
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl9.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.LabelControl9.Location = New System.Drawing.Point(612, 269)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(90, 16)
        Me.LabelControl9.TabIndex = 5
        Me.LabelControl9.Text = "Sub Menu List"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRefresh.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnRefresh.Appearance.Options.UseBackColor = True
        Me.btnRefresh.Appearance.Options.UseFont = True
        Me.btnRefresh.Appearance.Options.UseForeColor = True
        Me.btnRefresh.Location = New System.Drawing.Point(3, 3)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 35)
        Me.btnRefresh.TabIndex = 6
        Me.btnRefresh.Text = "Refresh All"
        '
        'PanelControl1
        '
        Me.PanelControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.PanelControl1.Appearance.Options.UseBackColor = True
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.Controls.Add(Me.btnDeleteSubMenu)
        Me.PanelControl1.Controls.Add(Me.btnDeleteHeader)
        Me.PanelControl1.Controls.Add(Me.btnRefresh)
        Me.PanelControl1.Controls.Add(Me.LabelControl9)
        Me.PanelControl1.Controls.Add(Me.LabelControl8)
        Me.PanelControl1.Location = New System.Drawing.Point(12, 595)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1180, 44)
        Me.PanelControl1.TabIndex = 7
        '
        'btnDeleteSubMenu
        '
        Me.btnDeleteSubMenu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteSubMenu.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDeleteSubMenu.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnDeleteSubMenu.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnDeleteSubMenu.Appearance.Options.UseBackColor = True
        Me.btnDeleteSubMenu.Appearance.Options.UseFont = True
        Me.btnDeleteSubMenu.Appearance.Options.UseForeColor = True
        Me.btnDeleteSubMenu.Location = New System.Drawing.Point(1033, 3)
        Me.btnDeleteSubMenu.Name = "btnDeleteSubMenu"
        Me.btnDeleteSubMenu.Size = New System.Drawing.Size(144, 35)
        Me.btnDeleteSubMenu.TabIndex = 8
        Me.btnDeleteSubMenu.Text = "Delete Sub Menu"
        '
        'btnDeleteHeader
        '
        Me.btnDeleteHeader.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteHeader.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDeleteHeader.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnDeleteHeader.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnDeleteHeader.Appearance.Options.UseBackColor = True
        Me.btnDeleteHeader.Appearance.Options.UseFont = True
        Me.btnDeleteHeader.Appearance.Options.UseForeColor = True
        Me.btnDeleteHeader.Location = New System.Drawing.Point(109, 3)
        Me.btnDeleteHeader.Name = "btnDeleteHeader"
        Me.btnDeleteHeader.Size = New System.Drawing.Size(144, 35)
        Me.btnDeleteHeader.TabIndex = 7
        Me.btnDeleteHeader.Text = "Delete Header Menu"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.PanelControl1)
        Me.LayoutControl1.Controls.Add(Me.GroupControl1)
        Me.LayoutControl1.Controls.Add(Me.dgvHeaderMenu)
        Me.LayoutControl1.Controls.Add(Me.dgvSubMenu)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1204, 651)
        Me.LayoutControl1.TabIndex = 8
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1204, 651)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.GroupControl1
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1184, 131)
        Me.LayoutControlItem1.Text = "LayoutControlItem1"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.dgvHeaderMenu
        Me.LayoutControlItem3.CustomizationFormText = "Main Menu"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 131)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(592, 452)
        Me.LayoutControlItem3.Text = "Main Menu"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(51, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.dgvSubMenu
        Me.LayoutControlItem4.CustomizationFormText = "Sub Menu"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(592, 131)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(592, 452)
        Me.LayoutControlItem4.Text = "Sub Menu"
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(51, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.PanelControl1
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 583)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(1184, 48)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'txtSubmenuId
        '
        Me.txtSubmenuId.Location = New System.Drawing.Point(767, 21)
        Me.txtSubmenuId.Name = "txtSubmenuId"
        Me.txtSubmenuId.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.txtSubmenuId.Properties.Appearance.Options.UseFont = True
        Me.txtSubmenuId.Size = New System.Drawing.Size(180, 20)
        Me.txtSubmenuId.TabIndex = 16
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl7.Location = New System.Drawing.Point(664, 24)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(83, 14)
        Me.LabelControl7.TabIndex = 15
        Me.LabelControl7.Text = "Sub Menu Id:"
        '
        'FrmMenuManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1204, 651)
        Me.Controls.Add(Me.LayoutControl1)
        Me.MinimumSize = New System.Drawing.Size(1220, 690)
        Me.Name = "FrmMenuManager"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Menu Manager - Header & Sub Menu Management"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtHeadId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSubMenuActive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubMenuName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubMenuCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkHeaderActive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHeaderCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHeaderName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbHeaderMenu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvHeaderMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSubMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubmenuId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkHeaderActive As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnAddHeader As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtHeaderCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtHeaderName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkSubMenuActive As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnAddSubMenu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtSubMenuCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSubMenuName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbHeaderMenu As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dgvHeaderMenu As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dgvSubMenu As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnDeleteSubMenu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteHeader As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtHeadId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSubmenuId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
End Class
