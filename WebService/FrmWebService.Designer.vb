<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUploadSalesAutoSync
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
        Me.TimerAutoSyncSales = New System.Windows.Forms.Timer(Me.components)
        Me.RichTextBoxErrorLoadReader = New System.Windows.Forms.RichTextBox()
        Me.btnClear = New DevExpress.XtraEditors.SimpleButton()
        Me.btnstart = New DevExpress.XtraEditors.SimpleButton()
        Me.btnstop = New DevExpress.XtraEditors.SimpleButton()
        Me.SuspendLayout()
        '
        'TimerAutoSyncSales
        '
        Me.TimerAutoSyncSales.Enabled = True
        Me.TimerAutoSyncSales.Interval = 1000
        '
        'RichTextBoxErrorLoadReader
        '
        Me.RichTextBoxErrorLoadReader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxErrorLoadReader.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBoxErrorLoadReader.Name = "RichTextBoxErrorLoadReader"
        Me.RichTextBoxErrorLoadReader.Size = New System.Drawing.Size(455, 235)
        Me.RichTextBoxErrorLoadReader.TabIndex = 0
        Me.RichTextBoxErrorLoadReader.Text = ""
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(381, 212)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 1
        Me.btnClear.Text = "Clear Log"
        '
        'btnstart
        '
        Me.btnstart.Location = New System.Drawing.Point(219, 212)
        Me.btnstart.Name = "btnstart"
        Me.btnstart.Size = New System.Drawing.Size(75, 23)
        Me.btnstart.TabIndex = 2
        Me.btnstart.Text = "Start"
        '
        'btnstop
        '
        Me.btnstop.Location = New System.Drawing.Point(300, 212)
        Me.btnstop.Name = "btnstop"
        Me.btnstop.Size = New System.Drawing.Size(75, 23)
        Me.btnstop.TabIndex = 3
        Me.btnstop.Text = "Stop"
        '
        'FrmUploadSalesAutoSync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(455, 235)
        Me.Controls.Add(Me.btnstop)
        Me.Controls.Add(Me.btnstart)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.RichTextBoxErrorLoadReader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FrmUploadSalesAutoSync"
        Me.Text = "FrmWebService"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TimerAutoSyncSales As System.Windows.Forms.Timer
    Friend WithEvents RichTextBoxErrorLoadReader As System.Windows.Forms.RichTextBox
    Friend WithEvents btnClear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnstart As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnstop As DevExpress.XtraEditors.SimpleButton
End Class
