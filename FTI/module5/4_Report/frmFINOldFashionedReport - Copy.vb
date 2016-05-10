Public Class frmFINOldFashionedReport1


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
End Class