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

		static CLArgs CheckCLArgs() => CheckCLArgs(App.CmdLArgs);

		static CLArgs CheckCLArgs(in string[] CmdLArgs)
		{
			CLArgs cla = new();

			if (CmdLArgs?.Length > 0)
			{
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
							cla.WindowState = CmdLArgs[++i].ToLower() switch
							{
								"nm" or "norm" or "normal" => WindowState.Normal,
								"mx" or "maxm" or "maximized" => WindowState.Maximized,
								"mn" or "minm" or "minimized" => WindowState.Minimized,
								_ => null
							};
							break;

						case "/wy":
						case "/wsty":
						case "/wstyl":
						case "/wstyle":
							if (i == (CmdLArgs.Length - 1))
								break;
							cla.WindowStyle = CmdLArgs[++i].ToLower() switch
							{
								"no" or "none" => WindowStyle.None,
								"sb" or "singleborder" or "singleborderwindow" => WindowStyle.SingleBorderWindow,
								"tl" or "tool" or "toolwindow" => WindowStyle.ToolWindow,
								"tb" or "threedborderwindow" => WindowStyle.ThreeDBorderWindow,
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
							cla.WindowStartupLocation = CmdLArgs[++i].ToLower() switch
							{
								"co" or "centerowner" => WindowStartupLocation.CenterOwner,
								"cs" or "centerscreen" => WindowStartupLocation.CenterScreen,
								"mn" or "manual" => WindowStartupLocation.Manual,
								_ => null
							};
							break;

						case "/rm":
						case "/resizemode":
							if (i == (CmdLArgs.Length - 1))
								break;
							cla.ResizeMode = CmdLArgs[++i].ToLower() switch
							{
								"no" or "noresize" => ResizeMode.NoResize,
								"mn" or "canminimize" => ResizeMode.CanMinimize,
								"rs" or "canresize" => ResizeMode.CanResize,
								"rg" or "canresizewithgrip" => ResizeMode.CanResizeWithGrip,
								_ => null
							};
							break;

						case "/tm":
						case "/topmost":
							if (i == (CmdLArgs.Length - 1))
								break;
							cla.Topmost = BoolChecker(CmdLArgs[++i]);
							break;

						case "/st":
						case "/showintaskbar":
							if (i == (CmdLArgs.Length - 1))
								break;
							cla.ShowInTaskbar = BoolChecker(CmdLArgs[++i]);
							break;

						case "/f11e":
						case "/f11enabled":
							if (i == (CmdLArgs.Length - 1))
								break;
							cla.F11Enabled = BoolChecker(CmdLArgs[++i]);
							break;

						case "/f12e":
						case "/f12enabled":
							if (i == (CmdLArgs.Length - 1))
								break;
							cla.F12Enabled = BoolChecker(CmdLArgs[++i]);
							break;

						case "/cfe":
						case "/closefunc":
						case "/closefunction":
						case "/closefunctionenabled":
							if (i == (CmdLArgs.Length - 1))
								break;
							cla.CloseFunctionEnabled = BoolChecker(CmdLArgs[++i]);
							break;

						case "/bfe":
						case "/backfunc":
						case "/backfunction":
						case "/backfunctionenabled":
							if (i == (CmdLArgs.Length - 1))
								break;
							cla.BackFunctionEnabled = BoolChecker(CmdLArgs[++i]);
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
								cla.Height = tmp;
							break;

						case "/w":
						case "/wi":
						case "/wid":
						case "/widt":
						case "/width":
							if (i == (CmdLArgs.Length - 1))
								break;
							if (int.TryParse(CmdLArgs[++i], out tmp))
								cla.Width = tmp;
							break;

						case "/l":
						case "/le":
						case "/lef":
						case "/left"://ウィンドウ表示位置指定用
							if (i == (CmdLArgs.Length - 1))
								break;
							if (int.TryParse(CmdLArgs[++i], out tmp))
								cla.Left = tmp;
							break;
						case "/t":
						case "/to":
						case "/top":
							if (i == (CmdLArgs.Length - 1))
								break;
							if (int.TryParse(CmdLArgs[++i], out tmp))
								cla.Top = tmp;
							break;

						case "/nbbve":
						case "/nblockbve":
						case "/notblockbve":
							if (i == (CmdLArgs.Length - 1))
								break;
							cla.NotBlickBVE = BoolChecker(CmdLArgs[++i]) ?? cla.NotBlickBVE;
							break;

						case "/bvefn":
						case "/bvefname":
						case "/bveexefname":
						case "/bvefilename":
						case "/bveexefilename":
							if (i == (CmdLArgs.Length - 1))
								break;
							cla.BveExeFileName = CmdLArgs[++i];
							break;

						case "/bvepn":
						case "/bvepname":
						case "/bveprocessname":
							if (i == (CmdLArgs.Length - 1))
								break;
							cla.BveProcessName = CmdLArgs[++i];
							break;

						default://オプション設定の該当なし => モジュール確認

							//ファイルの存在確認はLoadDllInstがやってくれる.
							try
							{
								//先にセレクタを確認
								cla.selector_toRet = ModLoader.LoadDllInst<ISelector>(CmdLArgs[i]);
							}
							catch (FileNotFoundException)
							{
								continue;//ファイルがないなら次の引数へ
							}
							catch (MissingMethodException)
							{
								LoadAndSetIPagesInstance(cla, CmdLArgs[i]);
							}
							catch (EntryPointNotFoundException)//ISelectorではなかった
							{
								LoadAndSetIPagesInstance(cla, CmdLArgs[i]);
							}
							catch (Exception e)
							{
								System.Diagnostics.Debug.WriteLine(e);
							}
							break;
					}
				}
			}

			return cla;
		}

		static void LoadAndSetIPagesInstance(CLArgs cla, in string path)
		{
			try
			{
				cla.page_toShow = ModLoader.LoadDllInst<IPages>(path);
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
