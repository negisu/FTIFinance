Public Class frmFTIFeesRule1

    Dim ds As DataSet
    Dim bsMEMBER_MAIN_GROUP As BindingSource
    Dim bsMEMBER_GROUP As BindingSource
    Dim bsMEMBER_MAIN_TYPE As BindingSource
    Dim bsMEMBER_TYPE As BindingSource
    Dim bsMB_MEMBER_RATE_MASTER As BindingSource

    Private Sub frmFeesRule1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet

        getMEMBER_MAIN_GROUP()
        getMEMBER_GROUP()
        getMEMBER_MAIN_TYPE()
        getMEMBER_TYPE()

        ComboBox1.SelectedIndex = 0
        ComboBox2.SelectedIndex = 0
        ComboBox3.SelectedIndex = 0
        ComboBox4.SelectedIndex = 0

        ComboBox1_SelectedIndexChanged(sender, e)
        ComboBox2_SelectedIndexChanged(sender, e)
        ComboBox3_SelectedIndexChanged(sender, e)
        ComboBox4_SelectedIndexChanged(sender, e)

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMEMBER_MAIN_GROUP()
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", "0")
        'parameters.Add("@p1", "1")
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT [MEMBER_MAIN_GROUP_CODE], [MEMBER_MAIN_GROUP_NAME], [INACTIVE] FROM [MB_MEMBER_MAIN_GROUP] "
        query &= "WHERE ([INACTIVE] <> 1) "
        query &= "ORDER BY MEMBER_MAIN_GROUP_CODE "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_MAIN_GROUP").Copy
        'dt.TableName = "MB_MEMBER_MAIN_GROUP"
        If ds.Tables.Contains("MB_MEMBER_MAIN_GROUP") = True Then
            ds.Tables("MB_MEMBER_MAIN_GROUP").Clear()
            ds.Tables("MB_MEMBER_MAIN_GROUP").Merge(dt)
            ds.Tables("MB_MEMBER_MAIN_GROUP").AcceptChanges()
        Else
            ds.Tables.Add(dt)
            bsMEMBER_MAIN_GROUP = New BindingSource(ds, "MB_MEMBER_MAIN_GROUP")

            ComboBox1.DataSource = bsMEMBER_MAIN_GROUP
            ComboBox1.DisplayMember = "MEMBER_MAIN_GROUP_NAME"
            ComboBox1.ValueMember = "MEMBER_MAIN_GROUP_CODE"
        End If

        
    End Sub

    Private Sub getMEMBER_GROUP()
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", MEMBER_MAIN_GROUP_CODE)
        'parameters.Add("@p1", "1")
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT MEMBER_MAIN_GROUP_CODE, [MEMBER_GROUP_CODE], [MEMBER_GROUP_NAME], [MEMBER_GROUP_NAME_EN], [INACTIVE] FROM [MB_MEMBER_GROUP] "
        query &= "WHERE ([INACTIVE] <> 1) "
        query &= "ORDER BY [MEMBER_GROUP_CODE] "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_GROUP").Copy
        'dt.TableName = "MB_MEMBER_GROUP"
        If ds.Tables.Contains("MB_MEMBER_GROUP") = True Then
            ds.Tables("MB_MEMBER_GROUP").Clear()
            ds.Tables("MB_MEMBER_GROUP").Merge(dt)
            ds.Tables("MB_MEMBER_GROUP").AcceptChanges()
        Else
            ds.Tables.Add(dt)
            bsMEMBER_GROUP = New BindingSource(ds, "MB_MEMBER_GROUP")

            ComboBox2.DataSource = bsMEMBER_GROUP
            ComboBox2.DisplayMember = "MEMBER_GROUP_NAME"
            ComboBox2.ValueMember = "MEMBER_GROUP_CODE"
        End If

        
    End Sub

    Private Sub getMEMBER_MAIN_TYPE()
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", MEMBER_MAIN_GROUP_CODE)
        'parameters.Add("@p1", MEMBER_GROUP_CODE)
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT MEMBER_MAIN_GROUP_CODE, [MEMBER_GROUP_CODE], [MEMBER_MAIN_TYPE_CODE], [MEMBER_MAIN_TYPE_NAME], [INACTIVE] FROM [MB_MEMBER_MAIN_TYPE] "
        'query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) "
        query &= "WHERE ([INACTIVE] <> 1) "
        query &= "ORDER BY MEMBER_MAIN_TYPE_CODE "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_MAIN_TYPE").Copy
        'dt.TableName = "MB_MEMBER_MAIN_TYPE"
        If ds.Tables.Contains("MB_MEMBER_MAIN_TYPE") = True Then
            ds.Tables("MB_MEMBER_MAIN_TYPE").Clear()
            ds.Tables("MB_MEMBER_MAIN_TYPE").Merge(dt)
            ds.Tables("MB_MEMBER_MAIN_TYPE").AcceptChanges()
        Else
            ds.Tables.Add(dt)
            bsMEMBER_MAIN_TYPE = New BindingSource(ds, "MB_MEMBER_MAIN_TYPE")

            ComboBox3.DataSource = bsMEMBER_MAIN_TYPE
            ComboBox3.DisplayMember = "MEMBER_MAIN_TYPE_NAME"
            ComboBox3.ValueMember = "MEMBER_MAIN_TYPE_CODE"
        End If

        
    End Sub

    Private Sub getMEMBER_TYPE()
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", MEMBER_MAIN_GROUP_CODE)
        'parameters.Add("@p1", MEMBER_GROUP_CODE)
        'parameters.Add("@p2", MEMBER_MAIN_TYPE_CODE)
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT  MEMBER_MAIN_GROUP_CODE, [MEMBER_GROUP_CODE], [MEMBER_MAIN_TYPE_CODE], [MEMBER_TYPE_CODE], [MEMBER_TYPE_NAME], [INACTIVE] FROM [MB_MEMBER_TYPE] "
        'query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) "
        query &= "WHERE ([INACTIVE] <> 1) "
        query &= "ORDER BY [MEMBER_TYPE_CODE] "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_TYPE").Copy
        'dt.TableName = "MB_MEMBER_TYPE"
        If ds.Tables.Contains("MB_MEMBER_TYPE") = True Then
            ds.Tables("MB_MEMBER_TYPE").Clear()
            ds.Tables("MB_MEMBER_TYPE").Merge(dt)
            ds.Tables("MB_MEMBER_TYPE").AcceptChanges()
        Else
            ds.Tables.Add(dt)
            bsMEMBER_TYPE = New BindingSource(ds, "MB_MEMBER_TYPE")

            ComboBox4.DataSource = bsMEMBER_TYPE
            ComboBox4.DisplayMember = "MEMBER_TYPE_NAME"
            ComboBox4.ValueMember = "MEMBER_TYPE_CODE"
        End If

        

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedValueChanged
        'If ComboBox1.SelectedValue IsNot Nothing Then getMEMBER_GROUP(ComboBox1.SelectedValue.ToString)
        If ComboBox1.SelectedValue IsNot Nothing And bsMEMBER_GROUP IsNot Nothing Then
            bsMEMBER_GROUP.Filter = String.Format("(MEMBER_MAIN_GROUP_CODE = '{0}')", ComboBox1.SelectedValue.ToString)
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedValueChanged
        'If ComboBox1.SelectedValue IsNot Nothing And ComboBox2.SelectedValue IsNot Nothing Then getMEMBER_MAIN_TYPE(ComboBox1.SelectedValue.ToString, ComboBox2.SelectedValue.ToString)
        If ComboBox1.SelectedValue IsNot Nothing And ComboBox2.SelectedValue IsNot Nothing And bsMEMBER_MAIN_TYPE IsNot Nothing Then
            bsMEMBER_MAIN_TYPE.Filter = String.Format("(MEMBER_MAIN_GROUP_CODE = '{0}') AND (MEMBER_GROUP_CODE = '{1}')", ComboBox1.SelectedValue.ToString, ComboBox2.SelectedValue.ToString)
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedValueChanged
        'If ComboBox1.SelectedValue IsNot Nothing And ComboBox2.SelectedValue IsNot Nothing And ComboBox3.SelectedValue IsNot Nothing Then getMEMBER_TYPE(ComboBox1.SelectedValue.ToString, ComboBox2.SelectedValue.ToString, ComboBox3.SelectedValue.ToString)
        If ComboBox1.SelectedValue IsNot Nothing And ComboBox2.SelectedValue IsNot Nothing And ComboBox3.SelectedValue IsNot Nothing And bsMEMBER_TYPE IsNot Nothing Then
            bsMEMBER_TYPE.Filter = String.Format("(MEMBER_MAIN_GROUP_CODE = '{0}') AND (MEMBER_GROUP_CODE = '{1}') AND (MEMBER_MAIN_TYPE_CODE = '{2}')", ComboBox1.SelectedValue.ToString, ComboBox2.SelectedValue.ToString, ComboBox3.SelectedValue.ToString)
        End If
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedValueChanged
        If ComboBox1.SelectedValue IsNot Nothing And ComboBox2.SelectedValue IsNot Nothing And ComboBox3.SelectedValue IsNot Nothing And ComboBox4.SelectedValue IsNot Nothing Then
            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", ComboBox1.SelectedValue.ToString)
            parameters.Add("@p1", ComboBox2.SelectedValue.ToString)
            parameters.Add("@p2", ComboBox3.SelectedValue.ToString)
            parameters.Add("@p3", ComboBox4.SelectedValue.ToString)

            '==============================================
            Dim query1 As String = "SELECT * FROM MB_MEMBER_RATE_MASTER "
            'query &= "SELECT [MEMBER_TYPE_CODE], [MEMBER_TYPE_NAME], [INACTIVE] FROM [MB_MEMBER_TYPE] "
            query1 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) "
            'query1 &= "ORDER BY [MEMBER_TYPE_CODE] "

            Dim dt1 As DataTable = fillWebSQL(query1, parameters, "MB_MEMBER_RATE_MASTER").Copy
            'dt1.TableName = "MB_MEMBER_RATE_MASTER"
            If ds.Tables.Contains("MB_MEMBER_RATE_MASTER") = True Then
                ds.Tables("MB_MEMBER_RATE_MASTER").Clear()
                ds.Tables("MB_MEMBER_RATE_MASTER").Merge(dt1)
                ds.Tables("MB_MEMBER_RATE_MASTER").AcceptChanges()
            Else
                ds.Tables.Add(dt1)
                bsMB_MEMBER_RATE_MASTER = New BindingSource(ds, "MB_MEMBER_RATE_MASTER")

                ComboBox5.DataBindings.Add("Text", bsMB_MEMBER_RATE_MASTER, "CALCULATE_TYPE")
                TextBox1.DataBindings.Add("Text", bsMB_MEMBER_RATE_MASTER, "CALCULATE_FIELD")
            End If

            'If ds.Tables("MB_MEMBER_RATE_MASTER").Rows.Count > 0 Then
            'ComboBox5.Text = ds.Tables("MB_MEMBER_RATE_MASTER").Rows(0).Item("CALCULATE_TYPE").ToString
            'TextBox1.Text = ds.Tables("MB_MEMBER_RATE_MASTER").Rows(0).Item("CALCULATE_FIELD").ToString

            'MessageBox.Show(ds.Tables("MB_MEMBER_RATE_MASTER").Rows(0).Item("CALCULATE_FIELD").ToString)
            'End If
            'MessageBox.Show(ds.Tables("MB_MEMBER_RATE_MASTER").Rows.Count.ToString)
            '==============================================
            Dim query2 As String = "SELECT * FROM MB_MEMBER_RATE_DETAIL "
            'query &= "SELECT [MEMBER_TYPE_CODE], [MEMBER_TYPE_NAME], [INACTIVE] FROM [MB_MEMBER_TYPE] "
            query2 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) "
            query2 &= "ORDER BY [RATE_SEQ] "

            Dim dt2 As DataTable = fillWebSQL(query2, parameters, "MB_MEMBER_RATE_DETAIL").Copy
            'dt2.TableName = "MB_MEMBER_RATE_DETAIL"
            If ds.Tables.Contains("MB_MEMBER_RATE_DETAIL") = True Then
                ds.Tables("MB_MEMBER_RATE_DETAIL").Clear()
                ds.Tables("MB_MEMBER_RATE_DETAIL").Merge(dt2)
                ds.Tables("MB_MEMBER_RATE_DETAIL").AcceptChanges()
            Else
                ds.Tables.Add(dt2)

                '==============================================
                DataGridView1.DataSource = ds.Tables("MB_MEMBER_RATE_DETAIL")

                For idx As Integer = 0 To DataGridView1.ColumnCount - 1
                    DataGridView1.Columns(idx).Visible = False
                Next

                DataGridView1.Columns("RATE_SEQ").Visible = True
                DataGridView1.Columns("START_INCOME_AMOUNT").Visible = True
                DataGridView1.Columns("END_INCOME_AMOUNT").Visible = True
                DataGridView1.Columns("MEMBER_YEARLY_RATE").Visible = True
                DataGridView1.Columns("START_MONTH_FIRST_YEAR").Visible = True
                DataGridView1.Columns("END_MONTH_FIRST_YEAR").Visible = True
                DataGridView1.Columns("FIRST_YEAR_RATE").Visible = True
                DataGridView1.Columns("FLAG_SUM_NEXT_YEAR").Visible = True
                DataGridView1.Columns("FIRST_REGIST_RATE").Visible = True

                DataGridView1.Columns("RATE_SEQ").Width = 50
                DataGridView1.Columns("START_INCOME_AMOUNT").Width = 100
                DataGridView1.Columns("END_INCOME_AMOUNT").Width = 100
                DataGridView1.Columns("MEMBER_YEARLY_RATE").Width = 80
                DataGridView1.Columns("START_MONTH_FIRST_YEAR").Width = 50
                DataGridView1.Columns("END_MONTH_FIRST_YEAR").Width = 50
                DataGridView1.Columns("FIRST_YEAR_RATE").Width = 80
                DataGridView1.Columns("FLAG_SUM_NEXT_YEAR").Width = 50
                DataGridView1.Columns("FIRST_REGIST_RATE").Width = 80

                DataGridView1.Columns("START_INCOME_AMOUNT").DefaultCellStyle.Format = "#,##0.00"
                DataGridView1.Columns("END_INCOME_AMOUNT").DefaultCellStyle.Format = "#,##0.00"
                DataGridView1.Columns("MEMBER_YEARLY_RATE").DefaultCellStyle.Format = "#,##0.00"
                DataGridView1.Columns("FIRST_YEAR_RATE").DefaultCellStyle.Format = "#,##0.00"
                DataGridView1.Columns("FIRST_REGIST_RATE").DefaultCellStyle.Format = "#,##0.00"

                DataGridView1.Columns("START_INCOME_AMOUNT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DataGridView1.Columns("END_INCOME_AMOUNT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DataGridView1.Columns("MEMBER_YEARLY_RATE").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DataGridView1.Columns("FIRST_YEAR_RATE").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DataGridView1.Columns("FIRST_REGIST_RATE").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If



            'If ds.Tables("MB_MEMBER_RATE_MASTER").Rows.Count > 0 Then ComboBox5.Text = ds.Tables("MB_MEMBER_RATE_MASTER").Rows(0).Item("CALCULATE_TYPE").ToString
            'ComboBox6.SelectedIndex = 0
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", ComboBox1.SelectedValue.ToString)
            parameters.Add("@p1", ComboBox2.SelectedValue.ToString)
            parameters.Add("@p2", ComboBox3.SelectedValue.ToString)
            parameters.Add("@p3", ComboBox4.SelectedValue.ToString)

            DataGridView1.EndEdit()
            bsMB_MEMBER_RATE_MASTER.EndEdit()
            Dim query1 As String = "SELECT * FROM MB_MEMBER_RATE_MASTER "
            query1 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) "

            If ds.Tables("MB_MEMBER_RATE_MASTER").GetChanges IsNot Nothing Then
                Try
                    updateWebSQL(query1, parameters, ds.Tables("MB_MEMBER_RATE_MASTER"))
                    ds.Tables("MB_MEMBER_RATE_MASTER").AcceptChanges()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "updateWebSQL")
                End Try
            End If

            Dim query2 As String = "SELECT * FROM MB_MEMBER_RATE_DETAIL "
            query2 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) "
            query2 &= "ORDER BY [RATE_SEQ] "

            If ds.Tables("MB_MEMBER_RATE_DETAIL").GetChanges IsNot Nothing Then
                Try
                    updateWebSQL(query2, parameters, ds.Tables("MB_MEMBER_RATE_DETAIL"))
                    ds.Tables("MB_MEMBER_RATE_DETAIL").AcceptChanges()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "updateWebSQL")
                End Try
            End If

            MessageBox.Show("Apply Successfully")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "MB_MEMBER_RATE_DETAIL.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class