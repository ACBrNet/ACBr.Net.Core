// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 01-06-2015
//
// Last Modified By : RFTD
// Last Modified On : 24-03-2016
// ***********************************************************************
// <copyright file="GenericClone.cs" company="ACBr.Net">
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
using System.Linq;
using System.Reflection;

namespace ACBr.Net.Core.Generics
{
    /// <summary>
    /// Classe GenericClone implementação generica da interface ICloneable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericClone<T> : ICloneable where T : class
    {
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>T.</returns>
        public T Clone()
        {
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var ret = Activator.CreateInstance(typeof(T)) as T;

            foreach (var prop in props.Where(prop => null != prop.GetSetMethod()))
            {
                var value = prop.GetValue(this, null);
                prop.SetValue(ret, value is ICloneable ? ((ICloneable)value).Clone() : value, null);
            }

            return ret;
        }

        /// <inheritdoc />
        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}