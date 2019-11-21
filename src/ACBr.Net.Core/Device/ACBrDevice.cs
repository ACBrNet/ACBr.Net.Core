// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 20-12-2018
//
// Last Modified By : RFTD
// Last Modified On : 20-12-2018
// ***********************************************************************
// <copyright file="ACBrDevice.cs" company="ACBr.Net">
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
using System.Text;

namespace ACBr.Net.Core.Device
{
    public abstract class ACBrDevice : IDisposable
    {
        #region Constructors

        protected ACBrDevice(ACBrDeviceConfig config)
        {
            Config = config;
        }

        ~ACBrDevice()
        {
            Dispose(false);
        }

        #endregion Constructors

        #region Properties

        public ACBrDeviceConfig Config { get; protected set; }

        #endregion Properties

        #region Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="dados"></param>
        /// <returns></returns>
        protected virtual byte[] WriteConvert(byte[] dados)
        {
            return Encoding.Convert(Encoding.UTF8, Config.Encoding, dados);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dados"></param>
        /// <returns></returns>
        protected virtual byte[] ReadConvert(byte[] dados)
        {
            return Encoding.Convert(Config.Encoding, Encoding.UTF8, dados);
        }

        public abstract bool Ativar();

        public abstract bool Desativar();

        public abstract void SendCommand(byte[] dados);

        public abstract byte[] GetResposta();

        protected abstract void Dispose(bool disposing);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Methods
    }
}