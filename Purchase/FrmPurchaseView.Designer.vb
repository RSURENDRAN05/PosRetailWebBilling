<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPurchaseView
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPurchaseView))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.BTNCANCEL = New DevExpress.XtraEditors.SimpleButton()
        Me.BTNoK = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControlGRNSelector = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumnPM_ID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnGRN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnBillNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnSupplierName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnRetrun = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBoxRetrun = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ImageCollectionGRNSelect = New DevExpress.Utils.ImageCollection(Me.components)
        Me.GridColumnBillAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnGivenTotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1OtherCount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1St_StaffName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1St_UserID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.GridControlGRNSelector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBoxRetrun, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollectionGRNSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.BTNCANCEL)
        Me.LayoutControl1.Controls.Add(Me.BTNoK)
        Me.LayoutControl1.Controls.Add(Me.GridControlGRNSelector)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(906, 371, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1014, 624)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'BTNCANCEL
        '
        Me.BTNCANCEL.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BTNCANCEL.Appearance.ForeColor = System.Drawing.Color.Black
        Me.BTNCANCEL.Appearance.Options.UseBackColor = True
        Me.BTNCANCEL.Appearance.Options.UseForeColor = True
        Me.BTNCANCEL.Image = CType(resources.GetObject("BTNCANCEL.Image"), System.Drawing.Image)
        Me.BTNCANCEL.Location = New System.Drawing.Point(917, 574)
        Me.BTNCANCEL.Name = "BTNCANCEL"
        Me.BTNCANCEL.Size = New System.Drawing.Size(85, 38)
        Me.BTNCANCEL.StyleController = Me.LayoutControl1
        Me.BTNCANCEL.TabIndex = 7
        Me.BTNCANCEL.Text = "CANCEL"
        '
        'BTNoK
        '
        Me.BTNoK.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BTNoK.Appearance.ForeColor = System.Drawing.Color.Black
        Me.BTNoK.Appearance.Options.UseBackColor = True
        Me.BTNoK.Appearance.Options.UseForeColor = True
        Me.BTNoK.Image = CType(resources.GetObject("BTNoK.Image"), System.Drawing.Image)
        Me.BTNoK.Location = New System.Drawing.Point(821, 574)
        Me.BTNoK.Name = "BTNoK"
        Me.BTNoK.Size = New System.Drawing.Size(92, 38)
        Me.BTNoK.StyleController = Me.LayoutControl1
        Me.BTNoK.TabIndex = 6
        Me.BTNoK.Text = "OK"
        '
        'GridControlGRNSelector
        '
        Me.GridControlGRNSelector.Location = New System.Drawing.Point(12, 28)
        Me.GridControlGRNSelector.MainView = Me.GridView1
        Me.GridControlGRNSelector.Name = "GridControlGRNSelector"
        Me.GridControlGRNSelector.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBoxRetrun})
        Me.GridControlGRNSelector.Size = New System.Drawing.Size(990, 542)
        Me.GridControlGRNSelector.TabIndex = 5
        Me.GridControlGRNSelector.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumnPM_ID, Me.GridColumnGRN, Me.GridColumnBillNo, Me.GridColumnDate, Me.GridColumnSupplierName, Me.GridColumnRetrun, Me.GridColumnBillAmount, Me.GridColumnGivenTotal, Me.GridColumn1OtherCount, Me.GridColumn1St_StaffName, Me.GridColumn1St_UserID})
        Me.GridView1.GridControl = Me.GridControlGRNSelector
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        '
        'GridColumnPM_ID
        '
        Me.GridColumnPM_ID.Caption = "GridColumn1"
        Me.GridColumnPM_ID.FieldName = "PM_ID"
        Me.GridColumnPM_ID.Name = "GridColumnPM_ID"
        '
        'GridColumnGRN
        '
        Me.GridColumnGRN.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnGRN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumnGRN.Caption = "GRN No"
        Me.GridColumnGRN.FieldName = "GRNNo"
        Me.GridColumnGRN.Name = "GridColumnGRN"
        Me.GridColumnGRN.Visible = True
        Me.GridColumnGRN.VisibleIndex = 0
        Me.GridColumnGRN.Width = 120
        '
        'GridColumnBillNo
        '
        Me.GridColumnBillNo.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnBillNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumnBillNo.Caption = "Bill No"
        Me.GridColumnBillNo.FieldName = "BillNo"
        Me.GridColumnBillNo.Name = "GridColumnBillNo"
        Me.GridColumnBillNo.Visible = True
        Me.GridColumnBillNo.VisibleIndex = 1
        Me.GridColumnBillNo.Width = 91
        '
        'GridColumnDate
        '
        Me.GridColumnDate.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumnDate.Caption = "Date"
        Me.GridColumnDate.DisplayFormat.FormatString = "dd-MM-yyyy"
        Me.GridColumnDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumnDate.FieldName = "PurchaseDate"
        Me.GridColumnDate.GroupFormat.FormatString = "dd-MM-yyyy"
        Me.GridColumnDate.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumnDate.Name = "GridColumnDate"
        Me.GridColumnDate.UnboundType = DevExpress.Data.UnboundColumnType.DateTime
        Me.GridColumnDate.Visible = True
        Me.GridColumnDate.VisibleIndex = 2
        Me.GridColumnDate.Width = 117
        '
        'GridColumnSupplierName
        '
        Me.GridColumnSupplierName.Caption = "Supplier Name"
        Me.GridColumnSupplierName.FieldName = "S_SupplierName"
        Me.GridColumnSupplierName.Name = "GridColumnSupplierName"
        Me.GridColumnSupplierName.Visible = True
        Me.GridColumnSupplierName.VisibleIndex = 3
        Me.GridColumnSupplierName.Width = 301
        '
        'GridColumnRetrun
        '
        Me.GridColumnRetrun.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnRetrun.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumnRetrun.Caption = "New/Retrun"
        Me.GridColumnRetrun.ColumnEdit = Me.RepositoryItemImageComboBoxRetrun
        Me.GridColumnRetrun.FieldName = "StatusPR"
        Me.GridColumnRetrun.Name = "GridColumnRetrun"
        Me.GridColumnRetrun.Visible = True
        Me.GridColumnRetrun.VisibleIndex = 4
        Me.GridColumnRetrun.Width = 105
        '
        'RepositoryItemImageComboBoxRetrun
        '
        Me.RepositoryItemImageComboBoxRetrun.AutoHeight = False
        Me.RepositoryItemImageComboBoxRetrun.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.RepositoryItemImageComboBoxRetrun.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)})
        Me.RepositoryItemImageComboBoxRetrun.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.RepositoryItemImageComboBoxRetrun.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("New", "PI", 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Return", "PR", 1)})
        Me.RepositoryItemImageComboBoxRetrun.Name = "RepositoryItemImageComboBoxRetrun"
        Me.RepositoryItemImageComboBoxRetrun.SmallImages = Me.ImageCollectionGRNSelect
        '
        'ImageCollectionGRNSelect
        '
        Me.ImageCollectionGRNSelect.ImageStream = CType(resources.GetObject("ImageCollectionGRNSelect.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollectionGRNSelect.Images.SetKeyName(0, "stop_green.png")
        Me.ImageCollectionGRNSelect.Images.SetKeyName(1, "stop_red.png")
        '
        'GridColumnBillAmount
        '
        Me.GridColumnBillAmount.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumnBillAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumnBillAmount.Caption = "Total Amount"
        Me.GridColumnBillAmount.FieldName = "BillAmount"
        Me.GridColumnBillAmount.Name = "GridColumnBillAmount"
        Me.GridColumnBillAmount.Visible = True
        Me.GridColumnBillAmount.VisibleIndex = 5
        Me.GridColumnBillAmount.Width = 121
        '
        'GridColumnGivenTotal
        '
        Me.GridColumnGivenTotal.Caption = "Bill Amount"
        Me.GridColumnGivenTotal.FieldName = "GivenTotal"
        Me.GridColumnGivenTotal.Name = "GridColumnGivenTotal"
        '
        'GridColumn1OtherCount
        '
        Me.GridColumn1OtherCount.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1OtherCount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1OtherCount.Caption = "PaymentType"
        Me.GridColumn1OtherCount.FieldName = "PaymentType"
        Me.GridColumn1OtherCount.Name = "GridColumn1OtherCount"
        Me.GridColumn1OtherCount.Visible = True
        Me.GridColumn1OtherCount.VisibleIndex = 6
        Me.GridColumn1OtherCount.Width = 117
        '
        'GridColumn1St_StaffName
        '
        Me.GridColumn1St_StaffName.Caption = "User Name"
        Me.GridColumn1St_StaffName.FieldName = "St_StaffName"
        Me.GridColumn1St_StaffName.Name = "GridColumn1St_StaffName"
        '
        'GridColumn1St_UserID
        '
        Me.GridColumn1St_UserID.Caption = "User ID"
        Me.GridColumn1St_UserID.FieldName = "St_UserID"
        Me.GridColumn1St_UserID.Name = "GridColumn1St_UserID"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1014, 624)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.GridControlGRNSelector
        Me.LayoutControlItem1.CustomizationFormText = "Select GRN"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(994, 562)
        Me.LayoutControlItem1.Text = "Select GRN"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(53, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.BTNoK
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(809, 562)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(96, 42)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.BTNCANCEL
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(905, 562)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(89, 42)
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 562)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(809, 42)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'FrmPurchaseView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1014, 624)
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.[Default]
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmPurchaseView"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Purchase View"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.GridControlGRNSelector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBoxRetrun, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollectionGRNSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridControlGRNSelector As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumnPM_ID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnGRN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnBillNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnSupplierName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnRetrun As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBoxRetrun As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumnBillAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnGivenTotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1OtherCount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1St_StaffName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1St_UserID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ImageCollectionGRNSelect As DevExpress.Utils.ImageCollection
    Friend WithEvents BTNCANCEL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BTNoK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
End Class
