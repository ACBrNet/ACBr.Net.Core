// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 08-30-2015
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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ACBr.Net.Core.Extensions
{
	/// <summary>
	/// Class EnumerableExtensions.
	/// </summary>
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Transforma uma lista em uma BindingList.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list">A lista</param>
		/// <returns>BindingList</returns>
		public static BindingList<T> ToBindingList<T>(this IEnumerable<T> list)
		{
			var ret = new BindingList<T>();

			foreach (var item in list)
			{
				ret.Add(item);
			}

			return ret;
		}

		/// <summary>
		/// Transforma uma lista de string em uma unica string.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <returns>String com todos os dados da lista de strings</returns>
		public static string AsString(this IEnumerable<string> array)
		{
			return string.Join(Environment.NewLine, array);
		}

		/// <summary>
		/// Faz cast de um ienumerable para outro tipo
		/// </summary>
		/// <param name="lista"></param>
		/// <param name="tipo"></param>
		/// <returns></returns>
		public static IEnumerable Cast(this IEnumerable lista, Type tipo)
		{
			var method = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(tipo);
			return (IEnumerable)method.Invoke(null, new object[] { lista });
		}
	}
}