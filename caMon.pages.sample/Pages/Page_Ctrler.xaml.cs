using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

using TR.BIDSSMemLib;

namespace caMon.pages.sample
{
	/// <summary>
	/// Page_Ctrler.xaml の相互作用ロジック
	/// </summary>
	public partial class Page_Ctrler : Page
	{
		public Page_Ctrler()
		{
			DataContext = new CtrlerDataClass();
			InitializeComponent();
		}

		private void TextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				(sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
		}
	}

	public class CtrlerDataClass : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		void OnPropertyChanged(in string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

		public CtrlerDataClass()
		{
			RevPosDic.Add(ReverserPosition.F, "Forward");
			RevPosDic.Add(ReverserPosition.N, "Neutral");
			RevPosDic.Add(ReverserPosition.R, "Backward");
			var handd = CtrlInput.GetHandD();
			_Brake = handd.B;
			_Power = handd.P;
			_Reverser = handd.R switch { -1 => ReverserPosition.R, 0 => ReverserPosition.N, 1 => ReverserPosition.F, _ => ReverserPosition.N };
		}

		private double _BrakePos;
		public double BrakePos
		{
			get => _BrakePos;
			set
			{
				CtrlInput.SetHandD(CtrlInput.HandType.BPos, value);
				_BrakePos = value;
				OnPropertyChanged(nameof(BrakePos));
			}
		}

		private double _PowerPos;
		public double PowerPos
		{
			get => _PowerPos;
			set
			{
				CtrlInput.SetHandD(CtrlInput.HandType.PPos, value);
				_PowerPos = value;
				OnPropertyChanged(nameof(PowerPos));
			}
		}

		private int _Brake;
		public int Brake
		{
			get => _Brake;
			set
			{
				CtrlInput.SetHandD(CtrlInput.HandType.Brake, value);
				_Brake = value;
				OnPropertyChanged(nameof(Brake));
			}
		}

		private int _Power;
		public int Power
		{
			get => _Power;
			set
			{
				CtrlInput.SetHandD(CtrlInput.HandType.Power, value);
				_Power = value;
				OnPropertyChanged(nameof(Power));
			}
		}

		public enum ReverserPosition
		{
			N, F, R
		}

		private ReverserPosition _Reverser = ReverserPosition.N;
		public ReverserPosition Reverser
		{
			get => _Reverser;
			set
			{
				CtrlInput.SetHandD(CtrlInput.HandType.Reverser,
					value switch
					{
						ReverserPosition.F => 1,
						ReverserPosition.N => 0,
						ReverserPosition.R => -1,
						_ => 0
					});
				_Reverser = value;
				OnPropertyChanged(nameof(Reverser));
			}
		}
		public Dictionary<ReverserPosition, string> RevPosDic { get; } = new();

		static void SetIsKeyPushed(in KeyNums key, in bool value) => CtrlInput.SetIsKeyPushed((int)key, value);

		private bool _Horn0 = false;
		public bool Horn0 { get => _Horn0; set { SetIsKeyPushed(KeyNums.Horn0, value); _Horn0 = value; } }
		private bool _Horn1 = false;
		public bool Horn1 { get => _Horn1; set { SetIsKeyPushed(KeyNums.Horn1, value); _Horn1 = value; } }
		private bool _MusicHorn = false;
		public bool MusicHorn { get => _MusicHorn; set { SetIsKeyPushed(KeyNums.MusicHorn, value); _MusicHorn = value; } }
		private bool _ConstSPD = false;
		public bool ConstSPD { get => _ConstSPD; set { SetIsKeyPushed(KeyNums.ConstSPD, value); _ConstSPD = value; } }
		private bool _ATS_S = false;
		public bool ATS_S { get => _ATS_S; set { SetIsKeyPushed(KeyNums.ATS_S, value); _ATS_S = value; } }
		private bool _ATS_A1 = false;
		public bool ATS_A1 { get => _ATS_A1; set { SetIsKeyPushed(KeyNums.ATS_A1, value); _ATS_A1 = value; } }
		private bool _ATS_A2 = false;
		public bool ATS_A2 { get => _ATS_A2; set { SetIsKeyPushed(KeyNums.ATS_A2, value); _ATS_A2 = value; } }
		private bool _ATS_B1 = false;
		public bool ATS_B1 { get => _ATS_B1; set { SetIsKeyPushed(KeyNums.ATS_B1, value); _ATS_B1 = value; } }
		private bool _ATS_B2 = false;
		public bool ATS_B2 { get => _ATS_B2; set { SetIsKeyPushed(KeyNums.ATS_B2, value); _ATS_B2 = value; } }
		private bool _ATS_C1 = false;
		public bool ATS_C1 { get => _ATS_C1; set { SetIsKeyPushed(KeyNums.ATS_C1, value); _ATS_C1 = value; } }
		private bool _ATS_C2 = false;
		public bool ATS_C2 { get => _ATS_C2; set { SetIsKeyPushed(KeyNums.ATS_C2, value); _ATS_C2 = value; } }
		private bool _ATS_D = false;
		public bool ATS_D { get => _ATS_D; set { SetIsKeyPushed(KeyNums.ATS_D, value); _ATS_D = value; } }
		private bool _ATS_E = false;
		public bool ATS_E { get => _ATS_E; set { SetIsKeyPushed(KeyNums.ATS_E, value); _ATS_E = value; } }
		private bool _ATS_F = false;
		public bool ATS_F { get => _ATS_F; set { SetIsKeyPushed(KeyNums.ATS_F, value); _ATS_F = value; } }
		private bool _ATS_G = false;
		public bool ATS_G { get => _ATS_G; set { SetIsKeyPushed(KeyNums.ATS_G, value); _ATS_G = value; } }
		private bool _ATS_H = false;
		public bool ATS_H { get => _ATS_H; set { SetIsKeyPushed(KeyNums.ATS_H, value); _ATS_H = value; } }
		private bool _ATS_I = false;
		public bool ATS_I { get => _ATS_I; set { SetIsKeyPushed(KeyNums.ATS_I, value); _ATS_I = value; } }
		private bool _ATS_J = false;
		public bool ATS_J { get => _ATS_J; set { SetIsKeyPushed(KeyNums.ATS_J, value); _ATS_J = value; } }
		private bool _ATS_K = false;
		public bool ATS_K { get => _ATS_K; set { SetIsKeyPushed(KeyNums.ATS_K, value); _ATS_K = value; } }
		private bool _ATS_L = false;
		public bool ATS_L { get => _ATS_L; set { SetIsKeyPushed(KeyNums.ATS_L, value); _ATS_L = value; } }

		enum KeyNums
		{
			Horn0,
			Horn1,
			MusicHorn,
			ConstSPD,
			ATS_S,
			ATS_A1,
			ATS_A2,
			ATS_B1,
			ATS_B2,
			ATS_C1,
			ATS_C2,
			ATS_D,
			ATS_E,
			ATS_F,
			ATS_G,
			ATS_H,
			ATS_I,
			ATS_J,
			ATS_K,
			ATS_L,
		}
	}
}
