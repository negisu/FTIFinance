Public Class frmFINEditList
    Public TRAN_TYPE As String
    Public Action As FORM_ACTION

    Private Sub frmFINPaymentNoticeSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Action = FORM_ACTION.Edit Then
            If TRAN_TYPE = "I1" Then
                Me.Text = "แก้ไขใบแจ้งหนี้"
                Label1.Text = "แก้ไขใบแจ้งหนี้"
                Me.BackColor = Color.LavenderBlush
                Panel10.BackColor = Color.Plum
            ElseIf TRAN_TYPE = "I2" Then
                Me.Text = "ออกใบลดยอดใบแจ้งหนี้"
                Label1.Text = "ออกใบลดยอดใบแจ้งหนี้"
                OK_Button.Text = "ออกใบลดยอดใบแจ้งหนี้"
                Me.BackColor = Color.Ivory
                Panel10.BackColor = Color.PaleGoldenrod
            ElseIf TRAN_TYPE = "R1" Then
                Me.Text = "แก้ไขใบเสร็จ"
                Label1.Text = "แก้ไขใบเสร็จ"
                Me.BackColor = Color.MistyRose
                Panel10.BackColor = Color.Salmon
            End If
        ElseIf Action = FORM_ACTION.SelectDoc Then
            If TRAN_TYPE = "%" Then
                Me.Text = "เลือกเอกสาร"
                Label1.Text = "เลือกเอกสาร"
                OK_Button.Text = "เลือก"
                DataGridView1.MultiSelect = True
            End If
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
        Dim query As String = "SELECT PN_HEAD.TRAN_NO,PN_HEAD.TRAN_DATE,SU_DIVISION.DIV_NAME,MB_COMP_PERSON.COMP_PERSON_NAME_TH,SUM(PN_DETAIL.BAL_AMT) as SUM_BAL_AMT,SUM(PN_DETAIL.PAY_AMT) as SUM_PAY_AMT ,SUM(PN_DETAIL.SUM_TOTAL) as SUM_SUM_TOTAL FROM PN_DETAIL "
        query &= "LEFT JOIN PN_HEAD ON PN_HEAD.TRAN_NO = PN_DETAIL.TRAN_NO "
        query &= "LEFT JOIN MB_COMP_PERSON ON PN_HEAD.AR_CODE = MB_COMP_PERSON.COMP_PERSON_CODE "
        query &= "LEFT JOIN SU_DIVISION ON PN_HEAD.DIV_CODE = SU_DIVISION.DIV_CODE WHERE "

        If Not PermissionHelper.isAdmin() Then
            query &= " PN_HEAD.DIV_CODE = @p9 "
            parameters.Add("@p9", user_div)
        End If

        query &= " AND PN_HEAD.CANCEL_FLAG IS NULL AND PN_HEAD.TRAN_TYPE LIKE @p10 "
        If TRAN_TYPE = "I2" Then
            parameters.Add("@p10", "I1")
        Else
            parameters.Add("@p10", TRAN_TYPE)
        End If

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

        query &= " GROUP BY PN_HEAD.TRAN_NO,PN_HEAD.TRAN_DATE, SU_DIVISION.DIV_NAME,MB_COMP_PERSON.COMP_PERSON_NAME_TH "
        query &= " ORDER BY TRAN_DATE "

        DataGridView1.DataSource = fillWebSQL(query, parameters, "PN_DETAIL")

        For i As Integer = 0 To DataGridView1.ColumnCount - 1
            With DataGridView1.Columns(i)
                .ReadOnly = True
                .Visible = False
            End With
        Next

        DataGridView1.Columns("DIV_NAME").HeaderText = "หน่วยงาน"
        DataGridView1.Columns("TRAN_DATE").HeaderText = "วันที่ทำรายการ"
        DataGridView1.Columns("TRAN_NO").HeaderText = "เลขที่เอกสาร"
        DataGridView1.Columns("COMP_PERSON_NAME_TH").HeaderText = "ชื่อผู้จ่ายเงิน"
        DataGridView1.Columns("SUM_SUM_TOTAL").HeaderText = "ยอดรวม"
        DataGridView1.Columns("SUM_PAY_AMT").HeaderText = "ยอดชำระ"
        DataGridView1.Columns("SUM_BAL_AMT").HeaderText = "ยอดค้างชำระ"

        DataGridView1.Columns("DIV_NAME").DisplayIndex = 3
        DataGridView1.Columns("TRAN_DATE").DisplayIndex = 0
        DataGridView1.Columns("TRAN_NO").DisplayIndex = 1
        DataGridView1.Columns("COMP_PERSON_NAME_TH").DisplayIndex = 2
        DataGridView1.Columns("SUM_SUM_TOTAL").DisplayIndex = 4
        DataGridView1.Columns("SUM_PAY_AMT").DisplayIndex = 5
        DataGridView1.Columns("SUM_BAL_AMT").DisplayIndex = 6

        DataGridView1.Columns("DIV_NAME").Visible = True
        DataGridView1.Columns("TRAN_DATE").Visible = True
        DataGridView1.Columns("TRAN_NO").Visible = True
        DataGridView1.Columns("COMP_PERSON_NAME_TH").Visible = True
        DataGridView1.Columns("SUM_SUM_TOTAL").Visible = True
        DataGridView1.Columns("SUM_PAY_AMT").Visible = True
        DataGridView1.Columns("SUM_BAL_AMT").Visible = True


        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.AutoResizeColumns()
        DataGridView1.Refresh()

    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            If Action = FORM_ACTION.SelectDoc Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                If (MessageBox.Show("คุณต้องการที่จะแก้ไขเอกสารหมายเลข " & DataGridView1.CurrentRow.Cells("TRAN_NO").Value.ToString() & " ใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Close()
                End If
            End If
            


        End If
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