using System;
using System.ComponentModel;
using System.Globalization;

using TR.BIDSSMemLib;

namespace BIDSDataUpdateNotifier
{
	public abstract class BoolValueProvider : IValueChecker<bool>
	{
		public abstract IValueUpdateTimingProvider? ValueUpdateTimingProvider { get; set; }

		public event PropertyChangedEventHandler? PropertyChanged;
		protected void OnPropertyChanged(in string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


		private int _Index = -1;
		public int Index { get => _Index; set { if (_Index != value) { _Index = value; OnPropertyChanged(nameof(Index)); ValueUpdater(SMemLib.PanelA); } } }

		public virtual bool Value { get => RawValue > 0; }

		private int _RawValue;
		public int RawValue
		{
			get => _RawValue;
			set
			{
				if (RawValue != value)
				{
					bool ValueRec = Value;

					_RawValue = value;
					OnPropertyChanged(nameof(RawValue));

					if (ValueRec != Value)
						OnPropertyChanged(nameof(Value));
				}
			}
		}

		protected void ValueUpdater(in int[] array)
		{
			if (0 <= Index && Index < array.Length)
				RawValue = array[Index];
		}
		public override bool Equals(object? obj)
		 => obj is string s && bool.TryParse(s, out bool result)
			? Value == result
			: base.Equals(obj);

		public override int GetHashCode() => base.GetHashCode();
	}

	public class BoolValueProvideFromPanel : BoolValueProvider
	{
		public BoolValueProvideFromPanel() => Init();
		public BoolValueProvideFromPanel(in int index) => Init(index);
		public BoolValueProvideFromPanel(in int index, in IValueUpdateTimingProvider timingProvider) => Init(index, timingProvider);

		private void Init(in int index = -1, in IValueUpdateTimingProvider? timingProvider = null)
		{
			Index = index;

			if (timingProvider is not null)
			{
				_ValueUpdateTimingProvider = timingProvider;
				timingProvider.Update += ValueUpdateTimingProvider_Update;
			}
			else
				SMemLib.SMC_PanelDChanged += SMemLib_SMC_PanelDChanged;
		}

		private void SMemLib_SMC_PanelDChanged(object? sender, TR.ValueChangedEventArgs<int[]> e) => ValueUpdater(e.NewValue);
		private void ValueUpdateTimingProvider_Update(object? sender, EventArgs e) => ValueUpdater(SMemLib.PanelA);

		private IValueUpdateTimingProvider? _ValueUpdateTimingProvider = null;
		public override IValueUpdateTimingProvider? ValueUpdateTimingProvider
		{
			get => _ValueUpdateTimingProvider;
			set
			{
				if(ValueUpdateTimingProvider != value)
				{
					if (_ValueUpdateTimingProvider is not null)
						_ValueUpdateTimingProvider.Update -= ValueUpdateTimingProvider_Update;
					else
						SMemLib.SMC_PanelDChanged -= SMemLib_SMC_PanelDChanged;

					_ValueUpdateTimingProvider = value;

					if (ValueUpdateTimingProvider is not null)
						ValueUpdateTimingProvider.Update += ValueUpdateTimingProvider_Update;
					else
						SMemLib.SMC_PanelDChanged += SMemLib_SMC_PanelDChanged;

					OnPropertyChanged(nameof(ValueUpdateTimingProvider));
				}
			}
		}
	}
}
