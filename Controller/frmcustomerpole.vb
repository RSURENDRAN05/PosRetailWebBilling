Imports System.Data
Imports System.IO
Imports System.IO.Ports

Public Class frmcustomerpole
    Dim myPort As Array
    Dim _dsM As DataTable
    Private Errstr As String = String.Empty
    Private Function CreateTableCom(ByRef ErrorMsg As String) As Boolean
        Try
            _dsM = New DataTable
            _dsM.TableName = "COMSettings"
            _dsM.Columns.Add("PortName", GetType(String)).DefaultValue = 0
            _dsM.Columns.Add("Parity", GetType(String)).DefaultValue = 0
            _dsM.Columns.Add("StopBits", GetType(String)).DefaultValue = 0
            _dsM.Columns.Add("DataBits", GetType(String)).DefaultValue = 0
            _dsM.Columns.Add("BaudRate", GetType(String)).DefaultValue = 0
            _dsM.Columns.Add("Startup", GetType(Integer)).DefaultValue = 2
            _dsM.Columns.Add("DefaultDisplay", GetType(String)).DefaultValue = "POSBean"
            If File.Exists(M_Details._appPath & "\Layout\CustomerCOMSettings.xml") Then
                _dsM.ReadXml(M_Details._appPath & "\Layout\CustomerCOMSettings.xml")
            End If
            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return False
        End Try
    End Function
    Private Sub frmWeighingScale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            myPort = IO.Ports.SerialPort.GetPortNames() 'Get all com ports available
            cmbportname.Properties.Items.Clear()
            For i = 0 To UBound(myPort)
                cmbportname.Properties.Items.Add(myPort(i))
            Next

            If CreateTableCom(Errstr) = False Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Errstr, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            If Not _dsM Is Nothing Then

                If _dsM.Rows.Count > 0 Then

                    cmbportname.Text = _dsM.Rows(0).Item("PortName")
                    cmbParity.Text = _dsM.Rows(0).Item("Parity")
                    cmbDataBit.Text = _dsM.Rows(0).Item("DataBits")
                    cmbStopBits.Text = _dsM.Rows(0).Item("StopBits")
                    cmbBaud.Text = _dsM.Rows(0).Item("BaudRate")
                    cmbStartupType.EditValue = Convert.ToInt32(_dsM.Rows(0).Item("Startup"))
                    txtDefaultDisplay.Text = _dsM.Rows(0).Item("DefaultDisplay")


                End If

            End If

        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim Input = InputBox("Port Name:")
            If Input <> "" Then
                cmbportname.Properties.Items.Add(Input)
            End If

        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            _dsM.Rows.Clear()
            _dsM.Rows.Add(cmbportname.Text, cmbParity.Text, cmbStopBits.Text, cmbDataBit.Text, cmbBaud.Text, cmbStartupType.EditValue, txtDefaultDisplay.Text)
            ' MDIForm.AlertControl1.Show(Me, Version, "Updated Successfully", MDIForm.ImageCollection1.Images(1))
            _dsM.WriteXml(M_Details._appPath & "\Layout\CustomerCOMSettings.xml", Data.XmlWriteMode.WriteSchema, True)
            CustomerDisplaySettings.BaudRate = cmbBaud.EditValue
            CustomerDisplaySettings.DataBits = cmbDataBit.EditValue
            CustomerDisplaySettings.DefaultDisplay = txtDefaultDisplay.Text
            CustomerDisplaySettings.Parity = cmbParity.EditValue
            CustomerDisplaySettings.PortName = cmbportname.EditValue
            CustomerDisplaySettings.Startup = cmbStartupType.EditValue
            CustomerDisplaySettings.StopBits = cmbStopBits.EditValue
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub barbtnTesting_Click(sender As Object, e As EventArgs) Handles barbtnTesting.Click
        Try

            Dim sp As New SerialPort()
            sp.PortName = cmbportname.Text
            sp.BaudRate = cmbBaud.EditValue

            Select Case cmbParity.Text
                Case "Even"
                    sp.Parity = Ports.Parity.Even
                Case "None"
                    sp.Parity = Ports.Parity.None
                Case "Mark"
                    sp.Parity = Ports.Parity.Mark
                Case "Odd"
                    sp.Parity = Ports.Parity.Odd
                Case "Space"
                    sp.Parity = Ports.Parity.Space
            End Select

            sp.DataBits = cmbDataBit.EditValue


            Select Case cmbStopBits.Text
                Case "None"
                    sp.StopBits = 0 ' Ports.StopBits.None
                Case "One"
                    sp.StopBits = 1 ' Ports.StopBits.One
                Case "OnePointFive"
                    sp.StopBits = 2.5 'Ports.StopBits.OnePointFive
                Case "Two"
                    sp.StopBits = 2 ' Ports.StopBits.Two
            End Select

            sp.Open()
            sp.WriteLine(txtDefaultDisplay.Text)
            sp.Close()
            sp.Dispose()
            sp = Nothing
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, M_Details.SoftwareVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
End Class