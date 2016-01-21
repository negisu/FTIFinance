Public Class frmFINReceiptCancle
    Dim parameters As New Dictionary(Of String, Object)

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If (MessageBox.Show("คุณต้องการที่จะยื่นคำร้องยกเลิกเอกสารหมายเลข " & DocCode.Text & " ใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
            Dim query As String
            query = String.Empty


            query = "UPDATE FN_RECEIPT_MASTER SET CANCEL_FLAG = @p0, CANCEL_REASON = @p1, CANCEL_BY = @p2 WHERE RECEIPT_CODE = '" & DocCode.Text & "'"

            parameters.Clear()
            parameters.Add("@p0", "P")
            parameters.Add("@p1", CANCEL_REASONComboBox.SelectedValue)
            parameters.Add("@p2", user_name)

            Try
                executeWebSQL(query, parameters)
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            End Try
            Me.Dispose()
        End If
    End Sub

    Private Sub frmFINPaymentNoticeCancle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CANCEL_REASONComboBox.DataSource = QueryHelper.selectStar("FN_DOC_CANCEL_REASON")
        CANCEL_REASONComboBox.DisplayMember = "CANCEL_REASON"
        CANCEL_REASONComboBox.ValueMember = "CANCEL_REASON"
    End Sub
End Class