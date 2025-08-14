<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintProfile
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintProfile))
        Me.btnBack = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnnew = New DevExpress.XtraEditors.SimpleButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtfilename = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtproname = New System.Windows.Forms.TextBox()
        Me.txtCmbtype = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtsinglemulti = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.chkActive = New DevExpress.XtraEditors.CheckButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblmode = New System.Windows.Forms.Label()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCmbtype.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsinglemulti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnBack
        '
        Me.btnBack.Appearance.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnBack.Appearance.BackColor2 = System.Drawing.SystemColors.ActiveCaption
        Me.btnBack.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnBack.Appearance.Options.UseBackColor = True
        Me.btnBack.Appearance.Options.UseFont = True
        Me.btnBack.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
        Me.btnBack.Image = CType(resources.GetObject("btnBack.Image"), System.Drawing.Image)
        Me.btnBack.Location = New System.Drawing.Point(7, 580)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(147, 65)
        Me.btnBack.TabIndex = 0
        Me.btnBack.Text = "Back "
        '
        'GridControl1
        '
        Me.GridControl1.Location = New System.Drawing.Point(3, 144)
        Me.GridControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
        Me.GridControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(947, 430)
        Me.GridControl1.TabIndex = 6
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Teal
        Me.GridView1.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(1, Byte), True)
        Me.GridView1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White
        Me.GridView1.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.GridView1.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.GridView1.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.Row.Options.UseTextOptions = True
        Me.GridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView1.ColumnPanelRowHeight = 30
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.RowHeight = 25
        '
        'btnAdd
        '
        Me.btnAdd.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.btnAdd.Appearance.Options.UseFont = True
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.Location = New System.Drawing.Point(852, 88)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(94, 48)
        Me.btnAdd.TabIndex = 7
        Me.btnAdd.Text = "Add"
        '
        'btnDel
        '
        Me.btnDel.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.btnDel.Appearance.Options.UseFont = True
        Me.btnDel.Image = CType(resources.GetObject("btnDel.Image"), System.Drawing.Image)
        Me.btnDel.Location = New System.Drawing.Point(862, 580)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(82, 38)
        Me.btnDel.TabIndex = 8
        Me.btnDel.Text = "Del"
        '
        'btnnew
        '
        Me.btnnew.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.btnnew.Appearance.Options.UseFont = True
        Me.btnnew.Image = CType(resources.GetObject("btnnew.Image"), System.Drawing.Image)
        Me.btnnew.Location = New System.Drawing.Point(751, 88)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(94, 48)
        Me.btnnew.TabIndex = 9
        Me.btnnew.Text = "New"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(5, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 26)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Print Profile"
        '
        'txtId
        '
        Me.txtId.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.txtId.Location = New System.Drawing.Point(184, 39)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(79, 27)
        Me.txtId.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.DimGray
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(58, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(3)
        Me.Label2.Size = New System.Drawing.Size(104, 27)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Profile Id :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.DimGray
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(401, 109)
        Me.Label3.Name = "Label3"
        Me.Label3.Padding = New System.Windows.Forms.Padding(3)
        Me.Label3.Size = New System.Drawing.Size(127, 27)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Profile Type :"
        '
        'txtfilename
        '
        Me.txtfilename.Font = New System.Drawing.Font("Tahoma", 13.0!)
        Me.txtfilename.Location = New System.Drawing.Point(550, 75)
        Me.txtfilename.Name = "txtfilename"
        Me.txtfilename.Size = New System.Drawing.Size(195, 28)
        Me.txtfilename.TabIndex = 13
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.DimGray
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(28, 75)
        Me.Label4.Name = "Label4"
        Me.Label4.Padding = New System.Windows.Forms.Padding(3)
        Me.Label4.Size = New System.Drawing.Size(134, 27)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Profile Name :"
        '
        'txtproname
        '
        Me.txtproname.Font = New System.Drawing.Font("Tahoma", 13.0!)
        Me.txtproname.Location = New System.Drawing.Point(184, 73)
        Me.txtproname.Name = "txtproname"
        Me.txtproname.Size = New System.Drawing.Size(195, 28)
        Me.txtproname.TabIndex = 15
        '
        'txtCmbtype
        '
        Me.txtCmbtype.Location = New System.Drawing.Point(550, 109)
        Me.txtCmbtype.Name = "txtCmbtype"
        Me.txtCmbtype.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 13.0!)
        Me.txtCmbtype.Properties.Appearance.Options.UseFont = True
        Me.txtCmbtype.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtCmbtype.Properties.Items.AddRange(New Object() {"Sales", "Guest Print", "Purchase", "Inventory", "Shift Close", "Day Close", "Tax Report", "Payouts", "Tax Cut Report"})
        Me.txtCmbtype.Size = New System.Drawing.Size(195, 28)
        Me.txtCmbtype.TabIndex = 17
        '
        'txtsinglemulti
        '
        Me.txtsinglemulti.Location = New System.Drawing.Point(184, 108)
        Me.txtsinglemulti.Name = "txtsinglemulti"
        Me.txtsinglemulti.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 13.0!)
        Me.txtsinglemulti.Properties.Appearance.Options.UseFont = True
        Me.txtsinglemulti.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtsinglemulti.Properties.Items.AddRange(New Object() {"Print", "Mail"})
        Me.txtsinglemulti.Size = New System.Drawing.Size(195, 28)
        Me.txtsinglemulti.TabIndex = 18
        '
        'chkActive
        '
        Me.chkActive.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkActive.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.chkActive.Appearance.ForeColor = System.Drawing.Color.Purple
        Me.chkActive.Appearance.Options.UseBackColor = True
        Me.chkActive.Appearance.Options.UseFont = True
        Me.chkActive.Appearance.Options.UseForeColor = True
        Me.chkActive.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.chkActive.Checked = True
        Me.chkActive.Location = New System.Drawing.Point(264, 39)
        Me.chkActive.LookAndFeel.UseDefaultLookAndFeel = False
        Me.chkActive.LookAndFeel.UseWindowsXPTheme = True
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(82, 28)
        Me.chkActive.TabIndex = 19
        Me.chkActive.Text = "Active"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.DimGray
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(411, 75)
        Me.Label5.Name = "Label5"
        Me.Label5.Padding = New System.Windows.Forms.Padding(3)
        Me.Label5.Size = New System.Drawing.Size(114, 27)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "File  Name :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.DimGray
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(-1, 109)
        Me.Label6.Name = "Label6"
        Me.Label6.Padding = New System.Windows.Forms.Padding(3)
        Me.Label6.Size = New System.Drawing.Size(163, 27)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Single / Multiple :"
        '
        'lblmode
        '
        Me.lblmode.AutoSize = True
        Me.lblmode.ForeColor = System.Drawing.Color.White
        Me.lblmode.Location = New System.Drawing.Point(549, 11)
        Me.lblmode.Name = "lblmode"
        Me.lblmode.Size = New System.Drawing.Size(11, 13)
        Me.lblmode.TabIndex = 22
        Me.lblmode.Text = "."
        '
        'frmPrintProfile
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(951, 647)
        Me.Controls.Add(Me.lblmode)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.chkActive)
        Me.Controls.Add(Me.txtsinglemulti)
        Me.Controls.Add(Me.txtCmbtype)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtproname)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtfilename)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnnew)
        Me.Controls.Add(Me.btnDel)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.btnBack)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmPrintProfile"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmPCsettings"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCmbtype.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsinglemulti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBack As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnnew As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtfilename As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtproname As System.Windows.Forms.TextBox
    Friend WithEvents txtCmbtype As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtsinglemulti As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents chkActive As DevExpress.XtraEditors.CheckButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblmode As System.Windows.Forms.Label
End Class
