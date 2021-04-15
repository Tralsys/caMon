using System;

using TR.BIDSSMemLib;

namespace caMon
{
	static public class SharedFuncs
	{
		static SharedFuncs() => SMemLib.Begin();

		static public Func<IPages> GetPageSampleModInstance = null;
		static public void SMem_RStart() => SMemLib.ReadStart();
		static public void SMem_RStop() => SMemLib.ReadStop();
	}
}
