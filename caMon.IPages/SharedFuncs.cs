using System;
using System.Collections.Generic;
using System.Text;
using TR.BIDSSMemLib;

namespace caMon
{
	static public class SharedFuncs
	{
		static public SMemLib SML;

		static SharedFuncs()
		{
			SML = new SMemLib();
		}

		static public void SMem_RStart() => SML.ReadStart(5);//BSMDだけ
		static public void SMem_RStop() => SML.ReadStop();
	}
}
