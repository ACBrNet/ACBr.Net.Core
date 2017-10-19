// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-24-2016
//
// Last Modified By : RFTD
// Last Modified On : 03-24-2016
// ***********************************************************************
// <copyright file="StaticRandom.cs" company="ACBr.Net">
//		        		   The MIT License (MIT)
//	     		    Copyright (c) 2016 Grupo ACBr.Net
//
//	 Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//	 The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//	 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace ACBr.Net.Core
{
	/// <summary>
	/// Thread-safe equivalent of System.Random, using just static methods.
	/// If all you want is a source of random numbers, this is an easy class to
	/// use. If you need to specify your own seeds (eg for reproducible sequences
	/// of numbers), use System.Random.
	/// </summary>
	public static class StaticRandom
	{
		#region Fields

		private static readonly Random random;
		private static readonly object myLock;

		#endregion Fields

		#region Constructors

		static StaticRandom()
		{
			random = new Random((int)DateTime.Now.TimeOfDay.TotalMilliseconds);
			myLock = new object();
		}

		#endregion Constructors

		#region Methods

		/// <summary>
		/// Returns a nonnegative random number.
		/// </summary>
		/// <returns>A 32-bit signed integer greater than or equal to zero and less than Int32.MaxValue.</returns>
		public static int Next()
		{
			lock (myLock)
			{
				return random.Next();
			}
		}

		/// <summary>
		/// Returns a nonnegative random number less than the specified maximum.
		/// </summary>
		/// <param name="max">The maximum.</param>
		/// <returns>A 32-bit signed integer greater than or equal to zero, and less than maxValue;
		/// that is, the range of return values includes zero but not maxValue.</returns>
		/// <exception cref="ArgumentOutOfRangeException">maxValue is less than zero.</exception>
		public static int Next(int max)
		{
			lock (myLock)
			{
				return random.Next(max);
			}
		}

		/// <summary>
		/// Returns a random number within a specified range.
		/// </summary>
		/// <param name="min">The inclusive lower bound of the random number returned.</param>
		/// <param name="max">The exclusive upper bound of the random number returned.
		/// maxValue must be greater than or equal to minValue.</param>
		/// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue;
		/// that is, the range of return values includes minValue but not maxValue.
		/// If minValue equals maxValue, minValue is returned.</returns>
		/// <exception cref="ArgumentOutOfRangeException">minValue is greater than maxValue.</exception>
		public static int Next(int min, int max)
		{
			lock (myLock)
			{
				return random.Next(min, max);
			}
		}

		/// <summary>
		/// Returns a random number between 0.0 and 1.0.
		/// </summary>
		/// <returns>A double-precision floating point number greater than or equal to 0.0, and less than 1.0.</returns>
		public static double NextDouble()
		{
			lock (myLock)
			{
				return random.NextDouble();
			}
		}

		/// <summary>
		/// Fills the elements of a specified array of bytes with random numbers.
		/// </summary>
		/// <param name="buffer">An array of bytes to contain random numbers.</param>
		/// <exception cref="ArgumentNullException">buffer is a null reference (Nothing in Visual Basic).</exception>
		public static void NextBytes(byte[] buffer)
		{
			lock (myLock)
			{
				random.NextBytes(buffer);
			}
		}

		#endregion Methods
	}
}