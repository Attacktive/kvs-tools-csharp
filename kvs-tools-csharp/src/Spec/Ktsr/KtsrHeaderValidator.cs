using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using KvsTools.Extension;

namespace KvsTools.Spec.Ktsr
{
	public static class KtsrHeaderValidator
	{
		private static readonly byte[] ExpectedPlatforms = { 0x01, 0x03, 0x04 };

		public static void Validate(string signature, byte[] chunkType, byte platform, byte[] gameId, uint fileSize1, uint fileSize2)
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

			var game = GameInfo.ById(gameId);
			if (game == null)
			{
				throw new InvalidGameIdException(gameId);
			}
		}

		public class InvalidKtsrHeaderException : DataException
		{
			public InvalidKtsrHeaderException(int expectedNumberOfBytes, int bytesRead) : base($"Expected to read {expectedNumberOfBytes} bytes but actually did {bytesRead}.") { }
		}

		private class InvalidSignatureException : DataException
		{
			internal InvalidSignatureException(string signature, Exception? cause = null) : base($"Unexpected file signature: {signature}", cause) { }
		}

		private class InvalidChunkTypeException : DataException
		{
			internal InvalidChunkTypeException(IEnumerable<byte> chunkType, Exception? cause = null) : base($"Unexpected chunk type: {chunkType.ToHexString()}", cause) { }
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
			internal InvalidGameIdException(IEnumerable<byte> gameId, Exception? cause = null) : base($"Unexpected game ID: {gameId.ToHexString()}", cause) { }
		}
	}
}