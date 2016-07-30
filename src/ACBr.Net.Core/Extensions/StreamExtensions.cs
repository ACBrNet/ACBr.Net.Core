// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 05-13-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="StreamExtensions.cs" company="ACBr.Net">
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
using System.IO;

namespace ACBr.Net.Core.Extensions
{
	/// <summary>
	/// Class StreamExtensions.
	/// </summary>
	public static class StreamExtensions
	{
		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="destination">The destination.</param>
		/// <param name="bufferSize">Size of the buffer.</param>
		public static void CopyTo(this Stream input, Stream destination, int bufferSize = 1048576)
		{
			var buffer = new byte[bufferSize];
			int read;
			do
			{
				read = input.Read(buffer, 0, bufferSize);
				destination.Write(buffer, 0, read);
			} while (read > 0);
		}

		/// <summary>
		/// Ares the equal.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="other">The other.</param>
		/// <returns><c>true</c> if stream are equals, <c>false</c> otherwise.</returns>
		public static bool AreEqual(this Stream input, Stream other)
		{
			var buffer = sizeof(long);

			if (input.Length != other.Length) return false;

			var iterations = (int)Math.Ceiling((double)input.Length / buffer);

			var one = new byte[buffer];
			var two = new byte[buffer];

			input.Position = 0;
			other.Position = 0;

			for (var i = 0; i < iterations; i++)
			{
				input.Read(one, 0, buffer);
				other.Read(two, 0, buffer);

				if (BitConverter.ToInt64(one, 0) == BitConverter.ToInt64(two, 0)) continue;

				input.Position = 0;
				other.Position = 0;
				return false;
			}

			input.Position = 0;
			other.Position = 0;
			return true;
		}
	}
}