using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KvsTools.extension
{
	public static class Extensions
	{
		public static string ToHexString(this IEnumerable<byte> source)
		{
			var stringBuilder = new StringBuilder();
			source.ToList().ForEach(aByte => stringBuilder.Append(aByte.ToString("X2")));

			return stringBuilder.ToString();
		}
	}
}