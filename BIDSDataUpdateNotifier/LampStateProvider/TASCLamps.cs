namespace BIDSDataUpdateNotifier.LampStateProvider
{
	public interface ITASCLamps<T>
	{
		/// <summary>TASC電源</summary>
		T Power { get; }
		/// <summary>TASCパターン</summary>
		T Pattern { get; }
		/// <summary>TASCブレーキ</summary>
		T Brake { get; }
		/// <summary>TASC切</summary>
		T Off { get; }
		/// <summary>TASC故障</summary>
		T Fault { get; }
		/// <summary>インチング制御中</summary>
		T Inching { get; }
	}
	public class TASCLamps : LampsClassBASE<TASCLampsIndexes>, ITASCLamps<BoolValueProvideFromPanel>
	{
		static private readonly TASCLampsIndexes DefaultIndexes = new()
		{
			Power = 85,
			Pattern = 86,
			Brake = 87,
			Off = 88,
			Inching = 89,
			Fault = -1//対応Indexなし
		};

		public TASCLamps() => LampsIndexes = DefaultIndexes;
		public TASCLamps(in TASCLampsIndexes index) => LampsIndexes = index;

		public override TASCLampsIndexes LampsIndexes
		{
			get => new() { Brake = Brake.Index, Fault = Fault.Index, Inching = Inching.Index, Off = Off.Index, Pattern = Pattern.Index, Power = Power.Index };
			set
			{
				Brake.Index = value.Brake;
				Fault.Index = value.Fault;
				Inching.Index = value.Inching;
				Off.Index = value.Off;
				Pattern.Index = value.Pattern;
				Power.Index = value.Power;
			}
		}

		public BoolValueProvideFromPanel Power { get; } = new();
		public BoolValueProvideFromPanel Pattern { get; } = new();
		public BoolValueProvideFromPanel Brake { get; } = new();
		public BoolValueProvideFromPanel Off { get; } = new();
		public BoolValueProvideFromPanel Fault { get; } = new();
		public BoolValueProvideFromPanel Inching { get; } = new();
	}

	public class TASCLampsIndexes : ITASCLamps<int>
	{
		public int Power { get; set; }
		public int Pattern { get; set; }
		public int Brake { get; set; }
		public int Off { get; set; }
		public int Fault { get; set; }
		public int Inching { get; set; }
	}
}
