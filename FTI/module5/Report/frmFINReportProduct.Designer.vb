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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.KeyWordTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TodayToButton = New System.Windows.Forms.Button()
        Me.ToDateTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.TodayFromButton = New System.Windows.Forms.Button()
        Me.FromDateTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.isToDate = New System.Windows.Forms.CheckBox()
        Me.isFromDate = New System.Windows.Forms.CheckBox()
        Me.SortColumnComboBox = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SortDirectionComboBox = New System.Windows.Forms.ComboBox()
        Me.btFind = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.RadioButton5 = New System.Windows.Forms.RadioButton()
        Me.RadioButton6 = New System.Windows.Forms.RadioButton()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
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
        Me.DataGridView1.Location = New System.Drawing.Point(16, 283)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(954, 307)
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
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.KeyWordTextBox)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(217, 37)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(435, 80)
        Me.GroupBox2.TabIndex = 157
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "ขอบเขตการค้นหา"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 50)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 17)
        Me.Label6.TabIndex = 167
        Me.Label6.Text = "ประเภทเอกสาร"
        '
        'KeyWordTextBox
        '
        Me.KeyWordTextBox.Location = New System.Drawing.Point(148, 21)
        Me.KeyWordTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.KeyWordTextBox.Name = "KeyWordTextBox"
        Me.KeyWordTextBox.Size = New System.Drawing.Size(277, 22)
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
        Me.GroupBox1.Controls.Add(Me.TodayToButton)
        Me.GroupBox1.Controls.Add(Me.ToDateTextBox)
        Me.GroupBox1.Controls.Add(Me.TodayFromButton)
        Me.GroupBox1.Controls.Add(Me.FromDateTextBox)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.isToDate)
        Me.GroupBox1.Controls.Add(Me.isFromDate)
        Me.GroupBox1.Location = New System.Drawing.Point(620, 123)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(358, 83)
        Me.GroupBox1.TabIndex = 72
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "วันที่ออกเอกสาร"
        '
        'TodayToButton
        '
        Me.TodayToButton.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.TodayToButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TodayToButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TodayToButton.Location = New System.Drawing.Point(286, 46)
        Me.TodayToButton.Margin = New System.Windows.Forms.Padding(4)
        Me.TodayToButton.Name = "TodayToButton"
        Me.TodayToButton.Size = New System.Drawing.Size(53, 26)
        Me.TodayToButton.TabIndex = 264
        Me.TodayToButton.Text = "วันนี้"
        Me.TodayToButton.UseVisualStyleBackColor = True
        '
        'ToDateTextBox
        '
        Me.ToDateTextBox.Enabled = False
        Me.ToDateTextBox.Location = New System.Drawing.Point(149, 46)
        Me.ToDateTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ToDateTextBox.Mask = "00/00/0000"
        Me.ToDateTextBox.Name = "ToDateTextBox"
        Me.ToDateTextBox.Size = New System.Drawing.Size(132, 22)
        Me.ToDateTextBox.TabIndex = 263
        Me.ToDateTextBox.ValidatingType = GetType(Date)
        '
        'TodayFromButton
        '
        Me.TodayFromButton.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.TodayFromButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TodayFromButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TodayFromButton.Location = New System.Drawing.Point(286, 17)
        Me.TodayFromButton.Margin = New System.Windows.Forms.Padding(4)
        Me.TodayFromButton.Name = "TodayFromButton"
        Me.TodayFromButton.Size = New System.Drawing.Size(53, 26)
        Me.TodayFromButton.TabIndex = 262
        Me.TodayFromButton.Text = "วันนี้"
        Me.TodayFromButton.UseVisualStyleBackColor = True
        '
        'FromDateTextBox
        '
        Me.FromDateTextBox.Enabled = False
        Me.FromDateTextBox.Location = New System.Drawing.Point(149, 17)
        Me.FromDateTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.FromDateTextBox.Mask = "00/00/0000"
        Me.FromDateTextBox.Name = "FromDateTextBox"
        Me.FromDateTextBox.Size = New System.Drawing.Size(132, 22)
        Me.FromDateTextBox.TabIndex = 261
        Me.FromDateTextBox.ValidatingType = GetType(Date)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(28, 51)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 17)
        Me.Label4.TabIndex = 74
        Me.Label4.Text = "ถึงวันที่"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(28, 21)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 17)
        Me.Label8.TabIndex = 75
        Me.Label8.Text = "จากวันที่"
        '
        'isToDate
        '
        Me.isToDate.AutoSize = True
        Me.isToDate.Location = New System.Drawing.Point(8, 52)
        Me.isToDate.Margin = New System.Windows.Forms.Padding(4)
        Me.isToDate.Name = "isToDate"
        Me.isToDate.Size = New System.Drawing.Size(18, 17)
        Me.isToDate.TabIndex = 75
        Me.isToDate.UseVisualStyleBackColor = True
        '
        'isFromDate
        '
        Me.isFromDate.AutoSize = True
        Me.isFromDate.Location = New System.Drawing.Point(8, 22)
        Me.isFromDate.Margin = New System.Windows.Forms.Padding(4)
        Me.isFromDate.Name = "isFromDate"
        Me.isFromDate.Size = New System.Drawing.Size(18, 17)
        Me.isFromDate.TabIndex = 74
        Me.isFromDate.UseVisualStyleBackColor = True
        '
        'SortColumnComboBox
        '
        Me.SortColumnComboBox.FormattingEnabled = True
        Me.SortColumnComboBox.Items.AddRange(New Object() {"รหัสรายการ", "ชื่อรายการ", "จำนวนรายการตกค้าง"})
        Me.SortColumnComboBox.Location = New System.Drawing.Point(120, 250)
        Me.SortColumnComboBox.Name = "SortColumnComboBox"
        Me.SortColumnComboBox.Size = New System.Drawing.Size(170, 24)
        Me.SortColumnComboBox.TabIndex = 160
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 253)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 17)
        Me.Label2.TabIndex = 161
        Me.Label2.Text = "เรียงด้วย"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(298, 252)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 17)
        Me.Label5.TabIndex = 164
        Me.Label5.Text = "รูปแบบการเรียง"
        '
        'SortDirectionComboBox
        '
        Me.SortDirectionComboBox.FormattingEnabled = True
        Me.SortDirectionComboBox.Items.AddRange(New Object() {"ไม่เรียง", "เรียงจากน้อยไปมาก", "เรียงจากมากไปน้อย"})
        Me.SortDirectionComboBox.Location = New System.Drawing.Point(423, 249)
        Me.SortDirectionComboBox.Name = "SortDirectionComboBox"
        Me.SortDirectionComboBox.Size = New System.Drawing.Size(175, 24)
        Me.SortDirectionComboBox.TabIndex = 163
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
        Me.btFind.Location = New System.Drawing.Point(767, 11)
        Me.btFind.Margin = New System.Windows.Forms.Padding(4)
        Me.btFind.Name = "btFind"
        Me.btFind.Size = New System.Drawing.Size(100, 36)
        Me.btFind.TabIndex = 166
        Me.btFind.Text = "ค้นหา"
        Me.btFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btFind.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.PaleTurquoise
        Me.Button2.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.Button2.Image = Global.FTI.My.Resources.Resources.imgNew
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.Location = New System.Drawing.Point(875, 11)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(100, 36)
        Me.Button2.TabIndex = 167
        Me.Button2.Text = "ภาพรวม"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.BackColor = System.Drawing.Color.PaleTurquoise
        Me.Cancel_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Cancel_Button.ForeColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Cancel_Button.Location = New System.Drawing.Point(848, 612)
        Me.Cancel_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(121, 28)
        Me.Cancel_Button.TabIndex = 168
        Me.Cancel_Button.Text = "ปิด"
        Me.Cancel_Button.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RadioButton2)
        Me.GroupBox3.Controls.Add(Me.RadioButton1)
        Me.GroupBox3.Location = New System.Drawing.Point(16, 37)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(194, 80)
        Me.GroupBox3.TabIndex = 169
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "ออกรายงานตาม"
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(14, 24)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(59, 21)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "สินค้า"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(14, 52)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(62, 21)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "ลูกหนี้"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.RadioButton3)
        Me.GroupBox4.Controls.Add(Me.RadioButton4)
        Me.GroupBox4.Location = New System.Drawing.Point(16, 123)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(194, 80)
        Me.GroupBox4.TabIndex = 170
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "สถานะเอกสาร"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.RadioButton5)
        Me.GroupBox5.Controls.Add(Me.RadioButton6)
        Me.GroupBox5.Location = New System.Drawing.Point(217, 123)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(194, 80)
        Me.GroupBox5.TabIndex = 171
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "สถานะหนี้"
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Location = New System.Drawing.Point(14, 49)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(63, 21)
        Me.RadioButton3.TabIndex = 173
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "ยกเลิก"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.Location = New System.Drawing.Point(14, 21)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(53, 21)
        Me.RadioButton4.TabIndex = 172
        Me.RadioButton4.TabStop = True
        Me.RadioButton4.Text = "ปกติ"
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'RadioButton5
        '
        Me.RadioButton5.AutoSize = True
        Me.RadioButton5.Location = New System.Drawing.Point(14, 49)
        Me.RadioButton5.Name = "RadioButton5"
        Me.RadioButton5.Size = New System.Drawing.Size(91, 21)
        Me.RadioButton5.TabIndex = 175
        Me.RadioButton5.TabStop = True
        Me.RadioButton5.Text = "ไมค้างชำระ"
        Me.RadioButton5.UseVisualStyleBackColor = True
        '
        'RadioButton6
        '
        Me.RadioButton6.AutoSize = True
        Me.RadioButton6.Location = New System.Drawing.Point(14, 21)
        Me.RadioButton6.Name = "RadioButton6"
        Me.RadioButton6.Size = New System.Drawing.Size(76, 21)
        Me.RadioButton6.TabIndex = 174
        Me.RadioButton6.TabStop = True
        Me.RadioButton6.Text = "ค้างชำระ"
        Me.RadioButton6.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.CheckBox2)
        Me.GroupBox6.Controls.Add(Me.CheckBox1)
        Me.GroupBox6.Location = New System.Drawing.Point(419, 123)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(194, 80)
        Me.GroupBox6.TabIndex = 172
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "ประเภทเอกสาร"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(6, 21)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(92, 21)
        Me.CheckBox1.TabIndex = 173
        Me.CheckBox1.Text = "ใบแจ้งชำระ"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(6, 48)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(83, 21)
        Me.CheckBox2.TabIndex = 174
        Me.CheckBox2.Text = "ใบแจ้งหนี้"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'frmFINReportProduct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.BackgroundImage = Global.FTI.My.Resources.Resources.BGs
        Me.ClientSize = New System.Drawing.Size(982, 653)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btFind)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.SortDirectionComboBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.SortColumnComboBox)
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
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents isToDate As System.Windows.Forms.CheckBox
    Friend WithEvents isFromDate As System.Windows.Forms.CheckBox
    Friend WithEvents KeyWordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TodayToButton As System.Windows.Forms.Button
    Friend WithEvents ToDateTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents TodayFromButton As System.Windows.Forms.Button
    Friend WithEvents FromDateTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents SortColumnComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents SortDirectionComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btFind As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton4 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton5 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton6 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
End Class
