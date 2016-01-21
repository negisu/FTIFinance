Public Class frmFTICommitteeGroups

    Dim ds As DataSet
    Dim bsMain As BindingSource
    Dim bsDesc As BindingSource

    Dim WithEvents BindingEFFECTIVE_DATE, BindingAPPROVE_DATE, BindingPUT_IN_DATE, BindingMOD_DATE As Binding

    Private Sub frmFTICommitteeGroup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        ds = New DataSet

        ComboBox1.SelectedIndex = 0

        getMB_PERIOD()
        getMB_COMMITTEE_TYPE()
        getMB_DATA_STATUS()
        getMB_DATA_MOVEMENT()
        getMB_PRIORITY()
        getMB_WORK_GROUP()
        getMB_COMMITTEE_WORK_GROUP(TextBox1.Text)
        'getMB_COMMITTEE_WORK_IN_PERIOD()

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub frmFTICommitteeGroup_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fCommittee = Nothing
    End Sub

    Private Sub getMB_COMMITTEE_TYPE()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_COMMITTEE_TYPE ORDER BY SEQ"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_COMMITTEE_TYPE").Copy

        If ds.Tables.Contains("MB_COMMITTEE_TYPE") = True Then
            ds.Tables("MB_COMMITTEE_TYPE").Clear()
            ds.Tables("MB_COMMITTEE_TYPE").Merge(dt)
            ds.Tables("MB_COMMITTEE_TYPE").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox3.DataSource = ds.Tables("MB_COMMITTEE_TYPE")
        ComboBox3.DisplayMember = "COMMITTEE_BY_NAME"
        ComboBox3.ValueMember = "COMMITTEE_BY"
    End Sub

    Private Sub getMB_WORK_GROUP()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_WORK_GROUP ORDER BY SEQ"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_WORK_GROUP").Copy

        If ds.Tables.Contains("MB_WORK_GROUP") = True Then
            ds.Tables("MB_WORK_GROUP").Clear()
            ds.Tables("MB_WORK_GROUP").Merge(dt)
            ds.Tables("MB_WORK_GROUP").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox2.DataSource = ds.Tables("MB_WORK_GROUP")
        ComboBox2.DisplayMember = "WORK_GROUP_TYPE_NAME"
        ComboBox2.ValueMember = "WORK_GROUP_TYPE"
    End Sub

    Private Sub getMB_DATA_STATUS()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_DATA_STATUS ORDER BY SEQ"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_DATA_STATUS").Copy

        If ds.Tables.Contains("MB_DATA_STATUS") = True Then
            ds.Tables("MB_DATA_STATUS").Clear()
            ds.Tables("MB_DATA_STATUS").Merge(dt)
            ds.Tables("MB_DATA_STATUS").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox5.DataSource = ds.Tables("MB_DATA_STATUS")
        ComboBox5.DisplayMember = "DATA_STATUS_NAME"
        ComboBox5.ValueMember = "DATA_STATUS"
    End Sub

    Private Sub getMB_DATA_MOVEMENT()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_DATA_MOVEMENT ORDER BY SEQ"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_DATA_MOVEMENT").Copy

        If ds.Tables.Contains("MB_DATA_MOVEMENT") = True Then
            ds.Tables("MB_DATA_MOVEMENT").Clear()
            ds.Tables("MB_DATA_MOVEMENT").Merge(dt)
            ds.Tables("MB_DATA_MOVEMENT").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox7.DataSource = ds.Tables("MB_DATA_MOVEMENT")
        ComboBox7.DisplayMember = "DATA_MOVEMENT_NAME"
        ComboBox7.ValueMember = "DATA_MOVEMENT"
    End Sub

    Private Sub getMB_PRIORITY()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_PRIORITY ORDER BY SEQ"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_PRIORITY").Copy

        If ds.Tables.Contains("MB_PRIORITY") = True Then
            ds.Tables("MB_PRIORITY").Clear()
            ds.Tables("MB_PRIORITY").Merge(dt)
            ds.Tables("MB_PRIORITY").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox6.DataSource = ds.Tables("MB_PRIORITY")
        ComboBox6.DisplayMember = "PRIORITY_RATE_NAME"
        ComboBox6.ValueMember = "PRIORITY_RATE"
    End Sub

    Private Sub getMB_PERIOD()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_PERIOD ORDER BY PERIOD_NAME DESC"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_PERIOD").Copy

        If ds.Tables.Contains("MB_PERIOD") = True Then
            ds.Tables("MB_PERIOD").Clear()
            ds.Tables("MB_PERIOD").Merge(dt)
            ds.Tables("MB_PERIOD").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox4.DataSource = ds.Tables("MB_PERIOD")
        ComboBox4.DisplayMember = "PERIOD_NAME"
        ComboBox4.ValueMember = "PERIOD_CODE"
    End Sub

    Private Function getDIV(ByVal DIV_CODE As String, ByRef DIV_NAME As String, ByRef DIV_SHORT As String) As Boolean
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", DIV_CODE)

        Dim query As String = String.Empty
        query &= "SELECT DIV_CODE, DIV_NAME, ABTN_DIV_NAME FROM SU_DIVISION WHERE DIV_CODE = @p0"

        Dim dt As DataTable = fillWebSQL(query, parameters, "SU_DIVISION").Copy

        If dt.Rows.Count > 0 Then
            DIV_NAME = dt.Rows(0).Item("DIV_NAME").ToString
            DIV_SHORT = dt.Rows(0).Item("ABTN_DIV_NAME").ToString
        Else
            DIV_NAME = String.Empty
            DIV_SHORT = String.Empty
        End If

        Return True
    End Function

    'Private Sub getMB_COMMITTEE_WORK_GROUP()
    '    Dim parameters As New Dictionary(Of String, Object)

    '    Dim query As String = String.Empty
    '    query &= "SELECT * FROM MB_COMMITTEE_WORK_GROUP ORDER BY WORK_GROUP_NAME"

    '    Dim dt As DataTable = fillWebSQL(query, parameters, "MB_COMMITTEE_WORK_GROUP").Copy

    '    If ds.Tables.Contains("MB_COMMITTEE_WORK_GROUP") = True Then
    '        ds.Tables("MB_COMMITTEE_WORK_GROUP").Clear()
    '        ds.Tables("MB_COMMITTEE_WORK_GROUP").Merge(dt)
    '        ds.Tables("MB_COMMITTEE_WORK_GROUP").AcceptChanges()
    '    Else
    '        ds.Tables.Add(dt)
    '    End If

    '    'ComboBox2.DataSource = ds.Tables("MB_COMMITTEE_WORK_GROUP")
    '    'ComboBox2.DisplayMember = "PERIOD_NAME"
    '    'ComboBox2.ValueMember = "PERIOD_CODE"
    'End Sub

    'Private Sub getMB_COMMITTEE_WORK_IN_PERIOD()
    '    Dim parameters As New Dictionary(Of String, Object)

    '    Dim query As String = String.Empty
    '    query &= "SELECT * FROM MB_COMMITTEE_FROM ORDER BY COMMITTEE_FROM_DESC"

    '    Dim dt As DataTable = fillWebSQL(query, parameters, "MB_COMMITTEE_FROM").Copy

    '    If ds.Tables.Contains("MB_COMMITTEE_FROM") = True Then
    '        ds.Tables("MB_COMMITTEE_FROM").Clear()
    '        ds.Tables("MB_COMMITTEE_FROM").Merge(dt)
    '        ds.Tables("MB_COMMITTEE_FROM").AcceptChanges()
    '    Else
    '        ds.Tables.Add(dt)
    '    End If

    '    'ComboBox3.DataSource = ds.Tables("MB_COMMITTEE_FROM")
    '    'ComboBox3.DisplayMember = "COMMITTEE_FROM_DESC"
    '    'ComboBox3.ValueMember = "COMMITTEE_FROM_CODE"
    'End Sub

    Private Sub getMB_COMMITTEE_WORK_GROUP(ByVal SEARCH As String)
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Dim SEARCH1 As String = "%" & SEARCH.Replace(" ", "%") & "%"

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", SEARCH1)
        parameters.Add("@p1", SEARCH)
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = "SELECT TOP 50 * FROM MB_COMMITTEE_WORK_GROUP WHERE (WORK_GROUP_NAME LIKE @p0) OR (WORK_GROUP_CODE = @p1) ORDER BY EFFECTIVE_DATE DESC"

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "MB_COMMITTEE_WORK_GROUP")

        If ds.Tables.Contains("MB_COMMITTEE_WORK_GROUP") = True Then
            ds.Tables("MB_COMMITTEE_WORK_GROUP").Clear()
            ds.Tables("MB_COMMITTEE_WORK_GROUP").Merge(dt)
            ds.Tables("MB_COMMITTEE_WORK_GROUP").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            'primary key
            ds.Tables("MB_COMMITTEE_WORK_GROUP").PrimaryKey = New DataColumn() {ds.Tables("MB_COMMITTEE_WORK_GROUP").Columns("WORK_GROUP_CODE")}

            'blinding
            bsMain = New BindingSource(ds, "MB_COMMITTEE_WORK_GROUP")

            DataGridView1.DataSource = bsMain

            For i As Integer = 0 To DataGridView1.ColumnCount - 1
                DataGridView1.Columns(i).Visible = False
            Next
            '
            DataGridView1.Columns("WORK_GROUP_CODE").Visible = True
            DataGridView1.Columns("WORK_GROUP_NAME").Visible = True
            DataGridView1.Columns("EFFECTIVE_DATE").Visible = True
            'DataGridView1.Columns("CONTACT_LAST_NAME_TH").Visible = True

            DataGridView1.Columns("WORK_GROUP_NAME").Width = 400

            'DataGridView1.Columns("COMMITTEE_FROM_CODE").Visible = True
            'DataGridView1.Columns("MEMBER_CODE").Visible = True
            'DataGridView1.Columns("ADDR_CODE").Visible = True
            'DataGridView1.Columns("COMP_PERSON_NAME_TH").Visible = True
            'TextBox9.DataBindings.Add("Text", bsRep, "CONTACT_FIRST_NAME_TH")
            'TextBox10.DataBindings.Add("Text", bsRep, "CONTACT_LAST_NAME_TH")
            'TextBox11.DataBindings.Add("Text", bsRep, "ADDR_NO_TH")
            'TextBox12.DataBindings.Add("Text", bsRep, "ADDR_MOO_TH")
            'TextBox13.DataBindings.Add("Text", bsRep, "ADDR_SOI_TH")
            'TextBox14.DataBindings.Add("Text", bsRep, "ADDR_ROAD_TH")
            'TextBox50.DataBindings.Add("Text", bsRep, "ADDR_SUB_DISTRICT_TH")
            'TextBox51.DataBindings.Add("Text", bsRep, "ADDR_DISTRICT_TH")
            'TextBox49.DataBindings.Add("Text", bsRep, "ADDR_PROVINCE_NAME_TH")

            'TextBox30.DataBindings.Add("Text", bsRep, "CONTACT_FIRST_NAME_EN")
            'TextBox31.DataBindings.Add("Text", bsRep, "CONTACT_LAST_NAME_EN")
            'TextBox32.DataBindings.Add("Text", bsRep, "ADDR_NO_EN")
            'TextBox33.DataBindings.Add("Text", bsRep, "ADDR_MOO_EN")
            'TextBox29.DataBindings.Add("Text", bsRep, "ADDR_SOI_EN")
            'TextBox28.DataBindings.Add("Text", bsRep, "ADDR_ROAD_EN")
            'TextBox52.DataBindings.Add("Text", bsRep, "ADDR_SUB_DISTRICT_EN")
            'TextBox53.DataBindings.Add("Text", bsRep, "ADDR_DISTRICT_EN")
            'TextBox54.DataBindings.Add("Text", bsRep, "ADDR_PROVINCE_NAME_EN")

            'TextBox15.DataBindings.Add("Text", bsRep, "PERSONAL_ID")
            'TextBox17.DataBindings.Add("Text", bsRep, "ADDR_TELEPHONE")
            'TextBox19.DataBindings.Add("Text", bsRep, "ADDR_FAX")
            'TextBox20.DataBindings.Add("Text", bsRep, "ADDR_EMAIL")

            'tbCONTACT_CODE.DataBindings.Add("Text", bsMain, "CONTACT_CODE")
            TextBox2.DataBindings.Add("Text", bsMain, "WORK_GROUP_CODE")
            TextBox3.DataBindings.Add("Text", bsMain, "WORK_GROUP_NAME")
            TextBox4.DataBindings.Add("Text", bsMain, "WORK_GROUP_SHORT_NAME")

            BindingEFFECTIVE_DATE = New Binding("Text", bsMain, "EFFECTIVE_DATE")
            MaskedTextBox1.DataBindings.Add(BindingEFFECTIVE_DATE)

            TextBox5.DataBindings.Add("Text", bsMain, "DIV_CODE")

            ComboBox2.DataBindings.Add(New Binding("SelectedValue", bsMain, "WORK_GROUP_TYPE", True, DataSourceUpdateMode.OnValidation))

            TextBox17.DataBindings.Add("Text", bsMain, "REMARK")

            ''TextBox6.DataBindings.Add("Text", bsMain, "WORK_GROUP_TYPE")


            'TextBox7.DataBindings.Add("Text", bsMain, "CONTACT_LAST_NAME_EN")
            'TextBox8.DataBindings.Add("Text", bsMain, "MEMBER_CODE")
            'TextBox9.DataBindings.Add("Text", bsMain, "COMP_PERSON_NAME_TH")

            'tbCOMP_PERSON_CODE.DataBindings.Add("Text", bsMain, "COMP_PERSON_CODE")
            'tbADDR_CODE.DataBindings.Add("Text", bsMain, "ADDR_CODE")

            'TextBox23.DataBindings.Add("Text", bsMain, "ADDR_NO_TH")
            'TextBox28.DataBindings.Add("Text", bsMain, "ADDR_MOO_TH")
            'TextBox16.DataBindings.Add("Text", bsMain, "ADDR_SOI_TH")
            'TextBox14.DataBindings.Add("Text", bsMain, "ADDR_ROAD_TH")
            'TextBox50.DataBindings.Add("Text", bsMain, "ADDR_SUB_DISTRICT_TH")
            'TextBox51.DataBindings.Add("Text", bsMain, "ADDR_DISTRICT_TH")
            ''ComboBox17.DataBindings.Add("Text", bsMain, "ADDR_SUB_DISTRICT_TH")
            ''ComboBox19.DataBindings.Add("Text", bsMain, "ADDR_DISTRICT_TH")
            'TextBox49.DataBindings.Add("Text", bsMain, "ADDR_PROVINCE_NAME_TH")

            'TextBox33.DataBindings.Add("Text", bsMain, "ADDR_NO_EN")
            'TextBox55.DataBindings.Add("Text", bsMain, "ADDR_MOO_EN")
            'TextBox30.DataBindings.Add("Text", bsMain, "ADDR_SOI_EN")
            'TextBox29.DataBindings.Add("Text", bsMain, "ADDR_ROAD_EN")
            'TextBox52.DataBindings.Add("Text", bsMain, "ADDR_SUB_DISTRICT_EN")
            'TextBox53.DataBindings.Add("Text", bsMain, "ADDR_DISTRICT_EN")
            ''ComboBox18.DataBindings.Add("Text", bsMain, "ADDR_SUB_DISTRICT_EN")
            ''ComboBox20.DataBindings.Add("Text", bsMain, "ADDR_DISTRICT_EN")
            'TextBox54.DataBindings.Add("Text", bsMain, "ADDR_PROVINCE_NAME_EN")

            'TextBox15.DataBindings.Add("Text", bsMain, "PERSONAL_ID")
            'TextBox17.DataBindings.Add("Text", bsMain, "ADDR_TELEPHONE")
            'TextBox19.DataBindings.Add("Text", bsMain, "ADDR_FAX")
            'TextBox20.DataBindings.Add("Text", bsMain, "ADDR_EMAIL")

            'TextBox18.DataBindings.Add("Text", bsMain, "ADDR_POSTCODE")

            'CheckBox1.DataBindings.Add(New Binding("Checked", bsMain, "FLAG_NON_GROUP", True, DataSourceUpdateMode.OnValidation))


            'ComboBox3.DataBindings.Add(New Binding("SelectedValue", bsMain, "COMMITTEE_FROM_CODE", True, DataSourceUpdateMode.OnValidation))
            'ComboBox4.DataBindings.Add(New Binding("SelectedValue", bsMain, "MEMBER_MAIN_GROUP_CODE", True, DataSourceUpdateMode.OnValidation))
            'ComboBox5.DataBindings.Add(New Binding("SelectedValue", bsMain, "MEMBER_GROUP_CODE", True, DataSourceUpdateMode.OnValidation))
            'ComboBox6.DataBindings.Add(New Binding("SelectedValue", bsMain, "GROUP_POSITION_CODE", True, DataSourceUpdateMode.OnValidation))
            'ComboBox7.DataBindings.Add(New Binding("SelectedValue", bsMain, "ADDR_CODE", True, DataSourceUpdateMode.OnValidation))

            'ComboBox10.DataBindings.Add(New Binding("SelectedValue", bsMain, "CONTACT_PRENAME_CODE", True, DataSourceUpdateMode.OnValidation))
            'ComboBox11.DataBindings.Add(New Binding("Text", bsMain, "SEX", True, DataSourceUpdateMode.OnValidation))
            ''ComboBox10.DataBindings.Add(New Binding("SelectedValue", bsRep, "CONTACT_PRENAME_CODE", True, DataSourceUpdateMode.OnValidation))
            ''ComboBox11.DataBindings.Add(New Binding("SelectedValue", bsRep, "SEX", True, DataSourceUpdateMode.OnValidation))

            ''TextBox18.DataBindings.Add("Text", bsRep, "ADDR_POSTCODE")

            gridControl1_CurrentRowChanged(Nothing, Nothing)

            AddHandler DataGridView1.SelectionChanged, AddressOf gridControl1_CurrentRowChanged
        End If
    End Sub

    Private Shared Sub Date_Format(sender As Object, e As ConvertEventArgs) Handles BindingEFFECTIVE_DATE.Format, BindingAPPROVE_DATE.Format, BindingPUT_IN_DATE.Format
        If e.Value Is Nothing Then
            e.Value = ""
        Else
            If IsDBNull(e.Value) = False Then
                e.Value = DirectCast(e.Value, DateTime).ToString("dd/MM/yyyy")
            Else
                e.Value = ""
            End If
        End If
    End Sub

    Private Shared Sub Date_Parse(sender As Object, e As ConvertEventArgs) Handles BindingEFFECTIVE_DATE.Parse, BindingAPPROVE_DATE.Parse, BindingPUT_IN_DATE.Parse
        If e.Value.ToString() = "  /  /" Then
            e.Value = Nothing
        End If
    End Sub

    Private Shared Sub modDate_Format(sender As Object, e As ConvertEventArgs) Handles BindingMOD_DATE.Format
        If e.Value Is Nothing Then
            e.Value = ""
        Else
            If IsDBNull(e.Value) = False Then
                e.Value = DirectCast(e.Value, DateTime).ToString("dd/MM/yyyy HH:mm")
            Else
                e.Value = ""
            End If
        End If
    End Sub

    Private Shared Sub modDate_Parse(sender As Object, e As ConvertEventArgs) Handles BindingMOD_DATE.Parse
        If e.Value.ToString() = "  /  /   :  " Then
            e.Value = Nothing
        End If
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            getMB_COMMITTEE_WORK_GROUP(TextBox1.Text)
        End If
    End Sub

    Private Sub gridControl1_CurrentRowChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ds.Tables("MB_COMMITTEE_WORK_GROUP").RejectChanges()

        'DateTimePicker1.Value = If() '.DataBindings.Add("Value", bsMain, "EFFECTIVE_DATE")

        If TextBox5.TextLength > 0 Then getDIV(TextBox5.Text, TextBox6.Text, "")

        If TextBox2.TextLength > 0 Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim parameters As New Dictionary(Of String, Object)
            Dim query As String = String.Empty

            'DIV_CODE
            'TextBox6.Text = String.Empty
            'If TextBox5.TextLength > 0 Then
            '    parameters.Clear()
            '    parameters.Add("@p0", TextBox5.Text)
            '    'parameters.Add("@p1", ComboBox7.SelectedValue)
            '    query = "SELECT DIV_NAME FROM SU_DIVISIONE WHERE DIV_CODE = @p0"

            '    Dim objDIV_NAME As Object = Nothing

            '    Try
            '        objDIV_NAME = client.ExecuteScalar(query, parameters, user_session)
            '    Catch ex As Exception
            '        MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar DIV_NAME")
            '    End Try

            '    If objDIV_NAME IsNot Nothing Then
            '        If objDIV_NAME IsNot DBNull.Value Then
            '            TextBox6.Text = objDIV_NAME.ToString
            '        End If
            '    End If
            'End If

            'get MB_COMMITTEE_WORK_IN_PERIOD
            parameters.Clear()
            parameters.Add("@p0", TextBox2.Text)

            query = "SELECT * FROM MB_COMMITTEE_WORK_IN_PERIOD WHERE WORK_GROUP_CODE = @p0"

            Dim dtMB_COMMITTEE_WORK_IN_PERIOD As DataTable = New DataTable

            dtMB_COMMITTEE_WORK_IN_PERIOD = fillWebSQL(query, parameters, "MB_COMMITTEE_WORK_IN_PERIOD")

            If ds.Tables.Contains("MB_COMMITTEE_WORK_IN_PERIOD") = True Then
                ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD").Clear()
                ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD").Merge(dtMB_COMMITTEE_WORK_IN_PERIOD)
                ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD").AcceptChanges()
            Else
                ds.Tables.Add(dtMB_COMMITTEE_WORK_IN_PERIOD)

                'primary key
                ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD").PrimaryKey = New DataColumn() {ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD").Columns("DIV_CODE"), ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD").Columns("WORK_GROUP_CODE"), ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD").Columns("PERIOD_SEQ"), ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD").Columns("PERIOD_CODE")}

                'DataGridView2.DataSource = ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD")

                'DataGridView2.Columns("WORK_GROUP_NAME").Width = 150
                'DataGridView2.Columns("POSITION_NAME_TH").Width = 150

                bsDesc = New BindingSource(ds, "MB_COMMITTEE_WORK_IN_PERIOD")

                TextBox7.DataBindings.Add("Text", bsDesc, "PERIOD_CODE")

                BindingAPPROVE_DATE = New Binding("Text", bsDesc, "APPROVE_DATE")
                MaskedTextBox2.DataBindings.Add(BindingAPPROVE_DATE)

                BindingPUT_IN_DATE = New Binding("Text", bsDesc, "PUT_IN_DATE")
                MaskedTextBox3.DataBindings.Add(BindingPUT_IN_DATE)

                TextBox9.DataBindings.Add("Text", bsDesc, "MEMBER_WITH")
                TextBox10.DataBindings.Add("Text", bsDesc, "CHAIRMAN_NAME")

                ComboBox3.DataBindings.Add(New Binding("SelectedValue", bsDesc, "COMMITTEE_BY", True, DataSourceUpdateMode.OnValidation))
                ComboBox4.DataBindings.Add(New Binding("SelectedValue", bsDesc, "PERIOD_CODE", True, DataSourceUpdateMode.OnValidation))

                TextBox11.DataBindings.Add("Text", bsDesc, "DIV_CODE")
                TextBox14.DataBindings.Add("Text", bsDesc, "WORK_GROUP_FIELD")
                TextBox15.DataBindings.Add("Text", bsDesc, "CONTACT_WORK_GROUP")
                TextBox16.DataBindings.Add("Text", bsDesc, "CONTACT_FTI")

                ComboBox5.DataBindings.Add(New Binding("SelectedValue", bsDesc, "DATA_STATUS", True, DataSourceUpdateMode.OnValidation))
                ComboBox6.DataBindings.Add(New Binding("SelectedValue", bsDesc, "PRIORITY_RATE", True, DataSourceUpdateMode.OnValidation))
                TextBox8.DataBindings.Add("Text", bsDesc, "UPD_BY")

                ComboBox7.DataBindings.Add(New Binding("SelectedValue", bsDesc, "DATA_MOVEMENT", True, DataSourceUpdateMode.OnValidation))

                BindingMOD_DATE = New Binding("Text", bsDesc, "UPD_DATE")
                MaskedTextBox4.DataBindings.Add(BindingMOD_DATE)
            End If

            If TextBox11.TextLength > 0 Then getDIV(TextBox11.Text, TextBox12.Text, TextBox13.Text)

            'meeting
            parameters.Clear()
            parameters.Add("@p0", TextBox2.Text)

            query = "SELECT * FROM MB_COMMITTEE_MEETING WHERE WORK_GROUP_CODE = @p0 ORDER BY MEETING_SEQ DESC"

            Dim dtMB_COMMITTEE_MEETING As DataTable = New DataTable

            dtMB_COMMITTEE_MEETING = fillWebSQL(query, parameters, "MB_COMMITTEE_MEETING")

            If ds.Tables.Contains("MB_COMMITTEE_MEETING") = True Then
                ds.Tables("MB_COMMITTEE_MEETING").Clear()
                ds.Tables("MB_COMMITTEE_MEETING").Merge(dtMB_COMMITTEE_MEETING)
                ds.Tables("MB_COMMITTEE_MEETING").AcceptChanges()
            Else
                ds.Tables.Add(dtMB_COMMITTEE_MEETING)

                'primary key
                ds.Tables("MB_COMMITTEE_MEETING").PrimaryKey = New DataColumn() {ds.Tables("MB_COMMITTEE_MEETING").Columns("DIV_CODE"), ds.Tables("MB_COMMITTEE_MEETING").Columns("WORK_GROUP_CODE"), ds.Tables("MB_COMMITTEE_MEETING").Columns("PERIOD_SEQ"), ds.Tables("MB_COMMITTEE_MEETING").Columns("PERIOD_CODE"), ds.Tables("MB_COMMITTEE_MEETING").Columns("MEETING_SEQ")}

                DataGridView2.DataSource = ds.Tables("MB_COMMITTEE_MEETING")

                For i As Integer = 0 To DataGridView2.ColumnCount - 1
                    DataGridView2.Columns(i).Visible = False
                Next

                DataGridView2.Columns("MEETING_SEQ").Visible = True
                DataGridView2.Columns("REMARK").Visible = True

                DataGridView2.Columns("REMARK").Width = 300
                'DataGridView2.Columns("POSITION_NAME_TH").Width = 150

                Dim colMEETING_DATE As New CalendarColumn()
                colMEETING_DATE.HeaderText = "MEETING_DATE"
                colMEETING_DATE.DataPropertyName = "MEETING_DATE"
                DataGridView2.Columns.Insert(6, colMEETING_DATE)
            End If

            DataGridView2.Columns("OU_CODE").Visible = False

            'represent
            parameters.Clear()
            parameters.Add("@p0", TextBox2.Text)

            query = "SELECT        MB_COMMITTEE_REPRESENT.DIV_CODE, MB_COMMITTEE_REPRESENT.WORK_GROUP_CODE, MB_COMMITTEE_REPRESENT.PERIOD_SEQ, MB_COMMITTEE_REPRESENT.PERIOD_CODE, "
            query &= "                         MB_COMMITTEE_REPRESENT.REPRESENT_SEQ, MB_COMMITTEE_REPRESENT.REPRESENT_CODE, MB_COMMITTEE_REPRESENT.POSITION_CODE, MB_COMMITTEE_REPRESENT.POSITION_TYPE, "
            query &= "                         MB_PRENAME.PRENAME_TH, MB_CONTACT.CONTACT_FIRST_NAME_TH, MB_CONTACT.CONTACT_LAST_NAME_TH, MB_PRENAME.PRENAME_EN, MB_CONTACT.CONTACT_FIRST_NAME_EN, "
            query &= "                         MB_CONTACT.CONTACT_LAST_NAME_EN, MB_POSITION.POSITION_NAME_TH, MB_POSITION.POSITION_NAME_EN, MB_COMMITTEE_REPRESENT.REMARK "
            query &= "FROM            MB_POSITION RIGHT OUTER JOIN "
            query &= "                         MB_COMMITTEE_REPRESENT ON MB_POSITION.POSITION_CODE = MB_COMMITTEE_REPRESENT.POSITION_CODE AND "
            query &= "                         MB_POSITION.POSITION_TYPE = MB_COMMITTEE_REPRESENT.POSITION_TYPE LEFT OUTER JOIN "
            query &= "                         MB_PRENAME RIGHT OUTER JOIN "
            query &= "                         MB_CONTACT ON MB_PRENAME.PRENAME_CODE = MB_CONTACT.CONTACT_PRENAME_CODE ON MB_COMMITTEE_REPRESENT.REPRESENT_CODE = MB_CONTACT.CONTACT_CODE "
            query &= "WHERE        (MB_COMMITTEE_REPRESENT.WORK_GROUP_CODE = @p0) "
            query &= "ORDER BY MB_COMMITTEE_REPRESENT.REPRESENT_SEQ"

            Dim dtMB_COMMITTEE_REPRESENT As DataTable = New DataTable

            dtMB_COMMITTEE_MEETING = fillWebSQL(query, parameters, "MB_COMMITTEE_REPRESENT")

            If ds.Tables.Contains("MB_COMMITTEE_REPRESENT") = True Then
                ds.Tables("MB_COMMITTEE_REPRESENT").Clear()
                ds.Tables("MB_COMMITTEE_REPRESENT").Merge(dtMB_COMMITTEE_MEETING)
                ds.Tables("MB_COMMITTEE_REPRESENT").AcceptChanges()
            Else
                ds.Tables.Add(dtMB_COMMITTEE_MEETING)

                'primary key
                ds.Tables("MB_COMMITTEE_REPRESENT").PrimaryKey = New DataColumn() {ds.Tables("MB_COMMITTEE_REPRESENT").Columns("DIV_CODE"), ds.Tables("MB_COMMITTEE_REPRESENT").Columns("WORK_GROUP_CODE"), ds.Tables("MB_COMMITTEE_REPRESENT").Columns("PERIOD_SEQ"), ds.Tables("MB_COMMITTEE_REPRESENT").Columns("PERIOD_CODE"), ds.Tables("MB_COMMITTEE_REPRESENT").Columns("REPRESENT_SEQ")}

                DataGridView3.DataSource = ds.Tables("MB_COMMITTEE_REPRESENT")

                For i As Integer = 0 To DataGridView3.ColumnCount - 1
                    DataGridView3.Columns(i).Visible = False
                    DataGridView3.Columns(i).ReadOnly = True
                Next
                DataGridView3.Columns("REMARK").ReadOnly = False

                DataGridView3.Columns("REPRESENT_SEQ").Visible = True
                DataGridView3.Columns("REPRESENT_CODE").Visible = True
                DataGridView3.Columns("PRENAME_TH").Visible = True
                DataGridView3.Columns("CONTACT_FIRST_NAME_TH").Visible = True
                DataGridView3.Columns("CONTACT_LAST_NAME_TH").Visible = True
                DataGridView3.Columns("POSITION_NAME_TH").Visible = True
                DataGridView3.Columns("REMARK").Visible = True

                DataGridView3.Columns("CONTACT_FIRST_NAME_TH").Width = 200
                DataGridView3.Columns("CONTACT_LAST_NAME_TH").Width = 200
                DataGridView3.Columns("POSITION_NAME_TH").Width = 150
            End If

            DataGridView3.Columns("DIV_CODE").Visible = False
        End If

    End Sub

    Private Sub btProfileSave_Click(sender As Object, e As EventArgs) Handles btCommitteeGroupsSave.Click
        bsMain.EndEdit()
        bsDesc.EndEdit()

        If MessageBox.Show("ยืนยันที่จะ" & btCommitteeGroupsSave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            'Dim row As DataRow = ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD").Rows.Find(New Object() {DataGridView1.CurrentRow.Cells("DIV_CODE").Value, DataGridView1.CurrentRow.Cells("WORK_GROUP_CODE").Value, DataGridView1.CurrentRow.Cells("PERIOD_SEQ").Value, DataGridView1.CurrentRow.Cells("PERIOD_CODE").Value})
            'ds.Tables("MB_COMMITTEE_PERIOD").PrimaryKey = New DataColumn() {ds.Tables("MB_COMMITTEE_PERIOD").Columns("PERIOD_SEQ"), ds.Tables("MB_COMMITTEE_PERIOD").Columns("PERIOD_CODE"), ds.Tables("MB_COMMITTEE_PERIOD").Columns("CONTACT_CODE")}

            'update it
            'query = String.Empty

            'Dim cols() As String = "PERIOD_SEQ,PERIOD_CODE,CONTACT_CODE,COMMITTEE_FROM_CODE,MEMBER_MAIN_GROUP_CODE,MEMBER_GROUP_CODE,GROUP_POSITION_CODE,GROUP_POSITION_TYPE,MEMBER_CODE,COMP_PERSON_CODE,ADDR_CODE".Split(","c)
            'For i As Integer = 0 To cols.Length - 1
            '    If row.HasVersion(DataRowVersion.Current) = True Then
            '        If row(cols(i), DataRowVersion.Original).ToString <> row(cols(i), DataRowVersion.Current).ToString Then
            '            'save it
            '            'DataGridView1.CurrentRow.Cells("PERIOD_CODE").Value.ToString & "=" &
            '            saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_COMMITTEE_PERIOD", cols(i), "UPDATE", If(IsDBNull(row(cols(i), DataRowVersion.Original)), "", row(cols(i), DataRowVersion.Original)), If(IsDBNull(row(cols(i), DataRowVersion.Current)), "", row(cols(i), DataRowVersion.Current)), user_name)
            '        End If
            '    End If
            'Next

            'update it
            Dim parameters As New Dictionary(Of String, Object)
            Dim query As String = String.Empty
            'save MB_MEMBERS

            'query = String.Empty

            query &= "SELECT * FROM MB_COMMITTEE_WORK_GROUP"

            If ds.Tables("MB_COMMITTEE_WORK_GROUP").GetChanges IsNot Nothing Then
                Try
                    updateWebSQL(query, parameters, ds.Tables("MB_COMMITTEE_WORK_GROUP"))
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=updateWebSQL")
                End Try
            End If

            ds.Tables("MB_COMMITTEE_WORK_GROUP").AcceptChanges()

            '========================================================

            parameters.Clear()
            parameters.Add("@p0", TextBox2.Text)

            query = "SELECT * FROM MB_COMMITTEE_WORK_IN_PERIOD WHERE WORK_GROUP_CODE = @p0"

            If ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD").GetChanges IsNot Nothing Then
                For i As Integer = 0 To ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD").GetChanges.Rows.Count - 1
                    ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD").GetChanges.Rows(i).Item("UPD_BY") = user_name
                    ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD").GetChanges.Rows(i).Item("UPD_DATE") = getSQLDate()
                Next

                Try
                    updateWebSQL(query, parameters, ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD"))
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=updateWebSQL")
                End Try
            End If

            ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD").AcceptChanges()

            'refresh grid
            'getMB_COMMITTEE_PERIOD(TextBox1.Text)
            gridControl1_CurrentRowChanged(sender, e)

            MessageBox.Show("บันทึกเสร็จสิ้น")
        End If
    End Sub

    Private Sub btContacts_Click(sender As Object, e As EventArgs) Handles btDIV1.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            Dim f As New frmMainDivs
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                TextBox5.Text = f.DataGridView1.CurrentRow.Cells("DIV_CODE").Value.ToString
                TextBox6.Text = f.DataGridView1.CurrentRow.Cells("DIV_NAME").Value.ToString
            End If
            f.Dispose()
            f = Nothing
        End If
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        'DataGridView1.EndEdit()
        bsMain.EndEdit()
        bsDesc.EndEdit()

        Dim f As New frmFTICommitteeGroupsNew
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            If MessageBox.Show("ยืนยันที่จะ" & btNew.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'Dim row As DataRow = ds.Tables("MB_COMMITTEE_PERIOD").Rows.Find(New Object() {DataGridView2.CurrentRow.Cells("PERIOD_SEQ").Value, DataGridView2.CurrentRow.Cells("PERIOD_CODE").Value, DataGridView2.CurrentRow.Cells("CONTACT_CODE").Value})

                Dim parameters As New Dictionary(Of String, Object)
                Dim query As String = String.Empty

                parameters.Clear()
                'parameters.Add("@p0", TextBox7.Text)

                query = "SELECT TOP 1 WORK_GROUP_CODE FROM MB_COMMITTEE_WORK_GROUP WHERE ISNUMERIC(WORK_GROUP_CODE) = 1 ORDER BY WORK_GROUP_CODE DESC"

                Dim objWORK_GROUP_CODE As Object = Nothing

                Try
                    objWORK_GROUP_CODE = client.ExecuteScalar(query, parameters, user_session)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar WORK_GROUP_CODE")
                End Try

                If objWORK_GROUP_CODE IsNot Nothing Then
                    If objWORK_GROUP_CODE IsNot DBNull.Value Then
                        'TextBox8.Text = objWORK_GROUP_CODE.ToString
                    Else
                        objWORK_GROUP_CODE = 1
                    End If
                Else
                    objWORK_GROUP_CODE = 1
                End If

                query = "INSERT INTO MB_COMMITTEE_WORK_GROUP (WORK_GROUP_CODE, WORK_GROUP_NAME, WORK_GROUP_SHORT_NAME, EFFECTIVE_DATE, DIV_CODE) VALUES (@p0,@p1,@p2,@p3,@p4)"
                parameters.Clear()
                parameters.Add("@p0", CInt(objWORK_GROUP_CODE) + 1)
                parameters.Add("@p1", f.TextBox3.Text)
                parameters.Add("@p2", f.TextBox4.Text)
                parameters.Add("@p3", CDate(f.MaskedTextBox1.Text))
                parameters.Add("@p4", f.TextBox5.Text)
                'parameters.Add("@p5", 2)

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                query = "INSERT INTO MB_COMMITTEE_WORK_IN_PERIOD (DIV_CODE, WORK_GROUP_CODE, PERIOD_SEQ, PERIOD_CODE, UPD_BY, UPD_DATE) VALUES (@p0,@p1,@p2,@p3,@p4,@p5)"
                parameters.Clear()
                parameters.Add("@p0", f.TextBox5.Text)
                parameters.Add("@p1", CInt(objWORK_GROUP_CODE) + 1)
                parameters.Add("@p2", 1)
                parameters.Add("@p3", f.TextBox7.Text)
                parameters.Add("@p4", user_name)
                parameters.Add("@p5", getSQLDate())
                'parameters.Add("@p3", f.TextBox9.Text)
                'parameters.Add("@p4", f.TextBox30.Text)
                'parameters.Add("@p5", 2)

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history 
                'saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_COMMITTEE_PERIOD", "PERIOD_CODE", "ADD", "", f.ComboBox2.SelectedValue.ToString, user_name)
                'saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_COMMITTEE_PERIOD", "CONTACT_CODE", "ADD", "", f.TextBox30.Text, user_name)

                'refresh grid
                TextBox1.Text = (CInt(objWORK_GROUP_CODE) + 1).ToString
                getMB_COMMITTEE_WORK_GROUP(TextBox1.Text)
                gridControl1_CurrentRowChanged(sender, e)
            End If
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            'DataGridView1.EndEdit()
            bsMain.EndEdit()
            bsDesc.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btDelete.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim rowMB_COMMITTEE_WORK_GROUP As DataRow = ds.Tables("MB_COMMITTEE_WORK_GROUP").Rows.Find(New Object() {DataGridView1.CurrentRow.Cells("WORK_GROUP_CODE").Value})

                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", rowMB_COMMITTEE_WORK_GROUP("PERIOD_SEQ"))
                'parameters.Add("@p1", row("PERIOD_CODE"))
                'parameters.Add("@p2", row("CONTACT_CODE"))
                'parameters.Add("@p3", row("WORK_GROUP_CODE"))
                'parameters.Add("@p4", row("POSITION_CODE"))
                'parameters.Add("@p5", row("POSITION_TYPE"))

                Dim query As String = "DELETE FROM MB_COMMITTEE_WORK_GROUP WHERE (WORK_GROUP_CODE = @p0)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                Dim rowMB_COMMITTEE_WORK_IN_PERIOD As DataRow = ds.Tables("MB_COMMITTEE_WORK_IN_PERIOD").Rows.Find(New Object() {DataGridView1.CurrentRow.Cells("DIV_CODE").Value, DataGridView1.CurrentRow.Cells("WORK_GROUP_CODE").Value, DataGridView1.CurrentRow.Cells("PERIOD_SEQ").Value, DataGridView1.CurrentRow.Cells("PERIOD_CODE").Value})

                'get latest number
                parameters.Clear()
                'Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", rowMB_COMMITTEE_WORK_IN_PERIOD("DIV_CODE"))
                parameters.Add("@p1", rowMB_COMMITTEE_WORK_IN_PERIOD("WORK_GROUP_CODE"))
                parameters.Add("@p2", rowMB_COMMITTEE_WORK_IN_PERIOD("PERIOD_SEQ"))
                parameters.Add("@p3", rowMB_COMMITTEE_WORK_IN_PERIOD("PERIOD_CODE"))
                'parameters.Add("@p3", row("WORK_GROUP_CODE"))
                'parameters.Add("@p4", row("POSITION_CODE"))
                'parameters.Add("@p5", row("POSITION_TYPE"))

                query = "DELETE FROM MB_COMMITTEE_WORK_IN_PERIOD WHERE (DIV_CODE = @p0) AND (WORK_GROUP_CODE = @p1) AND (PERIOD_SEQ = @p2) AND (PERIOD_CODE = @p3)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history 
                'saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_COMMITTEE_PERIOD", "PERIOD_CODE", "DELETE", row("PERIOD_CODE").ToString, "", user_name)
                'saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_COMMITTEE_PERIOD", "CONTACT_CODE", "DELETE", row("CONTACT_CODE").ToString, "", user_name)

                rowMB_COMMITTEE_WORK_GROUP.Delete()
                rowMB_COMMITTEE_WORK_IN_PERIOD.Delete()
                ds.AcceptChanges()

                'refresh grid
                'getMB_COMMITTEE_PERIOD(TextBox1.Text)
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btFind_Click(sender As Object, e As EventArgs) Handles btFind.Click
        getMB_COMMITTEE_WORK_GROUP(TextBox1.Text)
    End Sub

    Private Sub btDIV2_Click(sender As Object, e As EventArgs) Handles btDIV2.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            Dim f As New frmMainDivs
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                TextBox11.Text = f.DataGridView1.CurrentRow.Cells("DIV_CODE").Value.ToString
                TextBox12.Text = f.DataGridView1.CurrentRow.Cells("DIV_NAME").Value.ToString
                TextBox13.Text = f.DataGridView1.CurrentRow.Cells("ABTN_DIV_NAME").Value.ToString
            End If
            f.Dispose()
            f = Nothing
        End If
    End Sub

    Private Sub btMeetingAdd_Click(sender As Object, e As EventArgs) Handles btMeetingAdd.Click
        If TextBox2.TextLength > 0 Then
            If MessageBox.Show("ยืนยันที่จะ" & btMeetingAdd.Text & "?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Dim parameters As New Dictionary(Of String, Object)

                'get latest seq
                parameters.Add("@p0", TextBox2.Text)
                Dim query As String = "SELECT TOP 1 MEETING_SEQ FROM MB_COMMITTEE_MEETING WHERE WORK_GROUP_CODE = @p0"

                Dim objCNT As Object = Nothing

                Try
                    objCNT = client.ExecuteScalar(query, parameters, user_session)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT MEETING_SEQ")
                End Try

                If objCNT IsNot Nothing Then
                    If IsDBNull(objCNT) = True Then objCNT = 1
                Else
                    objCNT = 1
                End If

                objCNT = CInt(objCNT) + 1

                parameters.Clear()
                parameters.Add("@p0", TextBox5.Text)
                parameters.Add("@p1", TextBox2.Text)
                parameters.Add("@p2", 1)
                parameters.Add("@p3", TextBox7.Text)
                parameters.Add("@p4", objCNT)

                query = "INSERT INTO MB_COMMITTEE_MEETING (DIV_CODE, WORK_GROUP_CODE, PERIOD_SEQ, PERIOD_CODE, MEETING_SEQ) VALUES (@p0,@p1,@p2,@p3,@p4)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history
                'saveLOGS(DataGridView1.CurrentRow.Cells("REGIST_CODE").Value.ToString, "MB_COMP_PERSON_BUS_TYPE", "BUSSINESS_TYPE_CODE", "DEL", DataGridView8.CurrentRow.Cells("BUSSINESS_TYPE_CODE").Value, "", user_name)

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show(btMeetingAdd.Text & "เสร็จสิ้น")
            End If
        End If
        
    End Sub

    Private Sub btMeetingDel_Click(sender As Object, e As EventArgs) Handles btMeetingDel.Click
        If DataGridView2.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะ" & btMeetingDel.Text & " " & DataGridView2.CurrentRow.Cells("MEETING_SEQ").Value.ToString & "?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", TextBox5.Text)
                parameters.Add("@p1", TextBox2.Text)
                parameters.Add("@p2", 1)
                parameters.Add("@p3", TextBox7.Text)
                parameters.Add("@p4", DataGridView2.CurrentRow.Cells("MEETING_SEQ").Value)

                Dim query As String = "DELETE FROM MB_COMMITTEE_MEETING WHERE (DIV_CODE = @p0) AND (WORK_GROUP_CODE = @p1) AND (PERIOD_SEQ = @p2) AND (PERIOD_CODE = @p3) AND (MEETING_SEQ = @p4)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history
                'saveLOGS(DataGridView1.CurrentRow.Cells("REGIST_CODE").Value.ToString, "MB_COMP_PERSON_BUS_TYPE", "BUSSINESS_TYPE_CODE", "DEL", DataGridView8.CurrentRow.Cells("BUSSINESS_TYPE_CODE").Value, "", user_name)

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show(btMeetingDel.Text & "เสร็จสิ้น")
            End If
        End If
        

    End Sub

    Private Sub btMeetingSave_Click(sender As Object, e As EventArgs) Handles btMeetingSave.Click
        If DataGridView2.DataSource IsNot Nothing Then
            DataGridView2.EndEdit()
            If ds.Tables("MB_COMMITTEE_MEETING").GetChanges IsNot Nothing Then
                If MessageBox.Show("ยืนยันที่จะ" & btMeetingSave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                    Dim parameters As New Dictionary(Of String, Object)

                    'save MB_MEMBERS
                    Dim query As String = String.Empty

                    'update it
                    query = "SELECT * FROM MB_COMMITTEE_MEETING WHERE WORK_GROUP_CODE = @p0 ORDER BY MEETING_SEQ DESC"

                    parameters.Add("@p0", TextBox2.Text)

                    If ds.Tables("MB_COMMITTEE_MEETING").GetChanges IsNot Nothing Then
                        Try
                            updateWebSQL(query, parameters, ds.Tables("MB_COMMITTEE_MEETING"))
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=updateWebSQL")
                        End Try
                    End If

                    'refresh grid
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show("บันทึกเสร็จสิ้น")
                End If
            End If
        End If
        
    End Sub

    Private Sub btRepresentAdd_Click(sender As Object, e As EventArgs) Handles btRepresentAdd.Click
        If TextBox2.TextLength > 0 Then
            'add represent
            Dim f As New frmMainContacts
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                If MessageBox.Show("ยืนยันที่จะ" & btRepresentAdd.Text & "?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                    Dim parameters As New Dictionary(Of String, Object)

                    'get latest seq
                    parameters.Add("@p0", TextBox2.Text)
                    Dim query As String = "SELECT TOP 1 REPRESENT_SEQ FROM MB_COMMITTEE_REPRESENT WHERE WORK_GROUP_CODE = @p0"

                    Dim objCNT As Object = Nothing

                    Try
                        objCNT = client.ExecuteScalar(query, parameters, user_session)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT REPRESENT_SEQ")
                    End Try

                    If objCNT IsNot Nothing Then
                        If IsDBNull(objCNT) = True Then objCNT = 1
                    Else
                        objCNT = 1
                    End If

                    objCNT = CInt(objCNT) + 1

                    parameters.Clear()
                    parameters.Add("@p0", TextBox5.Text)
                    parameters.Add("@p1", TextBox2.Text)
                    parameters.Add("@p2", 1)
                    parameters.Add("@p3", TextBox7.Text)
                    parameters.Add("@p4", objCNT)
                    parameters.Add("@p5", f.DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                    query = "INSERT INTO MB_COMMITTEE_REPRESENT (DIV_CODE, WORK_GROUP_CODE, PERIOD_SEQ, PERIOD_CODE, REPRESENT_SEQ, REPRESENT_CODE) VALUES (@p0,@p1,@p2,@p3,@p4,@p5)"

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history
                    'saveLOGS(DataGridView1.CurrentRow.Cells("REGIST_CODE").Value.ToString, "MB_COMP_PERSON_BUS_TYPE", "BUSSINESS_TYPE_CODE", "DEL", DataGridView8.CurrentRow.Cells("BUSSINESS_TYPE_CODE").Value, "", user_name)

                    'refresh grid
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show(btRepresentAdd.Text & "เสร็จสิ้น")
                End If

                MessageBox.Show("เพิ่มผู้แทนเสร็จสิ้น")
            End If
            f.Dispose()
            f = Nothing
        Else
            MessageBox.Show("กรุณาเลือกสมาชิก")
        End If

        
    End Sub

    Private Sub btRepresentDel_Click(sender As Object, e As EventArgs) Handles btRepresentDel.Click
        If DataGridView3.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะ" & btRepresentDel.Text & " " & DataGridView3.CurrentRow.Cells("CONTACT_FIRST_NAME_TH").Value.ToString & " " & DataGridView3.CurrentRow.Cells("CONTACT_LAST_NAME_TH").Value.ToString & "?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", TextBox5.Text)
                parameters.Add("@p1", TextBox2.Text)
                parameters.Add("@p2", 1)
                parameters.Add("@p3", TextBox7.Text)
                parameters.Add("@p4", DataGridView3.CurrentRow.Cells("REPRESENT_SEQ").Value)

                Dim query As String = "DELETE FROM MB_COMMITTEE_REPRESENT WHERE (DIV_CODE = @p0) AND (WORK_GROUP_CODE = @p1) AND (PERIOD_SEQ = @p2) AND (PERIOD_CODE = @p3) AND (REPRESENT_SEQ = @p4)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history
                'saveLOGS(DataGridView1.CurrentRow.Cells("REGIST_CODE").Value.ToString, "MB_COMP_PERSON_BUS_TYPE", "BUSSINESS_TYPE_CODE", "DEL", DataGridView8.CurrentRow.Cells("BUSSINESS_TYPE_CODE").Value, "", user_name)

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show(btRepresentDel.Text & "เสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btRepresentSave_Click(sender As Object, e As EventArgs) Handles btRepresentSave.Click
        If DataGridView3.DataSource IsNot Nothing Then
            DataGridView3.EndEdit()
            If ds.Tables("MB_COMMITTEE_REPRESENT").GetChanges IsNot Nothing Then
                If MessageBox.Show("ยืนยันที่จะ" & btRepresentSave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                    Dim parameters As New Dictionary(Of String, Object)

                    'save MB_MEMBERS
                    Dim query As String = String.Empty

                    'update it
                    query = "SELECT DIV_CODE, WORK_GROUP_CODE, PERIOD_SEQ, PERIOD_CODE, REPRESENT_SEQ, REPRESENT_CODE, POSITION_CODE, POSITION_TYPE, REMARK FROM MB_COMMITTEE_REPRESENT WHERE WORK_GROUP_CODE = @p0 ORDER BY REPRESENT_SEQ"

                    parameters.Add("@p0", TextBox2.Text)

                    If ds.Tables("MB_COMMITTEE_REPRESENT").GetChanges IsNot Nothing Then
                        Try
                            updateWebSQL(query, parameters, ds.Tables("MB_COMMITTEE_REPRESENT"))
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=updateWebSQL")
                        End Try
                    End If

                    'refresh grid
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show("บันทึกเสร็จสิ้น")
                End If
            End If
        End If
    End Sub

    Private Sub btRepresentPosition_Click(sender As Object, e As EventArgs) Handles btRepresentPosition.Click
        If DataGridView3.CurrentRow IsNot Nothing Then
            Dim f As New frmMainPositions
            'f.TextBox1.Text = TextBox18.Text
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Dim row As DataRow = ds.Tables("MB_COMMITTEE_REPRESENT").Rows.Find(New Object() {DataGridView3.CurrentRow.Cells("DIV_CODE").Value, DataGridView3.CurrentRow.Cells("WORK_GROUP_CODE").Value, DataGridView3.CurrentRow.Cells("PERIOD_SEQ").Value, DataGridView3.CurrentRow.Cells("PERIOD_CODE").Value, DataGridView3.CurrentRow.Cells("REPRESENT_SEQ").Value})

                row("POSITION_CODE") = f.DataGridView1.CurrentRow.Cells("POSITION_CODE").Value

                'th en
                'TextBox16.Text = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_TH").Value.ToString
                'TextBox23.Text = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_EN").Value.ToString
                row("POSITION_TYPE") = f.DataGridView1.CurrentRow.Cells("POSITION_TYPE").Value

                row("POSITION_NAME_TH") = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_TH").Value
                row("POSITION_NAME_EN") = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_EN").Value
            End If
            f.Dispose()
            f = Nothing
        End If
    End Sub
End Class