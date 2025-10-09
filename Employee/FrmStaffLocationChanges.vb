Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class FrmStaffLocationChanges
    Dim _selectedLocationId As String = ""
    Private Sub FrmStaffLocationChanges_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DataLoad()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DataLoad()
        Try
            If _JsonData.LocationTable.Rows.Count > 0 Then
                txtnewLocation.Properties.DataSource = _JsonData.LocationTable
            Else
                txtnewLocation.Properties.DataSource = Nothing
            End If
            If _JsonData.SalesManDataTable.Rows.Count > 0 Then
                GridControlcurStaff.DataSource = _JsonData.SalesManDataTable
            End If
        Catch ex As Exception
            messageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            'getLocationInfo()
            dialog.Caption = "Please wait..."
            dialog.Caption = "Loading data..."
            GetSalesmanData()
            DataLoad()
            GetDataNewLocation(_selectedLocationId)
        Catch ex As Exception
            dialog.Close()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dialog.Close()
        End Try
    End Sub
    Private Sub GetDataNewLocation(ByVal _selectedLocId As String)
        Try
            Dim Dt As New DataTable
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=13&Comid=" & _companyInfo.ComId & "&Locid=" & _selectedLocId)
            Dim Userparsejson As JObject = JObject.Parse(json)
            Dt = Userparsejson("Data").ToObject(Of DataTable)()
            If Dt.Rows.Count > 0 Then
                GridControlnewLoction.DataSource = Dt
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtnewLocation_EditValueChanged(sender As Object, e As EventArgs) Handles txtnewLocation.EditValueChanged
        Try
            _selectedLocationId = "0"
            _selectedLocationId = txtnewLocation.EditValue
            GetDataNewLocation(_selectedLocationId)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridControlcurStaff_DoubleClick(sender As Object, e As EventArgs) Handles GridControlcurStaff.DoubleClick
        Try

            Me.DialogResult = MessageBox.Show("Are you sure want to change location for selected staff?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If Me.DialogResult = DialogResult.Yes Then
                ChangeLocation()
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ChangeLocation()
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            Dim empid = GridView1.GetFocusedRowCellValue("Id")
            Dim empname = GridView1.GetFocusedRowCellValue("SalesMan")
            _selectedLocationId = "0"
            _selectedLocationId = txtnewLocation.EditValue
            If _selectedLocationId = "0" Then
                MessageBox.Show("Please select new location")
                Exit Sub
            End If
            dialog.Caption = "Please wait..."
            dialog.Caption = "Changing location for " & empname & " ..."
            Dim newlocation As New NewLocation
            newlocation.EmployeeId = empid
            newlocation.NewLocId = _selectedLocationId
            Dim jsonData As String = Newtonsoft.Json.JsonConvert.SerializeObject(newlocation)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New System.Net.WebClient().UploadString(M_Details.LinkAjaxRequest & "SalesManCommission=15", "POST", jsonData)
            Dim responseObj As JObject = JObject.Parse(response)
            If responseObj("Success").ToObject(Of Boolean)() Then
                MessageBox.Show("Location changed successfully for " & empname, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnRefresh.PerformClick()
                dialog.Close()
            Else
                MessageBox.Show("Failed to change location for " & empname, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            dialog.Close()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dialog.Close()
        End Try
    End Sub
    Private Sub ChangeLocationCurrent()
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            Dim empid = GridView2.GetFocusedRowCellValue("Id")
            Dim empname = GridView2.GetFocusedRowCellValue("SalesMan")
            _selectedLocationId = _companyInfo.LocId
            If _selectedLocationId = "0" Then
                MessageBox.Show("Please select new location")
                Exit Sub
            End If
            dialog.Caption = "Please wait..."
            dialog.Caption = "Changing location for " & empname & " ..."
            Dim newlocation As New NewLocation
            newlocation.EmployeeId = empid
            newlocation.NewLocId = _selectedLocationId
            Dim jsonData As String = Newtonsoft.Json.JsonConvert.SerializeObject(newlocation)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim response As String = New System.Net.WebClient().UploadString(M_Details.LinkAjaxRequest & "SalesManCommission=15", "POST", jsonData)
            Dim responseObj As JObject = JObject.Parse(response)
            If responseObj("Success").ToObject(Of Boolean)() Then
                MessageBox.Show("Location changed successfully for " & empname, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnRefresh.PerformClick()
                dialog.Close()
            Else
                MessageBox.Show("Failed to change location for " & empname, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            dialog.Close()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dialog.Close()
        End Try
    End Sub
    Private Sub GridControlnewLoction_DoubleClick(sender As Object, e As EventArgs) Handles GridControlnewLoction.DoubleClick
        Try

            Me.DialogResult = MessageBox.Show("Are you sure want to change location for selected staff?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If Me.DialogResult = DialogResult.Yes Then
                ChangeLocationCurrent()
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class

Public Class NewLocation
    Public Property EmployeeId As String
    Public Property NewLocId As String
End Class
