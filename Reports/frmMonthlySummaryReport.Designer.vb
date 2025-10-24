<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMonthlySummaryReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMonthlySummaryReport))
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.btnprintreport = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrintPreview = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRefreshData = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ListBoxControlReportType = New DevExpress.XtraEditors.ListBoxControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.dtMonthYear = New DevExpress.XtraEditors.DateEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.StatusPanel = New DevExpress.XtraEditors.PanelControl()
        Me.lblTotalAmount = New DevExpress.XtraEditors.LabelControl()
        Me.lblFilterInfo = New DevExpress.XtraEditors.LabelControl()
        Me.lblStatus = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ListBoxControlReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtMonthYear.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtMonthYear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.GroupControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1301, 120)
        Me.PanelControl1.TabIndex = 0
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.btnprintreport)
        Me.GroupControl1.Controls.Add(Me.SimpleButton1)
        Me.GroupControl1.Controls.Add(Me.btnPrintPreview)
        Me.GroupControl1.Controls.Add(Me.btnRefreshData)
        Me.GroupControl1.Controls.Add(Me.btnSearch)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.ListBoxControlReportType)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.dtMonthYear)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1297, 116)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Report Parameters"
        '
        'btnprintreport
        '
        Me.btnprintreport.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnprintreport.Appearance.Options.UseFont = True
        Me.btnprintreport.Image = CType(resources.GetObject("btnprintreport.Image"), System.Drawing.Image)
        Me.btnprintreport.Location = New System.Drawing.Point(864, 35)
        Me.btnprintreport.Name = "btnprintreport"
        Me.btnprintreport.Size = New System.Drawing.Size(141, 48)
        Me.btnprintreport.TabIndex = 21
        Me.btnprintreport.Text = "Print Preview"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.SimpleButton1.Appearance.Options.UseFont = True
        Me.SimpleButton1.Image = CType(resources.GetObject("SimpleButton1.Image"), System.Drawing.Image)
        Me.SimpleButton1.Location = New System.Drawing.Point(1034, 35)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(115, 48)
        Me.SimpleButton1.TabIndex = 20
        Me.SimpleButton1.Text = "Export"
        '
        'btnPrintPreview
        '
        Me.btnPrintPreview.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrintPreview.Appearance.Options.UseFont = True
        Me.btnPrintPreview.Image = CType(resources.GetObject("btnPrintPreview.Image"), System.Drawing.Image)
        Me.btnPrintPreview.Location = New System.Drawing.Point(717, 35)
        Me.btnPrintPreview.Name = "btnPrintPreview"
        Me.btnPrintPreview.Size = New System.Drawing.Size(141, 48)
        Me.btnPrintPreview.TabIndex = 19
        Me.btnPrintPreview.Text = "Grid Preview"
        '
        'btnRefreshData
        '
        Me.btnRefreshData.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefreshData.Appearance.Options.UseFont = True
        Me.btnRefreshData.Image = CType(resources.GetObject("btnRefreshData.Image"), System.Drawing.Image)
        Me.btnRefreshData.Location = New System.Drawing.Point(1172, 35)
        Me.btnRefreshData.Name = "btnRefreshData"
        Me.btnRefreshData.Size = New System.Drawing.Size(115, 48)
        Me.btnRefreshData.TabIndex = 18
        Me.btnRefreshData.Text = "Refresh"
        '
        'btnSearch
        '
        Me.btnSearch.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnSearch.Appearance.Options.UseFont = True
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(596, 35)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(115, 48)
        Me.btnSearch.TabIndex = 17
        Me.btnSearch.Text = "Search"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(270, 38)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(64, 13)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Report Type:"
        '
        'ListBoxControlReportType
        '
        Me.ListBoxControlReportType.Location = New System.Drawing.Point(350, 35)
        Me.ListBoxControlReportType.Name = "ListBoxControlReportType"
        Me.ListBoxControlReportType.Size = New System.Drawing.Size(234, 70)
        Me.ListBoxControlReportType.TabIndex = 2
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(20, 38)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(66, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Month / Year:"
        '
        'dtMonthYear
        '
        Me.dtMonthYear.EditValue = Nothing
        Me.dtMonthYear.Location = New System.Drawing.Point(100, 35)
        Me.dtMonthYear.Name = "dtMonthYear"
        Me.dtMonthYear.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtMonthYear.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtMonthYear.Properties.CalendarTimeProperties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
        Me.dtMonthYear.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.[Default]
        Me.dtMonthYear.Properties.DisplayFormat.FormatString = "MMM yyyy"
        Me.dtMonthYear.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtMonthYear.Properties.EditFormat.FormatString = "MMM yyyy"
        Me.dtMonthYear.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtMonthYear.Properties.Mask.EditMask = "MMM yyyy"
        Me.dtMonthYear.Size = New System.Drawing.Size(150, 20)
        Me.dtMonthYear.TabIndex = 0
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.GridControl1)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 120)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1301, 503)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Report Data"
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControl1.Location = New System.Drawing.Point(2, 21)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(1297, 480)
        Me.GridControl1.TabIndex = 0
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FooterPanel.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.Row.Options.UseTextOptions = True
        Me.GridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsCustomization.AllowGroup = False
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.RowHeight = 25
        '
        'StatusPanel
        '
        Me.StatusPanel.Controls.Add(Me.lblTotalAmount)
        Me.StatusPanel.Controls.Add(Me.lblFilterInfo)
        Me.StatusPanel.Controls.Add(Me.lblStatus)
        Me.StatusPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.StatusPanel.Location = New System.Drawing.Point(0, 623)
        Me.StatusPanel.Name = "StatusPanel"
        Me.StatusPanel.Size = New System.Drawing.Size(1301, 30)
        Me.StatusPanel.TabIndex = 2
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.Location = New System.Drawing.Point(400, 8)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(93, 13)
        Me.lblTotalAmount.TabIndex = 2
        Me.lblTotalAmount.Text = "Total Amount: 0.00"
        '
        'lblFilterInfo
        '
        Me.lblFilterInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFilterInfo.Location = New System.Drawing.Point(1101, 8)
        Me.lblFilterInfo.Name = "lblFilterInfo"
        Me.lblFilterInfo.Size = New System.Drawing.Size(24, 13)
        Me.lblFilterInfo.TabIndex = 1
        Me.lblFilterInfo.Text = "Filter"
        '
        'lblStatus
        '
        Me.lblStatus.Location = New System.Drawing.Point(10, 8)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(31, 13)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "Ready"
        '
        'frmMonthlySummaryReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1301, 653)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.StatusPanel)
        Me.Name = "frmMonthlySummaryReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Monthly Summary Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.ListBoxControlReportType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtMonthYear.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtMonthYear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusPanel.ResumeLayout(False)
        Me.StatusPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtMonthYear As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ListBoxControlReportType As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents StatusPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblFilterInfo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTotalAmount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnprintreport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPrintPreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnRefreshData As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSearch As DevExpress.XtraEditors.SimpleButton

End Class
