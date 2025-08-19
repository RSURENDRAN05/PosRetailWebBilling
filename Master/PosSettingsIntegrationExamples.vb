'============================================
' POS Settings Form Integration Guide
'============================================
'
' To integrate the FrmPosSettings into your main menu,
' add the following code to your main form or menu handler:

' Example 1: Simple menu integration
Private Sub mnuPosSettings_Click(sender As Object, e As EventArgs) Handles mnuPosSettings.Click
    Try
        Dim frmSettings As New FrmPosSettings()
        frmSettings.ShowDialog()
    Catch ex As Exception
        XtraMessageBox.Show("Error opening POS Settings: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
End Sub

' Example 2: Using PosSettingsManager in your application
Private Sub LoadApplicationSettings()
    Try
        ' Load settings at application startup
        PosSettingsManager.LoadSettings()

        ' Use the settings in your application
        Me.Text = PosSettingsManager.CompanyName & " - POS System"

        ' Configure tax calculations
        Dim taxRate As Decimal = PosSettingsManager.TaxRate

        ' Check if discount is enabled
        If PosSettingsManager.EnableDiscount Then
            ' Enable discount functionality
        End If

        ' Configure printing
        If PosSettingsManager.PrintReceipt Then
            ' Setup receipt printing with the configured printer
            Dim printer As String = PosSettingsManager.ReceiptPrinter
        End If

    Catch ex As Exception
        ' Handle error loading settings
        XtraMessageBox.Show("Error loading application settings: " & ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Try
End Sub

' Example 3: Creating custom settings on-the-fly
Private Sub SaveCustomSetting()
    Try
        ' Save a custom setting programmatically
        Dim success As Boolean = PosSettingsManager.SaveSetting("CUSTOM_RECEIPT_FOOTER", "Thank you for your business!", "string")

        If success Then
            ' Setting saved successfully
            Dim footerText As String = PosSettingsManager.GetSetting("CUSTOM_RECEIPT_FOOTER")
        End If
    Catch ex As Exception
        XtraMessageBox.Show("Error saving custom setting: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
End Sub

' Example 4: Form Load Event Integration
Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Try
        ' Initialize POS Settings at application startup
        LoadApplicationSettings()

        ' Other initialization code...

    Catch ex As Exception
        XtraMessageBox.Show("Error during application startup: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
End Sub

' Example 5: Testing the POS Settings functionality
Private Sub btnTestSettings_Click(sender As Object, e As EventArgs) Handles btnTestSettings.Click
    Try
        Dim testForm As New TestPosSettingsForm()
        testForm.ShowDialog()
    Catch ex As Exception
        XtraMessageBox.Show("Error opening test form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
End Sub
