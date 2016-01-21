Imports System.Windows.Forms

Public Class frmMainExcelMatchColumns

    Friend EXCEL_PATH As String
    Friend COL_ADD As String
    Friend ds As DataSet

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmExcelMatchColumns_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'get sheet

        ds = getExcelDataV2(EXCEL_PATH)

        For Each dt As DataTable In ds.Tables
            ComboBox1.Items.Add(dt.TableName)
        Next

        If ComboBox1.Items.Count > 0 Then
            ComboBox1.SelectedIndex = 0
        End If

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        'Dim dt As DataTable = getExcelData(EXCEL_PATH, ComboBox1.SelectedValue.ToString)
        'DataGridView1.DataSource = dt


        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()
        DataGridView1.DataSource = ds.Tables(ComboBox1.Text)

        'DataGridView2.Rows.Clear()
        'Dim cols() As String = COL_ADD.Split(","c)
        'For i As Integer = 0 To cols.Length - 1
        '    Dim row As New DataGridViewRow
        '    row = DataGridView2.Rows(DataGridView2.Rows.Add())
        '    row.Cells("colName").Value = cols(i)
        '    'row.Cells("colValue").Value = ds.Tables("COLUMNS_TABLE").Rows(0).Item("COLUMN_NAME").ToString
        'Next

        'generate columns
        Dim dt As New DataTable("COLUMNS_TABLE")
        dt.Columns.Add("COLUMN_NAME", GetType(String))
        'dt.Columns.Add("ColumnNameTHAI", GetType(String))

        For i As Integer = 0 To ds.Tables(ComboBox1.Text).Columns.Count - 1
            Dim r As DataRow = dt.NewRow
            r.Item("COLUMN_NAME") = ds.Tables(ComboBox1.Text).Columns(i).ColumnName
            dt.Rows.Add(r)
        Next

        If ds.Tables.Contains("COLUMNS_TABLE") = True Then
            ds.Tables("COLUMNS_TABLE").Clear()
            ds.Tables("COLUMNS_TABLE").Merge(dt)
            ds.Tables("COLUMNS_TABLE").AcceptChanges()
        Else
            ds.Tables.Add(dt)
            ds.Tables("COLUMNS_TABLE").PrimaryKey = New DataColumn() {ds.Tables("COLUMNS_TABLE").Columns("COLUMN_NAME")}
        End If



        'Dim comboBoxColumn As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
        'comboBoxColumn.HeaderText = "COLUMN_NAME"
        'comboBoxColumn.DataPropertyName = "COLUMN_NAME"
        'comboBoxColumn.DataSource = ds.Tables("COLUMNS_TABLE")
        'comboBoxColumn.ValueMember = ds.Tables("COLUMNS_TABLE").Columns(0).ColumnName
        'comboBoxColumn.DisplayMember = ds.Tables("COLUMNS_TABLE").Columns(0).ColumnName

        'DataGridView2.Columns.RemoveAt(0)
        'DataGridView2.Columns.Insert(0, comboBoxColumn)

        Dim dgcb As DataGridViewComboBoxColumn = CType(DataGridView2.Columns(1), DataGridViewComboBoxColumn)
        'dgcb.Items.Clear()

        dgcb.DataSource = ds.Tables("COLUMNS_TABLE")
        dgcb.ValueMember = "COLUMN_NAME"
        dgcb.DisplayMember = "COLUMN_NAME"

        DataGridView2.Rows.Clear()

        Dim cols() As String = COL_ADD.Split(","c)
        For i As Integer = 0 To cols.Length - 1
            Dim row As New DataGridViewRow
            row = DataGridView2.Rows(DataGridView2.Rows.Add())
            row.Cells("colName").Value = cols(i)
            'row.Cells("colValue").Value = ds.Tables("COLUMNS_TABLE").Rows(0).Item("COLUMN_NAME").ToString
        Next
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub
End Class
