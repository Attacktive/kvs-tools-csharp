﻿using System;
using System.Linq;
using KvsTools.Extension;
using KvsTools.Util;

namespace KvsTools.Header.Source
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
			.Concat(RepetitionUtils.GetBytesOfNulls(12))
			.Concat(Signature.ToByteArray())
			.Concat(BitConverter.GetBytes(FileSize - 32))
			.Concat(UnknownBytes)
			.Concat(RepetitionUtils.GetBytesOfNulls(16))
			.ToArray();
	}
}