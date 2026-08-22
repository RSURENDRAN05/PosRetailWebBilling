<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVoucherUsageReport
    Inherits DevExpress.XtraEditors.XtraForm

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colBillNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVoucherCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBillAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCompanyName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLocationName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colShiftNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDayNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCreated = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnPrint = New DevExpress.XtraEditors.SimpleButton()
        Me.btnExport = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.dtEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.dtStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblStatusResults = New DevExpress.XtraEditors.LabelControl()
        Me.lblFillterDetails = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.lblTotalRecords = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotalAmount = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.dtEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControl1.Location = New System.Drawing.Point(0, 80)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(1200, 470)
        Me.GridControl1.TabIndex = 0
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colBillNo, Me.colVoucherCode, Me.colBillAmount, Me.colCompanyName, Me.colLocationName, Me.colShiftNo, Me.colDayNo, Me.colCreated})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BillAmount", Me.colBillAmount, "{0:n2}")})
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'colBillNo
        '
        Me.colBillNo.AppearanceCell.Options.UseTextOptions = True
        Me.colBillNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colBillNo.Caption = "Bill No"
        Me.colBillNo.FieldName = "BillNo"
        Me.colBillNo.Name = "colBillNo"
        Me.colBillNo.Visible = True
        Me.colBillNo.VisibleIndex = 0
        Me.colBillNo.Width = 100
        '
        'colVoucherCode
        '
        Me.colVoucherCode.AppearanceCell.Options.UseTextOptions = True
        Me.colVoucherCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colVoucherCode.Caption = "Voucher Code"
        Me.colVoucherCode.FieldName = "VoucherCode"
        Me.colVoucherCode.Name = "colVoucherCode"
        Me.colVoucherCode.Visible = True
        Me.colVoucherCode.VisibleIndex = 1
        Me.colVoucherCode.Width = 110
        '
        'colBillAmount
        '
        Me.colBillAmount.AppearanceCell.Options.UseTextOptions = True
        Me.colBillAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colBillAmount.Caption = "Bill Amount"
        Me.colBillAmount.FieldName = "BillAmount"
        Me.colBillAmount.Name = "colBillAmount"
        Me.colBillAmount.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BillAmount", "{0:n2}")})
        Me.colBillAmount.Visible = True
        Me.colBillAmount.VisibleIndex = 2
        Me.colBillAmount.Width = 100
        '
        'colCompanyName
        '
        Me.colCompanyName.AppearanceCell.Options.UseTextOptions = True
        Me.colCompanyName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colCompanyName.Caption = "Company"
        Me.colCompanyName.FieldName = "CompanyName"
        Me.colCompanyName.Name = "colCompanyName"
        Me.colCompanyName.Visible = True
        Me.colCompanyName.VisibleIndex = 3
        Me.colCompanyName.Width = 130
        '
        'colLocationName
        '
        Me.colLocationName.AppearanceCell.Options.UseTextOptions = True
        Me.colLocationName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colLocationName.Caption = "Location"
        Me.colLocationName.FieldName = "LocationName"
        Me.colLocationName.Name = "colLocationName"
        Me.colLocationName.Visible = True
        Me.colLocationName.VisibleIndex = 4
        Me.colLocationName.Width = 130
        '
        'colShiftNo
        '
        Me.colShiftNo.AppearanceCell.Options.UseTextOptions = True
        Me.colShiftNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colShiftNo.Caption = "Shift No"
        Me.colShiftNo.FieldName = "ShiftNo"
        Me.colShiftNo.Name = "colShiftNo"
        Me.colShiftNo.Width = 70
        '
        'colDayNo
        '
        Me.colDayNo.AppearanceCell.Options.UseTextOptions = True
        Me.colDayNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colDayNo.Caption = "Day No"
        Me.colDayNo.FieldName = "DayNo"
        Me.colDayNo.Name = "colDayNo"
        Me.colDayNo.Width = 70
        '
        'colCreated
        '
        Me.colCreated.AppearanceCell.Options.UseTextOptions = True
        Me.colCreated.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colCreated.Caption = "Created"
        Me.colCreated.FieldName = "Created"
        Me.colCreated.Name = "colCreated"
        Me.colCreated.Visible = True
        Me.colCreated.VisibleIndex = 5
        Me.colCreated.Width = 130
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btnPrint)
        Me.PanelControl1.Controls.Add(Me.btnExport)
        Me.PanelControl1.Controls.Add(Me.btnRefresh)
        Me.PanelControl1.Controls.Add(Me.btnSearch)
        Me.PanelControl1.Controls.Add(Me.dtEndDate)
        Me.PanelControl1.Controls.Add(Me.dtStartDate)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.lblStatusResults)
        Me.PanelControl1.Controls.Add(Me.lblFillterDetails)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1200, 80)
        Me.PanelControl1.TabIndex = 1
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(780, 10)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(100, 32)
        Me.btnPrint.TabIndex = 5
        Me.btnPrint.Text = "Print"
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(670, 10)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(100, 32)
        Me.btnExport.TabIndex = 4
        Me.btnExport.Text = "Export"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(560, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 32)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "Refresh"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(450, 10)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 32)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "Search"
        '
        'dtEndDate
        '
        Me.dtEndDate.EditValue = Nothing
        Me.dtEndDate.Location = New System.Drawing.Point(300, 14)
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
        Me.dtEndDate.Size = New System.Drawing.Size(130, 20)
        Me.dtEndDate.TabIndex = 1
        '
        'dtStartDate
        '
        Me.dtStartDate.EditValue = Nothing
        Me.dtStartDate.Location = New System.Drawing.Point(110, 14)
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
        Me.dtStartDate.Size = New System.Drawing.Size(130, 20)
        Me.dtStartDate.TabIndex = 0
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(260, 17)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(16, 13)
        Me.LabelControl2.TabIndex = 6
        Me.LabelControl2.Text = "To:"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(20, 17)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(28, 13)
        Me.LabelControl1.TabIndex = 7
        Me.LabelControl1.Text = "From:"
        '
        'lblStatusResults
        '
        Me.lblStatusResults.Location = New System.Drawing.Point(20, 44)
        Me.lblStatusResults.Name = "lblStatusResults"
        Me.lblStatusResults.Size = New System.Drawing.Size(43, 13)
        Me.lblStatusResults.TabIndex = 8
        Me.lblStatusResults.Text = "Ready..."
        '
        'lblFillterDetails
        '
        Me.lblFillterDetails.Location = New System.Drawing.Point(20, 60)
        Me.lblFillterDetails.Name = "lblFillterDetails"
        Me.lblFillterDetails.Size = New System.Drawing.Size(63, 13)
        Me.lblFillterDetails.TabIndex = 9
        Me.lblFillterDetails.Text = "Filter Details:"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.lblTotalRecords)
        Me.PanelControl2.Controls.Add(Me.lblTotalAmount)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(0, 550)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(1200, 34)
        Me.PanelControl2.TabIndex = 2
        '
        'lblTotalRecords
        '
        Me.lblTotalRecords.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalRecords.Location = New System.Drawing.Point(20, 10)
        Me.lblTotalRecords.Name = "lblTotalRecords"
        Me.lblTotalRecords.Size = New System.Drawing.Size(140, 14)
        Me.lblTotalRecords.TabIndex = 0
        Me.lblTotalRecords.Text = "Total Vouchers Used: 0"
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalAmount.Location = New System.Drawing.Point(300, 10)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(142, 14)
        Me.lblTotalAmount.TabIndex = 1
        Me.lblTotalAmount.Text = "Total Bill Amount: 0.00"
        '
        'frmVoucherUsageReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 584)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "frmVoucherUsageReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Voucher Usage Report"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.dtEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.PanelControl2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colBillNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVoucherCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBillAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCompanyName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLocationName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colShiftNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDayNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCreated As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnPrint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnExport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents dtEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtStartDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblStatusResults As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblFillterDetails As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblTotalRecords As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTotalAmount As DevExpress.XtraEditors.LabelControl
End Class
