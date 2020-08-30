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
			public ISelector selector_toRet = null;
			public IPages page_toShow = null;

			public WindowState WindowState = WindowState.Normal;
			public WindowStyle WindowStyle = WindowStyle.SingleBorderWindow;
			public WindowStartupLocation WindowStartupLocation = WindowStartupLocation.Manual;
			public ResizeMode ResizeMode = ResizeMode.CanResize;

			public bool Topmost = false;
			public bool ShowInTaskbar = true;

			public bool F11Enabled = true;
			public bool F12Enabled = true;

			public bool CloseFunctionEnabled = true;
			public bool BackFunctionEnabled = true;

			public int Height = 400;
			public int Width = 600;
			public int Left = 20;
			public int Top = 40;
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

					switch (App.CmdLArgs[i].ToLower())
					{
						case "/wa":
						case "/wsta":
						case "/wstat":
						case "/wstate":
							try
							{
								cla.WindowState = App.CmdLArgs[i + 1].ToLower() switch
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

									_ => throw new NotImplementedException()
								};
								i++;//無事に実行できたらインクリメント
							}
							catch (Exception e)
							{
								Console.WriteLine("WState CAOption : {0}", e);
							}
							break;

						case "/wy":
						case "/wsty":
						case "/wstyl":
						case "/wstyle":
							try
							{
								cla.WindowStyle = App.CmdLArgs[i + 1].ToLower() switch
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

									_ => throw new NotImplementedException()
								};
								i++;
							}
							catch (Exception e)
							{
								Console.WriteLine("WStyle CAOption : {0}", e);
							}
							break;

						case "/wl":
						case "/wstl":
						case "/wstlo":
						case "/wstloc":
						case "/windowstartuplocation":
							try
							{
								cla.WindowStartupLocation = App.CmdLArgs[i + 1].ToLower() switch
								{
									"co"=>WindowStartupLocation.CenterOwner,
									"cs"=>WindowStartupLocation.CenterScreen,
									"mn"=>WindowStartupLocation.Manual,

									"centerowner" => WindowStartupLocation.CenterOwner,
									"centerscreen" => WindowStartupLocation.CenterScreen,
									"manual" => WindowStartupLocation.Manual,

									_ => throw new NotImplementedException()
								};
								i++;
							}
							catch(Exception e)
							{
								Console.WriteLine("Height CAOption : {0}", e);
							}
							break;

						case "/rm":
						case "/resizemode":
							try
							{
								cla.ResizeMode = App.CmdLArgs[i + 1].ToLower() switch
								{
									"no" => ResizeMode.NoResize,
									"mn" => ResizeMode.CanMinimize,
									"rs" => ResizeMode.CanResize,
									"rg" => ResizeMode.CanResizeWithGrip,

									"noresize" => ResizeMode.NoResize,
									"canminimize" => ResizeMode.CanMinimize,
									"canresize" => ResizeMode.CanResize,
									"canresizewithgrip" => ResizeMode.CanResizeWithGrip,

									_ => throw new NotImplementedException()
								};
								i++;
							}
							catch(Exception e)
							{
								Console.WriteLine("ResizeMode CAOption : {0}", e);
							}
							break;

						case "/tm":
						case "/topmost":
							try
							{
								cla.Topmost = BoolChecker(App.CmdLArgs[i + 1]) ?? throw new NotImplementedException();
								i++;
							}
							catch (Exception e)
							{
								Console.WriteLine("TopMost CAOption : {0}", e);
							}
							break;

						case "/st":
						case "/showintaskbar":
							try
							{
								cla.ShowInTaskbar = BoolChecker(App.CmdLArgs[i + 1]) ?? throw new NotImplementedException();
								i++;
							}
							catch (Exception e)
							{
								Console.WriteLine("ShowInTaskbar CAOption : {0}", e);
							}
							break;

						case "/f11e":
						case "/f11enabled":
							try
							{
								cla.F11Enabled = BoolChecker(App.CmdLArgs[i + 1]) ?? throw new NotImplementedException();
								i++;
							}
							catch (Exception e)
							{
								Console.WriteLine("F11Enabled CAOption : {0}", e);
							}
							break;

						case "/f12e":
						case "/f12enabled":
							try
							{
								cla.F12Enabled = BoolChecker(App.CmdLArgs[i + 1]) ?? throw new NotImplementedException();
								i++;
							}
							catch (Exception e)
							{
								Console.WriteLine("F12Enabled CAOption : {0}", e);
							}
							break;

						case "/cfe":
						case "/closefunc":
						case "/closefunction":
						case "/closefunctionenabled":
							try
							{
								cla.CloseFunctionEnabled = BoolChecker(App.CmdLArgs[i + 1]) ?? throw new NotImplementedException();
								i++;
							}
							catch (Exception e)
							{
								Console.WriteLine("CloseFunctionEnabled CAOption : {0}", e);
							}
							break;

						case "/bfe":
						case "/backfunc":
						case "/backfunction":
						case "/backfunctionenabled":
							try
							{
								cla.BackFunctionEnabled = BoolChecker(App.CmdLArgs[i + 1]) ?? throw new NotImplementedException();
								i++;
							}
							catch (Exception e)
							{
								Console.WriteLine("BackFunctionEnabled CAOption : {0}", e);
							}
							break;


						case "/h":
						case "/he":
						case "/hei":
						case "/heig":
						case "/heigh":
						case "/height":
							try
							{
								cla.Height = int.Parse(App.CmdLArgs[i + 1]);
								i++;
							}
							catch(Exception e)
							{
								Console.WriteLine("Height CAOption : {0}", e);
							}
							break;

						case "/w":
						case "/wi":
						case "/wid":
						case "/widt":
						case "/width":
							try
							{
								cla.Width = int.Parse(App.CmdLArgs[i + 1]);
								i++;
							}
							catch (Exception e)
							{
								Console.WriteLine("Width CAOption : {0}", e);
							}
							break;

						case "/l":
						case "/le":
						case "/lef":
						case "/left"://ウィンドウ表示位置指定用
							try
							{
								cla.Left = int.Parse(App.CmdLArgs[i + 1]);
								i++;
							}
							catch (Exception e)
							{
								Console.WriteLine("Left CAOption : {0}", e);
							}
							break;
						case "/t":
						case "/to":
						case "/top":
							try
							{
								cla.Top = int.Parse(App.CmdLArgs[i + 1]);
								i++;
							}
							catch (Exception e)
							{
								Console.WriteLine("Top CAOption : {0}", e);
							}
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
