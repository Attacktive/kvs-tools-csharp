using System;
using System.Linq;
using KvsTools.extension;
using KvsTools.util;

namespace KvsTools.header.source
{
	public class SourceHeader
	{
		public static readonly int NumberOfBytes = 32;

		public uint FileSize { get; }
		public SourceSignature Signature { get; }
		public byte[] UnknownBytes { get; }

		public SourceHeader(uint fileSize, SourceSignature signature, byte[] unknownBytes)
		{
			FileSize = fileSize;
			Signature = signature;
			UnknownBytes = unknownBytes;
		}

		public override string ToString()
		{
			return $"FileSize: {FileSize}, Signature: {Signature.Value}, UnknownBytes: {UnknownBytes.ToHexString()}";
		}

		public byte[] ToBytes() => BitConverter.GetBytes(FileSize)
			.Concat(Signature.ToByteArray())
			.Concat(UnknownBytes)
			.Concat(RepetitionUtils.GetBytesOfNulls(16))
			.ToArray();
	}
}