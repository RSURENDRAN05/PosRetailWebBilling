Public Class MainForm
    Dim bankId As String = ""
    Dim profileId As String = ""
    Dim companyId As String = ""
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        If CheckForInternetConnection() = True Then
            If M_Details.licenceActive = "T" Then
                barstatustrial.Caption = "Trial Version"
            Else
                barstatustrial.Caption = "Licence Activated"
            End If
            bankId = ini.ReadValue("Bank", "BankId")
            profileId = ini.ReadValue("Profile", "ProfileId")
            companyId = ini.ReadValue("Bank", "CompanyId")
            barStatus.Caption = M_Details.SoftwareVersion
            baruserStatus.Caption = _companyInfo.UserId & "-" & _companyInfo.UserName
            barstatuscompany.Caption = _companyInfo.ComId & "-" & _companyInfo.CompanyName & "-" & _companyInfo.LocationName
            barbtnstatustime.Caption = Date.Now
            PanelScreenHeight = Screen.PrimaryScreen.Bounds.Height
            PanelScreenWith = Screen.PrimaryScreen.Bounds.Width
        End If
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub btnminimize_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnminimize.ItemClick
        Try
            Me.WindowState = FormWindowState.Minimized
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnExit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnExit.ItemClick
        Try
            Application.Exit()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'MenuLayOut Load
        Catch ex As Exception

        End Try
    End Sub

    Private Sub navcomp_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navcomp.LinkClicked
       
    End Sub

    Private Sub navlocation_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navlocation.LinkClicked
        Try
            FrmLocation.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

 
    Private Sub navnewuser_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navnewuser.LinkClicked
        Try
            FrmNewUser.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub navtaxmaster_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navtaxmaster.LinkClicked
        Try
            FrmTaxMaster.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub navmaingroup_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navmaingroup.LinkClicked
        Try
            FrmMainGroup.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub navsubgroup_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navsubgroup.LinkClicked
        Try
            FrmCateMaster.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub navproductmaster_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navproductmaster.LinkClicked
        Try
            FrmItemMaster.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub navnewclient_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navnewclient.LinkClicked
        Try
            FrmNewClient.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub navunitmaster_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navunitmaster.LinkClicked
        Try
            FrmUnitMaster.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub navsupplier_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navsupplier.LinkClicked
        Try
            FrmSupplier.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub navpurchaseentry_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navpurchaseentry.LinkClicked
        Try
            FrmPurchase.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    
    Private Sub navsales_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navsales.LinkClicked
        Try
            PosSales.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub navgroupmaster_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navgroupmaster.LinkClicked
        Try
            frmGroup.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub navparentmaster_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navparentmaster.LinkClicked
        Try
            frmParent.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub navledgercreate_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navledgercreate.LinkClicked
        Try
            frmLedgerCreate.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub navledgerEntry_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navledgerEntry.LinkClicked
        Try
            frmLedgerEntry.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub navledgerreport1_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navledgerreport1.LinkClicked
        Try
            frmLedgerReport.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub navchequeprint_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles navchequeprint.LinkClicked
        Try
            frmChequeprint.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
End Class
