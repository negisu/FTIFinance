Imports System.Windows.Forms

Public Class frmMainPermissionsNew

    'Friend MODULE_ID As Integer

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If ComboBox1.SelectedValue.ToString.Length > 0 And ComboBox2.SelectedValue.ToString.Length > 0 Then
            'check existing
            Dim pRep As New Dictionary(Of String, Object)
            'pRep.Add("@p0", MODULE_ID)
            pRep.Add("@p0", ComboBox1.SelectedValue)
            pRep.Add("@p1", ComboBox2.SelectedValue)

            Dim qRep As String = String.Empty
            qRep &= "SELECT COUNT(*) AS CNT * "
            qRep &= "FROM MB_PERMISSIONS "
            qRep &= "WHERE (FORM_NAME = @p0) AND OBJECT_NAME = @p1"

            Dim CNT As Integer = 0

            Try
                CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
            Catch ex As Exception
                MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT MB_PERMISSIONS")
            End Try

            If CNT = 0 Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("พบ " & ComboBox1.Text & " " & ComboBox2.Text & " ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIRepresentsNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        'getGROUPS()

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    'Private Sub getGROUPS()
    '    Dim parameters As New Dictionary(Of String, Object)
    '    'parameters.Add("@p0", searchValue)
    '    'parameters.Add("@p1", searchValue)

    '    Dim query As String = String.Empty
    '    query &= "SELECT * "
    '    query &= "FROM            GROUPS "
    '    'query &= "WHERE        (GROUP_NAME LIKE @p0) "
    '    query &= "ORDER BY GROUP_NAME"

    '    Dim dt As DataTable = New DataTable

    '    dt = fillWebSQL(query, parameters, "GROUPS")

    '    If ds.Tables.Contains("GROUPS") = True Then
    '        ds.Tables("GROUPS").Clear()
    '        ds.Tables("GROUPS").Merge(dt)
    '        ds.Tables("GROUPS").AcceptChanges()
    '    Else
    '        ds.Tables.Add(dt)

    '        ds.Tables("GROUPS").PrimaryKey = New DataColumn() {ds.Tables("GROUPS").Columns("ID")}

    '        Dim CHK As DataColumn = ds.Tables("GROUPS").Columns.Add("CHK", Type.GetType("System.Boolean"))
    '        CHK.DefaultValue = False

    '        DataGridView2.DataSource = ds.Tables("GROUPS")

    '        For i As Integer = 0 To DataGridView2.ColumnCount - 1
    '            DataGridView2.Columns(i).Visible = False
    '        Next
    '        DataGridView2.Columns("ID").Visible = True
    '        DataGridView2.Columns("GROUP_NAME").Visible = True
    '        DataGridView2.Columns("CHK").Visible = True
    '        'DataGridView1.Columns("PROVINCE_NAME_EN").Visible = True
    '        '.Columns("PROVINCE_AREA").Visible = True

    '        DataGridView2.Columns("GROUP_NAME").Width = 150
    '        'DataGridView1.Columns("PROVINCE_NAME_EN").Width = 150

    '        DataGridView2.Columns("ID").ReadOnly = True
    '        DataGridView2.Columns("GROUP_NAME").ReadOnly = True

    '        DataGridView2.Columns("CHK").DisplayIndex = 0
    '    End If
    'End Sub

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

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim arr As Dictionary(Of String, String) = getControlsByFormName(ComboBox1.SelectedValue.ToString)

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", ComboBox1.SelectedValue)

        Dim query As String = String.Empty
        query &= "SELECT OBJECT_NAME "
        query &= "FROM            MB_PERMISSIONS "
        query &= "WHERE        FORM_NAME = @p0 "

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "MB_MEMBER_STATUS")

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                arr.Remove(dt.Rows(i).Item("OBJECT_NAME").ToString)
            Next
        End If

        Dim sorted = From pair In arr
             Order By pair.Value
        Dim sortedDictionary = sorted.ToDictionary(Function(p) p.Key, Function(p) p.Value)

        ComboBox2.BeginUpdate()
        ComboBox2.DisplayMember = "Value"
        ComboBox2.ValueMember = "Key"
        ComboBox2.DataSource = New BindingSource(sortedDictionary, Nothing)
        ComboBox2.EndUpdate()
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub
End Class
