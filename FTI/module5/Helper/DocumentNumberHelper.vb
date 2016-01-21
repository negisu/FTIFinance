Public Class DocumentNumberHelper
    Public Shared Function getIV_DOC_RUNNING(OU_CODE As String, DIV_CODE As String, BUDGET_YEAR As String, SUB_TYPE As String) As String

        Dim searchValue As String = OU_CODE & DIV_CODE & BUDGET_YEAR & SUB_TYPE
        searchValue = "%" & searchValue & "%"

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", searchValue)

        Dim query As String = String.Empty
        query &= "SELECT TOP 1  * "
        query &= "FROM            IV_DOC_RUNNING "
        query &= "WHERE DOC_RUNNING_NO LIKE @p0 "
        query &= "ORDER BY CR_DATE DESC"

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "IV_DOC_RUNNING")

        dt = fillWebSQL(query, parameters, "PN_DOC_RUNNING")

        If dt.Rows.Count = 0 Then
            Return searchValue.Replace("%", String.Empty) & "000000"
        Else
            Dim DOC_RUNNING_NO As String = dt.Rows(0).Item("DOC_RUNNING_NO").ToString()

            Dim runningNumber As Integer = Integer.Parse(DOC_RUNNING_NO.Substring(DOC_RUNNING_NO.Length - 4, 4)) + 1
            Return searchValue.Replace("%", String.Empty) & String.Format("{0:000000}", runningNumber)
        End If

    End Function


    Public Shared Function getPN_DOC_RUNNING(OU_CODE As String, DIV_CODE As String, BUDGET_YEAR As String, SUB_TYPE As String) As String

        Dim searchValue As String = DIV_CODE & BUDGET_YEAR & SUB_TYPE
        searchValue = "%" & searchValue & "%"

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", searchValue)

        Dim query As String = String.Empty
        query &= "SELECT TOP 1  * "
        query &= "FROM            PN_DOC_RUNNING "
        query &= "WHERE DOC_RUNNING_NO LIKE @p0 "
        query &= "ORDER BY CR_DATE DESC"

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "PN_DOC_RUNNING")

        If dt.Rows.Count = 0 Then
            Return searchValue.Replace("%", String.Empty) & "000000"
        Else
            Dim DOC_RUNNING_NO As String = dt.Rows(0).Item("DOC_RUNNING_NO").ToString()

            Dim runningNumber As Integer = Integer.Parse(DOC_RUNNING_NO.Substring(DOC_RUNNING_NO.Length - 4, 4)) + 1
            Return searchValue.Replace("%", String.Empty) & String.Format("{0:000000}", runningNumber)
        End If


    End Function
End Class
