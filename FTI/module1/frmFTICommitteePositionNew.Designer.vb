<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFTICommitteePositionNew
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFTICommitteePositionNew))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.TextBox30 = New System.Windows.Forms.TextBox()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.btWORKgroup = New System.Windows.Forms.Button()
        Me.btPositions = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(407, 67)
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
        Me.OK_Button.BackgroundImage = CType(resources.GetObject("OK_Button.BackgroundImage"), System.Drawing.Image)
        Me.OK_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.OK_Button.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
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
        Me.Cancel_Button.BackgroundImage = CType(resources.GetObject("Cancel_Button.BackgroundImage"), System.Drawing.Image)
        Me.Cancel_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Cancel_Button.ForeColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        Me.Cancel_Button.UseVisualStyleBackColor = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!)
        Me.Label12.Location = New System.Drawing.Point(28, 15)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(43, 16)
        Me.Label12.TabIndex = 28
        Me.Label12.Text = "ชื่อคณะ"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!)
        Me.Label27.Location = New System.Drawing.Point(15, 41)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(60, 16)
        Me.Label27.TabIndex = 38
        Me.Label27.Text = "ชื่อตำแหน่ง"
        '
        'TextBox30
        '
        Me.TextBox30.Location = New System.Drawing.Point(83, 38)
        Me.TextBox30.Name = "TextBox30"
        Me.TextBox30.ReadOnly = True
        Me.TextBox30.Size = New System.Drawing.Size(394, 20)
        Me.TextBox30.TabIndex = 36
        '
        'TextBox9
        '
        Me.TextBox9.Location = New System.Drawing.Point(83, 12)
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.ReadOnly = True
        Me.TextBox9.Size = New System.Drawing.Size(394, 20)
        Me.TextBox9.TabIndex = 26
        '
        'btWORKgroup
        '
        Me.btWORKgroup.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btWORKgroup.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btWORKgroup.BackgroundImage = CType(resources.GetObject("btWORKgroup.BackgroundImage"), System.Drawing.Image)
        Me.btWORKgroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btWORKgroup.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btWORKgroup.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btWORKgroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btWORKgroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btWORKgroup.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btWORKgroup.Location = New System.Drawing.Point(483, 11)
        Me.btWORKgroup.Name = "btWORKgroup"
        Me.btWORKgroup.Size = New System.Drawing.Size(67, 23)
        Me.btWORKgroup.TabIndex = 39
        Me.btWORKgroup.Text = "คณะ..."
        Me.btWORKgroup.UseVisualStyleBackColor = False
        '
        'btPositions
        '
        Me.btPositions.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btPositions.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btPositions.BackgroundImage = CType(resources.GetObject("btPositions.BackgroundImage"), System.Drawing.Image)
        Me.btPositions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btPositions.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btPositions.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btPositions.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btPositions.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btPositions.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btPositions.Location = New System.Drawing.Point(483, 37)
        Me.btPositions.Name = "btPositions"
        Me.btPositions.Size = New System.Drawing.Size(67, 23)
        Me.btPositions.TabIndex = 40
        Me.btPositions.Text = "ตำแหน่ง..."
        Me.btPositions.UseVisualStyleBackColor = False
        '
        'frmFTICommiteePositionNew
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.BackgroundImage = Global.FTI.My.Resources.Resources.BGs
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(565, 108)
        Me.Controls.Add(Me.btPositions)
        Me.Controls.Add(Me.btWORKgroup)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.TextBox30)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.TextBox9)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFTICommiteePositionNew"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmFTICommiteePositionNew"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents TextBox30 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox9 As System.Windows.Forms.TextBox
    Friend WithEvents btWORKgroup As System.Windows.Forms.Button
    Friend WithEvents btPositions As System.Windows.Forms.Button

End Class
