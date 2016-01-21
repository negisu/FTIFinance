Imports System.Windows.Forms

Public Class frmMainPositions

    Dim ds As DataSet
    Dim parameters As New Dictionary(Of String, Object)
    Dim query As String

    Friend POSITION_TYPE As Integer

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmMainLocations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet
        query = String.Empty

        ComboBox1.SelectedIndex = 0

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getLOCATIONS()
        'dv = New DataView(dt)
        Dim searchValue As String = TextBox1.Text.Trim.Replace(" ", "%")
        searchValue = "%" & searchValue & "%"

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", searchValue)
        parameters.Add("@p1", searchValue)

        Dim query As String = String.Empty
        query &= "SELECT TOP 100  * "
        query &= "FROM            MB_POSITION "
        query &= "WHERE        ((POSITION_NAME_TH LIKE @p0) OR "
        query &= "                         (POSITION_NAME_EN LIKE @p1)) "

        If POSITION_TYPE > 0 Then
            query &= String.Format("AND (POSITION_TYPE = {0}) ", POSITION_TYPE)
        End If

        query &= "ORDER BY POSITION_NAME_TH, POSITION_NAME_EN"

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "MB_POSITION")

        If ds.Tables.Contains("MB_POSITION") = True Then
            ds.Tables("MB_POSITION").Clear()
            ds.Tables("MB_POSITION").Merge(dt)
            ds.Tables("MB_POSITION").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            ds.Tables("MB_POSITION").PrimaryKey = New DataColumn() {ds.Tables("MB_POSITION").Columns("POSITION_CODE")}

            DataGridView1.DataSource = ds.Tables("MB_POSITION")

            For i As Integer = 0 To DataGridView1.ColumnCount - 1
                DataGridView1.Columns(i).Visible = False
            Next
            DataGridView1.Columns("POSITION_CODE").Visible = True
            DataGridView1.Columns("POSITION_NAME_TH").Visible = True
            DataGridView1.Columns("POSITION_NAME_EN").Visible = True

            DataGridView1.Columns("POSITION_NAME_TH").Width = 230
            DataGridView1.Columns("POSITION_NAME_EN").Width = 230

            DataGridView1.Columns("POSITION_CODE").ReadOnly = True
        End If

        

        'ToolStripStatusLabel2.Text = dt.Rows.Count.ToString("#,##0")
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            getLOCATIONS()
        End If
    End Sub

    Private Sub btApply_Click(sender As Object, e As EventArgs) Handles btApply.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            'bs.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btApply.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_POSITION").Rows.Find(DataGridView1.CurrentRow.Cells("POSITION_CODE").Value)

                Try
                    Dim parameters As New Dictionary(Of String, Object)
                    'parameters.Add("@p0", MODULE_ID)

                    Dim query As String = String.Empty
                    query &= "SELECT * "
                    query &= "FROM            MB_POSITION "
                    'query &= "WHERE        (POSITION_NAME_TH LIKE @p0) OR "
                    'query &= "                         (POSITION_NAME_EN LIKE @p1) "
                    query &= "ORDER BY POSITION_NAME_TH, POSITION_NAME_EN"

                    If ds.Tables("MB_POSITION").GetChanges IsNot Nothing Then
                        Try
                            updateWebSQL(query, parameters, ds.Tables("MB_POSITION"))
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "updateWebSQL")
                        End Try
                    End If

                    MessageBox.Show("Apply Successfully")
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "MB_POSITION.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'refresh grid
                ds.Tables("MB_POSITION").AcceptChanges()

                MessageBox.Show("บันทึกเสร็จสิ้น")
            End If


        End If
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        'new contact
        Dim f As New frmMainPositionsNew
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

            'add it

            'generate code
            'SELECT TOP (1) POSITION_CODE FROM MB_POSITION ORDER BY CONVERT(INT, POSITION_CODE) DESC
            Dim parameters As New Dictionary(Of String, Object)
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            Dim query As String = "SELECT TOP (1) POSITION_CODE FROM MB_POSITION ORDER BY CONVERT(INT, POSITION_CODE) DESC"

            Dim CNT As Integer = 0
            Try
                CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT")
            End Try

            parameters.Clear()
            parameters.Add("@p0", (CNT + 1).ToString)
            parameters.Add("@p1", f.ComboBox1.Text)
            parameters.Add("@p2", f.TextBox9.Text)
            parameters.Add("@p3", f.TextBox30.Text)

            query = "INSERT INTO MB_POSITION (POSITION_CODE, POSITION_TYPE, POSITION_NAME_TH, POSITION_NAME_EN) VALUES (@p0,@p1,@p2,@p3)"

            Try
                executeWebSQL(query, parameters)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            End Try

            'logs
            'saveLOGS(CONTACT_CODE, "MB_CONTACT", "CONTACT_CODE", "ADD", "", f.TextBox9.Text, user_name)

            'refresh grid
            getLOCATIONS()

            MessageBox.Show("เพิ่มเสร็จสิ้น")
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        'del contact
        If DataGridView1.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView1.CurrentRow.Cells("POSITION_NAME_TH").Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("POSITION_CODE").Value)

                Dim query As String = "DELETE FROM MB_POSITION WHERE POSITION_CODE = @p0"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'logs
                'saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT", "CONTACT_CODE", "ADD", "", TextBox9.Text, user_name)

                'refresh grid
                getLOCATIONS()

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
        
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub
End Class
