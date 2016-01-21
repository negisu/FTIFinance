Imports Microsoft.Reporting.WinForms

Public Class frmFTIRDLCnewApply

    Dim ds As DataSet

    Private Sub frmFTIRDLCnewApply_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet

        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = "SELECT MEMBER_TYPE_CODE, MEMBER_TYPE_NAME FROM MB_MEMBER_TYPE WHERE (MEMBER_MAIN_GROUP_CODE IN ('000', '100', '200'))  GROUP BY MEMBER_TYPE_CODE, MEMBER_TYPE_NAME"

        Dim dt As DataTable = New DataTable
        Try
            dt = fillWebSQL(query, parameters, "MB_MEMBER_TYPE")
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
        End Try

        ds.Tables.Add(dt)

        ComboBox1.DataSource = ds.Tables("MB_MEMBER_TYPE")
        ComboBox1.DisplayMember = "MEMBER_TYPE_NAME"
        ComboBox1.ValueMember = "MEMBER_TYPE_CODE"

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", DateTimePicker1.Value)
        parameters.Add("@p1", DateTimePicker1.Value.Month)
        parameters.Add("@p2", CInt(DateTimePicker1.Value.ToString("yyyy", ciEN)))
        parameters.Add("@p3", ComboBox1.SelectedValue)
        'parameters.Add("@p4", New DateTime(DateTimePicker1.Value.Year, DateTimePicker1.Value.Month, DateTimePicker1.Value.Day, 23, 59, 59, 999))
        'parameters.Add("@p5", New DateTime(DateTimePicker2.Value.Year, DateTimePicker2.Value.Month, DateTimePicker2.Value.Day, 23, 59, 59, 999))

        Dim query As String = String.Empty
        'query &= "SELECT    ROW_NUMBER() OVER(ORDER BY MB_MEMBER.PREFIX, MB_MEMBER.RUNNING) AS ROW_NO, MB_MEMBER.MEMBER_CODE, MB_COMP_PERSON.COMP_PERSON_NAME_TH, MB_CONTACT.CONTACT_FIRST_NAME_TH + ' ' + MB_CONTACT.CONTACT_LAST_NAME_TH AS CONTACT_NAME_TH, "
        'query &= "                         MB_COMP_PERSON.MAIN_PRODUCTS_SERVICES, dbo.get5_3_25_7(MB_MEMBER.MEMBER_CODE, '100', @p0) AS MEMBER_GROUP, dbo.get5_3_25_7(MB_MEMBER.MEMBER_CODE, '200', @p0) "
        'query &= "                         AS MEMBER_PROVINCE, MB_COMP_PERSON_ADDRESS.ADDR_PROVINCE_NAME, MB_COMP_PERSON_ADDRESS.ADDR_TELEPHONE, 0 AS FEE_REGIST, 0 AS FEE_ANNUAL "
        'query &= "FROM            MB_MEMBER INNER JOIN "
        'query &= "                         MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE LEFT OUTER JOIN "
        'query &= "                         MB_COMP_PERSON_ADDRESS ON MB_COMP_PERSON.COMP_PERSON_CODE = MB_COMP_PERSON_ADDRESS.COMP_PERSON_CODE LEFT OUTER JOIN "
        'query &= "                         MB_CONTACT INNER JOIN "
        'query &= "                         MB_MEMBER_REPRESENT ON MB_CONTACT.CONTACT_CODE = MB_MEMBER_REPRESENT.CONTACT_CODE ON MB_MEMBER.REGIST_CODE = MB_MEMBER_REPRESENT.REGIST_CODE "
        'query &= "WHERE        (MONTH(MB_MEMBER.MEMBER_DATE) = @p1) AND (YEAR(MB_MEMBER.MEMBER_DATE) = @p2) AND (MB_COMP_PERSON_ADDRESS.ADDR_CODE = '001') AND (MB_MEMBER.MEMBER_TYPE_CODE = @p3) "
        'query &= "ORDER BY MB_MEMBER.PREFIX, MB_MEMBER.RUNNING"

        query &= "SELECT        ROW_NUMBER() OVER(ORDER BY MB_MEMBER.PREFIX, MB_MEMBER.RUNNING) AS ROW_NO, MB_MEMBER.MEMBER_CODE, MB_COMP_PERSON.COMP_PERSON_NAME_TH, MB_CONTACT.CONTACT_FIRST_NAME_TH + ' ' + MB_CONTACT.CONTACT_LAST_NAME_TH AS CONTACT_NAME_TH, "
        query &= "                         MB_COMP_PERSON.MAIN_PRODUCTS_SERVICES, dbo.get5_3_25_7(MB_MEMBER.MEMBER_CODE, '100', @p0) AS MEMBER_GROUP, dbo.get5_3_25_7(MB_MEMBER.MEMBER_CODE, '200', @p0) "
        query &= "                         AS MEMBER_PROVINCE, MB_COMP_PERSON_ADDRESS.ADDR_PROVINCE_NAME, MB_COMP_PERSON_ADDRESS.ADDR_TELEPHONE, 0 AS FEE_REGIST, 0 AS FEE_ANNUAL, "
        query &= "                         MB_MEMBER_RATE_DETAIL.FIRST_REGIST_RATE, MB_MEMBER_RATE_DETAIL.FIRST_YEAR_RATE "
        query &= "FROM            MB_MEMBER INNER JOIN "
        query &= "                         MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
        query &= "                         MB_MEMBER_RATE_DETAIL ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_RATE_DETAIL.MEMBER_MAIN_GROUP_CODE AND "
        query &= "                         MB_MEMBER.MEMBER_GROUP_CODE = MB_MEMBER_RATE_DETAIL.MEMBER_GROUP_CODE AND "
        query &= "                         MB_MEMBER.MEMBER_MAIN_TYPE_CODE = MB_MEMBER_RATE_DETAIL.MEMBER_MAIN_TYPE_CODE AND "
        query &= "                         MB_MEMBER.MEMBER_TYPE_CODE = MB_MEMBER_RATE_DETAIL.MEMBER_TYPE_CODE LEFT OUTER JOIN "
        query &= "                         MB_COMP_PERSON_ADDRESS ON MB_COMP_PERSON.COMP_PERSON_CODE = MB_COMP_PERSON_ADDRESS.COMP_PERSON_CODE LEFT OUTER JOIN "
        query &= "                         MB_CONTACT INNER JOIN "
        query &= "                         MB_MEMBER_REPRESENT ON MB_CONTACT.CONTACT_CODE = MB_MEMBER_REPRESENT.CONTACT_CODE ON MB_MEMBER.REGIST_CODE = MB_MEMBER_REPRESENT.REGIST_CODE "
        query &= "WHERE        (MONTH(MB_MEMBER.MEMBER_DATE) = @p1) AND (YEAR(MB_MEMBER.MEMBER_DATE) = @p2) AND (MB_COMP_PERSON_ADDRESS.ADDR_CODE = '001') AND (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN ('000', '100', '200')) AND (MB_MEMBER.MEMBER_TYPE_CODE = @p3) "
        query &= "                         AND (MB_MEMBER_RATE_DETAIL.INACTIVE = 'N') AND (MONTH(MB_MEMBER.REGIST_DATE) BETWEEN MB_MEMBER_RATE_DETAIL.START_MONTH_FIRST_YEAR AND "
        query &= "                         MB_MEMBER_RATE_DETAIL.END_MONTH_FIRST_YEAR) AND (MB_COMP_PERSON.INCOME_PER_YEAR BETWEEN MB_MEMBER_RATE_DETAIL.START_INCOME_AMOUNT AND "
        query &= "                         MB_MEMBER_RATE_DETAIL.END_INCOME_AMOUNT) AND (MB_MEMBER_REPRESENT.REPRESENT_SEQ = 1) "
        query &= "ORDER BY MB_MEMBER.PREFIX, MB_MEMBER.RUNNING"

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
        End If

        'ds.Tables("MB_MEMBER").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER").Columns("REGIST_CODE")}

        Dim f As New frmMainReportViewer

        f.ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "/Reports/module1/rdlc5.3.25.07.FTInewApplyMonths.rdlc"

        f.ReportViewer1.LocalReport.DataSources.Clear()
        f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("dsFTInewApply", ds.Tables("MB_MEMBER")))
        'f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("CourseInfo", Report1.Tables("CourseInfo")))
        'f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("rptParam", Report1.Tables("rptParam")))

        'ReportViewer1.PrinterSettings.PrinterName = DstTime1.config.Rows(ConfigIndx).Item("PrintReportHalfLetter").ToString
        'ReportViewer1.PrinterSettings.DefaultPageSettings.PaperSize = GetSelectedPaperSize(DstTime1.config.Rows(ConfigIndx).Item("PrintReportHalfLetter").ToString, DstTime1.config.Rows(ConfigIndx).Item("PrintReportHalfLetterSize").ToString)
        'ReportViewer1.PrinterSettings.DefaultPageSettings.PaperSource = GetSelectedPaperSource(DstTime1.config.Rows(ConfigIndx).Item("PrintReportHalfLetter").ToString, DstTime1.config.Rows(ConfigIndx).Item("PrintReportHalfLetterSource").ToString)
        'ReportViewer1.PrinterSettings.DefaultPageSettings.Landscape = True

        'Create a report parameter for the sales order number 
        Dim REPORT_TYPE As New ReportParameter()
        REPORT_TYPE.Name = "REPORT_TYPE"
        REPORT_TYPE.Values.Add(ComboBox1.Text)

        Dim REPORT_MONTH As New ReportParameter()
        REPORT_MONTH.Name = "REPORT_MONTH"
        REPORT_MONTH.Values.Add(DateTimePicker1.Value.ToString("MMMM yyyy", ciTH))

        Dim REPORT_QTY As New ReportParameter()
        REPORT_QTY.Name = "REPORT_QTY"
        REPORT_QTY.Values.Add(ds.Tables("MB_MEMBER").Rows.Count.ToString)

        'Set the report parameters for the report
        Dim params() As ReportParameter = {REPORT_TYPE, REPORT_MONTH, REPORT_QTY}
        f.ReportViewer1.LocalReport.SetParameters(params)

        f.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
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
End Class