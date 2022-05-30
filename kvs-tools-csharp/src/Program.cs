using System;
using kvs_tools_csharp.util;

namespace kvs_tools_csharp
{
	internal static class Program
	{
		private static int Main(params string[] args)
		{
			var (command, pathToFile) = CommandLineUtils.Parse(args);
			if (command == Command.None)
			{
				Console.WriteLine(
					@"Usage:
extract ./bgm.ktsl2stbin
archive ./directory/to/source/files"
				);

				return 1;
			}

			// TODO

			return 0;
		}
	}
}