Public Class frmFINPaymentNotice
    Dim ds As DataSet
    Dim MB_COMP_PERSON_ADDRESS As DataTable
    Dim ADDRESSDataTable As DataTable
    Dim HEADDataTable As DataTable
    Dim DETAILDataTable As DataTable

    Dim OU_CODE As String = "001"
    Dim tempTRAN_NO As String
    Dim TRAN_NO As String
    Dim VAT_RATE As Double = 7.0
    Dim vatType As String
    Dim isNew As Boolean = True


    Dim U_PRICE As Double
    Dim U_PRICE_VAT As Double
    Dim U_PRICE_INC_VAT As Double
    Dim NET_U_PRICE As Double
    Dim NET_U_PRICE_VAT As Double
    Dim NET_U_PRICE_INC_VAT As Double
    Dim DISC_AMT As Double
    Dim DISC_AMT_VAT As Double
    Dim DISC_AMT_INC_VAT As Double
    Dim TOTAL As Double
    Dim TOTAL_VAT As Double
    Dim SUM_TOTAL As Double
    Dim TOP_DISC_AMT As Double
    Dim GRAND_AMT As Double
    Dim GRAND_AMT_VAT As Double
    Dim GRAND_AMT_INC_VAT As Double

    Dim numberColumns() As String = _
        {"U_PRICE", "U_PRICE_VAT", "U_PRICE_INC_VAT", "NET_U_PRICE", "NET_U_PRICE_VAT", "NET_U_PRICE_INC_VAT", "DISC_AMT", "DISC_AMT_VAT", "DISC_AMT_INC_VAT", "TOTAL", "TOTAL_VAT", "SUM_TOTAL", "TOP_DISC_AMT", "GRAND_AMT", "GRAND_AMT_VAT", "GRAND_AMT_INC_VAT"}



    Dim isLockAll As Boolean = False
    Dim isChanged As Boolean = False
    Dim ex_rate As Double = 1.0


    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    Private Sub btProdAdd_Click(sender As Object, e As EventArgs) Handles btProdAdd.Click
        Dim f As New frmFINSubSection
        f.VATType = getVATType()
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Dim selectedRow As DataGridViewRow = f.DataGridView1.CurrentRow
            Dim newProductRow As DataRow = DETAILDataTable.NewRow()

            newProductRow("SUB_SECTION_CODE") = selectedRow.Cells("SUB_SECTION_CODE").Value
            newProductRow("NOTE") = selectedRow.Cells("SUB_SECTION_NAME").Value
            newProductRow("NOTE_EN") = selectedRow.Cells("SUB_SECTION_NAME_EN").Value
            newProductRow("DIV_CODE") = selectedRow.Cells("DIV_CODE").Value
            newProductRow("DIV_NAME") = selectedRow.Cells("DIV_NAME").Value
            newProductRow("BUDGET_YEAR") = DateTime.Now.ToString("yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat)
            newProductRow("QTY") = "1"
            newProductRow("DISC_AMT_INC_VAT") = "0.00"
            newProductRow("SEQ") = DETAILDataTable.Rows.Count + 1
            newProductRow("OU_CODE") = OU_CODE
            newProductRow("SUB_TYPE") = "PN1"
            Try
                DETAILDataTable.Rows.Add(newProductRow)
            Catch ex As ConstraintException
                Call MsgBox("รายการที่ต้องการเพิ่มมีอยู่แล้ว", 0, "พบข้อผิดพลาด")
            Catch

            End Try

        End If
        f.Dispose()
        f = Nothing

        DetailGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DetailGridView.AutoResizeColumns()
        DetailGridView.Refresh()
        DetailGridView.Update()

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

        initForm()
        loadForm()
        ' Display User Information
        calculateSum()
        AR_NAMETextBox.Select()
        If Not PermissionHelper.isAdmin() Then lockNonAdminInput()
        isChanged = False

    End Sub

    Sub DetailGridView_CellFormatting(ByVal sender As Object, _
    ByVal e As DataGridViewCellFormattingEventArgs) Handles DetailGridView.CellFormatting
        If e.ColumnIndex = Me.DetailGridView.Columns("NOTE").Index Then
            Me.DetailGridView.Rows(e.RowIndex).Cells(e.ColumnIndex).ToolTipText = Me.DetailGridView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString()
        End If
    End Sub
    Private Sub frmFINPaymentNotice_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fPN = Nothing
    End Sub


    Private Sub initForm()
        isNew = String.IsNullOrEmpty(TRAN_NOLabel.Text)

        CurrencyComboBox.SelectedIndex = 0
        VAT_RATEComboBox.SelectedIndex = 0
        FooterComboBox.SelectedIndex = 0
        CurrencyComboBox.SelectedIndex = 0
        AR_CODETextBox.Text = Nothing
        AR_NAMETextBox.Text = Nothing
        ds = Nothing
        ADDRESSDataTable = New DataTable
        HEADDataTable = New DataTable
        DETAILDataTable = New DataTable
        TRAN_NO = String.Empty
        vatType = "I"
        vatInclude.Checked = True

        TAX_NOTextBox.Text = Nothing

        POST_CODETextBox.Text = Nothing
        TELEPHONETextBox.Text = Nothing
        FAXTextBox.Text = Nothing
        NOTETextBox.TabIndex = Nothing
    End Sub
    Private Sub loadForm()
        gridViewInit()
        If isNew Then
            TRAN_NOLabel.Text = "AUTO"
            TRAN_DATEPicker.Value = DateTime.Now
            DUE_DATEPicker.Value = DateTime.Now
            CR_TERMTextBox.Text = "0"
            DocumentStatusLabel.Text = "ปกติ"
            PrintStatusLabel.Text = "0"
            getHEAD()
        Else
            TRAN_NO = TRAN_NOLabel.Text
            getHEAD()
            getDETAIL()
            Try
                getADDRESS()
            Catch
            End Try
        End If


    End Sub


    Private Sub gridViewInit()

        Dim query As String = "SELECT TOP 0 * FROM PN_DETAIL INNER JOIN IV_SUB_SECTION ON PN_DETAIL.SUB_SECTION_CODE = IV_SUB_SECTION.SUB_SECTION_CODE INNER JOIN SU_DIVISION ON IV_SUB_SECTION.DIV_CODE_INC = SU_DIVISION.DIV_CODE "
        DETAILDataTable = fillWebSQL(query, Nothing, "PN_DETAIL")
        DetailGridView.Columns.Clear()
        Dim primaryKey(1) As DataColumn
        primaryKey(0) = DETAILDataTable.Columns("SUB_SECTION_CODE")
        DETAILDataTable.PrimaryKey = primaryKey
        DetailGridView.DataSource = DETAILDataTable

        For i As Integer = 0 To DetailGridView.ColumnCount - 1
            With DetailGridView.Columns(i)
                .Visible = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.BackColor = Color.FromName("ControlLight")
                .ReadOnly = True
            End With
        Next
        For Each columnName As String In numberColumns
            Try
                With DetailGridView.Columns(columnName).DefaultCellStyle
                    .Format = "n2"
                    .Alignment = DataGridViewContentAlignment.MiddleRight
                End With
            Catch
            End Try
        Next

        With DetailGridView
            .Columns("SEQ").Visible = True
            .Columns("SUB_SECTION_CODE").Visible = True
            .Columns("NOTE").Visible = True
            .Columns("QTY").Visible = True
            .Columns("U_PRICE").Visible = True
            .Columns("U_PRICE_INC_VAT").Visible = True
            .Columns("NET_U_PRICE_INC_VAT").Visible = True
            .Columns("DISC_AMT").Visible = True
            .Columns("DISC_AMT_INC_VAT").Visible = True
            .Columns("TOTAL").Visible = True
            .Columns("TOTAL_VAT").Visible = True
            .Columns("SUM_TOTAL").Visible = True

            .Columns("SEQ").HeaderText = "ลำดับที่"
            .Columns("SUB_SECTION_CODE").HeaderText = "รหัสสินค้า"
            .Columns("NOTE").HeaderText = "ชื่อรายการ"
            .Columns("NOTE_EN").HeaderText = "Name"
            .Columns("DIV_NAME").HeaderText = "หน่วยงาน"
            .Columns("QTY").HeaderText = "จำนวน"
            .Columns("U_PRICE").HeaderText = "ราคาต่อหน่วย"
            .Columns("U_PRICE_INC_VAT").HeaderText = "ราคาต่อหน่วยรวมภาษีมูลค่าเพิ่ม"
            .Columns("NET_U_PRICE_INC_VAT").HeaderText = "ราคาทั้งหมดรวมภาษีมูลค่าเพิ่ม"
            .Columns("DISC_AMT").HeaderText = "ส่วนลด"
            .Columns("DISC_AMT_INC_VAT").HeaderText = "ส่วนลดรวมภาษีมูลค่าเพิ่ม"
            .Columns("TOTAL").HeaderText = "ยอดรวมหลังหักส่วนลด"
            .Columns("TOTAL_VAT").HeaderText = "ภาษีมูลค่าเพิ่ม"
            .Columns("SUM_TOTAL").HeaderText = "ยอดรวมรวมภาษีมูลค่าเพิ่มหลังหักส่วนลด"

            .Columns("QTY").DefaultCellStyle.BackColor = Color.White
            .Columns("NOTE").DefaultCellStyle.BackColor = Color.White
            .Columns("NOTE_EN").DefaultCellStyle.BackColor = Color.White
            .Columns("U_PRICE_INC_VAT").DefaultCellStyle.BackColor = Color.White
            .Columns("DISC_AMT_INC_VAT").DefaultCellStyle.BackColor = Color.White

            .Columns("SEQ").DisplayIndex = 0
            .Columns("SUB_SECTION_CODE").DisplayIndex = 1
            .Columns("NOTE").DisplayIndex = 2
            .Columns("NOTE_EN").DisplayIndex = 3
            .Columns("DIV_NAME").DisplayIndex = 4
            .Columns("QTY").DisplayIndex = 5
            .Columns("U_PRICE").DisplayIndex = 6
            .Columns("U_PRICE_INC_VAT").DisplayIndex = 7
            .Columns("NET_U_PRICE_INC_VAT").DisplayIndex = 8
            .Columns("DISC_AMT").DisplayIndex = 9
            .Columns("DISC_AMT_INC_VAT").DisplayIndex = 10
            .Columns("TOTAL").DisplayIndex = 11
            .Columns("TOTAL_VAT").DisplayIndex = 12
            .Columns("SUM_TOTAL").DisplayIndex = 13

            .Columns("QTY").ReadOnly = False
            .Columns("NOTE_EN").ReadOnly = False
            .Columns("NOTE_EN").ReadOnly = False
            .Columns("U_PRICE_INC_VAT").ReadOnly = False
            .Columns("DISC_AMT_INC_VAT").ReadOnly = False
        End With
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If isNew Then
            TRAN_NO = DocumentNumberHelper.getPN_DOC_RUNNING(OU_CODE, user_div, DateTime.Now.ToString("yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat), "PN1")
        End If

        If validatePN_DETAIL() And validateAddress() Then
            If (MessageBox.Show("คุณต้องการที่จะบันทึกเอกสารหมายเลข " & TRAN_NO.ToString() & " ใช่หรือไม่?", "บันทึกเอกสาร", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                If isNew Then
                    TRAN_NOLabel.Text = TRAN_NO
                    insertDOC_RUNNING()
                End If
                insertOrUpdateHEAD()
                insertOrUpdateADDRESS()
                insertOrUpdateDETAIL()
                isNew = False

            Else
                If isNew Then
                    TRAN_NO = Nothing
                End If

            End If
        Else
            Call MsgBox("คุณใส่ข้อมูลไม่ครบ กรุณาตรวจสอบใหม่อีกครั้ง", 0, "พบข้อผิดพลาด")
        End If

    End Sub

    Private Sub btProdDel_Click(sender As Object, e As EventArgs) Handles btProdDel.Click
        If DetailGridView.RowCount > 0 Then
            If (MessageBox.Show("คุณต้องการที่จะลบรายการ " & DetailGridView.CurrentRow.Cells("NOTE").Value.ToString() & " ใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                DetailGridView.Rows.Remove(DetailGridView.CurrentRow)
            End If
        Else
            Call MsgBox("ยังไม่มีรายการให้ลบ", 0, "พบข้อผิดพลาด")
        End If

    End Sub



    Private Sub DETAILGridView_DataError(ByVal sender As Object, _
ByVal e As DataGridViewDataErrorEventArgs) Handles DetailGridView.DataError


        Call MsgBox("คุณใส่ข้อมูลไม่ถูกต้อง กรุณากรอกเป็นตัวเลข", 0, "พบข้อผิดพลาด")
        DETAILDataTable.Rows(e.RowIndex).Item(e.ColumnIndex) = DBNull.Value
        DetailGridView.Refresh()
    End Sub

    Private Sub DETAILGridView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DetailGridView.CellValueChanged

        If Not DetailGridView.IsHandleCreated Then
            Return
        End If

        isChanged = True
        Dim U_PRICE As Double = 0.0
        Dim U_PRICE_INC_VAT As Double = 0.0
        Dim QTY As Double = 0.0
        Dim GROSS_AMT As Double = 0.0
        Dim NET_AMT As Double = 0.0
        Dim DISC_AMT As Double = 0.0
        Dim BAL_AMT As Double = 0.0
        Dim VAT_AMT As Double = 0.0
        Dim TOTAL_AMT As Double = 0.0


        If (DetailGridView.Columns(e.ColumnIndex).Name = "QTY" Or DetailGridView.Columns(e.ColumnIndex).Name = "U_PRICE" Or DetailGridView.Columns(e.ColumnIndex).Name = "U_PRICE_INC_VAT" Or DetailGridView.Columns(e.ColumnIndex).Name = "DISC_AMT_INC_VAT") And e.RowIndex > -1 Then


            DetailGridView.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.White
            Try
                Dim price As Double = Double.Parse(DetailGridView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString())
                If price < 0 Then
                    Call MsgBox("คุณใส่ข้อมูลไม่ถูกต้อง ตัวเลขไม่สามารถเป็นค่าติดลบได้", 0, "พบข้อผิดพลาด")
                    price = 0
                End If
                If (DetailGridView.Columns(e.ColumnIndex).Name = "QTY") Then
                    DETAILDataTable.Rows(e.RowIndex).Item(e.ColumnIndex) = Format(price, "0")
                Else
                    DETAILDataTable.Rows(e.RowIndex).Item(e.ColumnIndex) = Format(price, "0.0000")
                End If

            Catch
                Call MsgBox("คุณใส่ข้อมูลไม่ถูกต้อง กรุณากรอกเป็นตัวเลข", 0, "พบข้อผิดพลาด")
                Try
                    DETAILDataTable.Rows(e.RowIndex).Item(e.ColumnIndex) = Format(0, "0.0000")
                Catch
                    Try
                        DETAILDataTable.Rows(e.RowIndex).Item(e.ColumnIndex) = Format(0, "0")
                    Catch
                        DETAILDataTable.Rows(e.RowIndex).Item(e.ColumnIndex) = DBNull.Value
                    End Try
                End Try
            End Try

            If (DetailGridView.Columns(e.ColumnIndex).Name = "DISC_AMT_INC_VAT") Then
                Try
                    Dim net As Double = Double.Parse(DetailGridView.Rows(e.RowIndex).Cells("NET_U_PRICE_INC_VAT").Value.ToString())
                    Dim disc As Double = Double.Parse(DetailGridView.Rows(e.RowIndex).Cells("DISC_AMT_INC_VAT").Value.ToString())
                    If net = 0 Then
                        Call MsgBox("คุณใส่ข้อมูลไม่ถูกต้อง กรุณากรอกราคาสินค้าก่อน", 0, "พบข้อผิดพลาด")
                        DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT_INC_VAT") = Format(0, "0.0000")
                    Else
                        If net < disc Then
                            Call MsgBox("คุณใส่ข้อมูลไม่ถูกต้อง ส่วนลดห้ามมากกว่าราคารวมสินค้า", 0, "พบข้อผิดพลาด")
                            DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT_INC_VAT") = Format(0, "0.0000")
                        End If
                    End If
                Catch
                    If String.IsNullOrEmpty(DetailGridView.Rows(e.RowIndex).Cells("NET_U_PRICE_INC_VAT").Value.ToString()) Then
                        Call MsgBox("คุณใส่ข้อมูลไม่ถูกต้อง กรุณากรอกราคาสินค้าก่อน", 0, "พบข้อผิดพลาด")
                        DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT_INC_VAT") = Format(0, "0.0000")
                    End If

                End Try
            End If

            If vatExclude.Checked Then
                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("QTY") = Double.Parse(DetailGridView.Rows(e.RowIndex).Cells("QTY").Value.ToString())
                Catch
                End Try

                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("U_PRICE_INC_VAT") = Double.Parse(DetailGridView.Rows(e.RowIndex).Cells("U_PRICE").Value.ToString()) * (100 + VAT_RATE) / 100.0
                Catch
                End Try
                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("U_PRICE_VAT") = CType(DETAILDataTable.Rows(e.RowIndex).Item("U_PRICE_INC_VAT"), Double) - CType(DETAILDataTable.Rows(e.RowIndex).Item("U_PRICE"), Double)
                Catch
                End Try

                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("NET_U_PRICE") = CType(DETAILDataTable.Rows(e.RowIndex).Item("QTY"), Double) * CType(DETAILDataTable.Rows(e.RowIndex).Item("U_PRICE"), Double)
                Catch
                End Try

                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("NET_U_PRICE_VAT") = CType(DETAILDataTable.Rows(e.RowIndex).Item("QTY"), Double) * CType(DETAILDataTable.Rows(e.RowIndex).Item("U_PRICE_VAT"), Double)
                Catch
                End Try


                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("NET_U_PRICE_INC_VAT") = CType(DETAILDataTable.Rows(e.RowIndex).Item("NET_U_PRICE"), Double) + CType(DETAILDataTable.Rows(e.RowIndex).Item("NET_U_PRICE_VAT"), Double)
                Catch
                End Try

                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT") = Double.Parse(DetailGridView.Rows(e.RowIndex).Cells("DISC_AMT_INC_VAT").Value.ToString()) * 100.0 / (100 + VAT_RATE)
                Catch
                End Try

                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT_VAT") = CType(DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT_INC_VAT"), Double) - CType(DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT"), Double)
                Catch
                End Try

                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("TOTAL") = CType(DETAILDataTable.Rows(e.RowIndex).Item("NET_U_PRICE"), Double) - CType(DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT"), Double)
                Catch
                End Try

                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("TOTAL_VAT") = CType(DETAILDataTable.Rows(e.RowIndex).Item("NET_U_PRICE_VAT"), Double) - CType(DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT_VAT"), Double)
                Catch
                End Try

                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("SUM_TOTAL") = CType(DETAILDataTable.Rows(e.RowIndex).Item("NET_U_PRICE_INC_VAT"), Double) - CType(DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT_INC_VAT"), Double)
                Catch
                End Try

            ElseIf vatInclude.Checked Then
                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("QTY") = Double.Parse(DetailGridView.Rows(e.RowIndex).Cells("QTY").Value.ToString())
                Catch
                End Try

                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("U_PRICE") = Double.Parse(DetailGridView.Rows(e.RowIndex).Cells("U_PRICE_INC_VAT").Value.ToString()) * 100.0 / (100 + VAT_RATE)
                Catch
                End Try
                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("U_PRICE_VAT") = CType(DETAILDataTable.Rows(e.RowIndex).Item("U_PRICE_INC_VAT"), Double) - CType(DETAILDataTable.Rows(e.RowIndex).Item("U_PRICE"), Double)
                Catch
                End Try

                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("NET_U_PRICE") = CType(DETAILDataTable.Rows(e.RowIndex).Item("QTY"), Double) * CType(DETAILDataTable.Rows(e.RowIndex).Item("U_PRICE"), Double)
                Catch
                End Try

                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("NET_U_PRICE_VAT") = CType(DETAILDataTable.Rows(e.RowIndex).Item("QTY"), Double) * CType(DETAILDataTable.Rows(e.RowIndex).Item("U_PRICE_VAT"), Double)
                Catch
                End Try


                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("NET_U_PRICE_INC_VAT") = CType(DETAILDataTable.Rows(e.RowIndex).Item("NET_U_PRICE"), Double) + CType(DETAILDataTable.Rows(e.RowIndex).Item("NET_U_PRICE_VAT"), Double)
                Catch
                End Try

                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT") = Double.Parse(DetailGridView.Rows(e.RowIndex).Cells("DISC_AMT_INC_VAT").Value.ToString()) * 100.0 / (100 + VAT_RATE)
                Catch
                End Try

                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT_VAT") = CType(DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT_INC_VAT"), Double) - CType(DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT"), Double)
                Catch
                End Try

                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("TOTAL") = CType(DETAILDataTable.Rows(e.RowIndex).Item("NET_U_PRICE"), Double) - CType(DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT"), Double)
                Catch
                End Try

                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("TOTAL_VAT") = CType(DETAILDataTable.Rows(e.RowIndex).Item("NET_U_PRICE_VAT"), Double) - CType(DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT_VAT"), Double)
                Catch
                End Try

                Try
                    DETAILDataTable.Rows(e.RowIndex).Item("SUM_TOTAL") = CType(DETAILDataTable.Rows(e.RowIndex).Item("NET_U_PRICE_INC_VAT"), Double) - CType(DETAILDataTable.Rows(e.RowIndex).Item("DISC_AMT_INC_VAT"), Double)
                Catch
                End Try


            ElseIf nonVat.Checked Then

            End If
        End If
        calculateSum()
        DetailGridView.Refresh()
        DetailGridView.Update()
    End Sub

    Private Function doubleToStringFormat(input As Double) As String
        Return Format(input, "0.00")
    End Function

    Private Sub calculateSum()

        For Each columnName As String In numberColumns
            Try
                HEADDataTable.Rows(0).Item(columnName) = 0
            Catch ex As Exception
            End Try

            For i As Integer = 0 To DetailGridView.Rows.Count
                Try
                    HEADDataTable.Rows(0).Item(columnName) = (CType(HEADDataTable.Rows(0).Item(columnName), Double) + CType(DETAILDataTable.Rows(i).Item(columnName), Double))
                Catch ex As Exception
                End Try
            Next
        Next
        Try
            U_PRICE = CType(HEADDataTable.Rows(0).Item("U_PRICE"), Double)
            U_PRICE_VAT = CType(HEADDataTable.Rows(0).Item("U_PRICE_VAT"), Double)
            U_PRICE_INC_VAT = CType(HEADDataTable.Rows(0).Item("U_PRICE_INC_VAT"), Double)
        Catch ex As Exception

        End Try
        Try
            NET_U_PRICE = CType(HEADDataTable.Rows(0).Item("NET_U_PRICE"), Double)
            NET_U_PRICE_VAT = CType(HEADDataTable.Rows(0).Item("NET_U_PRICE_VAT"), Double)
            NET_U_PRICE_INC_VAT = CType(HEADDataTable.Rows(0).Item("NET_U_PRICE_INC_VAT"), Double)
        Catch ex As Exception

        End Try
        Try
            DISC_AMT = CType(HEADDataTable.Rows(0).Item("DISC_AMT"), Double)
            DISC_AMT_VAT = CType(HEADDataTable.Rows(0).Item("DISC_AMT_VAT"), Double)
            DISC_AMT_INC_VAT = CType(HEADDataTable.Rows(0).Item("DISC_AMT_INC_VAT"), Double)
        Catch ex As Exception

        End Try
        Try
            TOTAL = CType(HEADDataTable.Rows(0).Item("TOTAL"), Double)
            TOTAL_VAT = CType(HEADDataTable.Rows(0).Item("TOTAL_VAT"), Double)
            SUM_TOTAL = CType(HEADDataTable.Rows(0).Item("SUM_TOTAL"), Double)
        Catch ex As Exception

        End Try


        Try
            GRAND_AMT = SUM_TOTAL - TOP_DISC_AMT

            HEADDataTable.Rows(0).Item("GRAND_AMT") = GRAND_AMT

        Catch ex As Exception

        End Try



        showSum()

    End Sub

    Private Sub showSum()


        If Not CheckBox1.Checked Then
            SumLabel.Text = Format(TOTAL, "#,##0.0000")
            TopUpDiscountLabel.Text = Format(TOP_DISC_AMT, "#,##0.0000")
            VatLabel.Text = Format(TOTAL_VAT, "#,##0.0000")
            GrandTotalLabel.Text = Format(GRAND_AMT, "#,##0.0000")
        Else
            SumLabel.Text = Format((TOTAL * ex_rate), "#,##0.0000")
            TopUpDiscountLabel.Text = Format((TOP_DISC_AMT * ex_rate), "#,##0.0000")
            VatLabel.Text = Format((TOTAL_VAT * ex_rate), "#,##0.0000")
            GrandTotalLabel.Text = Format((GRAND_AMT * ex_rate), "#,##0.0000")
        End If

        Try
            NumberReadingLabel.Text = NumberHelper.NumberToThaiWord(GRAND_AMT)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub pickAR(sender As Object, e As EventArgs)
        Dim f As New frmFINAR_MEMBER
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Dim selectedRow As DataGridViewRow = f.DataGridView1.CurrentRow
            Dim docSelectedRow As DataGridViewRow = f.DocAddrGrid.CurrentRow
            Dim mailSelectedRow As DataGridViewRow = f.MailAddrGrid.CurrentRow

            AR_CODETextBox.Text = selectedRow.Cells("COMP_PERSON_CODE").Value.ToString()
            AR_NAMETextBox.Text = selectedRow.Cells("COMP_PERSON_NAME_TH").Value.ToString()
            AR_NAME_ENTextBox.Text = selectedRow.Cells("COMP_PERSON_NAME_EN").Value.ToString()
            TAX_NOTextBox.Text = selectedRow.Cells("TAX_ID").Value.ToString()
            MEMBER_CODETextBox.Text = selectedRow.Cells("MEMBER_CODE").Value.ToString()
            resetAddress()
            Try
                POST_CODETextBox.Text = docSelectedRow.Cells("ADDR_POSTCODE").Value.ToString()

                ADDR1_TH.Text = processAddressTH(docSelectedRow)
                ADDR1_EN.Text = processAddressEN(docSelectedRow)

                TELEPHONETextBox.Text = docSelectedRow.Cells("ADDR_TELEPHONE").Value.ToString()
                FAXTextBox.Text = docSelectedRow.Cells("ADDR_FAX").Value.ToString()
            Catch ex As Exception
                'resetAddress()
            End Try

            Try
                POST_CODETextBox2.Text = mailSelectedRow.Cells("ADDR_POSTCODE").Value.ToString()
                ATTN_NAME_THTextBox2.Text = "ฝ่ายบัญชีและการเงิน"
                ATTN_NAME_ENTextBox2.Text = "Finance and Accounting"

                ADDR1_TH2.Text = processAddressTH(mailSelectedRow)
                ADDR1_EN2.Text = processAddressEN(mailSelectedRow)

                TELEPHONETextBox2.Text = mailSelectedRow.Cells("ADDR_TELEPHONE").Value.ToString()
                FAXTextBox2.Text = mailSelectedRow.Cells("ADDR_FAX").Value.ToString()
            Catch ex As Exception
                'resetAddress()
            End Try

        End If
        f.Dispose()
        f = Nothing
    End Sub
    Private Sub resetAddress()
        ATTN_NAME_THTextBox2.Text = ""
        ATTN_NAME_ENTextBox2.Text = ""
        POST_CODETextBox.Text = ""
        POST_CODETextBox2.Text = ""
        ADDR1_TH.Text = ""
        ADDR1_EN.Text = ""
        ADDR1_TH2.Text = ""
        ADDR1_EN2.Text = ""
        TELEPHONETextBox.Text = ""
        TELEPHONETextBox2.Text = ""
        FAXTextBox.Text = ""
        FAXTextBox2.Text = ""
    End Sub

    Private Function validateAddress() As Boolean
        Dim retVal As Boolean = True


        If thai.Checked Then
            If String.IsNullOrEmpty(ATTN_NAME_THTextBox2.Text.Trim()) Then
                ATTN_NAME_THTextBox2.BackColor = ColorTranslator.FromHtml("#FFCCCC")
                retVal = False
            End If
            If String.IsNullOrEmpty(ADDR1_TH.Text.Trim()) Then
                ADDR1_TH.BackColor = ColorTranslator.FromHtml("#FFCCCC")
                retVal = False
            End If
            If String.IsNullOrEmpty(ADDR1_TH2.Text.Trim()) Then
                ADDR1_TH2.BackColor = ColorTranslator.FromHtml("#FFCCCC")
                retVal = False
            End If

        ElseIf eng.Checked Then
            If String.IsNullOrEmpty(ATTN_NAME_ENTextBox2.Text.Trim()) Then
                ATTN_NAME_ENTextBox2.BackColor = ColorTranslator.FromHtml("#FFCCCC")
                retVal = False
            End If
            If String.IsNullOrEmpty(ADDR1_EN.Text.Trim()) Then
                ADDR1_EN.BackColor = ColorTranslator.FromHtml("#FFCCCC")
                retVal = False
            End If
            If String.IsNullOrEmpty(ADDR1_EN2.Text.Trim()) Then
                ADDR1_EN2.BackColor = ColorTranslator.FromHtml("#FFCCCC")
                retVal = False
            End If
        End If
        Return retVal
    End Function

    Private Function processAddressTH(addressRow As DataGridViewRow) As String
        Return _
            (addressRow.Cells("ADDR_MOO").Value.ToString() & " " & _
            addressRow.Cells("ADDR_SOI").Value.ToString() & " " & _
            addressRow.Cells("ADDR_ROAD").Value.ToString() & " " & _
            addressRow.Cells("ADDR_SUB_DISTRICT").Value.ToString() & " " & _
            addressRow.Cells("ADDR_DISTRICT").Value.ToString() & " " & _
            addressRow.Cells("ADDR_PROVINCE_NAME").Value.ToString() & " " & _
            addressRow.Cells("ADDR_POSTCODE").Value.ToString()).Trim()
    End Function

    Private Function processAddressEN(addressRow As DataGridViewRow) As String
        Return _
            (addressRow.Cells("ADDR_MOO_EN").Value.ToString() & " " & _
            addressRow.Cells("ADDR_SOI_EN").Value.ToString() & " " & _
            addressRow.Cells("ADDR_ROAD_EN").Value.ToString() & " " & _
            addressRow.Cells("ADDR_SUB_DISTRICT_EN").Value.ToString() & " " & _
            addressRow.Cells("ADDR_DISTRICT_EN").Value.ToString() & " " & _
            addressRow.Cells("ADDR_PROVINCE_NAME_EN").Value.ToString() & " " & _
            addressRow.Cells("ADDR_POSTCODE").Value.ToString()).Trim()
    End Function

    Private Sub ProductDataGridView_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs)
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


    Private Sub getHEAD()
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = String.Empty
        query &= "SELECT  TOP 1 * "
        query &= "FROM            PN_HEAD LEFT JOIN MB_COMP_PERSON ON PN_HEAD.AR_CODE = MB_COMP_PERSON.COMP_PERSON_CODE LEFT JOIN MB_PRENAME ON MB_COMP_PERSON.PREN_CODE = MB_PRENAME.PRENAME_CODE "
        query &= "WHERE           TRAN_NO = @p0"

        parameters.Add("@p0", TRAN_NO)

        HEADDataTable = fillWebSQL(query, parameters, "PN_HEAD")
        If isNew Then
            HEADDataTable.Rows.Add()

            For Each columnName As String In numberColumns
                Try
                    HEADDataTable.Rows(0).Item(columnName) = 0
                Catch ex As Exception
                End Try
            Next

            Try
                'VAT_RATE = CDec(VAT_RATEComboBox.SelectedValue)
            Catch ex As Exception

            End Try

            TAX_RATELabel.Text = Format(VAT_RATE, "0.00")
            Return
        End If
        If String.IsNullOrEmpty(HEADDataTable.Rows(0).Item("CANCEL_FLAG").ToString()) Then
            DocumentStatusLabel.Text = "ปกติ"
        Else
            DocumentStatusLabel.Text = "ยกเลิกหรืออยู่ในขั้นตอนการยกเลิก"
        End If


        PrintStatusLabel.Text = HEADDataTable.Rows(0).Item("PRINT_NUM").ToString()
        Try
            TRAN_DATEPicker.Value = Date.Parse(HEADDataTable.Rows(0).Item("TRAN_DATE").ToString())
        Catch

        End Try

        Try
            DUE_DATEPicker.Value = Date.Parse(HEADDataTable.Rows(0).Item("DUE_DATE").ToString())
        Catch

        End Try
        Try
            VAT_RATE = CDec(HEADDataTable.Rows(0).Item("VAT_RATE"))
        Catch ex As Exception

        End Try
        CR_TERMTextBox.Text = HEADDataTable.Rows(0).Item("CR_TERM").ToString()
        AR_CODETextBox.Text = HEADDataTable.Rows(0).Item("AR_CODE").ToString()
        MEMBER_CODETextBox.Text = HEADDataTable.Rows(0).Item("MB_MEMBER_CODE").ToString()
        AR_NAMETextBox.Text = HEADDataTable.Rows(0).Item("PRENAME_FORMAT_TH").ToString().Replace("{0}", HEADDataTable.Rows(0).Item("COMP_PERSON_NAME_TH").ToString())
        AR_NAME_ENTextBox.Text = HEADDataTable.Rows(0).Item("PRENAME_FORMAT_EN").ToString().Replace("{0}", HEADDataTable.Rows(0).Item("COMP_PERSON_NAME_EN").ToString())
        NOTETextBox.Text = HEADDataTable.Rows(0).Item("NOTE").ToString()
        INNER_NOTETextBox.Text = HEADDataTable.Rows(0).Item("INNER_NOTE").ToString()
        Dim VATType As String = HEADDataTable.Rows(0).Item("VAT_TYPE").ToString()

        setVATType(VATType)

    End Sub

    Private Sub getADDRESS()
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = String.Empty
        query &= "SELECT  TOP 2 * "
        query &= "FROM            PN_ADDRESS "
        query &= "WHERE           TRAN_NO = @p0 ORDER BY ADDR_SEQ "
        parameters.Add("@p0", TRAN_NO)
        ADDRESSDataTable = fillWebSQL(query, parameters, "PN_ADDRESS")



        ADDR1_EN.Text = ADDRESSDataTable.Rows(0).Item("ADDR1_EN").ToString()


        ADDR1_TH.Text = ADDRESSDataTable.Rows(0).Item("ADDR1_TH").ToString()

        POST_CODETextBox.Text = ADDRESSDataTable.Rows(0).Item("POST_CODE").ToString()
        TELEPHONETextBox.Text = ADDRESSDataTable.Rows(0).Item("TELEPHONE").ToString()
        FAXTextBox.Text = ADDRESSDataTable.Rows(0).Item("FAX").ToString()

        ATTN_NAME_ENTextBox2.Text = ADDRESSDataTable.Rows(1).Item("ATTN_NAME_EN").ToString()
        ADDR1_EN2.Text = ADDRESSDataTable.Rows(1).Item("ADDR1_EN").ToString()

        ATTN_NAME_THTextBox2.Text = ADDRESSDataTable.Rows(1).Item("ATTN_NAME_TH").ToString()
        ADDR1_TH2.Text = ADDRESSDataTable.Rows(1).Item("ADDR1_TH").ToString()

        POST_CODETextBox2.Text = ADDRESSDataTable.Rows(1).Item("POST_CODE").ToString()
        TELEPHONETextBox2.Text = ADDRESSDataTable.Rows(1).Item("TELEPHONE").ToString()
        FAXTextBox2.Text = ADDRESSDataTable.Rows(1).Item("FAX").ToString()

    End Sub

    Private Sub getDETAIL()
        Dim query As String = "SELECT * FROM PN_DETAIL INNER JOIN IV_SUB_SECTION ON PN_DETAIL.SUB_SECTION_CODE = IV_SUB_SECTION.SUB_SECTION_CODE INNER JOIN SU_DIVISION ON IV_SUB_SECTION.DIV_CODE_INC=SU_DIVISION.DIV_CODE  WHERE TRAN_NO = @p0 ORDER BY SEQ"
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", TRAN_NO)
        DETAILDataTable = fillWebSQL(query, parameters, "PN_DETAIL")
        DetailGridView.DataSource = DETAILDataTable
        DetailGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DetailGridView.AutoResizeColumns()
        DetailGridView.Refresh()
        DetailGridView.Update()

    End Sub
    Private Sub insertOrUpdateHEAD()
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = "DELETE FROM PN_HEAD WHERE TRAN_NO = @p0"
        parameters.Add("@p0", TRAN_NO)
        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        Dim pn As New PN_HEAD
        pn.OU_CODE = OU_CODE
        If isNew Then
            pn.DIV_CODE = user_div
            pn.CR_BY = user_name
            pn.CR_DATE = DateTime.Now
        Else
            pn.DIV_CODE = HEADDataTable.Rows(0).Item("DIV_CODE").ToString()
        End If

        pn.TRAN_NO = TRAN_NO
        pn.TRAN_TYPE = "PN"
        Try
            pn.TRAN_DATE = TRAN_DATEPicker.Value
        Catch ex As Exception

        End Try

        Try
            pn.DUE_DATE = DUE_DATEPicker.Value
        Catch ex As Exception

        End Try
        Try
            pn.CR_TERM = Integer.Parse(CR_TERMTextBox.Text)
        Catch ex As Exception

        End Try

        Try
            pn.PRINT_NUM = Integer.Parse(PrintStatusLabel.Text)
        Catch ex As Exception

        End Try
        pn.UPD_BY = user_name
        pn.UPD_DATE = DateTime.Now
        pn.AR_CODE = AR_CODETextBox.Text
        pn.MB_MEMBER_CODE = MEMBER_CODETextBox.Text
        pn.VAT_TYPE = getVATType()
        pn.VAT_RATE = VAT_RATE

        pn.U_PRICE = U_PRICE
        pn.U_PRICE_VAT = U_PRICE_VAT
        pn.U_PRICE_INC_VAT = U_PRICE_INC_VAT

        pn.NET_U_PRICE = NET_U_PRICE
        pn.NET_U_PRICE_VAT = NET_U_PRICE_VAT
        pn.NET_U_PRICE_INC_VAT = NET_U_PRICE_INC_VAT

        pn.DISC_AMT = DISC_AMT
        pn.DISC_AMT_VAT = DISC_AMT_VAT
        pn.DISC_AMT_INC_VAT = DISC_AMT_INC_VAT

        pn.TOTAL = TOTAL
        pn.TOTAL_VAT = TOTAL_VAT
        pn.SUM_TOTAL = SUM_TOTAL

        pn.TOP_DISC_AMT = TOP_DISC_AMT
        pn.GRAND_AMT = GRAND_AMT
        pn.GRAND_AMT_VAT = GRAND_AMT_VAT
        pn.GRAND_AMT_INC_VAT = pn.GRAND_AMT_INC_VAT

        pn.BAL_AMT = GRAND_AMT_INC_VAT

        pn.NOTE = NOTETextBox.Text
        pn.INNER_NOTE = INNER_NOTETextBox.Text
        pn.TAX_ID = TAX_NOTextBox.Text
        pn.EX_RATE = ex_rate
        pn.CURR_CODE = CurrencyComboBox.Text

        QueryHelper.insertModel("PN_HEAD", pn)
        getHEAD()


    End Sub

    Private Sub insertOrUpdateADDRESS()

        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = "DELETE FROM PN_ADDRESS WHERE TRAN_NO = @p0"
        parameters.Add("@p0", TRAN_NO)

        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        Dim pn As New PN_ADDRESS

        If isNew Then
            pn.CR_BY = user_name
            pn.CR_DATE = DateTime.Now
        End If

        pn.TRAN_NO = TRAN_NO
        pn.AR_CODE = AR_CODETextBox.Text
        pn.ADDR_SEQ = 1
        pn.ADDR1_EN = ADDR1_EN.Text
        pn.ADDR1_TH = ADDR1_TH.Text
        pn.ATTN_NAME_EN = "NO"
        pn.ATTN_NAME_TH = "NO"
        pn.POST_CODE = POST_CODETextBox.Text
        pn.TELEPHONE = TELEPHONETextBox.Text
        pn.FAX = FAXTextBox.Text
        pn.UPD_BY = user_name
        pn.UPD_DATE = DateTime.Now
        pn.AR_CODE = AR_CODETextBox.Text

        QueryHelper.insertModel("PN_ADDRESS", pn)
        pn.ADDR_SEQ = 2
        pn.ADDR1_EN = ADDR1_EN2.Text
        pn.ADDR1_TH = ADDR1_TH2.Text
        pn.ATTN_NAME_EN = ATTN_NAME_ENTextBox2.Text
        pn.ATTN_NAME_TH = ATTN_NAME_THTextBox2.Text
        pn.POST_CODE = POST_CODETextBox2.Text
        pn.TELEPHONE = TELEPHONETextBox2.Text
        pn.FAX = FAXTextBox2.Text
        pn.UPD_BY = user_name
        pn.UPD_DATE = DateTime.Now
        pn.AR_CODE = AR_CODETextBox.Text

        QueryHelper.insertModel("PN_ADDRESS", pn)

    End Sub

    Private Sub insertOrUpdateDETAIL()
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = "DELETE FROM PN_DETAIL WHERE TRAN_NO = @p0"
        parameters.Add("@p0", TRAN_NO)

        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        For Each row As DataRow In DETAILDataTable.Rows
            If String.IsNullOrEmpty(row.Item("TRAN_NO").ToString()) Then
                row.Item("TRAN_NO") = TRAN_NO
                Dim pnD As PN_DETAIL = CType(ModelHelper.convertDataRowToModel(New PN_DETAIL, row), PN_DETAIL)
                QueryHelper.insertModel("PN_DETAIL", pnD)
            Else
                Dim pnD As PN_DETAIL = CType(ModelHelper.convertDataRowToModel(New PN_DETAIL, row), PN_DETAIL)
                QueryHelper.insertModel("PN_DETAIL", pnD)
            End If
        Next

    End Sub

    Private Sub insertDOC_RUNNING()
        Dim docRunning = New DOC_RUNNING
        docRunning.OU_CODE = OU_CODE
        docRunning.DIV_CODE = user_div
        docRunning.BUDGET_YEAR = Integer.Parse(DateTime.Now.ToString("yyyy", New System.Globalization.CultureInfo("th-TH").DateTimeFormat))
        docRunning.PERIOD = 0
        docRunning.SUB_TYPE = "PN1"
        docRunning.DOC_RUNNING_NO = TRAN_NO
        docRunning.CR_BY = user_name
        docRunning.CR_DATE = DateTime.Now
        QueryHelper.insertModel("PN_DOC_RUNNING", docRunning)
    End Sub

    Private Sub DETAILGridView_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs)
        calculateSum()
    End Sub


    Private Sub DETAILGridView_KeyDown(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter Then
            DetailGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
            'btProdAdd_Click(sender, e)
        End If
    End Sub

    Private Sub CurrencyComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CurrencyComboBox.SelectedIndexChanged
        currLabel1.Text = CurrencyComboBox.Text
        currLabel2.Text = CurrencyComboBox.Text
        currLabel3.Text = CurrencyComboBox.Text
        currLabel4.Text = CurrencyComboBox.Text
        currLabel5.Text = CurrencyComboBox.Text
        isChanged = True

        If CurrencyComboBox.SelectedIndex > 0 Then
            CheckBox1.Visible = True
            EX_RATETextBox.Visible = True
        Else
            CheckBox1.Visible = False
            EX_RATETextBox.Visible = False
            ex_rate = 1
            EX_RATETextBox.Text = "1"
            CheckBox1.Checked = False

        End If
    End Sub
    Private Sub setVATType(inpVATType As String)
        Try
            vatType = inpVATType
            DETAILDataTable.Rows.Clear()
            If inpVATType = "I" Then
                vatInclude.Checked = True
                With DetailGridView
                    .Refresh()
                    .Columns("U_PRICE").ReadOnly = True
                    .Columns("U_PRICE_INC_VAT").ReadOnly = False
                    .Columns("U_PRICE").DefaultCellStyle.BackColor = Color.FromName("ControlLight")
                    .Columns("U_PRICE_INC_VAT").DefaultCellStyle.BackColor = Color.White
                End With

            ElseIf inpVATType = "E" Then
                vatExclude.Checked = True
                With DetailGridView
                    .Refresh()
                    .Columns("U_PRICE").ReadOnly = False
                    .Columns("U_PRICE_INC_VAT").ReadOnly = True
                    .Columns("U_PRICE").DefaultCellStyle.BackColor = Color.White
                    .Columns("U_PRICE_INC_VAT").DefaultCellStyle.BackColor = Color.FromName("ControlLight")
                End With

            ElseIf inpVATType = "N" Then
                nonVat.Checked = True
                With DetailGridView
                    .Refresh()
                    .Columns("U_PRICE").ReadOnly = False
                    .Columns("U_PRICE_INC_VAT").ReadOnly = True
                    .Columns("U_PRICE").DefaultCellStyle.BackColor = Color.White
                    .Columns("U_PRICE_INC_VAT").DefaultCellStyle.BackColor = Color.FromName("ControlLight")
                End With

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function getVATType() As String
        If vatInclude.Checked Then Return "I"
        If vatExclude.Checked Then Return "E"
        If nonVat.Checked Then Return "N"
        Return "X"
    End Function

    Private Sub DETAILGridView_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DetailGridView.CellLeave
        DetailGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub vatInclude_MouseUp(sender As Object, e As MouseEventArgs) Handles vatInclude.MouseUp
        If ((vatType <> "I") And (Not String.IsNullOrEmpty(vatType))) Then
            If (MessageBox.Show("ข้อมูลรายการจะถูกล้าง คุณต้องการที่จะเปลี่ยนชนิตของภาษีมูลค่าเพิ่มใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                setVATType("I")
            End If

        End If
    End Sub

    Private Sub vatExclude_MouseUp(sender As Object, e As MouseEventArgs) Handles vatExclude.MouseUp
        If ((vatType <> "E") And (Not String.IsNullOrEmpty(vatType))) Then
            If (MessageBox.Show("ข้อมูลรายการจะถูกล้าง คุณต้องการที่จะเปลี่ยนชนิตของภาษีมูลค่าเพิ่มใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                setVATType("E")
            End If

        End If
    End Sub

    Private Sub nonVat_MouseUp(sender As Object, e As MouseEventArgs) Handles nonVat.MouseUp
        If ((vatType <> "N") And (Not String.IsNullOrEmpty(vatType))) Then
            If (MessageBox.Show("ข้อมูลรายการจะถูกล้าง คุณต้องการที่จะเปลี่ยนชนิตของภาษีมูลค่าเพิ่มใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                setVATType("N")
            End If
        End If
    End Sub

    Private Function validatePN_DETAIL() As Boolean
        Dim retVal As Boolean = True

        For Each row As DataGridViewRow In DetailGridView.Rows
            If String.IsNullOrEmpty(row.Cells("QTY").Value.ToString()) Then
                retVal = False
                row.Cells("QTY").Style.BackColor = ColorTranslator.FromHtml("#FFCCCC")
            ElseIf String.IsNullOrEmpty(row.Cells("U_PRICE").Value.ToString()) And getVATType() = "E" Then
                retVal = False
                row.Cells("U_PRICE").Style.BackColor = ColorTranslator.FromHtml("#FFCCCC")
            ElseIf String.IsNullOrEmpty(row.Cells("U_PRICE_INC_VAT").Value.ToString()) And getVATType() = "I" Then
                retVal = False
                row.Cells("U_PRICE_INC_VAT").Style.BackColor = ColorTranslator.FromHtml("#FFCCCC")
            ElseIf String.IsNullOrEmpty(row.Cells("DISC_AMT").Value.ToString()) Then
                retVal = False
                row.Cells("DISC_AMT_INC_VAT").Style.BackColor = ColorTranslator.FromHtml("#FFCCCC")
            End If
        Next
        DetailGridView.Refresh()
        DetailGridView.Update()
        Return retVal
    End Function

    Private Sub AR_NAMETextBox_KeyUp(sender As Object, e As KeyEventArgs) Handles AR_NAMETextBox.KeyUp
        If e.KeyCode = Keys.Enter Then
            pickAR(sender, e)
        End If
    End Sub

    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click

        If isNew Then
            Call MsgBox("คุณยังไม่ได้บันทึกเอกสารไม่สามารถพิมพ์ได้", 0, "พบข้อผิดพลาด")
        Else
            Dim printNum As Integer = 0
            Try
                PrintStatusLabel.Text = (Integer.Parse(PrintStatusLabel.Text) + 1).ToString
                insertOrUpdateHEAD()
                getHEAD()
            Catch ex As Exception

            End Try

            Dim param As New Dictionary(Of String, String)
            param.Add("TRAN_NO", TRAN_NO)

            Dim f As New frmMainReports
            f.reportPath = getParameters(5, "PN_REPORT")
            f.reportParameters = param
            If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                '
            End If
            f.Dispose()
            f = Nothing
        End If
    End Sub


    Private Sub CR_TERMTextBox_TextChanged(sender As Object, e As EventArgs) Handles CR_TERMTextBox.TextChanged
        If Not CType(sender, MaskedTextBox).IsHandleCreated Then Return
        isChanged = True
        Try
            DUE_DATEPicker.Value = TRAN_DATEPicker.Value.AddDays(Integer.Parse(CR_TERMTextBox.Text))
        Catch ex As Exception

        End Try
    End Sub


    Private Sub frmFINPaymentNotice_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not isLockAll Then
            If isNew Then
                If (MessageBox.Show("การแก้ไขที่ยังไม่ได้บันทึกจะไม่ได้รับการบันทึก คุณต้องการที่จะปิดหน้าต่างนี้ ใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No) Then
                    e.Cancel = True
                End If
            Else
                If (MessageBox.Show("การแก้ไขใบแจ้งชำระยังไม่ถูกบันทึก คุณต้องการที่จะปิดหน้าต่างนี้ ใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No) Then
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub AR_NAMETextBox_TextChanged(sender As Object, e As EventArgs) Handles AR_NAMETextBox.TextChanged
        If Not CType(sender, TextBox).IsHandleCreated Then Return
        AR_NAMEToolTip.SetToolTip(AR_NAMETextBox, AR_NAMETextBox.Text)
    End Sub

    Private Sub AR_NAME_ENTextBox_TextChanged(sender As Object, e As EventArgs) Handles AR_NAME_ENTextBox.TextChanged
        If Not CType(sender, TextBox).IsHandleCreated Then Return
        AR_NAME_ENToolTip.SetToolTip(AR_NAME_ENTextBox, AR_NAME_ENTextBox.Text)
    End Sub

    Private Sub SelectARButton_Click(sender As Object, e As EventArgs) Handles SelectARButton.Click
        pickAR(sender, e)
    End Sub

    Private Sub lockNonAdminInput()
        AR_NAMETextBox.ReadOnly = True
        AR_NAME_ENTextBox.ReadOnly = True
        vatInclude.Enabled = False
        vatExclude.Enabled = False
        nonVat.Enabled = False

        Try
            DetailGridView.Columns("NOTE").ReadOnly = True
            DetailGridView.Columns("NOTE").DefaultCellStyle.BackColor = Color.FromName("ControlLight")
            DetailGridView.Columns("NOTE_EN").ReadOnly = True
            DetailGridView.Columns("NOTE_EN").DefaultCellStyle.BackColor = Color.FromName("ControlLight")
        Catch
        End Try

    End Sub

    Public Sub LockAllInput()
        'Get the first control.
        Dim ctl As Control = Me.GetNextControl(Me, True)

        While Not ctl Is Nothing
            If TypeOf ctl Is TextBox Then
                DirectCast(ctl, TextBox).ReadOnly = True
            End If
            If TypeOf ctl Is RichTextBox Then
                DirectCast(ctl, RichTextBox).ReadOnly = True
            End If
            If TypeOf ctl Is MaskedTextBox Then
                DirectCast(ctl, MaskedTextBox).ReadOnly = True
            End If
            If TypeOf ctl Is RadioButton Then
                DirectCast(ctl, RadioButton).Enabled = False
            End If
            If TypeOf ctl Is ComboBox Then
                DirectCast(ctl, ComboBox).Enabled = False
            End If
            If TypeOf ctl Is Button Then
                DirectCast(ctl, Button).Enabled = False
            End If
            If TypeOf ctl Is DataGridView Then
                DirectCast(ctl, DataGridView).Enabled = False
            End If

            'Get the next control.
            ctl = Me.GetNextControl(ctl, True)
        End While
        isLockAll = True
        btnClose.Enabled = True
        PrintButton.Enabled = True
    End Sub


    Private Sub EX_RATETextBox_TextChanged(sender As Object, e As EventArgs) Handles EX_RATETextBox.TextChanged
        If Not CType(sender, TextBox).IsHandleCreated Then Return
        Try
            Dim exrate As Double = Double.Parse(EX_RATETextBox.Text)
            If exrate > 0 Then
                ex_rate = exrate
                calculateSum()

            Else
                Call MsgBox("กรุณากรอกข้อมูลเป็นตัวเลข", 0, "พบข้อผิดพลาด")
                EX_RATETextBox.Text = "1"
            End If
        Catch ex As Exception
            Call MsgBox("กรุณากรอกข้อมูลเป็นตัวเลข", 0, "พบข้อผิดพลาด")
            EX_RATETextBox.Text = "1"
        End Try
    End Sub

    Private Sub NOTETextBox_TextChanged(sender As Object, e As EventArgs) Handles NOTETextBox.TextChanged
        If Not CType(sender, TextBox).IsHandleCreated Then Return
        isChanged = True
    End Sub

    Private Sub INNER_NOTETextBox_TextChanged(sender As Object, e As EventArgs) Handles INNER_NOTETextBox.TextChanged
        If Not CType(sender, TextBox).IsHandleCreated Then Return
        isChanged = True
    End Sub

    Private Sub VAT_RATEComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles VAT_RATEComboBox.SelectedIndexChanged
        isChanged = True
        VatLabel.Text = Format(VAT_RATE, "0.00")
    End Sub

    Private Sub CR_TERMTextBox_KeyUp(sender As Object, e As KeyEventArgs) Handles CR_TERMTextBox.KeyUp
        If e.KeyCode = Keys.Enter Then
            CR_TERMTextBox.ValidateText()
            SelectARButton.Select()
        End If
    End Sub

    Private Sub ATTN_NAME_THTextBox2_TextChanged(sender As Object, e As EventArgs) Handles ATTN_NAME_THTextBox2.TextChanged
        If Not CType(sender, TextBox).IsHandleCreated Then Return
        isChanged = True
        ATTN_NAME_THTextBox2.BackColor = Color.White
    End Sub

    Private Sub ATTN_NAME_ENTextBox2_TextChanged(sender As Object, e As EventArgs) Handles ATTN_NAME_ENTextBox2.TextChanged
        If Not CType(sender, TextBox).IsHandleCreated Then Return
        isChanged = True
        ATTN_NAME_ENTextBox2.BackColor = Color.White
    End Sub

    Private Sub ADDR1_TH_TextChanged(sender As Object, e As EventArgs) Handles ADDR1_TH.TextChanged
        If Not CType(sender, TextBox).IsHandleCreated Then Return
        isChanged = True
        ADDR1_TH.BackColor = Color.White
    End Sub

    Private Sub ADDR1_EN_TextChanged(sender As Object, e As EventArgs) Handles ADDR1_EN.TextChanged
        If Not CType(sender, TextBox).IsHandleCreated Then Return
        isChanged = True
        ADDR1_EN.BackColor = Color.White
    End Sub

    Private Sub ADDR1_TH2_TextChanged(sender As Object, e As EventArgs) Handles ADDR1_TH2.TextChanged
        If Not CType(sender, TextBox).IsHandleCreated Then Return
        isChanged = True
        ADDR1_TH2.BackColor = Color.White
    End Sub

    Private Sub ADDR1_EN2_TextChanged(sender As Object, e As EventArgs) Handles ADDR1_EN2.TextChanged
        If Not CType(sender, TextBox).IsHandleCreated Then Return
        isChanged = True
        ADDR1_EN2.BackColor = Color.White
    End Sub

    Private Sub TELEPHONETextBox_TextChanged(sender As Object, e As EventArgs) Handles TELEPHONETextBox.TextChanged
        If Not CType(sender, TextBox).IsHandleCreated Then Return
        isChanged = True
        TELEPHONETextBox.BackColor = Color.White
    End Sub

    Private Sub TELEPHONETextBox2_TextChanged(sender As Object, e As EventArgs) Handles TELEPHONETextBox2.TextChanged
        If Not CType(sender, TextBox).IsHandleCreated Then Return
        isChanged = True
        TELEPHONETextBox2.BackColor = Color.White
    End Sub

    Private Sub FAXTextBox_TextChanged(sender As Object, e As EventArgs) Handles FAXTextBox.TextChanged
        If Not CType(sender, TextBox).IsHandleCreated Then Return
        isChanged = True
        FAXTextBox.BackColor = Color.White
    End Sub

    Private Sub FAXTextBox2_TextChanged(sender As Object, e As EventArgs) Handles FAXTextBox2.TextChanged
        If Not CType(sender, TextBox).IsHandleCreated Then Return
        isChanged = True
        FAXTextBox2.BackColor = Color.White
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        EX_RATETextBox.Enabled = CheckBox1.Checked

        If CheckBox1.Checked Then
            currLabel1.Text = "บาท"
            currLabel2.Text = "บาท"
            currLabel3.Text = "บาท"
            currLabel4.Text = "บาท"
            currLabel5.Text = "บาท"
        Else
            currLabel1.Text = CurrencyComboBox.Text
            currLabel2.Text = CurrencyComboBox.Text
            currLabel3.Text = CurrencyComboBox.Text
            currLabel4.Text = CurrencyComboBox.Text
            currLabel5.Text = CurrencyComboBox.Text
            EX_RATETextBox.Text = "1"
            ex_rate = 1
        End If
    End Sub

    Private Sub thai_MouseUp(sender As Object, e As MouseEventArgs) Handles thai.MouseUp
        TabControl1.SelectedIndex = 0
        TabControl2.SelectedIndex = 0
        DetailGridView.Columns("NOTE").Visible = True
        DetailGridView.Columns("NOTE_EN").Visible = False
        DetailGridView.Refresh()
    End Sub

    Private Sub eng_MouseUp(sender As Object, e As MouseEventArgs) Handles eng.MouseUp
        TabControl1.SelectedIndex = 1
        TabControl2.SelectedIndex = 1
        DetailGridView.Columns("NOTE").Visible = False
        DetailGridView.Columns("NOTE_EN").Visible = True
        DetailGridView.Refresh()
    End Sub

    Private Sub TopUpTextBox_TextChanged(sender As Object, e As EventArgs) Handles TopUpTextBox.TextChanged
        If TopUpTextBox.IsHandleCreated Then
            Try
                HEADDataTable.Rows(0).Item("TOP_DISC_AMT") = Double.Parse(TopUpTextBox.Text)
                TOP_DISC_AMT = CDec(HEADDataTable.Rows(0).Item("TOP_DISC_AMT"))
            Catch ex As Exception
                Call MsgBox("กรุณากรอกข้อมูลเป็นตัวเลข", 0, "พบข้อผิดพลาด")
                TopUpTextBox.Text = "0"
            End Try
            calculateSum()
        End If

    End Sub

    Private Sub TRAN_DATEPicker_ValueChanged(sender As Object, e As EventArgs) Handles TRAN_DATEPicker.ValueChanged
        DUE_DATEPicker.MinDate = TRAN_DATEPicker.Value
    End Sub

    Private Sub DUE_DATEPicker_ValueChanged(sender As Object, e As EventArgs) Handles DUE_DATEPicker.ValueChanged
        CR_TERMTextBox.Text = (TRAN_DATEPicker.Value - DUE_DATEPicker.Value).ToString("dd")
    End Sub

End Class