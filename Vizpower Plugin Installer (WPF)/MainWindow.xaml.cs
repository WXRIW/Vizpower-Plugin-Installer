using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using MessageBox = System.Windows.Forms.MessageBox;
using DialogResult = System.Windows.Forms.DialogResult;
using System.Threading;
using System.Windows.Threading;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using Microsoft.Win32;

namespace Vizpower_Plugin_Installer__WPF_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
		/*
        说明：
        更改版本号到Assembly Information改，测试版本更改字符串SpecialVersion，如“ Beta”（B前有空格）
        注意：版本号不要有两位的，必须是一位
        dll在Resources文件夹，替换掉原来的再编译即可
        */
		string SpecialVersion = ""; // " Beta" or Nothing

		//int ScreenDPI;// = int.Parse(Computer.Registry.GetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop\\WindowMetrics", "AppliedDPI", null));
		int CurrentVersion; bool SystemDarkMode = false; bool FirstNavi = false; Thickness OriginBtnInstallMargin; System.Windows.Controls.Button OriginButtonInstall; double OriginBtnWidth;
		string[] TestLoc; const string TestLocs = "C:\\iMeeting.exe#C:\\Program Files (x86)\\wxb\\iMeeting2.exe#C:\\Program Files (x86)\\wxb\\iMeeting.exe#C:\\Program Files\\wxb\\iMeeting2.exe#C:\\Program Files\\wxb\\iMeeting.exe#D:\\Program Files (x86)\\wxb\\iMeeting2.exe#D:\\Program Files (x86)\\wxb\\iMeeting.exe#D:\\Program Files\\wxb\\iMeeting2.exe#D:\\Program Files\\wxb\\iMeeting.exe#E:\\Program Files\\wxb\\iMeeting2.exe#E:\\Program Files\\wxb\\iMeeting.exe#E:\\Program Files (x86)\\wxb\\iMeeting2.exe#E:\\Program Files (x86)\\wxb\\iMeeting.exe#D:\\iMeeting2.exe#D:\\iMeeting.exe#D:\\wxb\\iMeeting2.exe#D:\\wxb\\iMeeting.exe#C:\\wxb\\iMeeting2.exe#C:\\wxb\\iMeeting.exe#F:\\Program Files\\wxb\\iMeeting2.exe#F:\\Program Files\\wxb\\iMeeting.exe#F:\\Program Files (x86)\\wxb\\iMeeting2.exe#F:\\Program Files (x86)\\wxb\\iMeeting.exe";
		Thread UpdateThread = null; Version AppVer = Assembly.GetExecutingAssembly().GetName().Version;
		DispatcherTimer CheckIfOver = new DispatcherTimer(); bool OnlineCheckOver = false;
		//These are for the checker thread.
		string Window_Title, ButtonInstall_Content, TextBoxLocation_Text;

		private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (SpecialVersion != "")
                MessageBox.Show("此版本是测试版本！", Title);
            Title = "无限宝第三方插件 Ver " + AppVer.Major + "." + AppVer.Minor + "." + AppVer.Build + SpecialVersion + " 安装程序";
            CurrentVersion = int.Parse(AppVer.Major.ToString() + AppVer.Minor.ToString() + AppVer.Build.ToString());

			OriginBtnInstallMargin = ButtonInstall.Margin;
			OriginButtonInstall = ButtonInstall;
			OriginBtnWidth = ButtonInstall.Width;

            //如果是XP则跳过联网检测
            if (Strings.Left(Environment.OSVersion.ToString(), 22) == "Microsoft Windows NT 5")
            {
				FontFamily = new System.Windows.Media.FontFamily("SimSun");
				try
				{
					TestLoc = Strings.Split(TestLocs, "#");
					for (int i = 0; i <= TestLoc.ToList().Count - 1; i++)
						if (File.Exists(TestLoc[i]))
						{
							TextBoxLocation_Text = TestLoc[i];
							FirstNavi = true;
							break;
						}
				}
				catch (Exception ex)
				{
					MessageBox.Show("运行时出错！\n详细错误信息：\n" + ex.ToString(), Window_Title);
				}

				//Check if installed
				try
				{
					if (FirstNavi == true)
					{
						if (File.Exists(TextBoxLocation_Text.Replace("iMeeting.exe", "CaptureDesktop.dll").Replace("iMeeting2.exe", "CaptureDesktop.dll")) && File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll")) && File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll")))
							ButtonInstall_Content = "更新";
					}
				}
				catch { }


				ButtonInstall.Content = ButtonInstall_Content;
				TextBoxLocation.Text = TextBoxLocation_Text;
				loadingWait.Visibility = Visibility.Hidden;
				labelUpdate.Visibility = Visibility.Hidden;

				return;
            }

			Window_Title = Title;
			ButtonInstall_Content = ButtonInstall.Content.ToString();
			TextBoxLocation_Text = TextBoxLocation.Text;
			CheckIfOver.Tick += CheckIfOver_Tick;
			CheckIfOver.Interval = TimeSpan.FromMilliseconds(50);
			CheckIfOver.Start();
			UpdateThread = new Thread(new ThreadStart(OnlineCheck));
			UpdateThread.Start();

            CheckDarkMode();
        }

		private void CheckIfOver_Tick(object sender, EventArgs e)
		{
			if (OnlineCheckOver)
			{
				CheckIfOver.IsEnabled = false;
				ButtonInstall.Content = ButtonInstall_Content;
				TextBoxLocation.Text = TextBoxLocation_Text;
				loadingWait.Visibility = Visibility.Hidden;
				labelUpdate.Visibility = Visibility.Hidden;
			}
		}

		private void OnlineCheck()
		{
			try
			{
				TestLoc = Strings.Split(TestLocs, "#");
				for (int i = 0; i <= TestLoc.ToList().Count - 1; i++)
					if (File.Exists(TestLoc[i]))
					{
						TextBoxLocation_Text = TestLoc[i];
						FirstNavi = true;
						break;
					}
			}
			catch (Exception ex)
			{
				MessageBox.Show("运行时出错！\n详细错误信息：\n" + ex.ToString(), Window_Title);
			}

			//Check if installed
			try
			{
				if (FirstNavi == true)
				{
					if (File.Exists(TextBoxLocation_Text.Replace("iMeeting.exe", "CaptureDesktop.dll").Replace("iMeeting2.exe", "CaptureDesktop.dll")) && File.Exists(TextBoxLocation_Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll")) && File.Exists(TextBoxLocation_Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll")))
						ButtonInstall_Content = "更新";
				}
			}
			catch { }

			try
			{
				string ForceUpdate, Ver, LinkTo; int VersionNum; string[] Str;
				string WebText = GetWebCode("https://gitee.com/klxn/wxbplugin/raw/master/service.txt");
				Str = Strings.Split(WebText, "<版本>");
				if (Str.ToList().Count < 2)
				{ 
					MessageBox.Show("连接服务器失败！", Window_Title);
					CheckDarkMode();
					OnlineCheckOver = true;
					return;
				}
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
					MessageBox.Show("处理联网信息时发生错误！", Window_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					VersionNum = int.Parse(AppVer.Major.ToString() + AppVer.Minor.ToString() + AppVer.Build.ToString());
				}
				if (VersionNum > CurrentVersion)
					if (ForceUpdate == "0")
					{
						if (MessageBox.Show("插件已更新，最新版本：" + Ver + "\n是否跳转下载更新？", Window_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
							Process.Start(LinkTo);
					}
					else
					{
						MessageBox.Show("插件已更新，最新版本：" + Ver + "\n即将跳转下载更新！", Window_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
						Process.Start(LinkTo);
						//ProjectData.EndApp();
						Environment.Exit(0);
					}
			}
			catch
			{
				MessageBox.Show("处理联网信息时发生错误！", Window_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			OnlineCheckOver = true;
		}

		private void ButtonInstall_Click(object sender, EventArgs e)
		{
			if (CheckBoxAgreement.IsChecked == false)
			{
				MessageBox.Show("请勾选已阅读并同意用户协议和免责声明", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			if (TextBoxLocation.Text == "" || TextBoxLocation.Text == " ")
			{
				MessageBox.Show("请手动输入或浏览找到 iMeeting.exe 文件位置", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			MessageBox.Show("如果您已经关闭无限宝和杀毒软件，点击“确定”以继续安装。", Title);
			TextBoxLocation.Text = OpenFileDialog.FileName.Replace("LoginTool.exe", "iMeeting.exe");
			try
			{
				byte[] b = Properties.Resources.CaptureDesktop;
				Stream s = File.Create(TextBoxLocation.Text.Replace("iMeeting.exe", "CaptureDesktop.dll").Replace("iMeeting2.exe", "CaptureDesktop.dll"));
				s.Write(b, 0, b.Length);
				s.Close();
				b = Properties.Resources.wxbPluginGUI;
				s = File.Create(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll"));
				s.Write(b, 0, b.Length);
				s.Close();
				b = Properties.Resources.wxbHookCore;
				s = File.Create(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbHookCore.dll").Replace("iMeeting2.exe", "wxbHookCore.dll"));
				s.Write(b, 0, b.Length);
				s.Close();
				MessageBox.Show("安装成功！", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch
			{
				MessageBox.Show("安装失败，无限宝是否正在运行？目录是否正确？是否以管理员模式运行了本安装程序？是否已关闭杀毒软件？", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				if (MessageBox.Show("是否查看帮助文件？", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
					Process.Start("https://gitee.com/klxn/wxbplugin/raw/master/install.png");
			}
		}

		private void ButtonNavigate_Click(object sender, EventArgs e)
		{
			if (FirstNavi == true)
			{
				FirstNavi = false;
				if (MessageBox.Show("本安装程序已自动为你填写好了路径，取消浏览并安装？", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
				{
					ButtonInstall_Click(sender, e);
					return;
				}
			}
			OpenFileDialog OpenFileDialog = new OpenFileDialog();
			OpenFileDialog.Filter = "无限宝登陆工具 (LoginTool.exe)|*.exe";
			if (OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				if (Strings.InStr(OpenFileDialog.FileName, "LoginTool.exe") != 0)
				{
					TextBoxLocation.Text = OpenFileDialog.FileName.Replace("LoginTool.exe", "iMeeting.exe");
					try
					{
						if (File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "CaptureDesktop.dll").Replace("iMeeting2.exe", "CaptureDesktop.dll")) && File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll")) && File.Exists(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll")))
							ButtonInstall.Content = "更新";
					}
					catch { }
				}
				else
				{
					MessageBox.Show("选择的程序不是无限宝登陆程序！", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
					if (MessageBox.Show("是否查看帮助文件？", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
						Process.Start("https://gitee.com/klxn/wxbplugin/raw/master/install.png");
				}
			}
		}

		private void ButtonUnistall_Click(object sender, RoutedEventArgs e)
		{
			if (TextBoxLocation.Text == "" || TextBoxLocation.Text == " ")
			{
				MessageBox.Show("请手动输入或浏览找到 iMeeting.exe 文件位置", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
						MessageBox.Show("卸载 CaptureDesktop.dll 时发生错误。", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
						FailedInProcess = true;
					}
					try
					{
						File.Delete(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbPluginGUI.dll").Replace("iMeeting2.exe", "wxbPluginGUI.dll"));
					}
					catch
					{
						MessageBox.Show("卸载 wxbPluginGUI.dll 时发生错误。", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
						FailedInProcess = true;
					}
					try
					{
						File.Delete(TextBoxLocation.Text.Replace("iMeeting.exe", "wxbHookCore.dll").Replace("iMeeting2.exe", "wxbHookCore.dll"));
					}
					catch
					{
						MessageBox.Show("卸载 wxbHookCore.dll 时发生错误。", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
						FailedInProcess = true;
					}
					if (FailedInProcess == false)
					{
						MessageBox.Show("卸载成功！", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
						ButtonInstall.Content = "安装";
					}
				}
				catch
				{
					MessageBox.Show("卸载失败，无限宝是否正在运行？目录是否正确？是否以管理员模式运行了本安装程序？是否已关闭杀毒软件？", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			else
			{
				MessageBox.Show("卸载成功！", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
				ButtonInstall.Content = "安装";
			}
		}

		private void CheckDarkMode()
		{
			try
			{
				System.Windows.Media.Brush BlackBrush = ButtonInstall.Foreground;
				System.Windows.Media.Brush ButtonBackgroundBrush = ButtonInstall.Foreground;
				System.Windows.Media.Brush WhiteBrush = TextBoxLocation.Background;

				string readValue = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", true).GetValue("AppsUseLightTheme").ToString();
				if (readValue == "0" && Strings.Left(Environment.OSVersion.ToString(), 23) == "Microsoft Windows NT 10")
				{
					SystemDarkMode = true;
					Background = BlackBrush;
					ButtonInstall.Background = ButtonBackgroundBrush;
					ButtonUnistall.Background = ButtonBackgroundBrush;
					ButtonNavigate.Background = ButtonBackgroundBrush;
					ButtonInstall.Foreground = WhiteBrush;
					ButtonUnistall.Foreground = WhiteBrush;
					ButtonNavigate.Foreground = WhiteBrush;
					TextBoxLocation.Foreground = WhiteBrush;
					TextBoxLocation.Background = BlackBrush;
					labelLocation.Foreground = WhiteBrush;
					CheckBoxAgreement.Foreground = WhiteBrush;
					loadingWait.Background = BlackBrush;
					labelUpdate.Foreground = WhiteBrush;

					WhiteBrush = TextBoxLocation.BorderBrush;

					ButtonInstall.BorderBrush = WhiteBrush;
					ButtonUnistall.BorderBrush = WhiteBrush;
					ButtonNavigate.BorderBrush = WhiteBrush;
				}
			}
			catch { }
		}

		private void ButtonInstall_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (CheckBoxAgreement.IsChecked == false)
			{
				int P = 10;// * ScreenDPI / 96;
				double CenterX, CenterY;
				CenterX = ButtonInstall.Width / 2;
				CenterY = ButtonInstall.Height / 2;				
				if (e.GetPosition(ButtonInstall).X >= CenterX)
					ButtonInstall.Margin = new Thickness(ButtonInstall.Margin.Left + e.GetPosition(ButtonInstall).X - ButtonInstall.Width - P,ButtonInstall.Margin.Top, ButtonInstall.Margin.Right, ButtonInstall.Margin.Bottom);
				else
					ButtonInstall.Margin = new Thickness(ButtonInstall.Margin.Left + e.GetPosition(ButtonInstall).X + P, ButtonInstall.Margin.Top, ButtonInstall.Margin.Right, ButtonInstall.Margin.Bottom);
				if (e.GetPosition(ButtonInstall).Y >= CenterY)
					ButtonInstall.Margin = new Thickness(ButtonInstall.Margin.Left, ButtonInstall.Margin.Top + e.GetPosition(ButtonInstall).Y - ButtonInstall.Height - P, ButtonInstall.Margin.Right, ButtonInstall.Margin.Bottom);
				else
					ButtonInstall.Margin = new Thickness(ButtonInstall.Margin.Left, ButtonInstall.Margin.Top + e.GetPosition(ButtonInstall).Y + P, ButtonInstall.Margin.Right, ButtonInstall.Margin.Bottom);
				/*
				if (e.GetPosition(ButtonInstall).X >= OriginBtnWidth)
					ButtonInstall.Width = e.GetPosition(ButtonInstall).X-2;//2 * ButtonInstall.Width - ;//new Thickness(ButtonInstall.Margin.Left + e.GetPosition(ButtonInstall).X - ButtonInstall.Width - P, ButtonInstall.Margin.Top, ButtonInstall.Margin.Right, ButtonInstall.Margin.Bottom);
				else
				{
					if(ButtonInstall.Width > OriginBtnWidth / 2)
					{
						ButtonInstall.Margin = new Thickness(OriginBtnInstallMargin.Left + e.GetPosition(this).X - 10 + P, ButtonInstall.Margin.Top, ButtonInstall.Margin.Right, ButtonInstall.Margin.Bottom);
						ButtonInstall.Width = OriginBtnWidth - e.GetPosition(this).X + 10;
						Title = (OriginButtonInstall.Width / 2).ToString();// e.GetPosition(this).X.ToString();
					}
					else
					{
						if(ButtonInstall.Margin.Left + ButtonInstall.Width / 2 >= )
						ButtonInstall.Margin = new Thickness(OriginBtnInstallMargin.Left + e.GetPosition(this).X - 10 + P, ButtonInstall.Margin.Top, ButtonInstall.Margin.Right, ButtonInstall.Margin.Bottom);
					//ButtonInstall.Width = OriginButtonInstall.Width - e.GetPosition(this).X - 10 + CenterX;
					}
				}*/
			}
		}

		private void CheckBoxAgreement_Checked(object sender, RoutedEventArgs e)
		{
			ButtonInstall.Margin = OriginBtnInstallMargin;
			ButtonInstall.Width = OriginButtonInstall.Width;
			ButtonInstall.Height = OriginButtonInstall.Height;
		}

		private void labelManual_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Process.Start("https://gitee.com/klxn/wxbplugin/raw/master/install.png");
		}

		private void labelDeclear_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Process.Start("https://yuhuison-1259460701.cos.ap-chengdu.myqcloud.com/mzsm.html");
		}

		private void label_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Process.Start("https://www.bilibili.com/video/BV1Ca4y1E7mx");
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
