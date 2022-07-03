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
		public byte[] Config => DefaultConfig;

		/// <summary>
		/// 4 bytes
		/// </summary>
		public uint FileSize { get; }

		/// <summary>
		/// 8 bytes
		/// </summary>
		/// <returns></returns>
		public byte[] Unknown { get; }

		/// <summary>
		/// 4 bytes
		/// <para>Starts with <see cref="MediaType"/> and does not include the tail paddings</para>
		/// </summary>
		public uint MediaSize { get; }

		private MediaHeader(uint fileSize, byte[] unknown, uint mediaSize)
		{
			FileSize = fileSize;
			Unknown = unknown;
			MediaSize = mediaSize;
		}

		public override string ToString()
		{
			return $"FileSize: {FileSize}, Unknown: {Unknown.ToHexString()}, MediaSize: {MediaSize}";
		}

		public byte[] ToBytes() => Config
			.Concat(BitConverter.GetBytes(FileSize))
			.Concat(Unknown)
			.Concat(BitConverter.GetBytes(MediaSize))
			.Concat(RepetitionUtils.GetBytesOfNulls(12))
			.ToArray();

		public static MediaHeader Parse(byte[] bytes)
		{
			var config = bytes.Take(4).ToArray();
			var fileSizeBytes = bytes.Skip(4).Take(4).ToArray();
			var unknown = bytes.Skip(8).Take(8).ToArray();
			var mediaSizeBytes = bytes.Skip(16).Take(4).ToArray();

			MediaHeaderValidator.Validate(config);

			var fileSize = BitConverter.ToUInt32(fileSizeBytes);
			var mediaSize = BitConverter.ToUInt32(mediaSizeBytes);

			return new MediaHeader(fileSize, unknown, mediaSize);
		}
	}
}