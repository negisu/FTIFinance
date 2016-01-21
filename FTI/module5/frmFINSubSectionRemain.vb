Imports System.Windows.Forms

Public Class frmFINSubSectionRemain

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
        TextBox1.Select()
    End Sub

    Private Sub getIV_SUB_SECTION()
        'dv = New DataView(dt)
        Dim searchValue As String = TextBox1.Text.Trim.Replace(" ", "%")
        searchValue = "%" & searchValue & "%"

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", searchValue)

        Dim query As String = String.Empty
        query &= "SELECT TOP 100  * "
        query &= "FROM            IV_SUB_SECTION "
        query &= "LEFT JOIN SU_DIVISION ON IV_SUB_SECTION.DIV_CODE_INC=SU_DIVISION.DIV_CODE "
        query &= "LEFT JOIN PL_ACTIVITY ON IV_SUB_SECTION.ATV_CODE_INC=PL_ACTIVITY.ATV_CODE "
        query &= "LEFT JOIN PL_PROJECT ON IV_SUB_SECTION.PROJ_ID_INC=PL_PROJECT.PROJ_ID "
        query &= "WHERE SUB_SECTION_NAME LIKE @p0 "
        query &= "ORDER BY SUB_SECTION_NAME"

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "IV_SUB_SECTION")

        If ds.Tables.Contains("IV_SUB_SECTION") = True Then
            ds.Tables("IV_SUB_SECTION").Clear()
            ds.Tables("IV_SUB_SECTION").Merge(dt)
            ds.Tables("IV_SUB_SECTION").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            ds.Tables("IV_SUB_SECTION").PrimaryKey = New DataColumn() {ds.Tables("IV_SUB_SECTION").Columns("SUB_SECTION_CODE")}

            DataGridView1.DataSource = ds.Tables("IV_SUB_SECTION")

            For i As Integer = 0 To DataGridView1.ColumnCount - 1
                DataGridView1.Columns(i).Visible = False
            Next
            DataGridView1.Columns("SUB_SECTION_CODE").Visible = True
            DataGridView1.Columns("SUB_SECTION_NAME").Visible = True
            DataGridView1.Columns("DIV_NAME").Visible = True
            DataGridView1.Columns("PROJ_NAME").Visible = True
            DataGridView1.Columns("ATV_NAME").Visible = True
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            DataGridView1.AutoResizeColumns()

            'DataGridView1.Columns("SUB_SECTION_NAME").Width = 500

            DataGridView1.Columns("SUB_SECTION_CODE").ReadOnly = True
        End If
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            getIV_SUB_SECTION()
        End If
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            OK_Button_Click(sender, e)
        End If
    End Sub
End Class
