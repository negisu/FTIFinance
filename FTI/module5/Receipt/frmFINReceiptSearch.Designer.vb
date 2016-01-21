<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFINReceiptSearch
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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.AR_CODETextBox = New System.Windows.Forms.TextBox()
        Me.DIV_NAMEComboBox = New System.Windows.Forms.ComboBox()
        Me.isDiv = New System.Windows.Forms.CheckBox()
        Me.ATV_NAMEComboBox = New System.Windows.Forms.ComboBox()
        Me.isAR = New System.Windows.Forms.CheckBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.isATV = New System.Windows.Forms.CheckBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.AR_NAMETextBox = New System.Windows.Forms.TextBox()
        Me.RefTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ToDateTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.FromDateTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.isToDate = New System.Windows.Forms.CheckBox()
        Me.isFromDate = New System.Windows.Forms.CheckBox()
        Me.isRef = New System.Windows.Forms.CheckBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btFind = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.ProcessComboBox = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(17, 231)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(751, 332)
        Me.DataGridView1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(148, 31)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "ค้นหาใบเสร็จ"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Controls.Add(Me.RefTextBox)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.GroupBox1)
        Me.GroupBox2.Controls.Add(Me.isRef)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 46)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(759, 178)
        Me.GroupBox2.TabIndex = 160
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "ขอบเขตการค้นหา"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.AR_CODETextBox)
        Me.GroupBox3.Controls.Add(Me.DIV_NAMEComboBox)
        Me.GroupBox3.Controls.Add(Me.isDiv)
        Me.GroupBox3.Controls.Add(Me.ATV_NAMEComboBox)
        Me.GroupBox3.Controls.Add(Me.isAR)
        Me.GroupBox3.Controls.Add(Me.Label22)
        Me.GroupBox3.Controls.Add(Me.isATV)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.Label21)
        Me.GroupBox3.Controls.Add(Me.AR_NAMETextBox)
        Me.GroupBox3.Location = New System.Drawing.Point(360, 23)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(377, 151)
        Me.GroupBox3.TabIndex = 152
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "อื่นๆ"
        '
        'AR_CODETextBox
        '
        Me.AR_CODETextBox.BackColor = System.Drawing.Color.LemonChiffon
        Me.AR_CODETextBox.Location = New System.Drawing.Point(115, 85)
        Me.AR_CODETextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.AR_CODETextBox.Name = "AR_CODETextBox"
        Me.AR_CODETextBox.ReadOnly = True
        Me.AR_CODETextBox.Size = New System.Drawing.Size(245, 22)
        Me.AR_CODETextBox.TabIndex = 188
        '
        'DIV_NAMEComboBox
        '
        Me.DIV_NAMEComboBox.FormattingEnabled = True
        Me.DIV_NAMEComboBox.Location = New System.Drawing.Point(115, 20)
        Me.DIV_NAMEComboBox.Margin = New System.Windows.Forms.Padding(4)
        Me.DIV_NAMEComboBox.Name = "DIV_NAMEComboBox"
        Me.DIV_NAMEComboBox.Size = New System.Drawing.Size(245, 24)
        Me.DIV_NAMEComboBox.TabIndex = 184
        '
        'isDiv
        '
        Me.isDiv.AutoSize = True
        Me.isDiv.Location = New System.Drawing.Point(8, 23)
        Me.isDiv.Margin = New System.Windows.Forms.Padding(4)
        Me.isDiv.Name = "isDiv"
        Me.isDiv.Size = New System.Drawing.Size(18, 17)
        Me.isDiv.TabIndex = 76
        Me.isDiv.UseVisualStyleBackColor = True
        '
        'ATV_NAMEComboBox
        '
        Me.ATV_NAMEComboBox.FormattingEnabled = True
        Me.ATV_NAMEComboBox.Location = New System.Drawing.Point(115, 50)
        Me.ATV_NAMEComboBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ATV_NAMEComboBox.Name = "ATV_NAMEComboBox"
        Me.ATV_NAMEComboBox.Size = New System.Drawing.Size(245, 24)
        Me.ATV_NAMEComboBox.TabIndex = 182
        '
        'isAR
        '
        Me.isAR.AutoSize = True
        Me.isAR.Location = New System.Drawing.Point(8, 87)
        Me.isAR.Margin = New System.Windows.Forms.Padding(4)
        Me.isAR.Name = "isAR"
        Me.isAR.Size = New System.Drawing.Size(18, 17)
        Me.isAR.TabIndex = 82
        Me.isAR.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label22.Location = New System.Drawing.Point(35, 52)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(65, 20)
        Me.Label22.TabIndex = 181
        Me.Label22.Text = "*กิจกรรม"
        '
        'isATV
        '
        Me.isATV.AutoSize = True
        Me.isATV.Location = New System.Drawing.Point(8, 55)
        Me.isATV.Margin = New System.Windows.Forms.Padding(4)
        Me.isATV.Name = "isATV"
        Me.isATV.Size = New System.Drawing.Size(18, 17)
        Me.isATV.TabIndex = 79
        Me.isATV.UseVisualStyleBackColor = True
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label23.Location = New System.Drawing.Point(36, 21)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(65, 20)
        Me.Label23.TabIndex = 179
        Me.Label23.Text = "หน่วยงาน"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label21.Location = New System.Drawing.Point(35, 85)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(69, 20)
        Me.Label21.TabIndex = 177
        Me.Label21.Text = "*ผู้จ่ายเงิน"
        '
        'AR_NAMETextBox
        '
        Me.AR_NAMETextBox.Location = New System.Drawing.Point(115, 117)
        Me.AR_NAMETextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.AR_NAMETextBox.Name = "AR_NAMETextBox"
        Me.AR_NAMETextBox.Size = New System.Drawing.Size(245, 22)
        Me.AR_NAMETextBox.TabIndex = 178
        '
        'RefTextBox
        '
        Me.RefTextBox.Location = New System.Drawing.Point(168, 28)
        Me.RefTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.RefTextBox.Name = "RefTextBox"
        Me.RefTextBox.Size = New System.Drawing.Size(175, 22)
        Me.RefTextBox.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(45, 32)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 17)
        Me.Label3.TabIndex = 71
        Me.Label3.Text = "เลขที่อ้างอิง"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ToDateTextBox)
        Me.GroupBox1.Controls.Add(Me.FromDateTextBox)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.isToDate)
        Me.GroupBox1.Controls.Add(Me.isFromDate)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 60)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(335, 87)
        Me.GroupBox1.TabIndex = 72
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "วันที่ออกเอกสาร"
        '
        'ToDateTextBox
        '
        Me.ToDateTextBox.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ToDateTextBox.Location = New System.Drawing.Point(151, 48)
        Me.ToDateTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ToDateTextBox.Mask = "00/00/0000"
        Me.ToDateTextBox.Name = "ToDateTextBox"
        Me.ToDateTextBox.Size = New System.Drawing.Size(175, 22)
        Me.ToDateTextBox.TabIndex = 154
        Me.ToDateTextBox.ValidatingType = GetType(Date)
        '
        'FromDateTextBox
        '
        Me.FromDateTextBox.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.FromDateTextBox.Location = New System.Drawing.Point(151, 20)
        Me.FromDateTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.FromDateTextBox.Mask = "00/00/0000"
        Me.FromDateTextBox.Name = "FromDateTextBox"
        Me.FromDateTextBox.Size = New System.Drawing.Size(175, 22)
        Me.FromDateTextBox.TabIndex = 153
        Me.FromDateTextBox.ValidatingType = GetType(Date)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(28, 52)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 17)
        Me.Label4.TabIndex = 74
        Me.Label4.Text = "ถึงวันที่"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(28, 23)
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
        Me.isFromDate.Location = New System.Drawing.Point(8, 23)
        Me.isFromDate.Margin = New System.Windows.Forms.Padding(4)
        Me.isFromDate.Name = "isFromDate"
        Me.isFromDate.Size = New System.Drawing.Size(18, 17)
        Me.isFromDate.TabIndex = 74
        Me.isFromDate.UseVisualStyleBackColor = True
        '
        'isRef
        '
        Me.isRef.AutoSize = True
        Me.isRef.Location = New System.Drawing.Point(25, 32)
        Me.isRef.Margin = New System.Windows.Forms.Padding(4)
        Me.isRef.Name = "isRef"
        Me.isRef.Size = New System.Drawing.Size(18, 17)
        Me.isRef.TabIndex = 73
        Me.isRef.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btnClear.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btnClear.Image = Global.FTI.My.Resources.Resources.imgClear
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnClear.Location = New System.Drawing.Point(560, 11)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 36)
        Me.btnClear.TabIndex = 162
        Me.btnClear.Text = "ล้าง  "
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClear.UseVisualStyleBackColor = False
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
        Me.btFind.Location = New System.Drawing.Point(668, 11)
        Me.btFind.Margin = New System.Windows.Forms.Padding(4)
        Me.btFind.Name = "btFind"
        Me.btFind.Size = New System.Drawing.Size(100, 36)
        Me.btFind.TabIndex = 161
        Me.btFind.Text = "ค้นหา"
        Me.btFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btFind.UseVisualStyleBackColor = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(477, 575)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(285, 36)
        Me.TableLayoutPanel1.TabIndex = 163
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.BackColor = System.Drawing.Color.PaleTurquoise
        Me.OK_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.OK_Button.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.OK_Button.ForeColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.OK_Button.Location = New System.Drawing.Point(10, 4)
        Me.OK_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(121, 28)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "ทำรายการ"
        Me.OK_Button.UseVisualStyleBackColor = False
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
        Me.Cancel_Button.Location = New System.Drawing.Point(153, 4)
        Me.Cancel_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(121, 28)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "ยกเลิก"
        Me.Cancel_Button.UseVisualStyleBackColor = False
        '
        'ProcessComboBox
        '
        Me.ProcessComboBox.FormattingEnabled = True
        Me.ProcessComboBox.Items.AddRange(New Object() {"แก้ไขใบเสร็จ", "ยกเลิกใบเสร็จ"})
        Me.ProcessComboBox.Location = New System.Drawing.Point(199, 582)
        Me.ProcessComboBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ProcessComboBox.Name = "ProcessComboBox"
        Me.ProcessComboBox.Size = New System.Drawing.Size(269, 24)
        Me.ProcessComboBox.TabIndex = 164
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(63, 586)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 17)
        Me.Label2.TabIndex = 165
        Me.Label2.Text = "*โปรดเลือกรายการ"
        '
        'frmFINReceiptSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.BackgroundImage = Global.FTI.My.Resources.Resources.BGs
        Me.ClientSize = New System.Drawing.Size(775, 625)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ProcessComboBox)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btFind)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmFINReceiptSearch"
        Me.Text = "frmFINReceiptSearch"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents isDiv As System.Windows.Forms.CheckBox
    Friend WithEvents isAR As System.Windows.Forms.CheckBox
    Friend WithEvents isATV As System.Windows.Forms.CheckBox
    Friend WithEvents RefTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents isToDate As System.Windows.Forms.CheckBox
    Friend WithEvents isFromDate As System.Windows.Forms.CheckBox
    Friend WithEvents isRef As System.Windows.Forms.CheckBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btFind As System.Windows.Forms.Button
    Friend WithEvents ToDateTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents FromDateTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents DIV_NAMEComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ATV_NAMEComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents AR_NAMETextBox As System.Windows.Forms.TextBox
    Friend WithEvents AR_CODETextBox As System.Windows.Forms.TextBox
    Friend WithEvents ProcessComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
