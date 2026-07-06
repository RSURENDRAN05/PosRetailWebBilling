<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGenerateTaxReport
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
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.BarSubItem1 = New DevExpress.XtraBars.BarSubItem()
        Me.barbtnpurgebydate = New DevExpress.XtraBars.BarButtonItem()
        Me.barbtnpurgebymonth = New DevExpress.XtraBars.BarButtonItem()
        Me.barbtnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.Bar3 = New DevExpress.XtraBars.Bar()
        Me.barSelectedNetAmt = New DevExpress.XtraBars.BarStaticItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.lblDetailNetAmt = New System.Windows.Forms.Label()
        Me.GridControlItemList = New DevExpress.XtraGrid.GridControl()
        Me.GridViewItemList = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lblHeaderNetAmt = New System.Windows.Forms.Label()
        Me.GridControlHeader = New DevExpress.XtraGrid.GridControl()
        Me.GridViewHeader = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumnSelected = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chkSelectHeader = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lblstatusgroup = New DevExpress.XtraEditors.GroupControl()
        Me.btnTransferDataDuplicateProcess = New DevExpress.XtraEditors.SimpleButton()
        Me.btnProcessAll = New DevExpress.XtraEditors.SimpleButton()
        Me.btnProcess = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSelectedItemDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtsetlimit = New DevExpress.XtraEditors.TextEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnBulkDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDelSelectedTrno = New DevExpress.XtraEditors.SimpleButton()
        Me.btnfinalprocess = New DevExpress.XtraEditors.SimpleButton()
        Me.txtNoofRow = New DevExpress.XtraEditors.TextEdit()
        Me.ListBoxControlPayment = New DevExpress.XtraEditors.ListBoxControl()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.FromDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.GridControlItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControlHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSelectHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblstatusgroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lblstatusgroup.SuspendLayout()
        CType(Me.txtsetlimit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.txtNoofRow.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListBoxControlPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FromDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FromDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1, Me.Bar3})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.barbtnClose, Me.BarSubItem1, Me.barbtnpurgebydate, Me.barbtnpurgebymonth, Me.barSelectedNetAmt})
        Me.BarManager1.MaxItemId = 5
        Me.BarManager1.StatusBar = Me.Bar3
        '
        'Bar1
        '
        Me.Bar1.BarName = "Tools"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem1, True), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barbtnClose, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Tools"
        '
        'BarSubItem1
        '
        Me.BarSubItem1.Caption = "Menu"
        Me.BarSubItem1.Id = 1
        Me.BarSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.barbtnpurgebydate, True), New DevExpress.XtraBars.LinkPersistInfo(Me.barbtnpurgebymonth, True)})
        Me.BarSubItem1.Name = "BarSubItem1"
        '
        'barbtnpurgebydate
        '
        Me.barbtnpurgebydate.Caption = "Purge By Date"
        Me.barbtnpurgebydate.Id = 2
        Me.barbtnpurgebydate.Name = "barbtnpurgebydate"
        '
        'barbtnpurgebymonth
        '
        Me.barbtnpurgebymonth.Caption = "Purge By Month"
        Me.barbtnpurgebymonth.Id = 3
        Me.barbtnpurgebymonth.Name = "barbtnpurgebymonth"
        '
        'barbtnClose
        '
        Me.barbtnClose.Caption = "Close"
        Me.barbtnClose.Id = 0
        Me.barbtnClose.Name = "barbtnClose"
        '
        'Bar3
        '
        Me.Bar3.BarName = "Status bar"
        Me.Bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.Bar3.DockCol = 0
        Me.Bar3.DockRow = 0
        Me.Bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.Bar3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.barSelectedNetAmt)})
        Me.Bar3.OptionsBar.AllowQuickCustomization = False
        Me.Bar3.OptionsBar.DrawDragBorder = False
        Me.Bar3.OptionsBar.UseWholeRow = True
        Me.Bar3.Text = "Status bar"
        '
        'barSelectedNetAmt
        '
        Me.barSelectedNetAmt.Caption = "Selected: RM 0.00"
        Me.barSelectedNetAmt.Id = 4
        Me.barSelectedNetAmt.Name = "barSelectedNetAmt"
        Me.barSelectedNetAmt.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1008, 29)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 704)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1008, 25)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 29)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 675)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1008, 29)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 675)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.lblDetailNetAmt)
        Me.LayoutControl1.Controls.Add(Me.GridControlItemList)
        Me.LayoutControl1.Controls.Add(Me.lblHeaderNetAmt)
        Me.LayoutControl1.Controls.Add(Me.GridControlHeader)
        Me.LayoutControl1.Controls.Add(Me.lblstatusgroup)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 29)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1008, 675)
        Me.LayoutControl1.TabIndex = 4
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'lblDetailNetAmt
        '
        Me.lblDetailNetAmt.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblDetailNetAmt.Location = New System.Drawing.Point(417, 151)
        Me.lblDetailNetAmt.Name = "lblDetailNetAmt"
        Me.lblDetailNetAmt.Size = New System.Drawing.Size(579, 20)
        Me.lblDetailNetAmt.TabIndex = 12
        Me.lblDetailNetAmt.Text = "Label6"
        '
        'GridControlItemList
        '
        Me.GridControlItemList.Location = New System.Drawing.Point(417, 191)
        Me.GridControlItemList.MainView = Me.GridViewItemList
        Me.GridControlItemList.MenuManager = Me.BarManager1
        Me.GridControlItemList.Name = "GridControlItemList"
        Me.GridControlItemList.Size = New System.Drawing.Size(579, 472)
        Me.GridControlItemList.TabIndex = 6
        Me.GridControlItemList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewItemList})
        '
        'GridViewItemList
        '
        Me.GridViewItemList.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridViewItemList.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewItemList.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridViewItemList.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewItemList.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridViewItemList.Appearance.Row.Options.UseFont = True
        Me.GridViewItemList.Appearance.Row.Options.UseTextOptions = True
        Me.GridViewItemList.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewItemList.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn11})
        Me.GridViewItemList.GridControl = Me.GridControlItemList
        Me.GridViewItemList.Name = "GridViewItemList"
        Me.GridViewItemList.OptionsView.ShowFooter = True
        Me.GridViewItemList.OptionsView.ShowGroupPanel = False
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "RowId"
        Me.GridColumn5.FieldName = "RowId"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        Me.GridColumn5.Width = 54
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Trno"
        Me.GridColumn6.FieldName = "Trno"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 2
        Me.GridColumn6.Width = 63
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "ItemName"
        Me.GridColumn7.FieldName = "ItemName"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 3
        Me.GridColumn7.Width = 217
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Qty"
        Me.GridColumn8.FieldName = "Qty"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 4
        Me.GridColumn8.Width = 72
        '
        'GridColumn9
        '
        Me.GridColumn9.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn9.Caption = "NetAmt"
        Me.GridColumn9.FieldName = "NetAmt"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NetAmt", "{0:n2}")})
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 5
        Me.GridColumn9.Width = 83
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Date"
        Me.GridColumn11.FieldName = "Date"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 1
        Me.GridColumn11.Width = 72
        '
        'lblHeaderNetAmt
        '
        Me.lblHeaderNetAmt.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblHeaderNetAmt.Location = New System.Drawing.Point(12, 151)
        Me.lblHeaderNetAmt.Name = "lblHeaderNetAmt"
        Me.lblHeaderNetAmt.Size = New System.Drawing.Size(401, 20)
        Me.lblHeaderNetAmt.TabIndex = 11
        Me.lblHeaderNetAmt.Text = "Label5"
        '
        'GridControlHeader
        '
        Me.GridControlHeader.Location = New System.Drawing.Point(12, 191)
        Me.GridControlHeader.MainView = Me.GridViewHeader
        Me.GridControlHeader.MenuManager = Me.BarManager1
        Me.GridControlHeader.Name = "GridControlHeader"
        Me.GridControlHeader.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.chkSelectHeader})
        Me.GridControlHeader.Size = New System.Drawing.Size(401, 472)
        Me.GridControlHeader.TabIndex = 5
        Me.GridControlHeader.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewHeader})
        '
        'GridViewHeader
        '
        Me.GridViewHeader.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridViewHeader.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewHeader.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridViewHeader.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewHeader.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridViewHeader.Appearance.Row.Options.UseFont = True
        Me.GridViewHeader.Appearance.Row.Options.UseTextOptions = True
        Me.GridViewHeader.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewHeader.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumnSelected, Me.GridColumn12, Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn10, Me.GridColumn13})
        Me.GridViewHeader.GridControl = Me.GridControlHeader
        Me.GridViewHeader.Name = "GridViewHeader"
        Me.GridViewHeader.OptionsSelection.MultiSelect = True
        Me.GridViewHeader.OptionsView.ShowAutoFilterRow = True
        Me.GridViewHeader.OptionsView.ShowFooter = True
        Me.GridViewHeader.OptionsView.ShowGroupPanel = False
        '
        'GridColumnSelected
        '
        Me.GridColumnSelected.Caption = "Select"
        Me.GridColumnSelected.ColumnEdit = Me.chkSelectHeader
        Me.GridColumnSelected.FieldName = "Selected"
        Me.GridColumnSelected.Name = "GridColumnSelected"
        Me.GridColumnSelected.Visible = True
        Me.GridColumnSelected.VisibleIndex = 0
        Me.GridColumnSelected.Width = 40
        '
        'chkSelectHeader
        '
        Me.chkSelectHeader.Caption = "Check"
        Me.chkSelectHeader.Name = "chkSelectHeader"
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "YesNo"
        Me.GridColumn12.FieldName = "CanProcess"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 1
        Me.GridColumn12.Width = 50
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "RowId"
        Me.GridColumn1.FieldName = "RowId"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 2
        Me.GridColumn1.Width = 50
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Trno"
        Me.GridColumn2.FieldName = "Trno"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 4
        Me.GridColumn2.Width = 50
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Payment"
        Me.GridColumn3.FieldName = "Payment"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 5
        Me.GridColumn3.Width = 56
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn4.Caption = "NetAmt"
        Me.GridColumn4.FieldName = "NetAmt"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NetAmt", "{0:n2}")})
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 6
        Me.GridColumn4.Width = 47
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Date"
        Me.GridColumn10.FieldName = "Date"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 3
        Me.GridColumn10.Width = 50
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Print"
        Me.GridColumn13.FieldName = "PrintStatus"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 7
        Me.GridColumn13.Width = 49
        '
        'lblstatusgroup
        '
        Me.lblstatusgroup.Controls.Add(Me.btnTransferDataDuplicateProcess)
        Me.lblstatusgroup.Controls.Add(Me.btnProcessAll)
        Me.lblstatusgroup.Controls.Add(Me.btnProcess)
        Me.lblstatusgroup.Controls.Add(Me.btnSelectedItemDelete)
        Me.lblstatusgroup.Controls.Add(Me.Label5)
        Me.lblstatusgroup.Controls.Add(Me.txtsetlimit)
        Me.lblstatusgroup.Controls.Add(Me.PanelControl1)
        Me.lblstatusgroup.Controls.Add(Me.txtNoofRow)
        Me.lblstatusgroup.Controls.Add(Me.ListBoxControlPayment)
        Me.lblstatusgroup.Controls.Add(Me.Label4)
        Me.lblstatusgroup.Controls.Add(Me.btnSearch)
        Me.lblstatusgroup.Controls.Add(Me.Label3)
        Me.lblstatusgroup.Controls.Add(Me.Label2)
        Me.lblstatusgroup.Controls.Add(Me.Label1)
        Me.lblstatusgroup.Controls.Add(Me.ToDateEdit)
        Me.lblstatusgroup.Controls.Add(Me.FromDateEdit)
        Me.lblstatusgroup.Location = New System.Drawing.Point(12, 28)
        Me.lblstatusgroup.Name = "lblstatusgroup"
        Me.lblstatusgroup.Size = New System.Drawing.Size(984, 119)
        Me.lblstatusgroup.TabIndex = 4
        Me.lblstatusgroup.Text = "Details :"
        '
        'btnTransferDataDuplicateProcess
        '
        Me.btnTransferDataDuplicateProcess.Location = New System.Drawing.Point(118, 91)
        Me.btnTransferDataDuplicateProcess.Name = "btnTransferDataDuplicateProcess"
        Me.btnTransferDataDuplicateProcess.Size = New System.Drawing.Size(107, 23)
        Me.btnTransferDataDuplicateProcess.TabIndex = 16
        Me.btnTransferDataDuplicateProcess.Text = "Data Transfer"
        '
        'btnProcessAll
        '
        Me.btnProcessAll.Location = New System.Drawing.Point(870, 82)
        Me.btnProcessAll.Name = "btnProcessAll"
        Me.btnProcessAll.Size = New System.Drawing.Size(109, 23)
        Me.btnProcessAll.TabIndex = 13
        Me.btnProcessAll.Text = "Process All "
        Me.btnProcessAll.Visible = False
        '
        'btnProcess
        '
        Me.btnProcess.Location = New System.Drawing.Point(754, 82)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(109, 23)
        Me.btnProcess.TabIndex = 9
        Me.btnProcess.Text = "Process Selected"
        Me.btnProcess.Visible = False
        '
        'btnSelectedItemDelete
        '
        Me.btnSelectedItemDelete.Location = New System.Drawing.Point(845, 53)
        Me.btnSelectedItemDelete.Name = "btnSelectedItemDelete"
        Me.btnSelectedItemDelete.Size = New System.Drawing.Size(134, 23)
        Me.btnSelectedItemDelete.TabIndex = 15
        Me.btnSelectedItemDelete.Text = "Delete Selected Item Only"
        Me.btnSelectedItemDelete.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(809, 27)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Set Limit :"
        Me.Label5.Visible = False
        '
        'txtsetlimit
        '
        Me.txtsetlimit.EditValue = "0"
        Me.txtsetlimit.Location = New System.Drawing.Point(879, 24)
        Me.txtsetlimit.MenuManager = Me.BarManager1
        Me.txtsetlimit.Name = "txtsetlimit"
        Me.txtsetlimit.Properties.DisplayFormat.FormatString = "n0"
        Me.txtsetlimit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtsetlimit.Properties.EditFormat.FormatString = "n0"
        Me.txtsetlimit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtsetlimit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtsetlimit.Size = New System.Drawing.Size(100, 20)
        Me.txtsetlimit.TabIndex = 13
        Me.txtsetlimit.Visible = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btnBulkDelete)
        Me.PanelControl1.Controls.Add(Me.btnDelSelectedTrno)
        Me.PanelControl1.Controls.Add(Me.btnfinalprocess)
        Me.PanelControl1.Location = New System.Drawing.Point(411, 24)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(119, 85)
        Me.PanelControl1.TabIndex = 12
        '
        'btnBulkDelete
        '
        Me.btnBulkDelete.Location = New System.Drawing.Point(5, 31)
        Me.btnBulkDelete.Name = "btnBulkDelete"
        Me.btnBulkDelete.Size = New System.Drawing.Size(109, 23)
        Me.btnBulkDelete.TabIndex = 15
        Me.btnBulkDelete.Text = "Bulk Delete"
        '
        'btnDelSelectedTrno
        '
        Me.btnDelSelectedTrno.Location = New System.Drawing.Point(5, 5)
        Me.btnDelSelectedTrno.Name = "btnDelSelectedTrno"
        Me.btnDelSelectedTrno.Size = New System.Drawing.Size(109, 23)
        Me.btnDelSelectedTrno.TabIndex = 14
        Me.btnDelSelectedTrno.Text = "Del By Invoice"
        '
        'btnfinalprocess
        '
        Me.btnfinalprocess.Location = New System.Drawing.Point(5, 57)
        Me.btnfinalprocess.Name = "btnfinalprocess"
        Me.btnfinalprocess.Size = New System.Drawing.Size(109, 23)
        Me.btnfinalprocess.TabIndex = 11
        Me.btnfinalprocess.Text = "Final Bulk Update"
        '
        'txtNoofRow
        '
        Me.txtNoofRow.EditValue = "2"
        Me.txtNoofRow.Location = New System.Drawing.Point(111, 71)
        Me.txtNoofRow.MenuManager = Me.BarManager1
        Me.txtNoofRow.Name = "txtNoofRow"
        Me.txtNoofRow.Properties.DisplayFormat.FormatString = "n0"
        Me.txtNoofRow.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtNoofRow.Properties.EditFormat.FormatString = "n0"
        Me.txtNoofRow.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtNoofRow.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtNoofRow.Size = New System.Drawing.Size(55, 20)
        Me.txtNoofRow.TabIndex = 3
        '
        'ListBoxControlPayment
        '
        Me.ListBoxControlPayment.Location = New System.Drawing.Point(231, 43)
        Me.ListBoxControlPayment.Name = "ListBoxControlPayment"
        Me.ListBoxControlPayment.Size = New System.Drawing.Size(170, 74)
        Me.ListBoxControlPayment.TabIndex = 11
        Me.ListBoxControlPayment.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "No Of Row Delete :"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(5, 91)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(107, 23)
        Me.btnSearch.TabIndex = 8
        Me.btnSearch.Text = "Search"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(228, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Paymode :"
        Me.Label3.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "To Date :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "From Date :"
        '
        'ToDateEdit
        '
        Me.ToDateEdit.EditValue = Nothing
        Me.ToDateEdit.Location = New System.Drawing.Point(66, 50)
        Me.ToDateEdit.MenuManager = Me.BarManager1
        Me.ToDateEdit.Name = "ToDateEdit"
        Me.ToDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ToDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ToDateEdit.Properties.CalendarTimeProperties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
        Me.ToDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.[Default]
        Me.ToDateEdit.Size = New System.Drawing.Size(100, 20)
        Me.ToDateEdit.TabIndex = 1
        '
        'FromDateEdit
        '
        Me.FromDateEdit.EditValue = Nothing
        Me.FromDateEdit.Location = New System.Drawing.Point(66, 24)
        Me.FromDateEdit.MenuManager = Me.BarManager1
        Me.FromDateEdit.Name = "FromDateEdit"
        Me.FromDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FromDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FromDateEdit.Properties.CalendarTimeProperties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
        Me.FromDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.[Default]
        Me.FromDateEdit.Size = New System.Drawing.Size(100, 20)
        Me.FromDateEdit.TabIndex = 0
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.LayoutControlItem2, Me.LayoutControlItem1, Me.LayoutControlItem4, Me.LayoutControlItem5})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1008, 675)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.GridControlItemList
        Me.LayoutControlItem3.CustomizationFormText = "List Of ItemDetails"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(405, 163)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(583, 492)
        Me.LayoutControlItem3.Text = "List Of ItemDetails"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(104, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.GridControlHeader
        Me.LayoutControlItem2.CustomizationFormText = "List Of Header Details"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 163)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(405, 492)
        Me.LayoutControlItem2.Text = "List Of Header Details"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(104, 13)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.lblstatusgroup
        Me.LayoutControlItem1.CustomizationFormText = "Tax Report Process.."
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(988, 139)
        Me.LayoutControlItem1.Text = "Tax Report Process.."
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(104, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.lblDetailNetAmt
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(405, 139)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(583, 24)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.lblHeaderNetAmt
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 139)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(405, 24)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'FrmGenerateTaxReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "FrmGenerateTaxReport"
        Me.Text = "FrmGenerateTaxReport"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.GridControlItemList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewItemList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControlHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSelectHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblstatusgroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lblstatusgroup.ResumeLayout(False)
        Me.lblstatusgroup.PerformLayout()
        CType(Me.txtsetlimit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.txtNoofRow.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListBoxControlPayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FromDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FromDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents Bar3 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents GridControlItemList As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewItemList As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridControlHeader As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewHeader As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumnSelected As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chkSelectHeader As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents lblstatusgroup As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnProcess As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNoofRow As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ToDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FromDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ListBoxControlPayment As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents lblDetailNetAmt As System.Windows.Forms.Label
    Friend WithEvents lblHeaderNetAmt As System.Windows.Forms.Label
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnfinalprocess As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents barbtnClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnProcessAll As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtsetlimit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnSelectedItemDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnTransferDataDuplicateProcess As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDelSelectedTrno As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BarSubItem1 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents barbtnpurgebydate As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barbtnpurgebymonth As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnBulkDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents barSelectedNetAmt As DevExpress.XtraBars.BarStaticItem
End Class
