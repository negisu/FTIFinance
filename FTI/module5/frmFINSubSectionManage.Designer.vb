<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFINSubSectionManage
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
        Me.components = New System.ComponentModel.Container()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.FTIDataSet = New FTI.FTIDataSet()
        Me.IVSUBSECTIONBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.IV_SUB_SECTIONTableAdapter = New FTI.FTIDataSetTableAdapters.IV_SUB_SECTIONTableAdapter()
        Me.SUBSECTIONCODEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.STDPRICEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VATTYPEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VATRATEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CRBYDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CRDATEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UPDBYDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UPDDATEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROGIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BGSECCODEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INACTIVEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FLAGMBDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SUBSECTIONNAMEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SUBSECTIONNAMEENDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DIVCODEINCDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PROJIDINCDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ATVCODEINCDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PREPAYFLAGDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PREPAYREFCODEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FLAGARDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TRANTYPEOLDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FLAGFNVOUCHERDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AVTCODEINC1DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FLAGACCOUNTDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FLAGPNDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FLAGIVDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTIDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IVSUBSECTIONBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SUBSECTIONCODEDataGridViewTextBoxColumn, Me.STDPRICEDataGridViewTextBoxColumn, Me.VATTYPEDataGridViewTextBoxColumn, Me.VATRATEDataGridViewTextBoxColumn, Me.CRBYDataGridViewTextBoxColumn, Me.CRDATEDataGridViewTextBoxColumn, Me.UPDBYDataGridViewTextBoxColumn, Me.UPDDATEDataGridViewTextBoxColumn, Me.PROGIDDataGridViewTextBoxColumn, Me.BGSECCODEDataGridViewTextBoxColumn, Me.INACTIVEDataGridViewTextBoxColumn, Me.FLAGMBDataGridViewTextBoxColumn, Me.SUBSECTIONNAMEDataGridViewTextBoxColumn, Me.SUBSECTIONNAMEENDataGridViewTextBoxColumn, Me.DIVCODEINCDataGridViewTextBoxColumn, Me.PROJIDINCDataGridViewTextBoxColumn, Me.ATVCODEINCDataGridViewTextBoxColumn, Me.PREPAYFLAGDataGridViewTextBoxColumn, Me.PREPAYREFCODEDataGridViewTextBoxColumn, Me.FLAGARDataGridViewTextBoxColumn, Me.TRANTYPEOLDDataGridViewTextBoxColumn, Me.FLAGFNVOUCHERDataGridViewTextBoxColumn, Me.AVTCODEINC1DataGridViewTextBoxColumn, Me.FLAGACCOUNTDataGridViewTextBoxColumn, Me.FLAGPNDataGridViewTextBoxColumn, Me.FLAGIVDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.IVSUBSECTIONBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(12, 12)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(866, 499)
        Me.DataGridView1.TabIndex = 0
        '
        'FTIDataSet
        '
        Me.FTIDataSet.DataSetName = "FTIDataSet"
        Me.FTIDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'IVSUBSECTIONBindingSource
        '
        Me.IVSUBSECTIONBindingSource.DataMember = "IV_SUB_SECTION"
        Me.IVSUBSECTIONBindingSource.DataSource = Me.FTIDataSet
        '
        'IV_SUB_SECTIONTableAdapter
        '
        Me.IV_SUB_SECTIONTableAdapter.ClearBeforeFill = True
        '
        'SUBSECTIONCODEDataGridViewTextBoxColumn
        '
        Me.SUBSECTIONCODEDataGridViewTextBoxColumn.DataPropertyName = "SUB_SECTION_CODE"
        Me.SUBSECTIONCODEDataGridViewTextBoxColumn.HeaderText = "SUB_SECTION_CODE"
        Me.SUBSECTIONCODEDataGridViewTextBoxColumn.Name = "SUBSECTIONCODEDataGridViewTextBoxColumn"
        '
        'STDPRICEDataGridViewTextBoxColumn
        '
        Me.STDPRICEDataGridViewTextBoxColumn.DataPropertyName = "STD_PRICE"
        Me.STDPRICEDataGridViewTextBoxColumn.HeaderText = "STD_PRICE"
        Me.STDPRICEDataGridViewTextBoxColumn.Name = "STDPRICEDataGridViewTextBoxColumn"
        '
        'VATTYPEDataGridViewTextBoxColumn
        '
        Me.VATTYPEDataGridViewTextBoxColumn.DataPropertyName = "VAT_TYPE"
        Me.VATTYPEDataGridViewTextBoxColumn.HeaderText = "VAT_TYPE"
        Me.VATTYPEDataGridViewTextBoxColumn.Name = "VATTYPEDataGridViewTextBoxColumn"
        '
        'VATRATEDataGridViewTextBoxColumn
        '
        Me.VATRATEDataGridViewTextBoxColumn.DataPropertyName = "VAT_RATE"
        Me.VATRATEDataGridViewTextBoxColumn.HeaderText = "VAT_RATE"
        Me.VATRATEDataGridViewTextBoxColumn.Name = "VATRATEDataGridViewTextBoxColumn"
        '
        'CRBYDataGridViewTextBoxColumn
        '
        Me.CRBYDataGridViewTextBoxColumn.DataPropertyName = "CR_BY"
        Me.CRBYDataGridViewTextBoxColumn.HeaderText = "CR_BY"
        Me.CRBYDataGridViewTextBoxColumn.Name = "CRBYDataGridViewTextBoxColumn"
        '
        'CRDATEDataGridViewTextBoxColumn
        '
        Me.CRDATEDataGridViewTextBoxColumn.DataPropertyName = "CR_DATE"
        Me.CRDATEDataGridViewTextBoxColumn.HeaderText = "CR_DATE"
        Me.CRDATEDataGridViewTextBoxColumn.Name = "CRDATEDataGridViewTextBoxColumn"
        '
        'UPDBYDataGridViewTextBoxColumn
        '
        Me.UPDBYDataGridViewTextBoxColumn.DataPropertyName = "UPD_BY"
        Me.UPDBYDataGridViewTextBoxColumn.HeaderText = "UPD_BY"
        Me.UPDBYDataGridViewTextBoxColumn.Name = "UPDBYDataGridViewTextBoxColumn"
        '
        'UPDDATEDataGridViewTextBoxColumn
        '
        Me.UPDDATEDataGridViewTextBoxColumn.DataPropertyName = "UPD_DATE"
        Me.UPDDATEDataGridViewTextBoxColumn.HeaderText = "UPD_DATE"
        Me.UPDDATEDataGridViewTextBoxColumn.Name = "UPDDATEDataGridViewTextBoxColumn"
        '
        'PROGIDDataGridViewTextBoxColumn
        '
        Me.PROGIDDataGridViewTextBoxColumn.DataPropertyName = "PROG_ID"
        Me.PROGIDDataGridViewTextBoxColumn.HeaderText = "PROG_ID"
        Me.PROGIDDataGridViewTextBoxColumn.Name = "PROGIDDataGridViewTextBoxColumn"
        '
        'BGSECCODEDataGridViewTextBoxColumn
        '
        Me.BGSECCODEDataGridViewTextBoxColumn.DataPropertyName = "BG_SEC_CODE"
        Me.BGSECCODEDataGridViewTextBoxColumn.HeaderText = "BG_SEC_CODE"
        Me.BGSECCODEDataGridViewTextBoxColumn.Name = "BGSECCODEDataGridViewTextBoxColumn"
        '
        'INACTIVEDataGridViewTextBoxColumn
        '
        Me.INACTIVEDataGridViewTextBoxColumn.DataPropertyName = "INACTIVE"
        Me.INACTIVEDataGridViewTextBoxColumn.HeaderText = "INACTIVE"
        Me.INACTIVEDataGridViewTextBoxColumn.Name = "INACTIVEDataGridViewTextBoxColumn"
        '
        'FLAGMBDataGridViewTextBoxColumn
        '
        Me.FLAGMBDataGridViewTextBoxColumn.DataPropertyName = "FLAG_MB"
        Me.FLAGMBDataGridViewTextBoxColumn.HeaderText = "FLAG_MB"
        Me.FLAGMBDataGridViewTextBoxColumn.Name = "FLAGMBDataGridViewTextBoxColumn"
        '
        'SUBSECTIONNAMEDataGridViewTextBoxColumn
        '
        Me.SUBSECTIONNAMEDataGridViewTextBoxColumn.DataPropertyName = "SUB_SECTION_NAME"
        Me.SUBSECTIONNAMEDataGridViewTextBoxColumn.HeaderText = "SUB_SECTION_NAME"
        Me.SUBSECTIONNAMEDataGridViewTextBoxColumn.Name = "SUBSECTIONNAMEDataGridViewTextBoxColumn"
        '
        'SUBSECTIONNAMEENDataGridViewTextBoxColumn
        '
        Me.SUBSECTIONNAMEENDataGridViewTextBoxColumn.DataPropertyName = "SUB_SECTION_NAME_EN"
        Me.SUBSECTIONNAMEENDataGridViewTextBoxColumn.HeaderText = "SUB_SECTION_NAME_EN"
        Me.SUBSECTIONNAMEENDataGridViewTextBoxColumn.Name = "SUBSECTIONNAMEENDataGridViewTextBoxColumn"
        '
        'DIVCODEINCDataGridViewTextBoxColumn
        '
        Me.DIVCODEINCDataGridViewTextBoxColumn.DataPropertyName = "DIV_CODE_INC"
        Me.DIVCODEINCDataGridViewTextBoxColumn.HeaderText = "DIV_CODE_INC"
        Me.DIVCODEINCDataGridViewTextBoxColumn.Name = "DIVCODEINCDataGridViewTextBoxColumn"
        '
        'PROJIDINCDataGridViewTextBoxColumn
        '
        Me.PROJIDINCDataGridViewTextBoxColumn.DataPropertyName = "PROJ_ID_INC"
        Me.PROJIDINCDataGridViewTextBoxColumn.HeaderText = "PROJ_ID_INC"
        Me.PROJIDINCDataGridViewTextBoxColumn.Name = "PROJIDINCDataGridViewTextBoxColumn"
        '
        'ATVCODEINCDataGridViewTextBoxColumn
        '
        Me.ATVCODEINCDataGridViewTextBoxColumn.DataPropertyName = "ATV_CODE_INC"
        Me.ATVCODEINCDataGridViewTextBoxColumn.HeaderText = "ATV_CODE_INC"
        Me.ATVCODEINCDataGridViewTextBoxColumn.Name = "ATVCODEINCDataGridViewTextBoxColumn"
        '
        'PREPAYFLAGDataGridViewTextBoxColumn
        '
        Me.PREPAYFLAGDataGridViewTextBoxColumn.DataPropertyName = "PRE_PAY_FLAG"
        Me.PREPAYFLAGDataGridViewTextBoxColumn.HeaderText = "PRE_PAY_FLAG"
        Me.PREPAYFLAGDataGridViewTextBoxColumn.Name = "PREPAYFLAGDataGridViewTextBoxColumn"
        '
        'PREPAYREFCODEDataGridViewTextBoxColumn
        '
        Me.PREPAYREFCODEDataGridViewTextBoxColumn.DataPropertyName = "PRE_PAY_REF_CODE"
        Me.PREPAYREFCODEDataGridViewTextBoxColumn.HeaderText = "PRE_PAY_REF_CODE"
        Me.PREPAYREFCODEDataGridViewTextBoxColumn.Name = "PREPAYREFCODEDataGridViewTextBoxColumn"
        '
        'FLAGARDataGridViewTextBoxColumn
        '
        Me.FLAGARDataGridViewTextBoxColumn.DataPropertyName = "FLAG_AR"
        Me.FLAGARDataGridViewTextBoxColumn.HeaderText = "FLAG_AR"
        Me.FLAGARDataGridViewTextBoxColumn.Name = "FLAGARDataGridViewTextBoxColumn"
        '
        'TRANTYPEOLDDataGridViewTextBoxColumn
        '
        Me.TRANTYPEOLDDataGridViewTextBoxColumn.DataPropertyName = "TRAN_TYPE_OLD"
        Me.TRANTYPEOLDDataGridViewTextBoxColumn.HeaderText = "TRAN_TYPE_OLD"
        Me.TRANTYPEOLDDataGridViewTextBoxColumn.Name = "TRANTYPEOLDDataGridViewTextBoxColumn"
        '
        'FLAGFNVOUCHERDataGridViewTextBoxColumn
        '
        Me.FLAGFNVOUCHERDataGridViewTextBoxColumn.DataPropertyName = "FLAG_FN_VOUCHER"
        Me.FLAGFNVOUCHERDataGridViewTextBoxColumn.HeaderText = "FLAG_FN_VOUCHER"
        Me.FLAGFNVOUCHERDataGridViewTextBoxColumn.Name = "FLAGFNVOUCHERDataGridViewTextBoxColumn"
        '
        'AVTCODEINC1DataGridViewTextBoxColumn
        '
        Me.AVTCODEINC1DataGridViewTextBoxColumn.DataPropertyName = "AVT_CODE_INC_1"
        Me.AVTCODEINC1DataGridViewTextBoxColumn.HeaderText = "AVT_CODE_INC_1"
        Me.AVTCODEINC1DataGridViewTextBoxColumn.Name = "AVTCODEINC1DataGridViewTextBoxColumn"
        '
        'FLAGACCOUNTDataGridViewTextBoxColumn
        '
        Me.FLAGACCOUNTDataGridViewTextBoxColumn.DataPropertyName = "FLAG_ACCOUNT"
        Me.FLAGACCOUNTDataGridViewTextBoxColumn.HeaderText = "FLAG_ACCOUNT"
        Me.FLAGACCOUNTDataGridViewTextBoxColumn.Name = "FLAGACCOUNTDataGridViewTextBoxColumn"
        '
        'FLAGPNDataGridViewTextBoxColumn
        '
        Me.FLAGPNDataGridViewTextBoxColumn.DataPropertyName = "FLAG_PN"
        Me.FLAGPNDataGridViewTextBoxColumn.HeaderText = "FLAG_PN"
        Me.FLAGPNDataGridViewTextBoxColumn.Name = "FLAGPNDataGridViewTextBoxColumn"
        '
        'FLAGIVDataGridViewTextBoxColumn
        '
        Me.FLAGIVDataGridViewTextBoxColumn.DataPropertyName = "FLAG_IV"
        Me.FLAGIVDataGridViewTextBoxColumn.HeaderText = "FLAG_IV"
        Me.FLAGIVDataGridViewTextBoxColumn.Name = "FLAGIVDataGridViewTextBoxColumn"
        '
        'frmFINSubSectionManage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(890, 523)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmFINSubSectionManage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmFINSubSectionManage"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTIDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IVSUBSECTIONBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents FTIDataSet As FTI.FTIDataSet
    Friend WithEvents IVSUBSECTIONBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents IV_SUB_SECTIONTableAdapter As FTI.FTIDataSetTableAdapters.IV_SUB_SECTIONTableAdapter
    Friend WithEvents SUBSECTIONCODEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents STDPRICEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VATTYPEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VATRATEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CRBYDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CRDATEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UPDBYDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UPDDATEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PROGIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BGSECCODEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INACTIVEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FLAGMBDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SUBSECTIONNAMEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SUBSECTIONNAMEENDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DIVCODEINCDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PROJIDINCDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ATVCODEINCDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PREPAYFLAGDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PREPAYREFCODEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FLAGARDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TRANTYPEOLDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FLAGFNVOUCHERDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AVTCODEINC1DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FLAGACCOUNTDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FLAGPNDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FLAGIVDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
