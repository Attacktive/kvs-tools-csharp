using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KvsTools.Header.Ktsr;

namespace KvsTools.Header.Source
{
	public static class SourceWriter
	{
		public static void WriteSources(KtsrHeader ktsrHeader, IEnumerable<byte[]> sourceFiles, string directoryName, SourceHeader sourceHeader)
		{
			var list = sourceFiles.ToList();
			var size = list.Count;
			var numberOfDigits = Math.Max(size.ToString().Length, 2);
			for (var i = 0; i < size; i++)
			{
				var currentBytes = list[i];

				var outputFileName = $"{directoryName}{Path.DirectorySeparatorChar}{ktsrHeader.Game.Name}-{i.ToString().PadLeft(numberOfDigits, '0')}.{sourceHeader.Signature.Extension}";
				File.WriteAllBytes(outputFileName, currentBytes);
			}
		}
	}
}