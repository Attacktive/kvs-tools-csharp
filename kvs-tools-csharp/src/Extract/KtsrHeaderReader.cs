using System;
using System.IO;
using KvsTools.Spec.Ktsr;

namespace KvsTools.Extract
{
	public static class KtsrHeaderReader
	{
		public static KtsrHeader ReadHeader(string pathToFile)
		{
			Console.WriteLine($"Trying to open {pathToFile}");

			using var fileStream = File.OpenRead(pathToFile);
			var buffer = new byte[KtsrHeader.NumberOfBytes];
			var bytesRead = fileStream.Read(buffer, 0, KtsrHeader.NumberOfBytes);
			if (bytesRead != KtsrHeader.NumberOfBytes)
			{
				throw new KtsrHeaderValidator.InvalidKtsrHeaderException(KtsrHeader.NumberOfBytes, bytesRead);
			}

			var ktsrHeader = KtsrHeader.Parse(buffer);
			Console.WriteLine($"ktsrHeader: {ktsrHeader}");

			return ktsrHeader;
		}
	}
}