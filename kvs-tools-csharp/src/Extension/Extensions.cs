using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KvsTools.Extension
{
	public static class Extensions
	{
		public static string ToHexString(this IEnumerable<byte> source)
		{
			var stringBuilder = new StringBuilder();
			source.ToList().ForEach(aByte => stringBuilder.AppendFormat("0x{0:x2}", aByte).Append(' '));

			return stringBuilder.ToString().TrimEnd();
		}
	}
}