<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFINReportProduct
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.KeyWordTextBox = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TO_DATEPicker = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.FROM_DATEPicker = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.isDoc = New System.Windows.Forms.RadioButton()
        Me.IsAR = New System.Windows.Forms.RadioButton()
        Me.isSubSection = New System.Windows.Forms.RadioButton()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.DOC_STATUSComboBox = New System.Windows.Forms.ComboBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.PAY_STATUSComboBox = New System.Windows.Forms.ComboBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.TRAN_TYPEComboBox = New System.Windows.Forms.ComboBox()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.PreviewReportButton = New System.Windows.Forms.Button()
        Me.CancelButton1 = New System.Windows.Forms.Button()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.FormTitleLabel = New System.Windows.Forms.Label()
        Me.btFind = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DIVGroupBox = New System.Windows.Forms.GroupBox()
        Me.IsDIV = New System.Windows.Forms.CheckBox()
        Me.DIV_CODETextBox = New System.Windows.Forms.TextBox()
        Me.DIV_NAMEComboBox = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel10.SuspendLayout()
        Me.DIVGroupBox.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(9, 165)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(868, 205)
        Me.DataGridView1.TabIndex = 0
        '
        'KeyWordTextBox
        '
        Me.KeyWordTextBox.Location = New System.Drawing.Point(5, 19)
        Me.KeyWordTextBox.Name = "KeyWordTextBox"
        Me.KeyWordTextBox.Size = New System.Drawing.Size(172, 20)
        Me.KeyWordTextBox.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TO_DATEPicker)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.FROM_DATEPicker)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 91)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(182, 65)
        Me.GroupBox1.TabIndex = 72
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "วันที่ออกเอกสาร"
        '
        'TO_DATEPicker
        '
        Me.TO_DATEPicker.CustomFormat = "dd/MM/yyyy"
        Me.TO_DATEPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TO_DATEPicker.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.TO_DATEPicker.Location = New System.Drawing.Point(61, 41)
        Me.TO_DATEPicker.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TO_DATEPicker.Name = "TO_DATEPicker"
        Me.TO_DATEPicker.Size = New System.Drawing.Size(115, 20)
        Me.TO_DATEPicker.TabIndex = 290
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 74
        Me.Label4.Text = "ถึงวันที่"
        '
        'FROM_DATEPicker
        '
        Me.FROM_DATEPicker.CustomFormat = "dd/MM/yyyy"
        Me.FROM_DATEPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FROM_DATEPicker.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.FROM_DATEPicker.Location = New System.Drawing.Point(61, 18)
        Me.FROM_DATEPicker.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.FROM_DATEPicker.Name = "FROM_DATEPicker"
        Me.FROM_DATEPicker.Size = New System.Drawing.Size(115, 20)
        Me.FROM_DATEPicker.TabIndex = 289
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 13)
        Me.Label8.TabIndex = 75
        Me.Label8.Text = "จากวันที่"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.isDoc)
        Me.GroupBox3.Controls.Add(Me.IsAR)
        Me.GroupBox3.Controls.Add(Me.isSubSection)
        Me.GroupBox3.Location = New System.Drawing.Point(202, 91)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(182, 42)
        Me.GroupBox3.TabIndex = 169
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "ออกรายงานตาม"
        '
        'isDoc
        '
        Me.isDoc.AutoSize = True
        Me.isDoc.Location = New System.Drawing.Point(126, 16)
        Me.isDoc.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.isDoc.Name = "isDoc"
        Me.isDoc.Size = New System.Drawing.Size(60, 17)
        Me.isDoc.TabIndex = 2
        Me.isDoc.TabStop = True
        Me.isDoc.Text = "เอกสาร"
        Me.isDoc.UseVisualStyleBackColor = True
        '
        'IsAR
        '
        Me.IsAR.AutoSize = True
        Me.IsAR.Location = New System.Drawing.Point(67, 18)
        Me.IsAR.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.IsAR.Name = "IsAR"
        Me.IsAR.Size = New System.Drawing.Size(53, 17)
        Me.IsAR.TabIndex = 1
        Me.IsAR.TabStop = True
        Me.IsAR.Text = "ลูกหนี้"
        Me.IsAR.UseVisualStyleBackColor = True
        '
        'isSubSection
        '
        Me.isSubSection.AutoSize = True
        Me.isSubSection.Checked = True
        Me.isSubSection.Location = New System.Drawing.Point(10, 20)
        Me.isSubSection.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.isSubSection.Name = "isSubSection"
        Me.isSubSection.Size = New System.Drawing.Size(51, 17)
        Me.isSubSection.TabIndex = 0
        Me.isSubSection.TabStop = True
        Me.isSubSection.Text = "สินค้า"
        Me.isSubSection.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.DOC_STATUSComboBox)
        Me.GroupBox4.Location = New System.Drawing.Point(143, 43)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox4.Size = New System.Drawing.Size(129, 42)
        Me.GroupBox4.TabIndex = 170
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "สถานะเอกสาร"
        '
        'DOC_STATUSComboBox
        '
        Me.DOC_STATUSComboBox.FormattingEnabled = True
        Me.DOC_STATUSComboBox.Items.AddRange(New Object() {"ทั้งหมด", "ปกติ", "ยกเลิก"})
        Me.DOC_STATUSComboBox.Location = New System.Drawing.Point(11, 17)
        Me.DOC_STATUSComboBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DOC_STATUSComboBox.Name = "DOC_STATUSComboBox"
        Me.DOC_STATUSComboBox.Size = New System.Drawing.Size(114, 21)
        Me.DOC_STATUSComboBox.TabIndex = 176
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.PAY_STATUSComboBox)
        Me.GroupBox5.Location = New System.Drawing.Point(286, 41)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox5.Size = New System.Drawing.Size(182, 45)
        Me.GroupBox5.TabIndex = 171
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "สถานะหนี้"
        '
        'PAY_STATUSComboBox
        '
        Me.PAY_STATUSComboBox.AutoCompleteCustomSource.AddRange(New String() {"ทั้งหมด", "ค้างชำระ", "ชำระแล้ว"})
        Me.PAY_STATUSComboBox.FormattingEnabled = True
        Me.PAY_STATUSComboBox.Items.AddRange(New Object() {"ทั้งหมด", "ค้างชำระ", "ชำระแล้ว"})
        Me.PAY_STATUSComboBox.Location = New System.Drawing.Point(10, 20)
        Me.PAY_STATUSComboBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PAY_STATUSComboBox.Name = "PAY_STATUSComboBox"
        Me.PAY_STATUSComboBox.Size = New System.Drawing.Size(165, 21)
        Me.PAY_STATUSComboBox.TabIndex = 292
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.TRAN_TYPEComboBox)
        Me.GroupBox6.Location = New System.Drawing.Point(7, 43)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox6.Size = New System.Drawing.Size(129, 42)
        Me.GroupBox6.TabIndex = 172
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "ประเภทเอกสาร"
        '
        'TRAN_TYPEComboBox
        '
        Me.TRAN_TYPEComboBox.DisplayMember = "%,"
        Me.TRAN_TYPEComboBox.FormattingEnabled = True
        Me.TRAN_TYPEComboBox.Items.AddRange(New Object() {"ทั้งหมด", "ใบแจ้งชำระ", "ใบแจ้งหนี้", "ใบเสร็จ"})
        Me.TRAN_TYPEComboBox.Location = New System.Drawing.Point(11, 17)
        Me.TRAN_TYPEComboBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TRAN_TYPEComboBox.Name = "TRAN_TYPEComboBox"
        Me.TRAN_TYPEComboBox.Size = New System.Drawing.Size(114, 21)
        Me.TRAN_TYPEComboBox.TabIndex = 175
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(9, 375)
        Me.DataGridView2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.RowTemplate.Height = 24
        Me.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView2.Size = New System.Drawing.Size(868, 162)
        Me.DataGridView2.TabIndex = 175
        '
        'PreviewReportButton
        '
        Me.PreviewReportButton.BackColor = System.Drawing.Color.PaleTurquoise
        Me.PreviewReportButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PreviewReportButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.PreviewReportButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.PreviewReportButton.FlatAppearance.BorderSize = 0
        Me.PreviewReportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PreviewReportButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.PreviewReportButton.ForeColor = System.Drawing.Color.Black
        Me.PreviewReportButton.Location = New System.Drawing.Point(453, 137)
        Me.PreviewReportButton.Name = "PreviewReportButton"
        Me.PreviewReportButton.Size = New System.Drawing.Size(79, 23)
        Me.PreviewReportButton.TabIndex = 174
        Me.PreviewReportButton.Text = "ดูรายงาน"
        Me.PreviewReportButton.UseVisualStyleBackColor = False
        '
        'CancelButton1
        '
        Me.CancelButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CancelButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.CancelButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelButton1.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.CancelButton1.FlatAppearance.BorderSize = 0
        Me.CancelButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CancelButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.CancelButton1.ForeColor = System.Drawing.Color.Black
        Me.CancelButton1.Location = New System.Drawing.Point(809, 542)
        Me.CancelButton1.Name = "CancelButton1"
        Me.CancelButton1.Size = New System.Drawing.Size(68, 23)
        Me.CancelButton1.TabIndex = 168
        Me.CancelButton1.Text = "ปิด"
        Me.CancelButton1.UseVisualStyleBackColor = False
        '
        'Panel10
        '
        Me.Panel10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel10.BackColor = System.Drawing.Color.PaleTurquoise
        Me.Panel10.BackgroundImage = Global.FTI.My.Resources.Resources.tabbg
        Me.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel10.Controls.Add(Me.FormTitleLabel)
        Me.Panel10.Controls.Add(Me.btFind)
        Me.Panel10.Location = New System.Drawing.Point(-1, 0)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(890, 37)
        Me.Panel10.TabIndex = 290
        '
        'FormTitleLabel
        '
        Me.FormTitleLabel.AutoSize = True
        Me.FormTitleLabel.BackColor = System.Drawing.Color.Transparent
        Me.FormTitleLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.FormTitleLabel.ForeColor = System.Drawing.Color.White
        Me.FormTitleLabel.Location = New System.Drawing.Point(3, 6)
        Me.FormTitleLabel.Name = "FormTitleLabel"
        Me.FormTitleLabel.Size = New System.Drawing.Size(70, 24)
        Me.FormTitleLabel.TabIndex = 250
        Me.FormTitleLabel.Text = "รายงาน"
        '
        'btFind
        '
        Me.btFind.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btFind.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btFind.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.btFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btFind.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btFind.FlatAppearance.BorderSize = 0
        Me.btFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btFind.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btFind.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btFind.Image = Global.FTI.My.Resources.Resources.imgSearch
        Me.btFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btFind.Location = New System.Drawing.Point(810, 3)
        Me.btFind.Name = "btFind"
        Me.btFind.Size = New System.Drawing.Size(70, 29)
        Me.btFind.TabIndex = 166
        Me.btFind.Text = "ค้นหา"
        Me.btFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btFind.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(537, 142)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 13)
        Me.Label2.TabIndex = 292
        Me.Label2.Text = "จำนวนรายการทั้งสิ้น"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(645, 142)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 13)
        Me.Label5.TabIndex = 293
        Me.Label5.Text = "0"
        '
        'DIVGroupBox
        '
        Me.DIVGroupBox.Controls.Add(Me.IsDIV)
        Me.DIVGroupBox.Controls.Add(Me.DIV_CODETextBox)
        Me.DIVGroupBox.Controls.Add(Me.DIV_NAMEComboBox)
        Me.DIVGroupBox.Location = New System.Drawing.Point(393, 91)
        Me.DIVGroupBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DIVGroupBox.Name = "DIVGroupBox"
        Me.DIVGroupBox.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DIVGroupBox.Size = New System.Drawing.Size(266, 42)
        Me.DIVGroupBox.TabIndex = 294
        Me.DIVGroupBox.TabStop = False
        Me.DIVGroupBox.Text = "หน่วยงาน"
        '
        'IsDIV
        '
        Me.IsDIV.AutoSize = True
        Me.IsDIV.Location = New System.Drawing.Point(11, 20)
        Me.IsDIV.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.IsDIV.Name = "IsDIV"
        Me.IsDIV.Size = New System.Drawing.Size(15, 14)
        Me.IsDIV.TabIndex = 296
        Me.IsDIV.UseVisualStyleBackColor = True
        '
        'DIV_CODETextBox
        '
        Me.DIV_CODETextBox.BackColor = System.Drawing.SystemColors.Info
        Me.DIV_CODETextBox.Location = New System.Drawing.Point(29, 18)
        Me.DIV_CODETextBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DIV_CODETextBox.Name = "DIV_CODETextBox"
        Me.DIV_CODETextBox.Size = New System.Drawing.Size(57, 20)
        Me.DIV_CODETextBox.TabIndex = 295
        '
        'DIV_NAMEComboBox
        '
        Me.DIV_NAMEComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.DIV_NAMEComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.DIV_NAMEComboBox.DisplayMember = "%,"
        Me.DIV_NAMEComboBox.Enabled = False
        Me.DIV_NAMEComboBox.FormattingEnabled = True
        Me.DIV_NAMEComboBox.Items.AddRange(New Object() {"ทั้งหมด", "ใบแจ้งชำระ", "ใบแจ้งหนี้", "ใบเสร็จ"})
        Me.DIV_NAMEComboBox.Location = New System.Drawing.Point(92, 17)
        Me.DIV_NAMEComboBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DIV_NAMEComboBox.Name = "DIV_NAMEComboBox"
        Me.DIV_NAMEComboBox.Size = New System.Drawing.Size(167, 21)
        Me.DIV_NAMEComboBox.TabIndex = 175
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.KeyWordTextBox)
        Me.GroupBox2.Location = New System.Drawing.Point(477, 41)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(182, 45)
        Me.GroupBox2.TabIndex = 295
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "คำค้น"
        '
        'frmFINReportProduct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(889, 578)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.DIVGroupBox)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel10)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.PreviewReportButton)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.CancelButton1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "frmFINReportProduct"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmFINReportProduct"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.DIVGroupBox.ResumeLayout(False)
        Me.DIVGroupBox.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents KeyWordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents btFind As System.Windows.Forms.Button
    Friend WithEvents CancelButton1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents IsAR As System.Windows.Forms.RadioButton
    Friend WithEvents isSubSection As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents TO_DATEPicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents FROM_DATEPicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents PreviewReportButton As System.Windows.Forms.Button
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents FormTitleLabel As System.Windows.Forms.Label
    Friend WithEvents isDoc As System.Windows.Forms.RadioButton
    Friend WithEvents DOC_STATUSComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents TRAN_TYPEComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents PAY_STATUSComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DIVGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents DIV_NAMEComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents DIV_CODETextBox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents IsDIV As System.Windows.Forms.CheckBox
End Class
