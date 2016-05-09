// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 04-21-2015
//
// Last Modified By : RFTD
// Last Modified On : 04-21-2015
// ***********************************************************************
// <copyright file="IntExtensions.cs" company="">
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

namespace ACBr.Net.Core.Extensions
{
    /// <summary>
    /// Classe IntExtensions.
    /// </summary>
    public static class IntExtensions
    {
		/// <summary>
		/// Determines whether the specified n is divisble for x.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="n">The n.</param>
		/// <returns><c>true</c> if the specified x is divisble; otherwise, <c>false</c>.</returns>
		public static bool IsDivisible(this int x, int n)
		{
			return (x % n) == 0;
		}

		/// <summary>
		/// Determines whether the specified value is odd.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the specified value is odd; otherwise, <c>false</c>.</returns>
		public static bool IsOdd(this int value)
		{
			return value % 2 != 0;
		}

		/// <summary>
		/// Zeroes the fill.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="length">The length.</param>
		/// <returns>System.String.</returns>
		public static string ZeroFill(this int? value, int length)
        {
            return value.HasValue ? value.Value.ToString().ZeroFill(length) : "".ZeroFill(length);
        }

        /// <summary>
        /// Zeroes the fill.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <returns>System.String.</returns>
        public static string ZeroFill(this int value, int length)
        {
            return ((int?)value).ZeroFill(length);
        }
    }
}