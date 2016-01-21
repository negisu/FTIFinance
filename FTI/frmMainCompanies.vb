Imports System.Windows.Forms

Public Class frmMainCompanies

    Dim ds As DataSet
    Dim parameters As New Dictionary(Of String, Object)
    Dim query As String

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

    Private Sub frmMainLocations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet
        query = String.Empty

        ComboBox1.SelectedIndex = 0

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMB_PRENAME()
        'dv = New DataView(dt)
        Dim searchValue As String = TextBox1.Text.Trim.Replace(" ", "%")
        searchValue = "%" & searchValue & "%"

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", searchValue)
        parameters.Add("@p1", searchValue)

        Dim query As String = String.Empty
        query &= "SELECT   TOP 100     MB_MEMBER.MEMBER_CODE, MB_COMP_PERSON.COMP_PERSON_NAME_TH, MB_COMP_PERSON.COMP_PERSON_NAME_EN "
        query &= "FROM            MB_MEMBER INNER JOIN "
        query &= "                         MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE "
        query &= String.Format("WHERE        (MB_MEMBER.MEMBER_MAIN_GROUP_CODE = '000') AND (MB_MEMBER.MEMBER_GROUP_CODE = '000') AND (LEN(MB_MEMBER.MEMBER_CODE) > 0) AND (MB_MEMBER.MEMBER_STATUS_CODE = 'A') AND (MB_MEMBER.MEMBER_CODE LIKE '{0}' OR MB_COMP_PERSON.COMP_PERSON_NAME_TH LIKE '{0}' OR MB_COMP_PERSON.COMP_PERSON_NAME_EN LIKE '{0}') ", searchValue)
        query &= "ORDER BY MB_MEMBER.PREFIX, MB_MEMBER.RUNNING"

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "MB_MEMBER")

        If ds.Tables.Contains("MB_MEMBER") = True Then
            ds.Tables("MB_MEMBER").Clear()
            ds.Tables("MB_MEMBER").Merge(dt)
            ds.Tables("MB_MEMBER").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            ds.Tables("MB_MEMBER").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER").Columns("MEMBER_CODE")}

            DataGridView1.DataSource = ds.Tables("MB_MEMBER")

            'For i As Integer = 0 To DataGridView1.ColumnCount - 1
            '    DataGridView1.Columns(i).Visible = False
            'Next
            'DataGridView1.Columns("MEMBER_CODE").Visible = True
            'DataGridView1.Columns("COMP_PERSON_NAME_TH").Visible = True
            'DataGridView1.Columns("COMP_PERSON_NAME_EN").Visible = True

            DataGridView1.Columns("COMP_PERSON_NAME_TH").Width = 150
            DataGridView1.Columns("COMP_PERSON_NAME_EN").Width = 150

            'DataGridView1.Columns("PROVINCE_CODE").ReadOnly = True
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
