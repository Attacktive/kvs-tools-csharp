using System;
using System.IO;
using KvsTools.header.ktsr;
using KvsTools.header.source;
using KvsTools.util;

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
					var sourceFiles = SourceHeaderReader.ReadHeader(pathToFile!);
					var directoryName = new FileInfo(pathToFile!).DirectoryName;
					SourceWriter.WriteSources(ktsrHeader, sourceFiles, directoryName!);
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