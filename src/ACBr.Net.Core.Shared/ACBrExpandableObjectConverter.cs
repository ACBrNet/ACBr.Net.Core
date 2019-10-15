// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 04-19-2014
//
// Last Modified By : RFTD
// Last Modified On : 04-19-2014
// ***********************************************************************
// <copyright file="ACBrExpandableObjectConverter.cs" company="ACBr.Net">
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
using System.ComponentModel;
using System.Globalization;

namespace ACBr.Net.Core
{
	/// <summary>
	/// Classe ACBrExpandableObjectConverter.
	/// </summary>
	public class ACBrExpandableObjectConverter : ExpandableObjectConverter
	{
		/// <summary>
		/// Converts to.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="culture">The culture.</param>
		/// <param name="value">The value.</param>
		/// <param name="destType">Type of the dest.</param>
		/// <returns>System.Object.</returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
		{
			if (value != null && destType == typeof(string))
			{
				return $"({value.GetType().Name})";
			}

			return base.ConvertTo(context, culture, value, destType);
		}
	}
}