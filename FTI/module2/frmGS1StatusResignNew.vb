Imports System.Windows.Forms

Public Class frmGS1StatusResignNew

    'Friend RETIRE_TYPE As String
    Friend REGIST_CODE As String

    Friend ds As DataSet

    Dim dvMB_MEMBER_MAIN_GROUP As DataView
    Dim dvMEMBER_GROUP As DataView
    Dim dvMEMBER_MAIN_TYPE As DataView
    Dim dvMEMBER_TYPE As DataView

    Friend MEMBER_MAIN_GROUP_CODE As String
    Friend MEMBER_GROUP_CODE As String
    Friend MEMBER_MAIN_TYPE_CODE As String
    Friend MEMBER_TYPE_CODE As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If GroupBox1.Enabled = True Then
            If ComboBox1.Text.Length > 0 And ComboBox3.SelectedValue.ToString.Length > 0 And ComboBox4.SelectedValue.ToString.Length > 0 And ComboBox14.SelectedValue.ToString.Length > 0 And ComboBox15.SelectedValue.ToString.Length > 0 Then
                'check existing
                Dim pRep As New Dictionary(Of String, Object)
                pRep.Add("@p0", TextBox1.Text)
                pRep.Add("@p1", TextBox2.Text)

                Dim qRep As String = String.Empty
                qRep &= "SELECT COUNT(*) AS CNT * "
                qRep &= "FROM MB_MEMBER_RETIRE "
                qRep &= "WHERE (REGIST_CODE = @p0) AND (RETIRE_TYPE = @p1) AND (APPROVE_DATE IS NULL)"

                Dim CNT As Integer = 0

                Try
                    CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
                Catch ex As Exception
                    MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT MB_CONTACT")
                End Try

                If CNT = 0 Then
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show("พบ " & TextBox1.Text & " " & TextBox2.Text & " ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
                End If
            Else
                MessageBox.Show("กรุณาเลือกประเภทสมาชิก")
            End If
        Else
            If ComboBox1.Text.Length > 0 Then
                'check existing
                Dim pRep As New Dictionary(Of String, Object)
                pRep.Add("@p0", TextBox1.Text)
                pRep.Add("@p1", TextBox2.Text)

                Dim qRep As String = String.Empty
                qRep &= "SELECT COUNT(*) AS CNT * "
                qRep &= "FROM MB_MEMBER_RETIRE "
                qRep &= "WHERE (REGIST_CODE = @p0) AND (RETIRE_TYPE = @p1) AND (APPROVE_DATE IS NULL)"

                Dim CNT As Integer = 0

                Try
                    CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
                Catch ex As Exception
                    MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT MB_CONTACT")
                End Try

                If CNT = 0 Then
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show("พบ " & TextBox1.Text & " " & TextBox2.Text & " ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
                End If
            Else
                MessageBox.Show("กรุณาเลือกประเภทสมาชิก")
            End If
        End If

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIRepresentsNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        Try
            '
        Catch ex As Exception

        End Try

        Dim sFTI_STATUS_REASON() As String = getParameters(1, "FTI_STATUS_REASON").Split(","c)
        ComboBox1.DataSource = sFTI_STATUS_REASON

        If TextBox1.Text = "T" Then
            GroupBox1.Enabled = True
        Else
            GroupBox1.Enabled = False
        End If

        If MEMBER_MAIN_GROUP_CODE IsNot Nothing Then
            dvMB_MEMBER_MAIN_GROUP = New DataView(ds.Tables("MB_MEMBER_MAIN_GROUP"))
            ComboBox3.DataSource = dvMB_MEMBER_MAIN_GROUP
            ComboBox3.DisplayMember = "MEMBER_MAIN_GROUP_NAME"
            ComboBox3.ValueMember = "MEMBER_MAIN_GROUP_CODE"

            dvMEMBER_GROUP = New DataView(ds.Tables("MB_MEMBER_GROUP"))
            ComboBox4.DataSource = dvMEMBER_GROUP
            ComboBox4.DisplayMember = "MEMBER_GROUP_NAME"
            ComboBox4.ValueMember = "MEMBER_GROUP_CODE"

            dvMEMBER_MAIN_TYPE = New DataView(ds.Tables("MB_MEMBER_MAIN_TYPE"))
            ComboBox14.DataSource = dvMEMBER_MAIN_TYPE
            ComboBox14.DisplayMember = "MEMBER_MAIN_TYPE_NAME"
            ComboBox14.ValueMember = "MEMBER_MAIN_TYPE_CODE"

            dvMEMBER_TYPE = New DataView(ds.Tables("MB_MEMBER_TYPE"))
            ComboBox15.DataSource = dvMEMBER_TYPE
            ComboBox15.DisplayMember = "MEMBER_TYPE_NAME"
            ComboBox15.ValueMember = "MEMBER_TYPE_CODE"

            dvMEMBER_GROUP.RowFilter = String.Format("MEMBER_MAIN_GROUP_CODE = '{0}'", MEMBER_MAIN_GROUP_CODE)
            dvMEMBER_MAIN_TYPE.RowFilter = String.Format("MEMBER_MAIN_GROUP_CODE = '{0}' AND MEMBER_GROUP_CODE = '{1}'", MEMBER_MAIN_GROUP_CODE, MEMBER_GROUP_CODE)
            dvMEMBER_TYPE.RowFilter = String.Format("MEMBER_MAIN_GROUP_CODE = '{0}' AND MEMBER_GROUP_CODE = '{1}' AND MEMBER_MAIN_TYPE_CODE = '{2}'", MEMBER_MAIN_GROUP_CODE, MEMBER_GROUP_CODE, MEMBER_MAIN_TYPE_CODE)

            ComboBox3.SelectedValue = MEMBER_MAIN_GROUP_CODE
            ComboBox4.SelectedValue = MEMBER_GROUP_CODE
            ComboBox14.SelectedValue = MEMBER_MAIN_TYPE_CODE
            ComboBox15.SelectedValue = MEMBER_TYPE_CODE

            'ComboBox3_SelectedIndexChanged(sender, e)
            'ComboBox4_SelectedIndexChanged(sender, e)
            'ComboBox14_SelectedIndexChanged(sender, e)
            'ComboBox3_SelectedIndexChanged(sender, e)
        End If

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedValueChanged
        If ComboBox3.SelectedValue IsNot Nothing And dvMEMBER_GROUP IsNot Nothing Then dvMEMBER_GROUP.RowFilter = String.Format("MEMBER_MAIN_GROUP_CODE = '{0}'", ComboBox3.SelectedValue.ToString)
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedValueChanged
        If ComboBox3.SelectedValue IsNot Nothing And ComboBox4.SelectedValue IsNot Nothing And dvMEMBER_MAIN_TYPE IsNot Nothing Then dvMEMBER_MAIN_TYPE.RowFilter = String.Format("MEMBER_MAIN_GROUP_CODE = '{0}' AND MEMBER_GROUP_CODE = '{1}'", ComboBox3.SelectedValue.ToString, ComboBox4.SelectedValue.ToString)
    End Sub

    Private Sub ComboBox14_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox14.SelectedValueChanged
        If ComboBox3.SelectedValue IsNot Nothing And ComboBox4.SelectedValue IsNot Nothing And ComboBox14.SelectedValue IsNot Nothing And dvMEMBER_TYPE IsNot Nothing Then dvMEMBER_TYPE.RowFilter = String.Format("MEMBER_MAIN_GROUP_CODE = '{0}' AND MEMBER_GROUP_CODE = '{1}' AND MEMBER_MAIN_TYPE_CODE = '{2}'", ComboBox3.SelectedValue.ToString, ComboBox4.SelectedValue.ToString, ComboBox14.SelectedValue.ToString)
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub
End Class
