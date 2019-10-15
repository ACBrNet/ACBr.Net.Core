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

            if (!(fieldInfo?.GetValue(classe) is Delegate handler)) return false;
            var subscribers = handler.GetInvocationList();
            return subscribers.Length != 0;
        }

        /// <summary>
        /// Dispara <exception cref="System.ArgumentNullException">ArgumentNullException</exception> se o objeto for nulo.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">O item para checar se é nulo.</param>
        /// <param name="name">O nome para lançar se na exception, se necessário</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static void ThrowIfNull<T>(this T data, string name) where T : IComparable<T>
        {
            Guard.Against<ArgumentNullException>(data == null, name);
        }

        /// <summary>
        /// Dispara <exception cref="System.ArgumentNullException">ArgumentNullException</exception> se o objeto for nulo.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">O item para checar se é nulo.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static void ThrowIfNull<T>(this T data) where T : IComparable<T>
        {
            Guard.Against<ArgumentNullException>(data == null, nameof(data));
        }

        /// <summary>
        /// Determina se o valor é nulo ou não.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">O valor.</param>
        /// <returns><c>true</c> se o valor especificado for nulo; caso contrario, <c>false</c>.</returns>
        public static bool IsNull<T>(this T value) where T : class
        {
            return Equals(value, null);
        }

        /// <summary>
        /// Checar se o valor esta entre os valores informados.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">O valor.</param>
        /// <param name="low">O mínimo.</param>
        /// <param name="high">O máximo.</param>
        /// <returns><c>true</c> se valor estiver entre o mínimo e o máximo, <c>false</c> se não.</returns>
        public static bool IsBetween<T>(this T obj, T low, T high) where T : IComparable<T>
        {
            return obj.CompareTo(low) >= 0 && obj.CompareTo(high) <= 0;
        }
    }
}