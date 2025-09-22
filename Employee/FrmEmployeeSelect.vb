Public Class FrmEmployeeSelect
    Private employeeTable As DataTable
    Public Property SelectedEmpId As Integer = 0
    Public Property SelectedEmpName As String = ""

    Private Sub FrmEmployeeSelect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadEmployees()
    End Sub

    Private Sub LoadEmployees()
        Try
            employeeTable = New DataTable()
            employeeTable.Columns.Add("emp_id", GetType(Integer))
            employeeTable.Columns.Add("emp_printname", GetType(String))

            Dim json As String = New System.Net.WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=1")
            Dim parsedJson As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.Linq.JObject.Parse(json)

            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")
                For Each item In dataArray
                    Dim empId As Integer = 0
                    Dim empName As String = ""

                    If item("emp_id") IsNot Nothing Then
                        empId = Convert.ToInt32(item("emp_id"))
                    End If

                    If item("emp_printname") IsNot Nothing Then
                        empName = item("emp_printname").ToString()
                    End If

                    If empId > 0 AndAlso Not String.IsNullOrEmpty(empName) Then
                        employeeTable.Rows.Add(empId, empName)
                    End If
                Next
            End If

            lstEmployees.DataSource = employeeTable
            lstEmployees.DisplayMember = "emp_printname"
            lstEmployees.ValueMember = "emp_id"

        Catch ex As Exception
            MessageBox.Show("Error loading employees: " & ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If lstEmployees.SelectedIndex >= 0 Then
            SelectedEmpId = Convert.ToInt32(lstEmployees.SelectedValue)
            SelectedEmpName = lstEmployees.Text
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            MessageBox.Show("Please select an employee.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub lstEmployees_DoubleClick(sender As Object, e As EventArgs) Handles lstEmployees.DoubleClick
        btnOK_Click(sender, e)
    End Sub
End Class
