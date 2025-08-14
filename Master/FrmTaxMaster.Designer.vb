<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTaxMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTaxMaster))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImagetaxactive = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.imgcol = New DevExpress.Utils.ImageCollection(Me.components)
        Me.btnsave = New DevExpress.XtraEditors.SimpleButton()
        Me.btncancel = New DevExpress.XtraEditors.SimpleButton()
        Me.chkactive = New DevExpress.XtraEditors.CheckEdit()
        Me.txttaxvalue = New DevExpress.XtraEditors.TextEdit()
        Me.txttaxname = New DevExpress.XtraEditors.TextEdit()
        Me.txttaxid = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.labletaxname1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.labletxttaxvalue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImagetaxactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgcol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkactive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttaxvalue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttaxname.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttaxid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.labletaxname1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.labletxttaxvalue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.LabelControl1)
        Me.LayoutControl1.Controls.Add(Me.GridControl1)
        Me.LayoutControl1.Controls.Add(Me.btnsave)
        Me.LayoutControl1.Controls.Add(Me.btncancel)
        Me.LayoutControl1.Controls.Add(Me.chkactive)
        Me.LayoutControl1.Controls.Add(Me.txttaxvalue)
        Me.LayoutControl1.Controls.Add(Me.txttaxname)
        Me.LayoutControl1.Controls.Add(Me.txttaxid)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(569, 387, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(594, 616)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl1.Location = New System.Drawing.Point(471, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(111, 24)
        Me.LabelControl1.StyleController = Me.LayoutControl1
        Me.LabelControl1.TabIndex = 11
        Me.LabelControl1.Text = "Tax Master"
        '
        'GridControl1
        '
        Me.GridControl1.Location = New System.Drawing.Point(12, 172)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImagetaxactive})
        Me.GridControl1.Size = New System.Drawing.Size(570, 398)
        Me.GridControl1.TabIndex = 10
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView1.Appearance.Row.Options.UseTextOptions = True
        Me.GridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "TaxId"
        Me.GridColumn1.FieldName = "TaxId"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "TaxName"
        Me.GridColumn2.FieldName = "TaxName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "TaxValue"
        Me.GridColumn3.FieldName = "TaxValue"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Active"
        Me.GridColumn4.ColumnEdit = Me.RepositoryItemImagetaxactive
        Me.GridColumn4.FieldName = "Active"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'RepositoryItemImagetaxactive
        '
        Me.RepositoryItemImagetaxactive.AutoHeight = False
        Me.RepositoryItemImagetaxactive.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)})
        Me.RepositoryItemImagetaxactive.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("In-Active", "0", 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Active", "1", 1)})
        Me.RepositoryItemImagetaxactive.Name = "RepositoryItemImagetaxactive"
        Me.RepositoryItemImagetaxactive.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple
        Me.RepositoryItemImagetaxactive.SmallImages = Me.imgcol
        '
        'imgcol
        '
        Me.imgcol.ImageStream = CType(resources.GetObject("imgcol.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.imgcol.Images.SetKeyName(0, "cancel-16x16.png")
        Me.imgcol.Images.SetKeyName(1, "accept.png")
        '
        'btnsave
        '
        Me.btnsave.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnsave.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnsave.Appearance.Options.UseBackColor = True
        Me.btnsave.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btnsave.Image = CType(resources.GetObject("btnsave.Image"), System.Drawing.Image)
        Me.btnsave.Location = New System.Drawing.Point(155, 114)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(204, 38)
        Me.btnsave.TabIndex = 9
        Me.btnsave.Text = "Save"
        '
        'btncancel
        '
        Me.btncancel.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btncancel.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btncancel.Appearance.Options.UseBackColor = True
        Me.btncancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(363, 114)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(219, 38)
        Me.btncancel.TabIndex = 8
        Me.btncancel.Text = "Cancel"
        '
        'chkactive
        '
        Me.chkactive.Location = New System.Drawing.Point(12, 114)
        Me.chkactive.Name = "chkactive"
        Me.chkactive.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.chkactive.Properties.Caption = "Active"
        Me.chkactive.Size = New System.Drawing.Size(139, 21)
        Me.chkactive.StyleController = Me.LayoutControl1
        Me.chkactive.TabIndex = 7
        '
        'txttaxvalue
        '
        Me.txttaxvalue.Location = New System.Drawing.Point(70, 80)
        Me.txttaxvalue.Name = "txttaxvalue"
        Me.txttaxvalue.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.txttaxvalue.Properties.Appearance.Options.UseFont = True
        Me.txttaxvalue.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txttaxvalue.Size = New System.Drawing.Size(512, 30)
        Me.txttaxvalue.StyleController = Me.LayoutControl1
        Me.txttaxvalue.TabIndex = 6
        '
        'txttaxname
        '
        Me.txttaxname.Location = New System.Drawing.Point(70, 46)
        Me.txttaxname.Name = "txttaxname"
        Me.txttaxname.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.txttaxname.Properties.Appearance.Options.UseFont = True
        Me.txttaxname.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txttaxname.Size = New System.Drawing.Size(512, 30)
        Me.txttaxname.StyleController = Me.LayoutControl1
        Me.txttaxname.TabIndex = 5
        '
        'txttaxid
        '
        Me.txttaxid.Location = New System.Drawing.Point(70, 12)
        Me.txttaxid.Name = "txttaxid"
        Me.txttaxid.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.txttaxid.Properties.Appearance.Options.UseFont = True
        Me.txttaxid.Properties.ReadOnly = True
        Me.txttaxid.Size = New System.Drawing.Size(106, 30)
        Me.txttaxid.StyleController = Me.LayoutControl1
        Me.txttaxid.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseBackColor = True
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.labletaxname1, Me.labletxttaxvalue, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.EmptySpaceItem1, Me.LayoutControlItem7, Me.LayoutControlItem8, Me.EmptySpaceItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(594, 616)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.txttaxid
        Me.LayoutControlItem1.CustomizationFormText = "Tax Id :"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(168, 34)
        Me.LayoutControlItem1.Text = "Tax Id :"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(55, 13)
        '
        'labletaxname1
        '
        Me.labletaxname1.Control = Me.txttaxname
        Me.labletaxname1.CustomizationFormText = "Tax Name :"
        Me.labletaxname1.Location = New System.Drawing.Point(0, 34)
        Me.labletaxname1.Name = "labletaxname1"
        Me.labletaxname1.Size = New System.Drawing.Size(574, 34)
        Me.labletaxname1.Text = "Tax Name :"
        Me.labletaxname1.TextSize = New System.Drawing.Size(55, 13)
        '
        'labletxttaxvalue
        '
        Me.labletxttaxvalue.Control = Me.txttaxvalue
        Me.labletxttaxvalue.CustomizationFormText = "Tax Value :"
        Me.labletxttaxvalue.Location = New System.Drawing.Point(0, 68)
        Me.labletxttaxvalue.Name = "labletxttaxvalue"
        Me.labletxttaxvalue.Size = New System.Drawing.Size(574, 34)
        Me.labletxttaxvalue.Text = "Tax Value :"
        Me.labletxttaxvalue.TextSize = New System.Drawing.Size(55, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.chkactive
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 102)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(143, 42)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.btncancel
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(351, 102)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(223, 42)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.btnsave
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(143, 102)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(208, 42)
        Me.LayoutControlItem6.Text = "LayoutControlItem6"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextToControlDistance = 0
        Me.LayoutControlItem6.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 562)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(574, 34)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.GridControl1
        Me.LayoutControlItem7.CustomizationFormText = "Details :"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 144)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(574, 418)
        Me.LayoutControlItem7.Text = "Details :"
        Me.LayoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(55, 13)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.LabelControl1
        Me.LayoutControlItem8.CustomizationFormText = "LayoutControlItem8"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(459, 0)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(115, 34)
        Me.LayoutControlItem8.Text = "LayoutControlItem8"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextToControlDistance = 0
        Me.LayoutControlItem8.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(168, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(291, 34)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'FrmTaxMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 616)
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmTaxMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FrmTaxMaster"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImagetaxactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgcol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkactive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttaxvalue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttaxname.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttaxid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.labletaxname1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.labletxttaxvalue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btncancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkactive As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txttaxvalue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txttaxname As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txttaxid As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents labletaxname1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents labletxttaxvalue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents imgcol As DevExpress.Utils.ImageCollection
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImagetaxactive As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
End Class
