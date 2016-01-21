Imports System.Text
Imports System.Web.Services.Protocols
'Imports FTI.ReportExecution2005
Imports System.IO
Imports Microsoft.Reporting.WinForms
Imports System.Security.Principal
Imports System.Net

Public Class frmMainReports

    'Friend URL As String
    Friend reportParameters As Dictionary(Of String, String)
    Friend reportPath As String
    'Dim SSRS As String
    Friend ReportServerUrl As String

    Private Sub frmReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'SSRS = getParameters(0, "")
        'ReportViewer1.ServerReport.ReportServerCredentials = New MyReportServerCredentials()

        'Set the processing mode for the ReportViewer to Remote
        ReportViewer1.ProcessingMode = ProcessingMode.Remote

        Dim serverReport As ServerReport
        serverReport = ReportViewer1.ServerReport

        'Get a reference to the default credentials
        Dim credentials As System.Net.ICredentials
        'credentials = New System.Net.NetworkCredential("demo", "P@$$w0rd", "FTI") 'System.Net.CredentialCache.DefaultCredentials
        credentials = New System.Net.NetworkCredential(getParameters(0, "ReportServerCredentialsUser"), getParameters(0, "ReportServerCredentialsPassword"), getParameters(0, "ReportServerCredentialsDomain")) 'System.Net.CredentialCache.DefaultCredentials

        'Get a reference to the report server credentials
        Dim rsCredentials As ReportServerCredentials
        rsCredentials = serverReport.ReportServerCredentials

        'Set the credentials for the server report
        rsCredentials.NetworkCredentials = credentials

        If ReportServerUrl IsNot Nothing Then
            If ReportServerUrl.Length > 0 Then
                'skip
            Else
                'it's empty
                ReportServerUrl = getParameters(0, "ReportServerUrl")
            End If
        Else
            'it's nothing
            ReportServerUrl = getParameters(0, "ReportServerUrl")
        End If
        'Dim ReportServerUrl As String = getParameters(0, "ReportServerUrl")

        reportPath = getParameters(0, "ReportPathPrefix") & reportPath & getParameters(0, "ReportPathSuffix")

        'Set the report server URL and report path
        'serverReport.ReportServerUrl = New Uri("http://<Server Name>/reportserver")
        'serverReport.ReportPath = "/AdventureWorks Sample Reports/Sales Order Detail"
        serverReport.ReportServerUrl = New Uri(ReportServerUrl)
        serverReport.ReportPath = reportPath

        ''Create the sales order number report parameter
        'Dim salesOrderNumber As New ReportParameter()
        'salesOrderNumber.Name = "SalesOrderNumber"
        'salesOrderNumber.Values.Add("SO43661")

        ''Set the report parameters for the report
        'Dim parameters() As ReportParameter = {salesOrderNumber}

        Try
            If reportParameters IsNot Nothing Then
                If reportParameters.Count > 0 Then
                    Dim paramList As New Generic.List(Of ReportParameter)
                    'Dim pair As KeyValuePair(Of String, String)
                    For Each pair As KeyValuePair(Of String, String) In reportParameters
                        'if you have report parameters - add them here
                        paramList.Add(New ReportParameter(pair.Key, pair.Value, True))
                    Next

                    ReportViewer1.ShowParameterPrompts = False
                    serverReport.SetParameters(paramList)
                Else

                End If
            End If

            'serverReport.SetParameters(parameters)
        Catch ex As Exception
            MessageBox.Show("ReportServerUrl=" & ReportServerUrl & vbCrLf & "ReportPath=" & reportPath & vbCrLf & ex.Message)
        End Try

        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.FullPage
        'ReportViewer1.ZoomPercent = 100

        'Refresh the report
        ReportViewer1.RefreshReport()
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub

    'Private Sub SetReportParameters()
    '    'Set Processing Mode
    '    ReportViewer1.ProcessingMode = ProcessingMode.Remote
    '    ' Set report server and report path
    '    ReportViewer1.ServerReport.ReportServerUrl = New Uri(SSRS)

    '    'ReportViewer1.ServerReport.ReportPath = "/YOUR-REPORT-PATH"
    '    ReportViewer1.ServerReport.ReportPath = reportPath

    '    Dim paramList As New Generic.List(Of ReportParameter)
    '    Dim pInfo As ReportParameterInfoCollection
    '    pInfo = ReportViewer1.ServerReport.GetParameters()

    '    If reportParameters IsNot Nothing Then
    '        If reportParameters.Count > 0 Then
    '            'Dim i As Integer = 0
    '            Dim pair As KeyValuePair(Of String, String)
    '            For Each pair In reportParameters
    '                ' Display Key and Value.
    '                'Console.WriteLine("{0}, {1}", pair.Key, pair.Value)
    '                'da.SelectCommand.Parameters.AddWithValue(pair.Key, pair.Value)

    '                'if you have report parameters - add them here
    '                paramList.Add(New ReportParameter(pair.Key, pair.Value, True))
    '                'paramList.Add(New ReportParameter("PARAM2-EXAMPLE", "2", True))

    '                'parameters(i) = New ParameterValue()
    '                'parameters(i).Name = pair.Key
    '                'parameters(i).Value = pair.Value
    '                'i += 1
    '            Next

    '            ReportViewer1.ServerReport.SetParameters(paramList)
    '        End If
    '    End If

    '    ' Process and render the report
    '    ReportViewer1.ServerReport.Refresh()

    '    ''output as PDF
    '    'Dim returnValue As Byte()
    '    'Dim format As String = "PDF"
    '    'Dim deviceinfo As String = ""
    '    'Dim mimeType As String = ""
    '    'Dim encoding As String = ""
    '    'Dim extension As String = "pdf"
    '    'Dim streams As String() = Nothing
    '    'Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing

    '    'returnValue = ReportViewer1.ServerReport.Render(format, deviceinfo, mimeType, encoding, extension, streams, warnings)
    '    'Response.Buffer = True
    '    'Response.Clear()

    '    'Response.ContentType = mimeType

    '    'Response.AddHeader("content-disposition", "attachment; filename=YOUR-OUTPUT-FILE-NAME.pdf")

    '    'Response.BinaryWrite(returnValue)
    '    'Response.Flush()
    '    'Response.End()
    'End Sub

    '<Serializable()> _
    'Public NotInheritable Class MyReportServerCredentials
    '    Implements IReportServerCredentials
    '    'Public userName As String = ConfigurationManager.AppSettings("rvUser")
    '    'Public password As String = ConfigurationManager.AppSettings("rvPassword")
    '    'Public domain As String = ConfigurationManager.AppSettings("rvDomain")

    '    Public ReadOnly Property ImpersonationUser() As WindowsIdentity _
    '            Implements IReportServerCredentials.ImpersonationUser
    '        Get
    '            'Use the default windows user.  Credentials will be
    '            'provided by the NetworkCredentials property.
    '            Return Nothing
    '        End Get
    '    End Property

    '    Public ReadOnly Property NetworkCredentials() As ICredentials _
    '            Implements IReportServerCredentials.NetworkCredentials
    '        Get
    '            'Read the user information from the web.config file. 
    '            'By reading the information on demand instead of storing
    '            'it, the credentials will not be stored in session,
    '            'reducing the vulnerable surface area to the web.config
    '            'file, which can be secured with an ACL.

    '            If (String.IsNullOrEmpty(userName)) Then
    '                Throw New Exception("Missing user name from web.config file")
    '            End If

    '            If (String.IsNullOrEmpty(password)) Then
    '                Throw New Exception("Missing password from web.config file")
    '            End If

    '            If (String.IsNullOrEmpty(domain)) Then
    '                Throw New Exception("Missing domain from web.config file")
    '            End If

    '            Return New NetworkCredential(userName, password, domain)

    '        End Get
    '    End Property

    '    Public Function GetFormsCredentials(ByRef authCookie As Cookie, _
    '                                        ByRef userName As String, _
    '                                        ByRef password As String, _
    '                                        ByRef authority As String) _
    '                                        As Boolean _
    '            Implements IReportServerCredentials.GetFormsCredentials

    '        authCookie = Nothing
    '        userName = Nothing
    '        password = Nothing
    '        authority = Nothing

    '        'Not using form credentials
    '        Return False

    '    End Function


    '    'Private Sub temp1()
    '    '    Dim rs As New ReportExecution2005.ReportExecutionService

    '    '    'Dim userName As String = user_name
    '    '    'Dim password As String = user_pass

    '    '    'Dim hdr As String = "Authorization: Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(Convert.ToString(userName & Convert.ToString(":")) & password)) + System.Environment.NewLine

    '    '    'WebBrowser1.Navigate([String].Format("http://{0}:{1}@" & URL, userName, password), Nothing, Nothing, hdr)
    '    '    'WebBrowser1.DocumentStream()

    '    '    'get report server

    '    '    'Dim rs As New ReportExecutionService()
    '    '    rs.Credentials = New System.Net.NetworkCredential("demo", "P@$$w0rd", "FTI") ' System.Net.CredentialCache.DefaultCredentials
    '    '    'rs.Url = "http://myserver/reportserver/ReportExecution2005.asmx"
    '    '    rs.Url = "http://ftimember.off.fti.or.th/_vti_bin/ReportServer/ReportExecution2005.asmx"

    '    '    ' Render arguments
    '    '    Dim result As Byte() = Nothing
    '    '    'Dim reportPath As String = "/AdventureWorks Sample Reports/Employee Sales Summary"
    '    '    'Dim reportPath As String = "/AdventureWorks Sample Reports/Employee Sales Summary"
    '    '    Dim format As String = "MHTML"
    '    '    Dim historyID As String = Nothing
    '    '    Dim devInfo As String = "<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>"

    '    '    ' Prepare report parameter.
    '    '    'Dim parameters As ParameterValue() = New ParameterValue(2) {}
    '    '    Dim parameters As ParameterValue() = Nothing

    '    '    If reportParameters IsNot Nothing Then
    '    '        If reportParameters.Count > 0 Then
    '    '            parameters = New ParameterValue(reportParameters.Count - 1) {}
    '    '            Dim i As Integer = 0
    '    '            Dim pair As KeyValuePair(Of String, String)
    '    '            For Each pair In reportParameters
    '    '                ' Display Key and Value.
    '    '                'Console.WriteLine("{0}, {1}", pair.Key, pair.Value)
    '    '                'da.SelectCommand.Parameters.AddWithValue(pair.Key, pair.Value)

    '    '                parameters(i) = New ParameterValue()
    '    '                parameters(i).Name = pair.Key
    '    '                parameters(i).Value = pair.Value
    '    '                i += 1
    '    '            Next

    '    '            'parameters(0) = New ParameterValue()
    '    '            'parameters(0).Name = "EmpID"
    '    '            'parameters(0).Value = "288"

    '    '            'parameters(1) = New ParameterValue()
    '    '            'parameters(1).Name = "ReportMonth"
    '    '            'parameters(1).Value = "6"
    '    '            '' June
    '    '            'parameters(2) = New ParameterValue()
    '    '            'parameters(2).Name = "ReportYear"
    '    '            'parameters(2).Value = "2004"
    '    '        End If
    '    '    End If

    '    '    Dim credentials As FTI.ReportExecution2005.DataSourceCredentials() = Nothing
    '    '    Dim showHideToggle As String = Nothing
    '    '    Dim encoding As String = Nothing
    '    '    Dim mimeType As String = Nothing
    '    '    Dim extension As String = Nothing
    '    '    Dim warnings As FTI.ReportExecution2005.Warning() = Nothing
    '    '    Dim reportHistoryParameters As ParameterValue() = Nothing
    '    '    Dim streamIDs As String() = Nothing

    '    '    Dim execInfo As New ExecutionInfo()
    '    '    Dim execHeader As New ExecutionHeader()

    '    '    rs.ExecutionHeaderValue = execHeader

    '    '    Dim SessionId As String = Nothing
    '    '    Try
    '    '        execInfo = rs.LoadReport(reportPath, historyID)

    '    '        If reportParameters IsNot Nothing Then
    '    '            If reportParameters.Count > 0 Then
    '    '                rs.SetExecutionParameters(parameters, "en-us")
    '    '            End If
    '    '        End If

    '    '        SessionId = rs.ExecutionHeaderValue.ExecutionID
    '    '    Catch ex As Exception
    '    '        MessageBox.Show(ex.Message, "SessionId")
    '    '        Exit Sub
    '    '    End Try

    '    '    'Console.WriteLine("SessionID: {0}", rs.ExecutionHeaderValue.ExecutionID)


    '    '    Try

    '    '        result = rs.Render(format, devInfo, extension, encoding, mimeType, warnings, streamIDs)

    '    '        execInfo = rs.GetExecutionInfo()

    '    '        'Console.WriteLine("Execution date and time: {0}", execInfo.ExecutionDateTime)
    '    '    Catch ex As SoapException
    '    '        'Console.WriteLine(ex.Detail.OuterXml)
    '    '        MessageBox.Show(ex.Message, "execInfo")
    '    '        Exit Sub
    '    '    End Try
    '    '    ' Write the contents of the report to an MHTML file.
    '    '    'Try
    '    '    '    Dim stream As FileStream = File.Create("report.mht", result.Length)
    '    '    '    Console.WriteLine("File created.")
    '    '    '    stream.Write(result, 0, result.Length)
    '    '    '    Console.WriteLine("Result written to the file.")
    '    '    '    stream.Close()
    '    '    'Catch e As Exception
    '    '    '    Console.WriteLine(e.Message)
    '    '    'End Try

    '    '    Dim stream As Stream = New MemoryStream(result)
    '    '    'WebBrowser1.DocumentStream = stream
    '    '    'WebBrowser1.Refresh()
    '    '    Me.ReportViewer1.RefreshReport()
    '    'End Sub
    'End Class
End Class