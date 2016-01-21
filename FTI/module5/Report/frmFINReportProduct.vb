Public Class frmFINReportProduct

    Private Sub search_Click(sender As Object, e As EventArgs) Handles btFind.Click
        If (DocTypeComboBox.SelectedIndex = 0) Then
            searchPN()
        Else
            searchIV()
        End If
    End Sub

    Private Sub searchIV()

        Dim searchValue As String = KeyWordTextBox.Text.Trim.Replace(" ", "%")
        searchValue = "%" & searchValue & "%"
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = " SELECT TOP 1000 BG_SUB_SECTION.SUB_SECTION_CODE, BG_SUB_SECTION.SUB_SECTION_NAME, COUNT(BG_SUB_SECTION.SUB_SECTION_CODE) AS CNT , SUM (IV_DETAIL.BAL_AMT) AS BAL "
        query &= " FROM IV_DETAIL INNER JOIN BG_SUB_SECTION ON BG_SUB_SECTION.SUB_SECTION_CODE = IV_DETAIL.SUB_SECTION_CODE INNER JOIN  IV_HEAD ON IV_DETAIL.TRAN_NO = IV_HEAD.TRAN_NO"
        query &= " WHERE IV_DETAIL.BAL_AMT > 0 AND BG_SUB_SECTION.SUB_SECTION_NAME LIKE @p0 "

        If PermissionHelper.isAdmin() Then
        Else
            query &= " AND BG_SUB_SECTION.DIV_CODE_INC = @p1 "
            parameters.Add("@p1", user_div)
        End If
        

        If isFromDate.Checked Then
            If isToDate.Checked Then
                query &= " AND (IV_HEAD.TRAN_DATE BETWEEN @p2 AND @p3) "
                parameters.Add("@p2", Date.ParseExact(FromDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat))
                parameters.Add("@p3", Date.ParseExact(ToDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat))
            Else
                query &= " AND IV_HEAD.TRAN_DATE >= @p4 "
                parameters.Add("@p4", Date.ParseExact(FromDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat))
            End If
        Else
            If isToDate.Checked Then
                query &= " AND IV_HEAD.TRAN_DATE <= @p5 "
                parameters.Add("@p5", Date.ParseExact(ToDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat))
            Else

            End If
        End If

        query &= " GROUP BY BG_SUB_SECTION.SUB_SECTION_CODE, BG_SUB_SECTION.SUB_SECTION_NAME "

        If SortColumnComboBox.SelectedIndex = 0 Then
            query &= " ORDER BY SUB_SECTION_CODE "
        ElseIf SortColumnComboBox.SelectedIndex = 1 Then
            query &= " ORDER BY SUB_SECTION_NAME "
        Else
            query &= " ORDER BY CNT "
        End If

        If SortDirectionComboBox.SelectedIndex = 0 Then

        ElseIf SortDirectionComboBox.SelectedIndex = 1 Then
            query &= " ASC "
        Else
            query &= " DESC "
        End If

        parameters.Add("@p0", searchValue)

        DataGridView1.DataSource = fillWebSQL(query, parameters, "BG_SUB_SECTION")
        DataGridView1.Columns("SUB_SECTION_CODE").HeaderText = "รหัสรายการ"
        DataGridView1.Columns("SUB_SECTION_NAME").HeaderText = "ชื่อรายการ"
        DataGridView1.Columns("CNT").HeaderText = "จำนวนรายการคงค้าง"
        DataGridView1.Columns("BAL").HeaderText = "ยอดคงค้าง"
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.AutoResizeColumns()
        DataGridView1.Refresh()


    End Sub


    Private Sub searchPN()

        Dim searchValue As String = KeyWordTextBox.Text.Trim.Replace(" ", "%")
        searchValue = "%" & searchValue & "%"
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = " SELECT TOP 1000 BG_SUB_SECTION.SUB_SECTION_CODE, BG_SUB_SECTION.SUB_SECTION_NAME, COUNT(BG_SUB_SECTION.SUB_SECTION_CODE) AS CNT "
        query &= " FROM PN_DETAIL INNER JOIN BG_SUB_SECTION ON BG_SUB_SECTION.SUB_SECTION_CODE = PN_DETAIL.SUB_SECTION_CODE INNER JOIN  PN_HEAD ON PN_DETAIL.TRAN_NO = PN_HEAD.TRAN_NO"
        query &= " WHERE PN_DETAIL.BAL_AMT > 0 AND BG_SUB_SECTION.SUB_SECTION_NAME LIKE @p0 "

        If PermissionHelper.isAdmin() Then
        Else
            query &= " AND BG_SUB_SECTION.DIV_CODE_INC = @p1 "
            parameters.Add("@p1", user_div)

        End If

        If isFromDate.Checked Then
            If isToDate.Checked Then
                query &= " AND (PN_HEAD.TRAN_DATE BETWEEN @p2 AND @p3) "
                parameters.Add("@p2", Date.ParseExact(FromDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat))
                parameters.Add("@p3", Date.ParseExact(ToDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat))
            Else
                query &= " AND PN_HEAD.TRAN_DATE >= @p4 "
                parameters.Add("@p4", Date.ParseExact(FromDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat))
            End If
        Else
            If isToDate.Checked Then
                query &= " AND PN_HEAD.TRAN_DATE <= @p5 "
                parameters.Add("@p5", Date.ParseExact(ToDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat))
            Else

            End If
        End If

        query &= " GROUP BY BG_SUB_SECTION.SUB_SECTION_CODE, BG_SUB_SECTION.SUB_SECTION_NAME "

        If SortColumnComboBox.SelectedIndex = 0 Then
            query &= " ORDER BY SUB_SECTION_CODE "
        ElseIf SortColumnComboBox.SelectedIndex = 1 Then
            query &= " ORDER BY SUB_SECTION_NAME "
        Else
            query &= " ORDER BY CNT "
        End If

        If SortDirectionComboBox.SelectedIndex = 0 Then

        ElseIf SortDirectionComboBox.SelectedIndex = 1 Then
            query &= " ASC "
        Else
            query &= " DESC "
        End If

        parameters.Add("@p0", searchValue)
        DataGridView1.DataSource = fillWebSQL(query, parameters, "BG_SUB_SECTION")
        DataGridView1.Columns("SUB_SECTION_CODE").HeaderText = "รหัสรายการ"
        DataGridView1.Columns("SUB_SECTION_NAME").HeaderText = "ชื่อรายการ"
        DataGridView1.Columns("CNT").HeaderText = "จำนวนรายการคงค้าง"
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.AutoResizeColumns()
        DataGridView1.Refresh()


    End Sub

    Private Sub frmFINReportProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SortColumnComboBox.SelectedIndex = 0
        SortDirectionComboBox.SelectedIndex = 0
        DocTypeComboBox.SelectedIndex = 0
    End Sub

    Private Sub isFromDate_CheckedChanged(sender As Object, e As EventArgs) Handles isFromDate.CheckedChanged
        FromDateTextBox.Enabled = isFromDate.Checked
        If isFromDate.Checked Then
            FromDateTextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
        End If
    End Sub

    Private Sub isToDate_CheckedChanged(sender As Object, e As EventArgs) Handles isToDate.CheckedChanged
        ToDateTextBox.Enabled = isToDate.Checked
        If isToDate.Checked Then
            ToDateTextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
        End If
    End Sub

    Private Sub TodayFromButton_Click(sender As Object, e As EventArgs) Handles TodayFromButton.Click
        FromDateTextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
    End Sub

    Private Sub TodayToButton_Click(sender As Object, e As EventArgs) Handles TodayToButton.Click
        ToDateTextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim param As New Dictionary(Of String, String)
        param.Add("SUB_SECTION_CODE", DataGridView1.CurrentRow.Cells("SUB_SECTION_CODE").Value.ToString)
        If isFromDate.Checked Then
            param.Add("FROM_DATE", Date.ParseExact(FromDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat).ToString("MM/dd/yyyy"))
        Else
            param.Add("FROM_DATE", "01/01/1753")
        End If
        If isToDate.Checked Then
            param.Add("TO_DATE", Date.ParseExact(ToDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat).ToString("MM/dd/yyyy"))
        Else
            param.Add("TO_DATE", "12/31/9456")
        End If

        If PermissionHelper.isAdmin() Then
            param.Add("DIV_CODE", "%")
        Else
            param.Add("DIV_CODE", user_div)
        End If
        param.Add("USER_NAME", user_name)
        param.Add("PROD_NAME", DataGridView1.CurrentRow.Cells("SUB_SECTION_NAME").Value.ToString)
        Dim f As New frmMainReports
        f.reportPath = getParameters(5, "PN_SUB_SECTION_CODE")
        f.reportParameters = param
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub FromDateTextBox_TypeValidationCompleted(sender As Object, e As TypeValidationEventArgs) Handles FromDateTextBox.TypeValidationCompleted
        If e.IsValidInput Then
            Try
                Dim tempDate As Date = Date.ParseExact(FromDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
                If tempDate > Date.Now.AddYears(100) Or tempDate < Date.Now.AddYears(-100) Then
                    Call MsgBox("ปีที่คุณกรอกอยู่นอกเหนือขอบเขตที่ระบบอนุญาติ (บวกลบ 100 ปี จากปีปัจจุบัน) กรุณากรอกใหม่อีกครั้ง", 0, "พบข้อผิดพลาด")
                    FromDateTextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
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
                    ToDateTextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim param As New Dictionary(Of String, String)
        If isFromDate.Checked Then
            param.Add("FROM_DATE", Date.ParseExact(FromDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat).ToString("MM/dd/yyyy"))
        Else
            param.Add("FROM_DATE", "01/01/1753")
        End If
        If isToDate.Checked Then
            param.Add("TO_DATE", Date.ParseExact(ToDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat).ToString("MM/dd/yyyy"))
        Else
            param.Add("TO_DATE", "12/31/9456")
        End If

        If PermissionHelper.isAdmin() Then
            param.Add("DIV_CODE", "%")
        Else
            param.Add("DIV_CODE", user_div)
        End If

        param.Add("USER_NAME", user_name)

        Dim f As New frmMainReports
        f.reportPath = getParameters(5, "PN_SUB_SECTION_CODE_OVERALL")
        f.reportParameters = param
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        Me.Dispose()
    End Sub
End Class