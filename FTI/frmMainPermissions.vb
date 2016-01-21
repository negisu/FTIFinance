Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

Public Class frmMainPermissions
    'Friend MODULE_ID As Integer
    Dim ds As DataSet
    Dim bs As BindingSource
    Dim clipboard As String

    Private Sub frmFeesRule2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet

        Try
            getPARAMETERS()
        Catch ex As Exception
            MessageBox.Show("getMB_PERMISSIONS" & vbCrLf & ex.Message)
        End Try

        ds.Tables("MB_PERMISSIONS").PrimaryKey = New DataColumn() {ds.Tables("MB_PERMISSIONS").Columns("FORM_NAME"), ds.Tables("MB_PERMISSIONS").Columns("OBJECT_NAME")}

        bs = New BindingSource(ds, "MB_PERMISSIONS")
        DataGridView1.DataSource = bs

        DataGridView1.Columns("MODULE").Visible = False

        'blinding
        'NumericUpDown1.DataBindings.Add(New Binding("Value", bs, "RATE_SEQ", True, DataSourceUpdateMode.OnValidation))
        'NumericUpDown3.DataBindings.Add(New Binding("Value", bs, "FIRST_REGIST_VALUE", True, DataSourceUpdateMode.OnValidation))

        Dim arr As New Dictionary(Of String, String)

        Dim list = AppDomain.CurrentDomain.GetAssemblies().ToList(). _
                    SelectMany(Function(s) s.GetTypes()). _
                    Where(Function(p) (p.BaseType Is [GetType]().BaseType AndAlso _
                                       p.Assembly Is [GetType]().Assembly))
        ComboBox1.Items.Clear()
        For Each type As Type In list
            Try
                Dim objForm As Control = DirectCast(Activator.CreateInstance(type), Control)
                arr.Add(type.Name, DirectCast(Activator.CreateInstance(type), Control).Text & " (" & type.Name & ")")
            Catch ex As Exception
                '
            End Try
            
        Next

        Dim sorted = From pair In arr
             Order By pair.Value
        Dim sortedDictionary = sorted.ToDictionary(Function(p) p.Key, Function(p) p.Value)

        ComboBox1.BeginUpdate()
        ComboBox1.DisplayMember = "Value"
        ComboBox1.ValueMember = "Key"
        ComboBox1.DataSource = New BindingSource(sortedDictionary, Nothing)
        ComboBox1.EndUpdate()

        ComboBox1.DataBindings.Add(New Binding("SelectedValue", bs, "FORM_NAME", True, DataSourceUpdateMode.OnValidation))
        ComboBox2.DataBindings.Add(New Binding("SelectedValue", bs, "OBJECT_NAME", True, DataSourceUpdateMode.OnValidation))
        TextBox1.DataBindings.Add(New Binding("Text", bs, "ENABLED_GROUPS", True, DataSourceUpdateMode.OnValidation))
        'TextBox5.DataBindings.Add(New Binding("Text", bs, "OBJ_DESC", True, DataSourceUpdateMode.OnValidation))

        getGROUPS()

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub frmFees_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fPermissions = Nothing
    End Sub

    Private Sub getPARAMETERS(Optional ByVal SEARCH As String = "")
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", ID)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_PERMISSIONS "
        'query &= "WHERE MODULE = @p0 "
        If SEARCH.Length > 0 Then
            query &= "WHERE (WHERE FORM_NAME LIKE @p0) OR (OBJECT_NAME LIKE @p1) OR (ENABLED_GROUPS LIKE @p2) "

            SEARCH = SEARCH.Replace(" ", "%")
            parameters.Add("@p0", SEARCH)
            parameters.Add("@p1", SEARCH)
            parameters.Add("@p2", SEARCH)
            'parameters.Add("@p4", SEARCH)
        End If
        query &= "ORDER BY FORM_NAME, OBJECT_NAME "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_PERMISSIONS").Copy

        If ds.Tables.Contains("MB_PERMISSIONS") = True Then
            ds.Tables("MB_PERMISSIONS").Clear()
            ds.Tables("MB_PERMISSIONS").Merge(dt)
            ds.Tables("MB_PERMISSIONS").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If
    End Sub

    Private Sub getGROUPS()
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", searchValue)
        'parameters.Add("@p1", searchValue)

        Dim query As String = String.Empty
        query &= "SELECT * "
        query &= "FROM            GROUPS "
        'query &= "WHERE        (GROUP_NAME LIKE @p0) "
        query &= "ORDER BY GROUP_NAME"

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "GROUPS")

        If ds.Tables.Contains("GROUPS") = True Then
            ds.Tables("GROUPS").Clear()
            ds.Tables("GROUPS").Merge(dt)
            ds.Tables("GROUPS").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            ds.Tables("GROUPS").PrimaryKey = New DataColumn() {ds.Tables("GROUPS").Columns("ID")}

            Dim CHK As DataColumn = ds.Tables("GROUPS").Columns.Add("CHK", Type.GetType("System.Boolean"))
            CHK.DefaultValue = False

            DataGridView2.DataSource = ds.Tables("GROUPS")

            For i As Integer = 0 To DataGridView2.ColumnCount - 1
                DataGridView2.Columns(i).Visible = False
            Next
            DataGridView2.Columns("ID").Visible = True
            DataGridView2.Columns("GROUP_NAME").Visible = True
            DataGridView2.Columns("CHK").Visible = True
            'DataGridView1.Columns("PROVINCE_NAME_EN").Visible = True
            '.Columns("PROVINCE_AREA").Visible = True

            DataGridView2.Columns("GROUP_NAME").Width = 150
            'DataGridView1.Columns("PROVINCE_NAME_EN").Width = 150

            DataGridView2.Columns("ID").ReadOnly = True
            DataGridView2.Columns("GROUP_NAME").ReadOnly = True

            DataGridView2.Columns("CHK").DisplayIndex = 0
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedValue IsNot Nothing Then
            Dim arr As Dictionary(Of String, String) = getControlsByFormName(ComboBox1.SelectedValue.ToString)

            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", ComboBox1.SelectedValue)

            Dim query As String = String.Empty
            query &= "SELECT OBJECT_NAME "
            query &= "FROM            MB_PERMISSIONS "
            query &= "WHERE        FORM_NAME = @p0 "

            'Dim dt As DataTable = New DataTable

            'dt = fillWebSQL(query, parameters, "MB_MEMBER_STATUS")

            'If dt.Rows.Count > 0 Then
            '    For i As Integer = 0 To dt.Rows.Count - 1
            '        arr.Remove(dt.Rows(i).Item("OBJECT_NAME").ToString)
            '    Next
            'End If

            Dim sorted = From pair In arr
                 Order By pair.Value
            Dim sortedDictionary = sorted.ToDictionary(Function(p) p.Key, Function(p) p.Value)

            ComboBox2.BeginUpdate()
            ComboBox2.DisplayMember = "Value"
            ComboBox2.ValueMember = "Key"
            ComboBox2.DataSource = New BindingSource(sortedDictionary, Nothing)
            ComboBox2.EndUpdate()

            'ComboBox2.SelectedIndex = ComboBox2.FindStringExact(ds.Tables("MB_PERMISSIONS").Rows(bs.Position).Item("OBJECT_NAME").ToString)
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btApply.Click
        Try
            Dim parameters As New Dictionary(Of String, Object)
            'parameters.Add("@p0", MODULE_ID)

            bs.EndEdit()
            Dim query As String = String.Empty
            query &= "SELECT * FROM MB_PERMISSIONS "
            'query &= "WHERE MODULE = @p0 "

            If ds.Tables("MB_PERMISSIONS").GetChanges IsNot Nothing Then
                Try
                    updateWebSQL(query, parameters, ds.Tables("MB_PERMISSIONS"))
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "updateWebSQL")
                End Try
            End If

            MessageBox.Show("Apply Successfully")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "MB_PERMISSIONS.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btExecute.Click
        getPARAMETERS(TextBox3.Text)
    End Sub

    Private Sub btGroups_Click(sender As Object, e As EventArgs) Handles btGroups.Click
        Dim f As New frmMainGroups
        'f.MODULE_ID = MODULE_ID
        f.ENABLED_GROUPS = TextBox1.Text
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = f.ENABLED_GROUPS
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        'new contact
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainPermissionsNew
        'f.MODULE_ID = MODULE_ID
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

            'add it

            'generate code
            'SELECT TOP (1) POSITION_CODE FROM MB_POSITION ORDER BY CONVERT(INT, POSITION_CODE) DESC
            Dim parameters As New Dictionary(Of String, Object)
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            Dim query As String = "INSERT INTO MB_PERMISSIONS (FORM_NAME, OBJECT_NAME, ENABLED_GROUPS) VALUES (@p0,@p1,@p2)"

            parameters.Clear()
            'parameters.Add("@p0", MODULE_ID)
            parameters.Add("@p0", f.ComboBox1.SelectedValue)
            parameters.Add("@p1", f.ComboBox2.SelectedValue)
            parameters.Add("@p2", f.TextBox1.Text)

            Try
                executeWebSQL(query, parameters)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            End Try

            'logs
            'saveLOGS(CONTACT_CODE, "MB_CONTACT", "CONTACT_CODE", "ADD", "", f.TextBox9.Text, user_name)

            'refresh grid
            getPARAMETERS(TextBox3.Text)

            MessageBox.Show("เพิ่มเสร็จสิ้น")
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        'del contact
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If DataGridView1.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView1.CurrentRow.Cells("OBJECT_NAME").Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("MODULE").Value)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("FORM_NAME").Value)
                parameters.Add("@p1", DataGridView1.CurrentRow.Cells("OBJECT_NAME").Value)

                Dim query As String = "DELETE FROM MB_PERMISSIONS WHERE FORM_NAME = @p0 AND OBJECT_NAME = @p1"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'logs
                'saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT", "CONTACT_CODE", "ADD", "", TextBox9.Text, user_name)

                'refresh grid
                getPARAMETERS(TextBox3.Text)

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Process.Start(System.Environment.CurrentDirectory & "/tools/spyxx.exe")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Process.Start(System.Environment.CurrentDirectory & "/tools/spyxx_amd64.exe")
    End Sub

    Private Sub llCopy_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llCopy.LinkClicked
        If TextBox1.TextLength > 0 Then
            clipboard = TextBox1.Text
        Else
            MessageBox.Show("ไม่มีอะไรให้คัดลอก")
        End If
    End Sub

    Private Sub llPaste_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llPaste.LinkClicked
        If clipboard.Length > 0 Then
            TextBox1.Text = clipboard
        Else
            MessageBox.Show("ไม่มีอะไรให้วาง")
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.TextLength > 0 Then
            If ds.Tables.Contains("GROUPS") = True Then
                If ds.Tables("GROUPS").Rows.Count > 0 Then
                    'clear old result
                    For r As Integer = 0 To ds.Tables("GROUPS").Rows.Count - 1
                        ds.Tables("GROUPS").Rows(r).Item("CHK") = False
                    Next

                    'If GROUPS.Length > 0 Then
                    Dim g() As String = TextBox1.Text.Split(","c)
                    'MessageBox.Show(g.Length.ToString)
                    For i As Integer = 0 To g.Length - 1
                        For r As Integer = 0 To ds.Tables("GROUPS").Rows.Count - 1
                            'ds.Tables("GROUPS").Rows(r).Item("CHK") = False
                            If CInt(g(i)) = CInt(ds.Tables("GROUPS").Rows(r).Item("ID")) Then
                                ds.Tables("GROUPS").Rows(r).Item("CHK") = True
                                Exit For
                            End If
                        Next
                    Next
                    'End If
                End If
            End If
            
        End If

    End Sub

    Private Sub DataGridView2_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellEndEdit
        DataGridView2.EndEdit()
        If ds.Tables.Contains("GROUPS") = True Then
            If ds.Tables("GROUPS").Rows.Count > 0 Then
                Dim s As String = String.Empty
                For r As Integer = 0 To ds.Tables("GROUPS").Rows.Count - 1
                    If CBool(ds.Tables("GROUPS").Rows(r).Item("CHK")) = True Then
                        If s.Length > 0 Then
                            s &= "," & ds.Tables("GROUPS").Rows(r).Item("ID").ToString
                        Else
                            s = ds.Tables("GROUPS").Rows(r).Item("ID").ToString
                        End If
                    End If
                Next
                TextBox1.Text = s
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