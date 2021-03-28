using System;
using System.Windows;
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

		private void SaveSetting(object sender, RoutedEventArgs e)
		{
			try
			{
				(DataContext as ATSPLamps)?.SaveIndexesToXML();
			}catch(Exception ex)
			{
				MessageBox.Show(ex.ToString(), typeof(ATSP).FullName);
			}
			ActionCompletedLabel.Visibility = Visibility.Visible;
		}
		private void LoadSetting(object sender, RoutedEventArgs e)
		{
			try
			{
				(DataContext as ATSPLamps)?.LoadIndexesFromXML();
			}catch(Exception ex)
			{
				MessageBox.Show(ex.ToString(), typeof(ATSP).FullName);
			}
			ActionCompletedLabel.Visibility = Visibility.Visible;
		}

		private void ClickToHideDo(object sender, System.Windows.Input.MouseButtonEventArgs e) => (sender as UIElement).Visibility = Visibility.Collapsed;
	}
}
