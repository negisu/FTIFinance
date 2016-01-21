Imports System.Windows.Forms
Imports System.IO

Public Class frmGS1ean13

    'Dim dt As DataTable
    Friend MEMBER_TYPE_CODE As String
    Dim bs As BindingSource
    Friend ds As DataSet

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If tbBUSSINESS_TYPE_CODE.TextLength > 0 And CInt(tAVAILABLE.Text) > 0 Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    'Private Sub getMB_MAIN_INDUSTRY(ByVal SEARCH As String)
    '    SEARCH = "%" & SEARCH.Replace(" ", "%") & "%"
    '    bs.Filter = String.Format("(BUSSINESS_TYPE_CODE LIKE '{0}') OR (BUSSINESS_TYPE_DESC_TH LIKE '{0}') OR (BUSSINESS_TYPE_DESC_EN LIKE '{0}')", SEARCH)
    'End Sub

    Private Sub frmFTIFileNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ComboBox1.DataSource = dt
        'ComboBox1.DisplayMember = "BUSSINESS_TYPE_DESC_TH"
        'ComboBox1.ValueMember = "BUSSINESS_TYPE_CODE"

        ds = New DataSet
        'ds.Tables.Add(dt)

        getMB_PRENAME()

        DataGridView10.DataSource = bs

        'For i As Integer = 0 To DataGridView10.ColumnCount - 1
        '    DataGridView10.Columns(i).Visible = False
        'Next
        DataGridView10.Columns("NOTE").Visible = False
        'DataGridView10.Columns("WORK_GROUP_NAME").Visible = True
        'DataGridView10.Columns("WORK_GROUP_SHORT_NAME").Visible = True

        DataGridView10.Columns("BOOK_NO").Width = 60
        DataGridView10.Columns("NO_PER").Width = 60
        DataGridView10.Columns("NO_PREFIX").Width = 50
        DataGridView10.Columns("RUNNING").Width = 60
        DataGridView10.Columns("SEQ").Width = 50
        DataGridView10.Columns("MEMBER_TYPE_CODE").Width = 50
        DataGridView10.Columns("NO_XXXC").Width = 60
        DataGridView10.Columns("TOTAL").Width = 70
        DataGridView10.Columns("USED").Width = 70
        DataGridView10.Columns("AVAILABLE").Width = 70

        DataGridView10.Columns("TOTAL").ReadOnly = True
        DataGridView10.Columns("USED").ReadOnly = True
        DataGridView10.Columns("AVAILABLE").ReadOnly = True

        'blinding
        'TextBox2.DataBindings.Add("Text", bs, "WORK_GROUP_CODE")
        'TextBox3.DataBindings.Add("Text", bs, "WORK_GROUP_NAME")
        'TextBox4.DataBindings.Add("Text", bs, "WORK_GROUP_SHORT_NAME")
        'TextBox9.DataBindings.Add("Text", bs, "MEMBER_WITH")
        'TextBox10.DataBindings.Add("Text", bs, "CHAIRMAN_NAME")

        tbBUSSINESS_TYPE_CODE.DataBindings.Add("Text", bs, "NO_FROM")
        TextBox2.DataBindings.Add("Text", bs, "NOTE")
        tAVAILABLE.DataBindings.Add("Text", bs, "AVAILABLE")

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
        'parameters.Add("@p0", searchValue)
        'parameters.Add("@p1", searchValue)
        'parameters.Add("@p2", searchValue)
        'parameters.Add("@p3", searchValue)
        'parameters.Add("@p4", searchValue)

        Dim query As String = String.Empty
        query &= "SELECT        BOOK_NO, NO_PER, NO_PREFIX, NO_FROM, NO_TO, NO_XXXC, RUNNING, MEMBER_TYPE_CODE, NOTE, SEQ, CAST(LEFT(NO_TO,LEN(NO_TO)-LEN(RUNNING)+1) AS INT) - CAST(LEFT(NO_FROM,LEN(NO_FROM)-LEN(RUNNING)+1) AS INT) + 1 AS TOTAL, dbo.getEAN13count('300', "
        query &= "                         NO_PREFIX + NO_FROM + NO_XXXC, NO_PREFIX + NO_TO + NO_XXXC) AS USED "
        query &= "FROM            MB_EAN13 "

        If MEMBER_TYPE_CODE IsNot Nothing Then
            If MEMBER_TYPE_CODE.Length > 0 Then
                query &= String.Format("WHERE MEMBER_TYPE_CODE = '{0}' OR MEMBER_TYPE_CODE LIKE '{0},' OR MEMBER_TYPE_CODE LIKE ',{0}'", MEMBER_TYPE_CODE)
            End If
        End If
        

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "MB_EAN13")

        If ds.Tables.Contains("MB_EAN13") = True Then
            ds.Tables("MB_EAN13").Clear()
            ds.Tables("MB_EAN13").Merge(dt)
            ds.Tables("MB_EAN13").AcceptChanges()
        Else
            dt.Columns.Add("AVAILABLE", System.Type.GetType("System.Int32"))
            'dt.Columns.Add("Discount", System.Type.GetType("System.Int32"))

            dt.Columns("AVAILABLE").Expression = "TOTAL-USED"

            ds.Tables.Add(dt)

            ds.Tables("MB_EAN13").PrimaryKey = New DataColumn() {ds.Tables("MB_EAN13").Columns("NO_FROM"), ds.Tables("MB_EAN13").Columns("NO_TO")}

            bs = New BindingSource(ds, "MB_EAN13")
        End If
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            btFindIndus_Click(sender, e)
        End If
    End Sub

    Private Sub btFindIndus_Click(sender As Object, e As EventArgs) Handles btFindIndus.Click
        Dim SEARCH As String = "%" & TextBox1.Text.Replace(" ", "%") & "%"
        bs.Filter = String.Format("(NO_FROM LIKE '{0}') OR (NO_TO LIKE '{0}') OR (NO_XXXC LIKE '{0}')", SEARCH)
    End Sub

    Private Sub btApply_Click(sender As Object, e As EventArgs) Handles btApply.Click
        DataGridView10.EndEdit()
        'bs.EndEdit()

        If MessageBox.Show("ยืนยันที่จะ" & btApply.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim row As DataRow = ds.Tables("MB_EAN13").Rows.Find(New Object() {DataGridView10.CurrentRow.Cells("NO_FROM").Value, DataGridView10.CurrentRow.Cells("NO_TO").Value})

            Try
                Dim parameters As New Dictionary(Of String, Object)
                'parameters.Add("@p0", MODULE_ID)

                Dim query As String = String.Empty
                query &= "SELECT        BOOK_NO, NO_PER, NO_PREFIX, NO_FROM, NO_TO, NO_XXXC, RUNNING, BOOK_TYPE, NOTE, SEQ "
                query &= "FROM            MB_EAN13"

                If ds.Tables("MB_EAN13").GetChanges IsNot Nothing Then
                    Try
                        updateWebSQL(query, parameters, ds.Tables("MB_EAN13"))
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "updateWebSQL")
                    End Try
                End If

                'MessageBox.Show("Apply Successfully")
            Catch ex As Exception
                MessageBox.Show(ex.Message, "MB_EAN13.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            'refresh grid
            getMB_PRENAME()

            MessageBox.Show("บันทึกเสร็จสิ้น")
        End If
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        'new contact
        'Dim f As New frmMainBusTypeNew
        'If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
        '    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        '    'add it

        '    'generate code
        '    'SELECT TOP (1) POSITION_CODE FROM MB_POSITION ORDER BY CONVERT(INT, POSITION_CODE) DESC
        '    Dim parameters As New Dictionary(Of String, Object)
        '    'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

        '    Dim query As String = "SELECT TOP (1) CONVERT(INT, BUSSINESS_TYPE_CODE) AS CNT FROM MB_BUSSINESS_TYPE ORDER BY CONVERT(INT, BUSSINESS_TYPE_CODE) DESC"

        '    Dim CNT As Integer = 0
        '    Try
        '        CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
        '    Catch ex As Exception
        '        MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT")
        '    End Try

        '    parameters.Clear()
        '    parameters.Add("@p0", Format((CNT + 1), "000"))
        '    parameters.Add("@p1", f.TextBox9.Text)
        '    parameters.Add("@p2", f.TextBox30.Text)
        '    'parameters.Add("@p3", f.TextBox1.Text)

        '    query = "INSERT INTO MB_BUSSINESS_TYPE (BUSSINESS_TYPE_CODE, BUSSINESS_TYPE_DESC_TH, BUSSINESS_TYPE_DESC_EN) VALUES (@p0,@p1,@p2)"

        '    Try
        '        executeWebSQL(query, parameters)
        '    Catch ex As Exception
        '        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        '    End Try

        '    'logs
        '    'saveLOGS(CONTACT_CODE, "MB_CONTACT", "CONTACT_CODE", "ADD", "", f.TextBox9.Text, user_name)

        '    'refresh grid
        '    getMB_MAIN_INDUSTRY(TextBox1.Text)

        '    MessageBox.Show("เพิ่มเสร็จสิ้น")
        'End If
        'f.Dispose()
        'f = Nothing
    End Sub

    Private Sub btDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        'del contact

        If DataGridView10.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView10.CurrentRow.Cells("BOOK_NO").Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView10.CurrentRow.Cells("NO_FROM").Value)
                parameters.Add("@p1", DataGridView10.CurrentRow.Cells("NO_TO").Value)

                Dim query As String = "DELETE FROM MB_EAN13 WHERE NO_FROM = @p0 AND NO_TO = @p1"

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
