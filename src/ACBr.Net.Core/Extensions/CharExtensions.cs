// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-23-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="CharExtensions.cs" company="ACBr.Net">
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

namespace ACBr.Net.Core.Extensions
{
    /// <summary>
    /// Classe CharExtensions.
    /// </summary>
    public static class CharExtensions
    {
		/// <summary>
		/// To the int32.
		/// </summary>
		/// <param name="dados">The dados.</param>
		/// <param name="dRetorno">The d retorno.</param>
		/// <returns>System.Int32.</returns>
		/// <exception cref="Exception">Erro ao converter string</exception>
		public static int ToInt32(this char dados, int dRetorno = -1)
        {
            try
            {
                int converted;
                if (!int.TryParse(dados.ToString(), out converted))
                    converted = dRetorno;

                return converted;
            }
            catch (Exception ex)
            {
                throw new ACBrException("Erro ao converter string", ex);
            }
        }

		/// <summary>
		/// Determines whether the specified value is empty.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.Boolean.</returns>
		public static bool IsEmpty(this char value)
		{
			return value == ' ';
		}
	}
}
