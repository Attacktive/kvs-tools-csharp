using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KvsTools.Spec
{
	public class GameInfo
	{
		public static readonly GameInfo AttackOnTitan2 = new GameInfo(
			"Attack on Titan 2",
			"Attack on Titan 2",
			new byte[] { 0x36, 0x0e, 0xf4, 0x05 }
		);

		public static readonly GameInfo AtelierRyza1 = new GameInfo(
			"Atelier Ryza 1",
			"Atelier Ryza 1",
			new byte[] { 0x4e, 0xc5, 0xe8, 0xc5 }
		);

		public static readonly GameInfo AtelierRyza2 = new GameInfo(
			"Atelier Ryza 2",
			"Atelier Ryza 2",
			new byte[] { 0xa9, 0x05, 0x49, 0xaf }
		);

		public static readonly GameInfo BlueReflectionSecondLight = new GameInfo(
			"BLUE REFLECTION: Second Light",
			"BLUE REFLECTION： Second Light",
			new byte[] { 0x11, 0x9d, 0xb0, 0x05 }
		);

		private static readonly List<GameInfo> Values = new List<GameInfo>
		{
			AttackOnTitan2, AtelierRyza1, AtelierRyza2, BlueReflectionSecondLight
		};

		public string Name { get; }
		public string FileSystemCompatibleName { get; }
		/// <summary>
		/// 4 bytes
		/// </summary>
		public byte[] Id { get; }

		private GameInfo(string name, string fileSystemCompatibleName, byte[] id)
		{
			Name = name;
			FileSystemCompatibleName = fileSystemCompatibleName;
			Id = id;
		}

		public static string ValuesString()
		{
			var stringBuilder = new StringBuilder();
			for (var i = 0; i < Values.Count; i++)
			{
				stringBuilder.AppendFormat("{0}: {1}\n", i, Values[i].Name);
			}

			return stringBuilder.ToString().TrimEnd();
		}

		public static GameInfo? ById(byte[] id)
		{
			return Values.FirstOrDefault(game => game.Id.SequenceEqual(id));
		}

		public static GameInfo? ByName(string name)
		{
			return Values.FirstOrDefault(game => game.Name.Equals(name, StringComparison.OrdinalIgnoreCase) || game.FileSystemCompatibleName.Equals(name, StringComparison.OrdinalIgnoreCase));
		}

		public static GameInfo ByIndex(int index)
		{
			return Values[index];
		}
	}
}