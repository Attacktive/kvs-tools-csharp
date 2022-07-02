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
			Console.WriteLine($"Trying to write to {path}");
			File.WriteAllBytes(path, data);
		}
	}
}