Imports System.Windows.Forms
Imports System.IO

Public Class frmMainProducts

    'Friend dtMB_MAIN_INDUSTRY As DataTable
    'Friend dtMB_PRODUCT As DataTable
    Friend MAIN_INDUSTRY_CODE As String

    Dim ds As DataSet
    Dim bsMB_MAIN_INDUSTRY As BindingSource
    Dim bsMB_PRODUCT As BindingSource

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If tbMAIN_INDUSTRY_CODE.TextLength > 0 And tbPRODUCT_CODE.TextLength > 0 Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub getMB_MAIN_INDUSTRY()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT MAIN_INDUSTRY_CODE, MAIN_INDUSTRY_DESC_TH, MAIN_INDUSTRY_DESC_EN FROM MB_MAIN_INDUSTRY ORDER BY MAIN_INDUSTRY_DESC_TH"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MAIN_INDUSTRY").Copy

        If ds.Tables.Contains("MB_MAIN_INDUSTRY") = True Then
            ds.Tables("MB_MAIN_INDUSTRY").Clear()
            ds.Tables("MB_MAIN_INDUSTRY").Merge(dt)
            ds.Tables("MB_MAIN_INDUSTRY").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If
    End Sub

    Private Sub getMB_PRODUCT()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT PRODUCT_CODE, PRODUCT_DESC_TH, PRODUCT_DESC_EN FROM MB_PRODUCT ORDER BY PRODUCT_DESC_TH"

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_PRODUCT").Copy

        If ds.Tables.Contains("MB_PRODUCT") = True Then
            ds.Tables("MB_PRODUCT").Clear()
            ds.Tables("MB_PRODUCT").Merge(dt)
            ds.Tables("MB_PRODUCT").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If
    End Sub

    Private Sub frmFTIFileNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet
        'ds.Tables.Add(dtMB_MAIN_INDUSTRY)
        'ds.Tables.Add(dtMB_PRODUCT)

        getMB_MAIN_INDUSTRY()
        getMB_PRODUCT()

        bsMB_MAIN_INDUSTRY = New BindingSource(ds, "MB_MAIN_INDUSTRY")
        bsMB_PRODUCT = New BindingSource(ds, "MB_PRODUCT")

        DataGridView10.DataSource = bsMB_MAIN_INDUSTRY
        'For i As Integer = 0 To DataGridView10.RowCount - 1
        '    DataGridView10.Columns(i).Visible = False
        'Next
        'DataGridView10.Columns("MAIN_INDUSTRY_CODE").Visible = True
        DataGridView10.Columns("MAIN_INDUSTRY_DESC_TH").Width = 200
        DataGridView10.Columns("MAIN_INDUSTRY_DESC_EN").Width = 200

        DataGridView1.DataSource = bsMB_PRODUCT
        'For i As Integer = 0 To DataGridView1.RowCount - 1
        '    DataGridView1.Columns(i).Visible = False
        'Next
        'DataGridView1.Columns("PRODUCT_CODE").Visible = True
        DataGridView1.Columns("PRODUCT_DESC_TH").Width = 200
        DataGridView1.Columns("PRODUCT_DESC_EN").Width = 200

        DataGridView10.Columns("MAIN_INDUSTRY_CODE").ReadOnly = True
        DataGridView1.Columns("PRODUCT_CODE").ReadOnly = True

        tbMAIN_INDUSTRY_CODE.DataBindings.Add("Text", bsMB_MAIN_INDUSTRY, "MAIN_INDUSTRY_CODE")
        tbPRODUCT_CODE.DataBindings.Add("Text", bsMB_PRODUCT, "PRODUCT_CODE")

        If MAIN_INDUSTRY_CODE IsNot Nothing And DataGridView10.Rows.Count > 0 Then TextBox1.Text = MAIN_INDUSTRY_CODE

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    'Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
    '    If e.KeyCode = Keys.Enter Then
    '        getMB_MAIN_INDUSTRY(TextBox1.Text)
    '    End If
    'End Sub

    'Private Sub TextBox2_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyUp
    '    If e.KeyCode = Keys.Enter Then
    '        getMB_PRODUCT(TextBox2.Text)
    '    End If
    'End Sub

    Private Sub btFindIndus_Click(sender As Object, e As EventArgs) Handles btFindIndus.Click
        TextBox1_TextChanged(sender, e)
    End Sub

    Private Sub btFindProd_Click(sender As Object, e As EventArgs) Handles btFindProd.Click
        TextBox2_TextChanged(sender, e)
    End Sub

    Private Sub btNewIndus_Click(sender As Object, e As EventArgs) Handles btNewIndus.Click
        'new contact
        Dim f As New frmMainIndusNew
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            'add it

            'generate code
            'SELECT TOP (1) POSITION_CODE FROM MB_POSITION ORDER BY CONVERT(INT, POSITION_CODE) DESC
            Dim parameters As New Dictionary(Of String, Object)
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            Dim query As String = "SELECT TOP (1) CONVERT(INT, MAIN_INDUSTRY_CODE) AS CNT FROM MAIN_INDUSTRY_CODE ORDER BY CONVERT(INT, MAIN_INDUSTRY_CODE) DESC"

            Dim CNT As Integer = 0
            Try
                CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT")
            End Try

            parameters.Clear()
            parameters.Add("@p0", Format((CNT + 1), "00000"))
            parameters.Add("@p1", f.TextBox9.Text)
            parameters.Add("@p2", f.TextBox30.Text)
            'parameters.Add("@p3", f.TextBox1.Text)

            query = "INSERT INTO MAIN_INDUSTRY_CODE (MAIN_INDUSTRY_CODE, MAIN_INDUSTRY_DESC_TH, MAIN_INDUSTRY_DESC_EN) VALUES (@p0,@p1,@p2)"

            Try
                executeWebSQL(query, parameters)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            End Try

            'logs
            'saveLOGS(CONTACT_CODE, "MB_CONTACT", "CONTACT_CODE", "ADD", "", f.TextBox9.Text, user_name)

            'refresh grid
            getMB_MAIN_INDUSTRY()

            MessageBox.Show("เพิ่มเสร็จสิ้น")
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btDeleteIndus_Click(sender As Object, e As EventArgs) Handles btDeleteIndus.Click
        'del contact
        If DataGridView10.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView10.CurrentRow.Cells("MAIN_INDUSTRY_DESC_TH").Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView10.CurrentRow.Cells("MAIN_INDUSTRY_CODE").Value)

                Dim query As String = "DELETE FROM MAIN_INDUSTRY_CODE WHERE MAIN_INDUSTRY_CODE = @p0"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'logs
                'saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT", "CONTACT_CODE", "ADD", "", TextBox9.Text, user_name)

                'refresh grid
                getMB_MAIN_INDUSTRY()

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btApplyIndus_Click(sender As Object, e As EventArgs) Handles btApplyIndus.Click
        DataGridView10.EndEdit()
        'bs.EndEdit()

        If MessageBox.Show("ยืนยันที่จะ" & btApplyIndus.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            'Dim row As DataRow = ds.Tables("MAIN_INDUSTRY_CODE").Rows.Find(DataGridView1.CurrentRow.Cells("MAIN_INDUSTRY_CODE").Value)

            Try
                Dim parameters As New Dictionary(Of String, Object)
                'parameters.Add("@p0", MODULE_ID)

                Dim query As String = String.Empty
                query &= "SELECT * "
                query &= "FROM            MAIN_INDUSTRY_CODE "
                'query &= "WHERE        (POSITION_NAME_TH LIKE @p0) OR "
                'query &= "                         (POSITION_NAME_EN LIKE @p1) "
                'query &= "ORDER BY POSITION_NAME_TH, POSITION_NAME_EN"

                If ds.Tables("MAIN_INDUSTRY_CODE").GetChanges IsNot Nothing Then
                    Try
                        updateWebSQL(query, parameters, ds.Tables("MAIN_INDUSTRY_CODE"))
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "updateWebSQL")
                    End Try
                End If

                MessageBox.Show("Apply Successfully")
            Catch ex As Exception
                MessageBox.Show(ex.Message, "MAIN_INDUSTRY_CODE.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            'refresh grid
            ds.Tables("MAIN_INDUSTRY_CODE").AcceptChanges()

            MessageBox.Show("บันทึกเสร็จสิ้น")
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim SEARCH As String = "%" & TextBox1.Text.Replace(" ", "%") & "%"
        bsMB_MAIN_INDUSTRY.Filter = String.Format("(MAIN_INDUSTRY_CODE LIKE '{0}') OR (MAIN_INDUSTRY_DESC_TH LIKE '{0}') OR (MAIN_INDUSTRY_DESC_EN LIKE '{0}')", SEARCH)
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Dim SEARCH As String = "%" & TextBox2.Text.Replace(" ", "%") & "%"
        bsMB_PRODUCT.Filter = String.Format("(PRODUCT_CODE LIKE '{0}') OR (PRODUCT_DESC_TH LIKE '{0}') OR (PRODUCT_DESC_EN LIKE '{0}')", SEARCH)
    End Sub

    Private Sub btNewProduct_Click(sender As Object, e As EventArgs) Handles btNewProduct.Click
        'new contact
        Dim f As New frmMainProductsNew
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            'add it

            'generate code
            'SELECT TOP (1) POSITION_CODE FROM MB_POSITION ORDER BY CONVERT(INT, POSITION_CODE) DESC
            Dim parameters As New Dictionary(Of String, Object)
            'parameters.Add("@p0", DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value)

            Dim query As String = "SELECT TOP 1 CASE WHEN ISNUMERIC(PRODUCT_CODE) > 0 THEN CAST(PRODUCT_CODE AS INT) ELSE 0 END AS CNT FROM [FTI].[dbo].[MB_PRODUCT] ORDER BY CASE WHEN ISNUMERIC(PRODUCT_CODE) > 0 THEN CAST(PRODUCT_CODE AS INT) ELSE 0 END DESC"

            Dim CNT As Integer = 0
            Try
                CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CNT")
            End Try

            parameters.Clear()
            parameters.Add("@p0", Format((CNT + 1), "00000"))
            parameters.Add("@p1", f.TextBox9.Text)
            parameters.Add("@p2", f.TextBox30.Text)
            'parameters.Add("@p3", f.TextBox1.Text)

            query = "INSERT INTO MB_PRODUCT (PRODUCT_CODE, PRODUCT_DESC_TH, PRODUCT_DESC_EN) VALUES (@p0,@p1,@p2)"

            Try
                executeWebSQL(query, parameters)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            End Try

            'logs
            'saveLOGS(CONTACT_CODE, "MB_CONTACT", "CONTACT_CODE", "ADD", "", f.TextBox9.Text, user_name)

            'refresh grid
            getMB_PRODUCT()

            MessageBox.Show("เพิ่มเสร็จสิ้น")
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btDelProduct_Click(sender As Object, e As EventArgs) Handles btDelProduct.Click
        'del contact
        If DataGridView1.CurrentRow IsNot Nothing Then
            If MessageBox.Show("ยืนยันที่จะลบ " & DataGridView1.CurrentRow.Cells("PRODUCT_DESC_TH").Value.ToString, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                'delete it
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", DataGridView1.CurrentRow.Cells("PRODUCT_CODE").Value)

                Dim query As String = "DELETE FROM MB_PRODUCT WHERE PRODUCT_CODE = @p0"

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try

                'logs
                'saveLOGS(DataGridView1.CurrentRow.Cells("CONTACT_CODE").Value.ToString, "MB_CONTACT", "CONTACT_CODE", "ADD", "", TextBox9.Text, user_name)

                'refresh grid
                getMB_PRODUCT()

                MessageBox.Show("ลบเสร็จสิ้น")
            End If
        End If
    End Sub

    Private Sub btApplyProduct_Click(sender As Object, e As EventArgs) Handles btApplyProduct.Click
        DataGridView1.EndEdit()
        'bs.EndEdit()

        If MessageBox.Show("ยืนยันที่จะ" & btApplyProduct.Text & "?", "ยืนยัน?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            'Dim row As DataRow = ds.Tables("MAIN_INDUSTRY_CODE").Rows.Find(DataGridView1.CurrentRow.Cells("MAIN_INDUSTRY_CODE").Value)

            Try
                Dim parameters As New Dictionary(Of String, Object)
                'parameters.Add("@p0", MODULE_ID)

                Dim query As String = String.Empty
                query &= "SELECT * "
                query &= "FROM            MB_PRODUCT "
                'query &= "WHERE        (POSITION_NAME_TH LIKE @p0) OR "
                'query &= "                         (POSITION_NAME_EN LIKE @p1) "
                'query &= "ORDER BY POSITION_NAME_TH, POSITION_NAME_EN"

                If ds.Tables("MB_PRODUCT").GetChanges IsNot Nothing Then
                    Try
                        updateWebSQL(query, parameters, ds.Tables("MB_PRODUCT"))
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "updateWebSQL")
                    End Try
                End If

                MessageBox.Show("Apply Successfully")
            Catch ex As Exception
                MessageBox.Show(ex.Message, "MB_PRODUCT.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            'refresh grid
            ds.Tables("MB_PRODUCT").AcceptChanges()

            MessageBox.Show("บันทึกเสร็จสิ้น")
        End If
    End Sub
End Class
