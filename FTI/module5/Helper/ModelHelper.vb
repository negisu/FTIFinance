Public Class ModelHelper
    Public Shared Function convertDataRowToModel(model As Object, datarow As DataRow) As Object

        For Each fi As System.Reflection.FieldInfo In model.GetType().GetFields()
            Try
                fi.SetValue(model, datarow.Item(fi.Name))
            Catch
                Try
                    fi.SetValue(model, Convert.ChangeType(datarow.Item(fi.Name), fi.GetValue(model).GetType))
                Catch

                End Try

            End Try
        Next
        Return model

    End Function
End Class