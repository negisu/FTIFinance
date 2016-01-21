Imports System.Windows.Forms

Public Class frmGS1ean13New

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If TextBox9.TextLength > 0 And TextBox30.TextLength > 0 Then
            'check existing
            Dim pRep As New Dictionary(Of String, Object)
            pRep.Add("@p0", TextBox9.Text)
            pRep.Add("@p1", TextBox30.Text)

            Dim qRep As String = String.Empty
            qRep &= "SELECT COUNT(*) AS CNT * "
            qRep &= "FROM MB_BUSSINESS_TYPE "
            qRep &= "WHERE (BUSSINESS_TYPE_DESC_TH = @p0) AND (BUSSINESS_TYPE_DESC_EN = @p1)"

            Dim CNT As Integer = 0

            Try
                CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
            Catch ex As Exception
                MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT MB_POSITION")
            End Try

            If CNT = 0 Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("พบ " & TextBox9.Text & " " & TextBox30.Text & " ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
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
