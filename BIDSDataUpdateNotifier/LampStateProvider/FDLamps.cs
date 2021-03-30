namespace BIDSDataUpdateNotifier.LampStateProvider
{
	public interface IFDLamps<T>
	{
		/// <summary>転動防止ブレーキ</summary>
		T RollingStopBrake { get; }
		/// <summary>定位置</summary>
		T JustPoint { get; }
		/// <summary>車両ドア全閉</summary>
		T CarDoorAllClosd { get; }
		/// <summary>ホームドア全閉</summary>
		T FDAllClosed { get; }
		/// <summary>ホームドア連携</summary>
		T SystemConnected { get; }
		/// <summary>ホームドア分離</summary>
		T SystemDisconnected { get; }
		/// <summary>ホームドア開放</summary>
		T SystemCutOut { get; }
	}
	public class FDLamps : LampsClassBASE<FDLampsIndexes>, IFDLamps<BoolValueProvideFromPanel>
	{
		static private readonly FDLampsIndexes DefaultIndexes = new()
		{
			JustPoint = 226,
			CarDoorAllClosd = 227,
			FDAllClosed = 228,
			SystemConnected = 229,
			SystemDisconnected = 230,

			RollingStopBrake = -1,
			SystemCutOut = -1
		};


		public override FDLampsIndexes LampsIndexes
		{
			get => new() { CarDoorAllClosd = CarDoorAllClosd.Index, FDAllClosed = FDAllClosed.Index, JustPoint = JustPoint.Index, RollingStopBrake = RollingStopBrake.Index, SystemConnected = SystemConnected.Index, SystemCutOut = SystemCutOut.Index, SystemDisconnected = SystemDisconnected.Index };
			set
			{
				CarDoorAllClosd.Index = value.CarDoorAllClosd;
				FDAllClosed.Index = value.FDAllClosed;
				JustPoint.Index = value.JustPoint;
				RollingStopBrake.Index = value.RollingStopBrake;
				SystemConnected.Index = value.SystemConnected;
				SystemCutOut.Index = value.SystemCutOut;
				SystemDisconnected.Index = value.SystemDisconnected;
			}
		}

		public BoolValueProvideFromPanel RollingStopBrake { get; } = new();
		public BoolValueProvideFromPanel JustPoint { get; } = new();
		public BoolValueProvideFromPanel CarDoorAllClosd { get; } = new();
		public BoolValueProvideFromPanel FDAllClosed { get; } = new();
		public BoolValueProvideFromPanel SystemConnected { get; } = new();
		public BoolValueProvideFromPanel SystemDisconnected { get; } = new();
		public BoolValueProvideFromPanel SystemCutOut { get; } = new();
	}

	public class FDLampsIndexes : IFDLamps<int>
	{
		public int RollingStopBrake { get; set; }
		public int JustPoint { get; set; }
		public int CarDoorAllClosd { get; set; }
		public int FDAllClosed { get; set; }
		public int SystemConnected { get; set; }
		public int SystemDisconnected { get; set; }
		public int SystemCutOut { get; set; }
	}
}
