<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFINReportAR
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
        Me.SearchButton = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SortDirectionComboBox = New System.Windows.Forms.ComboBox()
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
        Me.DataGridView1.Location = New System.Drawing.Point(16, 218)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(1001, 683)
        Me.DataGridView1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(595, 31)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "5.5.16.4/10 รายงานใบสรุปค้างชำระตามลูกหนี้หน่วยงาน"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.DocTypeComboBox)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.KeyWordTextBox)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 46)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(489, 82)
        Me.GroupBox2.TabIndex = 157
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "ขอบเขตการค้นหา"
        '
        'DocTypeComboBox
        '
        Me.DocTypeComboBox.FormattingEnabled = True
        Me.DocTypeComboBox.Items.AddRange(New Object() {"ใบแจ้งชำระ", "ใบแจ้งหนี้"})
        Me.DocTypeComboBox.Location = New System.Drawing.Point(147, 46)
        Me.DocTypeComboBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DocTypeComboBox.Name = "DocTypeComboBox"
        Me.DocTypeComboBox.Size = New System.Drawing.Size(335, 24)
        Me.DocTypeComboBox.TabIndex = 168
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 50)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 17)
        Me.Label6.TabIndex = 167
        Me.Label6.Text = "ประเภทเอกสาร"
        '
        'KeyWordTextBox
        '
        Me.KeyWordTextBox.Location = New System.Drawing.Point(147, 18)
        Me.KeyWordTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.KeyWordTextBox.Name = "KeyWordTextBox"
        Me.KeyWordTextBox.Size = New System.Drawing.Size(335, 22)
        Me.KeyWordTextBox.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 21)
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
        Me.GroupBox1.Location = New System.Drawing.Point(512, 46)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(357, 82)
        Me.GroupBox1.TabIndex = 72
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "วันที่ออกเอกสาร"
        '
        'TodayToButton
        '
        Me.TodayToButton.Location = New System.Drawing.Point(285, 46)
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
        Me.TodayFromButton.Location = New System.Drawing.Point(285, 17)
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
        Me.Label4.Location = New System.Drawing.Point(28, 50)
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
        Me.SortColumnComboBox.Items.AddRange(New Object() {"รหัสผู้จ่ายตัง", "ชื่อผู้จ่ายตัง", "จำนวนรายการตกค้าง", "ยอดคงค้าง"})
        Me.SortColumnComboBox.Location = New System.Drawing.Point(133, 135)
        Me.SortColumnComboBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SortColumnComboBox.Name = "SortColumnComboBox"
        Me.SortColumnComboBox.Size = New System.Drawing.Size(169, 24)
        Me.SortColumnComboBox.TabIndex = 160
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 138)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 17)
        Me.Label2.TabIndex = 161
        Me.Label2.Text = "เรียงด้วย"
        '
        'SearchButton
        '
        Me.SearchButton.Location = New System.Drawing.Point(776, 135)
        Me.SearchButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SearchButton.Name = "SearchButton"
        Me.SearchButton.Size = New System.Drawing.Size(75, 23)
        Me.SearchButton.TabIndex = 162
        Me.SearchButton.Text = "ค้นหา"
        Me.SearchButton.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(396, 138)
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
        Me.SortDirectionComboBox.Location = New System.Drawing.Point(520, 134)
        Me.SortDirectionComboBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SortDirectionComboBox.Name = "SortDirectionComboBox"
        Me.SortDirectionComboBox.Size = New System.Drawing.Size(175, 24)
        Me.SortDirectionComboBox.TabIndex = 163
        '
        'frmFINReportAR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1029, 912)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.SortDirectionComboBox)
        Me.Controls.Add(Me.SearchButton)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.SortColumnComboBox)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "frmFINReportAR"
        Me.Text = "frmFINAR"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents SearchButton As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents SortDirectionComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents DocTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
