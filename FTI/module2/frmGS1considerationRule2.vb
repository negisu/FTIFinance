Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

Public Class frmGS1considerationRule2

    Dim ds As DataSet
    Dim bs As BindingSource

    Private Sub frmFeesRule2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet

        Try
            getMEMBER_MAIN_GROUP()
            getMEMBER_GROUP()
            getMEMBER_MAIN_TYPE()
            'getMEMBER_TYPE()
        Catch ex As Exception
            MessageBox.Show("getMEMBER" & vbCrLf & ex.Message)
        End Try

        Dim errs As Integer = 0
        Try
            ds.Relations.Add("relMainGroupGroup", ds.Tables("MB_MEMBER_MAIN_GROUP").Columns("MEMBER_MAIN_GROUP_CODE"), ds.Tables("MB_MEMBER_GROUP").Columns("MEMBER_MAIN_GROUP_CODE"))
            errs = 1
            Dim colGroup As DataColumn() = New DataColumn() {ds.Tables("MB_MEMBER_GROUP").Columns("MEMBER_MAIN_GROUP_CODE"), ds.Tables("MB_MEMBER_GROUP").Columns("MEMBER_GROUP_CODE")}
            Dim colMainType As DataColumn() = New DataColumn() {ds.Tables("MB_MEMBER_MAIN_TYPE").Columns("MEMBER_MAIN_GROUP_CODE"), ds.Tables("MB_MEMBER_MAIN_TYPE").Columns("MEMBER_GROUP_CODE")}
            ds.Relations.Add("relGroupMainType", colGroup, colMainType)
            errs = 2
            'Dim colMainTypeType As DataColumn() = New DataColumn() {ds.Tables("MB_MEMBER_MAIN_TYPE").Columns("MEMBER_MAIN_GROUP_CODE"), ds.Tables("MB_MEMBER_MAIN_TYPE").Columns("MEMBER_GROUP_CODE"), ds.Tables("MB_MEMBER_MAIN_TYPE").Columns("MEMBER_MAIN_TYPE_CODE")}
            'Dim colType As DataColumn() = New DataColumn() {ds.Tables("MB_MEMBER_TYPE").Columns("MEMBER_MAIN_GROUP_CODE"), ds.Tables("MB_MEMBER_TYPE").Columns("MEMBER_GROUP_CODE"), ds.Tables("MB_MEMBER_TYPE").Columns("MEMBER_MAIN_TYPE_CODE")}
            'ds.Relations.Add("relMainTypeType", colMainTypeType, colType)
            errs = 3
        Catch ex As Exception
            MessageBox.Show("Relations=" & errs & vbCrLf & ex.Message)
        End Try

        Try
            With ComboBox1
                .DisplayMember = "MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_NAME"
                .ValueMember = "MEMBER_MAIN_GROUP_CODE"
                .DataSource = ds
            End With

            With ComboBox2
                .DisplayMember = "MB_MEMBER_MAIN_GROUP.relMainGroupGroup.MEMBER_GROUP_NAME"
                .ValueMember = "MEMBER_GROUP_CODE"
                .DataSource = ds
            End With

            With ComboBox3
                .DisplayMember = "MB_MEMBER_GROUP.relGroupMainType.MEMBER_MAIN_TYPE_NAME"
                .ValueMember = "MEMBER_MAIN_TYPE_CODE"
                .DataSource = ds
            End With

            'With ComboBox4
            '    .DisplayMember = "MB_MEMBER_MAIN_TYPE.relMainTypeType.MEMBER_TYPE_NAME"
            '    .ValueMember = "MEMBER_TYPE_CODE"
            '    .DataSource = ds
            'End With
        Catch ex As Exception
            MessageBox.Show("ComboBox1=" & errs & vbCrLf & ex.Message)
        End Try

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMEMBER_MAIN_GROUP()
        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM [MB_MEMBER_MAIN_GROUP] "
        query &= "WHERE (MEMBER_MAIN_GROUP_CODE = '300') "
        query &= "ORDER BY MEMBER_MAIN_GROUP_CODE "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_MAIN_GROUP").Copy
        'dt.TableName = "MB_MEMBER_MAIN_GROUP"
        'If ds.Tables.Contains("MB_MEMBER_MAIN_GROUP") Then ds.Tables.Remove("MB_MEMBER_MAIN_GROUP")
        ds.Tables.Add(dt)

        'ComboBox1.DataSource = ds.Tables("MB_MEMBER_MAIN_GROUP")
        'ComboBox1.DisplayMember = "MEMBER_MAIN_GROUP_NAME"
        'ComboBox1.ValueMember = "MEMBER_MAIN_GROUP_CODE"
    End Sub

    Private Sub getMEMBER_GROUP()
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", MEMBER_MAIN_GROUP_CODE)
        'parameters.Add("@p1", "1")
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT * FROM [MB_MEMBER_GROUP] "
        query &= "WHERE (MEMBER_MAIN_GROUP_CODE = '300') "
        query &= "ORDER BY [MEMBER_GROUP_CODE] "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_GROUP").Copy
        'dt.TableName = "MB_MEMBER_GROUP"
        'If ds.Tables.Contains("MB_MEMBER_GROUP") Then ds.Tables.Remove("MB_MEMBER_GROUP")
        ds.Tables.Add(dt)

        'ComboBox2.DataSource = ds.Tables("MB_MEMBER_GROUP")
        'ComboBox2.DisplayMember = "MEMBER_GROUP_NAME"
        'ComboBox2.ValueMember = "MEMBER_GROUP_CODE"
    End Sub

    Private Sub getMEMBER_MAIN_TYPE()
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", MEMBER_MAIN_GROUP_CODE)
        'parameters.Add("@p1", MEMBER_GROUP_CODE)
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT * FROM [MB_MEMBER_MAIN_TYPE] "
        query &= "WHERE (MEMBER_MAIN_GROUP_CODE = '300') "
        query &= "ORDER BY MEMBER_MAIN_TYPE_CODE "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_MAIN_TYPE").Copy
        'dt.TableName = "MB_MEMBER_MAIN_TYPE"
        'If ds.Tables.Contains("MB_MEMBER_MAIN_TYPE") Then ds.Tables.Remove("MB_MEMBER_MAIN_TYPE")
        ds.Tables.Add(dt)

        'ComboBox3.DataSource = ds.Tables("MB_MEMBER_MAIN_TYPE")
        'ComboBox3.DisplayMember = "MEMBER_MAIN_TYPE_NAME"
        'ComboBox3.ValueMember = "MEMBER_MAIN_TYPE_CODE"
    End Sub

    Private Sub getMEMBER_TYPE(ByVal MEMBER_MAIN_GROUP_CODE As String, ByVal MEMBER_GROUP_CODE As String, ByVal MEMBER_MAIN_TYPE_CODE As String)
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", MEMBER_MAIN_GROUP_CODE)
        parameters.Add("@p1", MEMBER_GROUP_CODE)
        parameters.Add("@p2", MEMBER_MAIN_TYPE_CODE)
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT * FROM [MB_MEMBER_TYPE] "
        query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) "
        query &= "ORDER BY [MEMBER_TYPE_CODE] "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_TYPE").Copy
        'dt.TableName = "MB_MEMBER_TYPE"
        'If ds.Tables.Contains("MB_MEMBER_TYPE") Then ds.Tables.Remove("MB_MEMBER_TYPE")
        If ds.Tables.Contains("MB_MEMBER_TYPE") = True Then
            ds.Tables("MB_MEMBER_TYPE").Clear()
            ds.Tables("MB_MEMBER_TYPE").Merge(dt)
            ds.Tables("MB_MEMBER_TYPE").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox4.DataSource = ds.Tables("MB_MEMBER_TYPE")
        ComboBox4.DisplayMember = "MEMBER_TYPE_NAME"
        ComboBox4.ValueMember = "MEMBER_TYPE_CODE"
    End Sub

    'Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.DropDownClosed
    '    If ComboBox1.SelectedValue IsNot Nothing Then getMEMBER_GROUP(ComboBox1.SelectedValue.ToString)
    'End Sub

    'Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.DropDownClosed
    '    If ComboBox1.SelectedValue IsNot Nothing And ComboBox2.SelectedValue IsNot Nothing Then getMEMBER_MAIN_TYPE(ComboBox1.SelectedValue.ToString, ComboBox2.SelectedValue.ToString)
    'End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.DropDownClosed
        If ComboBox1.SelectedValue IsNot Nothing And ComboBox2.SelectedValue IsNot Nothing And ComboBox3.SelectedValue IsNot Nothing Then getMEMBER_TYPE(ComboBox1.SelectedValue.ToString, ComboBox2.SelectedValue.ToString, ComboBox3.SelectedValue.ToString)
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.DropDownClosed
        If ComboBox1.SelectedValue IsNot Nothing And ComboBox2.SelectedValue IsNot Nothing And ComboBox3.SelectedValue IsNot Nothing And ComboBox4.SelectedValue IsNot Nothing Then
            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", ComboBox1.SelectedValue.ToString)
            parameters.Add("@p1", ComboBox2.SelectedValue.ToString)
            parameters.Add("@p2", ComboBox3.SelectedValue.ToString)
            parameters.Add("@p3", ComboBox4.SelectedValue.ToString)

            '==============================================
            Dim query1 As String = "SELECT * FROM MB_CONSIDERATION_COMPUTE_MASTER "
            'query &= "SELECT [MEMBER_TYPE_CODE], [MEMBER_TYPE_NAME], [INACTIVE] FROM [MB_MEMBER_TYPE] "
            query1 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) "
            'query1 &= "ORDER BY [MEMBER_TYPE_CODE] "

            Dim dt1 As DataTable = fillWebSQL(query1, parameters, "MB_CONSIDERATION_COMPUTE_MASTER").Copy
            'dt1.TableName = "MB_MEMBER_COMPUTE_MASTER"
            If ds.Tables.Contains("MB_CONSIDERATION_COMPUTE_MASTER") = True Then
                ds.Tables("MB_CONSIDERATION_COMPUTE_MASTER").Clear()
                ds.Tables("MB_CONSIDERATION_COMPUTE_MASTER").Merge(dt1)
                ds.Tables("MB_CONSIDERATION_COMPUTE_MASTER").AcceptChanges()
            Else
                ds.Tables.Add(dt1)
            End If
            '==============================================
            Dim query2 As String = "SELECT * FROM MB_CONSIDERATION_COMPUTE_DETAIL "
            'query &= "SELECT [MEMBER_TYPE_CODE], [MEMBER_TYPE_NAME], [INACTIVE] FROM [MB_MEMBER_TYPE] "
            query2 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) "
            query2 &= "ORDER BY [RATE_SEQ] "

            Dim dt2 As DataTable = fillWebSQL(query2, parameters, "MB_CONSIDERATION_COMPUTE_DETAIL").Copy
            'dt2.TableName = "MB_MEMBER_COMPUTE_DETAIL"
            If ds.Tables.Contains("MB_CONSIDERATION_COMPUTE_DETAIL") Then
                ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL").Clear()
                ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL").Merge(dt2)
                ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL").AcceptChanges()
            Else
                dt2.PrimaryKey = New DataColumn() {dt2.Columns("RATE_SEQ")}
                ds.Tables.Add(dt2)

                bs = New BindingSource(ds, "MB_CONSIDERATION_COMPUTE_DETAIL")
                DataGridView1.DataSource = bs

                For idx As Integer = 0 To DataGridView1.ColumnCount - 1
                    DataGridView1.Columns(idx).Visible = False
                Next

                DataGridView1.Columns("RATE_SEQ").Visible = True
                DataGridView1.Columns("QUERY").Visible = True
                DataGridView1.Columns("QUERY_VALUE").Visible = True
                'DataGridView1.Columns("FIRST_REGIST_VALUE").Visible = True
                DataGridView1.Columns("ACTIVE").Visible = True

                DataGridView1.Columns("RATE_SEQ").Width = 50
                DataGridView1.Columns("QUERY").Width = 300
                DataGridView1.Columns("QUERY_VALUE").Width = 100
                'DataGridView1.Columns("FIRST_REGIST_VALUE").Width = 100
                DataGridView1.Columns("ACTIVE").Width = 50

                'blinding
                NumericUpDown1.DataBindings.Add(New Binding("Value", bs, "RATE_SEQ", True, DataSourceUpdateMode.OnValidation))
                'NumericUpDown3.DataBindings.Add(New Binding("Value", bs, "FIRST_REGIST_VALUE", True, DataSourceUpdateMode.OnValidation))

                TextBox1.DataBindings.Add(New Binding("Text", bs, "QUERY", True, DataSourceUpdateMode.OnValidation))
                TextBox2.DataBindings.Add(New Binding("Text", bs, "QUERY_VALUE", True, DataSourceUpdateMode.OnValidation))
            End If
            '==============================================
        End If
    End Sub

    Private Sub gridControl1_CurrentRowChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim xrow As Xceed.Grid.DataRow = CType(RadGridView1.CurrentRow, Xceed.Grid.DataRow)

        'If DataGridView1.CurrentRow IsNot Nothing Then

        '    bs.Position = ds.Tables("MB_MEMBER_COMPUTE_DETAIL").Rows.IndexOf(ds.Tables("MB_MEMBER_COMPUTE_DETAIL").Rows.Find(DataGridView1.CurrentRow.Cells("RATE_SEQ").Value))

        '    'get info from prono
        '    'fillGuestLog(DataGridView1.CurrentRow.Cells("ID").Value.ToString)
        'End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", ComboBox1.SelectedValue.ToString)
            parameters.Add("@p1", ComboBox2.SelectedValue.ToString)
            parameters.Add("@p2", ComboBox3.SelectedValue.ToString)
            parameters.Add("@p3", ComboBox4.SelectedValue.ToString)

            bs.EndEdit()
            Dim query2 As String = "SELECT * FROM MB_CONSIDERATION_COMPUTE_DETAIL "
            query2 &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) "
            query2 &= "ORDER BY [RATE_SEQ] "

            If ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL").GetChanges IsNot Nothing Then
                Try
                    updateWebSQL(query2, parameters, ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL"))
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "updateWebSQL")
                End Try
            End If

            MessageBox.Show("Apply Successfully")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "MB_CONSIDERATION_COMPUTE_DETAIL.UPDATE Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'new
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", ComboBox1.SelectedValue.ToString)
        parameters.Add("@p1", ComboBox2.SelectedValue.ToString)
        parameters.Add("@p2", ComboBox3.SelectedValue.ToString)
        parameters.Add("@p3", ComboBox4.SelectedValue.ToString)
        'parameters.Add("@p4", 99)

        Dim query As String = "INSERT INTO MB_CONSIDERATION_COMPUTE_DETAIL (MEMBER_MAIN_GROUP_CODE, MEMBER_GROUP_CODE, MEMBER_MAIN_TYPE_CODE, MEMBER_TYPE_CODE,RATE_SEQ,ACTIVE) VALUES (@p0,@p1,@p2,@p3,99,1)"

        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        'fresh grid
        ComboBox4_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'del
        If DataGridView1.CurrentRow IsNot Nothing Then
            If MessageBox.Show("Are you sure to delete " & DataGridView1.CurrentRow.Cells("RATE_SEQ").Value.ToString & "?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim parameters As New Dictionary(Of String, Object)
                parameters.Add("@p0", ComboBox1.SelectedValue.ToString)
                parameters.Add("@p1", ComboBox2.SelectedValue.ToString)
                parameters.Add("@p2", ComboBox3.SelectedValue.ToString)
                parameters.Add("@p3", ComboBox4.SelectedValue.ToString)
                parameters.Add("@p4", DataGridView1.CurrentRow.Cells("RATE_SEQ").Value)

                Try
                    executeWebSQL("DELETE FROM MB_CONSIDERATION_COMPUTE_DETAIL WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) AND (RATE_SEQ = @p4)", parameters)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try

                'remove from grid
                ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL").Rows.Find(DataGridView1.CurrentRow.Cells("RATE_SEQ").Value).Delete()
                ds.Tables("MB_CONSIDERATION_COMPUTE_DETAIL").AcceptChanges()

                MessageBox.Show("Delete completed")
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ComboBox4_SelectedIndexChanged(sender, e)
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub
End Class