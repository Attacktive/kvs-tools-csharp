using System;
using System.IO;
using System.Linq;
using KvsTools.Spec.Media;

namespace KvsTools.Archive
{
	public static class MediaFileReader
	{
		public static void ReadMediaFiles(string path)
		{
			Console.WriteLine($"Trying to open files from {path}");

			var files = Directory.EnumerateFiles(path).Where(MediaType.IsMediaFile);
			Console.WriteLine($"files:\n{string.Join("\n", files)}");
		}
	}
}