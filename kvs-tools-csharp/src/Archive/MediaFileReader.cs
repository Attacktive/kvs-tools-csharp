using System;
using System.IO;
using System.Linq;
using KvsTools.Spec;
using KvsTools.Spec.Ktsr;
using KvsTools.Spec.Media;

namespace KvsTools.Archive
{
	public static class MediaFileReader
	{
		public static (KtsrHeader, byte[]) ReadMediaFiles(string path, GameInfo gameInfo)
		{
			Console.WriteLine($"Trying to open files from {path}");

			var files = Directory.EnumerateFiles(path).Where(MediaType.IsMediaFile);
			var mediaSize = checked((int)files.Select(file => new FileInfo(file)).Sum(file => file.Length));
			var totalSize = (uint)checked(KtsrHeader.NumberOfBytes + mediaSize);

			var ktsrHeader = new KtsrHeader(totalSize, gameInfo);
			Console.WriteLine($"KTSR header generated: {ktsrHeader}");

			using var memoryStream = new MemoryStream(mediaSize);
			using var binaryWriter = new BinaryWriter(memoryStream);

			binaryWriter.Write(ktsrHeader.Bytes);
			foreach (var file in files)
			{
				var bytes = File.ReadAllBytes(file);
				binaryWriter.Write(bytes);
			}

			var data = memoryStream.ToArray();

			return (ktsrHeader, data);
		}
	}
}