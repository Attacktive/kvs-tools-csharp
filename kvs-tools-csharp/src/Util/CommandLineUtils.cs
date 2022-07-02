using System;
using KvsTools.Spec;

namespace KvsTools.Util
{
	public static class CommandLineUtils
	{
		public static (Command, string?, GameInfo?) Parse(params string[] arguments)
		{
			if (arguments.Length < 2)
			{
				return (Command.None, null, null);
			}

			var commandArgument = arguments[0];
			var pathToFile = arguments[1];
			GameInfo? gameInfo = null;

			Command command;
			switch (commandArgument.ToLowerInvariant())
			{
				case "extract":
					command = Command.Extract;
					break;
				case "archive":
					if (arguments.Length > 2)
					{
						command = Command.Archive;
						var gameInfoArgument = arguments[2];
						gameInfo = int.TryParse(gameInfoArgument, out var index)? GameInfo.ByIndex(index): GameInfo.ByName(gameInfoArgument);
					}
					else
					{
						return (Command.None, null, null);
					}

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

			return (command, pathToFile, gameInfo);
		}
	}

	public enum Command
	{
		None,
		Extract,
		Archive
	}
}