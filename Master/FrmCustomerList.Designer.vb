<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCustomerList
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.GridControlCustomers = New DevExpress.XtraGrid.GridControl()
        Me.GridViewCustomers = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDeleteCustomer = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEditCustomer = New DevExpress.XtraEditors.SimpleButton()
        Me.btnNewCustomer = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.GridControlCustomers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewCustomers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.PanelControl1.Appearance.Options.UseBackColor = True
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(784, 35)
        Me.PanelControl1.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.DarkBlue
        Me.LabelControl1.Location = New System.Drawing.Point(12, 8)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(140, 19)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Customer Manager"
        '
        'GridControlCustomers
        '
        Me.GridControlCustomers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridControlCustomers.Location = New System.Drawing.Point(12, 41)
        Me.GridControlCustomers.MainView = Me.GridViewCustomers
        Me.GridControlCustomers.Name = "GridControlCustomers"
        Me.GridControlCustomers.Size = New System.Drawing.Size(760, 400)
        Me.GridControlCustomers.TabIndex = 1
        Me.GridControlCustomers.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewCustomers})
        '
        'GridViewCustomers
        '
        Me.GridViewCustomers.GridControl = Me.GridControlCustomers
        Me.GridViewCustomers.Name = "GridViewCustomers"
        Me.GridViewCustomers.OptionsView.ShowGroupPanel = False
        Me.GridViewCustomers.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridViewCustomers.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridViewCustomers.OptionsView.ShowAutoFilterRow = True
        Me.GridViewCustomers.OptionsSelection.MultiSelect = False
        '
        'PanelControl2
        '
        Me.PanelControl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl2.Controls.Add(Me.btnRefresh)
        Me.PanelControl2.Controls.Add(Me.btnDeleteCustomer)
        Me.PanelControl2.Controls.Add(Me.btnEditCustomer)
        Me.PanelControl2.Controls.Add(Me.btnNewCustomer)
        Me.PanelControl2.Location = New System.Drawing.Point(12, 447)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(760, 40)
        Me.PanelControl2.TabIndex = 2
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.btnRefresh.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnRefresh.Appearance.Options.UseBackColor = True
        Me.btnRefresh.Appearance.Options.UseFont = True
        Me.btnRefresh.Appearance.Options.UseForeColor = True
        Me.btnRefresh.Location = New System.Drawing.Point(675, 5)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(85, 30)
        Me.btnRefresh.TabIndex = 6
        Me.btnRefresh.Text = "Refresh (F5)"
        '
        'btnDeleteCustomer
        '
        Me.btnDeleteCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteCustomer.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnDeleteCustomer.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnDeleteCustomer.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnDeleteCustomer.Appearance.Options.UseBackColor = True
        Me.btnDeleteCustomer.Appearance.Options.UseFont = True
        Me.btnDeleteCustomer.Appearance.Options.UseForeColor = True
        Me.btnDeleteCustomer.Location = New System.Drawing.Point(584, 5)
        Me.btnDeleteCustomer.Name = "btnDeleteCustomer"
        Me.btnDeleteCustomer.Size = New System.Drawing.Size(85, 30)
        Me.btnDeleteCustomer.TabIndex = 5
        Me.btnDeleteCustomer.Text = "Delete (Del)"
        '
        'btnEditCustomer
        '
        Me.btnEditCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEditCustomer.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.btnEditCustomer.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnEditCustomer.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnEditCustomer.Appearance.Options.UseBackColor = True
        Me.btnEditCustomer.Appearance.Options.UseFont = True
        Me.btnEditCustomer.Appearance.Options.UseForeColor = True
        Me.btnEditCustomer.Location = New System.Drawing.Point(493, 5)
        Me.btnEditCustomer.Name = "btnEditCustomer"
        Me.btnEditCustomer.Size = New System.Drawing.Size(85, 30)
        Me.btnEditCustomer.TabIndex = 4
        Me.btnEditCustomer.Text = "Edit (Enter)"
        '
        'btnNewCustomer
        '
        Me.btnNewCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewCustomer.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnNewCustomer.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnNewCustomer.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnNewCustomer.Appearance.Options.UseBackColor = True
        Me.btnNewCustomer.Appearance.Options.UseFont = True
        Me.btnNewCustomer.Appearance.Options.UseForeColor = True
        Me.btnNewCustomer.Location = New System.Drawing.Point(402, 5)
        Me.btnNewCustomer.Name = "btnNewCustomer"
        Me.btnNewCustomer.Size = New System.Drawing.Size(85, 30)
        Me.btnNewCustomer.TabIndex = 3
        Me.btnNewCustomer.Text = "New (Insert)"
        '
        'FrmCustomerList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 499)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.GridControlCustomers)
        Me.Controls.Add(Me.PanelControl1)
        Me.MinimizeBox = False
        Me.Name = "FrmCustomerList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Customer List Manager"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.GridControlCustomers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewCustomers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GridControlCustomers As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewCustomers As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteCustomer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEditCustomer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnNewCustomer As DevExpress.XtraEditors.SimpleButton
End Class
