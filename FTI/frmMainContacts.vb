Imports System.Windows.Forms

Public Class frmMainContacts

    Dim ds As DataSet
    Dim bsRep As BindingSource
    Dim WithEvents BindingBIRTH_DATE, BindingAPPROVE_DATE, BindingPUT_IN_DATE, BindingMOD_DATE As Binding

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIRepresents_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet

        ComboBox1.SelectedIndex = 0

        Try
            Dim dtSEX As New DataTable("SEX")
            dtSEX.Columns.Add("NAME", GetType(String))
            ds.Tables.Add(dtSEX)
            ds.Tables("SEX").PrimaryKey = New DataColumn() {ds.Tables("SEX").Columns("NAME")}

            Dim M As DataRow = ds.Tables("SEX").NewRow
            M.Item("NAME") = "M"
            Dim F As DataRow = ds.Tables("SEX").NewRow
            F.Item("NAME") = "F"

            ds.Tables("SEX").Rows.Add(M)
            ds.Tables("SEX").Rows.Add(F)

            Dim dtSEX2 As DataTable = ds.Tables("SEX").Copy
            dtSEX2.TableName = "SEX2"

            ds.Tables.Add(dtSEX2)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ComboBox11.DataSource = ds.Tables("SEX")
        ComboBox11.ValueMember = "NAME"
        ComboBox11.DisplayMember = "NAME"

        ComboBox3.DataSource = ds.Tables("SEX2")
        ComboBox3.ValueMember = "NAME"
        ComboBox3.DisplayMember = "NAME"

        getMB_PRENAME()
        getMB_EDUCATION()
        getMB_LANGUAGE_SKILL()
        getMB_SKILL()

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getCONTACT()
        'represent

        Dim searchValue As String = TextBox1.Text.Trim.Replace(" ", "%")
        'searchValue = "%" & searchValue & "%"

        Dim pRep As New Dictionary(Of String, Object)
        'pRep.Add("@p0", row.Item("REGIST_CODE"))

        Dim qRep As String = String.Empty
        qRep &= "SELECT TOP 100 * "
        qRep &= "FROM MB_CONTACT "
        'qRep &= "WHERE (MB_MEMBER_REPRESENT.REGIST_CODE= @p0) "

        qRep &= String.Format(" WHERE CONTACT_FIRST_NAME_TH LIKE '%{0}%' OR CONTACT_LAST_NAME_TH LIKE '%{0}%' OR CONTACT_FIRST_NAME_EN LIKE '%{0}%' OR CONTACT_LAST_NAME_EN LIKE '%{0}%' OR CONTACT_CODE = '{1}'", searchValue, TextBox1.Text)

        qRep &= " ORDER BY CONTACT_FIRST_NAME_TH, CONTACT_LAST_NAME_TH "

        Dim dtRep As DataTable = New DataTable

        dtRep = fillWebSQL(qRep, pRep, "MB_CONTACT")

        If ds.Tables.Contains("MB_CONTACT") = True Then
            ds.Tables("MB_CONTACT").Clear()
            ds.Tables("MB_CONTACT").Merge(dtRep)
            ds.Tables("MB_CONTACT").AcceptChanges()
        Else
            ds.Tables.Add(dtRep)

            'primary key
            ds.Tables("MB_CONTACT").PrimaryKey = New DataColumn() {ds.Tables("MB_CONTACT").Columns("CONTACT_CODE")}

            'blinding
            bsRep = New BindingSource(ds, "MB_CONTACT")

            DataGridView1.DataSource = bsRep

            For i As Integer = 0 To DataGridView1.ColumnCount - 1
                DataGridView1.Columns(i).Visible = False
            Next
            '
            DataGridView1.Columns("CONTACT_CODE").Visible = True
            DataGridView1.Columns("CONTACT_FIRST_NAME_TH").Visible = True
            DataGridView1.Columns("CONTACT_LAST_NAME_TH").Visible = True

            TextBox9.DataBindings.Add("Text", bsRep, "CONTACT_FIRST_NAME_TH")
            TextBox10.DataBindings.Add("Text", bsRep, "CONTACT_LAST_NAME_TH")

            TextBox11.DataBindings.Add("Text", bsRep, "ADDR_NO_TH")
            TextBox12.DataBindings.Add("Text", bsRep, "ADDR_MOO_TH")
            TextBox13.DataBindings.Add("Text", bsRep, "ADDR_SOI_TH")
            TextBox14.DataBindings.Add("Text", bsRep, "ADDR_ROAD_TH")
            TextBox50.DataBindings.Add("Text", bsRep, "ADDR_SUB_DISTRICT_TH")
            TextBox51.DataBindings.Add("Text", bsRep, "ADDR_DISTRICT_TH")
            TextBox49.DataBindings.Add("Text", bsRep, "ADDR_PROVINCE_NAME_TH")

            TextBox30.DataBindings.Add("Text", bsRep, "CONTACT_FIRST_NAME_EN")
            TextBox31.DataBindings.Add("Text", bsRep, "CONTACT_LAST_NAME_EN")

            TextBox32.DataBindings.Add("Text", bsRep, "ADDR_NO_EN")
            TextBox33.DataBindings.Add("Text", bsRep, "ADDR_MOO_EN")
            TextBox29.DataBindings.Add("Text", bsRep, "ADDR_SOI_EN")
            TextBox28.DataBindings.Add("Text", bsRep, "ADDR_ROAD_EN")
            TextBox52.DataBindings.Add("Text", bsRep, "ADDR_SUB_DISTRICT_EN")
            TextBox53.DataBindings.Add("Text", bsRep, "ADDR_DISTRICT_EN")
            TextBox54.DataBindings.Add("Text", bsRep, "ADDR_PROVINCE_NAME_EN")

            TextBox15.DataBindings.Add("Text", bsRep, "PERSONAL_ID")
            TextBox17.DataBindings.Add("Text", bsRep, "ADDR_TELEPHONE")
            TextBox19.DataBindings.Add("Text", bsRep, "ADDR_FAX")
            TextBox20.DataBindings.Add("Text", bsRep, "ADDR_EMAIL")

            TextBox42.DataBindings.Add("Text", bsRep, "ADDR_MOBILE")
            TextBox43.DataBindings.Add("Text", bsRep, "PERSONAL_HOBBY")
            TextBox44.DataBindings.Add("Text", bsRep, "PERSONAL_MANAGE")
            TextBox45.DataBindings.Add("Text", bsRep, "PERSONAL_PHIL")
            TextBox46.DataBindings.Add("Text", bsRep, "PERSONAL_ATTI")

            cbPRENAME_TH.DataBindings.Add(New Binding("SelectedValue", bsRep, "CONTACT_PRENAME_CODE", True, DataSourceUpdateMode.OnValidation))
            cbPRENAME_EN.DataBindings.Add(New Binding("SelectedValue", bsRep, "CONTACT_PRENAME_CODE", True, DataSourceUpdateMode.OnValidation))
            ComboBox11.DataBindings.Add(New Binding("SelectedValue", bsRep, "SEX", True, DataSourceUpdateMode.OnValidation))
            'ComboBox10.DataBindings.Add("SelectedValue", bsRep, "CONTACT_PRENAME_CODE")
            'ComboBox11.DataBindings.Add("SelectedValue", bsRep, "SEX")
            'ComboBox12.DataBindings.Add("Text", bsRep, "ADDR_PROVINCE_CODE")
            'ComboBox12.DataBindings.Add("SelectedValue", bsRep, "ADDR_PROVINCE_CODE")

            TextBox18.DataBindings.Add("Text", bsRep, "ADDR_POSTCODE")

            tbCONTACT_CODE.DataBindings.Add("Text", bsRep, "CONTACT_CODE")

            BindingBIRTH_DATE = New Binding("Text", bsRep, "BIRTH_DATE")
            MaskedTextBox1.DataBindings.Add(BindingBIRTH_DATE)

            tbSPOUSE_CODE.DataBindings.Add("Text", bsRep, "SPOUSE_CODE")

            RadioButton1.DataBindings.Add("Tag", bsRep, "ADDR_MAIL")

            'work address
            TextBox24.DataBindings.Add("Text", bsRep, "ADDR_NO_TH1")
            TextBox26.DataBindings.Add("Text", bsRep, "ADDR_MOO_TH1")
            TextBox27.DataBindings.Add("Text", bsRep, "ADDR_SOI_TH1")
            TextBox35.DataBindings.Add("Text", bsRep, "ADDR_ROAD_TH1")
            TextBox34.DataBindings.Add("Text", bsRep, "ADDR_SUB_DISTRICT_TH1")
            TextBox25.DataBindings.Add("Text", bsRep, "ADDR_DISTRICT_TH1")
            TextBox21.DataBindings.Add("Text", bsRep, "ADDR_PROVINCE_NAME_TH1")

            TextBox36.DataBindings.Add("Text", bsRep, "ADDR_NO_EN1")
            TextBox40.DataBindings.Add("Text", bsRep, "ADDR_MOO_EN1")
            TextBox39.DataBindings.Add("Text", bsRep, "ADDR_SOI_EN1")
            TextBox37.DataBindings.Add("Text", bsRep, "ADDR_ROAD_EN1")
            TextBox41.DataBindings.Add("Text", bsRep, "ADDR_SUB_DISTRICT_EN1")
            TextBox38.DataBindings.Add("Text", bsRep, "ADDR_DISTRICT_EN1")
            TextBox23.DataBindings.Add("Text", bsRep, "ADDR_PROVINCE_NAME_EN1")

            TextBox16.DataBindings.Add("Text", bsRep, "ADDR_TELEPHONE1")
            TextBox7.DataBindings.Add("Text", bsRep, "ADDR_FAX1")
            TextBox8.DataBindings.Add("Text", bsRep, "ADDR_EMAIL1")

            TextBox22.DataBindings.Add("Text", bsRep, "ADDR_POSTCODE1")

            AddHandler DataGridView1.SelectionChanged, AddressOf gridControl1_CurrentRowChanged
        End If
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            getCONTACT()
        End If
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

        Dim dt2 As DataTable = dt.Copy
        dt2.TableName = "MB_PRENAME2"

        If ds.Tables.Contains("MB_PRENAME2") = True Then
            ds.Tables("MB_PRENAME2").Clear()
            ds.Tables("MB_PRENAME2").Merge(dt2)
            ds.Tables("MB_PRENAME2").AcceptChanges()
        Else
            ds.Tables.Add(dt2)
        End If

        cbPRENAME_TH.DataSource = ds.Tables("MB_PRENAME")
        cbPRENAME_TH.DisplayMember = "PRENAME_TH"
        cbPRENAME_TH.ValueMember = "PRENAME_CODE"

        cbPRENAME_EN.DataSource = ds.Tables("MB_PRENAME")
        cbPRENAME_EN.DisplayMember = "PRENAME_EN"
        cbPRENAME_EN.ValueMember = "PRENAME_CODE"

        ComboBox4.DataSource = ds.Tables("MB_PRENAME2")
        ComboBox4.DisplayMember = "PRENAME_TH"
        ComboBox4.ValueMember = "PRENAME_CODE"

        ComboBox2.DataSource = ds.Tables("MB_PRENAME2")
        ComboBox2.DisplayMember = "PRENAME_EN"
        ComboBox2.ValueMember = "PRENAME_CODE"
    End Sub

    Private Sub getMB_EDUCATION()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_EDUCATION ORDER BY EDUCATION_NAME_TH"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_EDUCATION").Copy

        If ds.Tables.Contains("MB_EDUCATION") = True Then
            ds.Tables("MB_EDUCATION").Clear()
            ds.Tables("MB_EDUCATION").Merge(dt)
            ds.Tables("MB_EDUCATION").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If
    End Sub

    Private Sub getMB_SKILL()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_SKILL ORDER BY SKILL_DESC"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_SKILL").Copy

        If ds.Tables.Contains("MB_SKILL") = True Then
            ds.Tables("MB_SKILL").Clear()
            ds.Tables("MB_SKILL").Merge(dt)
            ds.Tables("MB_SKILL").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If
    End Sub

    Private Sub getMB_LANGUAGE_SKILL()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_LANGUAGE_SKILL ORDER BY LANGUAGE_SKILL_NAME_TH"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_LANGUAGE_SKILL").Copy

        If ds.Tables.Contains("MB_LANGUAGE_SKILL") = True Then
            ds.Tables("MB_LANGUAGE_SKILL").Clear()
            ds.Tables("MB_LANGUAGE_SKILL").Merge(dt)
            ds.Tables("MB_LANGUAGE_SKILL").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If
    End Sub

    Private Sub gridControl1_CurrentRowChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim xrow As Xceed.Grid.DataRow = CType(RadGridView1.CurrentRow, Xceed.Grid.DataRow)

        If tbCONTACT_CODE.TextLength > 0 Then
            DataGridView1.CancelEdit()

            'Dim row As DataRow = ds.Tables("MB_CONTACT").Rows.Find(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)
            OpenImageFileDlg.FileName = String.Empty

            'get contact photo
            Dim parameters As New Dictionary(Of String, Object)
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)
            parameters.Add("@p0", tbCONTACT_CODE.Text)

            Dim query As String = String.Empty
            query &= "SELECT        PICTURE_DATA "
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

            '=========================================================

            'edu
            'parameters.Clear()
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            query = "SELECT * FROM MB_CONTACT_EDUCATION WHERE CONTACT_CODE = @p0 ORDER BY EDUCATION_SEQ "

            Dim dtMB_CONTACT_EDUCATION As DataTable = New DataTable

            dtMB_CONTACT_EDUCATION = fillWebSQL(query, parameters, "MB_CONTACT_EDUCATION")

            If ds.Tables.Contains("MB_CONTACT_EDUCATION") = True Then
                ds.Tables("MB_CONTACT_EDUCATION").Clear()
                ds.Tables("MB_CONTACT_EDUCATION").Merge(dtMB_CONTACT_EDUCATION)
                ds.Tables("MB_CONTACT_EDUCATION").AcceptChanges()
            Else
                ds.Tables.Add(dtMB_CONTACT_EDUCATION)

                'primary key
                ds.Tables("MB_CONTACT_EDUCATION").PrimaryKey = New DataColumn() {ds.Tables("MB_CONTACT_EDUCATION").Columns("EDUCATION_SEQ")}

                DataGridView2.DataSource = ds.Tables("MB_CONTACT_EDUCATION")

                For i As Integer = 0 To DataGridView2.ColumnCount - 1
                    DataGridView2.Columns(i).Visible = False
                Next
                '
                DataGridView2.Columns("EDUCATION_SEQ").Visible = True
                DataGridView2.Columns("DEPARTMENT_NAME").Visible = True
                DataGridView2.Columns("INSTITUTE_NAME").Visible = True
                DataGridView2.Columns("YEAR_EDUCATED").Visible = True

                'combobox
                Dim comboBoxColumn As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
                comboBoxColumn.HeaderText = "EDUCATION_NAME_TH"
                comboBoxColumn.DataPropertyName = "EDUCATION_CODE"
                comboBoxColumn.DataSource = ds.Tables("MB_EDUCATION")
                comboBoxColumn.ValueMember = "EDUCATION_CODE"
                comboBoxColumn.DisplayMember = "EDUCATION_NAME_TH"

                'DataGridView2.Columns.RemoveAt(4)
                DataGridView2.Columns.Insert(4, comboBoxColumn)

                DataGridView2.Columns("DEPARTMENT_NAME").Width = 150
                DataGridView2.Columns("INSTITUTE_NAME").Width = 150

                comboBoxColumn.Width = 150
            End If

            DataGridView2.Columns("OU_CODE").Visible = False

            '=========================================================

            'traning
            'parameters.Clear()
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            query = "SELECT * FROM MB_CONTACT_TRAINNING WHERE CONTACT_CODE = @p0 ORDER BY TRAIN_SEQ "

            Dim dtMB_CONTACT_TRAINNING As DataTable = New DataTable

            dtMB_CONTACT_TRAINNING = fillWebSQL(query, parameters, "MB_CONTACT_TRAINNING")

            If ds.Tables.Contains("MB_CONTACT_TRAINNING") = True Then
                ds.Tables("MB_CONTACT_TRAINNING").Clear()
                ds.Tables("MB_CONTACT_TRAINNING").Merge(dtMB_CONTACT_TRAINNING)
                ds.Tables("MB_CONTACT_TRAINNING").AcceptChanges()
            Else
                ds.Tables.Add(dtMB_CONTACT_TRAINNING)

                'primary key
                ds.Tables("MB_CONTACT_TRAINNING").PrimaryKey = New DataColumn() {ds.Tables("MB_CONTACT_TRAINNING").Columns("TRAIN_SEQ")}

                DataGridView3.DataSource = ds.Tables("MB_CONTACT_TRAINNING")

                For Each c As DataGridViewColumn In DataGridView3.Columns
                    c.Visible = False
                Next
                '
                DataGridView3.Columns("TRAIN_SEQ").Visible = True
                'DataGridView3.Columns("START_DATE").Visible = True
                'DataGridView3.Columns("END_DATE").Visible = True
                DataGridView3.Columns("TRAIN_QTY").Visible = True
                DataGridView3.Columns("TRAIN_UNIT").Visible = True
                DataGridView3.Columns("TRAIN_DETAIL").Visible = True
                DataGridView3.Columns("TRAIN_BY").Visible = True

                ''combobox
                'Dim comboBoxColumn As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
                'comboBoxColumn.HeaderText = "EDUCATION_CODE"
                'comboBoxColumn.DataPropertyName = "EDUCATION_CODE"
                'comboBoxColumn.DataSource = ds.Tables("MB_EDUCATION")
                'comboBoxColumn.ValueMember = "EDUCATION_CODE"
                'comboBoxColumn.DisplayMember = "EDUCATION_NAME_TH"

                'DataGridView2.Columns.RemoveAt(4)
                'DataGridView2.Columns.Insert(4, comboBoxColumn)

                DataGridView3.Columns("TRAIN_DETAIL").Width = 150
                DataGridView3.Columns("TRAIN_SEQ").Width = 50
                DataGridView3.Columns("TRAIN_QTY").Width = 50

                'comboBoxColumn.Width = 150

                Dim colSTART_DATE As New CalendarColumn()
                colSTART_DATE.HeaderText = "START_DATE"
                colSTART_DATE.DataPropertyName = "START_DATE"
                'DataGridView3.Columns.RemoveAt(4)
                DataGridView3.Columns.Insert(4, colSTART_DATE)

                Dim colEND_DATE As New CalendarColumn()
                colEND_DATE.HeaderText = "END_DATE"
                colEND_DATE.DataPropertyName = "END_DATE"
                'DataGridView3.Columns.RemoveAt(5)
                DataGridView3.Columns.Insert(5, colEND_DATE)
            End If

            DataGridView3.Columns("OU_CODE").Visible = False

            '=========================================================

            'lang
            'parameters.Clear()
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            query = "SELECT CONTACT_CODE, LANGUAGE_SEQ, LANGUAGE_SKILL_CODE FROM MB_CONTACT_LANGUAGE WHERE CONTACT_CODE = @p0 "

            Dim dtMB_CONTACT_LANGUAGE As DataTable = New DataTable

            dtMB_CONTACT_LANGUAGE = fillWebSQL(query, parameters, "MB_CONTACT_LANGUAGE")

            If ds.Tables.Contains("MB_CONTACT_LANGUAGE") = True Then
                ds.Tables("MB_CONTACT_LANGUAGE").Clear()
                ds.Tables("MB_CONTACT_LANGUAGE").Merge(dtMB_CONTACT_LANGUAGE)
                ds.Tables("MB_CONTACT_LANGUAGE").AcceptChanges()
            Else
                ds.Tables.Add(dtMB_CONTACT_LANGUAGE)

                'primary key
                ds.Tables("MB_CONTACT_LANGUAGE").PrimaryKey = New DataColumn() {ds.Tables("MB_CONTACT_LANGUAGE").Columns("LANGUAGE_SKILL_CODE")}

                DataGridView4.DataSource = ds.Tables("MB_CONTACT_LANGUAGE")

                For i As Integer = 0 To DataGridView4.ColumnCount - 1
                    DataGridView4.Columns(i).Visible = False
                Next
                '
                DataGridView4.Columns("LANGUAGE_SEQ").Visible = True
                'DataGridView2.Columns("DEPARTMENT_NAME").Visible = True
                'DataGridView2.Columns("INSTITUTE_NAME").Visible = True
                'DataGridView2.Columns("YEAR_EDUCATED").Visible = True

                'combobox
                Dim comboBoxColumn As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
                comboBoxColumn.HeaderText = "LANGUAGE_SKILL_NAME_TH"
                comboBoxColumn.DataPropertyName = "LANGUAGE_SKILL_CODE"
                comboBoxColumn.DataSource = ds.Tables("MB_LANGUAGE_SKILL")
                comboBoxColumn.ValueMember = "LANGUAGE_SKILL_CODE"
                comboBoxColumn.DisplayMember = "LANGUAGE_SKILL_NAME_TH"

                'DataGridView4.Columns.RemoveAt(2)
                DataGridView4.Columns.Insert(2, comboBoxColumn)

                'DataGridView4.Columns("LANGUAGE_SKILL_CODE").Width = 150
                'DataGridView4.Columns("INSTITUTE_NAME").Width = 150

                comboBoxColumn.Width = 150
            End If

            'DataGridView4.Columns("OU_CODE").Visible = False
            DataGridView4.Columns("CONTACT_CODE").Visible = False

            '=========================================================

            'skill
            'parameters.Clear()
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            query = "SELECT CONTACT_CODE, SKILL_SEQ, SKILL_CODE FROM MB_CONTACT_SKILL WHERE CONTACT_CODE = @p0 "

            Dim dtMB_CONTACT_SKILL As DataTable = New DataTable

            dtMB_CONTACT_SKILL = fillWebSQL(query, parameters, "MB_CONTACT_SKILL")

            If ds.Tables.Contains("MB_CONTACT_SKILL") = True Then
                ds.Tables("MB_CONTACT_SKILL").Clear()
                ds.Tables("MB_CONTACT_SKILL").Merge(dtMB_CONTACT_SKILL)
                ds.Tables("MB_CONTACT_SKILL").AcceptChanges()
            Else
                ds.Tables.Add(dtMB_CONTACT_SKILL)

                'primary key
                ds.Tables("MB_CONTACT_SKILL").PrimaryKey = New DataColumn() {ds.Tables("MB_CONTACT_SKILL").Columns("CONTACT_CODE"), ds.Tables("MB_CONTACT_SKILL").Columns("SKILL_SEQ"), ds.Tables("MB_CONTACT_SKILL").Columns("SKILL_CODE")}

                DataGridView5.DataSource = ds.Tables("MB_CONTACT_SKILL")

                For i As Integer = 0 To DataGridView5.ColumnCount - 1
                    DataGridView5.Columns(i).Visible = False
                Next
                '
                DataGridView5.Columns("SKILL_SEQ").Visible = True
                'DataGridView2.Columns("DEPARTMENT_NAME").Visible = True
                'DataGridView2.Columns("INSTITUTE_NAME").Visible = True
                'DataGridView2.Columns("YEAR_EDUCATED").Visible = True

                'combobox
                Dim comboBoxColumn As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
                comboBoxColumn.HeaderText = "SKILL_DESC"
                comboBoxColumn.DataPropertyName = "SKILL_CODE"
                comboBoxColumn.DataSource = ds.Tables("MB_SKILL")
                comboBoxColumn.ValueMember = "SKILL_CODE"
                comboBoxColumn.DisplayMember = "SKILL_DESC"

                'DataGridView5.Columns.RemoveAt(2)
                DataGridView5.Columns.Insert(2, comboBoxColumn)

                'DataGridView4.Columns("LANGUAGE_SKILL_CODE").Width = 150
                'DataGridView4.Columns("INSTITUTE_NAME").Width = 150

                comboBoxColumn.Width = 250
            End If

            'DataGridView4.Columns("OU_CODE").Visible = False
            DataGridView5.Columns("CONTACT_CODE").Visible = False

            '=========================================================

            'work
            'parameters.Clear()
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            query = String.Empty
            query &= "SELECT        MB_CONTACT_WORK.CONTACT_CODE, MB_CONTACT_WORK.WORK_SEQ, MB_CONTACT_WORK.START_MONTH, "
            query &= "                         MB_CONTACT_WORK.START_YEAR, MB_CONTACT_WORK.END_MONTH, MB_CONTACT_WORK.END_YEAR, MB_CONTACT_WORK.POSITION_CODE, MB_POSITION.POSITION_NAME_TH, MB_POSITION.POSITION_NAME_EN, "
            query &= "                         MB_CONTACT_WORK.WORK_PLACE, MB_CONTACT_WORK.WORK_DESC "
            query &= "FROM            MB_CONTACT_WORK LEFT OUTER JOIN "
            query &= "                         MB_POSITION ON MB_CONTACT_WORK.POSITION_CODE = MB_POSITION.POSITION_CODE "
            query &= "WHERE        (MB_CONTACT_WORK.CONTACT_CODE = @p0) "
            query &= "ORDER BY MB_CONTACT_WORK.WORK_SEQ"

            Dim dtMB_CONTACT_WORK As DataTable = New DataTable

            dtMB_CONTACT_WORK = fillWebSQL(query, parameters, "MB_CONTACT_WORK")

            If ds.Tables.Contains("MB_CONTACT_WORK") = True Then
                ds.Tables("MB_CONTACT_WORK").Clear()
                ds.Tables("MB_CONTACT_WORK").Merge(dtMB_CONTACT_WORK)
                ds.Tables("MB_CONTACT_WORK").AcceptChanges()
            Else
                ds.Tables.Add(dtMB_CONTACT_WORK)

                'primary key
                ds.Tables("MB_CONTACT_WORK").PrimaryKey = New DataColumn() {ds.Tables("MB_CONTACT_WORK").Columns("WORK_SEQ")}

                DataGridView6.DataSource = ds.Tables("MB_CONTACT_WORK")

                'For i As Integer = 0 To DataGridView6.ColumnCount - 1
                '    DataGridView6.Columns(i).Visible = False
                'Next
                '
                'DataGridView6.Columns("WORK_SEQ").Visible = True
                'DataGridView2.Columns("DEPARTMENT_NAME").Visible = True
                'DataGridView2.Columns("INSTITUTE_NAME").Visible = True
                'DataGridView2.Columns("YEAR_EDUCATED").Visible = True

                'DataGridView6.Columns("CONTACT_CODE").Visible = False
                'DataGridView6.Columns("POSITION_CODE").Visible = True


                'combobox
                'Dim comboBoxColumn As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
                'comboBoxColumn.HeaderText = "SKILL_CODE"
                'comboBoxColumn.DataPropertyName = "SKILL_CODE"
                'comboBoxColumn.DataSource = ds.Tables("MB_SKILL")
                'comboBoxColumn.ValueMember = "SKILL_CODE"
                'comboBoxColumn.DisplayMember = "SKILL_DESC"

                'DataGridView5.Columns.RemoveAt(2)
                'DataGridView5.Columns.Insert(2, comboBoxColumn)

                DataGridView6.Columns("WORK_PLACE").Width = 150
                'DataGridView4.Columns("INSTITUTE_NAME").Width = 150

                'comboBoxColumn.Width = 250
            End If

            DataGridView6.Columns("CONTACT_CODE").Visible = False
            DataGridView6.Columns("POSITION_CODE").Visible = False

            '=========================================================

            'insignai
            'parameters.Clear()
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            query = String.Empty
            query &= "SELECT        CONTACT_CODE, INSIGNIA_SEQ, INSIGNIA_DATE, INSIGNIA_LEVEL, INSIGNIA_REMARK "
            query &= "FROM            MB_CONTACT_INSIGNIA "
            query &= "WHERE        (CONTACT_CODE = @p0) "
            query &= "ORDER BY INSIGNIA_SEQ"

            Dim dtMB_CONTACT_INSIGNIA As DataTable = New DataTable

            dtMB_CONTACT_INSIGNIA = fillWebSQL(query, parameters, "MB_CONTACT_INSIGNIA")

            If ds.Tables.Contains("MB_CONTACT_INSIGNIA") = True Then
                ds.Tables("MB_CONTACT_INSIGNIA").Clear()
                ds.Tables("MB_CONTACT_INSIGNIA").Merge(dtMB_CONTACT_INSIGNIA)
                ds.Tables("MB_CONTACT_INSIGNIA").AcceptChanges()
            Else
                ds.Tables.Add(dtMB_CONTACT_INSIGNIA)

                'primary key
                ds.Tables("MB_CONTACT_INSIGNIA").PrimaryKey = New DataColumn() {ds.Tables("MB_CONTACT_INSIGNIA").Columns("INSIGNIA_SEQ")}

                DataGridView7.DataSource = ds.Tables("MB_CONTACT_INSIGNIA")

                'For i As Integer = 0 To DataGridView6.ColumnCount - 1
                '    DataGridView6.Columns(i).Visible = False
                'Next
                '
                DataGridView7.Columns("INSIGNIA_DATE").Visible = False
                'DataGridView2.Columns("DEPARTMENT_NAME").Visible = True
                'DataGridView2.Columns("INSTITUTE_NAME").Visible = True
                'DataGridView2.Columns("YEAR_EDUCATED").Visible = True

                'DataGridView6.Columns("CONTACT_CODE").Visible = False
                'DataGridView6.Columns("POSITION_CODE").Visible = True


                'combobox
                'Dim comboBoxColumn As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
                'comboBoxColumn.HeaderText = "SKILL_CODE"
                'comboBoxColumn.DataPropertyName = "SKILL_CODE"
                'comboBoxColumn.DataSource = ds.Tables("MB_SKILL")
                'comboBoxColumn.ValueMember = "SKILL_CODE"
                'comboBoxColumn.DisplayMember = "SKILL_DESC"

                'DataGridView5.Columns.RemoveAt(2)
                'DataGridView5.Columns.Insert(2, comboBoxColumn)

                Dim colSINSIGNIA_DATE As New CalendarColumn()
                colSINSIGNIA_DATE.HeaderText = "INSIGNIA_DATE"
                colSINSIGNIA_DATE.DataPropertyName = "INSIGNIA_DATE"
                'DataGridView7.Columns.RemoveAt(2)
                DataGridView7.Columns.Insert(2, colSINSIGNIA_DATE)

                DataGridView7.Columns("INSIGNIA_LEVEL").Width = 150
                'DataGridView4.Columns("INSTITUTE_NAME").Width = 150

                'comboBoxColumn.Width = 250
            End If

            DataGridView7.Columns("CONTACT_CODE").Visible = False
            'DataGridView7.Columns("POSITION_CODE").Visible = False

            '=========================================================

            'social
            'parameters.Clear()
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            query = String.Empty
            query &= "SELECT         CONTACT_CODE, SOCIAL_SEQ, SOCIAL_YEAR, SOCIAL_POSITION, SOCIAL_INSTITUTE, SOCIAL_WORK "
            query &= "FROM            MB_CONTACT_SOCIAL "
            query &= "WHERE        (CONTACT_CODE = @p0) "
            query &= "ORDER BY SOCIAL_SEQ"

            Dim dtMB_CONTACT_SOCIAL As DataTable = New DataTable

            dtMB_CONTACT_SOCIAL = fillWebSQL(query, parameters, "MB_CONTACT_SOCIAL")

            If ds.Tables.Contains("MB_CONTACT_SOCIAL") = True Then
                ds.Tables("MB_CONTACT_SOCIAL").Clear()
                ds.Tables("MB_CONTACT_SOCIAL").Merge(dtMB_CONTACT_SOCIAL)
                ds.Tables("MB_CONTACT_SOCIAL").AcceptChanges()
            Else
                ds.Tables.Add(dtMB_CONTACT_SOCIAL)

                'primary key
                ds.Tables("MB_CONTACT_SOCIAL").PrimaryKey = New DataColumn() {ds.Tables("MB_CONTACT_SOCIAL").Columns("SOCIAL_SEQ")}

                DataGridView8.DataSource = ds.Tables("MB_CONTACT_SOCIAL")

                'For i As Integer = 0 To DataGridView6.ColumnCount - 1
                '    DataGridView6.Columns(i).Visible = False
                'Next
                '
                'DataGridView6.Columns("WORK_SEQ").Visible = True
                'DataGridView2.Columns("DEPARTMENT_NAME").Visible = True
                'DataGridView2.Columns("INSTITUTE_NAME").Visible = True
                'DataGridView2.Columns("YEAR_EDUCATED").Visible = True

                'DataGridView6.Columns("CONTACT_CODE").Visible = False
                'DataGridView6.Columns("POSITION_CODE").Visible = True


                'combobox
                'Dim comboBoxColumn As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
                'comboBoxColumn.HeaderText = "SKILL_CODE"
                'comboBoxColumn.DataPropertyName = "SKILL_CODE"
                'comboBoxColumn.DataSource = ds.Tables("MB_SKILL")
                'comboBoxColumn.ValueMember = "SKILL_CODE"
                'comboBoxColumn.DisplayMember = "SKILL_DESC"

                'DataGridView5.Columns.RemoveAt(2)
                'DataGridView5.Columns.Insert(2, comboBoxColumn)

                DataGridView8.Columns("SOCIAL_POSITION").Width = 150
                DataGridView8.Columns("SOCIAL_INSTITUTE").Width = 150

                'comboBoxColumn.Width = 250
            End If

            DataGridView8.Columns("CONTACT_CODE").Visible = False
            'DataGridView7.Columns("POSITION_CODE").Visible = False

            '=========================================================

            'aword
            'parameters.Clear()
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            query = String.Empty
            query &= "SELECT         CONTACT_CODE, AWORD_SEQ, AWORD_YEAR, AWORD_DESC, AWORD_FROM "
            query &= "FROM            MB_CONTACT_AWORD "
            query &= "WHERE        (CONTACT_CODE = @p0) "
            query &= "ORDER BY AWORD_SEQ"

            Dim dtMB_CONTACT_AWORD As DataTable = New DataTable

            dtMB_CONTACT_AWORD = fillWebSQL(query, parameters, "MB_CONTACT_AWORD")

            If ds.Tables.Contains("MB_CONTACT_AWORD") = True Then
                ds.Tables("MB_CONTACT_AWORD").Clear()
                ds.Tables("MB_CONTACT_AWORD").Merge(dtMB_CONTACT_AWORD)
                ds.Tables("MB_CONTACT_AWORD").AcceptChanges()
            Else
                ds.Tables.Add(dtMB_CONTACT_AWORD)

                'primary key
                ds.Tables("MB_CONTACT_AWORD").PrimaryKey = New DataColumn() {ds.Tables("MB_CONTACT_AWORD").Columns("AWORD_SEQ")}

                DataGridView9.DataSource = ds.Tables("MB_CONTACT_AWORD")

                'For i As Integer = 0 To DataGridView6.ColumnCount - 1
                '    DataGridView6.Columns(i).Visible = False
                'Next
                '
                'DataGridView6.Columns("WORK_SEQ").Visible = True
                'DataGridView2.Columns("DEPARTMENT_NAME").Visible = True
                'DataGridView2.Columns("INSTITUTE_NAME").Visible = True
                'DataGridView2.Columns("YEAR_EDUCATED").Visible = True

                'DataGridView6.Columns("CONTACT_CODE").Visible = False
                'DataGridView6.Columns("POSITION_CODE").Visible = True


                'combobox
                'Dim comboBoxColumn As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
                'comboBoxColumn.HeaderText = "SKILL_CODE"
                'comboBoxColumn.DataPropertyName = "SKILL_CODE"
                'comboBoxColumn.DataSource = ds.Tables("MB_SKILL")
                'comboBoxColumn.ValueMember = "SKILL_CODE"
                'comboBoxColumn.DisplayMember = "SKILL_DESC"

                'DataGridView5.Columns.RemoveAt(2)
                'DataGridView5.Columns.Insert(2, comboBoxColumn)

                DataGridView9.Columns("AWORD_DESC").Width = 150
                DataGridView9.Columns("AWORD_FROM").Width = 150

                'comboBoxColumn.Width = 250
            End If

            DataGridView9.Columns("CONTACT_CODE").Visible = False
            'DataGridView7.Columns("POSITION_CODE").Visible = False

            'get committee
            'query = "SELECT MB_COMMITTEE_PERIOD.PERIOD_SEQ, MB_COMMITTEE_PERIOD.PERIOD_CODE, MB_COMMITTEE_PERIOD.CONTACT_CODE, MB_POSITION.POSITION_NAME_TH FROM MB_COMMITTEE_PERIOD INNER JOIN MB_POSITION ON MB_COMMITTEE_PERIOD.GROUP_POSITION_CODE = MB_POSITION.POSITION_CODE WHERE MB_COMMITTEE_PERIOD.CONTACT_CODE = @p0 ORDER BY MB_COMMITTEE_PERIOD.PERIOD_CODE"
            query = String.Empty
            query &= "SELECT        MB_COMMITTEE_PERIOD.PERIOD_SEQ, MB_COMMITTEE_PERIOD.PERIOD_CODE, MB_COMMITTEE_PERIOD.CONTACT_CODE, MB_POSITION.POSITION_NAME_TH, MB_PERIOD.PERIOD_NAME, "
            query &= "                         MB_PERIOD.START_YEAR, MB_PERIOD.END_YEAR "
            query &= "FROM            MB_COMMITTEE_PERIOD INNER JOIN "
            query &= "                         MB_POSITION ON MB_COMMITTEE_PERIOD.GROUP_POSITION_CODE = MB_POSITION.POSITION_CODE AND MB_COMMITTEE_PERIOD.GROUP_POSITION_TYPE = MB_POSITION.POSITION_TYPE INNER JOIN "
            query &= "                         MB_PERIOD ON MB_COMMITTEE_PERIOD.PERIOD_CODE = MB_PERIOD.PERIOD_CODE WHERE MB_COMMITTEE_PERIOD.CONTACT_CODE = @p0 ORDER BY MB_COMMITTEE_PERIOD.PERIOD_CODE"

            Dim dtMB_COMMITTEE_PERIOD As DataTable = New DataTable

            dtMB_COMMITTEE_PERIOD = fillWebSQL(query, parameters, "MB_COMMITTEE_PERIOD")

            If ds.Tables.Contains("MB_COMMITTEE_PERIOD") = True Then
                ds.Tables("MB_COMMITTEE_PERIOD").Clear()
                ds.Tables("MB_COMMITTEE_PERIOD").Merge(dtMB_COMMITTEE_PERIOD)
                ds.Tables("MB_COMMITTEE_PERIOD").AcceptChanges()
            Else
                ds.Tables.Add(dtMB_COMMITTEE_PERIOD)

                'primary key
                ds.Tables("MB_COMMITTEE_PERIOD").PrimaryKey = New DataColumn() {ds.Tables("MB_COMMITTEE_PERIOD").Columns("PERIOD_CODE"), ds.Tables("MB_COMMITTEE_PERIOD").Columns("PERIOD_SEQ")}

                DataGridView10.DataSource = ds.Tables("MB_COMMITTEE_PERIOD")

                'For i As Integer = 0 To DataGridView7.Columns.Count - 1
                '    DataGridView7.Columns(i).Visible = False
                'Next

                DataGridView10.Columns("CONTACT_CODE").Visible = False
                DataGridView10.Columns("START_YEAR").Visible = False
                DataGridView10.Columns("END_YEAR").Visible = False

                DataGridView10.Columns("PERIOD_SEQ").Width = 50
                DataGridView10.Columns("POSITION_NAME_TH").Width = 150
                DataGridView10.Columns("PERIOD_NAME").Width = 150

                'DataGridView7.Columns("PERIOD_SEQ").Visible = True
                'DataGridView7.Columns("PERIOD_CODE").Visible = True
            End If

            'spouse
            If tbSPOUSE_CODE.TextLength > 0 Then
                parameters.Clear()
                parameters.Add("@p0", tbSPOUSE_CODE.Text)

                query = "SELECT        CONTACT_CODE, CONTACT_PRENAME_CODE, CONTACT_FIRST_NAME_TH, CONTACT_FIRST_NAME_EN, CONTACT_LAST_NAME_TH, CONTACT_LAST_NAME_EN, SEX, PERSONAL_ID, BIRTH_DATE "
                query &= "FROM            MB_CONTACT "
                query &= "WHERE        (CONTACT_CODE = @p0)"

                Dim dtSPOUSE As DataTable = New DataTable

                dtSPOUSE = fillWebSQL(query, parameters, "SPOUSE")

                ComboBox4.SelectedValue = Nothing
                ComboBox2.SelectedValue = Nothing
                ComboBox3.SelectedValue = Nothing

                TextBox6.Text = String.Empty
                TextBox4.Text = String.Empty
                TextBox2.Text = String.Empty
                TextBox3.Text = String.Empty

                TextBox5.Text = String.Empty
                MaskedTextBox2.Text = String.Empty

                If dtSPOUSE.Rows.Count > 0 Then
                    ComboBox4.SelectedValue = dtSPOUSE.Rows(0).Item("CONTACT_PRENAME_CODE")
                    ComboBox2.SelectedValue = dtSPOUSE.Rows(0).Item("CONTACT_PRENAME_CODE")
                    ComboBox3.SelectedValue = dtSPOUSE.Rows(0).Item("SEX")

                    TextBox6.Text = dtSPOUSE.Rows(0).Item("CONTACT_FIRST_NAME_TH").ToString
                    TextBox4.Text = dtSPOUSE.Rows(0).Item("CONTACT_LAST_NAME_TH").ToString
                    TextBox2.Text = dtSPOUSE.Rows(0).Item("CONTACT_FIRST_NAME_EN").ToString
                    TextBox3.Text = dtSPOUSE.Rows(0).Item("CONTACT_LAST_NAME_EN").ToString

                    TextBox5.Text = dtSPOUSE.Rows(0).Item("PERSONAL_ID").ToString
                    MaskedTextBox2.Text = If(IsDBNull(dtSPOUSE.Rows(0).Item("BIRTH_DATE")), "", CDate(dtSPOUSE.Rows(0).Item("BIRTH_DATE")).ToString("dd/MM/yyyy", ciTH))
                End If
            End If

            'mail address
            'reset default
            RadioButton1.Checked = True

            If IsDBNull(RadioButton1.Tag) = False Then
                If CBool(RadioButton1.Tag) = True Then
                    RadioButton1.Checked = True
                Else
                    RadioButton2.Checked = True
                End If
            End If
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.DoubleClick
        'change photo
        If DataGridView1.CurrentRow IsNot Nothing Then
            If OpenImageFileDlg.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                PictureBox1.Image = GetImageThumbnail(OpenImageFileDlg.FileName, 120, 150)
            End If
        End If

        'load photo
        'Dim bytBLOBData() As Byte = DirectCast(DsMemberCtrl1.membersImage.Rows(0).Item("ImageData"), Byte())
        'Dim stmBLOBData As New System.IO.MemoryStream(bytBLOBData)
        'PictureBox1.Image = Image.FromStream(stmBLOBData)
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        'new contact
        Dim f As New frmMainContactsNew
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

            'add it

            'generate code
            Dim CONTACT_CODE As String = "C" & getSQLDate().ToString("yyMMddHHmmss", ciTH)

            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", CONTACT_CODE)
            parameters.Add("@p1", f.TextBox9.Text)
            parameters.Add("@p2", f.TextBox10.Text)
            parameters.Add("@p3", f.TextBox30.Text)
            parameters.Add("@p4", f.TextBox31.Text)
            Dim query As String = "INSERT INTO MB_CONTACT (CONTACT_CODE, CONTACT_FIRST_NAME_TH, CONTACT_LAST_NAME_TH, CONTACT_FIRST_NAME_EN, CONTACT_LAST_NAME_EN) VALUES (@p0,@p1,@p2,@p3,@p4)"

            Try
                executeWebSQL(query, parameters)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            End Try

            'logs
            saveLOGS(CONTACT_CODE, "MB_CONTACT", "CONTACT_CODE", "ADD", "", f.TextBox9.Text, user_name)

            'focus on new data
            TextBox1.Text = CONTACT_CODE

            'load new data
            getCONTACT()

            'refresh grid
            gridControl1_CurrentRowChanged(sender, e)

            MessageBox.Show("เพิ่มบุคคลเสร็จสิ้น")
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        'del contact
        If tbCONTACT_CODE.TextLength > 0 Then
            Dim ispass As Boolean = False

            If ds.Tables("MB_COMMITTEE_PERIOD").Rows.Count > 0 Then
                Dim d As Date = getSQLDate()
                Dim dv As New DataView(ds.Tables("MB_COMMITTEE_PERIOD"))
                dv.RowFilter = String.Format("START_YEAR <= {0} AND {0} <= END_YEAR", d.ToString("yyyy", ciTH))

                Dim dt As DataTable = dv.ToTable

                If dt.Rows.Count > 0 Then
                    ispass = False
                Else
                    ispass = True
                End If
            Else
                ispass = True
            End If

            If ispass = True Then
                If MessageBox.Show("ยืนยันที่จะลบ " & TextBox9.Text, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    'delete it
                    Dim parameters As New Dictionary(Of String, Object)
                    parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                    Dim query As String = "DELETE FROM MB_CONTACT WHERE CONTACT_CODE = @p0"

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'delete photo
                    query = "DELETE FROM MB_CONTACT_PICTURE WHERE CONTACT_CODE = @p0"

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'delete edu

                    'logs
                    saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT", "CONTACT_CODE", "ADD", "", TextBox9.Text, user_name)

                    'refresh grid
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show("ลบบุคคลเสร็จสิ้น")
                End If
            End If
        Else
            MessageBox.Show("กรุณาตรวจสอบกรรมการ วาระปัจจุบัน")
        End If
    End Sub

    Private Sub btLocationsRepresents_Click(sender As Object, e As EventArgs) Handles btLocationsByHome.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            Dim f As New frmMainLocations
            f.TextBox1.Text = TextBox18.Text
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                'If f.RadioButton1.Checked = True Then
                '    'th only
                '    TextBox50.Text = f.DataGridView1.CurrentRow.Cells("SUB_DISTRICT_NAME_TH").Value.ToString
                '    TextBox51.Text = f.DataGridView1.CurrentRow.Cells("DISTRICT_NAME_TH").Value.ToString
                '    TextBox49.Text = f.DataGridView1.CurrentRow.Cells("PROVINCE_NAME_TH").Value.ToString
                'Else
                '    'th en
                '    TextBox50.Text = f.DataGridView1.CurrentRow.Cells("SUB_DISTRICT_NAME_TH").Value.ToString
                '    TextBox51.Text = f.DataGridView1.CurrentRow.Cells("DISTRICT_NAME_TH").Value.ToString
                '    TextBox49.Text = f.DataGridView1.CurrentRow.Cells("PROVINCE_NAME_TH").Value.ToString

                '    TextBox52.Text = f.DataGridView1.CurrentRow.Cells("SUB_DISTRICT_NAME_EN").Value.ToString
                '    TextBox53.Text = f.DataGridView1.CurrentRow.Cells("DISTRICT_NAME_EN").Value.ToString
                '    TextBox54.Text = f.DataGridView1.CurrentRow.Cells("PROVINCE_NAME_EN").Value.ToString
                'End If

                'th en
                TextBox50.Text = f.DataGridView1.CurrentRow.Cells("SUB_DISTRICT_NAME_TH").Value.ToString
                TextBox51.Text = f.DataGridView1.CurrentRow.Cells("DISTRICT_NAME_TH").Value.ToString
                TextBox49.Text = f.DataGridView1.CurrentRow.Cells("PROVINCE_NAME_TH").Value.ToString

                TextBox52.Text = f.DataGridView1.CurrentRow.Cells("SUB_DISTRICT_NAME_EN").Value.ToString
                TextBox53.Text = f.DataGridView1.CurrentRow.Cells("DISTRICT_NAME_EN").Value.ToString
                TextBox54.Text = f.DataGridView1.CurrentRow.Cells("PROVINCE_NAME_EN").Value.ToString

                TextBox18.Text = f.DataGridView1.CurrentRow.Cells("POSTCODE").Value.ToString
            End If
            f.Dispose()
            f = Nothing
        End If
    End Sub

    Private Sub btRepresentPhotoSave_Click(sender As Object, e As EventArgs) Handles btRepresentPhotoSave.Click
        If OpenImageFileDlg.FileName.Length > 0 Then
            If MessageBox.Show("ยืนยันที่จะ" & btRepresentPhotoSave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'get exist image?
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                Dim query As String = String.Empty
                query &= "SELECT        COUNT(*) AS CNT "
                query &= "FROM            MB_CONTACT_PICTURE "
                query &= "WHERE        (CONTACT_CODE = @p0)"

                Dim CNT As Integer = 0
                Try
                    CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT")
                End Try

                Dim result As Integer = 0
                Dim bytBLOBData() As Byte = ImageToByte(GetImageThumbnail(OpenImageFileDlg.FileName, 120, 150))

                If CNT > 0 Then
                    'update
                    Dim param As New SqlClient.SqlParameter("@p0", SqlDbType.VarBinary, bytBLOBData.Length, ParameterDirection.Input, True, 0, 0, "PICTURE_DATA", DataRowVersion.Current, bytBLOBData)

                    parameters.Clear()
                    parameters.Add("@p0", param.Value)
                    parameters.Add("@p", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)
                    query = "UPDATE MB_CONTACT_PICTURE SET PICTURE_DATA = @p0 WHERE CONTACT_CODE = @p"

                    result = 0
                    Try
                        result = executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history
                    saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_PICTURE", "CONTACT_CODE", "UPDATE", "", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, user_name)
                Else
                    'add
                    'read file
                    Dim param As New SqlClient.SqlParameter("@p1", SqlDbType.VarBinary, bytBLOBData.Length, ParameterDirection.Input, True, 0, 0, "PICTURE_DATA", DataRowVersion.Current, bytBLOBData)

                    parameters.Clear()
                    parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)
                    parameters.Add("@p1", param.Value)
                    query = "INSERT INTO MB_CONTACT_PICTURE(CONTACT_CODE, PICTURE_DATA) VALUES (@p0,@p1)"

                    result = 0
                    Try
                        result = executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history
                    saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_PICTURE", "CONTACT_CODE", "ADD", "", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, user_name)
                End If

                OpenImageFileDlg.FileName = String.Empty

                MessageBox.Show("บันทึกเสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btRepresentAddressSave_Click(sender As Object, e As EventArgs) Handles btRepresentAddressSave.Click
        If tbCONTACT_CODE.TextLength > 0 Then
            DataGridView1.EndEdit()
            bsRep.EndEdit()

            Dim ispass As Boolean = False

            If ds.Tables("MB_COMMITTEE_PERIOD").Rows.Count > 0 Then
                Dim d As Date = getSQLDate()
                Dim dv As New DataView(ds.Tables("MB_COMMITTEE_PERIOD"))
                dv.RowFilter = String.Format("START_YEAR <= {0} AND {0} <= END_YEAR", d.ToString("yyyy", ciTH))

                Dim dt As DataTable = dv.ToTable

                If dt.Rows.Count > 0 Then
                    ispass = False
                Else
                    ispass = True
                End If
            Else
                ispass = True
            End If

            If ispass = True Then
                If MessageBox.Show("ยืนยันที่จะ" & btRepresentAddressSave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim row As DataRow = ds.Tables("MB_CONTACT").Rows.Find(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                    Dim parameters As New Dictionary(Of String, Object)

                    'save MB_MEMBERS
                    Dim query As String = String.Empty

                    'update it
                    parameters.Clear()
                    parameters.Add("@p0", If(IsDBNull(row("CONTACT_FIRST_NAME_TH")), "", row("CONTACT_FIRST_NAME_TH")))
                    parameters.Add("@p1", If(IsDBNull(row("CONTACT_LAST_NAME_TH")), "", row("CONTACT_LAST_NAME_TH")))
                    parameters.Add("@p2", If(IsDBNull(row("ADDR_NO_TH")), "", row("ADDR_NO_TH")))
                    parameters.Add("@p3", If(IsDBNull(row("ADDR_MOO_TH")), "", row("ADDR_MOO_TH")))
                    parameters.Add("@p4", If(IsDBNull(row("ADDR_SOI_TH")), "", row("ADDR_SOI_TH")))
                    parameters.Add("@p5", If(IsDBNull(row("ADDR_ROAD_TH")), "", row("ADDR_ROAD_TH")))
                    parameters.Add("@p6", If(IsDBNull(row("ADDR_SUB_DISTRICT_TH")), "", row("ADDR_SUB_DISTRICT_TH")))
                    parameters.Add("@p7", If(IsDBNull(row("ADDR_DISTRICT_TH")), "", row("ADDR_DISTRICT_TH")))
                    parameters.Add("@p8", If(IsDBNull(row("ADDR_PROVINCE_NAME_TH")), "", row("ADDR_PROVINCE_NAME_TH")))

                    parameters.Add("@p9", If(IsDBNull(row("CONTACT_FIRST_NAME_EN")), "", row("CONTACT_FIRST_NAME_EN")))
                    parameters.Add("@p10", If(IsDBNull(row("CONTACT_LAST_NAME_EN")), "", row("CONTACT_LAST_NAME_EN")))
                    parameters.Add("@p11", If(IsDBNull(row("ADDR_NO_EN")), "", row("ADDR_NO_EN")))
                    parameters.Add("@p12", If(IsDBNull(row("ADDR_MOO_EN")), "", row("ADDR_MOO_EN")))
                    parameters.Add("@p13", If(IsDBNull(row("ADDR_SOI_EN")), "", row("ADDR_SOI_EN")))
                    parameters.Add("@p14", If(IsDBNull(row("ADDR_ROAD_EN")), "", row("ADDR_ROAD_EN")))
                    parameters.Add("@p15", If(IsDBNull(row("ADDR_SUB_DISTRICT_EN")), "", row("ADDR_SUB_DISTRICT_EN")))
                    parameters.Add("@p16", If(IsDBNull(row("ADDR_DISTRICT_EN")), "", row("ADDR_DISTRICT_EN")))
                    parameters.Add("@p17", If(IsDBNull(row("ADDR_PROVINCE_NAME_EN")), "", row("ADDR_PROVINCE_NAME_EN")))

                    parameters.Add("@p18", If(IsDBNull(row("ADDR_TELEPHONE")), "", row("ADDR_TELEPHONE")))
                    parameters.Add("@p19", If(IsDBNull(row("ADDR_FAX")), "", row("ADDR_FAX")))
                    parameters.Add("@p20", If(IsDBNull(row("ADDR_EMAIL")), "", row("ADDR_EMAIL")))
                    parameters.Add("@p21", If(IsDBNull(row("CONTACT_PRENAME_CODE")), "", row("CONTACT_PRENAME_CODE")))
                    parameters.Add("@p22", If(IsDBNull(row("SEX")), "", row("SEX")))
                    parameters.Add("@p23", If(IsDBNull(row("ADDR_POSTCODE")), "", row("ADDR_POSTCODE")))
                    parameters.Add("@p24", If(IsDBNull(row("PERSONAL_ID")), "", row("PERSONAL_ID")))

                    parameters.Add("@p", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                    'ADDR_ROAD_EN,ADDR_SUB_DISTRICT_EN,ADDR_DISTRICT_EN,ADDR_PROVINCE_NAME_EN,ADDR_TELEPHONE,ADDR_FAX,ADDR_EMAIL,CONTACT_PRENAME_CODE,SEX,ADDR_POSTCODE
                    'MB_CONTACT
                    query = "UPDATE MB_CONTACT SET "
                    query &= "CONTACT_FIRST_NAME_TH = @p0, CONTACT_LAST_NAME_TH = @p1, ADDR_NO_TH = @p2, ADDR_MOO_TH = @p3, ADDR_SOI_TH = @p4, ADDR_ROAD_TH = @p5, ADDR_SUB_DISTRICT_TH = @p6, ADDR_DISTRICT_TH = @p7, ADDR_PROVINCE_NAME_TH = @p8, "
                    query &= "CONTACT_FIRST_NAME_EN = @p9, CONTACT_LAST_NAME_EN = @p10, ADDR_NO_EN = @p11, ADDR_MOO_EN = @p12, ADDR_SOI_EN = @p13, ADDR_ROAD_EN = @p14, ADDR_SUB_DISTRICT_EN = @p15, ADDR_DISTRICT_EN = @p16, ADDR_PROVINCE_NAME_EN = @p17, "
                    query &= "ADDR_TELEPHONE = @p18, ADDR_FAX = @p19, ADDR_EMAIL = @p20, CONTACT_PRENAME_CODE = @p21, SEX = @p22, ADDR_POSTCODE = @p23, PERSONAL_ID = @p24 "
                    query &= "WHERE CONTACT_CODE = @p"

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history 
                    Dim cols() As String = "CONTACT_FIRST_NAME_TH,CONTACT_LAST_NAME_TH,ADDR_NO_TH,ADDR_MOO_TH,ADDR_SOI_TH,ADDR_ROAD_TH,ADDR_SUB_DISTRICT_TH,ADDR_DISTRICT_TH,ADDR_PROVINCE_NAME_TH,CONTACT_FIRST_NAME_EN,CONTACT_LAST_NAME_EN,ADDR_NO_EN,ADDR_MOO_EN,ADDR_SOI_EN,ADDR_ROAD_EN,ADDR_SUB_DISTRICT_EN,ADDR_DISTRICT_EN,ADDR_PROVINCE_NAME_EN,ADDR_TELEPHONE,ADDR_FAX,ADDR_EMAIL,CONTACT_PRENAME_CODE,SEX,ADDR_POSTCODE,PERSONAL_ID,ADDR_NO_TH1,ADDR_MOO_TH1,ADDR_SOI_TH1,ADDR_ROAD_TH1,ADDR_SUB_DISTRICT_TH1,ADDR_DISTRICT_TH1,ADDR_PROVINCE_NAME_TH1,ADDR_NO_EN1,ADDR_MOO_EN1,ADDR_SOI_EN1,ADDR_ROAD_EN1,ADDR_SUB_DISTRICT_EN1,ADDR_DISTRICT_EN1,ADDR_PROVINCE_NAME_EN1,ADDR_POSTCODE1,ADDR_TELEPHONE1,ADDR_FAX1,ADDR_EMAIL1".Split(","c)
                    For i As Integer = 0 To cols.Length - 1
                        If row.HasVersion(DataRowVersion.Current) = True Then
                            If row(cols(i), DataRowVersion.Original).ToString <> row(cols(i), DataRowVersion.Current).ToString Then
                                'save it
                                saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT", cols(i), "UPDATE", If(IsDBNull(row(cols(i), DataRowVersion.Original)), "", row(cols(i), DataRowVersion.Original)), If(IsDBNull(row(cols(i), DataRowVersion.Current)), "", row(cols(i), DataRowVersion.Current)), user_name)
                            End If
                        End If
                    Next

                    'refresh grid
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show("บันทึกเสร็จสิ้น")
                End If
            End If
        Else
            MessageBox.Show("กรุณาตรวจสอบกรรมการ วาระปัจจุบัน")
        End If
    End Sub

    Private Sub btEDUadd_Click(sender As Object, e As EventArgs) Handles btEDUadd.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            bsRep.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btEDUadd.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_CONTACT").Rows.Find(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", row("CONTACT_CODE"))

                Dim query As String = String.Empty
                query &= "SELECT    TOP 1    EDUCATION_SEQ "
                query &= "FROM            MB_CONTACT_EDUCATION "
                query &= "WHERE        (CONTACT_CODE = @p0) ORDER BY EDUCATION_SEQ DESC"

                Dim objEDUCATION_SEQ As Object = Nothing
                Try
                    objEDUCATION_SEQ = client.ExecuteScalar(query, parameters, user_session)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar EDUCATION_SEQ")
                End Try

                If objEDUCATION_SEQ Is Nothing Then objEDUCATION_SEQ = 1

                If objEDUCATION_SEQ IsNot Nothing Then
                    If IsDBNull(objEDUCATION_SEQ) = True Then
                        objEDUCATION_SEQ = 0
                    End If

                    Dim EDUCATION_SEQ As Integer = CInt(objEDUCATION_SEQ) + 1

                    query = "INSERT INTO MB_CONTACT_EDUCATION (CONTACT_CODE, EDUCATION_SEQ) VALUES (@p0,@p1)"
                    'parameters.Add("@p0", row("CONTACT_CODE"))
                    parameters.Add("@p1", EDUCATION_SEQ)

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history 
                    saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_EDUCATION", "EDUCATION_SEQ", "ADD", 0, EDUCATION_SEQ, user_name)

                    'refresh grid
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show("บันทึกเสร็จสิ้น + " & EDUCATION_SEQ)
                Else
                    MessageBox.Show("ไม่พบ " & row("CONTACT_CODE").ToString)
                End If


            End If
        End If
    End Sub

    Private Sub btEDUdel_Click(sender As Object, e As EventArgs) Handles btEDUdel.Click
        If DataGridView2.CurrentRow IsNot Nothing Then
            DataGridView2.EndEdit()
            bsRep.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btEDUdel.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_CONTACT_EDUCATION").Rows.Find(DataGridView2.CurrentRow.Cells("EDUCATION_SEQ").Value)

                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", row("CONTACT_CODE"))
                parameters.Add("@p1", DataGridView2.CurrentRow.Cells("EDUCATION_SEQ").Value)

                Dim query As String = "DELETE FROM MB_CONTACT_EDUCATION WHERE (CONTACT_CODE = @p0) AND (EDUCATION_SEQ = @p1)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history 
                saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_EDUCATION", "EDUCATION_SEQ", "DELETE", DataGridView2.CurrentRow.Cells("EDUCATION_SEQ").Value, 0, user_name)

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btEDUsave_Click(sender As Object, e As EventArgs) Handles btEDUsave.Click

        If DataGridView2.CurrentRow IsNot Nothing Then
            DataGridView2.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btEDUsave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Dim row As DataRow = ds.Tables("MB_CONTACT_EDUCATION").Rows.Find(DataGridView2.CurrentRow.Cells("EDUCATION_SEQ").Value)

                Dim cols() As String = "EDUCATION_SEQ,EDUCATION_CODE,DEPARTMENT_NAME,INSTITUTE_NAME,YEAR_EDUCATED".Split(","c)
                For i As Integer = 0 To cols.Length - 1
                    If row.HasVersion(DataRowVersion.Current) = True Then
                        If row(cols(i), DataRowVersion.Original).ToString <> row(cols(i), DataRowVersion.Current).ToString Then
                            'save it
                            saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_EDUCATION", cols(i), "UPDATE", If(IsDBNull(row(cols(i), DataRowVersion.Original)), "", row(cols(i), DataRowVersion.Original)), If(IsDBNull(row(cols(i), DataRowVersion.Current)), "", row(cols(i), DataRowVersion.Current)), user_name)
                        End If
                    End If
                Next

                'update it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                Dim query As String = "SELECT * FROM MB_CONTACT_EDUCATION WHERE CONTACT_CODE = @p0 ORDER BY EDUCATION_SEQ "

                If ds.Tables("MB_CONTACT_EDUCATION").GetChanges IsNot Nothing Then
                    Try
                        updateWebSQL(query, parameters, ds.Tables("MB_CONTACT_EDUCATION"))
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

    Private Sub btTRAINadd_Click(sender As Object, e As EventArgs) Handles btTRAINadd.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            bsRep.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btTRAINadd.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_CONTACT").Rows.Find(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", row("CONTACT_CODE"))

                Dim query As String = String.Empty
                query &= "SELECT    TOP 1    TRAIN_SEQ "
                query &= "FROM            MB_CONTACT_TRAINNING "
                query &= "WHERE        (CONTACT_CODE = @p0) ORDER BY TRAIN_SEQ DESC"

                Dim objEDUCATION_SEQ As Object = Nothing
                Try
                    objEDUCATION_SEQ = client.ExecuteScalar(query, parameters, user_session)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar TRAIN_SEQ")
                End Try

                If objEDUCATION_SEQ Is Nothing Then objEDUCATION_SEQ = 1

                If objEDUCATION_SEQ IsNot Nothing Then
                    If IsDBNull(objEDUCATION_SEQ) = True Then
                        objEDUCATION_SEQ = 0
                    End If

                    Dim EDUCATION_SEQ As Integer = CInt(objEDUCATION_SEQ) + 1

                    query = "INSERT INTO MB_CONTACT_TRAINNING (CONTACT_CODE, TRAIN_SEQ) VALUES (@p0,@p1)"
                    'parameters.Add("@p0", row("CONTACT_CODE"))
                    parameters.Add("@p1", EDUCATION_SEQ)

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history 
                    saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_TRAINNING", "TRAIN_SEQ", "ADD", 0, EDUCATION_SEQ, user_name)

                    'refresh grid
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show("บันทึกเสร็จสิ้น + " & EDUCATION_SEQ)
                Else
                    MessageBox.Show("ไม่พบ " & row("CONTACT_CODE").ToString)
                End If


            End If
        End If
    End Sub

    Private Sub btTRAINdel_Click(sender As Object, e As EventArgs) Handles btTRAINdel.Click
        If DataGridView3.CurrentRow IsNot Nothing Then
            DataGridView3.EndEdit()
            bsRep.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btTRAINdel.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_CONTACT_TRAINNING").Rows.Find(DataGridView3.CurrentRow.Cells("TRAIN_SEQ").Value)

                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", row("CONTACT_CODE"))
                parameters.Add("@p1", DataGridView3.CurrentRow.Cells("TRAIN_SEQ").Value)

                Dim query As String = "DELETE FROM MB_CONTACT_TRAINNING WHERE (CONTACT_CODE = @p0) AND (TRAIN_SEQ = @p1)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history 
                saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_TRAINNING", "TRAIN_SEQ", "DELETE", DataGridView3.CurrentRow.Cells("TRAIN_SEQ").Value, 0, user_name)

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btTRAINsave_Click(sender As Object, e As EventArgs) Handles btTRAINsave.Click

        If DataGridView3.CurrentRow IsNot Nothing Then
            DataGridView3.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btTRAINsave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Dim row As DataRow = ds.Tables("MB_CONTACT_TRAINNING").Rows.Find(DataGridView3.CurrentRow.Cells("TRAIN_SEQ").Value)

                Dim cols() As String = "TRAIN_SEQ,START_DATE,END_DATE,TRAIN_QTY,TRAIN_UNIT,TRAIN_DETAIL,TRAIN_BY".Split(","c)
                For i As Integer = 0 To cols.Length - 1
                    If row.HasVersion(DataRowVersion.Current) = True Then
                        If row(cols(i), DataRowVersion.Original).ToString <> row(cols(i), DataRowVersion.Current).ToString Then
                            'save it
                            saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_TRAINNING", cols(i), "UPDATE", If(IsDBNull(row(cols(i), DataRowVersion.Original)), "", row(cols(i), DataRowVersion.Original)), If(IsDBNull(row(cols(i), DataRowVersion.Current)), "", row(cols(i), DataRowVersion.Current)), user_name)
                        End If
                    End If
                Next

                'update it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                Dim query As String = "SELECT * FROM MB_CONTACT_TRAINNING WHERE CONTACT_CODE = @p0 ORDER BY TRAIN_SEQ "

                If ds.Tables("MB_CONTACT_TRAINNING").GetChanges IsNot Nothing Then
                    Try
                        updateWebSQL(query, parameters, ds.Tables("MB_CONTACT_TRAINNING"))
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

    Private Sub btLANGadd_Click(sender As Object, e As EventArgs) Handles btLANGadd.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            bsRep.EndEdit()

            Dim f As New frmMainContactsLangNew
            f.CONTACT_CODE = DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString
            f.ComboBox1.DataSource = ds.Tables("MB_LANGUAGE_SKILL").Copy
            f.ComboBox1.DisplayMember = "LANGUAGE_SKILL_NAME_TH"
            f.ComboBox1.ValueMember = "LANGUAGE_SKILL_CODE"

            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                If MessageBox.Show("ยืนยันที่จะ" & btLANGadd.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim row As DataRow = ds.Tables("MB_CONTACT").Rows.Find(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                    'get latest number
                    Dim parameters As New Dictionary(Of String, Object)
                    'Dim parameters As New Dictionary(Of String, Object)
                    parameters.Add("@p0", row("CONTACT_CODE"))

                    Dim query As String = String.Empty
                    query &= "SELECT    TOP 1    LANGUAGE_SEQ "
                    query &= "FROM            MB_CONTACT_LANGUAGE "
                    query &= "WHERE        (CONTACT_CODE = @p0) ORDER BY LANGUAGE_SEQ DESC"

                    Dim objEDUCATION_SEQ As Object = Nothing
                    Try
                        objEDUCATION_SEQ = client.ExecuteScalar(query, parameters, user_session)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar LANGUAGE_SEQ")
                    End Try

                    If objEDUCATION_SEQ Is Nothing Then objEDUCATION_SEQ = 1

                    If objEDUCATION_SEQ IsNot Nothing Then
                        If IsDBNull(objEDUCATION_SEQ) = True Then
                            objEDUCATION_SEQ = 0
                        End If

                        Dim EDUCATION_SEQ As Integer = CInt(objEDUCATION_SEQ) + 1

                        query = "INSERT INTO MB_CONTACT_LANGUAGE (CONTACT_CODE, LANGUAGE_SEQ, LANGUAGE_SKILL_CODE) VALUES (@p0,@p1,@p2)"
                        parameters.Add("@p1", EDUCATION_SEQ)
                        parameters.Add("@p2", f.ComboBox1.SelectedValue)

                        Try
                            executeWebSQL(query, parameters)
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                        End Try

                        'save history 
                        saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_LANGUAGE", "LANGUAGE_SKILL_CODE", "ADD", 0, f.ComboBox1.SelectedValue, user_name)

                        'refresh grid
                        gridControl1_CurrentRowChanged(sender, e)

                        MessageBox.Show("บันทึกเสร็จสิ้น + " & EDUCATION_SEQ)
                    Else
                        MessageBox.Show("ไม่พบ " & row("CONTACT_CODE").ToString)
                    End If


                End If
            End If
            f.Dispose()
            f = Nothing


        End If
    End Sub

    Private Sub btLANGdel_Click(sender As Object, e As EventArgs) Handles btLANGdel.Click
        If DataGridView4.CurrentRow IsNot Nothing Then
            DataGridView4.EndEdit()
            bsRep.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btLANGdel.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_CONTACT_LANGUAGE").Rows.Find(DataGridView4.CurrentRow.Cells("LANGUAGE_SKILL_CODE").Value)

                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", row("CONTACT_CODE"))
                parameters.Add("@p1", DataGridView4.CurrentRow.Cells("LANGUAGE_SKILL_CODE").Value)

                Dim query As String = "DELETE FROM MB_CONTACT_LANGUAGE WHERE (CONTACT_CODE = @p0) AND (LANGUAGE_SKILL_CODE = @p1)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history 
                saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_LANGUAGE", "LANGUAGE_SKILL_CODE", "DELETE", DataGridView4.CurrentRow.Cells("LANGUAGE_SKILL_CODE").Value, 0, user_name)

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btLANGsave_Click(sender As Object, e As EventArgs) Handles btLANGsave.Click

        If DataGridView4.CurrentRow IsNot Nothing Then
            DataGridView4.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btLANGsave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Dim row As DataRow = ds.Tables("MB_CONTACT_LANGUAGE").Rows.Find(DataGridView4.CurrentRow.Cells("LANGUAGE_SKILL_CODE").Value)

                Dim cols() As String = "LANGUAGE_SEQ,LANGUAGE_SKILL_CODE".Split(","c)
                For i As Integer = 0 To cols.Length - 1
                    If row.HasVersion(DataRowVersion.Current) = True Then
                        If row(cols(i), DataRowVersion.Original).ToString <> row(cols(i), DataRowVersion.Current).ToString Then
                            'save it
                            saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_LANGUAGE", cols(i), "UPDATE", If(IsDBNull(row(cols(i), DataRowVersion.Original)), "", row(cols(i), DataRowVersion.Original)), If(IsDBNull(row(cols(i), DataRowVersion.Current)), "", row(cols(i), DataRowVersion.Current)), user_name)
                        End If
                    End If
                Next

                'update it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                Dim query As String = "SELECT * FROM MB_CONTACT_LANGUAGE WHERE CONTACT_CODE = @p0 ORDER BY LANGUAGE_SEQ "

                If ds.Tables("MB_CONTACT_LANGUAGE").GetChanges IsNot Nothing Then
                    Try
                        updateWebSQL(query, parameters, ds.Tables("MB_CONTACT_LANGUAGE"))
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

    Private Sub btSKILLadd_Click(sender As Object, e As EventArgs) Handles btSKILLadd.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            bsRep.EndEdit()

            Dim f As New frmMainContactsSkillNew
            f.CONTACT_CODE = DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString
            f.ComboBox1.DataSource = ds.Tables("MB_SKILL").Copy
            f.ComboBox1.DisplayMember = "SKILL_DESC"
            f.ComboBox1.ValueMember = "SKILL_CODE"

            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                If MessageBox.Show("ยืนยันที่จะ" & btSKILLadd.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim row As DataRow = ds.Tables("MB_CONTACT").Rows.Find(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                    'get latest number
                    Dim parameters As New Dictionary(Of String, Object)
                    'Dim parameters As New Dictionary(Of String, Object)
                    parameters.Add("@p0", row("CONTACT_CODE"))

                    Dim query As String = String.Empty
                    query &= "SELECT    TOP 1    SKILL_SEQ "
                    query &= "FROM            MB_CONTACT_SKILL "
                    query &= "WHERE        (CONTACT_CODE = @p0) ORDER BY SKILL_SEQ DESC"

                    Dim objEDUCATION_SEQ As Object = Nothing
                    Try
                        objEDUCATION_SEQ = client.ExecuteScalar(query, parameters, user_session)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar SKILL_SEQ")
                    End Try

                    If objEDUCATION_SEQ Is Nothing Then objEDUCATION_SEQ = 1

                    If objEDUCATION_SEQ IsNot Nothing Then
                        If IsDBNull(objEDUCATION_SEQ) = True Then
                            objEDUCATION_SEQ = 0
                        End If

                        Dim EDUCATION_SEQ As Integer = CInt(objEDUCATION_SEQ) + 1

                        query = "INSERT INTO MB_CONTACT_SKILL (CONTACT_CODE, SKILL_SEQ, SKILL_CODE) VALUES (@p0,@p1,@p2)"
                        'parameters.Add("@p0", row("CONTACT_CODE"))
                        parameters.Add("@p1", EDUCATION_SEQ)
                        parameters.Add("@p2", f.ComboBox1.SelectedValue)

                        Try
                            executeWebSQL(query, parameters)
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                        End Try

                        'save history 
                        saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_LANGUAGE", "SKILL_CODE", "ADD", 0, f.ComboBox1.SelectedValue, user_name)

                        'refresh grid
                        gridControl1_CurrentRowChanged(sender, e)

                        MessageBox.Show("บันทึกเสร็จสิ้น + " & EDUCATION_SEQ)
                    Else
                        MessageBox.Show("ไม่พบ " & row("CONTACT_CODE").ToString)
                    End If


                End If
            End If
            f.Dispose()
            f = Nothing
        End If
    End Sub

    Private Sub btSKILLdel_Click(sender As Object, e As EventArgs) Handles btSKILLdel.Click
        If DataGridView5.CurrentRow IsNot Nothing Then
            DataGridView5.EndEdit()
            bsRep.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btSKILLdel.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_CONTACT_SKILL").Rows.Find(New Object() {DataGridView5.CurrentRow.Cells("CONTACT_CODE").Value, DataGridView5.CurrentRow.Cells("SKILL_SEQ").Value, DataGridView5.CurrentRow.Cells("SKILL_CODE").Value})

                '(New Object() {DataGridView1.CurrentRow.Cells("PERIOD_SEQ").Value, DataGridView1.CurrentRow.Cells("PERIOD_CODE").Value, DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value})
                'CONTACT_CODE, SKILL_SEQ, SKILL_CODE
                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", row("CONTACT_CODE"))
                parameters.Add("@p1", DataGridView5.CurrentRow.Cells("SKILL_CODE").Value)
                parameters.Add("@p1", DataGridView5.CurrentRow.Cells("SKILL_CODE").Value)

                Dim query As String = "DELETE FROM MB_CONTACT_SKILL WHERE (CONTACT_CODE = @p0) AND (SKILL_CODE = @p1)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history 
                saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_SKILL", "SKILL_CODE", "DELETE", DataGridView5.CurrentRow.Cells("SKILL_CODE").Value, 0, user_name)

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btSKILLsave_Click(sender As Object, e As EventArgs) Handles btSKILLsave.Click

        If DataGridView5.CurrentRow IsNot Nothing Then
            DataGridView5.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btLANGsave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Dim row As DataRow = ds.Tables("MB_CONTACT_SKILL").Rows.Find(DataGridView5.CurrentRow.Cells("SKILL_CODE").Value)

                Dim cols() As String = "SKILL_SEQ,SKILL_CODE".Split(","c)
                For i As Integer = 0 To cols.Length - 1
                    If row.HasVersion(DataRowVersion.Current) = True Then
                        If row(cols(i), DataRowVersion.Original).ToString <> row(cols(i), DataRowVersion.Current).ToString Then
                            'save it
                            saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_SKILL", cols(i), "UPDATE", If(IsDBNull(row(cols(i), DataRowVersion.Original)), "", row(cols(i), DataRowVersion.Original)), If(IsDBNull(row(cols(i), DataRowVersion.Current)), "", row(cols(i), DataRowVersion.Current)), user_name)
                        End If
                    End If
                Next

                'update it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                Dim query As String = "SELECT * FROM MB_CONTACT_SKILL WHERE CONTACT_CODE = @p0 ORDER BY LANGUAGE_SEQ "

                If ds.Tables("MB_CONTACT_SKILL").GetChanges IsNot Nothing Then
                    Try
                        updateWebSQL(query, parameters, ds.Tables("MB_CONTACT_SKILL"))
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

    Private Sub btWORKadd_Click(sender As Object, e As EventArgs) Handles btWORKadd.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            bsRep.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btWORKadd.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_CONTACT").Rows.Find(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", row("CONTACT_CODE"))

                Dim query As String = String.Empty
                query &= "SELECT    TOP 1    WORK_SEQ "
                query &= "FROM            MB_CONTACT_WORK "
                query &= "WHERE        (CONTACT_CODE = @p0) ORDER BY WORK_SEQ DESC"

                Dim objEDUCATION_SEQ As Object = Nothing
                Try
                    objEDUCATION_SEQ = client.ExecuteScalar(query, parameters, user_session)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar WORK_SEQ")
                End Try

                If objEDUCATION_SEQ Is Nothing Then objEDUCATION_SEQ = 1

                If objEDUCATION_SEQ IsNot Nothing Then
                    If IsDBNull(objEDUCATION_SEQ) = True Then
                        objEDUCATION_SEQ = 0
                    End If

                    Dim EDUCATION_SEQ As Integer = CInt(objEDUCATION_SEQ) + 1

                    query = "INSERT INTO MB_CONTACT_WORK (CONTACT_CODE, WORK_SEQ) VALUES (@p0,@p1)"
                    parameters.Add("@p1", EDUCATION_SEQ)

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history 
                    saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_WORK", "WORK_SEQ", "ADD", 0, EDUCATION_SEQ, user_name)

                    'refresh grid
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show("บันทึกเสร็จสิ้น + " & EDUCATION_SEQ)
                Else
                    MessageBox.Show("ไม่พบ " & row("CONTACT_CODE").ToString)
                End If


            End If
        End If
    End Sub

    Private Sub btWORKdel_Click(sender As Object, e As EventArgs) Handles btWORKdel.Click
        If DataGridView6.CurrentRow IsNot Nothing Then
            DataGridView6.EndEdit()
            bsRep.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btWORKdel.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_CONTACT_WORK").Rows.Find(DataGridView6.CurrentRow.Cells("WORK_SEQ").Value)

                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", row("CONTACT_CODE"))
                parameters.Add("@p1", DataGridView6.CurrentRow.Cells("WORK_SEQ").Value)

                Dim query As String = "DELETE FROM MB_CONTACT_WORK WHERE (CONTACT_CODE = @p0) AND (WORK_SEQ = @p1)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history 
                saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_WORK", "WORK_SEQ", "DELETE", DataGridView6.CurrentRow.Cells("WORK_SEQ").Value, 0, user_name)

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btWORKsave_Click(sender As Object, e As EventArgs) Handles btWORKsave.Click

        If DataGridView6.CurrentRow IsNot Nothing Then
            DataGridView6.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btWORKsave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Dim row As DataRow = ds.Tables("MB_CONTACT_WORK").Rows.Find(DataGridView6.CurrentRow.Cells("WORK_SEQ").Value)

                Dim cols() As String = "WORK_SEQ,START_MONTH,START_YEAR,END_MONTH,END_YEAR,POSITION_CODE,POSITION_TYPE,WORK_PLACE,WORK_DESC".Split(","c)
                For i As Integer = 0 To cols.Length - 1
                    If row.HasVersion(DataRowVersion.Current) = True Then
                        If row(cols(i), DataRowVersion.Original).ToString <> row(cols(i), DataRowVersion.Current).ToString Then
                            'save it
                            saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_WORK", cols(i), "UPDATE", If(IsDBNull(row(cols(i), DataRowVersion.Original)), "", row(cols(i), DataRowVersion.Original)), If(IsDBNull(row(cols(i), DataRowVersion.Current)), "", row(cols(i), DataRowVersion.Current)), user_name)
                        End If
                    End If
                Next

                'update it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                Dim query As String = "SELECT * FROM MB_CONTACT_WORK WHERE CONTACT_CODE = @p0 ORDER BY WORK_SEQ "

                If ds.Tables("MB_CONTACT_WORK").GetChanges IsNot Nothing Then
                    Try
                        updateWebSQL(query, parameters, ds.Tables("MB_CONTACT_WORK"))
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

    Private Sub btWORKposition_Click(sender As Object, e As EventArgs) Handles btWORKposition.Click
        If DataGridView6.CurrentRow IsNot Nothing Then
            Dim f As New frmMainPositions
            'f.TextBox1.Text = TextBox18.Text
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                Dim row As DataRow = ds.Tables("MB_CONTACT_WORK").Rows.Find(DataGridView6.CurrentRow.Cells("WORK_SEQ").Value)
                row("POSITION_CODE") = f.DataGridView1.CurrentRow.Cells("POSITION_CODE").Value.ToString

                'th en
                'TextBox16.Text = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_TH").Value.ToString
                'TextBox23.Text = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_EN").Value.ToString

                row("POSITION_NAME_TH") = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_TH").Value.ToString
                row("POSITION_NAME_EN") = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_EN").Value.ToString
            End If
            f.Dispose()
            f = Nothing
        End If

    End Sub

    Private Sub btINSIGNAIadd_Click(sender As Object, e As EventArgs) Handles btINSIGNAIadd.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            bsRep.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btINSIGNAIadd.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_CONTACT").Rows.Find(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", row("CONTACT_CODE"))

                Dim query As String = String.Empty
                query &= "SELECT    TOP 1    INSIGNIA_SEQ "
                query &= "FROM            MB_CONTACT_INSIGNIA "
                query &= "WHERE        (CONTACT_CODE = @p0) ORDER BY INSIGNIA_SEQ DESC"

                Dim objEDUCATION_SEQ As Object = Nothing
                Try
                    objEDUCATION_SEQ = client.ExecuteScalar(query, parameters, user_session)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar WORK_SEQ")
                End Try

                If objEDUCATION_SEQ Is Nothing Then objEDUCATION_SEQ = 1

                If objEDUCATION_SEQ IsNot Nothing Then
                    If IsDBNull(objEDUCATION_SEQ) = True Then
                        objEDUCATION_SEQ = 0
                    End If

                    Dim EDUCATION_SEQ As Integer = CInt(objEDUCATION_SEQ) + 1

                    query = "INSERT INTO MB_CONTACT_INSIGNIA (CONTACT_CODE, INSIGNIA_SEQ) VALUES (@p0,@p1)"
                    parameters.Add("@p1", EDUCATION_SEQ)

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history 
                    saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_INSIGNIA", "INSIGNIA_SEQ", "ADD", 0, EDUCATION_SEQ, user_name)

                    'refresh grid
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show("บันทึกเสร็จสิ้น + " & EDUCATION_SEQ)
                Else
                    MessageBox.Show("ไม่พบ " & row("CONTACT_CODE").ToString)
                End If


            End If
        End If
    End Sub

    Private Sub btINSIGNAIdel_Click(sender As Object, e As EventArgs) Handles btINSIGNAIdel.Click
        If DataGridView7.CurrentRow IsNot Nothing Then
            DataGridView7.EndEdit()
            bsRep.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btINSIGNAIdel.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_CONTACT_INSIGNIA").Rows.Find(DataGridView7.CurrentRow.Cells("INSIGNIA_SEQ").Value)

                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", row("CONTACT_CODE"))
                parameters.Add("@p1", DataGridView7.CurrentRow.Cells("INSIGNIA_SEQ").Value)

                Dim query As String = "DELETE FROM MB_CONTACT_INSIGNIA WHERE (CONTACT_CODE = @p0) AND (INSIGNIA_SEQ = @p1)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history 
                saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_INSIGNIA", "INSIGNIA_SEQ", "DELETE", DataGridView7.CurrentRow.Cells("INSIGNIA_SEQ").Value, 0, user_name)

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btINSIGNAIsave_Click(sender As Object, e As EventArgs) Handles btINSIGNAIsave.Click

        If DataGridView7.CurrentRow IsNot Nothing Then
            DataGridView7.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btINSIGNAIsave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Dim row As DataRow = ds.Tables("MB_CONTACT_INSIGNIA").Rows.Find(DataGridView7.CurrentRow.Cells("INSIGNIA_SEQ").Value)

                Dim cols() As String = "INSIGNIA_SEQ,INSIGNIA_DATE,INSIGNIA_LEVEL,INSIGNIA_REMARK".Split(","c)
                For i As Integer = 0 To cols.Length - 1
                    If row.HasVersion(DataRowVersion.Current) = True Then
                        If row(cols(i), DataRowVersion.Original).ToString <> row(cols(i), DataRowVersion.Current).ToString Then
                            'save it
                            saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_INSIGNIA", cols(i), "UPDATE", If(IsDBNull(row(cols(i), DataRowVersion.Original)), "", row(cols(i), DataRowVersion.Original)), If(IsDBNull(row(cols(i), DataRowVersion.Current)), "", row(cols(i), DataRowVersion.Current)), user_name)
                        End If
                    End If
                Next

                'MessageBox.Show(ds.Tables("MB_CONTACT_INSIGNIA").GetChanges.Rows.Count.ToString)

                'update it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                Dim query As String = "SELECT CONTACT_CODE, INSIGNIA_SEQ, INSIGNIA_DATE, INSIGNIA_LEVEL, INSIGNIA_REMARK FROM MB_CONTACT_INSIGNIA WHERE CONTACT_CODE = @p0"

                If ds.Tables("MB_CONTACT_INSIGNIA").GetChanges IsNot Nothing Then
                    'MessageBox.Show()
                    Try
                        updateWebSQL(query, parameters, ds.Tables("MB_CONTACT_INSIGNIA"))
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

    Private Sub btSOCIALadd_Click(sender As Object, e As EventArgs) Handles btSOCIALadd.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            bsRep.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btSOCIALadd.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_CONTACT").Rows.Find(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", row("CONTACT_CODE"))

                Dim query As String = String.Empty
                query &= "SELECT    TOP 1    SOCIAL_SEQ "
                query &= "FROM            MB_CONTACT_SOCIAL "
                query &= "WHERE        (CONTACT_CODE = @p0) ORDER BY SOCIAL_SEQ DESC"

                Dim objEDUCATION_SEQ As Object = Nothing
                Try
                    objEDUCATION_SEQ = client.ExecuteScalar(query, parameters, user_session)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar SOCIAL_SEQ")
                End Try

                If objEDUCATION_SEQ Is Nothing Then objEDUCATION_SEQ = 1

                If objEDUCATION_SEQ IsNot Nothing Then
                    If IsDBNull(objEDUCATION_SEQ) = True Then
                        objEDUCATION_SEQ = 0
                    End If

                    Dim EDUCATION_SEQ As Integer = CInt(objEDUCATION_SEQ) + 1

                    query = "INSERT INTO MB_CONTACT_SOCIAL (CONTACT_CODE, SOCIAL_SEQ) VALUES (@p0,@p1)"
                    parameters.Add("@p1", EDUCATION_SEQ)

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history 
                    saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_SOCIAL", "SOCIAL_SEQ", "ADD", 0, EDUCATION_SEQ, user_name)

                    'refresh grid
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show("บันทึกเสร็จสิ้น + " & EDUCATION_SEQ)
                Else
                    MessageBox.Show("ไม่พบ " & row("CONTACT_CODE").ToString)
                End If


            End If
        End If
    End Sub

    Private Sub btSOCIALdel_Click(sender As Object, e As EventArgs) Handles btSOCIALdel.Click
        If DataGridView8.CurrentRow IsNot Nothing Then
            DataGridView8.EndEdit()
            bsRep.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btSOCIALdel.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_CONTACT_SOCIAL").Rows.Find(DataGridView8.CurrentRow.Cells("SOCIAL_SEQ").Value)

                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", row("CONTACT_CODE"))
                parameters.Add("@p1", DataGridView8.CurrentRow.Cells("SOCIAL_SEQ").Value)

                Dim query As String = "DELETE FROM MB_CONTACT_SOCIAL WHERE (CONTACT_CODE = @p0) AND (SOCIAL_SEQ = @p1)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history 
                saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_SOCIAL", "SOCIAL_SEQ", "DELETE", DataGridView8.CurrentRow.Cells("SOCIAL_SEQ").Value, 0, user_name)

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btSOCIALsave_Click(sender As Object, e As EventArgs) Handles btSOCIALsave.Click

        If DataGridView8.CurrentRow IsNot Nothing Then
            DataGridView8.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btSOCIALsave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Dim row As DataRow = ds.Tables("MB_CONTACT_SOCIAL").Rows.Find(DataGridView8.CurrentRow.Cells("SOCIAL_SEQ").Value)

                Dim cols() As String = "SOCIAL_SEQ,SOCIAL_YEAR,SOCIAL_POSITION,SOCIAL_INSTITUTE,SOCIAL_WORK".Split(","c)
                For i As Integer = 0 To cols.Length - 1
                    If row.HasVersion(DataRowVersion.Current) = True Then
                        If row(cols(i), DataRowVersion.Original).ToString <> row(cols(i), DataRowVersion.Current).ToString Then
                            'save it
                            saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_SOCIAL", cols(i), "UPDATE", If(IsDBNull(row(cols(i), DataRowVersion.Original)), "", row(cols(i), DataRowVersion.Original)), If(IsDBNull(row(cols(i), DataRowVersion.Current)), "", row(cols(i), DataRowVersion.Current)), user_name)
                        End If
                    End If
                Next

                'update it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                Dim query As String = "SELECT CONTACT_CODE, SOCIAL_SEQ, SOCIAL_YEAR, SOCIAL_POSITION, SOCIAL_INSTITUTE, SOCIAL_WORK FROM MB_CONTACT_SOCIAL WHERE CONTACT_CODE = @p0 ORDER BY SOCIAL_SEQ "

                If ds.Tables("MB_CONTACT_SOCIAL").GetChanges IsNot Nothing Then
                    Try
                        updateWebSQL(query, parameters, ds.Tables("MB_CONTACT_SOCIAL"))
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

    Private Sub btAWORDadd_Click(sender As Object, e As EventArgs) Handles btAWORDadd.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            bsRep.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btAWORDadd.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_CONTACT").Rows.Find(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", row("CONTACT_CODE"))

                Dim query As String = String.Empty
                query &= "SELECT    TOP 1    AWORD_SEQ "
                query &= "FROM            MB_CONTACT_AWORD "
                query &= "WHERE        (CONTACT_CODE = @p0) ORDER BY AWORD_SEQ DESC"

                Dim objEDUCATION_SEQ As Object = Nothing
                Try
                    objEDUCATION_SEQ = client.ExecuteScalar(query, parameters, user_session)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar AWORD_SEQ")
                End Try

                If objEDUCATION_SEQ Is Nothing Then objEDUCATION_SEQ = 1

                If objEDUCATION_SEQ IsNot Nothing Then
                    If IsDBNull(objEDUCATION_SEQ) = True Then
                        objEDUCATION_SEQ = 0
                    End If

                    Dim EDUCATION_SEQ As Integer = CInt(objEDUCATION_SEQ) + 1

                    query = "INSERT INTO MB_CONTACT_AWORD (CONTACT_CODE, AWORD_SEQ) VALUES (@p0,@p1)"
                    parameters.Add("@p1", EDUCATION_SEQ)

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history 
                    saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_AWORD", "AWORD_SEQ", "ADD", 0, EDUCATION_SEQ, user_name)

                    'refresh grid
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show("บันทึกเสร็จสิ้น + " & EDUCATION_SEQ)
                Else
                    MessageBox.Show("ไม่พบ " & row("CONTACT_CODE").ToString)
                End If


            End If
        End If
    End Sub

    Private Sub btAWORDdel_Click(sender As Object, e As EventArgs) Handles btAWORDdel.Click
        If DataGridView9.CurrentRow IsNot Nothing Then
            DataGridView9.EndEdit()
            bsRep.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btSOCIALdel.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_CONTACT_AWORD").Rows.Find(DataGridView9.CurrentRow.Cells("AWORD_SEQ").Value)

                'get latest number
                Dim parameters As New Dictionary(Of String, Object)
                'Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", row("CONTACT_CODE"))
                parameters.Add("@p1", DataGridView9.CurrentRow.Cells("AWORD_SEQ").Value)

                Dim query As String = "DELETE FROM MB_CONTACT_AWORD WHERE (CONTACT_CODE = @p0) AND (AWORD_SEQ = @p1)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history 
                saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_AWORD", "AWORD_SEQ", "DELETE", DataGridView9.CurrentRow.Cells("AWORD_SEQ").Value, 0, user_name)

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btAWORDsave_Click(sender As Object, e As EventArgs) Handles btAWORDsave.Click

        If DataGridView9.CurrentRow IsNot Nothing Then
            DataGridView9.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btAWORDsave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Dim row As DataRow = ds.Tables("MB_CONTACT_AWORD").Rows.Find(DataGridView9.CurrentRow.Cells("AWORD_SEQ").Value)

                Dim cols() As String = "AWORD_SEQ,AWORD_YEAR,AWORD_DESC,AWORD_FROM".Split(","c)
                For i As Integer = 0 To cols.Length - 1
                    If row.HasVersion(DataRowVersion.Current) = True Then
                        If row(cols(i), DataRowVersion.Original).ToString <> row(cols(i), DataRowVersion.Current).ToString Then
                            'save it
                            saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT_AWORD", cols(i), "UPDATE", If(IsDBNull(row(cols(i), DataRowVersion.Original)), "", row(cols(i), DataRowVersion.Original)), If(IsDBNull(row(cols(i), DataRowVersion.Current)), "", row(cols(i), DataRowVersion.Current)), user_name)
                        End If
                    End If
                Next

                'update it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                Dim query As String = "SELECT CONTACT_CODE, AWORD_SEQ, AWORD_YEAR, AWORD_DESC, AWORD_FROM FROM MB_CONTACT_AWORD WHERE CONTACT_CODE = @p0 ORDER BY AWORD_SEQ "

                If ds.Tables("MB_CONTACT_AWORD").GetChanges IsNot Nothing Then
                    Try
                        updateWebSQL(query, parameters, ds.Tables("MB_CONTACT_AWORD"))
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

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    Private Shared Sub Date_Format(sender As Object, e As ConvertEventArgs) Handles BindingBIRTH_DATE.Format
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

    Private Shared Sub Date_Parse(sender As Object, e As ConvertEventArgs) Handles BindingBIRTH_DATE.Parse
        If e.Value.ToString() = "  /  /" Then
            e.Value = Nothing
        End If
    End Sub

    Private Sub btSPOUSE_Click(sender As Object, e As EventArgs) Handles btSPOUSE.Click
        Dim f As New frmMainContacts
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            If MessageBox.Show("คุณต้องการที่จะเปลี่ยนคู่สมรสเป็น " & DataGridView1.CurrentRow.Cells("CONTACT_FIRST_NAME_TH").Value.ToString & " " & DataGridView1.CurrentRow.Cells("CONTACT_LAST_NAME_TH").Value.ToString & "?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim parameters As New Dictionary(Of String, Object)
                Dim query As String = "UPDATE MB_CONTACT SET SPOUSE_CODE = @p0 WHERE (CONTACT_CODE = @p1)"

                parameters.Clear()
                parameters.Add("@p0", f.DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)
                parameters.Add("@p1", tbCONTACT_CODE.Text)

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                '================================================== update each other

                parameters.Clear()
                parameters.Add("@p0", tbCONTACT_CODE.Text)
                parameters.Add("@p1", f.DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show(btSPOUSE.Text & "เสร็จสิ้น")
            End If
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btLocationsByWork_Click(sender As Object, e As EventArgs) Handles btLocationsByWork.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            Dim f As New frmMainLocations
            f.TextBox1.Text = TextBox22.Text
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                'th en
                TextBox34.Text = f.DataGridView1.CurrentRow.Cells("SUB_DISTRICT_NAME_TH").Value.ToString
                TextBox25.Text = f.DataGridView1.CurrentRow.Cells("DISTRICT_NAME_TH").Value.ToString
                TextBox21.Text = f.DataGridView1.CurrentRow.Cells("PROVINCE_NAME_TH").Value.ToString

                TextBox41.Text = f.DataGridView1.CurrentRow.Cells("SUB_DISTRICT_NAME_EN").Value.ToString
                TextBox38.Text = f.DataGridView1.CurrentRow.Cells("DISTRICT_NAME_EN").Value.ToString
                TextBox23.Text = f.DataGridView1.CurrentRow.Cells("PROVINCE_NAME_EN").Value.ToString

                TextBox22.Text = f.DataGridView1.CurrentRow.Cells("POSTCODE").Value.ToString
            End If
            f.Dispose()
            f = Nothing
        End If
    End Sub
End Class
