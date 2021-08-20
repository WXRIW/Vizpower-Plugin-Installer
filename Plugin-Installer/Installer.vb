Imports System.IO
Imports System.Net
Imports System.Text

Public Class Installer
    '说明
    '更改版本号到Assembly Information改，测试版本更改字符串SpecialVersion，如“ Beta”（B前有空格）
    '注意：版本号不要有两位的，必须是一位
    'dll在Resources文件夹，替换掉原来的再编译即可

    Dim ScreenDPI As Integer = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "AppliedDPI", Nothing)
    Dim OriginButtonLeft, OriginButtonTop, CurrentVersion As Integer, SpecialVersion = "" ' " Beta" or Nothing
    Private Sub Installer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ButtonInstall.Enabled = True
        If SpecialVersion <> "" Then MsgBox("此版本是测试版本！", 0, Text)
        Try
            TestLoc = Split(TestLocs, "#")
            For i = 0 To TestLoc.ToList.Count - 1
                If File.Exists(TestLoc(i)) Then TextBoxLocation.Text = TestLoc(i) : FirstNavi = True
            Next
            TextBoxLocation.SelectionStart = 0
            TextBoxLocation.SelectionLength = 0
        Catch : End Try
        MaximumSize = Size
        MinimumSize = Size
        OriginButtonLeft = ButtonInstall.Left
        OriginButtonTop = ButtonInstall.Top
        Text = "无限宝第三方插件 Ver " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & SpecialVersion & " 安装程序"
        CurrentVersion = My.Application.Info.Version.Major & My.Application.Info.Version.Minor & My.Application.Info.Version.Build
        '法一：
        'WebBrowserGetCode.Navigate("https://gitee.com/klxn/wxbplugin/raw/master/service.txt")
        '法二：（XP上无法使用，待4.0时更换此方法）
        'GetWebCode("https://gitee.com/klxn/wxbplugin/raw/master/service.txt")

        '如果是XP则跳过联网检测
        If Strings.Left(Environment.OSVersion.ToString, 22) = "Microsoft Windows NT 5" Then Exit Sub

        Try
            Dim ForceUpdate As String, VersionNum As Integer, Ver As String, Str() As String, LinkTo As String
            Dim WebText As String = GetWebCode("https://gitee.com/klxn/wxbplugin/raw/master/service.txt") '= WebBrowserGetCode.DocumentText
            Str = Split(WebText, "<版本>")
            If Str.ToList.Count < 2 Then MsgBox("连接服务器失败！", 0, Text) : Exit Sub
            Ver = Str(1)
            Str = Split(WebText, "<强制更新>")
            ForceUpdate = Str(1)
            Str = Split(WebText, "<链接>")
            LinkTo = Str(1)
            Try
                Dim Num As String = ""
                For i = 1 To Ver.Length
                    Num &= If(AscW(Mid(Ver, i, 1)) >= 48 And AscW(Mid(Ver, i, 1)) <= 57, Mid(Ver, i, 1), "")
                Next
                VersionNum = Int(Num)
                'MsgBox(VersionNum)
            Catch
                MsgBox("处理联网信息时发生错误！", MsgBoxStyle.Critical, Text)
                VersionNum = My.Application.Info.Version.Major & My.Application.Info.Version.Minor & My.Application.Info.Version.Build
            End Try
            If VersionNum > CurrentVersion Then
                'If Ver <> My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build Then
                If ForceUpdate = "0" Then
                    If MsgBox("插件已更新，最新版本：" & Ver & vbCrLf & "是否跳转下载更新？", MsgBoxStyle.Information + MsgBoxStyle.YesNo, Text) = MsgBoxResult.Yes Then
                        Process.Start(LinkTo)
                    End If
                Else
                    MsgBox("插件已更新，最新版本：" & Ver & vbCrLf & "即将跳转下载更新！", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Text)
                    Process.Start(LinkTo)
                    End
                End If
            End If
            'ButtonInstall.Text = "安装(&I)"
            'ButtonInstall.Enabled = True
        Catch ex As Exception
            MsgBox("处理联网信息时发生错误！", MsgBoxStyle.Critical, Text)
        End Try
        'DarkMode
        Try
            Dim readValue = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", Nothing)
            If readValue = 0 And Strings.Left(Environment.OSVersion.ToString, 23) = "Microsoft Windows NT 10" Then
                SystemDarkMode = True
                BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
                Label3.ForeColor = Color.White
                Label4.ForeColor = Color.LightBlue
                Label5.ForeColor = Color.LightBlue
                PictureBox1.BackColor = BackColor
                CheckBoxAgreement.ForeColor = Color.White
                TextBoxLocation.BackColor = Color.Black
                TextBoxLocation.ForeColor = Color.White
                ButtonInstall.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
                ButtonInstall.ForeColor = Color.White
                ButtonUnistall.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
                ButtonUnistall.ForeColor = Color.White
                ButtonNavigate.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
                ButtonNavigate.ForeColor = Color.White
            End If
        Catch ex As Exception : End Try
    End Sub
    Dim SystemDarkMode = False

    Private Sub ButtonInstall_MouseMove(sender As Object, e As MouseEventArgs) Handles ButtonInstall.MouseMove
        If CheckBoxAgreement.Checked = False Then
            Dim P As Integer = 10 * ScreenDPI / 96
            Dim CenterX, CenterY As Integer
            CenterX = ButtonInstall.Width / 2
            CenterY = ButtonInstall.Height / 2
            If e.Location.X >= CenterX Then
                ButtonInstall.Left = ButtonInstall.Left + e.Location.X - ButtonInstall.Width - P
            Else
                ButtonInstall.Left = ButtonInstall.Left + e.Location.X + P
            End If
            If e.Location.Y >= CenterY Then
                ButtonInstall.Top = ButtonInstall.Top + e.Location.Y - ButtonInstall.Height - P
            Else
                ButtonInstall.Top = ButtonInstall.Top + e.Location.Y + P
            End If
        End If
    End Sub

    Private Sub CheckBoxAgreement_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxAgreement.CheckedChanged
        ButtonInstall.Left = OriginButtonLeft
        ButtonInstall.Top = OriginButtonTop
    End Sub

    Private Sub ButtonInstall_Click(sender As Object, e As EventArgs) Handles ButtonInstall.Click
        If CheckBoxAgreement.Checked = False Then
            MsgBox("请勾选已阅读并同意用户协议和免责声明", MsgBoxStyle.Information, Text)
            Exit Sub
        End If
        If TextBoxLocation.Text = "" Or TextBoxLocation.Text = " " Then
            MsgBox("请手动输入或浏览找到iMeeting.exe文件位置", MsgBoxStyle.Information, Text)
            Exit Sub
        End If
        MsgBox("如果您已经关闭杀毒软件，点击‘确定’以继续安装。", 0, Text)
        Try
            Dim b() As Byte = My.Resources.CaptureDesktop
            Dim s As IO.Stream = File.Create(TextBoxLocation.Text.Replace("iMeeting.exe", "CaptureDesktop.dll").Replace("iMeeting2.exe", "CaptureDesktop.dll"))
            s.Write(b, 0, b.Length)
            s.Close()
            b = My.Resources.wxbPluginGUI
            s = File.Create(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll"))
            s.Write(b, 0, b.Length)
            s.Close()
            b = My.Resources.wxbHookCore
            s = File.Create(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbHookCore.dll").Replace("iMeeting2.exe", "wxbHookCore.dll"))
            s.Write(b, 0, b.Length)
            s.Close()
            MsgBox("安装成功！", MsgBoxStyle.Information, Text)
        Catch
            MsgBox("安装失败，无限宝是否正在运行？目录是否正确？是否以管理员模式运行了本安装程序？是否已关闭杀毒软件？", MsgBoxStyle.Critical, Text)
            If MsgBox("是否查看帮助文件？", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Text) = MsgBoxResult.Yes Then
                Process.Start("https://gitee.com/klxn/wxbplugin/raw/master/install.png")
            End If
        End Try
    End Sub

    Private Sub ButtonUnistall_Click(sender As Object, e As EventArgs) Handles ButtonUnistall.Click
        If File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "CaptureDesktop.dll").Replace("iMeeting2.exe", "CaptureDesktop.dll")) Or File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll")) Or File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbHookCore.dll").Replace("iMeeting2.exe", "wxbHookCore.dll")) Then
            Try
                File.Delete(TextBoxLocation.Text.Replace("iMeeting.exe", "CaptureDesktop.dll").Replace("iMeeting2.exe", "CaptureDesktop.dll"))
                File.Delete(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll"))
                File.Delete(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbHookCore.dll").Replace("iMeeting2.exe", "wxbHookCore.dll"))
                MsgBox("卸载成功！", MsgBoxStyle.Information, Text)
            Catch
                MsgBox("卸载失败，无限宝是否正在运行？目录是否正确？是否以管理员模式运行了本安装程序？是否已关闭杀毒软件？", MsgBoxStyle.Information, Text)
            End Try
        Else
            MsgBox("卸载成功！", MsgBoxStyle.Information, Text)
        End If
    End Sub
    Dim FirstNavi As Boolean = False
    Private Sub ButtonNavigate_Click(sender As Object, e As EventArgs) Handles ButtonNavigate.Click
        If FirstNavi Then
            FirstNavi = False
            If MsgBox("本安装程序已自动为你填写好了路径，取消浏览并安装？", MsgBoxStyle.Information & MsgBoxStyle.YesNo, Text) = MsgBoxResult.Yes Then
                ButtonInstall_Click(sender, e)
                Exit Sub
            End If
        End If
        Dim OpenFileDialog As OpenFileDialog = New OpenFileDialog
        'SaveFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        'OpenFileDialog.RestoreDirectory = True
        'Dim _Time As Date = FileIO.FileSystem.GetFileInfo(filePath).LastWriteTime
        OpenFileDialog.Filter = "无限宝登陆工具 (LoginTool.exe)|*.exe"
        If OpenFileDialog.ShowDialog = DialogResult.OK Then
            If InStr(OpenFileDialog.FileName, "LoginTool.exe") <> 0 Then
                TextBoxLocation.Text = OpenFileDialog.FileName.Replace("LoginTool.exe", "iMeeting.exe")
            Else
                MsgBox("选择的程序不是无限宝登陆程序！", MsgBoxStyle.Information, Text)
                If MsgBox("是否查看帮助文件？", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Text) = MsgBoxResult.Yes Then
                    Process.Start("https://gitee.com/klxn/wxbplugin/raw/master/install.png")
                End If
            End If
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Process.Start("https://gitee.com/klxn/wxbplugin/raw/master/install.png")
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Process.Start("https://yuhuison-1259460701.cos.ap-chengdu.myqcloud.com/mzsm.html")
    End Sub

    Function GetWebCode(ByVal strURL As String) As String
        Dim httpReq As System.Net.HttpWebRequest
        Dim httpResp As System.Net.HttpWebResponse
        Dim httpURL As New System.Uri(strURL)
        Dim ioS As System.IO.Stream, charSet As String, tCode As String
        Dim k() As Byte
        ReDim k(0)
        Dim dataQue As New Queue(Of Byte)
        httpReq = CType(WebRequest.Create(httpURL), HttpWebRequest)
        Dim sTime As Date = CDate("1990-09-21 00:00:00")
        httpReq.IfModifiedSince = sTime
        httpReq.Method = "GET"
        httpReq.Timeout = 7000

        Try
            httpResp = CType(httpReq.GetResponse(), HttpWebResponse)
        Catch
            Debug.Print("weberror")
            GetWebCode = "<title>no thing found</title>" : Exit Function
        End Try
        '以上为网络数据获取
        ioS = CType(httpResp.GetResponseStream, Stream)
        Do While ioS.CanRead = True
            Try
                dataQue.Enqueue(ioS.ReadByte)
            Catch
                Debug.Print("read error")
                Exit Do
            End Try
        Loop
        ReDim k(dataQue.Count - 1)
        For j As Integer = 0 To dataQue.Count - 1
            k(j) = dataQue.Dequeue
        Next
        '以上，为获取流中的的二进制数据
        tCode = Encoding.GetEncoding("UTF-8").GetString(k) '获取特定编码下的情况，毕竟UTF-8支持英文正常的显示
        charSet = Replace(GetByDiv2(tCode, "charset=", """"), """", "") '进行编码类型识别
        '以上，获取编码类型
        If charSet = "" Then 'defalt
            If httpResp.CharacterSet = "" Then
                tCode = Encoding.GetEncoding("UTF-8").GetString(k)
            Else
                tCode = Encoding.GetEncoding(httpResp.CharacterSet).GetString(k)
            End If
        Else
            tCode = Encoding.GetEncoding(charSet).GetString(k)
        End If
        Debug.Print(charSet)
        'Stop
        '以上，按照获得的编码类型进行数据转换
        '将得到的内容进行最后处理，比如判断是不是有出现字符串为空的情况
        GetWebCode = tCode
        If tCode = "" Then GetWebCode = "<title>no thing found</title>"
    End Function

    Function GetByDiv2(ByVal code As String, ByVal divBegin As String, ByVal divEnd As String)  '获取分隔符所夹的内容[完成，未测试]
        '仅用于获取编码数据
        Dim lgStart As Integer
        Dim lens As Integer
        Dim lgEnd As Integer
        lens = Len(divBegin)
        If InStr(1, code, divBegin) = 0 Then GetByDiv2 = "" : Exit Function
        lgStart = InStr(1, code, divBegin) + CInt(lens)

        lgEnd = InStr(lgStart + 1, code, divEnd)
        If lgEnd = 0 Then GetByDiv2 = "" : Exit Function
        GetByDiv2 = Mid(code, lgStart, lgEnd - lgStart)
    End Function
    Dim TestLoc() As String, TestLocs As String = "C:\Program Files (x86)\wxb\iMeeting2.exe#C:\Program Files (x86)\wxb\iMeeting.exe#C:\Program Files\wxb\iMeeting2.exe#C:\Program Files\wxb\iMeeting.exe#D:\Program Files (x86)\wxb\iMeeting2.exe#D:\Program Files (x86)\wxb\iMeeting.exe#D:\Program Files\wxb\iMeeting2.exe#D:\Program Files\wxb\iMeeting.exe#E:\Program Files\wxb\iMeeting2.exe#E:\Program Files\wxb\iMeeting.exe#E:\Program Files (x86)\wxb\iMeeting2.exe#E:\Program Files (x86)\wxb\iMeeting.exe#D:\iMeeting2.exe#D:\iMeeting.exe#D:\wxb\iMeeting2.exe#D:\wxb\iMeeting.exe#C:\wxb\iMeeting2.exe#C:\wxb\iMeeting.exe#F:\Program Files\wxb\iMeeting2.exe#F:\Program Files\wxb\iMeeting.exe#F:\Program Files (x86)\wxb\iMeeting2.exe#F:\Program Files (x86)\wxb\iMeeting.exe"
    Private Sub ButtonInstall_MouseEnter(sender As Object, e As EventArgs) Handles ButtonInstall.MouseEnter
        If SystemDarkMode = True Then ButtonInstall.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer))
    End Sub
    Private Sub ButtonInstall_MouseLeave(sender As Object, e As EventArgs) Handles ButtonInstall.MouseLeave
        If SystemDarkMode = True Then ButtonInstall.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
    End Sub
    Private Sub ButtonUnistall_MouseEnter(sender As Object, e As EventArgs) Handles ButtonUnistall.MouseEnter
        If SystemDarkMode = True Then ButtonUnistall.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer))
    End Sub
    Private Sub ButtonUnistall_MouseLeave(sender As Object, e As EventArgs) Handles ButtonUnistall.MouseLeave
        If SystemDarkMode = True Then ButtonUnistall.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
    End Sub
    Private Sub ButtonNavigate_MouseEnter(sender As Object, e As EventArgs) Handles ButtonNavigate.MouseEnter
        If SystemDarkMode = True Then ButtonNavigate.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer))
    End Sub
    Private Sub ButtonNavigate_MouseLeave(sender As Object, e As EventArgs) Handles ButtonNavigate.MouseLeave
        If SystemDarkMode = True Then ButtonNavigate.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(43, Byte), Integer))
    End Sub
End Class