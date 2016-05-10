Public Class frmFINReportProduct
    Private Sub search_Click(sender As Object, e As EventArgs) Handles btFind.Click
        Try
            DataGridView2.DataSource = Nothing
            DataGridView2.Refresh()
        Catch ex As Exception

        End Try
        search()
        Label5.Text = DataGridView1.Rows.Count.ToString()
    End Sub


    Private Sub search()

        Dim searchValue As String = KeyWordTextBox.Text.Trim.Replace(" ", "%")
        searchValue = "%" & searchValue & "%"
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = " SELECT TOP 1000 IV_SUB_SECTION.SUB_SECTION_CODE, IV_SUB_SECTION.SUB_SECTION_NAME, COUNT(IV_SUB_SECTION.SUB_SECTION_CODE) AS CNT  "
        If IsAR.Checked Then
            query = " SELECT TOP 1000 MB_COMP_PERSON.COMP_PERSON_NAME_TH, PN_HEAD.MB_MEMBER_CODE, COUNT(IV_SUB_SECTION.SUB_SECTION_CODE) AS CNT "
        End If
        If isDoc.Checked Then
            query = " SELECT TOP 1000 PN_HEAD.TRAN_NO,PN_HEAD.TRAN_DATE ,SU_DIVISION.DIV_NAME ,MB_COMP_PERSON.COMP_PERSON_NAME_TH , COUNT(IV_SUB_SECTION.SUB_SECTION_CODE) AS CNT ,SUM(PN_DETAIL.BAL_AMT) as SUM_BAL_AMT,SUM(PN_DETAIL.PAY_AMT) as SUM_PAY_AMT ,SUM(PN_DETAIL.SUM_TOTAL) as SUM_SUM_TOTAL "
        End If




        query &= " FROM PN_DETAIL INNER JOIN IV_SUB_SECTION ON IV_SUB_SECTION.SUB_SECTION_CODE = PN_DETAIL.SUB_SECTION_CODE INNER JOIN  PN_HEAD ON PN_DETAIL.TRAN_NO = PN_HEAD.TRAN_NO "
        query &= " LEFT JOIN MB_COMP_PERSON ON PN_HEAD.AR_CODE = MB_COMP_PERSON.COMP_PERSON_CODE "
        query &= " LEFT JOIN SU_DIVISION ON PN_HEAD.DIV_CODE = SU_DIVISION.DIV_CODE "

        If IsAR.Checked Then
            query &= " WHERE (PN_HEAD.MB_MEMBER_CODE LIKE @p0 "
            query &= " OR MB_COMP_PERSON.COMP_PERSON_NAME_TH LIKE @p0 "
            query &= " OR MB_COMP_PERSON.COMP_PERSON_NAME_EN LIKE @p0 "
            query &= " OR MB_COMP_PERSON.COMP_PERSON_CODE LIKE @p0) "
        Else
            query &= " WHERE IV_SUB_SECTION.SUB_SECTION_NAME LIKE @p0 "
        End If


        parameters.Add("@p0", searchValue)

        addFilterQuery(parameters, query)


        If isSubSection.Checked Then
            query &= " GROUP BY IV_SUB_SECTION.SUB_SECTION_CODE, IV_SUB_SECTION.SUB_SECTION_NAME "
            DataGridView1.DataSource = fillWebSQL(query, parameters, "PN_DETAIL")
            DataGridView1.Columns("SUB_SECTION_CODE").HeaderText = "รหัสรายการ"
            DataGridView1.Columns("SUB_SECTION_NAME").HeaderText = "ชื่อรายการ"
        Else
            
        End If
        If IsAR.Checked Then
            query &= " GROUP BY MB_COMP_PERSON.COMP_PERSON_NAME_TH, PN_HEAD.MB_MEMBER_CODE"
            DataGridView1.DataSource = fillWebSQL(query, parameters, "PN_DETAIL")
            DataGridView1.Columns("COMP_PERSON_NAME_TH").HeaderText = "ชื่อลูกหนี้"
            DataGridView1.Columns("MB_MEMBER_CODE").HeaderText = "เลขสมาชิก"
        End If
        If isDoc.Checked Then
            query &= " GROUP BY PN_HEAD.TRAN_NO,PN_HEAD.TRAN_DATE, SU_DIVISION.DIV_NAME,MB_COMP_PERSON.COMP_PERSON_NAME_TH "
            DataGridView1.DataSource = fillWebSQL(query, parameters, "PN_DETAIL")
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
        End If

        DataGridView1.Columns("CNT").HeaderText = "จำนวนรายการ"
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.AutoResizeColumns()
        DataGridView1.Refresh()


    End Sub

    Private Sub frmFINReportProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TO_DATEPicker.MinDate = FROM_DATEPicker.Value
        FROM_DATEPicker.Value = New Date(FROM_DATEPicker.Value.Year, FROM_DATEPicker.Value.Month, FROM_DATEPicker.Value.Day, 0, 0, 0)
        TO_DATEPicker.Value = New Date(TO_DATEPicker.Value.Year, TO_DATEPicker.Value.Month, TO_DATEPicker.Value.Day, 23, 59, 59)
        TRAN_TYPEComboBox.SelectedIndex = 0
        DOC_STATUSComboBox.SelectedIndex = 0
        PAY_STATUSComboBox.SelectedIndex = 0
        getSU_DIVISION()
        DIV_NAMEComboBox.SelectedIndex = 0

        If Not PermissionHelper.isAdmin() Then
            DIVGroupBox.Visible = False
        End If
    End Sub

    Private Sub TRAN_DATEPicker_ValueChanged(sender As Object, e As EventArgs) Handles FROM_DATEPicker.ValueChanged
        If Not FROM_DATEPicker.IsHandleCreated Then
            Return
        Else
            FROM_DATEPicker.Value = New Date(FROM_DATEPicker.Value.Year, FROM_DATEPicker.Value.Month, FROM_DATEPicker.Value.Day, 0, 0, 0)
        End If
        TO_DATEPicker.MinDate = FROM_DATEPicker.Value
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged

        If DataGridView1.SelectedRows.Count = 0 Then Return
        Dim SEARCH_CODE_LIST As New List(Of String)

        For Each item As DataGridViewRow In DataGridView1.SelectedRows
            If isSubSection.Checked Then
                SEARCH_CODE_LIST.Add("'" & item.Cells("SUB_SECTION_CODE").Value.ToString() & "'")
            ElseIf IsAR.Checked Then
                SEARCH_CODE_LIST.Add("'" & item.Cells("MB_MEMBER_CODE").Value.ToString() & "'")
            Else
                SEARCH_CODE_LIST.Add("'" & item.Cells("TRAN_NO").Value.ToString() & "'")
            End If
        Next

        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = " SELECT PN_HEAD.TRAN_NO, PN_HEAD.TRAN_DATE, PN_DETAIL.BAL_AMT, MB_COMP_PERSON.COMP_PERSON_NAME_TH, PN_DETAIL.NOTE "
        query &= " FROM PN_DETAIL INNER JOIN IV_SUB_SECTION ON IV_SUB_SECTION.SUB_SECTION_CODE = PN_DETAIL.SUB_SECTION_CODE INNER JOIN  PN_HEAD ON PN_DETAIL.TRAN_NO = PN_HEAD.TRAN_NO LEFT JOIN MB_COMP_PERSON ON PN_HEAD.AR_CODE = MB_COMP_PERSON.COMP_PERSON_CODE LEFT JOIN MB_PRENAME ON MB_COMP_PERSON.PREN_CODE = MB_PRENAME.PRENAME_CODE "
        If isSubSection.Checked Then
            query &= " WHERE IV_SUB_SECTION.SUB_SECTION_CODE IN (" & String.Join(",", SEARCH_CODE_LIST) & ") "
        ElseIf IsAR.Checked Then
            query &= " WHERE PN_HEAD.MB_MEMBER_CODE IN (" & String.Join(",", SEARCH_CODE_LIST) & ") "
        Else
            query &= " WHERE PN_HEAD.TRAN_NO IN (" & String.Join(",", SEARCH_CODE_LIST) & ") "
        End If

        addFilterQuery(parameters, query)

        query &= " ORDER BY IV_SUB_SECTION.SUB_SECTION_CODE "


        DataGridView2.DataSource = fillWebSQL(query, parameters, "IV_SUB_SECTION")

        For i As Integer = 0 To DataGridView2.ColumnCount - 1
            DataGridView2.Columns(i).ReadOnly = True
        Next
        DataGridView2.Columns("TRAN_DATE").HeaderText = "วันที่ทำรายการ"
        DataGridView2.Columns("TRAN_NO").HeaderText = "เลขที่เอกสาร"
        DataGridView2.Columns("BAL_AMT").HeaderText = "ยอดค้างชำระ"
        DataGridView2.Columns("COMP_PERSON_NAME_TH").HeaderText = "ชื่อลูกหนี้"
        DataGridView2.Columns("NOTE").HeaderText = "ชื่อรายการ"

        DataGridView2.Columns("TRAN_DATE").DisplayIndex = 1
        DataGridView2.Columns("TRAN_NO").DisplayIndex = 2
        DataGridView2.Columns("BAL_AMT").DisplayIndex = 3
        DataGridView2.Columns("COMP_PERSON_NAME_TH").DisplayIndex = 4
        DataGridView2.Columns("NOTE").DisplayIndex = 0


        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView2.AutoResizeColumns()
        DataGridView2.Refresh()
    End Sub
    Private Sub addFilterQuery(ByRef parameters As Dictionary(Of String, Object), ByRef query As String)
        If PermissionHelper.isAdmin() Then
            If IsDIV.Checked Then
                query &= " AND IV_SUB_SECTION.DIV_CODE_INC = @p1 "
                parameters.Add("@p1", DIV_CODETextBox.Text)
            End If
        Else
            query &= " AND IV_SUB_SECTION.DIV_CODE_INC = @p1 "
            parameters.Add("@p1", user_div)

        End If

        query &= " AND (PN_HEAD.TRAN_DATE BETWEEN @p2 AND @p3) "
        parameters.Add("@p2", FROM_DATEPicker.Value)
        parameters.Add("@p3", TO_DATEPicker.Value)

        If Not TRAN_TYPEComboBox.SelectedIndex = 0 Then
            If TRAN_TYPEComboBox.SelectedIndex = 1 Then
                query &= " AND (PN_HEAD.TRAN_TYPE = 'P1') "
            ElseIf TRAN_TYPEComboBox.SelectedIndex = 2 Then
                query &= " AND (PN_HEAD.TRAN_TYPE = 'I1') "
            ElseIf TRAN_TYPEComboBox.SelectedIndex = 3 Then
                query &= " AND (PN_HEAD.TRAN_TYPE = 'R1') "
            End If
        End If
        'query &= " AND COMPLETE_FLAG IS NULL "
        If Not DOC_STATUSComboBox.SelectedIndex = 0 Then
            If DOC_STATUSComboBox.SelectedIndex = 1 Then
                query &= " AND CANCEL_FLAG = 'N' "
            ElseIf DOC_STATUSComboBox.SelectedIndex = 2 Then
                query &= " AND CANCEL_FLAG = 'A' "
            End If
        End If

        If Not PAY_STATUSComboBox.SelectedIndex = 0 Then
            If PAY_STATUSComboBox.SelectedIndex = 1 Then
                query &= " AND BAL_AMT > 0 "
            ElseIf PAY_STATUSComboBox.SelectedIndex = 2 Then
                query &= " AND BAL_AMT = 0 "
            End If
        End If


    End Sub
    Private Sub PreviewReportButton_Click(sender As Object, e As EventArgs) Handles PreviewReportButton.Click
        If DataGridView1.SelectedRows.Count = 0 Then Return
        Dim param As New Dictionary(Of String, String)

        param.Add("FROM_DATE", FROM_DATEPicker.Value.ToString(New System.Globalization.CultureInfo("en-US").DateTimeFormat))
        param.Add("TO_DATE", TO_DATEPicker.Value.ToString(New System.Globalization.CultureInfo("en-US").DateTimeFormat))
        Dim SEARCH_CODE_LIST As New List(Of String)

        If PermissionHelper.isAdmin() Then
            param.Add("DIV_CODE", "%")
            If IsDIV.Checked Then
                param.Add("DIV_CODE", DIV_CODETextBox.Text)
            End If
        Else
            param.Add("DIV_CODE", user_div)
        End If

        If Not TRAN_TYPEComboBox.SelectedIndex = 0 Then
            If TRAN_TYPEComboBox.SelectedIndex = 1 Then
                param.Add("TRAN_TYPE", "P1")
            ElseIf TRAN_TYPEComboBox.SelectedIndex = 2 Then
                param.Add("TRAN_TYPE", "I1")
            End If
        End If

        param.Add("USER_NAME", user_name)
        Dim f As New frmMainReports
        Dim attrName As String = String.Empty
        For Each item As DataGridViewRow In DataGridView1.SelectedRows
            If isSubSection.Checked Then
                SEARCH_CODE_LIST.Add(item.Cells("SUB_SECTION_CODE").Value.ToString())

                attrName = "PN_SUB_SECTION_CODE_OVERALL"
            ElseIf IsAR.Checked Then
                SEARCH_CODE_LIST.Add(item.Cells("MB_MEMBER_CODE").Value.ToString())
                attrName = "PN_AR_CODE_OVERALL"
            End If
        Next


        param.Add("SUB_SECTION_CODE_LIST", String.Join(",", SEARCH_CODE_LIST))
        f.reportPath = getParameters(5, attrName)

        f.reportParameters = param
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing

    End Sub

    Private Sub ExportProcessingButton_Click(sender As Object, e As EventArgs)
        Dim headers = (From header As DataGridViewColumn In DataGridView2.Columns.Cast(Of DataGridViewColumn)() _
              Select header.HeaderText).ToArray
        Dim rows = From row As DataGridViewRow In DataGridView2.Rows.Cast(Of DataGridViewRow)() _
                   Where Not row.IsNewRow _
                   Select Array.ConvertAll(row.Cells.Cast(Of DataGridViewCell).ToArray, Function(c) If(c.Value IsNot Nothing, c.Value.ToString, ""))
        Using sw As New IO.StreamWriter("data.csv", False, System.Text.Encoding.UTF8)
            sw.WriteLine(String.Join(",", headers))
            For Each r In rows
                sw.WriteLine(String.Join(",", r))
            Next
        End Using
        Process.Start("data.csv")
    End Sub

    Private Sub frmFINReportProduct_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub TO_DATEPicker_ValueChanged(sender As Object, e As EventArgs) Handles TO_DATEPicker.ValueChanged
        If Not TO_DATEPicker.IsHandleCreated Then
            Return
        Else
            TO_DATEPicker.Value = New Date(TO_DATEPicker.Value.Year, TO_DATEPicker.Value.Month, TO_DATEPicker.Value.Day, 23, 59, 59)
        End If

    End Sub
    Private Sub getSU_DIVISION()
        DIV_NAMEComboBox.DataSource = QueryHelper.selectStar("SU_DIVISION").DefaultView
        DIV_NAMEComboBox.DisplayMember = "DIV_NAME"
        DIV_NAMEComboBox.ValueMember = "DIV_CODE"
        DIV_CODETextBox.Text = DIV_NAMEComboBox.SelectedValue.ToString()
    End Sub

    Private Sub DIV_NAMEComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DIV_NAMEComboBox.SelectedIndexChanged
        DIV_CODETextBox.Text = DIV_NAMEComboBox.SelectedValue.ToString()
    End Sub

    Private Sub IsDIV_CheckedChanged(sender As Object, e As EventArgs) Handles IsDIV.CheckedChanged
        DIV_NAMEComboBox.Enabled = IsDIV.Checked
    End Sub
End Class