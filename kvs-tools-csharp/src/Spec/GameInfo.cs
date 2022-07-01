using System.Collections.Generic;
using System.Linq;
using KvsTools.Spec.Media.Header;

namespace KvsTools.Spec
{
	public class GameInfo
	{
		public static readonly GameInfo AttackOnTitan2 = new GameInfo(
			"Attack on Titan 2",
			new byte[] { 0x36, 0x0e, 0xf4, 0x05 }, MediaHeader.DefaultConfig.Concat(
				new byte[]
				{
					0x20, 0xe9, 0x88, 0x00,
					0xca, 0xab, 0xa8, 0xa9,
					0x20, 0x00, 0x00, 0x00
				}
			).ToArray()
		);

		public static readonly GameInfo AtelierRyza1 = new GameInfo(
			"Atelier Ryza 1",
			new byte[] { 0x4e, 0xc5, 0xe8, 0xc5 }, MediaHeader.DefaultConfig.Concat(
				new byte[]
				{
					0xf0, 0x2c, 0x1b, 0x00,
					0x00, 0x70, 0x4c, 0x41,
					0x20, 0x00, 0x00, 0x00
				}
			).ToArray()
		);

		private static readonly List<GameInfo> List = new List<GameInfo>
		{
			AttackOnTitan2, AtelierRyza1
		};

		public string Name { get; }
		/// <summary>
		/// 4 bytes
		/// </summary>
		public byte[] Id { get; }
		public byte[] Entries { get; }

		private GameInfo(string name, byte[] id, byte[] entries)
		{
			Name = name;
			Id = id;
			Entries = entries;
		}

		public static GameInfo? ById(byte[] id)
		{
			return List.FirstOrDefault(game => game.Id.SequenceEqual(id));
		}
	}
}