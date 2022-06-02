using System;
using System.Collections.Generic;
using System.Linq;

namespace kvs_tools_csharp.util
{
	public static class RepetitionUtils
	{
		public static void RepeatAction(int repeatCount, Action action)
		{
			for (var i = 0; i < repeatCount; i++)
			{
				action();
			}
		}

		public static IEnumerable<byte> GetBytesOfNulls(int numberOfNulls)
		{
			return Enumerable.Repeat((byte)0, numberOfNulls);
		}
	}
}