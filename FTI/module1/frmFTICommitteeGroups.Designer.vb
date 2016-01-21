<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFTICommitteeGroups
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFTICommitteeGroups))
        Me.btFind = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.btCommitteeGroupsSave = New System.Windows.Forms.Button()
        Me.btDelete = New System.Windows.Forms.Button()
        Me.btNew = New System.Windows.Forms.Button()
        Me.btDIV1 = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.ComboBox6 = New System.Windows.Forms.ComboBox()
        Me.MaskedTextBox4 = New System.Windows.Forms.MaskedTextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.ComboBox5 = New System.Windows.Forms.ComboBox()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.MaskedTextBox3 = New System.Windows.Forms.MaskedTextBox()
        Me.MaskedTextBox2 = New System.Windows.Forms.MaskedTextBox()
        Me.btDIV2 = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox16 = New System.Windows.Forms.TextBox()
        Me.TextBox15 = New System.Windows.Forms.TextBox()
        Me.TextBox14 = New System.Windows.Forms.TextBox()
        Me.TextBox13 = New System.Windows.Forms.TextBox()
        Me.TextBox12 = New System.Windows.Forms.TextBox()
        Me.TextBox11 = New System.Windows.Forms.TextBox()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.TextBox10 = New System.Windows.Forms.TextBox()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TextBox17 = New System.Windows.Forms.TextBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.btMeetingSave = New System.Windows.Forms.Button()
        Me.btMeetingDel = New System.Windows.Forms.Button()
        Me.btMeetingAdd = New System.Windows.Forms.Button()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.btRepresentPosition = New System.Windows.Forms.Button()
        Me.btRepresentSave = New System.Windows.Forms.Button()
        Me.btRepresentDel = New System.Windows.Forms.Button()
        Me.btRepresentAdd = New System.Windows.Forms.Button()
        Me.DataGridView3 = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.MaskedTextBox1 = New System.Windows.Forms.MaskedTextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.ComboBox7 = New System.Windows.Forms.ComboBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btFind
        '
        Me.btFind.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btFind.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btFind.BackgroundImage = CType(resources.GetObject("btFind.BackgroundImage"), System.Drawing.Image)
        Me.btFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btFind.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btFind.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btFind.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btFind.Image = Global.FTI.My.Resources.Resources.imgSearch
        Me.btFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btFind.Location = New System.Drawing.Point(905, 9)
        Me.btFind.Name = "btFind"
        Me.btFind.Size = New System.Drawing.Size(75, 25)
        Me.btFind.TabIndex = 70
        Me.btFind.Text = "ค้นหา "
        Me.btFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btFind.UseVisualStyleBackColor = False
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
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 39)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(968, 205)
        Me.DataGridView1.TabIndex = 69
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(260, 12)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(639, 20)
        Me.TextBox1.TabIndex = 67
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"ส่วนของคำ"})
        Me.ComboBox1.Location = New System.Drawing.Point(12, 12)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(242, 21)
        Me.ComboBox1.TabIndex = 68
        '
        'btCommitteeGroupsSave
        '
        Me.btCommitteeGroupsSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btCommitteeGroupsSave.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btCommitteeGroupsSave.BackgroundImage = CType(resources.GetObject("btCommitteeGroupsSave.BackgroundImage"), System.Drawing.Image)
        Me.btCommitteeGroupsSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btCommitteeGroupsSave.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btCommitteeGroupsSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btCommitteeGroupsSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btCommitteeGroupsSave.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btCommitteeGroupsSave.Location = New System.Drawing.Point(396, 671)
        Me.btCommitteeGroupsSave.Name = "btCommitteeGroupsSave"
        Me.btCommitteeGroupsSave.Size = New System.Drawing.Size(200, 34)
        Me.btCommitteeGroupsSave.TabIndex = 36
        Me.btCommitteeGroupsSave.Text = "บันทึกคณะกรรมการ ภายใน/ภายนอก"
        Me.btCommitteeGroupsSave.UseVisualStyleBackColor = False
        '
        'btDelete
        '
        Me.btDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btDelete.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btDelete.BackgroundImage = CType(resources.GetObject("btDelete.BackgroundImage"), System.Drawing.Image)
        Me.btDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btDelete.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btDelete.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btDelete.Image = Global.FTI.My.Resources.Resources.imgDelete
        Me.btDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btDelete.Location = New System.Drawing.Point(93, 673)
        Me.btDelete.Name = "btDelete"
        Me.btDelete.Size = New System.Drawing.Size(75, 32)
        Me.btDelete.TabIndex = 73
        Me.btDelete.Text = "ลบ   "
        Me.btDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btDelete.UseVisualStyleBackColor = False
        '
        'btNew
        '
        Me.btNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btNew.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btNew.BackgroundImage = CType(resources.GetObject("btNew.BackgroundImage"), System.Drawing.Image)
        Me.btNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btNew.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btNew.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btNew.Image = Global.FTI.My.Resources.Resources.imgAdd
        Me.btNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btNew.Location = New System.Drawing.Point(12, 673)
        Me.btNew.Name = "btNew"
        Me.btNew.Size = New System.Drawing.Size(75, 32)
        Me.btNew.TabIndex = 72
        Me.btNew.Text = "เพิ่ม  "
        Me.btNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btNew.UseVisualStyleBackColor = False
        '
        'btDIV1
        '
        Me.btDIV1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDIV1.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btDIV1.BackgroundImage = CType(resources.GetObject("btDIV1.BackgroundImage"), System.Drawing.Image)
        Me.btDIV1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btDIV1.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btDIV1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btDIV1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btDIV1.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btDIV1.Location = New System.Drawing.Point(881, 304)
        Me.btDIV1.Name = "btDIV1"
        Me.btDIV1.Size = New System.Drawing.Size(90, 25)
        Me.btDIV1.TabIndex = 74
        Me.btDIV1.Text = "ฝ่าย..."
        Me.btDIV1.UseVisualStyleBackColor = False
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox2.Location = New System.Drawing.Point(185, 250)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(100, 20)
        Me.TextBox2.TabIndex = 75
        '
        'TextBox3
        '
        Me.TextBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox3.Location = New System.Drawing.Point(292, 250)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(679, 20)
        Me.TextBox3.TabIndex = 76
        '
        'TextBox4
        '
        Me.TextBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox4.Location = New System.Drawing.Point(185, 277)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(100, 20)
        Me.TextBox4.TabIndex = 77
        '
        'TextBox5
        '
        Me.TextBox5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox5.Location = New System.Drawing.Point(185, 304)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.TextBox5.Size = New System.Drawing.Size(100, 20)
        Me.TextBox5.TabIndex = 79
        '
        'TextBox6
        '
        Me.TextBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox6.Location = New System.Drawing.Point(292, 304)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.ReadOnly = True
        Me.TextBox6.Size = New System.Drawing.Size(583, 20)
        Me.TextBox6.TabIndex = 80
        '
        'ComboBox2
        '
        Me.ComboBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(185, 331)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(300, 21)
        Me.ComboBox2.TabIndex = 81
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(12, 358)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(969, 307)
        Me.TabControl1.TabIndex = 82
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label21)
        Me.TabPage1.Controls.Add(Me.ComboBox7)
        Me.TabPage1.Controls.Add(Me.Label20)
        Me.TabPage1.Controls.Add(Me.ComboBox6)
        Me.TabPage1.Controls.Add(Me.MaskedTextBox4)
        Me.TabPage1.Controls.Add(Me.Label19)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.TextBox8)
        Me.TabPage1.Controls.Add(Me.Label17)
        Me.TabPage1.Controls.Add(Me.ComboBox5)
        Me.TabPage1.Controls.Add(Me.ComboBox4)
        Me.TabPage1.Controls.Add(Me.MaskedTextBox3)
        Me.TabPage1.Controls.Add(Me.MaskedTextBox2)
        Me.TabPage1.Controls.Add(Me.btDIV2)
        Me.TabPage1.Controls.Add(Me.Label16)
        Me.TabPage1.Controls.Add(Me.Label15)
        Me.TabPage1.Controls.Add(Me.Label14)
        Me.TabPage1.Controls.Add(Me.Label13)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.TextBox16)
        Me.TabPage1.Controls.Add(Me.TextBox15)
        Me.TabPage1.Controls.Add(Me.TextBox14)
        Me.TabPage1.Controls.Add(Me.TextBox13)
        Me.TabPage1.Controls.Add(Me.TextBox12)
        Me.TabPage1.Controls.Add(Me.TextBox11)
        Me.TabPage1.Controls.Add(Me.ComboBox3)
        Me.TabPage1.Controls.Add(Me.TextBox10)
        Me.TabPage1.Controls.Add(Me.TextBox9)
        Me.TabPage1.Controls.Add(Me.TextBox7)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(961, 281)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "รายละเอียด"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(440, 167)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(113, 13)
        Me.Label20.TabIndex = 114
        Me.Label20.Text = "ขอบข่ายการดำเนินงาน"
        '
        'ComboBox6
        '
        Me.ComboBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox6.FormattingEnabled = True
        Me.ComboBox6.Location = New System.Drawing.Point(559, 164)
        Me.ComboBox6.Name = "ComboBox6"
        Me.ComboBox6.Size = New System.Drawing.Size(300, 21)
        Me.ComboBox6.TabIndex = 113
        '
        'MaskedTextBox4
        '
        Me.MaskedTextBox4.Location = New System.Drawing.Point(756, 242)
        Me.MaskedTextBox4.Mask = "00/00/0000 90:00"
        Me.MaskedTextBox4.Name = "MaskedTextBox4"
        Me.MaskedTextBox4.Size = New System.Drawing.Size(100, 20)
        Me.MaskedTextBox4.TabIndex = 112
        Me.MaskedTextBox4.ValidatingType = GetType(Date)
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(684, 245)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(66, 13)
        Me.Label19.TabIndex = 111
        Me.Label19.Text = "วันที่ปรับปรุง"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(484, 245)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(52, 13)
        Me.Label18.TabIndex = 110
        Me.Label18.Text = "ผู้ปรับปรุง"
        '
        'TextBox8
        '
        Me.TextBox8.Location = New System.Drawing.Point(543, 242)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(134, 20)
        Me.TextBox8.TabIndex = 109
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(99, 245)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(63, 13)
        Me.Label17.TabIndex = 108
        Me.Label17.Text = "สถานะข้อมูล"
        '
        'ComboBox5
        '
        Me.ComboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox5.FormattingEnabled = True
        Me.ComboBox5.Location = New System.Drawing.Point(168, 242)
        Me.ComboBox5.Name = "ComboBox5"
        Me.ComboBox5.Size = New System.Drawing.Size(101, 21)
        Me.ComboBox5.TabIndex = 107
        '
        'ComboBox4
        '
        Me.ComboBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Location = New System.Drawing.Point(276, 7)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(582, 21)
        Me.ComboBox4.TabIndex = 106
        '
        'MaskedTextBox3
        '
        Me.MaskedTextBox3.Location = New System.Drawing.Point(599, 33)
        Me.MaskedTextBox3.Mask = "00/00/0000"
        Me.MaskedTextBox3.Name = "MaskedTextBox3"
        Me.MaskedTextBox3.Size = New System.Drawing.Size(100, 20)
        Me.MaskedTextBox3.TabIndex = 105
        Me.MaskedTextBox3.ValidatingType = GetType(Date)
        '
        'MaskedTextBox2
        '
        Me.MaskedTextBox2.Location = New System.Drawing.Point(168, 33)
        Me.MaskedTextBox2.Mask = "00/00/0000"
        Me.MaskedTextBox2.Name = "MaskedTextBox2"
        Me.MaskedTextBox2.Size = New System.Drawing.Size(100, 20)
        Me.MaskedTextBox2.TabIndex = 104
        Me.MaskedTextBox2.ValidatingType = GetType(Date)
        '
        'btDIV2
        '
        Me.btDIV2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDIV2.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btDIV2.BackgroundImage = CType(resources.GetObject("btDIV2.BackgroundImage"), System.Drawing.Image)
        Me.btDIV2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btDIV2.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btDIV2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btDIV2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btDIV2.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btDIV2.Location = New System.Drawing.Point(864, 138)
        Me.btDIV2.Name = "btDIV2"
        Me.btDIV2.Size = New System.Drawing.Size(91, 25)
        Me.btDIV2.TabIndex = 102
        Me.btDIV2.Text = "ฝ่าย..."
        Me.btDIV2.UseVisualStyleBackColor = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(484, 219)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(109, 13)
        Me.Label16.TabIndex = 101
        Me.Label16.Text = "ผู้รับผิดชอบของ ส.อ.ท."
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(4, 219)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(158, 13)
        Me.Label15.TabIndex = 100
        Me.Label15.Text = "ผู้รับผิดชอบของหน่วยงานที่ติดต่อ"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(119, 193)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(43, 13)
        Me.Label14.TabIndex = 99
        Me.Label14.Text = "สายงาน"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(110, 167)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(52, 13)
        Me.Label13.TabIndex = 98
        Me.Label13.Text = "ชื่อย่อฝ่าย"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(135, 141)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(26, 13)
        Me.Label12.TabIndex = 97
        Me.Label12.Text = "ฝ่าย"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(71, 114)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 13)
        Me.Label11.TabIndex = 96
        Me.Label11.Text = "เป็นกรรมการโดย"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(59, 88)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(102, 13)
        Me.Label10.TabIndex = 95
        Me.Label10.Text = "ชื่อ/ตำแหน่งประธาน"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(129, 62)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(32, 13)
        Me.Label9.TabIndex = 94
        Me.Label9.Text = "สังกัด"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(509, 36)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(84, 13)
        Me.Label8.TabIndex = 93
        Me.Label8.Text = "วันที่เข้ากองเลขา"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(106, 36)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 92
        Me.Label7.Text = "วันที่อนุมัติ"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(125, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(36, 13)
        Me.Label6.TabIndex = 91
        Me.Label6.Text = "วาระปี"
        '
        'TextBox16
        '
        Me.TextBox16.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox16.Location = New System.Drawing.Point(599, 216)
        Me.TextBox16.Name = "TextBox16"
        Me.TextBox16.Size = New System.Drawing.Size(259, 20)
        Me.TextBox16.TabIndex = 90
        '
        'TextBox15
        '
        Me.TextBox15.Location = New System.Drawing.Point(168, 216)
        Me.TextBox15.Name = "TextBox15"
        Me.TextBox15.Size = New System.Drawing.Size(300, 20)
        Me.TextBox15.TabIndex = 89
        '
        'TextBox14
        '
        Me.TextBox14.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox14.Location = New System.Drawing.Point(168, 190)
        Me.TextBox14.Name = "TextBox14"
        Me.TextBox14.Size = New System.Drawing.Size(690, 20)
        Me.TextBox14.TabIndex = 88
        '
        'TextBox13
        '
        Me.TextBox13.Location = New System.Drawing.Point(168, 164)
        Me.TextBox13.Name = "TextBox13"
        Me.TextBox13.ReadOnly = True
        Me.TextBox13.Size = New System.Drawing.Size(100, 20)
        Me.TextBox13.TabIndex = 87
        '
        'TextBox12
        '
        Me.TextBox12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox12.Location = New System.Drawing.Point(274, 138)
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.ReadOnly = True
        Me.TextBox12.Size = New System.Drawing.Size(584, 20)
        Me.TextBox12.TabIndex = 85
        '
        'TextBox11
        '
        Me.TextBox11.Location = New System.Drawing.Point(168, 138)
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.ReadOnly = True
        Me.TextBox11.Size = New System.Drawing.Size(100, 20)
        Me.TextBox11.TabIndex = 84
        '
        'ComboBox3
        '
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(168, 111)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(300, 21)
        Me.ComboBox3.TabIndex = 83
        '
        'TextBox10
        '
        Me.TextBox10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox10.Location = New System.Drawing.Point(168, 85)
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New System.Drawing.Size(690, 20)
        Me.TextBox10.TabIndex = 82
        '
        'TextBox9
        '
        Me.TextBox9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox9.Location = New System.Drawing.Point(168, 59)
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(690, 20)
        Me.TextBox9.TabIndex = 81
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(168, 7)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.ReadOnly = True
        Me.TextBox7.Size = New System.Drawing.Size(100, 20)
        Me.TextBox7.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TextBox17)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(961, 281)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "อำนาจหน้าที่"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TextBox17
        '
        Me.TextBox17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox17.Location = New System.Drawing.Point(3, 3)
        Me.TextBox17.Multiline = True
        Me.TextBox17.Name = "TextBox17"
        Me.TextBox17.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox17.Size = New System.Drawing.Size(955, 275)
        Me.TextBox17.TabIndex = 88
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.btMeetingSave)
        Me.TabPage3.Controls.Add(Me.btMeetingDel)
        Me.TabPage3.Controls.Add(Me.btMeetingAdd)
        Me.TabPage3.Controls.Add(Me.DataGridView2)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(961, 281)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "รายละเอียดการประชุม"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'btMeetingSave
        '
        Me.btMeetingSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btMeetingSave.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btMeetingSave.BackgroundImage = CType(resources.GetObject("btMeetingSave.BackgroundImage"), System.Drawing.Image)
        Me.btMeetingSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btMeetingSave.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btMeetingSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btMeetingSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btMeetingSave.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btMeetingSave.Location = New System.Drawing.Point(865, 68)
        Me.btMeetingSave.Name = "btMeetingSave"
        Me.btMeetingSave.Size = New System.Drawing.Size(90, 25)
        Me.btMeetingSave.TabIndex = 77
        Me.btMeetingSave.Text = "บันทึกประชุม"
        Me.btMeetingSave.UseVisualStyleBackColor = False
        '
        'btMeetingDel
        '
        Me.btMeetingDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btMeetingDel.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btMeetingDel.BackgroundImage = CType(resources.GetObject("btMeetingDel.BackgroundImage"), System.Drawing.Image)
        Me.btMeetingDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btMeetingDel.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btMeetingDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btMeetingDel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btMeetingDel.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btMeetingDel.Location = New System.Drawing.Point(865, 37)
        Me.btMeetingDel.Name = "btMeetingDel"
        Me.btMeetingDel.Size = New System.Drawing.Size(90, 25)
        Me.btMeetingDel.TabIndex = 76
        Me.btMeetingDel.Text = "ลบประชุม"
        Me.btMeetingDel.UseVisualStyleBackColor = False
        '
        'btMeetingAdd
        '
        Me.btMeetingAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btMeetingAdd.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btMeetingAdd.BackgroundImage = CType(resources.GetObject("btMeetingAdd.BackgroundImage"), System.Drawing.Image)
        Me.btMeetingAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btMeetingAdd.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btMeetingAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btMeetingAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btMeetingAdd.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btMeetingAdd.Location = New System.Drawing.Point(865, 6)
        Me.btMeetingAdd.Name = "btMeetingAdd"
        Me.btMeetingAdd.Size = New System.Drawing.Size(90, 25)
        Me.btMeetingAdd.TabIndex = 75
        Me.btMeetingAdd.Text = "เพิ่มประชุม"
        Me.btMeetingAdd.UseVisualStyleBackColor = False
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AllowUserToResizeRows = False
        Me.DataGridView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(6, 6)
        Me.DataGridView2.MultiSelect = False
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(853, 269)
        Me.DataGridView2.TabIndex = 70
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.btRepresentPosition)
        Me.TabPage4.Controls.Add(Me.btRepresentSave)
        Me.TabPage4.Controls.Add(Me.btRepresentDel)
        Me.TabPage4.Controls.Add(Me.btRepresentAdd)
        Me.TabPage4.Controls.Add(Me.DataGridView3)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(961, 281)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "รายการผู้แทน"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'btRepresentPosition
        '
        Me.btRepresentPosition.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btRepresentPosition.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btRepresentPosition.BackgroundImage = CType(resources.GetObject("btRepresentPosition.BackgroundImage"), System.Drawing.Image)
        Me.btRepresentPosition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btRepresentPosition.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btRepresentPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btRepresentPosition.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btRepresentPosition.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btRepresentPosition.Location = New System.Drawing.Point(865, 68)
        Me.btRepresentPosition.Name = "btRepresentPosition"
        Me.btRepresentPosition.Size = New System.Drawing.Size(90, 25)
        Me.btRepresentPosition.TabIndex = 81
        Me.btRepresentPosition.Text = "ตำแหน่งผู้แทน"
        Me.btRepresentPosition.UseVisualStyleBackColor = False
        '
        'btRepresentSave
        '
        Me.btRepresentSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btRepresentSave.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btRepresentSave.BackgroundImage = CType(resources.GetObject("btRepresentSave.BackgroundImage"), System.Drawing.Image)
        Me.btRepresentSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btRepresentSave.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btRepresentSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btRepresentSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btRepresentSave.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btRepresentSave.Location = New System.Drawing.Point(865, 122)
        Me.btRepresentSave.Name = "btRepresentSave"
        Me.btRepresentSave.Size = New System.Drawing.Size(90, 25)
        Me.btRepresentSave.TabIndex = 80
        Me.btRepresentSave.Text = "บันทึกผู้แทน"
        Me.btRepresentSave.UseVisualStyleBackColor = False
        '
        'btRepresentDel
        '
        Me.btRepresentDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btRepresentDel.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btRepresentDel.BackgroundImage = CType(resources.GetObject("btRepresentDel.BackgroundImage"), System.Drawing.Image)
        Me.btRepresentDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btRepresentDel.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btRepresentDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btRepresentDel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btRepresentDel.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btRepresentDel.Location = New System.Drawing.Point(865, 37)
        Me.btRepresentDel.Name = "btRepresentDel"
        Me.btRepresentDel.Size = New System.Drawing.Size(90, 25)
        Me.btRepresentDel.TabIndex = 79
        Me.btRepresentDel.Text = "ลบผู้แทน"
        Me.btRepresentDel.UseVisualStyleBackColor = False
        '
        'btRepresentAdd
        '
        Me.btRepresentAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btRepresentAdd.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btRepresentAdd.BackgroundImage = CType(resources.GetObject("btRepresentAdd.BackgroundImage"), System.Drawing.Image)
        Me.btRepresentAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btRepresentAdd.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btRepresentAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btRepresentAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btRepresentAdd.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btRepresentAdd.Location = New System.Drawing.Point(865, 6)
        Me.btRepresentAdd.Name = "btRepresentAdd"
        Me.btRepresentAdd.Size = New System.Drawing.Size(90, 25)
        Me.btRepresentAdd.TabIndex = 78
        Me.btRepresentAdd.Text = "เพิ่มผู้แทน"
        Me.btRepresentAdd.UseVisualStyleBackColor = False
        '
        'DataGridView3
        '
        Me.DataGridView3.AllowUserToAddRows = False
        Me.DataGridView3.AllowUserToDeleteRows = False
        Me.DataGridView3.AllowUserToResizeRows = False
        Me.DataGridView3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView3.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView3.Location = New System.Drawing.Point(6, 6)
        Me.DataGridView3.MultiSelect = False
        Me.DataGridView3.Name = "DataGridView3"
        Me.DataGridView3.Size = New System.Drawing.Size(853, 269)
        Me.DataGridView3.TabIndex = 71
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(150, 253)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 13)
        Me.Label1.TabIndex = 83
        Me.Label1.Text = "คณะ"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(125, 280)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 84
        Me.Label2.Text = "ชื่อย่อคณะ"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(556, 280)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 85
        Me.Label3.Text = "วันที่ก่อตั้ง"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 307)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(157, 13)
        Me.Label4.TabIndex = 86
        Me.Label4.Text = "ฝ่ายที่รับผิดชอบในวาระปีปัจจุบัน"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(113, 334)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 87
        Me.Label5.Text = "ประเภทคณะ"
        '
        'MaskedTextBox1
        '
        Me.MaskedTextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.MaskedTextBox1.Location = New System.Drawing.Point(615, 277)
        Me.MaskedTextBox1.Mask = "00/00/0000"
        Me.MaskedTextBox1.Name = "MaskedTextBox1"
        Me.MaskedTextBox1.Size = New System.Drawing.Size(100, 20)
        Me.MaskedTextBox1.TabIndex = 103
        Me.MaskedTextBox1.ValidatingType = GetType(Date)
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(298, 245)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(58, 13)
        Me.Label21.TabIndex = 116
        Me.Label21.Text = "เคลื่อนไหว"
        '
        'ComboBox7
        '
        Me.ComboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox7.FormattingEnabled = True
        Me.ComboBox7.Location = New System.Drawing.Point(367, 242)
        Me.ComboBox7.Name = "ComboBox7"
        Me.ComboBox7.Size = New System.Drawing.Size(101, 21)
        Me.ComboBox7.TabIndex = 115
        '
        'frmFTICommitteeGroups
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.BackgroundImage = Global.FTI.My.Resources.Resources.BGs
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(993, 711)
        Me.Controls.Add(Me.MaskedTextBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.TextBox6)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.btCommitteeGroupsSave)
        Me.Controls.Add(Me.btDIV1)
        Me.Controls.Add(Me.btDelete)
        Me.Controls.Add(Me.btNew)
        Me.Controls.Add(Me.btFind)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Name = "frmFTICommitteeGroups"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmFTICommitteeGroups"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btFind As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents btCommitteeGroupsSave As System.Windows.Forms.Button
    Friend WithEvents btDelete As System.Windows.Forms.Button
    Friend WithEvents btNew As System.Windows.Forms.Button
    Friend WithEvents btDIV1 As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox10 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox9 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox12 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox11 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox13 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox16 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox15 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox14 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox17 As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btDIV2 As System.Windows.Forms.Button
    Friend WithEvents MaskedTextBox1 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents MaskedTextBox3 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents MaskedTextBox2 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents ComboBox5 As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents MaskedTextBox4 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents ComboBox6 As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView3 As System.Windows.Forms.DataGridView
    Friend WithEvents btMeetingSave As System.Windows.Forms.Button
    Friend WithEvents btMeetingDel As System.Windows.Forms.Button
    Friend WithEvents btMeetingAdd As System.Windows.Forms.Button
    Friend WithEvents btRepresentSave As System.Windows.Forms.Button
    Friend WithEvents btRepresentDel As System.Windows.Forms.Button
    Friend WithEvents btRepresentAdd As System.Windows.Forms.Button
    Friend WithEvents btRepresentPosition As System.Windows.Forms.Button
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents ComboBox7 As System.Windows.Forms.ComboBox
End Class
