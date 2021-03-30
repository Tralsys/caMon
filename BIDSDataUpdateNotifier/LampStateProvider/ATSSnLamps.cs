namespace BIDSDataUpdateNotifier.LampStateProvider
{
	public interface IATSSnLamps<T>
	{
		T Power { get; }
		T WhiteLamp { get; }
		T RedLamp { get; }
	}
	public class ATSSnLamps : LampsClassBASE<ATSSnLampsIndexes>, IATSSnLamps<BoolValueProvideFromPanel>
	{
		static private readonly ATSSnLampsIndexes DefaultIndexes = new()
		{
			Power = 2,
			RedLamp = 1,
			WhiteLamp = 0
		};

		public BoolValueProvideFromPanel Power { get; } = new(DefaultIndexes.Power);
		public BoolValueProvideFromPanel WhiteLamp { get; } = new(DefaultIndexes.WhiteLamp);
		public BoolValueProvideFromPanel RedLamp { get; } = new(DefaultIndexes.RedLamp);

		public override ATSSnLampsIndexes LampsIndexes
		{
			get => new() { RedLamp = RedLamp.Index, WhiteLamp = WhiteLamp.Index, Power = Power.Index };
			set
			{
				RedLamp.Index = value.RedLamp;
				WhiteLamp.Index = value.WhiteLamp;
				Power.Index = value.Power;
			}
		}
	}

	public class ATSSnLampsIndexes : IATSSnLamps<int>
	{
		public int Power { get; set; }
		public int WhiteLamp { get; set; }
		public int RedLamp { get; set; }
	}
}
