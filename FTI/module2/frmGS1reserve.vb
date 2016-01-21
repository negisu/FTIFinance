Imports System.Windows.Forms

Public Class frmGS1reserve

    'Friend MODULE_ID As Integer
    Friend MEMBER_TYPE_CODE As String

    Dim ds As DataSet
    Dim parameters As New Dictionary(Of String, Object)
    Dim query As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            'it's ok to go
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIRepresentsNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet
        query = String.Empty

        ComboBox1.SelectedIndex = 0

        getMB_PRENAME(TextBox1.Text)

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMB_PRENAME(ByVal SEARCH As String)
        'dv = New DataView(dt)
        SEARCH = "%" & TextBox1.Text.Trim.Replace(" ", "%") & "%"

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", SEARCH)
        'parameters.Add("@p1", searchValue)

        Dim query As String = String.Empty
        query &= "SELECT * "
        query &= "FROM            MB_EAN13_RESERVE "
        query &= "WHERE        (NO_BARCODE LIKE @p0) "
        If MEMBER_TYPE_CODE IsNot Nothing Then
            If MEMBER_TYPE_CODE.Length > 0 Then
                query &= "AND        (MEMBER_TYPE_CODE = '" & MEMBER_TYPE_CODE & "') "
            End If
        End If
        query &= "ORDER BY NO_PREFIX, NO_BARCODE"

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "MB_EAN13_RESERVE")

        If ds.Tables.Contains("MB_EAN13_RESERVE") = True Then
            ds.Tables("MB_EAN13_RESERVE").Clear()
            ds.Tables("MB_EAN13_RESERVE").Merge(dt)
            ds.Tables("MB_EAN13_RESERVE").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            ds.Tables("MB_EAN13_RESERVE").PrimaryKey = New DataColumn() {ds.Tables("MB_EAN13_RESERVE").Columns("NO_PREFIX"), ds.Tables("MB_EAN13_RESERVE").Columns("NO_BARCODE")}

            DataGridView1.DataSource = ds.Tables("MB_EAN13_RESERVE")

            'For i As Integer = 0 To DataGridView1.ColumnCount - 1
            '    DataGridView1.Columns(i).Visible = False
            'Next
            'DataGridView1.Columns("DIV_CODE").Visible = True
            'DataGridView1.Columns("DIV_NAME").Visible = True
            'DataGridView1.Columns("ABTN_DIV_NAME").Visible = True
            'DataGridView1.Columns("PROVINCE_NAME_EN").Visible = True
            '.Columns("PROVINCE_AREA").Visible = True

            'DataGridView1.Columns("DIV_NAME").Width = 150
            'DataGridView1.Columns("PROVINCE_NAME_EN").Width = 150

            DataGridView1.Columns("ID").ReadOnly = True
            DataGridView1.Columns("RANG_FROM").ReadOnly = True
            DataGridView1.Columns("RANG_TO").ReadOnly = True
        End If
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            getMB_PRENAME(TextBox1.Text)
        End If
    End Sub

    Private Sub btApply_Click(sender As Object, e As EventArgs) Handles btApply.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            'bs.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btApply.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_EAN13_RESERVE").Rows.Find(New Object() {DataGridView1.CurrentRow.Cells("NO_PREFIX").Value, DataGridView1.CurrentRow.Cells("NO_BARCODE").Value})

                Try
                    Dim parameters As New Dictionary(Of String, Object)
                    'parameters.Add("@p0", MODULE_ID)

                    Dim query As String = String.Empty
                    query &= "SELECT * "
                    query &= "FROM            MB_EAN13_RESERVE "
                    'query &= "WHERE        (NO_BARCODE LIKE @p0) "
                    'query &= "ORDER BY DIV_NAME"

                    If ds.Tables("MB_EAN13_RESERVE").GetChanges IsNot Nothing Then
                        Try
                            updateWebSQL(query, parameters, ds.Tables("MB_EAN13_RESERVE"))
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "updateWebSQL")
                        End Try
                    End If

                    MessageBox.Show("Apply Successfully")
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "MB_EAN13_RESERVE.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'refresh grid
                ds.Tables("MB_EAN13_RESERVE").AcceptChanges()

                MessageBox.Show("บันทึกเสร็จสิ้น")
            End If


        End If
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        'new contact
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmGS1reserveNew
        'f.MODULE_ID = MODULE_ID
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

            'add it

            'generate code
            'SELECT TOP (1) POSITION_CODE FROM MB_POSITION ORDER BY CONVERT(INT, POSITION_CODE) DESC
            Dim parameters As New Dictionary(Of String, Object)
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            Dim query As String = "INSERT INTO MB_EAN13_RESERVE (NO_PREFIX, NO_BARCODE, NO_XXXC, MEMBER_TYPE_CODE, NOTE) VALUES (@p0,@p1,@p2,@p3,@p4)"

            parameters.Clear()
            parameters.Add("@p0", f.TextBox9.Text)
            parameters.Add("@p1", f.TextBox1.Text)
            parameters.Add("@p2", f.TextBox2.Text)
            parameters.Add("@p3", f.ComboBox13.SelectedValue)
            parameters.Add("@p4", f.TextBox4.Text)

            Try
                executeWebSQL(query, parameters)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            End Try

            'logs
            'saveLOGS(CONTACT_CODE, "MB_CONTACT", "CONTACT_CODE", "ADD", "", f.TextBox9.Text, user_name)

            'refresh grid
            getMB_PRENAME(TextBox1.Text)

            MessageBox.Show("เพิ่มเสร็จสิ้น")
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        'del contact
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If DataGridView1.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView1.CurrentRow.Cells("NO_BARCODE").Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("ID").Value)

                Dim query As String = "DELETE FROM MB_EAN13_RESERVE WHERE ID = @p0"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'logs
                'saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT", "CONTACT_CODE", "ADD", "", TextBox9.Text, user_name)

                'refresh grid
                getMB_PRENAME(TextBox1.Text)

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

    Private Sub btFind_Click(sender As Object, e As EventArgs) Handles btFind.Click
        getMB_PRENAME(TextBox1.Text)
    End Sub
End Class
