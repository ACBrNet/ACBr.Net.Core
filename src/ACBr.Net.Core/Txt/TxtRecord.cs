// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 04-06-2017
//
// Last Modified By : RFTD
// Last Modified On : 04-06-2017
// ***********************************************************************
// <copyright file="TxtRecord.cs" company="ACBr.Net">
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
using System.Globalization;
using System.Linq;
using ACBr.Net.Core.Extensions;

namespace ACBr.Net.Core
{
    /// <inheritdoc />
    public abstract class TxtRecord<TClass> : TxtRecordBase<TClass, TxtFieldAttribute> where TClass : class
    {
        #region Methods

        /// <inheritdoc />
        protected override string ObterValor(object value, TxtFieldAttribute field)
        {
            var linhaRegistro = string.Empty;

            switch (field.Tipo)
            {
                case TxtInfo.Str:
                    linhaRegistro = $"{AdjustString(value?.ToString(), field.Minimo, field.Maximo, field.Preenchimento, field.CaracterPreenchimento)}";
                    break;

                case TxtInfo.StrNumber:
                    linhaRegistro = $"{AdjustString(value?.ToString().OnlyNumbers(), field.Minimo, field.Maximo, field.Preenchimento, field.CaracterPreenchimento)}";
                    break;

                case TxtInfo.Int:
                    linhaRegistro = $"{AdjustString(value?.ToString(), field.Minimo, field.Maximo, field.Preenchimento, field.CaracterPreenchimento)}";
                    break;

                case TxtInfo.Enum:
                    var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
                    var enumAttribute = member?.GetCustomAttributes(false).OfType<TxtEnumAttribute>().FirstOrDefault();
                    var enumValue = enumAttribute?.Value;
                    linhaRegistro = $"{enumValue ?? value.ToString()}";
                    break;

                case TxtInfo.Date:
                    if (DateTime.TryParse(value.ToString(), CultureInfo.CurrentCulture, DateTimeStyles.None, out var date))
                    {
                        linhaRegistro = $"{date:ddMMyyyy}";
                    }
                    break;

                case TxtInfo.Time:
                    if (DateTime.TryParse(value.ToString(), CultureInfo.CurrentCulture, DateTimeStyles.None, out _))
                    {
                        linhaRegistro = $"{value:hhmmss}";
                    }
                    break;

                case TxtInfo.MothYear:
                    if (DateTime.TryParse(value.ToString(), CultureInfo.CurrentCulture, DateTimeStyles.None, out var monthYear))
                    {
                        linhaRegistro = $"{monthYear:value:MMyyyy}";
                    }
                    break;

                case TxtInfo.Number:
                    if (decimal.TryParse(value?.ToString(), NumberStyles.Any, CultureInfo.CurrentCulture, out var number))
                    {
                        var numberFormat = new NumberFormatInfo
                        {
                            NumberDecimalDigits = field.QtdDecimais,
                            NumberDecimalSeparator = field.SeparadorDecimal.ToString(),
                            NumberGroupSeparator = string.Empty
                        };
                        linhaRegistro = $"{AdjustString(number.ToString("N", numberFormat), field.Minimo, field.Maximo, field.Preenchimento, field.CaracterPreenchimento)}";
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return linhaRegistro;
        }

        #endregion Methods
    }
}