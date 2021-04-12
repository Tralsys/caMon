using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

using Microsoft.WindowsAPICodePack.Dialogs;

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
		static readonly string MODS_DIRECTORY_ALT_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location), "mods");
		string using_mods_directory = MODS_DIRECTORY_ALT_PATH;
		bool ModsList_ListView_Setup_Init_Done = false;
		private bool disposedValue;

		void ModsList_ListView_Setup_Init()
		{
			if (ModsList_ListView_Setup_Init_Done)//実行済みならやらなくてOK
				return;

			ModsList_ListView_Setup_Init_Done = true;

			//ディレクトリが存在しないなら作る
			if (!Directory.Exists(MODS_DIRECTORY_ALT_PATH))
			{
				if (MessageBox.Show("modsフォルダが見つかりませんでした.  新規に作成しますか?\n作成するDirectoryのFullpath : " + MODS_DIRECTORY_ALT_PATH, "caMon.selector.default", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					Directory.CreateDirectory(MODS_DIRECTORY_ALT_PATH);

				//作成するしないに関わらずフォルダ選択は使用する
				ChooseCustomDirectory(null, null);
			}

		}

		bool CheckChooseOtherDirectory()
		{
			if (MessageBox.Show("指定されたフォルダに, 使用可能なファイルが見当たりませんでした.  別のフォルダを選択しますか?\nCurrentSearchingLocation : " + using_mods_directory, "caMon.selector.default", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				return ChooseCustomDirectory() && ModsList_ListView_SetUp();

			return false;//デフォルトは失敗

		}

		bool ModsList_ListView_SetUp()
		{
			ModsList_ListView_Setup_Init();

			//ref : https://dobon.net/vb/dotnet/file/getfiles.html
			string[] items;
			try
			{
				items = Directory.GetFiles(using_mods_directory + Path.DirectorySeparatorChar, "*.dll", SearchOption.TopDirectoryOnly);
			}catch(Exception e)
			{
				MessageBox.Show("ファイルリスト取得に失敗しました.\n" + e.Message, "caMon.selector.default");
				return false;
			}

			if (items.Length <= 0)
				return CheckChooseOtherDirectory();

			//ref : https://dobon.net/vb/dotnet/file/fileversion.html
			//ref : https://qiita.com/Kosen-amai/items/def339ea71cc69eeb9d0
			List<FileVersionInfo> fvil = new();
			foreach (var item in items)
			{
				var fv = FileVersionInfo.GetVersionInfo(item);
				if (fv is not null)
					fvil.Add(fv);
			}

			ModsList_ListView.DataContext = fvil;

			if (fvil.Count <= 0)
				return CheckChooseOtherDirectory();

			ModsList_ListView.SelectedIndex = 0;
			return true;
		}
		private bool ChooseCustomDirectory()
		{
			//ref : https://johobase.com/wpf-file-folder-common-dialog/
			CommonOpenFileDialog dig = new() { IsFolderPicker = true };

			if (dig.ShowDialog() == CommonFileDialogResult.Ok)
			{
				using_mods_directory = dig.FileName;
				return ModsList_ListView_SetUp();
			}

			return false;
		}
		private void ChooseCustomDirectory(object sender, RoutedEventArgs e) => ChooseCustomDirectory();

		/*ISelectorの実装*/

		public Page FrontPage => this;

		public event EventHandler BackToHome;
		public event EventHandler CloseApp;
		public event EventHandler<PageChangeEventArgs> PageChangeRequest;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					//Dispose managed object
				}

				disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void BackBtn_Click(object sender, RoutedEventArgs e) => BackToHome?.Invoke(this, null);

		private void CloseBtn_Click(object sender, RoutedEventArgs e) => CloseApp?.Invoke(this, null);

		private void LoadBtn_Click(object sender, RoutedEventArgs e)
		{
			SelectedPath = (ModsList_ListView.SelectedItem as FileVersionInfo)?.FileName;

			if (string.IsNullOrWhiteSpace(SelectedPath))
			{
				MessageBox.Show("modの指定がされていません.");
				return;
			}

			PageChangeRequest?.Invoke(this, new PageChangeEventArgs() { ModPath = SelectedPath });
		}

		private void ReloadList(object sender, RoutedEventArgs e) => ModsList_ListView_SetUp();

		private void OpenCustomFile(object sender, RoutedEventArgs e)
		{
			//ref : https://johobase.com/wpf-file-folder-common-dialog/
			var dig = new CommonOpenFileDialog();
			dig.Filters.Add(new CommonFileDialogFilter("*.dll", "*.dll"));
			if (dig.ShowDialog() == CommonFileDialogResult.Ok)
			{
				try
				{
					PageChangeRequest?.Invoke(this, new PageChangeEventArgs() { ModPath = dig.FileName });
				}catch(Exception ex)
				{
					MessageBox.Show("DLLの読み込みに失敗しました.\n" + ex.ToString(), "caMon.selector.default");
				}
			}
		}

		private void LoadSampleMod(object sender, RoutedEventArgs e) => PageChangeRequest?.Invoke(this, new PageChangeEventArgs() { NewPage = SharedFuncs.GetPageSampleModInstance?.Invoke() });
	}
}
