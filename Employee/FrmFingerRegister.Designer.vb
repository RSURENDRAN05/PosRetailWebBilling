<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFingerRegister
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFingerRegister))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.GridControlEmpFingerHeader = New DevExpress.XtraGrid.GridControl()
        Me.GridViewEmpFingerHeader = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.btnTest = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClear = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.lblstatusimage = New DevExpress.XtraEditors.PictureEdit()
        Me.lblfingerimage = New DevExpress.XtraEditors.PictureEdit()
        Me.GridControlEmpHeader = New DevExpress.XtraGrid.GridControl()
        Me.GridViewEmpHeader = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.GridControlEmpFingerHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewEmpFingerHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.lblstatusimage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfingerimage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControlEmpHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewEmpHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.GridControlEmpFingerHeader)
        Me.LayoutControl1.Controls.Add(Me.PanelControl1)
        Me.LayoutControl1.Controls.Add(Me.GridControlEmpHeader)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1008, 729)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'GridControlEmpFingerHeader
        '
        Me.GridControlEmpFingerHeader.Location = New System.Drawing.Point(433, 28)
        Me.GridControlEmpFingerHeader.MainView = Me.GridViewEmpFingerHeader
        Me.GridControlEmpFingerHeader.Name = "GridControlEmpFingerHeader"
        Me.GridControlEmpFingerHeader.Size = New System.Drawing.Size(563, 340)
        Me.GridControlEmpFingerHeader.TabIndex = 6
        Me.GridControlEmpFingerHeader.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewEmpFingerHeader})
        '
        'GridViewEmpFingerHeader
        '
        Me.GridViewEmpFingerHeader.GridControl = Me.GridControlEmpFingerHeader
        Me.GridViewEmpFingerHeader.Name = "GridViewEmpFingerHeader"
        Me.GridViewEmpFingerHeader.OptionsView.ShowGroupPanel = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btnDelete)
        Me.PanelControl1.Controls.Add(Me.btnRefresh)
        Me.PanelControl1.Controls.Add(Me.btnTest)
        Me.PanelControl1.Controls.Add(Me.btnClear)
        Me.PanelControl1.Controls.Add(Me.btnSave)
        Me.PanelControl1.Controls.Add(Me.lblstatusimage)
        Me.PanelControl1.Controls.Add(Me.lblfingerimage)
        Me.PanelControl1.Location = New System.Drawing.Point(433, 388)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(563, 329)
        Me.PanelControl1.TabIndex = 5
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(341, 185)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(101, 46)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Delete"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(447, 185)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(101, 46)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.Text = "Refresh"
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(235, 185)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(101, 46)
        Me.btnTest.TabIndex = 4
        Me.btnTest.Text = "Finger Test"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(129, 185)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(101, 46)
        Me.btnClear.TabIndex = 3
        Me.btnClear.Text = "Clear"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(23, 185)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(101, 46)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save"
        '
        'lblstatusimage
        '
        Me.lblstatusimage.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblstatusimage.EditValue = CType(resources.GetObject("lblstatusimage.EditValue"), Object)
        Me.lblstatusimage.Location = New System.Drawing.Point(2, 237)
        Me.lblstatusimage.Name = "lblstatusimage"
        Me.lblstatusimage.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblstatusimage.Properties.Appearance.Options.UseBackColor = True
        Me.lblstatusimage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        Me.lblstatusimage.Size = New System.Drawing.Size(559, 90)
        Me.lblstatusimage.TabIndex = 1
        '
        'lblfingerimage
        '
        Me.lblfingerimage.Location = New System.Drawing.Point(428, 19)
        Me.lblfingerimage.Name = "lblfingerimage"
        Me.lblfingerimage.Size = New System.Drawing.Size(120, 160)
        Me.lblfingerimage.TabIndex = 0
        '
        'GridControlEmpHeader
        '
        Me.GridControlEmpHeader.Location = New System.Drawing.Point(12, 28)
        Me.GridControlEmpHeader.MainView = Me.GridViewEmpHeader
        Me.GridControlEmpHeader.Name = "GridControlEmpHeader"
        Me.GridControlEmpHeader.Size = New System.Drawing.Size(417, 689)
        Me.GridControlEmpHeader.TabIndex = 4
        Me.GridControlEmpHeader.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewEmpHeader})
        '
        'GridViewEmpHeader
        '
        Me.GridViewEmpHeader.GridControl = Me.GridControlEmpHeader
        Me.GridViewEmpHeader.Name = "GridViewEmpHeader"
        Me.GridViewEmpHeader.OptionsView.ShowGroupPanel = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1008, 729)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.GridControlEmpHeader
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(421, 709)
        Me.LayoutControlItem1.Text = "Employee Info"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(69, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.PanelControl1
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(421, 360)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(567, 349)
        Me.LayoutControlItem2.Text = "Process"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(69, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.GridControlEmpFingerHeader
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(421, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(567, 360)
        Me.LayoutControlItem3.Text = "Finger Details"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(69, 13)
        '
        'FrmFingerRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "FrmFingerRegister"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Finger Register"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.GridControlEmpFingerHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewEmpFingerHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.lblstatusimage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfingerimage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControlEmpHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewEmpHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridControlEmpFingerHeader As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewEmpFingerHeader As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnTest As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnClear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblstatusimage As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents lblfingerimage As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents GridControlEmpHeader As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewEmpHeader As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
End Class
