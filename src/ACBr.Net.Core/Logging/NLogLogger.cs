// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="NLogLogger.cs" company="ACBr.Net">
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
using System.Reflection;

namespace ACBr.Net.Core.Logging
{
	/// <summary>
	/// Classe NLogLogger.
	/// </summary>
	public class NLogLogger : IACBrLogger
	{
		#region Fields

		/// <summary>
		/// The logger type
		/// </summary>
		private static readonly Type LoggerType = Type.GetType("NLog.Logger, NLog");

		/// <summary>
		/// The debug property getter
		/// </summary>
		private static readonly Func<object, bool> DebugPropertyGetter;

		/// <summary>
		/// The error property getter
		/// </summary>
		private static readonly Func<object, bool> ErrorPropertyGetter;

		/// <summary>
		/// The fatal property getter
		/// </summary>
		private static readonly Func<object, bool> FatalPropertyGetter;

		/// <summary>
		/// The information property getter
		/// </summary>
		private static readonly Func<object, bool> InfoPropertyGetter;

		/// <summary>
		/// The warn property getter
		/// </summary>
		private static readonly Func<object, bool> WarnPropertyGetter;

		/// <summary>
		/// The debug action
		/// </summary>
		private static readonly Action<object, string> DebugAction;

		/// <summary>
		/// The error action
		/// </summary>
		private static readonly Action<object, string> ErrorAction;

		/// <summary>
		/// The warn action
		/// </summary>
		private static readonly Action<object, string> WarnAction;

		/// <summary>
		/// The information action
		/// </summary>
		private static readonly Action<object, string> InfoAction;

		/// <summary>
		/// The fatal action
		/// </summary>
		private static readonly Action<object, string> FatalAction;

		/// <summary>
		/// The debug exception action
		/// </summary>
		private static readonly Action<object, string, Exception> DebugExceptionAction;

		/// <summary>
		/// The error exception action
		/// </summary>
		private static readonly Action<object, string, Exception> ErrorExceptionAction;

		/// <summary>
		/// The warn exception action
		/// </summary>
		private static readonly Action<object, string, Exception> WarnExceptionAction;

		/// <summary>
		/// The information exception action
		/// </summary>
		private static readonly Action<object, string, Exception> InfoExceptionAction;

		/// <summary>
		/// The fatal exception action
		/// </summary>
		private static readonly Action<object, string, Exception> FatalExceptionAction;

		/// <summary>
		/// The log
		/// </summary>
		private object Log;

		#endregion Fields

		#region Constructors

		/// <summary>
		/// Initializes static members of the <see cref="NLogLogger"/> class.
		/// </summary>
		static NLogLogger()
		{
			DebugPropertyGetter = CreatePropertyGetter("IsDebugEnabled");
			ErrorPropertyGetter = CreatePropertyGetter("IsErrorEnabled");
			FatalPropertyGetter = CreatePropertyGetter("IsFatalEnabled");
			InfoPropertyGetter = CreatePropertyGetter("IsInfoEnabled");
			WarnPropertyGetter = CreatePropertyGetter("IsWarnEnabled");

			DebugAction = CreateSimpleAction("Debug");
			ErrorAction = CreateSimpleAction("Error");
			WarnAction = CreateSimpleAction("Warn");
			InfoAction = CreateSimpleAction("Info");
			FatalAction = CreateSimpleAction("Fatal");

			DebugExceptionAction = CreateExceptionAction("Debug");
			ErrorExceptionAction = CreateExceptionAction("Error");
			WarnExceptionAction = CreateExceptionAction("Warn");
			InfoExceptionAction = CreateExceptionAction("Info");
			FatalExceptionAction = CreateExceptionAction("Fatal");
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NLogLogger"/> class.
		/// </summary>
		/// <param name="log">The log.</param>
		public NLogLogger(object log)
		{
			Log = log;
		}

		#endregion Constructors

		#region IACBrLogger Members

		#region Properties

		/// <summary>
		/// Gets a value indicating whether this instance is debug enabled.
		/// </summary>
		/// <value><c>true</c> if this instance is debug enabled; otherwise, <c>false</c>.</value>
		public bool IsDebugEnabled => DebugPropertyGetter(Log);

		/// <summary>
		/// Gets a value indicating whether this instance is error enabled.
		/// </summary>
		/// <value><c>true</c> if this instance is error enabled; otherwise, <c>false</c>.</value>
		public bool IsErrorEnabled => ErrorPropertyGetter(Log);

		/// <summary>
		/// Gets a value indicating whether this instance is fatal enabled.
		/// </summary>
		/// <value><c>true</c> if this instance is fatal enabled; otherwise, <c>false</c>.</value>
		public bool IsFatalEnabled => FatalPropertyGetter(Log);

		/// <summary>
		/// Gets a value indicating whether this instance is information enabled.
		/// </summary>
		/// <value><c>true</c> if this instance is information enabled; otherwise, <c>false</c>.</value>
		public bool IsInfoEnabled => InfoPropertyGetter(Log);

		/// <summary>
		/// Gets a value indicating whether this instance is warn enabled.
		/// </summary>
		/// <value><c>true</c> if this instance is warn enabled; otherwise, <c>false</c>.</value>
		public bool IsWarnEnabled => WarnPropertyGetter(Log);

		#endregion Properties

		#region IACBrLogger Methods

		/// <summary>
		/// Debugs the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception.</param>
		public void Debug(object message, Exception exception)
		{
			if (message == null || exception == null) return;
			DebugExceptionAction(Log, message.ToString(), exception);
		}

		/// <summary>
		/// Debugs the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Debug(object message)
		{
			if (message == null)
				return;

			DebugAction(Log, message.ToString());
		}

		/// <summary>
		/// Debugs the format.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The arguments.</param>
		public void DebugFormat(string format, params object[] args)
		{
			Debug(string.Format(format, args));
		}

		/// <summary>
		/// Errors the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception.</param>
		public void Error(object message, Exception exception)
		{
			if (message == null || exception == null) return;
			ErrorExceptionAction(Log, message.ToString(), exception);
		}

		/// <summary>
		/// Errors the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Error(object message)
		{
			if (message == null) return;

			ErrorAction(Log, message.ToString());
		}

		/// <summary>
		/// Errors the format.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The arguments.</param>
		public void ErrorFormat(string format, params object[] args)
		{
			Error(string.Format(format, args));
		}

		/// <summary>
		/// Fatals the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception.</param>
		public void Fatal(object message, Exception exception)
		{
			if (message == null || exception == null) return;
			FatalExceptionAction(Log, message.ToString(), exception);
		}

		/// <summary>
		/// Fatals the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Fatal(object message)
		{
			if (message == null) return;
			FatalAction(Log, message.ToString());
		}

		/// <summary>
		/// Informations the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception.</param>
		public void Info(object message, Exception exception)
		{
			if (message == null || exception == null) return;
			InfoExceptionAction(Log, message.ToString(), exception);
		}

		/// <summary>
		/// Informations the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Info(object message)
		{
			if (message == null) return;
			InfoAction(Log, message.ToString());
		}

		/// <summary>
		/// Informations the format.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The arguments.</param>
		public void InfoFormat(string format, params object[] args)
		{
			Info(string.Format(format, args));
		}

		/// <summary>
		/// Warns the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception.</param>
		public void Warn(object message, Exception exception)
		{
			if (message == null || exception == null) return;
			WarnExceptionAction(Log, message.ToString(), exception);
		}

		/// <summary>
		/// Warns the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Warn(object message)
		{
			if (message == null) return;
			WarnAction(Log, message.ToString());
		}

		/// <summary>
		/// Warns the format.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The arguments.</param>
		public void WarnFormat(string format, params object[] args)
		{
			Warn(string.Format(format, args));
		}

		#endregion IACBrLogger Methods

		#endregion IACBrLogger Members

		#region Private methods

		/// <summary>
		/// Creates the property getter.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns>Func&lt;System.Object, System.Boolean&gt;.</returns>
		private static Func<object, bool> CreatePropertyGetter(string propertyName)
		{
			var paramExpr = Expression.Parameter(typeof(object), "pv");
			Expression convertedExpr = Expression.Convert(paramExpr, LoggerType);
			Expression property = Expression.Property(convertedExpr, propertyName);

			return Expression.Lambda<Func<object, bool>>(property, paramExpr).Compile();
		}

		/// <summary>
		/// Creates the simple action.
		/// </summary>
		/// <param name="methodName">Name of the method.</param>
		/// <returns>Action&lt;System.Object, System.String&gt;.</returns>
		private static Action<object, string> CreateSimpleAction(string methodName)
		{
			var methodInfo = GetMethodInfo(methodName, new[] { typeof(string) });
			var instanceParam = Expression.Parameter(typeof(object), "i");
			var converterInstanceParam = Expression.Convert(instanceParam, LoggerType);
			var messageParam = Expression.Parameter(typeof(string), "m");

			var methodCall = Expression.Call(converterInstanceParam, methodInfo, messageParam);

			return (Action<object, string>)Expression.Lambda(methodCall, instanceParam, messageParam).Compile();
		}

		/// <summary>
		/// Creates the exception action.
		/// </summary>
		/// <param name="methodName">Name of the method.</param>
		/// <returns>Action&lt;System.Object, System.String, Exception&gt;.</returns>
		private static Action<object, string, Exception> CreateExceptionAction(string methodName)
		{
			var methodInfo = GetMethodInfo(methodName, new[] { typeof(string), typeof(Exception) });

			var messageParam = Expression.Parameter(typeof(string), "m");
			var instanceParam = Expression.Parameter(typeof(object), "i");
			var exceptionParam = Expression.Parameter(typeof(Exception), "e");
			var convertedParam = Expression.Convert(instanceParam, LoggerType);

			var methodCall = Expression.Call(convertedParam, methodInfo, new Expression[] { messageParam, exceptionParam });

			return (Action<object, string, Exception>)Expression.Lambda(methodCall, instanceParam, messageParam, exceptionParam).Compile();
		}

		/// <summary>
		/// Gets the method information.
		/// </summary>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns>MethodInfo.</returns>
		private static MethodInfo GetMethodInfo(string methodName, Type[] parameters)
		{
			return LoggerType.GetMethod(methodName, parameters);
		}

		#endregion Private methods
	}
}