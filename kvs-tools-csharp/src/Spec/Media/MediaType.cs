﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KvsTools.Spec.Media
{
	public class MediaType
	{
		public static readonly MediaType Kovs = new MediaType("KOVS", "kvs");
		public static readonly MediaType Ktss = new MediaType("KTSS", "kns");

		private static readonly List<MediaType> List = new List<MediaType>
		{
			Kovs, Ktss
		};

		public string Value { get; }
		public string Extension { get; }

		private MediaType(string value, string extension)
		{
			Value = value;
			Extension = extension;
		}

		public IEnumerable<byte> ToByteArray() => Encoding.UTF8.GetBytes(Value);

		public static MediaType? ByBytes(byte[] signatureBytes)
		{
			return List.FirstOrDefault(signature => signature.ToByteArray().SequenceEqual(signatureBytes));
		}
	}
}