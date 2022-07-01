using System;
using System.Collections.Generic;
using System.Data;
using KvsTools.Extension;

namespace KvsTools.Spec.Media.Body
{
	public static class MediaBodyValidator
	{
		public static void Validate(byte[] mediaTypeBytes)
		{
			var mediaType = MediaType.ByBytes(mediaTypeBytes);
			if (mediaType == null)
			{
				throw new InvalidMediaTypeException(mediaTypeBytes);
			}
		}

		private class InvalidMediaTypeException : DataException
		{
			internal InvalidMediaTypeException(IEnumerable<byte> signature, Exception? cause = null) : base($"Unexpected file media type: {signature.ToHexString()}", cause) { }
		}
	}
}