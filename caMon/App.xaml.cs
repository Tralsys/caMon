using System.Windows;

namespace caMon
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		internal static string[] CmdLArgs = null;//コマンドライン引数のリスト

		//ref : https://www.atmarkit.co.jp/fdotnet/dotnettips/879wpfapparg/wpfapparg.html
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			if (e.Args.Length <= 0)
				return;//CmdLArgsが0以下になるなら, CmdLArgsへの情報格納は行わない

			CmdLArgs = new string[e.Args.Length];
			e.Args.CopyTo(CmdLArgs, 0);
		}
	}
}
