using System.Windows.Controls;

using TR.BIDSSMemLib;

namespace caMon.pages.sample
{
	/// <summary>
	/// Page_BSMD.xaml の相互作用ロジック
	/// </summary>
	public partial class Page_BSMD : Page
	{

		BIDSData_toBind.BSMD_toBind bsmd2b = new BIDSData_toBind.BSMD_toBind();
		public Page_BSMD()
		{
			InitializeComponent();

			DataContext = bsmd2b;//Binding用

			SMemLib.SMC_BSMDChanged += SML_SMC_BSMDChanged;//BIDS Shared Memory Basic Dataが更新された際に実行される処理を登録する
		}

		/// <summary>BIDS Shared Memory Basic Dataが更新された際に実行されるように登録するメソッド</summary>
		/// <param name="sender">この関数の呼び出し元(通知するかは呼び出し元の実装次第)</param>
		/// <param name="e">実行に関連する情報が格納された引数</param>
		private void SML_SMC_BSMDChanged(object sender, TR.ValueChangedEventArgs<BIDSSharedMemoryData> e)
		{
			//MRPresというローカル変数に, 「処理完了後のMR圧(kPa)」を記録する処理
			float MRPres = e.NewValue.StateData.MR;

			//Speed_Oldというローカル変数に, 「処理実行前の速度(km/h)」を記録する処理
			float Speed_Old = e.OldValue.StateData.V;

			//上記二つに特に意味があるわけではない.

			bsmd2b.BSMD = e.NewValue;//表示データの更新
		}
	}
}
