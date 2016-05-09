// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 04-21-2015
//
// Last Modified By : RFTD
// Last Modified On : 04-21-2015
// ***********************************************************************
// <copyright file="IntExtensions.cs" company="">
// Esta biblioteca � software livre; voc� pode redistribu�-la e/ou modific�-la
// sob os termos da Licen�a P�blica Geral Menor do GNU conforme publicada pela
// Free Software Foundation; tanto a vers�o 2.1 da Licen�a, ou (a seu crit�rio)
// qualquer vers�o posterior.
//
// Esta biblioteca � distribu�da na expectativa de que seja �til, por�m, SEM
// NENHUMA GARANTIA; nem mesmo a garantia impl�cita de COMERCIABILIDADE OU
// ADEQUA��O A UMA FINALIDADE ESPEC�FICA. Consulte a Licen�a P�blica Geral Menor
// do GNU para mais detalhes. (Arquivo LICEN�A.TXT ou LICENSE.TXT)
//
// Voc� deve ter recebido uma c�pia da Licen�a P�blica Geral Menor do GNU junto
// com esta biblioteca; se n�o, escreva para a Free Software Foundation, Inc.,
// no endere�o 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.
// Voc� tamb�m pode obter uma copia da licen�a em:
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