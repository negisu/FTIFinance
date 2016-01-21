Public Class frmFINReceipt
    Dim ds As DataSet
    Dim AR_ADDRESSDataTable As DataTable
    Dim IV_HEADDataTable As DataTable
    Dim IV_DETAILDataTable As DataTable
    Dim OU_CODE As String = "001"
    Dim TRAN_NO As String
    Dim PN_TRAN_NO As String

    Dim isNew As Boolean = True
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    Private Sub btProdAdd_Click(sender As Object, e As EventArgs) Handles btProdAdd.Click
        Dim f As New frmFINSubSection
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Dim selectedRow As DataGridViewRow = f.DataGridView1.CurrentRow
            Dim newProductRow As DataRow = IV_DETAILDataTable.NewRow()

            newProductRow("SUB_SECTION_CODE") = selectedRow.Cells("SUB_SECTION_CODE").Value
            newProductRow("SUB_SECTION_NAME") = selectedRow.Cells("SUB_SECTION_NAME").Value
            newProductRow("DIV_CODE") = selectedRow.Cells("DIV_CODE").Value
            newProductRow("DIV_NAME") = selectedRow.Cells("DIV_NAME").Value
            Try
                IV_DETAILDataTable.Rows.Add(newProductRow)
            Catch ex As ConstraintException
                Call MsgBox("รายการที่ต้องการเพิ่มมีอยู่แล้ว", 0, "พบข้อผิดพลาด")
            Catch ex As Exception

            End Try
        End If
        f.Dispose()
        f = Nothing
        IV_DETAILGridView.Refresh()
        IV_DETAILGridView.Update()

    End Sub

    '==============================================================================================================================================='
    '==============================================================================================================================================='
    '==============================================================================================================================================='
    '     L               OOOOOOOO             A            DDDDDDD                                    
    '     L              O        O           A A           D      D                           
    '     L              O        O          A   A          D       D                          
    '     L              O        O         A     A         D       D                          
    '     L              O        O        AAAAAAAAA        D       D                          
    '     L              O        O       A         A       D      D                           
    '     LLLLLLLLL       OOOOOOOO       A           A      DDDDDDD                                              
    '==============================================================================================================================================='
    '==============================================================================================================================================='
    '==============================================================================================================================================='

    Private Sub frmFINPaymentNotice_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        getPL_PROJECT()
        getPL_ACTIVITY()
        getSU_DIVISION()
        Dim query As String = String.Empty
        Dim parameters As New Dictionary(Of String, Object)
        If String.IsNullOrEmpty(TRAN_NOLabel.Text) Then
            TRAN_NOLabel.Text = "<AUTO>"
            query = "SELECT TOP 0 * FROM IV_DETAIL INNER JOIN IV_SUB_SECTION ON IV_DETAIL.SUB_SECTION_CODE = IV_SUB_SECTION.SUB_SECTION_CODE INNER JOIN SU_DIVISION ON IV_SUB_SECTION.DIV_CODE_INC=SU_DIVISION.DIV_CODE "
        Else
            TRAN_NO = TRAN_NOLabel.Text
            getIV_HEAD()
            Try
                getIV_ADDRESS()
            Catch ex As Exception

            End Try

            query = "SELECT * FROM IV_DETAIL INNER JOIN IV_SUB_SECTION ON IV_DETAIL.SUB_SECTION_CODE = IV_SUB_SECTION.SUB_SECTION_CODE INNER JOIN SU_DIVISION ON IV_SUB_SECTION.DIV_CODE_INC=SU_DIVISION.DIV_CODE WHERE TRAN_NO = @p0"
            parameters.Add("@p0", TRAN_NO)
        End If
        ' Load ComboBox Choice Data from Database
        ' GridView Initialize
        IV_DETAILGridView.Columns.Clear()
        IV_DETAILDataTable = fillWebSQL(query, parameters, "IV_DETAIL")
        Dim primaryKey(1) As DataColumn
        primaryKey(0) = IV_DETAILDataTable.Columns("SUB_SECTION_CODE")
        IV_DETAILDataTable.PrimaryKey = primaryKey
        IV_DETAILGridView.DataSource = IV_DETAILDataTable
        For i As Integer = 0 To IV_DETAILGridView.ColumnCount - 1
            IV_DETAILGridView.Columns(i).Visible = False
        Next
        IV_DETAILGridView.Columns("SUB_SECTION_CODE").HeaderText = "รหัสรายการ"
        IV_DETAILGridView.Columns("SUB_SECTION_NAME").HeaderText = "ชื่อรายการ"
        IV_DETAILGridView.Columns("DIV_NAME").HeaderText = "หน่วยงาน"
        IV_DETAILGridView.Columns("QTY").HeaderText = "จำนวน"
        IV_DETAILGridView.Columns("U_PRICE").HeaderText = "ราคาต่อหน่วย"
        IV_DETAILGridView.Columns("GROSS_AMT").HeaderText = "ราคารวม"
        IV_DETAILGridView.Columns("DISC_AMT").HeaderText = "ส่วนลด"
        IV_DETAILGridView.Columns("NET_AMT").HeaderText = "ราคาหลังหักส่วนลด"
        IV_DETAILGridView.Columns("VAT_AMT").HeaderText = "ภาษีมูลค่าเพิ่ม"
        IV_DETAILGridView.Columns("TOTAL_AMT").HeaderText = "ราคารวมภาษีมูลค่าเพิ่ม"
        IV_DETAILGridView.Columns("NOTE").HeaderText = "หมายเหตุ"

        IV_DETAILGridView.Columns("SUB_SECTION_CODE").DisplayIndex = 0
        IV_DETAILGridView.Columns("SUB_SECTION_NAME").DisplayIndex = 1
        IV_DETAILGridView.Columns("DIV_NAME").DisplayIndex = 2
        IV_DETAILGridView.Columns("QTY").DisplayIndex = 3
        IV_DETAILGridView.Columns("U_PRICE").DisplayIndex = 4
        IV_DETAILGridView.Columns("GROSS_AMT").DisplayIndex = 5
        IV_DETAILGridView.Columns("DISC_AMT").DisplayIndex = 6
        IV_DETAILGridView.Columns("NET_AMT").DisplayIndex = 7
        IV_DETAILGridView.Columns("VAT_AMT").DisplayIndex = 8
        IV_DETAILGridView.Columns("TOTAL_AMT").DisplayIndex = 9
        IV_DETAILGridView.Columns("NOTE").DisplayIndex = 10

        IV_DETAILGridView.Columns("SUB_SECTION_CODE").Visible = True
        IV_DETAILGridView.Columns("SUB_SECTION_NAME").Visible = True
        IV_DETAILGridView.Columns("DIV_NAME").Visible = True
        IV_DETAILGridView.Columns("QTY").Visible = True
        IV_DETAILGridView.Columns("U_PRICE").Visible = True
        IV_DETAILGridView.Columns("GROSS_AMT").Visible = True
        IV_DETAILGridView.Columns("DISC_AMT").Visible = True
        IV_DETAILGridView.Columns("NET_AMT").Visible = True
        IV_DETAILGridView.Columns("VAT_AMT").Visible = True
        IV_DETAILGridView.Columns("TOTAL_AMT").Visible = True
        IV_DETAILGridView.Columns("NOTE").Visible = True

        IV_DETAILGridView.Columns("SUB_SECTION_CODE").ReadOnly = True
        IV_DETAILGridView.Columns("SUB_SECTION_NAME").ReadOnly = True
        IV_DETAILGridView.Columns("DIV_NAME").ReadOnly = True
        IV_DETAILGridView.Columns("GROSS_AMT").ReadOnly = True
        IV_DETAILGridView.Columns("NET_AMT").ReadOnly = True
        IV_DETAILGridView.Columns("VAT_AMT").ReadOnly = True
        IV_DETAILGridView.Columns("TOTAL_AMT").ReadOnly = True

        IV_DETAILGridView.Columns("SUB_SECTION_CODE").ValueType = GetType(String)
        IV_DETAILGridView.Columns("SUB_SECTION_NAME").ValueType = GetType(String)
        IV_DETAILGridView.Columns("DIV_NAME").ValueType = GetType(String)
        IV_DETAILGridView.Columns("QTY").ValueType = GetType(String)
        IV_DETAILGridView.Columns("U_PRICE").ValueType = GetType(String)
        IV_DETAILGridView.Columns("GROSS_AMT").ValueType = GetType(String)
        IV_DETAILGridView.Columns("DISC_AMT").ValueType = GetType(String)
        IV_DETAILGridView.Columns("NET_AMT").ValueType = GetType(String)
        IV_DETAILGridView.Columns("VAT_AMT").ValueType = GetType(String)
        IV_DETAILGridView.Columns("TOTAL_AMT").ValueType = GetType(String)
        IV_DETAILGridView.Columns("NOTE").ValueType = GetType(String)
        If (isNew) Then
            TRAN_DATETextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
        End If

        ' Display User Information
        UserNameLabel.Text = user_firstname & "    " & user_lastname
        calculateSum()

    End Sub

    Private Sub frmFINPaymentNotice_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fIV = Nothing
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If TRAN_NO = Nothing Then
            TRAN_NO = DocumentNumberHelper.getIV_DOC_RUNNING(OU_CODE, DIV_CODETextBox.Text, DateTime.Now.ToString("yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat), "PN1")
            TRAN_NOLabel.Text = TRAN_NO
        End If
        If (MessageBox.Show("คุณต้องการที่จะบันทึกเอกสารหมายเลข " & TRAN_NO.ToString() & " ใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
            insertOrUpdateIV_HEAD()
            insertOrUpdateIV_ADDRESS()
            insertOrUpdateIV_DETAIL()
            Dim pnDocRunning = New DOC_RUNNING
            pnDocRunning.OU_CODE = OU_CODE
            pnDocRunning.DIV_CODE = DIV_CODETextBox.Text
            pnDocRunning.BUDGET_YEAR = Integer.Parse(DateTime.Now.ToString("yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat))
            pnDocRunning.PERIOD = 0
            pnDocRunning.SUB_TYPE = "PN1"
            pnDocRunning.DOC_RUNNING_NO = TRAN_NO
            pnDocRunning.CR_BY = user_name
            pnDocRunning.CR_DATE = DateTime.Now
            QueryHelper.insertModel("IV_DOC_RUNNING", pnDocRunning)
        End If
    End Sub

    Private Sub btProdDel_Click(sender As Object, e As EventArgs) Handles btProdDel.Click
        IV_DETAILGridView.Rows.Remove(IV_DETAILGridView.CurrentRow)
    End Sub

    Private Sub AtvNameComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ATV_NAMEComboBox.SelectedIndexChanged
        ATV_CODETextBox.Text = ATV_NAMEComboBox.SelectedValue.ToString()
    End Sub

    Private Sub TodayButton_Click(sender As Object, e As EventArgs) Handles TodayButton.Click
        TRAN_DATETextBox.Text = DateTime.Now.ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
    End Sub

    Private Sub IsCurrencyCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles IsCurrencyCheckBox.CheckedChanged
        CurrencyComboBox.Enabled = IsCurrencyCheckBox.Checked
    End Sub

    Private Sub ProjNameComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PROJ_NAMEComboBox.SelectedIndexChanged
        PROJ_IDTextBox.Text = PROJ_NAMEComboBox.SelectedValue.ToString()
    End Sub

    Private Sub DIV_NAMEComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DIV_NAMEComboBox.SelectedIndexChanged
        DIV_CODETextBox.Text = DIV_NAMEComboBox.SelectedValue.ToString()
    End Sub

    Private Sub ProductDataGridView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles IV_DETAILGridView.CellValueChanged

        If (IV_DETAILGridView.Columns(e.ColumnIndex).Name = "QTY" Or IV_DETAILGridView.Columns(e.ColumnIndex).Name = "U_PRICE" Or IV_DETAILGridView.Columns(e.ColumnIndex).Name = "DISC_AMT") And e.RowIndex > -1 Then
            Try
                Dim price As Double = Double.Parse(IV_DETAILGridView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString())
                If (IV_DETAILGridView.Columns(e.ColumnIndex).Name = "QTY") Then
                    IV_DETAILDataTable.Rows(e.RowIndex).Item(e.ColumnIndex) = Format(price, "0")
                Else
                    IV_DETAILDataTable.Rows(e.RowIndex).Item(e.ColumnIndex) = Format(price, "0.00")
                End If

            Catch ex As Exception
                Call MsgBox("คุณใส่ข้อมูลไม่ถูกต้อง กรุณากรอกเป็นตัวเลข", 0, "พบข้อผิดพลาด")
                IV_DETAILDataTable.Rows(e.RowIndex).Item(e.ColumnIndex) = Nothing
            End Try
            Try
                IV_DETAILDataTable.Rows(e.RowIndex).Item("GROSS_AMT") = Format((Double.Parse(IV_DETAILGridView.Rows(e.RowIndex).Cells("QTY").Value.ToString()) * Double.Parse(IV_DETAILGridView.Rows(e.RowIndex).Cells("U_PRICE").Value.ToString())), "0.00")
            Catch ex As Exception

            End Try

            Try
                IV_DETAILDataTable.Rows(e.RowIndex).Item("NET_AMT") = Format((Double.Parse(IV_DETAILGridView.Rows(e.RowIndex).Cells("GROSS_AMT").Value.ToString()) - Double.Parse(IV_DETAILGridView.Rows(e.RowIndex).Cells("DISC_AMT").Value.ToString())), "0.00")
            Catch ex As Exception

            End Try

            Try
                IV_DETAILDataTable.Rows(e.RowIndex).Item("VAT_AMT") = Format((Double.Parse(IV_DETAILGridView.Rows(e.RowIndex).Cells("NET_AMT").Value.ToString()) * 0.07), "0.00")
            Catch ex As Exception

            End Try

            Try
                IV_DETAILGridView.Rows(e.RowIndex).Cells("TOTAL_AMT").Value = Format((Double.Parse(IV_DETAILGridView.Rows(e.RowIndex).Cells("NET_AMT").Value.ToString()) + Double.Parse(IV_DETAILGridView.Rows(e.RowIndex).Cells("VAT_AMT").Value.ToString())), "0.00")
            Catch ex As Exception

            End Try

        End If
        calculateSum()
        IV_DETAILGridView.Refresh()
        IV_DETAILGridView.Update()
    End Sub
    Private Sub calculateSum()
        Dim sum As Double
        Dim sumDiscount As Double
        Dim Total As Double
        For i As Integer = 0 To IV_DETAILGridView.Rows.Count
            Try
                sum += Double.Parse(IV_DETAILGridView.Rows(i).Cells("GROSS_AMT").Value.ToString())
            Catch ex As Exception

            End Try
            Try
                sumDiscount += Double.Parse(IV_DETAILGridView.Rows(i).Cells("NET_AMT").Value.ToString())
            Catch ex As Exception

            End Try
            Try
                Total += Double.Parse(IV_DETAILGridView.Rows(i).Cells("TOTAL_AMT").Value.ToString())
            Catch ex As Exception

            End Try
        Next
        SumLabel.Text = Format(sum, "#,##0.00")
        SumDiscountLabel.Text = Format(sumDiscount, "#,##0.00")
        TotalLabel.Text = Format(Total, "#,##0.00")
    End Sub
    Private Sub ArNameTextBox_DoubleClick(sender As Object, e As EventArgs) Handles AR_NAMETextBox.DoubleClick
        Dim f As New frmFINAR_MEMBER
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Dim selectedRow As DataGridViewRow = f.DataGridView1.CurrentRow
            AR_CODETextBox.Text = selectedRow.Cells("AR_CODE").Value.ToString()
            AR_NAMETextBox.Text = selectedRow.Cells("FULL_NAME").Value.ToString()
            TaxNoLabel.Text = selectedRow.Cells("TAX_ID").Value.ToString()
            getAR_ADDRESS()
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ProductDataGridView_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles IV_DETAILGridView.RowsRemoved
        calculateSum()
    End Sub

    '==============================================================================================================================================='
    '==============================================================================================================================================='
    '==============================================================================================================================================='
    '   DDDDDDD         A       TTTTTTTTT       A                  BBBBBBBB         A            SSSSS     EEEEEEEE                                   
    '   D      D       A A          T          A A                 B       B       A A          S     S    E                 
    '   D       D     A   A         T         A   A                B       B      A   A        S           E                
    '   D       D    A     A        T        A     A               BBBBBBBB      A     A        SSSSS      EEEEEEEE                         
    '   D       D   AAAAAAAAA       T       AAAAAAAAA              B       B    AAAAAAAAA            S     E                              
    '   D      D   A         A      T      A         A             B       B   A         A     S      S    E                 
    '   DDDDDDD   A           A     T     A           A            BBBBBBBB   A           A     SSSSSS     EEEEEEEE                             
    '==============================================================================================================================================='
    '==============================================================================================================================================='
    '==============================================================================================================================================='

    Private Sub getPL_ACTIVITY()
        ATV_NAMEComboBox.DataSource = QueryHelper.selectStar("PL_ACTIVITY").DefaultView
        ATV_NAMEComboBox.DisplayMember = "ATV_NAME"
        ATV_NAMEComboBox.ValueMember = "ATV_CODE"
        ATV_CODETextBox.Text = ATV_NAMEComboBox.SelectedValue.ToString()
    End Sub

    Private Sub getSU_DIVISION()
        DIV_NAMEComboBox.DataSource = QueryHelper.selectStar("SU_DIVISION").DefaultView
        DIV_NAMEComboBox.DisplayMember = "DIV_NAME"
        DIV_NAMEComboBox.ValueMember = "DIV_CODE"
        DIV_CODETextBox.Text = DIV_NAMEComboBox.SelectedValue.ToString()
    End Sub

    Private Sub getPL_PROJECT()
        PROJ_NAMEComboBox.DataSource = QueryHelper.selectStar("PL_PROJECT").DefaultView
        PROJ_NAMEComboBox.DisplayMember = "PROJ_NAME"
        PROJ_NAMEComboBox.ValueMember = "PROJ_ID"
        PROJ_IDTextBox.Text = PROJ_NAMEComboBox.SelectedValue.ToString()
    End Sub
    Private Sub getIV_HEAD()
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = String.Empty
        query &= "SELECT  TOP 1 * "
        query &= "FROM            IV_HEAD INNER JOIN AR_MEMBER2 ON IV_HEAD.AR_CODE = AR_MEMBER2.AR_CODE "
        query &= "WHERE           TRAN_NO = @p0"
        parameters.Add("@p0", TRAN_NO)
        IV_HEADDataTable = fillWebSQL(query, parameters, "IV_HEAD")


        DIV_CODETextBox.Text = IV_HEADDataTable.Rows(0).Item("DIV_CODE").ToString()
        TRAN_DATETextBox.Text = Date.Parse(IV_HEADDataTable.Rows(0).Item("TRAN_DATE").ToString()).ToString("{dd/MM/yyyy}", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
        ATV_NAMEComboBox.SelectedItem = ATV_NAMEComboBox.FindStringExact(IV_HEADDataTable.Rows(0).Item("ATV_CODE").ToString())
        ATV_CODETextBox.Text = ATV_NAMEComboBox.SelectedValue.ToString()
        PROJ_NAMEComboBox.SelectedItem = PROJ_NAMEComboBox.FindStringExact(IV_HEADDataTable.Rows(0).Item("PROJ_ID").ToString())
        PROJ_IDTextBox.Text = PROJ_NAMEComboBox.SelectedValue.ToString()

        AR_CODETextBox.Text = IV_HEADDataTable.Rows(0).Item("AR_CODE").ToString()
        AR_NAMETextBox.Text = IV_HEADDataTable.Rows(0).Item("FULL_NAME").ToString()

    End Sub

    Private Sub getIV_ADDRESS()
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = String.Empty
        query &= "SELECT  TOP 1 * "
        query &= "FROM            PN_ADDRESS "
        query &= "WHERE           TRAN_NO = @p0"
        parameters.Add("@p0", TRAN_NO)
        AR_ADDRESSDataTable = fillWebSQL(query, parameters, "PN_ADDRESS")


        ADDR1_EN.Text = AR_ADDRESSDataTable.Rows(0).Item("ADDR1_EN").ToString()
        ADDR2_EN.Text = AR_ADDRESSDataTable.Rows(0).Item("ADDR2_EN").ToString()
        ADDR3_EN.Text = AR_ADDRESSDataTable.Rows(0).Item("ADDR3_EN").ToString()
        ADDR1_TH.Text = AR_ADDRESSDataTable.Rows(0).Item("ADDR1_TH").ToString()
        ADDR2_TH.Text = AR_ADDRESSDataTable.Rows(0).Item("ADDR2_TH").ToString()
        ADDR3_TH.Text = AR_ADDRESSDataTable.Rows(0).Item("ADDR3_TH").ToString()

        ATTN_NAME_ENTextBox.Text = AR_ADDRESSDataTable.Rows(0).Item("ATTN_NAME_EN").ToString()
        ATTN_NAME_THTextBox.Text = AR_ADDRESSDataTable.Rows(0).Item("ATTN_NAME_TH").ToString()

        POST_CODETextBox.Text = AR_ADDRESSDataTable.Rows(0).Item("POST_CODE").ToString()
        TELEPHONETextBox.Text = AR_ADDRESSDataTable.Rows(0).Item("TELEPHONE").ToString()
        FAXTextBox.Text = AR_ADDRESSDataTable.Rows(0).Item("FAX").ToString()
    End Sub

    Private Sub getAR_ADDRESS()
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = String.Empty
        query &= "SELECT  * "
        query &= "FROM            AR_ADDRESS2 "
        query &= "WHERE           AR_CODE = '"
        query &= AR_CODETextBox.Text
        query &= "'"
        AR_ADDRESSDataTable = fillWebSQL(query, parameters, "AR_ADDRESS2")
    End Sub

    Private Sub getIV_DETAIL()
        Dim query As String = "SELECT * FROM IV_DETAIL INNER JOIN IV_SUB_SECTION ON IV_DETAIL.SUB_SECTION_CODE = IV_SUB_SECTION.SUB_SECTION_CODE INNER JOIN SU_DIVISION ON IV_SUB_SECTION.DIV_CODE_INC=SU_DIVISION.DIV_CODE  WHERE TRAN_NO = '@p0'"
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", TRAN_NO)
        IV_DETAILDataTable = fillWebSQL(query, New Dictionary(Of String, Object), "IV_DETAIL")
        IV_DETAILGridView.Refresh()
        IV_DETAILGridView.Update()
    End Sub

    Private Sub insertOrUpdateIV_HEAD()
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = "DELETE FROM IV_HEAD WHERE TRAN_NO = @p0"
        parameters.Add("@p0", TRAN_NO)

        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        Dim iv As New IV_HEAD
        iv.OU_CODE = OU_CODE
        iv.DIV_CODE = DIV_CODETextBox.Text
        iv.TRAN_NO = TRAN_NO
        iv.TRAN_DATE = Date.ParseExact(TRAN_DATETextBox.Text, "dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
        iv.ATV_CODE = ATV_CODETextBox.Text
        iv.UPD_BY = user_name
        iv.UPD_DATE = DateTime.Now
        iv.AR_CODE = AR_CODETextBox.Text
        iv.PROJ_ID = PROJ_IDTextBox.Text
        QueryHelper.insertModel("IV_HEAD", iv)

    End Sub

    Private Sub insertOrUpdateIV_ADDRESS()

        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = "DELETE FROM PN_ADDRESS WHERE TRAN_NO = @p0"
        parameters.Add("@p0", TRAN_NO)

        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        Dim iv As New IV_ADDRESS

        iv.TRAN_NO = TRAN_NO
        iv.AR_CODE = AR_CODETextBox.Text
        iv.ADDR1_EN = ADDR1_EN.Text
        iv.ADDR2_EN = ADDR2_EN.Text
        iv.ADDR3_EN = ADDR3_EN.Text
        iv.ADDR1_TH = ADDR1_TH.Text
        iv.ADDR2_TH = ADDR2_TH.Text
        iv.ADDR3_TH = ADDR3_TH.Text

        iv.ATTN_NAME_EN = ATTN_NAME_ENTextBox.Text
        iv.ATTN_NAME_TH = ATTN_NAME_THTextBox.Text
        iv.POST_CODE = POST_CODETextBox.Text
        iv.TELEPHONE = TELEPHONETextBox.Text
        iv.FAX = FAXTextBox.Text
        iv.UPD_BY = user_name
        iv.UPD_DATE = DateTime.Now
        iv.AR_CODE = AR_CODETextBox.Text

        QueryHelper.insertModel("IV_ADDRESS", iv)

    End Sub

    Private Sub insertOrUpdateIV_DETAIL()
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = "DELETE FROM IV_DETAIL WHERE TRAN_NO = @p0"
        parameters.Add("@p0", TRAN_NO)

        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        For Each row As DataRow In IV_DETAILDataTable.Rows
            If String.IsNullOrEmpty(row.Item("TRAN_NO").ToString()) Then
                row.Item("TRAN_NO") = TRAN_NO
            End If

            parameters = New Dictionary(Of String, Object)
            query = String.Empty
            parameters.Add("@p0", TRAN_NO)

            query &= "SELECT * FROM IV_DETAIL "
            query &= "WHERE TRAN_NO = @p0"

            If IV_DETAILDataTable.GetChanges IsNot Nothing Then
                Try
                    updateWebSQL(query, parameters, IV_DETAILDataTable.GetChanges)
                Catch ex As Exception
                    MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=updateWebSQL")
                End Try
            End If
        Next
    End Sub




    Private Sub selectAddressButton_Click(sender As Object, e As EventArgs) Handles selectAddressButton.Click
        Dim f As New frmFINAddress
        If (String.IsNullOrEmpty(AR_CODETextBox.Text)) Then
            Call MsgBox("กรุณาใส่ข้อมูลผู้จ่ายตัง", 0, "พบข้อผิดพลาด")
        Else

            f.AR_CODELabel.Text = AR_CODETextBox.Text
            f.AR_NAMELabel.Text = AR_NAMETextBox.Text
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                Dim selectedRow As DataGridViewRow = f.DataGridView1.CurrentRow

                ADDR1_TH.Text = selectedRow.Cells("ADDR1_TH").Value.ToString()
                ADDR2_TH.Text = selectedRow.Cells("ADDR2_TH").Value.ToString()
                ADDR3_TH.Text = selectedRow.Cells("ADDR3_TH").Value.ToString()

                ADDR1_EN.Text = selectedRow.Cells("ADDR1_EN").Value.ToString()
                ADDR2_EN.Text = selectedRow.Cells("ADDR2_EN").Value.ToString()
                ADDR3_EN.Text = selectedRow.Cells("ADDR3_EN").Value.ToString()

                ATTN_NAME_THTextBox.Text = selectedRow.Cells("ATTN_NAME_TH").Value.ToString()
                ATTN_NAME_ENTextBox.Text = selectedRow.Cells("ATTN_NAME_EN").Value.ToString()
                POST_CODETextBox.Text = selectedRow.Cells("POST_CODE").Value.ToString()
                TELEPHONETextBox.Text = selectedRow.Cells("TELEPHONE").Value.ToString()
                FAXTextBox.Text = selectedRow.Cells("FAX").Value.ToString()

            End If
            f.Dispose()
            f = Nothing
        End If
    End Sub

    Private Sub IV_DETAILGridView_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles IV_DETAILGridView.RowsAdded
        calculateSum()
    End Sub
End Class