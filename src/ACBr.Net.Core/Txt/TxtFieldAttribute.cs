// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 04-06-2017
//
// Last Modified By : RFTD
// Last Modified On : 04-06-2017
// ***********************************************************************
// <copyright file="TxtFieldAttribute.cs" company="ACBr.Net">
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

namespace ACBr.Net.Core
{
	[AttributeUsage(AttributeTargets.Property)]
	public class TxtFieldAttribute : Attribute
	{
		#region Constructors

		public TxtFieldAttribute(TxtInfo type)
		{
			Name = "";
			Type = type;
			Minimo = 0;
			Maximo = 0;
			Ordem = 0;
			Preenchimento = TxtFill.Esquerda;
			Caracter = ' ';
			Obrigatorio = false;
		}

		public TxtFieldAttribute(string name, TxtInfo type) : this(type)
		{
			Name = name;
		}

		public TxtFieldAttribute(TxtInfo type, bool obrigatorio) : this(type)
		{
			Obrigatorio = obrigatorio;
		}

		public TxtFieldAttribute(string name, TxtInfo type, bool obrigatorio) : this(type)
		{
			Name = name;
			Obrigatorio = obrigatorio;
		}

		#endregion Constructors

		#region Properties

		public string Name { get; set; }

		public TxtInfo Type { get; set; }

		public int Ordem { get; set; }

		public int Minimo { get; set; }

		public int Maximo { get; set; }

		public bool Obrigatorio { get; set; }

		public TxtFill Preenchimento { get; set; }

		public char Caracter { get; set; }

		#endregion Properties
	}
}