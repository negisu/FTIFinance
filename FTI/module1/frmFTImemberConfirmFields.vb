Imports System.Windows.Forms

Public Class frmFTImemberConfirmFields

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If TextBox1.TextLength > 0 And TextBox2.TextLength > 0 And TextBox3.TextLength > 0 And TextBox4.TextLength > 0 And TextBox5.TextLength > 0 Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIRepresentsNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = getParameters(1, "FTI_RPT_5.3.25.01_AT")
        TextBox2.Text = getParameters(1, "FTI_RPT_5.3.25.01_NAME")
        TextBox3.Text = getParameters(1, "FTI_RPT_5.3.25.01_POSITION")
        TextBox4.Text = getParameters(1, "FTI_RPT_5.3.25.01_USER")
        TextBox5.Text = getParameters(1, "FTI_RPT_5.3.25.01_TEL")

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub
End Class
