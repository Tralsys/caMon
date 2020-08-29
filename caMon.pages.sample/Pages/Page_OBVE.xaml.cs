using System.Windows.Controls;

namespace caMon.pages.sample
{
	/// <summary>
	/// Page_OBVE.xaml の相互作用ロジック
	/// </summary>
	public partial class Page_OBVE : Page
	{
		BIDSData_toBind.OBVED_toBind OtB = new BIDSData_toBind.OBVED_toBind();

		public Page_OBVE()
		{
			InitializeComponent();

			DataContext = OtB;//OpenDをBindingできるように

			SharedFuncs.SML.SMC_OpenDChanged += SML_SMC_OpenDChanged;//openBVE Dataが更新された際に実行される処理を登録する
		}

		/// <summary>openBVE Dataが更新された際に実行されるように登録するメソッド</summary>
		/// <param name="sender">この関数の呼び出し元(通知するかは呼び出し元の実装次第)</param>
		/// <param name="e">実行に関連する情報が格納された引数</param>
		private void SML_SMC_OpenDChanged(object sender, TR.ValueChangedEventArgs<OpenD> e) => OtB.OD = e.NewValue;
	}
}
