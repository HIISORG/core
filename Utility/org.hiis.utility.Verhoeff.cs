﻿using System;

namespace org.hiis.Utility {
	public static class Verhoeff {
		#region Define variables
		// The multiplication table
		static int[,] d = new int[,] {
			{0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
			{1, 2, 3, 4, 0, 6, 7, 8, 9, 5},
			{2, 3, 4, 0, 1, 7, 8, 9, 5, 6},
			{3, 4, 0, 1, 2, 8, 9, 5, 6, 7},
			{4, 0, 1, 2, 3, 9, 5, 6, 7, 8},
			{5, 9, 8, 7, 6, 0, 4, 3, 2, 1},
			{6, 5, 9, 8, 7, 1, 0, 4, 3, 2},
			{7, 6, 5, 9, 8, 2, 1, 0, 4, 3},
			{8, 7, 6, 5, 9, 3, 2, 1, 0, 4},
			{9, 8, 7, 6, 5, 4, 3, 2, 1, 0}
		};

		// The permutation table
		static int[,] p = new int[,] {
			{0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
			{1, 5, 7, 6, 2, 8, 3, 0, 9, 4},
			{5, 8, 0, 3, 7, 9, 6, 1, 4, 2},
			{8, 9, 1, 6, 0, 4, 3, 5, 2, 7},
			{9, 4, 5, 3, 1, 2, 6, 8, 7, 0},
			{4, 2, 8, 6, 5, 7, 3, 9, 0, 1},
			{2, 7, 9, 3, 8, 0, 6, 4, 1, 5},
			{7, 0, 4, 6, 9, 1, 3, 2, 5, 8}
		};

		// The inverse table
		static int[] inv = { 0, 4, 3, 2, 1, 5, 6, 7, 8, 9 };
		#endregion

		/// <summary>
		/// Validates that an entered number is Verhoeff compliant.
		/// NB: Make sure the check digit is the last one!
		/// </summary>
		/// <param name="num"></param>
		/// <returns>True if Verhoeff compliant, otherwise false</returns>
		public static bool Validate(string number) {
			int c = 0;
			int[] myArray = StringToReversedIntArray(number);

			for (int i = 0; i < myArray.Length; i++) {
				c = d[c, p[(i % 8), myArray[i]]];
			}

			return c == 0;
		}

		/// <summary>
		/// For a given number generates a Verhoeff digit
		/// Append this check digit to num
		/// </summary>
		/// <param name="num"></param>
		/// <returns>Verhoeff check digit as string</returns>
		public static string GenerateCheckSum(string number) {
			int c = 0;
			int[] myArray = StringToReversedIntArray(number);

			for (int i = 0; i < myArray.Length; i++) {
				c = d[c, p[((i + 1) % 8), myArray[i]]];
			}

			return inv[c].ToString();
		}

		/// <summary>
		/// Converts a string to a reversed integer array.
		/// </summary>
		/// <param name="num"></param>
		/// <returns>Reversed integer array</returns>
		private static int[] StringToReversedIntArray(string number) {
			int[] myArray = new int[number.Length];

			for (int i = 0; i < number.Length; i++) {
				myArray[i] = int.Parse(number.Substring(i, 1));
			}

			Array.Reverse(myArray);

			return myArray;
		}
	}
}
