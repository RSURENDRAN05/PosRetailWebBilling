<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDiscount
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.RadioGroup1 = New DevExpress.XtraEditors.RadioGroup()
        Me.txtpercentage = New DevExpress.XtraEditors.TextEdit()
        Me.txtamount = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.SimpleSeparator1 = New DevExpress.XtraLayout.SimpleSeparator()
        Me.SimpleSeparator2 = New DevExpress.XtraLayout.SimpleSeparator()
        Me.SimpleSeparator3 = New DevExpress.XtraLayout.SimpleSeparator()
        Me.SimpleSeparator5 = New DevExpress.XtraLayout.SimpleSeparator()
        Me.SimpleSeparator6 = New DevExpress.XtraLayout.SimpleSeparator()
        Me.SimpleSeparator7 = New DevExpress.XtraLayout.SimpleSeparator()
        Me.SimpleSeparator8 = New DevExpress.XtraLayout.SimpleSeparator()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.SimpleSeparator4 = New DevExpress.XtraLayout.SimpleSeparator()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.RadioGroup1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtamount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleSeparator1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleSeparator2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleSeparator3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleSeparator5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleSeparator6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleSeparator7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleSeparator8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleSeparator4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.Label1)
        Me.LayoutControl1.Controls.Add(Me.SimpleButton2)
        Me.LayoutControl1.Controls.Add(Me.SimpleButton1)
        Me.LayoutControl1.Controls.Add(Me.RadioGroup1)
        Me.LayoutControl1.Controls.Add(Me.txtpercentage)
        Me.LayoutControl1.Controls.Add(Me.txtamount)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(640, 192, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(319, 218)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(293, 20)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Discount Type "
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SimpleButton2.Appearance.Options.UseFont = True
        Me.SimpleButton2.Location = New System.Drawing.Point(157, 182)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(148, 22)
        Me.SimpleButton2.StyleController = Me.LayoutControl1
        Me.SimpleButton2.TabIndex = 8
        Me.SimpleButton2.Text = "Cancel"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SimpleButton1.Appearance.Options.UseFont = True
        Me.SimpleButton1.Location = New System.Drawing.Point(14, 182)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(137, 22)
        Me.SimpleButton1.StyleController = Me.LayoutControl1
        Me.SimpleButton1.TabIndex = 7
        Me.SimpleButton1.Text = "Ok"
        '
        'RadioGroup1
        '
        Me.RadioGroup1.Location = New System.Drawing.Point(83, 38)
        Me.RadioGroup1.Name = "RadioGroup1"
        Me.RadioGroup1.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Percentage"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Amount")})
        Me.RadioGroup1.Size = New System.Drawing.Size(222, 34)
        Me.RadioGroup1.StyleController = Me.LayoutControl1
        Me.RadioGroup1.TabIndex = 6
        '
        'txtpercentage
        '
        Me.txtpercentage.EditValue = ""
        Me.txtpercentage.Location = New System.Drawing.Point(83, 78)
        Me.txtpercentage.Name = "txtpercentage"
        Me.txtpercentage.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!)
        Me.txtpercentage.Properties.Appearance.Options.UseFont = True
        Me.txtpercentage.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtpercentage.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtpercentage.Size = New System.Drawing.Size(222, 46)
        Me.txtpercentage.StyleController = Me.LayoutControl1
        Me.txtpercentage.TabIndex = 5
        '
        'txtamount
        '
        Me.txtamount.Location = New System.Drawing.Point(83, 130)
        Me.txtamount.Name = "txtamount"
        Me.txtamount.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!)
        Me.txtamount.Properties.Appearance.Options.UseFont = True
        Me.txtamount.Properties.DisplayFormat.FormatString = "n2"
        Me.txtamount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtamount.Properties.EditFormat.FormatString = "n2"
        Me.txtamount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtamount.Size = New System.Drawing.Size(222, 46)
        Me.txtamount.StyleController = Me.LayoutControl1
        Me.txtamount.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.SimpleSeparator1, Me.SimpleSeparator2, Me.SimpleSeparator3, Me.SimpleSeparator5, Me.SimpleSeparator6, Me.SimpleSeparator7, Me.SimpleSeparator8, Me.LayoutControlItem6, Me.SimpleSeparator4})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(319, 218)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.BackColor = System.Drawing.Color.Silver
        Me.LayoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.White
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseBackColor = True
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseForeColor = True
        Me.LayoutControlItem2.Control = Me.txtpercentage
        Me.LayoutControlItem2.CustomizationFormText = "Disc Per % :"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(2, 66)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(295, 50)
        Me.LayoutControlItem2.Text = "Disc Per % :"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(66, 13)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.BackColor = System.Drawing.Color.Silver
        Me.LayoutControlItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.White
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseBackColor = True
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseForeColor = True
        Me.LayoutControlItem1.Control = Me.txtamount
        Me.LayoutControlItem1.CustomizationFormText = "Disc Amount :"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(2, 118)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(295, 50)
        Me.LayoutControlItem1.Text = "Disc Amount :"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(66, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.BackColor = System.Drawing.Color.Silver
        Me.LayoutControlItem3.AppearanceItemCaption.ForeColor = System.Drawing.Color.White
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseBackColor = True
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseForeColor = True
        Me.LayoutControlItem3.Control = Me.RadioGroup1
        Me.LayoutControlItem3.CustomizationFormText = "ItemWise Disc :"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(2, 26)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(295, 38)
        Me.LayoutControlItem3.Text = "Disc Type :"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(66, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.SimpleButton1
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(2, 170)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(141, 26)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.SimpleButton2
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(145, 170)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(152, 26)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'SimpleSeparator1
        '
        Me.SimpleSeparator1.AllowHotTrack = False
        Me.SimpleSeparator1.CustomizationFormText = "SimpleSeparator1"
        Me.SimpleSeparator1.Location = New System.Drawing.Point(2, 168)
        Me.SimpleSeparator1.Name = "SimpleSeparator1"
        Me.SimpleSeparator1.Size = New System.Drawing.Size(295, 2)
        Me.SimpleSeparator1.Text = "SimpleSeparator1"
        '
        'SimpleSeparator2
        '
        Me.SimpleSeparator2.AllowHotTrack = False
        Me.SimpleSeparator2.CustomizationFormText = "SimpleSeparator2"
        Me.SimpleSeparator2.Location = New System.Drawing.Point(2, 116)
        Me.SimpleSeparator2.Name = "SimpleSeparator2"
        Me.SimpleSeparator2.Size = New System.Drawing.Size(295, 2)
        Me.SimpleSeparator2.Text = "SimpleSeparator2"
        '
        'SimpleSeparator3
        '
        Me.SimpleSeparator3.AllowHotTrack = False
        Me.SimpleSeparator3.CustomizationFormText = "SimpleSeparator3"
        Me.SimpleSeparator3.Location = New System.Drawing.Point(2, 64)
        Me.SimpleSeparator3.Name = "SimpleSeparator3"
        Me.SimpleSeparator3.Size = New System.Drawing.Size(295, 2)
        Me.SimpleSeparator3.Text = "SimpleSeparator3"
        '
        'SimpleSeparator5
        '
        Me.SimpleSeparator5.AllowHotTrack = False
        Me.SimpleSeparator5.CustomizationFormText = "SimpleSeparator5"
        Me.SimpleSeparator5.Location = New System.Drawing.Point(0, 26)
        Me.SimpleSeparator5.Name = "SimpleSeparator5"
        Me.SimpleSeparator5.Size = New System.Drawing.Size(2, 172)
        Me.SimpleSeparator5.Text = "SimpleSeparator5"
        '
        'SimpleSeparator6
        '
        Me.SimpleSeparator6.AllowHotTrack = False
        Me.SimpleSeparator6.CustomizationFormText = "SimpleSeparator6"
        Me.SimpleSeparator6.Location = New System.Drawing.Point(2, 196)
        Me.SimpleSeparator6.Name = "SimpleSeparator6"
        Me.SimpleSeparator6.Size = New System.Drawing.Size(295, 2)
        Me.SimpleSeparator6.Text = "SimpleSeparator6"
        '
        'SimpleSeparator7
        '
        Me.SimpleSeparator7.AllowHotTrack = False
        Me.SimpleSeparator7.CustomizationFormText = "SimpleSeparator7"
        Me.SimpleSeparator7.Location = New System.Drawing.Point(0, 0)
        Me.SimpleSeparator7.Name = "SimpleSeparator7"
        Me.SimpleSeparator7.Size = New System.Drawing.Size(297, 2)
        Me.SimpleSeparator7.Text = "SimpleSeparator7"
        '
        'SimpleSeparator8
        '
        Me.SimpleSeparator8.AllowHotTrack = False
        Me.SimpleSeparator8.CustomizationFormText = "SimpleSeparator8"
        Me.SimpleSeparator8.Location = New System.Drawing.Point(143, 170)
        Me.SimpleSeparator8.Name = "SimpleSeparator8"
        Me.SimpleSeparator8.Size = New System.Drawing.Size(2, 26)
        Me.SimpleSeparator8.Text = "SimpleSeparator8"
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.Label1
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 2)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(297, 24)
        Me.LayoutControlItem6.Text = "LayoutControlItem6"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextToControlDistance = 0
        Me.LayoutControlItem6.TextVisible = False
        '
        'SimpleSeparator4
        '
        Me.SimpleSeparator4.AllowHotTrack = False
        Me.SimpleSeparator4.CustomizationFormText = "SimpleSeparator4"
        Me.SimpleSeparator4.Location = New System.Drawing.Point(297, 0)
        Me.SimpleSeparator4.Name = "SimpleSeparator4"
        Me.SimpleSeparator4.Size = New System.Drawing.Size(2, 198)
        Me.SimpleSeparator4.Text = "SimpleSeparator4"
        '
        'frmDiscount
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(319, 218)
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmDiscount"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Discount"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.RadioGroup1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtamount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleSeparator1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleSeparator2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleSeparator3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleSeparator5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleSeparator6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleSeparator7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleSeparator8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleSeparator4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RadioGroup1 As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents txtpercentage As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtamount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents SimpleSeparator1 As DevExpress.XtraLayout.SimpleSeparator
    Friend WithEvents SimpleSeparator2 As DevExpress.XtraLayout.SimpleSeparator
    Friend WithEvents SimpleSeparator3 As DevExpress.XtraLayout.SimpleSeparator
    Friend WithEvents SimpleSeparator5 As DevExpress.XtraLayout.SimpleSeparator
    Friend WithEvents SimpleSeparator6 As DevExpress.XtraLayout.SimpleSeparator
    Friend WithEvents SimpleSeparator7 As DevExpress.XtraLayout.SimpleSeparator
    Friend WithEvents SimpleSeparator8 As DevExpress.XtraLayout.SimpleSeparator
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents SimpleSeparator4 As DevExpress.XtraLayout.SimpleSeparator
End Class
