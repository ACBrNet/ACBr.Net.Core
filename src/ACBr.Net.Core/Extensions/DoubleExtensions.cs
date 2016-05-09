// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="DecimalExtensions.cs" company="ACBr.Net">
// Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la
// sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela
// Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério)
// qualquer versão posterior.
//
// Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM
// NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU
// ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor
// do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)
//
// Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto
// com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,
// no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.
// Você também pode obter uma copia da licença em:
// http://www.opensource.org/licenses/lgpl-license.php
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace ACBr.Net.Core.Extensions
{
	/// <summary>
	/// Class DoubleExtensions.
	/// </summary>
	public static class DoubleExtensions
	{
		/// <summary>
		/// Gets the numbers of page.
		/// </summary>
		/// <param name="valor">The valor.</param>
		/// <param name="pagesize">The pagesize.</param>
		/// <returns>System.Int32.</returns>
		public static int GetNumbersOfPage(this double valor, int pagesize = 100)
		{
			var value = valor / pagesize;
			if (value % 1 == 0)
				return (int)Math.Truncate(value);

			return ((int)Math.Truncate(value)) + 1;
		}

		/// <summary>
		/// To the extension.
		/// </summary>
		/// <param name="valor">The valor.</param>
		/// <returns>System.String.</returns>
		public static string ToExtension(this double valor)
		{
			var valorEscrever = new decimal(valor);
			return valorEscrever.ToExtension();
		}
	}
}