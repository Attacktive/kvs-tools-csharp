﻿using System;
using System.Data;
using System.Linq;
using kvs_tools_csharp.extension;

namespace kvs_tools_csharp.util.header
{
	public static class KtsrHeaderValidator
	{
		private static readonly byte[] ExpectedPlatforms = { 0x01, 0x03, 0x04 };

		public static void Validate(string signature, byte[] chunkType, byte platform, byte[] gameId, uint fileSize1, uint fileSize2, byte[] gameEntries)
		{
			if (signature != KtsrHeader.DefaultSignature)
			{
				throw new InvalidSignatureException(signature);
			}

			if (!chunkType.SequenceEqual(KtsrHeader.DefaultChunkType))
			{
				throw new InvalidChunkTypeException(chunkType);
			}

			if (!ExpectedPlatforms.Contains(platform))
			{
				throw new InvalidPlatformException(platform);
			}

			if (fileSize1 != fileSize2)
			{
				throw new InconsistentFileSizeException(fileSize1, fileSize2);
			}

			var game = Game.ById(gameId);
			if (game == null)
			{
				throw new InvalidGameIdException(gameId);
			}

			if (!gameEntries.SequenceEqual(game.Entries))
			{
				throw new InvalidGameEntriesException();
			}
		}

		private class InvalidSignatureException : DataException
		{
			internal InvalidSignatureException(string signature, Exception? cause = null) : base($"Unexpected file signature: {signature}", cause) { }
		}

		private class InvalidChunkTypeException : DataException
		{
			internal InvalidChunkTypeException(byte[] chunkType, Exception? cause = null) : base($"Unexpected chunk type: {chunkType.ToHexString()}", cause) { }
		}

		private class InvalidPlatformException : DataException
		{
			internal InvalidPlatformException(byte platform, Exception? cause = null) : base($"Unexpected platform: {platform}", cause) { }
		}

		private class InconsistentFileSizeException : DataException
		{
			internal InconsistentFileSizeException(uint fileSize1, uint fileSize2, Exception? cause = null) : base($"The file size data is inconsistent: {fileSize1} vs {fileSize2}", cause) { }
		}

		private class InvalidGameIdException : DataException
		{
			internal InvalidGameIdException(byte[] gameId, Exception? cause = null) : base($"Unexpected game ID: {gameId.ToHexString()}", cause) { }
		}

		private class InvalidGameEntriesException : DataException
		{
			internal InvalidGameEntriesException(Exception? cause = null) : base("The game entries data does not agree with the id", cause) { }
		}
	}
}