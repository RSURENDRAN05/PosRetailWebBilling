Imports Microsoft.VisualBasic
Imports System.Windows
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Printing
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraBars.Helpers
Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.Data.DataTable
Imports System.Data.DataSet
Imports System.Linq
Imports DevExpress.XtraWaitForm.WaitForm
Imports System.IO.Ports
Imports System.Management
Public Class frmPrintProfile
    Private _dataset As New DataSet
    Dim PrintTable As DataTable
    Dim errMsg As String
    Dim _dsGetData As DataSet
    Dim _pCSettingDs As DataSet
    Dim EDIT As Boolean = False
    Dim _Mode As Integer
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            Me.Close()
            'Me.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmPCsettings_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            _createTable()
            _dxmlLoad()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _dxmlLoad()
        If File.Exists(M_Details._appPath & "\Settings\PrintProfileSetting.xml") Then
            PrintTable.Rows.Clear()
            _pCSettingDs = New DataSet
            _pCSettingDs.ReadXml(M_Details._appPath & "\Settings\PrintProfileSetting.xml")
            PrintTable.BeginInit()
            For Each ROW In _pCSettingDs.Tables(0).Rows
                PrintTable.Rows.Add(ROW(0), ROW(1), ROW(2), ROW(3), ROW(4), ROW(5))
            Next
            PrintTable.AcceptChanges()
            PrintTable.EndInit()
            GridControl1.DataSource = PrintTable
        End If

    End Sub
     
    Public Function _createTable() As DataTable
        Try
            PrintTable = New DataTable
            PrintTable.TableName = "ProfileSetting"
            PrintTable.Columns.Add("ProfileID", GetType(Integer)) '0
            PrintTable.Columns.Add("ProfileName", GetType(String)) '1
            PrintTable.Columns.Add("FileName", GetType(String)) '2
            PrintTable.Columns.Add("ProfileType", GetType(String)) '3
            PrintTable.Columns.Add("ProfileSingle", GetType(String)) '3
            PrintTable.Columns.Add("ProfileActive", GetType(String)) '4
            Return PrintTable
        Catch ex As Exception

            Return PrintTable
        End Try
    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim state As String
            If chkActive.Checked Then
                state = "Active"
            Else
                state = "In-Active"
            End If

            Select Case _Mode
                Case 1 'save
                    Dim Id As Integer = PrintTable.Rows.Count + 1
                    PrintTable.BeginInit()
                    PrintTable.NewRow()
                    PrintTable.Rows.Add(Id, txtproname.Text, txtfilename.Text, txtCmbtype.Text, txtsinglemulti.Text, state)
                    PrintTable.AcceptChanges()
                    PrintTable.EndInit()
                    PrintTable.WriteXml(M_Details._appPath & "\Settings\PrintProfileSetting.xml", Data.XmlWriteMode.WriteSchema, True)
                    _dxmlLoad()
                    _clear()
                    enb(False)
                    lblmode.Text = "Mode : " & saveMode._noneMode
                    _Mode = Upd_save._reset
                Case 0 'update
                    PrintTable.BeginInit()
                    PrintTable.Rows(GridView1.FocusedRowHandle)("ProfileID") = txtId.Text
                    PrintTable.Rows(GridView1.FocusedRowHandle)("ProfileName") = txtproname.Text
                    PrintTable.Rows(GridView1.FocusedRowHandle)("FileName") = txtfilename.Text
                    PrintTable.Rows(GridView1.FocusedRowHandle)("ProfileType") = txtCmbtype.Text
                    PrintTable.Rows(GridView1.FocusedRowHandle)("ProfileSingle") = txtsinglemulti.Text
                    PrintTable.Rows(GridView1.FocusedRowHandle)("FILENAME") = txtfilename.Text
                    PrintTable.Rows(GridView1.FocusedRowHandle)("ProfileActive") = state
                    PrintTable.AcceptChanges()
                    PrintTable.EndInit()
                    GridControl1.DataSource = PrintTable
                    PrintTable.WriteXml(M_Details._appPath & "\Settings\PrintProfileSetting.xml", Data.XmlWriteMode.WriteSchema, True)
                    _dxmlLoad()
                    _clear()
                    enb(False)
                    lblmode.Text = "Mode : " & saveMode._noneMode
                    _Mode = Upd_save._reset
            End Select

        Catch ex As Exception

        End Try
    End Sub
   
    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            Dim state As String
            lblmode.Text = "Mode : " & saveMode._updateMode
            _Mode = Upd_save._update
            enb(True)
            txtId.Text = PrintTable.Rows(GridView1.FocusedRowHandle)("ProfileID").ToString
            txtproname.Text = PrintTable.Rows(GridView1.FocusedRowHandle)("ProfileName")
            txtfilename.Text = PrintTable.Rows(GridView1.FocusedRowHandle)("FileName")
            txtCmbtype.Text = PrintTable.Rows(GridView1.FocusedRowHandle)("ProfileType")
            txtsinglemulti.Text = PrintTable.Rows(GridView1.FocusedRowHandle)("ProfileSingle")
            state = PrintTable.Rows(GridView1.FocusedRowHandle)("ProfileActive")
            If state = "Active" Then
                chkActive.Checked = True
            Else
                chkActive.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

   

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        Try
            lblMode.Text = "Mode : " & saveMode._newMode
            _Mode = Upd_save._save
            btnAdd.Enabled = True
            _clear()
            enb(True)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _clear()
        Try
            txtId.Text = ""
            txtfilename.Text = ""
            txtproname.Text = ""
            txtCmbtype.SelectedIndex = 0
            txtsinglemulti.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Private Sub enb(ByRef b As Boolean)
        Try

            txtId.Enabled = b
            txtfilename.Enabled = b
            txtproname.Enabled = b
            txtCmbtype.Enabled = b
            txtsinglemulti.Enabled = b
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmLoad()
        Try
            enb(False)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Try
            GridView1.DeleteSelectedRows()
            PrintTable.AcceptChanges()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtfilename_Click(sender As Object, e As EventArgs) Handles txtfilename.Click
        Try
            Dim OpenFileDialog1 As New OpenFileDialog
            OpenFileDialog1.Title = "Please select a Print profile"
            OpenFileDialog1.InitialDirectory = M_Details._appPath & "\Reports\"
            OpenFileDialog1.Filter = "Profile Files|*.repx"
            OpenFileDialog1.FileName = ""
            ' OpenFileDialog1.ShowDialog()
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                txtfilename.Text = OpenFileDialog1.SafeFileName
            Else
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnprintdesign_Click(sender As Object, e As EventArgs) Handles btnprintdesign.Click
        Try
            frmPrintDesign.Show()
        Catch ex As Exception

        End Try
    End Sub
End Class