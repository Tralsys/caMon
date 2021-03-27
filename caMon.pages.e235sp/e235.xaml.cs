using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

using TR;
using TR.BIDSSMemLib;

namespace caMon.pages.e235sp
{
	/// <summary>
	/// e235.xaml の相互作用ロジック
	/// </summary>
	public partial class e235 : Page
	{
		DispatcherTimer timer = new DispatcherTimer();
		int timeOld = 0;
		caMonIF camonIF;
		public e235(caMonIF arg_camonIF)
		{
			InitializeComponent();

			camonIF = arg_camonIF;

			SMemLib.SMC_BSMDChanged += SMemLib_BIDSSMemChanged;

			timer.Tick += Timer_Tick;
			timer.Interval = new TimeSpan(0, 0, 0, 0, 40);
			timer.Start();
		}

		bool BIDSSMemIsEnabled = false;
		float SpeedVal = 0;
		float BCPresVal = 0;
		float MRPresVal = 0;
		int BNumVal = 0;
		int BMaxVal = 0;
		int ATSCheckBPosVal = 0;
		int EBPosVal = -1;
		int TimeVal = -1;

		double LocationVal = 0;
		float CurrentVal = 0;
		int ReverserVal = 0;
		bool IsDoorClosed = true;
		private void SMemLib_BIDSSMemChanged(object sender, ValueChangedEventArgs<BIDSSharedMemoryData> e)
		{
			BIDSSMemIsEnabled = e.NewValue.IsEnabled;

			SpeedVal = e.NewValue.StateData.V;
			BCPresVal = e.NewValue.StateData.BC;
			MRPresVal = e.NewValue.StateData.MR;

			BNumVal = e.NewValue.HandleData.B;
			BMaxVal = e.NewValue.SpecData.B;
			EBPosVal = BMaxVal + 1;

			ATSCheckBPosVal = e.NewValue.SpecData.A;

			TimeVal = e.NewValue.StateData.T;

			LocationVal = e.NewValue.StateData.Z;
			CurrentVal = e.NewValue.StateData.I;
			ReverserVal = e.NewValue.HandleData.R;
			IsDoorClosed = e.NewValue.IsDoorClosed;
		}


		double deltaT = 0;
		double OldLocation = 0;
		double deltaL = 0;
		/// <summary>位置情報ベースの速度[m/s]</summary>
		double RealSpeed = 0;
		bool IsFirstTimeToShow = true;
		private void Timer_Tick(object sender, EventArgs e)
		{
			if (!BIDSSMemIsEnabled)
			{
				SpeedVal = 0;
				BCPresVal = 0;
				MRPresVal = 700;
			}

			if (timeOld < TimeVal && OldLocation > (LocationVal - 1000) && OldLocation < (LocationVal + 1000))
			{
				deltaT = (double)(TimeVal - timeOld) / 1000;
				deltaL = LocationVal - OldLocation;
				RealSpeed = deltaL / deltaT;
				Lamps();
				BNum();
				CurrentDisp();
				BreakDisp();
				SpeedDisp();
				Clock();
			}

			timeOld = TimeVal;
			OldLocation = LocationVal;

			if (IsFirstTimeToShow)
			{
				Task.Delay(10);
				Cover.Visibility = Visibility.Collapsed;
				IsFirstTimeToShow = false;
			}
		}

		int KutenTimes = 0;
		int KassoTimes = 0;
		int RevOld = 999;
		private void Lamps()
		{
			//事故表示灯
			Accident.Visibility = BIDSSMemIsEnabled ? Visibility.Collapsed : Visibility.Collapsed;

			//空転
			if ((Math.Abs(RealSpeed) * 3.6 + 25) < Math.Abs(SpeedVal))
			{
				KutenTimes++;
				Kuten.Visibility = Visibility.Visible;

				KutenLab.Visibility = KutenTimes <= 8 ? Visibility.Visible : Visibility.Hidden;

				if (KutenTimes > 16)
					KutenTimes = 0;
			}
			else
			{
				Kuten.Visibility = KutenLab.Visibility = Visibility.Collapsed;
				KutenTimes = 0;
			}

			//滑走
			if ((Math.Abs(RealSpeed) * 3.6 - 25) > Math.Abs(SpeedVal))
			{
				KassoTimes++;
				Kasso.Visibility = Visibility.Visible;

				KassoLab.Visibility = KassoTimes <= 8 ? Visibility.Visible : Visibility.Collapsed;

				if (KassoTimes > 16)
					KassoTimes = 0;
				
			}
			else
			{
				Kasso.Visibility = KassoLab.Visibility = Visibility.Collapsed;
				KassoTimes = 0;
			}

			//後進
			Backgo.Visibility = BackgoLab.Visibility = SpeedVal < 0 ? Visibility.Visible : Visibility.Collapsed;

			//回生停止
			Kaisei.Visibility = KaiseiLab.Visibility = (CurrentVal == 0 && RealSpeed != 0 && BCPresVal > 0) ? Visibility.Visible : Visibility.Collapsed;

			//レバーサー位置表示
			if (ReverserVal != RevOld)
			{
				RevOld = ReverserVal;
				Forward.Visibility = ForwardLab.Visibility = ReverserVal > 0 ? Visibility.Visible:Visibility.Collapsed;
				Neutral.Visibility = NeutralLab.Visibility = ReverserVal == 0 ? Visibility.Visible : Visibility.Collapsed;
				Back.Visibility = BackLab.Visibility = ReverserVal < 0 ? Visibility.Visible : Visibility.Collapsed;
			}

			//ドア状態表示
			Door.Visibility = DoorLab.Visibility = IsDoorClosed ? Visibility.Visible : Visibility.Collapsed;
		}

		float CurrentValRec = float.NaN;
		private void CurrentDisp()
		{
			if (CurrentValRec == CurrentVal) return;
			CurrentValRec = CurrentVal;

			Current.Angle = CurrentVal * 0.135;
		}

		float BCPresValRec = float.NaN;
		float MRPresValRec = float.NaN;
		private void BreakDisp()
		{
			if (BCPresValRec != BCPresVal) BCHari.Angle = BCPresVal * 0.27 - 135;
			if(MRPresValRec!=MRPresVal) MRHari.Angle = MRPresVal * 0.27 - 135;

			BCPresValRec = BCPresVal;
			MRPresValRec = MRPresVal;
		}

		float SpeedValRec = float.NaN;
		private void SpeedDisp()
		{
			if (SpeedValRec == SpeedVal) return;
			SpeedValRec = SpeedVal;

			SPHari.Angle = (Math.Abs(SpeedVal) * 1.8) - 126;
			Speed.Content = Math.Abs((int)SpeedVal);
		}
		private void Clock()
		{
			TimeSpan ts = new TimeSpan(0, 0, (int)(TimeVal / 1000));
			Second.Angle = ts.Seconds * 6;
			Minute.Angle = ts.Minutes * 6 + (int)(ts.Seconds / 10);
			Hour.Angle = ts.Hours * 30 + ((int)(ts.Minutes / 10)) * 5;
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

		/// <summary>BBaseで使用する色のひとつ.  BPos表示で最大段に適用される色</summary>
		static readonly SolidColorBrush BBase_SCB01 = new SolidColorBrush(Color.FromRgb(240, 170, 40));
		/// <summary>BBaseで使用する色のひとつ.  BPos表示で最大段以外に適用される色</summary>
		static readonly SolidColorBrush BBase_SCB02 = new SolidColorBrush(Color.FromRgb(240, 200, 40));
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

		private void BElapText(int num)
		{
			B1E.Visibility = B1L.Visibility = (1 == num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B2E.Visibility = B2L.Visibility = (2 == num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B3E.Visibility = B3L.Visibility = (3 == num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B4E.Visibility = B4L.Visibility = (4 == num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B5E.Visibility = B5L.Visibility = (5 == num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B6E.Visibility = B6L.Visibility = (6 == num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B7E.Visibility = B7L.Visibility = (7 == num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
			B8E.Visibility = B8L.Visibility = (8 == num && num != BNum_numMaxRecP1) ? Visibility.Visible : Visibility.Collapsed;
		}

		private void NextP(object sender, RoutedEventArgs e) => camonIF.BackToHomeDo();
	}
}
