// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="Log4NetLoggerFactory.cs" company="ACBr.Net">
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
