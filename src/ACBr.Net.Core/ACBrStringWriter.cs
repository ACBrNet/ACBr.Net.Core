// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 07-30-2016
//
// Last Modified By : RFTD
// Last Modified On : 07-30-2016
// ***********************************************************************
// <copyright file="ACBrStringWriter.cs" company="ACBr.Net">
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
using System.Globalization;
using System.IO;
using System.Text;

namespace ACBr.Net.Core
{
    /// <summary>
    /// Classe derivada da StringWriter que aceita a mudança de enconding e usa UTF8 como padrão.
    /// </summary>
    public sealed class ACBrStringWriter : StringWriter
    {
        #region Constructors

        /// <summary>
        /// Inicializar uma nova instancida da classe <see cref="ACBrStringWriter" />.
        /// </summary>
        public ACBrStringWriter() : this(Encoding.UTF8, new StringBuilder(), CultureInfo.CurrentCulture)
        {
        }

        /// <summary>
        /// Inicializar uma nova instancida da classe <see cref="ACBrStringWriter" />.
        /// </summary>
        /// <param name="encoding"></param>
        public ACBrStringWriter(Encoding encoding) : this(encoding, new StringBuilder(), CultureInfo.CurrentCulture)
        {
        }

        /// <summary>
        /// Inicializar uma nova instancida da classe <see cref="ACBrStringWriter" />.
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="formatProvider"></param>
        public ACBrStringWriter(Encoding encoding, IFormatProvider formatProvider)
            : this(encoding, new StringBuilder(), formatProvider)
        {
        }

        /// <summary>
        /// Inicializar uma nova instancida da classe <see cref="ACBrStringWriter" />.
        /// </summary>
        /// <param name="formatProvider"></param>
        public ACBrStringWriter(IFormatProvider formatProvider)
            : this(Encoding.UTF8, new StringBuilder(), formatProvider)
        {
        }

        /// <summary>
        /// Inicializar uma nova instancida da classe <see cref="ACBrStringWriter" />.
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="sb"></param>
        public ACBrStringWriter(Encoding encoding, StringBuilder sb) : this(encoding, sb, CultureInfo.CurrentCulture)
        {
        }

        /// <summary>
        /// Inicializar uma nova instancida da classe <see cref="ACBrStringWriter" />.
        /// </summary>
        /// <param name="sb"></param>
        public ACBrStringWriter(StringBuilder sb) : this(Encoding.UTF8, sb, CultureInfo.CurrentCulture)
        {
        }

        /// <summary>
        /// Inicializar uma nova instancida da classe <see cref="ACBrStringWriter" />.
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="sb"></param>
        /// <param name="formatProvider"></param>
        public ACBrStringWriter(Encoding encoding, StringBuilder sb, IFormatProvider formatProvider) : base(sb, formatProvider)
        {
            Encoding = encoding;
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.</summary>
        /// <returns>The Encoding in which the output is written.</returns>
        /// <filterpriority>1</filterpriority>
        public override Encoding Encoding { get; }

        #endregion Propriedades
    }
}