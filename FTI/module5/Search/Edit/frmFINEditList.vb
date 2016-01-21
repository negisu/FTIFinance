Public Class frmFINEditList
    Public TRAN_TYPE As String

    Private Sub frmFINPaymentNoticeSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If TRAN_TYPE = "IV" Then
            Me.Text = "แก้ไขใบแจ้งหนี้"
            Label1.Text = "แก้ไขใบแจ้งหนี้"
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
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = "SELECT TOP 100 * FROM PN_HEAD LEFT JOIN AR_MEMBER2 ON PN_HEAD.AR_CODE = AR_MEMBER2.AR_CODE LEFT JOIN SU_DIVISION ON PN_HEAD.DIV_CODE = SU_DIVISION.DIV_CODE WHERE "

        If PermissionHelper.isAdmin() Then

        Else
            query &= " PN_HEAD.DIV_CODE = @p9 "
            parameters.Add("@p9", user_div)
        End If

        query &= " AND CANCEL_FLAG IS NULL AND TRAN_TYPE = @p10 "
        parameters.Add("@p10", TRAN_TYPE)
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

        DataGridView1.Columns("DIV_NAME").HeaderText = "หน่วยงาน"
        DataGridView1.Columns("TRAN_DATE").HeaderText = "วันที่ทำรายการ"
        DataGridView1.Columns("TRAN_NO").HeaderText = "เลขที่เอกสาร"
        DataGridView1.Columns("FULL_NAME").HeaderText = "ชื่อผู้จ่ายเงิน"
        DataGridView1.Columns("BAL_AMT").HeaderText = "ยอดค้างชำระ"
        DataGridView1.Columns("NOTE").HeaderText = "หมายเหตุ"

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
            If (MessageBox.Show("คุณต้องการที่จะแก้ไขเอกสารหมายเลข " & DataGridView1.CurrentRow.Cells("TRAN_NO").Value.ToString() & " ใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            End If

        End If
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
End Class