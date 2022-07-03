using System;
using System.Collections.Generic;
using System.IO;
using KvsTools.Spec.Ktsr;
using KvsTools.Spec.Media;
using KvsTools.Spec.Media.Body;
using KvsTools.Spec.Media.Header;

namespace KvsTools.Extract
{
	public static class MediaHeaderReader
	{
		public static IEnumerable<MediaFile> Read(string pathToFile)
		{
			Console.WriteLine($"Trying to open {pathToFile}");

			var mediaFiles = new List<MediaFile>();

			using var fileStream = File.OpenRead(pathToFile);
			using var binaryReader = new BinaryReader(fileStream);
			var index = KtsrHeader.NumberOfBytes;
			var headerBuffer = new byte[MediaHeader.NumberOfBytes];
			do
			{
				binaryReader.BaseStream.Seek(index, SeekOrigin.Begin);
				var bytesRead = binaryReader.Read(headerBuffer, 0, MediaHeader.NumberOfBytes);
				if (bytesRead == 0)
				{
					break;
				}

				index += bytesRead;

				var mediaHeader = MediaHeader.Parse(headerBuffer);
				var bodySize = checked((int)mediaHeader.FileSize - MediaHeader.NumberOfBytes);

				var contentBuffer = new byte[bodySize];
				bytesRead = binaryReader.Read(contentBuffer, 0, bodySize);
				index += bytesRead;

				var mediaBody = MediaBody.Parse(contentBuffer);

				var mediaFile = new MediaFile(mediaHeader, mediaBody);
				mediaFiles.Add(mediaFile);
			} while (index <= fileStream.Length);

			return mediaFiles;
		}
	}
}