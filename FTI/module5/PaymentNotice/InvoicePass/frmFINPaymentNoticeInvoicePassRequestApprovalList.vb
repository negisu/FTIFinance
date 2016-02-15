﻿Public Class frmFINPaymentNoticeInvoicePassRequestApprovalList

    Private Sub dataGridView1_CellClick(ByVal sender As Object, _
        ByVal e As DataGridViewCellEventArgs) _
        Handles DataGridView1.CellClick

        Try
            If e.RowIndex < 0 Or Not (e.ColumnIndex = DataGridView1.Columns("APPROVE").Index Or e.ColumnIndex = DataGridView1.Columns("REJECT").Index Or e.ColumnIndex = DataGridView1.Columns("PREVIEW").Index) Then Return
        Catch ex As Exception
            Return
        End Try

        Dim TRAN_NO As String = DataGridView1.Rows(e.RowIndex).Cells("TRAN_NO").Value.ToString()
        Dim HEADDataTable As DataTable
        Dim DETAILDataTable As DataTable
        Dim ADDRESSDataTable As DataTable
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", TRAN_NO)
        Dim query As String = "SELECT  TOP 1 * FROM PN_HEAD LEFT JOIN MB_COMP_PERSON ON PN_HEAD.AR_CODE = MB_COMP_PERSON.COMP_PERSON_CODE LEFT JOIN MB_PRENAME ON MB_COMP_PERSON.PREN_CODE = MB_PRENAME.PRENAME_CODE WHERE TRAN_NO = @p0"

        HEADDataTable = fillWebSQL(query, parameters, "PN_HEAD")

        query = "SELECT * FROM PN_DETAIL INNER JOIN IV_SUB_SECTION ON PN_DETAIL.SUB_SECTION_CODE = IV_SUB_SECTION.SUB_SECTION_CODE INNER JOIN SU_DIVISION ON IV_SUB_SECTION.DIV_CODE_INC=SU_DIVISION.DIV_CODE  WHERE TRAN_NO = @p0 ORDER BY SEQ"

        DETAILDataTable = fillWebSQL(query, parameters, "PN_DETAIL")

        query = "SELECT  TOP 2 * FROM            PN_ADDRESS WHERE           TRAN_NO = @p0 ORDER BY ADDR_SEQ "

        ADDRESSDataTable = fillWebSQL(query, parameters, "PN_ADDRESS")

        query = "UPDATE PN_HEAD SET POST_INVOICE_FLAG = @p1 WHERE TRAN_NO = @p0"


        Try
            If e.ColumnIndex = DataGridView1.Columns("APPROVE").Index Then
                If (MessageBox.Show("คุณต้องการที่จะอนุมัติการออกใบแจ้งหนี้จากเอกสารหมายเลข " & TRAN_NO & " ใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                    parameters.Add("@p1", "Q")
                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try
                    Me.frmFINPaymentNoticeCancleList_Load(sender, e)

                End If
            End If

        Catch ex As Exception

        End Try

        Try
            If e.ColumnIndex = DataGridView1.Columns("REJECT").Index Then
                If (MessageBox.Show("คุณไม่อนุมัติคำร้องขอยกเลิกเอกสารหมายเลข " & TRAN_NO & " ใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                    Dim myValue As String
                    myValue = InputBox("กรุณากรอกเหตุผลในการไม่อนุมัติ", "กรุณากรอกข้อมูล", "ไม่ได้กรอกเหตุผลในการไม่อนุมัติ")
                    If String.IsNullOrEmpty(myValue) Then
                        Call MsgBox("คุณยกเลิกการทำรายการ", MsgBoxStyle.Information, "แจ้งเตือน")
                    Else
                        query = "UPDATE PN_HEAD SET POST_INVOICE_FLAG = NULL , POST_INVOICE_REJECT_REASON = @p0  WHERE TRAN_NO = '" & TRAN_NO & "'"
                        parameters.Add("@p0", myValue)
                        Try
                            executeWebSQL(query, parameters)
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                        End Try
                        Me.frmFINPaymentNoticeCancleList_Load(sender, e)
                    End If
                End If
            End If

        Catch ex As Exception
        End Try

        Try
            If e.ColumnIndex = DataGridView1.Columns("PREVIEW").Index Then
                If fPN Is Nothing Then
                    fPN = New frmFINForm
                    fPN.TRAN_NOLabel.Text = TRAN_NO
                    'fPN.MdiParent = Me.MdiParent
                    fPN.WindowState = FormWindowState.Maximized
                    fPN.Show()
                    fPN.LockAllInput()
                Else
                    If (MessageBox.Show("มีหน้าต่างใบแจ้งชำระเปิดค้างไว้ คุณต้องการที่จะปิดและแก้ไขใบแจ้งชำระที่เลือกใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                        fPN.Dispose()
                        fPN = Nothing
                        fPN = New frmFINForm
                        fPN.TRAN_NOLabel.Text = TRAN_NO
                        'fPN.MdiParent = Me.MdiParent
                        fPN.WindowState = FormWindowState.Maximized
                        fPN.Show()
                        fPN.LockAllInput()
                    Else
                        'focus opening form
                        fPN.Show()
                        fPN.Focus()
                        fPN.LockAllInput()
                    End If
                End If
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub frmFINPaymentNoticeCancleList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.Columns.Clear()
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = String.Empty
        query &= "SELECT  * "
        query &= "FROM            PN_HEAD LEFT JOIN AR_MEMBER2 ON PN_HEAD.AR_CODE = AR_MEMBER2.AR_CODE "
        query &= "WHERE           POST_INVOICE_FLAG = @p0"

        Dim buttonColumn As New DataGridViewButtonColumn()
        buttonColumn.HeaderText = ""
        buttonColumn.Name = "APPROVE"
        buttonColumn.Text = "อนุมัติออกใบแจ้งหนี้"
        buttonColumn.UseColumnTextForButtonValue = True

        Dim buttonColumn2 As New DataGridViewButtonColumn()
        buttonColumn2.HeaderText = ""
        buttonColumn2.Name = "REJECT"
        buttonColumn2.Text = "ไม่อนุมัติ"
        buttonColumn2.UseColumnTextForButtonValue = True

        Dim previewButtonColumn As New DataGridViewButtonColumn()
        previewButtonColumn.HeaderText = "Preview"
        previewButtonColumn.Name = "PREVIEW"
        previewButtonColumn.Text = "ดู"
        previewButtonColumn.UseColumnTextForButtonValue = True
        previewButtonColumn.FlatStyle = FlatStyle.Flat

        parameters.Add("@p0", "P")
        DataGridView1.Columns.Add(buttonColumn)
        DataGridView1.Columns.Add(buttonColumn2)
        DataGridView1.Columns.Add(previewButtonColumn)

        DataGridView1.DataSource = fillWebSQL(query, parameters, "PN_HEAD")


        For i As Integer = 0 To DataGridView1.ColumnCount - 1
            DataGridView1.Columns(i).Visible = False
        Next
        DataGridView1.Columns("TRAN_NO").Visible = True
        DataGridView1.Columns("FULL_NAME").Visible = True
        Try
            DataGridView1.Columns("APPROVE").Visible = True
        Catch ex As Exception

        End Try

        Try
            DataGridView1.Columns("REJECT").Visible = True
        Catch ex As Exception

        End Try

        Try
            DataGridView1.Columns("PREVIEW").Visible = True
        Catch ex As Exception

        End Try
        DataGridView1.Columns("TRAN_NO").HeaderText = "เลขที่ใบแจ้งชำระ"
        DataGridView1.Columns("FULL_NAME").HeaderText = "ชื่อผู้จ่ายเงิน"

        DataGridView1.Columns("PREVIEW").DisplayIndex = 0
        DataGridView1.Columns("APPROVE").DisplayIndex = 1
        DataGridView1.Columns("REJECT").DisplayIndex = 2
        DataGridView1.Columns("TRAN_NO").DisplayIndex = 3
        DataGridView1.Columns("FULL_NAME").DisplayIndex = 4
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.AutoResizeColumns()
        DataGridView1.Refresh()
        DataGridView1.ClearSelection()
    End Sub

    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        Me.Dispose()
    End Sub
End Class