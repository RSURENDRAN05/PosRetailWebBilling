<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBarcodedesigner
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
        Me.SplitterItem2 = New DevExpress.XtraLayout.SplitterItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.ListBoxControl1 = New DevExpress.XtraEditors.ListBoxControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.RichEditControlBarcodeText = New DevExpress.XtraRichEdit.RichEditControl()
        Me.btndelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControlBarcodelabel = New DevExpress.XtraGrid.GridControl()
        Me.GridViewBarcodeLabel = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumnID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnScriptName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnBarcodeTxt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnNew = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.SplitterItem1 = New DevExpress.XtraLayout.SplitterItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.SimpleSeparator1 = New DevExpress.XtraLayout.SimpleSeparator()
        Me.SimpleSeparator2 = New DevExpress.XtraLayout.SimpleSeparator()
        Me.SimpleSeparator3 = New DevExpress.XtraLayout.SimpleSeparator()
        CType(Me.SplitterItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListBoxControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.GridControlBarcodelabel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewBarcodeLabel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitterItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleSeparator1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleSeparator2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleSeparator3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitterItem2
        '
        Me.SplitterItem2.AllowHotTrack = True
        Me.SplitterItem2.CustomizationFormText = "SplitterItem2"
        Me.SplitterItem2.Location = New System.Drawing.Point(241, 2)
        Me.SplitterItem2.Name = "SplitterItem2"
        Me.SplitterItem2.Size = New System.Drawing.Size(5, 742)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.ListBoxControl1
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 506)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(241, 238)
        Me.LayoutControlItem6.Text = "LayoutControlItem6"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextToControlDistance = 0
        Me.LayoutControlItem6.TextVisible = False
        '
        'ListBoxControl1
        '
        Me.ListBoxControl1.Items.AddRange(New Object() {"Product Number = <Material_no>", "Product Name = <ProductName>", "Product ShortName = <S_Shortname>", "Product Price = <ProductPrice>", "Product Fixed Price=<FixedPrice>", "Product Color = <ProductColor>", "Product ColorShort = <ProductShortColor>", "BrandName = <BrandName>", "BrandShortName = <BrandShortName>", "DesignName = <DesignName>", "DesignShortName = <DesignShortName>", "MaterialCategory = <MaterialCategory>", "MaterialCategoryShortName = <CShortName>", "DressSize = <DressSize>", "PackingDate=<PackingDate>", "No of Print = <NPrint>", "MRPRate=<MRPRate>", "BatchCode=<BatchCode>", "Exp Date =<ExpDate>"})
        Me.ListBoxControl1.Location = New System.Drawing.Point(9, 515)
        Me.ListBoxControl1.Name = "ListBoxControl1"
        Me.ListBoxControl1.Size = New System.Drawing.Size(237, 234)
        Me.ListBoxControl1.StyleController = Me.LayoutControl1
        Me.ListBoxControl1.TabIndex = 14
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.RichEditControlBarcodeText)
        Me.LayoutControl1.Controls.Add(Me.ListBoxControl1)
        Me.LayoutControl1.Controls.Add(Me.btndelete)
        Me.LayoutControl1.Controls.Add(Me.btnSave)
        Me.LayoutControl1.Controls.Add(Me.GridControlBarcodelabel)
        Me.LayoutControl1.Controls.Add(Me.btnNew)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(767, 224, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1151, 787)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'RichEditControlBarcodeText
        '
        Me.RichEditControlBarcodeText.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple
        Me.RichEditControlBarcodeText.Location = New System.Drawing.Point(257, 27)
        Me.RichEditControlBarcodeText.Name = "RichEditControlBarcodeText"
        Me.RichEditControlBarcodeText.Options.Fields.UseCurrentCultureDateTimeFormat = False
        Me.RichEditControlBarcodeText.Options.MailMerge.KeepLastParagraph = False
        Me.RichEditControlBarcodeText.Size = New System.Drawing.Size(885, 722)
        Me.RichEditControlBarcodeText.TabIndex = 15
        '
        'btndelete
        '
        Me.btndelete.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.btndelete.Appearance.Options.UseFont = True
        Me.btndelete.Location = New System.Drawing.Point(1006, 755)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 23)
        Me.btndelete.StyleController = Me.LayoutControl1
        Me.btndelete.TabIndex = 13
        Me.btndelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.btnSave.Appearance.Options.UseFont = True
        Me.btnSave.Location = New System.Drawing.Point(934, 755)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 23)
        Me.btnSave.StyleController = Me.LayoutControl1
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "Save"
        '
        'GridControlBarcodelabel
        '
        Me.GridControlBarcodelabel.Location = New System.Drawing.Point(9, 27)
        Me.GridControlBarcodelabel.MainView = Me.GridViewBarcodeLabel
        Me.GridControlBarcodelabel.Name = "GridControlBarcodelabel"
        Me.GridControlBarcodelabel.Size = New System.Drawing.Size(237, 479)
        Me.GridControlBarcodelabel.TabIndex = 5
        Me.GridControlBarcodelabel.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewBarcodeLabel})
        '
        'GridViewBarcodeLabel
        '
        Me.GridViewBarcodeLabel.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumnID, Me.GridColumnScriptName, Me.GridColumnBarcodeTxt})
        Me.GridViewBarcodeLabel.GridControl = Me.GridControlBarcodelabel
        Me.GridViewBarcodeLabel.Name = "GridViewBarcodeLabel"
        Me.GridViewBarcodeLabel.OptionsBehavior.Editable = False
        Me.GridViewBarcodeLabel.OptionsView.ShowGroupPanel = False
        '
        'GridColumnID
        '
        Me.GridColumnID.Caption = "ID"
        Me.GridColumnID.FieldName = "B_ID"
        Me.GridColumnID.Name = "GridColumnID"
        '
        'GridColumnScriptName
        '
        Me.GridColumnScriptName.Caption = "Label Name"
        Me.GridColumnScriptName.FieldName = "BarcodeName"
        Me.GridColumnScriptName.Name = "GridColumnScriptName"
        Me.GridColumnScriptName.Visible = True
        Me.GridColumnScriptName.VisibleIndex = 0
        '
        'GridColumnBarcodeTxt
        '
        Me.GridColumnBarcodeTxt.Caption = "GridColumn1"
        Me.GridColumnBarcodeTxt.FieldName = "Barcode"
        Me.GridColumnBarcodeTxt.Name = "GridColumnBarcodeTxt"
        '
        'btnNew
        '
        Me.btnNew.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.btnNew.Appearance.Options.UseFont = True
        Me.btnNew.Location = New System.Drawing.Point(1076, 755)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(66, 23)
        Me.btnNew.StyleController = Me.LayoutControl1
        Me.btnNew.TabIndex = 11
        Me.btnNew.Text = "New"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem4, Me.EmptySpaceItem1, Me.LayoutControlItem3, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.SplitterItem1, Me.SplitterItem2, Me.LayoutControlItem7, Me.SimpleSeparator1, Me.SimpleSeparator2, Me.SimpleSeparator3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(7, 7, 7, 7)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1151, 787)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.GridControlBarcodelabel
        Me.LayoutControlItem2.CustomizationFormText = "List Of Barcode Lable"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 2)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(241, 499)
        Me.LayoutControlItem2.Text = "List Of Barcode Lable :"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(108, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.btnSave
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(925, 746)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(72, 27)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(72, 27)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(72, 27)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 746)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(925, 27)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.btnNew
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(1067, 746)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(70, 27)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(70, 27)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(70, 27)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.btndelete
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(997, 746)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(70, 27)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(70, 27)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(70, 27)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'SplitterItem1
        '
        Me.SplitterItem1.AllowHotTrack = True
        Me.SplitterItem1.CustomizationFormText = "SplitterItem1"
        Me.SplitterItem1.Location = New System.Drawing.Point(0, 501)
        Me.SplitterItem1.Name = "SplitterItem1"
        Me.SplitterItem1.Size = New System.Drawing.Size(241, 5)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.RichEditControlBarcodeText
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(248, 2)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(889, 742)
        Me.LayoutControlItem7.Text = "Print Label"
        Me.LayoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(108, 13)
        '
        'SimpleSeparator1
        '
        Me.SimpleSeparator1.AllowHotTrack = False
        Me.SimpleSeparator1.CustomizationFormText = "SimpleSeparator1"
        Me.SimpleSeparator1.Location = New System.Drawing.Point(246, 2)
        Me.SimpleSeparator1.Name = "SimpleSeparator1"
        Me.SimpleSeparator1.Size = New System.Drawing.Size(2, 742)
        Me.SimpleSeparator1.Text = "SimpleSeparator1"
        '
        'SimpleSeparator2
        '
        Me.SimpleSeparator2.AllowHotTrack = False
        Me.SimpleSeparator2.CustomizationFormText = "SimpleSeparator2"
        Me.SimpleSeparator2.Location = New System.Drawing.Point(0, 744)
        Me.SimpleSeparator2.Name = "SimpleSeparator2"
        Me.SimpleSeparator2.Size = New System.Drawing.Size(1137, 2)
        Me.SimpleSeparator2.Text = "SimpleSeparator2"
        '
        'SimpleSeparator3
        '
        Me.SimpleSeparator3.AllowHotTrack = False
        Me.SimpleSeparator3.CustomizationFormText = "SimpleSeparator3"
        Me.SimpleSeparator3.Location = New System.Drawing.Point(0, 0)
        Me.SimpleSeparator3.Name = "SimpleSeparator3"
        Me.SimpleSeparator3.Size = New System.Drawing.Size(1137, 2)
        Me.SimpleSeparator3.Text = "SimpleSeparator3"
        '
        'frmBarcodedesigner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1151, 787)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "frmBarcodedesigner"
        Me.Text = "Barcode Designer"
        CType(Me.SplitterItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListBoxControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.GridControlBarcodelabel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewBarcodeLabel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitterItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleSeparator1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleSeparator2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleSeparator3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitterItem2 As DevExpress.XtraLayout.SplitterItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ListBoxControl1 As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents btndelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControlBarcodelabel As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewBarcodeLabel As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumnID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnScriptName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnBarcodeTxt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnNew As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents SplitterItem1 As DevExpress.XtraLayout.SplitterItem
    Friend WithEvents RichEditControlBarcodeText As DevExpress.XtraRichEdit.RichEditControl
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents SimpleSeparator1 As DevExpress.XtraLayout.SimpleSeparator
    Friend WithEvents SimpleSeparator2 As DevExpress.XtraLayout.SimpleSeparator
    Friend WithEvents SimpleSeparator3 As DevExpress.XtraLayout.SimpleSeparator
End Class
