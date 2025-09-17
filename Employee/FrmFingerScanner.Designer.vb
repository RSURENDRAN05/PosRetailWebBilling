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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.lblTimer = New System.Windows.Forms.Label()
        Me.txtempname = New DevExpress.XtraEditors.TextEdit()
        Me.txtempid = New DevExpress.XtraEditors.TextEdit()
        Me.lblmade = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnstopenroll = New DevExpress.XtraEditors.SimpleButton()
        Me.btnstartenroll = New DevExpress.XtraEditors.SimpleButton()
        Me.btneveningout = New DevExpress.XtraEditors.SimpleButton()
        Me.btnbreakin = New DevExpress.XtraEditors.SimpleButton()
        Me.btnbreakout = New DevExpress.XtraEditors.SimpleButton()
        Me.btnmorningin = New DevExpress.XtraEditors.SimpleButton()
        Me.lblimagestatus = New DevExpress.XtraEditors.PictureEdit()
        Me.lblfingerimage = New DevExpress.XtraEditors.PictureEdit()
        Me.GridControlLog = New DevExpress.XtraGrid.GridControl()
        Me.GridViewLog = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridControlEmpHeader = New DevExpress.XtraGrid.GridControl()
        Me.GridViewHeader = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.Img = New DevExpress.Utils.ImageCollection(Me.components)
        Me.TimerAtten = New System.Windows.Forms.Timer(Me.components)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.txtempname.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtempid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblimagestatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfingerimage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControlLog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewLog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControlEmpHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Img, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
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
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.lblTimer)
        Me.PanelControl1.Controls.Add(Me.txtempname)
        Me.PanelControl1.Controls.Add(Me.txtempid)
        Me.PanelControl1.Controls.Add(Me.lblmade)
        Me.PanelControl1.Controls.Add(Me.Label3)
        Me.PanelControl1.Controls.Add(Me.Label2)
        Me.PanelControl1.Controls.Add(Me.Label1)
        Me.PanelControl1.Controls.Add(Me.btnstopenroll)
        Me.PanelControl1.Controls.Add(Me.btnstartenroll)
        Me.PanelControl1.Controls.Add(Me.btneveningout)
        Me.PanelControl1.Controls.Add(Me.btnbreakin)
        Me.PanelControl1.Controls.Add(Me.btnbreakout)
        Me.PanelControl1.Controls.Add(Me.btnmorningin)
        Me.PanelControl1.Controls.Add(Me.lblimagestatus)
        Me.PanelControl1.Controls.Add(Me.lblfingerimage)
        Me.PanelControl1.Location = New System.Drawing.Point(437, 28)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(559, 346)
        Me.PanelControl1.TabIndex = 6
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
        'txtempname
        '
        Me.txtempname.Enabled = False
        Me.txtempname.Location = New System.Drawing.Point(202, 144)
        Me.txtempname.Name = "txtempname"
        Me.txtempname.Size = New System.Drawing.Size(172, 20)
        Me.txtempname.TabIndex = 13
        '
        'txtempid
        '
        Me.txtempid.Enabled = False
        Me.txtempid.Location = New System.Drawing.Point(202, 112)
        Me.txtempid.Name = "txtempid"
        Me.txtempid.Size = New System.Drawing.Size(43, 20)
        Me.txtempid.TabIndex = 12
        '
        'lblmade
        '
        Me.lblmade.AutoSize = True
        Me.lblmade.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblmade.ForeColor = System.Drawing.Color.Blue
        Me.lblmade.Location = New System.Drawing.Point(198, 46)
        Me.lblmade.Name = "lblmade"
        Me.lblmade.Size = New System.Drawing.Size(91, 24)
        Me.lblmade.TabIndex = 11
        Me.lblmade.Text = "lblmode"
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
        'btnstopenroll
        '
        Me.btnstopenroll.Appearance.BackColor = System.Drawing.Color.Maroon
        Me.btnstopenroll.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnstopenroll.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnstopenroll.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnstopenroll.Appearance.Options.UseBackColor = True
        Me.btnstopenroll.Appearance.Options.UseFont = True
        Me.btnstopenroll.Appearance.Options.UseForeColor = True
        Me.btnstopenroll.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.btnstopenroll.Location = New System.Drawing.Point(306, 177)
        Me.btnstopenroll.Name = "btnstopenroll"
        Me.btnstopenroll.Size = New System.Drawing.Size(151, 66)
        Me.btnstopenroll.TabIndex = 7
        Me.btnstopenroll.Text = "Stop Enroll"
        '
        'btnstartenroll
        '
        Me.btnstartenroll.Appearance.BackColor = System.Drawing.Color.Maroon
        Me.btnstartenroll.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnstartenroll.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnstartenroll.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnstartenroll.Appearance.Options.UseBackColor = True
        Me.btnstartenroll.Appearance.Options.UseFont = True
        Me.btnstartenroll.Appearance.Options.UseForeColor = True
        Me.btnstartenroll.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.btnstartenroll.Location = New System.Drawing.Point(149, 177)
        Me.btnstartenroll.Name = "btnstartenroll"
        Me.btnstartenroll.Size = New System.Drawing.Size(151, 66)
        Me.btnstartenroll.TabIndex = 6
        Me.btnstartenroll.Text = "Start Enroll"
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
        'lblimagestatus
        '
        Me.lblimagestatus.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblimagestatus.EditValue = CType(resources.GetObject("lblimagestatus.EditValue"), Object)
        Me.lblimagestatus.Location = New System.Drawing.Point(2, 249)
        Me.lblimagestatus.Name = "lblimagestatus"
        Me.lblimagestatus.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblimagestatus.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblimagestatus.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblimagestatus.Properties.Appearance.Options.UseBackColor = True
        Me.lblimagestatus.Properties.Appearance.Options.UseFont = True
        Me.lblimagestatus.Properties.Appearance.Options.UseForeColor = True
        Me.lblimagestatus.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray
        Me.lblimagestatus.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        Me.lblimagestatus.Size = New System.Drawing.Size(555, 95)
        Me.lblimagestatus.TabIndex = 1
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
        Me.GridControlEmpHeader.MainView = Me.GridViewHeader
        Me.GridControlEmpHeader.Name = "GridControlEmpHeader"
        Me.GridControlEmpHeader.Size = New System.Drawing.Size(421, 689)
        Me.GridControlEmpHeader.TabIndex = 4
        Me.GridControlEmpHeader.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewHeader})
        '
        'GridViewHeader
        '
        Me.GridViewHeader.GridControl = Me.GridControlEmpHeader
        Me.GridViewHeader.Name = "GridViewHeader"
        Me.GridViewHeader.OptionsView.ShowGroupPanel = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3})
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
        Me.LayoutControlItem1.Size = New System.Drawing.Size(425, 709)
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
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "FrmFingerScanner"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Finger Scanner"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.txtempname.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtempid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblimagestatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfingerimage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControlLog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewLog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControlEmpHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Img, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblimagestatus As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents lblfingerimage As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents GridControlLog As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewLog As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridControlEmpHeader As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewHeader As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnstopenroll As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnstartenroll As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btneveningout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnbreakin As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnbreakout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnmorningin As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Img As DevExpress.Utils.ImageCollection
    Friend WithEvents txtempname As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtempid As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblmade As System.Windows.Forms.Label
    Friend WithEvents lblTimer As System.Windows.Forms.Label
    Friend WithEvents TimerAtten As System.Windows.Forms.Timer
End Class
