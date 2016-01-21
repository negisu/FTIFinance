Public Class QueryHelper
    Public Shared Function selectStar(tableName As String) As DataTable
        Dim query As String = String.Empty
        query &= "SELECT  *  FROM " & tableName & " "
        Dim dt As DataTable = New DataTable
        Return fillWebSQL(query, New Dictionary(Of String, Object), tableName)
    End Function

    Public Shared Function insertModel(tableName As String, model As Object) As Boolean
        Dim columnName As String = String.Empty
        Dim values As String = String.Empty
        Dim parameters As New Dictionary(Of String, Object)
        Dim count As Integer = 0
        For Each fi As System.Reflection.FieldInfo In model.GetType().GetFields()
            Try
                If fi.GetValue(model).GetType() = GetType(Date) Then
                    If fi.GetValue(model).Equals(New Date) Then
                        Throw New NullReferenceException
                    Else
                        parameters.Add("@p" & count.ToString(), fi.GetValue(model))
                    End If
                Else
                    parameters.Add("@p" & count.ToString(), fi.GetValue(model))
                End If
                values &= "@p" & count.ToString() & ","
                columnName &= fi.Name & ","
                count += 1
            Catch ex As NullReferenceException

            End Try
        Next
        columnName = "(" & columnName.Substring(0, columnName.Length - 1) & ")"
        values = "(" & values.Substring(0, values.Length - 1) & ")"
        Dim query As String = "INSERT INTO " & tableName & " " & columnName & " VALUES " & values
        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            Return False
        End Try
        Return True
    End Function

    Public Shared Function getModel() As Object

        Return True
    End Function
    Private Shared Function insertOrUpdateModel(tableName As String, model As Object, identifiers As Dictionary(Of String, Object)) As Boolean


        Return True

    End Function

    Public Shared Function updateModel(tableName As String, model As Object, identifiers As Dictionary(Of String, Object)) As Boolean
        Dim values As String = String.Empty
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = "UPDATE " & tableName & " SET "
        Dim count As Integer = 0
        For Each fi As System.Reflection.FieldInfo In model.GetType().GetFields()
            Try
                If fi.GetValue(model).GetType() = GetType(Date) Then
                    If fi.GetValue(model).Equals(New Date) Then
                        Throw New NullReferenceException
                    Else
                        parameters.Add("@p" & count.ToString(), fi.GetValue(model))
                    End If
                Else
                    parameters.Add("@p" & count.ToString(), fi.GetValue(model))
                End If
                values &= fi.Name & " = @p" & count.ToString() & ","
                count += 1
            Catch ex As NullReferenceException

            End Try
        Next
        query &= values & "WHERE "

        For Each pair In identifiers
            parameters.Add("@p" & count.ToString(), pair.Value)
            query &= " " & pair.Key & " = @p" & count.ToString() & " "
        Next

        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            Return False
        End Try
        Return True

    End Function


End Class
