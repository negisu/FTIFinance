Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

Public Class frmMainUsers
    Dim ds As DataSet
    Dim bs As BindingSource
    Dim clipboard As String

    Private Sub frmFeesRule2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet

        clipboard = String.Empty

        Try
            getPARAMETERS()
        Catch ex As Exception
            MessageBox.Show("getMB_PERMISSIONS" & vbCrLf & ex.Message)
        End Try

        ds.Tables("USERS").PrimaryKey = New DataColumn() {ds.Tables("USERS").Columns("USER_ID")}

        bs = New BindingSource(ds, "USERS")
        DataGridView1.DataSource = bs

        DataGridView1.Columns("USER_PASSWORD").Visible = False

        'blinding
        'NumericUpDown1.DataBindings.Add(New Binding("Value", bs, "RATE_SEQ", True, DataSourceUpdateMode.OnValidation))
        'NumericUpDown3.DataBindings.Add(New Binding("Value", bs, "FIRST_REGIST_VALUE", True, DataSourceUpdateMode.OnValidation))

        TextBox2.DataBindings.Add(New Binding("Text", bs, "USER_ID", True, DataSourceUpdateMode.OnValidation))
        TextBox4.DataBindings.Add(New Binding("Text", bs, "USER_NAME", True, DataSourceUpdateMode.OnValidation))
        TextBox1.DataBindings.Add(New Binding("Text", bs, "USER_GROUP", True, DataSourceUpdateMode.OnValidation))
        'TextBox5.DataBindings.Add(New Binding("Text", bs, "OBJ_DESC", True, DataSourceUpdateMode.OnValidation))

        getGROUPS()

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    'Private Sub frmFees_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
    '    fPermissions = Nothing
    'End Sub

    Private Sub getPARAMETERS(Optional ByVal SEARCH As String = "")
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM USERS "
        'query &= "WHERE MODULE = @p0 "
        If SEARCH.Length > 0 Then
            query &= "AND ((WHERE USER_ID LIKE @p1) OR (USER_GROUP LIKE @p2)) "

            SEARCH = SEARCH.Replace(" ", "%")
            parameters.Add("@p1", SEARCH)
            parameters.Add("@p2", SEARCH)
            'parameters.Add("@p3", SEARCH)
            'parameters.Add("@p4", SEARCH)
        End If
        query &= "ORDER BY USER_ID "

        Dim dt As DataTable = fillWebSQL(query, parameters, "USERS").Copy

        If ds.Tables.Contains("USERS") = True Then
            ds.Tables("USERS").Clear()
            ds.Tables("USERS").Merge(dt)
            ds.Tables("USERS").AcceptChanges()
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btApply.Click
        Try
            Dim parameters As New Dictionary(Of String, Object)
            'parameters.Add("@p0", MODULE_ID)

            bs.EndEdit()
            Dim query As String = String.Empty
            query &= "SELECT * FROM USERS "
            'query &= "WHERE MODULE = @p0 "

            If ds.Tables("USERS").GetChanges IsNot Nothing Then
                Try
                    updateWebSQL(query, parameters, ds.Tables("USERS"))
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "updateWebSQL")
                End Try
            End If

            MessageBox.Show("Apply Successfully")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "USERS.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            TextBox1.Text = f.TextBox1.Text
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        'new contact
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainUsersNew
        'f.MODULE_ID = MODULE_ID
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

            'add it

            'generate code
            'SELECT TOP (1) POSITION_CODE FROM MB_POSITION ORDER BY CONVERT(INT, POSITION_CODE) DESC
            Dim parameters As New Dictionary(Of String, Object)
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            Dim query As String = "INSERT INTO USERS (USER_ID, USER_NAME, USER_GROUP, USER_DIV) VALUES (@p0,@p1,@p2,@p3)"

            parameters.Clear()
            parameters.Add("@p0", f.TextBox2.Text.ToUpper)
            parameters.Add("@p1", f.TextBox2.Text.ToUpper)
            parameters.Add("@p2", f.TextBox1.Text)
            parameters.Add("@p3", f.TextBox3.Text)

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
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView1.CurrentRow.Cells("USER_ID").Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("USER_ID").Value)
                'parameters.Add("@p1", DataGridView1.CurrentRow.Cells("FORM_NAME").Value)
                'parameters.Add("@p2", DataGridView1.CurrentRow.Cells("OBJECT_NAME").Value)

                Dim query As String = "DELETE FROM USERS WHERE USER_ID = @p0"

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

    Private Sub btUsers_Click(sender As Object, e As EventArgs) Handles btUsers.Click
        Dim f As New frmMainUsersInfo
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            TextBox2.Text = f.DataGridView1.CurrentRow.Cells("SAMAccountName").Value.ToString
            TextBox4.Text = f.DataGridView1.CurrentRow.Cells("displayName").Value.ToString
        End If
        f.Dispose()
        f = Nothing
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