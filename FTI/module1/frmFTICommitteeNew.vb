Imports System.Windows.Forms

Public Class frmFTICommitteeNew

    Dim ds As DataSet

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If ComboBox2.SelectedValue.ToString.Length > 0 And TextBox30.TextLength > 0 Then
            'check existing
            Dim pRep As New Dictionary(Of String, Object)
            pRep.Add("@p0", ComboBox2.SelectedValue.ToString)
            pRep.Add("@p1", TextBox30.Text)

            Dim qRep As String = String.Empty
            qRep &= "SELECT COUNT(*) AS CNT * "
            qRep &= "FROM MB_COMMITTEE_PERIOD "
            qRep &= "WHERE (PERIOD_CODE = @p0) AND (CONTACT_CODE = @p1)"

            Dim CNT As Integer = 0

            Try
                CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
            Catch ex As Exception
                MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT MB_COMMITTEE_PERIOD")
            End Try

            If CNT = 0 Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("พบ " & ComboBox2.SelectedValue.ToString & " " & TextBox30.Text & " ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
            End If
        End If
    End Sub

    Private Sub getMB_PERIOD()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_PERIOD ORDER BY PERIOD_NAME DESC"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_PERIOD").Copy

        If ds.Tables.Contains("MB_PERIOD") = True Then
            ds.Tables("MB_PERIOD").Clear()
            ds.Tables("MB_PERIOD").Merge(dt)
            ds.Tables("MB_PERIOD").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox2.DataSource = ds.Tables("MB_PERIOD")
        ComboBox2.DisplayMember = "PERIOD_NAME"
        ComboBox2.ValueMember = "PERIOD_CODE"
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIRepresentsNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet
        getMB_PERIOD()

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

    Private Sub btPositions_Click(sender As Object, e As EventArgs) Handles btContacts.Click
        Dim f As New frmMainContacts
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            TextBox30.Text = f.DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btPeriods_Click(sender As Object, e As EventArgs) Handles btPeriods.Click
        '
    End Sub
End Class
