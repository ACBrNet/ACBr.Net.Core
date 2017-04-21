// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 04-06-2017
//
// Last Modified By : RFTD
// Last Modified On : 04-06-2017
// ***********************************************************************
// <copyright file="AttributeExtensions.cs" company="ACBr.Net">
//		        		   The MIT License (MIT)
//	     		    Copyright (c) 2014 - 2017 Grupo ACBr.Net
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

namespace ACBr.Net.Core.Extensions
{
	public static class AttributeExtensions
	{
		public static TValue GetAttributeValue<TAttribute, TValue>(
			this ICustomAttributeProvider type,
			Func<TAttribute, TValue> valueSelector)
			where TAttribute : Attribute
		{
			var att = type.GetCustomAttributes(
					typeof(TAttribute), true
				)
				.FirstOrDefault() as TAttribute;

			return att != null ? valueSelector(att) : default(TValue);
		}

		public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider provider) where TAttribute : Attribute
		{
			var att = provider.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
			return att;
		}

		public static TAttribute[] GetAttributes<TAttribute>(this ICustomAttributeProvider type)
			where TAttribute : Attribute
		{
			var att = type.GetCustomAttributes(typeof(TAttribute), true)
				.Cast<TAttribute>().ToArray();
			return att;
		}

		public static bool HasAttribute<T>(this ICustomAttributeProvider provider) where T : Attribute
		{
			var atts = provider.GetCustomAttributes(typeof(T), true);
			return atts.Length > 0;
		}
	}
}