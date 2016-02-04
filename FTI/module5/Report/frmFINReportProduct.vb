Public Class frmFINReportProduct

    Private Sub search_Click(sender As Object, e As EventArgs) Handles btFind.Click

        searchPN()

    End Sub


    Private Sub searchPN()

        Dim searchValue As String = KeyWordTextBox.Text.Trim.Replace(" ", "%")
        searchValue = "%" & searchValue & "%"
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = " SELECT TOP 1000 IV_SUB_SECTION.SUB_SECTION_CODE, IV_SUB_SECTION.SUB_SECTION_NAME, COUNT(IV_SUB_SECTION.SUB_SECTION_CODE) AS CNT "
        query &= " FROM PN_DETAIL INNER JOIN IV_SUB_SECTION ON IV_SUB_SECTION.SUB_SECTION_CODE = PN_DETAIL.SUB_SECTION_CODE INNER JOIN  PN_HEAD ON PN_DETAIL.TRAN_NO = PN_HEAD.TRAN_NO"
        query &= " WHERE IV_SUB_SECTION.SUB_SECTION_NAME LIKE @p0 "
        'If DocTypeComboBox.SelectedIndex = 0 Then
        '    query &= " AND TRAN_TYPE = 'P1' "
        'Else
        '    query &= " AND TRAN_TYPE = 'I1' "
        'End If

        If PermissionHelper.isAdmin() Then
        Else
            query &= " AND IV_SUB_SECTION.DIV_CODE_INC = @p1 "
            parameters.Add("@p1", user_div)

        End If



        query &= " AND (PN_HEAD.TRAN_DATE BETWEEN @p2 AND @p3) "
        parameters.Add("@p2", FROM_DATEPicker.Value)
        parameters.Add("@p3", TO_DATEPicker.Value)

        query &= " GROUP BY IV_SUB_SECTION.SUB_SECTION_CODE, IV_SUB_SECTION.SUB_SECTION_NAME "


        parameters.Add("@p0", searchValue)
        DataGridView1.DataSource = fillWebSQL(query, parameters, "IV_SUB_SECTION")
        DataGridView1.Columns("SUB_SECTION_CODE").HeaderText = "รหัสรายการ"
        DataGridView1.Columns("SUB_SECTION_NAME").HeaderText = "ชื่อรายการ"
        DataGridView1.Columns("CNT").HeaderText = "จำนวนรายการคงค้าง"
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.AutoResizeColumns()
        DataGridView1.Refresh()


    End Sub

    Private Sub frmFINReportProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TO_DATEPicker.MinDate = FROM_DATEPicker.Value
    End Sub


    Private Sub temp(sender As Object, e As DataGridViewCellEventArgs)
        Dim param As New Dictionary(Of String, String)
        param.Add("SUB_SECTION_CODE", DataGridView1.CurrentRow.Cells("SUB_SECTION_CODE").Value.ToString)
        param.Add("FROM_DATE", FROM_DATEPicker.Value.ToString("MM/dd/yyyy"))
        param.Add("TO_DATE", TO_DATEPicker.Value.ToString("MM/dd/yyyy"))

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

    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
        Me.Dispose()
    End Sub

    Private Sub TRAN_DATEPicker_ValueChanged(sender As Object, e As EventArgs) Handles FROM_DATEPicker.ValueChanged
        If Not FROM_DATEPicker.IsHandleCreated Then
            Return
        End If
        TO_DATEPicker.MinDate = FROM_DATEPicker.Value
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim SUB_SECTION_CODE_LIST As New List(Of String)

        For Each item As DataGridViewRow In DataGridView1.SelectedRows
            SUB_SECTION_CODE_LIST.Add(item.Cells("SUB_SECTION_CODE").Value.ToString())
        Next

        Dim teststring = SUB_SECTION_CODE_LIST.ToArray.ToString()
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = " SELECT TOP 1000 IV_SUB_SECTION.SUB_SECTION_CODE, IV_SUB_SECTION.SUB_SECTION_NAME, COUNT(IV_SUB_SECTION.SUB_SECTION_CODE) AS CNT "
        query &= " FROM PN_DETAIL INNER JOIN IV_SUB_SECTION ON IV_SUB_SECTION.SUB_SECTION_CODE = PN_DETAIL.SUB_SECTION_CODE INNER JOIN  PN_HEAD ON PN_DETAIL.TRAN_NO = PN_HEAD.TRAN_NO"
        query &= " WHERE IV_SUB_SECTION.SUB_SECTION_CODE IN @p0 "
        parameters.Add("@p0", SUB_SECTION_CODE_LIST.ToArray)


        If PermissionHelper.isAdmin() Then
        Else
            query &= " AND IV_SUB_SECTION.DIV_CODE_INC = @p1 "
            parameters.Add("@p1", user_div)

        End If



        query &= " AND (PN_HEAD.TRAN_DATE BETWEEN @p2 AND @p3) "
        parameters.Add("@p2", FROM_DATEPicker.Value)
        parameters.Add("@p3", TO_DATEPicker.Value)

        query &= " GROUP BY IV_SUB_SECTION.SUB_SECTION_CODE, IV_SUB_SECTION.SUB_SECTION_NAME "



        DataGridView2.DataSource = fillWebSQL(query, parameters, "IV_SUB_SECTION")
        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView2.AutoResizeColumns()
        DataGridView2.Refresh()
    End Sub

End Class