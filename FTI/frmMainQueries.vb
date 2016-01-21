Imports Microsoft.Reporting.WinForms

Public Class frmMainQueries

    Dim ds As DataSet
    Dim bs As BindingSource

    Dim bQUERY_ENABLED_INSERT As Boolean ' = CBool(getParameters(0, "QUERY_ENABLED_INSERT"))
    Dim bQUERY_ENABLED_UPDATE As Boolean ' = CBool(getParameters(0, "QUERY_ENABLED_UPDATE"))
    Dim bQUERY_ENABLED_DELETE As Boolean ' = CBool(getParameters(0, "QUERY_ENABLED_DELETE"))

    Private Sub frmFTIapproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet

        bQUERY_ENABLED_INSERT = CBool(getParameters(0, "QUERY_ENABLED_INSERT"))
        bQUERY_ENABLED_UPDATE = CBool(getParameters(0, "QUERY_ENABLED_UPDATE"))
        bQUERY_ENABLED_DELETE = CBool(getParameters(0, "QUERY_ENABLED_DELETE"))

        getMB_QUERIES()

        bs = New BindingSource(ds, "MB_QUERIES")
        DataGridView2.DataSource = bs

        'DataGridView2.DataSource = ds.Tables("MB_QUERIES")

        'ds.Tables("MB_MEMBER").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER").Columns("REGIST_CODE")}

        For i As Integer = 0 To DataGridView2.ColumnCount - 1
            DataGridView2.Columns(i).Visible = False
        Next
        'MEMBER_MAIN_GROUP_CODE, MEMBER_GROUP_CODE, MEMBER_MAIN_TYPE_CODE, MEMBER_TYPE_CODE
        DataGridView2.Columns("QUERY_NAME").Visible = True
        'DataGridView1.Columns("MEMBER_GROUP_CODE").Visible = True
        'DataGridView1.Columns("MEMBER_MAIN_TYPE_CODE").Visible = True
        'DataGridView1.Columns("MEMBER_TYPE_CODE").Visible = True ' MEMBER_CODE
        'DataGridView1.Columns("MEMBER_CODE").Visible = True
        'DataGridView1.Columns("COMP_PERSON_NAME_TH").Visible = True
        'DataGridView1.Columns("COMP_PERSON_NAME_EN").Visible = True

        TextBox1.DataBindings.Add(New Binding("Text", bs, "QUERY_VALUE", True, DataSourceUpdateMode.OnValidation))

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

    Private Sub getMB_QUERIES()
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", "000")
        'parameters.Add("@p1", "100")
        'parameters.Add("@p2", "200")
        'parameters.Add("@p3", "999")
        'parameters.Add("@p1", "000")

        Dim query As String = String.Empty
        query &= "SELECT   * FROM MB_QUERIES WHERE QUERY_TYPE = 0 ORDER BY QUERY_NAME "

        Dim dt As DataTable = New DataTable
        Try
            dt = fillWebSQL(query, parameters, "MB_QUERIES")
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
        End Try

        If ds.Tables.Contains("MB_QUERIES") = True Then
            ds.Tables("MB_QUERIES").Clear()
            ds.Tables("MB_QUERIES").Merge(dt)
            ds.Tables("MB_QUERIES").AcceptChanges()
        Else
            ds.Tables.Add(dt)
            ds.Tables("MB_QUERIES").PrimaryKey = New DataColumn() {ds.Tables("MB_QUERIES").Columns("ID")}
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btSaveAs.Click
        If DataGridView1.Rows.Count > 0 Then
            If SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                'dtResult.WriteXml(SaveFileDialog1.FileName)
                Dim err As String = String.Empty
                Dim dsResult As New DataSet
                dsResult.Tables.Add(ds.Tables("RESULTS").Copy)

                Try
                    ExportDataSet(dsResult, SaveFileDialog1.FileName, err)
                    'saveAsXLSX(dsResult, SaveFileDialog1.FileName, err)
                    MessageBox.Show("Save Completed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show(ex.Message & vbCrLf & err)
                End Try
            End If
        End If
    End Sub

    Private Sub btExecute_Click(sender As Object, e As EventArgs) Handles btExecute.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Dim QueryType As String = TextBox1.Text.Substring(0, 6).ToUpper
        Dim results As Integer = 0

        'Console.Write("QueryType " & QueryType & vbCrLf)

        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = TextBox1.Text

        'sql mode
        Select Case QueryType.ToUpper
            Case "SELECT"
                Dim dt As DataTable = New DataTable
                Try
                    dt = fillWebSQL(query, parameters, "RESULTS")
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
                End Try

                If ds.Tables.Contains("RESULTS") = True Then ds.Tables.Remove("RESULTS")

                ds.Tables.Add(dt)

                DataGridView1.DataSource = Nothing
                DataGridView1.Columns.Clear()

                DataGridView1.DataSource = ds.Tables("RESULTS")

            Case "INSERT"
                If bQUERY_ENABLED_INSERT = True Then

                    Try
                        results = executeWebSQL(query, parameters)

                        MessageBox.Show("ดำเนินการเรียบร้อย, มีผล " & results & " รายการ")
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    DataGridView1.DataSource = Nothing
                    DataGridView1.Columns.Clear()
                Else
                    MessageBox.Show("ขออภัย ระบบไม่รองรับ " & QueryType.ToUpper)
                End If
            Case "UPDATE"
                If bQUERY_ENABLED_UPDATE = True Then

                    Try
                        results = executeWebSQL(query, parameters)

                        MessageBox.Show("ดำเนินการเรียบร้อย, มีผล " & results & " รายการ")
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    DataGridView1.DataSource = Nothing
                    DataGridView1.Columns.Clear()
                Else
                    MessageBox.Show("ขออภัย ระบบไม่รองรับ " & QueryType.ToUpper)
                End If
            Case "DELETE"
                If bQUERY_ENABLED_DELETE = True Then

                    Try
                        results = executeWebSQL(query, parameters)

                        MessageBox.Show("ดำเนินการเรียบร้อย, มีผล " & results & " รายการ")
                    Catch ex As Exception
                        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                    End Try

                    DataGridView1.DataSource = Nothing
                    DataGridView1.Columns.Clear()
                Else
                    MessageBox.Show("ขออภัย ระบบไม่รองรับ " & QueryType.ToUpper)
                End If
            Case Else
                'skip
                MessageBox.Show("ขออภัย ระบบไม่รองรับ " & QueryType.ToUpper)
        End Select
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        'add it

        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = "INSERT INTO MB_QUERIES (QUERY_NAME, QUERY_TYPE) VALUES (@p0,@p1)"

        parameters.Clear()
        parameters.Add("@p0", "NEW QUERY")
        parameters.Add("@p1", 0)

        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        'logs
        'saveLOGS(CONTACT_CODE, "MB_CONTACT", "CONTACT_CODE", "ADD", "", f.TextBox9.Text, user_name)

        'refresh grid
        getMB_QUERIES()

        MessageBox.Show("เพิ่มเสร็จสิ้น")
    End Sub

    Private Sub btDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        'del 
        If DataGridView2.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView2.CurrentRow.Cells("QUERY_NAME").Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView2.CurrentRow.Cells("ID").Value)

                Dim query As String = "DELETE FROM MB_QUERIES WHERE ID = @p0"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'logs
                'saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT", "CONTACT_CODE", "ADD", "", TextBox9.Text, user_name)

                'refresh grid
                getMB_QUERIES()

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btApply_Click(sender As Object, e As EventArgs) Handles btApply.Click
        'If DataGridView2.CurrentRow IsNot Nothing Then
        DataGridView2.EndEdit()
        bs.EndEdit()

        If ds.Tables.Contains("MB_QUERIES") = True Then
            If ds.Tables("MB_QUERIES").GetChanges IsNot Nothing Then
                If MessageBox.Show("ยืนยันที่จะ" & btApply.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim row As DataRow = ds.Tables("MB_QUERIES").Rows.Find(DataGridView2.CurrentRow.Cells("ID").Value)

                    Try
                        Dim parameters As New Dictionary(Of String, Object)
                        'parameters.Add("@p0", MODULE_ID)

                        Dim query As String = String.Empty
                        query &= "SELECT * FROM MB_QUERIES WHERE QUERY_TYPE = 0 ORDER BY QUERY_NAME "

                        If ds.Tables("MB_QUERIES").GetChanges IsNot Nothing Then
                            Try
                                updateWebSQL(query, parameters, ds.Tables("MB_QUERIES"))
                            Catch ex As Exception
                                MessageBox.Show(ex.Message, "updateWebSQL")
                            End Try
                        End If

                        'MessageBox.Show("Apply Successfully")
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "MB_QUERIES.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    'refresh grid
                    ds.Tables("MB_QUERIES").AcceptChanges()

                    MessageBox.Show("บันทึกเสร็จสิ้น")
                End If
            End If
            '
        End If
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