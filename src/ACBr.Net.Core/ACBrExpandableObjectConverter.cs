// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 04-19-2014
//
// Last Modified By : RFTD
// Last Modified On : 04-19-2014
// ***********************************************************************
// <copyright file="ACBrExpandableObjectConverter.cs" company="ACBr.Net">
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
using System.Globalization;
using System.ComponentModel;

namespace ACBr.Net.Core
{
    /// <summary>
    /// Classe ACBrExpandableObjectConverter.
    /// </summary>
    public class ACBrExpandableObjectConverter : ExpandableObjectConverter
    {
        /// <summary>
        /// Converts to.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <param name="destType">Type of the dest.</param>
        /// <returns>System.Object.</returns>
        public override object ConvertTo(ITypeDescriptorContext context,  CultureInfo culture,  object value, Type destType)
        {
            if ((value != null) && (destType == typeof(string)))
            {
                return (String.Format("({0})", value.GetType().Name));
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }
}
