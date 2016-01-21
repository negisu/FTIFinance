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
        Me.DocTypeComboBox = New System.Windows.Forms.ComboBox()
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
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        Me.DataGridView1.Location = New System.Drawing.Point(16, 157)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(954, 433)
        Me.DataGridView1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(463, 31)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "รายงานค้างชำระตามรหัสสินค้า (ใบแจ้งชำระ)"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.DocTypeComboBox)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.KeyWordTextBox)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 61)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(587, 52)
        Me.GroupBox2.TabIndex = 157
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "ขอบเขตการค้นหา"
        '
        'DocTypeComboBox
        '
        Me.DocTypeComboBox.FormattingEnabled = True
        Me.DocTypeComboBox.Items.AddRange(New Object() {"ใบแจ้งชำระ", "ใบแจ้งหนี้"})
        Me.DocTypeComboBox.Location = New System.Drawing.Point(147, 59)
        Me.DocTypeComboBox.Name = "DocTypeComboBox"
        Me.DocTypeComboBox.Size = New System.Drawing.Size(334, 24)
        Me.DocTypeComboBox.TabIndex = 168
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 64)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 17)
        Me.Label6.TabIndex = 167
        Me.Label6.Text = "ประเภทเอกสาร"
        '
        'KeyWordTextBox
        '
        Me.KeyWordTextBox.Location = New System.Drawing.Point(104, 21)
        Me.KeyWordTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.KeyWordTextBox.Name = "KeyWordTextBox"
        Me.KeyWordTextBox.Size = New System.Drawing.Size(478, 22)
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
        Me.GroupBox1.Location = New System.Drawing.Point(611, 61)
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
        Me.SortColumnComboBox.Location = New System.Drawing.Point(120, 119)
        Me.SortColumnComboBox.Name = "SortColumnComboBox"
        Me.SortColumnComboBox.Size = New System.Drawing.Size(170, 24)
        Me.SortColumnComboBox.TabIndex = 160
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 122)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 17)
        Me.Label2.TabIndex = 161
        Me.Label2.Text = "เรียงด้วย"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(298, 121)
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
        Me.SortDirectionComboBox.Location = New System.Drawing.Point(423, 118)
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
        'frmFINReportProduct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.BackgroundImage = Global.FTI.My.Resources.Resources.BGs
        Me.ClientSize = New System.Drawing.Size(982, 653)
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
        CType(Me.DataGridView1,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox2.PerformLayout
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.ResumeLayout(false)
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
    Friend WithEvents DocTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btFind As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
End Class
