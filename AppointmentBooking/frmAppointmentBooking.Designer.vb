<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAppointmentBooking
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAppointmentBooking))
        Me.layoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.txtCustomerId = New DevExpress.XtraEditors.TextEdit()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.gridAppointments = New DevExpress.XtraGrid.GridControl()
        Me.gridViewAppointments = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtCustomerName = New DevExpress.XtraEditors.TextEdit()
        Me.txtEmailAddress = New DevExpress.XtraEditors.TextEdit()
        Me.dateAppointment = New DevExpress.XtraEditors.DateEdit()
        Me.timeStart = New DevExpress.XtraEditors.TimeEdit()
        Me.timeEnd = New DevExpress.XtraEditors.TimeEdit()
        Me.spinReminderValue = New DevExpress.XtraEditors.SpinEdit()
        Me.cboReminderUnit = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.memoNotes = New DevExpress.XtraEditors.MemoEdit()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClear = New DevExpress.XtraEditors.SimpleButton()
        Me.layoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.layoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.layoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.layoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl1.SuspendLayout()
        CType(Me.txtCustomerId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridAppointments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridViewAppointments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmailAddress.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateAppointment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateAppointment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.timeStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.timeEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spinReminderValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboReminderUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.memoNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl1
        '
        Me.layoutControl1.Controls.Add(Me.txtCustomerId)
        Me.layoutControl1.Controls.Add(Me.btnRefresh)
        Me.layoutControl1.Controls.Add(Me.btnDelete)
        Me.layoutControl1.Controls.Add(Me.gridAppointments)
        Me.layoutControl1.Controls.Add(Me.txtCustomerName)
        Me.layoutControl1.Controls.Add(Me.txtEmailAddress)
        Me.layoutControl1.Controls.Add(Me.dateAppointment)
        Me.layoutControl1.Controls.Add(Me.timeStart)
        Me.layoutControl1.Controls.Add(Me.timeEnd)
        Me.layoutControl1.Controls.Add(Me.spinReminderValue)
        Me.layoutControl1.Controls.Add(Me.cboReminderUnit)
        Me.layoutControl1.Controls.Add(Me.memoNotes)
        Me.layoutControl1.Controls.Add(Me.btnSave)
        Me.layoutControl1.Controls.Add(Me.btnClear)
        Me.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl1.Name = "layoutControl1"
        Me.layoutControl1.Root = Me.layoutControlGroup1
        Me.layoutControl1.Size = New System.Drawing.Size(965, 639)
        Me.layoutControl1.TabIndex = 0
        Me.layoutControl1.Text = "layoutControl1"
        '
        'txtCustomerId
        '
        Me.txtCustomerId.Location = New System.Drawing.Point(106, 12)
        Me.txtCustomerId.Name = "txtCustomerId"
        Me.txtCustomerId.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.txtCustomerId.Properties.Appearance.Options.UseFont = True
        Me.txtCustomerId.Size = New System.Drawing.Size(117, 30)
        Me.txtCustomerId.StyleController = Me.layoutControl1
        Me.txtCustomerId.TabIndex = 18
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.Location = New System.Drawing.Point(481, 325)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(293, 38)
        Me.btnRefresh.StyleController = Me.layoutControl1
        Me.btnRefresh.TabIndex = 17
        Me.btnRefresh.Text = "Refresh List"
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Location = New System.Drawing.Point(285, 325)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(192, 38)
        Me.btnDelete.StyleController = Me.layoutControl1
        Me.btnDelete.TabIndex = 15
        Me.btnDelete.Text = "Delete"
        '
        'gridAppointments
        '
        Me.gridAppointments.Location = New System.Drawing.Point(12, 383)
        Me.gridAppointments.MainView = Me.gridViewAppointments
        Me.gridAppointments.Name = "gridAppointments"
        Me.gridAppointments.Size = New System.Drawing.Size(941, 244)
        Me.gridAppointments.TabIndex = 14
        Me.gridAppointments.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridViewAppointments})
        '
        'gridViewAppointments
        '
        Me.gridViewAppointments.GridControl = Me.gridAppointments
        Me.gridViewAppointments.Name = "gridViewAppointments"
        Me.gridViewAppointments.OptionsBehavior.Editable = False
        Me.gridViewAppointments.OptionsView.ShowAutoFilterRow = True
        Me.gridViewAppointments.OptionsView.ShowGroupPanel = False
        '
        'txtCustomerName
        '
        Me.txtCustomerName.Location = New System.Drawing.Point(321, 12)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.txtCustomerName.Properties.Appearance.Options.UseFont = True
        Me.txtCustomerName.Size = New System.Drawing.Size(632, 30)
        Me.txtCustomerName.StyleController = Me.layoutControl1
        Me.txtCustomerName.TabIndex = 4
        '
        'txtEmailAddress
        '
        Me.txtEmailAddress.Location = New System.Drawing.Point(106, 46)
        Me.txtEmailAddress.Name = "txtEmailAddress"
        Me.txtEmailAddress.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.txtEmailAddress.Properties.Appearance.Options.UseFont = True
        Me.txtEmailAddress.Size = New System.Drawing.Size(847, 30)
        Me.txtEmailAddress.StyleController = Me.layoutControl1
        Me.txtEmailAddress.TabIndex = 5
        '
        'dateAppointment
        '
        Me.dateAppointment.EditValue = Nothing
        Me.dateAppointment.Location = New System.Drawing.Point(106, 80)
        Me.dateAppointment.Name = "dateAppointment"
        Me.dateAppointment.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.dateAppointment.Properties.Appearance.Options.UseFont = True
        Me.dateAppointment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dateAppointment.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dateAppointment.Properties.CalendarTimeProperties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
        Me.dateAppointment.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.[Default]
        Me.dateAppointment.Size = New System.Drawing.Size(847, 30)
        Me.dateAppointment.StyleController = Me.layoutControl1
        Me.dateAppointment.TabIndex = 6
        '
        'timeStart
        '
        Me.timeStart.EditValue = New Date(2026, 1, 21, 0, 0, 0, 0)
        Me.timeStart.Location = New System.Drawing.Point(106, 114)
        Me.timeStart.Name = "timeStart"
        Me.timeStart.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.timeStart.Properties.Appearance.Options.UseFont = True
        Me.timeStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.timeStart.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
        Me.timeStart.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.[Default]
        Me.timeStart.Size = New System.Drawing.Size(847, 30)
        Me.timeStart.StyleController = Me.layoutControl1
        Me.timeStart.TabIndex = 7
        '
        'timeEnd
        '
        Me.timeEnd.EditValue = New Date(2026, 1, 21, 0, 0, 0, 0)
        Me.timeEnd.Location = New System.Drawing.Point(106, 148)
        Me.timeEnd.Name = "timeEnd"
        Me.timeEnd.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.timeEnd.Properties.Appearance.Options.UseFont = True
        Me.timeEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.timeEnd.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4)
        Me.timeEnd.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.[Default]
        Me.timeEnd.Size = New System.Drawing.Size(847, 30)
        Me.timeEnd.StyleController = Me.layoutControl1
        Me.timeEnd.TabIndex = 8
        '
        'spinReminderValue
        '
        Me.spinReminderValue.EditValue = New Decimal(New Integer() {30, 0, 0, 0})
        Me.spinReminderValue.Location = New System.Drawing.Point(106, 182)
        Me.spinReminderValue.Name = "spinReminderValue"
        Me.spinReminderValue.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.spinReminderValue.Properties.Appearance.Options.UseFont = True
        Me.spinReminderValue.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.spinReminderValue.Properties.MaxValue = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.spinReminderValue.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.spinReminderValue.Size = New System.Drawing.Size(375, 30)
        Me.spinReminderValue.StyleController = Me.layoutControl1
        Me.spinReminderValue.TabIndex = 9
        '
        'cboReminderUnit
        '
        Me.cboReminderUnit.EditValue = "Minutes"
        Me.cboReminderUnit.Location = New System.Drawing.Point(485, 182)
        Me.cboReminderUnit.Name = "cboReminderUnit"
        Me.cboReminderUnit.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.cboReminderUnit.Properties.Appearance.Options.UseFont = True
        Me.cboReminderUnit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboReminderUnit.Properties.Items.AddRange(New Object() {"Minutes", "Hours", "Days"})
        Me.cboReminderUnit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboReminderUnit.Size = New System.Drawing.Size(468, 30)
        Me.cboReminderUnit.StyleController = Me.layoutControl1
        Me.cboReminderUnit.TabIndex = 10
        '
        'memoNotes
        '
        Me.memoNotes.Location = New System.Drawing.Point(106, 216)
        Me.memoNotes.Name = "memoNotes"
        Me.memoNotes.Size = New System.Drawing.Size(847, 105)
        Me.memoNotes.StyleController = Me.layoutControl1
        Me.memoNotes.TabIndex = 11
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(12, 325)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(269, 38)
        Me.btnSave.StyleController = Me.layoutControl1
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "Save Appointment"
        '
        'btnClear
        '
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(778, 325)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(175, 38)
        Me.btnClear.StyleController = Me.layoutControl1
        Me.btnClear.TabIndex = 13
        Me.btnClear.Text = "Clear"
        '
        'layoutControlGroup1
        '
        Me.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1"
        Me.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.layoutControlGroup1.GroupBordersVisible = False
        Me.layoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.layoutControlItem1, Me.layoutControlItem2, Me.layoutControlItem3, Me.layoutControlItem4, Me.layoutControlItem5, Me.layoutControlItem6, Me.layoutControlItem7, Me.layoutControlItem8, Me.layoutControlItem9, Me.layoutControlItem10, Me.LayoutControlItem11, Me.LayoutControlItem12, Me.LayoutControlItem14, Me.LayoutControlItem15})
        Me.layoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.layoutControlGroup1.Name = "layoutControlGroup1"
        Me.layoutControlGroup1.Size = New System.Drawing.Size(965, 639)
        Me.layoutControlGroup1.Text = "layoutControlGroup1"
        Me.layoutControlGroup1.TextVisible = False
        '
        'layoutControlItem1
        '
        Me.layoutControlItem1.Control = Me.txtCustomerName
        Me.layoutControlItem1.CustomizationFormText = "Customer Name:"
        Me.layoutControlItem1.Location = New System.Drawing.Point(215, 0)
        Me.layoutControlItem1.Name = "layoutControlItem1"
        Me.layoutControlItem1.Size = New System.Drawing.Size(730, 34)
        Me.layoutControlItem1.Text = "Customer Name:"
        Me.layoutControlItem1.TextSize = New System.Drawing.Size(91, 13)
        '
        'layoutControlItem2
        '
        Me.layoutControlItem2.Control = Me.txtEmailAddress
        Me.layoutControlItem2.CustomizationFormText = "Email Address:"
        Me.layoutControlItem2.Location = New System.Drawing.Point(0, 34)
        Me.layoutControlItem2.Name = "layoutControlItem2"
        Me.layoutControlItem2.Size = New System.Drawing.Size(945, 34)
        Me.layoutControlItem2.Text = "Email Address:"
        Me.layoutControlItem2.TextSize = New System.Drawing.Size(91, 13)
        '
        'layoutControlItem3
        '
        Me.layoutControlItem3.Control = Me.dateAppointment
        Me.layoutControlItem3.CustomizationFormText = "Appointment Date:"
        Me.layoutControlItem3.Location = New System.Drawing.Point(0, 68)
        Me.layoutControlItem3.Name = "layoutControlItem3"
        Me.layoutControlItem3.Size = New System.Drawing.Size(945, 34)
        Me.layoutControlItem3.Text = "Appointment Date:"
        Me.layoutControlItem3.TextSize = New System.Drawing.Size(91, 13)
        '
        'layoutControlItem4
        '
        Me.layoutControlItem4.Control = Me.timeStart
        Me.layoutControlItem4.CustomizationFormText = "Start Time:"
        Me.layoutControlItem4.Location = New System.Drawing.Point(0, 102)
        Me.layoutControlItem4.Name = "layoutControlItem4"
        Me.layoutControlItem4.Size = New System.Drawing.Size(945, 34)
        Me.layoutControlItem4.Text = "Start Time:"
        Me.layoutControlItem4.TextSize = New System.Drawing.Size(91, 13)
        '
        'layoutControlItem5
        '
        Me.layoutControlItem5.Control = Me.timeEnd
        Me.layoutControlItem5.CustomizationFormText = "End Time:"
        Me.layoutControlItem5.Location = New System.Drawing.Point(0, 136)
        Me.layoutControlItem5.Name = "layoutControlItem5"
        Me.layoutControlItem5.Size = New System.Drawing.Size(945, 34)
        Me.layoutControlItem5.Text = "End Time:"
        Me.layoutControlItem5.TextSize = New System.Drawing.Size(91, 13)
        '
        'layoutControlItem6
        '
        Me.layoutControlItem6.Control = Me.spinReminderValue
        Me.layoutControlItem6.CustomizationFormText = "Remind Before:"
        Me.layoutControlItem6.Location = New System.Drawing.Point(0, 170)
        Me.layoutControlItem6.Name = "layoutControlItem6"
        Me.layoutControlItem6.Size = New System.Drawing.Size(473, 34)
        Me.layoutControlItem6.Text = "Remind Before:"
        Me.layoutControlItem6.TextSize = New System.Drawing.Size(91, 13)
        '
        'layoutControlItem7
        '
        Me.layoutControlItem7.Control = Me.cboReminderUnit
        Me.layoutControlItem7.CustomizationFormText = "layoutControlItem7"
        Me.layoutControlItem7.Location = New System.Drawing.Point(473, 170)
        Me.layoutControlItem7.Name = "layoutControlItem7"
        Me.layoutControlItem7.Size = New System.Drawing.Size(472, 34)
        Me.layoutControlItem7.Text = "layoutControlItem7"
        Me.layoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutControlItem7.TextToControlDistance = 0
        Me.layoutControlItem7.TextVisible = False
        '
        'layoutControlItem8
        '
        Me.layoutControlItem8.Control = Me.memoNotes
        Me.layoutControlItem8.CustomizationFormText = "Notes:"
        Me.layoutControlItem8.Location = New System.Drawing.Point(0, 204)
        Me.layoutControlItem8.Name = "layoutControlItem8"
        Me.layoutControlItem8.Size = New System.Drawing.Size(945, 109)
        Me.layoutControlItem8.Text = "Notes:"
        Me.layoutControlItem8.TextSize = New System.Drawing.Size(91, 13)
        '
        'layoutControlItem9
        '
        Me.layoutControlItem9.Control = Me.btnSave
        Me.layoutControlItem9.CustomizationFormText = "layoutControlItem9"
        Me.layoutControlItem9.Location = New System.Drawing.Point(0, 313)
        Me.layoutControlItem9.Name = "layoutControlItem9"
        Me.layoutControlItem9.Size = New System.Drawing.Size(273, 42)
        Me.layoutControlItem9.Text = "layoutControlItem9"
        Me.layoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutControlItem9.TextToControlDistance = 0
        Me.layoutControlItem9.TextVisible = False
        '
        'layoutControlItem10
        '
        Me.layoutControlItem10.Control = Me.btnClear
        Me.layoutControlItem10.CustomizationFormText = "layoutControlItem10"
        Me.layoutControlItem10.Location = New System.Drawing.Point(766, 313)
        Me.layoutControlItem10.Name = "layoutControlItem10"
        Me.layoutControlItem10.Size = New System.Drawing.Size(179, 42)
        Me.layoutControlItem10.Text = "layoutControlItem10"
        Me.layoutControlItem10.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutControlItem10.TextToControlDistance = 0
        Me.layoutControlItem10.TextVisible = False
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.gridAppointments
        Me.LayoutControlItem11.CustomizationFormText = "LayoutControlItem11"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 355)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(945, 264)
        Me.LayoutControlItem11.Text = "Appointment"
        Me.LayoutControlItem11.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(91, 13)
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.btnDelete
        Me.LayoutControlItem12.CustomizationFormText = "LayoutControlItem12"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(273, 313)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(196, 42)
        Me.LayoutControlItem12.Text = "LayoutControlItem12"
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem12.TextToControlDistance = 0
        Me.LayoutControlItem12.TextVisible = False
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.Control = Me.btnRefresh
        Me.LayoutControlItem14.CustomizationFormText = "LayoutControlItem14"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(469, 313)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(297, 42)
        Me.LayoutControlItem14.Text = "LayoutControlItem14"
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem14.TextToControlDistance = 0
        Me.LayoutControlItem14.TextVisible = False
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.Control = Me.txtCustomerId
        Me.LayoutControlItem15.CustomizationFormText = "Customer Id :"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(215, 34)
        Me.LayoutControlItem15.Text = "Customer Id :"
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(91, 13)
        '
        'frmAppointmentBooking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(965, 639)
        Me.Controls.Add(Me.layoutControl1)
        Me.Name = "frmAppointmentBooking"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Appointment Booking"
        CType(Me.layoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl1.ResumeLayout(False)
        CType(Me.txtCustomerId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridAppointments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridViewAppointments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmailAddress.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateAppointment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateAppointment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.timeStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.timeEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spinReminderValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboReminderUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.memoNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents layoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents txtCustomerName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtEmailAddress As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dateAppointment As DevExpress.XtraEditors.DateEdit
    Friend WithEvents timeStart As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents timeEnd As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents spinReminderValue As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents cboReminderUnit As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents memoNotes As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnClear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents layoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents layoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents layoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gridAppointments As DevExpress.XtraGrid.GridControl
    Friend WithEvents gridViewAppointments As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCustomerId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
End Class
