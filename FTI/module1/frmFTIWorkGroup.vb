Imports System.Windows.Forms

Public Class frmFTIWorkGroup

    Dim ds As DataSet
    Dim bs As BindingSource
    Dim parameters As New Dictionary(Of String, Object)
    Dim query As String

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

        getMB_PRENAME()

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMB_PRENAME()
        'dv = New DataView(dt)
        Dim searchValue As String = "%" & TextBox1.Text.Trim.Replace(" ", "%") & "%"

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", searchValue)
        parameters.Add("@p1", searchValue)
        parameters.Add("@p2", searchValue)
        parameters.Add("@p3", searchValue)
        parameters.Add("@p4", searchValue)

        Dim query As String = String.Empty
        query &= "SELECT TOP 100  * "
        query &= "FROM            MB_COMMITTEE_WORK_GROUP "
        query &= "WHERE        (WORK_GROUP_NAME LIKE @p0) OR "
        query &= "                         (MEMBER_WITH LIKE @p1) OR (CHAIRMAN_NAME LIKE @p2) OR (CONTACT_WORK_GROUP LIKE @p3) OR (WORK_GROUP_FIELD LIKE @p4) "
        query &= "ORDER BY WORK_GROUP_NAME"

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "MB_COMMITTEE_WORK_GROUP")

        If ds.Tables.Contains("MB_COMMITTEE_WORK_GROUP") = True Then
            ds.Tables("MB_COMMITTEE_WORK_GROUP").Clear()
            ds.Tables("MB_COMMITTEE_WORK_GROUP").Merge(dt)
            ds.Tables("MB_COMMITTEE_WORK_GROUP").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            ds.Tables("MB_COMMITTEE_WORK_GROUP").PrimaryKey = New DataColumn() {ds.Tables("MB_COMMITTEE_WORK_GROUP").Columns("WORK_GROUP_CODE")}

            bs = New BindingSource(ds, "MB_COMMITTEE_WORK_GROUP")

            DataGridView1.DataSource = bs

            For i As Integer = 0 To DataGridView1.ColumnCount - 1
                DataGridView1.Columns(i).Visible = False
            Next
            DataGridView1.Columns("WORK_GROUP_CODE").Visible = True
            DataGridView1.Columns("WORK_GROUP_NAME").Visible = True
            DataGridView1.Columns("WORK_GROUP_SHORT_NAME").Visible = True

            DataGridView1.Columns("WORK_GROUP_NAME").Width = 150

            'blinding
            TextBox2.DataBindings.Add("Text", bs, "WORK_GROUP_CODE")
            TextBox3.DataBindings.Add("Text", bs, "WORK_GROUP_NAME")
            TextBox4.DataBindings.Add("Text", bs, "WORK_GROUP_SHORT_NAME")
            TextBox9.DataBindings.Add("Text", bs, "MEMBER_WITH")
            TextBox10.DataBindings.Add("Text", bs, "CHAIRMAN_NAME")

            
        End If
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            getMB_PRENAME()
        End If
    End Sub

    Private Sub btApply_Click(sender As Object, e As EventArgs) Handles btApply.Click
        DataGridView1.EndEdit()
        'bs.EndEdit()

        If MessageBox.Show("ยืนยันที่จะ" & btApply.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            'Dim row As DataRow = ds.Tables("MB_PRENAME").Rows.Find(DataGridView1.CurrentRow.Cells("PRENAME_CODE").Value)

            Try
                Dim parameters As New Dictionary(Of String, Object)
                'parameters.Add("@p0", MODULE_ID)

                Dim query As String = String.Empty
                query &= "SELECT * "
                query &= "FROM            MB_PRENAME "
                'query &= "WHERE        (POSITION_NAME_TH LIKE @p0) OR "
                'query &= "                         (POSITION_NAME_EN LIKE @p1) "
                'query &= "ORDER BY POSITION_NAME_TH, POSITION_NAME_EN"

                If ds.Tables("MB_PRENAME").GetChanges IsNot Nothing Then
                    Try
                        updateWebSQL(query, parameters, ds.Tables("MB_PRENAME"))
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "updateWebSQL")
                    End Try
                End If

                MessageBox.Show("Apply Successfully")
            Catch ex As Exception
                MessageBox.Show(ex.Message, "MB_PRENAME.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            'refresh grid
            ds.Tables("MB_PRENAME").AcceptChanges()

            MessageBox.Show("บันทึกเสร็จสิ้น")
        End If
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        'new contact

        Dim f As New frmMainPreNameNew
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            'add it

            'generate code
            'SELECT TOP (1) POSITION_CODE FROM MB_POSITION ORDER BY CONVERT(INT, POSITION_CODE) DESC
            Dim parameters As New Dictionary(Of String, Object)
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            Dim query As String = "SELECT TOP (1) PRENAME_CODE FROM MB_PRENAME ORDER BY CONVERT(INT, PRENAME_CODE) DESC"

            Dim CNT As Integer = 0
            Try
                CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT")
            End Try

            parameters.Clear()
            parameters.Add("@p0", (CNT + 1).ToString)
            parameters.Add("@p1", f.TextBox9.Text)
            parameters.Add("@p2", f.TextBox30.Text)
            parameters.Add("@p3", f.NumericUpDown1.Value)

            query = "INSERT INTO MB_PRENAME (PRENAME_CODE, PRENAME_TH, PRENAME_EN, PRENAME_TYPE) VALUES (@p0,@p1,@p2,@p3)"

            Try
                executeWebSQL(query, parameters)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            End Try

            'logs
            'saveLOGS(CONTACT_CODE, "MB_CONTACT", "CONTACT_CODE", "ADD", "", f.TextBox9.Text, user_name)

            'refresh grid
            getMB_PRENAME()

            MessageBox.Show("เพิ่มเสร็จสิ้น")
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        'del contact

        If DataGridView1.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView1.CurrentRow.Cells("PRENAME_TH").Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'delete it
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("PRENAME_CODE").Value)

                Dim query As String = "DELETE FROM MB_PRENAME WHERE PRENAME_CODE = @p0"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'logs
                'saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT", "CONTACT_CODE", "ADD", "", TextBox9.Text, user_name)

                'refresh grid
                getMB_PRENAME()

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
