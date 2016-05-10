Public Class frmFINOldFashionedReport

    Private Sub frmFINOldFashionedReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Function sortedCriteria(query As String) As String
        query &= " ORDER BY "
        If ISMEMBER_CODE.Checked Then
            query &= " PN_HEAD.MB_MEMBER_CODE "
        End If
        If ISCOMP_PERSON_NAME_TH.Checked Then
            query &= " MB_COMP_PERSON.COMP_PERSON_CODE_TH "
        End If
        If ISTRAN_DATE.Checked Then
            query &= " PN_HEAD.TRAN_DATE "
        End If
        If ISTRAN_NO.Checked Then
            query &= " PN_HEAD.TRAN_NO "
        End If
        If ISGRAND_AMT.Checked Then
            query &= " TEST "
        End If





        If SortDirectionComboBox.SelectedIndex = 0 Then
            query &= " DESC "
        ElseIf SortDirectionComboBox.SelectedIndex = 1 Then
            query &= " ASC "
        Else
            Return query
        End If
        Return query
    End Function

    Private Sub TextBox1_DoubleClick(sender As Object, e As EventArgs) Handles TextBox1.DoubleClick

    End Sub

    Private Sub TextBox6_DoubleClick(sender As Object, e As EventArgs) Handles TextBox6.DoubleClick

    End Sub

    Private Sub FROM_DATEPicker_ValueChanged(sender As Object, e As EventArgs) Handles FROM_DATEPicker.ValueChanged
        If Not FROM_DATEPicker.IsHandleCreated Then
            Return
        Else
            FROM_DATEPicker.Value = New Date(FROM_DATEPicker.Value.Year, FROM_DATEPicker.Value.Month, FROM_DATEPicker.Value.Day, 0, 0, 0)
        End If
        TO_DATEPicker.MinDate = FROM_DATEPicker.Value
    End Sub

    Private Sub TO_DATEPicker_ValueChanged(sender As Object, e As EventArgs) Handles TO_DATEPicker.ValueChanged
        If Not TO_DATEPicker.IsHandleCreated Then
            Return
        Else
            TO_DATEPicker.Value = New Date(TO_DATEPicker.Value.Year, TO_DATEPicker.Value.Month, TO_DATEPicker.Value.Day, 23, 59, 59)
        End If
    End Sub
End Class