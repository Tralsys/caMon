#nullable enable
using System;
using System.IO;
using System.Windows;
using System.Windows.Navigation;

namespace caMon
{
	public partial class MainWindow : NavigationWindow
	{
		private class CLArgs
		{
			public ISelector? selector_toRet = null;
			public IPages? page_toShow = null;

			public WindowState? WindowState = null;
			public WindowStyle? WindowStyle = null;
			public WindowStartupLocation? WindowStartupLocation = null;
			public ResizeMode? ResizeMode = null;

			public bool? Topmost = null;
			public bool? ShowInTaskbar = null;

			public bool? F11Enabled = true;
			public bool? F12Enabled = true;

			public bool? CloseFunctionEnabled = true;
			public bool? BackFunctionEnabled = true;

			public int? Height = null;
			public int? Width = null;
			public int? Left = null;
			public int? Top = null;

			public bool NotBlickBVE = false;//独自機能のためnull非許容
			public string BveExeFileName = "BveTs.exe";//独自機能のためnull非許容
			public string BveProcessName = "BveTs";//独自機能のためnull非許容
		}

		CLArgs CheckCLArgs()
		{
			CLArgs cla = new CLArgs();

			if (App.CmdLArgs?.Length > 0)
			{
				for (int i = 0; i < App.CmdLArgs.Length; i++)
				{
					if (App.CmdLArgs[i].StartsWith("#"))//#から始まる文字列は使用しない(コメント機能)
						continue;
					int tmp;
					switch (App.CmdLArgs[i].ToLower())
					{
						case "/wa":
						case "/wsta":
						case "/wstat":
						case "/wstate":
							if (i == (App.CmdLArgs.Length - 1))
								break;
							cla.WindowState = App.CmdLArgs[++i].ToLower() switch
							{
								"nm" => WindowState.Normal,
								"mx" => WindowState.Maximized,
								"mn" => WindowState.Minimized,

								"norm" => WindowState.Normal,
								"maxm" => WindowState.Maximized,
								"minm" => WindowState.Minimized,

								"normal" => WindowState.Normal,
								"maximized" => WindowState.Maximized,
								"minimized" => WindowState.Minimized,

								_ => null
							};
							break;

						case "/wy":
						case "/wsty":
						case "/wstyl":
						case "/wstyle":
							if (i == (App.CmdLArgs.Length - 1))
								break;
							cla.WindowStyle = App.CmdLArgs[++i].ToLower() switch
							{
								"no" => WindowStyle.None,
								"sb" => WindowStyle.SingleBorderWindow,
								"tl" => WindowStyle.ToolWindow,
								"tb" => WindowStyle.ThreeDBorderWindow,

								"none" => WindowStyle.None,
								"singleborder" => WindowStyle.SingleBorderWindow,
								"tool" => WindowStyle.ToolWindow,
								"threedborder" => WindowStyle.ThreeDBorderWindow,

								"singleborderwindow" => WindowStyle.SingleBorderWindow,
								"toolwindow" => WindowStyle.ToolWindow,
								"threedborderwindow" => WindowStyle.ThreeDBorderWindow,

								_ => null
							};
							break;

						case "/wl":
						case "/wstl":
						case "/wstlo":
						case "/wstloc":
						case "/windowstartuplocation":
							if (i == (App.CmdLArgs.Length - 1))
								break;
							cla.WindowStartupLocation = App.CmdLArgs[++i].ToLower() switch
							{
								"co" => WindowStartupLocation.CenterOwner,
								"cs" => WindowStartupLocation.CenterScreen,
								"mn" => WindowStartupLocation.Manual,

								"centerowner" => WindowStartupLocation.CenterOwner,
								"centerscreen" => WindowStartupLocation.CenterScreen,
								"manual" => WindowStartupLocation.Manual,

								_ => null
							};
							break;

						case "/rm":
						case "/resizemode":
							if (i == (App.CmdLArgs.Length - 1))
								break;
							cla.ResizeMode = App.CmdLArgs[++i].ToLower() switch
							{
								"no" => ResizeMode.NoResize,
								"mn" => ResizeMode.CanMinimize,
								"rs" => ResizeMode.CanResize,
								"rg" => ResizeMode.CanResizeWithGrip,

								"noresize" => ResizeMode.NoResize,
								"canminimize" => ResizeMode.CanMinimize,
								"canresize" => ResizeMode.CanResize,
								"canresizewithgrip" => ResizeMode.CanResizeWithGrip,

								_ => null
							};
							break;

						case "/tm":
						case "/topmost":
							if (i == (App.CmdLArgs.Length - 1))
								break;
							cla.Topmost = BoolChecker(App.CmdLArgs[++i]);
							break;

						case "/st":
						case "/showintaskbar":
							if (i == (App.CmdLArgs.Length - 1))
								break;
							cla.ShowInTaskbar = BoolChecker(App.CmdLArgs[++i]);
							break;

						case "/f11e":
						case "/f11enabled":
							if (i == (App.CmdLArgs.Length - 1))
								break;
							cla.F11Enabled = BoolChecker(App.CmdLArgs[++i]);
							break;

						case "/f12e":
						case "/f12enabled":
							if (i == (App.CmdLArgs.Length - 1))
								break;
							cla.F12Enabled = BoolChecker(App.CmdLArgs[++i]);
							break;

						case "/cfe":
						case "/closefunc":
						case "/closefunction":
						case "/closefunctionenabled":
							if (i == (App.CmdLArgs.Length - 1))
								break;
							cla.CloseFunctionEnabled = BoolChecker(App.CmdLArgs[++i]);
							break;

						case "/bfe":
						case "/backfunc":
						case "/backfunction":
						case "/backfunctionenabled":
							if (i == (App.CmdLArgs.Length - 1))
								break;
							cla.BackFunctionEnabled = BoolChecker(App.CmdLArgs[++i]);
							break;


						case "/h":
						case "/he":
						case "/hei":
						case "/heig":
						case "/heigh":
						case "/height":
							if (i == (App.CmdLArgs.Length - 1))
								break;
							if (int.TryParse(App.CmdLArgs[++i], out tmp))
								cla.Height = tmp;
							break;

						case "/w":
						case "/wi":
						case "/wid":
						case "/widt":
						case "/width":
							if (i == (App.CmdLArgs.Length - 1))
								break;
							if (int.TryParse(App.CmdLArgs[++i], out tmp))
								cla.Width = tmp;
							break;

						case "/l":
						case "/le":
						case "/lef":
						case "/left"://ウィンドウ表示位置指定用
							if (i == (App.CmdLArgs.Length - 1))
								break;
							if (int.TryParse(App.CmdLArgs[++i], out tmp))
								cla.Left = tmp;
							break;
						case "/t":
						case "/to":
						case "/top":
							if (i == (App.CmdLArgs.Length - 1))
								break;
							if (int.TryParse(App.CmdLArgs[++i], out tmp))
								cla.Top = tmp;
							break;

						case "/nbbve":
						case "/nblockbve":
						case "/notblockbve":
							if (i == (App.CmdLArgs.Length - 1))
								break;
							cla.NotBlickBVE = BoolChecker(App.CmdLArgs[++i]) ?? cla.NotBlickBVE;
							break;

						case "/bvefn":
						case "/bvefname":
						case "/bveexefname":
						case "/bvefilename":
						case "/bveexefilename":
							if (i == (App.CmdLArgs.Length - 1))
								break;
							cla.BveExeFileName = App.CmdLArgs[++i];
							break;

						case "/bvepn":
						case "/bvepname":
						case "/bveprocessname":
							if (i == (App.CmdLArgs.Length - 1))
								break;
							cla.BveProcessName = App.CmdLArgs[++i];
							break;

						default://オプション設定の該当なし => モジュール確認

							//ファイルの存在確認はLoadDllInstがやってくれる.
							try
							{
								//先にセレクタを確認
								cla.selector_toRet = ModLoader.LoadDllInst<ISelector>(App.CmdLArgs[i]);
							}
							catch (FileNotFoundException)
							{
								continue;//ファイルがないなら次の引数へ
							}
							catch (MissingMethodException)
							{
								try
								{
									cla.page_toShow = ModLoader.LoadDllInst<IPages>(App.CmdLArgs[i]);
								}
								catch (Exception e)
								{
									Console.WriteLine(e);
								}
							}
							catch (EntryPointNotFoundException)//ISelectorではなかった
							{
								try
								{
									cla.page_toShow = ModLoader.LoadDllInst<IPages>(App.CmdLArgs[i]);
								}
								catch(Exception e)
								{
									Console.WriteLine(e);
								}
							}
							catch (Exception e)
							{
								Console.WriteLine(e);
							}
							break;
					}
				}
			}

			return cla;
		}

		bool? BoolChecker(in string s)
		{
			switch (s.ToLower())
			{
				case "t":
				case "tr":
				case "tru":
				case "true":
					return true;

				case "f":
				case "fa":
				case "fal":
				case "fals":
				case "false":
					return false;

				default:
					return null;
			}
		}
	}
}
