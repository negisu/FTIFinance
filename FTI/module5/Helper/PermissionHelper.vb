Public Class PermissionHelper
    Public Shared Function isAdmin() As Boolean
        If user_div = "AAA-AA" Or _
            user_div = "112-99" Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
