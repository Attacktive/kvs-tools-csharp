using System;
using System.Linq;
using System.Text;
using KvsTools.Util;

namespace KvsTools.Spec.Media.Body
{
	public class MediaBody
	{
		/// <summary>
		/// 4 bytes
		/// </summary>
		public MediaType MediaType { get; }

		/// <summary>
		/// 4 bytes
		/// </summary>
		public uint Size { get; }

		public byte[] Content { get; }

		private MediaBody(MediaType mediaType, uint size, byte[] content)
		{
			MediaType = mediaType;
			Size = size;
			Content = content;
		}

		public override string ToString()
		{
			return $"MediaType: {MediaType.Value}, Size: {Size}";
		}

		public byte[] ToBytes() => Encoding.UTF8.GetBytes(MediaType.Value)
			.Concat(BitConverter.GetBytes(Size))
			.Concat(Content)
			.ToArray();

		public static MediaBody Parse(byte[] bytes, bool toValidate)
		{
			var mediaTypeBytes = bytes.Take(4).ToArray();
			var sizeBytes = bytes.Skip(4).Take(4).ToArray();
			var content = bytes.Skip(8).ToArray();

			if (toValidate)
			{
				MediaBodyValidator.Validate(mediaTypeBytes);
			}

			var mediaType = MediaType.ByBytes(mediaTypeBytes)!;
			var size = BitConverter.ToUInt32(sizeBytes);

			return new MediaBody(mediaType, size, content);
		}
	}
}