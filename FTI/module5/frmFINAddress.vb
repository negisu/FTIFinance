Imports System.Windows.Forms

Public Class frmFINAddress

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


        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try

        getAR_ADDRESS()
    End Sub

    Private Sub getAR_ADDRESS()
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = String.Empty
        query &= "SELECT TOP 10 * "
        query &= "FROM            AR_ADDRESS2 "
        query &= "INNER JOIN MB_ADDRESS_TYPE ON AR_ADDRESS2.ADDRESS_TYPE_CODE = MB_ADDRESS_TYPE.ADDRESS_TYPE_CODE "
        query &= "WHERE AR_CODE = @p0 "
        parameters.Add("@p0", AR_CODELabel.Text)

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "AR_ADDRESS2")

        If ds.Tables.Contains("AR_ADDRESS2") = True Then
            ds.Tables("AR_ADDRESS2").Clear()
            ds.Tables("AR_ADDRESS2").Merge(dt)
            ds.Tables("AR_ADDRESS2").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            DataGridView1.DataSource = ds.Tables("AR_ADDRESS2")

            For i As Integer = 0 To DataGridView1.ColumnCount - 1
                DataGridView1.Columns(i).Visible = False
            Next
            DataGridView1.Columns("ADDR1_TH").Visible = True
            DataGridView1.Columns("ADDR2_TH").Visible = True
            DataGridView1.Columns("ADDR3_TH").Visible = True
            DataGridView1.Columns("ADDR1_EN").Visible = True
            DataGridView1.Columns("ADDR2_EN").Visible = True
            DataGridView1.Columns("ADDR3_EN").Visible = True
            DataGridView1.Columns("POST_CODE").Visible = True
            DataGridView1.Columns("TELEPHONE").Visible = True
            DataGridView1.Columns("FAX").Visible = True
            DataGridView1.Columns("ATTN_NAME_TH").Visible = True
            DataGridView1.Columns("ATTN_NAME_EN").Visible = True
            DataGridView1.Columns("ADDRESS_TYPE_NAME_TH").Visible = True
            DataGridView1.Columns("ADDRESS_TYPE_NAME_EN").Visible = True

            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            DataGridView1.AutoResizeColumns()

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
