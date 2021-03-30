namespace BIDSDataUpdateNotifier.LampStateProvider
{
	public interface IATSPLamps<T>
	{
		T Power { get; }
		T PatternOnComing { get; }
		T NormalBrake { get; }
		T EmergencyBrake { get; }
		T BrakeCutOut { get; }
		T ATSP { get; }
		T Fault { get; }
	}
	public class ATSPLamps : LampsClassBASE<ATSPLampsIndexes>, IATSPLamps<BoolValueProvideFromPanel>
	{
		static private readonly ATSPLampsIndexes DefaultIndexes = new()
		{
			Power = 2,
			PatternOnComing = 3,
			NormalBrake = 5,
			EmergencyBrake = 8,//CT氏のATS-Pプラグインにおけるindex  Ask氏のプラグインには対応インデックス無し
			BrakeCutOut = 4,
			ATSP = 6,
			Fault = 7,
		};

		public BoolValueProvideFromPanel Power { get; } = new(DefaultIndexes.Power);
		public BoolValueProvideFromPanel PatternOnComing { get; } = new(DefaultIndexes.PatternOnComing);
		public BoolValueProvideFromPanel NormalBrake { get; } = new(DefaultIndexes.NormalBrake);
		public BoolValueProvideFromPanel EmergencyBrake { get; } = new(DefaultIndexes.EmergencyBrake);
		public BoolValueProvideFromPanel BrakeCutOut { get; } = new(DefaultIndexes.BrakeCutOut);
		public BoolValueProvideFromPanel ATSP { get; } = new(DefaultIndexes.ATSP);
		public BoolValueProvideFromPanel Fault { get; } = new(DefaultIndexes.Fault);

		public override ATSPLampsIndexes LampsIndexes
		{
			get => new() { ATSP = ATSP.Index, BrakeCutOut = BrakeCutOut.Index, EmergencyBrake = EmergencyBrake.Index, Fault = Fault.Index, NormalBrake = NormalBrake.Index, PatternOnComing = PatternOnComing.Index, Power = Power.Index };
			set
			{
				ATSP.Index = value.ATSP;
				BrakeCutOut.Index = value.BrakeCutOut;
				EmergencyBrake.Index = value.EmergencyBrake;
				Fault.Index = value.Fault;
				NormalBrake.Index = value.NormalBrake;
				PatternOnComing.Index = value.PatternOnComing;
				Power.Index = value.Power;
			}
		}
	}

	public class ATSPLampsIndexes : IATSPLamps<int>
	{
		public int Power { get; set; }
		public int PatternOnComing { get; set; }
		public int NormalBrake { get; set; }
		public int EmergencyBrake { get; set; }
		public int BrakeCutOut { get; set; }
		public int ATSP { get; set; }
		public int Fault { get; set; }
	}
}
