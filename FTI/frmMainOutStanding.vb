Imports System.Windows.Forms

Public Class frmMainOutStanding

    'Friend MODULE_ID As Integer
    'Friend ENABLED_GROUPS As String

    Friend rows As Dictionary(Of String, String)

    Dim ds As DataSet
    'Dim parameters As New Dictionary(Of String, Object)
    'Dim query As String

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
        'query = String.Empty

        Dim dt As New DataTable("OUTSTANDING")
        dt.Columns.Add("ID", GetType(String))
        dt.Columns.Add("NAME", GetType(String))
        dt.Columns.Add("QTY", GetType(Integer))

        ' Loop over entries.
        Dim pair As KeyValuePair(Of String, String)
        For Each pair In rows
            ' Display Key and Value.
            'Console.WriteLine("{0}, {1}", pair.Key, pair.Value)
            Dim values() As String = pair.Value.Split(","c)
            dt.Rows.Add(pair.Key, values(0), CInt(values(1)))
        Next

        ds.Tables.Add(dt)
        DataGridView1.DataSource = ds.Tables("OUTSTANDING")

        DataGridView1.Columns("ID").Visible = False

        DataGridView1.Columns("NAME").Width = 200

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        If DataGridView1.CurrentRow IsNot Nothing Then
            'open
            Dim parentForm As frmMain = TryCast(Me.Owner, frmMain)

            Select Case DataGridView1.CurrentRow.Cells("ID").Value.ToString
                Case "tsFTIapproveMember"
                    parentForm.tsFTIapproveMember_Click(sender, e)
                Case "tsFTIapproveMemberGP"
                    parentForm.tsFTIapproveMemberGP_Click(sender, e)
                Case "tsFTIapproveStatusResign"
                    parentForm.tsFTIapproveStatusResign_Click(sender, e)
                Case "tsFTIDeleteRelease"
                    parentForm.tsFTIDeleteRelease_Click(sender, e)
                Case "tsGS1memberApproval"
                    parentForm.tsGS1memberApproval_Click(sender, e)
                Case "tsGS1expand"
                    parentForm.tsGS1expand_Click(sender, e)
                Case "tsGS1resign"
                    parentForm.tsGS1resign_Click(sender, e)
                Case "tsGS1blacklist"
                    parentForm.tsGS1blacklist_Click(sender, e)
                Case "tsGS1comeback"
                    parentForm.tsGS1comeback_Click(sender, e)
                Case Else
                    '
            End Select
        End If
    End Sub
End Class
