using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

using TR.BIDSSMemLib;

namespace caMon
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : NavigationWindow
	{
    SMemLib SML;
    IPages e233sp_inst = null;
    IPages e235sp_inst = null;
		public MainWindow()
		{
			InitializeComponent();

      SharedFuncs.SMem_RStart();

      e233sp_inst = new pages.e233sp.caMonIF();
      e235sp_inst = new pages.e235sp.caMonIF();

			e233sp_inst.BackToHome += E233sp_inst_BackToHome;
			e235sp_inst.BackToHome += E235sp_inst_BackToHome;

      NavigationService.Navigate(e233sp_inst.FrontPage);
		}

    private void E235sp_inst_BackToHome(object sender, EventArgs e) => NavigationService.Navigate(e233sp_inst.FrontPage);

		private void E233sp_inst_BackToHome(object sender, EventArgs e) => NavigationService.Navigate(e235sp_inst.FrontPage);

    private void MainWindowHeadder_PreviewKeyUp(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.F11)
      {
        switch (MainWindowHeadder.WindowState)
        {
          case WindowState.Maximized:
            MainWindowHeadder.WindowState = WindowState.Normal;
            MainWindowHeadder.WindowStyle = WindowStyle.SingleBorderWindow;
            break;
          case WindowState.Normal:
            MainWindowHeadder.WindowStyle = WindowStyle.None;
            MainWindowHeadder.WindowState = WindowState.Maximized;
            break;
        }
      }
      if (e.Key == System.Windows.Input.Key.F12)
      {
        switch (MainWindowHeadder.WindowStyle)
        {
          case WindowStyle.None:
            MainWindowHeadder.WindowStyle = WindowStyle.SingleBorderWindow;
            break;
          case WindowStyle.SingleBorderWindow:
            MainWindowHeadder.WindowStyle = WindowStyle.None;
            break;
        }
      }
    }

		private void MainWindowHeadder_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
      SharedFuncs.SMem_RStop();
		}
	}
}
