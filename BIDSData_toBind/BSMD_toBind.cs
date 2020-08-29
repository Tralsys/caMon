using System.ComponentModel;

namespace BIDSData_toBind
{
	public class BSMD_toBind : OnPropChangedIncluded
	{
		#region BIDS Base Data
		public bool BIDS_IsEnabled { get; private set; } = false;
		public int BIDS_Version { get; private set; } = 0;
		#endregion

		#region Spec Data
		public int BrakeNCount { get; private set; } = 0;
		public int PowerNCount { get; private set; } = 0;
		public int ATSCheckPos { get; private set; } = 0;
		public int B67Pos { get; private set; } = 0;
		public int CarCount { get; private set; } = 0;
		#endregion

		#region State Data
		public double MyLocation { get; private set; } = 0;
		public double MySpeed { get; private set; } = 0;
		public int Time { get; private set; } = 0;
		public double BCPres { get; private set; } = 0;
		public double MRPres { get; private set; } = 0;
		public double ERPres { get; private set; } = 0;
		public double BPPres { get; private set; } = 0;
		public double SAPPres { get; private set; } = 0;
		public double Current { get; private set; } = 0;
		#endregion

		#region Handle Position Data
		public int BrakeNPos { get; private set; } = 0;
		public int PowerNPos { get; private set; } = 0;
		public int ReverserPos { get; private set; } = 0;
		public int ConstSPD { get; private set; } = 0;
		#endregion

		public bool IsDoorClosed { get; private set; } = false;


		#region BVE Usable keys
		public double kmph { get => MySpeed; }
		public int kmphd0 { get => (int)(MySpeed % 10); }
		public int kmphd1 { get => (int)((MySpeed / 10) % 10); }
		public int kmphd2 { get => (int)((MySpeed / 100) % 10); }

		public double bc { get => BCPres; }
		public int bcd0 { get => (int)(BCPres % 10); }
		public int bcd1 { get => (int)((BCPres / 10) % 10); }
		public int bcd2 { get => (int)((BCPres / 100) % 10); }

		public double mr { get => MRPres; }
		public int mrd0 { get => (int)(MRPres % 10); }
		public int mrd1 { get => (int)((MRPres / 10) % 10); }
		public int mrd2 { get => (int)((MRPres / 100) % 10); }

		public double sap { get => SAPPres; }
		public int sapd0 { get => (int)(SAPPres % 10); }
		public int sapd1 { get => (int)((SAPPres / 10) % 10); }
		public int sapd2 { get => (int)((SAPPres / 100) % 10); }

		public double bp { get => BPPres; }
		public int bpd0 { get => (int)(BPPres % 10); }
		public int bpd1 { get => (int)((BPPres / 10) % 10); }
		public int bpd2 { get => (int)((BPPres / 100) % 10); }

		public double er { get => ERPres; }
		public int erd0 { get => (int)(ERPres % 10); }
		public int erd1 { get => (int)((ERPres / 10) % 10); }
		public int erd2 { get => (int)((ERPres / 100) % 10); }

		public bool door { get => IsDoorClosed; }
		public int doord0 { get => IsDoorClosed ? 1 : 0; }

		public bool csc { get => ConstSPD == 1; }
		public int cscd0 { get => ConstSPD; }

		public int power { get => PowerNPos; }

		public int brake { get => BrakeNPos; }

		public double hour { get => (double)min / 60; }
		public double min { get => (double)sec / 60; }
		public int sec { get => Time / 1000; }
		#endregion

		BIDSSharedMemoryData __BSMD = new BIDSSharedMemoryData();
		public BIDSSharedMemoryData BSMD
		{
			get => __BSMD;
			set
			{
				/*BIDS Base Data*/
				if (BIDS_IsEnabled != value.IsEnabled)
				{
					BIDS_IsEnabled = value.IsEnabled;
					OnPropertyChanged(nameof(BIDS_IsEnabled));
				}

				if (BIDS_Version != value.VersionNum)
				{
					BIDS_Version = value.VersionNum;
					OnPropertyChanged(nameof(BIDS_Version));
				}

				/*Spec Data*/
				if (BrakeNCount != value.SpecData.B)
				{
					BrakeNCount = value.SpecData.B;
					OnPropertyChanged(nameof(BrakeNCount));
				}

				if (PowerNCount != value.SpecData.P)
				{
					PowerNCount = value.SpecData.P;
					OnPropertyChanged(nameof(PowerNCount));
				}

				if (ATSCheckPos != value.SpecData.A)
				{
					ATSCheckPos = value.SpecData.A;
					OnPropertyChanged(nameof(ATSCheckPos));
				}

				if (B67Pos != value.SpecData.J)
				{
					B67Pos = value.SpecData.J;
					OnPropertyChanged(nameof(B67Pos));
				}

				if (CarCount != value.SpecData.C)
				{
					CarCount = value.SpecData.C;
					OnPropertyChanged(nameof(CarCount));
				}

				/*State Data*/
				if (MyLocation != value.StateData.Z)
				{
					MyLocation = value.StateData.Z;
					OnPropertyChanged(nameof(MyLocation));
				}

				if (MySpeed != value.StateData.V)
				{
					MySpeed = value.StateData.V;
					OnPropertyChanged(nameof(MySpeed));
					OnPropertyChanged(nameof(kmph));
					OnPropertyChanged(nameof(kmphd0));
					OnPropertyChanged(nameof(kmphd1));
					OnPropertyChanged(nameof(kmphd2));
				}

				if (Time != value.StateData.T)
				{
					Time = value.StateData.T;
					OnPropertyChanged(nameof(Time));
					OnPropertyChanged(nameof(hour));
					OnPropertyChanged(nameof(min));
					OnPropertyChanged(nameof(sec));
				}

				if (BCPres != value.StateData.BC)
				{
					BCPres = value.StateData.BC;
					OnPropertyChanged(nameof(BCPres));
					OnPropertyChanged(nameof(bc));
					OnPropertyChanged(nameof(bcd0));
					OnPropertyChanged(nameof(bcd1));
					OnPropertyChanged(nameof(bcd2));
				}

				if (MRPres != value.StateData.MR)
				{
					MRPres = value.StateData.MR;
					OnPropertyChanged(nameof(MRPres));
					OnPropertyChanged(nameof(mr));
					OnPropertyChanged(nameof(mrd0));
					OnPropertyChanged(nameof(mrd1));
					OnPropertyChanged(nameof(mrd2));
				}

				if (SAPPres != value.StateData.SAP)
				{
					SAPPres = value.StateData.SAP;
					OnPropertyChanged(nameof(SAPPres));
					OnPropertyChanged(nameof(sap));
					OnPropertyChanged(nameof(sapd0));
					OnPropertyChanged(nameof(sapd1));
					OnPropertyChanged(nameof(sapd2));
				}

				if (BPPres != value.StateData.BP)
				{
					BPPres = value.StateData.BP;
					OnPropertyChanged(nameof(BPPres));
					OnPropertyChanged(nameof(bp));
					OnPropertyChanged(nameof(bpd0));
					OnPropertyChanged(nameof(bpd1));
					OnPropertyChanged(nameof(bpd2));
				}

				if (ERPres != value.StateData.ER)
				{
					ERPres = value.StateData.ER;
					OnPropertyChanged(nameof(ERPres));
					OnPropertyChanged(nameof(er));
					OnPropertyChanged(nameof(erd0));
					OnPropertyChanged(nameof(erd1));
					OnPropertyChanged(nameof(erd2));
				}

				if (Current != value.StateData.I)
				{
					Current = value.StateData.I;
					OnPropertyChanged(nameof(Current));
				}

				if (BrakeNPos != value.HandleData.B)
				{
					BrakeNPos = value.HandleData.B;
					OnPropertyChanged(nameof(BrakeNPos));
					OnPropertyChanged(nameof(brake));
				}

				if (PowerNPos != value.HandleData.P)
				{
					PowerNPos = value.HandleData.P;
					OnPropertyChanged(nameof(PowerNPos));
					OnPropertyChanged(nameof(power));
				}

				if (ReverserPos != value.HandleData.R)
				{
					ReverserPos = value.HandleData.R;
					OnPropertyChanged(nameof(ReverserPos));
				}

				if (ConstSPD != value.HandleData.C)
				{
					ConstSPD = value.HandleData.C;
					OnPropertyChanged(nameof(ConstSPD));
					OnPropertyChanged(nameof(csc));
					OnPropertyChanged(nameof(cscd0));
				}

				if (IsDoorClosed != value.IsDoorClosed)
				{
					IsDoorClosed = value.IsDoorClosed;
					OnPropertyChanged(nameof(IsDoorClosed));
					OnPropertyChanged(nameof(door));
					OnPropertyChanged(nameof(doord0));
				}

				__BSMD = value;
			}
		}


	}
}
