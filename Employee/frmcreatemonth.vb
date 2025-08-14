Imports Newtonsoft.Json
Public Class frmcreatemonth
    Dim _mode As String = "New"
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
            Dim clsmonthofsalary As New monthofsalary
            If txtmonthname.Text = "" OrElse String.IsNullOrEmpty(txtmonthname.Text) Then
                MessageBox.Show("MonthName Missing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Sub
            Else
                If _mode = "New" Then
                    clsmonthofsalary.pems_id = 0
                    clsmonthofsalary.pems_monthname = txtmonthname.Text
                    clsmonthofsalary.pems_active = chkactive.CheckState
                    Dim PostString As String = JsonConvert.SerializeObject(clsmonthofsalary)
                    If _JsonSend(M_Details.LinkAjaxRequest & "EmployeeReq=12&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _DataLoad()
                        _Clear()
                    End If
                Else
                    clsmonthofsalary.pems_id = txtmonthId.Text
                    clsmonthofsalary.pems_monthname = txtmonthname.Text
                    clsmonthofsalary.pems_active = chkactive.CheckState
                    Dim PostString As String = JsonConvert.SerializeObject(clsmonthofsalary)
                    If _JsonSend(M_Details.LinkAjaxRequest & "EmployeeReq=13&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _DataLoad()
                        _Clear()
                    End If
                End If
            End If
        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub

    Private Sub frmcreatemonth_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            
            _DataLoad()
            _Clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _Clear()
        Try
            chkactive.CheckState = CheckState.Checked
            txtmonthId.Text = ""
            txtmonthname.Text = ""
            _mode = "New"
            btnsave.Text = "Save"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _DataLoad()
        Try
            getMonthOfSalaryInfo()
            If _JsonData.MonthOfSalary.Rows.Count > 0 Then
                GridControl1.DataSource = _JsonData.MonthOfSalary
            Else
                GridControl1.DataSource = Nothing
            End If
        Catch ex As Exception
            GridControl1.DataSource = Nothing
        End Try
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            _mode = "Update"
            btnsave.Text = "Update"
            Dim id = GridView1.GetFocusedRowCellValue("Id")
            Dim name = GridView1.GetFocusedRowCellValue("MonthName")
            Dim active = GridView1.GetFocusedRowCellValue("Active")
            txtmonthId.Text = id
            txtmonthname.Text = name
            If active = "Active" Then
                chkactive.CheckState = CheckState.Checked
            Else
                chkactive.CheckState = CheckState.Unchecked
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnclear_Click(sender As Object, e As EventArgs) Handles btnclear.Click
        Try
            _Clear()
        Catch ex As Exception

        End Try
    End Sub
End Class
Public Class monthofsalary
    Public Property pems_id As String
    Public Property pems_monthname As String
    Public Property pems_active As String
End Class