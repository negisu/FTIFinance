﻿



Public Class frmFINPaymentNoticeSearch
    Private Sub AR_NAMETextBox_DoubleClick(sender As Object, e As EventArgs) Handles AR_NAMETextBox.DoubleClick
        Dim f As New frmFINAR_MEMBER
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Dim selectedRow As DataGridViewRow = f.DataGridView1.CurrentRow
            AR_CODETextBox.Text = selectedRow.Cells("AR_CODE").Value.ToString()
            AR_NAMETextBox.Text = selectedRow.Cells("FULL_NAME").Value.ToString()
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub frmFINPaymentNoticeSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        ProcessComboBox.SelectedIndex = 0
        RefTextBox.Enabled = False
        FromDateTextBox.Enabled = False
        ToDateTextBox.Enabled = False

        DIV_NAMEComboBox.Enabled = False
        ATV_NAMEComboBox.Enabled = False
        AR_NAMETextBox.Enabled = False


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

    Private Sub isDiv_CheckedChanged(sender As Object, e As EventArgs) Handles isDiv.CheckedChanged
        DIV_NAMEComboBox.Enabled = isDiv.Checked
    End Sub

    Private Sub isATV_CheckedChanged(sender As Object, e As EventArgs) Handles isATV.CheckedChanged
        ATV_NAMEComboBox.Enabled = isATV.Checked
    End Sub

    Private Sub isAR_CheckedChanged(sender As Object, e As EventArgs) Handles isAR.CheckedChanged
        AR_NAMETextBox.Enabled = isAR.Checked
    End Sub


    Private Sub btFind_Click(sender As Object, e As EventArgs) Handles btFind.Click
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = "SELECT TOP 100 * FROM PN_HEAD LEFT JOIN AR_MEMBER2 ON PN_HEAD.AR_CODE = AR_MEMBER2.AR_CODE LEFT JOIN SU_DIVISION ON PN_HEAD.DIV_CODE = SU_DIVISION.DIV_CODE WHERE "

        If user_div = "AAA-AA" Then

        Else
            query &= "DIV_CODE = @p9 "
            parameters.Add("@p9", user_div)
        End If

        If isRef.Checked And Not String.IsNullOrEmpty(RefTextBox.Text) Then
            Dim searchValue As String = RefTextBox.Text.Trim.Replace(" ", "%")
            searchValue = "%" & searchValue & "%"
            query &= " AND TRAN_NO LIKE @p0 "
            parameters.Add("@p0", searchValue)

        End If
        If isFromDate.Checked And Not String.IsNullOrEmpty(FromDateTextBox.Text) Then
            query &= " AND TRAN_DATE >=  @p1"
            parameters.Add("@p1", Date.ParseExact(FromDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat))
        End If
        If isToDate.Checked And Not String.IsNullOrEmpty(ToDateTextBox.Text) Then
            query &= " AND TRAN_DATE <=  @p2"
            parameters.Add("@p2", Date.ParseExact(ToDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat))
        End If
        If isDiv.Checked Then
            query &= " AND DIV_CODE = @p3 "
            parameters.Add("@p3", DIV_NAMEComboBox.SelectedValue)
        End If

        If isATV.Checked Then
            query &= " AND ATV_CODE = @p4 "
            parameters.Add("@p4", DIV_NAMEComboBox.SelectedValue)
        End If
        If isAR.Checked And Not String.IsNullOrEmpty(AR_NAMETextBox.Text) Then
            query &= " AND AR_CODE = @p5 "
            parameters.Add("@p5", AR_CODETextBox.Text)
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

        DataGridView1.Columns("DIV_NAME").HeaderText = "หน่วยงาน"
        DataGridView1.Columns("TRAN_DATE").HeaderText = "วันที่ทำรายการ"
        DataGridView1.Columns("TRAN_NO").HeaderText = "เลขที่เอกสาร"
        DataGridView1.Columns("FULL_NAME").HeaderText = "ชื่อผู้จ่ายเงิน"
        DataGridView1.Columns("BAL_AMT").HeaderText = "ยอดค้างชำระ"
        DataGridView1.Columns("NOTE").HeaderText = "บันทึก"

        DataGridView1.Columns("DIV_NAME").DisplayIndex = 3
        DataGridView1.Columns("TRAN_DATE").DisplayIndex = 0
        DataGridView1.Columns("TRAN_NO").DisplayIndex = 1
        DataGridView1.Columns("FULL_NAME").DisplayIndex = 2
        DataGridView1.Columns("BAL_AMT").DisplayIndex = 4
        DataGridView1.Columns("NOTE").DisplayIndex = 5

        DataGridView1.Columns("DIV_NAME").Visible = True
        DataGridView1.Columns("TRAN_DATE").Visible = True
        DataGridView1.Columns("TRAN_NO").Visible = True
        DataGridView1.Columns("FULL_NAME").Visible = True
        DataGridView1.Columns("BAL_AMT").Visible = True
        DataGridView1.Columns("NOTE").Visible = True

        DataGridView1.Columns("DIV_NAME").ReadOnly = True
        DataGridView1.Columns("TRAN_DATE").ReadOnly = True
        DataGridView1.Columns("TRAN_NO").ReadOnly = True
        DataGridView1.Columns("FULL_NAME").ReadOnly = True
        DataGridView1.Columns("BAL_AMT").ReadOnly = True
        DataGridView1.Columns("NOTE").ReadOnly = True

        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.AutoResizeColumns()
        DataGridView1.Refresh()

    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            If (MessageBox.Show("คุณต้องการที่จะ " & ProcessComboBox.Text & " ใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            End If

        End If
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        RefTextBox.Text = ""
        FromDateTextBox.Clear()
        ToDateTextBox.Clear()
        DataGridView1.Columns.Clear()
    End Sub
    Private Sub FromDateTextBox_TypeValidationCompleted(sender As Object, e As TypeValidationEventArgs) Handles FromDateTextBox.TypeValidationCompleted
        If e.IsValidInput Then
            Try
                Dim tempDate As Date = Date.ParseExact(FromDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
                If tempDate > Date.Now.AddYears(100) Or tempDate < Date.Now.AddYears(-100) Then
                    Call MsgBox("ปีที่คุณกรอกอยู่นอกเหนือขอบเขตที่ระบบอนุญาติ (บวกลบ 100 ปี จากปีปัจจุบัน) กรุณากรอกใหม่อีกครั้ง", 0, "พบข้อผิดพลาด")
                End If

            Catch ex As FormatException
                Call MsgBox("วันที่คุณกรอกไม่มีในเดือนนั้น กรุณากรอกใหม่อีกครั้ง", 0, "พบข้อผิดพลาด")
                FromDateTextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
            End Try
        Else
            Call MsgBox("คุณกรอกวันที่ผิดรูปแบบ (กรอกไม่ครบ, ตัวเลขเดือนผิด หรือ ไม่มีวันนั้นในเดือน) กรุณากรอกใหม่อีกครัง", 0, "พบข้อผิดพลาด")
            FromDateTextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)

        End If
    End Sub

    Private Sub ToDateTextBox_TypeValidationCompleted(sender As Object, e As TypeValidationEventArgs) Handles ToDateTextBox.TypeValidationCompleted
        If e.IsValidInput Then
            Try
                Dim tempDate As Date = Date.ParseExact(ToDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
                If tempDate > Date.Now.AddYears(100) Or tempDate < Date.Now.AddYears(-100) Then
                    Call MsgBox("ปีที่คุณกรอกอยู่นอกเหนือขอบเขตที่ระบบอนุญาติ (บวกลบ 100 ปี จากปีปัจจุบัน) กรุณากรอกใหม่อีกครั้ง", 0, "พบข้อผิดพลาด")
                End If

            Catch ex As FormatException
                Call MsgBox("วันที่คุณกรอกไม่มีในเดือนนั้น กรุณากรอกใหม่อีกครั้ง", 0, "พบข้อผิดพลาด")
                ToDateTextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
            End Try
        Else
            Call MsgBox("คุณกรอกวันที่ผิดรูปแบบ (กรอกไม่ครบ, ตัวเลขเดือนผิด หรือ ไม่มีวันนั้นในเดือน) กรุณากรอกใหม่อีกครัง", 0, "พบข้อผิดพลาด")
            ToDateTextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)

        End If
    End Sub
End Class