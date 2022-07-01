using System;
using System.Linq;
using KvsTools.Extension;
using KvsTools.Util;

namespace KvsTools.Spec.Media.Header
{
	public class MediaHeader
	{
		public static readonly int NumberOfBytes = 32;

		public static readonly byte[] DefaultConfig = { 0x09, 0xd4, 0xf4, 0x15 };

		/// <summary>
		/// 4 bytes
		/// <para>Should be <see cref="DefaultConfig"/></para>
		/// </summary>
		public byte[] Config { get; } = DefaultConfig;

		/// <summary>
		/// 4 bytes
		/// </summary>
		public uint FileSize { get; }

		/// <summary>
		/// 12 bytes
		/// </summary>
		/// <returns></returns>
		public byte[] Chunk { get; }

		private MediaHeader(uint fileSize, byte[] chunk)
		{
			FileSize = fileSize;
			Chunk = chunk;
		}

		public override string ToString()
		{
			return $"FileSize: {FileSize}, Chunk: {Chunk.ToHexString()}";
		}

		public byte[] ToBytes() => Config
			.Concat(BitConverter.GetBytes(FileSize))
			.Concat(Chunk)
			.Concat(RepetitionUtils.GetBytesOfNulls(12))
			.ToArray();

		public static MediaHeader Parse(byte[] bytes)
		{
			var config = bytes.Take(4).ToArray();
			var fileSizeBytes = bytes.Skip(4).Take(4).ToArray();
			var chunk = bytes.Skip(8).Take(12).ToArray();

			MediaHeaderValidator.Validate(config);

			var fileSize = BitConverter.ToUInt32(fileSizeBytes);

			return new MediaHeader(fileSize, chunk);
		}
	}
}