using System;
using System.Windows.Controls;

namespace caMon.pages.BIDSDataUpdateNotifierUsecase
{
	/// <summary>
	/// CaMonIF.xaml の相互作用ロジック
	/// </summary>
	public partial class CaMonIF : Page, IPages
	{
		private bool disposedValue;

		public CaMonIF()
		{
			InitializeComponent();
		}

		#region IPages
		public Page FrontPage { get => this; }

		public event EventHandler BackToHome;
		public event EventHandler CloseApp;

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
		#endregion
	}
}
