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
	/// <summary>
	/// Atributo usado para o gerador de arquivo Txt.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class TxtFieldAttribute : Attribute
	{
		#region Constructors

		/// <summary>
		/// Inicializa uma nova instancia da classe <see cref="TxtFieldAttribute" />.
		/// </summary>
		/// <param name="tipo"></param>
		public TxtFieldAttribute(TxtInfo tipo)
		{
			Nome = "";
			Tipo = tipo;
			Minimo = 0;
			Maximo = 0;
			Ordem = 0;
			Preenchimento = TxtFill.Esquerda;
			CaracterPreenchimento = ' ';
			SeparadorDecimal = '.';
			QtdDecimais = 2;
			Obrigatorio = false;
		}

		/// <summary>
		/// Inicializa uma nova instancia da classe <see cref="TxtFieldAttribute" />.
		/// </summary>
		/// <param name="nome"></param>
		/// <param name="type"></param>
		public TxtFieldAttribute(string nome, TxtInfo type) : this(type)
		{
			Nome = nome;
		}

		/// <summary>
		/// Inicializa uma nova instancia da classe <see cref="TxtFieldAttribute" />.
		/// </summary>
		/// <param name="tipo"></param>
		/// <param name="obrigatorio"></param>
		public TxtFieldAttribute(TxtInfo tipo, bool obrigatorio) : this(tipo)
		{
			Obrigatorio = obrigatorio;
		}

		/// <summary>
		/// Inicializa uma nova instancia da classe <see cref="TxtFieldAttribute" />.
		/// </summary>
		/// <param name="nome"></param>
		/// <param name="type"></param>
		/// <param name="obrigatorio"></param>
		public TxtFieldAttribute(string nome, TxtInfo type, bool obrigatorio) : this(type)
		{
			Nome = nome;
			Obrigatorio = obrigatorio;
		}

		#endregion Constructors

		#region Properties

		/// <summary>
		/// Define/retorna o nome do campo.
		/// </summary>
		public string Nome { get; set; }

		/// <summary>
		/// Define/retorna o tipo de dado para processamento.
		/// </summary>
		public TxtInfo Tipo { get; set; }

		/// <summary>
		/// Define/retorna a ordem de inserção do dado.
		/// </summary>
		public int Ordem { get; set; }

		/// <summary>
		/// Define/retorna o tamanho mínimo do campo.
		/// </summary>
		public int Minimo { get; set; }

		/// <summary>
		/// Define/retorna o tamanho máximo do campo.
		/// </summary>
		public int Maximo { get; set; }

		/// <summary>
		/// Define/retorna se o campo é obrigatório.
		/// </summary>
		public bool Obrigatorio { get; set; }

		/// <summary>
		/// Define/retorna a partir da da onde sera feito o preenchimento em caso do valor sem menor que o mínimo.
		/// </summary>
		public TxtFill Preenchimento { get; set; }

		/// <summary>
		/// Define/retorna o caractere para ser usado no preenchimento.
		/// </summary>
		public char CaracterPreenchimento { get; set; }

		/// <summary>
		/// Define/retorna a quantidade de digitos decimais em caso de valor numérico.
		/// </summary>
		public int QtdDecimais { get; set; }

		/// <summary>
		/// Define/retorna o separador decimal utilizado em caso de valor numérico com decimais
		/// </summary>
		public char SeparadorDecimal { get; set; }

		#endregion Properties
	}
}