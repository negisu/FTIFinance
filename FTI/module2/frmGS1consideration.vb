Imports System.Linq.Expressions

Public Class frmGS1consideration

    Dim ds As DataSet

    Private Sub frmFTIconsideration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        ds = New DataSet

        ComboBox1.SelectedIndex = 0

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMB_MEMBER_ADVISOR(ByVal QTY As Integer, ByVal DATE_FROM As Date, ByVal DATE_TO As Date)
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", New DateTime(DATE_FROM.Year, DATE_FROM.Month, DATE_FROM.Day, 0, 0, 0))
        parameters.Add("@p1", New DateTime(DATE_TO.Year, DATE_TO.Month, DATE_TO.Day, 23, 59, 59))
        'parameters.Add("@p2", QTY)

        'Dim sFTI_MB_MEMBER_ADVISOR As String = getParameters(1, "FTI_MB_MEMBER_ADVISOR")

        Dim query As String = String.Empty
        query &= "SELECT        MB_MEMBER.MEMBER_MAIN_GROUP_CODE, MB_MEMBER.MEMBER_GROUP_CODE, MB_MEMBER.MEMBER_MAIN_TYPE_CODE, "
        query &= "                         MB_MEMBER.MEMBER_TYPE_CODE, MB_MEMBER_TYPE.MEMBER_TYPE_NAME, MB_MEMBER.ADVISOR_CODE, "
        query &= "                         MB_MEMBER_GROUP.MEMBER_GROUP_NAME, MB_MEMBER_GROUP.MEMBER_GROUP_RDLC_GROUP, COUNT(*) AS QTY, 0 AS QTY_VALUE, '' AS QTY_EXP "
        query &= "FROM            MB_MEMBER INNER JOIN "
        query &= "                         MB_MEMBER_TYPE ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_TYPE.MEMBER_MAIN_GROUP_CODE AND "
        query &= "                         MB_MEMBER.MEMBER_GROUP_CODE = MB_MEMBER_TYPE.MEMBER_GROUP_CODE AND "
        query &= "                         MB_MEMBER.MEMBER_MAIN_TYPE_CODE = MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE AND "
        query &= "                         MB_MEMBER.MEMBER_TYPE_CODE = MB_MEMBER_TYPE.MEMBER_TYPE_CODE LEFT OUTER JOIN "
        query &= "                         MB_MEMBER_GROUP ON MB_MEMBER.ADVISOR_CODE = MB_MEMBER_GROUP.MEMBER_GROUP_CODE "
        query &= "WHERE        (MB_MEMBER.MEMBER_MAIN_GROUP_CODE = '300') AND (MB_MEMBER.MEMBER_DATE BETWEEN @p0 AND @p1) "
        query &= "GROUP BY MB_MEMBER.MEMBER_MAIN_GROUP_CODE, MB_MEMBER.MEMBER_GROUP_CODE, MB_MEMBER.MEMBER_MAIN_TYPE_CODE, "
        query &= "                         MB_MEMBER.MEMBER_TYPE_CODE, MB_MEMBER_TYPE.MEMBER_TYPE_NAME, MB_MEMBER_GROUP.MEMBER_GROUP_NAME, "
        query &= "                         MB_MEMBER.ADVISOR_CODE, MB_MEMBER_GROUP.MEMBER_GROUP_RDLC_GROUP"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_ADVISOR").Copy

        If ds.Tables.Contains("MB_MEMBER_ADVISOR") = True Then
            ds.Tables("MB_MEMBER_ADVISOR").Clear()
            ds.Tables("MB_MEMBER_ADVISOR").Merge(dt)
            ds.Tables("MB_MEMBER_ADVISOR").AcceptChanges()
        Else
            ds.Tables.Add(dt)
            DataGridView1.DataSource = ds.Tables("MB_MEMBER_ADVISOR")
            DataGridView1.Columns("MEMBER_MAIN_GROUP_CODE").Width = 50
            DataGridView1.Columns("MEMBER_GROUP_CODE").Width = 50
            DataGridView1.Columns("MEMBER_MAIN_TYPE_CODE").Width = 50
            DataGridView1.Columns("MEMBER_TYPE_CODE").Width = 50

            DataGridView1.Columns("MEMBER_GROUP_RDLC_GROUP").Visible = False
            DataGridView1.Columns("QTY_EXP").Visible = False
        End If

        'Dim dv As New DataView(ds.Tables("MB_MEMBER_ADVISOR"))
        'Dim myRingCollection = From ds.Tables("MB_MEMBER_ADVISOR").GroupBy(Function(j) New With {Key j.category, Key j._date}).Select(Function(group) New With {Key .compKey = group.Key, Key .recs = group.ToList()})

        '============== COMPUTE ========================
        'Dim parameters As New Dictionary(Of String, Object)
        parameters.Clear()
        'parameters.Add("@p0", ComboBox5.SelectedValue.ToString)
        'parameters.Add("@p1", ComboBox2.SelectedValue.ToString)
        'parameters.Add("@p2", ComboBox3.SelectedValue.ToString)
        'parameters.Add("@p3", ComboBox4.SelectedValue.ToString)

        '==============================================
        Dim query2 As String = "SELECT * FROM MB_CONSIDERATION_COMPUTE_DETAIL "
        'query2 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) AND (ACTIVE = 1) "
        query2 &= "WHERE (ACTIVE = 1) "
        query2 &= "ORDER BY MEMBER_MAIN_GROUP_CODE, MEMBER_GROUP_CODE, MEMBER_MAIN_TYPE_CODE, MEMBER_TYPE_CODE, RATE_SEQ "

        Dim dt2 As DataTable = fillWebSQL(query2, parameters, "MB_CONSIDERATION_COMPUTE_DETAIL").Copy

        If ds.Tables.Contains("MB_CONSIDERATION_COMPUTE_DETAIL") = True Then
            ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL").Clear()
            ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL").Merge(dt2)
            ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL").AcceptChanges()
        Else
            ds.Tables.Add(dt2)
            dt2.PrimaryKey = New DataColumn() {dt2.Columns("MEMBER_MAIN_GROUP_CODE"), dt2.Columns("MEMBER_GROUP_CODE"), dt2.Columns("MEMBER_MAIN_TYPE_CODE"), dt2.Columns("MEMBER_TYPE_CODE"), dt2.Columns("RATE_SEQ")}
        End If
        '==============================================
        Dim idx As Integer = 0
        Dim i As Integer = 0
        Dim RULE_RATE As String = String.Empty
        Dim RULE_FORMULA As String = String.Empty
        Dim dv As New DataView(ds.Tables("MB_MEMBER_ADVISOR"))
        Try
            For i = 0 To ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL").Rows.Count - 1
                dv.RowFilter = String.Format("MEMBER_MAIN_GROUP_CODE='{0}' AND MEMBER_GROUP_CODE='{1}' AND MEMBER_MAIN_TYPE_CODE='{2}' AND MEMBER_TYPE_CODE='{3}'", ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL").Rows(i).Item("MEMBER_MAIN_GROUP_CODE").ToString, ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL").Rows(i).Item("MEMBER_GROUP_CODE").ToString, ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL").Rows(i).Item("MEMBER_MAIN_TYPE_CODE").ToString, ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL").Rows(i).Item("MEMBER_TYPE_CODE").ToString)
                'dv.Sort = ""
                Dim ro As DataTable = dv.ToTable

                If ro.Rows.Count > 0 Then
                    For idx = 0 To ro.Rows.Count - 1
                        RULE_RATE = ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL").Rows(i).Item("QUERY_VALUE").ToString
                        RULE_FORMULA = ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL").Rows(i).Item("QUERY").ToString & String.Format(" AND (ADVISOR_CODE = '{0}')", ds.Tables("MB_MEMBER_ADVISOR").Rows(idx).Item("ADVISOR_CODE").ToString)

                        'is it found?
                        If CInt(ro.Compute("COUNT(ADVISOR_CODE)", RULE_FORMULA)) > 0 Then
                            Dim colExp As DataColumn = New DataColumn
                            With colExp
                                .DataType = System.Type.GetType("System.Decimal")
                                .ColumnName = "EXP"
                                .Expression = RULE_RATE '"price + tax"
                            End With
                            ro.Columns.Add(colExp)

                            ds.Tables("MB_MEMBER_ADVISOR").Rows(idx).Item("QTY_VALUE") = CDec(ro.Compute("SUM(EXP)", RULE_FORMULA))
                            ds.Tables("MB_MEMBER_ADVISOR").Rows(idx).Item("QTY_EXP") = RULE_RATE
                            Exit For
                        End If
                    Next
                End If
            Next

            'MessageBox.Show("STEP 2 PROCESS HAS BEEN COMPLTED")
        Catch ex As Exception
            MessageBox.Show(RULE_RATE & vbCrLf & RULE_FORMULA & vbCrLf & ex.Message, "ERROR STEP=" & i & " " & idx)
            Exit Sub
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btFind.Click
        getMB_MEMBER_ADVISOR(CInt(NumericUpDown1.Value), DateTimePicker1.Value, DateTimePicker2.Value)


        'Dim f As New frmReportViewer
        ''Me.BindingSource1.DataSource = MyDataTable
        ''Dim rds As New ReportDataSource("DataSet1", Me.BindingSource1)
        'f.ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "/Reports/rdlctemp.rdlc"
        ''Me.ReportViewer1.LocalReport.DataSources.Clear()
        ''Me.ReportViewer1.LocalReport.DataSources.Add(rds)

        'f.WindowState = FormWindowState.Maximized
        'If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
        '    '
        'End If
        'f.Dispose()
        'f = Nothing
    End Sub

    Private Sub llRules_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llRules.LinkClicked
        Dim f As New frmGS1considerationRule2
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
        If ds.Tables("MB_MEMBER_ADVISOR").Rows.Count > 0 Then
            'print invoice
            Dim f As New frmMainReportViewer

            Select Case ComboBox1.SelectedIndex
                Case 0
                    f.ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "/Reports/module1/rdlc5.3.25.10.FTIconsideration1.rdlc"

                    f.ReportViewer1.LocalReport.DataSources.Clear()
                    'f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("dsFTInewApply", ds.Tables("MB_MEMBER")))
                    'f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("CourseInfo", Report1.Tables("CourseInfo")))
                    'f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("rptParam", Report1.Tables("rptParam")))

                    'ReportViewer1.PrinterSettings.PrinterName = DstTime1.config.Rows(ConfigIndx).Item("PrintReportHalfLetter").ToString
                    'ReportViewer1.PrinterSettings.DefaultPageSettings.PaperSize = GetSelectedPaperSize(DstTime1.config.Rows(ConfigIndx).Item("PrintReportHalfLetter").ToString, DstTime1.config.Rows(ConfigIndx).Item("PrintReportHalfLetterSize").ToString)
                    'ReportViewer1.PrinterSettings.DefaultPageSettings.PaperSource = GetSelectedPaperSource(DstTime1.config.Rows(ConfigIndx).Item("PrintReportHalfLetter").ToString, DstTime1.config.Rows(ConfigIndx).Item("PrintReportHalfLetterSource").ToString)
                    'ReportViewer1.PrinterSettings.DefaultPageSettings.Landscape = True

                    f.ReportViewer1.LocalReport.DataSources.Clear()
                    f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("dsConsideration1", ds.Tables("MB_MEMBER_ADVISOR")))

                    ''Create a report parameter for the sales order number 
                    'Dim PRICE As New ReportParameter()
                    'PRICE.Name = "PRICE"
                    'PRICE.Values.Add(FIRST_REGIST_RATE.ToString)

                    'Dim REGIST_CODE As New ReportParameter()
                    'REGIST_CODE.Name = "REGIST_CODE"
                    'REGIST_CODE.Values.Add(DataGridView1.CurrentRow.Cells("REGIST_CODE").Value.ToString)

                    'Dim COMP_PERSON_NAME_TH As New ReportParameter()
                    'COMP_PERSON_NAME_TH.Name = "COMP_PERSON_NAME_TH"
                    'COMP_PERSON_NAME_TH.Values.Add(DataGridView1.CurrentRow.Cells("COMP_PERSON_NAME_TH").Value.ToString)

                    'Dim COMP_PERSON_NAME_EN As New ReportParameter()
                    'COMP_PERSON_NAME_EN.Name = "COMP_PERSON_NAME_EN"
                    'COMP_PERSON_NAME_EN.Values.Add(DataGridView1.CurrentRow.Cells("COMP_PERSON_NAME_EN").Value.ToString)

                    'Dim REGIST_DATE As New ReportParameter()
                    'REGIST_DATE.Name = "REGIST_DATE"
                    'REGIST_DATE.Values.Add(DataGridView1.CurrentRow.Cells("REGIST_DATE").Value.ToString)

                    ''Set the report parameters for the report
                    'Dim params() As ReportParameter = {PRICE, REGIST_CODE, COMP_PERSON_NAME_TH, COMP_PERSON_NAME_EN, REGIST_DATE}
                    'f.ReportViewer1.LocalReport.SetParameters(params)
                Case Else
                    f.ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "/Reports/rdlctemp.rdlc"
            End Select

            f.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
            f.ReportViewer1.LocalReport.Refresh()

            f.WindowState = FormWindowState.Maximized
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                '
            End If
            f.Dispose()
            f = Nothing
        End If
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub
End Class