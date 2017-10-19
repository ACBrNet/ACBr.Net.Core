// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="DecimalExtensions.cs" company="ACBr.Net">
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

namespace ACBr.Net.Core.Extensions
{
	/// <summary>
	/// Classe DecimalExtensions.
	/// </summary>
	public static class DecimalExtensions
	{
		#region Fields

		/// <summary>
		/// The qualificadores
		/// </summary>
		private static readonly String[,] Qualificadores = {
				{"Centavo", "Centavos"},
				{"Real", "Reais"},
				{"e", "de"},
				{"Mil", "Mil"},
				{"Milhão", "Milhões"},
				{"Bilhão", "Bilhões"},
				{"Trilhão", "Trilhões"},
				{"Quatrilhão", "Quatrilhões"},
				{"Quintilhão", "Quintilhões"},
				{"Sextilhão", "Sextilhões"},
				{"Setilhão", "Setilhões"},
				{"Octilhão","Octilhões"},
				{"Nonilhão","Nonilhões"},
				{"Decilhão","Decilhões"}
		};

		/// <summary>
		/// The numeros
		/// </summary>
		private static readonly String[,] Numeros = {
				{"Zero", "Um", "Dois", "Três", "Quatro",
				 "Cinco", "Seis", "Sete", "Oito", "Nove",
				 "Dez","Onze", "Doze", "Treze", "Quatorze",
				 "Quinze", "Dezesseis", "Dezessete", "Dezoito", "Dezenove"},
				{"Vinte", "Trinta", "Quarenta", "Cinqüenta", "Sessenta",
				 "Setenta", "Oitenta", "Noventa",null,null,null,null,null,null,null,null,null,null,null,null},
				{"Cem", "Cento", "Duzentos", "Trezentos", "Quatrocentos", "Quinhentos", "Seiscentos",
				 "Setecentos", "Oitocentos", "Novecentos",null,null,null,null,null,null,null,null,null,null}
				};

		#endregion Fields

		#region Methods

		/// <summary>
		/// Decimals the places count.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.Int32.</returns>
		public static int DecimalPlacesCount(this decimal value)
		{
			return BitConverter.GetBytes(decimal.GetBits(value)[3])[2];
		}

		/// <summary>
		/// To the currency.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <param name="digits">The decimal places.</param>
		/// <param name="prefix">The prefix.</param>
		/// <param name="decimalsSeparator">The decimals separator.</param>
		/// <param name="thousandsSeparator">The thousands separator.</param>
		/// <returns>System.String.</returns>
		public static string ToCurrency(this decimal amount, int digits = 2,
			string prefix = "R$ ", char decimalsSeparator = ',', char thousandsSeparator = '.')
		{
			return ((decimal?)amount).ToCurrency(digits, prefix, decimalsSeparator, thousandsSeparator);
		}

		/// <summary>
		/// To the currency.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <param name="digits">The decimal places.</param>
		/// <param name="prefix">The prefix.</param>
		/// <param name="decimalsSeparator">The decimals separator.</param>
		/// <param name="thousandsSeparator">The thousands separator.</param>
		/// <returns>System.String.</returns>
		public static string ToCurrency(this decimal? amount, int digits = 2,
			string prefix = "R$ ", char decimalsSeparator = ',', char thousandsSeparator = '.')
		{
			var numberFormat = new NumberFormatInfo
			{
				CurrencyDecimalDigits = digits,
				CurrencyDecimalSeparator = decimalsSeparator.ToString(),
				CurrencyGroupSeparator = thousandsSeparator.ToString(),
				CurrencySymbol = prefix
			};
			return (amount ?? 0).ToString("c", numberFormat);
		}

		/// <summary>
		/// Inverts the signal.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <returns>System.Decimal.</returns>
		public static decimal InvertSignal(this decimal amount)
		{
			// ReSharper disable once PossibleInvalidOperationException
			return (decimal)((decimal?)amount).InvertSignal();
		}

		/// <summary>
		/// Inverts the signal.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <returns>System.Nullable&lt;System.Decimal&gt;.</returns>
		public static decimal? InvertSignal(this decimal? amount)
		{
			return (amount ?? 0) * -1;
		}

		/// <summary>
		/// To the extension.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <param name="invertersinal">if set to <c>true</c> [invertersinal].</param>
		/// <returns>System.String.</returns>
		public static string ToExtension(this decimal amount, bool invertersinal = false)
		{
			return ((decimal?)amount).ToExtension(invertersinal);
		}

		/// <summary>
		/// To the extension.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <param name="invertersinal">if set to <c>true</c> [invertersinal].</param>
		/// <returns>System.String.</returns>
		public static string ToExtension(this decimal? amount, bool invertersinal = false)
		{
			var valor = amount ?? 0;

			if (valor == 0) return Numeros[0, 0];

			if (valor < 0 && invertersinal)
			{
				valor = valor.InvertSignal();
			}
			else if (valor < 0)
			{
				return "Valor Negativo";
			}
			else if (valor >= 1000000000000000)
			{
				return "Valor não suportado pelo sistema.";
			}

			#region Function EscrevaParte

			var escrevaParte = new Func<decimal, string>((valor1) =>
			{
				if (valor1 <= 0)
					return string.Empty;

				var montagem = string.Empty;
				if (valor1 > 0 & valor1 < 1)
				{
					valor1 *= 100;
				}

				var strValor1 = valor1.ToString("000");
				var a = Convert.ToInt32(strValor1.Substring(0, 1));
				var b = Convert.ToInt32(strValor1.Substring(1, 1));
				var c = Convert.ToInt32(strValor1.Substring(2, 1));

				if (a == 1) montagem += b + c == 0 ? Numeros[2, 0] : Numeros[2, 1];
				else if (a == 2) montagem += Numeros[2, 2];
				else if (a == 3) montagem += Numeros[2, 3];
				else if (a == 4) montagem += Numeros[2, 4];
				else if (a == 5) montagem += Numeros[2, 5];
				else if (a == 6) montagem += Numeros[2, 6];
				else if (a == 7) montagem += Numeros[2, 7];
				else if (a == 8) montagem += Numeros[2, 8];
				else if (a == 9) montagem += Numeros[2, 9];

				if (b == 1)
				{
					if (c == 0) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[0, 10];
					else if (c == 1) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[0, 11];
					else if (c == 2) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[0, 12];
					else if (c == 3) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[0, 13];
					else if (c == 4) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[0, 14];
					else if (c == 5) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[0, 15];
					else if (c == 6) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[0, 16];
					else if (c == 7) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[0, 17];
					else if (c == 8) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[0, 18];
					else if (c == 9) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[0, 19];
				}
				else if (b == 2) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[1, 0];
				else if (b == 3) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[1, 1];
				else if (b == 4) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[1, 2];
				else if (b == 5) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[1, 3];
				else if (b == 6) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[1, 4];
				else if (b == 7) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[1, 5];
				else if (b == 8) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[1, 6];
				else if (b == 9) montagem += (a > 0 ? $" {Qualificadores[2, 0]} " : string.Empty) + Numeros[1, 7];

				if (strValor1.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty)
					montagem += $" {Qualificadores[2, 0]} ";

				if (strValor1.Substring(1, 1) != "1")
					if (c == 1) montagem += Numeros[0, 1];
					else if (c == 2) montagem += Numeros[0, 2];
					else if (c == 3) montagem += Numeros[0, 3];
					else if (c == 4) montagem += Numeros[0, 4];
					else if (c == 5) montagem += Numeros[0, 5];
					else if (c == 6) montagem += Numeros[0, 6];
					else if (c == 7) montagem += Numeros[0, 7];
					else if (c == 8) montagem += Numeros[0, 8];
					else if (c == 9) montagem += Numeros[0, 9];

				return montagem;
			});

			#endregion Function EscrevaParte

			var strValor = valor.ToString("000000000000000.00");
			var valorPorExtenso = string.Empty;

			for (var i = 0; i <= 15; i += 3)
			{
				valorPorExtenso += escrevaParte(strValor.Substring(i, 3).ToDecimal());
				if (i == 0 & valorPorExtenso != string.Empty)
				{
					if (strValor.Substring(0, 3).ToInt32() == 1)
					{
						valorPorExtenso += $" {Qualificadores[6, 0]} {(strValor.Substring(3, 12).ToDecimal() > 0 ? $" {Qualificadores[2, 0]} " : string.Empty)}";
					}
					else if (strValor.Substring(0, 3).ToInt32() > 1)
					{
						valorPorExtenso += $" {Qualificadores[6, 1]} {(strValor.Substring(3, 12).ToDecimal() > 0 ? $" {Qualificadores[2, 0]} " : string.Empty)}";
					}
				}
				else if (i == 3 & valorPorExtenso != string.Empty)
				{
					if (strValor.Substring(3, 3).ToInt32() == 1)
					{
						valorPorExtenso += $" {Qualificadores[5, 0]} {(strValor.Substring(6, 9).ToDecimal() > 0 ? $" {Qualificadores[2, 0]} " : string.Empty)}";
					}
					else if (strValor.Substring(3, 3).ToInt32() > 1)
					{
						valorPorExtenso += $" {Qualificadores[5, 1]} {(strValor.Substring(6, 9).ToDecimal() > 0 ? $" {Qualificadores[2, 0]} " : string.Empty)}";
					}
				}
				else if (i == 6 & valorPorExtenso != string.Empty)
				{
					if (strValor.Substring(6, 3).ToInt32() == 1)
					{
						valorPorExtenso += $" {Qualificadores[4, 0]} {(strValor.Substring(9, 6).ToDecimal() > 0 ? $" {Qualificadores[2, 0]} " : string.Empty)}";
					}
					else if (strValor.Substring(6, 3).ToInt32() > 1)
					{
						valorPorExtenso += $" {Qualificadores[4, 1]} {(strValor.Substring(9, 6).ToDecimal() > 0 ? $" {Qualificadores[2, 0]} " : string.Empty)}";
					}
				}
				else if (i == 9 & valorPorExtenso != string.Empty)
				{
					if (strValor.Substring(9, 3).ToInt32() > 0)
					{
						valorPorExtenso += $" {Qualificadores[3, 0]} {(strValor.Substring(6, 9).ToDecimal() > 0 ? $" {Qualificadores[2, 0]} " : string.Empty)}";
					}
				}

				if (i == 12)
				{
					if (valorPorExtenso.Length > 8)
					{
						if (valorPorExtenso.Substring(valorPorExtenso.Length - 6, 6) == Qualificadores[4, 1] |
							valorPorExtenso.Substring(valorPorExtenso.Length - 6, 6) == Qualificadores[5, 1])
						{
							valorPorExtenso += $" {Qualificadores[2, 1]}";
						}
						else if (valorPorExtenso.Substring(valorPorExtenso.Length - 7, 7) == Qualificadores[4, 1] |
								 valorPorExtenso.Substring(valorPorExtenso.Length - 7, 7) == Qualificadores[5, 1] |
								 valorPorExtenso.Substring(valorPorExtenso.Length - 8, 7) == Qualificadores[6, 1])
						{
							valorPorExtenso += $" {Qualificadores[2, 1]}";
						}
						else if (valorPorExtenso.Substring(valorPorExtenso.Length - 8, 8) == Qualificadores[6, 1])
						{
							valorPorExtenso += $" {Qualificadores[2, 1]}";
						}
					}

					if (strValor.Substring(0, 15).ToInt64() == 1)
					{
						valorPorExtenso += $" {Qualificadores[1, 0]}";
					}
					else if (strValor.Substring(0, 15).ToInt64() > 1)
					{
						valorPorExtenso += $" {Qualificadores[1, 1]}";
					}

					if (strValor.Substring(16, 2).ToInt32() > 0 && valorPorExtenso != string.Empty)
					{
						valorPorExtenso += $" {Qualificadores[2, 0]} ";
					}
				}

				if (i == 15)
				{
					if (strValor.Substring(16, 2).ToInt32() == 1)
					{
						valorPorExtenso += $" {Qualificadores[0, 0]}";
					}
					else if (strValor.Substring(16, 2).ToInt32() > 1)
					{
						valorPorExtenso += $" {Qualificadores[0, 1]}";
					}
				}
			}

			return valorPorExtenso;
		}

		/// <summary>
		/// To the remessa string.
		/// </summary>
		/// <param name="ammount">The ammount.</param>
		/// <param name="zerofill">The zerofill.</param>
		/// <param name="decimalPlaces">The decimal places.</param>
		/// <returns>System.String.</returns>
		public static string ToDecimalString(this decimal ammount, int zerofill = 13, int decimalPlaces = 2)
		{
			var intP = 1;
			for (var i = 0; i < decimalPlaces; i++)
			{
				intP *= 10;
			}

			return Math.Round(ammount * intP).ToString(CultureInfo.InvariantCulture).ZeroFill(zerofill);
		}

		/// <summary>
		/// Gets the percent value.
		/// </summary>
		/// <param name="valor">The valor.</param>
		/// <param name="porcentagem">The porcentagem.</param>
		/// <returns>System.Decimal.</returns>
		public static decimal GetPercentValue(this decimal valor, decimal porcentagem)
		{
			if (porcentagem < 0)
				return 0;

			return porcentagem / 100 * valor;
		}

		/// <summary>
		/// Gets the percent value.
		/// </summary>
		/// <param name="valor">The valor.</param>
		/// <param name="desconto">The desconto.</param>
		/// <returns>System.Decimal.</returns>
		public static decimal GetPercentFromValue(this decimal valor, decimal desconto)
		{
			if (valor < 1)
				return 0;

			return desconto * 100 / valor;
		}

		/// <summary>
		/// Gets the value from percent.
		/// </summary>
		/// <param name="valor">The valor.</param>
		/// <param name="porcentagem">The porcentagem.</param>
		/// <returns>System.Decimal.</returns>
		public static decimal GetValueFromPercent(this decimal valor, decimal porcentagem)
		{
			if (porcentagem > 100 || porcentagem < 1)
				return 0;

			return valor * 100 / porcentagem;
		}

		/// <summary>
		/// Substracts the percent value.
		/// </summary>
		/// <param name="valor">The valor.</param>
		/// <param name="porcentagem">The porcentagem.</param>
		/// <returns>System.Decimal.</returns>
		public static decimal SubstractPercentValue(this decimal valor, decimal porcentagem)
		{
			if (porcentagem < 0)
				return -1;

			var percentual = porcentagem / 100.0M;
			return valor - percentual * valor;
		}

		/// <summary>
		/// Adds the percent value.
		/// </summary>
		/// <param name="valor">The valor.</param>
		/// <param name="porcentagem">The porcentagem.</param>
		/// <returns>System.Decimal.</returns>
		public static decimal AddPercentValue(this decimal valor, decimal porcentagem)
		{
			if (porcentagem < 0)
				return -1;

			var percentual = porcentagem / 100.0M;
			return valor + percentual * valor;
		}

		/// <summary>
		/// Calculates the lucro.
		/// </summary>
		/// <param name="valor">The valor.</param>
		/// <param name="valorvenda">The valorvenda.</param>
		/// <returns>System.Decimal.</returns>
		public static decimal CalcLucro(this decimal valor, decimal valorvenda)
		{
			if (valor > valorvenda)
				return 0;

			var lucro = 0.0M;
			while (valor + lucro / 100.0M * valor < valorvenda)
			{
				lucro += 0.1M;
			}

			return lucro;
		}

		/// <summary>
		/// Pegar o valor das parcelas
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="parcelas">The parcelas.</param>
		/// <returns>System.Decimal.</returns>
		public static decimal GetParcelValue(this decimal value, int parcelas)
		{
			var parcel = value / parcelas;
			return (int)(parcel * 100M) / 100M;
		}

		/// <summary>
		/// as the truncate.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="decimalPlaces">The decimal places.</param>
		/// <returns>System.Decimal.</returns>
		public static decimal Trunc(this decimal value, int decimalPlaces = 2)
		{
			var factor = (decimal)Math.Pow(10, decimalPlaces);
			return Math.Truncate(value * factor) / factor;
		}

		/// <summary>
		/// Fracs the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.Decimal.</returns>
		public static decimal Frac(this decimal value)
		{
			return value - Math.Truncate(value);
		}

		/// <summary>
		/// Rounds the abnt.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="digits">The decimal places.</param>
		/// <param name="delta">The delta.</param>
		/// <returns>System.Decimal.</returns>
		public static decimal RoundABNT(this decimal value, int digits = 2, decimal delta = 0.00001M)
		{
			var negativo = (value < 0);

			var pow = (decimal)Math.Pow(10, Math.Abs(digits));
			var powValue = Math.Abs(value) / 10;
			var intValue = Math.Truncate(powValue);
			var fracValue = powValue.Frac();

			powValue = Math.Round(fracValue * 10M * pow, 9);
			var intCalc = Math.Truncate(powValue);
			var fracCalc = Math.Truncate(powValue.Frac() * 100);

			if (fracCalc > 50)
			{
				intCalc++;
			}
			else if (fracCalc == 50)
			{
				var lastNumber = (long)Math.Round((intCalc / 10).Frac() * 10);

				if (lastNumber.IsOdd())
				{
					intCalc++;
				}
				else
				{
					var restPart = (powValue * 10).Frac();
					if (restPart > delta)
						intCalc++;
				}
			}

			var result = intValue * 10 + intCalc / pow;
			return negativo ? result.InvertSignal() : result;
		}

		#endregion Methods
	}
}