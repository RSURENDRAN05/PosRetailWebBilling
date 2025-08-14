Public Class frmAbout 

    Private Sub frmAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBoxControl1.Items.Add(M_Details.SoftwareVersion)
        If M_Details.licenceActive = "T" Then
            ListBoxControl1.Items.Add("Trail Version")
        Else
            ListBoxControl1.Items.Add("Licence Activated")
        End If
        ListBoxControl1.Items.Add("Support Contact")
        ListBoxControl1.Items.Add("Contact What's App : +91 96290 86303")

    End Sub
End Class