Imports System.IO

Public Class frmFTInew

    Dim ds As DataSet
    Dim MEMBER_SHORT_NAME As String
    Dim REGIST_CODE As String

    Private Sub frmFTInew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet
        Dim dtFils As New DataTable("FILES")
        dtFils.Columns.Add("DOC_TYPE", GetType(Integer))
        dtFils.Columns.Add("docName", GetType(String))
        dtFils.Columns.Add("docPath", GetType(String))
        ds.Tables.Add(dtFils)

        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_MEMBER_FILES_TYPE ORDER BY DOC_TYPE_NAME"

        Dim dt As DataTable = New DataTable
        Try
            dt = fillWebSQL(query, parameters, "MB_MEMBER_FILES_TYPE")
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
        End Try

        If ds.Tables.Contains("MB_MEMBER_FILES_TYPE") = True Then
            ds.Tables("MB_MEMBER_FILES_TYPE").Clear()
            ds.Tables("MB_MEMBER_FILES_TYPE").Merge(dt)
            ds.Tables("MB_MEMBER_FILES_TYPE").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        DataGridView6.DataSource = ds.Tables("FILES")

        Dim comboBoxColumn As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
        comboBoxColumn.HeaderText = "DOC_TYPE"
        comboBoxColumn.DataPropertyName = "DOC_TYPE"
        comboBoxColumn.DataSource = ds.Tables("MB_MEMBER_FILES_TYPE")
        comboBoxColumn.ValueMember = ds.Tables("MB_MEMBER_FILES_TYPE").Columns(0).ColumnName
        comboBoxColumn.DisplayMember = ds.Tables("MB_MEMBER_FILES_TYPE").Columns(1).ColumnName

        DataGridView6.Columns.RemoveAt(0)
        DataGridView6.Columns.Insert(0, comboBoxColumn)

        getMB_MEMBER_GROUP()
        'getMEMBER_MAIN_GROUP()
        getMEMBER_MAIN_TYPE("000", "000")

        'get regist number
        parameters.Clear()
        'Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", "FTI_RUNNING_REGIST")

        query = String.Empty
        query = "SELECT OBJ_VALUE FROM MB_PARAMETERS WHERE OBJ_NAME = @p0"

        Dim running As Integer = 0
        Try
            running = CInt(client.ExecuteScalar(query, parameters, user_session))
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
        End Try

        Dim d As DateTime = getSQLDate()
        REGIST_CODE = "F" & d.ToString("yyMM", ciTH) & running.ToString("00000")

        'update MB_PARAMETERS
        parameters.Clear()
        'parameters.Add("@p0", running + 1)
        'parameters.Add("@p1", "FTI_RUNNING_REGIST")

        query = String.Empty
        query = "UPDATE MB_PARAMETERS SET OBJ_VALUE = '" & running + 1 & "' WHERE OBJ_NAME = 'FTI_RUNNING_REGIST'"

        Dim result As Integer = 0
        Try
            result = executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        'MessageBox.Show("UPDATE MB_PARAMETERS:" & result & " " & running + 1 & vbCrLf & query)

        'MB_MEMBER
        parameters.Clear()
        parameters.Add("@p0", "001")
        parameters.Add("@p1", "131-02")
        parameters.Add("@p2", REGIST_CODE)
        parameters.Add("@p3", "100")
        parameters.Add("@p4", "000")
        parameters.Add("@p5", "")
        parameters.Add("@p6", "")
        parameters.Add("@p7", d)
        parameters.Add("@p8", user_name)
        parameters.Add("@p9", d)
        parameters.Add("@p10", "X")
        parameters.Add("@p11", REGIST_CODE)
        parameters.Add("@p12", "N")
        'parameters.Add("@p13", MEMBER_SHORT_NAME & (member_running + 1))
        parameters.Add("@p13", "")

        If CheckBox1.Checked = True Then
            parameters.Add("@p14", ComboBox5.SelectedValue)
        Else
            parameters.Add("@p14", "")
        End If

        query = String.Empty
        query = "INSERT INTO MB_MEMBER (OU_CODE, DIV_CODE, REGIST_CODE, MEMBER_MAIN_GROUP_CODE, MEMBER_GROUP_CODE, MEMBER_MAIN_TYPE_CODE, MEMBER_TYPE_CODE, REGIST_DATE, CR_BY, CR_DATE, MEMBER_STATUS_CODE, COMP_PERSON_CODE, STATUS_TYPE, MEMBER_CODE, ADVISOR_CODE) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14)"

        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        'MB_COMP_PERSON
        parameters.Clear()
        parameters.Add("@p0", "001")
        parameters.Add("@p1", REGIST_CODE)
        parameters.Add("@p2", "001")
        parameters.Add("@p3", TextBox5.Text)
        parameters.Add("@p4", TextBox4.Text)
        parameters.Add("@p5", user_name)
        parameters.Add("@p6", d)

        query = String.Empty
        query = "INSERT INTO MB_COMP_PERSON (OU_CODE, COMP_PERSON_CODE, COMP_PERSON_TYPE_CODE, COMP_PERSON_NAME_TH, COMP_PERSON_NAME_EN, CR_BY, CR_DATE) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6)"

        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        Label1.Text = REGIST_CODE

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMB_MEMBER_GROUP()
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", "200")
        'parameters.Add("@p1", "1")
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT * FROM MB_MEMBER_GROUP "
        query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) "
        query &= "ORDER BY MEMBER_GROUP_NAME "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_GROUP").Copy
        'dt.TableName = "MB_MEMBER_MAIN_GROUP"
        If ds.Tables.Contains("MB_MEMBER_GROUP") Then ds.Tables.Remove("MB_MEMBER_GROUP")
        ds.Tables.Add(dt)

        ComboBox5.DataSource = ds.Tables("MB_MEMBER_GROUP")
        ComboBox5.DisplayMember = "MEMBER_GROUP_NAME"
        ComboBox5.ValueMember = "MEMBER_GROUP_CODE"
    End Sub

    Private Sub getMEMBER_MAIN_GROUP()
        Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", "0")
        'parameters.Add("@p1", "1")
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT [MEMBER_MAIN_GROUP_CODE], [MEMBER_MAIN_GROUP_NAME], [INACTIVE] FROM [MB_MEMBER_MAIN_GROUP] "
        'query &= "WHERE ([INACTIVE] <> 'N') "
        query &= "ORDER BY MEMBER_MAIN_GROUP_CODE "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_MAIN_GROUP").Copy
        'dt.TableName = "MB_MEMBER_MAIN_GROUP"
        If ds.Tables.Contains("MB_MEMBER_MAIN_GROUP") Then ds.Tables.Remove("MB_MEMBER_MAIN_GROUP")
        ds.Tables.Add(dt)

        ComboBox1.DataSource = ds.Tables("MB_MEMBER_MAIN_GROUP")
        ComboBox1.DisplayMember = "MEMBER_MAIN_GROUP_NAME"
        ComboBox1.ValueMember = "MEMBER_MAIN_GROUP_CODE"
    End Sub

    Private Sub getMEMBER_GROUP(ByVal MEMBER_MAIN_GROUP_CODE As String)
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", MEMBER_MAIN_GROUP_CODE)
        'parameters.Add("@p1", "1")
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT [MEMBER_GROUP_CODE], [MEMBER_GROUP_NAME], [MEMBER_GROUP_NAME_EN], [INACTIVE] FROM [MB_MEMBER_GROUP] "
        query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) "
        query &= "ORDER BY [MEMBER_GROUP_CODE] "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_GROUP").Copy
        'dt.TableName = "MB_MEMBER_GROUP"
        If ds.Tables.Contains("MB_MEMBER_GROUP") Then ds.Tables.Remove("MB_MEMBER_GROUP")
        ds.Tables.Add(dt)

        ComboBox2.DataSource = ds.Tables("MB_MEMBER_GROUP")
        ComboBox2.DisplayMember = "MEMBER_GROUP_NAME"
        ComboBox2.ValueMember = "MEMBER_GROUP_CODE"
    End Sub

    Private Sub getMEMBER_MAIN_TYPE(ByVal MEMBER_MAIN_GROUP_CODE As String, ByVal MEMBER_GROUP_CODE As String)
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", MEMBER_MAIN_GROUP_CODE)
        parameters.Add("@p1", MEMBER_GROUP_CODE)
        'parameters.Add("@p2", "2")
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        query &= "SELECT [MEMBER_MAIN_TYPE_CODE], [MEMBER_MAIN_TYPE_NAME], [INACTIVE] FROM [MB_MEMBER_MAIN_TYPE] "
        query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) "
        query &= "ORDER BY MEMBER_MAIN_TYPE_CODE "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_MAIN_TYPE").Copy
        'dt.TableName = "MB_MEMBER_MAIN_TYPE"
        If ds.Tables.Contains("MB_MEMBER_MAIN_TYPE") Then ds.Tables.Remove("MB_MEMBER_MAIN_TYPE")
        ds.Tables.Add(dt)

        ComboBox7.DataSource = ds.Tables("MB_MEMBER_MAIN_TYPE")
        ComboBox7.DisplayMember = "MEMBER_MAIN_TYPE_NAME"
        ComboBox7.ValueMember = "MEMBER_MAIN_TYPE_CODE"
    End Sub

    Private Sub getMEMBER_TYPE(ByVal MEMBER_MAIN_GROUP_CODE As String, ByVal MEMBER_GROUP_CODE As String, ByVal MEMBER_MAIN_TYPE_CODE As String)
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", MEMBER_MAIN_GROUP_CODE)
        parameters.Add("@p1", MEMBER_GROUP_CODE)
        parameters.Add("@p2", MEMBER_MAIN_TYPE_CODE)
        'parameters.Add("@p3", "3")

        Dim query As String = String.Empty
        'query &= "SELECT [MEMBER_TYPE_CODE], [MEMBER_TYPE_NAME], [INACTIVE] FROM [MB_MEMBER_TYPE] "
        query &= "SELECT * FROM MB_MEMBER_TYPE "
        query &= "WHERE (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) "
        query &= "ORDER BY [MEMBER_TYPE_CODE] "

        Dim dt As DataTable = fillWebSQL(query, parameters, "MB_MEMBER_TYPE").Copy
        'dt.TableName = "MB_MEMBER_TYPE"
        If ds.Tables.Contains("MB_MEMBER_TYPE") Then ds.Tables.Remove("MB_MEMBER_TYPE")
        ds.Tables.Add(dt)

        ComboBox6.DataSource = ds.Tables("MB_MEMBER_TYPE")
        ComboBox6.DisplayMember = "MEMBER_TYPE_NAME"
        ComboBox6.ValueMember = "MEMBER_TYPE_CODE"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DataGridView6.EndEdit()
        Button1.Enabled = False

        'check black list

        'check tax id that may duplicate and alert

        'save on aproval list

        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = String.Empty
        Dim member_running As Integer = 0

        'get member code
        parameters.Clear()
        parameters.Add("@p0", ComboBox1.SelectedValue)
        parameters.Add("@p1", ComboBox2.SelectedValue)
        parameters.Add("@p2", ComboBox3.SelectedValue)
        parameters.Add("@p3", ComboBox4.SelectedValue)

        query = "SELECT RUNNING FROM MB_MEMBER_TYPE WHERE MEMBER_GROUP_CODE = @p0 AND MEMBER_MAIN_TYPE_CODE = @p1 AND MEMBER_TYPE_CODE = @p2 AND MEMBER_TYPE_NAME = @p3"
        Try
            member_running = CInt(client.ExecuteScalar(query, parameters, user_session))
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
        End Try

        'update running
        parameters.Clear()
        parameters.Add("@p", member_running + 1)
        parameters.Add("@p0", ComboBox1.SelectedValue)
        parameters.Add("@p1", ComboBox2.SelectedValue)
        parameters.Add("@p2", ComboBox3.SelectedValue)
        parameters.Add("@p3", ComboBox4.SelectedValue)

        query = String.Empty
        query = "UPDATE MB_MEMBER_TYPE SET RUNNING = @p WHERE MEMBER_GROUP_CODE = @p0 AND MEMBER_MAIN_TYPE_CODE = @p1 AND MEMBER_TYPE_CODE = @p2 AND MEMBER_TYPE_NAME = @p3"

        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        'MB_COMP_PERSON_TAXID
        parameters.Clear()
        parameters.Add("@p0", "001")
        parameters.Add("@p1", REGIST_CODE)
        parameters.Add("@p2", TextBox3.Text)

        query = String.Empty
        query = "INSERT INTO MB_COMP_PERSON_TAXID (OU_CODE, COMP_PERSON_CODE, TAX_ID) VALUES (@p0,@p1,@p2)"

        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        'insert files
        If DataGridView6.Rows.Count > 0 Then
            parameters.Clear()
            parameters.Add("@p0", "FTI")
            parameters.Add("@p1", REGIST_CODE)
            parameters.Add("@p2", DBNull.Value)
            parameters.Add("@p3", DBNull.Value)
            parameters.Add("@p4", DBNull.Value)
            parameters.Add("@p5", DBNull.Value)

            query = "INSERT INTO MB_MEMBER_FILES (CATEGORY, REGIST_CODE, DOC_TYPE, DOC_NAME, FILE_NAME, FILE_DATA) VALUES (@p0,@p1,@p2,@p3,@p4,@p5)"
            For i As Integer = 0 To ds.Tables("FILES").Rows.Count - 1
                'read file
                Dim fsBLOBFile As New System.IO.FileStream(OpenFileDialog1.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)
                Dim bytBLOBData(Convert.ToInt32(fsBLOBFile.Length()) - 1) As Byte
                fsBLOBFile.Read(bytBLOBData, 0, bytBLOBData.Length)
                fsBLOBFile.Close()

                'generate parameter
                Dim param As New SqlClient.SqlParameter("@p5", SqlDbType.VarBinary, bytBLOBData.Length, ParameterDirection.Input, True, 0, 0, "FILE_DATA", DataRowVersion.Current, bytBLOBData)

                parameters("@p2") = ds.Tables("FILES").Rows(i).Item("DOC_TYPE")
                parameters("@p3") = ds.Tables("FILES").Rows(i).Item("docName")
                parameters("@p4") = ds.Tables("FILES").Rows(i).Item("docName")
                parameters("@p5") = param.Value

                Try
                    executeWebSQL(query, parameters)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
                End Try
            Next
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
        'INSERT INTO MB_MEMBER_FILES                          (CATEGORY, REGIST_CODE, DOC_TYPE, DOC_NAME, FILE_NAME, FILE_DATA) VALUES        (,,,,,)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.DropDownClosed
        'If ComboBox1.SelectedValue IsNot Nothing Then getMEMBER_GROUP(ComboBox1.SelectedValue.ToString)
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.DropDownClosed
        'If ComboBox1.SelectedValue IsNot Nothing And ComboBox2.SelectedValue IsNot Nothing Then getMEMBER_MAIN_TYPE(ComboBox1.SelectedValue.ToString, ComboBox2.SelectedValue.ToString)
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.DropDownClosed
        'If ComboBox1.SelectedValue IsNot Nothing And ComboBox2.SelectedValue IsNot Nothing And ComboBox3.SelectedValue IsNot Nothing Then getMEMBER_TYPE(ComboBox1.SelectedValue.ToString, ComboBox2.SelectedValue.ToString, ComboBox3.SelectedValue.ToString)
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.DropDownClosed
        'If ComboBox1.SelectedValue IsNot Nothing And ComboBox2.SelectedValue IsNot Nothing And ComboBox3.SelectedValue IsNot Nothing And ComboBox4.SelectedValue IsNot Nothing Then
        '    MEMBER_SHORT_NAME = ds.Tables("MB_MEMBER_TYPE").Rows(ComboBox4.SelectedIndex).Item("MEMBER_SHORT_NAME").ToString
        'End If

    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox7.DropDownClosed
        getMEMBER_TYPE("000", "000", ComboBox7.SelectedValue.ToString)
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If OpenFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            'chcek file size
            Dim information As FileInfo = My.Computer.FileSystem.GetFileInfo(OpenFileDialog1.FileName)
            Dim fileSizeMB As Double = (information.Length) / 1024 / 1024  'Value in MB
            Dim docSizeMB As Integer = 0

            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", "FTI_DOC_SIZE")

            Dim query As String = String.Empty
            query &= "SELECT OBJ_VALUE FROM MB_PARAMETERS WHERE OBJ_NAME = @p0"

            Try
                docSizeMB = CInt(client.ExecuteScalar(query, parameters, user_session))
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
            End Try

            If docSizeMB >= fileSizeMB Then
                'add to grid
                Dim r As DataRow = ds.Tables("FILES").NewRow
                r("DOC_TYPE") = DBNull.Value
                r("docName") = System.IO.Path.GetFileName(OpenFileDialog1.FileName)
                r("docPath") = OpenFileDialog1.FileName
                ds.Tables("FILES").Rows.Add(r)
            Else
                MessageBox.Show(Path.GetFileName(OpenFileDialog1.FileName) & " ขนาดเกินกำหนด (" & docSizeMB & "MB). กรุณาเลือกไฟล์อื่น.")
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