using System;
using System.Linq;
using kvs_tools_csharp.util;

namespace kvs_tools_csharp.header
{
	public partial class SourceHeader
	{
		public static readonly int NumberOfBytes = 32;

		public SourceSignature Signature { get; }
		public uint FileSize { get; }
		public byte[] UnknownBytes { get; }

		public SourceHeader(SourceSignature signature, uint fileSize, byte[] unknownBytes)
		{
			Signature = signature;
			FileSize = fileSize;
			UnknownBytes = unknownBytes;
		}

		public byte[] ToBytes() => Signature.ToByteArray()
			.Concat(BitConverter.GetBytes(FileSize))
			.Concat(UnknownBytes)
			.Concat(RepetitionUtils.GetBytesOfNulls(16))
			.ToArray();
	}
}