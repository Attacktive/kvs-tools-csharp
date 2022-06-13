using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KvsTools.header.ktsr;

namespace KvsTools.header.source
{
	public static class SourceWriter
	{
		public static void WriteSources(KtsrHeader ktsrHeader, IEnumerable<byte[]> sourceFiles)
		{
			var list = sourceFiles.ToList();
			var size = list.Count;
			var numberOfDigits = Math.Max(size.ToString().Length, 2);
			for (var i = 0; i < size; i++)
			{
				var currentBytes = list[i];

				// TODO: the extension must be taken from the source header.
				var outputFileName = $"{ktsrHeader.Game.Name}-{i.ToString().PadLeft(numberOfDigits, '0')}.kvs";
				File.WriteAllBytes(outputFileName, currentBytes);
			}
		}
	}
}