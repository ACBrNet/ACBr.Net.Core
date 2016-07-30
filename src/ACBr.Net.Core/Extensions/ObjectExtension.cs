// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="ObjectExtension.cs" company="ACBr.Net">
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
using ACBr.Net.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ACBr.Net.Core.Extensions
{
	/// <summary>
	/// Classe ObjectExtension.
	/// </summary>
	public static class ObjectExtension
	{
		/// <summary>
		/// Determines whether the specified t is in.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="t">The t.</param>
		/// <param name="values">The values.</param>
		/// <returns><c>true</c> if the specified t is in; otherwise, <c>false</c>.</returns>
		public static bool IsIn<T>(this T t, params T[] values)
		{
			return values.Contains(t);
		}

		/// <summary>
		/// Determines whether the specified object is in.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <param name="query">The query.</param>
		public static bool IsIn<T>(this T obj, IQueryable<T> query)
		{
			return query.Contains(obj);
		}

		/// <summary>
		/// Determines whether the specified object is in.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <param name="query">The query.</param>
		public static bool IsIn<T>(this T obj, IEnumerable<T> query)
		{
			return query.Contains(obj);
		}

		/// <summary>
		/// Determina se um evento esta setado ou não
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="classe">Classe</param>
		/// <param name="evento">Nome do evento</param>
		/// <returns><c>true</c> se o evento foi setado, <c>false</c> Senão.</returns>
		public static bool EventAssigned<T>(this T classe, string evento) where T : class
		{
			var fieldInfo = typeof(T).GetField(evento, BindingFlags.NonPublic | BindingFlags.Instance);

			var handler = fieldInfo?.GetValue(classe) as Delegate;
			if (handler == null) return false;

			var subscribers = handler.GetInvocationList();
			return subscribers.Length != 0;
		}

		/// <summary>
		/// Throws an ArgumentNullException if the given data item is null.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data">The item to check for nullity.</param>
		/// <param name="name">The name to use when throwing an exception, if necessary</param>
		/// <exception cref="System.ArgumentNullException"></exception>
		public static void ThrowIfNull<T>(this T data, string name) where T : IComparable<T>
		{
			Guard.Against<ArgumentNullException>(data == null, name);
		}

		/// <summary>
		/// Throws an ArgumentNullException if the given data item is null.
		/// No parameter name is specified.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data">The item to check for nullity.</param>
		/// <exception cref="System.ArgumentNullException"></exception>
		public static void ThrowIfNull<T>(this T data) where T : IComparable<T>
		{
			Guard.Against<ArgumentNullException>(data == null);
		}

		/// <summary>
		/// Determines whether the specified value is null.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the specified value is null; otherwise, <c>false</c>.</returns>
		public static bool IsNull<T>(this T value) where T : class
		{
			return Equals(value, null);
		}

		/// <summary>
		/// Betweens the specified low.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">The object.</param>
		/// <param name="low">The low.</param>
		/// <param name="high">The high.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public static bool Between<T>(this T obj, T low, T high) where T : IComparable<T>
		{
			return obj.CompareTo(low) >= 0 && obj.CompareTo(high) <= 0;
		}
	}
}