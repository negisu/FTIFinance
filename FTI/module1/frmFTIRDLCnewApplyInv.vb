Imports Microsoft.Reporting.WinForms

Public Class frmFTIRDLCnewApplyInv

    Dim ds As DataSet

    Private Sub frmFTIapproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", "000")
        parameters.Add("@p1", "100")
        parameters.Add("@p2", "200")
        parameters.Add("@p3", "999")
        'parameters.Add("@p1", "000")

        Dim query As String = String.Empty
        query &= "SELECT    MB_MEMBER.*, MB_COMP_PERSON.*, MB_MEMBER_MAIN_GROUP.*, MB_MEMBER_GROUP.* "
        query &= "FROM            MB_MEMBER INNER JOIN "
        query &= "                         MB_COMP_PERSON ON MB_MEMBER.OU_CODE = MB_COMP_PERSON.OU_CODE AND MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
        query &= "                         MB_MEMBER_MAIN_GROUP ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE INNER JOIN "
        query &= "                         MB_MEMBER_GROUP ON MB_MEMBER.MEMBER_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_GROUP_CODE AND "
        query &= "                         MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_MAIN_GROUP_CODE "
        query &= "WHERE        (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN (@p0, @p1, @p2, @p3)) AND (MB_MEMBER.MEMBER_STATUS_CODE = 'X') "
        'query &= "ORDER BY REGIST_DATE DESC "

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
        End If

        DataGridView1.DataSource = ds.Tables("MB_MEMBER")

        'ds.Tables("MB_MEMBER").PrimaryKey = New DataColumn() {ds.Tables("MB_MEMBER").Columns("REGIST_CODE")}

        For i As Integer = 0 To DataGridView1.ColumnCount - 1
            DataGridView1.Columns(i).Visible = False
        Next
        'MEMBER_MAIN_GROUP_CODE, MEMBER_GROUP_CODE, MEMBER_MAIN_TYPE_CODE, MEMBER_TYPE_CODE
        DataGridView1.Columns("MEMBER_MAIN_GROUP_CODE").Visible = True
        DataGridView1.Columns("MEMBER_GROUP_CODE").Visible = True
        DataGridView1.Columns("MEMBER_MAIN_TYPE_CODE").Visible = True
        DataGridView1.Columns("MEMBER_TYPE_CODE").Visible = True ' MEMBER_CODE
        DataGridView1.Columns("MEMBER_CODE").Visible = True
        DataGridView1.Columns("COMP_PERSON_NAME_TH").Visible = True
        DataGridView1.Columns("COMP_PERSON_NAME_EN").Visible = True

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        '
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            Dim f As New frmMainReportViewer

            f.ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "/Reports/module1/rdlcFTInewApplyInv.rdlc"

            f.ReportViewer1.LocalReport.DataSources.Clear()
            'f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("dsFTInewApply", ds.Tables("MB_MEMBER")))
            'f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("CourseInfo", Report1.Tables("CourseInfo")))
            'f.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("rptParam", Report1.Tables("rptParam")))

            'ReportViewer1.PrinterSettings.PrinterName = DstTime1.config.Rows(ConfigIndx).Item("PrintReportHalfLetter").ToString
            'ReportViewer1.PrinterSettings.DefaultPageSettings.PaperSize = GetSelectedPaperSize(DstTime1.config.Rows(ConfigIndx).Item("PrintReportHalfLetter").ToString, DstTime1.config.Rows(ConfigIndx).Item("PrintReportHalfLetterSize").ToString)
            'ReportViewer1.PrinterSettings.DefaultPageSettings.PaperSource = GetSelectedPaperSource(DstTime1.config.Rows(ConfigIndx).Item("PrintReportHalfLetter").ToString, DstTime1.config.Rows(ConfigIndx).Item("PrintReportHalfLetterSource").ToString)
            'ReportViewer1.PrinterSettings.DefaultPageSettings.Landscape = True

            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", DataGridView1.CurrentRow.Cells("MEMBER_MAIN_GROUP_CODE").Value)
            parameters.Add("@p1", DataGridView1.CurrentRow.Cells("MEMBER_GROUP_CODE").Value)
            parameters.Add("@p2", DataGridView1.CurrentRow.Cells("MEMBER_MAIN_TYPE_CODE").Value)
            parameters.Add("@p3", DataGridView1.CurrentRow.Cells("MEMBER_TYPE_CODE").Value)

            Dim CALCULATE_FIELD As String = "INCOME_PER_YEAR"

            Dim query As String = String.Empty
            query &= "SELECT CALCULATE_FIELD "
            query &= "FROM            MB_MEMBER_RATE_MASTER "
            query &= "WHERE        (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3)"

            Dim objCALCULATE_FIELD As Object = Nothing
            Try
                objCALCULATE_FIELD = client.ExecuteScalar(query, parameters, user_session)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar CALCULATE_FIELD")
            End Try

            If objCALCULATE_FIELD IsNot Nothing Then
                If IsDBNull(objCALCULATE_FIELD) = False Then
                    CALCULATE_FIELD = objCALCULATE_FIELD.ToString
                End If
            End If

            If IsDBNull(DataGridView1.CurrentRow.Cells(CALCULATE_FIELD).Value) = False Then
                'get regist number
                'Dim parameters As New Dictionary(Of String, Object)

                'parameters.Add("@p4", DataGridView1.CurrentRow.Cells("INCOME_PER_YEAR").Value)
                parameters.Add("@p4", DataGridView1.CurrentRow.Cells(CALCULATE_FIELD).Value)
                parameters.Add("@p5", CDate(DataGridView1.CurrentRow.Cells("REGIST_DATE").Value).Month)

                query = String.Empty
                query &= "SELECT        FIRST_REGIST_RATE "
                query &= "FROM            MB_MEMBER_RATE_DETAIL "
                query &= "WHERE        (MEMBER_MAIN_GROUP_CODE = @p0) AND (MEMBER_GROUP_CODE = @p1) AND (MEMBER_MAIN_TYPE_CODE = @p2) AND (MEMBER_TYPE_CODE = @p3) AND "
                query &= "                          (@p4 BETWEEN START_INCOME_AMOUNT AND END_INCOME_AMOUNT) AND (@p5 BETWEEN START_MONTH_FIRST_YEAR AND "
                query &= "                         END_MONTH_FIRST_YEAR) AND (INACTIVE = 0)"

                Dim FIRST_YEAR_RATE As Decimal = 0
                Try
                    FIRST_YEAR_RATE = CDec(client.ExecuteScalar(query, parameters, user_session))
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar FIRST_YEAR_RATE")
                End Try

                'Create a report parameter for the sales order number 
                Dim PRICE As New ReportParameter()
                PRICE.Name = "PRICE"
                PRICE.Values.Add(FIRST_YEAR_RATE.ToString)

                Dim REGIST_CODE As New ReportParameter()
                REGIST_CODE.Name = "REGIST_CODE"
                REGIST_CODE.Values.Add(DataGridView1.CurrentRow.Cells("REGIST_CODE").Value.ToString)

                Dim COMP_PERSON_NAME_TH As New ReportParameter()
                COMP_PERSON_NAME_TH.Name = "COMP_PERSON_NAME_TH"
                COMP_PERSON_NAME_TH.Values.Add(DataGridView1.CurrentRow.Cells("COMP_PERSON_NAME_TH").Value.ToString)

                Dim COMP_PERSON_NAME_EN As New ReportParameter()
                COMP_PERSON_NAME_EN.Name = "COMP_PERSON_NAME_EN"
                COMP_PERSON_NAME_EN.Values.Add(DataGridView1.CurrentRow.Cells("COMP_PERSON_NAME_EN").Value.ToString)

                Dim REGIST_DATE As New ReportParameter()
                REGIST_DATE.Name = "REGIST_DATE"
                REGIST_DATE.Values.Add(DataGridView1.CurrentRow.Cells("REGIST_DATE").Value.ToString)

                'Set the report parameters for the report
                Dim params() As ReportParameter = {PRICE, REGIST_CODE, COMP_PERSON_NAME_TH, COMP_PERSON_NAME_EN, REGIST_DATE}
                f.ReportViewer1.LocalReport.SetParameters(params)

                f.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
                f.ReportViewer1.LocalReport.Refresh()

                f.WindowState = FormWindowState.Maximized
                If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    '
                End If
                f.Dispose()
                f = Nothing
            Else
                MessageBox.Show("ไม่มีข้อมูลในฟิล " & CALCULATE_FIELD & " เพื่อใช้ในการคำนวณ")
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