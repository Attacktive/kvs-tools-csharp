using System.Linq;
using System.Text;

namespace kvs_tools_csharp.extension
{
	public static class Extensions
	{
		public static string ToHexString(this byte[] source)
		{
			var stringBuilder = new StringBuilder();
			source.ToList().ForEach(aByte => stringBuilder.Append(aByte.ToString("X2")));

			return stringBuilder.ToString();
		}
	}
}