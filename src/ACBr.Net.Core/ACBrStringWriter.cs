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

using System.IO;
using System.Text;

namespace ACBr.Net.Core
{
	/// <summary>
	/// Classe derivada da StringWriter que aceita a mudan�a de encond.
	/// </summary>
	public sealed class ACBrStringWriter : StringWriter
	{
		#region Constructors

		/// <summary>
		/// Inicializar uma nova instancida da classe <see cref="ACBrStringWriter" />.
		/// </summary>
		public ACBrStringWriter()
		{
			Encoding = Encoding.UTF8;
		}

		/// <summary>
		/// Inicializar uma nova instancida da classe <see cref="ACBrStringWriter" />.
		/// </summary>
		/// <param name="encoding"></param>
		public ACBrStringWriter(Encoding encoding)
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