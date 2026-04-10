<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFingerScanner
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFingerScanner))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.errorRichBox = New System.Windows.Forms.RichTextBox()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnFaceTrail = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.lbltype = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblTimer = New System.Windows.Forms.Label()
        Me.lblempname = New DevExpress.XtraEditors.TextEdit()
        Me.lblempid = New DevExpress.XtraEditors.TextEdit()
        Me.lblSessionmode = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnstartCapture = New DevExpress.XtraEditors.SimpleButton()
        Me.btneveningout = New DevExpress.XtraEditors.SimpleButton()
        Me.btnbreakin = New DevExpress.XtraEditors.SimpleButton()
        Me.btnbreakout = New DevExpress.XtraEditors.SimpleButton()
        Me.btnmorningin = New DevExpress.XtraEditors.SimpleButton()
        Me.lblstatusimage = New DevExpress.XtraEditors.PictureEdit()
        Me.lblfingerimage = New DevExpress.XtraEditors.PictureEdit()
        Me.GridControlLog = New DevExpress.XtraGrid.GridControl()
        Me.GridViewLog = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridControlEmpHeader = New DevExpress.XtraGrid.GridControl()
        Me.GridViewEmpHeader = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.Img = New DevExpress.Utils.ImageCollection(Me.components)
        Me.TimerAtten = New System.Windows.Forms.Timer(Me.components)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.lblempname.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblempid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblstatusimage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfingerimage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControlLog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewLog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControlEmpHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewEmpHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Img, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.errorRichBox)
        Me.LayoutControl1.Controls.Add(Me.PanelControl1)
        Me.LayoutControl1.Controls.Add(Me.GridControlLog)
        Me.LayoutControl1.Controls.Add(Me.GridControlEmpHeader)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1008, 729)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'errorRichBox
        '
        Me.errorRichBox.Location = New System.Drawing.Point(12, 492)
        Me.errorRichBox.Name = "errorRichBox"
        Me.errorRichBox.Size = New System.Drawing.Size(421, 225)
        Me.errorRichBox.TabIndex = 7
        Me.errorRichBox.Text = ""
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btnFaceTrail)
        Me.PanelControl1.Controls.Add(Me.btnRefresh)
        Me.PanelControl1.Controls.Add(Me.lbltype)
        Me.PanelControl1.Controls.Add(Me.Label4)
        Me.PanelControl1.Controls.Add(Me.lblTimer)
        Me.PanelControl1.Controls.Add(Me.lblempname)
        Me.PanelControl1.Controls.Add(Me.lblempid)
        Me.PanelControl1.Controls.Add(Me.lblSessionmode)
        Me.PanelControl1.Controls.Add(Me.Label3)
        Me.PanelControl1.Controls.Add(Me.Label2)
        Me.PanelControl1.Controls.Add(Me.Label1)
        Me.PanelControl1.Controls.Add(Me.btnstartCapture)
        Me.PanelControl1.Controls.Add(Me.btneveningout)
        Me.PanelControl1.Controls.Add(Me.btnbreakin)
        Me.PanelControl1.Controls.Add(Me.btnbreakout)
        Me.PanelControl1.Controls.Add(Me.btnmorningin)
        Me.PanelControl1.Controls.Add(Me.lblstatusimage)
        Me.PanelControl1.Controls.Add(Me.lblfingerimage)
        Me.PanelControl1.Location = New System.Drawing.Point(437, 28)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(559, 346)
        Me.PanelControl1.TabIndex = 6
        '
        'btnFaceTrail
        '
        Me.btnFaceTrail.Location = New System.Drawing.Point(414, 142)
        Me.btnFaceTrail.Name = "btnFaceTrail"
        Me.btnFaceTrail.Size = New System.Drawing.Size(131, 23)
        Me.btnFaceTrail.TabIndex = 25
        Me.btnFaceTrail.Text = "Face Trial"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(414, 177)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(131, 66)
        Me.btnRefresh.TabIndex = 24
        Me.btnRefresh.Text = "Refresh"
        '
        'lbltype
        '
        Me.lbltype.AutoSize = True
        Me.lbltype.Location = New System.Drawing.Point(336, 112)
        Me.lbltype.Name = "lbltype"
        Me.lbltype.Size = New System.Drawing.Size(38, 13)
        Me.lbltype.TabIndex = 23
        Me.lbltype.Text = "Label4"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(265, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Type  :"
        '
        'lblTimer
        '
        Me.lblTimer.AutoSize = True
        Me.lblTimer.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblTimer.ForeColor = System.Drawing.Color.Red
        Me.lblTimer.Location = New System.Drawing.Point(198, 76)
        Me.lblTimer.Name = "lblTimer"
        Me.lblTimer.Size = New System.Drawing.Size(81, 24)
        Me.lblTimer.TabIndex = 14
        Me.lblTimer.Text = "Timer :"
        '
        'lblempname
        '
        Me.lblempname.Enabled = False
        Me.lblempname.Location = New System.Drawing.Point(202, 144)
        Me.lblempname.Name = "lblempname"
        Me.lblempname.Size = New System.Drawing.Size(172, 20)
        Me.lblempname.TabIndex = 13
        '
        'lblempid
        '
        Me.lblempid.Enabled = False
        Me.lblempid.Location = New System.Drawing.Point(202, 112)
        Me.lblempid.Name = "lblempid"
        Me.lblempid.Size = New System.Drawing.Size(43, 20)
        Me.lblempid.TabIndex = 12
        '
        'lblSessionmode
        '
        Me.lblSessionmode.AutoSize = True
        Me.lblSessionmode.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblSessionmode.ForeColor = System.Drawing.Color.Blue
        Me.lblSessionmode.Location = New System.Drawing.Point(198, 46)
        Me.lblSessionmode.Name = "lblSessionmode"
        Me.lblSessionmode.Size = New System.Drawing.Size(0, 24)
        Me.lblSessionmode.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(133, 147)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Emp Name :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(150, 112)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Emp Id :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(153, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Mode :"
        '
        'btnstartCapture
        '
        Me.btnstartCapture.Appearance.BackColor = System.Drawing.Color.Maroon
        Me.btnstartCapture.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnstartCapture.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnstartCapture.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnstartCapture.Appearance.Options.UseBackColor = True
        Me.btnstartCapture.Appearance.Options.UseFont = True
        Me.btnstartCapture.Appearance.Options.UseForeColor = True
        Me.btnstartCapture.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.btnstartCapture.Location = New System.Drawing.Point(149, 177)
        Me.btnstartCapture.Name = "btnstartCapture"
        Me.btnstartCapture.Size = New System.Drawing.Size(225, 66)
        Me.btnstartCapture.TabIndex = 6
        Me.btnstartCapture.Text = "Start Enroll"
        '
        'btneveningout
        '
        Me.btneveningout.Appearance.BackColor = System.Drawing.Color.Navy
        Me.btneveningout.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btneveningout.Appearance.ForeColor = System.Drawing.Color.White
        Me.btneveningout.Appearance.Options.UseBackColor = True
        Me.btneveningout.Appearance.Options.UseFont = True
        Me.btneveningout.Appearance.Options.UseForeColor = True
        Me.btneveningout.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.btneveningout.Location = New System.Drawing.Point(455, 5)
        Me.btneveningout.Name = "btneveningout"
        Me.btneveningout.Size = New System.Drawing.Size(105, 35)
        Me.btneveningout.TabIndex = 5
        Me.btneveningout.Tag = "EveningOut"
        Me.btneveningout.Text = "EveningOut"
        '
        'btnbreakin
        '
        Me.btnbreakin.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnbreakin.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnbreakin.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnbreakin.Appearance.Options.UseBackColor = True
        Me.btnbreakin.Appearance.Options.UseFont = True
        Me.btnbreakin.Appearance.Options.UseForeColor = True
        Me.btnbreakin.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.btnbreakin.Location = New System.Drawing.Point(347, 5)
        Me.btnbreakin.Name = "btnbreakin"
        Me.btnbreakin.Size = New System.Drawing.Size(105, 35)
        Me.btnbreakin.TabIndex = 4
        Me.btnbreakin.Tag = "BreakIn"
        Me.btnbreakin.Text = "BreakIn"
        '
        'btnbreakout
        '
        Me.btnbreakout.Appearance.BackColor = System.Drawing.Color.Blue
        Me.btnbreakout.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnbreakout.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnbreakout.Appearance.Options.UseBackColor = True
        Me.btnbreakout.Appearance.Options.UseFont = True
        Me.btnbreakout.Appearance.Options.UseForeColor = True
        Me.btnbreakout.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.btnbreakout.Location = New System.Drawing.Point(239, 5)
        Me.btnbreakout.Name = "btnbreakout"
        Me.btnbreakout.Size = New System.Drawing.Size(105, 35)
        Me.btnbreakout.TabIndex = 3
        Me.btnbreakout.Tag = "BreakOut"
        Me.btnbreakout.Text = "BreakOut"
        '
        'btnmorningin
        '
        Me.btnmorningin.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnmorningin.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnmorningin.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnmorningin.Appearance.Options.UseBackColor = True
        Me.btnmorningin.Appearance.Options.UseFont = True
        Me.btnmorningin.Appearance.Options.UseForeColor = True
        Me.btnmorningin.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.btnmorningin.Location = New System.Drawing.Point(131, 5)
        Me.btnmorningin.Name = "btnmorningin"
        Me.btnmorningin.Size = New System.Drawing.Size(105, 35)
        Me.btnmorningin.TabIndex = 2
        Me.btnmorningin.Tag = "MorningIn"
        Me.btnmorningin.Text = "MorningIn"
        '
        'lblstatusimage
        '
        Me.lblstatusimage.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblstatusimage.EditValue = CType(resources.GetObject("lblstatusimage.EditValue"), Object)
        Me.lblstatusimage.Location = New System.Drawing.Point(2, 249)
        Me.lblstatusimage.Name = "lblstatusimage"
        Me.lblstatusimage.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblstatusimage.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblstatusimage.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblstatusimage.Properties.Appearance.Options.UseBackColor = True
        Me.lblstatusimage.Properties.Appearance.Options.UseFont = True
        Me.lblstatusimage.Properties.Appearance.Options.UseForeColor = True
        Me.lblstatusimage.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray
        Me.lblstatusimage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        Me.lblstatusimage.Size = New System.Drawing.Size(555, 95)
        Me.lblstatusimage.TabIndex = 1
        '
        'lblfingerimage
        '
        Me.lblfingerimage.Location = New System.Drawing.Point(5, 5)
        Me.lblfingerimage.Name = "lblfingerimage"
        Me.lblfingerimage.Size = New System.Drawing.Size(120, 160)
        Me.lblfingerimage.TabIndex = 0
        '
        'GridControlLog
        '
        Me.GridControlLog.Location = New System.Drawing.Point(437, 394)
        Me.GridControlLog.MainView = Me.GridViewLog
        Me.GridControlLog.Name = "GridControlLog"
        Me.GridControlLog.Size = New System.Drawing.Size(559, 323)
        Me.GridControlLog.TabIndex = 5
        Me.GridControlLog.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewLog})
        '
        'GridViewLog
        '
        Me.GridViewLog.GridControl = Me.GridControlLog
        Me.GridViewLog.Name = "GridViewLog"
        Me.GridViewLog.OptionsView.ShowGroupPanel = False
        '
        'GridControlEmpHeader
        '
        Me.GridControlEmpHeader.Location = New System.Drawing.Point(12, 28)
        Me.GridControlEmpHeader.MainView = Me.GridViewEmpHeader
        Me.GridControlEmpHeader.Name = "GridControlEmpHeader"
        Me.GridControlEmpHeader.Size = New System.Drawing.Size(421, 444)
        Me.GridControlEmpHeader.TabIndex = 4
        Me.GridControlEmpHeader.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewEmpHeader})
        '
        'GridViewEmpHeader
        '
        Me.GridViewEmpHeader.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.GridViewEmpHeader.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewEmpHeader.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridViewEmpHeader.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewEmpHeader.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GridViewEmpHeader.Appearance.Row.Options.UseFont = True
        Me.GridViewEmpHeader.Appearance.Row.Options.UseTextOptions = True
        Me.GridViewEmpHeader.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewEmpHeader.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn8})
        Me.GridViewEmpHeader.GridControl = Me.GridControlEmpHeader
        Me.GridViewEmpHeader.Name = "GridViewEmpHeader"
        Me.GridViewEmpHeader.OptionsBehavior.Editable = False
        Me.GridViewEmpHeader.OptionsBehavior.ReadOnly = True
        Me.GridViewEmpHeader.OptionsView.ShowGroupPanel = False
        Me.GridViewEmpHeader.RowHeight = 30
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "EmpId"
        Me.GridColumn1.FieldName = "emp_id"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 66
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "EmpName"
        Me.GridColumn2.FieldName = "emp_printname"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 248
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Type"
        Me.GridColumn8.FieldName = "emp_type"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 2
        Me.GridColumn8.Width = 85
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1008, 729)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.GridControlEmpHeader
        Me.LayoutControlItem1.CustomizationFormText = "Empolyee Info"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(425, 464)
        Me.LayoutControlItem1.Text = "Empolyee Info"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(95, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.GridControlLog
        Me.LayoutControlItem2.CustomizationFormText = "Log"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(425, 366)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(563, 343)
        Me.LayoutControlItem2.Text = "Log"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(95, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.PanelControl1
        Me.LayoutControlItem3.CustomizationFormText = "Finger Print Process"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(425, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(563, 366)
        Me.LayoutControlItem3.Text = "Finger Print Process"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(95, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.errorRichBox
        Me.LayoutControlItem4.CustomizationFormText = "Error Log"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 464)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(425, 245)
        Me.LayoutControlItem4.Text = "Error Log"
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(95, 13)
        '
        'Img
        '
        Me.Img.ImageSize = New System.Drawing.Size(144, 144)
        Me.Img.ImageStream = CType(resources.GetObject("Img.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.Img.Images.SetKeyName(0, "red-success-144.png")
        Me.Img.Images.SetKeyName(1, "green-success-144.png")
        '
        'TimerAtten
        '
        Me.TimerAtten.Enabled = True
        '
        'FrmFingerScanner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "FrmFingerScanner"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Finger Scanner"
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl1.ResumeLayout(false)
        CType(Me.PanelControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.PanelControl1.ResumeLayout(false)
        Me.PanelControl1.PerformLayout
        CType(Me.lblempname.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.lblempid.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.lblstatusimage.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.lblfingerimage.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridControlLog,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridViewLog,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridControlEmpHeader,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridViewEmpHeader,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Img,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblstatusimage As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents lblfingerimage As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents GridControlLog As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewLog As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridControlEmpHeader As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewEmpHeader As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnstartCapture As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btneveningout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnbreakin As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnbreakout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnmorningin As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Img As DevExpress.Utils.ImageCollection
    Friend WithEvents lblempname As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblempid As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblSessionmode As System.Windows.Forms.Label
    Friend WithEvents lblTimer As System.Windows.Forms.Label
    Friend WithEvents TimerAtten As System.Windows.Forms.Timer
    Friend WithEvents errorRichBox As System.Windows.Forms.RichTextBox
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lbltype As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFaceTrail As DevExpress.XtraEditors.SimpleButton
End Class
