// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-24-2016
//
// Last Modified By : RFTD
// Last Modified On : 03-24-2016
// ***********************************************************************
// <copyright file="LongExtensions.cs" company="ACBr.Net">
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
namespace ACBr.Net.Core.Extensions
{
	/// <summary>
	/// Class LongExtensions.
	/// </summary>
	public static class LongExtensions
	{
		/// <summary>
		/// Determines whether the specified n is divisble for x.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="n">The n.</param>
		/// <returns><c>true</c> if the specified x is divisble; otherwise, <c>false</c>.</returns>
		public static bool IsDivisible(this long x, int n)
		{
			return (x % n) == 0;
		}

		/// <summary>
		/// Determines whether the specified value is odd.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the specified value is odd; otherwise, <c>false</c>.</returns>
		public static bool IsOdd(this long value)
		{
			return value % 2 != 0;
		}

		/// <summary>
		/// Zeroes the fill.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="length">The length.</param>
		/// <returns>System.String.</returns>
		public static string ZeroFill(this long? value, int length)
		{
			return value.HasValue ? value.Value.ToString().ZeroFill(length) : "".ZeroFill(length);
		}

		/// <summary>
		/// Zeroes the fill.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="length">The length.</param>
		/// <returns>System.String.</returns>
		public static string ZeroFill(this long value, int length)
		{
			return ((int?)value).ZeroFill(length);
		}
	}
}