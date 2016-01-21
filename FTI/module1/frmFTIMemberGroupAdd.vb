Imports System.Windows.Forms

Public Class frmFTIMemberGroupAdd

    Dim dvMB_MEMBER_MAIN_GROUP As DataView
    Dim dvMEMBER_GROUP As DataView
    Dim dvMEMBER_MAIN_TYPE As DataView
    Dim dvMEMBER_TYPE As DataView
    Dim ds As DataSet

    Friend MEMBER_MAIN_GROUP As String
    Friend MEMBER_GROUP As String
    Friend MEMBER_MAIN_TYPE As String
    Friend MEMBER_TYPE As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'check existing
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", ComboBox3.SelectedValue)
        parameters.Add("@p1", ComboBox4.SelectedValue)
        parameters.Add("@p2", TextBox21.Text)

        Dim query As String = "SELECT COUNT(*) AS CNT FROM MB_MEMBER WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_CODE = @p2)"

        Dim CNT As Integer = 1
        Try
            CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT")
            Exit Sub
        End Try

        If CNT = 0 Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            MessageBox.Show("พบรหัสสมาชิก " & TextBox21.Text & " อยู่แล้ว " & CNT & "รายการ")
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIMemberGroupAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        ds = New DataSet

        getMEMBER_MAIN_GROUP()
        getMEMBER_GROUP()
        getMEMBER_MAIN_TYPE()
        getMEMBER_TYPE()

        ComboBox3.SelectedValue = MEMBER_MAIN_GROUP
        ComboBox4.SelectedValue = MEMBER_GROUP
        ComboBox14.SelectedValue = MEMBER_MAIN_TYPE
        ComboBox15.SelectedValue = MEMBER_TYPE

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMEMBER_MAIN_GROUP()
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", "0")
        'parameters.Add("@p1", "1")
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT [MEMBER_MAIN_GROUP_CODE], [MEMBER_MAIN_GROUP_NAME], [INACTIVE] FROM [MB_MEMBER_MAIN_GROUP] "
        'query &= "WHERE ([INACTIVE] <> 'N') "
        query &= "ORDER BY MEMBER_MAIN_GROUP_CODE "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_MAIN_GROUP").Copy
        'dt.TableName = "MB_MEMBER_MAIN_GROUP"
        If ds.Tables.Contains("MB_MEMBER_MAIN_GROUP") = True Then
            ds.Tables("MB_MEMBER_MAIN_GROUP").Clear()
            ds.Tables("MB_MEMBER_MAIN_GROUP").Merge(dt)
            ds.Tables("MB_MEMBER_MAIN_GROUP").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        If dvMB_MEMBER_MAIN_GROUP Is Nothing Then
            dvMB_MEMBER_MAIN_GROUP = New DataView(ds.Tables("MB_MEMBER_MAIN_GROUP"))
            ComboBox3.DataSource = dvMB_MEMBER_MAIN_GROUP
            ComboBox3.DisplayMember = "MEMBER_MAIN_GROUP_NAME"
            ComboBox3.ValueMember = "MEMBER_MAIN_GROUP_CODE"

            ComboBox3.SelectedIndex = -1
        End If


    End Sub

    Private Sub getMEMBER_GROUP()
        'ByVal MEMBER_MAIN_GROUP_CODE As String
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", MEMBER_MAIN_GROUP_CODE)
        'parameters.Add("@p1", "1")
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        '[MEMBER_GROUP_CODE], [MEMBER_GROUP_NAME], [MEMBER_GROUP_NAME_EN], [INACTIVE]
        query &= "SELECT * FROM [MB_MEMBER_GROUP] "
        'query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) "
        query &= "ORDER BY [MEMBER_GROUP_CODE] "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_GROUP").Copy
        'dt.TableName = "MB_MEMBER_GROUP"
        If ds.Tables.Contains("MB_MEMBER_GROUP") = True Then
            ds.Tables("MB_MEMBER_GROUP").Clear()
            ds.Tables("MB_MEMBER_GROUP").Merge(dt)
            ds.Tables("MB_MEMBER_GROUP").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        If dvMEMBER_GROUP Is Nothing Then
            dvMEMBER_GROUP = New DataView(ds.Tables("MB_MEMBER_GROUP"))
            ComboBox4.DataSource = dvMEMBER_GROUP
            ComboBox4.DisplayMember = "MEMBER_GROUP_NAME"
            ComboBox4.ValueMember = "MEMBER_GROUP_CODE"

            ComboBox4.SelectedIndex = -1
        End If


    End Sub

    Private Sub getMEMBER_MAIN_TYPE()
        'ByVal MEMBER_MAIN_GROUP_CODE As String, ByVal MEMBER_GROUP_CODE As String
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", MEMBER_MAIN_GROUP_CODE)
        'parameters.Add("@p1", MEMBER_GROUP_CODE)
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        '[MEMBER_MAIN_TYPE_CODE], [MEMBER_MAIN_TYPE_NAME], [INACTIVE]
        query &= "SELECT * FROM [MB_MEMBER_MAIN_TYPE] "
        'query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) "
        query &= "ORDER BY MEMBER_MAIN_TYPE_CODE "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_MAIN_TYPE").Copy
        'dt.TableName = "MB_MEMBER_MAIN_TYPE"
        If ds.Tables.Contains("MB_MEMBER_MAIN_TYPE") = True Then
            ds.Tables("MB_MEMBER_MAIN_TYPE").Clear()
            ds.Tables("MB_MEMBER_MAIN_TYPE").Merge(dt)
            ds.Tables("MB_MEMBER_MAIN_TYPE").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        If dvMEMBER_MAIN_TYPE Is Nothing Then
            dvMEMBER_MAIN_TYPE = New DataView(ds.Tables("MB_MEMBER_MAIN_TYPE"))
            ComboBox14.DataSource = dvMEMBER_MAIN_TYPE
            ComboBox14.DisplayMember = "MEMBER_MAIN_TYPE_NAME"
            ComboBox14.ValueMember = "MEMBER_MAIN_TYPE_CODE"

            ComboBox14.SelectedIndex = -1
        End If


    End Sub

    Private Sub getMEMBER_TYPE()
        'ByVal MEMBER_MAIN_GROUP_CODE As String, ByVal MEMBER_GROUP_CODE As String, ByVal MEMBER_MAIN_TYPE_CODE As String
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", MEMBER_MAIN_GROUP_CODE)
        'parameters.Add("@p1", MEMBER_GROUP_CODE)
        'parameters.Add("@p2", MEMBER_MAIN_TYPE_CODE)
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        '[MEMBER_TYPE_CODE], [MEMBER_TYPE_NAME], [INACTIVE]
        query &= "SELECT * FROM [MB_MEMBER_TYPE] "
        'query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) "
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

        If dvMEMBER_TYPE Is Nothing Then
            dvMEMBER_TYPE = New DataView(ds.Tables("MB_MEMBER_TYPE"))
            ComboBox15.DataSource = dvMEMBER_TYPE
            ComboBox15.DisplayMember = "MEMBER_TYPE_NAME"
            ComboBox15.ValueMember = "MEMBER_TYPE_CODE"

            ComboBox15.SelectedIndex = -1
        End If

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedValue IsNot Nothing And dvMEMBER_GROUP IsNot Nothing Then dvMEMBER_GROUP.RowFilter = String.Format("MEMBER_MAIN_GROUP_CODE = '{0}'", ComboBox3.SelectedValue.ToString)
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        If ComboBox3.SelectedValue IsNot Nothing And ComboBox4.SelectedValue IsNot Nothing And dvMEMBER_MAIN_TYPE IsNot Nothing Then dvMEMBER_MAIN_TYPE.RowFilter = String.Format("MEMBER_MAIN_GROUP_CODE = '{0}' AND MEMBER_GROUP_CODE = '{1}'", ComboBox3.SelectedValue.ToString, ComboBox4.SelectedValue.ToString)
    End Sub

    Private Sub ComboBox14_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox14.SelectedIndexChanged
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
