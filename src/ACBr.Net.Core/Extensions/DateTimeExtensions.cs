// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="DateTimeExtensions.cs" company="ACBr.Net">
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
            if (!dtNascimento.HasValue)
                return 0;

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
            if (!(dtNascimento.HasValue) || DateTime.Now < dtNascimento)
                return string.Empty;

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
            return string.Format("{0:0000}", (dataVencimento - dt).TotalDays % 9000 + 1000);
        }

        /// <summary>
        /// To the julian date.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.String.</returns>
		public static string ToJulianDate(this DateTime data)
		{
			return string.Format("{0:yy}{1:D3}", data, data.DayOfYear);
		}
    }
}
