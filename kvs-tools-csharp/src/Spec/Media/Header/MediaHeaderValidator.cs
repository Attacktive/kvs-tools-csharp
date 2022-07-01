using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using KvsTools.Extension;

namespace KvsTools.Spec.Media.Header
{
	public static class MediaHeaderValidator
	{
		public static void Validate(byte[] config)
		{
			if (!config.SequenceEqual(MediaHeader.DefaultConfig))
			{
				throw new InvalidConfigException(config);
			}
		}

		private class InvalidConfigException : DataException
		{
			internal InvalidConfigException(IEnumerable<byte> config, Exception? cause = null) : base($"Unexpected file config: {config.ToHexString()}. It has to be {MediaHeader.DefaultConfig.ToHexString()}.", cause) { }
		}
	}
}