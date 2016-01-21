Imports Microsoft.Reporting.WinForms

Public Class frmFTImemberSummary

    Dim ds As DataSet
    Dim dv As DataView
    'Dim bsMEMBER_MAIN_GROUP As BindingSource
    'Dim bsMEMBER_GROUP As BindingSource
    'Dim dvMEMBER_MAIN_GROUP As DataView
    Dim dvMEMBER_GROUP As DataView

    Private Sub frmFTIapproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet

        'getMEMBER_MAIN_GROUP()
        'getMEMBER_GROUP()

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    'Private Sub getMEMBER_MAIN_GROUP()
    '    Dim parameters As New Dictionary(Of String, Object)
    '    'parameters.Add("@p0", "0")
    '    'parameters.Add("@p1", "1")
    '    'parameters.Add("@p2", "2")
    '    'parameters.Add("@p3", "3")

    '    Dim query As String = String.Empty
    '    query &= "SELECT [MEMBER_MAIN_GROUP_CODE], [MEMBER_MAIN_GROUP_NAME], [INACTIVE] FROM [MB_MEMBER_MAIN_GROUP] "
    '    'query &= "WHERE ([INACTIVE] <> 'N') "
    '    query &= "ORDER BY MEMBER_MAIN_GROUP_CODE "

    '    Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_MAIN_GROUP").Copy
    '    'dt.TableName = "MB_MEMBER_MAIN_GROUP"
    '    If ds.Tables.Contains("MB_MEMBER_MAIN_GROUP") = True Then
    '        ds.Tables("MB_MEMBER_MAIN_GROUP").Clear()
    '        ds.Tables("MB_MEMBER_MAIN_GROUP").Merge(dt)
    '        ds.Tables("MB_MEMBER_MAIN_GROUP").AcceptChanges()
    '    Else
    '        ds.Tables.Add(dt)
    '        'bsMEMBER_MAIN_GROUP = New BindingSource(ds, "MB_MEMBER_MAIN_GROUP")

    '        'ComboBox1.DataSource = bsMEMBER_MAIN_GROUP
    '        'ComboBox1.DisplayMember = "MEMBER_MAIN_GROUP_NAME"
    '        'ComboBox1.ValueMember = "MEMBER_MAIN_GROUP_CODE"
    '    End If

    '    CheckedComboBox1.Items.Clear()
    '    For i As Integer = 0 To ds.Tables("MB_MEMBER_MAIN_GROUP").Rows.Count - 1
    '        Dim cc As New CheckComboBox.CCBoxItem(ds.Tables("MB_MEMBER_MAIN_GROUP").Rows(i).Item("MEMBER_MAIN_GROUP_NAME").ToString, i)
    '        CheckedComboBox1.Items.Add(cc)
    '    Next
    '    CheckedComboBox1.MaxDropDownItems = 5
    '    CheckedComboBox1.DisplayMember = "Name"
    '    CheckedComboBox1.ValueSeparator = ", "
    '    CheckedComboBox1.SetItemChecked(0, True)

    'End Sub

    'Private Sub getMEMBER_GROUP()
    '    Dim parameters As New Dictionary(Of String, Object)
    '    'parameters.Add("@p0", MEMBER_MAIN_GROUP_CODE)
    '    'parameters.Add("@p1", "1")
    '    'parameters.Add("@p2", "2")
    '    'parameters.Add("@p3", "3")

    '    Dim query As String = String.Empty
    '    query &= "SELECT MEMBER_MAIN_GROUP_CODE, [MEMBER_GROUP_CODE], [MEMBER_GROUP_NAME], [MEMBER_GROUP_NAME_EN], [INACTIVE] FROM [MB_MEMBER_GROUP] "
    '    'query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) "
    '    query &= "ORDER BY [MEMBER_GROUP_CODE] "

    '    Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_GROUP").Copy
    '    'dt.TableName = "MB_MEMBER_GROUP"
    '    If ds.Tables.Contains("MB_MEMBER_GROUP") = True Then
    '        ds.Tables("MB_MEMBER_GROUP").Clear()
    '        ds.Tables("MB_MEMBER_GROUP").Merge(dt)
    '        ds.Tables("MB_MEMBER_GROUP").AcceptChanges()
    '    Else
    '        ds.Tables.Add(dt)
    '        'bsMEMBER_GROUP = New BindingSource(ds, "MB_MEMBER_GROUP")
    '        dvMEMBER_GROUP = New DataView(ds.Tables("MB_MEMBER_GROUP"))

    '        'ComboBox2.DataSource = bsMEMBER_GROUP
    '        'ComboBox2.DisplayMember = "MEMBER_GROUP_NAME"
    '        'ComboBox2.ValueMember = "MEMBER_GROUP_CODE"
    '    End If

    '    CheckedComboBox2.Items.Clear()
    '    For i As Integer = 0 To ds.Tables("MB_MEMBER_GROUP").Rows.Count - 1
    '        Dim cc As New CheckComboBox.CCBoxItem(ds.Tables("MB_MEMBER_GROUP").Rows(i).Item("MEMBER_GROUP_NAME").ToString, i)
    '        CheckedComboBox2.Items.Add(cc)
    '    Next
    '    CheckedComboBox2.MaxDropDownItems = 5
    '    CheckedComboBox2.DisplayMember = "Name"
    '    CheckedComboBox2.ValueSeparator = ", "
    '    CheckedComboBox2.SetItemChecked(0, True)

    'End Sub

    'Private Sub getMB_MEMBER_RETIRE(ByVal GET_DATE As Date)
    '    If CheckedComboBox1.CheckedItems.Count > 0 And CheckedComboBox2.CheckedItems.Count > 0 Then
    '        Dim sMEMBER_MAIN_GROUP_CODE As String = String.Empty
    '        For Each cc As CheckComboBox.CCBoxItem In CheckedComboBox1.CheckedItems
    '            If sMEMBER_MAIN_GROUP_CODE.Length > 0 Then
    '                sMEMBER_MAIN_GROUP_CODE &= ", " & String.Format("'{0}'", ds.Tables("MB_MEMBER_MAIN_GROUP").Rows(cc.Value).Item("MEMBER_MAIN_GROUP_CODE").ToString)
    '            Else
    '                sMEMBER_MAIN_GROUP_CODE = String.Format("'{0}'", ds.Tables("MB_MEMBER_MAIN_GROUP").Rows(cc.Value).Item("MEMBER_MAIN_GROUP_CODE").ToString)
    '            End If
    '        Next

    '        Dim sMEMBER_GROUP_CODE As String = String.Empty
    '        For Each cc As CheckComboBox.CCBoxItem In CheckedComboBox2.CheckedItems
    '            If sMEMBER_GROUP_CODE.Length > 0 Then
    '                sMEMBER_GROUP_CODE &= ", " & String.Format("'{0}'", ds.Tables("MB_MEMBER_GROUP").Rows(cc.Value).Item("MEMBER_GROUP_CODE").ToString)
    '            Else
    '                sMEMBER_GROUP_CODE = String.Format("'{0}'", ds.Tables("MB_MEMBER_GROUP").Rows(cc.Value).Item("MEMBER_GROUP_CODE").ToString)
    '            End If
    '        Next

    '        Dim d As Date = New Date(GET_DATE.Year, GET_DATE.Month, 1, 0, 0, 0).AddMonths(1).AddMonths(-1)

    '        Dim parameters As New Dictionary(Of String, Object)
    '        parameters.Add("@p0", d)
    '        'parameters.Add("@p1", CInt(GET_DATE.ToString("yyyy", ciEN)))
    '        'parameters.Add("@p2", CInt(GET_DATE.ToString("MM", ciEN)))

    '        Dim query As String = String.Empty
    '        query &= "SELECT        MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE, MB_MEMBER_TYPE.MEMBER_TYPE_CODE, MB_MEMBER_TYPE.MEMBER_TYPE_NAME, COUNT(*) AS CNT "
    '        query &= "FROM            MB_MEMBER_TYPE INNER JOIN "
    '        query &= "                         MB_MEMBER ON MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE = MB_MEMBER.MEMBER_MAIN_TYPE_CODE AND "
    '        query &= "                         MB_MEMBER_TYPE.MEMBER_TYPE_CODE = MB_MEMBER.MEMBER_TYPE_CODE AND MB_MEMBER_TYPE.MEMBER_GROUP_CODE = MB_MEMBER.MEMBER_GROUP_CODE AND "
    '        query &= "                         MB_MEMBER_TYPE.MEMBER_MAIN_GROUP_CODE = MB_MEMBER.MEMBER_MAIN_GROUP_CODE "
    '        query &= "WHERE        (MB_MEMBER_TYPE.MEMBER_MAIN_GROUP_CODE IN (" & sMEMBER_MAIN_GROUP_CODE & ")) AND (MB_MEMBER_TYPE.MEMBER_GROUP_CODE IN (" & sMEMBER_GROUP_CODE & ")) AND (MB_MEMBER.MEMBER_STATUS_CODE = 'A') "
    '        query &= "GROUP BY MB_MEMBER_TYPE.MEMBER_TYPE_NAME, MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE, MB_MEMBER_TYPE.MEMBER_GROUP_CODE, MB_MEMBER_TYPE.MEMBER_TYPE_CODE "
    '        query &= "ORDER BY MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE, MB_MEMBER_TYPE.MEMBER_GROUP_CODE, MB_MEMBER_TYPE.MEMBER_TYPE_CODE"
    '        'query &= "GROUP BY MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE, MB_MEMBER_TYPE.MEMBER_TYPE_CODE "
    '        'query &= "ORDER BY MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE, MB_MEMBER_TYPE.MEMBER_TYPE_CODE"

    '        'MessageBox.Show(query)

    '        Dim dt As DataTable = New DataTable
    '        Try
    '            dt = fillWebSQL(query, parameters, "MB_MEMBER")
    '        Catch ex As Exception
    '            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
    '        End Try

    '        If ds.Tables.Contains("MB_MEMBER") = True Then
    '            ds.Tables("MB_MEMBER").Clear()
    '            ds.Tables("MB_MEMBER").Merge(dt)
    '            ds.Tables("MB_MEMBER").AcceptChanges()
    '        Else
    '            ds.Tables.Add(dt)

    '            dv = New DataView(ds.Tables("MB_MEMBER"))

    '            'ds.Tables("MB_MEMBER").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER").Columns("REGIST_CODE")}

    '            DataGridView1.DataSource = dv

    '            'For i As Integer = 0 To DataGridView1.ColumnCount - 1
    '            '    DataGridView1.Columns(i).Visible = False
    '            'Next

    '            'DataGridView1.Columns("MEMBER_TYPE_NAME").Visible = True
    '            'DataGridView1.Columns("MEMBER_SHORT_NAME").Visible = True
    '            'DataGridView1.Columns("BEGINS").Visible = True
    '            'DataGridView1.Columns("REGIST").Visible = True
    '            'DataGridView1.Columns("MOVE_TO").Visible = True
    '            'DataGridView1.Columns("HOLD").Visible = True
    '            'DataGridView1.Columns("RESIGN").Visible = True
    '            'DataGridView1.Columns("BLACK_LIST").Visible = True
    '            'DataGridView1.Columns("MOVE_FROM").Visible = True
    '            'DataGridView1.Columns("GROUPS").Visible = True
    '            'DataGridView1.Columns("PROVINCES").Visible = True
    '        End If

    '        'parameters.Clear()

    '        query = String.Empty
    '        query &= "SELECT        MONTH, MONTH_NAME_THA, MONTH_NAME_ENG, "
    '        query &= "                             (SELECT        COUNT(*) AS Expr1 "
    '        query &= "                               FROM            MB_MEMBER "
    '        query &= "                               WHERE        (MEMBER_STATUS_CODE = 'A') AND (MEMBER_MAIN_GROUP_CODE IN (" & sMEMBER_MAIN_GROUP_CODE & ")) AND (MEMBER_GROUP_CODE IN (" & sMEMBER_GROUP_CODE & ")) AND (MEMBER_DATE < DATEADD(dd, 1, CAST(CAST(YEAR(@p0) AS varchar) "
    '        query &= "                                                         + '-' + CAST(MB_MONTHS.MONTH AS varchar) + '-' + CAST(1 AS varchar) AS DATETIME)))) AS CNT "
    '        query &= "FROM            MB_MONTHS"

    '        'MessageBox.Show(query)

    '        Dim dt2 As DataTable = New DataTable
    '        Try
    '            dt2 = fillWebSQL(query, parameters, "MB_MEMBER2")
    '        Catch ex As Exception
    '            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
    '        End Try

    '        If ds.Tables.Contains("MB_MEMBER2") = True Then
    '            ds.Tables("MB_MEMBER2").Clear()
    '            ds.Tables("MB_MEMBER2").Merge(dt2)
    '            ds.Tables("MB_MEMBER2").AcceptChanges()
    '        Else
    '            ds.Tables.Add(dt2)
    '        End If

    '        query = String.Empty
    '        query &= "SELECT        MB_MEMBER_TYPE.MEMBER_MAIN_GROUP_CODE, MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE, "
    '        query &= "                             (SELECT DISTINCT COUNT(*) AS Expr1 "
    '        query &= "                               FROM            MB_MEMBER "
    '        query &= "                               WHERE        (MEMBER_MAIN_GROUP_CODE = MB_MEMBER_TYPE.MEMBER_MAIN_GROUP_CODE) AND (MEMBER_MAIN_TYPE_CODE = MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE) AND "
    '        query &= "                                                         (MEMBER_STATUS_CODE = 'A')) AS CNT "
    '        query &= "FROM            MB_MEMBER_TYPE INNER JOIN "
    '        query &= "                         MB_MEMBER AS MB_MEMBER_1 ON MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE = MB_MEMBER_1.MEMBER_MAIN_TYPE_CODE AND "
    '        query &= "                         MB_MEMBER_TYPE.MEMBER_GROUP_CODE = MB_MEMBER_1.MEMBER_GROUP_CODE AND MB_MEMBER_TYPE.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_1.MEMBER_MAIN_GROUP_CODE "
    '        query &= "WHERE        (MB_MEMBER_TYPE.MEMBER_MAIN_GROUP_CODE IN ('100', '200')) AND (MB_MEMBER_TYPE.MEMBER_GROUP_CODE IN (" & sMEMBER_GROUP_CODE & ")) AND (MB_MEMBER_1.MEMBER_STATUS_CODE = 'A') "
    '        query &= "GROUP BY MB_MEMBER_TYPE.MEMBER_MAIN_GROUP_CODE, MB_MEMBER_TYPE.MEMBER_GROUP_CODE, MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE "
    '        query &= "ORDER BY MB_MEMBER_TYPE.MEMBER_MAIN_GROUP_CODE, MB_MEMBER_TYPE.MEMBER_GROUP_CODE, MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE"
    '        'query &= "GROUP BY MB_MEMBER_TYPE.MEMBER_MAIN_GROUP_CODE, MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE "
    '        'query &= "ORDER BY MB_MEMBER_TYPE.MEMBER_MAIN_GROUP_CODE, MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE"

    '        Dim dt3 As DataTable = New DataTable
    '        Try
    '            dt3 = fillWebSQL(query, parameters, "MB_MEMBER3")
    '        Catch ex As Exception
    '            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
    '        End Try

    '        If ds.Tables.Contains("MB_MEMBER3") = True Then
    '            ds.Tables("MB_MEMBER3").Clear()
    '            ds.Tables("MB_MEMBER3").Merge(dt3)
    '            ds.Tables("MB_MEMBER3").AcceptChanges()
    '        Else
    '            ds.Tables.Add(dt3)
    '        End If
    '    End If

    'End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btPrintPreview.Click
        Me.Close()
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        'Dim f As New frmMainReportViewer
        ''Me.BindingSource1.DataSource = MyDataTable
        ''Dim rds As New ReportDataSource("DataSet1", Me.BindingSource1)
        'f.ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "/Reports/module1/rdlc5.3.25.11.1.FTImemberSummary.rdlc"
        ''Me.ReportViewer1.LocalReport.DataSources.Clear()
        ''Me.ReportViewer1.LocalReport.DataSources.Add(rds)

        ''Create a report parameter for the sales order number 

        'Dim REPORT_TYPE As New ReportParameter()
        'REPORT_TYPE.Name = "REPORT_TYPE"
        'REPORT_TYPE.Values.Add(DateTimePicker1.Value.ToString("MMMM yyyy", ciTH))

        'Dim REPORT_MONTH As New ReportParameter()
        'REPORT_MONTH.Name = "REPORT_MONTH"
        'REPORT_MONTH.Values.Add(DateTimePicker1.Value.ToString("MMMM yyyy", ciTH))

        'Dim REPORT_QTY As New ReportParameter()
        'REPORT_QTY.Name = "REPORT_QTY"
        'REPORT_QTY.Values.Add(DateTimePicker1.Value.ToString("MMMM yyyy", ciTH))

        ''Set the report parameters for the report
        'Dim params() As ReportParameter = {REPORT_TYPE, REPORT_MONTH, REPORT_QTY}
        'f.ReportViewer1.LocalReport.SetParameters(params)

        'f.ReportViewer1.LocalReport.DataSources.Clear()
        'f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("dsFTInewApply", ds.Tables("MB_MEMBER")))
        ''f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("ds2", ds.Tables("MB_MEMBER2")))

        'f.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        'f.ReportViewer1.ZoomMode = ZoomMode.Percent
        'f.ReportViewer1.ZoomPercent = 100
        'f.ReportViewer1.LocalReport.Refresh()

        'f.WindowState = FormWindowState.Maximized
        'If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
        '    '
        'End If
        'f.Dispose()
        'f = Nothing
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    'Private Sub btFind_Click(sender As Object, e As EventArgs)
    '    getMB_MEMBER_RETIRE(DateTimePicker1.Value)
    'End Sub

    'Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    'If ComboBox1.SelectedValue IsNot Nothing Then getMEMBER_GROUP(ComboBox1.SelectedValue.ToString)
    '    If CheckedComboBox1.CheckedItems.Count > 0 And dvMEMBER_GROUP IsNot Nothing Then
    '        Dim s As String = String.Empty
    '        For Each cc As CheckComboBox.CCBoxItem In CheckedComboBox1.CheckedItems
    '            If s.Length > 0 Then
    '                s &= ", " & String.Format("'{0}'", ds.Tables("MB_MEMBER_MAIN_GROUP").Rows(cc.Value).Item("MEMBER_MAIN_GROUP_CODE").ToString)
    '            Else
    '                s = String.Format("'{0}'", ds.Tables("MB_MEMBER_MAIN_GROUP").Rows(cc.Value).Item("MEMBER_MAIN_GROUP_CODE").ToString)
    '            End If
    '        Next
    '        dvMEMBER_GROUP.RowFilter = String.Format("(MEMBER_MAIN_GROUP_CODE IN ({0}))", s)

    '        Dim dt As DataTable = dvMEMBER_GROUP.ToTable
    '        If dt.Rows.Count > 0 Then
    '            CheckedComboBox2.Items.Clear()
    '            For i As Integer = 0 To dt.Rows.Count - 1
    '                Dim cc As New CheckComboBox.CCBoxItem(dt.Rows(i).Item("MEMBER_GROUP_NAME").ToString, i)
    '                CheckedComboBox2.Items.Add(cc)
    '            Next
    '            CheckedComboBox2.SetItemChecked(0, True)

    '            btFind_Click(sender, e)
    '        End If
    '    End If
    'End Sub

    'Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    'If ComboBox1.SelectedValue IsNot Nothing And ComboBox2.SelectedValue IsNot Nothing Then getMEMBER_MAIN_TYPE(ComboBox1.SelectedValue.ToString, ComboBox2.SelectedValue.ToString)
    '    If CheckedComboBox1.CheckedItems.Count > 0 And CheckedComboBox2.CheckedItems.Count > 0 Then
    '        'bsMEMBER_MAIN_TYPE.Filter = String.Format("(MEMBER_MAIN_GROUP_CODE = '{0}') AND (MEMBER_GROUP_CODE = '{1}')", ComboBox1.SelectedValue.ToString, ComboBox2.SelectedValue.ToString)
    '        btFind_Click(sender, e)
    '    End If
    'End Sub

    'Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
    '    If CheckedComboBox1.Items.Count > 0 Then
    '        For i As Integer = 0 To CheckedComboBox1.Items.Count - 1
    '            CheckedComboBox1.SetItemChecked(i, True)
    '        Next
    '    End If
    'End Sub

    'Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
    '    If CheckedComboBox1.Items.Count > 0 Then
    '        For i As Integer = 0 To CheckedComboBox1.Items.Count - 1
    '            CheckedComboBox1.SetItemChecked(i, False)
    '        Next
    '    End If
    'End Sub

    'Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
    '    If CheckedComboBox2.Items.Count > 0 Then
    '        For i As Integer = 0 To CheckedComboBox2.Items.Count - 1
    '            CheckedComboBox2.SetItemChecked(i, True)
    '        Next
    '    End If
    'End Sub

    'Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
    '    If CheckedComboBox2.Items.Count > 0 Then
    '        For i As Integer = 0 To CheckedComboBox2.Items.Count - 1
    '            CheckedComboBox2.SetItemChecked(i, False)
    '        Next
    '    End If
    'End Sub

    Private Sub LinkLabel1_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim reportParam As New Dictionary(Of String, String)
        'reportParam.Add("ReportParameter1", "SOME_STRING")

        Dim f As New frmMainReports
        f.reportPath = getParameters(1, "FTI_RPT_5.3.25.10.0_reportPath")
        f.reportParameters = reportParam

        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim reportParam As New Dictionary(Of String, String)
        'reportParam.Add("ReportParameter1", "SOME_STRING")

        Dim f As New frmMainReports
        f.reportPath = getParameters(1, "FTI_RPT_5.3.25.10.1_reportPath")
        f.reportParameters = reportParam

        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Dim reportParam As New Dictionary(Of String, String)
        'reportParam.Add("ReportParameter1", "SOME_STRING")

        Dim f As New frmMainReports
        f.reportPath = getParameters(1, "FTI_RPT_5.3.25.10.2_reportPath")
        f.reportParameters = reportParam

        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        Dim reportParam As New Dictionary(Of String, String)
        'reportParam.Add("ReportParameter1", "SOME_STRING")

        Dim f As New frmMainReports
        f.reportPath = getParameters(1, "FTI_RPT_5.3.25.10.3_reportPath")
        f.reportParameters = reportParam

        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub LinkLabel5_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        Dim reportParam As New Dictionary(Of String, String)
        'reportParam.Add("ReportParameter1", "SOME_STRING")

        Dim f As New frmMainReports
        f.reportPath = getParameters(1, "FTI_RPT_5.3.25.10.4_reportPath")
        f.reportParameters = reportParam

        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub
End Class