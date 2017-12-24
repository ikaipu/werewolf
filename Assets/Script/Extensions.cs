using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

public static class Extensions {
	// reffer this page: https://stackoverflow.com/questions/273313/randomize-a-listt
	public static List<T> Shuffle<T>(this List<T> list) {
		RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
		int n = list.Count;
		List<T> shuffledList = list;
		while (n > 1)
		{
			byte[] box = new byte[1];
			do provider.GetBytes(box);
			while (!(box[0] < n * (Byte.MaxValue / n)));
			int k = (box[0] % n);
			n--;
			T value = shuffledList[k];
			shuffledList[k] = shuffledList[n];
			shuffledList[n] = value;
		}
		return shuffledList;
	}
}
