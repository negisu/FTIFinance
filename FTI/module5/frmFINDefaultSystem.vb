Public Class frmFINDefaultSystem

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub


    Dim ds As DataSet
    Dim bs As BindingSource
    Dim _SelectedTableName As String = String.Empty
    Dim _SelectedFieldCode As String = String.Empty
    Dim _SelectedFieldName As String = String.Empty

    Private TAB1_FIELD_CODE As String = "TAB1_CODE"
    Private TAB1_FIELD_NAME As String = "TAB1_NAME"
    Private TAB1_FIELD_TYPE As String = "TAB1_TYPE"
    Private TAB1_TABLE_NAME As String = "TABLE_DOCUMENT"

    Private TAB2_FIELD_CODE As String = "TAB2_CODE"
    Private TAB2_FIELD_NAME As String = "TAB2_NAME"
    Private TAB2_FIELD_IS_PAY As String = "TAB2_IS_PAY"
    Private TAB2_FIELD_IS_GET As String = "TAB2_IS_GET"
    Private TAB2_FIELD_EXPIRED_ON_HAND As String = "TAB2_EXPIRED_ON_HAND"
    Private TAB2_FIELD_EXPIRED_OTW As String = "TAB2_EXPIRED_OTW"
    Private TAB2_TABLE_NAME As String = "TABLE_CHEQUE"

    Private TAB3_FIELD_CODE As String = "TAB3_CODE"
    Private TAB3_FIELD_NAME As String = "TAB3_NAME"
    Private TAB3_TABLE_NAME As String = "TABLE_ACCOUNTANCY"

    Private TAB4_FIELD_CODE As String = "TAB4_CODE"
    Private TAB4_FIELD_NAME As String = "TAB4_NAME"
    Private TAB4_TABLE_NAME As String = "TABLE_PAYMENT"

    Private TAB5_FIELD_CODE As String = "TAB5_CODE"
    Private TAB5_FIELD_NAME As String = "TAB5_NAME"
    Private TAB5_TABLE_NAME As String = "TABLE_RECEIVE"

    Private TAB6_FIELD_CODE As String = "TAB6_CODE"
    Private TAB6_FIELD_NAME As String = "TAB6_NAME"
    Private TAB6_TABLE_NAME As String = "TABLE_RECEIPT"

    Private TAB7_FIELD_CODE As String = "TAB7_CODE"
    Private TAB7_FIELD_NAME As String = "TAB7_NAME"
    Private TAB7_TABLE_NAME As String = "TABLE_ORDER"

    Private TAB8_FIELD_CODE As String = "TAB8_CODE"
    Private TAB8_FIELD_NAME As String = "TAB8_NAME"
    Private TAB8_TABLE_NAME As String = "TABLE_DEDUCTION"

    Private TAB9_FIELD_CODE As String = "TAB9_CODE"
    Private TAB9_FIELD_NAME As String = "TAB9_NAME"
    Private TAB9_TABLE_NAME As String = "TABLE_ACC_TYPE"


    Private Sub frmFINDefaultSystem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet
        cbSearch.Items.Clear()
        cbSearch.Items.Add("รหัสเอกสาร")
        cbSearch.Items.Add("ชื่อเอกสาร")
        cbSearch.Items.Add("ประเภทเอกสาร")
        cbSearch.SelectedIndex = 0
        DataGridView1.AutoGenerateColumns = False
        _SelectedTableName = TAB1_TABLE_NAME
        _SelectedFieldCode = TAB1_FIELD_CODE
        _SelectedFieldName = TAB1_FIELD_NAME
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        GetList()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If ValidateForm() Then

            Dim pRep As Dictionary(Of String, Object) = Nothing
            pRep = CreateCheckDuplicateParam()

            Dim qRep As String = String.Empty
            qRep = CreateCheckDuplicateQuery()

            Dim CNT As Integer = 0

            Try
                CNT = CInt(client.ExecuteScalar(qRep, pRep, user_session))
            Catch ex As Exception
                MessageBox.Show(qRep & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT " + _SelectedTableName)
            End Try

            If CNT = 0 Then

                Dim parameters As Dictionary(Of String, Object) = Nothing

                parameters = CreateInsertParam()


                qRep = String.Empty
                qRep = CreateInsertQuery()

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
                MessageBox.Show("ซ้ำ!! : พบค่านี้ในฐานข้อมูลอยู่แล้วจำนวน " & CNT & " รายการ")
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView1.CurrentRow.Cells(_SelectedFieldCode).Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells(_SelectedFieldCode).Value)

                Dim query As String = "DELETE FROM " + _SelectedTableName + " WHERE " + _SelectedFieldCode + " = @p0"

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
                    query &= "FROM " + _SelectedTableName

                    If ds.Tables(_SelectedTableName).GetChanges IsNot Nothing Then
                        Try
                            updateWebSQL(query, parameters, ds.Tables(_SelectedTableName))
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "updateWebSQL")
                        End Try
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message, _SelectedTableName + ".UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'refresh grid
                ds.Tables(_SelectedTableName).AcceptChanges()

                MessageBox.Show("บันทึกเสร็จสิ้น")
            End If
        End If

    End Sub


#Region "Method"
    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim value As Integer = TabControl1.SelectedIndex
        Select Case value
            Case 0
                'เอกสาร
                cbSearch.Items.Clear()
                cbSearch.Items.Add("รหัสเอกสาร")
                cbSearch.Items.Add("ชื่อเอกสาร")
                cbSearch.Items.Add("ประเภทเอกสาร")
                cbSearch.SelectedIndex = 0
                _SelectedTableName = TAB1_TABLE_NAME
                _SelectedFieldCode = TAB1_FIELD_CODE
                _SelectedFieldName = TAB1_FIELD_NAME
                GetList()
            Case 1
                'เช็ค
                cbSearch.Items.Clear()
                cbSearch.Items.Add("รหัสสถานะ")
                cbSearch.Items.Add("ชื่อสถานะ")
                cbSearch.SelectedIndex = 0
                _SelectedTableName = TAB2_TABLE_NAME
                _SelectedFieldCode = TAB2_FIELD_CODE
                _SelectedFieldName = TAB2_FIELD_NAME
                GetList()
            Case 2
                'บัญชี
                cbSearch.Items.Clear()
                cbSearch.Items.Add("รหัสประเภทบัญชี")
                cbSearch.Items.Add("ชื่อประเภทบัญชี")
                cbSearch.SelectedIndex = 0
                _SelectedTableName = TAB3_TABLE_NAME
                _SelectedFieldCode = TAB3_FIELD_CODE
                _SelectedFieldName = TAB3_FIELD_NAME
                GetList()
            Case 3
                'จ่ายเงิน
                cbSearch.Items.Clear()
                cbSearch.Items.Add("รหัสประเภทการจ่ายเงิน")
                cbSearch.Items.Add("ชื่อประเภทการจ่ายเงิน")
                cbSearch.SelectedIndex = 0
                _SelectedTableName = TAB4_TABLE_NAME
                _SelectedFieldCode = TAB4_FIELD_CODE
                _SelectedFieldName = TAB4_FIELD_NAME
                GetList()
            Case 4
                'รับเงิน
                cbSearch.Items.Clear()
                cbSearch.Items.Add("รหัสประเภทการรับเงิน")
                cbSearch.Items.Add("ชื่อประเภทการรับเงิน")
                cbSearch.SelectedIndex = 0
                _SelectedTableName = TAB5_TABLE_NAME
                _SelectedFieldCode = TAB5_FIELD_CODE
                _SelectedFieldName = TAB5_FIELD_NAME
                GetList()
            Case 5
                'ใบเสร็จรับเงิน
                cbSearch.Items.Clear()
                cbSearch.Items.Add("รหัสประเภทใบเสร็จรับเงิน")
                cbSearch.Items.Add("ชื่อประเภทใบเสร็จรับเงิน")
                cbSearch.SelectedIndex = 0
                _SelectedTableName = TAB6_TABLE_NAME
                _SelectedFieldCode = TAB6_FIELD_CODE
                _SelectedFieldName = TAB6_FIELD_NAME
                GetList()
            Case 6
                'ใบสั่งจ่าย
                cbSearch.Items.Clear()
                cbSearch.Items.Add("รหัสประเภทใบสั่งจ่าย")
                cbSearch.Items.Add("ชื่อประเภทใบสั่งจ่าย")
                cbSearch.SelectedIndex = 0
                _SelectedTableName = TAB7_TABLE_NAME
                _SelectedFieldCode = TAB7_FIELD_CODE
                _SelectedFieldName = TAB7_FIELD_NAME
                GetList()
            Case 7
                'รายการหัก
                cbSearch.Items.Clear()
                cbSearch.Items.Add("รหัสประเภทรายการหัก")
                cbSearch.Items.Add("ชื่อประเภทรายการหัก")
                cbSearch.SelectedIndex = 0
                _SelectedTableName = TAB8_TABLE_NAME
                _SelectedFieldCode = TAB8_FIELD_CODE
                _SelectedFieldName = TAB8_FIELD_NAME
                GetList()
            Case 8
                'บัญชีธนาคาร
                cbSearch.Items.Clear()
                cbSearch.Items.Add("รหัสประเภทรายการ")
                cbSearch.Items.Add("ชื่อประเภทรายการ")
                cbSearch.SelectedIndex = 0
                _SelectedTableName = TAB9_TABLE_NAME
                _SelectedFieldCode = TAB9_FIELD_CODE
                _SelectedFieldName = TAB9_FIELD_NAME
                GetList()
        End Select
    End Sub

    Private Sub GetList()
        'represent

        Dim searchValue As String = txtSearch.Text.Trim.Replace(" ", "%")

        Dim pRep As New Dictionary(Of String, Object)

        Dim qRep As String = String.Empty
        qRep &= "SELECT * "
        qRep &= "FROM " + _SelectedTableName

        qRep &= CriteriaAndOrder(qRep, searchValue)

        BindDataTable(qRep, pRep)

    End Sub

    Private Function CriteriaAndOrder(query As String, search As String) As String
        Dim _result As String = ""

        _result = query

        If Not String.IsNullOrEmpty(search) Then

            If cbSearch.SelectedIndex = 0 Then
                _result &= String.Format(" WHERE (" + _SelectedFieldCode + " LIKE '%{0}%')", search)
            End If
            If cbSearch.SelectedIndex = 1 Then
                _result &= String.Format(" WHERE (" + _SelectedFieldName + " LIKE '%{0}%')", search)
            End If
            If cbSearch.SelectedIndex = 2 Then 'for this statement in TAB1 use TAB1_FIELD_TYPE be fine.
                _result &= String.Format(" WHERE (" + TAB1_FIELD_TYPE + " LIKE '%{0}%')", search)
            End If
        End If

        _result &= " ORDER BY " + _SelectedFieldCode

        Return _result
    End Function

    Private Sub BindDataTable(query As String, parameter As Dictionary(Of String, Object))
        Dim dtRep As DataTable = New DataTable
        dtRep = fillWebSQL(query, parameter, _SelectedTableName)

        If ds.Tables.Contains(_SelectedTableName) = True Then
            ds.Tables(_SelectedTableName).Clear()
            ds.Tables(_SelectedTableName).Merge(dtRep)
            ds.Tables(_SelectedTableName).AcceptChanges()
        Else
            ds.Tables.Add(dtRep)

            ds.Tables(_SelectedTableName).PrimaryKey = New DataColumn() {ds.Tables(_SelectedTableName).Columns(_SelectedFieldCode)}

            'blinding
            bs = New BindingSource(ds, _SelectedTableName)


            Select Case _SelectedTableName
                Case TAB1_TABLE_NAME
                    'เอกสาร
                    DataGridView1.DataSource = bs
                Case TAB2_TABLE_NAME
                    'เช็ค
                    DataGridView2.DataSource = bs
                Case TAB3_TABLE_NAME
                    'บัญชี
                    DataGridView3.DataSource = bs
                Case TAB4_TABLE_NAME
                    'จ่ายเงิน
                    DataGridView4.DataSource = bs
                Case TAB5_TABLE_NAME
                    'รับเงิน
                    DataGridView5.DataSource = bs
                Case TAB6_TABLE_NAME
                    'ใบเสร็จรับเงิน
                    DataGridView6.DataSource = bs
                Case TAB7_TABLE_NAME
                    'ใบสั่งจ่าย
                    DataGridView7.DataSource = bs
                Case TAB8_TABLE_NAME
                    'รายการหัก
                    DataGridView8.DataSource = bs
                Case TAB9_TABLE_NAME
                    'บัญชีธนาคาร
                    DataGridView9.DataSource = bs
            End Select
        End If
    End Sub

    Private Sub Clear()
        txtSearch.Text = String.Empty

        txt1_1.Text = String.Empty
        txt1_2.Text = String.Empty
        txt1_3.Text = String.Empty

        txt2_1.Text = String.Empty
        txt2_2.Text = String.Empty
        rdo1.Checked = False
        rdo2.Checked = False
        txt2_3.Text = String.Empty
        txt2_4.Text = String.Empty

        txt3_1.Text = String.Empty
        txt3_2.Text = String.Empty

        txt4_1.Text = String.Empty
        txt4_2.Text = String.Empty

        txt5_1.Text = String.Empty
        txt5_2.Text = String.Empty

        txt6_1.Text = String.Empty
        txt6_2.Text = String.Empty

        txt7_1.Text = String.Empty
        txt7_2.Text = String.Empty

        txt8_1.Text = String.Empty
        txt8_2.Text = String.Empty

        txt9_1.Text = String.Empty
        txt9_2.Text = String.Empty

    End Sub

    Private Function ValidateForm() As Boolean
        Dim _result As Boolean = False

        Select Case _SelectedTableName
            Case TAB1_TABLE_NAME
                'เอกสาร
                _result = If(txt1_1.TextLength > 0 AndAlso txt1_2.TextLength > 0 AndAlso txt1_3.TextLength > 0, True, False)
            Case TAB2_TABLE_NAME
                'เช็ค
                _result = If(txt2_1.TextLength > 0 AndAlso txt2_2.TextLength > 0 AndAlso txt2_3.TextLength > 0 AndAlso txt2_4.TextLength > 0, True, False)
            Case TAB3_TABLE_NAME
                'บัญชี
                _result = If(txt3_1.TextLength > 0 AndAlso txt3_2.TextLength > 0, True, False)
            Case TAB4_TABLE_NAME
                'จ่ายเงิน
                _result = If(txt4_1.TextLength > 0 AndAlso txt4_2.TextLength > 0, True, False)
            Case TAB5_TABLE_NAME
                'รับเงิน
                _result = If(txt5_1.TextLength > 0 AndAlso txt5_2.TextLength > 0, True, False)
            Case TAB6_TABLE_NAME
                'ใบเสร็จรับเงิน
                _result = If(txt6_1.TextLength > 0 AndAlso txt6_2.TextLength > 0, True, False)
            Case TAB7_TABLE_NAME
                'ใบสั่งจ่าย
                _result = If(txt7_1.TextLength > 0 AndAlso txt7_2.TextLength > 0, True, False)
            Case TAB8_TABLE_NAME
                'รายการหัก
                _result = If(txt8_1.TextLength > 0 AndAlso txt8_2.TextLength > 0, True, False)
            Case TAB9_TABLE_NAME
                'บัญชีธนาคาร
                _result = If(txt9_1.TextLength > 0 AndAlso txt9_2.TextLength > 0, True, False)
        End Select

        Return _result
    End Function

    Private Function CreateCheckDuplicateParam() As Dictionary(Of String, Object)
        Dim _result As New Dictionary(Of String, Object)

        Select Case _SelectedTableName
            Case TAB1_TABLE_NAME
                'เอกสาร
                _result.Add("@p0", txt1_1.Text)
            Case TAB2_TABLE_NAME
                'เช็ค
                _result.Add("@p0", txt2_1.Text)
            Case TAB3_TABLE_NAME
                'บัญชี
                _result.Add("@p0", txt3_1.Text)
            Case TAB4_TABLE_NAME
                'จ่ายเงิน
                _result.Add("@p0", txt4_1.Text)
            Case TAB5_TABLE_NAME
                'รับเงิน
                _result.Add("@p0", txt5_1.Text)
            Case TAB6_TABLE_NAME
                'ใบเสร็จรับเงิน
                _result.Add("@p0", txt6_1.Text)
            Case TAB7_TABLE_NAME
                'ใบสั่งจ่าย
                _result.Add("@p0", txt7_1.Text)
            Case TAB8_TABLE_NAME
                'รายการหัก
                _result.Add("@p0", txt8_1.Text)
            Case TAB9_TABLE_NAME
                'บัญชีธนาคาร
                _result.Add("@p0", txt9_1.Text)
        End Select

        Return _result
    End Function
    Private Function CreateCheckDuplicateQuery() As String
        Dim _result As String = ""

        Select Case _SelectedTableName
            Case TAB1_TABLE_NAME
                'เอกสาร
                _result &= "SELECT COUNT(*) AS CNT * "
                _result &= "FROM " + TAB1_TABLE_NAME
                _result &= "WHERE (" + TAB1_FIELD_CODE + " = @p0)"
            Case TAB2_TABLE_NAME
                'เช็ค
                _result &= "SELECT COUNT(*) AS CNT * "
                _result &= "FROM " + TAB2_TABLE_NAME
                _result &= "WHERE (" + TAB2_FIELD_CODE + " = @p0)"
            Case TAB3_TABLE_NAME
                'บัญชี
                _result &= "SELECT COUNT(*) AS CNT * "
                _result &= "FROM " + TAB3_TABLE_NAME
                _result &= "WHERE (" + TAB3_FIELD_CODE + " = @p0)"
            Case TAB4_TABLE_NAME
                'จ่ายเงิน
                _result &= "SELECT COUNT(*) AS CNT * "
                _result &= "FROM " + TAB4_TABLE_NAME
                _result &= "WHERE (" + TAB4_FIELD_CODE + " = @p0)"
            Case TAB5_TABLE_NAME
                'รับเงิน
                _result &= "SELECT COUNT(*) AS CNT * "
                _result &= "FROM " + TAB5_TABLE_NAME
                _result &= "WHERE (" + TAB5_FIELD_CODE + " = @p0)"
            Case TAB6_TABLE_NAME
                'ใบเสร็จรับเงิน
                _result &= "SELECT COUNT(*) AS CNT * "
                _result &= "FROM " + TAB6_TABLE_NAME
                _result &= "WHERE (" + TAB6_FIELD_CODE + " = @p0)"
            Case TAB7_TABLE_NAME
                'ใบสั่งจ่าย
                _result &= "SELECT COUNT(*) AS CNT * "
                _result &= "FROM " + TAB7_TABLE_NAME
                _result &= "WHERE (" + TAB7_FIELD_CODE + " = @p0)"
            Case TAB8_TABLE_NAME
                'รายการหัก
                _result &= "SELECT COUNT(*) AS CNT * "
                _result &= "FROM " + TAB8_TABLE_NAME
                _result &= "WHERE (" + TAB8_FIELD_CODE + " = @p0)"
            Case TAB9_TABLE_NAME
                'บัญชีธนาคาร
                _result &= "SELECT COUNT(*) AS CNT * "
                _result &= "FROM " + TAB9_TABLE_NAME
                _result &= "WHERE (" + TAB9_FIELD_CODE + " = @p0)"
        End Select

        Return _result
    End Function

    Private Function CreateInsertParam() As Dictionary(Of String, Object)
        Dim _result As New Dictionary(Of String, Object)
        _result.Clear()

        Select Case _SelectedTableName
            Case TAB1_TABLE_NAME
                'เอกสาร
                _result.Add("@p0", txt1_1.Text)
                _result.Add("@p1", txt1_2.Text)
                _result.Add("@p2", txt1_3.Text)
            Case TAB2_TABLE_NAME
                'เช็ค
                _result.Add("@p0", txt2_1.Text)
                _result.Add("@p1", txt2_2.Text)
                _result.Add("@p2", If(rdo1.Checked, "Y", "N"))
                _result.Add("@p3", If(rdo2.Checked, "Y", "N"))
                _result.Add("@p4", txt2_3.Text)
                _result.Add("@p5", txt2_4.Text)
            Case TAB3_TABLE_NAME
                'บัญชี
                _result.Add("@p0", txt3_1.Text)
                _result.Add("@p1", txt3_2.Text)
            Case TAB4_TABLE_NAME
                'จ่ายเงิน
                _result.Add("@p0", txt4_1.Text)
                _result.Add("@p1", txt4_2.Text)
            Case TAB5_TABLE_NAME
                'รับเงิน
                _result.Add("@p0", txt5_1.Text)
                _result.Add("@p1", txt5_2.Text)
            Case TAB6_TABLE_NAME
                'ใบเสร็จรับเงิน
                _result.Add("@p0", txt6_1.Text)
                _result.Add("@p1", txt6_2.Text)
            Case TAB7_TABLE_NAME
                'ใบสั่งจ่าย
                _result.Add("@p0", txt7_1.Text)
                _result.Add("@p1", txt7_2.Text)
            Case TAB8_TABLE_NAME
                'รายการหัก
                _result.Add("@p0", txt8_1.Text)
                _result.Add("@p1", txt8_2.Text)
            Case TAB9_TABLE_NAME
                'บัญชีธนาคาร
                _result.Add("@p0", txt9_1.Text)
                _result.Add("@p1", txt9_2.Text)
        End Select

        Return _result
    End Function
    Private Function CreateInsertQuery() As String
        Dim _result As String = ""

        Select Case _SelectedTableName
            Case TAB1_TABLE_NAME
                'เอกสาร
                _result = "INSERT INTO " + TAB1_TABLE_NAME + " (" + TAB1_FIELD_CODE + ", " + TAB1_FIELD_NAME + ", " + TAB1_FIELD_TYPE + ") VALUES (@p0,@p1,@p2)"
            Case TAB2_TABLE_NAME
                'เช็ค
                _result = "INSERT INTO " + TAB2_TABLE_NAME + " (" + TAB2_FIELD_CODE + ", " + TAB2_FIELD_NAME + ", " + TAB2_FIELD_IS_PAY + ", " + TAB2_FIELD_IS_GET + ", " + TAB2_FIELD_EXPIRED_ON_HAND + ", " + TAB2_FIELD_EXPIRED_OTW + ") VALUES (@p0,@p1,@p2,@p3,@p4,@p5)"
            Case TAB3_TABLE_NAME
                'บัญชี
                _result = "INSERT INTO " + TAB3_TABLE_NAME + " (" + TAB3_FIELD_CODE + ", " + TAB3_FIELD_NAME + ") VALUES (@p0,@p1)"
            Case TAB4_TABLE_NAME
                'จ่ายเงิน
                _result = "INSERT INTO " + TAB4_TABLE_NAME + " (" + TAB4_FIELD_CODE + ", " + TAB4_FIELD_NAME + ") VALUES (@p0,@p1)"
            Case TAB5_TABLE_NAME
                'รับเงิน
                _result = "INSERT INTO " + TAB5_TABLE_NAME + " (" + TAB5_FIELD_CODE + ", " + TAB5_FIELD_NAME + ") VALUES (@p0,@p1)"
            Case TAB6_TABLE_NAME
                'ใบเสร็จรับเงิน
                _result = "INSERT INTO " + TAB6_TABLE_NAME + " (" + TAB6_FIELD_CODE + ", " + TAB6_FIELD_NAME + ") VALUES (@p0,@p1)"
            Case TAB7_TABLE_NAME
                'ใบสั่งจ่าย
                _result = "INSERT INTO " + TAB7_TABLE_NAME + " (" + TAB7_FIELD_CODE + ", " + TAB7_FIELD_NAME + ") VALUES (@p0,@p1)"
            Case TAB8_TABLE_NAME
                'รายการหัก
                _result = "INSERT INTO " + TAB8_TABLE_NAME + " (" + TAB8_FIELD_CODE + ", " + TAB8_FIELD_NAME + ") VALUES (@p0,@p1)"
            Case TAB9_TABLE_NAME
                'บัญชีธนาคาร
                _result = "INSERT INTO " + TAB9_TABLE_NAME + " (" + TAB9_FIELD_CODE + ", " + TAB9_FIELD_NAME + ") VALUES (@p0,@p1)"
        End Select

        Return _result
    End Function
#End Region

End Class