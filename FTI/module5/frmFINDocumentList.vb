Public Class frmFINDocumentList

    Private Sub btFind_Click(sender As Object, e As EventArgs) Handles btFind.Click
        Dim query As String = "SELECT * FROM "
        Dim parameters As New Dictionary(Of String, Object)
        If ComboBox1.SelectedIndex = 0 Then
            query &= "PN_HEAD"
        ElseIf ComboBox1.SelectedIndex = 1 Then
            query &= "IV_HEAD"
        ElseIf ComboBox1.SelectedIndex = 2 Then
            query &= "FN_RECEIPT_MASTER"

        End If
        query &= " WHERE BAL_AMT > @p0"
        parameters.Add("@p0", 0)
    End Sub
End Class