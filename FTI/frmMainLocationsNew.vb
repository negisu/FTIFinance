Imports System.Windows.Forms

Public Class frmMainLocationsNew

    Dim ds As DataSet

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If ComboBox1.SelectedValue.ToString.Length > 0 And TextBox2.TextLength > 0 And TextBox3.TextLength > 0 And ComboBox2.SelectedValue.ToString.Length > 0 And TextBox5.TextLength > 0 And TextBox6.TextLength > 0 Then
            'check existing
            Dim pRep As New Dictionary(Of String, Object)
            pRep.Add("@p0", ComboBox1.SelectedValue)
            pRep.Add("@p1", TextBox2.Text)
            pRep.Add("@p2", TextBox3.Text)
            pRep.Add("@p3", NumericUpDown1.Value)

            Dim qRep As String = String.Empty
            qRep &= "SELECT COUNT(*) AS CNT * "
            qRep &= "FROM MB_LOCATIONS "
            qRep &= "WHERE (PROVINCE_NAME_TH = @p0) AND (DISTRICT_NAME_TH = @p1) AND (SUB_DISTRICT_NAME_TH = @p2) AND (POSTCODE = @p3)"

            Dim CNT As Integer = 0

            Try
                CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
            Catch ex As Exception
                MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT MB_LOCATIONS")
            End Try

            If CNT = 0 Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("พบ " & ComboBox1.SelectedValue.ToString & " " & TextBox2.Text & " " & TextBox3.Text & " ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIRepresentsNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        ds = New DataSet

        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * "
        query &= "FROM            MB_PROVINCE "
        query &= "ORDER BY PROVINCE_NAME_TH"

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "MB_PROVINCE")

        If ds.Tables.Contains("MB_PROVINCE") = True Then
            ds.Tables("MB_PROVINCE").Clear()
            ds.Tables("MB_PROVINCE").Merge(dt)
            ds.Tables("MB_PROVINCE").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            ds.Tables("MB_PROVINCE").PrimaryKey = New DataColumn() {ds.Tables("MB_PROVINCE").Columns("PROVINCE_CODE")}

            ComboBox1.DataSource = ds.Tables("MB_PROVINCE")
            ComboBox1.ValueMember = "PROVINCE_NAME_TH"
            ComboBox1.DisplayMember = "PROVINCE_NAME_TH"

            ComboBox2.DataSource = ds.Tables("MB_PROVINCE")
            ComboBox2.ValueMember = "PROVINCE_NAME_EN"
            ComboBox2.DisplayMember = "PROVINCE_NAME_EN"
        End If

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
