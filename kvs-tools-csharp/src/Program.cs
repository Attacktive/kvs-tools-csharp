using System;
using kvs_tools_csharp.util;
using kvs_tools_csharp.util.header;

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
					KtsrHeaderReader.ReadHeader(pathToFile!);
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