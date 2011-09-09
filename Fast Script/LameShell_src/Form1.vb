

Public Class Form1
    Inherits System.Windows.Forms.Form
    Dim WithEvents _lameShell As New LameShell

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents cmdStart As System.Windows.Forms.Button
    Friend WithEvents lblFeedback As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents pBar As System.Windows.Forms.ProgressBar
    Friend WithEvents lstIgnored As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.cmdStart = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblFeedback = New System.Windows.Forms.Label
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.pBar = New System.Windows.Forms.ProgressBar
        Me.lstIgnored = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cmdStart
        '
        Me.cmdStart.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdStart.Location = New System.Drawing.Point(8, 8)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.TabIndex = 0
        Me.cmdStart.Text = "Start"
        '
        'lblFeedback
        '
        Me.lblFeedback.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFeedback.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.lblFeedback.Location = New System.Drawing.Point(88, 16)
        Me.lblFeedback.Name = "lblFeedback"
        Me.lblFeedback.Size = New System.Drawing.Size(152, 16)
        Me.lblFeedback.TabIndex = 2
        Me.lblFeedback.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdCancel.Location = New System.Drawing.Point(248, 8)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "Cancel"
        '
        'pBar
        '
        Me.pBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pBar.Location = New System.Drawing.Point(8, 40)
        Me.pBar.Name = "pBar"
        Me.pBar.Size = New System.Drawing.Size(312, 16)
        Me.pBar.TabIndex = 4
        '
        'lstIgnored
        '
        Me.lstIgnored.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstIgnored.Location = New System.Drawing.Point(8, 80)
        Me.lstIgnored.Name = "lstIgnored"
        Me.lstIgnored.Size = New System.Drawing.Size(312, 30)
        Me.lstIgnored.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Label1.Location = New System.Drawing.Point(8, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 16)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Ignored Stuff"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(328, 118)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstIgnored)
        Me.Controls.Add(Me.pBar)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.lblFeedback)
        Me.Controls.Add(Me.cmdStart)
        Me.Name = "Form1"
        Me.Text = "LameShell"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'BTW If you call Application.Exit this event would never be raised, just a demo afterall
        'you have to make sure Cancel is called when you are exiting the app - else lame.exe will be running until its done
        _lameShell.Cancel()
    End Sub

    Private Sub _lameShell_Done() Handles _lameShell.Done
        lblFeedback.Text = "Done"
    End Sub

    Private Sub _lameShell_Canceled() Handles _lameShell.Canceled
        lblFeedback.Text = "Canceled/Error"
        pBar.Value = 0
    End Sub

    Private Sub _lameShell_Progress(ByRef Progress As LameProgress) Handles _lameShell.Progress

        If pBar.Maximum <> Progress.FrameMax Then
            pBar.Value = 0
            pBar.Maximum = Progress.FrameMax
        Else
            pBar.Value = Progress.FrameCurrent
        End If
        lblFeedback.Text = Progress.PercentDone & "%" & " ETA:" & Progress.ETA

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        _lameShell.Cancel()
    End Sub

    Private Sub _lameShell_IgnoredLine(ByVal Line As String) Handles _lameShell.IgnoredLine
        lstIgnored.Items.Add(Line)
    End Sub

    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
        ' dont use stdin/stdout "-" as filenames , this is supported by lame.exe but not by the LameShell 
        _lameShell.InFile = Application.StartupPath & "\test.mp3"
        _lameShell.OutFile = Application.StartupPath & "\testOut.mp3"
        _lameShell.Options = "-b 32" '_lameShell options now go ahead and write a nice app wrapping these up :)
        If _lameShell.Start() Then
            lstIgnored.Items.Clear()
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
