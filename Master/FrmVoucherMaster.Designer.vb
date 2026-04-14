<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVoucherMaster
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
        Me.GridControlVM = New DevExpress.XtraGrid.GridControl()
        Me.GridViewVM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colVmId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVmPrefix = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVmBookNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVmStartNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVmEndNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVmActive = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVmUsedCount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVmCreatedDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtId = New DevExpress.XtraEditors.TextEdit()
        Me.txtPrefix = New DevExpress.XtraEditors.TextEdit()
        Me.spinBookNo = New DevExpress.XtraEditors.SpinEdit()
        Me.spinStartNo = New DevExpress.XtraEditors.SpinEdit()
        Me.spinEndNo = New DevExpress.XtraEditors.SpinEdit()
        Me.chkActive = New DevExpress.XtraEditors.CheckEdit()
        Me.btnNew = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lciGrid = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciPrefix = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciBookNo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciStartNo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciEndNo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciActive = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciBtnNew = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciBtnSave = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciBtnDelete = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lciBtnCancel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.GridControlVM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewVM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrefix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spinBookNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spinStartNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spinEndNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciPrefix, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciBookNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciStartNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lciEndNo, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.LayoutControl1.Controls.Add(Me.GridControlVM)
        Me.LayoutControl1.Controls.Add(Me.txtId)
        Me.LayoutControl1.Controls.Add(Me.txtPrefix)
        Me.LayoutControl1.Controls.Add(Me.spinBookNo)
        Me.LayoutControl1.Controls.Add(Me.spinStartNo)
        Me.LayoutControl1.Controls.Add(Me.spinEndNo)
        Me.LayoutControl1.Controls.Add(Me.chkActive)
        Me.LayoutControl1.Controls.Add(Me.btnNew)
        Me.LayoutControl1.Controls.Add(Me.btnSave)
        Me.LayoutControl1.Controls.Add(Me.btnDelete)
        Me.LayoutControl1.Controls.Add(Me.btnCancel)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1000, 640)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'GridControlVM
        '
        Me.GridControlVM.Location = New System.Drawing.Point(12, 12)
        Me.GridControlVM.MainView = Me.GridViewVM
        Me.GridControlVM.Name = "GridControlVM"
        Me.GridControlVM.Size = New System.Drawing.Size(620, 616)
        Me.GridControlVM.TabIndex = 0
        Me.GridControlVM.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewVM})
        '
        'GridViewVM
        '
        Me.GridViewVM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {
            Me.colVmId, Me.colVmPrefix, Me.colVmBookNo,
            Me.colVmStartNo, Me.colVmEndNo, Me.colVmActive,
            Me.colVmUsedCount, Me.colVmCreatedDate})
        Me.GridViewVM.GridControl = Me.GridControlVM
        Me.GridViewVM.Name = "GridViewVM"
        Me.GridViewVM.OptionsBehavior.ReadOnly = True
        Me.GridViewVM.OptionsView.ShowGroupPanel = False
        Me.GridViewVM.OptionsView.ColumnAutoWidth = True
        '
        'colVmId
        '
        Me.colVmId.Caption = "ID"
        Me.colVmId.FieldName = "Id"
        Me.colVmId.Name = "colVmId"
        Me.colVmId.Visible = True
        Me.colVmId.VisibleIndex = 0
        Me.colVmId.Width = 50
        Me.colVmId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colVmId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '
        'colVmPrefix
        '
        Me.colVmPrefix.Caption = "Prefix"
        Me.colVmPrefix.FieldName = "Prefix"
        Me.colVmPrefix.Name = "colVmPrefix"
        Me.colVmPrefix.Visible = True
        Me.colVmPrefix.VisibleIndex = 1
        Me.colVmPrefix.Width = 70
        Me.colVmPrefix.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colVmPrefix.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '
        'colVmBookNo
        '
        Me.colVmBookNo.Caption = "Book No"
        Me.colVmBookNo.FieldName = "BookNo"
        Me.colVmBookNo.Name = "colVmBookNo"
        Me.colVmBookNo.Visible = True
        Me.colVmBookNo.VisibleIndex = 2
        Me.colVmBookNo.Width = 70
        Me.colVmBookNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colVmBookNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '
        'colVmStartNo
        '
        Me.colVmStartNo.Caption = "Start No"
        Me.colVmStartNo.FieldName = "StartNo"
        Me.colVmStartNo.Name = "colVmStartNo"
        Me.colVmStartNo.Visible = True
        Me.colVmStartNo.VisibleIndex = 3
        Me.colVmStartNo.Width = 70
        Me.colVmStartNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colVmStartNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '
        'colVmEndNo
        '
        Me.colVmEndNo.Caption = "End No"
        Me.colVmEndNo.FieldName = "EndNo"
        Me.colVmEndNo.Name = "colVmEndNo"
        Me.colVmEndNo.Visible = True
        Me.colVmEndNo.VisibleIndex = 4
        Me.colVmEndNo.Width = 70
        Me.colVmEndNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colVmEndNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '
        'colVmActive
        '
        Me.colVmActive.Caption = "Active"
        Me.colVmActive.FieldName = "Active"
        Me.colVmActive.Name = "colVmActive"
        Me.colVmActive.Visible = True
        Me.colVmActive.VisibleIndex = 5
        Me.colVmActive.Width = 60
        Me.colVmActive.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colVmActive.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '
        'colVmUsedCount
        '
        Me.colVmUsedCount.Caption = "Used"
        Me.colVmUsedCount.FieldName = "UsedCount"
        Me.colVmUsedCount.Name = "colVmUsedCount"
        Me.colVmUsedCount.Visible = True
        Me.colVmUsedCount.VisibleIndex = 6
        Me.colVmUsedCount.Width = 55
        Me.colVmUsedCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colVmUsedCount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '
        'colVmCreatedDate
        '
        Me.colVmCreatedDate.Caption = "Created Date"
        Me.colVmCreatedDate.FieldName = "CreatedDate"
        Me.colVmCreatedDate.Name = "colVmCreatedDate"
        Me.colVmCreatedDate.Visible = True
        Me.colVmCreatedDate.VisibleIndex = 7
        Me.colVmCreatedDate.Width = 120
        Me.colVmCreatedDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colVmCreatedDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(742, 36)
        Me.txtId.Name = "txtId"
        Me.txtId.Properties.ReadOnly = True
        Me.txtId.Size = New System.Drawing.Size(246, 20)
        Me.txtId.TabIndex = 1
        '
        'txtPrefix
        '
        Me.txtPrefix.Location = New System.Drawing.Point(742, 70)
        Me.txtPrefix.Name = "txtPrefix"
        Me.txtPrefix.Size = New System.Drawing.Size(246, 20)
        Me.txtPrefix.TabIndex = 2
        '
        'spinBookNo
        '
        Me.spinBookNo.Location = New System.Drawing.Point(742, 104)
        Me.spinBookNo.Name = "spinBookNo"
        Me.spinBookNo.Properties.Increment = New Decimal(New Integer() {1, 0, 0, 0})
        Me.spinBookNo.Properties.MaxValue = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.spinBookNo.Properties.MinValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.spinBookNo.Size = New System.Drawing.Size(246, 20)
        Me.spinBookNo.TabIndex = 3
        '
        'spinStartNo
        '
        Me.spinStartNo.Location = New System.Drawing.Point(742, 138)
        Me.spinStartNo.Name = "spinStartNo"
        Me.spinStartNo.Properties.Increment = New Decimal(New Integer() {1, 0, 0, 0})
        Me.spinStartNo.Properties.MaxValue = New Decimal(New Integer() {999999, 0, 0, 0})
        Me.spinStartNo.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.spinStartNo.Size = New System.Drawing.Size(246, 20)
        Me.spinStartNo.TabIndex = 4
        '
        'spinEndNo
        '
        Me.spinEndNo.Location = New System.Drawing.Point(742, 172)
        Me.spinEndNo.Name = "spinEndNo"
        Me.spinEndNo.Properties.Increment = New Decimal(New Integer() {1, 0, 0, 0})
        Me.spinEndNo.Properties.MaxValue = New Decimal(New Integer() {999999, 0, 0, 0})
        Me.spinEndNo.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.spinEndNo.Size = New System.Drawing.Size(246, 20)
        Me.spinEndNo.TabIndex = 5
        '
        'chkActive
        '
        Me.chkActive.Location = New System.Drawing.Point(742, 206)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Properties.Caption = "Active"
        Me.chkActive.Size = New System.Drawing.Size(246, 19)
        Me.chkActive.TabIndex = 6
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(648, 580)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(80, 36)
        Me.btnNew.TabIndex = 7
        Me.btnNew.Text = "New"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(740, 580)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 36)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(832, 580)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 36)
        Me.btnDelete.TabIndex = 9
        Me.btnDelete.Text = "Delete"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(924, 580)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(76, 36)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {
            Me.lciGrid, Me.lciId, Me.lciPrefix, Me.lciBookNo,
            Me.lciStartNo, Me.lciEndNo, Me.lciActive,
            Me.lciBtnNew, Me.lciBtnSave, Me.lciBtnDelete, Me.lciBtnCancel,
            Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1000, 640)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lciGrid
        '
        Me.lciGrid.Control = Me.GridControlVM
        Me.lciGrid.Location = New System.Drawing.Point(0, 0)
        Me.lciGrid.Name = "lciGrid"
        Me.lciGrid.Size = New System.Drawing.Size(640, 620)
        Me.lciGrid.TextSize = New System.Drawing.Size(0, 0)
        Me.lciGrid.TextVisible = False
        '
        'lciId
        '
        Me.lciId.Control = Me.txtId
        Me.lciId.Location = New System.Drawing.Point(640, 0)
        Me.lciId.Name = "lciId"
        Me.lciId.Size = New System.Drawing.Size(360, 34)
        Me.lciId.Text = "ID"
        Me.lciId.TextSize = New System.Drawing.Size(80, 13)
        '
        'lciPrefix
        '
        Me.lciPrefix.Control = Me.txtPrefix
        Me.lciPrefix.Location = New System.Drawing.Point(640, 34)
        Me.lciPrefix.Name = "lciPrefix"
        Me.lciPrefix.Size = New System.Drawing.Size(360, 34)
        Me.lciPrefix.Text = "Voucher Prefix"
        Me.lciPrefix.TextSize = New System.Drawing.Size(80, 13)
        '
        'lciBookNo
        '
        Me.lciBookNo.Control = Me.spinBookNo
        Me.lciBookNo.Location = New System.Drawing.Point(640, 68)
        Me.lciBookNo.Name = "lciBookNo"
        Me.lciBookNo.Size = New System.Drawing.Size(360, 34)
        Me.lciBookNo.Text = "Book No"
        Me.lciBookNo.TextSize = New System.Drawing.Size(80, 13)
        '
        'lciStartNo
        '
        Me.lciStartNo.Control = Me.spinStartNo
        Me.lciStartNo.Location = New System.Drawing.Point(640, 102)
        Me.lciStartNo.Name = "lciStartNo"
        Me.lciStartNo.Size = New System.Drawing.Size(360, 34)
        Me.lciStartNo.Text = "Start No"
        Me.lciStartNo.TextSize = New System.Drawing.Size(80, 13)
        '
        'lciEndNo
        '
        Me.lciEndNo.Control = Me.spinEndNo
        Me.lciEndNo.Location = New System.Drawing.Point(640, 136)
        Me.lciEndNo.Name = "lciEndNo"
        Me.lciEndNo.Size = New System.Drawing.Size(360, 34)
        Me.lciEndNo.Text = "End No"
        Me.lciEndNo.TextSize = New System.Drawing.Size(80, 13)
        '
        'lciActive
        '
        Me.lciActive.Control = Me.chkActive
        Me.lciActive.Location = New System.Drawing.Point(640, 170)
        Me.lciActive.Name = "lciActive"
        Me.lciActive.Size = New System.Drawing.Size(360, 34)
        Me.lciActive.Text = "Status"
        Me.lciActive.TextSize = New System.Drawing.Size(80, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(640, 204)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(360, 370)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'lciBtnNew
        '
        Me.lciBtnNew.Control = Me.btnNew
        Me.lciBtnNew.Location = New System.Drawing.Point(640, 574)
        Me.lciBtnNew.Name = "lciBtnNew"
        Me.lciBtnNew.Size = New System.Drawing.Size(88, 46)
        Me.lciBtnNew.TextSize = New System.Drawing.Size(0, 0)
        Me.lciBtnNew.TextVisible = False
        '
        'lciBtnSave
        '
        Me.lciBtnSave.Control = Me.btnSave
        Me.lciBtnSave.Location = New System.Drawing.Point(728, 574)
        Me.lciBtnSave.Name = "lciBtnSave"
        Me.lciBtnSave.Size = New System.Drawing.Size(88, 46)
        Me.lciBtnSave.TextSize = New System.Drawing.Size(0, 0)
        Me.lciBtnSave.TextVisible = False
        '
        'lciBtnDelete
        '
        Me.lciBtnDelete.Control = Me.btnDelete
        Me.lciBtnDelete.Location = New System.Drawing.Point(816, 574)
        Me.lciBtnDelete.Name = "lciBtnDelete"
        Me.lciBtnDelete.Size = New System.Drawing.Size(88, 46)
        Me.lciBtnDelete.TextSize = New System.Drawing.Size(0, 0)
        Me.lciBtnDelete.TextVisible = False
        '
        'lciBtnCancel
        '
        Me.lciBtnCancel.Control = Me.btnCancel
        Me.lciBtnCancel.Location = New System.Drawing.Point(904, 574)
        Me.lciBtnCancel.Name = "lciBtnCancel"
        Me.lciBtnCancel.Size = New System.Drawing.Size(96, 46)
        Me.lciBtnCancel.TextSize = New System.Drawing.Size(0, 0)
        Me.lciBtnCancel.TextVisible = False
        '
        'FrmVoucherMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1000, 640)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "FrmVoucherMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Voucher Master"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.GridControlVM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewVM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrefix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spinBookNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spinStartNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spinEndNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciPrefix, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciBookNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciStartNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciEndNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciBtnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciBtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciBtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lciBtnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents GridControlVM As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewVM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colVmId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVmPrefix As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVmBookNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVmStartNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVmEndNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVmActive As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVmUsedCount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVmCreatedDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPrefix As DevExpress.XtraEditors.TextEdit
    Friend WithEvents spinBookNo As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents spinStartNo As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents spinEndNo As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents chkActive As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnNew As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lciGrid As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciPrefix As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciBookNo As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciStartNo As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciEndNo As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciActive As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciBtnNew As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciBtnSave As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciBtnDelete As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lciBtnCancel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
End Class
