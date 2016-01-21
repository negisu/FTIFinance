<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGS1selectNew
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGS1selectNew))
        Me.btNeverUse = New System.Windows.Forms.Button()
        Me.btResell = New System.Windows.Forms.Button()
        Me.btReserved = New System.Windows.Forms.Button()
        Me.btManual = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btNeverUse
        '
        Me.btNeverUse.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btNeverUse.BackgroundImage = CType(resources.GetObject("btNeverUse.BackgroundImage"), System.Drawing.Image)
        Me.btNeverUse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btNeverUse.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btNeverUse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btNeverUse.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btNeverUse.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btNeverUse.Image = Global.FTI.My.Resources.Resources.imgAdd
        Me.btNeverUse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btNeverUse.Location = New System.Drawing.Point(12, 12)
        Me.btNeverUse.Name = "btNeverUse"
        Me.btNeverUse.Size = New System.Drawing.Size(284, 32)
        Me.btNeverUse.TabIndex = 13
        Me.btNeverUse.Text = "เลขหมายที่ยังไม่ถูกนำมาใช้งาน"
        Me.btNeverUse.UseVisualStyleBackColor = False
        '
        'btResell
        '
        Me.btResell.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btResell.BackgroundImage = CType(resources.GetObject("btResell.BackgroundImage"), System.Drawing.Image)
        Me.btResell.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btResell.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btResell.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btResell.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btResell.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btResell.Image = Global.FTI.My.Resources.Resources.imgAdd
        Me.btResell.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btResell.Location = New System.Drawing.Point(12, 50)
        Me.btResell.Name = "btResell"
        Me.btResell.Size = New System.Drawing.Size(284, 32)
        Me.btResell.TabIndex = 15
        Me.btResell.Text = "เลขหมาย Resell"
        Me.btResell.UseVisualStyleBackColor = False
        '
        'btReserved
        '
        Me.btReserved.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btReserved.BackgroundImage = CType(resources.GetObject("btReserved.BackgroundImage"), System.Drawing.Image)
        Me.btReserved.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btReserved.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btReserved.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btReserved.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btReserved.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btReserved.Image = Global.FTI.My.Resources.Resources.imgAdd
        Me.btReserved.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btReserved.Location = New System.Drawing.Point(12, 88)
        Me.btReserved.Name = "btReserved"
        Me.btReserved.Size = New System.Drawing.Size(284, 32)
        Me.btReserved.TabIndex = 16
        Me.btReserved.Text = "เลขหมายจากการจอง"
        Me.btReserved.UseVisualStyleBackColor = False
        '
        'btManual
        '
        Me.btManual.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btManual.BackgroundImage = CType(resources.GetObject("btManual.BackgroundImage"), System.Drawing.Image)
        Me.btManual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btManual.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btManual.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btManual.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btManual.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btManual.Image = Global.FTI.My.Resources.Resources.imgAdd
        Me.btManual.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btManual.Location = New System.Drawing.Point(12, 126)
        Me.btManual.Name = "btManual"
        Me.btManual.Size = New System.Drawing.Size(284, 32)
        Me.btManual.TabIndex = 17
        Me.btManual.Text = "เลขหมายตามผู้ใช้กำหนด"
        Me.btManual.UseVisualStyleBackColor = False
        '
        'frmGS1selectNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.BackgroundImage = Global.FTI.My.Resources.Resources.BGs
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(312, 173)
        Me.Controls.Add(Me.btManual)
        Me.Controls.Add(Me.btReserved)
        Me.Controls.Add(Me.btResell)
        Me.Controls.Add(Me.btNeverUse)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGS1selectNew"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmGS1selectNew"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btNeverUse As System.Windows.Forms.Button
    Friend WithEvents btResell As System.Windows.Forms.Button
    Friend WithEvents btReserved As System.Windows.Forms.Button
    Friend WithEvents btManual As System.Windows.Forms.Button

End Class
