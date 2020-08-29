using System.Windows.Controls;

namespace caMon.pages.sample
{
	/// <summary>
	/// Page_PanelData.xaml の相互作用ロジック
	/// </summary>
	public partial class Page_PanelData : Page
	{
		public Page_PanelData()
		{
			InitializeComponent();

			SharedFuncs.SML.SMC_PanelDChanged += SML_SMC_PanelDChanged;//Panel Dataが更新された際に実行される処理を登録する
		}

		/// <summary>Panel Dataが更新された際に実行されるように登録するメソッド</summary>
		/// <param name="sender">この関数の呼び出し元(通知するかは呼び出し元の実装次第)</param>
		/// <param name="e">実行に関連する情報が格納された引数</param>
		private void SML_SMC_PanelDChanged(object sender, TR.ValueChangedEventArgs<int[]> e)
		{
			
		}
	}
}
