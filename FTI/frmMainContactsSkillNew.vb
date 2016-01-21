Imports System.Windows.Forms

Public Class frmMainContactsSkillNew

    Friend CONTACT_CODE As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If ComboBox1.SelectedIndex >= 0 Then
            'check existing
            Dim pRep As New Dictionary(Of String, Object)
            pRep.Add("@p0", CONTACT_CODE)
            pRep.Add("@p1", ComboBox1.SelectedValue)

            Dim qRep As String = String.Empty
            qRep &= "SELECT COUNT(*) AS CNT * "
            qRep &= "FROM MB_CONTACT_SKILL "
            qRep &= "WHERE (CONTACT_CODE = @p0) AND (SKILL_CODE = @p1)"

            Dim CNT As Integer = 0

            Try
                CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
            Catch ex As Exception
                MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT MB_CONTACT_LANGUAGE")
            End Try

            If CNT = 0 Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("พบ " & ComboBox1.Text & " ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIRepresentsNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
