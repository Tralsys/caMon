using System;
using System.Windows.Controls;

namespace caMon
{
	/// <summary>
	/// ライブラリのインターフェイス定義
	/// </summary>
	public interface IPages : IDisposable
	{
		/// <summary>ホーム画面に戻る際に発火させるイベント</summary>
		event EventHandler BackToHome;

		/// <summary>アプリケーションを終了させる際に発火させるイベント</summary>
		event EventHandler CloseApp;

		/// <summary>Pageライブラリ内のトップページ</summary>
		Page FrontPage { get; }
	}
}
