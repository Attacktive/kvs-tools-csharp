using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using kvs_tools_csharp.extension;
using kvs_tools_csharp.util.header;

namespace kvs_tools_csharp.header
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

				binaryReader.BaseStream.Seek(0, SeekOrigin.Current);
				var contentBuffer = new byte[sourceHeader.FileSize];
				index += binaryReader.Read(contentBuffer, 0, checked((int)sourceHeader.FileSize));

				sourceFiles.Add(contentBuffer);
			} while (index <= fileStream.Length);

			return sourceFiles;
		}

		private static SourceHeader ParseSourceHeader(byte[] bytes)
		{
			var signatureBytes = bytes.Take(4).ToArray();
			var fileSizeBytes = bytes.Skip(4).Take(4).ToArray();
			var unknownBytes = bytes.Skip(8).Take(4).ToArray();

			var sourceSignature = SourceSignature.BySignatureBytes(signatureBytes);
			if (sourceSignature == null)
			{
				throw new InvalidSignatureException(signatureBytes);
			}

			var fileSize = BitConverter.ToUInt32(fileSizeBytes);
			fileSize += 32;

			return new SourceHeader(sourceSignature, fileSize, unknownBytes);
		}

		private class InvalidSignatureException : DataException
		{
			internal InvalidSignatureException(byte[] signature, Exception? cause = null) : base($"Unexpected file signature: {signature.ToHexString()}", cause) { }
		}
	}
}