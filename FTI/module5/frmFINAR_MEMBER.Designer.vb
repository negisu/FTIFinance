<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFINAR_MEMBER
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFINAR_MEMBER))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btApply = New System.Windows.Forms.Button()
        Me.btNew = New System.Windows.Forms.Button()
        Me.btDelete = New System.Windows.Forms.Button()
        Me.btFind = New System.Windows.Forms.Button()
        Me.DocAddrGrid = New System.Windows.Forms.DataGridView()
        Me.MailAddrGrid = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DocAddrGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MailAddrGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(772, 602)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(195, 36)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.BackColor = System.Drawing.Color.PaleTurquoise
        Me.OK_Button.BackgroundImage = CType(resources.GetObject("OK_Button.BackgroundImage"), System.Drawing.Image)
        Me.OK_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.OK_Button.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.OK_Button.ForeColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.OK_Button.Location = New System.Drawing.Point(4, 4)
        Me.OK_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(89, 28)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "เลือก"
        Me.OK_Button.UseVisualStyleBackColor = False
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.BackColor = System.Drawing.Color.PaleTurquoise
        Me.Cancel_Button.BackgroundImage = CType(resources.GetObject("Cancel_Button.BackgroundImage"), System.Drawing.Image)
        Me.Cancel_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Cancel_Button.ForeColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Cancel_Button.Location = New System.Drawing.Point(101, 4)
        Me.Cancel_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(89, 28)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "ปิด"
        Me.Cancel_Button.UseVisualStyleBackColor = False
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
        Me.DataGridView1.Location = New System.Drawing.Point(16, 51)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 25
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.ShowEditingIcon = False
        Me.DataGridView1.Size = New System.Drawing.Size(953, 275)
        Me.DataGridView1.TabIndex = 5
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(16, 15)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(843, 22)
        Me.TextBox1.TabIndex = 3
        '
        'btApply
        '
        Me.btApply.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btApply.BackgroundImage = CType(resources.GetObject("btApply.BackgroundImage"), System.Drawing.Image)
        Me.btApply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btApply.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btApply.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btApply.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btApply.Image = Global.FTI.My.Resources.Resources.imgSave
        Me.btApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btApply.Location = New System.Drawing.Point(393, 595)
        Me.btApply.Margin = New System.Windows.Forms.Padding(4)
        Me.btApply.Name = "btApply"
        Me.btApply.Size = New System.Drawing.Size(100, 39)
        Me.btApply.TabIndex = 6
        Me.btApply.Text = "บันทึก"
        Me.btApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btApply.UseVisualStyleBackColor = False
        Me.btApply.Visible = False
        '
        'btNew
        '
        Me.btNew.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btNew.BackgroundImage = CType(resources.GetObject("btNew.BackgroundImage"), System.Drawing.Image)
        Me.btNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btNew.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btNew.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btNew.Image = Global.FTI.My.Resources.Resources.imgAdd
        Me.btNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btNew.Location = New System.Drawing.Point(16, 595)
        Me.btNew.Margin = New System.Windows.Forms.Padding(4)
        Me.btNew.Name = "btNew"
        Me.btNew.Size = New System.Drawing.Size(100, 39)
        Me.btNew.TabIndex = 7
        Me.btNew.Text = "เพิ่ม  "
        Me.btNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btNew.UseVisualStyleBackColor = False
        Me.btNew.Visible = False
        '
        'btDelete
        '
        Me.btDelete.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btDelete.BackgroundImage = CType(resources.GetObject("btDelete.BackgroundImage"), System.Drawing.Image)
        Me.btDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btDelete.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btDelete.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btDelete.Image = Global.FTI.My.Resources.Resources.imgDelete
        Me.btDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btDelete.Location = New System.Drawing.Point(124, 595)
        Me.btDelete.Margin = New System.Windows.Forms.Padding(4)
        Me.btDelete.Name = "btDelete"
        Me.btDelete.Size = New System.Drawing.Size(100, 39)
        Me.btDelete.TabIndex = 8
        Me.btDelete.Text = "ลบ   "
        Me.btDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btDelete.UseVisualStyleBackColor = False
        Me.btDelete.Visible = False
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
        Me.btFind.Location = New System.Drawing.Point(867, 7)
        Me.btFind.Margin = New System.Windows.Forms.Padding(4)
        Me.btFind.Name = "btFind"
        Me.btFind.Size = New System.Drawing.Size(100, 36)
        Me.btFind.TabIndex = 183
        Me.btFind.Text = "ค้นหา"
        Me.btFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btFind.UseVisualStyleBackColor = False
        '
        'DocAddrGrid
        '
        Me.DocAddrGrid.AllowUserToAddRows = False
        Me.DocAddrGrid.AllowUserToDeleteRows = False
        Me.DocAddrGrid.AllowUserToResizeRows = False
        Me.DocAddrGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DocAddrGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.DocAddrGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DocAddrGrid.Location = New System.Drawing.Point(15, 370)
        Me.DocAddrGrid.Margin = New System.Windows.Forms.Padding(4)
        Me.DocAddrGrid.MultiSelect = False
        Me.DocAddrGrid.Name = "DocAddrGrid"
        Me.DocAddrGrid.RowHeadersWidth = 25
        Me.DocAddrGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DocAddrGrid.ShowEditingIcon = False
        Me.DocAddrGrid.Size = New System.Drawing.Size(953, 83)
        Me.DocAddrGrid.TabIndex = 184
        '
        'MailAddrGrid
        '
        Me.MailAddrGrid.AllowUserToAddRows = False
        Me.MailAddrGrid.AllowUserToDeleteRows = False
        Me.MailAddrGrid.AllowUserToResizeRows = False
        Me.MailAddrGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MailAddrGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.MailAddrGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MailAddrGrid.Location = New System.Drawing.Point(13, 494)
        Me.MailAddrGrid.Margin = New System.Windows.Forms.Padding(4)
        Me.MailAddrGrid.MultiSelect = False
        Me.MailAddrGrid.Name = "MailAddrGrid"
        Me.MailAddrGrid.RowHeadersWidth = 25
        Me.MailAddrGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.MailAddrGrid.ShowEditingIcon = False
        Me.MailAddrGrid.Size = New System.Drawing.Size(953, 83)
        Me.MailAddrGrid.TabIndex = 185
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 341)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 25)
        Me.Label1.TabIndex = 186
        Me.Label1.Text = "ที่อยู่ออกใบกำกับ"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 465)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 25)
        Me.Label2.TabIndex = 187
        Me.Label2.Text = "ที่อยู่จัดส่ง"
        '
        'frmFINAR_MEMBER
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.BackgroundImage = Global.FTI.My.Resources.Resources.BGs
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(982, 653)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MailAddrGrid)
        Me.Controls.Add(Me.DocAddrGrid)
        Me.Controls.Add(Me.btFind)
        Me.Controls.Add(Me.btDelete)
        Me.Controls.Add(Me.btNew)
        Me.Controls.Add(Me.btApply)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFINAR_MEMBER"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ค้นหาลูกหนี้"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DocAddrGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MailAddrGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents btApply As System.Windows.Forms.Button
    Friend WithEvents btNew As System.Windows.Forms.Button
    Friend WithEvents btDelete As System.Windows.Forms.Button
    Friend WithEvents btFind As System.Windows.Forms.Button
    Friend WithEvents DocAddrGrid As System.Windows.Forms.DataGridView
    Friend WithEvents MailAddrGrid As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
