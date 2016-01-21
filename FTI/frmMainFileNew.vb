Imports System.Windows.Forms
Imports System.IO

Public Class frmMainFileNew

    Friend dt As DataTable
    Friend MODULE_ID As Integer
    Friend DOC_SIZE_PARAMETER As String
    Friend DOC_FILTER_PARAMETER As String
    Dim docSizeMB As Integer

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If ComboBox1.SelectedIndex >= 0 And TextBox1.TextLength > 0 Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFTIFileNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.DataSource = dt
        ComboBox1.DisplayMember = "DOC_TYPE_NAME"
        ComboBox1.ValueMember = "DOC_TYPE"

        'get parameter
        'docSizeMB = CInt(getParameters(1, "FTI_DOC_SIZE"))
        'OpenPDFFileDialog.Filter = getParameters(1, "FTI_DOC_FILTER")
        docSizeMB = CInt(getParameters(MODULE_ID, DOC_SIZE_PARAMETER))
        OpenPDFFileDialog.Filter = getParameters(MODULE_ID, DOC_FILTER_PARAMETER)

        Dim err As Integer = 0
        Try
            getPermissions(Me.Name, err, Me.Controls)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR=" & err)
        End Try
    End Sub

    Private Sub btBrowse_Click(sender As Object, e As EventArgs) Handles btBrowse.Click
        'browse file
        If OpenPDFFileDialog.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Dim information As FileInfo = My.Computer.FileSystem.GetFileInfo(OpenPDFFileDialog.FileName)
            Dim fileSizeMB As Double = (information.Length) / 1024 / 1024  'Value in MB

            If docSizeMB >= fileSizeMB Then
                'add to textbox
                TextBox1.Text = OpenPDFFileDialog.FileName
                'Dim r As DataRow = ds.Tables("FILES").NewRow
                'r("DOC_TYPE") = DBNull.Value
                'r("docName") = System.IO.Path.GetFileName(OpenFileDialog1.FileName)
                'r("docPath") = OpenFileDialog1.FileName
                'ds.Tables("FILES").Rows.Add(r)
            Else
                MessageBox.Show(Path.GetFileName(OpenPDFFileDialog.FileName) & " ขนาดเกินกำหนด (" & docSizeMB & "MB). กรุณาเลือกไฟล์อื่น.")
            End If
        End If
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Me.BackgroundImage IsNot Nothing Then
            e.Graphics.Clear(Me.BackColor)
            e.Graphics.DrawImage(Me.BackgroundImage, Me.ClientSize.Width - Me.BackgroundImage.Width - 80, Me.ClientSize.Height - Me.BackgroundImage.Height - 40)
        End If
    End Sub
End Class
