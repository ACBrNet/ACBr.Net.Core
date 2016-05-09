// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="NLogLoggerFactory.cs" company="ACBr.Net">
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
using System.Linq.Expressions;

namespace ACBr.Net.Core.Logging
{
    /// <summary>
    /// Classe NLogLoggerFactory.
    /// </summary>
    public class NLogLoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// The log manager type
        /// </summary>
        private static readonly Type LogManagerType = Type.GetType("NLog.LogManager, NLog");

        /// <summary>
        /// The create logger instance function
        /// </summary>
		private readonly static Func<string, object> CreateLoggerInstanceFunc;

        /// <summary>
        /// Initializes static members of the <see cref="NLogLoggerFactory"/> class.
        /// </summary>
        static NLogLoggerFactory()
        {
            CreateLoggerInstanceFunc = CreateLoggerInstance();
        }

        #region ILoggerFactory Members

        /// <summary>
        /// Loggers for.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>IInternalLogger.</returns>
        public IInternalLogger LoggerFor(Type type)
        {
            return new NLogLogger(CreateLoggerInstanceFunc(type.Name));
        }

        /// <summary>
        /// Loggers for.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <returns>IInternalLogger.</returns>
        public IInternalLogger LoggerFor(string keyName)
        {
            return new NLogLogger(CreateLoggerInstanceFunc(keyName));
        }

        #endregion ILoggerFactory Members

        /// <summary>
        /// Creates the logger instance.
        /// </summary>
        /// <returns>Func&lt;System.String, System.Object&gt;.</returns>
        private static Func<string, object> CreateLoggerInstance()
        {
            var method = LogManagerType.GetMethod("GetLogger", new[] { typeof(string) });
            var nameParam = Expression.Parameter(typeof(string));
            var methodCall = Expression.Call(null, method, nameParam);

            return Expression.Lambda<Func<string, object>>(methodCall, nameParam).Compile();
        }
    }
}