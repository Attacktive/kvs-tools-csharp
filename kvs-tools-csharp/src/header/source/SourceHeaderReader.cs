using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using kvs_tools_csharp.extension;
using kvs_tools_csharp.header.ktsr;

namespace kvs_tools_csharp.header.source
{
	public static class SourceHeaderReader
	{
		public static IEnumerable<byte[]> ReadHeader(string pathToFile)
		{
			Console.WriteLine($"Trying to open {pathToFile}");

			var sourceFiles = new List<byte[]>();

			using var fileStream = File.OpenRead(pathToFile);
			using var binaryReader = new BinaryReader(fileStream);
			var index = KtsrHeader.NumberOfBytes;
			var headerBuffer = new byte[SourceHeader.NumberOfBytes];
			do
			{
				binaryReader.BaseStream.Seek(index, SeekOrigin.Begin);
				index += binaryReader.Read(headerBuffer, 0, SourceHeader.NumberOfBytes);

				var sourceHeader = ParseSourceHeader(headerBuffer);
				// fixme: Why is it needed?
				var actualFileSize = checked((int)sourceHeader.FileSize + 13);

				binaryReader.BaseStream.Seek(0, SeekOrigin.Current);
				var contentBuffer = new byte[actualFileSize];
				var bytesRead = binaryReader.Read(contentBuffer, 0, actualFileSize);
				if (bytesRead == 0)
				{
					break;
				}

				index += bytesRead;

				Console.WriteLine($"Source File #{sourceFiles.Count + 1}: {sourceHeader}");

				sourceFiles.Add(contentBuffer);
			} while (index <= fileStream.Length);

			return sourceFiles;
		}

		private static SourceHeader ParseSourceHeader(byte[] bytes)
		{
			var fileSizeBytes = bytes.Take(4).ToArray();
			var signatureBytes = bytes.Skip(16).Take(4).ToArray();
			var unknownBytes = bytes.Skip(24).Take(8).ToArray();

			var sourceSignature = SourceSignature.BySignatureBytes(signatureBytes);
			if (sourceSignature == null)
			{
				throw new InvalidSignatureException(signatureBytes);
			}

			var fileSize = BitConverter.ToUInt32(fileSizeBytes);

			return new SourceHeader(fileSize, sourceSignature, unknownBytes);
		}

		private class InvalidSignatureException : DataException
		{
			internal InvalidSignatureException(IEnumerable<byte> signature, Exception? cause = null) : base($"Unexpected file signature: {signature.ToHexString()}", cause) { }
		}
	}
}