Imports System.Windows.Forms

Public Class frmGS1reserveNew

    'Friend MODULE_ID As Integer
    Dim ds As DataSet

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If TextBox9.TextLength > 0 And TextBox1.TextLength > 0 And TextBox2.TextLength > 0 And ComboBox13.SelectedIndex >= 0 Then
            'check existing
            Dim pRep As New Dictionary(Of String, Object)
            pRep.Add("@p0", TextBox9.Text)
            pRep.Add("@p1", TextBox1.Text)

            'check duplicate on self
            Dim qRep As String = String.Empty
            qRep &= "SELECT COUNT(*) AS CNT "
            qRep &= "FROM MB_EAN13_RESERVE "
            qRep &= "WHERE (NO_PREFIX = @p0) AND (NO_BARCODE = @p1)"

            Dim CNT As Integer = 0

            Try
                CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
            Catch ex As Exception
                MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT MB_EAN13_RESERVE")
            End Try

            If CNT = 0 Then
                'check duplicate on A
                pRep.Clear()
                pRep.Add("@p0", "300")
                pRep.Add("@p1", TextBox9.Text & TextBox1.Text & TextBox2.Text)
                pRep.Add("@p2", ComboBox13.SelectedValue)
                pRep.Add("@p3", TextBox9.Text)
                pRep.Add("@p4", TextBox2.Text)
                pRep.Add("@p5", True)

                'qRep &= "SELECT COUNT(*) AS CNT FROM dbo.fnc_EAN13check(@p0, @p1, @p2)"
                'qRep &= "SELECT COUNT(*) AS CNT FROM dbo.fnc_EAN13check(@p0, '8850002000000', 'A12', '885', 'XXXXXC', 1)"
                qRep = "SELECT COUNT(*) AS CNT FROM dbo.fnc_EAN13check(@p0, @p1, @p2, @p3, @p4, @p5)"

                'pRep.Clear()
                'pRep.Add("@p0", '300')
                'pRep.Add("@p1", TextBox9.Text & TextBox1.Text & TextBox2.Text)
                'pRep.Add("@p2", ComboBox13.SelectedValue)

                'qRep = "SELECT COUNT(*) AS CNT "
                'qRep &= "FROM FROM dbo.fnc_EAN13check(@p0, @p1, @p2) "

                CNT = 0

                Try
                    CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
                Catch ex As Exception
                    MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT fnc_EAN13check")
                End Try

                If CNT = 0 Then
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show("พบ " & TextBox1.Text & " ในฐานข้อมูลสมาชิกอยู่แล้วจำนวน " & CNT & " รายการ")
                End If
                
            Else
                MessageBox.Show("พบ " & TextBox1.Text & " ในฐานข้อมูลการจองอยู่แล้วจำนวน " & CNT & " รายการ")
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIRepresentsNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet

        TextBox9.Text = getParameters(2, "GS1_COUNTRY_CODE")

        getMEMBER_TYPE()

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    'Private Sub getMB_MEMBER_STATUS()
    '    Dim parameters As New Dictionary(Of String, Object)

    '    Dim query As String = String.Empty
    '    query &= "SELECT * FROM MB_MEMBER_STATUS WHERE MODULE = 2 ORDER BY MEMBER_STATUS_CODE"

    '    Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_STATUS").Copy

    '    If ds.Tables.Contains("MB_MEMBER_STATUS") = True Then
    '        ds.Tables("MB_MEMBER_STATUS").Clear()
    '        ds.Tables("MB_MEMBER_STATUS").Merge(dt)
    '        ds.Tables("MB_MEMBER_STATUS").AcceptChanges()
    '    Else
    '        ds.Tables.Add(dt)
    '    End If

    '    ComboBox13.DataSource = ds.Tables("MB_MEMBER_STATUS")
    '    ComboBox13.DisplayMember = "MEMBER_STATUS_NAME_TH"
    '    ComboBox13.ValueMember = "MEMBER_STATUS_CODE"
    'End Sub

    Private Sub getMEMBER_TYPE()
        'ByVal MEMBER_MAIN_GROUP_CODE As String, ByVal MEMBER_GROUP_CODE As String, ByVal MEMBER_MAIN_TYPE_CODE As String
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", "300")

        Dim query As String = String.Empty
        '[MEMBER_TYPE_CODE], [MEMBER_TYPE_NAME], [INACTIVE]
        query &= "SELECT * FROM [MB_MEMBER_TYPE] "
        'query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) "
        query &= "WHERE (MEMBER_MAIN_GROUP_CODE IN (@p0)) "
        query &= "ORDER BY [MEMBER_TYPE_CODE] "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_TYPE").Copy
        'dt.TableName = "MB_MEMBER_TYPE"
        If ds.Tables.Contains("MB_MEMBER_TYPE") = True Then
            ds.Tables("MB_MEMBER_TYPE").Clear()
            ds.Tables("MB_MEMBER_TYPE").Merge(dt)
            ds.Tables("MB_MEMBER_TYPE").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox13.DataSource = ds.Tables("MB_MEMBER_TYPE")
        ComboBox13.DisplayMember = "MEMBER_TYPE_NAME"
        ComboBox13.ValueMember = "MEMBER_TYPE_CODE"

    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub
End Class
