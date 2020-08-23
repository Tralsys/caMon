using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

using TR;

namespace caMon.pages.e233sp
{
	/// <summary>
	/// e233.xaml の相互作用ロジック
	/// </summary>
	public partial class e233 : Page
	{
		//Brush black = new SolidColorBrush(Colors.Black);
		//Brush white = new SolidColorBrush(Colors.White);
		DispatcherTimer timer = new DispatcherTimer();
		int timerInterval = 300;
		Point poi16 = new Point();
		Point poi2 = new Point();
		Point poi3 = new Point();
		Point poi4 = new Point();
		Point poi5 = new Point();
		int TimeOld = 0;
		double BCMar = 0;
		double MRMar = 0;
		double poin2 = 0;
		double spang = 0;
		caMonIF camonIF;
		public e233(caMonIF arg_camonIF)
		{
			InitializeComponent();

			camonIF = arg_camonIF;

			SharedFuncs.SML.SMC_BSMDChanged += SMemLib_BIDSSMemChanged;
			
			poi16 = new Point(0, 0);
			poi5 = new Point(0, 30);
			SPNum.Content = "0";
			EBRec.Width = 200;
			EBText.Width = 120;
			BCPres.Height = 384;//0.96*Pres
			SPHari.Angle = -36;
			//BCHari 400で175(三角部分はX=20,Y=30)
			//0で105 つまり差は200で35。Margin0(254,940),margin400(254,556)=>差は384。

			timer.Tick += Timer_Tick;
			timer.Interval = new TimeSpan(0, 0, 0, 0, timerInterval);
			timer.Start();

		}

		bool BIDSSMemIsEnabled = false;
		float SpeedAbsVal = float.NaN;
		float BCPresVal = float.NaN;
		float MRPresVal = float.NaN;
		int BNumVal = -1;
		int BMaxVal = -1;
		int EBPosVal = -1;
		int ATSCheckBPosVal = -1;
		int TimeVal = -1;
		private void SMemLib_BIDSSMemChanged(object sender, ValueChangedEventArgs<BIDSSharedMemoryData> e)
		{
			BIDSSMemIsEnabled = e.NewValue.IsEnabled;

			SpeedAbsVal = Math.Abs(e.NewValue.StateData.V);
			BCPresVal = e.NewValue.StateData.BC;
			MRPresVal = e.NewValue.StateData.MR;

			BNumVal = e.NewValue.HandleData.B;
			BMaxVal = e.NewValue.SpecData.B;
			EBPosVal = BMaxVal + 1;

			ATSCheckBPosVal = e.NewValue.SpecData.A;


			TimeVal = e.NewValue.StateData.T;
		}

		private void Timer_Tick(object sender, object e)
		{

			if (!BIDSSMemIsEnabled)
			{
				SpeedAbsVal = 0;
				BCPresVal = 0;
				MRPresVal = 700;
			}

			if (TimeVal < TimeOld)
				Task.Delay(10);

			TimeOld = TimeVal;

			if (Smooth.IsChecked == true && timerInterval == 300)
			{
				timerInterval = 0;
				timerStart();
			}
			if (Smooth.IsChecked == false && timerInterval != 300)
			{
				timerInterval = 300;
				timerStart();
			}

			BNum();

			int bcPresNum = BCPresDigi(BCPresVal);
			int mrPresNum = MRPresDigi(MRPresVal);

			SPNum.Content = ((int)SpeedAbsVal).ToString();//速度 文字表示

			if (Smooth.IsChecked == true)
			{
				BCPres.Height = BCPresVal * 0.96;
				MRMar = MRPresVal - 700;
				spang = (SpeedAbsVal * 1.8) - 36;
				poin2 = BCPresVal;

			}
			if (Smooth.IsChecked == false)
			{
				BCPres.Height = bcPresNum * 0.96;
				MRMar = mrPresNum - 700;
				spang = (((int)SpeedAbsVal) * 1.8) - 36;
				poin2 = bcPresNum;
			}

			SPHari.Angle = spang;
			poin2 = (poin2 * 0.175) + 105;

			double poin3 = poin2 + 20;

			poi2 = new Point((int)poin2, 0);
			poi3 = new Point((int)poin3, 15);
			poi4 = new Point((int)poin2, 30);

			BCMar = BCPres.Height + 110;

			PointCollection bcpoi = new PointCollection();
			MRMar = (MRMar * 2.55) + 112;

			bcpoi.Add(poi16);
			bcpoi.Add(poi2);
			bcpoi.Add(poi3);
			bcpoi.Add(poi4);
			bcpoi.Add(poi5);
			bcpoi.Add(poi16);

			BCHari.Points = bcpoi;
			BCHari.Margin = new Thickness(254, 0, 0, BCMar);
			MRHari.Margin = new Thickness(671, 0, 0, MRMar);

			Task.Delay(10);

			Cover.Visibility = Visibility.Collapsed;
		}


		private void timerStart()
		{
			timer.Stop();
			timer.Interval = new TimeSpan(0, 0, 0, 0, timerInterval);
			timer.Start();

		}

		private int BCPresDigi(double pres)
		{
			if (pres > 800) return 800;
			return (int)Math.Floor(pres / 20) * 20;
		}
		private int MRPresDigi(double pres)
		{
			if (pres < 700) return 700;
			if (pres > 1000) return 1000;
			return (int)Math.Floor(pres / 5) * 5;
		}

		int BNum_numRec = -1;
		int BNum_numMaxRec = -1;
		int BNum_numMaxRecP1 = -1;
		private void BNum()
		{
			//最大段+1=EB?=>B最大段-ATS確認+1で段数
			//ATS確認が「2」なら抑速あり。
			//EBRec=200
			//EBText=120
			//丸付きブレーキHeight=76
			//丸なしブレーキHeight=54
			//下地ブレーキHeight=54

			int numMax = BMaxVal + 1 - ATSCheckBPosVal;
			if (BNum_numMaxRec != numMax)
			{
				BNum_numMaxRec = numMax;
				BNum_numMaxRecP1 = BNum_numMaxRec + 1;
				BBase(numMax);

				BCover.Visibility = (numMax > 8 || numMax == 0) ? Visibility.Visible : Visibility.Collapsed;
			}
			int num = BNumVal + 1 - ATSCheckBPosVal;
			if (BNum_numRec != num)
			{
				BNum_numRec = num;

				BRec(num);

				BElapText(num);

				EBRec.Visibility = EBText.Visibility = BNumVal == EBPosVal ? Visibility.Visible : Visibility.Collapsed;
			}
		}

		/// <summary>BBaseで使用する色のひとつ.  BPos表示で最大段以外に適用される色</summary>
		static readonly SolidColorBrush BBase_SCB02 = new SolidColorBrush(Color.FromRgb(0xDD, 0xDD, 0));
		/// <summary>BBaseで使用する色のひとつ.  BPos表示で最大段に適用される色</summary>
		static readonly SolidColorBrush BBase_SCB01 = new SolidColorBrush(Color.FromRgb(0xFF, 0xAA, 0x66));
		/// <summary>ブレーキハンドル位置表示のベースを設定する</summary>
		/// <param name="num"></param>
		private void BBase(int num)
		{
			B1B.Visibility = num >= 1 ? Visibility.Visible : Visibility.Collapsed;
			B2B.Visibility = num >= 2 ? Visibility.Visible : Visibility.Collapsed;
			B3B.Visibility = num >= 3 ? Visibility.Visible : Visibility.Collapsed;
			B4B.Visibility = num >= 4 ? Visibility.Visible : Visibility.Collapsed;
			B5B.Visibility = num >= 5 ? Visibility.Visible : Visibility.Collapsed;
			B6B.Visibility = num >= 6 ? Visibility.Visible : Visibility.Collapsed;
			B7B.Visibility = num >= 7 ? Visibility.Visible : Visibility.Collapsed;
			B8B.Visibility = num >= 8 ? Visibility.Visible : Visibility.Collapsed;

			B1E.Fill = num == 1 ? BBase_SCB01 : BBase_SCB02;
			B2E.Fill = num == 2 ? BBase_SCB01 : BBase_SCB02;
			B3E.Fill = num == 3 ? BBase_SCB01 : BBase_SCB02;
			B4E.Fill = num == 4 ? BBase_SCB01 : BBase_SCB02;
			B5E.Fill = num == 5 ? BBase_SCB01 : BBase_SCB02;
			B6E.Fill = num == 6 ? BBase_SCB01 : BBase_SCB02;
			B7E.Fill = num == 7 ? BBase_SCB01 : BBase_SCB02;
			B8E.Fill = num == 8 ? BBase_SCB01 : BBase_SCB02;

		}
		private void BRec(int num)
		{
			B1R.Visibility = (0 < num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B2R.Visibility = (1 < num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B3R.Visibility = (2 < num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B4R.Visibility = (3 < num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B5R.Visibility = (4 < num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B6R.Visibility = (5 < num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B7R.Visibility = (6 < num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B8R.Visibility = (7 < num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
		}

		/// <summary>ブレーキハンドル位置表示の円形部分を司る</summary>
		/// <param name="num">ハンドル位置</param>
		private void BElapText(int num)
		{
			B1E.Visibility = B1T.Visibility = (1 == num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B2E.Visibility = B2T.Visibility = (2 == num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B3E.Visibility = B3T.Visibility = (3 == num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B4E.Visibility = B4T.Visibility = (4 == num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B5E.Visibility = B5T.Visibility = (5 == num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B6E.Visibility = B6T.Visibility = (6 == num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B7E.Visibility = B7T.Visibility = (7 == num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B8E.Visibility = B8T.Visibility = (8 == num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
		}

		private void NextP(object sender, RoutedEventArgs e) => camonIF.BackToHomeDo();
	}

}
