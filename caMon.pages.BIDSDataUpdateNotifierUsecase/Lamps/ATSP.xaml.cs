using System.Windows.Controls;

using BIDSDataUpdateNotifier.LampStateProvider;

namespace caMon.pages.BIDSDataUpdateNotifierUsecase.Lamps
{
	/// <summary>
	/// ATSP.xaml の相互作用ロジック
	/// </summary>
	public partial class ATSP : UserControl
	{
		public ATSP()
		{
			DataContext = new ATSPLamps();
			InitializeComponent();
		}
	}
}
