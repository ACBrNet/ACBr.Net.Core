// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="ObjectExtension.cs" company="ACBr.Net">
// Esta biblioteca � software livre; voc� pode redistribu�-la e/ou modific�-la
// sob os termos da Licen�a P�blica Geral Menor do GNU conforme publicada pela
// Free Software Foundation; tanto a vers�o 2.1 da Licen�a, ou (a seu crit�rio)
// qualquer vers�o posterior.
//
// Esta biblioteca � distribu�da na expectativa de que seja �til, por�m, SEM
// NENHUMA GARANTIA; nem mesmo a garantia impl�cita de COMERCIABILIDADE OU
// ADEQUA��O A UMA FINALIDADE ESPEC�FICA. Consulte a Licen�a P�blica Geral Menor
// do GNU para mais detalhes. (Arquivo LICEN�A.TXT ou LICENSE.TXT)
//
// Voc� deve ter recebido uma c�pia da Licen�a P�blica Geral Menor do GNU junto
// com esta biblioteca; se n�o, escreva para a Free Software Foundation, Inc.,
// no endere�o 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.
// Voc� tamb�m pode obter uma copia da licen�a em:
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
		/// Determina se um evento esta setado ou n�o
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="classe">Classe</param>
		/// <param name="evento">Nome do evento</param>
		/// <returns><c>true</c> se o evento foi setado, <c>false</c> Sen�o.</returns>
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