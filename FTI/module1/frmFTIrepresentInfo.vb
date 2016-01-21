Imports Microsoft.Reporting.WinForms

Public Class frmFTIrepresentInfo

    Dim ds As DataSet
    Dim dv As DataView

    Private Sub frmFTIapproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet

        Dim parameters As New Dictionary(Of String, Object)

        Dim query As String = String.Empty
        query &= "SELECT        MB_CONTACT.ADDR_POSTCODE, CASE WHEN MB_CONTACT.ADDR_POSTCODE IS NULL THEN '' ELSE MB_CONTACT.ADDR_POSTCODE END + ' (' + CONVERT(varchar(10), COUNT(*)) + ')' AS CNT "
        query &= "FROM            MB_MEMBER_REPRESENT INNER JOIN "
        query &= "                         MB_MEMBER ON MB_MEMBER_REPRESENT.REGIST_CODE = MB_MEMBER.REGIST_CODE INNER JOIN "
        query &= "                         MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
        query &= "                         MB_COMP_PERSON_ADDRESS ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON_ADDRESS.COMP_PERSON_CODE INNER JOIN "
        query &= "                         MB_CONTACT ON MB_MEMBER_REPRESENT.CONTACT_CODE = MB_CONTACT.CONTACT_CODE LEFT OUTER JOIN "
        query &= "                         MB_POSITION ON MB_MEMBER_REPRESENT.POSITION_CODE = MB_POSITION.POSITION_CODE "
        query &= "WHERE        (MB_COMP_PERSON_ADDRESS.ADDR_CODE = '001') AND (MB_MEMBER.MEMBER_STATUS_CODE = 'A') AND (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN ('000', '999')) "
        query &= "GROUP BY MB_CONTACT.ADDR_POSTCODE "
        query &= "ORDER BY MB_CONTACT.ADDR_POSTCODE"

        Dim dt As DataTable = New DataTable
        Try
            dt = fillWebSQL(query, parameters, "ADDR_POSTCODE1")
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
        End Try

        If ds.Tables.Contains("ADDR_POSTCODE1") = True Then
            ds.Tables("ADDR_POSTCODE1").Clear()
            ds.Tables("ADDR_POSTCODE1").Merge(dt)
            ds.Tables("ADDR_POSTCODE1").AcceptChanges()
        Else
            ds.Tables.Add(dt)
        End If

        ComboBox1.DataSource = ds.Tables("ADDR_POSTCODE1")
        ComboBox1.DisplayMember = "CNT"
        ComboBox1.ValueMember = "ADDR_POSTCODE"

        ComboBox1.SelectedIndex = 0

        '========================================

        Dim dt2 As DataTable = dt.Copy
        dt2.TableName = "ADDR_POSTCODE2"

        ds.Tables.Add(dt2)

        ComboBox2.DataSource = ds.Tables("ADDR_POSTCODE2")
        ComboBox2.DisplayMember = "CNT"
        ComboBox2.ValueMember = "ADDR_POSTCODE"

        ComboBox2.SelectedIndex = ComboBox2.Items.Count - 1

        '========================================
        ' POPULATE THE COMBO BOX.
        For Each sPrinters In System.Drawing.Printing.PrinterSettings.InstalledPrinters
            'cmbPrinterList.Items.Add(sPrinters)
            ComboBox3.Items.Add(sPrinters)
        Next
        If ComboBox3.Items.Count > 0 Then ComboBox3.SelectedIndex = 0
        '========================================

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMB_MEMBER_RETIRE(ByVal SEARCH_TEXT As String, ByVal POSTCODE_FROM As String, ByVal POSTCODE_TO As String)
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        SEARCH_TEXT = "%" & SEARCH_TEXT.Replace(" ", "%") & "%"

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", SEARCH_TEXT)
        parameters.Add("@p1", POSTCODE_FROM)
        parameters.Add("@p2", POSTCODE_TO)
        'parameters.Add("@p2", "200")
        'parameters.Add("@p3", "999")
        'parameters.Add("@p1", "000")

        Dim query As String = String.Empty
        query &= "SELECT        MB_MEMBER_REPRESENT.CONTACT_CODE, dbo.get5_3_25_2(MB_MEMBER_REPRESENT.REGIST_CODE, MB_MEMBER_REPRESENT.REPRESENT_SEQ, 'TH') AS CONTACT_NAME_TH, "
        query &= "                         dbo.get5_3_25_2(MB_MEMBER_REPRESENT.REGIST_CODE, MB_MEMBER_REPRESENT.REPRESENT_SEQ, 'EN') AS CONTACT_NAME_EN, MB_CONTACT.ADDR_POSTCODE "
        query &= "FROM            MB_MEMBER_REPRESENT INNER JOIN "
        query &= "                         MB_MEMBER ON MB_MEMBER_REPRESENT.REGIST_CODE = MB_MEMBER.REGIST_CODE INNER JOIN "
        query &= "                         MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
        query &= "                         MB_COMP_PERSON_ADDRESS ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON_ADDRESS.COMP_PERSON_CODE INNER JOIN "
        query &= "                         MB_CONTACT ON MB_MEMBER_REPRESENT.CONTACT_CODE = MB_CONTACT.CONTACT_CODE LEFT OUTER JOIN "
        query &= "                         MB_POSITION ON MB_MEMBER_REPRESENT.POSITION_CODE = MB_POSITION.POSITION_CODE "
        query &= "WHERE        (MB_COMP_PERSON_ADDRESS.ADDR_CODE = '001') AND (MB_MEMBER.MEMBER_STATUS_CODE = 'A')  AND (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN ('000', '999')) AND ((MB_MEMBER_REPRESENT.CONTACT_CODE LIKE @p0) OR (dbo.get5_3_25_2(MB_MEMBER_REPRESENT.REGIST_CODE, MB_MEMBER_REPRESENT.REPRESENT_SEQ, 'TH') LIKE @p0) OR (dbo.get5_3_25_2(MB_MEMBER_REPRESENT.REGIST_CODE, MB_MEMBER_REPRESENT.REPRESENT_SEQ, 'EN') LIKE @p0)) AND (MB_CONTACT.ADDR_POSTCODE BETWEEN @p1 AND @p2) "
        query &= "GROUP BY MB_MEMBER_REPRESENT.CONTACT_CODE, dbo.get5_3_25_2(MB_MEMBER_REPRESENT.REGIST_CODE, MB_MEMBER_REPRESENT.REPRESENT_SEQ, 'TH'), "
        query &= "                         dbo.get5_3_25_2(MB_MEMBER_REPRESENT.REGIST_CODE, MB_MEMBER_REPRESENT.REPRESENT_SEQ, 'EN'), MB_CONTACT.ADDR_POSTCODE "
        query &= "ORDER BY CONTACT_NAME_TH, CONTACT_NAME_EN"

        Dim dt As DataTable = New DataTable
        Try
            dt = fillWebSQL(query, parameters, "MB_MEMBER")
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
        End Try

        If ds.Tables.Contains("MB_MEMBER") = True Then
            ds.Tables("MB_MEMBER").Clear()
            ds.Tables("MB_MEMBER").Merge(dt)
            ds.Tables("MB_MEMBER").AcceptChanges()
        Else
            ds.Tables.Add(dt)

            dv = New DataView(ds.Tables("MB_MEMBER"))

            ds.Tables("MB_MEMBER").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER").Columns("CONTACT_CODE")}

            DataGridView1.DataSource = dv

            'For i As Integer = 0 To DataGridView1.ColumnCount - 1
            '    DataGridView1.Columns(i).Visible = False
            'Next

            'DataGridView1.Columns("MEMBER_CODE").Visible = True
            'DataGridView1.Columns("PRENAME_TH").Visible = True
            'DataGridView1.Columns("COMP_PERSON_NAME_TH").Visible = True
            'DataGridView1.Columns("ADDR_POSTCODE").Visible = True
            'DataGridView1.Columns("REGIST_CODE").Visible = True
            ''DataGridView1.Columns("RETIRE_REASON").Visible = True
            ''DataGridView1.Columns("MEMBER_STATUS_CODE").Visible = True

            'DataGridView1.Columns("COMP_PERSON_NAME_TH").Width = 200
        End If

        Label3.Text = String.Format("พบ {0} รายการ", ds.Tables("MB_MEMBER").Rows.Count.ToString("#,##0"))
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btPrintPreview.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim parameters As New Dictionary(Of String, Object)
            Dim query As String = String.Empty
            
            query &= "SELECT        MB_MEMBER_REPRESENT.CONTACT_CODE, dbo.get5_3_25_2(MB_MEMBER_REPRESENT.REGIST_CODE, MB_MEMBER_REPRESENT.REPRESENT_SEQ, 'TH') AS CONTACT_NAME_TH, "
            query &= "                         dbo.get5_3_25_2(MB_MEMBER_REPRESENT.REGIST_CODE, MB_MEMBER_REPRESENT.REPRESENT_SEQ, 'EN') AS CONTACT_NAME_EN, MB_MEMBER_REPRESENT.REPRESENT_SEQ, "
            query &= "                         MB_POSITION.POSITION_NAME_TH, MB_COMP_PERSON.COMP_PERSON_NAME_TH, MB_MEMBER.MEMBER_CODE, MB_PRENAME.PRENAME_TH "

            query &= "FROM            MB_MEMBER_REPRESENT INNER JOIN "
            query &= "                         MB_MEMBER ON MB_MEMBER_REPRESENT.REGIST_CODE = MB_MEMBER.REGIST_CODE INNER JOIN "
            query &= "                         MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
            query &= "                         MB_COMP_PERSON_ADDRESS ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON_ADDRESS.COMP_PERSON_CODE INNER JOIN "
            query &= "                         MB_CONTACT ON MB_MEMBER_REPRESENT.CONTACT_CODE = MB_CONTACT.CONTACT_CODE INNER JOIN "
            query &= "                         MB_PRENAME ON MB_COMP_PERSON.PREN_CODE = MB_PRENAME.PRENAME_CODE LEFT OUTER JOIN "
            query &= "                         MB_POSITION ON MB_MEMBER_REPRESENT.POSITION_CODE = MB_POSITION.POSITION_CODE "

            'query &= "FROM            MB_MEMBER_REPRESENT INNER JOIN "
            'query &= "                         MB_MEMBER ON MB_MEMBER_REPRESENT.REGIST_CODE = MB_MEMBER.REGIST_CODE INNER JOIN "
            'query &= "                         MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
            'query &= "                         MB_COMP_PERSON_ADDRESS ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON_ADDRESS.COMP_PERSON_CODE INNER JOIN "
            'query &= "                         MB_CONTACT ON MB_MEMBER_REPRESENT.CONTACT_CODE = MB_CONTACT.CONTACT_CODE LEFT OUTER JOIN "
            'query &= "                         MB_POSITION ON MB_MEMBER_REPRESENT.POSITION_CODE = MB_POSITION.POSITION_CODE "
            query &= "WHERE        (MB_COMP_PERSON_ADDRESS.ADDR_CODE = '001') AND (MB_MEMBER.MEMBER_STATUS_CODE = 'A')  AND (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN ('000', '999')) AND (MB_MEMBER_REPRESENT.CONTACT_CODE = @p0) AND (MB_PRENAME.PRENAME_TYPE = 2) "

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim i As Integer = 0

            parameters.Clear()
            parameters.Add("@p0", DataGridView1.SelectedRows(i).Cells("CONTACT_CODE").Value)

            Dim dt As DataTable = New DataTable
            Try
                dt = fillWebSQL(query, parameters, "MB_MEMBER")
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
            End Try

            Dim f As New frmMainReportViewer
            f.ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "/Reports/module1/rdlc5.3.25.09.FTIrepresentInfo.rdlc"

            'Create a report parameter for the sales order number 
            Dim CONTACT_CODE As New ReportParameter()
            CONTACT_CODE.Name = "CONTACT_CODE"
            CONTACT_CODE.Values.Add(DataGridView1.SelectedRows(i).Cells("CONTACT_CODE").Value.ToString)

            Dim CONTACT_NAME_TH As New ReportParameter()
            CONTACT_NAME_TH.Name = "CONTACT_NAME_TH"
            CONTACT_NAME_TH.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("CONTACT_NAME_TH").Value), "", DataGridView1.SelectedRows(i).Cells("CONTACT_NAME_TH").Value.ToString))

            Dim CONTACT_NAME_EN As New ReportParameter()
            CONTACT_NAME_EN.Name = "CONTACT_NAME_EN"
            CONTACT_NAME_EN.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("CONTACT_NAME_EN").Value), "", DataGridView1.SelectedRows(i).Cells("CONTACT_NAME_EN").Value.ToString))

            Dim DOC_DATE As New ReportParameter()
            DOC_DATE.Name = "DOC_DATE"
            DOC_DATE.Values.Add(getSQLDate.ToString("dd/MM/yyyy", ciTH))

            'get contact photo
            Dim CONTACT_PHOTO As New ReportParameter()
            CONTACT_PHOTO.Name = "CONTACT_PHOTO"

            query = String.Empty
            query &= "SELECT        PICTURE_DATA "
            query &= "FROM            MB_CONTACT_PICTURE "
            query &= "WHERE        (CONTACT_CODE = @p0)"

            Dim PICTURE_DATA As Object = Nothing
            Try
                PICTURE_DATA = client.ExecuteScalar(query, parameters, user_session)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar PICTURE_DATA")
            End Try

            If PICTURE_DATA IsNot Nothing Then
                If PICTURE_DATA IsNot DBNull.Value Then
                    Dim bytBLOBData() As Byte = DirectCast(PICTURE_DATA, Byte())
                    Dim stmBLOBData As New System.IO.MemoryStream(bytBLOBData)
                    'PictureBox1.Image = Image.FromStream(stmBLOBData)
                    CONTACT_PHOTO.Values.Add(ImageToBase64(Image.FromStream(stmBLOBData), System.Drawing.Imaging.ImageFormat.Bmp))
                Else
                    CONTACT_PHOTO.Values.Add("NO")
                End If

                'Dim bytBLOBData() As Byte = DirectCast(PICTURE_DATA, Byte())
                'Dim stmBLOBData As New System.IO.MemoryStream(bytBLOBData)
                'PictureBox1.Image = Image.FromStream(stmBLOBData)
                'CONTACT_PHOTO.Values.Add(If(IsDBNull(PICTURE_DATA), "NO", ImageToBase64(PICTURE_DATA)))
            Else
                CONTACT_PHOTO.Values.Add("NO")
            End If

            'Set the report parameters for the report
            Dim params() As ReportParameter = {CONTACT_CODE, CONTACT_NAME_TH, CONTACT_NAME_EN, DOC_DATE, CONTACT_PHOTO}
            'Dim params() As ReportParameter = {MEMBER_CODE, ADDR_NO, ADDR_MOO, ADDR_SOI, ADDR_ROAD, ADDR_SUB_DISTRICT, ADDR_DISTRICT, ADDR_PROVINCE_NAME, ADDR_POSTCODE, PRENAME_TH, COMP_PERSON_NAME_TH, REGIST_CAPITAL, EMPLOYEE_AMOUNT, ASSET1, ASSET2, ASSET3, ASSET4}
            f.ReportViewer1.LocalReport.SetParameters(params)

            f.ReportViewer1.LocalReport.DataSources.Clear()
            f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("ds", dt))

            f.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            f.ReportViewer1.ZoomMode = ZoomMode.Percent
            f.ReportViewer1.ZoomPercent = 100
            f.ReportViewer1.LocalReport.Refresh()

            f.WindowState = FormWindowState.Maximized
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                '
            End If
            f.Dispose()
            f = Nothing
        End If


    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    Private Sub btFind_Click(sender As Object, e As EventArgs) Handles btFind.Click
        getMB_MEMBER_RETIRE(TextBox1.Text, ComboBox1.SelectedValue.ToString, ComboBox2.SelectedValue.ToString)
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If DataGridView1.RowCount > 0 Then
            For i As Integer = 0 To DataGridView1.RowCount - 1
                DataGridView1.Rows(i).Selected = True
            Next
        End If
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        If DataGridView1.RowCount > 0 Then
            For i As Integer = 0 To DataGridView1.RowCount - 1
                DataGridView1.Rows(i).Selected = False
            Next
        End If
    End Sub

    Private Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim parameters As New Dictionary(Of String, Object)
            Dim query As String = String.Empty

            query &= "SELECT        MB_MEMBER_REPRESENT.CONTACT_CODE, dbo.get5_3_25_2(MB_MEMBER_REPRESENT.REGIST_CODE, MB_MEMBER_REPRESENT.REPRESENT_SEQ, 'TH') AS CONTACT_NAME_TH, "
            query &= "                         dbo.get5_3_25_2(MB_MEMBER_REPRESENT.REGIST_CODE, MB_MEMBER_REPRESENT.REPRESENT_SEQ, 'EN') AS CONTACT_NAME_EN, MB_MEMBER_REPRESENT.REPRESENT_SEQ, "
            query &= "                         MB_POSITION.POSITION_NAME_TH, MB_COMP_PERSON.COMP_PERSON_NAME_TH, MB_MEMBER.MEMBER_CODE, MB_PRENAME.PRENAME_TH "

            query &= "FROM            MB_MEMBER_REPRESENT INNER JOIN "
            query &= "                         MB_MEMBER ON MB_MEMBER_REPRESENT.REGIST_CODE = MB_MEMBER.REGIST_CODE INNER JOIN "
            query &= "                         MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
            query &= "                         MB_COMP_PERSON_ADDRESS ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON_ADDRESS.COMP_PERSON_CODE INNER JOIN "
            query &= "                         MB_CONTACT ON MB_MEMBER_REPRESENT.CONTACT_CODE = MB_CONTACT.CONTACT_CODE INNER JOIN "
            query &= "                         MB_PRENAME ON MB_COMP_PERSON.PREN_CODE = MB_PRENAME.PRENAME_CODE LEFT OUTER JOIN "
            query &= "                         MB_POSITION ON MB_MEMBER_REPRESENT.POSITION_CODE = MB_POSITION.POSITION_CODE "

            'query &= "FROM            MB_MEMBER_REPRESENT INNER JOIN "
            'query &= "                         MB_MEMBER ON MB_MEMBER_REPRESENT.REGIST_CODE = MB_MEMBER.REGIST_CODE INNER JOIN "
            'query &= "                         MB_COMP_PERSON ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
            'query &= "                         MB_COMP_PERSON_ADDRESS ON MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON_ADDRESS.COMP_PERSON_CODE INNER JOIN "
            'query &= "                         MB_CONTACT ON MB_MEMBER_REPRESENT.CONTACT_CODE = MB_CONTACT.CONTACT_CODE LEFT OUTER JOIN "
            'query &= "                         MB_POSITION ON MB_MEMBER_REPRESENT.POSITION_CODE = MB_POSITION.POSITION_CODE "
            query &= "WHERE        (MB_COMP_PERSON_ADDRESS.ADDR_CODE = '001') AND (MB_MEMBER.MEMBER_STATUS_CODE = 'A')  AND (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN ('000', '999')) AND (MB_MEMBER_REPRESENT.CONTACT_CODE = @p0) AND (MB_PRENAME.PRENAME_TYPE = 2) "

            Dim sFTI_RPT_DEVICEINFO As String = getParameters(1, "FTI_RPT_5.3.25.09_DEVICEINFO")

            For i As Integer = 0 To DataGridView1.SelectedRows.Count - 1
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

                parameters.Clear()
                parameters.Add("@p0", DataGridView1.SelectedRows(i).Cells("CONTACT_CODE").Value)

                Dim dt As DataTable = New DataTable
                Try
                    dt = fillWebSQL(query, parameters, "MB_MEMBER")
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=fillWebSQL")
                End Try

                Dim report As New LocalReport()
                report.ReportPath = System.Environment.CurrentDirectory & "/Reports/module1/rdlc5.3.25.09.FTIrepresentInfo.rdlc"

                'Create a report parameter for the sales order number 
                Dim CONTACT_CODE As New ReportParameter()
                CONTACT_CODE.Name = "CONTACT_CODE"
                CONTACT_CODE.Values.Add(DataGridView1.SelectedRows(i).Cells("CONTACT_CODE").Value.ToString)

                Dim CONTACT_NAME_TH As New ReportParameter()
                CONTACT_NAME_TH.Name = "CONTACT_NAME_TH"
                CONTACT_NAME_TH.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("CONTACT_NAME_TH").Value), "", DataGridView1.SelectedRows(i).Cells("CONTACT_NAME_TH").Value.ToString))

                Dim CONTACT_NAME_EN As New ReportParameter()
                CONTACT_NAME_EN.Name = "CONTACT_NAME_EN"
                CONTACT_NAME_EN.Values.Add(If(IsDBNull(DataGridView1.SelectedRows(i).Cells("CONTACT_NAME_EN").Value), "", DataGridView1.SelectedRows(i).Cells("CONTACT_NAME_EN").Value.ToString))

                Dim DOC_DATE As New ReportParameter()
                DOC_DATE.Name = "DOC_DATE"
                DOC_DATE.Values.Add(getSQLDate.ToString("dd/MM/yyyy", ciTH))

                'get contact photo
                Dim CONTACT_PHOTO As New ReportParameter()
                CONTACT_PHOTO.Name = "CONTACT_PHOTO"

                query = String.Empty
                query &= "SELECT        PICTURE_DATA "
                query &= "FROM            MB_CONTACT_PICTURE "
                query &= "WHERE        (CONTACT_CODE = @p0)"

                Dim PICTURE_DATA As Object = Nothing
                Try
                    PICTURE_DATA = client.ExecuteScalar(query, parameters, user_session)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar PICTURE_DATA")
                End Try

                If PICTURE_DATA IsNot Nothing Then
                    If PICTURE_DATA IsNot DBNull.Value Then
                        Dim bytBLOBData() As Byte = DirectCast(PICTURE_DATA, Byte())
                        Dim stmBLOBData As New System.IO.MemoryStream(bytBLOBData)
                        'PictureBox1.Image = Image.FromStream(stmBLOBData)
                        CONTACT_PHOTO.Values.Add(ImageToBase64(Image.FromStream(stmBLOBData), System.Drawing.Imaging.ImageFormat.Bmp))
                    Else
                        CONTACT_PHOTO.Values.Add("NO")
                    End If

                    'Dim bytBLOBData() As Byte = DirectCast(PICTURE_DATA, Byte())
                    'Dim stmBLOBData As New System.IO.MemoryStream(bytBLOBData)
                    'PictureBox1.Image = Image.FromStream(stmBLOBData)
                    'CONTACT_PHOTO.Values.Add(If(IsDBNull(PICTURE_DATA), "NO", ImageToBase64(PICTURE_DATA)))
                Else
                    CONTACT_PHOTO.Values.Add("NO")
                End If

                'Set the report parameters for the report
                Dim params() As ReportParameter = {CONTACT_CODE, CONTACT_NAME_TH, CONTACT_NAME_EN, DOC_DATE, CONTACT_PHOTO}

                report.SetParameters(params)
                report.DataSources.Add(New ReportDataSource("ds", dt))
                RDLCExport(report, sFTI_RPT_DEVICEINFO)
                RDLCPrint(ComboBox3.Text)
            Next
        End If
    End Sub
End Class