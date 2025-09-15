<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMultiplePriceSelection
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMultiplePriceSelection))
        Me.GridControlPrices = New DevExpress.XtraGrid.GridControl()
        Me.GridViewPrices = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.btnOk = New DevExpress.XtraEditors.SimpleButton()
        Me._TXTPASS = New DevExpress.XtraEditors.TextEdit()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_numclear = New DevExpress.XtraEditors.SimpleButton()
        Me.btn_dot = New DevExpress.XtraEditors.SimpleButton()
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSelectPrice = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.GridControlPrices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewPrices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me._TXTPASS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridControlPrices
        '
        Me.GridControlPrices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridControlPrices.Location = New System.Drawing.Point(12, 35)
        Me.GridControlPrices.MainView = Me.GridViewPrices
        Me.GridControlPrices.Name = "GridControlPrices"
        Me.GridControlPrices.Size = New System.Drawing.Size(307, 305)
        Me.GridControlPrices.TabIndex = 0
        Me.GridControlPrices.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewPrices})
        '
        'GridViewPrices
        '
        Me.GridViewPrices.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridViewPrices.GridControl = Me.GridControlPrices
        Me.GridViewPrices.Name = "GridViewPrices"
        Me.GridViewPrices.OptionsBehavior.Editable = False
        Me.GridViewPrices.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridViewPrices.OptionsView.ShowGroupPanel = False
        Me.GridViewPrices.RowHeight = 40
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.btnOk)
        Me.LayoutControl1.Controls.Add(Me._TXTPASS)
        Me.LayoutControl1.Controls.Add(Me.TableLayoutPanel1)
        Me.LayoutControl1.Controls.Add(Me.LabelControl1)
        Me.LayoutControl1.Controls.Add(Me.btnCancel)
        Me.LayoutControl1.Controls.Add(Me.btnSelectPrice)
        Me.LayoutControl1.Controls.Add(Me.GridControlPrices)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(581, 394)
        Me.LayoutControl1.TabIndex = 5
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'btnOk
        '
        Me.btnOk.Appearance.BackColor = System.Drawing.Color.White
        Me.btnOk.Appearance.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold)
        Me.btnOk.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnOk.Appearance.Options.UseBackColor = True
        Me.btnOk.Appearance.Options.UseFont = True
        Me.btnOk.Appearance.Options.UseForeColor = True
        Me.btnOk.Image = CType(resources.GetObject("btnOk.Image"), System.Drawing.Image)
        Me.btnOk.Location = New System.Drawing.Point(323, 300)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(246, 40)
        Me.btnOk.StyleController = Me.LayoutControl1
        Me.btnOk.TabIndex = 16
        Me.btnOk.Text = "Ok"
        '
        '_TXTPASS
        '
        Me._TXTPASS.Location = New System.Drawing.Point(323, 35)
        Me._TXTPASS.Name = "_TXTPASS"
        Me._TXTPASS.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold)
        Me._TXTPASS.Properties.Appearance.Options.UseFont = True
        Me._TXTPASS.Size = New System.Drawing.Size(246, 40)
        Me._TXTPASS.StyleController = Me.LayoutControl1
        Me._TXTPASS.TabIndex = 14
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btn_numclear, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_dot, 1, 3)
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(323, 79)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(246, 217)
        Me.TableLayoutPanel1.TabIndex = 15
        '
        'btn_numclear
        '
        Me.btn_numclear.Appearance.BackColor = System.Drawing.Color.Red
        Me.btn_numclear.Appearance.BackColor2 = System.Drawing.Color.Red
        Me.btn_numclear.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_numclear.Appearance.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold)
        Me.btn_numclear.Appearance.ForeColor = System.Drawing.Color.Yellow
        Me.btn_numclear.Appearance.Options.UseBackColor = True
        Me.btn_numclear.Appearance.Options.UseBorderColor = True
        Me.btn_numclear.Appearance.Options.UseFont = True
        Me.btn_numclear.Appearance.Options.UseForeColor = True
        Me.btn_numclear.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_numclear.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_numclear.Location = New System.Drawing.Point(166, 166)
        Me.btn_numclear.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_numclear.Name = "btn_numclear"
        Me.btn_numclear.Size = New System.Drawing.Size(76, 47)
        Me.btn_numclear.TabIndex = 14
        Me.btn_numclear.Text = "X"
        '
        'btn_dot
        '
        Me.btn_dot.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_dot.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_dot.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_dot.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_dot.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_dot.Appearance.Options.UseBackColor = True
        Me.btn_dot.Appearance.Options.UseBorderColor = True
        Me.btn_dot.Appearance.Options.UseFont = True
        Me.btn_dot.Appearance.Options.UseForeColor = True
        Me.btn_dot.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_dot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_dot.Location = New System.Drawing.Point(85, 166)
        Me.btn_dot.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_dot.Name = "btn_dot"
        Me.btn_dot.Size = New System.Drawing.Size(74, 47)
        Me.btn_dot.TabIndex = 13
        Me.btn_dot.Text = "00"
        '
        'btn_0
        '
        Me.btn_0.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_0.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_0.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_0.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_0.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_0.Appearance.Options.UseBackColor = True
        Me.btn_0.Appearance.Options.UseBorderColor = True
        Me.btn_0.Appearance.Options.UseFont = True
        Me.btn_0.Appearance.Options.UseForeColor = True
        Me.btn_0.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_0.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_0.Location = New System.Drawing.Point(4, 166)
        Me.btn_0.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_0.Name = "btn_0"
        Me.btn_0.Size = New System.Drawing.Size(74, 47)
        Me.btn_0.TabIndex = 12
        Me.btn_0.Text = "0"
        '
        'btn_3
        '
        Me.btn_3.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_3.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_3.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_3.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_3.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_3.Appearance.Options.UseBackColor = True
        Me.btn_3.Appearance.Options.UseBorderColor = True
        Me.btn_3.Appearance.Options.UseFont = True
        Me.btn_3.Appearance.Options.UseForeColor = True
        Me.btn_3.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_3.Location = New System.Drawing.Point(166, 112)
        Me.btn_3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_3.Name = "btn_3"
        Me.btn_3.Size = New System.Drawing.Size(76, 47)
        Me.btn_3.TabIndex = 10
        Me.btn_3.Text = "3"
        '
        'btn_2
        '
        Me.btn_2.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_2.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_2.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_2.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_2.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_2.Appearance.Options.UseBackColor = True
        Me.btn_2.Appearance.Options.UseBorderColor = True
        Me.btn_2.Appearance.Options.UseFont = True
        Me.btn_2.Appearance.Options.UseForeColor = True
        Me.btn_2.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_2.Location = New System.Drawing.Point(85, 112)
        Me.btn_2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_2.Name = "btn_2"
        Me.btn_2.Size = New System.Drawing.Size(74, 47)
        Me.btn_2.TabIndex = 9
        Me.btn_2.Text = "2"
        '
        'btn_1
        '
        Me.btn_1.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_1.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_1.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_1.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_1.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_1.Appearance.Options.UseBackColor = True
        Me.btn_1.Appearance.Options.UseBorderColor = True
        Me.btn_1.Appearance.Options.UseFont = True
        Me.btn_1.Appearance.Options.UseForeColor = True
        Me.btn_1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_1.Location = New System.Drawing.Point(4, 112)
        Me.btn_1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_1.Name = "btn_1"
        Me.btn_1.Size = New System.Drawing.Size(74, 47)
        Me.btn_1.TabIndex = 8
        Me.btn_1.Text = "1"
        '
        'btn_6
        '
        Me.btn_6.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_6.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_6.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_6.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_6.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_6.Appearance.Options.UseBackColor = True
        Me.btn_6.Appearance.Options.UseBorderColor = True
        Me.btn_6.Appearance.Options.UseFont = True
        Me.btn_6.Appearance.Options.UseForeColor = True
        Me.btn_6.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_6.Location = New System.Drawing.Point(166, 58)
        Me.btn_6.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_6.Name = "btn_6"
        Me.btn_6.Size = New System.Drawing.Size(76, 47)
        Me.btn_6.TabIndex = 6
        Me.btn_6.Text = "6"
        '
        'btn_5
        '
        Me.btn_5.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_5.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_5.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_5.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_5.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_5.Appearance.Options.UseBackColor = True
        Me.btn_5.Appearance.Options.UseBorderColor = True
        Me.btn_5.Appearance.Options.UseFont = True
        Me.btn_5.Appearance.Options.UseForeColor = True
        Me.btn_5.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_5.Location = New System.Drawing.Point(85, 58)
        Me.btn_5.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_5.Name = "btn_5"
        Me.btn_5.Size = New System.Drawing.Size(74, 47)
        Me.btn_5.TabIndex = 5
        Me.btn_5.Text = "5"
        '
        'btn_4
        '
        Me.btn_4.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_4.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_4.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_4.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_4.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_4.Appearance.Options.UseBackColor = True
        Me.btn_4.Appearance.Options.UseBorderColor = True
        Me.btn_4.Appearance.Options.UseFont = True
        Me.btn_4.Appearance.Options.UseForeColor = True
        Me.btn_4.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_4.Location = New System.Drawing.Point(4, 58)
        Me.btn_4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_4.Name = "btn_4"
        Me.btn_4.Size = New System.Drawing.Size(74, 47)
        Me.btn_4.TabIndex = 4
        Me.btn_4.Text = "4"
        '
        'btn_9
        '
        Me.btn_9.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_9.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_9.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_9.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_9.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_9.Appearance.Options.UseBackColor = True
        Me.btn_9.Appearance.Options.UseBorderColor = True
        Me.btn_9.Appearance.Options.UseFont = True
        Me.btn_9.Appearance.Options.UseForeColor = True
        Me.btn_9.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_9.Location = New System.Drawing.Point(166, 4)
        Me.btn_9.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_9.Name = "btn_9"
        Me.btn_9.Size = New System.Drawing.Size(76, 47)
        Me.btn_9.TabIndex = 2
        Me.btn_9.Text = "9"
        '
        'btn_8
        '
        Me.btn_8.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_8.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_8.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_8.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_8.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_8.Appearance.Options.UseBackColor = True
        Me.btn_8.Appearance.Options.UseBorderColor = True
        Me.btn_8.Appearance.Options.UseFont = True
        Me.btn_8.Appearance.Options.UseForeColor = True
        Me.btn_8.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_8.Location = New System.Drawing.Point(85, 4)
        Me.btn_8.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_8.Name = "btn_8"
        Me.btn_8.Size = New System.Drawing.Size(74, 47)
        Me.btn_8.TabIndex = 1
        Me.btn_8.Text = "8"
        '
        'btn_7
        '
        Me.btn_7.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_7.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_7.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_7.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_7.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_7.Appearance.Options.UseBackColor = True
        Me.btn_7.Appearance.Options.UseBorderColor = True
        Me.btn_7.Appearance.Options.UseFont = True
        Me.btn_7.Appearance.Options.UseForeColor = True
        Me.btn_7.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_7.Location = New System.Drawing.Point(4, 4)
        Me.btn_7.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_7.Name = "btn_7"
        Me.btn_7.Size = New System.Drawing.Size(74, 47)
        Me.btn_7.TabIndex = 0
        Me.btn_7.Text = "7"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.DarkBlue
        Me.LabelControl1.Location = New System.Drawing.Point(12, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(167, 19)
        Me.LabelControl1.StyleController = Me.LayoutControl1
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Select Multiple Price"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnCancel.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Appearance.Options.UseBackColor = True
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.Appearance.Options.UseForeColor = True
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.Location = New System.Drawing.Point(323, 344)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(246, 38)
        Me.btnCancel.StyleController = Me.LayoutControl1
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        '
        'btnSelectPrice
        '
        Me.btnSelectPrice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelectPrice.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.btnSelectPrice.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSelectPrice.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnSelectPrice.Appearance.Options.UseBackColor = True
        Me.btnSelectPrice.Appearance.Options.UseFont = True
        Me.btnSelectPrice.Appearance.Options.UseForeColor = True
        Me.btnSelectPrice.Image = CType(resources.GetObject("btnSelectPrice.Image"), System.Drawing.Image)
        Me.btnSelectPrice.Location = New System.Drawing.Point(12, 344)
        Me.btnSelectPrice.Name = "btnSelectPrice"
        Me.btnSelectPrice.Size = New System.Drawing.Size(307, 38)
        Me.btnSelectPrice.StyleController = Me.LayoutControl1
        Me.btnSelectPrice.TabIndex = 2
        Me.btnSelectPrice.Text = "Select Price"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.LayoutControlItem6, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem1, Me.LayoutControlItem7, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(581, 394)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.GridControlPrices
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 23)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(311, 309)
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.LabelControl1
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(561, 23)
        Me.LayoutControlItem6.Text = "LayoutControlItem6"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextToControlDistance = 0
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.btnSelectPrice
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 332)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(311, 42)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.btnCancel
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(311, 332)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(250, 42)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.TableLayoutPanel1
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(311, 67)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(250, 221)
        Me.LayoutControlItem1.Text = "LayoutControlItem1"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me._TXTPASS
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(311, 23)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(250, 44)
        Me.LayoutControlItem7.Text = "LayoutControlItem7"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextToControlDistance = 0
        Me.LayoutControlItem7.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.btnOk
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(311, 288)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(250, 44)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'FrmMultiplePriceSelection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(581, 394)
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmMultiplePriceSelection"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Multiple Price Selection"
        CType(Me.GridControlPrices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewPrices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me._TXTPASS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridControlPrices As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewPrices As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnSelectPrice As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btn_numclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn_dot As DevExpress.XtraEditors.SimpleButton
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
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents _TXTPASS As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
End Class
