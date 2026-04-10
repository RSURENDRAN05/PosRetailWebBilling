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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFingerRegister))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.errorRichBox = New System.Windows.Forms.RichTextBox()
        Me.GridControlEmpFingerDtl = New DevExpress.XtraGrid.GridControl()
        Me.GridViewEmpFingerDtl = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.nudFingerIndex = New System.Windows.Forms.NumericUpDown()
        Me.lbltype = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnstartCapture = New DevExpress.XtraEditors.SimpleButton()
        Me.lblempname = New System.Windows.Forms.Label()
        Me.lblempid = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.btnTest = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClear = New DevExpress.XtraEditors.SimpleButton()
        Me.lblstatusimage = New DevExpress.XtraEditors.PictureEdit()
        Me.lblfingerimage = New DevExpress.XtraEditors.PictureEdit()
        Me.GridControlEmpHeader = New DevExpress.XtraGrid.GridControl()
        Me.GridViewEmpHeader = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.Img = New DevExpress.Utils.ImageCollection(Me.components)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.GridControlEmpFingerDtl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewEmpFingerDtl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.nudFingerIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblstatusimage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfingerimage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControlEmpHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewEmpHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Img, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.errorRichBox)
        Me.LayoutControl1.Controls.Add(Me.GridControlEmpFingerDtl)
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
        'errorRichBox
        '
        Me.errorRichBox.Location = New System.Drawing.Point(12, 464)
        Me.errorRichBox.Name = "errorRichBox"
        Me.errorRichBox.Size = New System.Drawing.Size(417, 253)
        Me.errorRichBox.TabIndex = 7
        Me.errorRichBox.Text = ""
        '
        'GridControlEmpFingerDtl
        '
        Me.GridControlEmpFingerDtl.Location = New System.Drawing.Point(433, 28)
        Me.GridControlEmpFingerDtl.MainView = Me.GridViewEmpFingerDtl
        Me.GridControlEmpFingerDtl.Name = "GridControlEmpFingerDtl"
        Me.GridControlEmpFingerDtl.Size = New System.Drawing.Size(563, 308)
        Me.GridControlEmpFingerDtl.TabIndex = 6
        Me.GridControlEmpFingerDtl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewEmpFingerDtl})
        '
        'GridViewEmpFingerDtl
        '
        Me.GridViewEmpFingerDtl.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridViewEmpFingerDtl.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewEmpFingerDtl.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridViewEmpFingerDtl.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewEmpFingerDtl.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridViewEmpFingerDtl.Appearance.Row.Options.UseFont = True
        Me.GridViewEmpFingerDtl.Appearance.Row.Options.UseTextOptions = True
        Me.GridViewEmpFingerDtl.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewEmpFingerDtl.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7})
        Me.GridViewEmpFingerDtl.GridControl = Me.GridControlEmpFingerDtl
        Me.GridViewEmpFingerDtl.Name = "GridViewEmpFingerDtl"
        Me.GridViewEmpFingerDtl.OptionsBehavior.Editable = False
        Me.GridViewEmpFingerDtl.OptionsBehavior.ReadOnly = True
        Me.GridViewEmpFingerDtl.OptionsView.ShowGroupPanel = False
        Me.GridViewEmpFingerDtl.RowHeight = 30
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Id"
        Me.GridColumn3.FieldName = "id"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Width = 41
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "RefId"
        Me.GridColumn4.FieldName = "emp_id"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        Me.GridColumn4.Width = 46
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Name"
        Me.GridColumn5.FieldName = "emp_printname"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        Me.GridColumn5.Width = 152
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "FingerName"
        Me.GridColumn6.FieldName = "finger_name"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 2
        Me.GridColumn6.Width = 152
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Type"
        Me.GridColumn7.FieldName = "fingertype"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 3
        Me.GridColumn7.Width = 154
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.nudFingerIndex)
        Me.PanelControl1.Controls.Add(Me.lbltype)
        Me.PanelControl1.Controls.Add(Me.Label4)
        Me.PanelControl1.Controls.Add(Me.Label3)
        Me.PanelControl1.Controls.Add(Me.btnstartCapture)
        Me.PanelControl1.Controls.Add(Me.lblempname)
        Me.PanelControl1.Controls.Add(Me.lblempid)
        Me.PanelControl1.Controls.Add(Me.Label2)
        Me.PanelControl1.Controls.Add(Me.Label1)
        Me.PanelControl1.Controls.Add(Me.btnDelete)
        Me.PanelControl1.Controls.Add(Me.btnRefresh)
        Me.PanelControl1.Controls.Add(Me.btnTest)
        Me.PanelControl1.Controls.Add(Me.btnClear)
        Me.PanelControl1.Controls.Add(Me.lblstatusimage)
        Me.PanelControl1.Controls.Add(Me.lblfingerimage)
        Me.PanelControl1.Location = New System.Drawing.Point(433, 356)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(563, 361)
        Me.PanelControl1.TabIndex = 5
        '
        'nudFingerIndex
        '
        Me.nudFingerIndex.Location = New System.Drawing.Point(94, 17)
        Me.nudFingerIndex.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudFingerIndex.Name = "nudFingerIndex"
        Me.nudFingerIndex.Size = New System.Drawing.Size(50, 21)
        Me.nudFingerIndex.TabIndex = 22
        Me.nudFingerIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudFingerIndex.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lbltype
        '
        Me.lbltype.AutoSize = True
        Me.lbltype.Location = New System.Drawing.Point(106, 111)
        Me.lbltype.Name = "lbltype"
        Me.lbltype.Size = New System.Drawing.Size(38, 13)
        Me.lbltype.TabIndex = 21
        Me.lbltype.Text = "Label4"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Finger Index :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(35, 111)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Type  :"
        '
        'btnstartCapture
        '
        Me.btnstartCapture.Image = CType(resources.GetObject("btnstartCapture.Image"), System.Drawing.Image)
        Me.btnstartCapture.Location = New System.Drawing.Point(428, 171)
        Me.btnstartCapture.Name = "btnstartCapture"
        Me.btnstartCapture.Size = New System.Drawing.Size(120, 46)
        Me.btnstartCapture.TabIndex = 11
        Me.btnstartCapture.Text = "Enroll Employee"
        '
        'lblempname
        '
        Me.lblempname.AutoSize = True
        Me.lblempname.Location = New System.Drawing.Point(106, 85)
        Me.lblempname.Name = "lblempname"
        Me.lblempname.Size = New System.Drawing.Size(38, 13)
        Me.lblempname.TabIndex = 10
        Me.lblempname.Text = "Label4"
        '
        'lblempid
        '
        Me.lblempid.AutoSize = True
        Me.lblempid.Location = New System.Drawing.Point(106, 50)
        Me.lblempid.Name = "lblempid"
        Me.lblempid.Size = New System.Drawing.Size(38, 13)
        Me.lblempid.TabIndex = 9
        Me.lblempid.Text = "Label3"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "EmpName :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(35, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "EmpId :"
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Location = New System.Drawing.Point(288, 229)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(124, 46)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Delete"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.Location = New System.Drawing.Point(428, 229)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(124, 46)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.Text = "Refresh"
        '
        'btnTest
        '
        Me.btnTest.Image = CType(resources.GetObject("btnTest.Image"), System.Drawing.Image)
        Me.btnTest.Location = New System.Drawing.Point(148, 229)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(124, 46)
        Me.btnTest.TabIndex = 4
        Me.btnTest.Text = "Verify "
        '
        'btnClear
        '
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(8, 229)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(124, 46)
        Me.btnClear.TabIndex = 3
        Me.btnClear.Text = "Clear"
        '
        'lblstatusimage
        '
        Me.lblstatusimage.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblstatusimage.EditValue = CType(resources.GetObject("lblstatusimage.EditValue"), Object)
        Me.lblstatusimage.Location = New System.Drawing.Point(2, 281)
        Me.lblstatusimage.Name = "lblstatusimage"
        Me.lblstatusimage.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblstatusimage.Properties.Appearance.Options.UseBackColor = True
        Me.lblstatusimage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        Me.lblstatusimage.Size = New System.Drawing.Size(559, 78)
        Me.lblstatusimage.TabIndex = 1
        '
        'lblfingerimage
        '
        Me.lblfingerimage.Location = New System.Drawing.Point(428, 5)
        Me.lblfingerimage.Name = "lblfingerimage"
        Me.lblfingerimage.Size = New System.Drawing.Size(120, 160)
        Me.lblfingerimage.TabIndex = 0
        '
        'GridControlEmpHeader
        '
        Me.GridControlEmpHeader.Location = New System.Drawing.Point(12, 28)
        Me.GridControlEmpHeader.MainView = Me.GridViewEmpHeader
        Me.GridControlEmpHeader.Name = "GridControlEmpHeader"
        Me.GridControlEmpHeader.Size = New System.Drawing.Size(417, 416)
        Me.GridControlEmpHeader.TabIndex = 4
        Me.GridControlEmpHeader.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewEmpHeader})
        '
        'GridViewEmpHeader
        '
        Me.GridViewEmpHeader.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.GridViewEmpHeader.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewEmpHeader.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridViewEmpHeader.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewEmpHeader.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GridViewEmpHeader.Appearance.Row.Options.UseFont = True
        Me.GridViewEmpHeader.Appearance.Row.Options.UseTextOptions = True
        Me.GridViewEmpHeader.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewEmpHeader.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn8})
        Me.GridViewEmpHeader.GridControl = Me.GridControlEmpHeader
        Me.GridViewEmpHeader.Name = "GridViewEmpHeader"
        Me.GridViewEmpHeader.OptionsBehavior.Editable = False
        Me.GridViewEmpHeader.OptionsBehavior.ReadOnly = True
        Me.GridViewEmpHeader.OptionsView.ShowGroupPanel = False
        Me.GridViewEmpHeader.RowHeight = 30
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "EmpId"
        Me.GridColumn1.FieldName = "emp_id"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 66
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "EmpName"
        Me.GridColumn2.FieldName = "emp_printname"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 248
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Type"
        Me.GridColumn8.FieldName = "emp_type"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 2
        Me.GridColumn8.Width = 85
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4})
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
        Me.LayoutControlItem1.Size = New System.Drawing.Size(421, 436)
        Me.LayoutControlItem1.Text = "Employee Info"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(69, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.PanelControl1
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(421, 328)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(567, 381)
        Me.LayoutControlItem2.Text = "Process"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(69, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.GridControlEmpFingerDtl
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(421, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(567, 328)
        Me.LayoutControlItem3.Text = "Finger Details"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(69, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.errorRichBox
        Me.LayoutControlItem4.CustomizationFormText = "Error Log"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 436)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(421, 273)
        Me.LayoutControlItem4.Text = "Error Log"
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(69, 13)
        '
        'Img
        '
        Me.Img.ImageSize = New System.Drawing.Size(144, 144)
        Me.Img.ImageStream = CType(resources.GetObject("Img.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.Img.Images.SetKeyName(0, "red-success-144.png")
        Me.Img.Images.SetKeyName(1, "green-success-144.png")
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
        CType(Me.GridControlEmpFingerDtl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewEmpFingerDtl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.nudFingerIndex, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblstatusimage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfingerimage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControlEmpHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewEmpHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Img, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridControlEmpFingerDtl As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewEmpFingerDtl As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnTest As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnClear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblstatusimage As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents lblfingerimage As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents GridControlEmpHeader As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewEmpHeader As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lblempname As System.Windows.Forms.Label
    Friend WithEvents lblempid As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnstartCapture As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Img As DevExpress.Utils.ImageCollection
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lbltype As System.Windows.Forms.Label
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents errorRichBox As System.Windows.Forms.RichTextBox
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents nudFingerIndex As System.Windows.Forms.NumericUpDown
End Class
