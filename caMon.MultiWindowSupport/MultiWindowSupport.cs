using System;
using System.Collections.Generic;

namespace caMon
{
	/// <summary>複数ウィンドウを開くためのクラスが持つインターフェイス</summary>
	public interface IMultiWindowSupporter
	{
		/// <summary>現在開いているウィンドウのリスト</summary>
		IReadOnlyList<ICaMonPageHostable> ReadOnlyWindowList { get; }

		/// <summary>Windowインスタンスを作成するのに使用するメソッド</summary>
		Func<ICaMonPageHostable>? WindowInstanceCreator { get; set; }

		/// <summary>指定したインスタンスのページを持つウィンドウを開きます</summary>
		/// <param name="page">ページインスタンス</param>
		/// <returns>ホストされたウィンドウ</returns>
		ICaMonPageHostable? OpenNewWindow(IPages page);

		/// <summary>ウィンドウを開きます</summary>
		/// <returns>ホストされたウィンドウ</returns>
		ICaMonPageHostable? OpenNewWindow();

		/// <summary>ウィンドウのインスタンスを作成します</summary>
		/// <returns>作成されたウィンドウインスタンス</returns>
		public ICaMonPageHostable? CreateNewWindow();

		/// <summary>指定のウィンドウを閉じます</summary>
		/// <param name="win">ウィンドウ</param>
		void CloseWindow(ICaMonPageHostable win);

		/// <summary>指定のページインスタンスを持つウィンドウを閉じます</summary>
		/// <param name="page">ページインスタンス</param>
		/// <returns>実行に成功したか</returns>
		bool CloseWindow(IPages page);
	}

	/// <summary>caMonのページをホスティングできるクラスが持つべきインターフェイス</summary>
	public interface ICaMonPageHostable
	{
		/// <summary>表示中のページ</summary>
		IPages ShowingPage { get; set; }

		/// <summary>ウィンドウを開きます</summary>
		void Show();

		/// <summary>ウィンドウを閉じます</summary>
		void Close();
	}
	
	/// <summary>複数ウィンドウを開くのに使用するクラス</summary>
	public class MultiWindowSupporter : IMultiWindowSupporter
	{
		/// <summary>現在のプロセスで使用できるMultiWindowSupporterインスタンス</summary>
		static public MultiWindowSupporter Current { get; } = new();

		/// <summary>でWindowインスタンスを作成するのに使用するメソッド</summary>
		public Func<ICaMonPageHostable>? WindowInstanceCreator { get; set; } = null;

		/// <summary>現在開いているウィンドウのリスト</summary>
		List<ICaMonPageHostable> WindowList { get; } = new();

		/// <summary>現在開いているウィンドウのリスト</summary>
		public IReadOnlyList<ICaMonPageHostable> ReadOnlyWindowList { get => WindowList; }

		/// <summary>指定のウィンドウを閉じます</summary>
		/// <param name="win">ウィンドウ</param>
		public void CloseWindow(ICaMonPageHostable win)
		{
			win.Close();
			WindowList.Remove(win);
		}

		/// <summary>指定のページインスタンスを持つウィンドウを閉じます</summary>
		/// <param name="page">ページインスタンス</param>
		/// <returns>実行に成功したか</returns>
		public bool CloseWindow(IPages page)
		{
			if (WindowList.Count <= 0)
				return false;

			foreach (var v in WindowList)
				if (v.ShowingPage == page)
				{
					WindowList.Remove(v);
					v.Close();
					return true;
				}

			return false;
		}

		/// <summary>指定したインスタンスのページを持つウィンドウを開きます</summary>
		/// <param name="page">ページインスタンス</param>
		/// <returns>ホストされたウィンドウ</returns>
		public ICaMonPageHostable? OpenNewWindow(IPages page)
		{
			var ret = CreateNewWindow();

			if (ret is not null)
			{
				ret.ShowingPage = page;
				ret.Show();
			}

			return ret;
		}

		/// <summary>ウィンドウを開きます</summary>
		/// <returns>ホストされたウィンドウ</returns>
		public ICaMonPageHostable? OpenNewWindow()
		{
			var ret = CreateNewWindow();
			ret?.Show();
			return ret;
		}

		/// <summary>ウィンドウのインスタンスを作成します</summary>
		/// <returns>作成されたウィンドウインスタンス</returns>
		public ICaMonPageHostable? CreateNewWindow()
		{
			var ret = WindowInstanceCreator?.Invoke();
			if (ret is null)
				return null;

			WindowList.Add(ret);

			return ret;
		}
	}
}
