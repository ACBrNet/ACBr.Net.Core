// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 05-08-2017
//
// Last Modified By : RFTD
// Last Modified On : 05-08-2017
// ***********************************************************************
// <copyright file="ACBrEncoding.cs" company="ACBr.Net">
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

using System.Text;

namespace ACBr.Net.Core
{
    /// <summary>
    /// Classe com enconding para facilitar o uso.
    /// </summary>
    public static class ACBrEncoding
    {
        #region Fields

        private static Encoding iso88591;
        private static Encoding ibm850;
        private static Encoding ibm860;
        private static Encoding windows1252;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Retorna o enconding ISO-8859-1
        /// </summary>
        public static Encoding ISO88591 => iso88591 ?? (iso88591 = Encoding.GetEncoding("ISO-8859-1"));

        /// <summary>
        /// Retorna o enconding IBM850
        /// </summary>
        public static Encoding IBM850 => ibm850 ?? (ibm850 = Encoding.GetEncoding("IBM850"));

        /// <summary>
        /// Retorna o enconding IBM860
        /// </summary>
        public static Encoding IBM860 => ibm860 ?? (ibm860 = Encoding.GetEncoding("IBM860"));

        /// <summary>
        /// Retorna o enconding CP1252
        /// </summary>
        public static Encoding CP1252 => Windows1252;

        /// <summary>
        /// Retorna o enconding Windows-1252
        /// </summary>
        public static Encoding Windows1252 => windows1252 ?? (windows1252 = Encoding.GetEncoding("Windows-1252"));

        #endregion Properties
    }
}