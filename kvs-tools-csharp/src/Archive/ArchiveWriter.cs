using System;
using System.IO;
using KvsTools.Spec.Ktsr;

namespace KvsTools.Archive
{
	public static class ArchiveWriter
	{
		public static void WriteToFile(KtsrHeader ktsrHeader, byte[] data, string inputDirectoryName)
		{
			var path = Path.Combine(inputDirectoryName, $"{ktsrHeader.Game.FileSystemCompatibleName}.ktsl2stbin");
			var absolutePath = Path.GetFullPath(path);
			Console.WriteLine($"Trying to write to {absolutePath}");

			File.WriteAllBytes(path, data);

			Console.WriteLine($"Finished writing to {absolutePath}");
		}
	}
}