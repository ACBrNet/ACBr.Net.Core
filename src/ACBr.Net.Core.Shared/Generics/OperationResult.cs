// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 04-12-2018
//
// Last Modified By : RFTD
// Last Modified On : 04-12-2018
// ***********************************************************************
// <copyright file="OperationResult.cs" company="ACBr.Net">
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
using ACBr.Net.Core.Extensions;

namespace ACBr.Net.Core.Generics
{
    /// <summary>
    /// Class OperationResult.
    /// </summary>
    /// <typeparam name="TResult">The type of the t result.</typeparam>
    public class OperationResult<TResult>
    {
        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="OperationResult{TResult}"/> class from being created.
        /// </summary>
        private OperationResult()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this <see cref="OperationResult{TResult}"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success { get; private set; }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>The result.</value>
        public TResult Result { get; private set; }

        /// <summary>
        /// Gets the non success message.
        /// </summary>
        /// <value>The non success message.</value>
        public string FailureMessage { get; private set; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates the success result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>OperationResult&lt;TResult&gt;.</returns>
        public static OperationResult<TResult> CreateSuccessResult(TResult result)
        {
            return new OperationResult<TResult> { Success = true, Result = result };
        }

        /// <summary>
        /// Creates the failure.
        /// </summary>
        /// <param name="failureMessage">The non success message.</param>
        /// <returns>OperationResult&lt;TResult&gt;.</returns>
        public static OperationResult<TResult> CreateFailure(string failureMessage)
        {
            return new OperationResult<TResult> { Success = false, FailureMessage = failureMessage };
        }

        /// <summary>
        /// Creates the failure.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="failureMessage"></param>
        /// <returns>OperationResult&lt;TResult&gt;.</returns>
        public static OperationResult<TResult> CreateFailure(Exception ex, string failureMessage = "")
        {
            return new OperationResult<TResult>
            {
                Success = false,
                FailureMessage = failureMessage.IsEmpty() ? string.Format("{0}{1}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace) :
                                                                  failureMessage,
                Exception = ex
            };
        }

        #endregion Methods
    }
}