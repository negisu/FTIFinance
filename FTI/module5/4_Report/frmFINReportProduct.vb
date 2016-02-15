Public Class frmFINReportProduct

    Private Sub search_Click(sender As Object, e As EventArgs) Handles btFind.Click

        searchPN()

    End Sub


    Private Sub searchPN()

        Dim searchValue As String = KeyWordTextBox.Text.Trim.Replace(" ", "%")
        searchValue = "%" & searchValue & "%"
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = " SELECT TOP 1000 IV_SUB_SECTION.SUB_SECTION_CODE, IV_SUB_SECTION.SUB_SECTION_NAME, COUNT(IV_SUB_SECTION.SUB_SECTION_CODE) AS CNT "
        If IsAR.Checked Then
            query = " SELECT TOP 1000 MB_COMP_PERSON.COMP_PERSON_NAME_TH, MB_MEMBER.MEMBER_CODE, COUNT(IV_SUB_SECTION.SUB_SECTION_CODE) AS CNT "
        End If
        query &= " FROM PN_DETAIL INNER JOIN IV_SUB_SECTION ON IV_SUB_SECTION.SUB_SECTION_CODE = PN_DETAIL.SUB_SECTION_CODE INNER JOIN  PN_HEAD ON PN_DETAIL.TRAN_NO = PN_HEAD.TRAN_NO "
        query &= " LEFT JOIN MB_MEMBER ON PN_HEAD.AR_CODE = MB_MEMBER.COMP_PERSON_CODE "
        query &= " LEFT JOIN MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE "
        query &= " LEFT JOIN MB_MEMBER_STATUS ON MB_MEMBER.MEMBER_STATUS_CODE = MB_MEMBER_STATUS.MEMBER_STATUS_CODE  AND MB_MEMBER_STATUS.MODULE = '1' "
        query &= " LEFT JOIN MB_MEMBER_MAIN_GROUP ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE "
        If IsAR.Checked Then
            query &= " WHERE (MB_MEMBER.MEMBER_CODE LIKE @p0 "
            query &= " OR MB_COMP_PERSON.COMP_PERSON_NAME_TH LIKE @p0 "
            query &= " OR MB_COMP_PERSON.COMP_PERSON_NAME_EN LIKE @p0 "
            query &= " OR MB_COMP_PERSON.COMP_PERSON_CODE LIKE @p0) "
        Else
            query &= " WHERE IV_SUB_SECTION.SUB_SECTION_NAME LIKE @p0 "
        End If
        

        parameters.Add("@p0", searchValue)

        If PermissionHelper.isAdmin() Then
        Else
            query &= " AND IV_SUB_SECTION.DIV_CODE_INC = @p1 "
            parameters.Add("@p1", user_div)

        End If



        query &= " AND (PN_HEAD.TRAN_DATE BETWEEN @p2 AND @p3) "
        parameters.Add("@p2", FROM_DATEPicker.Value)
        parameters.Add("@p3", TO_DATEPicker.Value)

        If Not (IsP1.Checked And IsI1.Checked) Then
            If IsP1.Checked Then
                query &= " AND (PN_HEAD.TRAN_TYPE = 'P1') "
            End If
            If IsI1.Checked Then
                query &= " AND (PN_HEAD.TRAN_TYPE = 'I1') "
            End If
        End If

        If Not (IsNormal.Checked And IsCancel.Checked) Then
            If IsNormal.Checked Then
                query &= " AND CANCEL_FLAG <> 'A' "
            End If
            If IsCancel.Checked Then
                query &= " AND CANCEL_FLAG = 'A' "
            End If
        End If

        If Not (IsNotPaid.Checked And IsPaid.Checked) Then
            If IsNotPaid.Checked Then
                query &= " AND PN_DETAIL.BAL_AMT > 0 "
            End If
            If IsPaid.Checked Then
                query &= " AND PN_DETAIL.BAL_AMT = 0 "
            End If
        End If

        If isSubSection.Checked Then
            query &= " GROUP BY IV_SUB_SECTION.SUB_SECTION_CODE, IV_SUB_SECTION.SUB_SECTION_NAME "
            DataGridView1.DataSource = fillWebSQL(query, parameters, "IV_SUB_SECTION")
            DataGridView1.Columns("SUB_SECTION_CODE").HeaderText = "รหัสรายการ"
            DataGridView1.Columns("SUB_SECTION_NAME").HeaderText = "ชื่อรายการ"
        Else
            query &= " GROUP BY MB_COMP_PERSON.COMP_PERSON_NAME_TH, MB_MEMBER.MEMBER_CODE"
            DataGridView1.DataSource = fillWebSQL(query, parameters, "MB_MEMBER")
            DataGridView1.Columns("COMP_PERSON_NAME_TH").HeaderText = "ชื่อลูกหนี้"
            DataGridView1.Columns("MEMBER_CODE").HeaderText = "รหัสสมาชิก"
        End If
        
        DataGridView1.Columns("CNT").HeaderText = "จำนวนรายการ"
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.AutoResizeColumns()
        DataGridView1.Refresh()


    End Sub

    Private Sub frmFINReportProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TO_DATEPicker.MinDate = FROM_DATEPicker.Value
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim param As New Dictionary(Of String, String)

        param.Add("FROM_DATE", FROM_DATEPicker.Value.ToString("MM/dd/yyyy"))
        param.Add("TO_DATE", TO_DATEPicker.Value.ToString("MM/dd/yyyy"))

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


    Private Sub TRAN_DATEPicker_ValueChanged(sender As Object, e As EventArgs) Handles FROM_DATEPicker.ValueChanged
        If Not FROM_DATEPicker.IsHandleCreated Then
            Return
        End If
        TO_DATEPicker.MinDate = FROM_DATEPicker.Value
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick


        Dim SEARCH_CODE_LIST As New List(Of String)

        For Each item As DataGridViewRow In DataGridView1.SelectedRows
            If isSubSection.Checked Then
                SEARCH_CODE_LIST.Add("'" & item.Cells("SUB_SECTION_CODE").Value.ToString() & "'")
            Else
                SEARCH_CODE_LIST.Add("'" & item.Cells("MEMBER_CODE").Value.ToString() & "'")
            End If
        Next

        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = " SELECT PN_HEAD.TRAN_NO, PN_HEAD.TRAN_DATE, PN_DETAIL.BAL_AMT, MB_COMP_PERSON.COMP_PERSON_NAME_TH, PN_DETAIL.NOTE "
        query &= " FROM PN_DETAIL INNER JOIN IV_SUB_SECTION ON IV_SUB_SECTION.SUB_SECTION_CODE = PN_DETAIL.SUB_SECTION_CODE INNER JOIN  PN_HEAD ON PN_DETAIL.TRAN_NO = PN_HEAD.TRAN_NO LEFT JOIN MB_COMP_PERSON ON PN_HEAD.AR_CODE = MB_COMP_PERSON.COMP_PERSON_CODE LEFT JOIN MB_PRENAME ON MB_COMP_PERSON.PREN_CODE = MB_PRENAME.PRENAME_CODE "
        If isSubSection.Checked Then
            query &= " WHERE IV_SUB_SECTION.SUB_SECTION_CODE IN (" & String.Join(",", SEARCH_CODE_LIST) & ") "
        Else
            query &= " WHERE PN_HEAD.MB_MEMBER_CODE IN (" & String.Join(",", SEARCH_CODE_LIST) & ") "
        End If



        If PermissionHelper.isAdmin() Then
        Else
            query &= " AND IV_SUB_SECTION.DIV_CODE_INC = @p1 "
            parameters.Add("@p1", user_div)

        End If



        query &= " AND (PN_HEAD.TRAN_DATE BETWEEN @p2 AND @p3) "
        parameters.Add("@p2", FROM_DATEPicker.Value)
        parameters.Add("@p3", TO_DATEPicker.Value)

        If Not (IsP1.Checked And IsI1.Checked) Then
            If IsP1.Checked Then
                query &= " AND (PN_HEAD.TRAN_TYPE = 'P1') "
            End If
            If IsI1.Checked Then
                query &= " AND (PN_HEAD.TRAN_TYPE = 'I1') "
            End If
        End If

        If Not (IsNormal.Checked And IsCancel.Checked) Then
            If IsNormal.Checked Then
                query &= " AND CANCEL_FLAG <> 'A' "
            End If
            If IsCancel.Checked Then
                query &= " AND CANCEL_FLAG = 'A' "
            End If
        End If

        If Not (IsNotPaid.Checked And IsPaid.Checked) Then
            If IsNotPaid.Checked Then
                query &= " AND BAL_AMT > 0 "
            End If
            If IsPaid.Checked Then
                query &= " AND BAL_AMT = 0 "
            End If
        End If

        query &= " ORDER BY IV_SUB_SECTION.SUB_SECTION_CODE "


        DataGridView2.DataSource = fillWebSQL(query, parameters, "IV_SUB_SECTION")

        For i As Integer = 0 To DataGridView2.ColumnCount - 1
            DataGridView2.Columns(i).Visible = False
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

        DataGridView2.Columns("TRAN_DATE").Visible = True
        DataGridView2.Columns("TRAN_NO").Visible = True
        DataGridView2.Columns("BAL_AMT").Visible = True
        DataGridView2.Columns("COMP_PERSON_NAME_TH").Visible = True
        DataGridView2.Columns("NOTE").Visible = True

        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView2.AutoResizeColumns()
        DataGridView2.Refresh()
    End Sub

    Private Sub PreviewReportButton_Click(sender As Object, e As EventArgs) Handles PreviewReportButton.Click
        If DataGridView1.SelectedRows.Count = 0 Then Return
        Dim param As New Dictionary(Of String, String)

        param.Add("FROM_DATE", FROM_DATEPicker.Value.ToString("MM/dd/yyyy", New System.Globalization.CultureInfo("en-US").DateTimeFormat))
        param.Add("TO_DATE", TO_DATEPicker.Value.ToString("MM/dd/yyyy", New System.Globalization.CultureInfo("en-US").DateTimeFormat))
        Dim SEARCH_CODE_LIST As New List(Of String)

        If PermissionHelper.isAdmin() Then
            param.Add("DIV_CODE", "%")
        Else
            param.Add("DIV_CODE", user_div)
        End If

        If Not (IsP1.Checked And IsI1.Checked) Then
            If IsP1.Checked Then
                param.Add("TRAN_TYPE", "P1")
            End If
            If IsI1.Checked Then
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
            Else
                SEARCH_CODE_LIST.Add(item.Cells("MEMBER_CODE").Value.ToString())
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

    Private Sub ExportProcessingButton_Click(sender As Object, e As EventArgs) Handles ExportProcessingButton.Click
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
End Class