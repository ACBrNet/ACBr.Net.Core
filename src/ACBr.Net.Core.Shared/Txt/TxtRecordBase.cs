// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 04-06-2017
//
// Last Modified By : RFTD
// Last Modified On : 04-06-2017
// ***********************************************************************
// <copyright file="TxtRecordBase.cs" company="ACBr.Net">
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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ACBr.Net.Core.Extensions;
using ACBr.Net.Core.Generics;

namespace ACBr.Net.Core
{
    /// <summary>
    /// Classe base para gerador de arquivo Txt
    /// </summary>
    /// <typeparam name="TClass">Classe do arquivo</typeparam>
    /// <typeparam name="TAttribute">Atributo Txt</typeparam>
    public abstract class TxtRecordBase<TClass, TAttribute> : GenericClone<TClass> where TClass : class where TAttribute : Attribute
    {
        #region Methods

        /// <summary>
        /// Função para validação, se o valor do checkAction for false lança um exception
        /// </summary>
        /// <param name="checkAction">Função para checagem</param>
        /// <param name="mensagemErro">Mensagem de erro</param>
        /// <exception cref="ACBrException"></exception>
        protected virtual void Check(Func<bool> checkAction, string mensagemErro)
        {
            Check(checkAction(), mensagemErro);
        }

        /// <summary>
        /// Lança um exceção se o valor do check for false;
        /// </summary>
        /// <param name="check"></param>
        /// <param name="mensagemErro">Mensagem de erro</param>
        /// <exception cref="ACBrException"></exception>
        protected virtual void Check(bool check, string mensagemErro)
        {
            Guard.Against<ACBrException>(!check, mensagemErro);
        }

        /// <summary>
        /// Função para ajustar a string de acordo com os parametros informados
        /// </summary>
        /// <param name="value">String de origem</param>
        /// <param name="minimo">Tamanho minimo da string</param>
        /// <param name="maximo">Tamanho maximo da string</param>
        /// <param name="direction">Lado onde sera inserido o caracter</param>
        /// <param name="fillChar">Caractere de preenchimento</param>
        /// <returns>String ajustada</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected virtual string AdjustString(string value, int minimo, int maximo, TxtFill direction, char fillChar)
        {
            if (value.IsEmpty()) value = string.Empty;

            // limita o maximo
            value = value.Substring(0, Math.Min(maximo, value.Length));

            // checa se esta igual ou maior que o minimo
            if (value.Length >= minimo) return value;

            // ajusta o minimo
            switch (direction)
            {
                case TxtFill.Esquerda:
                    return value.FillLeft(minimo, fillChar);

                case TxtFill.Direta:
                    return value.FillRight(minimo, fillChar);

                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        /// <summary>
        /// Retorna todas as propriedades que tenha o atributo txt
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<PropertyInfo> GetProperties()
        {
            return typeof(TClass).GetProperties().Where(x => x.HasAttribute<TAttribute>());
        }

        /// <summary>
        /// Função para gerar a linha txt correspondente a esta classe
        /// </summary>
        /// <param name="writer"></param>
        /// <returns>A quantidade de linha inseridas no stream</returns>
        public abstract void GerarLinha(ACBrTxtWriter writer);

        /// <summary>
        /// Função que retorno o valor do item já ajustado de acordo com o atributo txt
        /// </summary>
        /// <param name="value">Valor do objeto</param>
        /// <param name="field">Atributo txt</param>
        /// <returns></returns>
        protected abstract string ObterValor(object value, TAttribute field);

        #endregion Methods
    }
}