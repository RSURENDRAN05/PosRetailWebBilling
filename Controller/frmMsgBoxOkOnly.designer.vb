<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMsgBoxOkOnly
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
        Me.btnYes = New DevExpress.XtraEditors.SimpleButton()
        Me.lblmsg = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnYes
        '
        Me.btnYes.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnYes.Appearance.BackColor2 = System.Drawing.Color.Black
        Me.btnYes.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnYes.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnYes.Appearance.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.btnYes.Appearance.Options.UseBackColor = True
        Me.btnYes.Appearance.Options.UseBorderColor = True
        Me.btnYes.Appearance.Options.UseFont = True
        Me.btnYes.Appearance.Options.UseForeColor = True
        Me.btnYes.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.btnYes.Location = New System.Drawing.Point(375, 105)
        Me.btnYes.Name = "btnYes"
        Me.btnYes.Size = New System.Drawing.Size(124, 46)
        Me.btnYes.TabIndex = 0
        Me.btnYes.Text = "Ok"
        '
        'lblmsg
        '
        Me.lblmsg.AutoSize = True
        Me.lblmsg.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.lblmsg.ForeColor = System.Drawing.Color.White
        Me.lblmsg.Location = New System.Drawing.Point(38, 43)
        Me.lblmsg.Name = "lblmsg"
        Me.lblmsg.Size = New System.Drawing.Size(34, 17)
        Me.lblmsg.TabIndex = 2
        Me.lblmsg.Text = "msg"
        '
        'frmMsgBoxOkOnly
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(511, 163)
        Me.Controls.Add(Me.lblmsg)
        Me.Controls.Add(Me.btnYes)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmMsgBoxOkOnly"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmMsgBox"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnYes As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblmsg As System.Windows.Forms.Label
End Class
