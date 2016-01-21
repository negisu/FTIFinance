Imports System.Windows.Forms

Public Class frmFINSubSection
    Public VATType As String = Nothing
    Dim ds As DataSet
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
        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
        TextBox1.Select()
    End Sub

    Private Sub getIV_SUB_SECTION()
        'dv = New DataView(dt)
        Dim searchValue As String = TextBox1.Text.Trim.Replace(" ", "%")
        searchValue = "%" & searchValue & "%"

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", searchValue)

        Dim query As String = String.Empty
        query &= "SELECT TOP 100  * "
        query &= "FROM            IV_SUB_SECTION "
        query &= "LEFT JOIN SU_DIVISION ON IV_SUB_SECTION.DIV_CODE_INC=SU_DIVISION.DIV_CODE "
        query &= "LEFT JOIN PL_ACTIVITY ON IV_SUB_SECTION.ATV_CODE_INC=PL_ACTIVITY.ATV_CODE "
        query &= "LEFT JOIN PL_PROJECT ON IV_SUB_SECTION.PROJ_ID_INC=PL_PROJECT.PROJ_ID "
        query &= "WHERE (SUB_SECTION_NAME LIKE @p0 "
        query &= "OR SUB_SECTION_CODE LIKE @p0 "
        query &= "OR DIV_NAME LIKE @p0 "
        query &= "OR PROJ_NAME LIKE @p0 "
        query &= "OR ATV_NAME LIKE @p0) "

        If user_div = "AAA-AA" Then
        Else
            query &= " AND IV_SUB_SECTION.DIV_CODE_INC = @p1 "
            parameters.Add("@p1", user_div)

        End If

        If Not String.IsNullOrEmpty(VATType) Then
            query &= " AND VAT_TYPE = @p2 "
            parameters.Add("@p2", VATType)
        End If
        query &= "ORDER BY SUB_SECTION_NAME"

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "IV_SUB_SECTION")

        If ds.Tables.Contains("IV_SUB_SECTION") = True Then
            ds.Tables("IV_SUB_SECTION").Clear()
            ds.Tables("IV_SUB_SECTION").Merge(dt)
            ds.Tables("IV_SUB_SECTION").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            ds.Tables("IV_SUB_SECTION").PrimaryKey = New DataColumn() {ds.Tables("IV_SUB_SECTION").Columns("SUB_SECTION_CODE")}

            DataGridView1.DataSource = ds.Tables("IV_SUB_SECTION")

            For i As Integer = 0 To DataGridView1.ColumnCount - 1
                DataGridView1.Columns(i).Visible = False
            Next
            DataGridView1.Columns("SUB_SECTION_CODE").Visible = True
            DataGridView1.Columns("SUB_SECTION_NAME").Visible = True
            DataGridView1.Columns("DIV_NAME").Visible = True
            DataGridView1.Columns("PROJ_NAME").Visible = True
            DataGridView1.Columns("ATV_NAME").Visible = True

            DataGridView1.Columns("SUB_SECTION_CODE").HeaderText = "รหัสสินค้า"
            DataGridView1.Columns("SUB_SECTION_NAME").HeaderText = "ชื่อสินค้า"
            DataGridView1.Columns("DIV_NAME").HeaderText = "หน่วยงาน"
            DataGridView1.Columns("PROJ_NAME").HeaderText = "โครงการ"
            DataGridView1.Columns("ATV_NAME").HeaderText = "กิจกรรม"
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            DataGridView1.AutoResizeColumns()

            'DataGridView1.Columns("SUB_SECTION_NAME").Width = 500

            DataGridView1.Columns("SUB_SECTION_CODE").ReadOnly = True

            DataGridView1.ClearSelection()
        End If
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            getIV_SUB_SECTION()
        End If
    End Sub

    Private Sub btApply_Click(sender As Object, e As EventArgs) Handles btApply.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            'bs.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btApply.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("IV_SUB_SECTION").Rows.Find(DataGridView1.CurrentRow.Cells("PRENAME_CODE").Value)

                Try
                    Dim parameters As New Dictionary(Of String, Object)
                    'parameters.Add("@p0", MODULE_ID)

                    Dim query As String = String.Empty
                    query &= "SELECT * "
                    query &= "FROM            IV_SUB_SECTION "

                    'query &= "WHERE        (POSITION_NAME_TH LIKE @p0) OR "
                    'query &= "                         (POSITION_NAME_EN LIKE @p1) "
                    'query &= "ORDER BY POSITION_NAME_TH, POSITION_NAME_EN"

                    If ds.Tables("IV_SUB_SECTION").GetChanges IsNot Nothing Then
                        Try
                            updateWebSQL(query, parameters, ds.Tables("IV_SUB_SECTION"))
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "updateWebSQL")
                        End Try
                    End If

                    MessageBox.Show("Apply Successfully")
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "IV_SUB_SECTION.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'refresh grid
                ds.Tables("IV_SUB_SECTION").AcceptChanges()

                MessageBox.Show("บันทึกเสร็จสิ้น")
            End If


        End If
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        'new contact
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainPreNameNew
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

            'add it

            'generate code
            'SELECT TOP (1) POSITION_CODE FROM MB_POSITION ORDER BY CONVERT(INT, POSITION_CODE) DESC
            Dim parameters As New Dictionary(Of String, Object)
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            Dim query As String = "SELECT TOP (1) PRENAME_CODE FROM IV_SUB_SECTION ORDER BY CONVERT(INT, PRENAME_CODE) DESC"

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

            query = "INSERT INTO IV_SUB_SECTION (PRENAME_CODE, PRENAME_TH, PRENAME_EN, PRENAME_TYPE) VALUES (@p0,@p1,@p2,@p3)"

            Try
                executeWebSQL(query, parameters)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            End Try

            'logs
            'saveLOGS(CONTACT_CODE, "MB_CONTACT", "CONTACT_CODE", "ADD", "", f.TextBox9.Text, user_name)

            'refresh grid
            getIV_SUB_SECTION()

            MessageBox.Show("เพิ่มเสร็จสิ้น")
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        'del contact
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If DataGridView1.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView1.CurrentRow.Cells("PRENAME_TH").Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("PRENAME_CODE").Value)

                Dim query As String = "DELETE FROM IV_SUB_SECTION WHERE PRENAME_CODE = @p0"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'logs
                'saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT", "CONTACT_CODE", "ADD", "", TextBox9.Text, user_name)

                'refresh grid
                getIV_SUB_SECTION()

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

    Private Sub DataGridView1_KeyUp(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = Keys.Enter Then
            OK_Button_Click(sender, e)
        End If
    End Sub


    Private Sub btFind_Click(sender As Object, e As EventArgs) Handles btFind.Click
        getIV_SUB_SECTION()
    End Sub

End Class
