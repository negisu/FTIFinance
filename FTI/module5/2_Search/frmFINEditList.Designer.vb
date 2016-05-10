<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFINEditList
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PAY_STATUSComboBox = New System.Windows.Forms.ComboBox()
        Me.RefTextBox = New System.Windows.Forms.TextBox()
        Me.isRef = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btFind = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TO_DATEPicker = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.FROM_DATEPicker = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DataGridView1.Location = New System.Drawing.Point(13, 119)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(718, 365)
        Me.DataGridView1.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.PAY_STATUSComboBox)
        Me.GroupBox2.Controls.Add(Me.RefTextBox)
        Me.GroupBox2.Controls.Add(Me.isRef)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 44)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(182, 69)
        Me.GroupBox2.TabIndex = 160
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "ขอบเขตการค้นหา"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 41)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 293
        Me.Label2.Text = "สถานะหนี้"
        '
        'PAY_STATUSComboBox
        '
        Me.PAY_STATUSComboBox.AutoCompleteCustomSource.AddRange(New String() {"ทั้งหมด", "ค้างชำระ", "ชำระแล้ว"})
        Me.PAY_STATUSComboBox.FormattingEnabled = True
        Me.PAY_STATUSComboBox.Items.AddRange(New Object() {"ทั้งหมด", "ค้างชำระ", "ชำระแล้ว"})
        Me.PAY_STATUSComboBox.Location = New System.Drawing.Point(80, 39)
        Me.PAY_STATUSComboBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PAY_STATUSComboBox.Name = "PAY_STATUSComboBox"
        Me.PAY_STATUSComboBox.Size = New System.Drawing.Size(97, 21)
        Me.PAY_STATUSComboBox.TabIndex = 292
        '
        'RefTextBox
        '
        Me.RefTextBox.Location = New System.Drawing.Point(80, 17)
        Me.RefTextBox.Name = "RefTextBox"
        Me.RefTextBox.Size = New System.Drawing.Size(97, 20)
        Me.RefTextBox.TabIndex = 3
        '
        'isRef
        '
        Me.isRef.AutoSize = True
        Me.isRef.Location = New System.Drawing.Point(7, 19)
        Me.isRef.Name = "isRef"
        Me.isRef.Size = New System.Drawing.Size(77, 17)
        Me.isRef.TabIndex = 73
        Me.isRef.Text = "เลขที่อ้างอิง"
        Me.isRef.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(518, 491)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(214, 29)
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
        Me.OK_Button.Location = New System.Drawing.Point(8, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(91, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "แก้ไข"
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
        Me.Cancel_Button.Location = New System.Drawing.Point(115, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(91, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "ปิด"
        Me.Cancel_Button.UseVisualStyleBackColor = False
        '
        'Panel10
        '
        Me.Panel10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel10.BackColor = System.Drawing.Color.DarkTurquoise
        Me.Panel10.BackgroundImage = Global.FTI.My.Resources.Resources.tabbg
        Me.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel10.Controls.Add(Me.Label1)
        Me.Panel10.Location = New System.Drawing.Point(-1, 0)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(740, 38)
        Me.Panel10.TabIndex = 290
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(10, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(164, 24)
        Me.Label1.TabIndex = 250
        Me.Label1.Text = "สอบถามใบแจ้งชำระ"
        '
        'btFind
        '
        Me.btFind.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btFind.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btFind.BackgroundImage = Global.FTI.My.Resources.Resources.searchCustomButton
        Me.btFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btFind.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btFind.FlatAppearance.BorderSize = 0
        Me.btFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btFind.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btFind.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btFind.Location = New System.Drawing.Point(656, 44)
        Me.btFind.Name = "btFind"
        Me.btFind.Size = New System.Drawing.Size(75, 22)
        Me.btFind.TabIndex = 161
        Me.btFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btFind.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.TO_DATEPicker)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.FROM_DATEPicker)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Location = New System.Drawing.Point(200, 44)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(182, 69)
        Me.GroupBox1.TabIndex = 291
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "วันที่ออกเอกสาร"
        '
        'TO_DATEPicker
        '
        Me.TO_DATEPicker.CustomFormat = "dd/MM/yyyy"
        Me.TO_DATEPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TO_DATEPicker.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.TO_DATEPicker.Location = New System.Drawing.Point(62, 41)
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
        Me.FROM_DATEPicker.Location = New System.Drawing.Point(62, 17)
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
        'frmFINEditList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(736, 531)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btFind)
        Me.Controls.Add(Me.Panel10)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmFINEditList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "สอบถามใบแจ้งชำระ"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RefTextBox As System.Windows.Forms.TextBox
    Friend WithEvents isRef As System.Windows.Forms.CheckBox
    Friend WithEvents btFind As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TO_DATEPicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents FROM_DATEPicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PAY_STATUSComboBox As System.Windows.Forms.ComboBox
End Class
