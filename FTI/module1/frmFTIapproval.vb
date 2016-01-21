Public Class frmFTIapproval

    Dim ds As DataSet
    Dim dv As DataView
    Friend MEMBER_CODE As String

    Private Sub frmFTIapproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet

        Label3.Text = String.Empty
        ComboBox1.SelectedIndex = 0

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", "000")
        parameters.Add("@p1", "100")
        parameters.Add("@p2", "200")
        parameters.Add("@p3", "999")
        'parameters.Add("@p1", "000")

        Dim query As String = String.Empty
        query &= "SELECT    MB_MEMBER.*, MB_COMP_PERSON.*, MB_MEMBER_MAIN_GROUP.*, MB_MEMBER_GROUP.* "
        query &= "FROM            MB_MEMBER INNER JOIN "
        query &= "                         MB_COMP_PERSON ON MB_MEMBER.OU_CODE = MB_COMP_PERSON.OU_CODE AND MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
        query &= "                         MB_MEMBER_MAIN_GROUP ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE INNER JOIN "
        query &= "                         MB_MEMBER_GROUP ON MB_MEMBER.MEMBER_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_GROUP_CODE AND "
        query &= "                         MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_MAIN_GROUP_CODE "
        query &= "WHERE        (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN (@p0, @p1, @p2, @p3)) AND (MB_MEMBER.MEMBER_STATUS_CODE = 'X') "

        If MEMBER_CODE = "<AUTO>" Then
            query &= " AND (MB_MEMBER.MEMBER_CODE = '<AUTO>') "
        Else
            query &= " AND (MB_MEMBER.MEMBER_CODE <> '<AUTO>') "
        End If

        'query &= "ORDER BY REGIST_DATE DESC "

        Dim dt As DataTable = New DataTable
        Try
            dt = fillWebSQL(query, parameters, "MB_MEMBER")
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
        End Try

        If ds.Tables.Contains("MB_MEMBER") = True Then
            ds.Tables("MB_MEMBER").Clear()
            ds.Tables("MB_MEMBER").Merge(dt)
            ds.Tables("MB_MEMBER").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        Label3.Text = String.Format("พบ {0} รายการ", ds.Tables("MB_MEMBER").Rows.Count.ToString("#,##0"))

        dv = New DataView(ds.Tables("MB_MEMBER"))

        ds.Tables("MB_MEMBER").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER").Columns("ROWID")}

        DataGridView1.DataSource = dv

        For i As Integer = 0 To DataGridView1.ColumnCount - 1
            DataGridView1.Columns(i).Visible = False
        Next
        'MEMBER_MAIN_GROUP_CODE, MEMBER_GROUP_CODE, MEMBER_MAIN_TYPE_CODE, MEMBER_TYPE_CODE
        DataGridView1.Columns("REGIST_CODE").Visible = True

        'DataGridView1.Columns("MEMBER_MAIN_GROUP_CODE").Visible = True
        'DataGridView1.Columns("MEMBER_GROUP_CODE").Visible = True
        'DataGridView1.Columns("MEMBER_MAIN_TYPE_CODE").Visible = True
        'DataGridView1.Columns("MEMBER_TYPE_CODE").Visible = True ' MEMBER_CODE

        DataGridView1.Columns("MEMBER_CODE").Visible = True
        DataGridView1.Columns("MEMBER_STATUS_CODE").Visible = True
        DataGridView1.Columns("COMP_PERSON_NAME_TH").Visible = True
        DataGridView1.Columns("COMP_PERSON_NAME_EN").Visible = True
        DataGridView1.Columns("MEMBER_MAIN_GROUP_NAME").Visible = True
        DataGridView1.Columns("MEMBER_GROUP_NAME").Visible = True
        DataGridView1.Columns("REGIST_DATE").Visible = True
        'DataGridView1.Columns("MEMBER_MAIN_TYPE_NAME").Visible = True
        'DataGridView1.Columns("MEMBER_TYPE_NAME").Visible = True

        'DataGridView1.Columns("MEMBER_CODE").Visible = True
        'DataGridView1.Columns("COMP_PERSON_NAME_TH").Visible = True
        'DataGridView1.Columns("COMP_PERSON_NAME_EN").Visible = True

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        '
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btApprove.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            If DataGridView1.SelectedRows.Count > 0 Then
                Dim parameters As New Dictionary(Of String, Object)
                Dim query As String = String.Empty

                For i As Integer = 0 To DataGridView1.SelectedRows.Count - 1
                    If DataGridView1.SelectedRows(i).Cells("MEMBER_CODE").Value.ToString <> "<AUTO>" Then
                        'update to active member // 
                        parameters.Clear()
                        parameters.Add("@p0", "A")
                        parameters.Add("@p1", "N")
                        parameters.Add("@p2", DateTimePicker1.Value)
                        parameters.Add("@p", DataGridView1.SelectedRows(i).Cells("ROWID").Value)

                        query = "UPDATE MB_MEMBER SET MEMBER_STATUS_CODE = @p0, STATUS_TYPE = @p1, MEMBER_DATE = @p2 WHERE ROWID = @p"
                        Try
                            executeWebSQL(query, parameters)
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                        End Try
                    Else
                        MessageBox.Show(DataGridView1.SelectedRows(i).Cells("REGIST_CODE").Value.ToString & " ยังไม่จ่ายเงิน")
                    End If
                    
                Next

                'refresh grid
                parameters.Clear()
                parameters.Add("@p0", "000")
                parameters.Add("@p1", "100")
                parameters.Add("@p2", "200")
                parameters.Add("@p3", "999")
                'parameters.Add("@p1", "000")

                query = String.Empty
                query &= "SELECT    MB_MEMBER.*, MB_COMP_PERSON.*, MB_MEMBER_MAIN_GROUP.*, MB_MEMBER_GROUP.* "
                query &= "FROM            MB_MEMBER INNER JOIN "
                query &= "                         MB_COMP_PERSON ON MB_MEMBER.OU_CODE = MB_COMP_PERSON.OU_CODE AND MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
                query &= "                         MB_MEMBER_MAIN_GROUP ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE INNER JOIN "
                query &= "                         MB_MEMBER_GROUP ON MB_MEMBER.MEMBER_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_GROUP_CODE AND "
                query &= "                         MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_MAIN_GROUP_CODE "
                'query &= "WHERE        (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN (@p0, @p1, @p2, @p3)) AND (MB_MEMBER.MEMBER_CODE IS NULL) AND (MB_MEMBER.MEMBER_STATUS_CODE = 'X') "
                query &= "WHERE        (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN (@p0, @p1, @p2, @p3)) AND (MB_MEMBER.MEMBER_STATUS_CODE = 'X') "
                'query &= "ORDER BY REGIST_DATE DESC "

                Dim dt As DataTable = New DataTable
                Try
                    dt = fillWebSQL(query, parameters, "MB_MEMBER")
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
                End Try

                If ds.Tables.Contains("MB_MEMBER") = True Then
                    ds.Tables("MB_MEMBER").Clear()
                    ds.Tables("MB_MEMBER").Merge(dt)
                    ds.Tables("MB_MEMBER").AcceptChanges()
                Else
                    ds.Tables.Add(dt)
                End If

            End If

        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        For i As Integer = 0 To DataGridView1.RowCount - 1
            DataGridView1.Rows(i).Selected = True
        Next
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        For i As Integer = 0 To DataGridView1.RowCount - 1
            DataGridView1.Rows(i).Selected = False
        Next
    End Sub

    Private Sub btPaid_Click(sender As Object, e As EventArgs) Handles btPaid.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            If DataGridView1.SelectedRows.Count > 0 Then
                Dim parameters As New Dictionary(Of String, Object)
                Dim query As String = String.Empty

                For i As Integer = 0 To DataGridView1.SelectedRows.Count - 1
                    If DataGridView1.SelectedRows(i).Cells("MEMBER_MAIN_GROUP_CODE").Value.ToString = "000" Then
                        'FTI only
                        'get member code
                        parameters.Clear()
                        parameters.Add("@p0", DataGridView1.SelectedRows(i).Cells("MEMBER_MAIN_GROUP_CODE").Value)
                        parameters.Add("@p1", DataGridView1.SelectedRows(i).Cells("MEMBER_GROUP_CODE").Value)
                        parameters.Add("@p2", DataGridView1.SelectedRows(i).Cells("MEMBER_MAIN_TYPE_CODE").Value)
                        parameters.Add("@p3", DataGridView1.SelectedRows(i).Cells("MEMBER_TYPE_CODE").Value)

                        '000 000 10 11
                        'MessageBox.Show(DataGridView1.SelectedRows(i).Cells("MEMBER_MAIN_GROUP_CODE").Value.ToString & vbCrLf & DataGridView1.SelectedRows(i).Cells("MEMBER_GROUP_CODE").Value.ToString & vbCrLf & DataGridView1.SelectedRows(i).Cells("MEMBER_MAIN_TYPE_CODE").Value.ToString & vbCrLf & DataGridView1.SelectedRows(i).Cells("MEMBER_TYPE_CODE").Value.ToString)

                        query = "SELECT MEMBER_SHORT_NAME + CONVERT(varchar, (RUNNING+1)) AS MEMBER_CODE FROM MB_MEMBER_TYPE WHERE MEMBER_MAIN_GROUP_CODE = @p0 AND MEMBER_GROUP_CODE = @p1 AND MEMBER_MAIN_TYPE_CODE = @p2 AND MEMBER_TYPE_CODE = @p3"

                        Dim MEMBER_CODE As String = String.Empty
                        Try
                            MEMBER_CODE = client.ExecuteScalar(query, parameters, user_session).ToString
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
                            Exit Sub
                        End Try

                        'update running
                        query = "UPDATE MB_MEMBER_TYPE SET RUNNING = RUNNING+1 WHERE MEMBER_MAIN_GROUP_CODE = @p0 AND MEMBER_GROUP_CODE = @p1 AND MEMBER_MAIN_TYPE_CODE = @p2 AND MEMBER_TYPE_CODE = @p3 "
                        Try
                            executeWebSQL(query, parameters)
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=executeWebSQL")
                            Exit Sub
                        End Try

                        'update to active member // 
                        parameters.Clear()
                        parameters.Add("@p0", MEMBER_CODE) 'MEMBER_CODE
                        parameters.Add("@p", DataGridView1.SelectedRows(i).Cells("ROWID").Value)

                        query = "UPDATE MB_MEMBER SET MEMBER_CODE = @p0 WHERE ROWID = @p"
                        Try
                            executeWebSQL(query, parameters)
                        Catch ex As Exception
                            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                        End Try

                        'save log
                        saveLOGS(DataGridView1.CurrentRow.Cells("REGIST_CODE").Value.ToString, "FTI_MAIN", "REGIST_CODE", "ADD", "R", MEMBER_CODE, user_name)
                    End If
                Next

                'refresh grid
                parameters.Clear()
                parameters.Add("@p0", "000")
                parameters.Add("@p1", "100")
                parameters.Add("@p2", "200")
                parameters.Add("@p3", "999")
                'parameters.Add("@p1", "000")

                query = String.Empty
                query &= "SELECT    MB_MEMBER.*, MB_COMP_PERSON.*, MB_MEMBER_MAIN_GROUP.*, MB_MEMBER_GROUP.* "
                query &= "FROM            MB_MEMBER INNER JOIN "
                query &= "                         MB_COMP_PERSON ON MB_MEMBER.OU_CODE = MB_COMP_PERSON.OU_CODE AND MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
                query &= "                         MB_MEMBER_MAIN_GROUP ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE INNER JOIN "
                query &= "                         MB_MEMBER_GROUP ON MB_MEMBER.MEMBER_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_GROUP_CODE AND "
                query &= "                         MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_MAIN_GROUP_CODE "
                query &= "WHERE        (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN (@p0, @p1, @p2, @p3)) AND (MB_MEMBER.MEMBER_CODE IS NULL) AND (MB_MEMBER.MEMBER_STATUS_CODE = 'X') "
                'query &= "ORDER BY REGIST_DATE DESC "

                Dim dt As DataTable = New DataTable
                Try
                    dt = fillWebSQL(query, parameters, "MB_MEMBER")
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
                End Try

                If ds.Tables.Contains("MB_MEMBER") = True Then
                    ds.Tables("MB_MEMBER").Clear()
                    ds.Tables("MB_MEMBER").Merge(dt)
                    ds.Tables("MB_MEMBER").AcceptChanges()
                Else
                    ds.Tables.Add(dt)
                End If

            End If

        End If
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        'If e.KeyCode = Keys.Enter Then
        Dim search As String = TextBox1.Text.Trim
        search = search.Replace(" ", "%")
        dv.RowFilter = String.Format("MEMBER_CODE LIKE '%{0}%' OR COMP_PERSON_NAME_TH LIKE '%{0}%' OR COMP_PERSON_NAME_EN LIKE '%{0}%'", search)
        'End If
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub
End Class