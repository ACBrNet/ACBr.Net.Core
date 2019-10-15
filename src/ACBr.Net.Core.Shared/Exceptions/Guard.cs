// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 01-31-2016
//
// Last Modified By : RFTD
// Last Modified On : 03-24-2016
// ***********************************************************************
// <copyright file="Guard.cs" company="ACBr.Net">
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

namespace ACBr.Net.Core
{
    /// <summary>
    /// Helper class for guard statements, which allow prettier
    /// code for guard clauses
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Will throw a <see cref="InvalidOperationException" /> if the assertion
        /// is true, with the specificied message.
        /// </summary>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <example>
        /// Sample usage:
        /// <code><![CDATA[
        /// Guard.Against(string.IsNullOrEmpty(name), "Name must have a value");
        /// ]]></code></example>

        public static void Against(bool assertion, string message = "")
        {
            if (assertion == false) return;
            throw new InvalidOperationException(message);
        }

        /// <summary>
        /// Will throw a <see cref="InvalidOperationException" /> if the assertion
        /// is true, with the specificied message.
        /// </summary>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <param name="args">Parametros da mensagem</param>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <example>
        /// Sample usage:
        /// <code><![CDATA[
        /// Guard.Against(string.IsNullOrEmpty(name), "Name must have a value");
        /// ]]></code></example>
        public static void Against(bool assertion, string message, params object[] args)
        {
            if (assertion == false) return;
            throw new InvalidOperationException(string.Format(message, args));
        }

        /// <summary>
        /// Will throw exception of type <typeparamref name="TException" />
        /// with the specified message if the assertion is true
        /// </summary>
        /// <typeparam name="TException">The type of the t exception.</typeparam>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <param name="beforeThowAction">The action.</param>
        /// <example>
        /// Sample usage:
        /// <code><![CDATA[
        /// Guard.Against<ArgumentException>(string.IsNullOrEmpty(name), "Name must have a value", (ex) => Logger.Erro("Name must have a value"));
        /// ]]></code></example>
        public static void Against<TException>(bool assertion, string message = "", Action<TException> beforeThowAction = null) where TException : Exception
        {
            if (assertion == false) return;
            var exception = (TException)Activator.CreateInstance(typeof(TException), message);
            beforeThowAction?.Invoke(exception);
            throw exception;
        }

        /// <summary>
        /// Will throw exception of type <typeparamref name="TException" />
        /// with the specified message if the assertion is true
        /// </summary>
        /// <typeparam name="TException">The type of the t exception.</typeparam>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        /// <example>
        /// Sample usage:
        /// <code><![CDATA[
        /// Guard.Against<ArgumentException>(string.IsNullOrEmpty(name), "{0} must have a value", Object);
        /// ]]></code></example>
        public static void Against<TException>(bool assertion, string message, params object[] args) where TException : Exception
        {
            if (assertion == false) return;
            throw (TException)Activator.CreateInstance(typeof(TException), string.Format(message, args));
        }
    }
}