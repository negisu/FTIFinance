Imports Microsoft.Reporting.WinForms

Public Class frmFTImemberExisting

    Dim ds As DataSet
    Dim dv As DataView

    Private Sub frmFTIapproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0

        ds = New DataSet

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMB_MEMBER_RETIRE(ByVal SEARCH As String)
        SEARCH = "%" & SEARCH.Replace(" ", "%")
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", SEARCH)
        'parameters.Add("@p1", TO_DATE)
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
        query &= "SELECT        MB_MEMBER.REGIST_CODE, MB_MEMBER.MEMBER_MAIN_GROUP_CODE, MB_MEMBER.MEMBER_GROUP_CODE, MB_MEMBER.MEMBER_MAIN_TYPE_CODE, MB_MEMBER.MEMBER_TYPE_CODE, "
        query &= "                         MB_MEMBER.MEMBER_CODE AS MEMBER_CODE_TH, MB_MEMBER.REGIST_DATE, MB_MEMBER.MEMBER_DATE, MB_MEMBER.MEMBER_STATUS_CODE, MB_MEMBER.COMP_PERSON_CODE, MB_PRENAME.PRENAME_TH, "
        query &= "                         MB_COMP_PERSON.COMP_PERSON_NAME_TH, MB_PRENAME.PRENAME_EN, MB_COMP_PERSON.COMP_PERSON_NAME_EN, MB_COMP_PERSON_ADDRESS.ADDR_NO AS ADDR_NO_TH, "
        query &= "                         MB_COMP_PERSON_ADDRESS.ADDR_MOO AS ADDR_MOO_TH, MB_COMP_PERSON_ADDRESS.ADDR_SOI AS ADDR_SOI_TH, MB_COMP_PERSON_ADDRESS.ADDR_ROAD AS ADDR_ROAD_TH, MB_COMP_PERSON_ADDRESS.ADDR_SUB_DISTRICT AS ADDR_SUB_DISTRICT_TH, "
        query &= "                         MB_COMP_PERSON_ADDRESS.ADDR_DISTRICT AS ADDR_DISTRICT_TH, MB_COMP_PERSON_ADDRESS.ADDR_PROVINCE_NAME AS ADDR_PROVINCE_NAME_TH, MB_COMP_PERSON_ADDRESS.ADDR_POSTCODE AS ADDR_POSTCODE_TH, MB_COMP_PERSON_ADDRESS.ADDR_NO_EN, "
        query &= "                         MB_COMP_PERSON_ADDRESS.ADDR_MOO_EN, MB_COMP_PERSON_ADDRESS.ADDR_SOI_EN, MB_COMP_PERSON_ADDRESS.ADDR_ROAD_EN, MB_COMP_PERSON_ADDRESS.ADDR_SUB_DISTRICT_EN, "
        query &= "                         MB_COMP_PERSON_ADDRESS.ADDR_DISTRICT_EN, MB_COMP_PERSON_ADDRESS.ADDR_PROVINCE_NAME_EN, MB_COMP_PERSON_ADDRESS.ADDR_POSTCODE_EN, "
        query &= "                         MB_PRENAME_1.PRENAME_TH AS CONTACT_PRENAME, MB_CONTACT.CONTACT_FIRST_NAME_TH AS CONTACT_FIRST_NAME, MB_CONTACT.CONTACT_LAST_NAME_TH AS CONTACT_LAST_NAME, "
        query &= "                         MB_PRENAME_1.PRENAME_EN AS CONTACT_PRENAME_EN, MB_CONTACT.CONTACT_FIRST_NAME_EN, MB_CONTACT.CONTACT_LAST_NAME_EN, MB_MEMBER_TYPE.MEMBER_TYPE_NAME AS MEMBER_TYPE_NAME_TH, MB_MEMBER_TYPE.MEMBER_TYPE_NAME_EN, MB_MEMBER.MEMBER_DATE, dbo.get5_3_25_3(MB_MEMBER.COMP_PERSON_CODE, 'TH') AS PRODUCTS_TH, dbo.get5_3_25_3(MB_MEMBER.COMP_PERSON_CODE, 'EN') AS PRODUCTS_EN, REPLACE(MB_MEMBER.MEMBER_CODE, MB_MEMBER_TYPE.MEMBER_SHORT_NAME, MB_MEMBER_TYPE.MEMBER_SHORT_NAME_EN) AS MEMBER_CODE_EN "
        query &= "FROM            MB_MEMBER INNER JOIN "
        query &= "                         MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
        query &= "                         MB_MEMBER_REPRESENT ON MB_MEMBER.REGIST_CODE = MB_MEMBER_REPRESENT.REGIST_CODE INNER JOIN "
        query &= "                         MB_COMP_PERSON_ADDRESS ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON_ADDRESS.COMP_PERSON_CODE INNER JOIN "
        query &= "                         MB_PRENAME ON MB_COMP_PERSON.PREN_CODE = MB_PRENAME.PRENAME_CODE INNER JOIN "
        query &= "                         MB_CONTACT ON MB_MEMBER_REPRESENT.CONTACT_CODE = MB_CONTACT.CONTACT_CODE INNER JOIN "
        query &= "                         MB_PRENAME AS MB_PRENAME_1 ON MB_CONTACT.CONTACT_PRENAME_CODE = MB_PRENAME_1.PRENAME_CODE INNER JOIN "
        query &= "                         MB_MEMBER_TYPE ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_TYPE.MEMBER_MAIN_GROUP_CODE AND "
        query &= "                         MB_MEMBER.MEMBER_GROUP_CODE = MB_MEMBER_TYPE.MEMBER_GROUP_CODE AND MB_MEMBER.MEMBER_MAIN_TYPE_CODE = MB_MEMBER_TYPE.MEMBER_MAIN_TYPE_CODE AND "
        query &= "                         MB_MEMBER.MEMBER_TYPE_CODE = MB_MEMBER_TYPE.MEMBER_TYPE_CODE "
        query &= "WHERE        ((MB_MEMBER.MEMBER_CODE LIKE @p0) OR (MB_MEMBER.MEMBER_CODE LIKE @p0)) AND (MB_MEMBER.MEMBER_STATUS_CODE = 'A') AND (MB_COMP_PERSON_ADDRESS.ADDR_CODE = '001') AND (MB_MEMBER_REPRESENT.REPRESENT_SEQ = 1) AND "
        query &= "                         (MB_PRENAME_1.PRENAME_TYPE = 1) AND (MB_PRENAME.PRENAME_TYPE = 2) AND (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN ('000', '100', '200', '999')) "
        query &= "ORDER BY MB_MEMBER.PREFIX, MB_MEMBER.RUNNING"

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

            DataGridView1.Columns("MEMBER_CODE_TH").Visible = True
            DataGridView1.Columns("PRENAME_TH").Visible = True
            DataGridView1.Columns("COMP_PERSON_NAME_TH").Visible = True
            DataGridView1.Columns("PRENAME_EN").Visible = True
            DataGridView1.Columns("COMP_PERSON_NAME_EN").Visible = True
            DataGridView1.Columns("MEMBER_DATE").Visible = True
            DataGridView1.Columns("MEMBER_CODE_EN").Visible = True
            'DataGridView1.Columns("RETIRE_REASON").Visible = True
            'DataGridView1.Columns("MEMBER_STATUS_CODE").Visible = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btPrintPreview.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            Dim fi As New frmFTImemberExistingFields
            If RadioButton1.Checked = True Then
                fi.LANG = "TH"
            Else
                fi.LANG = "EN"
            End If
            If IsDBNull(DataGridView1.CurrentRow.Cells("MEMBER_DATE").Value) = False Then fi.DateTimePicker2.Value = CDate(DataGridView1.CurrentRow.Cells("MEMBER_DATE").Value)
            If fi.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Dim f As New frmMainReports
                'Me.BindingSource1.DataSource = MyDataTable
                'Dim rds As New ReportDataSource("DataSet1", Me.BindingSource1)
                Dim LANG As String = String.Empty
                Dim ci As System.Globalization.CultureInfo

                Dim reportParameters As New Dictionary(Of String, String)

                'Dim MEMBER_CODE As New ReportParameter()
                'MEMBER_CODE.Name = "MEMBER_CODE"

                'Dim APPROVE_DATE As New ReportParameter()
                'APPROVE_DATE.Name = "APPROVE_DATE"

                If RadioButton1.Checked = True Then
                    f.reportPath = getParameters(1, "FTI_RPT_5.3.25.03_reportPathTH")
                    'f.ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "/Reports/module1/rdlc5.3.25.03.FTImemberExistingTH.rdlc"
                    ci = ciTH
                    LANG = "_TH"
                    reportParameters.Add("MEMBER_CODE", DataGridView1.CurrentRow.Cells("MEMBER_CODE_TH").Value.ToString)
                    reportParameters.Add("APPROVE_DATE", fi.DateTimePicker2.Value.ToString("dddd ที่ d MMMM yyyy", ci))
                    'MEMBER_CODE.Values.Add(DataGridView1.CurrentRow.Cells("MEMBER_CODE_TH").Value.ToString)
                    'APPROVE_DATE.Values.Add(fi.DateTimePicker2.Value.ToString("dddd ที่ d MMMM yyyy", ci))
                Else
                    'f.ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "/Reports/module1/rdlc5.3.25.03.FTImemberExistingEN.rdlc"
                    f.reportPath = getParameters(1, "FTI_RPT_5.3.25.03_reportPathEN")
                    ci = ciEN
                    LANG = "_EN"

                    reportParameters.Add("MEMBER_CODE", DataGridView1.CurrentRow.Cells("MEMBER_CODE_EN").Value.ToString)
                    reportParameters.Add("APPROVE_DATE", fi.DateTimePicker2.Value.ToString("MMMM d, yyyy", ci))
                    'MEMBER_CODE.Values.Add(DataGridView1.CurrentRow.Cells("MEMBER_CODE_EN").Value.ToString)
                    'APPROVE_DATE.Values.Add(fi.DateTimePicker2.Value.ToString("MMMM d, yyyy", ci))
                End If

                'Me.ReportViewer1.LocalReport.DataSources.Clear()
                'Me.ReportViewer1.LocalReport.DataSources.Add(rds)

                'Create a report parameter for the sales order number 

                reportParameters.Add("DOC_NO", fi.TextBox1.Text)
                reportParameters.Add("ADDR_NO", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_NO" & LANG).Value), "", DataGridView1.CurrentRow.Cells("ADDR_NO" & LANG).Value.ToString))
                reportParameters.Add("ADDR_MOO", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_MOO" & LANG).Value), "", DataGridView1.CurrentRow.Cells("ADDR_MOO" & LANG).Value.ToString))
                reportParameters.Add("ADDR_SOI", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_SOI" & LANG).Value), "", DataGridView1.CurrentRow.Cells("ADDR_SOI" & LANG).Value.ToString))
                reportParameters.Add("ADDR_ROAD", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_ROAD" & LANG).Value), "", DataGridView1.CurrentRow.Cells("ADDR_ROAD" & LANG).Value.ToString))
                reportParameters.Add("ADDR_SUB_DISTRICT", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_SUB_DISTRICT" & LANG).Value), "", DataGridView1.CurrentRow.Cells("ADDR_SUB_DISTRICT" & LANG).Value.ToString))
                reportParameters.Add("ADDR_DISTRICT", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_DISTRICT" & LANG).Value), "", DataGridView1.CurrentRow.Cells("ADDR_DISTRICT" & LANG).Value.ToString))
                reportParameters.Add("ADDR_PROVINCE_NAME", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_PROVINCE_NAME" & LANG).Value), "", DataGridView1.CurrentRow.Cells("ADDR_PROVINCE_NAME" & LANG).Value.ToString))
                reportParameters.Add("ADDR_POSTCODE", If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_POSTCODE" & LANG).Value), "", DataGridView1.CurrentRow.Cells("ADDR_POSTCODE" & LANG).Value.ToString))
                reportParameters.Add("MANAGER_NAME", fi.TextBox2.Text)
                reportParameters.Add("MANAGER_DESIGNATION", fi.TextBox3.Text)
                reportParameters.Add("DOC_DATE", fi.DateTimePicker1.Value.ToString("d MMMM yyyy", ci))
                reportParameters.Add("DOC_YEAR", fi.DateTimePicker3.Value.ToString("yyyy", ci))
                reportParameters.Add("PRENAME", DataGridView1.CurrentRow.Cells("PRENAME" & LANG).Value.ToString)
                reportParameters.Add("COMP_PERSON_NAME", DataGridView1.CurrentRow.Cells("COMP_PERSON_NAME" & LANG).Value.ToString)
                reportParameters.Add("MEMBER_TYPE_NAME", DataGridView1.CurrentRow.Cells("MEMBER_TYPE_NAME" & LANG).Value.ToString)
                reportParameters.Add("MAIN_INDUSTRY_DESC", DataGridView1.CurrentRow.Cells("PRODUCTS" & LANG).Value.ToString)

                'Dim DOC_NO As New ReportParameter()
                'DOC_NO.Name = "DOC_NO"
                'DOC_NO.Values.Add(fi.TextBox1.Text)


                'Dim ADDR_NO As New ReportParameter()
                'ADDR_NO.Name = "ADDR_NO"
                'ADDR_NO.Values.Add(If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_NO" & LANG).Value), "", DataGridView1.CurrentRow.Cells("ADDR_NO" & LANG).Value.ToString))

                'Dim ADDR_MOO As New ReportParameter()
                'ADDR_MOO.Name = "ADDR_MOO"
                'ADDR_MOO.Values.Add(If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_MOO" & LANG).Value), "", DataGridView1.CurrentRow.Cells("ADDR_MOO" & LANG).Value.ToString))

                'Dim ADDR_SOI As New ReportParameter()
                'ADDR_SOI.Name = "ADDR_SOI"
                'ADDR_SOI.Values.Add(If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_SOI" & LANG).Value), "", DataGridView1.CurrentRow.Cells("ADDR_SOI" & LANG).Value.ToString))

                'Dim ADDR_ROAD As New ReportParameter()
                'ADDR_ROAD.Name = "ADDR_ROAD"
                'ADDR_ROAD.Values.Add(If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_ROAD" & LANG).Value), "", DataGridView1.CurrentRow.Cells("ADDR_ROAD" & LANG).Value.ToString))

                'Dim ADDR_SUB_DISTRICT As New ReportParameter()
                'ADDR_SUB_DISTRICT.Name = "ADDR_SUB_DISTRICT"
                'ADDR_SUB_DISTRICT.Values.Add(If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_SUB_DISTRICT" & LANG).Value), "", DataGridView1.CurrentRow.Cells("ADDR_SUB_DISTRICT" & LANG).Value.ToString))

                'Dim ADDR_DISTRICT As New ReportParameter()
                'ADDR_DISTRICT.Name = "ADDR_DISTRICT"
                'ADDR_DISTRICT.Values.Add(If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_DISTRICT" & LANG).Value), "", DataGridView1.CurrentRow.Cells("ADDR_DISTRICT" & LANG).Value.ToString))

                'Dim ADDR_PROVINCE_NAME As New ReportParameter()
                'ADDR_PROVINCE_NAME.Name = "ADDR_PROVINCE_NAME"
                'ADDR_PROVINCE_NAME.Values.Add(If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_PROVINCE_NAME" & LANG).Value), "", DataGridView1.CurrentRow.Cells("ADDR_PROVINCE_NAME" & LANG).Value.ToString))

                'Dim ADDR_POSTCODE As New ReportParameter()
                'ADDR_POSTCODE.Name = "ADDR_POSTCODE"
                'ADDR_POSTCODE.Values.Add(If(IsDBNull(DataGridView1.CurrentRow.Cells("ADDR_POSTCODE" & LANG).Value), "", DataGridView1.CurrentRow.Cells("ADDR_POSTCODE" & LANG).Value.ToString))

                'Dim MANAGER_NAME As New ReportParameter()
                'MANAGER_NAME.Name = "MANAGER_NAME"
                'MANAGER_NAME.Values.Add(fi.TextBox2.Text)

                'Dim MANAGER_DESIGNATION As New ReportParameter()
                'MANAGER_DESIGNATION.Name = "MANAGER_DESIGNATION"
                'MANAGER_DESIGNATION.Values.Add(fi.TextBox3.Text)

                ''Dim DOC_USER_NAME As New ReportParameter()
                ''DOC_USER_NAME.Name = "DOC_USER_NAME"
                ''DOC_USER_NAME.Values.Add(fi.TextBox4.Text)

                ''Dim DOC_USER_TEL As New ReportParameter()
                ''DOC_USER_TEL.Name = "DOC_USER_TEL"
                ''DOC_USER_TEL.Values.Add(fi.TextBox5.Text)

                'Dim DOC_DATE As New ReportParameter()
                'DOC_DATE.Name = "DOC_DATE"
                'DOC_DATE.Values.Add(fi.DateTimePicker1.Value.ToString("d MMMM yyyy", ci))

                'Dim DOC_YEAR As New ReportParameter()
                'DOC_YEAR.Name = "DOC_YEAR"
                'DOC_YEAR.Values.Add(fi.DateTimePicker3.Value.ToString("yyyy", ci))

                'Dim PRENAME As New ReportParameter()
                'PRENAME.Name = "PRENAME"
                'PRENAME.Values.Add(DataGridView1.CurrentRow.Cells("PRENAME" & LANG).Value.ToString)


                'Dim COMP_PERSON_NAME As New ReportParameter()
                'COMP_PERSON_NAME.Name = "COMP_PERSON_NAME"
                'COMP_PERSON_NAME.Values.Add(DataGridView1.CurrentRow.Cells("COMP_PERSON_NAME" & LANG).Value.ToString)

                'Dim MEMBER_TYPE_NAME As New ReportParameter()
                'MEMBER_TYPE_NAME.Name = "MEMBER_TYPE_NAME"
                'MEMBER_TYPE_NAME.Values.Add(DataGridView1.CurrentRow.Cells("MEMBER_TYPE_NAME" & LANG).Value.ToString)

                'Dim PRODUCTS As New ReportParameter()
                'PRODUCTS.Name = "MAIN_INDUSTRY_DESC"
                'PRODUCTS.Values.Add(DataGridView1.CurrentRow.Cells("PRODUCTS" & LANG).Value.ToString)

                'Set the report parameters for the report
                'Dim params() As ReportParameter = {DOC_NO, MEMBER_CODE, ADDR_NO, ADDR_MOO, ADDR_SOI, ADDR_ROAD, ADDR_SUB_DISTRICT, ADDR_DISTRICT, ADDR_PROVINCE_NAME, ADDR_POSTCODE, APPROVE_DATE, MANAGER_NAME, MANAGER_DESIGNATION, DOC_YEAR, DOC_DATE, PRENAME, COMP_PERSON_NAME, MEMBER_TYPE_NAME, PRODUCTS}
                'f.ReportViewer1.LocalReport.SetParameters(params)

                f.reportParameters = reportParameters

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
            fi.Dispose()
            fi = Nothing
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
        getMB_MEMBER_RETIRE(TextBox1.Text)
    End Sub
End Class