// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-23-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="CharExtensions.cs" company="ACBr.Net">
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
using System;

namespace ACBr.Net.Core.Extensions
{
    /// <summary>
    /// Classe CharExtensions.
    /// </summary>
    public static class CharExtensions
    {
		/// <summary>
		/// To the int32.
		/// </summary>
		/// <param name="dados">The dados.</param>
		/// <param name="dRetorno">The d retorno.</param>
		/// <returns>System.Int32.</returns>
		/// <exception cref="Exception">Erro ao converter string</exception>
		public static int ToInt32(this char dados, int dRetorno = -1)
        {
            try
            {
                int converted;
                if (!int.TryParse(dados.ToString(), out converted))
                    converted = dRetorno;

                return converted;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao converter string", ex);
            }
        }

		/// <summary>
		/// Determines whether the specified value is empty.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.Boolean.</returns>
		public static bool IsEmpty(this char value)
		{
			return value == ' ';
		}
	}
}
