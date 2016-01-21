Imports System.IO
'Imports ChunkTransporter
Imports System.ServiceModel
Imports Microsoft.Reporting.WinForms
'Imports Microsoft.VisualBasic

Public Class frmINI

    'Dim dt As DataTable
    'Dim dv As DataView
    Dim ds As DataSet
    Dim bsMain As BindingSource
    Dim bsAddress As BindingSource
    Dim bsRep As BindingSource
    Dim dvMB_MEMBER_MAIN_GROUP As DataView
    Dim dvMEMBER_GROUP As DataView
    Dim dvMEMBER_MAIN_TYPE As DataView
    Dim dvMEMBER_TYPE As DataView
    Dim dvLOGS As DataView

    Friend INI_CODE As String
    Friend INI_NAME As String
    Friend INI_ID As Integer

    Private Sub frmFTI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        ds = New DataSet

        DateTimePicker2.Value = DateTimePicker1.Value.AddMonths(-1)

        Me.Text = INI_NAME
        btNew.Text = "สมัครสมาชิก " & INI_NAME
        btSQL.Text = "สอบถาม " & INI_NAME

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", "0")

        Dim query As String = String.Empty
        query &= "SELECT   TOP 1     MB_MEMBER.*, MB_COMP_PERSON.*, MB_MEMBER_MAIN_GROUP.*, MB_MEMBER_GROUP.* "
        'query &= "SELECT   TOP 1     MB_MEMBER.MEMBER_CODE "
        query &= "FROM            MB_MEMBER INNER JOIN "
        query &= "                         MB_COMP_PERSON ON MB_MEMBER.OU_CODE = MB_COMP_PERSON.OU_CODE AND MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
        query &= "                         MB_MEMBER_MAIN_GROUP ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE INNER JOIN "
        query &= "                         MB_MEMBER_GROUP ON MB_MEMBER.MEMBER_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_GROUP_CODE AND "
        query &= "                         MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_MAIN_GROUP_CODE "
        query &= "WHERE        (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN (@p0)) AND (MB_MEMBER.RETIRE_DATE IS NOT NULL) "

        'query &= "ORDER BY MB_MEMBER.MEMBER_CODE, MB_MEMBER.MEMBER_MAIN_GROUP_CODE, MB_MEMBER.MEMBER_GROUP_CODE "

        'Dim bt As String = client.Fill(query, parameters)
        'bt = UnZip(bt)

        'MessageBox.Show(bt)

        Dim dt As DataTable = New DataTable
        'dt.ReadXml(New StringReader(bt))

        'Dim obj As Object = client.Fill(query, parameters)
        'Dim tm As TableManifest = CType(obj, TableManifest)
        'dt = TableManifest.FetchTable(tm)
        'TabControl1.tabp

        Try
            dt = fillWebSQL(query, parameters, "MB_MEMBER")
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
        End Try

        'MessageBox.Show(dt.Rows.Count.ToString)

        If ds.Tables.Contains("MB_MEMBER") = True Then
            ds.Tables("MB_MEMBER").Clear()
            ds.Tables("MB_MEMBER").Merge(dt)
            ds.Tables("MB_MEMBER").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        '=====================================

        bsMain = New BindingSource(ds, "MB_MEMBER")

        '=====================================

        DataGridView1.DataSource = bsMain

        'ds.Tables("MB_MEMBER").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER").Columns("REGIST_CODE")}
        ds.Tables("MB_MEMBER").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER").Columns("ROWID")}

        Dim l As List(Of String) = New List(Of String)

        For i As Integer = 0 To DataGridView1.ColumnCount - 1
            DataGridView1.Columns(i).Visible = False

            If Microsoft.VisualBasic.Left(DataGridView1.Columns(i).Name, 4).ToLower <> "expr" Then
                l.Add(DataGridView1.Columns(i).Name)
            End If
        Next

        ' Sort alphabetically.
        l.Sort()

        'predefine fields
        Dim FTI_MAIN_SEARCH_PRE As String = getParameters(3, "INI_MAIN_SEARCH_PRE")

        'search as thai text
        Dim FTI_MAIN_SEARCH As String = getParameters(3, "INI_MAIN_SEARCH")
        Dim colFTI_MAIN_SEARCH() As String = FTI_MAIN_SEARCH.Split(","c)

        Dim dtSearch As New DataTable("SEARCHS")
        dtSearch.Columns.Add("ColumnNameENG", GetType(String))
        dtSearch.Columns.Add("ColumnNameTHAI", GetType(String))
        ds.Tables.Add(dtSearch)
        ds.Tables("SEARCHS").PrimaryKey = New DataColumn() {ds.Tables("SEARCHS").Columns("ColumnNameENG")}

        For idx As Integer = 0 To l.Count - 1
            'Console.WriteLine(element)
            'ComboBox1.Items.Add(l(idx))
            Dim r As DataRow = ds.Tables("SEARCHS").NewRow
            r("ColumnNameENG") = l(idx)
            r("ColumnNameTHAI") = l(idx)

            For col As Integer = 0 To colFTI_MAIN_SEARCH.Count - 1
                Dim co() As String = colFTI_MAIN_SEARCH(col).Split("|"c)

                If co(0).ToLower = l(idx).ToLower Then
                    r("ColumnNameTHAI") = co(1)
                    Exit For
                End If
            Next

            ds.Tables("SEARCHS").Rows.Add(r)
        Next

        'sort again with thai
        Dim dvSEARCHS As New DataView(ds.Tables("SEARCHS"))
        dvSEARCHS.Sort = "ColumnNameTHAI"
        Dim dtSEARCHS As DataTable = dvSEARCHS.ToTable
        ds.Tables("SEARCHS").Clear()

        Dim rFTI_MAIN_SEARCH_PRE As DataRow = ds.Tables("SEARCHS").NewRow
        rFTI_MAIN_SEARCH_PRE("ColumnNameENG") = FTI_MAIN_SEARCH_PRE
        rFTI_MAIN_SEARCH_PRE("ColumnNameTHAI") = "Predefine fields"
        ds.Tables("SEARCHS").Rows.Add(rFTI_MAIN_SEARCH_PRE)

        For idx As Integer = 0 To dtSEARCHS.Rows.Count - 1
            ds.Tables("SEARCHS").ImportRow(dtSEARCHS.Rows(idx))
        Next

        'add it into combobox
        ComboBox1.DataSource = ds.Tables("SEARCHS")
        ComboBox1.DisplayMember = "ColumnNameTHAI"
        ComboBox1.ValueMember = "ColumnNameENG"

        'ComboBox1.SelectedIndex = 0

        DataGridView1.Columns("MEMBER_CODE").Visible = True
        DataGridView1.Columns("MEMBER_STATUS_CODE").Visible = True
        DataGridView1.Columns("COMP_PERSON_NAME_TH").Visible = True
        DataGridView1.Columns("COMP_PERSON_NAME_EN").Visible = True
        DataGridView1.Columns("MEMBER_MAIN_GROUP_NAME").Visible = True
        DataGridView1.Columns("MEMBER_GROUP_NAME").Visible = True
        'DataGridView1.Columns("MEMBER_MAIN_TYPE_NAME").Visible = True
        'DataGridView1.Columns("MEMBER_TYPE_NAME").Visible = True

        DataGridView1.Columns("MEMBER_CODE").Width = 60
        DataGridView1.Columns("MEMBER_STATUS_CODE").Width = 50
        DataGridView1.Columns("COMP_PERSON_NAME_TH").Width = 200
        DataGridView1.Columns("COMP_PERSON_NAME_EN").Width = 200
        DataGridView1.Columns("MEMBER_MAIN_GROUP_NAME").Width = 150
        DataGridView1.Columns("MEMBER_GROUP_NAME").Width = 150
        'DataGridView1.Columns("MEMBER_MAIN_TYPE_NAME").Width = 150
        'DataGridView1.Columns("MEMBER_TYPE_NAME").Width = 150

        ToolStripStatusLabel2.Text = dt.Rows.Count.ToString("#,##0")

        'column as thai text
        For idx As Integer = 0 To DataGridView1.Columns.Count - 1
            For col As Integer = 0 To colFTI_MAIN_SEARCH.Count - 1
                Dim co() As String = colFTI_MAIN_SEARCH(col).Split("|"c)

                If co(0).ToLower = DataGridView1.Columns(idx).HeaderText.ToLower Then
                    DataGridView1.Columns(idx).HeaderText = co(1)
                    DataGridView1.Columns(idx).Width = CInt(co(2))
                    Exit For
                End If
            Next
        Next

        '=================================== files
        'Dim dtFils As New DataTable("FILES")
        'dtFils.Columns.Add("DOC_TYPE", GetType(Integer))
        'dtFils.Columns.Add("docName", GetType(String))
        'dtFils.Columns.Add("docPath", GetType(String))
        'ds.Tables.Add(dtFils)

        'parameters.Clear()
        ''Dim parameters As New Dictionary(Of String, Object)

        'query = String.Empty
        'query &= "SELECT * FROM MB_MEMBER_FILES_TYPE ORDER BY DOC_TYPE_NAME"

        'Dim dtMB_MEMBER_FILES_TYPE As DataTable = New DataTable
        'Try
        '    dtMB_MEMBER_FILES_TYPE = fillWebSQL(query, parameters, "MB_MEMBER_FILES_TYPE")
        'Catch ex As Exception
        '    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
        'End Try

        'If ds.Tables.Contains("MB_MEMBER_FILES_TYPE") = True Then
        '    ds.Tables("MB_MEMBER_FILES_TYPE").Clear()
        '    ds.Tables("MB_MEMBER_FILES_TYPE").Merge(dtMB_MEMBER_FILES_TYPE)
        'Else
        '    ds.Tables.Add(dtMB_MEMBER_FILES_TYPE)
        'End If

        'DataGridView6.DataSource = ds.Tables("FILES")

        'Dim comboBoxColumn As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
        'comboBoxColumn.HeaderText = "DOC_TYPE"
        'comboBoxColumn.DataPropertyName = "DOC_TYPE"
        'comboBoxColumn.DataSource = ds.Tables("MB_MEMBER_FILES_TYPE")
        'comboBoxColumn.ValueMember = ds.Tables("MB_MEMBER_FILES_TYPE").Columns(0).ColumnName
        'comboBoxColumn.DisplayMember = ds.Tables("MB_MEMBER_FILES_TYPE").Columns(1).ColumnName

        'DataGridView6.Columns.RemoveAt(0)
        'DataGridView6.Columns.Insert(0, comboBoxColumn)
        '=================================== end files

        Dim ERR_STEP As String = String.Empty
        Try
            ERR_STEP = "getMEMBER_MAIN_GROUP"
            getMEMBER_MAIN_GROUP()
            ERR_STEP = "getMEMBER_GROUP"
            getMEMBER_GROUP()
            ERR_STEP = "getMEMBER_MAIN_TYPE"
            getMEMBER_MAIN_TYPE()
            ERR_STEP = "getMEMBER_TYPE"
            getMEMBER_TYPE()
            ERR_STEP = "getMB_MEMBER_ADVISOR"
            getMB_MEMBER_ADVISOR()
            ERR_STEP = "getMB_ADDRESS_TYPE"
            getMB_ADDRESS_TYPE()
            ERR_STEP = "getMB_MEMBER_FILES_TYPE"
            getMB_MEMBER_FILES_TYPE()
            ERR_STEP = "getMB_PRENAME"
            getMB_PRENAME()
            ERR_STEP = "getMB_MEMBER_STATUS"
            getMB_MEMBER_STATUS()
            ERR_STEP = "getMB_BUSSINESS_TYPE"
            getMB_BUSSINESS_TYPE()
            ERR_STEP = "getMB_MAIN_INDUSTRY"
            getMB_MAIN_INDUSTRY()
            ERR_STEP = "getMB_PRODUCT"
            getMB_PRODUCT()
            ERR_STEP = "getMB_ISO"
            getMB_ISO()
        Catch ex As Exception
            MessageBox.Show(ex.Message, ERR_STEP)
        End Try


        'history log
        Dim dtLog As New DataTable("LOGS")
        dtLog.Columns.Add("ColumnName", GetType(String))
        dtLog.Columns.Add("ValueFrom", GetType(String))
        dtLog.Columns.Add("ValueTo", GetType(String))
        ds.Tables.Add(dtLog)
        ds.Tables("LOGS").PrimaryKey = New DataColumn() {ds.Tables("LOGS").Columns("ColumnName")}

        'blinding
        tbMEMBER_CODE.DataBindings.Add("Text", bsMain, "MEMBER_CODE")
        tbREGIST_CODE.DataBindings.Add("Text", bsMain, "REGIST_CODE")
        tbCOMP_PERSON_CODE.DataBindings.Add("Text", bsMain, "COMP_PERSON_CODE")
        ComboBox13.DataBindings.Add("SelectedValue", bsMain, "MEMBER_STATUS_CODE")
        TextBox2.DataBindings.Add("Text", bsMain, "COMP_PERSON_NAME_TH")
        TextBox4.DataBindings.Add("Text", bsMain, "COMP_PERSON_NAME_EN")

        tbMEMBER_MAIN_GROUP.DataBindings.Add("Text", bsMain, "MEMBER_MAIN_GROUP_CODE")
        tbMEMBER_GROUP.DataBindings.Add("Text", bsMain, "MEMBER_GROUP_CODE")
        tbMEMBER_MAIN_TYPE.DataBindings.Add("Text", bsMain, "MEMBER_MAIN_TYPE_CODE")
        tbMEMBER_TYPE.DataBindings.Add("Text", bsMain, "MEMBER_TYPE_CODE")

        ComboBox3.DataBindings.Add("SelectedValue", bsMain, "MEMBER_MAIN_GROUP_CODE")
        ComboBox4.DataBindings.Add("SelectedValue", bsMain, "MEMBER_GROUP_CODE")
        ComboBox14.DataBindings.Add("SelectedValue", bsMain, "MEMBER_MAIN_TYPE_CODE")
        ComboBox15.DataBindings.Add("SelectedValue", bsMain, "MEMBER_TYPE_CODE")

        NumericUpDown1.DataBindings.Add(New Binding("Value", bsMain, "REGIST_CAPITAL", True, DataSourceUpdateMode.OnValidation))
        NumericUpDown2.DataBindings.Add(New Binding("Value", bsMain, "VENTURE_CAP_THA", True, DataSourceUpdateMode.OnValidation))
        NumericUpDown3.DataBindings.Add(New Binding("Value", bsMain, "VENTURE_CAP_ENG", True, DataSourceUpdateMode.OnValidation))
        NumericUpDown4.DataBindings.Add(New Binding("Value", bsMain, "ASSET_AMOUNT", True, DataSourceUpdateMode.OnValidation))
        NumericUpDown5.DataBindings.Add(New Binding("Value", bsMain, "EMPLOYEE_AMOUNT", True, DataSourceUpdateMode.OnValidation))

        CheckBox2.DataBindings.Add(New Binding("Checked", bsMain, "BUS_TYPE_RELATE", True, DataSourceUpdateMode.OnValidation))
        CheckBox3.DataBindings.Add(New Binding("Checked", bsMain, "BUS_TYPE_DEALER", True, DataSourceUpdateMode.OnValidation))
        CheckBox4.DataBindings.Add(New Binding("Checked", bsMain, "BUS_TYPE_IMPORTER", True, DataSourceUpdateMode.OnValidation))
        CheckBox5.DataBindings.Add(New Binding("Checked", bsMain, "BUS_TYPE_EXPORTER", True, DataSourceUpdateMode.OnValidation))
        TextBox71.DataBindings.Add(New Binding("Text", bsMain, "BUS_TYPE_OTHER", True, DataSourceUpdateMode.OnValidation))

        'TextBox72.DataBindings.Add(New Binding("Text", bsMain, "MAIN_PRODUCTS_SERVICES", True, DataSourceUpdateMode.OnValidation))
        TextBox72.DataBindings.Add(New Binding("Text", bsMain, "PRODUCE_TECHNOLOGY_DESC", True, DataSourceUpdateMode.OnValidation))

        NumericUpDown6.DataBindings.Add(New Binding("Value", bsMain, "PRODUCE_AMOUNT", True, DataSourceUpdateMode.OnValidation))
        NumericUpDown7.DataBindings.Add(New Binding("Value", bsMain, "SALE_DOMESTIC_PERCENT", True, DataSourceUpdateMode.OnValidation))
        NumericUpDown8.DataBindings.Add(New Binding("Value", bsMain, "SALE_INTERNATIONAL_PERCENT", True, DataSourceUpdateMode.OnValidation))

        chkADVISOR.DataBindings.Add(New Binding("Checked", bsMain, "ADVISOR_CHECKED", True, DataSourceUpdateMode.OnValidation))
        cbADVISOR.DataBindings.Add(New Binding("SelectedValue", bsMain, "ADVISOR_CODE", True, DataSourceUpdateMode.OnValidation))
        tbADVISOR.DataBindings.Add(New Binding("Text", bsMain, "ADVISOR_CODE", True, DataSourceUpdateMode.OnValidation))

        'TextBox3.DataBindings.Add(New Binding("Text", bsMain, "TAX_ID", True, DataSourceUpdateMode.OnValidation))
        mtbTaxID.DataBindings.Add(New Binding("Text", bsMain, "TAX_ID", True, DataSourceUpdateMode.OnValidation))

        NumericUpDown11.DataBindings.Add(New Binding("Value", bsMain, "INCOME_PER_YEAR", True, DataSourceUpdateMode.OnValidation))

        NumericUpDown12.DataBindings.Add(New Binding("Value", bsMain, "ELECTRIC_AMOUNT", True, DataSourceUpdateMode.OnValidation))

        ComboBox5.DataBindings.Add(New Binding("SelectedValue", bsMain, "PREN_CODE", True, DataSourceUpdateMode.OnValidation))
        tbPREN_CODE.DataBindings.Add("Text", bsMain, "PREN_CODE")

        tbMEMBER_STATUS_CODE.DataBindings.Add("Text", bsMain, "MEMBER_STATUS_CODE")

        CheckBox6.DataBindings.Add(New Binding("Checked", bsMain, "PRODUCE_TECHNOLOGY_TH", True, DataSourceUpdateMode.OnValidation))
        CheckBox7.DataBindings.Add(New Binding("Checked", bsMain, "PRODUCE_TECHNOLOGY_EN", True, DataSourceUpdateMode.OnValidation))

        'NumericUpDown1.DataBindings.Add("Value", bsMain, "REGIST_CAPITAL")
        'NumericUpDown2.DataBindings.Add("Value", bsMain, "VENTURE_CAP_THA")
        'NumericUpDown3.DataBindings.Add("Value", bsMain, "VENTURE_CAP_ENG")
        'NumericUpDown4.DataBindings.Add("Value", bsMain, "ASSET_AMOUNT")
        'NumericUpDown5.DataBindings.Add("Value", bsMain, "EMPLOYEE_AMOUNT")

        'CheckBox2.DataBindings.Add("Checked", bsMain, "BUS_TYPE_RELATE")
        'CheckBox3.DataBindings.Add("Checked", bsMain, "BUS_TYPE_DEALER")
        'CheckBox4.DataBindings.Add("Checked", bsMain, "BUS_TYPE_IMPORTER")
        'CheckBox5.DataBindings.Add("Checked", bsMain, "BUS_TYPE_EXPORTER")
        'TextBox71.DataBindings.Add("Text", bsMain, "BUS_TYPE_OTHER")

        'TextBox72.DataBindings.Add("Text", bs, "MAIN_PRODUCTS_SERVICES")

        'NumericUpDown6.DataBindings.Add("Value", bs, "PRODUCE_AMOUNT")
        'NumericUpDown7.DataBindings.Add("Value", bs, "SALE_DOMESTIC_PERCENT")
        'NumericUpDown8.DataBindings.Add("Value", bs, "SALE_INTERNATIONAL_PERCENT")

        'CheckBox1.DataBindings.Add("Checked", bs, "ADVISOR_CHECKED")
        'ComboBox2.DataBindings.Add("SelectedValue", bs, "ADVISOR_CODE")

        AddHandler DataGridView1.SelectionChanged, AddressOf gridControl1_CurrentRowChanged
        'AddHandler ds.Tables("MB_MEMBER").ColumnChanged, AddressOf dtColumn_Changed

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub frmFTI_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fINI(INI_ID) = Nothing
    End Sub

    Private Sub dtColumn_Changed(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
        'Console.WriteLine("Changed Column[" & e.Column.ColumnName & "] from[" & e.Row(e.Column.ColumnName, DataRowVersion.Original).ToString & "] to[" & e.Row(e.Column.ColumnName, DataRowVersion.Current).ToString & "]")

        'dtLog.Columns.Add("ColumnName", GetType(String))
        'dtLog.Columns.Add("ValueFrom", GetType(String))
        'dtLog.Columns.Add("ValueTo", GetType(String))

        'check exist
        Dim row As DataRow = ds.Tables("LOGS").Rows.Find(e.Column.ColumnName)
        If row IsNot Nothing Then
            'update it
            'row.Item("ValueFrom") = e.Row(e.Column.ColumnName, DataRowVersion.Original).ToString
            row.Item("ValueTo") = e.Row(e.Column.ColumnName, DataRowVersion.Current).ToString
        Else
            'add it
            row = ds.Tables("LOGS").NewRow
            row.Item("ValueFrom") = e.Row(e.Column.ColumnName, DataRowVersion.Original).ToString
            row.Item("ValueTo") = e.Row(e.Column.ColumnName, DataRowVersion.Current).ToString
            ds.Tables("LOGS").Rows.Add(row)
        End If

    End Sub

    Private Sub getMEMBER(Optional ByVal REGIST_CODE As String = "")
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", INI_CODE)


        Dim query As String = String.Empty
        query &= "SELECT    TOP 1000    MB_MEMBER.*, MB_COMP_PERSON.*, MB_MEMBER_MAIN_GROUP.*, MB_MEMBER_GROUP.* "
        'query &= "FROM            MB_MEMBER INNER JOIN "
        'query &= "                         MB_COMP_PERSON ON MB_MEMBER.OU_CODE = MB_COMP_PERSON.OU_CODE AND MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
        'query &= "                         MB_MEMBER_MAIN_GROUP ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE INNER JOIN "
        'query &= "                         MB_MEMBER_GROUP ON MB_MEMBER.MEMBER_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_GROUP_CODE AND "
        'query &= "                         MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_MAIN_GROUP_CODE "
        query &= "FROM            MB_MEMBER_MAIN_GROUP LEFT OUTER JOIN "
        query &= "                         MB_MEMBER_GROUP ON "
        query &= "                         MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_MAIN_GROUP_CODE RIGHT OUTER JOIN "
        query &= "                         MB_MEMBER LEFT OUTER JOIN "
        query &= "                         MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE ON "
        query &= "                         MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE = MB_MEMBER.MEMBER_MAIN_GROUP_CODE AND "
        query &= "                         MB_MEMBER_GROUP.MEMBER_GROUP_CODE = MB_MEMBER.MEMBER_GROUP_CODE "
        query &= "WHERE        (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN (@p0)) AND (LEN(MB_MEMBER.REGIST_CODE) > 0) "

        'dv = New DataView(dt)
        Dim searchValue As String = TextBox1.Text.Trim.Replace(" ", "%")

        If REGIST_CODE.Length = 0 Then
            If ComboBox1.SelectedIndex = 0 Then
                'predefine fields
                'query &= String.Format(" AND (MEMBER_CODE LIKE '%{0}%' OR COMP_PERSON_NAME_TH LIKE '%{0}%' OR COMP_PERSON_NAME_EN LIKE '%{0}%')", searchValue)
                query &= String.Format(ComboBox1.SelectedValue.ToString, searchValue)
            Else
                query &= String.Format(" AND (" & ComboBox1.SelectedValue.ToString.Trim & " LIKE '%{0}%')", searchValue)
            End If
        Else
            'REGIST_CODE
            query &= String.Format(" AND (MB_MEMBER.REGIST_CODE = '{0}')", REGIST_CODE)
        End If

        query &= " ORDER BY MB_MEMBER.PREFIX DESC, MB_MEMBER.RUNNING"
        'query &= "ORDER BY MB_MEMBER.MEMBER_CODE, MB_MEMBER.MEMBER_MAIN_GROUP_CODE, MB_MEMBER.MEMBER_GROUP_CODE "

        'Dim bt As String = client.Fill(query, parameters)
        'bt = UnZip(bt)

        Dim dt As DataTable = New DataTable
        'dt.ReadXml(New StringReader(bt))

        dt = fillWebSQL(query, parameters, "MB_MEMBER")

        'Dim obj As Object = client.Fill(query, parameters)
        'Dim tm As TableManifest = CType(obj, TableManifest)
        'dt = TableManifest.FetchTable(tm)

        'If ds.Tables.Contains("MB_MEMBER") = True Then ds.Tables.Remove("MB_MEMBER")
        'ds.Tables.Add(dt)

        'ds.Tables("MB_MEMBER").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER").Columns("MEMBER_CODE")}


        'DataGridView1.DataSource = ds.Tables("MB_MEMBER")
        'ds.Tables("MB_MEMBER").Clear()
        'ds.Tables("MB_MEMBER").BeginLoadData()
        'ds.Tables("MB_MEMBER").AcceptChanges()
        ''this._TimeSheets.Tables["times"].LoadDataRow(newRow, true);

        'ds.Tables("MB_MEMBER").Merge(dt)
        'ds.Tables("MB_MEMBER").EndLoadData()

        If ds.Tables.Contains("MB_MEMBER") = True Then
            ds.Tables("MB_MEMBER").Clear()
            ds.Tables("MB_MEMBER").Merge(dt)
            ds.Tables("MB_MEMBER").AcceptChanges()
        Else
            ds.Tables.Add(dt)
            ds.Tables("MB_MEMBER").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER").Columns("ROWID")}
        End If

        'If isHide = True Then
        '    For i As Integer = 0 To DataGridView1.ColumnCount - 1
        '        DataGridView1.Columns(i).Visible = False

        '        If Microsoft.VisualBasic.Left(DataGridView1.Columns(i).Name, 4).ToLower <> "expr" Then
        '            ComboBox1.Items.Add(DataGridView1.Columns(i).Name)
        '        End If

        '    Next

        '    DataGridView1.Columns("MEMBER_CODE").Visible = True
        '    DataGridView1.Columns("MEMBER_STATUS_CODE").Visible = True
        '    DataGridView1.Columns("COMP_PERSON_NAME_TH").Visible = True
        '    DataGridView1.Columns("COMP_PERSON_NAME_EN").Visible = True
        '    DataGridView1.Columns("MEMBER_MAIN_GROUP_NAME").Visible = True
        '    DataGridView1.Columns("MEMBER_GROUP_NAME").Visible = True

        '    DataGridView1.Columns("MEMBER_CODE").Width = 60
        '    DataGridView1.Columns("MEMBER_STATUS_CODE").Width = 50
        '    DataGridView1.Columns("COMP_PERSON_NAME_TH").Width = 200
        '    DataGridView1.Columns("COMP_PERSON_NAME_EN").Width = 200
        '    DataGridView1.Columns("MEMBER_MAIN_GROUP_NAME").Width = 150
        '    DataGridView1.Columns("MEMBER_GROUP_NAME").Width = 150
        'End If

        ToolStripStatusLabel2.Text = dt.Rows.Count.ToString("#,##0")
    End Sub

    Private Sub getMEMBER_MAIN_GROUP()
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", INI_CODE)

        Dim query As String = String.Empty
        query &= "SELECT [MEMBER_MAIN_GROUP_CODE], [MEMBER_MAIN_GROUP_NAME], [INACTIVE] FROM [MB_MEMBER_MAIN_GROUP] "
        'query &= "WHERE ([INACTIVE] <> 'N') "
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
        End If

        If dvMB_MEMBER_MAIN_GROUP Is Nothing Then
            dvMB_MEMBER_MAIN_GROUP = New DataView(ds.Tables("MB_MEMBER_MAIN_GROUP"))
            ComboBox3.DataSource = dvMB_MEMBER_MAIN_GROUP
            ComboBox3.DisplayMember = "MEMBER_MAIN_GROUP_NAME"
            ComboBox3.ValueMember = "MEMBER_MAIN_GROUP_CODE"

            ComboBox3.SelectedIndex = -1
        End If


    End Sub

    Private Sub getMEMBER_GROUP()
        'ByVal MEMBER_MAIN_GROUP_CODE As String
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", INI_CODE)

        Dim query As String = String.Empty
        '[MEMBER_GROUP_CODE], [MEMBER_GROUP_NAME], [MEMBER_GROUP_NAME_EN], [INACTIVE]
        query &= "SELECT * FROM [MB_MEMBER_GROUP] "
        'query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) "
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
        End If

        If dvMEMBER_GROUP Is Nothing Then
            dvMEMBER_GROUP = New DataView(ds.Tables("MB_MEMBER_GROUP"))
            ComboBox4.DataSource = dvMEMBER_GROUP
            ComboBox4.DisplayMember = "MEMBER_GROUP_NAME"
            ComboBox4.ValueMember = "MEMBER_GROUP_CODE"

            ComboBox4.SelectedIndex = -1
        End If


    End Sub

    Private Sub getMEMBER_MAIN_TYPE()
        'ByVal MEMBER_MAIN_GROUP_CODE As String, ByVal MEMBER_GROUP_CODE As String
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", INI_CODE)

        Dim query As String = String.Empty
        '[MEMBER_MAIN_TYPE_CODE], [MEMBER_MAIN_TYPE_NAME], [INACTIVE]
        query &= "SELECT * FROM [MB_MEMBER_MAIN_TYPE] "
        'query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) "
        query &= "WHERE (MEMBER_MAIN_GROUP_CODE IN (@p0)) AND (INACTIVE <> 1) "
        query &= "ORDER BY MEMBER_MAIN_TYPE_CODE "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_MAIN_TYPE").Copy
        'dt.TableName = "MB_MEMBER_MAIN_TYPE"
        If ds.Tables.Contains("MB_MEMBER_MAIN_TYPE") = True Then
            ds.Tables("MB_MEMBER_MAIN_TYPE").Clear()
            ds.Tables("MB_MEMBER_MAIN_TYPE").Merge(dt)
            ds.Tables("MB_MEMBER_MAIN_TYPE").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        If dvMEMBER_MAIN_TYPE Is Nothing Then
            dvMEMBER_MAIN_TYPE = New DataView(ds.Tables("MB_MEMBER_MAIN_TYPE"))
            ComboBox14.DataSource = dvMEMBER_MAIN_TYPE
            ComboBox14.DisplayMember = "MEMBER_MAIN_TYPE_NAME"
            ComboBox14.ValueMember = "MEMBER_MAIN_TYPE_CODE"

            ComboBox14.SelectedIndex = -1
        End If


    End Sub

    Private Sub getMEMBER_TYPE()
        'ByVal MEMBER_MAIN_GROUP_CODE As String, ByVal MEMBER_GROUP_CODE As String, ByVal MEMBER_MAIN_TYPE_CODE As String
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", INI_CODE)


        Dim query As String = String.Empty
        '[MEMBER_TYPE_CODE], [MEMBER_TYPE_NAME], [INACTIVE]
        query &= "SELECT * FROM [MB_MEMBER_TYPE] "
        'query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) "
        query &= "WHERE (MEMBER_MAIN_GROUP_CODE IN (@p0)) AND (INACTIVE <> 1) "
        query &= "ORDER BY [MEMBER_TYPE_CODE] "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_TYPE").Copy
        'dt.TableName = "MB_MEMBER_TYPE"
        If ds.Tables.Contains("MB_MEMBER_TYPE") = True Then
            ds.Tables("MB_MEMBER_TYPE").Clear()
            ds.Tables("MB_MEMBER_TYPE").Merge(dt)
            ds.Tables("MB_MEMBER_TYPE").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        If dvMEMBER_TYPE Is Nothing Then
            dvMEMBER_TYPE = New DataView(ds.Tables("MB_MEMBER_TYPE"))
            ComboBox15.DataSource = dvMEMBER_TYPE
            ComboBox15.DisplayMember = "MEMBER_TYPE_NAME"
            ComboBox15.ValueMember = "MEMBER_TYPE_CODE"

            ComboBox15.SelectedIndex = -1
        End If

    End Sub

    Private Sub getMB_MEMBER_ADVISOR()
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", "200")

        Dim sFTI_MB_MEMBER_ADVISOR As String = String.Empty  '"SELECT MEMBER_MAIN_GROUP_CODE, MEMBER_GROUP_CODE, MEMBER_GROUP_NAME, MEMBER_GROUP_SHORT_NAME, GROUP_LOCATION, MEMBER_GROUP_NAME_EN, MEMBER_GROUP_RDLC_GROUP FROM MB_MEMBER_GROUP, REPLACE(MEMBER_GROUP_NAME,'จังหวัด','') AS MEMBER_GROUP_NAME2 WHERE " & getParameters(1, "FTI_MB_MEMBER_ADVISOR") & " ORDER BY MEMBER_GROUP_NAME"

        sFTI_MB_MEMBER_ADVISOR &= "SELECT        MEMBER_MAIN_GROUP_CODE, MEMBER_GROUP_CODE, MEMBER_GROUP_NAME, MEMBER_GROUP_SHORT_NAME, GROUP_LOCATION, MEMBER_GROUP_NAME_EN, MEMBER_GROUP_RDLC_GROUP, "
        sFTI_MB_MEMBER_ADVISOR &= "                         REPLACE(MEMBER_GROUP_NAME, 'จังหวัด', '') AS MEMBER_GROUP_NAME2 "
        sFTI_MB_MEMBER_ADVISOR &= "FROM            MB_MEMBER_GROUP "
        sFTI_MB_MEMBER_ADVISOR &= "WHERE " & getParameters(3, "INI_MB_MEMBER_ADVISOR") & " ORDER BY MEMBER_GROUP_NAME"

        Dim dt As DataTable = fillWebSQL(sFTI_MB_MEMBER_ADVISOR, parameters, "MB_MEMBER_ADVISOR").Copy
        'dt.TableName = "MB_MEMBER_MAIN_GROUP"
        If ds.Tables.Contains("MB_MEMBER_ADVISOR") = True Then
            ds.Tables("MB_MEMBER_ADVISOR").Clear()
            ds.Tables("MB_MEMBER_ADVISOR").Merge(dt)
            ds.Tables("MB_MEMBER_ADVISOR").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        cbADVISOR.DataSource = ds.Tables("MB_MEMBER_ADVISOR")
        cbADVISOR.DisplayMember = "MEMBER_GROUP_NAME2"
        cbADVISOR.ValueMember = "MEMBER_GROUP_CODE"
    End Sub

    Private Sub getMB_ADDRESS_TYPE()
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", "200")
        'parameters.Add("@p1", "1")
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_ADDRESS_TYPE "
        'query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) "
        'query &= "ORDER BY MEMBER_GROUP_NAME "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_ADDRESS_TYPE").Copy
        'dt.TableName = "MB_MEMBER_MAIN_GROUP"
        If ds.Tables.Contains("MB_ADDRESS_TYPE") = True Then
            ds.Tables("MB_ADDRESS_TYPE").Clear()
            ds.Tables("MB_ADDRESS_TYPE").Merge(dt)
            ds.Tables("MB_ADDRESS_TYPE").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If
    End Sub

    Private Sub getMB_PROVINCE()
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", "200")
        'parameters.Add("@p1", "1")
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_PROVINCE "
        'query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) "
        'query &= "ORDER BY MEMBER_GROUP_NAME "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_PROVINCE").Copy
        'dt.TableName = "MB_MEMBER_MAIN_GROUP"
        If ds.Tables.Contains("MB_PROVINCE") = True Then
            ds.Tables("MB_PROVINCE").Clear()
            ds.Tables("MB_PROVINCE").Merge(dt)
            ds.Tables("MB_PROVINCE").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        'ComboBox6.DataSource = ds.Tables("MB_PROVINCE")
        'ComboBox6.DisplayMember = "PROVINCE_NAME_TH"
        'ComboBox6.ValueMember = "PROVINCE_CODE"

        Dim dtAddr As DataTable = dt.Copy
        dtAddr.TableName = "MB_PROVINCE_ADDR"
        If ds.Tables.Contains("MB_PROVINCE_ADDR") = True Then
            ds.Tables("MB_PROVINCE_ADDR").Clear()
            ds.Tables("MB_PROVINCE_ADDR").Merge(dtAddr)
            ds.Tables("MB_PROVINCE_ADDR").AcceptChanges()
        Else
            ds.Tables.Add(dtAddr)
        End If

        'ComboBox12.DataSource = ds.Tables("MB_PROVINCE_ADDR")
        'ComboBox12.DisplayMember = "PROVINCE_NAME_TH"
        'ComboBox12.ValueMember = "PROVINCE_CODE"
    End Sub

    Private Sub getMB_MEMBER_FILES_TYPE()
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", "200")
        'parameters.Add("@p1", "1")
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_MEMBER_FILES_TYPE ORDER BY DOC_TYPE_NAME"
        'query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) "
        'query &= "ORDER BY MEMBER_GROUP_NAME "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_FILES_TYPE").Copy
        'dt.TableName = "MB_MEMBER_MAIN_GROUP"
        If ds.Tables.Contains("MB_MEMBER_FILES_TYPE") = True Then
            ds.Tables("MB_MEMBER_FILES_TYPE").Clear()
            ds.Tables("MB_MEMBER_FILES_TYPE").Merge(dt)
            ds.Tables("MB_MEMBER_FILES_TYPE").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If
    End Sub

    Private Sub getMB_PRENAME()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_PRENAME ORDER BY PRENAME_TH"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_PRENAME").Copy
        Dim dt2 As DataTable = dt.Copy
        dt2.TableName = "MB_PRENAME2"

        If ds.Tables.Contains("MB_PRENAME") = True Then
            ds.Tables("MB_PRENAME").Clear()
            ds.Tables("MB_PRENAME").Merge(dt)
            ds.Tables("MB_PRENAME").AcceptChanges()

            ds.Tables("MB_PRENAME2").Clear()
            ds.Tables("MB_PRENAME2").Merge(dt2)
            ds.Tables("MB_PRENAME2").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            ds.Tables.Add(dt2)
        End If

        ComboBox10.DataSource = ds.Tables("MB_PRENAME")
        ComboBox10.DisplayMember = "PRENAME_TH"
        ComboBox10.ValueMember = "PRENAME_CODE"

        ComboBox5.DataSource = ds.Tables("MB_PRENAME2")
        ComboBox5.DisplayMember = "PRENAME_TH"
        ComboBox5.ValueMember = "PRENAME_CODE"
    End Sub

    Private Sub getMB_MEMBER_STATUS()
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", INI_CODE)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_MEMBER_STATUS WHERE MODULE = @p0 ORDER BY MEMBER_STATUS_CODE"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_STATUS").Copy

        If ds.Tables.Contains("MB_MEMBER_STATUS") = True Then
            ds.Tables("MB_MEMBER_STATUS").Clear()
            ds.Tables("MB_MEMBER_STATUS").Merge(dt)
            ds.Tables("MB_MEMBER_STATUS").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox13.DataSource = ds.Tables("MB_MEMBER_STATUS")
        ComboBox13.DisplayMember = "MEMBER_STATUS_NAME_TH"
        ComboBox13.ValueMember = "MEMBER_STATUS_CODE"
    End Sub

    Private Sub getMB_BUSSINESS_TYPE()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT BUSSINESS_TYPE_CODE, BUSSINESS_TYPE_DESC_TH, BUSSINESS_TYPE_DESC_EN FROM MB_BUSSINESS_TYPE ORDER BY BUSSINESS_TYPE_DESC_TH"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_BUSSINESS_TYPE").Copy

        If ds.Tables.Contains("MB_BUSSINESS_TYPE") = True Then
            ds.Tables("MB_BUSSINESS_TYPE").Clear()
            ds.Tables("MB_BUSSINESS_TYPE").Merge(dt)
            ds.Tables("MB_BUSSINESS_TYPE").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If
    End Sub

    Private Sub getMB_MAIN_INDUSTRY()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT MAIN_INDUSTRY_CODE, MAIN_INDUSTRY_DESC_TH, MAIN_INDUSTRY_DESC_EN FROM MB_MAIN_INDUSTRY ORDER BY MAIN_INDUSTRY_DESC_TH"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MAIN_INDUSTRY").Copy

        If ds.Tables.Contains("MB_MAIN_INDUSTRY") = True Then
            ds.Tables("MB_MAIN_INDUSTRY").Clear()
            ds.Tables("MB_MAIN_INDUSTRY").Merge(dt)
            ds.Tables("MB_MAIN_INDUSTRY").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If
    End Sub

    Private Sub getMB_PRODUCT()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT PRODUCT_CODE, PRODUCT_DESC_TH, PRODUCT_DESC_EN FROM MB_PRODUCT ORDER BY PRODUCT_DESC_TH"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_PRODUCT").Copy

        If ds.Tables.Contains("MB_PRODUCT") = True Then
            ds.Tables("MB_PRODUCT").Clear()
            ds.Tables("MB_PRODUCT").Merge(dt)
            ds.Tables("MB_PRODUCT").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If
    End Sub

    Private Sub getMB_ISO()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT ISO_CODE, ISO_DESC_TH, ISO_DESC_EN FROM MB_ISO ORDER BY ISO_DESC_TH"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_ISO").Copy

        If ds.Tables.Contains("MB_ISO") = True Then
            ds.Tables("MB_ISO").Clear()
            ds.Tables("MB_ISO").Merge(dt)
            ds.Tables("MB_ISO").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If
    End Sub

    Private Sub getLOGS(ByVal TOP_VALUE As Integer, Optional ByVal DATE_FROM As Date = Nothing, Optional ByVal DATE_TO As Date = Nothing)
        'logs
        If tbREGIST_CODE.TextLength > 0 Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim pLogs As New Dictionary(Of String, Object)
            pLogs.Add("@p0", tbREGIST_CODE.Text)

            Dim qLogs As String = String.Empty
            qLogs &= "SELECT TOP " & TOP_VALUE & " * FROM MB_LOGS "
            qLogs &= "WHERE (REGIST_CODE = @p0) "
            If DATE_FROM <> Nothing Then
                pLogs.Add("@p1", New Date(DATE_FROM.Year, DATE_FROM.Month, DATE_FROM.Day, 0, 0, 0))
                pLogs.Add("@p2", New Date(DATE_TO.Year, DATE_TO.Month, DATE_TO.Day, 23, 59, 59))

                qLogs &= "AND (MODIFY_DATE BETWEEN @p1 AND @p2) "
            End If
            qLogs &= "ORDER BY ID DESC "

            Dim dtLogs As DataTable = New DataTable

            dtLogs = fillWebSQL(qLogs, pLogs, "MB_LOGS")

            If ds.Tables.Contains("MB_LOGS") = True Then
                ds.Tables("MB_LOGS").Clear()
                ds.Tables("MB_LOGS").Merge(dtLogs)
                ds.Tables("MB_LOGS").AcceptChanges()
            Else
                ds.Tables.Add(dtLogs)

                'primary key
                ds.Tables("MB_LOGS").PrimaryKey = New DataColumn() {ds.Tables("MB_LOGS").Columns("ID")}

                dvLOGS = New DataView(ds.Tables("MB_LOGS"))

                DataGridView4.DataSource = dvLOGS
            End If
        End If
    End Sub


    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedValue IsNot Nothing And dvMEMBER_GROUP IsNot Nothing Then dvMEMBER_GROUP.RowFilter = String.Format("MEMBER_MAIN_GROUP_CODE = '{0}'", ComboBox3.SelectedValue.ToString)
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        If ComboBox3.SelectedValue IsNot Nothing And ComboBox4.SelectedValue IsNot Nothing And dvMEMBER_MAIN_TYPE IsNot Nothing Then dvMEMBER_MAIN_TYPE.RowFilter = String.Format("MEMBER_MAIN_GROUP_CODE = '{0}' AND MEMBER_GROUP_CODE = '{1}'", ComboBox3.SelectedValue.ToString, ComboBox4.SelectedValue.ToString)
    End Sub

    Private Sub ComboBox14_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox14.SelectedIndexChanged
        If ComboBox3.SelectedValue IsNot Nothing And ComboBox4.SelectedValue IsNot Nothing And ComboBox14.SelectedValue IsNot Nothing And dvMEMBER_TYPE IsNot Nothing Then dvMEMBER_TYPE.RowFilter = String.Format("MEMBER_MAIN_GROUP_CODE = '{0}' AND MEMBER_GROUP_CODE = '{1}' AND MEMBER_MAIN_TYPE_CODE = '{2}'", ComboBox3.SelectedValue.ToString, ComboBox4.SelectedValue.ToString, ComboBox14.SelectedValue.ToString)
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            getMEMBER()
        End If
    End Sub

    Private Sub gridControl1_CurrentRowChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim xrow As Xceed.Grid.DataRow = CType(RadGridView1.CurrentRow, Xceed.Grid.DataRow)
        DataGridView1.EndEdit()
        'DataGridView1.CancelEdit()

        bsMain.EndEdit()
        'bsMain.CancelEdit()

        ds.Tables("MB_MEMBER").RejectChanges()

        If DataGridView1.CurrentRow IsNot Nothing Then

            'Dim idx As Integer = ds.Tables("MB_MEMBER").Rows.IndexOf(ds.Tables("MB_MEMBER").Rows.Find(DataGridView1.CurrentRow.Cells("ROWID").Value))

            'If idx >= 0 Then
            '    'MessageBox.Show("idx=" & idx)

            '    'MessageBox.Show("bs=" & bs.Count)

            '    'bs.CancelEdit()

            '    'bsMain.Position = idx 'bs.Find("MEMBER", DataGridView1.CurrentRow.Cells("ROWID").Value)
            '    'bs.Position = ds.Tables("MB_MEMBER").Rows.IndexOf(ds.Tables("MB_MEMBER").Rows.Find(DataGridView1.CurrentRow.Cells("ROWID").Value))

            '    'MessageBox.Show("RowIndex1=" & bs.Position)



            '    'MessageBox.Show("RowIndex2=" & bsMain.Position)
            'End If

            'MessageBox.Show(bs.Position.ToString)

            'get info from prono
            getInfo(DataGridView1.CurrentRow.Cells("ROWID").Value.ToString, DataGridView1.CurrentRow.Cells("COMP_PERSON_CODE").Value.ToString)

            If DataGridView2.Columns.Contains("OU_CODE") = True Then DataGridView2.Columns("OU_CODE").Visible = False
        End If

        'advisor
        cbADVISOR.Visible = chkADVISOR.Checked
        tbADVISOR.Visible = chkADVISOR.Checked
    End Sub

    Private Sub gridControl2_CurrentRowChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim xrow As Xceed.Grid.DataRow = CType(RadGridView1.CurrentRow, Xceed.Grid.DataRow)

        DataGridView2.CancelEdit()

    End Sub

    Private Sub gridControl5_CurrentRowChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim xrow As Xceed.Grid.DataRow = CType(RadGridView1.CurrentRow, Xceed.Grid.DataRow)

        If tbCONTACT_CODE.TextLength > 0 Then
            DataGridView5.CancelEdit()
            Dim row As DataRow = ds.Tables("MB_MEMBER_REPRESENT").Rows.Find(DataGridView5.CurrentRow.Cells("CONTACT_CODE").Value)
            'bsRep.Position = ds.Tables("MB_MEMBER_REPRESENT").Rows.IndexOf(row)

            'MessageBox.Show(row("SEX").ToString)

            'MessageBox.Show("bsAddress.Position: " & bsAddress.Position)

            'TextBox18.Text = row.Item("ADDR_POSTCODE").ToString

            If row("SEX").ToString.Length > 0 Then
                ComboBox11.Text = row("SEX").ToString
            Else
                ComboBox11.SelectedIndex = -1
            End If

            'get contact photo
            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", row("CONTACT_CODE"))

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

                DataGridView7.DataSource = ds.Tables("MB_COMMITTEE_PERIOD")

                'For i As Integer = 0 To DataGridView7.Columns.Count - 1
                '    DataGridView7.Columns(i).Visible = False
                'Next

                DataGridView7.Columns("CONTACT_CODE").Visible = False
                DataGridView7.Columns("START_YEAR").Visible = False
                DataGridView7.Columns("END_YEAR").Visible = False

                DataGridView7.Columns("PERIOD_SEQ").Width = 50
                DataGridView7.Columns("POSITION_NAME_TH").Width = 150
                DataGridView7.Columns("PERIOD_NAME").Width = 150

                'DataGridView7.Columns("PERIOD_SEQ").Visible = True
                'DataGridView7.Columns("PERIOD_CODE").Visible = True
            End If
        End If


    End Sub

    Private Sub getInfo(ByVal REGIST_CODE As String, ByVal COMP_PERSON_CODE As String)
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        'ComboBox3.DataBindings.Add("SelectedValue", bs, "MEMBER_MAIN_GROUP_CODE")
        'ComboBox4.DataBindings.Add("SelectedValue", bs, "MEMBER_GROUP_CODE")
        'ComboBox14.DataBindings.Add("SelectedValue", bs, "MEMBER_MAIN_TYPE_CODE")
        'ComboBox15.DataBindings.Add("SelectedValue", bs, "MEMBER_TYPE_CODE")

        'clear log
        ds.Tables("LOGS").Clear()

        'make sure it empty
        PictureBox1.Image = Nothing

        'print invoice
        'If tbMEMBER_CODE.Text = "<AUTO>" And ComboBox13.Text = "X" Then
        '    LinkLabel1.Visible = True
        'Else
        '    LinkLabel1.Visible = False
        'End If

        'info
        Dim row As DataRow = ds.Tables("MB_MEMBER").Rows.Find(REGIST_CODE)

        'Dim dtMB_MEMBER_MAIN_GROUP As DataTable = dvMB_MEMBER_MAIN_GROUP.ToTable
        'ComboBox3.SelectedIndex = -1
        'For i As Integer = 0 To dtMB_MEMBER_MAIN_GROUP.Rows.Count - 1
        '    If dtMB_MEMBER_MAIN_GROUP.Rows(i).Item("MEMBER_MAIN_GROUP_CODE").ToString = row.Item("MEMBER_MAIN_GROUP_CODE").ToString Then
        '        ComboBox3.SelectedIndex = i
        '        Exit For
        '    End If
        'Next

        'Dim dtMEMBER_GROUP As DataTable = dvMEMBER_GROUP.ToTable
        'ComboBox4.SelectedIndex = -1
        'For i As Integer = 0 To dtMEMBER_GROUP.Rows.Count - 1
        '    If dtMEMBER_GROUP.Rows(i).Item("MEMBER_GROUP_CODE").ToString = row.Item("MEMBER_GROUP_CODE").ToString Then
        '        ComboBox4.SelectedIndex = i
        '        Exit For
        '    End If
        'Next

        'Dim dtMEMBER_MAIN_TYPE As DataTable = dvMEMBER_MAIN_TYPE.ToTable
        'ComboBox14.SelectedIndex = -1
        'For i As Integer = 0 To dtMEMBER_MAIN_TYPE.Rows.Count - 1
        '    If dtMEMBER_MAIN_TYPE.Rows(i).Item("MEMBER_MAIN_TYPE_CODE").ToString = row.Item("MEMBER_MAIN_TYPE_CODE").ToString Then
        '        ComboBox14.SelectedIndex = i
        '        Exit For
        '    End If
        'Next

        'Dim dtMEMBER_TYPE As DataTable = dvMEMBER_TYPE.ToTable
        'ComboBox15.SelectedIndex = -1
        'For i As Integer = 0 To dtMEMBER_TYPE.Rows.Count - 1
        '    If dtMEMBER_TYPE.Rows(i).Item("MEMBER_TYPE_CODE").ToString = row.Item("MEMBER_TYPE_CODE").ToString Then
        '        ComboBox15.SelectedIndex = i
        '        Exit For
        '    End If
        'Next

        'TextBox21.Text = row.Item("MEMBER_CODE").ToString
        'TextBox22.Text = row.Item("REGIST_CODE").ToString
        'ComboBox13.Text = row.Item("MEMBER_STATUS_CODE").ToString
        'TextBox2.Text = row.Item("COMP_PERSON_NAME_TH").ToString
        'TextBox4.Text = row.Item("COMP_PERSON_NAME_EN").ToString

        'ComboBox3.SelectedValue = row.Item("MEMBER_MAIN_GROUP_CODE")
        'ComboBox4.SelectedValue = row.Item("MEMBER_GROUP_CODE")
        'ComboBox14.SelectedValue = row.Item("MEMBER_MAIN_TYPE_CODE")
        'ComboBox15.SelectedValue = row.Item("MEMBER_TYPE_CODE")
        'ComboBox2_SelectedIndexChanged(Nothing, Nothing)
        'ComboBox2.SelectedValue = tbADVISOR.Text
        ComboBox3_SelectedIndexChanged(Nothing, Nothing)
        ComboBox3.SelectedValue = tbMEMBER_MAIN_GROUP.Text
        ComboBox4_SelectedIndexChanged(Nothing, Nothing)
        ComboBox4.SelectedValue = tbMEMBER_GROUP.Text
        ComboBox14_SelectedIndexChanged(Nothing, Nothing)
        ComboBox14.SelectedValue = tbMEMBER_MAIN_TYPE.Text

        'make sure default value has been apply
        'IF(expression,<true return>,<false return>)
        TextBox4.Text = If(IsDBNull(row.Item("COMP_PERSON_NAME_EN")), "", row.Item("COMP_PERSON_NAME_EN").ToString)

        NumericUpDown1.Value = If(IsDBNull(row.Item("REGIST_CAPITAL")), 0, CDec(row.Item("REGIST_CAPITAL")))
        NumericUpDown2.Value = If(IsDBNull(row.Item("VENTURE_CAP_THA")), 0, CDec(row.Item("VENTURE_CAP_THA"))) 'If (IsDBNull(row.Item("VENTURE_CAP_THA")) = False then cdec(row.Item("VENTURE_CAP_THA")) else 0))
        NumericUpDown3.Value = If(IsDBNull(row.Item("VENTURE_CAP_ENG")), 0, CDec(row.Item("VENTURE_CAP_ENG")))
        NumericUpDown4.Value = If(IsDBNull(row.Item("ASSET_AMOUNT")), 0, CDec(row.Item("ASSET_AMOUNT")))
        NumericUpDown5.Value = If(IsDBNull(row.Item("EMPLOYEE_AMOUNT")), 0, CDec(row.Item("EMPLOYEE_AMOUNT")))

        CheckBox2.Checked = If(IsDBNull(row.Item("BUS_TYPE_RELATE")), False, CBool(row.Item("BUS_TYPE_RELATE")))
        CheckBox3.Checked = If(IsDBNull(row.Item("BUS_TYPE_DEALER")), False, CBool(row.Item("BUS_TYPE_DEALER")))
        CheckBox4.Checked = If(IsDBNull(row.Item("BUS_TYPE_IMPORTER")), False, CBool(row.Item("BUS_TYPE_IMPORTER")))
        CheckBox5.Checked = If(IsDBNull(row.Item("BUS_TYPE_EXPORTER")), False, CBool(row.Item("BUS_TYPE_EXPORTER")))
        TextBox71.Text = row.Item("BUS_TYPE_OTHER").ToString

        'TextBox72.Text = row.Item("MAIN_PRODUCTS_SERVICES").ToString
        TextBox72.Text = row.Item("PRODUCE_TECHNOLOGY_DESC").ToString

        NumericUpDown6.Value = If(IsDBNull(row.Item("PRODUCE_AMOUNT")), 0, CDec(row.Item("PRODUCE_AMOUNT")))
        NumericUpDown7.Value = If(IsDBNull(row.Item("SALE_DOMESTIC_PERCENT")), 0, CDec(row.Item("SALE_DOMESTIC_PERCENT")))
        NumericUpDown8.Value = If(IsDBNull(row.Item("SALE_INTERNATIONAL_PERCENT")), 0, CDec(row.Item("SALE_INTERNATIONAL_PERCENT")))

        chkADVISOR.Checked = If(IsDBNull(row.Item("ADVISOR_CHECKED")), False, CBool(row.Item("ADVISOR_CHECKED")))
        cbADVISOR.SelectedValue = row.Item("ADVISOR_CODE")

        NumericUpDown11.Value = If(IsDBNull(row.Item("INCOME_PER_YEAR")), 0, CDec(row.Item("INCOME_PER_YEAR")))

        NumericUpDown12.Value = If(IsDBNull(row.Item("ELECTRIC_AMOUNT")), 0, CDec(row.Item("ELECTRIC_AMOUNT")))

        CheckBox6.Checked = If(IsDBNull(row.Item("PRODUCE_TECHNOLOGY_TH")), False, CBool(row.Item("PRODUCE_TECHNOLOGY_TH")))
        CheckBox7.Checked = If(IsDBNull(row.Item("PRODUCE_TECHNOLOGY_EN")), False, CBool(row.Item("PRODUCE_TECHNOLOGY_EN")))

        'dates
        MaskedTextBox1.Text = If(IsDBNull(row.Item("REGIST_DATE")), "", CDate(row.Item("REGIST_DATE")).ToString("dd/MM/yyyy", ciTH))
        MaskedTextBox2.Text = If(IsDBNull(row.Item("MEMBER_DATE")), "", CDate(row.Item("MEMBER_DATE")).ToString("dd/MM/yyyy", ciTH))
        MaskedTextBox3.Text = If(IsDBNull(row.Item("RETIRE_DATE")), "", CDate(row.Item("RETIRE_DATE")).ToString("dd/MM/yyyy", ciTH))
        MaskedTextBox4.Text = If(IsDBNull(row.Item("APPROVE_RETIRE_DATE")), "", CDate(row.Item("APPROVE_RETIRE_DATE")).ToString("dd/MM/yyyy", ciTH))

        'address
        Dim pAddress As New Dictionary(Of String, Object)
        pAddress.Add("@p0", COMP_PERSON_CODE)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_COMP_PERSON_ADDRESS "
        query &= "WHERE (COMP_PERSON_CODE= @p0) "
        query &= "ORDER BY ADDR_CODE, ADDR_LANG"

        Dim dtAddress As DataTable = New DataTable

        dtAddress = fillWebSQL(query, pAddress, "MB_COMP_PERSON_ADDRESS")

        If ds.Tables.Contains("MB_COMP_PERSON_ADDRESS") = True Then
            ds.Tables("MB_COMP_PERSON_ADDRESS").Clear()
            ds.Tables("MB_COMP_PERSON_ADDRESS").Merge(dtAddress)
            ds.Tables("MB_COMP_PERSON_ADDRESS").AcceptChanges()

            'For i As Integer = 0 To DataGridView2.ColumnCount - 1
            '    DataGridView2.Columns(i).Visible = False
            'Next
            'DataGridView2.Columns("ADDR_LANG").Visible = True
            'DataGridView2.Columns(0).Visible = True
        Else
            ds.Tables.Add(dtAddress)

            'primary key
            ds.Tables("MB_COMP_PERSON_ADDRESS").PrimaryKey = New DataColumn() {ds.Tables("MB_COMP_PERSON_ADDRESS").Columns("ROWID")}

            'blinding
            bsAddress = New BindingSource(ds, "MB_COMP_PERSON_ADDRESS")

            DataGridView2.DataSource = bsAddress

            For i As Integer = 0 To DataGridView2.ColumnCount - 1
                DataGridView2.Columns(i).Visible = False
            Next
            DataGridView2.Columns("ADDR_CODE").Visible = True
            'DataGridView2.Columns(0).Visible = True
            'DataGridView2.Columns("CR_BY").Visible = True
            'DataGridView2.Columns("CR_DATE").Visible = True
            'DataGridView2.Columns("UPD_BY").Visible = True
            'DataGridView2.Columns("UPD_DATE").Visible = True

            TextBox37.DataBindings.Add("Text", bsAddress, "ADDR_POSTCODE")
            ComboBox9.DataBindings.Add("SelectedValue", bsAddress, "ADDR_CODE") ' address type
            'TH
            TextBox5.DataBindings.Add("Text", bsAddress, "ADDR_NO")
            TextBox6.DataBindings.Add("Text", bsAddress, "ADDR_MOO")
            TextBox7.DataBindings.Add("Text", bsAddress, "ADDR_SOI")
            TextBox8.DataBindings.Add("Text", bsAddress, "ADDR_ROAD")

            'LOCAL_CODE TH
            TextBox34.DataBindings.Add("Text", bsAddress, "ADDR_SUB_DISTRICT")
            TextBox35.DataBindings.Add("Text", bsAddress, "ADDR_DISTRICT")
            TextBox36.DataBindings.Add("Text", bsAddress, "ADDR_PROVINCE_NAME")

            'tel/fax/web/email TH
            TextBox38.DataBindings.Add("Text", bsAddress, "ADDR_TELEPHONE")
            TextBox42.DataBindings.Add("Text", bsAddress, "ADDR_FAX")
            TextBox43.DataBindings.Add("Text", bsAddress, "ADDR_WEBSITE")
            TextBox44.DataBindings.Add("Text", bsAddress, "ADDR_EMAIL")

            'ComboBox16.DataBindings.Add("Text", bsAddress, "ADDR_LANG")
            'EN
            TextBox24.DataBindings.Add("Text", bsAddress, "ADDR_NO_EN")
            TextBox25.DataBindings.Add("Text", bsAddress, "ADDR_MOO_EN")
            TextBox26.DataBindings.Add("Text", bsAddress, "ADDR_SOI_EN")
            TextBox27.DataBindings.Add("Text", bsAddress, "ADDR_ROAD_EN")

            'LOCAL_CODE EN
            TextBox41.DataBindings.Add("Text", bsAddress, "ADDR_SUB_DISTRICT_EN")
            TextBox40.DataBindings.Add("Text", bsAddress, "ADDR_DISTRICT_EN")
            TextBox39.DataBindings.Add("Text", bsAddress, "ADDR_PROVINCE_NAME_EN")

            'tel/fax/web/email EN
            TextBox48.DataBindings.Add("Text", bsAddress, "ADDR_TELEPHONE_EN")
            TextBox47.DataBindings.Add("Text", bsAddress, "ADDR_FAX_EN")
            TextBox46.DataBindings.Add("Text", bsAddress, "ADDR_WEBSITE_EN")
            TextBox45.DataBindings.Add("Text", bsAddress, "ADDR_EMAIL_EN")

            'ComboBox22.DataBindings.Add("Text", bsAddress, "ADDR_POSTCODE_EN")
            'ComboBox23.DataBindings.Add("SelectedValue", bsAddress, "ADDR_PROVINCE_CODE_EN")
            'ComboBox24.DataBindings.Add("Text", bsAddress, "ADDR_DISTRICT_EN")
            'ComboBox25.DataBindings.Add("Text", bsAddress, "ADDR_SUB_DISTRICT_EN")

            AddHandler DataGridView2.SelectionChanged, AddressOf gridControl2_CurrentRowChanged

            'combobox
            Dim comboBoxColumn As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
            comboBoxColumn.HeaderText = "ADDRESS_TYPE_NAME_TH"
            comboBoxColumn.DataPropertyName = "ADDR_CODE"
            comboBoxColumn.DataSource = ds.Tables("MB_ADDRESS_TYPE")
            comboBoxColumn.ValueMember = ds.Tables("MB_ADDRESS_TYPE").Columns(0).ColumnName
            comboBoxColumn.DisplayMember = ds.Tables("MB_ADDRESS_TYPE").Columns(1).ColumnName
            comboBoxColumn.Width = 200

            'DataGridView2.Columns.RemoveAt(0)
            DataGridView2.Columns.Insert(DataGridView2.Columns("ADDR_CODE").Index + 1, comboBoxColumn)

            'DataGridView2.Columns(0).Width = 150

            ComboBox9.DataSource = ds.Tables("MB_ADDRESS_TYPE")
            ComboBox9.DisplayMember = "ADDRESS_TYPE_NAME_TH"
            ComboBox9.ValueMember = "ADDRESS_TYPE_CODE"
        End If

        'fee

        'represent
        OpenImageFileDlg.FileName = String.Empty

        Dim pRep As New Dictionary(Of String, Object)
        pRep.Add("@p0", row.Item("REGIST_CODE"))
        Dim qRep As String = String.Empty
        qRep &= "SELECT MB_MEMBER_REPRESENT.*, MB_CONTACT.*, MB_POSITION.* "
        qRep &= "FROM            MB_MEMBER_REPRESENT LEFT OUTER JOIN "
        qRep &= "                         MB_CONTACT ON MB_MEMBER_REPRESENT.CONTACT_CODE = MB_CONTACT.CONTACT_CODE LEFT OUTER JOIN "
        qRep &= "                         MB_POSITION ON MB_MEMBER_REPRESENT.POSITION_CODE = MB_POSITION.POSITION_CODE "
        qRep &= "WHERE (MB_MEMBER_REPRESENT.REGIST_CODE= @p0) "
        qRep &= "ORDER BY REPRESENT_SEQ "

        Dim dtRep As DataTable = New DataTable

        dtRep = fillWebSQL(qRep, pRep, "MB_MEMBER_REPRESENT")

        If ds.Tables.Contains("MB_MEMBER_REPRESENT") = True Then
            ds.Tables("MB_MEMBER_REPRESENT").Clear()
            ds.Tables("MB_MEMBER_REPRESENT").Merge(dtRep)
            ds.Tables("MB_MEMBER_REPRESENT").AcceptChanges()
        Else
            ds.Tables.Add(dtRep)

            'primary key
            ds.Tables("MB_MEMBER_REPRESENT").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER_REPRESENT").Columns("CONTACT_CODE")}

            'blinding
            bsRep = New BindingSource(ds, "MB_MEMBER_REPRESENT")

            DataGridView5.DataSource = bsRep

            For i As Integer = 0 To DataGridView5.ColumnCount - 1
                DataGridView5.Columns(i).Visible = False
            Next
            '
            DataGridView5.Columns("REPRESENT_SEQ").Visible = True
            DataGridView5.Columns("CONTACT_FIRST_NAME_TH").Visible = True
            DataGridView5.Columns("CONTACT_LAST_NAME_TH").Visible = True

            DataGridView5.Columns("CONTACT_FIRST_NAME_TH").Width = 200
            DataGridView5.Columns("CONTACT_LAST_NAME_TH").Width = 200

            TextBox9.DataBindings.Add("Text", bsRep, "CONTACT_FIRST_NAME_TH")
            TextBox10.DataBindings.Add("Text", bsRep, "CONTACT_LAST_NAME_TH")
            TextBox11.DataBindings.Add("Text", bsRep, "ADDR_NO_TH")
            TextBox12.DataBindings.Add("Text", bsRep, "ADDR_MOO_TH")
            TextBox13.DataBindings.Add("Text", bsRep, "ADDR_SOI_TH")
            TextBox14.DataBindings.Add("Text", bsRep, "ADDR_ROAD_TH")
            TextBox50.DataBindings.Add("Text", bsRep, "ADDR_SUB_DISTRICT_TH")
            TextBox51.DataBindings.Add("Text", bsRep, "ADDR_DISTRICT_TH")
            'ComboBox17.DataBindings.Add("Text", bsRep, "ADDR_SUB_DISTRICT_TH")
            'ComboBox19.DataBindings.Add("Text", bsRep, "ADDR_DISTRICT_TH")
            TextBox49.DataBindings.Add("Text", bsRep, "ADDR_PROVINCE_NAME_TH")

            TextBox30.DataBindings.Add("Text", bsRep, "CONTACT_FIRST_NAME_EN")
            TextBox31.DataBindings.Add("Text", bsRep, "CONTACT_LAST_NAME_EN")
            TextBox32.DataBindings.Add("Text", bsRep, "ADDR_NO_EN")
            TextBox33.DataBindings.Add("Text", bsRep, "ADDR_MOO_EN")
            TextBox29.DataBindings.Add("Text", bsRep, "ADDR_SOI_EN")
            TextBox28.DataBindings.Add("Text", bsRep, "ADDR_ROAD_EN")
            TextBox52.DataBindings.Add("Text", bsRep, "ADDR_SUB_DISTRICT_EN")
            TextBox53.DataBindings.Add("Text", bsRep, "ADDR_DISTRICT_EN")
            'ComboBox18.DataBindings.Add("Text", bsRep, "ADDR_SUB_DISTRICT_EN")
            'ComboBox20.DataBindings.Add("Text", bsRep, "ADDR_DISTRICT_EN")
            TextBox54.DataBindings.Add("Text", bsRep, "ADDR_PROVINCE_NAME_EN")

            TextBox15.DataBindings.Add("Text", bsRep, "PERSONAL_ID")
            TextBox17.DataBindings.Add("Text", bsRep, "ADDR_TELEPHONE")
            TextBox19.DataBindings.Add("Text", bsRep, "ADDR_FAX")
            TextBox20.DataBindings.Add("Text", bsRep, "ADDR_EMAIL")

            'ComboBox10.DataBindings.Add("SelectedValue", bsRep, "CONTACT_PRENAME_CODE")
            ComboBox11.DataBindings.Add(New Binding("Text", bsRep, "SEX", True, DataSourceUpdateMode.OnValidation))
            ComboBox10.DataBindings.Add(New Binding("SelectedValue", bsRep, "CONTACT_PRENAME_CODE", True, DataSourceUpdateMode.OnValidation))
            'ComboBox11.DataBindings.Add(New Binding("Text", bsRep, "SEX", True, DataSourceUpdateMode.OnValidation))
            'ComboBox12.DataBindings.Add("Text", bsRep, "ADDR_PROVINCE_CODE")
            'ComboBox12.DataBindings.Add("SelectedValue", bsRep, "ADDR_PROVINCE_CODE")

            TextBox18.DataBindings.Add("Text", bsRep, "ADDR_POSTCODE")

            TextBox16.DataBindings.Add("Text", bsRep, "POSITION_NAME_TH")
            TextBox23.DataBindings.Add("Text", bsRep, "POSITION_NAME_EN")

            NumericUpDown10.DataBindings.Add(New Binding("Value", bsRep, "REPRESENT_SEQ", True, DataSourceUpdateMode.OnValidation))

            tbCONTACT_CODE.DataBindings.Add("Text", bsRep, "CONTACT_CODE")

            AddHandler DataGridView5.SelectionChanged, AddressOf gridControl5_CurrentRowChanged
        End If

        'position


        'files
        Dim pFiles As New Dictionary(Of String, Object)
        pFiles.Add("@p0", row.Item("REGIST_CODE"))
        Dim qFiles As String = String.Empty
        qFiles &= "SELECT ID, CATEGORY, REGIST_CODE, DOC_TYPE, DOC_NAME, FILE_NAME FROM MB_MEMBER_FILES "
        qFiles &= "WHERE (REGIST_CODE = @p0) "
        qFiles &= "ORDER BY ID "

        Dim dtFiles As DataTable = New DataTable

        dtFiles = fillWebSQL(qFiles, pFiles, "MB_MEMBER_FILES")

        If ds.Tables.Contains("MB_MEMBER_FILES") = True Then
            ds.Tables("MB_MEMBER_FILES").Clear()
            ds.Tables("MB_MEMBER_FILES").Merge(dtFiles)
            ds.Tables("MB_MEMBER_FILES").AcceptChanges()
        Else
            ds.Tables.Add(dtFiles)

            'primary key
            ds.Tables("MB_MEMBER_FILES").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER_FILES").Columns("ID")}

            DataGridView6.DataSource = ds.Tables("MB_MEMBER_FILES")

            For i As Integer = 0 To DataGridView6.ColumnCount - 1
                DataGridView6.Columns(i).Visible = False
            Next
            'DataGridView6.Columns("DOC_TYPE").Visible = True
            DataGridView6.Columns("FILE_NAME").Visible = True

            Dim comboBoxColumn As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
            comboBoxColumn.HeaderText = "DOC_TYPE"
            comboBoxColumn.DataPropertyName = "DOC_TYPE"
            comboBoxColumn.DataSource = ds.Tables("MB_MEMBER_FILES_TYPE")
            comboBoxColumn.ValueMember = ds.Tables("MB_MEMBER_FILES_TYPE").Columns(0).ColumnName
            comboBoxColumn.DisplayMember = ds.Tables("MB_MEMBER_FILES_TYPE").Columns(1).ColumnName

            DataGridView6.Columns.RemoveAt(0)
            DataGridView6.Columns.Insert(0, comboBoxColumn)
        End If

        If DataGridView6.Columns.Contains("ID") = True Then
            DataGridView6.Columns("ID").ReadOnly = True
            DataGridView6.Columns("ID").Visible = False
        End If

        'bus type
        Dim pBus As New Dictionary(Of String, Object)
        pBus.Add("@p0", COMP_PERSON_CODE)
        Dim qBus As String = String.Empty
        qBus &= "SELECT COMP_PERSON_CODE, BUSSINESS_TYPE_CODE FROM MB_COMP_PERSON_BUS_TYPE "
        qBus &= "WHERE (COMP_PERSON_CODE = @p0) "
        'qFiles &= "ORDER BY ID "

        Dim dtBus As DataTable = New DataTable

        dtBus = fillWebSQL(qBus, pBus, "MB_COMP_PERSON_BUS_TYPE")

        If ds.Tables.Contains("MB_COMP_PERSON_BUS_TYPE") = True Then
            ds.Tables("MB_COMP_PERSON_BUS_TYPE").Clear()
            ds.Tables("MB_COMP_PERSON_BUS_TYPE").Merge(dtBus)
            ds.Tables("MB_COMP_PERSON_BUS_TYPE").AcceptChanges()
        Else
            ds.Tables.Add(dtBus)

            'primary key
            ds.Tables("MB_COMP_PERSON_BUS_TYPE").PrimaryKey = New DataColumn() {ds.Tables("MB_COMP_PERSON_BUS_TYPE").Columns("BUSSINESS_TYPE_CODE")}

            DataGridView8.DataSource = ds.Tables("MB_COMP_PERSON_BUS_TYPE")

            For i As Integer = 0 To DataGridView8.ColumnCount - 1
                DataGridView8.Columns(i).Visible = False
            Next
            'DataGridView6.Columns("DOC_TYPE").Visible = True
            DataGridView8.Columns("BUSSINESS_TYPE_CODE").Visible = True

            Dim comboBoxColumnTH As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
            comboBoxColumnTH.HeaderText = "BUSSINESS_TYPE_DESC_TH"
            comboBoxColumnTH.DataPropertyName = "BUSSINESS_TYPE_CODE"
            comboBoxColumnTH.DataSource = ds.Tables("MB_BUSSINESS_TYPE")
            comboBoxColumnTH.ValueMember = ds.Tables("MB_BUSSINESS_TYPE").Columns("BUSSINESS_TYPE_CODE").ColumnName
            comboBoxColumnTH.DisplayMember = ds.Tables("MB_BUSSINESS_TYPE").Columns("BUSSINESS_TYPE_DESC_TH").ColumnName
            comboBoxColumnTH.Width = 250

            Dim cTH As Integer = DataGridView8.Columns("BUSSINESS_TYPE_CODE").Index

            DataGridView8.Columns.RemoveAt(cTH)
            DataGridView8.Columns.Insert(cTH, comboBoxColumnTH)

            Dim comboBoxColumnEN As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
            comboBoxColumnEN.HeaderText = "BUSSINESS_TYPE_DESC_EN"
            comboBoxColumnEN.DataPropertyName = "BUSSINESS_TYPE_CODE"
            comboBoxColumnEN.DataSource = ds.Tables("MB_BUSSINESS_TYPE")
            comboBoxColumnEN.ValueMember = ds.Tables("MB_BUSSINESS_TYPE").Columns("BUSSINESS_TYPE_CODE").ColumnName
            comboBoxColumnEN.DisplayMember = ds.Tables("MB_BUSSINESS_TYPE").Columns("BUSSINESS_TYPE_DESC_EN").ColumnName
            comboBoxColumnEN.Width = 250

            Dim cEN As Integer = comboBoxColumnTH.Index + 1

            'DataGridView8.Columns.RemoveAt(cEN)
            DataGridView8.Columns.Insert(cEN, comboBoxColumnEN)
        End If
        If DataGridView8.Columns.Contains("COMP_PERSON_CODE") = True Then DataGridView8.Columns("COMP_PERSON_CODE").Visible = False

        ''indus type
        'Dim pIndus As New Dictionary(Of String, Object)
        'pIndus.Add("@p0", COMP_PERSON_CODE)
        'Dim qIndus As String = String.Empty
        'qIndus &= "SELECT MAIN_INDUSTRY_CODE FROM MB_COMP_PERSON_MAIN_INDUS "
        'qIndus &= "WHERE (COMP_PERSON_CODE = @p0) "
        ''qFiles &= "ORDER BY ID "

        'Dim dtIndus As DataTable = New DataTable

        'dtIndus = fillWebSQL(qFiles, pFiles, "MB_COMP_PERSON_MAIN_INDUS")

        'If ds.Tables.Contains("MB_COMP_PERSON_MAIN_INDUS") = True Then
        '    ds.Tables("MB_COMP_PERSON_MAIN_INDUS").Clear()
        '    ds.Tables("MB_COMP_PERSON_MAIN_INDUS").Merge(dtIndus)
        '    ds.Tables("MB_COMP_PERSON_MAIN_INDUS").AcceptChanges()
        'Else
        '    ds.Tables.Add(dtIndus)

        '    'primary key
        '    ds.Tables("MB_COMP_PERSON_MAIN_INDUS").PrimaryKey = New DataColumn() {ds.Tables("MB_COMP_PERSON_MAIN_INDUS").Columns("MAIN_INDUSTRY_CODE")}

        '    DataGridView9.DataSource = ds.Tables("MB_COMP_PERSON_MAIN_INDUS")

        '    For i As Integer = 0 To DataGridView9.ColumnCount - 1
        '        DataGridView9.Columns(i).Visible = False
        '    Next
        '    'DataGridView6.Columns("DOC_TYPE").Visible = True
        '    DataGridView9.Columns("MAIN_INDUSTRY_CODE").Visible = True

        '    Dim comboBoxColumn As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
        '    comboBoxColumn.HeaderText = "MAIN_INDUSTRY_CODE"
        '    comboBoxColumn.DataPropertyName = "MAIN_INDUSTRY_CODE"
        '    comboBoxColumn.DataSource = ds.Tables("MB_MAIN_INDUSTRY")
        '    comboBoxColumn.ValueMember = ds.Tables("MB_MAIN_INDUSTRY").Columns(0).ColumnName
        '    comboBoxColumn.DisplayMember = ds.Tables("MB_MAIN_INDUSTRY").Columns(1).ColumnName

        '    Dim c As Integer = DataGridView9.Columns("MAIN_INDUSTRY_CODE").Index

        '    DataGridView9.Columns.RemoveAt(c)
        '    DataGridView9.Columns.Insert(c, comboBoxColumn)
        'End If

        'indus type
        Dim pIndus As New Dictionary(Of String, Object)
        pIndus.Add("@p0", COMP_PERSON_CODE)
        Dim qIndus As String = String.Empty
        qIndus &= "SELECT COMP_PERSON_CODE, MAIN_INDUSTRY_CODE, PRODUCT_SEQ, PRODUCT_CODE, LOGO_NAME_TH, LOGO_NAME_EN, PRODUCE_AMOUNT, EXPORT_AMOUNT, SIZE_DESC, IMPORT_FROM, EXPORT_TO, TYPE_WHOLESALER, TYPE_RETAILER FROM MB_COMP_PERSON_PRODUCT "
        qIndus &= "WHERE (COMP_PERSON_CODE = @p0) "
        qFiles &= "ORDER BY PRODUCT_SEQ "

        Dim dtIndus As DataTable = New DataTable

        dtIndus = fillWebSQL(qIndus, pIndus, "MB_COMP_PERSON_PRODUCT")

        If ds.Tables.Contains("MB_COMP_PERSON_PRODUCT") = True Then
            ds.Tables("MB_COMP_PERSON_PRODUCT").Clear()
            ds.Tables("MB_COMP_PERSON_PRODUCT").Merge(dtIndus)
            ds.Tables("MB_COMP_PERSON_PRODUCT").AcceptChanges()
        Else
            ds.Tables.Add(dtIndus)

            'primary key
            ds.Tables("MB_COMP_PERSON_PRODUCT").PrimaryKey = New DataColumn() {ds.Tables("MB_COMP_PERSON_PRODUCT").Columns("MAIN_INDUSTRY_CODE"), ds.Tables("MB_COMP_PERSON_PRODUCT").Columns("PRODUCT_SEQ")}

            DataGridView10.DataSource = ds.Tables("MB_COMP_PERSON_PRODUCT")

            For i As Integer = 0 To DataGridView10.ColumnCount - 1
                DataGridView10.Columns(i).Visible = False
            Next
            'DataGridView6.Columns("DOC_TYPE").Visible = True
            DataGridView10.Columns("PRODUCT_SEQ").Visible = True
            DataGridView10.Columns("LOGO_NAME_TH").Visible = True
            DataGridView10.Columns("LOGO_NAME_EN").Visible = True
            DataGridView10.Columns("PRODUCE_AMOUNT").Visible = True
            DataGridView10.Columns("EXPORT_AMOUNT").Visible = True
            DataGridView10.Columns("SIZE_DESC").Visible = True
            DataGridView10.Columns("IMPORT_FROM").Visible = True
            DataGridView10.Columns("EXPORT_TO").Visible = True
            DataGridView10.Columns("TYPE_WHOLESALER").Visible = True
            DataGridView10.Columns("TYPE_RETAILER").Visible = True

            Dim comboBoxIndusTH As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
            comboBoxIndusTH.HeaderText = "MAIN_INDUSTRY_DESC_TH"
            comboBoxIndusTH.DataPropertyName = "MAIN_INDUSTRY_CODE"
            comboBoxIndusTH.DataSource = ds.Tables("MB_MAIN_INDUSTRY")
            comboBoxIndusTH.ValueMember = ds.Tables("MB_MAIN_INDUSTRY").Columns("MAIN_INDUSTRY_CODE").ColumnName
            comboBoxIndusTH.DisplayMember = ds.Tables("MB_MAIN_INDUSTRY").Columns("MAIN_INDUSTRY_DESC_TH").ColumnName
            comboBoxIndusTH.Width = 250

            Dim cTH As Integer = DataGridView10.Columns("MAIN_INDUSTRY_CODE").Index

            DataGridView10.Columns.RemoveAt(cTH)
            DataGridView10.Columns.Insert(cTH, comboBoxIndusTH)

            Dim comboBoxIndusEN As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
            comboBoxIndusEN.HeaderText = "MAIN_INDUSTRY_DESC_EN"
            comboBoxIndusEN.DataPropertyName = "MAIN_INDUSTRY_CODE"
            comboBoxIndusEN.DataSource = ds.Tables("MB_MAIN_INDUSTRY")
            comboBoxIndusEN.ValueMember = ds.Tables("MB_MAIN_INDUSTRY").Columns("MAIN_INDUSTRY_CODE").ColumnName
            comboBoxIndusEN.DisplayMember = ds.Tables("MB_MAIN_INDUSTRY").Columns("MAIN_INDUSTRY_DESC_EN").ColumnName
            comboBoxIndusEN.Width = 250

            Dim cEN As Integer = comboBoxIndusTH.Index + 1

            'DataGridView10.Columns.RemoveAt(c)
            DataGridView10.Columns.Insert(cEN, comboBoxIndusEN)

            Dim comboBoxProdTH As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
            comboBoxProdTH.HeaderText = "PRODUCT_DESC_TH"
            comboBoxProdTH.DataPropertyName = "PRODUCT_CODE"
            comboBoxProdTH.DataSource = ds.Tables("MB_PRODUCT")
            comboBoxProdTH.ValueMember = ds.Tables("MB_PRODUCT").Columns("PRODUCT_CODE").ColumnName
            comboBoxProdTH.DisplayMember = ds.Tables("MB_PRODUCT").Columns("PRODUCT_DESC_TH").ColumnName
            comboBoxProdTH.Width = 250

            Dim coTH As Integer = DataGridView10.Columns("PRODUCT_CODE").Index

            DataGridView10.Columns.RemoveAt(coTH)
            DataGridView10.Columns.Insert(coTH, comboBoxProdTH)

            Dim comboBoxProdEN As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
            comboBoxProdEN.HeaderText = "PRODUCT_DESC_EN"
            comboBoxProdEN.DataPropertyName = "PRODUCT_CODE"
            comboBoxProdEN.DataSource = ds.Tables("MB_PRODUCT")
            comboBoxProdEN.ValueMember = ds.Tables("MB_PRODUCT").Columns("PRODUCT_CODE").ColumnName
            comboBoxProdEN.DisplayMember = ds.Tables("MB_PRODUCT").Columns("PRODUCT_DESC_EN").ColumnName
            comboBoxProdEN.Width = 250

            Dim coEN As Integer = comboBoxProdTH.Index + 1

            DataGridView10.Columns.RemoveAt(coEN)
            DataGridView10.Columns.Insert(coEN, comboBoxProdEN)
        End If
        If DataGridView10.Columns.Contains("COMP_PERSON_CODE") = True Then DataGridView10.Columns("COMP_PERSON_CODE").Visible = False

        'iso
        Dim pISO As New Dictionary(Of String, Object)
        pISO.Add("@p0", COMP_PERSON_CODE)
        Dim qISO As String = String.Empty
        qISO &= "SELECT COMP_PERSON_CODE, ISO_SEQ, ISO_CODE FROM MB_COMP_PERSON_ISO "
        qISO &= "WHERE (COMP_PERSON_CODE = @p0) "
        qISO &= "ORDER BY ISO_SEQ "

        Dim dtISO As DataTable = New DataTable

        dtISO = fillWebSQL(qISO, pISO, "MB_COMP_PERSON_ISO")

        If ds.Tables.Contains("MB_COMP_PERSON_ISO") = True Then
            ds.Tables("MB_COMP_PERSON_ISO").Clear()
            ds.Tables("MB_COMP_PERSON_ISO").Merge(dtISO)
            ds.Tables("MB_COMP_PERSON_ISO").AcceptChanges()
        Else
            ds.Tables.Add(dtISO)

            'primary key
            ds.Tables("MB_COMP_PERSON_ISO").PrimaryKey = New DataColumn() {ds.Tables("MB_COMP_PERSON_ISO").Columns("ISO_SEQ")}

            DataGridView9.DataSource = ds.Tables("MB_COMP_PERSON_ISO")

            For i As Integer = 0 To DataGridView9.ColumnCount - 1
                DataGridView9.Columns(i).Visible = False
            Next
            DataGridView9.Columns("ISO_SEQ").Visible = True
            'DataGridView10.Columns("LOGO_NAME_TH").Visible = True
            'DataGridView10.Columns("LOGO_NAME_EN").Visible = True
            'DataGridView10.Columns("PRODUCE_AMOUNT").Visible = True
            'DataGridView10.Columns("EXPORT_AMOUNT").Visible = True
            'DataGridView10.Columns("SIZE_DESC").Visible = True
            'DataGridView10.Columns("IMPORT_FROM").Visible = True
            'DataGridView10.Columns("EXPORT_TO").Visible = True
            'DataGridView10.Columns("TYPE_WHOLESALER").Visible = True
            'DataGridView10.Columns("TYPE_RETAILER").Visible = True

            Dim comboBoxIndusTH As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
            comboBoxIndusTH.HeaderText = "ISO_DESC_TH"
            comboBoxIndusTH.DataPropertyName = "ISO_CODE"
            comboBoxIndusTH.DataSource = ds.Tables("MB_ISO")
            comboBoxIndusTH.ValueMember = ds.Tables("MB_ISO").Columns("ISO_CODE").ColumnName
            comboBoxIndusTH.DisplayMember = ds.Tables("MB_ISO").Columns("ISO_DESC_TH").ColumnName
            comboBoxIndusTH.Width = 250

            Dim cTH As Integer = DataGridView9.Columns("ISO_CODE").Index

            DataGridView9.Columns.RemoveAt(cTH)
            DataGridView9.Columns.Insert(cTH, comboBoxIndusTH)

            Dim comboBoxIndusEN As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
            comboBoxIndusEN.HeaderText = "ISO_DESC_EN"
            comboBoxIndusEN.DataPropertyName = "ISO_CODE"
            comboBoxIndusEN.DataSource = ds.Tables("MB_ISO")
            comboBoxIndusEN.ValueMember = ds.Tables("MB_ISO").Columns("ISO_CODE").ColumnName
            comboBoxIndusEN.DisplayMember = ds.Tables("MB_ISO").Columns("ISO_DESC_EN").ColumnName
            comboBoxIndusEN.Width = 250

            Dim cEN As Integer = comboBoxIndusTH.Index + 1

            'DataGridView9.Columns.RemoveAt(cEN)
            DataGridView9.Columns.Insert(cEN, comboBoxIndusEN)
        End If
        If DataGridView9.Columns.Contains("COMP_PERSON_CODE") = True Then DataGridView9.Columns("COMP_PERSON_CODE").Visible = False

        'logs'
        getLOGS(CInt(NumericUpDown9.Value))
        'Dim pLogs As New Dictionary(Of String, Object)
        'pLogs.Add("@p0", row.Item("REGIST_CODE"))
        'Dim qLogs As String = String.Empty
        'qLogs &= "SELECT TOP " & NumericUpDown9.Value & " * FROM MB_LOGS "
        'qLogs &= "WHERE (REGIST_CODE = @p0) "
        'qLogs &= "ORDER BY ID DESC "

        'Dim dtLogs As DataTable = New DataTable

        'dtLogs = fillWebSQL(qLogs, pLogs, "MB_LOGS")

        'If ds.Tables.Contains("MB_LOGS") = True Then
        '    ds.Tables("MB_LOGS").Clear()
        '    ds.Tables("MB_LOGS").Merge(dtLogs)
        '    ds.Tables("MB_LOGS").AcceptChanges()
        'Else
        '    ds.Tables.Add(dtLogs)

        '    'primary key
        '    ds.Tables("MB_LOGS").PrimaryKey = New DataColumn() {ds.Tables("MB_LOGS").Columns("ID")}

        '    DataGridView4.DataSource = ds.Tables("MB_LOGS")
        'End If

        If DataGridView2.Columns.Contains("OU_CODE") = True Then DataGridView2.Columns("OU_CODE").Visible = False
        If DataGridView5.Columns.Contains("OU_CODE") = True Then DataGridView5.Columns("OU_CODE").Visible = False
    End Sub

    'Private Sub DataGridView6_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView6.CellContentDoubleClick
    '    If DataGridView6.CurrentRow IsNot Nothing Then
    '        openFile(CInt(DataGridView6.CurrentRow.Cells("ID").Value), DataGridView6.CurrentRow.Cells("FILE_NAME").Value.ToString)
    '    End If
    'End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles btNew.Click
        'new apply
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim running As Integer = 0
        Try
            'running = CInt(client.ExecuteScalar(query, parameters))
            running = CInt(getParameters(3, "INI_RUNNING_REGIST"))
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=getParameters=FTI_RUNNING_REGIST")
        End Try

        Dim d As DateTime = getSQLDate()
        Dim REGIST_CODE As String = "I" & d.ToString("yyMM", ciTH) & running.ToString("00000")

        'update MB_PARAMETERS
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Clear()
        'parameters.Add("@p0", running + 1)
        'parameters.Add("@p1", "FTI_RUNNING_REGIST")

        Dim query As String = String.Empty
        'query = String.Empty
        query = "UPDATE MB_PARAMETERS SET OBJ_VALUE = '" & running + 1 & "' WHERE OBJ_MODULE = 3 AND OBJ_NAME = 'INI_RUNNING_REGIST'"

        Dim result As Integer = 0
        Try
            result = executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        'MessageBox.Show("UPDATE MB_PARAMETERS:" & result & " " & running + 1 & vbCrLf & query)

        'MB_MEMBER
        parameters.Clear()
        parameters.Add("@p0", "001")
        parameters.Add("@p1", "131-02")
        parameters.Add("@p2", REGIST_CODE)
        parameters.Add("@p3", INI_CODE)
        parameters.Add("@p4", INI_CODE)
        parameters.Add("@p5", "10")
        parameters.Add("@p6", "11")
        parameters.Add("@p7", d)
        parameters.Add("@p8", user_name)
        parameters.Add("@p9", d)
        parameters.Add("@p10", "A")
        parameters.Add("@p11", REGIST_CODE)
        parameters.Add("@p12", "N")
        'parameters.Add("@p13", MEMBER_SHORT_NAME & (member_running + 1))
        parameters.Add("@p13", "<AUTO>")
        parameters.Add("@p14", "")

        'If CheckBox1.Checked = True Then
        '    parameters.Add("@p14", ComboBox2.SelectedValue)
        'Else
        '    parameters.Add("@p14", "")
        'End If

        query = String.Empty
        query = "INSERT INTO MB_MEMBER (OU_CODE, DIV_CODE, REGIST_CODE, MEMBER_MAIN_GROUP_CODE, MEMBER_GROUP_CODE, MEMBER_MAIN_TYPE_CODE, MEMBER_TYPE_CODE, REGIST_DATE, CR_BY, CR_DATE, MEMBER_STATUS_CODE, COMP_PERSON_CODE, STATUS_TYPE, MEMBER_CODE, ADVISOR_CODE) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14)"

        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        'MB_COMP_PERSON
        parameters.Clear()
        parameters.Add("@p0", "001")
        parameters.Add("@p1", REGIST_CODE)
        parameters.Add("@p2", "001")
        parameters.Add("@p3", "COMPANY_IN_THAI")
        parameters.Add("@p4", "COMPANY_IN_ENGLISH")
        parameters.Add("@p5", user_name)
        parameters.Add("@p6", d)

        query = String.Empty
        query = "INSERT INTO MB_COMP_PERSON (OU_CODE, COMP_PERSON_CODE, COMP_PERSON_TYPE_CODE, COMP_PERSON_NAME_TH, COMP_PERSON_NAME_EN, CR_BY, CR_DATE) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6)"

        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        tbREGIST_CODE.Text = REGIST_CODE

        'refresh grid
        getMEMBER(REGIST_CODE)
        'DataGridView1.Rows(0).Selected = True
        gridControl1_CurrentRowChanged(sender, e)

        MessageBox.Show("สร้าง " & REGIST_CODE & " สำเร็จ.")
    End Sub

    Private Sub btSaveGeneral_Click(sender As Object, e As EventArgs) Handles btSaveGeneral.Click
        'save general
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            bsMain.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btSaveGeneral.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'MessageBox.Show(ds.Tables("MB_MEMBER").GetChanges.Rows(0).Item("ROWID").ToString & " " & DataGridView1.CurrentRow.Cells("ROWID").Value.ToString)
                Dim row As DataRow = ds.Tables("MB_MEMBER").Rows.Find(DataGridView1.CurrentRow.Cells("ROWID").Value)

                'dates
                If row.Item("REGIST_DATE").ToString.Length > 0 And IsDate(MaskedTextBox1.Text) = True Then
                    If CDate(row.Item("REGIST_DATE").ToString).ToString("dd/MM/yyyy", ciTH) <> MaskedTextBox1.Text Then row.Item("REGIST_DATE") = CDate(MaskedTextBox1.Text)
                End If
                If row.Item("MEMBER_DATE").ToString.Length > 0 And IsDate(MaskedTextBox2.Text) = True Then
                    If CDate(row.Item("MEMBER_DATE").ToString).ToString("dd/MM/yyyy", ciTH) <> MaskedTextBox2.Text Then row.Item("REGIST_DATE") = CDate(MaskedTextBox2.Text)
                End If
                If row.Item("RETIRE_DATE").ToString.Length > 0 And IsDate(MaskedTextBox3.Text) = True Then
                    If CDate(row.Item("RETIRE_DATE").ToString).ToString("dd/MM/yyyy", ciTH) <> MaskedTextBox3.Text Then row.Item("REGIST_DATE") = CDate(MaskedTextBox3.Text)
                End If
                If row.Item("APPROVE_RETIRE_DATE").ToString.Length > 0 And IsDate(MaskedTextBox4.Text) = True Then
                    If CDate(row.Item("APPROVE_RETIRE_DATE").ToString).ToString("dd/MM/yyyy", ciTH) <> MaskedTextBox4.Text Then row.Item("REGIST_DATE") = CDate(MaskedTextBox4.Text)
                End If

                Dim query As String = String.Empty
                Dim parameters As New Dictionary(Of String, Object)


                'MessageBox.Show(ds.Tables("MB_MEMBER").GetChanges.Rows(0).Item("REGIST_CODE").ToString & " " & row("REGIST_CODE").ToString)

                'Dim cols() As String = "REGIST_CAPITAL,VENTURE_CAP_THA,VENTURE_CAP_ENG,ASSET_AMOUNT,EMPLOYEE_AMOUNT,BUS_TYPE_RELATE,BUS_TYPE_DEALER,BUS_TYPE_IMPORTER,BUS_TYPE_EXPORTER,BUS_TYPE_OTHER,MAIN_PRODUCTS_SERVICES,PRODUCE_AMOUNT,SALE_DOMESTIC_PERCENT,SALE_INTERNATIONAL_PERCENT,INCOME_PER_YEAR".Split(","c)
                'For i As Integer = 0 To ds.Tables("MB_MEMBER").Columns.Count - 1
                '    'MessageBox.Show(row.HasVersion(DataRowVersion.Current).ToString)

                '    If row.HasVersion(DataRowVersion.Current) Then
                '        If row(i, DataRowVersion.Current) Is row(i, DataRowVersion.Current) Then
                '            'Console.WriteLine("The original and the proposed are the same")
                '            'row.CancelEdit()
                '            'do nothing
                '        Else
                '            MessageBox.Show("YES")
                '            'save it
                '            parameters("@p2") = ds.Tables("MB_MEMBER").Columns(i).ColumnName
                '            parameters("@p5") = If(IsDBNull(row(i, DataRowVersion.Original)), "", row(i, DataRowVersion.Original))
                '            parameters("@p6") = If(IsDBNull(row(i, DataRowVersion.Current)), "", row(i, DataRowVersion.Current))
                '            Try
                '                executeWebSQL(query, parameters)
                '            Catch ex As Exception
                '                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                '            End Try

                '            'row.AcceptChanges()
                '        End If
                '    End If
                'Next


                parameters.Clear()
                parameters.Add("@p0", If(IsDBNull(row("ADVISOR_CHECKED")), False, row("ADVISOR_CHECKED")))
                parameters.Add("@p1", If(IsDBNull(row("ADVISOR_CODE")), "", row("ADVISOR_CODE")))
                parameters.Add("@p", row("ROWID"))

                'save MB_MEMBERS

                'query = String.Empty
                query = "UPDATE MB_MEMBER SET ADVISOR_CHECKED = @p0, ADVISOR_CODE = @p1 WHERE ROWID = @p"

                Dim result As Integer = 0
                Try
                    result = executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save MB_COMP_PERSON
                parameters.Clear()
                parameters.Add("@p0", If(IsDBNull(row("REGIST_CAPITAL")), 0, row("REGIST_CAPITAL")))
                parameters.Add("@p1", If(IsDBNull(row("VENTURE_CAP_THA")), 0, row("VENTURE_CAP_THA")))
                parameters.Add("@p2", If(IsDBNull(row("VENTURE_CAP_ENG")), 0, row("VENTURE_CAP_ENG")))
                parameters.Add("@p3", If(IsDBNull(row("ASSET_AMOUNT")), 0, row("ASSET_AMOUNT")))
                parameters.Add("@p4", If(IsDBNull(row("EMPLOYEE_AMOUNT")), 0, row("EMPLOYEE_AMOUNT")))
                parameters.Add("@p5", If(IsDBNull(row("BUS_TYPE_RELATE")), False, row("BUS_TYPE_RELATE")))
                parameters.Add("@p6", If(IsDBNull(row("BUS_TYPE_DEALER")), False, row("BUS_TYPE_DEALER")))
                parameters.Add("@p7", If(IsDBNull(row("BUS_TYPE_IMPORTER")), False, row("BUS_TYPE_IMPORTER")))
                parameters.Add("@p8", If(IsDBNull(row("BUS_TYPE_EXPORTER")), False, row("BUS_TYPE_EXPORTER")))
                parameters.Add("@p9", If(IsDBNull(row("BUS_TYPE_OTHER")), "", row("BUS_TYPE_OTHER")))
                parameters.Add("@p10", If(IsDBNull(row("MAIN_PRODUCTS_SERVICES")), "", row("MAIN_PRODUCTS_SERVICES")))
                parameters.Add("@p11", If(IsDBNull(row("PRODUCE_AMOUNT")), 0, row("PRODUCE_AMOUNT")))
                parameters.Add("@p12", If(IsDBNull(row("SALE_DOMESTIC_PERCENT")), 0, row("SALE_DOMESTIC_PERCENT")))
                parameters.Add("@p13", If(IsDBNull(row("SALE_INTERNATIONAL_PERCENT")), 0, row("SALE_INTERNATIONAL_PERCENT")))
                parameters.Add("@p14", If(IsDBNull(row("INCOME_PER_YEAR")), 0, row("INCOME_PER_YEAR")))
                parameters.Add("@p", row("COMP_PERSON_CODE"))

                query = String.Empty
                query = "UPDATE MB_COMP_PERSON SET REGIST_CAPITAL = @p0, VENTURE_CAP_THA = @p1, VENTURE_CAP_ENG = @p2, ASSET_AMOUNT = @p3, EMPLOYEE_AMOUNT = @p4, BUS_TYPE_RELATE = @p5, BUS_TYPE_DEALER = @p6, BUS_TYPE_IMPORTER = @p7, BUS_TYPE_EXPORTER = @p8, BUS_TYPE_OTHER = @p9, MAIN_PRODUCTS_SERVICES = @p10, PRODUCE_AMOUNT = @p11, SALE_DOMESTIC_PERCENT = @p12, SALE_INTERNATIONAL_PERCENT = @p13, INCOME_PER_YEAR = @p14 WHERE COMP_PERSON_CODE = @p"

                result = 0
                Try
                    result = executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'MessageBox.Show(result & " " & row("REGIST_CAPITAL").ToString & " " & DataGridView1.CurrentRow.Cells("ROWID").Value.ToString)

                'save history
                query = String.Empty
                query = "INSERT INTO MB_LOGS (REGIST_CODE, TABLE_NAME, COLUMN_NAME, MODIFY_TYPE, MODIFY_DATE, OLD_DATA, NEW_DATA, USER_BY) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7)"

                parameters.Clear()
                parameters.Add("@p0", row("REGIST_CODE"))
                parameters.Add("@p1", "FTI_GENERAL")
                parameters.Add("@p2", DBNull.Value)
                parameters.Add("@p3", "UPDATE")
                parameters.Add("@p4", getSQLDate())
                parameters.Add("@p5", DBNull.Value)
                parameters.Add("@p6", DBNull.Value)
                parameters.Add("@p7", user_name)

                'For Each dr As DataRow In ds.Tables("MB_MEMBER").Rows
                '    If dr.RowState = DataRowState.Modified Then

                '    End If
                'Next

                Dim cols() As String = "REGIST_CAPITAL,VENTURE_CAP_THA,VENTURE_CAP_ENG,ASSET_AMOUNT,EMPLOYEE_AMOUNT,BUS_TYPE_RELATE,BUS_TYPE_DEALER,BUS_TYPE_IMPORTER,BUS_TYPE_EXPORTER,BUS_TYPE_OTHER,MAIN_PRODUCTS_SERVICES,PRODUCE_AMOUNT,SALE_DOMESTIC_PERCENT,SALE_INTERNATIONAL_PERCENT,INCOME_PER_YEAR,PRODUCE_TECHNOLOGY_TH,PRODUCE_TECHNOLOGY_EN".Split(","c)

                For Each dc As String In cols
                    If row(dc, DataRowVersion.Original).ToString <> row(dc, DataRowVersion.Current).ToString Then
                        'MessageBox.Show(dc.ColumnName)
                        'save it
                        parameters("@p2") = dc
                        parameters("@p5") = If(IsDBNull(row(dc, DataRowVersion.Original)), "", row(dc, DataRowVersion.Original))
                        parameters("@p6") = If(IsDBNull(row(dc, DataRowVersion.Current)), "", row(dc, DataRowVersion.Current))
                        Try
                            executeWebSQL(query, parameters)
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                        End Try
                    End If
                Next

                'save changed
                ds.Tables("MB_MEMBER").AcceptChanges()

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("บันทึกเสร็จสิ้น")
            End If


        End If
    End Sub

    Private Sub btSaveMain_Click(sender As Object, e As EventArgs) Handles btSaveMain.Click
        'save main
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            bsMain.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btSaveMain.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_MEMBER").Rows.Find(DataGridView1.CurrentRow.Cells("ROWID").Value)

                'TextBox21.DataBindings.Add("Text", bs, "MEMBER_CODE")
                'TextBox22.DataBindings.Add("Text", bs, "REGIST_CODE")
                'ComboBox13.DataBindings.Add("Text", bs, "MEMBER_STATUS_CODE")
                'TextBox2.DataBindings.Add("Text", bs, "COMP_PERSON_NAME_TH")
                'TextBox4.DataBindings.Add("Text", bs, "COMP_PERSON_NAME_EN")

                'ComboBox3.DataBindings.Add("SelectedValue", bs, "MEMBER_MAIN_GROUP_CODE")
                'ComboBox4.DataBindings.Add("SelectedValue", bs, "MEMBER_GROUP_CODE")
                'ComboBox14.DataBindings.Add("SelectedValue", bs, "MEMBER_MAIN_TYPE_CODE")
                'ComboBox15.DataBindings.Add("SelectedValue", bs, "MEMBER_TYPE_CODE")

                Dim parameters As New Dictionary(Of String, Object)
                'parameters.Clear()
                parameters.Add("@p0", row("MEMBER_STATUS_CODE"))
                parameters.Add("@p1", row("MEMBER_MAIN_GROUP_CODE"))
                parameters.Add("@p2", row("MEMBER_GROUP_CODE"))
                parameters.Add("@p3", row("MEMBER_MAIN_TYPE_CODE"))
                parameters.Add("@p4", row("MEMBER_TYPE_CODE"))
                parameters.Add("@p", row("ROWID"))

                'save MB_MEMBERS
                Dim query As String = String.Empty
                'query = String.Empty
                query = "UPDATE MB_MEMBER SET MEMBER_STATUS_CODE = @p0, MEMBER_MAIN_GROUP_CODE = @p1, MEMBER_GROUP_CODE = @p2, MEMBER_MAIN_TYPE_CODE = @p3, MEMBER_TYPE_CODE = @p4  WHERE ROWID = @p"

                Dim result As Integer = 0
                Try
                    result = executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save MB_COMP_PERSON
                parameters.Clear()
                parameters.Add("@p0", row("COMP_PERSON_NAME_TH"))
                parameters.Add("@p1", row("COMP_PERSON_NAME_EN"))
                parameters.Add("@p2", row("PREN_CODE"))
                parameters.Add("@p", row("COMP_PERSON_CODE"))

                query = String.Empty
                query = "UPDATE MB_COMP_PERSON SET COMP_PERSON_NAME_TH = @p0, COMP_PERSON_NAME_EN = @p1, PREN_CODE = @p2 WHERE COMP_PERSON_CODE = @p"

                result = 0
                Try
                    result = executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history
                query = String.Empty
                query = "INSERT INTO MB_LOGS (REGIST_CODE, TABLE_NAME, COLUMN_NAME, MODIFY_TYPE, MODIFY_DATE, OLD_DATA, NEW_DATA, USER_BY) VALUES (@p0,@p1,@p2,@p3,GETDATE(),@p4,@p5,@p6)"

                parameters.Clear()
                parameters.Add("@p0", row("REGIST_CODE"))
                parameters.Add("@p1", "FTI_MAIN")
                parameters.Add("@p2", DBNull.Value)
                parameters.Add("@p3", "UPDATE")
                parameters.Add("@p4", DBNull.Value)
                parameters.Add("@p5", DBNull.Value)
                parameters.Add("@p6", user_name)

                Dim cols() As String = "MEMBER_STATUS_CODE,MEMBER_MAIN_GROUP_CODE,MEMBER_GROUP_CODE,MEMBER_MAIN_TYPE_CODE,MEMBER_TYPE_CODE,COMP_PERSON_NAME_TH,COMP_PERSON_NAME_EN,PREN_CODE,TAX_ID,PRODUCE_TECHNOLOGY_TH,PRODUCE_TECHNOLOGY_EN,ELECTRIC_AMOUNT,PRODUCE_TECHNOLOGY_DESC".Split(","c)
                For Each dc As String In cols
                    If row(dc, DataRowVersion.Original).ToString <> row(dc, DataRowVersion.Current).ToString Then
                        'MessageBox.Show(dc.ColumnName)
                        'save it
                        parameters("@p2") = dc
                        parameters("@p4") = If(IsDBNull(row(dc, DataRowVersion.Original)), "", row(dc, DataRowVersion.Original))
                        parameters("@p5") = If(IsDBNull(row(dc, DataRowVersion.Current)), "", row(dc, DataRowVersion.Current))
                        Try
                            executeWebSQL(query, parameters)
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                        End Try
                    End If
                Next

                ds.Tables("MB_MEMBER").AcceptChanges()

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("บันทึกเสร็จสิ้น")
            End If


        End If
    End Sub

    Private Sub btAddressAdd_Click(sender As Object, e As EventArgs) Handles btAddressAdd.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If tbCOMP_PERSON_CODE.TextLength > 0 Then
            Dim f As New frmFTIAddressNew
            f.dt = ds.Tables("MB_ADDRESS_TYPE").Copy
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                Dim query As String = String.Empty

                Dim row As DataRow = ds.Tables("MB_MEMBER").Rows.Find(DataGridView1.CurrentRow.Cells("ROWID").Value)

                Dim parameters As New Dictionary(Of String, Object)
                'parameters.Clear()
                parameters.Add("@p0", row("COMP_PERSON_CODE"))
                parameters.Add("@p1", f.ComboBox1.SelectedValue)
                'parameters.Add("@p2", f.ComboBox2.Text)

                'save MB_MEMBERS
                'Dim query As String = String.Empty
                query = String.Empty
                query = "INSERT INTO MB_COMP_PERSON_ADDRESS (COMP_PERSON_CODE, ADDR_CODE) VALUES (@p0,@p1)"

                Dim result As Integer = 0
                Try
                    result = executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history
                query = String.Empty
                query = "INSERT INTO MB_LOGS (REGIST_CODE, TABLE_NAME, COLUMN_NAME, MODIFY_TYPE, MODIFY_DATE, OLD_DATA, NEW_DATA, USER_BY) VALUES (@p0,@p1,@p2,@p3,GETDATE(),@p4,@p5,@p6)"

                parameters.Clear()
                parameters.Add("@p0", row("REGIST_CODE"))
                parameters.Add("@p1", "FTI_ADDRESS")
                parameters.Add("@p2", "COMP_PERSON_CODE")
                parameters.Add("@p3", "ADD")
                parameters.Add("@p4", "")
                parameters.Add("@p5", "")
                parameters.Add("@p6", user_name)

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("เพิ่มที่อยู่เสร็จสิ้น")
            End If
            f.Dispose()
            f = Nothing
        Else
            MessageBox.Show("กรุณาเลือกสมาชิก")
        End If

    End Sub

    Private Sub btAddressDelete_Click(sender As Object, e As EventArgs) Handles btAddressDelete.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If DataGridView2.CurrentRow IsNot Nothing Then
            If MessageBox.Show("คุณยืนยันที่จะลบรายการ [" & TextBox5.Text & "]?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim query As String = String.Empty

                Dim row As DataRow = ds.Tables("MB_MEMBER").Rows.Find(DataGridView1.CurrentRow.Cells("ROWID").Value)

                Dim parameters As New Dictionary(Of String, Object)
                'parameters.Clear()
                parameters.Add("@p0", row("COMP_PERSON_CODE"))
                parameters.Add("@p1", ComboBox9.SelectedValue)
                'parameters.Add("@p2", ComboBox16.Text)

                'save MB_MEMBERS
                'Dim query As String = String.Empty
                query = String.Empty
                query = "DELETE FROM MB_COMP_PERSON_ADDRESS WHERE COMP_PERSON_CODE = @p0 AND ADDR_CODE = @p1)"

                Dim result As Integer = 0
                Try
                    result = executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history
                query = String.Empty
                query = "INSERT INTO MB_LOGS (REGIST_CODE, TABLE_NAME, COLUMN_NAME, MODIFY_TYPE, MODIFY_DATE, OLD_DATA, NEW_DATA, USER_BY) VALUES (@p0,@p1,@p2,@p3,GETDATE(),@p4,@p5,@p6)"

                parameters.Clear()
                parameters.Add("@p0", row("REGIST_CODE"))
                parameters.Add("@p1", "FTI_ADDRESS")
                parameters.Add("@p2", "COMP_PERSON_CODE")
                parameters.Add("@p3", "DELETE")
                parameters.Add("@p4", "")
                parameters.Add("@p5", "")
                parameters.Add("@p6", user_name)

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("ลบที่อยู่เสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btAddressSave_Click(sender As Object, e As EventArgs) Handles btAddressSave.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        'save current address
        If DataGridView2.CurrentRow IsNot Nothing Then
            DataGridView2.EndEdit()
            bsAddress.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btAddressSave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_COMP_PERSON_ADDRESS").Rows.Find(DataGridView2.CurrentRow.Cells("ROWID").Value)

                Dim parameters As New Dictionary(Of String, Object)

                'save MB_MEMBERS
                Dim query As String = String.Empty

                'save history
                query = String.Empty
                query = "INSERT INTO MB_LOGS (REGIST_CODE, TABLE_NAME, COLUMN_NAME, MODIFY_TYPE, MODIFY_DATE, OLD_DATA, NEW_DATA, USER_BY) VALUES (@p0,@p1,@p2,@p3,GETDATE(),@p4,@p5,@p6)"

                parameters.Clear()
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("REGIST_CODE").Value)
                parameters.Add("@p1", "FTI_GENERAL")
                parameters.Add("@p2", DBNull.Value)
                parameters.Add("@p3", "UPDATE")
                parameters.Add("@p4", DBNull.Value)
                parameters.Add("@p5", DBNull.Value)
                parameters.Add("@p6", user_name)

                Dim cols() As String = "ADDR_NO,ADDR_MOO,ADDR_SOI,ADDR_ROAD,ADDR_POSTCODE,ADDR_PROVINCE_CODE,ADDR_DISTRICT,ADDR_SUB_DISTRICT,ADDR_CODE,ADDR_LANG".Split(","c)
                For Each dc As String In cols
                    If row(dc, DataRowVersion.Original).ToString <> row(dc, DataRowVersion.Current).ToString Then
                        'MessageBox.Show(dc.ColumnName)
                        'save it
                        parameters("@p2") = dc
                        parameters("@p4") = If(IsDBNull(row(dc, DataRowVersion.Original)), "", row(dc, DataRowVersion.Original))
                        parameters("@p5") = If(IsDBNull(row(dc, DataRowVersion.Current)), "", row(dc, DataRowVersion.Current))
                        Try
                            executeWebSQL(query, parameters)
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                        End Try
                    End If
                Next

                'update it
                query = String.Empty

                Dim pAddress As New Dictionary(Of String, Object)
                pAddress.Add("@p0", row("COMP_PERSON_CODE"))

                query &= "SELECT * FROM MB_COMP_PERSON_ADDRESS "
                query &= "WHERE (COMP_PERSON_CODE= @p0) "
                query &= "ORDER BY ADDR_CODE, ADDR_LANG"

                If ds.Tables("MB_COMP_PERSON_ADDRESS").GetChanges IsNot Nothing Then
                    Try
                        updateWebSQL(query, pAddress, ds.Tables("MB_COMP_PERSON_ADDRESS"))
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

    Private Sub btLogTop_Click(sender As Object, e As EventArgs) Handles btLogTop.Click
        getLOGS(CInt(NumericUpDown9.Value), DateTimePicker1.Value, DateTimePicker2.Value)

        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        ''logs
        'If tbREGIST_CODE.TextLength > 0 Then
        '    Dim pLogs As New Dictionary(Of String, Object)
        '    pLogs.Add("@p0", tbREGIST_CODE.Text)
        '    Dim qLogs As String = String.Empty
        '    qLogs &= "SELECT TOP " & NumericUpDown9.Value & " * FROM MB_LOGS "
        '    qLogs &= "WHERE (REGIST_CODE = @p0) "
        '    qLogs &= "ORDER BY ID DESC "

        '    Dim dtLogs As DataTable = New DataTable

        '    dtLogs = fillWebSQL(qLogs, pLogs, "MB_LOGS")

        '    If ds.Tables.Contains("MB_LOGS") = True Then
        '        ds.Tables("MB_LOGS").Clear()
        '        ds.Tables("MB_LOGS").Merge(dtLogs)
        '        ds.Tables("MB_LOGS").AcceptChanges()
        '    Else
        '        ds.Tables.Add(dtLogs)

        '        'primary key
        '        ds.Tables("MB_LOGS").PrimaryKey = New DataColumn() {ds.Tables("MB_LOGS").Columns("ID")}

        '        DataGridView4.DataSource = ds.Tables("MB_LOGS")
        '    End If
        'End If

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.DoubleClick
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        'change photo
        If DataGridView1.CurrentRow IsNot Nothing And DataGridView5.CurrentRow IsNot Nothing Then
            If OpenImageFileDlg.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                PictureBox1.Image = GetImageThumbnail(OpenImageFileDlg.FileName, 120, 150)
            End If
        End If

        'load photo
        'Dim bytBLOBData() As Byte = DirectCast(DsMemberCtrl1.membersImage.Rows(0).Item("ImageData"), Byte())
        'Dim stmBLOBData As New System.IO.MemoryStream(bytBLOBData)
        'PictureBox1.Image = Image.FromStream(stmBLOBData)
    End Sub

    Private Sub btRepresentAddressSave_Click(sender As Object, e As EventArgs) Handles btRepresentAddressSave.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        'represent address save
        If DataGridView5.CurrentRow IsNot Nothing Then
            DataGridView5.EndEdit()
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
                    Dim row As DataRow = ds.Tables("MB_MEMBER_REPRESENT").Rows.Find(DataGridView5.CurrentRow.Cells("CONTACT_CODE").Value)

                    Dim parameters As New Dictionary(Of String, Object)

                    'save MB_MEMBERS
                    Dim query As String = String.Empty

                    'save history 
                    'query = String.Empty
                    'query = "INSERT INTO MB_LOGS (REGIST_CODE, TABLE_NAME, COLUMN_NAME, MODIFY_TYPE, MODIFY_DATE, OLD_DATA, NEW_DATA, USER_BY) VALUES (@p0,@p1,@p2,@p3,GETDATE(),@p4,@p5,@p6)"

                    'parameters.Clear()
                    'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("REGIST_CODE").Value)
                    'parameters.Add("@p1", "FTI_REPRESENT")
                    'parameters.Add("@p2", DBNull.Value)
                    'parameters.Add("@p3", "UPDATE")
                    'parameters.Add("@p4", DBNull.Value)
                    'parameters.Add("@p5", DBNull.Value)
                    'parameters.Add("@p6", user_name)

                    'Dim cols() As String = "CONTACT_FIRST_NAME_TH,CONTACT_LAST_NAME_TH,ADDR_NO_TH,ADDR_MOO_TH,ADDR_SOI_TH,ADDR_ROAD_TH,ADDR_SUB_DISTRICT_TH,ADDR_DISTRICT_TH,ADDR_PROVINCE_NAME_TH,CONTACT_FIRST_NAME_EN,CONTACT_LAST_NAME_EN,ADDR_NO_EN,ADDR_MOO_EN,ADDR_SOI_EN,ADDR_ROAD_EN,ADDR_SUB_DISTRICT_EN,ADDR_DISTRICT_EN,ADDR_PROVINCE_NAME_EN,ADDR_TELEPHONE,ADDR_FAX,ADDR_EMAIL,CONTACT_PRENAME_CODE,SEX,ADDR_POSTCODE,REPRESENT_SEQ,POSITION_CODE".Split(","c)
                    'For i As Integer = 0 To cols.Length - 1
                    '    If row.HasVersion(DataRowVersion.Current) = True Then
                    '        If row(cols(i), DataRowVersion.Original).ToString <> row(cols(i), DataRowVersion.Current).ToString Then
                    '            'save it
                    '            parameters("@p2") = cols(i)
                    '            parameters("@p4") = If(IsDBNull(row(cols(i), DataRowVersion.Original)), "", row(cols(i), DataRowVersion.Original))
                    '            parameters("@p5") = If(IsDBNull(row(cols(i), DataRowVersion.Current)), "", row(cols(i), DataRowVersion.Current))
                    '            Try
                    '                executeWebSQL(query, parameters)
                    '            Catch ex As Exception
                    '                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    '            End Try
                    '        End If
                    '    End If
                    'Next

                    'update it
                    'query = String.Empty

                    Dim cols() As String = "REPRESENT_SEQ,POSITION_CODE".Split(","c)
                    For Each dc As String In cols
                        If row(dc, DataRowVersion.Original).ToString <> row(dc, DataRowVersion.Current).ToString Then
                            'MessageBox.Show(dc.ColumnName)
                            'save it
                            saveLOGS(DataGridView1.CurrentRow.Cells("REGIST_CODE").Value.ToString, "MB_MEMBER_REPRESENT", dc, "UPDATE", If(IsDBNull(row(dc, DataRowVersion.Original)), "", row(dc, DataRowVersion.Original)), If(IsDBNull(row(dc, DataRowVersion.Current)), "", row(dc, DataRowVersion.Current)), user_name)
                        End If
                    Next

                    parameters.Clear()
                    parameters.Add("@p0", NumericUpDown10.Value)
                    parameters.Add("@p1", If(IsDBNull(row("POSITION_CODE")), "", row("POSITION_CODE")))
                    parameters.Add("@p2", DataGridView1.CurrentRow.Cells("REGIST_CODE").Value)
                    parameters.Add("@p3", row("REPRESENT_SEQ", DataRowVersion.Original))

                    'MB_MEMBER_REPRESENT
                    ',REPRESENT_SEQ,POSITION_CODE WHERE REGIST_CODE
                    query = "UPDATE MB_MEMBER_REPRESENT SET REPRESENT_SEQ = @p0, POSITION_CODE = @p1 WHERE REGIST_CODE = @p2 AND REPRESENT_SEQ = @p3"

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

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

                    parameters.Add("@p", DataGridView5.CurrentRow.Cells("CONTACT_CODE").Value)

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

                    'save history 2
                    cols = "CONTACT_FIRST_NAME_TH,CONTACT_LAST_NAME_TH,ADDR_NO_TH,ADDR_MOO_TH,ADDR_SOI_TH,ADDR_ROAD_TH,ADDR_SUB_DISTRICT_TH,ADDR_DISTRICT_TH,ADDR_PROVINCE_NAME_TH,CONTACT_FIRST_NAME_EN,CONTACT_LAST_NAME_EN,ADDR_NO_EN,ADDR_MOO_EN,ADDR_SOI_EN,ADDR_ROAD_EN,ADDR_SUB_DISTRICT_EN,ADDR_DISTRICT_EN,ADDR_PROVINCE_NAME_EN,ADDR_TELEPHONE,ADDR_FAX,ADDR_EMAIL,CONTACT_PRENAME_CODE,SEX,ADDR_POSTCODE,PERSONAL_ID".Split(","c)
                    For Each dc As String In cols
                        If row(dc, DataRowVersion.Original).ToString <> row(dc, DataRowVersion.Current).ToString Then
                            'MessageBox.Show(dc.ColumnName)
                            'save it
                            saveLOGS(DataGridView5.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT", dc, "UPDATE", If(IsDBNull(row(dc, DataRowVersion.Original)), "", row(dc, DataRowVersion.Original)), If(IsDBNull(row(dc, DataRowVersion.Current)), "", row(dc, DataRowVersion.Current)), user_name)
                        End If
                    Next

                    'refresh grid
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show("บันทึกเสร็จสิ้น")
                End If
            Else
                MessageBox.Show("กรุณาตรวจสอบกรรมการ วาระปัจจุบัน")

            End If
            '
        End If
    End Sub

    Private Sub btRepresentAdd_Click(sender As Object, e As EventArgs) Handles btRepresentAdd.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If tbCOMP_PERSON_CODE.TextLength > 0 Then
            'add represent
            Dim f As New frmMainContacts
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                'add it
                Dim REPRESENT_SEQ As Integer = 1
                For i As Integer = 0 To ds.Tables("MB_MEMBER_REPRESENT").Rows.Count - 1
                    If CInt(ds.Tables("MB_MEMBER_REPRESENT").Rows(i).Item("REPRESENT_SEQ")) = REPRESENT_SEQ Then
                        REPRESENT_SEQ += 1
                    ElseIf CInt(ds.Tables("MB_MEMBER_REPRESENT").Rows(i).Item("REPRESENT_SEQ")) > REPRESENT_SEQ Then
                        Exit For
                    End If
                Next

                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("REGIST_CODE").Value)
                parameters.Add("@p1", f.DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)
                parameters.Add("@p2", REPRESENT_SEQ)
                parameters.Add("@p3", "")
                Dim query As String = "INSERT INTO MB_MEMBER_REPRESENT (REGIST_CODE, CONTACT_CODE, REPRESENT_SEQ, POSITION_CODE) VALUES (@p0,@p1,@p2,@p3)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'logs
                saveLOGS(DataGridView1.CurrentRow.Cells("REGIST_CODE").Value.ToString, "MB_MEMBER_REPRESENT", "CONTACT_CODE", "ADD", "", f.DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, user_name)

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("เพิ่มผู้แทนเสร็จสิ้น")
            End If
            f.Dispose()
            f = Nothing
        Else
            MessageBox.Show("กรุณาเลือกสมาชิก")
        End If

    End Sub

    Private Sub btRepresentDelete_Click(sender As Object, e As EventArgs) Handles btRepresentDelete.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        'delete represent
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
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("REGIST_CODE").Value)
                parameters.Add("@p1", DataGridView5.CurrentRow.Cells("REPRESENT_SEQ").Value)
                Dim query As String = "DELETE FROM MB_MEMBER_REPRESENT WHERE REGIST_CODE = @p0 AND REPRESENT_SEQ = @p1 "

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'logs
                saveLOGS(DataGridView1.CurrentRow.Cells("REGIST_CODE").Value.ToString, "MB_MEMBER_REPRESENT", "CONTACT_CODE", "DELETE", "", DataGridView5.CurrentRow.Cells("REPRESENT_SEQ").Value.ToString, user_name)

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("ลบผู้แทนเสร็จสิ้น")
            End If
        Else
            MessageBox.Show("กรุณาตรวจสอบกรรมการ วาระปัจจุบัน")
        End If

    End Sub

    Private Sub btFileAdd_Click(sender As Object, e As EventArgs) Handles btFileAdd.Click
        If tbCOMP_PERSON_CODE.TextLength > 0 Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Dim f As New frmMainFileNew
            f.dt = ds.Tables("MB_MEMBER_FILES_TYPE").Copy
            f.MODULE_ID = 1
            f.DOC_SIZE_PARAMETER = "FTI_DOC_SIZE"
            f.DOC_FILTER_PARAMETER = "FTI_DOC_FILTER"
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                'read file
                Dim fsBLOBFile As New System.IO.FileStream(f.OpenPDFFileDialog.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)
                Dim bytBLOBData(Convert.ToInt32(fsBLOBFile.Length()) - 1) As Byte
                fsBLOBFile.Read(bytBLOBData, 0, bytBLOBData.Length)
                fsBLOBFile.Close()

                'generate parameter
                Dim param As New SqlClient.SqlParameter("@p5", SqlDbType.VarBinary, bytBLOBData.Length, ParameterDirection.Input, True, 0, 0, "FILE_DATA", DataRowVersion.Current, bytBLOBData)

                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", "1")
                parameters.Add("@p1", tbREGIST_CODE.Text)
                parameters.Add("@p2", f.ComboBox1.SelectedValue)
                parameters.Add("@p3", System.IO.Path.GetFileName(f.OpenPDFFileDialog.FileName))
                parameters.Add("@p4", System.IO.Path.GetFileName(f.OpenPDFFileDialog.FileName))
                parameters.Add("@p5", param.Value)

                Dim query As String = "INSERT INTO MB_MEMBER_FILES (CATEGORY, REGIST_CODE, DOC_TYPE, DOC_NAME, FILE_NAME, FILE_DATA) VALUES (@p0,@p1,@p2,@p3,@p4,@p5)"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'save history
                query = String.Empty
                query = "INSERT INTO MB_LOGS (REGIST_CODE, TABLE_NAME, COLUMN_NAME, MODIFY_TYPE, MODIFY_DATE, OLD_DATA, NEW_DATA, USER_BY) VALUES (@p0,@p1,@p2,@p3,GETDATE(),@p4,@p5,@p6)"

                parameters.Clear()
                parameters.Add("@p0", tbREGIST_CODE.Text)
                parameters.Add("@p1", "FTI_FILES")
                parameters.Add("@p2", "MB_MEMBER_FILES")
                parameters.Add("@p3", "ADD")
                parameters.Add("@p4", System.IO.Path.GetFileName(f.OpenPDFFileDialog.FileName))
                parameters.Add("@p5", "")
                parameters.Add("@p6", user_name)

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'refresh grid
                gridControl1_CurrentRowChanged(sender, e)

                MessageBox.Show("เพิ่มไฟล์เสร็จสิ้น")
            End If
            f.Dispose()
            f = Nothing
        Else
            MessageBox.Show("กรุณาเลือกสมาชิก")
        End If


    End Sub

    Private Sub btFileDelete_Click(sender As Object, e As EventArgs) Handles btFileDelete.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        'delete file
        If MessageBox.Show("ยืนยันที่จะลบไฟล์ " & DataGridView6.CurrentRow.Cells("FILE_NAME").Value.ToString & "?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", DataGridView6.CurrentRow.Cells("ID").Value)

            Dim query As String = "DELETE FROM MB_MEMBER_FILES WHERE ID = @p0 "

            Try
                executeWebSQL(query, parameters)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            End Try

            'save history
            query = String.Empty
            query = "INSERT INTO MB_LOGS (REGIST_CODE, TABLE_NAME, COLUMN_NAME, MODIFY_TYPE, MODIFY_DATE, OLD_DATA, NEW_DATA, USER_BY) VALUES (@p0,@p1,@p2,@p3,GETDATE(),@p4,@p5,@p6)"

            parameters.Clear()
            parameters.Add("@p0", tbREGIST_CODE.Text)
            parameters.Add("@p1", "FTI_FILES")
            parameters.Add("@p2", "MB_MEMBER_FILES")
            parameters.Add("@p3", "DELETE")
            parameters.Add("@p4", DataGridView6.CurrentRow.Cells("ID").Value)
            parameters.Add("@p5", "")
            parameters.Add("@p6", user_name)

            Try
                executeWebSQL(query, parameters)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            End Try

            'refresh grid
            gridControl1_CurrentRowChanged(sender, e)

            MessageBox.Show("เพิ่มไฟล์เสร็จสิ้น")
        End If

    End Sub

    Private Sub btFileOpen_Click(sender As Object, e As EventArgs) Handles btFileOpen.Click
        If DataGridView6.CurrentRow IsNot Nothing Then
            openFile(CInt(DataGridView6.CurrentRow.Cells("ID").Value), DataGridView6.CurrentRow.Cells("FILE_NAME").Value.ToString)
        End If
    End Sub

    Private Sub btFileSave_Click(sender As Object, e As EventArgs) Handles btFileSave.Click
        'save file details changed
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If DataGridView6.CurrentRow IsNot Nothing Then
            DataGridView6.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btFileSave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim parameters As New Dictionary(Of String, Object)

                'save MB_MEMBERS
                Dim query As String = String.Empty

                'save history
                query = String.Empty
                query = "INSERT INTO MB_LOGS (REGIST_CODE, TABLE_NAME, COLUMN_NAME, MODIFY_TYPE, MODIFY_DATE, OLD_DATA, NEW_DATA, USER_BY) VALUES (@p0,@p1,@p2,@p3,GETDATE(),@p4,@p5,@p6)"

                parameters.Clear()
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("REGIST_CODE").Value)
                parameters.Add("@p1", "FTI_GENERAL")
                parameters.Add("@p2", DBNull.Value)
                parameters.Add("@p3", "UPDATE")
                parameters.Add("@p4", DBNull.Value)
                parameters.Add("@p5", DBNull.Value)
                parameters.Add("@p6", user_name)

                Dim cols() As String = "ADDR_NO,ADDR_MOO,ADDR_SOI,ADDR_ROAD,ADDR_POSTCODE,ADDR_PROVINCE_CODE,ADDR_DISTRICT,ADDR_SUB_DISTRICT,ADDR_CODE,ADDR_LANG".Split(","c)

                For r As Integer = 0 To ds.Tables("MB_MEMBER_FILES").Rows.Count - 1
                    Dim row As DataRow = ds.Tables("MB_MEMBER_FILES").Rows(r)

                    For Each dc As String In cols
                        If row(dc, DataRowVersion.Original).ToString <> row(dc, DataRowVersion.Current).ToString Then
                            'MessageBox.Show(dc.ColumnName)
                            'save it
                            parameters("@p2") = dc
                            parameters("@p4") = If(IsDBNull(row(dc, DataRowVersion.Original)), "", row(dc, DataRowVersion.Original))
                            parameters("@p5") = If(IsDBNull(row(dc, DataRowVersion.Current)), "", row(dc, DataRowVersion.Current))
                            Try
                                executeWebSQL(query, parameters)
                            Catch ex As Exception
                                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                            End Try
                        End If
                    Next
                Next

                'update it
                query = String.Empty

                Dim pFiles As New Dictionary(Of String, Object)
                pFiles.Add("@p0", DataGridView1.CurrentRow.Cells("REGIST_CODE").Value)
                Dim qFiles As String = String.Empty
                qFiles &= "SELECT ID, CATEGORY, REGIST_CODE, DOC_TYPE, DOC_NAME, FILE_NAME FROM MB_MEMBER_FILES "
                qFiles &= "WHERE (REGIST_CODE = @p0) "
                qFiles &= "ORDER BY ID "

                If ds.Tables("MB_MEMBER_FILES").GetChanges IsNot Nothing Then
                    Try
                        updateWebSQL(qFiles, pFiles, ds.Tables("MB_MEMBER_FILES"))
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

    Private Sub btRepresentPhotoSave_Click(sender As Object, e As EventArgs) Handles btRepresentPhotoSave.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If OpenImageFileDlg.FileName.Length > 0 Then
            If MessageBox.Show("ยืนยันที่จะ" & btRepresentPhotoSave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'get exist image?
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView2.CurrentRow.Cells("CONTACT_CODE").Value)

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
                    parameters.Add("@p", DataGridView2.CurrentRow.Cells("CONTACT_CODE").Value)
                    query = "UPDATE MB_CONTACT_PICTURE SET PICTURE_DATA = @p0 WHERE CONTACT_CODE = @p"

                    result = 0
                    Try
                        result = executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history
                    saveLOGS(tbREGIST_CODE.Text, "MB_CONTACT_PICTURE", "PICTURE_DATA", "UPDATE", "", System.IO.Path.GetFileName(OpenImageFileDlg.FileName), user_name)
                Else
                    'add
                    'read file
                    Dim param As New SqlClient.SqlParameter("@p1", SqlDbType.VarBinary, bytBLOBData.Length, ParameterDirection.Input, True, 0, 0, "PICTURE_DATA", DataRowVersion.Current, bytBLOBData)

                    parameters.Clear()
                    parameters.Add("@p0", DataGridView2.CurrentRow.Cells("CONTACT_CODE").Value)
                    parameters.Add("@p1", param.Value)
                    query = "INSERT INTO MB_CONTACT_PICTURE(CONTACT_CODE, PICTURE_DATA) VALUES (@p0,@p1)"

                    result = 0
                    Try
                        result = executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history
                    saveLOGS(tbREGIST_CODE.Text, "MB_CONTACT_PICTURE", "PICTURE_DATA", "ADD", "", System.IO.Path.GetFileName(OpenImageFileDlg.FileName), user_name)
                End If

                OpenImageFileDlg.FileName = String.Empty


                'Dim cmdAddImg As New SqlClient.SqlCommand("INSERT INTO membersImage (CODE, ImageData) VALUES (@param0,@param1)", Sqlcn)
                'Dim bytBLOBData() As Byte = ThumbNailImage(System.Drawing.Image.FromFile(OpenFileDlg.FileName).GetThumbnailImage(iW, iH, Nothing, Nothing))

                'Dim prm1 As New SqlClient.SqlParameter("@param0", SqlDbType.NVarChar, 25, ParameterDirection.Input, True, 0, 0, "CODE", DataRowVersion.Current, tbCode.Text)
                'Dim prm2 As New SqlClient.SqlParameter("@param1", SqlDbType.VarBinary, bytBLOBData.Length, ParameterDirection.Input, True, 0, 0, "ImageData", DataRowVersion.Current, bytBLOBData)
                'cmdAddImg.Parameters.Add(prm1)
                'cmdAddImg.Parameters.Add(prm2)
                'exeCmd(cmdAddImg)
            End If
        End If
    End Sub

    Private Sub btLocations_Click(sender As Object, e As EventArgs) Handles btLocationsAddress.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If DataGridView2.CurrentRow IsNot Nothing Then
            Dim f As New frmMainLocations
            f.TextBox1.Text = TextBox37.Text
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                'If f.RadioButton1.Checked = True Then
                '    'th only
                '    TextBox34.Text = f.DataGridView1.CurrentRow.Cells("SUB_DISTRICT_NAME_TH").Value.ToString
                '    TextBox35.Text = f.DataGridView1.CurrentRow.Cells("DISTRICT_NAME_TH").Value.ToString
                '    TextBox36.Text = f.DataGridView1.CurrentRow.Cells("PROVINCE_NAME_TH").Value.ToString
                'Else
                '    'th en
                '    TextBox34.Text = f.DataGridView1.CurrentRow.Cells("SUB_DISTRICT_NAME_TH").Value.ToString
                '    TextBox35.Text = f.DataGridView1.CurrentRow.Cells("DISTRICT_NAME_TH").Value.ToString
                '    TextBox36.Text = f.DataGridView1.CurrentRow.Cells("PROVINCE_NAME_TH").Value.ToString

                '    TextBox41.Text = f.DataGridView1.CurrentRow.Cells("SUB_DISTRICT_NAME_EN").Value.ToString
                '    TextBox40.Text = f.DataGridView1.CurrentRow.Cells("DISTRICT_NAME_EN").Value.ToString
                '    TextBox39.Text = f.DataGridView1.CurrentRow.Cells("PROVINCE_NAME_EN").Value.ToString
                'End If

                'th en
                TextBox34.Text = f.DataGridView1.CurrentRow.Cells("SUB_DISTRICT_NAME_TH").Value.ToString
                TextBox35.Text = f.DataGridView1.CurrentRow.Cells("DISTRICT_NAME_TH").Value.ToString
                TextBox36.Text = f.DataGridView1.CurrentRow.Cells("PROVINCE_NAME_TH").Value.ToString

                TextBox41.Text = f.DataGridView1.CurrentRow.Cells("SUB_DISTRICT_NAME_EN").Value.ToString
                TextBox40.Text = f.DataGridView1.CurrentRow.Cells("DISTRICT_NAME_EN").Value.ToString
                TextBox39.Text = f.DataGridView1.CurrentRow.Cells("PROVINCE_NAME_EN").Value.ToString

                TextBox37.Text = f.DataGridView1.CurrentRow.Cells("POSTCODE").Value.ToString
            End If
            f.Dispose()
            f = Nothing
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btPositions.Click
        If DataGridView5.CurrentRow IsNot Nothing Then
            Dim f As New frmMainPositions
            'f.TextBox1.Text = TextBox18.Text
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                Dim row As DataRow = ds.Tables("MB_MEMBER_REPRESENT").Rows.Find(DataGridView5.CurrentRow.Cells("CONTACT_CODE").Value)
                row("POSITION_CODE") = f.DataGridView1.CurrentRow.Cells("POSITION_CODE").Value.ToString

                'If f.RadioButton1.Checked = True Then
                '    'th only
                '    TextBox16.Text = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_TH").Value.ToString

                '    row("POSITION_NAME_TH") = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_TH").Value.ToString
                'Else
                '    'th en
                '    TextBox16.Text = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_TH").Value.ToString
                '    TextBox23.Text = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_EN").Value.ToString

                '    row("POSITION_NAME_TH") = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_TH").Value.ToString
                '    row("POSITION_NAME_EN") = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_EN").Value.ToString
                'End If

                'th en
                TextBox16.Text = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_TH").Value.ToString
                TextBox23.Text = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_EN").Value.ToString

                row("POSITION_NAME_TH") = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_TH").Value.ToString
                row("POSITION_NAME_EN") = f.DataGridView1.CurrentRow.Cells("POSITION_NAME_EN").Value.ToString
            End If
            f.Dispose()
            f = Nothing
        End If
    End Sub

    Private Sub btLocationsRepresents_Click(sender As Object, e As EventArgs) Handles btLocationsRepresents.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If DataGridView5.CurrentRow IsNot Nothing Then
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

    Private Sub btFind_Click(sender As Object, e As EventArgs) Handles btFind.Click
        TextBox1_KeyUp(sender, Nothing)
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    Private Sub btBusAdd_Click(sender As Object, e As EventArgs) Handles btBusAdd.Click
        If tbCOMP_PERSON_CODE.TextLength > 0 Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Dim f As New frmMainBusType
            f.dt = ds.Tables("MB_BUSSINESS_TYPE").Copy
            f.COMP_PERSON_CODE = tbCOMP_PERSON_CODE.Text
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                'check existing
                Dim pRep As New Dictionary(Of String, Object)
                pRep.Add("@p0", tbCOMP_PERSON_CODE.Text)
                pRep.Add("@p1", f.tbBUSSINESS_TYPE_CODE.Text)

                Dim qRep As String = String.Empty
                qRep &= "SELECT COUNT(*) AS CNT * "
                qRep &= "FROM MB_COMP_PERSON_BUS_TYPE "
                qRep &= "WHERE (COMP_PERSON_CODE = @p0) AND (BUSSINESS_TYPE_CODE = @p1)"

                Dim CNT As Integer = 0

                Try
                    CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
                Catch ex As Exception
                    MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT MB_COMP_PERSON_BUS_TYPE")
                End Try

                If CNT = 0 Then
                    Dim parameters As New Dictionary(Of String, Object)
                    parameters.Add("@p0", tbCOMP_PERSON_CODE.Text)
                    parameters.Add("@p1", f.tbBUSSINESS_TYPE_CODE.Text)
                    'parameters.Add("@p2", f.ComboBox1.SelectedValue)
                    'parameters.Add("@p3", System.IO.Path.GetFileName(f.OpenPDFFileDialog.FileName))
                    'parameters.Add("@p4", System.IO.Path.GetFileName(f.OpenPDFFileDialog.FileName))
                    'parameters.Add("@p5", param.Value)

                    Dim query As String = "INSERT INTO MB_COMP_PERSON_BUS_TYPE (COMP_PERSON_CODE, BUSSINESS_TYPE_CODE) VALUES (@p0,@p1)"

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history
                    saveLOGS(DataGridView1.CurrentRow.Cells("REGIST_CODE").Value.ToString, "MB_COMP_PERSON_BUS_TYPE", "BUSSINESS_TYPE_CODE", "ADD", "", f.tbBUSSINESS_TYPE_CODE.Text, user_name)

                    'refresh grid
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show(btBusAdd.Text & "เสร็จสิ้น")
                Else
                    MessageBox.Show("พบ " & f.tbBUSSINESS_TYPE_CODE.Text & " ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
                End If


            End If
            f.Dispose()
            f = Nothing
        Else
            MessageBox.Show("กรุณาเลือกสมาชิก")
        End If


    End Sub

    Private Sub btBusDel_Click(sender As Object, e As EventArgs) Handles btBusDel.Click
        'delete file
        If MessageBox.Show("ยืนยันที่จะ" & btBusDel.Text & " " & DataGridView8.CurrentRow.Cells("BUSSINESS_TYPE_CODE").Value.ToString & "?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", DataGridView8.CurrentRow.Cells("COMP_PERSON_CODE").Value)
            parameters.Add("@p1", DataGridView8.CurrentRow.Cells("BUSSINESS_TYPE_CODE").Value)

            Dim query As String = "DELETE FROM MB_COMP_PERSON_BUS_TYPE WHERE (COMP_PERSON_CODE = @p0) AND (BUSSINESS_TYPE_CODE = @p1)"

            Try
                executeWebSQL(query, parameters)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            End Try

            'save history
            saveLOGS(DataGridView1.CurrentRow.Cells("REGIST_CODE").Value.ToString, "MB_COMP_PERSON_BUS_TYPE", "BUSSINESS_TYPE_CODE", "DEL", DataGridView8.CurrentRow.Cells("BUSSINESS_TYPE_CODE").Value, "", user_name)

            'refresh grid
            gridControl1_CurrentRowChanged(sender, e)

            MessageBox.Show(btBusDel.Text & "เสร็จสิ้น")
        End If
    End Sub

    Private Sub btBusSave_Click(sender As Object, e As EventArgs) Handles btBusSave.Click
        'save file details changed
        DataGridView8.EndEdit()

        If MessageBox.Show("ยืนยันที่จะ" & btBusSave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Dim parameters As New Dictionary(Of String, Object)

            'save MB_MEMBERS
            Dim query As String = String.Empty

            'save history
            query = String.Empty
            query = "INSERT INTO MB_LOGS (REGIST_CODE, TABLE_NAME, COLUMN_NAME, MODIFY_TYPE, MODIFY_DATE, OLD_DATA, NEW_DATA, USER_BY) VALUES (@p0,@p1,@p2,@p3,GETDATE(),@p4,@p5,@p6)"

            parameters.Clear()
            parameters.Add("@p0", DataGridView1.CurrentRow.Cells("REGIST_CODE").Value)
            parameters.Add("@p1", "MB_COMP_PERSON_BUS_TYPE")
            parameters.Add("@p2", DBNull.Value)
            parameters.Add("@p3", "UPDATE")
            parameters.Add("@p4", DBNull.Value)
            parameters.Add("@p5", DBNull.Value)
            parameters.Add("@p6", user_name)

            Dim cols() As String = "BUSSINESS_TYPE_CODE".Split(","c)

            For r As Integer = 0 To ds.Tables("MB_COMP_PERSON_BUS_TYPE").Rows.Count - 1
                Dim row As DataRow = ds.Tables("MB_COMP_PERSON_BUS_TYPE").Rows(r)

                For Each dc As String In cols
                    If row(dc, DataRowVersion.Original).ToString <> row(dc, DataRowVersion.Current).ToString Then
                        'MessageBox.Show(dc.ColumnName)
                        'save it
                        parameters("@p2") = dc
                        parameters("@p4") = If(IsDBNull(row(dc, DataRowVersion.Original)), "", row(dc, DataRowVersion.Original))
                        parameters("@p5") = If(IsDBNull(row(dc, DataRowVersion.Current)), "", row(dc, DataRowVersion.Current))
                        Try
                            executeWebSQL(query, parameters)
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                        End Try
                    End If
                Next
            Next

            'update it
            query = String.Empty

            parameters.Clear()
            parameters.Add("@p0", DataGridView1.CurrentRow.Cells("COMP_PERSON_CODE").Value)

            query &= "SELECT COMP_PERSON_CODE, BUSSINESS_TYPE_CODE FROM MB_COMP_PERSON_BUS_TYPE "
            query &= "WHERE (COMP_PERSON_CODE = @p0) "

            If ds.Tables("MB_COMP_PERSON_BUS_TYPE").GetChanges IsNot Nothing Then
                Try
                    updateWebSQL(query, parameters, ds.Tables("MB_COMP_PERSON_BUS_TYPE"))
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=updateWebSQL")
                End Try
            End If

            'refresh grid
            gridControl1_CurrentRowChanged(sender, e)

            MessageBox.Show("บันทึกเสร็จสิ้น")
        End If
    End Sub

    Private Sub btProdAdd_Click(sender As Object, e As EventArgs) Handles btProdAdd.Click
        If tbCOMP_PERSON_CODE.TextLength > 0 Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Dim f As New frmMainProducts
            If ds.Tables("MB_COMP_PERSON_PRODUCT").Rows.Count > 0 Then f.MAIN_INDUSTRY_CODE = ds.Tables("MB_COMP_PERSON_PRODUCT").Rows(ds.Tables("MB_COMP_PERSON_PRODUCT").Rows.Count - 1).Item("MAIN_INDUSTRY_CODE").ToString

            'f.dtMB_MAIN_INDUSTRY = ds.Tables("MB_MAIN_INDUSTRY").Copy
            'f.dtMB_PRODUCT = ds.Tables("MB_PRODUCT").Copy
            'f.COMP_PERSON_CODE = tbCOMP_PERSON_CODE.Text

            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                'check existing
                Dim pRep As New Dictionary(Of String, Object)
                pRep.Add("@p0", tbCOMP_PERSON_CODE.Text)
                pRep.Add("@p1", f.tbMAIN_INDUSTRY_CODE.Text)
                pRep.Add("@p2", f.tbPRODUCT_CODE.Text)

                Dim qRep As String = String.Empty
                qRep &= "SELECT COUNT(*) AS CNT * "
                qRep &= "FROM MB_COMP_PERSON_PRODUCT "
                qRep &= "WHERE (COMP_PERSON_CODE = @p0) AND (MAIN_INDUSTRY_CODE = @p1) AND (PRODUCT_CODE = @p2)"

                Dim CNT As Integer = 0

                Try
                    CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
                Catch ex As Exception
                    MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT MB_COMP_PERSON_PRODUCT")
                End Try

                If CNT = 0 Then
                    Dim REPRESENT_SEQ As Integer = 1
                    For i As Integer = 0 To ds.Tables("MB_COMP_PERSON_PRODUCT").Rows.Count - 1
                        If CInt(ds.Tables("MB_COMP_PERSON_PRODUCT").Rows(i).Item("PRODUCT_SEQ")) = REPRESENT_SEQ Then
                            REPRESENT_SEQ += 1
                        ElseIf CInt(ds.Tables("MB_COMP_PERSON_PRODUCT").Rows(i).Item("PRODUCT_SEQ")) > REPRESENT_SEQ Then
                            Exit For
                        End If
                    Next

                    'MessageBox.Show(REPRESENT_SEQ.ToString)

                    Dim parameters As New Dictionary(Of String, Object)
                    parameters.Add("@p0", tbCOMP_PERSON_CODE.Text)
                    parameters.Add("@p1", f.tbMAIN_INDUSTRY_CODE.Text)
                    parameters.Add("@p2", REPRESENT_SEQ)
                    parameters.Add("@p3", f.tbPRODUCT_CODE.Text)
                    'parameters.Add("@p4", System.IO.Path.GetFileName(f.OpenPDFFileDialog.FileName))
                    'parameters.Add("@p5", param.Value)

                    Dim query As String = "INSERT INTO MB_COMP_PERSON_PRODUCT (COMP_PERSON_CODE, MAIN_INDUSTRY_CODE, PRODUCT_SEQ, PRODUCT_CODE) VALUES (@p0,@p1,@p2,@p3)"

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history
                    saveLOGS(DataGridView1.CurrentRow.Cells("REGIST_CODE").Value.ToString, "MB_COMP_PERSON_PRODUCT", "PRODUCT_CODE", "ADD", "", f.tbPRODUCT_CODE.Text, user_name)

                    'refresh grid
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show(btProdAdd.Text & "เสร็จสิ้น")
                Else
                    MessageBox.Show("พบ " & f.tbMAIN_INDUSTRY_CODE.Text & " " & f.tbPRODUCT_CODE.Text & " ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
                End If


            End If
            f.Dispose()
            f = Nothing
        Else
            MessageBox.Show("กรุณาเลือกสมาชิก")
        End If
    End Sub

    Private Sub btProdDel_Click(sender As Object, e As EventArgs) Handles btProdDel.Click
        'delete file
        If MessageBox.Show("ยืนยันที่จะ" & btProdDel.Text & " " & DataGridView10.CurrentRow.Cells("PRODUCT_CODE").Value.ToString & "?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", DataGridView10.CurrentRow.Cells("COMP_PERSON_CODE").Value)
            parameters.Add("@p1", DataGridView10.CurrentRow.Cells("MAIN_INDUSTRY_CODE").Value)
            parameters.Add("@p2", DataGridView10.CurrentRow.Cells("PRODUCT_SEQ").Value)
            parameters.Add("@p3", DataGridView10.CurrentRow.Cells("PRODUCT_CODE").Value)

            Dim query As String = "DELETE FROM MB_COMP_PERSON_PRODUCT WHERE (COMP_PERSON_CODE = @p0) AND (MAIN_INDUSTRY_CODE = @p1) AND (PRODUCT_SEQ = @p2) AND (PRODUCT_CODE = @p3)"

            Try
                executeWebSQL(query, parameters)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            End Try

            'save history
            saveLOGS(DataGridView1.CurrentRow.Cells("REGIST_CODE").Value.ToString, "MB_COMP_PERSON_PRODUCT", "PRODUCT_CODE", "DEL", DataGridView10.CurrentRow.Cells("PRODUCT_CODE").Value, "", user_name)

            'refresh grid
            gridControl1_CurrentRowChanged(sender, e)

            MessageBox.Show(btBusDel.Text & "เสร็จสิ้น")
        End If
    End Sub

    Private Sub btProdSave_Click(sender As Object, e As EventArgs) Handles btProdSave.Click
        'save file details changed
        DataGridView10.EndEdit()

        If MessageBox.Show("ยืนยันที่จะ" & btProdSave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Dim parameters As New Dictionary(Of String, Object)

            'save MB_MEMBERS
            Dim query As String = String.Empty

            'save history
            query = String.Empty
            query = "INSERT INTO MB_LOGS (REGIST_CODE, TABLE_NAME, COLUMN_NAME, MODIFY_TYPE, MODIFY_DATE, OLD_DATA, NEW_DATA, USER_BY) VALUES (@p0,@p1,@p2,@p3,GETDATE(),@p4,@p5,@p6)"

            parameters.Clear()
            parameters.Add("@p0", DataGridView1.CurrentRow.Cells("REGIST_CODE").Value)
            parameters.Add("@p1", "MB_COMP_PERSON_BUS_TYPE")
            parameters.Add("@p2", DBNull.Value)
            parameters.Add("@p3", "UPDATE")
            parameters.Add("@p4", DBNull.Value)
            parameters.Add("@p5", DBNull.Value)
            parameters.Add("@p6", user_name)

            Dim cols() As String = "MAIN_INDUSTRY_CODE,PRODUCT_SEQ,PRODUCT_CODE,LOGO_NAME_TH,LOGO_NAME_EN,PRODUCT_PICTURE_PATH,PRODUCE_AMOUNT,EXPORT_AMOUNT,SIZE_DESC,IMPORT_FROM,EXPORT_TO,TYPE_WHOLESALER,TYPE_RETAILER".Split(","c)

            For r As Integer = 0 To ds.Tables("MB_COMP_PERSON_PRODUCT").Rows.Count - 1
                Dim row As DataRow = ds.Tables("MB_COMP_PERSON_PRODUCT").Rows(r)

                For Each dc As String In cols
                    If row(dc, DataRowVersion.Original).ToString <> row(dc, DataRowVersion.Current).ToString Then
                        'MessageBox.Show(dc.ColumnName)
                        'save it
                        parameters("@p2") = dc
                        parameters("@p4") = If(IsDBNull(row(dc, DataRowVersion.Original)), "", row(dc, DataRowVersion.Original))
                        parameters("@p5") = If(IsDBNull(row(dc, DataRowVersion.Current)), "", row(dc, DataRowVersion.Current))
                        Try
                            executeWebSQL(query, parameters)
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                        End Try
                    End If
                Next
            Next

            'update it
            query = String.Empty

            parameters.Add("@p0", DataGridView1.CurrentRow.Cells("COMP_PERSON_CODE").Value)

            query &= "SELECT COMP_PERSON_CODE, MAIN_INDUSTRY_CODE, PRODUCT_SEQ, PRODUCT_CODE, LOGO_NAME_TH, LOGO_NAME_EN, PRODUCE_AMOUNT, EXPORT_AMOUNT, SIZE_DESC, IMPORT_FROM, EXPORT_TO, TYPE_WHOLESALER, TYPE_RETAILER FROM MB_COMP_PERSON_PRODUCT "
            query &= "WHERE (COMP_PERSON_CODE = @p0) "
            query &= "ORDER BY PRODUCT_SEQ "

            If ds.Tables("MB_COMP_PERSON_PRODUCT").GetChanges IsNot Nothing Then
                Try
                    updateWebSQL(query, parameters, ds.Tables("MB_COMP_PERSON_PRODUCT"))
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=updateWebSQL")
                End Try
            End If

            'refresh grid
            gridControl1_CurrentRowChanged(sender, e)

            MessageBox.Show("บันทึกเสร็จสิ้น")
        End If
    End Sub

    Private Sub btISOadd_Click(sender As Object, e As EventArgs) Handles btISOadd.Click
        If tbCOMP_PERSON_CODE.TextLength > 0 Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Dim f As New frmMainISO
            'f.dt = ds.Tables("MB_ISO").Copy
            'f.COMP_PERSON_CODE = tbCOMP_PERSON_CODE.Text

            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                'check existing
                Dim pRep As New Dictionary(Of String, Object)
                pRep.Add("@p0", tbCOMP_PERSON_CODE.Text)
                pRep.Add("@p1", f.tbISO_CODE.Text)

                Dim qRep As String = String.Empty
                qRep &= "SELECT COUNT(*) AS CNT * "
                qRep &= "FROM MB_COMP_PERSON_ISO "
                qRep &= "WHERE (COMP_PERSON_CODE = @p0) AND (ISO_CODE = @p2)"

                Dim CNT As Integer = 0

                Try
                    CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
                Catch ex As Exception
                    MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT MB_COMP_PERSON_ISO")
                End Try

                If CNT = 0 Then
                    Dim REPRESENT_SEQ As Integer = 1
                    For i As Integer = 0 To ds.Tables("MB_COMP_PERSON_ISO").Rows.Count - 1
                        If CInt(ds.Tables("MB_COMP_PERSON_ISO").Rows(i).Item("ISO_SEQ")) = REPRESENT_SEQ Then
                            REPRESENT_SEQ += 1
                        ElseIf CInt(ds.Tables("MB_COMP_PERSON_ISO").Rows(i).Item("ISO_SEQ")) > REPRESENT_SEQ Then
                            Exit For
                        End If
                    Next

                    'MessageBox.Show(REPRESENT_SEQ.ToString)

                    Dim parameters As New Dictionary(Of String, Object)
                    parameters.Add("@p0", tbCOMP_PERSON_CODE.Text)
                    parameters.Add("@p1", REPRESENT_SEQ)
                    parameters.Add("@p2", f.tbISO_CODE.Text)
                    'parameters.Add("@p3", f.tbPRODUCT_CODE.Text)
                    'parameters.Add("@p4", System.IO.Path.GetFileName(f.OpenPDFFileDialog.FileName))
                    'parameters.Add("@p5", param.Value)

                    Dim query As String = "INSERT INTO MB_COMP_PERSON_ISO (COMP_PERSON_CODE, ISO_SEQ, ISO_CODE) VALUES (@p0,@p1,@p2)"

                    Try
                        executeWebSQL(query, parameters)
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    'save history
                    saveLOGS(DataGridView1.CurrentRow.Cells("REGIST_CODE").Value.ToString, "MB_COMP_PERSON_ISO", "ISO_CODE", "ADD", "", f.tbISO_CODE.Text, user_name)

                    'refresh grid
                    gridControl1_CurrentRowChanged(sender, e)

                    MessageBox.Show(btProdAdd.Text & "เสร็จสิ้น")
                Else
                    MessageBox.Show("พบ " & f.tbISO_CODE.Text & " ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
                End If


            End If
            f.Dispose()
            f = Nothing
        Else
            MessageBox.Show("กรุณาเลือกสมาชิก")
        End If
    End Sub

    Private Sub btISOdel_Click(sender As Object, e As EventArgs) Handles btISOdel.Click
        'delete file
        If MessageBox.Show("ยืนยันที่จะ" & btISOdel.Text & " " & DataGridView9.CurrentRow.Cells("ISO_CODE").Value.ToString & "?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", DataGridView9.CurrentRow.Cells("COMP_PERSON_CODE").Value)
            parameters.Add("@p1", DataGridView9.CurrentRow.Cells("ISO_SEQ").Value)
            parameters.Add("@p2", DataGridView9.CurrentRow.Cells("ISO_CODE").Value)
            'parameters.Add("@p3", DataGridView10.CurrentRow.Cells("PRODUCT_CODE").Value)

            Dim query As String = "DELETE FROM MB_COMP_PERSON_ISO WHERE (COMP_PERSON_CODE = @p0) AND (ISO_SEQ = @p1) AND (ISO_CODE = @p2)"

            Try
                executeWebSQL(query, parameters)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            End Try

            'save history
            saveLOGS(DataGridView1.CurrentRow.Cells("REGIST_CODE").Value.ToString, "MB_COMP_PERSON_ISO", "ISO_CODE", "DEL", DataGridView9.CurrentRow.Cells("ISO_CODE").Value, "", user_name)

            'refresh grid
            gridControl1_CurrentRowChanged(sender, e)

            MessageBox.Show(btBusDel.Text & "เสร็จสิ้น")
        End If
    End Sub

    Private Sub btISOsave_Click(sender As Object, e As EventArgs) Handles btISOsave.Click
        'save file details changed
        DataGridView9.EndEdit()

        If MessageBox.Show("ยืนยันที่จะ" & btISOsave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Dim parameters As New Dictionary(Of String, Object)

            'save MB_MEMBERS
            Dim query As String = String.Empty

            'save history
            query = String.Empty
            query = "INSERT INTO MB_LOGS (REGIST_CODE, TABLE_NAME, COLUMN_NAME, MODIFY_TYPE, MODIFY_DATE, OLD_DATA, NEW_DATA, USER_BY) VALUES (@p0,@p1,@p2,@p3,GETDATE(),@p4,@p5,@p6)"

            parameters.Clear()
            parameters.Add("@p0", DataGridView1.CurrentRow.Cells("REGIST_CODE").Value)
            parameters.Add("@p1", "MB_COMP_PERSON_ISO")
            parameters.Add("@p2", DBNull.Value)
            parameters.Add("@p3", "UPDATE")
            parameters.Add("@p4", DBNull.Value)
            parameters.Add("@p5", DBNull.Value)
            parameters.Add("@p6", user_name)

            Dim cols() As String = "ISO_SEQ, ISO_CODE".Split(","c)

            For r As Integer = 0 To ds.Tables("MB_COMP_PERSON_ISO").Rows.Count - 1
                Dim row As DataRow = ds.Tables("MB_COMP_PERSON_ISO").Rows(r)

                For Each dc As String In cols
                    If row(dc, DataRowVersion.Original).ToString <> row(dc, DataRowVersion.Current).ToString Then
                        'MessageBox.Show(dc.ColumnName)
                        'save it
                        parameters("@p2") = dc
                        parameters("@p4") = If(IsDBNull(row(dc, DataRowVersion.Original)), "", row(dc, DataRowVersion.Original))
                        parameters("@p5") = If(IsDBNull(row(dc, DataRowVersion.Current)), "", row(dc, DataRowVersion.Current))
                        Try
                            executeWebSQL(query, parameters)
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                        End Try
                    End If
                Next
            Next

            'update it
            query = String.Empty

            parameters.Add("@p0", DataGridView1.CurrentRow.Cells("COMP_PERSON_CODE").Value)

            query &= "SELECT COMP_PERSON_CODE, ISO_SEQ, ISO_CODE FROM MB_COMP_PERSON_ISO "
            query &= "WHERE (COMP_PERSON_CODE = @p0) "
            query &= "ORDER BY ISO_SEQ "

            If ds.Tables("MB_COMP_PERSON_ISO").GetChanges IsNot Nothing Then
                Try
                    updateWebSQL(query, parameters, ds.Tables("MB_COMP_PERSON_ISO"))
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=updateWebSQL")
                End Try
            End If

            'refresh grid
            gridControl1_CurrentRowChanged(sender, e)

            MessageBox.Show("บันทึกเสร็จสิ้น")
        End If
    End Sub

    Private Sub chkADVISOR_CheckedChanged(sender As Object, e As EventArgs) Handles chkADVISOR.CheckedChanged
        'advisor
        cbADVISOR.Visible = chkADVISOR.Checked
        tbADVISOR.Visible = chkADVISOR.Checked
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Dim SEARCH As String = "%" & TextBox3.Text.Replace(" ", "%") & "%"
        dvLOGS.RowFilter = String.Format("TABLE_NAME LIKE '{0}' OR COLUMN_NAME LIKE '{0}' OR OLD_DATA LIKE '{0}' OR NEW_DATA LIKE '{0}' OR USER_BY LIKE '{0}'", SEARCH)
    End Sub

    Private Sub btStatus_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btStatus_Click_1(sender As Object, e As EventArgs) Handles btSQL.Click
        Dim f As New frmINIsmes
        f.MODULE_ID = CInt(INI_CODE)
        f.Text = btSQL.Text
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btConsideration_Click(sender As Object, e As EventArgs) Handles btConsideration.Click
        If fFeeINI(INI_ID) Is Nothing Then
            fFeeINI(INI_ID) = New frmINIFees
            fFeeINI(INI_ID).MdiParent = Me.MdiParent
            fFeeINI(INI_ID).WindowState = FormWindowState.Maximized

            fFeeINI(INI_ID).INI_CODE = INI_CODE
            fFeeINI(INI_ID).INI_NAME = INI_NAME
            fFeeINI(INI_ID).INI_ID = INI_ID

            fFeeINI(INI_ID).Show()
        Else
            'focus opening form
            fFeeINI(INI_ID).Show()
            fFeeINI(INI_ID).Focus()
        End If
    End Sub

    Private Sub btReports_Click(sender As Object, e As EventArgs) Handles btReports.Click
        '
    End Sub

    Private Sub btStatus_Click_2(sender As Object, e As EventArgs) Handles btStatus.Click
        Dim f As New frmMainStatus
        f.MODULE_ID = CInt(INI_CODE)
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub
End Class
