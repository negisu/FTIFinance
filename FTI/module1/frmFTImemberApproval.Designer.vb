﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFTImemberApproval
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btPrintPreview = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btFind = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(13, 39)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(859, 422)
        Me.DataGridView1.TabIndex = 0
        '
        'btPrintPreview
        '
        Me.btPrintPreview.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btPrintPreview.BackgroundImage = Global.FTI.My.Resources.Resources.imgBGbtn
        Me.btPrintPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btPrintPreview.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.btPrintPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btPrintPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btPrintPreview.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btPrintPreview.Location = New System.Drawing.Point(387, 467)
        Me.btPrintPreview.Name = "btPrintPreview"
        Me.btPrintPreview.Size = New System.Drawing.Size(110, 30)
        Me.btPrintPreview.TabIndex = 1
        Me.btPrintPreview.Text = "ตัวอย่างก่อนพิมพ์"
        Me.btPrintPreview.UseVisualStyleBackColor = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "MMMM yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(51, 12)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker1.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!)
        Me.Label2.Location = New System.Drawing.Point(16, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 16)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "วันที่"
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
        Me.btFind.Location = New System.Drawing.Point(257, 4)
        Me.btFind.Name = "btFind"
        Me.btFind.Size = New System.Drawing.Size(75, 30)
        Me.btFind.TabIndex = 12
        Me.btFind.Text = "ค้นหา"
        Me.btFind.UseVisualStyleBackColor = False
        '
        'frmFTImemberApproval
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.BackgroundImage = Global.FTI.My.Resources.Resources.BGs
        Me.ClientSize = New System.Drawing.Size(884, 501)
        Me.Controls.Add(Me.btFind)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.btPrintPreview)
        Me.Controls.Add(Me.DataGridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFTImemberApproval"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmFTImemberApproval"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btPrintPreview As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btFind As System.Windows.Forms.Button
End Class
