Public Class frmFINSubSectionManage

    Private Sub frmFINSubSectionManage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'FTIDataSet.IV_SUB_SECTION' table. You can move, or remove it, as needed.
        Me.IV_SUB_SECTIONTableAdapter.Fill(Me.FTIDataSet.IV_SUB_SECTION)

    End Sub
End Class