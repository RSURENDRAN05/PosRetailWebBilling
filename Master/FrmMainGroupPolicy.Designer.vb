<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMainGroupPolicy
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMainGroupPolicy))
        Me.layoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.btnUpdateStatus = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRemoveFromPolicy = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddToPolicy = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClear = New DevExpress.XtraEditors.SimpleButton()
        Me.txtMainName = New DevExpress.XtraEditors.TextEdit()
        Me.dgvMainGroups = New DevExpress.XtraGrid.GridControl()
        Me.gvMainGroups = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.dgvPolicies = New DevExpress.XtraGrid.GridControl()
        Me.gvPolicies = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.layoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.layoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.SimpleSeparator1 = New DevExpress.XtraLayout.SimpleSeparator()
        Me.layoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        CType(Me.layoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl1.SuspendLayout()
        CType(Me.txtMainName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvMainGroups, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMainGroups, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPolicies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPolicies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleSeparator1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl1
        '
        Me.layoutControl1.Controls.Add(Me.btnUpdateStatus)
        Me.layoutControl1.Controls.Add(Me.btnRemoveFromPolicy)
        Me.layoutControl1.Controls.Add(Me.btnAddToPolicy)
        Me.layoutControl1.Controls.Add(Me.btnRefresh)
        Me.layoutControl1.Controls.Add(Me.btnClear)
        Me.layoutControl1.Controls.Add(Me.txtMainName)
        Me.layoutControl1.Controls.Add(Me.dgvMainGroups)
        Me.layoutControl1.Controls.Add(Me.dgvPolicies)
        Me.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl1.Name = "layoutControl1"
        Me.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2640, 502, 250, 350)
        Me.layoutControl1.Root = Me.layoutControlGroup1
        Me.layoutControl1.Size = New System.Drawing.Size(1200, 700)
        Me.layoutControl1.TabIndex = 0
        Me.layoutControl1.Text = "layoutControl1"
        '
        'btnUpdateStatus
        '
        Me.btnUpdateStatus.Image = CType(resources.GetObject("btnUpdateStatus.Image"), System.Drawing.Image)
        Me.btnUpdateStatus.Location = New System.Drawing.Point(438, 36)
        Me.btnUpdateStatus.Name = "btnUpdateStatus"
        Me.btnUpdateStatus.Size = New System.Drawing.Size(119, 38)
        Me.btnUpdateStatus.StyleController = Me.layoutControl1
        Me.btnUpdateStatus.TabIndex = 13
        Me.btnUpdateStatus.Text = "Update Status"
        '
        'btnRemoveFromPolicy
        '
        Me.btnRemoveFromPolicy.Image = CType(resources.GetObject("btnRemoveFromPolicy.Image"), System.Drawing.Image)
        Me.btnRemoveFromPolicy.Location = New System.Drawing.Point(561, 36)
        Me.btnRemoveFromPolicy.Name = "btnRemoveFromPolicy"
        Me.btnRemoveFromPolicy.Size = New System.Drawing.Size(104, 38)
        Me.btnRemoveFromPolicy.StyleController = Me.layoutControl1
        Me.btnRemoveFromPolicy.TabIndex = 12
        Me.btnRemoveFromPolicy.Text = "Remove"
        '
        'btnAddToPolicy
        '
        Me.btnAddToPolicy.Image = CType(resources.GetObject("btnAddToPolicy.Image"), System.Drawing.Image)
        Me.btnAddToPolicy.Location = New System.Drawing.Point(12, 36)
        Me.btnAddToPolicy.Name = "btnAddToPolicy"
        Me.btnAddToPolicy.Size = New System.Drawing.Size(157, 38)
        Me.btnAddToPolicy.StyleController = Me.layoutControl1
        Me.btnAddToPolicy.TabIndex = 11
        Me.btnAddToPolicy.Text = "Save Main Group Policy"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.Location = New System.Drawing.Point(173, 36)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(133, 38)
        Me.btnRefresh.StyleController = Me.layoutControl1
        Me.btnRefresh.TabIndex = 14
        Me.btnRefresh.Text = "Refresh"
        '
        'btnClear
        '
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(310, 36)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(124, 38)
        Me.btnClear.StyleController = Me.layoutControl1
        Me.btnClear.TabIndex = 12
        Me.btnClear.Text = "Clear"
        '
        'txtMainName
        '
        Me.txtMainName.Location = New System.Drawing.Point(102, 12)
        Me.txtMainName.Name = "txtMainName"
        Me.txtMainName.Size = New System.Drawing.Size(332, 20)
        Me.txtMainName.StyleController = Me.layoutControl1
        Me.txtMainName.TabIndex = 8
        '
        'dgvMainGroups
        '
        Me.dgvMainGroups.Location = New System.Drawing.Point(12, 96)
        Me.dgvMainGroups.MainView = Me.gvMainGroups
        Me.dgvMainGroups.Name = "dgvMainGroups"
        Me.dgvMainGroups.Size = New System.Drawing.Size(423, 592)
        Me.dgvMainGroups.TabIndex = 4
        Me.dgvMainGroups.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvMainGroups})
        '
        'gvMainGroups
        '
        Me.gvMainGroups.GridControl = Me.dgvMainGroups
        Me.gvMainGroups.Name = "gvMainGroups"
        Me.gvMainGroups.OptionsBehavior.Editable = False
        Me.gvMainGroups.OptionsView.ShowAutoFilterRow = True
        Me.gvMainGroups.OptionsView.ShowGroupPanel = False
        '
        'dgvPolicies
        '
        Me.dgvPolicies.Location = New System.Drawing.Point(439, 105)
        Me.dgvPolicies.MainView = Me.gvPolicies
        Me.dgvPolicies.Name = "dgvPolicies"
        Me.dgvPolicies.Size = New System.Drawing.Size(749, 583)
        Me.dgvPolicies.TabIndex = 16
        Me.dgvPolicies.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvPolicies})
        '
        'gvPolicies
        '
        Me.gvPolicies.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gvPolicies.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvPolicies.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvPolicies.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvPolicies.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gvPolicies.Appearance.Row.Options.UseFont = True
        Me.gvPolicies.Appearance.Row.Options.UseTextOptions = True
        Me.gvPolicies.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvPolicies.GridControl = Me.dgvPolicies
        Me.gvPolicies.GroupPanelText = "Main Group Policy List"
        Me.gvPolicies.Name = "gvPolicies"
        Me.gvPolicies.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvPolicies.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvPolicies.OptionsBehavior.Editable = False
        Me.gvPolicies.OptionsBehavior.ReadOnly = True
        Me.gvPolicies.OptionsCustomization.AllowColumnMoving = False
        Me.gvPolicies.OptionsView.ShowGroupPanel = False
        Me.gvPolicies.RowHeight = 25
        '
        'layoutControlGroup1
        '
        Me.layoutControlGroup1.CustomizationFormText = "Root"
        Me.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.layoutControlGroup1.GroupBordersVisible = False
        Me.layoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.layoutControlItem9, Me.layoutControlItem7, Me.layoutControlItem1, Me.layoutControlItem13, Me.layoutControlItem11, Me.layoutControlItem5, Me.EmptySpaceItem2, Me.SimpleSeparator1, Me.layoutControlItem15, Me.layoutControlItem14, Me.EmptySpaceItem1})
        Me.layoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.layoutControlGroup1.Name = "Root"
        Me.layoutControlGroup1.Size = New System.Drawing.Size(1200, 700)
        Me.layoutControlGroup1.Text = "Root"
        Me.layoutControlGroup1.TextVisible = False
        '
        'layoutControlItem9
        '
        Me.layoutControlItem9.Control = Me.btnClear
        Me.layoutControlItem9.CustomizationFormText = "layoutControlItem9"
        Me.layoutControlItem9.Location = New System.Drawing.Point(298, 24)
        Me.layoutControlItem9.Name = "layoutControlItem9"
        Me.layoutControlItem9.Size = New System.Drawing.Size(128, 42)
        Me.layoutControlItem9.Text = "layoutControlItem9"
        Me.layoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutControlItem9.TextToControlDistance = 0
        Me.layoutControlItem9.TextVisible = False
        '
        'layoutControlItem7
        '
        Me.layoutControlItem7.Control = Me.dgvPolicies
        Me.layoutControlItem7.CustomizationFormText = "Main Group Policies"
        Me.layoutControlItem7.Location = New System.Drawing.Point(427, 68)
        Me.layoutControlItem7.Name = "layoutControlItem7"
        Me.layoutControlItem7.Size = New System.Drawing.Size(753, 612)
        Me.layoutControlItem7.Text = "Main Group Policies"
        Me.layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top
        Me.layoutControlItem7.TextSize = New System.Drawing.Size(85, 20)
        Me.layoutControlItem7.TextToControlDistance = 5
        '
        'layoutControlItem1
        '
        Me.layoutControlItem1.Control = Me.dgvMainGroups
        Me.layoutControlItem1.CustomizationFormText = "layoutControlItem1"
        Me.layoutControlItem1.Location = New System.Drawing.Point(0, 68)
        Me.layoutControlItem1.Name = "layoutControlItem1"
        Me.layoutControlItem1.Size = New System.Drawing.Size(427, 612)
        Me.layoutControlItem1.Text = "Main Group"
        Me.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.layoutControlItem1.TextSize = New System.Drawing.Size(54, 13)
        '
        'layoutControlItem13
        '
        Me.layoutControlItem13.Control = Me.btnAddToPolicy
        Me.layoutControlItem13.CustomizationFormText = "layoutControlItem13"
        Me.layoutControlItem13.Location = New System.Drawing.Point(0, 24)
        Me.layoutControlItem13.Name = "layoutControlItem13"
        Me.layoutControlItem13.Size = New System.Drawing.Size(161, 42)
        Me.layoutControlItem13.Text = "layoutControlItem13"
        Me.layoutControlItem13.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutControlItem13.TextToControlDistance = 0
        Me.layoutControlItem13.TextVisible = False
        '
        'layoutControlItem11
        '
        Me.layoutControlItem11.Control = Me.btnRefresh
        Me.layoutControlItem11.CustomizationFormText = "layoutControlItem11"
        Me.layoutControlItem11.Location = New System.Drawing.Point(161, 24)
        Me.layoutControlItem11.Name = "layoutControlItem11"
        Me.layoutControlItem11.Size = New System.Drawing.Size(137, 42)
        Me.layoutControlItem11.Text = "layoutControlItem11"
        Me.layoutControlItem11.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutControlItem11.TextToControlDistance = 0
        Me.layoutControlItem11.TextVisible = False
        '
        'layoutControlItem5
        '
        Me.layoutControlItem5.Control = Me.txtMainName
        Me.layoutControlItem5.CustomizationFormText = "Main Group Name:"
        Me.layoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.layoutControlItem5.Name = "layoutControlItem5"
        Me.layoutControlItem5.Size = New System.Drawing.Size(426, 24)
        Me.layoutControlItem5.Text = "Main Group Name:"
        Me.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.layoutControlItem5.TextSize = New System.Drawing.Size(85, 20)
        Me.layoutControlItem5.TextToControlDistance = 5
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(426, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(754, 24)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'SimpleSeparator1
        '
        Me.SimpleSeparator1.AllowHotTrack = False
        Me.SimpleSeparator1.CustomizationFormText = "SimpleSeparator1"
        Me.SimpleSeparator1.Location = New System.Drawing.Point(0, 66)
        Me.SimpleSeparator1.Name = "SimpleSeparator1"
        Me.SimpleSeparator1.Size = New System.Drawing.Size(1180, 2)
        Me.SimpleSeparator1.Text = "SimpleSeparator1"
        '
        'layoutControlItem15
        '
        Me.layoutControlItem15.Control = Me.btnUpdateStatus
        Me.layoutControlItem15.CustomizationFormText = "layoutControlItem15"
        Me.layoutControlItem15.Location = New System.Drawing.Point(426, 24)
        Me.layoutControlItem15.Name = "layoutControlItem15"
        Me.layoutControlItem15.Size = New System.Drawing.Size(123, 42)
        Me.layoutControlItem15.Text = "layoutControlItem15"
        Me.layoutControlItem15.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutControlItem15.TextToControlDistance = 0
        Me.layoutControlItem15.TextVisible = False
        '
        'layoutControlItem14
        '
        Me.layoutControlItem14.Control = Me.btnRemoveFromPolicy
        Me.layoutControlItem14.CustomizationFormText = "layoutControlItem14"
        Me.layoutControlItem14.Location = New System.Drawing.Point(549, 24)
        Me.layoutControlItem14.Name = "layoutControlItem14"
        Me.layoutControlItem14.Size = New System.Drawing.Size(108, 42)
        Me.layoutControlItem14.Text = "layoutControlItem14"
        Me.layoutControlItem14.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutControlItem14.TextToControlDistance = 0
        Me.layoutControlItem14.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(657, 24)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(523, 42)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'FrmMainGroupPolicy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 700)
        Me.Controls.Add(Me.layoutControl1)
        Me.Name = "FrmMainGroupPolicy"
        Me.Text = "Main Group Policy Management"
        CType(Me.layoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl1.ResumeLayout(False)
        CType(Me.txtMainName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvMainGroups, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMainGroups, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvPolicies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPolicies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleSeparator1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents layoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnClear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAddToPolicy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnRemoveFromPolicy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnUpdateStatus As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtMainName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dgvMainGroups As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvMainGroups As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dgvPolicies As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvPolicies As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents layoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents layoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents SimpleSeparator1 As DevExpress.XtraLayout.SimpleSeparator
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
End Class
