using System;

namespace BIDSDataUpdateNotifier
{
	public interface IValueUpdateTimingProvider
	{
		event EventHandler Update;
	}
}
