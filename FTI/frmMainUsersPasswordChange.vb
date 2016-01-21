Imports System.Windows.Forms

Public Class frmMainUsersPasswordChange

    'Friend MODULE_ID As Integer
    'Friend ENABLED_GROUPS As String

    Friend IS_RESET As Boolean

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If TextBox1.TextLength > 0 Then
            If TextBox2.TextLength > 0 Then
                If String.Compare(TextBox2.Text, TextBox3.Text, False) = 0 Then
                    If ValidatePassword(TextBox2.Text, 8, 1, 1, 1, 1) = True Then
                        If IS_RESET = False Then
                            If client.ChangePassword(user_name, TextBox1.Text, TextBox2.Text, user_session) = True Then
                                MessageBox.Show("เปลี่ยนรหัสผ่าน เรียบร้อย")
                                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                                Me.Close()
                            Else
                                MessageBox.Show("ระบบเกิดปัญหา กรุณาลองใหม่อีกครั้ง หรือ ติดต่อผู้ดูแลระบบ")
                            End If
                        Else
                            Me.DialogResult = System.Windows.Forms.DialogResult.OK
                            Me.Close()
                        End If
                        
                    Else
                        MessageBox.Show("รหัสผ่านยังไม่รัดกุม กรุณากรอกรหัสผ่านใหม่")
                        TextBox2.Focus()
                    End If
                    
                Else
                    MessageBox.Show("กรุณากรอกยืนยันรหัสผ่านใหม่")
                    TextBox2.Focus()
                End If
            Else
                MessageBox.Show("กรุณากรอกรหัสผ่านใหม่")
                TextBox2.Focus()
            End If
        Else
            MessageBox.Show("กรุณากรอกรหัสผ่านเดิม")
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIRepresentsNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IS_RESET = True Then
            'Label1.Visible = False
            'TextBox1.Visible = False
            TextBox4.ReadOnly = False
        Else
            TextBox4.ReadOnly = True
            TextBox4.Text = user_name
            'Label1.Visible = True
            'TextBox1.Visible = True
        End If

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub btGroups_Click(sender As Object, e As EventArgs)
        Dim f As New frmMainGroups
        'f.MODULE_ID = MODULE_ID
        f.ENABLED_GROUPS = TextBox1.Text
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = f.ENABLED_GROUPS
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btUsers_Click(sender As Object, e As EventArgs)
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
End Class
