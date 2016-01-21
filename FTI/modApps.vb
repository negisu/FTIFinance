Imports System.IO
'Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports System.IO.Compression
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports Excel
'Imports ClosedXML.Excel
Imports System.Text.RegularExpressions
'Imports System.Collections.Generic
Imports System.Reflection
Imports Microsoft.Reporting.WinForms
Imports System.Text
Imports System.Drawing.Printing
Imports System.Drawing.Imaging
'Imports DocumentFormat.OpenXml

Module modApps
    Private Declare Auto Function LogonUser Lib "advapi32.dll" (ByVal un As String, ByVal domain As String, ByVal pw As String, ByVal LogonType As Integer, ByVal LogonProvider As Integer, ByRef Token As IntPtr) As Boolean

    Public Declare Auto Function CloseHandle Lib "kernel32.dll" (ByVal handle As IntPtr) As Boolean

    Public Function Zip(text As String) As String
        Dim buffer As Byte() = System.Text.Encoding.Unicode.GetBytes(text)
        Dim ms As New MemoryStream()
        Using zip__1 As New System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress, True)
            zip__1.Write(buffer, 0, buffer.Length)
        End Using

        ms.Position = 0
        Dim outStream As New MemoryStream()

        Dim compressed As Byte() = New Byte(CInt(ms.Length - 1)) {}
        ms.Read(compressed, 0, compressed.Length)

        Dim gzBuffer As Byte() = New Byte(compressed.Length + 3) {}
        System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length)
        System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4)
        Return Convert.ToBase64String(gzBuffer)
    End Function

    Public Function UnZip(compressedText As String) As String
        Dim gzBuffer As Byte() = Convert.FromBase64String(compressedText)
        Using ms As New MemoryStream()
            Dim msgLength As Integer = BitConverter.ToInt32(gzBuffer, 0)
            ms.Write(gzBuffer, 4, gzBuffer.Length - 4)

            Dim buffer As Byte() = New Byte(msgLength - 1) {}

            ms.Position = 0
            Using zip As New System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress)
                zip.Read(buffer, 0, buffer.Length)
            End Using

            Return System.Text.Encoding.Unicode.GetString(buffer, 0, buffer.Length)
        End Using
    End Function

    Public Sub openURL(ByVal URL As String)
        Dim tokenHandle As New IntPtr(0)
        Try
            If LogonUser(user_name, "AA", user_pass, 2, 0, tokenHandle) Then
                Dim newId As New WindowsIdentity(tokenHandle)
                Using impersonatedUser As WindowsImpersonationContext = newId.Impersonate()
                    'perform impersonated commands
                    'System.IO.File.WriteAllText("C:ttestimp.txt", "test")
                    Process.Start("C:\Program Files\Internet Explorer\iexplore.exe", URL)
                End Using
                CloseHandle(tokenHandle)
            Else
                'logon failed
                MessageBox.Show("logon failed")
            End If
        Catch ex As Exception
            'exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private BUFFER_SIZE As Integer = 64 * 1024 '64kB

    Public Function CompressByte(inputData As Byte()) As Byte()
        If inputData Is Nothing Then
            Throw New ArgumentNullException("inputData must be non-null")
        End If

        Using compressIntoMs = New MemoryStream()
            Using gzs = New BufferedStream(New GZipStream(compressIntoMs, CompressionMode.Compress), BUFFER_SIZE)
                gzs.Write(inputData, 0, inputData.Length)
            End Using
            Return compressIntoMs.ToArray()
        End Using
    End Function

    Public Function DecompressByte(inputData As Byte()) As Byte()
        If inputData Is Nothing Then
            Throw New ArgumentNullException("inputData must be non-null")
        End If

        Using compressedMs = New MemoryStream(inputData)
            Using decompressedMs = New MemoryStream()
                Using gzs = New BufferedStream(New GZipStream(compressedMs, CompressionMode.Decompress), BUFFER_SIZE)
                    gzs.CopyTo(decompressedMs)
                End Using
                Return decompressedMs.ToArray()
            End Using
        End Using
    End Function

    Friend Function fillWebSQL(ByVal query As String, ByVal parameters As Dictionary(Of String, Object), ByVal tableName As String) As DataTable
        'Dim parameters As New Dictionary(Of String, Object)
        'parameters.Add("@p0", "000")

        Try
            Dim stream As Stream = New MemoryStream(DecompressByte(client.Fill(query, parameters, tableName, user_session)))
            Dim formatter As IFormatter = New BinaryFormatter()
            stream.Seek(0, SeekOrigin.Begin)
            Dim o As Object = formatter.Deserialize(stream)
            'Dim ds As DataSet = CType(o, DataSet)
            Dim dt As DataTable = CType(o, DataTable)

            'Return ds.Tables("result").Copy
            Return dt.Copy
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try

    End Function

    Friend Function updateWebSQL(ByVal query As String, ByVal parameters As Dictionary(Of String, Object), ByVal dt As DataTable) As Integer
        Dim data As DataTable = dt.GetChanges.Copy
        Dim result As Integer = 0

        If data IsNot Nothing Then
            data.RemotingFormat = System.Data.SerializationFormat.Binary
            Dim inMemData As MemoryStream = New MemoryStream()
            Dim bf As IFormatter = New BinaryFormatter()
            bf.Serialize(inMemData, data)

            result = client.Update(query, parameters, CompressByte(inMemData.ToArray()), user_session)
        End If

        Return result
    End Function

    Friend Function executeWebSQL(ByVal query As String, ByVal parameters As Dictionary(Of String, Object)) As Integer
        Dim result As Integer = 0

        'check parameters // cannot send dbnull over http(s)
        'If parameters IsNot Nothing Then
        '    If parameters.Count > 0 Then
        '        ' Loop over entries.
        '        Dim pair As KeyValuePair(Of String, Object)
        '        For Each pair In parameters
        '            ' Display Key and Value.
        '            'Console.WriteLine("{0}, {1}", pair.Key, pair.Value)
        '            'da.SelectCommand.Parameters.AddWithValue(pair.Key, pair.Value)
        '            If pair.Value Is DBNull.Value Then
        '                parameters(pair.Key) = ""
        '            End If
        '        Next
        '    End If
        'End If

        Try
            result = client.ExecuteNonQuery(query, parameters, user_session)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        Return result
    End Function

    Friend Function getWebUsers(ByVal tableName As String, ByVal filter As String) As DataTable
        Try
            Dim stream As Stream = New MemoryStream(DecompressByte(client.GetUsers(user_name, user_pass, filter, tableName, user_session)))
            Dim formatter As IFormatter = New BinaryFormatter()
            stream.Seek(0, SeekOrigin.Begin)
            Dim o As Object = formatter.Deserialize(stream)
            'Dim ds As DataSet = CType(o, DataSet)
            Dim dt As DataTable = CType(o, DataTable)

            'Return ds.Tables("result").Copy
            Return dt.Copy
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try

    End Function

    Friend Function getSQLDate() As DateTime
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = "SELECT GETDATE() AS DT"

        Dim d As DateTime = Convert.ToDateTime(client.ExecuteScalar(query, parameters, user_session), ciTH)

        Return d
    End Function

    Friend Sub openFile(ByVal FILE_ID As Integer, ByVal FILE_NAME As String)
        ' Create a file and write the byte data to a file.
        Dim pFiles As New Dictionary(Of String, Object)
        pFiles.Add("@p0", FILE_ID)

        Dim qFiles As String = String.Empty
        qFiles &= "SELECT FILE_DATA FROM MB_MEMBER_FILES "
        qFiles &= "WHERE (ID = @p0) "
        'qFiles &= "ORDER BY ID "

        Dim FILE_DATA As Object = DBNull.Value

        Try
            FILE_DATA = client.ExecuteScalar(qFiles, pFiles, user_session)
        Catch ex As Exception
            'MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
        End Try

        'write to temp directory
        Dim bytBLOBData() As Byte = DirectCast(FILE_DATA, Byte())
        Dim stmBLOBData As New System.IO.MemoryStream(bytBLOBData)

        Try
            Dim oFileStream As System.IO.FileStream
            oFileStream = New System.IO.FileStream(Path.GetTempPath & "\" & FILE_NAME, System.IO.FileMode.Create)
            oFileStream.Write(bytBLOBData, 0, bytBLOBData.Length)
            oFileStream.Close()

            'open it
            Process.Start(Path.GetTempPath & "\" & FILE_NAME)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "openFile=" & FILE_NAME)
        End Try

    End Sub

    Friend Function getParameters(ByVal MODULE_ID As Integer, ByVal PARAMETER_NAME As String) As String
        Dim result As String = String.Empty

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", MODULE_ID)
        parameters.Add("@p1", PARAMETER_NAME)

        Dim query As String = String.Empty
        'query = String.Empty
        query = "SELECT OBJ_VALUE FROM MB_PARAMETERS WHERE OBJ_MODULE = @p0 AND OBJ_NAME = @p1"

        Try
            result = client.ExecuteScalar(query, parameters, user_session).ToString
        Catch ex As Exception
            'MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
        End Try

        Return result
    End Function

    Friend Function updateParameters(ByVal MODULE_ID As Integer, ByVal PARAMETER_NAME As String, ByVal PARAMETER_VALUE As String) As Integer
        'update MB_PARAMETERS
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", PARAMETER_VALUE)
        parameters.Add("@p", PARAMETER_NAME)

        Dim query As String = "UPDATE MB_PARAMETERS SET OBJ_VALUE = @p0 WHERE OBJ_NAME = @p"

        Dim result As Integer = 0
        Try
            result = executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try

        Return result
    End Function

    Friend Function ThumbNailImage(ByVal oImg As Image) As Byte()
        Dim gr_dest As Graphics = Graphics.FromImage(oImg)
        Dim oStream As System.IO.MemoryStream = New System.IO.MemoryStream
        oImg.Save(oStream, Imaging.ImageFormat.Png)

        gr_dest.Dispose()
        Return oStream.ToArray
    End Function

    Friend Function GetImageThumbnailByte(ByVal FILENAME As String, ByVal WIDTH_VALUE As Integer, ByVal HEIGHT_VALUE As Integer) As Byte()
        Dim Imge As Image = System.Drawing.Image.FromFile(FILENAME)
        Dim iW As Integer = Imge.Width
        Dim iH As Integer = Imge.Height
        Dim lStop As Boolean = False
        Dim Minor As Integer
        Dim iMul As Double = 0

        Do Until lStop = True
            If iW > WIDTH_VALUE Then
                Minor = iW - WIDTH_VALUE
                iMul = Minor * 100 / iW
                iW = WIDTH_VALUE
                iH = Convert.ToInt32(System.Math.Round(iH - (iH * iMul / 100)))
            End If

            If iH > HEIGHT_VALUE Then
                Minor = iH - HEIGHT_VALUE
                iMul = Minor * 100 / iH
                iW = Convert.ToInt32(System.Math.Round(iW - (iW * iMul / 100)))
                iH = HEIGHT_VALUE
            End If

            'Console.Write("W " & iW & vbCrLf)
            'Console.Write("H " & iH & vbCrLf)

            If iW <= WIDTH_VALUE And iH <= HEIGHT_VALUE Then
                lStop = True
                Exit Do
            End If
        Loop

        'Console.Write("W " & iW & vbCrLf)
        'Console.Write("H " & iH & vbCrLf)

        'Dim bytBLOBData() As Byte = ConvertFromPictureBoxToArray(PictureBox1)
        Dim bytBLOBData() As Byte = ThumbNailImage(System.Drawing.Image.FromFile(FILENAME).GetThumbnailImage(iW, iH, Nothing, Nothing))

        'Dim result As Image
        Return bytBLOBData
    End Function

    Friend Function GetImageThumbnail(ByVal FILENAME As String, ByVal WIDTH_VALUE As Integer, ByVal HEIGHT_VALUE As Integer) As Image
        Dim Imge As Image = System.Drawing.Image.FromFile(FILENAME)
        Dim iW As Integer = Imge.Width
        Dim iH As Integer = Imge.Height
        Dim lStop As Boolean = False
        Dim Minor As Integer
        Dim iMul As Double = 0

        Do Until lStop = True
            If iW > WIDTH_VALUE Then
                Minor = iW - WIDTH_VALUE
                iMul = Minor * 100 / iW
                iW = WIDTH_VALUE
                iH = Convert.ToInt32(System.Math.Round(iH - (iH * iMul / 100)))
            End If

            If iH > HEIGHT_VALUE Then
                Minor = iH - HEIGHT_VALUE
                iMul = Minor * 100 / iH
                iW = Convert.ToInt32(System.Math.Round(iW - (iW * iMul / 100)))
                iH = HEIGHT_VALUE
            End If

            'Console.Write("W " & iW & vbCrLf)
            'Console.Write("H " & iH & vbCrLf)

            If iW <= WIDTH_VALUE And iH <= HEIGHT_VALUE Then
                lStop = True
                Exit Do
            End If
        Loop

        'Console.Write("W " & iW & vbCrLf)
        'Console.Write("H " & iH & vbCrLf)

        'Dim bytBLOBData() As Byte = ConvertFromPictureBoxToArray(PictureBox1)
        Dim bytBLOBData() As Byte = ThumbNailImage(System.Drawing.Image.FromFile(FILENAME).GetThumbnailImage(iW, iH, Nothing, Nothing))
        Dim stmBLOBData As New System.IO.MemoryStream(bytBLOBData)

        'Dim result As Image
        Return Image.FromStream(stmBLOBData)
    End Function

    Friend Function ImageToByte(ByVal IMAGE_IN As Image) As Byte()
        Dim imgCon As New ImageConverter()
        Return DirectCast(imgCon.ConvertTo(IMAGE_IN, GetType(Byte())), Byte())
    End Function

    Friend Sub saveLOGS(ByVal REGIST_CODE As String, ByVal TABLE_NAME As String, ByVal COLUMN_NAME As String, ByVal MODIFY_TYPE As String, ByVal OLD_DATA As Object, ByVal NEW_DATA As Object, ByVal USER_BY As String)
        Dim parameters As New Dictionary(Of String, Object)
        Dim query As String = String.Empty

        query = String.Empty
        query = "INSERT INTO MB_LOGS (REGIST_CODE, TABLE_NAME, COLUMN_NAME, MODIFY_TYPE, MODIFY_DATE, OLD_DATA, NEW_DATA, USER_BY) VALUES (@p0,@p1,@p2,@p3,GETDATE(),@p4,@p5,@p6)"

        parameters.Add("@p0", REGIST_CODE)
        parameters.Add("@p1", TABLE_NAME)
        parameters.Add("@p2", COLUMN_NAME)
        parameters.Add("@p3", MODIFY_TYPE) 'update / add / delete
        parameters.Add("@p4", OLD_DATA)
        parameters.Add("@p5", NEW_DATA)
        parameters.Add("@p6", USER_BY)

        Try
            executeWebSQL(query, parameters)
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
        End Try
    End Sub

    Friend Function IsExistStatus(ByVal STATUS_LIST As String, ByVal STATUS_COMPARE As String) As Boolean
        Dim result As Boolean = False

        Dim status() As String = STATUS_LIST.Split(","c)
        For i As Integer = 0 To status.Length - 1
            If STATUS_COMPARE.ToLower = status(i).ToLower Then
                result = True
                Exit For
            End If
        Next

        Return result
    End Function

    Friend Function getCurrentStatus(ByVal REGIST_CODE As String) As String
        Dim result As String = String.Empty

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", REGIST_CODE)

        Dim query As String = String.Empty
        'query = String.EmptyS
        query = "SELECT MEMBER_STATUS_CODE FROM MB_MEMBER WHERE REGIST_CODE = @p0"

        Try
            result = client.ExecuteScalar(query, parameters, user_session).ToString
        Catch ex As Exception
            'MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
        End Try

        Return result
    End Function

    'Friend Function getExcelSheets(ByVal EXCEL_PATH As String) As DataTable
    '    'Dim result As String = String.Empty

    '    Dim conn As String = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Mode=Read;Extended Properties=Excel 12.0;", EXCEL_PATH)
    '    Dim conExcel As New OleDb.OleDbConnection(conn)

    '    conExcel.Open()
    '    Dim dt As DataTable = conExcel.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})
    '    conExcel.Close()
    '    conExcel.Dispose()

    '    'foreach (DataRow row in dt.Rows)
    '    '        {
    '    '            excelSheets[i] = row["TABLE_NAME"].ToString();
    '    '            i++;
    '    '        }

    '    Return dt
    'End Function

    'Friend Function getExcelData(ByVal EXCEL_PATH As String, ByVal SHEET_NAME As String) As DataTable
    '    Dim conn As String = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Mode=Read;Extended Properties=Excel 12.0;", EXCEL_PATH)
    '    Dim conExcel As New OleDb.OleDbConnection(conn)
    '    'Dim cmdExcel As New OleDb.OleDbCommand("SELECT * FROM [" & SHEET_NAME & "]", conExcel)
    '    Dim daExcel As New OleDb.OleDbDataAdapter("SELECT * FROM [" & SHEET_NAME & "]", conExcel)

    '    Dim dt As New DataTable("SHEET_NAME")
    '    daExcel.Fill(dt)

    '    Return dt
    'End Function

    Friend Function getUserDIV(ByVal USER_ID As String) As String
        Dim result As String = String.Empty

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", USER_ID)

        Dim query As String = "SELECT USER_DIV FROM USERS WHERE USER_ID = @p0"

        Try
            result = client.ExecuteScalar(query, parameters, user_session).ToString
        Catch ex As Exception
            'MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
        End Try

        Return result
    End Function

    Friend Function getExcelDataV2(ByVal EXCEL_PATH As String) As DataSet
        Dim stream As FileStream = File.Open(EXCEL_PATH, FileMode.Open, FileAccess.Read)

        '1. Reading from a binary Excel file ('97-2003 format; *.xls)
        'Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateBinaryReader(stream)
        '...
        '2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
        Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
        '...
        '3. DataSet - The result of each spreadsheet will be created in the result.Tables
        'Dim result As DataSet = excelReader.AsDataSet()
        '...
        '4. DataSet - Create column names from first row
        excelReader.IsFirstRowAsColumnNames = True
        Dim result As DataSet = excelReader.AsDataSet()

        '5. Data Reader methods
        While excelReader.Read()
            'excelReader.GetInt32(0);
        End While

        '6. Free resources (IExcelDataReader is IDisposable)
        excelReader.Close()

        Return result
    End Function

    Friend Sub ExportDataSet(ByVal ds As DataSet, ByVal destination As String, ByRef err As String)
        Using workbook As DocumentFormat.OpenXml.Packaging.SpreadsheetDocument = DocumentFormat.OpenXml.Packaging.SpreadsheetDocument.Create(destination, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook)
            Dim workbookPart As DocumentFormat.OpenXml.Packaging.WorkbookPart = workbook.AddWorkbookPart()

            workbook.WorkbookPart.Workbook = New DocumentFormat.OpenXml.Spreadsheet.Workbook()

            workbook.WorkbookPart.Workbook.Sheets = New DocumentFormat.OpenXml.Spreadsheet.Sheets()

            For Each table As System.Data.DataTable In ds.Tables

                Dim sheetPart As DocumentFormat.OpenXml.Packaging.WorksheetPart = workbook.WorkbookPart.AddNewPart(Of DocumentFormat.OpenXml.Packaging.WorksheetPart)()
                Dim sheetData As DocumentFormat.OpenXml.Spreadsheet.SheetData = New DocumentFormat.OpenXml.Spreadsheet.SheetData()
                sheetPart.Worksheet = New DocumentFormat.OpenXml.Spreadsheet.Worksheet(sheetData)

                Dim sheets As DocumentFormat.OpenXml.Spreadsheet.Sheets = workbook.WorkbookPart.Workbook.GetFirstChild(Of DocumentFormat.OpenXml.Spreadsheet.Sheets)()
                Dim relationshipId As String = workbook.WorkbookPart.GetIdOfPart(sheetPart)

                Dim sheetId As UInteger = 1
                If sheets.Elements(Of DocumentFormat.OpenXml.Spreadsheet.Sheet)().Count() > 0 Then
                    sheetId = CUInt(sheets.Elements(Of DocumentFormat.OpenXml.Spreadsheet.Sheet)().[Select](Function(s) s.SheetId.Value).Max() + 1)
                End If

                Dim sheet As New DocumentFormat.OpenXml.Spreadsheet.Sheet() With {.Id = relationshipId, .SheetId = sheetId, .Name = table.TableName}
                sheets.Append(sheet)

                Dim headerRow As New DocumentFormat.OpenXml.Spreadsheet.Row()

                Dim columns As List(Of [String]) = New List(Of String)()
                For Each column As System.Data.DataColumn In table.Columns
                    columns.Add(column.ColumnName)

                    Dim cell As New DocumentFormat.OpenXml.Spreadsheet.Cell()
                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String
                    cell.CellValue = New DocumentFormat.OpenXml.Spreadsheet.CellValue(column.ColumnName)
                    headerRow.AppendChild(cell)
                Next

                sheetData.AppendChild(headerRow)

                For Each dsrow As System.Data.DataRow In table.Rows
                    Dim newRow As New DocumentFormat.OpenXml.Spreadsheet.Row()
                    For Each col As [String] In columns
                        Dim cell As New DocumentFormat.OpenXml.Spreadsheet.Cell()
                        cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String
                        cell.CellValue = New DocumentFormat.OpenXml.Spreadsheet.CellValue(Regex.Replace(dsrow(col).ToString(), "\p{C}+", ""))
                        '
                        newRow.AppendChild(cell)
                    Next

                    sheetData.AppendChild(newRow)

                Next
            Next
        End Using
    End Sub

    'Friend Sub saveAsXLSX(ds As DataSet, path As String, ByRef err As String, Optional ByVal TABLE_NAME As String = "")
    '    Dim wb As New XLWorkbook
    '    Dim isFound As Boolean = False
    '    For i As Integer = 0 To ds.Tables.Count - 1
    '        isFound = False
    '        If TABLE_NAME.Length = 0 Then
    '            'do it
    '            isFound = True
    '        Else
    '            If TABLE_NAME.ToLower = ds.Tables(i).TableName.ToLower Then isFound = True
    '        End If

    '        If isFound = True Then
    '            Dim ws As IXLWorksheet = wb.Worksheets.Add(ds.Tables(i).TableName)

    '            'header
    '            For c As Integer = 0 To ds.Tables(i).Columns.Count - 1
    '                ws.Cell(1, c + 1).Value = ds.Tables(i).Columns(c).ColumnName
    '            Next

    '            'content
    '            For r As Integer = 0 To ds.Tables(i).Rows.Count - 1
    '                For c As Integer = 0 To ds.Tables(i).Columns.Count - 1
    '                    err = String.Format("ro={0} co={1}: {2} ", r, ds.Tables(i).Columns(c).ColumnName, ds.Tables(i).Rows(r).Item(c).ToString & "<")
    '                    ws.Cell(r + 1 + 1, c + 1).Value = Regex.Replace(ds.Tables(i).Rows(r).Item(c).ToString.Trim, "\p{C}+", "")
    '                    'Regex.Replace(ds.Tables(i).Rows(r).Item(c).ToString, "/[^\P{C}\n]+/u", "")
    '                    'teststring = Regex.Replace(teststring, "\p{C}+", "")
    '                    'ws.Cell(r + 1 + 1, c + 1).Value = ds.Tables(i).Rows(r).Item(c).ToString
    '                    'Regex.Replace(s, @"[^\x20-\x7F]", "")
    '                Next
    '            Next
    '        End If
    '    Next

    '    wb.SaveAs(path)

    'End Sub

    'Friend Sub getPermissions(ByVal FORM_NAME As String, ByRef ERR As Integer, Optional ByRef FORM_CONTROLS As Control.ControlCollection = Nothing, Optional ByRef FORM_MENU As MenuStrip = Nothing, Optional ByRef FORM_TAB As TabControl = Nothing)
    Friend Sub getPermissions(ByVal FORM_NAME As String, ByRef ERR As Integer, ByRef FORM_CONTROLS As Control.ControlCollection)
        'Dim err As Integer = 0
        ERR = 1
        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@p0", FORM_NAME)

        Dim query As String = String.Empty
        query &= "SELECT * "
        query &= "FROM            MB_PERMISSIONS "
        query &= "WHERE        (FORM_NAME = @p0) "
        query &= "ORDER BY OBJECT_NAME"

        Dim dt As DataTable = New DataTable

        dt = fillWebSQL(query, parameters, "MB_PERMISSIONS")

        ERR = 2

        'MessageBox.Show(FORM_NAME & dt.Rows.Count)

        Dim USER_GROUPS As String = String.Empty

        parameters.Clear()
        parameters.Add("@p0", user_name.ToLower)

        query = "SELECT USER_GROUP FROM USERS WHERE USER_ID = @p0"

        Try
            USER_GROUPS = client.ExecuteScalar(query, parameters, user_session).ToString
        Catch ex As Exception
            MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
        End Try

        Dim sGROUPS() As String = USER_GROUPS.Split(","c)
        Dim dGROUPS As New Dictionary(Of Integer, Integer)
        For Each s As String In sGROUPS
            dGROUPS.Add(CInt(s), CInt(s))
        Next
        Dim isEnable As Boolean = False

        ERR = 10

        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(i).Item("OBJECT_NAME").ToString.Length > 0 Then
                ERR = 11

                isEnable = False
                Dim eGROUPS() As String = dt.Rows(i).Item("ENABLED_GROUPS").ToString.Split(","c)
                For Each e As String In eGROUPS
                    If dGROUPS.ContainsKey(CInt(e)) = True Then
                        isEnable = True
                        Exit For
                    End If
                Next

                ERR = 12

                Dim b As Boolean = getControlsByName(dt.Rows(i).Item("OBJECT_NAME").ToString, FORM_CONTROLS, isEnable)
            End If
        Next



        'If FORM_CONTROLS IsNot Nothing Then
        '    'For Each cont As Control In FORM_CONTROLS
        '    'MessageBox.Show(cont.Name)
        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        If dt.Rows(i).Item("OBJECT_NAME").ToString.Length > 0 Then
        '            ERR = 11
        '            Dim cont As Control = FORM_CONTROLS(dt.Rows(i).Item("OBJECT_NAME").ToString)
        '            If cont IsNot Nothing Then
        '                'found it as normal controls
        '                isEnable = False
        '                Dim eGROUPS() As String = dt.Rows(i).Item("ENABLED_GROUPS").ToString.Split(","c)
        '                For Each e As String In eGROUPS
        '                    If dGROUPS.ContainsKey(CInt(e)) = True Then
        '                        isEnable = True
        '                        Exit For
        '                    End If
        '                Next

        '                cont.Enabled = isEnable

        '                'mark as do not do again
        '                dt.Rows(i).Item("OBJECT_NAME") = DBNull.Value
        '            End If
        '        End If

        '        'MessageBox.Show(cont.Name.ToLower & "=" & dt.Rows(i).Item("OBJECT_NAME").ToString.ToLower)
        '        'If cont.Name.ToLower = dt.Rows(i).Item("OBJECT_NAME").ToString.ToLower And dt.Rows(i).Item("OBJECT_NAME").ToString.Length > 0 Then
        '        '    isFound = False
        '        '    Dim eGROUPS() As String = dt.Rows(i).Item("ENABLED_GROUPS").ToString.Split(","c)
        '        '    For Each e As String In eGROUPS
        '        '        If dGROUPS.ContainsKey(CInt(e)) = True Then
        '        '            isFound = True
        '        '            'MessageBox.Show("FOUND IT")
        '        '            Exit For
        '        '        End If
        '        '    Next

        '        '    cont.Enabled = isFound

        '        '    'mark as do not do again
        '        '    dt.Rows(i).Item("OBJECT_NAME") = DBNull.Value
        '        'End If
        '    Next
        '    'Next
        'End If

        'ERR = 20

        'If FORM_MENU IsNot Nothing Then

        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        If dt.Rows(i).Item("OBJECT_NAME").ToString.Length > 0 Then
        '            Dim t As ToolStripMenuItem = FindToolStripMenuItem(FORM_MENU.Items, dt.Rows(i).Item("OBJECT_NAME").ToString)

        '            If t IsNot Nothing Then
        '                'MessageBox.Show(t.Name)
        '                isEnable = False
        '                Dim eGROUPS() As String = dt.Rows(i).Item("ENABLED_GROUPS").ToString.Split(","c)
        '                For Each e As String In eGROUPS
        '                    If dGROUPS.ContainsKey(CInt(e)) = True Then
        '                        isEnable = True
        '                        'MessageBox.Show("FOUND IT")
        '                        Exit For
        '                    End If
        '                Next

        '                t.Enabled = isEnable

        '                'mark as do not do again
        '                dt.Rows(i).Item("OBJECT_NAME") = DBNull.Value
        '            End If
        '        End If

        '    Next
        'End If

        'ERR = 30

        'If FORM_TAB IsNot Nothing Then

        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        If dt.Rows(i).Item("OBJECT_NAME").ToString.Length > 0 Then
        '            Dim t As Windows.Forms.TabPage = FindTabPage(FORM_TAB.TabPages, dt.Rows(i).Item("OBJECT_NAME").ToString)

        '            If t IsNot Nothing Then
        '                'MessageBox.Show(t.Name)
        '                isEnable = False
        '                Dim eGROUPS() As String = dt.Rows(i).Item("ENABLED_GROUPS").ToString.Split(","c)
        '                For Each e As String In eGROUPS
        '                    If dGROUPS.ContainsKey(CInt(e)) = True Then
        '                        isEnable = True
        '                        'MessageBox.Show("FOUND IT")
        '                        Exit For
        '                    End If
        '                Next

        '                t.Enabled = isEnable

        '                'mark as do not do again
        '                dt.Rows(i).Item("OBJECT_NAME") = DBNull.Value
        '            End If
        '        End If

        '    Next
        'End If

    End Sub

    Private Function FindToolStripMenuItem(ByRef menus As ToolStripItemCollection, ByVal name As String) As ToolStripMenuItem
        Dim found As Boolean = False
        Dim t, temp As ToolStripMenuItem

        t = CType(menus(name), ToolStripMenuItem)

        If t Is Nothing Then
            Dim i As Integer = 0

            While Not found And i < menus.Count
                If menus(i).GetType Is GetType(ToolStripMenuItem) Then
                    temp = CType(menus(i), ToolStripMenuItem)
                    t = FindToolStripMenuItem(temp.DropDownItems, name)
                    found = (t IsNot Nothing)
                End If

                i += 1
            End While
        End If

        Return t
    End Function

    Private Function FindTabPage(ByRef Tabs As TabControl.TabPageCollection, ByVal name As String) As Windows.Forms.TabPage
        Dim t As Windows.Forms.TabPage = Nothing

        For Each ta As Windows.Forms.TabPage In Tabs
            If ta.Name = name Then
                t = ta
                Exit For
            End If
        Next

        Return t
    End Function

    '=======================================================================================================
    Public Function getControlsByFormName(ByVal FORM_NAME As String) As Dictionary(Of String, String)
        'Dim arr As New ArrayList
        'Dim a As New ArrayList
        Dim dict As New Dictionary(Of String, String)
        Dim d As New Dictionary(Of String, String)
        ' Creates and returns an instance of any object in the assembly by its type name.
        Dim f As Form = Nothing

        Try
            If FORM_NAME.LastIndexOf(".") = -1 Then
                'Appends the root namespace if not specified.
                FORM_NAME = [Assembly].GetEntryAssembly.GetName.Name & "." & FORM_NAME
            End If

            'DirectCast(CreateObjectInstance(formName), Form)
            f = DirectCast([Assembly].GetEntryAssembly.CreateInstance(FORM_NAME), Form)
        Catch ex As Exception
            f = Nothing
        End Try

        If f IsNot Nothing Then
            For Each ctrl As Control In f.Controls
                dict.Add(ctrl.Name, ctrl.Text & " (" & ctrl.Name & ")")

                'arr.Add(ctrl.Name)
                d = New Dictionary(Of String, String)

                If (ctrl.GetType Is GetType(MenuStrip)) Then
                    'MessageBox.Show(ctrl.Name)
                    d = getToolStripItems(CType(ctrl, MenuStrip).Items)
                    'MessageBox.Show(a.Count.ToString)
                End If
                If (ctrl.GetType Is GetType(TabControl)) Then
                    'MessageBox.Show(ctrl.Name)
                    d = getTabControlItems(CType(ctrl, TabControl).TabPages)
                    'MessageBox.Show(a.Count.ToString)
                End If

                If d.Count > 0 Then
                    For Each kvp As KeyValuePair(Of String, String) In d
                        dict.Add(kvp.Key, kvp.Value)
                    Next
                End If
            Next
        End If

        'arr.Sort()

        Return dict
    End Function

    Private Function getToolStripItems(ByVal dropDownItems As ToolStripItemCollection) As Dictionary(Of String, String)
        'Dim arr As New ArrayList
        'Dim a As New ArrayList
        Dim dict As New Dictionary(Of String, String)
        Dim d As New Dictionary(Of String, String)
        Dim temp As ToolStripMenuItem

        For i As Integer = 0 To dropDownItems.Count - 1
            If dropDownItems(i).GetType Is GetType(ToolStripMenuItem) Then
                temp = CType(dropDownItems(i), ToolStripMenuItem)

                d = New Dictionary(Of String, String)
                'arr.Add(temp.Name)
                dict.Add(temp.Name, temp.Text & " (" & temp.Name & ")")
                'MessageBox.Show(temp.Name)
                If temp.HasDropDown = True Then
                    d = getToolStripItems(temp.DropDownItems)
                End If

                If d.Count > 0 Then
                    For Each kvp As KeyValuePair(Of String, String) In d
                        dict.Add(kvp.Key, kvp.Value)
                    Next
                End If
            End If
        Next

        'arr.Sort()

        Return dict
    End Function

    Private Function getTabControlItems(ByVal TabPages As TabControl.TabPageCollection) As Dictionary(Of String, String)
        'Dim arr As New ArrayList
        'Dim a As New ArrayList
        'Dim temp As TabPage

        Dim dict As New Dictionary(Of String, String)
        'Dim d As New Dictionary(Of String, String)

        For Each page As TabPage In TabPages
            'arr.Add(page.Name)
            'dict.Add(page.Name)
            For Each ctl As Control In page.Controls
                dict.Add(ctl.Name, ctl.Text & " (" & ctl.Name & ")")
                'arr.Add(ctl.Name)
                'If TypeOf ctl Is RichTextBox Then
                '    MessageBox.Show(ctl.Text)
                'End If
            Next
        Next

        'arr.Sort()

        Return dict
    End Function

    '=======================================================================================================
    Public Function getControlsByName(ByVal CONTROL_NAME As String, ByRef CONTROLS As Control.ControlCollection, ByVal IS_ENABLED As Boolean) As Boolean
        Dim result As Boolean = False

        For Each ctrl As Control In CONTROLS
            If ctrl.Name.ToLower = CONTROL_NAME.ToLower Then
                ctrl.Enabled = IS_ENABLED
                result = True
                Exit For
            Else
                If (ctrl.GetType Is GetType(MenuStrip)) Then
                    Dim temp As ToolStripMenuItem = Nothing
                    temp = FindToolStripMenuItem(CType(ctrl, MenuStrip).Items, CONTROL_NAME)

                    If temp IsNot Nothing Then
                        temp.Enabled = IS_ENABLED
                        result = True
                        Exit For
                    End If
                End If
                If (ctrl.GetType Is GetType(TabControl)) Then
                    If getTabControlItemsByName(CType(ctrl, TabControl).TabPages, CONTROL_NAME, IS_ENABLED) = True Then
                        result = True
                        Exit For
                    End If
                End If
            End If
        Next

        Return result
    End Function

    Private Function getTabControlItemsByName(ByRef TabPages As TabControl.TabPageCollection, ByVal CONTROL_NAME As String, ByVal IS_ENABLED As Boolean) As Boolean
        Dim result As Boolean = False
        For Each page As TabPage In TabPages
            For Each ctl As Control In page.Controls
                ctl.Enabled = IS_ENABLED
                result = True
                Exit For
            Next
        Next

        Return result
    End Function
    '=======================================================================================================

    Friend Function ValidatePassword(ByVal pwd As String, Optional ByVal minLength As Integer = 8, Optional ByVal numUpper As Integer = 2, Optional ByVal numLower As Integer = 2, Optional ByVal numNumbers As Integer = 2, Optional ByVal numSpecial As Integer = 2) As Boolean

        ' Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters.
        Dim upper As New System.Text.RegularExpressions.Regex("[A-Z]")
        Dim lower As New System.Text.RegularExpressions.Regex("[a-z]")
        Dim number As New System.Text.RegularExpressions.Regex("[0-9]")
        ' Special is "none of the above".
        Dim special As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]")

        ' Check the length.
        If Len(pwd) < minLength Then Return False
        ' Check for minimum number of occurrences.
        If upper.Matches(pwd).Count < numUpper Then Return False
        If lower.Matches(pwd).Count < numLower Then Return False
        If number.Matches(pwd).Count < numNumbers Then Return False
        If special.Matches(pwd).Count < numSpecial Then Return False

        ' Passed all checks.
        Return True
    End Function

    '=======================================================================================================
    Private m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    Private m_renderedReport As Byte()()

    ' Export the given report as an EMF (Enhanced Metafile) file.
    Friend Sub RDLCExport(ByVal report As LocalReport, ByVal deviceInfo As String)
        'Dim deviceInfo As String = "<DeviceInfo>" & _
        '    "<OutputFormat>EMF</OutputFormat>" & _
        '    "<PageWidth>8.5in</PageWidth>" & _
        '    "<PageHeight>11in</PageHeight>" & _
        '    "<MarginTop>0.25in</MarginTop>" & _
        '    "<MarginLeft>0.25in</MarginLeft>" & _
        '    "<MarginRight>0.25in</MarginRight>" & _
        '    "<MarginBottom>0.25in</MarginBottom>" & _
        '    "</DeviceInfo>"
        Dim warnings As Warning() = Nothing
        m_streams = New List(Of Stream)()
        report.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
        For Each stream As Stream In m_streams
            stream.Position = 0
        Next
    End Sub

    ' Routine to provide to the report renderer, in order to
    ' save an image for each page of the report.
    Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
        Dim stream As Stream = New MemoryStream()
        m_streams.Add(stream)
        Return stream
    End Function

    ' Handler for PrintPageEvents
    Private Sub PrintPage(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        Dim pageImage As New Metafile(m_streams(m_currentPageIndex))

        ' Adjust rectangular area with printer margins.
        Dim adjustedRect As New Rectangle(ev.PageBounds.Left - CInt(ev.PageSettings.HardMarginX), _
                                          ev.PageBounds.Top - CInt(ev.PageSettings.HardMarginY), _
                                          ev.PageBounds.Width, _
                                          ev.PageBounds.Height)

        ' Draw a white background for the report
        ev.Graphics.FillRectangle(Brushes.White, adjustedRect)

        ' Draw the report content
        ev.Graphics.DrawImage(pageImage, adjustedRect)

        ' Prepare for the next page. Make sure we haven't hit the end.
        m_currentPageIndex += 1
        ev.HasMorePages = (m_currentPageIndex < m_streams.Count)
    End Sub

    Friend Sub RDLCPrint(ByVal PrinterName As String)
        If m_streams Is Nothing OrElse m_streams.Count = 0 Then
            Throw New Exception("Error: no stream to print.")
        End If
        Dim printDoc As New PrintDocument()
        If Not printDoc.PrinterSettings.IsValid Then
            Throw New Exception("Error: cannot find the default printer.")
        Else
            AddHandler printDoc.PrintPage, AddressOf PrintPage
            m_currentPageIndex = 0
            printDoc.PrinterSettings.PrinterName = PrinterName
            printDoc.Print()
        End If

        'cleaar steam
        If m_streams IsNot Nothing Then
            For Each stream As Stream In m_streams
                stream.Close()
            Next
            m_streams = Nothing
        End If
    End Sub

    ' Create a local report for Report.rdlc, load the data,
    ' export the report to an .emf file, and print it.
    'Private Sub Run()
    '    Dim report As New LocalReport()
    '    report.ReportPath = "..\..\Report.rdlc"
    '    report.DataSources.Add(New ReportDataSource("Sales", LoadSalesData()))
    '    Export(report)
    '    Print()
    'End Sub

    '=======================================================================================================

    Friend Function ImageToBase64(ByVal mImage As Image, ByVal format As System.Drawing.Imaging.ImageFormat) As String
        Using ms As New MemoryStream()
            ' Convert Image to byte[]
            mImage.Save(ms, format)
            Dim imageBytes As Byte() = ms.ToArray()
            ' Convert byte[] to Base64 String
            Dim base64String As String = Convert.ToBase64String(imageBytes)
            Return base64String
        End Using
    End Function

    '=======================================================================================================
    Friend Sub RDLCExport(ByVal report As ServerReport)
        'Dim deviceInfo As String = "<DeviceInfo>" & _
        '    "<OutputFormat>EMF</OutputFormat>" & _
        '    "<PageWidth>8.5in</PageWidth>" & _
        '    "<PageHeight>11in</PageHeight>" & _
        '    "<MarginTop>0.25in</MarginTop>" & _
        '    "<MarginLeft>0.25in</MarginLeft>" & _
        '    "<MarginRight>0.25in</MarginRight>" & _
        '    "<MarginBottom>0.25in</MarginBottom>" & _
        '    "</DeviceInfo>"
        Dim deviceInfo As String = String.Format("<DeviceInfo><OutputFormat>{0}</OutputFormat></DeviceInfo>", "emf")
        Dim firstPage As Byte() = Nothing
        Dim showHideToggle As String = Nothing
        Dim encoding As String = Nothing
        Dim mimeType As String = Nothing
        Dim extension As String = Nothing
        'Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing
        Dim warnings As Warning() = Nothing
        'Dim reportHistoryParameters As ParameterValue() = Nothing
        Dim streamIDs As String() = Nothing
        'Dim reportHistoryParameters As ParameterValue() = Nothing
        Dim format As String = "IMAGE"
        'Dim pages As Byte() = Nothing
        'Dim pages As New Dictionary(Of Integer, Byte())
        'reportParameters.Add("DATE_FROM", "TEST IT!!!")

        'Dim execInfo As New ExecutionInfo()
        'Dim execHeader As New ExecutionHeader()
        'Dim warnings As Warning() = Nothing

        'm_streams = New List(Of Stream)()
        'report.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
        'result = rs.Render(Format, devInfo, extension, Encoding, mimeType, warnings, streamIDs)
        'firstPage = report.Render(reportPath, format, Nothing, deviceInfo, Nothing, Nothing, Nothing, encoding, mimeType, warnings, streamIDs)
        firstPage = report.Render(format, deviceInfo, mimeType, encoding, Nothing, streamIDs, warnings)

        ' The total number of pages of the report is 1 + the streamIDs         
        Dim m_numberOfPages As Integer = streamIDs.Length + 1
        'pages = New Byte(m_numberOfPages - 1) {}
        'pages = New IList(Of Byte())

        ' The first page was already rendered
        'pages(0) = firstPage
        'pages.Add(0, firstPage)

        m_streams = New List(Of Stream)()

        'Dim stream As Stream = New MemoryStream(firstPage)
        m_streams.Add(New MemoryStream(firstPage))

        'MessageBox.Show("m_numberOfPages=" & m_numberOfPages)

        For pageIndex As Integer = 1 To m_numberOfPages - 1
            ' Build device info based on start page
            deviceInfo = String.Format("<DeviceInfo><OutputFormat>{0}</OutputFormat><StartPage>{1}</StartPage></DeviceInfo>", "emf", pageIndex + 1)
            'pages(pageIndex) = rs.Render(reportPath, format, Nothing, deviceInfo, Nothing, Nothing, Nothing, encoding, mimeType, reportHistoryParameters, warnings, streamIDs)
            'pages(pageIndex) = report.Render(reportPath, format, Nothing, deviceInfo, Nothing, Nothing, Nothing, encoding, mimeType, reportHistoryParameters, warnings, streamIDs)
            Dim page As Byte() = report.Render(format, deviceInfo, mimeType, encoding, Nothing, streamIDs, warnings)
            m_streams.Add(New MemoryStream(page))
        Next

        'report.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
        For Each stream As Stream In m_streams
            stream.Position = 0
        Next
    End Sub

    '=======================================================================================================
End Module
