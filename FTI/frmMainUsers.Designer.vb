<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainUsers
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainUsers))
        Me.lseparator1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btExecute = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btApply = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.btDelete = New System.Windows.Forms.Button()
        Me.btNew = New System.Windows.Forms.Button()
        Me.btGroups = New System.Windows.Forms.Button()
        Me.llCopy = New System.Windows.Forms.LinkLabel()
        Me.llPaste = New System.Windows.Forms.LinkLabel()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.btUsers = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lseparator1
        '
        Me.lseparator1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lseparator1.Location = New System.Drawing.Point(11, 40)
        Me.lseparator1.Name = "lseparator1"
        Me.lseparator1.Size = New System.Drawing.Size(760, 2)
        Me.lseparator1.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!)
        Me.Label1.Location = New System.Drawing.Point(11, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 16)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "ตัวกรอง"
        '
        'btExecute
        '
        Me.btExecute.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btExecute.BackgroundImage = CType(resources.GetObject("btExecute.BackgroundImage"), System.Drawing.Image)
        Me.btExecute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btExecute.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btExecute.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btExecute.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btExecute.Location = New System.Drawing.Point(696, 9)
        Me.btExecute.Name = "btExecute"
        Me.btExecute.Size = New System.Drawing.Size(75, 25)
        Me.btExecute.TabIndex = 1
        Me.btExecute.Text = "ดำเนินการ"
        Me.btExecute.UseVisualStyleBackColor = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(13, 45)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(758, 254)
        Me.DataGridView1.TabIndex = 9
        '
        'btApply
        '
        Me.btApply.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btApply.BackgroundImage = CType(resources.GetObject("btApply.BackgroundImage"), System.Drawing.Image)
        Me.btApply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btApply.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btApply.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btApply.Image = Global.FTI.My.Resources.Resources.imgSave
        Me.btApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btApply.Location = New System.Drawing.Point(357, 530)
        Me.btApply.Name = "btApply"
        Me.btApply.Size = New System.Drawing.Size(75, 32)
        Me.btApply.TabIndex = 5
        Me.btApply.Text = "บันทึก"
        Me.btApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btApply.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.AcceptsReturn = True
        Me.TextBox1.Location = New System.Drawing.Point(93, 337)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox1.Size = New System.Drawing.Size(597, 20)
        Me.TextBox1.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!)
        Me.Label2.Location = New System.Drawing.Point(37, 308)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 16)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "บัญชีผู้ใช้"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!)
        Me.Label3.Location = New System.Drawing.Point(371, 308)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 16)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "ชื่อผู้ใช้"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!)
        Me.Label4.Location = New System.Drawing.Point(41, 338)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 16)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "สิทธิ์ผู้ใช้"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(93, 307)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(175, 20)
        Me.TextBox2.TabIndex = 3
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(68, 12)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(622, 20)
        Me.TextBox3.TabIndex = 0
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(417, 307)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(354, 20)
        Me.TextBox4.TabIndex = 14
        '
        'btDelete
        '
        Me.btDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btDelete.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btDelete.BackgroundImage = CType(resources.GetObject("btDelete.BackgroundImage"), System.Drawing.Image)
        Me.btDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btDelete.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btDelete.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btDelete.Image = Global.FTI.My.Resources.Resources.imgDelete
        Me.btDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btDelete.Location = New System.Drawing.Point(94, 530)
        Me.btDelete.Name = "btDelete"
        Me.btDelete.Size = New System.Drawing.Size(75, 32)
        Me.btDelete.TabIndex = 16
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
        Me.btNew.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btNew.Image = Global.FTI.My.Resources.Resources.imgAdd
        Me.btNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btNew.Location = New System.Drawing.Point(13, 530)
        Me.btNew.Name = "btNew"
        Me.btNew.Size = New System.Drawing.Size(75, 32)
        Me.btNew.TabIndex = 15
        Me.btNew.Text = "เพิ่ม  "
        Me.btNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btNew.UseVisualStyleBackColor = False
        '
        'btGroups
        '
        Me.btGroups.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btGroups.BackgroundImage = CType(resources.GetObject("btGroups.BackgroundImage"), System.Drawing.Image)
        Me.btGroups.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btGroups.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btGroups.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btGroups.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btGroups.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btGroups.Location = New System.Drawing.Point(696, 335)
        Me.btGroups.Name = "btGroups"
        Me.btGroups.Size = New System.Drawing.Size(75, 25)
        Me.btGroups.TabIndex = 17
        Me.btGroups.Text = "เลือกกลุ่ม"
        Me.btGroups.UseVisualStyleBackColor = False
        '
        'llCopy
        '
        Me.llCopy.AutoSize = True
        Me.llCopy.Location = New System.Drawing.Point(662, 536)
        Me.llCopy.Name = "llCopy"
        Me.llCopy.Size = New System.Drawing.Size(60, 13)
        Me.llCopy.TabIndex = 18
        Me.llCopy.TabStop = True
        Me.llCopy.Text = "คัดลอกกลุ่ม"
        '
        'llPaste
        '
        Me.llPaste.AutoSize = True
        Me.llPaste.Location = New System.Drawing.Point(728, 536)
        Me.llPaste.Name = "llPaste"
        Me.llPaste.Size = New System.Drawing.Size(43, 13)
        Me.llPaste.TabIndex = 19
        Me.llPaste.TabStop = True
        Me.llPaste.Text = "วางกลุ่ม"
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
        Me.DataGridView2.Location = New System.Drawing.Point(13, 365)
        Me.DataGridView2.MultiSelect = False
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView2.Size = New System.Drawing.Size(759, 160)
        Me.DataGridView2.TabIndex = 49
        '
        'btUsers
        '
        Me.btUsers.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btUsers.BackgroundImage = CType(resources.GetObject("btUsers.BackgroundImage"), System.Drawing.Image)
        Me.btUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btUsers.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btUsers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btUsers.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btUsers.Location = New System.Drawing.Point(274, 305)
        Me.btUsers.Name = "btUsers"
        Me.btUsers.Size = New System.Drawing.Size(75, 25)
        Me.btUsers.TabIndex = 50
        Me.btUsers.Text = "เลือกผู้ใช้"
        Me.btUsers.UseVisualStyleBackColor = False
        '
        'frmMainUsers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.BackgroundImage = Global.FTI.My.Resources.Resources.BGs
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(784, 566)
        Me.Controls.Add(Me.btUsers)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.llPaste)
        Me.Controls.Add(Me.llCopy)
        Me.Controls.Add(Me.btGroups)
        Me.Controls.Add(Me.btDelete)
        Me.Controls.Add(Me.btNew)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.btApply)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.lseparator1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btExecute)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMainUsers"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " frmMainUsers"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lseparator1 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btExecute As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btApply As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents btDelete As System.Windows.Forms.Button
    Friend WithEvents btNew As System.Windows.Forms.Button
    Friend WithEvents btGroups As System.Windows.Forms.Button
    Friend WithEvents llCopy As System.Windows.Forms.LinkLabel
    Friend WithEvents llPaste As System.Windows.Forms.LinkLabel
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents btUsers As System.Windows.Forms.Button
End Class
