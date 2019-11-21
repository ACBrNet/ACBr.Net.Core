// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="DecimalExtensions.cs" company="ACBr.Net">
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

namespace ACBr.Net.Core.Extensions
{
	/// <summary>
	/// Class DoubleExtensions.
	/// </summary>
	public static class DoubleExtensions
	{
		/// <summary>
		/// Gets the numbers of page.
		/// </summary>
		/// <param name="valor">The valor.</param>
		/// <param name="pagesize">The pagesize.</param>
		/// <returns>System.Int32.</returns>
		public static int GetNumbersOfPage(this double valor, int pagesize = 100)
		{
			var value = valor / pagesize;
			if (value % 1 == 0) return (int)Math.Truncate(value);

			return ((int)Math.Truncate(value)) + 1;
		}

		/// <summary>
		/// To the extension.
		/// </summary>
		/// <param name="valor">The valor.</param>
		/// <returns>System.String.</returns>
		public static string ToExtension(this double valor)
		{
			var valorEscrever = new decimal(valor);
			return valorEscrever.ToExtension();
		}
	}
}