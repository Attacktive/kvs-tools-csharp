using System;
using System.IO;
using KvsTools.Header.Ktsr;
using KvsTools.Header.Source;
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
					var (sourceFiles, sourceHeader) = SourceHeaderReader.ReadHeader(pathToFile!);
					var directoryName = new FileInfo(pathToFile!).DirectoryName;
					SourceWriter.WriteSources(ktsrHeader, sourceFiles, directoryName!, sourceHeader);
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