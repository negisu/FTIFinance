Imports Microsoft.Reporting.WinForms

Public Class frmFTImemberChanged

    Dim ds As DataSet
    Dim dv As DataView

    Private Sub frmFTIapproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMB_MEMBER_RETIRE(ByVal GET_DATE As Date)
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", New Date(GET_DATE.Year, GET_DATE.Month + 1, 1, 0, 0, 0).AddMinutes(-1))
        'parameters.Add("@p1", CInt(GET_DATE.ToString("yyyy", ciEN)))
        'parameters.Add("@p2", CInt(GET_DATE.ToString("MM", ciEN)))

        Dim query As String = String.Empty
        query &= "SELECT        CAST(CAST(YEAR(MODIFY_DATE) AS varchar) + '-' + CAST(MONTH(MODIFY_DATE) AS varchar) + '-' + CAST(DAY(MODIFY_DATE) AS varchar) AS DATETIME) AS DAY_FOR, dbo.get5_3_25_8('FTI_ADDRESS', '', "
        query &= "                         CAST(CAST(YEAR(MODIFY_DATE) AS varchar) + '-' + CAST(MONTH(MODIFY_DATE) AS varchar) + '-' + CAST(DAY(MODIFY_DATE) AS varchar) AS DATETIME)) AS MOD_ADDRESS, dbo.get5_3_25_8('FTI_MAIN', "
        query &= "                         'COMP_PERSON_NAME_TH,COMP_PERSON_NAME_EN', CAST(CAST(YEAR(MODIFY_DATE) AS varchar) + '-' + CAST(MONTH(MODIFY_DATE) AS varchar) + '-' + CAST(DAY(MODIFY_DATE) AS varchar) "
        query &= "                         AS DATETIME)) AS MOD_NAME, dbo.get5_3_25_8('MB_MEMBER_REPRESENT', '', CAST(CAST(YEAR(MODIFY_DATE) AS varchar) + '-' + CAST(MONTH(MODIFY_DATE) AS varchar) + '-' + CAST(DAY(MODIFY_DATE) "
        query &= "                         AS varchar) AS DATETIME)) AS MOD_REPRESENT, dbo.get5_3_25_8('FTI_MAIN', 'REGIST_CODE', CAST(CAST(YEAR(MODIFY_DATE) AS varchar) + '-' + CAST(MONTH(MODIFY_DATE) AS varchar) "
        query &= "                         + '-' + CAST(DAY(MODIFY_DATE) AS varchar) AS DATETIME)) AS MOD_REGIST, dbo.get5_3_25_8('FTI_MAIN', 'APPROVE_RETIRE_DATE', CAST(CAST(YEAR(MODIFY_DATE) AS varchar) "
        query &= "                         + '-' + CAST(MONTH(MODIFY_DATE) AS varchar) + '-' + CAST(DAY(MODIFY_DATE) AS varchar) AS DATETIME)) AS MOD_RETIRE, dbo.get5_3_25_8('FTI_MAIN', 'BLACKLIST_DATE', CAST(CAST(YEAR(MODIFY_DATE) "
        query &= "                          AS varchar) + '-' + CAST(MONTH(MODIFY_DATE) AS varchar) + '-' + CAST(DAY(MODIFY_DATE) AS varchar) AS DATETIME)) AS MOD_BLACKLIST, '' AS MOD_OTHER "
        query &= "FROM            MB_LOGS "
        query &= "WHERE        (YEAR(MODIFY_DATE) = YEAR(@p0)) AND (MONTH(MODIFY_DATE) = MONTH(@p0)) "
        query &= "GROUP BY CAST(CAST(YEAR(MODIFY_DATE) AS varchar) + '-' + CAST(MONTH(MODIFY_DATE) AS varchar) + '-' + CAST(DAY(MODIFY_DATE) AS varchar) AS DATETIME) "
        query &= "ORDER BY DAY_FOR"

        'DATEADD(dd,1,CAST(CAST(YEAR(@p0) AS varchar) + '-' + CAST(MB_MONTHS.MONTH AS varchar) + '-' + CAST(1 AS varchar) AS DATETIME))
        'query &= "SELECT    * "
        'query &= "FROM            MB_MEMBER_RETIRE "
        'query &= "WHERE        (APPROVE_DATE IS NULL) AND (MEMBER_MAIN_GROUP_CODE IN (" & getParameters(1, "FTI_MAIN_GROUP_APPROVE") & ")) "
        'query &= "ORDER BY RETIRE_DATE DESC "

        Dim dt As DataTable = New DataTable
        Try
            dt = fillWebSQL(query, parameters, "MB_MEMBER")
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
        End Try

        If ds.Tables.Contains("MB_MEMBER") = True Then
            ds.Tables("MB_MEMBER").Clear()
            ds.Tables("MB_MEMBER").Merge(dt)
            ds.Tables("MB_MEMBER").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            dv = New DataView(ds.Tables("MB_MEMBER"))

            'ds.Tables("MB_MEMBER").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER").Columns("REGIST_CODE")}

            DataGridView1.DataSource = dv

            'For i As Integer = 0 To DataGridView1.ColumnCount - 1
            '    DataGridView1.Columns(i).Visible = False
            'Next

            'DataGridView1.Columns("MEMBER_TYPE_NAME").Visible = True
            'DataGridView1.Columns("MEMBER_SHORT_NAME").Visible = True
            'DataGridView1.Columns("BEGINS").Visible = True
            'DataGridView1.Columns("REGIST").Visible = True
            'DataGridView1.Columns("MOVE_TO").Visible = True
            'DataGridView1.Columns("HOLD").Visible = True
            'DataGridView1.Columns("RESIGN").Visible = True
            'DataGridView1.Columns("BLACK_LIST").Visible = True
            'DataGridView1.Columns("MOVE_FROM").Visible = True
            'DataGridView1.Columns("GROUPS").Visible = True
            'DataGridView1.Columns("PROVINCES").Visible = True
        End If

        'filter empty rows out
        If ds.Tables("MB_MEMBER").Rows.Count > 0 Then
            For Each r As DataRow In ds.Tables("MB_MEMBER").Rows
                If r("MOD_ADDRESS").ToString.Length = 0 And r("MOD_NAME").ToString.Length = 0 And r("MOD_REPRESENT").ToString.Length = 0 And r("MOD_REGIST").ToString.Length = 0 And r("MOD_RETIRE").ToString.Length = 0 And r("MOD_BLACKLIST").ToString.Length = 0 And r("MOD_OTHER").ToString.Length = 0 Then
                    r.Delete()
                End If
            Next

            ds.Tables("MB_MEMBER").AcceptChanges()
        End If

        ''parameters.Clear()

        'query = String.Empty
        'query &= "SELECT        CAST(CAST(YEAR(@p0) AS varchar) + '-' + CAST(MONTH AS varchar) + '-' + CAST(1 AS varchar) AS DATETIME) AS YEAR_FOR, YEAR(@p0) AS YEARS, MONTH, MONTH_NAME_THA, "
        'query &= "                         MONTH_NAME_ENG, "
        'query &= "                             (SELECT        COUNT(*) AS Expr1 "
        'query &= "                               FROM            MB_MEMBER "
        'query &= "                               WHERE        (MEMBER_STATUS_CODE = 'A') AND (MEMBER_MAIN_GROUP_CODE = '000') AND (MEMBER_DATE < DATEADD(dd,1,CAST(CAST(YEAR(@p0) AS varchar) + '-' + CAST(MB_MONTHS.MONTH AS varchar) "
        'query &= "                                                         + '-' + CAST(1 AS varchar) AS DATETIME)))) AS CNT "
        'query &= "FROM            MB_MONTHS "
        'query &= "UNION "
        'query &= "SELECT        CAST(CAST(YEAR(@p0) - 1 AS varchar) + '-' + CAST(MONTH AS varchar) + '-' + CAST(1 AS varchar) AS DATETIME) AS YEAR_FOR, YEAR(@p0) - 1 AS YEARS, MONTH, MONTH_NAME_THA, "
        'query &= "                         MONTH_NAME_ENG, "
        'query &= "                             (SELECT        COUNT(*) AS Expr1 "
        'query &= "                               FROM            MB_MEMBER AS MB_MEMBER_1 "
        'query &= "                               WHERE        (MEMBER_STATUS_CODE = 'A') AND (MEMBER_MAIN_GROUP_CODE = '000') AND (MEMBER_DATE < DATEADD(dd,1,CAST(CAST(YEAR(@p0) - 1 AS varchar) + '-' + CAST(MB_MONTHS_1.MONTH AS varchar) "
        'query &= "                                                         + '-' + CAST(1 AS varchar) AS DATETIME)))) AS CNT "
        'query &= "FROM            MB_MONTHS AS MB_MONTHS_1"

        'Dim dt2 As DataTable = New DataTable
        'Try
        '    dt2 = fillWebSQL(query, parameters, "MB_MEMBER2")
        'Catch ex As Exception
        '    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
        'End Try

        'If ds.Tables.Contains("MB_MEMBER2") = True Then
        '    ds.Tables("MB_MEMBER2").Clear()
        '    ds.Tables("MB_MEMBER2").Merge(dt2)
        '    ds.Tables("MB_MEMBER2").AcceptChanges()
        'Else
        '    ds.Tables.Add(dt2)
        'End If

        ''fix year
        'Dim dd As Date = DateTimePicker1.Value
        'Dim d As Date
        'For i As Integer = 0 To ds.Tables("MB_MEMBER2").Rows.Count - 1
        '    d = CDate(ds.Tables("MB_MEMBER2").Rows(i).Item("YEAR_FOR"))
        '    ds.Tables("MB_MEMBER2").Rows(i).Item("YEARS") = d.ToString("yyyy", ciTH)
        '    If d > dd Then ds.Tables("MB_MEMBER2").Rows(i).Item("CNT") = 0
        'Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btPrintPreview.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainReportViewer
        'Me.BindingSource1.DataSource = MyDataTable
        'Dim rds As New ReportDataSource("DataSet1", Me.BindingSource1)
        f.ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "/Reports/module1/rdlc5.3.25.08.FTImemberChanged.rdlc"
        'Me.ReportViewer1.LocalReport.DataSources.Clear()
        'Me.ReportViewer1.LocalReport.DataSources.Add(rds)

        'Create a report parameter for the sales order number 

        Dim DOC_DATE As New ReportParameter()
        DOC_DATE.Name = "DOC_DATE"
        DOC_DATE.Values.Add(DateTimePicker1.Value.ToString("MMMM yyyy", ciTH))

        'Set the report parameters for the report
        Dim params() As ReportParameter = {DOC_DATE}
        f.ReportViewer1.LocalReport.SetParameters(params)

        f.ReportViewer1.LocalReport.DataSources.Clear()
        f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("ds", ds.Tables("MB_MEMBER")))
        'f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("ds2", ds.Tables("MB_MEMBER2")))

        f.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        f.ReportViewer1.ZoomMode = ZoomMode.Percent
        f.ReportViewer1.ZoomPercent = 100
        f.ReportViewer1.LocalReport.Refresh()

        f.WindowState = FormWindowState.Maximized
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    Private Sub btFind_Click(sender As Object, e As EventArgs) Handles btFind.Click
        getMB_MEMBER_RETIRE(DateTimePicker1.Value)
    End Sub
End Class