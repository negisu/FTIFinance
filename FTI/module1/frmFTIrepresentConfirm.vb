Imports Microsoft.Reporting.WinForms

Public Class frmFTIrepresentConfirm

    Dim ds As DataSet
    Dim dv As DataView

    Private Sub frmFTIapproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        query &= "SELECT        MB_MEMBER.REGIST_CODE, MB_MEMBER.MEMBER_CODE, MB_PRENAME.PRENAME_TH, MB_COMP_PERSON.COMP_PERSON_NAME_TH, MB_COMP_PERSON_ADDRESS.ADDR_NO, MB_COMP_PERSON_ADDRESS.ADDR_MOO, "
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
            'DataGridView1.Columns("RETIRE_REASON").Visible = True
            'DataGridView1.Columns("MEMBER_STATUS_CODE").Visible = True

            DataGridView1.Columns("COMP_PERSON_NAME_TH").Width = 200
        End If

        Label3.Text = String.Format("พบ {0} รายการ", ds.Tables("MB_MEMBER").Rows.Count.ToString("#,##0"))
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btPrintPreview.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim parameters As New Dictionary(Of String, Object)
            Dim query As String = String.Empty
            query &= "SELECT        MB_MEMBER.MEMBER_CODE, MB_PRENAME.PRENAME_TH, MB_COMP_PERSON.COMP_PERSON_NAME_TH, MB_COMP_PERSON_ADDRESS.ADDR_NO, MB_COMP_PERSON_ADDRESS.ADDR_MOO, "
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
            query &= "WHERE        (MB_COMP_PERSON_ADDRESS.ADDR_CODE = '001') AND (MB_PRENAME.PRENAME_TYPE = 2) AND (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN ('000', '100', '200', '999')) AND "
            query &= "                         (MB_MEMBER.MEMBER_CODE = @p0) "
            query &= "ORDER BY MB_COMP_PERSON_ADDRESS.ADDR_POSTCODE, MB_MEMBER.PREFIX, MB_MEMBER.RUNNING, MB_MEMBER.MEMBER_MAIN_GROUP_CODE"

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim i As Integer = 0

            parameters.Clear()
            parameters.Add("@p0", DataGridView1.SelectedRows(i).Cells("MEMBER_CODE").Value)

            Dim dt As DataTable = New DataTable
            Try
                dt = fillWebSQL(query, parameters, "MB_MEMBER")
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
            End Try

            Dim reportParameters As New Dictionary(Of String, String)
            'reportParameters.Add("DATE_FROM", "TEST IT!!!")

            reportParameters.Clear()
            'parameters.Add("@p0", DataGridView1.SelectedRows(i).Cells("MEMBER_CODE").Value)

            'Dim dt As DataTable = New DataTable
            'Try
            '    dt = fillWebSQL(query, parameters, "MB_MEMBER")
            'Catch ex As Exception
            '    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
            'End Try

            reportParameters.Add("MEMBER_CODE", DataGridView1.CurrentRow.Cells("MEMBER_CODE").Value.ToString)
            reportParameters.Add("ADDR_NO", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_NO").Value), "", DataGridView1.CurrentRow.Cells("ADDR_NO").Value.ToString))
            reportParameters.Add("ADDR_MOO", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_MOO").Value), "", DataGridView1.CurrentRow.Cells("ADDR_MOO").Value.ToString))
            reportParameters.Add("ADDR_SOI", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_SOI").Value), "", DataGridView1.CurrentRow.Cells("ADDR_SOI").Value.ToString))
            reportParameters.Add("ADDR_ROAD", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_ROAD").Value), "", DataGridView1.CurrentRow.Cells("ADDR_ROAD").Value.ToString))
            reportParameters.Add("ADDR_SUB_DISTRICT", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_SUB_DISTRICT").Value), "", DataGridView1.CurrentRow.Cells("ADDR_SUB_DISTRICT").Value.ToString))
            reportParameters.Add("ADDR_DISTRICT", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_DISTRICT").Value), "", DataGridView1.CurrentRow.Cells("ADDR_DISTRICT").Value.ToString))
            reportParameters.Add("ADDR_PROVINCE_NAME", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_PROVINCE_NAME").Value), "", DataGridView1.CurrentRow.Cells("ADDR_PROVINCE_NAME").Value.ToString))
            reportParameters.Add("ADDR_POSTCODE", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_POSTCODE").Value), "", DataGridView1.CurrentRow.Cells("ADDR_POSTCODE").Value.ToString))
            reportParameters.Add("PRENAME_TH", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("PRENAME_TH").Value), "", DataGridView1.SelectedRows(i).Cells("PRENAME_TH").Value.ToString))
            reportParameters.Add("COMP_PERSON_NAME_TH", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("COMP_PERSON_NAME_TH").Value), "", DataGridView1.SelectedRows(i).Cells("COMP_PERSON_NAME_TH").Value.ToString))
            reportParameters.Add("REGIST_CAPITAL", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("REGIST_CAPITAL").Value), Nothing, DataGridView1.SelectedRows(i).Cells("REGIST_CAPITAL").Value.ToString))
            reportParameters.Add("EMPLOYEE_AMOUNT", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("EMPLOYEE_AMOUNT").Value), Nothing, DataGridView1.SelectedRows(i).Cells("EMPLOYEE_AMOUNT").Value.ToString))
            reportParameters.Add("ASSET1", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ASSET1").Value), "false", DataGridView1.SelectedRows(i).Cells("ASSET1").Value.ToString))
            reportParameters.Add("ASSET2", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ASSET2").Value), "false", DataGridView1.SelectedRows(i).Cells("ASSET2").Value.ToString))
            reportParameters.Add("ASSET3", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ASSET3").Value), "false", DataGridView1.SelectedRows(i).Cells("ASSET3").Value.ToString))
            reportParameters.Add("ASSET4", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ASSET4").Value), "false", DataGridView1.SelectedRows(i).Cells("ASSET4").Value.ToString))
            reportParameters.Add("ELECT1", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ELECT1").Value), "false", DataGridView1.SelectedRows(i).Cells("ELECT1").Value.ToString))
            reportParameters.Add("ELECT2", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ELECT2").Value), "false", DataGridView1.SelectedRows(i).Cells("ELECT2").Value.ToString))
            reportParameters.Add("PRODUCE_TECHNOLOGY_TH", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_TH").Value), "false", DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_TH").Value.ToString))
            reportParameters.Add("PRODUCE_TECHNOLOGY_EN", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_EN").Value), "false", DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_EN").Value.ToString))
            reportParameters.Add("PRODUCE_TECHNOLOGY_DESC", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_DESC").Value), "", DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_DESC").Value.ToString))
            reportParameters.Add("BUS_TYPE_RELATE", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_RELATE").Value), "false", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_RELATE").Value.ToString.ToLower))
            reportParameters.Add("BUS_TYPE_DEALER", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_DEALER").Value), "false", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_DEALER").Value.ToString.ToLower))
            reportParameters.Add("BUS_TYPE_IMPORTER", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_IMPORTER").Value), "false", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_IMPORTER").Value.ToString.ToLower))
            reportParameters.Add("BUS_TYPE_EXPORTER", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_EXPORTER").Value), "false", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_EXPORTER").Value.ToString.ToLower))
            reportParameters.Add("BUS_TYPE_OTHER", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_OTHER").Value), "", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_OTHER").Value.ToString))

            Dim f As New frmMainReports
            f.reportPath = getParameters(1, "FTI_RPT_5.3.25.02_reportPath")
            f.reportParameters = reportParameters
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                'do nothing
            End If
            f.Dispose()
            f = Nothing

            'Dim f As New frmMainReportViewer
            'f.ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "/Reports/module1/rdlc5.3.25.02.FTIrepresentConfirmation.rdlc"

            ''Create a report parameter for the sales order number 
            'Dim MEMBER_CODE As New ReportParameter()
            'MEMBER_CODE.Name = "MEMBER_CODE"
            'MEMBER_CODE.Values.Add(DataGridView1.SelectedRows(i).Cells("MEMBER_CODE").Value.ToString)

            'Dim ADDR_NO As New ReportParameter()
            'ADDR_NO.Name = "ADDR_NO"
            'ADDR_NO.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ADDR_NO").Value), "", DataGridView1.SelectedRows(i).Cells("ADDR_NO").Value.ToString))

            'Dim ADDR_MOO As New ReportParameter()
            'ADDR_MOO.Name = "ADDR_MOO"
            'ADDR_MOO.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ADDR_MOO").Value), "", DataGridView1.SelectedRows(i).Cells("ADDR_MOO").Value.ToString))

            'Dim ADDR_SOI As New ReportParameter()
            'ADDR_SOI.Name = "ADDR_SOI"
            'ADDR_SOI.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ADDR_SOI").Value), "", DataGridView1.SelectedRows(i).Cells("ADDR_SOI").Value.ToString))

            'Dim ADDR_ROAD As New ReportParameter()
            'ADDR_ROAD.Name = "ADDR_ROAD"
            'ADDR_ROAD.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ADDR_ROAD").Value), "", DataGridView1.SelectedRows(i).Cells("ADDR_ROAD").Value.ToString))

            'Dim ADDR_SUB_DISTRICT As New ReportParameter()
            'ADDR_SUB_DISTRICT.Name = "ADDR_SUB_DISTRICT"
            'ADDR_SUB_DISTRICT.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ADDR_SUB_DISTRICT").Value), "", DataGridView1.SelectedRows(i).Cells("ADDR_SUB_DISTRICT").Value.ToString))

            'Dim ADDR_DISTRICT As New ReportParameter()
            'ADDR_DISTRICT.Name = "ADDR_DISTRICT"
            'ADDR_DISTRICT.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ADDR_DISTRICT").Value), "", DataGridView1.SelectedRows(i).Cells("ADDR_DISTRICT").Value.ToString))

            'Dim ADDR_PROVINCE_NAME As New ReportParameter()
            'ADDR_PROVINCE_NAME.Name = "ADDR_PROVINCE_NAME"
            'ADDR_PROVINCE_NAME.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ADDR_PROVINCE_NAME").Value), "", DataGridView1.SelectedRows(i).Cells("ADDR_PROVINCE_NAME").Value.ToString))

            'Dim ADDR_POSTCODE As New ReportParameter()
            'ADDR_POSTCODE.Name = "ADDR_POSTCODE"
            'ADDR_POSTCODE.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ADDR_POSTCODE").Value), "", DataGridView1.SelectedRows(i).Cells("ADDR_POSTCODE").Value.ToString))

            'Dim PRENAME_TH As New ReportParameter()
            'PRENAME_TH.Name = "PRENAME_TH"
            'PRENAME_TH.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("PRENAME_TH").Value), "", DataGridView1.SelectedRows(i).Cells("PRENAME_TH").Value.ToString))

            'Dim COMP_PERSON_NAME_TH As New ReportParameter()
            'COMP_PERSON_NAME_TH.Name = "COMP_PERSON_NAME_TH"
            'COMP_PERSON_NAME_TH.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("COMP_PERSON_NAME_TH").Value), "", DataGridView1.SelectedRows(i).Cells("COMP_PERSON_NAME_TH").Value.ToString))

            'Dim REGIST_CAPITAL As New ReportParameter()
            'REGIST_CAPITAL.Name = "REGIST_CAPITAL"
            'REGIST_CAPITAL.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("REGIST_CAPITAL").Value), Nothing, DataGridView1.SelectedRows(i).Cells("REGIST_CAPITAL").Value.ToString))

            'Dim EMPLOYEE_AMOUNT As New ReportParameter()
            'EMPLOYEE_AMOUNT.Name = "EMPLOYEE_AMOUNT"
            'EMPLOYEE_AMOUNT.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("EMPLOYEE_AMOUNT").Value), Nothing, DataGridView1.SelectedRows(i).Cells("EMPLOYEE_AMOUNT").Value.ToString))

            'Dim ASSET1 As New ReportParameter()
            'ASSET1.Name = "ASSET1"
            'ASSET1.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ASSET1").Value), "false", DataGridView1.SelectedRows(i).Cells("ASSET1").Value.ToString))

            'Dim ASSET2 As New ReportParameter()
            'ASSET2.Name = "ASSET2"
            'ASSET2.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ASSET2").Value), "false", DataGridView1.SelectedRows(i).Cells("ASSET2").Value.ToString))

            'Dim ASSET3 As New ReportParameter()
            'ASSET3.Name = "ASSET3"
            'ASSET3.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ASSET3").Value), "false", DataGridView1.SelectedRows(i).Cells("ASSET3").Value.ToString))

            'Dim ASSET4 As New ReportParameter()
            'ASSET4.Name = "ASSET4"
            'ASSET4.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ASSET4").Value), "false", DataGridView1.SelectedRows(i).Cells("ASSET4").Value.ToString))

            ''MessageBox.Show(DataGridView1.SelectedRows(i).Cells("ASSET1").Value.ToString & DataGridView1.SelectedRows(i).Cells("ASSET2").Value.ToString & DataGridView1.SelectedRows(i).Cells("ASSET3").Value.ToString & DataGridView1.SelectedRows(i).Cells("ASSET4").Value.ToString)

            'Dim ELECT1 As New ReportParameter()
            'ELECT1.Name = "ELECT1"
            'ELECT1.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ELECT1").Value), "false", DataGridView1.SelectedRows(i).Cells("ELECT1").Value.ToString))

            'Dim ELECT2 As New ReportParameter()
            'ELECT2.Name = "ELECT2"
            'ELECT2.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ELECT2").Value), "false", DataGridView1.SelectedRows(i).Cells("ELECT2").Value.ToString))

            'Dim PRODUCE_TECHNOLOGY_TH As New ReportParameter()
            'PRODUCE_TECHNOLOGY_TH.Name = "PRODUCE_TECHNOLOGY_TH"
            'PRODUCE_TECHNOLOGY_TH.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_TH").Value), "false", DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_TH").Value.ToString))

            'Dim PRODUCE_TECHNOLOGY_EN As New ReportParameter()
            'PRODUCE_TECHNOLOGY_EN.Name = "PRODUCE_TECHNOLOGY_EN"
            'PRODUCE_TECHNOLOGY_EN.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_EN").Value), "false", DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_EN").Value.ToString))

            'Dim PRODUCE_TECHNOLOGY_DESC As New ReportParameter()
            'PRODUCE_TECHNOLOGY_DESC.Name = "PRODUCE_TECHNOLOGY_DESC"
            'PRODUCE_TECHNOLOGY_DESC.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_DESC").Value), "", DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_DESC").Value.ToString))

            'Dim BUS_TYPE_RELATE As New ReportParameter()
            'BUS_TYPE_RELATE.Name = "BUS_TYPE_RELATE"
            'BUS_TYPE_RELATE.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_RELATE").Value), "false", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_RELATE").Value.ToString.ToLower))

            'Dim BUS_TYPE_DEALER As New ReportParameter()
            'BUS_TYPE_DEALER.Name = "BUS_TYPE_DEALER"
            'BUS_TYPE_DEALER.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_DEALER").Value), "false", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_DEALER").Value.ToString.ToLower))

            'Dim BUS_TYPE_IMPORTER As New ReportParameter()
            'BUS_TYPE_IMPORTER.Name = "BUS_TYPE_IMPORTER"
            'BUS_TYPE_IMPORTER.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_IMPORTER").Value), "false", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_IMPORTER").Value.ToString.ToLower))

            'Dim BUS_TYPE_EXPORTER As New ReportParameter()
            'BUS_TYPE_EXPORTER.Name = "BUS_TYPE_EXPORTER"
            'BUS_TYPE_EXPORTER.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_EXPORTER").Value), "false", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_EXPORTER").Value.ToString.ToLower))

            'Dim BUS_TYPE_OTHER As New ReportParameter()
            'BUS_TYPE_OTHER.Name = "BUS_TYPE_OTHER"
            'BUS_TYPE_OTHER.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_OTHER").Value), "", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_OTHER").Value.ToString))

            'MessageBox.Show(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_RELATE").Value.ToString)

            'Set the report parameters for the report
            'Dim params() As ReportParameter = {MEMBER_CODE, ADDR_NO, ADDR_MOO, ADDR_SOI, ADDR_ROAD, ADDR_SUB_DISTRICT, ADDR_DISTRICT, ADDR_PROVINCE_NAME, ADDR_POSTCODE, PRENAME_TH, COMP_PERSON_NAME_TH, REGIST_CAPITAL, EMPLOYEE_AMOUNT, ASSET1, ASSET2, ASSET3, ASSET4, ELECT1, ELECT2, PRODUCE_TECHNOLOGY_TH, PRODUCE_TECHNOLOGY_EN, PRODUCE_TECHNOLOGY_DESC, BUS_TYPE_RELATE, BUS_TYPE_DEALER, BUS_TYPE_IMPORTER, BUS_TYPE_EXPORTER, BUS_TYPE_OTHER}
            'Dim params() As ReportParameter = {MEMBER_CODE, ADDR_NO, ADDR_MOO, ADDR_SOI, ADDR_ROAD, ADDR_SUB_DISTRICT, ADDR_DISTRICT, ADDR_PROVINCE_NAME, ADDR_POSTCODE, PRENAME_TH, COMP_PERSON_NAME_TH, REGIST_CAPITAL, EMPLOYEE_AMOUNT, ASSET1, ASSET2, ASSET3, ASSET4}
            'f.ReportViewer1.LocalReport.SetParameters(params)

            'f.ReportViewer1.LocalReport.DataSources.Clear()
            'f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("ds", dt))

            'f.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            'f.ReportViewer1.ZoomMode = ZoomMode.Percent
            'f.ReportViewer1.ZoomPercent = 100
            'f.ReportViewer1.LocalReport.Refresh()

            'f.WindowState = FormWindowState.Maximized
            'If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
            'End If
            'f.Dispose()
            'f = Nothing
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
            'Dim parameters As New Dictionary(Of String, Object)
            'Dim query As String = String.Empty
            'query &= "SELECT        MB_MEMBER.MEMBER_CODE, MB_PRENAME.PRENAME_TH, MB_COMP_PERSON.COMP_PERSON_NAME_TH, MB_COMP_PERSON_ADDRESS.ADDR_NO, MB_COMP_PERSON_ADDRESS.ADDR_MOO, "
            'query &= "                         MB_COMP_PERSON_ADDRESS.ADDR_SOI, MB_COMP_PERSON_ADDRESS.ADDR_ROAD, MB_COMP_PERSON_ADDRESS.ADDR_SUB_DISTRICT, MB_COMP_PERSON_ADDRESS.ADDR_DISTRICT, "
            'query &= "                         MB_COMP_PERSON_ADDRESS.ADDR_PROVINCE_NAME, MB_COMP_PERSON_ADDRESS.ADDR_POSTCODE, MB_COMP_PERSON_ADDRESS.ADDR_TELEPHONE, MB_COMP_PERSON_ADDRESS.ADDR_FAX, "
            'query &= "                         MB_COMP_PERSON_ADDRESS.ADDR_EMAIL, MB_COMP_PERSON_ADDRESS.ADDR_WEBSITE, MB_MEMBER_GROUP.MEMBER_GROUP_NAME, dbo.get5_3_25_2(MB_MEMBER.REGIST_CODE, 1, 'TH') "
            'query &= "                         AS CONTACT1_TH, dbo.get5_3_25_2(MB_MEMBER.REGIST_CODE, 1, 'EN') AS CONTACT1_EN, dbo.get5_3_25_2(MB_MEMBER.REGIST_CODE, 2, 'TH') AS CONTACT2_TH, "
            'query &= "                         dbo.get5_3_25_2(MB_MEMBER.REGIST_CODE, 2, 'EN') AS CONTACT2_EN, dbo.get5_3_25_2(MB_MEMBER.REGIST_CODE, 3, 'TH') AS CONTACT3_TH, dbo.get5_3_25_2(MB_MEMBER.REGIST_CODE, 3, 'EN') "
            'query &= "                         AS CONTACT3_EN, MB_COMP_PERSON.REGIST_CAPITAL, MB_COMP_PERSON.EMPLOYEE_AMOUNT, CAST(CASE WHEN MB_COMP_PERSON.asset_amount < 50000000 THEN 1 ELSE 0 END AS bit) AS ASSET1, "
            'query &= "                         CAST(CASE WHEN asset_amount BETWEEN 50000000 AND 100000000 THEN 1 ELSE 0 END AS bit) AS ASSET2, CAST(CASE WHEN asset_amount BETWEEN 100000001 AND "
            'query &= "                         200000000 THEN 1 ELSE 0 END AS bit) AS ASSET3, CAST(CASE WHEN MB_COMP_PERSON.asset_amount > 200000000 THEN 1 ELSE 0 END AS bit) AS ASSET4, "
            'query &= "                         CAST(CASE WHEN MB_COMP_PERSON.electric_amount < 1175000 THEN 1 ELSE 0 END AS bit) AS ELECT1, CAST(CASE WHEN MB_COMP_PERSON.electric_amount > 1175000 THEN 1 ELSE 0 END AS bit) "
            'query &= "                         AS ELECT2, MB_COMP_PERSON.PRODUCE_TECHNOLOGY_TH, MB_COMP_PERSON.PRODUCE_TECHNOLOGY_EN, MB_COMP_PERSON.PRODUCE_TECHNOLOGY_DESC, "
            'query &= "                         MB_COMP_PERSON.BUS_TYPE_RELATE, MB_COMP_PERSON.BUS_TYPE_DEALER, MB_COMP_PERSON.BUS_TYPE_IMPORTER, MB_COMP_PERSON.BUS_TYPE_EXPORTER, "
            'query &= "                         MB_COMP_PERSON.BUS_TYPE_OTHER "
            'query &= "FROM            MB_MEMBER_TYPE INNER JOIN "
            'query &= "                         MB_MEMBER INNER JOIN "
            'query &= "                         MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
            'query &= "                         MB_COMP_PERSON_ADDRESS ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON_ADDRESS.COMP_PERSON_CODE INNER JOIN "
            'query &= "                         MB_PRENAME ON MB_COMP_PERSON.PREN_CODE = MB_PRENAME.PRENAME_CODE ON MB_MEMBER_TYPE.MEMBER_MAIN_GROUP_CODE = MB_MEMBER.MEMBER_MAIN_GROUP_CODE AND "
            'query &= "                         MB_MEMBER_TYPE.MEMBER_GROUP_CODE = MB_MEMBER.MEMBER_GROUP_CODE AND MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE = MB_MEMBER.MEMBER_MAIN_TYPE_CODE AND "
            'query &= "                         MB_MEMBER_TYPE.MEMBER_TYPE_CODE = MB_MEMBER.MEMBER_TYPE_CODE INNER JOIN "
            'query &= "                         MB_MEMBER_GROUP ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_MAIN_GROUP_CODE AND "
            'query &= "                         MB_MEMBER.MEMBER_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_GROUP_CODE "
            'query &= "WHERE        (MB_COMP_PERSON_ADDRESS.ADDR_CODE = '001') AND (MB_PRENAME.PRENAME_TYPE = 2) AND (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN ('000', '100', '200', '999')) AND "
            'query &= "                         (MB_MEMBER.MEMBER_CODE = @p0) "
            'query &= "ORDER BY MB_COMP_PERSON_ADDRESS.ADDR_POSTCODE, MB_MEMBER.PREFIX, MB_MEMBER.RUNNING, MB_MEMBER.MEMBER_MAIN_GROUP_CODE"

            Dim reportParameters As New Dictionary(Of String, String)

            Dim sFTI_RPT_DEVICEINFO As String = getParameters(1, "FTI_RPT_5.3.25.02_DEVICEINFO")

            Dim report As New ServerReport()
            Dim ReportServerUrl As String = getParameters(0, "ReportServerUrl")
            report.ReportServerUrl = New Uri(ReportServerUrl)
            'report.ReportPath = System.Environment.CurrentDirectory & "/Reports/module1/rdlc5.3.25.02.FTIrepresentConfirmation.rdlc"
            report.ReportPath = getParameters(0, "ReportPathPrefix") & getParameters(1, "FTI_RPT_5.3.25.02_reportPath") & getParameters(0, "ReportPathSuffix")

            'getParameters(0, "ReportPathPrefix") & reportPath & getParameters(0, "ReportPathSuffix")

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

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            For i As Integer = 0 To DataGridView1.SelectedRows.Count - 1
                reportParameters.Clear()
                'parameters.Add("@p0", DataGridView1.SelectedRows(i).Cells("MEMBER_CODE").Value)

                'Dim dt As DataTable = New DataTable
                'Try
                '    dt = fillWebSQL(query, parameters, "MB_MEMBER")
                'Catch ex As Exception
                '    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
                'End Try

                reportParameters.Add("MEMBER_CODE", DataGridView1.CurrentRow.Cells("MEMBER_CODE").Value.ToString)
                reportParameters.Add("ADDR_NO", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_NO").Value), "", DataGridView1.CurrentRow.Cells("ADDR_NO").Value.ToString))
                reportParameters.Add("ADDR_MOO", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_MOO").Value), "", DataGridView1.CurrentRow.Cells("ADDR_MOO").Value.ToString))
                reportParameters.Add("ADDR_SOI", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_SOI").Value), "", DataGridView1.CurrentRow.Cells("ADDR_SOI").Value.ToString))
                reportParameters.Add("ADDR_ROAD", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_ROAD").Value), "", DataGridView1.CurrentRow.Cells("ADDR_ROAD").Value.ToString))
                reportParameters.Add("ADDR_SUB_DISTRICT", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_SUB_DISTRICT").Value), "", DataGridView1.CurrentRow.Cells("ADDR_SUB_DISTRICT").Value.ToString))
                reportParameters.Add("ADDR_DISTRICT", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_DISTRICT").Value), "", DataGridView1.CurrentRow.Cells("ADDR_DISTRICT").Value.ToString))
                reportParameters.Add("ADDR_PROVINCE_NAME", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_PROVINCE_NAME").Value), "", DataGridView1.CurrentRow.Cells("ADDR_PROVINCE_NAME").Value.ToString))
                reportParameters.Add("ADDR_POSTCODE", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_POSTCODE").Value), "", DataGridView1.CurrentRow.Cells("ADDR_POSTCODE").Value.ToString))
                reportParameters.Add("PRENAME_TH", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("PRENAME_TH").Value), "", DataGridView1.SelectedRows(i).Cells("PRENAME_TH").Value.ToString))
                reportParameters.Add("COMP_PERSON_NAME_TH", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("COMP_PERSON_NAME_TH").Value), "", DataGridView1.SelectedRows(i).Cells("COMP_PERSON_NAME_TH").Value.ToString))
                reportParameters.Add("REGIST_CAPITAL", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("REGIST_CAPITAL").Value), Nothing, DataGridView1.SelectedRows(i).Cells("REGIST_CAPITAL").Value.ToString))
                reportParameters.Add("EMPLOYEE_AMOUNT", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("EMPLOYEE_AMOUNT").Value), Nothing, DataGridView1.SelectedRows(i).Cells("EMPLOYEE_AMOUNT").Value.ToString))
                reportParameters.Add("ASSET1", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ASSET1").Value), "false", DataGridView1.SelectedRows(i).Cells("ASSET1").Value.ToString))
                reportParameters.Add("ASSET2", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ASSET2").Value), "false", DataGridView1.SelectedRows(i).Cells("ASSET2").Value.ToString))
                reportParameters.Add("ASSET3", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ASSET3").Value), "false", DataGridView1.SelectedRows(i).Cells("ASSET3").Value.ToString))
                reportParameters.Add("ASSET4", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ASSET4").Value), "false", DataGridView1.SelectedRows(i).Cells("ASSET4").Value.ToString))
                reportParameters.Add("ELECT1", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ELECT1").Value), "false", DataGridView1.SelectedRows(i).Cells("ELECT1").Value.ToString))
                reportParameters.Add("ELECT2", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ELECT2").Value), "false", DataGridView1.SelectedRows(i).Cells("ELECT2").Value.ToString))
                reportParameters.Add("PRODUCE_TECHNOLOGY_TH", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_TH").Value), "false", DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_TH").Value.ToString))
                reportParameters.Add("PRODUCE_TECHNOLOGY_EN", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_EN").Value), "false", DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_EN").Value.ToString))
                reportParameters.Add("PRODUCE_TECHNOLOGY_DESC", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_DESC").Value), "", DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_DESC").Value.ToString))
                reportParameters.Add("BUS_TYPE_RELATE", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_RELATE").Value), "false", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_RELATE").Value.ToString.ToLower))
                reportParameters.Add("BUS_TYPE_DEALER", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_DEALER").Value), "false", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_DEALER").Value.ToString.ToLower))
                reportParameters.Add("BUS_TYPE_IMPORTER", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_IMPORTER").Value), "false", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_IMPORTER").Value.ToString.ToLower))
                reportParameters.Add("BUS_TYPE_EXPORTER", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_EXPORTER").Value), "false", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_EXPORTER").Value.ToString.ToLower))
                reportParameters.Add("BUS_TYPE_OTHER", If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_OTHER").Value), "", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_OTHER").Value.ToString))

                'Create a report parameter for the sales order number 
                'Dim MEMBER_CODE As New ReportParameter()
                'MEMBER_CODE.Name = "MEMBER_CODE"
                'MEMBER_CODE.Values.Add(DataGridView1.SelectedRows(i).Cells("MEMBER_CODE").Value.ToString)

                'Dim ADDR_NO As New ReportParameter()
                'ADDR_NO.Name = "ADDR_NO"
                'ADDR_NO.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ADDR_NO").Value), "", DataGridView1.SelectedRows(i).Cells("ADDR_NO").Value.ToString))

                'Dim ADDR_MOO As New ReportParameter()
                'ADDR_MOO.Name = "ADDR_MOO"
                'ADDR_MOO.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ADDR_MOO").Value), "", DataGridView1.SelectedRows(i).Cells("ADDR_MOO").Value.ToString))

                'Dim ADDR_SOI As New ReportParameter()
                'ADDR_SOI.Name = "ADDR_SOI"
                'ADDR_SOI.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ADDR_SOI").Value), "", DataGridView1.SelectedRows(i).Cells("ADDR_SOI").Value.ToString))

                'Dim ADDR_ROAD As New ReportParameter()
                'ADDR_ROAD.Name = "ADDR_ROAD"
                'ADDR_ROAD.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ADDR_ROAD").Value), "", DataGridView1.SelectedRows(i).Cells("ADDR_ROAD").Value.ToString))

                'Dim ADDR_SUB_DISTRICT As New ReportParameter()
                'ADDR_SUB_DISTRICT.Name = "ADDR_SUB_DISTRICT"
                'ADDR_SUB_DISTRICT.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ADDR_SUB_DISTRICT").Value), "", DataGridView1.SelectedRows(i).Cells("ADDR_SUB_DISTRICT").Value.ToString))

                'Dim ADDR_DISTRICT As New ReportParameter()
                'ADDR_DISTRICT.Name = "ADDR_DISTRICT"
                'ADDR_DISTRICT.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ADDR_DISTRICT").Value), "", DataGridView1.SelectedRows(i).Cells("ADDR_DISTRICT").Value.ToString))

                'Dim ADDR_PROVINCE_NAME As New ReportParameter()
                'ADDR_PROVINCE_NAME.Name = "ADDR_PROVINCE_NAME"
                'ADDR_PROVINCE_NAME.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ADDR_PROVINCE_NAME").Value), "", DataGridView1.SelectedRows(i).Cells("ADDR_PROVINCE_NAME").Value.ToString))

                'Dim ADDR_POSTCODE As New ReportParameter()
                'ADDR_POSTCODE.Name = "ADDR_POSTCODE"
                'ADDR_POSTCODE.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ADDR_POSTCODE").Value), "", DataGridView1.SelectedRows(i).Cells("ADDR_POSTCODE").Value.ToString))

                'Dim PRENAME_TH As New ReportParameter()
                'PRENAME_TH.Name = "PRENAME_TH"
                'PRENAME_TH.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("PRENAME_TH").Value), "", DataGridView1.SelectedRows(i).Cells("PRENAME_TH").Value.ToString))

                'Dim COMP_PERSON_NAME_TH As New ReportParameter()
                'COMP_PERSON_NAME_TH.Name = "COMP_PERSON_NAME_TH"
                'COMP_PERSON_NAME_TH.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("COMP_PERSON_NAME_TH").Value), "", DataGridView1.SelectedRows(i).Cells("COMP_PERSON_NAME_TH").Value.ToString))

                'Dim REGIST_CAPITAL As New ReportParameter()
                'REGIST_CAPITAL.Name = "REGIST_CAPITAL"
                'REGIST_CAPITAL.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("REGIST_CAPITAL").Value), Nothing, DataGridView1.SelectedRows(i).Cells("REGIST_CAPITAL").Value.ToString))

                'Dim EMPLOYEE_AMOUNT As New ReportParameter()
                'EMPLOYEE_AMOUNT.Name = "EMPLOYEE_AMOUNT"
                'EMPLOYEE_AMOUNT.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("EMPLOYEE_AMOUNT").Value), Nothing, DataGridView1.SelectedRows(i).Cells("EMPLOYEE_AMOUNT").Value.ToString))

                'Dim ASSET1 As New ReportParameter()
                'ASSET1.Name = "ASSET1"
                'ASSET1.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ASSET1").Value), "false", DataGridView1.SelectedRows(i).Cells("ASSET1").Value.ToString))

                'Dim ASSET2 As New ReportParameter()
                'ASSET2.Name = "ASSET2"
                'ASSET2.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ASSET2").Value), "false", DataGridView1.SelectedRows(i).Cells("ASSET2").Value.ToString))

                'Dim ASSET3 As New ReportParameter()
                'ASSET3.Name = "ASSET3"
                'ASSET3.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ASSET3").Value), "false", DataGridView1.SelectedRows(i).Cells("ASSET3").Value.ToString))

                'Dim ASSET4 As New ReportParameter()
                'ASSET4.Name = "ASSET4"
                'ASSET4.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ASSET4").Value), "false", DataGridView1.SelectedRows(i).Cells("ASSET4").Value.ToString))

                'MessageBox.Show(DataGridView1.SelectedRows(i).Cells("ASSET1").Value.ToString & DataGridView1.SelectedRows(i).Cells("ASSET2").Value.ToString & DataGridView1.SelectedRows(i).Cells("ASSET3").Value.ToString & DataGridView1.SelectedRows(i).Cells("ASSET4").Value.ToString)

                'Dim ELECT1 As New ReportParameter()
                'ELECT1.Name = "ELECT1"
                'ELECT1.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ELECT1").Value), "false", DataGridView1.SelectedRows(i).Cells("ELECT1").Value.ToString))

                'Dim ELECT2 As New ReportParameter()
                'ELECT2.Name = "ELECT2"
                'ELECT2.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("ELECT2").Value), "false", DataGridView1.SelectedRows(i).Cells("ELECT2").Value.ToString))

                'Dim PRODUCE_TECHNOLOGY_TH As New ReportParameter()
                'PRODUCE_TECHNOLOGY_TH.Name = "PRODUCE_TECHNOLOGY_TH"
                'PRODUCE_TECHNOLOGY_TH.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_TH").Value), "false", DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_TH").Value.ToString))

                'Dim PRODUCE_TECHNOLOGY_EN As New ReportParameter()
                'PRODUCE_TECHNOLOGY_EN.Name = "PRODUCE_TECHNOLOGY_EN"
                'PRODUCE_TECHNOLOGY_EN.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_EN").Value), "false", DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_EN").Value.ToString))

                'Dim PRODUCE_TECHNOLOGY_DESC As New ReportParameter()
                'PRODUCE_TECHNOLOGY_DESC.Name = "PRODUCE_TECHNOLOGY_DESC"
                'PRODUCE_TECHNOLOGY_DESC.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_DESC").Value), "", DataGridView1.SelectedRows(i).Cells("PRODUCE_TECHNOLOGY_DESC").Value.ToString))

                'Dim BUS_TYPE_RELATE As New ReportParameter()
                'BUS_TYPE_RELATE.Name = "BUS_TYPE_RELATE"
                'BUS_TYPE_RELATE.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_RELATE").Value), "false", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_RELATE").Value.ToString.ToLower))

                'Dim BUS_TYPE_DEALER As New ReportParameter()
                'BUS_TYPE_DEALER.Name = "BUS_TYPE_DEALER"
                'BUS_TYPE_DEALER.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_DEALER").Value), "false", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_DEALER").Value.ToString.ToLower))

                'Dim BUS_TYPE_IMPORTER As New ReportParameter()
                'BUS_TYPE_IMPORTER.Name = "BUS_TYPE_IMPORTER"
                'BUS_TYPE_IMPORTER.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_IMPORTER").Value), "false", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_IMPORTER").Value.ToString.ToLower))

                'Dim BUS_TYPE_EXPORTER As New ReportParameter()
                'BUS_TYPE_EXPORTER.Name = "BUS_TYPE_EXPORTER"
                'BUS_TYPE_EXPORTER.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_EXPORTER").Value), "false", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_EXPORTER").Value.ToString.ToLower))

                'Dim BUS_TYPE_OTHER As New ReportParameter()
                'BUS_TYPE_OTHER.Name = "BUS_TYPE_OTHER"
                'BUS_TYPE_OTHER.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("BUS_TYPE_OTHER").Value), "", DataGridView1.SelectedRows(i).Cells("BUS_TYPE_OTHER").Value.ToString))

                'Set the report parameters for the report
                'Dim params() As ReportParameter = {MEMBER_CODE, ADDR_NO, ADDR_MOO, ADDR_SOI, ADDR_ROAD, ADDR_SUB_DISTRICT, ADDR_DISTRICT, ADDR_PROVINCE_NAME, ADDR_POSTCODE, PRENAME_TH, COMP_PERSON_NAME_TH}
                'f.ReportViewer1.LocalReport.SetParameters(params)

                'f.ReportViewer1.LocalReport.DataSources.Clear()
                'f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("ds", dt))

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
            Next
        End If
    End Sub
End Class