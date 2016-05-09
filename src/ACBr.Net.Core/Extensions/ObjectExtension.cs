// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="ObjectExtension.cs" company="ACBr.Net">
// Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la
// sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela
// Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério)
// qualquer versão posterior.
//
// Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM
// NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU
// ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor
// do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)
//
// Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto
// com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,
// no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.
// Você também pode obter uma copia da licença em:
// http://www.opensource.org/licenses/lgpl-license.php
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ACBr.Net.Core.Exceptions;

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
			if (handler == null)
				return false;

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