<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSalesmanList
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSalesmanList))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.GridControlSalesman = New DevExpress.XtraGrid.GridControl()
        Me.GridViewSalesman = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumnEmpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnSalesmanName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemButtonEditSelect = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GridControlSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonEditSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridControlSalesman
        '
        Me.GridControlSalesman.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControlSalesman.Location = New System.Drawing.Point(0, 50)
        Me.GridControlSalesman.MainView = Me.GridViewSalesman
        Me.GridControlSalesman.Name = "GridControlSalesman"
        Me.GridControlSalesman.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemButtonEditSelect})
        Me.GridControlSalesman.Size = New System.Drawing.Size(411, 439)
        Me.GridControlSalesman.TabIndex = 0
        Me.GridControlSalesman.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewSalesman})
        '
        'GridViewSalesman
        '
        Me.GridViewSalesman.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridViewSalesman.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewSalesman.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridViewSalesman.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewSalesman.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridViewSalesman.Appearance.Row.Options.UseFont = True
        Me.GridViewSalesman.Appearance.Row.Options.UseTextOptions = True
        Me.GridViewSalesman.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewSalesman.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumnEmpId, Me.GridColumnSalesmanName, Me.GridColumn1})
        Me.GridViewSalesman.GridControl = Me.GridControlSalesman
        Me.GridViewSalesman.Name = "GridViewSalesman"
        Me.GridViewSalesman.OptionsBehavior.Editable = False
        Me.GridViewSalesman.OptionsBehavior.ReadOnly = True
        Me.GridViewSalesman.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridViewSalesman.OptionsView.ShowGroupPanel = False
        Me.GridViewSalesman.RowHeight = 30
        '
        'GridColumnEmpId
        '
        Me.GridColumnEmpId.Caption = "Emp ID"
        Me.GridColumnEmpId.FieldName = "Id"
        Me.GridColumnEmpId.Name = "GridColumnEmpId"
        Me.GridColumnEmpId.Visible = True
        Me.GridColumnEmpId.VisibleIndex = 0
        Me.GridColumnEmpId.Width = 93
        '
        'GridColumnSalesmanName
        '
        Me.GridColumnSalesmanName.Caption = "Salesman Name"
        Me.GridColumnSalesmanName.FieldName = "SalesMan"
        Me.GridColumnSalesmanName.Name = "GridColumnSalesmanName"
        Me.GridColumnSalesmanName.Visible = True
        Me.GridColumnSalesmanName.VisibleIndex = 1
        Me.GridColumnSalesmanName.Width = 353
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Select"
        Me.GridColumn1.ColumnEdit = Me.RepositoryItemButtonEditSelect
        Me.GridColumn1.FieldName = "Select"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.ReadOnly = True
        Me.GridColumn1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 2
        Me.GridColumn1.Width = 152
        '
        'RepositoryItemButtonEditSelect
        '
        Me.RepositoryItemButtonEditSelect.AutoHeight = False
        SerializableAppearanceObject1.Options.UseTextOptions = True
        SerializableAppearanceObject1.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemButtonEditSelect.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleRight, CType(resources.GetObject("RepositoryItemButtonEditSelect.Buttons"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.RepositoryItemButtonEditSelect.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.RepositoryItemButtonEditSelect.Name = "RepositoryItemButtonEditSelect"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btnCancel)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(411, 50)
        Me.PanelControl1.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.Location = New System.Drawing.Point(322, 14)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(85, 31)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.Visible = False
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Location = New System.Drawing.Point(12, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(112, 19)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Salesman List"
        '
        'FrmSalesmanList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(411, 489)
        Me.Controls.Add(Me.GridControlSalesman)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmSalesmanList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Salesman List"
        CType(Me.GridControlSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonEditSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridControlSalesman As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewSalesman As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumnEmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnSalesmanName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnDesignation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnItemName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnSubGroupName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnCommissionType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnFixedAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemButtonEditSelect As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
End Class
