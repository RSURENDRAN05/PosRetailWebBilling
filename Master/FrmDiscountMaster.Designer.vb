<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDiscountMaster
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
        Me.GroupControlDiscountData = New DevExpress.XtraEditors.GroupControl()
        Me.GridControlDiscounts = New DevExpress.XtraGrid.GridControl()
        Me.GridViewDiscounts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumnId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnCreated = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupControlDiscountEntry = New DevExpress.XtraEditors.GroupControl()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnNew = New DevExpress.XtraEditors.SimpleButton()
        Me.chkStatus = New DevExpress.XtraEditors.CheckEdit()
        Me.txtDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.txtValue = New DevExpress.XtraEditors.TextEdit()
        Me.cmbType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GroupControlDiscountData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControlDiscountData.SuspendLayout()
        CType(Me.GridControlDiscounts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewDiscounts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControlDiscountEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControlDiscountEntry.SuspendLayout()
        CType(Me.chkStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControlDiscountData
        '
        Me.GroupControlDiscountData.Controls.Add(Me.GridControlDiscounts)
        Me.GroupControlDiscountData.Location = New System.Drawing.Point(12, 12)
        Me.GroupControlDiscountData.Name = "GroupControlDiscountData"
        Me.GroupControlDiscountData.Size = New System.Drawing.Size(760, 350)
        Me.GroupControlDiscountData.TabIndex = 0
        Me.GroupControlDiscountData.Text = "Discount Master Data"
        '
        'GridControlDiscounts
        '
        Me.GridControlDiscounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControlDiscounts.Location = New System.Drawing.Point(2, 23)
        Me.GridControlDiscounts.MainView = Me.GridViewDiscounts
        Me.GridControlDiscounts.Name = "GridControlDiscounts"
        Me.GridControlDiscounts.Size = New System.Drawing.Size(756, 325)
        Me.GridControlDiscounts.TabIndex = 0
        Me.GridControlDiscounts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewDiscounts})
        '
        'GridViewDiscounts
        '
        Me.GridViewDiscounts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumnId, Me.GridColumnName, Me.GridColumnType, Me.GridColumnValue, Me.GridColumnDescription, Me.GridColumnStatus, Me.GridColumnCreated})
        Me.GridViewDiscounts.GridControl = Me.GridControlDiscounts
        Me.GridViewDiscounts.Name = "GridViewDiscounts"
        Me.GridViewDiscounts.OptionsBehavior.Editable = False
        Me.GridViewDiscounts.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridViewDiscounts.OptionsView.ShowGroupPanel = False
        '
        'GridColumnId
        '
        Me.GridColumnId.FieldName = "Id"
        Me.GridColumnId.Name = "GridColumnId"
        Me.GridColumnId.Visible = False
        '
        'GridColumnName
        '
        Me.GridColumnName.Caption = "Discount Name"
        Me.GridColumnName.FieldName = "Name"
        Me.GridColumnName.Name = "GridColumnName"
        Me.GridColumnName.Visible = True
        Me.GridColumnName.VisibleIndex = 0
        Me.GridColumnName.Width = 150
        '
        'GridColumnType
        '
        Me.GridColumnType.Caption = "Type"
        Me.GridColumnType.FieldName = "Type"
        Me.GridColumnType.Name = "GridColumnType"
        Me.GridColumnType.Visible = True
        Me.GridColumnType.VisibleIndex = 1
        Me.GridColumnType.Width = 100
        '
        'GridColumnValue
        '
        Me.GridColumnValue.Caption = "Value"
        Me.GridColumnValue.FieldName = "Value"
        Me.GridColumnValue.Name = "GridColumnValue"
        Me.GridColumnValue.Visible = True
        Me.GridColumnValue.VisibleIndex = 2
        Me.GridColumnValue.Width = 80
        '
        'GridColumnDescription
        '
        Me.GridColumnDescription.Caption = "Description"
        Me.GridColumnDescription.FieldName = "Description"
        Me.GridColumnDescription.Name = "GridColumnDescription"
        Me.GridColumnDescription.Visible = True
        Me.GridColumnDescription.VisibleIndex = 3
        Me.GridColumnDescription.Width = 200
        '
        'GridColumnStatus
        '
        Me.GridColumnStatus.Caption = "Status"
        Me.GridColumnStatus.FieldName = "Status"
        Me.GridColumnStatus.Name = "GridColumnStatus"
        Me.GridColumnStatus.Visible = True
        Me.GridColumnStatus.VisibleIndex = 4
        Me.GridColumnStatus.Width = 80
        '
        'GridColumnCreated
        '
        Me.GridColumnCreated.Caption = "Created Date"
        Me.GridColumnCreated.FieldName = "Created"
        Me.GridColumnCreated.Name = "GridColumnCreated"
        Me.GridColumnCreated.Visible = True
        Me.GridColumnCreated.VisibleIndex = 5
        Me.GridColumnCreated.Width = 120
        '
        'GroupControlDiscountEntry
        '
        Me.GroupControlDiscountEntry.Controls.Add(Me.btnCancel)
        Me.GroupControlDiscountEntry.Controls.Add(Me.btnSave)
        Me.GroupControlDiscountEntry.Controls.Add(Me.btnDelete)
        Me.GroupControlDiscountEntry.Controls.Add(Me.btnEdit)
        Me.GroupControlDiscountEntry.Controls.Add(Me.btnNew)
        Me.GroupControlDiscountEntry.Controls.Add(Me.chkStatus)
        Me.GroupControlDiscountEntry.Controls.Add(Me.txtDescription)
        Me.GroupControlDiscountEntry.Controls.Add(Me.txtValue)
        Me.GroupControlDiscountEntry.Controls.Add(Me.cmbType)
        Me.GroupControlDiscountEntry.Controls.Add(Me.txtName)
        Me.GroupControlDiscountEntry.Controls.Add(Me.LabelControl5)
        Me.GroupControlDiscountEntry.Controls.Add(Me.LabelControl4)
        Me.GroupControlDiscountEntry.Controls.Add(Me.LabelControl3)
        Me.GroupControlDiscountEntry.Controls.Add(Me.LabelControl2)
        Me.GroupControlDiscountEntry.Controls.Add(Me.LabelControl1)
        Me.GroupControlDiscountEntry.Location = New System.Drawing.Point(12, 368)
        Me.GroupControlDiscountEntry.Name = "GroupControlDiscountEntry"
        Me.GroupControlDiscountEntry.Size = New System.Drawing.Size(760, 200)
        Me.GroupControlDiscountEntry.TabIndex = 1
        Me.GroupControlDiscountEntry.Text = "Discount Entry"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(665, 165)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 14
        Me.btnCancel.Text = "Cancel"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(584, 165)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 13
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(503, 165)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 12
        Me.btnDelete.Text = "Delete"
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(422, 165)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 23)
        Me.btnEdit.TabIndex = 11
        Me.btnEdit.Text = "Edit"
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(341, 165)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 23)
        Me.btnNew.TabIndex = 10
        Me.btnNew.Text = "New"
        '
        'chkStatus
        '
        Me.chkStatus.Location = New System.Drawing.Point(580, 35)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.Properties.Caption = "Active"
        Me.chkStatus.Size = New System.Drawing.Size(75, 19)
        Me.chkStatus.TabIndex = 9
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(120, 85)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(620, 70)
        Me.txtDescription.TabIndex = 8
        '
        'txtValue
        '
        Me.txtValue.Location = New System.Drawing.Point(370, 35)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(100, 20)
        Me.txtValue.TabIndex = 7
        '
        'cmbType
        '
        Me.cmbType.Location = New System.Drawing.Point(370, 60)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbType.Properties.Items.AddRange(New Object() {"percentage", "amount"})
        Me.cmbType.Size = New System.Drawing.Size(100, 20)
        Me.cmbType.TabIndex = 6
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(120, 35)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(150, 20)
        Me.txtName.TabIndex = 5
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(20, 88)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(57, 13)
        Me.LabelControl5.TabIndex = 4
        Me.LabelControl5.Text = "Description:"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(300, 63)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(28, 13)
        Me.LabelControl4.TabIndex = 3
        Me.LabelControl4.Text = "Type:"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(300, 38)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(30, 13)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "Value:"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(20, 63)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(0, 13)
        Me.LabelControl2.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(20, 38)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(75, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Discount Name:"
        '
        'FrmDiscountMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 580)
        Me.Controls.Add(Me.GroupControlDiscountEntry)
        Me.Controls.Add(Me.GroupControlDiscountData)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDiscountMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Discount Master Management"
        CType(Me.GroupControlDiscountData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControlDiscountData.ResumeLayout(False)
        CType(Me.GridControlDiscounts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewDiscounts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControlDiscountEntry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControlDiscountEntry.ResumeLayout(False)
        Me.GroupControlDiscountEntry.PerformLayout()
        CType(Me.chkStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControlDiscountData As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridControlDiscounts As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewDiscounts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumnId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnCreated As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupControlDiscountEntry As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnNew As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkStatus As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
