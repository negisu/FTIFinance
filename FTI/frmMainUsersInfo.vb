Imports System.Windows.Forms

Public Class frmMainUsersInfo

    'Friend MODULE_ID As Integer
    'Friend ENABLED_GROUPS As String

    Dim ds As DataSet
    'Dim parameters As New Dictionary(Of String, Object)
    'Dim query As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If ds.Tables.Contains("USERS") = True Then
            If DataGridView1.CurrentRow IsNot Nothing Then
                'it's ok to go
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        End If

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIRepresentsNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet
        'query = String.Empty

        ComboBox1.SelectedIndex = 0

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getUsers(ByVal SEARCH As String)
        'dv = New DataView(dt)
        'Dim searchValue As String = TextBox1.Text.Trim & "*"

        Dim dt As DataTable = New DataTable

        'ค้นจาก บัญชีผู้ใช้(SAMAccountName)
        'ค้นจาก ชื่อที่ปรากฏให้เห็น(displayName)
        'ค้นจาก ชื่อ(givenName)
        'ค้นจาก นามสกุล(sn)
        'ค้นจาก ชื่อเต็ม(cn)

        Dim filter As String = String.Empty
        Select Case ComboBox1.SelectedIndex
            Case 0
                filter = String.Format("(SAMAccountName={0}*)", SEARCH)
            Case 1
                filter = String.Format("(displayName={0}*)", SEARCH)
            Case 2
                filter = String.Format("(givenName={0}*)", SEARCH)
            Case 3
                filter = String.Format("(sn={0}*)", SEARCH)
            Case 4
                filter = String.Format("(cn={0}*)", SEARCH)
            Case Else
                filter = String.Format("(SAMAccountName={0}*)", SEARCH)
        End Select

        dt = getWebUsers("USERS", filter)

        If ds.Tables.Contains("USERS") = True Then
            ds.Tables("USERS").Clear()
            ds.Tables("USERS").Merge(dt)
            ds.Tables("USERS").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            ds.Tables("USERS").PrimaryKey = New DataColumn() {ds.Tables("USERS").Columns("SAMAccountName")}

            DataGridView1.DataSource = ds.Tables("USERS")

            For i As Integer = 0 To DataGridView1.ColumnCount - 1
                DataGridView1.Columns(i).Visible = False
            Next
            DataGridView1.Columns("SAMAccountName").Visible = True
            DataGridView1.Columns("displayName").Visible = True
            DataGridView1.Columns("givenName").Visible = True
            DataGridView1.Columns("sn").Visible = True
            DataGridView1.Columns("cn").Visible = True

            DataGridView1.Columns("displayName").Width = 150

            'DataGridView1.Columns("ID").ReadOnly = True

            'DataGridView1.Columns("CHK").DisplayIndex = 0
        End If
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            getUsers(TextBox1.Text)
        End If
    End Sub

    Private Sub btChangePassword_Click(sender As Object, e As EventArgs) Handles btChangePassword.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            Dim f As New frmMainUsersPasswordChange
            f.IS_RESET = True
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                If MessageBox.Show("คุณมั่นใจแล้วหรือที่จะกำหนดรหัสใหม่แก่ " & DataGridView1.CurrentRow.Cells("SAMAccountName").Value.ToString, "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    If client.ResetPassword(f.TextBox4.Text, f.TextBox1.Text, DataGridView1.CurrentRow.Cells("SAMAccountName").Value.ToString, f.TextBox2.Text, user_session) = True Then
                        MessageBox.Show("เปลี่ยนรหัสผ่าน เรียบร้อย")
                        Me.DialogResult = System.Windows.Forms.DialogResult.OK
                        Me.Close()
                    Else
                        MessageBox.Show("ระบบเกิดปัญหา กรุณาลองใหม่อีกครั้ง หรือ ติดต่อผู้ดูแลระบบ")
                    End If
                End If
            End If
            f.Dispose()
            f = Nothing
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
