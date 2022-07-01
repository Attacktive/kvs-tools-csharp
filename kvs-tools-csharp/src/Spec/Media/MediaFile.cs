using System.Linq;
using KvsTools.Spec.Media.Body;
using KvsTools.Spec.Media.Header;

namespace KvsTools.Spec.Media
{
	public class MediaFile
	{
		public MediaHeader Header { get; }
		public MediaBody Body { get; }

		public byte[] ToBytes() => Header.ToBytes().Concat(Body.ToBytes()).ToArray();

		public MediaFile(MediaHeader header, MediaBody body)
		{
			Header = header;
			Body = body;
		}

		public override string ToString()
		{
			return $"Header: {Header}, Body: {Body}";
		}
	}
}