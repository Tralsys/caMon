#nullable enable
using System;
using System.IO;
using System.Windows;
using System.Windows.Navigation;

namespace caMon
{
	public partial class MainWindow : NavigationWindow
	{
		public interface IMainWindowSettings
		{
			ISelector? selector_toRet { get; set; }
			IPages? page_toShow { get; set; }

			WindowState? WindowState { get; set; }
			WindowStyle? WindowStyle { get; set; }
			WindowStartupLocation? WindowStartupLocation { get; set; }
			ResizeMode? ResizeMode { get; set; }

			bool? Topmost { get; set; }
			bool? ShowInTaskbar { get; set; }

			bool? F11Enabled { get; set; }
			bool? F12Enabled { get; set; }

			bool? CloseFunctionEnabled { get; set; }
			bool? BackFunctionEnabled { get; set; }

			int? Height { get; set; }
			int? Width { get; set; }
			int? Left { get; set; }
			int? Top { get; set; }

			bool NotBlickBVE { get; set; }//独自機能のためnull非許容
			string BveExeFileName { get; set; }//独自機能のためnull非許容
			string BveProcessName { get; set; }//独自機能のためnull非許容
		}
		public class Settings : IMainWindowSettings
		{
			public ISelector? selector_toRet { get; set; } = null;
			public IPages? page_toShow { get; set; } = null;

			public WindowState? WindowState { get; set; } = null;
			public WindowStyle? WindowStyle { get; set; } = null;
			public WindowStartupLocation? WindowStartupLocation { get; set; } = null;
			public ResizeMode? ResizeMode { get; set; } = null;

			public bool? Topmost { get; set; } = null;
			public bool? ShowInTaskbar { get; set; } = null;

			public bool? F11Enabled { get; set; } = true;
			public bool? F12Enabled { get; set; } = true;

			public bool? CloseFunctionEnabled { get; set; } = true;
			public bool? BackFunctionEnabled { get; set; } = true;

			public int? Height { get; set; } = null;
			public int? Width { get; set; } = null;
			public int? Left { get; set; } = null;
			public int? Top { get; set; } = null;

			public bool NotBlickBVE { get; set; } = false;//独自機能のためnull非許容
			public string BveExeFileName { get; set; } = "BveTs.exe";//独自機能のためnull非許容
			public string BveProcessName { get; set; } = "BveTs";//独自機能のためnull非許容

			public void SetSettings(in string[] CmdLArgs)
			{
				if (CmdLArgs.Length <= 0)
					return;

				for (int i = 0; i < CmdLArgs.Length; i++)
				{
					if (CmdLArgs[i].StartsWith("#"))//#から始まる文字列は使用しない(コメント機能)
						continue;
					int tmp;
					switch (CmdLArgs[i].ToLower())
					{
						case "/wa":
						case "/wsta":
						case "/wstat":
						case "/wstate":
							if (i == (CmdLArgs.Length - 1))
								break;
							WindowState = CmdLArgs[++i].ToLower() switch
							{
								"nm" or "norm" or "normal" => System.Windows.WindowState.Normal,
								"mx" or "maxm" or "maximized" => System.Windows.WindowState.Maximized,
								"mn" or "minm" or "minimized" => System.Windows.WindowState.Minimized,
								_ => null
							};
							break;

						case "/wy":
						case "/wsty":
						case "/wstyl":
						case "/wstyle":
							if (i == (CmdLArgs.Length - 1))
								break;
							WindowStyle = CmdLArgs[++i].ToLower() switch
							{
								"no" or "none" => System.Windows.WindowStyle.None,
								"sb" or "singleborder" or "singleborderwindow" => System.Windows.WindowStyle.SingleBorderWindow,
								"tl" or "tool" or "toolwindow" => System.Windows.WindowStyle.ToolWindow,
								"tb" or "threedborderwindow" => System.Windows.WindowStyle.ThreeDBorderWindow,
								_ => null
							};
							break;

						case "/wl":
						case "/wstl":
						case "/wstlo":
						case "/wstloc":
						case "/windowstartuplocation":
							if (i == (CmdLArgs.Length - 1))
								break;
							WindowStartupLocation = CmdLArgs[++i].ToLower() switch
							{
								"co" or "centerowner" => System.Windows.WindowStartupLocation.CenterOwner,
								"cs" or "centerscreen" => System.Windows.WindowStartupLocation.CenterScreen,
								"mn" or "manual" => System.Windows.WindowStartupLocation.Manual,
								_ => null
							};
							break;

						case "/rm":
						case "/resizemode":
							if (i == (CmdLArgs.Length - 1))
								break;
							ResizeMode = CmdLArgs[++i].ToLower() switch
							{
								"no" or "noresize" => System.Windows.ResizeMode.NoResize,
								"mn" or "canminimize" => System.Windows.ResizeMode.CanMinimize,
								"rs" or "canresize" => System.Windows.ResizeMode.CanResize,
								"rg" or "canresizewithgrip" => System.Windows.ResizeMode.CanResizeWithGrip,
								_ => null
							};
							break;

						case "/tm":
						case "/topmost":
							if (i == (CmdLArgs.Length - 1))
								break;
							Topmost = BoolChecker(CmdLArgs[++i]);
							break;

						case "/st":
						case "/showintaskbar":
							if (i == (CmdLArgs.Length - 1))
								break;
							ShowInTaskbar = BoolChecker(CmdLArgs[++i]);
							break;

						case "/f11e":
						case "/f11enabled":
							if (i == (CmdLArgs.Length - 1))
								break;
							F11Enabled = BoolChecker(CmdLArgs[++i]);
							break;

						case "/f12e":
						case "/f12enabled":
							if (i == (CmdLArgs.Length - 1))
								break;
							F12Enabled = BoolChecker(CmdLArgs[++i]);
							break;

						case "/cfe":
						case "/closefunc":
						case "/closefunction":
						case "/closefunctionenabled":
							if (i == (CmdLArgs.Length - 1))
								break;
							CloseFunctionEnabled = BoolChecker(CmdLArgs[++i]);
							break;

						case "/bfe":
						case "/backfunc":
						case "/backfunction":
						case "/backfunctionenabled":
							if (i == (CmdLArgs.Length - 1))
								break;
							BackFunctionEnabled = BoolChecker(CmdLArgs[++i]);
							break;


						case "/h":
						case "/he":
						case "/hei":
						case "/heig":
						case "/heigh":
						case "/height":
							if (i == (CmdLArgs.Length - 1))
								break;
							if (int.TryParse(CmdLArgs[++i], out tmp))
								Height = tmp;
							break;

						case "/w":
						case "/wi":
						case "/wid":
						case "/widt":
						case "/width":
							if (i == (CmdLArgs.Length - 1))
								break;
							if (int.TryParse(CmdLArgs[++i], out tmp))
								Width = tmp;
							break;

						case "/l":
						case "/le":
						case "/lef":
						case "/left"://ウィンドウ表示位置指定用
							if (i == (CmdLArgs.Length - 1))
								break;
							if (int.TryParse(CmdLArgs[++i], out tmp))
								Left = tmp;
							break;
						case "/t":
						case "/to":
						case "/top":
							if (i == (CmdLArgs.Length - 1))
								break;
							if (int.TryParse(CmdLArgs[++i], out tmp))
								Top = tmp;
							break;

						case "/nbbve":
						case "/nblockbve":
						case "/notblockbve":
							if (i == (CmdLArgs.Length - 1))
								break;
							NotBlickBVE = BoolChecker(CmdLArgs[++i]) ?? NotBlickBVE;
							break;

						case "/bvefn":
						case "/bvefname":
						case "/bveexefname":
						case "/bvefilename":
						case "/bveexefilename":
							if (i == (CmdLArgs.Length - 1))
								break;
							BveExeFileName = CmdLArgs[++i];
							break;

						case "/bvepn":
						case "/bvepname":
						case "/bveprocessname":
							if (i == (CmdLArgs.Length - 1))
								break;
							BveProcessName = CmdLArgs[++i];
							break;

						default://オプション設定の該当なし => モジュール確認

							//ファイルの存在確認はLoadDllInstがやってくれる.
							try
							{
								//先にセレクタを確認
								selector_toRet = ModLoader.LoadDllInst<ISelector>(CmdLArgs[i]);
							}
							catch (FileNotFoundException)
							{
								continue;//ファイルがないなら次の引数へ
							}
							catch (MissingMethodException)
							{
								LoadAndSetIPagesInstance(CmdLArgs[i]);
							}
							catch (EntryPointNotFoundException)//ISelectorではなかった
							{
								LoadAndSetIPagesInstance(CmdLArgs[i]);
							}
							catch (Exception e)
							{
								System.Diagnostics.Debug.WriteLine(e);
							}
							break;
					}
				}
			}

			void LoadAndSetIPagesInstance(in string path)
			{
				try
				{
					page_toShow = ModLoader.LoadDllInst<IPages>(path);
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine(e);
				}
			}

			static bool? BoolChecker(in string s)
				 => s.ToLower() switch
				 {
					 "t" or "tr" or "tru" or "true" => true,
					 "f" or "fa" or "fal" or "fals" or "false" => false,
					 _ => null
				 };
		}
	}
}
