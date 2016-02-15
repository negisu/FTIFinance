Public Class frmFINPaymentNoticeCancelRequestList
    Public TRAN_TYPE As String

    Private Sub frmFINPaymentNoticeSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If TRAN_TYPE = "I1" Then
            Me.Text = "ยกเลิกใบแจ้งหนี้"
            Label1.Text = "ยกเลิกใบแจ้งหนี้"
        End If

        RefTextBox.Enabled = False
        FromDateTextBox.Enabled = False
        ToDateTextBox.Enabled = False

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub isRef_CheckedChanged(sender As Object, e As EventArgs) Handles isRef.CheckedChanged
        RefTextBox.Enabled = isRef.Checked
    End Sub

    Private Sub isFromDate_CheckedChanged(sender As Object, e As EventArgs) Handles isFromDate.CheckedChanged
        FromDateTextBox.Enabled = isFromDate.Checked
    End Sub

    Private Sub isToDate_CheckedChanged(sender As Object, e As EventArgs) Handles isToDate.CheckedChanged
        ToDateTextBox.Enabled = isToDate.Checked
    End Sub

    Private Sub btFind_Click(sender As Object, e As EventArgs) Handles btFind.Click
        DataGridView1.Columns.Clear()

        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = "SELECT TOP 100 * FROM PN_HEAD LEFT JOIN AR_MEMBER2 ON PN_HEAD.AR_CODE = AR_MEMBER2.AR_CODE LEFT JOIN SU_DIVISION ON PN_HEAD.DIV_CODE = SU_DIVISION.DIV_CODE WHERE "

        If PermissionHelper.isAdmin() Then

        Else
            query &= " PN_HEAD.DIV_CODE = @p9 "
            parameters.Add("@p9", user_div)
        End If

        query &= " AND TRAN_TYPE = @p10 "
        parameters.Add("@p10", TRAN_TYPE)
        'query &= " AND CANCEL_FLAG IS NULL "

        If isRef.Checked And Not String.IsNullOrEmpty(RefTextBox.Text) Then
            Dim searchValue As String = RefTextBox.Text.Trim.Replace(" ", "%")
            searchValue = "%" & searchValue & "%"
            query &= " AND TRAN_NO LIKE @p0 "
            parameters.Add("@p0", searchValue)

        End If
        If isFromDate.Checked And Not String.IsNullOrEmpty(FromDateTextBox.Text.Replace("/", String.Empty).Trim) Then
            query &= " AND TRAN_DATE >=  @p1"
            parameters.Add("@p1", Date.ParseExact(FromDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat))
        End If
        If isToDate.Checked And Not String.IsNullOrEmpty(ToDateTextBox.Text.Replace("/", String.Empty).Trim) Then
            query &= " AND TRAN_DATE <=  @p2"
            parameters.Add("@p2", Date.ParseExact(ToDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat))
        End If

        query = query.Replace("WHERE  AND", "WHERE ")
        query = query.Trim()
        If query.Substring(query.Length - 5, 5) = "WHERE" Then
            query = query.Replace("WHERE", String.Empty)
        End If
        DataGridView1.DataSource = fillWebSQL(query, parameters, "PN_HEAD")

        For i As Integer = 0 To DataGridView1.ColumnCount - 1
            DataGridView1.Columns(i).Visible = False
        Next

        Dim gridCheckbox As New DataGridViewCheckBoxColumn
        gridCheckbox.Name = "CANCEL_REQUEST"


        Dim restoreButtonColumn As New DataGridViewButtonColumn()
        restoreButtonColumn.HeaderText = "ถอนคำร้อง"
        restoreButtonColumn.Name = "RESTORE"
        restoreButtonColumn.Text = "ถอน"
        restoreButtonColumn.UseColumnTextForButtonValue = True
        restoreButtonColumn.FlatStyle = FlatStyle.Flat

        Dim previewButtonColumn As New DataGridViewButtonColumn()
        previewButtonColumn.HeaderText = "Preview"
        previewButtonColumn.Name = "PREVIEW"
        previewButtonColumn.Text = "ดู"
        previewButtonColumn.UseColumnTextForButtonValue = True
        previewButtonColumn.FlatStyle = FlatStyle.Flat

        Dim statusTextBox As New DataGridViewTextBoxColumn
        statusTextBox.ReadOnly = True
        statusTextBox.Name = "STATUS"
        statusTextBox.HeaderText = "สถานะเอกสาร"


        DataGridView1.Columns.Add(statusTextBox)
        DataGridView1.Columns.Add(gridCheckbox)
        DataGridView1.Columns.Add(restoreButtonColumn)
        DataGridView1.Columns.Add(previewButtonColumn)

        DataGridView1.Columns("DIV_NAME").HeaderText = "หน่วยงาน"
        DataGridView1.Columns("TRAN_DATE").HeaderText = "วันที่ทำรายการ"
        DataGridView1.Columns("TRAN_NO").HeaderText = "เลขที่เอกสาร"
        DataGridView1.Columns("FULL_NAME").HeaderText = "ชื่อผู้จ่ายเงิน"
        DataGridView1.Columns("BAL_AMT").HeaderText = "ยอดค้างชำระ"
        DataGridView1.Columns("NOTE").HeaderText = "หมายเหตุ"
        DataGridView1.Columns("CANCEL_REQUEST").HeaderText = "ยกเลิก"

        DataGridView1.Columns("CANCEL_REASON").HeaderText = "เหตุผลที่ต้องการยกเลิก"
        DataGridView1.Columns("CANCEL_REJECT_REASON").HeaderText = "เหตุผลที่ไม่อนุมัติยกเลิก"

        DataGridView1.Columns("PREVIEW").DisplayIndex = 0
        DataGridView1.Columns("STATUS").DisplayIndex = 1
        DataGridView1.Columns("RESTORE").DisplayIndex = 2
        DataGridView1.Columns("CANCEL_REQUEST").DisplayIndex = 3
        DataGridView1.Columns("CANCEL_REASON").DisplayIndex = 4
        DataGridView1.Columns("CANCEL_REJECT_REASON").DisplayIndex = 5
        DataGridView1.Columns("TRAN_DATE").DisplayIndex = 6
        DataGridView1.Columns("TRAN_NO").DisplayIndex = 7
        DataGridView1.Columns("FULL_NAME").DisplayIndex = 8
        DataGridView1.Columns("DIV_NAME").DisplayIndex = 9
        DataGridView1.Columns("BAL_AMT").DisplayIndex = 10
        DataGridView1.Columns("NOTE").DisplayIndex = 11


        DataGridView1.Columns("DIV_NAME").Visible = True
        DataGridView1.Columns("TRAN_DATE").Visible = True
        DataGridView1.Columns("TRAN_NO").Visible = True
        DataGridView1.Columns("FULL_NAME").Visible = True
        DataGridView1.Columns("BAL_AMT").Visible = True
        DataGridView1.Columns("NOTE").Visible = True
        DataGridView1.Columns("CANCEL_REASON").Visible = True
        DataGridView1.Columns("CANCEL_REJECT_REASON").Visible = True

        DataGridView1.Columns("DIV_NAME").ReadOnly = True
        DataGridView1.Columns("TRAN_DATE").ReadOnly = True
        DataGridView1.Columns("TRAN_NO").ReadOnly = True
        DataGridView1.Columns("FULL_NAME").ReadOnly = True
        DataGridView1.Columns("BAL_AMT").ReadOnly = True
        DataGridView1.Columns("NOTE").ReadOnly = True
        DataGridView1.Columns("CANCEL_REASON").ReadOnly = True
        DataGridView1.Columns("CANCEL_REJECT_REASON").ReadOnly = True



        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.AutoResizeColumns()
        DataGridView1.Refresh()

        For Each row As DataGridViewRow In DataGridView1.Rows
            If String.IsNullOrEmpty(row.Cells("CANCEL_FLAG").Value.ToString()) Then
                If String.IsNullOrEmpty(row.Cells("CANCEL_REJECT_REASON").Value.ToString()) Then
                    row.Cells("STATUS").Value = "ปกติ"
                    row.Cells("STATUS").Style.BackColor = ColorTranslator.FromHtml("#CCFFCC")
                    Dim buttonCell As DataGridViewButtonCell = CType(row.Cells("RESTORE"), DataGridViewButtonCell)
                    buttonCell.Style.BackColor = Color.FromName("ControlLight")
                Else
                    row.Cells("STATUS").Value = "ไม่อนุมัติยกเลิก"
                    row.Cells("STATUS").Style.BackColor = ColorTranslator.FromHtml("#CCFFFF")
                    Dim buttonCell As DataGridViewButtonCell = CType(row.Cells("RESTORE"), DataGridViewButtonCell)
                    buttonCell.Style.BackColor = Color.FromName("ControlLight")
                End If


            Else
                If row.Cells("CANCEL_FLAG").Value.ToString() = "P" Then
                    row.Cells("STATUS").Value = "รออนุมัติคำร้องยกเลิก"
                    row.Cells("STATUS").Style.BackColor = ColorTranslator.FromHtml("#FFFFCC")
                    Dim checkBoxCell As DataGridViewCheckBoxCell = CType(row.Cells("CANCEL_REQUEST"), DataGridViewCheckBoxCell)
                    Dim textBoxCell As DataGridViewTextBoxCell = CType(row.Cells("CANCEL_REASON"), DataGridViewTextBoxCell)
                    checkBoxCell.Style.BackColor = Color.FromName("ControlLight")
                    checkBoxCell.ReadOnly = True
                    textBoxCell.Style.BackColor = Color.FromName("ControlLight")
                    Dim buttonCell As DataGridViewButtonCell = CType(row.Cells("RESTORE"), DataGridViewButtonCell)
                    buttonCell.Style.BackColor = ColorTranslator.FromHtml("#FFCC99")


                Else
                    If row.Cells("CANCEL_FLAG").Value.ToString() = "A" Then
                        row.Cells("STATUS").Value = "อนุมัติคำร้องยกเลิก"
                        row.Cells("STATUS").Style.BackColor = ColorTranslator.FromHtml("#FF6699")
                        Dim buttonCell As DataGridViewButtonCell = CType(row.Cells("RESTORE"), DataGridViewButtonCell)
                        buttonCell.Style.BackColor = Color.FromName("ControlLight")


                        Dim checkBoxCell As DataGridViewCheckBoxCell = CType(row.Cells("CANCEL_REQUEST"), DataGridViewCheckBoxCell)
                        Dim textBoxCell As DataGridViewTextBoxCell = CType(row.Cells("CANCEL_REASON"), DataGridViewTextBoxCell)
                        checkBoxCell.Style.BackColor = Color.FromName("ControlLight")
                        checkBoxCell.ReadOnly = True
                        textBoxCell.Style.BackColor = Color.FromName("ControlLight")

                    End If
                End If

            End If

            If String.IsNullOrEmpty(row.Cells("CANCEL_REASON").Value.ToString()) Then
                row.Cells("CANCEL_REASON").ReadOnly = False
            End If
        Next
        DataGridView1.ClearSelection()
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim isCancel As Boolean = Convert.ToBoolean(row.Cells("CANCEL_REQUEST").Value)
                If isCancel Then
                    Try
                        Dim str As String = row.Cells("CANCEL_REASON").Value.ToString()
                    Catch ex As Exception
                        Call MsgBox("คุณยังกรอกเหตุผลไม่ครบ", 0, "พบข้อผิดพลาด")
                        Return
                    End Try
                End If
            Next

            If (MessageBox.Show("คุณต้องการที่จะยกเลิกเอกสารที่เลือกใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                For Each row As DataGridViewRow In DataGridView1.Rows
                    Dim isCancel As Boolean = Convert.ToBoolean(row.Cells("CANCEL_REQUEST").Value)
                    If isCancel Then
                        Dim query As String = "UPDATE PN_HEAD SET CANCEL_FLAG = @p0, CANCEL_REASON = @p1, CANCEL_BY = @p2 WHERE TRAN_NO = @p3"
                        Dim parameters As New Dictionary(Of String, Object)
                        Dim cancleReason As String = row.Cells("CANCEL_REASON").Value.ToString()
                        If TRAN_TYPE = "PN" Then
                            parameters.Add("@p0", "P")
                        Else
                            parameters.Add("@p0", "A")    
                        End If
                        parameters.Add("@p1", cancleReason)
                        parameters.Add("@p2", user_name)
                        parameters.Add("@p3", row.Cells("TRAN_NO").Value)
                        Try
                            executeWebSQL(query, parameters)
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                        End Try


                    End If
                Next

                btFind_Click(sender, e)
            End If
        End If
    End Sub


    Private Sub dataGridView1_CellClick(ByVal sender As Object, _
    ByVal e As DataGridViewCellEventArgs) _
    Handles DataGridView1.CellClick
        Try
            If e.RowIndex < 0 Or Not (e.ColumnIndex = DataGridView1.Columns("PREVIEW").Index Or e.ColumnIndex = DataGridView1.Columns("RESTORE").Index) Then Return
        Catch ex As Exception
            Return
        End Try

        Try
            If e.ColumnIndex = DataGridView1.Columns("RESTORE").Index Then
                If DataGridView1.Rows(e.RowIndex).Cells("CANCEL_FLAG").Value.ToString() = "A" Or String.IsNullOrEmpty(DataGridView1.Rows(e.RowIndex).Cells("CANCEL_FLAG").Value.ToString()) Then Return
            End If
        Catch ex As Exception

        End Try

        Dim TRAN_NO As String = DataGridView1.Rows(e.RowIndex).Cells("TRAN_NO").Value.ToString()
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String
        query = "UPDATE PN_HEAD SET CANCEL_FLAG = NULL , CANCEL_REASON = NULL WHERE TRAN_NO = '" & TRAN_NO & "'"

        parameters.Clear()

        Try
            If e.ColumnIndex = DataGridView1.Columns("RESTORE").Index Then
                If (MessageBox.Show("คุณต้องการที่จะถอนคำร้องยกเลิกเอกสารหมายเลข " & TRAN_NO & " ใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try
                    btFind_Click(sender, e)
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
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        RefTextBox.Text = ""
        FromDateTextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
        ToDateTextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
        DataGridView1.Columns.Clear()
    End Sub

    Private Sub FromDateTextBox_TypeValidationCompleted(sender As Object, e As TypeValidationEventArgs) Handles FromDateTextBox.TypeValidationCompleted

        Try
            Dim tempDate As Date = Date.ParseExact(FromDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
            If tempDate > Date.Now.AddYears(100) Or tempDate < Date.Now.AddYears(-100) Then
                Call MsgBox("ปีที่คุณกรอกอยู่นอกเหนือขอบเขตที่ระบบอนุญาติ (บวกลบ 100 ปี จากปีปัจจุบัน) กรุณากรอกใหม่อีกครั้ง", 0, "พบข้อผิดพลาด")
            End If

        Catch ex As FormatException
            Call MsgBox("คุณกรอกวันที่ผิดรูปแบบ (กรอกไม่ครบ, ตัวเลขเดือนผิด หรือ ไม่มีวันนั้นในเดือน) กรุณากรอกใหม่อีกครัง", 0, "พบข้อผิดพลาด")
            FromDateTextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
        End Try

    End Sub

    Private Sub ToDateTextBox_TypeValidationCompleted(sender As Object, e As TypeValidationEventArgs) Handles ToDateTextBox.TypeValidationCompleted

        Try
            Dim tempDate As Date = Date.ParseExact(ToDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
            If tempDate > Date.Now.AddYears(100) Or tempDate < Date.Now.AddYears(-100) Then
                Call MsgBox("ปีที่คุณกรอกอยู่นอกเหนือขอบเขตที่ระบบอนุญาติ (บวกลบ 100 ปี จากปีปัจจุบัน) กรุณากรอกใหม่อีกครั้ง", 0, "พบข้อผิดพลาด")
            End If

        Catch ex As FormatException
            Call MsgBox("คุณกรอกวันที่ผิดรูปแบบ (กรอกไม่ครบ, ตัวเลขเดือนผิด หรือ ไม่มีวันนั้นในเดือน) กรุณากรอกใหม่อีกครัง", 0, "พบข้อผิดพลาด")
            ToDateTextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
        End Try

    End Sub

    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        Me.Dispose()
    End Sub
End Class