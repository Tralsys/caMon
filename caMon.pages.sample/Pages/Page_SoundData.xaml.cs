using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using TR.BIDSSMemLib;

namespace caMon.pages.sample
{
	/// <summary>
	/// Page_ArrayData.xaml の相互作用ロジック
	/// </summary>
	public partial class Page_SoundData : Page
	{
		TextBlock[] ArrayElems = new TextBlock[256];
		TextBlock[] RowLabelTBs = new TextBlock[16];
		TextBlock[] ColLabelTBs = new TextBlock[16];

		public Page_SoundData()
		{
			InitializeComponent();

			#region 表示内容の準備
			//列に関するラベル
			for (int i = 0; i < ColLabelTBs.Length; i++)
			{
				ColLabelTBs[i] = new TextBlock()
				{
					Text = string.Format("0x_{0:X}", i),
					HorizontalAlignment = HorizontalAlignment.Center,
					VerticalAlignment = VerticalAlignment.Center
				};

				Grid.SetColumn(ColLabelTBs[i], i + 1);
				Grid.SetRow(ColLabelTBs[i], 0);

				ArrayDataShowGrid.Children.Add(ColLabelTBs[i]);
			}

			//行に関するラベル
			for (int i = 0; i < RowLabelTBs.Length; i++)
			{
				RowLabelTBs[i] = new TextBlock()
				{
					Text = string.Format("0x{0:X}_", i),
					HorizontalAlignment = HorizontalAlignment.Center,
					VerticalAlignment = VerticalAlignment.Center
				};

				Grid.SetColumn(RowLabelTBs[i], 0);
				Grid.SetRow(RowLabelTBs[i], i + 1);

				ArrayDataShowGrid.Children.Add(RowLabelTBs[i]);
			}

			//各要素のTextBlock
			for (int i = 0; i < ArrayElems.Length; i++)
			{
				ArrayElems[i] = new TextBlock()
				{
					Text = (SMemLib.SoundA.Length > i ? SMemLib.SoundA[i] : 0).ToString(),//初期値の設定
					HorizontalAlignment = HorizontalAlignment.Center,
					VerticalAlignment = VerticalAlignment.Center
				};

				Grid.SetColumn(ArrayElems[i], (i % (ArrayDataShowGrid.ColumnDefinitions.Count - 1)) + 1);
				Grid.SetRow(ArrayElems[i], (i / (ArrayDataShowGrid.ColumnDefinitions.Count - 1)) + 1);

				ArrayDataShowGrid.Children.Add(ArrayElems[i]);
			}
			#endregion

			SMemLib.SMC_SoundDChanged += SML_SMC_SoundDChanged;//Sound Dataが更新された際に実行される処理を登録する
		}

		/// <summary>Sound Dataが更新された際に実行されるように登録するメソッド</summary>
		/// <param name="sender">この関数の呼び出し元(通知するかは呼び出し元の実装次第)</param>
		/// <param name="e">実行に関連する情報が格納された引数</param>
		private void SML_SMC_SoundDChanged(object sender, TR.ValueChangedEventArgs<int[]> e)
		{
			if (e.OldValue.Length >= ArrayElems.Length && e.NewValue.Length >= ArrayElems.Length) //配列長は新旧ともArrayElems以上に長い.
			{
				Parallel.For(0, ArrayElems.Length, (i) =>
				{
					if (e.OldValue[i] != e.NewValue[i])//値の更新がある
						TBTextChanger(i, e.NewValue[i]);//表示更新
																						//更新がなければ何もしない.
				});
			}
			else if (e.NewValue.Length == e.OldValue.Length) //新旧ともSoundElem未満の配列長であり, 配列長は同じ
			{
				Parallel.For(0, e.NewValue.Length, (i) =>
				{
					if (e.OldValue[i] != e.NewValue[i])//値の更新がある
						TBTextChanger(i, e.NewValue[i]);//表示更新
																						//更新がなければ何もしない.
				});

				//配列長が短くなってないなら, 残りのArrayElemsは初期状態のままなはず.
			}
			else if (e.NewValue.Length < e.OldValue.Length) //新版の配列長が短い <=> 新版がSoundElemの配列長未満になり, 初期化する領域がある
			{
				Parallel.For(0, e.NewValue.Length, (i) =>
				{
					if (e.OldValue[i] != e.NewValue[i])//値の更新がある
						TBTextChanger(i, e.NewValue[i]);//表示更新
																						//更新がなければ何もしない.
				});

				//配列が短くなって初期化する必要が出てきた部分
				Parallel.For(e.NewValue.Length, Math.Min(ArrayElems.Length, e.OldValue.Length), (i) =>
				{
					ArrayElems[i].Text = "0";
				});
			}
			else //新版の配列が長い <=> 新版がSoundElemの配列長をオーバーし, 比較なしに値を入れる領域がある
			{
				Parallel.For(0, e.OldValue.Length, (i) =>
				{
					if (e.OldValue[i] != e.NewValue[i])//値の更新がある
						TBTextChanger(i, e.NewValue[i]);//表示更新
																						//更新がなければ何もしない.
				});

				Parallel.For(e.OldValue.Length, Math.Min(e.NewValue.Length, ArrayElems.Length), (i) =>
				{
					ArrayElems[i].Text = e.NewValue[i].ToString();//表示設定
				});
			}
		}

		private void TBTextChanger(int index, int value) => Dispatcher.Invoke(() => ArrayElems[index].Text = value.ToString());

	}
}
