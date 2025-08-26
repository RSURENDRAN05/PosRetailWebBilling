<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShiftcloseII
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmShiftcloseII))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepPrintNoteCount = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtnum = New DevExpress.XtraEditors.TextEdit()
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
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.btnsubmit = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.btncancel = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.SimpleLabelItem1 = New DevExpress.XtraLayout.SimpleLabelItem()
        Me.lblstatus = New DevExpress.XtraLayout.SimpleLabelItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepPrintNoteCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtnum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleLabelItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblstatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Appearance.Control.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LayoutControl1.Appearance.Control.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LayoutControl1.Appearance.Control.Options.UseBackColor = True
        Me.LayoutControl1.Controls.Add(Me.btncancel)
        Me.LayoutControl1.Controls.Add(Me.btnsubmit)
        Me.LayoutControl1.Controls.Add(Me.GridControl1)
        Me.LayoutControl1.Controls.Add(Me.txtnum)
        Me.LayoutControl1.Controls.Add(Me.TableLayoutPanel1)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(658, 421, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1099, 762)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'GridControl1
        '
        Me.GridControl1.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GridControl1.Location = New System.Drawing.Point(12, 67)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepPrintNoteCount})
        Me.GridControl1.Size = New System.Drawing.Size(552, 683)
        Me.GridControl1.TabIndex = 28
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FooterPanel.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.Row.Options.UseTextOptions = True
        Me.GridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.RowHeight = 40
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "PrintNote"
        Me.GridColumn1.FieldName = "PrintNote"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 206
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Count X"
        Me.GridColumn2.ColumnEdit = Me.RepPrintNoteCount
        Me.GridColumn2.FieldName = "Count"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 210
        '
        'RepPrintNoteCount
        '
        Me.RepPrintNoteCount.AutoHeight = False
        Me.RepPrintNoteCount.Name = "RepPrintNoteCount"
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn3.Caption = "Amount"
        Me.GridColumn3.DisplayFormat.FormatString = "n2"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn3.FieldName = "Amount"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Amount", "{0:0.00}", 0.0R)})
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 257
        '
        'txtnum
        '
        Me.txtnum.Location = New System.Drawing.Point(568, 67)
        Me.txtnum.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtnum.Name = "txtnum"
        Me.txtnum.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 30.0!, System.Drawing.FontStyle.Bold)
        Me.txtnum.Properties.Appearance.Options.UseFont = True
        Me.txtnum.Properties.Mask.EditMask = "f0"
        Me.txtnum.Size = New System.Drawing.Size(519, 66)
        Me.txtnum.StyleController = Me.LayoutControl1
        Me.txtnum.TabIndex = 27
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(568, 156)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(519, 552)
        Me.TableLayoutPanel1.TabIndex = 26
        '
        'btn_numclear
        '
        Me.btn_numclear.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_numclear.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_numclear.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_numclear.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btn_numclear.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_numclear.Appearance.Options.UseBackColor = True
        Me.btn_numclear.Appearance.Options.UseBorderColor = True
        Me.btn_numclear.Appearance.Options.UseFont = True
        Me.btn_numclear.Appearance.Options.UseForeColor = True
        Me.btn_numclear.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_numclear.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_numclear.Location = New System.Drawing.Point(349, 417)
        Me.btn_numclear.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_numclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btn_numclear.Name = "btn_numclear"
        Me.btn_numclear.Size = New System.Drawing.Size(165, 129)
        Me.btn_numclear.TabIndex = 14
        Me.btn_numclear.Text = "C"
        '
        'btn_ok
        '
        Me.btn_ok.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_ok.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_ok.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_ok.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btn_ok.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_ok.Appearance.Options.UseBackColor = True
        Me.btn_ok.Appearance.Options.UseBorderColor = True
        Me.btn_ok.Appearance.Options.UseFont = True
        Me.btn_ok.Appearance.Options.UseForeColor = True
        Me.btn_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_ok.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_ok.Location = New System.Drawing.Point(177, 417)
        Me.btn_ok.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_ok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btn_ok.Name = "btn_ok"
        Me.btn_ok.Size = New System.Drawing.Size(164, 129)
        Me.btn_ok.TabIndex = 13
        Me.btn_ok.Text = "OK"
        '
        'btn_0
        '
        Me.btn_0.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_0.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_0.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_0.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btn_0.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_0.Appearance.Options.UseBackColor = True
        Me.btn_0.Appearance.Options.UseBorderColor = True
        Me.btn_0.Appearance.Options.UseFont = True
        Me.btn_0.Appearance.Options.UseForeColor = True
        Me.btn_0.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_0.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_0.Location = New System.Drawing.Point(5, 417)
        Me.btn_0.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_0.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btn_0.Name = "btn_0"
        Me.btn_0.Size = New System.Drawing.Size(164, 129)
        Me.btn_0.TabIndex = 12
        Me.btn_0.Text = "0"
        '
        'btn_3
        '
        Me.btn_3.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_3.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_3.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_3.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btn_3.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_3.Appearance.Options.UseBackColor = True
        Me.btn_3.Appearance.Options.UseBorderColor = True
        Me.btn_3.Appearance.Options.UseFont = True
        Me.btn_3.Appearance.Options.UseForeColor = True
        Me.btn_3.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_3.Location = New System.Drawing.Point(349, 280)
        Me.btn_3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btn_3.Name = "btn_3"
        Me.btn_3.Size = New System.Drawing.Size(165, 127)
        Me.btn_3.TabIndex = 10
        Me.btn_3.Text = "3"
        '
        'btn_2
        '
        Me.btn_2.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_2.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_2.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_2.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btn_2.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_2.Appearance.Options.UseBackColor = True
        Me.btn_2.Appearance.Options.UseBorderColor = True
        Me.btn_2.Appearance.Options.UseFont = True
        Me.btn_2.Appearance.Options.UseForeColor = True
        Me.btn_2.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_2.Location = New System.Drawing.Point(177, 280)
        Me.btn_2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btn_2.Name = "btn_2"
        Me.btn_2.Size = New System.Drawing.Size(164, 127)
        Me.btn_2.TabIndex = 9
        Me.btn_2.Text = "2"
        '
        'btn_1
        '
        Me.btn_1.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_1.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_1.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_1.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btn_1.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_1.Appearance.Options.UseBackColor = True
        Me.btn_1.Appearance.Options.UseBorderColor = True
        Me.btn_1.Appearance.Options.UseFont = True
        Me.btn_1.Appearance.Options.UseForeColor = True
        Me.btn_1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_1.Location = New System.Drawing.Point(5, 280)
        Me.btn_1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btn_1.Name = "btn_1"
        Me.btn_1.Size = New System.Drawing.Size(164, 127)
        Me.btn_1.TabIndex = 8
        Me.btn_1.Text = "1"
        '
        'btn_6
        '
        Me.btn_6.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_6.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_6.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_6.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btn_6.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_6.Appearance.Options.UseBackColor = True
        Me.btn_6.Appearance.Options.UseBorderColor = True
        Me.btn_6.Appearance.Options.UseFont = True
        Me.btn_6.Appearance.Options.UseForeColor = True
        Me.btn_6.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_6.Location = New System.Drawing.Point(349, 143)
        Me.btn_6.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btn_6.Name = "btn_6"
        Me.btn_6.Size = New System.Drawing.Size(165, 127)
        Me.btn_6.TabIndex = 6
        Me.btn_6.Text = "6"
        '
        'btn_5
        '
        Me.btn_5.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_5.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_5.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_5.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btn_5.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_5.Appearance.Options.UseBackColor = True
        Me.btn_5.Appearance.Options.UseBorderColor = True
        Me.btn_5.Appearance.Options.UseFont = True
        Me.btn_5.Appearance.Options.UseForeColor = True
        Me.btn_5.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_5.Location = New System.Drawing.Point(177, 143)
        Me.btn_5.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btn_5.Name = "btn_5"
        Me.btn_5.Size = New System.Drawing.Size(164, 127)
        Me.btn_5.TabIndex = 5
        Me.btn_5.Text = "5"
        '
        'btn_4
        '
        Me.btn_4.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_4.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_4.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_4.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btn_4.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_4.Appearance.Options.UseBackColor = True
        Me.btn_4.Appearance.Options.UseBorderColor = True
        Me.btn_4.Appearance.Options.UseFont = True
        Me.btn_4.Appearance.Options.UseForeColor = True
        Me.btn_4.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_4.Location = New System.Drawing.Point(5, 143)
        Me.btn_4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btn_4.Name = "btn_4"
        Me.btn_4.Size = New System.Drawing.Size(164, 127)
        Me.btn_4.TabIndex = 4
        Me.btn_4.Text = "4"
        '
        'btn_9
        '
        Me.btn_9.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_9.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_9.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_9.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btn_9.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_9.Appearance.Options.UseBackColor = True
        Me.btn_9.Appearance.Options.UseBorderColor = True
        Me.btn_9.Appearance.Options.UseFont = True
        Me.btn_9.Appearance.Options.UseForeColor = True
        Me.btn_9.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_9.Location = New System.Drawing.Point(349, 6)
        Me.btn_9.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btn_9.Name = "btn_9"
        Me.btn_9.Size = New System.Drawing.Size(165, 127)
        Me.btn_9.TabIndex = 2
        Me.btn_9.Text = "9"
        '
        'btn_8
        '
        Me.btn_8.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_8.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_8.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_8.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btn_8.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_8.Appearance.Options.UseBackColor = True
        Me.btn_8.Appearance.Options.UseBorderColor = True
        Me.btn_8.Appearance.Options.UseFont = True
        Me.btn_8.Appearance.Options.UseForeColor = True
        Me.btn_8.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_8.Location = New System.Drawing.Point(177, 6)
        Me.btn_8.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_8.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btn_8.Name = "btn_8"
        Me.btn_8.Size = New System.Drawing.Size(164, 127)
        Me.btn_8.TabIndex = 1
        Me.btn_8.Text = "8"
        '
        'btn_7
        '
        Me.btn_7.Appearance.BackColor = System.Drawing.Color.White
        Me.btn_7.Appearance.BackColor2 = System.Drawing.Color.White
        Me.btn_7.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_7.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btn_7.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_7.Appearance.Options.UseBackColor = True
        Me.btn_7.Appearance.Options.UseBorderColor = True
        Me.btn_7.Appearance.Options.UseFont = True
        Me.btn_7.Appearance.Options.UseForeColor = True
        Me.btn_7.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.btn_7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_7.Location = New System.Drawing.Point(5, 6)
        Me.btn_7.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.btn_7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btn_7.Name = "btn_7"
        Me.btn_7.Size = New System.Drawing.Size(164, 127)
        Me.btn_7.TabIndex = 0
        Me.btn_7.Text = "7"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseBackColor = True
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem1, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.SimpleLabelItem1, Me.lblstatus})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1099, 762)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.TableLayoutPanel1
        Me.LayoutControlItem2.CustomizationFormText = "Amount :"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(556, 125)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(523, 575)
        Me.LayoutControlItem2.Text = "Amount :"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(465, 16)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtnum
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(556, 55)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(523, 70)
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.GridControl1
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 55)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(556, 687)
        Me.LayoutControlItem1.Text = "LayoutControlItem1"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'btnsubmit
        '
        Me.btnsubmit.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnsubmit.Appearance.Options.UseFont = True
        Me.btnsubmit.Image = CType(resources.GetObject("btnsubmit.Image"), System.Drawing.Image)
        Me.btnsubmit.Location = New System.Drawing.Point(568, 712)
        Me.btnsubmit.Name = "btnsubmit"
        Me.btnsubmit.Size = New System.Drawing.Size(257, 38)
        Me.btnsubmit.StyleController = Me.LayoutControl1
        Me.btnsubmit.TabIndex = 29
        Me.btnsubmit.Text = "Submit"
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.btnsubmit
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(556, 700)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(261, 42)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'btncancel
        '
        Me.btncancel.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btncancel.Appearance.Options.UseFont = True
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(829, 712)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(258, 38)
        Me.btncancel.StyleController = Me.LayoutControl1
        Me.btncancel.TabIndex = 30
        Me.btncancel.Text = "Cancel"
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.btncancel
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(817, 700)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(262, 42)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'SimpleLabelItem1
        '
        Me.SimpleLabelItem1.AllowHotTrack = False
        Me.SimpleLabelItem1.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 25.0!, System.Drawing.FontStyle.Bold)
        Me.SimpleLabelItem1.AppearanceItemCaption.Options.UseFont = True
        Me.SimpleLabelItem1.CustomizationFormText = "Shift Close Print Note "
        Me.SimpleLabelItem1.Location = New System.Drawing.Point(0, 0)
        Me.SimpleLabelItem1.Name = "SimpleLabelItem1"
        Me.SimpleLabelItem1.Size = New System.Drawing.Size(539, 55)
        Me.SimpleLabelItem1.Text = "Shift Close Print Note "
        Me.SimpleLabelItem1.TextSize = New System.Drawing.Size(465, 51)
        '
        'lblstatus
        '
        Me.lblstatus.AllowHotTrack = False
        Me.lblstatus.AppearanceItemCaption.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblstatus.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.lblstatus.AppearanceItemCaption.ForeColor = System.Drawing.Color.Yellow
        Me.lblstatus.AppearanceItemCaption.Options.UseBackColor = True
        Me.lblstatus.AppearanceItemCaption.Options.UseFont = True
        Me.lblstatus.AppearanceItemCaption.Options.UseForeColor = True
        Me.lblstatus.CustomizationFormText = "Labellblstatus"
        Me.lblstatus.Location = New System.Drawing.Point(539, 0)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(540, 55)
        Me.lblstatus.Text = "Labellblstatus"
        Me.lblstatus.TextSize = New System.Drawing.Size(465, 16)
        '
        'frmShiftcloseII
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1099, 762)
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmShiftcloseII"
        Me.Text = "ShiftcloseII"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepPrintNoteCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtnum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleLabelItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblstatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
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
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtnum As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepPrintNoteCount As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btncancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnsubmit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents SimpleLabelItem1 As DevExpress.XtraLayout.SimpleLabelItem
    Friend WithEvents lblstatus As DevExpress.XtraLayout.SimpleLabelItem
End Class
