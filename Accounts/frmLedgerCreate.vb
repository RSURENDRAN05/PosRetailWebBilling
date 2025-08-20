Imports Newtonsoft.Json
Public Class frmLedgerCreate

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmLedgerCreate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            chkactive.CheckState = CheckState.Checked
            txtopendate.Text = Date.Now.ToString("dd-MM-yyyy")
            _DataLoad()
            _Clear()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _DataLoad()
        Try
            getLedgerInfo()
            If _JsonData.AccountLedger.Rows.Count > 0 Then
                GridControl1.DataSource = _JsonData.AccountLedger
            Else
                GridControl1.DataSource = Nothing
            End If
            getGroupInfo()
            If _JsonData.AccountGroup.Rows.Count > 0 Then
                txtgroupname.Properties.DataSource = _JsonData.AccountGroup
            End If
            getParentInfo()
            If _JsonData.AccountParent.Rows.Count > 0 Then
                txtparentname.Properties.DataSource = _JsonData.AccountParent
            End If
            chkactive.CheckState = CheckState.Checked
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _Clear()
        Try
            txtledgername.Text = ""
            txtopendate.Text = ""
            txtopeningbalance.EditValue = 0.0
            txtdrcr.SelectedText = "Dr"
            txtgroupname.EditValue = 1
            txtparentname.EditValue = 1
            txtopendate.Text = Date.Now.ToString("dd-MM-yyyy")
            chkactive.CheckState = CheckState.Checked
            btnsave.Text = "Save"
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim dialog As New DevExpress.Utils.WaitDialogForm()

        Try
            Dim strDate As String = ""
            _DateConversion(txtopendate.EditValue, strDate)
            Dim comp As New LedgerMaster
            If btnsave.Text = "Save" Then
                dialog.Caption = "Connecting To Server"
                If txtledgername.Text.Length > 0 Then

                    comp.ledgerId = 0
                    comp.ledgerrefId = 0
                    comp.ledgerName = txtledgername.Text
                    comp.ledgeropenDate = strDate
                    comp.ledgeropenbal = txtopeningbalance.EditValue
                    comp.ledgerdrcr = txtdrcr.SelectedItem.ToString
                    comp.ledgerType = txtledgertype.Text
                    comp.ledgergroupId = txtgroupname.EditValue
                    comp.ledgerparenId = txtparentname.EditValue
                    If chkactive.CheckState = CheckState.Checked Then
                        comp.ledgerActive = "Active"
                    Else
                        comp.ledgerActive = "InActive"
                    End If
                    Dim PostString As String = JsonConvert.SerializeObject(comp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AccountRequest=9&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _DataLoad()
                    End If
                    txtledgername.Text = ""
                    txtopeningbalance.EditValue = 0.0
                    txtdrcr.SelectedIndex = 0
                    txtgroupname.EditValue = 1
                    txtparentname.EditValue = 1
                    chkactive.CheckState = CheckState.Checked

                    btnsave.Text = "Save"
                End If
            Else
                dialog.Caption = "Connecting To Server"
                If txtgroupname.Text.Length > 0 Then
                    comp.ledgerId = txtledgerid.EditValue
                    comp.ledgerrefId = 0
                    comp.ledgerName = txtledgername.Text
                    comp.ledgeropenDate = strDate
                    comp.ledgeropenbal = txtopeningbalance.EditValue
                    comp.ledgerdrcr = txtdrcr.SelectedItem.ToString
                    comp.ledgerType = txtledgertype.Text
                    comp.ledgergroupId = txtgroupname.EditValue
                    comp.ledgerparenId = txtparentname.EditValue
                    If chkactive.CheckState = CheckState.Checked Then
                        comp.ledgerActive = "Active"
                    Else
                        comp.ledgerActive = "InActive"
                    End If

                    Dim PostString As String = JsonConvert.SerializeObject(comp)
                    If _JsonSend(M_Details.LinkAjaxRequest & "AccountRequest=10&json=" & PostString) = True Then
                        dialog.Caption = "Data Saved Success.."
                        _DataLoad()
                    End If
                End If
                txtledgername.Text = ""
                txtopeningbalance.EditValue = 0.0
                txtdrcr.SelectedIndex = 0
                txtgroupname.EditValue = 1
                txtparentname.EditValue = 1
                chkactive.CheckState = CheckState.Checked
                btnsave.Text = "Save"
            End If

        Catch ex As Exception
            dialog.Close()
        Finally
            dialog.Close()
        End Try
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        Try
            btnsave.Text = "Update"
            Dim id = GridView1.GetFocusedRowCellValue("Id")
            Dim ledgername = GridView1.GetFocusedRowCellValue("LedgerName")
            Dim groupname = GridView1.GetFocusedRowCellValue("GroupName")
            Dim parentname = GridView1.GetFocusedRowCellValue("ParentName")
            Dim drcr = GridView1.GetFocusedRowCellValue("DrCr")
            Dim active = GridView1.GetFocusedRowCellValue("Active")
            Dim opdate = GridView1.GetFocusedRowCellValue("OpenDate")
            Dim opbalance = GridView1.GetFocusedRowCellValue("OpeningBalance")
            Dim ledgertype = GridView1.GetFocusedRowCellValue("LedgerType")
            txtledgerid.Text = id
            txtledgername.Text = ledgername
            txtgroupname.Text = groupname
            txtparentname.Text = parentname
            txtopendate.Text = opdate
            If String.IsNullOrEmpty(opbalance) Then
                txtopeningbalance.Text = 0
            Else
                txtopeningbalance.Text = opbalance
            End If
            txtledgertype.Text = ledgertype
            If drcr = "Dr" Then
                txtdrcr.SelectedIndex = 0
            Else
                txtdrcr.SelectedIndex = 1
            End If
            If active = "1" Then
                chkactive.Checked = CheckState.Checked
            Else
                chkactive.Checked = CheckState.Unchecked
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
Class LedgerMaster
    Public Property ledgerId As String
    Public Property ledgerrefId As String
    Public Property ledgerName As String
    Public Property ledgerparenId As String
    Public Property ledgergroupId As String
    Public Property ledgerType As String
    Public Property ledgeropenDate As String
    Public Property ledgeropenbal As String
    Public Property ledgerdrcr As String
    Public Property ledgerActive As String
End Class
