﻿Imports System.Windows.Forms

Public Class frmMainMAINTYPEDESC

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

        ComboBox1.SelectedIndex = 0

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMB_PRENAME()
        'dv = New DataView(dt)
        Dim searchValue As String = TextBox1.Text.Trim.Replace(" ", "%")
        searchValue = "%" & searchValue & "%"

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", searchValue)
        parameters.Add("@p1", searchValue)
        parameters.Add("@p2", searchValue)
        parameters.Add("@p3", searchValue)
        parameters.Add("@p4", searchValue)
        parameters.Add("@p5", searchValue)

        Dim query As String = String.Empty
        query &= "SELECT MEMBER_MAIN_GROUP_CODE, MEMBER_GROUP_CODE, MEMBER_MAIN_TYPE_CODE, MEMBER_TYPE_CODE, MEMBER_TYPE_NAME, MEMBER_TYPE_NAME_EN, MEMBER_SHORT_NAME, MEMBER_SHORT_NAME_EN, INCM_FIRST_REGIST, INCM_FIRST_YEARLY, INCM_YEARLY, VAT_TYPE, VAT_RATE, INCM_NEXT_YEAR, INCM_PRE_PAID, INCM_PRE_FIRST_REGIST, INCM_PRE_FIRST_YEARLY, INCM_PRE_NEXT_YEAR, INCM_PRE_YEARLY, RUNNING, INACTIVE "
        query &= "FROM            MB_MEMBER_TYPE "
        query &= "WHERE        (MEMBER_MAIN_GROUP_CODE LIKE @p0) OR "
        query &= "                         (MEMBER_GROUP_CODE LIKE @p1) OR (MEMBER_MAIN_TYPE_CODE LIKE @p2) OR (MEMBER_TYPE_CODE LIKE @p3) OR (MEMBER_TYPE_NAME LIKE @p4) OR (MEMBER_TYPE_NAME_EN LIKE @p5) "
        query &= "ORDER BY MEMBER_MAIN_GROUP_CODE, MEMBER_GROUP_CODE, MEMBER_MAIN_TYPE_CODE"

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "MB_MEMBER_TYPE")

        If ds.Tables.Contains("MB_MEMBER_TYPE") = True Then
            ds.Tables("MB_MEMBER_TYPE").Clear()
            ds.Tables("MB_MEMBER_TYPE").Merge(dt)
            ds.Tables("MB_MEMBER_TYPE").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            ds.Tables("MB_MEMBER_TYPE").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER_TYPE").Columns("MEMBER_MAIN_GROUP_CODE"), ds.Tables("MB_MEMBER_TYPE").Columns("MEMBER_GROUP_CODE"), ds.Tables("MB_MEMBER_TYPE").Columns("MEMBER_MAIN_TYPE_CODE"), ds.Tables("MB_MEMBER_TYPE").Columns("MEMBER_TYPE_CODE")}

            DataGridView1.DataSource = ds.Tables("MB_MEMBER_TYPE")

            'For i As Integer = 0 To DataGridView1.ColumnCount - 1
            '    DataGridView1.Columns(i).Visible = False
            'Next
            'DataGridView1.Columns("PROVINCE_CODE").Visible = True
            'DataGridView1.Columns("PROVINCE_NAME_TH").Visible = True
            'DataGridView1.Columns("PROVINCE_NAME_EN").Visible = True
            'DataGridView1.Columns("PROVINCE_AREA").Visible = True

            DataGridView1.Columns("MEMBER_MAIN_GROUP_CODE").Width = 50
            DataGridView1.Columns("MEMBER_GROUP_CODE").Width = 50
            DataGridView1.Columns("MEMBER_MAIN_TYPE_CODE").Width = 50
            DataGridView1.Columns("MEMBER_TYPE_CODE").Width = 50
            DataGridView1.Columns("MEMBER_TYPE_NAME").Width = 150
            DataGridView1.Columns("MEMBER_TYPE_NAME_EN").Width = 150

            DataGridView1.Columns("MEMBER_MAIN_GROUP_CODE").ReadOnly = True
            DataGridView1.Columns("MEMBER_GROUP_CODE").ReadOnly = True
            DataGridView1.Columns("MEMBER_MAIN_TYPE_CODE").ReadOnly = True
            DataGridView1.Columns("MEMBER_TYPE_CODE").ReadOnly = True
        End If
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            getMB_PRENAME()
        End If
    End Sub

    Private Sub btApply_Click(sender As Object, e As EventArgs) Handles btApply.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If DataGridView1.CurrentRow IsNot Nothing Then
            DataGridView1.EndEdit()
            'bs.EndEdit()

            If MessageBox.Show("ยืนยันที่จะ" & btApply.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim row As DataRow = ds.Tables("MB_MEMBER_TYPE").Rows.Find(New Object() {DataGridView1.CurrentRow.Cells("MEMBER_MAIN_GROUP_CODE").Value, DataGridView1.CurrentRow.Cells("MEMBER_GROUP_CODE").Value, DataGridView1.CurrentRow.Cells("MEMBER_MAIN_TYPE_CODE").Value, DataGridView1.CurrentRow.Cells("MEMBER_TYPE_CODE").Value})

                Try
                    Dim parameters As New Dictionary(Of String, Object)
                    'parameters.Add("@p0", MODULE_ID)

                    Dim query As String = String.Empty
                    query &= "SELECT MEMBER_MAIN_GROUP_CODE, MEMBER_GROUP_CODE, MEMBER_MAIN_TYPE_CODE, MEMBER_TYPE_CODE, MEMBER_TYPE_NAME, MEMBER_TYPE_NAME_EN, MEMBER_SHORT_NAME, MEMBER_SHORT_NAME_EN, INCM_FIRST_REGIST, INCM_FIRST_YEARLY, INCM_YEARLY, VAT_TYPE, VAT_RATE, INCM_NEXT_YEAR, INCM_PRE_PAID, INCM_PRE_FIRST_REGIST, INCM_PRE_FIRST_YEARLY, INCM_PRE_NEXT_YEAR, INCM_PRE_YEARLY, RUNNING, INACTIVE "
                    query &= "FROM            MB_MEMBER_TYPE "

                    If ds.Tables("MB_MEMBER_TYPE").GetChanges IsNot Nothing Then
                        Try
                            updateWebSQL(query, parameters, ds.Tables("MB_MEMBER_TYPE"))
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "updateWebSQL")
                        End Try
                    End If

                    MessageBox.Show("Apply Successfully")
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "MB_MEMBER_TYPE.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'refresh grid
                ds.Tables("MB_MEMBER_TYPE").AcceptChanges()

                MessageBox.Show("บันทึกเสร็จสิ้น")
            End If


        End If
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        'new contact
        'Dim f As New frmMainProvincesNew
        'If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        '    'add it
        '    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        '    'generate code
        '    'SELECT TOP (1) POSITION_CODE FROM MB_POSITION ORDER BY CONVERT(INT, POSITION_CODE) DESC
        '    Dim parameters As New Dictionary(Of String, Object)
        '    'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

        '    Dim query As String = "SELECT TOP (1) PROVINCE_CODE FROM MB_PROVINCE ORDER BY CONVERT(INT, PROVINCE_CODE) DESC"

        '    Dim CNT As Integer = 0
        '    Try
        '        CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
        '    Catch ex As Exception
        '        MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT")
        '    End Try

        '    parameters.Clear()
        '    parameters.Add("@p0", (CNT + 1).ToString)
        '    parameters.Add("@p1", f.TextBox9.Text)
        '    parameters.Add("@p2", f.TextBox30.Text)
        '    parameters.Add("@p3", f.TextBox1.Text)

        '    query = "INSERT INTO MB_PROVINCE (PROVINCE_CODE, PROVINCE_NAME_TH, PROVINCE_NAME_EN, PROVINCE_AREA) VALUES (@p0,@p1,@p2,@p3)"

        '    Try
        '        executeWebSQL(query, parameters)
        '    Catch ex As Exception
        '        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        '    End Try

        '    'logs
        '    'saveLOGS(CONTACT_CODE, "MB_CONTACT", "CONTACT_CODE", "ADD", "", f.TextBox9.Text, user_name)

        '    'refresh grid
        '    getMB_PRENAME()

        '    MessageBox.Show("เพิ่มเสร็จสิ้น")
        'End If
        'f.Dispose()
        'f = Nothing
    End Sub

    Private Sub btDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        'del contact
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If DataGridView1.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView1.CurrentRow.Cells("MEMBER_TYPE_NAME").Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("MEMBER_MAIN_GROUP_CODE").Value)
                parameters.Add("@p1", DataGridView1.CurrentRow.Cells("MEMBER_GROUP_CODE").Value)
                parameters.Add("@p2", DataGridView1.CurrentRow.Cells("MEMBER_MAIN_TYPE_CODE").Value)
                parameters.Add("@p3", DataGridView1.CurrentRow.Cells("MEMBER_TYPE_CODE").Value)

                Dim query As String = "DELETE FROM MB_MEMBER_TYPE WHERE MEMBER_MAIN_GROUP_CODE = @p0 AND MEMBER_GROUP_CODE = @p1 AND MEMBER_MAIN_TYPE_CODE = @p2 AND MEMBER_TYPE_CODE = @p3"

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
