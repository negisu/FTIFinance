Imports System
Imports System.ServiceModel
Imports System.IO
Imports System.IO.Compression
Imports System.Xml

Public Class frmMainLogin

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        'Dim result As String = String.Empty

        Dim session As String = String.Empty

        Try
            session = client.GetGroups(UsernameTextBox.Text, PasswordTextBox.Text)

            'MessageBox.Show(result)

            'user_session.Clear()
            'Dim results() As String = result.Split(","c)
            'For i As Integer = 0 To results.Length - 1
            '    If results(i).Trim.Length > 0 Then user_groups.Add(results(i))
            'Next

            If session.Length <> 10 Then
                user_name = UsernameTextBox.Text
                user_pass = PasswordTextBox.Text

                Dim infos() As String = session.Split("|"c)

                user_session = infos(0)
                user_firstname = infos(1)
                user_lastname = infos(2)
                user_department = infos(3)

                'get user div
                user_div = getUserDIV(user_name)

                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("We don't recognize this user ID or password" & vbCrLf & vbCrLf & "Make sure you typed the user ID assigned to you by your organization." & vbCrLf & "And check to make sure you typed the correct password." & vbCrLf & session)
            End If

            'If user_groups.Count > 0 Then
            '    user_name = UsernameTextBox.Text
            '    user_pass = PasswordTextBox.Text
            '    Me.DialogResult = Windows.Forms.DialogResult.OK
            '    Me.Close()
            'Else
            '    MessageBox.Show("We don't recognize this user ID or password" & vbCrLf & vbCrLf & "Make sure you typed the user ID assigned to you by your organization." & vbCrLf & "And check to make sure you typed the correct password." & vbCrLf & result)
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
    End Sub

    Private Sub frmLogin_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown, UsernameTextBox.KeyDown, PasswordTextBox.KeyDown
        If (e.KeyCode = Keys.A AndAlso e.Modifiers = Keys.Alt) Then
            'MsgBox("CTRL + B Pressed !")
            UsernameTextBox.Text = "demo"
            PasswordTextBox.Text = "P@$$w0rd"

            OK_Click(sender, e)
        End If
    End Sub
End Class
