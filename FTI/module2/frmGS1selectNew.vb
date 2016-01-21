Imports System.Windows.Forms

Public Class frmGS1selectNew

    'Friend MODULE_ID As Integer
    Friend MODE_ID As Integer

    'Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If DataGridView1.CurrentRow IsNot Nothing Then
    '        'it's ok to go
    '        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    '        Me.Close()
    '    End If

    'End Sub

    'Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    '    Me.Close()
    'End Sub

    Private Sub frmFTIRepresentsNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub btNeverUse_Click(sender As Object, e As EventArgs) Handles btNeverUse.Click
        MODE_ID = 100
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    Private Sub btResell_Click(sender As Object, e As EventArgs) Handles btResell.Click
        MODE_ID = 200
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btReserved_Click(sender As Object, e As EventArgs) Handles btReserved.Click
        MODE_ID = 300
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btManual_Click(sender As Object, e As EventArgs) Handles btManual.Click
        MODE_ID = 400
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class
