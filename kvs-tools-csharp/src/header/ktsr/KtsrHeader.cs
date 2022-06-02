using System.Collections.Generic;
using System.Linq;
using kvs_tools_csharp.extension;

namespace kvs_tools_csharp.header.ktsr
{
	public class KtsrHeader
	{
		public static readonly int NumberOfBytes = 80;
		public static readonly string DefaultSignature = "KTSR";
		public static readonly byte[] DefaultChunkType = { 0x02, 0x94, 0xdd, 0xfc };

		public string Signature { get; }

		public byte[] ChunkType { get; }

		public byte Version { get; }

		public byte Platform { get; }

		public uint FileSize { get; }

		public Game Game { get; }

		public KtsrHeader(string signature, byte[] chunkType, byte version, byte platform, uint fileSize, Game game)
		{
			Signature = signature;
			ChunkType = chunkType;
			Version = version;
			Platform = platform;
			FileSize = fileSize;
			Game = game;
		}

		public override string ToString()
		{
			return $"Signature: {Signature}, ChunkType: {ChunkType.ToHexString()}, Version: {Version}, Platform: {Platform}, FileSize: {FileSize}, GameId: {Game.Id.ToHexString()}, GameEntries: {Game.Entries.ToHexString()}";
		}
	}

	public class Game
	{
		public static readonly Game AttackOnTitan2 = new Game
		(
			new byte[] { 0x09, 0xd4, 0xf4, 0x15 },
			new byte[]
			{
				0x09, 0xd4, 0xf4, 0x15,
				0x20, 0xe9, 0x88, 0x00,
				0xca, 0xab, 0xa8, 0xa9,
				0x20, 0x00, 0x00, 0x00
			}
		);

		public static readonly Game AtelierRyza1 = new Game
		(
			new byte[] { 0x4e, 0xc5, 0xe8, 0xc5 },
			new byte[]
			{
				0x09, 0xd4, 0xf4, 0x15,
				0xf0, 0x2c, 0x1b, 0x00,
				0x00, 0x70, 0x4c, 0x41,
				0x20, 0x00, 0x00, 0x00
			}
		);

		private static readonly List<Game> List = new List<Game>
		{
			AttackOnTitan2, AtelierRyza1
		};

		public byte[] Id { get; }
		public byte[] Entries { get; }

		private Game(byte[] id, byte[] entries)
		{
			Id = id;
			Entries = entries;
		}

		public static Game? ById(byte[] id)
		{
			return List.FirstOrDefault(game => game.Id.SequenceEqual(id));
		}
	}
}