<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFTICommitteeGroupsReports
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
        Me.btFind = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btPrint = New System.Windows.Forms.Button()
        Me.CheckedComboBox1 = New FTI.CheckComboBox.CheckedComboBox()
        Me.CheckedComboBox2 = New FTI.CheckComboBox.CheckedComboBox()
        Me.btExport = New System.Windows.Forms.Button()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel4 = New System.Windows.Forms.LinkLabel()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btFind
        '
        Me.btFind.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btFind.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.btFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btFind.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btFind.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btFind.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btFind.Image = Global.FTI.My.Resources.Resources.imgSearch
        Me.btFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btFind.Location = New System.Drawing.Point(340, 65)
        Me.btFind.Name = "btFind"
        Me.btFind.Size = New System.Drawing.Size(84, 30)
        Me.btFind.TabIndex = 3
        Me.btFind.Text = "ค้นหา   "
        Me.btFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btFind.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!)
        Me.Label1.Location = New System.Drawing.Point(12, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "วาระปี"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!)
        Me.Label2.Location = New System.Drawing.Point(24, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 16)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "ฝ่าย"
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
        Me.DataGridView1.Location = New System.Drawing.Point(12, 101)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(760, 419)
        Me.DataGridView1.TabIndex = 7
        '
        'btPrint
        '
        Me.btPrint.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btPrint.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.btPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btPrint.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btPrint.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btPrint.Location = New System.Drawing.Point(279, 526)
        Me.btPrint.Name = "btPrint"
        Me.btPrint.Size = New System.Drawing.Size(110, 30)
        Me.btPrint.TabIndex = 8
        Me.btPrint.Text = "ตัวอย่างก่อนพิมพ์"
        Me.btPrint.UseVisualStyleBackColor = False
        '
        'CheckedComboBox1
        '
        Me.CheckedComboBox1.CheckOnClick = True
        Me.CheckedComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.CheckedComboBox1.DropDownHeight = 1
        Me.CheckedComboBox1.FormattingEnabled = True
        Me.CheckedComboBox1.IntegralHeight = False
        Me.CheckedComboBox1.Location = New System.Drawing.Point(56, 12)
        Me.CheckedComboBox1.Name = "CheckedComboBox1"
        Me.CheckedComboBox1.Size = New System.Drawing.Size(563, 21)
        Me.CheckedComboBox1.TabIndex = 11
        Me.CheckedComboBox1.ValueSeparator = ", "
        '
        'CheckedComboBox2
        '
        Me.CheckedComboBox2.CheckOnClick = True
        Me.CheckedComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.CheckedComboBox2.DropDownHeight = 1
        Me.CheckedComboBox2.FormattingEnabled = True
        Me.CheckedComboBox2.IntegralHeight = False
        Me.CheckedComboBox2.Location = New System.Drawing.Point(56, 39)
        Me.CheckedComboBox2.Name = "CheckedComboBox2"
        Me.CheckedComboBox2.Size = New System.Drawing.Size(563, 21)
        Me.CheckedComboBox2.TabIndex = 12
        Me.CheckedComboBox2.ValueSeparator = ", "
        '
        'btExport
        '
        Me.btExport.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btExport.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.btExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btExport.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btExport.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btExport.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btExport.Location = New System.Drawing.Point(395, 526)
        Me.btExport.Name = "btExport"
        Me.btExport.Size = New System.Drawing.Size(110, 30)
        Me.btExport.TabIndex = 13
        Me.btExport.Text = "บันทึกเป็น XLSX"
        Me.btExport.UseVisualStyleBackColor = False
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(625, 15)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(64, 13)
        Me.LinkLabel1.TabIndex = 14
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "เลือกทั้งหมด"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Location = New System.Drawing.Point(695, 15)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(77, 13)
        Me.LinkLabel2.TabIndex = 15
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "ไม่เลือกทั้งหมด"
        '
        'LinkLabel3
        '
        Me.LinkLabel3.AutoSize = True
        Me.LinkLabel3.Location = New System.Drawing.Point(695, 42)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(77, 13)
        Me.LinkLabel3.TabIndex = 17
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "ไม่เลือกทั้งหมด"
        '
        'LinkLabel4
        '
        Me.LinkLabel4.AutoSize = True
        Me.LinkLabel4.Location = New System.Drawing.Point(625, 42)
        Me.LinkLabel4.Name = "LinkLabel4"
        Me.LinkLabel4.Size = New System.Drawing.Size(64, 13)
        Me.LinkLabel4.TabIndex = 16
        Me.LinkLabel4.TabStop = True
        Me.LinkLabel4.Text = "เลือกทั้งหมด"
        '
        'frmFTICommitteeGroupsReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.BackgroundImage = Global.FTI.My.Resources.Resources.BGs
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.LinkLabel3)
        Me.Controls.Add(Me.LinkLabel4)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.btExport)
        Me.Controls.Add(Me.CheckedComboBox2)
        Me.Controls.Add(Me.CheckedComboBox1)
        Me.Controls.Add(Me.btPrint)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btFind)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFTICommitteeGroupsReports"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmFTICommitteeGroupsReports"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btFind As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btPrint As System.Windows.Forms.Button
    Friend WithEvents CheckedComboBox1 As CheckComboBox.CheckedComboBox
    Friend WithEvents CheckedComboBox2 As CheckComboBox.CheckedComboBox
    Friend WithEvents btExport As System.Windows.Forms.Button
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel3 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel4 As System.Windows.Forms.LinkLabel
End Class
