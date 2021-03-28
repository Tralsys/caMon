using System.ComponentModel;

namespace BIDSDataUpdateNotifier
{
	public interface IValueChecker<T> : INotifyPropertyChanged
	{
		/// <summary>Array Index</summary>
		int Index { get; set; }
		/// <summary>Value to Show</summary>
		T Value { get; }
		/// <summary>Raw Value</summary>
		int RawValue { get; }
		IValueUpdateTimingProvider ValueUpdateTimingProvider { get; set; }
	}
}
