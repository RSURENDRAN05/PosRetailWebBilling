Imports System.Net
Imports Newtonsoft.Json.Linq
Imports DPFP
Imports DPFP.Capture
Imports DPFP.Processing
Imports DPFP.Verification
Public Class FrmFingerRegister
    Implements DPFP.Capture.EventHandler
    Private salesmenTable As DataTable
    Private Capturer As Capture
    Private Enroller As Enrollment   ' For registration
    Private Verifier As Verification ' For verification
#Region "InitalLoad"

    Private Sub SetupDataTables()
        ' Setup Salesmen DataTable
        salesmenTable = New DataTable()
        salesmenTable.Columns.Add("emp_id", GetType(Integer))
        salesmenTable.Columns.Add("emp_printname", GetType(String))
        GridControlEmpHeader.DataSource = salesmenTable
    End Sub
    Private Function GetEmployeeNameById(empId As Integer) As String
        Try
            For Each row As DataRow In salesmenTable.Rows
                If Convert.ToInt32(row("emp_id")) = empId Then
                    Return row("emp_printname").ToString()
                End If
            Next
            Return "Unknown Employee"
        Catch ex As Exception
            Return "Error Loading Name"
        End Try
    End Function

    Private Sub LoadSalesmen()
        Try
            Dim json As String = New WebClient().DownloadString(M_Details.LinkAjaxRequest & "SalesManCommission=1")
            Dim parsedJson As JObject = JObject.Parse(json)

            salesmenTable.Clear()
            If parsedJson("Success").ToString() = "True" Then
                Dim dataArray = parsedJson("Data")


                For Each item In dataArray
                    Dim empId As Integer = 0
                    Dim empName As String = ""

                    ' Try multiple possible field name variations for emp_id
                    If item("emp_id") IsNot Nothing AndAlso Not IsDBNull(item("emp_id")) Then
                        empId = Convert.ToInt32(item("emp_id"))
                    ElseIf item("EmpId") IsNot Nothing AndAlso Not IsDBNull(item("EmpId")) Then
                        empId = Convert.ToInt32(item("EmpId"))
                    ElseIf item("empid") IsNot Nothing AndAlso Not IsDBNull(item("empid")) Then
                        empId = Convert.ToInt32(item("empid"))
                    End If

                    ' Try multiple possible field name variations for employee name
                    If item("emp_printname") IsNot Nothing AndAlso item("emp_printname").ToString() <> "" Then
                        empName = item("emp_printname").ToString()
                    ElseIf item("EmpPrintName") IsNot Nothing AndAlso item("EmpPrintName").ToString() <> "" Then
                        empName = item("EmpPrintName").ToString()
                    ElseIf item("emp_firstname") IsNot Nothing AndAlso item("emp_firstname").ToString() <> "" Then
                        empName = item("emp_firstname").ToString()
                    ElseIf item("EmpFirstName") IsNot Nothing AndAlso item("EmpFirstName").ToString() <> "" Then
                        empName = item("EmpFirstName").ToString()
                    ElseIf item("emp_name") IsNot Nothing AndAlso item("emp_name").ToString() <> "" Then
                        empName = item("emp_name").ToString()
                    ElseIf item("EmpName") IsNot Nothing AndAlso item("EmpName").ToString() <> "" Then
                        empName = item("EmpName").ToString()
                    End If

                    salesmenTable.Rows.Add(empId, empName)
                Next

            Else
                MessageBox.Show("Failed to load salesmen: " & parsedJson("Msg").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            ' Refresh the Salesmen GridControl
            GridControlEmpHeader.RefreshDataSource()
            GridControlEmpHeader.Refresh()
        Catch ex As Exception
            MessageBox.Show("Error loading salesmen: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            LoadSalesmen()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FrmFingerRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetupDataTables()
            LoadSalesmen()
        Catch ex As Exception

        End Try
    End Sub


#End Region
#Region "ScanFingerPrintModule"
    Private Sub GridViewEmpHeader_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridViewEmpHeader.RowClick
        Try
            Dim foucustedRow As Integer = 0
            foucustedRow = GridViewEmpHeader.FocusedRowHandle
            Dim empId = GridViewEmpHeader.GetFocusedRowCellValue("emp_id")
            Dim empName = GridViewEmpHeader.GetFocusedRowCellValue("emp_printname")
            lblempid.Text = empId
            lblempname.Text = empName
        Catch ex As Exception

        End Try
    End Sub
    Private Sub StartCapture()
        Try
            Capturer = New Capture()
            If Capturer IsNot Nothing Then
                Capturer.EventHandler = Me
                Capturer.StartCapture()
                MessageBox.Show("Place your finger on the reader.")
            Else
                MessageBox.Show("Unable to start capture.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Public Sub OnComplete(Capture As Object, ReaderSerialNumber As String, Sample As Sample) _
    Implements EventHandler.OnComplete

        ' Convert the sample to an image
        Dim bmp As Bitmap = ConvertSampleToBitmap(Sample)
        lblfingerimage.Image = bmp  ' Show fingerprint image (PictureBox1: 150x200)

        ' Process the fingerprint (for template)
        ProcessSample(Sample)
    End Sub

    Public Sub OnFingerTouch(Capture As Object, ReaderSerialNumber As String) _
        Implements EventHandler.OnFingerTouch
        ' Finger touched
    End Sub

    Public Sub OnFingerGone(Capture As Object, ReaderSerialNumber As String) _
        Implements EventHandler.OnFingerGone
        ' Finger removed
    End Sub

    Public Sub OnReaderConnect(Capture As Object, ReaderSerialNumber As String) _
        Implements EventHandler.OnReaderConnect
        MessageBox.Show("Reader connected.")
    End Sub

    Public Sub OnReaderDisconnect(Capture As Object, ReaderSerialNumber As String) _
        Implements EventHandler.OnReaderDisconnect
        MessageBox.Show("Reader disconnected.")
    End Sub

    Public Sub OnSampleQuality(Capture As Object, ReaderSerialNumber As String, Feedback As CaptureFeedback) _
        Implements EventHandler.OnSampleQuality
        ' Can check quality here
    End Sub
    Private Function ConvertSampleToBitmap(Sample As Sample) As Bitmap
        Dim convertor As New DPFP.Capture.SampleConversion()
        Dim bitmap As Bitmap = Nothing
        convertor.ConvertToPicture(Sample, bitmap)
        Return bitmap
    End Function
    Private Sub ProcessSample(Sample As Sample)
        Dim features As DPFP.FeatureSet = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Enrollment)

        If features IsNot Nothing Then
            If Enroller Is Nothing Then Enroller = New Enrollment()

            Enroller.AddFeatures(features)

            If Enroller.TemplateStatus = Enrollment.Status.Ready Then
                ' Save template to DB (as byte array)
                Dim template As DPFP.Template = Enroller.Template
                Dim stream As New IO.MemoryStream()
                template.Serialize(stream)
                Dim bytes() As Byte = stream.ToArray()

                ' TODO: Upload `bytes` to MySQL for emp_id
                MessageBox.Show("Fingerprint captured & template created.")
                Capturer.StopCapture()
            End If
        End If
    End Sub

    Private Function ExtractFeatures(Sample As Sample, purpose As DPFP.Processing.DataPurpose) As DPFP.FeatureSet
        Dim extractor As New DPFP.Processing.FeatureExtraction()
        Dim feedback As DPFP.Capture.CaptureFeedback = Nothing
        Dim features As New DPFP.FeatureSet()
        extractor.CreateFeatureSet(Sample, purpose, feedback, features)

        If feedback = DPFP.Capture.CaptureFeedback.Good Then
            Return features
        Else
            Return Nothing
        End If
    End Function
    Private Sub btnstartCapture_Click(sender As Object, e As EventArgs) Handles btnstartCapture.Click
        Try
            StartCapture()
        Catch ex As Exception

        End Try
    End Sub
#End Region

   
End Class