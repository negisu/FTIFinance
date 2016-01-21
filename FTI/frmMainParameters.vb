Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

Public Class frmMainParameters
    Friend MODULE_ID As Integer
    Dim ds As DataSet
    Dim bs As BindingSource

    Private Sub frmFeesRule2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet

        Try
            getPARAMETERS(MODULE_ID)
        Catch ex As Exception
            MessageBox.Show("getPARAMETERS" & vbCrLf & ex.Message)
        End Try

        ds.Tables("MB_PARAMETERS").PrimaryKey = New DataColumn() {ds.Tables("MB_PARAMETERS").Columns("OBJ_NAME")}

        bs = New BindingSource(ds, "MB_PARAMETERS")
        DataGridView1.DataSource = bs

        DataGridView1.Columns("OBJ_MODULE").Visible = False

        DataGridView1.Columns("OBJ_NAME").Width = 200
        DataGridView1.Columns("OBJ_VALUE").Width = 200
        DataGridView1.Columns("OBJ_LABEL").Width = 180

        'blinding
        'NumericUpDown1.DataBindings.Add(New Binding("Value", bs, "RATE_SEQ", True, DataSourceUpdateMode.OnValidation))
        'NumericUpDown3.DataBindings.Add(New Binding("Value", bs, "FIRST_REGIST_VALUE", True, DataSourceUpdateMode.OnValidation))

        TextBox2.DataBindings.Add(New Binding("Text", bs, "OBJ_NAME", True, DataSourceUpdateMode.OnValidation))
        TextBox4.DataBindings.Add(New Binding("Text", bs, "OBJ_LABEL", True, DataSourceUpdateMode.OnValidation))
        TextBox1.DataBindings.Add(New Binding("Text", bs, "OBJ_VALUE", True, DataSourceUpdateMode.OnValidation))
        TextBox5.DataBindings.Add(New Binding("Text", bs, "OBJ_DESC", True, DataSourceUpdateMode.OnValidation))

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getPARAMETERS(ByVal ID As Integer, Optional ByVal SEARCH As String = "")
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", ID)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_PARAMETERS "
        query &= "WHERE OBJ_MODULE = @p0 "
        If SEARCH.Length > 0 Then
            query &= "AND ((OBJ_NAME LIKE @p1) OR (OBJ_LABEL LIKE @p2) OR (OBJ_DESC LIKE @p3) OR (OBJ_VALUE LIKE @p4)) "

            SEARCH = "%" & SEARCH.Replace(" ", "%") & "%"
            parameters.Add("@p1", SEARCH)
            parameters.Add("@p2", SEARCH)
            parameters.Add("@p3", SEARCH)
            parameters.Add("@p4", SEARCH)
        End If
        query &= "ORDER BY OBJ_NAME "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_PARAMETERS").Copy

        If ds.Tables.Contains("MB_PARAMETERS") = True Then
            ds.Tables("MB_PARAMETERS").Clear()
            ds.Tables("MB_PARAMETERS").Merge(dt)
            ds.Tables("MB_PARAMETERS").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btApply.Click
        Try
            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", MODULE_ID)

            bs.EndEdit()
            Dim query As String = String.Empty
            query &= "SELECT * FROM MB_PARAMETERS "
            query &= "WHERE OBJ_MODULE = @p0 "

            If ds.Tables("MB_PARAMETERS").GetChanges IsNot Nothing Then
                Try
                    updateWebSQL(query, parameters, ds.Tables("MB_PARAMETERS"))
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "updateWebSQL")
                End Try
            End If

            MessageBox.Show("Apply Successfully")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "MB_PARAMETERS.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btExecute.Click
        'getPARAMETERS(MODULE_ID, TextBox3.Text)
        TextBox3_TextChanged(sender, e)
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Dim SEARCH As String = "%" & TextBox3.Text.Replace(" ", "%") & "%"
        bs.Filter = String.Format("(OBJ_NAME LIKE '{0}') OR (OBJ_LABEL LIKE '{0}') OR (OBJ_DESC LIKE '{0}') OR (OBJ_VALUE LIKE '{0}')", SEARCH)
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub
End Class