<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainProducts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainProducts))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridView10 = New System.Windows.Forms.DataGridView()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btFindIndus = New System.Windows.Forms.Button()
        Me.btFindProd = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.tbMAIN_INDUSTRY_CODE = New System.Windows.Forms.TextBox()
        Me.tbPRODUCT_CODE = New System.Windows.Forms.TextBox()
        Me.lseparator1 = New System.Windows.Forms.Label()
        Me.btDeleteIndus = New System.Windows.Forms.Button()
        Me.btNewIndus = New System.Windows.Forms.Button()
        Me.btApplyIndus = New System.Windows.Forms.Button()
        Me.btDelProduct = New System.Windows.Forms.Button()
        Me.btNewProduct = New System.Windows.Forms.Button()
        Me.btApplyProduct = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.DataGridView10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(508, 561)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.BackColor = System.Drawing.Color.PaleTurquoise
        Me.OK_Button.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.OK_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.OK_Button.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.ForeColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        Me.OK_Button.UseVisualStyleBackColor = False
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.BackColor = System.Drawing.Color.PaleTurquoise
        Me.Cancel_Button.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.Cancel_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Button.ForeColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        Me.Cancel_Button.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!)
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "อุตสาหกรรม"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!)
        Me.Label2.Location = New System.Drawing.Point(27, 317)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 16)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "ผลิตภัณฑ์"
        '
        'DataGridView10
        '
        Me.DataGridView10.AllowUserToAddRows = False
        Me.DataGridView10.AllowUserToDeleteRows = False
        Me.DataGridView10.AllowUserToResizeRows = False
        Me.DataGridView10.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView10.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.DataGridView10.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView10.Location = New System.Drawing.Point(12, 38)
        Me.DataGridView10.Name = "DataGridView10"
        Me.DataGridView10.Size = New System.Drawing.Size(642, 221)
        Me.DataGridView10.TabIndex = 46
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 342)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(642, 213)
        Me.DataGridView1.TabIndex = 47
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(85, 12)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(488, 20)
        Me.TextBox1.TabIndex = 48
        '
        'btFindIndus
        '
        Me.btFindIndus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btFindIndus.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btFindIndus.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.btFindIndus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btFindIndus.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btFindIndus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btFindIndus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btFindIndus.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btFindIndus.Image = Global.FTI.My.Resources.Resources.imgSearch
        Me.btFindIndus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btFindIndus.Location = New System.Drawing.Point(579, 10)
        Me.btFindIndus.Name = "btFindIndus"
        Me.btFindIndus.Size = New System.Drawing.Size(75, 23)
        Me.btFindIndus.TabIndex = 63
        Me.btFindIndus.Text = "ค้นหา"
        Me.btFindIndus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btFindIndus.UseVisualStyleBackColor = False
        '
        'btFindProd
        '
        Me.btFindProd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btFindProd.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btFindProd.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.btFindProd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btFindProd.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btFindProd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btFindProd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btFindProd.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btFindProd.Image = Global.FTI.My.Resources.Resources.imgSearch
        Me.btFindProd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btFindProd.Location = New System.Drawing.Point(579, 314)
        Me.btFindProd.Name = "btFindProd"
        Me.btFindProd.Size = New System.Drawing.Size(75, 23)
        Me.btFindProd.TabIndex = 65
        Me.btFindProd.Text = "ค้นหา"
        Me.btFindProd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btFindProd.UseVisualStyleBackColor = False
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox2.Location = New System.Drawing.Point(85, 316)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(488, 20)
        Me.TextBox2.TabIndex = 64
        '
        'tbMAIN_INDUSTRY_CODE
        '
        Me.tbMAIN_INDUSTRY_CODE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbMAIN_INDUSTRY_CODE.Location = New System.Drawing.Point(486, 277)
        Me.tbMAIN_INDUSTRY_CODE.Name = "tbMAIN_INDUSTRY_CODE"
        Me.tbMAIN_INDUSTRY_CODE.ReadOnly = True
        Me.tbMAIN_INDUSTRY_CODE.Size = New System.Drawing.Size(81, 20)
        Me.tbMAIN_INDUSTRY_CODE.TabIndex = 71
        '
        'tbPRODUCT_CODE
        '
        Me.tbPRODUCT_CODE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbPRODUCT_CODE.Location = New System.Drawing.Point(573, 277)
        Me.tbPRODUCT_CODE.Name = "tbPRODUCT_CODE"
        Me.tbPRODUCT_CODE.ReadOnly = True
        Me.tbPRODUCT_CODE.Size = New System.Drawing.Size(81, 20)
        Me.tbPRODUCT_CODE.TabIndex = 72
        '
        'lseparator1
        '
        Me.lseparator1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lseparator1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lseparator1.Location = New System.Drawing.Point(12, 306)
        Me.lseparator1.Name = "lseparator1"
        Me.lseparator1.Size = New System.Drawing.Size(642, 2)
        Me.lseparator1.TabIndex = 73
        '
        'btDeleteIndus
        '
        Me.btDeleteIndus.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btDeleteIndus.BackgroundImage = CType(resources.GetObject("btDeleteIndus.BackgroundImage"), System.Drawing.Image)
        Me.btDeleteIndus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btDeleteIndus.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btDeleteIndus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btDeleteIndus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btDeleteIndus.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btDeleteIndus.Image = Global.FTI.My.Resources.Resources.imgDelete
        Me.btDeleteIndus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btDeleteIndus.Location = New System.Drawing.Point(93, 265)
        Me.btDeleteIndus.Name = "btDeleteIndus"
        Me.btDeleteIndus.Size = New System.Drawing.Size(75, 32)
        Me.btDeleteIndus.TabIndex = 78
        Me.btDeleteIndus.Text = "ลบ   "
        Me.btDeleteIndus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btDeleteIndus.UseVisualStyleBackColor = False
        '
        'btNewIndus
        '
        Me.btNewIndus.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btNewIndus.BackgroundImage = CType(resources.GetObject("btNewIndus.BackgroundImage"), System.Drawing.Image)
        Me.btNewIndus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btNewIndus.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btNewIndus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btNewIndus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btNewIndus.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btNewIndus.Image = Global.FTI.My.Resources.Resources.imgAdd
        Me.btNewIndus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btNewIndus.Location = New System.Drawing.Point(12, 265)
        Me.btNewIndus.Name = "btNewIndus"
        Me.btNewIndus.Size = New System.Drawing.Size(75, 32)
        Me.btNewIndus.TabIndex = 77
        Me.btNewIndus.Text = "เพิ่ม  "
        Me.btNewIndus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btNewIndus.UseVisualStyleBackColor = False
        '
        'btApplyIndus
        '
        Me.btApplyIndus.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btApplyIndus.BackgroundImage = CType(resources.GetObject("btApplyIndus.BackgroundImage"), System.Drawing.Image)
        Me.btApplyIndus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btApplyIndus.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btApplyIndus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btApplyIndus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btApplyIndus.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btApplyIndus.Image = Global.FTI.My.Resources.Resources.imgSave
        Me.btApplyIndus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btApplyIndus.Location = New System.Drawing.Point(296, 265)
        Me.btApplyIndus.Name = "btApplyIndus"
        Me.btApplyIndus.Size = New System.Drawing.Size(75, 32)
        Me.btApplyIndus.TabIndex = 76
        Me.btApplyIndus.Text = "บันทึก"
        Me.btApplyIndus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btApplyIndus.UseVisualStyleBackColor = False
        '
        'btDelProduct
        '
        Me.btDelProduct.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btDelProduct.BackgroundImage = CType(resources.GetObject("btDelProduct.BackgroundImage"), System.Drawing.Image)
        Me.btDelProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btDelProduct.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btDelProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btDelProduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btDelProduct.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btDelProduct.Image = Global.FTI.My.Resources.Resources.imgDelete
        Me.btDelProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btDelProduct.Location = New System.Drawing.Point(93, 561)
        Me.btDelProduct.Name = "btDelProduct"
        Me.btDelProduct.Size = New System.Drawing.Size(75, 32)
        Me.btDelProduct.TabIndex = 81
        Me.btDelProduct.Text = "ลบ   "
        Me.btDelProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btDelProduct.UseVisualStyleBackColor = False
        '
        'btNewProduct
        '
        Me.btNewProduct.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btNewProduct.BackgroundImage = CType(resources.GetObject("btNewProduct.BackgroundImage"), System.Drawing.Image)
        Me.btNewProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btNewProduct.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btNewProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btNewProduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btNewProduct.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btNewProduct.Image = Global.FTI.My.Resources.Resources.imgAdd
        Me.btNewProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btNewProduct.Location = New System.Drawing.Point(12, 561)
        Me.btNewProduct.Name = "btNewProduct"
        Me.btNewProduct.Size = New System.Drawing.Size(75, 32)
        Me.btNewProduct.TabIndex = 80
        Me.btNewProduct.Text = "เพิ่ม  "
        Me.btNewProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btNewProduct.UseVisualStyleBackColor = False
        '
        'btApplyProduct
        '
        Me.btApplyProduct.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btApplyProduct.BackgroundImage = CType(resources.GetObject("btApplyProduct.BackgroundImage"), System.Drawing.Image)
        Me.btApplyProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btApplyProduct.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btApplyProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btApplyProduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btApplyProduct.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btApplyProduct.Image = Global.FTI.My.Resources.Resources.imgSave
        Me.btApplyProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btApplyProduct.Location = New System.Drawing.Point(296, 561)
        Me.btApplyProduct.Name = "btApplyProduct"
        Me.btApplyProduct.Size = New System.Drawing.Size(75, 32)
        Me.btApplyProduct.TabIndex = 79
        Me.btApplyProduct.Text = "บันทึก"
        Me.btApplyProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btApplyProduct.UseVisualStyleBackColor = False
        '
        'frmMainProducts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.BackgroundImage = Global.FTI.My.Resources.Resources.BGs
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(666, 602)
        Me.Controls.Add(Me.btDelProduct)
        Me.Controls.Add(Me.btNewProduct)
        Me.Controls.Add(Me.btApplyProduct)
        Me.Controls.Add(Me.btDeleteIndus)
        Me.Controls.Add(Me.btNewIndus)
        Me.Controls.Add(Me.btApplyIndus)
        Me.Controls.Add(Me.lseparator1)
        Me.Controls.Add(Me.tbPRODUCT_CODE)
        Me.Controls.Add(Me.tbMAIN_INDUSTRY_CODE)
        Me.Controls.Add(Me.btFindProd)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.btFindIndus)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.DataGridView10)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMainProducts"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmMainProducts"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.DataGridView10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DataGridView10 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents btFindIndus As System.Windows.Forms.Button
    Friend WithEvents btFindProd As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents tbMAIN_INDUSTRY_CODE As System.Windows.Forms.TextBox
    Friend WithEvents tbPRODUCT_CODE As System.Windows.Forms.TextBox
    Friend WithEvents lseparator1 As System.Windows.Forms.Label
    Friend WithEvents btDeleteIndus As System.Windows.Forms.Button
    Friend WithEvents btNewIndus As System.Windows.Forms.Button
    Friend WithEvents btApplyIndus As System.Windows.Forms.Button
    Friend WithEvents btDelProduct As System.Windows.Forms.Button
    Friend WithEvents btNewProduct As System.Windows.Forms.Button
    Friend WithEvents btApplyProduct As System.Windows.Forms.Button

End Class
