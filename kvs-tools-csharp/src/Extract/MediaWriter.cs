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
		public static void WriteToFile(KtsrHeader ktsrHeader, IEnumerable<MediaFile> mediaFiles, string directoryName)
		{
			var list = mediaFiles.ToList();
			var size = list.Count;
			var numberOfDigits = Math.Max(size.ToString().Length, 2);
			for (var i = 0; i < size; i++)
			{
				var mediaFile = list[i];

				var outputFileName = $"{directoryName}{Path.DirectorySeparatorChar}{ktsrHeader.Game.Name}-{i.ToString().PadLeft(numberOfDigits, '0')}.{mediaFile.Body.MediaType.Extension}";
				File.WriteAllBytes(outputFileName, mediaFile.ToBytes());
			}
		}
	}
}