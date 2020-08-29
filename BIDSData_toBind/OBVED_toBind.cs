using System;
using System.Collections.Generic;
using System.Text;

namespace BIDSData_toBind
{
	public class OBVED_toBind : OnPropChangedIncluded
	{
		public bool OD_IsEnabled { get; private set; } = false;
		public int OD_Version { get; private set; } = 0;

		public double Radius { get; private set; } = 0;
		public double Cant { get; private set; } = 0;
		public double Pitch { get; private set; } = 0;
		public double ElapTime { get; private set; } = 0;
		public bool PreTrain_IsEnabled { get; private set; } = false;
		public double PreTrain_Location { get; private set; } = 0;
		public double PreTrain_Distance { get; private set; } = 0;
		public double PreTrain_Speed { get; private set; } = 0;
		public int SelfBCount { get; private set; } = 0;
		public int SelfBPos { get; private set; } = 0;

		private OpenD __OD = new OpenD();
		public OpenD OD
		{
			get => __OD;
			set
			{
				if (OD_IsEnabled != value.IsEnabled)
				{
					OD_IsEnabled = value.IsEnabled;
					OnPropertyChanged(nameof(OD_IsEnabled));
				}
				if (OD_Version != value.Ver)
				{
					OD_Version = value.Ver;
					OnPropertyChanged(nameof(OD_Version));
				}

				if (Radius != value.Radius)
				{
					Radius = value.Radius;
					OnPropertyChanged(nameof(Radius));
				}
				if (Cant != value.Cant)
				{
					Cant = value.Cant;
					OnPropertyChanged(nameof(Cant));
				}
				if (Pitch != value.Pitch)
				{
					Pitch = value.Pitch;
					OnPropertyChanged(nameof(Pitch));
				}
				if (ElapTime != value.ElapTime)
				{
					ElapTime = value.ElapTime;
					OnPropertyChanged(nameof(ElapTime));
				}

				if (PreTrain_IsEnabled != value.PreTrain.IsEnabled)
				{
					PreTrain_IsEnabled = value.PreTrain.IsEnabled;
					OnPropertyChanged(nameof(PreTrain_IsEnabled));
				}
				if (PreTrain_Location != value.PreTrain.Location)
				{
					PreTrain_Location = value.PreTrain.Location;
					OnPropertyChanged(nameof(PreTrain_Location));
				}
				if (PreTrain_Distance != value.PreTrain.Distance)
				{
					PreTrain_Distance = value.PreTrain.Distance;
					OnPropertyChanged(nameof(PreTrain_Distance));
				}
				if (PreTrain_Speed != value.PreTrain.Speed)
				{
					PreTrain_Speed = value.PreTrain.Speed;
					OnPropertyChanged(nameof(PreTrain_Speed));
				}

				if (SelfBCount != value.SelfBCount)
				{
					SelfBCount = value.SelfBCount;
					OnPropertyChanged(nameof(SelfBCount));
				}
				if (SelfBPos != value.SelfBPosition)
				{
					SelfBPos = value.SelfBPosition;
					OnPropertyChanged(nameof(SelfBPos));
				}

				__OD = value;
			}
		}
	}
}
