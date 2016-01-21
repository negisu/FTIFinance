Public Class frmFTICommittee

    Dim ds As DataSet
    Dim bsMain As BindingSource
    Dim bsAddr As BindingSource

    Private Sub frmFTICommitteeGroup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        ds = New DataSet

        ComboBox1.SelectedIndex = 0

        getMB_PERIOD()
        getMB_COMMITTEE_FROM()
        getMB_MEMBER_MAIN_GROUP()
        getMB_MEMBER_GROUP()
        getMB_POSITION()
        getMB_PRENAME()

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

    Private Sub getMB_PERIOD()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_PERIOD ORDER BY PERIOD_NAME"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_PERIOD").Copy

        If ds.Tables.Contains("MB_PERIOD") = True Then
            ds.Tables("MB_PERIOD").Clear()
            ds.Tables("MB_PERIOD").Merge(dt)
            ds.Tables("MB_PERIOD").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox2.DataSource = ds.Tables("MB_PERIOD")
        ComboBox2.DisplayMember = "PERIOD_NAME"
        ComboBox2.ValueMember = "PERIOD_CODE"
    End Sub

    Private Sub getMB_COMMITTEE_FROM()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_COMMITTEE_FROM ORDER BY COMMITTEE_FROM_DESC"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_COMMITTEE_FROM").Copy

        If ds.Tables.Contains("MB_COMMITTEE_FROM") = True Then
            ds.Tables("MB_COMMITTEE_FROM").Clear()
            ds.Tables("MB_COMMITTEE_FROM").Merge(dt)
            ds.Tables("MB_COMMITTEE_FROM").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox3.DataSource = ds.Tables("MB_COMMITTEE_FROM")
        ComboBox3.DisplayMember = "COMMITTEE_FROM_DESC"
        ComboBox3.ValueMember = "COMMITTEE_FROM_CODE"
    End Sub

    Private Sub getMB_MEMBER_MAIN_GROUP()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_MEMBER_MAIN_GROUP ORDER BY MEMBER_MAIN_GROUP_NAME"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_MAIN_GROUP").Copy

        If ds.Tables.Contains("MB_MEMBER_MAIN_GROUP") = True Then
            ds.Tables("MB_MEMBER_MAIN_GROUP").Clear()
            ds.Tables("MB_MEMBER_MAIN_GROUP").Merge(dt)
            ds.Tables("MB_MEMBER_MAIN_GROUP").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox4.DataSource = ds.Tables("MB_MEMBER_MAIN_GROUP")
        ComboBox4.DisplayMember = "MEMBER_MAIN_GROUP_NAME"
        ComboBox4.ValueMember = "MEMBER_MAIN_GROUP_CODE"
    End Sub

    Private Sub getMB_MEMBER_GROUP()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_MEMBER_GROUP ORDER BY MEMBER_GROUP_NAME"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_GROUP").Copy

        If ds.Tables.Contains("MB_MEMBER_GROUP") = True Then
            ds.Tables("MB_MEMBER_GROUP").Clear()
            ds.Tables("MB_MEMBER_GROUP").Merge(dt)
            ds.Tables("MB_MEMBER_GROUP").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox5.DataSource = ds.Tables("MB_MEMBER_GROUP")
        ComboBox5.DisplayMember = "MEMBER_GROUP_NAME"
        ComboBox5.ValueMember = "MEMBER_GROUP_CODE"
    End Sub

    Private Sub getMB_POSITION()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_POSITION WHERE POSITION_TYPE = 2 ORDER BY POSITION_NAME_TH, POSITION_NAME_EN"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_POSITION").Copy

        If ds.Tables.Contains("MB_POSITION") = True Then
            ds.Tables("MB_POSITION").Clear()
            ds.Tables("MB_POSITION").Merge(dt)
            ds.Tables("MB_POSITION").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox6.DataSource = ds.Tables("MB_POSITION")
        ComboBox6.DisplayMember = "POSITION_NAME_TH"
        ComboBox6.ValueMember = "POSITION_CODE"
    End Sub

    Private Sub getMB_PRENAME()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_PRENAME ORDER BY PRENAME_TH"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_PRENAME").Copy

        If ds.Tables.Contains("MB_PRENAME") = True Then
            ds.Tables("MB_PRENAME").Clear()
            ds.Tables("MB_PRENAME").Merge(dt)
            ds.Tables("MB_PRENAME").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox10.DataSource = ds.Tables("MB_PRENAME")
        ComboBox10.DisplayMember = "PRENAME_TH"
        ComboBox10.ValueMember = "PRENAME_CODE"
    End Sub

    Private Sub getMB_COMMITTEE_PERIOD(Optional ByVal SEARCH As String = "")
        'Dim searchValue As String = SEARCH.Trim.Replace(" ", "%")
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", "0")
        'parameters.Add("@p1", "1")
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = "SELECT TOP 1000 MB_COMMITTEE_PERIOD.*, MB_CONTACT.*, MB_PRENAME.*, MB_MEMBER.*, MB_COMP_PERSON.* "
        query &= "FROM            MB_COMMITTEE_PERIOD INNER JOIN "
        query &= "                         MB_CONTACT ON MB_COMMITTEE_PERIOD.CONTACT_CODE = MB_CONTACT.CONTACT_CODE INNER JOIN "
        query &= "                         MB_PRENAME ON MB_CONTACT.CONTACT_PRENAME_CODE = MB_PRENAME.PRENAME_CODE INNER JOIN "
        query &= "                         MB_MEMBER ON MB_COMMITTEE_PERIOD.MEMBER_CODE = MB_MEMBER.MEMBER_CODE INNER JOIN "
        query &= "                         MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE "

        Select Case ComboBox1.SelectedIndex
            Case 0 'pre
                Dim searchValue As String = SEARCH.Trim.Replace(" ", "%")
                query &= String.Format("WHERE ((MB_COMMITTEE_PERIOD.MEMBER_CODE LIKE '%{0}%') OR (MB_CONTACT.CONTACT_FIRST_NAME_TH LIKE '%{0}%') OR (MB_CONTACT.CONTACT_LAST_NAME_TH LIKE '%{0}%') OR (MB_COMP_PERSON.COMP_PERSON_NAME_TH LIKE '%{0}%')) ", searchValue)
            Case 1 'period
                query &= String.Format("WHERE (MB_COMMITTEE_PERIOD.PERIOD_CODE = '{0}') ", SEARCH)
        End Select
        query &= "AND (MB_MEMBER.MEMBER_MAIN_GROUP_CODE = '000') AND (MB_MEMBER.MEMBER_GROUP_CODE = '000') "
        query &= "ORDER BY MB_COMMITTEE_PERIOD.PERIOD_CODE DESC, MB_CONTACT.CONTACT_FIRST_NAME_TH, MB_CONTACT.CONTACT_LAST_NAME_TH"

        'MessageBox.Show(query)

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "MB_COMMITTEE_PERIOD")

        If ds.Tables.Contains("MB_COMMITTEE_PERIOD") = True Then
            ds.Tables("MB_COMMITTEE_PERIOD").Clear()
            ds.Tables("MB_COMMITTEE_PERIOD").Merge(dt)
            ds.Tables("MB_COMMITTEE_PERIOD").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            'primary key
            ds.Tables("MB_COMMITTEE_PERIOD").PrimaryKey = New DataColumn() {ds.Tables("MB_COMMITTEE_PERIOD").Columns("PERIOD_SEQ"), ds.Tables("MB_COMMITTEE_PERIOD").Columns("PERIOD_CODE"), ds.Tables("MB_COMMITTEE_PERIOD").Columns("CONTACT_CODE")}

            'blinding
            bsMain = New BindingSource(ds, "MB_COMMITTEE_PERIOD")

            DataGridView1.DataSource = bsMain

            For i As Integer = 0 To DataGridView1.ColumnCount - 1
                DataGridView1.Columns(i).Visible = False
            Next
            '
            DataGridView1.Columns("PERIOD_SEQ").Visible = True
            DataGridView1.Columns("PERIOD_CODE").Visible = True
            DataGridView1.Columns("CONTACT_FIRST_NAME_TH").Visible = True
            DataGridView1.Columns("CONTACT_LAST_NAME_TH").Visible = True

            DataGridView1.Columns("COMMITTEE_FROM_CODE").Visible = True
            DataGridView1.Columns("MEMBER_CODE").Visible = True
            DataGridView1.Columns("ADDR_CODE").Visible = True
            DataGridView1.Columns("COMP_PERSON_NAME_TH").Visible = True
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

            tbCONTACT_CODE.DataBindings.Add("Text", bsMain, "CONTACT_CODE")
            TextBox2.DataBindings.Add("Text", bsMain, "PRENAME_TH")
            TextBox3.DataBindings.Add("Text", bsMain, "CONTACT_FIRST_NAME_TH")
            TextBox4.DataBindings.Add("Text", bsMain, "CONTACT_LAST_NAME_TH")
            TextBox5.DataBindings.Add("Text", bsMain, "PRENAME_EN")
            TextBox6.DataBindings.Add("Text", bsMain, "CONTACT_FIRST_NAME_EN")
            TextBox7.DataBindings.Add("Text", bsMain, "CONTACT_LAST_NAME_EN")
            TextBox8.DataBindings.Add("Text", bsMain, "MEMBER_CODE")
            TextBox9.DataBindings.Add("Text", bsMain, "COMP_PERSON_NAME_TH")

            tbCOMP_PERSON_CODE.DataBindings.Add("Text", bsMain, "COMP_PERSON_CODE")
            tbADDR_CODE.DataBindings.Add("Text", bsMain, "ADDR_CODE")

            TextBox23.DataBindings.Add("Text", bsMain, "ADDR_NO_TH")
            TextBox28.DataBindings.Add("Text", bsMain, "ADDR_MOO_TH")
            TextBox16.DataBindings.Add("Text", bsMain, "ADDR_SOI_TH")
            TextBox14.DataBindings.Add("Text", bsMain, "ADDR_ROAD_TH")
            TextBox50.DataBindings.Add("Text", bsMain, "ADDR_SUB_DISTRICT_TH")
            TextBox51.DataBindings.Add("Text", bsMain, "ADDR_DISTRICT_TH")
            'ComboBox17.DataBindings.Add("Text", bsMain, "ADDR_SUB_DISTRICT_TH")
            'ComboBox19.DataBindings.Add("Text", bsMain, "ADDR_DISTRICT_TH")
            TextBox49.DataBindings.Add("Text", bsMain, "ADDR_PROVINCE_NAME_TH")

            TextBox33.DataBindings.Add("Text", bsMain, "ADDR_NO_EN")
            TextBox55.DataBindings.Add("Text", bsMain, "ADDR_MOO_EN")
            TextBox30.DataBindings.Add("Text", bsMain, "ADDR_SOI_EN")
            TextBox29.DataBindings.Add("Text", bsMain, "ADDR_ROAD_EN")
            TextBox52.DataBindings.Add("Text", bsMain, "ADDR_SUB_DISTRICT_EN")
            TextBox53.DataBindings.Add("Text", bsMain, "ADDR_DISTRICT_EN")
            'ComboBox18.DataBindings.Add("Text", bsMain, "ADDR_SUB_DISTRICT_EN")
            'ComboBox20.DataBindings.Add("Text", bsMain, "ADDR_DISTRICT_EN")
            TextBox54.DataBindings.Add("Text", bsMain, "ADDR_PROVINCE_NAME_EN")

            TextBox15.DataBindings.Add("Text", bsMain, "PERSONAL_ID")
            TextBox17.DataBindings.Add("Text", bsMain, "ADDR_TELEPHONE")
            TextBox19.DataBindings.Add("Text", bsMain, "ADDR_FAX")
            TextBox20.DataBindings.Add("Text", bsMain, "ADDR_EMAIL")

            TextBox18.DataBindings.Add("Text", bsMain, "ADDR_POSTCODE")

            TextBox21.DataBindings.Add("Text", bsMain, "ADDR_MOBILE")

            CheckBox1.DataBindings.Add(New Binding("Checked", bsMain, "FLAG_NON_GROUP", True, DataSourceUpdateMode.OnValidation))

            ComboBox2.DataBindings.Add(New Binding("SelectedValue", bsMain, "PERIOD_CODE", True, DataSourceUpdateMode.OnValidation))
            ComboBox3.DataBindings.Add(New Binding("SelectedValue", bsMain, "COMMITTEE_FROM_CODE", True, DataSourceUpdateMode.OnValidation))
            ComboBox4.DataBindings.Add(New Binding("SelectedValue", bsMain, "MEMBER_MAIN_GROUP_CODE", True, DataSourceUpdateMode.OnValidation))
            ComboBox5.DataBindings.Add(New Binding("SelectedValue", bsMain, "MEMBER_GROUP_CODE", True, DataSourceUpdateMode.OnValidation))
            ComboBox6.DataBindings.Add(New Binding("SelectedValue", bsMain, "GROUP_POSITION_CODE", True, DataSourceUpdateMode.OnValidation))
            ComboBox7.DataBindings.Add(New Binding("SelectedValue", bsMain, "ADDR_CODE", True, DataSourceUpdateMode.OnValidation))

            ComboBox10.DataBindings.Add(New Binding("SelectedValue", bsMain, "CONTACT_PRENAME_CODE", True, DataSourceUpdateMode.OnValidation))
            ComboBox11.DataBindings.Add(New Binding("Text", bsMain, "SEX", True, DataSourceUpdateMode.OnValidation))
            'ComboBox10.DataBindings.Add(New Binding("SelectedValue", bsRep, "CONTACT_PRENAME_CODE", True, DataSourceUpdateMode.OnValidation))
            'ComboBox11.DataBindings.Add(New Binding("SelectedValue", bsRep, "SEX", True, DataSourceUpdateMode.OnValidation))

            'TextBox18.DataBindings.Add("Text", bsRep, "ADDR_POSTCODE")

            AddHandler DataGridView1.SelectionChanged, AddressOf gridControl1_CurrentRowChanged
        End If
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            getMB_COMMITTEE_PERIOD(TextBox1.Text)
        End If
    End Sub

    Private Sub gridControl1_CurrentRowChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ds.Tables("MB_COMMITTEE_PERIOD").RejectChanges()

        If tbCONTACT_CODE.TextLength > 0 Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim parameters As New Dictionary(Of String, Object)
            Dim query As String = String.Empty

            'ADDR_CODE
            If tbCOMP_PERSON_CODE.TextLength > 0 Then
                parameters.Clear()
                parameters.Add("@p0", tbCOMP_PERSON_CODE.Text)
                'parameters.Add("@p1", ComboBox7.SelectedValue)
                query = "SELECT MB_COMP_PERSON_ADDRESS.*, MB_ADDRESS_TYPE.* FROM MB_COMP_PERSON_ADDRESS INNER JOIN MB_ADDRESS_TYPE ON MB_COMP_PERSON_ADDRESS.ADDR_CODE = MB_ADDRESS_TYPE.ADDRESS_TYPE_CODE WHERE MB_COMP_PERSON_ADDRESS.COMP_PERSON_CODE = @p0"

                Dim dtMB_COMP_PERSON_ADDRESS As DataTable = New DataTable

                dtMB_COMP_PERSON_ADDRESS = fillWebSQL(query, parameters, "MB_COMP_PERSON_ADDRESS")

                If ds.Tables.Contains("MB_COMP_PERSON_ADDRESS") = True Then
                    ds.Tables("MB_COMP_PERSON_ADDRESS").Clear()
                    ds.Tables("MB_COMP_PERSON_ADDRESS").Merge(dtMB_COMP_PERSON_ADDRESS)
                    ds.Tables("MB_COMP_PERSON_ADDRESS").AcceptChanges()
                Else
                    ds.Tables.Add(dtMB_COMP_PERSON_ADDRESS)

                    'primary key
                    ds.Tables("MB_COMP_PERSON_ADDRESS").PrimaryKey = New DataColumn() {ds.Tables("MB_COMP_PERSON_ADDRESS").Columns("ADDR_CODE")}

                    bsAddr = New BindingSource(ds, "MB_COMP_PERSON_ADDRESS")

                    ComboBox7.DataSource = bsAddr
                    ComboBox7.DisplayMember = "ADDRESS_TYPE_NAME_TH"
                    ComboBox7.ValueMember = "ADDR_CODE"

                    TextBox37.DataBindings.Add("Text", bsAddr, "ADDR_POSTCODE")

                    'TH
                    TextBox10.DataBindings.Add("Text", bsAddr, "ADDR_NO")
                    TextBox11.DataBindings.Add("Text", bsAddr, "ADDR_MOO")
                    TextBox12.DataBindings.Add("Text", bsAddr, "ADDR_SOI")
                    TextBox13.DataBindings.Add("Text", bsAddr, "ADDR_ROAD")

                    'LOCAL_CODE TH
                    TextBox34.DataBindings.Add("Text", bsAddr, "ADDR_SUB_DISTRICT")
                    TextBox35.DataBindings.Add("Text", bsAddr, "ADDR_DISTRICT")
                    TextBox36.DataBindings.Add("Text", bsAddr, "ADDR_PROVINCE_NAME")

                    'tel/fax/web/email TH
                    TextBox38.DataBindings.Add("Text", bsAddr, "ADDR_TELEPHONE")
                    TextBox42.DataBindings.Add("Text", bsAddr, "ADDR_FAX")
                    TextBox43.DataBindings.Add("Text", bsAddr, "ADDR_WEBSITE")
                    TextBox44.DataBindings.Add("Text", bsAddr, "ADDR_EMAIL")

                    'ComboBox16.DataBindings.Add("Text", bsAddr, "ADDR_LANG")
                    'EN
                    TextBox24.DataBindings.Add("Text", bsAddr, "ADDR_NO_EN")
                    TextBox25.DataBindings.Add("Text", bsAddr, "ADDR_MOO_EN")
                    TextBox26.DataBindings.Add("Text", bsAddr, "ADDR_SOI_EN")
                    TextBox27.DataBindings.Add("Text", bsAddr, "ADDR_ROAD_EN")

                    'LOCAL_CODE EN
                    TextBox41.DataBindings.Add("Text", bsAddr, "ADDR_SUB_DISTRICT_EN")
                    TextBox40.DataBindings.Add("Text", bsAddr, "ADDR_DISTRICT_EN")
                    TextBox39.DataBindings.Add("Text", bsAddr, "ADDR_PROVINCE_NAME_EN")

                    'tel/fax/web/email EN
                    TextBox48.DataBindings.Add("Text", bsAddr, "ADDR_TELEPHONE_EN")
                    TextBox47.DataBindings.Add("Text", bsAddr, "ADDR_FAX_EN")
                    TextBox46.DataBindings.Add("Text", bsAddr, "ADDR_WEBSITE_EN")
                    TextBox45.DataBindings.Add("Text", bsAddr, "ADDR_EMAIL_EN")
                End If

                ComboBox7.SelectedValue = tbADDR_CODE.Text
            Else
                ComboBox7.DataSource = Nothing
            End If

            'get MB_COMMITTEE_POSITION
            parameters.Clear()
            parameters.Add("@p0", ComboBox2.SelectedValue)
            parameters.Add("@p1", tbCONTACT_CODE.Text)

            query = "SELECT        MB_COMMITTEE_POSITION.PERIOD_SEQ, MB_COMMITTEE_POSITION.PERIOD_CODE, MB_COMMITTEE_POSITION.CONTACT_CODE, MB_COMMITTEE_POSITION.WORK_GROUP_CODE, "
            query &= "                         MB_COMMITTEE_WORK_GROUP.WORK_GROUP_NAME, MB_COMMITTEE_WORK_GROUP.WORK_GROUP_SHORT_NAME, MB_COMMITTEE_POSITION.POSITION_CODE, MB_POSITION.POSITION_NAME_TH, "
            query &= "                         MB_COMMITTEE_POSITION.POSITION_TYPE "
            query &= "FROM            MB_COMMITTEE_POSITION INNER JOIN "
            query &= "                         MB_COMMITTEE_WORK_GROUP ON MB_COMMITTEE_POSITION.WORK_GROUP_CODE = MB_COMMITTEE_WORK_GROUP.WORK_GROUP_CODE INNER JOIN "
            query &= "                         MB_POSITION ON MB_COMMITTEE_POSITION.POSITION_CODE = MB_POSITION.POSITION_CODE "
            query &= "WHERE MB_COMMITTEE_POSITION.PERIOD_CODE = @p0 AND MB_COMMITTEE_POSITION.CONTACT_CODE = @p1 AND MB_POSITION.POSITION_TYPE = 2 "

            Dim dtMB_COMMITTEE_POSITION As DataTable = New DataTable

            dtMB_COMMITTEE_POSITION = fillWebSQL(query, parameters, "MB_COMMITTEE_POSITION")

            If ds.Tables.Contains("MB_COMMITTEE_POSITION") = True Then
                ds.Tables("MB_COMMITTEE_POSITION").Clear()
                ds.Tables("MB_COMMITTEE_POSITION").Merge(dtMB_COMMITTEE_POSITION)
                ds.Tables("MB_COMMITTEE_POSITION").AcceptChanges()
            Else
                ds.Tables.Add(dtMB_COMMITTEE_POSITION)

                'primary key
                ds.Tables("MB_COMMITTEE_POSITION").PrimaryKey = New DataColumn() {ds.Tables("MB_COMMITTEE_POSITION").Columns("PERIOD_SEQ"), ds.Tables("MB_COMMITTEE_POSITION").Columns("PERIOD_CODE"), ds.Tables("MB_COMMITTEE_POSITION").Columns("CONTACT_CODE"), ds.Tables("MB_COMMITTEE_POSITION").Columns("WORK_GROUP_CODE"), ds.Tables("MB_COMMITTEE_POSITION").Columns("POSITION_CODE")}

                DataGridView2.DataSource = ds.Tables("MB_COMMITTEE_POSITION")

                DataGridView2.Columns("WORK_GROUP_NAME").Width = 150
                DataGridView2.Columns("POSITION_NAME_TH").Width = 150
            End If

            'get contact photo
            parameters.Clear()
            parameters.Add("@p0", tbCONTACT_CODE.Text)

            query = "SELECT        PICTURE_DATA "
            query &= "FROM            MB_CONTACT_PICTURE "
            query &= "WHERE        (CONTACT_CODE = @p0)"

            Dim PICTURE_DATA As Object = Nothing
            Try
                PICTURE_DATA = client.ExecuteScalar(query, parameters, user_session)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar PICTURE_DATA")
            End Try

            If PICTURE_DATA IsNot Nothing Then
                If PICTURE_DATA IsNot DBNull.Value Then
                    Dim bytBLOBData() As Byte = DirectCast(PICTURE_DATA, Byte())
                    Dim stmBLOBData As New System.IO.MemoryStream(bytBLOBData)
                    PictureBox1.Image = Image.FromStream(stmBLOBData)
                Else
                    PictureBox1.Image = Nothing
                End If
            Else
                PictureBox1.Image = Nothing
            End If

            'get logs
            parameters.Clear()
            parameters.Add("@p0", tbCONTACT_CODE.Text)

            query = "SELECT TOP 50 * FROM MB_LOGS WHERE REGIST_CODE = @p0 ORDER BY MODIFY_DATE DESC"

            Dim dtMB_LOGS As DataTable = New DataTable

            dtMB_LOGS = fillWebSQL(query, parameters, "MB_LOGS")

            If ds.Tables.Contains("MB_LOGS") = True Then
                ds.Tables("MB_LOGS").Clear()
                ds.Tables("MB_LOGS").Merge(dtMB_LOGS)
                ds.Tables("MB_LOGS").AcceptChanges()
            Else
                ds.Tables.Add(dtMB_LOGS)

                'primary key
                ds.Tables("MB_LOGS").PrimaryKey = New DataColumn() {ds.Tables("MB_LOGS").Columns("ID")}

                DataGridView3.DataSource = ds.Tables("MB_LOGS")

                'DataGridView3.Columns("WORK_GROUP_NAME").Width = 150
                'DataGridView3.Columns("POSITION_NAME_TH").Width = 150
            End If
        End If

    End Sub

    Private Sub btProfileSave_Click(sender As Object, e As EventArgs) Handles btProfileSave.Click
        bsMain.EndEdit()

        If MessageBox.Show("ยืนยันที่จะ" & btProfileSave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim row As DataRow = ds.Tables("MB_COMMITTEE_PERIOD").Rows.Find(New Object() {DataGridView1.CurrentRow.Cells("PERIOD_SEQ").Value, DataGridView1.CurrentRow.Cells("PERIOD_CODE").Value, DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value})
            'ds.Tables("MB_COMMITTEE_PERIOD").PrimaryKey = New DataColumn() {ds.Tables("MB_COMMITTEE_PERIOD").Columns("PERIOD_SEQ"), ds.Tables("MB_COMMITTEE_PERIOD").Columns("PERIOD_CODE"), ds.Tables("MB_COMMITTEE_PERIOD").Columns("CONTACT_CODE")}

            'update it
            'query = String.Empty

            Dim cols() As String = "PERIOD_SEQ,PERIOD_CODE,CONTACT_CODE,COMMITTEE_FROM_CODE,MEMBER_MAIN_GROUP_CODE,MEMBER_GROUP_CODE,GROUP_POSITION_CODE,GROUP_POSITION_TYPE,MEMBER_CODE,COMP_PERSON_CODE,ADDR_CODE".Split(","c)
            For i As Integer = 0 To cols.Length - 1
                If row.HasVersion(DataRowVersion.Current) = True Then
                    If row(cols(i), DataRowVersion.Original).ToString <> row(cols(i), DataRowVersion.Current).ToString Then
                        'save it
                        'DataGridView1.CurrentRow.Cells("PERIOD_CODE").Value.ToString & "=" &
                        saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_COMMITTEE_PERIOD", cols(i), "UPDATE", If(IsDBNull(row(cols(i), DataRowVersion.Original)), "", row(cols(i), DataRowVersion.Original)), If(IsDBNull(row(cols(i), DataRowVersion.Current)), "", row(cols(i), DataRowVersion.Current)), user_name)
                    End If
                End If
            Next

            'update it
            Dim parameters As New Dictionary(Of String, Object)
            Dim query As String = String.Empty
            'save MB_MEMBERS

            'query = String.Empty

            query &= "SELECT * FROM MB_COMMITTEE_PERIOD "

            If ds.Tables("MB_COMMITTEE_PERIOD").GetChanges IsNot Nothing Then
                Try
                    updateWebSQL(query, parameters, ds.Tables("MB_COMMITTEE_PERIOD"))
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=updateWebSQL")
                End Try
            End If

            ds.Tables("MB_COMMITTEE_PERIOD").AcceptChanges()

            'refresh grid
            'getMB_COMMITTEE_PERIOD(TextBox1.Text)
            gridControl1_CurrentRowChanged(sender, e)

            MessageBox.Show("บันทึกเสร็จสิ้น")
        End If
    End Sub

    Private Sub btContacts_Click(sender As Object, e As EventArgs)
        If DataGridView1.CurrentRow IsNot Nothing Then
            Dim f As New frmMainContacts
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                If MessageBox.Show("ยืนยันที่จะ" & " " & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim parameters As New Dictionary(Of String, Object)
                    Dim query As String = String.Empty

                    parameters.Clear()
                    parameters.Add("@p0", f.DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)
                    parameters.Add("@p1", DataGridView1.CurrentRow.Cells("PERIOD_SEQ").Value)
                    parameters.Add("@p2", DataGridView1.CurrentRow.Cells("PERIOD_CODE").Value)
                    parameters.Add("@p3", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                    query = "UPDATE MB_COMMITTEE_PERIOD SET CONTACT_CODE = @p0 WHERE PERIOD_SEQ = @p1 AND PERIOD_CODE = @p2 AND CONTACT_CODE = @p3"

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history
                    saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_COMMITTEE_PERIOD", "CONTACT_CODE", "UPDATE", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, f.DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, user_name)

                    'refresh grid
                    getMB_COMMITTEE_PERIOD(TextBox1.Text)
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show("บันทึกเสร็จสิ้น")
                End If

            End If
            f.Dispose()
            f = Nothing
        End If
    End Sub

    Private Sub btCompany_Click(sender As Object, e As EventArgs) Handles btCompany.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            Dim f As New frmMainCompanies
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                If MessageBox.Show("ยืนยันที่จะ" & btCompany.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim parameters As New Dictionary(Of String, Object)
                    Dim query As String = String.Empty

                    parameters.Clear()
                    parameters.Add("@p0", f.DataGridView1.CurrentRow.Cells("MEMBER_CODE").Value)
                    parameters.Add("@p1", DataGridView1.CurrentRow.Cells("PERIOD_SEQ").Value)
                    parameters.Add("@p2", DataGridView1.CurrentRow.Cells("PERIOD_CODE").Value)
                    parameters.Add("@p3", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                    query = "UPDATE MB_COMMITTEE_PERIOD SET MEMBER_CODE = @p0 WHERE PERIOD_SEQ = @p1 AND PERIOD_CODE = @p2 AND CONTACT_CODE = @p3"

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history
                    saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_COMMITTEE_PERIOD", "MEMBER_CODE", "UPDATE", DataGridView1.CurrentRow.Cells("MEMBER_CODE").Value.ToString, f.DataGridView1.CurrentRow.Cells("MEMBER_CODE").Value.ToString, user_name)

                    'refresh grid
                    getMB_COMMITTEE_PERIOD(TextBox1.Text)
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show("บันทึกเสร็จสิ้น")
                End If

            End If
            f.Dispose()
            f = Nothing
        End If
    End Sub

    Private Sub btEDUadd_Click(sender As Object, e As EventArgs) Handles btEDUadd.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            bsMain.EndEdit()

            Dim f As New frmFTICommitteePositionNew
            f.PERIOD_SEQ = 1
            f.PERIOD_CODE = ComboBox2.SelectedValue.ToString
            f.CONTACT_CODE = tbCONTACT_CODE.Text
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                If MessageBox.Show("ยืนยันที่จะ" & btEDUadd.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    'Dim row As DataRow = ds.Tables("MB_COMMITTEE_PERIOD").Rows.Find(New Object() {DataGridView2.CurrentRow.Cells("PERIOD_SEQ").Value, DataGridView2.CurrentRow.Cells("PERIOD_CODE").Value, DataGridView2.CurrentRow.Cells("CONTACT_CODE").Value})

                    Dim parameters As New Dictionary(Of String, Object)
                    Dim query As String = String.Empty

                    query = "INSERT INTO MB_COMMITTEE_POSITION (PERIOD_SEQ, PERIOD_CODE, CONTACT_CODE, WORK_GROUP_CODE, POSITION_CODE, POSITION_TYPE) VALUES (@p0,@p1,@p2,@p3,@p4,@p5)"
                    parameters.Add("@p0", 1)
                    parameters.Add("@p1", ComboBox2.SelectedValue)
                    parameters.Add("@p2", tbCONTACT_CODE.Text)
                    parameters.Add("@p3", f.TextBox9.Text)
                    parameters.Add("@p4", f.TextBox30.Text)
                    parameters.Add("@p5", 2)

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history 
                    saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_COMMITTEE_POSITION", "POSITION_CODE", "ADD", 0, f.TextBox30.Text, user_name)

                    'refresh grid
                    gridControl1_CurrentRowChanged(sender, e)
                End If
            End If
            f.Dispose()
            f = Nothing
        End If
        
    End Sub

    Private Sub btEDUdel_Click(sender As Object, e As EventArgs) Handles btEDUdel.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            bsMain.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btEDUdel.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_COMMITTEE_POSITION").Rows.Find(New Object() {DataGridView1.CurrentRow.Cells("PERIOD_SEQ").Value, DataGridView2.CurrentRow.Cells("PERIOD_CODE").Value, DataGridView2.CurrentRow.Cells("CONTACT_CODE").Value, DataGridView2.CurrentRow.Cells("WORK_GROUP_CODE").Value, DataGridView2.CurrentRow.Cells("POSITION_CODE").Value})

                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", row("PERIOD_SEQ"))
                parameters.Add("@p1", row("PERIOD_CODE"))
                parameters.Add("@p2", row("CONTACT_CODE"))
                parameters.Add("@p3", row("WORK_GROUP_CODE"))
                parameters.Add("@p4", row("POSITION_CODE"))
                'parameters.Add("@p5", row("POSITION_TYPE"))

                Dim query As String = "DELETE FROM MB_COMMITTEE_POSITION WHERE (PERIOD_SEQ = @p0) AND (PERIOD_CODE = @p1) AND (CONTACT_CODE = @p2) AND (WORK_GROUP_CODE = @p3) AND (POSITION_CODE = @p4)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history 
                saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_COMMITTEE_POSITION", "POSITION_CODE", "DELETE", DataGridView2.CurrentRow.Cells("POSITION_CODE").Value, 0, user_name)

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btEDUsave_Click(sender As Object, e As EventArgs) Handles btEDUsave.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView2.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btEDUsave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

                Dim row As DataRow = ds.Tables("MB_COMMITTEE_POSITION").Rows.Find(New Object() {DataGridView1.CurrentRow.Cells("PERIOD_SEQ").Value, DataGridView2.CurrentRow.Cells("PERIOD_CODE").Value, DataGridView2.CurrentRow.Cells("CONTACT_CODE").Value, DataGridView2.CurrentRow.Cells("WORK_GROUP_CODE").Value, DataGridView2.CurrentRow.Cells("POSITION_CODE").Value})

                Dim cols() As String = "PERIOD_SEQ,PERIOD_CODE,CONTACT_CODE,WORK_GROUP_CODE,POSITION_CODE,POSITION_TYPE".Split(","c)
                For i As Integer = 0 To cols.Length - 1
                    If row.HasVersion(DataRowVersion.Current) = True Then
                        If row(cols(i), DataRowVersion.Original).ToString <> row(cols(i), DataRowVersion.Current).ToString Then
                            'save it
                            saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_COMMITTEE_POSITION", cols(i), "UPDATE", If(IsDBNull(row(cols(i), DataRowVersion.Original)), "", row(cols(i), DataRowVersion.Original)), If(IsDBNull(row(cols(i), DataRowVersion.Current)), "", row(cols(i), DataRowVersion.Current)), user_name)
                        End If
                    End If
                Next

                'update it
                Dim parameters As New Dictionary(Of String, Object)
                'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                Dim query As String = "SELECT * FROM MB_COMMITTEE_POSITION "

                If ds.Tables("MB_COMMITTEE_POSITION").GetChanges IsNot Nothing Then
                    Try
                        updateWebSQL(query, parameters, ds.Tables("MB_COMMITTEE_POSITION"))
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=updateWebSQL")
                    End Try
                End If

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("บันทึกเสร็จสิ้น")
            End If
        End If
        
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        DataGridView1.EndEdit()
        'bsMain.EndEdit()

        Dim f As New frmFTICommitteeNew
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            If MessageBox.Show("ยืนยันที่จะ" & btNew.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'Dim row As DataRow = ds.Tables("MB_COMMITTEE_PERIOD").Rows.Find(New Object() {DataGridView2.CurrentRow.Cells("PERIOD_SEQ").Value, DataGridView2.CurrentRow.Cells("PERIOD_CODE").Value, DataGridView2.CurrentRow.Cells("CONTACT_CODE").Value})

                Dim parameters As New Dictionary(Of String, Object)
                Dim query As String = String.Empty

                query = "INSERT INTO MB_COMMITTEE_PERIOD (PERIOD_SEQ, PERIOD_CODE, CONTACT_CODE) VALUES (@p0,@p1,@p2)"
                parameters.Add("@p0", 1)
                parameters.Add("@p1", f.ComboBox2.SelectedValue)
                parameters.Add("@p2", f.TextBox30.Text)
                'parameters.Add("@p3", f.TextBox9.Text)
                'parameters.Add("@p4", f.TextBox30.Text)
                'parameters.Add("@p5", 2)

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history 
                saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_COMMITTEE_PERIOD", "PERIOD_CODE", "ADD", "", f.ComboBox2.SelectedValue.ToString, user_name)
                saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_COMMITTEE_PERIOD", "CONTACT_CODE", "ADD", "", f.TextBox30.Text, user_name)

                'refresh grid
                getMB_COMMITTEE_PERIOD(TextBox1.Text)
                gridControl1_CurrentRowChanged(sender, e)
            End If
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            bsMain.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btDelete.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_COMMITTEE_PERIOD").Rows.Find(New Object() {DataGridView1.CurrentRow.Cells("PERIOD_SEQ").Value, DataGridView2.CurrentRow.Cells("PERIOD_CODE").Value, DataGridView2.CurrentRow.Cells("CONTACT_CODE").Value})

                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", row("PERIOD_SEQ"))
                parameters.Add("@p1", row("PERIOD_CODE"))
                parameters.Add("@p2", row("CONTACT_CODE"))
                'parameters.Add("@p3", row("WORK_GROUP_CODE"))
                'parameters.Add("@p4", row("POSITION_CODE"))
                'parameters.Add("@p5", row("POSITION_TYPE"))

                Dim query As String = "DELETE FROM MB_COMMITTEE_PERIOD WHERE (PERIOD_SEQ = @p0) AND (PERIOD_CODE = @p1) AND (CONTACT_CODE = @p2)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history 
                saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_COMMITTEE_PERIOD", "PERIOD_CODE", "DELETE", row("PERIOD_CODE").ToString, "", user_name)
                saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_COMMITTEE_PERIOD", "CONTACT_CODE", "DELETE", row("CONTACT_CODE").ToString, "", user_name)

                'refresh grid
                getMB_COMMITTEE_PERIOD(TextBox1.Text)
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
    End Sub
End Class