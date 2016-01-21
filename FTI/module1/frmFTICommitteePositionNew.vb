Imports System.Windows.Forms

Public Class frmFTICommitteePositionNew

    Friend PERIOD_SEQ As Integer
    Friend PERIOD_CODE As String
    Friend CONTACT_CODE As String
    'Friend WORK_GROUP_CODE As String
    'Friend POSITION_CODE As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If TextBox9.TextLength > 0 And TextBox30.TextLength > 0 Then
            'check existing
            Dim pRep As New Dictionary(Of String, Object)
            pRep.Add("@p0", PERIOD_SEQ)
            pRep.Add("@p1", PERIOD_CODE)
            pRep.Add("@p2", CONTACT_CODE)
            pRep.Add("@p3", TextBox9.Text)
            pRep.Add("@p4", TextBox30.Text)

            Dim qRep As String = String.Empty
            qRep &= "SELECT COUNT(*) AS CNT * "
            qRep &= "FROM MB_COMMITTEE_POSITION "
            qRep &= "WHERE (PERIOD_SEQ = @p0) AND (PERIOD_CODE = @p1) AND (CONTACT_CODE = @p2) AND (WORK_GROUP_CODE = @p3) AND (POSITION_CODE = @p4)"

            Dim CNT As Integer = 0

            Try
                CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
            Catch ex As Exception
                MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT MB_COMMITTEE_POSITION")
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

    Private Sub btWORKgroup_Click(sender As Object, e As EventArgs) Handles btWORKgroup.Click
        Dim f As New frmFTIWorkGroup
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            TextBox9.Text = f.DataGridView1.CurrentRow.Cells("WORK_GROUP_CODE").Value.ToString
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btPositions_Click(sender As Object, e As EventArgs) Handles btPositions.Click
        Dim f As New frmMainPositions
        f.POSITION_TYPE = 2
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            TextBox30.Text = f.DataGridView1.CurrentRow.Cells("POSITION_CODE").Value.ToString
        End If
        f.Dispose()
        f = Nothing
    End Sub
End Class
