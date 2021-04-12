using System.Windows;

namespace caMon
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		//ref : https://www.atmarkit.co.jp/fdotnet/dotnettips/879wpfapparg/wpfapparg.html
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			SharedFuncs.SMem_RStart();//コマンドライン引数のチェック前にSMemを起動させておく

			SharedFuncs.GetPageSampleModInstance ??= () => new pages.sample.samplePage();//サンプルmodのインスタンスを取得するためのメソッドを登録

			MainWindowSettings settings = new();
			settings.SetSettings(e.Args);//UI要素を表示させる前に引数チェック

			MainWindow = new MainWindow(settings);
			MainWindow.Show();
		}
	}
}
