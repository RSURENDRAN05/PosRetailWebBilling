<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalesMasterReport
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
        Dim GridLevelNode3 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode4 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalesMasterReport))
        Me.BandedGridView1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.GridBand1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.GridColumn4 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.BandedGridView2 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.GridBand2 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colInvoiceId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRefId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTrNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPrefix = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTotalQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTotalAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colItemDiscPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colItemDiscAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBillDiscPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBillDiscAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTotDiscPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTotDiscAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colGrossAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTaxAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colServiceCharge = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRoundOff = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNetAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSaleType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBillType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBillStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayMode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCustomerId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCounterName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colUserId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colComId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLocId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPmId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBillRemarks = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAdvAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colOutstanding = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colGivenAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCurrencyEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colBalAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colShiftNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDayNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCreated = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.colModified = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnPrintPreview = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSaveLayout = New DevExpress.XtraEditors.SimpleButton()
        Me.btnExport = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrint = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.dtEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.dtStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.lblStatusResults = New DevExpress.XtraEditors.LabelControl()
        Me.lblFillterDetails = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.lblTotalRecords = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotalAmount = New DevExpress.XtraEditors.LabelControl()
        Me.ImageCollectionBillSelect = New DevExpress.Utils.ImageCollection(Me.components)
        Me.RepositoryItemImageComboBoxMasterRpt = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepositoryItemImageComboBoxMasterPrint = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        CType(Me.BandedGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BandedGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCurrencyEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.dtEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.ImageCollectionBillSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBoxMasterRpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBoxMasterPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BandedGridView1
        '
        Me.BandedGridView1.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BandedGridView1.Appearance.FooterPanel.Options.UseFont = True
        Me.BandedGridView1.Appearance.FooterPanel.Options.UseTextOptions = True
        Me.BandedGridView1.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.BandedGridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.BandedGridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.BandedGridView1.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.BandedGridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BandedGridView1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.BandedGridView1.Appearance.Row.Options.UseFont = True
        Me.BandedGridView1.Appearance.Row.Options.UseTextOptions = True
        Me.BandedGridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BandedGridView1.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.GridBand1})
        Me.BandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10, Me.GridColumn11})
        Me.BandedGridView1.GridControl = Me.GridControl1
        Me.BandedGridView1.Name = "BandedGridView1"
        Me.BandedGridView1.OptionsBehavior.Editable = False
        Me.BandedGridView1.OptionsBehavior.ReadOnly = True
        Me.BandedGridView1.OptionsView.ShowFooter = True
        Me.BandedGridView1.OptionsView.ShowGroupPanel = False
        Me.BandedGridView1.RowHeight = 30
        '
        'GridBand1
        '
        Me.GridBand1.Caption = "GridBand1"
        Me.GridBand1.Columns.Add(Me.GridColumn4)
        Me.GridBand1.Columns.Add(Me.GridColumn5)
        Me.GridBand1.Columns.Add(Me.GridColumn6)
        Me.GridBand1.Columns.Add(Me.GridColumn7)
        Me.GridBand1.Columns.Add(Me.GridColumn8)
        Me.GridBand1.Columns.Add(Me.GridColumn9)
        Me.GridBand1.Columns.Add(Me.GridColumn10)
        Me.GridBand1.Columns.Add(Me.GridColumn11)
        Me.GridBand1.Name = "GridBand1"
        Me.GridBand1.VisibleIndex = 0
        Me.GridBand1.Width = 600
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn4.Caption = "TRNO"
        Me.GridColumn4.FieldName = "psid_invoice_trno"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridColumn5.Caption = "ITEM NAME"
        Me.GridColumn5.FieldName = "psid_invoice_description"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn6.Caption = "Qty"
        Me.GridColumn6.FieldName = "psid_invoice_proqty"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn7.Caption = "Price"
        Me.GridColumn7.FieldName = "psid_invoice_rate"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        '
        'GridColumn8
        '
        Me.GridColumn8.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn8.Caption = "Net Amount"
        Me.GridColumn8.FieldName = "psid_invoice_netamt"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GridColumn8.Visible = True
        '
        'GridColumn9
        '
        Me.GridColumn9.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn9.Caption = "Salesman"
        Me.GridColumn9.FieldName = "emp_printname"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        '
        'GridColumn10
        '
        Me.GridColumn10.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn10.Caption = "SalemanPer%"
        Me.GridColumn10.FieldName = "psid_invoice_salemanper"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        '
        'GridColumn11
        '
        Me.GridColumn11.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn11.Caption = "Commission"
        Me.GridColumn11.FieldName = "Commission"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GridColumn11.Visible = True
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        GridLevelNode3.LevelTemplate = Me.BandedGridView1
        GridLevelNode3.RelationName = "HeaderDetails"
        GridLevelNode4.LevelTemplate = Me.BandedGridView2
        GridLevelNode4.RelationName = "HeaderPayment"
        Me.GridControl1.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode3, GridLevelNode4})
        Me.GridControl1.Location = New System.Drawing.Point(0, 80)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCurrencyEdit1, Me.RepositoryItemDateEdit1, Me.RepositoryItemImageComboBoxMasterRpt, Me.RepositoryItemImageComboBoxMasterPrint})
        Me.GridControl1.Size = New System.Drawing.Size(1398, 490)
        Me.GridControl1.TabIndex = 0
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.BandedGridView2, Me.GridView1, Me.BandedGridView1})
        '
        'BandedGridView2
        '
        Me.BandedGridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.BandedGridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.BandedGridView2.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.BandedGridView2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BandedGridView2.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.BandedGridView2.Appearance.Row.Options.UseFont = True
        Me.BandedGridView2.Appearance.Row.Options.UseTextOptions = True
        Me.BandedGridView2.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BandedGridView2.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.GridBand2})
        Me.BandedGridView2.GridControl = Me.GridControl1
        Me.BandedGridView2.Name = "BandedGridView2"
        Me.BandedGridView2.OptionsBehavior.Editable = False
        Me.BandedGridView2.OptionsBehavior.ReadOnly = True
        Me.BandedGridView2.OptionsView.ShowGroupPanel = False
        '
        'GridBand2
        '
        Me.GridBand2.Caption = "GridBand2"
        Me.GridBand2.Name = "GridBand2"
        Me.GridBand2.VisibleIndex = 0
        '
        'GridView1
        '
        Me.GridView1.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FooterPanel.Options.UseFont = True
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
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colInvoiceId, Me.colRefId, Me.colTrNo, Me.colDate, Me.colPrefix, Me.colTotalQty, Me.colTotalAmount, Me.colItemDiscPer, Me.colItemDiscAmt, Me.colBillDiscPer, Me.colBillDiscAmt, Me.colTotDiscPer, Me.colTotDiscAmt, Me.colGrossAmt, Me.colTaxAmt, Me.colServiceCharge, Me.colRoundOff, Me.colNetAmt, Me.colSaleType, Me.colBillType, Me.colBillStatus, Me.colPayMode, Me.colCustomerId, Me.colDescription, Me.colCounterName, Me.colUserId, Me.colComId, Me.colLocId, Me.colPmId, Me.colPrint, Me.colBillRemarks, Me.colAdvAmt, Me.colOutstanding, Me.colGivenAmt, Me.colBalAmt, Me.colShiftNo, Me.colDayNo, Me.colCreated, Me.colModified, Me.GridColumn1, Me.GridColumn2, Me.GridColumn3})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "psih_invoice_tnetamt", Me.colNetAmt, "{0:n2}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "psih_invoice_id", Nothing, "Count: {0}")})
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsSelection.MultiSelect = True
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.RowHeight = 30
        '
        'colInvoiceId
        '
        Me.colInvoiceId.Caption = "Invoice ID"
        Me.colInvoiceId.FieldName = "psih_invoice_id"
        Me.colInvoiceId.Name = "colInvoiceId"
        Me.colInvoiceId.Width = 80
        '
        'colRefId
        '
        Me.colRefId.Caption = "Ref ID"
        Me.colRefId.FieldName = "psih_invoice_refid"
        Me.colRefId.Name = "colRefId"
        Me.colRefId.Width = 70
        '
        'colTrNo
        '
        Me.colTrNo.AppearanceCell.Options.UseTextOptions = True
        Me.colTrNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colTrNo.Caption = "Transaction No"
        Me.colTrNo.FieldName = "psih_invoice_trno"
        Me.colTrNo.Name = "colTrNo"
        Me.colTrNo.Visible = True
        Me.colTrNo.VisibleIndex = 0
        Me.colTrNo.Width = 100
        '
        'colDate
        '
        Me.colDate.AppearanceCell.Options.UseTextOptions = True
        Me.colDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colDate.Caption = "Date"
        Me.colDate.FieldName = "psih_invoice_date"
        Me.colDate.Name = "colDate"
        Me.colDate.Visible = True
        Me.colDate.VisibleIndex = 1
        Me.colDate.Width = 90
        '
        'colPrefix
        '
        Me.colPrefix.Caption = "Prefix"
        Me.colPrefix.FieldName = "psih_invoice_prefix"
        Me.colPrefix.Name = "colPrefix"
        Me.colPrefix.Width = 60
        '
        'colTotalQty
        '
        Me.colTotalQty.AppearanceCell.Options.UseTextOptions = True
        Me.colTotalQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colTotalQty.Caption = "Total Qty"
        Me.colTotalQty.FieldName = "psih_invoice_tqty"
        Me.colTotalQty.Name = "colTotalQty"
        Me.colTotalQty.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "psih_invoice_tqty", "{0:n2}")})
        Me.colTotalQty.Width = 80
        '
        'colTotalAmount
        '
        Me.colTotalAmount.AppearanceCell.Options.UseTextOptions = True
        Me.colTotalAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colTotalAmount.Caption = "Total Amount"
        Me.colTotalAmount.FieldName = "psih_invoice_tamount"
        Me.colTotalAmount.Name = "colTotalAmount"
        Me.colTotalAmount.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "psih_invoice_tamount", "{0:n2}")})
        Me.colTotalAmount.Width = 100
        '
        'colItemDiscPer
        '
        Me.colItemDiscPer.AppearanceCell.Options.UseTextOptions = True
        Me.colItemDiscPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colItemDiscPer.Caption = "Item Disc %"
        Me.colItemDiscPer.FieldName = "psih_invoice_titemdisper"
        Me.colItemDiscPer.Name = "colItemDiscPer"
        Me.colItemDiscPer.Width = 80
        '
        'colItemDiscAmt
        '
        Me.colItemDiscAmt.Caption = "Item Disc Amt"
        Me.colItemDiscAmt.FieldName = "psih_invoice_titemdisamt"
        Me.colItemDiscAmt.Name = "colItemDiscAmt"
        Me.colItemDiscAmt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "psih_invoice_titemdisamt", "{0:n2}")})
        Me.colItemDiscAmt.Width = 90
        '
        'colBillDiscPer
        '
        Me.colBillDiscPer.Caption = "Bill Disc %"
        Me.colBillDiscPer.FieldName = "psih_invoice_tbilldiscper"
        Me.colBillDiscPer.Name = "colBillDiscPer"
        Me.colBillDiscPer.Width = 80
        '
        'colBillDiscAmt
        '
        Me.colBillDiscAmt.Caption = "Bill Disc Amt"
        Me.colBillDiscAmt.FieldName = "psih_invoice_tbilldiscamt"
        Me.colBillDiscAmt.Name = "colBillDiscAmt"
        Me.colBillDiscAmt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "psih_invoice_tbilldiscamt", "{0:n2}")})
        Me.colBillDiscAmt.Width = 90
        '
        'colTotDiscPer
        '
        Me.colTotDiscPer.Caption = "Tot Disc %"
        Me.colTotDiscPer.FieldName = "psih_invoice_totdiscper"
        Me.colTotDiscPer.Name = "colTotDiscPer"
        Me.colTotDiscPer.Width = 80
        '
        'colTotDiscAmt
        '
        Me.colTotDiscAmt.AppearanceCell.Options.UseTextOptions = True
        Me.colTotDiscAmt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colTotDiscAmt.Caption = "Tot Disc Amt"
        Me.colTotDiscAmt.FieldName = "psih_invoice_totdiscamt"
        Me.colTotDiscAmt.Name = "colTotDiscAmt"
        Me.colTotDiscAmt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "psih_invoice_totdiscamt", "{0:n2}")})
        Me.colTotDiscAmt.Width = 90
        '
        'colGrossAmt
        '
        Me.colGrossAmt.AppearanceCell.Options.UseTextOptions = True
        Me.colGrossAmt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colGrossAmt.Caption = "Gross Amount"
        Me.colGrossAmt.FieldName = "psih_invoice_tgrossamt"
        Me.colGrossAmt.Name = "colGrossAmt"
        Me.colGrossAmt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "psih_invoice_tgrossamt", "{0:n2}")})
        Me.colGrossAmt.Width = 100
        '
        'colTaxAmt
        '
        Me.colTaxAmt.Caption = "Tax Amount"
        Me.colTaxAmt.FieldName = "psih_invoice_ttaxamt"
        Me.colTaxAmt.Name = "colTaxAmt"
        Me.colTaxAmt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "psih_invoice_ttaxamt", "{0:n2}")})
        Me.colTaxAmt.Width = 90
        '
        'colServiceCharge
        '
        Me.colServiceCharge.Caption = "Service Charge"
        Me.colServiceCharge.FieldName = "psih_invoice_sercharge"
        Me.colServiceCharge.Name = "colServiceCharge"
        Me.colServiceCharge.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "psih_invoice_sercharge", "{0:n2}")})
        Me.colServiceCharge.Width = 90
        '
        'colRoundOff
        '
        Me.colRoundOff.Caption = "Round Off"
        Me.colRoundOff.FieldName = "psih_invoice_roundoff"
        Me.colRoundOff.Name = "colRoundOff"
        Me.colRoundOff.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "psih_invoice_roundoff", "{0:n2}")})
        Me.colRoundOff.Width = 80
        '
        'colNetAmt
        '
        Me.colNetAmt.AppearanceCell.Options.UseTextOptions = True
        Me.colNetAmt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colNetAmt.Caption = "Net Amount"
        Me.colNetAmt.FieldName = "psih_invoice_tnetamt"
        Me.colNetAmt.Name = "colNetAmt"
        Me.colNetAmt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "psih_invoice_tnetamt", "{0:n2}")})
        Me.colNetAmt.Visible = True
        Me.colNetAmt.VisibleIndex = 4
        Me.colNetAmt.Width = 100
        '
        'colSaleType
        '
        Me.colSaleType.AppearanceCell.Options.UseTextOptions = True
        Me.colSaleType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colSaleType.Caption = "Sale Type"
        Me.colSaleType.FieldName = "psih_invoice_saletype"
        Me.colSaleType.Name = "colSaleType"
        Me.colSaleType.Visible = True
        Me.colSaleType.VisibleIndex = 2
        Me.colSaleType.Width = 80
        '
        'colBillType
        '
        Me.colBillType.AppearanceCell.Options.UseTextOptions = True
        Me.colBillType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colBillType.Caption = "Bill Type"
        Me.colBillType.FieldName = "psih_invoice_billtype"
        Me.colBillType.Name = "colBillType"
        Me.colBillType.Visible = True
        Me.colBillType.VisibleIndex = 3
        Me.colBillType.Width = 80
        '
        'colBillStatus
        '
        Me.colBillStatus.AppearanceCell.Options.UseTextOptions = True
        Me.colBillStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colBillStatus.Caption = "Bill Status"
        Me.colBillStatus.ColumnEdit = Me.RepositoryItemImageComboBoxMasterRpt
        Me.colBillStatus.FieldName = "psih_invoice_billstatus"
        Me.colBillStatus.Name = "colBillStatus"
        Me.colBillStatus.Width = 80
        '
        'colPayMode
        '
        Me.colPayMode.Caption = "Payment Mode"
        Me.colPayMode.FieldName = "psih_invoice_paymode"
        Me.colPayMode.Name = "colPayMode"
        Me.colPayMode.Width = 90
        '
        'colCustomerId
        '
        Me.colCustomerId.Caption = "Customer ID"
        Me.colCustomerId.FieldName = "psih_invoice_customerid"
        Me.colCustomerId.Name = "colCustomerId"
        Me.colCustomerId.Width = 90
        '
        'colDescription
        '
        Me.colDescription.Caption = "Customer"
        Me.colDescription.FieldName = "psih_invoice_description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.Visible = True
        Me.colDescription.VisibleIndex = 5
        Me.colDescription.Width = 120
        '
        'colCounterName
        '
        Me.colCounterName.AppearanceCell.Options.UseTextOptions = True
        Me.colCounterName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colCounterName.Caption = "Counter Name"
        Me.colCounterName.FieldName = "psih_invoice_countername"
        Me.colCounterName.Name = "colCounterName"
        Me.colCounterName.Visible = True
        Me.colCounterName.VisibleIndex = 6
        Me.colCounterName.Width = 100
        '
        'colUserId
        '
        Me.colUserId.Caption = "User ID"
        Me.colUserId.FieldName = "psih_invoice_userid"
        Me.colUserId.Name = "colUserId"
        Me.colUserId.Width = 70
        '
        'colComId
        '
        Me.colComId.Caption = "Company ID"
        Me.colComId.FieldName = "psih_invoice_comid"
        Me.colComId.Name = "colComId"
        Me.colComId.Width = 70
        '
        'colLocId
        '
        Me.colLocId.Caption = "Location ID"
        Me.colLocId.FieldName = "psih_invoice_locid"
        Me.colLocId.Name = "colLocId"
        Me.colLocId.Width = 70
        '
        'colPmId
        '
        Me.colPmId.Caption = "PM ID"
        Me.colPmId.FieldName = "psih_invoice_pmid"
        Me.colPmId.Name = "colPmId"
        Me.colPmId.Width = 60
        '
        'colPrint
        '
        Me.colPrint.Caption = "Print"
        Me.colPrint.ColumnEdit = Me.RepositoryItemImageComboBoxMasterPrint
        Me.colPrint.FieldName = "psih_invoice_print"
        Me.colPrint.Name = "colPrint"
        Me.colPrint.Width = 50
        '
        'colBillRemarks
        '
        Me.colBillRemarks.Caption = "Bill Remarks"
        Me.colBillRemarks.FieldName = "psih_invoice_billremarks"
        Me.colBillRemarks.Name = "colBillRemarks"
        Me.colBillRemarks.Width = 100
        '
        'colAdvAmt
        '
        Me.colAdvAmt.Caption = "Advance Amount"
        Me.colAdvAmt.FieldName = "psih_invoice_advamt"
        Me.colAdvAmt.Name = "colAdvAmt"
        Me.colAdvAmt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "psih_invoice_advamt", "{0:n2}")})
        Me.colAdvAmt.Width = 90
        '
        'colOutstanding
        '
        Me.colOutstanding.Caption = "Outstanding"
        Me.colOutstanding.FieldName = "psih_invoice_outstanding"
        Me.colOutstanding.Name = "colOutstanding"
        Me.colOutstanding.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "psih_invoice_outstanding", "{0:n2}")})
        Me.colOutstanding.Width = 90
        '
        'colGivenAmt
        '
        Me.colGivenAmt.Caption = "Given Amount"
        Me.colGivenAmt.ColumnEdit = Me.RepositoryItemCurrencyEdit1
        Me.colGivenAmt.FieldName = "psih_invoice_givenamt"
        Me.colGivenAmt.Name = "colGivenAmt"
        Me.colGivenAmt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "psih_invoice_givenamt", "{0:n2}")})
        Me.colGivenAmt.Width = 90
        '
        'RepositoryItemCurrencyEdit1
        '
        Me.RepositoryItemCurrencyEdit1.Caption = "Check"
        Me.RepositoryItemCurrencyEdit1.Name = "RepositoryItemCurrencyEdit1"
        '
        'colBalAmt
        '
        Me.colBalAmt.Caption = "Balance Amount"
        Me.colBalAmt.FieldName = "psih_invoice_balamt"
        Me.colBalAmt.Name = "colBalAmt"
        Me.colBalAmt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "psih_invoice_balamt", "{0:n2}")})
        Me.colBalAmt.Width = 90
        '
        'colShiftNo
        '
        Me.colShiftNo.AppearanceCell.Options.UseTextOptions = True
        Me.colShiftNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colShiftNo.Caption = "Shift No"
        Me.colShiftNo.FieldName = "psih_invoice_shiftno"
        Me.colShiftNo.Name = "colShiftNo"
        Me.colShiftNo.Width = 70
        '
        'colDayNo
        '
        Me.colDayNo.Caption = "Day No"
        Me.colDayNo.FieldName = "psih_invoice_dayno"
        Me.colDayNo.Name = "colDayNo"
        Me.colDayNo.Width = 60
        '
        'colCreated
        '
        Me.colCreated.Caption = "Created"
        Me.colCreated.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.colCreated.FieldName = "psih_invoice_created"
        Me.colCreated.Name = "colCreated"
        Me.colCreated.Width = 90
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.CalendarTimeProperties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
        Me.RepositoryItemDateEdit1.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.[Default]
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.EditFormat.FormatString = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.Mask.EditMask = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        '
        'colModified
        '
        Me.colModified.Caption = "Modified"
        Me.colModified.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.colModified.FieldName = "psih_invoice_modified"
        Me.colModified.Name = "colModified"
        Me.colModified.Width = 90
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Company"
        Me.GridColumn1.FieldName = "pcm_name"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 7
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Location"
        Me.GridColumn2.FieldName = "plm_name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 8
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "BillCreated"
        Me.GridColumn3.FieldName = "psih_invoice_created"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 9
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btnPrintPreview)
        Me.PanelControl1.Controls.Add(Me.btnSaveLayout)
        Me.PanelControl1.Controls.Add(Me.btnExport)
        Me.PanelControl1.Controls.Add(Me.btnPrint)
        Me.PanelControl1.Controls.Add(Me.btnRefresh)
        Me.PanelControl1.Controls.Add(Me.btnSearch)
        Me.PanelControl1.Controls.Add(Me.dtEndDate)
        Me.PanelControl1.Controls.Add(Me.dtStartDate)
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.lblStatusResults)
        Me.PanelControl1.Controls.Add(Me.lblFillterDetails)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1398, 80)
        Me.PanelControl1.TabIndex = 1
        '
        'btnPrintPreview
        '
        Me.btnPrintPreview.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrintPreview.Appearance.Options.UseFont = True
        Me.btnPrintPreview.Image = CType(resources.GetObject("btnPrintPreview.Image"), System.Drawing.Image)
        Me.btnPrintPreview.Location = New System.Drawing.Point(1198, 5)
        Me.btnPrintPreview.Name = "btnPrintPreview"
        Me.btnPrintPreview.Size = New System.Drawing.Size(134, 44)
        Me.btnPrintPreview.TabIndex = 13
        Me.btnPrintPreview.Text = "Print"
        '
        'btnSaveLayout
        '
        Me.btnSaveLayout.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnSaveLayout.Appearance.Options.UseFont = True
        Me.btnSaveLayout.Image = CType(resources.GetObject("btnSaveLayout.Image"), System.Drawing.Image)
        Me.btnSaveLayout.Location = New System.Drawing.Point(1059, 5)
        Me.btnSaveLayout.Name = "btnSaveLayout"
        Me.btnSaveLayout.Size = New System.Drawing.Size(134, 44)
        Me.btnSaveLayout.TabIndex = 12
        Me.btnSaveLayout.Text = "SaveLayout"
        '
        'btnExport
        '
        Me.btnExport.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnExport.Appearance.Options.UseFont = True
        Me.btnExport.Image = CType(resources.GetObject("btnExport.Image"), System.Drawing.Image)
        Me.btnExport.Location = New System.Drawing.Point(917, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(134, 44)
        Me.btnExport.TabIndex = 11
        Me.btnExport.Text = "Export"
        '
        'btnPrint
        '
        Me.btnPrint.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrint.Appearance.Options.UseFont = True
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.Location = New System.Drawing.Point(775, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(134, 44)
        Me.btnPrint.TabIndex = 10
        Me.btnPrint.Text = "Grid Preview"
        '
        'btnRefresh
        '
        Me.btnRefresh.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.Appearance.Options.UseFont = True
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.Location = New System.Drawing.Point(633, 5)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(134, 44)
        Me.btnRefresh.TabIndex = 9
        Me.btnRefresh.Text = "Refresh"
        '
        'btnSearch
        '
        Me.btnSearch.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnSearch.Appearance.Options.UseFont = True
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(491, 5)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(134, 44)
        Me.btnSearch.TabIndex = 8
        Me.btnSearch.Text = "Search"
        '
        'dtEndDate
        '
        Me.dtEndDate.EditValue = Nothing
        Me.dtEndDate.Location = New System.Drawing.Point(172, 12)
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
        'dtStartDate
        '
        Me.dtStartDate.EditValue = Nothing
        Me.dtStartDate.Location = New System.Drawing.Point(39, 12)
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
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(145, 15)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(19, 13)
        Me.LabelControl4.TabIndex = 5
        Me.LabelControl4.Text = "To :"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(7, 15)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(31, 13)
        Me.LabelControl3.TabIndex = 4
        Me.LabelControl3.Text = "From :"
        '
        'lblStatusResults
        '
        Me.lblStatusResults.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblStatusResults.Location = New System.Drawing.Point(5, 63)
        Me.lblStatusResults.Name = "lblStatusResults"
        Me.lblStatusResults.Size = New System.Drawing.Size(61, 13)
        Me.lblStatusResults.TabIndex = 1
        Me.lblStatusResults.Text = "Location ID :"
        '
        'lblFillterDetails
        '
        Me.lblFillterDetails.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblFillterDetails.Location = New System.Drawing.Point(5, 41)
        Me.lblFillterDetails.Name = "lblFillterDetails"
        Me.lblFillterDetails.Size = New System.Drawing.Size(66, 13)
        Me.lblFillterDetails.TabIndex = 0
        Me.lblFillterDetails.Text = "Company ID :"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.lblTotalRecords)
        Me.PanelControl2.Controls.Add(Me.lblTotalAmount)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(0, 570)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(1398, 30)
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
        'ImageCollectionBillSelect
        '
        Me.ImageCollectionBillSelect.ImageStream = CType(resources.GetObject("ImageCollectionBillSelect.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollectionBillSelect.Images.SetKeyName(0, "stop_green.png")
        Me.ImageCollectionBillSelect.Images.SetKeyName(1, "stop_red.png")
        Me.ImageCollectionBillSelect.Images.SetKeyName(2, "stop_blue.png")
        Me.ImageCollectionBillSelect.Images.SetKeyName(3, "cancel-16x16.png")
        '
        'RepositoryItemImageComboBoxMasterRpt
        '
        Me.RepositoryItemImageComboBoxMasterRpt.AutoHeight = False
        Me.RepositoryItemImageComboBoxMasterRpt.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBoxMasterRpt.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("New", "Closed", 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cancel", "Cancel", 3)})
        Me.RepositoryItemImageComboBoxMasterRpt.Name = "RepositoryItemImageComboBoxMasterRpt"
        Me.RepositoryItemImageComboBoxMasterRpt.SmallImages = Me.ImageCollectionBillSelect
        '
        'RepositoryItemImageComboBoxMasterPrint
        '
        Me.RepositoryItemImageComboBoxMasterPrint.AutoHeight = False
        Me.RepositoryItemImageComboBoxMasterPrint.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBoxMasterPrint.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("No", "0", 2), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("YES", "1", 0)})
        Me.RepositoryItemImageComboBoxMasterPrint.Name = "RepositoryItemImageComboBoxMasterPrint"
        Me.RepositoryItemImageComboBoxMasterPrint.SmallImages = Me.ImageCollectionBillSelect
        '
        'frmSalesMasterReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1398, 600)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "frmSalesMasterReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sales Master Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.BandedGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BandedGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCurrencyEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.ImageCollectionBillSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBoxMasterRpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBoxMasterPrint, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents btnPrint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnExport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblTotalAmount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTotalRecords As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RepositoryItemCurrencyEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit

    ' Grid Columns
    Friend WithEvents colInvoiceId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRefId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTrNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPrefix As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTotalQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTotalAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colItemDiscPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colItemDiscAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBillDiscPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBillDiscAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTotDiscPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTotDiscAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colGrossAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTaxAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colServiceCharge As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRoundOff As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNetAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSaleType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBillType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBillStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPayMode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCustomerId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCounterName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUserId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colComId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLocId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPmId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBillRemarks As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAdvAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOutstanding As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colGivenAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBalAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colShiftNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDayNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCreated As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colModified As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lblStatusResults As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnSaveLayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BandedGridView1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents GridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents BandedGridView2 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents GridBand2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents btnPrintPreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RepositoryItemImageComboBoxMasterRpt As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents ImageCollectionBillSelect As DevExpress.Utils.ImageCollection
    Friend WithEvents RepositoryItemImageComboBoxMasterPrint As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
End Class
