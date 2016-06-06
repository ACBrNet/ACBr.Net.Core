// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="Log4NetLoggerFactory.cs" company="ACBr.Net">
//		        		   The MIT License (MIT)
//	     		    Copyright (c) 2016 Grupo ACBr.Net
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
        /// <returns>IACBrLogger.</returns>
		public IACBrLogger LoggerFor(string keyName)
		{
			return new Log4NetLogger(GetLoggerByNameDelegate(keyName));
		}

        /// <summary>
        /// Loggers for.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>IACBrLogger.</returns>
		public IACBrLogger LoggerFor(Type type)
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
