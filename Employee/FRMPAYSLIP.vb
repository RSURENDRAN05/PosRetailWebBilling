
Imports System.Data.SqlClient
Imports System.Data
Imports System.Windows
Imports System.Globalization
Imports System.IO

Public Class FRMPAYSLIP
    Dim Excel As String
    Dim MyConnection As System.Data.OleDb.OleDbConnection
    Dim DtSet As System.Data.DataSet
    Dim _DSFORM As New DataSet
    Dim DaReader As SqlDataReader
    Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
    Dim OpFile As New OpenFileDialog
    Dim _fileExten As String
    Function _ExcelSheetLoad() As Boolean
        Try
            OpFile.InitialDirectory = "D:\"
            OpFile.Filter = "All Files (*.*)|*.*|Excel files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv|XLS Files (*.xls)|*xls"
            If OpFile.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

                Dim _con As String = String.Empty
                Dim fi As New IO.FileInfo(OpFile.FileName)
                Dim fileName As String = OpFile.FileName
                _btnbrowse.Text = fileName
                Excel = fi.FullName
                _fileExten = Path.GetExtension(fileName)

                Select Case _fileExten
                    Case ".xls"
                        _con = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Excel + ";Extended Properties='Excel 8.0 Xml;HDR=YES'"

                    Case ".xlsx"
                        _con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Excel + ";Extended Properties='Excel 12.0 Xml;HDR=YES'"
                End Select
                MyConnection = New System.Data.OleDb.OleDbConnection(_con)
                MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection)
                MyCommand.TableMappings.Add("Table", "TimeSheet")
                DtSet = New System.Data.DataSet
                MyCommand.Fill(DtSet)
                GridControl1.DataSource = DtSet.Tables(0)
                MyConnection.Close()

            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error On Loading")
            Return False
        End Try
    End Function
    Private Sub _PRINTFORLOAD()
        Try
            _DSFORM.ReadXml(AppDomain.CurrentDomain.BaseDirectory & "\Print\PRINTFORMAT.XML")
            Dim tA As DataTable
            tA = _DSFORM.Tables(0)
            For Each _ROW As DataRow In tA.Rows
                ComboBoxEdit1.Properties.Items.Add(_ROW(1))
            Next
            ComboBoxEdit1.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _btnbrowse_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles _btnbrowse.ButtonClick
        Try
            If _ExcelSheetLoad() = False Then
                '   MessageBox.Show("Load", "Failed")
            Else
                DtSet.Tables(0).TableName = "Ram"
                DtSet.WriteXml(AppDomain.CurrentDomain.BaseDirectory & "\Print\PAYSLIP.XML", XmlWriteMode.IgnoreSchema)
            End If
        Catch ex As Exception

        End Try
    End Sub

   
    Public Function printa(ByVal b As Boolean) As Boolean
        Try
            Dim ST As String = ComboBoxEdit1.SelectedItem

            Dim _rptstaf As New rptStaffProfile
            DtSet.Tables(0).TableName = "Ram"
            _rptstaf.LoadLayout(AppDomain.CurrentDomain.BaseDirectory & ST.ToString)
            _rptstaf.DataSource = DtSet.Tables(0)
            Dim pt As New DevExpress.XtraReports.UI.ReportPrintTool(_rptstaf)
            If b = True Then
                pt.ShowPreviewDialog()

            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Try
            printa(True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FRMPAYSLIP_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            _PRINTFORLOAD()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _BTNEXPORT_Click(sender As Object, e As EventArgs) Handles _BTNEXPORT.Click
        Try
            GridControl1.ShowPrintPreview()
        Catch ex As Exception

        End Try
    End Sub
End Class