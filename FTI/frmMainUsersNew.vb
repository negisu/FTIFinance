Imports System.Windows.Forms

Public Class frmMainUsersNew

    'Friend MODULE_ID As Integer
    'Friend ENABLED_GROUPS As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If TextBox2.TextLength > 0 And TextBox1.TextLength > 0 Then
            'check existing
            Dim pRep As New Dictionary(Of String, Object)
            pRep.Add("@p0", TextBox2.Text)
            'pRep.Add("@p1", ComboBox1.Text)
            'pRep.Add("@p2", ComboBox2.Text)

            Dim qRep As String = String.Empty
            qRep &= "SELECT COUNT(*) AS CNT * "
            qRep &= "FROM USERS "
            qRep &= "WHERE (USER_ID = @p0)"

            Dim CNT As Integer = 0

            Try
                CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
            Catch ex As Exception
                MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT MB_PERMISSIONS")
            End Try

            If CNT = 0 Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("พบ " & TextBox2.Text & " ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
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

    Private Sub btGroups_Click(sender As Object, e As EventArgs) Handles btGroups.Click
        Dim f As New frmMainGroups
        'f.MODULE_ID = MODULE_ID
        f.ENABLED_GROUPS = TextBox1.Text
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = f.ENABLED_GROUPS
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btUsers_Click(sender As Object, e As EventArgs) Handles btUsers.Click
        Dim f As New frmMainUsersInfo
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            TextBox2.Text = f.DataGridView1.CurrentRow.Cells("SAMAccountName").Value.ToString
            'TextBox4.Text = f.DataGridView1.CurrentRow.Cells("displayName").Value.ToString
        End If
        f.Dispose()
        f = Nothing
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    Private Sub btDiv_Click(sender As Object, e As EventArgs) Handles btDiv.Click
        Dim f As New frmMainDivs
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            TextBox3.Text = f.DataGridView1.CurrentRow.Cells("DIV_CODE").Value.ToString
            'TextBox6.Text = f.DataGridView1.CurrentRow.Cells("DIV_NAME").Value.ToString
        End If
        f.Dispose()
        f = Nothing
    End Sub
End Class
