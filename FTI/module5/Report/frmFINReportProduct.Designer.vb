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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.KeyWordTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TO_DATEPicker = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.FROM_DATEPicker = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btFind = New System.Windows.Forms.Button()
        Me.CancelButton = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.IsAR = New System.Windows.Forms.RadioButton()
        Me.isSubSection = New System.Windows.Forms.RadioButton()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.IsCancel = New System.Windows.Forms.CheckBox()
        Me.IsNormal = New System.Windows.Forms.CheckBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.IsPaid = New System.Windows.Forms.CheckBox()
        Me.IsNotPaid = New System.Windows.Forms.CheckBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.IsI1 = New System.Windows.Forms.CheckBox()
        Me.IsP1 = New System.Windows.Forms.CheckBox()
        Me.ExportProcessingButton = New System.Windows.Forms.Button()
        Me.PreviewReportButton = New System.Windows.Forms.Button()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 42)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(955, 296)
        Me.DataGridView1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 3)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(166, 31)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "รายงานเอกสาร"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.KeyWordTextBox)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(984, 13)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(208, 52)
        Me.GroupBox2.TabIndex = 157
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "ขอบเขตการค้นหา"
        '
        'KeyWordTextBox
        '
        Me.KeyWordTextBox.Location = New System.Drawing.Point(85, 21)
        Me.KeyWordTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.KeyWordTextBox.Name = "KeyWordTextBox"
        Me.KeyWordTextBox.Size = New System.Drawing.Size(114, 22)
        Me.KeyWordTextBox.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 24)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 17)
        Me.Label3.TabIndex = 71
        Me.Label3.Text = "คำค้น"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.TO_DATEPicker)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.FROM_DATEPicker)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Location = New System.Drawing.Point(984, 71)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(208, 95)
        Me.GroupBox1.TabIndex = 72
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "วันที่ออกเอกสาร"
        '
        'TO_DATEPicker
        '
        Me.TO_DATEPicker.CustomFormat = "dd/MM/yyyy"
        Me.TO_DATEPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TO_DATEPicker.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.TO_DATEPicker.Location = New System.Drawing.Point(85, 54)
        Me.TO_DATEPicker.Name = "TO_DATEPicker"
        Me.TO_DATEPicker.Size = New System.Drawing.Size(119, 22)
        Me.TO_DATEPicker.TabIndex = 290
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 55)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 17)
        Me.Label4.TabIndex = 74
        Me.Label4.Text = "ถึงวันที่"
        '
        'FROM_DATEPicker
        '
        Me.FROM_DATEPicker.CustomFormat = "dd/MM/yyyy"
        Me.FROM_DATEPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FROM_DATEPicker.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.FROM_DATEPicker.Location = New System.Drawing.Point(85, 25)
        Me.FROM_DATEPicker.Name = "FROM_DATEPicker"
        Me.FROM_DATEPicker.Size = New System.Drawing.Size(119, 22)
        Me.FROM_DATEPicker.TabIndex = 289
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(11, 25)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 17)
        Me.Label8.TabIndex = 75
        Me.Label8.Text = "จากวันที่"
        '
        'btFind
        '
        Me.btFind.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btFind.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btFind.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.btFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btFind.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btFind.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btFind.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btFind.Image = Global.FTI.My.Resources.Resources.imgSearch
        Me.btFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btFind.Location = New System.Drawing.Point(984, 516)
        Me.btFind.Margin = New System.Windows.Forms.Padding(4)
        Me.btFind.Name = "btFind"
        Me.btFind.Size = New System.Drawing.Size(208, 36)
        Me.btFind.TabIndex = 166
        Me.btFind.Text = "ค้นหา"
        Me.btFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btFind.UseVisualStyleBackColor = False
        '
        'CancelButton
        '
        Me.CancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CancelButton.BackColor = System.Drawing.Color.PaleTurquoise
        Me.CancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.CancelButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.CancelButton.Location = New System.Drawing.Point(984, 659)
        Me.CancelButton.Margin = New System.Windows.Forms.Padding(4)
        Me.CancelButton.Name = "CancelButton"
        Me.CancelButton.Size = New System.Drawing.Size(208, 28)
        Me.CancelButton.TabIndex = 168
        Me.CancelButton.Text = "ปิด"
        Me.CancelButton.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.IsAR)
        Me.GroupBox3.Controls.Add(Me.isSubSection)
        Me.GroupBox3.Location = New System.Drawing.Point(984, 173)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(208, 80)
        Me.GroupBox3.TabIndex = 169
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "ออกรายงานตาม"
        '
        'IsAR
        '
        Me.IsAR.AutoSize = True
        Me.IsAR.Location = New System.Drawing.Point(14, 52)
        Me.IsAR.Name = "IsAR"
        Me.IsAR.Size = New System.Drawing.Size(62, 21)
        Me.IsAR.TabIndex = 1
        Me.IsAR.TabStop = True
        Me.IsAR.Text = "ลูกหนี้"
        Me.IsAR.UseVisualStyleBackColor = True
        '
        'isSubSection
        '
        Me.isSubSection.AutoSize = True
        Me.isSubSection.Checked = True
        Me.isSubSection.Location = New System.Drawing.Point(14, 24)
        Me.isSubSection.Name = "isSubSection"
        Me.isSubSection.Size = New System.Drawing.Size(59, 21)
        Me.isSubSection.TabIndex = 0
        Me.isSubSection.TabStop = True
        Me.isSubSection.Text = "สินค้า"
        Me.isSubSection.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.IsCancel)
        Me.GroupBox4.Controls.Add(Me.IsNormal)
        Me.GroupBox4.Location = New System.Drawing.Point(984, 345)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(208, 80)
        Me.GroupBox4.TabIndex = 170
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "สถานะเอกสาร(ประมวลผล)"
        '
        'IsCancel
        '
        Me.IsCancel.AutoSize = True
        Me.IsCancel.Location = New System.Drawing.Point(14, 48)
        Me.IsCancel.Name = "IsCancel"
        Me.IsCancel.Size = New System.Drawing.Size(64, 21)
        Me.IsCancel.TabIndex = 175
        Me.IsCancel.Text = "ยกเลิก"
        Me.IsCancel.UseVisualStyleBackColor = True
        '
        'IsNormal
        '
        Me.IsNormal.AutoSize = True
        Me.IsNormal.Location = New System.Drawing.Point(14, 21)
        Me.IsNormal.Name = "IsNormal"
        Me.IsNormal.Size = New System.Drawing.Size(54, 21)
        Me.IsNormal.TabIndex = 174
        Me.IsNormal.Text = "ปกติ"
        Me.IsNormal.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.IsPaid)
        Me.GroupBox5.Controls.Add(Me.IsNotPaid)
        Me.GroupBox5.Location = New System.Drawing.Point(984, 431)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(208, 80)
        Me.GroupBox5.TabIndex = 171
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "สถานะหนี้(ประมวลผล)"
        '
        'IsPaid
        '
        Me.IsPaid.AutoSize = True
        Me.IsPaid.Location = New System.Drawing.Point(14, 49)
        Me.IsPaid.Name = "IsPaid"
        Me.IsPaid.Size = New System.Drawing.Size(92, 21)
        Me.IsPaid.TabIndex = 177
        Me.IsPaid.Text = "ไม่ค้างชำระ"
        Me.IsPaid.UseVisualStyleBackColor = True
        '
        'IsNotPaid
        '
        Me.IsNotPaid.AutoSize = True
        Me.IsNotPaid.Location = New System.Drawing.Point(14, 22)
        Me.IsNotPaid.Name = "IsNotPaid"
        Me.IsNotPaid.Size = New System.Drawing.Size(77, 21)
        Me.IsNotPaid.TabIndex = 176
        Me.IsNotPaid.Text = "ค้างชำระ"
        Me.IsNotPaid.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.Controls.Add(Me.IsI1)
        Me.GroupBox6.Controls.Add(Me.IsP1)
        Me.GroupBox6.Location = New System.Drawing.Point(984, 259)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(208, 80)
        Me.GroupBox6.TabIndex = 172
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "ประเภทเอกสาร"
        '
        'IsI1
        '
        Me.IsI1.AutoSize = True
        Me.IsI1.Location = New System.Drawing.Point(14, 48)
        Me.IsI1.Name = "IsI1"
        Me.IsI1.Size = New System.Drawing.Size(83, 21)
        Me.IsI1.TabIndex = 174
        Me.IsI1.Text = "ใบแจ้งหนี้"
        Me.IsI1.UseVisualStyleBackColor = True
        '
        'IsP1
        '
        Me.IsP1.AutoSize = True
        Me.IsP1.Location = New System.Drawing.Point(14, 21)
        Me.IsP1.Name = "IsP1"
        Me.IsP1.Size = New System.Drawing.Size(92, 21)
        Me.IsP1.TabIndex = 173
        Me.IsP1.Text = "ใบแจ้งชำระ"
        Me.IsP1.UseVisualStyleBackColor = True
        '
        'ExportProcessingButton
        '
        Me.ExportProcessingButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExportProcessingButton.BackColor = System.Drawing.Color.PaleTurquoise
        Me.ExportProcessingButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExportProcessingButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ExportProcessingButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.ExportProcessingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExportProcessingButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.ExportProcessingButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.ExportProcessingButton.Location = New System.Drawing.Point(984, 623)
        Me.ExportProcessingButton.Margin = New System.Windows.Forms.Padding(4)
        Me.ExportProcessingButton.Name = "ExportProcessingButton"
        Me.ExportProcessingButton.Size = New System.Drawing.Size(208, 28)
        Me.ExportProcessingButton.TabIndex = 173
        Me.ExportProcessingButton.Text = "ส่งออกประมวลผล"
        Me.ExportProcessingButton.UseVisualStyleBackColor = False
        '
        'PreviewReportButton
        '
        Me.PreviewReportButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PreviewReportButton.BackColor = System.Drawing.Color.PaleTurquoise
        Me.PreviewReportButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PreviewReportButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.PreviewReportButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.PreviewReportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PreviewReportButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.PreviewReportButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.PreviewReportButton.Location = New System.Drawing.Point(984, 587)
        Me.PreviewReportButton.Margin = New System.Windows.Forms.Padding(4)
        Me.PreviewReportButton.Name = "PreviewReportButton"
        Me.PreviewReportButton.Size = New System.Drawing.Size(208, 28)
        Me.PreviewReportButton.TabIndex = 174
        Me.PreviewReportButton.Text = "ดูรายงาน"
        Me.PreviewReportButton.UseVisualStyleBackColor = False
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(12, 390)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.RowTemplate.Height = 24
        Me.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView2.Size = New System.Drawing.Size(955, 297)
        Me.DataGridView2.TabIndex = 175
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 349)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 31)
        Me.Label2.TabIndex = 176
        Me.Label2.Text = "ประมวลผล"
        '
        'frmFINReportProduct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.BackgroundImage = Global.FTI.My.Resources.Resources.BGs
        Me.ClientSize = New System.Drawing.Size(1199, 712)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.PreviewReportButton)
        Me.Controls.Add(Me.ExportProcessingButton)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.CancelButton)
        Me.Controls.Add(Me.btFind)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmFINReportProduct"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmFINReportProduct"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents KeyWordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btFind As System.Windows.Forms.Button
    Friend WithEvents CancelButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents IsAR As System.Windows.Forms.RadioButton
    Friend WithEvents isSubSection As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents IsI1 As System.Windows.Forms.CheckBox
    Friend WithEvents IsP1 As System.Windows.Forms.CheckBox
    Friend WithEvents IsCancel As System.Windows.Forms.CheckBox
    Friend WithEvents IsNormal As System.Windows.Forms.CheckBox
    Friend WithEvents IsPaid As System.Windows.Forms.CheckBox
    Friend WithEvents IsNotPaid As System.Windows.Forms.CheckBox
    Friend WithEvents TO_DATEPicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents FROM_DATEPicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents ExportProcessingButton As System.Windows.Forms.Button
    Friend WithEvents PreviewReportButton As System.Windows.Forms.Button
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
