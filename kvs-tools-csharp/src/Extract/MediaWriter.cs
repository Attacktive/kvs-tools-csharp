using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KvsTools.Spec.Ktsr;
using KvsTools.Spec.Media;

namespace KvsTools.Extract
{
	public static class MediaWriter
	{
		public static void WriteToFile(KtsrHeader ktsrHeader, IEnumerable<MediaFile> mediaFiles, string inputDirectoryName)
		{
			var directoryPath = Path.Combine(inputDirectoryName, ktsrHeader.Game.FileSystemCompatibleName);
			Directory.CreateDirectory(directoryPath);

			var list = mediaFiles.ToList();
			var size = list.Count;
			var numberOfDigits = Math.Max(size.ToString().Length, 2);
			var fileNamePrefix = Path.Combine(directoryPath, ktsrHeader.Game.FileSystemCompatibleName);
			for (var i = 0; i < size; i++)
			{
				var mediaFile = list[i];
				var outputFileName = $"{fileNamePrefix}-{i.ToString().PadLeft(numberOfDigits, '0')}.{mediaFile.Body.MediaType.Extension}";

				File.WriteAllBytes(outputFileName, mediaFile.ToBytes());
				Console.WriteLine($"{outputFileName}: {mediaFile.Header}");
			}
		}
	}
}