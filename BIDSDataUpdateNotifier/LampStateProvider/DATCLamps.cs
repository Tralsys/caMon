namespace BIDSDataUpdateNotifier.LampStateProvider
{
	public interface IDATCLamps<T>
	{
		/// <summary>デジタルATC</summary>
		T DATC { get; }
		/// <summary>ATC</summary>
		T ATC { get; }
		/// <summary>切</summary>
		T Off { get; }
		/// <summary>パターン低減</summary>
		T LowerPattern { get; }
		/// <summary>非常運転</summary>
		T EmergencyOperation { get; }
		/// <summary>ATC常用</summary>
		T NormalBrake { get; }
		/// <summary>ATC非常</summary>
		T EmergencyBrake { get; }
		/// <summary>停通防止動作</summary>
		T MisspassingPreventer { get; }
		/// <summary>ATC電源</summary>
		T Power { get; }
		/// <summary>ATC開放</summary>
		T CutOut{ get; }

	}
	public class DATCLamps : LampsClassBASE<DATCLampsIndexes>, IDATCLamps<BoolValueProvideFromPanel>
	{
		static private readonly DATCLampsIndexes DefaultIndexes = new()
		{
			DATC = 65,
			NormalBrake = 64,
			EmergencyBrake = 63,

			ATC = -1,
			CutOut = -1,
			EmergencyOperation = -1,
			LowerPattern = -1,
			MisspassingPreventer = -1,
			Off = -1,
			Power = -1
		};


		public override DATCLampsIndexes LampsIndexes
		{
			get => new() { ATC = ATC.Index, CutOut = CutOut.Index, DATC = DATC.Index, EmergencyBrake = EmergencyBrake.Index, EmergencyOperation = EmergencyOperation.Index, LowerPattern = LowerPattern.Index, MisspassingPreventer = MisspassingPreventer.Index, NormalBrake = NormalBrake.Index, Off = Off.Index, Power = Power.Index };
			set
			{
				ATC.Index = value.ATC;
				CutOut.Index = value.CutOut;
				DATC.Index = value.DATC;
				EmergencyBrake.Index = value.EmergencyBrake;
				EmergencyOperation.Index = value.EmergencyOperation;
				LowerPattern.Index = value.LowerPattern;
				MisspassingPreventer.Index = value.MisspassingPreventer;
				NormalBrake.Index = value.NormalBrake;
				Off.Index = value.Off;
				Power.Index = value.Power;
			}
		}

		public BoolValueProvideFromPanel DATC { get; } = new();
		public BoolValueProvideFromPanel ATC { get; } = new();
		public BoolValueProvideFromPanel Off { get; } = new();
		public BoolValueProvideFromPanel LowerPattern { get; } = new();
		public BoolValueProvideFromPanel EmergencyOperation { get; } = new();
		public BoolValueProvideFromPanel NormalBrake { get; } = new();
		public BoolValueProvideFromPanel EmergencyBrake { get; } = new();
		public BoolValueProvideFromPanel MisspassingPreventer { get; } = new();
		public BoolValueProvideFromPanel Power { get; } = new();
		public BoolValueProvideFromPanel CutOut { get; } = new();
	}

	public class DATCLampsIndexes : IDATCLamps<int>
	{
		public int DATC { get; set; }
		public int ATC { get; set; }
		public int Off { get; set; }
		public int LowerPattern { get; set; }
		public int EmergencyOperation { get; set; }
		public int NormalBrake { get; set; }
		public int EmergencyBrake { get; set; }
		public int MisspassingPreventer { get; set; }
		public int Power { get; set; }
		public int CutOut { get; set; }
	}
}
