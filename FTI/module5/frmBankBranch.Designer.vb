<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBankBranch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBankBranch))
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txt2 = New System.Windows.Forms.TextBox()
        Me.txt1 = New System.Windows.Forms.TextBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.cbSearch = New System.Windows.Forms.ComboBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.chk1 = New System.Windows.Forms.CheckBox()
        Me.ddlBank = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BANK_CODE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BANK_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BRANCH_CODE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BRANCH_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LOCAL_YN = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btnSave.BackgroundImage = CType(resources.GetObject("btnSave.BackgroundImage"), System.Drawing.Image)
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSave.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btnSave.Image = Global.FTI.My.Resources.Resources.imgSave
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(676, 408)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 32)
        Me.btnSave.TabIndex = 110
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btnClear.BackgroundImage = CType(resources.GetObject("btnClear.BackgroundImage"), System.Drawing.Image)
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btnClear.Image = Global.FTI.My.Resources.Resources.imgClear
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnClear.Location = New System.Drawing.Point(579, 408)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 32)
        Me.btnClear.TabIndex = 109
        Me.btnClear.Text = "ล้าง  "
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnRemove
        '
        Me.btnRemove.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btnRemove.BackgroundImage = CType(resources.GetObject("btnRemove.BackgroundImage"), System.Drawing.Image)
        Me.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemove.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemove.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btnRemove.Image = Global.FTI.My.Resources.Resources.imgDelete
        Me.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemove.Location = New System.Drawing.Point(151, 408)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(75, 32)
        Me.btnRemove.TabIndex = 108
        Me.btnRemove.Text = "ลบ   "
        Me.btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRemove.UseVisualStyleBackColor = False
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btnAdd.BackgroundImage = CType(resources.GetObject("btnAdd.BackgroundImage"), System.Drawing.Image)
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAdd.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btnAdd.Image = Global.FTI.My.Resources.Resources.imgAdd
        Me.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAdd.Location = New System.Drawing.Point(54, 408)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 32)
        Me.btnAdd.TabIndex = 107
        Me.btnAdd.Text = "เพิ่ม  "
        Me.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btnSearch.BackgroundImage = CType(resources.GetObject("btnSearch.BackgroundImage"), System.Drawing.Image)
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btnSearch.Image = Global.FTI.My.Resources.Resources.imgSearch
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(711, 18)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 25)
        Me.btnSearch.TabIndex = 106
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'Label30
        '
        Me.Label30.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label30.Location = New System.Drawing.Point(388, 321)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(44, 16)
        Me.Label30.TabIndex = 105
        Me.Label30.Text = "นอกเขต"
        '
        'Label29
        '
        Me.Label29.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label29.Location = New System.Drawing.Point(388, 357)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(45, 16)
        Me.Label29.TabIndex = 104
        Me.Label29.Text = "ชื่อสาขา"
        '
        'Label28
        '
        Me.Label28.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label28.Location = New System.Drawing.Point(51, 357)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(53, 16)
        Me.Label28.TabIndex = 103
        Me.Label28.Text = "รหัสสาขา"
        '
        'txt2
        '
        Me.txt2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt2.Location = New System.Drawing.Point(467, 354)
        Me.txt2.Name = "txt2"
        Me.txt2.Size = New System.Drawing.Size(284, 20)
        Me.txt2.TabIndex = 101
        '
        'txt1
        '
        Me.txt1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt1.Location = New System.Drawing.Point(151, 354)
        Me.txt1.Name = "txt1"
        Me.txt1.Size = New System.Drawing.Size(186, 20)
        Me.txt1.TabIndex = 100
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Location = New System.Drawing.Point(224, 20)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(481, 20)
        Me.txtSearch.TabIndex = 98
        '
        'cbSearch
        '
        Me.cbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSearch.FormattingEnabled = True
        Me.cbSearch.Items.AddRange(New Object() {"รหัสธนาคาร", "ธนาคาร", "รหัสสาขา", "ชื่อสาขา"})
        Me.cbSearch.Location = New System.Drawing.Point(12, 20)
        Me.cbSearch.Name = "cbSearch"
        Me.cbSearch.Size = New System.Drawing.Size(206, 21)
        Me.cbSearch.TabIndex = 99
        '
        'DataGridView1
        '
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.BANK_CODE, Me.BANK_NAME, Me.BRANCH_CODE, Me.BRANCH_NAME, Me.LOCAL_YN})
        Me.DataGridView1.Location = New System.Drawing.Point(54, 81)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(697, 219)
        Me.DataGridView1.TabIndex = 97
        '
        'chk1
        '
        Me.chk1.AutoSize = True
        Me.chk1.Location = New System.Drawing.Point(467, 323)
        Me.chk1.Name = "chk1"
        Me.chk1.Size = New System.Drawing.Size(15, 14)
        Me.chk1.TabIndex = 111
        Me.chk1.UseVisualStyleBackColor = True
        '
        'ddlBank
        '
        Me.ddlBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlBank.FormattingEnabled = True
        Me.ddlBank.Items.AddRange(New Object() {"รหัสสาขา", "ชื่อสาขา"})
        Me.ddlBank.Location = New System.Drawing.Point(151, 316)
        Me.ddlBank.Name = "ddlBank"
        Me.ddlBank.Size = New System.Drawing.Size(186, 21)
        Me.ddlBank.TabIndex = 112
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(51, 321)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 16)
        Me.Label1.TabIndex = 113
        Me.Label1.Text = "ธนาคาร"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "BANK_CODE"
        Me.DataGridViewTextBoxColumn1.HeaderText = "รหัสธนาคาร"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 90
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "BANK_NAME"
        Me.DataGridViewTextBoxColumn2.HeaderText = "ธนาคาร"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 200
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "BRANCH_CODE"
        Me.DataGridViewTextBoxColumn3.HeaderText = "รหัสสาขา"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 75
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "BRANCH_NAME"
        Me.DataGridViewTextBoxColumn4.HeaderText = "ชื่อสาขา"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 200
        '
        'BANK_CODE
        '
        Me.BANK_CODE.DataPropertyName = "BANK_CODE"
        Me.BANK_CODE.HeaderText = "รหัสธนาคาร"
        Me.BANK_CODE.Name = "BANK_CODE"
        Me.BANK_CODE.ReadOnly = True
        Me.BANK_CODE.Width = 90
        '
        'BANK_NAME
        '
        Me.BANK_NAME.DataPropertyName = "BANK_NAME"
        Me.BANK_NAME.HeaderText = "ธนาคาร"
        Me.BANK_NAME.Name = "BANK_NAME"
        Me.BANK_NAME.ReadOnly = True
        Me.BANK_NAME.Width = 200
        '
        'BRANCH_CODE
        '
        Me.BRANCH_CODE.DataPropertyName = "BRANCH_CODE"
        Me.BRANCH_CODE.HeaderText = "รหัสสาขา"
        Me.BRANCH_CODE.Name = "BRANCH_CODE"
        Me.BRANCH_CODE.ReadOnly = True
        Me.BRANCH_CODE.Width = 75
        '
        'BRANCH_NAME
        '
        Me.BRANCH_NAME.DataPropertyName = "BRANCH_NAME"
        Me.BRANCH_NAME.HeaderText = "ชื่อสาขา"
        Me.BRANCH_NAME.Name = "BRANCH_NAME"
        Me.BRANCH_NAME.Width = 200
        '
        'LOCAL_YN
        '
        Me.LOCAL_YN.DataPropertyName = "LOCAL_YN"
        Me.LOCAL_YN.FalseValue = "N"
        Me.LOCAL_YN.HeaderText = "นอกเขต"
        Me.LOCAL_YN.Name = "LOCAL_YN"
        Me.LOCAL_YN.TrueValue = "Y"
        '
        'frmBankBranch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.BackgroundImage = Global.FTI.My.Resources.Resources.BGs
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(798, 452)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ddlBank)
        Me.Controls.Add(Me.chk1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.txt2)
        Me.Controls.Add(Me.txt1)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.cbSearch)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmBankBranch"
        Me.Text = "frmBankBranch"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txt2 As System.Windows.Forms.TextBox
    Friend WithEvents txt1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents cbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents chk1 As System.Windows.Forms.CheckBox
    Friend WithEvents ddlBank As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BANK_CODE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BANK_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BRANCH_CODE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BRANCH_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LOCAL_YN As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
