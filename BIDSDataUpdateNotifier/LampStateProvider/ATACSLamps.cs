namespace BIDSDataUpdateNotifier.LampStateProvider
{
	public interface IATACSLamps<T>
	{
		/// <summary>ATACS電源</summary>
		T Power { get; }
		/// <summary>ATACS開放</summary>
		T CutOut { get; }
		/// <summary>"ATACS"表示灯</summary>
		T ATACS { get; }
		/// <summary>ATACS切</summary>
		T Off { get; }
		/// <summary>ATACS常用</summary>
		T NormalBrake { get; }
		/// <summary>ATACS非常</summary>
		T EmergencyBrake { get; }
		/// <summary>停通防止動作</summary>
		T MisspassingPreventer { get; }
		/// <summary>後退検知</summary>
		T RollbackDetected { get; }
		/// <summary>パターン低減</summary>
		T LowerPattern { get; }
		/// <summary>非常運転</summary>
		T EmergencyOperation { get; }
		/// <summary>ID結合(対応表示灯なし)</summary>
		T IDLinked { get; }
		/// <summary>車上ID故障</summary>
		T CarIDFault { get; }
		/// <summary>ATACS故障</summary>
		T Fault { get; }
		/// <summary>1系故障</summary>
		T System1Fault { get; }
		/// <summary>2系故障</summary>
		T System2Fault { get; }
	}
	public class ATACSLamps : LampsClassBASE<ATACSLampsIndexes>, IATACSLamps<BoolValueProvideFromPanel>
	{
		static private readonly ATACSLampsIndexes DefaultIndexes = new()
		{
			ATACS = 41,
			CutOut = 39,
			EmergencyBrake = 44,
			EmergencyOperation = 48,
			IDLinked = 49,
			LowerPattern = 47,
			MisspassingPreventer = 45,
			NormalBrake = 43,
			Off = 42,
			Power = 38,
			RollbackDetected = 46,

			#region デフォルトアサインでは非対応な機能
			CarIDFault = -1,
			Fault = -1,
			System1Fault = -1,
			System2Fault = -1,
			#endregion
		};

		public ATACSLamps() => LampsIndexes = DefaultIndexes;
		public ATACSLamps(in ATACSLampsIndexes index) => LampsIndexes = index;

		public override ATACSLampsIndexes LampsIndexes
		{
			get => new() { ATACS = ATACS.Index, CarIDFault = CarIDFault.Index, CutOut = CutOut.Index, EmergencyBrake = EmergencyBrake.Index, EmergencyOperation = EmergencyOperation.Index, Fault = Fault.Index, IDLinked = IDLinked.Index, LowerPattern = LowerPattern.Index, MisspassingPreventer = MisspassingPreventer.Index, NormalBrake = NormalBrake.Index, Off = Off.Index, Power = Power.Index, RollbackDetected = RollbackDetected.Index, System1Fault = System1Fault.Index, System2Fault = System2Fault.Index };
			set
			{
				ATACS.Index = value.ATACS;
				CarIDFault.Index = value.CarIDFault;
				CutOut.Index = value.CutOut;
				EmergencyBrake.Index = value.EmergencyBrake;
				EmergencyOperation.Index = value.EmergencyOperation;
				Fault.Index = value.Fault;
				IDLinked.Index = value.IDLinked;
				LowerPattern.Index = value.LowerPattern;
				MisspassingPreventer.Index = value.MisspassingPreventer;
				NormalBrake.Index = value.NormalBrake;
				Off.Index = value.Off;
				Power.Index = value.Power;
				RollbackDetected.Index = value.RollbackDetected;
				System1Fault.Index = value.System1Fault;
				System2Fault.Index = value.System2Fault;
			}
		}

		public BoolValueProvideFromPanel Power { get; } = new();
		public BoolValueProvideFromPanel CutOut { get; } = new();
		public BoolValueProvideFromPanel ATACS { get; } = new();
		public BoolValueProvideFromPanel Off { get; } = new();
		public BoolValueProvideFromPanel NormalBrake { get; } = new();
		public BoolValueProvideFromPanel EmergencyBrake { get; } = new();
		public BoolValueProvideFromPanel MisspassingPreventer { get; } = new();
		public BoolValueProvideFromPanel RollbackDetected { get; } = new();
		public BoolValueProvideFromPanel LowerPattern { get; } = new();
		public BoolValueProvideFromPanel EmergencyOperation { get; } = new();
		public BoolValueProvideFromPanel IDLinked { get; } = new();
		public BoolValueProvideFromPanel CarIDFault { get; } = new();
		public BoolValueProvideFromPanel Fault { get; } = new();
		public BoolValueProvideFromPanel System1Fault { get; } = new();
		public BoolValueProvideFromPanel System2Fault { get; } = new();
	}

	public class ATACSLampsIndexes : IATACSLamps<int>
	{
		public int Power { get; set; }
		public int CutOut { get; set; }
		public int ATACS { get; set; }
		public int Off { get; set; }
		public int NormalBrake { get; set; }
		public int EmergencyBrake { get; set; }
		public int MisspassingPreventer { get; set; }
		public int RollbackDetected { get; set; }
		public int LowerPattern { get; set; }
		public int EmergencyOperation { get; set; }
		public int IDLinked { get; set; }
		public int CarIDFault { get; set; }
		public int Fault { get; set; }
		public int System1Fault { get; set; }
		public int System2Fault { get; set; }
	}
}
