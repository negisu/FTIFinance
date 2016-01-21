Public Class frmFTIapprovalStatus

    Dim ds As DataSet
    Dim dv As DataView

    Friend RETIRE_TYPE As String

    Private Sub frmFTIapproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet

        ComboBox1.SelectedIndex = 0

        Try
            getMB_MEMBER_RETIRE()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        dv = New DataView(ds.Tables("MB_MEMBER_RETIRE"))

        'ds.Tables("MB_MEMBER_RETIRE").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER_RETIRE").Columns("REGIST_CODE")}

        DataGridView1.DataSource = dv

        For i As Integer = 0 To DataGridView1.ColumnCount - 1
            DataGridView1.Columns(i).Visible = False
        Next

        DataGridView1.Columns("REGIST_CODE").Visible = True
        DataGridView1.Columns("MEMBER_CODE").Visible = True
        DataGridView1.Columns("COMP_PERSON_NAME_TH").Visible = True
        DataGridView1.Columns("COMP_PERSON_NAME_EN").Visible = True
        DataGridView1.Columns("RETIRE_TYPE").Visible = True
        DataGridView1.Columns("RETIRE_REASON").Visible = True
        DataGridView1.Columns("MEMBER_STATUS_CODE").Visible = True

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMB_MEMBER_RETIRE()
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", "000")
        'parameters.Add("@p1", "100")
        'parameters.Add("@p2", "200")
        'parameters.Add("@p3", "999")
        'parameters.Add("@p1", "000")

        Dim query As String = String.Empty
        query &= "SELECT        MB_MEMBER_RETIRE.REGIST_CODE, MB_MEMBER.MEMBER_CODE, MB_COMP_PERSON.COMP_PERSON_NAME_TH, "
        query &= "                         MB_COMP_PERSON.COMP_PERSON_NAME_EN, MB_MEMBER_RETIRE.MEMBER_MAIN_GROUP_CODE, MB_MEMBER_RETIRE.MEMBER_GROUP_CODE, "
        query &= "                         MB_MEMBER_RETIRE.MEMBER_MAIN_TYPE_CODE, MB_MEMBER_RETIRE.MEMBER_TYPE_CODE, MB_MEMBER_RETIRE.COMP_PERSON_CODE, "
        query &= "                         MB_MEMBER_RETIRE.RETIRE_TYPE, MB_MEMBER_RETIRE.RETIRE_DATE, MB_MEMBER_RETIRE.RETIRE_REASON, "
        query &= "                         MB_MEMBER_RETIRE.MEMBER_STATUS_CODE, MB_MEMBER_RETIRE.MEMBER_CODE "
        query &= "FROM            MB_MEMBER_RETIRE LEFT OUTER JOIN "
        query &= "                         MB_MEMBER ON MB_MEMBER_RETIRE.REGIST_CODE = MB_MEMBER.REGIST_CODE LEFT OUTER JOIN "
        query &= "                         MB_COMP_PERSON ON MB_MEMBER_RETIRE.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE "

        If RETIRE_TYPE = "R" Then ' release deleted member
            Dim sFTI_STATUS_BLACKLIST_RELEASE() As String = getParameters(1, "FTI_STATUS_BLACKLIST_RELEASE").Split("|"c)
            query &= "WHERE        (DATEDIFF(mm, MB_MEMBER_RETIRE.APPROVE_DATE, GETDATE()) > " & CInt(getParameters(1, "FTI_STATUS_BLACKLIST_RELEASE_AGE")) & ") AND (MB_MEMBER_RETIRE.MEMBER_MAIN_GROUP_CODE IN (" & getParameters(1, "FTI_MAIN_GROUP_APPROVE") & ")) AND (MB_MEMBER_RETIRE.RETIRE_TYPE = '" & sFTI_STATUS_BLACKLIST_RELEASE(0) & "') "
        Else
            query &= "WHERE        (MB_MEMBER_RETIRE.APPROVE_DATE IS NULL) AND (MB_MEMBER_RETIRE.MEMBER_MAIN_GROUP_CODE IN (" & getParameters(1, "FTI_MAIN_GROUP_APPROVE") & ")) AND (MB_MEMBER_RETIRE.RETIRE_TYPE = '" & RETIRE_TYPE & "') "
        End If

        query &= "ORDER BY MB_MEMBER_RETIRE.RETIRE_DATE DESC"

        'query &= "SELECT    * "
        'query &= "FROM            MB_MEMBER_RETIRE "
        'query &= "WHERE        (APPROVE_DATE IS NULL) AND (MEMBER_MAIN_GROUP_CODE IN (" & getParameters(1, "FTI_MAIN_GROUP_APPROVE") & ")) "
        'query &= "ORDER BY RETIRE_DATE DESC "

        Dim dt As DataTable = New DataTable
        Try
            dt = fillWebSQL(query, parameters, "MB_MEMBER_RETIRE")
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
        End Try

        'MessageBox.Show(dt.Rows.Count.ToString)

        If ds.Tables.Contains("MB_MEMBER_RETIRE") = True Then
            ds.Tables("MB_MEMBER_RETIRE").Clear()
            ds.Tables("MB_MEMBER_RETIRE").Merge(dt)
            ds.Tables("MB_MEMBER_RETIRE").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        '
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btApprove.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            If DataGridView1.SelectedRows.Count > 0 Then
                Dim parameters As New Dictionary(Of String, Object)
                Dim query As String = String.Empty

                Dim sFTI_STATUS_RESIGN() As String = getParameters(1, "FTI_STATUS_RESIGN").Split(","c)
                Dim sFTI_STATUS_MOVE() As String = getParameters(1, "FTI_STATUS_MOVE").Split(","c)
                Dim sFTI_STATUS_BLACKLIST() As String = getParameters(1, "FTI_STATUS_BLACKLIST").Split(","c)

                For i As Integer = 0 To DataGridView1.SelectedRows.Count - 1
                    'update self
                    parameters.Clear()
                    parameters.Add("@p0", DateTimePicker1.Value)
                    parameters.Add("@p1", user_name)
                    parameters.Add("@p", DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value)

                    query = "UPDATE MB_MEMBER_RETIRE SET APPROVE_DATE = @p0, UPD_BY = @p1, UPD_DATE = GETDATE() WHERE (REGIST_CODE = @p)"
                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    If RETIRE_TYPE = "R" Then
                        Dim sFTI_STATUS_BLACKLIST_RELEASE() As String = getParameters(1, "FTI_STATUS_BLACKLIST_RELEASE").Split("|"c)
                        'log
                        saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "BLACKLIST_RELEASE", "UPDATE", "", DateTimePicker1.Value.ToString("dd/MM/yyyy", ciTH), user_name)

                        'update old member
                        parameters.Clear()
                        parameters.Add("@p0", sFTI_STATUS_BLACKLIST_RELEASE(1))
                        parameters.Add("@p", DataGridView1.SelectedRows(i).Cells("MEMBER_CODE").Value)

                        query = "UPDATE MB_MEMBER SET MEMBER_STATUS_CODE = @p0 WHERE (MEMBER_CODE = @p)"
                        Try
                            executeWebSQL(query, parameters)
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                        End Try

                        'log
                        saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "MEMBER_STATUS_CODE", "UPDATE", getCurrentStatus(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString), sFTI_STATUS_BLACKLIST_RELEASE(1), user_name)

                        'log
                        saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "MEMBER_STATUS_CODE", "UPDATE", DataGridView1.SelectedRows(i).Cells("RETIRE_TYPE").Value.ToString.ToUpper, sFTI_STATUS_BLACKLIST_RELEASE(1), user_name)
                    Else
                        Select Case DataGridView1.SelectedRows(i).Cells("RETIRE_TYPE").Value.ToString.ToUpper
                            Case sFTI_STATUS_MOVE(1).ToUpper
                                'move status

                                'duplicate record //
                                Dim running As Integer = 0
                                Try
                                    'running = CInt(client.ExecuteScalar(query, parameters))
                                    running = CInt(getParameters(1, "FTI_RUNNING_REGIST"))
                                Catch ex As Exception
                                    MessageBox.Show(ex.Message, "ERROR=getParameters=FTI_RUNNING_REGIST")
                                End Try

                                Dim d As DateTime = getSQLDate()
                                Dim REGIST_CODE As String = "F" & d.ToString("yyMM", ciTH) & running.ToString("00000")

                                'update MB_PARAMETERS
                                query = "UPDATE MB_PARAMETERS SET OBJ_VALUE = '" & running + 1 & "' WHERE OBJ_MODULE = 1 AND OBJ_NAME = 'FTI_RUNNING_REGIST'"

                                Dim result As Integer = 0
                                Try
                                    result = executeWebSQL(query, parameters)
                                Catch ex As Exception
                                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                                End Try

                                '==============================================
                                'get member code
                                parameters.Clear()
                                parameters.Add("@p0", DataGridView1.SelectedRows(i).Cells("MEMBER_MAIN_GROUP_CODE").Value)
                                parameters.Add("@p1", DataGridView1.SelectedRows(i).Cells("MEMBER_GROUP_CODE").Value)
                                parameters.Add("@p2", DataGridView1.SelectedRows(i).Cells("MEMBER_MAIN_TYPE_CODE").Value)
                                parameters.Add("@p3", DataGridView1.SelectedRows(i).Cells("MEMBER_TYPE_CODE").Value)

                                '000 000 10 11
                                'MessageBox.Show(DataGridView1.SelectedRows(i).Cells("MEMBER_MAIN_GROUP_CODE").Value.ToString & vbCrLf & DataGridView1.SelectedRows(i).Cells("MEMBER_GROUP_CODE").Value.ToString & vbCrLf & DataGridView1.SelectedRows(i).Cells("MEMBER_MAIN_TYPE_CODE").Value.ToString & vbCrLf & DataGridView1.SelectedRows(i).Cells("MEMBER_TYPE_CODE").Value.ToString)

                                query = "SELECT MEMBER_SHORT_NAME + CONVERT(varchar, (RUNNING+1)) AS MEMBER_CODE FROM MB_MEMBER_TYPE WHERE MEMBER_MAIN_GROUP_CODE = @p0 AND MEMBER_GROUP_CODE = @p1 AND MEMBER_MAIN_TYPE_CODE = @p2 AND MEMBER_TYPE_CODE = @p3"

                                Dim MEMBER_CODE As String = String.Empty
                                Try
                                    MEMBER_CODE = client.ExecuteScalar(query, parameters, user_session).ToString
                                Catch ex As Exception
                                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
                                    Exit Sub
                                End Try

                                'update running
                                query = "UPDATE MB_MEMBER_TYPE SET RUNNING = RUNNING+1 WHERE MEMBER_MAIN_GROUP_CODE = @p0 AND MEMBER_GROUP_CODE = @p1 AND MEMBER_MAIN_TYPE_CODE = @p2 AND MEMBER_TYPE_CODE = @p3 "
                                Try
                                    executeWebSQL(query, parameters)
                                Catch ex As Exception
                                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=executeWebSQL")
                                    Exit Sub
                                End Try
                                '==============================================
                                'mark new code on rows
                                parameters.Clear()
                                parameters.Add("@p0", MEMBER_CODE)
                                parameters.Add("@p", DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value)

                                query = "UPDATE MB_MEMBER_RETIRE SET MEMBER_CODE = @p0 WHERE (REGIST_CODE = @p)"
                                Try
                                    executeWebSQL(query, parameters)
                                Catch ex As Exception
                                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                                End Try

                                'MB_MEMBER
                                parameters.Clear()
                                parameters.Add("@p0", REGIST_CODE) 'new regist code
                                parameters.Add("@p1", DataGridView1.SelectedRows(i).Cells("MEMBER_MAIN_GROUP_CODE").Value) '1
                                parameters.Add("@p2", DataGridView1.SelectedRows(i).Cells("MEMBER_GROUP_CODE").Value) '2
                                parameters.Add("@p3", DataGridView1.SelectedRows(i).Cells("MEMBER_MAIN_TYPE_CODE").Value) '3
                                parameters.Add("@p4", DataGridView1.SelectedRows(i).Cells("MEMBER_TYPE_CODE").Value) '4
                                parameters.Add("@p5", MEMBER_CODE) '4
                                parameters.Add("@p6", sFTI_STATUS_MOVE(0)) '4
                                parameters.Add("@p", DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value) 'old regist code

                                query = String.Empty
                                query &= "INSERT INTO MB_MEMBER "
                                query &= "                         (OU_CODE, DIV_CODE, REGIST_CODE, MEMBER_MAIN_GROUP_CODE, MEMBER_GROUP_CODE, MEMBER_MAIN_TYPE_CODE, MEMBER_TYPE_CODE, "
                                query &= "                         MEMBER_CODE, REGIST_DATE, MEMBER_DATE, RETIRE_DATE, APPROVE_RETIRE_DATE, COMMITTION_AMNT, PROG_ID, CR_BY, CR_DATE, UPD_BY, "
                                query &= "                         UPD_DATE, MEMBER_STATUS_CODE, COMP_PERSON_CODE, STATUS_TYPE, OLD_REGIST_CODE, REDUCE_CODE, REDUCE_DATE, REMARK, UPD_PROG_ID) "
                                query &= "SELECT        OU_CODE, DIV_CODE, @p0 AS REGIST_CODE, @p1 AS MEMBER_MAIN_GROUP_CODE, @p2 AS MEMBER_GROUP_CODE, @p3 AS MEMBER_MAIN_TYPE_CODE, "
                                query &= "                         @p4 AS MEMBER_TYPE_CODE, @p5 AS MEMBER_CODE, GETDATE() AS REGIST_DATE, NULL AS MEMBER_DATE, RETIRE_DATE, NULL AS APPROVE_RETIRE_DATE, "
                                query &= "                         COMMITTION_AMNT, PROG_ID, CR_BY, CR_DATE, UPD_BY, UPD_DATE, @p6 AS MEMBER_STATUS_CODE, COMP_PERSON_CODE, STATUS_TYPE, OLD_REGIST_CODE, "
                                query &= "                         REDUCE_CODE, REDUCE_DATE, REMARK, UPD_PROG_ID "
                                query &= "FROM            MB_MEMBER AS MB_MEMBER_1 "
                                query &= "WHERE        (REGIST_CODE = @p)"

                                Try
                                    executeWebSQL(query, parameters)
                                Catch ex As Exception
                                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                                End Try

                                'MB_COMP_PERSON
                                parameters.Clear()
                                parameters.Add("@p0", REGIST_CODE)
                                parameters.Add("@p", DataGridView1.SelectedRows(i).Cells("COMP_PERSON_CODE").Value)

                                query = String.Empty
                                query &= "INSERT INTO MB_COMP_PERSON "
                                query &= "                         (COMP_PERSON_CODE, COMP_PERSON_TYPE_CODE, PREN_CODE, COMP_PERSON_NAME_TH, COMP_PERSON_NAME_EN, "
                                query &= "                         INCOME_PER_YEAR, FLAG_COMMITTEE_SUPPORT, MEMBER_IN_GROUP, FTI_MEMBER_IN_GROUP, ASSET_RANGE, ASSET_AMOUNT, REGIST_CAPITAL, "
                                query &= "                         REGIST_DATE, REGIST_NO, EMPLOYEE_AMOUNT, EMPLOYEE_YEAR, POWER_SUPPLY_RANGE, POWER_CONTROL, ELECTRIC_AMOUNT, OIL_AMOUNT, "
                                query &= "                         PRODUCE_TECHNOLOGY_TH, PRODUCE_TECHNOLOGY_EN, PRODUCE_TECHNOLOGY_DESC, PRODUCE_AMOUNT, SALE_DOMESTIC_PERCENT, "
                                query &= "                         SALE_INTERNATIONAL_PERCENT, COMP_HISTORY, COMP_LOCATION_PATH, MAIN_COMP_CODE, REMARK, PROG_ID, CR_BY, CR_DATE, UPD_BY, UPD_DATE, "
                                query &= "                         FIRST_NAME_TH, LAST_NAME_TH, FIRST_NAME_EN, LAST_NAME_EN, COMP_BUILD_DATE, COMP_BUILD_DATE_TMP, REGIST_CAPITAL_IN, "
                                query &= "                         REGIST_CAPITAL_OUT, REGIST_CAPITAL_PAY, COUNTRY_EXPORT, VENTURE_CAP_THA, VENTURE_CAP_ENG, BANK_CODE_IN, BANK_CODE_OUT, "
                                query &= "                         PERCENT_SHAREHOLDER, SHAREHOLDER_COUNTRY, PARTNER_COUNTRY, EMPLOYEE_MALE_AMT, EMPLOYEE_FEMALE_AMT, BUS_TYPE_RELATE, "
                                query &= "                         BUS_TYPE_DEALER, BUS_TYPE_IMPORTER, BUS_TYPE_EXPORTER, BUS_TYPE_OTHER, MAIN_PRODUCTS_SERVICES, TAX_ID) "
                                query &= "SELECT        @p0 AS COMP_PERSON_CODE, COMP_PERSON_TYPE_CODE, PREN_CODE, COMP_PERSON_NAME_TH, COMP_PERSON_NAME_EN, "
                                query &= "                         INCOME_PER_YEAR, FLAG_COMMITTEE_SUPPORT, MEMBER_IN_GROUP, FTI_MEMBER_IN_GROUP, ASSET_RANGE, ASSET_AMOUNT, REGIST_CAPITAL, "
                                query &= "                         GETDATE() AS REGIST_DATE, REGIST_NO, EMPLOYEE_AMOUNT, EMPLOYEE_YEAR, POWER_SUPPLY_RANGE, POWER_CONTROL, ELECTRIC_AMOUNT, OIL_AMOUNT, "
                                query &= "                         PRODUCE_TECHNOLOGY_TH, PRODUCE_TECHNOLOGY_EN, PRODUCE_TECHNOLOGY_DESC, PRODUCE_AMOUNT, SALE_DOMESTIC_PERCENT, "
                                query &= "                         SALE_INTERNATIONAL_PERCENT, COMP_HISTORY, COMP_LOCATION_PATH, MAIN_COMP_CODE, REMARK, PROG_ID, CR_BY, CR_DATE, UPD_BY, UPD_DATE, "
                                query &= "                         FIRST_NAME_TH, LAST_NAME_TH, FIRST_NAME_EN, LAST_NAME_EN, COMP_BUILD_DATE, COMP_BUILD_DATE_TMP, REGIST_CAPITAL_IN, "
                                query &= "                         REGIST_CAPITAL_OUT, REGIST_CAPITAL_PAY, COUNTRY_EXPORT, VENTURE_CAP_THA, VENTURE_CAP_ENG, BANK_CODE_IN, BANK_CODE_OUT, "
                                query &= "                         PERCENT_SHAREHOLDER, SHAREHOLDER_COUNTRY, PARTNER_COUNTRY, EMPLOYEE_MALE_AMT, EMPLOYEE_FEMALE_AMT, BUS_TYPE_RELATE, "
                                query &= "                         BUS_TYPE_DEALER, BUS_TYPE_IMPORTER, BUS_TYPE_EXPORTER, BUS_TYPE_OTHER, MAIN_PRODUCTS_SERVICES, TAX_ID "
                                query &= "FROM            MB_COMP_PERSON AS MB_COMP_PERSON_1 "
                                query &= "WHERE        (COMP_PERSON_CODE = @p)"

                                Try
                                    executeWebSQL(query, parameters)
                                Catch ex As Exception
                                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                                End Try

                                'address
                                parameters.Clear()
                                parameters.Add("@p0", REGIST_CODE)
                                parameters.Add("@p", DataGridView1.SelectedRows(i).Cells("COMP_PERSON_CODE").Value)

                                query = String.Empty
                                query &= "INSERT INTO MB_COMP_PERSON_ADDRESS "
                                query &= "                         (COMP_PERSON_CODE, ADDR_CODE, ADDR_LANG, ADDR_NO, ADDR_MOO, ADDR_SOI, ADDR_ROAD, ADDR_SUB_DISTRICT, ADDR_DISTRICT, "
                                query &= "                         ADDR_PROVINCE_CODE, ADDR_PROVINCE_NAME, ADDR_POSTCODE, ADDR_TELEPHONE, ADDR_FAX, ADDR_WEBSITE, ADDR_EMAIL, ADDR_NO_EN, "
                                query &= "                         ADDR_MOO_EN, ADDR_SOI_EN, ADDR_ROAD_EN, ADDR_SUB_DISTRICT_EN, ADDR_DISTRICT_EN, ADDR_PROVINCE_CODE_EN, ADDR_PROVINCE_NAME_EN, "
                                query &= "                         ADDR_POSTCODE_EN, ADDR_TELEPHONE_EN, ADDR_FAX_EN, ADDR_WEBSITE_EN, ADDR_EMAIL_EN, LOCAL_CODE, PROG_ID, CR_BY, CR_DATE, UPD_BY, "
                                query &= "                         UPD_DATE) "
                                query &= "SELECT        @p0 AS COMP_PERSON_CODE, ADDR_CODE, ADDR_LANG, ADDR_NO, ADDR_MOO, ADDR_SOI, ADDR_ROAD, ADDR_SUB_DISTRICT, ADDR_DISTRICT, "
                                query &= "                         ADDR_PROVINCE_CODE, ADDR_PROVINCE_NAME, ADDR_POSTCODE, ADDR_TELEPHONE, ADDR_FAX, ADDR_WEBSITE, ADDR_EMAIL, ADDR_NO_EN, "
                                query &= "                         ADDR_MOO_EN, ADDR_SOI_EN, ADDR_ROAD_EN, ADDR_SUB_DISTRICT_EN, ADDR_DISTRICT_EN, ADDR_PROVINCE_CODE_EN, ADDR_PROVINCE_NAME_EN, "
                                query &= "                         ADDR_POSTCODE_EN, ADDR_TELEPHONE_EN, ADDR_FAX_EN, ADDR_WEBSITE_EN, ADDR_EMAIL_EN, LOCAL_CODE, PROG_ID, CR_BY, CR_DATE, UPD_BY, "
                                query &= "                         UPD_DATE "
                                query &= "FROM            MB_COMP_PERSON_ADDRESS AS MB_COMP_PERSON_ADDRESS_1 "
                                query &= "WHERE        (COMP_PERSON_CODE = @p)"

                                Try
                                    executeWebSQL(query, parameters)
                                Catch ex As Exception
                                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                                End Try

                                'fee

                                'represent
                                parameters.Clear()
                                parameters.Add("@p0", REGIST_CODE)
                                parameters.Add("@p", DataGridView1.SelectedRows(i).Cells("COMP_PERSON_CODE").Value)

                                query = String.Empty
                                query &= "INSERT INTO MB_MEMBER_REPRESENT "
                                query &= "                         (REGIST_CODE, REPRESENT_SEQ, CONTACT_CODE, POSITION_SEQ, POSITION_CODE, PROG_ID, CR_BY, CR_DATE, UPD_BY, UPD_DATE, PRIORITY_SEQ, "
                                query &= "                         CONTACT_TYPE_CODE) "
                                query &= "SELECT        @p0 AS REGIST_CODE, REPRESENT_SEQ, CONTACT_CODE, POSITION_SEQ, POSITION_CODE, PROG_ID, CR_BY, CR_DATE, UPD_BY, UPD_DATE, "
                                query &= "                         PRIORITY_SEQ, CONTACT_TYPE_CODE "
                                query &= "FROM            MB_MEMBER_REPRESENT AS MB_MEMBER_REPRESENT_1 "
                                query &= "WHERE        (REGIST_CODE = @p)"

                                Try
                                    executeWebSQL(query, parameters)
                                Catch ex As Exception
                                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                                End Try

                                'position

                                'files
                                parameters.Clear()
                                parameters.Add("@p0", REGIST_CODE)
                                parameters.Add("@p", DataGridView1.SelectedRows(i).Cells("COMP_PERSON_CODE").Value)

                                query = String.Empty
                                query &= "INSERT INTO MB_MEMBER_FILES "
                                query &= "                         (CATEGORY, REGIST_CODE, DOC_TYPE, DOC_NAME, FILE_NAME, FILE_DATA) "
                                query &= "SELECT        CATEGORY, @p0 AS REGIST_CODE, DOC_TYPE, DOC_NAME, FILE_NAME, FILE_DATA "
                                query &= "FROM            MB_MEMBER_FILES AS MB_MEMBER_FILES_1 "
                                query &= "WHERE        (REGIST_CODE = @p)"

                                Try
                                    executeWebSQL(query, parameters)
                                Catch ex As Exception
                                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                                End Try

                                'update old member
                                parameters.Clear()
                                parameters.Add("@p0", sFTI_STATUS_RESIGN(1))
                                parameters.Add("@p", DataGridView1.SelectedRows(i).Cells("MEMBER_CODE").Value)

                                query = "UPDATE MB_MEMBER SET MEMBER_STATUS_CODE = @p0 WHERE (MEMBER_CODE = @p)"
                                Try
                                    executeWebSQL(query, parameters)
                                Catch ex As Exception
                                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                                End Try

                                'log
                                saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "MEMBER_CODE", "UPDATE", DataGridView1.SelectedRows(i).Cells("MEMBER_CODE").Value.ToString, MEMBER_CODE, user_name)

                                'log
                                saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "MEMBER_STATUS_CODE", "UPDATE", getCurrentStatus(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString), DataGridView1.SelectedRows(i).Cells("MEMBER_STATUS_CODE").Value.ToString, user_name)

                                'log
                                saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "MEMBER_STATUS_CODE", "UPDATE", DataGridView1.SelectedRows(i).Cells("RETIRE_TYPE").Value.ToString.ToUpper, DataGridView1.SelectedRows(i).Cells("MEMBER_STATUS_CODE").Value.ToString, user_name)
                            Case sFTI_STATUS_RESIGN(1).ToUpper
                                'update old member // mark as retire
                                If DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString = "000" Then
                                    parameters.Clear()
                                    parameters.Add("@p0", DateTimePicker1.Value)
                                    parameters.Add("@p1", DateTimePicker1.Value)
                                    parameters.Add("@p", DataGridView1.SelectedRows(i).Cells("MEMBER_CODE").Value)

                                    query = "UPDATE MB_MEMBER SET RETIRE_DATE = @p0, APPROVE_RETIRE_DATE = @p1 WHERE (MEMBER_CODE = @p)"
                                    Try
                                        executeWebSQL(query, parameters)
                                    Catch ex As Exception
                                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                                    End Try

                                    'log
                                    saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "RETIRE_DATE", "UPDATE", "", DateTimePicker1.Value.ToString("dd/MM/yyyy", ciTH), user_name)

                                    'log
                                    saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "APPROVE_RETIRE_DATE", "UPDATE", "", DateTimePicker1.Value.ToString("dd/MM/yyyy", ciTH), user_name)

                                    'update old member
                                    parameters.Clear()
                                    parameters.Add("@p0", DataGridView1.SelectedRows(i).Cells("MEMBER_STATUS_CODE").Value)
                                    parameters.Add("@p", DataGridView1.SelectedRows(i).Cells("MEMBER_CODE").Value)

                                    query = "UPDATE MB_MEMBER SET MEMBER_STATUS_CODE = @p0 WHERE (MEMBER_CODE = @p)"
                                    Try
                                        executeWebSQL(query, parameters)
                                    Catch ex As Exception
                                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                                    End Try

                                    'log
                                    saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "MEMBER_STATUS_CODE", "UPDATE", getCurrentStatus(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString), DataGridView1.SelectedRows(i).Cells("MEMBER_STATUS_CODE").Value.ToString, user_name)

                                    'log
                                    saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "MEMBER_STATUS_CODE", "UPDATE", DataGridView1.SelectedRows(i).Cells("RETIRE_TYPE").Value.ToString.ToUpper, DataGridView1.SelectedRows(i).Cells("MEMBER_STATUS_CODE").Value.ToString, user_name)
                                Else
                                    parameters.Clear()
                                    parameters.Add("@p0", DateTimePicker1.Value)
                                    parameters.Add("@p1", DateTimePicker1.Value)
                                    parameters.Add("@p", DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value)

                                    query = "UPDATE MB_MEMBER SET RETIRE_DATE = @p0, APPROVE_RETIRE_DATE = @p1 WHERE (REGIST_CODE = @p)"
                                    Try
                                        executeWebSQL(query, parameters)
                                    Catch ex As Exception
                                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                                    End Try

                                    'log
                                    saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "RETIRE_DATE", "UPDATE", "", DateTimePicker1.Value.ToString("dd/MM/yyyy", ciTH), user_name)

                                    'log
                                    saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "APPROVE_RETIRE_DATE", "UPDATE", "", DateTimePicker1.Value.ToString("dd/MM/yyyy", ciTH), user_name)

                                    'update old member
                                    parameters.Clear()
                                    parameters.Add("@p0", DataGridView1.SelectedRows(i).Cells("MEMBER_STATUS_CODE").Value)
                                    parameters.Add("@p", DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value)

                                    query = "UPDATE MB_MEMBER SET MEMBER_STATUS_CODE = @p0 WHERE (REGIST_CODE = @p)"
                                    Try
                                        executeWebSQL(query, parameters)
                                    Catch ex As Exception
                                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                                    End Try

                                    'log
                                    saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "MEMBER_STATUS_CODE", "UPDATE", getCurrentStatus(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString), DataGridView1.SelectedRows(i).Cells("MEMBER_STATUS_CODE").Value.ToString, user_name)

                                    'log
                                    saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "MEMBER_STATUS_CODE", "UPDATE", DataGridView1.SelectedRows(i).Cells("RETIRE_TYPE").Value.ToString.ToUpper, DataGridView1.SelectedRows(i).Cells("MEMBER_STATUS_CODE").Value.ToString, user_name)
                                End If
                            Case sFTI_STATUS_BLACKLIST(1).ToUpper
                                'log
                                saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "BLACKLIST_DATE", "UPDATE", "", DateTimePicker1.Value.ToString("dd/MM/yyyy", ciTH), user_name)

                                'update old member
                                parameters.Clear()
                                parameters.Add("@p0", DataGridView1.SelectedRows(i).Cells("MEMBER_STATUS_CODE").Value)
                                parameters.Add("@p", DataGridView1.SelectedRows(i).Cells("MEMBER_CODE").Value)

                                query = "UPDATE MB_MEMBER SET MEMBER_STATUS_CODE = @p0 WHERE (MEMBER_CODE = @p)"
                                Try
                                    executeWebSQL(query, parameters)
                                Catch ex As Exception
                                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                                End Try

                                'log
                                saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "MEMBER_STATUS_CODE", "UPDATE", getCurrentStatus(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString), DataGridView1.SelectedRows(i).Cells("MEMBER_STATUS_CODE").Value.ToString, user_name)

                                'log
                                saveLOGS(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "MEMBER_STATUS_CODE", "UPDATE", DataGridView1.SelectedRows(i).Cells("RETIRE_TYPE").Value.ToString.ToUpper, DataGridView1.SelectedRows(i).Cells("MEMBER_STATUS_CODE").Value.ToString, user_name)
                        End Select
                    End If
                    

                    
                Next

                'refresh grid
                getMB_MEMBER_RETIRE()
            End If
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        For i As Integer = 0 To DataGridView1.RowCount - 1
            DataGridView1.Rows(i).Selected = True
        Next
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        For i As Integer = 0 To DataGridView1.RowCount - 1
            DataGridView1.Rows(i).Selected = False
        Next
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        'If e.KeyCode = Keys.Enter Then
        Dim search As String = TextBox1.Text.Trim
        search = search.Replace(" ", "%")
        dv.RowFilter = String.Format("MEMBER_CODE LIKE '%{0}%' OR COMP_PERSON_NAME_TH LIKE '%{0}%' OR COMP_PERSON_NAME_EN LIKE '%{0}%'", search)

        'DataGridView1.Columns("MEMBER_CODE").Visible = True
        'DataGridView1.Columns("COMP_PERSON_NAME_TH").Visible = True
        'DataGridView1.Columns("COMP_PERSON_NAME_EN").Visible = True
        'DataGridView1.Columns("RETIRE_TYPE").Visible = True
        'DataGridView1.Columns("RETIRE_REASON").Visible = True
        'End If
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub
End Class