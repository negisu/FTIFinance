Public Class frmBankAccount

    Dim ds As DataSet
    Dim bs As BindingSource

    Dim bsddlBank As BindingSource
    Dim bsddlBranch As BindingSource
    Dim bsddlType As BindingSource
    Dim bsddlCode As BindingSource

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    Private Sub frmBankAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet
        cbSearch.SelectedIndex = 0
        DataGridView1.AutoGenerateColumns = False

        GetBank()
        GetBankBranch()
        GetAccType()
        GetAccCode()
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
            bsddlBank = New BindingSource(ds, "DB_BANK")
            ddlBank.DisplayMember = "BANK_NAME"
            ddlBank.ValueMember = "BANK_CODE"
            ddlBank.SelectedIndex = -1
            ddlBank.DataSource = bsddlBank
            

        End If
    End Sub

    Private Sub GetBankBranch()
        Dim pRep As New Dictionary(Of String, Object)
        If ddlBank.SelectedIndex >= 0 Then

            Dim qRep As String = String.Empty
            qRep &= "SELECT * "
            qRep &= " FROM DB_BRANCH"
            qRep &= " WHERE DB_BRANCH.BANK_CODE = " + DirectCast(ddlBank.SelectedValue, String) + " "
            qRep &= " ORDER BY BRANCH_NAME "

            Dim dtRep As DataTable = New DataTable

            dtRep = fillWebSQL(qRep, pRep, "DB_BRANCH")

            If ds.Tables.Contains("DB_BRANCH") = True Then
                ds.Tables("DB_BRANCH").Clear()
                ds.Tables("DB_BRANCH").Merge(dtRep)
                ds.Tables("DB_BRANCH").AcceptChanges()
            Else
                ds.Tables.Add(dtRep)

                'blinding
                bsddlBranch = New BindingSource(ds, "DB_BRANCH")

                ddlBranch.DataSource = bsddlBranch
                ddlBranch.DisplayMember = "BRANCH_NAME"
                ddlBranch.ValueMember = "BRANCH_CODE"

            End If
        End If
    End Sub

    Private Sub GetAccType()
        Dim pRep As New Dictionary(Of String, Object)

        Dim qRep As String = String.Empty
        qRep &= "SELECT * "
        qRep &= "FROM DB_ACCOUNT_TYPE"

        qRep &= " ORDER BY ACCOUNT_TYPE_NAME "

        Dim dtRep As DataTable = New DataTable

        dtRep = fillWebSQL(qRep, pRep, "DB_ACCOUNT_TYPE")

        If ds.Tables.Contains("DB_ACCOUNT_TYPE") = True Then
            ds.Tables("DB_ACCOUNT_TYPE").Clear()
            ds.Tables("DB_ACCOUNT_TYPE").Merge(dtRep)
            ds.Tables("DB_ACCOUNT_TYPE").AcceptChanges()
        Else
            ds.Tables.Add(dtRep)

            'blinding
            bsddlType = New BindingSource(ds, "DB_ACCOUNT_TYPE")

            ddlType.DataSource = bsddlType
            ddlType.DisplayMember = "ACCOUNT_TYPE_NAME"
            ddlType.ValueMember = "ACCOUNT_TYPE_CODE"

        End If
    End Sub

    Private Sub GetAccCode()
        Dim pRep As New Dictionary(Of String, Object)

        Dim qRep As String = String.Empty
        qRep &= "SELECT * "
        qRep &= "FROM GL_ACCOUNT"

        qRep &= " ORDER BY ACC_NAME "

        Dim dtRep As DataTable = New DataTable

        dtRep = fillWebSQL(qRep, pRep, "GL_ACCOUNT")

        If ds.Tables.Contains("GL_ACCOUNT") = True Then
            ds.Tables("GL_ACCOUNT").Clear()
            ds.Tables("GL_ACCOUNT").Merge(dtRep)
            ds.Tables("GL_ACCOUNT").AcceptChanges()
        Else
            ds.Tables.Add(dtRep)

            'blinding
            bsddlCode = New BindingSource(ds, "GL_ACCOUNT")

            ddlCode.DataSource = bsddlCode
            ddlCode.DisplayMember = "ACC_NAME"
            ddlCode.ValueMember = "ACC_ID"

        End If
    End Sub

    Private Sub ddlBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBank.SelectedIndexChanged
        GetBankBranch()
    End Sub

    Private Sub Clear()
        txt1.Text = String.Empty
        txt2.Text = String.Empty
        txtSearch.Text = String.Empty
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        GetList()
    End Sub

    Private Sub GetList()
        'represent

        Dim searchValue As String = txtSearch.Text.Trim.Replace(" ", "%")

        Dim pRep As New Dictionary(Of String, Object)

        Dim qRep As String = String.Empty
        qRep &= " SELECT * "
        qRep &= " FROM DB_ACCOUNT "

        If Not String.IsNullOrEmpty(searchValue) Then
            If cbSearch.SelectedIndex = 0 Then
                qRep &= String.Format(" WHERE ( ACCOUNT_NUMBER LIKE '%{0}%')", searchValue)
            End If
            If cbSearch.SelectedIndex = 1 Then
                qRep &= String.Format(" WHERE ( ACCOUNT_NAME LIKE '%{0}%')", searchValue)
            End If
            If cbSearch.SelectedIndex = 2 Then
                qRep &= String.Format(" WHERE ( BANK_CODE LIKE '%{0}%')", searchValue)
            End If
            If cbSearch.SelectedIndex = 3 Then
                qRep &= String.Format(" WHERE ( BRANCH_CODE LIKE '%{0}%')", searchValue)
            End If
            If cbSearch.SelectedIndex = 4 Then
                qRep &= String.Format(" WHERE ( ACCOUNT_TYPE_CODE LIKE '%{0}%')", searchValue)
            End If
            If cbSearch.SelectedIndex = 5 Then
                qRep &= String.Format(" WHERE ( ACC_ID LIKE '%{0}%')", searchValue)
            End If
        End If

        qRep &= " ORDER BY ACCOUNT_NUMBER "

        Dim dtRep As DataTable = New DataTable

        dtRep = fillWebSQL(qRep, pRep, "DB_ACCOUNT")

        If ds.Tables.Contains("DB_ACCOUNT") = True Then
            ds.Tables("DB_ACCOUNT").Clear()
            ds.Tables("DB_ACCOUNT").Merge(dtRep)
            ds.Tables("DB_ACCOUNT").AcceptChanges()
        Else
            ds.Tables.Add(dtRep)

            ds.Tables("DB_ACCOUNT").PrimaryKey = New DataColumn() {ds.Tables("DB_ACCOUNT").Columns("ACCOUNT_NUMBER")}

            'blinding
            bs = New BindingSource(ds, "DB_ACCOUNT")

            DataGridView1.DataSource = bs

        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txt1.TextLength > 0 And txt2.TextLength > 0 Then

            Dim pRep As New Dictionary(Of String, Object)
            pRep.Add("@p0", txt1.Text)

            Dim qRep As String = String.Empty
            qRep &= "SELECT COUNT(*) AS CNT * "
            qRep &= "FROM DB_ACCOUNT "
            qRep &= "WHERE ( ACCOUNT_NUMBER = @p0)"

            Dim CNT As Integer = 0

            Try
                CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
            Catch ex As Exception
                MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT DB_ACCOUNT ")
            End Try

            If CNT = 0 Then

                Dim parameters As New Dictionary(Of String, Object)

                parameters.Clear()
                parameters.Add("@p0", txt1.Text)
                parameters.Add("@p1", txt2.Text)
                parameters.Add("@p2", DirectCast(ddlBank.SelectedValue, String))
                parameters.Add("@p3", DirectCast(ddlBranch.SelectedValue, String))
                parameters.Add("@p4", DirectCast(ddlType.SelectedValue, String))
                parameters.Add("@p5", DirectCast(ddlCode.SelectedValue, String))

                qRep = String.Empty
                ' Not found relate field with OU_CODE, DIV_CODE, use static instead.
                qRep = "INSERT INTO DB_ACCOUNT (ACCOUNT_NUMBER, ACCOUNT_NAME, BANK_CODE, BRANCH_CODE, ACCOUNT_TYPE_CODE, ACC_ID, OU_CODE, DIV_CODE ) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,'001','112-01')"

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
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView1.CurrentRow.Cells("ACCOUNT_NUMBER").Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("ACCOUNT_NUMBER").Value)

                Dim query As String = "DELETE FROM DB_ACCOUNT WHERE ACCOUNT_NUMBER = @p0"

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
                    query &= "FROM DB_ACCOUNT "

                    If ds.Tables("DB_ACCOUNT").GetChanges IsNot Nothing Then
                        Try
                            updateWebSQL(query, parameters, ds.Tables("DB_ACCOUNT"))
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "updateWebSQL")
                        End Try
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "DB_ACCOUNT.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'refresh grid
                ds.Tables("DB_ACCOUNT").AcceptChanges()

                MessageBox.Show("บันทึกเสร็จสิ้น")
            End If
        End If
    End Sub
End Class