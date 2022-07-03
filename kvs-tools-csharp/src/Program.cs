using System;
using System.IO;
using KvsTools.Archive;
using KvsTools.Extract;
using KvsTools.Spec;
using KvsTools.Util;

namespace KvsTools
{
	internal static class Program
	{
		private static int Main(params string[] args)
		{
			var (command, pathToFile, gameInfo) = CommandLineUtils.Parse(args);

			switch (command)
			{
				case Command.Extract:
					var inputDirectoryName = new FileInfo(pathToFile!).DirectoryName!;
					var ktsrHeader = KtsrHeaderReader.ReadHeader(pathToFile!);
					var mediaFiles = MediaHeaderReader.Read(pathToFile!);
					MediaWriter.WriteToFile(ktsrHeader, mediaFiles, inputDirectoryName);
					break;
				case Command.Archive:
					var (generatedKtsrHeader, data) = MediaFileReader.ReadMediaFiles(pathToFile!, gameInfo!);
					ArchiveWriter.WriteToFile(generatedKtsrHeader, data, pathToFile!);
					break;
				default:
					Console.WriteLine(
						@$"Usage:
extract ./bgm.ktsl2stbin
archive ./directory/to/source/files <the index or the name of the game>
List of supported games:
{GameInfo.ValuesString()}"
					);

					return 1;
			}

			return 0;
		}
	}
}