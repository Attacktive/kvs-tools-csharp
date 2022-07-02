using System;

namespace KvsTools.Util
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
			var pathToFile = arguments[1];

			Command command;
			switch (commandArgument.ToLowerInvariant())
			{
				case "extract":
					command = Command.Extract;
					break;
				case "archive":
					command = Command.Archive;
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
				throw new ArgumentException($"The 2nd argument ({pathToFile}) is invalid.");
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