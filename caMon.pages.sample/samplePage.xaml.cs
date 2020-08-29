using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace caMon.pages.sample
{
	/// <summary>
	/// samplePage.xaml の相互作用ロジック
	/// </summary>
	public partial class samplePage : Page, IPages
	{
		public samplePage()
		{
			InitializeComponent();
		}

		public Page FrontPage => this;

		public event EventHandler BackToHome;
		public event EventHandler CloseApp;

		public void Dispose()
		{
		}

		private void CloseDo(object sender, RoutedEventArgs e) => CloseApp?.Invoke(this, null);

		private void BackToHomeDo(object sender, RoutedEventArgs e) => BackToHome?.Invoke(this, null);

		private void Page_BSMD_Show(object sender, RoutedEventArgs e) => Frame_toShow.Source = new Uri(@"Pages\Page_BSMD.xaml", UriKind.Relative);
		private void Page_Ctrler_Show(object sender, RoutedEventArgs e) => Frame_toShow.Source = new Uri(@"Pages\Page_Ctrler.xaml", UriKind.Relative);
		private void Page_OBVE_Show(object sender, RoutedEventArgs e) => Frame_toShow.Source = new Uri(@"Pages\Page_OBVE.xaml", UriKind.Relative);
		private void Page_PanelData_Show(object sender, RoutedEventArgs e) => Frame_toShow.Source = new Uri(@"Pages\Page_PanelData.xaml", UriKind.Relative);
		private void Page_SoundData_Show(object sender, RoutedEventArgs e) => Frame_toShow.Source = new Uri(@"Pages\Page_SoundData.xaml", UriKind.Relative);
	}
}
