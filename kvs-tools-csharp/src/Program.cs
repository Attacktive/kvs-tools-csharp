using System;
using kvs_tools_csharp.header.ktsr;
using kvs_tools_csharp.header.source;
using kvs_tools_csharp.util;

namespace kvs_tools_csharp
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