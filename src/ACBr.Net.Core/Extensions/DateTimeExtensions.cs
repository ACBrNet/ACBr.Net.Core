// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="DateTimeExtensions.cs" company="ACBr.Net">
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
	/// Classe DateTimeExtensions.
	/// </summary>
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Gets the idade.
		/// </summary>
		/// <param name="dtNascimento">The dt nascimento.</param>
		/// <returns>System.Int32.</returns>
		public static int GetIdade(this DateTime dtNascimento)
		{
			return ((DateTime?)dtNascimento).GetIdade();
		}

		/// <summary>
		/// Gets the idade.
		/// </summary>
		/// <param name="dtNascimento">The dt nascimento.</param>
		/// <returns>System.Int32.</returns>
		public static int GetIdade(this DateTime? dtNascimento)
		{
			if (!dtNascimento.HasValue) return 0;

			var yearsAge = DateTime.Now.Year - dtNascimento.Value.Year;

			if (DateTime.Now.Month < dtNascimento.Value.Month ||
				(DateTime.Now.Month == dtNascimento.Value.Month &&
				 DateTime.Now.Day < dtNascimento.Value.Day))
			{
				yearsAge--;
			}

			return yearsAge;
		}

		/// <summary>
		/// Gets the idade full.
		/// </summary>
		/// <param name="dtNascimento">The dt nascimento.</param>
		/// <returns>System.String.</returns>
		public static string GetIdadeFull(this DateTime dtNascimento)
		{
			return ((DateTime?)dtNascimento).GetIdadeFull();
		}

		/// <summary>
		/// Gets the idade full.
		/// </summary>
		/// <param name="dtNascimento">The dt nascimento.</param>
		/// <returns>System.String.</returns>
		public static string GetIdadeFull(this DateTime? dtNascimento)
		{
			if (!(dtNascimento.HasValue) || DateTime.Now < dtNascimento) return string.Empty;

			int idDias = 0, idMeses = 0, idAnos = 0;
			var dAtual = DateTime.Now;
			string ta = "", tm = "", td = "";

			if (dAtual.Day < dtNascimento.Value.Day)
			{
				idDias = (DateTime.DaysInMonth(dAtual.Year, dAtual.Month - 1));

				idMeses = -1;
				if (idDias == 28 && dtNascimento.Value.Day == 29)
					idDias = 29;
			}

			if (dAtual.Month < dtNascimento.Value.Month)
			{
				idMeses = idMeses + 12;
				idAnos = -1;
			}

			idDias = dAtual.Day - dtNascimento.Value.Day + idDias;
			idMeses = dAtual.Month - dtNascimento.Value.Month + idMeses;
			idAnos = dAtual.Year - dtNascimento.Value.Year + idAnos;

			if (idAnos > 1)
				ta = idAnos + " anos ";
			else if (idAnos == 1)
				ta = idAnos + "ano";

			if (idMeses > 1)
				tm = idMeses + " meses ";
			else if (idMeses == 1)
				tm = idMeses + " mês ";

			if (idDias > 1)
				td = idDias + " dias ";
			else if (idDias == 1)
				td = idDias + " dia ";

			return ta + tm + td;
		}

		/// <summary>
		/// Calculars the fator vencimento.
		/// </summary>
		/// <param name="dataVencimento">The data vencimento.</param>
		/// <returns>System.String.</returns>
		public static string CalcularFatorVencimento(this DateTime dataVencimento)
		{
			var dt = new DateTime(1997, 10, 07);
			return $"{(dataVencimento - dt).TotalDays % 9000 + 1000:0000}";
		}

		/// <summary>
		/// To the julian date.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns>System.String.</returns>
		public static string ToJulianDate(this DateTime data)
		{
			return $"{data:yy}{data.DayOfYear:D3}";
		}
	}
}