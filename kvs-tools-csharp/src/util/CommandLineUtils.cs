using System;

namespace kvs_tools_csharp.util
{
	public static class CommandLineUtils
	{
		public static (Command, string?) Parse(params string[] arguments)
		{
			if (arguments.Length != 2)
			{
				throw new ArgumentException();
			}

			Command command;
			string? pathToFile;
			switch (arguments[0].ToLower())
			{
				case "extract":
					command = Command.Extract;
					pathToFile = arguments[1];
					break;
				case "archive":
					command = Command.Archive;
					// TODO
					pathToFile = "TODO";
					break;
				default:
					command = Command.None;
					pathToFile = null;
					break;
			}

			if (command == Command.None)
			{
				throw new ArgumentException();
			}

			return (command, pathToFile);
		}
	}

	public enum Command
	{
		None,
		Extract,
		Archive
	}
}