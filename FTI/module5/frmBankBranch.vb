Public Class frmBankBranch
    Dim ds As DataSet
    Dim bs As BindingSource
    Dim bsddl As BindingSource

    Private FIELD_BANK_CODE As String = "BANK_CODE"
    Private FIELD_BANK_NAME As String = "BANK_NAME"
    Private FIELD_BRANCH_CODE As String = "BRANCH_CODE"
    Private FIELD_BRANCH_NAME As String = "BRANCH_NAME"
    Private FIELD_IS_OUTSIDE As String = "LOCAL_YN"
    Private TABLE_NAME As String = "DB_BRANCH"

    Private Sub frmBankBranch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet
        cbSearch.SelectedIndex = 0
        DataGridView1.AutoGenerateColumns = False

        GetBank()
    End Sub

    Private Sub Clear()
        txt1.Text = String.Empty
        txt2.Text = String.Empty
        chk1.Checked = False
        txtSearch.Text = String.Empty
    End Sub
    Private Sub GetBank()
        Dim pRep As New Dictionary(Of String, Object)

        Dim qRep As String = String.Empty
        qRep &= "SELECT * "
        qRep &= "FROM DB_BANK"

        qRep &= " ORDER BY BANK_NAME "

        Dim dtRep As DataTable = New DataTable

        dtRep = fillWebSQL(qRep, pRep, "DB_BANK")

        If ds.Tables.Contains("DB_BANK") = True Then
            ds.Tables("DB_BANK").Clear()
            ds.Tables("DB_BANK").Merge(dtRep)
            ds.Tables("DB_BANK").AcceptChanges()
        Else
            ds.Tables.Add(dtRep)

            'blinding
            bsddl = New BindingSource(ds, "DB_BANK")

            ddlBank.DataSource = bsddl
            ddlBank.DisplayMember = "BANK_NAME"
            ddlBank.ValueMember = "BANK_CODE"

        End If
    End Sub
    Private Sub GetList()
        'represent

        Dim searchValue As String = txtSearch.Text.Trim.Replace(" ", "%")

        Dim pRep As New Dictionary(Of String, Object)

        Dim qRep As String = String.Empty
        qRep &= " SELECT DB_BRANCH.* , DB_BANK.BANK_NAME "
        qRep &= " FROM " + TABLE_NAME
        qRep &= " INNER JOIN DB_BANK ON DB_BRANCH.BANK_CODE=DB_BANK.BANK_CODE "

        If Not String.IsNullOrEmpty(searchValue) Then
            If cbSearch.SelectedIndex = 0 Then
                qRep &= String.Format(" WHERE (" + FIELD_BANK_CODE + " LIKE '%{0}%')", searchValue)
            End If
            If cbSearch.SelectedIndex = 1 Then
                qRep &= String.Format(" WHERE (" + FIELD_BANK_NAME + " LIKE '%{0}%')", searchValue)
            End If
            If cbSearch.SelectedIndex = 2 Then
                qRep &= String.Format(" WHERE (" + FIELD_BRANCH_CODE + " LIKE '%{0}%')", searchValue)
            End If
            If cbSearch.SelectedIndex = 3 Then
                qRep &= String.Format(" WHERE (" + FIELD_BRANCH_NAME + " LIKE '%{0}%')", searchValue)
            End If
        End If

        qRep &= " ORDER BY " + FIELD_BANK_CODE

        Dim dtRep As DataTable = New DataTable

        dtRep = fillWebSQL(qRep, pRep, TABLE_NAME)

        If ds.Tables.Contains(TABLE_NAME) = True Then
            ds.Tables(TABLE_NAME).Clear()
            ds.Tables(TABLE_NAME).Merge(dtRep)
            ds.Tables(TABLE_NAME).AcceptChanges()
        Else
            ds.Tables.Add(dtRep)

            'ds.Tables(TABLE_NAME).PrimaryKey = New DataColumn() {ds.Tables(TABLE_NAME).Columns(FIELD_BANK_CODE)}

            'blinding
            bs = New BindingSource(ds, TABLE_NAME)

            DataGridView1.DataSource = bs

        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        GetList()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txt1.TextLength > 0 And txt2.TextLength > 0 Then

            Dim pRep As New Dictionary(Of String, Object)
            pRep.Add("@p0", txt1.Text)

            Dim qRep As String = String.Empty
            qRep &= "SELECT COUNT(*) AS CNT * "
            qRep &= "FROM " + TABLE_NAME
            qRep &= "WHERE (" + FIELD_BRANCH_CODE + " = @p0)"

            Dim CNT As Integer = 0

            Try
                CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
            Catch ex As Exception
                MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT " + TABLE_NAME)
            End Try

            If CNT = 0 Then

                Dim parameters As New Dictionary(Of String, Object)

                parameters.Clear()
                parameters.Add("@p0", txt1.Text)
                parameters.Add("@p1", txt2.Text)
                parameters.Add("@p2", If(chk1.Checked, "Y", "N"))
                parameters.Add("@p3", DirectCast(ddlBank.SelectedValue, String))

                qRep = String.Empty
                qRep = "INSERT INTO " + TABLE_NAME + " (" + FIELD_BRANCH_CODE + ", " + FIELD_BRANCH_NAME + ", " + FIELD_IS_OUTSIDE + ", " + FIELD_BANK_CODE + ") VALUES (@p0,@p1,@p2,@p3)"

                Try
                    executeWebSQL(qRep, parameters)
                Catch ex As Exception
                    MessageBox.Show(qRep & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'refresh grid
                GetList()

                MessageBox.Show("เพิ่มเสร็จสิ้น")
                Clear()
            Else
                MessageBox.Show("พบ " & txt1.Text & " ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
            End If
        End If
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView1.CurrentRow.Cells(FIELD_BRANCH_CODE).Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells(FIELD_BRANCH_CODE).Value)

                Dim query As String = "DELETE FROM " + TABLE_NAME + " WHERE " + FIELD_BRANCH_CODE + " = @p0"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'refresh grid
                GetList()

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Clear()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btnSave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                Try
                    Dim parameters As New Dictionary(Of String, Object)

                    Dim query As String = String.Empty
                    query &= "SELECT * "
                    query &= "FROM " + TABLE_NAME

                    If ds.Tables(TABLE_NAME).GetChanges IsNot Nothing Then
                        Try
                            updateWebSQL(query, parameters, ds.Tables(TABLE_NAME))
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "updateWebSQL")
                        End Try
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message, TABLE_NAME + ".UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'refresh grid
                ds.Tables(TABLE_NAME).AcceptChanges()

                MessageBox.Show("บันทึกเสร็จสิ้น")
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