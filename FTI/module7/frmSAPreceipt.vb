Imports System.Linq.Expressions

Public Class frmSAPreceipt

    Dim ds As DataSet
    Dim dFROM As Date
    Dim dTO As Date

    Private Sub frmFTIconsideration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        ds = New DataSet

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub getMB_MEMBER_ADVISOR(ByVal DATE_FROM As Date, ByVal DATE_TO As Date)
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", New DateTime(DATE_FROM.Year, DATE_FROM.Month, DATE_FROM.Day, 0, 0, 0))
        parameters.Add("@p1", New DateTime(DATE_TO.Year, DATE_TO.Month, DATE_TO.Day, 23, 59, 59))
        'parameters.Add("@p2", QTY)

        dFROM = DATE_FROM
        dTO = DATE_TO

        'Dim sFTI_MB_MEMBER_ADVISOR As String = getParameters(1, "FTI_MB_MEMBER_ADVISOR")

        Dim query As String = String.Empty
        query &= "SELECT   DIV_CODE, SUB_TYPE, RECEIPT_CODE, RECEIPT_DATE, REF_SUB_TYPE, REF_NO, REF_DATE, PL_DIV_CODE, PROJ_ID, ATV_CODE, CASHIER_CODE, ACCOUNT_NUMBER, CURR_CODE, ARTYPE_CODE, "
        query &= "                         AR_CODE, BANK_CODE, BRANCH_CODE, EX_RATE, REMARK, CR_BY, CR_DATE, UPD_BY, UPD_DATE, RECEIPT_AMT, PRINT_NUM, DELIVER_YN, CANCEL_FLAG, CANCEL_REASON, CANCEL_BY, "
        query &= "                         CANCEL_DATE, CLOSE_YN, REF_ITF_GL, TAX_YN, USE_FLAG, AR_NAME, CR_PROG_ID, VAT_RATE, VAT_TYPE, GROSS_AMT, PAY_AMT, PAY_DATE, RN_AMT, BAL_AMT, NOTE, POST_GL_DATE, REF_GL, "
        query &= "                         TAX_ID "
        query &= "FROM            FN_RECEIPT_MASTER "
        query &= "WHERE        (RECEIPT_DATE BETWEEN @p0 And @p1) "
        query &= "ORDER BY RECEIPT_DATE"

        Dim dt As DataTable = fillWebSQL(query, parameters, "FN_RECEIPT_MASTER").Copy

        If ds.Tables.Contains("FN_RECEIPT_MASTER") = True Then
            ds.Tables("FN_RECEIPT_MASTER").Clear()
            ds.Tables("FN_RECEIPT_MASTER").Merge(dt)
            ds.Tables("FN_RECEIPT_MASTER").AcceptChanges()
        Else
            dt.PrimaryKey = New DataColumn() {dt.Columns("RECEIPT_CODE")}
            ds.Tables.Add(dt)
            DataGridView1.DataSource = ds.Tables("FN_RECEIPT_MASTER")
            'DataGridView1.Columns("MEMBER_MAIN_GROUP_CODE").Width = 50
            'DataGridView1.Columns("MEMBER_GROUP_CODE").Width = 50
            'DataGridView1.Columns("MEMBER_MAIN_TYPE_CODE").Width = 50
            'DataGridView1.Columns("MEMBER_TYPE_CODE").Width = 50

            'DataGridView1.Columns("MEMBER_GROUP_RDLC_GROUP").Visible = False
            'DataGridView1.Columns("QTY_EXP").Visible = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btFind.Click
        getMB_MEMBER_ADVISOR(DateTimePicker1.Value, DateTimePicker2.Value)


        'Dim f As New frmReportViewer
        ''Me.BindingSource1.DataSource = MyDataTable
        ''Dim rds As New ReportDataSource("DataSet1", Me.BindingSource1)
        'f.ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "/Reports/rdlctemp.rdlc"
        ''Me.ReportViewer1.LocalReport.DataSources.Clear()
        ''Me.ReportViewer1.LocalReport.DataSources.Add(rds)

        'f.WindowState = FormWindowState.Maximized
        'If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
        '    '
        'End If
        'f.Dispose()
        'f = Nothing
    End Sub

    Private Sub llRules_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Dim f As New frmFTIconsiderationRule2
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
        If ds.Tables("FN_RECEIPT_MASTER").Rows.Count > 0 Then
            MessageBox.Show(btPrint.Text & " เสร็จสิ้น")
        End If
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub
End Class