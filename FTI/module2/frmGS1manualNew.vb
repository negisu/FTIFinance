Imports System.Windows.Forms

Public Class frmGS1manualNew

    Friend MEMBER_TYPE_CODE As String
    Friend BARCODE As String

    Dim NO_PREFIX As String
    Dim NO_XXXC As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If MaskedTextBox1.TextLength > 0 Then
            BARCODE = MaskedTextBox1.Text.Replace("Z", "C")
            'check existing
            Dim pRep As New Dictionary(Of String, Object)
            pRep.Add("@p0", "300")
            pRep.Add("@p1", BARCODE)
            pRep.Add("@p2", MEMBER_TYPE_CODE)
            pRep.Add("@p3", NO_PREFIX)
            pRep.Add("@p4", NO_XXXC)
            pRep.Add("@p5", True)

            Dim qRep As String = String.Empty
            'qRep &= "SELECT COUNT(*) AS CNT FROM dbo.fnc_EAN13check(@p0, @p1, @p2)"
            'qRep &= "SELECT COUNT(*) AS CNT FROM dbo.fnc_EAN13check(@p0, '8850002000000', 'A12', '885', 'XXXXXC', 1)"
            qRep &= "SELECT COUNT(*) AS CNT FROM dbo.fnc_EAN13check(@p0, @p1, @p2, @p3, @p4, @p5)"

            Dim CNT As Integer = 0

            Try
                CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
            Catch ex As Exception
                MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT GROUPS")
            End Try

            If CNT = 0 Then
                pRep.Clear()
                pRep.Add("@p0", BARCODE)

                qRep = "SELECT dbo.getEANcheckDigit(@p0)"

                Try
                    BARCODE = client.ExecuteScalar(qRep, pRep, user_session).ToString
                Catch ex As Exception
                    MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT GROUPS")
                End Try

                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("พบ " & MaskedTextBox1.Text & " ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIRepresentsNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'getinfo
        'Dim NO_PREFIX As String
        'Dim NO_XXXC As String
        'Dim NO_FROM As String

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", MEMBER_TYPE_CODE)

        Dim query As String = String.Empty
        query &= "SELECT        TOP (1) NO_PREFIX, NO_FROM, NO_TO, NO_XXXC, RUNNING, MEMBER_TYPE_CODE "
        query &= "FROM            MB_EAN13 "
        query &= "WHERE        (MEMBER_TYPE_CODE = @p0)"

        If MEMBER_TYPE_CODE IsNot Nothing Then
            If MEMBER_TYPE_CODE.Length > 0 Then
                query &= "WHERE MEMBER_TYPE_CODE = '" & MEMBER_TYPE_CODE & "'"
            End If
        End If

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "MB_EAN13")

        MaskedTextBox1.Mask = dt.Rows(0).Item("NO_PREFIX").ToString & New String("0"c, dt.Rows(0).Item("NO_FROM").ToString.Length - dt.Rows(0).Item("RUNNING").ToString.Length - 1) & New String("Y"c, dt.Rows(0).Item("RUNNING").ToString.Length - 1) & dt.Rows(0).Item("NO_XXXC").ToString.Replace("C", "Z")

        NO_PREFIX = dt.Rows(0).Item("NO_PREFIX").ToString
        NO_XXXC = dt.Rows(0).Item("NO_XXXC").ToString

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
