// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-23-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="CharExtensions.cs" company="ACBr.Net">
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
