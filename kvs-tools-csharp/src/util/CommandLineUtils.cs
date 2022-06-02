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

			var commandArgument = arguments[0];
			var pathArgument = arguments[1];

			Command command;
			string? pathToFile;
			switch (commandArgument.ToLowerInvariant())
			{
				case "extract":
					command = Command.Extract;
					pathToFile = pathArgument;
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
				throw new ArgumentException($"The 1st argument ({commandArgument}) must be either \"extract\" or \"archive\".");
			}

			if (pathToFile == null)
			{
				throw new ArgumentException($"The 2nd argument ({pathArgument}) is invalid.");
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