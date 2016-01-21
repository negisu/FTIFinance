Imports System.Windows.Forms

Public Class frmFINAR_MEMBER

    Dim ds As DataSet
    Dim parameters As New Dictionary(Of String, Object)
    Dim query As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        

        If DataGridView1.CurrentRow IsNot Nothing Then


            If DocAddrGrid.CurrentRow IsNot Nothing Or MailAddrGrid.CurrentRow IsNot Nothing Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                If (MessageBox.Show("ที่อยู่ยังไม่ถูกเลือกหรือเลือกไม่ครบ คุณต้องการดำเนินการต่อ ใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Close()
                End If

            End If

            
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
        query &= "SELECT TOP 1000  * "
        query &= "FROM            MB_MEMBER "
        query &= "LEFT JOIN MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE "
        query &= "LEFT JOIN MB_MEMBER_STATUS ON MB_MEMBER.MEMBER_STATUS_CODE = MB_MEMBER_STATUS.MEMBER_STATUS_CODE  AND MB_MEMBER_STATUS.MODULE = '1' "
        query &= "LEFT JOIN MB_MEMBER_MAIN_GROUP ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE "
        query &= "WHERE MEMBER_CODE LIKE @p0 "
        query &= "OR COMP_PERSON_NAME_TH LIKE @p0 "
        query &= "OR COMP_PERSON_NAME_EN LIKE @p0 "
        query &= "OR MB_COMP_PERSON.COMP_PERSON_CODE LIKE @p0 "
        query &= "OR TAX_ID LIKE @p0 "
        query &= "ORDER BY MB_MEMBER.MEMBER_MAIN_GROUP_CODE "

        'query &= "SELECT TOP 1000  * "
        'query &= "FROM            MB_COMP_PERSON "
        'query &= "WHERE COMP_PERSON_NAME_TH LIKE @p0 "
        'query &= "OR COMP_PERSON_NAME_EN LIKE @p0 "
        'query &= "OR COMP_PERSON_CODE LIKE @p0 "
        'query &= "OR TAX_ID LIKE @p0 "
        'query &= "ORDER BY COMP_PERSON_CODE"

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "MB_MEMBER")

        If ds.Tables.Contains("MB_MEMBER") = True Then
            ds.Tables("MB_MEMBER").Clear()
            ds.Tables("MB_MEMBER").Merge(dt)
            ds.Tables("MB_MEMBER").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            'ds.Tables("MB_MEMBER").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER").Columns("MEMBER_CODE")}

            DataGridView1.DataSource = ds.Tables("MB_MEMBER")

            For i As Integer = 0 To DataGridView1.ColumnCount - 1
                DataGridView1.Columns(i).Visible = False
                DataGridView1.Columns(i).ReadOnly = True
                DataGridView1.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next
            DataGridView1.Columns("MEMBER_CODE").Visible = True
            DataGridView1.Columns("MEMBER_MAIN_GROUP_NAME").Visible = True
            DataGridView1.Columns("MEMBER_STATUS_NAME_TH").Visible = True
            DataGridView1.Columns("COMP_PERSON_CODE").Visible = True
            DataGridView1.Columns("TAX_ID").Visible = True
            DataGridView1.Columns("COMP_PERSON_NAME_TH").Visible = True
            DataGridView1.Columns("COMP_PERSON_NAME_EN").Visible = True

            DataGridView1.Columns("MEMBER_CODE").HeaderText = "รหัสสมาชิก"
            DataGridView1.Columns("MEMBER_MAIN_GROUP_NAME").HeaderText = "กลุ่มสมาชิก"
            DataGridView1.Columns("MEMBER_STATUS_NAME_TH").HeaderText = "สถานะ"
            DataGridView1.Columns("COMP_PERSON_CODE").HeaderText = "รหัสประจำตัว"
            DataGridView1.Columns("TAX_ID").HeaderText = "เลขประจำตัวผู้เสียภาษี"
            DataGridView1.Columns("COMP_PERSON_NAME_TH").HeaderText = "ชื่อภาษาไทย"
            DataGridView1.Columns("COMP_PERSON_NAME_EN").HeaderText = "English Name"

            DataGridView1.Columns("MEMBER_CODE").DisplayIndex = 0
            DataGridView1.Columns("MEMBER_MAIN_GROUP_NAME").DisplayIndex = 1
            DataGridView1.Columns("MEMBER_STATUS_NAME_TH").DisplayIndex = 2
            DataGridView1.Columns("COMP_PERSON_CODE").DisplayIndex = 3
            DataGridView1.Columns("TAX_ID").DisplayIndex = 4
            DataGridView1.Columns("COMP_PERSON_NAME_TH").DisplayIndex = 5
            DataGridView1.Columns("COMP_PERSON_NAME_EN").DisplayIndex = 6

            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            DataGridView1.AutoResizeColumns()


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

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DocAddrGrid.CellContentClick

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        If e.RowIndex = -1 Then
        Else

            Dim val As String = DataGridView1.Rows(e.RowIndex).Cells("COMP_PERSON_CODE").Value.ToString()
            Dim parameters As New Dictionary(Of String, Object)
            Dim query As String = String.Empty
            query &= "SELECT * "
            query &= "FROM            MB_COMP_PERSON_ADDRESS "
            query &= "WHERE           COMP_PERSON_CODE LIKE @p0 AND ADDR_CODE = 003"
            parameters.Add("@p0", val)

            DocAddrGrid.DataSource = fillWebSQL(query, parameters, "MB_COMP_PERSON_ADDRESS")

            For i As Integer = 0 To DocAddrGrid.ColumnCount - 1
                DocAddrGrid.Columns(i).Visible = False
            Next
            DocAddrGrid.Columns("ADDR_NOTE").DisplayIndex = 0
            DocAddrGrid.Columns("ADDR_NOTE").Visible = True
            DocAddrGrid.Columns("ADDR_MOO").Visible = True
            DocAddrGrid.Columns("ADDR_SOI").Visible = True
            DocAddrGrid.Columns("ADDR_ROAD").Visible = True
            DocAddrGrid.Columns("ADDR_SUB_DISTRICT").Visible = True
            DocAddrGrid.Columns("ADDR_DISTRICT").Visible = True
            DocAddrGrid.Columns("ADDR_PROVINCE_NAME").Visible = True
            DocAddrGrid.Columns("ADDR_POSTCODE").Visible = True
            DocAddrGrid.Columns("ADDR_TELEPHONE").Visible = True
            DocAddrGrid.Columns("ADDR_FAX").Visible = True

            DocAddrGrid.Columns("ADDR_NOTE").HeaderText = "สาขา"
            DocAddrGrid.Columns("ADDR_MOO").HeaderText = "หมู่"
            DocAddrGrid.Columns("ADDR_SOI").HeaderText = "ซอย"
            DocAddrGrid.Columns("ADDR_ROAD").HeaderText = "ถนน"
            DocAddrGrid.Columns("ADDR_SUB_DISTRICT").HeaderText = "แขวง/ตำบล"
            DocAddrGrid.Columns("ADDR_DISTRICT").HeaderText = "เขต/อำเภอ"
            DocAddrGrid.Columns("ADDR_PROVINCE_NAME").HeaderText = "จังหวัด"
            DocAddrGrid.Columns("ADDR_POSTCODE").HeaderText = "รหัสไปรษณีย์"
            DocAddrGrid.Columns("ADDR_TELEPHONE").HeaderText = "โทรศัพท์"
            DocAddrGrid.Columns("ADDR_FAX").HeaderText = "FAX"

            DocAddrGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            DocAddrGrid.AutoResizeColumns()
            DocAddrGrid.Refresh()
            DocAddrGrid.Update()

            DocAddrGrid.ClearSelection()


            parameters = New Dictionary(Of String, Object)
            query = String.Empty
            query &= "SELECT * "
            query &= "FROM            MB_COMP_PERSON_ADDRESS "
            query &= "WHERE           COMP_PERSON_CODE LIKE @p0 AND ADDR_CODE = 002"
            parameters.Add("@p0", val)

            MailAddrGrid.DataSource = fillWebSQL(query, parameters, "MB_COMP_PERSON_ADDRESS")
            For i As Integer = 0 To MailAddrGrid.ColumnCount - 1
                MailAddrGrid.Columns(i).Visible = False
            Next
            MailAddrGrid.Columns("ADDR_NOTE").DisplayIndex = 0
            MailAddrGrid.Columns("ADDR_NOTE").Visible = True
            MailAddrGrid.Columns("ADDR_MOO").Visible = True
            MailAddrGrid.Columns("ADDR_SOI").Visible = True
            MailAddrGrid.Columns("ADDR_ROAD").Visible = True
            MailAddrGrid.Columns("ADDR_SUB_DISTRICT").Visible = True
            MailAddrGrid.Columns("ADDR_DISTRICT").Visible = True
            MailAddrGrid.Columns("ADDR_PROVINCE_NAME").Visible = True
            MailAddrGrid.Columns("ADDR_POSTCODE").Visible = True
            MailAddrGrid.Columns("ADDR_TELEPHONE").Visible = True
            MailAddrGrid.Columns("ADDR_FAX").Visible = True

            MailAddrGrid.Columns("ADDR_NOTE").HeaderText = "สาขา"
            MailAddrGrid.Columns("ADDR_MOO").HeaderText = "หมู่"
            MailAddrGrid.Columns("ADDR_SOI").HeaderText = "ซอย"
            MailAddrGrid.Columns("ADDR_ROAD").HeaderText = "ถนน"
            MailAddrGrid.Columns("ADDR_SUB_DISTRICT").HeaderText = "แขวง/ตำบล"
            MailAddrGrid.Columns("ADDR_DISTRICT").HeaderText = "เขต/อำเภอ"
            MailAddrGrid.Columns("ADDR_PROVINCE_NAME").HeaderText = "จังหวัด"
            MailAddrGrid.Columns("ADDR_POSTCODE").HeaderText = "รหัสไปรษณีย์"
            MailAddrGrid.Columns("ADDR_TELEPHONE").HeaderText = "โทรศัพท์"
            MailAddrGrid.Columns("ADDR_FAX").HeaderText = "FAX"

            MailAddrGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            MailAddrGrid.AutoResizeColumns()
            MailAddrGrid.Refresh()
            MailAddrGrid.Update()

            MailAddrGrid.ClearSelection()

        End If
    End Sub
End Class
