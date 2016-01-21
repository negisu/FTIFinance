Imports System.Windows.Forms

Public Class frmMainLocations

    Dim ds As DataSet
    'Dim parameters As New Dictionary(Of String, Object)
    'Dim query As String

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
        'query = String.Empty

        ComboBox1.SelectedIndex = 0

        getLOCATIONS()

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
        parameters.Add("@p2", searchValue)
        parameters.Add("@p3", searchValue)
        parameters.Add("@p4", searchValue)
        parameters.Add("@p5", searchValue)
        parameters.Add("@p6", searchValue)

        Dim query As String = String.Empty
        query &= "SELECT TOP 100  * "
        query &= "FROM            MB_LOCATIONS "
        query &= "WHERE        (PROVINCE_NAME_TH LIKE @p0) OR "
        query &= "                         (DISTRICT_NAME_TH LIKE @p1) OR "
        query &= "                         (SUB_DISTRICT_NAME_TH LIKE @p2) OR "
        query &= "                         (PROVINCE_NAME_EN LIKE @p3) OR "
        query &= "                         (DISTRICT_NAME_EN LIKE @p4) OR "
        query &= "                         (SUB_DISTRICT_NAME_EN LIKE @p5) OR "
        query &= "                         (CONVERT(varchar, POSTCODE) LIKE @p6) "
        query &= "ORDER BY POSTCODE, PROVINCE_NAME_TH, DISTRICT_NAME_TH, SUB_DISTRICT_NAME_TH, PROVINCE_NAME_EN, DISTRICT_NAME_EN, SUB_DISTRICT_NAME_EN"

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "MB_LOCATIONS")

        If ds.Tables.Contains("MB_LOCATIONS") = True Then
            ds.Tables("MB_LOCATIONS").Clear()
            ds.Tables("MB_LOCATIONS").Merge(dt)
            ds.Tables("MB_LOCATIONS").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            ds.Tables("MB_LOCATIONS").PrimaryKey = New DataColumn() {ds.Tables("MB_LOCATIONS").Columns("LOCAL_CODE")}

            DataGridView1.DataSource = ds.Tables("MB_LOCATIONS")

            'For i As Integer = 0 To DataGridView1.ColumnCount - 1
            '    DataGridView1.Columns(i).Visible = False
            'Next
            'DataGridView1.Columns("LOCAL_CODE").Visible = True
            'DataGridView1.Columns("POSITION_NAME_TH").Visible = True
            'DataGridView1.Columns("POSITION_NAME_EN").Visible = True

            'DataGridView1.Columns("POSITION_NAME_TH").Width = 230
            'DataGridView1.Columns("POSITION_NAME_EN").Width = 230

            DataGridView1.Columns("LOCAL_CODE").ReadOnly = True
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
                Dim row As DataRow = ds.Tables("MB_LOCATIONS").Rows.Find(DataGridView1.CurrentRow.Cells("LOCAL_CODE").Value)

                Try
                    Dim parameters As New Dictionary(Of String, Object)
                    'parameters.Add("@p0", MODULE_ID)

                    Dim query As String = String.Empty
                    query &= "SELECT * "
                    query &= "FROM            MB_LOCATIONS "
                    'query &= "WHERE        (POSITION_NAME_TH LIKE @p0) OR "
                    'query &= "                         (POSITION_NAME_EN LIKE @p1) "
                    'query &= "ORDER BY POSITION_NAME_TH, POSITION_NAME_EN"

                    If ds.Tables("MB_LOCATIONS").GetChanges IsNot Nothing Then
                        Try
                            updateWebSQL(query, parameters, ds.Tables("MB_LOCATIONS"))
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "updateWebSQL")
                        End Try
                    End If

                    MessageBox.Show("Apply Successfully")
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "MB_LOCATIONS.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'refresh grid
                ds.Tables("MB_LOCATIONS").AcceptChanges()

                MessageBox.Show("บันทึกเสร็จสิ้น")
            End If


        End If
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        'new contact
        Dim f As New frmMainLocationsNew
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

            'add it

            'generate code
            'SELECT TOP (1) POSITION_CODE FROM MB_POSITION ORDER BY CONVERT(INT, POSITION_CODE) DESC
            Dim parameters As New Dictionary(Of String, Object)
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            Dim query As String = "SELECT TOP (1) LOCAL_CODE FROM MB_LOCATIONS ORDER BY LOCAL_CODE DESC"

            Dim CNT As Integer = 0
            Try
                CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT")
            End Try

            parameters.Clear()
            parameters.Add("@p0", (CNT + 1).ToString)
            parameters.Add("@p1", f.ComboBox1.SelectedValue)
            parameters.Add("@p2", f.TextBox2.Text)
            parameters.Add("@p3", f.TextBox3.Text)
            parameters.Add("@p4", f.ComboBox2.SelectedValue)
            parameters.Add("@p5", f.TextBox5.Text)
            parameters.Add("@p6", f.TextBox6.Text)
            parameters.Add("@p7", f.NumericUpDown1.Value)

            query = "INSERT INTO MB_LOCATIONS (LOCAL_CODE, PROVINCE_NAME_TH, DISTRICT_NAME_TH, SUB_DISTRICT_NAME_TH, PROVINCE_NAME_EN, DISTRICT_NAME_EN, SUB_DISTRICT_NAME_EN, POSTCODE) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7)"

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
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView1.CurrentRow.Cells("LOCAL_CODE").Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("LOCAL_CODE").Value)

                Dim query As String = "DELETE FROM MB_LOCATIONS WHERE LOCAL_CODE = @p0"

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

    Private Sub btFindIndus_Click(sender As Object, e As EventArgs) Handles btFindIndus.Click
        getLOCATIONS()
    End Sub
End Class

