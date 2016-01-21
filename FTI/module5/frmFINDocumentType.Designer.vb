<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFINDocumentType
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
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txt2 = New System.Windows.Forms.TextBox()
        Me.txt3 = New System.Windows.Forms.TextBox()
        Me.txt1 = New System.Windows.Forms.TextBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.cbSearch = New System.Windows.Forms.ComboBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt5 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt6 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkY1 = New System.Windows.Forms.RadioButton()
        Me.chkN1 = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt4 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkN2 = New System.Windows.Forms.RadioButton()
        Me.chkY2 = New System.Windows.Forms.RadioButton()
        Me._CODE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me._NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me._CODE_DEPT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me._NAME_DEPT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me._CONTACT_NAME_THA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me._CONTACT_NAME_ENG = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me._IS_TRANSFER = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me._IS_CUT_DEPT = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btnAdd.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAdd.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAdd.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btnAdd.Image = Global.FTI.My.Resources.Resources.imgAdd
        Me.btnAdd.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnAdd.Location = New System.Drawing.Point(12, 388)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 32)
        Me.btnAdd.TabIndex = 111
        Me.btnAdd.Text = "เพิ่ม  "
        Me.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btnDelete.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDelete.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnDelete.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btnDelete.Image = Global.FTI.My.Resources.Resources.imgDelete
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnDelete.Location = New System.Drawing.Point(93, 388)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 32)
        Me.btnDelete.TabIndex = 110
        Me.btnDelete.Text = "ลบ   "
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btnClear.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btnClear.Image = Global.FTI.My.Resources.Resources.imgClear
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnClear.Location = New System.Drawing.Point(630, 388)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 32)
        Me.btnClear.TabIndex = 109
        Me.btnClear.Text = "ล้าง  "
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btnSave.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSave.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btnSave.Image = Global.FTI.My.Resources.Resources.imgSave
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnSave.Location = New System.Drawing.Point(711, 388)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 32)
        Me.btnSave.TabIndex = 108
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btnSearch.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btnSearch.Image = Global.FTI.My.Resources.Resources.imgSearch
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(711, 12)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 25)
        Me.btnSearch.TabIndex = 107
        Me.btnSearch.Text = "ค้นหา "
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'Label30
        '
        Me.Label30.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label30.Location = New System.Drawing.Point(25, 239)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(170, 16)
        Me.Label30.TabIndex = 104
        Me.Label30.Text = "รหัสประเภทเอกสารย่อยระบบลูกหนี้"
        '
        'Label29
        '
        Me.Label29.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label29.Location = New System.Drawing.Point(392, 213)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(109, 16)
        Me.Label29.TabIndex = 103
        Me.Label29.Text = "ชื่อประเภทเอกสารย่อย"
        '
        'Label28
        '
        Me.Label28.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label28.Location = New System.Drawing.Point(20, 213)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(122, 16)
        Me.Label28.TabIndex = 102
        Me.Label28.Text = "*รหัสประเภทเอกสารย่อย"
        '
        'txt2
        '
        Me.txt2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt2.Location = New System.Drawing.Point(201, 238)
        Me.txt2.Name = "txt2"
        Me.txt2.Size = New System.Drawing.Size(159, 20)
        Me.txt2.TabIndex = 101
        '
        'txt3
        '
        Me.txt3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt3.Location = New System.Drawing.Point(560, 212)
        Me.txt3.Name = "txt3"
        Me.txt3.Size = New System.Drawing.Size(226, 20)
        Me.txt3.TabIndex = 100
        '
        'txt1
        '
        Me.txt1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt1.Location = New System.Drawing.Point(201, 212)
        Me.txt1.Name = "txt1"
        Me.txt1.Size = New System.Drawing.Size(159, 20)
        Me.txt1.TabIndex = 99
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Location = New System.Drawing.Point(224, 15)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(481, 20)
        Me.txtSearch.TabIndex = 97
        '
        'cbSearch
        '
        Me.cbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSearch.FormattingEnabled = True
        Me.cbSearch.Items.AddRange(New Object() {"รหัสประเภทเอกสารย่อย", "ชื่อประเภทเอกสารย่อย", "รหัสประเภทเอกสรย่อยระบบลูกหนี้", "ชื่อประเภทเอกสารย่อยระบบลูกหนี้", "ชื่อที่แสดง"})
        Me.cbSearch.Location = New System.Drawing.Point(12, 15)
        Me.cbSearch.Name = "cbSearch"
        Me.cbSearch.Size = New System.Drawing.Size(206, 21)
        Me.cbSearch.TabIndex = 98
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
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me._CODE, Me._NAME, Me._CODE_DEPT, Me._NAME_DEPT, Me._CONTACT_NAME_THA, Me._CONTACT_NAME_ENG, Me._IS_TRANSFER, Me._IS_CUT_DEPT})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 43)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView1.Size = New System.Drawing.Size(774, 161)
        Me.DataGridView1.TabIndex = 96
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 430)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(798, 22)
        Me.StatusStrip1.TabIndex = 112
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(60, 17)
        Me.ToolStripStatusLabel1.Text = "Record(s):"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(13, 17)
        Me.ToolStripStatusLabel2.Text = "0"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 272)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(200, 16)
        Me.Label1.TabIndex = 114
        Me.Label1.Text = "ชื่อที่แสดงใน Pre-printed Form (ไทย)"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt5
        '
        Me.txt5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt5.Location = New System.Drawing.Point(238, 269)
        Me.txt5.Name = "txt5"
        Me.txt5.Size = New System.Drawing.Size(353, 20)
        Me.txt5.TabIndex = 113
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 298)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(212, 16)
        Me.Label2.TabIndex = 116
        Me.Label2.Text = "ชื่อที่แสดงใน Pre-printed Form (อังกฤษ)"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt6
        '
        Me.txt6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt6.Location = New System.Drawing.Point(237, 297)
        Me.txt6.Name = "txt6"
        Me.txt6.Size = New System.Drawing.Size(353, 20)
        Me.txt6.TabIndex = 115
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 328)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 16)
        Me.Label3.TabIndex = 117
        Me.Label3.Text = "โอนไประบบบัญชี"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkY1
        '
        Me.chkY1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkY1.AutoSize = True
        Me.chkY1.Location = New System.Drawing.Point(17, 4)
        Me.chkY1.Name = "chkY1"
        Me.chkY1.Size = New System.Drawing.Size(38, 17)
        Me.chkY1.TabIndex = 118
        Me.chkY1.TabStop = True
        Me.chkY1.Text = "ใช่"
        Me.chkY1.UseVisualStyleBackColor = True
        '
        'chkN1
        '
        Me.chkN1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkN1.AutoSize = True
        Me.chkN1.Location = New System.Drawing.Point(70, 3)
        Me.chkN1.Name = "chkN1"
        Me.chkN1.Size = New System.Drawing.Size(51, 17)
        Me.chkN1.TabIndex = 119
        Me.chkN1.TabStop = True
        Me.chkN1.Text = "ไม่ใช่"
        Me.chkN1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label4.Location = New System.Drawing.Point(20, 353)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 16)
        Me.Label4.TabIndex = 120
        Me.Label4.Text = "ลดยอดหนี้ใบแจ้งหนี้"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label5.Location = New System.Drawing.Point(392, 239)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(162, 16)
        Me.Label5.TabIndex = 124
        Me.Label5.Text = "ชื่อประเภทเอกสารย่อยระบบลูกหนี้"
        '
        'txt4
        '
        Me.txt4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt4.Location = New System.Drawing.Point(560, 238)
        Me.txt4.Name = "txt4"
        Me.txt4.Size = New System.Drawing.Size(226, 20)
        Me.txt4.TabIndex = 123
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.DarkRed
        Me.Label6.Location = New System.Drawing.Point(185, 404)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(376, 16)
        Me.Label6.TabIndex = 125
        Me.Label6.Text = "**ลดยอดหนี้ใบแจ้งหนี้ กำหนดสำหรับประเภทเอกสารรับคืน(ลดหนี้ใบเสร็จ)เท่านั้น"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkN1)
        Me.Panel1.Controls.Add(Me.chkY1)
        Me.Panel1.Location = New System.Drawing.Point(140, 322)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(151, 27)
        Me.Panel1.TabIndex = 126
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkN2)
        Me.Panel2.Controls.Add(Me.chkY2)
        Me.Panel2.Location = New System.Drawing.Point(140, 349)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(151, 27)
        Me.Panel2.TabIndex = 127
        '
        'chkN2
        '
        Me.chkN2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkN2.AutoSize = True
        Me.chkN2.Location = New System.Drawing.Point(70, 3)
        Me.chkN2.Name = "chkN2"
        Me.chkN2.Size = New System.Drawing.Size(51, 17)
        Me.chkN2.TabIndex = 119
        Me.chkN2.TabStop = True
        Me.chkN2.Text = "ไม่ใช่"
        Me.chkN2.UseVisualStyleBackColor = True
        '
        'chkY2
        '
        Me.chkY2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkY2.AutoSize = True
        Me.chkY2.Location = New System.Drawing.Point(17, 4)
        Me.chkY2.Name = "chkY2"
        Me.chkY2.Size = New System.Drawing.Size(38, 17)
        Me.chkY2.TabIndex = 118
        Me.chkY2.TabStop = True
        Me.chkY2.Text = "ใช่"
        Me.chkY2.UseVisualStyleBackColor = True
        '
        '_CODE
        '
        Me._CODE.HeaderText = "รหัสประเภทเอกสารย่อย"
        Me._CODE.Name = "_CODE"
        Me._CODE.ReadOnly = True
        Me._CODE.Width = 200
        '
        '_NAME
        '
        Me._NAME.HeaderText = "ชื่อประเภทเอกสารย่อย"
        Me._NAME.Name = "_NAME"
        Me._NAME.Width = 200
        '
        '_CODE_DEPT
        '
        Me._CODE_DEPT.HeaderText = "รหัสประเภทเอกสารย่อยระบบลูกหนี้"
        Me._CODE_DEPT.Name = "_CODE_DEPT"
        Me._CODE_DEPT.Visible = False
        Me._CODE_DEPT.Width = 200
        '
        '_NAME_DEPT
        '
        Me._NAME_DEPT.HeaderText = "ชื่อประเภทเอกสารย่อยระบบลูกหนี้"
        Me._NAME_DEPT.Name = "_NAME_DEPT"
        Me._NAME_DEPT.Visible = False
        Me._NAME_DEPT.Width = 200
        '
        '_CONTACT_NAME_THA
        '
        Me._CONTACT_NAME_THA.HeaderText = "ชื่อที่แสดง (ไทย)"
        Me._CONTACT_NAME_THA.Name = "_CONTACT_NAME_THA"
        Me._CONTACT_NAME_THA.Width = 150
        '
        '_CONTACT_NAME_ENG
        '
        Me._CONTACT_NAME_ENG.HeaderText = "ชื่อที่แสดง(อังกฤษ)"
        Me._CONTACT_NAME_ENG.Name = "_CONTACT_NAME_ENG"
        Me._CONTACT_NAME_ENG.Width = 150
        '
        '_IS_TRANSFER
        '
        Me._IS_TRANSFER.HeaderText = "โอนไประบบบัญชีอื่น"
        Me._IS_TRANSFER.Name = "_IS_TRANSFER"
        Me._IS_TRANSFER.Visible = False
        Me._IS_TRANSFER.Width = 120
        '
        '_IS_CUT_DEPT
        '
        Me._IS_CUT_DEPT.HeaderText = "ลดยอดหนี้ใบแจ้งหนี้"
        Me._IS_CUT_DEPT.Name = "_IS_CUT_DEPT"
        Me._IS_CUT_DEPT.Visible = False
        Me._IS_CUT_DEPT.Width = 120
        '
        'frmFINDocumentType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.BackgroundImage = Global.FTI.My.Resources.Resources.BGs
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(798, 452)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt4)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt5)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.txt2)
        Me.Controls.Add(Me.txt3)
        Me.Controls.Add(Me.txt1)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.cbSearch)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmFINDocumentType"
        Me.Text = "กำหนดประเภทเอกสาร"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txt2 As System.Windows.Forms.TextBox
    Friend WithEvents txt3 As System.Windows.Forms.TextBox
    Friend WithEvents txt1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents cbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt5 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt6 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkY1 As System.Windows.Forms.RadioButton
    Friend WithEvents chkN1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt4 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkN2 As System.Windows.Forms.RadioButton
    Friend WithEvents chkY2 As System.Windows.Forms.RadioButton
    Friend WithEvents _CODE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents _NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents _CODE_DEPT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents _NAME_DEPT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents _CONTACT_NAME_THA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents _CONTACT_NAME_ENG As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents _IS_TRANSFER As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents _IS_CUT_DEPT As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
