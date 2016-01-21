<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGS1smes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGS1smes))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btSaveAs = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btExecute = New System.Windows.Forms.Button()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.btDelete = New System.Windows.Forms.Button()
        Me.btNew = New System.Windows.Forms.Button()
        Me.btApply = New System.Windows.Forms.Button()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(13, 232)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(859, 232)
        Me.DataGridView1.TabIndex = 0
        '
        'btSaveAs
        '
        Me.btSaveAs.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btSaveAs.BackgroundImage = CType(resources.GetObject("btSaveAs.BackgroundImage"), System.Drawing.Image)
        Me.btSaveAs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btSaveAs.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btSaveAs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btSaveAs.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btSaveAs.Location = New System.Drawing.Point(376, 470)
        Me.btSaveAs.Name = "btSaveAs"
        Me.btSaveAs.Size = New System.Drawing.Size(132, 25)
        Me.btSaveAs.TabIndex = 1
        Me.btSaveAs.Text = "บันทึกเป็น Excel (*.xlsx)"
        Me.btSaveAs.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.AcceptsReturn = True
        Me.TextBox1.Location = New System.Drawing.Point(13, 12)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox1.Size = New System.Drawing.Size(616, 185)
        Me.TextBox1.TabIndex = 5
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
        Me.btExecute.Location = New System.Drawing.Point(376, 202)
        Me.btExecute.Name = "btExecute"
        Me.btExecute.Size = New System.Drawing.Size(132, 25)
        Me.btExecute.TabIndex = 6
        Me.btExecute.Text = "ดำเนินการ"
        Me.btExecute.UseVisualStyleBackColor = False
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
        Me.DataGridView2.Location = New System.Drawing.Point(635, 12)
        Me.DataGridView2.MultiSelect = False
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(237, 147)
        Me.DataGridView2.TabIndex = 7
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
        Me.btDelete.Location = New System.Drawing.Point(716, 165)
        Me.btDelete.Name = "btDelete"
        Me.btDelete.Size = New System.Drawing.Size(75, 32)
        Me.btDelete.TabIndex = 13
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
        Me.btNew.Location = New System.Drawing.Point(635, 165)
        Me.btNew.Name = "btNew"
        Me.btNew.Size = New System.Drawing.Size(75, 32)
        Me.btNew.TabIndex = 12
        Me.btNew.Text = "เพิ่ม   "
        Me.btNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btNew.UseVisualStyleBackColor = False
        '
        'btApply
        '
        Me.btApply.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btApply.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btApply.BackgroundImage = CType(resources.GetObject("btApply.BackgroundImage"), System.Drawing.Image)
        Me.btApply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btApply.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btApply.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btApply.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btApply.Image = Global.FTI.My.Resources.Resources.imgSave
        Me.btApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btApply.Location = New System.Drawing.Point(797, 165)
        Me.btApply.Name = "btApply"
        Me.btApply.Size = New System.Drawing.Size(75, 32)
        Me.btApply.TabIndex = 14
        Me.btApply.Text = "บันทึก"
        Me.btApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btApply.UseVisualStyleBackColor = False
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 477)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Label1"
        '
        'frmFINsmes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.BackgroundImage = Global.FTI.My.Resources.Resources.BGs
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(884, 501)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btApply)
        Me.Controls.Add(Me.btDelete)
        Me.Controls.Add(Me.btNew)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.btExecute)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.btSaveAs)
        Me.Controls.Add(Me.DataGridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFINsmes"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmFINsmes"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btSaveAs As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents btExecute As System.Windows.Forms.Button
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents btDelete As System.Windows.Forms.Button
    Friend WithEvents btNew As System.Windows.Forms.Button
    Friend WithEvents btApply As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
