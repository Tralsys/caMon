using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace caMon.selector.default_
{
	/// <summary>
	/// SelectPage.xaml の相互作用ロジック
	/// </summary>
	public partial class SelectPage : Page, ISelector
	{
		string SelectedPath = string.Empty;

		public SelectPage() => InitializeComponent();
		
		private void Page_Loaded(object sender, RoutedEventArgs e) => ModsList_ListView_SetUp();//初期化されても表示されない可能性があるため

		void ModsList_ListView_SetUp()
		{
			//ListViewの初期化
			ModsList_ListView.Items.Clear();

			//ディレクトリが存在しないなら作る.
			Directory.CreateDirectory(@"mods");


			//ref : https://dobon.net/vb/dotnet/file/getfiles.html
			var items = Directory.GetFiles(@"mods\", "*.dll", SearchOption.TopDirectoryOnly);

			if(items.Length<=0)
			{
				Task.Run(() => MessageBox.Show("modがmodsフォルダに見当たりませんでした."));//ウィンドウは確実に表示する.
				return;
			}

			//ref : https://dobon.net/vb/dotnet/file/fileversion.html
			//ref : https://qiita.com/Kosen-amai/items/def339ea71cc69eeb9d0
			List<FileVersionInfo> fvil = new List<FileVersionInfo>();
			foreach (var item in items)
			{
				var fv = FileVersionInfo.GetVersionInfo(item);
				if (fv != null)
					fvil.Add(fv);
			}

			ModsList_ListView.DataContext = fvil;

			if (fvil.Count > 0)
				ModsList_ListView.SelectedIndex = 0;
			else
				Task.Run(() => MessageBox.Show("modがmodsフォルダに見当たりませんでした."));//ウィンドウは確実に表示する.
		}


		/*ISelectorの実装*/

		public Page FrontPage => this;

		public event EventHandler BackToHome;
		public event EventHandler CloseApp;
		public event EventHandler<PageChangeEventArgs> PageChangeRequest;

		public void Dispose()
		{
		}

		private void BackBtn_Click(object sender, RoutedEventArgs e) => BackToHome?.Invoke(this, null);

		private void CloseBtn_Click(object sender, RoutedEventArgs e) => CloseApp?.Invoke(this, null);

		private void LoadBtn_Click(object sender, RoutedEventArgs e)
		{
			SelectedPath = (ModsList_ListView.SelectedItem as FileVersionInfo).FileName;

			if (string.IsNullOrWhiteSpace(SelectedPath))
			{
				MessageBox.Show("modの指定がされていません.");
				return;
			}

			PageChangeRequest?.Invoke(this, new PageChangeEventArgs() { ModPath = SelectedPath });
		}
	}
}
