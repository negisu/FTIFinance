Public Class frmFINReportAR

    Private Sub search_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
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

        Dim query As String = " SELECT AR_MEMBER2.AR_CODE, AR_MEMBER2.FULL_NAME, COUNT(AR_MEMBER2.AR_CODE) AS CNT, SUM (IV_DETAIL.BAL_AMT) AS BAL "
        query &= " FROM IV_DETAIL INNER JOIN IV_HEAD ON IV_DETAIL.TRAN_NO = IV_HEAD.TRAN_NO INNER JOIN AR_MEMBER2 ON IV_HEAD.AR_CODE = AR_MEMBER2.AR_CODE "
        query &= " WHERE IV_DETAIL.BAL_AMT > 0 AND AR_MEMBER2.FULL_NAME LIKE @p0 "



        If user_div = "AAA-AA" Then
        Else
            query &= " AND IV_HEAD.DIV_CODE = @p1 "
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

        query &= " GROUP BY AR_MEMBER2.FULL_NAME, AR_MEMBER2.AR_CODE "

        If SortColumnComboBox.SelectedIndex = 0 Then
            query &= " ORDER BY AR_MEMBER2.AR_CODE "
        ElseIf SortColumnComboBox.SelectedIndex = 1 Then
            query &= " ORDER BY AR_MEMBER2.FULL_NAME "
        ElseIf SortColumnComboBox.SelectedIndex = 2 Then
            query &= " ORDER BY CNT "
        Else
            query &= " ORDER BY BAL "
        End If

        If SortDirectionComboBox.SelectedIndex = 0 Then

        ElseIf SortDirectionComboBox.SelectedIndex = 1 Then
            query &= " ASC "
        Else
            query &= " DESC "
        End If

        parameters.Add("@p0", searchValue)

        DataGridView1.DataSource = fillWebSQL(query, parameters, "IV_DETAIL")
        DataGridView1.Columns("AR_CODE").HeaderText = "รหัสผู้จ่ายตัง"
        DataGridView1.Columns("FULL_NAME").HeaderText = "ชื่อผู้จ่ายตัง"
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

        Dim query As String = " SELECT AR_MEMBER2.AR_CODE, AR_MEMBER2.FULL_NAME, COUNT(AR_MEMBER2.AR_CODE) AS CNT, SUM (PN_DETAIL.BAL_AMT) AS BAL "
        query &= " FROM PN_DETAIL INNER JOIN PN_HEAD ON PN_DETAIL.TRAN_NO = PN_HEAD.TRAN_NO INNER JOIN AR_MEMBER2 ON PN_HEAD.AR_CODE = AR_MEMBER2.AR_CODE "
        query &= " WHERE PN_DETAIL.BAL_AMT > 0 AND AR_MEMBER2.FULL_NAME LIKE @p0 "



        If user_div = "AAA-AA" Then
        Else
            query &= " AND PN_HEAD.DIV_CODE = @p1 "
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

        query &= " GROUP BY AR_MEMBER2.FULL_NAME, AR_MEMBER2.AR_CODE "

        If SortColumnComboBox.SelectedIndex = 0 Then
            query &= " ORDER BY AR_MEMBER2.AR_CODE "
        ElseIf SortColumnComboBox.SelectedIndex = 1 Then
            query &= " ORDER BY AR_MEMBER2.FULL_NAME "
        ElseIf SortColumnComboBox.SelectedIndex = 2 Then
            query &= " ORDER BY CNT "
        Else
            query &= " ORDER BY BAL "
        End If

        If SortDirectionComboBox.SelectedIndex = 0 Then

        ElseIf SortDirectionComboBox.SelectedIndex = 1 Then
            query &= " ASC "
        Else
            query &= " DESC "
        End If

        parameters.Add("@p0", searchValue)

        DataGridView1.DataSource = fillWebSQL(query, parameters, "PN_DETAIL")
        DataGridView1.Columns("AR_CODE").HeaderText = "รหัสผู้จ่ายตัง"
        DataGridView1.Columns("FULL_NAME").HeaderText = "ชื่อผู้จ่ายตัง"
        DataGridView1.Columns("CNT").HeaderText = "จำนวนรายการคงค้าง"
        DataGridView1.Columns("BAL").HeaderText = "ยอดคงค้าง"
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
    End Sub

    Private Sub isToDate_CheckedChanged(sender As Object, e As EventArgs) Handles isToDate.CheckedChanged
        ToDateTextBox.Enabled = isToDate.Checked
    End Sub

    Private Sub TodayFromButton_Click(sender As Object, e As EventArgs) Handles TodayFromButton.Click
        FromDateTextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
    End Sub

    Private Sub TodayToButton_Click(sender As Object, e As EventArgs) Handles TodayToButton.Click
        ToDateTextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim param As New Dictionary(Of String, String)
        param.Add("AR_CODE", DataGridView1.CurrentRow.Cells("AR_CODE").Value.ToString)
        If isFromDate.Checked Then
            param.Add("START_DATE", Date.ParseExact(FromDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat).ToString)
        End If
        If isToDate.Checked Then
            param.Add("END_DATE", Date.ParseExact(ToDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat).ToString)
        End If
        Dim f As New frmMainReports
        f.reportPath = getParameters(5, "AR_REMAIN_REPORT")
        f.reportParameters = param
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim param As New Dictionary(Of String, String)
        param.Add("AR_CODE", DataGridView1.CurrentRow.Cells("AR_CODE").Value.ToString)
        If isFromDate.Checked Then
            param.Add("START_DATE", Date.ParseExact(FromDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat).ToString)
        End If
        If isToDate.Checked Then
            param.Add("END_DATE", Date.ParseExact(ToDateTextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat).ToString)
        End If
        Dim f As New frmMainReports
        f.reportPath = getParameters(5, "AR_REMAIN_REPORT")
        f.reportParameters = param
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub
End Class