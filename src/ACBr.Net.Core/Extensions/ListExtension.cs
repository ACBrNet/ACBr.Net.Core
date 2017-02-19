// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 06-04-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="IListExtension.cs" company="ACBr.Net">
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
using System.Collections.Generic;

namespace ACBr.Net.Core.Extensions
{
	/// <summary>
	/// Class IListExtension.
	/// </summary>
	public static class ListExtension
	{
		/// <summary>
		/// Adiciona uma string com quebra de linha na lista como se fosse uma ou mais linhas
		/// </summary>
		/// <param name="list">A lista.</param>
		/// <param name="texto">O texto.</param>
		public static void AddText(this IList<string> list, string texto)
		{
			var textos = texto.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
			list.AddRange(textos);
		}

		/// <summary>
		/// Remove as linhas vazias da lista.
		/// </summary>
		/// <param name="retorno">The retorno.</param>
		public static void RemoveEmptyLines(this IList<string> retorno)
		{
			var i = 0;
			while (i < retorno.Count)
			{
				if (retorno[i].IsEmpty())
				{
					retorno.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
		}

		/// <summary>
		/// Adds the range.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list">The list.</param>
		/// <param name="itens">The itens.</param>
		public static void AddRange<T>(this IList<T> list, IEnumerable<T> itens)
		{
			foreach (var item in itens)
			{
				list.Add(item);
			}
		}
	}
}