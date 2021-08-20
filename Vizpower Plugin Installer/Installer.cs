using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using Vizpower_Plugin_Installer.Properties;

namespace Vizpower_Plugin_Installer
{
    public partial class Installer : Form
    {
        public Installer()
        {
            InitializeComponent();
        }
        /*
        说明：
        更改版本号到Assembly Information改，测试版本更改字符串SpecialVersion，如“ Beta”（B前有空格）
        注意：版本号不要有两位的，必须是一位
        dll在Resources文件夹，替换掉原来的再编译即可
        */
        int ScreenDPI;// = int.Parse(Computer.Registry.GetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop\\WindowMetrics", "AppliedDPI", null));
        int OriginButtonLeft, OriginButtonTop, CurrentVersion; bool SystemDarkMode = false;
        string[] TestLoc; const string TestLocs = "C:\\Program Files (x86)\\wxb\\iMeeting2.exe#C:\\Program Files (x86)\\wxb\\iMeeting.exe#C:\\Program Files\\wxb\\iMeeting2.exe#C:\\Program Files\\wxb\\iMeeting.exe#D:\\Program Files (x86)\\wxb\\iMeeting2.exe#D:\\Program Files (x86)\\wxb\\iMeeting.exe#D:\\Program Files\\wxb\\iMeeting2.exe#D:\\Program Files\\wxb\\iMeeting.exe#E:\\Program Files\\wxb\\iMeeting2.exe#E:\\Program Files\\wxb\\iMeeting.exe#E:\\Program Files (x86)\\wxb\\iMeeting2.exe#E:\\Program Files (x86)\\wxb\\iMeeting.exe#D:\\iMeeting2.exe#D:\\iMeeting.exe#D:\\wxb\\iMeeting2.exe#D:\\wxb\\iMeeting.exe#C:\\wxb\\iMeeting2.exe#C:\\wxb\\iMeeting.exe#F:\\Program Files\\wxb\\iMeeting2.exe#F:\\Program Files\\wxb\\iMeeting.exe#F:\\Program Files (x86)\\wxb\\iMeeting2.exe#F:\\Program Files (x86)\\wxb\\iMeeting.exe";
        string SpecialVersion = ""; // " Beta" or Nothing

        private void Installer_Load(object sender, EventArgs e)
        {
            MaximumSize = Size;
            MinimumSize = Size;
            OriginButtonLeft = ButtonInstall.Left;
            OriginButtonTop = ButtonInstall.Top;
            if (SpecialVersion != "")
                MessageBox.Show("此版本是测试版本！", Text);
            Version AppVer = Assembly.GetExecutingAssembly().GetName().Version;
            Text = "无限宝第三方插件 Ver " + AppVer.Major + "." + AppVer.Minor + "." + AppVer.Build + SpecialVersion + " 安装程序";
            CurrentVersion = int.Parse(AppVer.Major.ToString() + AppVer.Minor.ToString() + AppVer.Build.ToString());

			//如果是XP则跳过联网检测
			if (Strings.Left(Environment.OSVersion.ToString(), 22) == "Microsoft Windows NT 5")
			{
				//MessageBox.Show("", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

            ScreenDPI = int.Parse(Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop\\WindowMetrics", true).GetValue("AppliedDPI").ToString());
			//MessageBox.Show(ScreenDPI.ToString());

            try
            {
                TestLoc = Strings.Split(TestLocs, "#");
                for(int i = 0; i <= TestLoc.ToList().Count -1; i++)
                    if(File.Exists(TestLoc[i]))
                    {
                        TextBoxLocation.Text = TestLoc[i];
						FirstNavi = true;
						break;
                    }
                TextBoxLocation.SelectionStart = 0;
                TextBoxLocation.SelectionLength = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show("运行时出错！\n详细错误信息：\n" + ex.ToString(), Text);
            }

			//Check if installed
			try
			{
				if(FirstNavi == true)
				{
					if (File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "CaptureDesktop.dll").Replace("iMeeting2.exe", "CaptureDesktop.dll")) && File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll")) && File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll")))
						ButtonInstall.Text = "更新(&U)";
				}
			}
			catch { }

            try
            {
				string ForceUpdate, Ver, LinkTo; int VersionNum; string[] Str;
				string WebText = GetWebCode("https://gitee.com/klxn/wxbplugin/raw/master/service.txt");
				Str = Strings.Split(WebText, "<版本>");
				if (Str.ToList().Count < 2) { MessageBox.Show("连接服务器失败！", Text); CheckDarkMode(); return; }
				Ver = Str[1];
				Str = Strings.Split(WebText, "<强制更新>");
				ForceUpdate = Str[1];
				Str = Strings.Split(WebText, "<链接>");
				LinkTo = Str[1];
				try
				{
					string Num = "";
					for (int i = 1; i <= Ver.Length; i++)
						Num += (Strings.AscW(Strings.Mid(Ver, i, 1)) >= 48 && Strings.AscW(Strings.Mid(Ver, i, 1)) <= 57) ? Strings.Mid(Ver, i, 1) : "";
					VersionNum = int.Parse(Num);
				}
				catch
				{
					MessageBox.Show("处理联网信息时发生错误！", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					VersionNum = int.Parse(AppVer.Major.ToString() + AppVer.Minor.ToString() + AppVer.Build.ToString());
				}
				if (VersionNum > CurrentVersion)
					if (ForceUpdate == "0") 
					{
						if (MessageBox.Show("插件已更新，最新版本：" + Ver + "\n是否跳转下载更新？", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == dialogResult.Yes)
							Process.Start(LinkTo);
					}
					else
					{
						MessageBox.Show("插件已更新，最新版本：" + Ver + "\n即将跳转下载更新！", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
						Process.Start(LinkTo);
						Environment.Exit(0);
					}
			}
            catch
            {                
                MessageBox.Show("处理联网信息时发生错误！", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
			CheckDarkMode();
		}

		private void CheckDarkMode()
		{
			try
			{
				string readValue = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", true).GetValue("AppsUseLightTheme").ToString();
				if (readValue == "0" && Strings.Left(Environment.OSVersion.ToString(), 23) == "Microsoft Windows NT 10")
				{
					SystemDarkMode = true;
					BackColor = Color.FromArgb(32, 32, 32);
					Label3.ForeColor = Color.White;
					Label4.ForeColor = Color.LightBlue;
					Label5.ForeColor = Color.LightBlue;
					PictureBox1.BackColor = BackColor;
					CheckBoxAgreement.ForeColor = Color.White;
					TextBoxLocation.BackColor = Color.Black;
					TextBoxLocation.ForeColor = Color.White;
					ButtonInstall.BackColor = Color.FromArgb(43, 43, 43);
					ButtonInstall.ForeColor = Color.White;
					ButtonUnistall.BackColor = Color.FromArgb(43, 43, 43);
					ButtonUnistall.ForeColor = Color.White;
					ButtonNavigate.BackColor = Color.FromArgb(43, 43, 43);
					ButtonNavigate.ForeColor = Color.White;
				}
			}
			catch { }
		}
        private void ButtonInstall_Click(object sender, EventArgs e)
        {
			if (CheckBoxAgreement.Checked == false)
			{
				MessageBox.Show("请勾选已阅读并同意用户协议和免责声明", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			if(TextBoxLocation.Text=="" || TextBoxLocation.Text == " ")
			{
				MessageBox.Show("请手动输入或浏览找到 iMeeting.exe 文件位置", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			MessageBox.Show("如果您已经关闭无限宝和杀毒软件，点击“确定”以继续安装。", Text);
			try
			{
				byte[] b = Resources.CaptureDesktop;
				Stream s = File.Create(TextBoxLocation.Text.Replace("iMeeting.exe", "CaptureDesktop.dll").Replace("iMeeting2.exe", "CaptureDesktop.dll"));
				s.Write(b, 0, b.Length);
				s.Close();
				b = Resources.wxbPluginGUI;
				s = File.Create(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll"));
				s.Write(b, 0, b.Length);
				s.Close();
				b = Resources.wxbHookCore;
				s = File.Create(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbHookCore.dll").Replace("iMeeting2.exe", "wxbHookCore.dll"));
				s.Write(b, 0, b.Length);
				s.Close();
				MessageBox.Show("安装成功！", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch
			{
				MessageBox.Show("安装失败，无限宝是否正在运行？目录是否正确？是否以管理员模式运行了本安装程序？是否已关闭杀毒软件？", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				if (MessageBox.Show("是否查看帮助文件？", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					Process.Start("https://gitee.com/klxn/wxbplugin/raw/master/install.png");
			}
		}

		private void ButtonInstall_MouseMove(object sender, MouseEventArgs e)
		{
			if (CheckBoxAgreement.Checked == false)
			{
				int P = 10 * ScreenDPI / 96;
				int CenterX, CenterY;
				CenterX = ButtonInstall.Width / 2;
				CenterY = ButtonInstall.Height / 2;
				if (e.Location.X >= CenterX)
					ButtonInstall.Left = ButtonInstall.Left + e.Location.X - ButtonInstall.Width - P;
				else
					ButtonInstall.Left = ButtonInstall.Left + e.Location.X + P;
				if (e.Location.Y >= CenterY)
					ButtonInstall.Top = ButtonInstall.Top + e.Location.Y - ButtonInstall.Height - P;
				else
					ButtonInstall.Top = ButtonInstall.Top + e.Location.Y + P;
			}
		}

		private void CheckBoxAgreement_CheckedChanged(object sender, EventArgs e)
		{
			ButtonInstall.Left = OriginButtonLeft;
			ButtonInstall.Top = OriginButtonTop;
		}

		private void ButtonUnistall_Click(object sender, EventArgs e)
		{
			if (TextBoxLocation.Text == "" || TextBoxLocation.Text == " ")
			{
				MessageBox.Show("请手动输入或浏览找到 iMeeting.exe 文件位置", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			if (File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "CaptureDesktop.dll").Replace("iMeeting2.exe", "CaptureDesktop.dll")) || File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll")) || File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll")))
			{
				try
				{
					bool FailedInProcess = false;
					try
					{
						File.Delete(TextBoxLocation.Text.Replace("iMeeting.exe", "CaptureDesktop.dll").Replace("iMeeting2.exe", "CaptureDesktop.dll"));
					}
					catch
					{
						MessageBox.Show("卸载 CaptureDesktop.dll 时发生错误。", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
						FailedInProcess = true;
					}
					try
					{
						File.Delete(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll"));
					}
					catch
					{
						MessageBox.Show("卸载 wxbPluginGUI.dll 时发生错误。", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
						FailedInProcess = true;
					}
					try
					{
						File.Delete(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbHookCore.dll").Replace("iMeeting2.exe", "wxbHookCore.dll"));
					}
					catch
					{
						MessageBox.Show("卸载 wxbHookCore.dll 时发生错误。", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
						FailedInProcess = true;
					}
					if (FailedInProcess == false)
					{
						MessageBox.Show("卸载成功！", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
						ButtonInstall.Text = "安装(&I)";	
					}
				}
				catch
				{
					MessageBox.Show("卸载失败，无限宝是否正在运行？目录是否正确？是否以管理员模式运行了本安装程序？是否已关闭杀毒软件？", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			else
			{
				MessageBox.Show("卸载成功！", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				ButtonInstall.Text = "安装(&I)";
			}
		}
		
		bool FirstNavi = false;
		private void ButtonNavigate_Click(object sender, EventArgs e)
		{
			if (FirstNavi == true)
			{
				FirstNavi = false;
				if (MessageBox.Show("本安装程序已自动为你填写好了路径，取消浏览并安装？", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
				{
					ButtonInstall_Click(sender, e);
					return;
				}
			}
			OpenFileDialog OpenFileDialog = new OpenFileDialog();
			OpenFileDialog.Filter = "无限宝登陆工具 (LoginTool.exe)|*.exe";
			if(OpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				if (Strings.InStr(OpenFileDialog.FileName, "LoginTool.exe") != 0)
				{
					TextBoxLocation.Text = OpenFileDialog.FileName.Replace("LoginTool.exe", "iMeeting.exe");
					try
					{
						if (File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "CaptureDesktop.dll").Replace("iMeeting2.exe", "CaptureDesktop.dll")) && File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll")) && File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll")))
							ButtonInstall.Text = "更新(&U)";
					}
					catch { }
				}
				else
				{
					MessageBox.Show("选择的程序不是无限宝登陆程序！", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
					if (MessageBox.Show("是否查看帮助文件？", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						Process.Start("https://gitee.com/klxn/wxbplugin/raw/master/install.png");
				}
			}
		}

		private void Label4_Click(object sender, EventArgs e)
		{
			Process.Start("https://gitee.com/klxn/wxbplugin/raw/master/install.png");
		}

		private void Label5_Click(object sender, EventArgs e)
		{
			Process.Start("https://yuhuison-1259460701.cos.ap-chengdu.myqcloud.com/mzsm.html");
		}

		private void ButtonInstall_MouseLeave(object sender, EventArgs e)
		{
			if(SystemDarkMode == true)
				ButtonInstall.BackColor = Color.FromArgb(43, 43, 43);
		}

		private void ButtonInstall_MouseEnter(object sender, EventArgs e)
		{
			if (SystemDarkMode == true)
				ButtonInstall.BackColor = Color.FromArgb(70, 70, 70);
		}

		private void ButtonUnistall_MouseLeave(object sender, EventArgs e)
		{
			if (SystemDarkMode == true)
				ButtonUnistall.BackColor = Color.FromArgb(43, 43, 43);
		}

		private void ButtonUnistall_MouseEnter(object sender, EventArgs e)
		{
			if (SystemDarkMode == true)
				ButtonUnistall.BackColor = Color.FromArgb(70, 70, 70);
		}

		private void ButtonNavigate_MouseLeave(object sender, EventArgs e)
		{
			if (SystemDarkMode == true)
				ButtonNavigate.BackColor = Color.FromArgb(43, 43, 43);
		}

		private void ButtonNavigate_MouseEnter(object sender, EventArgs e)
		{
			if (SystemDarkMode == true)
				ButtonNavigate.BackColor = Color.FromArgb(70, 70, 70);
		}

		public string GetWebCode(string strURL)
		{
			Uri arg_15_0 = new Uri(strURL);
			byte[] i = new byte[1];
			Queue<byte> dataQue = new Queue<byte>();
			HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(arg_15_0);
			DateTime sTime = Conversions.ToDate("1990-09-21 00:00:00");
			httpReq.IfModifiedSince = sTime;
			httpReq.Method = "GET";
			httpReq.Timeout = 7000;
			HttpWebResponse httpResp;
			try
			{
				httpResp = (HttpWebResponse)httpReq.GetResponse();
			}
			catch (Exception arg_58_0)
			{
				ProjectData.SetProjectError(arg_58_0);
				string GetWebCode = "<title>no thing found</title>";
				ProjectData.ClearProjectError();
				return GetWebCode;
			}
			Stream ioS = httpResp.GetResponseStream();
			checked
			{
				while (ioS.CanRead)
				{
					try
					{
						dataQue.Enqueue((byte)ioS.ReadByte());
					}
					catch (Exception arg_87_0)
					{
						ProjectData.SetProjectError(arg_87_0);
						ProjectData.ClearProjectError();
						break;
					}
				}
				i = new byte[dataQue.Count - 1 + 1];
				int num = dataQue.Count - 1;
				for (int j = 0; j <= num; j++)
				{
					i[j] = dataQue.Dequeue();
				}
				string tCode = Encoding.GetEncoding("UTF-8").GetString(i);
				string charSet = Strings.Replace(Conversions.ToString(this.GetByDiv2(tCode, "charset=", "\"")), "\"", "", 1, -1, CompareMethod.Binary);
				if (Operators.CompareString(charSet, "", false) == 0)
				{
					if (Operators.CompareString(httpResp.CharacterSet, "", false) == 0)
					{
						tCode = Encoding.GetEncoding("UTF-8").GetString(i);
					}
					else
					{
						tCode = Encoding.GetEncoding(httpResp.CharacterSet).GetString(i);
					}
				}
				else
				{
					tCode = Encoding.GetEncoding(charSet).GetString(i);
				}
				string GetWebCode = tCode;
				if (Operators.CompareString(tCode, "", false) == 0)
				{
					GetWebCode = "<title>no thing found</title>";
				}
				return GetWebCode;
			}
		}

		public object GetByDiv2(string code, string divBegin, string divEnd)
		{
			int lens = Strings.Len(divBegin);
			checked
			{
				object GetByDiv2;
				if (Strings.InStr(1, code, divBegin, CompareMethod.Binary) == 0)
				{
					GetByDiv2 = "";
				}
				else
				{
					int lgStart = Strings.InStr(1, code, divBegin, CompareMethod.Binary) + lens;
					int lgEnd = Strings.InStr(lgStart + 1, code, divEnd, CompareMethod.Binary);
					if (lgEnd == 0)
					{
						GetByDiv2 = "";
					}
					else
					{
						GetByDiv2 = Strings.Mid(code, lgStart, lgEnd - lgStart);
					}
				}
				return GetByDiv2;
			}
		}
	}
}
