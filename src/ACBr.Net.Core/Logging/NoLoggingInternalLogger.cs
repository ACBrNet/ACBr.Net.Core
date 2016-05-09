// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="NoLoggingInternalLogger.cs" company="ACBr.Net">
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

namespace ACBr.Net.Core.Logging
{
    /// <summary>
    /// Classe NoLoggingInternalLogger.
    /// </summary>
	public class NoLoggingInternalLogger : IInternalLogger
	{
        /// <summary>
        /// Gets a value indicating whether this instance is error enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is error enabled; otherwise, <c>false</c>.</value>
		public bool IsErrorEnabled
		{
			get { return false; }
		}

        /// <summary>
        /// Gets a value indicating whether this instance is fatal enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is fatal enabled; otherwise, <c>false</c>.</value>
		public bool IsFatalEnabled
		{
			get { return false; }
		}

        /// <summary>
        /// Gets a value indicating whether this instance is debug enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is debug enabled; otherwise, <c>false</c>.</value>
		public bool IsDebugEnabled
		{
			get { return false; }
		}

        /// <summary>
        /// Gets a value indicating whether this instance is information enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is information enabled; otherwise, <c>false</c>.</value>
		public bool IsInfoEnabled
		{
			get { return false; }
		}

        /// <summary>
        /// Gets a value indicating whether this instance is warn enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is warn enabled; otherwise, <c>false</c>.</value>
		public bool IsWarnEnabled
		{
			get { return false; }
		}

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
		public void Error(object message)
		{
		}

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
		public void Error(object message, Exception exception)
		{
		}

        /// <summary>
        /// Errors the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
		public void ErrorFormat(string format, params object[] args)
		{
		}

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
		public void Fatal(object message)
		{
		}

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
		public void Fatal(object message, Exception exception)
		{
		}

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
		public void Debug(object message)
		{
		}

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
		public void Debug(object message, Exception exception)
		{
		}

        /// <summary>
        /// Debugs the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
		public void DebugFormat(string format, params object[] args)
		{
		}

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
		public void Info(object message)
		{
		}

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
		public void Info(object message, Exception exception)
		{
		}

        /// <summary>
        /// Informations the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
		public void InfoFormat(string format, params object[] args)
		{
		}

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
		public void Warn(object message)
		{
		}

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
		public void Warn(object message, Exception exception)
		{
		}

        /// <summary>
        /// Warns the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
		public void WarnFormat(string format, params object[] args)
		{
		}
	}
}
