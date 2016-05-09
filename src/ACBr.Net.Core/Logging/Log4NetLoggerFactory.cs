// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="Log4NetLoggerFactory.cs" company="ACBr.Net">
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
    /// Classe Log4NetLoggerFactory.
    /// </summary>
	public class Log4NetLoggerFactory : ILoggerFactory
	{
        /// <summary>
        /// The log manager type
        /// </summary>
		private static readonly Type LogManagerType = Type.GetType("log4net.LogManager, log4net");
        /// <summary>
        /// The get logger by name delegate
        /// </summary>
		private static readonly Func<string, object> GetLoggerByNameDelegate;
        /// <summary>
        /// The get logger by type delegate
        /// </summary>
		private static readonly Func<Type, object> GetLoggerByTypeDelegate;
        /// <summary>
        /// Initializes static members of the <see cref="Log4NetLoggerFactory"/> class.
        /// </summary>
		static Log4NetLoggerFactory()
		{
			GetLoggerByNameDelegate = GetGetLoggerMethodCall<string>();
			GetLoggerByTypeDelegate = GetGetLoggerMethodCall<Type>();
		}
        /// <summary>
        /// Loggers for.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <returns>IInternalLogger.</returns>
		public IInternalLogger LoggerFor(string keyName)
		{
			return new Log4NetLogger(GetLoggerByNameDelegate(keyName));
		}

        /// <summary>
        /// Loggers for.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>IInternalLogger.</returns>
		public IInternalLogger LoggerFor(Type type)
		{
			return new Log4NetLogger(GetLoggerByTypeDelegate(type));
		}

        /// <summary>
        /// Gets the get logger method call.
        /// </summary>
        /// <typeparam name="TParameter">The type of the t parameter.</typeparam>
        /// <returns>Func&lt;TParameter, System.Object&gt;.</returns>
		private static Func<TParameter, object> GetGetLoggerMethodCall<TParameter>()
		{
			var method = LogManagerType.GetMethod("GetLogger", new[] { typeof(TParameter) });
			ParameterExpression resultValue;
			var keyParam = Expression.Parameter(typeof(TParameter), "key");
			var methodCall = Expression.Call(null, method, resultValue = keyParam);
			return Expression.Lambda<Func<TParameter, object>>(methodCall, resultValue).Compile();
		}
	}
}
