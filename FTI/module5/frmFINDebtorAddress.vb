Public Class frmFINDebtorAddress

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    Dim ds As DataSet
    Dim bs As BindingSource

    Private FIELD_CODE As String = "_CODE"

    Private FIELD_COMPANY As String = "FIELD_COMPANY"
    Private FIELD_ADDRESS_THA1 As String = "FIELD_ADDRESS_THA1"
    Private FIELD_ADDRESS_THA2 As String = "FIELD_ADDRESS_THA2"
    Private FIELD_ADDRESS_THA3 As String = "FIELD_ADDRESS_THA3"
    Private FIELD_ADDRESS_ENG1 As String = "FIELD_ADDRESS_ENG1"
    Private FIELD_ADDRESS_ENG2 As String = "FIELD_ADDRESS_ENG2"
    Private FIELD_ADDRESS_ENG3 As String = "FIELD_ADDRESS_ENG3"
    Private FIELD_CONTACT_NAME_THA As String = "FIELD_CONTACT_NAME_THA"
    Private FIELD_CONTACT_NAME_ENG As String = "FIELD_CONTACT_NAME_ENG"
    Private FIELD_POSTCODE As String = "FIELD_POSTCODE"
    Private FIELD_TELEPHONE As String = "FIELD_TELEPHONE"
    Private FIELD_FAX As String = "FIELD_FAX"

    Private TABLE_NAME As String = "DEPTOR_ADDRESS"

    Private Sub frmFINDebtorAddress_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet
        cbSearch.SelectedIndex = 0
        DataGridView1.AutoGenerateColumns = False
    End Sub
    Private Sub Clear()
        txt2.Text = String.Empty
        txt1.Text = String.Empty
        txt3.Text = String.Empty
        txt6.Text = String.Empty
        txt4.Text = String.Empty
        txt7.Text = String.Empty
        txt5.Text = String.Empty
        txt8.Text = String.Empty
        txt9.Text = String.Empty
        txt10.Text = String.Empty
        txt11.Text = String.Empty
        txt12.Text = String.Empty
        txt13.Text = String.Empty
        txtSearch.Text = String.Empty
    End Sub
    Private Sub GetList()
        'represent

        Dim searchValue As String = txtSearch.Text.Trim.Replace(" ", "%")

        Dim pRep As New Dictionary(Of String, Object)

        Dim qRep As String = String.Empty
        qRep &= "SELECT * "
        qRep &= "FROM " + TABLE_NAME

        If Not String.IsNullOrEmpty(searchValue) Then
            If cbSearch.SelectedIndex = 0 Then
                qRep &= String.Format(" WHERE (" + FIELD_CODE + " LIKE '%{0}%')", searchValue)
            End If
            If cbSearch.SelectedIndex = 1 Then
                qRep &= String.Format(" WHERE (" + FIELD_COMPANY + " LIKE '%{0}%')", searchValue)
            End If
            If cbSearch.SelectedIndex = 2 Then
                qRep &= String.Format(" WHERE (" + FIELD_ADDRESS_THA1 + " LIKE '%{0}%') OR (" + FIELD_ADDRESS_THA2 + " LIKE '%{0}%') OR (" + FIELD_ADDRESS_THA3 + " LIKE '%{0}%') OR (" + FIELD_ADDRESS_ENG1 + " LIKE '%{0}%') OR (" + FIELD_ADDRESS_ENG2 + " LIKE '%{0}%') OR (" + FIELD_ADDRESS_ENG3 + " LIKE '%{0}%') OR", searchValue)
            End If
            If cbSearch.SelectedIndex = 3 Then
                qRep &= String.Format(" WHERE (" + FIELD_CONTACT_NAME_ENG + " LIKE '%{0}%') OR (" + FIELD_CONTACT_NAME_THA + " LIKE '%{0}%')", searchValue)
            End If
            If cbSearch.SelectedIndex = 4 Then
                qRep &= String.Format(" WHERE (" + FIELD_POSTCODE + " LIKE '%{0}%')", searchValue)
            End If
            If cbSearch.SelectedIndex = 5 Then
                qRep &= String.Format(" WHERE (" + FIELD_TELEPHONE + " LIKE '%{0}%')", searchValue)
            End If
            If cbSearch.SelectedIndex = 6 Then
                qRep &= String.Format(" WHERE (" + FIELD_FAX + " LIKE '%{0}%')", searchValue)
            End If
        End If

        qRep &= " ORDER BY " + FIELD_CODE

        Dim dtRep As DataTable = New DataTable

        dtRep = fillWebSQL(qRep, pRep, TABLE_NAME)

        If ds.Tables.Contains(TABLE_NAME) = True Then
            ds.Tables(TABLE_NAME).Clear()
            ds.Tables(TABLE_NAME).Merge(dtRep)
            ds.Tables(TABLE_NAME).AcceptChanges()
        Else
            ds.Tables.Add(dtRep)

            ds.Tables(TABLE_NAME).PrimaryKey = New DataColumn() {ds.Tables(TABLE_NAME).Columns(FIELD_CODE)}

            'blinding
            bs = New BindingSource(ds, TABLE_NAME)

            DataGridView1.DataSource = bs

        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        GetList()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txt2.TextLength > 0 And txt1.TextLength > 0 Then

            Dim pRep As New Dictionary(Of String, Object)
            pRep.Add("@p0", txt2.Text)

            Dim qRep As String = String.Empty
            qRep &= "SELECT COUNT(*) AS CNT * "
            qRep &= "FROM " + TABLE_NAME
            qRep &= "WHERE (" + FIELD_CODE + " = @p0)"

            Dim CNT As Integer = 0

            Try
                CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
            Catch ex As Exception
                MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT " + TABLE_NAME)
            End Try

            If CNT = 0 Then

                Dim parameters As New Dictionary(Of String, Object)

                parameters.Clear()
                parameters.Add("@p0", txt2.Text)
                parameters.Add("@p1", txt1.Text)

                parameters.Add("@p2", txt3.Text)
                parameters.Add("@p3", txt6.Text)
                parameters.Add("@p4", txt4.Text)
                parameters.Add("@p5", txt7.Text)
                parameters.Add("@p6", txt5.Text)
                parameters.Add("@p7", txt8.Text)
                parameters.Add("@p8", txt9.Text)
                parameters.Add("@p9", txt10.Text)
                parameters.Add("@p10", txt11.Text)
                parameters.Add("@p11", txt12.Text)
                parameters.Add("@p12", txt13.Text)

                qRep = String.Empty

                qRep = "INSERT INTO " + TABLE_NAME + " (" + FIELD_CODE + ", " + FIELD_COMPANY + ", " + FIELD_ADDRESS_THA1 + ", " + FIELD_ADDRESS_THA2 + ", " + FIELD_ADDRESS_THA3 + ", " + FIELD_ADDRESS_ENG1 + ", " + FIELD_ADDRESS_ENG2 + ", " + FIELD_ADDRESS_ENG3 + ", " + FIELD_CONTACT_NAME_THA + ", " + FIELD_CONTACT_NAME_ENG + ", " + FIELD_POSTCODE + ", " + FIELD_TELEPHONE + ", " + FIELD_FAX + ") VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12)"

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
                MessageBox.Show("พบ " & txt2.Text & " ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView1.CurrentRow.Cells(FIELD_CODE).Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells(FIELD_CODE).Value)

                Dim query As String = "DELETE FROM " + TABLE_NAME + " WHERE " + FIELD_CODE + " = @p0"

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
End Class