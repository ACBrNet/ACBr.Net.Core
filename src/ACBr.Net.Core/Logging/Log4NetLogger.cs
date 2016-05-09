// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="Log4NetLogger.cs" company="ACBr.Net">
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
    /// Classe Log4NetLogger.
    /// </summary>
	public class Log4NetLogger : IInternalLogger
	{
        /// <summary>
        /// The i log type
        /// </summary>
		private static readonly Type LogType = Type.GetType("log4net.ILog, log4net");
        /// <summary>
        /// The is error enabled delegate
        /// </summary>
		private static readonly Func<object, bool> IsErrorEnabledDelegate;
        /// <summary>
        /// The is fatal enabled delegate
        /// </summary>
		private static readonly Func<object, bool> IsFatalEnabledDelegate;
        /// <summary>
        /// The is debug enabled delegate
        /// </summary>
		private static readonly Func<object, bool> IsDebugEnabledDelegate;
        /// <summary>
        /// The is information enabled delegate
        /// </summary>
		private static readonly Func<object, bool> IsInfoEnabledDelegate;
        /// <summary>
        /// The is warn enabled delegate
        /// </summary>
		private static readonly Func<object, bool> IsWarnEnabledDelegate;

        /// <summary>
        /// The error delegate
        /// </summary>
		private static readonly Action<object, object> ErrorDelegate;
        /// <summary>
        /// The error exception delegate
        /// </summary>
		private static readonly Action<object, object, Exception> ErrorExceptionDelegate;
        /// <summary>
        /// The error format delegate
        /// </summary>
		private static readonly Action<object, string, object[]> ErrorFormatDelegate;

        /// <summary>
        /// The fatal delegate
        /// </summary>
		private static readonly Action<object, object> FatalDelegate;
        /// <summary>
        /// The fatal exception delegate
        /// </summary>
		private static readonly Action<object, object, Exception> FatalExceptionDelegate;

        /// <summary>
        /// The debug delegate
        /// </summary>
		private static readonly Action<object, object> DebugDelegate;
        /// <summary>
        /// The debug exception delegate
        /// </summary>
		private static readonly Action<object, object, Exception> DebugExceptionDelegate;
        /// <summary>
        /// The debug format delegate
        /// </summary>
		private static readonly Action<object, string, object[]> DebugFormatDelegate;

        /// <summary>
        /// The information delegate
        /// </summary>
		private static readonly Action<object, object> InfoDelegate;
        /// <summary>
        /// The information exception delegate
        /// </summary>
		private static readonly Action<object, object, Exception> InfoExceptionDelegate;
        /// <summary>
        /// The information format delegate
        /// </summary>
		private static readonly Action<object, string, object[]> InfoFormatDelegate;

        /// <summary>
        /// The warn delegate
        /// </summary>
		private static readonly Action<object, object> WarnDelegate;
        /// <summary>
        /// The warn exception delegate
        /// </summary>
		private static readonly Action<object, object, Exception> WarnExceptionDelegate;
        /// <summary>
        /// The warn format delegate
        /// </summary>
		private static readonly Action<object, string, object[]> WarnFormatDelegate;

        /// <summary>
        /// The logger
        /// </summary>
		private readonly object Logger;

        /// <summary>
        /// Initializes static members of the <see cref="Log4NetLogger"/> class.
        /// </summary>
		static Log4NetLogger()
		{
			IsErrorEnabledDelegate = GetPropertyGetter("IsErrorEnabled");
			IsFatalEnabledDelegate = GetPropertyGetter("IsFatalEnabled");
			IsDebugEnabledDelegate = GetPropertyGetter("IsDebugEnabled");
			IsInfoEnabledDelegate = GetPropertyGetter("IsInfoEnabled");
			IsWarnEnabledDelegate = GetPropertyGetter("IsWarnEnabled");
			ErrorDelegate = GetMethodCallForMessage("Error");
			ErrorExceptionDelegate = GetMethodCallForMessageException("Error");
			ErrorFormatDelegate = GetMethodCallForMessageFormat("ErrorFormat");

			FatalDelegate = GetMethodCallForMessage("Fatal");
			FatalExceptionDelegate = GetMethodCallForMessageException("Fatal");

			DebugDelegate = GetMethodCallForMessage("Debug");
			DebugExceptionDelegate = GetMethodCallForMessageException("Debug");
			DebugFormatDelegate = GetMethodCallForMessageFormat("DebugFormat");

			InfoDelegate = GetMethodCallForMessage("Info");
			InfoExceptionDelegate = GetMethodCallForMessageException("Info");
			InfoFormatDelegate = GetMethodCallForMessageFormat("InfoFormat");

			WarnDelegate = GetMethodCallForMessage("Warn");
			WarnExceptionDelegate = GetMethodCallForMessageException("Warn");
			WarnFormatDelegate = GetMethodCallForMessageFormat("WarnFormat");
		}

        /// <summary>
        /// Gets the property getter.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Func&lt;System.Object, System.Boolean&gt;.</returns>
		private static Func<object, bool> GetPropertyGetter(string propertyName)
		{
			var funcParam = Expression.Parameter(typeof(object), "l");
			Expression convertedParam = Expression.Convert(funcParam, LogType);
			Expression property = Expression.Property(convertedParam, propertyName);
			return (Func<object, bool>)Expression.Lambda(property, funcParam).Compile();
		}

        /// <summary>
        /// Gets the method call for message.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>Action&lt;System.Object, System.Object&gt;.</returns>
		private static Action<object, object> GetMethodCallForMessage(string methodName)
		{
			var loggerParam = Expression.Parameter(typeof(object), "l");
			var messageParam = Expression.Parameter(typeof(object), "o");
			Expression convertedParam = Expression.Convert(loggerParam, LogType);
			var methodCall = Expression.Call(convertedParam, LogType.GetMethod(methodName, new[] { typeof(object) }), messageParam);
			return (Action<object, object>)Expression.Lambda(methodCall, loggerParam, messageParam).Compile();
		}

        /// <summary>
        /// Gets the method call for message exception.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>Action&lt;System.Object, System.Object, Exception&gt;.</returns>
		private static Action<object, object, Exception> GetMethodCallForMessageException(string methodName)
		{
			var loggerParam = Expression.Parameter(typeof(object), "l");
			var messageParam = Expression.Parameter(typeof(object), "o");
			var exceptionParam = Expression.Parameter(typeof(Exception), "e");
			Expression convertedParam = Expression.Convert(loggerParam, LogType);
			var methodCall = Expression.Call(convertedParam, LogType.GetMethod(methodName, new[] { typeof(object), typeof(Exception) }), messageParam, exceptionParam);
			return (Action<object, object, Exception>)Expression.Lambda(methodCall, loggerParam, messageParam, exceptionParam).Compile();
		}

        /// <summary>
        /// Gets the method call for message format.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>Action&lt;System.Object, System.String, System.Object[]&gt;.</returns>
		private static Action<object, string, object[]> GetMethodCallForMessageFormat(string methodName)
		{
			var loggerParam = Expression.Parameter(typeof(object), "l");
			var formatParam = Expression.Parameter(typeof(string), "f");
			var parametersParam = Expression.Parameter(typeof(object[]), "p");
			Expression convertedParam = Expression.Convert(loggerParam, LogType);
			var methodCall = Expression.Call(convertedParam, LogType.GetMethod(methodName, new[] { typeof(string), typeof(object[]) }), formatParam, parametersParam);
			return (Action<object, string, object[]>)Expression.Lambda(methodCall, loggerParam, formatParam, parametersParam).Compile();
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetLogger"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
		public Log4NetLogger(object logger)
		{
			Logger = logger;
		}

        /// <summary>
        /// Gets a value indicating whether this instance is error enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is error enabled; otherwise, <c>false</c>.</value>
		public bool IsErrorEnabled
		{
			get { return IsErrorEnabledDelegate(Logger); }
		}

        /// <summary>
        /// Gets a value indicating whether this instance is fatal enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is fatal enabled; otherwise, <c>false</c>.</value>
		public bool IsFatalEnabled
		{
			get { return IsFatalEnabledDelegate(Logger); }
		}

        /// <summary>
        /// Gets a value indicating whether this instance is debug enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is debug enabled; otherwise, <c>false</c>.</value>
		public bool IsDebugEnabled
		{
			get { return IsDebugEnabledDelegate(Logger); }
		}

        /// <summary>
        /// Gets a value indicating whether this instance is information enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is information enabled; otherwise, <c>false</c>.</value>
		public bool IsInfoEnabled
		{
			get { return IsInfoEnabledDelegate(Logger); }
		}

        /// <summary>
        /// Gets a value indicating whether this instance is warn enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is warn enabled; otherwise, <c>false</c>.</value>
		public bool IsWarnEnabled
		{
			get { return IsWarnEnabledDelegate(Logger); }
		}

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
		public void Error(object message)
		{
			if (IsErrorEnabled)
				ErrorDelegate(Logger, message);
		}

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
		public void Error(object message, Exception exception)
		{
			if (IsErrorEnabled)
				ErrorExceptionDelegate(Logger, message, exception);
		}

        /// <summary>
        /// Errors the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
		public void ErrorFormat(string format, params object[] args)
		{
			if (IsErrorEnabled)
				ErrorFormatDelegate(Logger, format, args);
		}

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
		public void Fatal(object message)
		{
			if (IsFatalEnabled)
				FatalDelegate(Logger, message);
		}

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
		public void Fatal(object message, Exception exception)
		{
			if (IsFatalEnabled)
				FatalExceptionDelegate(Logger, message, exception);
		}

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
		public void Debug(object message)
		{
			if (IsDebugEnabled)
				DebugDelegate(Logger, message);
		}

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
		public void Debug(object message, Exception exception)
		{
			if (IsDebugEnabled)
				DebugExceptionDelegate(Logger, message, exception);
		}

        /// <summary>
        /// Debugs the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
		public void DebugFormat(string format, params object[] args)
		{
			if (IsDebugEnabled)
				DebugFormatDelegate(Logger, format, args);
		}

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
		public void Info(object message)
		{
			if (IsInfoEnabled)
				InfoDelegate(Logger, message);
		}

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
		public void Info(object message, Exception exception)
		{
			if (IsInfoEnabled)
				InfoExceptionDelegate(Logger, message, exception);
		}

        /// <summary>
        /// Informations the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
		public void InfoFormat(string format, params object[] args)
		{
			if (IsInfoEnabled)
				InfoFormatDelegate(Logger, format, args);
		}

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
		public void Warn(object message)
		{
			if (IsWarnEnabled)
				WarnDelegate(Logger, message);
		}

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
		public void Warn(object message, Exception exception)
		{
			if (IsWarnEnabled)
				WarnExceptionDelegate(Logger, message, exception);
		}

        /// <summary>
        /// Warns the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
		public void WarnFormat(string format, params object[] args)
		{
			if (IsWarnEnabled)
				WarnFormatDelegate(Logger, format, args);
		}
	}
}
