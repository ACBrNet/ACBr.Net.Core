// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-21-2014
//
// Last Modified By : RFTD
// Last Modified On : 01-30-2015
// ***********************************************************************
// <copyright file="NoLoggingInternalLogger.cs" company="ACBr.Net">
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
