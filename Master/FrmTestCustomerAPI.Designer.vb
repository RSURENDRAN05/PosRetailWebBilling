<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmTestCustomerAPI
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
        Me.btnTestAPI = New DevExpress.XtraEditors.SimpleButton()
        Me.txtResults = New DevExpress.XtraEditors.MemoEdit()
        Me.btnLoadToGrid = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControlTest = New DevExpress.XtraGrid.GridControl()
        Me.GridViewTest = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.txtResults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControlTest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewTest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnTestAPI
        '
        Me.btnTestAPI.Location = New System.Drawing.Point(12, 12)
        Me.btnTestAPI.Name = "btnTestAPI"
        Me.btnTestAPI.Size = New System.Drawing.Size(100, 30)
        Me.btnTestAPI.TabIndex = 0
        Me.btnTestAPI.Text = "Test API"
        '
        'txtResults
        '
        Me.txtResults.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtResults.Location = New System.Drawing.Point(12, 48)
        Me.txtResults.Name = "txtResults"
        Me.txtResults.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtResults.Size = New System.Drawing.Size(760, 200)
        Me.txtResults.TabIndex = 1
        '
        'btnLoadToGrid
        '
        Me.btnLoadToGrid.Location = New System.Drawing.Point(118, 12)
        Me.btnLoadToGrid.Name = "btnLoadToGrid"
        Me.btnLoadToGrid.Size = New System.Drawing.Size(100, 30)
        Me.btnLoadToGrid.TabIndex = 2
        Me.btnLoadToGrid.Text = "Load to Grid"
        '
        'GridControlTest
        '
        Me.GridControlTest.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridControlTest.Location = New System.Drawing.Point(12, 254)
        Me.GridControlTest.MainView = Me.GridViewTest
        Me.GridControlTest.Name = "GridControlTest"
        Me.GridControlTest.Size = New System.Drawing.Size(760, 300)
        Me.GridControlTest.TabIndex = 3
        Me.GridControlTest.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewTest})
        '
        'GridViewTest
        '
        Me.GridViewTest.GridControl = Me.GridControlTest
        Me.GridViewTest.Name = "GridViewTest"
        '
        'FrmTestCustomerAPI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 566)
        Me.Controls.Add(Me.GridControlTest)
        Me.Controls.Add(Me.btnLoadToGrid)
        Me.Controls.Add(Me.txtResults)
        Me.Controls.Add(Me.btnTestAPI)
        Me.Name = "FrmTestCustomerAPI"
        Me.Text = "Customer API Test"
        CType(Me.txtResults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControlTest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnTestAPI As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtResults As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btnLoadToGrid As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControlTest As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewTest As DevExpress.XtraGrid.Views.Grid.GridView
End Class
