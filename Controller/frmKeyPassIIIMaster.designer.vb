<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKeyPassIIIMaster
    Inherits System.Windows.Forms.Form

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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_numclear = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_ok = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_0 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_3 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_2 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_1 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_6 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_5 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_4 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_9 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_8 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_7 = New DevExpress.XtraEditors.SimpleButton()
        Me._TXTPASS = New DevExpress.XtraEditors.TextEdit()
        Me.lbltitle = New DevExpress.XtraEditors.LabelControl()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.btnfingerprint = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me._TXTPASS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnfingerprint, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_numclear, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_ok, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_0, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_3, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_2, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_6, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_5, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_4, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_9, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_8, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_7, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 86)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(282, 308)
        Me.TableLayoutPanel1.TabIndex = 28
        '
        'btn_numclear
        '
        Me.btn_numclear.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_numclear.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_numclear.Appearance.BorderColor = System.Drawing.Color.White
        Me.btn_numclear.Appearance.Font = New System.Drawing.Font("Tahoma", 23.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_numclear.Appearance.ForeColor = System.Drawing.Color.Red
        Me.btn_numclear.Appearance.Options.UseBackColor = True
        Me.btn_numclear.Appearance.Options.UseBorderColor = True
        Me.btn_numclear.Appearance.Options.UseFont = True
        Me.btn_numclear.Appearance.Options.UseForeColor = True
        Me.btn_numclear.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_numclear.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_numclear.Location = New System.Drawing.Point(191, 197)
        Me.btn_numclear.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_numclear.Name = "btn_numclear"
        Me.btn_numclear.Size = New System.Drawing.Size(86, 56)
        Me.btn_numclear.TabIndex = 14
        Me.btn_numclear.Text = "X"
        '
        'btn_ok
        '
        Me.btn_ok.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_ok.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_ok.Appearance.BorderColor = System.Drawing.Color.White
        Me.btn_ok.Appearance.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ok.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btn_ok.Appearance.Options.UseBackColor = True
        Me.btn_ok.Appearance.Options.UseBorderColor = True
        Me.btn_ok.Appearance.Options.UseFont = True
        Me.btn_ok.Appearance.Options.UseForeColor = True
        Me.btn_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_ok.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_ok.Location = New System.Drawing.Point(98, 197)
        Me.btn_ok.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_ok.Name = "btn_ok"
        Me.btn_ok.Size = New System.Drawing.Size(85, 56)
        Me.btn_ok.TabIndex = 13
        Me.btn_ok.Text = "OK"
        '
        'btn_0
        '
        Me.btn_0.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_0.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_0.Appearance.BorderColor = System.Drawing.Color.White
        Me.btn_0.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_0.Appearance.ForeColor = System.Drawing.Color.White
        Me.btn_0.Appearance.Options.UseBackColor = True
        Me.btn_0.Appearance.Options.UseBorderColor = True
        Me.btn_0.Appearance.Options.UseFont = True
        Me.btn_0.Appearance.Options.UseForeColor = True
        Me.btn_0.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_0.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_0.Location = New System.Drawing.Point(5, 197)
        Me.btn_0.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_0.Name = "btn_0"
        Me.btn_0.Size = New System.Drawing.Size(85, 56)
        Me.btn_0.TabIndex = 12
        Me.btn_0.Text = "0"
        '
        'btn_3
        '
        Me.btn_3.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_3.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_3.Appearance.BorderColor = System.Drawing.Color.White
        Me.btn_3.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_3.Appearance.ForeColor = System.Drawing.Color.White
        Me.btn_3.Appearance.Options.UseBackColor = True
        Me.btn_3.Appearance.Options.UseBorderColor = True
        Me.btn_3.Appearance.Options.UseFont = True
        Me.btn_3.Appearance.Options.UseForeColor = True
        Me.btn_3.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_3.Location = New System.Drawing.Point(191, 133)
        Me.btn_3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_3.Name = "btn_3"
        Me.btn_3.Size = New System.Drawing.Size(86, 56)
        Me.btn_3.TabIndex = 10
        Me.btn_3.Text = "3"
        '
        'btn_2
        '
        Me.btn_2.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_2.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_2.Appearance.BorderColor = System.Drawing.Color.White
        Me.btn_2.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_2.Appearance.ForeColor = System.Drawing.Color.White
        Me.btn_2.Appearance.Options.UseBackColor = True
        Me.btn_2.Appearance.Options.UseBorderColor = True
        Me.btn_2.Appearance.Options.UseFont = True
        Me.btn_2.Appearance.Options.UseForeColor = True
        Me.btn_2.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_2.Location = New System.Drawing.Point(98, 133)
        Me.btn_2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_2.Name = "btn_2"
        Me.btn_2.Size = New System.Drawing.Size(85, 56)
        Me.btn_2.TabIndex = 9
        Me.btn_2.Text = "2"
        '
        'btn_1
        '
        Me.btn_1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_1.Appearance.BorderColor = System.Drawing.Color.White
        Me.btn_1.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_1.Appearance.ForeColor = System.Drawing.Color.White
        Me.btn_1.Appearance.Options.UseBackColor = True
        Me.btn_1.Appearance.Options.UseBorderColor = True
        Me.btn_1.Appearance.Options.UseFont = True
        Me.btn_1.Appearance.Options.UseForeColor = True
        Me.btn_1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_1.Location = New System.Drawing.Point(5, 133)
        Me.btn_1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_1.Name = "btn_1"
        Me.btn_1.Size = New System.Drawing.Size(85, 56)
        Me.btn_1.TabIndex = 8
        Me.btn_1.Text = "1"
        '
        'btn_6
        '
        Me.btn_6.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_6.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_6.Appearance.BorderColor = System.Drawing.Color.White
        Me.btn_6.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_6.Appearance.ForeColor = System.Drawing.Color.White
        Me.btn_6.Appearance.Options.UseBackColor = True
        Me.btn_6.Appearance.Options.UseBorderColor = True
        Me.btn_6.Appearance.Options.UseFont = True
        Me.btn_6.Appearance.Options.UseForeColor = True
        Me.btn_6.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_6.Location = New System.Drawing.Point(191, 69)
        Me.btn_6.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_6.Name = "btn_6"
        Me.btn_6.Size = New System.Drawing.Size(86, 56)
        Me.btn_6.TabIndex = 6
        Me.btn_6.Text = "6"
        '
        'btn_5
        '
        Me.btn_5.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_5.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_5.Appearance.BorderColor = System.Drawing.Color.White
        Me.btn_5.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_5.Appearance.ForeColor = System.Drawing.Color.White
        Me.btn_5.Appearance.Options.UseBackColor = True
        Me.btn_5.Appearance.Options.UseBorderColor = True
        Me.btn_5.Appearance.Options.UseFont = True
        Me.btn_5.Appearance.Options.UseForeColor = True
        Me.btn_5.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_5.Location = New System.Drawing.Point(98, 69)
        Me.btn_5.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_5.Name = "btn_5"
        Me.btn_5.Size = New System.Drawing.Size(85, 56)
        Me.btn_5.TabIndex = 5
        Me.btn_5.Text = "5"
        '
        'btn_4
        '
        Me.btn_4.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_4.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_4.Appearance.BorderColor = System.Drawing.Color.White
        Me.btn_4.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_4.Appearance.ForeColor = System.Drawing.Color.White
        Me.btn_4.Appearance.Options.UseBackColor = True
        Me.btn_4.Appearance.Options.UseBorderColor = True
        Me.btn_4.Appearance.Options.UseFont = True
        Me.btn_4.Appearance.Options.UseForeColor = True
        Me.btn_4.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_4.Location = New System.Drawing.Point(5, 69)
        Me.btn_4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_4.Name = "btn_4"
        Me.btn_4.Size = New System.Drawing.Size(85, 56)
        Me.btn_4.TabIndex = 4
        Me.btn_4.Text = "4"
        '
        'btn_9
        '
        Me.btn_9.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_9.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_9.Appearance.BorderColor = System.Drawing.Color.White
        Me.btn_9.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_9.Appearance.ForeColor = System.Drawing.Color.White
        Me.btn_9.Appearance.Options.UseBackColor = True
        Me.btn_9.Appearance.Options.UseBorderColor = True
        Me.btn_9.Appearance.Options.UseFont = True
        Me.btn_9.Appearance.Options.UseForeColor = True
        Me.btn_9.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_9.Location = New System.Drawing.Point(191, 5)
        Me.btn_9.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_9.Name = "btn_9"
        Me.btn_9.Size = New System.Drawing.Size(86, 56)
        Me.btn_9.TabIndex = 2
        Me.btn_9.Text = "9"
        '
        'btn_8
        '
        Me.btn_8.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_8.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_8.Appearance.BorderColor = System.Drawing.Color.White
        Me.btn_8.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_8.Appearance.ForeColor = System.Drawing.Color.White
        Me.btn_8.Appearance.Options.UseBackColor = True
        Me.btn_8.Appearance.Options.UseBorderColor = True
        Me.btn_8.Appearance.Options.UseFont = True
        Me.btn_8.Appearance.Options.UseForeColor = True
        Me.btn_8.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_8.Location = New System.Drawing.Point(98, 5)
        Me.btn_8.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_8.Name = "btn_8"
        Me.btn_8.Size = New System.Drawing.Size(85, 56)
        Me.btn_8.TabIndex = 1
        Me.btn_8.Text = "8"
        '
        'btn_7
        '
        Me.btn_7.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_7.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btn_7.Appearance.BorderColor = System.Drawing.Color.White
        Me.btn_7.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_7.Appearance.ForeColor = System.Drawing.Color.White
        Me.btn_7.Appearance.Options.UseBackColor = True
        Me.btn_7.Appearance.Options.UseBorderColor = True
        Me.btn_7.Appearance.Options.UseFont = True
        Me.btn_7.Appearance.Options.UseForeColor = True
        Me.btn_7.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_7.Location = New System.Drawing.Point(5, 5)
        Me.btn_7.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_7.Name = "btn_7"
        Me.btn_7.Size = New System.Drawing.Size(85, 56)
        Me.btn_7.TabIndex = 0
        Me.btn_7.Text = "7"
        '
        '_TXTPASS
        '
        Me._TXTPASS.Location = New System.Drawing.Point(12, 47)
        Me._TXTPASS.Name = "_TXTPASS"
        Me._TXTPASS.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._TXTPASS.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold)
        Me._TXTPASS.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me._TXTPASS.Properties.Appearance.Options.UseBackColor = True
        Me._TXTPASS.Properties.Appearance.Options.UseFont = True
        Me._TXTPASS.Properties.Appearance.Options.UseForeColor = True
        Me._TXTPASS.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me._TXTPASS.Properties.UseSystemPasswordChar = True
        Me._TXTPASS.Size = New System.Drawing.Size(283, 40)
        Me._TXTPASS.TabIndex = 27
        '
        'lbltitle
        '
        Me.lbltitle.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.lbltitle.Location = New System.Drawing.Point(12, 8)
        Me.lbltitle.Margin = New System.Windows.Forms.Padding(2)
        Me.lbltitle.Name = "lbltitle"
        Me.lbltitle.Size = New System.Drawing.Size(61, 24)
        Me.lbltitle.TabIndex = 29
        Me.lbltitle.Text = "lblTitle"
        '
        'btnClose
        '
        Me.btnClose.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btnClose.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btnClose.Appearance.BorderColor = System.Drawing.Color.White
        Me.btnClose.Appearance.Font = New System.Drawing.Font("Tahoma", 23.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Appearance.ForeColor = System.Drawing.Color.Yellow
        Me.btnClose.Appearance.Options.UseBackColor = True
        Me.btnClose.Appearance.Options.UseBorderColor = True
        Me.btnClose.Appearance.Options.UseFont = True
        Me.btnClose.Appearance.Options.UseForeColor = True
        Me.btnClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btnClose.Location = New System.Drawing.Point(264, 2)
        Me.btnClose.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(45, 39)
        Me.btnClose.TabIndex = 30
        Me.btnClose.Text = "X"
        '
        'btnfingerprint
        '
        Me.btnfingerprint.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btnfingerprint.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.btnfingerprint.Appearance.BorderColor = System.Drawing.Color.White
        Me.btnfingerprint.Appearance.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnfingerprint.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnfingerprint.Appearance.Options.UseBackColor = True
        Me.btnfingerprint.Appearance.Options.UseBorderColor = True
        Me.btnfingerprint.Appearance.Options.UseFont = True
        Me.btnfingerprint.Appearance.Options.UseForeColor = True
        Me.btnfingerprint.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.TableLayoutPanel1.SetColumnSpan(Me.btnfingerprint, 2)
        Me.btnfingerprint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnfingerprint.Location = New System.Drawing.Point(5, 261)
        Me.btnfingerprint.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btnfingerprint.Name = "btnfingerprint"
        Me.btnfingerprint.Size = New System.Drawing.Size(178, 42)
        Me.btnfingerprint.TabIndex = 15
        Me.btnfingerprint.Text = "FingerPrint"
        '
        'frmKeyPassIIIMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(311, 406)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lbltitle)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me._TXTPASS)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmKeyPassIIIMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmKeyPass"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me._TXTPASS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btn_numclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_ok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_0 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_3 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_6 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_5 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_4 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_9 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_8 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_7 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents _TXTPASS As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lbltitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnfingerprint As DevExpress.XtraEditors.SimpleButton
End Class
