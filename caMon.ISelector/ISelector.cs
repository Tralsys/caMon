using System;

namespace caMon
{
	public interface ISelector : IPages
	{
		/// <summary>別ページへの推移を要求するイベント</summary>
		event EventHandler<PageChangeEventArgs> PageChangeRequest;
	}

	/// <summary>選択画面から実画面を表示するためのイベントで使用する引数のクラス</summary>
	public class PageChangeEventArgs : EventArgs
	{
		/// <summary>次に表示するページ</summary>
		public IPages NewPage;

		/// <summary>次に表示するページが実装されたmodファイルへのパス</summary>
		public string ModPath;
	}
}
