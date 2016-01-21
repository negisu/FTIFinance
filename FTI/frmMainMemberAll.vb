Imports System.Windows.Forms

Public Class frmMainMemberAll

    Dim ds As DataSet
    Dim parameters As New Dictionary(Of String, Object)
    Dim query As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'it's ok to go
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIRepresentsNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet
        query = String.Empty

        ComboBox1.SelectedIndex = 0

        getMB_PRENAME(TextBox1.Text)

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMB_PRENAME(Optional ByVal SEARCH As String = "")
        'dv = New DataView(dt)
        Dim searchValue As String = TextBox1.Text.Trim.Replace(" ", "%")
        searchValue = "%" & searchValue & "%"

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", searchValue)
        'parameters.Add("@p1", searchValue)

        Dim query As String = String.Empty
        query &= "SELECT   TOP 1     MB_MEMBER.*, MB_COMP_PERSON.*, MB_MEMBER_MAIN_GROUP.*, MB_MEMBER_GROUP.* "
        'query &= "SELECT   TOP 1     MB_MEMBER.MEMBER_CODE "
        query &= "FROM            MB_MEMBER INNER JOIN "
        query &= "                         MB_COMP_PERSON ON MB_MEMBER.OU_CODE = MB_COMP_PERSON.OU_CODE AND MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
        query &= "                         MB_MEMBER_MAIN_GROUP ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE INNER JOIN "
        query &= "                         MB_MEMBER_GROUP ON MB_MEMBER.MEMBER_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_GROUP_CODE AND "
        query &= "                         MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_MAIN_GROUP_CODE "

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

            DataGridView1.DataSource = ds.Tables("GROUPS")

            For i As Integer = 0 To DataGridView1.ColumnCount - 1
                DataGridView1.Columns(i).Visible = False
            Next
            DataGridView1.Columns("ID").Visible = True
            DataGridView1.Columns("GROUP_NAME").Visible = True
            DataGridView1.Columns("CHK").Visible = True
            'DataGridView1.Columns("PROVINCE_NAME_EN").Visible = True
            '.Columns("PROVINCE_AREA").Visible = True

            DataGridView1.Columns("GROUP_NAME").Width = 150
            'DataGridView1.Columns("PROVINCE_NAME_EN").Width = 150

            DataGridView1.Columns("ID").ReadOnly = True

            DataGridView1.Columns("CHK").DisplayIndex = 0
        End If
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            getMB_PRENAME()
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
