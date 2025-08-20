<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDiscountSelection
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
        Me.GroupControlPredefined = New DevExpress.XtraEditors.GroupControl()
        Me.GridControlDiscounts = New DevExpress.XtraGrid.GridControl()
        Me.GridViewDiscounts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumnId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupControlCustom = New DevExpress.XtraEditors.GroupControl()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnApplyCustom = New DevExpress.XtraEditors.SimpleButton()
        Me.txtCustomAmount = New DevExpress.XtraEditors.TextEdit()
        Me.txtCustomPercentage = New DevExpress.XtraEditors.TextEdit()
        Me.RadioGroupCustom = New DevExpress.XtraEditors.RadioGroup()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GroupControlPredefined, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControlPredefined.SuspendLayout()
        CType(Me.GridControlDiscounts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewDiscounts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControlCustom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControlCustom.SuspendLayout()
        CType(Me.txtCustomAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioGroupCustom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControlPredefined
        '
        Me.GroupControlPredefined.Controls.Add(Me.GridControlDiscounts)
        Me.GroupControlPredefined.Location = New System.Drawing.Point(12, 12)
        Me.GroupControlPredefined.Name = "GroupControlPredefined"
        Me.GroupControlPredefined.Size = New System.Drawing.Size(660, 300)
        Me.GroupControlPredefined.TabIndex = 0
        Me.GroupControlPredefined.Text = "Predefined Discounts"
        '
        'GridControlDiscounts
        '
        Me.GridControlDiscounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControlDiscounts.Location = New System.Drawing.Point(2, 21)
        Me.GridControlDiscounts.MainView = Me.GridViewDiscounts
        Me.GridControlDiscounts.Name = "GridControlDiscounts"
        Me.GridControlDiscounts.Size = New System.Drawing.Size(656, 277)
        Me.GridControlDiscounts.TabIndex = 0
        Me.GridControlDiscounts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewDiscounts})
        '
        'GridViewDiscounts
        '
        Me.GridViewDiscounts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumnId, Me.GridColumnName, Me.GridColumnType, Me.GridColumnValue, Me.GridColumnDescription})
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
        '
        'GridColumnName
        '
        Me.GridColumnName.Caption = "Discount Name"
        Me.GridColumnName.FieldName = "Name"
        Me.GridColumnName.Name = "GridColumnName"
        Me.GridColumnName.Visible = True
        Me.GridColumnName.VisibleIndex = 0
        Me.GridColumnName.Width = 200
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
        Me.GridColumnValue.Width = 100
        '
        'GridColumnDescription
        '
        Me.GridColumnDescription.Caption = "Description"
        Me.GridColumnDescription.FieldName = "Description"
        Me.GridColumnDescription.Name = "GridColumnDescription"
        Me.GridColumnDescription.Visible = True
        Me.GridColumnDescription.VisibleIndex = 3
        Me.GridColumnDescription.Width = 250
        '
        'GroupControlCustom
        '
        Me.GroupControlCustom.Controls.Add(Me.btnCancel)
        Me.GroupControlCustom.Controls.Add(Me.btnApplyCustom)
        Me.GroupControlCustom.Controls.Add(Me.txtCustomAmount)
        Me.GroupControlCustom.Controls.Add(Me.txtCustomPercentage)
        Me.GroupControlCustom.Controls.Add(Me.RadioGroupCustom)
        Me.GroupControlCustom.Controls.Add(Me.LabelControl1)
        Me.GroupControlCustom.Location = New System.Drawing.Point(12, 318)
        Me.GroupControlCustom.Name = "GroupControlCustom"
        Me.GroupControlCustom.Size = New System.Drawing.Size(660, 120)
        Me.GroupControlCustom.TabIndex = 1
        Me.GroupControlCustom.Text = "Custom Discount"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(548, 72)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        '
        'btnApplyCustom
        '
        Me.btnApplyCustom.Location = New System.Drawing.Point(548, 35)
        Me.btnApplyCustom.Name = "btnApplyCustom"
        Me.btnApplyCustom.Size = New System.Drawing.Size(75, 23)
        Me.btnApplyCustom.TabIndex = 4
        Me.btnApplyCustom.Text = "Apply"
        '
        'txtCustomAmount
        '
        Me.txtCustomAmount.Location = New System.Drawing.Point(400, 55)
        Me.txtCustomAmount.Name = "txtCustomAmount"
        Me.txtCustomAmount.Size = New System.Drawing.Size(100, 20)
        Me.txtCustomAmount.TabIndex = 3
        '
        'txtCustomPercentage
        '
        Me.txtCustomPercentage.Location = New System.Drawing.Point(250, 55)
        Me.txtCustomPercentage.Name = "txtCustomPercentage"
        Me.txtCustomPercentage.Size = New System.Drawing.Size(100, 20)
        Me.txtCustomPercentage.TabIndex = 2
        '
        'RadioGroupCustom
        '
        Me.RadioGroupCustom.Location = New System.Drawing.Point(140, 35)
        Me.RadioGroupCustom.Name = "RadioGroupCustom"
        Me.RadioGroupCustom.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Percentage (%)"), New DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Amount (Rs.)")})
        Me.RadioGroupCustom.Size = New System.Drawing.Size(385, 60)
        Me.RadioGroupCustom.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(15, 55)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(104, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Select Discount Type:"
        '
        'FrmDiscountSelection
        '
        Me.AcceptButton = Me.btnApplyCustom
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(684, 450)
        Me.Controls.Add(Me.GroupControlCustom)
        Me.Controls.Add(Me.GroupControlPredefined)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDiscountSelection"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Select Discount"
        CType(Me.GroupControlPredefined, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControlPredefined.ResumeLayout(False)
        CType(Me.GridControlDiscounts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewDiscounts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControlCustom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControlCustom.ResumeLayout(False)
        Me.GroupControlCustom.PerformLayout()
        CType(Me.txtCustomAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioGroupCustom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControlPredefined As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridControlDiscounts As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewDiscounts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumnId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupControlCustom As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnApplyCustom As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtCustomAmount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCustomPercentage As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RadioGroupCustom As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
