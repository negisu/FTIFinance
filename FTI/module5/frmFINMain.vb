﻿Public Class frmFINMain

    Private Sub ReportByProductToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportByProductToolStripMenuItem.Click
        Dim f As New frmFINReportProduct

        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        End If

        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ReportByARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportByARToolStripMenuItem.Click
        Dim f As New frmFINReportAR

        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub AddPNToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddPNToolStripMenuItem.Click
        If fPN Is Nothing Then
            fPN = New frmFINPaymentNotice
            fPN.TRAN_TYPE = "P1"
            fPN.MdiParent = Me.MdiParent
            fPN.WindowState = FormWindowState.Maximized
            fPN.Show()
        Else
            'focus opening form
            fPN.Show()
            fPN.Focus()
        End If
    End Sub

    Private Sub AddIVToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AddIVToolStripMenuItem1.Click
        If fPN Is Nothing Then
            fPN = New frmFINPaymentNotice
            fPN.TRAN_TYPE = "I1"
            fPN.MdiParent = Me.MdiParent
            fPN.WindowState = FormWindowState.Maximized
            fPN.Show()
        Else
            'focus opening form
            fPN.Show()
            fPN.Focus()
        End If
    End Sub

    Private Sub PNRequestInvoiceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PNRequestInvoiceToolStripMenuItem.Click
        Dim f As New frmFINPaymentNoticeInvoicePassRequestApprovalList
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        End If

        f.Dispose()
        f = Nothing
    End Sub

    Private Sub PNRequestCancelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PNRequestCancelToolStripMenuItem.Click
        Dim f As New frmFINPaymentNoticeCancelRequestApprovalList
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        End If

        f.Dispose()
        f = Nothing
    End Sub

    Private Sub frmFINMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fFMain.Dispose()
        fFMain = Nothing
    End Sub

    Private Sub รายงานตามฝายToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles รายงานตามฝายToolStripMenuItem.Click
        Dim param As New Dictionary(Of String, String)
        param.Add("DIV_CODE", user_div)

        Dim f As New frmMainReports
        f.reportPath = getParameters(5, "DIV_REMAIN_REPORT")
        f.reportParameters = param
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub


    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        Dim param As New Dictionary(Of String, String)
        param.Add("DIV_CODE", user_div)

        Dim f As New frmMainReports
        f.reportPath = getParameters(5, "5.5.16.1")
        f.reportParameters = param
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub รายงานรายละเอยดเอกสารคางชำระToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles รายงานรายละเอยดเอกสารคางชำระToolStripMenuItem.Click
        Dim url As String = "http://ftimember.off.fti.or.th/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/FTI%20Reports/5-FIN/5.5.16.2.rdl&Source=http%3A%2F%2Fftimember%2Eoff%2Efti%2Eor%2Eth%2FFTI%2520Reports%2FForms%2FAllItems%2Easpx%3FRootFolder%3D%252FFTI%2520Reports%252F5%252DFIN%26FolderCTID%3D0x012000682451D666938E44939EB360D21DFDBB%26View%3D%257B34B74CC8%252DF74D%252D42D1%252D9F79%252D34E50ECFC67D%257D"
        Process.Start(url)
    End Sub

    Private Sub EditPaymentNoticeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditPNToolStripMenuItem.Click

        Dim f As New frmFINEditList
        f.TRAN_TYPE = "P1"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Dim TRAN_NO As String = f.DataGridView1.CurrentRow.Cells("TRAN_NO").Value.ToString()
            If fPN Is Nothing Then
                fPN = New frmFINPaymentNotice
                fPN.TRAN_TYPE = "P1"
                fPN.TRAN_NOLabel.Text = TRAN_NO
                fPN.MdiParent = Me.MdiParent
                fPN.WindowState = FormWindowState.Maximized
                fPN.Show()
            Else
                If (MessageBox.Show("มีหน้าต่างใบแจ้งชำระเปิดค้างไว้ คุณต้องการที่จะปิดและแก้ไขใบแจ้งชำระที่เลือกใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                    fPN.Dispose()
                    fPN = Nothing
                    fPN = New frmFINPaymentNotice
                    fPN.TRAN_TYPE = "P1"
                    fPN.TRAN_NOLabel.Text = TRAN_NO
                    fPN.MdiParent = Me.MdiParent
                    fPN.WindowState = FormWindowState.Maximized
                    fPN.Show()
                Else
                    'focus opening form
                    fPN.Show()
                    fPN.Focus()
                End If

            End If
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub CancelRequestPNToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelRequestPNToolStripMenuItem.Click
        Dim f As New frmFINPaymentNoticeCancelRequestList
        f.TRAN_TYPE = "P1"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub frmFINMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If PermissionHelper.isAdmin() Then
        Else
            PNRequestCancelToolStripMenuItem.Visible = False
            PNRequestInvoiceToolStripMenuItem.Visible = False
        End If
    End Sub

    Private Sub InvoicePassToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InvoicePassToolStripMenuItem.Click
        Dim f As New frmFINPaymentNoticeInvoicePassRequestList
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub EditIVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditIVToolStripMenuItem.Click
        Dim f As New frmFINEditList
        f.TRAN_TYPE = "I1"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Dim TRAN_NO As String = f.DataGridView1.CurrentRow.Cells("TRAN_NO").Value.ToString()
            If fPN Is Nothing Then
                fPN = New frmFINPaymentNotice
                fPN.TRAN_TYPE = "I1"
                fPN.TRAN_NOLabel.Text = TRAN_NO
                fPN.MdiParent = Me.MdiParent
                fPN.WindowState = FormWindowState.Maximized
                fPN.Show()
            Else
                If (MessageBox.Show("มีหน้าต่างใบแจ้งชำระเปิดค้างไว้ คุณต้องการที่จะปิดและแก้ไขใบแจ้งชำระที่เลือกใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                    fPN.Dispose()
                    fPN = Nothing
                    fPN = New frmFINPaymentNotice
                    fPN.TRAN_TYPE = "I1"
                    fPN.TRAN_NOLabel.Text = TRAN_NO
                    fPN.MdiParent = Me.MdiParent
                    fPN.WindowState = FormWindowState.Maximized
                    fPN.Show()
                Else
                    'focus opening form
                    fPN.Show()
                    fPN.Focus()
                End If

            End If
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub CancelRequestIVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelRequestIVToolStripMenuItem.Click
        Dim f As New frmFINPaymentNoticeCancelRequestList
        f.TRAN_TYPE = "I1"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub ลดหนToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ลดหนToolStripMenuItem.Click
        Dim f As New frmFINEditList
        f.TRAN_TYPE = "I2"
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Dim TRAN_NO As String = f.DataGridView1.CurrentRow.Cells("TRAN_NO").Value.ToString()
            If fPN Is Nothing Then
                fPN = New frmFINPaymentNotice
                fPN.TRAN_TYPE = "I2"
                fPN.TRAN_NO_REFLabel.Text = TRAN_NO
                fPN.MdiParent = Me.MdiParent
                fPN.WindowState = FormWindowState.Maximized
                fPN.Show()
            Else
                If (MessageBox.Show("มีหน้าต่างใบแจ้งชำระเปิดค้างไว้ คุณต้องการที่จะปิดและแก้ไขใบแจ้งชำระที่เลือกใช่หรือไม่?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                    fPN.Dispose()
                    fPN = Nothing
                    fPN = New frmFINPaymentNotice
                    fPN.TRAN_TYPE = "I2"
                    fPN.TRAN_NO_REFLabel.Text = TRAN_NO
                    fPN.MdiParent = Me.MdiParent
                    fPN.WindowState = FormWindowState.Maximized
                    fPN.Show()
                Else
                    'focus opening form
                    fPN.Show()
                    fPN.Focus()
                End If

            End If
        End If
        f.Dispose()
        f = Nothing
    End Sub

    Private Sub คำรองออกใบแจงหนจากแผนกตางๆToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles คำรองออกใบแจงหนจากแผนกตางๆToolStripMenuItem.Click
        Dim f As New frmFINPaymentNoticeInvoicePassRequestApprovalListAccount
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        End If

        f.Dispose()
        f = Nothing
    End Sub

    Private Sub IVSUBSECTIONMANAGEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IVSUBSECTIONMANAGEToolStripMenuItem.Click
        Dim f As New frmFINSubSectionManage
        If f.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        End If

        f.Dispose()
        f = Nothing
    End Sub
End Class