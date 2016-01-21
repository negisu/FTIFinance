Imports System.ServiceModel
Imports Microsoft.Reporting.WinForms

'Imports Telerik.WinControls

Public Class frmMain

    Dim isSuccess As Boolean

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed Then
            Dim version As String = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
            Me.Text &= " " & version
        End If
        'ThemeResolutionService.ApplicationThemeName = "Desert"
        'Application.OpenForms["frmMain"].Controls["dgvPlaylist"] as DataGridView;
        'Application.OpenForms("").Controls("")
        'For Each ctl As Control In Me.Controls
        '    ctl.Name = ""
        '    'ctl.Enabled = False
        '    Dim text1 As TextBox = CType(Me.Controls("text_1"), TextBox)
        'Next

        ToolStripStatusLabel1.Text = String.Empty

        'user_groups = New Collection

        Dim binding As BasicHttpBinding = New BasicHttpBinding()
        binding.MaxBufferPoolSize = 2147483647
        binding.MaxBufferSize = 2147483647
        binding.MaxReceivedMessageSize = 2147483647
        binding.ReaderQuotas.MaxStringContentLength = 2147483647
        binding.ReaderQuotas.MaxArrayLength = 2147483647
        binding.ReaderQuotas.MaxDepth = 2147483647
        binding.ReaderQuotas.MaxBytesPerRead = 2147483647

        client = New iSQL.webSQLClient
        client.Endpoint.Binding = binding

        'config date format
        ciTH = New System.Globalization.CultureInfo("th-TH", False)
        With ciTH
            .DateTimeFormat.DateSeparator = "/"
            .DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            .DateTimeFormat.LongDatePattern = "dddd, MMMM dd, yyyy"
            .DateTimeFormat.TimeSeparator = ":"
            .DateTimeFormat.ShortTimePattern = "HH:mm"
            .DateTimeFormat.LongTimePattern = "HH:mm:ss"
            '.DateTimeFormat.AMDesignator = "AM"
            '.DateTimeFormat.PMDesignator = "PM"
            '.NumberFormat.CurrencySymbol = "฿"
        End With
        ciEN = New System.Globalization.CultureInfo("en-GB", False)
        With ciEN
            .DateTimeFormat.DateSeparator = "/"
            .DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            .DateTimeFormat.LongDatePattern = "dddd, MMMM dd, yyyy"
            .DateTimeFormat.TimeSeparator = ":"
            .DateTimeFormat.ShortTimePattern = "HH:mm"
            .DateTimeFormat.LongTimePattern = "HH:mm:ss"
            '.DateTimeFormat.AMDesignator = "AM"
            '.DateTimeFormat.PMDesignator = "PM"
            '.NumberFormat.CurrencySymbol = "฿"
        End With

        '==============================================
        isSuccess = False
        Dim f As New frmMainLogin
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '==============================================
            'Dim list = AppDomain.CurrentDomain.GetAssemblies().ToList(). _
            '        SelectMany(Function(s) s.GetTypes()). _
            '        Where(Function(p) (p.BaseType Is [GetType]().BaseType AndAlso _
            '                           p.Assembly Is [GetType]().Assembly))
            'For Each type As Type In list
            '    Dim typeName As String = type.Name
            'Next
            '==============================================
            'generate INSTITUTION menu
            generateINSTITUTION()
            '==============================================

            Dim err As Integer = 0
            Try
                getPermissions(Me.Name, err, Me.Controls)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "ERROR=" & err)
            End Try

            isSuccess = True

            checkAfterLogon(sender, e)
            ToolStripStatusLabel1.Text = String.Format("{0} {1} ({2})", user_firstname, user_lastname, user_department)


        End If
        f.Dispose()
        f = Nothing

        If isSuccess = False Then Me.Close()

        tsFINANCE.Enabled = True
    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles tsExit.Click
        Me.Close()
    End Sub

    Private Sub checkAfterLogon(sender As Object, e As EventArgs)
        'check FTI new
        Dim rows As New Dictionary(Of String, String)

        If tsFTIapproveMember.Enabled = True And tsFTI.Enabled = True Then
            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", "000")
            parameters.Add("@p1", "100")
            parameters.Add("@p2", "200")
            parameters.Add("@p3", "999")
            'parameters.Add("@p1", "000")

            Dim query As String = String.Empty
            query &= "SELECT    COUNT(*) AS CNT "
            query &= "FROM            MB_MEMBER INNER JOIN "
            query &= "                         MB_COMP_PERSON ON MB_MEMBER.OU_CODE = MB_COMP_PERSON.OU_CODE AND MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
            query &= "                         MB_MEMBER_MAIN_GROUP ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE INNER JOIN "
            query &= "                         MB_MEMBER_GROUP ON MB_MEMBER.MEMBER_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_GROUP_CODE AND "
            query &= "                         MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_MAIN_GROUP_CODE "
            query &= "WHERE       (DATEDIFF(dd, MB_MEMBER.REGIST_DATE, GETDATE()) > " & CInt(getParameters(1, "FTI_STATUS_NEW_AGE")) & ") AND (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN (@p0, @p1, @p2, @p3)) AND (MB_MEMBER.MEMBER_STATUS_CODE = 'X') "
            query &= " AND (MB_MEMBER.MEMBER_CODE = '<AUTO>') "

            Dim CNT As Integer = 0
            Try
                CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
                Exit Sub
            End Try

            If CNT > 0 Then
                'If MessageBox.Show("คุณมี" & tsFTIapproveMember.Text & "คงค้าง " & CNT & " รายการ. คุณต้องการตรวจสอบหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '    tsFTIapproveMember_Click(sender, e)
                'End If

                rows.Add("tsFTIapproveMember", tsFTIapproveMember.Text & "," & CNT)
            End If

        End If

        'check FTI group // province
        If tsFTIapproveMemberGP.Enabled = True And tsFTI.Enabled = True Then
            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", "000")
            parameters.Add("@p1", "100")
            parameters.Add("@p2", "200")
            parameters.Add("@p3", "999")
            'parameters.Add("@p1", "000")

            Dim query As String = String.Empty
            query &= "SELECT    COUNT(*) AS CNT "
            query &= "FROM            MB_MEMBER INNER JOIN "
            query &= "                         MB_COMP_PERSON ON MB_MEMBER.OU_CODE = MB_COMP_PERSON.OU_CODE AND MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
            query &= "                         MB_MEMBER_MAIN_GROUP ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE INNER JOIN "
            query &= "                         MB_MEMBER_GROUP ON MB_MEMBER.MEMBER_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_GROUP_CODE AND "
            query &= "                         MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_MAIN_GROUP_CODE "
            query &= "WHERE       (DATEDIFF(dd, MB_MEMBER.REGIST_DATE, GETDATE()) > " & CInt(getParameters(1, "FTI_STATUS_GROUP_ADD_AGE")) & ") AND (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN (@p0, @p1, @p2, @p3)) AND (MB_MEMBER.MEMBER_STATUS_CODE = 'X') "
            query &= " AND (MB_MEMBER.MEMBER_CODE <> '<AUTO>') "

            Dim CNT As Integer = 0
            Try
                CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
                Exit Sub
            End Try

            If CNT > 0 Then
                'If MessageBox.Show("คุณมี" & tsFTIapproveMemberGP.Text & "คงค้าง " & CNT & " รายการ. คุณต้องการตรวจสอบหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '    tsFTIapproveMemberGP_Click(sender, e)
                'End If
                rows.Add("tsFTIapproveMemberGP", tsFTIapproveMemberGP.Text & "," & CNT)
            End If

        End If

        'check FTI resign
        If tsFTIapproveStatusResign.Enabled = True And tsFTI.Enabled = True Then
            Dim parameters As New Dictionary(Of String, Object)

            Dim query As String = String.Empty
            query &= "SELECT        COUNT(*) AS CNT "
            query &= "FROM            MB_MEMBER_RETIRE LEFT OUTER JOIN "
            query &= "                         MB_MEMBER ON MB_MEMBER_RETIRE.REGIST_CODE = MB_MEMBER.REGIST_CODE LEFT OUTER JOIN "
            query &= "                         MB_COMP_PERSON ON MB_MEMBER_RETIRE.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE "
            query &= "WHERE        (DATEDIFF(dd, MB_MEMBER_RETIRE.RETIRE_DATE, GETDATE()) > " & CInt(getParameters(1, "FTI_STATUS_RESIGN_AGE")) & ") AND (MB_MEMBER_RETIRE.APPROVE_DATE IS NULL) AND (MB_MEMBER_RETIRE.MEMBER_MAIN_GROUP_CODE IN (" & getParameters(1, "FTI_MAIN_GROUP_APPROVE") & ")) AND (MB_MEMBER_RETIRE.RETIRE_TYPE = 'C') "
            'query &= "ORDER BY MB_MEMBER_RETIRE.RETIRE_DATE DESC"

            Dim CNT As Integer = 0
            Try
                CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
                Exit Sub
            End Try

            If CNT > 0 Then
                'If MessageBox.Show("คุณมี" & tsFTIapproveStatusResign.Text & "คงค้าง " & CNT & " รายการ. คุณต้องการตรวจสอบหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '    tsFTIapproveStatusResign_Click(sender, e)
                'End If
                rows.Add("tsFTIapproveStatusResign", tsFTIapproveStatusResign.Text & "," & CNT)
            End If

        End If

        'check FTI delete release
        If tsFTIDeleteRelease.Enabled = True And tsFTI.Enabled = True Then
            Dim sFTI_STATUS_BLACKLIST_RELEASE() As String = getParameters(1, "FTI_STATUS_BLACKLIST_RELEASE").Split("|"c)
            Dim query As String = String.Empty
            'query &= "SELECT        MB_MEMBER_RETIRE.REGIST_CODE, MB_MEMBER.MEMBER_CODE, MB_COMP_PERSON.COMP_PERSON_NAME_TH, "
            'query &= "                         MB_COMP_PERSON.COMP_PERSON_NAME_EN, MB_MEMBER_RETIRE.MEMBER_MAIN_GROUP_CODE, MB_MEMBER_RETIRE.MEMBER_GROUP_CODE, "
            'query &= "                         MB_MEMBER_RETIRE.MEMBER_MAIN_TYPE_CODE, MB_MEMBER_RETIRE.MEMBER_TYPE_CODE, MB_MEMBER_RETIRE.COMP_PERSON_CODE, "
            'query &= "                         MB_MEMBER_RETIRE.RETIRE_TYPE, MB_MEMBER_RETIRE.RETIRE_DATE, MB_MEMBER_RETIRE.RETIRE_REASON, "
            'query &= "                         MB_MEMBER_RETIRE.MEMBER_STATUS_CODE, MB_MEMBER_RETIRE.MEMBER_CODE "
            query &= "SELECT COUNT(*) AS CNT "
            query &= "FROM            MB_MEMBER_RETIRE LEFT OUTER JOIN "
            query &= "                         MB_MEMBER ON MB_MEMBER_RETIRE.REGIST_CODE = MB_MEMBER.REGIST_CODE LEFT OUTER JOIN "
            query &= "                         MB_COMP_PERSON ON MB_MEMBER_RETIRE.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE "
            query &= "WHERE        (DATEDIFF(mm, MB_MEMBER_RETIRE.APPROVE_DATE, GETDATE()) > " & CInt(getParameters(1, "FTI_STATUS_BLACKLIST_RELEASE_AGE")) & ") AND (MB_MEMBER_RETIRE.MEMBER_MAIN_GROUP_CODE IN (" & getParameters(1, "FTI_MAIN_GROUP_APPROVE") & ")) AND (MB_MEMBER_RETIRE.RETIRE_TYPE = '" & sFTI_STATUS_BLACKLIST_RELEASE(0) & "') "
            'query &= "ORDER BY MB_MEMBER_RETIRE.RETIRE_DATE DESC"

            Dim parameters As New Dictionary(Of String, Object)
            Dim CNT As Integer = 0
            Try
                CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
                Exit Sub
            End Try

            If CNT > 0 Then
                'If MessageBox.Show("คุณมี" & tsFTIDeleteRelease.Text & "คงค้าง " & CNT & " รายการ. คุณต้องการตรวจสอบหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '    tsFTIDeleteRelease_Click(sender, e)
                'End If
                rows.Add("tsFTIDeleteRelease", tsFTIDeleteRelease.Text & "," & CNT)
            End If

        End If

        '============== GS1
        'check GS1 new approve
        If tsGS1memberApproval.Enabled = True And tsGS1.Enabled = True Then
            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p0", "300")
            'parameters.Add("@p1", "100")
            'parameters.Add("@p2", "200")
            'parameters.Add("@p3", "999")
            'parameters.Add("@p1", "000")

            Dim query As String = String.Empty
            query &= "SELECT    COUNT(*) AS CNT "
            query &= "FROM            MB_MEMBER INNER JOIN "
            query &= "                         MB_COMP_PERSON ON MB_MEMBER.OU_CODE = MB_COMP_PERSON.OU_CODE AND MB_MEMBER.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE INNER JOIN "
            query &= "                         MB_MEMBER_MAIN_GROUP ON MB_MEMBER.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE INNER JOIN "
            query &= "                         MB_MEMBER_GROUP ON MB_MEMBER.MEMBER_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_GROUP_CODE AND "
            query &= "                         MB_MEMBER_MAIN_GROUP.MEMBER_MAIN_GROUP_CODE = MB_MEMBER_GROUP.MEMBER_MAIN_GROUP_CODE "
            query &= "WHERE        (DATEDIFF(dd, MB_MEMBER.REGIST_DATE, GETDATE()) > " & CInt(getParameters(2, "GS1_STATUS_NEW_AGE")) & ") AND (MB_MEMBER.MEMBER_MAIN_GROUP_CODE IN (@p0)) AND (MB_MEMBER.MEMBER_STATUS_CODE IN ('X')) "
            query &= " AND (MB_MEMBER.MEMBER_CODE IN ('<AUTO>', '<PAID>')) "

            Dim CNT As Integer = 0
            Try
                CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
                Exit Sub
            End Try

            If CNT > 0 Then
                'If MessageBox.Show("คุณมี" & tsGS1memberApproval.Text & "คงค้าง " & CNT & " รายการ. คุณต้องการตรวจสอบหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '    tsGS1memberApproval_Click(sender, e)
                'End If
                rows.Add("tsGS1memberApproval", tsGS1memberApproval.Text & "," & CNT)
            End If
        End If

        'check GS1 expand
        If tsGS1expand.Enabled = True And tsGS1.Enabled = True Then
            Dim parameters As New Dictionary(Of String, Object)

            Dim query As String = String.Empty
            query &= "SELECT        COUNT(*) AS CNT "
            query &= "FROM            MB_MEMBER_RETIRE LEFT OUTER JOIN "
            query &= "                         MB_MEMBER ON MB_MEMBER_RETIRE.REGIST_CODE = MB_MEMBER.REGIST_CODE LEFT OUTER JOIN "
            query &= "                         MB_COMP_PERSON ON MB_MEMBER_RETIRE.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE "
            query &= "WHERE        (DATEDIFF(dd, MB_MEMBER_RETIRE.RETIRE_DATE, GETDATE()) > " & CInt(getParameters(2, "GS1_STATUS_EXPAND")) & ") AND (MB_MEMBER_RETIRE.APPROVE_DATE IS NULL) AND (MB_MEMBER_RETIRE.RETIRE_TYPE = 'E') "
            'query &= "ORDER BY MB_MEMBER_RETIRE.RETIRE_DATE DESC"

            Dim CNT As Integer = 0
            Try
                CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
                Exit Sub
            End Try

            If CNT > 0 Then
                'If MessageBox.Show("คุณมี" & tsGS1expand.Text & "คงค้าง " & CNT & " รายการ. คุณต้องการตรวจสอบหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '    tsGS1expand_Click(sender, e)
                'End If
                rows.Add("tsGS1expand", tsGS1expand.Text & "," & CNT)
            End If
        End If

        'check GS1 resign
        If tsGS1resign.Enabled = True And tsGS1.Enabled = True Then
            Dim parameters As New Dictionary(Of String, Object)

            Dim query As String = String.Empty
            query &= "SELECT        COUNT(*) AS CNT "
            query &= "FROM            MB_MEMBER_RETIRE LEFT OUTER JOIN "
            query &= "                         MB_MEMBER ON MB_MEMBER_RETIRE.REGIST_CODE = MB_MEMBER.REGIST_CODE LEFT OUTER JOIN "
            query &= "                         MB_COMP_PERSON ON MB_MEMBER_RETIRE.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE "
            query &= "WHERE        (DATEDIFF(dd, MB_MEMBER_RETIRE.RETIRE_DATE, GETDATE()) > " & CInt(getParameters(2, "GS1_STATUS_RESIGN_AGE")) & ") AND (MB_MEMBER_RETIRE.APPROVE_DATE IS NULL) AND (MB_MEMBER_RETIRE.MEMBER_MAIN_GROUP_CODE IN (" & getParameters(2, "GS1_MAIN_GROUP_APPROVE") & ")) AND (MB_MEMBER_RETIRE.RETIRE_TYPE = 'C') "
            'query &= "ORDER BY MB_MEMBER_RETIRE.RETIRE_DATE DESC"

            Dim CNT As Integer = 0
            Try
                CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
                Exit Sub
            End Try

            If CNT > 0 Then
                'If MessageBox.Show("คุณมี" & tsGS1resign.Text & "คงค้าง " & CNT & " รายการ. คุณต้องการตรวจสอบหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '    tsGS1resign_Click(sender, e)
                'End If
                rows.Add("tsGS1resign", tsGS1resign.Text & "," & CNT)
            End If
        End If

        'check GS1 blacklist
        If tsGS1blacklist.Enabled = True And tsGS1.Enabled = True Then
            Dim parameters As New Dictionary(Of String, Object)

            Dim query As String = String.Empty
            query &= "SELECT        COUNT(*) AS CNT "
            query &= "FROM            MB_MEMBER_RETIRE LEFT OUTER JOIN "
            query &= "                         MB_MEMBER ON MB_MEMBER_RETIRE.REGIST_CODE = MB_MEMBER.REGIST_CODE LEFT OUTER JOIN "
            query &= "                         MB_COMP_PERSON ON MB_MEMBER_RETIRE.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE "
            query &= "WHERE        (DATEDIFF(dd, MB_MEMBER_RETIRE.RETIRE_DATE, GETDATE()) > " & CInt(getParameters(2, "GS1_STATUS_BLACKLIST_AGE")) & ") AND (MB_MEMBER_RETIRE.APPROVE_DATE IS NULL) AND (MB_MEMBER_RETIRE.RETIRE_TYPE = 'D') "
            'query &= "ORDER BY MB_MEMBER_RETIRE.RETIRE_DATE DESC"

            Dim CNT As Integer = 0
            Try
                CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
                Exit Sub
            End Try

            If CNT > 0 Then
                'If MessageBox.Show("คุณมี" & tsGS1blacklist.Text & "คงค้าง " & CNT & " รายการ. คุณต้องการตรวจสอบหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '    tsGS1blacklist_Click(sender, e)
                'End If
                rows.Add("tsGS1blacklist", tsGS1blacklist.Text & "," & CNT)
            End If
        End If

        'check GS1 comeback
        If tsGS1comeback.Enabled = True And tsGS1.Enabled = True Then
            Dim parameters As New Dictionary(Of String, Object)

            Dim query As String = String.Empty
            query &= "SELECT        COUNT(*) AS CNT "
            query &= "FROM            MB_MEMBER_RETIRE LEFT OUTER JOIN "
            query &= "                         MB_MEMBER ON MB_MEMBER_RETIRE.REGIST_CODE = MB_MEMBER.REGIST_CODE LEFT OUTER JOIN "
            query &= "                         MB_COMP_PERSON ON MB_MEMBER_RETIRE.COMP_PERSON_CODE = MB_COMP_PERSON.COMP_PERSON_CODE "
            query &= "WHERE        (DATEDIFF(dd, MB_MEMBER_RETIRE.RETIRE_DATE, GETDATE()) > " & CInt(getParameters(2, "GS1_STATUS_HOLD_AGE")) & ") AND (MB_MEMBER_RETIRE.APPROVE_DATE IS NULL) AND (MB_MEMBER_RETIRE.RETIRE_TYPE = 'S') "
            'query &= "ORDER BY MB_MEMBER_RETIRE.RETIRE_DATE DESC"

            Dim CNT As Integer = 0
            Try
                CNT = CInt(client.ExecuteScalar(query, parameters, user_session))
            Catch ex As Exception
                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteScalar")
                Exit Sub
            End Try

            If CNT > 0 Then
                'If MessageBox.Show("คุณมี" & tsGS1comeback.Text & "คงค้าง " & CNT & " รายการ. คุณต้องการตรวจสอบหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '    tsGS1comeback_Click(sender, e)
                'End If
                rows.Add("tsGS1comeback", tsGS1comeback.Text & "," & CNT)
            End If
        End If

        If rows.Count > 0 Then
            Dim f As New frmMainOutStanding
            f.rows = rows
            If f.ShowDialog(Me) = DialogResult.OK Then
                '
            End If
            f.Dispose()
            f = Nothing
        End If
    End Sub

    Private Sub generateINSTITUTION()
        Dim menus() As String = getParameters(0, "Institutions").Split(","c)
        ReDim fINI(menus.Count)
        ReDim fFeeINI(menus.Count)

        For i As Integer = 0 To menus.Count - 1
            'Add a submenu to Menu 1
            Dim info() As String = menus(i).Split("|"c)
            Dim menu As New ToolStripMenuItem() With {.Text = info(0), .Name = info(1), .Tag = info(2), .AccessibleDescription = i.ToString}
            'We have a reference to menu1 already, but here's how you can find the menu item by name...
            tsINSTITUTION.DropDownItems.Add(menu)
            AddHandler menu.Click, AddressOf mnuItem_Clicked
            'For Each item As ToolStripMenuItem In ContextMenuStrip1.Items
            '    If item.Name = "mnuItem1" Then
            '        item.DropDownItems.Add(menu2)
            '        AddHandler menu2.Click, AddressOf mnuItem_Clicked
            '    End If
            'Next
            'Dim fruitToolStripMenuItem As New ToolStripMenuItem("Fruit", Nothing, , "Fruit")
            'tsINSTITUTION.ite()
        Next

    End Sub

    Private Sub mnuItem_Clicked(sender As Object, e As EventArgs)
        'ContextMenuStrip1.Hide() 'Sometimes the menu items can remain open.  May not be necessary for you.
        Dim item As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
        If item IsNot Nothing Then
            'MsgBox("You've clicked " & item.Name)
            'MessageBox.Show("You've clicked " & item.Tag.ToString)
            Dim INI_ID As Integer = CInt(item.AccessibleDescription)

            If fINI(INI_ID) Is Nothing Then
                fINI(INI_ID) = New frmINI
                fINI(INI_ID).MdiParent = Me
                fINI(INI_ID).WindowState = FormWindowState.Maximized

                fINI(INI_ID).INI_CODE = item.Tag.ToString
                fINI(INI_ID).INI_NAME = item.Text
                fINI(INI_ID).INI_ID = INI_ID

                fINI(INI_ID).Show()
            Else
                'focus opening form
                fINI(INI_ID).Show()
                fINI(INI_ID).Focus()
            End If

            'Dim f As New frmINI
            'f.WindowState = FormWindowState.Maximized
            'f.INI_CODE = item.Tag.ToString
            'f.INI_NAME = item.Text
            'f.INI_ID = CInt(item.AccessibleDescription)
            'If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '    '
            'End If
            'f.Dispose()
            'f = Nothing
        End If
    End Sub

    Private Sub ToolStripMenuItem7_Click(sender As Object, e As EventArgs) Handles tsFTImember.Click
        'fti
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If fFTI Is Nothing Then
            fFTI = New frmFTI
            fFTI.MdiParent = Me
            fFTI.WindowState = FormWindowState.Maximized
            fFTI.Show()
        Else
            'focus opening form
            fFTI.Show()
            fFTI.Focus()
        End If
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If isSuccess = True Then
            'loged on
            If (MessageBox.Show("คุณต้องการที่จะปิดโปรแกรม ใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No) Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        client.Close()
    End Sub

    Private Sub ToolStripMenuItem19_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem19.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmFTIRDLCnewApplyInv
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem20_Click(sender As Object, e As EventArgs) Handles tsFTIfee.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        'fee process
        If fFee Is Nothing Then
            fFee = New frmFTIFees
            fFee.MdiParent = Me
            fFee.WindowState = FormWindowState.Maximized
            fFee.Show()
        Else
            'focus opening form
            fFee.Show()
            fFee.Focus()
        End If
    End Sub

    Private Sub ToolStripMenuItem22_Click(sender As Object, e As EventArgs)
        'fti new member
        Dim f As New frmFTInew
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            'add into fti member // wait for approve
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem21_Click(sender As Object, e As EventArgs) Handles tsFTIconsideration.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        'fti consideration
        Dim f As New frmFTIconsideration
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            'do nothing
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem29_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem29.Click
        Dim f As New frmFTImemberConfirm
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem30_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem30.Click
        Dim f As New frmFTImemberApproval
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem31_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem31.Click
        Dim f As New frmFTIconsideration
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            'do nothing
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem40_Click(sender As Object, e As EventArgs)
        'fti approve
        Dim f As New frmFTIapproval
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            'do nothing
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem43_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem43.Click
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmFTIRDLCnewApply
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Friend Sub tsFTIapproveMember_Click(sender As Object, e As EventArgs) Handles tsFTIapproveMember.Click
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmFTIapproval
        f.MEMBER_CODE = "<AUTO>"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem13_Click(sender As Object, e As EventArgs) Handles tsFTIparameters.Click
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainParameters
        f.MODULE_ID = 1
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem15_Click(sender As Object, e As EventArgs)
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainPositions
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Friend Sub tsFTIapproveStatusResign_Click(sender As Object, e As EventArgs) Handles tsFTIapproveStatusResign.Click
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmFTIapprovalStatus
        f.RETIRE_TYPE = "C"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem40_Click_1(sender As Object, e As EventArgs) Handles tsFTIsmes.Click
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmFTIsmes
        f.MODULE_ID = 1
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem44_Click(sender As Object, e As EventArgs) Handles tsFTIstatus.Click
        'status type
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainStatus
        f.MODULE_ID = 1
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles tsParameters.Click
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainParameters
        f.MODULE_ID = 0
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing '
    End Sub

    Private Sub ToolStripMenuItem47_Click(sender As Object, e As EventArgs) Handles tsPermissions.Click
        'permission
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        If fPermissions Is Nothing Then
            fPermissions = New frmMainPermissions
            'fPermissions.MODULE_ID = 1
            'fFee.MdiParent = Me
            'fFee.WindowState = FormWindowState.Maximized
            fPermissions.Show()
        Else
            'focus opening form
            fPermissions.Show()
            fPermissions.Focus()
        End If
    End Sub

    Private Sub ToolStripMenuItem45_Click(sender As Object, e As EventArgs) Handles tsQueries.Click
        'query
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainQueries
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem46_Click(sender As Object, e As EventArgs) Handles tsUsers.Click
        'users
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainUsers
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem2_Click_1(sender As Object, e As EventArgs) Handles tsContacts.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainContacts
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles tsPositions.Click
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainPositions
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles tsPreName.Click
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainPreName
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem5_Click_1(sender As Object, e As EventArgs) Handles tsPostAddress.Click
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainLocations
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles tsProvinces.Click
        'provinces
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainProvinces
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem7_Click_1(sender As Object, e As EventArgs) Handles tsAddressType.Click
        'address type
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainAddressType
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem8_Click(sender As Object, e As EventArgs) Handles tsFilesType.Click
        'file type
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmMainFileType
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub tsLogOff_Click(sender As Object, e As EventArgs) Handles tsLogOff.Click
        '==============================================
        'close all child form
        For Each child As Form In Me.MdiChildren
            child.Close()
        Next

        isSuccess = False
        Dim f As New frmMainLogin
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Dim err As Integer = 0
            Try
                'getPermissions(Me.Name, err, Me.Controls, Me.MenuStrip1)
                getPermissions(Me.Name, err, Me.Controls)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "ERROR=" & err)
            End Try

            '==============================================
            'Dim list = AppDomain.CurrentDomain.GetAssemblies().ToList(). _
            '        SelectMany(Function(s) s.GetTypes()). _
            '        Where(Function(p) (p.BaseType Is [GetType]().BaseType AndAlso _
            '                           p.Assembly Is [GetType]().Assembly))
            'For Each type As Type In list
            '    Dim typeName As String = type.Name
            'Next
            '==============================================
            isSuccess = True

            checkAfterLogon(sender, e)
            ToolStripStatusLabel1.Text = String.Format("{0} {1} ({2})", user_firstname, user_lastname, user_department)
        End If
        f.Dispose()
        f = Nothing

        If isSuccess = False Then Me.Close()
    End Sub

    'Private Sub TypeReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
    '    Dim f As New frmTypeReceipt
    '    If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
    '        '
    '    End If
    '    f.Dispose()
    '    f = Nothing
    'End Sub

    Private Sub tsChangePassword_Click(sender As Object, e As EventArgs) Handles tsChangePassword.Click
        Dim f As New frmMainUsersPasswordChange
        f.IS_RESET = False
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub tsFTIcommitteeGroup_Click(sender As Object, e As EventArgs) Handles tsFTIcommitteeGroup.Click
        'fCommitteeGroup
        If fCommitteeGroup Is Nothing Then
            fCommitteeGroup = New frmFTICommitteeGroups
            fCommitteeGroup.MdiParent = Me
            fCommitteeGroup.WindowState = FormWindowState.Maximized
            fCommitteeGroup.Show()
        Else
            'focus opening form
            fCommitteeGroup.Show()
            fCommitteeGroup.Focus()
        End If
    End Sub

    Private Sub tsFTIcommitteeYear_Click(sender As Object, e As EventArgs) Handles tsFTIcommitteeYear.Click
        'fCommittee
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If fCommittee Is Nothing Then
            fCommittee = New frmFTICommittee
            fCommittee.MdiParent = Me
            fCommittee.WindowState = FormWindowState.Maximized
            fCommittee.Show()
        Else
            'focus opening form
            fCommittee.Show()
            fCommittee.Focus()
        End If
    End Sub

    Private Sub tsFTIcommitteeProfiel_Click(sender As Object, e As EventArgs) Handles tsFTIcommitteeProfiel.Click
        Dim f As New frmMainContacts
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    'Private Sub BankToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
    '    Dim f As New frmBank
    '    If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
    '        '
    '    End If
    '    f.Dispose()
    '    f = Nothing
    'End Sub

    'Private Sub BankBranchToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
    '    Dim f As New frmBankBranch
    '    If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
    '        '
    '    End If
    '    f.Dispose()
    '    f = Nothing
    'End Sub

    'Private Sub BankAccountToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
    '    Dim f As New frmBankAccount
    '    If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
    '        '
    '    End If
    '    f.Dispose()
    '    f = Nothing
    'End Sub

    Private Sub tsFTIreport1_2_Click(sender As Object, e As EventArgs) Handles tsFTIreport1_2.Click
        Dim f As New frmFTImemberResign
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim f As New frmFTImemberStatus
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem2_Click_2(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Dim f As New frmFTImemberHold
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem35_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem35.Click
        Dim f As New frmFTIrepresentConfirm
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem36_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem36.Click
        Dim f As New frmFTImemberExisting
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem33_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem33.Click
        Dim f As New frmFTIrepresentLabel
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem3_Click_1(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        Dim f As New frmFTImemberChanged
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem4_Click_1(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        Dim f As New frmFTIrepresentInfo
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem5_Click_2(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        Dim f As New frmFTImemberSummary
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem8_Click_1(sender As Object, e As EventArgs) Handles tsFTIapproveStatusMove.Click
        Dim f As New frmFTIapprovalStatus
        f.RETIRE_TYPE = "T"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem7_Click_2(sender As Object, e As EventArgs) Handles tsFTIapproveStatusHold.Click
        Dim f As New frmFTIapprovalStatus
        f.RETIRE_TYPE = "S"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem6_Click_1(sender As Object, e As EventArgs) Handles tsFTIapproveStatusBlacklist.Click
        Dim f As New frmFTIapprovalStatus
        f.RETIRE_TYPE = "D"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Friend Sub tsFTIDeleteRelease_Click(sender As Object, e As EventArgs) Handles tsFTIDeleteRelease.Click
        Dim f As New frmFTIapprovalStatus
        f.RETIRE_TYPE = "R"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Friend Sub tsFTIapproveMemberGP_Click(sender As Object, e As EventArgs) Handles tsFTIapproveMemberGP.Click
        Dim f As New frmFTIapproval
        f.MEMBER_CODE = "<>"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub tsFTIcommitteeGroupReports_Click(sender As Object, e As EventArgs) Handles tsFTIcommitteeGroupReports.Click
        Dim f As New frmFTICommitteeGroupsReports
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem32_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem32.Click
        '
    End Sub

    Private Sub tsGS1member_Click(sender As Object, e As EventArgs) Handles tsGS1member.Click
        'gs1
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        If fGS1 Is Nothing Then
            fGS1 = New frmGS1
            fGS1.MdiParent = Me
            fGS1.WindowState = FormWindowState.Maximized
            fGS1.Show()
        Else
            'focus opening form
            fGS1.Show()
            fGS1.Focus()
        End If
    End Sub

    Private Sub ToolStripMenuItem16_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem16.Click
        Dim f As New frmMainParameters
        f.MODULE_ID = 2
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem17_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem17.Click
        Dim f As New frmMainStatus
        f.MODULE_ID = 2
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub tsEAN13_Click(sender As Object, e As EventArgs) Handles tsEAN13.Click
        Dim f As New frmGS1ean13
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Friend Sub tsGS1memberApproval_Click(sender As Object, e As EventArgs) Handles tsGS1memberApproval.Click
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim f As New frmGS1approval
        f.MEMBER_CODE = "<AUTO>"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub



    '            Try
    '                executeWebSQL(query, parameters)
    '            Catch ex As Exception
    '                MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
    '            End Try

    '        End If
    '    End If

    '    f.Dispose()
    '    f = Nothing
    'End Sub

    'Private Sub คนหาToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles คนหาToolStripMenuItem1.Click
    '    Dim f As New frmFINInvoiceSearch
    '    If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
    '        Dim TRAN_NO As String = f.DataGridView1.CurrentRow.Cells("TRAN_NO").Value.ToString()
    '        If f.ProcessComboBox.SelectedIndex = 0 Then
    '            'แก้ไข
    '            If fIV Is Nothing Then
    '                fIV = New frmFINInvoice
    '                fIV.TRAN_NOLabel.Text = TRAN_NO
    '                fIV.MdiParent = Me
    '                fIV.WindowState = FormWindowState.Maximized
    '                fIV.Show()
    '            Else
    '                'focus opening form
    '                fIV.Show()
    '                fIV.Focus()
    '            End If
    '        ElseIf f.ProcessComboBox.SelectedIndex = 1 Then
    '            'ยื่นยกเลิก
    '            Dim fPNC As New frmFINPaymentNoticeCancle
    '            fPNC.UserNameLabel.Text = user_firstname & "    " & user_lastname
    '            fPNC.UserDepartmentLabel.Text = user_department
    '            fPNC.DocCode.Text = TRAN_NO
    '            If fPNC.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

    '            End If
    '            f.Dispose()
    '            f = Nothing
    '        ElseIf f.ProcessComboBox.SelectedIndex = 2 Then
    '            'ยื่นขอใบแจ้งหนี้
    '            Dim parameters As New Dictionary(Of String, Object)
    '            Dim query As String = "UPDATE PN_HEAD SET POST_INVOICE_FLAG = @p0 WHERE TRAN_NO = '" & TRAN_NO & "'"
    '            parameters.Add("@p0", "P")


    Private Sub คำรองออกใบแจงหนToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'Dim f As New frmFINPaymentNoticeInvoiceRequestList
        'If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
        '    Dim TRAN_NO As String = f.DataGridView1.CurrentRow.Cells("TRAN_NO").Value.ToString()
        '    If fIV Is Nothing Then
        '        fIV = New frmFINInvoice
        '        fIV.PN_TRAN_NOLabel.Text = TRAN_NO
        '        fIV.MdiParent = Me
        '        fIV.WindowState = FormWindowState.Maximized
        '        fIV.Show()
        '    Else
        '        'focus opening form
        '        fIV.Show()
        '        fIV.Focus()
        '    End If
        'End If
    End Sub

    Friend Sub tsGS1expand_Click(sender As Object, e As EventArgs) Handles tsGS1expand.Click
        Dim f As New frmGS1approvalStatus
        f.RETIRE_TYPE = "E"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Friend Sub tsGS1resign_Click(sender As Object, e As EventArgs) Handles tsGS1resign.Click
        Dim f As New frmGS1approvalStatus
        f.RETIRE_TYPE = "C"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            'Dim TRAN_NO As String = f.DataGridView1.CurrentRow.Cells("TRAN_NO").Value.ToString()
            'If f.ProcessComboBox.SelectedIndex = 0 Then
            '    'แก้ไข
            '    If fIV Is Nothing Then
            '        fIV = New frmFINInvoice
            '        fIV.TRAN_NOLabel.Text = TRAN_NO
            '        fIV.MdiParent = Me
            '        fIV.WindowState = FormWindowState.Maximized
            '        fIV.Show()
            '    Else
            '        'focus opening form
            '        fIV.Show()
            '        fIV.Focus()
            '    End If
            'ElseIf f.ProcessComboBox.SelectedIndex = 1 Then
            '    'เพิ่มลดหนี้n
            '    Dim fPNC As New frmFINInvoice
            '    fPNC.UserNameLabel.Text = user_firstname & "    " & user_lastname
            '    'fPNC.UserDepartmentLabel.Text = user_department
            '    'fPNC.DocCode.Text = TRAN_NO
            '    If fPNC.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

            '    End If
            '    f.Dispose()
            '    f = Nothing
            'ElseIf f.ProcessComboBox.SelectedIndex = 2 Then
            '    'ออกใบเสร็จ
            '    Dim parameters As New Dictionary(Of String, Object)
            '    Dim query As String = "UPDATE PN_HEAD SET POST_INVOICE_FLAG = @p0 WHERE TRAN_NO = '" & TRAN_NO & "'"
            '    parameters.Add("@p0", "P")

            '    Try
            '        executeWebSQL(query, parameters)
            '    Catch ex As Exception
            '        MessageBox.Show(query & vbCrLf & ex.Message, "ERROR=client.ExecuteNonQuery")
            '    End Try
        End If
    End Sub

    Friend Sub tsGS1blacklist_Click(sender As Object, e As EventArgs) Handles tsGS1blacklist.Click
        Dim f As New frmGS1approvalStatus
        f.RETIRE_TYPE = "D"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Friend Sub tsGS1comeback_Click(sender As Object, e As EventArgs) Handles tsGS1comeback.Click
        Dim f As New frmGS1approvalStatus
        f.RETIRE_TYPE = "S"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub tsGS1reserve_Click(sender As Object, e As EventArgs) Handles tsGS1reserve.Click
        Dim f As New frmGS1reserve
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem18_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem18.Click
        Dim f As New frmGS1smes
        f.MODULE_ID = 2
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub tsMIAN_GROUP_Click(sender As Object, e As EventArgs) Handles tsMIAN_GROUP.Click
        Dim f As New frmMainGroups
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            

        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub กรอกขอมลดวยตนเองToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        Dim f As New frmFINInvoice
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ToolStripMenuItem12_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem12.Click
        If fFeeGS1 Is Nothing Then
            fFeeGS1 = New frmGS1Fees
            fFeeGS1.MdiParent = Me
            fFeeGS1.WindowState = FormWindowState.Maximized
            fFeeGS1.Show()
        Else
            'focus opening form
            fFeeGS1.Show()
            fFeeGS1.Focus()
            fRC.Show()
            fRC.Focus()
        End If
    End Sub


    Private Sub tsFINANCE_Click(sender As Object, e As EventArgs) Handles tsFINANCE.Click
        If fFMain Is Nothing Then
            fFMain = New frmFINMain
            fFMain.MdiParent = Me
            fFMain.WindowState = FormWindowState.Maximized
            fFMain.Show()
        Else
            'focus opening form
            fFMain.Show()
            fFMain.Focus()
        End If
    End Sub

    Private Sub tsSAPar_Click(sender As Object, e As EventArgs) Handles tsSAPar.Click
        '
    End Sub

    Private Sub tsSAPproduct_Click(sender As Object, e As EventArgs) Handles tsSAPproduct.Click
        '
    End Sub

    Private Sub tsSAPreceipt_Click(sender As Object, e As EventArgs) Handles tsSAPreceipt.Click
        Dim f As New frmSAPreceipt
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub tsSAPinvoice_Click(sender As Object, e As EventArgs) Handles tsSAPinvoice.Click
        '
    End Sub

    Private Sub tsSAPB1_Click(sender As Object, e As EventArgs) Handles tsSAPB1.Click
        'Dim reportParam As New Dictionary(Of String, String)
        'reportParam.Add("ReportParameter1", "SOME_STRING")

        'Dim f As New frmMainReports
        'f.reportPath = getParameters(1, "FTI_RPT_5.3.25.01_reportPath")
        'f.reportParameters = reportParam

        'If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
        '    '
        'End If
        'f.Dispose()
        'f = Nothing
    End Sub

    Private Sub DemoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DemoToolStripMenuItem.Click
        If fFMain Is Nothing Then
            fFMain = New frmFINMain
            fFMain.MdiParent = Me
            fFMain.WindowState = FormWindowState.Maximized
            fFMain.Show()
        Else
            'focus opening form
            fFMain.Show()
            fFMain.Focus()
        End If
    End Sub
End Class
