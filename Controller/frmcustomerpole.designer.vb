<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmcustomerpole
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
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.cmbStartupType = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.barbtnTesting = New DevExpress.XtraEditors.SimpleButton()
        Me.txtDefaultDisplay = New DevExpress.XtraEditors.TextEdit()
        Me.cmbBaud = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.btnUpdate = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbportname = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbDataBit = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbStopBits = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbParity = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.SimpleSeparator1 = New DevExpress.XtraLayout.SimpleSeparator()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.XtraTabProperties = New DevExpress.XtraTab.XtraTabPage()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbStartupType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtDefaultDisplay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBaud.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbportname.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbDataBit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbStopBits.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbParity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleSeparator1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabProperties.SuspendLayout()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.LayoutControlItem5.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem5.Control = Me.cmbStartupType
        Me.LayoutControlItem5.CustomizationFormText = "Startup Type :"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 175)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(225, 26)
        Me.LayoutControlItem5.Text = "Startup Type :"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(84, 16)
        '
        'cmbStartupType
        '
        Me.cmbStartupType.Location = New System.Drawing.Point(99, 187)
        Me.cmbStartupType.Name = "cmbStartupType"
        Me.cmbStartupType.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.cmbStartupType.Properties.Appearance.Options.UseFont = True
        Me.cmbStartupType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbStartupType.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Automatic", 0, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Manual", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Disabled", 2, -1)})
        Me.cmbStartupType.Size = New System.Drawing.Size(134, 22)
        Me.cmbStartupType.StyleController = Me.LayoutControl1
        Me.cmbStartupType.TabIndex = 8
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.barbtnTesting)
        Me.LayoutControl1.Controls.Add(Me.txtDefaultDisplay)
        Me.LayoutControl1.Controls.Add(Me.cmbBaud)
        Me.LayoutControl1.Controls.Add(Me.btnAdd)
        Me.LayoutControl1.Controls.Add(Me.btnUpdate)
        Me.LayoutControl1.Controls.Add(Me.cmbportname)
        Me.LayoutControl1.Controls.Add(Me.cmbDataBit)
        Me.LayoutControl1.Controls.Add(Me.cmbStopBits)
        Me.LayoutControl1.Controls.Add(Me.cmbParity)
        Me.LayoutControl1.Controls.Add(Me.cmbStartupType)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(646, 124, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(245, 277)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'barbtnTesting
        '
        Me.barbtnTesting.Location = New System.Drawing.Point(95, 243)
        Me.barbtnTesting.Name = "barbtnTesting"
        Me.barbtnTesting.Size = New System.Drawing.Size(66, 22)
        Me.barbtnTesting.StyleController = Me.LayoutControl1
        Me.barbtnTesting.TabIndex = 14
        Me.barbtnTesting.Text = "Testing"
        '
        'txtDefaultDisplay
        '
        Me.txtDefaultDisplay.Location = New System.Drawing.Point(12, 31)
        Me.txtDefaultDisplay.Name = "txtDefaultDisplay"
        Me.txtDefaultDisplay.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.txtDefaultDisplay.Properties.Appearance.Options.UseFont = True
        Me.txtDefaultDisplay.Size = New System.Drawing.Size(221, 22)
        Me.txtDefaultDisplay.StyleController = Me.LayoutControl1
        Me.txtDefaultDisplay.TabIndex = 13
        '
        'cmbBaud
        '
        Me.cmbBaud.Location = New System.Drawing.Point(99, 161)
        Me.cmbBaud.Name = "cmbBaud"
        Me.cmbBaud.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.cmbBaud.Properties.Appearance.Options.UseFont = True
        Me.cmbBaud.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBaud.Properties.Items.AddRange(New Object() {"9600", "19200", "38400", "57600", "115200", "0000"})
        Me.cmbBaud.Size = New System.Drawing.Size(134, 22)
        Me.cmbBaud.StyleController = Me.LayoutControl1
        Me.cmbBaud.TabIndex = 7
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(212, 57)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(21, 22)
        Me.btnAdd.StyleController = Me.LayoutControl1
        Me.btnAdd.TabIndex = 10
        Me.btnAdd.Text = "+"
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(165, 243)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(68, 22)
        Me.btnUpdate.StyleController = Me.LayoutControl1
        Me.btnUpdate.TabIndex = 9
        Me.btnUpdate.Text = "Update"
        '
        'cmbportname
        '
        Me.cmbportname.Location = New System.Drawing.Point(99, 57)
        Me.cmbportname.Name = "cmbportname"
        Me.cmbportname.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.cmbportname.Properties.Appearance.Options.UseFont = True
        Me.cmbportname.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbportname.Size = New System.Drawing.Size(109, 22)
        Me.cmbportname.StyleController = Me.LayoutControl1
        Me.cmbportname.TabIndex = 7
        '
        'cmbDataBit
        '
        Me.cmbDataBit.Location = New System.Drawing.Point(99, 135)
        Me.cmbDataBit.Name = "cmbDataBit"
        Me.cmbDataBit.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.cmbDataBit.Properties.Appearance.Options.UseFont = True
        Me.cmbDataBit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDataBit.Properties.Items.AddRange(New Object() {"8", "9", "10", "12", "16", "0"})
        Me.cmbDataBit.Size = New System.Drawing.Size(134, 22)
        Me.cmbDataBit.StyleController = Me.LayoutControl1
        Me.cmbDataBit.TabIndex = 6
        '
        'cmbStopBits
        '
        Me.cmbStopBits.Location = New System.Drawing.Point(99, 109)
        Me.cmbStopBits.Name = "cmbStopBits"
        Me.cmbStopBits.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.cmbStopBits.Properties.Appearance.Options.UseFont = True
        Me.cmbStopBits.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbStopBits.Properties.Items.AddRange(New Object() {"None", "One", "OnePointFive", "Two", "Nothing"})
        Me.cmbStopBits.Size = New System.Drawing.Size(134, 22)
        Me.cmbStopBits.StyleController = Me.LayoutControl1
        Me.cmbStopBits.TabIndex = 5
        '
        'cmbParity
        '
        Me.cmbParity.Location = New System.Drawing.Point(99, 83)
        Me.cmbParity.Name = "cmbParity"
        Me.cmbParity.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.cmbParity.Properties.Appearance.Options.UseFont = True
        Me.cmbParity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbParity.Properties.Items.AddRange(New Object() {"Even", "Mark", "None", "Odd", "Space", "Nothing"})
        Me.cmbParity.Size = New System.Drawing.Size(134, 22)
        Me.cmbParity.StyleController = Me.LayoutControl1
        Me.cmbParity.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.EmptySpaceItem2, Me.LayoutControlItem8, Me.LayoutControlItem4, Me.LayoutControlItem7, Me.SimpleSeparator1, Me.LayoutControlItem11, Me.LayoutControlItem9, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(245, 277)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem1.Control = Me.cmbParity
        Me.LayoutControlItem1.CustomizationFormText = "Parity :"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 71)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(225, 26)
        Me.LayoutControlItem1.Text = "Parity :"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(84, 16)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem2.Control = Me.cmbStopBits
        Me.LayoutControlItem2.CustomizationFormText = "StopBits :"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 97)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(225, 26)
        Me.LayoutControlItem2.Text = "StopBits :"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(84, 16)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem3.Control = Me.cmbDataBit
        Me.LayoutControlItem3.CustomizationFormText = "DataBits :"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 123)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(225, 26)
        Me.LayoutControlItem3.Text = "DataBits :"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(84, 16)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.btnUpdate
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(153, 231)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(72, 26)
        Me.LayoutControlItem6.Text = "LayoutControlItem6"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextToControlDistance = 0
        Me.LayoutControlItem6.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 231)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(83, 26)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.LayoutControlItem8.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem8.Control = Me.cmbBaud
        Me.LayoutControlItem8.CustomizationFormText = "Baud Rate :"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 149)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(225, 26)
        Me.LayoutControlItem8.Text = "Baud Rate :"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(84, 16)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem4.Control = Me.cmbportname
        Me.LayoutControlItem4.CustomizationFormText = "PortName :"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 45)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(200, 26)
        Me.LayoutControlItem4.Text = "PortName :"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(84, 16)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.btnAdd
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(200, 45)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(25, 26)
        Me.LayoutControlItem7.Text = "LayoutControlItem7"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextToControlDistance = 0
        Me.LayoutControlItem7.TextVisible = False
        '
        'SimpleSeparator1
        '
        Me.SimpleSeparator1.AllowHotTrack = False
        Me.SimpleSeparator1.CustomizationFormText = "SimpleSeparator1"
        Me.SimpleSeparator1.Location = New System.Drawing.Point(0, 201)
        Me.SimpleSeparator1.Name = "SimpleSeparator1"
        Me.SimpleSeparator1.Size = New System.Drawing.Size(225, 12)
        Me.SimpleSeparator1.Spacing = New DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5)
        Me.SimpleSeparator1.Text = "SimpleSeparator1"
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.LayoutControlItem11.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem11.Control = Me.txtDefaultDisplay
        Me.LayoutControlItem11.CustomizationFormText = "Default Display"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(225, 45)
        Me.LayoutControlItem11.Text = "Default Display"
        Me.LayoutControlItem11.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(84, 16)
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.barbtnTesting
        Me.LayoutControlItem9.CustomizationFormText = "LayoutControlItem9"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(83, 231)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(70, 26)
        Me.LayoutControlItem9.Text = "LayoutControlItem9"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem9.TextToControlDistance = 0
        Me.LayoutControlItem9.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 213)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(225, 18)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'XtraTabProperties
        '
        Me.XtraTabProperties.Controls.Add(Me.LayoutControl1)
        Me.XtraTabProperties.Name = "XtraTabProperties"
        Me.XtraTabProperties.Size = New System.Drawing.Size(245, 277)
        Me.XtraTabProperties.Text = "Properties"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabProperties
        Me.XtraTabControl1.Size = New System.Drawing.Size(251, 305)
        Me.XtraTabControl1.TabIndex = 1
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabProperties})
        '
        'frmcustomerpole
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(251, 305)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Name = "frmcustomerpole"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Customer Pole Settings"
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbStartupType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtDefaultDisplay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBaud.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbportname.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbDataBit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbStopBits.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbParity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleSeparator1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabProperties.ResumeLayout(False)
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cmbStartupType As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents barbtnTesting As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtDefaultDisplay As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbBaud As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnUpdate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbportname As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbDataBit As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbStopBits As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbParity As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents SimpleSeparator1 As DevExpress.XtraLayout.SimpleSeparator
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents XtraTabProperties As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
End Class
