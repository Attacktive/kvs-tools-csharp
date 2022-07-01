using System;
using System.IO;
using KvsTools.Extract;
using KvsTools.Util;

namespace KvsTools
{
	internal static class Program
	{
		private static int Main(params string[] args)
		{
			var (command, pathToFile) = CommandLineUtils.Parse(args);
			switch (command)
			{
				case Command.None:
					Console.WriteLine(
						@"Usage:
extract ./bgm.ktsl2stbin
archive ./directory/to/source/files"
					);

					return 1;
				case Command.Extract:
					var ktsrHeader = KtsrHeaderReader.ReadHeader(pathToFile!);
					var mediaFiles = MediaHeaderReader.Read(pathToFile!);
					var inputDirectoryName = new FileInfo(pathToFile!).DirectoryName;
					MediaWriter.WriteToFile(ktsrHeader, mediaFiles, inputDirectoryName!);
					break;
				case Command.Archive:
				default:
					// TODO
					break;
			}

			return 0;
		}
	}
}