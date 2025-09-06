Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports DevExpress.LookAndFeel
Imports DevExpress.XtraBars.Helpers

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
        baruserStatus.Caption = _companyInfo.UserId & "-" & _companyInfo.UserName & " PMID  : " & _companyInfo.CompanyPMId
        barstatuscompany.Caption = _companyInfo.ComId & "-" & _companyInfo.CompanyName & "-" & _companyInfo.LocationName
        barcurshiftno.Caption = "CurShiftNo : " & _saleSetting._curShiftno
        bardayno.Caption = "CurDayNo : " & _saleSetting._curDayno
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

        ' Apply skin to MainMaster form (skins already initialized at app startup)
        SkinManager.LoadSkinSetting()

        ' Initialize the skin gallery
        InitializeSkinGallery()


    End Sub
    Private Sub MenuReading()
        Try
            If getUserPolicyInfo(_companyInfo.UserId) = True Then
                'MainHear
                If _companyInfo.UserRoleId.ToString = "1" Then
                    ' Admin users - keep all tabs visible including Sales tab for Menu Design
                    RibbonPageSales.Visible = True
                    Exit Sub
                End If
                RibbonPageMaster.Visible = False
                RibbonPagePurchase.Visible = False
                RibbonPageSales.Visible = False
                RibbonPageAccounts.Visible = False
                RibbonPageUser.Visible = False
                RibbonPageEmployee.Visible = False
                RibbonPageSettings.Visible = False
                RibbonReports.Visible = False

                'SubMen
                barcompany.Enabled = False
                barlocation.Enabled = False
                barnewsupplier.Enabled = False
                barnewpurchase.Enabled = False
                barmasterpurchasereport.Enabled = False
                bartaxmaster.Enabled = False
                barunitmaster.Enabled = False
                barmaingroup.Enabled = False
                barnewcustomer.Enabled = False
                barsalessummaryreport.Enabled = False
                barmaterial.Enabled = False
                barsubgroup.Enabled = False
                barpossales.Enabled = False
                barledgerReport.Enabled = False
                barchequeentry.Enabled = False
                barledgerentry.Enabled = False
                barnewledger.Enabled = False
                barparentgroup.Enabled = False
                barcreategroup.Enabled = False
                baruserpolicy.Enabled = False
                barnewuser.Enabled = False
                barbtnpayslipprint.Enabled = False
                barbtnGenerateSalary.Enabled = False
                barbtngeneratemonth.Enabled = False
                barbtncreatemonth.Enabled = False
                barbtnemployeeinfo.Enabled = False
                barmenuheader.Enabled = False
                barbtnPrintProfile.Enabled = False
                barbtnprintdesign.Enabled = False
                barsystemsettings.Enabled = False
                barnewdiscountmaster.Enabled = False
                barSalesCommission.Enabled = False
                barsalesdetailsreport.Enabled = False
                barbtnchronicalreport.Enabled = False
                barbtnadvancepaymentreport.Enabled = False
                barbtnmonthlysummaryreport.Enabled = False
                barbtnmaingrouppolicy.Enabled = False
                If _JsonData.UserPolicyTable.Rows.Count > 0 Then
                    'Master
                    Dim MenuMaster As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "MenuMaster"
                    If MenuMaster.Any Then
                        If MenuMaster(0)("menu_active") = "1" Then
                            RibbonPageMaster.Visible = True
                        Else
                            RibbonPageMaster.Visible = False
                        End If
                    End If
                    Dim Company As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "Company"
                    If Company.Any Then
                        If Company(0)("menu_active") = "1" Then
                            barcompany.Enabled = True
                        Else
                            barcompany.Enabled = False
                        End If
                    End If
                    Dim Location As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "Location"
                    If Location.Any Then
                        If Location(0)("menu_active") = "1" Then
                            barlocation.Enabled = True
                        Else
                            barlocation.Enabled = False
                        End If
                    End If
                    'Purchase
                    Dim MenuPurchase As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "MenuPurchase"
                    If MenuPurchase.Any Then
                        If MenuPurchase(0)("menu_active") = "1" Then
                            RibbonPagePurchase.Visible = True
                        Else
                            RibbonPagePurchase.Visible = False
                        End If
                    End If
                    Dim NewSupplier As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "NewSupplier"
                    If NewSupplier.Any Then
                        If NewSupplier(0)("menu_active") = "1" Then
                            barnewsupplier.Enabled = True
                        Else
                            barnewsupplier.Enabled = False
                        End If
                    End If
                    Dim NewPurchase As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "NewPurchase"
                    If NewPurchase.Any Then
                        If NewPurchase(0)("menu_active") = "1" Then
                            barnewpurchase.Enabled = True
                        Else
                            barnewpurchase.Enabled = False
                        End If
                    End If
                    Dim MasterPurchaseReport As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "MasterPurchaseReport"
                    If MasterPurchaseReport.Any Then
                        If MasterPurchaseReport(0)("menu_active") = "1" Then
                            barmasterpurchasereport.Enabled = True
                        Else
                            barmasterpurchasereport.Enabled = False
                        End If
                    End If
                    'Sales
                    Dim MenuSales As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "MenuSales"
                    If MenuSales.Any Then
                        If MenuSales(0)("menu_active") = "1" Then
                            RibbonPageSales.Visible = True
                        Else
                            RibbonPageSales.Visible = False
                        End If
                    Else
                        ' Always show Sales tab if no permission record exists (for Menu Design access)
                        RibbonPageSales.Visible = True
                    End If
                    Dim TaxMaster As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "TaxMaster"
                    If TaxMaster.Any Then
                        If TaxMaster(0)("menu_active") = "1" Then
                            bartaxmaster.Enabled = True
                        Else
                            bartaxmaster.Enabled = False
                        End If
                    End If
                    Dim Unitmaster As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "Unitmaster"
                    If Unitmaster.Any Then
                        If Unitmaster(0)("menu_active") = "1" Then
                            barunitmaster.Enabled = True
                        Else
                            barunitmaster.Enabled = False
                        End If
                    End If
                    Dim MainGroup As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "MainGroup"
                    If MainGroup.Any Then
                        If MainGroup(0)("menu_active") = "1" Then
                            barmaingroup.Enabled = True
                        Else
                            barmaingroup.Enabled = False
                        End If
                    End If
                    Dim MainGroupPolicy As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "MainGroupPolicy"
                    If MainGroupPolicy.Any Then
                        If MainGroupPolicy(0)("menu_active") = "1" Then
                            barbtnmaingrouppolicy.Enabled = True
                        Else
                            barbtnmaingrouppolicy.Enabled = False
                        End If
                    End If

                    Dim SubGroup As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "SubGroup"
                    If SubGroup.Any Then
                        If SubGroup(0)("menu_active") = "1" Then
                            barsubgroup.Enabled = True
                        Else
                            barsubgroup.Enabled = False
                        End If
                    End If
                    Dim ProductMaster As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "ProductMaster"
                    If ProductMaster.Any Then
                        If ProductMaster(0)("menu_active") = "1" Then
                            barmaterial.Enabled = True
                        Else
                            barmaterial.Enabled = False

                        End If
                    End If
                    Dim PosSales1 As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "PosSales"
                    If PosSales1.Any Then
                        If PosSales1(0)("menu_active") = "1" Then
                            barpossales.Enabled = True
                        Else
                            barpossales.Enabled = False
                        End If
                    End If

                    Dim NewCustomer As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "NewCustomer"
                    If NewCustomer.Any Then
                        If PosSales1(0)("menu_active") = "1" Then
                            barnewcustomer.Enabled = True
                        Else
                            barnewcustomer.Enabled = False

                        End If
                    End If
                    Dim DiscountMaster As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "DiscountMaster"
                    If DiscountMaster.Any Then
                        If DiscountMaster(0)("menu_active") = "1" Then
                            barnewdiscountmaster.Enabled = True
                        Else
                            barnewdiscountmaster.Enabled = False

                        End If
                    End If
                    Dim SalesCommission As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "SalesCommission"
                    If SalesCommission.Any Then
                        If SalesCommission(0)("menu_active") = "1" Then
                            barSalesCommission.Enabled = True
                        Else
                            barSalesCommission.Enabled = False

                        End If
                    End If

                    'Accounts
                    Dim MenuAccounts As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "MenuAccounts"
                    If MenuAccounts.Any Then
                        If MenuAccounts(0)("menu_active") = "1" Then
                            RibbonPageAccounts.Visible = True
                        Else
                            RibbonPageAccounts.Visible = False
                        End If
                    End If
                    Dim CreateGroup As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "CreateGroup"
                    If CreateGroup.Any Then
                        If CreateGroup(0)("menu_active") = "1" Then
                            barcreategroup.Enabled = True
                        Else
                            barcreategroup.Enabled = False
                        End If
                    End If
                    Dim CreateParent As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "CreateParent"
                    If CreateParent.Any Then
                        If CreateParent(0)("menu_active") = "1" Then
                            barparentgroup.Enabled = True
                        Else
                            barparentgroup.Enabled = False

                        End If
                    End If
                    Dim NewLedger As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "NewLedger"
                    If NewLedger.Any Then
                        If NewLedger(0)("menu_active") = "1" Then
                            barnewledger.Enabled = True
                        Else
                            barnewledger.Enabled = False

                        End If
                    End If
                    Dim LedgerEntry As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "LedgerEntry"
                    If LedgerEntry.Any Then
                        If LedgerEntry(0)("menu_active") = "1" Then
                            barledgerentry.Enabled = True
                        Else
                            barledgerentry.Enabled = False

                        End If
                    End If
                    Dim ChequeEntry As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "ChequeEntry"
                    If ChequeEntry.Any Then
                        If ChequeEntry(0)("menu_active") = "1" Then
                            barchequeentry.Enabled = True
                        Else
                            barchequeentry.Enabled = False

                        End If
                    End If
                    Dim LedgerReport As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "LedgerReport"
                    If LedgerReport.Any Then
                        If LedgerReport(0)("menu_active") = "1" Then
                            barledgerReport.Enabled = True
                        Else
                            barledgerReport.Enabled = False

                        End If
                    End If
                    'UserMaster
                    Dim MenuUser As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "MenuUser"
                    If MenuUser.Any Then
                        If MenuUser(0)("menu_active") = "1" Then
                            RibbonPageUser.Visible = True
                        Else
                            RibbonPageUser.Visible = False
                        End If
                    End If
                    Dim NewUser As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "NewUser"
                    If NewUser.Any Then
                        If NewUser(0)("menu_active") = "1" Then
                            barnewuser.Enabled = True
                        Else
                            barnewuser.Enabled = False
                        End If
                    End If
                    Dim GroupPolicy As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "GroupPolicy"
                    If GroupPolicy.Any Then
                        If GroupPolicy(0)("menu_active") = "1" Then
                            baruserpolicy.Enabled = True
                        Else
                            baruserpolicy.Enabled = False

                        End If
                    End If
                    'emplyoee
                    Dim MenuEmployee As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "MenuEmployee"
                    If MenuEmployee.Any Then
                        If MenuEmployee(0)("menu_active") = "1" Then
                            RibbonPageEmployee.Visible = True
                        Else
                            RibbonPageEmployee.Visible = False
                        End If
                    End If
                    Dim EmployeeInfo As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "EmployeeInfo"
                    If EmployeeInfo.Any Then
                        If EmployeeInfo(0)("menu_active") = "1" Then
                            barbtnemployeeinfo.Enabled = True
                        Else
                            barbtnemployeeinfo.Enabled = False
                        End If
                    End If
                    Dim CreateMonth As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "CreateMonth"
                    If CreateMonth.Any Then
                        If CreateMonth(0)("menu_active") = "1" Then
                            barbtncreatemonth.Enabled = True
                        Else
                            barbtncreatemonth.Enabled = False

                        End If
                    End If
                    Dim GenerateMonth As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "GenerateMonth"
                    If GenerateMonth.Any Then
                        If GenerateMonth(0)("menu_active") = "1" Then
                            barbtngeneratemonth.Enabled = True
                        Else
                            barbtngeneratemonth.Enabled = False

                        End If
                    End If
                    Dim GenerateSalary As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "GenerateSalary"
                    If GenerateSalary.Any Then
                        If GenerateSalary(0)("menu_active") = "1" Then
                            barbtnGenerateSalary.Enabled = True
                        Else
                            barbtnGenerateSalary.Enabled = False

                        End If
                    End If
                    Dim PaySlipPrint As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "PaySlipPrint"
                    If PaySlipPrint.Any Then
                        If PaySlipPrint(0)("menu_active") = "1" Then
                            barbtnpayslipprint.Enabled = True
                        Else
                            barbtnpayslipprint.Enabled = False

                        End If
                    End If
                    'Settings
                    Dim MenuSettings As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "MenuSettings"
                    If MenuSettings.Any Then
                        If MenuSettings(0)("menu_active") = "1" Then
                            RibbonPageSettings.Visible = True
                        Else
                            RibbonPageSettings.Visible = False
                        End If
                    End If
                    Dim SystemSettings As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "SystemSettings"
                    If SystemSettings.Any Then
                        If SystemSettings(0)("menu_active") = "1" Then
                            barsystemsettings.Enabled = True
                        Else
                            barsystemsettings.Enabled = False
                        End If
                    End If
                    Dim PrintDesign As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "PrintDesign"
                    If PrintDesign.Any Then
                        If PrintDesign(0)("menu_active") = "1" Then
                            barbtnprintdesign.Enabled = True
                        Else
                            barbtnprintdesign.Enabled = False

                        End If
                    End If
                    Dim SalesProfile As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "SalesProfile"
                    If SalesProfile.Any Then
                        If SalesProfile(0)("menu_active") = "1" Then
                            barbtnPrintProfile.Enabled = True
                        Else
                            barbtnPrintProfile.Enabled = False

                        End If
                    End If
                    Dim MainMenuList As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "MainMenuList"
                    If MainMenuList.Any Then
                        If MainMenuList(0)("menu_active") = "1" Then
                            barmenuheader.Enabled = True
                        Else
                            barmenuheader.Enabled = False

                        End If
                    End If

                    'SalesReport
                    Dim MenuSalesReport As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "MenuSalesReport"
                    If MenuSalesReport.Any Then
                        If MenuSalesReport(0)("menu_active") = "1" Then
                            RibbonReports.Visible = True
                        Else
                            RibbonReports.Visible = False
                        End If
                    End If
                    Dim MasterSalesReport As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "MasterSalesReport"
                    If MasterSalesReport.Any Then
                        If MasterSalesReport(0)("menu_active") = "1" Then
                            barsalessummaryreport.Enabled = True
                        Else
                            barsalessummaryreport.Enabled = False
                        End If
                    End If
                    Dim SalesDetailReport As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "SalesDetailReport"
                    If SalesDetailReport.Any Then
                        If SalesDetailReport(0)("menu_active") = "1" Then
                            barsalesdetailsreport.Enabled = True
                        Else
                            barsalesdetailsreport.Enabled = False
                        End If
                    End If
                    Dim ChronicalReport As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "ChronicalReport"
                    If ChronicalReport.Any Then
                        If ChronicalReport(0)("menu_active") = "1" Then
                            barbtnchronicalreport.Enabled = True
                        Else
                            barbtnchronicalreport.Enabled = False
                        End If
                    End If
                    Dim AdvancePayReport As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "AdvancePayReport"
                    If AdvancePayReport.Any Then
                        If AdvancePayReport(0)("menu_active") = "1" Then
                            barbtnadvancepaymentreport.Enabled = True
                        Else
                            barbtnadvancepaymentreport.Enabled = False
                        End If
                    End If
                    Dim MonthlySummaryReport As EnumerableRowCollection(Of DataRow) = From dtrow As DataRow In _JsonData.UserPolicyTable Where dtrow("menu_name") = "MonthlySummaryReport"
                    If MonthlySummaryReport.Any Then
                        If MonthlySummaryReport(0)("menu_active") = "1" Then
                            barbtnmonthlysummaryreport.Enabled = True
                        Else
                            barbtnmonthlysummaryreport.Enabled = False
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

    Private Sub barpurchasereport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barmasterpurchasereport.ItemClick
        Try
            FrmPurchaseView.MdiParent = Me
            FrmPurchaseView.Show()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub barpossales_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barpossales.ItemClick
        Try
            ' Show the new DevExpress LayoutControl-based POS form
            PosSalesII.MdiParent = Me
            PosSalesII.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barmastersalesreport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barsalessummaryreport.ItemClick
        Try
            ' Show the Sales Report form
            Dim salesReportForm As New frmSalesSummaryReport()
            salesReportForm.MdiParent = Me
            salesReportForm.Show()
        Catch ex As Exception
            MessageBox.Show("Error opening Sales Report: " & ex.Message, "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            frmPrintDesign.Show()
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

    Private Sub barbtnledgerReport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barledgerReport.ItemClick
        Try


            frmLedgerReport.MdiParent = Me
            frmLedgerReport.Text = "LedgerReport"
            frmLedgerReport.Show()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub barnewcustomer_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barnewcustomer.ItemClick
        Try
            FrmCustomerList.ShowDialog()
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
    Private Sub barsystemsettings_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barsystemsettings.ItemClick
        Try
            FrmPosSettings.MdiParent = Me
            FrmPosSettings.Show()
        Catch ex As Exception

        End Try
    End Sub
    ' Initialize the skin gallery using DevExpress SkinHelper (much more reliable!)
    Private Sub InitializeSkinGallery()
        Try
            ' Use DevExpress built-in SkinHelper to initialize the gallery
            ' This automatically populates the gallery with all available skins
            ' and handles skin preview images and application
            SkinHelper.InitSkinGallery(skinRibbonGalleryBarItem, True)

            ' Get the current active skin (from UserLookAndFeel or saved settings)
            Dim currentSkin As String = UserLookAndFeel.Default.SkinName
            If String.IsNullOrEmpty(currentSkin) OrElse currentSkin = "Default" Then
                currentSkin = SkinManager.GetSavedSkin()
            End If

            System.Diagnostics.Debug.WriteLine("InitializeSkinGallery - Current active skin: " & currentSkin)

            ' Find and select the current skin in the gallery
            If Not String.IsNullOrEmpty(currentSkin) Then
                Dim found As Boolean = False
                For Each group As DevExpress.XtraBars.Ribbon.GalleryItemGroup In skinRibbonGalleryBarItem.Gallery.Groups
                    For Each item As DevExpress.XtraBars.Ribbon.GalleryItem In group.Items
                        ' Check both Caption and Value properties for skin name matching
                        If item.Caption = currentSkin OrElse
                           (item.Value IsNot Nothing AndAlso item.Value.ToString() = currentSkin) Then
                            item.Checked = True
                            found = True
                            System.Diagnostics.Debug.WriteLine("Found and selected skin in gallery: " & currentSkin)
                            Exit For
                        End If
                    Next
                    If found Then Exit For
                Next

                If Not found Then
                    System.Diagnostics.Debug.WriteLine("Warning: Current skin '" & currentSkin & "' not found in gallery")
                End If
            End If

            System.Diagnostics.Debug.WriteLine("Skin gallery initialized using SkinHelper with " &
                                             skinRibbonGalleryBarItem.Gallery.Groups(0).Items.Count & " skins")
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error initializing skin gallery with SkinHelper: " & ex.Message)
        End Try
    End Sub

    ' Handle skin gallery item selection
    Private Sub skinRibbonGalleryBarItem_GalleryItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs) Handles skinRibbonGalleryBarItem.GalleryItemClick
        Try
            Dim selectedItem As DevExpress.XtraBars.Ribbon.GalleryItem = e.Item
            If selectedItem IsNot Nothing Then
                Dim skinName As String = ""

                ' Get skin name from the item (SkinHelper may use different properties)
                If selectedItem.Value IsNot Nothing Then
                    skinName = selectedItem.Value.ToString()
                ElseIf Not String.IsNullOrEmpty(selectedItem.Caption) Then
                    skinName = selectedItem.Caption
                End If

                If Not String.IsNullOrEmpty(skinName) Then

                    ' Save the skin setting (when using SkinHelper, skin is applied automatically)
                    SkinManager.SaveSkinSetting(skinName)

                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Skin '" & skinName & "' applied and saved!" & vbCrLf &
                        "Check Debug Output for persistence test results.",
                        "Theme Changed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error in SkinHelper GalleryItemClick: " & ex.Message & vbCrLf & ex.StackTrace)
            DevExpress.XtraEditors.XtraMessageBox.Show(
                "Error applying skin: " & ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub barnewdiscountmaster_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barnewdiscountmaster.ItemClick
        Try
            FrmDiscountMaster.MdiParent = Me
            FrmDiscountMaster.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barSalesCommission_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barSalesCommission.ItemClick
        Try
            FrmSalesCommission.MdiParent = Me
            FrmSalesCommission.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barsalesdetailsreport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barsalesdetailsreport.ItemClick
        Try
            frmSalesDetailsReport.MdiParent = Me
            frmSalesDetailsReport.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnchronicalreport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnchronicalreport.ItemClick
        Try
            frmSalesChronicalReport.MdiParent = Me
            frmSalesChronicalReport.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnadvancepaymentreport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnadvancepaymentreport.ItemClick
        Try
            frmAdvancePaymentReport.MdiParent = Me
            frmAdvancePaymentReport.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnmonthlysummaryreport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnmonthlysummaryreport.ItemClick
        Try
            frmMonthlySummaryReport.MdiParent = Me
            frmMonthlySummaryReport.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub barbtnmaingrouppolicy_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnmaingrouppolicy.ItemClick
        Try
            FrmMainGroupPolicy.MdiParent = Me
            FrmMainGroupPolicy.Show()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub barbtnMenuDesign1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnMenuDesign1.ItemClick
        Try
            FrmMenuDesign.MdiParent = Me
            FrmMenuDesign.Show()
        Catch ex As Exception

        End Try
    End Sub
End Class
