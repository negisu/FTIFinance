Public Class frmINIFees

    Dim ds As DataSet
    Dim bsMEMBER_MAIN_GROUP As BindingSource
    Dim bsMEMBER_GROUP As BindingSource
    Dim bsMEMBER_MAIN_TYPE As BindingSource
    Dim bsMEMBER_TYPE As BindingSource

    Friend INI_CODE As String
    Friend INI_NAME As String
    Friend INI_ID As Integer

    Private Sub frmFees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet
        Label2.Text = String.Empty
        ComboBox1.SelectedIndex = 0

        getMEMBER_MAIN_GROUP()
        getMEMBER_GROUP()

        ComboBox5.SelectedIndex = 0
        ComboBox2.SelectedIndex = 0

        ComboBox5_SelectedIndexChanged(sender, e)

        'Label2.Text = String.Format("พบ {0} รายการ", ds.Tables("MB_MEMBER").Rows.Count.ToString("#,##0"))
        'Timer1.Start()

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try

    End Sub

    Private Sub frmFees_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fFee = Nothing
    End Sub

    Private Sub getMEMBER_MAIN_GROUP()
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", INI_CODE)
        'parameters.Add("@p1", "1")
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT [MEMBER_MAIN_GROUP_CODE], [MEMBER_MAIN_GROUP_NAME], [INACTIVE] FROM [MB_MEMBER_MAIN_GROUP] "
        query &= "WHERE (MEMBER_MAIN_GROUP_CODE IN (@p0)) AND (INACTIVE <> 1) "
        query &= "ORDER BY MEMBER_MAIN_GROUP_CODE "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_MAIN_GROUP").Copy
        'dt.TableName = "MB_MEMBER_MAIN_GROUP"
        If ds.Tables.Contains("MB_MEMBER_MAIN_GROUP") = True Then
            ds.Tables("MB_MEMBER_MAIN_GROUP").Clear()
            ds.Tables("MB_MEMBER_MAIN_GROUP").Merge(dt)
            ds.Tables("MB_MEMBER_MAIN_GROUP").AcceptChanges()
        Else
            ds.Tables.Add(dt)
            bsMEMBER_MAIN_GROUP = New BindingSource(ds, "MB_MEMBER_MAIN_GROUP")
        End If

        ComboBox5.DataSource = bsMEMBER_MAIN_GROUP
        ComboBox5.DisplayMember = "MEMBER_MAIN_GROUP_NAME"
        ComboBox5.ValueMember = "MEMBER_MAIN_GROUP_CODE"
    End Sub

    Private Sub getMEMBER_GROUP()
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", INI_CODE)
        'parameters.Add("@p1", "1")
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT MEMBER_MAIN_GROUP_CODE, [MEMBER_GROUP_CODE], [MEMBER_GROUP_NAME], [MEMBER_GROUP_NAME_EN], [INACTIVE] FROM [MB_MEMBER_GROUP] "
        query &= "WHERE (MEMBER_MAIN_GROUP_CODE IN (@p0)) AND (INACTIVE <> 1) "
        query &= "ORDER BY [MEMBER_GROUP_CODE] "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_GROUP").Copy
        'dt.TableName = "MB_MEMBER_GROUP"
        If ds.Tables.Contains("MB_MEMBER_GROUP") = True Then
            ds.Tables("MB_MEMBER_GROUP").Clear()
            ds.Tables("MB_MEMBER_GROUP").Merge(dt)
            ds.Tables("MB_MEMBER_GROUP").AcceptChanges()
        Else
            ds.Tables.Add(dt)
            bsMEMBER_GROUP = New BindingSource(ds, "MB_MEMBER_GROUP")
        End If

        ComboBox2.DataSource = bsMEMBER_GROUP
        ComboBox2.DisplayMember = "MEMBER_GROUP_NAME"
        ComboBox2.ValueMember = "MEMBER_GROUP_CODE"
    End Sub

    'Private Sub getMEMBER_MAIN_TYPE()
    '    Dim parameters As New Dictionary(Of String, Object)
    '    'parameters.Add("@p0", MEMBER_MAIN_GROUP_CODE)
    '    'parameters.Add("@p1", MEMBER_GROUP_CODE)
    '    'parameters.Add("@p2", "2")
    '    'parameters.Add("@p3", "3")

    '    Dim query As String = String.Empty
    '    query &= "SELECT [MEMBER_MAIN_TYPE_CODE], [MEMBER_MAIN_TYPE_NAME], [INACTIVE] FROM [MB_MEMBER_MAIN_TYPE] "
    '    query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) "
    '    query &= "ORDER BY MEMBER_MAIN_TYPE_CODE "

    '    Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_MAIN_TYPE").Copy
    '    'dt.TableName = "MB_MEMBER_MAIN_TYPE"
    '    If ds.Tables.Contains("MB_MEMBER_MAIN_TYPE") Then ds.Tables.Remove("MB_MEMBER_MAIN_TYPE")
    '    ds.Tables.Add(dt)

    '    ComboBox3.DataSource = ds.Tables("MB_MEMBER_MAIN_TYPE")
    '    ComboBox3.DisplayMember = "MEMBER_MAIN_TYPE_NAME"
    '    ComboBox3.ValueMember = "MEMBER_MAIN_TYPE_CODE"
    'End Sub

    'Private Sub getMEMBER_TYPE(ByVal MEMBER_MAIN_GROUP_CODE As String, ByVal MEMBER_GROUP_CODE As String, ByVal MEMBER_MAIN_TYPE_CODE As String)
    '    Dim parameters As New Dictionary(Of String, Object)
    '    parameters.Add("@p0", MEMBER_MAIN_GROUP_CODE)
    '    parameters.Add("@p1", MEMBER_GROUP_CODE)
    '    parameters.Add("@p2", MEMBER_MAIN_TYPE_CODE)
    '    'parameters.Add("@p3", "3")

    '    Dim query As String = String.Empty
    '    query &= "SELECT [MEMBER_TYPE_CODE], [MEMBER_TYPE_NAME], [INACTIVE] FROM [MB_MEMBER_TYPE] "
    '    query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) "
    '    query &= "ORDER BY [MEMBER_TYPE_CODE] "

    '    Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_TYPE").Copy
    '    'dt.TableName = "MB_MEMBER_TYPE"
    '    If ds.Tables.Contains("MB_MEMBER_TYPE") Then ds.Tables.Remove("MB_MEMBER_TYPE")
    '    ds.Tables.Add(dt)

    '    ComboBox4.DataSource = ds.Tables("MB_MEMBER_TYPE")
    '    ComboBox4.DisplayMember = "MEMBER_TYPE_NAME"
    '    ComboBox4.ValueMember = "MEMBER_TYPE_CODE"
    'End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedValueChanged
        'If ComboBox5.SelectedValue IsNot Nothing Then getMEMBER_GROUP(ComboBox5.SelectedValue.ToString)
        If ComboBox5.SelectedValue IsNot Nothing And bsMEMBER_GROUP IsNot Nothing Then
            bsMEMBER_GROUP.Filter = String.Format("(MEMBER_MAIN_GROUP_CODE = '{0}')", ComboBox5.SelectedValue.ToString)
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedValueChanged
        'If ComboBox5.SelectedValue IsNot Nothing And ComboBox2.SelectedValue IsNot Nothing Then getMEMBER_MAIN_TYPE(ComboBox5.SelectedValue.ToString, ComboBox2.SelectedValue.ToString)
        If ComboBox5.SelectedValue IsNot Nothing And ComboBox2.SelectedValue IsNot Nothing Then
            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", ComboBox5.SelectedValue.ToString)
            parameters.Add("@p1", ComboBox2.SelectedValue.ToString)
            'parameters.Add("@p2", ComboBox3.SelectedValue.ToString)
            'parameters.Add("@p3", ComboBox4.SelectedValue.ToString)

            '==============================================
            Try
                Dim query1 As String = "SELECT * FROM MB_MEMBER_RATE_MASTER "
                'query &= "SELECT [MEMBER_TYPE_CODE], [MEMBER_TYPE_NAME], [INACTIVE] FROM [MB_MEMBER_TYPE] "
                'query1 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) "
                query1 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) "

                Dim dt1 As DataTable = fillWebSQL(query1, parameters, "MB_MEMBER_RATE_MASTER").Copy
                'dt1.TableName = "MB_MEMBER_RATE_MASTER"
                If ds.Tables.Contains("MB_MEMBER_RATE_MASTER") Then ds.Tables.Remove("MB_MEMBER_RATE_MASTER")
                ds.Tables.Add(dt1)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "PROCESS 1")
            End Try

            '==============================================
            Try
                Dim query2 As String = "SELECT * FROM MB_MEMBER_RATE_DETAIL "
                'query &= "SELECT [MEMBER_TYPE_CODE], [MEMBER_TYPE_NAME], [INACTIVE] FROM [MB_MEMBER_TYPE] "
                'query2 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) "
                query2 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) "
                query2 &= "ORDER BY [RATE_SEQ] "

                Dim dt2 As DataTable = fillWebSQL(query2, parameters, "MB_MEMBER_RATE_DETAIL").Copy
                'dt2.TableName = "MB_MEMBER_RATE_DETAIL"
                If ds.Tables.Contains("MB_MEMBER_RATE_DETAIL") Then ds.Tables.Remove("MB_MEMBER_RATE_DETAIL")
                ds.Tables.Add(dt2)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "PROCESS 2")
            End Try

            '==============================================
            'INCOME_PER_YEAR
            'get member
            '(MB_MEMBER.MEMBER_MAIN_GROUP_CODE = @p0) AND (MB_MEMBER.MEMBER_GROUP_CODE = @p1) AND (MB_MEMBER.MEMBER_MAIN_TYPE_CODE = @p2) AND (MB_MEMBER.MEMBER_TYPE_CODE = @p3)
            Dim query3 As String = getParameters(1, "FTI_FEE_RULE2_QUERY")
            Try
                'Dim query3 As String = "SELECT MB_MEMBER.MEMBER_MAIN_GROUP_CODE, MB_MEMBER.MEMBER_GROUP_CODE, MB_MEMBER.MEMBER_MAIN_TYPE_CODE, MB_MEMBER.MEMBER_TYPE_CODE, MB_MEMBER.MEMBER_CODE, MB_COMP_PERSON.COMP_PERSON_NAME_TH, MB_COMP_PERSON.COMP_PERSON_NAME_EN, MB_MEMBER.REGIST_DATE, "
                'query3 &= "(SELECT TOP (1) INCOME_AMOUNT FROM MB_COMPANY_INCOME WHERE (COMP_PERSON_CODE = MB_MEMBER.COMP_PERSON_CODE) ORDER BY YEAR DESC) AS INCOME_AMOUNT, "
                'query3 &= "MB_COMP_PERSON.ASSET_AMOUNT, MB_COMP_PERSON.EMPLOYEE_AMOUNT, MB_COMP_PERSON.REGIST_CAPITAL, MB_COMP_PERSON.PERCENT_SHAREHOLDER "
                'query3 &= "FROM            MB_MEMBER INNER JOIN "
                'query3 &= "                         MB_COMP_PERSON ON MB_MEMBER.OU_CODE = MB_COMP_PERSON.OU_CODE AND "
                'query3 &= "                         MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE "
                ''query3 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) AND (MB_MEMBER.MEMBER_STATUS_CODE = 'A') "
                'query3 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MB_MEMBER.MEMBER_STATUS_CODE = 'A') "
                'query3 &= "ORDER BY MB_MEMBER.PREFIX, MB_MEMBER.RUNNING "

                Dim dt3 As DataTable = fillWebSQL(query3, parameters, "MB_MEMBER").Copy
                'dt3.PrimaryKey = New DataColumn() {dt3.Columns("MEMBER_CODE")}
                dt3.Columns.Add("STEP1", Type.GetType("System.Double"))
                dt3.Columns.Add("STEP2", Type.GetType("System.Double"))
                dt3.Columns.Add("STEP3", Type.GetType("System.Double"))
                dt3.Columns.Add("STEP4", Type.GetType("System.Double"))

                If ds.Tables.Contains("MB_MEMBER") = True Then
                    ds.Tables("MB_MEMBER").Clear()
                    ds.Tables("MB_MEMBER").Merge(dt3)
                    ds.Tables("MB_MEMBER").AcceptChanges()
                Else
                    ds.Tables.Add(dt3)
                    ds.Tables("MB_MEMBER").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER").Columns("MEMBER_CODE"), ds.Tables("MB_MEMBER").Columns("MEMBER_MAIN_GROUP_CODE"), ds.Tables("MB_MEMBER").Columns("MEMBER_GROUP_CODE"), ds.Tables("MB_MEMBER").Columns("MEMBER_MAIN_TYPE_CODE"), ds.Tables("MB_MEMBER").Columns("MEMBER_TYPE_CODE")}

                    DataGridView1.DataSource = ds.Tables("MB_MEMBER")

                    '==============================================
                    For idx As Integer = 0 To DataGridView1.ColumnCount - 1
                        DataGridView1.Columns(idx).ReadOnly = True
                    Next
                    DataGridView1.Columns("STEP3").ReadOnly = False

                    DataGridView1.Columns("MEMBER_MAIN_GROUP_CODE").Visible = False
                    DataGridView1.Columns("MEMBER_GROUP_CODE").Visible = False
                    DataGridView1.Columns("MEMBER_MAIN_TYPE_CODE").Visible = False
                    DataGridView1.Columns("MEMBER_TYPE_CODE").Visible = False

                    'DataGridView1.Columns("MEMBER_CODE").Visible = True
                    'DataGridView1.Columns("STEP1").Visible = True
                    'DataGridView1.Columns("STEP2").Visible = True
                    'DataGridView1.Columns("STEP3").Visible = True
                    'DataGridView1.Columns("START_MONTH_FIRST_YEAR").Visible = True
                    'DataGridView1.Columns("END_MONTH_FIRST_YEAR").Visible = True
                    'DataGridView1.Columns("FIRST_YEAR_RATE").Visible = True
                    'DataGridView1.Columns("FLAG_SUM_NEXT_YEAR").Visible = True
                    'DataGridView1.Columns("FIRST_REGIST_RATE").Visible = True

                    DataGridView1.Columns("MEMBER_CODE").Width = 100
                    'DataGridView1.Columns("COMP_PERSON_NAME_TH").Width = 200
                    'DataGridView1.Columns("COMP_PERSON_NAME_EN").Width = 200
                    DataGridView1.Columns("STEP1").Width = 100
                    DataGridView1.Columns("STEP2").Width = 100
                    DataGridView1.Columns("STEP3").Width = 100
                    DataGridView1.Columns("STEP4").Width = 100
                    'DataGridView1.Columns("START_MONTH_FIRST_YEAR").Width = 50
                    'DataGridView1.Columns("END_MONTH_FIRST_YEAR").Width = 50
                    'DataGridView1.Columns("FIRST_YEAR_RATE").Width = 80
                    'DataGridView1.Columns("FLAG_SUM_NEXT_YEAR").Width = 50
                    'DataGridView1.Columns("FIRST_REGIST_RATE").Width = 80

                    'DataGridView1.Columns("MEMBER_CODE").ReadOnly = True
                    'DataGridView1.Columns("STEP1").ReadOnly = True
                    'DataGridView1.Columns("STEP2").ReadOnly = True

                    'ComboBox5.Text = ds.Tables("MB_MEMBER_RATE_MASTER").Rows(0).Item("CALCULATE_TYPE").ToString
                    'ComboBox6.SelectedIndex = 0
                End If
                'dt3.TableName = "MB_MEMBER"
                'If ds.Tables.Contains("MB_MEMBER") Then ds.Tables.Remove("MB_MEMBER")

                'Label2.Text = String.Format("พบ {0} รายการ", ds.Tables("MB_MEMBER").Rows.Count.ToString("#,##0"))



                'DataGridView1.Columns("INCOME_AMOUNT").de.Format = "#,##0.00"
                'DataGridView1.Columns("ASSET_AMOUNT").DefaultCellStyle.Format = "#,##0.00"
                'DataGridView1.Columns("EMPLOYEE_AMOUNT").DefaultCellStyle.Format = "#,##0.00"
                'DataGridView1.Columns("REGIST_CAPITAL").DefaultCellStyle.Format = "#,##0.00"
                'DataGridView1.Columns("PERCENT_SHAREHOLDER").DefaultCellStyle.Format = "#,##0.00"
                'DataGridView1.Columns("STEP1").DefaultCellStyle.Format = "#,##0.00"
                'DataGridView1.Columns("STEP2").DefaultCellStyle.Format = "#,##0.00"
                'DataGridView1.Columns("STEP3").DefaultCellStyle.Format = "#,##0.00"
                'DataGridView1.Columns("STEP4").DefaultCellStyle.Format = "#,##0.00"

                'DataGridView1.Columns("INCOME_AMOUNT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'DataGridView1.Columns("ASSET_AMOUNT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'DataGridView1.Columns("EMPLOYEE_AMOUNT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'DataGridView1.Columns("REGIST_CAPITAL").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'DataGridView1.Columns("PERCENT_SHAREHOLDER").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'DataGridView1.Columns("STEP1").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'DataGridView1.Columns("STEP2").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'DataGridView1.Columns("STEP3").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'DataGridView1.Columns("STEP4").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Catch ex As Exception
                MessageBox.Show(ex.Message, "PROCESS 3")
            End Try

            Label2.Text = String.Format("พบ {0} รายการ", ds.Tables("MB_MEMBER").Rows.Count.ToString("#,##0"))
        End If
    End Sub

    'Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    If ComboBox5.SelectedValue IsNot Nothing And ComboBox2.SelectedValue IsNot Nothing And ComboBox3.SelectedValue IsNot Nothing Then getMEMBER_TYPE(ComboBox5.SelectedValue.ToString, ComboBox2.SelectedValue.ToString, ComboBox3.SelectedValue.ToString)
    'End Sub

    'Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    If ComboBox5.SelectedValue IsNot Nothing And ComboBox2.SelectedValue IsNot Nothing And ComboBox3.SelectedValue IsNot Nothing And ComboBox4.SelectedValue IsNot Nothing Then
    '        Dim parameters As New Dictionary(Of String, Object)
    '        parameters.Add("@p0", ComboBox5.SelectedValue.ToString)
    '        parameters.Add("@p1", ComboBox2.SelectedValue.ToString)
    '        parameters.Add("@p2", ComboBox3.SelectedValue.ToString)
    '        parameters.Add("@p3", ComboBox4.SelectedValue.ToString)

    '        '==============================================
    '        Try
    '            Dim query1 As String = "SELECT * FROM MB_MEMBER_RATE_MASTER "
    '            'query &= "SELECT [MEMBER_TYPE_CODE], [MEMBER_TYPE_NAME], [INACTIVE] FROM [MB_MEMBER_TYPE] "
    '            query1 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) "
    '            'query1 &= "ORDER BY [MEMBER_TYPE_CODE] "

    '            Dim dt1 As DataTable = fillWebSQL(query1, parameters, "MB_MEMBER_RATE_MASTER").Copy
    '            'dt1.TableName = "MB_MEMBER_RATE_MASTER"
    '            If ds.Tables.Contains("MB_MEMBER_RATE_MASTER") Then ds.Tables.Remove("MB_MEMBER_RATE_MASTER")
    '            ds.Tables.Add(dt1)
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message, "PROCESS 1")
    '        End Try

    '        '==============================================
    '        Try
    '            Dim query2 As String = "SELECT * FROM MB_MEMBER_RATE_DETAIL "
    '            'query &= "SELECT [MEMBER_TYPE_CODE], [MEMBER_TYPE_NAME], [INACTIVE] FROM [MB_MEMBER_TYPE] "
    '            query2 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) "
    '            query2 &= "ORDER BY [RATE_SEQ] "

    '            Dim dt2 As DataTable = fillWebSQL(query2, parameters, "MB_MEMBER_RATE_DETAIL").Copy
    '            'dt2.TableName = "MB_MEMBER_RATE_DETAIL"
    '            If ds.Tables.Contains("MB_MEMBER_RATE_DETAIL") Then ds.Tables.Remove("MB_MEMBER_RATE_DETAIL")
    '            ds.Tables.Add(dt2)
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message, "PROCESS 2")
    '        End Try

    '        '==============================================
    '        'get member
    '        Try
    '            Dim query3 As String = "SELECT MB_MEMBER.MEMBER_CODE, MB_COMP_PERSON.COMP_PERSON_NAME_TH, MB_COMP_PERSON.COMP_PERSON_NAME_EN, MB_MEMBER.REGIST_DATE, "
    '            query3 &= "(SELECT TOP (1) INCOME_AMOUNT FROM MB_COMPANY_INCOME WHERE (COMP_PERSON_CODE = MB_MEMBER.COMP_PERSON_CODE) ORDER BY YEAR DESC) AS INCOME_AMOUNT, "
    '            query3 &= "MB_COMP_PERSON.ASSET_AMOUNT, MB_COMP_PERSON.EMPLOYEE_AMOUNT, MB_COMP_PERSON.REGIST_CAPITAL, MB_COMP_PERSON.PERCENT_SHAREHOLDER "
    '            query3 &= "FROM            MB_MEMBER INNER JOIN "
    '            query3 &= "                         MB_COMP_PERSON ON MB_MEMBER.OU_CODE = MB_COMP_PERSON.OU_CODE AND "
    '            query3 &= "                         MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE "
    '            query3 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) AND (MB_MEMBER.MEMBER_STATUS_CODE = 'A') "
    '            query3 &= "ORDER BY MB_MEMBER.PREFIX, MB_MEMBER.RUNNING "

    '            Dim dt3 As DataTable = fillWebSQL(query3, parameters, "MB_MEMBER").Copy
    '            'dt3.PrimaryKey = New DataColumn() {dt3.Columns("MEMBER_CODE")}
    '            dt3.Columns.Add("STEP1", Type.GetType("System.Double"))
    '            dt3.Columns.Add("STEP2", Type.GetType("System.Double"))
    '            dt3.Columns.Add("STEP3", Type.GetType("System.Double"))
    '            dt3.Columns.Add("STEP4", Type.GetType("System.Double"))

    '            If ds.Tables.Contains("MB_MEMBER") = True Then
    '                ds.Tables("MB_MEMBER").Clear()
    '                ds.Tables("MB_MEMBER").Merge(dt3)
    '                ds.Tables("MB_MEMBER").AcceptChanges()
    '            Else
    '                ds.Tables.Add(dt3)
    '                ds.Tables("MB_MEMBER").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER").Columns("MEMBER_CODE")}

    '                DataGridView1.DataSource = ds.Tables("MB_MEMBER")

    '                '==============================================
    '                For idx As Integer = 0 To DataGridView1.ColumnCount - 1
    '                    DataGridView1.Columns(idx).ReadOnly = True
    '                Next
    '                DataGridView1.Columns("STEP3").ReadOnly = False

    '                'DataGridView1.Columns("MEMBER_CODE").Visible = True
    '                'DataGridView1.Columns("STEP1").Visible = True
    '                'DataGridView1.Columns("STEP2").Visible = True
    '                'DataGridView1.Columns("STEP3").Visible = True
    '                'DataGridView1.Columns("START_MONTH_FIRST_YEAR").Visible = True
    '                'DataGridView1.Columns("END_MONTH_FIRST_YEAR").Visible = True
    '                'DataGridView1.Columns("FIRST_YEAR_RATE").Visible = True
    '                'DataGridView1.Columns("FLAG_SUM_NEXT_YEAR").Visible = True
    '                'DataGridView1.Columns("FIRST_REGIST_RATE").Visible = True

    '                DataGridView1.Columns("MEMBER_CODE").Width = 100
    '                'DataGridView1.Columns("COMP_PERSON_NAME_TH").Width = 200
    '                'DataGridView1.Columns("COMP_PERSON_NAME_EN").Width = 200
    '                DataGridView1.Columns("STEP1").Width = 100
    '                DataGridView1.Columns("STEP2").Width = 100
    '                DataGridView1.Columns("STEP3").Width = 100
    '                DataGridView1.Columns("STEP4").Width = 100
    '                'DataGridView1.Columns("START_MONTH_FIRST_YEAR").Width = 50
    '                'DataGridView1.Columns("END_MONTH_FIRST_YEAR").Width = 50
    '                'DataGridView1.Columns("FIRST_YEAR_RATE").Width = 80
    '                'DataGridView1.Columns("FLAG_SUM_NEXT_YEAR").Width = 50
    '                'DataGridView1.Columns("FIRST_REGIST_RATE").Width = 80

    '                'DataGridView1.Columns("MEMBER_CODE").ReadOnly = True
    '                'DataGridView1.Columns("STEP1").ReadOnly = True
    '                'DataGridView1.Columns("STEP2").ReadOnly = True

    '                'ComboBox5.Text = ds.Tables("MB_MEMBER_RATE_MASTER").Rows(0).Item("CALCULATE_TYPE").ToString
    '                'ComboBox6.SelectedIndex = 0
    '            End If
    '            'dt3.TableName = "MB_MEMBER"
    '            'If ds.Tables.Contains("MB_MEMBER") Then ds.Tables.Remove("MB_MEMBER")




    '            'DataGridView1.Columns("INCOME_AMOUNT").de.Format = "#,##0.00"
    '            'DataGridView1.Columns("ASSET_AMOUNT").DefaultCellStyle.Format = "#,##0.00"
    '            'DataGridView1.Columns("EMPLOYEE_AMOUNT").DefaultCellStyle.Format = "#,##0.00"
    '            'DataGridView1.Columns("REGIST_CAPITAL").DefaultCellStyle.Format = "#,##0.00"
    '            'DataGridView1.Columns("PERCENT_SHAREHOLDER").DefaultCellStyle.Format = "#,##0.00"
    '            'DataGridView1.Columns("STEP1").DefaultCellStyle.Format = "#,##0.00"
    '            'DataGridView1.Columns("STEP2").DefaultCellStyle.Format = "#,##0.00"
    '            'DataGridView1.Columns("STEP3").DefaultCellStyle.Format = "#,##0.00"
    '            'DataGridView1.Columns("STEP4").DefaultCellStyle.Format = "#,##0.00"

    '            'DataGridView1.Columns("INCOME_AMOUNT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '            'DataGridView1.Columns("ASSET_AMOUNT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '            'DataGridView1.Columns("EMPLOYEE_AMOUNT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '            'DataGridView1.Columns("REGIST_CAPITAL").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '            'DataGridView1.Columns("PERCENT_SHAREHOLDER").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '            'DataGridView1.Columns("STEP1").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '            'DataGridView1.Columns("STEP2").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '            'DataGridView1.Columns("STEP3").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '            'DataGridView1.Columns("STEP4").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message, "PROCESS 3")
    '        End Try


    '    End If
    'End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Dim f As New frmFTIFeesRule1
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim f As New frmFTIFeesRule2
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ds.Tables.Contains("MB_MEMBER") = True Then
            If ds.Tables("MB_MEMBER").Rows.Count > 0 Then
                Dim INCOME_AMOUNT As Decimal = 0
                'Dim MEMBER_YEARLY_RATE As Decimal = CDec(ds.Tables("MB_MEMBER_RATE_MASTER").Rows(0).Item("MEMBER_YEARLY_RATE"))
                Dim REGIST_DATE As Date
                Dim YEARLY_RATE As Object = DBNull.Value
                Dim idx As Integer = 0
                Dim Formula As String = String.Empty
                Try
                    For idx = 0 To ds.Tables("MB_MEMBER").Rows.Count - 1
                        YEARLY_RATE = DBNull.Value
                        If IsDBNull(ds.Tables("MB_MEMBER").Rows(idx).Item("INCOME_PER_YEAR")) = False And IsDBNull(ds.Tables("MB_MEMBER").Rows(idx).Item("REGIST_DATE")) = False Then
                            INCOME_AMOUNT = CDec(ds.Tables("MB_MEMBER").Rows(idx).Item("INCOME_PER_YEAR"))
                            If INCOME_AMOUNT >= 0 Then
                                REGIST_DATE = CDate(ds.Tables("MB_MEMBER").Rows(idx).Item("REGIST_DATE"))
                                Formula = String.Format("((START_INCOME_AMOUNT <= {0} AND {0} <= END_INCOME_AMOUNT) OR (START_INCOME_AMOUNT <= {0} AND END_INCOME_AMOUNT IS NULL)) AND START_MONTH_FIRST_YEAR <= {1} AND {1} <= END_MONTH_FIRST_YEAR", INCOME_AMOUNT, REGIST_DATE.Month)
                                YEARLY_RATE = CDec(ds.Tables("MB_MEMBER_RATE_DETAIL").Compute("SUM(MEMBER_YEARLY_RATE)", Formula))

                                ds.Tables("MB_MEMBER").Rows(idx).Item("STEP1") = YEARLY_RATE
                            Else
                                'it's negative
                            End If
                        Else
                            'it's null
                        End If

                    Next
                    MessageBox.Show("STEP 1 PROCESS HAS BEEN COMPLTED")
                Catch ex As Exception
                    MessageBox.Show(ex.Message & vbCrLf & Formula, "ERROR STEP=" & idx)
                    Exit Sub
                End Try
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ds.Tables.Contains("MB_MEMBER") = True Then
            If ds.Tables("MB_MEMBER").Rows.Count > 0 Then
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", ComboBox5.SelectedValue.ToString)
                parameters.Add("@p1", ComboBox2.SelectedValue.ToString)
                'parameters.Add("@p2", ComboBox3.SelectedValue.ToString)
                'parameters.Add("@p3", ComboBox4.SelectedValue.ToString)

                '==============================================
                Dim query2 As String = "SELECT * FROM MB_MEMBER_COMPUTE_DETAIL "
                'query &= "SELECT [MEMBER_TYPE_CODE], [MEMBER_TYPE_NAME], [INACTIVE] FROM [MB_MEMBER_TYPE] "
                'query2 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) AND (INACTIVE = 'N') "
                query2 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (ACTIVE = 1) "
                query2 &= "ORDER BY [RATE_SEQ] "

                Dim dt2 As DataTable = fillWebSQL(query2, parameters, "MB_MEMBER_COMPUTE_DETAIL").Copy
                'dt2.TableName = "MB_MEMBER_COMPUTE_DETAIL"
                'dt2.PrimaryKey = New DataColumn() {dt2.Columns("RATE_SEQ")}
                'If ds.Tables.Contains("MB_MEMBER_COMPUTE_DETAIL") Then ds.Tables.Remove("MB_MEMBER_COMPUTE_DETAIL")
                'ds.Tables.Add(dt2)
                '==============================================
                Dim idx As Integer = 0
                'Dim COMPUTE_RATE As Object = DBNull.Value
                Dim RULE_RATE As Decimal = 0
                Dim Formula As String = String.Empty
                Dim dv As New DataView(dt2)
                Dim dtCOMPUTE As DataTable
                Try
                    For idx = 0 To ds.Tables("MB_MEMBER").Rows.Count - 1
                        'COMPUTE_RATE = DBNull.Value
                        dv.RowFilter = String.Format("(MEMBER_MAIN_GROUP_CODE = '{0}') AND (MEMBER_GROUP_CODE = '{1}') AND (MEMBER_MAIN_TYPE_CODE = '{2}') AND (MEMBER_TYPE_CODE = '{3}')", ComboBox5.SelectedValue.ToString, ComboBox2.SelectedValue.ToString, ds.Tables("MB_MEMBER").Rows(idx).Item("MEMBER_MAIN_TYPE_CODE").ToString, ds.Tables("MB_MEMBER").Rows(idx).Item("MEMBER_TYPE_CODE").ToString)
                        dtCOMPUTE = dv.ToTable
                        If dtCOMPUTE.Rows.Count > 0 Then
                            For i As Integer = 0 To dtCOMPUTE.Rows.Count - 1
                                RULE_RATE = CDec(dtCOMPUTE.Rows(i).Item("QUERY_VALUE"))
                                Formula = dtCOMPUTE.Rows(i).Item("QUERY").ToString & String.Format(" AND (MEMBER_CODE = '{0}')", ds.Tables("MB_MEMBER").Rows(idx).Item("MEMBER_CODE").ToString)

                                'is it found?
                                If CInt(ds.Tables("MB_MEMBER").Compute("COUNT(MEMBER_CODE)", Formula)) > 0 Then
                                    ds.Tables("MB_MEMBER").Rows(idx).Item("STEP2") = RULE_RATE
                                End If
                            Next

                            'For i As Integer = 0 To ds.Tables("MB_MEMBER_COMPUTE_DETAIL").Rows.Count - 1
                            '    RULE_RATE = CDec(ds.Tables("MB_MEMBER_COMPUTE_DETAIL").Rows(i).Item("QUERY_VALUE"))
                            '    Formula = ds.Tables("MB_MEMBER_COMPUTE_DETAIL").Rows(i).Item("QUERY").ToString & String.Format(" AND (MEMBER_CODE = '{0}')", ds.Tables("MB_MEMBER").Rows(idx).Item("MEMBER_CODE").ToString)

                            '    'is it found?
                            '    If CInt(ds.Tables("MB_MEMBER").Compute("COUNT(MEMBER_CODE)", Formula)) > 0 Then
                            '        ds.Tables("MB_MEMBER").Rows(idx).Item("STEP2") = RULE_RATE
                            '    End If
                            'Next
                        End If


                        'If IsDBNull(ds.Tables("MB_MEMBER").Rows(idx).Item("INCOME_AMOUNT")) = False And IsDBNull(ds.Tables("MB_MEMBER").Rows(idx).Item("REGIST_DATE")) = False Then
                        '    INCOME_AMOUNT = CDec(ds.Tables("MB_MEMBER").Rows(idx).Item("INCOME_AMOUNT"))
                        '    If INCOME_AMOUNT >= 0 Then
                        '        REGIST_DATE = CDate(ds.Tables("MB_MEMBER").Rows(idx).Item("REGIST_DATE"))
                        '        Formula = String.Format("((START_INCOME_AMOUNT <= {0} AND {0} <= END_INCOME_AMOUNT) OR (START_INCOME_AMOUNT <= {0} AND END_INCOME_AMOUNT IS NULL)) AND START_MONTH_FIRST_YEAR <= {1} AND {1} <= END_MONTH_FIRST_YEAR", INCOME_AMOUNT, REGIST_DATE.Month)
                        '        YEARLY_RATE = CDec(ds.Tables("MB_MEMBER_RATE_DETAIL").Compute("SUM(MEMBER_YEARLY_RATE)", Formula))

                        '        ds.Tables("MB_MEMBER").Rows(idx).Item("STEP1") = YEARLY_RATE
                        '    Else
                        '        'it's negative
                        '    End If
                        'Else
                        '    'it's null
                        'End If

                    Next

                    MessageBox.Show("STEP 2 PROCESS HAS BEEN COMPLTED")
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "ERROR STEP=" & idx)
                    Exit Sub
                End Try
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ds.Tables.Contains("MB_MEMBER") = True Then
            If ds.Tables("MB_MEMBER").Rows.Count > 0 Then
                If OpenFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    Dim f As New frmMainExcelMatchColumns
                    f.EXCEL_PATH = OpenFileDialog1.FileName
                    f.COL_ADD = "MEMBER_CODE,STEP3"
                    If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Dim dtExcel As DataTable = f.ds.Tables(f.ComboBox1.Text)

                        For idx = 0 To dtExcel.Rows.Count - 1
                            For r As Integer = 0 To ds.Tables("MB_MEMBER").Rows.Count - 1
                                If dtExcel.Rows(idx).Item(f.DataGridView2.Rows(0).Cells("colValue").Value.ToString).ToString = ds.Tables("MB_MEMBER").Rows(r).Item("MEMBER_CODE").ToString Then
                                    ds.Tables("MB_MEMBER").Rows(r).Item("STEP3") = dtExcel.Rows(idx).Item(f.DataGridView2.Rows(1).Cells("colValue").Value.ToString)
                                    Exit For
                                End If
                            Next
                            'Dim r As DataRow = ds.Tables("MB_MEMBER").Rows.Find(dtExcel.Rows(idx).Item(f.DataGridView2.Rows(0).Cells("colValue").Value.ToString))

                            'If r IsNot Nothing Then
                            '    r.Item("STEP3") = dtExcel.Rows(idx).Item(f.DataGridView2.Rows(1).Cells("colValue").Value.ToString)
                            'End If

                            'If ds.Tables("MB_MEMBER").Rows(idx).Item("STEP3") IsNot DBNull.Value Then
                            '    ds.Tables("MB_MEMBER").Rows(idx).Item("STEP4") = ds.Tables("MB_MEMBER").Rows(idx).Item("STEP3")
                            'ElseIf ds.Tables("MB_MEMBER").Rows(idx).Item("STEP2") IsNot DBNull.Value Then
                            '    ds.Tables("MB_MEMBER").Rows(idx).Item("STEP4") = ds.Tables("MB_MEMBER").Rows(idx).Item("STEP2")
                            'ElseIf ds.Tables("MB_MEMBER").Rows(idx).Item("STEP1") IsNot DBNull.Value Then
                            '    ds.Tables("MB_MEMBER").Rows(idx).Item("STEP4") = ds.Tables("MB_MEMBER").Rows(idx).Item("STEP1")
                            'End If
                        Next
                    End If
                    f.Dispose()
                    f = Nothing
                End If
            End If
        End If


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        '
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If ds.Tables.Contains("MB_MEMBER") = True Then
            If ds.Tables("MB_MEMBER").Rows.Count > 0 Then
                For idx = 0 To ds.Tables("MB_MEMBER").Rows.Count - 1
                    If ds.Tables("MB_MEMBER").Rows(idx).Item("STEP3") IsNot DBNull.Value Then
                        ds.Tables("MB_MEMBER").Rows(idx).Item("STEP4") = ds.Tables("MB_MEMBER").Rows(idx).Item("STEP3")
                    ElseIf ds.Tables("MB_MEMBER").Rows(idx).Item("STEP2") IsNot DBNull.Value Then
                        ds.Tables("MB_MEMBER").Rows(idx).Item("STEP4") = ds.Tables("MB_MEMBER").Rows(idx).Item("STEP2")
                    ElseIf ds.Tables("MB_MEMBER").Rows(idx).Item("STEP1") IsNot DBNull.Value Then
                        ds.Tables("MB_MEMBER").Rows(idx).Item("STEP4") = ds.Tables("MB_MEMBER").Rows(idx).Item("STEP1")
                    End If
                Next
            End If
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