using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BIDSDataUpdateNotifier
{
	internal class ConstValues
	{

		public static string DllLocation { get; } = Assembly.GetExecutingAssembly().Location;
		public static string DllDirectory { get; } = Path.GetDirectoryName(DllLocation);
	}
}
