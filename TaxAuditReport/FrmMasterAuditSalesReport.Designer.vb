<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMasterAuditSalesReport
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.FromDate = New DevExpress.XtraBars.BarEditItem()
        Me.repFromDate = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.ToDate = New DevExpress.XtraBars.BarEditItem()
        Me.repToDate = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.barbtnHeader = New DevExpress.XtraBars.BarButtonItem()
        Me.barbtnDetail = New DevExpress.XtraBars.BarButtonItem()
        Me.barbtnPaymode = New DevExpress.XtraBars.BarButtonItem()
        Me.barbtnExport = New DevExpress.XtraBars.BarButtonItem()
        Me.barbtnPrint = New DevExpress.XtraBars.BarButtonItem()
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.tabHeader = New DevExpress.XtraTab.XtraTabPage()
        Me.GridControlHeader = New DevExpress.XtraGrid.GridControl()
        Me.GridViewHeader = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabDetail = New DevExpress.XtraTab.XtraTabPage()
        Me.GridControlDetail = New DevExpress.XtraGrid.GridControl()
        Me.GridViewDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabPaymode = New DevExpress.XtraTab.XtraTabPage()
        Me.GridControlPaymode = New DevExpress.XtraGrid.GridControl()
        Me.GridViewPaymode = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repFromDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repToDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.tabHeader.SuspendLayout()
        CType(Me.GridControlHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabDetail.SuspendLayout()
        CType(Me.GridControlDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPaymode.SuspendLayout()
        CType(Me.GridControlPaymode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewPaymode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1, Me.Bar2})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.FromDate, Me.ToDate, Me.barbtnHeader, Me.barbtnDetail, Me.barbtnPaymode, Me.barbtnExport, Me.barbtnPrint})
        Me.BarManager1.MaxItemId = 7
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.repFromDate, Me.repToDate})
        Me.BarManager1.StatusBar = Me.Bar2
        '
        'Bar1
        '
        Me.Bar1.BarName = "Tools"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {
            New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.FromDate, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.ToDate, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barbtnHeader, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barbtnDetail, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barbtnPaymode, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barbtnExport, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barbtnPrint, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar1.OptionsBar.MultiLine = True
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Tools"
        '
        'FromDate
        '
        Me.FromDate.Caption = "From :"
        Me.FromDate.Edit = Me.repFromDate
        Me.FromDate.Id = 0
        Me.FromDate.Name = "FromDate"
        Me.FromDate.Width = 121
        '
        'repFromDate
        '
        Me.repFromDate.AutoHeight = False
        Me.repFromDate.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.repFromDate.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.repFromDate.CalendarTimeProperties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
        Me.repFromDate.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.[Default]
        Me.repFromDate.Name = "repFromDate"
        '
        'ToDate
        '
        Me.ToDate.Caption = "To :"
        Me.ToDate.Edit = Me.repToDate
        Me.ToDate.Id = 1
        Me.ToDate.Name = "ToDate"
        Me.ToDate.Width = 121
        '
        'repToDate
        '
        Me.repToDate.AutoHeight = False
        Me.repToDate.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.repToDate.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.repToDate.CalendarTimeProperties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
        Me.repToDate.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.[Default]
        Me.repToDate.Name = "repToDate"
        '
        'barbtnHeader
        '
        Me.barbtnHeader.Caption = "Load Header"
        Me.barbtnHeader.Id = 2
        Me.barbtnHeader.Name = "barbtnHeader"
        '
        'barbtnDetail
        '
        Me.barbtnDetail.Caption = "Load Detail"
        Me.barbtnDetail.Id = 3
        Me.barbtnDetail.Name = "barbtnDetail"
        '
        'barbtnPaymode
        '
        Me.barbtnPaymode.Caption = "Load Paymode"
        Me.barbtnPaymode.Id = 4
        Me.barbtnPaymode.Name = "barbtnPaymode"
        '
        'barbtnExport
        '
        Me.barbtnExport.Caption = "Export"
        Me.barbtnExport.Id = 5
        Me.barbtnExport.Name = "barbtnExport"
        '
        'barbtnPrint
        '
        Me.barbtnPrint.Caption = "Print"
        Me.barbtnPrint.Id = 6
        Me.barbtnPrint.Name = "barbtnPrint"
        '
        'Bar2 (status bar)
        '
        Me.Bar2.BarName = "Status bar"
        Me.Bar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.Bar2.OptionsBar.AllowQuickCustomization = False
        Me.Bar2.OptionsBar.DrawDragBorder = False
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Status bar"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1100, 29)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 623)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1100, 23)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 29)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 594)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1100, 29)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 594)
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 29)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.tabHeader
        Me.XtraTabControl1.Size = New System.Drawing.Size(1100, 594)
        Me.XtraTabControl1.TabIndex = 0
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabHeader, Me.tabDetail, Me.tabPaymode})
        '
        'tabHeader
        '
        Me.tabHeader.Controls.Add(Me.GridControlHeader)
        Me.tabHeader.Name = "tabHeader"
        Me.tabHeader.Size = New System.Drawing.Size(1098, 568)
        Me.tabHeader.Text = "Header"
        '
        'GridControlHeader
        '
        Me.GridControlHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControlHeader.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GridControlHeader.Location = New System.Drawing.Point(0, 0)
        Me.GridControlHeader.MainView = Me.GridViewHeader
        Me.GridControlHeader.Name = "GridControlHeader"
        Me.GridControlHeader.Size = New System.Drawing.Size(1098, 568)
        Me.GridControlHeader.TabIndex = 0
        Me.GridControlHeader.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewHeader})
        '
        'GridViewHeader
        '
        Me.GridViewHeader.GridControl = Me.GridControlHeader
        Me.GridViewHeader.Name = "GridViewHeader"
        Me.GridViewHeader.OptionsView.ShowGroupPanel = True
        Me.GridViewHeader.OptionsView.ShowFooter = True
        Me.GridViewHeader.OptionsView.ShowAutoFilterRow = True
        '
        'tabDetail
        '
        Me.tabDetail.Controls.Add(Me.GridControlDetail)
        Me.tabDetail.Name = "tabDetail"
        Me.tabDetail.Size = New System.Drawing.Size(1098, 568)
        Me.tabDetail.Text = "Detail"
        '
        'GridControlDetail
        '
        Me.GridControlDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControlDetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GridControlDetail.Location = New System.Drawing.Point(0, 0)
        Me.GridControlDetail.MainView = Me.GridViewDetail
        Me.GridControlDetail.Name = "GridControlDetail"
        Me.GridControlDetail.Size = New System.Drawing.Size(1098, 568)
        Me.GridControlDetail.TabIndex = 0
        Me.GridControlDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewDetail})
        '
        'GridViewDetail
        '
        Me.GridViewDetail.GridControl = Me.GridControlDetail
        Me.GridViewDetail.Name = "GridViewDetail"
        Me.GridViewDetail.OptionsView.ShowGroupPanel = True
        Me.GridViewDetail.OptionsView.ShowFooter = True
        Me.GridViewDetail.OptionsView.ShowAutoFilterRow = True
        '
        'tabPaymode
        '
        Me.tabPaymode.Controls.Add(Me.GridControlPaymode)
        Me.tabPaymode.Name = "tabPaymode"
        Me.tabPaymode.Size = New System.Drawing.Size(1098, 568)
        Me.tabPaymode.Text = "Paymode"
        '
        'GridControlPaymode
        '
        Me.GridControlPaymode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControlPaymode.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GridControlPaymode.Location = New System.Drawing.Point(0, 0)
        Me.GridControlPaymode.MainView = Me.GridViewPaymode
        Me.GridControlPaymode.Name = "GridControlPaymode"
        Me.GridControlPaymode.Size = New System.Drawing.Size(1098, 568)
        Me.GridControlPaymode.TabIndex = 0
        Me.GridControlPaymode.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewPaymode})
        '
        'GridViewPaymode
        '
        Me.GridViewPaymode.GridControl = Me.GridControlPaymode
        Me.GridViewPaymode.Name = "GridViewPaymode"
        Me.GridViewPaymode.OptionsView.ShowGroupPanel = True
        Me.GridViewPaymode.OptionsView.ShowFooter = True
        Me.GridViewPaymode.OptionsView.ShowAutoFilterRow = True
        '
        'FrmMasterAuditSalesReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1100, 646)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "FrmMasterAuditSalesReport"
        Me.Text = "Master Audit Sales Report"
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repFromDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repToDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControlHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabHeader.ResumeLayout(False)
        CType(Me.GridViewDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControlDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabDetail.ResumeLayout(False)
        CType(Me.GridViewPaymode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControlPaymode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPaymode.ResumeLayout(False)
        Me.XtraTabControl1.ResumeLayout(False)
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents FromDate As DevExpress.XtraBars.BarEditItem
    Friend WithEvents repFromDate As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents ToDate As DevExpress.XtraBars.BarEditItem
    Friend WithEvents repToDate As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents barbtnHeader As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barbtnDetail As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barbtnPaymode As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barbtnExport As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barbtnPrint As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabHeader As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GridControlHeader As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewHeader As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tabDetail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GridControlDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tabPaymode As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GridControlPaymode As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewPaymode As DevExpress.XtraGrid.Views.Grid.GridView
End Class
