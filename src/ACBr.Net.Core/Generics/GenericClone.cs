// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 01-06-2015
//
// Last Modified By : RFTD
// Last Modified On : 24-03-2016
// ***********************************************************************
// <copyright file="GenericClone.cs" company="ACBr.Net">
//     Copyright (c) ACBr.Net. All rights reserved.
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
        /// Cria um novo objeto que é uma copia da instancia atual.
        /// </summary>
        /// <returns>T.</returns>
        public T Clone()
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            var clone = Activator.CreateInstance(typeof(T)) as T;

			foreach (var prop in properties.Where(prop => null != prop.GetSetMethod()))
			{
				var value = prop.GetValue(this, null);
				prop.SetValue(clone, value.GetType().IsAssignableFrom(typeof (ICloneable)) ? ((ICloneable) value).Clone() : value, null);
            }

	        return clone;
        }

        /// <summary>
        /// Cria um novo objeto que é uma copia da instancia atual.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}