using System.IO;
using System.Reflection;

namespace BIDSDataUpdateNotifier
{
	internal class ConstValues
	{

		public static string DllLocation { get; } = Assembly.GetExecutingAssembly().Location;
		public static string DllDirectory { get; } = Path.GetDirectoryName(DllLocation) ?? string.Empty;
	}
}
