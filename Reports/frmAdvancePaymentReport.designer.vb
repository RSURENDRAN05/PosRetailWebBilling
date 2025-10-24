<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdvancePaymentReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAdvancePaymentReport))
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemCurrencyEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnAdvance = New DevExpress.XtraEditors.SimpleButton()
        Me.btnprintreport = New DevExpress.XtraEditors.SimpleButton()
        Me.ListBoxControlSalemanList = New DevExpress.XtraEditors.ListBoxControl()
        Me.ListBoxControlReportList = New DevExpress.XtraEditors.ListBoxControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ListBoxControlReportType = New DevExpress.XtraEditors.ListBoxControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.dtStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.dtEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.btnExport = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrintPreview = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.lblStatusResults = New DevExpress.XtraEditors.LabelControl()
        Me.lblFillterDetails = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.lblTotalRecords = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotalAmount = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCurrencyEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.ListBoxControlSalemanList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListBoxControlReportList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ListBoxControlReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControl1.Location = New System.Drawing.Point(0, 124)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCurrencyEdit1, Me.RepositoryItemDateEdit1})
        Me.GridControl1.Size = New System.Drawing.Size(1266, 499)
        Me.GridControl1.TabIndex = 0
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.DarkBlue
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.GridView1.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.GridView1.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.SelectedRow.BackColor = System.Drawing.Color.Orange
        Me.GridView1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black
        Me.GridView1.Appearance.SelectedRow.Options.UseBackColor = True
        Me.GridView1.Appearance.SelectedRow.Options.UseForeColor = True
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "psih_invoice_id", Nothing, "Count: {0}")})
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsSelection.MultiSelect = True
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.RowHeight = 30
        '
        'RepositoryItemCurrencyEdit1
        '
        Me.RepositoryItemCurrencyEdit1.Caption = "Check"
        Me.RepositoryItemCurrencyEdit1.Name = "RepositoryItemCurrencyEdit1"
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.CalendarTimeProperties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
        Me.RepositoryItemDateEdit1.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.[Default]
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.EditFormat.FormatString = "dd/MM/yyyy HH:mm:ss"
        Me.RepositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.Mask.EditMask = "dd/MM/yyyy HH:mm:ss"
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btnAdvance)
        Me.PanelControl1.Controls.Add(Me.btnprintreport)
        Me.PanelControl1.Controls.Add(Me.ListBoxControlSalemanList)
        Me.PanelControl1.Controls.Add(Me.ListBoxControlReportList)
        Me.PanelControl1.Controls.Add(Me.GroupControl1)
        Me.PanelControl1.Controls.Add(Me.btnExport)
        Me.PanelControl1.Controls.Add(Me.btnPrintPreview)
        Me.PanelControl1.Controls.Add(Me.btnRefresh)
        Me.PanelControl1.Controls.Add(Me.btnSearch)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1266, 124)
        Me.PanelControl1.TabIndex = 1
        '
        'btnAdvance
        '
        Me.btnAdvance.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnAdvance.Appearance.Options.UseFont = True
        Me.btnAdvance.Image = CType(resources.GetObject("btnAdvance.Image"), System.Drawing.Image)
        Me.btnAdvance.Location = New System.Drawing.Point(651, 84)
        Me.btnAdvance.Name = "btnAdvance"
        Me.btnAdvance.Size = New System.Drawing.Size(164, 36)
        Me.btnAdvance.TabIndex = 17
        Me.btnAdvance.Text = "Advance Pay"
        '
        'btnprintreport
        '
        Me.btnprintreport.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnprintreport.Appearance.Options.UseFont = True
        Me.btnprintreport.Image = CType(resources.GetObject("btnprintreport.Image"), System.Drawing.Image)
        Me.btnprintreport.Location = New System.Drawing.Point(921, 5)
        Me.btnprintreport.Name = "btnprintreport"
        Me.btnprintreport.Size = New System.Drawing.Size(115, 44)
        Me.btnprintreport.TabIndex = 16
        Me.btnprintreport.Text = "Print"
        '
        'ListBoxControlSalemanList
        '
        Me.ListBoxControlSalemanList.Location = New System.Drawing.Point(460, 5)
        Me.ListBoxControlSalemanList.Name = "ListBoxControlSalemanList"
        Me.ListBoxControlSalemanList.Size = New System.Drawing.Size(186, 115)
        Me.ListBoxControlSalemanList.TabIndex = 15
        '
        'ListBoxControlReportList
        '
        Me.ListBoxControlReportList.Location = New System.Drawing.Point(295, 5)
        Me.ListBoxControlReportList.Name = "ListBoxControlReportList"
        Me.ListBoxControlReportList.Size = New System.Drawing.Size(159, 115)
        Me.ListBoxControlReportList.TabIndex = 14
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.ListBoxControlReportType)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Controls.Add(Me.LabelControl4)
        Me.GroupControl1.Controls.Add(Me.dtStartDate)
        Me.GroupControl1.Controls.Add(Me.dtEndDate)
        Me.GroupControl1.Location = New System.Drawing.Point(5, 5)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(284, 114)
        Me.GroupControl1.TabIndex = 13
        Me.GroupControl1.Text = "Date Filter"
        '
        'ListBoxControlReportType
        '
        Me.ListBoxControlReportType.Location = New System.Drawing.Point(116, 46)
        Me.ListBoxControlReportType.Name = "ListBoxControlReportType"
        Me.ListBoxControlReportType.Size = New System.Drawing.Size(159, 63)
        Me.ListBoxControlReportType.TabIndex = 15
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(10, 58)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(67, 13)
        Me.LabelControl1.TabIndex = 10
        Me.LabelControl1.Text = "Report Type :"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(10, 27)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(31, 13)
        Me.LabelControl3.TabIndex = 4
        Me.LabelControl3.Text = "From :"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(148, 27)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(19, 13)
        Me.LabelControl4.TabIndex = 5
        Me.LabelControl4.Text = "To :"
        '
        'dtStartDate
        '
        Me.dtStartDate.EditValue = Nothing
        Me.dtStartDate.Location = New System.Drawing.Point(42, 24)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtStartDate.Properties.CalendarTimeProperties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
        Me.dtStartDate.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.[Default]
        Me.dtStartDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.dtStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtStartDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.dtStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtStartDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.dtStartDate.Size = New System.Drawing.Size(100, 20)
        Me.dtStartDate.TabIndex = 6
        '
        'dtEndDate
        '
        Me.dtEndDate.EditValue = Nothing
        Me.dtEndDate.Location = New System.Drawing.Point(175, 24)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtEndDate.Properties.CalendarTimeProperties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
        Me.dtEndDate.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.[Default]
        Me.dtEndDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.dtEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtEndDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.dtEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtEndDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.dtEndDate.Size = New System.Drawing.Size(100, 20)
        Me.dtEndDate.TabIndex = 7
        '
        'btnExport
        '
        Me.btnExport.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnExport.Appearance.Options.UseFont = True
        Me.btnExport.Image = CType(resources.GetObject("btnExport.Image"), System.Drawing.Image)
        Me.btnExport.Location = New System.Drawing.Point(1042, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(116, 44)
        Me.btnExport.TabIndex = 11
        Me.btnExport.Text = "Export"
        '
        'btnPrintPreview
        '
        Me.btnPrintPreview.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrintPreview.Appearance.Options.UseFont = True
        Me.btnPrintPreview.Image = CType(resources.GetObject("btnPrintPreview.Image"), System.Drawing.Image)
        Me.btnPrintPreview.Location = New System.Drawing.Point(767, 5)
        Me.btnPrintPreview.Name = "btnPrintPreview"
        Me.btnPrintPreview.Size = New System.Drawing.Size(148, 44)
        Me.btnPrintPreview.TabIndex = 10
        Me.btnPrintPreview.Text = "Grid Preview"
        '
        'btnRefresh
        '
        Me.btnRefresh.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.Appearance.Options.UseFont = True
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.Location = New System.Drawing.Point(832, 84)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(105, 36)
        Me.btnRefresh.TabIndex = 9
        Me.btnRefresh.Text = "Refresh"
        '
        'btnSearch
        '
        Me.btnSearch.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnSearch.Appearance.Options.UseFont = True
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(651, 5)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(110, 44)
        Me.btnSearch.TabIndex = 8
        Me.btnSearch.Text = "Search"
        '
        'lblStatusResults
        '
        Me.lblStatusResults.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblStatusResults.Location = New System.Drawing.Point(393, 17)
        Me.lblStatusResults.Name = "lblStatusResults"
        Me.lblStatusResults.Size = New System.Drawing.Size(61, 13)
        Me.lblStatusResults.TabIndex = 1
        Me.lblStatusResults.Text = "Location ID :"
        '
        'lblFillterDetails
        '
        Me.lblFillterDetails.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblFillterDetails.Location = New System.Drawing.Point(393, 0)
        Me.lblFillterDetails.Name = "lblFillterDetails"
        Me.lblFillterDetails.Size = New System.Drawing.Size(66, 13)
        Me.lblFillterDetails.TabIndex = 0
        Me.lblFillterDetails.Text = "Company ID :"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.lblTotalRecords)
        Me.PanelControl2.Controls.Add(Me.lblTotalAmount)
        Me.PanelControl2.Controls.Add(Me.lblStatusResults)
        Me.PanelControl2.Controls.Add(Me.lblFillterDetails)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(0, 623)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(1266, 30)
        Me.PanelControl2.TabIndex = 2
        '
        'lblTotalRecords
        '
        Me.lblTotalRecords.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTotalRecords.Location = New System.Drawing.Point(23, 8)
        Me.lblTotalRecords.Name = "lblTotalRecords"
        Me.lblTotalRecords.Size = New System.Drawing.Size(91, 13)
        Me.lblTotalRecords.TabIndex = 1
        Me.lblTotalRecords.Text = "Total Records: 0"
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTotalAmount.Location = New System.Drawing.Point(200, 8)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(107, 13)
        Me.lblTotalAmount.TabIndex = 0
        Me.lblTotalAmount.Text = "Total Amount: 0.00"
        '
        'frmAdvancePaymentReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1266, 653)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "frmAdvancePaymentReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Advance Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCurrencyEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.ListBoxControlSalemanList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListBoxControlReportList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.ListBoxControlReportType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.PanelControl2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblFillterDetails As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtStartDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPrintPreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnExport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblTotalAmount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTotalRecords As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RepositoryItemCurrencyEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit

    ' Grid Columns
    Friend WithEvents lblStatusResults As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ListBoxControlReportList As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ListBoxControlSalemanList As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ListBoxControlReportType As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents btnprintreport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAdvance As DevExpress.XtraEditors.SimpleButton
End Class
