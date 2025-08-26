<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeleteReason
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
        Me.lstBoxControll = New DevExpress.XtraEditors.ListBoxControl()
        Me.btnHide = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        CType(Me.lstBoxControll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstBoxControll
        '
        Me.lstBoxControll.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lstBoxControll.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!, System.Drawing.FontStyle.Bold)
        Me.lstBoxControll.Appearance.Options.UseBackColor = True
        Me.lstBoxControll.Appearance.Options.UseFont = True
        Me.lstBoxControll.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Standard
        Me.lstBoxControll.Location = New System.Drawing.Point(4, 1)
        Me.lstBoxControll.Name = "lstBoxControll"
        Me.lstBoxControll.Size = New System.Drawing.Size(317, 326)
        Me.lstBoxControll.TabIndex = 0
        '
        'btnHide
        '
        Me.btnHide.BackColor = System.Drawing.Color.Red
        Me.btnHide.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHide.ForeColor = System.Drawing.Color.White
        Me.btnHide.Location = New System.Drawing.Point(25, 331)
        Me.btnHide.Margin = New System.Windows.Forms.Padding(1)
        Me.btnHide.Name = "btnHide"
        Me.btnHide.Size = New System.Drawing.Size(136, 59)
        Me.btnHide.TabIndex = 2
        Me.btnHide.Text = "HIDE"
        Me.btnHide.UseVisualStyleBackColor = False
        '
        'btnOk
        '
        Me.btnOk.BackColor = System.Drawing.Color.Navy
        Me.btnOk.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.ForeColor = System.Drawing.Color.White
        Me.btnOk.Location = New System.Drawing.Point(178, 331)
        Me.btnOk.Margin = New System.Windows.Forms.Padding(1)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(136, 59)
        Me.btnOk.TabIndex = 3
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'frmDeleteReason
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(324, 390)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnHide)
        Me.Controls.Add(Me.lstBoxControll)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmDeleteReason"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DeleteReason"
        CType(Me.lstBoxControll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstBoxControll As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents btnHide As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
End Class
