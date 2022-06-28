using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KvsTools.Header.Source
{
	public class SourceSignature
	{
		public static readonly SourceSignature Kovs = new SourceSignature("KOVS", "kvs");
		public static readonly SourceSignature Ktss = new SourceSignature("KTSS", "kns");

		private static readonly List<SourceSignature> List = new List<SourceSignature>
		{
			Kovs, Ktss
		};

		public string Value { get; }
		public string Extension { get; }

		private SourceSignature(string value, string extension)
		{
			Value = value;
			Extension = extension;
		}

		public IEnumerable<byte> ToByteArray() => Encoding.UTF8.GetBytes(Value);

		public static SourceSignature? BySignatureBytes(byte[] signatureBytes)
		{
			return List.FirstOrDefault(signature => signature.ToByteArray().SequenceEqual(signatureBytes));
		}
	}
}