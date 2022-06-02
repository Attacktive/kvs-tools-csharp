using System;
using System.IO;
using System.Linq;
using System.Text;

namespace kvs_tools_csharp.util.header
{
	public static class KtsrHeaderReader
	{
		public static KtsrHeader ReadHeader(string pathToFile)
		{
			Console.WriteLine($"Trying to open {pathToFile}");

			using var source = File.OpenRead(pathToFile);
			var buffer = new byte[KtsrHeader.NumberOfBytes];
			var bytesRead = source.Read(buffer, 0, KtsrHeader.NumberOfBytes);
			if (bytesRead != KtsrHeader.NumberOfBytes)
			{
				throw new KtsrHeaderValidator.InvalidKtsrHeaderException(KtsrHeader.NumberOfBytes, bytesRead);
			}

			var ktsrHeader = ParseKtsrHeader(buffer);
			Console.WriteLine($"ktsrHeader: {ktsrHeader}");

			return ktsrHeader;
		}

		private static KtsrHeader ParseKtsrHeader(byte[] bytes, bool toValidate = true)
		{
			var signatureBytes = bytes.Take(4).ToArray();
			var chunkTypeBytes = bytes.Skip(4).Take(4).ToArray();
			var versionByte = bytes[8];
			var platformByte = bytes[11];
			var gameIdBytes = bytes.Skip(12).Take(4).ToArray();
			var fileSize1Bytes = bytes.Skip(24).Take(4).ToArray();
			var fileSize2Bytes = bytes.Skip(28).Take(4).ToArray();
			var gameEntriesBytes = bytes.Skip(64).Take(32).ToArray();

			var signature = Encoding.UTF8.GetString(signatureBytes);
			var fileSize1 = BitConverter.ToUInt32(fileSize1Bytes);
			var fileSize2 = BitConverter.ToUInt32(fileSize2Bytes);

			if (toValidate)
			{
				KtsrHeaderValidator.Validate(signature, chunkTypeBytes, platformByte, gameIdBytes, fileSize1, fileSize2, gameEntriesBytes);
			}

			return new KtsrHeader(signature, chunkTypeBytes, versionByte, platformByte, fileSize1, Game.ById(gameIdBytes)!);
		}
	}
}