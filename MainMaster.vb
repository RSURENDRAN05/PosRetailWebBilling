Public Class MainMaster 
    Dim bankId As String = ""
    Dim profileId As String = ""

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        If M_Details.licenceActive = "T" Then
            barstatustrial.Caption = "Trial Version"
        Else
            barstatustrial.Caption = "Licence Activated"
        End If
        bankId = ini.ReadValue("Bank", "BankId")
        profileId = ini.ReadValue("Profile", "ProfileId")

        barStatus.Caption = M_Details.SoftwareVersion
        baruserStatus.Caption = _companyInfo.UserId & "-" & _companyInfo.UserName
        barstatuscompany.Caption = _companyInfo.ComId & "-" & _companyInfo.CompanyName & "-" & _companyInfo.LocationName
        barbtnstatustime.Caption = Date.Now
        PanelScreenHeight = Screen.PrimaryScreen.Bounds.Height
        PanelScreenWith = Screen.PrimaryScreen.Bounds.Width
        ' Add any initialization after the InitializeComponent() call.
        MenuReading()
        If _JsonData.CompanyTable.Rows.Count > 0 Then
            cmbCompany.Properties.DataSource = _JsonData.CompanyTable
        Else
            cmbCompany.Properties.DataSource = Nothing
        End If
        If _JsonData.LocationTable.Rows.Count > 0 Then
            cmbLocation.Properties.DataSource = _JsonData.LocationTable
        Else
            cmbLocation.Properties.DataSource = Nothing
        End If
        cmbCompany.EditValue = _companyInfo.ComId
        cmbLocation.EditValue = _companyInfo.LocId
        'RibbonPage2.Visible = False 'ClientInfo
    End Sub
    Private Sub MenuReading()
        Try
            If getMenuSetting() = True Then
                If _JsonData.MenuSettings.Rows.Count > 0 Then
                    Dim Company As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "Company"
                    If Company.Any Then
                        If Company(0)("pmr_active") = "1" Then
                            barcompany.Enabled = True
                        Else
                            barcompany.Enabled = False
                        End If
                    End If
                    Dim Location As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "Location"
                    If Location.Any Then
                        If Location(0)("pmr_active") = "1" Then
                            barlocation.Enabled = True
                        Else
                            barlocation.Enabled = False
                        End If
                    End If
                    Dim NewClient As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "NewClient"
                    If NewClient.Any Then
                        If NewClient(0)("pmr_active") = "1" Then
                            barnewclient.Enabled = True
                        Else
                            barnewclient.Enabled = False
                        End If
                    End If
                    Dim NewSupplier As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "NewSupplier"
                    If NewSupplier.Any Then
                        If NewSupplier(0)("pmr_active") = "1" Then
                            barnewsupplier.Enabled = True
                        Else
                            barnewsupplier.Enabled = False
                        End If
                    End If
                    Dim NewPurchase As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "NewPurchase"
                    If NewPurchase.Any Then
                        If NewPurchase(0)("pmr_active") = "1" Then
                            barnewpurchase.Enabled = True
                        Else
                            barnewpurchase.Enabled = False
                        End If
                    End If
                    Dim TaxMaster As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "TaxMaster"
                    If TaxMaster.Any Then
                        If TaxMaster(0)("pmr_active") = "1" Then
                            bartaxmaster.Enabled = True
                        Else
                            bartaxmaster.Enabled = False
                        End If
                    End If
                    Dim Unitmaster As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "Unitmaster"
                    If Unitmaster.Any Then
                        If Unitmaster(0)("pmr_active") = "1" Then
                            barunitmaster.Enabled = True
                        Else
                            barunitmaster.Enabled = False
                        End If
                    End If
                    Dim MainGroup As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "MainGroup"
                    If MainGroup.Any Then
                        If MainGroup(0)("pmr_active") = "1" Then
                            barmaingroup.Enabled = True
                        Else
                            barmaingroup.Enabled = False
                        End If
                    End If
                    Dim SubGroup As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "SubGroup"
                    If SubGroup.Any Then
                        If SubGroup(0)("pmr_active") = "1" Then
                            barsubgroup.Enabled = True
                        Else
                            barsubgroup.Enabled = False
                        End If
                    End If
                    Dim ProductMaster As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "ProductMaster"
                    If ProductMaster.Any Then
                        If ProductMaster(0)("pmr_active") = "1" Then
                            barmaterial.Enabled = True
                        Else
                            barmaterial.Enabled = False
                        End If
                    End If
                    Dim PosSales1 As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "PosSales"
                    If PosSales1.Any Then
                        If PosSales1(0)("pmr_active") = "1" Then
                            barpossales.Enabled = True
                        Else
                            barpossales.Enabled = False
                        End If
                    End If
                    Dim CreateGroup As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "CreateGroup"
                    If CreateGroup.Any Then
                        If CreateGroup(0)("pmr_active") = "1" Then
                            barcreategroup.Enabled = True
                        Else
                            barcreategroup.Enabled = False
                        End If
                    End If
                    Dim CreateParent As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "CreateParent"
                    If CreateParent.Any Then
                        If CreateParent(0)("pmr_active") = "1" Then
                            barparentgroup.Enabled = True
                        Else
                            barparentgroup.Enabled = False
                        End If
                    End If
                    Dim NewLedger As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "NewLedger"
                    If NewLedger.Any Then
                        If NewLedger(0)("pmr_active") = "1" Then
                            barnewledger.Enabled = True
                        Else
                            barnewledger.Enabled = False
                        End If
                    End If
                    Dim LedgerEntry As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "LedgerEntry"
                    If LedgerEntry.Any Then
                        If LedgerEntry(0)("pmr_active") = "1" Then
                            barledgerentry.Enabled = True
                        Else
                            barledgerentry.Enabled = False
                        End If
                    End If
                    Dim ChequeEntry As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "ChequeEntry"
                    If ChequeEntry.Any Then
                        If ChequeEntry(0)("pmr_active") = "1" Then
                            barchequeentry.Enabled = True
                        Else
                            barchequeentry.Enabled = False
                        End If
                    End If
                    'Dim SystemSetting As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "SystemSetting"
                    'If SystemSetting.Any Then
                    '    If SystemSetting(0)("pmr_active") = "1" Then
                    '        frm.Enabled = True
                    '    Else
                    '        frmChequeprint.Enabled = False
                    '    End If
                    'End If
                    'Dim SystemSetting As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "SystemSetting"
                    'If SystemSetting.Any Then
                    '    If SystemSetting(0)("pmr_active") = "1" Then
                    '        frm.Enabled = True
                    '    Else
                    '        frmChequeprint.Enabled = False
                    '    End If
                    'End If
                    Dim SalesProfile As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "SalesProfile"
                    If SalesProfile.Any Then
                        If SalesProfile(0)("pmr_active") = "1" Then
                            barbtnPrintProfile.Enabled = True
                        Else
                            barbtnPrintProfile.Enabled = False
                        End If
                    End If
                    Dim PrintDesign As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.MenuSettings Where dtrow("pmr_name") = "PrintDesign"
                    If PrintDesign.Any Then
                        If PrintDesign(0)("pmr_active") = "1" Then
                            barbtnprintdesign.Enabled = True
                        Else
                            barbtnprintdesign.Enabled = False
                        End If
                    End If
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Structure _Menu
        Public Shared Company As Boolean
    End Structure
    Private Sub barcompany_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barcompany.ItemClick
        Try
            'FrmCompany.ShowDialog()
            FrmCompany.MdiParent = Me
            FrmCompany.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barlocation_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barlocation.ItemClick
        Try
            'FrmLocation.ShowDialog()
            FrmLocation.MdiParent = Me
            FrmLocation.Show()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub bartaxmaster_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bartaxmaster.ItemClick
        Try
            'FrmTaxMaster.ShowDialog()
            FrmTaxMaster.MdiParent = Me
            FrmTaxMaster.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barunitmaster_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barunitmaster.ItemClick
        Try
            'FrmUnitMaster.ShowDialog()
            FrmUnitMaster.MdiParent = Me
            FrmUnitMaster.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barmaingroup_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barmaingroup.ItemClick
        Try
            'FrmMainGroup.ShowDialog()
            FrmMainGroup.MdiParent = Me
            FrmMainGroup.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barsubgroup_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barsubgroup.ItemClick
        Try
            'FrmCateMaster.ShowDialog()
            FrmCateMaster.MdiParent = Me
            FrmCateMaster.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barmaterial_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barmaterial.ItemClick
        Try
            'FrmItemMaster.ShowDialog()
            FrmItemMaster.MdiParent = Me
            FrmItemMaster.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barnewclient_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barnewclient.ItemClick
        Try
            'FrmNewClient.ShowDialog()
            FrmNewClient.MdiParent = Me
            FrmNewClient.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barnewsupplier_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barnewsupplier.ItemClick
        Try
            'FrmSupplier.ShowDialog()
            FrmSupplier.MdiParent = Me
            FrmSupplier.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barnewpurchase_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barnewpurchase.ItemClick
        Try
            FrmPurchase.MdiParent = Me
            FrmPurchase.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barpurchasereport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barpurchasereport.ItemClick
        Try
            FrmPurchaseView.MdiParent = Me
            FrmPurchaseView.Show()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub barpossales_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barpossales.ItemClick
        Try
            PosSales.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barcreategroup_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barcreategroup.ItemClick
        Try
            frmGroup.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barparentgroup_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barparentgroup.ItemClick
        Try
            frmParent.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barnewledger_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barnewledger.ItemClick
        Try
            frmLedgerCreate.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barledgerentry_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barledgerentry.ItemClick
        Try
            'frmLedgerEntry.ShowDialog("PAYMENT")
            frmLedgerEntry.MdiParent = Me
            frmLedgerEntry.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barchequeentry_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barchequeentry.ItemClick
        Try
            frmChequeprint.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barnewuser_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barnewuser.ItemClick
        Try
            FrmNewUser.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MainMaster_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            End
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnPrintProfile_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnPrintProfile.ItemClick
        Try
            frmPrintProfile.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnprintdesign_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnprintdesign.ItemClick
        Try
            frmreportdesign.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnemployeeinfo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnemployeeinfo.ItemClick
        Try
            'frmEmployee.Show()
            frmEmployee.MdiParent = Me
            frmEmployee.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnpayslipprint_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnpayslipprint.ItemClick
        Try
            FRMPAYSLIP.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnledgerReport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnledgerReport.ItemClick
        Try


            frmLedgerReport.MdiParent = Me
            frmLedgerReport.Text = "LedgerReport"
            frmLedgerReport.Show()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub barnewcustomer_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barnewcustomer.ItemClick
        Try
            FrmNewCustomer.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtncreatemonth_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtncreatemonth.ItemClick
        Try
            frmcreatemonth.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    
    Private Sub cmbCompany_EditValueChanged(sender As Object, e As EventArgs) Handles cmbCompany.EditValueChanged
        Try
            cmbCompany_ListChanged(Nothing, Nothing)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbLocation_EditValueChanged(sender As Object, e As EventArgs) Handles cmbLocation.EditValueChanged
        Try
            cmbLocation_ListChanged(Nothing, Nothing)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbCompany_ListChanged(sender As Object, e As System.ComponentModel.ListChangedEventArgs) Handles cmbCompany.ListChanged
        Try

            Dim j As System.Data.DataRowView = cmbCompany.GetSelectedDataRow
            Dim r As DataRow = j.Row
            _companyInfo.ComId = r("COID")
            _companyInfo.CompanyName = r("CompanyName")
            barstatuscompany.Caption = _companyInfo.ComId & "-" & _companyInfo.CompanyName & "-" & _companyInfo.LocId & "-" & _companyInfo.LocationName
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbLocation_ListChanged(sender As Object, e As System.ComponentModel.ListChangedEventArgs) Handles cmbLocation.ListChanged
        Try

            Dim j As System.Data.DataRowView = cmbLocation.GetSelectedDataRow
            Dim r As DataRow = j.Row
            _companyInfo.LocId = r("plm_id")
            _companyInfo.LocationName = r("plm_name")
            barstatuscompany.Caption = _companyInfo.ComId & "-" & _companyInfo.CompanyName & "-" & _companyInfo.LocId & "-" & _companyInfo.LocationName
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtngeneratemonth_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtngeneratemonth.ItemClick
        Try
            frmGenerateMonth.MdiParent = Me
            frmGenerateMonth.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnGenerateSalary_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnGenerateSalary.ItemClick
        Try
            frmFinalProcess.MdiParent = Me
            frmFinalProcess.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barmenuheader_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barmenuheader.ItemClick
        Try
            FrmMenuManager.MdiParent = Me
            FrmMenuManager.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub baruserpolicy_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles baruserpolicy.ItemClick
        Try
            FrmGroupPolicyManagerSimple.MdiParent = Me
            FrmGroupPolicyManagerSimple.Show()
        Catch ex As Exception

        End Try
    End Sub
End Class