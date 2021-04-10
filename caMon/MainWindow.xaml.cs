using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace caMon
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : NavigationWindow
	{
		/// <summary>ページセレクタのインスタンス.  起動中は不変</summary>
		readonly ISelector Selector_inst = null;

		IPages __ShowingPage = null;
		IPages ShowingPage
		{
			get => __ShowingPage;
			set
			{
				//NULLは許容されない
				if (value is null)
					throw new Exception("Setting the null value is not allowed.");

				//新ページの設定
				value.BackToHome += OnBackToHome;
				value.CloseApp += OnCloseAppFired;
				NavigationService.Navigate(value.FrontPage);

				//旧ページの解放
				if (__ShowingPage is not null)
				{
					__ShowingPage.BackToHome -= OnBackToHome;
					__ShowingPage.CloseApp -= OnCloseAppFired;
					__ShowingPage.Dispose();
				}

				__ShowingPage = value;//表示中ページ記録の更新
			}
		}

		readonly Settings CLA = null;

		Process _BveProcess = null;
		Process BveProcess
		{
			get
			{
				if (_BveProcess?.HasExited == true)//プロセスが存在する
					return BveProcess;

				foreach(var p in Process.GetProcessesByName(CLA.BveProcessName))
				{
					try
					{
						if (CLA.BveExeFileName.Equals(Path.GetFileName(p.MainModule.FileName)))
							return (_BveProcess = p);
					}catch(Exception)
					{
					}
				}
				return (_BveProcess= null);
			}
		}

		public MainWindow(in Settings settings)
		{
			CLA = settings;

			InitializeComponent();

			if (CLA.NotBlickBVE && !string.IsNullOrWhiteSpace(CLA.BveExeFileName) && !string.IsNullOrWhiteSpace(CLA.BveProcessName))
			{
				MouseDown += (_, _) => SetBveWindowToFront();
				SetBveWindowToFront();
			}

			Selector_inst = CLA.selector_toRet ?? new selector.default_.SelectPage();//コマンドライン引数でセレクタが設定されてれば, それを使用する.

			Selector_inst.PageChangeRequest += Selector_inst_PageChangeRequest;

			//初期ページはセレクタ画面
			//表示するページの登録(イベント他)
			ShowingPage = CLA.page_toShow ?? Selector_inst;
		}

		private void SetBveWindowToFront()
		{
			//active window ref : https://dobon.net/vb/dotnet/process/appactivate.html
			Process p = BveProcess;
			if (p is not null)
				Microsoft.VisualBasic.Interaction.AppActivate(p.Id);
		}


		private void ApplyCLArgs()
		{
			//ウィンドウの各種プロパティをセット
			Height = CLA.Height ?? Height;
			Width = CLA.Width ?? Width;

			Left = CLA.Left ?? Left;
			Top = CLA.Top ?? Top;

			Topmost = CLA.Topmost ?? Topmost;
			ShowInTaskbar = CLA.ShowInTaskbar ?? ShowInTaskbar;

			WindowStartupLocation = CLA.WindowStartupLocation ?? WindowStartupLocation;
			ResizeMode = CLA.ResizeMode ?? ResizeMode;

			WindowState = CLA.WindowState ?? WindowState;
			WindowStyle = CLA.WindowStyle ?? WindowStyle;
		}

		private void Selector_inst_PageChangeRequest(object sender, PageChangeEventArgs e)
		{
			if(e is not null)
			{
				if(e.NewPage is not null)
					ShowingPage = e.NewPage;
				else if (!string.IsNullOrWhiteSpace(e.ModPath))
					try
					{
						//pathからmodをload
						ShowingPage = ModLoader.LoadDllInst<IPages>(e.ModPath);
					}
					catch (FileNotFoundException fnfe)
					{
						MessageBox.Show("指定のmodファイルが見つかりませんでした\n" + fnfe.ToString());
					}
					catch (EntryPointNotFoundException epnfe)
					{
						MessageBox.Show("指定のmodファイルにページ実装が含まれていませんでした\n" + epnfe.ToString());
					}
					catch (Exception ex)
					{
						MessageBox.Show("不明なエラーが発生しました.\n" + ex.ToString());
					}
				else
					MessageBox.Show("次のページの指定がされていません");
			}

		}

		private void OnCloseAppFired(object sender, EventArgs e)
		{
			if (CLA.CloseFunctionEnabled == true)
				this.Close();
		}

		private void OnBackToHome(object sender, EventArgs e)
		{
			if (CLA.BackFunctionEnabled == true)
				ShowingPage = Selector_inst;
		}

		private void MainWindowHeadder_PreviewKeyUp(object sender, KeyEventArgs e)
		{
			if (CLA.F11Enabled == true && e.Key == Key.F11)
			{
				switch (MainWindowHeadder.WindowState)
				{
					case WindowState.Maximized:
						MainWindowHeadder.WindowState = WindowState.Normal;
						MainWindowHeadder.WindowStyle = WindowStyle.SingleBorderWindow;
						break;
					case WindowState.Normal:
						MainWindowHeadder.WindowStyle = WindowStyle.None;
						MainWindowHeadder.WindowState = WindowState.Maximized;
						break;
				}
			}
			if (CLA.F12Enabled == true && e.Key == Key.F12)
			{
				switch (MainWindowHeadder.WindowStyle)
				{
					case WindowStyle.None:
						MainWindowHeadder.WindowStyle = WindowStyle.SingleBorderWindow;
						break;
					case WindowStyle.SingleBorderWindow:
						MainWindowHeadder.WindowStyle = WindowStyle.None;
						break;
				}
			}
		}

		private void MainWindowHeadder_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			ShowingPage?.Dispose();
			SharedFuncs.SMem_RStop();
		}

		private void MainWindowHeadder_Initialized(object sender, EventArgs e) => ApplyCLArgs();
	}
}
