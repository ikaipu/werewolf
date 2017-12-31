using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Script
{
	public static class Extensions {
		// refer this page: https://stackoverflow.com/questions/273313/randomize-a-listt
		public static List<T> Shuffle<T>(this List<T> list) {
			var provider = new RNGCryptoServiceProvider();
			var n = list.Count;
			var shuffledList = list;
			while (n > 1)
			{
				var box = new byte[1];
				do provider.GetBytes(box);
				while (!(box[0] < n * (byte.MaxValue / n)));
				var k = (box[0] % n);
				n--;
				var value = shuffledList[k];
				shuffledList[k] = shuffledList[n];
				shuffledList[n] = value;
			}
			return shuffledList;
		}
	}
}
