Imports System.Windows.Forms

Public Class frmGS1resellNew

    'Friend MODULE_ID As Integer
    Friend MEMBER_TYPE_CODE As String

    Dim ds As DataSet
    Dim parameters As New Dictionary(Of String, Object)
    Dim query As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            'it's ok to go
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIRepresentsNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet
        query = String.Empty

        'ComboBox1.SelectedIndex = 0

        getMB_PRENAME()

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMB_PRENAME()
        'dv = New DataView(dt)
        'SEARCH = "%" & TextBox1.Text.Trim.Replace(" ", "%") & "%"

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", "300")
        parameters.Add("@p1", "C")
        parameters.Add("@p2", MEMBER_TYPE_CODE)
        parameters.Add("@p3", getParameters(2, "GS1_RESELL_OLDER"))

        Dim query As String = String.Empty
        query &= "SELECT * FROM fnc_GS1resell(@p0, @p1, @p2, @p3)"

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "RESELL")

        If ds.Tables.Contains("RESELL") = True Then
            ds.Tables("RESELL").Clear()
            ds.Tables("RESELL").Merge(dt)
            ds.Tables("RESELL").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            ds.Tables("RESELL").PrimaryKey = New DataColumn() {ds.Tables("RESELL").Columns("MEMBER_CODE")}

            DataGridView1.DataSource = ds.Tables("RESELL")

            'For i As Integer = 0 To DataGridView1.ColumnCount - 1
            '    DataGridView1.Columns(i).Visible = False
            'Next
            'DataGridView1.Columns("DIV_CODE").Visible = True
            'DataGridView1.Columns("DIV_NAME").Visible = True
            'DataGridView1.Columns("ABTN_DIV_NAME").Visible = True
            'DataGridView1.Columns("PROVINCE_NAME_EN").Visible = True
            '.Columns("PROVINCE_AREA").Visible = True

            'DataGridView1.Columns("MEMBER_CODE").Width = 100
            'DataGridView1.Columns("RETIRE_DATE").Width = 100
            'DataGridView1.Columns("STATUS_CODE").Width = 150
            'DataGridView1.Columns("MEMBER_TYPE_CODE").Width = 150

            'DataGridView1.Columns("DIV_CODE").ReadOnly = True
        End If
    End Sub

    'Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs)
    '    If e.KeyCode = Keys.Enter Then
    '        getMB_PRENAME(TextBox1.Text)
    '    End If
    'End Sub

    'Private Sub btApply_Click(sender As Object, e As EventArgs)
    '    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
    '    If DataGridView1.CurrentRow IsNot Nothing Then
    '        DataGridView1.EndEdit()
    '        'bs.EndEdit()

    '        If MessageBox.Show("ยืนยันที่จะ" & btApply.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
    '            Dim row As DataRow = ds.Tables("GROUPS").Rows.Find(DataGridView1.CurrentRow.Cells("ID").Value)

    '            Try
    '                Dim parameters As New Dictionary(Of String, Object)
    '                'parameters.Add("@p0", MODULE_ID)

    '                Dim query As String = String.Empty
    '                query &= "SELECT * "
    '                query &= "FROM            GROUPS "
    '                'query &= "WHERE        (POSITION_NAME_TH LIKE @p0) OR "
    '                'query &= "                         (POSITION_NAME_EN LIKE @p1) "
    '                'query &= "ORDER BY POSITION_NAME_TH, POSITION_NAME_EN"

    '                If ds.Tables("GROUPS").GetChanges IsNot Nothing Then
    '                    Try
    '                        updateWebSQL(query, parameters, ds.Tables("GROUPS"))
    '                    Catch ex As Exception
    '                        MessageBox.Show(ex.Message, "updateWebSQL")
    '                    End Try
    '                End If

    '                MessageBox.Show("Apply Successfully")
    '            Catch ex As Exception
    '                MessageBox.Show(ex.Message, "GROUPS.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            End Try

    '            'refresh grid
    '            ds.Tables("GROUPS").AcceptChanges()

    '            MessageBox.Show("บันทึกเสร็จสิ้น")
    '        End If


    '    End If
    'End Sub

    'Private Sub btNew_Click(sender As Object, e As EventArgs)
    '    'new contact
    '    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
    '    Dim f As New frmMainGroupsNew
    '    'f.MODULE_ID = MODULE_ID
    '    If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

    '        'add it

    '        'generate code
    '        'SELECT TOP (1) POSITION_CODE FROM MB_POSITION ORDER BY CONVERT(INT, POSITION_CODE) DESC
    '        Dim parameters As New Dictionary(Of String, Object)
    '        'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

    '        Dim query As String = "INSERT INTO GROUPS (GROUP_NAME) VALUES (@p0)"

    '        parameters.Clear()
    '        parameters.Add("@p0", f.TextBox9.Text)
    '        'parameters.Add("@p1", MODULE_ID)
    '        'parameters.Add("@p2", f.TextBox30.Text)
    '        'parameters.Add("@p3", f.TextBox1.Text)

    '        Try
    '            executeWebSQL(query, parameters)
    '        Catch ex As Exception
    '            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
    '        End Try

    '        'logs
    '        'saveLOGS(CONTACT_CODE, "MB_CONTACT", "CONTACT_CODE", "ADD", "", f.TextBox9.Text, user_name)

    '        'refresh grid
    '        getMB_PRENAME(TextBox1.Text)

    '        MessageBox.Show("เพิ่มเสร็จสิ้น")
    '    End If
    '    f.Dispose()
    '    f = Nothing
    'End Sub

    'Private Sub btDelete_Click(sender As Object, e As EventArgs)
    '    'del contact
    '    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
    '    If DataGridView1.CurrentRow IsNot Nothing Then
    '        If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView1.CurrentRow.Cells("GROUP_NAME").Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
    '            'delete it
    '            Dim parameters As New Dictionary(Of String, Object)
    '            parameters.Add("@p0", DataGridView1.CurrentRow.Cells("ID").Value)

    '            Dim query As String = "DELETE FROM GROUPS WHERE ID = @p0"

    '            Try
    '                executeWebSQL(query, parameters)
    '            Catch ex As Exception
    '                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
    '            End Try

    '            'logs
    '            'saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT", "CONTACT_CODE", "ADD", "", TextBox9.Text, user_name)

    '            'refresh grid
    '            getMB_PRENAME(TextBox1.Text)

    '            MessageBox.Show("ลบเสร็จสิ้น")
    '        End If
    '    End If

    'End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    'Private Sub btFind_Click(sender As Object, e As EventArgs)
    '    getMB_PRENAME(TextBox1.Text)
    'End Sub
End Class
