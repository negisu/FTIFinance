Imports System
Imports System.ServiceModel
Imports System.IO
Imports System.IO.Compression
Imports System.Xml

Public Class frmTypeReceipt
    Dim ds As DataSet
    Dim bsRep As BindingSource

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub
    Private Sub frmTypeReceipt_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ds = New DataSet
        cbSearch.SelectedIndex = 0
        DataGridView1.AutoGenerateColumns = False

    End Sub

    Private Sub getListTypeReceipt()
        'represent

        Dim searchValue As String = TextBox1.Text.Trim.Replace(" ", "%")

        Dim pRep As New Dictionary(Of String, Object)

        Dim qRep As String = String.Empty
        qRep &= "SELECT Top 100 * "
        qRep &= "FROM FN_RECEIPT_TYPE "

        If Not String.IsNullOrEmpty(searchValue) Then
            If cbSearch.SelectedIndex = 0 Then
                qRep &= String.Format(" WHERE (RECEIPT_TYPE_CODE LIKE '%{0}%')", searchValue)
            End If
            If cbSearch.SelectedIndex = 1 Then
                qRep &= String.Format(" WHERE (RECEIPT_TYPE_NAME LIKE '%{0}%')", searchValue)
            End If
            If cbSearch.SelectedIndex = 2 Then
                qRep &= String.Format(" WHERE (ACC_ID LIKE '%{0}%')", searchValue)
            End If
            If cbSearch.SelectedIndex = 3 Then
                qRep &= String.Format(" WHERE (ACCOUNT_NUMBER LIKE '%{0}%')", searchValue)
            End If
        End If

        qRep &= " ORDER BY RECEIPT_TYPE_CODE, RECEIPT_TYPE_NAME "

        Dim dtRep As DataTable = New DataTable

        dtRep = fillWebSQL(qRep, pRep, "FN_RECEIPT_TYPE")

        If ds.Tables.Contains("FN_RECEIPT_TYPE") = True Then
            ds.Tables("FN_RECEIPT_TYPE").Clear()
            ds.Tables("FN_RECEIPT_TYPE").Merge(dtRep)
            ds.Tables("FN_RECEIPT_TYPE").AcceptChanges()
        Else
            ds.Tables.Add(dtRep)

            ds.Tables("FN_RECEIPT_TYPE").PrimaryKey = New DataColumn() {ds.Tables("FN_RECEIPT_TYPE").Columns("clnCode")}

            'blinding
            bsRep = New BindingSource(ds, "FN_RECEIPT_TYPE")

            DataGridView1.DataSource = bsRep

        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        getListTypeReceipt()
    End Sub

    Private Sub clear()
        tbCode.Text = String.Empty
        tbName.Text = String.Empty
        tbAccCode.Text = String.Empty
        tbAccName.Text = String.Empty
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If tbAccCode.TextLength > 0 Then

            Dim pRep As New Dictionary(Of String, Object)
            pRep.Add("@p0", tbAccCode.Text)

            Dim qRep As String = String.Empty
            qRep &= "SELECT COUNT(*) AS CNT * "
            qRep &= "FROM FN_RECEIPT_TYPE "
            qRep &= "WHERE (ACC_ID = @p0)"

            Dim CNT As Integer = 0

            Try
                CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
            Catch ex As Exception
                MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT FN_RECEIPT_TYPE")
            End Try

            If CNT = 0 Then

                Dim parameters As New Dictionary(Of String, Object)

                parameters.Clear()
                parameters.Add("@p0", tbCode.Text)
                parameters.Add("@p1", tbName.Text)
                parameters.Add("@p2", tbAccCode.Text)
                parameters.Add("@p3", tbAccName.Text)

                qRep = String.Empty
                qRep = "INSERT INTO FN_RECEIPT_TYPE (RECEIPT_TYPE_CODE, RECEIPT_TYPE_NAME, ACC_ID, ACCOUNT_NUMBER) VALUES (@p0,@p1,@p2,@p3)"

                Try
                    executeWebSQL(qRep, parameters)
                Catch ex As Exception
                    MessageBox.Show(qRep & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'refresh grid
                getListTypeReceipt()

                MessageBox.Show("เพิ่มเสร็จสิ้น")
                clear()
            Else
                MessageBox.Show("พบ " & tbAccCode.Text & " ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView1.CurrentRow.Cells("clnCode").Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("clnCode").Value)

                Dim query As String = "DELETE FROM FN_RECEIPT_TYPE WHERE RECEIPT_TYPE_CODE = @p0"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'refresh grid
                getListTypeReceipt()

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        clear()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            'bs.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btnSave.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'Dim row As DataRow = ds.Tables("FN_RECEIPT_TYPE").Rows.Find(DataGridView1.CurrentRow.Cells("clnCode").Value)

                Try
                    Dim parameters As New Dictionary(Of String, Object)

                    Dim query As String = String.Empty
                    query &= "SELECT * "
                    query &= "FROM FN_RECEIPT_TYPE "

                    If ds.Tables("FN_RECEIPT_TYPE").GetChanges IsNot Nothing Then
                        Try
                            updateWebSQL(query, parameters, ds.Tables("FN_RECEIPT_TYPE"))
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "updateWebSQL")
                        End Try
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "FN_RECEIPT_TYPE.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'refresh grid
                ds.Tables("FN_RECEIPT_TYPE").AcceptChanges()

                MessageBox.Show("บันทึกเสร็จสิ้น")
            End If
        End If
    End Sub
End Class