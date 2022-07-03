using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KvsTools.Extension;
using KvsTools.Util;

namespace KvsTools.Spec.Ktsr
{
	public class KtsrHeader
	{
		public static readonly int NumberOfBytes = 64;
		public static readonly string DefaultSignature = "KTSR";
		public static readonly byte[] DefaultChunkType = { 0x02, 0x94, 0xdd, 0xfc };

		/// <summary>
		/// 4 bytes
		/// <para>Should be <see cref="DefaultSignature"/>, nothing else</para>
		/// </summary>
		public string Signature => DefaultSignature;

		/// <summary>
		/// 4 bytes
		/// <para>Should be <see cref="DefaultChunkType"/>, nothing else</para>
		/// </summary>
		public byte[] ChunkType => DefaultChunkType;

		/// <summary>
		/// 1 byte
		/// <para>Haven't seen anything other than 1</para>
		/// <value>1</value>
		/// </summary>
		public byte Version { get; }

		/// <summary>
		/// 1 byte
		/// <para>platform</para>
		/// <value>0x01: PC, 0x03: PS Vita, 0x04: Switch</value>
		/// </summary>
		public byte Platform { get; }

		/// <summary>
		/// 4 bytes
		/// <para>file size</para>
		/// for some reason, it repeats twice
		/// </summary>
		public uint FileSize { get; }

		public GameInfo Game { get; }

		public byte[] Bytes =>
			new List<byte>()
				.Concat(Encoding.UTF8.GetBytes(Signature))
				.Concat(ChunkType)
				.Concat(new[] { Version })
				.Concat(RepetitionUtils.GetBytesOfNulls(2))
				.Concat(new[] { Platform })
				.Concat(Game.Id)
				.Concat(RepetitionUtils.GetBytesOfNulls(8))
				.Concat(BitConverter.GetBytes(FileSize))
				.Concat(BitConverter.GetBytes(FileSize))
				.Concat(RepetitionUtils.GetBytesOfNulls(32))
				.ToArray();

		public KtsrHeader(uint fileSize, GameInfo gameInfo) : this(1, 1, fileSize, gameInfo) { }

		public KtsrHeader(byte version, byte platform, uint fileSize, GameInfo gameInfo)
		{
			Version = version;
			Platform = platform;
			FileSize = fileSize;
			Game = gameInfo;
		}

		public override string ToString()
		{
			return $"Signature: {Signature}, ChunkType: {ChunkType.ToHexString()}, Version: {Version}, Platform: {Platform}, FileSize: {FileSize}, GameId: {Game.Id.ToHexString()} ({Game.Name})";
		}

		public static KtsrHeader Parse(IReadOnlyList<byte> bytes)
		{
			var signatureBytes = bytes.Take(4).ToArray();
			var chunkTypeBytes = bytes.Skip(4).Take(4).ToArray();
			var versionByte = bytes[8];
			var platformByte = bytes[11];
			var gameIdBytes = bytes.Skip(12).Take(4).ToArray();
			var fileSize1Bytes = bytes.Skip(24).Take(4).ToArray();
			var fileSize2Bytes = bytes.Skip(28).Take(4).ToArray();

			var signature = Encoding.UTF8.GetString(signatureBytes);
			var fileSize1 = BitConverter.ToUInt32(fileSize1Bytes);
			var fileSize2 = BitConverter.ToUInt32(fileSize2Bytes);

			KtsrHeaderValidator.Validate(signature, chunkTypeBytes, platformByte, gameIdBytes, fileSize1, fileSize2);

			return new KtsrHeader(versionByte, platformByte, fileSize1, GameInfo.ById(gameIdBytes)!);
		}
	}
}