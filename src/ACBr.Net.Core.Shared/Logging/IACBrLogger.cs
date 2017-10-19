// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 11-08-2013
// ***********************************************************************
// <copyright file="IACBrLogger.cs" company="ACBr.Net">
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

namespace ACBr.Net.Core.Logging
{
	/// <summary>
	/// Interface IACBrLogger
	/// </summary>
	public interface IACBrLogger
	{
		/// <summary>
		/// Gets a value indicating whether this instance is error enabled.
		/// </summary>
		/// <value><c>true</c> if this instance is error enabled; otherwise, <c>false</c>.</value>
		bool IsErrorEnabled { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is fatal enabled.
		/// </summary>
		/// <value><c>true</c> if this instance is fatal enabled; otherwise, <c>false</c>.</value>
		bool IsFatalEnabled { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is debug enabled.
		/// </summary>
		/// <value><c>true</c> if this instance is debug enabled; otherwise, <c>false</c>.</value>
		bool IsDebugEnabled { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is information enabled.
		/// </summary>
		/// <value><c>true</c> if this instance is information enabled; otherwise, <c>false</c>.</value>
		bool IsInfoEnabled { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is warn enabled.
		/// </summary>
		/// <value><c>true</c> if this instance is warn enabled; otherwise, <c>false</c>.</value>
		bool IsWarnEnabled { get; }

		/// <summary>
		/// Errors the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		void Error(object message);

		/// <summary>
		/// Errors the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception.</param>
		void Error(object message, Exception exception);

		/// <summary>
		/// Errors the format.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The arguments.</param>
		void ErrorFormat(string format, params object[] args);

		/// <summary>
		/// Fatals the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		void Fatal(object message);

		/// <summary>
		/// Fatals the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception.</param>
		void Fatal(object message, Exception exception);

		/// <summary>
		/// Debugs the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		void Debug(object message);

		/// <summary>
		/// Debugs the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception.</param>
		void Debug(object message, Exception exception);

		/// <summary>
		/// Debugs the format.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The arguments.</param>
		void DebugFormat(string format, params object[] args);

		/// <summary>
		/// Informations the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		void Info(object message);

		/// <summary>
		/// Informations the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception.</param>
		void Info(object message, Exception exception);

		/// <summary>
		/// Informations the format.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The arguments.</param>
		void InfoFormat(string format, params object[] args);

		/// <summary>
		/// Warns the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		void Warn(object message);

		/// <summary>
		/// Warns the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception.</param>
		void Warn(object message, Exception exception);

		/// <summary>
		/// Warns the format.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The arguments.</param>
		void WarnFormat(string format, params object[] args);
	}
}