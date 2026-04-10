Imports System.Data
Imports System.Data.SqlClient
Public Class frmBarcodedesigner
    Public txtID As Integer = 0
    Const caption As String = "Barcode Designer"
    Private CloseFlag As Boolean = False
    Private ErrorMessage As String = String.Empty
    Dim G_BarcodeName As String = String.Empty
#Region "Method"

    Private Function LoadMethod(ByRef ErrorMsg As String) As Boolean
        Try

            'Dim _DataSet As New DataSet
            '_DataSet = dbcls._sqlDataAdapter("BarcodeLabel_load", Nothing)
            'GridControlBarcodelabel.DataSource = _DataSet.Tables(0)
            'G_BarcodeName = String.Empty
            Return True
        Catch ex As Exception

            ErrorMsg = ex.Message
            Return False
        End Try

    End Function

    Private Function _ClearMethod() As Boolean
        Try
            btndelete.Enabled = False
            RichEditControlBarcodeText.Text = ""
            btnSave.Text = "Save"
            G_BarcodeName = String.Empty
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub SaveMethod()

        Try
            If RichEditControlBarcodeText.Text = String.Empty Then


                RichEditControlBarcodeText.Focus()


            Else

                G_BarcodeName = String.Empty
                If frmInputBox.ShowDialog = Windows.Forms.DialogResult.OK Then

                    Dim _SqlParameter(1) As SqlParameter
                    _SqlParameter(0) = New SqlParameter("@Barcode", RichEditControlBarcodeText.Text)
                    _SqlParameter(1) = New SqlParameter("@BarcodeName", G_BarcodeName)
                    ' dbcls._ExecuteNonQuery("[Barcodelabel_insert]", _SqlParameter)
                    'MDIForm.AlertControl1.Show(Me, caption, String.Format("The {0} is Saved Successfully", G_BarcodeName), MDIForm.ImageCollection1.Images(1))
                    _ClearMethod()
                    LoadMethod(ErrorMessage)

                End If


            End If

        Catch ex As Exception

            'MDIForm.AlertControl1.Show(Me, caption, ex.Message, MDIForm.ImageCollection1.Images(0))
        End Try

    End Sub

    Private Sub UpdateMethod()

        Try
            If RichEditControlBarcodeText.Text = String.Empty Then

                ' MDIForm.AlertControl1.Show(Me, caption, "Please Enter the Brand Name", MDIForm.ImageCollection1.Images(0))
                RichEditControlBarcodeText.Focus()


            Else

                '  G_BarcodeName = String.Empty


                Dim _SqlParameter(1) As SqlParameter
                _SqlParameter(0) = New SqlParameter("@Barcode", RichEditControlBarcodeText.Text)
                _SqlParameter(1) = New SqlParameter("@BarcodeName", G_BarcodeName)
                ' dbcls._ExecuteNonQuery("[Barcodelabel_update]", _SqlParameter)
                '  MDIForm.AlertControl1.Show(Me, caption, String.Format("The {0} is Saved Successfully", G_BarcodeName), MDIForm.ImageCollection1.Images(1))
                _ClearMethod()
                LoadMethod(ErrorMessage)


            End If

        Catch ex As Exception

            '  MDIForm.AlertControl1.Show(Me, caption, ex.Message, MDIForm.ImageCollection1.Images(0))

        End Try

    End Sub

    Private Sub DeleteMethod()

        Try
            If RichEditControlBarcodeText.Text = String.Empty Then

                '  MDIForm.AlertControl1.Show(Me, caption, "Please Enter the Brand Name", MDIForm.ImageCollection1.Images(0))
                RichEditControlBarcodeText.Focus()


            Else

                '  G_BarcodeName = String.Empty


                Dim _SqlParameter(1) As SqlParameter
                _SqlParameter(0) = New SqlParameter("@Barcode", RichEditControlBarcodeText.Text)
                _SqlParameter(1) = New SqlParameter("@BarcodeName", G_BarcodeName)
                ' dbcls._ExecuteNonQuery("[Barcodelabel_delete]", _SqlParameter)
                ' MDIForm.AlertControl1.Show(Me, caption, String.Format("The {0} is Saved Successfully", G_BarcodeName), MDIForm.ImageCollection1.Images(1))
                _ClearMethod()
                LoadMethod(ErrorMessage)


            End If

        Catch ex As Exception

            ' MDIForm.AlertControl1.Show(Me, caption, ex.Message, MDIForm.ImageCollection1.Images(0))

        End Try

    End Sub
#End Region


    Private Sub frmBarcodedesigner_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadMethod(ErrorMessage)
        _ClearMethod()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            If btnSave.Text = "Save" Then
                SaveMethod()
            ElseIf btnSave.Text = "Update" Then
                UpdateMethod()
            End If

        Catch ex As Exception
            ' MDIForm.AlertControl1.Show(Me, caption, ex.Message, MDIForm.ImageCollection1.Images(0))
        End Try

    End Sub

    Private Sub GridControlBarcodelabel_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridControlBarcodelabel.MouseDoubleClick
        Try


            Dim hi As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = GridViewBarcodeLabel.CalcHitInfo((TryCast(sender, Control)).PointToClient(Control.MousePosition))
            If hi.RowHandle >= 0 Then

                ' txtID = GridViewBarcodeLabel.GetDataRow(hi.RowHandle).Item("B_ID").ToString
                G_BarcodeName = GridViewBarcodeLabel.GetDataRow(hi.RowHandle).Item("BarcodeName").ToString()
                RichEditControlBarcodeText.Text = GridViewBarcodeLabel.GetDataRow(hi.RowHandle).Item("Barcode").ToString()

                btnSave.Text = "Update"
                btndelete.Enabled = True

            Else

                _ClearMethod()

            End If
        Catch ex As Exception
            '  MDIForm.AlertControl1.Show(Me, caption, ex.Message, MDIForm.ImageCollection1.Images(0))
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        _ClearMethod()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click

        If DevExpress.XtraEditors.XtraMessageBox.Show("Are you sure to delete?", caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            DeleteMethod()
        End If

    End Sub
End Class