Imports Microsoft.Reporting.WinForms

Public Class frmFTIrepresentLabel

    Dim ds As DataSet
    Dim dv As DataView

    Private Sub frmFTIapproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        ds = New DataSet

        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT        MB_COMP_PERSON_ADDRESS.ADDR_POSTCODE, MB_COMP_PERSON_ADDRESS.ADDR_POSTCODE + ' (' + CONVERT(varchar(10), COUNT(*)) + ')' AS CNT "
        query &= "FROM            MB_MEMBER_TYPE INNER JOIN "
        query &= "                         MB_MEMBER INNER JOIN "
        query &= "                         MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
        query &= "                         MB_COMP_PERSON_ADDRESS ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON_ADDRESS.COMP_PERSON_CODE INNER JOIN "
        query &= "                         MB_PRENAME ON MB_COMP_PERSON.PREN_CODE = MB_PRENAME.PRENAME_CODE ON MB_MEMBER_TYPE.MEMBER_MAIN_GROUP_CODE = MB_MEMBER.MEMBER_MAIN_GROUP_CODE AND "
        query &= "                         MB_MEMBER_TYPE.MEMBER_GROUP_CODE = MB_MEMBER.MEMBER_GROUP_CODE AND MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE = MB_MEMBER.MEMBER_MAIN_TYPE_CODE AND "
        query &= "                         MB_MEMBER_TYPE.MEMBER_TYPE_CODE = MB_MEMBER.MEMBER_TYPE_CODE INNER JOIN "
        query &= "                         MB_MEMBER_GROUP ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_MAIN_GROUP_CODE AND "
        query &= "                         MB_MEMBER.MEMBER_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_GROUP_CODE "
        query &= "WHERE        (MB_COMP_PERSON_ADDRESS.ADDR_CODE = '001') AND (MB_PRENAME.PRENAME_TYPE = 2) AND (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN ('000', '999')) AND "
        query &= "                         (MB_MEMBER.MEMBER_STATUS_CODE IN ('A')) AND (MB_COMP_PERSON_ADDRESS.ADDR_POSTCODE IS NOT NULL) "
        query &= "GROUP BY MB_COMP_PERSON_ADDRESS.ADDR_POSTCODE "
        query &= "ORDER BY MB_COMP_PERSON_ADDRESS.ADDR_POSTCODE"

        Dim dt As DataTable = New DataTable
        Try
            dt = fillWebSQL(query, parameters, "ADDR_POSTCODE1")
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
        End Try

        If ds.Tables.Contains("ADDR_POSTCODE1") = True Then
            ds.Tables("ADDR_POSTCODE1").Clear()
            ds.Tables("ADDR_POSTCODE1").Merge(dt)
            ds.Tables("ADDR_POSTCODE1").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox1.DataSource = ds.Tables("ADDR_POSTCODE1")
        ComboBox1.DisplayMember = "CNT"
        ComboBox1.ValueMember = "ADDR_POSTCODE"

        ComboBox1.SelectedIndex = 0

        '========================================

        Dim dt2 As DataTable = dt.Copy
        dt2.TableName = "ADDR_POSTCODE2"

        ds.Tables.Add(dt2)

        ComboBox2.DataSource = ds.Tables("ADDR_POSTCODE2")
        ComboBox2.DisplayMember = "CNT"
        ComboBox2.ValueMember = "ADDR_POSTCODE"

        ComboBox2.SelectedIndex = ComboBox2.Items.Count - 1

        '========================================
        ' POPULATE THE COMBO BOX.
        For Each sPrinters In System.Drawing.Printing.PrinterSettings.InstalledPrinters
            'cmbPrinterList.Items.Add(sPrinters)
            ComboBox3.Items.Add(sPrinters)
        Next
        If ComboBox3.Items.Count > 0 Then ComboBox3.SelectedIndex = 0
        '========================================

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMB_MEMBER_RETIRE(ByVal SEARCH_TEXT As String, ByVal POSTCODE_FROM As String, ByVal POSTCODE_TO As String)
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        SEARCH_TEXT = "%" & SEARCH_TEXT.Replace(" ", "%") & "%"

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", SEARCH_TEXT)
        parameters.Add("@p1", POSTCODE_FROM)
        parameters.Add("@p2", POSTCODE_TO)
        'parameters.Add("@p2", "200")
        'parameters.Add("@p3", "999")
        'parameters.Add("@p1", "000")

        'Dim CONTACT_FIRST_NAME As New ReportParameter()
        'CONTACT_FIRST_NAME.Name = "CONTACT_FIRST_NAME"
        'CONTACT_FIRST_NAME.Values.Add(DataGridView1.CurrentRow.Cells("CONTACT_FIRST_NAME").Value.ToString)

        'Dim CONTACT_LAST_NAME As New ReportParameter()
        'CONTACT_LAST_NAME.Name = "CONTACT_LAST_NAME"
        'CONTACT_LAST_NAME.Values.Add(DataGridView1.CurrentRow.Cells("CONTACT_LAST_NAME").Value.ToString)

        'Dim COMP_PERSON_NAME As New ReportParameter()
        'COMP_PERSON_NAME.Name = "COMP_PERSON_NAME"
        'COMP_PERSON_NAME.Values.Add(DataGridView1.CurrentRow.Cells("COMP_PERSON_NAME").Value.ToString)

        Dim query As String = String.Empty
        'query &= "SELECT        MB_MEMBER.MEMBER_CODE, MB_PRENAME.PRENAME_TH, MB_COMP_PERSON.COMP_PERSON_NAME_TH, MB_COMP_PERSON_ADDRESS.ADDR_NO, MB_COMP_PERSON_ADDRESS.ADDR_MOO, "
        'query &= "                         MB_COMP_PERSON_ADDRESS.ADDR_SOI, MB_COMP_PERSON_ADDRESS.ADDR_ROAD, MB_COMP_PERSON_ADDRESS.ADDR_SUB_DISTRICT, MB_COMP_PERSON_ADDRESS.ADDR_DISTRICT, "
        'query &= "                         MB_COMP_PERSON_ADDRESS.ADDR_PROVINCE_NAME, MB_COMP_PERSON_ADDRESS.ADDR_POSTCODE, MB_COMP_PERSON_ADDRESS.ADDR_TELEPHONE, MB_COMP_PERSON_ADDRESS.ADDR_FAX, "
        'query &= "                         MB_COMP_PERSON_ADDRESS.ADDR_EMAIL, MB_COMP_PERSON_ADDRESS.ADDR_WEBSITE, MB_MEMBER_GROUP.MEMBER_GROUP_NAME "
        query &= "SELECT   ROW_NUMBER() OVER(ORDER BY MB_COMP_PERSON_ADDRESS.ADDR_POSTCODE, MB_MEMBER.PREFIX, MB_MEMBER.RUNNING, MB_MEMBER.MEMBER_MAIN_GROUP_CODE) % 2 AS REMAINDER,    MB_MEMBER.REGIST_CODE, MB_MEMBER.MEMBER_CODE, MB_PRENAME.PRENAME_TH, MB_COMP_PERSON.COMP_PERSON_NAME_TH, MB_COMP_PERSON_ADDRESS.ADDR_NO, MB_COMP_PERSON_ADDRESS.ADDR_MOO, "
        query &= "                         MB_COMP_PERSON_ADDRESS.ADDR_SOI, MB_COMP_PERSON_ADDRESS.ADDR_ROAD, MB_COMP_PERSON_ADDRESS.ADDR_SUB_DISTRICT, MB_COMP_PERSON_ADDRESS.ADDR_DISTRICT, "
        query &= "                         MB_COMP_PERSON_ADDRESS.ADDR_PROVINCE_NAME, MB_COMP_PERSON_ADDRESS.ADDR_POSTCODE, MB_COMP_PERSON_ADDRESS.ADDR_TELEPHONE, MB_COMP_PERSON_ADDRESS.ADDR_FAX, "
        query &= "                         MB_COMP_PERSON_ADDRESS.ADDR_EMAIL, MB_COMP_PERSON_ADDRESS.ADDR_WEBSITE, MB_MEMBER_GROUP.MEMBER_GROUP_NAME, dbo.get5_3_25_2(MB_MEMBER.REGIST_CODE, 1, 'TH') "
        query &= "                         AS CONTACT1_TH, dbo.get5_3_25_2(MB_MEMBER.REGIST_CODE, 1, 'EN') AS CONTACT1_EN, dbo.get5_3_25_2(MB_MEMBER.REGIST_CODE, 2, 'TH') AS CONTACT2_TH, "
        query &= "                         dbo.get5_3_25_2(MB_MEMBER.REGIST_CODE, 2, 'EN') AS CONTACT2_EN, dbo.get5_3_25_2(MB_MEMBER.REGIST_CODE, 3, 'TH') AS CONTACT3_TH, dbo.get5_3_25_2(MB_MEMBER.REGIST_CODE, 3, 'EN') "
        query &= "                         AS CONTACT3_EN, MB_COMP_PERSON.REGIST_CAPITAL, MB_COMP_PERSON.EMPLOYEE_AMOUNT, CAST(CASE WHEN MB_COMP_PERSON.asset_amount < 50000000 THEN 1 ELSE 0 END AS bit) AS ASSET1, "
        query &= "                         CAST(CASE WHEN asset_amount BETWEEN 50000000 AND 100000000 THEN 1 ELSE 0 END AS bit) AS ASSET2, CAST(CASE WHEN asset_amount BETWEEN 100000001 AND "
        query &= "                         200000000 THEN 1 ELSE 0 END AS bit) AS ASSET3, CAST(CASE WHEN MB_COMP_PERSON.asset_amount > 200000000 THEN 1 ELSE 0 END AS bit) AS ASSET4, "
        query &= "                         CAST(CASE WHEN MB_COMP_PERSON.electric_amount < 1175000 THEN 1 ELSE 0 END AS bit) AS ELECT1, CAST(CASE WHEN MB_COMP_PERSON.electric_amount > 1175000 THEN 1 ELSE 0 END AS bit) "
        query &= "                         AS ELECT2, MB_COMP_PERSON.PRODUCE_TECHNOLOGY_TH, MB_COMP_PERSON.PRODUCE_TECHNOLOGY_EN, MB_COMP_PERSON.PRODUCE_TECHNOLOGY_DESC, "
        query &= "                         MB_COMP_PERSON.BUS_TYPE_RELATE, MB_COMP_PERSON.BUS_TYPE_DEALER, MB_COMP_PERSON.BUS_TYPE_IMPORTER, MB_COMP_PERSON.BUS_TYPE_EXPORTER, "
        query &= "                         MB_COMP_PERSON.BUS_TYPE_OTHER "
        query &= "FROM            MB_MEMBER_TYPE INNER JOIN "
        query &= "                         MB_MEMBER INNER JOIN "
        query &= "                         MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
        query &= "                         MB_COMP_PERSON_ADDRESS ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON_ADDRESS.COMP_PERSON_CODE INNER JOIN "
        query &= "                         MB_PRENAME ON MB_COMP_PERSON.PREN_CODE = MB_PRENAME.PRENAME_CODE ON MB_MEMBER_TYPE.MEMBER_MAIN_GROUP_CODE = MB_MEMBER.MEMBER_MAIN_GROUP_CODE AND "
        query &= "                         MB_MEMBER_TYPE.MEMBER_GROUP_CODE = MB_MEMBER.MEMBER_GROUP_CODE AND MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE = MB_MEMBER.MEMBER_MAIN_TYPE_CODE AND "
        query &= "                         MB_MEMBER_TYPE.MEMBER_TYPE_CODE = MB_MEMBER.MEMBER_TYPE_CODE INNER JOIN "
        query &= "                         MB_MEMBER_GROUP ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_MAIN_GROUP_CODE AND "
        query &= "                         MB_MEMBER.MEMBER_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_GROUP_CODE "
        query &= "WHERE        (MB_COMP_PERSON_ADDRESS.ADDR_CODE = '001') AND (MB_PRENAME.PRENAME_TYPE = 2) AND (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN ('000', '999')) AND "
        query &= "                         (MB_MEMBER.MEMBER_STATUS_CODE IN ('A')) AND ((MB_MEMBER.MEMBER_CODE LIKE @p0) OR (MB_COMP_PERSON.COMP_PERSON_NAME_TH LIKE @p0)) AND (MB_COMP_PERSON_ADDRESS.ADDR_POSTCODE BETWEEN @p1 AND @p2) "
        query &= "ORDER BY MB_COMP_PERSON_ADDRESS.ADDR_POSTCODE, MB_MEMBER.PREFIX, MB_MEMBER.RUNNING, MB_MEMBER.MEMBER_MAIN_GROUP_CODE"

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

            ds.Tables("MB_MEMBER").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER").Columns("REGIST_CODE")}

            DataGridView1.DataSource = dv

            For i As Integer = 0 To DataGridView1.ColumnCount - 1
                DataGridView1.Columns(i).Visible = False
            Next

            DataGridView1.Columns("MEMBER_CODE").Visible = True
            DataGridView1.Columns("PRENAME_TH").Visible = True
            DataGridView1.Columns("COMP_PERSON_NAME_TH").Visible = True
            DataGridView1.Columns("ADDR_POSTCODE").Visible = True
            DataGridView1.Columns("REGIST_CODE").Visible = True
            'DataGridView1.Columns("Remainder").Visible = True
            'DataGridView1.Columns("MEMBER_STATUS_CODE").Visible = True

            DataGridView1.Columns("COMP_PERSON_NAME_TH").Width = 200
        End If

        Label3.Text = String.Format("พบ {0} รายการ", ds.Tables("MB_MEMBER").Rows.Count.ToString("#,##0"))
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btPrintPreview.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim reportParameters As New Dictionary(Of String, String)

            'Dim dt As DataTable = ds.Tables("MB_MEMBER").Clone
            Dim REGIST_CODE As String = String.Empty
            For i As Integer = 0 To DataGridView1.SelectedRows.Count - 1
                'Dim r As DataRow = ds.Tables("MB_MEMBER").Rows.Find(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value)

                'If r IsNot Nothing Then dt.ImportRow(r)

                If REGIST_CODE.Length > 0 Then
                    REGIST_CODE &= ",'" & DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString & "'"
                Else
                    REGIST_CODE &= "'" & DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString & "'"
                End If
            Next

            reportParameters.Add("REGIST_CODE", REGIST_CODE)

            Dim f As New frmMainReports
            f.reportPath = getParameters(1, "FTI_RPT_5.3.25.04_reportPath")
            f.reportParameters = reportParameters

            'f.ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "/Reports/module1/rdlc5.3.25.04.FTIrepresentLabel.rdlc"

            'f.ReportViewer1.LocalReport.DataSources.Clear()
            'f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("ds", dt))

            'f.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            'f.ReportViewer1.ZoomMode = ZoomMode.Percent
            'f.ReportViewer1.ZoomPercent = 100
            'f.ReportViewer1.LocalReport.Refresh()

            'f.WindowState = FormWindowState.Maximized
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

    Private Sub btFind_Click(sender As Object, e As EventArgs) Handles btFind.Click
        getMB_MEMBER_RETIRE(TextBox1.Text, ComboBox1.SelectedValue.ToString, ComboBox2.SelectedValue.ToString)
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If DataGridView1.RowCount > 0 Then
            For i As Integer = 0 To DataGridView1.RowCount - 1
                DataGridView1.Rows(i).Selected = True
            Next
        End If
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        If DataGridView1.RowCount > 0 Then
            For i As Integer = 0 To DataGridView1.RowCount - 1
                DataGridView1.Rows(i).Selected = False
            Next
        End If
    End Sub

    Private Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim reportParameters As New Dictionary(Of String, String)

            'Dim dt As DataTable = ds.Tables("MB_MEMBER").Clone
            Dim REGIST_CODE As String = String.Empty
            For i As Integer = 0 To DataGridView1.SelectedRows.Count - 1
                'Dim r As DataRow = ds.Tables("MB_MEMBER").Rows.Find(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value)

                'If r IsNot Nothing Then dt.ImportRow(r)

                If REGIST_CODE.Length > 0 Then
                    REGIST_CODE &= ",'" & DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString & "'"
                Else
                    REGIST_CODE &= "'" & DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString & "'"
                End If
            Next

            reportParameters.Add("REGIST_CODE", REGIST_CODE)

            'Dim f As New frmMainReports
            'f.reportPath = getParameters(1, "FTI_RPT_5.3.25.04_reportPath")
            'f.reportParameters = reportParameters

            Dim sFTI_RPT_DEVICEINFO As String = getParameters(1, "FTI_RPT_5.3.25.04_DEVICEINFO")

            Dim report As New ServerReport()
            Dim ReportServerUrl As String = getParameters(0, "ReportServerUrl")
            report.ReportServerUrl = New Uri(ReportServerUrl)
            'report.ReportPath = System.Environment.CurrentDirectory & "/Reports/module1/rdlc5.3.25.02.FTIrepresentConfirmation.rdlc"
            report.ReportPath = getParameters(0, "ReportPathPrefix") & getParameters(1, "FTI_RPT_5.3.25.04_reportPath") & getParameters(0, "ReportPathSuffix")

            '=================================================================
            'Get a reference to the default credentials
            Dim credentials As System.Net.ICredentials
            'credentials = New System.Net.NetworkCredential("demo", "P@$$w0rd", "FTI") 'System.Net.CredentialCache.DefaultCredentials
            credentials = New System.Net.NetworkCredential(getParameters(0, "ReportServerCredentialsUser"), getParameters(0, "ReportServerCredentialsPassword"), getParameters(0, "ReportServerCredentialsDomain")) 'System.Net.CredentialCache.DefaultCredentials

            'Get a reference to the report server credentials
            Dim rsCredentials As ReportServerCredentials
            rsCredentials = report.ReportServerCredentials

            'Set the credentials for the server report
            rsCredentials.NetworkCredentials = credentials
            '=================================================================

            'Dim report As New LocalReport()
            'report.ReportPath = System.Environment.CurrentDirectory & "/Reports/module1/rdlc5.3.25.04.FTIrepresentLabel.rdlc"

            Try
                If reportParameters IsNot Nothing Then
                    If reportParameters.Count > 0 Then
                        Dim paramList As New Generic.List(Of ReportParameter)
                        'Dim pair As KeyValuePair(Of String, String)
                        For Each pair As KeyValuePair(Of String, String) In reportParameters
                            'if you have report parameters - add them here
                            paramList.Add(New ReportParameter(pair.Key, pair.Value, True))
                        Next

                        report.SetParameters(paramList)
                    End If
                End If

                'serverReport.SetParameters(parameters)
            Catch ex As Exception
                MessageBox.Show("ReportServerUrl=" & ReportServerUrl & vbCrLf & "ReportPath=" & report.ReportPath & vbCrLf & ex.Message)
            End Try

            'report.SetParameters(params)
            'report.DataSources.Add(New ReportDataSource("ds", dt))
            RDLCExport(report)
            RDLCPrint(ComboBox3.Text)
        End If
    End Sub
End Class