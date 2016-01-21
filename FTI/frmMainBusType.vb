Imports System.Windows.Forms
Imports System.IO

Public Class frmMainBusType

    Friend dt As DataTable
    Friend COMP_PERSON_CODE As String
    Dim bs As BindingSource
    Dim ds As DataSet

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If tbBUSSINESS_TYPE_CODE.TextLength > 0 Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub getMB_MAIN_INDUSTRY(ByVal SEARCH As String)
        SEARCH = "%" & SEARCH.Replace(" ", "%") & "%"
        bs.Filter = String.Format("(BUSSINESS_TYPE_CODE LIKE '{0}') OR (BUSSINESS_TYPE_DESC_TH LIKE '{0}') OR (BUSSINESS_TYPE_DESC_EN LIKE '{0}')", SEARCH)
    End Sub

    Private Sub frmFTIFileNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ComboBox1.DataSource = dt
        'ComboBox1.DisplayMember = "BUSSINESS_TYPE_DESC_TH"
        'ComboBox1.ValueMember = "BUSSINESS_TYPE_CODE"

        ds = New DataSet
        ds.Tables.Add(dt)

        bs = New BindingSource(ds, "MB_BUSSINESS_TYPE")
        DataGridView10.DataSource = bs

        tbBUSSINESS_TYPE_CODE.DataBindings.Add("Text", bs, "BUSSINESS_TYPE_CODE")

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            getMB_MAIN_INDUSTRY(TextBox1.Text)
        End If
    End Sub

    Private Sub btFindIndus_Click(sender As Object, e As EventArgs) Handles btFindIndus.Click
        getMB_MAIN_INDUSTRY(TextBox1.Text)
    End Sub

    Private Sub btApply_Click(sender As Object, e As EventArgs) Handles btApply.Click
        DataGridView10.EndEdit()
        'bs.EndEdit()

        If MessageBox.Show("ยืนยันที่จะ" & btApply.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim row As DataRow = ds.Tables("MB_BUSSINESS_TYPE").Rows.Find(DataGridView10.CurrentRow.Cells("BUSSINESS_TYPE_CODE").Value)

            Try
                Dim parameters As New Dictionary(Of String, Object)
                'parameters.Add("@p0", MODULE_ID)

                Dim query As String = String.Empty
                query &= "SELECT * "
                query &= "FROM            MB_BUSSINESS_TYPE "
                'query &= "WHERE        (POSITION_NAME_TH LIKE @p0) OR "
                'query &= "                         (POSITION_NAME_EN LIKE @p1) "
                'query &= "ORDER BY POSITION_NAME_TH, POSITION_NAME_EN"

                If ds.Tables("MB_BUSSINESS_TYPE").GetChanges IsNot Nothing Then
                    Try
                        updateWebSQL(query, parameters, ds.Tables("MB_BUSSINESS_TYPE"))
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "updateWebSQL")
                    End Try
                End If

                MessageBox.Show("Apply Successfully")
            Catch ex As Exception
                MessageBox.Show(ex.Message, "MB_BUSSINESS_TYPE.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            'refresh grid
            ds.Tables("MB_BUSSINESS_TYPE").AcceptChanges()

            MessageBox.Show("บันทึกเสร็จสิ้น")
        End If
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        'new contact
        Dim f As New frmMainBusTypeNew
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            'add it

            'generate code
            'SELECT TOP (1) POSITION_CODE FROM MB_POSITION ORDER BY CONVERT(INT, POSITION_CODE) DESC
            Dim parameters As New Dictionary(Of String, Object)
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            Dim query As String = "SELECT TOP (1) CONVERT(INT, BUSSINESS_TYPE_CODE) AS CNT FROM MB_BUSSINESS_TYPE ORDER BY CONVERT(INT, BUSSINESS_TYPE_CODE) DESC"

            Dim CNT As Integer = 0
            Try
                CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT")
            End Try

            parameters.Clear()
            parameters.Add("@p0", Format((CNT + 1), "000"))
            parameters.Add("@p1", f.TextBox9.Text)
            parameters.Add("@p2", f.TextBox30.Text)
            'parameters.Add("@p3", f.TextBox1.Text)

            query = "INSERT INTO MB_BUSSINESS_TYPE (BUSSINESS_TYPE_CODE, BUSSINESS_TYPE_DESC_TH, BUSSINESS_TYPE_DESC_EN) VALUES (@p0,@p1,@p2)"

            Try
                executeWebSQL(query, parameters)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            End Try

            'logs
            'saveLOGS(CONTACT_CODE, "MB_CONTACT", "CONTACT_CODE", "ADD", "", f.TextBox9.Text, user_name)

            'refresh grid
            getMB_MAIN_INDUSTRY(TextBox1.Text)

            MessageBox.Show("เพิ่มเสร็จสิ้น")
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        'del contact

        If DataGridView10.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView10.CurrentRow.Cells("BUSSINESS_TYPE_DESC_TH").Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView10.CurrentRow.Cells("BUSSINESS_TYPE_CODE").Value)

                Dim query As String = "DELETE FROM MB_BUSSINESS_TYPE WHERE BUSSINESS_TYPE_CODE = @p0"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'logs
                'saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT", "CONTACT_CODE", "ADD", "", TextBox9.Text, user_name)

                'refresh grid
                getMB_MAIN_INDUSTRY(TextBox1.Text)

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
