Imports System.Windows.Forms

Public Class frmFTICommitteeGroupsNew

    Friend dtMB_PERIOD As DataTable
    Dim ds As DataSet

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If TextBox3.TextLength > 0 And MaskedTextBox1.TextLength > 0 And TextBox5.TextLength > 0 And TextBox6.TextLength > 0 Then
            'check existing
            Dim pRep As New Dictionary(Of String, Object)
            pRep.Add("@p0", TextBox3.Text)
            'pRep.Add("@p1", TextBox4.Text)

            Dim qRep As String = String.Empty
            qRep &= "SELECT COUNT(*) AS CNT * "
            qRep &= "FROM MB_COMMITTEE_WORK_GROUP "
            qRep &= "WHERE (WORK_GROUP_NAME = @p0)"

            Dim CNT As Integer = 0

            Try
                CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
            Catch ex As Exception
                MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT MB_COMMITTEE_WORK_GROUP")
            End Try

            If CNT = 0 Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("พบ " & TextBox3.Text & " " & TextBox4.Text & " ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIRepresentsNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet

        ds.Tables.Add(dtMB_PERIOD)

        ComboBox4.DataSource = ds.Tables("MB_PERIOD")
        ComboBox4.DisplayMember = "PERIOD_NAME"
        ComboBox4.ValueMember = "PERIOD_CODE"

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

    'Private Sub btWORKgroup_Click(sender As Object, e As EventArgs) Handles btWORKgroup.Click
    '    Dim f As New frmFTIWorkGroup
    '    If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
    '        TextBox9.Text = f.DataGridView1.CurrentRow.Cells("WORK_GROUP_CODE").Value.ToString
    '    End If
    '    f.Dispose()
    '    f = Nothing
    'End Sub

    Private Sub btDIV1_Click(sender As Object, e As EventArgs) Handles btDIV1.Click
        Dim f As New frmMainDivs
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            TextBox5.Text = f.DataGridView1.CurrentRow.Cells("DIV_CODE").Value.ToString
            TextBox6.Text = f.DataGridView1.CurrentRow.Cells("DIV_NAME").Value.ToString
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        If ComboBox4.Items.Count > 0 Then
            If ComboBox4.SelectedIndex >= 0 Then TextBox7.Text = ComboBox4.SelectedValue.ToString
        End If
    End Sub
End Class
