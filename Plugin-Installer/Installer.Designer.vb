<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Installer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Installer))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxLocation = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CheckBoxAgreement = New System.Windows.Forms.CheckBox()
        Me.ButtonNavigate = New System.Windows.Forms.Button()
        Me.ButtonInstall = New System.Windows.Forms.Button()
        Me.ButtonUnistall = New System.Windows.Forms.Button()
        Me.WebBrowserGetCode = New System.Windows.Forms.WebBrowser()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(316, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "为了阻挡无限宝的打击，本程序诞生了！"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(12, 40)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(452, 46)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "本程序释放的文件会引起部分杀毒软件的误报，如果出现，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "请更换杀毒软件（建议使用火绒）"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 98)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(229, 23)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "无限宝 iMeeting.exe 位置："
        '
        'TextBoxLocation
        '
        Me.TextBoxLocation.Location = New System.Drawing.Point(18, 132)
        Me.TextBoxLocation.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxLocation.Name = "TextBoxLocation"
        Me.TextBoxLocation.Size = New System.Drawing.Size(468, 29)
        Me.TextBoxLocation.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label4.Font = New System.Drawing.Font("Microsoft YaHei", 9.0!)
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(12, 175)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(189, 20)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "使用说明（点我！点我！）"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label5.Font = New System.Drawing.Font("Microsoft YaHei", 9.0!)
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(342, 175)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(144, 20)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "用户协议和免责声明"
        '
        'CheckBoxAgreement
        '
        Me.CheckBoxAgreement.AutoSize = True
        Me.CheckBoxAgreement.Font = New System.Drawing.Font("Microsoft YaHei", 9.0!)
        Me.CheckBoxAgreement.Location = New System.Drawing.Point(230, 174)
        Me.CheckBoxAgreement.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxAgreement.Name = "CheckBoxAgreement"
        Me.CheckBoxAgreement.Size = New System.Drawing.Size(121, 24)
        Me.CheckBoxAgreement.TabIndex = 0
        Me.CheckBoxAgreement.Text = "已阅读并同意"
        Me.CheckBoxAgreement.UseVisualStyleBackColor = True
        '
        'ButtonNavigate
        '
        Me.ButtonNavigate.Location = New System.Drawing.Point(387, 91)
        Me.ButtonNavigate.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonNavigate.Name = "ButtonNavigate"
        Me.ButtonNavigate.Size = New System.Drawing.Size(100, 37)
        Me.ButtonNavigate.TabIndex = 9
        Me.ButtonNavigate.Text = "浏览(&N)"
        Me.ButtonNavigate.UseVisualStyleBackColor = True
        '
        'ButtonInstall
        '
        Me.ButtonInstall.Location = New System.Drawing.Point(78, 208)
        Me.ButtonInstall.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonInstall.Name = "ButtonInstall"
        Me.ButtonInstall.Size = New System.Drawing.Size(150, 40)
        Me.ButtonInstall.TabIndex = 5
        Me.ButtonInstall.Text = "安装(&I)"
        Me.ButtonInstall.UseVisualStyleBackColor = True
        '
        'ButtonUnistall
        '
        Me.ButtonUnistall.Location = New System.Drawing.Point(268, 208)
        Me.ButtonUnistall.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonUnistall.Name = "ButtonUnistall"
        Me.ButtonUnistall.Size = New System.Drawing.Size(150, 40)
        Me.ButtonUnistall.TabIndex = 6
        Me.ButtonUnistall.Text = "卸载(&U)"
        Me.ButtonUnistall.UseVisualStyleBackColor = True
        '
        'WebBrowserGetCode
        '
        Me.WebBrowserGetCode.Location = New System.Drawing.Point(12, 312)
        Me.WebBrowserGetCode.Margin = New System.Windows.Forms.Padding(2)
        Me.WebBrowserGetCode.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowserGetCode.Name = "WebBrowserGetCode"
        Me.WebBrowserGetCode.Size = New System.Drawing.Size(20, 20)
        Me.WebBrowserGetCode.TabIndex = 10
        Me.WebBrowserGetCode.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(-3, 224)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(41, 35)
        Me.PictureBox1.TabIndex = 11
        Me.PictureBox1.TabStop = False
        '
        'Installer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(499, 258)
        Me.Controls.Add(Me.ButtonInstall)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.WebBrowserGetCode)
        Me.Controls.Add(Me.ButtonUnistall)
        Me.Controls.Add(Me.ButtonNavigate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CheckBoxAgreement)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxLocation)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Microsoft YaHei", 10.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "Installer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "无限宝第三方插件安装程序"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxLocation As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents CheckBoxAgreement As CheckBox
    Friend WithEvents ButtonNavigate As Button
    Friend WithEvents ButtonInstall As Button
    Friend WithEvents ButtonUnistall As Button
    Friend WithEvents WebBrowserGetCode As WebBrowser
    Friend WithEvents PictureBox1 As PictureBox
End Class
